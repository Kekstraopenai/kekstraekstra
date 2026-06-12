using System;

namespace TheraEngine
{
	// Token: 0x02000035 RID: 53
	public class ValueGraph64
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000921C File Offset: 0x0000741C
		public double AvgInt
		{
			get
			{
				return (double)((int)Math.Round(this.Avg));
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000922B File Offset: 0x0000742B
		public ValueGraph64(int {11840})
		{
			this.MeasurmentsHistorySize = {11840};
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00009248 File Offset: 0x00007448
		public void Push(double {11841})
		{
			object obj = this.{11843};
			lock (obj)
			{
				if (this.{11842} == 0)
				{
					this.Avg = {11841};
				}
				else
				{
					double num = Math.Max(1.0, 0.5 * (double)(this.MeasurmentsHistorySize - this.{11842} + 1)) / (double)this.MeasurmentsHistorySize;
					this.Avg += ({11841} - this.Avg) * num;
				}
				this.{11842}++;
				this.{11842} = Math.Min(this.{11842}, this.MeasurmentsHistorySize + 1);
			}
		}

		// Token: 0x040000F0 RID: 240
		public double Avg;

		// Token: 0x040000F1 RID: 241
		public int MeasurmentsHistorySize;

		// Token: 0x040000F2 RID: 242
		private int {11842};

		// Token: 0x040000F3 RID: 243
		private object {11843} = new object();
	}
}
