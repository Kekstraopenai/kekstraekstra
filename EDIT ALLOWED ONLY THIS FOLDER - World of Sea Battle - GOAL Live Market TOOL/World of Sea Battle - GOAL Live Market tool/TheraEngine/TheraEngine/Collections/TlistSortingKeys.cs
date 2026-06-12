using System;

namespace TheraEngine.Collections
{
	// Token: 0x0200012C RID: 300
	internal class TlistSortingKeys<T>
	{
		// Token: 0x040005C5 RID: 1477
		internal float[] cFloat = Tlist<float>.EmptyReadonly.Array;

		// Token: 0x040005C6 RID: 1478
		internal int[] cInt = Tlist<int>.EmptyReadonly.Array;

		// Token: 0x040005C7 RID: 1479
		internal int[] counters = Tlist<int>.EmptyReadonly.Array;

		// Token: 0x040005C8 RID: 1480
		internal T[] clone = Tlist<T>.EmptyReadonly.Array;
	}
}
