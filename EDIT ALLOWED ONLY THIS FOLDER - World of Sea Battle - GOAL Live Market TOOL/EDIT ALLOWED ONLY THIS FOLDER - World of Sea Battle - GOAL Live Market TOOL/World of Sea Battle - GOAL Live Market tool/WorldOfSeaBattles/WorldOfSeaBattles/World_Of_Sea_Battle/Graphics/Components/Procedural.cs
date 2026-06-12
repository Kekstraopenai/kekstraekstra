using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Game;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using UWContentPipelineExtensionRuntime;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x02000475 RID: 1141
	public class Procedural
	{
		// Token: 0x060018F1 RID: 6385 RVA: 0x000D9D0C File Offset: 0x000D7F0C
		public Procedural(string {23940}, ModelHardpoint[] {23941}, float {23942}, Matrix {23943})
		{
			this.{23971} = {23940};
			this.{23970} = {23942};
			this.{23962} = {23941};
			this.{23975} = {23943};
			this.{23976} = Gcc.GetExactIsleScale({23940});
			this.Scene = new SmartInstancing();
			Vector2 vector = default(Vector2);
			Vector2 vector2 = default(Vector2);
			for (int i = 0; i < {23941}.Length; i++)
			{
				Vector2 vector3 = {23941}[i].Transform.Translation.XZ();
				if (i == 0)
				{
					vector = vector3;
					vector2 = vector3;
				}
				else
				{
					vector = Vector2.Min(vector, vector3);
					vector2 = Vector2.Max(vector2, vector3);
				}
			}
			QuadTree.Area bounds = new QuadTree.Area(vector.X, vector.Y, vector2.X - vector.X, vector2.Y - vector.Y);
			this.{23973} = new GenericQuadTree<ModelHardpoint>(bounds);
			foreach (ModelHardpoint modelHardpoint in {23941})
			{
				float num = 1f;
				ModelHardpoint modelHardpoint2 = this.{23973}.FindClosest(modelHardpoint.Position, ref num);
				if (modelHardpoint2 == null || !(modelHardpoint2.Position == modelHardpoint.Position))
				{
					this.{23973}.Insert(modelHardpoint);
				}
			}
			this.{23974} = new GenericQuadTree<Procedural.Shape>(bounds);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x000D9E58 File Offset: 0x000D8058
		public void Append(GeneratorLayer {23944}, GeneratorPreset {23945}, int {23946} = 0)
		{
			if ({23944}.ModelKey.Contains("Rock_big_as"))
			{
				{23944}.Height.Item1 = 1f;
				{23944}.RandomAngles = new Vector3(0f, 1f, 0f);
			}
			this.{23972} = {23945}.Name;
			this.{23961} = new Sequence({23945}.Seed + {23946});
			this.{23964} = {23944};
			this.{23965} = {23944}.Chance;
			this.{23966} = (float){23944}.Quantity;
			this.{23968} = {23945}.RemovePointsInRadius;
			string[] exceptKeys = null;
			if ({23944}.ExceptModelKeys != null)
			{
				exceptKeys = {23944}.ExceptModelKeys.Split(",", StringSplitOptions.None);
			}
			UWModel[] source = (from {23993} in LocalContent.Loaded.TESTProceduralModels
			where {23993}.MeshName.Contains({23944}.ModelKey) && (exceptKeys == null || !exceptKeys.Any((string {23994}) => {23993}.MeshName.Contains({23994})))
			select {23993}).ToArray<UWModel>();
			List<UWModel> list = (from {23986} in source
			where !{23986}.MeshName.StartsWith("Lod1") && !{23986}.MeshName.StartsWith("lodhq")
			select {23986} into {23987}
			orderby {23987}.MeshName
			select {23987}).ToList<UWModel>();
			List<UWModel> list2 = (from {23988} in source
			where !{23988}.MeshName.StartsWith("Lod0") && !{23988}.MeshName.StartsWith("lodhq")
			select {23988} into {23989}
			orderby {23989}.MeshName
			select {23989}).ToList<UWModel>();
			List<UWModel> list3 = (from {23990} in source
			where !{23990}.MeshName.StartsWith("Lod0") && !{23990}.MeshName.StartsWith("Lod1")
			select {23990} into {23991}
			orderby {23991}.MeshName
			select {23991}).ToList<UWModel>();
			this.{23969} = new ValueTuple<UWModel, UWModel, UWModel>[list2.Count];
			int count = list.Count;
			int count2 = list2.Count;
			for (int i = 0; i < list.Count; i++)
			{
				string meshName = list[i].MeshName;
				string meshName2 = list2[i].MeshName;
				string meshName3 = list3[i].MeshName;
				if (meshName.IndexOf('_') > 0)
				{
					string text = meshName;
					int num = meshName.IndexOf('_');
					string a = text.Substring(num, text.Length - num);
					text = meshName2;
					num = meshName2.IndexOf('_');
					string b = text.Substring(num, text.Length - num);
					a != b;
				}
				this.{23969}[i] = new ValueTuple<UWModel, UWModel, UWModel>(list[i], list2[i], list3[i]);
			}
			if (this.{23964}.OnlyExtremePoints)
			{
				this.{23967} = Procedural.ConvexHullHelper.PointsNearConvexHull((from {23992} in this.{23962}
				select {23992}.Transform.Translation).ToList<Vector3>(), (double)this.{23964}.ExtremePointsPadding);
			}
			if (this.{23969}.Length != 0)
			{
				this.Append();
			}
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x000DA1A0 File Offset: 0x000D83A0
		private bool {23947}(ModelHardpoint {23948})
		{
			Vector3 translation = {23948}.Transform.Translation;
			Vector3 vector;
			Vector3.Transform(ref translation, ref this.{23975}, out vector);
			float num = vector.Y / this.{23976}.Y;
			return num > this.{23964}.Height.Item1 && num < this.{23964}.Height.Item2 && (this.{23964}.SlopeCheck != SlopeMode.DisableSlope || {23948}.Transform.Up.Normal().Y >= 0.9f) && (this.{23964}.SlopeCheck != SlopeMode.OnlySlope || {23948}.Transform.Up.Normal().Y <= 0.9f) && (!this.{23964}.ConsistenceCheck || this.{23949}({23948}, 50f, 100f, 10f)) && (!this.{23964}.OnlyExtremePoints || this.{23967}.Contains({23948}.Transform.Translation));
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x000DA2A8 File Offset: 0x000D84A8
		public void Append()
		{
			if (this.{23964}.Quantity > 0)
			{
				this.{23965} = 50f;
			}
			int num = 0;
			int num2 = 0;
			while (num2 < this.{23962}.Length && (this.{23964}.Quantity <= 0 || this.{23966} != 0f))
			{
				if (this.{23961}.Chanse(this.{23965}))
				{
					ModelHardpoint modelHardpoint = this.{23962}[num2];
					if (this.{23947}(modelHardpoint))
					{
						num++;
						this.{23954}(ref num2, this.{23964}, modelHardpoint);
						if (this.{23966} > 0f)
						{
							this.{23966} -= 1f;
						}
						while (this.{23964}.NextNearPositionCopyChance > 0f && this.{23961}.Chanse(this.{23964}.NextNearPositionCopyChance) && this.{23964}.NextNearPositionCopyChance < 90f)
						{
							float num3 = 50f;
							ModelHardpoint modelHardpoint2 = this.{23973}.FindClosest(modelHardpoint.Transform.Translation.XZ(), ref num3);
							if (modelHardpoint2 != null)
							{
								this.{23954}(ref num2, this.{23964}, modelHardpoint2);
							}
						}
					}
				}
				num2++;
			}
			if (this.{23966} > 0f && num > 0)
			{
				this.Append();
			}
			if (this.{23965} <= 100f)
			{
				return;
			}
			this.{23965} -= 100f;
			this.Append();
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000DA418 File Offset: 0x000D8618
		private bool {23949}(ModelHardpoint {23950}, float {23951}, float {23952}, float {23953})
		{
			float num = 0f;
			float num2 = {23950}.Transform.Translation.Y;
			float num3 = {23950}.Transform.Translation.Y;
			int num4 = 0;
			Vector2 vector = {23950}.Transform.Translation.XZ();
			QuadTree.Area area = new QuadTree.Area(vector.X - {23951}, vector.Y - {23951}, {23951} * 2f, {23951} * 2f);
			this.{23963}.Clear();
			this.{23973}.FetchPointsInArea(ref area, this.{23963});
			for (int i = 0; i < this.{23963}.Count; i++)
			{
				float y = this.{23963}[i].Transform.Translation.Y;
				num += y;
				num4++;
				num2 = Math.Min(num2, y);
				num3 = Math.Max(num3, y);
			}
			return num4 == 0 || (Math.Abs(num / (float)num4 - {23950}.Transform.Translation.Y) < {23952} && num3 - num2 < {23953});
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x000DA524 File Offset: 0x000D8724
		private void {23954}(ref int {23955}, in GeneratorLayer {23956}, ModelHardpoint {23957})
		{
			Vector3 vector = {23957}.Transform.Translation;
			ValueTuple<UWModel, UWModel, UWModel> valueTuple = this.{23961}.Pick<ValueTuple<UWModel, UWModel, UWModel>>(this.{23969});
			float num = this.{23961}.Range({23956}.Scale.Start, {23956}.Scale.End);
			num /= this.{23970};
			num *= 1.05f;
			float num2 = {23956}.UseCommonRemovePointsInRadius ? this.{23968} : {23956}.RemovePointsInRadius;
			float num3 = valueTuple.Item1.CommonSphere.Radius * num * num2;
			Vector3 vector2 = Vector3.Zero;
			if ({23956}.ShufflePosition > 0f)
			{
				float shufflePosition = {23956}.ShufflePosition;
				Vector2 vector3 = {23957}.Transform.Translation.XZ();
				QuadTree.Area area = new QuadTree.Area(vector3.X - shufflePosition, vector3.Y - shufflePosition, shufflePosition * 2f, shufflePosition * 2f);
				this.{23963}.Clear();
				this.{23973}.FetchPointsInArea(ref area, this.{23963});
				if (this.{23963}.Count > 0)
				{
					ModelHardpoint modelHardpoint;
					for (;;)
					{
						modelHardpoint = this.{23963}[this.{23961}.RangeInt(0, this.{23963}.Count)];
						if (modelHardpoint.Transform.Translation != vector)
						{
							break;
						}
						if (this.{23963}.Count == 1)
						{
							goto IL_16E;
						}
					}
					vector2 = (modelHardpoint.Transform.Translation - vector).Normal() * shufflePosition;
				}
			}
			IL_16E:
			vector += vector2;
			float {23959} = num3;
			Vector2 vector4 = vector.XZ();
			if (this.{23958}({23959}, vector4))
			{
				return;
			}
			Matrix matrix;
			if (this.{23964}.RandomAngles != Vector3.Zero)
			{
				matrix = Matrix.CreateFromAxisAngle(this.{23964}.RandomAngles, this.{23961}.Range(0f, 360f)) * ({23956}.UseSlopeNormal ? Matrix.CreateScale(num / {23957}.Transform.Up.Length()) : Matrix.CreateScale(num)) * ({23956}.UseSlopeNormal ? {23957}.Transform : Matrix.CreateTranslation(vector));
			}
			else if ({23956}.UseSlopeNormal)
			{
				matrix = Matrix.CreateScale(num / {23957}.Transform.Up.Length()) * {23957}.Transform * Matrix.CreateTranslation(vector2);
			}
			else
			{
				matrix = Matrix.CreateRotationY({23956}.UseAngleByPosition ? MathF.Atan2(vector.Y, vector.X) : this.{23961}.Angle()) * Matrix.CreateScale(num) * Matrix.CreateTranslation(vector);
			}
			SmartInstancing scene = this.Scene;
			SmartInstancing.Item item = new SmartInstancing.Item(valueTuple.Item1, valueTuple.Item1, valueTuple.Item2, ref matrix);
			scene.Add(item);
			this.IsEmpty = false;
			if (num2 > 0f)
			{
				this.{23974}.Insert(new Procedural.Shape(new Vector3(vector.X, vector.Z, num3)));
			}
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000DA828 File Offset: 0x000D8A28
		private bool {23958}(float {23959}, in Vector2 {23960})
		{
			QuadTree.Area area = new QuadTree.Area({23960}.X - {23959}, {23960}.Y - {23959}, {23959} * 2f, {23959} * 2f);
			return this.{23974}.CountPointsInArea(ref area) > 0;
		}

		// Token: 0x0400173C RID: 5948
		public SmartInstancing Scene;

		// Token: 0x0400173D RID: 5949
		public bool IsEmpty;

		// Token: 0x0400173E RID: 5950
		private Sequence {23961};

		// Token: 0x0400173F RID: 5951
		private ModelHardpoint[] {23962};

		// Token: 0x04001740 RID: 5952
		private List<ModelHardpoint> {23963} = new List<ModelHardpoint>(100);

		// Token: 0x04001741 RID: 5953
		private GeneratorLayer {23964};

		// Token: 0x04001742 RID: 5954
		private float {23965};

		// Token: 0x04001743 RID: 5955
		private float {23966};

		// Token: 0x04001744 RID: 5956
		private List<Vector3> {23967};

		// Token: 0x04001745 RID: 5957
		private float {23968};

		// Token: 0x04001746 RID: 5958
		[TupleElementNames(new string[]
		{
			"high",
			"low",
			"hq"
		})]
		private ValueTuple<UWModel, UWModel, UWModel>[] {23969};

		// Token: 0x04001747 RID: 5959
		private readonly float {23970};

		// Token: 0x04001748 RID: 5960
		private readonly string {23971};

		// Token: 0x04001749 RID: 5961
		private string {23972};

		// Token: 0x0400174A RID: 5962
		private GenericQuadTree<ModelHardpoint> {23973};

		// Token: 0x0400174B RID: 5963
		private GenericQuadTree<Procedural.Shape> {23974};

		// Token: 0x0400174C RID: 5964
		private Matrix {23975};

		// Token: 0x0400174D RID: 5965
		private Vector3 {23976};

		// Token: 0x02000476 RID: 1142
		private static class ConvexHullHelper
		{
			// Token: 0x060018F8 RID: 6392 RVA: 0x000DA86A File Offset: 0x000D8A6A
			private static double Orientation(Vector3 {23977}, Vector3 {23978}, Vector3 {23979})
			{
				return (double)(({23978}.Z - {23977}.Z) * ({23979}.X - {23978}.X) - ({23978}.X - {23977}.X) * ({23979}.Z - {23978}.Z));
			}

			// Token: 0x060018F9 RID: 6393 RVA: 0x000DA8A4 File Offset: 0x000D8AA4
			private static List<Vector3> ConvexHull(List<Vector3> {23980})
			{
				if ({23980}.Count < 3)
				{
					return new List<Vector3>({23980});
				}
				List<Vector3> list = new List<Vector3>();
				int num = 0;
				for (int i = 1; i < {23980}.Count; i++)
				{
					if ({23980}[i].X < {23980}[num].X)
					{
						num = i;
					}
				}
				int num2 = num;
				do
				{
					list.Add({23980}[num2]);
					int num3 = (num2 + 1) % {23980}.Count;
					for (int j = 0; j < {23980}.Count; j++)
					{
						if (Procedural.ConvexHullHelper.Orientation({23980}[num2], {23980}[j], {23980}[num3]) < 0.0)
						{
							num3 = j;
						}
					}
					num2 = num3;
				}
				while (num2 != num);
				return list;
			}

			// Token: 0x060018FA RID: 6394 RVA: 0x000DA95C File Offset: 0x000D8B5C
			public static List<Vector3> PointsNearConvexHull(List<Vector3> {23981}, double {23982})
			{
				List<Vector3> list = Procedural.ConvexHullHelper.ConvexHull({23981});
				List<Vector3> list2 = new List<Vector3>();
				foreach (Vector3 vector in {23981})
				{
					float num = float.MaxValue;
					foreach (Vector3 value in list)
					{
						float num2 = Vector3.DistanceSquared(vector, value);
						if (num2 < num)
						{
							num = num2;
						}
					}
					if ((double)num <= {23982})
					{
						list2.Add(vector);
					}
				}
				return list2;
			}
		}

		// Token: 0x02000477 RID: 1143
		private class Shape : IGenericQuadTreeItem
		{
			// Token: 0x17000205 RID: 517
			// (get) Token: 0x060018FB RID: 6395 RVA: 0x000DAA14 File Offset: 0x000D8C14
			public Vector2 Position
			{
				get
				{
					return this.{23985}.XZ();
				}
			}

			// Token: 0x060018FC RID: 6396 RVA: 0x000DAA21 File Offset: 0x000D8C21
			public Shape(Vector3 {23984})
			{
				this.{23985} = {23984};
			}

			// Token: 0x0400174E RID: 5966
			private Vector3 {23985};
		}
	}
}
