using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Core;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Eventing;
using TheraEngine.Scene;
using WorldOfSeaBattles;
using WorldOfSeaBattles.Interface.PortPagesUi.Trade;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Scenes
{
	// Token: 0x02000036 RID: 54
	internal sealed class PortScene : Scene
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000030FD File Offset: 0x000012FD
		public override bool CleanScene
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000030FD File Offset: 0x000012FD
		public override bool UseStaticCamera
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000030FD File Offset: 0x000012FD
		public override bool UseStaticWeather
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000AE54 File Offset: 0x00009054
		public bool IsMainPage
		{
			get
			{
				return this.{16530} is {20791};
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000AE64 File Offset: 0x00009064
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000AE6C File Offset: 0x0000906C
		public bool IsVisibleMainUi
		{
			get
			{
				return this.{16532};
			}
			set
			{
				this.{16532} = value;
				if (this.{16530} != null)
				{
					this.{16530}.IsVisible = value;
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000AE89 File Offset: 0x00009089
		public string TradehouseName
		{
			get
			{
				if (Global.Player.NearPort.Type != PortType.PirateBay)
				{
					return Local.PortVisualScene_1;
				}
				return Local.PortVisualScene_1_alt;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000AEA8 File Offset: 0x000090A8
		public string QuesthouseName
		{
			get
			{
				if (!Global.Player.NearPort.IsStronghold)
				{
					return Local.PortVisualScene_51;
				}
				return Local.PortVisualScene_51_temple;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000AEC6 File Offset: 0x000090C6
		public PersonalIsleStatus CurrentPersonalIsle
		{
			get
			{
				return this.{16529};
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000AECE File Offset: 0x000090CE
		public bool IsAbleToChangeShip
		{
			get
			{
				return Global.Player.NearPortType == PortEnteringType.Port || (Global.Player.NearPortType == PortEnteringType.PersonalIsle && this.{16529} != null && this.{16529}.AllowChangeShipsAndMending);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000AF01 File Offset: 0x00009101
		public bool IsAbleToChangeFlags
		{
			get
			{
				return Global.Player.NearPortType == PortEnteringType.Port || Global.Player.NearPortType == PortEnteringType.PersonalIsle;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000AECE File Offset: 0x000090CE
		public bool IsAbleToMendingShip
		{
			get
			{
				return Global.Player.NearPortType == PortEnteringType.Port || (Global.Player.NearPortType == PortEnteringType.PersonalIsle && this.{16529} != null && this.{16529}.AllowChangeShipsAndMending);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000AF54 File Offset: 0x00009154
		public override void Initialize(ContentManager {16493})
		{
			this.{16520} = new PortVisualScene({16493});
			this.{16521} = new Timer(3000f);
			Global.Game.EvEntryToGame += delegate()
			{
			};
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000AFA6 File Offset: 0x000091A6
		public override void OnBegin()
		{
			Global.Game.SceneGame.IncreaseMouse();
			Global.Game.IsMouseVisible = true;
			this.LoadInterface(this.{16538}, this.{16539});
			this.{16525} = 0f;
			this.IsVisibleMainUi = true;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000AFE8 File Offset: 0x000091E8
		public override void OnEnd()
		{
			if (this.{16520}.Loaded)
			{
				this.{16520}.Disconnect();
			}
			this.{16522} = 0;
			this.{16523} = 0;
			this.{16526} = 0f;
			this.{16524} = 0;
			this.{16525} = 0f;
			this.{16527}.Reset();
			this.{16534} = null;
			this.{16531}.Clear();
			UiControl uiControl = this.{16530};
			if (uiControl != null)
			{
				uiControl.RemoveFromContainer();
			}
			this.{16530} = null;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000B070 File Offset: 0x00009270
		public void Prepare(bool {16494}, bool {16495})
		{
			if ({16494})
			{
				Session.Account.WorldFlagEnteredToPort = Session.Account.WorldFlag;
			}
			this.{16538} = {16494};
			this.{16539} = {16495};
			if (Global.Player.NearPortType == PortEnteringType.PersonalIsle)
			{
				PlayerPersonalIsles personalIsles = Session.Account.PersonalIsles;
				Vector2 position = Global.Player.Position;
				this.{16529} = personalIsles.GetNearTo(position);
			}
			else
			{
				this.{16529} = null;
			}
			Global.Player.EntryToPort();
			Steam.CheckAllAchievements();
			this.{16520}.Connect();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000B0F4 File Offset: 0x000092F4
		public void AcceptExit()
		{
			this.{16520}.Disconnect();
			Global.Player.ExitOfPort();
			Global.Game.StaticSystem.SetDefaultSky();
			Global.Game.ChangeSceneToGame();
			if (this.{16537} != null)
			{
				this.{16537}();
				this.{16537} = null;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000B14C File Offset: 0x0000934C
		public void RefreshVisualScene()
		{
			{17177} currentInstance = {17177}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.BlockAndClose();
			}
			{17107} currentInstance2 = {17107}.CurrentInstance;
			if (currentInstance2 != null)
			{
				currentInstance2.BlockAndClose();
			}
			if (Global.Player.NearPortType == PortEnteringType.PersonalIsle)
			{
				PlayerPersonalIsles personalIsles = Session.Account.PersonalIsles;
				Vector2 position = Global.Player.Position;
				this.{16529} = personalIsles.GetNearTo(position);
				if ({20547}.CurrentInstance == null)
				{
					new {20547}(this.{16529});
				}
			}
			else
			{
				this.{16529} = null;
			}
			this.{16520}.Disconnect();
			this.{16520}.Connect();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000B1DC File Offset: 0x000093DC
		public void MakeAccSync()
		{
			Global.Network.Send(new OnTempSync(Session.Account, Session.UsedCraftTimeSync));
			this.{16525} = 0f;
			Global.Settings.Logbook.Flush();
			Session.UsedCraftTimeSync = 0f;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000B22C File Offset: 0x0000942C
		public override void Update(ref FrameTime {16496})
		{
			try
			{
				if (Global.Game.IsActive && {22913}.CurrentInstance == null)
				{
					if (InputHelper.LeftWasClicked && !Global.Game.GetInterfaceManager.HasFocusControls && (this.IsMainPage || {22409}.CurrentInstance != null || Global.Player.PreviewShip != null))
					{
						{22409} currentInstance = {22409}.CurrentInstance;
						if ((currentInstance == null || !currentInstance.IsMoving) && !{18561}.IsOpen)
						{
							this.{16523}++;
						}
					}
					if (InputHelper.RightWasClicked)
					{
						this.{16522}++;
					}
				}
				if (InputHelper.LeftWasReleased)
				{
					this.{16523} = Math.Max(0, this.{16523} - 1);
				}
				if (InputHelper.RightWasReleased)
				{
					this.{16522} = Math.Max(0, this.{16522} - 1);
				}
				if (this.{16523} + this.{16522} != this.{16524})
				{
					this.{16524} = this.{16523} + this.{16522};
					if (this.{16524} == 0)
					{
						Global.Game.SceneGame.IncreaseMouse();
					}
					else
					{
						Global.Game.SceneGame.DecreaseMouse();
					}
				}
				if ((InputHelper.LastMouseState.RightPressed || InputHelper.LastMouseState.LeftPressed) && Global.Game.SceneGame.MouseState == 0 && {22279}.CurrentInstance == null)
				{
					this.{16526} += {16496}.msElapsed;
				}
				else
				{
					this.{16526} = 0f;
				}
				if (this.{16526} > 1500f)
				{
					this.{16527}.SpeedPerSec = 1f;
					this.{16527}.Evalute(ref {16496}, true);
				}
				else
				{
					this.{16527}.SpeedPerSec = 1.3f;
					this.{16527}.Evalute(ref {16496}, false);
				}
				Global.Game.GetInterfaceManager.Host.Opacity = 1f - this.{16527}.CurrentSoftValueSmoothstep;
				Global.Game.IsMouseVisible = (Global.Game.SceneGame.MouseState != 0);
				Global.Camera.EnabledMouseRead = (Global.Game.SceneGame.MouseState == 0);
				if (this.{16535} > 0f)
				{
					this.{16535} -= {16496}.msElapsed;
					if (this.{16535} < 1f)
					{
						this.{16535} = 0f;
						this.MakeAccSync();
						Global.Network.Send(new OnPortEndMsg((short)this.{16536})
						{
							ServerRespLHPosition = Global.Player.GetShipPositionInfo
						});
					}
				}
				this.{16520}.LoopEffects(ref {16496});
				if (this.{16521}.Sample(ref {16496}))
				{
					VisualHelper.UpdateUniqueInventory();
				}
				if (GameScene.GameHasInputFocus && Global.Settings.kb_Map.IsClick && ({22478}.CurrentInstance == null || !{22478}.CurrentInstance.IsInput))
				{
					{22913}.TryToOpen();
				}
				this.{16525} += {16496}.secElapsed;
				if (this.{16525} > 240f)
				{
					this.MakeAccSync();
				}
				PortEnteringType nearPortType = Global.Player.NearPortType;
				bool flag = nearPortType == PortEnteringType.Port || nearPortType == PortEnteringType.PersonalIsle;
				if (flag && Global.Player.MapInfo.IsWorldmap)
				{
					Session.Account.WorldFlag = Session.Account.WorldFlag.Mapback();
					Session.Game.CheckFlag();
				}
				if (Global.Settings.kb_Action.IsClick && GameScene.GameHasInputFocus && ({22478}.CurrentInstance == null || !{22478}.CurrentInstance.IsInput) && {18807}.CurrentInstance == null && Global.Player.UsedShipPlayer == Session.Account.Shipyard.CurrentRealShip)
				{
					{19086}.CreateExitOfPortMenu();
				}
				if ({16496}.EvaluteTimerMs2(ref this.{16528}) && Gameplay.EducationQuestsList.Size > 0)
				{
					if (!Session.Account.Quests.ProgressRunningQuests.Any((QuestRunningProgress {16540}) => {16540}.Info.Company == QuestCompany.Education) && Session.Account.Rang < 20)
					{
						foreach (QuestInfo questInfo in ((IEnumerable<QuestInfo>)Gameplay.EducationQuestsList))
						{
							QuestEducationFlag questEducationFlag = (QuestEducationFlag)questInfo.FirstStep;
							if (!Session.Account.EducationQuest.HasFlags(questEducationFlag.Mask))
							{
								Global.Network.Send(new OnGetQuestMsg(questInfo.ID, QuestAction.Get, -1, null));
								break;
							}
						}
					}
				}
			}
			catch (NullReferenceException)
			{
				if (Debugger.IsAttached)
				{
					throw;
				}
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000B6E0 File Offset: 0x000098E0
		public override void Render2D()
		{
			if (Global.Game.GetCurrentSceneName == GameSceneName.Port && this.IsVisibleMainUi && Global.Player.MapInfo.IsWorldmap)
			{
				Engine.GS.SetTexture(AtlasPortGui.Texture.Tex);
				this.{16520}.Render2D();
				Color color = Color.White * Global.Game.GetInterfaceManager.Host.Opacity;
				bool flag = true;
				{21078} {21078} = this.{16530} as {21078};
				if ({21078} != null)
				{
					flag = {21078}.IsVisible;
				}
				if (flag)
				{
					Engine.GS.SetTexture(AtlasPortGui.Texture.Tex);
					Device gs = Engine.GS;
					Rectangle rectangle = new Rectangle(502, 301, 258, 136);
					Rectangle uiarea = Engine.GS.UIArea;
					gs.Draw(rectangle, uiarea, color);
					Engine.GS.ReturnBackTexture();
				}
			}
			Engine.GS.SetTexture(AtlasPortGui.Texture.Tex);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000B7DD File Offset: 0x000099DD
		public void Render3DMainScene()
		{
			if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
			{
				this.{16520}.Render();
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000B7F7 File Offset: 0x000099F7
		public void Render3DStatic()
		{
			if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
			{
				this.{16520}.RenderStatic();
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000B811 File Offset: 0x00009A11
		public void Render3DMainSceneGBuffer(IGBufferBuilder {16497})
		{
			if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
			{
				this.{16520}.RenderToGBuffer({16497});
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000B82C File Offset: 0x00009A2C
		public {22079} ShipsHolder
		{
			get
			{
				return this.{16534};
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000B834 File Offset: 0x00009A34
		public bool IsPortExiting
		{
			get
			{
				return this.{16535} > 0f;
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000B844 File Offset: 0x00009A44
		public void LoadInterface(bool {16498}, bool {16499})
		{
			this.{16534} = new {22079}();
			this.{16531} = new Tlist<UiControl>();
			Tlist<UiControl> tlist = this.{16531};
			UiControl uiControl = new {19994}();
			tlist.Add(uiControl);
			Tlist<UiControl> tlist2 = this.{16531};
			uiControl = new {19970}();
			tlist2.Add(uiControl);
			Tlist<UiControl> tlist3 = this.{16531};
			uiControl = new {19717}();
			tlist3.Add(uiControl);
			this.mainHandler(null);
			GameplayHelper.MakePortEnteringActions({16498});
			this.{16534}.SeeTargetChanged += this.{16500};
			this.{16534}.SelectAndSetAsViewCurrentShip();
			EducationHelper.EnterToPortFromGame({16499});
			if (Global.Player.NearPort.PortID != (int)Session.Account.StartPortId)
			{
				EducationHelper.MakeFlag(EducationOnboarding.NewPort, true);
				if (Global.Player.NearPort.NearRegion.LevelInt >= 3)
				{
					EducationHelper.MakeFlag(EducationOnboarding.NewPortHazardZone3, true);
				}
			}
			if (!PlatformTuning.DisableShop && Session.Account.Rang >= 15 && Session.EducState_PortLaunchCounter >= 1 && Session.TimeFromLastReceivedDamageSec > 40f && !Session.Account.Analytics.IsShowSupportWindow && Rand.Chanse(10f))
			{
				new {17312}(Local.invite_friends, delegate()
				{
					Helpers.OpenReferalWebPage();
				}, delegate()
				{
				});
				Session.Account.Analytics.IsShowSupportWindow = true;
			}
			this.{16528} = 2000f;
			if (Global.Player.NearPortType == PortEnteringType.PersonalIsle)
			{
				new {20547}(this.{16529});
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00003100 File Offset: 0x00001300
		private void {16500}()
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000B9E4 File Offset: 0x00009BE4
		public void CheckSelectedShip(Action {16501}, bool {16502}, bool {16503})
		{
			if (!Global.Player.IsPortEntry)
			{
				{16501}();
				return;
			}
			PlayerShipDynamicInfo currentRealShip = Session.Account.Shipyard.CurrentRealShip;
			this.{16534}.See(currentRealShip);
			GameplayHelper.PortLeave({16501}, {16502}, {16503});
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000BA28 File Offset: 0x00009C28
		public void TryRegisterCallExitToWorld(int {16504}, string {16505})
		{
			Action <>9__1;
			this.CheckSelectedShip(delegate
			{
				Action {25302};
				if (({25302} = <>9__1) == null)
				{
					{25302} = (<>9__1 = delegate()
					{
						if (Session.Account.WorldFlag == OpenWorldFlag.NoFlag)
						{
							{19994}.Logbook(Local.lbe_left_port2(Session.Account.WorldFlag.ToStringLocalShort()), LBFlags.L0);
						}
						else
						{
							{19994}.Logbook(Local.lbe_left_port(Session.Account.WorldFlag.ToStringLocalShort()), LBFlags.L0);
						}
						this.{16508}(false, {16504}, {16505});
					});
				}
				EducationHelper.ExitToWoldClicked({25302});
			}, true, false);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000BA64 File Offset: 0x00009C64
		public void FastExitWithoutCheckShip(Action {16506}, string {16507})
		{
			this.{16537} = (Action)Delegate.Combine(this.{16537}, {16506});
			this.{16508}(true, -1, {16507});
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000BA88 File Offset: 0x00009C88
		private void {16508}(bool {16509}, int {16510}, string {16511})
		{
			this.{16536} = {16510};
			if ({16509})
			{
				this.{16535} = 1f;
				return;
			}
			this.{16535} = IntroScreenRenderer.AnimationTime;
			if (!Session.EducState_IsInitialMessageShown && Session.Account.Rang == 1)
			{
				Session.EducState_IsInitialMessageShown = true;
				Global.Render.PostProcess.RunIntroScreen(new IntroScreenRenderer(Local.main_idea, true, IntroScreenRenderer.ExtraEffects.None, null));
				return;
			}
			Global.Render.PostProcess.RunIntroScreen(new IntroScreenRenderer({16511} ?? Local.loc_world, false, ({16511} == null) ? IntroScreenRenderer.ExtraEffects.None : IntroScreenRenderer.ExtraEffects.Lights, null));
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000BB24 File Offset: 0x00009D24
		internal void UpdateGuiForViewShip()
		{
			Global.Player.RestoreHp(1E-06f);
			{22079} {22079} = this.{16534};
			if ({22079} == null)
			{
				return;
			}
			{22079}.See(Session.Account.Shipyard.CurrentRealShip);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000BB54 File Offset: 0x00009D54
		internal void LoadInterfaceShipInfoOnly()
		{
			this.UpdateGuiForViewShip();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000BB5C File Offset: 0x00009D5C
		public void ReloadPort()
		{
			Global.Game.GetInterfaceManager.ClearAll();
			this.{16531}.Clear();
			this.{16530} = null;
			this.LoadInterface(false, false);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000BB88 File Offset: 0x00009D88
		public void mainHandler(ClickUiEventArgs {16512})
		{
			if (this.{16530} is {21338})
			{
				Global.Game.ScenePort.MakeAccSync();
			}
			if (this.{16530} is {21078} && Session.Account.Rang >= 8 && Session.Account.Shipyard.List.Count <= 2 && !PlatformTuning.DisableShop && PlatformTuning.EnableContextOffers)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_BuyStartPacket, true);
			}
			GameplayHelper.CheckDesigns();
			this.{16518}(new {20791}());
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000BC08 File Offset: 0x00009E08
		public void verfHandler(ClickUiEventArgs {16513})
		{
			this.{16518}(new {21078}());
			DonationSystem.CheckForShipsFBO();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000BC1A File Offset: 0x00009E1A
		public void workshopHandler(ClickUiEventArgs {16514})
		{
			this.{16518}(new {21338}());
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000BC27 File Offset: 0x00009E27
		public void tradeHandler(ClickUiEventArgs {16515})
		{
			Global.Game.ScenePort.MakeAccSync();
			this.{16518}(new PortTradePage_SyncProtected());
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000BC44 File Offset: 0x00009E44
		public void realShopHandler(ClickUiEventArgs {16516}, DonationSystem.PacketOffer {16517} = null)
		{
			{20881} {20881} = {20881}.Open({16517});
			if ({20881} != null)
			{
				this.{16518}({20881});
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000BC64 File Offset: 0x00009E64
		private void {16518}(UiControl {16519})
		{
			if (!({16519} is {20791}))
			{
				Global.Network.Send(new OnAnalyticsEventClient(OnAnalyticsEventClient.Type.OpenPortPage, {16519}.GetType().Name, 0));
			}
			if (this.{16530} != null)
			{
				{17068} {17068} = this.{16530} as {17068};
				if ({17068} != null)
				{
					{17068}.BlockAndClose();
				}
				else
				{
					{17625} {17625} = this.{16530} as {17625};
					if ({17625} != null)
					{
						{17625}.BlockAndClose();
					}
					else
					{
						this.{16530}.RemoveFromContainer();
					}
				}
			}
			this.{16530} = {16519};
			bool flag = false;
			bool flag2 = false;
			IPortPage portPage = this.{16530} as IPortPage;
			if (portPage != null)
			{
				flag = portPage.CreateChatUi;
				flag2 = portPage.CreateShipStatUi;
			}
			if (flag)
			{
				if ({22478}.CurrentInstance == null)
				{
					Tlist<UiControl> tlist = this.{16531};
					UiControl uiControl = new {22478}(true);
					tlist.Add(uiControl);
				}
			}
			else
			{
				{22478} currentInstance = {22478}.CurrentInstance;
			}
			if (flag2)
			{
				if ({22001}.CurrentInstance == null)
				{
					new {22001}();
					new UiOpacityAnimation({22001}.CurrentInstance, 0f, 1f, 500f);
				}
			}
			else if ({22001}.CurrentInstance != null)
			{
				{22001}.CurrentInstance.RemoveFromContainer();
			}
			Global.Game.ScenePort.UpdateGuiForViewShip();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000BD7C File Offset: 0x00009F7C
		public void UpdateTopmostControls()
		{
			foreach (UiControl uiControl in ((IEnumerable<UiControl>)this.{16531}))
			{
				uiControl.MoveToFrontLevel();
			}
			{22676} currentButton = {22478}.currentButton;
			if (currentButton != null)
			{
				currentButton.MoveToFrontLevel();
			}
			{22687} currentLSViewBox = {22478}.currentLSViewBox;
			if (currentLSViewBox != null)
			{
				currentLSViewBox.MoveToFrontLevel();
			}
			{22001} currentInstance = {22001}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.MoveToFrontLevel();
			}
			{22623} currentInstance2 = {22623}.CurrentInstance;
			if (currentInstance2 == null)
			{
				return;
			}
			currentInstance2.MoveToFrontLevel();
		}

		// Token: 0x0400011A RID: 282
		private PortVisualScene {16520};

		// Token: 0x0400011B RID: 283
		private Timer {16521};

		// Token: 0x0400011C RID: 284
		private int {16522};

		// Token: 0x0400011D RID: 285
		private int {16523};

		// Token: 0x0400011E RID: 286
		private int {16524};

		// Token: 0x0400011F RID: 287
		private float {16525};

		// Token: 0x04000120 RID: 288
		private float {16526};

		// Token: 0x04000121 RID: 289
		private SoftTrigger {16527} = new SoftTrigger(0f, 1f, 0.66f);

		// Token: 0x04000122 RID: 290
		private float {16528};

		// Token: 0x04000123 RID: 291
		private PersonalIsleStatus {16529};

		// Token: 0x04000124 RID: 292
		private UiControl {16530};

		// Token: 0x04000125 RID: 293
		private Tlist<UiControl> {16531} = new Tlist<UiControl>();

		// Token: 0x04000126 RID: 294
		private bool {16532} = true;

		// Token: 0x04000127 RID: 295
		private bool {16533};

		// Token: 0x04000128 RID: 296
		private {22079} {16534};

		// Token: 0x04000129 RID: 297
		private float {16535};

		// Token: 0x0400012A RID: 298
		private int {16536};

		// Token: 0x0400012B RID: 299
		private Action {16537};

		// Token: 0x0400012C RID: 300
		private bool {16538};

		// Token: 0x0400012D RID: 301
		private bool {16539};
	}
}
