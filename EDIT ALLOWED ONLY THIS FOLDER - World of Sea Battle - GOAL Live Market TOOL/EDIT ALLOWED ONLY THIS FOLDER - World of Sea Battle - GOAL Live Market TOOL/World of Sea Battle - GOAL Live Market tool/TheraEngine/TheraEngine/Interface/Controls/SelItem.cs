using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000CB RID: 203
	public class SelItem<TItem>
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x0001C548 File Offset: 0x0001A748
		public SelItem(TItem {13899}, string {13900})
		{
			this.Value = {13899};
			this.CoverDecoration = Rectangle.Empty;
			this.Text = {13900};
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001C569 File Offset: 0x0001A769
		public SelItem(TItem {13901}, string {13902}, Rectangle {13903})
		{
			this.Value = {13901};
			this.CoverDecoration = {13903};
			this.Text = {13902};
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001C586 File Offset: 0x0001A786
		public SelItem(TItem {13904}, string {13905}, ToolTip {13906}) : this({13904}, {13905})
		{
			this.ToolTip = {13906};
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001C597 File Offset: 0x0001A797
		public SelItem(TItem {13907}, string {13908}, Rectangle {13909}, ToolTip {13910}) : this({13907}, {13908}, {13909})
		{
			this.ToolTip = {13910};
		}

		// Token: 0x0400041A RID: 1050
		public readonly TItem Value;

		// Token: 0x0400041B RID: 1051
		public Rectangle CoverDecoration;

		// Token: 0x0400041C RID: 1052
		public string Text;

		// Token: 0x0400041D RID: 1053
		public ToolTip ToolTip;

		// Token: 0x0400041E RID: 1054
		[Nullable(new byte[]
		{
			2,
			0,
			0,
			0
		})]
		public Action<UiControl, SelItem<TItem>> AddExtraContent;
	}
}
