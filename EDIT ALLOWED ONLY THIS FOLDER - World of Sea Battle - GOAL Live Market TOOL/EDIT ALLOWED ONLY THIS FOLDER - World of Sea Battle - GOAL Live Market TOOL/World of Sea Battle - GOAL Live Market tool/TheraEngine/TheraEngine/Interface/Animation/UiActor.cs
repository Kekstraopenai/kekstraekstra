using System;

namespace TheraEngine.Interface.Animation
{
	// Token: 0x020000DC RID: 220
	public sealed class UiActor : UiAction
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x0001F0CC File Offset: 0x0001D2CC
		public UiActor(UiControl {14194}, Action {14195})
		{
			{14194}.AddAction(this);
			this.{14198} = {14195};
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001F0E2 File Offset: 0x0001D2E2
		protected internal override bool Update(ref FrameTime {14196}, UiControl {14197})
		{
			this.{14198}();
			this.{14198} = null;
			return true;
		}

		// Token: 0x0400046D RID: 1133
		private Action {14198};
	}
}
