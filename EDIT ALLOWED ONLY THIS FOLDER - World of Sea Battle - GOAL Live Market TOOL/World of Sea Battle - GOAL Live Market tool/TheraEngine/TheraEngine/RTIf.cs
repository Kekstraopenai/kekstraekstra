using System;

namespace TheraEngine
{
	// Token: 0x02000016 RID: 22
	public struct RTIf
	{
		// Token: 0x06000071 RID: 113 RVA: 0x0000327C File Offset: 0x0000147C
		public unsafe RTIf(float {11360})
		{
			this.{11363} = new RTI(*(int*)(&{11360}));
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003290 File Offset: 0x00001490
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000032AD File Offset: 0x000014AD
		public unsafe float Value
		{
			get
			{
				int value = this.{11363}.Value;
				return *(float*)(&value);
			}
			set
			{
				this.{11363}.Value = *(int*)(&value);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000032C0 File Offset: 0x000014C0
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000032DB File Offset: 0x000014DB
		public override int GetHashCode()
		{
			return this.{11363}.GetHashCode();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000032EE File Offset: 0x000014EE
		public static implicit operator RTIf(float {11362})
		{
			return new RTIf({11362});
		}

		// Token: 0x04000064 RID: 100
		private RTI {11363};
	}
}
