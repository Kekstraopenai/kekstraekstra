using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using ManualPacketSerialization;
using ManualPacketSerialization.Externs;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x0200050C RID: 1292
	internal class LanguageSettings : IMPSerializable
	{
		// Token: 0x06001CE6 RID: 7398 RVA: 0x00108F28 File Offset: 0x00107128
		public void Boxing(WriterExtern {25395})
		{
			{25395}.WriteByte((this.CurrentGameLocale != null) ? ((byte)this.CurrentGameLocale.Value) : byte.MaxValue);
			{25395}.WriteByte((this.LauncherLocale != null) ? ((byte)this.LauncherLocale.Value) : byte.MaxValue);
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x00108F84 File Offset: 0x00107184
		public void Unboxing(WriterExtern {25396})
		{
			byte b = {25396}.ReadByte();
			this.CurrentGameLocale = ((b == byte.MaxValue) ? null : new Locale?(LanguageSettings.Validate((Locale)b)));
			byte b2 = {25396}.ReadByte();
			this.LauncherLocale = ((b2 == byte.MaxValue) ? null : new Locale?(LanguageSettings.Validate((Locale)b2)));
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x00108FE7 File Offset: 0x001071E7
		private static Locale Validate(Locale {25397})
		{
			if (Enum.GetValues<Locale>().Contains({25397}))
			{
				return {25397};
			}
			return Locale.En;
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x00108FFC File Offset: 0x001071FC
		public static Locale GetExpectedDefaultLocale()
		{
			string confidentISOCountryCode = Global.ConfidentISOCountryCode;
			Locale? locale = PlatformTuning.TryMapCountryToLocale(LanguageSettings.<GetExpectedDefaultLocale>g__NormalizeCountry|5_0(RegionInfo.CurrentRegion.TwoLetterISORegionName));
			if (locale == null)
			{
				return PlatformTuning.TryMapCountryToLocale(LanguageSettings.<GetExpectedDefaultLocale>g__NormalizeCountry|5_0(confidentISOCountryCode)).GetValueOrDefault(Locale.En);
			}
			return locale.GetValueOrDefault();
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0010904C File Offset: 0x0010724C
		public static void SetLauncherLocale(Locale {25398})
		{
			bool flag = Global.Settings.Language.LauncherLocale == null;
			bool flag2 = Global.Settings.Language.LauncherLocale != null && Global.Settings.Language.LauncherLocale.Value != {25398};
			if (flag || flag2)
			{
				Global.Settings.Language.CurrentGameLocale = new Locale?({25398});
			}
			Global.Settings.Language.LauncherLocale = new Locale?({25398});
			LocaleInfo.SetLocale(Global.Settings.Language.CurrentGameLocale.Value, true);
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x001090EC File Offset: 0x001072EC
		public static void SetUserLocale(Locale {25399}, bool {25400} = true)
		{
			Global.Settings.Language.CurrentGameLocale = new Locale?({25399});
			if ({25400})
			{
				LocaleInfo.SetLocale({25399}, true);
			}
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0010910D File Offset: 0x0010730D
		[NullableContext(2)]
		[CompilerGenerated]
		internal static string <GetExpectedDefaultLocale>g__NormalizeCountry|5_0(string {25401})
		{
			if (string.IsNullOrWhiteSpace({25401}))
			{
				return null;
			}
			{25401} = {25401}.Trim().ToUpperInvariant();
			if ({25401}.Length != 2)
			{
				return null;
			}
			return {25401};
		}

		// Token: 0x04001C8A RID: 7306
		public Locale? CurrentGameLocale;

		// Token: 0x04001C8B RID: 7307
		public Locale? LauncherLocale;
	}
}
