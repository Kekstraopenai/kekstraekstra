using System;
using Common.Resources;

namespace World_Of_Sea_Battle.Core
{
	// Token: 0x020004E8 RID: 1256
	internal sealed class PassingCacheItem
	{
		// Token: 0x06001BD7 RID: 7127 RVA: 0x000FFBAC File Offset: 0x000FDDAC
		public PassingCacheItem(MapForPassingInfo {25171}, int {25172}, PassingMapDiffCard[] {25173})
		{
			this.MapInfo = {25171};
			this.DiffLevel = {25172};
			this.DiffCards = {25173};
		}

		// Token: 0x04001A99 RID: 6809
		public int DiffLevel;

		// Token: 0x04001A9A RID: 6810
		public PassingMapDiffCard[] DiffCards;

		// Token: 0x04001A9B RID: 6811
		public MapForPassingInfo MapInfo;
	}
}
