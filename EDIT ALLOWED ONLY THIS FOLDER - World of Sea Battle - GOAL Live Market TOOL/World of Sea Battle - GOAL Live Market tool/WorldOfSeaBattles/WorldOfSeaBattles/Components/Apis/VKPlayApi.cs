using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Common;
using SDL2;
using TheraEngine;
using World_Of_Sea_Battle.Components;

namespace WorldOfSeaBattles.Components.Apis
{
	// Token: 0x020005BA RID: 1466
	public static class VKPlayApi
	{
		// Token: 0x060021BA RID: 8634 RVA: 0x0012DA58 File Offset: 0x0012BC58
		public static void CreateOrderAndOpenInOverlay(PaymentRequest {26589})
		{
			VKPlayApi.<CreateOrderAndOpenInOverlay>d__0 <CreateOrderAndOpenInOverlay>d__;
			<CreateOrderAndOpenInOverlay>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CreateOrderAndOpenInOverlay>d__.request = {26589};
			<CreateOrderAndOpenInOverlay>d__.<>1__state = -1;
			<CreateOrderAndOpenInOverlay>d__.<>t__builder.Start<VKPlayApi.<CreateOrderAndOpenInOverlay>d__0>(ref <CreateOrderAndOpenInOverlay>d__);
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x0012DA90 File Offset: 0x0012BC90
		private static Task<string> CreateOrderAsync(PaymentRequest {26590})
		{
			VKPlayApi.<CreateOrderAsync>d__1 <CreateOrderAsync>d__;
			<CreateOrderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<CreateOrderAsync>d__.payRequest = {26590};
			<CreateOrderAsync>d__.<>1__state = -1;
			<CreateOrderAsync>d__.<>t__builder.Start<VKPlayApi.<CreateOrderAsync>d__1>(ref <CreateOrderAsync>d__);
			return <CreateOrderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x0012DAD4 File Offset: 0x0012BCD4
		public static void OpenOverlay(string {26591})
		{
			try
			{
				Process.Start(new ProcessStartInfo("vkplay://demandbrowserform/" + CommonGameConfig.CurrentSettings.VKPlayGCID + "?url=" + UrlEncoder.Default.Encode({26591}) + "&width=70%25&height=80%25&caption=Payment&noextraparams=1")
				{
					UseShellExecute = true
				});
			}
			catch (Exception {25356})
			{
				if (Debugger.IsAttached)
				{
					Engine.SetClipboardText({26591});
					Engine.ShowMessageBox(Local.Helpers_0, "", SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION);
				}
				Helpers.SendError({25356}, "OpenVKOverlay", false, false);
			}
		}
	}
}
