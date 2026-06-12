using System;

namespace TheraEngine
{
	// Token: 0x02000029 RID: 41
	public class Ref<T> where T : struct
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00002EF6 File Offset: 0x000010F6
		public Ref()
		{
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000085E6 File Offset: 0x000067E6
		public Ref(T {11759})
		{
			this.Value = {11759};
		}

		// Token: 0x040000C1 RID: 193
		public T Value;
	}
}
