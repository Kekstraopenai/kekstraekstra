using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000170 RID: 368
	internal class {18698} : {18637}
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x0004183F File Offset: 0x0003FA3F
		public override void Begin()
		{
			base.Begin();
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00041F3C File Offset: 0x0004013C
		public override void Update(ref FrameTime {18699})
		{
			if (!this.{18700})
			{
				ObservableTlist<Vector3> value = Global.Player.NearPort.VisualTips.FirstOrDefault((KeyValuePair<PortTipConnection, ObservableTlist<Vector3>> {18701}) => {18701}.Key == PortTipConnection.PirateTrader).Value;
				if (value != null && Global.Camera.RotateToTarget(value[0].XZ, {18699}.secElapsed))
				{
					this.{18700} = true;
				}
			}
			base.Update(ref {18699});
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0000744B File Offset: 0x0000564B
		protected override string BuildText()
		{
			return "";
		}

		// Token: 0x0400078A RID: 1930
		private bool {18700};
	}
}
