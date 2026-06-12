using System;
using System.Numerics;

namespace TheraEngine.PacketValues
{
	// Token: 0x02000074 RID: 116
	public struct Bits64
	{
		// Token: 0x1700008B RID: 139
		public bool this[int {12475}]
		{
			get
			{
				return (this.Value & 1L << {12475}) != 0L;
			}
			set
			{
				if (value)
				{
					this.Value |= 1L << {12476};
					return;
				}
				this.Value &= ~(1L << {12476});
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0001073C File Offset: 0x0000E93C
		public int Count
		{
			get
			{
				return BitOperations.PopCount((ulong)this.Value);
			}
		}

		// Token: 0x04000252 RID: 594
		public long Value;
	}
}
