using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine.Assets.Content;
using TheraEngine.Assets.Graphics;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface.Eventing;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000C3 RID: 195
	public class TextBox : UiControl
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00019F96 File Offset: 0x00018196
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00019FA0 File Offset: 0x000181A0
		[Nullable(2)]
		private static TextBox Active
		{
			[NullableContext(2)]
			get
			{
				return TextBox.active;
			}
			[NullableContext(2)]
			set
			{
				if (TextBox.EnableEXT)
				{
					if (value == null)
					{
						TextInputEXT.StopTextInput();
					}
					else
					{
						Vector2 vector = Engine.Game.Window.ClientBounds.WidthHeight() / Engine.GS.UIArea.WidthHeight();
						Vector4 vector2 = new Vector4(value.Pos.XY.X, value.Pos.XY.Y, value.Pos.WH.X, value.Pos.WH.Y);
						vector2.X *= vector.X;
						vector2.Y *= vector.Y;
						vector2.Z *= vector.X;
						vector2.W *= vector.Y;
						TextInputEXT.SetInputRectangle(new Rectangle(vector2.X, vector2.Y, vector2.Z, vector2.W, false));
						TextInputEXT.StartTextInput();
					}
				}
				TextBox.active = value;
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001A0A6 File Offset: 0x000182A6
		static TextBox()
		{
			TextInputEXT.TextInput += delegate(char {13758})
			{
				if (TextBox.Active == null)
				{
					return;
				}
				if ({13758} == '\b')
				{
					TextBox.Active.{13710}(true);
					return;
				}
				if ({13758} == '\u007f')
				{
					TextBox.Active.{13710}(false);
					return;
				}
				if ({13758} == '\u0016')
				{
					if (Engine.ClipboardContainsText)
					{
						string clipboardText = Engine.ClipboardText;
						TextBox textBox = TextBox.Active;
						for (int i = 0; i < clipboardText.Length; i++)
						{
							textBox.{13712}(clipboardText[i]);
						}
						return;
					}
				}
				else
				{
					TextBox.Active.{13712}({13758});
				}
			};
			TextInputEXT.TextEditing += delegate(string {13759}, int {13760}, int {13761})
			{
			};
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001A0D8 File Offset: 0x000182D8
		public static bool IsThereInput
		{
			get
			{
				return TextBox.inputCounter != 0;
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060004F5 RID: 1269 RVA: 0x0001A0E4 File Offset: 0x000182E4
		// (remove) Token: 0x060004F6 RID: 1270 RVA: 0x0001A11C File Offset: 0x0001831C
		public event Action EvGotEnter
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{13722};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{13722}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{13722};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{13722}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060004F7 RID: 1271 RVA: 0x0001A154 File Offset: 0x00018354
		// (remove) Token: 0x060004F8 RID: 1272 RVA: 0x0001A18C File Offset: 0x0001838C
		public event Action EvLostEnter
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{13723};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{13723}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{13723};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{13723}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060004F9 RID: 1273 RVA: 0x0001A1C4 File Offset: 0x000183C4
		// (remove) Token: 0x060004FA RID: 1274 RVA: 0x0001A1FC File Offset: 0x000183FC
		public event Action<string> EvTextChanged
		{
			[CompilerGenerated]
			add
			{
				Action<string> action = this.{13724};
				Action<string> action2;
				do
				{
					action2 = action;
					Action<string> value2 = (Action<string>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<string>>(ref this.{13724}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<string> action = this.{13724};
				Action<string> action2;
				do
				{
					action2 = action;
					Action<string> value2 = (Action<string>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<string>>(ref this.{13724}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060004FB RID: 1275 RVA: 0x0001A234 File Offset: 0x00018434
		// (remove) Token: 0x060004FC RID: 1276 RVA: 0x0001A26C File Offset: 0x0001846C
		public event Action EvEntering
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{13725};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{13725}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{13725};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{13725}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001A2A1 File Offset: 0x000184A1
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0001A2A9 File Offset: 0x000184A9
		public ExpandoTextureLinePath Multitexture
		{
			get
			{
				return this.{13740};
			}
			set
			{
				if (value == null)
				{
					this.{13740} = null;
					this.{13739} = false;
					return;
				}
				this.{13740} = value;
				this.{13739} = true;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0001A2CB File Offset: 0x000184CB
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x0001A2D3 File Offset: 0x000184D3
		public ExpandoTexturePath Multitexture2
		{
			get
			{
				return this.{13741};
			}
			set
			{
				if (value == null)
				{
					this.{13741} = null;
					this.{13739} = false;
					return;
				}
				this.{13741} = value;
				this.{13739} = true;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0001A2F5 File Offset: 0x000184F5
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0001A2FD File Offset: 0x000184FD
		public string Text
		{
			get
			{
				return this.{13726};
			}
			set
			{
				if (this.{13726} != value)
				{
					this.{13743} = value.Length;
					this.{13704}(value);
				}
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0001A320 File Offset: 0x00018520
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x0001A328 File Offset: 0x00018528
		public bool IsEnter
		{
			get
			{
				return this.{13727};
			}
			set
			{
				if (this.{13727} != value)
				{
					this.{13727} = value;
					if (value)
					{
						this.{13728} = 1.1f;
						TextBox.inputCounter++;
						TextBox.Active = this;
						Action action = this.{13722};
						if (action == null)
						{
							return;
						}
						action();
						return;
					}
					else
					{
						TextBox.inputCounter--;
						Action action2 = this.{13723};
						if (action2 != null)
						{
							action2();
						}
						if (TextBox.Active == this)
						{
							TextBox.Active = null;
						}
					}
				}
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0001A3A1 File Offset: 0x000185A1
		public int CursorPosition
		{
			get
			{
				return this.{13743};
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0001A3A9 File Offset: 0x000185A9
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0001A3B1 File Offset: 0x000185B1
		public TextBoxMaskType Mask
		{
			get
			{
				return this.{13744};
			}
			set
			{
				this.{13744} = value;
				this.{13706}();
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0001A3C0 File Offset: 0x000185C0
		public bool HasModeratorError
		{
			get
			{
				return this.{13747} != null;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0001A3CC File Offset: 0x000185CC
		public int ParseInt
		{
			get
			{
				int result;
				if (!int.TryParse(this.{13726}, out result))
				{
					return 0;
				}
				return result;
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001A3EC File Offset: 0x000185EC
		public TextBox(Marker {13682}, Rectangle {13683}, CustomSpriteFont {13684}, Color {13685}, Rectangle {13686}, PositionAlignment {13687} = PositionAlignment.LeftUp, PositionAlignment {13688} = PositionAlignment.LeftUp) : base({13682}, {13687}, {13688}, Color.White, false)
		{
			this.PathTexture = {13683};
			this.PathCursor = {13686};
			this.Font = {13684};
			this.FontColor = {13685};
			this.{13726} = string.Empty;
			this.{13727} = false;
			this.{13728} = 1f;
			this.{13744} = TextBoxMaskType.Disabled;
			this.{13733} = 600f;
			this.{13735} = 600f;
			this.CharsMode = SpecialCharMode.Default;
			this.AttachMaxLengthModerator(128, null, Color.Transparent);
			this.Border = 5f;
			this.LineMaxSizeMode = DirectionDef.Zero;
			this.{13737} = {13682}.WH.X;
			this.{13738} = {13682}.XY.X;
			this.{13740} = null;
			this.{13739} = false;
			this.isMarkerChangedCall = true;
			this.{13706}();
			this.isMarkerChangedCall = false;
			base.EvClick += this.{13719};
			base.EvRemoveFromContainer += this.{13721};
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001A4FB File Offset: 0x000186FB
		public TextBox(Vector2 {13689}, Rectangle {13690}, CustomSpriteFont {13691}, Color {13692}, Rectangle {13693}, PositionAlignment {13694} = PositionAlignment.LeftUp, PositionAlignment {13695} = PositionAlignment.LeftUp) : this(new Marker(ref {13689}, ref {13690}), {13690}, {13691}, {13692}, {13693}, {13694}, {13695})
		{
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001A518 File Offset: 0x00018718
		internal override void Update(ref FrameTime {13696}, ref int {13697})
		{
			base.Update(ref {13696}, ref {13697});
			if (!this.{13727})
			{
				this.{13728} = ((base.InputMode == MouseInputMode.NoFocus) ? 0.7f : 1f);
				return;
			}
			if (base.InputMode == MouseInputMode.NoFocus && (InputHelper.NowMouseState.LeftPressed || InputHelper.NowMouseState.RightPressed))
			{
				this.IsEnter = false;
				return;
			}
			this.{13708}({13696}.msElapsed);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001A584 File Offset: 0x00018784
		internal override void Render()
		{
			float opcaity = base.GetOpcaity();
			float brightness = base.GetBrightness();
			Color color = UiControl.ComputeColor(brightness, opcaity, this.BasicColor * this.{13728});
			if (this.{13739} && !this.EnabledMultipliedLines)
			{
				this.{13740}.Render(base.Pos, color);
			}
			else
			{
				Device gs = Engine.GS;
				Marker pos = base.Pos;
				Rectangle rectangle = pos.ToRect();
				gs.Draw(this.PathTexture, rectangle, color);
			}
			if (!string.IsNullOrEmpty(this.{13729}))
			{
				Engine.GS.SetFont(this.Font);
				if (this.EnabledMultipliedLines && this.{13742} != null)
				{
					Marker pos;
					Vector2 vector;
					if (this.{13739})
					{
						if (this.{13741} != null)
						{
							ExpandoTexturePath expandoTexturePath = this.{13741};
							pos = base.Pos;
							vector = new Vector2(base.Pos.WH.X, Math.Max(this.{13742}.SizeContents.Y, base.Pos.WH.Y));
							expandoTexturePath.Render(new Marker(ref pos.XY, ref vector), color, null);
						}
						else
						{
							ExpandoTextureLinePath expandoTextureLinePath = this.{13740};
							pos = base.Pos;
							vector = new Vector2(base.Pos.WH.X, Math.Max(this.{13742}.SizeContents.Y, base.Pos.WH.Y));
							expandoTextureLinePath.Render(new Marker(ref pos.XY, ref vector), color);
						}
					}
					UiControl uiControl = this.{13742};
					pos = base.Pos;
					vector = new Vector2(2f, 0f);
					uiControl.Pos = pos.Offset(vector);
					this.{13742}.Render();
				}
				else
				{
					Device gs2 = Engine.GS;
					string {14599} = this.{13729};
					Color color2 = UiControl.ComputeColor(brightness, opcaity, this.FontColor * this.{13728});
					gs2.DrawString({14599}, this.{13730}, color2);
				}
			}
			else if (!string.IsNullOrEmpty(this.DefaultText))
			{
				Engine.GS.SetFont(this.Font);
				Device gs3 = Engine.GS;
				string defaultText = this.DefaultText;
				Color color2 = UiControl.ComputeColor(brightness, opcaity, this.FontColor * 0.4f);
				gs3.DrawString(defaultText, this.{13730}, color2);
			}
			if (this.{13727})
			{
				Rectangle rectangle2;
				rectangle2.X = (int)(base.Pos.XY.X + this.{13732}.X);
				rectangle2.Y = (int)(base.Pos.XY.Y + this.{13732}.Y);
				rectangle2.Width = 2;
				rectangle2.Height = (int)(this.Font.MaxCharSize.Y - 3f);
				float scale = 1f + (float)Math.Sin(Engine.Game.GameTotalTimeSec * 4.0) * 0.8f;
				Device gs4 = Engine.GS;
				Color color2 = this.FontColor * scale;
				gs4.Draw(this.PathCursor, rectangle2, color2);
			}
			if (!string.IsNullOrEmpty(this.{13747}))
			{
				Engine.GS.SetFont(Fonts.Arial_10);
				Device gs5 = Engine.GS;
				string {14599}2 = this.{13747};
				Vector2 vector = this.{13730} + new Vector2(0f, Math.Max(base.Pos.WH.Y - 10f, this.Font.MaxCharSize.Y));
				Color color2 = UiControl.ComputeColor(brightness, opcaity, this.{13746});
				gs5.DrawString({14599}2, vector, color2);
			}
			base.Render();
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001A90F File Offset: 0x00018B0F
		public void AttachModerator(TextBox.Moderator {13698}, Color {13699})
		{
			this.{13745} = {13698};
			this.{13746} = {13699};
			this.{13704}(this.{13726});
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001A92C File Offset: 0x00018B2C
		public void AttachMaxLengthModerator(int {13700}, string {13701}, Color {13702})
		{
			this.{13745} = delegate(ref string {13763})
			{
				if ({13763}.Length > {13700})
				{
					if ({13701} != null)
					{
						if ({13763}.Length > {13700} + 10)
						{
							{13763} = {13763}.Substring(0, {13700} + 10);
						}
						return string.Format({13701}, {13700});
					}
					{13763} = {13763}.Substring(0, {13700});
				}
				return null;
			};
			this.{13746} = {13702};
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001A966 File Offset: 0x00018B66
		public void SetCustomErrorUntilTextChange(string {13703})
		{
			this.{13747} = {13703};
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001A970 File Offset: 0x00018B70
		private void {13704}(string {13705})
		{
			this.{13726} = (string.IsNullOrEmpty({13705}) ? string.Empty : {13705});
			this.{13726} = this.Font.Validate(this.{13726});
			if (this.{13744} == TextBoxMaskType.Count && this.{13726}.Length > 1 && this.{13726}[0] == '0')
			{
				this.{13726} = this.{13726}.Substring(1);
			}
			this.{13747} = null;
			if (this.{13745} != null)
			{
				this.{13747} = this.{13745}(ref this.{13726});
			}
			if (this.WordSizeLimit && this.{13726}.Length > 30)
			{
				int num = 0;
				for (int i = 0; i < this.{13726}.Length; i++)
				{
					if (this.{13726}[i] == ' ')
					{
						num = i;
					}
					if (i - num > 30)
					{
						this.{13726} = this.{13726}.Insert(i, " ");
						num = i;
					}
				}
			}
			Action<string> action = this.{13724};
			if (action != null)
			{
				action(this.{13726});
			}
			this.{13743} = Math.Min(this.{13726}.Length, this.{13743});
			this.isMarkerChangedCall = true;
			this.{13706}();
			this.isMarkerChangedCall = false;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000E21A File Offset: 0x0000C41A
		protected override bool AllowResize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001AAB4 File Offset: 0x00018CB4
		private void {13706}()
		{
			this.{13729} = this.{13726};
			if (this.{13744} == TextBoxMaskType.Password && !string.IsNullOrEmpty(this.{13729}))
			{
				int length = this.{13729}.Length;
				char[] array = new char[length];
				for (int i = 0; i < length; i++)
				{
					array[i] = '*';
				}
				this.{13729} = new string(array);
			}
			this.{13731} = this.Font.Measure(this.{13729});
			Vector2 vector = (string.IsNullOrEmpty(this.{13726}) || this.{13743} == 0) ? new Vector2(0f, this.{13731}.Y) : this.Font.Measure(this.{13729}.Substring(0, this.{13743}));
			if (this.LineMaxSizeMode != DirectionDef.Zero)
			{
				float num = this.{13731}.X - this.{13737} + this.Border * 2f;
				if (num < 0f)
				{
					base.Pos = base.Pos.SetWidth(this.{13737});
					if (this.LineMaxSizeMode == DirectionDef.Left)
					{
						base.Pos = base.Pos.SetX(this.{13738});
					}
				}
				else
				{
					base.Pos = base.Pos.SetWidth(this.{13737} + num + this.Border);
					if (this.LineMaxSizeMode == DirectionDef.Left)
					{
						base.Pos = base.Pos.SetX(this.{13738} - num - this.Border);
					}
				}
			}
			else if (this.{13731}.X > this.{13737} && !this.EnabledMultipliedLines)
			{
				float num2 = (base.Pos.WH.X - this.Border * 2f) / (this.{13731}.X / (float)this.{13729}.Length * 0.5f) - 3f;
				if (!string.IsNullOrEmpty(this.{13729}) && (float)this.{13729}.Length > num2)
				{
					this.{13729} = "<< " + this.{13729}.Substring(this.{13729}.Length - (int)num2 - 1);
				}
			}
			this.{13732}.X = this.Border + vector.X;
			this.{13707}();
			this.{13732}.Y = this.{13730}.Y - base.Pos.XY.Y + 1f;
			if (this.EnabledMultipliedLines)
			{
				if (this.{13729}.Length == 0)
				{
					this.{13742} = new TextBlockControl(Marker.OneUnit, new TextBlockBuilder.TextBlockFragment[0], PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				}
				else
				{
					this.{13742} = TextBlockBuilder.CreateBlock(base.Pos.WH.X - this.Border * 2f - 1f, this.{13726}, this.FontColor, this.Font, 0f).Create(Vector2.Zero);
				}
				this.{13742}.RemoveFromContainer();
				int num3 = 0;
				foreach (TextBlockBuilder.TextBlockFragment textBlockFragment in this.{13742}.blocks)
				{
					if (num3 + textBlockFragment.Text.Length > this.{13743})
					{
						this.{13732}.X = this.Border + textBlockFragment.Start.X + ((this.{13742}.blocks.Length == 0) ? 0f : textBlockFragment.Font.Measure(textBlockFragment.Text.Substring(0, this.{13743} - num3)).X) - 4f;
						this.{13732}.Y = this.Border + textBlockFragment.Start.Y - 4f;
						return;
					}
					num3 += textBlockFragment.Text.Length;
				}
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001AEB4 File Offset: 0x000190B4
		private void {13707}()
		{
			this.{13730} = new Vector2(base.Pos.XY.X + this.Font.baseFont.Spacing + this.Border, base.Pos.XY.Y + base.Pos.WH.Y / 2f - this.Font.MaxCharSize.Y / 2f);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001AF32 File Offset: 0x00019132
		protected internal override void MarkerChanged()
		{
			this.{13707}();
			this.{13737} = base.Pos.WH.X;
			this.{13738} = base.Pos.XY.X;
			base.MarkerChanged();
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001AF6C File Offset: 0x0001916C
		private void {13708}(float {13709})
		{
			if (this.{13725} != null && InputHelper.IsClick(Keys.Enter))
			{
				this.{13725}();
			}
			this.{13735} = Math.Min(this.{13735} + {13709} * 0.5f, 450f);
			if ((!TextBox.EnableEXT && InputHelper.NowInputState.IsDown(Keys.LeftControl) && InputHelper.IsClick(Keys.V)) || (InputHelper.NowInputState.IsDown(Keys.V) && InputHelper.IsClick(Keys.LeftControl) && Engine.ClipboardContainsText))
			{
				string clipboardText = Engine.ClipboardText;
				for (int i = 0; i < clipboardText.Length; i++)
				{
					this.{13712}(clipboardText[i]);
				}
				return;
			}
			Keys[] downKeys = InputHelper.NowInputState.DownKeys;
			if (downKeys != null && downKeys.Length != 0)
			{
				foreach (Keys keys in downKeys)
				{
					if (InputHelper.IsClick(keys))
					{
						if (!TextBox.EnableEXT && (keys == Keys.Back || keys == Keys.Delete) && this.{13726}.Length != 0)
						{
							this.{13710}(keys == Keys.Back);
						}
						if (keys == Keys.Left)
						{
							this.{13743} = Math.Max(0, this.{13743} - 1);
							this.isMarkerChangedCall = true;
							this.{13706}();
							this.isMarkerChangedCall = false;
						}
						if (keys == Keys.Right)
						{
							this.{13743} = Math.Min(this.{13743} + 1, this.{13726}.Length);
							this.isMarkerChangedCall = true;
							this.{13706}();
							this.isMarkerChangedCall = false;
						}
					}
					if (keys == Keys.Left || keys == Keys.Right)
					{
						if (this.{13735} > 80f)
						{
							this.{13736} = 0f;
						}
						this.{13735} -= {13709};
						this.{13736} = Math.Min(4f, this.{13736} + {13709} / 600f);
						if (this.{13735} < 0f)
						{
							this.{13735} += 80f / (1f + MathF.Pow(2f * this.{13736}, 0.6f));
							if (keys == Keys.Left)
							{
								this.{13743} = Math.Max(0, this.{13743} - 1);
							}
							if (keys == Keys.Right)
							{
								this.{13743} = Math.Min(this.{13743} + 1, this.{13726}.Length);
							}
							this.isMarkerChangedCall = true;
							this.{13706}();
							this.isMarkerChangedCall = false;
						}
					}
					else
					{
						this.{13735} = 600f;
						this.{13736} = 0f;
					}
					if (keys == Keys.Home)
					{
						this.{13743} = 0;
					}
					if (keys == Keys.End)
					{
						this.{13743} = this.{13726}.Length;
					}
					if (!TextBox.EnableEXT && (InputHelper.NowInputState.IsDown(Keys.Back) || InputHelper.NowInputState.IsDown(Keys.Delete)))
					{
						this.{13733} -= {13709};
						this.{13734} = Math.Min(500f, this.{13734} + {13709} * 0.25f);
						if (this.{13733} < 0f)
						{
							this.{13733} = 40f + (500f - this.{13734});
							this.{13710}(InputHelper.NowInputState.IsDown(Keys.Back));
						}
					}
				}
				if (!TextBox.EnableEXT)
				{
					string text = KeyboardTextInputHelper.Sample(InputHelper.NowInputState.DownKeys, InputHelper.LastInputState.DownKeys, this.{13744} == TextBoxMaskType.Count);
					if (!string.IsNullOrEmpty(text))
					{
						foreach (char {13713} in text)
						{
							this.{13712}({13713});
						}
					}
				}
			}
			if (InputHelper.NowInputState.IsUp(Keys.Delete) && InputHelper.NowInputState.IsUp(Keys.Back))
			{
				this.{13733} = 600f;
				this.{13734} = 0f;
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001B320 File Offset: 0x00019520
		private void {13710}(bool {13711})
		{
			if (this.{13726}.Length == 0)
			{
				return;
			}
			if ({13711})
			{
				if (this.{13743} == 0)
				{
					return;
				}
				this.{13743}--;
				StringBuilder stringBuilder = new StringBuilder(this.{13726});
				stringBuilder.Remove(this.{13743}, 1);
				this.{13704}(stringBuilder.ToString());
				return;
			}
			else
			{
				if (this.{13743} == this.{13726}.Length)
				{
					return;
				}
				StringBuilder stringBuilder2 = new StringBuilder(this.{13726});
				stringBuilder2.Remove(this.{13743}, 1);
				this.{13704}(stringBuilder2.ToString());
				return;
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001B3B8 File Offset: 0x000195B8
		private void {13712}(char {13713})
		{
			if (!this.{13714}({13713}))
			{
				return;
			}
			this.{13743}++;
			if (this.{13743} - 1 == this.{13726}.Length || string.IsNullOrEmpty(this.{13726}))
			{
				ReadOnlySpan<char> str = this.{13726};
				char c = {13713};
				this.{13704}(str + new ReadOnlySpan<char>(ref c));
				return;
			}
			this.{13704}(this.{13726}.Insert(this.{13743} - 1, {13713}.ToString()));
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001B440 File Offset: 0x00019640
		private bool {13714}(char {13715})
		{
			if ({13715} == '\b')
			{
				return false;
			}
			if (!this.AllowNewLine && ({13715} == '\r' || {13715} == '\n'))
			{
				return false;
			}
			bool flag = KeyboardTextInputHelper.IsOfSupportedDigits({13715});
			bool flag2 = this.CharsMode == SpecialCharMode.Default || (KeyboardTextInputHelper.IsKeyboardInputChar({13715}) || flag);
			bool flag3 = this.{13744} != TextBoxMaskType.Count || flag;
			flag2 = (flag2 || (this.CharsMode == SpecialCharMode.OnlyWorldAndNumersAndSpace && " .,-_".Contains({13715})) || (this.CharsMode == SpecialCharMode.SlimCharMap && KeyboardTextInputHelper.IsOfSlimCharMap({13715})) || (this.CharsMode == SpecialCharMode.OnlyWordsAndNumbers && ".,-_".Contains({13715})));
			return flag3 && flag2;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001B4D8 File Offset: 0x000196D8
		public static float NonLettersAmount(string {13716})
		{
			int num = 0;
			int length = {13716}.Length;
			for (int i = 0; i < length; i++)
			{
				char c = {13716}[i];
				if (KeyboardTextInputHelper.IsOfSupportedDigits(c) || KeyboardTextInputHelper.IsOfFullCharMap(c) || c == ' ')
				{
					num++;
				}
			}
			return (float)num / (float)length;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001B520 File Offset: 0x00019720
		public float NonLettersAmount()
		{
			return TextBox.NonLettersAmount(this.{13726});
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001B530 File Offset: 0x00019730
		public TextBox BindNumerticChange(int {13717}, Action<int> {13718})
		{
			this.{13744} = TextBoxMaskType.Count;
			this.Text = {13717}.ToString();
			this.EvTextChanged += delegate(string {13762})
			{
				int obj;
				if (int.TryParse({13762}, out obj))
				{
					{13718}(obj);
				}
			};
			return this;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001B574 File Offset: 0x00019774
		protected override void CleanResources()
		{
			if (TextBox.Active == this)
			{
				TextBox.Active = null;
			}
			this.{13724} = null;
			this.{13722} = null;
			this.{13723} = null;
			this.{13725} = null;
			if (this.IsEnter)
			{
				this.IsEnter = false;
			}
			base.CleanResources();
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001B5C0 File Offset: 0x000197C0
		[CompilerGenerated]
		private void {13719}(ClickUiEventArgs {13720})
		{
			if (!this.{13727})
			{
				this.IsEnter = true;
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001B5D1 File Offset: 0x000197D1
		[CompilerGenerated]
		private void {13721}()
		{
			this.IsEnter = false;
		}

		// Token: 0x040003CC RID: 972
		private static bool EnableEXT = true;

		// Token: 0x040003CD RID: 973
		private static TextBox active;

		// Token: 0x040003CE RID: 974
		private static int inputCounter;

		// Token: 0x040003CF RID: 975
		private const float c_NoFocus_AddColor = 0.7f;

		// Token: 0x040003D0 RID: 976
		private const float c_Focus_AddColor = 1f;

		// Token: 0x040003D1 RID: 977
		private const float c_Enter_AddColor = 1.1f;

		// Token: 0x040003D2 RID: 978
		private const float c_BackspaceTimer_ms = 600f;

		// Token: 0x040003D3 RID: 979
		private const float c_BackspaceMinTimer_ms = 80f;

		// Token: 0x040003D4 RID: 980
		private const float c_BackspaceSleep_ms = 70f;

		// Token: 0x040003D5 RID: 981
		[CompilerGenerated]
		private Action {13722};

		// Token: 0x040003D6 RID: 982
		[CompilerGenerated]
		private Action {13723};

		// Token: 0x040003D7 RID: 983
		[CompilerGenerated]
		private Action<string> {13724};

		// Token: 0x040003D8 RID: 984
		[CompilerGenerated]
		private Action {13725};

		// Token: 0x040003D9 RID: 985
		public Rectangle PathTexture;

		// Token: 0x040003DA RID: 986
		public Rectangle PathCursor;

		// Token: 0x040003DB RID: 987
		public CustomSpriteFont Font;

		// Token: 0x040003DC RID: 988
		public Color FontColor;

		// Token: 0x040003DD RID: 989
		public string DefaultText;

		// Token: 0x040003DE RID: 990
		public SpecialCharMode CharsMode;

		// Token: 0x040003DF RID: 991
		public float Border;

		// Token: 0x040003E0 RID: 992
		public DirectionDef LineMaxSizeMode;

		// Token: 0x040003E1 RID: 993
		public bool EnabledMultipliedLines;

		// Token: 0x040003E2 RID: 994
		public bool AllowNewLine;

		// Token: 0x040003E3 RID: 995
		public bool WordSizeLimit = true;

		// Token: 0x040003E4 RID: 996
		private string {13726};

		// Token: 0x040003E5 RID: 997
		private bool {13727};

		// Token: 0x040003E6 RID: 998
		private float {13728};

		// Token: 0x040003E7 RID: 999
		private string {13729};

		// Token: 0x040003E8 RID: 1000
		private Vector2 {13730};

		// Token: 0x040003E9 RID: 1001
		private Vector2 {13731};

		// Token: 0x040003EA RID: 1002
		private Vector2 {13732};

		// Token: 0x040003EB RID: 1003
		private float {13733};

		// Token: 0x040003EC RID: 1004
		private float {13734};

		// Token: 0x040003ED RID: 1005
		private float {13735};

		// Token: 0x040003EE RID: 1006
		private float {13736};

		// Token: 0x040003EF RID: 1007
		private float {13737};

		// Token: 0x040003F0 RID: 1008
		private float {13738};

		// Token: 0x040003F1 RID: 1009
		private bool {13739};

		// Token: 0x040003F2 RID: 1010
		private ExpandoTextureLinePath {13740};

		// Token: 0x040003F3 RID: 1011
		private ExpandoTexturePath {13741};

		// Token: 0x040003F4 RID: 1012
		private TextBlockControl {13742};

		// Token: 0x040003F5 RID: 1013
		private int {13743};

		// Token: 0x040003F6 RID: 1014
		private TextBoxMaskType {13744};

		// Token: 0x040003F7 RID: 1015
		private TextBox.Moderator {13745};

		// Token: 0x040003F8 RID: 1016
		private Color {13746};

		// Token: 0x040003F9 RID: 1017
		private string {13747};

		// Token: 0x020000C4 RID: 196
		// (Invoke) Token: 0x06000521 RID: 1313
		public delegate string Moderator(ref string {13752});
	}
}
