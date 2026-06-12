using System;
using System.Diagnostics;

namespace TheraEngine
{
	// Token: 0x0200002E RID: 46
	public class StopwatchGraph
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00008BA4 File Offset: 0x00006DA4
		public float AvgMs
		{
			get
			{
				return this.{11807}.Avg;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00008BB1 File Offset: 0x00006DB1
		public float MinMs
		{
			get
			{
				return this.{11807}.Min;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00008BBE File Offset: 0x00006DBE
		public float MaxMs
		{
			get
			{
				return this.{11807}.Max;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00008BCB File Offset: 0x00006DCB
		public float TotalMs
		{
			get
			{
				return this.{11807}.Total;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public float SurgesPercent
		{
			get
			{
				return this.{11807}.SurgesPercent;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00008BE5 File Offset: 0x00006DE5
		public StopwatchGraph()
		{
			this.{11806} = new Stopwatch();
			this.{11807} = new ValueCounter();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008C03 File Offset: 0x00006E03
		public void Begin()
		{
			this.{11806}.Start();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008C10 File Offset: 0x00006E10
		public void End()
		{
			this.{11806}.Stop();
			this.{11807}.Push((float)this.{11806}.Elapsed.TotalMilliseconds);
			this.{11806}.Reset();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008C52 File Offset: 0x00006E52
		public void Clean()
		{
			this.{11807}.AvgAndClean();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008C60 File Offset: 0x00006E60
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Avg: ",
				Math.Round((double)this.AvgMs, 2).ToString(),
				", Max: ",
				Math.Round((double)this.MaxMs, 2).ToString(),
				", Min: ",
				Math.Round((double)this.MinMs, 2).ToString(),
				", Surges: ",
				Math.Round((double)this.SurgesPercent, 1).ToString(),
				"%"
			});
		}

		// Token: 0x040000D8 RID: 216
		private Stopwatch {11806};

		// Token: 0x040000D9 RID: 217
		private ValueCounter {11807};
	}
}
