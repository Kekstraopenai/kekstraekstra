using System;
using TheraEngine.Interface.Controls;

namespace TheraEngine.Interface.Eventing
{
	// Token: 0x020000A1 RID: 161
	public class ProgressBarChangeEventArgs : EventArgs
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x00016270 File Offset: 0x00014470
		internal ProgressBarChangeEventArgs(ProgressBar {13059}, float {13060})
		{
			this.NewValue = {13060};
			this.Sender = {13059};
		}

		// Token: 0x04000335 RID: 821
		public readonly float NewValue;

		// Token: 0x04000336 RID: 822
		public readonly ProgressBar Sender;
	}
}
