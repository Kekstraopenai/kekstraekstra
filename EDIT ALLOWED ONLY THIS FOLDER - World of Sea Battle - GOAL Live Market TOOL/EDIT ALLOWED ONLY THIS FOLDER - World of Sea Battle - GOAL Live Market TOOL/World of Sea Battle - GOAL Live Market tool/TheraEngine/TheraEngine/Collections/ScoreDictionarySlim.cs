using System;

namespace TheraEngine.Collections
{
	// Token: 0x0200011E RID: 286
	public class ScoreDictionarySlim
	{
		// Token: 0x17000171 RID: 369
		public float this[int key]
		{
			get
			{
				if (key >= this.raw.Length)
				{
					return 0f;
				}
				return this.raw[key];
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x000284C0 File Offset: 0x000266C0
		public ScoreDictionarySlim() : this(4)
		{
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000284C9 File Offset: 0x000266C9
		public ScoreDictionarySlim(int initialTypeCapacity)
		{
			this.raw = new float[initialTypeCapacity];
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000284E0 File Offset: 0x000266E0
		public void AddScore(int key, float score)
		{
			if (key < 0)
			{
				throw new ArgumentException();
			}
			if (key > this.raw.Length)
			{
				Array.Resize<float>(ref this.raw, Math.Max(this.raw.Length * 2, key));
			}
			this.raw[key] += score;
		}

		// Token: 0x040005A0 RID: 1440
		private float[] raw;
	}
}
