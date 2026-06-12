using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UWContentPipelineExtensionRuntime.Tags;

namespace TheraEngine.Graphics.Models
{
	// Token: 0x02000160 RID: 352
	public class MeshPartData
	{
		// Token: 0x06000976 RID: 2422 RVA: 0x0002DE74 File Offset: 0x0002C074
		public MeshPartData(VertexBuffer {15151}, IndexBuffer {15152})
		{
			this.XnaVertexBuffer = {15151};
			this.vertexStride = this.XnaVertexBuffer.VertexDeclaration.VertexStride;
			this.vertexOffset = 0;
			this.verticesCount = {15151}.VertexCount;
			this.indicesCount = {15152}.IndexCount;
			this.XnaIndexBuffer = {15152};
			this.vertexStartIndex = 0;
			this.PrimitiveCount = {15152}.IndexCount / 3;
			this.LocalSpaceBoundingSphere = default(BoundingSphere);
			this.LocalSpaceBoundingBox = default(BoundingBox);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0002DEF8 File Offset: 0x0002C0F8
		public MeshPartData(ModelMeshPart {15153}, UWModelMeshPartTag {15154})
		{
			this.XnaVertexBuffer = {15153}.VertexBuffer;
			this.vertexStride = this.XnaVertexBuffer.VertexDeclaration.VertexStride;
			this.vertexOffset = {15153}.VertexOffset;
			this.verticesCount = {15153}.NumVertices;
			this.indicesCount = {15153}.PrimitiveCount * 3;
			this.XnaIndexBuffer = {15153}.IndexBuffer;
			this.vertexStartIndex = {15153}.StartIndex;
			this.PrimitiveCount = {15153}.PrimitiveCount;
			this.LocalSpaceBoundingSphere = {15154}.LocalBoundingSphere;
			this.LocalSpaceBoundingBox = {15154}.LocalBoundingBox;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0002DF90 File Offset: 0x0002C190
		public MeshPartData(MeshPartData {15155})
		{
			this.XnaVertexBuffer = {15155}.XnaVertexBuffer;
			this.vertexStride = {15155}.vertexStride;
			this.vertexOffset = {15155}.vertexOffset;
			this.verticesCount = {15155}.verticesCount;
			this.indicesCount = {15155}.indicesCount;
			this.XnaIndexBuffer = {15155}.XnaIndexBuffer;
			this.vertexStartIndex = {15155}.vertexStartIndex;
			this.PrimitiveCount = {15155}.PrimitiveCount;
			this.LocalSpaceBoundingSphere = {15155}.LocalSpaceBoundingSphere;
			this.LocalSpaceBoundingBox = {15155}.LocalSpaceBoundingBox;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0002E01C File Offset: 0x0002C21C
		public VertexPositionNormalTexture[] BuildVertexBufferFull()
		{
			VertexPositionNormalTexture[] array = new VertexPositionNormalTexture[this.verticesCount];
			if (this.vertexStride == 24 && this.XnaVertexBuffer.VertexDeclaration.GetVertexElements().Length == 3)
			{
				Vector3[] array2 = new Vector3[array.Length];
				this.XnaVertexBuffer.GetData<Vector3>(this.vertexOffset * this.vertexStride, array2, 0, array.Length, this.vertexStride);
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new VertexPositionNormalTexture(array2[i], default(Vector3), default(Vector2));
				}
			}
			else
			{
				this.XnaVertexBuffer.GetData<VertexPositionNormalTexture>(this.vertexOffset * this.vertexStride, array, 0, this.verticesCount, this.vertexStride);
			}
			return array;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0002E0E0 File Offset: 0x0002C2E0
		public VertexPositionNormal[] BuildVertexBufferSlim()
		{
			VertexPositionNormal[] array = new VertexPositionNormal[this.verticesCount];
			if (this.vertexStride == 20)
			{
				VertexPositionNormalNormalizedShort4[] array2 = new VertexPositionNormalNormalizedShort4[this.verticesCount];
				this.XnaVertexBuffer.GetData<VertexPositionNormalNormalizedShort4>(this.vertexOffset * this.vertexStride, array2, 0, this.verticesCount, 0);
				for (int i = 0; i < array2.Length; i++)
				{
					Vector4 vector = array2[i].Normal.ToVector4();
					Vector3 {15121} = new Vector3(vector.X, vector.Y, vector.Z);
					{15121}.Normalize();
					array[i] = new VertexPositionNormal(array2[i].Position, {15121});
				}
			}
			else
			{
				this.XnaVertexBuffer.GetData<VertexPositionNormal>(this.vertexOffset * this.vertexStride, array, 0, this.verticesCount, this.vertexStride);
			}
			return array;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0002E1B8 File Offset: 0x0002C3B8
		public int[] BuildIndexBuffer()
		{
			if (this.XnaIndexBuffer.IndexElementSize == IndexElementSize.SixteenBits)
			{
				ushort[] array = new ushort[this.indicesCount];
				this.XnaIndexBuffer.GetData<ushort>(this.vertexStartIndex * 2, array, 0, this.indicesCount);
				int num = (int)array[0];
				int[] array2 = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = (int)array[i] - num;
				}
				return array2;
			}
			int[] array3 = new int[this.indicesCount];
			int num2 = array3[0];
			this.XnaIndexBuffer.GetData<int>(this.vertexStartIndex * 4, array3, 0, this.indicesCount);
			for (int j = 0; j < array3.Length; j++)
			{
				array3[j] -= num2;
			}
			return array3;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0002E274 File Offset: 0x0002C474
		public ushort[] BuildIndexBufferInt16()
		{
			if (this.XnaIndexBuffer.IndexElementSize == IndexElementSize.SixteenBits)
			{
				ushort[] array = new ushort[this.indicesCount];
				this.XnaIndexBuffer.GetData<ushort>(this.vertexStartIndex * 2, array, 0, this.indicesCount);
				ushort num = array[0];
				for (int i = 0; i < array.Length; i++)
				{
					ushort[] array2 = array;
					int num2 = i;
					array2[num2] -= num;
				}
				return array;
			}
			throw new NotSupportedException("BuildIndexBufferInt16");
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0002E2E0 File Offset: 0x0002C4E0
		public MeshTriangle[] BuildTriangleShape()
		{
			int[] array = this.BuildIndexBuffer();
			VertexPositionNormal[] array2 = this.BuildVertexBufferSlim();
			MeshTriangle[] array3 = new MeshTriangle[array.Length / 3];
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i] = new MeshTriangle(ref array2[array[i * 3]], ref array2[array[i * 3 + 1]], ref array2[array[i * 3 + 2]]);
			}
			return array3;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0002E346 File Offset: 0x0002C546
		public void DisposeWithSourceData()
		{
			if (this.XnaIndexBuffer.IsDisposed)
			{
				return;
			}
			this.XnaIndexBuffer.Dispose();
			this.XnaVertexBuffer.Dispose();
		}

		// Token: 0x04000671 RID: 1649
		public BoundingSphere LocalSpaceBoundingSphere;

		// Token: 0x04000672 RID: 1650
		public BoundingBox LocalSpaceBoundingBox;

		// Token: 0x04000673 RID: 1651
		public int PrimitiveCount;

		// Token: 0x04000674 RID: 1652
		public readonly VertexBuffer XnaVertexBuffer;

		// Token: 0x04000675 RID: 1653
		public readonly IndexBuffer XnaIndexBuffer;

		// Token: 0x04000676 RID: 1654
		internal int vertexOffset;

		// Token: 0x04000677 RID: 1655
		internal int verticesCount;

		// Token: 0x04000678 RID: 1656
		internal int vertexStartIndex;

		// Token: 0x04000679 RID: 1657
		internal int indicesCount;

		// Token: 0x0400067A RID: 1658
		internal int vertexStride;
	}
}
