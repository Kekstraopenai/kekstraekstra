using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Interface.Controls;

namespace TheraEngine.Interface
{
	// Token: 0x0200007C RID: 124
	public class Composer
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00010D36 File Offset: 0x0000EF36
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00010D3E File Offset: 0x0000EF3E
		public ComposerFontStratagy FontStrategy { get; private set; } = new ComposerFontStratagy();

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00010D47 File Offset: 0x0000EF47
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00010D4F File Offset: 0x0000EF4F
		public float Width { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00010D58 File Offset: 0x0000EF58
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00010D60 File Offset: 0x0000EF60
		public float Space { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00010D69 File Offset: 0x0000EF69
		public float CurrentHeight
		{
			get
			{
				return this.{12621}.Pos.WH.Y;
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00010D80 File Offset: 0x0000EF80
		public Composer(float {12519}, float {12520} = 1f)
		{
			this.Width = {12519};
			this.Space = {12520} + 2f;
			this.{12621} = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{12621}.RemoveFromContainer();
			if (this.Space < 0f)
			{
				this.{12621}.BorderThickness = this.Space;
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00010DF0 File Offset: 0x0000EFF0
		public Composer AddHeader(string {12521}, Color? {12522} = null)
		{
			if (this.{12623} == null)
			{
				this.{12621}.AddItem(new UiControl[]
				{
					this.{12623} = new Label(Vector2.Zero, this.FontStrategy.HeaderFont, {12522} ?? this.FontStrategy.GetTextColorAndFont(ComposerTextStyle.Wheat).Item2, {12521}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				this.{12621}.AddSpace(2f);
				this.ProbablyHasHeader = true;
				return this.{12617}();
			}
			throw new InvalidOperationException("Header already added");
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00010E8D File Offset: 0x0000F08D
		public Composer UpdateHeader(string {12523})
		{
			if (this.{12623} != null)
			{
				this.{12623}.Text = {12523};
			}
			return this;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		private UiControl {12524}(string {12525}, ComposerTextStyle {12526}, bool {12527})
		{
			ValueTuple<CustomSpriteFont, Color> textColorAndFont = this.FontStrategy.GetTextColorAndFont({12526});
			CustomSpriteFont customSpriteFont = textColorAndFont.Item1;
			Color item = textColorAndFont.Item2;
			if ({12527} && {12525}.Length > 50)
			{
				customSpriteFont = Fonts.Arial_10;
			}
			TextBlockControl textBlockControl = TextBlockBuilder.CreateBlock(this.Width, {12525}, item, customSpriteFont, this.FontStrategy.PlainTextInterval).Create(Vector2.Zero);
			textBlockControl.Pos = textBlockControl.Pos.SetWidth(textBlockControl.Pos.WH.X - 10f - customSpriteFont.Measure("A").X);
			if ({12526}.Background == null)
			{
				return textBlockControl;
			}
			if (this.TextBackgroundPath == null)
			{
				throw new InvalidOperationException("Composer.TextBackgroundPath is not set");
			}
			Image image = new Image(textBlockControl.Pos, this.TextBackgroundPath.Value.Tex, this.TextBackgroundPath.Value.Path, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			image.BasicColor = {12526}.Background.Value;
			image.AddChild(textBlockControl);
			return image;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00010FAA File Offset: 0x0000F1AA
		public Composer AddText(string {12528}, ComposerTextStyle {12529}, bool {12530} = true)
		{
			this.{12621}.AddItem(new UiControl[]
			{
				this.{12524}({12528}, {12529}, {12530})
			});
			return this.{12617}();
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00010FD0 File Offset: 0x0000F1D0
		public Composer AddText(float {12531}, string {12532}, ComposerTextStyle {12533}, bool {12534} = true)
		{
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddSpace({12531} + {12531} / 12f);
			stackForm.AddItem(new UiControl[]
			{
				this.{12524}({12532}, {12533}, {12534})
			});
			this.{12621}.AddItem(new UiControl[]
			{
				stackForm
			});
			return this.{12617}();
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0001102E File Offset: 0x0000F22E
		public Composer AddImage(Texture2D {12535})
		{
			return this.AddImage({12535}, {12535}.Bounds);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0001103D File Offset: 0x0000F23D
		public Composer AddImage(Texture2D {12536}, Rectangle {12537})
		{
			return this.AddImage({12536}, {12537}, (float){12537}.Height);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001104E File Offset: 0x0000F24E
		public Composer AddImage(Texture2D {12538}, float {12539})
		{
			return this.AddImage({12538}, {12538}.Bounds, {12539});
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0001105E File Offset: 0x0000F25E
		public Composer AddImage(Texture2D {12540}, Rectangle {12541}, float {12542})
		{
			return this.AddImage({12540}, {12541}, {12542}, {12542});
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0001106C File Offset: 0x0000F26C
		public Composer AddImage(Texture2D {12543}, Rectangle {12544}, float {12545}, float {12546})
		{
			this.{12621}.AddItem(new UiControl[]
			{
				new Image(new Marker(0f, 0f, {12545}, {12546}), {12543}, {12544}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			return this.{12617}();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000110AE File Offset: 0x0000F2AE
		public Composer AddUi(UiControl {12547})
		{
			this.{12621}.AddItem(new UiControl[]
			{
				{12547}
			});
			return this.{12617}();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000110CC File Offset: 0x0000F2CC
		public Composer AddImageAndText(string {12548}, Texture2D {12549}, Rectangle {12550}, float {12551}, ComposerTextStyle {12552}, bool {12553} = true)
		{
			return this.{12574}({12548}, {12549}, {12550}, Color.White, {12551}, {12552}, {12553}, false);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000110F0 File Offset: 0x0000F2F0
		public Composer AddImageAndText(string {12554}, Texture2D {12555}, Rectangle {12556}, Color {12557}, float {12558}, ComposerTextStyle {12559}, bool {12560} = true)
		{
			return this.{12574}({12554}, {12555}, {12556}, {12557}, {12558}, {12559}, {12560}, false);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00011110 File Offset: 0x0000F310
		public Composer AddTextAndImage(string {12561}, Texture2D {12562}, Rectangle {12563}, float {12564}, ComposerTextStyle {12565}, bool {12566} = true)
		{
			return this.{12574}({12561}, {12562}, {12563}, Color.White, {12564}, {12565}, {12566}, true);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00011134 File Offset: 0x0000F334
		public Composer AddTextAndImage(string {12567}, Texture2D {12568}, Rectangle {12569}, Color {12570}, float {12571}, ComposerTextStyle {12572}, bool {12573} = true)
		{
			return this.{12574}({12567}, {12568}, {12569}, {12570}, {12571}, {12572}, {12573}, true);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00011154 File Offset: 0x0000F354
		private Composer {12574}(string {12575}, Texture2D {12576}, Rectangle {12577}, Color {12578}, float {12579}, ComposerTextStyle {12580}, bool {12581}, bool {12582})
		{
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.Width -= {12579};
			UiControl uiControl = this.{12524}({12575}, {12580}, {12581});
			this.Width += {12579};
			Form form = new Form(new Marker(0f, 0f, {12579}, uiControl.Pos.WH.Y), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form.AddChildPos(new Image(new Marker(0f, 0f, {12579}, {12579}), {12576}, {12577}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = {12578}
			}, PositionAlignment.LeftUp, PositionAlignment.Center, 0f);
			if ({12582})
			{
				stackForm.AddItem(new UiControl[]
				{
					uiControl
				});
				stackForm.AddSpace({12579} / 12f);
				stackForm.AddItem(new UiControl[]
				{
					form
				});
			}
			else
			{
				stackForm.AddItem(new UiControl[]
				{
					form
				});
				stackForm.AddSpace({12579} / 12f);
				stackForm.AddItem(new UiControl[]
				{
					uiControl
				});
			}
			this.{12621}.AddItem(new UiControl[]
			{
				stackForm
			});
			return this.{12617}();
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0001127A File Offset: 0x0000F47A
		public Composer AddSpace(float {12583} = 18f)
		{
			if ({12583} - this.Space <= 0f)
			{
				return this;
			}
			this.{12621}.AddSpace({12583} - this.Space);
			return this.{12617}();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000112A8 File Offset: 0x0000F4A8
		public Composer AddAlign(float {12584} = 18f)
		{
			UiControl uiControl = this.{12621}.GetChildren.LastOrDefault((UiControl {12624}) => !({12624} is Form));
			float num = (uiControl != null) ? uiControl.Pos.WH.X : 0f;
			this.{12621}.AddSpace(Math.Max(0f, {12584} - this.Space - num));
			return this.{12617}();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00011324 File Offset: 0x0000F524
		public Composer AddSeparator(Rectangle {12585}, Rectangle {12586}, Texture2D {12587}, string {12588})
		{
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			int num = 2;
			Label label = new Label(Vector2.Zero, this.FontStrategy.SubHeaderFont, this.FontStrategy.GetTextColorAndFont(ComposerTextStyle.Wheat).Item2, {12588}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			float {11528} = this.Width / 2f - label.Pos.WH.X / 2f + (float)(num * 2);
			stackForm.AddItem(new UiControl[]
			{
				new Image(new Marker(0f, 0f, {11528}, (float){12585}.Height), {12587}, {12585}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			stackForm.AddSpace((float)num);
			stackForm.AddItem(new UiControl[]
			{
				label
			});
			stackForm.AddSpace((float)num);
			stackForm.AddItem(new UiControl[]
			{
				new Image(new Marker(0f, 0f, {11528}, (float){12586}.Height), {12587}, {12586}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			this.{12621}.AddSpace((float)num);
			this.{12621}.AddItem(new UiControl[]
			{
				stackForm
			});
			this.{12621}.AddSpace((float)num);
			return this.{12617}();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0001144C File Offset: 0x0000F64C
		public Composer AddColumnsText(string {12589}, string {12590}, ComposerTextStyle {12591})
		{
			ValueTuple<CustomSpriteFont, Color> textColorAndFont = this.FontStrategy.GetTextColorAndFont({12591});
			CustomSpriteFont item = textColorAndFont.Item1;
			Color item2 = textColorAndFont.Item2;
			Label label = new Label(Vector2.Zero, item, item2, {12589}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Label label2 = new Label(Vector2.Zero, item, item2, {12590}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				label
			});
			stackForm.AddSpace(Math.Max(1f, this.Width * 0.66f - label.Pos.WH.X));
			stackForm.AddItem(new UiControl[]
			{
				label2
			});
			this.{12621}.AddItem(new UiControl[]
			{
				stackForm
			});
			return this.{12617}();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00011510 File Offset: 0x0000F710
		public Composer AddColumnsText(string {12592}, int {12593}, int {12594}, string {12595}, ComposerTextStyle {12596})
		{
			ValueTuple<CustomSpriteFont, Color> textColorAndFont = this.FontStrategy.GetTextColorAndFont({12596});
			CustomSpriteFont item = textColorAndFont.Item1;
			Color item2 = textColorAndFont.Item2;
			Label label = new Label(Vector2.Zero, item, item2, {12592}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				label
			});
			stackForm.AddSpace(Math.Max(1f, 350f - label.Pos.WH.X));
			for (int i = 0; i < {12593}; i++)
			{
				Label label2 = new Label(Vector2.Zero, item, item2, {12595}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm.AddItem(new UiControl[]
				{
					label2
				});
			}
			for (int j = 0; j < {12594} - {12593}; j++)
			{
				Label label3 = new Label(Vector2.Zero, item, item2 * 0.45f, {12595}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm.AddItem(new UiControl[]
				{
					label3
				});
			}
			this.{12621}.AddItem(new UiControl[]
			{
				stackForm
			});
			return this.{12617}();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00011618 File Offset: 0x0000F818
		public Composer OpenHorizontal()
		{
			if (this.{12622} != null)
			{
				throw new InvalidOperationException("Previous horizontal nore haven't closed");
			}
			this.{12622} = this.{12621};
			this.{12621} = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.ProbablyHasHeader = true;
			return this;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00011654 File Offset: 0x0000F854
		public Composer CloseHorizontal()
		{
			this.{12622}.AddItem(new UiControl[]
			{
				this.{12621}
			});
			this.{12621} = this.{12622};
			this.{12622} = null;
			return this.{12617}();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001168C File Offset: 0x0000F88C
		public Composer AddCustom(UiControl {12597}, PositionAlignment {12598} = PositionAlignment.Center)
		{
			if ({12598} == PositionAlignment.Center)
			{
				Form form = new Form(new Marker(0f, 0f, this.Width, {12597}.Pos.WH.Y), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form.AnimatedFocus = false;
				form.AddChildPos({12597}, PositionAlignment.Center, PositionAlignment.Center, 0f);
				{12597} = form;
			}
			this.{12621}.AddItem(new UiControl[]
			{
				{12597}
			});
			return this.{12617}();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000116FC File Offset: 0x0000F8FC
		public Composer AddCustomInRow(float {12599}, IEnumerable<UiControl> {12600})
		{
			return this.AddCustomInRow({12599}, {12600}.ToArray<UiControl>());
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0001170C File Offset: 0x0000F90C
		public Composer AddCustomInRow(float {12601} = 0f, params UiControl[] {12602})
		{
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.BorderThickness = {12601} / 2f;
			float num = {12602}.Sum((UiControl {12625}) => {12625}.Pos.WH.X + {12601});
			stackForm.AddSpace(this.Width / 2f - num / 2f);
			foreach (UiControl uiControl in {12602})
			{
				stackForm.AddItem(new UiControl[]
				{
					uiControl
				});
			}
			this.{12621}.AddItem(new UiControl[]
			{
				stackForm
			});
			return this.{12617}();
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000117BC File Offset: 0x0000F9BC
		public Composer AddComposer(Composer {12603})
		{
			this.ProbablyHasHeader |= {12603}.ProbablyHasHeader;
			this.{12621}.AddItem(new UiControl[]
			{
				{12603}.ComposeInStack(null)
			});
			return this.{12617}();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00011808 File Offset: 0x0000FA08
		public StackForm ComposeInStack(Vector2? {12604} = null)
		{
			if ({12604} != null)
			{
				UiControl uiControl = this.{12621};
				Marker pos = this.{12621}.Pos;
				Vector2 value = {12604}.Value;
				uiControl.Pos = pos.SetXY(value);
			}
			return this.{12621};
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0001184C File Offset: 0x0000FA4C
		public Form ComposeInForm(Rectangle {12605}, float {12606}, Vector2? {12607} = null)
		{
			Vector2 vector = {12607} ?? Vector2.Zero;
			Form form = new Form(new Marker(ref vector, this.Width, this.{12621}.Pos.WH.Y).Border({12606}), {12605}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			form.AddChildPos(this.{12621}, PositionAlignment.LeftUp, PositionAlignment.LeftUp, {12606});
			return form;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000118C0 File Offset: 0x0000FAC0
		public Image ComposeInImage(Texture2D {12608}, Rectangle {12609}, float {12610}, Vector2? {12611} = null)
		{
			Vector2 vector = {12611} ?? Vector2.Zero;
			Image image = new Image(new Marker(ref vector, this.Width, this.{12621}.Pos.WH.Y).Border({12610}), {12608}, {12609}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			image.AddChildPos(this.{12621}, PositionAlignment.LeftUp, PositionAlignment.LeftUp, {12610});
			return image;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0001192C File Offset: 0x0000FB2C
		public Viewbox ComposeInFormWithScroll(Marker {12612}, Rectangle {12613}, Rectangle {12614}, Rectangle {12615}, Rectangle {12616})
		{
			Viewbox viewbox = new Viewbox({12612}, {12613}, {12614}, {12615}, {12616});
			viewbox.AddItem(new UiControl[]
			{
				this.{12621}
			});
			return viewbox;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001195C File Offset: 0x0000FB5C
		private Composer {12617}()
		{
			if (this.Space > 0f)
			{
				this.{12621}.AddSpace(this.Space);
			}
			return this;
		}

		// Token: 0x04000270 RID: 624
		[CompilerGenerated]
		private ComposerFontStratagy {12618};

		// Token: 0x04000271 RID: 625
		[CompilerGenerated]
		private float {12619};

		// Token: 0x04000272 RID: 626
		[CompilerGenerated]
		private float {12620};

		// Token: 0x04000273 RID: 627
		public ImageDecription? TextBackgroundPath;

		// Token: 0x04000274 RID: 628
		internal bool ProbablyHasHeader;

		// Token: 0x04000275 RID: 629
		private StackForm {12621};

		// Token: 0x04000276 RID: 630
		[Nullable(2)]
		private StackForm {12622};

		// Token: 0x04000277 RID: 631
		[Nullable(2)]
		private Label {12623};
	}
}
