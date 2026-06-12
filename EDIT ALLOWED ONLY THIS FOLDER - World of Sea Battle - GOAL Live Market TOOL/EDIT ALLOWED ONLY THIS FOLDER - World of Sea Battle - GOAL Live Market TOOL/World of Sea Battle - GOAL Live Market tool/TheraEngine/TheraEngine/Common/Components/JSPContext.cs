using System;
using System.Collections.Generic;

namespace Common.Components
{
	// Token: 0x02000009 RID: 9
	public class JSPContext
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002A33 File Offset: 0x00000C33
		public JSPContext(string {11262})
		{
			this.Filename = {11262};
		}

		// Token: 0x0400001C RID: 28
		public readonly string Filename;

		// Token: 0x0400001D RID: 29
		public bool ConvertNumbersToString = true;

		// Token: 0x0400001E RID: 30
		public bool WriteAlerts;

		// Token: 0x0400001F RID: 31
		public List<string> AlertsOrNull;

		// Token: 0x04000020 RID: 32
		public bool CaseInsensitive = true;
	}
}
