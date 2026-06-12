using System;
using Microsoft.Xna.Framework;
using TheraEngine;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000166 RID: 358
	internal class {18657} : {18637}
	{
		// Token: 0x06000854 RID: 2132 RVA: 0x000419E1 File Offset: 0x0003FBE1
		public {18657}(bool {18659})
		{
			this.{18661} = Vector2.Distance(Global.Player.Position, Global.Player.NearPort.EntryPos);
			this.{18662} = {18659};
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0004183F File Offset: 0x0003FA3F
		public override void Begin()
		{
			base.Begin();
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00041A14 File Offset: 0x0003FC14
		public override void Update(ref FrameTime {18660})
		{
			float num = Vector2.Distance(Global.Player.Position, Global.Player.NearPort.EntryPos);
			if (this.{18662} && num < this.{18661} / 2f)
			{
				base.OnCompleted();
			}
			base.Update(ref {18660});
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0000744B File Offset: 0x0000564B
		protected override string BuildText()
		{
			return "";
		}

		// Token: 0x0400077B RID: 1915
		private float {18661};

		// Token: 0x0400077C RID: 1916
		private bool {18662};
	}
}
