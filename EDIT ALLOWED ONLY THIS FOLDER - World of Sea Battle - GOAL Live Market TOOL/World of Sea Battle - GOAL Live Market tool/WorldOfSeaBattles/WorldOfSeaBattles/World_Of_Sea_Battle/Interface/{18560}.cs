using System;
using TheraEngine.Collections;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000152 RID: 338
	internal sealed class {18560}
	{
		// Token: 0x060007B2 RID: 1970 RVA: 0x00003100 File Offset: 0x00001300
		public static void Render3D()
		{
		}

		// Token: 0x040006D1 RID: 1745
		public static bool closed = true;

		// Token: 0x040006D2 RID: 1746
		public static bool extremeVisibleDistance = false;

		// Token: 0x040006D3 RID: 1747
		internal static Tlist<object> createdIslesTotal;

		// Token: 0x040006D4 RID: 1748
		public static bool disallowCamZoom = false;

		// Token: 0x040006D5 RID: 1749
		public static {18560}.VisibleDistance visibility = {18560}.VisibleDistance.Normal;

		// Token: 0x02000153 RID: 339
		public enum VisibleDistance
		{
			// Token: 0x040006D7 RID: 1751
			Normal,
			// Token: 0x040006D8 RID: 1752
			HighWithOcean,
			// Token: 0x040006D9 RID: 1753
			HighNoOcean
		}
	}
}
