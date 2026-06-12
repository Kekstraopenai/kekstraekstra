using System;
using System.Runtime.CompilerServices;
using World_Of_Sea_Battle.Components.Client;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000528 RID: 1320
	public class SyncErrorTracker
	{
		// Token: 0x06001D79 RID: 7545 RVA: 0x0010BE2F File Offset: 0x0010A02F
		private static string getPackets()
		{
			return "Send:" + EntryPoint.substructPackets(Networking.lastSendMessages) + ";Receive: " + EntryPoint.substructPackets(Networking.lastReceiveMessages);
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0010BE54 File Offset: 0x0010A054
		public void AssertEquality(string {25569}, int {25570}, string {25571}, string {25572})
		{
			if ({25571} == {25572})
			{
				this.{25573} = 0;
				return;
			}
			if (this.{25573} == 0)
			{
				this.{25574} = SyncErrorTracker.getPackets();
				this.{25575} = {25571};
				this.{25576} = {25572};
			}
			this.{25573}++;
			if (this.{25573} == {25570})
			{
				Exception {25356} = new AssertFailException();
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(94, 5);
				defaultInterpolatedStringHandler.AppendLiteral("Рассинхронизация ");
				defaultInterpolatedStringHandler.AppendFormatted({25569});
				defaultInterpolatedStringHandler.AppendLiteral(": Клиент: (");
				defaultInterpolatedStringHandler.AppendFormatted(this.{25575});
				defaultInterpolatedStringHandler.AppendLiteral("), Сервер: (");
				defaultInterpolatedStringHandler.AppendFormatted(this.{25576});
				defaultInterpolatedStringHandler.AppendLiteral("), ");
				defaultInterpolatedStringHandler.AppendLiteral("Пакеты на момент обнаружения: (");
				defaultInterpolatedStringHandler.AppendFormatted(this.{25574});
				defaultInterpolatedStringHandler.AppendLiteral("), Пакеты сейчас: (");
				defaultInterpolatedStringHandler.AppendFormatted(SyncErrorTracker.getPackets());
				defaultInterpolatedStringHandler.AppendLiteral(")");
				Helpers.SendError({25356}, defaultInterpolatedStringHandler.ToStringAndClear(), false, false);
			}
		}

		// Token: 0x04001CEE RID: 7406
		private int {25573};

		// Token: 0x04001CEF RID: 7407
		private string {25574};

		// Token: 0x04001CF0 RID: 7408
		private string {25575};

		// Token: 0x04001CF1 RID: 7409
		private string {25576};
	}
}
