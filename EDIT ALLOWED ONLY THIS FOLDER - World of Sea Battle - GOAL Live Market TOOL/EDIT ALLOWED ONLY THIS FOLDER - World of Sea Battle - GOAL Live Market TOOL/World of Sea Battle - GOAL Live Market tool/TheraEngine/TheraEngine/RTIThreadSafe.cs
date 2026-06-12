using System;

namespace TheraEngine
{
	// Token: 0x02000017 RID: 23
	public class RTIThreadSafe
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000032F8 File Offset: 0x000014F8
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003340 File Offset: 0x00001540
		public int Value
		{
			get
			{
				object obj = this.{11370};
				int value;
				lock (obj)
				{
					value = this.{11369}.Value;
				}
				return value;
			}
			set
			{
				object obj = this.{11370};
				lock (obj)
				{
					this.{11369}.Value = value;
				}
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003388 File Offset: 0x00001588
		public RTIThreadSafe(int {11367})
		{
			this.{11369} = {11367};
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000339C File Offset: 0x0000159C
		public RTIThreadSafe(RTI {11368})
		{
			this.{11369} = {11368};
		}

		// Token: 0x04000065 RID: 101
		private RTI {11369};

		// Token: 0x04000066 RID: 102
		private object {11370};
	}
}
