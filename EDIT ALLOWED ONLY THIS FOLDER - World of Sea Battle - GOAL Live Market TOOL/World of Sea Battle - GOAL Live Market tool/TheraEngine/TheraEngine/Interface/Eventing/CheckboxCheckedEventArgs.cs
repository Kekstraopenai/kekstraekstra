using System;
using TheraEngine.Interface.Controls;

namespace TheraEngine.Interface.Eventing
{
	// Token: 0x0200009F RID: 159
	public class CheckboxCheckedEventArgs : EventArgs
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x00016244 File Offset: 0x00014444
		internal CheckboxCheckedEventArgs(CheckboxControl {13051}, bool {13052})
		{
			this.NewValue = {13052};
			this.Sender = {13051};
		}

		// Token: 0x0400032F RID: 815
		public readonly bool NewValue;

		// Token: 0x04000330 RID: 816
		public readonly CheckboxControl Sender;

		// Token: 0x04000331 RID: 817
		public bool UndoCheck;
	}
}
