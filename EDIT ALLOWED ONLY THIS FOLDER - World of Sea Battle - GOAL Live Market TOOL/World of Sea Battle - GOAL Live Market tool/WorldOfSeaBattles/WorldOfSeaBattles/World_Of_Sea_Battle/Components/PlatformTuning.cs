using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Game;
using WorldOfSeaBattles;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000515 RID: 1301
	public static class PlatformTuning
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x00109E23 File Offset: 0x00108023
		public static bool InviteFriendsLinkVisible
		{
			get
			{
				return !Session.VKPlay.IsActive;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x00109E32 File Offset: 0x00108032
		public static bool ExternalLoginAPI
		{
			get
			{
				return Session.VKPlay.IsActive || Steam.IsActive || PlatformTuning.DebuggingAuth != null;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x00109E51 File Offset: 0x00108051
		public static bool EnablePaymentLinkCopy
		{
			get
			{
				return !Session.VKPlay.IsActive && !Steam.IsActive;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool DisableShop
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x00109E69 File Offset: 0x00108069
		public static bool DisableShopWindowNow
		{
			get
			{
				return Steam.IsActive && Session.Account.Rang < 4;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool DisablePremAnUniqueShips
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x00109E81 File Offset: 0x00108081
		public static bool EnableContextOffers
		{
			get
			{
				return !Steam.IsActive;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x000070D7 File Offset: 0x000052D7
		public static bool EnableLootboxes
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x00109E8B File Offset: 0x0010808B
		public static bool EnableLootboxesChanseTooltip
		{
			get
			{
				return Steam.IsActive || LocaleInfo.Current.Id > Locale.Ru;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x00109EA3 File Offset: 0x001080A3
		public static string HelpdeskLink
		{
			get
			{
				if (!Session.VKPlay.IsActive)
				{
					return Local.launcher_support_ref;
				}
				return Local.launcher_support_vk_ref;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x00109EBC File Offset: 0x001080BC
		public static Paystation? ForceUsePaystation
		{
			get
			{
				if (Session.VKPlay.IsActive)
				{
					return new Paystation?(Paystation.VKPlay);
				}
				if (!Steam.IsActive)
				{
					return null;
				}
				return new Paystation?(Paystation.Steam);
			}
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x00109EF3 File Offset: 0x001080F3
		public static PlatformType GetPlatform()
		{
			if (Session.VKPlay.IsActive)
			{
				return PlatformType.VKPlay;
			}
			if (File.Exists("AppxManifest.xml"))
			{
				return PlatformType.MSStore;
			}
			if (Steam.IsActive)
			{
				return PlatformType.Steam;
			}
			return PlatformType.Standalone;
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x00109F1C File Offset: 0x0010811C
		public static Task<ExternalAuth> GetAutoLoginCreds()
		{
			PlatformTuning.<GetAutoLoginCreds>d__24 <GetAutoLoginCreds>d__;
			<GetAutoLoginCreds>d__.<>t__builder = AsyncTaskMethodBuilder<ExternalAuth>.Create();
			<GetAutoLoginCreds>d__.<>1__state = -1;
			<GetAutoLoginCreds>d__.<>t__builder.Start<PlatformTuning.<GetAutoLoginCreds>d__24>(ref <GetAutoLoginCreds>d__);
			return <GetAutoLoginCreds>d__.<>t__builder.Task;
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x00109F58 File Offset: 0x00108158
		public static Locale? TryMapCountryToLocale(string {25466})
		{
			bool flag = {25466} == "RU" || {25466} == "UA" || {25466} == "KZ" || {25466} == "BY";
			if (flag)
			{
				return new Locale?(Locale.Ru);
			}
			flag = ({25466} == "DE" || {25466} == "AT" || {25466} == "CH");
			if (flag)
			{
				return new Locale?(Locale.De);
			}
			flag = ({25466} == "FR" || {25466} == "BE" || {25466} == "CH" || {25466} == "LU" || {25466} == "MC");
			if (flag)
			{
				return new Locale?(Locale.Fr);
			}
			if ({25466} != null)
			{
				int length = {25466}.Length;
				if (length == 2)
				{
					switch ({25466}[1])
					{
					case 'A':
						if (!({25466} == "PA"))
						{
							goto IL_28A;
						}
						break;
					case 'B':
					case 'D':
					case 'F':
					case 'G':
					case 'H':
					case 'J':
					case 'K':
					case 'M':
					case 'P':
					case 'Q':
					case 'U':
					case 'W':
						goto IL_28A;
					case 'C':
						if (!({25466} == "EC"))
						{
							goto IL_28A;
						}
						break;
					case 'E':
						if (!({25466} == "PE"))
						{
							goto IL_28A;
						}
						break;
					case 'I':
						if (!({25466} == "NI"))
						{
							goto IL_28A;
						}
						break;
					case 'L':
						if (!({25466} == "CL"))
						{
							goto IL_28A;
						}
						break;
					case 'N':
						if (!({25466} == "HN"))
						{
							goto IL_28A;
						}
						break;
					case 'O':
						if (!({25466} == "CO") && !({25466} == "BO") && !({25466} == "DO"))
						{
							goto IL_28A;
						}
						break;
					case 'R':
						if (!({25466} == "AR") && !({25466} == "CR") && !({25466} == "PR"))
						{
							goto IL_28A;
						}
						break;
					case 'S':
						if (!({25466} == "ES"))
						{
							goto IL_28A;
						}
						break;
					case 'T':
						if (!({25466} == "GT"))
						{
							goto IL_28A;
						}
						break;
					case 'V':
						if (!({25466} == "SV"))
						{
							goto IL_28A;
						}
						break;
					case 'X':
						if (!({25466} == "MX"))
						{
							goto IL_28A;
						}
						break;
					case 'Y':
						if (!({25466} == "UY") && !({25466} == "PY"))
						{
							goto IL_28A;
						}
						break;
					default:
						goto IL_28A;
					}
					flag = true;
					goto IL_28C;
				}
			}
			IL_28A:
			flag = false;
			IL_28C:
			if (flag)
			{
				return new Locale?(Locale.Es);
			}
			if ({25466} == "PL")
			{
				return new Locale?(Locale.Pl);
			}
			if ({25466} == "JP")
			{
				return new Locale?(Locale.Ja);
			}
			if ({25466} == "KR")
			{
				return new Locale?(Locale.Ko);
			}
			flag = ({25466} == "CN" || {25466} == "TW" || {25466} == "HK" || {25466} == "MO");
			if (flag)
			{
				return new Locale?(Locale.Zh);
			}
			return null;
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x0010A284 File Offset: 0x00108484
		public static PlatformTuning.PayRegion MatchDefaultRegion
		{
			get
			{
				string regionByIp = Global.ConfidentISOCountryCode;
				string regionByLang2;
				if (LocaleInfo.Current.Id == Locale.Ru)
				{
					regionByLang2 = "RU";
				}
				else
				{
					Locale id = LocaleInfo.Current.Id;
					bool flag = id - Locale.De <= 3;
					regionByLang2 = (flag ? "EU" : "");
				}
				string regionByLang = regionByLang2;
				PlatformTuning.PayRegion result;
				if ((result = PlatformTuning.AvailablePayRegions.FirstOrDefault((PlatformTuning.PayRegion {25497}) => {25497}.ISONames.Contains(regionByIp))) == null && (result = PlatformTuning.AvailablePayRegions.FirstOrDefault((PlatformTuning.PayRegion {25498}) => {25498}.ISONames.Contains(regionByLang))) == null)
				{
					result = PlatformTuning.AvailablePayRegions.First((PlatformTuning.PayRegion {25493}) => {25493}.ISONames.Length == 0);
				}
				return result;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x0010A344 File Offset: 0x00108544
		[Nullable(2)]
		public static string ConfidentISOPayRegion
		{
			[NullableContext(2)]
			get
			{
				PlatformTuning.PayRegion payRegion = PlatformTuning.AvailablePayRegions.FirstOrDefault((PlatformTuning.PayRegion {25494}) => {25494}.payRegion == Global.Settings.PreferredPayRegion) ?? PlatformTuning.MatchDefaultRegion;
				if (payRegion.ISONames.Contains(Global.ConfidentISOCountryCode))
				{
					return Global.ConfidentISOCountryCode;
				}
				if (payRegion.ISONames.Length != 0)
				{
					return payRegion.ISONames.First<string>();
				}
				return null;
			}
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0010A3B2 File Offset: 0x001085B2
		private static bool DisableUAOnForeingers(PlatformTuning.PayRegion {25467})
		{
			return {25467}.payInCurrency != Currency.Uah || LocaleInfo.Current.Id == Locale.Ru;
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x0010A3CC File Offset: 0x001085CC
		public static IEnumerable<PlatformTuning.PayRegion> AvailablePayRegions
		{
			get
			{
				if (Steam.IsActive)
				{
					return from {25495} in PlatformTuning.AllPayRegions
					where {25495}.payInCurrency != Currency.Rub && PlatformTuning.DisableUAOnForeingers({25495})
					select {25495};
				}
				if (!Session.VKPlay.IsActive)
				{
					IEnumerable<PlatformTuning.PayRegion> allPayRegions = PlatformTuning.AllPayRegions;
					Func<PlatformTuning.PayRegion, bool> predicate;
					if ((predicate = PlatformTuning.<>O.<0>__DisableUAOnForeingers) == null)
					{
						predicate = (PlatformTuning.<>O.<0>__DisableUAOnForeingers = new Func<PlatformTuning.PayRegion, bool>(PlatformTuning.DisableUAOnForeingers));
					}
					return allPayRegions.Where(predicate);
				}
				return PlatformTuning.AllPayRegions.Where(delegate(PlatformTuning.PayRegion {25496})
				{
					if (!(Session.VKPlay.Currency == "RUB"))
					{
						return {25496}.payInCurrency == Currency.Usd;
					}
					return {25496}.payInCurrency == Currency.Rub;
				});
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x0010A468 File Offset: 0x00108668
		public static bool ChatOnlyMyLanguageDefault
		{
			get
			{
				try
				{
					return !Global.GetCurrentServer().Id.StartsWith("ru", StringComparison.OrdinalIgnoreCase) && LocaleInfo.Current.Id > Locale.Ru;
				}
				catch
				{
				}
				return false;
			}
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0010A4B8 File Offset: 0x001086B8
		public static bool IsMyLanguage(Locale {25468})
		{
			return {25468} == Locale.Ru == (LocaleInfo.Current.Id == Locale.Ru);
		}

		// Token: 0x04001CB4 RID: 7348
		[Nullable(2)]
		public static ExternalAuth DebuggingAuth;

		// Token: 0x04001CB5 RID: 7349
		public static readonly PlatformTuning.PayRegion[] AllPayRegions = new PlatformTuning.PayRegion[]
		{
			new PlatformTuning.PayRegion("EU €", Currency.Eur, new string[]
			{
				"DE",
				"FR",
				"ES",
				"IT",
				"NL",
				"BE",
				"PT",
				"CY",
				"FI",
				"SK",
				"SI",
				"AT",
				"GR",
				"IE",
				"LT",
				"LU",
				"MT"
			}),
			new PlatformTuning.PayRegion("RU ₽", Currency.Rub, new string[]
			{
				"RU"
			}),
			new PlatformTuning.PayRegion("NA $", Currency.Usd, new string[]
			{
				"US",
				"CA",
				"AU"
			}),
			new PlatformTuning.PayRegion("UA ₴", Currency.Uah, new string[]
			{
				"UA"
			}),
			new PlatformTuning.PayRegion("WORLD", Currency.Usd, Array.Empty<string>())
		};

		// Token: 0x02000516 RID: 1302
		public class PayRegion : IEquatable<PlatformTuning.PayRegion>
		{
			// Token: 0x06001D2D RID: 7469 RVA: 0x0010A604 File Offset: 0x00108804
			public PayRegion(string {25473}, Currency {25474}, string[] {25475})
			{
				this.payRegion = {25473};
				this.payInCurrency = {25474};
				this.ISONames = {25475};
				base..ctor();
			}

			// Token: 0x170002B3 RID: 691
			// (get) Token: 0x06001D2E RID: 7470 RVA: 0x0010A621 File Offset: 0x00108821
			[Nullable(1)]
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[NullableContext(1)]
				[CompilerGenerated]
				get
				{
					return typeof(PlatformTuning.PayRegion);
				}
			}

			// Token: 0x170002B4 RID: 692
			// (get) Token: 0x06001D2F RID: 7471 RVA: 0x0010A62D File Offset: 0x0010882D
			// (set) Token: 0x06001D30 RID: 7472 RVA: 0x0010A635 File Offset: 0x00108835
			public string payRegion { get; set; }

			// Token: 0x170002B5 RID: 693
			// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0010A63E File Offset: 0x0010883E
			// (set) Token: 0x06001D32 RID: 7474 RVA: 0x0010A646 File Offset: 0x00108846
			public Currency payInCurrency { get; set; }

			// Token: 0x170002B6 RID: 694
			// (get) Token: 0x06001D33 RID: 7475 RVA: 0x0010A64F File Offset: 0x0010884F
			// (set) Token: 0x06001D34 RID: 7476 RVA: 0x0010A657 File Offset: 0x00108857
			public string[] ISONames { get; set; }

			// Token: 0x06001D35 RID: 7477 RVA: 0x0010A660 File Offset: 0x00108860
			[NullableContext(1)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("PayRegion");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x06001D36 RID: 7478 RVA: 0x0010A6AC File Offset: 0x001088AC
			[NullableContext(1)]
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder {25479})
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				{25479}.Append("payRegion = ");
				{25479}.Append(this.payRegion);
				{25479}.Append(", payInCurrency = ");
				{25479}.Append(this.payInCurrency.ToString());
				{25479}.Append(", ISONames = ");
				{25479}.Append(this.ISONames);
				return true;
			}

			// Token: 0x06001D37 RID: 7479 RVA: 0x0010A718 File Offset: 0x00108918
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=(PlatformTuning.PayRegion {25480}, PlatformTuning.PayRegion {25481})
			{
				return !({25480} == {25481});
			}

			// Token: 0x06001D38 RID: 7480 RVA: 0x0010A724 File Offset: 0x00108924
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==(PlatformTuning.PayRegion {25482}, PlatformTuning.PayRegion {25483})
			{
				return {25482} == {25483} || ({25482} != null && {25482}.Equals({25483}));
			}

			// Token: 0x06001D39 RID: 7481 RVA: 0x0010A738 File Offset: 0x00108938
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.{25490})) * -1521134295 + EqualityComparer<Currency>.Default.GetHashCode(this.{25491})) * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(this.{25492});
			}

			// Token: 0x06001D3A RID: 7482 RVA: 0x0010A79A File Offset: 0x0010899A
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object {25484})
			{
				return this.Equals({25484} as PlatformTuning.PayRegion);
			}

			// Token: 0x06001D3B RID: 7483 RVA: 0x0010A7A8 File Offset: 0x001089A8
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(PlatformTuning.PayRegion {25485})
			{
				return this == {25485} || ({25485} != null && this.EqualityContract == {25485}.EqualityContract && EqualityComparer<string>.Default.Equals(this.{25490}, {25485}.{25490}) && EqualityComparer<Currency>.Default.Equals(this.{25491}, {25485}.{25491}) && EqualityComparer<string[]>.Default.Equals(this.{25492}, {25485}.{25492}));
			}

			// Token: 0x06001D3D RID: 7485 RVA: 0x0010A821 File Offset: 0x00108A21
			[CompilerGenerated]
			protected PayRegion([Nullable(1)] PlatformTuning.PayRegion {25486})
			{
				this.payRegion = {25486}.{25490};
				this.payInCurrency = {25486}.{25491};
				this.ISONames = {25486}.{25492};
			}

			// Token: 0x06001D3E RID: 7486 RVA: 0x0010A84D File Offset: 0x00108A4D
			[CompilerGenerated]
			public void Deconstruct(out string {25487}, out Currency {25488}, out string[] {25489})
			{
				{25487} = this.payRegion;
				{25488} = this.payInCurrency;
				{25489} = this.ISONames;
			}

			// Token: 0x04001CB6 RID: 7350
			[CompilerGenerated]
			private readonly string {25490};

			// Token: 0x04001CB7 RID: 7351
			[CompilerGenerated]
			private readonly Currency {25491};

			// Token: 0x04001CB8 RID: 7352
			[CompilerGenerated]
			private readonly string[] {25492};
		}

		// Token: 0x02000517 RID: 1303
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001CB9 RID: 7353
			public static Func<PlatformTuning.PayRegion, bool> <0>__DisableUAOnForeingers;
		}
	}
}
