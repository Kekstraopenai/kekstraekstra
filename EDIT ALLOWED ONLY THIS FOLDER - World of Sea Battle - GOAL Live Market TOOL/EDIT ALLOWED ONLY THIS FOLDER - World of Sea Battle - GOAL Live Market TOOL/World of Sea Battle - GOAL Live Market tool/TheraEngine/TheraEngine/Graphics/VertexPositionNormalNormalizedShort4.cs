using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace TheraEngine.Graphics
{
	// Token: 0x02000151 RID: 337
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VertexPositionNormalNormalizedShort4 : IVertexType
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x0002DC1C File Offset: 0x0002BE1C
		public VertexPositionNormalNormalizedShort4(Vector3 {15116}, NormalizedShort4 {15117})
		{
			this.Position = {15116};
			this.Normal = {15117};
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0002DC2C File Offset: 0x0002BE2C
		VertexDeclaration IVertexType.VertexDeclaration
		{
			get
			{
				return VertexPositionNormalNormalizedShort4.vertexDeclaration;
			}
		}

		// Token: 0x0400065C RID: 1628
		public Vector3 Position;

		// Token: 0x0400065D RID: 1629
		public NormalizedShort4 Normal;

		// Token: 0x0400065E RID: 1630
		public static readonly VertexDeclaration vertexDeclaration = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.NormalizedShort4, VertexElementUsage.Normal, 0)
		});
	}
}
