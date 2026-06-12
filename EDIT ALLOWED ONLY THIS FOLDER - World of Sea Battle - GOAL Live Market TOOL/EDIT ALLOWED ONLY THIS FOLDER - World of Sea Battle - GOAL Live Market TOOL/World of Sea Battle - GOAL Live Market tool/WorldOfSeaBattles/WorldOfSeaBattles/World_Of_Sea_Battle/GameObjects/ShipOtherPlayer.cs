using System;
using System.Runtime.CompilerServices;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x0200005C RID: 92
	internal sealed class ShipOtherPlayer : ShipPlayerBase, IClientShip
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00017097 File Offset: 0x00015297
		public override PlayerAccount AccountConnection
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000070D7 File Offset: 0x000052D7
		public override bool DetailPhysics
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0001709A File Offset: 0x0001529A
		public override int MirrorEngageInPortBattlePortID
		{
			get
			{
				if (!this.VisualFlags.HasFlag(ShipVisualStatusFlags.EngageInPb))
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000170B7 File Offset: 0x000152B7
		public RemotePlayerDynamicInfo RemoteInfo
		{
			get
			{
				return (RemotePlayerDynamicInfo)this.UsedShip;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002BA RID: 698 RVA: 0x000170C4 File Offset: 0x000152C4
		public bool HideNickname
		{
			get
			{
				return (this.RemoteInfo.Flags.HideNicknameAndGuild(this) && !this.IsContainsPlayerGuild && !Session.IsShipContainsPlayerGroup(this.uID) && (Session.Guild == null || !Session.Guild.IsTrusted(this.Client.Guild.Tag, false))) || (this.MapInfo.IsEnableArenaUi && Session.CurrentArenaSession != null && Session.CurrentArenaSession.ModeInfo.HideNames);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00017144 File Offset: 0x00015344
		public bool IsDecorationVisible
		{
			get
			{
				Vector3 position3D = base.Position3D;
				Vector3 position = Engine.GS.Camera.Position;
				return position3D.X <= position.X + 350f && position3D.Y <= position.Y + 350f && position3D.X >= position.X - 350f && position3D.Y >= position.Y - 350f && ref position3D.DTest(ref position, 350f);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000171C8 File Offset: 0x000153C8
		public uint AccountSID
		{
			get
			{
				return this.RemoteInfo.AccountSID;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000171D5 File Offset: 0x000153D5
		public bool IsContainsPlayerGuild
		{
			get
			{
				return Session.Guild != null && Session.Guild.Tag == this.Client.Guild.Tag;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002BE RID: 702 RVA: 0x000171FF File Offset: 0x000153FF
		public bool FullDisablePeaceMode
		{
			get
			{
				return this.VisualFlags.HasFlag(ShipVisualStatusFlags.PFDisallowedByWar);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00017217 File Offset: 0x00015417
		public bool HasSmugglingQuest
		{
			get
			{
				return this.VisualFlags.HasFlag(ShipVisualStatusFlags.SmugglingQuest);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0001722F File Offset: 0x0001542F
		public bool HasWantedLevel
		{
			get
			{
				return this.VisualFlags.HasFlag(ShipVisualStatusFlags.HavingWantedLevel);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00017248 File Offset: 0x00015448
		public bool IsPeace
		{
			get
			{
				return this.RemoteInfo.Flags.IsPeaceMode() && !this.FullDisablePeaceMode;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00017267 File Offset: 0x00015467
		public HealthBarStyle StatusColor
		{
			get
			{
				return this.{16936}();
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00017270 File Offset: 0x00015470
		public bool MakeTransparentForMe
		{
			get
			{
				return this.AccountSID != 1U && ((this.IsPeace && !this.FullDisablePeaceMode) || (this.MapInfo.IsWorldmap && Session.Account.WorldFlag.IsPeaceMode() && (!this.FullDisablePeaceMode || Session.Account.IsPeaceActivated)) || this.MirrorEngageInPortBattlePortID != -1 != Session.EngagingInPortBattle > PbsBatlleSide.None || (base.AllowEnteringPort || this.IsStatic || this.ProtectionSafeZoneTimeout > 0f) || (this.MirrorEngageInPortBattlePortID == -1 && Gameplay.IsPlayerPeaceByRank(Session.Account.Rang, this.RemoteInfo.VisibleAccRank, this.MapInfo, base.IsOutsideSeam || Global.Player.IsOutsideSeam)));
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00017348 File Offset: 0x00015548
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00017350 File Offset: 0x00015550
		public bool AggressorFlag { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00017359 File Offset: 0x00015559
		public override float PlayerMarchingModeBonusBySkill
		{
			get
			{
				return this.{16941}.RemoteMarchingBonusSkill;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00017366 File Offset: 0x00015566
		public override bool PlayerDisableDestructionTilt
		{
			get
			{
				return this.{16941}.PlayerDisableDestructionTilt;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00017373 File Offset: 0x00015573
		protected override ShipDynamicInfo AskActualShip
		{
			get
			{
				return this.{16941};
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001737B File Offset: 0x0001557B
		public ShipOtherPlayer()
		{
			this.ClientWeaponsShooting = new CommonShotRenderer<CommonShotInfo>(new CommonCannonGunCallback<CommonShotInfo>(this.{16937}), delegate()
			{
			});
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000173B9 File Offset: 0x000155B9
		public override void ClearResources()
		{
			base.ClearResources();
			this.ClientWeaponsShooting.Reset();
			this.IsDecoration = false;
			this.AggressorFlag = false;
			this.{16942} = 0f;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000173E8 File Offset: 0x000155E8
		public void Initialize(RemotePlayerDynamicInfo {16926}, PlayerCreatePacket {16927}, WorldMapInfo {16928}, bool {16929})
		{
			this.{16941} = {16926};
			base.Initialize({16927}.uID, {16927}.PositionInfo, {16928});
			this.Client.ClientInitialize({16927}.Name, {16929}, {16927});
			this.Client.Guild = {16927}.GuildInfo;
			this.VisualFlags = {16927}.VisualFlags;
			this.ProtectionSafeZoneTimeout = {16927}.ProtectionSafeZoneTimeout;
			this.safeZoneCanBeesetted = true;
			this.SetAgressorFlag({16927}.AggressorFlag);
			this.SetState({16927});
			this.FirstController.Reset();
			this.FirstController.Set({16927}.ControlCode);
			base.DebugEnabled = {16927}.DebugEnabled;
			this.basicCapacitySpeedFactor = {16927}.CurrentCapacitySpeedFactor;
			this.physicsBody.CanUseWheel = {16927}.CanUseWheel;
			if (Global.Game.GetCurrentSceneName != GameSceneName.Entry)
			{
				this.Client.SwitchSailesState();
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000174C3 File Offset: 0x000156C3
		public override void Update(ref FrameTime {16930})
		{
			base.Update(ref {16930});
			if ({16930}.EvaluteTimerSec2(ref this.{16942}))
			{
				this.SetAgressorFlag(false);
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000174E4 File Offset: 0x000156E4
		public override void MakeDamage(in DamageData {16931}, int {16932})
		{
			if ({16931}.SourcePawnOrShipUID == Global.Player.uID)
			{
				this.{16942} = 120f;
			}
			if ({16931}.Flags.HasFlag(SpecificDamageFlags.IsHealthDamageReduced))
			{
				this.Client.healthBar.OnReducedDamageReceived({16931}.HealthDamage);
			}
			base.MakeDamage({16931}, {16932});
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00017548 File Offset: 0x00015748
		public void Render2D()
		{
			if (this.IsDecoration)
			{
				return;
			}
			HealthBarStyle statusColor = this.StatusColor;
			ShipOtherPlayer.statusIconsTemp.Clear();
			if (!base.AllowEnteringPort && !this.IsStatic && this.ProtectionSafeZoneTimeout <= 0f)
			{
				if (this.HasSmugglingQuest)
				{
					ShipOtherPlayer.statusIconsTemp.Add(AtlasObjs.с_smugglingQuestIcon);
				}
				if (this.HasWantedLevel)
				{
					ShipOtherPlayer.statusIconsTemp.Add(AtlasObjs.p_pvpMarkerBlack);
				}
			}
			this.Client.Render2D(statusColor, ShipOtherPlayer.statusIconsTemp);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000175CB File Offset: 0x000157CB
		public void SetAgressorFlag(bool {16933})
		{
			this.AggressorFlag = {16933};
			this.{16942} = ({16933} ? 120f : 0f);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		protected override void OnRespawn()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000175E9 File Offset: 0x000157E9
		protected override void DestroyCallback()
		{
			FXEngine.ShipDeath(this);
			base.DestroyCallback();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000175F7 File Offset: 0x000157F7
		protected override void ehUnselectMap(Ship {16934})
		{
			base.ehUnselectMap({16934});
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00016FD7 File Offset: 0x000151D7
		public void ApplySetControlPacket(OnShipControllerChangeMsg {16935})
		{
			Global.Network.CorrectPosition(this, {16935}.StartPosition.Position, Session.LastPing);
			this.FirstController.Set({16935}.ControllerCode);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00017600 File Offset: 0x00015800
		private HealthBarStyle {16936}()
		{
			if (base.UsedShipPlayer.CraftFrom.ID == 51 || this.IsDecoration)
			{
				return HealthBarStyle.Blue;
			}
			if (this.HideNickname)
			{
				if (!this.AggressorFlag)
				{
					return HealthBarHelper.GetStyle(Relation.Neutral);
				}
				return HealthBarStyle.Red;
			}
			else
			{
				if (Session.IsShipContainsPlayerGroup(this.uID) || (Session.Guild != null && this.MapInfo.IsWorldmap && Session.Guild.IsTrusted(this.Client.Guild.Tag, false)))
				{
					return HealthBarHelper.GetStyle(Relation.Ally);
				}
				if (!this.MapInfo.IsWorldmap)
				{
					return HealthBarHelper.GetStyle(Relation.Enemy);
				}
				if (!this.AggressorFlag)
				{
					Decorator game = Session.Game;
					return HealthBarHelper.GetStyle(FractionAPI.GeneralStatusWith(game, this.Client.Guild, this.RemoteInfo.Reputations));
				}
				return HealthBarStyle.Red;
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00015E24 File Offset: 0x00014024
		[CompilerGenerated]
		private void {16937}(CommonShotInfo {16938}, int {16939})
		{
			Global.Game.WorldInstance.LocalCommonCannonGunCallback(this, -1, {16938}, {16939}, this.ClientWeaponsShooting.Mode);
		}

		// Token: 0x04000230 RID: 560
		public bool IsDecoration;

		// Token: 0x04000231 RID: 561
		public ShipVisualStatusFlags VisualFlags;

		// Token: 0x04000232 RID: 562
		[CompilerGenerated]
		private bool {16940};

		// Token: 0x04000233 RID: 563
		private RemotePlayerDynamicInfo {16941};

		// Token: 0x04000234 RID: 564
		private float {16942};

		// Token: 0x04000235 RID: 565
		private static Tlist<Rectangle> statusIconsTemp = new Tlist<Rectangle>();
	}
}
