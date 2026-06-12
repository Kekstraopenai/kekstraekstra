using System;

namespace TheraEngine.PacketValues
{
	// Token: 0x02000073 RID: 115
	public struct Bits32
	{
		// Token: 0x1700008A RID: 138
		public bool this[int {12472}]
		{
			get
			{
				return (this.Value & 1 << {12472}) != 0;
			}
			set
			{
				if (value)
				{
					this.Value |= 1 << {12473};
					return;
				}
				this.Value &= ~(1 << {12473});
			}
		}

		// Token: 0x04000251 RID: 593
		public int Value;
	}
}
