using System;

namespace TheraEngine.PacketValues
{
	// Token: 0x02000076 RID: 118
	public struct Bits256
	{
		// Token: 0x1700008E RID: 142
		public bool this[int {12481}]
		{
			get
			{
				if ({12481} < 64)
				{
					return (this.a1 & 1L << {12481}) != 0L;
				}
				if ({12481} < 128)
				{
					return (this.a2 & 1L << {12481} - 64) != 0L;
				}
				if ({12481} < 192)
				{
					return (this.a3 & 1L << {12481} - 128) != 0L;
				}
				if ({12481} < 256)
				{
					return (this.a3 & 1L << {12481} - 192) != 0L;
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (value)
				{
					if ({12482} < 64)
					{
						this.a1 |= 1L << {12482};
						return;
					}
					if ({12482} < 128)
					{
						this.a2 |= 1L << {12482} - 64;
						return;
					}
					if ({12482} < 192)
					{
						this.a3 |= 1L << {12482} - 128;
						return;
					}
					if ({12482} < 256)
					{
						this.a4 |= 1L << {12482} - 192;
						return;
					}
				}
				else
				{
					if ({12482} < 64)
					{
						this.a1 &= ~(1L << {12482});
						return;
					}
					if ({12482} < 128)
					{
						this.a2 &= ~(1L << {12482} - 64);
						return;
					}
					if ({12482} < 192)
					{
						this.a3 &= ~(1L << {12482} - 128);
						return;
					}
					if ({12482} < 256)
					{
						this.a4 &= ~(1L << {12482} - 192);
					}
				}
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000109C5 File Offset: 0x0000EBC5
		public void Clean()
		{
			this.a1 = 0L;
			this.a2 = 0L;
			this.a3 = 0L;
			this.a4 = 0L;
		}

		// Token: 0x04000255 RID: 597
		public long a1;

		// Token: 0x04000256 RID: 598
		public long a2;

		// Token: 0x04000257 RID: 599
		public long a3;

		// Token: 0x04000258 RID: 600
		public long a4;
	}
}
