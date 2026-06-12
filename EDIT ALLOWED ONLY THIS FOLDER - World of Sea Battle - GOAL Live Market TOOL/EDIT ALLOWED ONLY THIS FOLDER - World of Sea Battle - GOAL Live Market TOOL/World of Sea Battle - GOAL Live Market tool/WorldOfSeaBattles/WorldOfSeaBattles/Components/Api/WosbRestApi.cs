using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using World_Of_Sea_Battle.Core;

namespace WorldOfSeaBattles.Components.Api
{
	// Token: 0x020005C4 RID: 1476
	internal class WosbRestApi
	{
		// Token: 0x060021CF RID: 8655 RVA: 0x0012E898 File Offset: 0x0012CA98
		public static HttpClient CreateRequest(WosbRestApi.TargetServer {26629})
		{
			string text;
			if ({26629} != WosbRestApi.TargetServer.Current)
			{
				text = "http://replicator.worldofseabattle.com:8080";
			}
			else
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 2);
				defaultInterpolatedStringHandler.AppendLiteral("http://");
				defaultInterpolatedStringHandler.AppendFormatted(Global.GetCurrentServer().Ip);
				defaultInterpolatedStringHandler.AppendLiteral(":");
				defaultInterpolatedStringHandler.AppendFormatted<int>(23778);
				text = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			string uriString = text;
			return new HttpClient
			{
				BaseAddress = new Uri(uriString),
				Timeout = TimeSpan.FromSeconds(15.0)
			};
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x0012E91C File Offset: 0x0012CB1C
		public static Task<string> TryPost(string {26630}, HttpContent {26631}, WosbRestApi.TargetServer {26632})
		{
			WosbRestApi.<TryPost>d__2 <TryPost>d__;
			<TryPost>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<TryPost>d__.path = {26630};
			<TryPost>d__.content = {26631};
			<TryPost>d__.target = {26632};
			<TryPost>d__.<>1__state = -1;
			<TryPost>d__.<>t__builder.Start<WosbRestApi.<TryPost>d__2>(ref <TryPost>d__);
			return <TryPost>d__.<>t__builder.Task;
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x0012E970 File Offset: 0x0012CB70
		public static Task<string> TryGet(string {26633}, WosbRestApi.TargetServer {26634})
		{
			WosbRestApi.<TryGet>d__3 <TryGet>d__;
			<TryGet>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<TryGet>d__.path = {26633};
			<TryGet>d__.target = {26634};
			<TryGet>d__.<>1__state = -1;
			<TryGet>d__.<>t__builder.Start<WosbRestApi.<TryGet>d__3>(ref <TryGet>d__);
			return <TryGet>d__.<>t__builder.Task;
		}

		// Token: 0x020005C5 RID: 1477
		public enum TargetServer
		{
			// Token: 0x040020F4 RID: 8436
			Current,
			// Token: 0x040020F5 RID: 8437
			AllServers
		}
	}
}
