using System;

namespace TheraEngine
{
	// Token: 0x02000030 RID: 48
	public class TickCounter
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00008DB0 File Offset: 0x00006FB0
		public int CurrentValue
		{
			get
			{
				return this.{11814};
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008DB8 File Offset: 0x00006FB8
		public TickCounter(int {11813})
		{
			this.{11815} = {11813};
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00008DC7 File Offset: 0x00006FC7
		public bool Tick()
		{
			this.{11814}++;
			if (this.{11814} >= this.{11815})
			{
				this.{11814} = 0;
				return true;
			}
			return false;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00008DEF File Offset: 0x00006FEF
		public void Reset()
		{
			this.{11814} = 0;
		}

		// Token: 0x040000DD RID: 221
		private int {11814};

		// Token: 0x040000DE RID: 222
		private readonly int {11815};
	}
}
