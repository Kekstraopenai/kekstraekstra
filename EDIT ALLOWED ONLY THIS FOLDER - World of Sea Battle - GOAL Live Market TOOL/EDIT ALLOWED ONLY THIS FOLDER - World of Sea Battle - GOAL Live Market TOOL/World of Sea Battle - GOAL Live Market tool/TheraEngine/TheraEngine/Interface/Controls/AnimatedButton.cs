using System;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Content;
using TheraEngine.Grphics.Device;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000C8 RID: 200
	public class AnimatedButton : Form
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x0001B6F5 File Offset: 0x000198F5
		public AnimatedButton(Marker {13781}, Rectangle {13782}, Rectangle {13783}, Rectangle {13784}, PositionAlignment {13785} = PositionAlignment.LeftUp, PositionAlignment {13786} = PositionAlignment.LeftUp) : base({13781}, Rectangle.Empty, Color.White, {13785}, {13786})
		{
			this.FirstTexturePath = {13782};
			this.SecoundTexturePath = {13783};
			this.FocusedTexturePath = {13784};
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001B724 File Offset: 0x00019924
		public AnimatedButton(Vector2 {13787}, Rectangle {13788}, Rectangle {13789}, Rectangle {13790}, PositionAlignment {13791} = PositionAlignment.LeftUp, PositionAlignment {13792} = PositionAlignment.LeftUp)
		{
			Vector2 vector = new Vector2((float){13788}.Width, (float){13788}.Height);
			this..ctor(new Marker(ref {13787}, ref vector), {13788}, {13789}, {13790}, {13791}, {13792});
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001B75C File Offset: 0x0001995C
		public AnimatedButton(Vector2 {13793}, Rectangle {13794}, Rectangle {13795}, PositionAlignment {13796} = PositionAlignment.LeftUp, PositionAlignment {13797} = PositionAlignment.LeftUp)
		{
			Vector2 vector = new Vector2((float){13794}.Width, (float){13794}.Height);
			this..ctor(new Marker(ref {13793}, ref vector), {13794}, {13794}, {13795}, {13796}, {13797});
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001B793 File Offset: 0x00019993
		internal override void Update(ref FrameTime {13798}, ref int {13799})
		{
			base.Update(ref {13798}, ref {13799});
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001B7A0 File Offset: 0x000199A0
		internal override void Render()
		{
			Rectangle rectangle = base.Pos.ToRect();
			float brightness = base.GetBrightness();
			float opcaity = base.GetOpcaity();
			Color value = UiControl.ComputeColor(brightness, opcaity, this.BasicColor);
			if (base.InputMode == MouseInputMode.Focused || this.ForceFocusedState)
			{
				Engine.GS.Draw(this.FocusedTexturePath, rectangle, value);
			}
			else if (this.FirstTexturePath.Width > 0)
			{
				if (this.FirstTexturePath == this.SecoundTexturePath)
				{
					Engine.GS.Draw(this.FirstTexturePath, rectangle, value);
				}
				else
				{
					float scale = (float)Math.Cos(Engine.Game.GameTotalTimeSec * 5.0) / 2f + 0.5f;
					Engine.GS.Draw(this.FirstTexturePath, rectangle, value);
					Device gs = Engine.GS;
					Color color = value * scale;
					gs.Draw(this.SecoundTexturePath, rectangle, color);
				}
			}
			base.Render();
			if (this.{13804} != null && this.{13804}.TextHasValue)
			{
				Engine.GS.SetFont(this.{13804}.Font);
				value = UiControl.ComputeColor(brightness, opcaity, this.TextColor);
				Vector2 vector;
				if (!this.{13805})
				{
					Device gs2 = Engine.GS;
					string value2 = this.{13804}.Value;
					vector = base.Pos.Center - this.{13804}.TextSize / 2f;
					gs2.DrawString(value2, vector, value);
					return;
				}
				Device gs3 = Engine.GS;
				string value3 = this.{13804}.Value;
				vector = new Vector2(base.Pos.XY.X + this.{13804}.Font.baseFont.Spacing + 1f, base.Pos.Center.Y - this.{13804}.TextSize.Y / 2f);
				gs3.DrawString(value3, vector, value);
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001B99F File Offset: 0x00019B9F
		public AnimatedButton SetText(string {13800}, CustomSpriteFont {13801}, Color {13802}, bool {13803} = false)
		{
			if ({13801} == null)
			{
				throw new ArgumentNullException("font");
			}
			if (this.{13804} == null)
			{
				this.{13804} = new TextEntry();
			}
			this.TextColor = {13802};
			this.{13804}.Include({13800}, {13801});
			this.{13805} = {13803};
			return this;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000DD52 File Offset: 0x0000BF52
		protected override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000179C6 File Offset: 0x00015BC6
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x040003FE RID: 1022
		public Rectangle FirstTexturePath;

		// Token: 0x040003FF RID: 1023
		public Rectangle SecoundTexturePath;

		// Token: 0x04000400 RID: 1024
		public Rectangle FocusedTexturePath;

		// Token: 0x04000401 RID: 1025
		public bool ForceFocusedState;

		// Token: 0x04000402 RID: 1026
		private TextEntry {13804};

		// Token: 0x04000403 RID: 1027
		private bool {13805};

		// Token: 0x04000404 RID: 1028
		public Color TextColor;
	}
}
