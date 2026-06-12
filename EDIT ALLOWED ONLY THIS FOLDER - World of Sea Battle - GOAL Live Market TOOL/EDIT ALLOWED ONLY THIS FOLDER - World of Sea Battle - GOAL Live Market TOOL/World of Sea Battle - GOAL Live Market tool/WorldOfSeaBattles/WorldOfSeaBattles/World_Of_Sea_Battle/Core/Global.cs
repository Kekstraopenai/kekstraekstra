using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using Common;
using Common.Packets;
using WorldOfSeaBattles;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Components.Client;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics;

namespace World_Of_Sea_Battle.Core
{
	// Token: 0x020004ED RID: 1261
	public class Global
	{
		// Token: 0x06001BFD RID: 7165 RVA: 0x0010036A File Offset: 0x000FE56A
		public static void Shopstat(string {25194}, int {25195})
		{
			Global.Network.Send(new OnAnalyticsEventClient(OnAnalyticsEventClient.Type.BuyItem, {25194}, {25195}));
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00100384 File Offset: 0x000FE584
		[NullableContext(2)]
		public static ServerList.ServerInfo GetCurrentServer()
		{
			if (ServerList.Servers == null || ServerList.Servers.Length == 0)
			{
				return null;
			}
			ServerList.ServerInfo serverInfo;
			if ((serverInfo = ServerList.Servers.FirstOrDefault((ServerList.ServerInfo {25196}) => {25196}.Id == Global.Settings.SavedServerID)) == null)
			{
				serverInfo = (ServerList.GetFirstRecomended(LocaleInfo.Current.Id, Global.ConfidentISOCountryCode) ?? ServerList.Servers.First<ServerList.ServerInfo>());
			}
			ServerList.ServerInfo serverInfo2 = serverInfo;
			Global.Settings.SavedServerID = serverInfo2.Id;
			return serverInfo2;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00100403 File Offset: 0x000FE603
		internal static void ServerInitializationThread()
		{
			ThreadPool.QueueUserWorkItem(delegate(object {25197})
			{
				Global.<>c.<<ServerInitializationThread>b__11_0>d <<ServerInitializationThread>b__11_0>d;
				<<ServerInitializationThread>b__11_0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<ServerInitializationThread>b__11_0>d.<>1__state = -1;
				<<ServerInitializationThread>b__11_0>d.<>t__builder.Start<Global.<>c.<<ServerInitializationThread>b__11_0>d>(ref <<ServerInitializationThread>b__11_0>d);
			});
		}

		// Token: 0x04001B00 RID: 6912
		internal static Main Game;

		// Token: 0x04001B01 RID: 6913
		internal static NetworkManager Network;

		// Token: 0x04001B02 RID: 6914
		internal static LocalSettings Settings;

		// Token: 0x04001B03 RID: 6915
		internal static IPEndPoint ServerAddress;

		// Token: 0x04001B04 RID: 6916
		internal static RenderStatistics RenderStats;

		// Token: 0x04001B05 RID: 6917
		internal static ShipCurrentPlayer Player;

		// Token: 0x04001B06 RID: 6918
		internal static GameCamera Camera;

		// Token: 0x04001B07 RID: 6919
		internal static Renderer Render;

		// Token: 0x04001B08 RID: 6920
		internal static string ConfidentISOCountryCode = Steam.IsActive ? Steam.GetCountry() : RegionInfo.CurrentRegion.TwoLetterISORegionName;
	}
}
