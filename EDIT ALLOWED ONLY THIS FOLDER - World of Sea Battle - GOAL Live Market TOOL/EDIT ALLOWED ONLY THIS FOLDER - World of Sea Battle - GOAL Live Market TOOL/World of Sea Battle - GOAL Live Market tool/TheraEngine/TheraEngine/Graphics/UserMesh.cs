using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Graphics
{
	// Token: 0x0200014E RID: 334
	public class UserMesh : DisposableObject
	{
		// Token: 0x06000953 RID: 2387 RVA: 0x0002D9C4 File Offset: 0x0002BBC4
		public static UserMesh Create<T>(T[] {15096}, int {15097}) where T : struct, IVertexType
		{
			UserMesh userMesh = new UserMesh();
			userMesh.Initialize<T>({15096}, {15097});
			return userMesh;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002D9D4 File Offset: 0x0002BBD4
		protected void Initialize<T>(T[] {15098}, int {15099}) where T : struct, IVertexType
		{
			if ({15099} == 0)
			{
				throw new ArgumentException();
			}
			this.typeVertex = typeof(T);
			this.vertexBuffer = new VertexBuffer(Engine.GS.graphicsDevice, this.typeVertex, {15099}, BufferUsage.WriteOnly);
			this.vertexBuffer.SetData<T>({15098}, 0, {15099});
			this.verticesCount = {15099};
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0002DA2C File Offset: 0x0002BC2C
		public void Rewrite<T>(T[] {15100}, int {15101}) where T : struct, IVertexType
		{
			if (typeof(T) != this.typeVertex)
			{
				throw new InvalidOperationException("Trying to rewrite UserMesh using other data type");
			}
			this.verticesCount = {15101};
			this.vertexBuffer.SetData<T>({15100}, 0, {15101});
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0000E3AE File Offset: 0x0000C5AE
		protected UserMesh()
		{
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0002DA65 File Offset: 0x0002BC65
		public void Render()
		{
			Engine.GS.Render3DUserMesh(this.vertexBuffer, PrimitiveType.TriangleList, this.verticesCount / 3);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0002DA80 File Offset: 0x0002BC80
		public override void Dispose()
		{
			VertexBuffer vertexBuffer = this.vertexBuffer;
			if (vertexBuffer != null)
			{
				vertexBuffer.Dispose();
			}
			this.vertexBuffer = null;
			base.Dispose();
		}

		// Token: 0x04000652 RID: 1618
		protected Type typeVertex;

		// Token: 0x04000653 RID: 1619
		protected VertexBuffer vertexBuffer;

		// Token: 0x04000654 RID: 1620
		protected int verticesCount;
	}
}
