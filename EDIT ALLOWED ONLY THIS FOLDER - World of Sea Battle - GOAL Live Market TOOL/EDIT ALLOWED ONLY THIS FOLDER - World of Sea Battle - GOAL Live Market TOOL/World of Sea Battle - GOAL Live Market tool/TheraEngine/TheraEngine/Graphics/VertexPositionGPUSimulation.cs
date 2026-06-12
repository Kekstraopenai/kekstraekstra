using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Graphics
{
	// Token: 0x02000150 RID: 336
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VertexPositionGPUSimulation : IVertexType
	{
		// Token: 0x0600095D RID: 2397 RVA: 0x0002DB0E File Offset: 0x0002BD0E
		public VertexPositionGPUSimulation(Vector3 {15109}, Vector3 {15110}, Vector2 {15111}, Vector4 {15112})
		{
			this.LocalPosition = {15109};
			this.ObjectCenter = {15110};
			this.UV = {15111};
			this.Color = {15112};
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0002DB30 File Offset: 0x0002BD30
		public override bool Equals(object {15113})
		{
			if ({15113} == null)
			{
				return false;
			}
			if ({15113}.GetType() != base.GetType())
			{
				return false;
			}
			VertexPositionGPUSimulation vertexPositionGPUSimulation = (VertexPositionGPUSimulation){15113};
			return vertexPositionGPUSimulation.LocalPosition == this.LocalPosition && vertexPositionGPUSimulation.ObjectCenter == this.ObjectCenter && vertexPositionGPUSimulation.UV == this.UV && vertexPositionGPUSimulation.Color == this.Color;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0002DBB4 File Offset: 0x0002BDB4
		public VertexDeclaration VertexDeclaration
		{
			get
			{
				return VertexPositionGPUSimulation.vertexDeclaration;
			}
		}

		// Token: 0x04000657 RID: 1623
		public Vector3 LocalPosition;

		// Token: 0x04000658 RID: 1624
		public Vector3 ObjectCenter;

		// Token: 0x04000659 RID: 1625
		public Vector2 UV;

		// Token: 0x0400065A RID: 1626
		public Vector4 Color;

		// Token: 0x0400065B RID: 1627
		private static VertexDeclaration vertexDeclaration = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0),
			new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 1),
			new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.Color, 0)
		});
	}
}
