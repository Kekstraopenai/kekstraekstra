using System;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace TheraEngine.Helpers
{
	// Token: 0x020000E9 RID: 233
	internal static class KeyboardTextCustomConverter
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x00020314 File Offset: 0x0001E514
		public static char GetLocalizedKeyAnalogue(Keys {14383}, bool {14384}, bool {14385}, KeyboardTextCustomConverter.LayoutLocalization? {14386} = null)
		{
			KeyboardTextCustomConverter.LayoutLocalization layoutLocalization = {14386}.GetValueOrDefault();
			if ({14386} == null)
			{
				layoutLocalization = KeyboardTextCustomConverter.DeduceLocalization();
				{14386} = new KeyboardTextCustomConverter.LayoutLocalization?(layoutLocalization);
			}
			if (KeyboardTextCustomConverter.IsDigitOrUniversalSymbol({14383}))
			{
				return KeyboardTextCustomConverter.GetDigitOrUniversalSymbol({14383});
			}
			KeyboardTextCustomConverter.LayoutLocalization? layoutLocalization2 = {14386};
			layoutLocalization = KeyboardTextCustomConverter.LayoutLocalization.Russian;
			if (layoutLocalization2.GetValueOrDefault() == layoutLocalization & layoutLocalization2 != null)
			{
				return KeyboardTextCustomConverter.GetRussianKeyAnalogue({14383}, {14384}, {14385});
			}
			if ({14386}.GetValueOrDefault() == KeyboardTextCustomConverter.LayoutLocalization.German)
			{
				return KeyboardTextCustomConverter.GetGermanKeyAnalogue({14383}, {14384}, {14385});
			}
			if ({14386}.GetValueOrDefault() == KeyboardTextCustomConverter.LayoutLocalization.French)
			{
				return KeyboardTextCustomConverter.GetFrenchKeyAnalogue({14383}, {14384}, {14385});
			}
			return KeyboardTextCustomConverter.GetEnglishKeyAnalogue({14383}, {14384}, {14385});
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0002039E File Offset: 0x0001E59E
		private static bool IsDigitOrUniversalSymbol(Keys {14387})
		{
			return ({14387} >= Keys.NumPad0 && {14387} <= Keys.NumPad9) || {14387} == Keys.Multiply || {14387} == Keys.Add || {14387} == Keys.Subtract || {14387} == Keys.Divide || {14387} == Keys.Space;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000203C8 File Offset: 0x0001E5C8
		private static char GetDigitOrUniversalSymbol(Keys {14388})
		{
			char result;
			if ({14388} != Keys.Space)
			{
				switch ({14388})
				{
				case Keys.NumPad0:
					return '0';
				case Keys.NumPad1:
					return '1';
				case Keys.NumPad2:
					return '2';
				case Keys.NumPad3:
					return '3';
				case Keys.NumPad4:
					return '4';
				case Keys.NumPad5:
					return '5';
				case Keys.NumPad6:
					return '6';
				case Keys.NumPad7:
					return '7';
				case Keys.NumPad8:
					return '8';
				case Keys.NumPad9:
					return '9';
				case Keys.Multiply:
					return '*';
				case Keys.Add:
					return '+';
				case Keys.Subtract:
					return '-';
				case Keys.Divide:
					return '/';
				}
				result = '\0';
			}
			else
			{
				result = ' ';
			}
			return result;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00020478 File Offset: 0x0001E678
		private static char GetEnglishKeyAnalogue(Keys {14389}, bool {14390}, bool {14391})
		{
			if ({14389} >= Keys.A && {14389} <= Keys.Z)
			{
				char c = {14389}.ToString()[0];
				if (!{14390})
				{
					c = char.ToLower(c);
				}
				return c;
			}
			char result;
			switch ({14389})
			{
			case Keys.D0:
				result = ({14391} ? ')' : '0');
				break;
			case Keys.D1:
				result = ({14391} ? '!' : '1');
				break;
			case Keys.D2:
				result = ({14391} ? '@' : '2');
				break;
			case Keys.D3:
				result = ({14391} ? '#' : '3');
				break;
			case Keys.D4:
				result = ({14391} ? '$' : '4');
				break;
			case Keys.D5:
				result = ({14391} ? '%' : '5');
				break;
			case Keys.D6:
				result = ({14391} ? '^' : '6');
				break;
			case Keys.D7:
				result = ({14391} ? '&' : '7');
				break;
			case Keys.D8:
				result = ({14391} ? '*' : '8');
				break;
			case Keys.D9:
				result = ({14391} ? '(' : '9');
				break;
			default:
				switch ({14389})
				{
				case Keys.OemSemicolon:
					result = ({14391} ? ':' : ';');
					break;
				case Keys.OemPlus:
					result = ({14391} ? '+' : '=');
					break;
				case Keys.OemComma:
					result = ({14391} ? '<' : ',');
					break;
				case Keys.OemMinus:
					result = ({14391} ? '_' : '-');
					break;
				case Keys.OemPeriod:
					result = ({14391} ? '>' : '.');
					break;
				case Keys.OemQuestion:
					result = ({14391} ? '?' : '/');
					break;
				case Keys.OemTilde:
					result = ({14391} ? '~' : '`');
					break;
				default:
					switch ({14389})
					{
					case Keys.OemOpenBrackets:
						return {14391} ? '{' : '[';
					case Keys.OemPipe:
						return {14391} ? '|' : '\\';
					case Keys.OemCloseBrackets:
						return {14391} ? '}' : ']';
					case Keys.OemQuotes:
						return {14391} ? '"' : '\'';
					case Keys.OemBackslash:
						return {14391} ? '|' : '\\';
					}
					result = '\0';
					break;
				}
				break;
			}
			return result;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00020668 File Offset: 0x0001E868
		private static char GetRussianKeyAnalogue(Keys {14392}, bool {14393}, bool {14394})
		{
			switch ({14392})
			{
			case Keys.D0:
				return {14394} ? ')' : '0';
			case Keys.D1:
				return {14394} ? '!' : '1';
			case Keys.D2:
				return {14394} ? '"' : '2';
			case Keys.D3:
				return {14394} ? '№' : '3';
			case Keys.D4:
				return {14394} ? ';' : '4';
			case Keys.D5:
				return {14394} ? '%' : '5';
			case Keys.D6:
				return {14394} ? ':' : '6';
			case Keys.D7:
				return {14394} ? '?' : '7';
			case Keys.D8:
				return {14394} ? '*' : '8';
			case Keys.D9:
				return {14394} ? '(' : '9';
			case (Keys)58:
			case (Keys)59:
			case (Keys)60:
			case (Keys)61:
			case (Keys)62:
			case (Keys)63:
			case (Keys)64:
				break;
			case Keys.A:
				return {14393} ? 'Ф' : 'ф';
			case Keys.B:
				return {14393} ? 'И' : 'и';
			case Keys.C:
				return {14393} ? 'С' : 'с';
			case Keys.D:
				return {14393} ? 'В' : 'в';
			case Keys.E:
				return {14393} ? 'У' : 'у';
			case Keys.F:
				return {14393} ? 'А' : 'а';
			case Keys.G:
				return {14393} ? 'П' : 'п';
			case Keys.H:
				return {14393} ? 'Р' : 'р';
			case Keys.I:
				return {14393} ? 'Ш' : 'ш';
			case Keys.J:
				return {14393} ? 'О' : 'о';
			case Keys.K:
				return {14393} ? 'Л' : 'л';
			case Keys.L:
				return {14393} ? 'Д' : 'д';
			case Keys.M:
				return {14393} ? 'Ь' : 'ь';
			case Keys.N:
				return {14393} ? 'Т' : 'т';
			case Keys.O:
				return {14393} ? 'Щ' : 'щ';
			case Keys.P:
				return {14393} ? 'З' : 'з';
			case Keys.Q:
				return {14393} ? 'Й' : 'й';
			case Keys.R:
				return {14393} ? 'К' : 'к';
			case Keys.S:
				return {14393} ? 'Ы' : 'ы';
			case Keys.T:
				return {14393} ? 'Е' : 'е';
			case Keys.U:
				return {14393} ? 'Г' : 'г';
			case Keys.V:
				return {14393} ? 'М' : 'м';
			case Keys.W:
				return {14393} ? 'Ц' : 'ц';
			case Keys.X:
				return {14393} ? 'Ч' : 'ч';
			case Keys.Y:
				return {14393} ? 'Н' : 'н';
			case Keys.Z:
				return {14393} ? 'Я' : 'я';
			default:
				switch ({14392})
				{
				case Keys.OemSemicolon:
					return {14393} ? 'Ж' : 'ж';
				case Keys.OemPlus:
					return {14394} ? '+' : '=';
				case Keys.OemComma:
					return {14393} ? 'Б' : 'б';
				case Keys.OemMinus:
					return {14394} ? '_' : '-';
				case Keys.OemPeriod:
					return {14393} ? 'Ю' : 'ю';
				case Keys.OemQuestion:
					return {14394} ? ',' : '.';
				case Keys.OemTilde:
					return {14393} ? 'Е' : 'е';
				default:
					switch ({14392})
					{
					case Keys.OemOpenBrackets:
						return {14393} ? 'Х' : 'х';
					case Keys.OemPipe:
						return {14394} ? '|' : '\\';
					case Keys.OemCloseBrackets:
						return {14393} ? 'Ъ' : 'ъ';
					case Keys.OemQuotes:
						return {14393} ? 'Э' : 'э';
					case Keys.OemBackslash:
						return {14394} ? '|' : '\\';
					}
					break;
				}
				break;
			}
			return '\0';
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00020B08 File Offset: 0x0001ED08
		private static char GetGermanKeyAnalogue(Keys {14395}, bool {14396}, bool {14397})
		{
			if ({14395} >= Keys.A && {14395} <= Keys.Z)
			{
				char c = {14395}.ToString()[0];
				if ({14395} == Keys.Y)
				{
					c = 'Z';
				}
				else if ({14395} == Keys.Z)
				{
					c = 'Y';
				}
				if (!{14396})
				{
					c = char.ToLower(c);
				}
				return c;
			}
			char result;
			switch ({14395})
			{
			case Keys.D0:
				result = ({14397} ? '=' : '0');
				break;
			case Keys.D1:
				result = ({14397} ? '!' : '1');
				break;
			case Keys.D2:
				result = ({14397} ? '"' : '2');
				break;
			case Keys.D3:
				result = ({14397} ? '§' : '3');
				break;
			case Keys.D4:
				result = ({14397} ? '$' : '4');
				break;
			case Keys.D5:
				result = ({14397} ? '%' : '5');
				break;
			case Keys.D6:
				result = ({14397} ? '&' : '6');
				break;
			case Keys.D7:
				result = ({14397} ? '/' : '7');
				break;
			case Keys.D8:
				result = ({14397} ? '(' : '8');
				break;
			case Keys.D9:
				result = ({14397} ? ')' : '9');
				break;
			default:
				switch ({14395})
				{
				case Keys.OemSemicolon:
					result = ({14397} ? 'Ö' : 'ö');
					break;
				case Keys.OemPlus:
					result = ({14397} ? '`' : '´');
					break;
				case Keys.OemComma:
					result = ({14397} ? ';' : ',');
					break;
				case Keys.OemMinus:
					result = ({14397} ? '?' : 'ß');
					break;
				case Keys.OemPeriod:
					result = ({14397} ? ':' : '.');
					break;
				case Keys.OemQuestion:
					result = ({14397} ? '_' : '-');
					break;
				case Keys.OemTilde:
					result = ({14397} ? '°' : '^');
					break;
				default:
					switch ({14395})
					{
					case Keys.OemOpenBrackets:
						return {14397} ? 'Ü' : 'ü';
					case Keys.OemPipe:
						return {14397} ? '\'' : '#';
					case Keys.OemCloseBrackets:
						return {14397} ? '*' : '+';
					case Keys.OemQuotes:
						return {14397} ? 'Ä' : 'ä';
					case Keys.OemBackslash:
						return {14397} ? '\'' : '#';
					}
					result = '\0';
					break;
				}
				break;
			}
			return result;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00020D2C File Offset: 0x0001EF2C
		private static char GetFrenchKeyAnalogue(Keys {14398}, bool {14399}, bool {14400})
		{
			if ({14398} >= Keys.A && {14398} <= Keys.Z)
			{
				char c = {14398}.ToString()[0];
				if ({14398} == Keys.A)
				{
					c = 'Q';
				}
				else if ({14398} == Keys.Q)
				{
					c = 'A';
				}
				else if ({14398} == Keys.W)
				{
					c = 'Z';
				}
				else if ({14398} == Keys.Z)
				{
					c = 'W';
				}
				if ({14398} == Keys.M)
				{
					c = ({14399} ? '?' : ',');
				}
				else if (!{14399})
				{
					c = char.ToLower(c);
				}
				return c;
			}
			char result;
			switch ({14398})
			{
			case Keys.D0:
				result = ({14400} ? '0' : 'à');
				break;
			case Keys.D1:
				result = ({14400} ? '1' : '&');
				break;
			case Keys.D2:
				result = ({14400} ? '2' : 'é');
				break;
			case Keys.D3:
				result = ({14400} ? '3' : '"');
				break;
			case Keys.D4:
				result = ({14400} ? '4' : '\'');
				break;
			case Keys.D5:
				result = ({14400} ? '5' : '(');
				break;
			case Keys.D6:
				result = ({14400} ? '6' : '-');
				break;
			case Keys.D7:
				result = ({14400} ? '7' : 'è');
				break;
			case Keys.D8:
				result = ({14400} ? '8' : '_');
				break;
			case Keys.D9:
				result = ({14400} ? '9' : 'ç');
				break;
			default:
				switch ({14398})
				{
				case Keys.OemSemicolon:
					result = ({14400} ? 'M' : 'm');
					break;
				case Keys.OemPlus:
					result = ({14400} ? '+' : '=');
					break;
				case Keys.OemComma:
					result = ({14400} ? '.' : ';');
					break;
				case Keys.OemMinus:
					result = ({14400} ? '°' : ')');
					break;
				case Keys.OemPeriod:
					result = ({14400} ? '/' : ':');
					break;
				case Keys.OemQuestion:
					result = ({14400} ? '§' : '!');
					break;
				case Keys.OemTilde:
					result = ({14400} ? '~' : '²');
					break;
				default:
					switch ({14398})
					{
					case Keys.OemOpenBrackets:
						return {14400} ? '¨' : '^';
					case Keys.OemPipe:
						return {14400} ? 'µ' : '*';
					case Keys.OemCloseBrackets:
						return {14400} ? '£' : '$';
					case Keys.OemQuotes:
						return {14400} ? '%' : 'ù';
					case Keys.OemBackslash:
						return {14400} ? 'µ' : '*';
					}
					result = '\0';
					break;
				}
				break;
			}
			return result;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00020F7A File Offset: 0x0001F17A
		internal static Keys TransformToNumPad(Keys {14401})
		{
			switch ({14401})
			{
			case Keys.PageUp:
				return Keys.NumPad9;
			case Keys.PageDown:
				return Keys.NumPad3;
			case Keys.End:
				return Keys.NumPad1;
			case Keys.Home:
				return Keys.NumPad7;
			default:
				return {14401};
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00020FA4 File Offset: 0x0001F1A4
		private static void GetFlags(out bool {14402}, out bool {14403}, out KeyboardTextCustomConverter.LayoutLocalization {14404}, out bool {14405}, Keys[] {14406})
		{
			{14402} = ({14406}.Contains(Keys.LeftShift) || {14406}.Contains(Keys.RightShift));
			{14405} = {14406}.Contains(Keys.NumLock);
			{14403} = Engine.CapsLockEnabled;
			{14404} = KeyboardTextCustomConverter.DeduceLocalization();
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00020FE4 File Offset: 0x0001F1E4
		private static KeyboardTextCustomConverter.LayoutLocalization DeduceLocalization()
		{
			string inputLayoutLanguage = Engine.PlatformTools.GetInputLayoutLanguage();
			KeyboardTextCustomConverter.LayoutLocalization result;
			if (!(inputLayoutLanguage == "en"))
			{
				if (!(inputLayoutLanguage == "ru"))
				{
					if (!(inputLayoutLanguage == "ua"))
					{
						if (!(inputLayoutLanguage == "de"))
						{
							if (!(inputLayoutLanguage == "fr"))
							{
								result = KeyboardTextCustomConverter.LayoutLocalization.English;
							}
							else
							{
								result = KeyboardTextCustomConverter.LayoutLocalization.French;
							}
						}
						else
						{
							result = KeyboardTextCustomConverter.LayoutLocalization.German;
						}
					}
					else
					{
						result = KeyboardTextCustomConverter.LayoutLocalization.Russian;
					}
				}
				else
				{
					result = KeyboardTextCustomConverter.LayoutLocalization.Russian;
				}
			}
			else
			{
				result = KeyboardTextCustomConverter.LayoutLocalization.English;
			}
			return result;
		}

		// Token: 0x020000EA RID: 234
		public enum LayoutLocalization
		{
			// Token: 0x040004B5 RID: 1205
			Russian,
			// Token: 0x040004B6 RID: 1206
			English,
			// Token: 0x040004B7 RID: 1207
			German,
			// Token: 0x040004B8 RID: 1208
			French
		}
	}
}
