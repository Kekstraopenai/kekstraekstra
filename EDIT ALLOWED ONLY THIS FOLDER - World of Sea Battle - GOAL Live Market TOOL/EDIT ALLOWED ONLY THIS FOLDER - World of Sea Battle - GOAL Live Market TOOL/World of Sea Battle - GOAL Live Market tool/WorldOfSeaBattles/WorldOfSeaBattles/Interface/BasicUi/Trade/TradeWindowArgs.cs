using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Data;
using Common.Game;
using Common.Packets;
using Common.Resources;
using TheraEngine;
using WorldOfSeaBattles.Interface.PortPagesUi.Trade;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Interface;

namespace WorldOfSeaBattles.Interface.BasicUi.Trade
{
	// Token: 0x020005AF RID: 1455
	internal class TradeWindowArgs
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x0012C168 File Offset: 0x0012A368
		public string itemsSourceName
		{
			get
			{
				if (this.type == TradeType.TradeBetweenPlayers)
				{
					return Local.hold;
				}
				if (this.type == TradeType.Holding)
				{
					return Local.TradePortInterface_4_all;
				}
				if (this.type == TradeType.Cannons)
				{
					return Local.TradePortInterface_2b;
				}
				if (this.type == TradeType.Ship)
				{
					return Local.TradePortInterface_2;
				}
				if (this.type != TradeType.Booking)
				{
					return Local.TradePortInterface_4;
				}
				return "";
			}
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x0012C1C8 File Offset: 0x0012A3C8
		public int minPrice(bool {26511})
		{
			TradeType tradeType = this.type;
			if (tradeType == TradeType.Holding || tradeType == TradeType.TradeBetweenPlayers)
			{
				return 0;
			}
			if (!{26511})
			{
				return 200;
			}
			return 10;
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x0012C1FC File Offset: 0x0012A3FC
		public bool IsMouseInputAllowed(RTIf {26512}, RTI {26513}, int {26514}, IStorageAsset {26515})
		{
			if (this.type == TradeType.Holding)
			{
				return (float){26513}.Value * {26512}.Value * WosbTrading.GetTradingTax(Global.Player.NearPort, {26515}, true, false) > 0f;
			}
			return {26512}.Value * (float){26513}.Value >= (float){26514};
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x0012C255 File Offset: 0x0012A455
		public int ComputePlusToNeededGold(RTIf {26516}, RTI {26517})
		{
			if (this.type != TradeType.Booking)
			{
				return 0;
			}
			return (int)MathF.Ceiling({26516}.Value * (float){26517}.Value);
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0012C278 File Offset: 0x0012A478
		public int ComputePlusToNeededGold(RTI {26518}, RTI {26519})
		{
			if (this.type != TradeType.Booking)
			{
				return 0;
			}
			return {26518}.Value * {26519}.Value;
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x0012C294 File Offset: 0x0012A494
		public float TaxHelper(IStorageAsset {26520}, bool {26521})
		{
			if (this.type == TradeType.TradeBetweenPlayers)
			{
				return WosbTrading.TradingInSeaBetweenPlayersTax();
			}
			return WosbTrading.GetTradingTax(Global.Player.NearPort, {26520}, this.type == TradeType.Holding, {26521});
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x0012C2C0 File Offset: 0x0012A4C0
		public static TradeWindowArgs CreateSellShip(Action<TradeOrderCommonPartial> {26522}, IEnumerable<TradeOrderCommon> {26523})
		{
			return new TradeWindowArgs
			{
				title = Local.TradePortInterface_1,
				taxName = Local.TradePort_fees,
				type = TradeType.Ship,
				forceSelectItem = new ProceduralShipInfo(Global.Player.UsedShipPlayer),
				MyOrdersInCurrentPort = {26523},
				Complete = {26522},
				PricePerOneName = Local.TradePortInterface_11,
				ShouldHaveQuantityBar = false,
				GetDescription = Local.TradePortInterface_6,
				Items = Enumerable.Empty<ValueTuple<IStorageAsset, GenericTradeWindow.ItemFlags>>()
			};
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x0012C340 File Offset: 0x0012A540
		public static TradeWindowArgs CreateSellCannons(Action<TradeOrderCommonPartial> {26524}, IEnumerable<TradeOrderCommon> {26525})
		{
			TradeWindowArgs tradeWindowArgs = new TradeWindowArgs();
			tradeWindowArgs.title = Local.TradePortInterface_1;
			tradeWindowArgs.taxName = Local.TradePort_fees;
			tradeWindowArgs.type = TradeType.Cannons;
			tradeWindowArgs.MyOrdersInCurrentPort = {26525};
			tradeWindowArgs.Complete = {26524};
			tradeWindowArgs.Items = from {26534} in Session.Account.CannonsAtStorage.CannonGameInfo
			select new ValueTuple<CannonGameInfo, GenericTradeWindow.ItemFlags>({26534}.Info, WosbTrading.AllowTradeOrderWith({26534}.Info, Global.Player.NearPort, Session.Game.NearPortAllowBondmanTrade) ? GenericTradeWindow.ItemFlags.Allow : GenericTradeWindow.ItemFlags.Disallow) into {26535}
			select new ValueTuple<IStorageAsset, GenericTradeWindow.ItemFlags>({26535}.Item1, {26535}.Item2);
			return tradeWindowArgs;
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x0012C3E0 File Offset: 0x0012A5E0
		public static TradeWindowArgs CreateSellResource(Action<TradeOrderCommonPartial> {26526}, IEnumerable<TradeOrderCommon> {26527})
		{
			TradeWindowArgs tradeWindowArgs = new TradeWindowArgs();
			tradeWindowArgs.title = Local.TradePortInterface_1;
			tradeWindowArgs.taxName = Local.TradePort_fees;
			tradeWindowArgs.type = TradeType.Other;
			tradeWindowArgs.MyOrdersInCurrentPort = {26527};
			tradeWindowArgs.Complete = {26526};
			tradeWindowArgs.Items = TradeWindowArgs.GetResources(tradeWindowArgs.type);
			return tradeWindowArgs;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x0012C430 File Offset: 0x0012A630
		public static TradeWindowArgs CreateBooking(Action<TradeOrderCommonPartial> {26528}, IEnumerable<TradeOrderCommon> {26529})
		{
			TradeWindowArgs tradeWindowArgs = new TradeWindowArgs();
			tradeWindowArgs.title = Local.TradePortInterface_0;
			tradeWindowArgs.taxName = Local.TradePort_fees;
			tradeWindowArgs.type = TradeType.Booking;
			tradeWindowArgs.MyOrdersInCurrentPort = {26529};
			tradeWindowArgs.Complete = {26528};
			tradeWindowArgs.Items = TradeWindowArgs.GetResources(tradeWindowArgs.type);
			tradeWindowArgs.ShouldModesBeEquals = true;
			tradeWindowArgs.GetDescription = Local.TradePortInterface_7;
			tradeWindowArgs.GetContinueButtonText = Local.TradePortInterface_9;
			return tradeWindowArgs;
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x0012C49C File Offset: 0x0012A69C
		public static TradeWindowArgs CreateHolding(Action<TradeOrderCommonPartial> {26530}, IEnumerable<TradeOrderCommon> {26531})
		{
			TradeWindowArgs tradeWindowArgs = new TradeWindowArgs();
			tradeWindowArgs.title = Local.TradePortInterface_1_b;
			tradeWindowArgs.taxName = Local.TradePort_fees_hold;
			tradeWindowArgs.type = TradeType.Holding;
			tradeWindowArgs.MyOrdersInCurrentPort = {26531};
			tradeWindowArgs.Complete = {26530};
			tradeWindowArgs.Items = TradeWindowArgs.GetResources(tradeWindowArgs.type);
			tradeWindowArgs.ShouldHaveCostBar = false;
			tradeWindowArgs.GetDescription = Local.TradePortInterface_holdTt(WosbTrading.TradeOrderLifetimeHolding);
			return tradeWindowArgs;
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x0012C508 File Offset: 0x0012A708
		public static TradeWindowArgs CreateTradeBetweenPlayersInSea(int {26532})
		{
			TradeWindowArgs tradeWindowArgs = new TradeWindowArgs();
			tradeWindowArgs.title = Local.TradePortInterface_1;
			tradeWindowArgs.taxName = Local.tradeBetweenPlayers_query_tax;
			tradeWindowArgs.type = TradeType.TradeBetweenPlayers;
			tradeWindowArgs.MyOrdersInCurrentPort = Enumerable.Empty<TradeOrderCommon>();
			tradeWindowArgs.Complete = delegate(TradeOrderCommonPartial {26537})
			{
				if ({26537}.CurrentCount > 0)
				{
					Global.Network.Send(new OnTradeBetweenPlayers(OnTradeBetweenPlayers.Flag.AskTarget, new TradeOrderBetweenPlayers(Global.Player.uID, {26532}, new GSI().Exs((int){26537}.ItemInfo.ID, {26537}.CurrentCount), (int){26537}.Price.CostPerUnit * {26537}.CurrentCount)));
					{19994}.Me({19988}.Info, Local.tradeBetweenPlayers_query_sent, Array.Empty<object>());
				}
			};
			tradeWindowArgs.Items = from {26536} in Global.Player.ResourcesOfHold.ResourceInfo
			select new ValueTuple<IStorageAsset, GenericTradeWindow.ItemFlags>({26536}.Info, {26536}.Info.CannotBeMovedBetweenPlayers ? GenericTradeWindow.ItemFlags.Disallow : GenericTradeWindow.ItemFlags.Allow);
			tradeWindowArgs.ShouldHaveCostBar = true;
			tradeWindowArgs.GetDescription = Local.TradePortInterface_holdTt(WosbTrading.TradeOrderLifetimeHolding);
			return tradeWindowArgs;
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x0012C5BB File Offset: 0x0012A7BB
		private static IEnumerable<ValueTuple<IStorageAsset, GenericTradeWindow.ItemFlags>> GetResources(TradeType {26533})
		{
			TradeWindowArgs.<GetResources>d__26 <GetResources>d__ = new TradeWindowArgs.<GetResources>d__26(-2);
			<GetResources>d__.<>3__type = {26533};
			return <GetResources>d__;
		}

		// Token: 0x0400208A RID: 8330
		public IEnumerable<ValueTuple<IStorageAsset, GenericTradeWindow.ItemFlags>> Items;

		// Token: 0x0400208B RID: 8331
		public IEnumerable<TradeOrderCommon> MyOrdersInCurrentPort;

		// Token: 0x0400208C RID: 8332
		public Action<TradeOrderCommonPartial> Complete;

		// Token: 0x0400208D RID: 8333
		public string title;

		// Token: 0x0400208E RID: 8334
		public string taxName;

		// Token: 0x0400208F RID: 8335
		public IStorageAsset forceSelectItem;

		// Token: 0x04002090 RID: 8336
		public TradeType type;

		// Token: 0x04002091 RID: 8337
		public bool ShouldModesBeEquals;

		// Token: 0x04002092 RID: 8338
		public string PricePerOneName = Local.TradePortInterface_12;

		// Token: 0x04002093 RID: 8339
		public bool ShouldHaveQuantityBar = true;

		// Token: 0x04002094 RID: 8340
		public bool ShouldHaveCostBar = true;

		// Token: 0x04002095 RID: 8341
		public string GetDescription = Local.TradePortInterface_8;

		// Token: 0x04002096 RID: 8342
		public string GetContinueButtonText = Local.TradePortInterface_10;
	}
}
