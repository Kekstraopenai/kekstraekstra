using System;
using System.Runtime.CompilerServices;

namespace TheraEngine.Collections
{
	// Token: 0x0200012B RID: 299
	internal class TlistTypeWrapper<T>
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x000291B3 File Offset: 0x000273B3
		public TlistTypeWrapper()
		{
			this.IsManaged = RuntimeHelpers.IsReferenceOrContainsReferences<T>();
			this.IsMemoryCritical = (!this.IsManaged && Unsafe.SizeOf<T>() >= 64);
		}

		// Token: 0x040005C3 RID: 1475
		public bool IsManaged;

		// Token: 0x040005C4 RID: 1476
		public bool IsMemoryCritical;
	}
}
