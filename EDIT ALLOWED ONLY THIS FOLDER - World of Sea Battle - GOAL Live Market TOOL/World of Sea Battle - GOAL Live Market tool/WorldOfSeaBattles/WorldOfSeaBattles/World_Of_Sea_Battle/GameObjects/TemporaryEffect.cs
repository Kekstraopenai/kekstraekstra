using System;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x02000065 RID: 101
	public class TemporaryEffect<T>
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0001AF57 File Offset: 0x00019157
		public TemporaryEffect(T {17034}, float {17035})
		{
			this.Data = {17034};
			this.InitialTime = {17035};
			this.TimeoutMs = {17035};
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001AF74 File Offset: 0x00019174
		public float FadeEffect(float {17036})
		{
			if (this.TimeoutMs > {17036})
			{
				return 1f;
			}
			return this.TimeoutMs / {17036};
		}

		// Token: 0x0400027B RID: 635
		public readonly T Data;

		// Token: 0x0400027C RID: 636
		public readonly float InitialTime;

		// Token: 0x0400027D RID: 637
		public float TimeoutMs;
	}
}
