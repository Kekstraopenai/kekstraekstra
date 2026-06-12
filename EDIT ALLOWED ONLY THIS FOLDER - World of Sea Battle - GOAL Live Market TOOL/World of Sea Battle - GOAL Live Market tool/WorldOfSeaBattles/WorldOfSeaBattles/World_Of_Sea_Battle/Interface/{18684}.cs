using System;
using TheraEngine;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200016C RID: 364
	internal class {18684} : {18637}
	{
		// Token: 0x0600086C RID: 2156 RVA: 0x00041C12 File Offset: 0x0003FE12
		public {18684}()
		{
			this.timeoutMs = 60000f;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00041C25 File Offset: 0x0003FE25
		public override void Begin()
		{
			this.{18686} = Session.EducState_ShipMendingHpCounter;
			base.Begin();
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00041C38 File Offset: 0x0003FE38
		public override void Update(ref FrameTime {18685})
		{
			if (Session.EducState_ShipMendingHpCounter - this.{18686} >= 55f || Global.Player.UsedShip.FirstHP.Summary >= Global.Player.UsedShip.MaxHp || Global.Settings.kb_Item1.IsDown)
			{
				base.OnCompleted();
			}
			base.Update(ref {18685});
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0000744B File Offset: 0x0000564B
		protected override string BuildText()
		{
			return "";
		}

		// Token: 0x04000785 RID: 1925
		private float {18686};
	}
}
