using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ManualPacketSerialization;
using ManualPacketSerialization.Externs;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000522 RID: 1314
	internal sealed class SavedCredentials : ITKSerializable
	{
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x0010AF7A File Offset: 0x0010917A
		public bool HasAnySavedCreds
		{
			get
			{
				return this.{25535}.Count > 0;
			}
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0010AF8C File Offset: 0x0010918C
		[return: TupleElementNames(new string[]
		{
			"login",
			"password"
		})]
		public ValueTuple<string, string> GetSaved(string {25522})
		{
			ValueTuple<string, string> result;
			if (this.{25535}.TryGetValue({25522}, out result))
			{
				return result;
			}
			return new ValueTuple<string, string>(string.Empty, string.Empty);
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0010AFBC File Offset: 0x001091BC
		public void WhenSuccessLogin(string {25523}, string {25524})
		{
			string savedServerID = Global.Settings.SavedServerID;
			this.LastLoginServer = savedServerID;
			ValueTuple<string, string> valueTuple;
			if (this.{25535}.TryGetValue(savedServerID, out valueTuple))
			{
				this.{25535}[savedServerID] = new ValueTuple<string, string>({25523}, {25524});
				return;
			}
			this.{25535}.Add(savedServerID, new ValueTuple<string, string>({25523}, {25524}));
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0010B014 File Offset: 0x00109214
		public void UpdatePass(string {25525})
		{
			string savedServerID = Global.Settings.SavedServerID;
			if (this.{25535}.ContainsKey(savedServerID))
			{
				this.{25535}[savedServerID] = new ValueTuple<string, string>(this.{25535}[savedServerID].Item1, {25525});
			}
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0010B05D File Offset: 0x0010925D
		public void OldInit(string {25526}, string {25527}, string {25528})
		{
			this.LastLoginServer = {25526};
			this.{25535}.Add({25526}, new ValueTuple<string, string>({25527}, {25528}));
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x00003100 File Offset: 0x00001300
		public void PreInit()
		{
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x00003100 File Offset: 0x00001300
		public void PostInit()
		{
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0010B097 File Offset: 0x00109297
		public void Boxing(TKWriterExtern {25529})
		{
			{25529}.Write(1, this.LastLoginServer);
			{25529}.WriteContent(2, new Action<WriterExtern>(this.{25533}));
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0010B0BC File Offset: 0x001092BC
		public void Load(short {25530}, WriterExtern {25531}, out bool {25532})
		{
			{25532} = true;
			if ({25530} == 1)
			{
				{25531}.ReadString(out this.LastLoginServer, null);
				return;
			}
			if ({25530} != 2)
			{
				{25532} = false;
				return;
			}
			byte b = {25531}.ReadByte();
			for (int i = 0; i < (int)b; i++)
			{
				string key;
				{25531}.ReadString(out key, null);
				string item;
				{25531}.ReadString(out item, null);
				string item2;
				{25531}.ReadString(out item2, null);
				this.{25535}.Add(key, new ValueTuple<string, string>(item, item2));
			}
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0010B12C File Offset: 0x0010932C
		[NullableContext(1)]
		[CompilerGenerated]
		private void {25533}(WriterExtern {25534})
		{
			{25534}.WriteByte((byte)this.{25535}.Count);
			foreach (KeyValuePair<string, ValueTuple<string, string>> keyValuePair in this.{25535})
			{
				{25534}.Write(keyValuePair.Key, null);
				{25534}.Write(keyValuePair.Value.Item1, null);
				{25534}.Write(keyValuePair.Value.Item2, null);
			}
		}

		// Token: 0x04001CDC RID: 7388
		[TupleElementNames(new string[]
		{
			"login",
			"password"
		})]
		private Dictionary<string, ValueTuple<string, string>> {25535} = new Dictionary<string, ValueTuple<string, string>>();

		// Token: 0x04001CDD RID: 7389
		public string LastLoginServer = "";
	}
}
