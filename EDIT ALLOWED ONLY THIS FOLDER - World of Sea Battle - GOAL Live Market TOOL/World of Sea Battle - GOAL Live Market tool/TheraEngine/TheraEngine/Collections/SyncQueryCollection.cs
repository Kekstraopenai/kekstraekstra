using System;

namespace TheraEngine.Collections
{
	// Token: 0x02000127 RID: 295
	public class SyncQueryCollection<T>
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x00028E4B File Offset: 0x0002704B
		public SyncQueryCollection(int initialCapacity, Action<Exception> errorHandler)
		{
			this.errorHandler = errorHandler;
			this.actions1 = new Tlist<Action<T>>(initialCapacity);
			this.actions2 = new Tlist<Action<T>>(initialCapacity / 2);
			this.syncRoot = new object();
			this.commonSyncRoot = new object();
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00028E8C File Offset: 0x0002708C
		public void Apply(T argument)
		{
			object obj = this.commonSyncRoot;
			lock (obj)
			{
				object obj2 = this.syncRoot;
				lock (obj2)
				{
					this.actions1SyncRoot = true;
				}
				for (int i = 0; i < this.actions1.Size; i++)
				{
					try
					{
						this.actions1.Array[i](argument);
					}
					catch (Exception obj3)
					{
						this.errorHandler(obj3);
					}
				}
				this.actions1.Clear();
				obj2 = this.syncRoot;
				lock (obj2)
				{
					this.actions1SyncRoot = false;
				}
				for (int j = 0; j < this.actions2.Size; j++)
				{
					try
					{
						this.actions2.Array[j](argument);
					}
					catch (Exception obj4)
					{
						this.errorHandler(obj4);
					}
				}
				this.actions2.Clear();
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00028FD8 File Offset: 0x000271D8
		public void Add(Action<T> actionDelegate)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.actions1SyncRoot)
				{
					this.actions2.Add(actionDelegate);
				}
				else
				{
					this.actions1.Add(actionDelegate);
				}
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00029038 File Offset: 0x00027238
		public void Clear()
		{
			object obj = this.commonSyncRoot;
			lock (obj)
			{
				object obj2 = this.syncRoot;
				lock (obj2)
				{
					this.actions1.Clear();
					this.actions2.Clear();
				}
			}
		}

		// Token: 0x040005B8 RID: 1464
		private Tlist<Action<T>> actions1;

		// Token: 0x040005B9 RID: 1465
		private Tlist<Action<T>> actions2;

		// Token: 0x040005BA RID: 1466
		private object syncRoot;

		// Token: 0x040005BB RID: 1467
		private object commonSyncRoot;

		// Token: 0x040005BC RID: 1468
		private bool actions1SyncRoot;

		// Token: 0x040005BD RID: 1469
		private Action<Exception> errorHandler;
	}
}
