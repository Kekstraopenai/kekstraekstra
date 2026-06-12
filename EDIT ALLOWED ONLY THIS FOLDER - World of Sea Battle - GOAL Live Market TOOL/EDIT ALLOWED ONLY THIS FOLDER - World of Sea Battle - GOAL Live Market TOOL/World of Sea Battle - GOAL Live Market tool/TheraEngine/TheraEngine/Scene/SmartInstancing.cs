using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;

namespace TheraEngine.Scene
{
	// Token: 0x0200003A RID: 58
	public class SmartInstancing : IDisposable
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00009918 File Offset: 0x00007B18
		public SmartInstancing()
		{
			this.{11888} = new Tlist<SmartInstancing.Item>();
			this.{11889} = new Dictionary<Material, Dictionary<UWModel, SmartInstancing.Drawcall>>();
			this.{11890} = new Stack<SmartInstancing.Drawcall>();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000997B File Offset: 0x00007B7B
		public void Add(in SmartInstancing.Item {11879})
		{
			if (float.IsNaN({11879}.Pos.M11))
			{
				throw new ArgumentException("item has NaN matrix");
			}
			this.{11888}.Add({11879});
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000099A8 File Offset: 0x00007BA8
		public void Update(Vector3 {11880}, Matrix {11881})
		{
			if (this.{11893} > 20)
			{
				return;
			}
			if (this.{11888}.Array.Length > this.{11888}.Size + 1000)
			{
				this.{11888}.Trim();
			}
			this.{11892} -= Engine.Game.GameTime.ElapsedUpdate;
			if (this.{11892} > 0f)
			{
				return;
			}
			float num = this.Lod0Distance * this.Lod0Distance;
			float num2 = this.Lod1Distance * this.Lod1Distance;
			float num3 = this.ThrottlingEndDistance * this.ThrottlingEndDistance;
			int num4 = 0;
			int num5 = this.{11888}.Size;
			if (this.UpdateOptimization != 2)
			{
				if (num5 > 10)
				{
					int num6 = Math.Max(1, this.{11888}.Size / 20);
					num4 = num6 * this.{11891};
					num5 = Math.Min(num6, num5 - num4);
					this.{11891}++;
					if (this.{11891} * num6 > this.{11888}.Size)
					{
						this.{11891} = 0;
						if (this.UpdateOptimization == 1)
						{
							this.{11892} = Rand.Range(2000f, 2020f);
						}
					}
				}
				else
				{
					this.{11892} = Rand.Range(2000f, 2020f);
				}
			}
			if (this.EnableVariableLod)
			{
				{11880}.Y = 0f;
			}
			for (int i = num4; i < num4 + num5; i++)
			{
				ref SmartInstancing.Item ptr = ref this.{11888}.Array[i];
				Vector3 vector;
				Vector3.Transform(ref ptr.Translation, ref {11881}, out vector);
				if (this.EnableVariableLod)
				{
					vector.Y *= 6f;
				}
				float num7;
				Vector3.DistanceSquared(ref {11880}, ref vector, out num7);
				UWModel uwmodel = null;
				if (this.AlwaysMinLod)
				{
					if (num7 < num3)
					{
						uwmodel = ptr.Lod2;
					}
				}
				else if (num7 < num)
				{
					uwmodel = ptr.Lod0;
				}
				else if (num7 < num2)
				{
					uwmodel = ptr.Lod1;
				}
				else if (num7 < num3)
				{
					uwmodel = ptr.Lod2;
				}
				double num8 = 1.0 - (double)(num7 / (this.ThrottlingEndDistance * this.ThrottlingEndDistance));
				if (ptr.CanBeThrottled && (num8 < (double)(ptr.RandomValue * ptr.RandomValue) || this.MinThrottling > ptr.RandomValue))
				{
					uwmodel = null;
				}
				if (uwmodel != ptr.State)
				{
					if (ptr.State != null)
					{
						Material material = ptr.State.Drawcalls[0].Material;
						Dictionary<UWModel, SmartInstancing.Drawcall> dictionary;
						SmartInstancing.Drawcall drawcall;
						if (this.{11889}.TryGetValue(material, out dictionary) && dictionary.TryGetValue(ptr.State, out drawcall))
						{
							if (!drawcall.Transforms.FastRemove(ptr.Pos) && Debugger.IsAttached)
							{
								throw new InvalidOperationException("Fatal error occured on SmartInstancing");
							}
							drawcall.OperatingTransformsCount = 0;
							if (drawcall.Transforms.Size == 0)
							{
								this.{11890}.Push(drawcall);
								dictionary.Remove(ptr.State);
							}
						}
					}
					if (uwmodel != null)
					{
						Material material2 = uwmodel.Drawcalls[0].Material;
						Dictionary<UWModel, SmartInstancing.Drawcall> dictionary2;
						if (!this.{11889}.TryGetValue(material2, out dictionary2))
						{
							dictionary2 = new Dictionary<UWModel, SmartInstancing.Drawcall>();
							this.{11889}.Add(material2, dictionary2);
						}
						SmartInstancing.Drawcall drawcall2;
						if (!dictionary2.TryGetValue(uwmodel, out drawcall2))
						{
							drawcall2 = ((this.{11890}.Count == 0) ? new SmartInstancing.Drawcall() : this.{11890}.Pop());
							dictionary2.Add(uwmodel, drawcall2);
							drawcall2.OperatingTransformsCount = 0;
						}
						drawcall2.Transforms.Add(ptr.Pos);
						drawcall2.OperatingTransformsCount = 0;
					}
					ptr.State = uwmodel;
				}
			}
			int num9 = 1000000;
			foreach (KeyValuePair<Material, Dictionary<UWModel, SmartInstancing.Drawcall>> keyValuePair in this.{11889})
			{
				foreach (KeyValuePair<UWModel, SmartInstancing.Drawcall> keyValuePair2 in keyValuePair.Value)
				{
					SmartInstancing.Drawcall value = keyValuePair2.Value;
					if (value.Transforms.Size != 0)
					{
						if (value.IndividualBuffer == null || value.Transforms.Size > value.IndividualBuffer.VertexCount || value.IndividualBuffer.VertexCount - num9 > value.Transforms.Size)
						{
							DynamicVertexBuffer individualBuffer = value.IndividualBuffer;
							if (individualBuffer != null)
							{
								individualBuffer.Dispose();
							}
							try
							{
								value.IndividualBuffer = new DynamicVertexBuffer(Engine.Game.GraphicsDevice, InstancedModelRenderer.instanceVertexDeclaration, value.Transforms.Size, BufferUsage.WriteOnly);
							}
							catch (InvalidOperationException)
							{
								value.IndividualBuffer = null;
								this.{11893}++;
								continue;
							}
						}
						if (value.OperatingTransformsCount != value.Transforms.Size)
						{
							value.IndividualBuffer.SetData<Matrix>(value.Transforms.Array, 0, value.Transforms.Size, SetDataOptions.None);
							value.OperatingTransformsCount = value.Transforms.Size;
						}
						if (value.Transforms.Array.Length - num9 > value.Transforms.Size)
						{
							value.Transforms.Trim();
						}
					}
				}
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009F30 File Offset: 0x00008130
		public void Draw(ISceneObject3DParent {11882}, ModelTransformedScene {11883}, int {11884} = 1)
		{
			if (this.{11889}.Count == 0)
			{
				return;
			}
			Matrix matrix;
			{11883}.Transform.CreateWorldMatrix(out matrix);
			{11882}.SetWorld(ref matrix, null, {11883});
			foreach (KeyValuePair<Material, Dictionary<UWModel, SmartInstancing.Drawcall>> keyValuePair in this.{11889})
			{
				bool flag = false;
				foreach (KeyValuePair<UWModel, SmartInstancing.Drawcall> keyValuePair2 in keyValuePair.Value)
				{
					if (keyValuePair2.Value.OperatingTransformsCount != 0 && keyValuePair2.Value.IndividualBuffer != null)
					{
						if (!flag)
						{
							{11882}.SetForPart(keyValuePair2.Key.Drawcalls.First<ModelPartShadercall>());
							{11882}.ApplyPass(keyValuePair2.Key.Drawcalls.First<ModelPartShadercall>().GeometryType);
							flag = true;
						}
						MeshPartData meshPartData = keyValuePair2.Key.Drawcalls.First<ModelPartShadercall>().Parts.First();
						Engine.GS.RenderInstancing(meshPartData.vertexStartIndex, meshPartData.vertexOffset, keyValuePair2.Value.IndividualBuffer, meshPartData.XnaVertexBuffer, meshPartData.XnaIndexBuffer, meshPartData.verticesCount, meshPartData.PrimitiveCount, keyValuePair2.Value.OperatingTransformsCount, {11884});
					}
				}
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000A0CC File Offset: 0x000082CC
		public void Draw(IGBufferBuilder {11885}, ModelTransformedScene {11886})
		{
			if (this.{11889}.Count == 0)
			{
				return;
			}
			Matrix matrix;
			{11886}.Transform.CreateWorldMatrix(out matrix);
			{11885}.ApplyPass(ref matrix, true);
			foreach (KeyValuePair<Material, Dictionary<UWModel, SmartInstancing.Drawcall>> keyValuePair in this.{11889})
			{
				if (keyValuePair.Key.Properties.RasterizerOptions != MaterialRasterizeOptions.AlphaMaterialWithoutShadow)
				{
					if ({11885}.MaterialAnalyzingEnable)
					{
						Texture2D tex = keyValuePair.Key.Albedo.Tex;
						if (tex.Format == SurfaceFormat.Dxt3 || tex.Format == SurfaceFormat.Dxt5 || tex.Format == SurfaceFormat.Color)
						{
							{11885}.RestartPassMaterialAnalyze(keyValuePair.Key.Albedo.Tex);
						}
						else
						{
							{11885}.RestartPassMaterialAnalyze(null);
						}
					}
					foreach (KeyValuePair<UWModel, SmartInstancing.Drawcall> keyValuePair2 in keyValuePair.Value)
					{
						if (keyValuePair2.Value.OperatingTransformsCount != 0 && keyValuePair2.Value.IndividualBuffer != null)
						{
							MeshPartData meshPartData = keyValuePair2.Key.Drawcalls.First<ModelPartShadercall>().Parts.First();
							Engine.GS.RenderInstancing(meshPartData.vertexStartIndex, meshPartData.vertexOffset, keyValuePair2.Value.IndividualBuffer, meshPartData.XnaVertexBuffer, meshPartData.XnaIndexBuffer, meshPartData.verticesCount, meshPartData.PrimitiveCount, keyValuePair2.Value.OperatingTransformsCount, 1);
						}
					}
				}
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000A290 File Offset: 0x00008490
		private void {11887}()
		{
			foreach (KeyValuePair<Material, Dictionary<UWModel, SmartInstancing.Drawcall>> keyValuePair in this.{11889})
			{
				foreach (KeyValuePair<UWModel, SmartInstancing.Drawcall> keyValuePair2 in keyValuePair.Value)
				{
					keyValuePair2.Value.Transforms.Clear();
					this.{11890}.Push(keyValuePair2.Value);
				}
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000A33C File Offset: 0x0000853C
		public void Dispose()
		{
			this.{11887}();
			while (this.{11890}.Count > 0)
			{
				DynamicVertexBuffer individualBuffer = this.{11890}.Pop().IndividualBuffer;
				if (individualBuffer != null)
				{
					individualBuffer.Dispose();
				}
			}
			this.{11889}.Clear();
			this.{11888}.Clear();
		}

		// Token: 0x04000105 RID: 261
		private Tlist<SmartInstancing.Item> {11888};

		// Token: 0x04000106 RID: 262
		private Dictionary<Material, Dictionary<UWModel, SmartInstancing.Drawcall>> {11889};

		// Token: 0x04000107 RID: 263
		private Stack<SmartInstancing.Drawcall> {11890};

		// Token: 0x04000108 RID: 264
		private int {11891};

		// Token: 0x04000109 RID: 265
		private float {11892};

		// Token: 0x0400010A RID: 266
		private int {11893};

		// Token: 0x0400010B RID: 267
		public float Lod0Distance = 50f;

		// Token: 0x0400010C RID: 268
		public float Lod1Distance = 200f;

		// Token: 0x0400010D RID: 269
		public float ThrottlingEndDistance = 500f;

		// Token: 0x0400010E RID: 270
		public bool EnableVariableLod = true;

		// Token: 0x0400010F RID: 271
		public bool AlwaysMinLod;

		// Token: 0x04000110 RID: 272
		public float MinThrottling;

		// Token: 0x04000111 RID: 273
		public int UpdateOptimization = 1;

		// Token: 0x0200003B RID: 59
		public struct Item
		{
			// Token: 0x060001BF RID: 447 RVA: 0x0000A390 File Offset: 0x00008590
			public Item(UWModel {11898}, UWModel {11899}, UWModel {11900}, ref Matrix {11901})
			{
				this.State = null;
				this.Lod0 = {11898};
				this.Lod1 = {11899};
				this.Lod2 = {11900};
				this.Pos = {11901};
				this.Translation = {11901}.Translation;
				this.RandomValue = Rand.Range(0f, 1f);
				this.CanBeThrottled = ({11898}.CommonSphere.Radius < 20f);
			}

			// Token: 0x04000112 RID: 274
			public UWModel Lod0;

			// Token: 0x04000113 RID: 275
			public UWModel Lod1;

			// Token: 0x04000114 RID: 276
			public UWModel Lod2;

			// Token: 0x04000115 RID: 277
			public Matrix Pos;

			// Token: 0x04000116 RID: 278
			public Vector3 Translation;

			// Token: 0x04000117 RID: 279
			public float RandomValue;

			// Token: 0x04000118 RID: 280
			public bool CanBeThrottled;

			// Token: 0x04000119 RID: 281
			internal UWModel State;
		}

		// Token: 0x0200003C RID: 60
		private class Drawcall
		{
			// Token: 0x060001C0 RID: 448 RVA: 0x0000A400 File Offset: 0x00008600
			public Drawcall()
			{
				this.Transforms = new Tlist<Matrix>(4);
				this.IndividualBuffer = null;
			}

			// Token: 0x0400011A RID: 282
			public Tlist<Matrix> Transforms;

			// Token: 0x0400011B RID: 283
			public DynamicVertexBuffer IndividualBuffer;

			// Token: 0x0400011C RID: 284
			public int OperatingTransformsCount;
		}
	}
}
