using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Graphics
{
	// Token: 0x0200014F RID: 335
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VertexPosition : IVertexType
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x0002DAA0 File Offset: 0x0002BCA0
		public VertexPosition(Vector3 {15103})
		{
			this.Position = {15103};
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002DAA9 File Offset: 0x0002BCA9
		public override bool Equals(object {15104})
		{
			return {15104} != null && !({15104}.GetType() != base.GetType()) && ((VertexPosition){15104}).Position == this.Position;
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0002DAE5 File Offset: 0x0002BCE5
		public VertexDeclaration VertexDeclaration
		{
			get
			{
				return VertexPosition.vertexDeclaration;
			}
		}

		// Token: 0x04000655 RID: 1621
		public Vector3 Position;

		// Token: 0x04000656 RID: 1622
		private static VertexDeclaration vertexDeclaration = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0)
		});
	}
}
