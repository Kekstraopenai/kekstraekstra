using System;

namespace TheraEngine.Interface.Animation
{
	// Token: 0x020000E3 RID: 227
	public sealed class UiRemoveAction : UiAction
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x0001F6E4 File Offset: 0x0001D8E4
		public UiRemoveAction(UiControl {14320})
		{
			{14320}.AddAction(this);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001F6F3 File Offset: 0x0001D8F3
		protected internal override bool Update(ref FrameTime {14321}, UiControl {14322})
		{
			{14322}.RemoveFromContainer();
			return true;
		}
	}
}
