using System;
using Common.Data;
using Common.Game;
using WorldOfSeaBattles.Interface.PortPagesUi.Trade;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace WorldOfSeaBattles.Interface.BasicUi.Trade
{
	// Token: 0x020005AE RID: 1454
	internal static class TradeTypesHelper
	{
		// Token: 0x0600217F RID: 8575 RVA: 0x0012C0EC File Offset: 0x0012A2EC
		public static int ComputeMaxCount(IStorageAsset {26506}, TradeType {26507})
		{
			if ({26507} == TradeType.Ship)
			{
				return 1;
			}
			if ({26507} == TradeType.Booking)
			{
				return (int)((float)Session.Account.Gold / WosbTrading.GetTradeMinAndMaxCost({26506}).X);
			}
			if (Global.Player.IsPortEntry)
			{
				return Session.Account.GetItemsCountInStorage({26506});
			}
			return Global.Player.UsedShipPlayer.GetItemsCountInHold({26506});
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x0012C144 File Offset: 0x0012A344
		public static string GetValueOfK(IStorageAsset {26508}, TradeType {26509})
		{
			if ({26509} == TradeType.Ship)
			{
				return string.Empty;
			}
			return StringHelper.GetValueOfK(TradeTypesHelper.ComputeMaxCount({26508}, {26509}));
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x0012C15C File Offset: 0x0012A35C
		public static bool NeedToExtendImages(TradeType {26510})
		{
			return {26510} != TradeType.Booking;
		}
	}
}
