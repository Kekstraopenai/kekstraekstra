using System;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Content;
using TheraEngine.Interface.Controls;

namespace TheraEngine.Interface
{
	// Token: 0x02000086 RID: 134
	public readonly struct ToolTipCharacteristics
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00012784 File Offset: 0x00010984
		public Color ColorToColor
		{
			get
			{
				if (this.Color == CharacteristicsColor.Gray)
				{
					return Microsoft.Xna.Framework.Color.Gray * 1.3f;
				}
				if (this.Color == CharacteristicsColor.Blue)
				{
					return new Color(116, 104, 173);
				}
				if (this.Color == CharacteristicsColor.Lime || this.Color == CharacteristicsColor.LimeBold)
				{
					return new Color(105, 173, 117) * 1.1f;
				}
				if (this.Color != CharacteristicsColor.Wheat && this.Color != CharacteristicsColor.WheatBold)
				{
					return new Color(193, 101, 83);
				}
				return Microsoft.Xna.Framework.Color.Wheat;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00012812 File Offset: 0x00010A12
		public ToolTipCharacteristics(string {12725}, string {12726})
		{
			this.Left = {12725};
			this.Right = {12726};
			this.Color = CharacteristicsColor.Gray;
			this.Level = 0;
			this.MaxLevel = 0;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00012837 File Offset: 0x00010A37
		public ToolTipCharacteristics(string {12727}, string {12728}, CharacteristicsColor {12729})
		{
			this.Left = {12727};
			this.Right = {12728};
			this.Color = {12729};
			this.Level = 0;
			this.MaxLevel = 0;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001285C File Offset: 0x00010A5C
		public ToolTipCharacteristics(string {12730}, CharacteristicsColor {12731})
		{
			this.Left = {12730};
			this.Right = "";
			this.Color = {12731};
			this.Level = 0;
			this.MaxLevel = 0;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00012885 File Offset: 0x00010A85
		public ToolTipCharacteristics(string {12732}, CharacteristicsColor {12733}, int {12734}, int {12735})
		{
			this.Left = {12732};
			this.Right = "";
			this.Color = {12733};
			this.Level = {12734};
			this.MaxLevel = {12735};
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000128B0 File Offset: 0x00010AB0
		public void WriteTo(TextBlockBuilder {12736}, CustomSpriteFont {12737}, CustomSpriteFont {12738}, bool {12739} = false)
		{
			Color colorToColor = this.ColorToColor;
			if ({12737}.Measure(this.Left).X > 300f && string.IsNullOrEmpty(this.Right))
			{
				{12736}.WriteLines(this.Left, colorToColor, (this.Color == CharacteristicsColor.WheatBold || this.Color == CharacteristicsColor.LimeBold) ? {12738} : {12737}, 300f, new float?(0f));
			}
			else
			{
				{12736}.WriteLine(this.Left, colorToColor, (this.Color == CharacteristicsColor.WheatBold || this.Color == CharacteristicsColor.LimeBold) ? {12738} : {12737});
			}
			if (!string.IsNullOrEmpty(this.Right))
			{
				{12736}.Write(this.Right, colorToColor * ((this.Color == CharacteristicsColor.Gray) ? 1.3f : 1.15f), {12738}, new float?(220f), false);
				return;
			}
			if (this.MaxLevel > 0)
			{
				int num = 0;
				for (int i = 0; i < this.Level; i++)
				{
					{12736}.Write("☠", Microsoft.Xna.Framework.Color.Wheat, {12738}, new float?(220f + (float)(num++ * 15)), false);
				}
				if (!{12739})
				{
					for (int j = 0; j < this.MaxLevel - this.Level; j++)
					{
						{12736}.Write("☠", Microsoft.Xna.Framework.Color.Gray * 0.5f, {12738}, new float?(220f + (float)(num++ * 15)), false);
					}
				}
			}
		}

		// Token: 0x040002AF RID: 687
		public readonly string Left;

		// Token: 0x040002B0 RID: 688
		public readonly string Right;

		// Token: 0x040002B1 RID: 689
		public readonly CharacteristicsColor Color;

		// Token: 0x040002B2 RID: 690
		public readonly int Level;

		// Token: 0x040002B3 RID: 691
		public readonly int MaxLevel;
	}
}
