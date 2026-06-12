using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Graphics
{
	// Token: 0x0200014D RID: 333
	public class UserIndexedMesh : UserMesh
	{
		// Token: 0x0600094E RID: 2382 RVA: 0x0002D907 File Offset: 0x0002BB07
		public static UserIndexedMesh Create<T>(T[] {15085}, int {15086}, short[] {15087}, int {15088}) where T : struct, IVertexType
		{
			UserIndexedMesh userIndexedMesh = new UserIndexedMesh();
			userIndexedMesh.Initialize<T>({15085}, {15086}, {15087}, {15088});
			return userIndexedMesh;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0002D918 File Offset: 0x0002BB18
		protected void Initialize<T>(T[] {15089}, int {15090}, short[] {15091}, int {15092}) where T : struct, IVertexType
		{
			if ({15090} == 0)
			{
				throw new ArgumentException();
			}
			base.Initialize<T>({15089}, {15090});
			this.{15094} = new IndexBuffer(Engine.GS.graphicsDevice, IndexElementSize.SixteenBits, {15092}, BufferUsage.WriteOnly);
			this.{15095} = {15091}.Length / 3;
			this.{15094}.SetData<short>({15091});
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0002D966 File Offset: 0x0002BB66
		protected UserIndexedMesh()
		{
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0002D96E File Offset: 0x0002BB6E
		public new void Render()
		{
			Engine.GS.Render3DBufferedPart(this.vertexBuffer, this.{15094}, 0, this.verticesCount, 0, this.{15095});
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0002D994 File Offset: 0x0002BB94
		public void RenderExternal(GraphicsDevice {15093})
		{
			{15093}.SetVertexBuffer(this.vertexBuffer);
			{15093}.Indices = this.{15094};
			{15093}.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, this.verticesCount, 0, this.{15095});
		}

		// Token: 0x04000650 RID: 1616
		private IndexBuffer {15094};

		// Token: 0x04000651 RID: 1617
		private int {15095};
	}
}
