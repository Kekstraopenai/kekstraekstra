using System;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x020001A6 RID: 422
	public class AnimationLabelManager
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00035578 File Offset: 0x00033778
		internal TimeSpan CurrentTimeItem1
		{
			get
			{
				return new TimeSpan((long)(this.CurrentTime.Value.Item1 * (double)this.{15956}.currentClipValue.Duration.Ticks));
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x000355B8 File Offset: 0x000337B8
		internal TimeSpan CurrentTimeItem2
		{
			get
			{
				return new TimeSpan((long)(this.CurrentTime.Value.Item2 * (double)this.{15956}.currentClipValue.Duration.Ticks));
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000355F5 File Offset: 0x000337F5
		internal AnimationLabelManager(AnimationUnit {15951})
		{
			this.DictionaryRef = null;
			this.{15956} = {15951};
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0003560C File Offset: 0x0003380C
		public void BeginAnimation(object {15952})
		{
			this.CurrentAnimationTag = {15952};
			this.CurrentTime = new ValueTuple<double, double>?(this.DictionaryRef.animations[{15952}]);
			this.{15956}.currentTimeValue = this.CurrentTimeItem1;
			this.{15956}.currentKeyframe = 0;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00035659 File Offset: 0x00033859
		private static bool EqualsHelper(object {15953}, object {15954})
		{
			if ({15953} == null || {15954} == null)
			{
				return false;
			}
			if ({15953}.GetType().IsEnum)
			{
				return {15953}.GetHashCode() == {15954}.GetHashCode();
			}
			return object.Equals({15953}, {15954});
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00035688 File Offset: 0x00033888
		public void EvaluteAnimation(object {15955})
		{
			if (AnimationLabelManager.EqualsHelper({15955}, this.CurrentAnimationTag))
			{
				return;
			}
			if (this.CurrentTime == null || AnimationLabelManager.EqualsHelper(this.CurrentAnimationTag, this.AllowAgressiveOverlapAnimationTag))
			{
				this.BeginAnimation({15955});
				return;
			}
			ValueTuple<double, double> valueTuple = this.DictionaryRef.animations[{15955}];
			if (this.CurrentTime.Value.Item1 == valueTuple.Item1)
			{
				return;
			}
			if (Math.Abs(this.CurrentTimeItem2.TotalSeconds - this.{15956}.currentTimeValue.TotalSeconds) < 0.2)
			{
				this.BeginAnimation({15955});
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00035730 File Offset: 0x00033930
		public float CurrentAnimationFactor
		{
			get
			{
				return (float)((this.CurrentTimeItem2.TotalSeconds - this.{15956}.currentTimeValue.TotalSeconds) / (this.CurrentTimeItem2.TotalSeconds - this.CurrentTimeItem1.TotalSeconds));
			}
		}

		// Token: 0x04000835 RID: 2101
		public AnimationDictionary DictionaryRef;

		// Token: 0x04000836 RID: 2102
		private AnimationUnit {15956};

		// Token: 0x04000837 RID: 2103
		internal ValueTuple<double, double>? CurrentTime;

		// Token: 0x04000838 RID: 2104
		public object CurrentAnimationTag;

		// Token: 0x04000839 RID: 2105
		public object AllowAgressiveOverlapAnimationTag;
	}
}
