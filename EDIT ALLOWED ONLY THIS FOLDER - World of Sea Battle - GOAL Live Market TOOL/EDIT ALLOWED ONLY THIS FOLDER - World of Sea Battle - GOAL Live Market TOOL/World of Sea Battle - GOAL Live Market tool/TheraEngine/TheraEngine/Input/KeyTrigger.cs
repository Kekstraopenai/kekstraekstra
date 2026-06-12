using System;

namespace TheraEngine.Input
{
	// Token: 0x020000E6 RID: 230
	public class KeyTrigger : Trigger
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0001FD0A File Offset: 0x0001DF0A
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x0001FD12 File Offset: 0x0001DF12
		public override bool Value
		{
			get
			{
				return base.Value;
			}
			protected set
			{
				base.Value = value;
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001FD1B File Offset: 0x0001DF1B
		public KeyTrigger(float {14342}, float {14343}) : base(false)
		{
			this.{14346} = {14342};
			this.{14347} = {14343};
			this.{14348} = this.{14346};
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001FD40 File Offset: 0x0001DF40
		public bool Update(ref FrameTime {14344}, bool {14345})
		{
			if (this.Value)
			{
				this.{14348} -= {14344}.msElapsed;
				if (this.{14348} < 0f && !{14345})
				{
					this.{14348} = this.{14346};
					this.Value = false;
					return true;
				}
			}
			else if ({14345})
			{
				this.{14348} -= {14344}.msElapsed;
				if (this.{14348} < 0f)
				{
					this.{14348} = this.{14347};
					this.Value = true;
					return true;
				}
			}
			else
			{
				this.{14348} = this.{14346};
			}
			return false;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001FDD2 File Offset: 0x0001DFD2
		public void ResetState()
		{
			this.Value = false;
			this.{14348} = this.{14346};
		}

		// Token: 0x040004A8 RID: 1192
		private readonly float {14346};

		// Token: 0x040004A9 RID: 1193
		private readonly float {14347};

		// Token: 0x040004AA RID: 1194
		private float {14348};
	}
}
