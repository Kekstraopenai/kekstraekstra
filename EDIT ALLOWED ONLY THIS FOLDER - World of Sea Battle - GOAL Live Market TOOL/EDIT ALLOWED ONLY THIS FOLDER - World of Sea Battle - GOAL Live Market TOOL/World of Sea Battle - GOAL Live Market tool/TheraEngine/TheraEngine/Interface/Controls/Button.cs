using System;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Content;
using TheraEngine.Grphics.Device;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000A4 RID: 164
	public class Button : UiControl
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x000162B2 File Offset: 0x000144B2
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x000162BF File Offset: 0x000144BF
		public string Text
		{
			get
			{
				return this.{13108}.Value;
			}
			set
			{
				if (this.{13108} == null || !this.{13108}.TextHasValue)
				{
					throw new InvalidOperationException("call SetText");
				}
				this.{13108}.Include(value, this.{13108}.Font);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x000162F8 File Offset: 0x000144F8
		public CustomSpriteFont Font
		{
			get
			{
				return this.{13108}.Font;
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00016305 File Offset: 0x00014505
		public Button(Marker {13079}, Rectangle {13080}, Color {13081}, PositionAlignment {13082} = PositionAlignment.LeftUp, PositionAlignment {13083} = PositionAlignment.LeftUp) : base({13079}, {13082}, {13083}, {13081}, false)
		{
			this.TexturePath = {13080};
			this.{13108} = new TextEntry();
			this.{13106} = 0f;
			this.TextColor = Color.White;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001633C File Offset: 0x0001453C
		public Button(Vector2 {13084}, Rectangle {13085}, PositionAlignment {13086} = PositionAlignment.LeftUp, PositionAlignment {13087} = PositionAlignment.LeftUp)
		{
			Vector2 vector = new Vector2((float){13085}.Width, (float){13085}.Height);
			this..ctor(new Marker(ref {13084}, ref vector), {13085}, Color.White, {13086}, {13087});
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00016378 File Offset: 0x00014578
		internal override void Update(ref FrameTime {13088}, ref int {13089})
		{
			base.Update(ref {13088}, ref {13089});
			this.{13106} = ((base.InputMode == MouseInputMode.NoFocus || this.TextureFocusedFiller.Width > 0) ? 0f : ((base.InputMode == MouseInputMode.Focused) ? 0.25f : 0f));
			if (this.{13107} != null)
			{
				string text = this.{13107}();
				if (text != this.{13108}.Value)
				{
					this.{13108}.Include(text, this.{13108}.Font);
				}
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00016404 File Offset: 0x00014604
		internal override void Render()
		{
			float opcaity = base.GetOpcaity();
			float brightness = base.GetBrightness();
			Color color = UiControl.ComputeColor(brightness, opcaity, this.BasicColor);
			if (this.TexturePath.Width > 0)
			{
				Device gs = Engine.GS;
				Rectangle rectangle = base.Pos.ToRect();
				gs.Draw(this.TexturePath, rectangle, color);
			}
			if (this.{13106} > 0f)
			{
				Device gs2 = Engine.GS;
				Rectangle rectangle = base.Pos.ToRect();
				Color color2 = UiControl.ComputeColor(brightness + this.{13106}, opcaity, this.BasicColor);
				gs2.Draw(this.TexturePath, rectangle, color2);
			}
			if (this.TextureFocusedFiller.Width > 0 && base.InputMode == MouseInputMode.Focused)
			{
				Device gs3 = Engine.GS;
				Rectangle rectangle = base.Pos.ToRect();
				gs3.Draw(this.TextureFocusedFiller, rectangle, color);
			}
			base.Render();
			if (this.{13108}.TextHasValue)
			{
				Engine.GS.SetFont(this.{13108}.Font);
				Color color3 = UiControl.ComputeColor(brightness + this.{13106}, opcaity, this.TextColor);
				if (!this.{13109})
				{
					Device gs4 = Engine.GS;
					string value = this.{13108}.Value;
					Vector2 vector = base.Pos.Center - this.{13108}.TextSize / 2f;
					gs4.DrawString(value, vector, color3);
				}
				else
				{
					Device gs5 = Engine.GS;
					string value2 = this.{13108}.Value;
					Vector2 vector = new Vector2(base.Pos.XY.X + 1f + Math.Max(0f, (base.Pos.WH.Y - this.{13108}.TextSize.Y) * 0.5f), base.Pos.Center.Y - MathF.Floor(this.{13108}.TextSize.Y / 2f));
					gs5.DrawString(value2, vector, color3);
				}
			}
			if (this.missclickProtectionStatus > 0f && this.missclickProtectionStatus < 1f && base.InputMode == MouseInputMode.Focused)
			{
				float num = base.Pos.WH.X * this.missclickProtectionStatus;
				Device gs6 = Engine.GS;
				Rectangle rectangle = new Marker(base.Pos.Center.X - num * 0.5f, base.Pos.End.Y - 5f, num, 4f).ToRect();
				Color color2 = Color.Lime * opcaity;
				gs6.Draw(this.TexturePath, rectangle, color2);
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000166BA File Offset: 0x000148BA
		public Button SetText(string {13090}, CustomSpriteFont {13091}, Color {13092}, bool {13093} = false)
		{
			if ({13091} == null)
			{
				throw new ArgumentNullException("font");
			}
			this.{13107} = null;
			this.TextColor = {13092};
			this.{13108}.Include({13090}, {13091});
			this.{13109} = {13093};
			return this;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000166F0 File Offset: 0x000148F0
		public Button SetText(string {13094}, float {13095}, CustomSpriteFont[] {13096}, CustomSpriteFont {13097}, Color {13098}, bool {13099} = false)
		{
			int num = Array.FindIndex<CustomSpriteFont>({13096}, (CustomSpriteFont {13110}) => {13110} == {13097});
			if (num == -1)
			{
				num = {13096}.Length - 1;
			}
			for (int i = num; i >= 0; i--)
			{
				CustomSpriteFont {13091} = {13096}[i];
				this.SetText({13094}, {13091}, {13098}, {13099});
				if (this.{13108}.TextSize.X <= {13095})
				{
					return this;
				}
			}
			this.SetText({13094}, {13096}[0], {13098}, {13099});
			return this;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001676C File Offset: 0x0001496C
		public Button SetInnerContent(params UiControl[] {13100})
		{
			base.ClearAllChild();
			UiControl {12952};
			if ({13100}.Length == 1)
			{
				{12952} = {13100}[0];
			}
			else
			{
				StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				foreach (UiControl uiControl in {13100})
				{
					stackForm.AddItem(new UiControl[]
					{
						uiControl
					});
				}
				{12952} = stackForm;
			}
			base.AddChildPos({12952}, PositionAlignment.Center, PositionAlignment.Center, 0f);
			return this;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000167D1 File Offset: 0x000149D1
		public Button SetLiveText(Func<string> {13101}, CustomSpriteFont {13102}, Color {13103}, bool {13104} = false)
		{
			if ({13102} == null)
			{
				throw new ArgumentNullException("font");
			}
			this.{13107} = {13101};
			this.TextColor = {13103};
			this.{13108}.Include({13101}(), {13102});
			this.{13109} = {13104};
			return this;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001680A File Offset: 0x00014A0A
		public new void AddChild(UiControl {13105})
		{
			base.AddChild({13105});
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000DD52 File Offset: 0x0000BF52
		protected override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00016813 File Offset: 0x00014A13
		protected override void CleanResources()
		{
			this.{13108} = null;
			base.CleanResources();
		}

		// Token: 0x0400033C RID: 828
		private const float c_noFocusColorFactor = 0f;

		// Token: 0x0400033D RID: 829
		private const float c_focusColorFactor = 0.25f;

		// Token: 0x0400033E RID: 830
		private const float c_downColorfactor = 0f;

		// Token: 0x0400033F RID: 831
		public Rectangle TexturePath;

		// Token: 0x04000340 RID: 832
		public Rectangle TextureFocusedFiller;

		// Token: 0x04000341 RID: 833
		public Color TextColor;

		// Token: 0x04000342 RID: 834
		private float {13106};

		// Token: 0x04000343 RID: 835
		private Func<string> {13107};

		// Token: 0x04000344 RID: 836
		private TextEntry {13108};

		// Token: 0x04000345 RID: 837
		private bool {13109};
	}
}
