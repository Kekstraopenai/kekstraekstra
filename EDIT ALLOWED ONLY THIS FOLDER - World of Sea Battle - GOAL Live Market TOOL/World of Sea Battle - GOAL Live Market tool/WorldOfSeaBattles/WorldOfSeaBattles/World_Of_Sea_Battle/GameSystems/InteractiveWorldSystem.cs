using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using ManualPacketSerialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Core;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface.Controls;
using TheraEngine.Scene.ParticleSystem;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Interface;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.GameSystems
{
	// Token: 0x020004C4 RID: 1220
	internal sealed class InteractiveWorldSystem : GameSceneSystem
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x000EEF64 File Offset: 0x000ED164
		[TupleElementNames(new string[]
		{
			"isle",
			"data",
			"secFromEvent"
		})]
		public Tlist<ValueTuple<Isle, DynamicBuildCreatePacket, float>> BuildingsCache
		{
			[return: TupleElementNames(new string[]
			{
				"isle",
				"data",
				"secFromEvent"
			})]
			get
			{
				return this.{24804};
			}
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000EEF7C File Offset: 0x000ED17C
		public override void Initialize(ContentManager {24780})
		{
			this.{24804} = new Tlist<ValueTuple<Isle, DynamicBuildCreatePacket, float>>(17);
			this.VisibleResearchPoints = new Tlist<TraderInSeaPlaceInfo>();
			this.ContainsTraderPositionsInSea = new Tlist<TraderInSeaPlaceInfo>();
			this.ContainsWanderingTradersInSea = new Tlist<ShipNpc>();
			this.ContainsWorldFactories = new Tlist<FactoryPlaceIsleInfo>();
			this.ContainsWorldQuests = new Tlist<QuestInfo>();
			this.VisibleWorldQuests = new Tlist<QuestInfo>();
			base.Initialize({24780});
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000EEFE0 File Offset: 0x000ED1E0
		public override void Off()
		{
			this.{24804}.Clear();
			this.VisibleResearchPoints.Clear();
			this.ContainsTraderPositionsInSea.Clear();
			this.ContainsWanderingTradersInSea.Clear();
			this.ContainsWorldFactories.Clear();
			this.ContainsWorldQuests.Clear();
			this.VisibleGuildFort = null;
			this.ContainsGuildFort = null;
			this.ContainsWorldActivity = null;
			this.EmpireLootNotification = null;
			base.Off();
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000EF058 File Offset: 0x000ED258
		public override void Update(ref FrameTime {24781})
		{
			if (Global.Player == null)
			{
				return;
			}
			for (int i = 0; i < this.{24804}.Size; i++)
			{
				ValueTuple<Isle, DynamicBuildCreatePacket, float>[] array = this.{24804}.Array;
				int num = i;
				array[num].Item3 = array[num].Item3 + {24781}.secElapsed;
			}
			this.VisibleResearchPoints.Clear();
			this.ContainsTraderPositionsInSea.Clear();
			this.ContainsWanderingTradersInSea.Clear();
			this.ContainsWorldFactories.Clear();
			this.ContainsWorldQuests.Clear();
			this.VisibleWorldQuests.Clear();
			this.VisibleGuildFort = null;
			this.ContainsGuildFort = null;
			this.VisibleTowerForCapturing = null;
			this.ContainsTowerForCapturing = null;
			this.ContainsWorldActivity = null;
			this.ContainsLighthouseEnteringZone = false;
			this.EmpireLootNotification = null;
			WorldMapInfo mapInfo = Global.Player.MapInfo;
			Vector2 position = Global.Player.Position;
			if (mapInfo.IsWorldmap)
			{
				using (IEnumerator<FactoryPlaceIsleInfo> enumerator = ((IEnumerable<FactoryPlaceIsleInfo>)mapInfo.Factories).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						FactoryPlaceIsleInfo worldMine = enumerator.Current;
						if (worldMine.ContainsInteropZone(position, 0f) && (!worldMine.IsHidden || Session.Account.PersonalIsles.Data.Contains((PersonalIsleStatus {24811}) => (int){24811}.PlaceIndex == worldMine.FcID)) && (worldMine.Predefines.First() != FactoryType.Temp_PersonalIsle || Session.Account.PersonalIsleLimit > 0))
						{
							this.ContainsWorldFactories.Add(worldMine);
						}
					}
				}
				if (!Global.Player.IsPortEntry)
				{
					foreach (QuestInfo questInfo in Session.Account.Quests.GetOpenedQuests(Global.Player.NearPort, null, false, false))
					{
						this.VisibleWorldQuests.Add(questInfo);
						if (ref questInfo.LocationPos.DTest(position, (float)QuestInfo.GiveQuestInSeaRadius.Value))
						{
							this.ContainsWorldQuests.Add(questInfo);
						}
					}
				}
				float num2 = float.MaxValue;
				float num3 = float.MaxValue;
				if (!Global.Player.UsedShip.StaticInfo.IsBalloon)
				{
					foreach (ValueTuple<Isle, DynamicBuildCreatePacket, float> valueTuple in ((IEnumerable<ValueTuple<Isle, DynamicBuildCreatePacket, float>>)this.{24804}))
					{
						Isle item = valueTuple.Item1;
						DynamicBuildCreatePacket item2 = valueTuple.Item2;
						IMPSerializable visualData = item2.VisualData;
						if (visualData is GuildBuildingVisualData)
						{
							GuildBuildingVisualData guildBuildingVisualData = (GuildBuildingVisualData)visualData;
							PbsBuildingPlaceInfo pbsBuildingPlaceInfo = Gameplay.WorldMap.Ports.Array[(int)guildBuildingVisualData.PortID].PbSystem[(int)guildBuildingVisualData.PlaceIDInPort];
							float num4 = Vector2.DistanceSquared(position, pbsBuildingPlaceInfo.Position);
							if (item.Statement.GroupID == WorldObjectID.WGuildFortTower && num4 < num3 && guildBuildingVisualData.StatusIcon != 5 && guildBuildingVisualData.StatusIcon != 6 && item2.NowStrength == 0f)
							{
								num3 = num4;
								this.VisibleTowerForCapturing = item;
								if (num4 < pbsBuildingPlaceInfo.TowerCapturingRadius * pbsBuildingPlaceInfo.TowerCapturingRadius && Session.EngagingInPortBattle == PbsBatlleSide.Attacker)
								{
									this.ContainsTowerForCapturing = item;
								}
							}
							if (guildBuildingVisualData.StatusIcon != 5 && guildBuildingVisualData.StatusIcon != 6 && item2.NowStrength > 0f)
							{
								Decorator game = Session.Game;
								float num5 = WosbPbs.TimeToStartTowerAttackMs(game, guildBuildingVisualData.StatusIcon == 2, Session.Game.NearPortStatus);
								if (num5 == -1f)
								{
									item.GuildTowerAttackTimerMs = 0f;
								}
								else
								{
									if (Vector2.Distance(Global.Player.Position, pbsBuildingPlaceInfo.Position) < guildBuildingVisualData.AttackTimerMaxDistance && !Global.Player.IsStatic)
									{
										item.GuildTowerAttackTimerMs += {24781}.msElapsed;
									}
									else
									{
										item.GuildTowerAttackTimerMs -= {24781}.msElapsed;
									}
									item.GuildTowerAttackTimerMs = Math.Clamp(item.GuildTowerAttackTimerMs, 0f, num5);
								}
							}
							else
							{
								item.GuildTowerAttackTimerMs = 0f;
							}
							if (item.Statement.GroupID == WorldObjectID.WGuildFort && num4 < num2)
							{
								num2 = num4;
								this.VisibleGuildFort = item;
								this.VisibleGuildFortData = guildBuildingVisualData;
								if (num4 < pbsBuildingPlaceInfo.EntryPointPerRadius * pbsBuildingPlaceInfo.EntryPointPerRadius)
								{
									this.ContainsGuildFort = item;
									bool flag;
									if (Session.EngagingInPortBattle == PbsBatlleSide.Attacker)
									{
										int portID = (int)guildBuildingVisualData.PortID;
										IslePortInfo engagingInPortBattlePort = Session.EngagingInPortBattlePort;
										int? num6 = (engagingInPortBattlePort != null) ? new int?(engagingInPortBattlePort.PortID) : null;
										flag = (portID == num6.GetValueOrDefault() & num6 != null);
									}
									else
									{
										flag = false;
									}
									bool flag2 = flag;
									GuildCommon guild = Session.Guild;
									if (((guild != null) ? guild.Tag : null) == guildBuildingVisualData.CapturedByGuild && Session.EngagingInPortBattle == PbsBatlleSide.None)
									{
										this.ContainsGuildFortMode = InteractiveWorldSystem.FortInteropMode.Managment;
									}
									else if (guildBuildingVisualData.FortStatus == GuildFortStatus.AllowCapturing && flag2)
									{
										this.ContainsGuildFortMode = InteractiveWorldSystem.FortInteropMode.CapturingByUnits;
									}
									else if (guildBuildingVisualData.FortStatus == GuildFortStatus.AllowRobbingFirstTime && flag2)
									{
										this.ContainsGuildFortMode = InteractiveWorldSystem.FortInteropMode.RobbingFirstTime;
									}
									else if (guildBuildingVisualData.FortStatus == GuildFortStatus.AllowRobbingSecoundTime && flag2)
									{
										this.ContainsGuildFortMode = InteractiveWorldSystem.FortInteropMode.RobbingSecoundTime;
									}
									else
									{
										this.ContainsGuildFortMode = InteractiveWorldSystem.FortInteropMode.No;
										this.ContainsGuildFort = null;
									}
								}
							}
						}
					}
				}
				if (!Global.Player.UsedShip.StaticInfo.IsBalloon)
				{
					PlayerWorldActivityStatus playerWorldActivityStatus = Session.Account.WorldActivities.FindNear(Global.Player.Position, (PlayerWorldActivityStatus {24805}) => {24805}.Position, (float)QuestInfo.GiveQuestInSeaRadius.Value);
					if (playerWorldActivityStatus.Position != Vector2.Zero && playerWorldActivityStatus.TimeToFinishSec == 0f)
					{
						this.ContainsWorldActivity = new PlayerWorldActivityStatus?(playerWorldActivityStatus);
					}
				}
				bool containsLighthouseEnteringZone = false;
				if (Session.Account.CaptainSkills[PDynamicAccountBonus.BAllowEnterThroughLighthouse] > 0 && !Session.Account.TensityMode && Session.EngagingInPortBattle == PbsBatlleSide.None)
				{
					IslePortInfo np = Global.Player.NearPort;
					WorldMapInfo mapInfo2 = Global.Player.MapInfo;
					Vector2 position2 = Global.Player.Position;
					IslePortInfo islePortInfo = mapInfo2.FindNearPortBy(position2, (IslePortInfo {24812}) => {24812} != np);
					if (np.ReferencedPharosesTransformed.Concat(islePortInfo.ReferencedPharosesTransformed).Any((IslePortPharosInfo {24806}) => Vector2.Distance({24806}.MapGlobalPosition, Global.Player.Position) < 60f))
					{
						containsLighthouseEnteringZone = true;
					}
				}
				this.ContainsLighthouseEnteringZone = containsLighthouseEnteringZone;
				this.EmpireLootNotification = Global.Game.WorldInstance.DropList.FindNear(Global.Player.Position, delegate(ClientDrop {24807})
				{
					if ({24807}.ModelType != DropModel.EmpireUnderwaterBox || !{24807}.Available)
					{
						return new Vector2(10000000f, 0f);
					}
					return {24807}.Position;
				});
				ClientDrop empireLootNotification = this.EmpireLootNotification;
				if (empireLootNotification != null && empireLootNotification.ModelType == DropModel.EmpireUnderwaterBox && (this.EmpireLootNotification == null || Vector2.Distance(this.EmpireLootNotification.Position, Global.Player.Position) <= 200f))
				{
					if (Global.Player.IsOutsideSeam)
					{
						goto IL_75B;
					}
					WorldMapInfo mapInfo3 = Global.Player.MapInfo;
					Vector2 position2 = this.EmpireLootNotification.Position;
					if (mapInfo3.IsInsideMap(position2, true))
					{
						goto IL_75B;
					}
				}
				this.EmpireLootNotification = null;
			}
			IL_75B:
			foreach (ValueTuple<Isle, DynamicBuildCreatePacket, float> valueTuple2 in ((IEnumerable<ValueTuple<Isle, DynamicBuildCreatePacket, float>>)this.{24804}))
			{
				if (valueTuple2.Item2.NowStrength == 0f && {24781}.secElapsed > 0f && Rand.Chanse(5f * {24781}.Factor))
				{
					WorldObjectID groupID = valueTuple2.Item1.Statement.GroupID;
					bool flag3 = groupID == WorldObjectID.WGuildFort || groupID == WorldObjectID.WorldFort;
					if (flag3)
					{
						FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall(valueTuple2.Item1.SceneObject.Transform.Transform3X3(valueTuple2.Item1.SceneObject.CombinedModelSpaceBS.Center), Vector3.Up * 6f), 20f, 1000f, 6000f, 0f, false, Rand.Pick<FXEngine.PowderParticleType>(new FXEngine.PowderParticleType[]
						{
							FXEngine.PowderParticleType.Dark,
							FXEngine.PowderParticleType.GrayDim
						}), 0.5f, null);
					}
					else
					{
						FXEngine.SampleFlameAndSmoke(valueTuple2.Item1.SceneObject.Transform.Transform3X3(valueTuple2.Item1.SceneObject.CombinedModelSpaceBS.Center + new Vector3(0f, valueTuple2.Item1.SceneObject.CombinedModelSpaceBS.Radius * 0.6f, 0f)), 4f, false, true, false, null, 1f);
					}
				}
			}
			if (!Global.Player.IsPortEntry)
			{
				foreach (TraderInSeaPlaceInfo traderInSeaPlaceInfo in ((IEnumerable<TraderInSeaPlaceInfo>)mapInfo.TradersInSea))
				{
					float num7 = Vector2.DistanceSquared(traderInSeaPlaceInfo.Position, position);
					if (num7 < 122500f && num7 < 1944.8098f)
					{
						this.ContainsTraderPositionsInSea.Add(traderInSeaPlaceInfo);
					}
				}
				if (Global.Player.MapInfo.IsWorldmap)
				{
					foreach (ShipNpc shipNpc in ((IEnumerable<ShipNpc>)Global.Game.WorldInstance.NpcList))
					{
						if (shipNpc.IsActiveWanderingTrader && Vector2.DistanceSquared(shipNpc.Position, position) < 1225f)
						{
							this.ContainsWanderingTradersInSea.Add(shipNpc);
						}
					}
				}
			}
			this.ActualInteropMessageString = null;
			this.InteropIsBoarding = false;
			this.InteropIsWhaleHarpoon = false;
			this.ShowArenaLootTip = null;
			bool flag4 = false;
			if (!Global.Player.IsDestroyed && (!Global.Player.MapInfo.IsEducationMap || ({18593}.CurrentInstance != null && {18593}.CurrentInstance.IsEntryToPortTask)) && !Global.Player.IsInPortEnteringNow)
			{
				bool flag5 = Global.Settings.kb_Action.IsClick && GameScene.GameHasInputFocus && {18139}.CurrentInstance == null && !TextBox.IsThereInput && !KeyInputControl.IsInputElements;
				QuestRunningProgress questRunningProgress;
				if (Session.Account.Quests.ProgressRunningQuests.TryFind((QuestRunningProgress {24808}) => {24808}.CurrentStep is QuestIndividualLoot && {24808}.RuntimeParameter != null && Vector2.Distance({24808}.RuntimeParameter.PositionParameter, Global.Player.Position) < 16f + Global.Player.UsedShip.StaticInfo.CorpusHalfLength, out questRunningProgress))
				{
					this.ActualInteropMessageString = "[" + Keys.Space.GetKeyName() + "] - " + Local.ClientDrop_4.ToLower();
					flag4 = true;
					if (flag5)
					{
						Global.Network.Send(new OnGetTreasuryMapMsg(questRunningProgress.Info.ID));
					}
				}
				Ship ship;
				if (Global.Game.WorldInstance.GetClosestBoardingTarget(out ship))
				{
					this.BoardingModeUnitsCountProblem = ((float)ship.UsedShip.Crew.Count * 0.7f > (float)Global.Player.UsedShip.Crew.Count);
					bool flag6 = ((IClientShip)ship).GetClient.StatusColor == HealthBarStyle.Lime;
					if (flag6)
					{
						this.ActualInteropMessageString = Local.boarding_problem_ally;
					}
					else if (Session.WReload.BoardingHookReloadingLeftSec > 0f)
					{
						this.ActualInteropMessageString = string.Concat(new string[]
						{
							"[",
							Keys.Space.GetKeyName(),
							"] - ",
							Local.boarding,
							" ",
							Math.Round((double)Session.WReload.BoardingHookReloadingLeftSec).ToString()
						});
					}
					else
					{
						this.ActualInteropMessageString = string.Concat(new string[]
						{
							"[",
							Keys.Space.GetKeyName(),
							"] - ",
							Local.boarding,
							" ",
							((IClientShip)ship).GetClient.GetName2()
						});
					}
					if (!flag6)
					{
						flag4 = true;
						this.InteropIsBoarding = true;
					}
					if (flag5 && ((!this.BoardingModeUnitsCountProblem && !flag6) || WosbBoarding.BoardingTestingMode))
					{
						InGameSightUi.FalkonetSights.TryRunBoardingHooks(ship);
					}
				}
				else
				{
					ClientDrop {24295};
					if (Global.Game.WorldInstance.GetClosestWhaleForHarpoon(out {24295}))
					{
						ClientDrop nearDrop = ClientDrop.nearDrop;
						if (nearDrop != null && !nearDrop.IsLooting)
						{
							this.WhaleNoHarpoonProblem = (Global.Player.ResourcesOfHold[36] == 0);
							this.ActualInteropMessageString = "[" + Keys.Space.GetKeyName() + "] - " + Local.whale_harpoon;
							this.InteropIsWhaleHarpoon = true;
							if (flag5 && !this.WhaleNoHarpoonProblem && Session.WReload.BoardingHookReloadingLeftSec == 0f)
							{
								WhaleHarpoonController.Click({24295});
								goto IL_153A;
							}
							goto IL_153A;
						}
					}
					if ((Global.Player.IsEntryToPortZoneContains || this.ContainsLighthouseEnteringZone) && !Global.Player.IsPortEntry)
					{
						if (Global.Player.GetBattleTimer > 0f)
						{
							this.ActualInteropMessageString = Local.PlayerShipGui_8(Global.Player.GetBattleTimerSec);
						}
						else
						{
							this.ActualInteropMessageString = "port";
							flag4 = true;
							if (flag5)
							{
								if (this.ContainsLighthouseEnteringZone)
								{
									if (Session.CurrentCrewJob == null && Global.Player.CheckBattleTimer())
									{
										Session.CurrentCrewJob = new ApplyingEffectCache(Local.PlayerShipGui_7, 5000f, new Action(this.{24788}), new Func<bool>(this.{24789}));
									}
								}
								else
								{
									Global.Player.PortEntering(this.ContainsLighthouseEnteringZone);
								}
							}
						}
					}
					else if ((this.ContainsWorldFactories.Size > 0 || this.ContainsWorldQuests.Size > 0) && Session.CurrentCrewJob == null)
					{
						this.ActualInteropMessageString = "[" + Global.Settings.kb_Action.KeyToString + "] - " + Local.GameSettingsWindow_91;
						flag4 = true;
						if (this.ContainsWorldFactories.Size > 0 && flag5 && {18233}.CurrentInstance == null && Global.Player.CheckBattleTimer())
						{
							if (this.ContainsWorldFactories.First().Predefines.Contains(FactoryType.Temp_PersonalIsle))
							{
								if (Session.Account.PersonalIsles.Data.Find(new Func<PersonalIsleStatus, bool>(this.{24790})) == null)
								{
									if (Session.Account.Rang < EducationHelper.PersonalIsleAvailableSinceRank)
									{
										{19994}.Me({19988}.InfoRed, Local.CaptainSkillsInfoWindow_243(EducationHelper.PersonalIsleAvailableSinceRank), Array.Empty<object>());
									}
									else if (Session.Account.PersonalIsles.Data.Size >= Session.Account.PersonalIsleLimit && !Global.Player.DebugEnabled)
									{
										{19994}.Me({19988}.InfoRed, Local.personal_isle_open_err(Session.Account.PersonalIsles.Data.Size, Session.Account.PersonalIsleLimit), Array.Empty<object>());
									}
									else
									{
										new {17107}("", Local.WorldMapUi_personalIsle + Local.personal_isle_open(Session.Account.PersonalIsles.Data.Size, Session.Account.PersonalIsleLimit), "", new Action<int>(this.{24792}), true, null, new string[]
										{
											Local.personal_isle_start,
											Local.close
										});
									}
								}
								else
								{
									Global.Network.Send(new OnPortStartMsg(Global.Player.ResourcesOfHold, Session.Account.Xp, true, false));
								}
							}
							else
							{
								new {18233}(this.ContainsWorldFactories.First());
							}
						}
						if (this.ContainsWorldQuests.Size > 0 && flag5 && {19779}.CurrentInstance == null)
						{
							new {19779}(true, null, null);
						}
					}
					else if (this.ContainsTowerForCapturing != null)
					{
						this.ActualInteropMessageString = "[" + Global.Settings.kb_Action.KeyToString + "] - " + Local.PlayerShipGui_11_a;
						flag4 = true;
						if (flag5 && Session.CurrentCrewJob == null)
						{
							Session.CurrentCrewJob = new ApplyingEffectCache(Local.capturing, 30000f, new Action(this.{24796}), new Func<bool>(this.{24797}))
							{
								CanBeStopped = true
							};
						}
					}
					else if (this.ContainsGuildFort != null && this.ContainsGuildFortMode != InteractiveWorldSystem.FortInteropMode.No)
					{
						this.ActualInteropMessageString = "[" + Global.Settings.kb_Action.KeyToString + "] - " + ((this.ContainsGuildFortMode == InteractiveWorldSystem.FortInteropMode.Managment) ? Local.PlayerShipGui_10 : ((this.ContainsGuildFortMode == InteractiveWorldSystem.FortInteropMode.CapturingByUnits) ? Local.PlayerShipGui_11 : Local.PlayerShipGui_12));
						flag4 = true;
						if (flag5)
						{
							if (this.ContainsGuildFortMode == InteractiveWorldSystem.FortInteropMode.Managment && {17107}.CurrentInstance == null && {19275}.CurrentInstance == null)
							{
								if (Global.Player.CheckBattleTimer())
								{
									Global.Network.Send(new OnBuildingHoldOperatingMsg(this.ContainsGuildFort.DynamicObjectUID.Value, new GSI(), new GSI(), false));
								}
							}
							else if (Session.CurrentCrewJob == null && {18139}.CurrentInstance == null)
							{
								if (this.ContainsGuildFortMode == InteractiveWorldSystem.FortInteropMode.RobbingFirstTime || this.ContainsGuildFortMode == InteractiveWorldSystem.FortInteropMode.RobbingSecoundTime)
								{
									new {17312}((this.ContainsGuildFortMode == InteractiveWorldSystem.FortInteropMode.RobbingFirstTime) ? Local.pb_robbing_first : Local.pb_robbing_any, new Action(this.{24798}), delegate()
									{
									});
								}
								else if (Global.Player.UsedShip.Crew.Raw.IsEmpty)
								{
									{19994}.Me({19988}.Info, Local.ClientDrop_0, Array.Empty<object>());
								}
								else
								{
									Session.CurrentCrewJob = new ApplyingEffectCache(Local.capturing, 10000f, new Action(this.{24801}), new Func<bool>(this.{24802}));
								}
							}
						}
					}
					else if (this.ContainsTraderPositionsInSea.Size != 0)
					{
						this.ActualInteropMessageString = "[" + Global.Settings.kb_Action.KeyToString + "] - " + Local.GameSettingsWindow_91;
						flag4 = true;
						if (flag5 && {18417}.CurrentInstance == null)
						{
							if (Global.Player.MapInfo.IsWorldmap)
							{
								new {18417}(this.ContainsTraderPositionsInSea.Array[0], 0);
							}
							else
							{
								new {18417}(true, false);
							}
						}
					}
					else if (this.ContainsWanderingTradersInSea.Size != 0)
					{
						this.ActualInteropMessageString = "[" + Global.Settings.kb_Action.KeyToString + "] - " + Local.GameSettingsWindow_91;
						flag4 = true;
						if (flag5 && {18417}.CurrentInstance == null)
						{
							new {18417}(null, this.ContainsWanderingTradersInSea.Array[0].uID);
						}
					}
					else if (Session.ServerWorldStatus.NearPbAvanpostStatus != PbsEngageProblem.None && Session.EngagingInPortBattle == PbsBatlleSide.None && !Global.Player.IsPortEntry)
					{
						string str = Local.enterPb;
						if (Session.ServerWorldStatus.NearPbAvanpostStatus != PbsEngageProblem.Display_Allow)
						{
							str = Local.enterPb_problem;
						}
						else
						{
							flag4 = true;
							if (flag5)
							{
								new {17312}(Local.enterPb + "?", delegate()
								{
									Global.Network.Send(new OnRegisterToBattleMsg(true, Global.Player.NearPort.PortID));
								}, delegate()
								{
								});
							}
						}
						this.ActualInteropMessageString = "[" + Global.Settings.kb_Action.KeyToString + "] - " + str;
					}
					else if (Global.Player.ProtectionSafeZoneTimeout > 0f)
					{
						this.ActualInteropMessageString = (Global.Player.IsProtectionSafeZoneTimeoutNotFrozen ? (Local.PlayerShipGui_9 + Math.Round((double)Global.Player.ProtectionSafeZoneTimeout).ToString()) : Local.PlayerShipGui_9);
					}
					else if (Session.CurrentArenaSession != null && Global.Player.MapInfo.IsEnableArenaUi)
					{
						if ((Session.CurrentArenaSession.ModeInfo.EnableDooblonsAndUpgrades || Session.CurrentArenaSession.ModeInfo.EnableHoldManagement) && {18139}.CurrentInstance == null && Vector2.Distance(Session.CurrentArenaSession.MyBasePosition, position) < 37f && {18909}.CurrentInstance == null)
						{
							flag4 = true;
							this.ActualInteropMessageString = "[" + Global.Settings.kb_Action.KeyToString + "] - " + Local.equipment2;
							this.ShowArenaLootTip = ((Global.Player.ResourcesOfHold[19] > 0) ? ((Global.Player.GetBattleTimer == 0f) ? Local.stop_to_leave_loot : Local.PlayerShipGui_8(Global.Player.GetBattleTimerSec)) : null);
							if ({18417}.CurrentInstance == null && flag5)
							{
								new {18417}(Session.CurrentArenaSession.ModeInfo.EnableDooblonsAndUpgrades, Session.CurrentArenaSession.ModeInfo.EnableHoldManagement);
							}
						}
						else
						{
							{18417} currentInstance = {18417}.CurrentInstance;
							if (currentInstance != null)
							{
								currentInstance.RemoveFromContainer();
							}
						}
					}
				}
			}
			IL_153A:
			this.CurrentPickDropKey = ((!flag4) ? Global.Settings.kb_Action.Key : Global.Settings.kb_Accept.Key);
			if (Session.WorldMapMarkerPosition == null)
			{
				if (Session.Account.Quests.ProgressRunningQuests.Any(delegate(QuestRunningProgress {24809})
				{
					if ({24809}.Info.Company == QuestCompany.Education)
					{
						QuestFollowToPortHeader questFollowToPortHeader = {24809}.CurrentStep as QuestFollowToPortHeader;
						if (questFollowToPortHeader != null)
						{
							return questFollowToPortHeader.ToMyHomePort;
						}
					}
					return false;
				}) && {22913}.CurrentInstance == null)
				{
					Session.WorldMapMarkerPosition = new Vector2?(Gameplay.WorldMap.Ports[(int)Session.Account.StartPortId].EntryPos);
				}
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000F06CC File Offset: 0x000EE8CC
		public void RenderBuildings2D()
		{
			if (this.{24804}.Size > 0)
			{
				Engine.GS.SetTexture(AtlasGameGui.Texture);
				foreach (ValueTuple<Isle, DynamicBuildCreatePacket, float> valueTuple in ((IEnumerable<ValueTuple<Isle, DynamicBuildCreatePacket, float>>)this.{24804}))
				{
					Isle item = valueTuple.Item1;
					DynamicBuildCreatePacket item2 = valueTuple.Item2;
					if (item2.MaxStrength < 10000000f)
					{
						HealthBarStyle {24245} = HealthBarStyle.Red;
						string text = "";
						Rectangle? rectangle = null;
						bool flag = true;
						string text2 = string.Empty;
						IMPSerializable visualData = item2.VisualData;
						if (visualData is GuildBuildingVisualData)
						{
							GuildBuildingVisualData guildBuildingVisualData = (GuildBuildingVisualData)visualData;
							flag = (guildBuildingVisualData.ShowHpAndAllowDamage && Session.Guild != null);
							if (Session.Guild != null)
							{
								{24245} = (InteractiveWorldSystem.<RenderBuildings2D>g__IsAllyPort|30_0(guildBuildingVisualData) ? HealthBarStyle.Lime : HealthBarStyle.Red);
							}
							if (item.Statement.GroupID == WorldObjectID.WGuildFort)
							{
								if (!string.IsNullOrEmpty(guildBuildingVisualData.CapturedByGuild))
								{
									text = guildBuildingVisualData.CapturedByGuild;
								}
								text2 = ((guildBuildingVisualData.FortStatus == GuildFortStatus.AllowRobbingFirstTime) ? Local.PlayerShipGui_12 : text2);
								text2 = ((guildBuildingVisualData.FortStatus == GuildFortStatus.AllowCapturing) ? Local.capture : text2);
							}
							if (item.Statement.GroupID == WorldObjectID.WGuildFortTower)
							{
								if (guildBuildingVisualData.StatusIcon == 5)
								{
									rectangle = new Rectangle?(new Rectangle(2288, 236, 43, 44));
								}
								else if (item2.NowStrength == 0f)
								{
									rectangle = new Rectangle?(new Rectangle(2288, 196, 43, 39));
								}
								else if (item2.NowStrength < WosbPbs.StrengthDisablingAttack)
								{
									rectangle = new Rectangle?(new Rectangle(2332, 241, 43, 39));
								}
							}
						}
						else
						{
							visualData = item2.VisualData;
							if (visualData is ArenaOrPassingVisualData)
							{
								ArenaOrPassingVisualData arenaOrPassingVisualData = (ArenaOrPassingVisualData)visualData;
								if (Global.Player.MapInfo.IsEnableArenaUi && Session.CurrentArenaSession != null)
								{
									{24245} = ((arenaOrPassingVisualData.ArenaTeam == Session.CurrentArenaSession.MyTeamID) ? HealthBarStyle.Lime : HealthBarStyle.Red);
									if (arenaOrPassingVisualData.StatusIcon == 1)
									{
										text2 = Local.pbsstatus_protection;
									}
								}
							}
							else
							{
								visualData = item2.VisualData;
								if (visualData is WorldFortVisualData)
								{
									WorldFortVisualData worldFortVisualData = (WorldFortVisualData)visualData;
									flag = !worldFortVisualData.IsDestroyed;
								}
							}
						}
						flag &= (item2.NowStrength != 0f | item2.UnitsCount > 0);
						WorldObjectID groupID = item.Statement.GroupID;
						bool flag2 = groupID - WorldObjectID.ArenaFort <= 1 || groupID == WorldObjectID.WorldFort;
						int num = flag2 ? 25 : 21;
						Vector2? vector = HealthBarHelper.RenderTowerStrengthAtlGameGui({24245}, item.Statement, item2.NowStrength, item2.MaxStrength, !flag, new float?((float)num), text2, (item.Statement.GroupID == WorldObjectID.WGuildFort) ? 1.1f : 0.8f, text2.Length > 0);
						if (vector != null)
						{
							if (!string.IsNullOrEmpty(text) && Global.Render.UiMode == InterfaceMode.Default)
							{
								Vector2 value = vector.Value - new Vector2(0f, 26f);
								Device gs = Engine.GS;
								Vector2 vector2 = value + new Vector2(-41f, -28f);
								Color color = Color.White;
								gs.Draw(AtlasGameGui.fort_guildTagForm, vector2, color);
								Engine.GS.SetFont(Fonts.Philosopher_14Bold);
								Device gs2 = Engine.GS;
								string {14590} = text;
								vector2 = value + new Vector2(0f, 4f);
								color = Color.White;
								gs2.DrawStringCenteredShadow({14590}, vector2, color, 0.8f);
							}
							if (rectangle != null)
							{
								if (Session.EngagingInPortBattle != PbsBatlleSide.None)
								{
									Device gs3 = Engine.GS;
									Rectangle value2 = rectangle.Value;
									Vector2 vector2 = vector.Value + new Vector2((float)(-(float)rectangle.Value.Width / 2), (float)(-50 - rectangle.Value.Height / 2));
									Color color = Color.White * (float)(0.699999988079071 + 0.30000001192092896 * Math.Sin(Global.Game.GameTotalTimeSec * 4.0));
									gs3.Draw(value2, vector2, color);
								}
							}
							else
							{
								visualData = item2.VisualData;
								if (visualData is GuildBuildingVisualData)
								{
									GuildBuildingVisualData guildBuildingVisualData2 = (GuildBuildingVisualData)visualData;
									if (item.IsVisibleByDist && Session.EngagingInPortBattle == PbsBatlleSide.None && !InteractiveWorldSystem.<RenderBuildings2D>g__IsAllyPort|30_0(guildBuildingVisualData2) && Session.Account.WorldFlag != OpenWorldFlag.Peaceful)
									{
										float num2 = 0.5f;
										float num3 = 0f;
										if (item.GuildTowerAttackTimerMs > 0f)
										{
											Decorator game = Session.Game;
											float num4 = WosbPbs.TimeToStartTowerAttackMs(game, guildBuildingVisualData2.StatusIcon == 2, Session.Game.NearPortStatus);
											num3 = item.GuildTowerAttackTimerMs / num4;
										}
										if (num3 > 0f)
										{
											Vector2 vector3 = vector.Value - new Vector2((float)AtlasObjs.marker_sightingRed.Width * num2 * AtlasObjs.MarkerSightingRedScaleX / 2f, 0f);
											Engine.GS.SetTexture(AtlasObjs.Texture);
											Engine.GS.DrawProgressbarVertical(AtlasObjs.marker_sightingRed, AtlasObjs.marker_sightingRedFree, vector3, num3, num2 * AtlasObjs.MarkerSightingRedScaleY, new Color?((num3 == 1f) ? (Color.White * 0.66f) : Color.White));
											Engine.GS.SetTexture(AtlasGameGui.Texture);
										}
									}
								}
							}
						}
					}
				}
				Engine.GS.SetTexture(AtlasObjs.Texture);
			}
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x000F0C6C File Offset: 0x000EEE6C
		public bool BuildingHadEvents(in Vector2 {24782})
		{
			for (int i = 0; i < this.{24804}.Size; i++)
			{
				if (this.{24804}.Array[i].Item3 < 10f && this.{24804}.Array[i].Item2.Position == {24782})
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x000F0CD8 File Offset: 0x000EEED8
		public void CreateOrUpdateDyanmicBuildings(OnCreateorUpdateDynamicBuildings {24783})
		{
			if ({24783}.createBuildings != null)
			{
				foreach (DynamicBuildCreatePacket dynamicBuildCreatePacket in ((IEnumerable<DynamicBuildCreatePacket>){24783}.createBuildings))
				{
					Isle item = this.{24784}(dynamicBuildCreatePacket);
					Tlist<ValueTuple<Isle, DynamicBuildCreatePacket, float>> tlist = this.{24804};
					ValueTuple<Isle, DynamicBuildCreatePacket, float> valueTuple = new ValueTuple<Isle, DynamicBuildCreatePacket, float>(item, dynamicBuildCreatePacket, 500f);
					tlist.Add(valueTuple);
				}
			}
			if ({24783}.changeBuildings != null)
			{
				foreach (DynamicBuildVisibleChanges dynamicBuildVisibleChanges in ((IEnumerable<DynamicBuildVisibleChanges>){24783}.changeBuildings))
				{
					bool flag = false;
					int i = 0;
					while (i < this.{24804}.Size)
					{
						if (this.{24804}.Array[i].Item2.uIDinScene == dynamicBuildVisibleChanges.uID)
						{
							flag = true;
							if (dynamicBuildVisibleChanges.IsChangesNowStrength && dynamicBuildVisibleChanges.NowStrength == 0f && this.{24804}.Array[i].Item2.NowStrength > 0f)
							{
								FXEngine.TowerDestruct(this.{24804}.Array[i].Item1.Statement);
							}
							bool flag2;
							this.{24804}.Array[i].Item2.Actualize(dynamicBuildVisibleChanges, out flag2);
							if (flag2)
							{
								this.{24804}.Array[i].Item1.Dispose();
								Global.Game.WorldInstance.MapObjectLayer.FastRemove(this.{24804}.Array[i].Item1);
								Global.Game.WorldInstance.MapVisibleObjectLayer.FastRemove(this.{24804}.Array[i].Item1);
								Isle item2 = this.{24784}(this.{24804}.Array[i].Item2);
								this.{24804}.Array[i].Item1 = item2;
							}
							this.{24804}.Array[i].Item3 = 0f;
							if (this.{24804}.Array[i].Item1.Statement.GroupID != WorldObjectID.WGuildFortTower)
							{
								break;
							}
							float num = float.MaxValue;
							int num2 = -1;
							for (int j = 0; j < this.{24804}.Size; j++)
							{
								if (this.{24804}.Array[j].Item1.Statement.GroupID == WorldObjectID.WGuildFort)
								{
									float num3;
									Vector2.DistanceSquared(ref this.{24804}.Array[j].Item2.Position, ref this.{24804}.Array[i].Item2.Position, out num3);
									if (num3 < num)
									{
										num = num3;
										num2 = j;
									}
								}
							}
							if (num2 != -1)
							{
								this.{24804}.Array[num2].Item3 = 0f;
								break;
							}
							break;
						}
						else
						{
							i++;
						}
					}
					Assert.Report(!flag, "changeBuildings not found");
				}
			}
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000F1038 File Offset: 0x000EF238
		private Isle {24784}(DynamicBuildCreatePacket {24785})
		{
			int {16658} = -1;
			DynamicBuildingInfo dynamicBuildingInfo = Gameplay.DyanmicBuildingsTable.Array[(int){24785}.ModelID];
			IMPSerializable visualData = {24785}.VisualData;
			if (visualData is GuildBuildingVisualData)
			{
				GuildBuildingVisualData guildBuildingVisualData = (GuildBuildingVisualData)visualData;
				{16658} = Gameplay.WorldMap.Ports.Array[(int)guildBuildingVisualData.PortID].PbSystem[(int)guildBuildingVisualData.PlaceIDInPort].ReplaceTerrainID;
			}
			IsleInstance isleInstance = Gameplay.WorldMap.GetVisibleIsles({24785}.Position).FindNear({24785}.Position, delegate(IsleInstance {24810})
			{
				if (!{24810}.ModelData.Path.Contains("decor") && {24810}.ReplacebleXfxID != -1)
				{
					return {24810}.GlobalPosition;
				}
				return new Vector2(100000f);
			});
			if (isleInstance != null)
			{
				{16658} = isleInstance.ReplacebleXfxID;
			}
			Isle isle = new Isle(new IsleInstance({24785}, false), {16658});
			isle.DynamicObjectUID = new int?({24785}.uIDinScene);
			if (isle.Statement.ModelData.DesturctionParts.Size > 0)
			{
				isle.AddGetHp(delegate
				{
					foreach (ValueTuple<Isle, DynamicBuildCreatePacket, float> valueTuple in ((IEnumerable<ValueTuple<Isle, DynamicBuildCreatePacket, float>>)this.{24804}))
					{
						if (valueTuple.Item2.uIDinScene == {24785}.uIDinScene)
						{
							return valueTuple.Item2.NowStrength / valueTuple.Item2.MaxStrength;
						}
					}
					return 1f;
				});
			}
			Tlist<Isle> mapObjectLayer = Global.Game.WorldInstance.MapObjectLayer;
			for (int i = 0; i < mapObjectLayer.Size; i++)
			{
				if (mapObjectLayer.Array[i].Statement.GlobalPosition == isle.Statement.GlobalPosition && mapObjectLayer.Array[i].Statement.GroupID == isle.Statement.GroupID)
				{
					for (int j = 0; j < this.{24804}.Size; j++)
					{
						if (this.{24804}.Array[j].Item1 == mapObjectLayer.Array[i])
						{
							mapObjectLayer.Array[i].Dispose();
							mapObjectLayer.RemoveAt(i);
							this.{24804}.RemoveAt(j);
							bool {25297} = true;
							string[] array = new string[6];
							array[0] = "AppendDynamicBuilding building is exists already, nearPort: ";
							array[1] = Global.Player.NearPort.PortName;
							array[2] = ", playerPos: ";
							array[3] = Global.Player.Position.ToString();
							array[4] = ", buildingPos: ";
							int num = 5;
							Vector2 position = {24785}.Position;
							array[num] = position.ToString();
							Assert.Report({25297}, string.Concat(array));
							break;
						}
					}
				}
			}
			mapObjectLayer.Add(isle);
			if (isle.Statement.GroupID == WorldObjectID.WGuildFort)
			{
				visualData = {24785}.VisualData;
				if (visualData is GuildBuildingVisualData)
				{
					GuildBuildingVisualData guildBuildingVisualData2 = (GuildBuildingVisualData)visualData;
					if (guildBuildingVisualData2.FlagImage.Length != 0)
					{
						isle.CustomLoadedFlagTexture = new VirtualTexture("dispose", VisualHelper.LoadTexture2DFromBytes(guildBuildingVisualData2.FlagImage, int.MaxValue, false));
					}
					else
					{
						ShipDesignInfo shipDesignInfo;
						ShipDesignInfo shipDesignInfo2;
						LocalContent.GetFractionGeraldics(Session.Game.NearPortStatus.CapturerFraction, out shipDesignInfo, out shipDesignInfo2);
						if (shipDesignInfo != null)
						{
							isle.CustomLoadedFlagTexture = DesignElementExtenstions.DesignElementTexture(shipDesignInfo);
						}
					}
				}
			}
			return isle;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000F1354 File Offset: 0x000EF554
		public void TryRemoveDynamicBuilding(int {24786})
		{
			Tlist<Isle> mapObjectLayer = Global.Game.WorldInstance.MapObjectLayer;
			for (int i = 0; i < this.{24804}.Size; i++)
			{
				if (this.{24804}.Array[i].Item2.uIDinScene == {24786})
				{
					this.{24804}.Array[i].Item1.Dispose();
					mapObjectLayer.FastRemove(this.{24804}.Array[i].Item1);
					Global.Game.WorldInstance.MapVisibleObjectLayer.FastRemove(this.{24804}.Array[i].Item1);
					this.{24804}.FastRemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000F141C File Offset: 0x000EF61C
		[return: TupleElementNames(new string[]
		{
			"isle",
			"data",
			"secFromEvent"
		})]
		public ValueTuple<Isle, DynamicBuildCreatePacket, float> GetIsleByUID(int {24787})
		{
			for (int i = 0; i < this.{24804}.Size; i++)
			{
				int? dynamicObjectUID = this.{24804}.Array[i].Item1.DynamicObjectUID;
				if (dynamicObjectUID.GetValueOrDefault() == {24787} & dynamicObjectUID != null)
				{
					return this.{24804}.Array[i];
				}
			}
			return new ValueTuple<Isle, DynamicBuildCreatePacket, float>(null, default(DynamicBuildCreatePacket), 0f);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x000F1497 File Offset: 0x000EF697
		[CompilerGenerated]
		private void {24788}()
		{
			Global.Player.PortEntering(this.ContainsLighthouseEnteringZone);
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x000F14A9 File Offset: 0x000EF6A9
		[CompilerGenerated]
		private bool {24789}()
		{
			return Global.Player.GetBattleTimer == 0f && this.ContainsLighthouseEnteringZone && InputHelper.NowInputState.IsUp(Keys.Escape) && !Global.Settings.kb_ds_Forward.IsClick;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000F14E6 File Offset: 0x000EF6E6
		[NullableContext(1)]
		[CompilerGenerated]
		private bool {24790}(PersonalIsleStatus {24791})
		{
			return (int){24791}.PlaceIndex == this.ContainsWorldFactories.First().FcID;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000F1500 File Offset: 0x000EF700
		[CompilerGenerated]
		private void {24792}(int {24793})
		{
			if ({24793} == 0)
			{
				Session.CurrentCrewJob = new ApplyingEffectCache(Local.PlayerShipGui_11b, 10000f, new Action(this.{24794}), new Func<bool>(this.{24795}))
				{
					CanBeStopped = true
				};
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000F1538 File Offset: 0x000EF738
		[CompilerGenerated]
		private void {24794}()
		{
			Global.Network.Send(new OnPersonalIsleRequest(OnPersonalIsleRequest.Operation.BuildIsle, (byte)this.ContainsWorldFactories.First().FcID, 0, 0f));
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000F1566 File Offset: 0x000EF766
		[CompilerGenerated]
		private bool {24795}()
		{
			return this.ContainsWorldFactories.Size > 0 && Global.Player.GetBattleTimer == 0f;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000F1589 File Offset: 0x000EF789
		[CompilerGenerated]
		private void {24796}()
		{
			Global.Network.Send(new OnBorardFortOrTowerMsg(this.ContainsTowerForCapturing.DynamicObjectUID.Value));
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x000F15AF File Offset: 0x000EF7AF
		[CompilerGenerated]
		private bool {24797}()
		{
			return this.ContainsTowerForCapturing != null;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x000F15BA File Offset: 0x000EF7BA
		[CompilerGenerated]
		private void {24798}()
		{
			Session.CurrentCrewJob = new ApplyingEffectCache(Local.robbing, 25000f, new Action(this.{24799}), new Func<bool>(this.{24800}))
			{
				CanBeStopped = true
			};
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x000F15EF File Offset: 0x000EF7EF
		[CompilerGenerated]
		private void {24799}()
		{
			Global.Network.Send(new OnBorardFortOrTowerMsg(this.ContainsGuildFort.DynamicObjectUID.Value));
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x000F1615 File Offset: 0x000EF815
		[CompilerGenerated]
		private bool {24800}()
		{
			return this.ContainsGuildFort != null && Session.EngagingInPortBattle > PbsBatlleSide.None;
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x000F15EF File Offset: 0x000EF7EF
		[CompilerGenerated]
		private void {24801}()
		{
			Global.Network.Send(new OnBorardFortOrTowerMsg(this.ContainsGuildFort.DynamicObjectUID.Value));
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x000F1629 File Offset: 0x000EF829
		[CompilerGenerated]
		private bool {24802}()
		{
			return this.ContainsGuildFort != null;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x000F1634 File Offset: 0x000EF834
		[CompilerGenerated]
		internal static bool <RenderBuildings2D>g__IsAllyPort|30_0(GuildBuildingVisualData {24803})
		{
			return Session.Guild != null && (Session.Guild.Tag == {24803}.CapturedByGuild || Session.Guild.IsTrusted({24803}.CapturedByGuild, false) || ({24803}.StatusIcon == 2 && Session.EngagingInPortBattle == PbsBatlleSide.Defender));
		}

		// Token: 0x04001973 RID: 6515
		public Tlist<TraderInSeaPlaceInfo> ContainsTraderPositionsInSea;

		// Token: 0x04001974 RID: 6516
		public Tlist<TraderInSeaPlaceInfo> VisibleResearchPoints;

		// Token: 0x04001975 RID: 6517
		public Tlist<ShipNpc> ContainsWanderingTradersInSea;

		// Token: 0x04001976 RID: 6518
		public Tlist<FactoryPlaceIsleInfo> ContainsWorldFactories;

		// Token: 0x04001977 RID: 6519
		public Tlist<QuestInfo> VisibleWorldQuests;

		// Token: 0x04001978 RID: 6520
		public Tlist<QuestInfo> ContainsWorldQuests;

		// Token: 0x04001979 RID: 6521
		public Isle VisibleGuildFort;

		// Token: 0x0400197A RID: 6522
		public Isle ContainsGuildFort;

		// Token: 0x0400197B RID: 6523
		public GuildBuildingVisualData VisibleGuildFortData;

		// Token: 0x0400197C RID: 6524
		public InteractiveWorldSystem.FortInteropMode ContainsGuildFortMode;

		// Token: 0x0400197D RID: 6525
		public bool ContainsLighthouseEnteringZone;

		// Token: 0x0400197E RID: 6526
		public PlayerWorldActivityStatus? ContainsWorldActivity;

		// Token: 0x0400197F RID: 6527
		public Isle VisibleTowerForCapturing;

		// Token: 0x04001980 RID: 6528
		public Isle ContainsTowerForCapturing;

		// Token: 0x04001981 RID: 6529
		public string ActualInteropMessageString;

		// Token: 0x04001982 RID: 6530
		public bool InteropIsBoarding;

		// Token: 0x04001983 RID: 6531
		public bool InteropIsWhaleHarpoon;

		// Token: 0x04001984 RID: 6532
		public bool BoardingModeUnitsCountProblem;

		// Token: 0x04001985 RID: 6533
		public bool WhaleNoHarpoonProblem;

		// Token: 0x04001986 RID: 6534
		public string ShowArenaLootTip;

		// Token: 0x04001987 RID: 6535
		public ClientDrop EmpireLootNotification;

		// Token: 0x04001988 RID: 6536
		public Keys CurrentPickDropKey = Keys.Space;

		// Token: 0x04001989 RID: 6537
		[TupleElementNames(new string[]
		{
			"isle",
			"data",
			"secFromEvent"
		})]
		private Tlist<ValueTuple<Isle, DynamicBuildCreatePacket, float>> {24804};

		// Token: 0x020004C5 RID: 1221
		public enum FortInteropMode
		{
			// Token: 0x0400198B RID: 6539
			No,
			// Token: 0x0400198C RID: 6540
			Managment,
			// Token: 0x0400198D RID: 6541
			RobbingFirstTime,
			// Token: 0x0400198E RID: 6542
			RobbingSecoundTime,
			// Token: 0x0400198F RID: 6543
			CapturingByUnits
		}
	}
}
