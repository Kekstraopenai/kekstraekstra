using System;
using Common;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000288 RID: 648
	public class {20500}
	{
		// Token: 0x06000E43 RID: 3651 RVA: 0x00003100 File Offset: 0x00001300
		public void InitializeInterface()
		{
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00078F54 File Offset: 0x00077154
		public static void CheckGuildAnnouncment()
		{
			if (Session.Guild != null && Global.Settings.SavedGuildAnnouncmentVersion != Session.Guild.CurrentAnnouncment.Item2 && !string.IsNullOrEmpty(Session.Guild.CurrentAnnouncment.Item1))
			{
				{19994}.Invite(Local.guild_announcment, delegate
				{
					if (Session.Guild != null && {20364}.CurrentInstance == null)
					{
						new {20364}();
					}
				}, null, null, 10000f);
			}
		}
	}
}
