using System;
using System.Collections.Generic;

namespace TheraEngine.Collections
{
	// Token: 0x02000112 RID: 274
	public class LinkedDictionrary<X, Y> : Dictionary<X, ObservableTlist<Y>>
	{
		// Token: 0x17000162 RID: 354
		public new ObservableTlist<Y> this[X key]
		{
			get
			{
				return this.Get(key);
			}
			set
			{
				base.Add(key, value);
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00027382 File Offset: 0x00025582
		public LinkedDictionrary()
		{
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0002738A File Offset: 0x0002558A
		public LinkedDictionrary(int count) : base(count)
		{
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00027394 File Offset: 0x00025594
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

		// Token: 0x060007CC RID: 1996 RVA: 0x000273C4 File Offset: 0x000255C4
		public ObservableTlist<Y> Get(X key)
		{
			ObservableTlist<Y> result;
			if (!base.TryGetValue(key, out result))
			{
				return LinkedDictionrary<X, Y>._emptyTlist;
			}
			return result;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000273E4 File Offset: 0x000255E4
		public bool Remove(X key, Y value)
		{
			ObservableTlist<Y> observableTlist;
			return base.TryGetValue(key, out observableTlist) && observableTlist.FastRemove(value);
		}

		// Token: 0x04000579 RID: 1401
		private static ObservableTlist<Y> _emptyTlist = new ObservableTlist<Y>
		{
			Array = new Y[0],
			Size = 0
		};
	}
}
