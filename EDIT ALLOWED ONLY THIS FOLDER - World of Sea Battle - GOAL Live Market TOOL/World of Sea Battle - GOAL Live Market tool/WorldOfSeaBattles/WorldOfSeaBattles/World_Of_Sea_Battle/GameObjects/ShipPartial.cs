using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Graphics;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.ProcedureGeneration.Generation3D;
using TheraEngine.Scene;
using TheraEngine.Scene.ParticleSystem;
using UWPhysicsWOSLib;
using UWPhysicsWOSLib.Shapes;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Graphics.Effects;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x0200005F RID: 95
	internal class ShipPartial
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000070D7 File Offset: 0x000052D7
		public static bool drawGuildTagOnHp
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002DE RID: 734 RVA: 0x000176E7 File Offset: 0x000158E7
		public Vector2 Graphics2DPos
		{
			get
			{
				return this.{16993};
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002DF RID: 735 RVA: 0x000176EF File Offset: 0x000158EF
		public ShipScene Scene
		{
			get
			{
				return this.{17003};
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x000176F7 File Offset: 0x000158F7
		public ShipDestructionHelper DecalRenderer
		{
			get
			{
				return this.{17004};
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x000176FF File Offset: 0x000158FF
		public Vector2 GetSailStartValue
		{
			get
			{
				return this.{16991};
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00017707 File Offset: 0x00015907
		public bool IsVisible
		{
			get
			{
				return this.{16995};
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0001770F File Offset: 0x0001590F
		public bool IsVisibleWithOcclusionQueryAndCorpusTransparancy
		{
			get
			{
				return this.{16999} && this.{17002} > 0.2f;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00017728 File Offset: 0x00015928
		public bool IsCorusTransparentNow
		{
			get
			{
				return this.{17002} < 0.9f;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00017737 File Offset: 0x00015937
		public bool IsVisibleWithOcclusioAndNear
		{
			get
			{
				return this.{16999} && this.{17000} < 2;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0001774C File Offset: 0x0001594C
		public bool IsInitialized
		{
			get
			{
				return this.{17006} != null;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00017757 File Offset: 0x00015957
		public float Transparancy2D
		{
			get
			{
				return this.{17001};
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00017760 File Offset: 0x00015960
		public HealthBarStyle StatusColor
		{
			get
			{
				ShipNpc shipNpc = this.{17006} as ShipNpc;
				if (shipNpc != null)
				{
					return shipNpc.StatusColor;
				}
				ShipOtherPlayer shipOtherPlayer = this.{17006} as ShipOtherPlayer;
				if (shipOtherPlayer == null)
				{
					return HealthBarHelper.GetStyle(Relation.Neutral);
				}
				return shipOtherPlayer.StatusColor;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0001779F File Offset: 0x0001599F
		public float BlinkingMinimapEffect
		{
			get
			{
				if (this.{17007} < 1f)
				{
					return MathF.Pow(1f - this.{17007}, 1.5f);
				}
				return 0f;
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000177CC File Offset: 0x000159CC
		public ShipPartial(Ship {16944})
		{
			this.{17006} = {16944};
			this.{16987} = new Timer(800f);
			this.{16988} = new Timer(150f);
			this.{16989} = new Timer(300f);
			this.{16990} = new Timer(100f);
			this.healthBar = new HealthBar();
			this.{16996} = new LightSourceOcclusionTest();
			this.{16996}.QuerySize = 130;
			this.{16996}.UpdateIntervalMs = 100f;
			this.{17003} = new ShipScene();
			this.sailingAnimation = new Tlist<SoftTrigger>();
			this.{17005} = new Timer(4000f);
			this.{17004} = new ShipDestructionHelper();
			this.ExampleDesigns = new Tlist<ShipDesignInfo>(2);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001789C File Offset: 0x00015A9C
		public void CleanResources()
		{
			this.ResearchingBySpyglassLevel = 0f;
			this.healthBar.ClearResources();
			this.{16987}.Reset();
			this.{16988}.Reset();
			this.{16989}.Reset();
			this.{16990}.Reset();
			this.{16993} = Vector2.Zero;
			this.{16991} = Vector2.Zero;
			this.{16986} = new HealthBarSetup();
			this.{16992} = string.Empty;
			this.{16995} = false;
			this.ServerPositionDisplay = null;
			if (this.DeckCoversOpening != null)
			{
				this.DeckCoversOpening.CurrentSoftValue = 1f;
			}
			this.{17008} = 0f;
			this.{16994} = null;
			this.ExampleDesigns.Clear();
			this.{16998} = false;
			this.ShowTextMessage = null;
			this.{16997} = -1;
			this.{17003}.CleanResources();
			try
			{
				LightSourceOcclusionTest lightSourceOcclusionTest = this.{16996};
				if (lightSourceOcclusionTest != null)
				{
					lightSourceOcclusionTest.Dispose();
				}
				this.{16996} = new LightSourceOcclusionTest();
				this.{16996}.QuerySize = 130;
			}
			catch
			{
			}
			this.{17000} = 0;
			this.IsTransparantSail = false;
			this.sailingAnimation.Clear();
			this.ExampleSailTexturePreview = null;
			this.{17004}.CleanResouces();
			this.{17007} = 0f;
			this.{17001} = 0f;
			this.{17009} = false;
			this.{17002} = 1f;
			this.GunSoundLimiter = null;
			this.{17010} = 0f;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00017A28 File Offset: 0x00015C28
		public void ClientInitialize(string {16945}, bool {16946}, [Nullable(2)] ShipCreatePacket {16947})
		{
			this.{17004}.Initialize(this.{17006});
			this.{16986}.Update(this.{17006}, this);
			this.SetName({16945});
			this.{16991} = new Vector2(Rand.Range(0f, 10f), Rand.Range(0f, 10f));
			this.healthBar.Initialize(this.{17006});
			this.ServerPositionDisplay = new ShipServerPositionDisplay();
			this.DeckCoversOpening = new SoftTrigger(0f, 1f, 0.33f);
			this.UpdateModel();
			if (this.ItemsInHoldExemplary == null)
			{
				this.ItemsInHoldExemplary = new GSI();
			}
			this.{17006}.EvHpResotred += this.{16982};
			if (!{16946} || this.{17006}.UsedShip.IsInvisibilityBonusEnabled)
			{
				this.{17002} = 0.01f;
			}
			if ({16947} != null)
			{
				this.ItemsInHoldExemplary = {16947}.ItemsInHoldExemplary;
				using (IEnumerator<PublicDesignInfo> enumerator = ((IEnumerable<PublicDesignInfo>){16947}.PublicDesigns).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PublicDesignInfo item = enumerator.Current;
						if (!Session.PublicDesignsCache.Any((PublicDesignInfo {17011}) => {17011}.ID == item.ID))
						{
							Session.PublicDesignsCache.Add(item);
						}
					}
				}
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00017B8C File Offset: 0x00015D8C
		public void Update(ref FrameTime {16948})
		{
			NpcVisualEffect npcVisualEffect = NpcVisualEffect.Regular;
			ShipNpc shipNpc = this.{17006} as ShipNpc;
			if (shipNpc != null)
			{
				npcVisualEffect = shipNpc.UsedShipNpc.Information.SpecialEffect;
			}
			this.{17007} += {16948}.secElapsed;
			this.LastBallCollisionNormal.Z = this.LastBallCollisionNormal.Z + {16948}.secElapsed;
			Vector3 position3D = this.{17006}.Position3D;
			bool flag = this.{17006} is ShipCurrentPlayer;
			float num = (Global.Player == null) ? 0f : Vector2.Distance(Global.Player.Position, this.{17006}.Position);
			float num2 = Vector3.Distance(Global.Camera.Position, position3D);
			float num3 = this.{17006}.UsedShip.StaticInfo.BSRadius * 1.1f * (float)((num2 > 60f) ? 2 : 1);
			Vector3 {14982} = this.{17006}.Transform.Transform3X3(this.{17006}.UsedShip.StaticInfo.BSphere.Center);
			this.{16995} = Global.Camera.IsVisible({14982}, num3);
			this.{16998} = (num2 < 40f + num3 * 2f);
			this.{16999} = (flag || !this.{16996}.HasResults || this.{16996}.LastResult > 0f || (this.{16998} && this.{16995}));
			this.{17000} = ((num2 < Global.Render.LODDistanceForShips) ? 0 : ((num2 < Global.Render.LODDistanceForShips_Lod2) ? 1 : ((num2 < Global.Render.LODDistanceForShips_Lod3) ? 2 : 3)));
			if (Global.Camera.IsSpyglass)
			{
				if (flag)
				{
					this.{16999} = false;
				}
				this.{17000} = Math.Max(0, this.{17000} - 3);
			}
			float num4 = num + 5f;
			if (this.{16999} && num4 < WosbVisibility.VisibleDistanceNicknames(Global.Player))
			{
				this.{17001} = MathHelper.Clamp(this.{17001} + {16948}.secElapsed * 2f, 0f, 1f);
			}
			else
			{
				this.{17001} = MathHelper.Clamp(this.{17001} - {16948}.secElapsed * 2f, 0f, 1f);
			}
			float num5 = (Global.Player == null) ? 200f : WosbVisibility.VisibleDistance(Global.Player, Global.Game.StaticSystem.GetSkyShader.DayOrNight, CommonGlobal.CurrentClientWeather.FogLevelClient);
			float num6 = (Global.Player == null) ? 400f : WosbVisibility.ShipSilhouetteDistance(Global.Player, Global.Game.StaticSystem.GetSkyShader.DayOrNight, CommonGlobal.CurrentClientWeather.FogLevelClient);
			if (!Global.Camera.IsSpyglass)
			{
				this.{17001} *= MathF.Sqrt(1f - MathHelper.Lerp(Geometry.Saturate((num4 - (num5 + 5f) * 0.75f) / 600f), Geometry.Saturate((num4 - (num5 + 5f) * 0.9f) / 400f), Global.Game.StaticSystem.GetSkyShader.DayOrNight));
			}
			bool flag2 = false;
			if (this.{17006}.MapInfo.IsWorldmap)
			{
				Ship ship = this.{17006};
				ShipNpc np2 = ship as ShipNpc;
				if (np2 != null && np2.IsPlayerCaper && np2.MakeTransparentForMe && (Session.Group == null || !Session.Group.Members.Contains((GroupMemberInfo {17012}) => {17012}.UID == np2.UidPlayerForCaper)) && np2.UidPlayerForCaper != Global.Player.uID && !np2.WeaponsAreShooting)
				{
					Vector2 position = this.{17006}.Position;
					if (!Gameplay.IsInHazardZone(position, 0f))
					{
						WorldMapInfo worldMap = Gameplay.WorldMap;
						Vector2 position2 = this.{17006}.Position;
						if (worldMap.IsInsideMap(position2, true))
						{
							Vector2 position3 = Global.Player.Position;
							if (!Gameplay.IsInHazardZone(position3, 0f))
							{
								WorldMapInfo worldMap2 = Gameplay.WorldMap;
								Vector2 position4 = Global.Player.Position;
								if (worldMap2.IsInsideMap(position4, true))
								{
									{16948}.EvaluteTimerSec(ref this.{17010});
									if (this.{17010} == 0f)
									{
										flag2 = true;
										goto IL_463;
									}
									goto IL_463;
								}
							}
						}
					}
				}
			}
			this.{17010} = 45f;
			IL_463:
			if (!this.{17006}.UsedShip.StaticInfo.IsBalloon)
			{
				this.{17001} *= MathF.Sqrt(Geometry.InverseLerp(0f, 0.4f, this.{17002}));
				if (flag2)
				{
					this.{17002} = Math.Max(0f, this.{17002} - {16948}.secElapsed * 0.5f);
				}
				else
				{
					float num7 = 1f;
					ShipOtherPlayer shipOtherPlayer = this.{17006} as ShipOtherPlayer;
					if (shipOtherPlayer != null && shipOtherPlayer.GetBattleTimer > 0f)
					{
						num7 = 1f;
					}
					if (this.{17006}.MapInfo.IsWorldmap && Session.Account != null && Global.Game.GetCurrentSceneName != GameSceneName.Entry && ((IClientShip)this.{17006}).MakeTransparentForMe)
					{
						this.{17001} *= 0.94f;
						num7 *= MathF.Pow(Geometry.InverseLerp(20f, 70f, num), 1.3f) * 0.5f + 0.5f;
					}
					else if (Global.Player != null && this.{17006}.MapInfo.IsWorldmap && Global.Player.IsEntryToPortZoneContains && Vector2.Distance(this.{17006}.Position, this.{17006}.NearPort.EntryPos) < this.{17006}.NearPort.GlobalEntryRadius && this.{17006} != Global.Player)
					{
						num7 *= MathF.Pow(Geometry.InverseLerp(10f, 25f, num), 1.3f) * 0.6f + 0.4f;
					}
					if (this.{17006}.UsedShip.IsInvisibilityBonusEnabled && (num > 70f || this.{17006} == Global.Player))
					{
						num7 = ((this.{17006} == Global.Player) ? 0.4f : ((this.{17006}.WeaponsAreShootingWihtoutExtraTimers || this.{17006}.FalkonetShooting.ShotIsProcessing || this.{17006}.IsShottingVisible) ? 0.3f : 0.01f));
					}
					float speed = {16948}.secElapsed * ((num7 == 1f) ? 0.2f : 0.1f) * 4f;
					Geometry.Evalute(ref this.{17002}, num7, speed);
					this.{17002} = Math.Max(0.01f, this.{17002});
					this.{17002} = Math.Min(this.{17002}, 1f - Geometry.InverseLerp(num6 - 25f, num6, num));
				}
			}
			else
			{
				this.{17002} = 1f;
			}
			float zoom = ((GameCamera)Engine.GS.Camera).Zoom;
			if (flag && zoom > 11f)
			{
				this.{16998} = false;
				if (!Global.Camera.IsSpyglass && this.{16996}.HasResults && this.{16997} != this.{16996}.Version && flag && this.{16996}.LastResult < 0.001f && Global.Game.IsActive && Global.Camera.CameraEffects.PositionOffset.LengthSquared() < 16f)
				{
					((GameCamera)Engine.GS.Camera).OnOcclusionCloseCamera();
					this.{16997} = this.{16996}.Version;
				}
			}
			float num8 = this.{17006}.physicsBody.VelocityPerSec.Length() / 60f;
			if (this.{16995} && num2 < 210f && !this.{17006}.UsedShip.StaticInfo.IsBalloon && this.{17002} > 0.2f)
			{
				float num9 = {16948}.msElapsed * 20.25f * num8 * 2f;
				if (num9 > {16948}.msElapsed * 3f)
				{
					num9 = {16948}.msElapsed * 3f;
				}
				FrameTime frameTime = new FrameTime(num9, num9 / 1000f);
				if (num8 > 0.01f && this.{16987}.Sample(ref frameTime))
				{
					FXEngine.CreateShipWaves(this.{17006}, ref {16948});
				}
				if (num8 > 0.01f && (Global.Render.LogicallyOverloadingFactor < 0.1f || num2 < 105f) && this.{16988}.Sample(ref frameTime))
				{
					FXEngine.CreateShipWavesParticles(this.{17006}, ref {16948});
				}
			}
			if (this.{17006}.UsedShip.FirstHP.BigBurningProcessTime > 0f)
			{
				float bigBurningFlareUpLevel = this.{17006}.UsedShip.FirstHP.BigBurningFlareUpLevel;
				FXEngine.SampleShipBurning(this.{17006}, bigBurningFlareUpLevel);
				if (this.{17005}.Sample(ref {16948}))
				{
					Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.FireBig, this.{17006}.Position3D, 0.5f + 0.5f * bigBurningFlareUpLevel, false);
				}
			}
			else
			{
				this.{17005}.Reset();
			}
			if (!this.{17006}.UsedShip.StaticInfo.IsBalloon && !this.{17006}.UsedShip.IsInvisibilityBonusEnabled && this.IsVisibleWithOcclusionQueryAndCorpusTransparancy)
			{
				float num10 = 1f - Geometry.Saturate(this.{17006}.UsedShip.HpFactor / 0.4f);
				ShipNpc shipNpc2 = this.{17006} as ShipNpc;
				if (shipNpc2 != null && shipNpc2.IsFirebrand)
				{
					num10 = 1f;
				}
				if (num10 > 0f && Rand.Chanse(20f * (0.5f + num10 * 0.5f) * {16948}.Factor))
				{
					FXEngine.SampleShipSmoke(num10, this.{17006});
				}
			}
			FXEngine.SampleShipMicroburning(this.{17006}, this.{17006}.UsedShip.FirstHP.aliveMicroburning, this.{17006}.Transform);
			if (Global.Settings.kb_KeyShowMouse.IsRelease || Global.Settings.kb_KeyShowMouse.IsDown)
			{
				this.{16986}.Update(this.{17006}, this);
			}
			if (this.{16989}.Sample(ref {16948}))
			{
				this.{16986}.Update(this.{17006}, this);
				if (npcVisualEffect != NpcVisualEffect.Regular)
				{
					FXEngine.ExecuteParticlesSpecialEffect(npcVisualEffect, this.{17006});
				}
				this.AvailableForBoardingByPlayer = false;
				this.AvailableForBoardingByPlayerWithDistance = false;
				if (Global.Player != null && Global.Player.MapInfo.IsBoardingEnable && EducationHelper.AllowBoarding && (this.{17006} is Npc || (Global.Player.MapInfo.IsWorldmap && !Session.Account.WorldFlag.IsPeaceMode()) || Global.Player.MapInfo.IsEnableArenaUi) && {18139}.CurrentInstance == null && !Global.Player.UsedShip.StaticInfo.IsBalloon)
				{
					if (!Session.IsShipContainsPlayerGroup(this.{17006}.uID) || this.StatusColor == HealthBarStyle.Red)
					{
						ShipNpc shipNpc3 = this.{17006} as ShipNpc;
						if ((shipNpc3 == null || !shipNpc3.IsAllyByCaper(Global.Player.uID)) && this.StatusColor != HealthBarStyle.Lime)
						{
							Ship player = Global.Player;
							Ship {3876} = this.{17006};
							ShipOtherPlayer shipOtherPlayer2 = this.{17006} as ShipOtherPlayer;
							this.AvailableForBoardingByPlayerWithDistance = (WosbBoarding.CheckFor(player, {3876}, shipOtherPlayer2 != null && shipOtherPlayer2.IsPeace, true, true) == BoardingAvailability.Allow);
							Ship player2 = Global.Player;
							Ship {3876}2 = this.{17006};
							ShipOtherPlayer shipOtherPlayer3 = this.{17006} as ShipOtherPlayer;
							this.AvailableForBoardingByPlayer = (WosbBoarding.CheckFor(player2, {3876}2, shipOtherPlayer3 != null && shipOtherPlayer3.IsPeace, true, false) == BoardingAvailability.Allow);
							goto IL_BF4;
						}
					}
					this.AvailableForBoardingByPlayerWithDistance = false;
					this.AvailableForBoardingByPlayer = false;
				}
				IL_BF4:
				ShipNpc shipNpc4 = this.{17006} as ShipNpc;
				if (shipNpc4 != null && shipNpc4.AgressionToCurrentPlayer != NpcAgression.No && shipNpc4.CurrentAgressionTargetUID == -1)
				{
					float num11 = shipNpc4.UsedShipNpc.Information.MaxAgressionDistance(shipNpc4.AgressionToCurrentPlayer, Session.Account, Global.Game.StaticSystem.GetSkyShader.DayOrNight);
					if (num - num11 < 30f && num - num11 > 0f && Session.TimeFromLastReceivedDamageSec > 45f)
					{
						{20059} currentInstance = {20059}.CurrentInstance;
						if (currentInstance != null)
						{
							currentInstance.HitscanUpdate(this.{17006}.Position, {20059}.c_hitWarning);
						}
					}
				}
				if (Global.Game.GetCurrentSceneName == GameSceneName.Game && !Global.Camera.IsFreeMode && Global.Settings.SailTransparancy != LocalSettings.SailTransparancyMode.NoUse && this.{17006}.UsedShip.StaticInfo.ID != 1)
				{
					if (flag)
					{
						this.IsTransparantSail = false;
						if (Global.Camera.IsMinZoom || Global.Settings.SailTransparancy == LocalSettings.SailTransparancyMode.Always)
						{
							this.IsTransparantSail = true;
						}
						else if (Global.Settings.SailTransparancy == LocalSettings.SailTransparancyMode.WhenZoom)
						{
							this.IsTransparantSail = (Global.Camera.Zoom < 13.5f + Global.Player.UsedShip.StaticInfo.CorpusHalfLength * 0.6f);
						}
						else if (Global.Settings.SailTransparancy == LocalSettings.SailTransparancyMode.InBattle)
						{
							this.IsTransparantSail = (this.{17006}.FirstController.LinearStateCode == 2 || Session.TimeFromLastReceivedDamageSec < 15f || !this.{17006}.UsedShip.Cannons.IsAllReloadedApprox);
						}
					}
					else
					{
						this.IsTransparantSail = (Global.Settings.SailTransparancy != LocalSettings.SailTransparancyMode.NoUse && num2 < 15f + this.{17006}.UsedShip.StaticInfo.CorpusHalfLength);
					}
				}
				else
				{
					this.IsTransparantSail = false;
				}
				if (this.{17006}.CraftFrom.ID != 62 || this.{17006} != Global.Player || Global.Player.GetBattleTimer != 0f)
				{
					ShipNpc shipNpc5 = this.{17006} as ShipNpc;
					if (shipNpc5 == null)
					{
						goto IL_EBC;
					}
					NpcShipDynamicInfo usedShipNpc = shipNpc5.UsedShipNpc;
					if (usedShipNpc == null)
					{
						goto IL_EBC;
					}
					NpcInfo information = usedShipNpc.Information;
					if (information == null || information.Descritpion != NpcType.Empire_Legendary3l)
					{
						goto IL_EBC;
					}
				}
				for (int i = 0; i < 8; i++)
				{
					FXEngine.Template_VolumetricFog_Sample(this.{17006}.Position3D + Rand.NextVector3(-1f, 1f).Normal() * (float)(17 + i) * new Vector3(1f, 0.1f, 1f), 0.2f, 4000f, 10f);
				}
			}
			IL_EBC:
			if (this.{16990}.Sample(ref {16948}) && this.{17006}.physicsBody.CanUseWheel && this.{17006}.UsedShip.StaticInfo.HasSteamWheel)
			{
				Vector3 center = this.{17006}.UsedShip.StaticInfo.Model.pipeOut.CommonSphere.Center;
				Matrix matrix;
				this.{17006}.Transform.CreateWorldMatrix(out matrix);
				Vector3 {12170};
				Vector3.Transform(ref center, ref matrix, out {12170});
				FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({12170}), 0.5f, 500f, 3000f, 1f, false, FXEngine.PowderParticleType.Dark, 0.7f, null);
			}
			InterestPointsManager.UpdateResearch(num, ref this.ResearchingBySpyglassLevel, {16948}, Global.Game.InterestPoints.ShipInSight == this.{17006});
			{14982} = ShipPartial.ComputeTopPoisiton(position3D, num2, this.{17006}.UsedShip.StaticInfo.BSRadius);
			if (Global.Camera.IsVisible({14982}, 5f))
			{
				this.{16993} = Global.Camera.GetProjection({14982});
				this.{16993}.X = (float)((int)this.{16993}.X);
				this.{16993}.Y = (float)((int)this.{16993}.Y);
			}
			else
			{
				this.{16993} = new Vector2(0f, 1000000f);
			}
			this.healthBar.Update(ref {16948});
			if (this.{17006}.OpenSailesFromIndices.Length == this.sailingAnimation.Size && this.{17006}.UsedShip.FirstHP.FloodingFactor == 0f)
			{
				float num12 = 0f;
				float num13 = 0f;
				int num14 = 0;
				bool flag3 = this.{17006}.UsedShip.FirstHP.FloodingFactor > 0f;
				float {11490} = 1f / Gameplay.CSailAnimDurationSec(this.{17006}) * (flag3 ? 0.4f : 1f);
				FrameTime {11489} = {16948} * {11490};
				for (int j = 0; j < this.sailingAnimation.Size; j++)
				{
					bool flag4 = flag3 || !this.{17006}.OpenSailesFromIndices[j];
					if (this.sailingAnimation.Array[j].IsChangesAvailable(flag4))
					{
						float {11490}2 = 0.8f + 0.4f * HashHelper.greater(j);
						FrameTime frameTime2 = {11489} * {11490}2;
						num12 += this.sailingAnimation.Array[j].CurrentSoftValue;
						this.sailingAnimation.Array[j].Evalute(ref frameTime2, flag4);
						num13 += this.sailingAnimation.Array[j].CurrentSoftValue;
						num14++;
					}
				}
				num13 /= (float)num14;
				num12 /= (float)num14;
				if (!this.{17006}.UsedShip.StaticInfo.IsBalloon && ((Global.Player != null && !Global.Player.IsPortEntry) || num < 50f))
				{
					if (num12 == 1f && num13 < 1f)
					{
						Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.ShipSailDeploy, this.{17006}.Position3D, 0.4f, false);
					}
					if (num12 == 0f && num13 > 0f)
					{
						Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.ShipSailClose, this.{17006}.Position3D, 0.4f, false);
					}
				}
			}
			if (this.{17006}.FirstController.LinearStateCode != 3)
			{
				this.{17008} = 1600f;
			}
			{16948}.EvaluteTimerMs(ref this.{17008});
			this.DeckCoversOpening.Evalute(ref {16948}, this.{17008} > 0f);
			if (this.ShowTextMessage != null && {16948}.EvaluteTimerMs2(ref this.ShowTextMessage.TimeoutMs))
			{
				this.ShowTextMessage = null;
			}
			ShipNpc shipNpc6 = this.{17006} as ShipNpc;
			if (shipNpc6 != null && shipNpc6.DisasterMode && num < 80f && !this.{17009})
			{
				this.{17009} = true;
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00018E6C File Offset: 0x0001706C
		public void OcclusionQuery()
		{
			if (this.{16998})
			{
				this.{16996}.CleanResults();
				return;
			}
			this.queryPosition = this.{17006}.Position3D - Engine.GS.Camera.Direction * this.{17006}.UsedShip.StaticInfo.BSRadius * ((this.{17006} == Global.Player) ? 0.1f : 0.33f) + new Vector3(0f, this.{17006}.UsedShip.StaticInfo.CorpusShape.LocalCenter.Y + this.{17006}.UsedShip.StaticInfo.CorpusShape.FinalHeight * 0.3f * 0.5f, 0f);
			Vector3 vector = this.queryPosition - Engine.GS.Camera.Position;
			Vector2 vector2 = vector.XZ();
			float num = vector2.Length();
			vector2.Normalize();
			vector2 = Vector2.Lerp(vector2, Engine.GS.Camera.Direction.XZNormal(), 0.08f);
			vector.X = vector2.X * num;
			vector.Z = vector2.Y * num;
			this.queryPosition = Engine.GS.Camera.Position + vector * ((this.{17006} == Global.Player) ? 0.66f : 1f);
			this.{16996}.BeginTest(this.queryPosition, this.IsVisible, this.{17006}.UsedShip.StaticInfo.IsBalloon, Global.Render.ItemsShader);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00019024 File Offset: 0x00017224
		public void RenderModel()
		{
			if (!this.{16999})
			{
				return;
			}
			this.{16951}((float)((this.IsTransparantSail || this.{17002} < 1f || (Global.Camera.IsMinZoom && !Global.Camera.IsFreeMode && this.{17006} == Global.Player)) ? 0 : 1), this.{17002} == 1f);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00019090 File Offset: 0x00017290
		public void RenderTransparantSail()
		{
			if (!this.{16999})
			{
				return;
			}
			this.{16951}(this.{17002} * (this.IsTransparantSail ? ((Global.Camera.IsMinZoom && !Global.Camera.IsFreeMode) ? 0.175f : 0.3f) : ((this.{17002} == 1f) ? ((Global.Camera.IsMinZoom && this.{17006} == Global.Player) ? 0.5f : 0f) : 1f)), (this.{17002} == 1f) ? 0f : this.{17002});
			this.{17003}.RenderTransparentUnits();
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00019140 File Offset: 0x00017340
		public Texture2D GetDecal(ShipDesignInfo {16949}, ShipPlayerBase {16950})
		{
			if ({16949} == null)
			{
				return null;
			}
			if (LocalContent.IsFullDesign((int){16949}.ID))
			{
				return null;
			}
			Texture2D result;
			try
			{
				result = LocalContent.GetDecalForSail({16949}, null).Tex;
			}
			catch (NullReferenceException)
			{
				{16950}.UsedShipPlayer.RemoveDesignElement(1);
				{16950}.UsedShipPlayer.RemoveDesignElement(3);
				result = null;
			}
			return result;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000191A4 File Offset: 0x000173A4
		public void CheckForFullDesign()
		{
			ShipPlayerBase shipPlayerBase = this.{17006} as ShipPlayerBase;
			if (shipPlayerBase != null)
			{
				ShipDesignInfo shipDesignInfo = this.GetPreview(ShipDesignCategory.Decal1) ?? shipPlayerBase.UsedShipPlayer.GetDesignElement(1);
				ShipDesignInfo shipDesignInfo2 = this.GetPreview(ShipDesignCategory.Decal2) ?? shipPlayerBase.UsedShipPlayer.GetDesignElement(3);
				if (shipDesignInfo != null && LocalContent.IsFullDesign((int)shipDesignInfo.ID))
				{
					Global.Render.CommonShader.SetOrResetTextureReplaceDesign(new int?((int)shipDesignInfo.ID));
				}
				else if (shipDesignInfo2 != null && LocalContent.IsFullDesign((int)shipDesignInfo2.ID))
				{
					Global.Render.CommonShader.SetOrResetTextureReplaceDesign(new int?((int)shipDesignInfo2.ID));
				}
			}
			int designReplaceId = this.GetDesignReplaceId();
			if (designReplaceId != 0)
			{
				Global.Render.CommonShader.SetOrResetTextureReplaceDesign(new int?(designReplaceId));
			}
			if (this.UseDesignEditor != null)
			{
				Global.Render.CommonShader.SetOrResetTextureReplaceDesign(new int?(999));
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0001928C File Offset: 0x0001748C
		public int GetDesignReplaceId()
		{
			ShipPlayerBase shipPlayerBase = this.{17006} as ShipPlayerBase;
			if (shipPlayerBase != null)
			{
				ShipDesignInfo shipDesignInfo = this.GetPreview(ShipDesignCategory.ShipFullDesign) ?? shipPlayerBase.UsedShipPlayer.GetDesignElement(4);
				if (shipDesignInfo != null)
				{
					return (int)shipDesignInfo.ID;
				}
			}
			else
			{
				ShipNpc shipNpc = this.{17006} as ShipNpc;
				if (shipNpc != null)
				{
					ShipDesignInfo shipCorpusDesing = shipNpc.UsedShipNpc.ShipCorpusDesing;
					if (shipCorpusDesing == null)
					{
						return 0;
					}
					return (int)shipCorpusDesing.ID;
				}
			}
			return 0;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000192F4 File Offset: 0x000174F4
		private void {16951}(float {16952}, float {16953})
		{
			if ({16952} == 0f && {16953} == 0f)
			{
				return;
			}
			this.CheckForFullDesign();
			if ({16953} > 0f)
			{
				Global.Render.CommonShader.SetShipDestrDecal(this.{17004}.IntencityMap);
				this.{17003}.DrawCorpus(this.{17000}, {16953});
				Global.Render.CommonShader.SetShipDestrDecal(null);
				this.{17003}.DrawWeaponsAndPaddles({16953});
				this.{17003}.DrawSteamWheel();
			}
			if ({16952} > 0f)
			{
				int? currentDesignReplace = Global.Render.CommonShader.CurrentDesignReplace;
				this.{17003}.DrawMastsAndFrames(({16952} == 1f) ? 1f : ({16952} * 0.6f));
				Global.Render.CommonShader.SetOrResetTextureReplaceDesign(null);
				this.{17003}.DrawSailes({16952}, this.{16991});
				this.{17003}.DrawFlags({16952});
				this.{17003}.DrawFlagpoles({16952});
			}
			Global.Render.CommonShader.SetOrResetTextureReplaceDesign(null);
			if (Debugging.DebugInfo && {16953} == 1f)
			{
				this.{16955}();
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001941B File Offset: 0x0001761B
		public void CheckVisibilityAndRenderToGBuffer(IGBufferBuilder {16954})
		{
			if (!this.{16999})
			{
				return;
			}
			if (this.{17002} > 0.1f)
			{
				this.{17003}.GBuffer({16954}, this.{16999});
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00019445 File Offset: 0x00017645
		public void DrawEffectsAndDecals()
		{
			this.{17003}.DrawEffectsAndDecals();
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00019454 File Offset: 0x00017654
		private void {16955}()
		{
			for (int i = 0; i < this.{17006}.UsedShip.StaticInfo.HitboxTableByNodeID.Size; i++)
			{
				Tlist<IShipHitbox> hitboxTableByNodeID = this.{17006}.UsedShip.StaticInfo.HitboxTableByNodeID;
				if (hitboxTableByNodeID.Array[i].Type != HitboxType.Sail)
				{
					if (hitboxTableByNodeID.Array[i].Type == HitboxType.Corpus && this.{17006}.UsedShip.StaticInfo.EditorBoxShape != null)
					{
						ShipPartial.DisplayShape(this.{17006}.Transform, this.{17006}.UsedShip.StaticInfo.EditorBoxShape);
					}
					else
					{
						foreach (BoxShape {16957} in hitboxTableByNodeID.Array[i].EnumerateBaseShapes())
						{
							ShipPartial.DisplayShape(this.{17006}.Transform, {16957});
						}
					}
				}
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00019554 File Offset: 0x00017754
		private static void DisplayShape(Transform3D {16956}, BoxShape {16957})
		{
			Transform3D transform3D = new Transform3D();
			transform3D.Translation = Vector3.Transform({16957}.LocalCenter, {16956}.CreateWorldMatrixRotationAndScaleOnly()) + {16956}.Translation;
			transform3D.Scales = new Vector3({16957}.FinalLength, {16957}.FinalHeight, {16957}.FinalWidth) * 0.3f;
			transform3D.RotatesAll = {16956}.RotatesAll;
			Global.Render.ItemsShader.Texture.SetValue(AtlasObjs.Texture.Tex);
			Global.Render.ItemsShader.World.SetValue(transform3D.CreateWorldMatrix());
			Global.Render.ItemsShader.BeginPassTextureNoColor();
			LocalContent.Loaded.DebugBoxShapeDisplay.OptimizedRenderAllBuffers();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00019614 File Offset: 0x00017814
		private static void DisplayUnitSphere(Vector3 {16958})
		{
			LocalContent.Loaded.DebugSphereDisplay.Transform.Translation = {16958};
			LocalContent.Loaded.DebugSphereDisplay.Transform.Scales = Vector3.One * 1.5f;
			Global.Render.ItemsShader.BeginPassTextureNoColor();
			Global.Render.CommonShader.RenderObject(LocalContent.Loaded.DebugSphereDisplay, false, 1f, false, 0f, false);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00003100 File Offset: 0x00001300
		private void {16959}()
		{
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00019690 File Offset: 0x00017890
		private void {16960}()
		{
			Vector2 value = Engine.GS.MouseToUI - Engine.GS.UIArea.HalfWidthHeightInt();
			value.Normalize();
			Vector2 vector = this.{17006}.Position + value * 10f;
			UserIndexedMesh userIndexedMesh = SphereGenerator.Begin_VertexPositionColor(32, 1f, Color.Blue);
			Global.Render.ItemsShader.World.SetValue(Matrix.CreateTranslation(new Vector3(vector.X, 2f, vector.Y)));
			Global.Render.ItemsShader.BeginPass(false, false);
			userIndexedMesh.Render();
			Global.Render.ItemsShader.SetForRender(Matrix.Identity, Vector4.One);
			Global.Render.ItemsShader.BeginPass(false, false);
			Vector2 vector2;
			vector2.X = this.{17006}.Position.X - vector.X;
			vector2.Y = this.{17006}.Position.Y - vector.Y;
			vector2.Normalize();
			float num = Geometry.AxisNorm(MathF.Atan2(vector2.Y, vector2.X) - this.{17006}.Rotation - 1.5707964f);
			Vector2 vector3 = ProjectedShipNormals.CollisionAngleToReprojNormal(num, this.{17006});
			Vector3 vector4 = new Vector3(vector.X, 3f, vector.Y);
			Engine.GS.Render3DLine<VertexPositionColor>(new VertexPositionColor(vector4, Color.Blue), new VertexPositionColor(vector4 + new Vector3(vector3.X, 0f, vector3.Y) * 6f, Color.White));
			Engine.GS.Render3DLine<VertexPositionColor>(new VertexPositionColor(this.{17006}.Position3D, Color.Blue), new VertexPositionColor(vector4, Color.White));
			for (int i = 0; i <= 100; i++)
			{
				num = (float)i / 100f * 6.2831855f;
				Vector2 vector5 = ProjectedShipNormals.CollisionAngleToReprojNormal(num + 1.5707964f, this.{17006});
				vector4 = this.{17006}.Position3D + new Vector3(MathF.Cos(num + this.{17006}.Rotation) * 5f, 3f, MathF.Sin(num + this.{17006}.Rotation) * 5f);
				Engine.GS.Render3DLine<VertexPositionColor>(new VertexPositionColor(vector4, Color.Red), new VertexPositionColor(vector4 + new Vector3(vector5.X, 0f, vector5.Y), Color.White));
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0001992C File Offset: 0x00017B2C
		public void Render2D(HealthBarStyle {16961}, Tlist<Rectangle> {16962} = null)
		{
			if (this.{17001} == 0f || Global.Render.UiMode == InterfaceMode.Off)
			{
				return;
			}
			Color color = Color.White * this.{17001};
			this.healthBar.Render(this.{16993} + this.{16986}.HealthBarOffset, {16961}, this.{17001});
			if (this.{17006}.MapInfo.IsWorldmap)
			{
				ShipOtherPlayer shipOtherPlayer = this.{17006} as ShipOtherPlayer;
				if (shipOtherPlayer != null && shipOtherPlayer.MirrorEngageInPortBattlePortID != -1 && Session.EngagingInPortBattle == PbsBatlleSide.None)
				{
					ShipPartial.DrawMarker(this.{16993}, AtlasObjs.c_pbLabelIcon, Vector2.Zero, 1f, color);
					goto IL_14C;
				}
			}
			if ({16962} != null)
			{
				for (int i = 0; i < {16962}.Size; i++)
				{
					Rectangle rectangle = {16962}.Array[i];
					Vector2 vector;
					vector.X = this.{16993}.X - (float)(rectangle.Width * {16962}.Size / 2) + (float)(i * rectangle.Width);
					vector.Y = this.{16993}.Y - (float)rectangle.Height - 15f;
					Device gs = Engine.GS;
					Color color2 = Color.White * 0.7f * this.{17001};
					gs.Draw(rectangle, vector, color2);
				}
			}
			IL_14C:
			ShipNpc shipNpc = this.{17006} as ShipNpc;
			if (shipNpc != null)
			{
				if (shipNpc.IsPlayerCaper)
				{
					Rectangle rectangle2 = shipNpc.IsAllyByCaper(Global.Player.uID) ? AtlasObjs.c_shipCapturedByMeIcon : AtlasObjs.c_shipCapturedOtherIcon;
					Vector2 vector2;
					vector2.X = this.{16993}.X - (float)(rectangle2.Width / 2);
					vector2.Y = this.{16993}.Y - (float)rectangle2.Height - 25f;
					Engine.GS.Draw(rectangle2, vector2, color);
				}
				else
				{
					float num = AtlasObjs.MarkerSightingRedScaleY * 0.5f;
					if (Global.Player.MapInfo.IsEducationMap && {18646}.IsFirstScenario)
					{
						ShipPartial.DrawMarker(this.{16993}, AtlasObjs.marker_sightingRed, new Vector2(0f, -5f), num, color);
					}
					else if (shipNpc.AgressionToCurrentPlayer != NpcAgression.No && !Global.Player.IsDestroyed && shipNpc.CurrentAgressionTargetUID != Global.Player.uID)
					{
						float num2 = shipNpc.UsedShipNpc.Information.MaxAgressionDistance(shipNpc.AgressionToCurrentPlayer, Session.Account, Global.Game.StaticSystem.GetSkyShader.DayOrNight);
						float num3 = Vector2.Distance(Global.Player.Position, shipNpc.Position);
						if (shipNpc.CurrentAgressionTargetUID == Global.Player.uID)
						{
							ShipPartial.DrawMarker(this.{16993}, AtlasObjs.marker_sightingRed, new Vector2(0f, -5f), num, color);
						}
						else
						{
							this.{16968}(AtlasObjs.marker_sightingRed, AtlasObjs.marker_sightingRedFree, Geometry.Saturate((num3 - num2) / 70f), new Vector2(0f, -5f), num, color);
						}
					}
					Rectangle npcIcon = AtlasObjs.GetNpcIcon(shipNpc.UsedShipNpc.Information.Extras.Icon);
					if (npcIcon.Width > 0)
					{
						this.{16986}.iconSize = 27;
						this.{16986}.iconOffset = new Vector2(0f, -4f);
						this.{16986}.IconOrEmpty = npcIcon;
					}
					else
					{
						this.{16986}.IconOrEmpty = Rectangle.Empty;
					}
					if (this.{17006}.MapInfo.IsWorldmap && Session.LastMinimapAndGroupUpdate.enemies.Contains(new Func<EnemyStateTransfer, bool>(this.{16984})))
					{
						ShipPartial.DrawMarker(this.{16993}, AtlasObjs.c_shipMarkerByMinimap_targetRed, new Vector2(0f, 0f), 16f / (float)AtlasObjs.c_shipMarkerByMinimap_targetRed.Width, color);
					}
				}
				if (shipNpc.ApproximatelyFirebrandTimeoutMs > 0f)
				{
					HealthBarHelper.DrawForShip(this.{16993} + new Vector2(0f, 14f), HealthBarStyle.Blue, shipNpc.ApproximatelyFirebrandTimeoutMs / (float)PowderKeg.FirebrandTimeoutMs, this.{17001}, 0f, Vector2.Zero);
				}
			}
			if (this.ResearchingBySpyglassLevel > 0f)
			{
				this.{16986}.DrawIcons(this.{16993}, this.{17001});
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00019D7C File Offset: 0x00017F7C
		public static void DrawMarker(Vector2 {16963}, Rectangle {16964}, Vector2 {16965}, float {16966}, Color {16967})
		{
			Vector2 vector;
			vector.X = {16963}.X - (float)({16964}.Width / 2) * {16966} + {16965}.X;
			vector.Y = {16963}.Y - (float){16964}.Height * {16966} - 20f + {16965}.Y;
			Device gs = Engine.GS;
			Vector2 zero = Vector2.Zero;
			gs.Draw({16964}, vector, zero, 0f, {16966}, {16967});
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00019DEC File Offset: 0x00017FEC
		private void {16968}(Rectangle {16969}, Rectangle {16970}, float {16971}, Vector2 {16972}, float {16973}, Color {16974})
		{
			Vector2 vector;
			vector.X = this.{16993}.X - (float)({16969}.Width / 2) * {16973} + {16972}.X;
			vector.Y = this.{16993}.Y - (float){16969}.Height * {16973} - 20f + {16972}.Y;
			Engine.GS.DrawProgressbarVertical({16969}, {16970}, vector, 1f - {16971}, {16973}, new Color?({16974}));
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00019E6C File Offset: 0x0001806C
		public ShipDesignInfo GetPreview(ShipDesignCategory {16975})
		{
			for (int i = 0; i < this.ExampleDesigns.Size; i++)
			{
				if (this.ExampleDesigns.Array[i].Category == {16975})
				{
					return this.ExampleDesigns.Array[i];
				}
			}
			return null;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00019EB4 File Offset: 0x000180B4
		public void RenderText()
		{
			if (this.{17001} == 0f || Global.Render.UiMode == InterfaceMode.Off)
			{
				return;
			}
			if (this.ResearchingBySpyglassLevel >= 1f)
			{
				this.{16986}.DrawText(this.{16993}, this.{17001}, (IClientShip)this.{17006});
			}
			if (this.ShowTextMessage != null && Global.Render.UiMode == InterfaceMode.Default)
			{
				Engine.GS.SetFont(Fonts.Philosopher_12);
				Vector2 value = this.{16993} - new Vector2(Engine.GS.Font.Measure(this.ShowTextMessage.Data).X / 2f, -25f) - new Vector2(0f, 10f);
				Device gs = Engine.GS;
				string data = this.ShowTextMessage.Data;
				Vector2 vector = value + new Vector2(0f, 10f);
				Color color = Color.White * (this.{17001} * this.ShowTextMessage.FadeEffect(1500f));
				gs.DrawString(data, vector, color);
			}
			if (this.{17006}.UsedShip.FirstHP.Summary != 0f)
			{
				this.healthBar.RenderText(this.{16993} + this.{16986}.HealthBarOffset, this.{17001}, this.{17006});
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0001A01D File Offset: 0x0001821D
		public void SetName(string {16976})
		{
			this.{16992} = {16976};
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001A028 File Offset: 0x00018228
		public string GetName2()
		{
			bool flag;
			return this.GetName(out flag);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001A040 File Offset: 0x00018240
		public string GetName2AndTag()
		{
			bool flag;
			string name = this.GetName(out flag);
			if (!flag && !string.IsNullOrEmpty(this.Guild.Tag))
			{
				return "[" + this.Guild.Tag + "] " + name;
			}
			return name;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0001A088 File Offset: 0x00018288
		public string GetRealName()
		{
			return this.{16992};
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001A090 File Offset: 0x00018290
		public string GetName(out bool {16977})
		{
			ShipOtherPlayer shipOtherPlayer = this.{17006} as ShipOtherPlayer;
			if (shipOtherPlayer != null && shipOtherPlayer.HideNickname)
			{
				{16977} = true;
				return shipOtherPlayer.UsedShipPlayer.CraftFrom.ShipName;
			}
			{16977} = false;
			return this.{16992};
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0001A0D1 File Offset: 0x000182D1
		public void CreateLampLight(ShipDesignInfo {16978})
		{
			if (this.{16994} != null)
			{
				this.{16994}.ManualRemove();
				this.{16994} = null;
			}
			if ({16978} != null && !this.{17006}.IsDestroyed)
			{
				this.{16994} = new ShipCustomLightEffect(this.{17006}, {16978});
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0001A110 File Offset: 0x00018310
		public void UpdateModel()
		{
			if (this.{17006} != null)
			{
				this.{17003}.UpdateModelData(this.{17006});
				if (this.sailingAnimation.Size != this.{17006}.UsedShip.StaticInfo.SailHitboxes.Length)
				{
					this.sailingAnimation.Clear();
					for (int i = 0; i < this.{17006}.UsedShip.StaticInfo.SailHitboxes.Length; i++)
					{
						Tlist<SoftTrigger> tlist = this.sailingAnimation;
						SoftTrigger softTrigger = new SoftTrigger(0f, 1f, 1f);
						tlist.Add(softTrigger);
					}
				}
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001A1AC File Offset: 0x000183AC
		public void UpdateDesignsPositions()
		{
			this.{17003}.UpdateCustomPositions(this.{17006});
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001A1C0 File Offset: 0x000183C0
		public void SwitchSailesState()
		{
			if (this.{17006}.OpenSailesFromIndices.Length == this.sailingAnimation.Size)
			{
				for (int i = 0; i < this.sailingAnimation.Size; i++)
				{
					this.sailingAnimation.Array[i].SetValue(!this.{17006}.OpenSailesFromIndices[i]);
				}
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0001A220 File Offset: 0x00018420
		public static Vector3 ComputeTopPoisiton(Vector3 {16979}, float {16980}, float {16981})
		{
			float num = 0.4f + 0.6f * Geometry.Saturate({16980} / 130f);
			if (Global.Camera.IsSpyglass)
			{
				num *= 0.65f;
			}
			return {16979} + new Vector3(0f, ({16981} + 3f) * num, 0f);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0001A278 File Offset: 0x00018478
		[CompilerGenerated]
		private void {16982}(float {16983})
		{
			this.{17004}.Restore((this.{17006}.UsedShip.FirstHP.Summary > this.{17006}.UsedShip.MaxHp - 1f) ? 1f : ({16983} / this.{17006}.UsedShip.MaxHp));
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0001A2D6 File Offset: 0x000184D6
		[CompilerGenerated]
		private bool {16984}(EnemyStateTransfer {16985})
		{
			return {16985}.ShipUID == this.{17006}.uID;
		}

		// Token: 0x04000238 RID: 568
		private const float c_MinimalVelocityLengthToEffectParticle = 0.01f;

		// Token: 0x04000239 RID: 569
		private const float c_octopusMinTransparancy = 0.01f;

		// Token: 0x0400023A RID: 570
		private HealthBarSetup {16986};

		// Token: 0x0400023B RID: 571
		private Timer {16987};

		// Token: 0x0400023C RID: 572
		private Timer {16988};

		// Token: 0x0400023D RID: 573
		private Timer {16989};

		// Token: 0x0400023E RID: 574
		private Timer {16990};

		// Token: 0x0400023F RID: 575
		private Vector2 {16991};

		// Token: 0x04000240 RID: 576
		private string {16992};

		// Token: 0x04000241 RID: 577
		private Vector2 {16993};

		// Token: 0x04000242 RID: 578
		internal HealthBar healthBar;

		// Token: 0x04000243 RID: 579
		private ShipCustomLightEffect {16994};

		// Token: 0x04000244 RID: 580
		private bool {16995};

		// Token: 0x04000245 RID: 581
		private LightSourceOcclusionTest {16996};

		// Token: 0x04000246 RID: 582
		private int {16997};

		// Token: 0x04000247 RID: 583
		private bool {16998};

		// Token: 0x04000248 RID: 584
		private bool {16999};

		// Token: 0x04000249 RID: 585
		private int {17000};

		// Token: 0x0400024A RID: 586
		private float {17001};

		// Token: 0x0400024B RID: 587
		private float {17002};

		// Token: 0x0400024C RID: 588
		private ShipScene {17003};

		// Token: 0x0400024D RID: 589
		private ShipDestructionHelper {17004};

		// Token: 0x0400024E RID: 590
		internal Tlist<SoftTrigger> sailingAnimation;

		// Token: 0x0400024F RID: 591
		private Timer {17005};

		// Token: 0x04000250 RID: 592
		private readonly Ship {17006};

		// Token: 0x04000251 RID: 593
		private float {17007};

		// Token: 0x04000252 RID: 594
		private float {17008};

		// Token: 0x04000253 RID: 595
		internal Vector3 queryPosition;

		// Token: 0x04000254 RID: 596
		private bool {17009};

		// Token: 0x04000255 RID: 597
		private float {17010};

		// Token: 0x04000256 RID: 598
		public Dictionary<string, string> UseDesignEditor;

		// Token: 0x04000257 RID: 599
		public GSI ItemsInHoldExemplary;

		// Token: 0x04000258 RID: 600
		public GuildShortInfo Guild;

		// Token: 0x04000259 RID: 601
		public TemporaryEffect<string> ShowTextMessage;

		// Token: 0x0400025A RID: 602
		public bool AvailableForBoardingByPlayerWithDistance;

		// Token: 0x0400025B RID: 603
		public bool AvailableForBoardingByPlayer;

		// Token: 0x0400025C RID: 604
		public ShipServerPositionDisplay ServerPositionDisplay;

		// Token: 0x0400025D RID: 605
		public Tlist<ShipDesignInfo> ExampleDesigns;

		// Token: 0x0400025E RID: 606
		public byte? ExampleSailTexturePreview;

		// Token: 0x0400025F RID: 607
		public bool IsTransparantSail;

		// Token: 0x04000260 RID: 608
		public SoftTrigger DeckCoversOpening;

		// Token: 0x04000261 RID: 609
		public Stopwatch GunSoundLimiter;

		// Token: 0x04000262 RID: 610
		public float ResearchingBySpyglassLevel;

		// Token: 0x04000263 RID: 611
		public Vector3 LastBallCollisionNormal;
	}
}
