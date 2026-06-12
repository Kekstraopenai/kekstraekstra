using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Content;

namespace TheraEngine.Interface
{
	// Token: 0x0200007A RID: 122
	[NullableContext(2)]
	[Nullable(0)]
	public readonly struct ComposerTextStyle
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x00010B43 File Offset: 0x0000ED43
		public ComposerTextStyle(Color {12504}, bool {12505}, CustomSpriteFont {12506} = null, Color? {12507} = null)
		{
			this.Color = {12504};
			this.IsBold = {12505};
			this.OverrideFont = {12506};
			this.Background = {12507};
		}

		// Token: 0x04000262 RID: 610
		public static readonly ComposerTextStyle Gray = new ComposerTextStyle(Color.Gray * 1.3f, false, null, null);

		// Token: 0x04000263 RID: 611
		public static readonly ComposerTextStyle Wheat = new ComposerTextStyle(Color.Lerp(Color.Wheat, Color.White, 0.5f), false, null, null);

		// Token: 0x04000264 RID: 612
		public static readonly ComposerTextStyle WheatBold = new ComposerTextStyle(Color.Wheat, true, null, null);

		// Token: 0x04000265 RID: 613
		public static readonly ComposerTextStyle Blue = new ComposerTextStyle(new Color(116, 104, 173), false, null, null);

		// Token: 0x04000266 RID: 614
		public static readonly ComposerTextStyle Lime = new ComposerTextStyle(new Color(105, 173, 117) * 1.1f, false, null, null);

		// Token: 0x04000267 RID: 615
		public static readonly ComposerTextStyle LimeBold = new ComposerTextStyle(new Color(105, 173, 117) * 1.1f, true, null, null);

		// Token: 0x04000268 RID: 616
		public static readonly ComposerTextStyle Warning = new ComposerTextStyle(new Color(255, 186, 118), false, null, null);

		// Token: 0x04000269 RID: 617
		public static readonly ComposerTextStyle Orange = new ComposerTextStyle(new Color(211, 175, 117), false, null, null);

		// Token: 0x0400026A RID: 618
		public static readonly ComposerTextStyle Red = new ComposerTextStyle(new Color(245, 106, 121), false, null, null);

		// Token: 0x0400026B RID: 619
		public static readonly ComposerTextStyle BareWhite = new ComposerTextStyle(Color.White, false, null, null);

		// Token: 0x0400026C RID: 620
		public readonly Color Color;

		// Token: 0x0400026D RID: 621
		public readonly bool IsBold;

		// Token: 0x0400026E RID: 622
		public readonly CustomSpriteFont OverrideFont;

		// Token: 0x0400026F RID: 623
		public readonly Color? Background;
	}
}
