using System;
using System.Threading;

namespace TheraEngine
{
	// Token: 0x0200002F RID: 47
	public class ReverseSemaphore
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00008D01 File Offset: 0x00006F01
		public ReverseSemaphore()
		{
			this.{11809} = 0;
			this.{11810} = new object();
			this.{11811} = new ManualResetEventSlim(true);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008D27 File Offset: 0x00006F27
		public void Wait()
		{
			this.{11811}.Wait();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008D34 File Offset: 0x00006F34
		public void ChangeCounter(int {11808})
		{
			object obj = this.{11810};
			lock (obj)
			{
				int num = this.{11809} + {11808};
				if (num < 0 || {11808} == 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (this.{11809} == 0)
				{
					this.{11811}.Reset();
				}
				else if (num == 0)
				{
					this.{11811}.Set();
				}
				this.{11809} = num;
			}
		}

		// Token: 0x040000DA RID: 218
		private int {11809};

		// Token: 0x040000DB RID: 219
		private object {11810};

		// Token: 0x040000DC RID: 220
		private ManualResetEventSlim {11811};
	}
}
