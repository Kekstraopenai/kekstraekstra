using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Content;
using TheraEngine.Components;

namespace TheraEngine.Interface
{
	// Token: 0x02000079 RID: 121
	public class ComposerFontStratagy
	{
		// Token: 0x060002DE RID: 734 RVA: 0x00010AAA File Offset: 0x0000ECAA
		public ComposerFontStratagy()
		{
			this.HeaderFont = Fonts.Philosopher_16;
			this.SubHeaderFont = Fonts.Philosopher_14;
			this.DefaultTextFont = Fonts.Arial_12;
			this.DefaultTextBoldFont = Fonts.Philosopher_14Bold;
			this.PlainTextInterval = -2f;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00010AE9 File Offset: 0x0000ECE9
		public ComposerFontStratagy(CustomSpriteFont {12494}, CustomSpriteFont {12495}, CustomSpriteFont {12496}, CustomSpriteFont {12497}, float {12498})
		{
			this.HeaderFont = {12494};
			this.SubHeaderFont = {12495};
			this.DefaultTextFont = {12496};
			this.DefaultTextBoldFont = {12497};
			this.PlainTextInterval = {12498};
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00010B16 File Offset: 0x0000ED16
		[return: TupleElementNames(new string[]
		{
			"font",
			"color"
		})]
		internal ValueTuple<CustomSpriteFont, Color> GetTextColorAndFont(ComposerTextStyle {12499})
		{
			return new ValueTuple<CustomSpriteFont, Color>({12499}.OverrideFont ?? ({12499}.IsBold ? this.DefaultTextBoldFont : this.DefaultTextFont), {12499}.Color);
		}

		// Token: 0x0400025D RID: 605
		public CustomSpriteFont HeaderFont;

		// Token: 0x0400025E RID: 606
		public CustomSpriteFont SubHeaderFont;

		// Token: 0x0400025F RID: 607
		public CustomSpriteFont DefaultTextFont;

		// Token: 0x04000260 RID: 608
		public CustomSpriteFont DefaultTextBoldFont;

		// Token: 0x04000261 RID: 609
		public float PlainTextInterval;
	}
}
