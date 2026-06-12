using System;
using TheraEngine.Helpers;

namespace TheraEngine.Interface.Animation
{
	// Token: 0x020000DE RID: 222
	public sealed class UiBrightnessAnimation : UiAction
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x0001F173 File Offset: 0x0001D373
		public UiBrightnessAnimation(UiControl {14222}, float {14223}, float {14224}) : this({14222}, 0f, {14223}, {14224}, false)
		{
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001F184 File Offset: 0x0001D384
		public UiBrightnessAnimation(UiControl {14225}, float {14226}, float {14227}, float {14228}) : this({14225}, {14226}, {14227}, {14228}, true)
		{
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001F194 File Offset: 0x0001D394
		private UiBrightnessAnimation(UiControl {14229}, float {14230}, float {14231}, float {14232}, bool {14233})
		{
			this.{14238} = 1000f / {14232};
			this.{14239} = {14232};
			this.{14236} = {14230};
			this.{14237} = {14231};
			if ({14233} && {14229}.AnimationsCount == 0)
			{
				{14229}.Brightness = {14230};
			}
			{14229}.AddAction(this);
			this.{14241} = {14233};
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001F1F0 File Offset: 0x0001D3F0
		protected internal override bool Update(ref FrameTime {14234}, UiControl {14235})
		{
			if (!this.{14241})
			{
				this.{14236} = {14235}.Brightness;
				this.{14241} = true;
			}
			this.{14239} -= {14234}.msElapsed;
			if (this.{14239} < 1f)
			{
				{14235}.Brightness = this.{14237};
				return true;
			}
			this.{14240} += this.{14238} * {14234}.secElapsed;
			this.{14240} = Geometry.Saturate(this.{14240});
			{14235}.Brightness = this.{14236} * (1f - this.{14240}) + this.{14237} * this.{14240};
			return false;
		}

		// Token: 0x04000471 RID: 1137
		private float {14236};

		// Token: 0x04000472 RID: 1138
		private float {14237};

		// Token: 0x04000473 RID: 1139
		private float {14238};

		// Token: 0x04000474 RID: 1140
		private float {14239};

		// Token: 0x04000475 RID: 1141
		private float {14240};

		// Token: 0x04000476 RID: 1142
		private bool {14241};
	}
}
