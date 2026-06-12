using System;

namespace TheraEngine.Interface.Animation
{
	// Token: 0x020000DB RID: 219
	public sealed class UiActionsSleep : UiAction
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x0001F094 File Offset: 0x0001D294
		public UiActionsSleep(UiControl {14187}, float {14188})
		{
			this.{14191} = {14188};
			{14187}.AddAction(this);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001F0AA File Offset: 0x0001D2AA
		protected internal override bool Update(ref FrameTime {14189}, UiControl {14190})
		{
			this.{14191} -= {14189}.msElapsed;
			return this.{14191} < 1f;
		}

		// Token: 0x0400046C RID: 1132
		private float {14191};
	}
}
