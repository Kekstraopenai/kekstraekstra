using System;
using TheraEngine.Collections;

namespace TheraEngine.Reactive
{
	// Token: 0x02000067 RID: 103
	public class ReactSubscribtion<T> : IDisposable
	{
		// Token: 0x0600029D RID: 669 RVA: 0x0000F942 File Offset: 0x0000DB42
		internal ReactSubscribtion(Tlist<ReactSubscribtion<T>> {12386}, Action<T> {12387})
		{
			this.source = {12386};
			this.action = {12387};
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000F958 File Offset: 0x0000DB58
		public void Dispose()
		{
			Tlist<ReactSubscribtion<T>> tlist = this.source;
			if (tlist != null)
			{
				tlist.FastRemove(this);
			}
			this.source = null;
		}

		// Token: 0x0400023A RID: 570
		private Tlist<ReactSubscribtion<T>> source;

		// Token: 0x0400023B RID: 571
		internal Action<T> action;
	}
}
