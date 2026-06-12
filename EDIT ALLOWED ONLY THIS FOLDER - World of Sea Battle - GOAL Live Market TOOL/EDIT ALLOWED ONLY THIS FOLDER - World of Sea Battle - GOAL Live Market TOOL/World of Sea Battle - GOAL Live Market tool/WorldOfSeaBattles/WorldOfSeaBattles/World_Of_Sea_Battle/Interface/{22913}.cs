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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Graphics;
using TheraEngine.Graphics.Models;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using TheraEngine.Reactive;
using TheraEngine.Scene;
using TheraEngine.Scene.Lighting;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200040D RID: 1037
	internal sealed class {22913} : CustomUi
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x000BCCE0 File Offset: 0x000BAEE0
		private static float c_visibleTradingRoutes
		{
			get
			{
				return 1.3f;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x000BCCE7 File Offset: 0x000BAEE7
		private static float c_visibleTowersAtScale
		{
			get
			{
				return 3.5f;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x000BCCEE File Offset: 0x000BAEEE
		private static float c_IconsScaleGeneral
		{
			get
			{
				return 0.95f;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x000BCCF5 File Offset: 0x000BAEF5
		private bool distanceMeterTool
		{
			get
			{
				return InputHelper.NowInputState.IsDown(Keys.LeftControl) || InputHelper.NowInputState.IsDown(Keys.RightControl);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x000BCD19 File Offset: 0x000BAF19
		private bool distanceMeterMode
		{
			get
			{
				return this.distanceMeterTool;
			}
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x000BCD24 File Offset: 0x000BAF24
		public {22913}() : base(true)
		{
			{19779} currentInstance = {19779}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.BlockAndClose();
			}
			this.{23048} = new Timer(5000f);
			{22913}.CurrentInstance = this;
			this.AnimatedFocus = false;
			{22478} currentInstance2 = {22478}.CurrentInstance;
			if (currentInstance2 != null)
			{
				currentInstance2.MoveToFrontLevel();
			}
			Global.Game.SceneGame.IncreaseMouse();
			GameScene.IncreaseGameInput();
			base.EvRemoveFromContainer += delegate()
			{
				Global.Game.SceneGame.DecreaseMouse();
				{22913}.CurrentInstance = null;
				GameScene.DecreaseGameInput();
			};
			this.{23052} = new Tlist<TradingRouteInfo>(from {23089} in Gameplay.TradingRoutesInfo
			where {23089}.IsVisible(Session.Account) && {23089}.GetMapTransparancy(Global.Player.Position) > 0f
			select {23089});
			this.{22984}();
			this.{22915}();
			this.{23047} = new BillboardParent_VPCT();
			Vector2 vector = {22913}.BilboardCenter(-Gameplay.WorldMapSizeXY / 2f);
			Vector2 vector2 = {22913}.BilboardCenter(Gameplay.WorldMapSizeXY / 2f);
			this.{23047}.SetPos(-Math.Abs(vector2.X - vector.X) * 1.03f, Math.Abs(vector2.Y - vector.Y) * 1.01f);
			this.{23047}.Transform(Matrix.CreateRotationX(1.5707964f));
			this.{23047}.Transform(Matrix.CreateRotationY(3.1415927f));
			this.{23047}.SetUV(new Vector4(0f, 0f, 1f, 1f));
			this.{23047}.Transform(Matrix.CreateTranslation(new Vector3(0f, -0.7f, 0f)));
			this.{22971}();
			this.{23018} = InputHelper.NowMouseState.ScrollValue;
			if ({22913}.moveOffset == Vector2.Zero)
			{
				this.{22914}();
			}
			base.AddChild(new Button(new Vector2((float)(Engine.GS.UIArea.Width - 78), 10f), {22913}.c_closeButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				PositionAlignment_X = PositionAlignment.RightDown
			}.ExClick(new Action<ClickUiEventArgs>(this.{22996})));
			this.{23025} = new ModelTransformedScene(LocalContent.Loaded.WorldMap[0], Transform3D.Identity);
			Vector3 {14809} = new Vector3(0.005f);
			Vector3 {14807} = new Vector3(-4f, -0.56f, -0.1f);
			Locale? currentGameLocale = Global.Settings.Language.CurrentGameLocale;
			Locale locale = Locale.Ru;
			UWModel[] array = (currentGameLocale.GetValueOrDefault() == locale & currentGameLocale != null) ? LocalContent.Loaded.MapClocks : LocalContent.Loaded.MapClocksEng;
			this.{23025}.AddObject(new ModelRenderer(array[0])
			{
				LocalTransformOrNull = new Transform3D({14807}, Vector3.Zero, {14809})
			}, true);
			ModelTransformedScene modelTransformedScene = this.{23025};
			ModelRenderer modelRenderer = new ModelRenderer(array[1]);
			modelRenderer.LocalTransformOrNull = new Transform3D({14807}, Vector3.Zero, {14809});
			ModelRenderer {11959} = modelRenderer;
			this.clockModel = modelRenderer;
			modelTransformedScene.AddObject({11959}, true);
			this.{23025}.Transform.Translation.Y = 10f;
			this.{23025}.Transform.MiddleScale = 2f;
			this.{23039} = new Form(base.Pos, PositionAlignment.Both, PositionAlignment.Both);
			this.{23039}.RenderToDepthMap = false;
			this.{23039}.ToolTip = new ToolTip(this.{23038} = new ToolTipState(null));
			this.{23039}.AnimatedFocus = false;
			base.AddChild(this.{23039});
			EducationHelper.MakeFlag(EducationOnboarding.OpenWorldMap, true);
			if (EducationHelper.ShowWindOnGlobalMap)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_WindOnMap, true);
			}
			GSI gsi = new GSI();
			foreach (GSI {5459} in Session.Account.EnumerateAllResources(true, true, false, true))
			{
				gsi.Add({5459});
			}
			if (gsi.NamesCount > 5)
			{
				this.{23046} = new {22879}(gsi);
				base.AddChild(this.{23046});
			}
			this.{23045} = new {22887}(400f, {22913}.liveTrading ?? new Tlist<PortLiveTrading>())
			{
				IsVisible = false
			};
			base.AddChild(this.{23045});
			new UiOpacityAnimation(this, 0f, 1f, 400f);
			if (Session.LastMapResponseElapsed.Elapsed.TotalSeconds > 10.0 || Session.LastMapResponse == null)
			{
				Global.Network.Send(default(OnLoadGlobalMap));
			}
			else
			{
				this.SetupState(Session.LastMapResponse.Value);
			}
			Session.Account.FogOfWar.SetData(Global.Render.txFogOfWar);
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000BD258 File Offset: 0x000BB458
		private void {22914}()
		{
			{22913}.moveOffset = new Vector2(-1f, 1f) * Global.Player.PositionClampedToMap.YX() / 5f;
			{22913}.scrollScale = 2.8f;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000BD298 File Offset: 0x000BB498
		private void {22915}()
		{
			this.{23027} = new Tlist<{22913}.IconRenderer3D>();
			Tlist<{22913}.IconRenderer3D> tlist = this.{23027};
			{22913}.IconRenderer3D iconRenderer3D = new {22913}.IconRenderer3D(new Vector2(1000000f), {22913}.c_playerMarker, null, Global.Player.Rotation, 1f, null, 1f, null);
			tlist.Add(iconRenderer3D);
			Tlist<{22913}.IconRenderer3D> tlist2 = this.{23027};
			iconRenderer3D = new {22913}.IconRenderer3D(new Vector2(1000000f), {22913}.c_playerLine, null, Global.Player.Rotation, 6f, null, 0.33f, null);
			tlist2.Add(iconRenderer3D);
			Tlist<{22913}.IconRenderer3D> tlist3 = this.{23027};
			iconRenderer3D = new {22913}.IconRenderer3D(new Vector2(10000000f, 0f), {22913}.c_iconHighlight, null, 0f, 1f, null, 1f, null);
			tlist3.Add(iconRenderer3D);
			Tlist<{22913}.IconRenderer3D> tlist4 = this.{23027};
			iconRenderer3D = new {22913}.IconRenderer3D(new Vector2(10000000f, 0f), {22913}.c_pfArea, null, 0f, 1f, null, 1f, null);
			tlist4.Add(iconRenderer3D);
			this.{23028}.Clear();
			for (int i = 0; i < 5; i++)
			{
				{22913}.IconRenderer3D iconRenderer3D2 = new {22913}.IconRenderer3D(this.{22993}(new Vector2(169f, 998f)), {22913}.c_wind, null, 0f, 1.4f, null, 1f, null);
				this.{23027}.Add(iconRenderer3D2);
				this.{23028}.Add(iconRenderer3D2);
			}
			if (this.{23019} != null && Session.Account.Rang > 1)
			{
				this.{23019}.EnumerateBlocks(new Action<Point, byte>(this.{22998}));
			}
			using (IEnumerator<WeatherArea> enumerator = ((IEnumerable<WeatherArea>)CommonGlobal.CurrentClientWeather.StormAreas).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					WeatherArea item = enumerator.Current;
					if (Vector2.Distance(Global.Player.Position, item.CurrentPosition) < item.SExternalRadius * 2f + 500f || Debugging.DebugInfo || Global.Player.DebugEnabled)
					{
						{22913}.IconRenderer3D iconRenderer3D3 = new {22913}.IconRenderer3D(Vector2.Zero, (item.Type == WeatherAreaType.Fog) ? {22913}.c_weatherAreaFog : {22913}.c_weatherArea, null, 0f, 1f, null, 1f, null)
						{
							Size = new Vector2(1.7f * item.WExternalRadius) / Gameplay.WorldMapSizeXY * {22913}.mapSizeInModelSpace()
						};
						iconRenderer3D3.RealTimeUpdate = (() => new Vector2?(item.CurrentPosition));
						iconRenderer3D3.SetPosition(item.CurrentPosition, 0f, true);
						Tlist<ValueTuple<{22913}.IconRenderer3D, WeatherArea>> tlist5 = this.{23034};
						ValueTuple<{22913}.IconRenderer3D, WeatherArea> valueTuple = new ValueTuple<{22913}.IconRenderer3D, WeatherArea>(iconRenderer3D3, item);
						tlist5.Add(valueTuple);
						iconRenderer3D3.SetVisibilityGroups(new {22887}.Id[]
						{
							{22887}.Id.WeatherEffects
						});
						this.{23027}.Add(iconRenderer3D3);
					}
				}
			}
			WorldMapInfo worldMap = Gameplay.WorldMap;
			this.{23035} = new Dictionary<int, {22913}.IconRenderer3D>();
			this.{22968}();
			this.{22966}(Gameplay.WorldHazardZones);
			this.AddPorts(worldMap.Ports, this.{23051});
			this.{22975}();
			this.AddTradersInSea(worldMap.TradersInSea);
			this.AddFactories(Gameplay.WorldMap.Factories);
			this.{22978}();
			this.{22974}();
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x000BD608 File Offset: 0x000BB808
		public void SetupState(OnLoadGlobalMapResponse {22916})
		{
			this.{23019} = {22916}.hazardData;
			{22913}.liveTrading = {22916}.tradeStatus;
			this.{23020} = {22916}.captureSystem;
			this.{23021} = {22916}.worldFactoriesStatus;
			this.{23022} = {22916}.GuildMembers;
			this.{23024} = {22916}.PBInvites;
			this.{23050}.Clear();
			this.{23051} = {22916}.auctionLotsInfo;
			this.{23053} = {22916}.TradingRoutesLoad;
			this.{23054} = {22916}.ShipsPerRouteDebug;
			this.{22915}();
			foreach (KeyValuePair<int, {22913}.IconRenderer3D> keyValuePair in this.{23033})
			{
				PlayerBuildingState playerBuildingState = Session.Account.Buildings.Get(keyValuePair.Key);
				FactoryMineLivelInfo factoryMineLivelInfo = (playerBuildingState != null) ? playerBuildingState.Level : null;
				if (factoryMineLivelInfo != null)
				{
					int num = Session.Account.Buildings.AllBuildings.IndexOf(playerBuildingState);
					if (num >= 0 && num < this.{23021}.Size)
					{
						GSI gsi = this.{23021}.Array[num];
						if (gsi[factoryMineLivelInfo.ConsumedResId] == 0 && factoryMineLivelInfo.ConsumedResCount > 0)
						{
							{22913}.IconRenderer3D value = keyValuePair.Value;
							Rectangle rectangle = factoryMineLivelInfo.WorksWithoutPriovision(Session.Account) ? {22913}.c_iconMineGold : {22913}.c_iconMineRed;
							value.SetUv(rectangle);
						}
						else if ((float)gsi[factoryMineLivelInfo.ProducedResId] >= factoryMineLivelInfo.HoldCapacity(Session.Account))
						{
							keyValuePair.Value.SetUv({22913}.c_iconMineGold);
						}
					}
				}
			}
			foreach ({22913}.IconRenderer3D iconRenderer3D in ((IEnumerable<{22913}.IconRenderer3D>)this.{23027}))
			{
				if (iconRenderer3D.Tag is PbsStatus)
				{
					iconRenderer3D.Tag = null;
				}
			}
			foreach (PortCaptureStatus portCaptureStatus in ((IEnumerable<PortCaptureStatus>)this.{23020}))
			{
				foreach ({22913}.IconRenderer3D iconRenderer3D2 in ((IEnumerable<{22913}.IconRenderer3D>)this.{23027}))
				{
					if (iconRenderer3D2.Tag != null && iconRenderer3D2.Tag == portCaptureStatus.PortInstance)
					{
						iconRenderer3D2.Tag = portCaptureStatus;
					}
				}
			}
			if (!this.{23049})
			{
				this.{22917}({22916});
			}
			using (IEnumerator<PbsStatus> enumerator4 = this.{23020}.Enumerate<PbsStatus>().GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					PbsStatus capturePort = enumerator4.Current;
					{22913}.IconRenderer3D iconRenderer3D3 = this.{23035}[capturePort.PortID];
					bool flag = iconRenderer3D3.VisibilityGroups.Contains({22887}.Id.PortsWithResources);
					Dictionary<PbsBatlleSide, PbsAvanpostShortInfo> pbRespawn = capturePort.PortInstance.PbRespawn;
					float num2 = 0.9f + 0.1f * (float)capturePort.grownCity;
					iconRenderer3D3.Size *= num2;
					iconRenderer3D3.UpdateGeometry(1f);
					PbsBatlleSide? pbsBatlleSide = null;
					if ((double)capturePort.TimeToBattleCompletion + capturePort.TimeToBeginBattle > 0.0 && Session.Guild != null)
					{
						PBInvite invite = this.{23024}.FirstOrDefault((PBInvite {23097}) => (int){23097}.PortID == capturePort.PortID);
						if (invite.AtSide != PbsBatlleSide.None && pbRespawn.Count != 0)
						{
							pbsBatlleSide = new PbsBatlleSide?(invite.AtSide);
							Vector2 pos = pbRespawn[invite.AtSide].Center;
							Tlist<{22913}.IconRenderer3D> tlist = this.{23027};
							{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(pos, {22913}.c_enterpb_mark, delegate()
							{
								this.{22927}(capturePort, invite, pos);
							}, 0f, 1.1f, null, 1f, null);
							tlist.Add(iconRenderer3D4);
							this.{23050}.Add(pos);
						}
					}
					if (iconRenderer3D3.IsVisibe)
					{
						Rectangle rectangle;
						switch (capturePort.CapturerFraction)
						{
						case FractionID.Pirate:
							rectangle = new Rectangle(1756, 243, 80, 80);
							break;
						case FractionID.Antilia:
							rectangle = new Rectangle(1756, 0, 80, 80);
							break;
						case FractionID.Espaniol:
							rectangle = new Rectangle(1756, 81, 80, 80);
							break;
						case FractionID.KaiAndSeveria:
							rectangle = new Rectangle(1756, 162, 80, 80);
							break;
						default:
							rectangle = default(Rectangle);
							break;
						}
						Rectangle rectangle2 = rectangle;
						if (rectangle2.Width > 0)
						{
							Tlist<{22913}.IconRenderer3D> tlist2 = this.{23027};
							{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(capturePort.PortInstance.EntryPos, rectangle2, null, 0f, 0.83f * num2, null, 1f, null).SetVisibilityGroups(iconRenderer3D3.VisibilityGroups).SetColor(Color.White * (flag ? 0.85f : 0.7f));
							tlist2.Add(iconRenderer3D4);
						}
						switch (capturePort.grownCity)
						{
						case 1:
							rectangle = new Rectangle(1756, 324, 80, 90);
							break;
						case 2:
							rectangle = new Rectangle(1756, 415, 80, 90);
							break;
						case 3:
							rectangle = new Rectangle(1756, 506, 80, 90);
							break;
						default:
							rectangle = default(Rectangle);
							break;
						}
						Rectangle rectangle3 = rectangle;
						if (rectangle3.Width > 0)
						{
							Tlist<{22913}.IconRenderer3D> tlist3 = this.{23027};
							{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(capturePort.PortInstance.EntryPos - new Vector2(60f, 0f) * num2, rectangle3, null, 0f, 0.83f * num2 * 1.2f, null, 1f, null).SetVisibilityGroups(iconRenderer3D3.VisibilityGroups).SetColor(flag ? Color.White : new Color(177, 197, 255));
							tlist3.Add(iconRenderer3D4);
						}
					}
					if (!capturePort.PortInstance.MakeLockedForPlayer(Session.Account))
					{
						if ((capturePort.TimeToBattleCompletion > 0f && Session.EngagingInPortBattle != PbsBatlleSide.None) || Debugging.DebugInfo)
						{
							if (pbsBatlleSide.GetValueOrDefault() != PbsBatlleSide.Attacker)
							{
								Tlist<{22913}.IconRenderer3D> tlist4 = this.{23027};
								{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(pbRespawn[PbsBatlleSide.Attacker].Center, {22913}.c_enterpb_mark_dim, delegate()
								{
									this.{22924}(true, capturePort);
								}, 0f, 0.7f, null, 1f, null);
								tlist4.Add(iconRenderer3D4);
							}
							if (pbsBatlleSide.GetValueOrDefault() != PbsBatlleSide.Defender)
							{
								Tlist<{22913}.IconRenderer3D> tlist5 = this.{23027};
								{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(pbRespawn[PbsBatlleSide.Defender].Center, {22913}.c_enterpb_mark_dim, delegate()
								{
									this.{22924}(false, capturePort);
								}, 0f, 0.7f, null, 1f, null);
								tlist5.Add(iconRenderer3D4);
							}
						}
						else if (pbRespawn.Count > 0)
						{
							Tlist<{22913}.IconRenderer3D> tlist6 = this.{23027};
							{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(pbRespawn[PbsBatlleSide.Attacker].Center, {22913}.c_enterpb_mark_dim, delegate()
							{
								this.{22924}(true, capturePort);
							}, 0f, 0.7f, null, 1f, null).SetVisibilityGroups(new {22887}.Id[]
							{
								{22887}.Id.PbRespawns
							});
							tlist6.Add(iconRenderer3D4);
							Tlist<{22913}.IconRenderer3D> tlist7 = this.{23027};
							iconRenderer3D4 = new {22913}.IconRenderer3D(pbRespawn[PbsBatlleSide.Defender].Center, {22913}.c_enterpb_mark_dim, delegate()
							{
								this.{22924}(false, capturePort);
							}, 0f, 0.7f, null, 1f, null).SetVisibilityGroups(new {22887}.Id[]
							{
								{22887}.Id.PbRespawns
							});
							tlist7.Add(iconRenderer3D4);
						}
						using (IEnumerator<PbsBuildingStatus> enumerator5 = ((IEnumerable<PbsBuildingStatus>)capturePort.Buildings).GetEnumerator())
						{
							while (enumerator5.MoveNext())
							{
								PbsBuildingStatus fortBuilding = enumerator5.Current;
								if (fortBuilding.IsFort)
								{
									Rectangle path = (!capturePort.IsCapturedByGuild) ? {22913}.c_fort_enemy : ((Session.Guild != null && capturePort.CapturedBy == Session.Guild.Tag) ? {22913}.c_fort_my : {22913}.c_fort_neutral);
									Tlist<{22913}.IconRenderer3D> tlist8 = this.{23027};
									{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(fortBuilding.Place.Position, path, delegate()
									{
										this.{22950}(path, fortBuilding, capturePort);
									}, 0f, 1f, delegate()
									{
										if (Session.Guild != null && Session.Guild.Tag == capturePort.CapturedBy)
										{
											Global.Network.Send(new OnBuildingHoldOperatingMsg(fortBuilding.uIDDynamicServerBuild, new GSI(), new GSI(), false));
										}
									}, 1f, null)
									{
										VisibleOnZoom = {22913}.c_visibleTowersAtScale
									}.SetVisibilityGroups(new {22887}.Id[]
									{
										{22887}.Id.FortsAndTowers
									});
									tlist8.Add(iconRenderer3D4);
								}
								if (fortBuilding.IsTower)
								{
									Rectangle path = (fortBuilding.Strength > 0f && fortBuilding.Server_CapturedByAttacker) ? {22913}.c_towerCaptured : {22913}.TowerTexPath(fortBuilding.Strength / fortBuilding.MaxStrength);
									Tlist<{22913}.IconRenderer3D> tlist9 = this.{23027};
									{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(fortBuilding.Place.Position, path, delegate()
									{
										this.{22954}(path, fortBuilding, capturePort);
									}, 0f, 0.7f, null, 1f, null)
									{
										VisibleOnZoom = {22913}.c_visibleTowersAtScale
									}.SetVisibilityGroups(new {22887}.Id[]
									{
										{22887}.Id.FortsAndTowers
									});
									tlist9.Add(iconRenderer3D4);
								}
							}
						}
						if (!this.{23049})
						{
							if (capturePort.TimeToBattleCompletion > 0f)
							{
								Tlist<{22913}.IconRenderer3D> tlist10 = this.{23026};
								{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(capturePort.PortInstance.EntryPos, {22913}.c_battleHighlight, null, 0f, 1f, null, 1f, null)
								{
									InitialColor = Color.Red * 0.45f
								};
								tlist10.Add(iconRenderer3D4);
							}
							else if (capturePort.EmpireCaptureLevel > 0.01f)
							{
								Tlist<{22913}.IconRenderer3D> tlist11 = this.{23026};
								{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(capturePort.PortInstance.EntryPos, {22913}.c_battleHighlight, null, 0f, 1f, null, 1f, null)
								{
									InitialColor = new Color(119, 164, 255) * 0.8f
								};
								tlist11.Add(iconRenderer3D4);
							}
							else if (capturePort.Tensity.Effects.BlockFortWindowActions)
							{
								Tlist<{22913}.IconRenderer3D> tlist12 = this.{23026};
								{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(capturePort.PortInstance.EntryPos, {22913}.c_battleHighlight, null, 0f, 1f, null, 1f, null)
								{
									InitialColor = Color.Orange * 0.4f
								};
								tlist12.Add(iconRenderer3D4);
							}
							else if (capturePort.Display_IsWindowActive && !capturePort.PortInstance.CaptureWithoutWindow && capturePort.TimeToProtectionTensity == 0f)
							{
								Tlist<{22913}.IconRenderer3D> tlist13 = this.{23026};
								{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(capturePort.PortInstance.EntryPos, {22913}.c_battleHighlight, null, 0f, 1f, null, 1f, null)
								{
									InitialColor = Color.LightGray * 0.4f
								};
								tlist13.Add(iconRenderer3D4);
							}
						}
					}
					bool flag2 = Session.Guild != null && Session.Guild.HasHqOrLicense(capturePort.PortID);
					Color color = new Color(250, 211, 109);
					if (capturePort.IsCapturedByGuild)
					{
						iconRenderer3D3.AltText = "[" + capturePort.CapturedBy + "]";
						if (!string.IsNullOrEmpty(capturePort.VassalTag))
						{
							{22913}.IconRenderer3D iconRenderer3D5 = iconRenderer3D3;
							iconRenderer3D5.AltText = iconRenderer3D5.AltText + " | [" + capturePort.VassalTag + "]";
						}
						Relation relation = (Session.Guild == null) ? Session.Account.Reputations.NeutralStatusWith(capturePort.CapturerFraction) : ((capturePort.StatusWith(Session.Guild) == Relation.Ally) ? Relation.Ally : ((FractionAPI.StatusWith(capturePort.CapturerFraction, Session.Guild.Fraction) == Relation.Enemy) ? Relation.Enemy : Relation.Neutral));
						Color altTextColor = (relation == Relation.Ally) ? (new Color(141, 173, 110) * 1.2f) : ((relation == Relation.Enemy) ? (flag2 ? color : new Color(255, 155, 109)) : Color.LightGray);
						iconRenderer3D3.AltTextColor = altTextColor;
						if (flag2)
						{
							{22913}.IconRenderer3D iconRenderer3D6 = iconRenderer3D3;
							iconRenderer3D6.AltText += " ⚖";
						}
						if (Session.Guild != null && (FractionAPI.StatusWith(capturePort.CapturerFraction, Session.Guild.Fraction) == Relation.Ally || Session.Guild.IsTrusted(capturePort.CapturedBy, false)))
						{
							iconRenderer3D3.VisibilityGroups = iconRenderer3D3.VisibilityGroups.Concat(new {22887}.Id[]
							{
								{22887}.Id.AllyFractionPorts
							}).ToArray<{22887}.Id>();
						}
					}
					else if (flag2)
					{
						iconRenderer3D3.AltText = "⚖";
						iconRenderer3D3.AltTextColor = color;
					}
				}
			}
			foreach (PortCaptureStatus portCaptureStatus2 in ((IEnumerable<PortCaptureStatus>)this.{23020}))
			{
				bool flag3 = portCaptureStatus2 is ArabPortCaptureStatus || portCaptureStatus2 is PiratePortCaptureStatus;
				if (flag3 && portCaptureStatus2.PortInstance.AllowCapturePiratePort && portCaptureStatus2.IsCapturedByGuild)
				{
					{22913}.IconRenderer3D iconRenderer3D7 = this.{23035}[portCaptureStatus2.PortID];
					iconRenderer3D7.AltText = "[" + portCaptureStatus2.CapturedBy + "]";
					iconRenderer3D7.AltTextColor = ((Session.Guild != null && Session.Guild.Fraction != FractionID.Pirate) ? (new Color(141, 173, 110) * 1.2f) : (Color.LightGray * 0.7f));
				}
			}
			using (IEnumerator<GlobalMapOnlineEvent> enumerator6 = ((IEnumerable<GlobalMapOnlineEvent>){22916}.globalEvents).GetEnumerator())
			{
				while (enumerator6.MoveNext())
				{
					{22913}.<>c__DisplayClass152_5 CS$<>8__locals6 = new {22913}.<>c__DisplayClass152_5();
					CS$<>8__locals6.<>4__this = this;
					CS$<>8__locals6.globalEvent = enumerator6.Current;
					{22913}.<>c__DisplayClass152_6 CS$<>8__locals7 = new {22913}.<>c__DisplayClass152_6();
					CS$<>8__locals7.CS$<>8__locals5 = CS$<>8__locals6;
					bool flag4 = CS$<>8__locals7.CS$<>8__locals5.globalEvent.Type == GlobalEventMarkerType.NpcEvent && CS$<>8__locals7.CS$<>8__locals5.globalEvent.Argument == -1;
					{22913}.<>c__DisplayClass152_6 CS$<>8__locals8 = CS$<>8__locals7;
					Rectangle path2;
					if (CS$<>8__locals7.CS$<>8__locals5.globalEvent.Type != GlobalEventMarkerType.PlayerVisible && !flag4)
					{
						if (CS$<>8__locals7.CS$<>8__locals5.globalEvent.Type != GlobalEventMarkerType.NpcEvent)
						{
							if (CS$<>8__locals7.CS$<>8__locals5.globalEvent.Type != GlobalEventMarkerType.RareTreasures)
							{
								if (CS$<>8__locals7.CS$<>8__locals5.globalEvent.Type != GlobalEventMarkerType.PlayerAdvert)
								{
									throw new Exception();
								}
								path2 = (((byte)CS$<>8__locals7.CS$<>8__locals5.globalEvent.Argument == 3) ? {22913}.c_advertGray : {22913}.c_advert);
							}
							else
							{
								path2 = {22913}.c_quest_redChest;
							}
						}
						else
						{
							path2 = ((Gameplay.NpcsInfo[CS$<>8__locals7.CS$<>8__locals5.globalEvent.Argument].Descritpion == NpcType.Empire_Legendary3l) ? {22913}.c_event_redSkull : ((Gameplay.NpcsInfo[CS$<>8__locals7.CS$<>8__locals5.globalEvent.Argument].Descritpion == NpcType.Empire_Legendary2l) ? {22913}.c_event_yellowEmpire : (Gameplay.NpcsInfo[CS$<>8__locals7.CS$<>8__locals5.globalEvent.Argument].Extras.IsTrader ? {22913}.c_event_yellowCaravan : {22913}.c_event_yellowSkull)));
						}
					}
					else
					{
						path2 = {22913}.c_quest_grayFlag;
					}
					CS$<>8__locals8.path = path2;
					float num3 = (CS$<>8__locals7.CS$<>8__locals5.globalEvent.Type == GlobalEventMarkerType.PlayerAdvert && (byte)CS$<>8__locals7.CS$<>8__locals5.globalEvent.Argument == 3) ? 360f : 1099.7646f;
					CompressedPosition position = CS$<>8__locals7.CS$<>8__locals5.globalEvent.Position;
					Vector2 position2 = position.Position;
					float {23067} = (CS$<>8__locals7.path == {22913}.c_event_yellowSkull || CS$<>8__locals7.path == {22913}.c_event_yellowEmpire || CS$<>8__locals7.path == {22913}.c_event_yellowCaravan || CS$<>8__locals7.path == {22913}.c_quest_grayFlag) ? 0.8f : 1f;
					if ((flag4 || CS$<>8__locals7.CS$<>8__locals5.globalEvent.AlwaysVisibleOnMap || Vector2.Distance(position2, Global.Player.Position) <= num3) && (CS$<>8__locals7.CS$<>8__locals5.globalEvent.Type != GlobalEventMarkerType.PlayerAdvert || CS$<>8__locals7.CS$<>8__locals5.globalEvent.PlayerAdvertSID != Session.Account.SID))
					{
						if (!Session.Account.FogOfWar.IsOpened(position2, 0))
						{
							if (flag4 || CS$<>8__locals7.CS$<>8__locals5.globalEvent.SilhouetteInFogOfWar)
							{
								Tlist<{22913}.IconRenderer3D> tlist14 = this.{23027};
								{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(position2, CS$<>8__locals7.path, null, 0f, {23067}, null, 1f, null).SetVisibilityGroups(new {22887}.Id[]
								{
									{22887}.Id.GlobalEvent
								}).SetColor(Color.White * 0.5f);
								tlist14.Add(iconRenderer3D4);
							}
						}
						else if (!Session.Account.WantedLevelShowsMeAtMap || CS$<>8__locals7.CS$<>8__locals5.globalEvent.Type != GlobalEventMarkerType.PlayerAdvert || GlobalMapOnlineEvent.PlayerAdvertOptions[CS$<>8__locals7.CS$<>8__locals5.globalEvent.Argument].pvpMode)
						{
							{22913}.IconRenderer3D iconRenderer3D8 = new {22913}.IconRenderer3D(position2, CS$<>8__locals7.path, delegate()
							{
								{22913} <>4__this = CS$<>8__locals7.CS$<>8__locals5.<>4__this;
								Rectangle path3 = CS$<>8__locals7.path;
								string {22921} = "";
								string description = CS$<>8__locals7.CS$<>8__locals5.globalEvent.GetDescription();
								Action<ToolTipState> {22923};
								if (({22923} = CS$<>8__locals7.CS$<>8__locals5.<>9__11) == null)
								{
									{22923} = (CS$<>8__locals7.CS$<>8__locals5.<>9__11 = delegate(ToolTipState {23098})
									{
										if (CS$<>8__locals7.CS$<>8__locals5.globalEvent.Type == GlobalEventMarkerType.PlayerAdvert)
										{
											if ((byte)CS$<>8__locals7.CS$<>8__locals5.globalEvent.Argument != 3)
											{
												{23098}.AppendText(Local.mapAdvert_alert, Color.Gray, false, false);
											}
											if (!GlobalMapOnlineEvent.PlayerAdvertOptions[CS$<>8__locals7.CS$<>8__locals5.globalEvent.Argument].pvpMode)
											{
												{23098}.AppendText(CS$<>8__locals7.CS$<>8__locals5.globalEvent.PlayerAdvertNickname, Color.White, true, false);
												{23098}.AppendText(Local.ChatBoxGui_20, Color.SoftLime, true, false);
											}
										}
									});
								}
								<>4__this.SimpleToolTipCreator(path3, {22921}, description, {22923});
							}, 0f, {23067}, null, 1f, null)
							{
								IsFlickering = CS$<>8__locals7.CS$<>8__locals5.globalEvent.IsFlickering
							};
							iconRenderer3D8.SetVisibilityGroups(new {22887}.Id[]
							{
								{22887}.Id.GlobalEvent
							});
							if (CS$<>8__locals7.CS$<>8__locals5.globalEvent.Type == GlobalEventMarkerType.PlayerAdvert && !GlobalMapOnlineEvent.PlayerAdvertOptions[CS$<>8__locals7.CS$<>8__locals5.globalEvent.Argument].pvpMode && (byte)CS$<>8__locals7.CS$<>8__locals5.globalEvent.Argument != 3)
							{
								{22913}.IconRenderer3D iconRenderer3D9 = iconRenderer3D8;
								iconRenderer3D9.ClickToolTip = (Action)Delegate.Combine(iconRenderer3D9.ClickToolTip, new Action(delegate()
								{
									new {17558}(new {17549}(CS$<>8__locals7.CS$<>8__locals5.globalEvent.PlayerAdvertSID, CS$<>8__locals7.CS$<>8__locals5.globalEvent.PlayerAdvertNickname, Array.Empty<{17549}.OptionalAction>()));
								}));
							}
							this.{23027}.Add(iconRenderer3D8);
						}
					}
				}
			}
			using (IEnumerator<IslePortInfo> enumerator7 = ((IEnumerable<IslePortInfo>)Gameplay.WorldMap.Ports).GetEnumerator())
			{
				while (enumerator7.MoveNext())
				{
					IslePortInfo port = enumerator7.Current;
					if (this.{23035}[port.PortID].IsVisibe)
					{
						using (IEnumerator<IslePortPharosInfo> enumerator8 = ((IEnumerable<IslePortPharosInfo>)port.ReferencedPharosesTransformed).GetEnumerator())
						{
							while (enumerator8.MoveNext())
							{
								IslePortPharosInfo item = enumerator8.Current;
								if (!this.{23050}.Any((Vector2 {23099}) => Vector2.DistanceSquared(item.MapGlobalPosition, {23099}) < 10000f))
								{
									Tlist<{22913}.IconRenderer3D> tlist15 = this.{23027};
									{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(item.MapGlobalPosition, {22913}.c_iconLighhouse, delegate()
									{
										this.{22935}(item, port);
									}, 0f, 0.8f, null, 1f, null)
									{
										VisibleOnZoom = {22913}.c_visibleTowersAtScale
									}.SetColor(Color.White * 0.6f).SetVisibilityGroups(new {22887}.Id[]
									{
										{22887}.Id.Port
									});
									tlist15.Add(iconRenderer3D4);
								}
							}
						}
					}
				}
			}
			this.OnGroupMembersUpdate();
			this.{23032} = new {22913}.IconRenderer3D(new Vector2(10000000f, 0f), {22913}.c_iconMarker, null, 0f, 0.75f, null, 1f, null);
			this.{23032}.RealTimeUpdate = (() => Session.WorldMapMarkerPosition);
			this.{23027}.Add(this.{23032});
			this.{23049} = true;
			this.{22985}();
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000BEBEC File Offset: 0x000BCDEC
		private void {22917}(in OnLoadGlobalMapResponse {22918})
		{
			Rectangle uiarea = Engine.GS.UIArea;
			base.AddChild(new Image(new Marker(ref uiarea), AtlasPortGui.Texture.Tex, new Rectangle(502, 301, 258, 136), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				RenderToDepthMap = false,
				PositionAlignment_X = PositionAlignment.Both,
				PositionAlignment_Y = PositionAlignment.Both
			});
			StackForm stack = new StackForm(new Vector2(8f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			int num = 309;
			CustomSpriteFont philosopher_14Bold = Fonts.Philosopher_14Bold;
			CustomSpriteFont arial_ = Fonts.Arial_12;
			Vector2 vector = new Vector2(19f, 19f);
			Color color = new Color(239, 201, 255) * 0.8039216f;
			Color color2 = new Color(181, 210, 255) * 0.8039216f;
			Color color3 = Color.White * 0.8039216f;
			if ({22918}.pvpLeaderboard.Size > 0)
			{
				int num2 = 70 + {22918}.pvpLeaderboard.Size * 19;
				Form form = new Form(new Marker(0f, 0f, (float)num, (float)num2), {22913}.c_formBack_black, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				StackForm stackForm = new StackForm(vector, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, philosopher_14Bold, color3, "☠" + Local.WorldMapUi_leg, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				stackForm.BorderThickness = -2f;
				stackForm.AddSpace(7f);
				foreach (string text in ((IEnumerable<string>){22918}.pvpLeaderboard))
				{
					string[] array = text.Split(new char[]
					{
						'$'
					}, 2);
					string {13345} = array[0];
					string {13345}2 = array[1];
					StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					Label label = new Label(Vector2.Zero, Fonts.Philosopher_14, new Color(255, 199, 178) * 0.8039216f, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					stackForm2.AddItem(new UiControl[]
					{
						label
					});
					stackForm2.AddSpace(Math.Max(1f, 40f - label.Pos.WH.X));
					stackForm2.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Philosopher_14, Color.White * 0.68235296f, {13345}2, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					stackForm.AddItem(new UiControl[]
					{
						stackForm2
					});
				}
				form.AddChild(stackForm);
				stack.AddItem(new UiControl[]
				{
					form
				});
			}
			if ({22918}.daysToEmpireInvasion > 0 && Session.EventActionsPipeline.EventCategoryAmount(EActionCaterory.BEmpireAttack) == 0f)
			{
				int num3 = 60;
				Form form2 = new Form(new Marker(0f, 0f, (float)num, (float)num3), {22913}.c_formBack_violet, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form2.AddChild(new Label(vector, philosopher_14Bold, color, ({22918}.daysToEmpireInvasion == 1) ? Local.WorldMapUi_56_yester : Local.WorldMapUi_56({22918}.daysToEmpireInvasion), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				stack.AddItem(new UiControl[]
				{
					form2
				});
			}
			if ({22918}.daysToSeasonEnd <= 7)
			{
				int num4 = 60;
				Form form3 = new Form(new Marker(0f, 0f, (float)num, (float)num4), {22913}.c_formBack_violet, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form3.AddChild(new Label(vector, philosopher_14Bold, color, Local.WorldMapUi_season_end({22918}.daysToSeasonEnd), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				stack.AddItem(new UiControl[]
				{
					form3
				});
			}
			using (IEnumerator<EventActionBase> enumerator2 = ((IEnumerable<EventActionBase>)Session.EventActionsPipeline.CurrentActions).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					EventActionBase item = enumerator2.Current;
					EABehavior1 eabehavior = item.Behavior as EABehavior1;
					if (eabehavior != null && eabehavior.ShouldShowAtWorldMap)
					{
						int num5 = 114;
						Form form4 = new Form(new Marker(0f, 0f, (float)num, (float)num5), (eabehavior.Category == EActionCaterory.BEmpireAttack) ? {22913}.c_formBack_violet : {22913}.c_formBack_blue, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							AnimatedFocus = false
						};
						form4.AddChild(new Label(vector, philosopher_14Bold, (eabehavior.Category == EActionCaterory.BEmpireAttack) ? color : color2, item.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							Shadowed = true
						});
						form4.AddChild(TextBlockBuilder.CreateBlock(231f, item.Description.Substring(0, Math.Min(item.Description.Length, 55)) + "... [" + Local.url_button + "]", (eabehavior.Category == EActionCaterory.BEmpireAttack) ? color : color2, arial_, -3f).Create(vector + new Vector2(0f, 24f)));
						stack.AddItem(new UiControl[]
						{
							form4
						});
						form4.EvClick += delegate(ClickUiEventArgs {23104})
						{
							new {19779}(false, item, null);
						};
					}
				}
			}
			if (this.{23034}.Count((ValueTuple<{22913}.IconRenderer3D, WeatherArea> {23090}) => {23090}.Item2.Type == WeatherAreaType.Storm) > 0)
			{
				int num6 = 83;
				Form form5 = new Form(new Marker(0f, 0f, (float)num, (float)num6), {22913}.c_formBack_blue, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form5.AddChild(new Label(vector, philosopher_14Bold, color2, Local.strom, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					Shadowed = true
				});
				form5.AddChild(TextBlockBuilder.CreateBlock(231f, Local.storm_tt(MathF.Round(Gameplay.StormBonusSpeed(1f, 0f) * PlayerShipInfo.Temp_displaySpeedRefactoring, 1)), color2, arial_, 0f).Create(vector + new Vector2(0f, 24f)));
				stack.AddItem(new UiControl[]
				{
					form5
				});
			}
			base.AddChild(stack);
			Form form6 = new Form(new Vector2(8f, (float)(Engine.GS.UIArea.Height - {22913}.c_formBack_black_down.Height - 8)), {22913}.c_formBack_black_down, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				PositionAlignment_Y = PositionAlignment.RightDown,
				AnimatedFocus = false
			};
			form6.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, color3, Local.WorldMapUi_downTt, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, 50f);
			base.AddChild(form6);
			Form downFormAdvert = new Form(new Vector2(8f, (float)(Engine.GS.UIArea.Height - {22913}.c_formBack_black_down.Height * 2 - 8)), {22913}.c_formBack_advert, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				PositionAlignment_Y = PositionAlignment.RightDown,
				AnimatedFocus = false
			};
			downFormAdvert.AddChildPos(new LiveLabel(Vector2.Zero, Fonts.Philosopher_14, color3, () => Local.WorldMapUi_advert + " " + ((Session.Account.WorldMapAdvertActiveCooldownSec > 0f) ? StringHelper.TimeMMMSS((double)Session.Account.WorldMapAdvertActiveCooldownSec) : ((Session.Account.WorldMapAdvertCooldownSec > 0f) ? StringHelper.TimeMMMSS((double)Session.Account.WorldMapAdvertCooldownSec) : string.Empty)), 100), PositionAlignment.LeftUp, PositionAlignment.Center, 50f);
			downFormAdvert.Opacity = 0.9f;
			base.AddChild(downFormAdvert);
			downFormAdvert.UpdateComplete += delegate(UiControl {23100})
			{
				downFormAdvert.TexturePath = ((Session.Account.WorldMapAdvertActiveCooldownSec > 0f) ? {22913}.c_formBack_advert_active : {22913}.c_formBack_advert);
				downFormAdvert.Brightness = ((Session.Account.WorldMapAdvertActiveCooldownSec == 0f && Session.Account.WorldMapAdvertCooldownSec > 0f) ? 0.5f : 1f);
			};
			Action <>9__8;
			form6.EvClick += delegate(ClickUiEventArgs {23101})
			{
				if (stack.IsVisible)
				{
					this.{23045}.Pos = this.{23045}.Pos.SetXY({23101}.Sender.Pos.XY.X, {23101}.Sender.Pos.XY.Y - this.{23045}.Pos.WH.Y);
					this.{23045}.IsVisible = true;
					new UiOpacityAnimation(this.{23045}, 0f, 1f, 300f);
					if (this.{23046} != null)
					{
						this.{23046}.IsVisible = true;
						new UiOpacityAnimation(this.{23046}, 0f, 1f, 300f);
					}
				}
				else
				{
					new UiOpacityAnimation(this.{23045}, 1f, 0f, 300f);
					UiControl {14194} = this.{23045};
					Action {14195};
					if (({14195} = <>9__8) == null)
					{
						{14195} = (<>9__8 = delegate()
						{
							this.{23045}.IsVisible = false;
						});
					}
					new UiActor({14194}, {14195});
					if (this.{23046} != null)
					{
						this.{23046}.IsVisible = false;
						new UiOpacityAnimation(this.{23046}, 1f, 0f, 300f);
					}
				}
				stack.IsVisible = !stack.IsVisible;
			};
			downFormAdvert.EvClick += delegate(ClickUiEventArgs {23091})
			{
				{22827}.Open();
			};
			downFormAdvert.UpdateComplete += delegate(UiControl {23102})
			{
				{23102}.Opacity = (this.{23045}.IsVisible ? (1f - this.{23045}.Opacity) : 1f);
			};
			Form form7 = new Form(Vector2.Zero, new Rectangle(180, 0, 14, 14), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form6.AddChildPos(form7, PositionAlignment.RightDown, PositionAlignment.Center, 25f);
			form7.UpdateComplete += delegate(UiControl {23103})
			{
				{23103}.IsVisible = (this.{23045} != null && this.{23045}.IsFiltersChanged());
			};
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x000BF43C File Offset: 0x000BD63C
		public void UpdateState(PbsStatus {22919})
		{
			for (int i = 0; i < this.{23020}.Size; i++)
			{
				if (this.{23020}.Array[i].PortID == {22919}.PortID)
				{
					this.{23020}.Array[i] = {22919};
				}
			}
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x000BF488 File Offset: 0x000BD688
		protected override void UserBackRender()
		{
			Color.White * base.Opacity;
			bool highDetailing = Global.Settings.HighDetailing;
			Global.Settings.HighDetailing = false;
			Engine.GS.End2D();
			Engine.GS.SetRenderTarget(Global.Render.rtMain3d);
			Engine.GS.ClearDepthBuffer();
			bool multiSampleAntiAlias = Engine.GS.GetRasterizerState().MultiSampleAntiAlias;
			if (Global.Settings.AntialiasingQuality != LocalSettings.RendererAntialias.Off)
			{
				Engine.GS.SetRasterizerStateOptionAA(true);
			}
			this.{22980}(0, null);
			Engine.GS.ClearDepthBuffer();
			Engine.GS.SetRasterizerStateOptionAA(multiSampleAntiAlias);
			Engine.GS.ReturnRenderTarget();
			ScreenPlaneRenderer.DrawWithDefaultShader(Global.Render.rtMain3d.Resource, null, null);
			Engine.GS.Begin2D(true);
			Engine.GS.SetTexture(OtherTextures.WorldMapUiElements);
			Engine.GS.SetRasterizerStateUiScissor();
			Global.Settings.HighDetailing = highDetailing;
			Vector2 vector = this.{22991}(Engine.GS.MouseToUI);
			if (this.distanceMeterTool)
			{
				if (this.{23044} == null && this.distanceMeterMode)
				{
					this.{23044} = new Vector2?(Engine.GS.MouseToUI);
				}
				Vector2 vector2 = this.{22991}(this.{23044}.GetValueOrDefault(Engine.GS.MouseToUI));
				Vector2 vector3 = vector;
				Vector2 vector4 = this.{22989}(vector2);
				Vector2 vector5 = this.{22989}(vector3);
				Color pink = Color.Pink;
				Engine.GS.Line2D({22913}.cLine, vector4, vector5, pink, 6);
				Engine.GS.SetFont(Fonts.Philosopher_14);
				Vector2 vector6 = vector2 - vector3;
				float num = vector6.Length() / 1819f * 14146.483f;
				string[] array = new string[2];
				int num2 = 0;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
				defaultInterpolatedStringHandler.AppendFormatted<int>((int)num);
				array[num2] = defaultInterpolatedStringHandler.ToStringAndClear();
				array[1] = MathF.Ceiling(CommonSupport.MiddleSailingTime(Global.Player.UsedShip.Speed + Global.Player.UsedShip.MarchingModeSpeed, num) / 60f).ToString() + Local.StringConstants_66_B;
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					CustomSpriteFont customSpriteFont = (i == 0) ? Fonts.Philosopher_14 : Fonts.Arial_10;
					Engine.GS.SetFont(customSpriteFont);
					Vector2 value = customSpriteFont.Measure(text);
					Vector2 value2 = (vector4 + vector5) * 0.5f + new Vector2(0f, (float)(18 * i));
					Device gs = Engine.GS;
					vector6 = value2 - value * 0.5f;
					Rectangle rectangle = new Marker(ref vector6, ref value).Border(3f).ToRect();
					gs.Draw({22913}.cWhite, rectangle, pink);
					Device gs2 = Engine.GS;
					string {14610} = text;
					Color color = Color.Black * 0.8f;
					gs2.DrawStringCentered({14610}, value2, color);
				}
			}
			if (!this.distanceMeterMode)
			{
				this.{23044} = null;
			}
			if ({22913}.mapScale > 1.8000001f)
			{
				Engine.GS.SetFont(Fonts.Philosopher_14Bold);
				float num3 = 0.4f + 0.6f * (float)Engine.GS.UIArea.Width / 1900f;
				float num4 = Math.Min(1.5f, {22913}.mapScale * 0.5f) * 0.9f * num3;
				foreach ({22913}.IconRenderer3D iconRenderer3D in ((IEnumerable<{22913}.IconRenderer3D>)this.{23027}))
				{
					if (!string.IsNullOrEmpty(iconRenderer3D.Text) && iconRenderer3D.IsVisibleByGroups() && iconRenderer3D.IsVisibe)
					{
						bool flag = iconRenderer3D.VisibilityGroups.Contains({22887}.Id.GroupMembers);
						Color value3 = (flag || iconRenderer3D.VisibilityGroups.Length == 0) ? iconRenderer3D.TextColor : ((iconRenderer3D.TextColor == Color.White) ? (Color.White * 1.2f * base.Opacity) : (new Color(206, 173, 130) * 1.2f * base.Opacity));
						if (flag)
						{
							Device gs3 = Engine.GS;
							string text2 = iconRenderer3D.Text;
							Vector2 vector6 = iconRenderer3D.LastSSPositions * new Vector2(1f, 1.01f) + new Vector2(iconRenderer3D.Size.X * 160f, -iconRenderer3D.Size.Y * 120f) + new Vector2(-10f, 0f) * num3;
							Color color = value3 * 0.9f;
							gs3.DrawStringShadowed(text2, vector6, color, num4 * iconRenderer3D.TextScale, 0.5f);
						}
						else
						{
							Device gs4 = Engine.GS;
							string text3 = iconRenderer3D.Text;
							Vector2 vector6 = iconRenderer3D.LastSSPositions * new Vector2(1f, 1.01f) + new Vector2(0f, iconRenderer3D.Size.Y * 160f * num3) * num4;
							gs4.DrawStringCenteredShadow(text3, vector6, value3, num4 * iconRenderer3D.TextScale, 0.5f);
						}
						if (!string.IsNullOrEmpty(iconRenderer3D.AltText))
						{
							Device gs5 = Engine.GS;
							string altText = iconRenderer3D.AltText;
							Vector2 vector6 = iconRenderer3D.LastSSPositions * new Vector2(1f, 1.01f) + new Vector2(0f, iconRenderer3D.Size.Y * 160f * num3 + 20f) * num4;
							Color color = iconRenderer3D.AltTextColor * base.Opacity;
							gs5.DrawStringCenteredShadow(altText, vector6, color, num4, 0.5f);
						}
					}
				}
				if (Session.Account.Rang == 1)
				{
					{22913}.IconRenderer3D iconRenderer3D2 = this.{23027}[0];
					Device gs6 = Engine.GS;
					string youre_here = Local.youre_here;
					Vector2 vector6 = iconRenderer3D2.LastSSPositions * new Vector2(1f, 1.01f) - new Vector2(0f, iconRenderer3D2.Size.Y * 250f * num3) * num4 * 1.1f;
					Color color = new Color(139, 204, 63) * base.Opacity;
					gs6.DrawStringCenteredShadow(youre_here, vector6, color, num4 * 1.1f, 0.5f);
				}
			}
			foreach (ValueTuple<{22913}.IconRenderer3D, WeatherArea> valueTuple in ((IEnumerable<ValueTuple<{22913}.IconRenderer3D, WeatherArea>>)this.{23034}))
			{
				valueTuple.Item1.SetPosition(valueTuple.Item2.CurrentPosition, 0f, true);
			}
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x000BFBA8 File Offset: 0x000BDDA8
		protected override void UserFrontRender()
		{
			Vector2 {23081} = (base.InputMode != MouseInputMode.NoFocus) ? Engine.GS.MouseToUI : new Vector2(1000000f, 100000f);
			Math.Min(this.{23016}, 1f);
			float num = float.MaxValue;
			if (!InputHelper.NowMouseState.RightPressed)
			{
				Matrix matrix;
				this.{23025}.Transform.CreateWorldMatrix(out matrix);
				foreach ({22913}.IconRenderer3D iconRenderer3D in ((IEnumerable<{22913}.IconRenderer3D>)this.{23027}))
				{
					if (iconRenderer3D.CreatToolTipContent != null && iconRenderer3D.IsVisibe && iconRenderer3D.IsVisibleByGroups() && iconRenderer3D.AsPathGeometry == null)
					{
						float screenSpaceDistance = iconRenderer3D.GetScreenSpaceDistance({23081}, ref matrix);
						if (screenSpaceDistance < num)
						{
							this.{23036} = iconRenderer3D;
							num = screenSpaceDistance;
						}
					}
				}
				if (num > 625f)
				{
					foreach ({22913}.IconRenderer3D iconRenderer3D2 in ((IEnumerable<{22913}.IconRenderer3D>)this.{23027}))
					{
						if (iconRenderer3D2.CreatToolTipContent != null && iconRenderer3D2.IsVisibe && iconRenderer3D2.IsVisibleByGroups() && iconRenderer3D2.AsPathGeometry != null)
						{
							float screenSpaceDistance2 = iconRenderer3D2.GetScreenSpaceDistance({23081}, ref matrix);
							if (screenSpaceDistance2 < num)
							{
								this.{23036} = iconRenderer3D2;
								num = screenSpaceDistance2;
							}
						}
					}
				}
			}
			if (num < 625f && !this.distanceMeterTool)
			{
				if (this.{23036}.AsPathGeometry == null)
				{
					this.{23027}.Array[2].Size = new Vector2(this.{23036}.Size.Y + 0.2f);
					this.{23027}.Array[2].SetPosition(this.{23036}.MapPos, (float)(Global.Game.GameTotalTimeSec % 6.2831854820251465), true);
				}
				else
				{
					this.{23027}.Array[2].SetPosition(new Vector2(10000000f, 0f), 0f, true);
				}
				this.{23027}.Array[3].SetPosition(new Vector2(10000000f, 0f), 0f, true);
				if (this.{23036} != this.{23031})
				{
					this.{23039}.IsVisible = true;
					this.{23038} = new ToolTipState(null);
					this.{23036}.CreatToolTipContent();
					this.{23039}.ToolTip.CurrentContent = this.{23038};
					this.{23039}.ToolTip.Opacity = 0.95f;
					this.{23039}.ToolTip.RefreshIfIsOpen();
					this.{23031} = this.{23036};
				}
			}
			else
			{
				this.{23027}.Array[2].SetPosition(new Vector2(10000000f, 0f), 0f, true);
				this.{23027}.Array[3].SetPosition(new Vector2(10000000f, 0f), 0f, true);
				this.{23031} = null;
				this.{23039}.IsVisible = false;
			}
			num = 90000f;
			Vector2 vector = this.{22991}(Engine.GS.MouseToUI) / OtherTextures.WorldMapSize.WidthHeight() - new Vector2(0.5f);
			float y = vector.Y;
			vector.Y = vector.X;
			vector.X = -y;
			vector *= Gameplay.WorldMapSizeXY;
			Vector2 vector2;
			vector = (vector2 = vector * 1.05f);
			this.{23037} = null;
			if (Gameplay.WorldMap.IsInsideMap(vector2, true) && !this.distanceMeterTool)
			{
				this.{23037} = Gameplay.GetNearWorldRegion(vector);
				if (!this.{23039}.IsVisible)
				{
					string text = this.{23037}.Name;
					if (this.{23037}.Feature.HasFlag(WorldMapRegionInfo.FeatureType.NorthRegion))
					{
						text = "❄ " + text;
					}
					float num2 = Math.Max(0.8f, Math.Min(2f, {22913}.mapScale * 0.5f));
					float num3 = 0.8f;
					float num4 = (text.Length > 15) ? 1.3f : 0.9f;
					Vector2 vector3 = Engine.GS.MouseToUI - new Vector2((float){22913}.c_smallToolTip_s.Width * 1.4f * 0.5f, (float){22913}.c_smallToolTip_s.Height) * num2;
					Marker marker = new Marker(ref vector3, (float){22913}.c_smallToolTip_s.Width * 1.4f * num2 * num4, (float){22913}.c_smallToolTip_s.Height * num2 * 1.2f);
					Device gs = Engine.GS;
					Rectangle rectangle = marker.ToRect();
					Color color = Color.White * 0.9f;
					gs.Draw({22913}.c_smallToolTip_s, rectangle, color);
					Engine.GS.SetFont(Fonts.Arial_10);
					Device gs2 = Engine.GS;
					string {14613} = text;
					vector3 = marker.Center + new Vector2(0f, -15f) * num2 * 1.1f;
					color = Color.White * 0.7f;
					gs2.DrawStringCentered({14613}, vector3, color, num2 * num3 * 1.1f);
					Device gs3 = Engine.GS;
					string {14613}2 = Local.WorldMapUi_2(this.{23037}.LevelInt + 1) ?? "";
					vector3 = marker.Center + new Vector2(0f, -3f) * num2;
					color = ((this.{23037}.LevelInt - Global.Player.UsedShipPlayer.CraftFrom.AssotiatedHazardZoneLevel > 2) ? Color.Orange : Color.White);
					gs3.DrawStringCentered({14613}2, vector3, color, num2 * num3);
					if (this.{23020} != null)
					{
						FractionID? regionStatus = this.{23037}.GetRegionStatus(this.{23020}.Enumerate<PbsStatus>());
						Relation relation = (Session.Guild == null || regionStatus == null) ? Relation.Neutral : FractionAPI.StatusWith(Session.Guild.Fraction, regionStatus.Value);
						Color color2 = (relation == Relation.Neutral) ? Color.Gray : ((relation == Relation.Ally) ? Color.SoftLime : Color.IndianRed);
						string text2 = (regionStatus.GetValueOrDefault() == FractionID.None) ? "" : ((regionStatus == null) ? Local.uncaptured_region : regionStatus.Value.GetName());
						Vector2 value = Engine.GS.Font.Measure(text2);
						Vector2 {14627} = marker.Center + new Vector2(0f, 9f) * num2 - value / 2f * num2 * num3;
						Rectangle rectangle2 = (regionStatus == null) ? Rectangle.Empty : CommonAtlas.GetFractionFlagPrerender(regionStatus.Value);
						if (rectangle2.Width > 0)
						{
							int num5 = 20;
							int num6 = 12;
							{14627}.X -= (float)(num5 / 2) * num2;
							Device gs4 = Engine.GS;
							Texture2D tex = CommonAtlas.Texture.Tex;
							rectangle = new Marker(ref {14627}, (float)num5 * num2, (float)num6 * num2).ToRect();
							color = Color.White;
							gs4.DrawCustomTexture(tex, rectangle2, rectangle, color);
							{14627}.X += (float)num5 * num2;
						}
						Engine.GS.DrawString(text2, {14627}, color2, 0f, Vector2.Zero, num2 * num3);
					}
				}
				using (IEnumerator<{22913}.IconRenderer3D> enumerator = ((IEnumerable<{22913}.IconRenderer3D>)this.{23030}).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						{22913}.IconRenderer3D iconRenderer3D3 = enumerator.Current;
						iconRenderer3D3.IsVisibe = (iconRenderer3D3.Tag == this.{23037});
					}
					goto IL_7C6;
				}
			}
			foreach ({22913}.IconRenderer3D iconRenderer3D4 in ((IEnumerable<{22913}.IconRenderer3D>)this.{23030}))
			{
				iconRenderer3D4.IsVisibe = false;
			}
			IL_7C6:
			float num7 = MathF.Pow(Math.Max(0f, 1f - this.{23016}), 3f) * 0.5f;
			if (num7 > 0f)
			{
				Device gs5 = Engine.GS;
				Rectangle rectangle = Engine.GS.UIArea;
				Color color = Color.Black * num7;
				gs5.Draw({22913}.cWhite, rectangle, color);
			}
			Engine.GS.ReturnBackTexture();
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000C0418 File Offset: 0x000BE618
		private void SimpleToolTipCreator(Rectangle {22920}, string {22921}, string {22922}, Action<ToolTipState> {22923} = null)
		{
			if (!string.IsNullOrEmpty({22922}))
			{
				string[] array = {22922}.Split(new string[]
				{
					Environment.NewLine,
					";"
				}, StringSplitOptions.None);
				this.{23038} = new ToolTipState({22921}, (array.Length == 0) ? null : array[0], Array.Empty<ToolTipCharacteristics>());
				for (int i = 1; i < array.Length; i++)
				{
					this.{23038}.AppendText(array[i], {22913}.featureColor, false, false);
				}
			}
			else
			{
				this.{23038} = new ToolTipState({22921}, "", Array.Empty<ToolTipCharacteristics>());
			}
			if ({22923} != null)
			{
				{22923}(this.{23038});
			}
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x000C04B2 File Offset: 0x000BE6B2
		private void {22924}(bool {22925}, PbsStatus {22926})
		{
			this.SimpleToolTipCreator({22913}.c_battleHighlight, {22926}.PortInstance.PortName, {22925} ? Local.WorldMapUi_57 : Local.WorldMapUi_58, null);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x000C04DC File Offset: 0x000BE6DC
		private void {22927}(PbsStatus {22928}, PBInvite {22929}, Vector2 {22930})
		{
			this.SimpleToolTipCreator({22913}.c_battleHighlight, Local.enterPb + " " + {22928}.PortInstance.PortName, "", delegate(ToolTipState {23105})
			{
				{22913}.<>c__DisplayClass159_1 CS$<>8__locals2;
				CS$<>8__locals2.x = {23105};
				if (Session.EngagingInPortBattle == PbsBatlleSide.None)
				{
					if ({22928}.PortInstance != Global.Player.NearAquatoria)
					{
						{22913}.<EnterPBToolTip>g__condition|159_1(Local.pbenter_problem1, false, ref CS$<>8__locals2);
					}
					if ({22929}.Status.HasFlag(PbsEngageProblem.NoRespawns))
					{
						{22913}.<EnterPBToolTip>g__condition|159_1(Local.pbenter_problem2, false, ref CS$<>8__locals2);
					}
					if ({22929}.Status.HasFlag(PbsEngageProblem.TeamHaveFullFill))
					{
						{22913}.<EnterPBToolTip>g__condition|159_1(Local.pbenter_problem3, false, ref CS$<>8__locals2);
					}
					if ({22929}.Status.HasFlag(PbsEngageProblem.TrustedGuilds))
					{
						{22913}.<EnterPBToolTip>g__condition|159_1(Local.pbenter_problem4, false, ref CS$<>8__locals2);
					}
					if ({22929}.Status.HasFlag(PbsEngageProblem.NotPlacesForOtherParticipants))
					{
						{22913}.<EnterPBToolTip>g__condition|159_1(Local.pbenter_problem5, false, ref CS$<>8__locals2);
					}
					if (Session.Guild != null && !Session.Guild.GetMember(Session.Account.SID).Rights.AllowEngageInPortBattle)
					{
						{22913}.<EnterPBToolTip>g__condition|159_1(Local.pbenter_problem6, false, ref CS$<>8__locals2);
					}
					if ({22929}.Status.HasFlag(PbsEngageProblem.HavingQuestsOrEffects))
					{
						{22913}.<EnterPBToolTip>g__condition|159_1(Local.pbenter_problem7, false, ref CS$<>8__locals2);
					}
					if ({22929}.Status.HasFlag(PbsEngageProblem.ShallowOrShipRistrictions))
					{
						{22913}.<EnterPBToolTip>g__condition|159_1(Local.pbenter_problem8, false, ref CS$<>8__locals2);
					}
					if ({22929}.Status.HasFlag(PbsEngageProblem.OnlyOneSuchShipCanBe))
					{
						{22913}.<EnterPBToolTip>g__condition|159_1(Local.pbenter_problem9(Global.Player.CraftFrom.ShipName), false, ref CS$<>8__locals2);
					}
				}
				else
				{
					CS$<>8__locals2.x.AppendText(Local.pbenter_already, Color.Orange, false, false);
				}
				string text;
				if ({22928}.PortInstance.PortBattleTeamLimitation != 2147483647)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 1);
					defaultInterpolatedStringHandler.AppendLiteral(" / ");
					defaultInterpolatedStringHandler.AppendFormatted<int>({22928}.PortInstance.PortBattleTeamLimitation);
					text = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				else
				{
					text = "";
				}
				string str = text;
				CS$<>8__locals2.x.AppendText(Local.pbenter_team_att + {22929}.UsedPlacesAttacker.ToString() + str, Color.Gray, false, false);
				CS$<>8__locals2.x.AppendText(Local.pbenter_team_def + {22929}.UsedPlacesDefender.ToString() + str, Color.Gray, false, false);
				if ({22929}.MaxPlacesForOthers != 0)
				{
					ToolTipState x = CS$<>8__locals2.x;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(3, 3);
					defaultInterpolatedStringHandler2.AppendFormatted(Local.pbenter_team_trusted);
					defaultInterpolatedStringHandler2.AppendFormatted<int>({22929}.UsedPlacesForOthers);
					defaultInterpolatedStringHandler2.AppendLiteral(" / ");
					defaultInterpolatedStringHandler2.AppendFormatted<int>({22929}.MaxPlacesForOthers);
					x.AppendText(defaultInterpolatedStringHandler2.ToStringAndClear(), Color.Gray, false, false);
				}
				CS$<>8__locals2.x.AppendText(Local.pbenter_team_tooltip, Color.Gray * 0.8f, false, false);
			});
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x000C0538 File Offset: 0x000BE738
		private void {22931}(TraderInSeaPlaceInfo {22932}, QuestInfo {22933}, bool {22934})
		{
			this.SimpleToolTipCreator({22913}.c_iconTrader, {22932}.Name, {22932}.Description, delegate(ToolTipState {23106})
			{
				if ({22933} != null && !string.IsNullOrEmpty({22933}.TextToolTipOrNull))
				{
					{23106}.AppendText(" ", Color.White, false, false);
					{23106}.AppendText({22933}.TextToolTipOrNull, Color.Wheat, false, false);
				}
			});
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x000C0575 File Offset: 0x000BE775
		private void {22935}(IslePortPharosInfo {22936}, IslePortInfo {22937})
		{
			this.SimpleToolTipCreator({22913}.c_iconLighhouse, Local.WorldMapUi_pharosHeader, Local.WorldMapUi_pharosText({22937}.PortName), null);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x000C0593 File Offset: 0x000BE793
		private void {22938}(Rectangle {22939}, QuestInfo {22940})
		{
			this.{23038} = QuestHelper.GetQuestToolTip(true, {22940});
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000C05A4 File Offset: 0x000BE7A4
		private void {22941}(FactoryPlaceIsleInfo {22942}, PersonalIsleStatus {22943})
		{
			if ({22943} == null || !{22943}.AllowSendExpetionsCurrently(Session.Account))
			{
				return;
			}
			if ((float){22943}.ExpeditionPriceCT > {22943}.IsleCraftTime.Value)
			{
				new {17107}(Local.personal_isle_craftTime_not_enough(Math.Floor((double){22943}.IsleCraftTime.Value), {22943}.ExpeditionPriceCT), "");
				return;
			}
			if (!{20547}.CheckWarehouse({22943}))
			{
				return;
			}
			PlayerBuildingState[] array = {22943}.FetchAvailableFactoriesForExpedition(Session.Account);
			string text = "";
			foreach (PlayerBuildingState playerBuildingState in array)
			{
				text = text + Environment.NewLine + playerBuildingState.TypeInfo.Name;
			}
			new {17107}("", Local.activity_expedition_ask({22943}.ExpeditionPriceCT), (text.Length == 0) ? "-" : text, delegate(int {23107})
			{
				if ({23107} == 0)
				{
					int size = Session.Account.WorldActivities.Size;
					if ({22943}.CreateExpeditions(Session.Account))
					{
						Global.Network.Send(new OnPersonalIsleRequest(OnPersonalIsleRequest.Operation.RunExpeditions, {22943}.PlaceIndex, 0, 0f));
						for (int j = size; j < Session.Account.WorldActivities.Size; j++)
						{
							this.{22976}(Session.Account.WorldActivities.Array[j]);
						}
						new {17107}("", Local.activity_expedition_success);
						return;
					}
					new {17107}("", Local.activity_expedition_err);
				}
			}, true, null, new string[]
			{
				Local.to_continue,
				Local.to_back
			});
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x000C06E0 File Offset: 0x000BE8E0
		private void {22944}(FactoryPlaceIsleInfo {22945}, PersonalIsleStatus {22946})
		{
			if ({22946} == null)
			{
				this.SimpleToolTipCreator(Rectangle.Empty, Local.FcTemp_PersonalIsle, Local.WorldMapUi_personalIsle + ((Session.Account.Rang < EducationHelper.PersonalIsleAvailableSinceRank) ? (". " + Local.CaptainSkillsInfoWindow_243(EducationHelper.PersonalIsleAvailableSinceRank)) : ""), delegate(ToolTipState {23092})
				{
				});
				return;
			}
			Tlist<TextBlockBuilder> tlist = new Tlist<TextBlockBuilder>();
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_12, 0f);
			TextBlockBuilder textBlockBuilder2 = textBlockBuilder;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
			defaultInterpolatedStringHandler.AppendFormatted(Local.personal_isle_craftTime);
			defaultInterpolatedStringHandler.AppendFormatted<int>((int){22946}.IsleCraftTime.Value);
			defaultInterpolatedStringHandler.AppendLiteral(" / ");
			defaultInterpolatedStringHandler.AppendFormatted<int>({22946}.IsleCraftTimeLimit);
			textBlockBuilder2.WriteLine(defaultInterpolatedStringHandler.ToStringAndClear(), Color.Wheat);
			TextBlockBuilder textBlockBuilder3 = textBlockBuilder;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(7, 5);
			defaultInterpolatedStringHandler2.AppendFormatted(Local.storage_d);
			defaultInterpolatedStringHandler2.AppendFormatted<double>(Math.Ceiling((double)({22946}.StorageResources.ComputeMass<ResourceInfo>() / 1000f)));
			defaultInterpolatedStringHandler2.AppendLiteral(" ");
			defaultInterpolatedStringHandler2.AppendFormatted(Local.tonn);
			defaultInterpolatedStringHandler2.AppendLiteral(". / ");
			defaultInterpolatedStringHandler2.AppendFormatted<int>({22946}.StorageLimit / 1000);
			defaultInterpolatedStringHandler2.AppendLiteral(" ");
			defaultInterpolatedStringHandler2.AppendFormatted(Local.tonn);
			defaultInterpolatedStringHandler2.AppendLiteral(".");
			textBlockBuilder3.WriteLine(defaultInterpolatedStringHandler2.ToStringAndClear(), new Color(122, 127, 109) * 1.4f);
			if ({22946}.StorageResources.IsEmpty)
			{
				textBlockBuilder.WriteLine(Local.empty, Color.Wheat);
			}
			if ({22946}.InternalBuildings.Contains(PersonalIsleStatus.InternalBuilding.Workshop))
			{
				textBlockBuilder.WriteLine(Local.opened_b + ": " + {22946}.PickedWorkshopInfo.NeedsFactoryInPort.Value.ToStringLocal(), new Color(122, 127, 109) * 1.4f);
			}
			if ({22946}.InternalBuildings.Contains(PersonalIsleStatus.InternalBuilding.Factory))
			{
				textBlockBuilder.WriteLine(Local.producing_b + ": " + {22946}.PickedFactoryOutputRes.Name, new Color(122, 127, 109) * 1.4f);
			}
			if ({22946}.AllowSendExpetionsCurrently(Session.Account))
			{
				textBlockBuilder.WriteLine(Local.activity_expeditionSend, Color.Wheat, Fonts.Philosopher_14Bold);
			}
			tlist.Add(textBlockBuilder);
			int {22963} = 13;
			Color {22964} = new Color(122, 127, 109);
			TextBlockBuilder[] array = this.MultipageStorageHelper({22946}.StorageResources, {22963}, {22964}, textBlockBuilder).ToArray<TextBlockBuilder>();
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(4, 1);
			defaultInterpolatedStringHandler3.AppendLiteral(" / ");
			defaultInterpolatedStringHandler3.AppendFormatted<int>(array.Length);
			defaultInterpolatedStringHandler3.AppendLiteral(")");
			string text = defaultInterpolatedStringHandler3.ToStringAndClear();
			Tlist<string> tlist2 = new Tlist<string>();
			string text2 = Local.FcTemp_PersonalIsle + " (1" + text;
			tlist2.Add(text2);
			Tlist<string> tlist3 = tlist2;
			for (int i = 0; i < array.Length; i++)
			{
				Tlist<string> tlist4 = tlist3;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(2, 3);
				defaultInterpolatedStringHandler4.AppendFormatted(Local.FcTemp_PersonalIsle);
				defaultInterpolatedStringHandler4.AppendLiteral(" (");
				defaultInterpolatedStringHandler4.AppendFormatted<int>(i + 2);
				defaultInterpolatedStringHandler4.AppendFormatted(text);
				text2 = defaultInterpolatedStringHandler4.ToStringAndClear();
				tlist4.Add(text2);
			}
			for (int j = 1; j < array.Length; j++)
			{
				TextBlockBuilder textBlockBuilder4 = array[j];
				tlist.Add(textBlockBuilder4);
			}
			ToolTip toolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(tlist3.ToArray<string>(), (from {23093} in tlist
			select new ToolTipConstructorPage({23093})).ToArray<ToolTipConstructorPage>(), CommonAtlas.tt_arrowKeys_left, CommonAtlas.tt_arrowKeys_right, CommonAtlas.Texture.Tex, new Action<Tab>(this.{23004})));
			this.{23038}.AddElement(toolTip.CurrentContent.QueryElements().Array[0]);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x000C0ACC File Offset: 0x000BECCC
		private void {22947}(FactoryPlaceIsleInfo {22948}, PlayerBuildingState {22949})
		{
			if ({22949} != null)
			{
				if (this.{23021} != null)
				{
					int num = Session.Account.Buildings.AllBuildings.IndexOf({22949});
					ToolTip toolTip = {18233}.ToolTip({22948}, {22949}.Type, (int){22949}.LevelIndex, this.{23021}.Array[num], false);
					this.{23038}.AddElement(toolTip.CurrentContent.QueryElements().Array[0]);
					return;
				}
			}
			else
			{
				FactoryGameInfo singlePredef = ({22948}.Predefines.Size == 1) ? WosbCrafting.FactoriesInfo[{22948}.Predefines.Array[0]] : null;
				this.SimpleToolTipCreator(Rectangle.Empty, (singlePredef != null) ? singlePredef.Name : Local.WorldMapUi_mineH, (singlePredef != null) ? ((singlePredef.Type == FactoryType.Temp_PersonalIsle) ? Local.WorldMapUi_personalIsle : ((singlePredef.Levels[0].DisplayOutputRes.ID == 22) ? Local.desc_farm_tt : Local.desc_mine_tt)) : Local.WorldMapUi_mineTtGeneric, delegate(ToolTipState {23108})
				{
					if (singlePredef == null || singlePredef.Type != FactoryType.Temp_PersonalIsle)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
						defaultInterpolatedStringHandler.AppendFormatted(Local.cost_build);
						defaultInterpolatedStringHandler.AppendLiteral(": ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(WosbCrafting.OpenFactoryPriceGold);
						defaultInterpolatedStringHandler.AppendLiteral(" ");
						defaultInterpolatedStringHandler.AppendFormatted(Local.gold2);
						{23108}.AppendText(defaultInterpolatedStringHandler.ToStringAndClear(), Color.White, false, false);
					}
				});
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x000C0BF0 File Offset: 0x000BEDF0
		private void {22950}(Rectangle {22951}, PbsBuildingStatus {22952}, PbsStatus {22953})
		{
			this.SimpleToolTipCreator({22951}, {22952}.UiName, Local.pb_fort_tt, delegate(ToolTipState {23109})
			{
				this.{22958}({23109}, {22952}, {22953});
				if (Session.Guild != null && Session.Guild.Tag == {22953}.CapturedBy)
				{
					{23109}.AppendText(Local.WorldMapUi_51, Color.Orange, true, false);
				}
			});
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x000C0C3C File Offset: 0x000BEE3C
		private void {22954}(Rectangle {22955}, PbsBuildingStatus {22956}, PbsStatus {22957})
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
			defaultInterpolatedStringHandler.AppendLiteral("[");
			defaultInterpolatedStringHandler.AppendFormatted<int>({22956}.TowerNumber);
			defaultInterpolatedStringHandler.AppendLiteral("] ");
			defaultInterpolatedStringHandler.AppendFormatted({22956}.UiName);
			this.SimpleToolTipCreator({22955}, defaultInterpolatedStringHandler.ToStringAndClear(), Local.pb_tower_tt, delegate(ToolTipState {23110})
			{
				this.{22958}({23110}, {22956}, {22957});
			});
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x000C0CC8 File Offset: 0x000BEEC8
		private void {22958}(ToolTipState {22959}, PbsBuildingStatus {22960}, PbsStatus {22961})
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted(Local.WorldMapUi_50);
			defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round((double)({22960}.HpFactor * 100f)));
			defaultInterpolatedStringHandler.AppendLiteral("%");
			{22959}.AppendText(defaultInterpolatedStringHandler.ToStringAndClear(), Color.Orange, true, false);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x000C0D23 File Offset: 0x000BEF23
		private IEnumerable<TextBlockBuilder> MultipageStorageHelper(GSI {22962}, int {22963}, Color {22964}, TextBlockBuilder {22965} = null)
		{
			{22913}.<MultipageStorageHelper>d__169 <MultipageStorageHelper>d__ = new {22913}.<MultipageStorageHelper>d__169(-2);
			<MultipageStorageHelper>d__.<>3__res = {22962};
			<MultipageStorageHelper>d__.<>3__resLimitOnOnePage = {22963};
			<MultipageStorageHelper>d__.<>3__resHeaderColor = {22964};
			<MultipageStorageHelper>d__.<>3__appendFirst = {22965};
			return <MultipageStorageHelper>d__;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x000C0D4C File Offset: 0x000BEF4C
		private void {22966}(Vector3[] {22967})
		{
			foreach (Vector3 vector in {22967})
			{
				for (int j = 0; j < 100; j++)
				{
					float num = (float)j / 100f * 6.2831855f;
					{22913}.IconRenderer3D iconRenderer3D = new {22913}.IconRenderer3D(vector.XY() + Geometry.SubstructRotate(num, vector.Z), new Rectangle(867, 654, 56, 62), null, num + 3.1415927f + Rand.Range(-0.5f, 0.5f), 0.6f, null, 1f, null);
					iconRenderer3D.SetColor(Color.White * 0.75f);
					this.{23027}.Add(iconRenderer3D);
				}
				bool flag = Session.EventActionsPipeline.EventCategoryAmount(EActionCaterory.ClosedCenter) > 0f;
				if (Session.Account.Rang < Gameplay.HazardWatersBlockUntilRank || flag)
				{
					{22913}.IconRenderer3D iconRenderer3D2 = new {22913}.IconRenderer3D(new Vector2(300f, 300f), {22913}.c_red_circle, null, 0f, 2f, null, 1f, null)
					{
						Size = new Vector2(1f)
					};
					iconRenderer3D2.SetColor(Color.White * 0.75f);
					this.{23027}.Add(iconRenderer3D2);
					string text = flag ? "Coming soon" : Local.redcircle_text1;
					string text2 = flag ? "" : Local.redcircle_text2(Gameplay.HazardWatersBlockUntilRank);
					{22913}.IconRenderer3D iconRenderer3D3 = new {22913}.IconRenderer3D(new Vector2(-700f, 500f), {22913}.c_iconPort, null, 0f, 0.1f, null, 1f, null)
					{
						Text = text,
						TextColor = Color.White * 0.7f,
						TextScale = 1.3f
					};
					{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(new Vector2(-950f, 500f), {22913}.c_iconPort, null, 0f, 0.1f, null, 1f, null)
					{
						Text = text2,
						TextColor = Color.White * 0.7f,
						TextScale = 1.3f
					};
					this.{23027}.Add(iconRenderer3D3);
					this.{23027}.Add(iconRenderer3D4);
				}
			}
			EABehavior1 actionBehavior = Session.EventActionsPipeline.GetActionBehavior1(EActionCaterory.RandomPvpArena);
			if (actionBehavior != null)
			{
				{22913}.IconRenderer3D iconRenderer3D5 = new {22913}.IconRenderer3D(actionBehavior.Position, {22913}.c_battleHighlight, new Action(this.{23008}), 0f, 1f, null, 1f, null);
				iconRenderer3D5.Size = new Vector2(2.2f * (float)actionBehavior.ArgumentInt) / Gameplay.WorldMapSizeXY * {22913}.mapSizeInModelSpace() / ({22913}.c_IconsScaleGeneral / 0.95f);
				iconRenderer3D5.SetPosition(actionBehavior.Position, 0f, true);
				iconRenderer3D5.SetColor({22913}.c_pfColorRed * 0.6f);
				this.{23027}.Add(iconRenderer3D5);
				{22913}.IconRenderer3D iconRenderer3D6 = new {22913}.IconRenderer3D(actionBehavior.Position, {22913}.c_battleHighlight, null, 0f, 1f, null, 1f, null);
				iconRenderer3D6.Size = new Vector2(2.2f * (float)actionBehavior.Argument2Int) / Gameplay.WorldMapSizeXY * {22913}.mapSizeInModelSpace() / ({22913}.c_IconsScaleGeneral / 0.95f);
				iconRenderer3D6.SetPosition(actionBehavior.Position, 0f, true);
				iconRenderer3D6.SetColor({22913}.c_pfColorRed);
				this.{23027}.Add(iconRenderer3D6);
			}
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x000C10E0 File Offset: 0x000BF2E0
		private void {22968}()
		{
			foreach (WorldMapRegionInfo worldMapRegionInfo in Session.Game.GetRegionRecomendation())
			{
				{22913}.IconRenderer3D iconRenderer3D = new {22913}.IconRenderer3D(worldMapRegionInfo.CenterPos, new Rectangle(398, 669, 221, 207), new Action(this.{23009}), 0f, 0.9f, null, 1f, null);
				iconRenderer3D.SetColor(Color.White * 0.7f);
				this.{23027}.Add(iconRenderer3D);
			}
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000C1190 File Offset: 0x000BF390
		private void AddPorts(Tlist<IslePortInfo> {22969}, Dictionary<int, float> {22970})
		{
			using (IEnumerator<IslePortInfo> enumerator = ((IEnumerable<IslePortInfo>){22969}).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IslePortInfo port = enumerator.Current;
					GSI res = Session.Account.ResourcesInPorts.GetHolder(port.PortID);
					bool makeDisallowed = port.MakeLockedForPlayer(Session.Account);
					bool flag = makeDisallowed && Session.Account.FogOfWar.IsOpened(port.EntryPos, 4);
					Rectangle {23064} = makeDisallowed ? {22913}.c_iconPort_Locked : (port.IsArabPort ? ((res.NamesCount > 0) ? {22913}.c_iconPortArab_Res : {22913}.c_iconPortArab) : ((port.Type == PortType.PirateBay) ? ((res.NamesCount > 0) ? {22913}.c_iconPort_pirate_Res : {22913}.c_iconPort_pirate) : ((port.Type == PortType.NeutralBay) ? ((res.NamesCount > 0) ? {22913}.c_iconPort_bb_Res : {22913}.c_iconPort_bb) : ((res.NamesCount > 0) ? {22913}.c_iconPort_Res : {22913}.c_iconPort))));
					{22913}.IconRenderer3D iconRenderer3D = new {22913}.IconRenderer3D(port.EntryPos, {23064}, delegate()
					{
						{22831} {22831} = new {22831}(makeDisallowed, res, port, {22913}.liveTrading, this.{23020}, {22970});
						this.{23038} = {22831}.CreateToolTip();
					}, 0f, 0.9f, null, 1f, null)
					{
						Text = port.PortNameShort,
						TextColor = Color.Black * 0.8f,
						Tag = port,
						IsVisibe = (!makeDisallowed || flag)
					};
					Tlist<{22887}.Id> tlist = new Tlist<{22887}.Id>();
					Tlist<{22887}.Id> tlist2 = tlist;
					{22887}.Id id = {22887}.Id.Port;
					tlist2.Add(id);
					if (!res.IsEmpty)
					{
						Tlist<{22887}.Id> tlist3 = tlist;
						id = {22887}.Id.PortsWithResources;
						tlist3.Add(id);
					}
					if (Session.Account.ResourcesInPorts.GetStorageLevel(port.PortID) != 0)
					{
						Tlist<{22887}.Id> tlist4 = tlist;
						id = {22887}.Id.PortsWithStorages;
						tlist4.Add(id);
					}
					if (Session.Account.ResourcesInPorts.GetOpenedFactories(port.PortID).Any<FactoryType>())
					{
						Tlist<{22887}.Id> tlist5 = tlist;
						id = {22887}.Id.PortsWithWorkshops;
						tlist5.Add(id);
					}
					iconRenderer3D.SetVisibilityGroups(tlist.ToArray());
					if (EducationHelper.HighlightMyPorts && res.NamesCount > 0)
					{
						Tlist<{22913}.IconRenderer3D> tlist6 = this.{23027};
						{22913}.IconRenderer3D iconRenderer3D2 = new {22913}.IconRenderer3D(port.EntryPos, {22913}.c_iconPort_backForNewPlayer, null, 0f, 1.1875f, null, 1f, null)
						{
							VisibilityGroups = iconRenderer3D.VisibilityGroups
						};
						tlist6.Add(iconRenderer3D2);
					}
					this.{23027}.Add(iconRenderer3D);
					this.{23035}.Add(port.PortID, iconRenderer3D);
					string text;
					if (!Session.Game.CanEnterNearPortWithFlags(Session.Account.WorldFlag, out text, port))
					{
						iconRenderer3D.SetColor(Color.White * 0.45f);
					}
				}
			}
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00003100 File Offset: 0x00001300
		private void {22971}()
		{
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x000C14D0 File Offset: 0x000BF6D0
		private void AddTradersInSea(Tlist<TraderInSeaPlaceInfo> {22972})
		{
			using (IEnumerator<TraderInSeaPlaceInfo> enumerator = ((IEnumerable<TraderInSeaPlaceInfo>){22972}).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TraderInSeaPlaceInfo trader = enumerator.Current;
					Vector2 pos = trader.Position;
					Rectangle {23064} = (trader.Type == TraderInSeaType.SmallVilage && Debugging.DebugInfo) ? {22913}.c_targetEnemy : ((trader.Type == TraderInSeaType.Altar) ? {22913}.c_quest_unknown : {22913}.c_iconTrader);
					bool flag = false;
					QuestInfo addInfo = null;
					foreach (QuestRunningProgress questRunningProgress in ((IEnumerable<QuestRunningProgress>)Session.Account.Quests.ProgressRunningQuests))
					{
						QuestTransferOrder questTransferOrder = questRunningProgress.Info.FirstStep as QuestTransferOrder;
						if (questTransferOrder != null && questTransferOrder.TargetAsPort == null && Vector2.DistanceSquared(questTransferOrder.TargetPosition, pos) < 1f)
						{
							{23064} = {22913}.c_iconTrader_quest;
							flag = true;
							addInfo = questRunningProgress.Info;
						}
					}
					if ((trader.Type == TraderInSeaType.Altar || Gameplay.WorldMap.IsInsideMap(pos, true) || flag) && (((Vector2.Distance(pos, Global.Player.Position) < 1832.941f || trader.AlwaysVisibleInWorldMap) && Session.Account.FogOfWar.IsOpened(pos, 1)) || flag || Global.Player.DebugEnabled))
					{
						Tlist<{22913}.IconRenderer3D> tlist = this.{23027};
						{22913}.IconRenderer3D iconRenderer3D = new {22913}.IconRenderer3D(Global.Player.MapInfo.ClampToMap(pos, true), {23064}, delegate()
						{
							this.{22931}(trader, addInfo, Global.Player.MapInfo.IsInsideMap(pos, true));
						}, 0f, (trader.AlwaysVisibleInWorldMap ? 1f : 0.7f) * 1.2f, null, 1f, null).SetVisibilityGroups(new {22887}.Id[]
						{
							{22887}.Id.TraderInSea
						});
						tlist.Add(iconRenderer3D);
					}
				}
			}
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x000C1710 File Offset: 0x000BF910
		private void AddFactories(Tlist<FactoryPlaceIsleInfo> {22973})
		{
			using (IEnumerator<FactoryPlaceIsleInfo> enumerator = ((IEnumerable<FactoryPlaceIsleInfo>){22973}).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					{22913}.<>c__DisplayClass175_0 CS$<>8__locals1 = new {22913}.<>c__DisplayClass175_0();
					CS$<>8__locals1.<>4__this = this;
					CS$<>8__locals1.mine = enumerator.Current;
					bool flag = CS$<>8__locals1.mine.Predefines.Array[0] == FactoryType.Temp_PersonalIsle;
					PersonalIsleStatus havingPersonalIsle = flag ? Session.Account.PersonalIsles.Data.FirstOrDefault((PersonalIsleStatus {23111}) => (int){23111}.PlaceIndex == CS$<>8__locals1.mine.FcID) : null;
					PlayerBuildingState havingBuilding = Session.Account.Buildings.Get(CS$<>8__locals1.mine.FcID);
					if (havingPersonalIsle != null || havingBuilding != null || (!CS$<>8__locals1.mine.IsHidden && Session.Account.FogOfWar.IsOpened(CS$<>8__locals1.mine.GlobalPosition, 1)))
					{
						{22913}.IconRenderer3D iconRenderer3D = null;
						if (flag)
						{
							if (havingPersonalIsle == null && Vector2.Distance(Global.Player.Position, CS$<>8__locals1.mine.GlobalPosition) > 1832.941f && !Debugging.DebugInfo)
							{
								continue;
							}
							Rectangle {23064} = (havingPersonalIsle != null) ? {22913}.c_iconIsleGreen : {22913}.c_iconIsleUnresearched;
							iconRenderer3D = new {22913}.IconRenderer3D(CS$<>8__locals1.mine.GlobalPosition, {23064}, delegate()
							{
								CS$<>8__locals1.<>4__this.{22944}(CS$<>8__locals1.mine, havingPersonalIsle);
							}, 0f, (havingPersonalIsle != null) ? 1.15f : 0.8f, delegate()
							{
								CS$<>8__locals1.<>4__this.{22941}(CS$<>8__locals1.mine, havingPersonalIsle);
							}, 1f, null)
							{
								Text = ((havingPersonalIsle == null) ? "" : havingPersonalIsle.Name)
							};
							iconRenderer3D.SetVisibilityGroups(new {22887}.Id[]
							{
								(havingPersonalIsle == null) ? {22887}.Id.Factory : {22887}.Id.FactoryCaptured
							});
						}
						else
						{
							if (Global.Player.MapInfo.IsInsideMap(CS$<>8__locals1.mine.GlobalPosition, true))
							{
								Rectangle {23064}2 = (havingBuilding != null) ? {22913}.c_iconMineGreen : {22913}.c_iconMine;
								iconRenderer3D = new {22913}.IconRenderer3D(CS$<>8__locals1.mine.GlobalPosition, {23064}2, delegate()
								{
									CS$<>8__locals1.<>4__this.{22947}(CS$<>8__locals1.mine, havingBuilding);
								}, 0f, (havingBuilding == null) ? 0.8f : 1f, null, 1f, null);
								if (havingBuilding == null)
								{
									iconRenderer3D.VisibleOnZoom = {22913}.c_visibleTowersAtScale;
								}
							}
							else if (havingBuilding != null)
							{
								iconRenderer3D = new {22913}.IconRenderer3D(CS$<>8__locals1.mine.GlobalPosition, {22913}.c_iconMineGreen, delegate()
								{
									CS$<>8__locals1.<>4__this.SimpleToolTipCreator({22913}.c_iconIsleGreen, havingBuilding.TypeInfo.Name, Local.WorldMapUi_traderInSeaOutsideQuest, null);
								}, 0f, 0.9f, null, 1f, null);
							}
							if (iconRenderer3D != null)
							{
								iconRenderer3D.SetVisibilityGroups(new {22887}.Id[]
								{
									(havingBuilding == null) ? {22887}.Id.Factory : {22887}.Id.FactoryCaptured
								});
							}
						}
						if (iconRenderer3D != null)
						{
							this.{23027}.Add(iconRenderer3D);
							if (this.{23033}.ContainsKey(CS$<>8__locals1.mine.FcID))
							{
								this.{23033}[CS$<>8__locals1.mine.FcID] = iconRenderer3D;
							}
							else
							{
								this.{23033}.Add(CS$<>8__locals1.mine.FcID, iconRenderer3D);
							}
						}
					}
				}
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x000C1A88 File Offset: 0x000BFC88
		private void {22974}()
		{
			using (IEnumerator<QuestInfo> enumerator = Session.Account.Quests.GetOpenedQuests(null, null, false, false).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					{22913}.<>c__DisplayClass176_0 CS$<>8__locals1 = new {22913}.<>c__DisplayClass176_0();
					CS$<>8__locals1.<>4__this = this;
					CS$<>8__locals1.quest = enumerator.Current;
					{22913}.IconRenderer3D iconApi = null;
					iconApi = new {22913}.IconRenderer3D(CS$<>8__locals1.quest.LocationPos, CS$<>8__locals1.quest.Icon, delegate()
					{
						CS$<>8__locals1.<>4__this.{22938}(CS$<>8__locals1.quest.Icon, CS$<>8__locals1.quest);
					}, 0f, 0.75f, delegate()
					{
						if (InputHelper.NowInputState.IsDown(Keys.LeftAlt) || InputHelper.NowInputState.IsDown(Keys.RightAlt))
						{
							Global.Network.Send(new OnGetQuestMsg(CS$<>8__locals1.quest.ID, QuestAction.HideMarker, -1, null));
							iconApi.IsVisibe = false;
						}
					}, 1f, null).SetVisibilityGroups(new {22887}.Id[]
					{
						{22887}.Id.QuestMarker
					});
					this.{23027}.Add(iconApi);
				}
			}
			using (IEnumerator<QuestRunningProgress> enumerator2 = ((IEnumerable<QuestRunningProgress>)Session.Account.Quests.ProgressRunningQuests).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					QuestRunningProgress quest = enumerator2.Current;
					if (quest.CurrentStep is QuestIndividualLoot && quest.RuntimeParameter != null)
					{
						Tlist<{22913}.IconRenderer3D> tlist = this.{23027};
						{22913}.IconRenderer3D iconRenderer3D = new {22913}.IconRenderer3D(quest.RuntimeParameter.PositionParameter, {22913}.c_quest_trasuryMap, delegate()
						{
							this.SimpleToolTipCreator({22913}.c_quest_trasuryMap, "", quest.Info.GetName(Session.Account), null);
						}, 0f, 0.7f, null, 1f, null).SetVisibilityGroups(new {22887}.Id[]
						{
							{22887}.Id.QuestMarker
						});
						tlist.Add(iconRenderer3D);
					}
				}
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000C1C60 File Offset: 0x000BFE60
		private void {22975}()
		{
			foreach (QuestRunningProgress questRunningProgress in ((IEnumerable<QuestRunningProgress>)Session.Account.Quests.ProgressRunningQuests))
			{
				string name = questRunningProgress.Info.GetName(Session.Account);
				if (questRunningProgress.CurrentStep is QuestReturnBack)
				{
					Tlist<{22913}.IconRenderer3D> tlist = this.{23027};
					{22913}.IconRenderer3D iconRenderer3D = new {22913}.IconRenderer3D(questRunningProgress.Info.LocationPos, {22913}.c_iconTrader_quest, delegate()
					{
						this.SimpleToolTipCreator({22913}.c_iconTrader_quest, name, Local.WorldMapUi_1, delegate(ToolTipState {23095})
						{
							{23095}.AppendText(Local.click_for_marker, Color.LightGray, false, false);
						});
					}, 0f, 1f, null, 1f, null).SetVisibilityGroups(new {22887}.Id[]
					{
						{22887}.Id.QuestMarker
					});
					tlist.Add(iconRenderer3D);
				}
			}
			foreach (PlayerWorldActivityStatus {22977} in ((IEnumerable<PlayerWorldActivityStatus>)Session.Account.WorldActivities))
			{
				this.{22976}({22977});
			}
			foreach (ClientDrop clientDrop in ((IEnumerable<ClientDrop>)Global.Game.WorldInstance.DropList))
			{
				if (clientDrop.ModelType == DropModel.IsleFarming && clientDrop.DisplayResourcesOnWorldmap != null)
				{
					GSI resCopy = clientDrop.DisplayResourcesOnWorldmap.Clone();
					Tlist<{22913}.IconRenderer3D> tlist2 = this.{23027};
					{22913}.IconRenderer3D iconRenderer3D = new {22913}.IconRenderer3D(clientDrop.Position, {22913}.c_lootIsle, delegate()
					{
						this.{23038} = new ToolTipState(new Composer(350f, 1f).AddHeader(Local.IsleFarming, null).AddSeparatorWosb(Local.can_give).AddCustomInRow(3f, from {23096} in resCopy.ResourceInfo
						select new Image(36, {23096}.Info.IconTexture, {23096}.Info.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp)));
					}, 0f, 0.9f, null, 1f, null).SetVisibilityGroups(new {22887}.Id[]
					{
						{22887}.Id.WorldActivity
					});
					tlist2.Add(iconRenderer3D);
				}
				if (clientDrop.ModelType == DropModel.Fishing)
				{
					Tlist<{22913}.IconRenderer3D> tlist3 = this.{23027};
					{22913}.IconRenderer3D iconRenderer3D = new {22913}.IconRenderer3D(clientDrop.Position, {22913}.c_lootFish, new Action(this.{23010}), 0f, 0.82f, null, 1f, null).SetVisibilityGroups(new {22887}.Id[]
					{
						{22887}.Id.WorldActivity
					});
					tlist3.Add(iconRenderer3D);
				}
				if (clientDrop.ModelType == DropModel.Whale)
				{
					WhaleController.SubModel subtype = clientDrop.Whale.Subtype;
					Tlist<{22913}.IconRenderer3D> tlist4 = this.{23027};
					{22913}.IconRenderer3D iconRenderer3D = new {22913}.IconRenderer3D(clientDrop.Position, {22913}.c_lootWhale, delegate()
					{
						this.SimpleToolTipCreator({22913}.c_lootWhale, "", subtype.ToStringLocal(), null);
					}, 0f, 0.8f, null, 1f, null).SetVisibilityGroups(new {22887}.Id[]
					{
						{22887}.Id.WorldActivity
					});
					tlist4.Add(iconRenderer3D);
				}
			}
			using (IEnumerator<ShipPositionInfo> enumerator4 = ((IEnumerable<ShipPositionInfo>)Gameplay.WorldMap.WorldForts).GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					{22913}.<>c__DisplayClass177_3 CS$<>8__locals4 = new {22913}.<>c__DisplayClass177_3();
					CS$<>8__locals4.<>4__this = this;
					CS$<>8__locals4.worldFort = enumerator4.Current;
					if (Session.Account.FogOfWar.IsOpened(CS$<>8__locals4.worldFort.Position, 1))
					{
						ValueTuple<Isle, DynamicBuildCreatePacket, float> valueTuple = Global.Game.InteractiveWorldSystem.BuildingsCache.FirstOrDefault(([TupleElementNames(new string[]
						{
							"isle",
							"data",
							"secFromEvent"
						})] ValueTuple<Isle, DynamicBuildCreatePacket, float> {23112}) => {23112}.Item2.Position == CS$<>8__locals4.worldFort.Position);
						Isle item = valueTuple.Item1;
						DynamicBuildCreatePacket item2 = valueTuple.Item2;
						if (item != null)
						{
							Rectangle path = (item2.NowStrength == 0f) ? Rectangle.Empty : {22913}.c_fort_emire;
							if (path.Width > 0)
							{
								Tlist<{22913}.IconRenderer3D> tlist5 = this.{23027};
								{22913}.IconRenderer3D iconRenderer3D = new {22913}.IconRenderer3D(CS$<>8__locals4.worldFort.Position, path, delegate()
								{
									CS$<>8__locals4.<>4__this.SimpleToolTipCreator(path, (path == {22913}.c_fort_emire_destroyed) ? Local.worldEmpireFort_Destroyed : Local.worldEmpireFort, Local.worldEmpireFort_tt, null);
								}, 0f, 1f, null, 1f, null)
								{
									IsFlickering = (item2.NowStrength < item2.MaxStrength * 0.9f)
								}.SetVisibilityGroups(new {22887}.Id[]
								{
									{22887}.Id.GlobalEvent
								});
								tlist5.Add(iconRenderer3D);
							}
						}
					}
				}
			}
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x000C20B8 File Offset: 0x000C02B8
		private void {22976}(PlayerWorldActivityStatus {22977})
		{
			if ({22977}.Activity == PlayerWorldActivityStatus.Type.ExpeditionToAndFromIsle)
			{
				FactoryPlaceIsleInfo factoryPlaceIsleInfo = Gameplay.WorldMap.Factories.Array[{22977}.Parameter1];
				float num = PersonalIsleStatus.ExpeditionDurationSec(Vector2.Distance(factoryPlaceIsleInfo.GlobalPosition, {22977}.Position));
				float num2 = Math.Abs(Geometry.Saturate({22977}.TimeToFinishSec / num) - 0.5f) * 2f;
				num2 = Math.Max(0.05f, num2);
				Vector2 {23063} = Vector2.Lerp(factoryPlaceIsleInfo.GlobalPosition, {22977}.Position, num2);
				Tlist<{22913}.IconRenderer3D> tlist = this.{23027};
				{22913}.IconRenderer3D iconRenderer3D = new {22913}.IconRenderer3D({23063}, {22913}.c_iconExpedition, new Action(this.{23011}), 0f, 0.8f, null, 1f, null).SetVisibilityGroups(new {22887}.Id[]
				{
					{22887}.Id.FactoryCaptured
				});
				tlist.Add(iconRenderer3D);
			}
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x000C2180 File Offset: 0x000C0380
		private void {22978}()
		{
			using (IEnumerator<TradingRouteInfo> enumerator = ((IEnumerable<TradingRouteInfo>)this.{23052}).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TradingRouteInfo route = enumerator.Current;
					TradingRouteType type = route.Type;
					string tooltipName = type.GetTradingRouteName();
					string tooltipDesc = type.GetTradingRouteDescription();
					bool hasResources = route.ResourceInfos != null && route.ResourceInfos.Length != 0;
					string tooltipResInfo = hasResources ? Local.resource_info_available : Local.resource_info_varied;
					Tlist<{22913}.IconRenderer3D> tlist = this.{23027};
					{22913}.IconRenderer3D iconRenderer3D = new {22913}.IconRenderer3D(route.Nodes.First<Vector2>(), Rectangle.Empty, delegate()
					{
						Composer composer = new Composer(300f, 1f).AddHeader(tooltipName, null).AddText(tooltipDesc, ComposerTextStyle.Wheat, true).AddText(tooltipResInfo, ComposerTextStyle.Wheat, true);
						if (hasResources)
						{
							composer.AddSpace(5f);
							for (int i = 0; i < route.ResourceInfos.Length; i++)
							{
								ResourceInfo resourceInfo = route.ResourceInfos[i];
								Texture2D iconTexture = route.ResourceInfos[i].IconTexture;
								composer.AddImageAndText(resourceInfo.Name, iconTexture, new Rectangle(0, 0, iconTexture.Width, iconTexture.Height), 30f, ComposerTextStyle.Wheat, true);
								composer.AddSpace(5f);
							}
						}
						float num = (float)((this.{23053} == null) ? byte.MaxValue : this.{23053}[route.Id - 1]) / 255f * 100f;
						if (num < 90f)
						{
							composer.AddSpace(5f);
							composer.AddText(Local.route_under_attack_1, ComposerTextStyle.Warning, true);
							composer.AddText(Local.route_under_attack_2(MathF.Round(num)), ComposerTextStyle.Warning, true);
						}
						this.{23038} = new ToolTipState(composer);
					}, 0f, 1f, null, 1f, null)
					{
						AsPathGeometry = route.Nodes,
						VisibleOnZoom = {22913}.c_visibleTradingRoutes
					}.SetVisibilityGroups(new {22887}.Id[]
					{
						{22887}.Id.TradingRoutes
					});
					tlist.Add(iconRenderer3D);
				}
			}
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x000C22B4 File Offset: 0x000C04B4
		private void {22979}()
		{
			if (Debugging.DebugInfo)
			{
				byte maxValue = byte.MaxValue;
				using (IEnumerator<TradingRouteInfo> enumerator = ((IEnumerable<TradingRouteInfo>)Gameplay.TradingRoutesInfo).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TradingRouteInfo tradingRouteInfo = enumerator.Current;
						float num = Math.Clamp(Math.Abs(tradingRouteInfo.Nodes[0].X) / (float)maxValue * 0.1f, 0f, (float)maxValue);
						Vector2[] nodes = tradingRouteInfo.Nodes;
						float g = Math.Clamp(Math.Abs(nodes[nodes.Length - 1].Y) / (float)maxValue * 0.1f, 0f, (float)maxValue);
						Color {14638} = new Color(num, g, num);
						for (int i = 1; i < tradingRouteInfo.Nodes.Length; i++)
						{
							Vector2 {22987} = tradingRouteInfo.Nodes[i - 1];
							Vector2 {22987}2 = tradingRouteInfo.Nodes[i];
							Marker mapPath = {22913}.MapPath;
							Vector2 {14636} = this.{22989}({22913}.globalToMap({22987}, ref mapPath));
							Vector2 {14637} = this.{22989}({22913}.globalToMap({22987}2, ref mapPath));
							Engine.GS.Line2D({22913}.cLine, {14636}, {14637}, {14638}, 6);
						}
					}
					return;
				}
			}
			if ({22913}.mapScale > {22913}.c_visibleTradingRoutes && {22887}.Statuses[{22887}.Id.TradingRoutes].Value)
			{
				foreach (TradingRouteInfo tradingRouteInfo2 in ((IEnumerable<TradingRouteInfo>)this.{23052}))
				{
					if (tradingRouteInfo2.IsVisible(Session.Account))
					{
						Color color;
						switch (tradingRouteInfo2.Type)
						{
						case TradingRouteType.Main:
							color = Color.LemonChiffon;
							break;
						case TradingRouteType.Coastal:
							color = Color.PaleGoldenrod;
							break;
						case TradingRouteType.Supply:
							color = Color.LightBlue;
							break;
						case TradingRouteType.Central:
							color = Color.Gold;
							break;
						case TradingRouteType.Smuggling:
							color = Color.Silver;
							break;
						default:
							color = Color.White;
							break;
						}
						Color color2 = color;
						float mapTransparancy = tradingRouteInfo2.GetMapTransparancy(Global.Player.Position);
						color2 *= mapTransparancy;
						color2 *= 0.8f;
						for (int j = 1; j < tradingRouteInfo2.Nodes.Length; j++)
						{
							Vector2 {22987}3 = tradingRouteInfo2.Nodes[j - 1];
							Vector2 {22987}4 = tradingRouteInfo2.Nodes[j];
							Marker mapPath2 = {22913}.MapPath;
							Vector2 vector = this.{22989}({22913}.globalToMap({22987}3, ref mapPath2));
							Vector2 vector2 = this.{22989}({22913}.globalToMap({22987}4, ref mapPath2));
							if (Vector2.DistanceSquared(vector, vector2) > 100f)
							{
								int {14650} = (tradingRouteInfo2.Type == TradingRouteType.Central) ? 8 : 4;
								Engine.GS.DottedLine2D({22913}.cLine, vector, vector2, color2, {14650});
							}
						}
					}
				}
			}
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x000C25A8 File Offset: 0x000C07A8
		private void {22980}(int {22981}, Texture2D {22982})
		{
			Vector3 value;
			value.X = -{22913}.moveOffset.X / 100f;
			value.Y = 0f;
			value.Z = -{22913}.moveOffset.Y / 100f;
			this.{23025}.Transform.MiddleScale = 2f * {22913}.mapScale;
			Vector2 vector = new Vector2(3f, 0.25f);
			Global.Camera.QueryInstantiatedRender(new Action(this.{23012}), new Vector3(--0f, 41.440002f, 9.0692005f) * 0.6f + value + new Vector3(0f, 0f, vector.X), this.{23025}.Transform.Translation + value + new Vector3(0f, 0f, vector.Y));
			Global.Render.CommonShader.OperationCommonSetCameraState();
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x000C26B0 File Offset: 0x000C08B0
		public void OnGroupMembersUpdate()
		{
			foreach ({22913}.IconRenderer3D iconRenderer3D in ((IEnumerable<{22913}.IconRenderer3D>)this.{23029}))
			{
				this.{23027}.Remove(iconRenderer3D);
			}
			this.{23029}.Clear();
			foreach (AllyStateTransfer allyStateTransfer in ((IEnumerable<AllyStateTransfer>)Session.LastMinimapAndGroupUpdate.allies))
			{
				if (allyStateTransfer.IsOneMap)
				{
					string name = allyStateTransfer.FetchName(Session.group);
					if (!string.IsNullOrEmpty(name))
					{
						CompressedShipPositionInfo position = allyStateTransfer.Position;
						{22913}.IconRenderer3D iconRenderer3D2 = new {22913}.IconRenderer3D(position.Position, allyStateTransfer.IsActiveAvanpostShip ? {22913}.c_allyMarkerAvanpost : {22913}.c_allyMarker, delegate()
						{
							this.SimpleToolTipCreator({22913}.c_allyMarker, name, null, null);
						}, 0f, allyStateTransfer.IsActiveAvanpostShip ? 1f : 0.66f, null, 1f, null);
						iconRenderer3D2.SetVisibilityGroups(new {22887}.Id[]
						{
							{22887}.Id.GroupMembers
						});
						this.{23029}.Add(iconRenderer3D2);
						this.{23027}.Add(iconRenderer3D2);
					}
				}
			}
			using (IEnumerator<EnemyStateTransfer> enumerator3 = ((IEnumerable<EnemyStateTransfer>)Session.LastMinimapAndGroupUpdate.enemies).GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					EnemyStateTransfer enemy = enumerator3.Current;
					CompressedShipPositionInfo position = enemy.Position;
					{22913}.IconRenderer3D iconRenderer3D3 = new {22913}.IconRenderer3D(position.Position, {22913}.c_targetEnemy, delegate()
					{
						this.SimpleToolTipCreator({22913}.c_targetEnemy, Local.WorldMapUi_59, (enemy.ByQuestID == 0) ? "" : Gameplay.QuestsInfo.FromID(enemy.ByQuestID).GetName(Session.Account), null);
					}, 0f, 0.66f, null, 1f, null);
					iconRenderer3D3.SetVisibilityGroups(new {22887}.Id[]
					{
						{22887}.Id.QuestTargets
					});
					this.{23029}.Add(iconRenderer3D3);
					this.{23027}.Add(iconRenderer3D3);
				}
			}
			if (Session.Guild != null && this.{23022} != null)
			{
				foreach (GuildMemberPosition guildMemberPosition in ((IEnumerable<GuildMemberPosition>)this.{23022}))
				{
					GuildMember member = Session.Guild.GetMember(guildMemberPosition.AccountSID);
					if (member != null)
					{
						{22913}.IconRenderer3D iconRenderer3D4 = new {22913}.IconRenderer3D(guildMemberPosition.Position.Position, {22913}.c_allyMarker2, delegate()
						{
							this.SimpleToolTipCreator({22913}.c_allyMarker2, member.Cached.Name, "", null);
						}, 0f, 0.66f, null, 1f, null);
						iconRenderer3D4.SetVisibilityGroups(new {22887}.Id[]
						{
							{22887}.Id.GuildMembers
						});
						this.{23029}.Add(iconRenderer3D4);
						this.{23027}.Add(iconRenderer3D4);
					}
				}
			}
			if (this.{23029}.Size > 0)
			{
				this.{22985}();
			}
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x000C29D8 File Offset: 0x000C0BD8
		protected override void UserUpdate(ref FrameTime {22983})
		{
			if ((Global.Settings.kb_Map.IsClick || InputHelper.NowInputState.IsDown(Keys.Escape)) && this.{23041})
			{
				base.RemoveFromContainer();
			}
			this.{23016} += {22983}.secElapsed;
			this.{22984}();
			float worldTime = Global.Render.GetSceneManager.WorldTime;
			this.clockModel.LocalTransformOrNull.Yaw = (float)(6.283185307179586 * ((double)worldTime / 24.0 + 0.5));
			this.{23027}.Array[0].SetPosition(Global.Player.Position, Global.Player.Rotation, true);
			this.{23027}.Array[1].SetPosition(Global.Player.Position + Global.Player.Normal * 2000f, Global.Player.Rotation, true);
			{22913}.IconRenderer3D iconRenderer3D = this.{23027}.Array[0];
			Rectangle rectangle = Global.Player.IsOutsideSeam ? {22913}.c_playerMarkerGray : {22913}.c_playerMarker;
			iconRenderer3D.SetUv(rectangle);
			this.{23041} = true;
			Vector3 windDirection = CommonGlobal.CurrentClientWeather.WindDirection;
			float num = MathF.Atan2(windDirection.Z, windDirection.X);
			float num2 = 0.3f;
			Vector2 value = Global.Player.Position - windDirection.XZ * 900f;
			Vector2 value2 = Global.Player.Position + windDirection.XZ * 900f;
			Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
			for (int i = 0; i < this.{23028}.Size; i++)
			{
				if (EducationHelper.ShowWindOnGlobalMap)
				{
					float num3 = (float)((Global.Game.GameTotalTimeSec / 9.0 + (double)((float)i / (float)this.{23028}.Size)) % 1.0);
					Vector2 {23073} = Vector2.Lerp(value, value2, num3);
					float scale = Geometry.Saturate(num3 / num2) * Geometry.Saturate((1f - num3) / num2);
					this.{23028}.Array[i].SetPosition({23073}, num - 1.5707964f, false);
					this.{23028}.Array[i].SetColor(Color.White * scale);
				}
				else
				{
					this.{23028}.Array[i].SetColor(Color.Transparent);
				}
			}
			if (this.{23020} != null)
			{
				foreach (PbsStatus pbsStatus in this.{23020}.Enumerate<PbsStatus>())
				{
					pbsStatus.TimeToBeginBattle = Math.Max(0.0, pbsStatus.TimeToBeginBattle - (double){22983}.secElapsed);
					pbsStatus.TimeToBattleCompletion = Math.Max(0f, pbsStatus.TimeToBattleCompletion - {22983}.secElapsed);
					pbsStatus.TimeToResetPortByTensitySec = Math.Max(0f, pbsStatus.TimeToResetPortByTensitySec - {22983}.secElapsed);
				}
			}
			if (this.{23031} != null && InputHelper.LeftWasClicked && Vector2.Distance(Engine.GS.MouseToUIPrev, Engine.GS.MouseToUI) < 0.1f)
			{
				Action clickToolTip = this.{23031}.ClickToolTip;
				if (clickToolTip != null)
				{
					clickToolTip();
				}
			}
			foreach ({22913}.IconRenderer3D iconRenderer3D2 in ((IEnumerable<{22913}.IconRenderer3D>)this.{23027}))
			{
				if (iconRenderer3D2.VisibleOnZoom != 0f)
				{
					iconRenderer3D2.IsVisibe = ({22913}.mapScale > iconRenderer3D2.VisibleOnZoom);
				}
				if (iconRenderer3D2.RealTimeUpdate != null)
				{
					Vector2? vector = iconRenderer3D2.RealTimeUpdate();
					if (vector != null)
					{
						iconRenderer3D2.IsVisibe = true;
						iconRenderer3D2.SetPosition(vector.Value, 0f, true);
					}
					else
					{
						iconRenderer3D2.IsVisibe = false;
					}
				}
			}
			if (InputHelper.RightWasClicked)
			{
				if (Session.WorldMapMarkerPosition == null)
				{
					Session.WorldMapMarkerPosition = new Vector2?((this.{23031} != null && this.{23031}.AsPathGeometry == null) ? this.{23031}.MapPos : this.{22993}(this.{22991}(Engine.GS.MouseToUI)));
				}
				else
				{
					Session.WorldMapMarkerPosition = null;
				}
			}
			this.{23043}.X = this.{23043}.X + -CommonGlobal.CurrentClientWeather.WindXZNormal.Y * {22983}.secElapsed * 0.02f;
			this.{23043}.Y = this.{23043}.Y + CommonGlobal.CurrentClientWeather.WindXZNormal.X * {22983}.secElapsed * 0.02f;
			if (InputHelper.IsClick(Keys.C))
			{
				this.{22914}();
			}
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x000C2EC0 File Offset: 0x000C10C0
		private void {22984}()
		{
			Vector2 value = new Vector2(2048f, 1536f);
			if (!this.{23041})
			{
				return;
			}
			float num = 1f + 0.00075f * (float)(InputHelper.NowMouseState.ScrollValue - this.{23018});
			this.{23018} = InputHelper.NowMouseState.ScrollValue;
			if ({19779}.CurrentInstance == null && {18328}.CurrentInstance == null)
			{
				float num2 = MathHelper.Clamp({22913}.scrollScale * num, 1.2f, 6f);
				{22913}.moveOffset *= num2 / {22913}.scrollScale;
				if (num2 / {22913}.scrollScale > 1f)
				{
					{22913}.moveOffset -= (Engine.GS.MouseToUI / Engine.GS.UIArea.WidthHeight() - new Vector2(0.5f)) * 3f * 75f;
				}
				{22913}.scrollScale = num2;
			}
			if (InputHelper.NowMouseState.LeftPressed && base.InputMode != MouseInputMode.NoFocus && !this.distanceMeterMode)
			{
				{22913}.moveOffset += (Engine.GS.MouseToUI - Engine.GS.MouseToUIPrev) * 1.4f;
			}
			if ({22913}.mapScale != {22913}.scrollScale)
			{
				{22913}.mapScale = {22913}.scrollScale;
				this.{22985}();
			}
			{22913}.moveOffset = Vector2.Max({22913}.moveOffset, new Vector2(-617f, -473f) * Math.Min(13.690001f, {22913}.mapScale * {22913}.mapScale) * 0.4f);
			{22913}.moveOffset = Vector2.Min({22913}.moveOffset, new Vector2(662f, 452f) * Math.Min(13.690001f, {22913}.mapScale * {22913}.mapScale) * 0.4f);
			this.{23017} = Engine.GS.UIArea.HalfWidthHeightInt() - value * {22913}.mapScale + {22913}.moveOffset;
			this.{23017}.X = (float)((int)this.{23017}.X);
			this.{23017}.Y = (float)((int)this.{23017}.Y);
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x000C3108 File Offset: 0x000C1308
		private void {22985}()
		{
			foreach ({22913}.IconRenderer3D iconRenderer3D in ((IEnumerable<{22913}.IconRenderer3D>)this.{23027}))
			{
				if (iconRenderer3D.Size.X < 0.5f || iconRenderer3D.VisibilityGroups.Contains({22887}.Id.GroupMembers))
				{
					iconRenderer3D.UpdateGeometry(1f - Math.Max(0f, 0.1f * ({22913}.mapScale - 1.2f) - 0.1f));
				}
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000C319C File Offset: 0x000C139C
		private static Vector2 mapSizeInModelSpace()
		{
			return new Vector2(Vector2.Distance({22913}.BilboardCenter(new Vector2(0f, 0f)), {22913}.BilboardCenter(new Vector2(14146.483f, 0f))), Vector2.Distance({22913}.BilboardCenter(new Vector2(0f, 0f)), {22913}.BilboardCenter(new Vector2(0f, 17290.12f))));
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x000C3208 File Offset: 0x000C1408
		private static Vector2 BilboardCenter(Vector2 {22986})
		{
			Marker mapPath = {22913}.MapPath;
			return new Vector2(-3.7391305f, -2.826087f) + {22913}.globalToMap({22986}, ref mapPath) / new Vector2(4096f, 3072f) * new Vector2(15.9f, 11f) * 0.8f;
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x000C326C File Offset: 0x000C146C
		private static Vector2 globalToMap(Vector2 {22987}, ref Marker {22988})
		{
			float num = ({22987}.Y + 8645.06f) / 17290.12f;
			float num2 = 1f - ({22987}.X + 7073.2417f) / 14146.483f;
			Vector2 result;
			result.X = {22988}.XY.X + num * {22988}.WH.X;
			result.Y = {22988}.XY.Y + num2 * {22988}.WH.Y;
			return result;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x000C32E8 File Offset: 0x000C14E8
		private Vector2 {22989}(Vector2 {22990})
		{
			float num = {22990}.X / (float)OtherTextures.WorldMapSize.Width;
			float num2 = {22990}.Y / (float)OtherTextures.WorldMapSize.Height;
			num = -3.744f * (1f - num * 2f);
			num2 = -2.828f * (1f - num2 * 2f);
			Vector3 {14977} = this.{23025}.Transform.Transform3X3(new Vector3(num, -0.615f, num2));
			return Engine.GS.Camera.GetProjection({14977}, this.{23040});
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x000C3378 File Offset: 0x000C1578
		private Vector2 {22991}(Vector2 {22992})
		{
			float num = 0.6f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = (float)OtherTextures.WorldMapSize.Width * 1f;
			float num5 = (float)OtherTextures.WorldMapSize.Height * 1f;
			for (int i = 0; i < 24; i++)
			{
				Vector2 value = this.{22989}(new Vector2(num2 + num4 / 2f, num3));
				Vector2 value2 = this.{22989}(new Vector2(num2 + num4 / 2f, num3 + num5));
				Vector2 value3 = this.{22989}(new Vector2(num2 + num4, num3 + num5 / 2f));
				Vector2 value4 = this.{22989}(new Vector2(num2, num3 + num5 / 2f));
				float num6 = Vector2.DistanceSquared({22992}, value);
				float num7 = Vector2.DistanceSquared({22992}, value2);
				float num8 = Vector2.DistanceSquared({22992}, value3);
				float num9 = Vector2.DistanceSquared({22992}, value4);
				if (num6 < num7)
				{
					num5 *= num;
				}
				else
				{
					num3 += num5 * (1f - num);
				}
				if (num9 < num8)
				{
					num4 *= num;
				}
				else
				{
					num2 += num4 * (1f - num);
				}
			}
			return new Vector2(num2 + num4 / 2f, num3 + num5 / 2f);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x000C34A8 File Offset: 0x000C16A8
		private Vector2 {22993}(Vector2 {22994})
		{
			Vector2 vector = {22994} / OtherTextures.WorldMapSize.WidthHeight();
			float divider = {22913}.MapPath.WH.X / (float)OtherTextures.WorldMapSize.Width;
			return (new Vector2(1f - vector.Y, vector.X) * Gameplay.WorldMapSizeXY - Gameplay.WorldMapSizeXY / 2f) / divider;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x000C351D File Offset: 0x000C171D
		public static {22913} TryToOpen()
		{
			if ({22913}.CurrentInstance == null && Global.Player.MapInfo.IsWorldmap)
			{
				return new {22913}();
			}
			return null;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x000C353E File Offset: 0x000C173E
		public static Rectangle TowerTexPath(float {22995})
		{
			if ({22995} == 0f)
			{
				return {22913}.c_towerIconDestroyed;
			}
			if ({22995} > 0.66f)
			{
				return {22913}.c_towerIconDefault;
			}
			return {22913}.c_towerIconHalfDestroyed;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x000AC3EC File Offset: 0x000AA5EC
		[CompilerGenerated]
		private void {22996}(ClickUiEventArgs {22997})
		{
			base.RemoveFromContainer();
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000C3DAC File Offset: 0x000C1FAC
		[CompilerGenerated]
		private void {22998}(Point {22999}, byte {23000})
		{
			if ({23000} < 50)
			{
				return;
			}
			Vector2 vector = new Vector2(-7073.2417f, -8645.06f) + new Vector2((float){22999}.X + 0.5f, (float){22999}.Y + 0.5f) / 22f * Gameplay.WorldMapSizeXY;
			if (!Session.Account.FogOfWar.IsOpened(vector, 2))
			{
				return;
			}
			{23000} = (byte)Math.Min(255, (int)({23000} + 2));
			{22913}.IconRenderer3D iconRenderer3D = new {22913}.IconRenderer3D(Vector2.Zero, {22913}.c_hazardSector, null, Rand.Angle(), 1f, null, 1f, null)
			{
				Size = {22913}.mapSizeInModelSpace() / 22f * Rand.Range(0.9f, 1.4f) * 1.5f
			};
			iconRenderer3D.SetPosition(vector, 0f, true);
			iconRenderer3D.Geometry.SetCol(new Color(255f, 1f - (float){23000} / 255f, 0f, 255f) * (Math.Min(192f, (float){23000}) / 255f * 0.21f));
			iconRenderer3D.SetVisibilityGroups(new {22887}.Id[]
			{
				{22887}.Id.WaterHazardLevel
			});
			this.{23027}.Add(iconRenderer3D);
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000C3EF6 File Offset: 0x000C20F6
		[CompilerGenerated]
		internal static void <EnterPBToolTip>g__condition|159_1(string {23001}, bool {23002}, ref {22913}.<>c__DisplayClass159_1 {23003})
		{
			if ({23002})
			{
				{23003}.x.AppendText("✔ " + {23001}, Color.Green, false, false);
				return;
			}
			{23003}.x.AppendText("x  " + {23001}, Color.Orange, false, false);
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x000C3F36 File Offset: 0x000C2136
		[CompilerGenerated]
		private void {23004}(Tab {23005})
		{
			{23005}.Select(Math.Min({23005}.GetPagesCount - 1, this.{23023}));
			{23005}.EvChangeForm += this.{23006};
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000C3F63 File Offset: 0x000C2163
		[CompilerGenerated]
		private void {23006}(ChangeTabFormEventArgs {23007})
		{
			this.{23023} = {23007}.FirstItemIndex;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x000C3F71 File Offset: 0x000C2171
		[CompilerGenerated]
		private void {23008}()
		{
			this.SimpleToolTipCreator(Rectangle.Empty, Local.action_randomPvpArena_name, Local.action_randomPvpArena_desc(500), null);
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x000C3F93 File Offset: 0x000C2193
		[CompilerGenerated]
		private void {23009}()
		{
			this.SimpleToolTipCreator(Rectangle.Empty, Local.hidden_port_text, Local.target_region_recomendation, null);
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x000C3FAB File Offset: 0x000C21AB
		[CompilerGenerated]
		private void {23010}()
		{
			this.SimpleToolTipCreator({22913}.c_lootFish, "", Local.gamewiki_earning_text6, null);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x000C3FC3 File Offset: 0x000C21C3
		[CompilerGenerated]
		private void {23011}()
		{
			this.SimpleToolTipCreator({22913}.c_iconExpedition, Local.activity_expedition1, Local.activity_expedition2, null);
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x000C3FDC File Offset: 0x000C21DC
		[CompilerGenerated]
		private void {23012}()
		{
			this.{23040} = Global.Camera.ViewMultiplyProjection;
			Geometry.Evalute(ref this.{23042}, (HashHelper.NoiseCurve((float)Global.Game.GameTotalTimeSec * 4f) * 0.5f + 0.5f) * 6f, 0.064f);
			PointLight pointLight = new PointLight(this.{23025}.Transform.Transform3X3(new Vector3(-4.666f, 1.623f, -1.406f)), Color.Orange.ToVector3(), 1.3f * (this.{23042} * 0.4f + 0.6f) * 1.1f, 15f);
			Vector2 vector = this.{22991}(Engine.GS.MouseToUI) / OtherTextures.WorldMapSize.WidthHeight() - new Vector2(0.5f);
			float y = vector.Y;
			vector.Y = vector.X;
			vector.X = -y;
			vector *= Gameplay.WorldMapSizeXY;
			Vector2 vector2 = {22913}.BilboardCenter(vector);
			PointLight pointLight2 = new PointLight(this.{23025}.Transform.Transform3X3(new Vector3(vector2.X, 5f / {22913}.mapScale * 0.3f, vector2.Y)), Color.LightYellow.ToVector3(), 2f + {22913}.mapScale * 3f, 0.5f);
			Vector2 vector3 = {22913}.BilboardCenter(Global.Player.Position);
			PointLight pointLight3 = new PointLight(this.{23025}.Transform.Transform3X3(new Vector3(vector3.X, 0f, vector3.Y)), Color.LightGreen.ToVector3(), 2f + {22913}.mapScale * 3f, 0.2f);
			Global.Render.CommonShader.OperationCommonSetCameraState();
			Global.Render.CommonShader.SetCustomLightData(Vector3.One * 0.3f, Vector3.Zero, Vector3.Zero, -new Vector3(0.1f, 0.9f, 0.5f).Normal(), pointLight);
			Global.Render.Pointlights.Add(pointLight2);
			Global.Render.Pointlights.Add(pointLight3);
			Engine.GS.SetDepthBuffer(DepthBuffer.ReadAndWrite);
			this.{23025}.SetModelData(0, LocalContent.Loaded.WorldMap[0]);
			Global.Render.CommonShader.ObjectCommonTransparancy.SetValue(base.Opacity);
			Global.Render.CommonShader.RenderIsle(this.{23025}, false, -1);
			Global.Render.CommonShader.ObjectCommonTransparancy.SetValue(1);
			Global.Render.CommonShader.ResetCustomLightData(pointLight);
			Global.Render.CommonShader.SetCustomLightData(Vector3.One * 1.1f * 0.95f * 0.75f, Vector3.Zero, Vector3.Zero, -new Vector3(0.1f, 0.9f, 0.5f).Normal(), pointLight);
			this.{23025}.SetModelData(0, LocalContent.Loaded.WorldMap[1]);
			Engine.GS.SetDepthBuffer(DepthBuffer.ReadOnly);
			Global.Render.CommonShader.ObjectCommonTransparancy.SetValue(base.Opacity);
			this.{23025}.GetModels[1].LocalVisible = false;
			this.{23025}.GetModels[2].LocalVisible = false;
			Global.Render.CommonShader.RenderWorldMap(this.{23025}, OtherTextures.WorldMap.Tex, OtherTextures.WorldMapClosed.Tex, Global.Render.txFogOfWar, 1.08f);
			this.{23025}.GetModels[1].LocalVisible = true;
			this.{23025}.GetModels[2].LocalVisible = true;
			Global.Render.CommonShader.ObjectCommonTransparancy.SetValue(1);
			Global.Render.CommonShader.ResetCustomLightData(pointLight);
			Global.Render.Pointlights.Remove(pointLight2);
			Global.Render.Pointlights.Remove(pointLight3);
			Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
			Matrix matrix = this.{23025}.Transform.CreateWorldMatrix();
			float fogNearDistance = Global.Render.ItemsShader.fogNearDistance;
			float fogFarDistance = Global.Render.ItemsShader.fogFarDistance;
			Global.Render.ItemsShader.SetForRender(this.{23025}.Transform.CreateWorldMatrix(), Vector4.One * base.Opacity);
			Global.Render.ItemsShader.ManualSetFog(10000f, 10000f);
			Global.Render.ItemsShader.Texture.SetValue(OtherTextures.WorldMapUiElements);
			Global.Render.ItemsShader.MainLightDirection.SetValue(Vector3.Down);
			Global.Render.ItemsShader.MainLightColor.SetValue(Vector3.One);
			Global.Render.ItemsShader.BeginPass(true, false);
			if (this.{23037} != null)
			{
				this.{23013}(this.{23037}, Vector4.One);
			}
			if (Global.Player.MapInfo.IsWorldmap)
			{
				Vector2 position = Global.Player.Position;
				this.{23013}(Gameplay.GetNearWorldRegion(position), Vector4.One * 0.5f);
			}
			foreach (WorldMapRegionInfo worldMapRegionInfo in Session.Game.GetRegionRecomendation())
			{
				WorldMapRegionInfo worldMapRegionInfo2 = worldMapRegionInfo;
				Vector2 position = Global.Player.Position;
				if (worldMapRegionInfo2 != Gameplay.GetNearWorldRegion(position))
				{
					this.{23013}(worldMapRegionInfo, (Color.Lime * 0.25f).ToVector4());
				}
			}
			Engine.GS.Begin2D(true);
			Engine.GS.SetTexture(OtherTextures.WorldMapUiElements);
			this.{22979}();
			Engine.GS.End2D();
			Global.Render.ItemsShader.Texture.SetValue(OtherTextures.WorldMapUiElements);
			Global.Render.ItemsShader.MainLightDirection.SetValue(Vector3.Down);
			Global.Render.ItemsShader.MainLightColor.SetValue(Vector3.One);
			Global.Render.ItemsShader.BeginPass(true, false);
			float num = 0f;
			foreach ({22913}.IconRenderer3D iconRenderer3D in ((IEnumerable<{22913}.IconRenderer3D>)this.{23026}))
			{
				iconRenderer3D.AnimationSetLerps((float)(((double)num + Global.Game.GameTotalTimeSec * 0.699999988079071) % 1.0));
				iconRenderer3D.Render(ref matrix, this.{23036});
				num += 0.5f;
			}
			for (int i = 0; i < this.{23027}.Size; i++)
			{
				{22913}.IconRenderer3D iconRenderer3D2 = this.{23027}.Array[(i >= this.{23027}.Size - 1) ? 0 : (i + 1)];
				if (iconRenderer3D2.IsVisibe && iconRenderer3D2.IsVisibleByGroups())
				{
					if (iconRenderer3D2.CustomTexture != null)
					{
						Global.Render.ItemsShader.Texture.SetValue(iconRenderer3D2.CustomTexture);
						Global.Render.ItemsShader.BeginPass(true, false);
						iconRenderer3D2.Render(ref matrix, this.{23036});
						Global.Render.ItemsShader.Texture.SetValue(OtherTextures.WorldMapUiElements);
						Global.Render.ItemsShader.BeginPass(true, false);
					}
					else
					{
						iconRenderer3D2.Render(ref matrix, this.{23036});
					}
				}
			}
			Global.Render.ItemsShader.ManualSetFog(fogNearDistance, fogFarDistance);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x000C47C4 File Offset: 0x000C29C4
		[CompilerGenerated]
		private void {23013}(WorldMapRegionInfo {23014}, Vector4 {23015})
		{
			Global.Render.WorldMapRegionEffect.Parameters[0].SetValue({23014}.TextureMask.ToVector3());
			Global.Render.WorldMapRegionEffect.Parameters[1].SetValue(Gameplay.WorldMap.RegionsTex);
			Global.Render.WorldMapRegionEffect.Parameters[2].SetValue(Gameplay.WorldMap.RegionsTexBlurred);
			Global.Render.WorldMapRegionEffect.Parameters[3].SetValue({23015});
			Global.Render.WorldMapRegionEffect.CurrentTechnique.Passes[0].Apply();
			this.{23047}.Render();
		}

		// Token: 0x0400143D RID: 5181
		private const float c_minScale = 1.2f;

		// Token: 0x0400143E RID: 5182
		private const float c_maxScale = 5f;

		// Token: 0x0400143F RID: 5183
		private const float c_initialScale = 2f;

		// Token: 0x04001440 RID: 5184
		private const float c_visiblePortNamesAtScale = 1.5f;

		// Token: 0x04001441 RID: 5185
		public static readonly Marker MapPath = new Marker(88f, 80f, 2224f, 1820f);

		// Token: 0x04001442 RID: 5186
		private static float mapScale = 1f;

		// Token: 0x04001443 RID: 5187
		private static float scrollScale;

		// Token: 0x04001444 RID: 5188
		private static Vector2 moveOffset;

		// Token: 0x04001445 RID: 5189
		public static {22913} CurrentInstance;

		// Token: 0x04001446 RID: 5190
		public static readonly Rectangle c_quest_trade = new Rectangle(241, 379, 98, 92);

		// Token: 0x04001447 RID: 5191
		public static readonly Rectangle c_quest_tradeSmug = new Rectangle(341, 379, 98, 92);

		// Token: 0x04001448 RID: 5192
		public static readonly Rectangle c_quest_tradeGold = new Rectangle(1046, 775, 98, 92);

		// Token: 0x04001449 RID: 5193
		public static readonly Rectangle c_quest_tradePassanger = new Rectangle(982, 621, 92, 92);

		// Token: 0x0400144A RID: 5194
		public static readonly Rectangle c_quest_battle = new Rectangle(483, 377, 98, 92);

		// Token: 0x0400144B RID: 5195
		public static readonly Rectangle c_quest_battleGold = new Rectangle(978, 526, 98, 92);

		// Token: 0x0400144C RID: 5196
		public static readonly Rectangle c_quest_unknown = new Rectangle(583, 377, 98, 92);

		// Token: 0x0400144D RID: 5197
		public static readonly Rectangle c_quest_unknown_mapColor = new Rectangle(70, 197, 98, 92);

		// Token: 0x0400144E RID: 5198
		public static readonly Rectangle c_quest_yellowFlag = new Rectangle(978, 435, 98, 92);

		// Token: 0x0400144F RID: 5199
		public static readonly Rectangle c_quest_grayFlag = new Rectangle(972, 0, 98, 92);

		// Token: 0x04001450 RID: 5200
		public static readonly Rectangle c_event_redSkull = new Rectangle(560, 472, 98, 92);

		// Token: 0x04001451 RID: 5201
		public static readonly Rectangle c_event_yellowEmpire = new Rectangle(399, 877, 98, 92);

		// Token: 0x04001452 RID: 5202
		public static readonly Rectangle c_event_yellowSkull = new Rectangle(298, 667, 98, 92);

		// Token: 0x04001453 RID: 5203
		public static readonly Rectangle c_event_yellowCaravan = new Rectangle(498, 877, 98, 92);

		// Token: 0x04001454 RID: 5204
		public static readonly Rectangle c_quest_redChest = new Rectangle(972, 93, 98, 92);

		// Token: 0x04001455 RID: 5205
		public static readonly Rectangle c_enterpb_mark = new Rectangle(1080, 574, 98, 92);

		// Token: 0x04001456 RID: 5206
		public static readonly Rectangle c_enterpb_mark_dim = new Rectangle(1406, 438, 98, 92);

		// Token: 0x04001457 RID: 5207
		public static readonly Rectangle c_quest_trasuryMap = new Rectangle(623, 747, 98, 98);

		// Token: 0x04001458 RID: 5208
		public static readonly Rectangle c_advert = new Rectangle(1081, 674, 98, 98);

		// Token: 0x04001459 RID: 5209
		public static readonly Rectangle c_advertGray = new Rectangle(1180, 674, 98, 98);

		// Token: 0x0400145A RID: 5210
		private static readonly Color c_pfColorGreen = new Color(186, 255, 102);

		// Token: 0x0400145B RID: 5211
		private static readonly Color c_pfColorRed = new Color(255, 120, 99);

		// Token: 0x0400145C RID: 5212
		public static Rectangle cWhite = new Rectangle(205, 81, 1, 1);

		// Token: 0x0400145D RID: 5213
		public static Rectangle cLine = new Rectangle(218, 79, 5, 5);

		// Token: 0x0400145E RID: 5214
		public static Rectangle c_formBack_black = new Rectangle(305, 140, 204, 123);

		// Token: 0x0400145F RID: 5215
		public static Rectangle c_formBack_violet = new Rectangle(511, 155, 204, 75);

		// Token: 0x04001460 RID: 5216
		public static Rectangle c_formBack_blue = new Rectangle(511, 231, 204, 75);

		// Token: 0x04001461 RID: 5217
		public static Rectangle c_formBack_black_down = new Rectangle(470, 307, 311, 69);

		// Token: 0x04001462 RID: 5218
		public static Rectangle c_formBack_advert_active = new Rectangle(726, 814, 311, 69);

		// Token: 0x04001463 RID: 5219
		public static Rectangle c_formBack_advert = new Rectangle(726, 744, 311, 69);

		// Token: 0x04001464 RID: 5220
		public static Rectangle c_checkbox_en = new Rectangle(330, 95, 26, 26);

		// Token: 0x04001465 RID: 5221
		public static Rectangle c_checkbox_dis = new Rectangle(304, 95, 26, 26);

		// Token: 0x04001466 RID: 5222
		public static Rectangle c_iconPort = new Rectangle(0, 376, 116, 116);

		// Token: 0x04001467 RID: 5223
		public static Rectangle c_iconPort_Locked = new Rectangle(568, 0, 116, 116);

		// Token: 0x04001468 RID: 5224
		public static Rectangle c_iconPort_Res = new Rectangle(116, 376, 116, 116);

		// Token: 0x04001469 RID: 5225
		public static Rectangle c_iconPort_special = new Rectangle(746, 437, 113, 116);

		// Token: 0x0400146A RID: 5226
		public static Rectangle c_iconPort_special_Res = new Rectangle(860, 437, 113, 116);

		// Token: 0x0400146B RID: 5227
		public static Rectangle c_iconPort_pirate = new Rectangle(742, 0, 113, 116);

		// Token: 0x0400146C RID: 5228
		public static Rectangle c_iconPort_pirate_Res = new Rectangle(856, 0, 113, 116);

		// Token: 0x0400146D RID: 5229
		public static Rectangle c_iconPort_bb = new Rectangle(1525, 545, 113, 116);

		// Token: 0x0400146E RID: 5230
		public static Rectangle c_iconPort_bb_Res = new Rectangle(1639, 545, 113, 116);

		// Token: 0x0400146F RID: 5231
		public static Rectangle c_iconPort_backForTopPort = new Rectangle(743, 118, 125, 125);

		// Token: 0x04001470 RID: 5232
		public static Rectangle c_iconPort_backForNewPlayer = new Rectangle(0, 292, 83, 83);

		// Token: 0x04001471 RID: 5233
		public static Rectangle c_iconPortArab = new Rectangle(620, 625, 116, 116);

		// Token: 0x04001472 RID: 5234
		public static Rectangle c_iconPortArab_Res = new Rectangle(736, 625, 116, 116);

		// Token: 0x04001473 RID: 5235
		public static Rectangle c_fort_my = new Rectangle(418, 568, 48, 48);

		// Token: 0x04001474 RID: 5236
		public static Rectangle c_fort_neutral = new Rectangle(301, 568, 48, 48);

		// Token: 0x04001475 RID: 5237
		public static Rectangle c_fort_enemy = new Rectangle(184, 568, 48, 48);

		// Token: 0x04001476 RID: 5238
		public static Rectangle c_fort_emire = new Rectangle(401, 267, 48, 48);

		// Token: 0x04001477 RID: 5239
		public static Rectangle c_fort_emire_destroyed = new Rectangle(401, 316, 48, 48);

		// Token: 0x04001478 RID: 5240
		public static Rectangle c_iconLighhouse = new Rectangle(795, 311, 48, 48);

		// Token: 0x04001479 RID: 5241
		public static Rectangle c_closeButton = new Rectangle(0, 94, 75, 75);

		// Token: 0x0400147A RID: 5242
		public static Rectangle c_iconMine = new Rectangle(104, 104, 98, 92);

		// Token: 0x0400147B RID: 5243
		public static Rectangle c_iconMineGreen = new Rectangle(204, 104, 98, 92);

		// Token: 0x0400147C RID: 5244
		public static Rectangle c_iconMineRed = new Rectangle(255, 0, 98, 92);

		// Token: 0x0400147D RID: 5245
		public static Rectangle c_iconMineGold = new Rectangle(860, 554, 98, 92);

		// Token: 0x0400147E RID: 5246
		public static Rectangle c_iconExpedition = new Rectangle(872, 118, 98, 92);

		// Token: 0x0400147F RID: 5247
		public static Rectangle c_iconIsleGreen = new Rectangle(976, 188, 92, 92);

		// Token: 0x04001480 RID: 5248
		public static Rectangle c_iconIsleUnresearched = new Rectangle(298, 267, 92, 92);

		// Token: 0x04001481 RID: 5249
		public static Rectangle c_iconIsleResearch = new Rectangle(169, 197, 98, 92);

		// Token: 0x04001482 RID: 5250
		public static Rectangle c_iconIsleResearch_completed = new Rectangle(1278, 574, 98, 92);

		// Token: 0x04001483 RID: 5251
		public static Rectangle c_iconTrader = new Rectangle(0, 0, 98, 92);

		// Token: 0x04001484 RID: 5252
		public static Rectangle c_iconTrader_quest = new Rectangle(946, 340, 98, 92);

		// Token: 0x04001485 RID: 5253
		public static Rectangle c_iconMarker = new Rectangle(1406, 339, 98, 98);

		// Token: 0x04001486 RID: 5254
		public static Rectangle c_lootFish = new Rectangle(99, 667, 98, 92);

		// Token: 0x04001487 RID: 5255
		public static Rectangle c_lootWhale = new Rectangle(198, 667, 98, 92);

		// Token: 0x04001488 RID: 5256
		public static Rectangle c_lootIsle = new Rectangle(0, 667, 98, 92);

		// Token: 0x04001489 RID: 5257
		public static Rectangle c_playerMarker = new Rectangle(363, 42, 70, 86);

		// Token: 0x0400148A RID: 5258
		public static Rectangle c_playerMarkerGray = new Rectangle(0, 170, 70, 86);

		// Token: 0x0400148B RID: 5259
		public static Rectangle c_allyMarker = new Rectangle(675, 477, 64, 64);

		// Token: 0x0400148C RID: 5260
		public static Rectangle c_allyMarker2 = new Rectangle(740, 554, 64, 64);

		// Token: 0x0400148D RID: 5261
		public static Rectangle c_allyMarkerAvanpost = new Rectangle(854, 329, 46, 46);

		// Token: 0x0400148E RID: 5262
		public static Rectangle c_targetEnemy = new Rectangle(675, 542, 64, 64);

		// Token: 0x0400148F RID: 5263
		public static Rectangle c_playerLine = new Rectangle(697, 0, 44, 150);

		// Token: 0x04001490 RID: 5264
		public static Rectangle c_iconHighlight = new Rectangle(433, 0, 134, 134);

		// Token: 0x04001491 RID: 5265
		public static Rectangle c_towerIconDefault = new Rectangle(469, 564, 52, 52);

		// Token: 0x04001492 RID: 5266
		public static Rectangle c_towerIconHalfDestroyed = new Rectangle(575, 564, 52, 52);

		// Token: 0x04001493 RID: 5267
		public static Rectangle c_towerIconDestroyed = new Rectangle(805, 564, 52, 52);

		// Token: 0x04001494 RID: 5268
		public static Rectangle c_towerCaptured = new Rectangle(686, 424, 52, 52);

		// Token: 0x04001495 RID: 5269
		public static Rectangle c_battleHighlight = new Rectangle(99, 0, 78, 78);

		// Token: 0x04001496 RID: 5270
		public static Rectangle c_hazardSector = new Rectangle(1208, 339, 197, 197);

		// Token: 0x04001497 RID: 5271
		public static Rectangle c_smallToolTip = new Rectangle(778, 378, 167, 54);

		// Token: 0x04001498 RID: 5272
		public static Rectangle c_smallToolTip_s = new Rectangle(169, 290, 119, 54);

		// Token: 0x04001499 RID: 5273
		public static Rectangle c_pfArea = new Rectangle(1418, 0, 336, 336);

		// Token: 0x0400149A RID: 5274
		public static Rectangle c_weatherArea = new Rectangle(1079, 0, 338, 338);

		// Token: 0x0400149B RID: 5275
		public static Rectangle c_weatherAreaFog = new Rectangle(1376, 574, 148, 147);

		// Token: 0x0400149C RID: 5276
		public static Rectangle c_wind = new Rectangle(1538, 339, 204, 204);

		// Token: 0x0400149D RID: 5277
		public static Rectangle c_red_circle = new Rectangle(4, 804, 400, 350);

		// Token: 0x0400149E RID: 5278
		private static readonly Color featureColor = Color.Wheat * 0.7f;

		// Token: 0x0400149F RID: 5279
		public static Tlist<PortLiveTrading> liveTrading;

		// Token: 0x040014A0 RID: 5280
		private float {23016};

		// Token: 0x040014A1 RID: 5281
		private Vector2 {23017};

		// Token: 0x040014A2 RID: 5282
		private int {23018};

		// Token: 0x040014A3 RID: 5283
		private WaterHazardLevelsInfo {23019};

		// Token: 0x040014A4 RID: 5284
		private PortCaputreStatusList {23020};

		// Token: 0x040014A5 RID: 5285
		private Tlist<GSI> {23021};

		// Token: 0x040014A6 RID: 5286
		private Tlist<GuildMemberPosition> {23022};

		// Token: 0x040014A7 RID: 5287
		private int {23023};

		// Token: 0x040014A8 RID: 5288
		private Tlist<PBInvite> {23024};

		// Token: 0x040014A9 RID: 5289
		private ModelTransformedScene {23025};

		// Token: 0x040014AA RID: 5290
		private Tlist<{22913}.IconRenderer3D> {23026} = new Tlist<{22913}.IconRenderer3D>();

		// Token: 0x040014AB RID: 5291
		private Tlist<{22913}.IconRenderer3D> {23027} = new Tlist<{22913}.IconRenderer3D>();

		// Token: 0x040014AC RID: 5292
		private Tlist<{22913}.IconRenderer3D> {23028} = new Tlist<{22913}.IconRenderer3D>();

		// Token: 0x040014AD RID: 5293
		private Tlist<{22913}.IconRenderer3D> {23029} = new Tlist<{22913}.IconRenderer3D>();

		// Token: 0x040014AE RID: 5294
		private Tlist<{22913}.IconRenderer3D> {23030} = new Tlist<{22913}.IconRenderer3D>();

		// Token: 0x040014AF RID: 5295
		private {22913}.IconRenderer3D {23031};

		// Token: 0x040014B0 RID: 5296
		private {22913}.IconRenderer3D {23032};

		// Token: 0x040014B1 RID: 5297
		private Dictionary<int, {22913}.IconRenderer3D> {23033} = new Dictionary<int, {22913}.IconRenderer3D>(128);

		// Token: 0x040014B2 RID: 5298
		private Tlist<ValueTuple<{22913}.IconRenderer3D, WeatherArea>> {23034} = new Tlist<ValueTuple<{22913}.IconRenderer3D, WeatherArea>>();

		// Token: 0x040014B3 RID: 5299
		private Dictionary<int, {22913}.IconRenderer3D> {23035};

		// Token: 0x040014B4 RID: 5300
		private {22913}.IconRenderer3D {23036};

		// Token: 0x040014B5 RID: 5301
		private WorldMapRegionInfo {23037};

		// Token: 0x040014B6 RID: 5302
		private ToolTipState {23038};

		// Token: 0x040014B7 RID: 5303
		private Form {23039};

		// Token: 0x040014B8 RID: 5304
		private Matrix {23040};

		// Token: 0x040014B9 RID: 5305
		private bool {23041};

		// Token: 0x040014BA RID: 5306
		private float {23042} = 1f;

		// Token: 0x040014BB RID: 5307
		private Vector2 {23043};

		// Token: 0x040014BC RID: 5308
		private Vector2? {23044};

		// Token: 0x040014BD RID: 5309
		private {22887} {23045};

		// Token: 0x040014BE RID: 5310
		private {22879} {23046};

		// Token: 0x040014BF RID: 5311
		private BillboardParent_VPCT {23047};

		// Token: 0x040014C0 RID: 5312
		private Timer {23048};

		// Token: 0x040014C1 RID: 5313
		private bool {23049};

		// Token: 0x040014C2 RID: 5314
		private Tlist<Vector2> {23050} = new Tlist<Vector2>();

		// Token: 0x040014C3 RID: 5315
		private Dictionary<int, float> {23051};

		// Token: 0x040014C4 RID: 5316
		private Tlist<TradingRouteInfo> {23052};

		// Token: 0x040014C5 RID: 5317
		private byte[] {23053};

		// Token: 0x040014C6 RID: 5318
		private byte[] {23054};

		// Token: 0x040014C7 RID: 5319
		public readonly ModelRenderer clockModel;

		// Token: 0x0200040E RID: 1038
		private class IconRenderer3D
		{
			// Token: 0x060016C3 RID: 5827 RVA: 0x000C4888 File Offset: 0x000C2A88
			public IconRenderer3D(Vector2 {23063}, Rectangle {23064}, Action {23065} = null, float {23066} = 0f, float {23067} = 1f, Action {23068} = null, float {23069} = 1f, Texture2D {23070} = null)
			{
				this.InitialColor = Color.White;
				this.CreatToolTipContent = {23065};
				this.Size = new Vector2((float){23064}.Width / 400f * {23067});
				this.{23086} = {23069};
				this.Geometry = new BillboardParent_VPCT();
				this.MapPos = {23063};
				this.SetPosition({23063}, {23066}, true);
				this.Geometry.SetCol(this.InitialColor);
				this.ClickToolTip = {23068};
				this.CustomTexture = {23070};
				this.SetUv({23064});
			}

			// Token: 0x060016C4 RID: 5828 RVA: 0x000C4977 File Offset: 0x000C2B77
			public {22913}.IconRenderer3D SetVisibilityGroups(params {22887}.Id[] {23071})
			{
				this.VisibilityGroups = {23071};
				return this;
			}

			// Token: 0x060016C5 RID: 5829 RVA: 0x000C4984 File Offset: 0x000C2B84
			public void SetUv(in Rectangle {23072})
			{
				Texture2D texture2D = this.CustomTexture ?? OtherTextures.WorldMapUiElements;
				this.Geometry.SetUV({23072}, new Vector2((float)texture2D.Width, (float)texture2D.Height));
			}

			// Token: 0x060016C6 RID: 5830 RVA: 0x000C49C8 File Offset: 0x000C2BC8
			public void SetPosition(Vector2 {23073}, float {23074} = 0f, bool {23075} = true)
			{
				this.LastSSPositions = default(Vector2);
				if ({23073}.X < 999999f && {23075})
				{
					Global.Player.MapInfo.ClampToMap(ref {23073}, true);
				}
				this.{23085} = {23074};
				this.MapPos = {23073};
				this.UpdateGeometry(this.{23088});
			}

			// Token: 0x060016C7 RID: 5831 RVA: 0x000C4A20 File Offset: 0x000C2C20
			public void AnimationSetLerps(float {23076})
			{
				this.Size = new Vector2((0.5f + {23076}) * 0.6f);
				this.Geometry.SetCol(this.InitialColor * MathF.Sqrt(4f * {23076} * (1f - {23076})));
				this.UpdateGeometry(1f);
			}

			// Token: 0x060016C8 RID: 5832 RVA: 0x000C4A7C File Offset: 0x000C2C7C
			public void UpdateGeometry(float {23077} = 1f)
			{
				this.{23088} = {23077};
				Vector2 vector = {22913}.BilboardCenter(this.MapPos);
				this.Geometry.SetPos(-this.Size.Y * this.{23086} * {22913}.c_IconsScaleGeneral * {23077}, this.Size.X / this.{23086} * {22913}.c_IconsScaleGeneral * {23077});
				this.Geometry.Transform(Matrix.CreateRotationX(1.5707964f));
				this.Geometry.Transform(Matrix.CreateRotationY(3.1415927f - this.{23085}));
				this.Geometry.Transform(Matrix.CreateTranslation(this.{23084} = new Vector3(vector.X, -0.6f, vector.Y)));
			}

			// Token: 0x060016C9 RID: 5833 RVA: 0x000C4B3C File Offset: 0x000C2D3C
			public {22913}.IconRenderer3D SetColor(Color {23078})
			{
				this.InitialColor = {23078};
				this.Geometry.SetCol({23078});
				return this;
			}

			// Token: 0x060016CA RID: 5834 RVA: 0x000C4B54 File Offset: 0x000C2D54
			public void Render(ref Matrix {23079}, {22913}.IconRenderer3D {23080})
			{
				if (this.IsFlickering)
				{
					this.Geometry.SetCol(this.InitialColor * (0.7f + 0.3f * (float)Math.Sin(Global.Game.GameTotalTimeSec * 4.0)));
				}
				this.Geometry.Render();
				this.LastSSPositions = Engine.GS.Camera.GetProjection(Vector3.Transform(this.{23084}, {23079}));
				if (this.AsPathGeometry != null)
				{
					this.AsPathGeometryCache = (this.AsPathGeometryCache ?? new Vector2[this.AsPathGeometry.Length]);
					for (int i = 0; i < this.AsPathGeometry.Length; i++)
					{
						Vector2 vector = {22913}.BilboardCenter(this.AsPathGeometry[i]);
						Vector2 projection = Engine.GS.Camera.GetProjection(Vector3.Transform(new Vector3(vector.X, -0.6f, vector.Y), {23079}));
						this.AsPathGeometryCache[i] = projection;
					}
				}
				if ({23080} == this && this.{23087} != null && this.AsPathGeometry == null)
				{
					this.{23087}.Render(ref {23079}, null);
				}
			}

			// Token: 0x060016CB RID: 5835 RVA: 0x000C4C82 File Offset: 0x000C2E82
			public bool IsVisibleByGroups()
			{
				return this.VisibilityGroups.Length == 0 || {22887}.Statuses.Any((KeyValuePair<{22887}.Id, Reactive<bool>> {23083}) => {23083}.Value.Value && this.VisibilityGroups.Contains({23083}.Key));
			}

			// Token: 0x060016CC RID: 5836 RVA: 0x000C4CA8 File Offset: 0x000C2EA8
			public float GetScreenSpaceDistance(Vector2 {23081}, ref Matrix {23082})
			{
				if (this.AsPathGeometryCache == null)
				{
					return Vector2.DistanceSquared({23081}, this.LastSSPositions);
				}
				{23081} += new Vector2(0f, 15f);
				float num = TheraEngine.Helpers.Geometry.DistanceSquaredToLineSegments({23081}, this.AsPathGeometryCache);
				if (num > 225f)
				{
					return float.MaxValue;
				}
				return num;
			}

			// Token: 0x040014C8 RID: 5320
			public Vector2 LastSSPositions;

			// Token: 0x040014C9 RID: 5321
			public BillboardParent_VPCT Geometry;

			// Token: 0x040014CA RID: 5322
			public Action CreatToolTipContent;

			// Token: 0x040014CB RID: 5323
			public Vector2 Size;

			// Token: 0x040014CC RID: 5324
			public Vector2 MapPos;

			// Token: 0x040014CD RID: 5325
			public object Tag;

			// Token: 0x040014CE RID: 5326
			public Color InitialColor;

			// Token: 0x040014CF RID: 5327
			public Action ClickToolTip;

			// Token: 0x040014D0 RID: 5328
			public string Text;

			// Token: 0x040014D1 RID: 5329
			public Color TextColor = Color.Black * 0.8f;

			// Token: 0x040014D2 RID: 5330
			public string AltText;

			// Token: 0x040014D3 RID: 5331
			public Color AltTextColor = Color.Black * 0.8f;

			// Token: 0x040014D4 RID: 5332
			public float VisibleOnZoom;

			// Token: 0x040014D5 RID: 5333
			public bool IsVisibe = true;

			// Token: 0x040014D6 RID: 5334
			public Func<Vector2?> RealTimeUpdate;

			// Token: 0x040014D7 RID: 5335
			public {22887}.Id[] VisibilityGroups = Tlist<{22887}.Id>.EmptyReadonly.Array;

			// Token: 0x040014D8 RID: 5336
			public bool IsFlickering;

			// Token: 0x040014D9 RID: 5337
			public Texture2D CustomTexture;

			// Token: 0x040014DA RID: 5338
			public float TextScale = 1f;

			// Token: 0x040014DB RID: 5339
			public Vector2[] AsPathGeometry;

			// Token: 0x040014DC RID: 5340
			public Vector2[] AsPathGeometryCache;

			// Token: 0x040014DD RID: 5341
			private Vector3 {23084};

			// Token: 0x040014DE RID: 5342
			private float {23085};

			// Token: 0x040014DF RID: 5343
			private float {23086} = 1f;

			// Token: 0x040014E0 RID: 5344
			private {22913}.IconRenderer3D {23087};

			// Token: 0x040014E1 RID: 5345
			private float {23088} = 1f;
		}
	}
}
