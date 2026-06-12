using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using Common;
using Microsoft.Win32;
using TheraEngine;
using WorldOfSeaBattles.Components.Api;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000506 RID: 1286
	public static class Helpers
	{
		// Token: 0x06001CC3 RID: 7363 RVA: 0x00107AB8 File Offset: 0x00105CB8
		public static void SendError(Exception {25356}, string {25357}, bool {25358}, bool {25359} = false)
		{
			string text = ({25356} is AssertFailException) ? {25356}.Message : {25356}.ExceptionToStr(true, {25358});
			text = (string.IsNullOrEmpty({25357}) ? text : ({25357} + ": " + text));
			if ({25359})
			{
				text = "CRITICAL: " + text;
			}
			WosbRestApi.TryPost("/report/crashs", new StringContent(text), (Session.Account != null) ? WosbRestApi.TargetServer.Current : WosbRestApi.TargetServer.AllServers);
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00107B24 File Offset: 0x00105D24
		private static void OpenURL2(string {25360})
		{
			string name = "htmlfile\\shell\\open\\command";
			string fileName = ((string)Registry.ClassesRoot.OpenSubKey(name, false).GetValue(null, null)).Split('"', StringSplitOptions.None)[1];
			new Process
			{
				StartInfo = 
				{
					FileName = fileName,
					Arguments = {25360}
				}
			}.Start();
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00107B80 File Offset: 0x00105D80
		public static void SendTestPostRequest(string {25361}, string {25362})
		{
			try
			{
				WebRequest webRequest = WebRequest.Create({25361});
				webRequest.Method = "POST";
				webRequest.ContentType = "application/x-www-form-urlencoded";
				byte[] bytes = Encoding.Default.GetBytes({25362});
				webRequest.ContentLength = (long)bytes.Length;
				Stream requestStream = webRequest.GetRequestStream();
				requestStream.Write(bytes, 0, bytes.Length);
				requestStream.Close();
				HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
				Stream responseStream = httpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream);
				streamReader.ReadToEnd();
				streamReader.Close();
				responseStream.Close();
				httpWebResponse.Close();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x00107C18 File Offset: 0x00105E18
		public static void ExecuteBrowser(string {25363}, bool {25364} = false)
		{
			try
			{
				Process.Start(new ProcessStartInfo({25363})
				{
					UseShellExecute = true
				});
			}
			catch (Exception {25356})
			{
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					Helpers.OpenURL2({25363});
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				{
					Process.Start("xdg-open", {25363});
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
				{
					Process.Start("open", {25363});
				}
				else
				{
					if (!{25364})
					{
						Engine.SetClipboardText({25363});
						new {17312}(Local.Helpers_0);
					}
					Helpers.SendError({25356}, "OpenBrowser", false, false);
				}
			}
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x00107CB8 File Offset: 0x00105EB8
		public static void OpenReferalWebPage()
		{
			Helpers.ExecuteBrowser(Local.referal_link_ref(CommonGlobal.GetReferalFromSid(Session.Account.SID)), false);
		}
	}
}
