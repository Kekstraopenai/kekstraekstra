using System;
using System.Collections.Concurrent;

namespace TheraEngine.Collections
{
	// Token: 0x0200011F RID: 287
	public class ConcurrentScoreDictionary<T>
	{
		// Token: 0x17000172 RID: 370
		public float this[T key]
		{
			get
			{
				float result;
				if (this.dict.TryGetValue(key, out result))
				{
					return result;
				}
				return 0f;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00028554 File Offset: 0x00026754
		public int Count
		{
			get
			{
				return this.dict.Count;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00028561 File Offset: 0x00026761
		public ConcurrentDictionary<T, float> BaseDictionary
		{
			get
			{
				return this.dict;
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00028569 File Offset: 0x00026769
		public ConcurrentScoreDictionary()
		{
			this.dict = new ConcurrentDictionary<T, float>();
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0002857C File Offset: 0x0002677C
		public ConcurrentScoreDictionary(ConcurrentDictionary<T, float> baseDict)
		{
			this.dict = baseDict;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0002858C File Offset: 0x0002678C
		public void AddScore(T key, float addScore)
		{
			this.dict.AddOrUpdate(key, addScore, (T x, float y) => y + addScore);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000285C5 File Offset: 0x000267C5
		public void Clear()
		{
			this.dict.Clear();
		}

		// Token: 0x040005A1 RID: 1441
		private ConcurrentDictionary<T, float> dict;
	}
}
