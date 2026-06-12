using System;

namespace TheraEngine
{
	// Token: 0x0200001C RID: 28
	public class DisposableObject : IDisposable
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003B0B File Offset: 0x00001D0B
		public bool IsDisposed
		{
			get
			{
				return this.{11402};
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003B13 File Offset: 0x00001D13
		public DisposableObject()
		{
			this.{11402} = false;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003B24 File Offset: 0x00001D24
		~DisposableObject()
		{
			if (!this.{11402})
			{
				this.Dispose();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003B58 File Offset: 0x00001D58
		public virtual void Dispose()
		{
			if (!this.{11402})
			{
				this.{11402} = true;
				this.Tag = null;
			}
		}

		// Token: 0x0400008A RID: 138
		public object Tag;

		// Token: 0x0400008B RID: 139
		private bool {11402};
	}
}
