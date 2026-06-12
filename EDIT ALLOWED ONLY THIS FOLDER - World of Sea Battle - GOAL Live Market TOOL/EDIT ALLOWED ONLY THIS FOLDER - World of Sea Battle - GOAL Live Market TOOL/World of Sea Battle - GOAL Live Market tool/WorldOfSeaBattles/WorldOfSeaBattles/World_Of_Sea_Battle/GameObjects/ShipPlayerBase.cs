using System;
using System.Runtime.CompilerServices;
using Common.Game;
using Common.Resources;
using TheraEngine;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x02000064 RID: 100
	internal abstract class ShipPlayerBase : Player
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00016B72 File Offset: 0x00014D72
		public override bool IsInBoarding
		{
			get
			{
				return {18139}.IsInBoardingUi(this.uID);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0001ADF2 File Offset: 0x00018FF2
		public ShipPartial GetClient
		{
			get
			{
				return this.Client;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0001ADFA File Offset: 0x00018FFA
		public override bool EnableUpgradeWear
		{
			get
			{
				return !this.MapInfo.IsReducedFarDistView || Session.CurrentArenaSession.ModeInfo.ModeEnum > ArenaMode.DuelTraning;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0001AE1D File Offset: 0x0001901D
		public ShipPlayerBase()
		{
			this.Client = new ShipPartial(this);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001AE31 File Offset: 0x00019031
		public override void ClearResources()
		{
			this.Client.CleanResources();
			this.{17031} = 0;
			base.ClearResources();
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001AE4B File Offset: 0x0001904B
		protected void Initialize(int {17022}, ShipPositionInfo {17023}, WorldMapInfo {17024})
		{
			this.ClearResources();
			base.EvMapTeleport += this.{17027};
			base.InternalInitialize({17023}, {17022}, {17024});
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001AE70 File Offset: 0x00019070
		public new virtual void Update(ref FrameTime {17025})
		{
			base.Update(ref {17025});
			if (this.{17030} > 1f)
			{
				this.{17030} -= {17025}.msElapsed;
			}
			if (this.FirstController.AxisStateCode != this.{17031} && !this.UsedShip.StaticInfo.IsBalloon)
			{
				this.{17031} = this.FirstController.AxisStateCode;
				if (this.{17030} <= 1f)
				{
					Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.WoodCreak, base.Transform.Translation, 1f, false);
					this.{17030} += (float)Rand.RangeInt(600, 1200);
				}
			}
			this.Client.Update(ref {17025});
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001AF32 File Offset: 0x00019132
		public virtual void ForceUpdateShipEffects()
		{
			base.CheckShip(true);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001AF3B File Offset: 0x0001913B
		protected override void OnShipUpdated()
		{
			this.Client.UpdateModel();
			base.OnShipUpdated();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00003100 File Offset: 0x00001300
		protected virtual void ehUnselectMap(Ship {17026})
		{
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0001AF4E File Offset: 0x0001914E
		[NullableContext(1)]
		[CompilerGenerated]
		private void {17027}(Ship {17028}, WorldMapInfo {17029})
		{
			this.ehUnselectMap(this);
		}

		// Token: 0x04000278 RID: 632
		private float {17030};

		// Token: 0x04000279 RID: 633
		private int {17031};

		// Token: 0x0400027A RID: 634
		public readonly ShipPartial Client;
	}
}
