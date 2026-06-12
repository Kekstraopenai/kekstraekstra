using System;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200023E RID: 574
	internal class {20105} : Form
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0006CAC0 File Offset: 0x0006ACC0
		// (set) Token: 0x06000D0C RID: 3340 RVA: 0x0006CAC8 File Offset: 0x0006ACC8
		public Button Button { get; private set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0006CAD1 File Offset: 0x0006ACD1
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x0006CAD9 File Offset: 0x0006ACD9
		public Action<FractionID> OnFractionChanged { get; set; }

		// Token: 0x06000D0F RID: 3343 RVA: 0x0006CAE2 File Offset: 0x0006ACE2
		public {20105}(Vector2 {20110}, params FractionID[] {20111}) : base({20110}, {20105}.c_bigContractForm, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.{20121} = {20111};
			this.AnimatedFocus = false;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0006CB00 File Offset: 0x0006AD00
		public void SelectFraction(FractionID {20112})
		{
			base.ClearAllChild();
			string {13345} = ({20112} == FractionID.None) ? Local.flotilia : {20112}.GetName();
			int num = 23;
			base.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_16Bold, Color.Black * 0.9f, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, (float)num);
			base.AddChildPos(new Form(Vector2.Zero, new Rectangle(146, 155, 35, 26), PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {20122})
			{
				this.SelectFraction(this.{20115}({20112}, -1));
			}), PositionAlignment.LeftUp, PositionAlignment.LeftUp, 55f, (float)num, false);
			base.AddChildPos(new Form(Vector2.Zero, new Rectangle(182, 155, 35, 26), PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {20123})
			{
				this.SelectFraction(this.{20115}({20112}, 1));
			}), PositionAlignment.RightDown, PositionAlignment.LeftUp, 55f, (float)num, false);
			base.AddChildPos(new Form(new Marker(0f, 0f, 96f, 96f), {20105}.GetFractionIcon({20112}), Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			}, PositionAlignment.Center, PositionAlignment.LeftUp, (float)(num += 35));
			string text = ({20112} == FractionID.None) ? ((Session.Guild != null) ? Local.flotilia_downgrade : Local.flotilia_no_contract) : {20112}.GetContactDescription();
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_12, -1f);
			foreach (string text2 in text.Split('#', StringSplitOptions.None))
			{
				if (string.IsNullOrEmpty(text2))
				{
					textBlockBuilder.WriteLine(" ", Color.White);
				}
				else
				{
					textBlockBuilder.WriteLines(text2, Color.Black * 0.8f, textBlockBuilder.defaultFont, (float)({20105}.c_bigContractForm.Width - 45), new float?(0f));
				}
			}
			Rectangle rectangle = new Rectangle(122, 2904, 5, 5);
			Vector2 vector = base.Pos.XY + new Vector2(20f, (float)(num + 100));
			Viewbox viewbox = new Viewbox(new Marker(ref vector, base.Pos.WH.X - 40f + 5f, base.Pos.WH.Y - 110f - 130f + 10f), rectangle, rectangle, rectangle, CommonAtlas.whitePixel);
			viewbox.AddItem(new UiControl[]
			{
				textBlockBuilder.Create(Vector2.Zero)
			});
			base.AddChild(viewbox);
			this.Button = new Button(Vector2.Zero + new Vector2(0f, 80f), {17625}.c_btLight_big, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChildPos(this.Button, PositionAlignment.Center, PositionAlignment.RightDown, 20f);
			this.SetButtonEnabled(false);
			this.SetButtonText(Local.not_available);
			Action<FractionID> onFractionChanged = this.OnFractionChanged;
			if (onFractionChanged == null)
			{
				return;
			}
			onFractionChanged({20112});
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0006CDFC File Offset: 0x0006AFFC
		public void SetButtonEnabled(bool {20113})
		{
			this.Button.AllowMouseInput = {20113};
			this.Button.Opacity = ({20113} ? 1f : 0.5f);
			this.Button.IsVisible = !string.IsNullOrEmpty(this.Button.Text);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0006CE50 File Offset: 0x0006B050
		public void SetButtonText(string {20114})
		{
			CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
			this.Button.SetText({20114}, philosopher_, Color.Black * 0.9f, false);
			float num = Math.Max(100f, philosopher_.Measure({20114}).X + 20f);
			this.Button.Pos = this.Button.Pos.Offset((this.Button.Pos.WH.X - num) / 2f, 0f).SetWidth(num);
			this.Button.IsVisible = !string.IsNullOrEmpty({20114});
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0006CEFC File Offset: 0x0006B0FC
		private FractionID {20115}(FractionID {20116}, int {20117})
		{
			int num = Array.IndexOf<FractionID>(this.{20121}, {20116});
			return this.{20121}[(num + {20117} + this.{20121}.Length) % this.{20121}.Length];
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0006CF34 File Offset: 0x0006B134
		private static Rectangle GetFractionIcon(FractionID {20118})
		{
			switch ({20118})
			{
			case FractionID.Pirate:
				return {20105}.c_piracyIcon;
			case FractionID.Antilia:
				return {20105}.c_antiliaIcon;
			case FractionID.Espaniol:
				return {20105}.c_espanolIcon;
			case FractionID.KaiAndSeveria:
				return {20105}.c_severiaIcon;
			case FractionID.Empire:
				return {20105}.c_empireIcon;
			case FractionID.TradeUnion:
				return {20105}.c_traderIcon;
			case FractionID.None:
				return {20105}.c_flotiliaIcon;
			default:
				return Rectangle.Empty;
			}
		}

		// Token: 0x04000C4A RID: 3146
		private static readonly Rectangle c_bigContractForm = new Rectangle(1340, 3115, 369, 501);

		// Token: 0x04000C4B RID: 3147
		private static readonly Rectangle c_traderIcon = new Rectangle(3030, 3450, 128, 128);

		// Token: 0x04000C4C RID: 3148
		private static readonly Rectangle c_piracyIcon = new Rectangle(3158, 3450, 128, 128);

		// Token: 0x04000C4D RID: 3149
		private static readonly Rectangle c_empireIcon = new Rectangle(3286, 3450, 128, 128);

		// Token: 0x04000C4E RID: 3150
		private static readonly Rectangle c_severiaIcon = new Rectangle(3030, 3578, 128, 128);

		// Token: 0x04000C4F RID: 3151
		private static readonly Rectangle c_espanolIcon = new Rectangle(3158, 3578, 128, 128);

		// Token: 0x04000C50 RID: 3152
		private static readonly Rectangle c_antiliaIcon = new Rectangle(3286, 3578, 128, 128);

		// Token: 0x04000C51 RID: 3153
		private static readonly Rectangle c_flotiliaIcon = new Rectangle(3030, 3706, 128, 128);

		// Token: 0x04000C52 RID: 3154
		[CompilerGenerated]
		private Button {20119};

		// Token: 0x04000C53 RID: 3155
		[CompilerGenerated]
		private Action<FractionID> {20120};

		// Token: 0x04000C54 RID: 3156
		private readonly FractionID[] {20121};
	}
}
