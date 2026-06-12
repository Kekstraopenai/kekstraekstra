using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine.Assets.Content;
using TheraEngine.Input;
using TheraEngine.Interface.Eventing;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000CE RID: 206
	public class KeyInputControl : Button
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001CF59 File Offset: 0x0001B159
		public static bool IsInputElements
		{
			get
			{
				return KeyInputControl.inputCounter != 0;
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000589 RID: 1417 RVA: 0x0001CF64 File Offset: 0x0001B164
		// (remove) Token: 0x0600058A RID: 1418 RVA: 0x0001CF9C File Offset: 0x0001B19C
		public event Action<Keys> EvChangeKey
		{
			[CompilerGenerated]
			add
			{
				Action<Keys> action = this.{14011};
				Action<Keys> action2;
				do
				{
					action2 = action;
					Action<Keys> value2 = (Action<Keys>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Keys>>(ref this.{14011}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Keys> action = this.{14011};
				Action<Keys> action2;
				do
				{
					action2 = action;
					Action<Keys> value2 = (Action<Keys>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Keys>>(ref this.{14011}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001CFD1 File Offset: 0x0001B1D1
		public Keys CurrentKey
		{
			get
			{
				return this.{14012};
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001CFDC File Offset: 0x0001B1DC
		public KeyInputControl(Vector2 {13984}, Rectangle {13985}, Rectangle {13986}, Keys {13987}, CustomSpriteFont {13988}, Func<Keys, string> {13989}, string {13990}, PositionAlignment {13991} = PositionAlignment.LeftUp, PositionAlignment {13992} = PositionAlignment.LeftUp) : this(new Marker(ref {13984}, ref {13985}), {13985}, {13986}, {13987}, {13988}, {13989}, {13990}, {13991}, {13992})
		{
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001D008 File Offset: 0x0001B208
		public KeyInputControl(Marker {13993}, Rectangle {13994}, Rectangle {13995}, Keys {13996}, CustomSpriteFont {13997}, Func<Keys, string> {13998}, string {13999}, PositionAlignment {14000} = PositionAlignment.LeftUp, PositionAlignment {14001} = PositionAlignment.LeftUp) : base({13993}, {13994}, Color.White, {14000}, {14001})
		{
			KeyInputControl <>4__this = this;
			this.{14013} = {13997};
			this.{14016} = {13998};
			this.{14014} = {13994};
			this.{14015} = {13995};
			this.SetKey({13996}, false);
			base.EvClick += delegate(ClickUiEventArgs {14019})
			{
				<>4__this.SetText({13999}, <>4__this.{14013}, Color.White, false);
				<>4__this.TexturePath = <>4__this.{14015};
				if (!<>4__this.{14017})
				{
					KeyInputControl.inputCounter++;
					<>4__this.{14017} = true;
				}
			};
			base.EvRemoveFromContainer += delegate()
			{
				if (<>4__this.{14017} || <>4__this.{14018})
				{
					KeyInputControl.inputCounter--;
				}
			};
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001D088 File Offset: 0x0001B288
		internal override void Update(ref FrameTime {14002}, ref int {14003})
		{
			if (this.{14018})
			{
				KeyInputControl.inputCounter--;
				this.{14018} = false;
			}
			if (this.{14017})
			{
				if (InputHelper.NowMouseState.LeftPressed && base.InputMode == MouseInputMode.NoFocus)
				{
					this.SetKey(this.{14012}, false);
					this.{14010}();
				}
				else if (InputHelper.NowInputState.DownKeys.Length != 0)
				{
					Keys keys = InputHelper.NowInputState.DownKeys[0];
					if (keys != this.{14012} && !string.IsNullOrEmpty(this.{14016}(keys)))
					{
						this.SetKey(keys, true);
					}
					else
					{
						this.SetKey(this.{14012}, false);
					}
					this.{14010}();
				}
			}
			base.Update(ref {14002}, ref {14003});
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001D13C File Offset: 0x0001B33C
		internal override void Render()
		{
			base.Render();
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001D144 File Offset: 0x0001B344
		public void SetKey(Keys {14004}, bool {14005} = false)
		{
			this.{14012} = {14004};
			base.SetText(({14004} == Keys.None) ? "XXX" : this.{14016}({14004}), this.{14013}, ({14004} == Keys.None) ? Color.Orange : Color.White, false);
			if ({14005})
			{
				Action<Keys> action = this.{14011};
				if (action == null)
				{
					return;
				}
				action({14004});
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000B877 File Offset: 0x00009A77
		public new void SetText(string {14006}, CustomSpriteFont {14007}, Color {14008}, bool {14009} = false)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000DD52 File Offset: 0x0000BF52
		protected override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000E21A File Offset: 0x0000C41A
		internal override bool IsDynamicStorage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001D19F File Offset: 0x0001B39F
		private void {14010}()
		{
			if (this.{14017})
			{
				this.{14018} = true;
			}
			this.TexturePath = this.{14014};
			this.{14017} = false;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001D1C3 File Offset: 0x0001B3C3
		protected override void CleanResources()
		{
			this.{14011} = null;
			base.CleanResources();
		}

		// Token: 0x0400042C RID: 1068
		private static int inputCounter;

		// Token: 0x0400042D RID: 1069
		[CompilerGenerated]
		private Action<Keys> {14011};

		// Token: 0x0400042E RID: 1070
		private Keys {14012};

		// Token: 0x0400042F RID: 1071
		private CustomSpriteFont {14013};

		// Token: 0x04000430 RID: 1072
		private Rectangle {14014};

		// Token: 0x04000431 RID: 1073
		private Rectangle {14015};

		// Token: 0x04000432 RID: 1074
		private Func<Keys, string> {14016};

		// Token: 0x04000433 RID: 1075
		private bool {14017};

		// Token: 0x04000434 RID: 1076
		private bool {14018};
	}
}
