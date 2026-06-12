using System;

namespace TheraEngine.PacketValues
{
	// Token: 0x02000071 RID: 113
	public struct Bits8
	{
		// Token: 0x17000088 RID: 136
		public bool this[int {12466}]
		{
			get
			{
				return ((int)this.Value & 1 << {12466}) != 0;
			}
			set
			{
				if (value)
				{
					this.Value |= (byte)(1 << {12467});
					return;
				}
				this.Value &= (byte)(~(byte)(1 << {12467}));
			}
		}

		// Token: 0x0400024F RID: 591
		public byte Value;
	}
}
