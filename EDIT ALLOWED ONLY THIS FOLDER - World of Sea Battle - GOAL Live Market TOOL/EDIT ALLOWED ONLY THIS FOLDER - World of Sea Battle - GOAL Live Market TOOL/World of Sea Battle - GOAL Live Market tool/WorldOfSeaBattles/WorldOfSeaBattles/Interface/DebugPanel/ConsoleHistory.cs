using System;

namespace WorldOfSeaBattles.Interface.DebugPanel
{
	// Token: 0x0200058F RID: 1423
	public class ConsoleHistory
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060020E7 RID: 8423 RVA: 0x0012707C File Offset: 0x0012527C
		public int MaxSize { get; } = 100;

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x00127084 File Offset: 0x00125284
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x0012708C File Offset: 0x0012528C
		public ConsoleHistory()
		{
			this.history = new string[this.MaxSize];
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x001270B4 File Offset: 0x001252B4
		public void Add(string command)
		{
			this.lastIndex++;
			if (this.lastIndex >= this.MaxSize)
			{
				this.lastIndex = 0;
			}
			this.count = Math.Clamp(this.count + 1, 0, this.MaxSize);
			this.history[this.lastIndex] = command;
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x0012710C File Offset: 0x0012530C
		public string Last(int extra = 0)
		{
			extra = Math.Clamp(extra, 0, this.MaxSize - 1);
			int num = this.lastIndex - extra;
			if (num < 0)
			{
				num += this.MaxSize;
			}
			return this.history[num] ?? "";
		}

		// Token: 0x04001FFB RID: 8187
		private readonly string[] history;

		// Token: 0x04001FFC RID: 8188
		private int lastIndex = -1;

		// Token: 0x04001FFD RID: 8189
		private int count;
	}
}
