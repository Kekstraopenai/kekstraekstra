using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using TheraEngine;
using TheraEngine.Collections;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Components.Client
{
	// Token: 0x02000550 RID: 1360
	internal class ConnectionModule
	{
		// Token: 0x06001EEB RID: 7915 RVA: 0x001158DF File Offset: 0x00113ADF
		public ConnectionModule(Networking {25861})
		{
			this.{25869} = {25861};
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x00115904 File Offset: 0x00113B04
		public void Stop(bool {25862} = true, bool {25863} = false, bool {25864} = false)
		{
			if (this.isStarted)
			{
				this.isStarted = false;
				if (this.{25869}.IsConnected)
				{
					string {10146} = string.Concat(new string[]
					{
						"notCFEsa: ",
						{25862}.ToString(),
						", have_account: ",
						(Session.Account != null).ToString(),
						", last_packets: , last packets: Send: ",
						EntryPoint.substructPackets(Networking.lastSendMessages),
						", Receive: ",
						EntryPoint.substructPackets(Networking.lastReceiveMessages)
					});
					PlayerAccount playerAccount = ({25862} && Global.Player.IsPortEntry) ? Session.Account : null;
					this.{25869}.Send(new OnDisconnectionFromClientMsg({10146}, Session.SafeExitFlags, {25863}, {25864}, playerAccount));
					if (playerAccount != null)
					{
						Global.Settings.Logbook.Flush();
					}
					Session.SafeExitFlags = false;
				}
				if (Global.Game.GetCurrentSceneName == GameSceneName.Entry)
				{
					Global.Game.SceneEntry.AuthError(this.{25871}, this.{25872});
				}
				this.{25870} = 0f;
				this.{25869}.Stop();
				this.gameProcessSettsLoaded = false;
			}
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x00115A2C File Offset: 0x00113C2C
		public void LoginAsync(IPEndPoint {25865}, AuthParams {25866})
		{
			if (this.isStarted)
			{
				throw new InvalidOperationException();
			}
			this.isStarted = true;
			ThreadPool.QueueUserWorkItem(delegate(object {25873})
			{
				this.{25870} = 6000f;
				SocketError socketStatus;
				string text;
				bool flag = this.{25869}.Start({25865}, out socketStatus, out text);
				{25866}.SocketStatus = socketStatus;
				if ({25866}.SocketStatus == SocketError.Success && flag)
				{
					Networking networking = this.{25869};
					string empty = string.Empty;
					string query = {25866}.Query;
					string {10174} = PasswordHashing.Hash({25866}.Password);
					bool regNew = {25866}.RegNew;
					uint regReferalSID = {25866}.RegReferalSID;
					string regName = {25866}.RegName;
					GameVersion gameVersion = Version.GameVersion;
					int magicNumber = CommonGlobal.MagicNumber;
					byte[] bytes = ConnectionModule.fingerPrint.Bytes;
					Tlist<string> tlist = new Tlist<string>();
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 2);
					defaultInterpolatedStringHandler.AppendFormatted("Lang:");
					defaultInterpolatedStringHandler.AppendFormatted<Locale>(LocaleInfo.Current.Id);
					string text2 = defaultInterpolatedStringHandler.ToStringAndClear();
					tlist.Add(text2);
					string text3 = "Region:" + RegionInfo.CurrentRegion.Name;
					tlist.Add(text3);
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(0, 2);
					defaultInterpolatedStringHandler2.AppendFormatted("Platform:");
					defaultInterpolatedStringHandler2.AppendFormatted<PlatformType>({25866}.Platform);
					string text4 = defaultInterpolatedStringHandler2.ToStringAndClear();
					tlist.Add(text4);
					networking.Send(new OnLoginMsg(empty, query, {10174}, regNew, regReferalSID, regName, gameVersion, magicNumber, bytes, tlist, LocaleInfo.Current.Id, Global.Settings.IsFirstLaunch, {25866}.External));
					return;
				}
				this.{25871} = null;
				this.{25872} = text;
				this.Stop(false, false, false);
			});
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x00115A7C File Offset: 0x00113C7C
		public void OnResponseLoginResult(OnLoginResultMsg {25867})
		{
			if ({25867}.Result == LoginQueryResult.Success && {25867}.YourAccount != null)
			{
				Session.InstallAccount({25867}.YourAccount);
				Session.ServerSessionHash = {25867}.SessionHash;
				Global.Game.SceneEntry.AuthSucces({25867}.YourAccount);
			}
			else
			{
				this.{25871} = new LoginQueryResult?({25867}.Result);
				this.{25872} = {25867}.Args;
				this.Stop(false, false, false);
			}
			if (this.{25870} != 0f)
			{
				this.{25870} += 5000f;
			}
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00115B0C File Offset: 0x00113D0C
		public void OnGameEntry()
		{
			this.{25870} = 0f;
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x00115B19 File Offset: 0x00113D19
		public void Update(ref FrameTime {25868})
		{
			if ({25868}.EvaluteTimerMs2(ref this.{25870}))
			{
				this.{25871} = null;
				this.{25872} = "Server update hang";
				this.Stop(false, false, false);
			}
		}

		// Token: 0x04001E8B RID: 7819
		public static FingerPrintF fingerPrint;

		// Token: 0x04001E8C RID: 7820
		private Networking {25869};

		// Token: 0x04001E8D RID: 7821
		internal bool isStarted;

		// Token: 0x04001E8E RID: 7822
		internal Stopwatch timeFromLastReceivedPacket = Stopwatch.StartNew();

		// Token: 0x04001E8F RID: 7823
		internal Stopwatch timeFromLastSentPacket = Stopwatch.StartNew();

		// Token: 0x04001E90 RID: 7824
		internal bool gameProcessSettsLoaded;

		// Token: 0x04001E91 RID: 7825
		private float {25870};

		// Token: 0x04001E92 RID: 7826
		private LoginQueryResult? {25871};

		// Token: 0x04001E93 RID: 7827
		[Nullable(2)]
		private string {25872};
	}
}
