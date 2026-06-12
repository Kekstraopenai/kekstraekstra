using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Graphics.Models;

namespace TheraEngine.Scene
{
	// Token: 0x0200003D RID: 61
	public class InstancedModelRenderer : IDisposable
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x0000A41B File Offset: 0x0000861B
		public InstancedModelRenderer()
		{
			this.{11917} = new Dictionary<Material, Dictionary<UWModel, InstancedModelRenderer.Drawcall>>();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000A430 File Offset: 0x00008630
		public void CleanCache()
		{
			foreach (KeyValuePair<Material, Dictionary<UWModel, InstancedModelRenderer.Drawcall>> keyValuePair in this.{11917})
			{
				foreach (KeyValuePair<UWModel, InstancedModelRenderer.Drawcall> keyValuePair2 in keyValuePair.Value)
				{
					DynamicVertexBuffer individualBuffer = keyValuePair2.Value.IndividualBuffer;
					if (individualBuffer != null)
					{
						individualBuffer.Dispose();
					}
				}
				keyValuePair.Value.Clear();
			}
			this.{11917}.Clear();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000A4E8 File Offset: 0x000086E8
		public void AddToCache(UWModel {11902}, ref Matrix {11903})
		{
			Material material = {11902}.Drawcalls.First<ModelPartShadercall>().Material;
			Dictionary<UWModel, InstancedModelRenderer.Drawcall> dictionary;
			if (!this.{11917}.TryGetValue(material, out dictionary))
			{
				dictionary = new Dictionary<UWModel, InstancedModelRenderer.Drawcall>();
				this.{11917}.Add(material, dictionary);
			}
			InstancedModelRenderer.Drawcall drawcall;
			if (!dictionary.TryGetValue({11902}, out drawcall))
			{
				drawcall = new InstancedModelRenderer.Drawcall();
				dictionary.Add({11902}, drawcall);
			}
			drawcall.Transforms.Add({11903});
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000A550 File Offset: 0x00008750
		public void BuildCache()
		{
			if (this.UseCachingStrategy)
			{
				foreach (KeyValuePair<Material, Dictionary<UWModel, InstancedModelRenderer.Drawcall>> keyValuePair in this.{11917})
				{
					foreach (KeyValuePair<UWModel, InstancedModelRenderer.Drawcall> keyValuePair2 in keyValuePair.Value)
					{
						this.{11908}(keyValuePair2.Value, ref keyValuePair2.Value.IndividualBuffer);
					}
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000A5FC File Offset: 0x000087FC
		public void ModifyMatrices(InstancedModelRenderer.RefAction<Matrix> {11904}, int {11905} = 0)
		{
			int num = this.{11918};
			this.{11918} = num + 1;
			int num2 = num % {11905};
			foreach (KeyValuePair<Material, Dictionary<UWModel, InstancedModelRenderer.Drawcall>> keyValuePair in this.{11917})
			{
				foreach (KeyValuePair<UWModel, InstancedModelRenderer.Drawcall> keyValuePair2 in keyValuePair.Value)
				{
					for (int i = 0; i < keyValuePair2.Value.Transforms.Size; i++)
					{
						if (num2++ % {11905} == 0)
						{
							{11904}(ref keyValuePair2.Value.Transforms.Array[i]);
						}
					}
				}
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000A6E8 File Offset: 0x000088E8
		private DynamicVertexBuffer {11906}(InstancedModelRenderer.Drawcall {11907})
		{
			if ({11907}.IndividualBuffer != null)
			{
				return {11907}.IndividualBuffer;
			}
			this.{11908}({11907}, ref InstancedModelRenderer.instanceVertexBuffer);
			return InstancedModelRenderer.instanceVertexBuffer;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000A70C File Offset: 0x0000890C
		private void {11908}(InstancedModelRenderer.Drawcall {11909}, ref DynamicVertexBuffer {11910})
		{
			Tlist<Matrix> transforms = {11909}.Transforms;
			if ({11910} == null || transforms.Size > {11910}.VertexCount)
			{
				DynamicVertexBuffer dynamicVertexBuffer = {11910};
				if (dynamicVertexBuffer != null)
				{
					dynamicVertexBuffer.Dispose();
				}
				{11910} = new DynamicVertexBuffer(Engine.Game.GraphicsDevice, InstancedModelRenderer.instanceVertexDeclaration, transforms.Size, BufferUsage.WriteOnly);
			}
			{11910}.SetData<Matrix>(transforms.Array, 0, transforms.Size, SetDataOptions.None);
			{11909}.OperatingTransformsCount = transforms.Size;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000A780 File Offset: 0x00008980
		public void Instantiate(ISceneObject3DParent {11911}, ModelTransformedScene {11912}, int {11913} = 1)
		{
			if (this.{11917}.Count == 0)
			{
				return;
			}
			Matrix matrix;
			{11912}.Transform.CreateWorldMatrix(out matrix);
			{11911}.SetWorld(ref matrix, null, {11912});
			foreach (KeyValuePair<Material, Dictionary<UWModel, InstancedModelRenderer.Drawcall>> keyValuePair in this.{11917})
			{
				bool flag = false;
				foreach (KeyValuePair<UWModel, InstancedModelRenderer.Drawcall> keyValuePair2 in keyValuePair.Value)
				{
					if (!flag)
					{
						{11911}.SetForPart(keyValuePair2.Key.Drawcalls.First<ModelPartShadercall>());
						{11911}.ApplyPass(keyValuePair2.Key.Drawcalls.First<ModelPartShadercall>().GeometryType);
						flag = true;
					}
					MeshPartData meshPartData = keyValuePair2.Key.Drawcalls.First<ModelPartShadercall>().Parts.First();
					Engine.GS.RenderInstancing(meshPartData.vertexStartIndex, meshPartData.vertexOffset, this.{11906}(keyValuePair2.Value), meshPartData.XnaVertexBuffer, meshPartData.XnaIndexBuffer, meshPartData.verticesCount, meshPartData.PrimitiveCount, keyValuePair2.Value.OperatingTransformsCount, {11913});
				}
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000A8E4 File Offset: 0x00008AE4
		public void Instantiate(IGBufferBuilder {11914}, ModelTransformedScene {11915}, int {11916} = 1)
		{
			if (this.{11917}.Count == 0)
			{
				return;
			}
			Matrix matrix;
			{11915}.Transform.CreateWorldMatrix(out matrix);
			{11914}.ApplyPass(ref matrix, true);
			foreach (KeyValuePair<Material, Dictionary<UWModel, InstancedModelRenderer.Drawcall>> keyValuePair in this.{11917})
			{
				if (keyValuePair.Key.Properties.RasterizerOptions != MaterialRasterizeOptions.AlphaMaterialWithoutShadow)
				{
					if ({11914}.MaterialAnalyzingEnable)
					{
						Texture2D tex = keyValuePair.Key.Albedo.Tex;
						if (tex.Format == SurfaceFormat.Dxt3 || tex.Format == SurfaceFormat.Dxt5 || tex.Format == SurfaceFormat.Color)
						{
							{11914}.RestartPassMaterialAnalyze(keyValuePair.Key.Albedo.Tex);
						}
						else
						{
							{11914}.RestartPassMaterialAnalyze(null);
						}
					}
					foreach (KeyValuePair<UWModel, InstancedModelRenderer.Drawcall> keyValuePair2 in keyValuePair.Value)
					{
						MeshPartData meshPartData = keyValuePair2.Key.Drawcalls.First<ModelPartShadercall>().Parts.First();
						Engine.GS.RenderInstancing(meshPartData.vertexStartIndex, meshPartData.vertexOffset, this.{11906}(keyValuePair2.Value), meshPartData.XnaVertexBuffer, meshPartData.XnaIndexBuffer, meshPartData.verticesCount, meshPartData.PrimitiveCount, keyValuePair2.Value.OperatingTransformsCount, {11916});
					}
				}
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000AA88 File Offset: 0x00008C88
		public void Dispose()
		{
			if (this.UseCachingStrategy)
			{
				this.CleanCache();
			}
		}

		// Token: 0x0400011D RID: 285
		private static DynamicVertexBuffer instanceVertexBuffer;

		// Token: 0x0400011E RID: 286
		private static Tlist<Matrix> tempList = new Tlist<Matrix>();

		// Token: 0x0400011F RID: 287
		public static readonly VertexDeclaration instanceVertexDeclaration = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
			new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
			new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
			new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3)
		});

		// Token: 0x04000120 RID: 288
		private Dictionary<Material, Dictionary<UWModel, InstancedModelRenderer.Drawcall>> {11917};

		// Token: 0x04000121 RID: 289
		public bool UseCachingStrategy;

		// Token: 0x04000122 RID: 290
		private int {11918};

		// Token: 0x0200003E RID: 62
		private class Drawcall
		{
			// Token: 0x060001CC RID: 460 RVA: 0x0000AB02 File Offset: 0x00008D02
			public Drawcall()
			{
				this.Transforms = new Tlist<Matrix>(4);
				this.IndividualBuffer = null;
			}

			// Token: 0x04000123 RID: 291
			public Tlist<Matrix> Transforms;

			// Token: 0x04000124 RID: 292
			public DynamicVertexBuffer IndividualBuffer;

			// Token: 0x04000125 RID: 293
			public int OperatingTransformsCount;
		}

		// Token: 0x0200003F RID: 63
		// (Invoke) Token: 0x060001CE RID: 462
		public delegate void RefAction<T>(ref T {11923});
	}
}
