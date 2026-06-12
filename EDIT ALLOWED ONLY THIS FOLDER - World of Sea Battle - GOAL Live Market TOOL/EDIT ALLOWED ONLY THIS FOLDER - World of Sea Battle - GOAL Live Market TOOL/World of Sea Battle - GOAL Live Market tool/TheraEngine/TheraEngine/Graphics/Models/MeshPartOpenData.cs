using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Graphics.Models
{
	// Token: 0x0200016A RID: 362
	public class MeshPartOpenData
	{
		// Token: 0x060009AE RID: 2478 RVA: 0x0002F35E File Offset: 0x0002D55E
		public MeshPartOpenData(MeshPartData {15238})
		{
			this.VertexBuffer = {15238}.BuildVertexBufferSlim();
			this.IndexBuffer = {15238}.BuildIndexBufferInt16();
			this.PrimitiveCount = {15238}.PrimitiveCount;
			this.LocalSpaceBoundingSphere = {15238}.LocalSpaceBoundingSphere;
		}

		// Token: 0x0400069F RID: 1695
		public readonly VertexPositionNormal[] VertexBuffer;

		// Token: 0x040006A0 RID: 1696
		public readonly ushort[] IndexBuffer;

		// Token: 0x040006A1 RID: 1697
		public BoundingSphere LocalSpaceBoundingSphere;

		// Token: 0x040006A2 RID: 1698
		public readonly int PrimitiveCount;
	}
}
