using System;

namespace TheraEngine.Interface.Eventing
{
	// Token: 0x020000A3 RID: 163
	public class ShowToolTipEventArgs : EventArgs
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x0001629C File Offset: 0x0001449C
		internal ShowToolTipEventArgs(ToolTip {13067}, UiControl {13068})
		{
			this.Sender = {13067};
			this.Parent = {13068};
		}

		// Token: 0x04000339 RID: 825
		public readonly ToolTip Sender;

		// Token: 0x0400033A RID: 826
		public readonly UiControl Parent;

		// Token: 0x0400033B RID: 827
		public bool UndoAction;
	}
}
