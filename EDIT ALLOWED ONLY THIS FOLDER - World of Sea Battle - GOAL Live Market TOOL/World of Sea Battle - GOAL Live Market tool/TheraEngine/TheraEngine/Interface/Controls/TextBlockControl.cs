using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Grphics.Device;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000BF RID: 191
	public class TextBlockControl : UiControl
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x00019CE1 File Offset: 0x00017EE1
		internal TextBlockControl(Marker {13648}, TextBlockBuilder.TextBlockFragment[] {13649}, PositionAlignment {13650} = PositionAlignment.LeftUp, PositionAlignment {13651} = PositionAlignment.LeftUp) : base({13648}, {13650}, {13651}, Color.White, false)
		{
			this.SizeContents = {13648}.WH;
			this.blocks = {13649};
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00016B89 File Offset: 0x00014D89
		internal override void Update(ref FrameTime {13652}, ref int {13653})
		{
			base.Update(ref {13652}, ref {13653});
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00019D08 File Offset: 0x00017F08
		internal override void Render()
		{
			int num = this.blocks.Length;
			Vector2 xy = base.Pos.XY;
			float opcaity = base.GetOpcaity();
			if (this.BackTexturePath.Width > 0)
			{
				Device gs = Engine.GS;
				Rectangle rectangle = base.Pos.Border(this.BackTexturePathBorder).ToRect();
				Color color = this.BackTexturePathColor * opcaity;
				gs.Draw(this.BackTexturePath, rectangle, color);
			}
			Color? color2 = (base.InputMode != MouseInputMode.NoFocus && this.HighlightColor != null) ? new Color?(this.HighlightColor.Value) : null;
			Color {11469} = UiControl.ComputeColor(base.GetBrightness(), opcaity, this.BasicColor);
			for (int i = 0; i < num; i++)
			{
				TextBlockBuilder.TextBlockFragment textBlockFragment = this.blocks[i];
				if (textBlockFragment.Font == null)
				{
					Device gs2 = Engine.GS;
					Texture2D tex = textBlockFragment.Tex;
					Vector2 vector = xy + textBlockFragment.Start;
					Rectangle rectangle = new Marker(ref vector, ref textBlockFragment.Size).ToRect();
					Color color = {11469}.Multiply(textBlockFragment.Color);
					gs2.DrawCustomTexture(tex, textBlockFragment.TexPath, rectangle, color);
				}
				else
				{
					Engine.GS.SetFont(textBlockFragment.Font);
					if (this.ShadowingAmount > 0f)
					{
						Color color3 = {11469}.Multiply(color2 ?? textBlockFragment.Color);
						Device gs3 = Engine.GS;
						string text = textBlockFragment.Text;
						Vector2 vector = xy + textBlockFragment.Start;
						Color color = Color.Black * MathF.Pow((float)color3.A / 255f, 2f) * 0.6f;
						gs3.DrawStringShadowed(text, vector, color);
						Device gs4 = Engine.GS;
						string text2 = textBlockFragment.Text;
						vector = xy + textBlockFragment.Start;
						color = new Color(color3.ToVector4() * new Vector4(1f, 1f, 1f, 0.7f));
						gs4.DrawString(text2, vector, color);
					}
					else
					{
						Device gs5 = Engine.GS;
						string text3 = textBlockFragment.Text;
						Vector2 vector = xy + textBlockFragment.Start;
						Color color = {11469}.Multiply(color2 ?? textBlockFragment.Color);
						gs5.DrawString(text3, vector, color);
					}
				}
			}
			base.Render();
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000E21A File Offset: 0x0000C41A
		protected override bool AllowResize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00019F87 File Offset: 0x00018187
		protected override void CleanResources()
		{
			this.blocks = null;
			base.CleanResources();
		}

		// Token: 0x040003B8 RID: 952
		public Rectangle BackTexturePath;

		// Token: 0x040003B9 RID: 953
		public Color BackTexturePathColor;

		// Token: 0x040003BA RID: 954
		public float BackTexturePathBorder;

		// Token: 0x040003BB RID: 955
		public Color? HighlightColor;

		// Token: 0x040003BC RID: 956
		public float ShadowingAmount;

		// Token: 0x040003BD RID: 957
		internal TextBlockBuilder.TextBlockFragment[] blocks;

		// Token: 0x040003BE RID: 958
		internal Vector2 SizeContents;
	}
}
