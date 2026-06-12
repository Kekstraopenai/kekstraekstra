using System;

namespace TheraEngine.Interface.Animation
{
	// Token: 0x020000E2 RID: 226
	public sealed class UiOpacityAnimation : UiAction
	{
		// Token: 0x0600060B RID: 1547 RVA: 0x0001F5D0 File Offset: 0x0001D7D0
		public UiOpacityAnimation(UiControl {14299}, float {14300}, float {14301}) : this({14299}, 0f, {14300}, {14301}, false)
		{
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		public UiOpacityAnimation(UiControl {14302}, float {14303}, float {14304}, float {14305}) : this({14302}, {14303}, {14304}, {14305}, true)
		{
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001F5F0 File Offset: 0x0001D7F0
		private UiOpacityAnimation(UiControl {14306}, float {14307}, float {14308}, float {14309}, bool {14310})
		{
			this.{14315} = 1000f / {14309};
			this.{14316} = {14309};
			this.{14313} = {14307};
			this.{14314} = {14308};
			if ({14310} && {14306}.AnimationsCount == 0)
			{
				{14306}.Opacity = {14307};
			}
			{14306}.AddAction(this);
			this.{14318} = {14310};
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001F64C File Offset: 0x0001D84C
		protected internal override bool Update(ref FrameTime {14311}, UiControl {14312})
		{
			if (!this.{14318})
			{
				this.{14313} = {14312}.Opacity;
				this.{14318} = true;
			}
			this.{14316} -= {14311}.msElapsed;
			if (this.{14316} < 1f)
			{
				{14312}.Opacity = this.{14314};
				return true;
			}
			this.{14317} += this.{14315} * {14311}.secElapsed;
			{14312}.Opacity = this.{14313} * (1f - this.{14317}) + this.{14314} * this.{14317};
			return false;
		}

		// Token: 0x04000489 RID: 1161
		private float {14313};

		// Token: 0x0400048A RID: 1162
		private float {14314};

		// Token: 0x0400048B RID: 1163
		private float {14315};

		// Token: 0x0400048C RID: 1164
		private float {14316};

		// Token: 0x0400048D RID: 1165
		private float {14317};

		// Token: 0x0400048E RID: 1166
		private bool {14318};
	}
}
