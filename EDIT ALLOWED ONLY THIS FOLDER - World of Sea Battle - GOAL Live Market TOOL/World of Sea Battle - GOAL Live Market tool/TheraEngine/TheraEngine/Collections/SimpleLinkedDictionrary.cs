using System;

namespace TheraEngine.Collections
{
	// Token: 0x02000122 RID: 290
	public class SimpleLinkedDictionrary<X, Y>
	{
		// Token: 0x06000824 RID: 2084 RVA: 0x000286BE File Offset: 0x000268BE
		public SimpleLinkedDictionrary() : this(new Tlist<SimpleLinkedDictionrary<X, Y>.Node>())
		{
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000286CB File Offset: 0x000268CB
		public SimpleLinkedDictionrary(Tlist<SimpleLinkedDictionrary<X, Y>.Node> initialInnerList)
		{
			this.InnerList = initialInnerList;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x000286E8 File Offset: 0x000268E8
		public Tlist<Y> Fetch(X key, bool crearing)
		{
			for (int i = 0; i < this.InnerList.Size; i++)
			{
				if (this.InnerList.Array[i].Key.Equals(key))
				{
					return this.InnerList.Array[i].Value;
				}
			}
			if (crearing)
			{
				Tlist<Y> tlist = new Tlist<Y>();
				Tlist<SimpleLinkedDictionrary<X, Y>.Node> innerList = this.InnerList;
				SimpleLinkedDictionrary<X, Y>.Node node = new SimpleLinkedDictionrary<X, Y>.Node(key, tlist);
				innerList.Add(node);
				return tlist;
			}
			return null;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002876C File Offset: 0x0002696C
		public void Add(X key, Y item)
		{
			Tlist<Y> tlist = this.Fetch(key, true);
			if (this.DisallowAddSameItem)
			{
				for (int i = 0; i < tlist.Size; i++)
				{
					if (tlist.Array[i].Equals(item))
					{
						return;
					}
				}
			}
			tlist.Add(item);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000287C8 File Offset: 0x000269C8
		public bool TrySearch(X key, Func<Y, bool> searchFunc, out Y item)
		{
			Tlist<Y> tlist = this.Fetch(key, false);
			if (tlist == null)
			{
				item = default(Y);
				return false;
			}
			for (int i = 0; i < tlist.Size; i++)
			{
				if (searchFunc(tlist.Array[i]))
				{
					item = tlist.Array[i];
					return true;
				}
			}
			item = default(Y);
			return false;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002882C File Offset: 0x00026A2C
		public bool TryRemove(X key, Func<Y, bool> searchFunc, out Y item)
		{
			Tlist<Y> tlist = this.Fetch(key, false);
			item = default(Y);
			if (tlist == null)
			{
				return false;
			}
			for (int i = 0; i < tlist.Size; i++)
			{
				if (searchFunc(tlist.Array[i]))
				{
					item = tlist.Array[i];
					tlist.FastRemoveAt(i);
					if (tlist.Size == 0 && this.AutoCleanup)
					{
						for (int j = 0; j < this.InnerList.Size; j++)
						{
							if (this.InnerList.Array[j].Key.Equals(key))
							{
								this.InnerList.FastRemoveAt(j);
								break;
							}
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x040005A5 RID: 1445
		public Tlist<SimpleLinkedDictionrary<X, Y>.Node> InnerList;

		// Token: 0x040005A6 RID: 1446
		public bool DisallowAddSameItem = true;

		// Token: 0x040005A7 RID: 1447
		public bool AutoCleanup = true;

		// Token: 0x02000123 RID: 291
		public struct Node
		{
			// Token: 0x0600082A RID: 2090 RVA: 0x000288F2 File Offset: 0x00026AF2
			public Node(X key, Tlist<Y> value)
			{
				this.Key = key;
				this.Value = value;
			}

			// Token: 0x040005A8 RID: 1448
			public X Key;

			// Token: 0x040005A9 RID: 1449
			public Tlist<Y> Value;
		}
	}
}
