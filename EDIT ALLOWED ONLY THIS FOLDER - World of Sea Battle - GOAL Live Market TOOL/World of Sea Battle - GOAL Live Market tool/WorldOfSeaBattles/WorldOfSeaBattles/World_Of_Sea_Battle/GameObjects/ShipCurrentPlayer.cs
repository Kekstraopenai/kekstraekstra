using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Interface;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x02000053 RID: 83
	internal sealed class ShipCurrentPlayer : ShipPlayerBase, IClientShip
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000070D7 File Offset: 0x000052D7
		public override bool DetailPhysics
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00013780 File Offset: 0x00011980
		public override int MirrorEngageInPortBattlePortID
		{
			get
			{
				if (Session.EngagingInPortBattlePort != null)
				{
					return Session.EngagingInPortBattlePort.PortID;
				}
				return -1;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00013795 File Offset: 0x00011995
		public override PlayerAccount AccountConnection
		{
			get
			{
				return Session.Account;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000030FD File Offset: 0x000012FD
		public bool MakeTransparentForMe
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0001379C File Offset: 0x0001199C
		public bool IsInPortEnteringNow
		{
			get
			{
				return this.{16856} > 0f;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000137AB File Offset: 0x000119AB
		public bool IsMarchingMode
		{
			get
			{
				return this.{16851}.LinearStateCode == 3;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600024E RID: 590 RVA: 0x000137BB File Offset: 0x000119BB
		public bool IsMarchingModeWithLag
		{
			get
			{
				return this.FirstController.LinearStateCode == 3 || this.{16851}.LinearStateCode == 3;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600024F RID: 591 RVA: 0x000137DB File Offset: 0x000119DB
		public bool CrewIsBusy
		{
			get
			{
				return (Session.CurrentCrewJob != null && Session.CurrentCrewJob.IsLargeEffect) || {18139}.CurrentInstance != null;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000250 RID: 592 RVA: 0x000137FA File Offset: 0x000119FA
		protected override ShipDynamicInfo AskActualShip
		{
			get
			{
				return this.PreviewShip ?? base.AskActualShip;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0001380C File Offset: 0x00011A0C
		public ShipCurrentPlayer()
		{
			this.{16851} = new ShipKeyController();
			this.{16842}();
			Global.Settings.kb_ds_Forward.EvChange += this.{16840};
			this.ClientWeaponsShooting = new CommonShotRenderer<CommonShotInfo>(new CommonCannonGunCallback<CommonShotInfo>(this.{16844}), new CommonCannonCompleteAttackCallback(this.SynchronizeCannonGunBucketsAfterComplete));
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00013890 File Offset: 0x00011A90
		public override void ClearResources()
		{
			this.{16843}();
			this.{16851}.Change = null;
			this.{16854} = 0f;
			this.{16856} = 0f;
			this.{16857} = 0f;
			this.{16858} = 0;
			this.{16859} = 0f;
			this.{16860} = 0f;
			this.{16863} = default(Vector2);
			this.{16866} = false;
			this.{16867} = 0f;
			this.ClientWeaponsShooting.Reset();
			this.{16868} = 0;
			base.ClearResources();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00013924 File Offset: 0x00011B24
		public void Initialize(int {16796}, ShipPositionInfo {16797})
		{
			WorldMapInfo {17024};
			if (Session.Account.SavedMAPID == 0)
			{
				{17024} = Gameplay.StartMap;
			}
			else
			{
				{17024} = Gameplay.MapFromID((int)Session.Account.SavedMAPID);
			}
			base.Initialize({16796}, {16797}, {17024});
			this.Client.ClientInitialize(Session.Account.PlayerName, true, null);
			this.{16851}.Change = new Action(this.{16847});
			this.ehUnselectMap(this);
			base.EvMapTeleport += this.{16848};
			this.Client.SwitchSailesState();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000139B0 File Offset: 0x00011BB0
		public override void Update(ref FrameTime {16798})
		{
			Debugging.Update();
			{19028}.WhenUpdate();
			Session.WReload.Update(ref {16798}, this);
			Global.Settings.DeathController.Update(ref {16798});
			CommonGlobal.CurrentClientWeather = this.MapInfo.Weather;
			CommonGlobal.WorldWeather.ClientUpdatePointedMode(this);
			if (this.MapInfo.IsEducationMap)
			{
				CommonGlobal.EducationMapWeather.ClientUpdatePointedMode(this);
			}
			this.Client.Guild.Tag = ((Session.Guild != null) ? Session.Guild.Tag : "");
			bool flag = this.{16856} == 0f && !base.IsPortEntry && GameScene.GameHasInputFocus && !{18909}.BlockShip && {18807}.CurrentInstance == null && !Global.Camera.IsFreeMode;
			bool flag2 = flag && (!base.IsDestroyed || base.UsedShipPlayer.CraftFrom.ID == 1);
			if ({16798}.EvaluteTimerMs2(ref this.{16857}))
			{
				this.FirstController.Set(this.{16858});
			}
			this.{16799}({16798}, flag);
			if (!base.SpeedAllowMending && this.IsMendingBegin && this.FirstController.GetCode == this.{16851}.GetCode)
			{
				this.StopMending(true);
			}
			base.Update(ref {16798});
			if (!flag2)
			{
				this.{16866} = true;
			}
			if (flag2 && !InputHelper.NowMouseState.LeftPressed)
			{
				this.{16866} = false;
			}
			this.{16802}();
			this.{16860} = Math.Max(0f, this.{16860} - {16798}.secElapsed * 0.4f);
			if ((!this.MapInfo.IsEducationMap || ({18593}.CurrentInstance != null && {18593}.CurrentInstance.IsEntryToPortTask)) && !base.IsDestroyed)
			{
				if ({16798}.EvaluteTimerMs2(ref this.{16856}))
				{
					Global.Network.Send(new OnPortStartMsg(Global.Player.ResourcesOfHold, Session.Account.Xp, false, this.{16855}));
				}
			}
			else
			{
				this.{16854} = 0f;
			}
			if (flag2 && Global.Settings.kb_SendGroupCommand.IsClick && {17539}.CurrentInstance == null)
			{
				new {17539}();
			}
			if (flag2 && Global.Settings.kb_Map.IsClick && !Global.Camera.IsSpyglass && !TextBox.IsThereInput && !KeyInputControl.IsInputElements)
			{
				{22913}.TryToOpen();
			}
			{16798}.EvaluteTimerMs(ref this.{16859});
			{16798}.EvaluteTimerMs(ref this.{16854});
			if (Session.LastDeathPosition != null && Vector2.Distance(Session.LastDeathPosition.Value, base.Position) < 150f && !base.IsDestroyed)
			{
				Session.LastDeathPosition = null;
			}
			if (this.{16864}.Sample(ref {16798}))
			{
				CrewNotificationManager.RegularUpdate();
				if (!base.IsPortEntry && this.UsedShip.FirstHP.Summary > 0f)
				{
					if (base.DestructByTiltAmount > 0.6f)
					{
						Global.Game.SoundSystem.PlaySound(GameStaticSoundName.Bell, 0.03f, 1f);
					}
					if (base.DestructByTiltAmount > 0.999f)
					{
						Global.Network.Send(new OnWorldActionMsg(this.uID, WorldRandomAction.ShipOverturned, "", null, 0, null));
					}
				}
			}
			this.{16803}({16798});
			this.{16805}();
			if (Session.WorldMapMarkerPosition != null && this.MapInfo.IsWorldmap && Vector2.DistanceSquared(base.Position, Session.WorldMapMarkerPosition.Value) < 2500f)
			{
				Session.WorldMapMarkerPosition = null;
			}
			if (base.NowSpeed != 0f)
			{
				Global.Game.inactivityDuration = 0f;
			}
			if ((!Global.Game.IsActive || {22913}.CurrentInstance != null) && base.DestructByTiltAmount > 0.5f)
			{
				this.ResetSpeedTo0();
			}
			if (base.UsedShipPlayer.CanGoOnly1Speed)
			{
				this.ResetSpeedTo1();
			}
			if (base.UsedShipPlayer.CanGoOnly2Speed)
			{
				this.ResetSpeedTo2();
			}
			if (!this.MapInfo.IsWorldmap || (!base.IsEntryToPortZoneContains && !base.IsPortEntry))
			{
				GameplayHelper.EnableNextHoldManagement = true;
			}
			if (this.MapInfo.IsEnableArenaUi && {18909}.CurrentInstance == null && flag2 && Global.Settings.kb_OpenLogbook.IsClick && Session.LoadedArenaUiInfo != null)
			{
				new {18909}(Session.LoadedArenaUiInfo.Value, true, null);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00013E18 File Offset: 0x00012018
		private void {16799}(FrameTime {16800}, bool {16801})
		{
			if (!{16801})
			{
				this.{16853}.ResetState();
				this.{16852}.ResetState();
				this.{16851}.EvaluteAxisSpeed(0);
			}
			if (this.CrewIsBusy)
			{
				this.{16851}.Set(0);
				return;
			}
			if (this.{16857} == 0f && {16801})
			{
				this.{16853}.Update(ref {16800}, Global.Settings.kb_ds_Right.IsDown || this.{16868} == 1);
				this.{16852}.Update(ref {16800}, Global.Settings.kb_ds_Left.IsDown || this.{16868} == -1);
				this.{16851}.EvaluteAxisSpeed(this.{16852}.Value ? -1 : ((this.{16853}.Value > false) ? 1 : 0));
				int num = base.UsedShipPlayer.CanGoOnly1Speed ? 1 : (base.UsedShipPlayer.CanGoOnly2Speed ? 2 : int.MaxValue);
				bool flag = Global.Game.WorldInstance.ShallowsDetectionComponent.CheckForAllowSpeedUp(this.{16851}.LinearStateCode);
				if (flag)
				{
					bool flag2 = this.FirstController.LinearStateCode < num;
					if (!flag2)
					{
						int linearStateCode = this.FirstController.LinearStateCode;
						bool flag3 = linearStateCode == 0 || linearStateCode == 4;
						flag2 = flag3;
					}
					flag = flag2;
				}
				if (flag)
				{
					if (Global.Settings.kb_ds_Forward.IsDown)
					{
						this.{16863}.X = this.{16863}.X + {16800}.msElapsed;
					}
					if (Global.Settings.kb_ds_Forward.IsClick || this.{16863}.X > 366f)
					{
						if (Global.Settings.HighDetailing && this.{16851}.LinearStateCode == 2 && this.UsedShip.Cannons.Items.Size > 0)
						{
							Global.Game.SoundSystem.PlaySound(GameStaticSoundName.OpenDecks, 5f, 1f);
						}
						if (this.{16851}.LinearStateCode == 4)
						{
							this.{16851}.SetLinearSpeed(0);
						}
						else
						{
							CrewNotificationManager.ForwardKey();
							this.{16851}.EvaluteLinearSpeed(1);
						}
						this.{16863}.X = 0f;
					}
				}
				else
				{
					this.{16863}.X = 0f;
				}
				if (Global.Settings.kb_ds_Backward.IsDown)
				{
					this.{16863}.Y = this.{16863}.Y + {16800}.msElapsed;
				}
				if (Global.Settings.kb_ds_Backward.IsClick || this.{16863}.Y > 366f)
				{
					if (this.{16851}.LinearStateCode != 4)
					{
						if (Global.Settings.HighDetailing && this.{16851}.LinearStateCode == 3 && this.UsedShip.Cannons.Items.Size > 0)
						{
							Global.Game.SoundSystem.PlaySound(GameStaticSoundName.OpenDecks, 5f, 1f);
						}
						if (this.{16851}.LinearStateCode == 0 && ShipCurrentPlayer.timeFromLastDownClick.Elapsed.TotalMilliseconds > 300.0 && this.physicsBody.NowSpeed <= 1f && !this.UsedShip.StaticInfo.IsBalloon)
						{
							this.{16851}.SetLinearSpeed(4);
						}
						else
						{
							this.{16851}.EvaluteLinearSpeed(-1);
						}
						CrewNotificationManager.BackwardKey();
					}
					this.{16863}.Y = 0f;
					ShipCurrentPlayer.timeFromLastDownClick.Restart();
					return;
				}
			}
			else
			{
				this.{16863} = default(Vector2);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0001419C File Offset: 0x0001239C
		private void {16802}()
		{
			if (!this.{16866} && !Global.Game.IsMouseVisible && !Global.Settings.kb_Mortar_ModifierKey.IsDown && (InputHelper.NowMouseState.LeftPressed || (InputHelper.NowMouseState.RightPressed && Global.Settings.RightMouseAction == RightMouseKeyAction.SingleCannonGun)) && Global.Camera.EnableMouseByActivation)
			{
				if (this.IsMarchingMode || !InGameSightUi.CannonSights.HasActiveCannons)
				{
					if (this.{16861})
					{
						this.{16860} += 1f;
					}
					this.{16861} = false;
				}
				else
				{
					CannonLocation? lastActiveNearBoard = InGameSightUi.CannonSights.LastActiveNearBoard;
					bool isActiveFireguns = InGameSightUi.CannonSights.IsActiveFireguns;
					CannonsAttackMode cannonsAttackMode = (InputHelper.NowMouseState.RightPressed && Global.Settings.RightMouseAction == RightMouseKeyAction.SingleCannonGun && !isActiveFireguns) ? CannonsAttackMode.SingleCannon : InGameSightUi.CannonSights.PickedGunMode;
					this.{16860} = 0f;
					if (InputHelper.NowInputState.IsDown(Keys.LeftAlt) && InputHelper.LeftWasClicked && Session.Account.CaptainSkills[PDynamicAccountBonus.BDoubleShot] > 0)
					{
						CannonLocation? cannonLocation = lastActiveNearBoard;
						CannonLocation cannonLocation2 = CannonLocation.LeftSide;
						if (((cannonLocation.GetValueOrDefault() == cannonLocation2 & cannonLocation != null) || lastActiveNearBoard.GetValueOrDefault() == CannonLocation.RightSide) && Global.Player.UsedShip.Cannons.GetReloadFactor(CannonLocation.LeftSide) >= 1f && Global.Player.UsedShip.Cannons.GetReloadFactor(CannonLocation.RightSide) >= 1f)
						{
							this.{16820}(CannonsAttackMode.AllRandomized, false, false);
							this.{16820}(CannonsAttackMode.AllRandomized, true, false);
						}
					}
					else if ((cannonsAttackMode == CannonsAttackMode.SingleCannon || isActiveFireguns || (lastActiveNearBoard != null && this.ClientWeaponsShooting.CanShotAgain(lastActiveNearBoard.Value))) && ShipCurrentPlayer.singleShotTimer.Elapsed.TotalMilliseconds > (double)(isActiveFireguns ? 400f : this.UsedShip.StaticInfo.SingleModeShotInterval))
					{
						this.{16820}(cannonsAttackMode, false, isActiveFireguns);
						ShipCurrentPlayer.singleShotTimer.Restart();
					}
				}
				if ((this.{16860} >= 3f && Session.Account.Rang < 20) || (this.{16860} > 1f && this.MapInfo.IsEducationMap))
				{
					if (this.IsMarchingMode)
					{
						{19994}.Me({19988}.Info, Local.ShipCurrentPlayer_0, Array.Empty<object>());
						this.{16860} = 0f;
						return;
					}
					if ({17312}.CurrentInstance == null && this.MapInfo.IsEducationMap)
					{
						if (this.{16862} == 0 || this.{16862} == 3 || this.{16862} == 10)
						{
							new {17312}(true, new {17464}[]
							{
								new {17464}(default(Rectangle), Local.ShipCurrentPlayer_1)
							});
						}
						this.{16860} = 0f;
						this.{16862}++;
						return;
					}
				}
			}
			else
			{
				this.{16861} = true;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00014480 File Offset: 0x00012680
		private void {16803}(FrameTime {16804})
		{
			bool flag;
			Session.Account.FoodAtShip.Update(this, ref {16804}, out flag);
			if (this.{16865}.Sample(ref {16804}) && Global.Settings.AutoFoodConsumtion > 0f && Session.Account.FoodAtShip.CurrentHungerLevel > Math.Max(0.1f, 1f - Global.Settings.AutoFoodConsumtion) && (!Global.Settings.AutoFoodConsumtionOnlyOnNorth || Session.Account.FoodAtShip.HasFoodConsumption(this) == FoodConsumption.Mode.Full))
			{
				GSI gsi = this.ResourcesOfHoldAllowed();
				foreach (GSILocalPair gsilocalPair in ((IEnumerable<GSILocalPair>)Global.Settings.FoodConsumptionFilter.Clone()))
				{
					if (gsi[gsilocalPair.ID] == 0)
					{
						Global.Settings.FoodConsumptionFilter[gsilocalPair.ID] = 0;
					}
				}
				Tlist<GSILocalEnumerablePair<ResourceInfo>> tlist = new Tlist<GSILocalEnumerablePair<ResourceInfo>>(from {16869} in gsi.ResourceInfo
				where {16869}.Info.FoodValue(Global.Player) > 0f && Global.Settings.FoodConsumptionFilter[(int){16869}.Info.ID] > 0
				select {16869});
				if (tlist.Size > 0)
				{
					GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair = tlist.MaxItem((GSILocalEnumerablePair<ResourceInfo> {16870}) => (float)(-(float){16870}.Info.MediumCost.Value));
					int num = Session.Account.FoodAtShip.NeededCountOfSpecificFood(this, gsilocalEnumerablePair.Info);
					if (num > 0)
					{
						Global.Network.Send(new OnTakeFoodMsg(new GSI().Exs((int)gsilocalEnumerablePair.Info.ID, num), Global.Player.uID));
					}
				}
			}
			if (Session.Account.FoodAtShip.HasFoodConsumption(this) == FoodConsumption.Mode.Full)
			{
				if (this.{16867} == 0f)
				{
					{18945}.TryShowNotif(Local.winter_zone_notif1, Local.winter_zone_notif2, null);
				}
				this.{16867} = 20000f;
				if (Session.Account.FoodAtShip.CurrentHungerLevel > 0.5f)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_EnterFoodConsumptionZone, true);
				}
			}
			{16804}.EvaluteTimerMs(ref this.{16867});
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000146B0 File Offset: 0x000128B0
		private void {16805}()
		{
			if (!base.IsPortEntry && this.MapInfo.IsWorldmap)
			{
				foreach (Vector3 vector in Gameplay.WorldHazardZones)
				{
					Vector2 {11447} = base.Position - vector.XY();
					if ({11447}.Length() < vector.Z + 75f && Vector2.Dot({11447}.Normal(), base.Normal) < 0f && {11447}.Length() > vector.Z - 50f)
					{
						if (Session.Account.DisallowEnteringHazardWaters && this.ResetSpeedTo0())
						{
							{19994}.Me({19988}.InfoRed, Local.hazard_zone_disallow, Array.Empty<object>());
						}
						EducationHelper.MakeFlag(EducationOnboarding.GameTT_EnterHazardZone, true);
					}
				}
			}
			this.{16868} = 0;
			if (this.MapInfo.IsEducationMap)
			{
				float num = Math.Max(0f, this.MapInfo.MapSize.X * 0.5f - base.Position.Length());
				float num2 = Vector2.Dot(base.Position.Normal, base.Normal);
				if (num < MathHelper.Lerp(10f, 55f, num2) && num2 > 0.1f)
				{
					this.{16868} = ((base.Rotation > MathF.Atan2(base.Position.Y, base.Position.X)) ? 1 : -1);
				}
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00014828 File Offset: 0x00012A28
		public void PortEntering(bool {16806})
		{
			if (Global.Player.MapInfo.IsEducationMap)
			{
				this.{16807}(false);
				return;
			}
			RTI mCharge = Session.ServerWorldStatus.NearPortMooringCharge.Value;
			string {17365};
			if (!Session.Game.CanEnterNearPortWithFlags(Session.Account.WorldFlag, out {17365}, null))
			{
				new {17312}({17365});
				return;
			}
			if (mCharge.Value == 0)
			{
				this.{16807}({16806});
				return;
			}
			if ({17312}.CurrentInstance == null)
			{
				if (Session.Account.Rang < 15 && mCharge.Value > Session.Account.Gold)
				{
					this.{16807}({16806});
					return;
				}
				if (Session.Account.Gold > 30000 && Session.Account.Rang > 27)
				{
					if (Global.Player.{16807}({16806}))
					{
						Session.Account.Gold -= mCharge.Value;
						{19994}.MeAndLogbook({19988}.Gold, Local.lbe_mooring(mCharge.Value), null);
						return;
					}
				}
				else
				{
					if (Session.Account.Gold >= 0)
					{
						new {17312}((PbsStatus.MooringChangeForHold(Global.Player, Session.Game.MapMyFraction, Session.Game.NearPortGuild.Fraction, Global.Player.NearPort) > 0) ? Local.mooring_pay_for_hold(mCharge.Value) : Local.ShipCurrentPlayer_2(mCharge.Value), delegate()
						{
							if ({16806} ? (!this.CheckBattleTimer()) : (!Global.Player.AllowEnteringPort))
							{
								return;
							}
							Session.Account.Gold -= mCharge.Value;
							this.{16807}({16806});
						}, null);
						return;
					}
					new {17312}(Local.gold_not_enough);
				}
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00014A08 File Offset: 0x00012C08
		private bool {16807}(bool {16808})
		{
			if (this.{16854} > 0f)
			{
				return false;
			}
			this.{16855} = {16808};
			Global.Render.PostProcess.RunIntroScreen(new IntroScreenRenderer(Local.loc_port, false, IntroScreenRenderer.ExtraEffects.None, Global.Game.SoundSystem.NearPortStyleSound()));
			this.{16856} = IntroScreenRenderer.AnimationTime;
			this.{16854} = 1500f;
			return true;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00014A6C File Offset: 0x00012C6C
		protected override void DestroyCallback()
		{
			FXEngine.ShipDeath(this);
			Global.Render.PostProcess.PlayerDeath();
			Session.LastDeathPosition = new Vector2?(base.Position);
			Global.Game.GetInterfaceManager.ClearAll();
			Global.Settings.DeathController.OnDeath();
			new {19215}();
			this.{16843}();
			base.DestroyCallback();
			Session.CurrentCrewJob = null;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00014AD4 File Offset: 0x00012CD4
		public override void ForceUpdateShipEffects()
		{
			base.ForceUpdateShipEffects();
			if (base.UsedShipPlayer.StaticInfo.BowFigurePosition == Vector3.Zero)
			{
				ShipDesignInfo shipDesignInfo = base.UsedShipPlayer.RemoveDesignElement(5);
				if (shipDesignInfo != null)
				{
					GSI desingElementsAtStorage = Session.Account.DesingElementsAtStorage;
					int id = (int)shipDesignInfo.ID;
					int num = desingElementsAtStorage[id];
					desingElementsAtStorage[id] = num + 1;
				}
			}
			this.Client.UpdateModel();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00014B40 File Offset: 0x00012D40
		protected override void OnShipUpdated()
		{
			base.OnShipUpdated();
			Global.Camera.SetTargetObject(this, Global.Game.GetCurrentSceneName == GameSceneName.Port, new Vector2?(Global.Camera.Rotation.XY()));
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00014B74 File Offset: 0x00012D74
		public override bool UpdateCapacity()
		{
			float capacitySpeedFactor = base.CapacitySpeedFactor;
			bool flag = base.UpdateCapacity();
			if (flag && base.UsedShipPlayer.GetItemsMass() > this.UsedShip.Capacity && base.CapacitySpeedFactor < capacitySpeedFactor && Global.Game.GetCurrentSceneName == GameSceneName.Game && ShipCurrentPlayer.timeFromLastOverloadNotif.Elapsed.TotalSeconds > 10.0)
			{
				{19994}.Me({19988}.InfoRed, Local.PortAllInterface_13 + "!", Array.Empty<object>());
				ShipCurrentPlayer.timeFromLastOverloadNotif.Restart();
			}
			return flag;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00014C04 File Offset: 0x00012E04
		protected override void WhenSailingXpAdded(int {16809}, bool {16810})
		{
			{19994}.MeAndLogbook({19988}.Info, {16810} ? Local.lbe_xp_for_sailing_withBonus({16809}) : Local.lbe_xp_for_sailing({16809}), null);
			if (Session.Account.Rang >= 6)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_SailingXp, true);
			}
			base.WhenSailingXpAdded({16809}, {16810});
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00014C5C File Offset: 0x00012E5C
		protected override void ehUnselectMap(Ship {16811})
		{
			if (Debugging.SessionMapEditor)
			{
				{18560}.closed = false;
				this.MapInfo = Gameplay.ArenaMaps.FromID(6);
			}
			Global.Game.StaticSystem.SetMap(this.MapInfo);
			Global.Game.WorldInstance.SetMap(this.MapInfo);
			if (this.MapInfo.IsEducationMap)
			{
				Global.Render.GetSceneManager.UseOverrideTime = new float?((float)11);
			}
			else
			{
				Global.Render.GetSceneManager.UseOverrideTime = null;
			}
			if (this.MapInfo.IsWorldmap)
			{
				if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
				{
					Global.Game.ScenePort.RefreshVisualScene();
				}
				Session.CurrentPassingSession = null;
				Session.CurrentArenaSession = null;
			}
			{22478} currentInstance = {22478}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.MapChanged();
			}
			base.ehUnselectMap({16811});
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00003100 File Offset: 0x00001300
		protected override void OnRespawn()
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00014D38 File Offset: 0x00012F38
		public override void MakeDamage(in DamageData {16812}, int {16813})
		{
			{19390} currentInstance = {19390}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.RemoveFromContainer();
			}
			InGameSightUi.OnAction();
			if ({16812}.SourcePawnType != DamageID.Burning && {16812}.SourcePawnType != DamageID.Other)
			{
				if ({16813} > 0)
				{
					Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({16813});
					if (shipFromUID != null)
					{
						Session.LastDamage = ((IClientShip)shipFromUID).GetClient.GetName2();
						if (Session.TimeFromLastReceivedDamage == null)
						{
							Session.TimeFromLastReceivedDamage = Stopwatch.StartNew();
						}
						else
						{
							Session.TimeFromLastReceivedDamage.Restart();
						}
						{20059} currentInstance2 = {20059}.CurrentInstance;
						if (currentInstance2 != null)
						{
							currentInstance2.HitscanUpdate(shipFromUID.Position, {20059}.c_hitRed);
						}
						Session.LastDamageByPlayer = (shipFromUID is Player);
					}
				}
				else
				{
					Session.LastDamage = null;
				}
			}
			CrewNotificationManager.WhenDamageReceive({16812}.HealthDamage);
			Global.Camera.CameraEffects.OnReceveDamage({16812}.SourcePawnType, {16812}.HealthDamage);
			EducationHelper.OnReceiveDamage({16812});
			int totalItemsCount = this.UsedShip.Crew.HurtedCrew.GetTotalItemsCount();
			base.MakeDamage({16812}, {16813});
			int totalItemsCount2 = this.UsedShip.Crew.HurtedCrew.GetTotalItemsCount();
			Session.LastDeathByMyself = (base.IsDestroyed && {16813} == this.uID);
			int deadCrewCounter = Session.DeadCrewCounter;
			DamageData damageData = {16812};
			Session.DeadCrewCounter = deadCrewCounter + (damageData.TotalCrewKills - Math.Max(0, totalItemsCount2 - totalItemsCount));
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00014E88 File Offset: 0x00013088
		public override void EntryToPort()
		{
			this.{16843}();
			this.{16854} = 0f;
			base.EntryToPort();
			Global.Settings.GamemodeDoublones.Value = 0;
			Session.SouredResourcesCounter = 0;
			Session.DeadCrewCounter = 0;
			Global.Game.WorldInstance.ChangeWithPortScene(true);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00014ED8 File Offset: 0x000130D8
		public override void ExitOfPort()
		{
			this.Client.ExampleDesigns.Clear();
			base.ExitOfPort();
			InGameSightUi.CannonSights.ShowGunModeAnimation();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00014EFC File Offset: 0x000130FC
		public void RespawnMessageHandler(OnLocPlayerRespawnResultMsg {16814})
		{
			if ({16814}.entryToPort)
			{
				bool flag = this.MapInfo.IsWorldmap && {16814}.entryToPort && this.MapInfo.GetNearPort({16814}.PositionForSync.Position) != base.NearPort;
				bool isWorldmap = this.MapInfo.IsWorldmap;
				if (!this.MapInfo.IsWorldmap)
				{
					base.TeleportMapChange(Gameplay.WorldMap);
				}
				base.Respawn({16814}.PositionForSync, {16814}.restoreFullHp);
				flag &= (base.NearPortType == PortEnteringType.Port);
				Global.Settings.DeathController.OnRespawn(base.NearPortType, isWorldmap, flag);
				Global.Game.ChangeSceneToPort(true, true);
				EducationHelper.AfterRespawn(flag, true);
				return;
			}
			base.Respawn({16814}.PositionForSync, {16814}.restoreFullHp);
			this.UsedShip.Cannons.BeginReloadWeapons(this, null, null, null);
			Global.Settings.DeathController.OnRespawn(PortEnteringType.None, false, false);
			EducationHelper.AfterRespawn(false, false);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00015003 File Offset: 0x00013203
		public bool CheckBattleTimerAndSpeed()
		{
			if (base.IsPortEntry)
			{
				return true;
			}
			if (!this.CheckBattleTimer())
			{
				return false;
			}
			if (base.NowSpeed > 0f)
			{
				{19994}.Me({19988}.Info, Local.ShipCurrentPlayer_6, Array.Empty<object>());
				return false;
			}
			return true;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0001503B File Offset: 0x0001323B
		public bool CheckBattleTimer()
		{
			if (base.IsPortEntry)
			{
				return true;
			}
			if (base.GetBattleTimer > 0f)
			{
				{19994}.Me({19988}.Info, Local.ShipCurrentPlayer_7(Global.Player.GetBattleTimerSec), Array.Empty<object>());
				return false;
			}
			return true;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00015078 File Offset: 0x00013278
		public void BeginMending()
		{
			if (this.IsMendingBegin)
			{
				throw new InvalidOperationException();
			}
			if (this.UsedShip.FirstHP.Summary >= this.UsedShip.MaxHp && this.UsedShip.IsSailesFull)
			{
				return;
			}
			if (this.UsedShip.CanRepairWithSpeed2)
			{
				this.ResetSpeedTo2();
			}
			else
			{
				this.ResetSpeedTo1();
			}
			this.IsMendingBegin = true;
			Global.Network.Send(new OnLocMendingStartOrStop(true));
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000150F5 File Offset: 0x000132F5
		public void StopMending(bool {16815})
		{
			if (!this.IsMendingBegin)
			{
				throw new InvalidOperationException();
			}
			this.IsMendingBegin = false;
			if ({16815})
			{
				Global.Network.Send(new OnLocMendingStartOrStop(false));
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00015124 File Offset: 0x00013324
		private bool {16816}(bool {16817}, bool {16818}, bool {16819})
		{
			if (base.IsDestroyed)
			{
				return false;
			}
			if (base.AllowEnteringPort)
			{
				{19994}.Me({19988}.Info, Local.ShipCurrentPlayer_8, Array.Empty<object>());
				return false;
			}
			return !base.IsDestroyed && (this.{16859} == 0f || !{16818}) && !this.CrewIsBusy && (!{16818} || InGameSightUi.CannonSights.HasActiveAndReloadedCannons || base.DebugEnabled || {16819}) && (!{16818} || InGameSightUi.CannonSights.LastActiveNearBoard != null);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000151B4 File Offset: 0x000133B4
		private void {16820}(CannonsAttackMode {16821}, bool {16822}, bool {16823})
		{
			InGameSightUi.OnAction();
			if (!this.{16816}(true, !{16822}, {16823}))
			{
				return;
			}
			CannonLocation? lastActiveNearBoard = InGameSightUi.CannonSights.LastActiveNearBoard;
			if (!{16823} && base.UsedShipPlayer.BallsOfHold[Global.Settings.SelectedCannonBalls[lastActiveNearBoard.Value]] == 0 && !this.{16824}(Global.Settings.SelectedCannonBalls[lastActiveNearBoard.Value]))
			{
				return;
			}
			if (this.IsMendingBegin)
			{
				this.StopMending(false);
			}
			this.SynchronizeCannonGunBucketsAfter(Global.Settings.SelectedCannonBalls[lastActiveNearBoard.Value], {16821});
			if (((lastActiveNearBoard.Value == CannonLocation.InBack || lastActiveNearBoard.Value == CannonLocation.InFront) && {16821} == CannonsAttackMode.AllRandomized) || (lastActiveNearBoard.Value == CannonLocation.InFront && Global.Player.UsedShip.StaticInfo.FrontSidePorts.Length == 1))
			{
				{16821} = CannonsAttackMode.AllNormal;
			}
			Vector3 finalAttackNormal = InGameSightUi.CannonSights.FinalAttackNormal;
			Vector2 finalAttackDirection = InGameSightUi.CannonSights.FinalAttackDirection;
			OnLocAttackMsg onLocAttackMsg = new OnLocAttackMsg(finalAttackNormal, InGameSightUi.CannonSights.ReductionFactor * ((Session.Account.Rang <= 2) ? 0.3f : ((Session.Account.Rang < 5) ? 0.5f : 1f)), finalAttackDirection, base.Rotation, (this.UsedShip.Cannons.HavingFireguns && lastActiveNearBoard.Value == CannonLocation.InFront) ? 1 : ((byte)Global.Settings.SelectedCannonBalls[lastActiveNearBoard.Value]), {16821}, (byte)Global.Settings.SightIndex, {16822});
			Global.Network.Send(onLocAttackMsg);
			EducationHelper.OnCannonsGun({16821} > CannonsAttackMode.SingleCannon);
			if (Global.Settings.VoicesMode >= LocalSettings.ShipVoices.BackgroundOnly && !InGameSightUi.CannonSights.IsActiveFireguns && ShipCurrentPlayer.voiceEffectTimeout.Elapsed.TotalSeconds > 5.0 && Rand.Chanse((float)(({16821} == CannonsAttackMode.SingleCannon) ? 2 : 30)))
			{
				Global.Game.SoundSystem.PlaySound(GameStaticSoundName.V_RandSound, 0.03f, 1f);
				ShipCurrentPlayer.voiceEffectTimeout.Restart();
			}
			if ({16821} != CannonsAttackMode.SingleCannon && ShipCurrentPlayer.voiceEffectTimeout2.Elapsed.TotalSeconds > 12.0)
			{
				Global.Game.SoundSystem.PlayVoice(VoiceSoundEffect.Fire);
				ShipCurrentPlayer.voiceEffectTimeout2.Restart();
			}
			if (Global.Game.InterestPoints.ShipInSight != null)
			{
				ShipOtherPlayer shipOtherPlayer = Global.Game.InterestPoints.ShipInSight as ShipOtherPlayer;
				if (shipOtherPlayer != null)
				{
					EducationHelper.OnProbablyPvpAttempt(shipOtherPlayer);
				}
			}
			Session.EducState_GunShotCounter++;
			CrewNotificationManager.WhenFire();
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0001543C File Offset: 0x0001363C
		private bool {16824}(int {16825})
		{
			CannonBallInfo srcAmmoInfo = Gameplay.BallsInfo.FromID({16825});
			if (base.UsedShipPlayer.BallsOfHold.CannonBallInfo.Any((GSILocalEnumerablePair<CannonBallInfo> {16872}) => {16872}.Info.AmmoType == srcAmmoInfo.AmmoType))
			{
				{19994}.Me({19988}.Info, Local.ShipCurrentPlayer_10(srcAmmoInfo.Name), Array.Empty<object>());
				int[] source;
				if (srcAmmoInfo.AmmoType != CannonAmmoType.CannonBall)
				{
					if (srcAmmoInfo.AmmoType != CannonAmmoType.FalkonetBall)
					{
						source = Array.Empty<int>();
					}
					else
					{
						(source = new int[1])[0] = 13;
					}
				}
				else
				{
					RuntimeHelpers.InitializeArray(source = new int[4], fieldof(<PrivateImplementationDetails>.D188352C82AACF028C9B02DE24B5A7743F6E24D2B8FB484481C813A8B24AB3ED).FieldHandle);
				}
				foreach (CannonBallInfo cannonBallInfo in from {16871} in source
				select Gameplay.BallsInfo[{16871}])
				{
					if (base.UsedShipPlayer.BallsOfHold[(int)cannonBallInfo.ID] > 0 || cannonBallInfo.Infinity)
					{
						if (cannonBallInfo.AmmoType == CannonAmmoType.MortarBall)
						{
							Global.Settings.SelectedMortarBallsID = (int)cannonBallInfo.ID;
						}
						else if (cannonBallInfo.AmmoType == CannonAmmoType.FalkonetBall)
						{
							Global.Settings.SelectedFalkonetsID = (int)cannonBallInfo.ID;
						}
						else
						{
							foreach (CannonLocation {2295} in Enum.GetValues<CannonLocation>())
							{
								Global.Settings.SelectedCannonBalls[{2295}] = (int)cannonBallInfo.ID;
							}
						}
						return true;
					}
				}
				return false;
			}
			{19994}.Me({19988}.Info, (srcAmmoInfo.AmmoType == CannonAmmoType.CannonBall) ? Local.ShipCurrentPlayer_11 : Local.ShipCurrentPlayer_10(srcAmmoInfo.Name), Array.Empty<object>());
			return false;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00015610 File Offset: 0x00013810
		public void SynchronizeCannonGunBucketsAfter(int {16826}, CannonsAttackMode {16827})
		{
			base.BeginBattleTimer(false, false);
			this.{16859} = (float)(({16827} == CannonsAttackMode.SingleCannon) ? 100 : 1000);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00015630 File Offset: 0x00013830
		public void SynchronizeCannonGunBucketsBefore(CannonCommon {16828}, int {16829}, bool {16830})
		{
			if ({16828} != null)
			{
				{16828}.BeginLoad(this.UsedShip.Cannons, Gameplay.BallsInfo.FromID({16829}), {16830} ? (1f - this.UsedShip.ReloadingBonusAfterSingleShot) : 1f);
			}
			if ({16829} != 12)
			{
				Global.Camera.CameraEffects.OnWeaponSingleShot(InGameSightUi.CannonSights.PickedGunMode == CannonsAttackMode.SingleCannon);
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00015698 File Offset: 0x00013898
		public void SynchronizeCannonGunBucketsAfterComplete()
		{
			this.{16859} = 0f;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000156A8 File Offset: 0x000138A8
		public void SynchronizeCannonGunStart(CommonShotInfo[] {16831}, CannonBallInfo {16832})
		{
			InGameSightUi.CurrentInstance.OnGun({16831}.Length);
			if ({16832}.ID != 12)
			{
				if (base.UsedShipPlayer.BallsOfHold[(int){16832}.ID] - {16831}.Length < 100 && base.UsedShipPlayer.BallsOfHold[(int){16832}.ID] >= 100)
				{
					CrewNotificationManager.WhenLowAmmo();
				}
				base.UsedShipPlayer.BallsOfHold.AddOrRemove((int){16832}.ID, -{16831}.Length);
				if (base.UsedShipPlayer.BallsOfHold[(int){16832}.ID] == 0)
				{
					this.{16824}((int){16832}.ID);
				}
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00015748 File Offset: 0x00013948
		public void MortarGun(Vector3 {16833}, bool {16834})
		{
			if (!this.{16816}(true, false, false) || base.IsDestroyedOrFlooding)
			{
				return;
			}
			if (!InGameSightUi.MortarSights.HasAmmo)
			{
				if (InGameSightUi.MortarSights.ShouldUsePowderKegs)
				{
					{19994}.Me({19988}.Info, Local.PortAllInterface_18b(Gameplay.PowderKegsInfo.FromID(MortarShot.PowderKegMortarType).Name), Array.Empty<object>());
					return;
				}
				{19994}.Me({19988}.Info, Local.PortAllInterface_18, Array.Empty<object>());
				return;
			}
			else
			{
				CannonBallInfo cannonBallInfo = InGameSightUi.MortarSights.ShouldUsePowderKegs ? Gameplay.BallsInfo.FromID(19) : Session.SelectedMortarBalls;
				int num = InGameSightUi.MortarSights.ShouldUsePowderKegs ? 100 : base.UsedShipPlayer.BallsOfHold.GetCount((int)cannonBallInfo.ID);
				bool {2543} = Vector2.Dot({16833}.XY(), base.FastNormal) > 0f;
				int num2 = 0;
				int num3 = 0;
				foreach (ValueTuple<CannonLocationInfo, CannonGameInfo> valueTuple in this.UsedShip.StaticInfo.FetchMortars({2543}, this.UsedShip))
				{
					if (num2++ < num)
					{
						if (valueTuple.Item2.Feature != CannonFeature.PowderKegMortar)
						{
							FXEngine.GunEffect(valueTuple.Item1.GetPosition(this), new Vector3({16833}.X, 1f, {16833}.Y) * 0.65f, this.physicsBody.VelocityPerSec / 60f, true, true, 1f, null);
						}
						num3++;
					}
				}
				if (num3 > 0 && cannonBallInfo.ID == 19)
				{
					base.UsedShipPlayer.PowderKegsOfHold.AddOrRemove(MortarShot.PowderKegMortarType, -1);
				}
				else
				{
					base.UsedShipPlayer.BallsOfHold.AddOrRemove((int)cannonBallInfo.ID, -num3);
				}
				Global.Network.Send(new OnLocMortarAttackMsg({16833}, (int)cannonBallInfo.ID));
				this.UpdateCapacity();
				Global.Camera.CameraEffects.OnWeaponSingleShot(true);
				if ({16834})
				{
					Session.WReload.BeginReloadingBackMortar(Global.Player, cannonBallInfo);
					return;
				}
				Session.WReload.BeginReloadingRearMortar(Global.Player, cannonBallInfo);
				return;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00015974 File Offset: 0x00013B74
		public void RunPowderKeg(float {16835})
		{
			if (Global.Player.UsedShip.FirstHP.FloodingFactor > 0f || {18807}.CurrentInstance != null)
			{
				return;
			}
			if (!this.{16816}(true, false, false))
			{
				return;
			}
			if (Global.Player.UsedShipPlayer.CraftFrom.Rank > Session.SelectedPowderKegsInfo.AvailableSinceRank.GetValueOrDefault(100))
			{
				return;
			}
			bool flag;
			if (Session.SelectedPowderKegsInfo.isFirebrand && InGameSightUi.PowderKegSights.FirebrandTarget(out flag) == null)
			{
				return;
			}
			if (this.UsedShip.StaticInfo.IsBalloon && base.NowSpeed > 5f)
			{
				{19994}.Me({19988}.Info, Local.balloon_height_problem(5), Array.Empty<object>());
				return;
			}
			if (Global.Player.UsedShipPlayer.PowderKegsOfHold.GetCount(Global.Settings.SelectedPowderKegs) > 0)
			{
				GSI powderKegsOfHold = Global.Player.UsedShipPlayer.PowderKegsOfHold;
				int selectedPowderKegs = Global.Settings.SelectedPowderKegs;
				int num = powderKegsOfHold[selectedPowderKegs];
				powderKegsOfHold[selectedPowderKegs] = num - 1;
				Global.Network.Send(new OnLocAttackPowderKeg((byte)Global.Settings.SelectedPowderKegs, {16835}, Session.SelectedPowderKegsInfo.isFirebrand ? Global.Game.InterestPoints.ShipInSight.uID : -1));
				Session.WReload.BeginReloadingPowderKegs(Global.Player, Session.SelectedPowderKegsInfo);
				Global.Player.UpdateCapacity();
				Global.Player.OnFalkonetOrKegShot();
				return;
			}
			{19994}.Me({19988}.Info, Local.ShipCurrentPlayer_10(Session.SelectedPowderKegsInfo.Name), Array.Empty<object>());
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00015B08 File Offset: 0x00013D08
		public bool RunFalkonets(Vector3 {16836}, float {16837}, int {16838}, int {16839})
		{
			CannonBallInfo cannonBallInfo = Gameplay.BallsInfo.FromID({16838});
			if (!this.{16816}(true, false, false))
			{
				return false;
			}
			if (!cannonBallInfo.IsBoardingHook && !cannonBallInfo.Infinity && base.UsedShipPlayer.BallsOfHold[{16838}] == 0 && !this.{16824}((int)cannonBallInfo.ID))
			{
				return false;
			}
			if (cannonBallInfo.IsBoardingHook && this.UsedShip.Crew.Count == 0)
			{
				{19994}.Me({19988}.Info, Local.ClientDrop_0, Array.Empty<object>());
				return false;
			}
			Tlist<FalkonetShotInfoRemote> tlist = this.UsedShip.StaticInfo.FetchActiveFalkonet(this, cannonBallInfo, {16836}, {16837} + 10f);
			if (tlist.Size > 0)
			{
				Global.Network.Send(new OnRunFalkonetBallMsg(this.uID, 0, tlist, {16839}));
				if (cannonBallInfo.IsBoardingHook)
				{
					Session.WReload.BeginReloadingBoardingHooks(Global.Player);
				}
				else
				{
					Session.WReload.BeginReloadingFalkonets(Global.Player);
				}
				Global.Player.OnFalkonetOrKegShot();
				EducationHelper.OnFalkonetGun();
				return true;
			}
			return false;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00015C0C File Offset: 0x00013E0C
		protected override bool Get_ShowSailesInPort
		{
			get
			{
				return Global.Settings.ShowSailesInPort || {21544}.CurrentInstance != null || {22279}.CurrentInstance != null || {20881}.CurrentInstance != null || {20755}.CurrentInstance != null;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00015C39 File Offset: 0x00013E39
		public bool CanUseShipSkill
		{
			get
			{
				return this.MapInfo.IsWorldmap && !base.IsDestroyedOrFlooding && this.MirrorEngageInPortBattlePortID == -1;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00015C5B File Offset: 0x00013E5B
		private void {16840}(Keys {16841})
		{
			this.{16842}();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00015C63 File Offset: 0x00013E63
		private void {16842}()
		{
			this.{16852} = new KeyTrigger(1f, 75f);
			this.{16853} = new KeyTrigger(1f, 75f);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00015C8F File Offset: 0x00013E8F
		private void {16843}()
		{
			this.{16857} = 0f;
			this.{16851}.Reset();
			this.{16852}.ResetState();
			this.{16853}.ResetState();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00003100 File Offset: 0x00001300
		public void Render2D()
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00015CBD File Offset: 0x00013EBD
		public void ResetSpeedTo2()
		{
			if (this.{16851}.LinearStateCode == 3)
			{
				this.{16851}.SetLinearSpeed(2);
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00015CD9 File Offset: 0x00013ED9
		public void ResetSpeedTo1()
		{
			if (this.{16851}.LinearStateCode > 1 && this.{16851}.LinearStateCode != 4)
			{
				this.{16851}.SetLinearSpeed(1);
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00015D03 File Offset: 0x00013F03
		public bool ResetSpeedTo0()
		{
			if (this.{16851}.LinearStateCode > 0)
			{
				this.{16851}.SetLinearSpeed(0);
				return true;
			}
			return false;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00015D22 File Offset: 0x00013F22
		public void ResetSpeedAndRotation()
		{
			if (this.{16851}.GetCode != 0)
			{
				this.{16851}.Set(0);
				this.{16852}.ResetState();
				this.{16853}.ResetState();
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00015D54 File Offset: 0x00013F54
		public GSI ResourcesOfHoldAllowed()
		{
			GSI gsi = base.ResourcesOfHold;
			bool flag = false;
			foreach (QuestRunningProgress questRunningProgress in ((IEnumerable<QuestRunningProgress>)Session.Account.Quests.ProgressRunningQuests))
			{
				QuestTransferOrder questTransferOrder = questRunningProgress.CurrentStep as QuestTransferOrder;
				if (questTransferOrder != null)
				{
					if (!flag)
					{
						gsi = gsi.Clone();
					}
					gsi[questTransferOrder.ResourceID] = Math.Max(0, gsi[questTransferOrder.ResourceID] - questTransferOrder.ResourceCount.Value);
				}
			}
			return gsi;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00015E24 File Offset: 0x00014024
		[CompilerGenerated]
		private void {16844}(CommonShotInfo {16845}, int {16846})
		{
			Global.Game.WorldInstance.LocalCommonCannonGunCallback(this, -1, {16845}, {16846}, this.ClientWeaponsShooting.Mode);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00015E44 File Offset: 0x00014044
		[CompilerGenerated]
		private void {16847}()
		{
			this.{16857} = Math.Min(150f, Math.Max(1f, Session.LastPing));
			this.{16858} = this.{16851}.GetCode;
			Global.Network.Send(new OnPlayerLocSetKeys(this.{16851}.GetCode));
			Global.Camera.CameraEffects.OnSailingSpeedChange(this.{16851}.LinearStateCode);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00015EBA File Offset: 0x000140BA
		[NullableContext(1)]
		[CompilerGenerated]
		private void {16848}(Ship {16849}, WorldMapInfo {16850})
		{
			if (!{16850}.IsWorldmap || !this.MapInfo.IsWorldmap)
			{
				Session.LastDeathPosition = null;
			}
			this.ehUnselectMap(this);
		}

		// Token: 0x040001E7 RID: 487
		private ShipKeyController {16851};

		// Token: 0x040001E8 RID: 488
		private KeyTrigger {16852};

		// Token: 0x040001E9 RID: 489
		private KeyTrigger {16853};

		// Token: 0x040001EA RID: 490
		private float {16854};

		// Token: 0x040001EB RID: 491
		private bool {16855};

		// Token: 0x040001EC RID: 492
		private float {16856};

		// Token: 0x040001ED RID: 493
		private float {16857};

		// Token: 0x040001EE RID: 494
		private byte {16858};

		// Token: 0x040001EF RID: 495
		private float {16859};

		// Token: 0x040001F0 RID: 496
		private float {16860};

		// Token: 0x040001F1 RID: 497
		private bool {16861};

		// Token: 0x040001F2 RID: 498
		private int {16862};

		// Token: 0x040001F3 RID: 499
		private Vector2 {16863};

		// Token: 0x040001F4 RID: 500
		private Timer {16864} = new Timer(1000f);

		// Token: 0x040001F5 RID: 501
		private Timer {16865} = new Timer(5000f);

		// Token: 0x040001F6 RID: 502
		private bool {16866};

		// Token: 0x040001F7 RID: 503
		private float {16867};

		// Token: 0x040001F8 RID: 504
		private static Stopwatch timeFromLastOverloadNotif = Stopwatch.StartNew();

		// Token: 0x040001F9 RID: 505
		private int {16868};

		// Token: 0x040001FA RID: 506
		private static Stopwatch singleShotTimer = Stopwatch.StartNew();

		// Token: 0x040001FB RID: 507
		public PlayerShipDynamicInfo PreviewShip;

		// Token: 0x040001FC RID: 508
		private static Stopwatch timeFromLastDownClick = Stopwatch.StartNew();

		// Token: 0x040001FD RID: 509
		private static Stopwatch voiceEffectTimeout = Stopwatch.StartNew();

		// Token: 0x040001FE RID: 510
		private static Stopwatch voiceEffectTimeout2 = Stopwatch.StartNew();
	}
}
