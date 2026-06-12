using System;

namespace TheraEngine
{
	// Token: 0x02000033 RID: 51
	public class ValueCounter
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00008F9C File Offset: 0x0000719C
		public float SurgesPercent
		{
			get
			{
				return (float)this.SurgesNum / (float)this.{11832} * 100f;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00008FB3 File Offset: 0x000071B3
		public int NumOfMeasurements
		{
			get
			{
				return this.{11832};
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008FBC File Offset: 0x000071BC
		public void Push(float {11831})
		{
			object obj = this.{11833};
			lock (obj)
			{
				this.Total += {11831};
				this.Min = Math.Min({11831}, this.Min);
				this.Max = Math.Max({11831}, this.Max);
				this.{11832}++;
				this.Avg = this.Total / (float)this.{11832};
				if ({11831} > this.Avg * 2f)
				{
					this.SurgesNum++;
				}
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00009068 File Offset: 0x00007268
		public float AvgAndClean()
		{
			object obj = this.{11833};
			float result;
			lock (obj)
			{
				float avg = this.Avg;
				this.Avg = 0f;
				this.Total = 0f;
				this.Min = float.MaxValue;
				this.Max = float.MinValue;
				this.{11832} = 0;
				this.SurgesNum = 0;
				result = avg;
			}
			return result;
		}

		// Token: 0x040000E4 RID: 228
		public float Avg;

		// Token: 0x040000E5 RID: 229
		public float Total;

		// Token: 0x040000E6 RID: 230
		public float Min = float.MaxValue;

		// Token: 0x040000E7 RID: 231
		public float Max = float.MinValue;

		// Token: 0x040000E8 RID: 232
		public int SurgesNum;

		// Token: 0x040000E9 RID: 233
		private int {11832};

		// Token: 0x040000EA RID: 234
		private object {11833} = new object();
	}
}
