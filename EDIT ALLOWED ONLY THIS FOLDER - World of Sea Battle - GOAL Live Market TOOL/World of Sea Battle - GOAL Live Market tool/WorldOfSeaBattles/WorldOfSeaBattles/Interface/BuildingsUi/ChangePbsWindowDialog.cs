using System;
using System.Linq;
using Common;
using Common.Game;
using Common.Packets;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Interface;

namespace WorldOfSeaBattles.Interface.BuildingsUi
{
	// Token: 0x0200059F RID: 1439
	internal class ChangePbsWindowDialog
	{
		// Token: 0x06002145 RID: 8517 RVA: 0x0012A3CC File Offset: 0x001285CC
		public ChangePbsWindowDialog()
		{
			int[] allowedHours = new int[]
			{
				0,
				1,
				8,
				9,
				10,
				11,
				12,
				13,
				14,
				15,
				16,
				17,
				18,
				19,
				20,
				21,
				22,
				23
			};
			new {17107}(Local.GuidFortManagingUi_wind1, Local.change_window_tt(Session.Guild.GetEffectDurationDays(GuildTemporaryEffect.Type.BlockChangeWindowAgain, Session.EventActionsPipeline), WosbPbs.ChageAttackWindowPrice.Value), Local.ChangeAttackWindowDesc, (from {26441} in allowedHours
			select new PbsAttackWindow({26441})).ToArray<PbsAttackWindow>(), delegate(int {26442})
			{
				if (Session.Guild.ConquerBadges < WosbPbs.ChageAttackWindowPrice.Value)
				{
					new {17312}(Local.conquer_badges_not_enough);
					return;
				}
				PbsAttackWindow window = new PbsAttackWindow(allowedHours[{26442}]);
				new {17312}(Local.GuidFortManagingUi_wind1q(window.ToStringFull(null, false)), delegate()
				{
					Global.Network.Send(new OnChangeAttackWindowMsg(window, false));
				}, delegate()
				{
				});
			});
		}
	}
}
