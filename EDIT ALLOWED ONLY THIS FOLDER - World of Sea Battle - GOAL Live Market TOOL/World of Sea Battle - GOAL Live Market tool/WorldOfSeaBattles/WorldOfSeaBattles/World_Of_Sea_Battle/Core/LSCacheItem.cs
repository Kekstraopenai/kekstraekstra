using System;
using Common;
using ManualPacketSerialization;
using ManualPacketSerialization.Externs;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;

namespace World_Of_Sea_Battle.Core
{
	// Token: 0x020004E5 RID: 1253
	internal sealed class LSCacheItem : IMPSerializable
	{
		// Token: 0x06001BCD RID: 7117 RVA: 0x000FF9B4 File Offset: 0x000FDBB4
		public LSCacheItem(string {25147}, uint {25148}, bool {25149})
		{
			this.TargetName = {25147};
			this.TargetSID = {25148};
			this.IsOnline = {25149};
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x000FF9DC File Offset: 0x000FDBDC
		public LSCacheItem()
		{
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x000FF9EF File Offset: 0x000FDBEF
		void IMPSerializable.{25150}(WriterExtern {25151})
		{
			{25151}.Write(this.TargetName, null);
			{25151}.WriteStruct<uint>(this.TargetSID);
			{25151}.WriteTlistImps(this.SavedMessages);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x000FFA16 File Offset: 0x000FDC16
		void IMPSerializable.{25152}(WriterExtern {25153})
		{
			{25153}.ReadString(out this.TargetName, null);
			{25153}.ReadStruct<uint>(out this.TargetSID);
			{25153}.ReadTlistImps(out this.SavedMessages);
		}

		// Token: 0x04001A85 RID: 6789
		public string TargetName;

		// Token: 0x04001A86 RID: 6790
		public uint TargetSID;

		// Token: 0x04001A87 RID: 6791
		public bool NewMessages;

		// Token: 0x04001A88 RID: 6792
		public bool IsOnline;

		// Token: 0x04001A89 RID: 6793
		internal Tlist<LSCacheItem.MessageWithColor> SavedMessages = new Tlist<LSCacheItem.MessageWithColor>();

		// Token: 0x020004E6 RID: 1254
		internal class MessageWithColor : IMPSerializable
		{
			// Token: 0x06001BD1 RID: 7121 RVA: 0x000FFA3D File Offset: 0x000FDC3D
			public void Boxing(WriterExtern {25154})
			{
				{25154}.Write(this.Text, null);
				{25154}.WriteStruct<Color>(this.Color);
				{25154}.WriteByte(this.GuildRankId);
			}

			// Token: 0x06001BD2 RID: 7122 RVA: 0x000FFA64 File Offset: 0x000FDC64
			public void Unboxing(WriterExtern {25155})
			{
				{25155}.ReadString(out this.Text, null);
				{25155}.ReadStruct<Color>(out this.Color);
				this.GuildRankId = {25155}.ReadByte();
			}

			// Token: 0x04001A8A RID: 6794
			public string Text;

			// Token: 0x04001A8B RID: 6795
			public Color Color;

			// Token: 0x04001A8C RID: 6796
			public byte GuildRankId;
		}
	}
}
