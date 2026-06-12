using System;
using System.Collections.Concurrent;

namespace TheraEngine.Collections
{
	// Token: 0x02000115 RID: 277
	public class ObjectPool<T>
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x000276CD File Offset: 0x000258CD
		public int Count
		{
			get
			{
				return this.objects.Count;
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000276DC File Offset: 0x000258DC
		public ObjectPool(Func<T> objectGenerator, int preloadCapacity)
		{
			this.PreloadCapacity = preloadCapacity;
			this.objectGenerator = objectGenerator;
			this.objects = new ConcurrentQueue<T>();
			for (int i = 0; i < preloadCapacity; i++)
			{
				this.objects.Enqueue(objectGenerator());
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00027728 File Offset: 0x00025928
		public T Get()
		{
			T result;
			if (!this.objects.TryDequeue(out result))
			{
				return this.objectGenerator();
			}
			return result;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00027751 File Offset: 0x00025951
		public void Return(T item)
		{
			this.objects.Enqueue(item);
		}

		// Token: 0x04000580 RID: 1408
		private readonly ConcurrentQueue<T> objects;

		// Token: 0x04000581 RID: 1409
		private readonly Func<T> objectGenerator;

		// Token: 0x04000582 RID: 1410
		public readonly int PreloadCapacity;
	}
}
