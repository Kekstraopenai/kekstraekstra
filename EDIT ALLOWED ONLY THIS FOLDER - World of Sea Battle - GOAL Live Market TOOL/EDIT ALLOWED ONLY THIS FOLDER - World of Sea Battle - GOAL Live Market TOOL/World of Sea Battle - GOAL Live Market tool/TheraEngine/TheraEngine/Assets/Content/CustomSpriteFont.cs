using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Assets.Content
{
	// Token: 0x020001AF RID: 431
	public sealed class CustomSpriteFont
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0003613B File Offset: 0x0003433B
		public SpriteFont Font
		{
			get
			{
				return this.baseFont;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00036143 File Offset: 0x00034343
		public SpriteFont AlternativeFont
		{
			get
			{
				return this.alternativeFont;
			}
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0003614C File Offset: 0x0003434C
		internal CustomSpriteFont(SpriteFont {16023}, SpriteFont {16024})
		{
			this.Downscale = (({16023}.Characters.Count > 5000) ? 1.5f : 2f);
			this.baseFont = {16023};
			this.alternativeFont = {16024};
			this.MaxCharSize = this.Measure("A");
			this.{16032} = new HashSet<char>();
			foreach (char item in this.baseFont.Characters)
			{
				this.{16032}.Add(item);
			}
			foreach (char item2 in {16024}.Characters)
			{
				this.{16032}.Add(item2);
			}
			foreach (char item3 in Environment.NewLine)
			{
				this.{16032}.Add(item3);
			}
			if (CustomSpriteFont.AllSpecialCharacters == null)
			{
				CustomSpriteFont.AllSpecialCharacters = new List<string>();
				foreach (char c in {16023}.Characters)
				{
					if (!char.IsLetterOrDigit(c) && !char.IsPunctuation(c) && !char.IsWhiteSpace(c) && !char.IsControl(c))
					{
						CustomSpriteFont.AllSpecialCharacters.Add(c.ToString());
					}
				}
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000362EC File Offset: 0x000344EC
		public Vector2 Measure(string {16025})
		{
			Vector2 vector = this.{16030}({16025}) ? this.baseFont.MeasureString({16025}) : this.alternativeFont.MeasureString({16025});
			vector.X = MathF.Ceiling(vector.X / this.Downscale);
			vector.Y = MathF.Ceiling(vector.Y / this.Downscale);
			return vector;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00036350 File Offset: 0x00034550
		public Vector2 MeasureWithoutDownscale(string {16026})
		{
			if (!this.{16030}({16026}))
			{
				return this.alternativeFont.MeasureString({16026});
			}
			return this.baseFont.MeasureString({16026});
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00036374 File Offset: 0x00034574
		public string Validate(string {16027})
		{
			if (string.IsNullOrEmpty({16027}))
			{
				return {16027};
			}
			foreach (char item in {16027})
			{
				if (!this.{16032}.Contains(item))
				{
					return this.{16028}({16027});
				}
			}
			return {16027};
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000363C0 File Offset: 0x000345C0
		private string {16028}(string {16029})
		{
			StringBuilder stringBuilder = new StringBuilder({16029}.Length);
			foreach (char c in {16029})
			{
				if (this.{16032}.Contains(c))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00036410 File Offset: 0x00034610
		private bool {16030}(string {16031})
		{
			if (this.alternativeFont == null)
			{
				return false;
			}
			foreach (char c in {16031})
			{
				if (c != '\r' && c != '\n' && c != ' ' && !this.baseFont.Characters.Contains(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000867 RID: 2151
		public readonly float Downscale;

		// Token: 0x04000868 RID: 2152
		public const string SC_Skull = "☠";

		// Token: 0x04000869 RID: 2153
		public const string SC_Pen = "✎";

		// Token: 0x0400086A RID: 2154
		public const string SC_Anchor = "⚓";

		// Token: 0x0400086B RID: 2155
		public const string SC_Sabers = "⚔";

		// Token: 0x0400086C RID: 2156
		public const string SC_Message = "✉";

		// Token: 0x0400086D RID: 2157
		public const string SC_Mass = "⚖";

		// Token: 0x0400086E RID: 2158
		public const string SC_Okay = "✔";

		// Token: 0x0400086F RID: 2159
		public const string SC_Points = "◊";

		// Token: 0x04000870 RID: 2160
		public const string SC_Star = "☆";

		// Token: 0x04000871 RID: 2161
		public const string SC_StarFilled = "★";

		// Token: 0x04000872 RID: 2162
		public const string SC_Quad = "❖";

		// Token: 0x04000873 RID: 2163
		public const string SC_Quad2 = "✦";

		// Token: 0x04000874 RID: 2164
		public const string SC_Quad3 = "◈";

		// Token: 0x04000875 RID: 2165
		public const string SC_Cross = "✕";

		// Token: 0x04000876 RID: 2166
		public const string SC_List = "≡";

		// Token: 0x04000877 RID: 2167
		public const string SC_Crown = "♕";

		// Token: 0x04000878 RID: 2168
		public const string SC_Wheel = "⎈";

		// Token: 0x04000879 RID: 2169
		public static List<string> AllSpecialCharacters;

		// Token: 0x0400087A RID: 2170
		internal readonly SpriteFont baseFont;

		// Token: 0x0400087B RID: 2171
		internal readonly SpriteFont alternativeFont;

		// Token: 0x0400087C RID: 2172
		private HashSet<char> {16032};

		// Token: 0x0400087D RID: 2173
		public readonly Vector2 MaxCharSize;
	}
}
