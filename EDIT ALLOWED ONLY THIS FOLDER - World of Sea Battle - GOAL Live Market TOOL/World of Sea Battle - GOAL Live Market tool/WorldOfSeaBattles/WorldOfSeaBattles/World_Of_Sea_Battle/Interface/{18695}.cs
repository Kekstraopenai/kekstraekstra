using System;
using TheraEngine;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200016F RID: 367
	internal class {18695} : {18637}
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x0004197F File Offset: 0x0003FB7F
		public {18695}()
		{
			this.timeoutMs = 20000f;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00041EDF File Offset: 0x000400DF
		public override void Begin()
		{
			{18695}.Active = true;
			base.Begin();
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00041EED File Offset: 0x000400ED
		public override void Update(ref FrameTime {18696})
		{
			if (!this.{18697} && {17745}.CurrentInstance != null && Global.Game.IsMouseVisible)
			{
				this.{18697} = true;
			}
			if (this.{18697} && {17745}.CurrentInstance == null)
			{
				base.OnCompleted();
			}
			base.Update(ref {18696});
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0000744B File Offset: 0x0000564B
		protected override string BuildText()
		{
			return "";
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00041F2D File Offset: 0x0004012D
		public override void Dispose()
		{
			{18695}.Active = false;
			base.Dispose();
		}

		// Token: 0x04000788 RID: 1928
		private bool {18697};

		// Token: 0x04000789 RID: 1929
		public static bool Active;
	}
}
