using System;
using Microsoft.Xna.Framework;
using TheraEngine;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Effects;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200016A RID: 362
	internal class {18673} : {18637}
	{
		// Token: 0x06000864 RID: 2148 RVA: 0x00041B47 File Offset: 0x0003FD47
		public {18673}(Vector2 {18675})
		{
			this.targetPosition = {18675};
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0004183F File Offset: 0x0003FA3F
		public override void Begin()
		{
			base.Begin();
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00041B58 File Offset: 0x0003FD58
		public override void Update(ref FrameTime {18676})
		{
			float num = Vector2.Distance(Global.Player.Position, this.targetPosition);
			if (num < 42f)
			{
				Global.Player.ResetSpeedTo1();
			}
			if (num < 30f)
			{
				base.OnCompleted();
			}
			if (num < 120f && !this.{18677})
			{
				this.{18677} = true;
				new WhalesFormWaterFSEffect(Global.Player);
			}
			base.Update(ref {18676});
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0000744B File Offset: 0x0000564B
		protected override string BuildText()
		{
			return "";
		}

		// Token: 0x04000781 RID: 1921
		public readonly Vector2 targetPosition;

		// Token: 0x04000782 RID: 1922
		private bool {18677};
	}
}
