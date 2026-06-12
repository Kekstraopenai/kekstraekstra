using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;

namespace WorldOfSeaBattles.Interface.DebugPanel
{
	// Token: 0x02000598 RID: 1432
	public class DebugConsoleInput : Form
	{
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x00128721 File Offset: 0x00126921
		// (set) Token: 0x0600211D RID: 8477 RVA: 0x00128729 File Offset: 0x00126929
		internal ConsoleHistory History { get; set; } = new ConsoleHistory();

		// Token: 0x0600211E RID: 8478 RVA: 0x00128734 File Offset: 0x00126934
		public DebugConsoleInput(Vector2 pos, DebugConsoleForm console) : base(new Marker(ref pos, ref DebugConsoleInput.commandInput), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.console = console;
			base.EvRemoveFromContainer += delegate()
			{
				this.RemoveCommandSuggestions();
			};
			TextBox textBox = new TextBox(base.Pos.XY, DebugConsoleInput.commandInput, Fonts.Arial_10, Color.White, AtlasGameGui.rect_asset_whitepixel_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			textBox.IsEnter = true;
			TextBox {13204} = textBox;
			this.inputPanel = textBox;
			base.AddChild({13204});
			Marker marker = base.Pos.Offset(base.Pos.WH.X + 10f, 30f);
			Vector2 vector = new Vector2(100f, 40f);
			Marker {13184} = marker.Resize(vector);
			this.helpForm = new Form({13184}, AtlasGameGui.rect_asset_whitepixel_1px, Color.Black * 0.5f, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			base.AddChild(this.helpForm);
			this.UpdateHelp();
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x00128850 File Offset: 0x00126A50
		public void UserUpdate(ref FrameTime frameTime)
		{
			if (this.historyIndex == -1 && this.suggestionIndex == -1)
			{
				this.lastRealText = this.inputPanel.Text;
			}
			if (this.inputPanel.IsEnter && this.suggestionIndex != -1)
			{
				this.RemoveCommandSuggestions();
			}
			if (InputHelper.IsClick(Keys.Enter))
			{
				if (this.inputPanel.IsEnter && !string.IsNullOrEmpty(this.inputPanel.Text))
				{
					this.Execute(this.inputPanel.Text);
				}
				else if (!this.inputPanel.IsEnter && this.suggestions.Count > 0 && this.suggestionIndex != -1)
				{
					this.inputPanel.Text = this.lastSuggestion.Complete(this.suggestionIndex);
					this.inputPanel.IsEnter = true;
				}
			}
			if (this.History.Count > 0)
			{
				if (InputHelper.IsClick(Keys.Up))
				{
					this.historyIndex++;
					this.UpdateHistory();
				}
				else if (InputHelper.IsClick(Keys.Down))
				{
					this.historyIndex--;
					this.UpdateHistory();
				}
			}
			if (InputHelper.IsClick(Keys.Tab))
			{
				this.SelectNextSuggestion(InputHelper.NowInputState.IsDown(Keys.LeftShift));
			}
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0012898C File Offset: 0x00126B8C
		private void SelectNextSuggestion(bool reverse)
		{
			this.historyIndex = -1;
			if (this.suggestions.Count == 1)
			{
				this.inputPanel.IsEnter = true;
				this.RemoveCommandSuggestions();
				return;
			}
			if (this.suggestions.Count == 0)
			{
				this.UpdateCommandSuggestions(this.inputPanel.Text);
			}
			if (!reverse)
			{
				this.<SelectNextSuggestion>g__MoveNext|16_0();
			}
			else
			{
				this.<SelectNextSuggestion>g__MovePrev|16_1();
			}
			if (this.suggestions.Count > 0)
			{
				this.inputPanel.Text = this.lastSuggestion.Complete(this.suggestionIndex);
				for (int i = 0; i < this.suggestions.Count; i++)
				{
					this.suggestions[i].BasicColor = ((this.suggestionIndex == i) ? Color.Aqua : Color.White);
				}
				this.inputPanel.IsEnter = false;
			}
			else
			{
				this.inputPanel.IsEnter = true;
				this.RemoveCommandSuggestions();
			}
			this.UpdateHelp();
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x00128A7C File Offset: 0x00126C7C
		private void UpdateHistory()
		{
			this.historyIndex = Math.Clamp(this.historyIndex, -1, this.History.Count);
			if (this.historyIndex == -1)
			{
				this.inputPanel.Text = this.lastRealText;
				return;
			}
			this.inputPanel.Text = this.History.Last(this.historyIndex);
			this.RemoveCommandSuggestions();
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x00128AE4 File Offset: 0x00126CE4
		private void RemoveCommandSuggestions()
		{
			for (int i = 0; i < this.suggestions.Count; i++)
			{
				ToolTip toolTip = this.suggestions[i].ToolTip;
				if (toolTip != null)
				{
					toolTip.CloseIfIsOpen();
				}
				this.suggestions[i].RemoveFromContainer();
			}
			this.suggestions.Clear();
			this.suggestionIndex = -1;
			this.UpdateMarker();
			this.UpdateHelp();
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x00128B54 File Offset: 0x00126D54
		private void UpdateCommandSuggestions(string command = "")
		{
			this.RemoveCommandSuggestions();
			this.lastSuggestion = CommonGlobal.Commands.GetSuggestions(command);
			if (this.lastSuggestion == null)
			{
				return;
			}
			CommandSuggestion[] values = this.lastSuggestion.Values;
			this.suggestionIndex = -1;
			for (int i = 0; i < values.Length; i++)
			{
				int buttonIndex = i;
				Button button = new Button(this.inputPanel.Pos.Offset(0f, this.inputPanel.Pos.WH.Y + 2f + (float)(DebugConsoleInput.commandBack.Height * i)), DebugConsoleInput.commandBack, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button.SetText(" " + values[i].Name, Fonts.Arial_10, Color.White, true);
				button.EvClick += delegate(ClickUiEventArgs x)
				{
					this.suggestionIndex = buttonIndex;
					this.inputPanel.Text = this.lastSuggestion.Complete(this.suggestionIndex);
					CommandSuggestion value = this.lastSuggestion.GetValue(this.suggestionIndex);
					if (value != null && value.ExecuteOnClick)
					{
						this.Execute(this.inputPanel.Text);
					}
					this.RemoveCommandSuggestions();
					this.inputPanel.IsEnter = true;
				};
				button.BasicColor *= 0.1f;
				string description = values[i].Description;
				if (!string.IsNullOrWhiteSpace(description))
				{
					button.ToolTipState = new ToolTipState("", description, Array.Empty<ToolTipCharacteristics>());
				}
				this.suggestions.Add(button);
				this.console.AddChild(button);
			}
			this.UpdateMarker();
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x00128CA4 File Offset: 0x00126EA4
		private void Execute(string command)
		{
			DebugPanel.Execute(command, false);
			this.History.Add(command);
			this.console.Write(command, Color.White);
			this.inputPanel.Text = "";
			this.historyIndex = -1;
			this.RemoveCommandSuggestions();
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x00128CF4 File Offset: 0x00126EF4
		private void UpdateMarker()
		{
			UiControl uiControl = (this.suggestions.Count > 0) ? this.suggestions.Last<Button>() : this.inputPanel;
			base.Pos = base.Pos.SetHeight(uiControl.Pos.End.Y - base.Pos.XY.Y);
			this.console.Pos = this.console.Pos.SetHeight(uiControl.Pos.End.Y - this.console.Pos.XY.Y);
			UiControl getParent = this.console.GetParent;
			if (getParent != null)
			{
				getParent.Pos = getParent.Pos.SetHeight(uiControl.Pos.End.Y - getParent.Pos.XY.Y);
			}
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x00128DEC File Offset: 0x00126FEC
		private void UpdateHelp()
		{
			Color gray = Color.Gray;
			CommandSuggestions commandSuggestions = this.lastSuggestion;
			TextBlockBuilder textBlockBuilder;
			if (commandSuggestions != null && commandSuggestions.Values.Length != 0 && this.suggestionIndex != -1)
			{
				CommandSuggestion value = this.lastSuggestion.GetValue(this.suggestionIndex);
				string text = ((value != null) ? value.Description : null) ?? "";
				textBlockBuilder = TextBlockBuilder.CreateBlock(300f, text, gray, Fonts.Arial_10, 1f);
				this.helpForm.IsVisible = !string.IsNullOrWhiteSpace(text);
			}
			else
			{
				textBlockBuilder = new TextBlockBuilder(Fonts.Arial_10, 1f);
				textBlockBuilder.Write("Новое управление в консоли:", gray);
				textBlockBuilder.WriteLine("Tab - автодополнение и выбор в нем", gray);
				textBlockBuilder.WriteLine("Shift+Tab - обратный порядок выбора", gray);
				textBlockBuilder.WriteLine("Enter - применить автодополненние", gray);
				textBlockBuilder.WriteLine("Up/Down - выбор в истории команд", gray);
				this.helpForm.IsVisible = true;
			}
			this.helpForm.ClearAllChild();
			this.helpForm.AddChildPos(textBlockBuilder.Create(), PositionAlignment.LeftUp, PositionAlignment.LeftUp, 5f);
			this.helpForm.Pos = this.helpForm.GetChildren[0].Pos.Border(5f);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00128F5E File Offset: 0x0012715E
		[CompilerGenerated]
		private void <SelectNextSuggestion>g__MoveNext|16_0()
		{
			this.suggestionIndex++;
			if (this.suggestionIndex >= this.suggestions.Count)
			{
				this.suggestionIndex = 0;
			}
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00128F88 File Offset: 0x00127188
		[CompilerGenerated]
		private void <SelectNextSuggestion>g__MovePrev|16_1()
		{
			this.suggestionIndex--;
			if (this.suggestionIndex < 0)
			{
				this.suggestionIndex = this.suggestions.Count - 1;
			}
		}

		// Token: 0x04002025 RID: 8229
		private static readonly Rectangle commandInput = new Rectangle(1210, 581, 336, 19);

		// Token: 0x04002026 RID: 8230
		private static readonly Rectangle commandBack = new Rectangle(1212, 253, 496, 16);

		// Token: 0x04002027 RID: 8231
		private readonly TextBox inputPanel;

		// Token: 0x04002028 RID: 8232
		private readonly Form helpForm;

		// Token: 0x04002029 RID: 8233
		private readonly DebugConsoleForm console;

		// Token: 0x0400202A RID: 8234
		private readonly List<Button> suggestions = new List<Button>();

		// Token: 0x0400202C RID: 8236
		private int suggestionIndex = -1;

		// Token: 0x0400202D RID: 8237
		private int historyIndex = -1;

		// Token: 0x0400202E RID: 8238
		private string lastRealText;

		// Token: 0x0400202F RID: 8239
		private CommandSuggestions lastSuggestion;
	}
}
