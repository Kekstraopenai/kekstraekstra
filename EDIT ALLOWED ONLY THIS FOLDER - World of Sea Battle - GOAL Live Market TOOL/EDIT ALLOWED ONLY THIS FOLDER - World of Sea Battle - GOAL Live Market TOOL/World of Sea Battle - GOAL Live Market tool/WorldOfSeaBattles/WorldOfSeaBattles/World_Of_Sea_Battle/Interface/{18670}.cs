using System;
using TheraEngine;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000169 RID: 361
	internal class {18670} : {18637}
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x0004183F File Offset: 0x0003FA3F
		public override void Begin()
		{
			base.Begin();
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00041B14 File Offset: 0x0003FD14
		public override void Update(ref FrameTime {18671})
		{
			if ({19779}.CurrentInstance != null)
			{
				this.{18672} = true;
			}
			else if (this.{18672})
			{
				this.timeoutMs = 2000f;
				this.{18672} = false;
			}
			base.Update(ref {18671});
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0000744B File Offset: 0x0000564B
		protected override string BuildText()
		{
			return "";
		}

		// Token: 0x04000780 RID: 1920
		private bool {18672};
	}
}
