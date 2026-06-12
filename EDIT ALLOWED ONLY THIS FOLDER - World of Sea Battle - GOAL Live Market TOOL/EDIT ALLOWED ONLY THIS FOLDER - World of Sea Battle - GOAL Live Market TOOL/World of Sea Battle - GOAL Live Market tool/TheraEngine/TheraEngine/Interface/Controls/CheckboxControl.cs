using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Content;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface.Eventing;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000A6 RID: 166
	public class CheckboxControl : UiControl
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000433 RID: 1075 RVA: 0x00016830 File Offset: 0x00014A30
		// (remove) Token: 0x06000434 RID: 1076 RVA: 0x00016864 File Offset: 0x00014A64
		public static event Action<CheckboxControl> CheckEffectHandler;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000435 RID: 1077 RVA: 0x00016898 File Offset: 0x00014A98
		// (remove) Token: 0x06000436 RID: 1078 RVA: 0x000168CC File Offset: 0x00014ACC
		public static event Action<CheckboxControl> UncheckEffectHandler;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000437 RID: 1079 RVA: 0x00016900 File Offset: 0x00014B00
		// (remove) Token: 0x06000438 RID: 1080 RVA: 0x00016938 File Offset: 0x00014B38
		public event Action<CheckboxCheckedEventArgs> EvCheck
		{
			[CompilerGenerated]
			add
			{
				Action<CheckboxCheckedEventArgs> action = this.{13157};
				Action<CheckboxCheckedEventArgs> action2;
				do
				{
					action2 = action;
					Action<CheckboxCheckedEventArgs> value2 = (Action<CheckboxCheckedEventArgs>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<CheckboxCheckedEventArgs>>(ref this.{13157}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<CheckboxCheckedEventArgs> action = this.{13157};
				Action<CheckboxCheckedEventArgs> action2;
				do
				{
					action2 = action;
					Action<CheckboxCheckedEventArgs> value2 = (Action<CheckboxCheckedEventArgs>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<CheckboxCheckedEventArgs>>(ref this.{13157}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0001696D File Offset: 0x00014B6D
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00016978 File Offset: 0x00014B78
		public bool IsChecked
		{
			get
			{
				return this.{13159};
			}
			set
			{
				if (this.{13159} != value)
				{
					this.{13159} = value;
					CheckboxCheckedEventArgs checkboxCheckedEventArgs = new CheckboxCheckedEventArgs(this, value);
					Action<CheckboxCheckedEventArgs> action = this.{13157};
					if (action != null)
					{
						action(checkboxCheckedEventArgs);
					}
					if (checkboxCheckedEventArgs.UndoCheck)
					{
						this.{13159} = !value;
						return;
					}
					if (value)
					{
						this.{13160}.TexturePath = this.CheckedTexturePath;
					}
					else
					{
						this.{13160}.TexturePath = this.UncheckedTexturePath;
					}
					Marker pos = base.Pos;
					Vector2 vector = new Vector2((float)this.{13160}.TexturePath.Width, (float)this.{13160}.TexturePath.Height);
					base.Pos = new Marker(ref pos.XY, ref vector);
					UiControl uiControl = this.{13160};
					pos = this.{13160}.Pos;
					vector = new Vector2((float)this.{13160}.TexturePath.Width, (float)this.{13160}.TexturePath.Height);
					uiControl.Pos = new Marker(ref pos.XY, ref vector);
				}
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00016A7C File Offset: 0x00014C7C
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00016A84 File Offset: 0x00014C84
		public Rectangle? DisabledMode
		{
			get
			{
				return this.{13158};
			}
			set
			{
				this.{13158} = value;
				if (this.{13158} != null)
				{
					this.{13160}.TexturePath = this.{13158}.Value;
					return;
				}
				if (this.{13159})
				{
					this.{13160}.TexturePath = this.CheckedTexturePath;
					return;
				}
				this.{13160}.TexturePath = this.UncheckedTexturePath;
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00016AE8 File Offset: 0x00014CE8
		public CheckboxControl(Vector2 {13134}, Rectangle {13135}, Rectangle {13136}, bool {13137} = false, PositionAlignment {13138} = PositionAlignment.LeftUp, PositionAlignment {13139} = PositionAlignment.LeftUp) : base(new Marker(ref {13134}, ref {13135}), {13138}, {13139}, Color.White, false)
		{
			this.{13161} = new TextEntry();
			this.{13160} = new Button({13134}, Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.CheckedTexturePath = {13135};
			this.UncheckedTexturePath = {13136};
			this.{13159} = !{13137};
			this.IsChecked = {13137};
			base.EvClick += this.{13155};
			base.AddChild(this.{13160});
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00016B6B File Offset: 0x00014D6B
		public CheckboxControl(Vector2 {13140}, Rectangle {13141}, Rectangle {13142}, string {13143}, CustomSpriteFont {13144}, Color {13145}, bool {13146} = false, PositionAlignment {13147} = PositionAlignment.LeftUp, PositionAlignment {13148} = PositionAlignment.LeftUp) : this({13140}, {13141}, {13142}, {13146}, {13147}, {13148})
		{
			this.SetText({13143}, {13144}, {13145});
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00016B89 File Offset: 0x00014D89
		internal override void Update(ref FrameTime {13149}, ref int {13150})
		{
			base.Update(ref {13149}, ref {13150});
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00016B94 File Offset: 0x00014D94
		internal override void Render()
		{
			if (this.{13161}.TextHasValue)
			{
				Engine.GS.SetFont(this.{13161}.Font);
				Device gs = Engine.GS;
				string value = this.{13161}.Value;
				Color color = this.{13162}.Multiply(this.BasicColor) * (base.GetOpcaity() * ((base.InputMode != MouseInputMode.NoFocus) ? 1.1f : 0.9f));
				gs.DrawString(value, this.{13163}, color);
			}
			this.{13160}.BasicColor = this.BasicColor;
			base.Render();
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00016C29 File Offset: 0x00014E29
		public CheckboxControl SetText(string {13151}, CustomSpriteFont {13152}, Color {13153})
		{
			if ({13152} == null)
			{
				throw new ArgumentNullException();
			}
			this.{13161}.Include({13151}, {13152});
			this.{13162} = {13153};
			this.{13154}();
			return this;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000E21A File Offset: 0x0000C41A
		protected override bool AllowResize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00016C4F File Offset: 0x00014E4F
		protected internal override void MarkerChanged()
		{
			this.{13154}();
			base.MarkerChanged();
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00016C60 File Offset: 0x00014E60
		private void {13154}()
		{
			this.{13163} = new Vector2(base.Pos.XY.X + (float)this.{13160}.TexturePath.Width + 1f + 2f, base.Pos.Center.Y - this.{13161}.TextSize.Y / 2f);
			base.Pos = new Marker(base.Pos.XY.X, base.Pos.XY.Y, (float)this.CheckedTexturePath.Width + (this.{13161}.TextHasValue ? this.{13161}.TextSize.X : 0f), base.Pos.WH.Y);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00016D3C File Offset: 0x00014F3C
		protected override void CleanResources()
		{
			this.{13157} = null;
			base.CleanResources();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00016D4C File Offset: 0x00014F4C
		[CompilerGenerated]
		private void {13155}(ClickUiEventArgs {13156})
		{
			if (this.{13158} != null)
			{
				return;
			}
			if (!this.{13159} && this.{13157} != null)
			{
				CheckboxControl.CheckEffectHandler(this);
			}
			if (this.{13159} && this.{13157} != null)
			{
				CheckboxControl.UncheckEffectHandler(this);
			}
			this.IsChecked = !this.{13159};
		}

		// Token: 0x04000349 RID: 841
		private const float c_OffsetOfButtonToText = 2f;

		// Token: 0x0400034A RID: 842
		[CompilerGenerated]
		private Action<CheckboxCheckedEventArgs> {13157};

		// Token: 0x0400034B RID: 843
		public Rectangle CheckedTexturePath;

		// Token: 0x0400034C RID: 844
		public Rectangle UncheckedTexturePath;

		// Token: 0x0400034D RID: 845
		private Rectangle? {13158};

		// Token: 0x0400034E RID: 846
		private bool {13159};

		// Token: 0x0400034F RID: 847
		private Button {13160};

		// Token: 0x04000350 RID: 848
		private TextEntry {13161};

		// Token: 0x04000351 RID: 849
		private Color {13162};

		// Token: 0x04000352 RID: 850
		private Vector2 {13163};
	}
}
