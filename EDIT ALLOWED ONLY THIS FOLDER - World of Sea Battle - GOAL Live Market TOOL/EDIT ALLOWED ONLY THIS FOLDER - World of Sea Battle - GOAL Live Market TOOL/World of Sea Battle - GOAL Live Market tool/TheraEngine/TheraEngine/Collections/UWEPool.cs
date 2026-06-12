using System;

namespace TheraEngine.Collections
{
	// Token: 0x02000134 RID: 308
	public class UWEPool<T> where T : class, IPoolObject, new()
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0002AB7E File Offset: 0x00028D7E
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0002AB86 File Offset: 0x00028D86
		public bool AllowGrow { get; set; } = true;

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0002AB8F File Offset: 0x00028D8F
		public int Capacity
		{
			get
			{
				return this.startSize;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0002AB97 File Offset: 0x00028D97
		public int FreeCount
		{
			get
			{
				return this.list.Size;
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002ABA4 File Offset: 0x00028DA4
		public UWEPool(int initSize)
		{
			if (initSize <= 0)
			{
				throw new ArgumentOutOfRangeException("startSize");
			}
			this.startSize = initSize;
			this.list = new Tlist<T>(initSize);
			for (int i = 0; i < initSize; i++)
			{
				Tlist<T> tlist = this.list;
				T t = Activator.CreateInstance<T>();
				tlist.Add(t);
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0002AC00 File Offset: 0x00028E00
		public void Add(in T item)
		{
			T t = item;
			t.ClearResources();
			if (this.list.Size >= this.startSize && !this.AllowGrow)
			{
				return;
			}
			this.list.Add(item);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002AC4C File Offset: 0x00028E4C
		public T Pop()
		{
			if (this.list.Size == 0)
			{
				return Activator.CreateInstance<T>();
			}
			this.list.Size--;
			T result = this.list.Array[this.list.Size];
			this.list.Array[this.list.Size] = UWEPool<T>.defualtT;
			return result;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0002ACBC File Offset: 0x00028EBC
		public void Return(Tlist<T> list)
		{
			for (int i = 0; i < list.Size; i++)
			{
				this.Add(list.Array[i]);
			}
			list.Clear();
		}

		// Token: 0x040005DE RID: 1502
		private static T defualtT;

		// Token: 0x040005DF RID: 1503
		private readonly Tlist<T> list;

		// Token: 0x040005E0 RID: 1504
		private readonly int startSize;
	}
}
