using System;

namespace World_Of_Sea_Battle.Core
{
	// Token: 0x020004E9 RID: 1257
	internal class ApplyingEffectCache
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x000FFBC9 File Offset: 0x000FDDC9
		public bool IsLargeEffect
		{
			get
			{
				return this.InitialTimeMs > 4000f;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x000FFBD8 File Offset: 0x000FDDD8
		public float ProgressFactor
		{
			get
			{
				return (this.InitialTimeMs - this.RemainTimeMs) / this.InitialTimeMs;
			}
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000FFBEE File Offset: 0x000FDDEE
		public ApplyingEffectCache(string {25178}, float {25179}, Action {25180} = null, Func<bool> {25181} = null)
		{
			this.Text = {25178};
			this.InitialTimeMs = {25179};
			this.RemainTimeMs = {25179};
			this.onComplete = {25180};
			this.checkIsAvailable = {25181};
		}

		// Token: 0x04001A9C RID: 6812
		public string Text;

		// Token: 0x04001A9D RID: 6813
		public float InitialTimeMs;

		// Token: 0x04001A9E RID: 6814
		public float RemainTimeMs;

		// Token: 0x04001A9F RID: 6815
		public bool CanBeStopped;

		// Token: 0x04001AA0 RID: 6816
		public Func<bool> checkIsAvailable;

		// Token: 0x04001AA1 RID: 6817
		public Action onComplete;
	}
}
