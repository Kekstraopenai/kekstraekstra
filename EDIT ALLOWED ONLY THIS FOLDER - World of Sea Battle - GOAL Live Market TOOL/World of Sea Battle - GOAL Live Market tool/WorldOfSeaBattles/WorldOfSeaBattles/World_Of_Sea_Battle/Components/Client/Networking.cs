using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Common.Packets;
using HPCISockets.HighPacketLevel;
using ManualPacketSerialization;
using ReskanaProgect;
using ReskanaProgect.Network;
using TheraEngine;
using TheraEngine.Collections;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Components.Client
{
	// Token: 0x02000552 RID: 1362
	internal class Networking
	{
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06001EF3 RID: 7923 RVA: 0x00115CF4 File Offset: 0x00113EF4
		// (remove) Token: 0x06001EF4 RID: 7924 RVA: 0x00115D2C File Offset: 0x00113F2C
		public event Action<OnDisconnectionFromServerMsg?> DisconnectionMessageHandler
		{
			[CompilerGenerated]
			add
			{
				Action<OnDisconnectionFromServerMsg?> action = this.{25898};
				Action<OnDisconnectionFromServerMsg?> action2;
				do
				{
					action2 = action;
					Action<OnDisconnectionFromServerMsg?> value2 = (Action<OnDisconnectionFromServerMsg?>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<OnDisconnectionFromServerMsg?>>(ref this.{25898}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<OnDisconnectionFromServerMsg?> action = this.{25898};
				Action<OnDisconnectionFromServerMsg?> action2;
				do
				{
					action2 = action;
					Action<OnDisconnectionFromServerMsg?> value2 = (Action<OnDisconnectionFromServerMsg?>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<OnDisconnectionFromServerMsg?>>(ref this.{25898}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06001EF5 RID: 7925 RVA: 0x00115D64 File Offset: 0x00113F64
		// (remove) Token: 0x06001EF6 RID: 7926 RVA: 0x00115D9C File Offset: 0x00113F9C
		public event Action OnReconnectionBegin
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{25899};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{25899}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{25899};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{25899}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06001EF7 RID: 7927 RVA: 0x00115DD4 File Offset: 0x00113FD4
		// (remove) Token: 0x06001EF8 RID: 7928 RVA: 0x00115E0C File Offset: 0x0011400C
		public event Action OnReconnectionSuccess
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{25900};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{25900}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{25900};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{25900}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x00115E41 File Offset: 0x00114041
		public bool IsRun
		{
			get
			{
				return this.{25905};
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x00115E49 File Offset: 0x00114049
		public bool IsConnected
		{
			get
			{
				return this.{25906};
			}
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x00115E54 File Offset: 0x00114054
		public Networking()
		{
			this.Netstat = new NetworkStatistics();
			this.NetstatPs = new NetworkStatistics();
			this.{25903} = new NetworkStatistics();
			this.Routers = new ServerL7(ReflectivePacketID.TransferReflectionSource);
			this.{25901} = new BufferedDataHolder(new byte[Config.MaxPacketSize], 0);
			this.{25904} = new UnboxingHelper();
			this.{25880}();
			Config.InternalErrorsLogger = delegate(string {25911})
			{
				Helpers.SendError(new Exception("Reskana internal error: " + {25911}), "net", false, false);
			};
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00115F1C File Offset: 0x0011411C
		private void {25880}()
		{
			this.clientBase = new ReskanaClient();
			this.clientBase.ConnectionBroken += this.{25896};
			this.clientBase.NextPacket += this.{25888};
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x00115F58 File Offset: 0x00114158
		public bool Start(IPEndPoint {25881}, out SocketError {25882}, out string {25883})
		{
			{25883} = string.Empty;
			Networking.lastSendMessages.Clear();
			Networking.lastReceiveMessages.Clear();
			this.{25904}.Reset();
			try
			{
				this.clientBase.Connect({25881});
			}
			catch (Exception)
			{
				{25882} = SocketError.NotConnected;
				this.{25905} = false;
				this.{25906} = false;
				this.Netstat.Reset();
				return false;
			}
			Global.Network.Conection.timeFromLastReceivedPacket.Restart();
			Global.Network.Conection.timeFromLastSentPacket.Restart();
			this.{25906} = true;
			this.{25905} = true;
			this.clientBase.StartReceiving();
			{25882} = SocketError.Success;
			return true;
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x00116018 File Offset: 0x00114218
		public void Stop()
		{
			if (!this.{25905})
			{
				return;
			}
			this.{25906} = false;
			this.{25905} = false;
			this.clientBase.Disconnect(true, true);
			object obj = this.{25908};
			lock (obj)
			{
				this.{25909}.Clear();
			}
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x00116084 File Offset: 0x00114284
		public void Update(ref FrameTime {25884})
		{
			if ({25884}.EvaluteTimerMs2(ref this.{25902}))
			{
				this.{25902} = 1000f;
				this.NetstatPs.CopyFrom(this.{25903});
				this.{25903}.Reset();
			}
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x001160BC File Offset: 0x001142BC
		public void Send(IPacketBase {25885})
		{
			if (!this.{25906})
			{
				return;
			}
			Global.Network.Conection.timeFromLastSentPacket.Restart();
			object obj = this.{25907};
			lock (obj)
			{
				if ({25885}.Flow == FlowOfPack.FromServerToClient)
				{
					throw new InvalidOperationException();
				}
				this.{25901}.StartIndex = 0;
				this.{25901}.LastOperationBytesCount = 0;
				DeltaStream.Boxing(ReflectivePacketID.TransferReflectionSource, {25885}, this.{25901}, true);
				Tlist<Type> tlist = Networking.lastSendMessages;
				Type type = {25885}.GetType();
				tlist.Add(type);
				if (Networking.lastSendMessages.Size > 5)
				{
					Networking.lastSendMessages.RemoveAt(0);
				}
				ReskanaClient reskanaClient = this.clientBase;
				BufferSegmentStruct bufferSegmentStruct = new BufferSegmentStruct(this.{25901}.UsedBuffer, this.{25901}.StartIndex, this.{25901}.LastOperationBytesCount);
				reskanaClient.Send(bufferSegmentStruct, false);
				this.Netstat.sendBytes += this.{25901}.LastOperationBytesCount;
				this.Netstat.sendPackets++;
				this.{25903}.sendBytes += this.{25901}.LastOperationBytesCount;
				this.{25903}.sendPackets++;
			}
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x00116218 File Offset: 0x00114418
		public void Query<T>(IPacketBase {25886}, Action<T> {25887}) where T : IPacketBase
		{
			if (!this.{25906})
			{
				return;
			}
			this.Routers.AddTemporary<T>(delegate(ref T {25912})
			{
				{25887}({25912});
			}, ServerTaskType.NotStated);
			this.Send({25886});
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x0011625C File Offset: 0x0011445C
		private void {25888}(BufferSegmentStruct {25889})
		{
			if (!this.{25906})
			{
				return;
			}
			this.Netstat.readPackets++;
			this.Netstat.readBytes += {25889}.Length;
			this.{25903}.readPackets++;
			this.{25903}.readBytes += {25889}.Length;
			Global.Network.Conection.timeFromLastReceivedPacket.Restart();
			UnboxingResult unboxingResult = this.{25904}.Proceed(ReflectivePacketID.TransferReflectionSource, {25889});
			if (unboxingResult.Error == UnboxingError.LessBytes)
			{
				return;
			}
			this.{25910} = unboxingResult.FinalPacketType.Name;
			Networking.lastReceiveMessages.Add(unboxingResult.FinalPacketType);
			if (Networking.lastReceiveMessages.Size > 5)
			{
				Networking.lastReceiveMessages.RemoveAt(0);
			}
			if (unboxingResult.FinalPacketType.Equals(typeof(OnDisconnectionFromServerMsg)))
			{
				this.OnExternalDisconnect(new OnDisconnectionFromServerMsg?((OnDisconnectionFromServerMsg)unboxingResult.Packet));
				return;
			}
			if (unboxingResult.FinalPacketType.Equals(typeof(OnPingMsg)))
			{
				Tlist<SavedLogbookMessage> tlist;
				if (Global.Settings.Logbook.NeedToSend.Size > 50)
				{
					tlist = new Tlist<SavedLogbookMessage>();
					tlist.Add(Global.Settings.Logbook.NeedToSend, 0, 50);
					Global.Settings.Logbook.NeedToSend.RemoveRange(0, 50);
				}
				else
				{
					tlist = Global.Settings.Logbook.NeedToSend.Clone();
					Global.Settings.Logbook.NeedToSend.Clear();
				}
				this.Send(new OnPingResultMsg((float)Global.Game.GameTotalTimeSec, tlist));
			}
			object obj = this.{25908};
			lock (obj)
			{
				this.{25909}.Add(unboxingResult);
			}
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x0011644C File Offset: 0x0011464C
		public void CompletePackets()
		{
			object obj = this.{25908};
			lock (obj)
			{
				for (int i = 0; i < this.{25909}.Size; i++)
				{
					this.Routers.Proceed(ref this.{25909}.Array[i]);
					LocalSettings settings = Global.Settings;
					if (settings != null)
					{
						LogbookController logbook = settings.Logbook;
						if (logbook != null)
						{
							logbook.DontBatchNext();
						}
					}
				}
				this.{25909}.Clear();
			}
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x001164E0 File Offset: 0x001146E0
		private void OnExternalDisconnect(OnDisconnectionFromServerMsg? {25890})
		{
			if (this.{25898} == null)
			{
				throw new InvalidOperationException("DisconnectionMessageHandler in ClientNativeCore is null!");
			}
			this.{25898}({25890});
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x00116501 File Offset: 0x00114701
		private void {25891}(ref UnboxingResult {25892})
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			stringBuilder.Append("  RECV  ");
			stringBuilder.Append({25892}.LengthInBytes);
			stringBuilder.Append(" bytes, Packet=");
			stringBuilder.Append({25892}.FinalPacketType.Name);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x00116540 File Offset: 0x00114740
		private void {25893}(int {25894}, string {25895})
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			stringBuilder.Append("  SEND  ");
			stringBuilder.Append({25894});
			stringBuilder.Append(" bytes, Packet=");
			stringBuilder.Append({25895});
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x00116588 File Offset: 0x00114788
		[CompilerGenerated]
		private void {25896}(BrokenConnectionInfo {25897})
		{
			if ({25897}.ByException != null)
			{
				Helpers.SendError({25897}.ByException, this.{25910} + " | " + {25897}.ByException.Message, false, false);
				this.OnExternalDisconnect(null);
				return;
			}
			if ({25897}.ByMalformation)
			{
				if (Global.Game != null && Session.Account != null && Session.Account != null)
				{
					string playerName = Session.Account.PlayerName;
				}
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 2);
				defaultInterpolatedStringHandler.AppendLiteral("Neterror ");
				defaultInterpolatedStringHandler.AppendFormatted({25897}.ToString());
				defaultInterpolatedStringHandler.AppendLiteral(" scene");
				defaultInterpolatedStringHandler.AppendFormatted<GameSceneName>(Global.Game.GetCurrentSceneName);
				Helpers.SendError(new Exception(defaultInterpolatedStringHandler.ToStringAndClear()), "net", false, false);
				this.OnExternalDisconnect(null);
				return;
			}
			if (this.{25906})
			{
				this.{25906} = false;
				this.OnExternalDisconnect(null);
			}
		}

		// Token: 0x04001E97 RID: 7831
		private const int logSize = 5;

		// Token: 0x04001E98 RID: 7832
		public static Tlist<Type> lastSendMessages = new Tlist<Type>(6);

		// Token: 0x04001E99 RID: 7833
		public static Tlist<Type> lastReceiveMessages = new Tlist<Type>(6);

		// Token: 0x04001E9A RID: 7834
		[CompilerGenerated]
		private Action<OnDisconnectionFromServerMsg?> {25898};

		// Token: 0x04001E9B RID: 7835
		[CompilerGenerated]
		private Action {25899};

		// Token: 0x04001E9C RID: 7836
		[CompilerGenerated]
		private Action {25900};

		// Token: 0x04001E9D RID: 7837
		public readonly NetworkStatistics Netstat;

		// Token: 0x04001E9E RID: 7838
		public readonly NetworkStatistics NetstatPs;

		// Token: 0x04001E9F RID: 7839
		public readonly ServerL7 Routers;

		// Token: 0x04001EA0 RID: 7840
		public ReskanaClient clientBase;

		// Token: 0x04001EA1 RID: 7841
		private BufferedDataHolder {25901};

		// Token: 0x04001EA2 RID: 7842
		private float {25902} = 1000f;

		// Token: 0x04001EA3 RID: 7843
		private NetworkStatistics {25903};

		// Token: 0x04001EA4 RID: 7844
		private UnboxingHelper {25904};

		// Token: 0x04001EA5 RID: 7845
		private bool {25905};

		// Token: 0x04001EA6 RID: 7846
		private bool {25906};

		// Token: 0x04001EA7 RID: 7847
		private object {25907} = new object();

		// Token: 0x04001EA8 RID: 7848
		private object {25908} = new object();

		// Token: 0x04001EA9 RID: 7849
		private Tlist<UnboxingResult> {25909} = new Tlist<UnboxingResult>(50);

		// Token: 0x04001EAA RID: 7850
		private string {25910} = string.Empty;
	}
}
