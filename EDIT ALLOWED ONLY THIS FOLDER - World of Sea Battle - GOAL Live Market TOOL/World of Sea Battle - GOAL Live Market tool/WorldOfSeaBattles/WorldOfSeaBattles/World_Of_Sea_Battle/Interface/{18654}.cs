using System;
using TheraEngine;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000165 RID: 357
	internal class {18654} : {18637}
	{
		// Token: 0x06000850 RID: 2128 RVA: 0x0004197F File Offset: 0x0003FB7F
		public {18654}()
		{
			this.timeoutMs = 20000f;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0004183F File Offset: 0x0003FA3F
		public override void Begin()
		{
			base.Begin();
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00041994 File Offset: 0x0003FB94
		public override void Update(ref FrameTime {18655})
		{
			if (Global.Camera.IsSpyglass)
			{
				this.{18656} = true;
			}
			if (this.{18656} && !Global.Camera.IsSpyglass)
			{
				this.timeoutMs = 2000f;
				this.{18656} = false;
			}
			base.Update(ref {18655});
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0000744B File Offset: 0x0000564B
		protected override string BuildText()
		{
			return "";
		}

		// Token: 0x0400077A RID: 1914
		private bool {18656};
	}
}
