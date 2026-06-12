using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x02000053 RID: 83
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct VolumtericParticleVertex : IVertexType
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000CD8C File Offset: 0x0000AF8C
		VertexDeclaration IVertexType.VertexDeclaration
		{
			get
			{
				return VolumtericParticleVertex.vertexDeclaration;
			}
		}

		// Token: 0x04000190 RID: 400
		public Vector3 Position;

		// Token: 0x04000191 RID: 401
		public Vector3 ParticleCenter;

		// Token: 0x04000192 RID: 402
		public Vector3 SizeAndRotation;

		// Token: 0x04000193 RID: 403
		public Vector4 Color;

		// Token: 0x04000194 RID: 404
		public Vector2 Texture;

		// Token: 0x04000195 RID: 405
		public Vector3 Stretch;

		// Token: 0x04000196 RID: 406
		private static readonly VertexDeclaration vertexDeclaration = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0),
			new VertexElement(24, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 1),
			new VertexElement(36, VertexElementFormat.Vector4, VertexElementUsage.Color, 0),
			new VertexElement(52, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 2),
			new VertexElement(60, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 3)
		});
	}
}
