using System;

namespace TheraEngine.PacketValues
{
	// Token: 0x02000075 RID: 117
	public struct Bits128
	{
		// Token: 0x1700008D RID: 141
		public bool this[int {12478}]
		{
			get
			{
				if ({12478} < 64)
				{
					return (this.a1 & 1L << {12478}) != 0L;
				}
				if ({12478} < 128)
				{
					return (this.a2 & 1L << {12478} - 64) != 0L;
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (value)
				{
					if ({12479} < 64)
					{
						this.a1 |= 1L << {12479};
						return;
					}
					if ({12479} < 128)
					{
						this.a2 |= 1L << {12479} - 64;
						return;
					}
				}
				else
				{
					if ({12479} < 64)
					{
						this.a1 &= ~(1L << {12479});
						return;
					}
					if ({12479} < 128)
					{
						this.a2 &= ~(1L << {12479} - 64);
					}
				}
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0001080D File Offset: 0x0000EA0D
		public void Clean()
		{
			this.a1 = 0L;
			this.a2 = 0L;
		}

		// Token: 0x04000253 RID: 595
		public long a1;

		// Token: 0x04000254 RID: 596
		public long a2;
	}
}
