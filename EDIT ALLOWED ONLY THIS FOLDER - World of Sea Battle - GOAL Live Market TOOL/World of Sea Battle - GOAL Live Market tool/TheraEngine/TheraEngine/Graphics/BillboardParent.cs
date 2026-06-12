using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Graphics
{
	// Token: 0x02000137 RID: 311
	public abstract class BillboardParent<T> where T : struct, IVertexType
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x0002B1B0 File Offset: 0x000293B0
		public BillboardParent()
		{
			this.array = new T[6];
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002B1C4 File Offset: 0x000293C4
		public void Render()
		{
			Engine.GS.Render3DSquere<T>(this.array, BillboardParent<T>.vertexDeclaration.VertexDeclaration);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0002B1E0 File Offset: 0x000293E0
		public void Render(VertexDeclaration {14902})
		{
			Engine.GS.Render3DSquere<T>(this.array, {14902});
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002B1F4 File Offset: 0x000293F4
		// Note: this type is marked as 'beforefieldinit'.
		static BillboardParent()
		{
			T t = Activator.CreateInstance<T>();
			BillboardParent<T>.vertexDeclaration = new BillboardParent<T>.Wrapper<T>(t.VertexDeclaration);
		}

		// Token: 0x040005EF RID: 1519
		private static readonly short[] indeces = new short[]
		{
			0,
			1,
			2,
			2,
			1,
			3
		};

		// Token: 0x040005F0 RID: 1520
		protected static readonly Color whiteColor = Color.White;

		// Token: 0x040005F1 RID: 1521
		protected static Vector3 _cs3_1;

		// Token: 0x040005F2 RID: 1522
		protected static Vector3 _cs3_2;

		// Token: 0x040005F3 RID: 1523
		protected static Vector3 _cs3_3;

		// Token: 0x040005F4 RID: 1524
		protected static Vector3 _cs3_4;

		// Token: 0x040005F5 RID: 1525
		protected static Vector2 _cs2_1;

		// Token: 0x040005F6 RID: 1526
		protected static Vector2 _cs2_2;

		// Token: 0x040005F7 RID: 1527
		private static BillboardParent<T>.Wrapper<T> vertexDeclaration;

		// Token: 0x040005F8 RID: 1528
		public T[] array;

		// Token: 0x02000138 RID: 312
		private readonly struct Wrapper<T2>
		{
			// Token: 0x060008DD RID: 2269 RVA: 0x0002B23E File Offset: 0x0002943E
			public Wrapper(VertexDeclaration {14904})
			{
				this.VertexDeclaration = {14904};
			}

			// Token: 0x040005F9 RID: 1529
			public readonly VertexDeclaration VertexDeclaration;
		}
	}
}
