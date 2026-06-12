using System;
using Microsoft.Xna.Framework;

namespace TheraEngine
{
	// Token: 0x02000034 RID: 52
	public class ValueGraph
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000910D File Offset: 0x0000730D
		public int AvgInt
		{
			get
			{
				return (int)Math.Round((double)this.Avg);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000911C File Offset: 0x0000731C
		public object SyncRoot
		{
			get
			{
				return this.{11838};
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009124 File Offset: 0x00007324
		public ValueGraph(int {11835})
		{
			this.MeasurmentsHistorySize = {11835};
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00009140 File Offset: 0x00007340
		public void Push(float {11836})
		{
			object obj = this.{11838};
			lock (obj)
			{
				if (this.{11837} == 0)
				{
					this.Avg = {11836};
					this.MaxSmooth = {11836};
				}
				else
				{
					float amount = Math.Max(1f, 0.5f * (float)(this.MeasurmentsHistorySize - this.{11837} + 1)) / (float)this.MeasurmentsHistorySize;
					this.Avg = MathHelper.Lerp(this.Avg, {11836}, amount);
					this.MaxSmooth = MathHelper.Lerp(this.MaxSmooth, {11836}, amount);
					this.MaxSmooth = Math.Max(this.MaxSmooth, {11836});
				}
				this.{11837}++;
				this.{11837} = Math.Min(this.{11837}, this.MeasurmentsHistorySize + 1);
			}
		}

		// Token: 0x040000EB RID: 235
		public float Avg;

		// Token: 0x040000EC RID: 236
		public float MaxSmooth;

		// Token: 0x040000ED RID: 237
		public int MeasurmentsHistorySize;

		// Token: 0x040000EE RID: 238
		private int {11837};

		// Token: 0x040000EF RID: 239
		private object {11838} = new object();
	}
}
