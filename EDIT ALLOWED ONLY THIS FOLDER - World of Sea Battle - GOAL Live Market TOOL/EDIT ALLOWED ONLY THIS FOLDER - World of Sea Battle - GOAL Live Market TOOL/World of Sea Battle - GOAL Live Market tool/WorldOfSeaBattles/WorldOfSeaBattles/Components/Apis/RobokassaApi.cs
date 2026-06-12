using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;
using Common;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace WorldOfSeaBattles.Components.Apis
{
	// Token: 0x020005B8 RID: 1464
	public static class RobokassaApi
	{
		// Token: 0x060021AF RID: 8623 RVA: 0x0012D7A4 File Offset: 0x0012B9A4
		private static string GenerateSignature(Dictionary<string, string> {26579}, string {26580})
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append({26579}["MerchantLogin"]);
			stringBuilder.Append(":");
			stringBuilder.Append({26579}["OutSum"]);
			stringBuilder.Append(":");
			stringBuilder.Append({26579}["InvId"]);
			stringBuilder.Append(":");
			stringBuilder.Append({26579}["Receipt"]);
			stringBuilder.Append(":");
			stringBuilder.Append({26580});
			return BitConverter.ToString(MD5.HashData(Encoding.UTF8.GetBytes(stringBuilder.ToString()))).Replace("-", "").ToLower();
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0012D868 File Offset: 0x0012BA68
		public static string CreatePaymentLink(PaymentRequest {26581})
		{
			float {16157} = Currency.Rub.AmountInCurrency({26581}.MonetsAmount, Paystation.Robokassa);
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{
					"Description",
					"World of Sea Battle"
				},
				{
					"Culture",
					"ru"
				},
				{
					"MerchantLogin",
					CommonGameConfig.CurrentSettings.RobokassaLogin
				},
				{
					"InvId",
					{26581}.ServerOperationId.ToString()
				},
				{
					"OutSum",
					{16157}.ToString()
				},
				{
					"Email",
					CommonSupport.IsEmail(Session.Account.EMail) ? Session.Account.EMail : ""
				}
			};
			string str = JsonSerializer.Serialize(new
			{
				{16146} = "usn_income",
				{16147} = new <>f__AnonymousType1<string, int, float, string, string, string>[]
				{
					new
					{
						{16155} = "Индивидуальный заказ в игре World of Sea Battle",
						{16156} = 1,
						{16157} = {16157},
						{16158} = "full_payment",
						{16159} = "service",
						{16160} = "none"
					}
				}
			}, null);
			dictionary["Receipt"] = HttpUtility.UrlEncode(str);
			string value = RobokassaApi.GenerateSignature(dictionary, CommonGameConfig.CurrentSettings.RobokassaPassword);
			dictionary["SignatureValue"] = value;
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(string.Empty);
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				nameValueCollection[keyValuePair.Key] = keyValuePair.Value;
			}
			return new UriBuilder(CommonGameConfig.CurrentSettings.RobokassaApiUrl)
			{
				Query = nameValueCollection.ToString()
			}.ToString();
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x0012D9F4 File Offset: 0x0012BBF4
		public static void CreateOrderAndOpenInBrowser(PaymentRequest {26582})
		{
			string text = RobokassaApi.CreatePaymentLink({26582});
			if (text != null)
			{
				Helpers.ExecuteBrowser(text, false);
			}
		}
	}
}
