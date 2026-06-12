using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Content;
using TheraEngine.Core;

namespace TheraEngine.Components
{
	// Token: 0x02000100 RID: 256
	public static class Fonts
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00024A1C File Offset: 0x00022C1C
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x00024A23 File Offset: 0x00022C23
		public static bool CurrentLanguageIsCjk { get; private set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00024A2B File Offset: 0x00022C2B
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x00024A32 File Offset: 0x00022C32
		public static CustomSpriteFont Philosopher_9 { get; private set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00024A3A File Offset: 0x00022C3A
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x00024A41 File Offset: 0x00022C41
		public static CustomSpriteFont Philosopher_12 { get; private set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00024A49 File Offset: 0x00022C49
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x00024A50 File Offset: 0x00022C50
		public static CustomSpriteFont Philosopher_12Bold { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00024A58 File Offset: 0x00022C58
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x00024A5F File Offset: 0x00022C5F
		public static CustomSpriteFont Philosopher_14 { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00024A67 File Offset: 0x00022C67
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x00024A6E File Offset: 0x00022C6E
		public static CustomSpriteFont Philosopher_14Bold { get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x00024A76 File Offset: 0x00022C76
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x00024A7D File Offset: 0x00022C7D
		public static CustomSpriteFont Philosopher_16 { get; private set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00024A85 File Offset: 0x00022C85
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x00024A8C File Offset: 0x00022C8C
		public static CustomSpriteFont Philosopher_16Bold { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00024A94 File Offset: 0x00022C94
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x00024A9B File Offset: 0x00022C9B
		public static CustomSpriteFont Philosopher_18 { get; private set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00024AA3 File Offset: 0x00022CA3
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x00024AAA File Offset: 0x00022CAA
		public static CustomSpriteFont Philosopher_24 { get; private set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x00024AB2 File Offset: 0x00022CB2
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x00024AB9 File Offset: 0x00022CB9
		public static CustomSpriteFont Philosopher_24Bold { get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00024AC1 File Offset: 0x00022CC1
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x00024AC8 File Offset: 0x00022CC8
		public static CustomSpriteFont Philosopher_36 { get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00024AD0 File Offset: 0x00022CD0
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x00024AD7 File Offset: 0x00022CD7
		public static CustomSpriteFont Arial_8 { get; private set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00024ADF File Offset: 0x00022CDF
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x00024AE6 File Offset: 0x00022CE6
		public static CustomSpriteFont Arial_9 { get; private set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x00024AEE File Offset: 0x00022CEE
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x00024AF5 File Offset: 0x00022CF5
		public static CustomSpriteFont Arial_10 { get; private set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00024AFD File Offset: 0x00022CFD
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x00024B04 File Offset: 0x00022D04
		public static CustomSpriteFont Arial_10Bold { get; private set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00024B0C File Offset: 0x00022D0C
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x00024B13 File Offset: 0x00022D13
		public static CustomSpriteFont Arial_12 { get; private set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00024B1B File Offset: 0x00022D1B
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x00024B22 File Offset: 0x00022D22
		public static CustomSpriteFont F_m12_Ghotic { get; private set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00024B2A File Offset: 0x00022D2A
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x00024B31 File Offset: 0x00022D31
		public static CustomSpriteFont F_m14_Ghotic { get; private set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00024B39 File Offset: 0x00022D39
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x00024B40 File Offset: 0x00022D40
		public static CustomSpriteFont F_m14_ThinBold { get; private set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00024B48 File Offset: 0x00022D48
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x00024B4F File Offset: 0x00022D4F
		public static CustomSpriteFont NotoSansJP_14 { get; private set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00024B57 File Offset: 0x00022D57
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x00024B5E File Offset: 0x00022D5E
		public static CustomSpriteFont NotoSansJP_14_Bold { get; private set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00024B66 File Offset: 0x00022D66
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x00024B6D File Offset: 0x00022D6D
		public static CustomSpriteFont[] PhilosopherSizes { get; private set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0000DD52 File Offset: 0x0000BF52
		private static Fonts.Locale DefaultLocale
		{
			get
			{
				return Fonts.Locale.En;
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00024B78 File Offset: 0x00022D78
		internal static void Load(ContentManager {14785}, string {14786}, string {14787})
		{
			Fonts.currentLocale = Fonts.GetCurrentLocale({14787});
			Fonts.Locale locale = Fonts.currentLocale;
			bool currentLanguageIsCjk = locale - Fonts.Locale.Zh <= 2;
			Fonts.CurrentLanguageIsCjk = currentLanguageIsCjk;
			Fonts.Philosopher_9 = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Philosopher_9"]);
			Fonts.Philosopher_12 = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Philosopher_12"]);
			Fonts.Philosopher_12Bold = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Philosopher_12Bold"]);
			Fonts.Philosopher_14 = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Philosopher_14"]);
			Fonts.Philosopher_14Bold = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Philosopher_14Bold"]);
			Fonts.Philosopher_16 = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Philosopher_16"]);
			Fonts.Philosopher_16Bold = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Philosopher_16Bold"]);
			Fonts.Philosopher_18 = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Philosopher_18"]);
			Fonts.Philosopher_24 = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Philosopher_24"]);
			Fonts.Philosopher_24Bold = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Philosopher_24Bold"]);
			Fonts.Philosopher_36 = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Philosopher_36"]);
			Fonts.Arial_8 = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Arial_8"]);
			Fonts.Arial_9 = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Arial_9"]);
			Fonts.Arial_10 = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Arial_10"]);
			Fonts.Arial_10Bold = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Arial_10Bold"]);
			Fonts.Arial_12 = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["Arial_12"]);
			Fonts.F_m12_Ghotic = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["F_m12_Ghotic"]);
			Fonts.F_m14_Ghotic = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["F_m14_Ghotic"]);
			Fonts.F_m14_ThinBold = Fonts.LoadFontForLocale({14785}, {14786}, Fonts.fontDefinitions["F_m14_ThinBold"]);
			if (Fonts.currentLocale == Fonts.Locale.Ja)
			{
				Fonts.NotoSansJP_14 = Fonts.Philosopher_14;
				Fonts.NotoSansJP_14_Bold = Fonts.Philosopher_14Bold;
			}
			else
			{
				Fonts.NotoSansJP_14 = new CustomSpriteFont({14785}.Load<SpriteFont>({14786} + "NotoSansJP_14"), {14785}.Load<SpriteFont>({14786} + "NotoSansJP_14"));
				Fonts.NotoSansJP_14_Bold = new CustomSpriteFont({14785}.Load<SpriteFont>({14786} + "NotoSansJP_14_Bold"), {14785}.Load<SpriteFont>({14786} + "NotoSansJP_14_Bold"));
			}
			Fonts.PhilosopherSizes = new CustomSpriteFont[]
			{
				Fonts.Philosopher_9,
				Fonts.Philosopher_12,
				Fonts.Philosopher_14,
				Fonts.Philosopher_16,
				Fonts.Philosopher_18,
				Fonts.Philosopher_24,
				Fonts.Philosopher_36
			};
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00024E64 File Offset: 0x00023064
		private static CustomSpriteFont LoadFontForLocale(ContentManager {14788}, string {14789}, Dictionary<Fonts.Locale, string> {14790})
		{
			string text;
			string str = {14790}.TryGetValue(Fonts.currentLocale, out text) ? text : {14790}[Fonts.DefaultLocale];
			string text2;
			string str2 = {14790}.TryGetValue(Fonts.Locale.Ja, out text2) ? text2 : {14790}[Fonts.Locale.Ja];
			return new CustomSpriteFont({14788}.Load<SpriteFont>({14789} + str), {14788}.Load<SpriteFont>({14789} + str2));
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00024EC4 File Offset: 0x000230C4
		private static Fonts.Locale GetCurrentLocale(string {14791})
		{
			Fonts.Locale result;
			if (!Enum.TryParse<Fonts.Locale>({14791}, out result))
			{
				return Fonts.DefaultLocale;
			}
			return result;
		}

		// Token: 0x04000516 RID: 1302
		private static Fonts.Locale currentLocale;

		// Token: 0x04000517 RID: 1303
		private static readonly Dictionary<string, Dictionary<Fonts.Locale, string>> fontDefinitions = new Dictionary<string, Dictionary<Fonts.Locale, string>>
		{
			{
				"Philosopher_9",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"Game_9"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_12"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_12"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_12"
					}
				}
			},
			{
				"Philosopher_12",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"Game_12"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_12"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_12"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_12"
					}
				}
			},
			{
				"Philosopher_12Bold",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"Game_12b"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_12_Bold"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_12_Bold"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_12_Bold"
					}
				}
			},
			{
				"Philosopher_14",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"Game_14p"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_14"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_14"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_14"
					}
				}
			},
			{
				"Philosopher_14Bold",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"Game_14b"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_14_Bold"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_14_Bold"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_14_Bold"
					}
				}
			},
			{
				"Philosopher_16",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"Game_16p"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_16"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_16"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_16"
					}
				}
			},
			{
				"Philosopher_16Bold",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"Game_16b"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_16_Bold"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_16_Bold"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_16_Bold"
					}
				}
			},
			{
				"Philosopher_18",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"Game_18p"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_18"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_18"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_18"
					}
				}
			},
			{
				"Philosopher_24",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"Game_24p"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_18"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_18"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_18"
					}
				}
			},
			{
				"Philosopher_24Bold",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"Game_24b"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_18_Bold"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_18_Bold"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_18_Bold"
					}
				}
			},
			{
				"Philosopher_36",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"Game_36p"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_18_Bold"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_18_Bold"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_18_Bold"
					}
				}
			},
			{
				"Arial_8",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"F_m8_Compact"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_8"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_8"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_8"
					}
				}
			},
			{
				"Arial_9",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"F_m9_Normal"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_9"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_9"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_9"
					}
				}
			},
			{
				"Arial_10",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"F_m10_Normal_s1"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_10"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_10"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_10"
					}
				}
			},
			{
				"Arial_10Bold",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"F_m10_Bold_s1"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_10_Bold"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_10_Bold"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_10_Bold"
					}
				}
			},
			{
				"Arial_12",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"F_m12_Normal_s1"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_12"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_12"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_12"
					}
				}
			},
			{
				"F_m12_Ghotic",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"F_m12_Ghotic"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_12"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_12"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_12"
					}
				}
			},
			{
				"F_m14_Ghotic",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"F_m14_Ghotic"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_14"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_14"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_14"
					}
				}
			},
			{
				"F_m14_ThinBold",
				new Dictionary<Fonts.Locale, string>
				{
					{
						Fonts.DefaultLocale,
						"F_m14_ThinBold"
					},
					{
						Fonts.Locale.Zh,
						"NotoSansSC_14_Bold"
					},
					{
						Fonts.Locale.Ja,
						"NotoSansJP_14_Bold"
					},
					{
						Fonts.Locale.Ko,
						"NotoSansKR_14_Bold"
					}
				}
			}
		};

		// Token: 0x02000101 RID: 257
		private enum Locale : byte
		{
			// Token: 0x04000530 RID: 1328
			Ru,
			// Token: 0x04000531 RID: 1329
			En,
			// Token: 0x04000532 RID: 1330
			De,
			// Token: 0x04000533 RID: 1331
			Fr,
			// Token: 0x04000534 RID: 1332
			Es,
			// Token: 0x04000535 RID: 1333
			Pl,
			// Token: 0x04000536 RID: 1334
			Zh,
			// Token: 0x04000537 RID: 1335
			Ja,
			// Token: 0x04000538 RID: 1336
			Ko
		}
	}
}
