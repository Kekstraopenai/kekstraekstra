using System;
using System.Collections.Generic;

namespace TheraEngine.Collections
{
	// Token: 0x02000116 RID: 278
	public class ObjectPoolWithLock<T>
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0002775F File Offset: 0x0002595F
		public int Count
		{
			get
			{
				return this.objects.Count;
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002776C File Offset: 0x0002596C
		public ObjectPoolWithLock(Func<T> objectGenerator, int preloadCapacity)
		{
			this.PreloadCapacity = preloadCapacity;
			this.objectGenerator = objectGenerator;
			this.objects = new Stack<T>(preloadCapacity);
			for (int i = 0; i < preloadCapacity; i++)
			{
				this.objects.Push(objectGenerator());
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000277C4 File Offset: 0x000259C4
		public T Get()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (this.objects.Count > 0)
				{
					return this.objects.Pop();
				}
			}
			return this.objectGenerator();
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00027828 File Offset: 0x00025A28
		public void Return(T item)
		{
			object obj = this.locker;
			lock (obj)
			{
				this.objects.Push(item);
			}
		}

		// Token: 0x04000583 RID: 1411
		private readonly Stack<T> objects;

		// Token: 0x04000584 RID: 1412
		private readonly Func<T> objectGenerator;

		// Token: 0x04000585 RID: 1413
		public readonly int PreloadCapacity;

		// Token: 0x04000586 RID: 1414
		private object locker = new object();
	}
}
