using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020003F6 RID: 1014
	public static class {22827}
	{
		// Token: 0x06001618 RID: 5656 RVA: 0x000B95E8 File Offset: 0x000B77E8
		public static void Open()
		{
			if (Session.Account.WorldMapAdvertActiveCooldownSec > 0f)
			{
				new {17312}(Local.mapAdvert_cancel, delegate()
				{
					Session.Account.WorldMapAdvertCooldownSec = GlobalMapOnlineEvent.PlayerAdvertCooldownSec;
					Global.Network.Send(new OnMakeWorldMapAdvert(0, 0f));
				}, delegate()
				{
				});
				return;
			}
			if (Session.Account.WorldMapAdvertCooldownSec > 0f)
			{
				return;
			}
			string[] textOptions = (from {22828} in GlobalMapOnlineEvent.PlayerAdvertOptions
			where {22828}.allowPick
			select {22828}).Select(delegate(GlobalMapOnlineEvent.PlayerAdvert {22829})
			{
				if (!{22829}.pvpMode || !Session.Account.WorldFlag.IsPeaceMode())
				{
					return {22829}.text();
				}
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
				defaultInterpolatedStringHandler.AppendFormatted({22829}.text());
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(Environment.NewLine);
				defaultInterpolatedStringHandler.AppendLiteral("(");
				defaultInterpolatedStringHandler.AppendFormatted(Local.unavailable_pve);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}).ToArray<string>();
			new {17107}(Local.WorldMapUi_advert, Local.mapAdvert_tt1, Local.mapAdvert_tt2, delegate(int {22830})
			{
				if ({22830} == textOptions.Length)
				{
					return;
				}
				if (textOptions[{22830}].Contains(Local.unavailable_pve))
				{
					{22827}.Open();
					return;
				}
				Session.Account.WorldMapAdvertCooldownSec = GlobalMapOnlineEvent.PlayerAdvertCooldownSec + GlobalMapOnlineEvent.PlayerAdvertDurationSec;
				Global.Network.Send(new OnMakeWorldMapAdvert((byte){22830}, Session.Account.WorldMapAdvertCooldownSec));
			}, true, null, textOptions.Concat(new string[]
			{
				Local.close
			}).ToArray<string>());
		}
	}
}
