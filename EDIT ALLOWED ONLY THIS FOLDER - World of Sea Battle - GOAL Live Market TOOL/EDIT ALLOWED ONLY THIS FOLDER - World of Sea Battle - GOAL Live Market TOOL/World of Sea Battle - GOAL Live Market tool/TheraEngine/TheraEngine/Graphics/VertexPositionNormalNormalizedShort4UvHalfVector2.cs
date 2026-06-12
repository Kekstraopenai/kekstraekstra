using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace TheraEngine.Graphics
{
	// Token: 0x02000152 RID: 338
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VertexPositionNormalNormalizedShort4UvHalfVector2 : IVertexType
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0002DC67 File Offset: 0x0002BE67
		VertexDeclaration IVertexType.VertexDeclaration
		{
			get
			{
				return VertexPositionNormalNormalizedShort4UvHalfVector2.vertexDeclaration;
			}
		}

		// Token: 0x0400065F RID: 1631
		public Vector3 Position;

		// Token: 0x04000660 RID: 1632
		public NormalizedShort4 Normal;

		// Token: 0x04000661 RID: 1633
		public HalfVector2 UV;

		// Token: 0x04000662 RID: 1634
		public static readonly VertexDeclaration vertexDeclaration = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.NormalizedShort4, VertexElementUsage.Normal, 0),
			new VertexElement(20, VertexElementFormat.HalfVector2, VertexElementUsage.TextureCoordinate, 0)
		});
	}
}
