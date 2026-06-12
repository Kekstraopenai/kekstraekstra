using System;

namespace World_Of_Sea_Battle.Core
{
	// Token: 0x020004E3 RID: 1251
	internal class RenderStatistics
	{
		// Token: 0x06001BCA RID: 7114 RVA: 0x000FF980 File Offset: 0x000FDB80
		public RenderStatistics()
		{
			this.ResetCounters();
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x000FF98E File Offset: 0x000FDB8E
		public void ResetCounters()
		{
			this.OceanParticleRenderCount = 0;
		}

		// Token: 0x04001A7C RID: 6780
		public int OceanParticleRenderCount;

		// Token: 0x04001A7D RID: 6781
		public int IsleRenderCount;

		// Token: 0x04001A7E RID: 6782
		public int IsleAllCount;

		// Token: 0x04001A7F RID: 6783
		public int ShipRenderCount;

		// Token: 0x04001A80 RID: 6784
		public int ShipPlayersRenderCount;

		// Token: 0x04001A81 RID: 6785
		public int ShipAllCount;
	}
}
