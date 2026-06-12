using System;
using TheraEngine.Helpers;

namespace TheraEngine.Collections
{
	// Token: 0x0200010D RID: 269
	public class CountingSort<T>
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00026FF0 File Offset: 0x000251F0
		public int MaxKey
		{
			get
			{
				return this.maxKey;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00026FF8 File Offset: 0x000251F8
		public int QueuedCount
		{
			get
			{
				return this.items.Size;
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00027005 File Offset: 0x00025205
		public CountingSort(int minKey, int maxKey, int initialItemsCount)
		{
			this.minKey = minKey;
			this.maxKey = maxKey;
			this.items = new Tlist<CountingSort<T>.Tuple>(initialItemsCount);
			this.count = new int[maxKey - minKey];
			this.LastResult = new Tlist<T>(initialItemsCount);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00027044 File Offset: 0x00025244
		public void Append(int key, T value)
		{
			key -= this.minKey;
			Tlist<CountingSort<T>.Tuple> tlist = this.items;
			CountingSort<T>.Tuple tuple = new CountingSort<T>.Tuple(key, value);
			tlist.Add(tuple);
			this.count[key]++;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00027084 File Offset: 0x00025284
		public void Sort()
		{
			if (this.items.Size != 0)
			{
				if (this.items.Size > this.LastResult.Array.Length)
				{
					int newSize = HashHelper.NextSize(false, this.items.Size);
					Array.Resize<T>(ref this.LastResult.Array, newSize);
				}
				for (int i = 1; i < this.count.Length; i++)
				{
					this.count[i] = this.count[i] + this.count[i - 1];
				}
				for (int j = this.items.Size - 1; j >= 0; j--)
				{
					CountingSort<T>.Tuple tuple = this.items.Array[j];
					int num = this.count[tuple.Key] - 1;
					this.LastResult.Array[num] = tuple.Value;
					this.count[tuple.Key]--;
				}
				int size = this.LastResult.Size;
				this.LastResult.Size = this.items.Size;
				if (CountingSort<T>.TypeDesc.IsManaged)
				{
					this.items.Clear();
					if (size > this.LastResult.Size)
					{
						Array.Clear(this.LastResult.Array, this.LastResult.Size, size - this.LastResult.Size);
					}
				}
				else
				{
					this.items.Size = 0;
				}
				Array.Clear(this.count, 0, this.count.Length);
				return;
			}
			if (CountingSort<T>.TypeDesc.IsManaged)
			{
				this.LastResult.Clear();
				return;
			}
			this.LastResult.Size = 0;
		}

		// Token: 0x0400056C RID: 1388
		private static readonly TlistTypeWrapper<T> TypeDesc = new TlistTypeWrapper<T>();

		// Token: 0x0400056D RID: 1389
		private Tlist<CountingSort<T>.Tuple> items;

		// Token: 0x0400056E RID: 1390
		private int minKey;

		// Token: 0x0400056F RID: 1391
		private int maxKey;

		// Token: 0x04000570 RID: 1392
		private int minGivenKey;

		// Token: 0x04000571 RID: 1393
		private int maxGivenKey;

		// Token: 0x04000572 RID: 1394
		private int[] count;

		// Token: 0x04000573 RID: 1395
		public Tlist<T> LastResult;

		// Token: 0x0200010E RID: 270
		private struct Tuple
		{
			// Token: 0x060007BA RID: 1978 RVA: 0x00027238 File Offset: 0x00025438
			public Tuple(int key, T value)
			{
				this.Key = key;
				this.Value = value;
			}

			// Token: 0x04000574 RID: 1396
			public int Key;

			// Token: 0x04000575 RID: 1397
			public T Value;
		}
	}
}
