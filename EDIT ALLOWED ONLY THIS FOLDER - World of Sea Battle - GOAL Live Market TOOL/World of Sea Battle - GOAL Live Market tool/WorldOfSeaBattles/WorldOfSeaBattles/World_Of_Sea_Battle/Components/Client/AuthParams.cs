using System;
using System.Net.Sockets;
using Common.Account;
using Common.Game;
using Common.Packets;

namespace World_Of_Sea_Battle.Components.Client
{
	// Token: 0x0200054F RID: 1359
	internal class AuthParams
	{
		// Token: 0x04001E80 RID: 7808
		public string Query;

		// Token: 0x04001E81 RID: 7809
		public string Password;

		// Token: 0x04001E82 RID: 7810
		public bool RegNew;

		// Token: 0x04001E83 RID: 7811
		public string RegName;

		// Token: 0x04001E84 RID: 7812
		public uint RegReferalSID;

		// Token: 0x04001E85 RID: 7813
		public ExternalAuth External;

		// Token: 0x04001E86 RID: 7814
		public PlatformType Platform;

		// Token: 0x04001E87 RID: 7815
		public bool IsConnected;

		// Token: 0x04001E88 RID: 7816
		public SocketError SocketStatus;

		// Token: 0x04001E89 RID: 7817
		public LoginQueryResult LastOperationResult = LoginQueryResult.Error;

		// Token: 0x04001E8A RID: 7818
		public PlayerAccount LastOperationResultAccount;
	}
}
