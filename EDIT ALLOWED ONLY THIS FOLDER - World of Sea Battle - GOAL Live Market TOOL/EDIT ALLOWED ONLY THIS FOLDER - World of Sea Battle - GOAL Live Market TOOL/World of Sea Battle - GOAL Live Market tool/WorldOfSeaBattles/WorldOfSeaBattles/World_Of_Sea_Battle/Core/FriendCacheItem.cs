using System;

namespace World_Of_Sea_Battle.Core
{
	// Token: 0x020004E4 RID: 1252
	internal sealed class FriendCacheItem
	{
		// Token: 0x06001BCC RID: 7116 RVA: 0x000FF997 File Offset: 0x000FDB97
		public FriendCacheItem(uint {25141}, string {25142}, bool {25143})
		{
			this.AccountSID = {25141};
			this.Name = {25142};
			this.IsOnline = {25143};
		}

		// Token: 0x04001A82 RID: 6786
		public readonly uint AccountSID;

		// Token: 0x04001A83 RID: 6787
		public readonly string Name;

		// Token: 0x04001A84 RID: 6788
		public bool IsOnline;
	}
}
