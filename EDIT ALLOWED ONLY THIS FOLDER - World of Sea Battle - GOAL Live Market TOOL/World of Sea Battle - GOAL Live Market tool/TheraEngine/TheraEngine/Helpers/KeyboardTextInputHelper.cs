using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace TheraEngine.Helpers
{
	// Token: 0x020000EB RID: 235
	public static class KeyboardTextInputHelper
	{
		// Token: 0x06000651 RID: 1617 RVA: 0x00021056 File Offset: 0x0001F256
		public static bool IsKeyboardInputChar(char {14407})
		{
			return "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz".Contains({14407}) || "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЬьЫыЪъЭэЮюЯя".Contains({14407}) || "ßÄäÖöÜü".Contains({14407}) || "àéèçù".Contains({14407});
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0002108C File Offset: 0x0001F28C
		public static bool IsOfSupportedDigits(char {14408})
		{
			return "1234567890".Contains({14408});
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00021099 File Offset: 0x0001F299
		public static bool IsOfSlimCharMap(char {14409})
		{
			return " ()_-.,@".Contains({14409});
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000210A6 File Offset: 0x0001F2A6
		public static bool IsOfFullCharMap(char {14410})
		{
			return KeyboardTextInputHelper.FullCharMap.Contains({14410});
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000210B3 File Offset: 0x0001F2B3
		private static void GetFlags(out bool {14411}, out bool {14412}, out bool {14413}, Keys[] {14414})
		{
			{14411} = ({14414}.Contains(Keys.LeftShift) || {14414}.Contains(Keys.RightShift));
			{14413} = {14414}.Contains(Keys.NumLock);
			{14412} = Engine.CapsLockEnabled;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000210E8 File Offset: 0x0001F2E8
		public static string Sample(Keys[] {14415}, Keys[] {14416}, bool {14417} = false)
		{
			string text = string.Empty;
			if ({14415} != {14416})
			{
				bool flag;
				bool flag2;
				bool flag3;
				KeyboardTextInputHelper.GetFlags(out flag, out flag2, out flag3, {14415});
				if ({14417})
				{
					flag2 = false;
					flag = false;
				}
				bool {14384} = flag || flag2;
				int num = {14415}.Length;
				for (int i = 0; i < num; i++)
				{
					Keys keys = {14415}[i];
					if (!{14416}.Contains(keys))
					{
						if (flag3 && !KeyboardTextInputHelper.UseWinApi)
						{
							keys = KeyboardTextCustomConverter.TransformToNumPad(keys);
						}
						if (!{14417} || (keys >= Keys.D0 && keys <= Keys.D9) || (keys >= Keys.NumPad0 && keys <= Keys.NumPad9))
						{
							if (KeyboardTextInputHelper.UseWinApi)
							{
								string text2 = Engine.PlatformTools.MapKeyboardKey(keys);
								if (!string.IsNullOrEmpty(text2) && text2.Length == 1 && text2[0] != '\r' && text2[0] != '\n' && text2[0] != '\t')
								{
									ReadOnlySpan<char> str = text;
									char c = text2[0];
									text = str + new ReadOnlySpan<char>(ref c);
								}
							}
							else
							{
								ReadOnlySpan<char> str2 = text;
								char localizedKeyAnalogue = KeyboardTextCustomConverter.GetLocalizedKeyAnalogue(keys, {14384}, flag, null);
								text = str2 + new ReadOnlySpan<char>(ref localizedKeyAnalogue);
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00021212 File Offset: 0x0001F412
		public static string[] SplitSmart(this string {14418}, params char[] {14419})
		{
			if (string.IsNullOrEmpty({14418}))
			{
				return Array.Empty<string>();
			}
			if (KeyboardTextInputHelper.ContainsCjkCharacters({14418}))
			{
				return KeyboardTextInputHelper.SplitIntoTextElements({14418});
			}
			if ({14419} == null || {14419}.Length == 0)
			{
				{14419} = new char[]
				{
					' '
				};
			}
			return {14418}.Split({14419}, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0002124D File Offset: 0x0001F44D
		public static bool EndsWithCjk(this string {14420})
		{
			return !string.IsNullOrEmpty({14420}) && KeyboardTextInputHelper.IsCjkCharacter({14420}[{14420}.Length - 1]);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0002126C File Offset: 0x0001F46C
		private static bool ContainsCjkCharacters(string {14421})
		{
			for (int i = 0; i < {14421}.Length; i++)
			{
				if (KeyboardTextInputHelper.IsCjkCharacter({14421}[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x000212A0 File Offset: 0x0001F4A0
		private static bool IsCjkCharacter(char {14422})
		{
			return ({14422} >= '㐀' && {14422} <= '䶿') || ({14422} >= '一' && {14422} <= '鿿') || ({14422} >= '豈' && {14422} <= '﫿') || ({14422} >= '⺀' && {14422} <= '⻿') || ({14422} >= '\u3000' && {14422} <= '〿') || ({14422} >= '぀' && {14422} <= 'ゟ') || ({14422} >= '゠' && {14422} <= 'ヿ') || ({14422} >= 'ㇰ' && {14422} <= 'ㇿ') || ({14422} >= 'ｦ' && {14422} <= 'ﾟ');
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00021348 File Offset: 0x0001F548
		private static string[] SplitIntoTextElements(string {14423})
		{
			List<string> list = new List<string>();
			TextElementEnumerator textElementEnumerator = StringInfo.GetTextElementEnumerator({14423});
			while (textElementEnumerator.MoveNext())
			{
				object obj = textElementEnumerator.Current;
				string text = obj as string;
				if (text != null && text.Length > 0)
				{
					list.Add(text);
				}
			}
			return list.ToArray();
		}

		// Token: 0x040004B9 RID: 1209
		public static bool UseWinApi = true;

		// Token: 0x040004BA RID: 1210
		private const string CyrilicCharMap = "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЬьЫыЪъЭэЮюЯя";

		// Token: 0x040004BB RID: 1211
		private const string LatinCharMap = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";

		// Token: 0x040004BC RID: 1212
		private const string GermanCharMap = "ßÄäÖöÜü";

		// Token: 0x040004BD RID: 1213
		private const string FrenchCharMap = "àéèçù";

		// Token: 0x040004BE RID: 1214
		private const string NumbersCharMap = "1234567890";

		// Token: 0x040004BF RID: 1215
		private const string SlimCharMap = " ()_-.,@";

		// Token: 0x040004C0 RID: 1216
		private static readonly string FullCharMap = "!@#$%^&*()~-`№;:?_+/\\|<>{}[]'=\"";
	}
}
