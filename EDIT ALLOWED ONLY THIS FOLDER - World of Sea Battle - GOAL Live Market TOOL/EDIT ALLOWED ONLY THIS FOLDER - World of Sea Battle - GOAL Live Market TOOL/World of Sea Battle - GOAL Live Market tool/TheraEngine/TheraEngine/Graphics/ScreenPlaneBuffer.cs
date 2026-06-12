using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Graphics
{
	// Token: 0x02000149 RID: 329
	public class ScreenPlaneBuffer
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0002D0B7 File Offset: 0x0002B2B7
		public Vector4 Rectangle
		{
			get
			{
				return this.{15073};
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0002D0C0 File Offset: 0x0002B2C0
		public void Build()
		{
			if (Engine.GS == null)
			{
				return;
			}
			VertexPositionTexture[] array = new VertexPositionTexture[]
			{
				new VertexPositionTexture(new Vector3(-1f, -1f, 1f), new Vector2(0f, 1f)),
				new VertexPositionTexture(new Vector3(-1f, 1f, 1f), new Vector2(0f, 0f)),
				new VertexPositionTexture(new Vector3(1f, -1f, 1f), new Vector2(1f, 1f)),
				new VertexPositionTexture(new Vector3(1f, 1f, 1f), new Vector2(1f, 0f))
			};
			this.{15073} = new Vector4(0f, 0f, 1f, 1f);
			VertexBuffer vertexBuffer = this.vertexBufferScreenPlane;
			if (vertexBuffer != null)
			{
				vertexBuffer.Dispose();
			}
			this.vertexBufferScreenPlane = new VertexBuffer(Engine.GS.graphicsDevice, typeof(VertexPositionTexture), array.Length, BufferUsage.WriteOnly);
			this.vertexBufferScreenPlane.SetData<VertexPositionTexture>(array);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0002D1F8 File Offset: 0x0002B3F8
		public void Build(Vector4 {15071}, bool {15072})
		{
			if (Engine.GS == null)
			{
				return;
			}
			Vector2 vector;
			vector.X = ({15071}.X - 0.5f) / 0.5f;
			vector.Y = ({15071}.Y - 0.5f) / -0.5f;
			Vector2 vector2;
			vector2.X = vector.X + {15071}.Z * 2f;
			vector2.Y = vector.Y - {15071}.W * 2f;
			VertexPositionTexture[] array2;
			if (!{15072})
			{
				VertexPositionTexture[] array = new VertexPositionTexture[4];
				array[0] = new VertexPositionTexture(new Vector3(vector.X, vector.Y, 1f), new Vector2({15071}.X, {15071}.Y));
				array[1] = new VertexPositionTexture(new Vector3(vector.X, vector2.Y, 1f), new Vector2({15071}.X, {15071}.Y + {15071}.W));
				array[2] = new VertexPositionTexture(new Vector3(vector2.X, vector.Y, 1f), new Vector2({15071}.X + {15071}.Z, {15071}.Y));
				array2 = array;
				array[3] = new VertexPositionTexture(new Vector3(vector2.X, vector2.Y, 1f), new Vector2({15071}.X + {15071}.Z, {15071}.Y + {15071}.W));
			}
			else
			{
				VertexPositionTexture[] array3 = new VertexPositionTexture[4];
				array3[0] = new VertexPositionTexture(new Vector3(vector.X, vector.Y, 1f), new Vector2(0f, 0f));
				array3[1] = new VertexPositionTexture(new Vector3(vector.X, vector2.Y, 1f), new Vector2(0f, 1f));
				array3[2] = new VertexPositionTexture(new Vector3(vector2.X, vector.Y, 1f), new Vector2(1f, 0f));
				array2 = array3;
				array3[3] = new VertexPositionTexture(new Vector3(vector2.X, vector2.Y, 1f), new Vector2(1f, 1f));
			}
			VertexPositionTexture[] array4 = array2;
			this.{15073} = {15071};
			VertexBuffer vertexBuffer = this.vertexBufferScreenPlane;
			if (vertexBuffer != null)
			{
				vertexBuffer.Dispose();
			}
			this.vertexBufferScreenPlane = new VertexBuffer(Engine.GS.graphicsDevice, typeof(VertexPositionTexture), array4.Length, BufferUsage.WriteOnly);
			this.vertexBufferScreenPlane.SetData<VertexPositionTexture>(array4);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0002D480 File Offset: 0x0002B680
		public void SendToGPU()
		{
			if (this.vertexBufferScreenPlane != null && this.vertexBufferScreenPlane.GraphicsDevice.IsDisposed)
			{
				this.vertexBufferScreenPlane.Dispose();
				this.vertexBufferScreenPlane = null;
				return;
			}
			Engine.GS.Render3DUserMesh(this.vertexBufferScreenPlane, PrimitiveType.TriangleStrip, 2);
		}

		// Token: 0x04000646 RID: 1606
		internal VertexBuffer vertexBufferScreenPlane;

		// Token: 0x04000647 RID: 1607
		private Vector4 {15073};
	}
}
