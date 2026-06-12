using System;

namespace TheraEngine.Collections
{
	// Token: 0x02000128 RID: 296
	public readonly struct TinyMap<TKey, TValue>
	{
		// Token: 0x17000179 RID: 377
		public TValue this[TKey key]
		{
			get
			{
				return this.Data[this.getIndex(key)];
			}
			set
			{
				this.Data[this.getIndex(key)] = value;
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000290E3 File Offset: 0x000272E3
		public TinyMap(Func<TKey, int> getIndex, int maxIndex)
		{
			this.getIndex = getIndex;
			this.Data = new TValue[maxIndex];
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000290F8 File Offset: 0x000272F8
		public void Set(byte[] fromData)
		{
			Buffer.BlockCopy(fromData, 0, this.Data, 0, fromData.Length);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0002910B File Offset: 0x0002730B
		public void CleanArray()
		{
			Array.Clear(this.Data, 0, this.Data.Length);
		}

		// Token: 0x040005BE RID: 1470
		public readonly TValue[] Data;

		// Token: 0x040005BF RID: 1471
		private readonly Func<TKey, int> getIndex;
	}
}
