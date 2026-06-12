using System;
using TheraEngine;
using TheraEngine.Input;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000167 RID: 359
	internal class {18663} : {18637}
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x0004183F File Offset: 0x0003FA3F
		public override void Begin()
		{
			base.Begin();
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00041A6C File Offset: 0x0003FC6C
		public override void Update(ref FrameTime {18664})
		{
			if (InputHelper.NowMouseState.RightPressed || InputHelper.NowMouseState.LeftPressed)
			{
				this.{18666} += {18664}.secElapsed;
				if (this.{18666} > 1f)
				{
					this.{18665} = true;
				}
			}
			else if (this.{18665} && this.timeoutMs == 0f)
			{
				this.timeoutMs = 500f;
			}
			base.Update(ref {18664});
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0000744B File Offset: 0x0000564B
		protected override string BuildText()
		{
			return "";
		}

		// Token: 0x0400077D RID: 1917
		private bool {18665};

		// Token: 0x0400077E RID: 1918
		private float {18666};
	}
}
