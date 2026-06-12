using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace WorldOfSeaBattles.Components.Apis
{
	// Token: 0x020005B3 RID: 1459
	public class PaymentRequest
	{
		// Token: 0x060021A3 RID: 8611 RVA: 0x0012CC44 File Offset: 0x0012AE44
		public static Task<PaymentRequest> Create(int {26557}, Currency {26558}, Paystation {26559})
		{
			PaymentRequest.<Create>d__4 <Create>d__;
			<Create>d__.<>t__builder = AsyncTaskMethodBuilder<PaymentRequest>.Create();
			<Create>d__.monetsAmount = {26557};
			<Create>d__.initialCurrency = {26558};
			<Create>d__.paystation = {26559};
			<Create>d__.<>1__state = -1;
			<Create>d__.<>t__builder.Start<PaymentRequest.<Create>d__4>(ref <Create>d__);
			return <Create>d__.<>t__builder.Task;
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x0012CC98 File Offset: 0x0012AE98
		[return: TupleElementNames(new string[]
		{
			"paystation",
			"currency"
		})]
		public static ValueTuple<Paystation, Currency> GetCurrentPaystation()
		{
			Sequence sequence = new Sequence((int)(Session.Account.SID + (uint)Session.Account.Analytics.SummaryDonations));
			float chanseXsollaForRubles = CommonGameConfig.CurrentSettings.ChanseXsollaForRubles;
			Paystation paystation = PlatformTuning.ForceUsePaystation ?? ((Global.Settings.PreferredCurrency != Currency.Rub || sequence.Chanse(chanseXsollaForRubles)) ? Paystation.Xsolla : Paystation.Robokassa);
			if (paystation == Paystation.VKPlay && ((Session.Account.Rang >= 20 && Session.Account.Analytics.SummaryDonations > 0 && sequence.Chanse(CommonGameConfig.CurrentSettings.ChanseVkToXsTop30)) || sequence.Chanse(CommonGameConfig.CurrentSettings.ChanseVkToXsAll)))
			{
				paystation = Paystation.Robokassa;
			}
			if (paystation == Paystation.Steam & ((Session.Account.Rang >= 20 && Session.Account.Analytics.SummaryDonations > 0 && sequence.Chanse(CommonGameConfig.CurrentSettings.ChanseSteamToXsTop30)) || sequence.Chanse(CommonGameConfig.CurrentSettings.ChanseSteamToXsAll)))
			{
				paystation = Paystation.Xsolla;
			}
			Currency item = Global.Settings.PreferredCurrency;
			if (paystation == Paystation.VKPlay)
			{
				item = ((Session.VKPlay.Currency == "RUB") ? Currency.Rub : Currency.Usd);
			}
			return new ValueTuple<Paystation, Currency>(paystation, item);
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0012CDD0 File Offset: 0x0012AFD0
		public static Task<string> GetPaymentLink(int {26560})
		{
			PaymentRequest.<GetPaymentLink>d__6 <GetPaymentLink>d__;
			<GetPaymentLink>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<GetPaymentLink>d__.monetsAmount = {26560};
			<GetPaymentLink>d__.<>1__state = -1;
			<GetPaymentLink>d__.<>t__builder.Start<PaymentRequest.<GetPaymentLink>d__6>(ref <GetPaymentLink>d__);
			return <GetPaymentLink>d__.<>t__builder.Task;
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0012CE14 File Offset: 0x0012B014
		public static void ProcessPaymentUseSettings(int {26561})
		{
			PaymentRequest.<ProcessPaymentUseSettings>d__7 <ProcessPaymentUseSettings>d__;
			<ProcessPaymentUseSettings>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ProcessPaymentUseSettings>d__.monetsAmount = {26561};
			<ProcessPaymentUseSettings>d__.<>1__state = -1;
			<ProcessPaymentUseSettings>d__.<>t__builder.Start<PaymentRequest.<ProcessPaymentUseSettings>d__7>(ref <ProcessPaymentUseSettings>d__);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x0012CE4C File Offset: 0x0012B04C
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 3);
			defaultInterpolatedStringHandler.AppendLiteral("Amount: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.MonetsAmount);
			defaultInterpolatedStringHandler.AppendLiteral(", cur: ");
			defaultInterpolatedStringHandler.AppendFormatted<Currency>(this.InitialCurrency);
			defaultInterpolatedStringHandler.AppendLiteral(", ip: ");
			defaultInterpolatedStringHandler.AppendFormatted(this.ClientIP);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x040020A6 RID: 8358
		public Currency InitialCurrency;

		// Token: 0x040020A7 RID: 8359
		public int MonetsAmount;

		// Token: 0x040020A8 RID: 8360
		public int ServerOperationId;

		// Token: 0x040020A9 RID: 8361
		public string ClientIP;
	}
}
