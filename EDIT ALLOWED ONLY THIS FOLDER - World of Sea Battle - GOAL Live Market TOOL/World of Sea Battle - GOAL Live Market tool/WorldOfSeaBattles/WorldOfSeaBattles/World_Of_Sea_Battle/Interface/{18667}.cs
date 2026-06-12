using System;
using TheraEngine;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000168 RID: 360
	internal class {18667} : {18637}
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x0004183F File Offset: 0x0003FA3F
		public override void Begin()
		{
			base.Begin();
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00041AE1 File Offset: 0x0003FCE1
		public override void Update(ref FrameTime {18668})
		{
			if ({22913}.CurrentInstance != null)
			{
				this.{18669} = true;
			}
			else if (this.{18669})
			{
				this.timeoutMs = 2000f;
				this.{18669} = false;
			}
			base.Update(ref {18668});
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0000744B File Offset: 0x0000564B
		protected override string BuildText()
		{
			return "";
		}

		// Token: 0x0400077F RID: 1919
		private bool {18669};
	}
}
