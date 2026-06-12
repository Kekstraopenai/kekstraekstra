using System;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020000B4 RID: 180
	internal class {17549}
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x00024598 File Offset: 0x00022798
		public {17549}(uint {17553}, string {17554}, params {17549}.OptionalAction[] {17555})
		{
			this.AccountSID = {17553};
			this.Name = this.{17556}({17554});
			this.OptionalActions = {17555};
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000245C8 File Offset: 0x000227C8
		private string {17556}(string {17557})
		{
			if ({17557}.Length > 5 && {17557}[0] == '[' && {17557}[4] == ']')
			{
				return {17557}.Substring(6, {17557}.Length - 6);
			}
			return {17557};
		}

		// Token: 0x040003D8 RID: 984
		public uint AccountSID;

		// Token: 0x040003D9 RID: 985
		public string Name;

		// Token: 0x040003DA RID: 986
		public {17549}.OptionalAction[] OptionalActions;

		// Token: 0x040003DB RID: 987
		public string ChatMessage = string.Empty;

		// Token: 0x020000B5 RID: 181
		public enum OptionalAction
		{
			// Token: 0x040003DD RID: 989
			Not,
			// Token: 0x040003DE RID: 990
			AcceptOrRejectInviteToGuild,
			// Token: 0x040003DF RID: 991
			RemoveFromGuildOrChangeStatus,
			// Token: 0x040003E0 RID: 992
			ReplyChat,
			// Token: 0x040003E1 RID: 993
			CopyMessage
		}
	}
}
