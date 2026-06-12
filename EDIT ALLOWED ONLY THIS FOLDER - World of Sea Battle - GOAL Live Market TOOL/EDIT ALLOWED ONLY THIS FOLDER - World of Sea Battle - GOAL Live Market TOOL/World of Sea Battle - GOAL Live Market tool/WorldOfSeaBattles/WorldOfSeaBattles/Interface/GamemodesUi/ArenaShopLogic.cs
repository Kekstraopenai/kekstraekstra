using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Packets;
using TheraEngine;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Interface;

namespace WorldOfSeaBattles.Interface.GamemodesUi
{
	// Token: 0x0200058C RID: 1420
	public class ArenaShopLogic
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x00126BE3 File Offset: 0x00124DE3
		private RTI CurrentRating
		{
			get
			{
				return Session.Account.ArenaRating;
			}
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x00126BF0 File Offset: 0x00124DF0
		public static int GetOfferAvaliableCount(TraderOffer {26336})
		{
			return Math.Min(({26336}.DesignId != -1 || {26336}.BuyCooldownDays > 0) ? 1 : int.MaxValue, Session.Account.ArenaCurrency.Value / {26336}.Price.Value);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x00126C40 File Offset: 0x00124E40
		public static double GetOfferCooldownSecs(TraderOffer {26337})
		{
			TraderOfferCooldown traderOfferCooldown;
			if (!Session.Account.ArenaShopCooldowns.TryFind((TraderOfferCooldown {26350}) => {26350}.Id == {26337}.BuyCooldownID, out traderOfferCooldown))
			{
				return 0.0;
			}
			return traderOfferCooldown.RemainSec;
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x00126C89 File Offset: 0x00124E89
		public bool IsAvailable(TraderOffer {26338}, int {26339})
		{
			return this.IsCategoryUnlocked({26339}) && this.CanAfford({26338}, 1);
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x00126CA0 File Offset: 0x00124EA0
		public bool IsCategoryUnlocked(int {26340})
		{
			bool result;
			switch ({26340})
			{
			case 1:
				result = true;
				break;
			case 2:
				result = (this.CurrentRating.Value >= 2500);
				break;
			case 3:
				result = (this.CurrentRating.Value >= 8000);
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x00126D00 File Offset: 0x00124F00
		public int GetCategoryRequirement(int {26341})
		{
			int result;
			switch ({26341})
			{
			case 1:
				result = 0;
				break;
			case 2:
				result = 2500;
				break;
			case 3:
				result = 8000;
				break;
			default:
				result = 0;
				break;
			}
			return result;
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x00126D3C File Offset: 0x00124F3C
		public void TryBuy(int {26342}, TraderOffer {26343}, Action {26344} = null)
		{
			if (!this.IsCategoryUnlocked({26342}))
			{
				return;
			}
			if ({26343}.BuyCooldownDays != 0 && Session.Account.ArenaShopCooldowns.Any((TraderOfferCooldown {26351}) => {26351}.Id == {26343}.BuyCooldownID))
			{
				return;
			}
			double num = ArenaShopLogic.GetOfferCooldownSecs({26343});
			int num2 = 86400;
			if (num == 0.0 && {26343}.BuyCooldownDays > 0)
			{
				num = (double)({26343}.BuyCooldownDays * num2);
			}
			if ({26343}.DesignId > 0 && {26343}.BuyCooldownDays == 0)
			{
				if (this.CanAfford({26343}, 1))
				{
					this.{26347}({26343}, 1);
					return;
				}
			}
			else
			{
				Func<int, string> {21866} = new Func<int, string>({26343}.GetName);
				string shop = Local.shop;
				Action<int> {21868} = delegate(int {26352})
				{
					if (this.CanAfford({26343}, {26352}))
					{
						this.{26347}({26343}, {26352});
					}
					Action complete = {26344};
					if (complete == null)
					{
						return;
					}
					complete();
				};
				int offerAvaliableCount = ArenaShopLogic.GetOfferAvaliableCount({26343});
				Func<int, string> {21870} = (int {26353}) => Local.arena_currency_minus({26343}.Price.Value * {26353});
				string {21873} = (num > 0.0) ? (" " + Local.arena_shop_offer_time_cooldown(StringHelper.TimeD(num))) : string.Empty;
				new {21838}({21866}, shop, {21868}, offerAvaliableCount, {21870}, null, null, {21873}, null);
			}
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x00126E9C File Offset: 0x0012509C
		public bool CanAfford(TraderOffer {26345}, int {26346} = 1)
		{
			return Session.Account.ArenaCurrency.Value >= {26345}.Price.Value * {26346};
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x00126ED0 File Offset: 0x001250D0
		private void {26347}(in TraderOffer {26348}, int {26349})
		{
			ValueTuple<string, int, int, int> valueTuple = {26348}.Apply(Session.Account, true, {26349}, null);
			string item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			int item3 = valueTuple.Item3;
			int item4 = valueTuple.Item4;
			LogbookController logbook = Global.Settings.Logbook;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
			defaultInterpolatedStringHandler.AppendLiteral("+");
			defaultInterpolatedStringHandler.AppendFormatted<int>(item2);
			defaultInterpolatedStringHandler.AppendLiteral(" ");
			defaultInterpolatedStringHandler.AppendFormatted(item);
			defaultInterpolatedStringHandler.AppendLiteral(" (");
			defaultInterpolatedStringHandler.AppendFormatted(Local.arena_shop);
			defaultInterpolatedStringHandler.AppendLiteral(")");
			logbook.Write(defaultInterpolatedStringHandler.ToStringAndClear(), LBFlags.L1);
			if (item3 > 0)
			{
				Global.Network.NetClient.Send(new OnPremiumAddMsg(OnPremiumAddMsg.Subscription.PremiumDays, item3, 0));
			}
			if (item4 > 0)
			{
				Global.Network.NetClient.Send(new OnPremiumAddMsg(OnPremiumAddMsg.Subscription.LegendaryFlagDays, item4, 0));
			}
			if (item3 > 0 || item4 > 0)
			{
				Global.Game.ScenePort.MakeAccSync();
			}
			Global.Network.NetClient.Send(new OnAnalyticsEventClient(OnAnalyticsEventClient.Type.BuyArenaOrPirateShop, item, item2));
		}

		// Token: 0x04001FF4 RID: 8180
		private const int IntermediateThreshold = 2500;

		// Token: 0x04001FF5 RID: 8181
		private const int AdvancedThreshold = 8000;
	}
}
