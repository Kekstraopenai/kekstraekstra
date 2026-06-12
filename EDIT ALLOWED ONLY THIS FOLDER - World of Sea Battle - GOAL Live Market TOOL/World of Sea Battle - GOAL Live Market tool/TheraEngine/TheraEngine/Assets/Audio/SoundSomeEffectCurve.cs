using System;

namespace TheraEngine.Assets.Audio
{
	// Token: 0x020001BE RID: 446
	public struct SoundSomeEffectCurve
	{
		// Token: 0x06000AFF RID: 2815 RVA: 0x00036F4B File Offset: 0x0003514B
		public SoundSomeEffectCurve(SoundSomeEffectCurve.Interpolation {16135}, float {16136}, float {16137}, float {16138})
		{
			this.{16140} = {16135};
			this.{16141} = {16136};
			this.{16142} = {16137};
			this.{16143} = {16138};
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00036F6C File Offset: 0x0003516C
		internal float GetValue(float {16139})
		{
			if (this.{16140} != SoundSomeEffectCurve.Interpolation.Linear)
			{
				throw new NotSupportedException();
			}
			if ({16139} >= 0.5f)
			{
				return this.{16142} * (1f - {16139}) * 2f + this.{16143} * ({16139} - 0.5f) * 2f;
			}
			return this.{16141} * (0.5f - {16139}) * 2f + this.{16142} * {16139} * 2f;
		}

		// Token: 0x040008A3 RID: 2211
		private SoundSomeEffectCurve.Interpolation {16140};

		// Token: 0x040008A4 RID: 2212
		private float {16141};

		// Token: 0x040008A5 RID: 2213
		private float {16142};

		// Token: 0x040008A6 RID: 2214
		private float {16143};

		// Token: 0x020001BF RID: 447
		public enum Interpolation
		{
			// Token: 0x040008A8 RID: 2216
			Linear
		}
	}
}
