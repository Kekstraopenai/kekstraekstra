using System;
using System.Linq;
using Common;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000164 RID: 356
	internal class {18646} : {18637}
	{
		// Token: 0x0600084B RID: 2123 RVA: 0x00041811 File Offset: 0x0003FA11
		public {18646}(int {18648})
		{
			{18646}.IsFirstScenario = ({18648} == 0);
			this.{18651} = {18648};
			this.{18650} = 1000000f;
			this.{18652} = Session.EducState_GunShotCounter;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0004183F File Offset: 0x0003FA3F
		public override void Begin()
		{
			base.Begin();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00041848 File Offset: 0x0003FA48
		public override void Update(ref FrameTime {18649})
		{
			if (!this.{18653} && this.{18651} == 0)
			{
				ShipNpc shipNpc = Global.Game.WorldInstance.NpcList.FirstOrDefault<ShipNpc>();
				if (shipNpc != null && Global.Camera.RotateToTarget(shipNpc.Position, {18649}.secElapsed))
				{
					this.{18653} = true;
				}
			}
			if (Session.EducState_GunShotCounter - this.{18652} == 2)
			{
				this.{18650} = 3000f;
			}
			if (this.{18651} == 0 && this.{18650} != -1f)
			{
				{18649}.EvaluteTimerMs(ref this.{18650});
				if (this.{18650} == 0f && {17312}.CurrentInstance == null)
				{
					{19548}.CurrentInstance.ShowFalkonets = true;
					new {17312}(true, new {17464}[]
					{
						new {17464}(default(Rectangle), Local.onboarding_education_falkonetuse(Global.Settings.kb_Falkonet.KeyToString))
					});
					this.{18650} = -1f;
				}
			}
			if ({18646}.IsFirstScenario)
			{
				Global.Player.ResetSpeedTo2();
			}
			base.Update(ref {18649});
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0000744B File Offset: 0x0000564B
		protected override string BuildText()
		{
			return "";
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0004194F File Offset: 0x0003FB4F
		public override void Dispose()
		{
			{18646}.IsFirstScenario = false;
			Global.Settings.SelectedCannonBalls[CannonLocation.RightSide] = 1;
			Global.Settings.SelectedCannonBalls[CannonLocation.LeftSide] = 1;
			base.Dispose();
		}

		// Token: 0x04000775 RID: 1909
		public static bool IsFirstScenario;

		// Token: 0x04000776 RID: 1910
		private float {18650};

		// Token: 0x04000777 RID: 1911
		private int {18651};

		// Token: 0x04000778 RID: 1912
		private int {18652};

		// Token: 0x04000779 RID: 1913
		private bool {18653};
	}
}
