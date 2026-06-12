using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Graphics
{
	// Token: 0x02000153 RID: 339
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VertexPositionNormal : IVertexType
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x0002DCC1 File Offset: 0x0002BEC1
		public VertexPositionNormal(Vector3 {15120}, Vector3 {15121})
		{
			this.Position = {15120};
			this.Normal = {15121};
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0002DCD1 File Offset: 0x0002BED1
		VertexDeclaration IVertexType.VertexDeclaration
		{
			get
			{
				return VertexPositionNormal.vertexDeclaration;
			}
		}

		// Token: 0x04000663 RID: 1635
		public Vector3 Position;

		// Token: 0x04000664 RID: 1636
		public Vector3 Normal;

		// Token: 0x04000665 RID: 1637
		public static readonly VertexDeclaration vertexDeclaration = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0)
		});
	}
}
