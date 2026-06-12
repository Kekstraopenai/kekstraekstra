using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Graphics;
using TheraEngine.Grphics.Device;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000A9 RID: 169
	public class Image : UiControl
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x00017281 File Offset: 0x00015481
		public Image(Marker {13257}, Func<Texture2D> {13258}, PositionAlignment {13259} = PositionAlignment.LeftUp, PositionAlignment {13260} = PositionAlignment.LeftUp) : base({13257}, {13259}, {13260}, Color.White, false)
		{
			this.TextureLive = {13258};
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001729A File Offset: 0x0001549A
		public Image(Marker {13261}, Func<Texture2D> {13262}, Func<Rectangle> {13263}, PositionAlignment {13264} = PositionAlignment.LeftUp, PositionAlignment {13265} = PositionAlignment.LeftUp) : base({13261}, {13264}, {13265}, Color.White, false)
		{
			this.TextureLive = {13262};
			this.TexturePathLive = {13263};
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000172BB File Offset: 0x000154BB
		public Image(int {13266}, Texture2D {13267}, Rectangle {13268}, PositionAlignment {13269} = PositionAlignment.LeftUp, PositionAlignment {13270} = PositionAlignment.LeftUp) : this(new Marker(0f, 0f, (float){13266}, (float){13266}), {13267}, {13268}, {13269}, {13270})
		{
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000172DC File Offset: 0x000154DC
		public Image(Marker {13271}, Texture2D {13272}, Rectangle {13273}, PositionAlignment {13274} = PositionAlignment.LeftUp, PositionAlignment {13275} = PositionAlignment.LeftUp) : base({13271}, {13274}, {13275}, Color.White, false)
		{
			this.Texture = {13272};
			this.TexturePath = {13273};
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00017300 File Offset: 0x00015500
		public Image(Vector2 {13276}, Texture2D {13277}, PositionAlignment {13278} = PositionAlignment.LeftUp, PositionAlignment {13279} = PositionAlignment.LeftUp)
		{
			Rectangle bounds = {13277}.Bounds;
			this..ctor(new Marker(ref {13276}, ref bounds), {13277}, {13277}.Bounds, {13278}, {13279});
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001732D File Offset: 0x0001552D
		public Image(Marker {13280}, Texture2D {13281}, PositionAlignment {13282} = PositionAlignment.LeftUp, PositionAlignment {13283} = PositionAlignment.LeftUp) : this({13280}, {13281}, {13281}.Bounds, {13282}, {13283})
		{
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00017340 File Offset: 0x00015540
		public Image(Vector2 {13284}, Texture2D {13285}, Rectangle {13286}, PositionAlignment {13287} = PositionAlignment.LeftUp, PositionAlignment {13288} = PositionAlignment.LeftUp) : this(new Marker(ref {13284}, ref {13286}), {13285}, {13286}, {13287}, {13288})
		{
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00017358 File Offset: 0x00015558
		public Image(Vector2 {13289}, VirtualTexture {13290}, Rectangle {13291}, PositionAlignment {13292} = PositionAlignment.LeftUp, PositionAlignment {13293} = PositionAlignment.LeftUp) : this(new Marker(ref {13289}, ref {13291}), () => {13290}.Tex, () => {13290}.Tex.Bounds, {13292}, {13293})
		{
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00016B89 File Offset: 0x00014D89
		internal override void Update(ref FrameTime {13294}, ref int {13295})
		{
			base.Update(ref {13294}, ref {13295});
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000173A0 File Offset: 0x000155A0
		internal override void Render()
		{
			if (this.TextureLive != null)
			{
				this.Texture = this.TextureLive();
				this.TexturePath = ((this.TexturePathLive == null) ? this.Texture.Bounds : this.TexturePathLive());
			}
			Color color = UiControl.ComputeColor(base.GetBrightness(), base.GetOpcaity(), this.BasicColor);
			if (Engine.GS.CurrentTexture == this.Texture)
			{
				Device gs = Engine.GS;
				Rectangle rectangle = base.Pos.ToRect();
				gs.Draw(this.TexturePath, rectangle, color);
			}
			else
			{
				Device gs2 = Engine.GS;
				Texture2D texture = this.Texture;
				Rectangle rectangle = base.Pos.ToRect();
				gs2.DrawCustomTexture(texture, this.TexturePath, rectangle, color);
			}
			base.Render();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001680A File Offset: 0x00014A0A
		public new void AddChild(UiControl {13296})
		{
			base.AddChild({13296});
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000DD52 File Offset: 0x0000BF52
		protected override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000362 RID: 866
		public Texture2D Texture;

		// Token: 0x04000363 RID: 867
		public Rectangle TexturePath;

		// Token: 0x04000364 RID: 868
		public Func<Texture2D> TextureLive;

		// Token: 0x04000365 RID: 869
		public Func<Rectangle> TexturePathLive;
	}
}
