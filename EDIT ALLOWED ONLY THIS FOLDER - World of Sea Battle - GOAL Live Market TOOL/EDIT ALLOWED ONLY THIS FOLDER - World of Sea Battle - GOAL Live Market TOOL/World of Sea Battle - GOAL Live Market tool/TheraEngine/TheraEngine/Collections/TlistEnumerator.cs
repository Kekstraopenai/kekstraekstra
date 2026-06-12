using System;
using System.Collections;
using System.Collections.Generic;

namespace TheraEngine.Collections
{
	// Token: 0x02000129 RID: 297
	internal struct TlistEnumerator<T> : IEnumerator<!0>, IEnumerator, IDisposable
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x00029121 File Offset: 0x00027321
		public TlistEnumerator(Tlist<T> list)
		{
			this.currentIndex = -1;
			this.list = list;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00029131 File Offset: 0x00027331
		public T Current
		{
			get
			{
				return this.list.Array[this.currentIndex];
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00029149 File Offset: 0x00027349
		object IEnumerator.Current
		{
			get
			{
				return this.list.Array[this.currentIndex];
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00029166 File Offset: 0x00027366
		public void Dispose()
		{
			this.list = null;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0002916F File Offset: 0x0002736F
		public bool MoveNext()
		{
			this.currentIndex++;
			return this.currentIndex < this.list.Size;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00029192 File Offset: 0x00027392
		public void Reset()
		{
			this.currentIndex = 0;
		}

		// Token: 0x040005C0 RID: 1472
		private Tlist<T> list;

		// Token: 0x040005C1 RID: 1473
		private int currentIndex;
	}
}
