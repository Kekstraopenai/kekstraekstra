using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Scene
{
	// Token: 0x02000048 RID: 72
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct TrailVertex
	{
		// Token: 0x04000149 RID: 329
		public Vector3 Position;

		// Token: 0x0400014A RID: 330
		public Vector2 TextureCoordinate;

		// Token: 0x0400014B RID: 331
		public float Visibility;

		// Token: 0x0400014C RID: 332
		public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
			new VertexElement(20, VertexElementFormat.Single, VertexElementUsage.TextureCoordinate, 0)
		});
	}
}
