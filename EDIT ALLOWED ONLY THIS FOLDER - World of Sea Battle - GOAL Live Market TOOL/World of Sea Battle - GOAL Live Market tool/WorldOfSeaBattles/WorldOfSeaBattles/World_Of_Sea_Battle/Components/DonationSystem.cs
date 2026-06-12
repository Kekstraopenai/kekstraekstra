using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Data;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x0200053A RID: 1338
	internal class DonationSystem
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x0010EE90 File Offset: 0x0010D090
		public static bool ShowBuyCraftTimeToolTip
		{
			get
			{
				return (Session.Account.AvailCraftTime.Value < (float)(Session.Account.CraftTimeLimit / 3) && Session.Account.TradingSubscriptionSec == 0f) || Session.Account.AvailCraftTime.Value < (float)(Session.Account.CraftTimeLimit / 5);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x0010EEEC File Offset: 0x0010D0EC
		public static bool ShowPeacefulFlagToolTip
		{
			get
			{
				return Session.Account.PeaceTimeSec == 0.0 && !PlatformTuning.DisableShop;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x0010EF0D File Offset: 0x0010D10D
		public static bool SwitchStartPackets
		{
			get
			{
				return Session.Account.Rang >= 18;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x0010EF20 File Offset: 0x0010D120
		public static int[] QuickRefilPrices
		{
			get
			{
				return new int[]
				{
					500,
					1000,
					4000,
					10000
				};
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool MorePriceExperiment
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x000070D7 File Offset: 0x000052D7
		public static bool Disable1RangShipsExperiment
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool EnableFirstBuyOffer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x0010EF33 File Offset: 0x0010D133
		public static bool EnableBuyResToCraftShip
		{
			get
			{
				return !PlatformTuning.DisableShop && HashHelper.greaterBool((int)Session.Account.SID);
			}
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x0010EF50 File Offset: 0x0010D150
		[return: TupleElementNames(new string[]
		{
			"goldCost",
			"monetsCost",
			"resources"
		})]
		public static ValueTuple<RTI, RTI, GSI> GetDesignElementCost(ShipDesignInfo {25664})
		{
			if ({25664}.InShopByGold.Value == 0)
			{
				RTI item = DonationSystem.PriceByTear[{25664}.Category][{25664}.CostTier];
				if ({25664}.AssociatedShip != null && {25664}.AssociatedShip.Rank >= 5)
				{
					item = new RTI((int)((float)item.Value * 0.66f));
				}
				return new ValueTuple<RTI, RTI, GSI>(0, item, GSI.Empty);
			}
			if ({25664}.ID == 423)
			{
				return new ValueTuple<RTI, RTI, GSI>(0, 0, new GSI().Exs(70, {25664}.InShopByGold.Value / Gameplay.ItemsInfo.FromID(70).MediumCost.Value));
			}
			return new ValueTuple<RTI, RTI, GSI>({25664}.InShopByGold.Value, 0, GSI.Empty);
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x000329D4 File Offset: 0x00030BD4
		private static float WithoutDiscount()
		{
			return 1f;
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x0010F03D File Offset: 0x0010D23D
		public static float GetDiscount(EActionCaterory {25665})
		{
			return 1f - Session.EventActionsPipeline.EventCategoryAmount({25665}) + (DonationSystem.MorePriceExperiment ? 0.2f : 0f);
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0010F064 File Offset: 0x0010D264
		public static int GetRoundedPriceWithDiscount(int {25666}, float {25667})
		{
			return (int)Math.Ceiling((double)((float){25666} * {25667}));
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x0010F074 File Offset: 0x0010D274
		private static void OpenCrewChest(RTI {25668})
		{
			if (Session.Account.Monets.Value < {25668}.Value)
			{
				{20881}.ShowBuyMonetsToolTip({25668}.Value);
				return;
			}
			string[] {17139} = new string[]
			{
				Local.PortEquipUnitsFilterShipWindow_4,
				Local.PortEquipUnitsFilterShipWindow_3,
				Local.PortEquipUnitsFilterShipWindow_2,
				Local.PortEquipUnitsFilterShipWindow_5
			};
			new {17107}(Local.PortRealShopPage_crew_chest, Local.PortRealShopPage_crew_chest_desc, "", delegate(int {25770})
			{
				PlayerAccount account = Session.Account;
				account.Monets.Value = account.Monets.Value - {25668}.Value;
				Global.Network.Send(new OnOpenChestMsg(new byte[]
				{
					(byte){25770}
				}, DateTime.Now, OnOpenChestMsg.Flags.OpenCrewChest, Session.Account.Monets.Value, 0, 0, {25668}.Value, 0));
				DonationSystem.waitResponse = true;
			}, false, null, {17139});
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x0010F108 File Offset: 0x0010D308
		internal static void DefaultChestOpened(OnOpenChestMsg {25669})
		{
			if ({25669}.flags == OnOpenChestMsg.Flags.OpenCrewChest)
			{
				for (int i = 0; i < {25669}.effects.Length; i++)
				{
					SpecialUnitInstance specialUnitInstance = new SpecialUnitInstance((int){25669}.effects[i]);
					Session.Account.SpecialUnitsAtStorage.Add(specialUnitInstance);
					{19994}.MeAndLogbook({19988}.GiveCrew, Local.lbe_newunit(specialUnitInstance.GetInfo.Name), new LBFlags?(LBFlags.L1));
				}
				{19994}.MeAndLogbook({19988}.GiveDoubloons, Local.lbe_real_shop(Local.PortRealShopPage_crew_chest, Session.Account.Monets.Value), new LBFlags?(LBFlags.L2));
				Global.Shopstat("chest_crew", {25669}.statFinalPrice);
			}
			else
			{
				ValueTuple<string, string, ShopChestApplyHelper> valueTuple = DonationSystem.<DefaultChestOpened>g__PickChest|79_0({25669}.flags);
				string item = valueTuple.Item1;
				string item2 = valueTuple.Item2;
				ShopChestApplyHelper item3 = valueTuple.Item3;
				new {19197}({25669}, item3);
				if ({25669}.statUseKeys > 0)
				{
					{19994}.MeAndLogbook({19988}.GiveDoubloons, Local.PortRealShopPage_3_b + Session.Account.NearPortStorage[43].ToString(), new LBFlags?(LBFlags.L2));
				}
				else
				{
					{19994}.MeAndLogbook({19988}.GiveDoubloons, Local.lbe_real_shop(item, Session.Account.Monets.Value), new LBFlags?(LBFlags.L2));
					Global.Shopstat(item2, {25669}.statFinalPrice);
				}
			}
			DonationSystem.waitResponse = false;
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x0010F24C File Offset: 0x0010D44C
		private static void CreateCaravan(RTI {25670})
		{
			double tonnsOfHavingRes = Math.Ceiling((double)Global.Player.ResourcesOfHold.ComputeMass<ResourceInfo>() / 1000.0);
			Action<IslePortInfo, {18533}> <>9__2;
			new {17312}(Local.donate_caravan_step1(Global.Player.ResourcesOfHold.GetTotalItemsCount(), tonnsOfHavingRes), delegate()
			{
				if (tonnsOfHavingRes > 1000.0)
				{
					new {17312}(Local.donate_caravan_fail(1000));
					return;
				}
				if (Global.Player.ResourcesOfHold.IsEmpty)
				{
					new {17312}(Local.PortRealShopPage_141);
					return;
				}
				string donate_caravan_step = Local.donate_caravan_step2;
				Func<IslePortInfo, bool> {18538} = (IslePortInfo {25726}) => true;
				Action<IslePortInfo, {18533}> {18539};
				if (({18539} = <>9__2) == null)
				{
					{18539} = (<>9__2 = delegate(IslePortInfo {25771}, {18533} {25772})
					{
						{25772}.BlockAndClose();
						new {17312}(Local.PortRealShopPage_142(Global.Player.ResourcesOfHold.GetTotalItemsCount(), Math.Ceiling((double)Global.Player.ResourcesOfHold.ComputeMass<ResourceInfo>() / 1000.0), {25771}.PortName), delegate()
						{
							if (Session.Account.TemporaryFreeResTravel.Value > 0 || DonationSystem.DefaultProceed({25670}, Local.PortRealShopPage_143, "travel_res"))
							{
								if (Session.Account.TemporaryFreeResTravel.Value > 0)
								{
									PlayerAccount account = Session.Account;
									int value = account.TemporaryFreeResTravel.Value;
									account.TemporaryFreeResTravel.Value = value - 1;
								}
								GSI {5459} = Global.Player.ResourcesOfHold.Clone();
								Global.Player.ResourcesOfHold.Clean();
								Session.Account.ResourcesInPorts.GetHolder({25771}.PortID).Add({5459});
								{19994}.MeAndLogbook({19988}.Big_Yellow, Local.PortRealShopPage_144({25771}.PortName), new LBFlags?(LBFlags.L2));
							}
						}, false);
					});
				}
				new {18533}(donate_caravan_step, {18538}, {18539});
			}, false);
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x0010F2C4 File Offset: 0x0010D4C4
		internal static void OpenChest(OnOpenChestMsg.Flags {25671}, bool {25672}, RTI {25673})
		{
			if (DonationSystem.waitResponse)
			{
				return;
			}
			int num = 10;
			if ({25671} == OnOpenChestMsg.Flags.OpenSingleChest && {25672} && Session.Account.NearPortStorage[43] >= num)
			{
				Session.Account.NearPortStorage.AddOrRemove(43, -num);
				Global.Shopstat("chest_free", {25673}.Value);
				Global.Network.Send(new OnOpenChestMsg(new byte[0], DateTime.Now, OnOpenChestMsg.Flags.OpenSingleChest, Session.Account.Monets.Value, Session.Account.NearPortStorage[43], 0, 0, num));
				DonationSystem.waitResponse = true;
				return;
			}
			if (Session.Account.Monets.Value < {25673}.Value)
			{
				{20881}.ShowBuyMonetsToolTip({25673}.Value);
				return;
			}
			PlayerAccount account = Session.Account;
			account.Monets.Value = account.Monets.Value - {25673}.Value;
			Global.Network.Send(new OnOpenChestMsg(new byte[0], DateTime.Now, {25671}, Session.Account.Monets.Value, Session.Account.NearPortStorage[43], 0, {25673}.Value, 0));
			DonationSystem.waitResponse = true;
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x0010F3FA File Offset: 0x0010D5FA
		private static void MakePacket(RTI {25674}, string {25675}, Func<string> {25676})
		{
			if (DonationSystem.DefaultProceed({25674}, {25675}, null))
			{
				Global.Shopstat({25676}(), {25674}.Value);
			}
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0010F418 File Offset: 0x0010D618
		private static void AddRangsHelper(int {25677})
		{
			for (int i = 0; i < {25677}; i++)
			{
				Session.Account.AddXp(Session.Account.XpToNextRang + 1, false);
			}
			Global.Game.ScenePort.MakeAccSync();
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x0010F458 File Offset: 0x0010D658
		private static void AddShipsHelper(int? {25678}, int? {25679}, params int[] {25680})
		{
			foreach (int {3506} in {25680})
			{
				DonationSystem.<>c__DisplayClass84_0 CS$<>8__locals1 = new DonationSystem.<>c__DisplayClass84_0();
				PlayerShipInfo {2035} = Gameplay.PlayersInfo.FromID({3506});
				CS$<>8__locals1.createdShip = Session.Account.Shipyard.CreateNewShip({2035}, Session.Account, new int?({25678}.GetValueOrDefault(7)), true, true, true);
				Global.Game.ScenePort.ShipsHolder.Add(CS$<>8__locals1.createdShip);
				DonationSystem.<>c__DisplayClass84_0 CS$<>8__locals2 = CS$<>8__locals1;
				ShipBonusEffect[] array = new ShipBonusEffect[9];
				RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.EB5C302EC37FB1343DEAAA876DDBC2593F079034436E04AF047A14C899DA50A2).FieldHandle);
				CS$<>8__locals2.bestUpgrades = array;
				if ({25679} != null)
				{
					int num = 100;
					foreach (ShipBonusEffect shipBonusEffect in CS$<>8__locals1.bestUpgrades)
					{
						IEnumerable<ShipUpgradeInfo> shipUpgradesInfo = Gameplay.ShipUpgradesInfo;
						Func<ShipUpgradeInfo, bool> predicate;
						if ((predicate = CS$<>8__locals1.<>9__0) == null)
						{
							predicate = (CS$<>8__locals1.<>9__0 = delegate(ShipUpgradeInfo {25773})
							{
								ShipUpgradeInstance shipUpgradeInstance;
								return {25773}.Name != Local.removed && {25773}.CategoryUi != ShipUpgradeCategory.Sailes && {25773}.CategoryUi != ShipUpgradeCategory.Modification && CS$<>8__locals1.bestUpgrades.Contains({25773}.GetEffects(CS$<>8__locals1.createdShip.CraftFrom).First<ShipBonus>().Type) && !Session.Account.Shipyard.StoredUpgrades.TrySearch((byte)CS$<>8__locals1.createdShip.CraftFrom.ID, (ShipUpgradeInstance {25774}) => {25773}.ID == (short){25774}.ID, out shipUpgradeInstance);
							});
						}
						ShipUpgradeInfo shipUpgradeInfo = shipUpgradesInfo.FirstOrDefault(predicate);
						if (shipUpgradeInfo != null)
						{
							Session.Account.Shipyard.AddUpgrade(new ShipUpgradeInstance((byte)shipUpgradeInfo.ID, (float)shipUpgradeInfo.WearAmount.Value), CS$<>8__locals1.createdShip);
							int? num3;
							int? num2 = num3 = {25679};
							{25679} = num3 - 1;
							int? num4 = num2;
							int num5 = 0;
							if (num4.GetValueOrDefault() == num5 & num4 != null)
							{
								break;
							}
						}
						if (num-- == 0)
						{
							break;
						}
					}
				}
				if (Session.Account.ShipPlacesLeft <= 0)
				{
					PlayerAccount account = Session.Account;
					int j = account.MaxShipsCount.Value;
					account.MaxShipsCount.Value = j + 1;
				}
			}
			Global.Game.ScenePort.mainHandler(null);
			new {19197}(Local.ship_was_crafted_1_basic);
			if (Global.Player.IsPortEntry && Global.Player.NearPortType == PortEnteringType.Port)
			{
				Global.Game.ScenePort.ShipsHolder.See(Session.Account.Shipyard.List[Session.Account.Shipyard.List.Count - 1]);
			}
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x0010F674 File Offset: 0x0010D874
		private static bool DefaultProceed(RTI {25681}, string {25682}, string {25683})
		{
			if ({25681}.Value > Session.Account.Monets.Value)
			{
				{20881}.ShowBuyMonetsToolTip({25681}.Value);
				return false;
			}
			PlayerAccount account = Session.Account;
			account.Monets.Value = account.Monets.Value - {25681}.Value;
			if ({25683} != null)
			{
				Global.Shopstat({25683}, {25681}.Value);
			}
			{19994}.MeAndLogbook({19988}.GiveDoubloons, Local.lbe_real_shop({25682}, Session.Account.Monets.Value), new LBFlags?(LBFlags.L2));
			return true;
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x0010F6FC File Offset: 0x0010D8FC
		public static void BuyPlaceInPort(bool {25684})
		{
			if (Session.Account.MaxShipsCount.Value >= Session.Account.HardShipPlacesLimit)
			{
				new {17312}(Local.hard_ship_places_limit(Session.Account.HardShipPlacesLimit));
				return;
			}
			if ({25684})
			{
				if (DonationSystem.placeInPortCost_r.Value > Session.Account.Monets.Value)
				{
					{20881}.ShowBuyMonetsToolTip(DonationSystem.placeInPortCost_r.Value);
					return;
				}
				PlayerAccount account = Session.Account;
				account.Monets.Value = account.Monets.Value - DonationSystem.placeInPortCost_r.Value;
				{19994}.MeAndLogbook({19988}.GiveDoubloons, Local.lbe_real_shop(Local.PortСraftShipWindow_16b, Session.Account.Monets.Value), new LBFlags?(LBFlags.L2));
				Global.Shopstat("place_in_port", DonationSystem.placeInPortCost_r.Value);
			}
			else
			{
				int num = Gameplay.PlaceForShipCostVSkull(Session.Account);
				if (num > Session.Account.NearPortStorage.GetCount(38))
				{
					new {17312}(Local.PortRealShopPage_16);
					return;
				}
				Session.Account.NearPortStorage.AddOrRemove(38, -num);
				{19994}.MeAndLogbook({19988}.GiveDoubloons, Local.Current("lbe_real_shop_vudu", new object[]
				{
					num
				}), new LBFlags?(LBFlags.L2));
			}
			PlayerAccount account2 = Session.Account;
			account2.MaxShipsCount.Value = account2.MaxShipsCount.Value + 1;
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x0010F858 File Offset: 0x0010DA58
		internal static DonationSystem.PacketOffer GetPacketIdWithShip(int {25685})
		{
			PlayerShipInfo shipInfo = Gameplay.PlayersInfo.FromID({25685});
			return DonationSystem.Packets.FirstOrDefault((DonationSystem.PacketOffer {25775}) => {25775}.InnerText() != Local.chest_26_name && {25775}.InnerText() != Local.chest_ny && ({25775}.InnerText().Contains(shipInfo.ShipName) || {25775}.InnerText2().Contains(shipInfo.ShipName)));
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x0010F894 File Offset: 0x0010DA94
		internal static void CheckForShipsFBO()
		{
			if (!PlatformTuning.DisableShop && {18593}.CurrentInstance == null && DonationSystem.EnableFirstBuyOffer && Session.Account.Rang >= 9 && Session.Account.Shipyard.List.Count <= 2)
			{
				UiControl uiControl = new {17312}(Local.fbo_with_ships_text, delegate(int {25727})
				{
					if ({25727} == 0)
					{
						Global.Network.Send(new OnAnalyticsEventClient(OnAnalyticsEventClient.Type.OpenPortPage, "FBO-ships", 1));
						Global.Game.ScenePort.realShopHandler(null, DonationSystem.Packets.FirstOrDefault((DonationSystem.PacketOffer {25728}) => {25728}.InnerText() == Local.PortRealShopPage_111));
					}
				}, new string[]
				{
					Local.to_show,
					Local.close
				});
				Rectangle rectangle = new Rectangle(2341, 649, 215, 74);
				uiControl.AddChildPos(new Image(new Marker(0f, 0f, (float)rectangle.Width, (float)rectangle.Height).Scale(0.8f), AtlasPortGui.Texture.Tex, rectangle, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 270f);
			}
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x0010F98C File Offset: 0x0010DB8C
		private static ToolTipState ChestToolTipHelper(string {25686}, ChestRewardInfo[] {25687})
		{
			Composer composer = new Composer(300f, -1f);
			if (!string.IsNullOrEmpty({25686}))
			{
				composer.AddText({25686}, ComposerTextStyle.Wheat, false);
			}
			foreach (DonationSystem.GroupedReward groupedReward in DonationSystem.GroupAndNormalizeRewards({25687}))
			{
				ComposerTextStyle composerTextStyle;
				switch (groupedReward.Tier)
				{
				case 1:
					composerTextStyle = new ComposerTextStyle(Color.LightYellow * 0.8f, false, null, null);
					break;
				case 2:
					composerTextStyle = new ComposerTextStyle(Color.SkyBlue, false, null, null);
					break;
				case 3:
					composerTextStyle = new ComposerTextStyle(Color.Violet, false, null, null);
					break;
				default:
					composerTextStyle = new ComposerTextStyle(Color.LightYellow * 0.8f, false, null, null);
					break;
				}
				ComposerTextStyle {12529} = composerTextStyle;
				string str = string.Empty;
				if (PlatformTuning.EnableLootboxesChanseTooltip)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted<float>(groupedReward.TotalWeight);
					defaultInterpolatedStringHandler.AppendLiteral("%");
					str = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				composer.AddText("• " + groupedReward.RewardCategory + str, {12529}, false);
			}
			foreach (ChestRewardInfo chestRewardInfo in {25687})
			{
				if (chestRewardInfo.AssuranceOpen != null && chestRewardInfo.AbleToReceiveAndDisplay(Session.Account))
				{
					int item = chestRewardInfo.AssuranceOpen.Value.Item2;
					int num = Session.Account.ChestStat[chestRewardInfo.AssuranceOpen.Value.Item1];
					int value = item - num + 1;
					if (num > 0)
					{
						Composer composer2 = composer;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(4, 4);
						defaultInterpolatedStringHandler2.AppendFormatted("♕");
						defaultInterpolatedStringHandler2.AppendLiteral(" ");
						defaultInterpolatedStringHandler2.AppendFormatted<LocalizedString>(chestRewardInfo.OverrideChatMessage ?? chestRewardInfo.Complete(null, 0, DateTime.Now));
						defaultInterpolatedStringHandler2.AppendLiteral(": ");
						defaultInterpolatedStringHandler2.AppendFormatted<int>(value);
						defaultInterpolatedStringHandler2.AppendLiteral(" ");
						defaultInterpolatedStringHandler2.AppendFormatted(Local.assurance_chest);
						composer2.AddText(defaultInterpolatedStringHandler2.ToStringAndClear(), new ComposerTextStyle(Color.Violet * 0.5f, false, Fonts.Arial_10, null), false);
					}
				}
			}
			composer.AddSeparatorWosb("⚓");
			composer.AddText(Local.PortRealShopPage_Buy, ComposerTextStyle.LimeBold, true);
			return new ToolTipState(composer);
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x0010FC64 File Offset: 0x0010DE64
		private static List<DonationSystem.GroupedReward> GroupAndNormalizeRewards(ChestRewardInfo[] {25688})
		{
			List<DonationSystem.GroupedReward> list = (from {25733} in (from {25729} in {25688}
			group {25729} by {25729}.RewardCategory).Select(delegate(IGrouping<string, ChestRewardInfo> {25730})
			{
				float num3 = {25730}.Sum((ChestRewardInfo {25731}) => {25731}.Weight);
				double num4;
				if (num3 > 10f)
				{
					num4 = Math.Round((double)num3);
				}
				else if (num3 > 1f)
				{
					num4 = Math.Round((double)num3, 1);
				}
				else
				{
					num4 = Math.Round((double)num3, 2);
				}
				return new DonationSystem.GroupedReward({25730}.Key, (float)num4, {25730}.Max((ChestRewardInfo {25732}) => {25732}.Tier));
			})
			orderby {25733}.Tier descending
			select {25733}).ThenBy((DonationSystem.GroupedReward {25734}) => {25734}.TotalWeight).ToList<DonationSystem.GroupedReward>();
			float num = list.Sum((DonationSystem.GroupedReward {25735}) => {25735}.TotalWeight);
			double num2 = Math.Round(100.0 - (double)num, 2);
			if (Math.Abs(num2) > 0.001 && list.Count > 0)
			{
				List<DonationSystem.GroupedReward> list2 = list;
				DonationSystem.GroupedReward groupedReward = list2[list2.Count - 1];
				groupedReward.TotalWeight = (float)Math.Round((double)groupedReward.TotalWeight + num2, 2);
			}
			return list;
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x0010FD80 File Offset: 0x0010DF80
		public static void AskForBuyPlaceInPort()
		{
			if (PlatformTuning.DisableShop)
			{
				int num = Gameplay.PlaceForShipCostVSkull(Session.Account);
				IStorageAsset resource = Gameplay.GetResource(38, StorageAssetEnum.SimpleItem);
				bool {17457} = num > Session.Account.NearPortStorage[38];
				string tradePortInterface_51_noshop = Local.TradePortInterface_51_noshop;
				Action<int> {17386} = delegate(int {25736})
				{
					if ({25736} == 0)
					{
						DonationSystem.BuyPlaceInPort(false);
					}
				};
				{17443}[] array = new {17443}[2];
				int num2 = 0;
				string shop = Local.shop;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
				defaultInterpolatedStringHandler.AppendLiteral("-");
				defaultInterpolatedStringHandler.AppendFormatted<int>(num);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(resource.getName);
				array[num2] = new {17443}(shop, defaultInterpolatedStringHandler.ToStringAndClear(), {17312}.cIconPlus, {17457}, 0f);
				array[1] = new {17443}(Local.no, "", {17312}.cIconReject, false, 0f);
				new {17312}(tradePortInterface_51_noshop, {17386}, array);
				return;
			}
			new {17312}(Local.TradePortInterface_51, delegate(int {25737})
			{
				if ({25737} == 0)
				{
					Global.Game.ScenePort.realShopHandler(null, null);
				}
			}, new string[]
			{
				Local.PortEquipDesignsShipWindow_9,
				Local.close
			});
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x0010FEAC File Offset: 0x0010E0AC
		// Note: this type is marked as 'beforefieldinit'.
		static DonationSystem()
		{
			Dictionary<ShipDesignCategory, Dictionary<DesignElementCostTier, RTI>> dictionary = new Dictionary<ShipDesignCategory, Dictionary<DesignElementCostTier, RTI>>();
			ShipDesignCategory key = ShipDesignCategory.ShipFullDesign;
			Dictionary<DesignElementCostTier, RTI> dictionary2 = new Dictionary<DesignElementCostTier, RTI>();
			dictionary2[DesignElementCostTier.Tier1] = 390;
			dictionary2[DesignElementCostTier.Tier2] = 490;
			dictionary2[DesignElementCostTier.Tier3] = 690;
			dictionary2[DesignElementCostTier.Tier4] = 990;
			dictionary2[DesignElementCostTier.Tier5] = 1290;
			dictionary2[DesignElementCostTier.TierU] = 9779;
			dictionary[key] = dictionary2;
			ShipDesignCategory key2 = ShipDesignCategory.SailTexture;
			Dictionary<DesignElementCostTier, RTI> dictionary3 = new Dictionary<DesignElementCostTier, RTI>();
			dictionary3[DesignElementCostTier.Tier1] = 137;
			dictionary3[DesignElementCostTier.Tier2] = 370;
			dictionary3[DesignElementCostTier.Tier3] = 590;
			dictionary[key2] = dictionary3;
			ShipDesignCategory key3 = ShipDesignCategory.Decal1;
			Dictionary<DesignElementCostTier, RTI> dictionary4 = new Dictionary<DesignElementCostTier, RTI>();
			dictionary4[DesignElementCostTier.Tier1] = 137;
			dictionary4[DesignElementCostTier.Tier2] = 370;
			dictionary4[DesignElementCostTier.Tier3] = 590;
			dictionary4[DesignElementCostTier.Tier4] = 900;
			dictionary[key3] = dictionary4;
			ShipDesignCategory key4 = ShipDesignCategory.Decal2;
			Dictionary<DesignElementCostTier, RTI> dictionary5 = new Dictionary<DesignElementCostTier, RTI>();
			dictionary5[DesignElementCostTier.Tier1] = 137;
			dictionary5[DesignElementCostTier.Tier2] = 370;
			dictionary5[DesignElementCostTier.Tier3] = 590;
			dictionary5[DesignElementCostTier.Tier4] = 900;
			dictionary[key4] = dictionary5;
			ShipDesignCategory key5 = ShipDesignCategory.Flag;
			Dictionary<DesignElementCostTier, RTI> dictionary6 = new Dictionary<DesignElementCostTier, RTI>();
			dictionary6[DesignElementCostTier.Tier1] = 77;
			dictionary6[DesignElementCostTier.Tier2] = 170;
			dictionary[key5] = dictionary6;
			ShipDesignCategory key6 = ShipDesignCategory.Satellite;
			Dictionary<DesignElementCostTier, RTI> dictionary7 = new Dictionary<DesignElementCostTier, RTI>();
			dictionary7[DesignElementCostTier.Tier1] = 259;
			dictionary7[DesignElementCostTier.Tier2] = 370;
			dictionary7[DesignElementCostTier.Tier3] = 579;
			dictionary7[DesignElementCostTier.Tier4] = 757;
			dictionary[key6] = dictionary7;
			ShipDesignCategory key7 = ShipDesignCategory.BowFigure;
			Dictionary<DesignElementCostTier, RTI> dictionary8 = new Dictionary<DesignElementCostTier, RTI>();
			dictionary8[DesignElementCostTier.Tier1] = 127;
			dictionary8[DesignElementCostTier.Tier2] = 290;
			dictionary8[DesignElementCostTier.Tier3] = 370;
			dictionary[key7] = dictionary8;
			DonationSystem.PriceByTear = dictionary;
			DonationSystem.peaceContractDurationHours = 4;
			DonationSystem.tradingContractDurationDays = 7;
			DonationSystem.holdProtectionFurationMinutes = 60;
			DonationSystem.PremiumPagePackets = new DonationSystem.PacketOffer[]
			{
				new DonationSystem.PacketOffer(DonationSystem.peaceCost, new Func<float>(DonationSystem.WithoutDiscount), () => Local.PortRealShopPage_127, () => Local.PortRealShopPage_128(DonationSystem.peaceContractDurationHours.Value), "", delegate(DonationSystem.Offer {25738})
				{
					if (DonationSystem.DefaultProceed({25738}.GetFinalPrice(), Local.PortRealShopPage_127, "pf"))
					{
						int num = DonationSystem.peaceContractDurationHours.Value * 3600;
						Global.Network.Send(new OnPremiumAddMsg(OnPremiumAddMsg.Subscription.PeaceSec, num, DonationSystem.peaceCost.Value));
						Session.Account.PeaceTimeSec += (double)num;
					}
				}, () => !Global.Player.IsPortEntry || Global.Player.NearPortType != PortEnteringType.PersonalIsle, null),
				new DonationSystem.PacketOffer(DonationSystem.factoriesAndCraftBoostCost, new Func<float>(DonationSystem.WithoutDiscount), () => Local.PortRealShopPage_131, () => Local.PortRealShopPage_132(DonationSystem.tradingContractDurationDays.Value), "", delegate(DonationSystem.Offer {25739})
				{
					if (DonationSystem.DefaultProceed({25739}.GetFinalPrice(), Local.PortRealShopPage_131, "trading_subsc"))
					{
						int num = DonationSystem.tradingContractDurationDays.Value * 24 * 3600;
						Global.Network.Send(new OnPremiumAddMsg(OnPremiumAddMsg.Subscription.TradingSec, num, DonationSystem.factoriesAndCraftBoostCost.Value));
						Session.Account.TradingSubscriptionSec += (float)num;
						Session.Account.AvailCraftTime.Value = Math.Max(Session.Account.AvailCraftTime.Value, (float)Session.Account.CraftTimeLimit);
					}
				}, () => !Global.Player.IsPortEntry || Global.Player.NearPortType != PortEnteringType.PersonalIsle, null),
				new DonationSystem.PacketOffer(0, new Func<float>(DonationSystem.WithoutDiscount), () => Local.PortRealShopPage_135, () => Local.PortRealShopPage_136(DonationSystem.holdProtectionFurationMinutes.Value), "", delegate(DonationSystem.Offer {25740})
				{
					if (Session.Account.PassingMapScrolls < DonationSystem.HoldSubscriptionPriceScrolls.Value)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
						defaultInterpolatedStringHandler.AppendFormatted(Local.scrolls_need);
						defaultInterpolatedStringHandler.AppendLiteral(": ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(DonationSystem.HoldSubscriptionPriceScrolls.Value);
						defaultInterpolatedStringHandler.AppendLiteral("!");
						new {17312}(defaultInterpolatedStringHandler.ToStringAndClear());
						return;
					}
					int num = DonationSystem.holdProtectionFurationMinutes.Value * 60;
					Global.Network.Send(new OnPremiumAddMsg(OnPremiumAddMsg.Subscription.HoldProtectionSec, num, 0));
					Session.Account.HoldProtectionSubscriptionSec += (float)num;
					Session.Account.HoldProtectionBlockBuySec = (float)(num * 3);
					GSI treasuryMaps = Session.Account.TreasuryMaps;
					treasuryMaps[35] = treasuryMaps[35] - DonationSystem.HoldSubscriptionPriceScrolls.Value;
					{19988} {20000} = {19988}.Okay;
					object portRealShopPage_ = Local.PortRealShopPage_135;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(2, 2);
					defaultInterpolatedStringHandler2.AppendFormatted<int>(DonationSystem.HoldSubscriptionPriceScrolls.Value);
					defaultInterpolatedStringHandler2.AppendLiteral("x ");
					defaultInterpolatedStringHandler2.AppendFormatted(Local.res_35_name);
					{19994}.MeAndLogbook({20000}, Local.lbe_bought_general(portRealShopPage_, defaultInterpolatedStringHandler2.ToStringAndClear()), null);
				}, () => !Global.Player.IsPortEntry || Global.Player.NearPortType != PortEnteringType.PersonalIsle, null)
			};
			DonationSystem.HoldSubscriptionPriceScrolls = 40;
			DonationSystem.c_30k = new RTI(30000);
			DonationSystem.c_20k = new RTI(20000);
			DonationSystem.c_50 = new RTI(50);
			DonationSystem.c_15k = new RTI(15000);
			DonationSystem.c_50k = new RTI(50000);
			DonationSystem.c_10 = new RTI(10);
			DonationSystem.c_20 = new RTI(20);
			DonationSystem.c_250 = new RTI(250);
			DonationSystem.c_300 = new RTI(300);
			DonationSystem.c_2500 = new RTI(2500);
			DonationSystem.c_5 = new RTI(5);
			DonationSystem.c_7 = new RTI(7);
			DonationSystem.c_10000 = new RTI(10000);
			DonationSystem.c_1000 = new RTI(1000);
			DonationSystem.c_2000 = new RTI(2000);
			DonationSystem.c_4000 = new RTI(4000);
			DonationSystem.c_100 = new RTI(100);
			DonationSystem.c_450k = new RTI(450000);
			DonationSystem.MaxRanWhenPacketWithRankWorks = new RTI(19);
			DonationSystem.Packets = new DonationSystem.PacketOffer[]
			{
				new DonationSystem.PacketOffer(DonationSystem.chestCost, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopChest), () => Local.PortRealShopPage_101q, () => Local.PortRealShopPage_102, "", delegate(DonationSystem.Offer {25741})
				{
					DonationSystem.OpenChest(OnOpenChestMsg.Flags.OpenSingleChest, false, {25741}.GetFinalPrice());
				}, () => PlatformTuning.EnableLootboxes, () => DonationSystem.ChestToolTipHelper(Local.chest_following_reward, ShopChestApplyHelper.BigChest.Elements)),
				new DonationSystem.PacketOffer(DonationSystem.chestCostBig, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopChest), () => Local.PortRealShopPage_103, () => Local.PortRealShopPage_104, "", delegate(DonationSystem.Offer {25742})
				{
					DonationSystem.OpenChest(OnOpenChestMsg.Flags.OpenTripleChest, false, {25742}.GetFinalPrice());
				}, () => PlatformTuning.EnableLootboxes, () => DonationSystem.ChestToolTipHelper(Local.PortRealShopPage_104, new ChestRewardInfo[0])),
				new DonationSystem.PacketOffer(DonationSystem.c_null, new Func<float>(DonationSystem.WithoutDiscount), () => Local.PortRealShopPage_105, () => Local.PortRealShopPage_106, "", delegate(DonationSystem.Offer {25743})
				{
				}, null, null),
				new DonationSystem.PacketOffer(DonationSystem.c_980, new Func<float>(DonationSystem.WithoutDiscount), () => Local.war_marks_pack, () => Local.PortRealShopPage_118, "", delegate(DonationSystem.Offer {25744})
				{
					DonationSystem.MakePacket({25744}.GetFinalPrice(), Local.war_marks_pack, delegate
					{
						Session.Account.NearPortStorage.AddOrRemove(37, DonationSystem.c_2000.Value);
						return "packet_marks_big";
					});
				}, null, null),
				new DonationSystem.PacketOffer(DonationSystem.c_1290, new Func<float>(DonationSystem.WithoutDiscount), () => Local.PortRealShopPage_cannonskit, () => Local.PortRealShopPage_cannonskit_d, "", delegate(DonationSystem.Offer {25745})
				{
					DonationSystem.MakePacket({25745}.GetFinalPrice(), Local.PortRealShopPage_cannonskit, delegate
					{
						GSI cannonsAtStorage = Session.Account.CannonsAtStorage;
						cannonsAtStorage[16] = cannonsAtStorage[16] + 40;
						Session.Account.Gold += DonationSystem.c_30k.Value;
						Session.Account.Shipyard.AddXpAllShips(DonationSystem.c_20k.Value, true);
						return "packet_weap";
					});
				}, null, null),
				new DonationSystem.PacketOffer(DonationSystem.c_1390, new Func<float>(DonationSystem.WithoutDiscount), () => Local.PortRealShopPage_109, () => Local.PortRealShopPage_110, "", delegate(DonationSystem.Offer {25746})
				{
					DonationSystem.MakePacket({25746}.GetFinalPrice(), Local.PortRealShopPage_109, delegate
					{
						Session.Account.Gold += DonationSystem.c_30k.Value;
						GSI treasuryMaps = Session.Account.TreasuryMaps;
						treasuryMaps[35] = treasuryMaps[35] + DonationSystem.c_50.Value;
						DonationSystem.AddShipsHelper(new int?(2), null, new int[]
						{
							5
						});
						DonationSystem.AddShipsHelper(new int?(2), null, new int[]
						{
							10
						});
						DonationSystem.AddShipsHelper(new int?(11), null, new int[]
						{
							19
						});
						DonationSystem.AddShipsHelper(new int?(2), null, new int[]
						{
							27
						});
						if (Session.Account.Rang <= DonationSystem.MaxRanWhenPacketWithRankWorks.Value)
						{
							DonationSystem.AddRangsHelper(5);
						}
						return "packet: 5 ranks, le cerf";
					});
				}, null, null)
				{
					OverrideToolTipText = Local.PortRealShop_packetTt_ranks(DonationSystem.MaxRanWhenPacketWithRankWorks.Value)
				},
				new DonationSystem.PacketOffer(DonationSystem.c_1970, new Func<float>(DonationSystem.WithoutDiscount), () => Local.PortRealShopPage_115, () => Local.PortRealShopPage_1001, "", delegate(DonationSystem.Offer {25747})
				{
					DonationSystem.MakePacket({25747}.GetFinalPrice(), Local.PortRealShopPage_115, delegate
					{
						DonationSystem.AddShipsHelper(new int?(20), new int?(7), new int[]
						{
							44
						});
						Session.Account.Gold += DonationSystem.c_30k.Value;
						return "packet_hms_bel";
					});
				}, null, null),
				new DonationSystem.PacketOffer(DonationSystem.c_490, new Func<float>(DonationSystem.WithoutDiscount), () => Local.PortRealShopPage_111, () => Local.PortRealShopPage_112, "", delegate(DonationSystem.Offer {25748})
				{
					DonationSystem.MakePacket({25748}.GetFinalPrice(), Local.PortRealShopPage_111, delegate
					{
						Session.Account.NearPortStorage.AddOrRemove(37, DonationSystem.c_100.Value);
						Session.Account.Gold += DonationSystem.c_15k.Value;
						DonationSystem.AddShipsHelper(null, null, new int[]
						{
							12
						});
						if (Session.Account.Rang <= DonationSystem.MaxRanWhenPacketWithRankWorks.Value)
						{
							DonationSystem.AddRangsHelper(5);
						}
						return "packet: 5 ranks, black prince";
					});
				}, null, null)
				{
					OverrideToolTipText = Local.PortRealShop_packetTt_ranks(DonationSystem.MaxRanWhenPacketWithRankWorks.Value)
				},
				new DonationSystem.PacketOffer(DonationSystem.c_270, new Func<float>(DonationSystem.WithoutDiscount), () => Local.PortRealShopPage_113, () => Local.PortRealShopPage_114, "", delegate(DonationSystem.Offer {25749})
				{
					DonationSystem.MakePacket({25749}.GetFinalPrice(), Local.PortRealShopPage_113, delegate
					{
						Session.Account.Gold += DonationSystem.c_15k.Value;
						Session.Account.CBallsAtStorage.AddOrRemove(6, DonationSystem.c_50.Value);
						DonationSystem.AddShipsHelper(null, null, new int[]
						{
							16
						});
						if (Session.Account.Rang <= DonationSystem.MaxRanWhenPacketWithRankWorks.Value)
						{
							DonationSystem.AddRangsHelper(4);
						}
						return "packet: 4 ranks, golden ap";
					});
				}, null, null)
				{
					OverrideToolTipText = Local.PortRealShop_packetTt_ranks(DonationSystem.MaxRanWhenPacketWithRankWorks.Value)
				},
				new DonationSystem.PacketOffer(DonationSystem.c_2490, new Func<float>(DonationSystem.WithoutDiscount), () => Local.PortRealShopPage_1000, () => Local.PortRealShopPage_1002, "", delegate(DonationSystem.Offer {25750})
				{
					DonationSystem.MakePacket({25750}.GetFinalPrice(), Local.PortRealShopPage_1000, delegate
					{
						DonationSystem.AddShipsHelper(new int?(31), new int?(7), new int[]
						{
							31
						});
						Session.Account.Gold += DonationSystem.c_30k.Value;
						return "packet_hms_vic";
					});
				}, null, null),
				new DonationSystem.PacketOffer(DonationSystem.c_190, new Func<float>(DonationSystem.WithoutDiscount), () => Local.PortRealShopPage_115, () => Local.PortRealShopPage_116, "", delegate(DonationSystem.Offer {25751})
				{
					DonationSystem.MakePacket({25751}.GetFinalPrice(), Local.PortRealShopPage_115, delegate
					{
						Session.Account.Gold += DonationSystem.c_15k.Value;
						DonationSystem.AddShipsHelper(null, null, new int[]
						{
							8
						});
						if (Session.Account.Rang <= DonationSystem.MaxRanWhenPacketWithRankWorks.Value)
						{
							DonationSystem.AddRangsHelper(3);
						}
						return "packet: 3 ranks, phoenix";
					});
				}, null, null)
				{
					OverrideToolTipText = Local.PortRealShop_packetTt_ranks(DonationSystem.MaxRanWhenPacketWithRankWorks.Value)
				},
				new DonationSystem.PacketOffer(DonationSystem.c_257, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems), () => Local.PortRealShopPage_117, () => "", "", delegate(DonationSystem.Offer {25752})
				{
					DonationSystem.MakePacket({25752}.GetFinalPrice(), Local.PortRealShopPage_117, delegate
					{
						Session.Account.NearPortStorage.AddOrRemove(37, DonationSystem.c_300.Value);
						Session.Account.NearPortStorage.AddOrRemove(38, DonationSystem.c_20.Value);
						return "packet_vudu_marks_small";
					});
				}, null, null),
				new DonationSystem.PacketOffer(DonationSystem.c_127, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems), delegate()
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 3);
					defaultInterpolatedStringHandler.AppendLiteral("10x ");
					defaultInterpolatedStringHandler.AppendFormatted(Gameplay.PowerupItems.Array[31].Name);
					defaultInterpolatedStringHandler.AppendLiteral(", 10x ");
					defaultInterpolatedStringHandler.AppendFormatted(Gameplay.PowerupItems.Array[30].Name);
					defaultInterpolatedStringHandler.AppendLiteral(", 10x ");
					defaultInterpolatedStringHandler.AppendFormatted(Gameplay.PowerupItems.Array[16].Name);
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}, () => "", "", delegate(DonationSystem.Offer {25753})
				{
					RTI finalPrice = {25753}.GetFinalPrice();
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 3);
					defaultInterpolatedStringHandler.AppendLiteral("10x ");
					defaultInterpolatedStringHandler.AppendFormatted(Gameplay.PowerupItems.Array[31].Name);
					defaultInterpolatedStringHandler.AppendLiteral(", 10x ");
					defaultInterpolatedStringHandler.AppendFormatted(Gameplay.PowerupItems.Array[30].Name);
					defaultInterpolatedStringHandler.AppendLiteral(", 10x ");
					defaultInterpolatedStringHandler.AppendFormatted(Gameplay.PowerupItems.Array[16].Name);
					DonationSystem.MakePacket(finalPrice, defaultInterpolatedStringHandler.ToStringAndClear(), delegate
					{
						Session.Account.PowerupItemsAtStorage.AddOrRemove(31, DonationSystem.c_10.Value);
						Session.Account.PowerupItemsAtStorage.AddOrRemove(30, DonationSystem.c_10.Value);
						Session.Account.PowerupItemsAtStorage.AddOrRemove(16, DonationSystem.c_10.Value);
						return "packet_PowerupItem";
					});
				}, null, null),
				new DonationSystem.PacketOffer(DonationSystem.c_95, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems), () => Local.PortRealShopPage_120, () => "", "", delegate(DonationSystem.Offer {25754})
				{
					DonationSystem.MakePacket({25754}.GetFinalPrice(), Local.PortRealShopPage_120, delegate
					{
						GSI treasuryMaps = Session.Account.TreasuryMaps;
						treasuryMaps[35] = treasuryMaps[35] + DonationSystem.c_100.Value;
						return "packet_scrolls";
					});
				}, null, null),
				new DonationSystem.PacketOffer(DonationSystem.c_95, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems), () => Local.PortRealShopPage_121, () => "", "", delegate(DonationSystem.Offer {25755})
				{
					DonationSystem.MakePacket({25755}.GetFinalPrice(), Local.PortRealShopPage_121, delegate
					{
						Session.Account.CBallsAtStorage.AddOrRemove(7, DonationSystem.c_2000.Value);
						Session.Account.CBallsAtStorage.AddOrRemove(16, DonationSystem.c_50.Value);
						Session.Account.PowderKegsAtStorage.AddOrRemove(5, DonationSystem.c_10.Value);
						return "packet_ammo";
					});
				}, null, null),
				new DonationSystem.PacketOffer(DonationSystem.c_55, new Func<float>(DonationSystem.WithoutDiscount), () => Local.PortRealShopPage_122, () => "", "", delegate(DonationSystem.Offer {25756})
				{
					DonationSystem.MakePacket({25756}.GetFinalPrice(), Local.PortRealShopPage_122, delegate
					{
						Session.Account.NearPortStorage.AddOrRemove(14, DonationSystem.c_5.Value);
						return "packet_bronze";
					});
				}, () => false, null),
				new DonationSystem.PacketOffer(DonationSystem.c_270, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems), () => Local.PortRealShopPage_123, () => "", "", delegate(DonationSystem.Offer {25757})
				{
					DonationSystem.MakePacket({25757}.GetFinalPrice(), Local.PortRealShopPage_123, delegate
					{
						Session.Account.NearPortStorage.AddOrRemove(4, DonationSystem.c_10000.Value);
						return "packet_iron";
					});
				}, () => false, null),
				new DonationSystem.PacketOffer(DonationSystem.c_245, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems), () => Local.PortRealShopPage_124, () => "", "", delegate(DonationSystem.Offer {25758})
				{
					DonationSystem.MakePacket({25758}.GetFinalPrice(), Local.PortRealShopPage_124, delegate
					{
						Session.Account.Gold += DonationSystem.c_50k.Value;
						return "packet_gold_small";
					});
				}, null, null),
				new DonationSystem.PacketOffer(DonationSystem.c_1590, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems), () => Local.PortRealShopPage_125, () => "", "", delegate(DonationSystem.Offer {25759})
				{
					DonationSystem.MakePacket({25759}.GetFinalPrice(), Local.PortRealShopPage_125, delegate
					{
						Session.Account.Gold += DonationSystem.c_450k.Value;
						return "packet_gold_big";
					});
				}, null, null),
				new DonationSystem.PacketOffer(DonationSystem.c_97, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems), delegate()
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 3);
					defaultInterpolatedStringHandler.AppendLiteral("10x ");
					defaultInterpolatedStringHandler.AppendFormatted(Gameplay.PowerupItems.Array[15].Name);
					defaultInterpolatedStringHandler.AppendLiteral(", 10x ");
					defaultInterpolatedStringHandler.AppendFormatted(Gameplay.PowerupItems.Array[14].Name);
					defaultInterpolatedStringHandler.AppendLiteral(", 10x ");
					defaultInterpolatedStringHandler.AppendFormatted(Gameplay.PowerupItems.Array[2].Name);
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}, () => "", "", delegate(DonationSystem.Offer {25760})
				{
					RTI finalPrice = {25760}.GetFinalPrice();
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 3);
					defaultInterpolatedStringHandler.AppendLiteral("10x ");
					defaultInterpolatedStringHandler.AppendFormatted(Gameplay.PowerupItems.Array[15].Name);
					defaultInterpolatedStringHandler.AppendLiteral(", 10x ");
					defaultInterpolatedStringHandler.AppendFormatted(Gameplay.PowerupItems.Array[14].Name);
					defaultInterpolatedStringHandler.AppendLiteral(", 10x ");
					defaultInterpolatedStringHandler.AppendFormatted(Gameplay.PowerupItems.Array[2].Name);
					DonationSystem.MakePacket(finalPrice, defaultInterpolatedStringHandler.ToStringAndClear(), delegate
					{
						Session.Account.PowerupItemsAtStorage.AddOrRemove(15, DonationSystem.c_10.Value);
						Session.Account.PowerupItemsAtStorage.AddOrRemove(14, DonationSystem.c_10.Value);
						Session.Account.PowerupItemsAtStorage.AddOrRemove(2, DonationSystem.c_10.Value);
						return "packet_PowerupItems_3";
					});
				}, null, null),
				new DonationSystem.PacketOffer(DonationSystem.c_490, new Func<float>(DonationSystem.WithoutDiscount), () => Local.PortRealShopPage_137, () => "", "", delegate(DonationSystem.Offer {25761})
				{
					DonationSystem.CreateCaravan({25761}.GetFinalPrice());
				}, () => Global.Player.IsPortEntry && Global.Player.NearPortType == PortEnteringType.PersonalIsle, null),
				new DonationSystem.PacketOffer(DonationSystem.c_490, new Func<float>(DonationSystem.WithoutDiscount), () => Local.PortRealShopPage_138, () => "", "", delegate(DonationSystem.Offer {25762})
				{
					DonationSystem.CreateCaravan({25762}.GetFinalPrice());
				}, () => !Global.Player.IsPortEntry || Global.Player.NearPortType != PortEnteringType.PersonalIsle, null),
				new DonationSystem.PacketOffer(DonationSystem.chestCrew, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems), () => Local.PortRealShopPage_crew_chest, () => "", "", delegate(DonationSystem.Offer {25763})
				{
					DonationSystem.OpenCrewChest({25763}.GetFinalPrice());
				}, () => PlatformTuning.EnableLootboxes, null),
				new DonationSystem.PacketOffer(2740, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems), () => Local.items_pack(Gameplay.ItemsInfo.FromID(67).Name, 140), () => "", "", delegate(DonationSystem.Offer {25764})
				{
					DonationSystem.MakePacket({25764}.GetFinalPrice(), Local.items_pack(Gameplay.ItemsInfo.FromID(67).Name, 140), delegate
					{
						Session.Account.NearPortStorage.AddOrRemove(67, 140);
						return "packet_drafts";
					});
				}, () => Session.Account.Rang > 10, null),
				new DonationSystem.PacketOffer(1890, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems), () => Local.items_pack(Gameplay.ItemsInfo.FromID(15).Name, 600), () => "", "", delegate(DonationSystem.Offer {25765})
				{
					DonationSystem.MakePacket({25765}.GetFinalPrice(), Local.items_pack(Gameplay.ItemsInfo.FromID(15).Name, 600), delegate
					{
						Session.Account.NearPortStorage.AddOrRemove(15, 600);
						return "packet_escudo";
					});
				}, () => Session.Account.Rang > 10, null),
				new DonationSystem.PacketOffer(DonationSystem.chestCostEmp, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopChest), () => Local.chest_new25, () => Local.chest_new25_desc, "", delegate(DonationSystem.Offer {25766})
				{
					DonationSystem.OpenChest(OnOpenChestMsg.Flags.OpenEmpireChest, false, {25766}.GetFinalPrice());
				}, () => PlatformTuning.EnableLootboxes, () => DonationSystem.ChestToolTipHelper(Local.chest_following_reward, ShopChestApplyHelper.EmpireChest.Elements)),
				new DonationSystem.PacketOffer(135, () => 1f, () => Local.chest_26_name, () => Local.chest_26_desc, "", delegate(DonationSystem.Offer {25767})
				{
					DonationSystem.OpenChest(OnOpenChestMsg.Flags.OpenIncasTreasureChest, false, {25767}.GetFinalPrice());
				}, () => PlatformTuning.EnableLootboxes, () => DonationSystem.ChestToolTipHelper(Local.chest_following_reward, ShopChestApplyHelper.IncasTreasureChest.Elements)),
				new DonationSystem.PacketOffer(290, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems), () => Local.items_pack(Gameplay.ItemsInfo.FromID(69).Name, 55), () => "", "", delegate(DonationSystem.Offer {25768})
				{
					DonationSystem.MakePacket({25768}.GetFinalPrice(), Local.items_pack(Gameplay.ItemsInfo.FromID(69).Name, 55), delegate
					{
						Session.Account.NearPortStorage.AddOrRemove(69, DonationSystem.c_55.Value);
						return "packet_goldfish";
					});
				}, () => CalendarEvents.HasTrader && Session.Account.Rang >= 7, null),
				new DonationSystem.PacketOffer(197, () => 0.82f, () => Local.chest_ny, () => Local.chest_ny_desc, "", delegate(DonationSystem.Offer {25769})
				{
					DonationSystem.OpenChest(OnOpenChestMsg.Flags.OpenNewYearChest, false, {25769}.GetFinalPrice());
				}, () => PlatformTuning.EnableLootboxes && CalendarEvents.CurrentEvent.Type == CalendarEvent.NewYear && CalendarEvents.IsActiveExtended, () => DonationSystem.ChestToolTipHelper(Local.chest_following_reward, ShopChestApplyHelper.NewYearChest.Elements))
			};
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x0011107C File Offset: 0x0010F27C
		[CompilerGenerated]
		[return: TupleElementNames(new string[]
		{
			"name",
			"analyticsId",
			"source"
		})]
		internal static ValueTuple<string, string, ShopChestApplyHelper> <DefaultChestOpened>g__PickChest|79_0(OnOpenChestMsg.Flags {25689})
		{
			switch ({25689})
			{
			case OnOpenChestMsg.Flags.OpenSingleChest:
				return new ValueTuple<string, string, ShopChestApplyHelper>(Local.PortRealShopPage_101q, "chest", ShopChestApplyHelper.BigChest);
			case OnOpenChestMsg.Flags.OpenTripleChest:
				return new ValueTuple<string, string, ShopChestApplyHelper>(Local.PortRealShopPage_103, "chest_x3", ShopChestApplyHelper.BigChest);
			case OnOpenChestMsg.Flags.OpenEmpireChest:
				return new ValueTuple<string, string, ShopChestApplyHelper>(Local.chest_new25, "chest_emp", ShopChestApplyHelper.EmpireChest);
			case OnOpenChestMsg.Flags.OpenIncasTreasureChest:
				return new ValueTuple<string, string, ShopChestApplyHelper>(Local.chest_26_name, "chest_incas", ShopChestApplyHelper.IncasTreasureChest);
			case OnOpenChestMsg.Flags.OpenNewYearChest:
				return new ValueTuple<string, string, ShopChestApplyHelper>(Local.chest_ny, "chest_ny", ShopChestApplyHelper.NewYearChest);
			}
			throw new ArgumentException("Invalid chest flag");
		}

		// Token: 0x04001DBD RID: 7613
		private static readonly RTI chestCost = new RTI(88);

		// Token: 0x04001DBE RID: 7614
		private static readonly RTI chestCostEmp = new RTI(99);

		// Token: 0x04001DBF RID: 7615
		private static readonly RTI chestCostBig = new RTI(247);

		// Token: 0x04001DC0 RID: 7616
		private static readonly RTI chestCrew = new RTI(345);

		// Token: 0x04001DC1 RID: 7617
		private static readonly RTI placeInPortCost_r = new RTI(75);

		// Token: 0x04001DC2 RID: 7618
		private static readonly RTI peaceCost = new RTI(127);

		// Token: 0x04001DC3 RID: 7619
		private static readonly RTI factoriesAndCraftBoostCost = new RTI(297);

		// Token: 0x04001DC4 RID: 7620
		private static readonly RTI c_245 = new RTI(245);

		// Token: 0x04001DC5 RID: 7621
		private static readonly RTI c_2490 = new RTI(2490);

		// Token: 0x04001DC6 RID: 7622
		private static readonly RTI c_2390 = new RTI(2390);

		// Token: 0x04001DC7 RID: 7623
		private static readonly RTI c_1970 = new RTI(1970);

		// Token: 0x04001DC8 RID: 7624
		private static readonly RTI c_1590 = new RTI(1590);

		// Token: 0x04001DC9 RID: 7625
		private static readonly RTI c_1290 = new RTI(1290);

		// Token: 0x04001DCA RID: 7626
		private static readonly RTI c_1390 = new RTI(1390);

		// Token: 0x04001DCB RID: 7627
		private static readonly RTI c_490 = new RTI(490);

		// Token: 0x04001DCC RID: 7628
		private static readonly RTI c_397 = new RTI(397);

		// Token: 0x04001DCD RID: 7629
		private static readonly RTI c_345 = new RTI(345);

		// Token: 0x04001DCE RID: 7630
		private static readonly RTI c_270 = new RTI(270);

		// Token: 0x04001DCF RID: 7631
		private static readonly RTI c_190 = new RTI(190);

		// Token: 0x04001DD0 RID: 7632
		private static readonly RTI c_155 = new RTI(155);

		// Token: 0x04001DD1 RID: 7633
		private static readonly RTI c_257 = new RTI(257);

		// Token: 0x04001DD2 RID: 7634
		private static readonly RTI c_980 = new RTI(980);

		// Token: 0x04001DD3 RID: 7635
		private static readonly RTI c_57 = new RTI(57);

		// Token: 0x04001DD4 RID: 7636
		private static readonly RTI c_75 = new RTI(75);

		// Token: 0x04001DD5 RID: 7637
		private static readonly RTI c_95 = new RTI(95);

		// Token: 0x04001DD6 RID: 7638
		private static readonly RTI c_55 = new RTI(55);

		// Token: 0x04001DD7 RID: 7639
		private static readonly RTI c_97 = new RTI(97);

		// Token: 0x04001DD8 RID: 7640
		private static readonly RTI c_127 = new RTI(127);

		// Token: 0x04001DD9 RID: 7641
		private static readonly RTI c_null = new RTI(0);

		// Token: 0x04001DDA RID: 7642
		private static bool waitResponse;

		// Token: 0x04001DDB RID: 7643
		public static readonly RTI priceFullCraftTimeOnIsle = new RTI(257);

		// Token: 0x04001DDC RID: 7644
		public static readonly DonationSystem.PremiumOffer[] PremiumOffers = new DonationSystem.PremiumOffer[]
		{
			new DonationSystem.PremiumOffer(3, new RTI(99), 0, null),
			new DonationSystem.PremiumOffer(7, new RTI(155), 0, null),
			new DonationSystem.PremiumOffer(30, new RTI(535), 20, null),
			new DonationSystem.PremiumOffer(90, new RTI(1390), 25, null),
			new DonationSystem.PremiumOffer(180, new RTI(2590), 30, null),
			new DonationSystem.PremiumOffer(36000, new RTI(4770), 0, null)
		};

		// Token: 0x04001DDD RID: 7645
		private static readonly Dictionary<ShipDesignCategory, Dictionary<DesignElementCostTier, RTI>> PriceByTear;

		// Token: 0x04001DDE RID: 7646
		private static readonly RTI peaceContractDurationHours;

		// Token: 0x04001DDF RID: 7647
		private static readonly RTI tradingContractDurationDays;

		// Token: 0x04001DE0 RID: 7648
		private static readonly RTI holdProtectionFurationMinutes;

		// Token: 0x04001DE1 RID: 7649
		public static readonly DonationSystem.PacketOffer[] PremiumPagePackets;

		// Token: 0x04001DE2 RID: 7650
		public static readonly RTI HoldSubscriptionPriceScrolls;

		// Token: 0x04001DE3 RID: 7651
		private static readonly RTI c_30k;

		// Token: 0x04001DE4 RID: 7652
		private static readonly RTI c_20k;

		// Token: 0x04001DE5 RID: 7653
		private static readonly RTI c_50;

		// Token: 0x04001DE6 RID: 7654
		private static readonly RTI c_15k;

		// Token: 0x04001DE7 RID: 7655
		private static readonly RTI c_50k;

		// Token: 0x04001DE8 RID: 7656
		private static readonly RTI c_10;

		// Token: 0x04001DE9 RID: 7657
		private static readonly RTI c_20;

		// Token: 0x04001DEA RID: 7658
		private static readonly RTI c_250;

		// Token: 0x04001DEB RID: 7659
		private static readonly RTI c_300;

		// Token: 0x04001DEC RID: 7660
		private static readonly RTI c_2500;

		// Token: 0x04001DED RID: 7661
		private static readonly RTI c_5;

		// Token: 0x04001DEE RID: 7662
		private static readonly RTI c_7;

		// Token: 0x04001DEF RID: 7663
		private static readonly RTI c_10000;

		// Token: 0x04001DF0 RID: 7664
		private static readonly RTI c_1000;

		// Token: 0x04001DF1 RID: 7665
		private static readonly RTI c_2000;

		// Token: 0x04001DF2 RID: 7666
		private static readonly RTI c_4000;

		// Token: 0x04001DF3 RID: 7667
		private static readonly RTI c_100;

		// Token: 0x04001DF4 RID: 7668
		private static readonly RTI c_450k;

		// Token: 0x04001DF5 RID: 7669
		internal static readonly RTI MaxRanWhenPacketWithRankWorks;

		// Token: 0x04001DF6 RID: 7670
		public static readonly DonationSystem.PacketOffer[] Packets;

		// Token: 0x0200053B RID: 1339
		public abstract class Offer
		{
			// Token: 0x06001DF6 RID: 7670 RVA: 0x00111130 File Offset: 0x0010F330
			public Offer(RTI {25693}, Func<float> {25694}, Action<DonationSystem.Offer> {25695})
			{
				this.Price = {25693};
				this.Discount = {25694};
				this.Apply = {25695};
			}

			// Token: 0x06001DF7 RID: 7671 RVA: 0x00111150 File Offset: 0x0010F350
			public RTI GetFinalPrice()
			{
				return (int)Math.Ceiling((double)((float)this.Price.Value * this.Discount()));
			}

			// Token: 0x06001DF8 RID: 7672 RVA: 0x00111184 File Offset: 0x0010F384
			public void Click()
			{
				this.Apply(this);
			}

			// Token: 0x04001DF7 RID: 7671
			public readonly RTI Price;

			// Token: 0x04001DF8 RID: 7672
			public readonly Func<float> Discount;

			// Token: 0x04001DF9 RID: 7673
			public Action<DonationSystem.Offer> Apply;
		}

		// Token: 0x0200053C RID: 1340
		public class PremiumOffer : DonationSystem.Offer
		{
			// Token: 0x06001DF9 RID: 7673 RVA: 0x00111192 File Offset: 0x0010F392
			public PremiumOffer(RTI {25700}, RTI {25701}, int {25702} = 0, Action<DonationSystem.Offer> {25703} = null) : base({25701}, () => DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopPremium), {25703})
			{
				this.DisplayOwnDiscount = {25702};
				this.Days = {25700};
			}

			// Token: 0x04001DFA RID: 7674
			public readonly RTI Days;

			// Token: 0x04001DFB RID: 7675
			public readonly int DisplayOwnDiscount;
		}

		// Token: 0x0200053E RID: 1342
		public class PacketOffer : DonationSystem.Offer
		{
			// Token: 0x06001DFD RID: 7677 RVA: 0x001111E0 File Offset: 0x0010F3E0
			public PacketOffer(RTI {25712}, Func<float> {25713}, Func<string> {25714}, Func<string> {25715}, string {25716}, Action<DonationSystem.Offer> {25717}, Func<bool> {25718} = null, Func<ToolTipState> {25719} = null) : base({25712}, {25713}, {25717})
			{
				this.InnerText = {25714};
				this.InnerText2 = {25715};
				this.AnalyticsId = {25716};
				if ({25718} != null)
				{
					this.CheckForDisplay = {25718};
				}
				else
				{
					this.CheckForDisplay = (() => true);
				}
				this.OverrideToolTipText = null;
				this.RichTooltip = {25719};
			}

			// Token: 0x04001DFE RID: 7678
			public readonly Func<bool> CheckForDisplay;

			// Token: 0x04001DFF RID: 7679
			public readonly Func<string> InnerText;

			// Token: 0x04001E00 RID: 7680
			public readonly Func<string> InnerText2;

			// Token: 0x04001E01 RID: 7681
			public readonly string AnalyticsId;

			// Token: 0x04001E02 RID: 7682
			public string OverrideToolTipText;

			// Token: 0x04001E03 RID: 7683
			public Func<ToolTipState> RichTooltip;
		}

		// Token: 0x02000540 RID: 1344
		public class GroupedReward
		{
			// Token: 0x06001E01 RID: 7681 RVA: 0x0011125C File Offset: 0x0010F45C
			public GroupedReward(string {25723}, float {25724}, int {25725})
			{
			}

			// Token: 0x04001E06 RID: 7686
			public string RewardCategory = {25723};

			// Token: 0x04001E07 RID: 7687
			public float TotalWeight = {25724};

			// Token: 0x04001E08 RID: 7688
			public int Tier = {25725};
		}
	}
}
