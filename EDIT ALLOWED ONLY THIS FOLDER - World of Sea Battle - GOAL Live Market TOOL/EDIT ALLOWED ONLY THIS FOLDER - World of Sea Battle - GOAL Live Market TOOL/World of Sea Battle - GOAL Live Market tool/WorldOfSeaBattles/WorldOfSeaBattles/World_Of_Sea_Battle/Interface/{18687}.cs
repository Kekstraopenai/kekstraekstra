using System;
using Common.Resources;
using TheraEngine;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200016D RID: 365
	internal class {18687} : {18637}
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x00041C9C File Offset: 0x0003FE9C
		public {18687}()
		{
			Global.Settings.SelectedCannonBalls[CannonLocation.RightSide] = 1;
			Global.Settings.SelectedCannonBalls[CannonLocation.LeftSide] = 1;
			this.timeoutMs = 40000f;
			this.{18689} = Global.Settings.SelectedCannonBalls.Value;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0004183F File Offset: 0x0003FA3F
		public override void Begin()
		{
			base.Begin();
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00041CF1 File Offset: 0x0003FEF1
		public override void Update(ref FrameTime {18688})
		{
			if (this.{18689} != Global.Settings.SelectedCannonBalls.Value)
			{
				this.timeoutMs = 3000f;
				this.{18689} = Global.Settings.SelectedCannonBalls.Value;
			}
			base.Update(ref {18688});
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0000744B File Offset: 0x0000564B
		protected override string BuildText()
		{
			return "";
		}

		// Token: 0x04000786 RID: 1926
		private int {18689};
	}
}
