using System;
using System.Runtime.CompilerServices;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Grphics.Device;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x0200005A RID: 90
	internal sealed class ShipNpc : Npc, IClientShip
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000070D7 File Offset: 0x000052D7
		public override bool DetailPhysics
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00016B72 File Offset: 0x00014D72
		public override bool IsInBoarding
		{
			get
			{
				return {18139}.IsInBoardingUi(this.uID);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00016B7F File Offset: 0x00014D7F
		public ShipPartial GetClient
		{
			get
			{
				return this.Client;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00016B87 File Offset: 0x00014D87
		public HealthBarStyle StatusColor
		{
			get
			{
				return this.{16920}();
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00016B8F File Offset: 0x00014D8F
		public bool IsFirebrand
		{
			get
			{
				return this.FirebrandTargetUID != 0;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00016B9C File Offset: 0x00014D9C
		public bool MakeTransparentForMe
		{
			get
			{
				if (base.UsedShipNpc.Information.Descritpion == NpcType.PortPatrol && Session.Game.PatrolCannotInteropMe)
				{
					return true;
				}
				if (this.MapInfo.IsWorldmap)
				{
					if ((this.OwnerFlags.IsPeaceMode() && this.UidPlayerForCaper != Global.Player.uID) || (base.IsPlayerCaper && Session.Account.WorldFlag.IsPeaceMode()))
					{
						return true;
					}
					if (base.UsedShipNpc.Information.Descritpion == NpcType.Empire_Invasion && Session.Account.WorldFlag.IsPeaceMode())
					{
						return true;
					}
					if (Global.Player.IsStaticForAllNpcs && !base.IsPlayerCaper && this.FirebrandTargetUID == 0)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00016C5C File Offset: 0x00014E5C
		public OpenWorldFlag VisibleWorldFlags
		{
			get
			{
				if (base.IsPlayerCaper && this.MapInfo.IsEducationMap)
				{
					return OpenWorldFlag.Trader;
				}
				if (!base.IsPlayerCaper || !this.MapInfo.IsWorldmap)
				{
					return base.UsedShipNpc.Information.Extras.Flags;
				}
				return this.OwnerFlags;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00016CB4 File Offset: 0x00014EB4
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00016CBC File Offset: 0x00014EBC
		public bool IsShieldActive { get; set; }

		// Token: 0x060002A9 RID: 681 RVA: 0x00016CC8 File Offset: 0x00014EC8
		public ShipNpc()
		{
			this.Client = new ShipPartial(this);
			this.ClientWeaponsShooting = new CommonShotRenderer<CommonShotInfo>(new CommonCannonGunCallback<CommonShotInfo>(this.{16921}), delegate()
			{
			});
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00016D24 File Offset: 0x00014F24
		public override void ClearResources()
		{
			this.FirebrandTargetUID = 0;
			this.ApproximatelyFirebrandTimeoutMs = 0f;
			this.ProbablyWasDamagedByOtherPlayers = false;
			this.Client.CleanResources();
			this.ClientWeaponsShooting.Reset();
			this.TradingRoute = null;
			base.ClearResources();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00016D64 File Offset: 0x00014F64
		public void Initialize(NpcCreatePacket {16914}, bool {16915})
		{
			this.ClearResources();
			base.InternalInitialize({16914}, Global.Player.MapInfo, (Global.Player.MapInfo is MapForPassingInfo) ? Session.CurrentPassingSession.DiffCards : null);
			this.SetState({16914});
			this.FirstController.Set({16914}.ControllerState);
			this.UidPlayerForCaper = {16914}.UidPlayerForCaper;
			this.FirebrandTargetUID = {16914}.FirebrandTargetUID;
			this.AgressionToCurrentPlayer = {16914}.AgressionToPlayer;
			this.OwnerFlags = {16914}.OwnerFlags;
			this.OwnerName = {16914}.OwnerName;
			this.DisasterMode = {16914}.DisasterMode;
			this.CurrentAgressionTargetUID = {16914}.AgressionTargetUID;
			this.TradingRoute = (({16914}.TradingRouteId == 0) ? null : Gameplay.TradingRoutesInfo[(int){16914}.TradingRouteId]);
			this.IsShieldActive = {16914}.IsShieldActive;
			if ({16914}.FirebrandTargetUID != 0)
			{
				this.ApproximatelyFirebrandTimeoutMs = {16914}.FirebrandTimeoutMs;
			}
			this.Client.ClientInitialize(base.UsedShipNpc.Information.NpcName, {16915}, {16914});
			this.Client.Guild = {16914}.GuildInfo;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00016E81 File Offset: 0x00015081
		public new void Update(ref FrameTime {16916})
		{
			base.Update(ref {16916});
			this.Client.Update(ref {16916});
			{16916}.EvaluteTimerMs(ref this.ApproximatelyFirebrandTimeoutMs);
			if (base.IsPlayerCaper)
			{
				this.AgressionToCurrentPlayer = NpcAgression.No;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00016EB4 File Offset: 0x000150B4
		public void Render2D()
		{
			if (this.IsShieldActive)
			{
				Device gs = Engine.GS;
				Texture2D tex = AtlasGameGui.Texture.Tex;
				Vector2 vector = this.Client.Graphics2DPos - {18139}.c_protectChecked.HalfWidthHeight().X * Vector2.One + new Vector2(0f, -50f);
				Color color = Color.Violet * this.Client.Transparancy2D * 0.8f;
				gs.DrawCustomTexture(tex, {18139}.c_protectChecked, vector, color);
			}
			this.Client.Render2D(this.StatusColor, null);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00016F58 File Offset: 0x00015158
		public override void MakeDamage(in DamageData {16917}, int {16918})
		{
			if ({16917}.Flags.HasFlag(SpecificDamageFlags.IsHealthDamageReduced))
			{
				this.Client.healthBar.OnReducedDamageReceived({16917}.HealthDamage);
			}
			base.MakeDamage({16917}, {16918});
			if (base.IsDestroyedOrFlooding && {16918} == Global.Player.uID && base.UsedShipNpc.Information.Extras.Flags == OpenWorldFlag.Pirate)
			{
				EducationHelper.MakeFlag(EducationOnboarding.DestroyNpcBlack, true);
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00016FD7 File Offset: 0x000151D7
		public void ApplyMovePacket(OnShipControllerChangeMsg {16919})
		{
			Global.Network.CorrectPosition(this, {16919}.StartPosition.Position, Session.LastPing);
			this.FirstController.Set({16919}.ControllerCode);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00017006 File Offset: 0x00015206
		protected override void DestroyCallback()
		{
			FXEngine.ShipDeath(this);
			base.DestroyCallback();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00017014 File Offset: 0x00015214
		private HealthBarStyle {16920}()
		{
			if (this.IsAllyByCaper(Global.Player.uID))
			{
				return HealthBarStyle.Lime;
			}
			if (this.FirebrandTargetUID == Global.Player.uID)
			{
				return HealthBarHelper.GetStyle(Relation.Enemy);
			}
			if (this.MapInfo.IsPassingUi)
			{
				return HealthBarStyle.Red;
			}
			if (!this.MapInfo.IsWorldmap && !this.MapInfo.IsEnableArenaUi)
			{
				FractionID fraction = this.Client.Guild.Fraction;
			}
			return HealthBarStyle.Blue;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00015E24 File Offset: 0x00014024
		[CompilerGenerated]
		private void {16921}(CommonShotInfo {16922}, int {16923})
		{
			Global.Game.WorldInstance.LocalCommonCannonGunCallback(this, -1, {16922}, {16923}, this.ClientWeaponsShooting.Mode);
		}

		// Token: 0x04000223 RID: 547
		public readonly ShipPartial Client;

		// Token: 0x04000224 RID: 548
		public NpcAgression AgressionToCurrentPlayer;

		// Token: 0x04000225 RID: 549
		public int CurrentAgressionTargetUID = -1;

		// Token: 0x04000226 RID: 550
		public int FirebrandTargetUID;

		// Token: 0x04000227 RID: 551
		public float ApproximatelyFirebrandTimeoutMs;

		// Token: 0x04000228 RID: 552
		public string OwnerName;

		// Token: 0x04000229 RID: 553
		public OpenWorldFlag OwnerFlags;

		// Token: 0x0400022A RID: 554
		public bool ProbablyWasDamagedByOtherPlayers;

		// Token: 0x0400022B RID: 555
		public bool DisasterMode;

		// Token: 0x0400022C RID: 556
		[Nullable(2)]
		public TradingRouteInfo TradingRoute;

		// Token: 0x0400022D RID: 557
		[CompilerGenerated]
		private bool {16924};
	}
}
