using System;

namespace World_Of_Sea_Battle.Components.Client
{
	// Token: 0x02000555 RID: 1365
	internal class NetworkStatistics
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x001166CA File Offset: 0x001148CA
		public int BytesSend
		{
			get
			{
				return this.sendBytes;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x001166D2 File Offset: 0x001148D2
		public int BytesRead
		{
			get
			{
				return this.readBytes;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x001166DA File Offset: 0x001148DA
		public int PacketsSend
		{
			get
			{
				return this.sendPackets;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x001166E2 File Offset: 0x001148E2
		public int PacketsRead
		{
			get
			{
				return this.readPackets;
			}
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x001166EA File Offset: 0x001148EA
		public NetworkStatistics()
		{
			this.sendBytes = 0;
			this.sendPackets = 0;
			this.readBytes = 0;
			this.readPackets = 0;
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x00116710 File Offset: 0x00114910
		public NetworkStatistics Substruct(NetworkStatistics {25913})
		{
			return new NetworkStatistics
			{
				sendBytes = this.sendBytes - {25913}.sendBytes,
				readBytes = this.readBytes - {25913}.readBytes,
				sendPackets = this.sendPackets - {25913}.sendPackets,
				readPackets = this.readPackets - {25913}.readPackets
			};
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x0011676E File Offset: 0x0011496E
		public void CopyFrom(NetworkStatistics {25914})
		{
			this.sendBytes = {25914}.sendBytes;
			this.readBytes = {25914}.readBytes;
			this.sendPackets = {25914}.sendPackets;
			this.readPackets = {25914}.readPackets;
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x001167A0 File Offset: 0x001149A0
		public NetworkStatistics Clone()
		{
			return new NetworkStatistics
			{
				sendBytes = this.sendBytes,
				readBytes = this.readBytes,
				sendPackets = this.sendPackets,
				readPackets = this.readPackets
			};
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x001167D7 File Offset: 0x001149D7
		internal void Reset()
		{
			this.sendBytes = 0;
			this.sendPackets = 0;
			this.readBytes = 0;
			this.readPackets = 0;
		}

		// Token: 0x04001EAE RID: 7854
		internal int sendBytes;

		// Token: 0x04001EAF RID: 7855
		internal int readBytes;

		// Token: 0x04001EB0 RID: 7856
		internal int sendPackets;

		// Token: 0x04001EB1 RID: 7857
		internal int readPackets;
	}
}
