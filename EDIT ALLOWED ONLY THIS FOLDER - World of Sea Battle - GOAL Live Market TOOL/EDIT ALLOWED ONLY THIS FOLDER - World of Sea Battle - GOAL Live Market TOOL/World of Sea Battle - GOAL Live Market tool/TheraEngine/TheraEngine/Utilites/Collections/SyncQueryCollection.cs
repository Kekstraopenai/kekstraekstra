using System;
using TheraEngine.Collections;

namespace TheraEngine.Utilites.Collections
{
	// Token: 0x02000136 RID: 310
	public class SyncQueryCollection<T>
	{
		// Token: 0x060008D5 RID: 2261 RVA: 0x0002AF4D File Offset: 0x0002914D
		public SyncQueryCollection(int initialCapacity, Action<Exception> errorHandler)
		{
			this.errorHandler = errorHandler;
			this.actions1 = new Tlist<Action<T>>(initialCapacity);
			this.actions2 = new Tlist<Action<T>>(initialCapacity / 2);
			this.syncRoot = new object();
			this.commonSyncRoot = new object();
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002AF8C File Offset: 0x0002918C
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

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002B0D8 File Offset: 0x000292D8
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

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002B138 File Offset: 0x00029338
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

		// Token: 0x040005E9 RID: 1513
		private Tlist<Action<T>> actions1;

		// Token: 0x040005EA RID: 1514
		private Tlist<Action<T>> actions2;

		// Token: 0x040005EB RID: 1515
		private object syncRoot;

		// Token: 0x040005EC RID: 1516
		private object commonSyncRoot;

		// Token: 0x040005ED RID: 1517
		private bool actions1SyncRoot;

		// Token: 0x040005EE RID: 1518
		private Action<Exception> errorHandler;
	}
}
