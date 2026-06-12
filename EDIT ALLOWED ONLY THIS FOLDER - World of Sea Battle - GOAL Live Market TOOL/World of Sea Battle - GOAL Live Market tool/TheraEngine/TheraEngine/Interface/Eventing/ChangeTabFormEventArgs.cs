using System;
using TheraEngine.Interface.Controls;

namespace TheraEngine.Interface.Eventing
{
	// Token: 0x0200009E RID: 158
	public class ChangeTabFormEventArgs : EventArgs
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x0001621F File Offset: 0x0001441F
		internal ChangeTabFormEventArgs(Tab {13045}, Form {13046}, Form {13047}, int {13048})
		{
			this.Sender = {13045};
			this.FirstItem = {13046};
			this.SecoundItem = {13047};
			this.FirstItemIndex = {13048};
		}

		// Token: 0x0400032B RID: 811
		public readonly Tab Sender;

		// Token: 0x0400032C RID: 812
		public readonly Form FirstItem;

		// Token: 0x0400032D RID: 813
		public readonly Form SecoundItem;

		// Token: 0x0400032E RID: 814
		public readonly int FirstItemIndex;
	}
}
