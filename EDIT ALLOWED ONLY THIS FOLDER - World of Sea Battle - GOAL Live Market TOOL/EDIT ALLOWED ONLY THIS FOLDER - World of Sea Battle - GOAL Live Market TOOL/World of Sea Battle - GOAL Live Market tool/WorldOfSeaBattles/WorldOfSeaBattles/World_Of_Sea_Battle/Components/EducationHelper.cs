using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x020004FF RID: 1279
	internal class EducationHelper
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x001048EA File Offset: 0x00102AEA
		private static bool loaded
		{
			get
			{
				return Session.Account != null && Global.Player != null;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x001048FD File Offset: 0x00102AFD
		public static bool MakeInvisibleLeftUpPanel
		{
			get
			{
				return EducationHelper.loaded && Session.Account.Rang < 4 && Session.Guild == null;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x0010491D File Offset: 0x00102B1D
		public static bool MakeInvisibleShipButtons
		{
			get
			{
				return EducationHelper.loaded && Session.Account.Rang < 3 && Global.Player.UsedShipPlayer.Upgrades.InstalledCount == 0;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x0010494C File Offset: 0x00102B4C
		public static bool MakeInvisibleShipButtons_Upgrade
		{
			get
			{
				return EducationHelper.MakeInvisibleShipButtons && !Session.Account.IsEducationInProgress(EducationOnboarding.InstallUpgrade, true, false);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x00104968 File Offset: 0x00102B68
		public static bool MakeInvisibleSelectShipButton
		{
			get
			{
				return EducationHelper.loaded && Session.Account.Rang < 5 && Session.Account.Shipyard.List.Count == 1;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00104997 File Offset: 0x00102B97
		public static bool MakeInvisibleSelectFlagsButton
		{
			get
			{
				return EducationHelper.loaded && Session.Account.Rang < EducationHelper.MakeInvisibleSelectFlagsButton_endRank + 1 && !Session.Account.EducationQuest[EducationOnboarding.GameTT_Flags] && Session.Account.WorldFlag == OpenWorldFlag.Peaceful;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x001049D8 File Offset: 0x00102BD8
		public static bool MakeTravelAvailable
		{
			get
			{
				return EducationHelper.loaded && (Session.Account.CaptainSkills[PDynamicAccountBonus.СWorldTravellingLevel] > 0 || Session.Account.EducationQuest.HasFlag(EducationOnboarding.InitialQuest_OpenTravel) || Session.Account.Rang > 15);
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x00104A25 File Offset: 0x00102C25
		public static bool MakeInvisibleExitInLighthouse
		{
			get
			{
				return EducationHelper.loaded && Session.Account.Rang < 5;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00104A3D File Offset: 0x00102C3D
		public static bool MakeInvisibleSomeCraftsAndWeapons
		{
			get
			{
				return EducationHelper.loaded && Session.Account.Rang < 7;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x00104A55 File Offset: 0x00102C55
		public static int ArenaAvailableSinceRank
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x00104A58 File Offset: 0x00102C58
		public static bool MakeInvisibleArenaAndModesPortButtons
		{
			get
			{
				return EducationHelper.loaded && Session.Account.Rang < EducationHelper.ArenaAvailableSinceRank;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x00104A74 File Offset: 0x00102C74
		public static bool ShowSkillPointsToolTip
		{
			get
			{
				return EducationHelper.loaded && Session.Account.Rang > 1;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x00104A8C File Offset: 0x00102C8C
		public static bool ShowPowerupItemsPage
		{
			get
			{
				return EducationHelper.loaded && Session.Account.Rang > 3;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x00104AA4 File Offset: 0x00102CA4
		public static int ShowLegendarySkillsRank
		{
			get
			{
				return 30;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x00104AA8 File Offset: 0x00102CA8
		public static bool AllowBoarding
		{
			get
			{
				return EducationHelper.loaded && (Session.Account.EducationQuest.HasFlag(EducationOnboarding.GameTT_OpenBoarding) || Session.Account.Rang >= 6 || Session.Account.Quests.HasBoardingQuests || Global.Settings.SelectedCannonBalls[CannonLocation.LeftSide] == 4);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x00104A25 File Offset: 0x00102C25
		public static bool AvailableResToSellFlickering
		{
			get
			{
				return EducationHelper.loaded && Session.Account.Rang < 5;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x00104B04 File Offset: 0x00102D04
		public static bool ArenaWindowShowRating
		{
			get
			{
				return EducationHelper.loaded && Session.Account.ArenaRating.Value > 0;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x00104B21 File Offset: 0x00102D21
		public static bool AskAboutTakingOffEquipmentForJustCraftedShip
		{
			get
			{
				return EducationHelper.loaded && Session.Account.Shipyard.List.Count <= 3;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x00104B46 File Offset: 0x00102D46
		public static bool MakeTransparentPortsOnMinimap
		{
			get
			{
				return EducationHelper.loaded && Session.Account.Rang <= 6;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x00104B61 File Offset: 0x00102D61
		public static bool HighlightMyPorts
		{
			get
			{
				return Session.Account.Rang <= 5;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x00104B73 File Offset: 0x00102D73
		public static bool AlwaysShowSpeedIcon
		{
			get
			{
				return Global.Player.MapInfo.IsEducationMap;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x00104B84 File Offset: 0x00102D84
		public static int PersonalIsleAvailableSinceRank
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x00104B88 File Offset: 0x00102D88
		public static bool ShowRealShopToolTip
		{
			get
			{
				return (Session.Account.Rang <= 5 || !Session.Account.EducationQuest.HasFlag(EducationOnboarding.OpenRealShop)) && Session.Account.Analytics.SummaryDonations == 0;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x00104BBE File Offset: 0x00102DBE
		public static float ShowUnavailableForAttackTt
		{
			get
			{
				if (Session.Account.Rang < 5)
				{
					return 0.8f;
				}
				if (Session.Account.Rang >= 15)
				{
					return 0f;
				}
				return 0.5f;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x00104BEC File Offset: 0x00102DEC
		public static bool EnableLootCombat
		{
			get
			{
				Vector2 position = Global.Player.Position;
				return Gameplay.GetNearWorldRegion(position).LevelInt >= 3;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x00104C16 File Offset: 0x00102E16
		public static bool DailyQuestsVisible
		{
			get
			{
				return Session.Account.EducationQuest.HasFlag(EducationOnboarding.InitialQuest_OpenDaily) || Session.Account.Rang > 10;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x00104C3B File Offset: 0x00102E3B
		public static bool DailyQuestsTier2Visible
		{
			get
			{
				return Session.Account.EducationQuest.HasFlag(EducationOnboarding.InitialQuest_OpenDaily_2) || Session.Account.Rang > 22;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x00104C60 File Offset: 0x00102E60
		public static bool ShowWindOnGlobalMap
		{
			get
			{
				return Session.Account.Rang >= 3;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x00104C72 File Offset: 0x00102E72
		public static bool ShowSights
		{
			get
			{
				return !Global.Player.MapInfo.IsEducationMap || {20059}.CurrentInstance != null;
			}
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x00104C90 File Offset: 0x00102E90
		public static bool IsPortButtonAvailableAndOpenable(PortTipConnection {25299})
		{
			if (Session.Account.Rang >= 10)
			{
				return true;
			}
			if (!Session.Account.EducationQuest.HasFlag(EducationOnboarding.InteropQuestUnit))
			{
				return false;
			}
			if ({25299} == PortTipConnection.Taverna)
			{
				return Session.Account.EducationQuest.HasFlag(EducationOnboarding.InitialQuest_OpenTaverna);
			}
			if ({25299} == PortTipConnection.Trade)
			{
				return Session.Account.Rang >= 3 || Session.Account.IsEducationInProgress(EducationOnboarding.ShopCannonBalls, true, false) || Session.Account.IsEducationInProgress(EducationOnboarding.SellItemsInTrade, true, false) || Session.Account.IsEducationInProgress(EducationOnboarding.TradeBetweenPorts, true, false);
			}
			return {25299} != PortTipConnection.Workshop || Session.Account.Rang > 5 || Session.Account.IsEducationInProgress(EducationOnboarding.CraftCannon, true, false) || Session.Account.IsEducationInProgress(EducationOnboarding.BuildNextShip, true, false);
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00104D50 File Offset: 0x00102F50
		public static bool HideUpgrade(ShipUpgradeInfo {25300})
		{
			if (Session.Account.Rang >= 8)
			{
				return false;
			}
			return {25300}.GetEffects(Global.Player.UsedShipPlayer.CraftFrom).Any((ShipBonus {25320}) => {25320}.Type == ShipBonusEffect.MExtraUpgradePlaces || {25320}.Type == ShipBonusEffect.PBoardingGold || {25320}.Type == ShipBonusEffect.PTiltReduce || {25320}.Type == ShipBonusEffect.PDamageCannonIfStrengthBelow30P || {25320}.Type == ShipBonusEffect.PReduceReceivesMortarDamage);
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00104DA5 File Offset: 0x00102FA5
		public static IEnumerable<CrewNotificationManager.RandomPhrase> GetSupportPhrases()
		{
			return new EducationHelper.<GetSupportPhrases>d__63(-2);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00104DAE File Offset: 0x00102FAE
		public static void OnFalkonetGun()
		{
			Session.FalkonedUsedThisSession = true;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x00104DB8 File Offset: 0x00102FB8
		public static void OnCannonsGun(bool {25301})
		{
			if (!Session.Account.EducationQuest.HasFlag(EducationOnboarding.GameTT_SwitchCannonBalls) && Global.Player.UsedShipPlayer.BallsOfHold[12] > 0 && Global.Player.GetBattleTimer > 0f)
			{
				Session.EducState_BallsChangeTtCounter += ({25301} ? 10 : 1);
				if (Session.EducState_BallsChangeTtCounter > 100)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_SwitchCannonBalls, true);
				}
			}
			if ({25301} && Global.Game.InterestPoints.ShipInSight != null)
			{
				if (Vector2.Distance(Global.Game.InterestPoints.ShipInSight.Position, Global.Player.Position) > Global.Player.UsedShip.Cannons.MinMaxDistance.Y + Global.Player.UsedShip.BallDistanceBonusValue + 15f)
				{
					Session.EducState_HighDistanceTt++;
				}
				else
				{
					Session.EducState_HighDistanceTt = 0;
				}
			}
			if (!Session.FalkonedUsedThisSession && !Session.Account.EducationQuest.HasFlag(EducationOnboarding.FalkonetDamage) && Global.Player.GetBattleTimer > 0f)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_UseFalkonets, true);
				return;
			}
			if (!Session.PowderKegsUsedThisSession && !Session.Account.EducationQuest.HasFlag(EducationOnboarding.PowderKegDamage) && Global.Player.GetBattleTimer > 0f && Session.Account.Rang > 1 && !Global.Player.UsedShipPlayer.PowderKegsOfHold.IsEmpty)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_UsePowderKegs, true);
			}
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x00104F2C File Offset: 0x0010312C
		public static void ExitToWoldClicked(Action {25302})
		{
			if (Session.EducState_PortLaunchCounter >= 2 && Session.Account.Rang >= EducationHelper.MakeInvisibleSelectFlagsButton_endRank && !Session.Account.EducationQuest.HasFlag(EducationOnboarding.GameTT_Flags))
			{
				new {21990}({25302});
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_Flags, true);
				return;
			}
			{25302}();
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x00104F7B File Offset: 0x0010317B
		public static void EnterToGameFromPort()
		{
			if (Session.Account.Rang <= 5 && Global.Player.ResourcesOfHold.GetCount(1) == 0 && Session.Account.NearPortStorage[1] > 0)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_PickWoodForMending, true);
			}
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x00104FB8 File Offset: 0x001031B8
		public static void EnterToPortFromGame(bool {25303})
		{
			if (!Session.Account.IsEducationInProgress(EducationOnboarding.DestroyNpcBlack, false, false) || !{25303})
			{
				Session.EducState_PortLaunchCounter++;
			}
			if (Global.Player.NearPortType != PortEnteringType.PersonalIsle)
			{
				if (Global.Player.NearPort.Type != PortType.PirateBay)
				{
					PortCaptureStatus byPortId = Session.Game.WorldPorts.GetByPortId(Global.Player.NearPort.PortID);
					if (byPortId == null || byPortId.CapturerFraction != FractionID.Pirate)
					{
						goto IL_82;
					}
				}
				Session.EducState_PirateBayLaunchCounter++;
			}
			IL_82:
			if (Session.Account.Rang == 1 && Session.EducState_PortLaunchCounter == 1)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_FirstPortEntry, true);
			}
			else if (Session.HasCapers && Session.Account.Rang > 1)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_MendingAndDecraftCapers, true);
			}
			else if (Session.Account.Rang >= 4 && Session.Account.Rang <= 8 && Global.Settings.UnreadGamepedia.Size > 0)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_OpenGamepediaByHand, true);
			}
			if (Session.EducState_PirateBayLaunchCounter == 1)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_PirateTrader, true);
			}
			if (!EducationHelper.MakeInvisibleExitInLighthouse && {17312}.CurrentInstance == null)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_ExitLighthouse, true);
			}
			if (Session.EducState_PortLaunchCounter > 1 && Session.Account.Analytics.TotalGameTimeHours > (double)new Sequence((int)Session.Account.SID).Pick<int>(new int[]
			{
				10,
				20,
				30,
				40
			}) && !Session.Account.EducationQuest.HasFlag(EducationOnboarding.PollOpened) && {17312}.CurrentInstance == null)
			{
				EducationHelper.MakeFlag(EducationOnboarding.PollOpened, false);
				new {22659}();
			}
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x00105148 File Offset: 0x00103348
		public static void OnShipWasLooted()
		{
			if (Global.Player.MapInfo.IsWorldmap && !Session.EducState_HoldTtWasShown && !Session.Account.EducationQuest.HasFlag(EducationOnboarding.OpenHoldFromGame) && Session.Account.Rang < 10)
			{
				Session.EducState_HoldTtWasShown = true;
				{19994}.Me({19988}.Info, Local.educ_holdtt(Global.Settings.kb_OpenHold.KeyToString), Array.Empty<object>());
			}
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x001051B4 File Offset: 0x001033B4
		public static void OnArenaWasStarted()
		{
			if (Session.EducState_ArenaSocialUiToolTipWasShown || Session.Account.Rang > 20)
			{
				return;
			}
			Session.EducState_ArenaSocialUiToolTipWasShown = true;
			{19994}.Me({19988}.Info, Local.educ_arenatt(Global.Settings.kb_SendGroupCommand.KeyToString), Array.Empty<object>());
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x00003100 File Offset: 0x00001300
		public static void OnProbablyPvpAttempt(ShipOtherPlayer {25304})
		{
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x001051F3 File Offset: 0x001033F3
		internal static void OnReceiveDamage(in DamageData {25305})
		{
			if (Session.Account.Rang > 2 && ({25305}.SourcePawnType == DamageID.CannonBall || {25305}.SourcePawnType == DamageID.FalkonetBall))
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_ShipBattleDetails, true);
			}
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x0010521C File Offset: 0x0010341C
		internal static void SendDamageFromPowderKeg(PowderKegInfo {25306})
		{
			EducationHelper.MakeFlag(EducationOnboarding.PowderKegDamage, false);
			Session.PowderKegsUsedThisSession = true;
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x0010522C File Offset: 0x0010342C
		public static void RecieveDamageFromPoderKeg(PowderKegInfo {25307})
		{
			if ({25307}.IsInvisibleMine)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_ReceivedMineDamage, true);
			}
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x00105240 File Offset: 0x00103440
		internal static void OnMakeCannonBallDamage(ClientCannonBall {25308}, Ship {25309} = null)
		{
			if (Global.Player.MapInfo.IsEducationMap)
			{
				return;
			}
			if ({25308}.BallInfo.AmmoType == CannonAmmoType.FalkonetBall)
			{
				EducationHelper.MakeFlag(EducationOnboarding.FalkonetDamage, false);
			}
			if ({25308}.BallInfo.ID == 4)
			{
				EducationHelper.MakeFlag(EducationOnboarding.AnticrewDamage, false);
			}
			if ({25308}.BallInfo.AmmoType == CannonAmmoType.CannonBall && {25308}.DamageFlags.HasFlag(SpecificDamageFlags.SternDamage))
			{
				EducationHelper.MakeFlag(EducationOnboarding.KillThroughStern, false);
			}
			if ({25309} != null && {25308}.DamageFlags.HasFlag(SpecificDamageFlags.SingleSailDamage) && {25309}.UsedShip.FirstSailHP <= 0.9f)
			{
				EducationHelper.MakeEducationFlagWhenQuestActive(EducationOnboarding.DestroySailes, true);
			}
			if ((Global.Player.MapInfo.IsWorldmap && Global.Player.UsedShipPlayer.BallsOfHold[4] > 40 && Session.Account.Rang >= 6) || (Session.Account.Quests.HasBoardingQuests && {25309} != null && {25309}.UsedShip.HpFactor < 0.8f))
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_OpenBoarding, true);
			}
			if ({25309} != null)
			{
				ShipNpc shipNpc = {25309} as ShipNpc;
				if (shipNpc != null && shipNpc.ProbablyWasDamagedByOtherPlayers)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_LootDivision, true);
				}
			}
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x00105373 File Offset: 0x00103573
		internal static void OnPowerupItemUsed(PowerupItemInfo {25310})
		{
			if (Global.Player.GetBattleTimer > 0f)
			{
				EducationHelper.MakeFlag(EducationOnboarding.UsePowerupItemInBattle, false);
			}
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x0010538E File Offset: 0x0010358E
		public static void NewRank()
		{
			if (Session.Account.Rang >= EducationHelper.ShowLegendarySkillsRank)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_LegendSkillsWasOpened, true);
			}
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x001053A9 File Offset: 0x001035A9
		public static void AfterRespawn(bool {25311}, bool {25312})
		{
			if ({25311})
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_RespawnNotInNearPort, true);
				return;
			}
			if (!Session.Account.WorldFlag.IsPeaceMode() && Session.LastDamageByPlayer)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_Flags, true);
			}
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x001053D7 File Offset: 0x001035D7
		public static void MakeEducationFlagWhenQuestActive(EducationOnboarding {25313}, bool {25314} = true)
		{
			if (Session.Account.IsEducationInProgress({25313}, false, false))
			{
				EducationHelper.MakeFlag({25313}, {25314});
			}
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x001053EF File Offset: 0x001035EF
		public static void MakeFlagIfNoActiveUi(EducationOnboarding {25315})
		{
			if ({18593}.CurrentInstance == null)
			{
				EducationHelper.MakeFlag({25315}, true);
			}
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x00105400 File Offset: 0x00103600
		public static void WhenGetQuest(QuestInfo {25316})
		{
			QuestTransferOrder questTransferOrder = {25316}.FirstStep as QuestTransferOrder;
			if (questTransferOrder != null)
			{
				IslePortInfo targetAsPort = questTransferOrder.TargetAsPort;
				Session.WorldMapMarkerPosition = new Vector2?((targetAsPort != null) ? targetAsPort.EntryPos : questTransferOrder.TargetPosition);
			}
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x00105440 File Offset: 0x00103640
		public static void MakeFlag(EducationOnboarding {25317}, bool {25318} = true)
		{
			if (Global.Player.MapInfo.IsEducationMap)
			{
				return;
			}
			if (Session.Account.UpdateEducationFlags({25317}) || {25317} == EducationOnboarding.GameTT_LowReputationKickFromGuild)
			{
				Global.Network.Send(new OnSyncEducationFlags({25317}));
				if ({25318} && {25317}.ToString().Contains("GameTT") && Global.Settings.EnableGameTooltips && {18593}.CurrentInstance == null)
				{
					new {18593}(new EducationOnboarding?({25317}));
				}
			}
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x001054C4 File Offset: 0x001036C4
		public static Action GetEducationQuestNeedToFinishInPort()
		{
			if (Global.Player == null || Session.Account == null || !Global.Player.MapInfo.IsWorldmap)
			{
				return null;
			}
			QuestRunningProgress questRunningProgress = Session.Account.Quests.ProgressRunningQuests.FirstOrDefault(delegate(QuestRunningProgress {25321})
			{
				QuestEducationFlag questEducationFlag = {25321}.Info.FirstStep as QuestEducationFlag;
				return questEducationFlag != null && Session.Account.EducationQuest.HasFlags(questEducationFlag.Mask);
			});
			if (questRunningProgress == null)
			{
				return null;
			}
			QuestFollowToPortHeader questFollowToPortHeader = questRunningProgress.CurrentStep as QuestFollowToPortHeader;
			if (questFollowToPortHeader != null && questFollowToPortHeader.ToMyHomePort && (int)Session.Account.StartPortId != Global.Player.NearPort.PortID)
			{
				return null;
			}
			return delegate()
			{
				Global.Game.ScenePort.MakeAccSync();
				Global.Network.Send(new OnSyncEducationFlags(EducationOnboarding.Server_WhenPortEntry));
			};
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x00105580 File Offset: 0x00103780
		internal static void Update(ref FrameTime {25319})
		{
			if (Global.Player != null && Session.Account != null)
			{
				if (Session.Account.IsEducationInProgress(EducationOnboarding.CompleteQuest, false, false) && !Session.Account.EducationQuest.HasFlag(EducationOnboarding.InitialQuest_OpenTaverna))
				{
					Session.Account.EducationQuest.Append(EducationOnboarding.InitialQuest_OpenTaverna);
					Assert.Report(true, "has not InitialQuest_OpenTaverna");
				}
				if (Session.Account.Rang >= 8 && Global.Player.MapInfo.IsWorldmap && Global.Player.NowSpeed < 5f && Session.TimeFromLastSendedCBDamageSec > 30f && Session.TimeFromLastReceivedDamageSec > 30f)
				{
					if (Global.Game.WorldInstance.EnumerateAllPlayers().Any((Player {25322}) => Vector2.Distance({25322}.Position, Global.Player.Position) < 70f && {25322}.NowSpeed < 1f) && !Session.Account.EducationQuest.HasFlag(EducationOnboarding.OpenSocialMenu))
					{
						EducationHelper.MakeFlag(EducationOnboarding.GameTT_OpenSocialMenu, true);
					}
				}
				if (Global.Player.UsedShipPlayer.GetItemsMass() / Global.Player.UsedShipPlayer.Capacity > 0.7f)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_ShipLoadingAndOverloading, true);
				}
				if (Global.Player.MapInfo.IsPassingUi && Global.Settings.GamemodeDoublones.Value > 50 && !Global.Player.UsedShipPlayer.HasArenaUpgrades())
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_UseDoublonesPassingMap, true);
				}
				if (Global.Player.MapInfo.IsWorldmap && !Global.Player.IsEntryToPortZoneContains)
				{
					if (Session.EducState_PortLaunchCounter == 1)
					{
						EducationHelper.MakeFlag(EducationOnboarding.GameTT_FirstExitPort, true);
					}
					if (Global.Player.UsedShipPlayer.Mortars.Iterate().Any((CannonGameInstance {25323}) => {25323}.Info.Feature > CannonFeature.Default))
					{
						EducationHelper.MakeFlag(EducationOnboarding.GameTT_FirstExitSpecificMortar, true);
					}
				}
				if (Global.Player.IsEntryToPortZoneContains && Global.Player.GetBattleTimer > 0f)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_BattleTimer, true);
				}
				if (Session.Guild != null && Session.Account.Reputations.ShouldKickByReputation(Session.Guild, Session.Game.WorldPorts, -10f) && EducationHelper.ttlToNextLowReputationAlert == 0f)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_LowReputationKickFromGuild, true);
					EducationHelper.ttlToNextLowReputationAlert = 420f;
				}
				if (Session.Account.Shipyard.CountOfCompletlyResearchedClasses() > 0)
				{
					EducationHelper.MakeFlag(EducationOnboarding.FinishAnyShipBranchResearch, true);
				}
				if (Global.Player.IsPortEntry && Session.Account.NearPortStorage[27] >= 10)
				{
					EducationHelper.MakeFlag(EducationOnboarding.MakeBulkByQuest, true);
				}
				if (Session.Account.IsEducationInProgress(EducationOnboarding.ResearchSkillSailingCategory, false, false) && Session.Account.EducationQuest[EducationOnboarding.BuyWoodInOtherPort] && {19779}.CurrentInstance == null && {17312}.CurrentInstance == null && Global.Player.IsPortEntry && Global.Game.ScenePort.IsMainPage)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_SkillsWindowToolTip, true);
				}
				if (Session.Account.IsEducationInProgress(EducationOnboarding.GetItemsFromFactory, false, false) && {19779}.CurrentInstance == null && {17107}.CurrentInstance == null && {17312}.CurrentInstance == null)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_GetResFromOverloadedFactory, true);
				}
				if (Global.Player.IsInShallowWater > 0f)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_Shallow, true);
				}
				if (Global.Player.UsedShipPlayer.BallsOfHold[2] > 0)
				{
					if (Global.Game.WorldInstance.FindPowderKegNearPlayer((ClientPowderKeg {25324}) => {25324}.SourceUID <= 0) != null && Session.Account.Rang > 5)
					{
						EducationHelper.MakeFlag(EducationOnboarding.GameTT_DestroyPowderKegByFire, true);
					}
				}
				{25319}.EvaluteTimerSec(ref EducationHelper.ttlToNextLowReputationAlert);
			}
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00105914 File Offset: 0x00103B14
		public static void ShowBlockingByQuestError()
		{
			new {17312}(Local.education_quest_not_opened, delegate(int {25325})
			{
				if ({25325} == 0)
				{
					new {19779}(false, null, null);
				}
			}, new {17443}[]
			{
				new {17443}(Local.know_more, "", {17312}.cIconSpyglass, false, 0f),
				new {17443}(Local.close, "", {17312}.cIconReject, false, 0f)
			});
		}

		// Token: 0x04001C5D RID: 7261
		private static int MakeInvisibleSelectFlagsButton_endRank = 6;

		// Token: 0x04001C5E RID: 7262
		public const float InvisibleOpacity = 0.3f;

		// Token: 0x04001C5F RID: 7263
		private static float ttlToNextLowReputationAlert;
	}
}
