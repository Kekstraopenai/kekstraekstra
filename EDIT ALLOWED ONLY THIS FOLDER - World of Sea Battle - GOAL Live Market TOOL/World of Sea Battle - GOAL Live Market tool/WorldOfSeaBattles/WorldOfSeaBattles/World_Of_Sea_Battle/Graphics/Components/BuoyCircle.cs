using System;
using System.Collections.Generic;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x0200046E RID: 1134
	public class BuoyCircle
	{
		// Token: 0x060018A9 RID: 6313 RVA: 0x000D4E05 File Offset: 0x000D3005
		public void Clean()
		{
			this.VisibleInstances.Size = 0;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x000D4E14 File Offset: 0x000D3014
		public void Update(Vector2 {23722}, float {23723}, ModelTransformedScene {23724})
		{
			int num = (int)Math.Round((double)({23723} / 2.5f));
			BuoyCircle.tempList.Size = 0;
			float num2 = 6.2831855f / (float)num;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = {23722} + new Vector2({23723} * MathF.Cos(num2 * (float)i), {23723} * MathF.Sin(num2 * (float)i));
				BuoyCircle.tempList.Add(vector);
			}
			this.Update(BuoyCircle.tempList, {23724});
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x000D4E8C File Offset: 0x000D308C
		public void Update(Tlist<Vector2> {23725}, ModelTransformedScene {23726})
		{
			Vector2 value = (Global.Player == null) ? Engine.GS.Camera.Position.XZ() : Global.Player.Position;
			this.VisibleInstances.Size = 0;
			for (int i = 0; i < {23725}.Size; i++)
			{
				Vector2 vector = {23725}.Array[i];
				float num = Vector2.Distance(vector, value);
				float num2 = Math.Min(1f - Geometry.InverseLerp(200f, 210f, num), Geometry.InverseLerp(7f, 14f, num));
				if (num2 > 0.01f && Vector3.Dot(Engine.GS.Camera.Position - vector.X0Y(), Engine.GS.Camera.Direction) < 0f)
				{
					Vector3 vector2;
					float num3;
					CommonGlobal.CurrentClientWeather.NormalAndHeightHelper((Global.Player == null) ? Gameplay.WorldMap : Global.Player.MapInfo, vector.X, vector.Y, out vector2, out num3);
					{23726}.Transform.Translation.X = vector.X;
					{23726}.Transform.Translation.Y = num3 + 0.15f;
					{23726}.Transform.Translation.Z = vector.Y;
					{23726}.Transform.Roll = -vector2.Z;
					{23726}.Transform.Pitch = -vector2.X;
					{23726}.Transform.MiddleScale = 0.08f + 0.03f * Math.Min(1f, num / 150f);
					Tlist<BuoyCircle.RenderInstance> visibleInstances = this.VisibleInstances;
					BuoyCircle.RenderInstance renderInstance = new BuoyCircle.RenderInstance({23726}, new Vector3(vector.X, num3 + 0.15f, vector.Y), 0.08f + 0.03f * Math.Min(1f, num / 150f), num2, new Vector3(-vector2.X, 0f, -vector2.Z), i, {23726}.Transform.Transform3X3(new Vector3(0f, 25f, 0f)));
					visibleInstances.Add(renderInstance);
				}
			}
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x000D50B8 File Offset: 0x000D32B8
		public void Draw(bool {23727})
		{
			foreach (BuoyCircle.RenderInstance renderInstance in ((IEnumerable<BuoyCircle.RenderInstance>)this.VisibleInstances))
			{
				if (renderInstance.Transparancy == 1f != {23727})
				{
					renderInstance.Model.Transform.Translation = renderInstance.Position;
					renderInstance.Model.Transform.RotatesAll = renderInstance.Rotates;
					renderInstance.Model.Transform.MiddleScale = renderInstance.Scale;
					Global.Render.CommonShader.RenderObject(renderInstance.Model, false, renderInstance.Transparancy, false, 0f, false);
				}
			}
		}

		// Token: 0x0400170F RID: 5903
		public Tlist<BuoyCircle.RenderInstance> VisibleInstances = new Tlist<BuoyCircle.RenderInstance>();

		// Token: 0x04001710 RID: 5904
		private static Tlist<Vector2> tempList = new Tlist<Vector2>();

		// Token: 0x0200046F RID: 1135
		public struct RenderInstance
		{
			// Token: 0x060018AF RID: 6319 RVA: 0x000D5197 File Offset: 0x000D3397
			public RenderInstance(ModelTransformedScene {23735}, Vector3 {23736}, float {23737}, float {23738}, Vector3 {23739}, int {23740}, Vector3 {23741})
			{
				this.Model = {23735};
				this.Position = {23736};
				this.Scale = {23737};
				this.Transparancy = {23738};
				this.Rotates = {23739};
				this.UnitqueKey = {23740};
				this.LightPosition = {23741};
			}

			// Token: 0x04001711 RID: 5905
			public ModelTransformedScene Model;

			// Token: 0x04001712 RID: 5906
			public Vector3 Position;

			// Token: 0x04001713 RID: 5907
			public float Scale;

			// Token: 0x04001714 RID: 5908
			public float Transparancy;

			// Token: 0x04001715 RID: 5909
			public Vector3 Rotates;

			// Token: 0x04001716 RID: 5910
			public int UnitqueKey;

			// Token: 0x04001717 RID: 5911
			public Vector3 LightPosition;
		}
	}
}
