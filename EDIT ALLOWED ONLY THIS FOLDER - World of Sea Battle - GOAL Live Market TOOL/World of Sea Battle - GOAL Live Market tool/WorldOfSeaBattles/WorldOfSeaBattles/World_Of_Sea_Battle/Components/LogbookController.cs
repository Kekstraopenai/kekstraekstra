using System;
using System.Collections.Generic;
using System.Diagnostics;
using Common;
using Common.Packets;
using ManualPacketSerialization;
using ManualPacketSerialization.Externs;
using TheraEngine.Collections;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x0200050E RID: 1294
	public class LogbookController : IMPSerializable
	{
		// Token: 0x06001CEF RID: 7407 RVA: 0x00109166 File Offset: 0x00107366
		public void DontBatchNext()
		{
			this.{25413} = true;
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x00109170 File Offset: 0x00107370
		public void Write(string {25402}, LBFlags {25403})
		{
			try
			{
				DateTime now = DateTime.Now;
				bool flag;
				if (!this.{25413} && this.{25412}.Elapsed.TotalMilliseconds < 1.0 && this.AllMessages.Size > 0)
				{
					LogbookController.LogbookMessage logbookMessage = this.AllMessages.Array[this.AllMessages.Size - 1];
					if ({25403} == logbookMessage.Flags)
					{
						string text = logbookMessage.Text;
						string text2 = text;
						if (text2[text2.Length - 1] == ' ')
						{
							string text3 = text;
							text = text3.Substring(0, text3.Length - 1);
						}
						string text4 = text;
						char c = text4[text4.Length - 1];
						if (c != ':' && c != ',' && c != ';' && c != '.')
						{
							text += ",";
						}
						text = text + " " + {25402};
						this.AllMessages.Array[this.AllMessages.Size - 1] = new LogbookController.LogbookMessage(text, now, logbookMessage.Flags);
						flag = ({25403} - LBFlags.L1 <= 1);
						if (flag && this.NeedToSend.Size > 0)
						{
							this.NeedToSend[this.NeedToSend.Size - 1] = new SavedLogbookMessage(text, now);
						}
						return;
					}
				}
				this.{25413} = false;
				this.{25412}.Restart();
				Tlist<LogbookController.LogbookMessage> allMessages = this.AllMessages;
				LogbookController.LogbookMessage logbookMessage2 = new LogbookController.LogbookMessage({25402}, now, {25403});
				allMessages.Add(logbookMessage2);
				flag = ({25403} - LBFlags.L1 <= 1);
				if (flag)
				{
					Tlist<SavedLogbookMessage> needToSend = this.NeedToSend;
					SavedLogbookMessage savedLogbookMessage = new SavedLogbookMessage({25402}, now);
					needToSend.Add(savedLogbookMessage);
				}
				this.{25410}++;
				this.{25411}++;
				while (this.AllMessages.Size >= 500)
				{
					this.AllMessages.RemoveAt(0);
					this.{25410} = Math.Min(this.{25410}, this.AllMessages.Size);
				}
			}
			catch (IndexOutOfRangeException {25356})
			{
				Helpers.SendError({25356}, "Write logbook: " + {25402}, true, false);
			}
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x001093A4 File Offset: 0x001075A4
		public IEnumerable<LogbookController.LogbookMessage> FetchLast(int {25404}, string {25405} = null)
		{
			LogbookController.<FetchLast>d__12 <FetchLast>d__ = new LogbookController.<FetchLast>d__12(-2);
			<FetchLast>d__.<>4__this = this;
			<FetchLast>d__.<>3__last = {25404};
			<FetchLast>d__.<>3__useFilter = {25405};
			return <FetchLast>d__;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x001093C2 File Offset: 0x001075C2
		public IEnumerable<LogbookController.LogbookMessage[]> FetchAll(int {25406})
		{
			LogbookController.<FetchAll>d__13 <FetchAll>d__ = new LogbookController.<FetchAll>d__13(-2);
			<FetchAll>d__.<>4__this = this;
			<FetchAll>d__.<>3__messagesPerPage = {25406};
			return <FetchAll>d__;
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x001093DC File Offset: 0x001075DC
		public void Update()
		{
			for (int i = 0; i < this.{25410}; i++)
			{
				LogbookController.LogbookMessage logbookMessage = this.AllMessages.Array[this.AllMessages.Size - 1 - i];
				ChatRoomType logbookRoom = {22478}.LogbookRoom;
				ChatMessageDefault chatMessageDefault = new ChatMessageDefault(DateTime.Now.TimeOfDay.ToString("hh\\:mm"), logbookMessage.Text, 0U, LocaleInfo.Current.Id);
				OnChatMessageEvent onChatMessageEvent = new OnChatMessageEvent(logbookRoom, ref chatMessageDefault);
				{22478}.Put(onChatMessageEvent);
			}
			if (Global.Player != null && !Global.Player.IsPortEntry && this.{25411} > 0)
			{
				this.Flush();
			}
			this.{25410} = 0;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0010948A File Offset: 0x0010768A
		public void Flush()
		{
			this.{25409}.Clear();
			this.{25409}.Add(this.AllMessages);
			this.{25411} = 0;
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x001094AF File Offset: 0x001076AF
		public void Boxing(WriterExtern {25407})
		{
			{25407}.WriteTlistStruct(this.{25409});
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x001094BD File Offset: 0x001076BD
		public void Unboxing(WriterExtern {25408})
		{
			{25408}.ReadTlistStruct(out this.{25409});
			this.AllMessages.Add(this.{25409});
		}

		// Token: 0x04001C90 RID: 7312
		public const int MaxMessages = 500;

		// Token: 0x04001C91 RID: 7313
		public Tlist<LogbookController.LogbookMessage> AllMessages = new Tlist<LogbookController.LogbookMessage>();

		// Token: 0x04001C92 RID: 7314
		private Tlist<LogbookController.LogbookMessage> {25409} = new Tlist<LogbookController.LogbookMessage>();

		// Token: 0x04001C93 RID: 7315
		public Tlist<SavedLogbookMessage> NeedToSend = new Tlist<SavedLogbookMessage>();

		// Token: 0x04001C94 RID: 7316
		private int {25410};

		// Token: 0x04001C95 RID: 7317
		private int {25411};

		// Token: 0x04001C96 RID: 7318
		private Stopwatch {25412} = Stopwatch.StartNew();

		// Token: 0x04001C97 RID: 7319
		private bool {25413};

		// Token: 0x0200050F RID: 1295
		public struct LogbookMessage : IMPSerializable
		{
			// Token: 0x06001CF7 RID: 7415 RVA: 0x001094DC File Offset: 0x001076DC
			public LogbookMessage(string {25417}, DateTime {25418}, LBFlags {25419})
			{
				this.Text = {25417};
				this.LocalTime = {25418};
				this.Flags = {25419};
			}

			// Token: 0x1700029F RID: 671
			// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x001094F3 File Offset: 0x001076F3
			public string ShortDateTimeFormat
			{
				get
				{
					return this.LocalTime.ToString("HH:mm");
				}
			}

			// Token: 0x06001CF9 RID: 7417 RVA: 0x00109505 File Offset: 0x00107705
			public void Boxing(WriterExtern {25420})
			{
				{25420}.Write(this.Text, null);
				{25420}.WriteStruct<long>(this.LocalTime.Ticks);
				{25420}.WriteByte((byte)this.Flags);
			}

			// Token: 0x06001CFA RID: 7418 RVA: 0x00109534 File Offset: 0x00107734
			public void Unboxing(WriterExtern {25421})
			{
				{25421}.ReadString(out this.Text, null);
				long ticks;
				{25421}.ReadStruct<long>(out ticks);
				this.LocalTime = new DateTime(ticks);
				this.Flags = (LBFlags){25421}.ReadByte();
			}

			// Token: 0x04001C98 RID: 7320
			public string Text;

			// Token: 0x04001C99 RID: 7321
			public DateTime LocalTime;

			// Token: 0x04001C9A RID: 7322
			public LBFlags Flags;
		}
	}
}
