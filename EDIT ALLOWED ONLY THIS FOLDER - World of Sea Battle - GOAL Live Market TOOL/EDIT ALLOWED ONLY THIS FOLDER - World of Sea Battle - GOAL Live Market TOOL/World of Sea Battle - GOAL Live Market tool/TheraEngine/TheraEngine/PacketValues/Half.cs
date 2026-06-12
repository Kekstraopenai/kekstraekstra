using System;
using System.Runtime.InteropServices;

namespace TheraEngine.PacketValues
{
	// Token: 0x02000078 RID: 120
	[StructLayout(LayoutKind.Explicit, Size = 2)]
	public struct Half
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00010A3C File Offset: 0x0000EC3C
		public short PackedValue
		{
			get
			{
				return this.{12488};
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00010A44 File Offset: 0x0000EC44
		public byte[] Bytes
		{
			get
			{
				return BitConverter.GetBytes(this.{12488});
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00010A51 File Offset: 0x0000EC51
		public static byte PackUNorm8(float {12486})
		{
			if ({12486} <= 0.0019607844f)
			{
				return 0;
			}
			if ({12486} >= 0.9980392f)
			{
				return byte.MaxValue;
			}
			return (byte)({12486} * 255f);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00010A73 File Offset: 0x0000EC73
		public static float UnpackUNorm8(byte {12487})
		{
			if ({12487} == 0)
			{
				return 0f;
			}
			if ({12487} == 255)
			{
				return 1f;
			}
			return (float){12487} * 0.003921569f;
		}

		// Token: 0x0400025A RID: 602
		public static readonly float MaxValue = 65536f;

		// Token: 0x0400025B RID: 603
		public static readonly float MinValue = 0.65535f;

		// Token: 0x0400025C RID: 604
		[FieldOffset(0)]
		private short {12488};
	}
}
