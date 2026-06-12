using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Helpers;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000B9 RID: 185
	public class TextBlockBuilder
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x000186FD File Offset: 0x000168FD
		public Vector2 CurrentLocalPoint
		{
			get
			{
				return this.{13634};
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00018705 File Offset: 0x00016905
		public Vector2 Size
		{
			get
			{
				return this.{13635};
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00018710 File Offset: 0x00016910
		public TextBlockBuilder(CustomSpriteFont {13552}, float {13553} = 0f)
		{
			if ({13552} == null)
			{
				throw new ArgumentNullException("defaultFont");
			}
			this.defaultFont = {13552};
			this.{13633} = {13553};
			this.blocks = new Tlist<TextBlockBuilder.TextBlockFragment>();
			this.{13634} = Vector2.Zero;
			this.{13636} = 0;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001875C File Offset: 0x0001695C
		public void WriteSpaceLine(float {13554})
		{
			this.{13634}.X = 0f;
			this.{13634}.Y = this.{13634}.Y + {13554};
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00018780 File Offset: 0x00016980
		public void Write(string {13555}, Color {13556})
		{
			this.Write({13555}, {13556}, this.defaultFont, null, false);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000187A5 File Offset: 0x000169A5
		public void Write(string {13557}, Color {13558}, float {13559}, bool {13560} = false)
		{
			this.Write({13557}, {13558}, this.defaultFont, new float?({13559}), {13560});
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000187BD File Offset: 0x000169BD
		public void WriteLine()
		{
			this.WriteLine(" ", Color.White, this.defaultFont);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000187D5 File Offset: 0x000169D5
		public void WriteLine(string {13561}, Color {13562})
		{
			this.WriteLine({13561}, {13562}, this.defaultFont);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000187E8 File Offset: 0x000169E8
		public void Write(string {13563}, Color {13564}, CustomSpriteFont {13565}, float? {13566} = null, bool {13567} = false)
		{
			if (this.replaceFontOrNull != null)
			{
				{13565} = this.replaceFontOrNull;
			}
			{13563} = {13565}.Validate({13563});
			TextBlockBuilder.TextBlockFragment textBlockFragment = default(TextBlockBuilder.TextBlockFragment);
			Vector2 vector = {13565}.Measure({13563});
			textBlockFragment.Size = vector;
			this.{13637} = Math.Max(this.{13637}, vector.Y);
			Vector2 value = default(Vector2);
			textBlockFragment.Line = Math.Max(1, this.{13636});
			for (int i = 0; i < this.blocks.Size; i++)
			{
				if (this.blocks.Array[i].Line == textBlockFragment.Line && this.blocks.Array[i].Font != null && this.blocks.Array[i].Font != {13565})
				{
					value.Y = this.blocks.Array[i].Size.Y - vector.Y;
					value.X = -4f;
				}
			}
			textBlockFragment.Start = this.{13634} + value;
			if ({13566} != null)
			{
				if ({13567})
				{
					textBlockFragment.Start.X = {13566}.Value - vector.X;
					this.{13634}.X = this.{13634}.X + ({13566}.Value - this.{13634}.X - vector.X);
				}
				else
				{
					textBlockFragment.Start.X = {13566}.Value;
					this.{13634}.X = this.{13634}.X + ({13566}.Value - this.{13634}.X);
				}
			}
			textBlockFragment.Font = {13565};
			textBlockFragment.Text = {13563};
			textBlockFragment.Color = {13564};
			this.{13635}.X = MathHelper.Max(this.{13635}.X, this.{13634}.X + vector.X);
			this.{13635}.Y = MathHelper.Max(this.{13635}.Y, this.{13634}.Y + vector.Y);
			this.{13634}.X = this.{13634}.X + vector.X;
			this.blocks.Add(textBlockFragment);
			if (this.{13636} == 0)
			{
				this.{13636} = 1;
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00018A38 File Offset: 0x00016C38
		public void WriteLine(string {13568}, Color {13569}, CustomSpriteFont {13570})
		{
			if (this.replaceFontOrNull != null)
			{
				{13570} = this.replaceFontOrNull;
			}
			{13568} = {13570}.Validate({13568});
			TextBlockBuilder.TextBlockFragment textBlockFragment = default(TextBlockBuilder.TextBlockFragment);
			Vector2 vector = {13570}.Measure({13568} + "A");
			textBlockFragment.Size = vector;
			if (this.{13636} != 0)
			{
				this.{13634}.Y = this.{13634}.Y + (this.{13637} + this.{13633});
				this.{13637} = 0f;
			}
			this.{13637} = Math.Max(this.{13637}, vector.Y);
			int num = -1;
			int i = 0;
			while (i < this.blocks.Size)
			{
				int num2 = this.blocks.Size - i - 1;
				TextBlockBuilder.TextBlockFragment textBlockFragment2 = this.blocks.Array[num2];
				if (textBlockFragment2.Line == this.{13636})
				{
					num = num2;
				}
				if (textBlockFragment2.Line != this.{13636} && num > 0)
				{
					if (this.blocks.Array[num].Font != null && this.blocks.Array[num].Font != {13570})
					{
						this.{13634}.Y = this.{13634}.Y + 3f;
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			textBlockFragment.Line = this.{13636} + 1;
			this.{13636}++;
			this.{13634}.X = 0f;
			this.{13635}.X = MathHelper.Max(this.{13635}.X, vector.X);
			this.{13635}.Y = MathHelper.Max(this.{13635}.Y, this.{13634}.Y + vector.Y);
			if ({13568}.Length == 0)
			{
				return;
			}
			textBlockFragment.Start = this.{13634};
			textBlockFragment.Font = {13570};
			textBlockFragment.Text = {13568};
			textBlockFragment.Color = {13569};
			this.blocks.Add(textBlockFragment);
			this.{13634}.X = vector.X;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00018C38 File Offset: 0x00016E38
		public void WriteLines(string {13571}, Color {13572}, CustomSpriteFont {13573}, float {13574}, float? {13575} = 0f)
		{
			float num = this.{13633};
			this.{13633} = ({13575} ?? this.{13633});
			this.WriteLines(TextBlockBuilder.CreateBlock({13574}, {13571}, {13572}, {13573}, {13575} ?? this.{13633}));
			this.{13633} = num;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00018CA0 File Offset: 0x00016EA0
		public void WriteLines(TextBlockBuilder {13576})
		{
			for (int i = 0; i < {13576}.blocks.Size; i++)
			{
				TextBlockBuilder.TextBlockFragment textBlockFragment = {13576}.blocks.Array[i];
				if (textBlockFragment.Font != null)
				{
					this.WriteLine(textBlockFragment.Text, textBlockFragment.Color, (this.replaceFontOrNull != null) ? this.replaceFontOrNull : textBlockFragment.Font);
				}
				else
				{
					this.WriteImage(textBlockFragment.Tex, textBlockFragment.TexPath, textBlockFragment.Size.X / (float)textBlockFragment.TexPath.Width, null);
				}
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00018D40 File Offset: 0x00016F40
		public void WriteImage(Texture2D {13577}, Rectangle {13578}, float {13579} = 1f, Color? {13580} = null)
		{
			TextBlockBuilder.TextBlockFragment textBlockFragment = default(TextBlockBuilder.TextBlockFragment);
			textBlockFragment.Size = {13578}.WidthHeight() * {13579};
			textBlockFragment.Line = Math.Max(1, this.{13636});
			textBlockFragment.TexPath = {13578};
			textBlockFragment.Tex = {13577};
			this.{13635}.X = MathHelper.Max(this.{13635}.X, this.{13634}.X + textBlockFragment.Size.X);
			textBlockFragment.Start = this.{13634} - new Vector2(0f, (this.{13637} == 0f) ? 0f : ((textBlockFragment.Size.Y - this.{13637}) / 2f));
			textBlockFragment.Font = null;
			textBlockFragment.Text = null;
			textBlockFragment.Color = ({13580} ?? Color.White);
			this.blocks.Add(textBlockFragment);
			this.{13634}.X = this.{13634}.X + textBlockFragment.Size.X;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00018E60 File Offset: 0x00017060
		public void WriteImageTextSize(Texture2D {13581}, Rectangle {13582}, Color? {13583} = null)
		{
			TextBlockBuilder.TextBlockFragment textBlockFragment = default(TextBlockBuilder.TextBlockFragment);
			textBlockFragment.Size = new Vector2(this.defaultFont.MaxCharSize.Y, this.defaultFont.MaxCharSize.Y) + new Vector2(2f);
			textBlockFragment.Line = Math.Max(1, this.{13636});
			textBlockFragment.TexPath = {13582};
			textBlockFragment.Tex = {13581};
			this.{13635}.X = MathHelper.Max(this.{13635}.X, this.{13634}.X + textBlockFragment.Size.X);
			textBlockFragment.Start = this.{13634} - new Vector2(0f, (this.{13637} == 0f) ? 0f : ((textBlockFragment.Size.Y - this.{13637}) / 2f + 1f));
			textBlockFragment.Font = null;
			textBlockFragment.Text = null;
			textBlockFragment.Color = ({13583} ?? Color.White);
			this.blocks.Add(textBlockFragment);
			this.{13634}.X = this.{13634}.X + (textBlockFragment.Size.X - 2f);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00018FB0 File Offset: 0x000171B0
		public void Clear()
		{
			this.blocks.Clear();
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00018FC0 File Offset: 0x000171C0
		public TextBlockControl Create(Vector2 {13584}, float {13585}, float {13586})
		{
			Vector2 vector = {13584} + new Vector2({13585}, {13586});
			Vector2 vector2 = this.{13635} + new Vector2(10f, 0f);
			return new TextBlockControl(new Marker(ref vector, ref vector2), this.blocks.ToArray(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00019014 File Offset: 0x00017214
		public TextBlockControl Create()
		{
			Vector2 zero = Vector2.Zero;
			Vector2 vector = this.{13635} + new Vector2(10f, 0f);
			return new TextBlockControl(new Marker(ref zero, ref vector), this.blocks.ToArray(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00019060 File Offset: 0x00017260
		public TextBlockControl Create(Vector2 {13587})
		{
			Vector2 vector = this.{13635} + new Vector2(10f, 0f);
			return new TextBlockControl(new Marker(ref {13587}, ref vector), this.blocks.ToArray(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000190A4 File Offset: 0x000172A4
		public TextBlockControl CreateCentroid()
		{
			TextBlockBuilder.TextBlockFragment[] array = this.blocks.ToArray();
			if (array.Length != 0)
			{
				int line = array[0].Line;
				float num = 0f;
				foreach (TextBlockBuilder.TextBlockFragment textBlockFragment in array)
				{
					if (line != textBlockFragment.Line)
					{
						if (num > 0f)
						{
							for (int j = 0; j < array.Length; j++)
							{
								if (array[j].Line == line)
								{
									TextBlockBuilder.TextBlockFragment[] array2 = array;
									int num2 = j;
									array2[num2].Start.X = array2[num2].Start.X - num / 2f;
								}
							}
						}
						num = 0f;
						line = textBlockFragment.Line;
					}
					num = Math.Max(textBlockFragment.Start.X + textBlockFragment.Size.X, num);
				}
				if (num > 0f)
				{
					for (int k = 0; k < array.Length; k++)
					{
						if (array[k].Line == line)
						{
							TextBlockBuilder.TextBlockFragment[] array3 = array;
							int num3 = k;
							array3[num3].Start.X = array3[num3].Start.X - num / 2f;
						}
					}
				}
			}
			for (int l = 0; l < array.Length; l++)
			{
				TextBlockBuilder.TextBlockFragment[] array4 = array;
				int num4 = l;
				array4[num4].Start.X = array4[num4].Start.X + this.{13635}.X / 2f;
			}
			Vector2 zero = Vector2.Zero;
			Vector2 vector = this.{13635} + new Vector2(10f, 0f);
			return new TextBlockControl(new Marker(ref zero, ref vector), array, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00019227 File Offset: 0x00017427
		public TextBlockControl CreateCentroid(float {13588}, float {13589})
		{
			return this.CreateCentroid(new Vector2({13588}, {13589}));
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00019238 File Offset: 0x00017438
		public TextBlockControl CreateCentroid(Vector2 {13590})
		{
			TextBlockBuilder.TextBlockFragment[] array = this.blocks.ToArray();
			{13590}.X += 6f;
			if (array.Length != 0)
			{
				int line = array[0].Line;
				float num = 0f;
				foreach (TextBlockBuilder.TextBlockFragment textBlockFragment in array)
				{
					if (line != textBlockFragment.Line)
					{
						if (num > 0f)
						{
							for (int j = 0; j < array.Length; j++)
							{
								if (array[j].Line == line)
								{
									TextBlockBuilder.TextBlockFragment[] array2 = array;
									int num2 = j;
									array2[num2].Start.X = array2[num2].Start.X - num / 2f;
								}
							}
						}
						num = 0f;
						line = textBlockFragment.Line;
					}
					num = Math.Max(textBlockFragment.Start.X + textBlockFragment.Size.X, num);
				}
				if (num > 0f)
				{
					for (int k = 0; k < array.Length; k++)
					{
						if (array[k].Line == line)
						{
							TextBlockBuilder.TextBlockFragment[] array3 = array;
							int num3 = k;
							array3[num3].Start.X = array3[num3].Start.X - num / 2f;
						}
					}
				}
			}
			Vector2 vector = this.{13635} + new Vector2(10f, 0f);
			return new TextBlockControl(new Marker(ref {13590}, ref vector), array, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001938C File Offset: 0x0001758C
		public static TextBlockControl CreateEmpty(Vector2 {13591})
		{
			Vector2 zero = Vector2.Zero;
			return new TextBlockControl(new Marker(ref {13591}, ref zero), new TextBlockBuilder.TextBlockFragment[0], PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000193B8 File Offset: 0x000175B8
		public static Color SpecialColorHighlight(string {13592}, Color {13593}, Color {13594})
		{
			if (!{13592}.Contains('#'))
			{
				if (!{13592}.All((char {13638}) => "01234567890+-.,xх%".Contains({13638})) || ({13592}.Length <= 1 && ({13592}.Length <= 0 || {13592}[0] == '-')))
				{
					return {13593};
				}
			}
			return {13594};
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00019418 File Offset: 0x00017618
		public static TextBlockBuilder CreateBlockSpecial(float {13595}, string {13596}, Color {13597}, Color {13598}, CustomSpriteFont {13599}, CustomSpriteFont {13600}, int {13601} = 0)
		{
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_14, 0f);
			if (!{13596}.Contains('#'))
			{
				Func<char, bool> predicate;
				if ((predicate = TextBlockBuilder.<>O.<0>__IsDigit) == null)
				{
					predicate = (TextBlockBuilder.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
				}
				if (!{13596}.Any(predicate))
				{
					textBlockBuilder.WriteLines(TextBlockBuilder.CreateBlock({13595}, {13596}, {13597}, {13599}, (float){13601}));
					return textBlockBuilder;
				}
			}
			return TextBlockBuilder.CreateBlockSpecial({13595}, {13596}, {13599}, {13600}, (string {13640}) => TextBlockBuilder.SpecialColorHighlight({13640}, {13597}, {13598}), {13601});
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000194AC File Offset: 0x000176AC
		public static TextBlockBuilder CreateBlockSpecial(float {13602}, string {13603}, CustomSpriteFont {13604}, CustomSpriteFont {13605}, Func<string, Color> {13606}, int {13607} = 0)
		{
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_14, (float){13607});
			string[] array = {13603}.SplitSmart(new char[]
			{
				' '
			});
			bool[] array2 = new bool[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Contains(Environment.NewLine))
				{
					array[i] = array[i].Replace(Environment.NewLine, "");
					array2[i] = true;
				}
			}
			Color[] array3 = array.Select({13606}).ToArray<Color>();
			array = (from {13639} in array
			select {13639}.Replace("#", "") + ({13639}.EndsWithCjk() ? "" : " ")).ToArray<string>();
			textBlockBuilder.WriteLine("", Color.White, {13604});
			for (int j = 0; j < array.Length; j++)
			{
				var <>f__AnonymousType = new
				{
					{11258} = array[j],
					{11259} = array3[j]
				};
				Vector2 vector = {13604}.Measure(<>f__AnonymousType.word);
				if (textBlockBuilder.CurrentLocalPoint.X + vector.X >= {13602} || array2[j])
				{
					textBlockBuilder.WriteLine("", <>f__AnonymousType.color, {13604});
				}
				textBlockBuilder.Write(<>f__AnonymousType.word, <>f__AnonymousType.color, {13604}, null, false);
			}
			return textBlockBuilder;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000195F0 File Offset: 0x000177F0
		public static TextBlockBuilder CreateBlockSpecial(float {13608}, float {13609}, string {13610}, Color {13611}, Color {13612}, CustomSpriteFont[] {13613}, CustomSpriteFont {13614} = null, int {13615} = 0)
		{
			int num = Array.FindIndex<CustomSpriteFont>({13613}, (CustomSpriteFont {13641}) => {13641} == {13614});
			if (num == -1)
			{
				num = {13613}.Length - 1;
			}
			float num2 = {13608} - 20f;
			Func<string, Color> <>9__3;
			for (int i = num; i >= 0; i--)
			{
				CustomSpriteFont customSpriteFont = {13613}[i];
				TextBlockBuilder textBlockBuilder;
				if (TextBlockBuilder.<CreateBlockSpecial>g__HasHighlights|36_2({13610}))
				{
					float {13602} = num2;
					CustomSpriteFont {13604} = customSpriteFont;
					CustomSpriteFont {13605} = customSpriteFont;
					Func<string, Color> {13606};
					if (({13606} = <>9__3) == null)
					{
						{13606} = (<>9__3 = ((string {13642}) => TextBlockBuilder.SpecialColorHighlight({13642}, {13611}, {13612})));
					}
					textBlockBuilder = TextBlockBuilder.CreateBlockSpecial({13602}, {13610}, {13604}, {13605}, {13606}, {13615});
				}
				else
				{
					textBlockBuilder = TextBlockBuilder.CreateBlock(num2, {13610}, {13611}, customSpriteFont, (float){13615});
				}
				if (textBlockBuilder.{13635}.X <= {13608} && textBlockBuilder.{13635}.Y <= {13609})
				{
					return textBlockBuilder;
				}
			}
			CustomSpriteFont customSpriteFont2 = {13613}[0];
			if (TextBlockBuilder.<CreateBlockSpecial>g__HasHighlights|36_2({13610}))
			{
				return TextBlockBuilder.CreateBlockSpecial(num2, {13610}, customSpriteFont2, customSpriteFont2, (string {13643}) => TextBlockBuilder.SpecialColorHighlight({13643}, {13611}, {13612}), {13615});
			}
			return TextBlockBuilder.CreateBlock(num2, {13610}, {13611}, customSpriteFont2, (float){13615});
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00019700 File Offset: 0x00017900
		public static TextBlockBuilder CreateBlock(float {13616}, string {13617}, Color {13618}, CustomSpriteFont {13619}, float {13620} = 1f)
		{
			{13617} = {13617}.Replace(Environment.NewLine, " " + Environment.NewLine);
			string[] array = {13617}.SplitSmart(new char[]
			{
				' '
			});
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder({13619}, {13620});
			int num = array.Length;
			float num2 = 0f;
			StringBuilder stringBuilder = new StringBuilder({13617}.Length);
			Vector2 vector = {13619}.Measure(" ");
			for (int i = 0; i < num; i++)
			{
				string text = array[i];
				Vector2 vector2 = {13619}.Measure(text);
				if (vector2.X > {13616})
				{
					stringBuilder.Append(text);
					textBlockBuilder.WriteLine(stringBuilder.ToString(), {13618});
					num2 = 0f;
					stringBuilder.Clear();
					if (num2 != 0f)
					{
						i--;
					}
				}
				else if (text.Contains(Environment.NewLine))
				{
					text = text.Replace(Environment.NewLine, string.Empty);
					array[i] = text;
					num2 = 0f;
					i--;
					textBlockBuilder.WriteLine(stringBuilder.ToString(), {13618});
					stringBuilder.Clear();
				}
				else if (vector2.X + num2 > {13616})
				{
					num2 = 0f;
					i--;
					textBlockBuilder.WriteLine(stringBuilder.ToString(), {13618});
					stringBuilder.Clear();
				}
				else
				{
					stringBuilder.Append(text);
					num2 += vector2.X;
					if (!text.EndsWithCjk())
					{
						stringBuilder.Append(' ');
						num2 += vector.X;
					}
				}
			}
			textBlockBuilder.WriteLine(stringBuilder.ToString(), {13618});
			return textBlockBuilder;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00019890 File Offset: 0x00017A90
		public static TextBlockBuilder CreateLimitedBlock(CustomSpriteFont {13621}, int {13622}, int {13623}, IEnumerable<TextBlockItemInitialize> {13624})
		{
			List<TextBlockItemInitialize> list = new List<TextBlockItemInitialize>();
			foreach (TextBlockItemInitialize textBlockItemInitialize in {13624})
			{
				foreach (string {13547} in textBlockItemInitialize.Text.SplitSmart(new char[]
				{
					' '
				}))
				{
					list.Add(new TextBlockItemInitialize({13547}, textBlockItemInitialize.Color, textBlockItemInitialize.SingleLine));
				}
			}
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder({13621}, (float){13622});
			int count = list.Count;
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder({13623});
			Color color = list[0].Color;
			bool flag = false;
			for (int j = 0; j < count; j++)
			{
				if (list[j].Color != color)
				{
					textBlockBuilder.Write(stringBuilder.ToString(), color);
					stringBuilder.Clear();
					color = list[j].Color;
					flag = true;
				}
				string text = list[j].Text;
				if (text.Length > {13623})
				{
					int num2 = {13623} - num;
					string text2 = text.Substring(0, num2);
					stringBuilder.Append(text2);
					if (text.Length != text2.Length)
					{
						list[j].Text = text.Substring(num2, text.Length - num2);
					}
					if (flag)
					{
						textBlockBuilder.Write(stringBuilder.ToString(), list[j].Color);
					}
					else
					{
						textBlockBuilder.WriteLine(stringBuilder.ToString(), list[j].Color);
					}
					num = 0;
					stringBuilder.Clear();
					j--;
					flag = false;
				}
				else if (list[j].SingleLine)
				{
					num = 0;
					j--;
					textBlockBuilder.WriteLine(stringBuilder.ToString(), list[j].Color);
					stringBuilder.Clear();
				}
				else if (text.Length + num > {13623})
				{
					num = 0;
					j--;
					textBlockBuilder.WriteLine(stringBuilder.ToString(), list[j].Color);
					stringBuilder.Clear();
					flag = false;
				}
				else
				{
					stringBuilder.Append(text);
					stringBuilder.Append(' ');
					num += text.Length + 1;
				}
			}
			if (flag)
			{
				textBlockBuilder.Write(stringBuilder.ToString(), list[list.Count - 1].Color);
			}
			else
			{
				textBlockBuilder.WriteLine(stringBuilder.ToString(), list[list.Count - 1].Color);
			}
			return textBlockBuilder;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00019B44 File Offset: 0x00017D44
		public static TextBlockBuilder CreateLimitedBlock(CustomSpriteFont {13625}, int {13626}, int {13627}, params TextBlockItemInitialize[] {13628})
		{
			return TextBlockBuilder.CreateLimitedBlock({13625}, {13626}, {13627}, {13628});
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00019B50 File Offset: 0x00017D50
		public static TextBlockBuilder operator +(TextBlockBuilder {13629}, TextBlockBuilder {13630})
		{
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder({13629}.defaultFont, {13629}.{13633});
			TextBlockBuilder[] array = new TextBlockBuilder[]
			{
				{13629},
				{13630}
			};
			for (int i = 0; i < array.Length; i++)
			{
				foreach (TextBlockBuilder.TextBlockFragment textBlockFragment in ((IEnumerable<TextBlockBuilder.TextBlockFragment>)array[i].blocks))
				{
					if (textBlockFragment.Font != null)
					{
						textBlockBuilder.WriteLine(textBlockFragment.Text, textBlockFragment.Color, textBlockFragment.Font);
					}
					else
					{
						textBlockBuilder.WriteImage(textBlockFragment.Tex, textBlockFragment.TexPath, 1f, null);
					}
				}
			}
			return textBlockBuilder;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00019C14 File Offset: 0x00017E14
		public void ReplaceColor(Color {13631})
		{
			for (int i = 0; i < this.blocks.Size; i++)
			{
				if (this.blocks.Array[i].Tex == null)
				{
					this.blocks.Array[i].Color = {13631};
				}
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00019C66 File Offset: 0x00017E66
		[CompilerGenerated]
		internal static bool <CreateBlockSpecial>g__HasHighlights|36_2(string {13632})
		{
			if (!{13632}.Contains('#'))
			{
				Func<char, bool> predicate;
				if ((predicate = TextBlockBuilder.<>O.<0>__IsDigit) == null)
				{
					predicate = (TextBlockBuilder.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
				}
				return {13632}.Any(predicate);
			}
			return true;
		}

		// Token: 0x0400039E RID: 926
		public CustomSpriteFont defaultFont;

		// Token: 0x0400039F RID: 927
		public CustomSpriteFont replaceFontOrNull;

		// Token: 0x040003A0 RID: 928
		private float {13633};

		// Token: 0x040003A1 RID: 929
		public Tlist<TextBlockBuilder.TextBlockFragment> blocks;

		// Token: 0x040003A2 RID: 930
		private Vector2 {13634};

		// Token: 0x040003A3 RID: 931
		private Vector2 {13635};

		// Token: 0x040003A4 RID: 932
		private int {13636};

		// Token: 0x040003A5 RID: 933
		private float {13637};

		// Token: 0x020000BA RID: 186
		public struct TextBlockFragment
		{
			// Token: 0x040003A6 RID: 934
			public int Line;

			// Token: 0x040003A7 RID: 935
			public Vector2 Start;

			// Token: 0x040003A8 RID: 936
			public Vector2 Size;

			// Token: 0x040003A9 RID: 937
			public CustomSpriteFont Font;

			// Token: 0x040003AA RID: 938
			public Color Color;

			// Token: 0x040003AB RID: 939
			public string Text;

			// Token: 0x040003AC RID: 940
			public Texture2D Tex;

			// Token: 0x040003AD RID: 941
			public Rectangle TexPath;
		}

		// Token: 0x020000BB RID: 187
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040003AE RID: 942
			public static Func<char, bool> <0>__IsDigit;
		}
	}
}
