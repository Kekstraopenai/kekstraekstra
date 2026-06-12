using System;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Collections;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x0200048A RID: 1162
	internal class FalkonetsController : IInGameSightUI
	{
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x000E182A File Offset: 0x000DFA2A
		private static float AnimationInterval
		{
			get
			{
				return 110f;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x000E1834 File Offset: 0x000DFA34
		private Tlist<FalkonetShotInfoRemote> activeFalconets
		{
			get
			{
				return Global.Player.UsedShip.StaticInfo.FetchActiveFalkonet(Global.Player, Gameplay.BallsInfo.FromID((int)Session.SelectedFalkonetsInfo.ID), this.{24192}, this.{24194} + 10f);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x000E1880 File Offset: 0x000DFA80
		public float RenderAmount
		{
			get
			{
				return Math.Min(1f, this.{24193} * 3f);
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x000E1898 File Offset: 0x000DFA98
		private bool useAutoSighting
		{
			get
			{
				return Global.Settings.EnableAutoFalkonets;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x000E18A4 File Offset: 0x000DFAA4
		private bool smokeBombsUsed
		{
			get
			{
				return Session.SelectedFalkonetsInfo.ID == 14;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x000E18B4 File Offset: 0x000DFAB4
		private bool keyIsDown
		{
			get
			{
				return (InputHelper.NowMouseState.RightPressed && Global.Settings.RightMouseAction == RightMouseKeyAction.FalkonetGun) || Global.Settings.kb_Falkonet.IsDown;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x000E18E0 File Offset: 0x000DFAE0
		private bool keyIsReady
		{
			get
			{
				if (!this.smokeBombsUsed && !this.useAutoSighting)
				{
					return (InputHelper.RightWasClicked && Global.Settings.RightMouseAction == RightMouseKeyAction.FalkonetGun) || Global.Settings.kb_Falkonet.IsClick;
				}
				return (InputHelper.RightWasReleased && Global.Settings.RightMouseAction == RightMouseKeyAction.FalkonetGun) || Global.Settings.kb_Falkonet.IsRelease;
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000E195C File Offset: 0x000DFB5C
		public void Update(ref FrameTime {24178}, bool {24179})
		{
			Vector2 vector = Engine.GS.Camera.Direction.XZNormal();
			this.{24192}.X = vector.X;
			this.{24192}.Z = vector.Y;
			this.{24192}.Y = Math.Max(0.2f, Math.Min(1f, Engine.GS.Camera.Direction.Y * 1.5f + 1f));
			this.{24194} = 0f;
			if (this.{24191})
			{
				if (this.smokeBombsUsed)
				{
					Ship ship = this.{24184}();
					if (ship != null)
					{
						this.{24185}(ship);
					}
				}
				else if (this.useAutoSighting)
				{
					if (this.{24195} >= FalkonetsController.AnimationInterval * (this.{24195} != 0f))
					{
						if (this.{24196} < this.activeFalconets.Size)
						{
							this.{24196}++;
							Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UIButtonInterop, 0.03f, 4f);
						}
						this.{24195} -= FalkonetsController.AnimationInterval;
					}
					this.{24195} += {24178}.msElapsed;
				}
			}
			this.{24191} = ({24179} && this.keyIsDown && Session.WReload.FalkonetsReloadingLeftSec == 0f);
			if (!{24179} && this.keyIsReady && Session.WReload.FalkonetsReloadingLeftSec == 0f && Global.Player.IsMarchingMode && Session.Account.Rang <= 4)
			{
				{19994}.Me({19988}.Info, Local.ShipCurrentPlayer_0, Array.Empty<object>());
			}
			if ({24179} && this.keyIsReady && Session.WReload.FalkonetsReloadingLeftSec == 0f)
			{
				Global.Player.RunFalkonets(this.{24192}, this.{24192}.Y * Session.SelectedFalkonetsInfo.DistanceFactor, (int)Session.SelectedFalkonetsInfo.ID, 0);
				this.{24196} = 0;
			}
			if (this.{24191})
			{
				this.{24193} += {24178}.secElapsed;
				InGameSightUi.OnAction();
				return;
			}
			this.{24196} = 0;
			this.{24193} = 0f;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000E1B98 File Offset: 0x000DFD98
		public void TryRunBoardingHooks(Ship {24180})
		{
			if (Session.WReload.BoardingHookReloadingLeftSec == 0f)
			{
				Vector2 vector = ({24180}.Position - Global.Player.Position).Normal();
				float num = Geometry.AxisNorm(MathF.Atan2(vector.Y, vector.X) - Global.Player.Rotation);
				if (num > 0f && num < 3.1415927f)
				{
					num = 1.5707964f;
				}
				else
				{
					num = -1.5707964f;
				}
				vector = Geometry.FastSinCos(num + Global.Player.Rotation);
				Global.Player.RunFalkonets(vector.X0Y(), 100f, 17, {24180}.uID);
			}
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00003100 File Offset: 0x00001300
		public void Render2DAnyMode(float {24181})
		{
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000E1C44 File Offset: 0x000DFE44
		public void Render2D(float {24182})
		{
			FalkonetsController.<>c__DisplayClass24_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.elementsTransparancy = {24182};
			if (this.{24191})
			{
				if (this.smokeBombsUsed)
				{
					this.{24187}(ref CS$<>8__locals1);
					return;
				}
				if (this.useAutoSighting && this.{24196} > 0)
				{
					this.{24189}(ref CS$<>8__locals1);
				}
			}
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000E1C94 File Offset: 0x000DFE94
		public void Render3D(float {24183})
		{
			if (Session.SelectedFalkonetsInfo.ID == 14 && this.{24191})
			{
				float scale = {24183} * ((Session.WReload.FalkonetsReloadingLeftSec == 0f) ? 1f : 0.4f);
				Color color = InGameSightUi.GetDarkCyanLight * scale * 5f;
				Vector2 vector = this.{24192}.XZ() * this.{24192}.Y * Session.SelectedFalkonetsInfo.DistanceFactor;
				Global.Render.ItemsShader.RenderCircle(new Vector3(vector.X, -100f, vector.Y), 8f, 10f, color, GPUCircleType.SoftCircle, null);
				Vector2 {24264} = vector;
				float {24265} = 10f;
				Color color2 = color * 2f;
				MortarController.AreaSightHelperHeight({24264}, {24265}, {24183}, color2);
			}
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00003100 File Offset: 0x00001300
		public void Reset()
		{
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000E1D6C File Offset: 0x000DFF6C
		private Ship {24184}()
		{
			Ship result = null;
			float num = 350f;
			Vector2 vector = Engine.GS.UIArea.HalfWidthHeight();
			foreach (Ship ship in Global.Game.WorldInstance.EnumerateShips(true, true, false))
			{
				Vector3 position3D = ship.Position3D;
				if (((IClientShip)ship).GetClient.IsVisibleWithOcclusionQueryAndCorpusTransparancy && ((IClientShip)ship).GetClient.IsVisible && ((IClientShip)ship).GetClient.StatusColor != HealthBarStyle.Lime)
				{
					Vector2 projection = Global.Camera.GetProjection(ref position3D);
					float num2;
					Vector2.Distance(ref projection, ref vector, out num2);
					if (num2 < num)
					{
						num = num2;
						result = ship;
					}
				}
			}
			return result;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x000E1E40 File Offset: 0x000E0040
		[CompilerGenerated]
		private void {24185}(Ship {24186})
		{
			Vector2 {11447} = {24186}.Position - Global.Player.Position;
			if (Vector2.Dot(this.{24192}.XZ(), {11447}.Normal()) > 0.97f && {11447}.Length() < Session.SelectedFalkonetsInfo.DistanceFactor + 10f)
			{
				this.{24194} = 1f;
				float num = Math.Max(20f, {11447}.Length());
				ref ShipPositionInfo ptr = {24186}.ReconstructPosition(500f + 1000f * num / Session.SelectedFalkonetsInfo.Speed, {24186}.GetShipPositionInfo);
				num *= 1.05f;
				Vector2 vector = (ptr.Position - Global.Player.Position).Normal();
				this.{24192}.X = vector.X;
				this.{24192}.Z = vector.Y;
				this.{24192}.Y = Math.Min(1f, num / Session.SelectedFalkonetsInfo.DistanceFactor);
			}
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000E1F48 File Offset: 0x000E0148
		[CompilerGenerated]
		private void {24187}(ref FalkonetsController.<>c__DisplayClass24_0 {24188})
		{
			Rectangle {11433} = new Rectangle(1434, 1409, 130, 130);
			Rectangle {11433}2 = new Rectangle(735, 464, 130, 130);
			float num = this.{24192}.Y * Session.SelectedFalkonetsInfo.DistanceFactor;
			Vector3 vector = Global.Player.Position3D + this.{24192} * new Vector3(1f, 0f, 1f) * num;
			Vector2 projectionSmoothed = Global.Camera.GetProjectionSmoothed(ref vector);
			Color color = Session.SelectedFalkonetsInfo.IsBoardingHook ? Color.OrangeRed : Color.Pink;
			float num2 = 1f - 0.5f * num / 140f;
			Device gs = Engine.GS;
			Vector2 vector2 = {11433}2.HalfWidthHeight();
			gs.Draw({11433}2, projectionSmoothed, vector2, 0f, num2 * 0.3f, color);
			if (this.{24194} == 1f)
			{
				for (int i = 0; i < 3; i++)
				{
					float num3 = ((float)i / 3f + this.{24193}) % 1f;
					float scale = MathF.Sqrt(4f * num3 * (1f - num3));
					Device gs2 = Engine.GS;
					vector2 = {11433}.HalfWidthHeight();
					float {14558} = 0f;
					float {14559} = num3;
					Color color2 = Color.White * {24188}.elementsTransparancy * scale;
					gs2.Draw({11433}, projectionSmoothed, vector2, {14558}, {14559}, color2);
				}
			}
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000E20C4 File Offset: 0x000E02C4
		[CompilerGenerated]
		private void {24189}(ref FalkonetsController.<>c__DisplayClass24_0 {24190})
		{
			float num = this.{24192}.Y * Session.SelectedFalkonetsInfo.DistanceFactor;
			int num2 = Global.Camera.IsSpyglass ? 1 : this.activeFalconets.Size;
			float num3 = (num2 == 1) ? 1.5f : ((num2 >= 8) ? 1f : 1.3f);
			Color color = Color.White * ((num2 >= 8) ? 0.7f : 0.9f) * {24190}.elementsTransparancy;
			float {14559} = 0.1f * num3;
			Matrix matrix;
			Global.Player.Transform.CreateWorldMatrix(out matrix);
			int num4 = 0;
			while (num4 < this.{24196} && num4 < this.activeFalconets.Size)
			{
				Vector3 value = Vector3.Transform(this.activeFalconets[num4].StartPosition, matrix);
				float num5 = Geometry.Saturate(0.3f - Math.Abs(Engine.GS.Camera.Rotation.X));
				float num6 = 0.66f - num5;
				float ballHeight = Functions.GetBallHeight(num6, new Functions.HeightDist
				{
					SightDistance = num,
					SightHeight = 1f
				}, 0f);
				Vector3 vector = value + this.{24192} * new Vector3(1f, 0f, 1f) * num * num6 + new Vector3(0f, ballHeight + 0.5f, 0f);
				Vector2 projectionSmoothed = Global.Camera.GetProjectionSmoothed(ref vector);
				Engine.GS.Draw(CannonsController.p_sightMicro, projectionSmoothed, CannonsController.p_sightMicroCannon_centr, 0f, {14559}, color);
				num4++;
			}
		}

		// Token: 0x040017E2 RID: 6114
		private bool {24191};

		// Token: 0x040017E3 RID: 6115
		private Vector3 {24192};

		// Token: 0x040017E4 RID: 6116
		private float {24193};

		// Token: 0x040017E5 RID: 6117
		private float {24194} = 100f;

		// Token: 0x040017E6 RID: 6118
		private float {24195};

		// Token: 0x040017E7 RID: 6119
		private int {24196};
	}
}
