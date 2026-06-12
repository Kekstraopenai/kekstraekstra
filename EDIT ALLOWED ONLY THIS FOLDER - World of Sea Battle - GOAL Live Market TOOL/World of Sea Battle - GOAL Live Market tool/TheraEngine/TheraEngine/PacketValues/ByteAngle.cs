using System;
using TheraEngine.Helpers;

namespace TheraEngine.PacketValues
{
	// Token: 0x02000077 RID: 119
	public struct ByteAngle
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x000109E7 File Offset: 0x0000EBE7
		public float Angle
		{
			get
			{
				return (float)this.RawValue / 255f * 6.2831855f - 3.1415927f;
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00010A02 File Offset: 0x0000EC02
		public ByteAngle(float {12485})
		{
			Geometry.AxisNormFast(ref {12485});
			{12485} = ({12485} + 3.1415927f) / 6.2831855f * 255f;
			this.RawValue = (byte)Math.Max(0f, Math.Min({12485}, 255f));
		}

		// Token: 0x04000259 RID: 601
		public byte RawValue;
	}
}
