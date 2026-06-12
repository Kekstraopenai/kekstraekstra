using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Collections;
using WorldOfSeaBattles.Components.Apis;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Core
{
	// Token: 0x020004EA RID: 1258
	internal sealed class Session
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x000FFC1A File Offset: 0x000FDE1A
		public static Rectangle CaptainIcon
		{
			get
			{
				return CommonAtlas.captainIcons[Math.Min(CommonAtlas.captainIcons.Length - 1, (int)Session.Account.SelectedCaptainIcon)];
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x000FFC3E File Offset: 0x000FDE3E
		public static Decorator Game
		{
			get
			{
				return new Decorator(Global.Player, Session.Account, ref Session.ServerWorldStatus, Session.Guild);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x000FFC59 File Offset: 0x000FDE59
		public static PowerupItemsSlots ActivePowerupItemSlots
		{
			get
			{
				return Session.Account.Shipyard.CurrentRealShip.PowerupItemSlots;
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06001BDE RID: 7134 RVA: 0x000FFC70 File Offset: 0x000FDE70
		// (remove) Token: 0x06001BDF RID: 7135 RVA: 0x000FFCA4 File Offset: 0x000FDEA4
		public static event Action FriendsChanged;

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x000FFCD7 File Offset: 0x000FDED7
		public static GuildMember MyMemberInGuild
		{
			get
			{
				return Session.Guild.GetMember(Session.Account.SID);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x000FFCED File Offset: 0x000FDEED
		public static PbsBatlleSide EngagingInPortBattleWorld
		{
			get
			{
				if (!Global.Player.MapInfo.IsWorldmap)
				{
					return PbsBatlleSide.None;
				}
				return Session.EngagingInPortBattle;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x000FFD08 File Offset: 0x000FDF08
		public static float TimeFromLastReceivedDamageSec
		{
			get
			{
				if (Session.TimeFromLastReceivedDamage != null)
				{
					return (float)Session.TimeFromLastReceivedDamage.Elapsed.TotalSeconds;
				}
				return 100000f;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x000FFD38 File Offset: 0x000FDF38
		public static float TimeFromLastSendedCBDamageSec
		{
			get
			{
				if (Session.TimeFromLastSendedCBDamage != null)
				{
					return (float)Session.TimeFromLastSendedCBDamage.Elapsed.TotalSeconds;
				}
				return 100000f;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x000FFD68 File Offset: 0x000FDF68
		public static float TTimeFromLastKillSec
		{
			get
			{
				if (Session.TimeFromLastKill != null)
				{
					return (float)Session.TimeFromLastKill.Elapsed.TotalSeconds;
				}
				return 100000f;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x000FFD95 File Offset: 0x000FDF95
		public static CannonBallInfo SelectedFalkonetsInfo
		{
			get
			{
				return Gameplay.BallsInfo.FromID(Global.Settings.SelectedFalkonetsID);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x000FFDAB File Offset: 0x000FDFAB
		public static PowderKegInfo SelectedPowderKegsInfo
		{
			get
			{
				return Gameplay.PowderKegsInfo.FromID(Global.Settings.SelectedPowderKegs);
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x000FFDC1 File Offset: 0x000FDFC1
		public static CannonBallInfo SelectedMortarBalls
		{
			get
			{
				return Gameplay.BallsInfo.FromID(Global.Settings.SelectedMortarBallsID);
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x000FFDD7 File Offset: 0x000FDFD7
		// (set) Token: 0x06001BE9 RID: 7145 RVA: 0x000FFDDE File Offset: 0x000FDFDE
		public static GroupCommon Group
		{
			get
			{
				return Session.group;
			}
			set
			{
				if (value == null)
				{
					Session.group.Dispose();
				}
				Session.group = value;
				Action<GroupCommon> evGroupChanged = Session.EvGroupChanged;
				if (evGroupChanged == null)
				{
					return;
				}
				evGroupChanged(value);
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06001BEA RID: 7146 RVA: 0x000FFE04 File Offset: 0x000FE004
		// (remove) Token: 0x06001BEB RID: 7147 RVA: 0x000FFE38 File Offset: 0x000FE038
		public static event Action<GroupCommon> EvGroupChanged;

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x000FFE6B File Offset: 0x000FE06B
		public static bool HasCapers
		{
			get
			{
				return Session.LastMinimapAndGroupUpdate.allies.Any((AllyStateTransfer {25191}) => {25191}.ShipClass == byte.MaxValue);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x000FFE9B File Offset: 0x000FE09B
		public static int CountOfCapers
		{
			get
			{
				return Session.LastMinimapAndGroupUpdate.allies.Count((AllyStateTransfer {25192}) => {25192}.ShipClass == byte.MaxValue && {25192}.uID != -1);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x000FFECB File Offset: 0x000FE0CB
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x000FFED2 File Offset: 0x000FE0D2
		public static bool IsInRandomPvpZone { get; set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x000FFEDA File Offset: 0x000FE0DA
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x000FFEE1 File Offset: 0x000FE0E1
		public static VKPlayData VKPlay { get; set; }

		// Token: 0x06001BF2 RID: 7154 RVA: 0x000FFEE9 File Offset: 0x000FE0E9
		public static void OnFriendsChanged()
		{
			Action friendsChanged = Session.FriendsChanged;
			if (friendsChanged == null)
			{
				return;
			}
			friendsChanged();
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x000FFEFC File Offset: 0x000FE0FC
		public static bool IsShipContainsPlayerGroup(int {25189})
		{
			if (Global.Player == null)
			{
				return false;
			}
			if (Global.Player.MapInfo.IsPassingUi)
			{
				return true;
			}
			if (!Global.Player.MapInfo.IsWorldmap)
			{
				return Session.CurrentArenaSession != null && Array.IndexOf<int>(Session.CurrentArenaSession.ArenaAllyPlayersUid, {25189}) != -1;
			}
			return Session.Group != null && Session.Group.ContainsUID({25189});
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x000FFF6C File Offset: 0x000FE16C
		public static void InstallAccount(PlayerAccount {25190})
		{
			Session.Account = {25190};
			if ({25190}.Analytics.TotalGameTimeSec < 60.0)
			{
				Session.IsFirstGameRun = true;
			}
			{25190}.EvGiveNewLevel += delegate(int {25193})
			{
				{19028}.QueueAchievementDisplay({25193});
				EducationHelper.NewRank();
			};
			Action {1588};
			if (({1588} = Session.<>O.<0>__WhenNewDraftOpened) == null)
			{
				{1588} = (Session.<>O.<0>__WhenNewDraftOpened = new Action(CrewNotificationManager.WhenNewDraftOpened));
			}
			{25190}.EvOpenNextShip += {1588};
			QuestEngine quests = {25190}.Quests;
			Action<QuestEngine.CompletedQuestArgs> {7271};
			if (({7271} = Session.<>O.<1>__WhenCompleteQuest) == null)
			{
				{7271} = (Session.<>O.<1>__WhenCompleteQuest = new Action<QuestEngine.CompletedQuestArgs>(CrewNotificationManager.WhenCompleteQuest));
			}
			quests.OnCompleteQuest += {7271};
			QuestEngine quests2 = {25190}.Quests;
			Action<QuestInfo> {7269};
			if (({7269} = Session.<>O.<2>__WhenFailQuest) == null)
			{
				{7269} = (Session.<>O.<2>__WhenFailQuest = new Action<QuestInfo>(CrewNotificationManager.WhenFailQuest));
			}
			quests2.OnFailQuest += {7269};
			QuestEngine quests3 = {25190}.Quests;
			Action<QuestInfo, QuestRunningProgress, bool> {7273};
			if (({7273} = Session.<>O.<3>__WhenQuestProgress) == null)
			{
				{7273} = (Session.<>O.<3>__WhenQuestProgress = new Action<QuestInfo, QuestRunningProgress, bool>(CrewNotificationManager.WhenQuestProgress));
			}
			quests3.OnProgress += {7273};
			QuestEngine quests4 = {25190}.Quests;
			Action<QuestInfo> {7267};
			if (({7267} = Session.<>O.<4>__WhenRunQuest) == null)
			{
				{7267} = (Session.<>O.<4>__WhenRunQuest = new Action<QuestInfo>(CrewNotificationManager.WhenRunQuest));
			}
			quests4.OnRunQuest += {7267};
			QuestEngine quests5 = {25190}.Quests;
			Action {7275};
			if (({7275} = Session.<>O.<5>__WhenQuestsInteractiveDisablesRemoved) == null)
			{
				{7275} = (Session.<>O.<5>__WhenQuestsInteractiveDisablesRemoved = new Action(CrewNotificationManager.WhenQuestsInteractiveDisablesRemoved));
			}
			quests5.OnInteractiveSomeDisablesRemoved += {7275};
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x001000A0 File Offset: 0x000FE2A0
		public static void RemoveResources()
		{
			Session.ChatOffCache.Clear();
			Session.FriendsCache.Clear();
			Session.FriendsRequests.Clear();
			Session.LSEventButtonState = false;
			Session.Guild = null;
			Session.LoadedGuildIconTexture = null;
			Session.EducState_PortLaunchCounter = 0;
			Session.EducState_PirateBayLaunchCounter = 0;
			Session.LastDamage = null;
			Session.CurrentArenaSession = null;
			Session.CurrentPassingSession = null;
			Session.CachedUiArenaMode = null;
			Session.LastDeathPosition = null;
			Session.HasNotYetReviewedActions = false;
			Session.SelectedChatRoom = ChatRoomType.Global;
			Session.SaveChatInput = null;
			Session.TimeFromLastReceivedDamage = null;
			Session.TimeFromLastKill = null;
			Session.LastSentCBDamageUID = -1;
			Session.FriendsChanged = null;
			Session.EducState_ShipMendingHpCounter = 0f;
			Session.ArenaGames = new ArenaGamesCount();
			Session.EngagingInPortBattle = PbsBatlleSide.None;
			Session.group = null;
			Session.EvGroupChanged = null;
			Session.IsFirstGameRun = false;
			Session.TimeFromLastSendedCBDamage = null;
			Session.CurrentCrewJob = null;
			Session.NotificationsTooltip = false;
			Session.LastLoadedNonEmptyCannonBalls = null;
			Session.WorldMapMarkerPosition = null;
			Session.WReload = new WeaponsReloading();
			{19994}.SessionFinished();
			Session.LsMessageDraft = string.Empty;
			Session.ChatIsHidden = false;
			Session.GuildUnreceivedSalary = 0;
			Session.LastMinimapAndGroupUpdate.allies.Clear();
			Session.LastMinimapAndGroupUpdate.enemies.Clear();
			Session.LastMapResponse = null;
			Session.EducState_GunShotCounter = 0;
			Session.SouredResourcesCounter = 0;
			Session.DeadCrewCounter = 0;
			Session.LastTraderInSeaOffers = null;
			Session.IsInRandomPvpZone = false;
			Session.RandomPvpZoneRewardAmount = 0;
			Session.RandomPvpZoneRewardStep = 0;
			Session.RandomPvpZoneKills = 0;
			Session.LastDamageByPlayer = false;
			Session.PlayerPublicDesigns.Clear();
			Session.PublicDesignsCache.Clear();
			Session.CurrentPortAuctionResources.Clean();
			Session.UsedCraftTimeSync = 0f;
			Session.WhaleHarpoonShots = 0;
			Session.AvanpostModeRemainRespawns = 0;
			Session.HadReferalBonus = false;
			Session.LoadedArenaUiInfo = null;
			Session.SaldoAnalytics = new ResourceFlowAnalytics();
		}

		// Token: 0x04001AA2 RID: 6818
		public static PlayerAccount Account;

		// Token: 0x04001AA3 RID: 6819
		public static string ServerSessionHash;

		// Token: 0x04001AA4 RID: 6820
		public static ServerWorldInformation ServerWorldStatus;

		// Token: 0x04001AA5 RID: 6821
		public static int AvanpostModeRemainRespawns;

		// Token: 0x04001AA6 RID: 6822
		public static ClientEventActionsPipeline EventActionsPipeline = new ClientEventActionsPipeline();

		// Token: 0x04001AA7 RID: 6823
		public static Tlist<ValueTuple<bool, OnChatMessageEvent>> ChatOffCache = new Tlist<ValueTuple<bool, OnChatMessageEvent>>(100);

		// Token: 0x04001AA8 RID: 6824
		public static ChatRoomType SelectedChatRoom = ChatRoomType.Global;

		// Token: 0x04001AA9 RID: 6825
		public static string SaveChatInput;

		// Token: 0x04001AAA RID: 6826
		public static bool ChatIsHidden;

		// Token: 0x04001AAB RID: 6827
		public static Tlist<FriendCacheItem> FriendsCache = new Tlist<FriendCacheItem>(10);

		// Token: 0x04001AAC RID: 6828
		public static Tlist<FriendRequest> FriendsRequests = new Tlist<FriendRequest>();

		// Token: 0x04001AAD RID: 6829
		public static bool LSEventButtonState;

		// Token: 0x04001AAE RID: 6830
		public static string LsMessageDraft = string.Empty;

		// Token: 0x04001AB0 RID: 6832
		public static GuildCommon Guild;

		// Token: 0x04001AB1 RID: 6833
		public static CapturePortsRistrictionType GuildCacheServer_CapturingRistriction;

		// Token: 0x04001AB2 RID: 6834
		public static int GuildUnreceivedSalary;

		// Token: 0x04001AB3 RID: 6835
		public static int GuildCacheServer_Rating;

		// Token: 0x04001AB4 RID: 6836
		public static PbsBatlleSide EngagingInPortBattle;

		// Token: 0x04001AB5 RID: 6837
		public static IslePortInfo EngagingInPortBattlePort;

		// Token: 0x04001AB6 RID: 6838
		public static OnUpdatePbTimeoutOrStatus PortBattleInfo;

		// Token: 0x04001AB7 RID: 6839
		public static Texture2D LoadedGuildIconTexture;

		// Token: 0x04001AB8 RID: 6840
		public static ArenaCacheItem CurrentArenaSession;

		// Token: 0x04001AB9 RID: 6841
		public static PassingCacheItem CurrentPassingSession;

		// Token: 0x04001ABA RID: 6842
		public static ArenaMode? CachedUiArenaMode;

		// Token: 0x04001ABB RID: 6843
		public static OnLoadArena? LoadedArenaUiInfo;

		// Token: 0x04001ABC RID: 6844
		public static int EducState_PortLaunchCounter;

		// Token: 0x04001ABD RID: 6845
		public static int EducState_PirateBayLaunchCounter;

		// Token: 0x04001ABE RID: 6846
		public static bool EducState_IsInitialMessageShown;

		// Token: 0x04001ABF RID: 6847
		public static int EducState_LootedShipsCount = 0;

		// Token: 0x04001AC0 RID: 6848
		public static float EducState_ShipMendingHpCounter;

		// Token: 0x04001AC1 RID: 6849
		public static bool EducState_ArenaSocialUiToolTipWasShown;

		// Token: 0x04001AC2 RID: 6850
		public static bool EducState_HoldTtWasShown;

		// Token: 0x04001AC3 RID: 6851
		public static int EducState_BallsChangeTtCounter = 0;

		// Token: 0x04001AC4 RID: 6852
		public static int EducState_HighDistanceTt = 0;

		// Token: 0x04001AC5 RID: 6853
		public static int EducState_GunShotCounter;

		// Token: 0x04001AC6 RID: 6854
		public static int EducState_OpenMapCounter;

		// Token: 0x04001AC7 RID: 6855
		public static bool EducState_CrewRatioToolTipShown;

		// Token: 0x04001AC8 RID: 6856
		public static bool LastDamageByPlayer;

		// Token: 0x04001AC9 RID: 6857
		public static bool FalkonedUsedThisSession;

		// Token: 0x04001ACA RID: 6858
		public static bool PowderKegsUsedThisSession;

		// Token: 0x04001ACB RID: 6859
		public static string LastDamage;

		// Token: 0x04001ACC RID: 6860
		public static bool LastDeathByMyself;

		// Token: 0x04001ACD RID: 6861
		public static Stopwatch TimeFromLastReceivedDamage;

		// Token: 0x04001ACE RID: 6862
		public static Stopwatch TimeFromLastKill;

		// Token: 0x04001ACF RID: 6863
		public static int LastSentCBDamageUID;

		// Token: 0x04001AD0 RID: 6864
		public static Stopwatch TimeFromLastSendedCBDamage;

		// Token: 0x04001AD1 RID: 6865
		public static Vector2? LastDeathPosition;

		// Token: 0x04001AD2 RID: 6866
		public static bool HasNotYetReviewedActions;

		// Token: 0x04001AD3 RID: 6867
		public static ApplyingEffectCache CurrentCrewJob;

		// Token: 0x04001AD4 RID: 6868
		public static SelectedCannonBalls LastLoadedNonEmptyCannonBalls;

		// Token: 0x04001AD5 RID: 6869
		public static ArenaGamesCount ArenaGames;

		// Token: 0x04001AD6 RID: 6870
		public static int LastOnline;

		// Token: 0x04001AD7 RID: 6871
		public static float LastPing;

		// Token: 0x04001AD8 RID: 6872
		public static bool IsFirstGameRun;

		// Token: 0x04001AD9 RID: 6873
		public static GroupCommon group;

		// Token: 0x04001ADB RID: 6875
		public static OnMinimapAndGroupUpdateMsg LastMinimapAndGroupUpdate = new OnMinimapAndGroupUpdateMsg(new Tlist<AllyStateTransfer>(), new Tlist<EnemyStateTransfer>(), new Tlist<MercanaryOrderTrackingInfo>(), 0);

		// Token: 0x04001ADC RID: 6876
		public static Vector3 MortarShotParamPowerupItem = new Vector3(0f, 0f, 1f);

		// Token: 0x04001ADD RID: 6877
		public static bool NotificationsTooltip;

		// Token: 0x04001ADE RID: 6878
		public static Vector2? WorldMapMarkerPosition;

		// Token: 0x04001ADF RID: 6879
		public static WeaponsReloading WReload = new WeaponsReloading();

		// Token: 0x04001AE0 RID: 6880
		public static bool SafeExitFlags = false;

		// Token: 0x04001AE1 RID: 6881
		public static bool UnreadLogbookFlag = false;

		// Token: 0x04001AE2 RID: 6882
		public static bool ForceResetPassword = false;

		// Token: 0x04001AE3 RID: 6883
		public static OnLoadGlobalMapResponse? LastMapResponse;

		// Token: 0x04001AE4 RID: 6884
		public static Stopwatch LastMapResponseElapsed = Stopwatch.StartNew();

		// Token: 0x04001AE5 RID: 6885
		public static int SouredResourcesCounter;

		// Token: 0x04001AE6 RID: 6886
		public static int DeadCrewCounter;

		// Token: 0x04001AE7 RID: 6887
		public static int AuctionSummaryTradePerSession = 0;

		// Token: 0x04001AE8 RID: 6888
		internal static TraderInSeaPoint LastTraderInSeaOffers;

		// Token: 0x04001AEA RID: 6890
		public static int RandomPvpZoneRewardAmount;

		// Token: 0x04001AEB RID: 6891
		public static int RandomPvpZoneRewardStep;

		// Token: 0x04001AEC RID: 6892
		public static int RandomPvpZoneKills;

		// Token: 0x04001AED RID: 6893
		public static Tlist<PublicDesignInfo> PlayerPublicDesigns = new Tlist<PublicDesignInfo>();

		// Token: 0x04001AEE RID: 6894
		public static Tlist<PublicDesignInfo> PublicDesignsCache = new Tlist<PublicDesignInfo>();

		// Token: 0x04001AEF RID: 6895
		public static GSI CurrentPortAuctionResources = new GSI();

		// Token: 0x04001AF1 RID: 6897
		public static float UsedCraftTimeSync;

		// Token: 0x04001AF2 RID: 6898
		public static int WhaleHarpoonShots;

		// Token: 0x04001AF3 RID: 6899
		public static bool HadReferalBonus;

		// Token: 0x04001AF4 RID: 6900
		public static float SecToServerReboot;

		// Token: 0x04001AF5 RID: 6901
		public static ResourceFlowAnalytics SaldoAnalytics = new ResourceFlowAnalytics();

		// Token: 0x020004EB RID: 1259
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001AF6 RID: 6902
			public static Action <0>__WhenNewDraftOpened;

			// Token: 0x04001AF7 RID: 6903
			public static Action<QuestEngine.CompletedQuestArgs> <1>__WhenCompleteQuest;

			// Token: 0x04001AF8 RID: 6904
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Action<QuestInfo> <2>__WhenFailQuest;

			// Token: 0x04001AF9 RID: 6905
			[Nullable(new byte[]
			{
				0,
				1,
				1
			})]
			public static Action<QuestInfo, QuestRunningProgress, bool> <3>__WhenQuestProgress;

			// Token: 0x04001AFA RID: 6906
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Action<QuestInfo> <4>__WhenRunQuest;

			// Token: 0x04001AFB RID: 6907
			public static Action <5>__WhenQuestsInteractiveDisablesRemoved;
		}
	}
}
