using System;

namespace TheraEngine.Interface.Animation
{
	// Token: 0x020000DD RID: 221
	public sealed class UiBlinkingAnimation : UiAction
	{
		// Token: 0x06000600 RID: 1536 RVA: 0x0001F0F7 File Offset: 0x0001D2F7
		public UiBlinkingAnimation(UiControl {14202}, float {14203}, float {14204})
		{
			this.{14207} = {14203};
			this.{14209} = {14204};
			{14202}.AddAction(this);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001F114 File Offset: 0x0001D314
		protected internal override bool Update(ref FrameTime {14205}, UiControl {14206})
		{
			this.{14208} += {14205}.secElapsed;
			{14206}.FirstOpacity = (MathF.Sin(this.{14208} * 6.2831855f * this.{14207}) * 0.5f + 0.5f) * (1f - this.{14209}) + this.{14209};
			return false;
		}

		// Token: 0x0400046E RID: 1134
		private float {14207};

		// Token: 0x0400046F RID: 1135
		private float {14208};

		// Token: 0x04000470 RID: 1136
		private float {14209};
	}
}
