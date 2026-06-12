using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common;

namespace WorldOfSeaBattles.Components.Apis
{
	// Token: 0x020005BF RID: 1471
	public class XsollaService : IDisposable
	{
		// Token: 0x060021C4 RID: 8644 RVA: 0x0012E104 File Offset: 0x0012C304
		private XsollaService()
		{
			this.{26611}.DefaultRequestHeaders.UserAgent.ParseAdd("payment gateway");
			this.{26611}.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			this.{26611}.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", CommonGameConfig.CurrentSettings.XsollaToken);
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x0012E180 File Offset: 0x0012C380
		public Task<string> CreateOrderAsync(PaymentRequest {26604}, string {26605}, bool {26606} = false)
		{
			XsollaService.<CreateOrderAsync>d__3 <CreateOrderAsync>d__;
			<CreateOrderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<CreateOrderAsync>d__.<>4__this = this;
			<CreateOrderAsync>d__.request = {26604};
			<CreateOrderAsync>d__.country = {26605};
			<CreateOrderAsync>d__.emailError = {26606};
			<CreateOrderAsync>d__.<>1__state = -1;
			<CreateOrderAsync>d__.<>t__builder.Start<XsollaService.<CreateOrderAsync>d__3>(ref <CreateOrderAsync>d__);
			return <CreateOrderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0012E1DC File Offset: 0x0012C3DC
		public static Task<string> CreatePaymentLink(PaymentRequest {26607}, string {26608})
		{
			XsollaService.<CreatePaymentLink>d__4 <CreatePaymentLink>d__;
			<CreatePaymentLink>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<CreatePaymentLink>d__.request = {26607};
			<CreatePaymentLink>d__.country = {26608};
			<CreatePaymentLink>d__.<>1__state = -1;
			<CreatePaymentLink>d__.<>t__builder.Start<XsollaService.<CreatePaymentLink>d__4>(ref <CreatePaymentLink>d__);
			return <CreatePaymentLink>d__.<>t__builder.Task;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0012E228 File Offset: 0x0012C428
		public static void CreateOrderAndOpenInBrowser(PaymentRequest {26609}, string {26610})
		{
			XsollaService.<CreateOrderAndOpenInBrowser>d__5 <CreateOrderAndOpenInBrowser>d__;
			<CreateOrderAndOpenInBrowser>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CreateOrderAndOpenInBrowser>d__.request = {26609};
			<CreateOrderAndOpenInBrowser>d__.country = {26610};
			<CreateOrderAndOpenInBrowser>d__.<>1__state = -1;
			<CreateOrderAndOpenInBrowser>d__.<>t__builder.Start<XsollaService.<CreateOrderAndOpenInBrowser>d__5>(ref <CreateOrderAndOpenInBrowser>d__);
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x0012E267 File Offset: 0x0012C467
		public void Dispose()
		{
			HttpClient httpClient = this.{26611};
			if (httpClient == null)
			{
				return;
			}
			httpClient.Dispose();
		}

		// Token: 0x040020DB RID: 8411
		private readonly HttpClient {26611} = new HttpClient();

		// Token: 0x040020DC RID: 8412
		private readonly bool {26612};
	}
}
