using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Input;

namespace TheraEngine.Graphics.Models
{
	// Token: 0x02000165 RID: 357
	public class ModelPartShadercall
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x0002E4DC File Offset: 0x0002C6DC
		public ModelPartShadercall(Material {15171}, Tlist<MeshPartData> {15172}, ModelGeometryType {15173})
		{
			this.Parts = {15172};
			this.Material = {15171};
			this.GeometryType = {15173};
			this.PrimitivesCount = this.Parts.Sum((MeshPartData {15179}) => {15179}.PrimitiveCount);
			Dictionary<VertexBuffer, Tlist<MeshPartData>> dictionary = new Dictionary<VertexBuffer, Tlist<MeshPartData>>();
			for (int i = 0; i < {15172}.Size; i++)
			{
				MeshPartData meshPartData = {15172}.Array[i];
				Tlist<MeshPartData> tlist;
				if (dictionary.TryGetValue(meshPartData.XnaVertexBuffer, out tlist))
				{
					tlist.Add(meshPartData);
				}
				else
				{
					dictionary.Add(meshPartData.XnaVertexBuffer, new Tlist<MeshPartData>(new MeshPartData[]
					{
						meshPartData
					}));
				}
			}
			this.{15177} = new DeviceStreamContext[dictionary.Count];
			this.{15178} = new VertexBufferBinding[dictionary.Count];
			int num = 0;
			foreach (KeyValuePair<VertexBuffer, Tlist<MeshPartData>> keyValuePair in dictionary)
			{
				this.{15177}[num] = new DeviceStreamContext
				{
					IndexBuffer = keyValuePair.Value.Array[0].XnaIndexBuffer,
					VertexBuffer = keyValuePair.Key,
					Sets = this.Compress(keyValuePair.Value)
				};
				this.{15178}[num] = new VertexBufferBinding(keyValuePair.Key);
				num++;
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002E64C File Offset: 0x0002C84C
		private Tlist<MeshPartData> Compress(Tlist<MeshPartData> {15174})
		{
			return {15174};
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0002E64F File Offset: 0x0002C84F
		internal void RenderWithTreshold(int {15175}, float {15176})
		{
			if (InputHelper.NowInputState.IsDown(Keys.O))
			{
				if ((float)this.PrimitivesCount / (float){15175} > {15176})
				{
					this.Render();
					return;
				}
			}
			else
			{
				this.Render();
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0002E679 File Offset: 0x0002C879
		internal void Render()
		{
			Engine.GS.Render3DCompressedMesh(this.{15177}, this.{15178}, false);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002E694 File Offset: 0x0002C894
		public override string ToString()
		{
			return "Drawcall (gpu send):  Count buffers: " + this.{15177}.Length.ToString() + " From parts: " + this.Parts.Size.ToString();
		}

		// Token: 0x04000684 RID: 1668
		public readonly Tlist<MeshPartData> Parts;

		// Token: 0x04000685 RID: 1669
		public readonly Material Material;

		// Token: 0x04000686 RID: 1670
		public readonly ModelGeometryType GeometryType;

		// Token: 0x04000687 RID: 1671
		public readonly int PrimitivesCount;

		// Token: 0x04000688 RID: 1672
		private DeviceStreamContext[] {15177};

		// Token: 0x04000689 RID: 1673
		private VertexBufferBinding[] {15178};
	}
}
