using System;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;

namespace WorldOfSeaBattles.Interface.DebugPanel
{
	// Token: 0x02000596 RID: 1430
	public class DebugConsoleForm : Form
	{
		// Token: 0x0600210E RID: 8462 RVA: 0x00128280 File Offset: 0x00126480
		public DebugConsoleForm(Vector2 pos) : base(new Marker(ref pos, ref DebugConsoleForm.window), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.AnimatedFocus = false;
			base.AddChild(new Form(pos, DebugConsoleForm.window, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
			this.output = new TextBlockBuilder(Fonts.Arial_10, 1f);
			this.input = new DebugConsoleInput(base.Pos.XY + new Vector2(18f, 326f), this);
			Marker marker = DebugConsoleForm.contentFormView;
			Marker pos2 = base.Pos;
			this.cutterForm = new Form(marker.Offset(pos2.XY), AtlasGameGui.rect_asset_whitepixel_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false,
				UseScissor = true,
				BasicColor = Color.Transparent
			};
			pos2 = base.Pos;
			Button button = new Button(new Marker(pos2.End.X - 75f, base.Pos.XY.Y + 3f, 60f, 15f), AtlasGameGui.basicBlueButton, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button.SetText(DebugPanel.Pinned ? "Открепить" : "Закрепить", Fonts.Arial_8, Color.White, false);
			button.EvClick += delegate(ClickUiEventArgs _)
			{
				DebugPanel.Pinned = !DebugPanel.Pinned;
			};
			button.ToolTipState = new ToolTipState(null, "Закрепленное окно не мешает управлению кораблем, т.к. не блокирует мышь и клавиатуру", Array.Empty<ToolTipCharacteristics>());
			base.AddChild(new UiControl[]
			{
				this.input,
				this.cutterForm,
				button
			});
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x0012841C File Offset: 0x0012661C
		public void UserUpdate(ref FrameTime frameTime)
		{
			this.input.UserUpdate(ref frameTime);
			if (InputHelper.LastMouseState.ScrollValue != InputHelper.NowMouseState.ScrollValue)
			{
				this.scrollOffset += (float)(InputHelper.NowMouseState.ScrollValue - InputHelper.LastMouseState.ScrollValue) * 0.1f;
				this.UpdateTextPosition();
			}
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x0012847A File Offset: 0x0012667A
		internal void Set(ConsoleHistory savedHistory, TextBlockBuilder savedContent)
		{
			this.input.History = (savedHistory ?? this.input.History);
			this.output = (savedContent ?? this.output);
			this.BuildOutput();
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x001284B0 File Offset: 0x001266B0
		internal void Write(string msg, Color color)
		{
			if (string.IsNullOrWhiteSpace(msg))
			{
				return;
			}
			if (msg.StartsWith("password "))
			{
				msg = "password " + new string('*', msg.Length - "password ".Length);
			}
			foreach (string msg2 in msg.Split('\n', StringSplitOptions.None))
			{
				this.InnerWrite(msg2, color);
			}
			this.BuildOutput();
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x00128524 File Offset: 0x00126724
		private void InnerWrite(string msg, Color color)
		{
			msg = this.output.defaultFont.Validate(msg);
			this.output.WriteLines(msg, color, this.output.defaultFont, DebugConsoleForm.contentFormView.WH.X - 16f, new float?(0f));
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0012857C File Offset: 0x0012677C
		private void BuildOutput()
		{
			TextBlockControl textBlockControl = this.outputBlock;
			if (textBlockControl != null)
			{
				textBlockControl.RemoveFromContainer();
			}
			this.outputBlock = this.output.Create(this.cutterForm.Pos.XY + base.Pos.XY);
			this.cutterForm.AddChild(this.outputBlock);
			this.UpdateTextPosition();
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x001285E4 File Offset: 0x001267E4
		private void UpdateTextPosition()
		{
			if (this.outputBlock == null)
			{
				return;
			}
			float num = Math.Max(this.outputBlock.Pos.WH.Y - this.cutterForm.Pos.WH.Y, 0f);
			this.scrollOffset = Math.Clamp(this.scrollOffset, 0f, num);
			this.outputBlock.Pos = this.cutterForm.Pos.SetY(this.cutterForm.Pos.XY.Y - (num - this.scrollOffset)).SetHeight(this.outputBlock.Pos.WH.Y);
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x001286A0 File Offset: 0x001268A0
		public TextBlockBuilder GetContent()
		{
			return this.output;
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x001286A8 File Offset: 0x001268A8
		public ConsoleHistory GetHistory()
		{
			return this.input.History;
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x001286B5 File Offset: 0x001268B5
		public void ClearContent()
		{
			this.output.Clear();
			this.BuildOutput();
		}

		// Token: 0x0400201C RID: 8220
		private static readonly Rectangle window = new Rectangle(1210, 230, 500, 350);

		// Token: 0x0400201D RID: 8221
		private static readonly Marker contentFormView = new Marker(6f, 26f, 486f, 296f);

		// Token: 0x0400201E RID: 8222
		private Form cutterForm;

		// Token: 0x0400201F RID: 8223
		private TextBlockBuilder output;

		// Token: 0x04002020 RID: 8224
		private DebugConsoleInput input;

		// Token: 0x04002021 RID: 8225
		private float scrollOffset;

		// Token: 0x04002022 RID: 8226
		private TextBlockControl outputBlock;
	}
}
