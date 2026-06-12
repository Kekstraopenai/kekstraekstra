using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Interface;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x02000486 RID: 1158
	internal class CannonsController : IInGameSightUI
	{
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x000DF572 File Offset: 0x000DD772
		public static float SightHitEffectDurationMs
		{
			get
			{
				return 500f;
			}
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x000DF57C File Offset: 0x000DD77C
		public static Rectangle GunModeIconPath(CannonsAttackMode {24123})
		{
			if ({24123} == CannonsAttackMode.SingleCannon)
			{
				return CannonsController.p_gunMode_single;
			}
			if ({24123} == CannonsAttackMode.AllRandomized)
			{
				return CannonsController.p_gunMode_all;
			}
			if ({24123} != CannonsAttackMode.AllNormal)
			{
				throw new NotSupportedException();
			}
			return CannonsController.p_gunMode_normal;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x000DF5AB File Offset: 0x000DD7AB
		public float ReductionFactor
		{
			get
			{
				return this.{24158};
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x000DF5B3 File Offset: 0x000DD7B3
		public float ReductionFactorGain
		{
			get
			{
				return MathF.Pow(this.{24158}, 0.8f);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x000DF5C5 File Offset: 0x000DD7C5
		public Vector2 NowDirection
		{
			get
			{
				return this.{24160};
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x000DF5CD File Offset: 0x000DD7CD
		public Vector2 FinalAttackDirection
		{
			get
			{
				return this.{24164};
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x000DF5D5 File Offset: 0x000DD7D5
		public Vector3 FinalAttackNormal
		{
			get
			{
				return this.{24163};
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x000DF5DD File Offset: 0x000DD7DD
		public Vector2 FollowDir
		{
			get
			{
				return this.{24162};
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x000DF5E5 File Offset: 0x000DD7E5
		public bool HasActiveAndReloadedCannons
		{
			get
			{
				return this.{24169}.Count((CannonCommon {24172}) => {24172}.IsReloaded) > 0;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x000DF614 File Offset: 0x000DD814
		public bool HasActiveCannons
		{
			get
			{
				return this.{24169}.Size > 0;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x000DF624 File Offset: 0x000DD824
		public bool IsActiveFireguns
		{
			get
			{
				return this.{24169}.Size > 0 && this.{24169}.Array[0].GameInfo.Feature == CannonFeature.Firegun;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x000DF650 File Offset: 0x000DD850
		public CannonLocation? LastActiveNearBoard
		{
			get
			{
				return this.{24170};
			}
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x000DF658 File Offset: 0x000DD858
		private void {24124}()
		{
			this.{24167} = CannonsController.GunModeIconPath(this.PickedGunMode);
			string text = (this.PickedGunMode == CannonsAttackMode.SingleCannon) ? ((Session.Account.CaptainSkills[PDynamicAccountBonus.BSingleGunAdditionalDamage] > 0) ? Local.Sight_0 : Local.Sight_0.Split('#', StringSplitOptions.None)[0]) : ((this.PickedGunMode == CannonsAttackMode.AllNormal) ? Local.Sight_1 : Local.Sight_2);
			this.{24168} = text.Split('#', StringSplitOptions.None);
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x000DF6D0 File Offset: 0x000DD8D0
		public CannonsController()
		{
			this.VolumeSightRender = new VolumetricSightShader();
			this.{24124}();
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x000DF730 File Offset: 0x000DD930
		public static Vector3 GetViewNormal(Vector2 {24125})
		{
			Vector3 result = new Vector3(0f, 0f, -1f);
			Matrix matrix;
			Matrix.CreateFromYawPitchRoll(-{24125}.Y, {24125}.X, 0f, out matrix);
			Vector3.Transform(ref result, ref matrix, out result);
			return result;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x000DF778 File Offset: 0x000DD978
		private Vector3 {24126}(Vector3 {24127}, float {24128}, float {24129}, float {24130}, float {24131})
		{
			Functions.HeightDist {4654} = new Functions.HeightDist
			{
				SightDistance = {24128},
				SightHeight = {24129}
			};
			if (Global.Camera.IsSpyglass)
			{
				return {24127} + new Vector3({24130} * {24128}, Functions.GetBallHeight(0.9f, {4654}, 0f) - 0.5f, {24131} * {24128});
			}
			return {24127} + new Vector3({24130} * {24128}, Functions.GetBallHeight(0.66f, {4654}, 0f) - 0.5f, {24131} * {24128});
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x000DF800 File Offset: 0x000DDA00
		private CannonBallInfo GetAmmoInfo
		{
			get
			{
				return Gameplay.BallsInfo.FromID((this.{24170} != null) ? ((this.{24170}.Value == CannonLocation.InFront && Global.Player.UsedShip.Cannons.HavingFireguns) ? 1 : Global.Settings.SelectedCannonBalls[this.{24170}.Value]) : 1);
			}
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x000DF868 File Offset: 0x000DDA68
		public void Update(ref FrameTime {24132}, bool {24133})
		{
			bool flag = InputHelper.NowInputState.IsUp(Global.Settings.kb_Action.Key);
			if (InputHelper.NowMouseState.RightPressed)
			{
				InGameSightUi.OnAction();
			}
			if (Global.Settings.RightMouseAction == RightMouseKeyAction.AddDispersion)
			{
				this.{24159}.Evalute(ref {24132}, InputHelper.NowMouseState.RightPressed);
			}
			if (flag)
			{
				Vector2.Dot(this.{24163}.XZNormal(), Global.Player.Normal);
				float num = 0f;
				this.{24162} = new Vector2(Global.Camera.Rotation.X + 0.1f + num * 0.66f, Global.Camera.Rotation.Y);
			}
			if (Global.Player.IsMarchingMode)
			{
				this.{24157} = Math.Min(1f, this.{24157} + {24132}.secElapsed * 0.33f);
			}
			else
			{
				float num2 = 0.85f - (float)Math.Min(70, Global.Player.UsedShip.StaticInfo.LeftSidePorts.Length) * 0.005f;
				this.{24157} = Math.Max(this.{24157} - Math.Min(1.1f, 0.1f + 3f * this.{24157}) * num2 * 0.4f * {24132}.secElapsed * ((this.{24169}.Size > 0 && this.{24169}.First().GameInfo.WorseScatter) ? 0.8f : 1f) * (1f + Global.Player.UsedShip.SightFocusSpeedAdd + 0f), 0f);
			}
			this.{24160}.Y = this.{24160}.Y + Global.Player.physicsBody.AngularVelocity * 0.5f;
			Geometry.AxisNormFast(ref this.{24160}.Y);
			if (this.{24160} != this.{24162})
			{
				Vector2 vector = this.{24162};
				float num3 = MathHelper.Clamp(Geometry.AxisDistance(this.{24160}.Y, this.{24162}.Y), 0f, 1f);
				if (this.{24157} < 0.8f)
				{
					this.{24157} = MathHelper.Clamp(this.{24157} + {24132}.secElapsed * 3f * 0.5f * num3, 0f, 0.8f);
				}
				if (this.{24165} != null)
				{
					float num4 = 2.2f * {24132}.secElapsed * num3;
					num4 *= 1f + (float)Session.Account.CaptainSkills[PDynamicAccountBonus.PSightSpeed] / 100f;
					Geometry.AngularMovement(ref this.{24160}.Y, this.{24162}.Y, num4);
					this.{24160}.X = this.{24162}.X;
				}
				else
				{
					this.{24160} = this.{24162};
				}
				this.{24162} = vector;
			}
			if (Global.Player.ClientWeaponsShooting.ShotIsProcessing && this.{24171} != -10f && Geometry.AxisDistance(this.{24171}, this.{24160}.Y) > 0.03f)
			{
				Global.Network.Send(new OnLocSightAxisChange(this.{24171} - this.{24160}.Y));
				this.{24171} = this.{24160}.Y;
			}
			if (float.IsNaN(this.{24157}))
			{
				this.{24157} = 0f;
			}
			this.{24158} = Geometry.Saturate(this.{24157} + this.{24159}.CurrentSoftValue);
			this.{24164} = this.{24155}();
			this.{24163} = CannonsController.GetViewNormal(this.{24164});
			this.{24169}.Clear();
			this.{24170} = CannonsController.GetNearNotEmptyBoard(this.{24163}.XZNormal());
			CannonBallInfo getAmmoInfo = this.GetAmmoInfo;
			this.{24165} = Global.Player.UsedShip.Cannons.SelectAvailableCannons(this.{24160}, Global.Player, this.{24169}, (this.PickedGunMode == CannonsAttackMode.SingleCannon) ? CannonsAttackMode.AllNormal : this.PickedGunMode, this.{24163}.Y, Global.Player.UsedShipPlayer, getAmmoInfo, false);
			this.{24161} = null;
			CannonLocation? nb = CannonsController.GetNearNotEmptyBoard(Engine.GS.Camera.Direction.XZNormal());
			if (this.{24169}.Size == 0 && Global.Player.UsedShip.Cannons.Items.Size > 0 && nb != null && Global.Player.UsedShip.Cannons.Items.Any(delegate(CannonCommon {24174})
			{
				CannonLocation side = {24174}.Location.Side;
				CannonLocation? nb = nb;
				return side == nb.GetValueOrDefault() & nb != null;
			}) && !InGameSightUi.MortarSights.IsActive && InGameSightUi.FalkonetSights.RenderAmount == 0f)
			{
				float num5 = Global.Player.UsedShip.Cannons.Items.Max(delegate(CannonCommon {24175})
				{
					CannonLocation side = {24175}.Location.Side;
					CannonLocation? nb = nb;
					if (!(side == nb.GetValueOrDefault() & nb != null))
					{
						return 0f;
					}
					return {24175}.GameInfo.MaxAxis;
				}) + Global.Player.UsedShip.CannonsAxisBonusRad;
				float y = Global.Player.UsedShip.Cannons.Items.First(delegate(CannonCommon {24176})
				{
					CannonLocation side = {24176}.Location.Side;
					CannonLocation? nb = nb;
					return side == nb.GetValueOrDefault() & nb != null;
				}).Location.CurrentDirection.Y;
				float num6 = Global.Player.Rotation + 3.1415927f + y - num5 * 0.98f;
				float num7 = Global.Player.Rotation + 3.1415927f + y + num5 * 0.98f;
				Geometry.AxisNormFast(ref num6);
				Geometry.AxisNormFast(ref num7);
				float num8 = Geometry.AxisDistance(num6, this.{24160}.Y);
				float num9 = Geometry.AxisDistance(num7, this.{24160}.Y);
				if (Math.Min(num8, num9) - num5 < 0.31415927f)
				{
					this.{24170} = nb;
					this.{24161} = new Vector2?(new Vector2(this.{24160}.X, (num8 < num9) ? num6 : num7));
					this.{24165} = Global.Player.UsedShip.Cannons.SelectAvailableCannons(this.{24161}.Value, Global.Player, this.{24169}, (this.PickedGunMode == CannonsAttackMode.SingleCannon) ? CannonsAttackMode.AllNormal : this.PickedGunMode, this.{24163}.Y, Global.Player.UsedShipPlayer, getAmmoInfo, false);
				}
			}
			{24132}.EvaluteTimerMs(ref this.{24166});
			if (GameScene.GameHasInputFocus && !Global.Game.IsMouseVisible && !Global.Player.IsDestroyed)
			{
				if (Global.Settings.RightMouseAction != RightMouseKeyAction.SingleCannonGun)
				{
					if (Global.Settings.kb_SwitchCascadeGunMode.IsClick)
					{
						InGameSightUi.OnAction();
						this.PickedGunMode = EnumHelper.RollValue<CannonsAttackMode>(this.PickedGunMode, new CannonsAttackMode[]
						{
							CannonsAttackMode.X_AllAntiNormal
						});
						if (this.PickedGunMode == CannonsAttackMode.AllRandomized && Session.Account.CaptainSkills[PDynamicAccountBonus.BAllowAllBoradGun] == 0)
						{
							this.PickedGunMode = CannonsAttackMode.SingleCannon;
						}
						this.{24124}();
						this.{24166} = 3000f;
					}
				}
				else if (Global.Settings.kb_SwitchCascadeGunMode.IsClick && Session.Account.CaptainSkills[PDynamicAccountBonus.BAllowAllBoradGun] > 0)
				{
					if (this.PickedGunMode == CannonsAttackMode.AllNormal)
					{
						this.PickedGunMode = CannonsAttackMode.AllRandomized;
					}
					else
					{
						this.PickedGunMode = CannonsAttackMode.AllNormal;
					}
					InGameSightUi.OnAction();
					this.{24124}();
					this.{24166} = 3000f;
				}
			}
			if (Session.Account.CaptainSkills[PDynamicAccountBonus.BAllowAllBoradGun] == 0 && this.PickedGunMode == CannonsAttackMode.AllRandomized)
			{
				this.PickedGunMode = CannonsAttackMode.AllNormal;
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x000DFFF0 File Offset: 0x000DE1F0
		public void Render2DAnyMode(float {24134})
		{
			if (this.{24166} != 0f)
			{
				Vector2 value = Engine.GS.UIArea.HalfWidthHeightInt() - new Vector2(128f, (float)(-(float)this.{24167}.Height / 2));
				Color color = Color.White * (this.{24166} / 3000f);
				Engine.GS.Draw(this.{24167}, value, color);
				Engine.GS.SetFont(Fonts.Philosopher_16);
				Device gs = Engine.GS;
				string {14599} = this.{24168}[0];
				Vector2 vector = value + new Vector2(0f, 64f);
				gs.DrawString({14599}, vector, color);
				if (this.{24168}.Length > 1)
				{
					Engine.GS.SetFont(Fonts.Arial_12);
					Device gs2 = Engine.GS;
					string {14599}2 = this.{24168}[1];
					vector = value + new Vector2(0f, 89f);
					gs2.DrawString({14599}2, vector, color);
				}
			}
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x000E00E8 File Offset: 0x000DE2E8
		public void Render2D(float {24135})
		{
			if (Global.Player.UsedShip.Cannons.Count == 0 || {24135} == 0f)
			{
				return;
			}
			if (Global.Settings.SightIndex == 1)
			{
				this.{24143}({24135});
			}
			if (Global.Settings.SightIndex == 3)
			{
				this.{24145}({24135});
				return;
			}
			if (Global.Settings.SightIndex == 2)
			{
				this.{24147}({24135});
			}
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x000E0154 File Offset: 0x000DE354
		public void Render3DProjected(float {24136})
		{
			if ({24136} == 0f || Global.Render.UiMode != InterfaceMode.Default)
			{
				return;
			}
			Vector2 position = Global.Player.Position;
			this.{24137}(CannonLocation.RightSide, {24136});
			this.{24137}(CannonLocation.LeftSide, {24136});
			this.{24137}(CannonLocation.InFront, {24136});
			this.{24137}(CannonLocation.InBack, {24136});
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x000E01A4 File Offset: 0x000DE3A4
		private void {24137}(CannonLocation {24138}, float {24139})
		{
			float num = ({24138} == CannonLocation.RightSide) ? -1.5707964f : (({24138} == CannonLocation.LeftSide) ? 1.5707964f : (({24138} == CannonLocation.InFront) ? 0f : 3.1415927f));
			Vector2 normal = Global.Player.Normal;
			Vector2 value = Geometry.RotateVector2(Global.Player.Normal, -1.5707964f);
			Vector2 value2;
			Geometry.RotateVector2Fast(ref normal, num, out value2);
			float num2 = Math.Max(0f, (Vector2.Dot(value2, Global.Camera.Direction.XZNormal()) * 0.5f + 0.5f - 0.35f) / 0.65f);
			if (Global.Player.MapInfo.IsEducationMap)
			{
				num2 = MathF.Sqrt(num2) * 1.3f;
			}
			if ({22279}.CurrentInstance != null)
			{
				num2 = 1f;
			}
			float num3 = num2 * {24139};
			if (num3 == 0f)
			{
				return;
			}
			float num4 = Global.Player.UsedShip.Cannons.BoardMiddleAxis({24138}) + Global.Player.UsedShip.CannonsAxisBonusRad;
			float num5 = (Global.Player.UsedShip.Cannons.Items.Size == 0) ? 0f : Global.Player.UsedShip.Cannons.Items.Max(delegate(CannonCommon {24177})
			{
				if ({24177}.Location.Side != {24138})
				{
					return 0f;
				}
				return {24177}.GameInfo.MaxDistance;
			});
			if ({24138} != CannonLocation.InFront || !Global.Player.UsedShip.Cannons.HavingFireguns)
			{
				num5 *= Gameplay.BallsInfo.FromID(Global.Settings.SelectedCannonBalls[{24138}]).DistanceFactor;
			}
			num5 += Global.Player.UsedShip.BallDistanceBonusValue;
			if (num4 > 0f && num5 > 0f)
			{
				Vector2 vector = Global.Player.Position;
				if ({24138} == CannonLocation.InFront)
				{
					vector += normal * Global.Player.UsedShip.StaticInfo.CannonMax.X;
				}
				if ({24138} == CannonLocation.InBack)
				{
					vector += normal * Global.Player.UsedShip.StaticInfo.CannonMin.X;
				}
				Vector2 {15467};
				Vector2 {15468};
				switch ({24138})
				{
				case CannonLocation.LeftSide:
					{15467} = vector + normal * Global.Player.UsedShip.StaticInfo.CannonMax.X;
					{15468} = vector + normal * Global.Player.UsedShip.StaticInfo.CannonMin.X;
					break;
				case CannonLocation.RightSide:
					{15467} = vector + normal * Global.Player.UsedShip.StaticInfo.CannonMin.X;
					{15468} = vector + normal * Global.Player.UsedShip.StaticInfo.CannonMax.X;
					break;
				case CannonLocation.InFront:
					{15467} = vector + value * Global.Player.UsedShip.StaticInfo.CannonMax.Y;
					{15468} = vector + value * Global.Player.UsedShip.StaticInfo.CannonMin.Y;
					break;
				case CannonLocation.InBack:
					{15467} = vector - value * Global.Player.UsedShip.StaticInfo.CannonMax.Y;
					{15468} = vector - value * Global.Player.UsedShip.StaticInfo.CannonMin.Y;
					break;
				default:
					throw new NotSupportedException();
				}
				Vector2 vector2;
				Geometry.RotateVector2Fast(ref normal, num - num4, out vector2);
				vector2 = vector2 * num5 + vector;
				Vector2 vector3;
				Geometry.RotateVector2Fast(ref normal, num + num4, out vector3);
				vector3 = vector3 * num5 + vector;
				Vector2 vector4 = this.{24164};
				float rotation = Global.Player.Rotation;
				Vector2 zero = Vector2.Zero;
				Vector2 zero2 = Vector2.Zero;
				Global.Render.ItemsShader.RenderProjectedSight({15467}, {15468}, vector2, vector3, zero2, zero, InGameSightUi.GetDimGray * num3 * (1.7f + 0.4f * Global.Game.StaticSystem.GetSkyShader.DayOrNight) * 1.2f);
			}
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x000E0620 File Offset: 0x000DE820
		public void Render3D(float {24140})
		{
			if ({24140} == 0f)
			{
				return;
			}
			if (this.{24170} != null && this.GetAmmoInfo[CannonBallInfoEffects.FireArea] && this.{24165} != null && this.{24169}.Size > 0)
			{
				float num = this.{24165}.Value.SightDistance / this.{24169}.First().GameInfo.MaxDistance;
				CannonLocationInfo cannonLocationInfo = (this.{24170}.Value == CannonLocation.InFront) ? Global.Player.UsedShip.StaticInfo.FrontSidePorts.FirstOrDefault<CannonLocationInfo>() : ((this.{24170}.Value == CannonLocation.InBack) ? Global.Player.UsedShip.StaticInfo.BackSidePorts.FirstOrDefault<CannonLocationInfo>() : null);
				float num2 = (cannonLocationInfo == null) ? 0f : (cannonLocationInfo.Position.X * Global.Player.Transform.MiddleScale);
				float num3 = 1.5f * MathF.Pow(this.{24165}.Value.SightDistance, 0.94f);
				MortarController.AreaSightHelper(Global.Player.Position - Geometry.SubstructRotateFast(this.{24164}.Y + 1.5707964f, num3 + num2), 8f, {24140});
			}
			if (Global.Settings.SightIndex == 0 && Global.Player.UsedShip.Cannons.Count != 0)
			{
				this.{24141}({24140});
			}
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x000E0798 File Offset: 0x000DE998
		private void {24141}(float {24142})
		{
			if (this.{24165} == null)
			{
				return;
			}
			Vector2 vector = this.{24164};
			LocalContent.Loaded.SightLine.Transform.Translation = Global.Player.Position3D.AddY(Global.Player.UsedShip.StaticInfo.MiddleCannonsHeight);
			LocalContent.Loaded.SightLine.Transform.Scales = Vector3.One;
			LocalContent.Loaded.SightLine.Transform.Yaw = this.{24164}.Y;
			Vector2 vector2 = (this.{24170} != null) ? Global.Player.UsedShip.Cannons.GetMinMaxReloadFactor(this.{24170}.Value) : new Vector2(0f, 0f);
			this.VolumeSightRender.SetTexture(AtlasObjs.Texture.Tex);
			this.VolumeSightRender.DrawEffect(Global.Camera.ViewMultiplyProjection, LocalContent.Loaded.SightLine.Transform.CreateWorldMatrix(), new Vector2(this.{24165}.Value.SightDistance, this.{24165}.Value.SightHeight), 1f - this.ReductionFactorGain * 1.5f, ((vector2.X < 1f) ? (Color.Lerp(Color.Pink, InGameSightUi.SightColor, 0.5f) * 0.7f) : (InGameSightUi.SightColor * 1.8f)).ToVector4() * {24142});
			LocalContent.Loaded.SightLine.GetModels[0].Model.OptimizedRenderAllBuffers();
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000E0948 File Offset: 0x000DEB48
		private void {24143}(float {24144})
		{
			if (this.{24165} == null || this.{24170} == null)
			{
				return;
			}
			float num = 0.5f;
			float y = Engine.GS.Camera.Position.Y;
			float num2 = Math.Max(0f, y - num);
			Vector3 value = new Vector3(0f, 0.01f + num2 / 100f, 0f);
			Vector3 vector = this.{24163} + value;
			int num3 = Global.Camera.IsSpyglass ? 1 : Global.Player.UsedShip.Cannons.NumberOfCannons(this.{24170}.Value);
			float num4 = (num3 >= 50) ? 0.8f : ((num3 >= 30) ? 0.9f : ((num3 >= 8) ? 1f : ((num3 == 1) ? 1.5f : 1.3f)));
			num4 *= 1.15f - ((this.{24165} != null) ? this.{24165}.GetValueOrDefault().SightDistance : 100f) / 240f;
			Color value2 = Color.White * ((num3 >= 8) ? 0.7f : 0.9f) * {24144};
			float num5 = 0.04f + 0.1f * num4;
			float num6 = this.ReductionFactorGain;
			Matrix matrix;
			Global.Player.Transform.CreateWorldMatrix(out matrix);
			foreach (CannonCommon cannonCommon in ((IEnumerable<CannonCommon>)this.{24169}))
			{
				CannonBallInfo {4583} = Gameplay.BallsInfo.FromID(Global.Settings.SelectedCannonBalls[cannonCommon.Location.Side]);
				float sightDistance = this.{24165}.Value.SightDistance;
				Vector3 vector2 = vector;
				num6 = cannonCommon.ModifySightReductionFactor(num6, {4583}, Global.Player.UsedShip, true, false);
				float sightHeight = this.{24165}.Value.SightHeight;
				float reloadFactor = cannonCommon.ReloadFactor;
				float num7 = 1f - Math.Min(1f, (cannonCommon.RollbackEffectMax - cannonCommon.RollbackEffect) / 4000f);
				float num8 = Math.Max(0f, num7) * Math.Min(1f, (1f - num7) * 25f);
				cannonCommon.FocusAndGunDispersion(ref vector2, ref sightDistance, ref sightHeight, Math.Min(1f, num6 + ((this.PickedGunMode == CannonsAttackMode.AllRandomized) ? (0.1f * num8) : 0f)), cannonCommon.GameInfo.MaxDistance, true);
				Vector3 position = cannonCommon.GetPosition(ref matrix);
				Vector3 vector3 = this.{24126}(position, sightDistance, sightHeight, vector2.X, vector2.Z);
				vector3.Y += (vector2.Y - vector.Y) * 20f;
				Vector2 projectionSmoothed = Engine.GS.Camera.GetProjectionSmoothed(ref vector3);
				if (reloadFactor == 0f)
				{
					Device gs = Engine.GS;
					float {14558} = 0f;
					float {14559} = num5 * (1f + num8 * 0.2f);
					Color color = value2 * (1f - num8 * 0.3f);
					gs.Draw(CannonsController.p_sightMicro_red, projectionSmoothed, CannonsController.p_sightMicroCannon_centr, {14558}, {14559}, color);
				}
				else if (reloadFactor == 1f)
				{
					Device gs2 = Engine.GS;
					float {14558}2 = 0f;
					float {14559}2 = num5 * (1f + num8 * 0.2f);
					Color color = value2 * (1f - num8 * 0.3f);
					gs2.Draw(CannonsController.p_sightMicro, projectionSmoothed, CannonsController.p_sightMicroCannon_centr, {14558}2, {14559}2, color);
				}
				else
				{
					CannonsController.DrawSightCircle(projectionSmoothed, num5 * (1f + num8 * 0.2f), value2 * (1f - num8 * 0.3f), reloadFactor, true);
				}
				float num9 = cannonCommon.ClientHitEffectMs / CannonsController.SightHitEffectDurationMs;
				if (num9 > 0f)
				{
					Device gs3 = Engine.GS;
					float {14558}3 = 0f;
					float {14559}3 = num5 * (1f + num8 * 0.2f);
					Color color = Color.White * num9;
					gs3.Draw(CannonsController.p_sightMicro_effect, projectionSmoothed, CannonsController.p_sightMicroCannon_centr, {14558}3, {14559}3, color);
					Device gs4 = Engine.GS;
					float {14558}4 = 0f;
					float {14559}4 = num5 * (1f + num8 * 0.2f);
					color = Color.Yellow;
					int r = (int)color.R;
					color = Color.Yellow;
					int g = (int)color.G;
					color = Color.Yellow;
					color = new Color(r, g, (int)color.B, 64) * num9;
					gs4.Draw(CannonsController.p_sightMicro_effect, projectionSmoothed, CannonsController.p_sightMicroCannon_centr, {14558}4, {14559}4, color);
				}
			}
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x000E0DE8 File Offset: 0x000DEFE8
		private void {24145}(float {24146})
		{
			{24146} *= 1f - InGameSightUi.FalkonetSights.RenderAmount;
			float num = 0.5f;
			Vector2 vector2;
			if (this.{24165} == null)
			{
				CannonBallInfo getAmmoInfo = this.GetAmmoInfo;
				Vector3 vector = Global.Player.Position3D + this.{24163} * new Vector3(1f, 0f, 1f) * Functions.GetData(140f, this.{24163}.Y, 1f, getAmmoInfo.DistanceFactor).SightDistance;
				Vector2 projection = Engine.GS.Camera.GetProjection(ref vector);
				Device gs = Engine.GS;
				vector2 = new Vector2((float)(CannonsController.p_simpleSightCenter.Width / 2), 0f);
				float {14558} = 0f;
				float {14559} = num;
				Color color = Color.White * 0.8f * {24146};
				gs.Draw(CannonsController.p_simpleSightCenter, projection, vector2, {14558}, {14559}, color);
				return;
			}
			float num2 = (165f + this.ReductionFactorGain * 160f) * num;
			Vector2 vector3 = new Vector2((float)(CannonsController.p_simpleSightLineLeft.Width / 2), (float)(CannonsController.p_simpleSightLineLeft.Height / 2));
			Vector2 vector4 = (this.{24170} != null) ? Global.Player.UsedShip.Cannons.GetMinMaxReloadFactor(this.{24170}.Value) : new Vector2(0f, 0f);
			Vector3 vector5 = this.{24126}(Global.Player.Position3D.AddY(Global.Player.UsedShip.StaticInfo.MiddleCannonsHeight), this.{24165}.Value.SightDistance, this.{24165}.Value.SightHeight, this.{24163}.X, this.{24163}.Z);
			Vector2 projection2 = Engine.GS.Camera.GetProjection(ref vector5);
			Color color2 = ((vector4.X < 1f) ? Color.Gray : Color.White) * 0.8f * {24146};
			Color color3 = Color.Cyan * 0.5f * {24146};
			Device gs2 = Engine.GS;
			vector2 = projection2 + new Vector2(-num2 * 0.5f, -30f * num);
			gs2.Draw(CannonsController.p_simpleSightLineLeft, vector2, vector3, 0f, num, color2);
			Device gs3 = Engine.GS;
			vector2 = projection2 + new Vector2(num2 * 0.5f, -30f * num);
			gs3.Draw(CannonsController.p_simpleSightLineRight, vector2, vector3, 0f, num, color2);
			Device gs4 = Engine.GS;
			vector2 = new Vector2((float)(CannonsController.p_simpleSightCenter.Width / 2), 0f);
			gs4.Draw(CannonsController.p_simpleSightCenter, projection2, vector2, 0f, num, color2);
			float num3 = 1f - vector4.X;
			if (num3 > 0f)
			{
				Marker marker = new Marker(ref CannonsController.p_simpleSightLineLeft).SetHeight((float)CannonsController.p_simpleSightLineLeft.Height * num3);
				Marker marker2 = new Marker(ref CannonsController.p_simpleSightLineRight).SetHeight((float)CannonsController.p_simpleSightLineRight.Height * num3);
				Device gs5 = Engine.GS;
				Rectangle rectangle = marker.ToRect();
				vector2 = projection2 + new Vector2(-num2 * 0.5f, -30f * num);
				gs5.Draw(rectangle, vector2, vector3, 0f, num, color3);
				Device gs6 = Engine.GS;
				rectangle = marker2.ToRect();
				vector2 = projection2 + new Vector2(num2 * 0.5f, -30f * num);
				gs6.Draw(rectangle, vector2, vector3, 0f, num, color3);
			}
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x000E118C File Offset: 0x000DF38C
		private void {24147}(float {24148})
		{
			if (this.{24165} == null)
			{
				return;
			}
			Vector3 position3D = Global.Player.Position3D;
			Vector2 vector = (this.{24170} != null) ? Global.Player.UsedShip.Cannons.GetMinMaxReloadFactor(this.{24170}.Value) : new Vector2(0f, 0f);
			Vector3 vector2 = this.{24126}(Global.Player.Position3D.AddY(Global.Player.UsedShip.StaticInfo.MiddleCannonsHeight), this.{24165}.Value.SightDistance, this.{24165}.Value.SightHeight, this.{24163}.X, this.{24163}.Z);
			Vector2 projection = Engine.GS.Camera.GetProjection(ref vector2);
			float num = 1f;
			Device gs = Engine.GS;
			Rectangle rectangle = new Marker(projection.X - 25f * num, projection.Y - 1f, 50f * num, 3f * num).ToRect();
			Color color = ((vector.Y == 0f) ? Color.Cyan : Color.LightCyan) * {24148};
			gs.Draw(AtlasObjs.whitepixel_1px, rectangle, color);
			Device gs2 = Engine.GS;
			rectangle = new Marker(projection.X - 25f * num, projection.Y - 1f, 50f * (1f - vector.X) * num, 3f * num).ToRect();
			color = Color.Gray * {24148};
			gs2.Draw(AtlasObjs.whitepixel_1px, rectangle, color);
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x000E133C File Offset: 0x000DF53C
		public static void DrawSightCircle(Vector2 {24149}, float {24150}, Color {24151}, float {24152}, bool {24153} = true)
		{
			Rectangle rectangle = {24153} ? CannonsController.p_sightMicro_red : CannonsController.p_sightMicro;
			rectangle.Height = (int)((float)rectangle.Height * (1f - {24152}));
			int height = rectangle.Height;
			Engine.GS.Draw(rectangle, {24149}, CannonsController.p_sightMicroCannon_centr, 0f, {24150}, {24151});
			rectangle = CannonsController.p_sightMicro_sp;
			rectangle.Height = (int)((float)rectangle.Height * {24152});
			rectangle.Y += height;
			Device gs = Engine.GS;
			Vector2 vector = {24149} + new Vector2(0f, (float)height * {24150});
			gs.Draw(rectangle, vector, CannonsController.p_sightMicroCannon_centr, 0f, {24150}, {24151});
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x00003100 File Offset: 0x00001300
		public void OnGun(int {24154})
		{
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000E13E6 File Offset: 0x000DF5E6
		public void Reset()
		{
			this.{24166} = 0f;
			this.{24171} = -10f;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x000E13FE File Offset: 0x000DF5FE
		public void ShowGunModeAnimation()
		{
			this.{24166} = 3000f;
			InGameSightUi.OnAction();
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x000E1410 File Offset: 0x000DF610
		private Vector2 {24155}()
		{
			Vector2 vector = this.{24161} ?? this.{24160};
			vector.X += 0.03141593f;
			if (Global.Camera.IsSpyglass)
			{
				vector.X -= 0.12f;
			}
			Geometry.AxisNorm(vector.X);
			if (float.IsNaN(vector.X))
			{
				throw new Exception();
			}
			return vector;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x000E148C File Offset: 0x000DF68C
		public static CannonLocation? GetNearNotEmptyBoard(Vector2 {24156})
		{
			if (Global.Player.UsedShip.Cannons.Count == 0)
			{
				return null;
			}
			float num = MathF.Atan2({24156}.Y, {24156}.X);
			num -= Global.Player.Rotation + 1.5707964f;
			{24156} = new Vector2(MathF.Cos(num), MathF.Sin(num));
			if ({24156}.Y < -0.7809f && (Global.Player.UsedShip.Cannons.IsBoardNotEmpty(CannonLocation.InFront) || Global.Player.UsedShip.Mortars.Count > 0))
			{
				return new CannonLocation?(CannonLocation.InFront);
			}
			if ({24156}.Y > 0.7809f)
			{
				if (!Global.Player.UsedShip.Cannons.IsBoardNotEmpty(CannonLocation.InBack))
				{
					if (!Global.Player.UsedShip.StaticInfo.MortarPorts.Any((CannonLocationInfo {24173}) => {24173}.Side == CannonLocation.InBack))
					{
						goto IL_FF;
					}
				}
				return new CannonLocation?(CannonLocation.InBack);
			}
			IL_FF:
			if ({24156}.X < 0f)
			{
				if (Global.Player.UsedShip.Cannons.IsBoardNotEmpty(CannonLocation.RightSide))
				{
					return new CannonLocation?(CannonLocation.RightSide);
				}
				return null;
			}
			else
			{
				if (Global.Player.UsedShip.Cannons.IsBoardNotEmpty(CannonLocation.LeftSide))
				{
					return new CannonLocation?(CannonLocation.LeftSide);
				}
				return null;
			}
		}

		// Token: 0x040017BE RID: 6078
		public static readonly Rectangle p_sightMicro = new Rectangle(1304, 1279, 128, 128);

		// Token: 0x040017BF RID: 6079
		public static readonly Rectangle p_sightMicro_red = new Rectangle(1304, 1410, 128, 128);

		// Token: 0x040017C0 RID: 6080
		public static readonly Rectangle p_sightMicro_sp = new Rectangle(1435, 1279, 128, 128);

		// Token: 0x040017C1 RID: 6081
		public static readonly Rectangle p_sightMicro_effect = new Rectangle(1435, 1410, 128, 128);

		// Token: 0x040017C2 RID: 6082
		public static readonly Rectangle p_simpleSightLineLeft = new Rectangle(1000, 1130, 39, 147);

		// Token: 0x040017C3 RID: 6083
		public static readonly Rectangle p_simpleSightLineRight = new Rectangle(1040, 1130, 39, 147);

		// Token: 0x040017C4 RID: 6084
		public static readonly Rectangle p_simpleSightCenter = new Rectangle(1090, 1225, 46, 52);

		// Token: 0x040017C5 RID: 6085
		public static readonly Rectangle p_gunMode_single = new Rectangle(931, 1128, 64, 64);

		// Token: 0x040017C6 RID: 6086
		public static readonly Rectangle p_gunMode_normal = new Rectangle(801, 1063, 64, 64);

		// Token: 0x040017C7 RID: 6087
		public static readonly Rectangle p_gunMode_all = new Rectangle(931, 1063, 64, 64);

		// Token: 0x040017C8 RID: 6088
		public static readonly Rectangle p_gunMode_rev = new Rectangle(866, 1063, 64, 64);

		// Token: 0x040017C9 RID: 6089
		public static readonly Vector2 p_sightMicroCannon_centr = new Vector2((float)(CannonsController.p_sightMicro.Width / 2), (float)(CannonsController.p_sightMicro.Height / 2));

		// Token: 0x040017CA RID: 6090
		public CannonsAttackMode PickedGunMode = CannonsAttackMode.AllNormal;

		// Token: 0x040017CB RID: 6091
		private float {24157};

		// Token: 0x040017CC RID: 6092
		private float {24158};

		// Token: 0x040017CD RID: 6093
		private SoftTrigger {24159} = new SoftTrigger(0f, 0.6f, 1.5f);

		// Token: 0x040017CE RID: 6094
		private Vector2 {24160};

		// Token: 0x040017CF RID: 6095
		private Vector2? {24161};

		// Token: 0x040017D0 RID: 6096
		private Vector2 {24162};

		// Token: 0x040017D1 RID: 6097
		private Vector3 {24163};

		// Token: 0x040017D2 RID: 6098
		private Vector2 {24164};

		// Token: 0x040017D3 RID: 6099
		public VolumetricSightShader VolumeSightRender;

		// Token: 0x040017D4 RID: 6100
		private Functions.HeightDist? {24165};

		// Token: 0x040017D5 RID: 6101
		private float {24166};

		// Token: 0x040017D6 RID: 6102
		private Rectangle {24167};

		// Token: 0x040017D7 RID: 6103
		private string[] {24168};

		// Token: 0x040017D8 RID: 6104
		private const float attackSwitchAnimationTtl_max = 3000f;

		// Token: 0x040017D9 RID: 6105
		private const float cannonGunFilterSwitchAnimationTtl_max = 2000f;

		// Token: 0x040017DA RID: 6106
		private Tlist<CannonCommon> {24169} = new Tlist<CannonCommon>(100);

		// Token: 0x040017DB RID: 6107
		private CannonLocation? {24170};

		// Token: 0x040017DC RID: 6108
		private float {24171} = -10f;
	}
}
