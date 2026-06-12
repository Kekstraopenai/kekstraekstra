using System;
using TheraEngine;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200016B RID: 363
	internal class {18678} : {18637}
	{
		// Token: 0x06000868 RID: 2152 RVA: 0x00041BC2 File Offset: 0x0003FDC2
		public {18678}(int {18680})
		{
			this.{18683} = {18680};
			this.timeoutMs = 40000f;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00041BDC File Offset: 0x0003FDDC
		public override void Begin()
		{
			this.{18682} = Session.EducState_LootedShipsCount;
			base.Begin();
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00041BEF File Offset: 0x0003FDEF
		public override void Update(ref FrameTime {18681})
		{
			if (Session.EducState_LootedShipsCount - this.{18682} >= this.{18683})
			{
				base.OnCompleted();
			}
			base.Update(ref {18681});
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0000744B File Offset: 0x0000564B
		protected override string BuildText()
		{
			return "";
		}

		// Token: 0x04000783 RID: 1923
		private int {18682};

		// Token: 0x04000784 RID: 1924
		private int {18683};
	}
}
