using System;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Content;
using TheraEngine.Interface.Eventing;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000AB RID: 171
	public class LabelButton : Label
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x00017489 File Offset: 0x00015689
		public LabelButton(Vector2 {13303}, string {13304}, CustomSpriteFont {13305}, Color {13306}, Color {13307}, Action<ClickUiEventArgs> {13308} = null) : base({13303}, {13305}, {13306}, {13304}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.FocusColor = {13307};
			this.DefaultColor = {13306};
			if ({13308} != null)
			{
				base.EvClick += {13308};
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000174B4 File Offset: 0x000156B4
		internal override void Update(ref FrameTime {13309}, ref int {13310})
		{
			base.Update(ref {13309}, ref {13310});
			this.BasicColor = ((base.InputMode == MouseInputMode.Focused) ? this.FocusColor : this.DefaultColor);
		}

		// Token: 0x04000367 RID: 871
		public Color FocusColor;

		// Token: 0x04000368 RID: 872
		public Color DefaultColor;
	}
}
