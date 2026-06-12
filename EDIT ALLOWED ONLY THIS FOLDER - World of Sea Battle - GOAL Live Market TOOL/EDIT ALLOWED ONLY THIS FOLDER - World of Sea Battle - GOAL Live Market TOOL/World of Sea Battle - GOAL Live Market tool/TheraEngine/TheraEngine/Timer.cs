using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TheraEngine
{
	// Token: 0x02000031 RID: 49
	public class Timer
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000195 RID: 405 RVA: 0x00008DF8 File Offset: 0x00006FF8
		// (remove) Token: 0x06000196 RID: 406 RVA: 0x00008E30 File Offset: 0x00007030
		public event Action SampleAction
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{11821};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{11821}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{11821};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{11821}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00008E65 File Offset: 0x00007065
		public float Interval
		{
			get
			{
				return this.{11822};
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008E6D File Offset: 0x0000706D
		public Timer(float {11819})
		{
			this.{11822} = {11819};
			this.{11823} = {11819};
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008E84 File Offset: 0x00007084
		public bool Sample(ref FrameTime {11820})
		{
			this.{11823} -= {11820}.msElapsed;
			if (this.{11823} < 0f)
			{
				this.{11823} += this.{11822};
				Action action = this.{11821};
				if (action != null)
				{
					action();
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00008ED8 File Offset: 0x000070D8
		public void Reset()
		{
			this.{11823} = 0f;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008EE5 File Offset: 0x000070E5
		public void Ceiling()
		{
			this.{11823} = this.{11822};
		}

		// Token: 0x040000DF RID: 223
		[CompilerGenerated]
		private Action {11821};

		// Token: 0x040000E0 RID: 224
		private float {11822};

		// Token: 0x040000E1 RID: 225
		private float {11823};
	}
}
