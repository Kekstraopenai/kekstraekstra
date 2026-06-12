using System;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Interface.Controls;

namespace TheraEngine.Interface
{
	// Token: 0x02000084 RID: 132
	public struct ToolTipConstructorPage
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0001266C File Offset: 0x0001086C
		private static CustomSpriteFont DefatulFont
		{
			get
			{
				return Fonts.Arial_10;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00012673 File Offset: 0x00010873
		public ToolTipConstructorPage(string[] {12707}, Color[] {12708})
		{
			this.Text = {12707};
			this.Colors = {12708};
			this.OverrideData = null;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0001268C File Offset: 0x0001088C
		public ToolTipConstructorPage(string[] {12709}, Color {12710})
		{
			this.Text = {12709};
			this.Colors = new Color[{12709}.Length];
			for (int i = 0; i < {12709}.Length; i++)
			{
				this.Colors[i] = {12710};
			}
			this.OverrideData = null;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000126D0 File Offset: 0x000108D0
		public ToolTipConstructorPage(string {12711}, Color {12712})
		{
			this.Text = new string[]
			{
				{12711}
			};
			this.Colors = new Color[]
			{
				{12712}
			};
			this.OverrideData = null;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000126FD File Offset: 0x000108FD
		public ToolTipConstructorPage(TextBlockBuilder {12713})
		{
			this.Text = null;
			this.Colors = null;
			this.OverrideData = {12713};
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00012714 File Offset: 0x00010914
		internal TextBlockBuilder InternalCreate()
		{
			if (this.OverrideData != null)
			{
				return this.OverrideData;
			}
			float {13616} = 350f;
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(ToolTipConstructorPage.DefatulFont, 0f);
			for (int i = 0; i < this.Text.Length; i++)
			{
				textBlockBuilder.WriteLines(TextBlockBuilder.CreateBlock({13616}, this.Text[i], this.Colors[i], ToolTipConstructorPage.DefatulFont, 0f));
			}
			return textBlockBuilder;
		}

		// Token: 0x040002A4 RID: 676
		public string[] Text;

		// Token: 0x040002A5 RID: 677
		public Color[] Colors;

		// Token: 0x040002A6 RID: 678
		public TextBlockBuilder OverrideData;
	}
}
