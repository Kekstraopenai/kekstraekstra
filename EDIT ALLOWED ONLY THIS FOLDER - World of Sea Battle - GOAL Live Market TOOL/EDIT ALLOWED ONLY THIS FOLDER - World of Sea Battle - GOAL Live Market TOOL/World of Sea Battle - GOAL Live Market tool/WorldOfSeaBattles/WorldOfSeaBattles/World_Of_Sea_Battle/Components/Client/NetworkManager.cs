using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Common;
using Common.Account;
using Common.Data;
using Common.Game;
using Common.Game.Console;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using HPCISockets.HighPacketLevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components.Architecture;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface.Animation;
using WorldOfSeaBattles;
using WorldOfSeaBattles.Interface.DebugPanel;
using WorldOfSeaBattles.Interface.PortPagesUi.Trade;
using World_Of_Sea_Battle.Components.Console;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Graphics.Effects;
using World_Of_Sea_Battle.Interface;
using WOS.Server.Core;

namespace World_Of_Sea_Battle.Components.Client
{
	// Token: 0x02000556 RID: 1366
	internal sealed class NetworkManager : IUpdateableObject
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x001167F5 File Offset: 0x001149F5
		public ConnectionModule Conection
		{
			get
			{
				return this.{25985};
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x001167FD File Offset: 0x001149FD
		public bool IsStarted
		{
			get
			{
				return this.{25985}.isStarted;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x0011680A File Offset: 0x00114A0A
		public bool IsCacheSynchronized
		{
			get
			{
				return this.{25979};
			}
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x00116814 File Offset: 0x00114A14
		public NetworkManager()
		{
			this.NetClient = new Networking();
			this.{25985} = new ConnectionModule(this.NetClient);
			this.NetClient.DisconnectionMessageHandler += delegate(OnDisconnectionFromServerMsg? {25943})
			{
				if (!this.IsStarted)
				{
					return;
				}
				bool {25940} = false;
				if ({25943} != null)
				{
					this.{25984} = {25943}.Value.Args.ToStringLocal();
					{25940} = {25943}.Value.IsWaitAccountToSync;
				}
				else
				{
					this.{25984} = Local.NetworkManager_0;
				}
				this.{25939}({25940}, this.{25984});
			};
			this.NetClient.OnReconnectionSuccess += delegate()
			{
			};
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x001168EB File Offset: 0x00114AEB
		public void Initialize()
		{
			this.CreateHandlers();
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x001168F4 File Offset: 0x00114AF4
		public void CreateHandlers()
		{
			this.NetClient.Routers.Add<OnLoginResultMsg>(new ReceiverCallback<OnLoginResultMsg>(this.{25944}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnStartStateMsg>(new ReceiverCallback<OnStartStateMsg>(this.{25946}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnPingMsg>(new ReceiverCallback<OnPingMsg>(this.{25948}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnMinimapAndGroupUpdateMsg>(delegate(ref OnMinimapAndGroupUpdateMsg {25993})
			{
				{19891} currentInstance = {19891}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.SetPositions({25993}.allies, {25993}.enemies);
				}
				{22913} currentInstance2 = {22913}.CurrentInstance;
				if (currentInstance2 != null)
				{
					currentInstance2.OnGroupMembersUpdate();
				}
				if ({22478}.CurrentInstance != null)
				{
					{22623}.Update({25993}.allies);
				}
				else
				{
					{22623} currentInstance3 = {22623}.CurrentInstance;
					if (currentInstance3 != null)
					{
						currentInstance3.RemoveFromContainer();
					}
				}
				Session.LastMinimapAndGroupUpdate = {25993};
				Session.Account.MercanaryQuestsTracking.RemoveAll((int {25994}) => !Session.LastMinimapAndGroupUpdate.trackingMercanaryOrders.Any((MercanaryOrderTrackingInfo {26146}) => {26146}.Order.ServerID == {25994}));
				Session.AvanpostModeRemainRespawns = (int){25993}.avanpostModeRemainRespawns;
				if ({25993}.allies.Any((AllyStateTransfer {25995}) => {25995}.ShipClass == byte.MaxValue && {25995}.uID > 0 && {25995}.CapturedShipInfo.AmmoInHold == 0))
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_NoCurrentAmmoInCapturedShip, true);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnUpdateServerWorldInformation>(delegate(ref OnUpdateServerWorldInformation {25996})
			{
				Session.ServerWorldStatus = {25996}.NewValue;
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnWeatherChanges>(delegate(ref OnWeatherChanges {25997})
			{
				if ({25997}.instanceId == 0)
				{
					if ({25997}.setAsInitialState)
					{
						CommonGlobal.WorldWeather.ClientSetInitialState({25997}.state);
					}
					else
					{
						CommonGlobal.WorldWeather.ClientSetState({25997}.state);
					}
				}
				if ({25997}.instanceId == 1)
				{
					if ({25997}.setAsInitialState)
					{
						CommonGlobal.ArenaWeather.ClientSetInitialState({25997}.state);
					}
					else
					{
						CommonGlobal.ArenaWeather.ClientSetState({25997}.state);
					}
				}
				if ({25997}.gameTimeIfChanged >= 0f)
				{
					Global.Render.GetSceneManager.WorldTime = {25997}.gameTimeIfChanged;
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnAcceptLocSetKeys>(new ReceiverCallback<OnAcceptLocSetKeys>(this.{25950}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnShipControllerChangeMsg>(delegate(ref OnShipControllerChangeMsg {25998})
			{
				Global.Game.WorldInstance.ApplShipMovePacket({25998});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnDebugSetMode>(delegate(ref OnDebugSetMode {25999})
			{
				OnDebugSetMode cx = {25999};
				Global.Game.WorldInstance.QueryAction<ShipOtherPlayer>({25999}.UID, delegate(ShipOtherPlayer {26147})
				{
					{26147}.DebugEnabled = cx.DebugEnabled;
				});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnCorrectMovMsg>(new ReceiverCallback<OnCorrectMovMsg>(this.{25952}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnExternalGPSChangeMsg>(new ReceiverCallback<OnExternalGPSChangeMsg>(this.{25954}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnAcceptPortStartMsg>(delegate(ref OnAcceptPortStartMsg {26000})
			{
				if (Global.Player.MapInfo.IsEducationMap)
				{
					Global.Player.EducationLeave();
				}
				Global.Player.Position = {26000}.PositionToSync.Position;
				Global.Player.Rotation = {26000}.PositionToSync.Rotation;
				Global.Game.ChangeSceneToPort(true, false);
				Session.CurrentPortAuctionResources = {26000}.CurrentPortAuctionResources;
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnPortEndMsg>(delegate(ref OnPortEndMsg {26001})
			{
				Global.Game.ScenePort.AcceptExit();
				Global.Player.Position = {26001}.ServerRespLHPosition.Position;
				Global.Player.Rotation = {26001}.ServerRespLHPosition.Rotation;
				if ({26001}.inLighthouse == 1)
				{
					Global.Player.OnExitWithProtection(Vector2.Zero, 0f, true);
				}
				Global.Settings.Logbook.Flush();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnChangeAccountPeropertyMsg>(delegate(ref OnChangeAccountPeropertyMsg {26002})
			{
				if ({26002}.Property == AccountProtectionProperty.Error_NameOrEmailIsUsed)
				{
					new {17312}(Local.NetworkManager_9);
					return;
				}
				new {17312}(Local.NetworkManager_7);
				if ({26002}.Property == AccountProtectionProperty.Name)
				{
					Session.Account.PlayerName = {26002}.NewValue;
					if (Session.Account.HasFreeNextChangeNickname)
					{
						Session.Account.HasFreeNextChangeNickname = false;
						Session.Account.Sanctions.RemoveSanction(SanctionType.BlockAllChats);
						return;
					}
					PlayerAccount account = Session.Account;
					account.Monets.Value = account.Monets.Value - Gameplay.ChangeNicknameCost.Value;
					Global.Shopstat("name_change", Gameplay.ChangeNicknameCost.Value);
					return;
				}
				else
				{
					if ({26002}.Property == AccountProtectionProperty.EMail)
					{
						Session.Account.EMail = {26002}.NewValue;
						return;
					}
					if ({26002}.Property == AccountProtectionProperty.Password)
					{
						Session.Account.PasswordHash = {26002}.NewValue;
					}
					return;
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnSetStartCharacterInfoResultMsg>(delegate(ref OnSetStartCharacterInfoResultMsg {26003})
			{
				{18738}.CurrentInstance.Response({26003});
				if ({26003}.NameIsValid && {26003}.ReferalFriendSIDIsValid)
				{
					Session.Account.PlayerName = {26003}.Name;
					Session.Account.ReferalFriendSID = {26003}.ReferalFriendSID;
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnSanctionAppliedMsg>(delegate(ref OnSanctionAppliedMsg {26004})
			{
				Session.Account.Sanctions.AddTime(ref {26004}.ServerTime, {26004}.AddedTime, {26004}.Type, {26004}.Message);
				string {860} = StringHelper.TimeDHM({26004}.AddedTime.TotalSeconds, false);
				SanctionType type = {26004}.Type;
				if (type != SanctionType.AccessBlocking && type - SanctionType.BlockGlobalChatOnline <= 1)
				{
					string text = Sanction.GetLocalizedReason({26004}.Message);
					if (!string.IsNullOrEmpty(text))
					{
						text = Local.NetworkManager_13_reason(text);
					}
					ChatRoomType {10417} = ChatRoomType.Special;
					string {10405} = {26004}.IsIndefinite ? Local.ChatBoxGui_7 : (Local.ChatBoxGui_7 + " (" + {26004}.SourceName + ")");
					string {10406};
					if (!{26004}.IsIndefinite)
					{
						string text2 = Local.NetworkManager_13(string.Empty, {860});
						{10406} = text2.Substring(1, text2.Length - 1) + text;
					}
					else
					{
						{10406} = Local.ChatBoxGui_10 + text;
					}
					ChatMessageDefault chatMessageDefault = new ChatMessageDefault({10405}, {10406}, 0U, LocaleInfo.Current.Id);
					OnChatMessageEvent onChatMessageEvent = new OnChatMessageEvent({10417}, ref chatMessageDefault);
					{22478}.Put(onChatMessageEvent);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnLocMendingStartOrStop>(delegate(ref OnLocMendingStartOrStop {26005})
			{
				if (Global.Player.IsMendingBegin)
				{
					Global.Player.StopMending(false);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnMendingLoopMsg>(delegate(ref OnMendingLoopMsg {26006})
			{
				Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({26006}.uID);
				if (shipFromUID == null)
				{
					return;
				}
				shipFromUID.ApplyOnMendingLoopMsg({26006});
				if ({26006}.uID == Global.Player.uID)
				{
					Session.EducState_ShipMendingHpCounter += {26006}.finalVal;
					if ({26006}.WoodCount != 0)
					{
						Global.Player.ResourcesOfHold.AddOrRemove(1, -{26006}.WoodCount);
					}
					if ({26006}.CottonCount != 0)
					{
						Global.Player.ResourcesOfHold.AddOrRemove(3, -{26006}.CottonCount);
					}
					if ({26006}.GoldCount != 0)
					{
						Session.Account.Gold -= {26006}.GoldCount;
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnSteamWheelCoalLoopMsg>(delegate(ref OnSteamWheelCoalLoopMsg {26007})
			{
				Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({26007}.uID);
				if (shipFromUID == null)
				{
					return;
				}
				shipFromUID.StartOrStopSteamWheelCoalUsing({26007}.stop);
				if ({26007}.uID == Global.Player.uID && {26007}.coalRemoveAmount != 0)
				{
					Global.Player.ResourcesOfHold.AddOrRemove(12, -{26007}.coalRemoveAmount);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnChatMessageEvent>(new ReceiverCallback<OnChatMessageEvent>(this.{25956}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnPowerupItemUse>(delegate(ref OnPowerupItemUse {26008})
			{
				PowerupItemInfo powerupItemInfo = Gameplay.PowerupItems.Array[(int){26008}.ItemID];
				int num = {26008}.ShipUIDs.Length;
				for (int i = 0; i < num; i++)
				{
					Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({26008}.ShipUIDs[i]);
					if (shipFromUID != null)
					{
						if (shipFromUID == Global.Player)
						{
							EducationHelper.OnPowerupItemUsed(powerupItemInfo);
							if ({26008}.goldMinus > 0)
							{
								Session.Account.Gold -= {26008}.goldMinus;
								{19994}.MeAndLogbook({19988}.Info, Local.lbe_bribe({26008}.goldMinus), null);
							}
							if ({26008}.stopItemEffect)
							{
								Session.Account.PowerupItemStop(powerupItemInfo, {26008}.slotIndex == byte.MaxValue);
							}
							else
							{
								Session.Account.PowerupItemAddCooldown(powerupItemInfo, Global.Player, (int){26008}.slotIndex);
								if (Session.CurrentCrewJob == null && Global.Player.DestructByTiltAmount == 0f)
								{
									Session.CurrentCrewJob = new ApplyingEffectCache(powerupItemInfo.Name, 1500f, null, null);
								}
							}
							if (Session.CurrentArenaSession != null && !{26008}.stopItemEffect && {26008}.slotIndex != 255)
							{
								int slotIndex = Global.Player.UsedShipPlayer.PowerupItemSlots.GetSlotIndex(powerupItemInfo);
								if (slotIndex != -1)
								{
									Session.CurrentArenaSession.PowerupItemUseLimit[slotIndex]--;
								}
							}
						}
						if ({26008}.stopItemEffect)
						{
							powerupItemInfo.Reset(shipFromUID);
						}
						else
						{
							PowerupItemEffectHelper.ApplyClient(shipFromUID, powerupItemInfo, {26008}.SourceShip, {26008}.slotIndex == byte.MaxValue);
						}
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnSendOrReceiveLSMsg>(new ReceiverCallback<OnSendOrReceiveLSMsg>(this.{25958}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnChangesEventActionsMsg>(delegate(ref OnChangesEventActionsMsg {26009})
			{
				Session.EventActionsPipeline.UpdateWaitActions({26009});
				for (int i = 0; i < (int){26009}.RemoveActionsArraySize; i++)
				{
					int {25266} = {26009}.RemoveActions[i];
					Session.EventActionsPipeline.ActionEndEvent({25266});
				}
				for (int j = 0; j < {26009}.AddActions.Size; j++)
				{
					Session.EventActionsPipeline.ActionBeginEvenet({26009}.AddActions.Array[j]);
				}
				if ({19779}.CurrentInstance == null)
				{
					Session.HasNotYetReviewedActions = true;
					return;
				}
				{19779}.CurrentInstance.UpdateEventActionsUi();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnSyncEACompleteMsg>(delegate(ref OnSyncEACompleteMsg {26010})
			{
				int aid = {26010}.AID;
				EventActionBase eventActionBase = Session.EventActionsPipeline.CurrentActions.Find((EventActionBase {26148}) => {26148}.AID == aid);
				((OneTimeBonus)eventActionBase.Behavior).Apply(Session.Account);
				{19779} currentInstance = {19779}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.UpdateEventActionsUi();
				}
				if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
				{
					Global.Game.ScenePort.ReloadPort();
				}
				{19994}.MeAndLogbook({19988}.Big_Gray, Local.lbe_bonus(eventActionBase.Name), null);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnArenaRatingCompleteMsg>(delegate(ref OnArenaRatingCompleteMsg {26011})
			{
				string text = string.Empty;
				string {20004} = string.Empty;
				string text2 = string.Empty;
				ComplexBonus reward;
				if ({26011}.ArenaRating == -2147483648)
				{
					reward = ArenaTournamentGameplay.GetReward({26011}.TournamentReward);
					text2 = (({26011}.TournamentReward == RatingRewardId.LuckyPlace) ? Local.NetworkManager_16_b : Local.NetworkManager_16({26011}.PlaceIndex + 1));
				}
				else
				{
					reward = ArenaRatingGameplay.GetReward({26011}.TournamentReward, {26011}.ArenaRating);
					text2 = Local.NetworkManager_17({26011}.PlaceIndex + 1);
				}
				reward.Apply(Session.Account, false, 1f);
				foreach (ComplexBonus.Annotation annotation in reward.DisplayText(1f, false))
				{
					text = text + ", " + annotation.Text;
				}
				{20004} = text2 + text;
				{19994}.MeAndLogbook({19988}.Big_Yellow, {20004}, text2, null);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnBonusCodeEntryResultReceived>(delegate(ref OnBonusCodeEntryResultReceived {26012})
			{
				if ({26012}.Entries.Size == 0)
				{
					new {17312}(Local.bonus_codes_window_wrong_code);
					return;
				}
				{19994}.Logbook(Local.bonus_codes_applied({26012}.Code) + ({26012}.Entries.Any((BonusCodeEntry {26013}) => {26013}.Type == BonusCodeEntryType.Resource) ? Local.bonus_codes_applied_storageInfo(Session.Account.NearPortStorageName) : ""), LBFlags.L0);
				StringBuilder stringBuilder = new StringBuilder();
				foreach (BonusCodeEntry bonusCodeEntry in ((IEnumerable<BonusCodeEntry>){26012}.Entries))
				{
					bonusCodeEntry.Apply(Session.Account, {26012}.ServerTime);
					{19994}.Logbook(bonusCodeEntry.GetUIText(), LBFlags.L0);
					stringBuilder.AppendLine(bonusCodeEntry.GetUIText());
				}
				new {19197}(stringBuilder.ToString());
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnLoadOneRatingOrTournament>(delegate(ref OnLoadOneRatingOrTournament {26014})
			{
				{22782} currentInstance = {22782}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.QueryResponse({26014});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnReferalNewStatusMsg>(new ReceiverCallback<OnReferalNewStatusMsg>(this.{25960}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnEducationStatusChangeMsg>(delegate(ref OnEducationStatusChangeMsg {26015})
			{
				{18593} currentInstance = {18593}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.ServerTaskCompleted();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnPlayerCapacityUpdate>(delegate(ref OnPlayerCapacityUpdate {26016})
			{
				OnPlayerCapacityUpdate m = {26016};
				Global.Game.WorldInstance.QueryAction<ShipNpc>({26016}.uID, delegate(ShipNpc {26149})
				{
					{26149}.Client.ItemsInHoldExemplary = m.ItemsInHoldExemplary;
				});
				Global.Game.WorldInstance.QueryAction<ShipOtherPlayer>({26016}.uID, delegate(ShipOtherPlayer {26150})
				{
					if (m.MaxSpeedWithoutCapacityFactor != 0f)
					{
						{26150}.RemoteInfo.UpdatePlayerMaxSpeed(m);
						{26150}.SetCapacityFactorRemote(m.capacityFactor);
					}
					{26150}.Client.ItemsInHoldExemplary = m.ItemsInHoldExemplary;
				});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGiveAchievement>(delegate(ref OnGiveAchievement {26017})
			{
				foreach (AchievementEnum achievementEnum in {26017}.AchievementID)
				{
					{19028}.QueueAchievementDisplay(achievementEnum);
					Session.Account.Achievements.AddSingle(achievementEnum);
					Steam.CheckAchievement(achievementEnum);
					ValueTuple<GSI, string> reward = WosbAchievements.GetReward(achievementEnum);
					WosbAchievements.AddReward(Global.Player, reward.Item1);
					bool flag = WosbAchievements.RewardAddToStorage(Global.Player) && !Global.Player.IsPortEntry;
					{19994}.Logbook(Local.reward_for_achievement(Gameplay.AchievementsByEnum[achievementEnum].Name), LBFlags.L1);
					foreach (GSILocalEnumerablePair<ResourceInfo> {5413} in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)reward.Item1.ResourceInfo))
					{
						{19994}.MeAndLogbook({19988}.GiveResources, {5413}.ToStringNC(true) + (flag ? (" (" + Local.in_storage_2 + ")") : ""), null);
					}
				}
				EducationHelper.MakeFlag(EducationOnboarding.GetAchievment, false);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnProfileQueryMsg>(delegate(ref OnProfileQueryMsg {26018})
			{
				{22746} currentInstance = {22746}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.RemoveFromContainer();
				}
				new {22746}({26018}.profile);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnUpdateCurrentCalendarEventDay>(delegate(ref OnUpdateCurrentCalendarEventDay {26019})
			{
				CalendarEvents.CurrentEvent.CurrentDayCached = {26019}.CurrentDay;
				CalendarEvents.CurrentEvent.SyncedFromServer = true;
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnServerRebootPlanned>(delegate(ref OnServerRebootPlanned {26020})
			{
				Session.SecToServerReboot = (float){26020}.SecToReboot;
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnShieldProtectorsEmpty>(delegate(ref OnShieldProtectorsEmpty {26021})
			{
				(Global.Game.WorldInstance.GetShipFromUID({26021}.UID) as ShipNpc).IsShieldActive = false;
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnNpcAsCaperStatusChangeMsg>(delegate(ref OnNpcAsCaperStatusChangeMsg {26022})
			{
				OnNpcAsCaperStatusChangeMsg msg = {26022};
				Global.Game.WorldInstance.QueryAction<ShipNpc>({26022}.NpcUID, delegate(ShipNpc {26151})
				{
					{26151}.UidPlayerForCaper = msg.uidPlayerForCaper;
					if ({26151}.UidPlayerForCaper != -1)
					{
						{26151}.IsShieldActive = false;
					}
					if (msg.uidPlayerForCaper == Global.Player.uID && Global.Player.MapInfo.IsWorldmap)
					{
						EducationHelper.MakeFlag(EducationOnboarding.GameTT_CaptureShip, true);
					}
					if (msg.mendingSailes)
					{
						{26151}.RestoreSailes(1f);
					}
					if (msg.mendingCorpusHpAmount > 0f)
					{
						{26151}.RestoreHp(msg.mendingCorpusHpAmount);
					}
					if (!string.IsNullOrEmpty(msg.setOwnerName))
					{
						{26151}.OwnerName = msg.setOwnerName;
					}
					{26151}.UsedShipNpc.SetBonusWhenCaptured();
					{26151}.OwnerFlags = msg.setOwnerFlags;
					{26151}.GetClient.Guild = msg.GuildInfo;
					if (msg.DestroyShip)
					{
						{26151}.MakeSimpleDamage(100000f);
					}
				});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnNpcAsCaperActionMsg>(delegate(ref OnNpcAsCaperActionMsg {26023})
			{
				if ({26023}.Action == OnNpcAsCaperActionMsg.Type.DestroyForResources)
				{
					Session.Account.NearPortStorage.Add({26023}.parameter);
					{19994}.Logbook(Local.lbe_ship_destroyed("NPS"), LBFlags.L1);
					foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){26023}.parameter.ResourceInfo))
					{
						{19994}.Logbook(gsilocalEnumerablePair.Info.Name + "+ " + gsilocalEnumerablePair.Count.ToString(), LBFlags.L1);
					}
				}
				if ({26023}.Action == OnNpcAsCaperActionMsg.Type.MoveAmmoFrom || {26023}.Action == OnNpcAsCaperActionMsg.Type.MoveAmmoTo || {26023}.Action == OnNpcAsCaperActionMsg.Type.MoveResFrom || {26023}.Action == OnNpcAsCaperActionMsg.Type.MoveResTo)
				{
					bool isInPortOrIsleWithStorage = Session.Game.IsInPortOrIsleWithStorage;
					if ({26023}.Action == OnNpcAsCaperActionMsg.Type.MoveResFrom)
					{
						(isInPortOrIsleWithStorage ? Session.Account.NearPortStorage : Global.Player.ResourcesOfHold).Add({26023}.parameter);
					}
					if ({26023}.Action == OnNpcAsCaperActionMsg.Type.MoveResTo)
					{
						(isInPortOrIsleWithStorage ? Session.Account.NearPortStorage : Global.Player.ResourcesOfHold).Remove({26023}.parameter, 1, false);
					}
					if ({26023}.Action == OnNpcAsCaperActionMsg.Type.MoveAmmoFrom)
					{
						(isInPortOrIsleWithStorage ? Session.Account.CBallsAtStorage : Global.Player.UsedShipPlayer.BallsOfHold).Add({26023}.parameter);
					}
					if ({26023}.Action == OnNpcAsCaperActionMsg.Type.MoveAmmoTo)
					{
						(isInPortOrIsleWithStorage ? Session.Account.CBallsAtStorage : Global.Player.UsedShipPlayer.BallsOfHold).Remove({26023}.parameter, 1, false);
					}
					Global.Player.UpdateCapacity();
					WosbTreasuryMaps.DetachMaps(Session.Account);
					{18214} {18214} = {17956}.CurrentInstance as {18214};
					if ({18214} != null)
					{
						{18214}.Sync({26023}.instanceOrNull);
						{18214}.Reload();
					}
				}
				if ({26023}.Action == OnNpcAsCaperActionMsg.Type.RequestStatusAndStop)
				{
					if ({17956}.CurrentInstance != null)
					{
						{17956}.CurrentInstance.RemoveFromContainer();
					}
					new {18214}({26023}.instanceOrNull);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnCreateShipsMsg>(delegate(ref OnCreateShipsMsg {26024})
			{
				for (int i = 0; i < {26024}.npcs.Size; i++)
				{
					Global.Game.WorldInstance.SpawnNpc({26024}.npcs.Array[i]);
				}
				for (int j = 0; j < {26024}.players.Size; j++)
				{
					Global.Game.WorldInstance.SpawnPlayer({26024}.players.Array[j]);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnCreateorUpdateDynamicBuildings>(delegate(ref OnCreateorUpdateDynamicBuildings {26025})
			{
				if (!{18560}.closed)
				{
					return;
				}
				Global.Game.InteractiveWorldSystem.CreateOrUpdateDyanmicBuildings({26025});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnRemoveObjectsFromSceneMsg>(delegate(ref OnRemoveObjectsFromSceneMsg {26026})
			{
				for (int i = 0; i < {26026}.removeUids.Size; i++)
				{
					Global.Game.WorldInstance.RemoveSVNObjectsFromScene({26026}.removeUids.Array[i]);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnLocAttackCorrectMsg>(delegate(ref OnLocAttackCorrectMsg {26027})
			{
				Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({26027}.shipUid);
				if (shipFromUID != null)
				{
					Vector3 newNormal = {26027}.resight;
					if (shipFromUID.ClientWeaponsShooting.ShotIsProcessing)
					{
						shipFromUID.ClientWeaponsShooting.ChangeRemainBuckets(delegate(CommonShotInfo {26152})
						{
							float scaleFactor = {26152}.StartNormalMultiplySpeed.Length();
							{26152}.StartNormalMultiplySpeed = scaleFactor * newNormal;
							return {26152};
						});
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnLocPlayerRespawnResultMsg>(delegate(ref OnLocPlayerRespawnResultMsg {26028})
			{
				Session.Account.Gold -= {26028}.payGold;
				if ({26028}.portId == -1)
				{
					Global.Player.TeleportMapChange(Gameplay.WorldMap);
				}
				Global.Player.RespawnMessageHandler({26028});
				if (!{26028}.entryToPort)
				{
					Global.Game.GetInterfaceManager.ClearAll();
					Global.Game.SceneGame.CreateInterface();
				}
				if ({26028}.setProtectionMode)
				{
					Global.Player.OnExitWithProtection({26028}.protectionZoneCenterR.XY(), {26028}.protectionZoneCenterR.Z, true);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnDestroyByFloodingSync>(delegate(ref OnDestroyByFloodingSync {26029})
			{
				Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({26029}.uID);
				if (shipFromUID == null)
				{
					return;
				}
				shipFromUID.ForceDestroy(true);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnCreateBallsMsg>(delegate(ref OnCreateBallsMsg {26030})
			{
				Global.Game.WorldInstance.AddCannonBalls(ref {26030});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnBallsCollisionMsg>(delegate(ref OnBallsCollisionMsg {26031})
			{
				for (int i = 0; i < {26031}.CollisionData.Size; i++)
				{
					Global.Game.WorldInstance.ApplyCannonBallCollision({26031}.CollisionData.Array[i]);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnFireAreaByServerCreate>(delegate(ref OnFireAreaByServerCreate {26032})
			{
				foreach (FireAreaSample fireAreaSample in ((IEnumerable<FireAreaSample>){26032}.Positions))
				{
					Global.Game.WorldInstance.ContactFireArea(fireAreaSample.Pos);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnFightAffectsMsg>(delegate(ref OnFightAffectsMsg {26033})
			{
				float wantedLevelNps = Session.Account.WantedLevelNps;
				Session.Account.WantedLevelNps = Math.Min(1f, Session.Account.WantedLevelNps + {26033}.increaseNpsWantedLevel);
				if ((wantedLevelNps < WosbNpcs.NpcWantedLevelDiff1 && Session.Account.WantedLevelNps >= WosbNpcs.NpcWantedLevelDiff1) || (wantedLevelNps < WosbNpcs.NpcWantedLevelDiff2 && Session.Account.WantedLevelNps >= WosbNpcs.NpcWantedLevelDiff2) || (wantedLevelNps < WosbNpcs.NpcWantedLevelDiff3 && Session.Account.WantedLevelNps >= WosbNpcs.NpcWantedLevelDiff3))
				{
					{19994}.Me({19988}.RedFlag, Local.notif_hh_1, Array.Empty<object>());
				}
				if ({26033}.increaseNpsWantedLevel > 0f)
				{
					Session.Account.StartWantedLevelNpsCooldown();
				}
				if (Session.Account.WantedLevelNps > 0.2f)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_WantedLevelNps, true);
				}
				foreach (PbsTensity.Guild guild in ((IEnumerable<PbsTensity.Guild>){26033}.tensityChanges))
				{
					string str = string.IsNullOrEmpty(guild.GuildTag) ? string.Empty : (" [" + guild.GuildTag + "]");
					float {284} = MathF.Round(100f * guild.Amount);
					{19994}.Me({19988}.Info, Local.tensity + str + ": " + StringHelper.pn({284}), Array.Empty<object>());
				}
				if ({26033}.changeReputation.Amount != 0f)
				{
					float num = Session.Account.Reputations.Change({26033}.changeReputation, Session.Account, Session.Guild);
					if ({26033}.changeReputation.Fraction == FractionID.TradeUnion)
					{
						{19994}.Me({19988}.Big_Fraction, string.Empty, new object[]
						{
							new {19989}(FractionID.Antilia, num),
							new {19989}(FractionID.Espaniol, num),
							new {19989}(FractionID.KaiAndSeveria, num)
						});
						return;
					}
					FractionID? fractionID;
					if (Session.Game.MapMyFraction == null || fractionID.GetValueOrDefault().IsNation())
					{
						{19994}.Me({19988}.Big_Fraction, string.Empty, new object[]
						{
							new {19989}({26033}.changeReputation.Fraction, num)
						});
						return;
					}
					if ({26033}.changeReputation.Fraction != Session.Guild.Fraction)
					{
						num = -num / (float)(FractionDecorator.Nations.Size - 1);
					}
					{19994}.Me({19988}.Big_Fraction, string.Empty, new object[]
					{
						new {19989}(Session.Guild.Fraction, num)
					});
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnFightBonusMsg>(delegate(ref OnFightBonusMsg {26034})
			{
				InGameSightUi.CurrentInstance.OnKill();
				if (Session.TimeFromLastKill != null)
				{
					Session.TimeFromLastKill.Restart();
				}
				else
				{
					Session.TimeFromLastKill = Stopwatch.StartNew();
				}
				CrewNotificationManager.WhenKillSomeone({26034});
				{26034}.Bonus.Apply(Session.Account);
				GSI gsi = Global.Player.ResourcesOfHold;
				gsi[37] = gsi[37] + {26034}.MarksBonus;
				gsi = Session.Account.TreasuryMaps;
				gsi[72] = gsi[72] + {26034}.BonusPirateMonet;
				if ({26034}.BlackMark)
				{
					Session.Account.AddBlackMark({26034}.KillCase, Session.Guild != null);
				}
				if ({26034}.TradingRouteId != -1)
				{
					Session.Account.OpenedTradingRoutes[{26034}.TradingRouteId] = true;
				}
				if ({26034}.KillCase != KillCase.Npc && {26034}.KillCase != KillCase.NpcLegendary)
				{
					EducationHelper.MakeFlag(EducationOnboarding.MakePvp, false);
				}
				if ({26034}.KillCase == KillCase.NpcLegendary)
				{
					EducationHelper.MakeFlag(EducationOnboarding.KillLegendShip, false);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnShipCollisionMsg>(delegate(ref OnShipCollisionMsg {26035})
			{
				Global.Game.WorldInstance.ApplyShipCollision(ref {26035}, !{26035}.IsIsle);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnRunFalkonetBallMsg>(delegate(ref OnRunFalkonetBallMsg {26036})
			{
				if ({26036}.SenderUID == Global.Player.uID && {26036}.Parts.Size > 0)
				{
					CannonBallInfo cannonBallInfo = Gameplay.BallsInfo.FromID((int){26036}.Parts.First().AmmoID);
					if (!cannonBallInfo.IsBoardingHook && !cannonBallInfo.Infinity)
					{
						Global.Player.UsedShipPlayer.BallsOfHold.AddOrRemove((int)cannonBallInfo.ID, -1);
					}
				}
				Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({26036}.SenderUID);
				if (shipFromUID != null)
				{
					shipFromUID.FalkonetShooting.Queued.Add({26036}.Parts);
					shipFromUID.FalkonetShooting.BeginGunEventCommon(shipFromUID, {26036}.serverRandomValue, null, new Action<Ship, FalkonetShotInfoRemote, bool>(Global.Game.WorldInstance.CreateFalkonetByCallback));
					Player player = shipFromUID as Player;
					if (player != null)
					{
						player.OnFalkonetOrKegShot();
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnAddComplexBonusToAccount>(delegate(ref OnAddComplexBonusToAccount {26037})
			{
				if ({26037}.Notify != null)
				{
					{19994}.MeAndLogbook({19988}.Info, {26037}.Notify.ToString(), new LBFlags?(LBFlags.L1));
				}
				{26037}.Bonus.Apply(Session.Account, true, 1f);
				foreach (ComplexBonus.Annotation annotation in {26037}.Bonus.DisplayText(1f, false))
				{
					{19988} {20000};
					switch (annotation.Key)
					{
					case ComplexBonus.AnnotationKey.Gold:
						{20000} = {19988}.Gold;
						break;
					case ComplexBonus.AnnotationKey.GoldForGuild:
						{20000} = {19988}.Gold;
						break;
					case ComplexBonus.AnnotationKey.Item:
						{20000} = {19988}.GiveResources;
						break;
					case ComplexBonus.AnnotationKey.Xp:
						{20000} = {19988}.GiveScrolls;
						break;
					case ComplexBonus.AnnotationKey.XpForShips:
						{20000} = {19988}.GiveScrolls;
						break;
					case ComplexBonus.AnnotationKey.LegendaryFlag:
					case ComplexBonus.AnnotationKey.Ship:
					case ComplexBonus.AnnotationKey.Achievement:
					case ComplexBonus.AnnotationKey.ConquerorBadges:
					case ComplexBonus.AnnotationKey.EducationFlag:
						goto IL_FC;
					case ComplexBonus.AnnotationKey.RandomDesign:
						{20000} = {19988}.GiveScrolls;
						break;
					case ComplexBonus.AnnotationKey.SpecificDesign:
						{20000} = {19988}.GiveScrolls;
						break;
					case ComplexBonus.AnnotationKey.Cannon:
						{20000} = {19988}.GiveResources;
						break;
					case ComplexBonus.AnnotationKey.Unit:
						{20000} = {19988}.GiveCrew;
						break;
					case ComplexBonus.AnnotationKey.UnknownUnit:
						{20000} = {19988}.GiveCrew;
						break;
					case ComplexBonus.AnnotationKey.Monets:
						{20000} = {19988}.Gold;
						break;
					case ComplexBonus.AnnotationKey.Ball:
						{20000} = {19988}.GiveResources;
						break;
					case ComplexBonus.AnnotationKey.PowderKeg:
						{20000} = {19988}.GiveResources;
						break;
					case ComplexBonus.AnnotationKey.HoldProtection:
						{20000} = {19988}.GiveScrolls;
						break;
					case ComplexBonus.AnnotationKey.Rank:
						{20000} = {19988}.GiveScrolls;
						break;
					case ComplexBonus.AnnotationKey.ArenaCurrency:
						{20000} = {19988}.GiveScrolls;
						break;
					default:
						goto IL_FC;
					}
					IL_FF:
					{19994}.MeAndLogbook({20000}, annotation.Text, null);
					continue;
					IL_FC:
					{20000} = {19988}.Info;
					goto IL_FF;
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnAddMoneyToAccount>(delegate(ref OnAddMoneyToAccount {26038})
			{
				PlayerAccount account = Session.Account;
				account.Monets.Value = account.Monets.Value + {26038}.Monets;
				Session.Account.Gold += {26038}.Gold;
				if ({26038}.itemsSign == 1)
				{
					Session.Account.NearPortStorage.Add({26038}.Items);
				}
				else
				{
					Session.Account.NearPortStorage.Remove({26038}.Items, 1, false);
				}
				if ({26038}.Gold > 0 || {26038}.Monets > 0)
				{
					string text = Local.NetworkManager_22 + (({26038}.Gold == 0) ? string.Empty : ({26038}.Gold.ToString() + "_" + Local.gold2)) + (({26038}.Monets == 0) ? string.Empty : (Local.NetworkManager_23 + {26038}.Monets.ToString() + ")"));
					{19994}.MeAndLogbook({19988}.Big_Gray, text, text, null);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnAddArenaRatingAndCurrency>(delegate(ref OnAddArenaRatingAndCurrency {26039})
			{
				PlayerAccount account = Session.Account;
				account.ArenaRating.Value = account.ArenaRating.Value + {26039}.Rating;
				PlayerAccount account2 = Session.Account;
				account2.ArenaCurrency.Value = account2.ArenaCurrency.Value + {26039}.Currency;
				if ({26039}.Rating > 0 || {26039}.Currency > 0)
				{
					string text = Local.NetworkManager_22 + (({26039}.Rating == 0) ? string.Empty : (Local.arena_rating + "_" + {26039}.Rating.ToString())) + (({26039}.Currency == 0) ? string.Empty : (Local.arena_currency + "_" + {26039}.Currency.ToString()));
					{19994}.MeAndLogbook({19988}.Big_Gray, text, text, null);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnRandomPvpZone>(delegate(ref OnRandomPvpZone {26040})
			{
				if (Session.IsInRandomPvpZone != {26040}.Inside)
				{
					if (Session.RandomPvpZoneRewardAmount != 0)
					{
						{19994}.Logbook(Local.bonus_for_pvp_zone({26040}.Gold.ToString() + " " + Local.gold2), LBFlags.L0);
					}
					Session.RandomPvpZoneRewardAmount = 0;
					Session.RandomPvpZoneKills = 0;
					Session.RandomPvpZoneRewardStep = 0;
				}
				Session.IsInRandomPvpZone = {26040}.Inside;
				if ({26040}.Gold > 0)
				{
					if ({26040}.Type == OnRandomPvpZone.RewardType.BeingInZone)
					{
						Session.RandomPvpZoneRewardStep = {26040}.Gold;
					}
					else if ({26040}.Type == OnRandomPvpZone.RewardType.DestructionOfEnemy)
					{
						Session.RandomPvpZoneKills++;
					}
					Session.RandomPvpZoneRewardAmount += {26040}.Gold;
					Session.Account.Gold += {26040}.Gold;
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnCreatePowderKegsMsg>(delegate(ref OnCreatePowderKegsMsg {26041})
			{
				Global.Game.WorldInstance.CreatePowderKeg(ref {26041});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnPowderKegExplosionMsg>(delegate(ref OnPowderKegExplosionMsg {26042})
			{
				Global.Game.WorldInstance.PowderKegExplosion(ref {26042});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnMortarAttackMsg>(delegate(ref OnMortarAttackMsg {26043})
			{
				Global.Game.WorldInstance.CreateMortarShot({26043});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnShipsSomeDamageMsg>(delegate(ref OnShipsSomeDamageMsg {26044})
			{
				for (int i = 0; i < {26044}.CollisionData.Size; i++)
				{
					Global.Game.WorldInstance.ApplyGeneralDamage({26044}.CollisionData.Array[i], {26044}.CollisionData.Array[i].SourcePawnOrShipUID);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnNotificationMsg>(delegate(ref OnNotificationMsg {26045})
			{
				if ({26045}.MeAndLogbook)
				{
					{19994}.MeAndLogbook({19988}.Info, {26045}.Notification.ToString(), new LBFlags?(LBFlags.L1));
				}
				else
				{
					{19994}.Logbook({26045}.Notification.ToString(), LBFlags.L1);
				}
				Session.UnreadLogbookFlag = true;
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnSetAggressorFlag>(delegate(ref OnSetAggressorFlag {26046})
			{
				ShipOtherPlayer shipOtherPlayer = Global.Game.WorldInstance.GetShipFromUID({26046}.ShipUID) as ShipOtherPlayer;
				if (shipOtherPlayer != null)
				{
					shipOtherPlayer.SetAgressorFlag({26046}.AggressorFlag);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnLocBoardingQueryError>(delegate(ref OnLocBoardingQueryError {26047})
			{
				{19994}.Me({19988}.Info, {26047}.reason.ToStringLocal(), Array.Empty<object>());
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnBoardingRun>(delegate(ref OnBoardingRun {26048})
			{
				Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({26048}.sourceUID);
				Ship shipFromUID2 = Global.Game.WorldInstance.GetShipFromUID({26048}.targetUID);
				Player player = shipFromUID as Player;
				if (player != null)
				{
					player.BeginBattleTimer(true, false);
				}
				Player player2 = shipFromUID2 as Player;
				if (player2 != null)
				{
					player2.BeginBattleTimer(true, false);
				}
				if (shipFromUID != null && shipFromUID2 != null)
				{
					Ship.GetCloseToTheShip(shipFromUID, shipFromUID2);
					new BoardingFSEffect(shipFromUID, shipFromUID2, 0f);
				}
				if ({26048}.targetUID == Global.Player.uID || {26048}.sourceUID == Global.Player.uID)
				{
					if ({18139}.CurrentInstance != null)
					{
						{18139}.CurrentInstance.RemoveFromContainer();
					}
					if ({26048}.targetUID == Global.Player.uID && shipFromUID != null)
					{
						new {18139}(shipFromUID);
					}
					if ({26048}.sourceUID == Global.Player.uID && shipFromUID2 != null)
					{
						new {18139}(shipFromUID2);
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnBoardingRealTimeChangesMsg>(delegate(ref OnBoardingRealTimeChangesMsg {26049})
			{
				{18139} currentInstance = {18139}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.ApplyAndDisplayChanges({26049});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnSomeBoardingFail>(delegate(ref OnSomeBoardingFail {26050})
			{
				if ({26050}.SourceUID == Global.Player.uID || {26050}.TargetUID == Global.Player.uID)
				{
					if ({18139}.CurrentInstance != null)
					{
						{19994}.Me({19988}.Info, Local.NetworkManager_24, Array.Empty<object>());
					}
					{18139} currentInstance = {18139}.CurrentInstance;
					if (currentInstance != null)
					{
						currentInstance.RemoveFromContainer();
					}
				}
				Global.Game.WorldInstance.DisconnectBoardingHooks({26050}.SourceUID);
				for (int i = 0; i < BoardingFSEffect.ActiveEffects.Size; i++)
				{
					BoardingFSEffect.ActiveEffects.Array[i].CheckAndQueueToRemove({26050}.SourceUID, {26050}.TargetUID);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnFortRobbingFinished>(new ReceiverCallback<OnFortRobbingFinished>(this.{25962}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnBoardingFinishFinalPart>(new ReceiverCallback<OnBoardingFinishFinalPart>(this.{25964}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnCrewChangesMsg>(new ReceiverCallback<OnCrewChangesMsg>(this.{25966}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnSetBoardingMode>(delegate(ref OnSetBoardingMode {26051})
			{
				{18139}.CurrentInstance.SwitchToWaitActionMode(!{26051}.hideUiOnly);
				if ({26051}.hideUiOnly)
				{
					return;
				}
				Tlist<{17443}> tlist = new Tlist<{17443}>();
				{17443} {17443};
				if ({26051}.isShipNpc)
				{
					Tlist<{17443}> tlist2 = tlist;
					{17443} = new {17443}(Local.NetworkManager_50, Local.NetworkManager_51_np, {17312}.cIconShipFire, false, 0f);
					tlist2.Add({17443});
				}
				else
				{
					Tlist<{17443}> tlist3 = tlist;
					{17443} = new {17443}(Local.NetworkManager_50, Local.NetworkManager_51, {17312}.cIconShipFire, false, 0f);
					tlist3.Add({17443});
				}
				Tlist<{17443}> tlist4 = tlist;
				{17443} = new {17443}(Local.NetworkManager_52, ({26051}.targetManOfCrewAvailableBondman == 0) ? Local.NetworkManager_53_onlyCrew({26051}.targetManOfCrewAvailableUnits) : Local.NetworkManager_53({26051}.targetManOfCrewAvailableUnits, {26051}.targetManOfCrewAvailableBondman), {17312}.cIconShield, {26051}.targetManOfCrewAvailableUnits + {26051}.targetManOfCrewAvailableBondman == 0, 0f);
				tlist4.Add({17443});
				if ({26051}.allowCapture)
				{
					Tlist<{17443}> tlist5 = tlist;
					{17443} = new {17443}(Local.capture, Local.NetworkManager_56({26051}.remainingCaptureLimit), {17312}.cIconSpyglass, {26051}.remainingCaptureLimit == 0, 0f);
					tlist5.Add({17443});
				}
				bool allowCaptureCached = {26051}.allowCapture;
				{17312} {17312} = new {17312}({26051}.isDestroyFullUnits ? Local.NetworkManager_59 : Local.NetworkManager_60, delegate(int {26153})
				{
					Global.Network.Send(new OnSetBoardingMode(({26153} == 0) ? BoardingMode.RobAndDestroy : (({26153} == 1) ? BoardingMode.ManTheCrew : (allowCaptureCached ? BoardingMode.CaptureNpc : BoardingMode.RobAndDestroy))));
				}, tlist.ToArray());
				{17312}.TimeoutMs = 14000f;
				{17312}.MissclickProtection = 900f;
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnBoardingResightingOrProtectMsg>(delegate(ref OnBoardingResightingOrProtectMsg {26052})
			{
				{18139} currentInstance = {18139}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.EnemyStatusChange({26052});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnAcceptLocGroupInviteMsg>(delegate(ref OnAcceptLocGroupInviteMsg {26053})
			{
				switch ({26053}.QueryResult)
				{
				case CreateGroupQueryResult.Error_Already:
					{19994}.Me({19988}.Info, Local.NetworkManager_61, Array.Empty<object>());
					return;
				case CreateGroupQueryResult.Error_NotFound:
					{19994}.Me({19988}.Info, Local.NetworkManager_62, Array.Empty<object>());
					return;
				case CreateGroupQueryResult.Error_Blacklist:
					{19994}.Me({19988}.Info, Local.StringConstants_1, Array.Empty<object>());
					return;
				default:
					throw new NotSupportedException();
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGroupInviteMsg>(delegate(ref OnGroupInviteMsg {26054})
			{
				if (string.IsNullOrEmpty({26054}.Name))
				{
					Global.Game.WorldInstance.QueryAction<ShipOtherPlayer>({26054}.SourceUID, delegate(ShipOtherPlayer {26154})
					{
						{26054}.Name = {26154}.Client.GetRealName();
					});
				}
				{19994}.Invite(Local.NetworkManager_64({26054}.Name), delegate
				{
					Global.Network.Send(new OnTakeGroupQueryMsg({26054}.SourceUID, true));
				}, new Keys?(Global.Settings.kb_Accept.Key), new Keys?(Global.Settings.kb_Undo.Key), 25000f);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnTakeGroupErrorMsg>(delegate(ref OnTakeGroupErrorMsg {26055})
			{
				switch ({26055}.ErrorType)
				{
				case TakeGroupErrorType.NoAvailablePlaces:
					{19994}.Me({19988}.Info, Local.NetworkManager_66, Array.Empty<object>());
					return;
				case TakeGroupErrorType.GroupNotFound:
					{19994}.Me({19988}.Info, Local.NetworkManager_65, Array.Empty<object>());
					return;
				case TakeGroupErrorType.QueryTimeout:
					{19994}.Me({19988}.Info, Local.NetworkManager_67, Array.Empty<object>());
					return;
				default:
					throw new NotSupportedException();
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGMSetMsg>(delegate(ref OnGMSetMsg {26056})
			{
				if ({26056}.Info == null)
				{
					{17539} currentInstance = {17539}.CurrentInstance;
					if (currentInstance != null)
					{
						currentInstance.RemoveFromContainer();
					}
					Session.Group = null;
					{19994}.Me({19988}.Info, Local.NetworkManager_68, Array.Empty<object>());
					return;
				}
				Session.Group = {26056}.Info;
				if (Global.Player != null)
				{
					EducationHelper.MakeFlag(EducationOnboarding.CreateOrJoinGroup, true);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGMAddMsg>(delegate(ref OnGMAddMsg {26057})
			{
				if (Session.Group != null)
				{
					Session.Group.Members.Add({26057}.AddedMember);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGMRemoveMsg>(delegate(ref OnGMRemoveMsg {26058})
			{
				if ({26058}.RemovedUID == Global.Player.uID)
				{
					{17539} currentInstance = {17539}.CurrentInstance;
					if (currentInstance != null)
					{
						currentInstance.RemoveFromContainer();
					}
					Session.Group = null;
					return;
				}
				Session.Group.RemoveMember({26058}.RemovedUID);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnSendGroupCommandMsg>(delegate(ref OnSendGroupCommandMsg {26059})
			{
				{22478} currentInstance = {22478}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.SpecialMessage({26059}.MessageId, {26059}.SourceNameOrNull, {26059}.TargetName);
				}
				GroupCommandInfo groupCommandInfo = new GroupCommandInfo({26059}.MessageId);
				if (groupCommandInfo.NpcCommand)
				{
					return;
				}
				if (!groupCommandInfo.NpcCommand)
				{
					ShipOtherPlayer shipOtherPlayer = Global.Game.WorldInstance.GetShipFromUID({26059}.SourceUID) as ShipOtherPlayer;
					if (shipOtherPlayer != null)
					{
						shipOtherPlayer.Client.ShowTextMessage = new TemporaryEffect<string>(groupCommandInfo.DisplayName + (groupCommandInfo.HasTarget ? {26059}.TargetName : ""), 6000f);
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGuildActionLogRecord>(delegate(ref OnGuildActionLogRecord {26060})
			{
				if (Session.Guild != null)
				{
					Session.Guild.Log.Add({26060}.LogRecord);
					if (Session.Guild.Log.Size >= 200)
					{
						Session.Guild.Log.RemoveRange(0, 10);
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnSearchGuildQueryResult>(delegate(ref OnSearchGuildQueryResult {26061})
			{
				if ({26061}.isInvite)
				{
					if ({26061}.restrictionTimeout != -1L)
					{
						new {17312}(Local.NetworkManager_69 + new LocalizedDateTime({26061}.restrictionTimeout, false).GetDateAndTime());
						return;
					}
					{19994}.MeAndLogbook({19988}.Big_Yellow, Local.lbe_guild_equest({26061}.Tag), new LBFlags?(LBFlags.L0));
					return;
				}
				else
				{
					{20143} currentInstance = {20143}.CurrentInstance;
					if (currentInstance == null)
					{
						return;
					}
					currentInstance.Response({26061});
					return;
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGuildRatingActionMsg>(delegate(ref OnGuildRatingActionMsg {26062})
			{
				{20143} currentInstance = {20143}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.Response(new OnGuildRatingActionMsg?({26062}));
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnCreateGuildQueryResult>(delegate(ref OnCreateGuildQueryResult {26063})
			{
				{20143}.ChangeStatus({26063}.Status);
				if ({26063}.Status == CreateGuildRequestStatus.Completed)
				{
					Session.Guild = {26063}.Guild;
					Session.Account.Gold -= GuildCommon.GuildCreateCostGold({26063}.Guild.IsFlotilia, false).Value;
					new {20364}();
					{22478} currentInstance = {22478}.CurrentInstance;
					if (currentInstance != null)
					{
						currentInstance.GuildChanged();
					}
					Session.Account.Reputations.ChangeAfterEnterToGuild(Session.Account, {26063}.Guild);
				}
				if ({26063}.Status == CreateGuildRequestStatus.TimeRestriction)
				{
					new {17312}(Local.NetworkManager_72 + new LocalizedDateTime({26063}.TimeRestrictionEnd, false).GetDateAndTime());
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnChangeGuldTextProperty>(delegate(ref OnChangeGuldTextProperty {26064})
			{
				Session.Guild.UpdateTextField({26064});
				{20364} currentInstance = {20364}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.Reload();
				}
				if ({20364}.CurrentInstance == null)
				{
					{20500}.CheckGuildAnnouncment();
					return;
				}
				Global.Settings.SavedGuildAnnouncmentVersion = Session.Guild.CurrentAnnouncment.Item2;
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGuildMembersChanges>(delegate(ref OnGuildMembersChanges {26065})
			{
				if (Session.Guild == null)
				{
					return;
				}
				if (({26065}.Action == OnGuildMembersChanges.GAction.RemoveMember && Session.Account.SID == {26065}.PlayerSid) || {26065}.Action == OnGuildMembersChanges.GAction.DestroyGuild)
				{
					string text = Local.lbe_guild_removed(Session.Guild.Tag);
					if ({26065}.Arg == 1)
					{
						text = text + ": " + ((Session.Guild.Fraction == FractionID.TradeUnion) ? Local.kick_guild_reputation_trade : Local.kick_guild_reputation_nation);
					}
					{20364} currentInstance = {20364}.CurrentInstance;
					if (currentInstance != null)
					{
						currentInstance.RemoveFromContainer();
					}
					{19994}.MeAndLogbook({19988}.Big_Red, text, null);
					bool isFlotilia = Session.Guild.IsFlotilia;
					Session.Guild = null;
					Session.Account.Reputations.ChangeAfterExitOfGuild(Session.Account, Session.Guild, isFlotilia);
					{22478} currentInstance2 = {22478}.CurrentInstance;
					if (currentInstance2 == null)
					{
						return;
					}
					currentInstance2.GuildChanged();
					return;
				}
				else
				{
					if ({26065}.Action == OnGuildMembersChanges.GAction.RemoveMember)
					{
						Session.Guild.RemoveMemberAndUpdate({26065}.PlayerSid);
					}
					if ({26065}.Action == OnGuildMembersChanges.GAction.AcceptInvite)
					{
						Session.Guild.AddMemberByInvite({26065}.PlayerSid, {26065}.Cached);
					}
					if ({26065}.Action == OnGuildMembersChanges.GAction.RemoveInvite)
					{
						Session.Guild.TryRemoveInvite({26065}.PlayerSid);
					}
					if ({26065}.Action == OnGuildMembersChanges.GAction.AddInvite)
					{
						Session.Guild.AddInvite(new GuildInvite({26065}.PlayerSid, {26065}.Cached.Name, 604800.0, {26065}.Parameter));
					}
					if ({26065}.Action == OnGuildMembersChanges.GAction.SetRoleMember)
					{
						if ((int){26065}.Arg == GuildAccessRightsInfo.FounderRights.RankId)
						{
							Session.Guild.GetFounder.RankId = (byte)(GuildAccessRightsInfo.FounderRights.RankId - 1);
							{19994}.MeAndLogbook({19988}.Big_Yellow, Local.NetworkManager_77 + Session.Guild.GetMember({26065}.PlayerSid).Cached.Name, null);
						}
						Session.Guild.GetMember({26065}.PlayerSid).RankId = {26065}.Arg;
						if ({26065}.PlayerSid == Session.Account.SID)
						{
							{19994}.MeAndLogbook({19988}.Big_Red, Local.lbe_guild_new_role(Session.Guild.GetMember({26065}.PlayerSid).Rights.DisplayName), null);
						}
					}
					if ({26065}.Action == OnGuildMembersChanges.GAction.ResetFields)
					{
						Session.Guild.ResetCustomFields(true, true, true);
					}
					if ({26065}.Action == OnGuildMembersChanges.GAction.UpgradeFlotilia)
					{
						Session.Guild.SetFraction((FractionID){26065}.Arg, Session.EventActionsPipeline);
						Session.Guild.UpdateName({26065}.Parameter);
						if ({20143}.CurrentInstance != null || {20364}.CurrentInstance != null)
						{
							{20143} currentInstance3 = {20143}.CurrentInstance;
							if (currentInstance3 != null)
							{
								currentInstance3.RemoveFromContainer();
							}
							{20364} currentInstance4 = {20364}.CurrentInstance;
							if (currentInstance4 != null)
							{
								currentInstance4.RemoveFromContainer();
							}
							new {20364}();
						}
					}
					if ({26065}.Action == OnGuildMembersChanges.GAction.PbCaptureModeChanged)
					{
						Session.Guild.PbCaptureMode = (PbCaptureMode){26065}.Arg;
					}
					{20364} currentInstance5 = {20364}.CurrentInstance;
					if (currentInstance5 == null)
					{
						return;
					}
					currentInstance5.Reload();
					return;
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnYourAcceptedToGuild>(delegate(ref OnYourAcceptedToGuild {26066})
			{
				Session.Guild = {26066}.Cache;
				{19994}.MeAndLogbook({19988}.Big_Red, Local.lbe_guild_accepted({26066}.Cache.Tag), null);
				if ({20143}.CurrentInstance != null)
				{
					{20143}.CurrentInstance.RemoveFromContainer();
					new {20364}();
				}
				Session.Account.Reputations.ChangeAfterEnterToGuild(Session.Account, {26066}.Cache);
				{22478} currentInstance = {22478}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.GuildChanged();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnChangeGuildMintOrTreasuryMsg>(delegate(ref OnChangeGuildMintOrTreasuryMsg {26067})
			{
				if ({26067}.isTreasury)
				{
					Session.Guild.Treasury = {26067}.Argument;
					return;
				}
				if ({26067}.Argument == -1)
				{
					{19994}.Me({19988}.Big_Gray, Local.NetworkManager_75, Array.Empty<object>());
					return;
				}
				if ({26067}.targetSID == Session.Account.SID)
				{
					Session.Account.Gold += {26067}.Argument;
					{19994}.MeAndLogbook({19988}.Big_Yellow, Local.notf_mintshare({26067}.Argument), new LBFlags?(LBFlags.L1));
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnChangeGuildHqPort>(delegate(ref OnChangeGuildHqPort {26068})
			{
				if (Session.Guild != null)
				{
					if ({26068}.openOrDestroy)
					{
						Session.Guild.TryMakeEffect(GuildTemporaryEffect.Type.HqOrLicenseInPort, "", {26068}.portId, Session.EventActionsPipeline);
					}
					else
					{
						Session.Guild.TryRemoveHqOrLicense((int){26068}.portId);
					}
				}
				{20364} currentInstance = {20364}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.Reload();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnMakeGuildEffect>(delegate(ref OnMakeGuildEffect {26069})
			{
				if (Session.Guild != null)
				{
					if ({26069}.remove)
					{
						Session.Guild.TryRemoveEffect({26069}.Category, {26069}.argument, new byte?({26069}.argument2));
					}
					else
					{
						Session.Guild.TryMakeEffect({26069}.Category, {26069}.argument, {26069}.argument2, Session.EventActionsPipeline);
					}
					{20364} currentInstance = {20364}.CurrentInstance;
					if (currentInstance == null)
					{
						return;
					}
					currentInstance.Reload();
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGuildImage>(delegate(ref OnGuildImage {26070})
			{
				Session.LoadedGuildIconTexture = VisualHelper.LoadTexture2DFromBytes({26070}.Data, int.MaxValue, false);
				GuildCommon guild = Session.Guild;
				if (guild != null)
				{
					guild.UpdateImage({26070}.Data);
				}
				{20364} currentInstance = {20364}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.UpdateImage();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGuildFractionSetMsg>(delegate(ref OnGuildFractionSetMsg {26071})
			{
				if (Session.Guild == null)
				{
					return;
				}
				if ({26071}.status == OnGuildFractionSetMsg.Status.BalanceError)
				{
					new {17312}(Local.fr_change_balanceerror);
				}
				if ({26071}.status == OnGuildFractionSetMsg.Status.ActivePbError)
				{
					new {17312}(Local.fr_change_activepberror);
					return;
				}
				FractionID fraction = Session.Guild.Fraction;
				Session.Guild.SetFraction({26071}.NewFraction, Session.EventActionsPipeline);
				Session.Account.Reputations.ChangeAfterEnterToGuild(Session.Account, Session.Guild);
				{20364} currentInstance = {20364}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.Reload();
				}
				{19994}.MeAndLogbook({19988}.Big_Red, {26071}.imposter ? Local.fraction_changed_imposter(fraction.GetName()) : Local.fraction_changed(Session.Guild.Fraction.GetName()), null);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGuildRulesChanged>(delegate(ref OnGuildRulesChanged {26072})
			{
				if (Session.Guild == null)
				{
					return;
				}
				Session.Guild.ChangeTrustedGuilds({26072});
				{20364} currentInstance = {20364}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.Reload();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGuildSalaryAction>(delegate(ref OnGuildSalaryAction {26073})
			{
				if (Session.Guild == null)
				{
					return;
				}
				{20364} currentInstance = {20364}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.Reload();
				}
				if ({26073}.amount > 0)
				{
					{19994}.MeAndLogbook({19988}.Gold, Local.lbe_guildsalary({26073}.amount), null);
					Session.Account.Gold += {26073}.amount;
				}
				Session.GuildUnreceivedSalary = 0;
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnOpenTradeHq>(delegate(ref OnOpenTradeHq {26074})
			{
				if (Session.Guild == null)
				{
					return;
				}
				Session.Guild.TryMakeEffect(GuildTemporaryEffect.Type.HqOrLicenseInPort, "", {26074}.PortId, Session.EventActionsPipeline);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnLoadGlobalMapResponse>(delegate(ref OnLoadGlobalMapResponse {26075})
			{
				{22913} currentInstance = {22913}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.SetupState({26075});
				}
				Session.LastMapResponse = new OnLoadGlobalMapResponse?({26075});
				Session.LastMapResponseElapsed.Restart();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnSyncGuildResourcesAndIngouts>(delegate(ref OnSyncGuildResourcesAndIngouts {26076})
			{
				if (Session.Guild == null)
				{
					return;
				}
				Session.Guild.Treasury = {26076}.Ingouts;
				Session.Guild.Mint = {26076}.mint;
				Session.Guild.CachedSupplyMissionCount = {26076}.supplyMissionProgress;
				Session.Guild.SetConquerBadgesCountDirect({26076}.conquerBadges);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnAddGuildTitle>(delegate(ref OnAddGuildTitle {26077})
			{
				if (Session.Guild == null)
				{
					return;
				}
				if ({26077}.UpdateCurrent)
				{
					Session.Guild.PotentialTitleId = {26077}.TitleId;
					return;
				}
				GSI receivedTitles = Session.Guild.ReceivedTitles;
				int titleId = (int){26077}.TitleId;
				int num = receivedTitles[titleId];
				receivedTitles[titleId] = num + 1;
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnUpdatePbTimeoutOrStatus>(delegate(ref OnUpdatePbTimeoutOrStatus {26078})
			{
				if (Session.Guild == null)
				{
					return;
				}
				Session.PortBattleInfo = {26078};
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<ObPbsBattleResultsMsg>(delegate(ref ObPbsBattleResultsMsg {26079})
			{
				ArenaBattleResult arenaBattleResult;
				if ({26079}.engageInBattle == PbsBatlleSide.Attacker)
				{
					arenaBattleResult = ({26079}.isFortCaptured ? ArenaBattleResult.YourComandWin : ({26079}.isFortRobbed ? ArenaBattleResult.Tie : ArenaBattleResult.YourComandFail));
				}
				else
				{
					arenaBattleResult = ({26079}.isFortCaptured ? ArenaBattleResult.YourComandFail : ({26079}.isFortRobbed ? ArenaBattleResult.Tie : ArenaBattleResult.YourComandWin));
				}
				string text;
				if (!{26079}.isFortRobbed)
				{
					text = "";
				}
				else
				{
					string fort_was_robbed = Local.fort_was_robbed;
					string str;
					if ({26079}.robbedMint <= 0)
					{
						str = "";
					}
					else
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
						defaultInterpolatedStringHandler.AppendLiteral(" +");
						defaultInterpolatedStringHandler.AppendFormatted<int>({26079}.robbedMint);
						defaultInterpolatedStringHandler.AppendLiteral(" ");
						defaultInterpolatedStringHandler.AppendFormatted(Local.gold2);
						str = defaultInterpolatedStringHandler.ToStringAndClear();
					}
					text = fort_was_robbed + str;
				}
				string {19059} = text;
				string portName = Gameplay.WorldMap.Ports.Array[(int){26079}.portId].PortName;
				if (arenaBattleResult == ArenaBattleResult.YourComandWin)
				{
					{19994}.Logbook(Local.lbe_pb_win(portName), LBFlags.L1);
				}
				else if (arenaBattleResult == ArenaBattleResult.YourComandFail)
				{
					{19994}.Logbook(Local.lbe_pb_lost(portName), LBFlags.L1);
				}
				else
				{
					{19994}.Logbook(Local.lbe_pb_tie(portName), LBFlags.L1);
				}
				ArenaPlayerShowing arenaPlayerShowing;
				if (!{26079}.mySide.TryFind((ArenaPlayerShowing {26080}) => {26080}.PlayerName == Session.Account.PlayerName, out arenaPlayerShowing))
				{
					if (!{26079}.enemySide.TryFind((ArenaPlayerShowing {26081}) => {26081}.PlayerName == Session.Account.PlayerName, out arenaPlayerShowing))
					{
						Assert.Report(true, "myShowing is null, ObPbsBattleResultsMsg");
					}
				}
				new {19037}(null, new ArenaPersonalResults
				{
					Result = arenaBattleResult,
					Bonus = arenaPlayerShowing.ComputedServerReward
				}, {26079}.mySide, {26079}.enemySide, {19059}, "", {26079}.addConquestBadgesToMyGuild, 0, 0, null, portName + ", " + arenaBattleResult.ToStrLocal() + "!");
				arenaPlayerShowing.ComputedServerReward.Apply(Session.Account);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnBuildingHoldOperatingMsg>(delegate(ref OnBuildingHoldOperatingMsg {26082})
			{
				if ({26082}.changes_isAdd)
				{
					Global.Player.ResourcesOfHold.Remove({26082}.changes, 1, false);
					Global.Player.UsedShipPlayer.BallsOfHold.Remove({26082}.changeBalls, 1, false);
					foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){26082}.changes.ResourceInfo))
					{
						{19994}.Logbook(Local.lbe_moved_tofort(gsilocalEnumerablePair.Info.Name, gsilocalEnumerablePair.Count), LBFlags.L0);
					}
					using (IEnumerator<GSILocalEnumerablePair<CannonBallInfo>> enumerator2 = ((IEnumerable<GSILocalEnumerablePair<CannonBallInfo>>){26082}.changeBalls.CannonBallInfo).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							GSILocalEnumerablePair<CannonBallInfo> gsilocalEnumerablePair2 = enumerator2.Current;
							{19994}.Logbook(Local.lbe_moved_tofort(gsilocalEnumerablePair2.Info.Name, gsilocalEnumerablePair2.Count), LBFlags.L0);
						}
						goto IL_1DF;
					}
				}
				Global.Player.ResourcesOfHold.Add({26082}.changes);
				Global.Player.UsedShipPlayer.BallsOfHold.Add({26082}.changeBalls);
				foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair3 in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){26082}.changes.ResourceInfo))
				{
					{19994}.Logbook(Local.lbe_moved_fromfort(gsilocalEnumerablePair3.Info.Name, gsilocalEnumerablePair3.Count), LBFlags.L0);
				}
				foreach (GSILocalEnumerablePair<CannonBallInfo> gsilocalEnumerablePair4 in ((IEnumerable<GSILocalEnumerablePair<CannonBallInfo>>){26082}.changeBalls.CannonBallInfo))
				{
					{19994}.Logbook(Local.lbe_moved_fromfort(gsilocalEnumerablePair4.Info.Name, gsilocalEnumerablePair4.Count), LBFlags.L0);
				}
				IL_1DF:
				Global.Player.UpdateCapacity();
				if ({26082}.allPortStatus != null)
				{
					PbsBuildingStatus pbsBuildingStatus = {26082}.allPortStatus.Buildings.Array[{26082}.BuildingInstanceIndex];
					if ({18328}.CurrentInstance == null)
					{
						new {18328}(pbsBuildingStatus, {26082}.allPortStatus);
					}
					else
					{
						{18328} currentInstance = {18328}.CurrentInstance;
						if (currentInstance != null)
						{
							currentInstance.ExternalUpdate(pbsBuildingStatus, {26082}.allPortStatus);
						}
					}
					if ({22913}.CurrentInstance != null)
					{
						{22913}.CurrentInstance.UpdateState({26082}.allPortStatus);
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnChangeAttackWindowMsg>(delegate(ref OnChangeAttackWindowMsg {26083})
			{
				if (!{26083}.success)
				{
					new {17312}(Local.NetworkManager_85);
					return;
				}
				Session.Guild.PbsWindow = {26083}.window;
				Session.Guild.TryMakeEffect(GuildTemporaryEffect.Type.BlockChangeWindowAgain, string.Empty, 0, Session.EventActionsPipeline);
				{20364} currentInstance = {20364}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.Reload();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnBuildingGuildOperationMsg>(delegate(ref OnBuildingGuildOperationMsg {26084})
			{
				if ({26084}.Action == OnBuildingGuildOperationMsg.OpType.BuyTensityProtection && {26084}.BuildingUid == 0)
				{
					new {17312}(Local.NetworkManager_85);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGuildWindowRequest>(delegate(ref OnGuildWindowRequest {26085})
			{
				Session.GuildCacheServer_CapturingRistriction = {26085}.capturingRistriction;
				Session.GuildCacheServer_Rating = {26085}.Rating;
				foreach (GuildMemberCacheUpdate guildMemberCacheUpdate in ((IEnumerable<GuildMemberCacheUpdate>){26085}.cacheSyncWithoutNames))
				{
					GuildMember member = Session.Guild.GetMember(guildMemberCacheUpdate.SID);
					if (member != null)
					{
						string name = member.Cached.Name;
						member.Cached = guildMemberCacheUpdate.Value;
						member.Cached.Name = name;
					}
				}
				{20364} currentInstance = {20364}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.UpdateQuests({26085}.quests);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnRegisterToBattleResultMsg>(delegate(ref OnRegisterToBattleResultMsg {26086})
			{
				if ({26086}.EngageInPb)
				{
					{18945}.TryShowNotif(Local.NetworkManager_6, null, null);
					Session.EngagingInPortBattle = {26086}.AcceptedSide;
					Session.EngagingInPortBattlePort = Gameplay.WorldMap.Ports.Array[{26086}.AcceptedPortID];
					Session.PortBattleInfo = {26086}.Status;
					Session.Account.IsPeaceActivated = false;
					if ({26086}.SetPositionIfNotZero != Vector2.Zero)
					{
						Global.Player.Position = {26086}.SetPositionIfNotZero;
						Global.Render.PostProcess.GradientAnimationBegin(3000f, false, false);
						return;
					}
				}
				else
				{
					Session.EngagingInPortBattle = PbsBatlleSide.None;
					Session.EngagingInPortBattlePort = null;
					if ({26086}.MessageId == 1)
					{
						new {17312}(Local.RespawnPbWithoutPoints);
					}
					else if ({26086}.MessageId == 2)
					{
						new {17312}(Local.RegisterToPbOtherError);
					}
					Global.Settings.DeathController.OnDisembarkPB();
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnSupplyPort>(delegate(ref OnSupplyPort {26087})
			{
				Session.Game.NearPortStatus.Dev.AddXP({26087}.GetXp());
				{19994}.MeAndLogbook({19988}.Info, Local.PortSupply_Notify(StringHelper.BigValueHelper({26087}.GetXp()), Global.Player.NearPort.PortName), null);
				if (!{26087}.IsConquerBadges)
				{
					Session.Account.NearPortStorage.AddOrRemove({26087}.ResId, -{26087}.Quantity);
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
					defaultInterpolatedStringHandler.AppendLiteral("-");
					defaultInterpolatedStringHandler.AppendFormatted<int>({26087}.Quantity);
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted(Gameplay.ItemsInfo.FromID({26087}.ResId).Name);
					{19994}.Logbook(defaultInterpolatedStringHandler.ToStringAndClear(), LBFlags.L0);
				}
				{20664} instance = {20664}.Instance;
				if (instance == null)
				{
					return;
				}
				instance.Update();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnPortSupplyInfoRequest>(delegate(ref OnPortSupplyInfoRequest {26088})
			{
				{20664} instance = {20664}.Instance;
				if (instance != null)
				{
					instance.UpdateSupplyInfo({26088}.SupplyInfo);
				}
				{20664} instance2 = {20664}.Instance;
				if (instance2 == null)
				{
					return;
				}
				instance2.Update();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGlobalPlayerSearch>(delegate(ref OnGlobalPlayerSearch {26089})
			{
				{22585} currentInstance = {22585}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.GlobalSearchResponse({26089});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnReceiveFriendRequest>(new ReceiverCallback<OnReceiveFriendRequest>(this.{25968}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnFriendsChangedMsg>(delegate(ref OnFriendsChangedMsg {26090})
			{
				uint sidOfFriend = {26090}.TargetAccountSID;
				Session.FriendsRequests.RemoveAll((FriendRequest {26143}) => {26143}.FromSID == sidOfFriend);
				if ({26090}.IsAdd)
				{
					Tlist<FriendCacheItem> friendsCache = Session.FriendsCache;
					FriendCacheItem friendCacheItem = new FriendCacheItem({26090}.TargetAccountSID, {26090}.Name, {26090}.IsOnline);
					friendsCache.Add(friendCacheItem);
					Session.Account.Friends.Add({26090}.TargetAccountSID);
					{19994}.MeAndLogbook({19988}.Info, Local.NetworkManager_88({26090}.Name), null);
				}
				else
				{
					Session.FriendsCache.RemoveAll((FriendCacheItem {26144}) => {26144}.AccountSID == sidOfFriend);
					Session.Account.Friends.Remove({26090}.TargetAccountSID);
				}
				Session.OnFriendsChanged();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnFriendStatusChangeMsg>(delegate(ref OnFriendStatusChangeMsg {26091})
			{
				if ({26091}.InitializeAllFriends.Size > 0)
				{
					for (int i = 0; i < Session.Account.Friends.Size; i++)
					{
						Tlist<FriendCacheItem> friendsCache = Session.FriendsCache;
						FriendCacheItem friendCacheItem = new FriendCacheItem(Session.Account.Friends.Array[i], {26091}.InitializeAllFriends.Array[i].CachedName, {26091}.InitializeAllFriends.Array[i].Flags == 1);
						friendsCache.Add(friendCacheItem);
					}
				}
				if ({26091}.AccountSID != 0U)
				{
					foreach (FriendCacheItem friendCacheItem2 in ((IEnumerable<FriendCacheItem>)Session.FriendsCache))
					{
						if (friendCacheItem2.AccountSID == {26091}.AccountSID)
						{
							friendCacheItem2.IsOnline = {26091}.IsOnline;
						}
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnHoldActionMsg>(delegate(ref OnHoldActionMsg {26092})
			{
				IStorageAsset resource = Gameplay.GetResource((int){26092}.ID, {26092}.type);
				Global.Player.UsedShipPlayer.AddOrRemoveItemsInHold(resource, -Math.Min(Global.Player.UsedShipPlayer.GetItemsCountInHold(resource), {26092}.count));
				Global.Player.UpdateCapacity();
				{17745} currentInstance = {17745}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.ExternalUpdate();
				}
				{19994}.Logbook(Local.lbe_dropped(resource.getName, {26092}.count), LBFlags.L1);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnTakeFoodMsg>(delegate(ref OnTakeFoodMsg {26093})
			{
				if (Global.Game.WorldInstance.GetShipFromUID({26093}.shipUID) == Global.Player)
				{
					Session.Account.FoodAtShip.TryTakeFood(Global.Player, {26093}.takenFood);
					{17745} currentInstance = {17745}.CurrentInstance;
					if (currentInstance != null)
					{
						currentInstance.ExternalUpdate();
					}
					foreach (GSILocalEnumerablePair<ResourceInfo> {5413} in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){26093}.takenFood.ResourceInfo))
					{
						if ({17745}.CurrentInstance == null)
						{
							{19994}.MeAndLogbook({19988}.Minus, Local.feed_crew + ": " + {5413}.ToStringNC(true), null);
						}
						else
						{
							{19994}.Logbook(Local.feed_crew + ": " + {5413}.ToStringNC(true), LBFlags.L0);
						}
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnCreateDropMsg>(delegate(ref OnCreateDropMsg {26094})
			{
				for (int i = 0; i < {26094}.createChests.Size; i++)
				{
					Global.Game.WorldInstance.AddDrop(ref {26094}.createChests.Array[i]);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGetDropMsg>(delegate(ref OnGetDropMsg {26095})
			{
				ClientDrop dropByUid = Global.Game.WorldInstance.GetDropByUid({26095}.DropUID);
				if (dropByUid != null)
				{
					dropByUid.LoadFactor = {26095}.updateLoadFactor;
				}
				switch ({26095}.Operation)
				{
				case DropOperation.OpenRequestResult:
					if ({19275}.CurrentInstance == null && dropByUid != null)
					{
						new {19275}({26095}.DropUID, {26095}.ActualState);
						goto IL_1E5;
					}
					goto IL_1E5;
				case DropOperation.Apply:
					if ({26095}.model == DropModel.RealShipLoot && !dropByUid.BindedShipFloodingDataOrNull.isNpcShip)
					{
						Global.Player.BeginBattleTimer(false, false);
					}
					CrewNotificationManager.WhenDropGet({26095}.ActualState, {26095}.luckWorks, {26095}.showPremiumToolTip);
					Global.Player.ResourcesOfHold.Add({26095}.ActualState.Resources);
					WosbTreasuryMaps.DetachMaps(Session.Account);
					Session.Account.AddXp({26095}.ActualState.BonusXp, true);
					Session.Account.Gold += {26095}.ActualState.GoldBonus;
					Session.Account.CannonsInHold.Add({26095}.ActualState.Cannons);
					if ({26095}.ActualState.PowerupItemIDindex != 255)
					{
						Session.Account.PowerupItemExtraSlot = {26095}.ActualState.PowerupItemIDindex;
					}
					Global.Player.UsedShipPlayer.PowderKegsOfHold.Add({26095}.ActualState.PowderKegs);
					Global.Player.UsedShipPlayer.BallsOfHold.Add({26095}.ActualState.Ammo);
					if ({26095}.model == DropModel.Fishing)
					{
						EducationHelper.MakeFlag(EducationOnboarding.DoFishing, false);
					}
					Global.Player.UpdateCapacity();
					if ({26095}.ActualState.MineTrapInside)
					{
						{19994}.Me({19988}.Info, Local.mine_trap, Array.Empty<object>());
						goto IL_1E5;
					}
					goto IL_1E5;
				case DropOperation.Update:
					if ({19275}.CurrentInstance != null && {19275}.CurrentInstance.DropUID == {26095}.DropUID && dropByUid != null)
					{
						{19275}.CurrentInstance.ExternalUpdate({26095}.ActualState);
						goto IL_1E5;
					}
					goto IL_1E5;
				}
				throw new NotSupportedException();
				IL_1E5:
				if ({26095}.infoCrewDamage != null && !{26095}.infoCrewDamage.IsEmpty)
				{
					{19994}.MeAndLogbook({19988}.GiveCrew, Local.Current("unit_dead" + Rand.RangeInt(1, 6).ToString(), new object[]
					{
						Gameplay.UnitsInfo.FromID({26095}.infoCrewDamage.RandomName()).Name
					}), null);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnEnteredLootCombatMode>(delegate(ref OnEnteredLootCombatMode {26096})
			{
				if ({26096}.InitiatorUID == Global.Player.uID)
				{
					if (Session.Account.WorldFlag.IsPeaceMode())
					{
						Session.Account.WorldFlag = OpenWorldFlag.Pirate;
						Session.Account.IsPeaceActivated = false;
						{19994}.Me({19988}.InfoRed, Local.pvp_mode, Array.Empty<object>());
						return;
					}
				}
				else
				{
					ShipOtherPlayer otherPlayerFromUID = Global.Game.WorldInstance.GetOtherPlayerFromUID({26096}.InitiatorUID);
					if ({26096}.AskAboutTakingOffPeacefulFlagUids.Contains(Global.Player.uID))
					{
						{19994}.Me({19988}.Big_Red, Local.battle_for_loot_notif(otherPlayerFromUID.Client.GetName2()), Array.Empty<object>());
						if (Session.Account.WorldFlag.IsPeaceMode())
						{
							OnEnteredLootCombatMode m = {26096};
							{19994}.Invite(Local.battle_for_loot_ask, delegate
							{
								Global.Network.Send(new OnEnteredLootCombatMode(m.InitiatorUID, Tlist<int>.EmptyReadonly));
							}, new Keys?(Global.Settings.kb_Accept.Key), new Keys?(Global.Settings.kb_Undo.Key), 12000f);
						}
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnCreateOrFinishWorldActivity>(delegate(ref OnCreateOrFinishWorldActivity {26097})
			{
				Session.Account.WorldActivities.Add({26097}.createdOrFinished);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnWhaleUpdateMsg>(delegate(ref OnWhaleUpdateMsg {26098})
			{
				ClientDrop dropByUid = Global.Game.WorldInstance.GetDropByUid({26098}.dropUid);
				if (dropByUid != null && dropByUid.ModelType == DropModel.Whale)
				{
					dropByUid.Whale = {26098}.newStatus;
					dropByUid.Whale.Client_CorrectPostion = {26098}.currentServerPos - dropByUid.Position;
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnTestServerShipStatShift>(new ReceiverCallback<OnTestServerShipStatShift>(this.{25970}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnTestServerShipStatShiftLoad>(new ReceiverCallback<OnTestServerShipStatShiftLoad>(this.{25972}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnDebugPosDisplay>(delegate(ref OnDebugPosDisplay {26099})
			{
				ShipCurrentPlayer player = Global.Player;
				if (player != null)
				{
					((IClientShip)player).GetClient.ServerPositionDisplay.Set({26099});
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnConsoleCommandExecute>(delegate(ref OnConsoleCommandExecute {26100})
			{
				ClientCommandContext ctx = new ClientCommandContext();
				Decorator game = Session.Game;
				CommonCommandContext commonCtx = new CommonCommandContext(ref game, Global.Player, Global.Player.AccountConnection);
				OnConsoleCommandResult item = Global.Game.ClientCommands.Execute(ctx, commonCtx, {26100}.RawString).Item1;
				DebugPanel.WriteToConsole(item.Message, item.Color);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnConsoleCommandExecuteAsOtherClient>(delegate(ref OnConsoleCommandExecuteAsOtherClient {26101})
			{
				string rawString = {26101}.RawString;
				Global.Game.WorldInstance.QueryAction<ShipOtherPlayer>({26101}.uID, delegate(ShipOtherPlayer {26145})
				{
					Decorator game = Session.Game;
					CommonCommandContext commonCtx = new CommonCommandContext(ref game, {26145}, null);
					Global.Game.ClientCommands.Execute(null, commonCtx, rawString);
				});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnConsoleCommandResult>(delegate(ref OnConsoleCommandResult {26102})
			{
				DebugPanel.WriteToConsole({26102}.Message, {26102}.Color);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnPaymentStateChangedMsg>(delegate(ref OnPaymentStateChangedMsg {26103})
			{
				PlayerAccount account = Session.Account;
				account.Monets.Value = account.Monets.Value + {26103}.MonetsCount;
				if ({26103}.ReferalNotification != null)
				{
					Session.Account.Analytics.SummarySharedDonations += {26103}.MonetsCount;
					{19994}.MeAndLogbook({19988}.Big_Monets, {26103}.ReferalNotification.ToString(), new LBFlags?(LBFlags.L2));
				}
				else
				{
					Session.Account.Analytics.SummaryDonations += {26103}.MonetsCount;
					string text = Local.Monets2 + " +" + {26103}.MonetsCount.ToString();
					if (Session.Account.Analytics.SummaryDonations == {26103}.MonetsCount)
					{
						new {18593}(new EducationOnboarding?(EducationOnboarding.GameTT_FirstCoins));
						{19994}.Logbook(text, LBFlags.L2);
					}
					else
					{
						{19994}.MeAndLogbook({19988}.Big_Monets, text, new LBFlags?(LBFlags.L2));
					}
					{21395} currentInstance = {21395}.CurrentInstance;
					if (currentInstance != null && currentInstance.WaitingMode)
					{
						{21395} currentInstance2 = {21395}.CurrentInstance;
						if (currentInstance2 != null)
						{
							currentInstance2.BlockAndClose();
						}
					}
				}
				Global.Game.SoundSystem.PlaySound(GameStaticSoundName.Money2, 0.03f, 1f);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnPremiumAddCompletedMsg>(delegate(ref OnPremiumAddCompletedMsg {26104})
			{
				Session.Account.PremiumEnds = {26104}.DateOfEndPremium;
				if (!string.IsNullOrEmpty({26104}.eventTextOrNull))
				{
					new {19197}({26104}.eventTextOrNull);
				}
				if (!PlatformTuning.DisableShop && {26104}.DateOfEndPremium.Date.Year == 0)
				{
					new {17312}(Local.premium_finished_info, delegate()
					{
						Global.Game.ScenePort.realShopHandler(null, null);
						{20881}.RedirectToSubscriptionsPage(-1);
					}, delegate()
					{
					});
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnEventAddMonets>(delegate(ref OnEventAddMonets {26105})
			{
				if ({26105}.amount == -1)
				{
					Session.Account.ApplyWelcomeBackBonus({26105}.serverTime);
					new {17107}("", Session.Account.IsEndlessPremium ? Local.welcome_back(Session.Account.PlayerName, Gameplay.WelcomeBack_Gold(Session.Account.Rang), Gameplay.WelcomeBack_FreeResTeleports) : Local.welcome_back_prem(Session.Account.PlayerName, Gameplay.WelcomeBack_PremDays.Value, Gameplay.WelcomeBack_Gold(Session.Account.Rang), Gameplay.WelcomeBack_FreeResTeleports), Local.welcome_back_2, delegate(int {26106})
					{
						if ({26106} == 0)
						{
							Session.Account.WelcomeBackApplyTips();
						}
					}, true, null, new string[]
					{
						Local.welcome_back_enable_tt,
						Local.to_continue
					});
					return;
				}
				PlayerAccount account = Session.Account;
				account.Monets.Value = account.Monets.Value + {26105}.amount;
				if (!string.IsNullOrEmpty({26105}.eventTextOrNull))
				{
					new {19197}({26105}.eventTextOrNull);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnOpenChestMsg>(delegate(ref OnOpenChestMsg {26107})
			{
				if ({26107}.flags == OnOpenChestMsg.Flags.UseFiveCoinsAltar || {26107}.flags == OnOpenChestMsg.Flags.UseSingleCoinAltar)
				{
					int num = ({26107}.flags == OnOpenChestMsg.Flags.UseSingleCoinAltar) ? 1 : 5;
					Global.Player.ResourcesOfHold.AddOrRemove(15, -num);
					{19994}.MeAndLogbook({19988}.GiveResources, Gameplay.ItemsInfo.FromID(15).Name + " -" + num.ToString(), new LBFlags?(LBFlags.L1));
					new StringBuilder();
					for (int i = 0; i < {26107}.effects.Length; i++)
					{
						{19994}.MeAndLogbook({19988}.GiveScrolls, ShopChestApplyHelper.Atlar.ApplyAltar(Session.Account, {26107}.serverRandomValue, {26107}.effects[i]).ToString(), new LBFlags?(LBFlags.L1));
					}
					Global.Player.UpdateCapacity();
					return;
				}
				DonationSystem.DefaultChestOpened({26107});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnPublicDesignOrderPlacedMsg>(delegate(ref OnPublicDesignOrderPlacedMsg {26108})
			{
				if ({26108}.Operation == OnPublicDesignOrderPlacedMsg.State.Placed)
				{
					PlayerAccount account = Session.Account;
					account.Monets.Value = account.Monets.Value - Gameplay.PublicDesignPrice.Value;
					{19994}.MeAndLogbook({19988}.Okay, Local.lbe_public_design_order(Gameplay.PublicDesignPrice), new LBFlags?(LBFlags.L2));
				}
				if ({26108}.Operation == OnPublicDesignOrderPlacedMsg.State.ApprovedOnline)
				{
					Session.PlayerPublicDesigns.Add({26108}.InstanceOrNull);
					Session.PublicDesignsCache.Add({26108}.InstanceOrNull);
					{19994}.MeAndLogbook({19988}.Okay, Local.lbe_public_design_order_approve, new LBFlags?(LBFlags.L2));
				}
				if ({26108}.Operation == OnPublicDesignOrderPlacedMsg.State.DisappoveOnline)
				{
					PlayerAccount account2 = Session.Account;
					account2.Monets.Value = account2.Monets.Value + Gameplay.PublicDesignPrice.Value;
					{19994}.MeAndLogbook({19988}.InfoRed, {26108}.Message.ToString(), new LBFlags?(LBFlags.L2));
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnTradeLoadOrUpdateWindow>(new ReceiverCallback<OnTradeLoadOrUpdateWindow>(this.{25974}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnWorldActionMsg>(delegate(ref OnWorldActionMsg {26109})
			{
				Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({26109}.ShipUID);
				switch ({26109}.Action)
				{
				case WorldRandomAction.SouredResources:
					if ({26109}.SouredResourcesOrNull != null)
					{
						Global.Player.ResourcesOfHold.Remove({26109}.SouredResourcesOrNull, 1, false);
						Global.Player.UpdateCapacity();
						if (Session.SouredResourcesCounter == 0)
						{
							foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){26109}.SouredResourcesOrNull.ResourceInfo))
							{
								{19994}.MeAndLogbook({19988}.Minus, Local.NetworkManager_104(gsilocalEnumerablePair.Info.Name), null);
							}
						}
						Session.SouredResourcesCounter += {26109}.SouredResourcesOrNull.GetTotalItemsCount();
					}
					if ({26109}.SouredCannonBallsOrNull == null)
					{
						return;
					}
					Global.Player.UsedShipPlayer.BallsOfHold.Remove({26109}.SouredCannonBallsOrNull, 1, false);
					Global.Player.UpdateCapacity();
					using (IEnumerator<GSILocalEnumerablePair<CannonBallInfo>> enumerator2 = ((IEnumerable<GSILocalEnumerablePair<CannonBallInfo>>){26109}.SouredCannonBallsOrNull.CannonBallInfo).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							GSILocalEnumerablePair<CannonBallInfo> gsilocalEnumerablePair2 = enumerator2.Current;
							{19994}.MeAndLogbook({19988}.Minus, Local.NetworkManager_104_b(gsilocalEnumerablePair2.Info.Name) + " -" + gsilocalEnumerablePair2.Count.ToString(), null);
						}
						return;
					}
					break;
				case WorldRandomAction.DetectionPirates:
					{19994}.Me({19988}.InfoRed, ({26109}.Args == "hh") ? Local.NetworkManager_95_hh : Local.NetworkManager_95, Array.Empty<object>());
					return;
				case WorldRandomAction.NpcRetreat:
					{19994}.Me({19988}.Info, {26109}.Args + Local.NetworkManager_96, Array.Empty<object>());
					return;
				case WorldRandomAction.ShipOverturned:
					if (shipFromUID != null)
					{
						Ship ship = shipFromUID;
						DamageData damageData = new DamageData(shipFromUID.uID, -1, DamageID.Collision, shipFromUID.UsedShip.MaxHp + 1f, 0f);
						ship.MakeDamage(damageData, -1);
						return;
					}
					return;
				case WorldRandomAction.CaptureNpcItemFail:
					{19994}.Me({19988}.Info, Local.NetworkManager_103, Array.Empty<object>());
					return;
				case WorldRandomAction.SetFlagWorld:
				{
					OpenWorldFlag args = (OpenWorldFlag){26109}.Args2;
					if (shipFromUID is ShipCurrentPlayer)
					{
						if (args == OpenWorldFlag.Peaceful || args == OpenWorldFlag.Trader)
						{
							{18945}.TryShowNotif(Local.returnPf_warning, Local.pvp_mode_disallow, null);
						}
						else
						{
							if (args != OpenWorldFlag.PeacefulDisallowed)
							{
								Session.Account.IsPeaceActivated = false;
							}
							Session.Account.TensityMode = false;
							if (Session.Account.WorldFlag.IsPeaceMode())
							{
								{19994}.Me({19988}.InfoRed, Local.pvp_mode, Array.Empty<object>());
							}
						}
						Session.Account.WorldFlag = args;
						return;
					}
					ShipOtherPlayer shipOtherPlayer = shipFromUID as ShipOtherPlayer;
					if (shipOtherPlayer != null)
					{
						shipOtherPlayer.RemoteInfo.Flags = args;
						return;
					}
					ShipNpc shipNpc = shipFromUID as ShipNpc;
					if (shipNpc != null)
					{
						shipNpc.OwnerFlags = args;
						return;
					}
					return;
				}
				case WorldRandomAction.SetFlagWarning:
					{18945}.TryShowNotif(Global.Player.IsOutsideSeam ? Local.resetPf_warningSeamless : ((Session.Account.WorldFlag.Mapback() == OpenWorldFlag.Trader) ? Local.resetPf_warning_traderFlag : Local.resetPf_warning), Local.pvp_mode_alert, new float?((float)20000));
					if (Global.Player.IsOutsideSeam)
					{
						EducationHelper.MakeFlag(EducationOnboarding.GameTT_EnterSeamlessSea, true);
						return;
					}
					return;
				case WorldRandomAction.SetFlagSpeedInfo:
					{18945}.TryShowNotif(Local.returnPf_warningSpeed, "", null);
					return;
				case WorldRandomAction.PBFlagResetWarningWarning:
					{19994}.Me({19988}.Big_Red, Local.pbFlag_warning2, Array.Empty<object>());
					return;
				case WorldRandomAction.PBRegisterToolTip:
					{18945}.TryShowNotif(Local.pb_notif_battlebegan, null, null);
					return;
				case WorldRandomAction.PBLabelSet:
					if ({26109}.ShipUID == Global.Player.uID)
					{
						Session.Account.WorldFlag = OpenWorldFlag.War;
						return;
					}
					Global.Game.WorldInstance.QueryAction<ShipOtherPlayer>({26109}.ShipUID, delegate(ShipOtherPlayer {26110})
					{
						{26110}.VisualFlags |= ShipVisualStatusFlags.EngageInPb;
						{26110}.RemoteInfo.Flags = OpenWorldFlag.War;
					});
					return;
				case WorldRandomAction.PBLabelReset:
					Global.Game.WorldInstance.QueryAction<ShipOtherPlayer>({26109}.ShipUID, delegate(ShipOtherPlayer {26111})
					{
						{26111}.VisualFlags &= (ShipVisualStatusFlags)251;
					});
					return;
				case WorldRandomAction.PBTowersCountDestructed:
					{18945}.TryShowNotif(Local.pb_notif_towerdestroy({26109}.Args), null, null);
					return;
				case WorldRandomAction.PBTowersCountCaptured:
				{
					string[] array = {26109}.Args.Split('|', StringSplitOptions.None);
					{18945}.TryShowNotif(Local.pb_notif_towercaptured(array[0]), array[1], null);
					return;
				}
				case WorldRandomAction.PBAllTowersDestroyedAndFortReady:
					{18945}.TryShowNotif(Local.pb_botif_alldestr, Local.pb_notif_finishStep, null);
					return;
				case WorldRandomAction.PBFortDestructed:
					{18945}.TryShowNotif(Local.pb_notif_fort_destructed, null, null);
					return;
				case WorldRandomAction.PBFortAllowToRob:
					{18945}.TryShowNotif(Local.pb_notif_fort_allowToBeRobbed, null, null);
					return;
				case WorldRandomAction.PBFortRobbed:
					{18945}.TryShowNotif(Local.pb_notif_fort_robbed({26109}.Args), Local.pb_notif_fort_mint_robbed({26109}.Args2), null);
					return;
				case WorldRandomAction.WarCompanyErrorOnceQuest:
					new {17107}(Local.war_comp_err1({26109}.Args), "");
					return;
				case WorldRandomAction.WarCompanyErrorGroup:
					new {17107}(Local.war_comp_err2, "");
					return;
				case WorldRandomAction.Thundershot:
					if (shipFromUID != null)
					{
						shipFromUID.CreateBoardingBurning(-1, PredefinedBurningType.Finite);
						shipFromUID.MakeSimpleDamage(shipFromUID.UsedShip.MaxHp * 0.25f);
						shipFromUID.physicsBody.NowSpeed *= 0.5f;
						FXEngine.CreateLighting(shipFromUID.Position, true);
						return;
					}
					return;
				case WorldRandomAction.FinshPlayerWorldActivityWithRes:
					break;
				case WorldRandomAction.ApplyWorldResearchPointByClient:
					return;
				case WorldRandomAction.CombatModeFailed:
					{19994}.Me({19988}.InfoRed, Local.battle_for_loot_fail, Array.Empty<object>());
					return;
				case WorldRandomAction.ChangeVisibleTitle:
				{
					ShipOtherPlayer shipOtherPlayer2 = shipFromUID as ShipOtherPlayer;
					if (shipOtherPlayer2 != null)
					{
						shipOtherPlayer2.RemoteInfo.SelectedCaptainTitle = (CaptainTitle){26109}.Args2;
						return;
					}
					return;
				}
				case WorldRandomAction.MakeTemporaryPeaceFlag:
				{
					if (shipFromUID is ShipCurrentPlayer)
					{
						Session.Account.WorldFlag = OpenWorldFlag.Peaceful;
						{19994}.Me({19988}.InfoRed, Local.pvp_mode_disallow, Array.Empty<object>());
						Session.Account.PeaceExtraTimeSec = Math.Max(Session.Account.PeaceExtraTimeSec, (float){26109}.Args2);
						Session.Account.IsPeaceActivated = true;
						return;
					}
					ShipOtherPlayer shipOtherPlayer3 = shipFromUID as ShipOtherPlayer;
					if (shipOtherPlayer3 != null)
					{
						shipOtherPlayer3.RemoteInfo.Flags = OpenWorldFlag.Peaceful;
						return;
					}
					return;
				}
				case WorldRandomAction.FishingBuff:
				{
					ShipPlayerBase shipPlayerBase = shipFromUID as ShipPlayerBase;
					if (shipPlayerBase == null)
					{
						return;
					}
					shipPlayerBase.UsedShipPlayer.TryMakeFinsingBuff();
					if (shipPlayerBase == Global.Player)
					{
						{19994}.Me({19988}.Info, Local.fishing_result, Array.Empty<object>());
						return;
					}
					return;
				}
				default:
					return;
				}
				int shipUID = {26109}.ShipUID;
				if (!{26109}.SouredResourcesOrNull.IsEmpty)
				{
					PlayerWorldActivityStatus playerWorldActivityStatus = Session.Account.WorldActivities.Array[shipUID];
					PersonalIsleStatus nearTo = Session.Account.PersonalIsles.GetNearTo(playerWorldActivityStatus.Position);
					if (nearTo != null)
					{
						nearTo.StorageResources.Add({26109}.SouredResourcesOrNull);
					}
				}
				Session.Account.WorldActivities.FastRemoveAt(shipUID);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnDroppedResources>(delegate(ref OnDroppedResources {26112})
			{
				{19994}.Logbook(Local.lbe_mydeath, LBFlags.L1);
				Session.Account.Shipyard.CurrentRealShip.BallsOfHold.Remove({26112}.CannonBalls, 1, false);
				foreach (GSILocalEnumerablePair<CannonBallInfo> {5413} in ((IEnumerable<GSILocalEnumerablePair<CannonBallInfo>>){26112}.CannonBalls.CannonBallInfo))
				{
					{19994}.Logbook({5413}.ToStringNC(true), LBFlags.L1);
				}
				Global.Player.ResourcesOfHold.Remove({26112}.Resources, 1, false);
				foreach (GSILocalEnumerablePair<ResourceInfo> {5413}2 in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){26112}.Resources.ResourceInfo))
				{
					{19994}.Logbook({5413}2.ToStringNC(true), LBFlags.L1);
				}
				Session.Account.CannonsInHold.Remove({26112}.CannonsFromHold, 1, false);
				foreach (GSILocalEnumerablePair<CannonGameInfo> {5413}3 in ((IEnumerable<GSILocalEnumerablePair<CannonGameInfo>>){26112}.CannonsFromHold.CannonGameInfo))
				{
					{19994}.Logbook({5413}3.ToStringNC(true), LBFlags.L1);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnMakeStaticTradeMsg>(delegate(ref OnMakeStaticTradeMsg {26113})
			{
				PortTradePage_SyncProtected currentInstance = PortTradePage_SyncProtected.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.StmResponse({26113});
				}
				if (!{26113}.SucceedInServer)
				{
					{19994}.Me({19988}.InfoRed, Local.NetworkManager_105, Array.Empty<object>());
					return;
				}
				string {19995};
				string {19998};
				Session.Account.ApplyStaticTradeHelper({26113}, Global.Player, out {19995}, out {19998});
				Global.Player.UpdateCapacity();
				if ({26113}.Target == OnMakeStaticTradeMsg.Source.PortAmmo)
				{
					EducationHelper.MakeFlag(EducationOnboarding.ShopCannonBalls, true);
				}
				if ({26113}.Target == OnMakeStaticTradeMsg.Source.PortResource)
				{
					EducationHelper.MakeFlag(EducationOnboarding.SellItemsInTrade, true);
				}
				if ({26113}.Target == OnMakeStaticTradeMsg.Source.PortResource && {26113}.OfferOrResID == 1 && Global.Player.NearPort.PortID != (int)Session.Account.StartPortId && Session.Account.IsEducationInProgress(EducationOnboarding.BuyWoodInOtherPort, false, false))
				{
					EducationHelper.MakeFlag(EducationOnboarding.BuyWoodInOtherPort, true);
					if (PortTradePage_SyncProtected.CurrentInstance != null)
					{
						PortTradePage_SyncProtected.CurrentInstance.ShowWoodQuestToolTip = true;
					}
				}
				{19994}.Logbook({19995}, LBFlags.L1);
				if ({17177}.CurrentInstance == null)
				{
					{19994}.Me({19988}.Okay, {19998}, Array.Empty<object>());
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnTradeBetweenPlayers>(delegate(ref OnTradeBetweenPlayers {26114})
			{
				if ({26114}.Status == OnTradeBetweenPlayers.Flag.AskTarget)
				{
					TradeOrderBetweenPlayers order = {26114}.Order;
					ShipOtherPlayer otherPlayerFromUID = Global.Game.WorldInstance.GetOtherPlayerFromUID({26114}.Order.SourceUID);
					string name = ((otherPlayerFromUID != null) ? otherPlayerFromUID.Client.GetName2() : null) ?? "";
					Action <>9__185;
					{19994}.Invite(Local.tradeBetweenPlayers_query_short(name), delegate
					{
						string {17371} = ((order.TotalPrice == 0) ? Local.tradeBetweenPlayers_query_full_free(name, order.Content.ResourceInfo.First<GSILocalEnumerablePair<ResourceInfo>>().Count, order.Content.ResourceInfo.First<GSILocalEnumerablePair<ResourceInfo>>().Info.Name) : Local.tradeBetweenPlayers_query_full(name, order.Content.ResourceInfo.First<GSILocalEnumerablePair<ResourceInfo>>().Count, order.Content.ResourceInfo.First<GSILocalEnumerablePair<ResourceInfo>>().Info.Name, order.TotalPrice, Math.Round((double)((float)order.TotalPrice / (float)order.Content.GetTotalItemsCount()), 1))) + ". " + Local.to_continue + "?";
						Action {17372};
						if (({17372} = <>9__185) == null)
						{
							{17372} = (<>9__185 = delegate()
							{
								Global.Network.Send(new OnTradeBetweenPlayers(OnTradeBetweenPlayers.Flag.Accept, order));
							});
						}
						new {17312}({17371}, {17372}, delegate()
						{
						}).TimeoutMs = 15000f;
					}, new Keys?(Global.Settings.kb_Accept.Key), null, 15000f);
					return;
				}
				if ({26114}.Status == OnTradeBetweenPlayers.Flag.Fail)
				{
					{19994}.Me({19988}.InfoRed, Local.tradeBetweenPlayers_query_fail, Array.Empty<object>());
					return;
				}
				if ({26114}.Status == OnTradeBetweenPlayers.Flag.Accept)
				{
					string name2 = {26114}.Order.Content.ResourceInfo.First<GSILocalEnumerablePair<ResourceInfo>>().Info.Name;
					GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair = {26114}.Order.Content.ResourceInfo.First<GSILocalEnumerablePair<ResourceInfo>>();
					if (Global.Player.uID == {26114}.Order.SourceUID)
					{
						Global.Player.ResourcesOfHold.Remove({26114}.Order.Content, 1, false);
						Session.Account.Gold += {26114}.Order.TotalPrice - {26114}.Order.Tax;
						ShipOtherPlayer shipOtherPlayer = Global.Game.WorldInstance.GetShipFromUID({26114}.Order.TargetUID) as ShipOtherPlayer;
						{19994}.Me({19988}.GiveResources, gsilocalEnumerablePair.ToStringNC(true), Array.Empty<object>());
						{19994}.Me({19988}.Gold, Local.gold + " +" + ({26114}.Order.TotalPrice - {26114}.Order.Tax).ToString(), Array.Empty<object>());
						{19994}.Logbook(Local.lbe_sold_player(name2, gsilocalEnumerablePair.Count, {26114}.Order.TotalPrice - {26114}.Order.Tax, (shipOtherPlayer != null) ? shipOtherPlayer.Client.GetName2() : null), LBFlags.L1);
					}
					else
					{
						Global.Player.ResourcesOfHold.Add({26114}.Order.Content);
						Session.Account.Gold -= {26114}.Order.TotalPrice;
						ShipOtherPlayer shipOtherPlayer2 = Global.Game.WorldInstance.GetShipFromUID({26114}.Order.SourceUID) as ShipOtherPlayer;
						{19994}.Me({19988}.GiveResources, gsilocalEnumerablePair.ToStringNC(false), Array.Empty<object>());
						{19994}.Me({19988}.Gold, Local.gold + " -" + {26114}.Order.TotalPrice.ToString(), Array.Empty<object>());
						{19994}.Logbook(Local.lbe_bought_player(name2, gsilocalEnumerablePair.Count, {26114}.Order.TotalPrice, (shipOtherPlayer2 != null) ? shipOtherPlayer2.Client.GetName2() : null), LBFlags.L1);
					}
					Global.Player.UpdateCapacity();
					return;
				}
				throw new NotSupportedException();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnAuctionQueryResponse>(delegate(ref OnAuctionQueryResponse {26115})
			{
				PortTradePage_SyncProtected currentInstance = PortTradePage_SyncProtected.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.AuctionResponse({26115});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnAuctionOrderAction>(delegate(ref OnAuctionOrderAction {26116})
			{
				if ({26116}.action == OnAuctionOrderAction.ActionType.Create)
				{
					PortTradePage_SyncProtected currentInstance = PortTradePage_SyncProtected.CurrentInstance;
					if (currentInstance != null)
					{
						currentInstance.AddAuctionOrder({26116}.content);
					}
					if ({26116}.content.Mode == TradeOrderMode.Shop)
					{
						{19994}.Logbook(Local.lbe_auction_new_order({26116}.content.ItemInfo.getName), LBFlags.L1);
						return;
					}
					if ({26116}.content.Mode == TradeOrderMode.Holding)
					{
						{19994}.Logbook(Local.lbe_auction_new_holding({26116}.content.ItemInfo.getName, {26116}.content.CurrentCount, {26116}.content.Price.StringAddPrefix({26116}.content.CostMargin, false)), LBFlags.L1);
						return;
					}
					if ({26116}.content.ItemInfo.getType == StorageAssetEnum.Ship_DisplayOnly)
					{
						{19994}.Logbook(Local.lbe_auction_new_ship({26116}.content.ItemInfo.getName), LBFlags.L1);
						return;
					}
					{19994}.Logbook(Local.lbe_auction_new({26116}.content.ItemInfo.getName, {26116}.content.CurrentCount, {26116}.content.Price.StringAddPrefix({26116}.content.CostTotal, false)), LBFlags.L1);
					return;
				}
				else if ({26116}.action == OnAuctionOrderAction.ActionType.Apply)
				{
					if ({26116}.content.CurrentCount == 0)
					{
						PortTradePage_SyncProtected currentInstance2 = PortTradePage_SyncProtected.CurrentInstance;
						if (currentInstance2 != null)
						{
							currentInstance2.RemoveAuctionOrderCount({26116}.content.OrderServerID, int.MaxValue);
						}
						new {17312}(Local.NetworkManager_106);
						return;
					}
					PortTradePage_SyncProtected currentInstance3 = PortTradePage_SyncProtected.CurrentInstance;
					if (currentInstance3 != null)
					{
						currentInstance3.RemoveAuctionOrderCount({26116}.content.OrderServerID, {26116}.content.CurrentCount);
					}
					TradeOrderCommon.CompleteForCustomer(Session.Account, {26116}.content, {26116}.ByMonets);
					if ({26116}.content.Mode == TradeOrderMode.Holding)
					{
						{19994}.Logbook(Local.lbe_auction_return({26116}.content.ItemInfo.getName, {26116}.content.CurrentCount), LBFlags.L1);
					}
					else if ({26116}.content.Mode == TradeOrderMode.Shop)
					{
						{19994}.Logbook(Local.lbe_auction_sold({26116}.content.ItemInfo.getName, {26116}.content.CurrentCount, {26116}.content.Price.StringAddPrefix({26116}.ByMonets ? {26116}.content.CostTotalConvToMonets : {26116}.content.CostTotal, {26116}.ByMonets)), LBFlags.L1);
					}
					else
					{
						{19994}.Logbook(Local.lbe_auction_bought({26116}.content.ItemInfo.getName, {26116}.content.CurrentCount, {26116}.content.Price.StringAddPrefix({26116}.ByMonets ? {26116}.content.CostTotalConvToMonets : {26116}.content.CostTotal, {26116}.ByMonets)), LBFlags.L1);
					}
					IStorageAsset itemInfo = {26116}.content.ItemInfo;
					if (itemInfo is ProceduralShipInfo && ((ProceduralShipInfo)itemInfo).ShipInfo.Rank <= 5)
					{
						EducationHelper.MakeFlag(EducationOnboarding.BuildOrBuyShipRank5, true);
					}
					if ({26116}.content.Mode == TradeOrderMode.Shop)
					{
						{19994}.Me({19988}.Okay, "+" + {26116}.content.Price.StringAddPrefix({26116}.content.CostTotal, false), Array.Empty<object>());
						return;
					}
					{19994}.Me({19988}.Okay, {26116}.content.ItemInfo.getName + " +" + {26116}.content.CurrentCount.ToString(), Array.Empty<object>());
					return;
				}
				else
				{
					if ({26116}.action == OnAuctionOrderAction.ActionType.Make)
					{
						PortTradePage_SyncProtected currentInstance4 = PortTradePage_SyncProtected.CurrentInstance;
						if (currentInstance4 != null)
						{
							currentInstance4.RemoveAuctionOrderCount({26116}.content.OrderServerID, {26116}.content.CurrentCount);
						}
						TradeOrderCommon.CompleteForCreatorAnySync(Session.Account, {26116}.content);
						return;
					}
					if ({26116}.action == OnAuctionOrderAction.ActionType.Undo)
					{
						PortTradePage_SyncProtected currentInstance5 = PortTradePage_SyncProtected.CurrentInstance;
						if (currentInstance5 != null)
						{
							currentInstance5.RemoveAuctionOrderCount({26116}.content.OrderServerID, {26116}.content.CurrentCount);
						}
						if ({26116}.content.Mode != TradeOrderMode.Shop)
						{
							if ({26116}.content.OwnerSID != 0U)
							{
								new {17312}({19994}.Logbook(Local.NetworkManager_107({26116}.content.ItemInfo.getName, {26116}.content.CurrentCount), LBFlags.L1));
							}
						}
						else if ({26116}.content.OwnerSID != 0U)
						{
							new {17312}({19994}.Logbook(Local.NetworkManager_110({26116}.content.Price.StringAddPrefix({26116}.content.CostTotal, false)), LBFlags.L1));
						}
						TradeOrderCommon.UndoHelper(Session.Account, {26116}.content, (int){26116}.content.PortID);
					}
					return;
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnTraderInSeaOpenMsg>(delegate(ref OnTraderInSeaOpenMsg {26117})
			{
				{18417} currentInstance = {18417}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.ReceivedResponse({26117});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnTraderInSeaOperatingMsg>(delegate(ref OnTraderInSeaOperatingMsg {26118})
			{
				Session.LastTraderInSeaOffers.MakeOfferOrPartInSea(Global.Player, {26118}.offer);
				Global.Player.UpdateCapacity();
				Global.Game.SoundSystem.PlaySound(GameStaticSoundName.Money1, 0.03f, 1f);
				{18417} currentInstance = {18417}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.StmResponse({26118});
				}
				if ({26118}.offer.CountAbs != 0)
				{
					if ({26118}.offer.PriceID != 0)
					{
						{19994}.Logbook(Local.lbe_exchange({26118}.offer.PriceRes.Name, Math.Abs({26118}.offer.PriceWithSign(Session.Account) * {26118}.offer.CountAbs), {26118}.offer.Item.getName, {26118}.offer.CountAbs), LBFlags.L0);
						return;
					}
					if ({26118}.offer.IsPlayerBuy)
					{
						{19994}.Logbook(Local.lbe_bought({26118}.offer.Item.getName, {26118}.offer.CountAbs, Math.Abs({26118}.offer.PriceWithSign(Session.Account) * {26118}.offer.CountAbs)), LBFlags.L0);
						return;
					}
					{19994}.Logbook(Local.lbe_sold({26118}.offer.Item.getName, {26118}.offer.CountAbs, Math.Abs({26118}.offer.PriceWithSign(Session.Account) * {26118}.offer.CountAbs)), LBFlags.L0);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGetCrewPortLimits>(delegate(ref OnGetCrewPortLimits {26119})
			{
				{21596} currentInstance = {21596}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.ReceiveResponse({26119});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnJoinOrLeaveArenaFaield>(delegate(ref OnJoinOrLeaveArenaFaield {26120})
			{
				if ({26120}.Error == 4)
				{
					new {17312}(Local.enterArenaServerError_tournament);
				}
				else
				{
					new {17312}(Local.enterArenaServerError_other);
				}
				{18807} currentInstance = {18807}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.RemoveFromContainer();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnLoadArena>(delegate(ref OnLoadArena {26121})
			{
				MapArenaInfo mapArenaInfo = Gameplay.ArenaMaps.FromID((int){26121}.MapID);
				Session.CurrentArenaSession = new ArenaCacheItem(mapArenaInfo, {26121}.InitialFortStrength, {26121}.InitialTowersStrength, {26121}.Mode, {26121}.MyTeamID, {26121}.YourComandUid);
				Session.CachedUiArenaMode = null;
				if (!Global.Game.IsActive)
				{
					Global.Game.FlashWindow();
					Global.Game.SoundSystem.PlaySound(GameStaticSoundName.Bell, 0.03f, 1f);
				}
				Session.Account.PowerupItemsReload.Clear();
				Global.Player.Position = {26121}.NewPosition.Position;
				Global.Player.Rotation = {26121}.NewPosition.Rotation;
				Global.Player.TeleportMapChange(mapArenaInfo);
				{18807} currentInstance = {18807}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.RemoveFromContainer();
				}
				{18826} currentInstance2 = {18826}.CurrentInstance;
				if (currentInstance2 != null)
				{
					currentInstance2.RemoveFromContainer();
				}
				Global.Render.PostProcess.GradientAnimationBegin(2000f, false, false);
				Session.LoadedArenaUiInfo = new OnLoadArena?({26121});
				new {18909}({26121}, false, null);
				if (!Session.CurrentArenaSession.ModeInfo.IsDuetl)
				{
					{18937} currentInstance3 = {18937}.CurrentInstance;
					if (currentInstance3 != null)
					{
						currentInstance3.RemoveFromContainer();
					}
					new {18937}();
				}
				Global.Camera.StartApproximatingAnimation(Vector3.Up * 15f);
				new UiOpacityAnimation(Global.Game.GetInterfaceManager.Host, 0f, 1f, 3000f);
				EducationHelper.OnArenaWasStarted();
				Global.Game.ScenePort.MakeAccSync();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnCompleteArena>(new ReceiverCallback<OnCompleteArena>(this.{25977}), ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnArenaScoreChanges>(delegate(ref OnArenaScoreChanges {26122})
			{
				if (Session.CurrentArenaSession == null)
				{
					return;
				}
				if ({26122}.shipuID == -2)
				{
					Session.CurrentArenaSession.RemainTimeSec = 120f;
					{18945}.TryShowNotif(Local.remain_time(2), Local.end_of_arena_loot, null);
					return;
				}
				Session.CurrentArenaSession.Team1Score = {26122}.newTeam1Score;
				Session.CurrentArenaSession.Team2Score = {26122}.newTeam2Score;
				Session.CurrentArenaSession.RemainLoot = {26122}.remainLoot;
				if (Session.CurrentArenaSession.ModeInfo.WinKretery == ArenaWinKretery.LootCount)
				{
					Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({26122}.shipuID);
					if (shipFromUID != null)
					{
						ShipPlayerBase shipPlayerBase = shipFromUID as ShipPlayerBase;
						if (shipPlayerBase != null)
						{
							if (shipPlayerBase.ResourcesOfHold != null)
							{
								shipPlayerBase.ResourcesOfHold[19] = 0;
							}
							ShipOtherPlayer shipOtherPlayer = shipFromUID as ShipOtherPlayer;
							if (shipOtherPlayer != null)
							{
								shipOtherPlayer.Client.ItemsInHoldExemplary[19] = 0;
							}
							if (shipFromUID == Global.Player)
							{
								Global.Player.UpdateCapacity();
							}
						}
					}
					string str = Session.IsShipContainsPlayerGroup({26122}.shipuID) ? Local.ally : Local.enemy;
					{19994}.Me({19988}.Info, str + {26122}.playerName + Local.NetworkManager_120, Array.Empty<object>());
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnMakeArenaUpgrade>(delegate(ref OnMakeArenaUpgrade {26123})
			{
				Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({26123}.ShipUID);
				if (shipFromUID != null)
				{
					ShipPlayerBase shipPlayerBase = shipFromUID as ShipPlayerBase;
					if (shipPlayerBase != null)
					{
						ArenaUpgradeInfo {2735} = Gameplay.ArenaUpgrades.FromID((int){26123}.upgradeId);
						shipPlayerBase.UsedShipPlayer.MakeArenaUpgrade({2735}, shipPlayerBase);
						if (shipPlayerBase == Global.Player)
						{
							{18417} currentInstance = {18417}.CurrentInstance;
							if (currentInstance != null)
							{
								currentInstance.ReceivedResponse({26123});
							}
							Global.Game.SoundSystem.PlaySound(GameStaticSoundName.AchievOrLevelUp, 0.03f, 1f);
						}
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnRunAnimationExitOfEventMap>(delegate(ref OnRunAnimationExitOfEventMap {26124})
			{
				Global.Render.PostProcess.GradientAnimationBegin(1500f, false, true);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnAddDoubloonsMsg>(delegate(ref OnAddDoubloonsMsg {26125})
			{
				LocalSettings settings = Global.Settings;
				settings.GamemodeDoublones.Value = settings.GamemodeDoublones.Value + {26125}.Amount;
				{19994}.Me({19988}.GiveDoubloons, Local.NetworkManager_124({26125}.Amount), Array.Empty<object>());
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnArenaUpdateWaitQueue>(delegate(ref OnArenaUpdateWaitQueue {26126})
			{
				{18807} currentInstance = {18807}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.UpdateQueue({26126}.rangsCurrentMode, {26126}.TimeToStart, {26126}.totalGamesInfo);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnArenaDeadZoneAction>(delegate(ref OnArenaDeadZoneAction {26127})
			{
				if ({26127}.reducingWasStarted && Session.CurrentArenaSession != null)
				{
					Session.CurrentArenaSession.DeadZoneIsReducingNow = true;
					{18945}.TryShowNotif(Local.NetworkManager_125, null, null);
				}
				if ({26127}.killShipUid != -1)
				{
					Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({26127}.killShipUid);
					if (shipFromUID != null)
					{
						if ({26127}.damageToShip >= shipFromUID.UsedShip.FirstHP.Summary)
						{
							{19994}.Me({19988}.InfoRed, Local.ship + ((IClientShip)shipFromUID).GetClient.GetName2() + Local.NetworkManager_127, Array.Empty<object>());
						}
						else if (shipFromUID.uID == Global.Player.uID && !Global.Player.MapInfo.IsPassingUi)
						{
							{19994}.Me({19988}.InfoRed, Local.NetworkManager_128, Array.Empty<object>());
						}
						if ({26127}.damageToShip > 0f)
						{
							shipFromUID.MakeSimpleDamage({26127}.damageToShip);
						}
						if ({26127}.totalSaielsDamage > 0f)
						{
							shipFromUID.MakeSimpleDamageSailes({26127}.totalSaielsDamage);
						}
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnNpcAgressionTargetChanged>(delegate(ref OnNpcAgressionTargetChanged {26128})
			{
				Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({26128}.NpcUID);
				ShipNpc shipNpc = shipFromUID as ShipNpc;
				if (shipNpc != null)
				{
					shipNpc.CurrentAgressionTargetUID = {26128}.NewTargetUID;
					if ({26128}.NewTargetUID == Global.Player.uID)
					{
						if ({26128}.ByRedSpyglass)
						{
							Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.DisasterBell, shipNpc.Position3D, 1f, false);
						}
						if ({26128}.IsInShieldGroup)
						{
							new SignalRocketFSEffect(shipFromUID, Color.OrangeRed);
						}
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnPassingMapTeleportStatusChanged>(delegate(ref OnPassingMapTeleportStatusChanged {26129})
			{
				{22585} currentInstance = {22585}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.RemoveFromContainer();
				}
				PassingMapDiffCard[] {25173} = (from {26130} in {26129}.DiffCards
				select (PassingMapDiffCard){26130}).ToArray<PassingMapDiffCard>();
				MapForPassingInfo mapForPassingInfo = Gameplay.MapsForPassing.FromID((int){26129}.MapID);
				Session.CurrentPassingSession = new PassingCacheItem(mapForPassingInfo, {26129}.DiffLevel, {25173});
				Global.Player.Position = {26129}.NewPosition;
				Global.Player.TeleportMapChange(mapForPassingInfo);
				if (Global.Player.IsPortEntry)
				{
					Global.Game.ScenePort.AcceptExit();
				}
				new NeedleFromWaterEffect(Global.Player.Position, false);
				Global.Player.UsedShipPlayer.MakeArenaUpgrade({26129}.preinstallUpgrades);
				if ({26129}.showWaitPlayerMessage)
				{
					{18945}.TryShowNotif(Local.NewWaveScreen_0, "", new float?(60000f));
				}
				if (mapForPassingInfo.CollisableObjects.Size != 0)
				{
					new {19024}();
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnPassingMapStatusChanged>(delegate(ref OnPassingMapStatusChanged {26131})
			{
				if ({26131}.Status == MassingMapStatus.ExitUi)
				{
					{19994}.Invite(Local.leave_pm, delegate
					{
						Global.Network.Send(default(OnPassingMapExitUi));
					}, new Keys?(Global.Settings.kb_Accept.Key), null, -1f);
				}
				else if ({26131}.Status == MassingMapStatus.CompleteAndTeleport)
				{
					if (!Global.Player.IsDestroyed)
					{
						Global.Player.TeleportMapChange(Gameplay.WorldMap);
						Global.Player.FinishingGamemode({26131}.SyncPositionBeforeComplete);
						if ({26131}.entryToPort)
						{
							Global.Game.ChangeSceneToPort(false, false);
						}
					}
					else
					{
						Global.Player.FinishingGamemode({26131}.SyncPositionBeforeComplete);
					}
					Global.Settings.GamemodeDoublones = 0;
				}
				else if ({26131}.Status == MassingMapStatus.UpdateWave)
				{
					if ({26131}.NextWaveIndex == 0)
					{
						{19024}.ShowDiffCards(Session.CurrentPassingSession.DiffCards);
						Global.Game.SoundSystem.PlaySound(GameStaticSoundName.Mapscreen, 0.03f, 1f);
					}
					if ({18139}.CurrentInstance == null)
					{
						{18945}.CloseExisted();
					}
					if (!Global.Player.IsDestroyed)
					{
						{18945}.TryShowNotif(Local.NewWaveScreen_2({26131}.NextWaveIndex + 1), Local.NewWaveScreen_3, new float?({26131}.WaitTimeForNextAction));
					}
					if ({26131}.NextWaveIndex >= 1)
					{
						for (;;)
						{
							if (Session.Account.OpenedPassingMaps.Count((byte {26132}) => (short){26132} == Global.Player.MapInfo.ID) >= 2)
							{
								break;
							}
							Session.Account.OpenedPassingMaps = Session.Account.OpenedPassingMaps.Concat(new byte[]
							{
								(byte)Global.Player.MapInfo.ID
							}).ToArray<byte>();
						}
					}
				}
				if ({26131}.ItemBonus != null && !{26131}.ItemBonus.IsEmpty)
				{
					CrewNotificationManager.WhenPassingMapWaveBonus({26131}.ItemBonus);
					Global.Player.ResourcesOfHold.Add({26131}.ItemBonus);
				}
				if ({26131}.ScrollsReturn != 0)
				{
					{19994}.MeAndLogbook({19988}.Big_Gray, Local.NetworkManager_129({26131}.ScrollsReturn), new LBFlags?(LBFlags.L1));
					GSI treasuryMaps = Session.Account.TreasuryMaps;
					treasuryMaps[35] = treasuryMaps[35] + {26131}.ScrollsReturn;
				}
				if ({26131}.completedMapStats != null)
				{
					new {19037}({26131}.completedMapStats);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGetFactoryStatus>(delegate(ref OnGetFactoryStatus {26133})
			{
				{18233} currentInstance = {18233}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.ResoponseHandler({26133});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnFactoryOpenOrUpgradeMsg>(delegate(ref OnFactoryOpenOrUpgradeMsg {26134})
			{
				if ({26134}.operation == OnFactoryOpenOrUpgradeMsg.Operation.BuildOrUpgrade || {26134}.operation == OnFactoryOpenOrUpgradeMsg.Operation.BuildOrUpgradeInsuranePaper)
				{
					if (Session.Account.Buildings.Get((int){26134}.placeIndex) == null)
					{
						FactoryType parameter = (FactoryType){26134}.parameter;
						FactoryGameInfo factoryGameInfo = WosbCrafting.FactoriesInfo[parameter];
						factoryGameInfo.BuildHelper(Session.Account, {26134}.operation == OnFactoryOpenOrUpgradeMsg.Operation.BuildOrUpgradeInsuranePaper);
						Tlist<PlayerBuildingState> allBuildings = Session.Account.Buildings.AllBuildings;
						PlayerBuildingState playerBuildingState = new PlayerBuildingState
						{
							LevelIndex = (byte)(({26134}.operation == OnFactoryOpenOrUpgradeMsg.Operation.BuildOrUpgradeInsuranePaper) ? (factoryGameInfo.Levels.Length - 1) : 0),
							PlaceIndex = {26134}.placeIndex,
							Type = parameter
						};
						allBuildings.Add(playerBuildingState);
					}
					else
					{
						Session.Account.Buildings.Get((int){26134}.placeIndex).UpgradeHelper(Session.Account);
					}
					EducationHelper.MakeFlag(EducationOnboarding.BuildFactory, true);
					{19994}.Logbook(Local.lbe_built(Session.Account.Buildings.Get((int){26134}.placeIndex).TypeInfo.Name), LBFlags.L1);
				}
				else if ({26134}.operation == OnFactoryOpenOrUpgradeMsg.Operation.Destroy)
				{
					Tlist<PlayerBuildingState> allBuildings2 = Session.Account.Buildings.AllBuildings;
					PlayerBuildingState playerBuildingState = Session.Account.Buildings.Get((int){26134}.placeIndex);
					allBuildings2.Remove(playerBuildingState);
				}
				else if ({26134}.operation == OnFactoryOpenOrUpgradeMsg.Operation.GetStorage)
				{
					PlayerBuildingState playerBuildingState2 = Session.Account.Buildings.Get((int){26134}.placeIndex);
					FactoryMineLivelInfo level = playerBuildingState2.Level;
					Global.Player.ResourcesOfHold.AddOrRemove(level.ProducedResId, {26134}.parameter);
					{19994}.Me({19988}.GiveResources, Gameplay.ItemsInfo.FromID(level.ProducedResId).Name + " +" + {26134}.parameter.ToString(), Array.Empty<object>());
					{19994}.Logbook(Local.lbe_moved_fromfactory(Gameplay.ItemsInfo.FromID(level.ProducedResId).Name, {26134}.parameter, playerBuildingState2.TypeInfo.Name), LBFlags.L1);
					EducationHelper.MakeFlag(EducationOnboarding.GetItemsFromFactory, true);
				}
				else if ({26134}.operation == OnFactoryOpenOrUpgradeMsg.Operation.AddConsumedRes)
				{
					PlayerBuildingState playerBuildingState3 = Session.Account.Buildings.Get((int){26134}.placeIndex);
					int consumedResId = playerBuildingState3.Level.ConsumedResId;
					Global.Player.ResourcesOfHold.AddOrRemove(consumedResId, -{26134}.parameter);
					{19994}.Logbook(Local.lbe_moved_tofactory(Gameplay.ItemsInfo.FromID(consumedResId).Name, {26134}.parameter, playerBuildingState3.TypeInfo.Name), LBFlags.L0);
				}
				else if ({26134}.operation == OnFactoryOpenOrUpgradeMsg.Operation.AddBondman)
				{
					Global.Player.ResourcesOfHold.AddOrRemove(8, -{26134}.parameter);
					{19994}.Logbook(Local.lbe_moved_tofactory(Gameplay.ItemsInfo.FromID(8).Name, {26134}.parameter, Session.Account.Buildings.Get((int){26134}.placeIndex).TypeInfo.Name), LBFlags.L1);
				}
				else if ({26134}.operation == OnFactoryOpenOrUpgradeMsg.Operation.AddAnimals)
				{
					Global.Player.ResourcesOfHold.AddOrRemove(22, -{26134}.parameter);
					{19994}.Logbook(Local.lbe_moved_tofactory(Gameplay.ItemsInfo.FromID(22).Name, {26134}.parameter, Session.Account.Buildings.Get((int){26134}.placeIndex).TypeInfo.Name), LBFlags.L1);
				}
				{18233} currentInstance = {18233}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.ResoponseHandler({26134});
				}
				Global.Player.UpdateCapacity();
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnPersonalIsleRequest>(delegate(ref OnPersonalIsleRequest {26135})
			{
				if ({26135}.operation == OnPersonalIsleRequest.Operation.BuildIsle)
				{
					Tlist<PersonalIsleStatus> data = Session.Account.PersonalIsles.Data;
					PersonalIsleStatus personalIsleStatus = new PersonalIsleStatus({26135}.placeIndex, {26135}.initialCraftTime);
					data.Add(personalIsleStatus);
					Global.Network.Send(new OnPortStartMsg(Global.Player.ResourcesOfHold, Session.Account.Xp, true, false));
				}
				if ({26135}.operation == OnPersonalIsleRequest.Operation.DestroyIsle)
				{
					Session.Account.PersonalIsles.RemoveIsle((int){26135}.placeIndex, Session.Account);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnQuestsChangesMsg>(delegate(ref OnQuestsChangesMsg {26136})
			{
				if ({26136}.DaysLeft > 0)
				{
					Session.Account.EvaluteDays({26136}.DaysLeft);
					GameplayHelper.AutostartDailyQuestsAndShowMessages(Session.Account);
				}
				if ({26136}.Changes != null)
				{
					int dynamicEffectsCount = Session.Account.Shipyard.CurrentRealShip.DynamicEffectsCount;
					QuestEngine quests = Session.Account.Quests;
					Decorator game = Session.Game;
					quests.CommonApplyChanges(game, {26136}.Changes, null);
					if ({26136}.Changes.CompletedQuests != null && {26136}.Changes.CompletedQuests.Size > 0 && Gameplay.QuestsInfo.FromID({26136}.Changes.CompletedQuests.First().ID).LocationPort != null)
					{
						EducationHelper.MakeFlag(EducationOnboarding.CompleteQuest, false);
					}
					foreach (QuestsSerializedChanges.CompletedQuest completedQuest in ((IEnumerable<QuestsSerializedChanges.CompletedQuest>){26136}.Changes.CompletedQuests))
					{
						if (Gameplay.QuestsInfo.FromID(completedQuest.ID).FirstStep is QuestTransferOrder)
						{
							Global.Player.UpdateCapacity();
						}
						Global.Settings.DisableTrackingForQuests.Remove(completedQuest.ID);
					}
					foreach (int num in ((IEnumerable<int>){26136}.Changes.FailedQuests))
					{
						Global.Settings.DisableTrackingForQuests.Remove(num);
					}
					if (Session.Account.Shipyard.CurrentRealShip.DynamicEffectsCount != dynamicEffectsCount && !Global.Player.IsPortEntry && !Global.Player.IsDestroyed)
					{
						Global.Player.UsedShip.FirstHP.Check(Global.Player.UsedShip.MaxHp);
					}
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGroupRewardReceivedMsg>(delegate(ref OnGroupRewardReceivedMsg {26137})
			{
				Session.Account.Gold += {26137}.gold;
				Session.Account.AddXp({26137}.xp, true);
				string text = ({26137}.gold > 0) ? (Local.bonus_gold + {26137}.gold.ToString()) : "";
				if ({26137}.xp > 0 && {26137}.gold > 0)
				{
					text += ", ";
				}
				if ({26137}.xp > 0)
				{
					text = text + Local.bonus_xp + {26137}.xp.ToString();
				}
				{19994}.MeAndLogbook({19988}.Gold, Local.group_bonus + text, null);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGetQuestMsg>(delegate(ref OnGetQuestMsg {26138})
			{
				QuestInfo questInfo = Gameplay.QuestsInfo.FromID({26138}.ID);
				if (questInfo.Company == QuestCompany.Daily && {19779}.DisablaeTrackingForDaily)
				{
					Global.Settings.DisableTrackingForQuests.AddIfNotContains({26138}.ID);
				}
				Session.Account.Quests.CommonRunQuest({26138}.ID, Session.Account, {26138}.Parameter);
				Global.Player.UpdateCapacity();
				EducationHelper.WhenGetQuest(questInfo);
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnGetTreasuryMapMsg>(delegate(ref OnGetTreasuryMapMsg {26139})
			{
				{26139}.Reward.Apply(Session.Account, true, 1f);
				Global.Player.UpdateCapacity();
				foreach (ComplexBonus.Annotation annotation in {26139}.Reward.DisplayText(1f, false))
				{
					{19994}.MeAndLogbook({19988}.GiveResources, annotation.Text, null);
				}
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnLoadMercanaryOrders>(delegate(ref OnLoadMercanaryOrders {26140})
			{
				new {20816}({26140});
			}, ServerTaskType.NotStated);
			this.NetClient.Routers.Add<OnMercanaryOrderUpdates>(delegate(ref OnMercanaryOrderUpdates {26141})
			{
				{20816} currentInstance = {20816}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.WhenResponse({26141});
				}
				if ({26141}.updateType == OnMercanaryOrderUpdates.Update.Reward)
				{
					PlayerAccount account = Session.Account;
					account.Monets.Value = account.Monets.Value + {26141}.Instance.PaymentForHeadToCustomer;
					if ({26141}.Instance.Target is MercanaryOrder.TargetPlayer)
					{
						{19994}.MeAndLogbook({19988}.GiveDoubloons, Local.mercanary_reward_player({26141}.Instance.PaymentForHeadToCustomer), new LBFlags?(LBFlags.L1));
					}
					if ({26141}.Instance.Target is MercanaryOrder.TargetGuild)
					{
						{19994}.MeAndLogbook({19988}.GiveDoubloons, Local.mercanary_reward_guild({26141}.Instance.PaymentForHeadToCustomer), new LBFlags?(LBFlags.L1));
					}
				}
			}, ServerTaskType.NotStated);
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x001184FE File Offset: 0x001166FE
		public void Stop(bool {25915} = true)
		{
			this.{25985}.Stop({25915}, false, false);
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x00118510 File Offset: 0x00116710
		private void {25916}(OnChatMessageEvent {25917})
		{
			{22478}.Put({25917});
			ShipOtherPlayer fromAccountSID = Global.Game.WorldInstance.GetFromAccountSID({25917}.SID);
			if (fromAccountSID != null && !fromAccountSID.HideNickname && !Session.Account.BlacklistNames.Contains(HashHelper.SrtingHashMd5Based(fromAccountSID.Client.GetRealName())))
			{
				fromAccountSID.Client.ShowTextMessage = new TemporaryEffect<string>(({25917}.Message.Length > 25) ? ({25917}.Message.Substring(0, 23) + "...") : {25917}.Message, 6000f);
			}
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x001185B0 File Offset: 0x001167B0
		private void {25918}(OnCompleteArena {25919})
		{
			string {19059} = null;
			string {19060} = null;
			if ({25919}.mode.GetInfo().WinKretery == ArenaWinKretery.LootCount)
			{
				{19059} = Local.NetworkManager_113({25919}.additionalScoreMyTeam);
				{19060} = Local.NetworkManager_113({25919}.additionalScoreEnemyTeam);
			}
			if ({25919}.PersonalResult.GamemodeRatingChange != 0 && {25919}.mode == ArenaMode.DuelRating)
			{
				{19059} = Local.tourn_points + (({25919}.PersonalResult.GamemodeRatingChange > 0) ? ("+" + {25919}.PersonalResult.GamemodeRatingChange.ToString()) : {25919}.PersonalResult.GamemodeRatingChange.ToString());
			}
			{19037} rewardWindow = new {19037}(new ArenaMode?({25919}.mode), {25919}.PersonalResult, {25919}.TableMyTeam, {25919}.TableEnemyTeam, {19059}, {19060}, 0, {25919}.RewardScrolls, {25919}.RewardMarks, {25919}.PersonalResult.LossTournamentLive ? new byte?({25919}.PersonalResult.RemainLives) : null, null);
			rewardWindow.EvRemoveFromContainer += delegate()
			{
				if (rewardWindow.IsClosedByHand && Session.Account.Analytics.TotalArenaGamesCount >= 7 && Session.Account.IsEducationInProgress(EducationOnboarding.PlayArena7Games, false, false))
				{
					EducationHelper.MakeFlag(EducationOnboarding.PlayArena7Games, true);
				}
			};
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x001186D4 File Offset: 0x001168D4
		private void {25920}(OnTestServerShipStatShift {25921})
		{
			if ({25921}.ship != null)
			{
				PlayerShipInfo {10320} = Gameplay.PlayersInfo.FromID({25921}.ship.ShipInfoId);
				{25921}.ship.Set({10320});
				if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
				{
					Global.Game.ScenePort.UpdateGuiForViewShip();
				}
			}
			if ({25921}.cannon != null)
			{
				CannonGameInfo {10337} = Gameplay.CannonsGameInfo.FromID({25921}.cannon.CannonInfoId);
				{25921}.cannon.Set({10337});
			}
			if ({25921}.unit != null)
			{
				UnitInfo {10352} = Gameplay.UnitsInfo.FromID({25921}.unit.UnitInfoId);
				{25921}.unit.Set({10352});
			}
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0011877C File Offset: 0x0011697C
		private void {25922}(OnSendOrReceiveLSMsg {25923})
		{
			if (!Global.Settings.LsSavedCacheData.HasCache({25923}.Packet.SID))
			{
				Tlist<LSCacheItem> cache = Global.Settings.LsSavedCacheData.Cache;
				LSCacheItem lscacheItem = new LSCacheItem({25923}.Packet.PlayerName, {25923}.Packet.SID, {25923}.targetIsOnline);
				cache.Add(lscacheItem);
			}
			if (string.IsNullOrEmpty({25923}.Packet.Message))
			{
				{22478}.TryOpen({25923}.Packet.SID, {25923}.Packet.PlayerName, true, {25923}.targetIsOnline, "");
				return;
			}
			{22478}.AddMessage({25923}.Packet.SID, {25923}.Packet.PlayerName, {25923}.Packet.Message, {25923}.targetIsOnline, {25923}.guildTag, {25923}.guildRankId);
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00118850 File Offset: 0x00116A50
		private void {25924}(FriendRequest {25925}, bool {25926})
		{
			if (Session.FriendsRequests.Any((FriendRequest {26155}) => {26155}.FromSID == {25925}.FromSID))
			{
				return;
			}
			Session.FriendsRequests.Add({25925});
			if (!{25926})
			{
				{19994}.Invite(Local.NetworkManager_87({25925}.CachedName), delegate
				{
					Global.Network.Send(new OnFriendRequest({25925}.FromSID, true));
				}, new Keys?(Global.Settings.kb_Accept.Key), new Keys?(Global.Settings.kb_Undo.Key), 25000f);
			}
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x001188E4 File Offset: 0x00116AE4
		private void {25927}(Ship {25928}, GSI {25929})
		{
			if ({25929} == null || {25929}.IsEmpty)
			{
				return;
			}
			if ({25928} == Global.Player)
			{
				Global.Player.ResourcesOfHold.Add({25929});
				Global.Player.UpdateCapacity();
				using (IEnumerator<GSILocalEnumerablePair<ResourceInfo>> enumerator = ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){25929}.ResourceInfo).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GSILocalEnumerablePair<ResourceInfo> {5413} = enumerator.Current;
						{19994}.MeAndLogbook({19988}.GiveResources, {5413}.ToStringNC(false), new LBFlags?(LBFlags.L1));
					}
					return;
				}
			}
			GSI itemsInHoldExemplary = ((IClientShip){25928}).GetClient.ItemsInHoldExemplary;
			if (itemsInHoldExemplary == null)
			{
				return;
			}
			itemsInHoldExemplary.Add({25929});
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x00118990 File Offset: 0x00116B90
		private void {25930}(Ship {25931}, GSI {25932})
		{
			if ({25932} == null || {25932}.IsEmpty)
			{
				return;
			}
			if ({25931} == Global.Player)
			{
				Global.Player.ResourcesOfHold.Remove({25932}, 1, false);
				Global.Player.UpdateCapacity();
				using (IEnumerator<GSILocalEnumerablePair<ResourceInfo>> enumerator = ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){25932}.ResourceInfo).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GSILocalEnumerablePair<ResourceInfo> {5413} = enumerator.Current;
						{19994}.MeAndLogbook({19988}.GiveResources, {5413}.ToStringNC(true), null);
					}
					return;
				}
			}
			GSI itemsInHoldExemplary = ((IClientShip){25931}).GetClient.ItemsInHoldExemplary;
			if (itemsInHoldExemplary == null)
			{
				return;
			}
			itemsInHoldExemplary.TryRemove({25932});
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x00118A44 File Offset: 0x00116C44
		public void CorrectPosition(Ship {25933}, in ShipPositionInfo {25934}, float {25935})
		{
			int num = 15;
			float num2 = 0.7f;
			int num3 = 600;
			if ({25935} > (float)num3 || {19717}.IsRedPing)
			{
				return;
			}
			ShipPositionInfo shipPositionInfo;
			if ({25933}.uID != Global.Player.uID)
			{
				shipPositionInfo = {25933}.ReconstructPosition({25935}, {25934});
			}
			else
			{
				shipPositionInfo = {25933}.ReconstructPosition({25935} * 0.5f, {25934});
			}
			ShipPositionInfo createServerTransformPhisycsOnly = {25933}.CreateServerTransformPhisycsOnly;
			Vector2 vector;
			Vector2.Subtract(ref shipPositionInfo.Position, ref createServerTransformPhisycsOnly.Position, out vector);
			float num4 = Geometry.AxisDistance(shipPositionInfo.Rotation, createServerTransformPhisycsOnly.Rotation);
			float num5 = shipPositionInfo.Rotation - createServerTransformPhisycsOnly.Rotation;
			Geometry.AxisNorm(ref num5);
			num5 = (float)Math.Sign(num5);
			float num6 = 0.1f * MathHelper.Clamp({25935} / 10f * {25933}.physicsBody.NowSpeed / 17f, 1f, 2.3f);
			float num7 = 0.025132744f * (({25933}.physicsBody.AngularVelocity == 0f) ? 0f : 1f);
			float num8 = vector.Length() - num6;
			float num9 = num4 - num7;
			vector.Normalize();
			Vector2 vector2;
			vector2.X = ((num8 > 0f) ? (vector.X * num8) : 0f);
			vector2.Y = ((num8 > 0f) ? (vector.Y * num8) : 0f);
			float num10 = (num9 > 0f) ? (num9 * num5) : 0f;
			if (num10 == 0f && vector2.X == 0f && vector2.Y == 0f)
			{
				return;
			}
			if (Math.Abs(num10) > num2 || vector2.Length() > (float)num)
			{
				{25933}.Position += vector2;
				{25933}.Rotation += num10;
				return;
			}
			{25933}.Corrector.Clean();
			{25933}.Corrector.PushLinear(num10, vector2, 2000f);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00118C38 File Offset: 0x00116E38
		public void Update(ref FrameTime {25936})
		{
			this.NetClient.Update(ref {25936});
			if (this.{25985}.gameProcessSettsLoaded)
			{
				{19717}.IsRedPing = ((float)this.{25985}.timeFromLastReceivedPacket.ElapsedMilliseconds > 2f * CommonGameConfig.CurrentSettings.SpecialMsgTimer + 200f);
			}
			if (!Debugger.IsAttached && this.Conection.timeFromLastReceivedPacket.Elapsed.TotalSeconds > 15.0 && this.NetClient.IsRun && Global.Game.GetCurrentSceneName != GameSceneName.Entry)
			{
				bool {25940} = false;
				if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
				{
					{25940} = true;
				}
				if (this.Conection.timeFromLastReceivedPacket.Elapsed.TotalSeconds < 30.0 && this.Conection.timeFromLastSentPacket.Elapsed.TotalSeconds < 10.0)
				{
					Helpers.SendError(new Exception(string.Concat(new string[]
					{
						"TEST3: Timeout, ",
						this.NetClient.clientBase.DebugInformation(),
						", last received: ",
						((int)this.Conection.timeFromLastReceivedPacket.Elapsed.TotalSeconds).ToString(),
						", last sent: ",
						((int)this.Conection.timeFromLastSentPacket.Elapsed.TotalSeconds).ToString(),
						", packets: ",
						EntryPoint.substructPackets(Networking.lastReceiveMessages)
					})), "Timeout", false, false);
				}
				this.{25939}({25940}, DisconnectionFromServerMessage.Timeout.ToStringLocal());
			}
			if (this.{25983})
			{
				Global.Game.RegisterCallBreakToEntryOffline(this.{25984});
				this.{25983} = false;
				return;
			}
			if (Session.CurrentArenaSession != null && Session.CurrentArenaSession.DeadZoneIsReducingNow)
			{
				Session.CurrentArenaSession.DeadZoneDistance = Math.Max(120f, Session.CurrentArenaSession.DeadZoneDistance - {25936}.secElapsed * 1.5f);
			}
			if (Global.Player != null && (Global.Player.MapInfo.IsPassingUi || Global.Player.MapInfo.IsEnableArenaUi))
			{
				if ({25936}.EvaluteTimerMs2(ref this.{25986}))
				{
					LocalSettings settings = Global.Settings;
					int value = settings.GamemodeDoublones.Value;
					settings.GamemodeDoublones.Value = value + 1;
					this.{25986} = 5000f;
				}
				if (Session.CurrentArenaSession != null)
				{
					{25936}.EvaluteTimerSec(ref Session.CurrentArenaSession.RemainTimeSec);
				}
			}
			this.{25985}.Update(ref {25936});
			Session.PortBattleInfo.newTimeoutValue = Math.Max(0f, Session.PortBattleInfo.newTimeoutValue - (float)(Session.PortBattleInfo.timeBoosted ? 2 : 1) * {25936}.secElapsed);
			if (Global.Player != null && Session.CurrentCrewJob != null)
			{
				bool flag = {25936}.EvaluteTimerMs2(ref Session.CurrentCrewJob.RemainTimeMs);
				bool flag2 = (Session.CurrentCrewJob.checkIsAvailable != null && !Session.CurrentCrewJob.checkIsAvailable()) || Global.Player.IsDestroyedOrFlooding || {18139}.CurrentInstance != null;
				if (flag || flag2 || (Session.CurrentCrewJob.CanBeStopped && (InputHelper.IsClick(Keys.W) || InputHelper.IsClick(Keys.Escape) || InputHelper.IsClick(Keys.Enter))))
				{
					if (flag)
					{
						Action onComplete = Session.CurrentCrewJob.onComplete;
						if (onComplete != null)
						{
							onComplete();
						}
					}
					Session.CurrentCrewJob = null;
				}
			}
			if (Session.Account != null)
			{
				Session.Account.Update(Global.Player, (double){25936}.secElapsed);
				if (Global.Player != null)
				{
					Global.Settings.Logbook.Update();
					EducationHelper.Update(ref {25936});
					if (Session.ForceResetPassword)
					{
						{19413}.OpenChangePasswordDialog(true);
						Session.ForceResetPassword = false;
					}
					if (Session.Account.ClientLoadedGoldCompensation > 0)
					{
						{19994}.MeAndLogbook({19988}.Info, Local.removedResCompensation(Session.Account.ClientLoadedGoldCompensation), new LBFlags?(LBFlags.L2));
						Session.Account.ClientLoadedGoldCompensation = 0;
					}
					if (Session.Account.ClientLoadedEventGoldfish > 0)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
						defaultInterpolatedStringHandler.AppendFormatted(Local.lbe_given2(Gameplay.ItemsInfo.FromID(15).Name));
						defaultInterpolatedStringHandler.AppendLiteral(" +");
						defaultInterpolatedStringHandler.AppendFormatted<int>(Session.Account.ClientLoadedEventGoldfish);
						defaultInterpolatedStringHandler.AppendFormatted(Local.pcs);
						string text = defaultInterpolatedStringHandler.ToStringAndClear();
						{19994}.Logbook(text, LBFlags.L1);
						new {19197}(text);
					}
					Session.SaldoAnalytics.Sync();
					if (this.{25982}.Sample(ref {25936}))
					{
						OnAnalyticsResourcesSaldo? onAnalyticsResourcesSaldo = Session.SaldoAnalytics.Peek();
						if (onAnalyticsResourcesSaldo != null)
						{
							this.NetClient.Send(onAnalyticsResourcesSaldo.Value);
						}
					}
				}
				if ({25936}.EvaluteTimerMs2(ref this.timeToUpdateDailies))
				{
					GameplayHelper.AutostartDailyQuestsAndShowMessages(Session.Account);
				}
			}
			else
			{
				this.timeToUpdateDailies = 0f;
			}
			if (Session.Guild != null)
			{
				Session.Guild.Update(ref {25936});
			}
			{25936}.EvaluteTimerSec(ref Session.SecToServerReboot);
			this.NetClient.CompletePackets();
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x00119134 File Offset: 0x00117334
		private void {25937}(CommonGameConfig {25938})
		{
			CommonGameConfig.CurrentSettings = {25938};
			this.{25985}.gameProcessSettsLoaded = true;
			Global.Render.GetSceneManager.TimeSpeed = {25938}.TimeSpeed;
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x00119160 File Offset: 0x00117360
		private void {25939}(bool {25940}, string {25941})
		{
			this.{25984} = {25941};
			if (this.{25985}.isStarted)
			{
				this.{25985}.Stop({25940}, false, false);
			}
			if (Global.Game.GetCurrentSceneName != GameSceneName.Entry)
			{
				this.{25983} = true;
				return;
			}
			Global.Game.SceneEntry.ResetIncludeGameQuery();
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x001191B5 File Offset: 0x001173B5
		public void Send(IPacketBase {25942})
		{
			this.NetClient.Send({25942});
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00119224 File Offset: 0x00117424
		[CompilerGenerated]
		private void {25944}(ref OnLoginResultMsg {25945})
		{
			if (Global.Settings.IsFirstLaunch)
			{
				Global.Settings.SetChatOnlyMyLanguageDefaultValue();
			}
			Global.Settings.IsFirstLaunch = false;
			this.{25979} = false;
			this.{25985}.OnResponseLoginResult({25945});
			if ({25945}.Result == LoginQueryResult.Success && {25945}.SpecialFlags.HasFlag(LoginSpecialFlags.WithTemporaryPassword))
			{
				Session.ForceResetPassword = true;
			}
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x00119294 File Offset: 0x00117494
		[CompilerGenerated]
		private void {25946}(ref OnStartStateMsg {25947})
		{
			OnStartStateMsg x = {25947};
			Action action = delegate()
			{
				this.{25937}(x.ProcessSettings);
				LocalizedDateTime.ServerTimeZone = x.ServerTimezone;
				CommonGlobal.WorldWeather.ClientSetInitialState(x.WeatherState);
				Global.Game.StaticSystem.SetCurrentRainPower();
				Global.Game.WorldInstance.SetPlayerUID(x.PlayerUID);
				Global.Render.GetSceneManager.WorldTime = x.CurrentGameTime;
				Session.EventActionsPipeline.Clear();
				for (int i = 0; i < x.Actions.Size; i++)
				{
					Session.EventActionsPipeline.ActionBeginEvenet(x.Actions.Array[i]);
				}
				Session.EventActionsPipeline.UpdateWaitActions(x.WaitActions);
				if (Session.Account.Rang < 6)
				{
					foreach (EventActionBase eventActionBase in ((IEnumerable<EventActionBase>)Session.EventActionsPipeline.CurrentActions))
					{
						Session.Account.ReadNotificationsEventActions.AddIfNotContains(eventActionBase.AID);
					}
				}
				Session.Guild = x.GuildInfo;
				Session.GuildUnreceivedSalary = x.UnreceivedSalary;
				this.{25979} = true;
			};
			if (Global.Game.SceneEntry.IsEntryToGameAnmation)
			{
				Global.Game.SceneEntry.StartStateMessageHandler = action;
			}
			else
			{
				action();
			}
			if ({25947}.UnreadNotifications.Size > 0)
			{
				int num = 0;
				int num2 = 0;
				foreach (OflineNotification oflineNotification in ((IEnumerable<OflineNotification>){25947}.UnreadNotifications))
				{
					object value = oflineNotification.Value;
					if (value is FriendRequest)
					{
						FriendRequest {25925} = (FriendRequest)value;
						this.{25924}({25925}, true);
						num2++;
					}
					else
					{
						value = oflineNotification.Value;
						if (value is ChatMessageDefault)
						{
							ChatMessageDefault {10428} = (ChatMessageDefault)value;
							this.{25922}(new OnSendOrReceiveLSMsg({10428}, LSFlags.None, false, "", 0));
						}
						else
						{
							LocalizedString localizedString = oflineNotification.Value as LocalizedString;
							if (localizedString != null)
							{
								Global.Settings.Logbook.DontBatchNext();
								{19994}.Logbook(localizedString.ToString(), LBFlags.L1);
								num++;
							}
							else
							{
								AccumulatedAuctionNotification accumulatedAuctionNotification = oflineNotification.Value as AccumulatedAuctionNotification;
								if (accumulatedAuctionNotification != null)
								{
									if (!accumulatedAuctionNotification.TotalGotByOrders.IsEmpty)
									{
										Global.Settings.Logbook.DontBatchNext();
										{19994}.Logbook(Local.notf_ptradeShop_massive(Gameplay.WorldMap.Ports.Array[(int)accumulatedAuctionNotification.PortID].PortName, accumulatedAuctionNotification.TotalGotByOrders.ResourceInfo.ToStringWithComma<ResourceInfo>()), LBFlags.L1);
									}
									if (!accumulatedAuctionNotification.TotalSold.IsEmpty)
									{
										Global.Settings.Logbook.DontBatchNext();
										{19994}.Logbook(Local.notf_ptradeSell_massive(Gameplay.WorldMap.Ports.Array[(int)accumulatedAuctionNotification.PortID].PortName, accumulatedAuctionNotification.TotalSold.ResourceInfo.ToStringWithComma<ResourceInfo>(), accumulatedAuctionNotification.TotalSoldGold), LBFlags.L1);
									}
									num++;
								}
							}
						}
					}
				}
				if (num > 0)
				{
					Session.NotificationsTooltip = true;
				}
			}
			Session.PlayerPublicDesigns = {25947}.MyPublicDesigns;
			Session.PublicDesignsCache = {25947}.MyPublicDesigns.Clone();
			ShipDesignInfo.PublicDesignResolver = ((int {25992}) => Session.PublicDesignsCache.FirstOrDefault((PublicDesignInfo {26142}) => {26142}.ID == {25992}));
			ShipDesignInfo.ServerIdParam = Global.GetCurrentServer().Id;
			Session.CurrentPortAuctionResources = x.CurrentPortAuctionResources;
			Session.SecToServerReboot = x.SecToReboot;
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x0011953C File Offset: 0x0011773C
		[CompilerGenerated]
		private void {25948}(ref OnPingMsg {25949})
		{
			if (Global.Game.GetCurrentSceneName != GameSceneName.Entry)
			{
				Session.LastPing = {25949}.SecoundPing;
				Session.LastOnline = (int){25949}.Online;
				Global.Game.WorldInstance.ShipSilhouettes.Push({25949}.ShipSilhouettes);
				Session.ArenaGames = {25949}.arenaGamesCount;
				Global.Player.MapInfo.Weather.CorrectWavesPosition({25949}.wavesPosition);
				if (Global.Game.GetCurrentSceneName == GameSceneName.Game && {25949}.resInHold != null && !{25949}.resInHold.Equals(Global.Player.ResourcesOfHold) && !Global.Player.IsDestroyed)
				{
					string {25571} = GSI.PutDiff({25949}.resInHold, Global.Player.ResourcesOfHold);
					this.{25987}.AssertEquality("hold-diff", 3, {25571}, "");
				}
				else
				{
					this.{25987}.AssertEquality("hold-diff", 3, "", "");
				}
				if (Global.Game.GetCurrentSceneName == GameSceneName.Game)
				{
					this.{25988}.AssertEquality("XP", 3, Session.Account.Xp.ToString(), {25949}.serverXp.ToString());
				}
				if (Global.Player != null && Global.Game.GetCurrentSceneName == GameSceneName.Game)
				{
					short num = Global.Player.IsDestroyed ? short.MaxValue : ((short)Global.Player.UsedShip.CompressedHP);
					if (Math.Abs((int)(num - {25949}.Debug_HpFactor)) <= 2)
					{
						num = {25949}.Debug_HpFactor;
					}
					this.{25990}.AssertEquality("client-hp", 3, num.ToString(), {25949}.Debug_HpFactor.ToString());
					if ({25949}.Debug_HpFactor == 32767 && num != {25949}.Debug_HpFactor && !Global.Player.IsDestroyed)
					{
						Global.Player.ForceDestroy(false);
					}
				}
				foreach (OnChatMessageEvent {25917} in ((IEnumerable<OnChatMessageEvent>){25949}.newChatMessages))
				{
					this.{25916}({25917});
				}
			}
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x00119754 File Offset: 0x00117954
		[CompilerGenerated]
		private void {25950}(ref OnAcceptLocSetKeys {25951})
		{
			if ((short){25951}.MapID == Global.Player.MapInfo.ID)
			{
				this.CorrectPosition(Global.Player, {25951}.PositionInfo, Session.LastPing);
			}
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x00119784 File Offset: 0x00117984
		[CompilerGenerated]
		private void {25952}(ref OnCorrectMovMsg {25953})
		{
			try
			{
				foreach (ShipPositionInfoWithUid shipPositionInfoWithUid in ((IEnumerable<ShipPositionInfoWithUid>){25953}.Data))
				{
					Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID(shipPositionInfoWithUid.UID);
					if (shipFromUID != null)
					{
						this.CorrectPosition(shipFromUID, shipPositionInfoWithUid.Position, Session.LastPing);
					}
				}
			}
			catch (Exception {25356})
			{
				Helpers.SendError({25356}, "OnCorrectMovMsg", true, false);
			}
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x00119814 File Offset: 0x00117A14
		[CompilerGenerated]
		private void {25954}(ref OnExternalGPSChangeMsg {25955})
		{
			this.{25937}({25955}.ProcessSettings);
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x00119822 File Offset: 0x00117A22
		[CompilerGenerated]
		private void {25956}(ref OnChatMessageEvent {25957})
		{
			this.{25916}({25957});
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x00119830 File Offset: 0x00117A30
		[CompilerGenerated]
		private void {25958}(ref OnSendOrReceiveLSMsg {25959})
		{
			if ({22478}.CurrentInstance != null)
			{
				if ({25959}.Flags == LSFlags.InMyBlacklist || {25959}.Flags == LSFlags.InOtherBlacklist)
				{
					if (!{22478}.SpecialMessage({25959}.Packet.SID, Local.StringConstants_1))
					{
						new {17312}(Local.StringConstants_1);
						return;
					}
				}
				else
				{
					this.{25922}({25959});
				}
			}
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00119888 File Offset: 0x00117A88
		[CompilerGenerated]
		private void {25960}(ref OnReferalNewStatusMsg {25961})
		{
			switch ({25961}.StateCode)
			{
			case ReferalState.ConnectedNewPlayer:
				{19994}.MeAndLogbook({19988}.Big_Gray, Local.lbe_ref_joined({25961}.NewFriendName), Local.lbe_ref_joined({25961}.NewFriendName), null);
				this.{25924}(new FriendRequest({25961}.NewFriendSID, {25961}.NewFriendName, 2), true);
				return;
			case ReferalState.BonusForFriend:
			{
				string text = Gameplay.AddBonusReferalOrigin(Session.Account, new DateTime({25961}.serverTime));
				{19994}.MeAndLogbook({19988}.Big_Gray, text, text, null);
				return;
			}
			case ReferalState.BonusForJoin:
			{
				string text = Gameplay.AddBonusReferalInvited(Session.Account, new DateTime({25961}.serverTime));
				Session.HadReferalBonus = true;
				return;
			}
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00119940 File Offset: 0x00117B40
		[CompilerGenerated]
		private void {25962}(ref OnFortRobbingFinished {25963})
		{
			Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({25963}.ShipUID);
			if (shipFromUID == null)
			{
				return;
			}
			if (shipFromUID == Global.Player)
			{
				string text = ({25963}.GivenResources == null || {25963}.GivenResources.IsEmpty) ? Local.frotRob_noRes : Local.frotRob_success;
				{19994}.MeAndLogbook({19988}.Info, text, text, null);
				if ({25963}.DisplayGoldToGuild > 0)
				{
					{19994}.MeAndLogbook({19988}.Gold, Local.frotRob_gold({25963}.DisplayGoldToGuild), null);
				}
			}
			if ({25963}.GivenResources != null)
			{
				this.{25927}(shipFromUID, {25963}.GivenResources);
			}
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x001199E0 File Offset: 0x00117BE0
		[CompilerGenerated]
		private void {25964}(ref OnBoardingFinishFinalPart {25965})
		{
			for (int i = 0; i < BoardingFSEffect.ActiveEffects.Size; i++)
			{
				BoardingFSEffect.ActiveEffects.Array[i].CheckAndQueueToRemove({25965}.shipUid, {25965}.shipUid);
			}
			Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({25965}.shipUid);
			if (shipFromUID == null)
			{
				return;
			}
			Global.Game.WorldInstance.DisconnectBoardingHooks(shipFromUID.uID);
			int num = 0;
			if ({25965}.IsWin == BoardingResult.Fail)
			{
				if ({25965}.manTheCrew != null)
				{
					Assert.Report(!shipFromUID.UsedShip.Crew.Remove({25965}.manTheCrew, false, out num), "CAPTURED ApplyDestruct + manTheCrewCount: " + num.ToString());
				}
				if ({25965}.cannonsChanges != null)
				{
					shipFromUID.UsedShip.Cannons.RemoveStolenCannons({25965}.cannonsChanges);
					if (shipFromUID == Global.Player)
					{
						{19994}.MeAndLogbook({19988}.GiveResources, Local.lb_mycannonsstolen({25965}.cannonsChanges.CannonGameInfo.First<GSILocalEnumerablePair<CannonGameInfo>>().Info.Name), new LBFlags?(LBFlags.L1));
					}
				}
			}
			else
			{
				if ({25965}.manTheCrew != null && shipFromUID == Global.Player)
				{
					foreach (GSILocalEnumerablePair<UnitInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<UnitInfo>>){25965}.manTheCrew.UnitInfo))
					{
						if (gsilocalEnumerablePair.Info.Type == UnitType.Special)
						{
							{19994}.MeAndLogbook({19988}.GiveCrew, gsilocalEnumerablePair.Info.Name + " " + Local.on_ship, new LBFlags?(LBFlags.L1));
						}
					}
					GSI gsi = shipFromUID.UsedShip.Crew.AddHelper({25965}.manTheCrew, Global.Player);
					if (!gsi.IsEmpty)
					{
						{19994}.MeAndLogbook({19988}.GiveCrew, Local.lb_captured_crew(gsi.GetTotalItemsCount()), null);
					}
					EducationHelper.MakeFlag(EducationOnboarding.CaptureBondman, true);
				}
				if ({25965}.cannonsChanges != null && shipFromUID == Global.Player)
				{
					Session.Account.CannonsInHold.Add({25965}.cannonsChanges);
					foreach (GSILocalEnumerablePair<CannonGameInfo> gsilocalEnumerablePair2 in ((IEnumerable<GSILocalEnumerablePair<CannonGameInfo>>){25965}.cannonsChanges.CannonGameInfo))
					{
						{19994}.MeAndLogbook({19988}.GiveCrew, Local.lb_mycannonsgiven(gsilocalEnumerablePair2.Count, gsilocalEnumerablePair2.Info.Name), new LBFlags?(LBFlags.L1));
					}
				}
			}
			if ({25965}.burnShip)
			{
				shipFromUID.CreateBoardingBurning(-1, PredefinedBurningType.Infinite);
			}
			if ({25965}.restoreStrength > 0f)
			{
				shipFromUID.RestoreHp({25965}.restoreStrength);
			}
			if (shipFromUID == Global.Player)
			{
				Ship shipFromUID2 = Global.Game.WorldInstance.GetShipFromUID({25965}.secondShipUid);
				bool flag;
				string text = (shipFromUID2 == null) ? "?" : ((IClientShip)shipFromUID2).GetClient.GetName(out flag);
				{18139} currentInstance = {18139}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.Close();
				}
				if ({25965}.IsWin == BoardingResult.Fail)
				{
					Session.WReload.ResetAll();
					Global.Player.UsedShip.Cannons.BeginReloadWeapons(Global.Player, null, null, null);
				}
				if ({25965}.IsWin == BoardingResult.Fail)
				{
					if (!string.IsNullOrEmpty(text))
					{
						{19994}.Logbook(Local.lbe_baording_failed(text), LBFlags.L0);
					}
					if (num > 0)
					{
						{19994}.MeAndLogbook({19988}.GiveCrew, Local.lb_captured_byenemy(num), new LBFlags?(LBFlags.L0));
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(text))
					{
						{19994}.Logbook(Local.lbe_baording_won(text), LBFlags.L0);
					}
					if ({25965}.restoreStrength > 0f)
					{
						{19994}.Me({19988}.Okay, Local.strength + " +" + ((int){25965}.restoreStrength).ToString(), Array.Empty<object>());
					}
				}
				if ({25965}.addGold != 0)
				{
					{19994}.MeAndLogbook({19988}.Gold, Local.gold + (({25965}.addGold > 0) ? " +" : " ") + {25965}.addGold.ToString(), null);
					Session.Account.Gold += {25965}.addGold;
				}
				if ({25965}.BonusXP > 0)
				{
					{19994}.MeAndLogbook({19988}.GiveScrolls, Local.xp_plus({25965}.BonusXP), null);
					Session.Account.AddXp({25965}.BonusXP, true);
				}
			}
			if ({25965}.IsWin == BoardingResult.Win)
			{
				this.{25927}(shipFromUID, {25965}.HoldChanges);
				return;
			}
			this.{25930}(shipFromUID, {25965}.HoldChanges);
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00119E70 File Offset: 0x00118070
		[CompilerGenerated]
		private void {25966}(ref OnCrewChangesMsg {25967})
		{
			Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({25967}.ShipUID);
			if (shipFromUID != null)
			{
				int num = 0;
				if ({25967}.destructions != null)
				{
					if ({25967}.mode == BoardingFinishMode.LootUnitInSea)
					{
						shipFromUID.UsedShip.Crew.Add({25967}.destructions, true);
					}
					else
					{
						shipFromUID.UsedShip.Crew.Remove({25967}.destructions, false, out num);
					}
					if ({25967}.serverUnitsTest != null && !this.{25991} && shipFromUID.UsedShip.Crew.ToString() != {25967}.serverUnitsTest.ToString())
					{
						Helpers.SendError(new Exception(string.Concat(new string[]
						{
							"CAPTURED ApplyDestruct + changesAmount: ",
							num.ToString(),
							Environment.NewLine,
							"Client: ",
							shipFromUID.UsedShip.Crew.ToString(),
							Environment.NewLine,
							"Server: ",
							{25967}.serverUnitsTest.ToString()
						})), "OnCrewChangesMsg + " + {25967}.mode.ToString(), false, false);
						this.{25991} = true;
					}
					if ({25967}.mode == BoardingFinishMode.LeftAndDead && shipFromUID == Global.Player)
					{
						{19994}.MeAndLogbook({19988}.Minus, Local.lb_crew_leftdead({25967}.destructions.GetTotalItemsCount()), null);
					}
					if ({25967}.mode == BoardingFinishMode.UsedForActivity && shipFromUID == Global.Player)
					{
						{19994}.MeAndLogbook({19988}.Minus, Local.lb_crew_used({25967}.destructions.GetTotalItemsCount()), null);
					}
					if ({25967}.mode == BoardingFinishMode.DeathFromHunger && shipFromUID == Global.Player)
					{
						{19994}.MeAndLogbook({19988}.Minus, Local.lb_deathFromHunger("-" + {25967}.destructions.GetTotalItemsCount().ToString()), null);
						Session.Account.FoodAtShip.ApplyDeathFromHunger(Global.Player);
					}
				}
				if ({25967}.mode == BoardingFinishMode.CaptureFort && shipFromUID == Global.Player)
				{
					{19994}.MeAndLogbook({19988}.Minus, Local.NetworkManager_46 + {25967}.destructions.GetTotalItemsCount().ToString(), null);
					{19994}.MeAndLogbook({19988}.Info, Local.NetworkManager_45({25967}.LossOfFortCount), null);
				}
			}
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x0011A0C6 File Offset: 0x001182C6
		[CompilerGenerated]
		private void {25968}(ref OnReceiveFriendRequest {25969})
		{
			this.{25924}({25969}.IncomingRequest, false);
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x0011A0D5 File Offset: 0x001182D5
		[CompilerGenerated]
		private void {25970}(ref OnTestServerShipStatShift {25971})
		{
			this.{25920}({25971});
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x0011A0E4 File Offset: 0x001182E4
		[CompilerGenerated]
		private void {25972}(ref OnTestServerShipStatShiftLoad {25973})
		{
			for (int i = 0; i < {25973}.packets.Size; i++)
			{
				this.{25920}({25973}.packets.Array[i]);
			}
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x0011A120 File Offset: 0x00118320
		[CompilerGenerated]
		private void {25974}(ref OnTradeLoadOrUpdateWindow {25975})
		{
			if (PortTradePage_SyncProtected.CurrentInstance != null)
			{
				PortTradePage_SyncProtected.CurrentInstance.Load({25975}.tradeStatus, {25975}.uniqueOffers);
				PortTradePage_SyncProtected.CurrentInstance.EvRemoveFromContainer += this.{25976};
				return;
			}
			this.Send(new OnTradeWindowOpenOrCloseQuery(TradeLoadWindowMode.Close));
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0011A172 File Offset: 0x00118372
		[CompilerGenerated]
		private void {25976}()
		{
			this.Send(new OnTradeWindowOpenOrCloseQuery(TradeLoadWindowMode.Close));
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x0011A188 File Offset: 0x00118388
		[CompilerGenerated]
		private void {25977}(ref OnCompleteArena {25978})
		{
			if ({25978}.BallsOfHold != null)
			{
				Session.Account.Shipyard.CurrentRealShip.BallsOfHold.Clean();
				Session.Account.Shipyard.CurrentRealShip.BallsOfHold.Add({25978}.BallsOfHold);
				Session.Account.Shipyard.CurrentRealShip.PrivateResourcesOfHold[8] = 0;
			}
			if ({25978}.PowderKegsOfHold != null)
			{
				Session.Account.Shipyard.CurrentRealShip.PowderKegsOfHold.Clean();
				Session.Account.Shipyard.CurrentRealShip.PowderKegsOfHold.Add({25978}.PowderKegsOfHold);
			}
			if ({25978}.PowerupItemsAtStorage != null)
			{
				Session.Account.PowerupItemsAtStorage.Clean();
				Session.Account.PowerupItemsAtStorage.Add({25978}.PowerupItemsAtStorage);
			}
			if ({25978}.RewardScrolls != 0)
			{
				GSI treasuryMaps = Session.Account.TreasuryMaps;
				treasuryMaps[35] = treasuryMaps[35] + {25978}.RewardScrolls;
			}
			if ({25978}.RewardMarks != 0)
			{
				Session.Account.Shipyard.CurrentRealShip.PrivateResourcesOfHold.AddOrRemove(37, {25978}.RewardMarks);
			}
			{25978}.PersonalResult.Bonus.Apply(Session.Account);
			PlayerAccount account = Session.Account;
			account.ArenaRating.Value = account.ArenaRating.Value + {25978}.PersonalResult.ArenaRatingChange;
			PlayerAccount account2 = Session.Account;
			account2.ArenaCurrency.Value = account2.ArenaCurrency.Value + {25978}.PersonalResult.ArenaRatingChange;
			Session.Account.ArenaPenaltySec += {25978}.PersonalResult.AddArenaPenltyTime;
			Global.Settings.GamemodeDoublones = 0;
			if ({25978}.TableMyTeam.Size == 0 && {25978}.TableEnemyTeam.Size == 0)
			{
				return;
			}
			Session.Account.Analytics.TotalArenaGamesCount++;
			if ({25978}.PersonalResult.Result == ArenaBattleResult.YourComandWin)
			{
				Session.Account.Analytics.TotalArenaWins++;
			}
			if ({25978}.teleportToBackMap)
			{
				Global.Player.TeleportMapChange(Gameplay.WorldMap);
				Global.Player.FinishingGamemode({25978}.PositionToSync);
				Session.Account.PowerupItemsReload.Clear();
				if ({25978}.entryToPort)
				{
					Global.Game.ChangeSceneToPort(false, false);
				}
				else
				{
					Global.Game.GetInterfaceManager.ClearAll();
					Global.Game.SceneGame.CreateInterface();
				}
				{18937} currentInstance = {18937}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.RemoveFromContainer();
				}
				this.{25918}({25978});
			}
			else
			{
				OnCompleteArena cached = {25978};
				{19994}.Invite(Local.watch_arena_result, delegate
				{
					this.{25918}(cached);
				}, null, null, -1f);
			}
			if ({25978}.PersonalResult.Result == ArenaBattleResult.YourComandWin)
			{
				{19994}.Logbook(Local.lbe_arena_win({25978}.mode.ArenaModeName(false)), LBFlags.L0);
				return;
			}
			if ({25978}.PersonalResult.Result == ArenaBattleResult.YourComandFail)
			{
				{19994}.Logbook(Local.lbe_arena_lost({25978}.mode.ArenaModeName(false)), LBFlags.L0);
				return;
			}
			{19994}.Logbook(Local.lbe_arena_tie({25978}.mode.ArenaModeName(false)), LBFlags.L0);
		}

		// Token: 0x04001EB2 RID: 7858
		public readonly Networking NetClient;

		// Token: 0x04001EB3 RID: 7859
		private bool {25979};

		// Token: 0x04001EB4 RID: 7860
		private Timer {25980} = new Timer(10000f);

		// Token: 0x04001EB5 RID: 7861
		private Timer {25981} = new Timer(1000f);

		// Token: 0x04001EB6 RID: 7862
		private Timer {25982} = new Timer(180000f);

		// Token: 0x04001EB7 RID: 7863
		private volatile bool {25983};

		// Token: 0x04001EB8 RID: 7864
		private string {25984};

		// Token: 0x04001EB9 RID: 7865
		private ConnectionModule {25985};

		// Token: 0x04001EBA RID: 7866
		private float {25986} = 1f;

		// Token: 0x04001EBB RID: 7867
		private SyncErrorTracker {25987} = new SyncErrorTracker();

		// Token: 0x04001EBC RID: 7868
		private SyncErrorTracker {25988} = new SyncErrorTracker();

		// Token: 0x04001EBD RID: 7869
		private SyncErrorTracker {25989} = new SyncErrorTracker();

		// Token: 0x04001EBE RID: 7870
		private SyncErrorTracker {25990} = new SyncErrorTracker();

		// Token: 0x04001EBF RID: 7871
		private bool {25991};

		// Token: 0x04001EC0 RID: 7872
		public float timeToUpdateDailies;
	}
}
