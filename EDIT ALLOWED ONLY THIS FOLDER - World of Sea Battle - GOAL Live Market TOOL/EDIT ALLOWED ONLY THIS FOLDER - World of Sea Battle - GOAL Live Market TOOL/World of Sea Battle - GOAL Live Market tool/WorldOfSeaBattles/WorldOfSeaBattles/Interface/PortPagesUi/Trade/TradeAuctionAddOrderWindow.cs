using System;
using System.Collections.Generic;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using WorldOfSeaBattles.Interface.BasicUi.Trade;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Interface;

namespace WorldOfSeaBattles.Interface.PortPagesUi.Trade
{
	// Token: 0x02000589 RID: 1417
	internal sealed class TradeAuctionAddOrderWindow
	{
		// Token: 0x060020D2 RID: 8402 RVA: 0x00126984 File Offset: 0x00124B84
		public TradeAuctionAddOrderWindow(TradeType {26329}, IEnumerable<TradeOrderCommon> {26330})
		{
			TradeAuctionAddOrderWindow <>4__this = this;
			this.{26334} = {26329};
			TradeWindowArgs tradeWindowArgs;
			if ({26329} != TradeType.Booking)
			{
				if ({26329} != TradeType.Ship)
				{
					if ({26329} != TradeType.Holding)
					{
						if ({26329} != TradeType.Other)
						{
							if ({26329} != TradeType.Cannons)
							{
								throw new NotSupportedException();
							}
							tradeWindowArgs = TradeWindowArgs.CreateSellCannons(new Action<TradeOrderCommonPartial>(this.{26331}), {26330});
						}
						else
						{
							tradeWindowArgs = TradeWindowArgs.CreateSellResource(new Action<TradeOrderCommonPartial>(this.{26331}), {26330});
						}
					}
					else
					{
						tradeWindowArgs = TradeWindowArgs.CreateHolding(new Action<TradeOrderCommonPartial>(this.{26331}), {26330});
					}
				}
				else
				{
					tradeWindowArgs = TradeWindowArgs.CreateSellShip(new Action<TradeOrderCommonPartial>(this.{26331}), {26330});
				}
			}
			else
			{
				tradeWindowArgs = TradeWindowArgs.CreateBooking(new Action<TradeOrderCommonPartial>(this.{26331}), {26330});
			}
			TradeWindowArgs {26460} = tradeWindowArgs;
			GenericTradeWindow window = new GenericTradeWindow({26460});
			window.CustomMiddleware = delegate()
			{
				<>4__this.{26335} = new CheckboxControl(window.Pos.XY + new Vector2(50f, 342f), CommonAtlas.vd_checkBox_26px_true, CommonAtlas.vd_checkBox_26px_false, false, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				<>4__this.{26335}.SetText(Local.Trade_allowSellPartial, Fonts.Arial_12, {17177}.textColor * 0.7f);
				window.AddChild(<>4__this.{26335});
				if ({26329} != TradeType.Other)
				{
					<>4__this.{26335}.IsVisible = false;
				}
			};
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x00126A70 File Offset: 0x00124C70
		private void {26331}(TradeOrderCommonPartial {26332})
		{
			TradeOrderCommon tradeOrderCommon = new TradeOrderCommon(0, 0U, {26332}.ItemInfo, {26332}.CurrentCount, {26332}.Price, this.{26333}(), Global.Player.NearPort.PortID);
			Exception ex;
			if (!TradeOrderCommon.CreationHelper(Session.Account, tradeOrderCommon, out ex))
			{
				throw new InvalidOperationException("OnTradePortPPCreateOrder: IOR-CLIENT Cache-attack (tcp delay)");
			}
			Global.Network.Send(new OnAuctionOrderAction(OnAuctionOrderAction.ActionType.Create, tradeOrderCommon, false));
			Session.AuctionSummaryTradePerSession += tradeOrderCommon.CostMarginConvertedToGold + tradeOrderCommon.CostTotalConvToGold;
			EducationHelper.MakeFlag(EducationOnboarding.AuctionCreateOrder, true);
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x00126B00 File Offset: 0x00124D00
		private TradeOrderMode {26333}()
		{
			TradeType tradeType = this.{26334};
			if (tradeType == TradeType.Holding)
			{
				return TradeOrderMode.Holding;
			}
			if (tradeType == TradeType.Booking)
			{
				return TradeOrderMode.Shop;
			}
			if (this.{26335} == null || !this.{26335}.IsChecked)
			{
				return TradeOrderMode.AllowSellPartial;
			}
			return TradeOrderMode.DisallowSellPartial;
		}

		// Token: 0x04001FE8 RID: 8168
		private readonly TradeType {26334};

		// Token: 0x04001FE9 RID: 8169
		private CheckboxControl {26335};
	}
}
