using System;
using System.Runtime.CompilerServices;

namespace TheraEngine.Grphics.Device
{
	// Token: 0x020000FA RID: 250
	public class DeviceStatistics
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x000243E4 File Offset: 0x000225E4
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x000243EC File Offset: 0x000225EC
		public int CountUnselectedTexture { get; internal set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x000243F5 File Offset: 0x000225F5
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x000243FD File Offset: 0x000225FD
		public int CountRender2DCycle { get; internal set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00024406 File Offset: 0x00022606
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0002440E File Offset: 0x0002260E
		public int CountRender2DMethod { get; internal set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00024417 File Offset: 0x00022617
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x0002441F File Offset: 0x0002261F
		public int CountSetRenderTargetMethod { get; internal set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00024428 File Offset: 0x00022628
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x00024430 File Offset: 0x00022630
		public int VerticesRenderedCount { get; internal set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00024439 File Offset: 0x00022639
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x00024441 File Offset: 0x00022641
		public int Drawcalls { get; internal set; }

		// Token: 0x06000717 RID: 1815 RVA: 0x0002444A File Offset: 0x0002264A
		internal void Reset()
		{
			this.CountUnselectedTexture = 0;
			this.CountRender2DCycle = 0;
			this.CountRender2DMethod = 0;
			this.CountSetRenderTargetMethod = 0;
			this.VerticesRenderedCount = 0;
			this.Drawcalls = 0;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00024478 File Offset: 0x00022678
		public override bool Equals(object {14726})
		{
			if ({14726} == null)
			{
				return false;
			}
			if ({14726}.GetType() != base.GetType())
			{
				return false;
			}
			DeviceStatistics deviceStatistics = (DeviceStatistics){14726};
			return deviceStatistics.CountRender2DCycle == this.CountRender2DCycle && deviceStatistics.CountRender2DMethod == this.CountRender2DMethod && deviceStatistics.CountSetRenderTargetMethod == this.CountSetRenderTargetMethod && deviceStatistics.Drawcalls == this.Drawcalls && deviceStatistics.CountUnselectedTexture == this.CountUnselectedTexture && deviceStatistics.VerticesRenderedCount == this.VerticesRenderedCount;
		}

		// Token: 0x04000503 RID: 1283
		[CompilerGenerated]
		private int {14727};

		// Token: 0x04000504 RID: 1284
		[CompilerGenerated]
		private int {14728};

		// Token: 0x04000505 RID: 1285
		[CompilerGenerated]
		private int {14729};

		// Token: 0x04000506 RID: 1286
		[CompilerGenerated]
		private int {14730};

		// Token: 0x04000507 RID: 1287
		[CompilerGenerated]
		private int {14731};

		// Token: 0x04000508 RID: 1288
		[CompilerGenerated]
		private int {14732};
	}
}
