using System;
using TheraEngine.Interface.Controls;

namespace TheraEngine.Interface.Eventing
{
	// Token: 0x020000A2 RID: 162
	public class ScrollBarChangeEventArgs : EventArgs
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x00016286 File Offset: 0x00014486
		internal ScrollBarChangeEventArgs(ScrollBarControl {13063}, float {13064})
		{
			this.Sender = {13063};
			this.ScrollFactor = {13064};
		}

		// Token: 0x04000337 RID: 823
		public readonly ScrollBarControl Sender;

		// Token: 0x04000338 RID: 824
		public readonly float ScrollFactor;
	}
}
