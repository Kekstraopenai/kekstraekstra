using System;

namespace TheraEngine.PacketValues
{
	// Token: 0x02000072 RID: 114
	public struct Bits16
	{
		// Token: 0x17000089 RID: 137
		public bool this[int {12469}]
		{
			get
			{
				return ((int)this.Value & 1 << {12469}) != 0;
			}
			set
			{
				if (value)
				{
					this.Value |= (ushort)(1 << {12470});
					return;
				}
				this.Value &= (ushort)(~(ushort)(1 << {12470}));
			}
		}

		// Token: 0x04000250 RID: 592
		public ushort Value;
	}
}
