using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Interface.Eventing
{
	// Token: 0x020000A0 RID: 160
	public class ClickUiEventArgs : EventArgs
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x0001625A File Offset: 0x0001445A
		internal ClickUiEventArgs(UiControl {13055}, Vector2 {13056})
		{
			this.ClickPosition = {13056};
			this.Sender = {13055};
		}

		// Token: 0x04000332 RID: 818
		public readonly Vector2 ClickPosition;

		// Token: 0x04000333 RID: 819
		public readonly UiControl Sender;

		// Token: 0x04000334 RID: 820
		public bool DoNotHandling;
	}
}
