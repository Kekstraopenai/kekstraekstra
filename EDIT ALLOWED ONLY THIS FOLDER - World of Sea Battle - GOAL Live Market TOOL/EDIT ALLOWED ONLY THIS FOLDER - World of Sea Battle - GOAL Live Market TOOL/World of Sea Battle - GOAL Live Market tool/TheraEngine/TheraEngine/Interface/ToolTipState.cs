using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;

namespace TheraEngine.Interface
{
	// Token: 0x02000087 RID: 135
	public sealed class ToolTipState
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00012A11 File Offset: 0x00010C11
		public TextBlockBuilder Builder
		{
			get
			{
				return this.builder;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00012A19 File Offset: 0x00010C19
		public ToolTipState(CustomSpriteFont {12767} = null) : this(new TextBlockBuilder({12767} ?? Fonts.Arial_10Bold, 0f))
		{
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00012A35 File Offset: 0x00010C35
		public ToolTipState(TextBlockBuilder {12768})
		{
			this.builder = {12768};
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00012A44 File Offset: 0x00010C44
		public ToolTipState(Composer {12769})
		{
			ToolTipState <>4__this = this;
			this.updateElementFunc = delegate()
			{
				<>4__this.AddElement({12769}.ComposeInStack(new Vector2?(Vector2.Zero)));
				<>4__this.requiredBackgroundHeaderEffect = {12769}.ProbablyHasHeader;
			};
			this.builder = new TextBlockBuilder(Fonts.Arial_10, 0f);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00012A94 File Offset: 0x00010C94
		public ToolTipState(float {12770}, float {12771}, Rectangle {12772}, Rectangle {12773}, Texture2D {12774}, Action<Tab> {12775}, params Composer[] {12776})
		{
			ToolTipState <>4__this = this;
			this.updateElementFunc = delegate()
			{
				Tab tab = new Tab(new Marker(0f, 0f, {12770}, {12771}), Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Composer[] composers = {12776};
				Action<UiControl> <>9__1;
				for (int i = 0; i < composers.Length; i++)
				{
					Form form = composers[i].ComposeInForm(Rectangle.Empty, 0f, null);
					tab.Add(new Form[]
					{
						form
					});
					Image image = new Image(new Vector2(form.Pos.XY.X - 44f, form.Pos.Center.Y - (float)({12772}.Height / 2)), {12774}, {12772}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					Image image2 = new Image(new Vector2(form.Pos.End.X - (float){12773}.Width + 44f + 1f, form.Pos.Center.Y - (float)({12772}.Height / 2)), {12774}, {12773}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					form.AddChild(new UiControl[]
					{
						image,
						image2
					});
					new UiBrightnessAnimation(image, 1f, 2f, 500f);
					new UiBrightnessAnimation(image, 2f, 1f, 500f);
					new UiBrightnessAnimation(image2, 1f, 2f, 500f);
					new UiBrightnessAnimation(image2, 2f, 1f, 500f);
					UiControl uiControl = form;
					Action<UiControl> {12880};
					if (({12880} = <>9__1) == null)
					{
						{12880} = (<>9__1 = delegate(UiControl {12816})
						{
							if (InputHelper.IsClick(Keys.Left))
							{
								tab.BackToCycle();
							}
							if (InputHelper.IsClick(Keys.Right))
							{
								tab.NextToCycle();
							}
						});
					}
					uiControl.UpdateComplete += {12880};
					tab.Select(0);
					Action<Tab> onTabInitialized = {12775};
					if (onTabInitialized != null)
					{
						onTabInitialized(tab);
					}
				}
				<>4__this.AddElement(tab);
			};
			this.builder = new TextBlockBuilder(Fonts.Arial_10, 0f);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00012B10 File Offset: 0x00010D10
		public ToolTipState(string {12777}, string {12778}, params ToolTipCharacteristics[] {12779})
		{
			this.{12799}({12777}, {12778}, {12779});
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00012B24 File Offset: 0x00010D24
		public ToolTipState(string {12780}, ToolTipCharacteristics[] {12781}, int {12782}, Rectangle {12783}, Rectangle {12784}, Texture2D {12785}, CustomSpriteFont {12786} = null)
		{
			ToolTipState <>4__this = this;
			if ({12781}.Length <= {12782})
			{
				this.{12799}({12780}, "", {12781});
				return;
			}
			CustomSpriteFont customSpriteFont = {12786} ?? Fonts.Arial_12;
			CustomSpriteFont {12738} = {12786} ?? Fonts.Philosopher_14Bold;
			int num = (int)Math.Ceiling((double)((float){12781}.Length / (float){12782}));
			string[] headers = new string[num];
			ToolTipConstructorPage[] pages = new ToolTipConstructorPage[num];
			int num2 = {12782};
			int num3 = -1;
			foreach (ToolTipCharacteristics toolTipCharacteristics in {12781})
			{
				if (num2 == {12782})
				{
					num3++;
					num2 = 0;
					pages[num3] = new ToolTipConstructorPage(new TextBlockBuilder(customSpriteFont, 0f));
					headers[num3] = string.Concat(new string[]
					{
						{12780},
						" ",
						(num3 + 1).ToString(),
						" / ",
						num.ToString()
					});
				}
				num2++;
				toolTipCharacteristics.WriteTo(pages.ElementAt(num3).InternalCreate(), customSpriteFont, {12738}, false);
			}
			this.builder = new TextBlockBuilder(Fonts.Arial_10, 0f);
			this.updateElementFunc = delegate()
			{
				<>4__this.AddElement(ToolTipState.CreateToolTipContentWithTab(headers, pages, {12783}, {12784}, {12785}, null));
			};
			this.requiredBackgroundHeaderEffect = true;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00012CA0 File Offset: 0x00010EA0
		public void WriteHeader(string {12787})
		{
			this.builder.WriteLine({12787}, Color.White);
			this.builder.Write(" ", Color.White, Fonts.Philosopher_18, null, false);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00012CE2 File Offset: 0x00010EE2
		public ToolTipState(ToolTipConstructorPage {12788})
		{
			this.builder = {12788}.InternalCreate();
			this.requiredBackgroundHeaderEffect = true;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00012D00 File Offset: 0x00010F00
		public ToolTipState(string[] {12789}, ToolTipConstructorPage[] {12790}, Rectangle {12791}, Rectangle {12792}, Texture2D {12793}, Action<Tab> {12794} = null)
		{
			ToolTipState <>4__this = this;
			this.builder = new TextBlockBuilder(Fonts.Arial_10, 0f);
			this.updateElementFunc = delegate()
			{
				<>4__this.AddElement(ToolTipState.CreateToolTipContentWithTab({12789}, {12790}, {12791}, {12792}, {12793}, {12794}));
			};
			this.requiredBackgroundHeaderEffect = true;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00012D7C File Offset: 0x00010F7C
		public void AppendText(string {12795}, Color {12796}, bool {12797} = false, bool {12798} = false)
		{
			CustomSpriteFont {13619} = {12797} ? ({12798} ? Fonts.Arial_12 : Fonts.Arial_10Bold) : ({12798} ? Fonts.Philosopher_14Bold : Fonts.Arial_10);
			this.builder.WriteLines(TextBlockBuilder.CreateBlock(350f, {12795}, {12796}, {13619}, 0f));
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00012DCC File Offset: 0x00010FCC
		private void {12799}(string {12800}, string {12801}, params ToolTipCharacteristics[] {12802})
		{
			if ({12800} == null)
			{
				{12800} = string.Empty;
			}
			if ({12801} == null)
			{
				{12801} = string.Empty;
			}
			int num = 350;
			if ({12801}.Length < 100 && {12800}.Length < 30)
			{
				num = Math.Max(300, num - 50);
			}
			this.builder = new TextBlockBuilder(Fonts.Philosopher_14, 0f);
			if ({12800}.Length > 0)
			{
				this.WriteHeader({12800});
			}
			CustomSpriteFont customSpriteFont = Fonts.Arial_12;
			CustomSpriteFont {12738} = Fonts.Philosopher_14Bold;
			if ({12801}.Length > 0 || {12802}.Length != 0)
			{
				if ({12801}.Length > 100 || {12802}.Length != 0)
				{
					customSpriteFont = Fonts.Arial_10;
					{12738} = Fonts.Arial_10Bold;
				}
				if (!{12801}.Contains('#'))
				{
					IEnumerable<char> source = {12801};
					Func<char, bool> predicate;
					if ((predicate = ToolTipState.<>O.<0>__IsDigit) == null)
					{
						predicate = (ToolTipState.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
					}
					if (!source.Any(predicate))
					{
						if ({12801}.Length > 0)
						{
							this.builder.WriteLines(TextBlockBuilder.CreateBlock(350f, {12801}, Color.Wheat * (({12802}.Length != 0) ? 0.5f : 0.8f), customSpriteFont, 0f));
							goto IL_252;
						}
						goto IL_252;
					}
				}
				string[] array = {12801}.SplitSmart(new char[]
				{
					' '
				});
				Color textColor = Color.Wheat * (({12802}.Length != 0) ? 0.5f : 0.8f);
				Color hlTextColor = Color.Lerp(Color.Gold, Color.Wheat, 0.5f);
				Color[] array2 = array.Select(delegate(string {12817})
				{
					if (!{12817}.Contains('#'))
					{
						if (!{12817}.All((char {12810}) => "01234567890+-.,".Contains({12810})))
						{
							return textColor;
						}
					}
					return hlTextColor;
				}).ToArray<Color>();
				array = (from {12811} in array
				select {12811}.Replace("#", "") + ({12811}.EndsWithCjk() ? "" : " ")).ToArray<string>();
				this.builder.WriteLine("", Color.White, customSpriteFont);
				for (int i = 0; i < array.Length; i++)
				{
					var <>f__AnonymousType = new
					{
						{11258} = array[i],
						{11259} = array2[i]
					};
					Vector2 vector = customSpriteFont.Measure(<>f__AnonymousType.word);
					if (this.builder.CurrentLocalPoint.X + vector.X >= 350f)
					{
						this.builder.WriteLine("", <>f__AnonymousType.color, customSpriteFont);
					}
					this.builder.Write(<>f__AnonymousType.word, <>f__AnonymousType.color, customSpriteFont, null, false);
				}
			}
			IL_252:
			this.requiredBackgroundHeaderEffect = (!string.IsNullOrEmpty({12800}) && !string.IsNullOrEmpty({12801}));
			if ({12802}.Length != 0)
			{
				if (this.builder.Size != Vector2.Zero)
				{
					this.builder.WriteSpaceLine(4f);
				}
				foreach (ToolTipCharacteristics toolTipCharacteristics in {12802})
				{
					this.builder.WriteSpaceLine(2f);
					toolTipCharacteristics.WriteTo(this.builder, customSpriteFont, {12738}, false);
				}
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000130B4 File Offset: 0x000112B4
		private static Form CreateToolTipContentWithTab(string[] {12803}, ToolTipConstructorPage[] {12804}, Rectangle {12805}, Rectangle {12806}, Texture2D {12807}, Action<Tab> {12808} = null)
		{
			IEnumerable<TextBlockBuilder> source = from {12812} in {12804}
			select {12812}.InternalCreate();
			Vector2 vector = new Vector2(source.Max((TextBlockBuilder {12813}) => {12813}.Size.X), source.Max((TextBlockBuilder {12814}) => {12814}.Size.Y));
			vector.X = Math.Max(vector.X, {12803}.Max((string {12815}) => Fonts.Philosopher_14.Measure({12815}).X));
			vector.X += 15f;
			vector.Y += 40f;
			Tab tab = new Tab(new Marker(0f, 0f, ref vector), Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			IEnumerable<Form> enumerable = source.Select(delegate(TextBlockBuilder {12818})
			{
				Form form3 = new Form(tab.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form3.AddChild({12818}.Create(new Vector2(5f, 35f)));
				tab.Add(new Form[]
				{
					form3
				});
				return form3;
			});
			int num = 0;
			foreach (Form form in enumerable)
			{
				form.AddChild(new Label(new Vector2(form.Pos.Center.X, 12f), Fonts.Philosopher_14, Color.White, {12803}[num++], PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			}
			Form form2 = new Form(tab.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form2.AddChild(tab);
			Image image = new Image(new Vector2(form2.Pos.XY.X - 44f, form2.Pos.Center.Y - (float)({12805}.Height / 2)), {12807}, {12805}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Image image2 = new Image(new Vector2(form2.Pos.End.X - (float){12806}.Width + 44f + 1f, form2.Pos.Center.Y - (float)({12805}.Height / 2)), {12807}, {12806}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form2.AddChild(new UiControl[]
			{
				image,
				image2
			});
			new UiBrightnessAnimation(image, 1f, 2f, 500f);
			new UiBrightnessAnimation(image, 2f, 1f, 500f);
			new UiBrightnessAnimation(image2, 1f, 2f, 500f);
			new UiBrightnessAnimation(image2, 2f, 1f, 500f);
			form2.UpdateComplete += delegate(UiControl {12819})
			{
				if (InputHelper.IsClick(Keys.Left))
				{
					tab.BackToCycle();
				}
				if (InputHelper.IsClick(Keys.Right))
				{
					tab.NextToCycle();
				}
			};
			tab.Select(0);
			if ({12808} != null)
			{
				{12808}(tab);
			}
			return form2;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000133B0 File Offset: 0x000115B0
		public ToolTipState AddElement(UiControl {12809})
		{
			if (this.addElements == null)
			{
				this.addElements = new Tlist<UiControl>();
			}
			{12809}.RemoveFromContainer();
			this.addElements.Add({12809});
			this.requiredBackgroundHeaderEffect = true;
			return this;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000133E0 File Offset: 0x000115E0
		public Tlist<UiControl> QueryElements()
		{
			Action action = this.updateElementFunc;
			if (action != null)
			{
				action();
			}
			return this.addElements;
		}

		// Token: 0x040002B4 RID: 692
		private const int BasicMaxWidth = 350;

		// Token: 0x040002B5 RID: 693
		internal TextBlockBuilder builder;

		// Token: 0x040002B6 RID: 694
		internal Tlist<UiControl> addElements;

		// Token: 0x040002B7 RID: 695
		internal Action updateElementFunc;

		// Token: 0x040002B8 RID: 696
		internal bool requiredBackgroundHeaderEffect;

		// Token: 0x02000088 RID: 136
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040002B9 RID: 697
			public static Func<char, bool> <0>__IsDigit;
		}
	}
}
