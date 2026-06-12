using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Collections;

namespace TheraEngine.Graphics
{
	// Token: 0x0200014C RID: 332
	public sealed class SpriteBatch3D<T> : DisposableObject where T : struct, IVertexType
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0002D6A6 File Offset: 0x0002B8A6
		public Span<T> AsSpan
		{
			get
			{
				return this.list.AsSpan;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0002D6B3 File Offset: 0x0002B8B3
		public int Count
		{
			get
			{
				return this.list.Size;
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0002D6C0 File Offset: 0x0002B8C0
		public SpriteBatch3D() : this(512)
		{
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0002D6CD File Offset: 0x0002B8CD
		public SpriteBatch3D(int {15080})
		{
			if ({15080} < 1 || {15080} > 65535)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if ({15080} == 8000)
			{
				{15080} = 8192;
			}
			this.list = new Tlist<T>({15080});
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0002D707 File Offset: 0x0002B907
		public void Reset()
		{
			this.list.Size = 0;
			this.bufferUpdated = false;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0002D71C File Offset: 0x0002B91C
		public void Add(ref T {15081})
		{
			this.list.Add({15081});
			this.bufferUpdated = false;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0002D731 File Offset: 0x0002B931
		public void Add(T[] {15082})
		{
			this.list.Add({15082});
			this.bufferUpdated = false;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0002D748 File Offset: 0x0002B948
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe void UnsafeAddQuadBased(T[] {15083})
		{
			Span<T> span = new Span<T>({15083}, 0, 3);
			Span<T> destination = new Span<T>(this.list.Array, this.list.Size, 6);
			span.CopyTo(destination);
			*destination[3] = *span[2];
			*destination[4] = *span[1];
			*destination[5] = {15083}[3];
			this.list.Size += 6;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0002D7E0 File Offset: 0x0002B9E0
		public void Render(EffectPass {15084})
		{
			if (this.list.Size == 0)
			{
				return;
			}
			if (this.internalVertexBuffer == null || this.internalVertexBuffer.VertexCount < this.list.Array.Length)
			{
				VertexBuffer vertexBuffer = this.internalVertexBuffer;
				if (vertexBuffer != null)
				{
					vertexBuffer.Dispose();
				}
				this.internalVertexBuffer = new VertexBuffer(Engine.GS.graphicsDevice, typeof(T), this.list.Array.Length, BufferUsage.WriteOnly);
				this.bufferUpdated = false;
			}
			if (!this.bufferUpdated)
			{
				Engine.GS.graphicsDevice.SetVertexBuffer(null);
				this.internalVertexBuffer.SetData<T>(this.list.Array, 0, this.list.Size);
				this.bufferUpdated = true;
			}
			if ({15084} != null)
			{
				{15084}.Apply();
			}
			Engine.GS.Render3DUserMesh(this.internalVertexBuffer, PrimitiveType.TriangleList, this.list.Size / 3);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0002D8CA File Offset: 0x0002BACA
		public UserMesh CreateMesh()
		{
			return UserMesh.Create<T>(this.list.Array, this.list.Size);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0002D8E7 File Offset: 0x0002BAE7
		public override void Dispose()
		{
			VertexBuffer vertexBuffer = this.internalVertexBuffer;
			if (vertexBuffer != null)
			{
				vertexBuffer.Dispose();
			}
			this.internalVertexBuffer = null;
			base.Dispose();
		}

		// Token: 0x0400064B RID: 1611
		public const int RecommendedMaxVerticesCount = 8000;

		// Token: 0x0400064C RID: 1612
		private const int defautCapacity = 512;

		// Token: 0x0400064D RID: 1613
		private Tlist<T> list;

		// Token: 0x0400064E RID: 1614
		private VertexBuffer internalVertexBuffer;

		// Token: 0x0400064F RID: 1615
		private bool bufferUpdated;
	}
}
