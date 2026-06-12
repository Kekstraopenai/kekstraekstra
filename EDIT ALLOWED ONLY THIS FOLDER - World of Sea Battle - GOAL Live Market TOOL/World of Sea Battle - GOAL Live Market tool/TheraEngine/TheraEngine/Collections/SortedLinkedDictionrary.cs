using System;
using System.Collections.Generic;

namespace TheraEngine.Collections
{
	// Token: 0x02000110 RID: 272
	public class SortedLinkedDictionrary<X, Y> : SortedDictionary<X, ObservableTlist<Y>>
	{
		// Token: 0x17000161 RID: 353
		public new ObservableTlist<Y> this[X key]
		{
			get
			{
				return this.Get(key);
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0002728B File Offset: 0x0002548B
		public SortedLinkedDictionrary(bool inverseSort = false) : base(new SortedLinkedDictionrary<X, Y>.InternalComparer(inverseSort))
		{
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x000272A0 File Offset: 0x000254A0
		public void Add(X key, Y value)
		{
			ObservableTlist<Y> observableTlist;
			if (!base.TryGetValue(key, out observableTlist))
			{
				observableTlist = new ObservableTlist<Y>();
				base.Add(key, observableTlist);
			}
			observableTlist.Add(value);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000272D0 File Offset: 0x000254D0
		public ObservableTlist<Y> Get(X key)
		{
			ObservableTlist<Y> result;
			if (!base.TryGetValue(key, out result))
			{
				return SortedLinkedDictionrary<X, Y>._emptyTlist;
			}
			return result;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000272F0 File Offset: 0x000254F0
		public bool Remove(X key, Y value)
		{
			ObservableTlist<Y> observableTlist;
			return base.TryGetValue(key, out observableTlist) && observableTlist.FastRemove(value);
		}

		// Token: 0x04000576 RID: 1398
		private static ObservableTlist<Y> _emptyTlist = new ObservableTlist<Y>
		{
			Array = new Y[0],
			Size = 0
		};

		// Token: 0x02000111 RID: 273
		private struct InternalComparer : IComparer<X>
		{
			// Token: 0x060007C5 RID: 1989 RVA: 0x00027331 File Offset: 0x00025531
			public InternalComparer(bool inverseSort)
			{
				this.inverseSort = inverseSort;
				this.defaultComparer = Comparer<X>.Default;
			}

			// Token: 0x060007C6 RID: 1990 RVA: 0x00027348 File Offset: 0x00025548
			int IComparer<!0>.Compare(X x, X y)
			{
				int num = this.defaultComparer.Compare(x, y);
				if (this.inverseSort)
				{
					return -num;
				}
				return num;
			}

			// Token: 0x04000577 RID: 1399
			private IComparer<X> defaultComparer;

			// Token: 0x04000578 RID: 1400
			private bool inverseSort;
		}
	}
}
