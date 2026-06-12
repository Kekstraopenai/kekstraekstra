using System;
using System.Collections.Generic;

namespace TheraEngine.Collections
{
	// Token: 0x0200012A RID: 298
	internal struct FunctorComparer<T> : IComparer<!0>
	{
		// Token: 0x06000853 RID: 2131 RVA: 0x0002919B File Offset: 0x0002739B
		public FunctorComparer(Comparison<T> comparison)
		{
			this.comparison = comparison;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000291A4 File Offset: 0x000273A4
		public int Compare(T x, T y)
		{
			return this.comparison(x, y);
		}

		// Token: 0x040005C2 RID: 1474
		private Comparison<T> comparison;
	}
}
