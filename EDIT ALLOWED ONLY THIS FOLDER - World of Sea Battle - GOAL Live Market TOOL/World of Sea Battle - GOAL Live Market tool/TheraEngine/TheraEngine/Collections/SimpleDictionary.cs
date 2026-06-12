using System;
using System.Threading;
using TheraEngine.Helpers;

namespace TheraEngine.Collections
{
	// Token: 0x02000121 RID: 289
	public class SimpleDictionary<T> where T : class
	{
		// Token: 0x06000821 RID: 2081 RVA: 0x000285DC File Offset: 0x000267DC
		public void AddOrUpdate(int index, T item)
		{
			this.rwl.EnterWriteLock();
			if (index >= this.Prototype.Size)
			{
				Array.Resize<T>(ref this.Prototype.Array, HashHelper.NextSize(true, index));
			}
			this.Prototype.Array[index] = item;
			this.Prototype.Size = Math.Max(this.Prototype.Size, index + 1);
			this.rwl.ExitWriteLock();
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00028654 File Offset: 0x00026854
		public T Get(int index)
		{
			T result = default(T);
			this.rwl.EnterReadLock();
			if (index < this.Prototype.Size)
			{
				result = this.Prototype.Array[index];
			}
			this.rwl.ExitReadLock();
			return result;
		}

		// Token: 0x040005A3 RID: 1443
		public readonly Tlist<T> Prototype = new Tlist<T>();

		// Token: 0x040005A4 RID: 1444
		private ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();
	}
}
