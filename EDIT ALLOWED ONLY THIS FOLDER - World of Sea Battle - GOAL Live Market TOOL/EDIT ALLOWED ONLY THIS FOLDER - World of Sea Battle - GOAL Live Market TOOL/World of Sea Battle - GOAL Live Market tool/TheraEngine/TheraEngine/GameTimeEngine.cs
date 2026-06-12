using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace TheraEngine
{
	// Token: 0x02000018 RID: 24
	public class GameTimeEngine
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000033AB File Offset: 0x000015AB
		public float ElapsedUpdateReal
		{
			get
			{
				return (float)this.{11383};
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000033B4 File Offset: 0x000015B4
		public float ElapsedDrawReal
		{
			get
			{
				return (float)this.{11384};
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000033BD File Offset: 0x000015BD
		public float TimeUpdate
		{
			get
			{
				return (float)this.{11381};
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000033C6 File Offset: 0x000015C6
		public float TimeDraw
		{
			get
			{
				return (float)this.{11382};
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000033CF File Offset: 0x000015CF
		public double TotalGameTimeMs
		{
			get
			{
				return this.{11385};
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000033D8 File Offset: 0x000015D8
		public void BeginUpdate(double {11371}, Game {11372})
		{
			long ticks = DateTime.UtcNow.Ticks;
			if (this.{11385} == 0.0)
			{
				this.{11376} = ticks;
				this.{11377} = ticks;
				this.{11386}.Push(1.0);
			}
			this.LastTicksInternalDiff = new TimeSpan(Math.Abs(ticks - this.{11377})).TotalMilliseconds;
			if (this.LastTicksInternalDiff > 2000.0 || ticks < this.{11377})
			{
				this.{11376} = ticks;
				this.{11385} = 0.0;
				this.{11386}.Push(1.0);
			}
			this.{11377} = ticks;
			if (this.{11374}.IsRunning)
			{
				this.{11374}.Stop();
				this.{11383} = this.{11374}.Elapsed.TotalMilliseconds;
			}
			else
			{
				this.{11383} = {11371};
			}
			float {11836} = 1000f / (float)this.{11383};
			this.UpsCounter.Push({11836});
			this.{11374}.Reset();
			this.{11374}.Start();
			double totalMilliseconds = new TimeSpan(ticks - this.{11376}).TotalMilliseconds;
			this.LastTimeDiff = this.{11385} - totalMilliseconds;
			if (this.LastTimeDiff < -5000.0)
			{
				this.{11385} += -this.LastTimeDiff - 5000.0;
			}
			if (this.LastTimeDiff > 5000.0)
			{
				this.{11385} = totalMilliseconds;
				this.{11379}++;
				if (this.{11379} > 3)
				{
					throw new AccessViolationException("Speedhack probably is used with coef. " + this.{11386}.Avg.ToString());
				}
				this.{11378} = ticks;
			}
			if ((ticks - this.{11378}) / 600000000L >= 2L)
			{
				this.{11379} = 0;
			}
			double num = Math.Abs(this.LastTimeDiff);
			if (num > 500.0)
			{
				double num2 = (num - 500.0) / 1500.0;
				int num3 = Math.Sign(this.LastTimeDiff);
				double {11841} = Math.Max(0.5624999850988388, Math.Min(1.5, 1.0 - (double)num3 * num2));
				this.{11386}.Push({11841});
			}
			else
			{
				this.{11386}.Push(1.0);
			}
			{11371} *= this.{11386}.Avg;
			this.{11385} += {11371};
			this.ElapsedUpdate = (float){11371} * this.TimeSpeed;
			this.ElapsedUpdateSec = (float)({11371} / 1000.0 * (double)this.TimeSpeed);
			TimeSpan timeSpan = TimeSpan.FromMilliseconds(1000.0 / (double)this.TargetFPS);
			TimeSpan timeSpan2 = TimeSpan.FromMilliseconds(2000.0 / (double)this.TargetFPS);
			if (this.ClampUpdateFrequency)
			{
				if (((this.{11382} > timeSpan.TotalMilliseconds * 2.0 && this.{11381} > timeSpan.TotalMilliseconds * 0.25) || (this.{11382} > timeSpan.TotalMilliseconds * 1.5 && this.{11381} > timeSpan.TotalMilliseconds * 0.5)) && {11372}.IsFixedTimeStep != FixedTimeStepMode.Disable)
				{
					if (this.{11387} > 30)
					{
						this.{11380} = true;
					}
					else
					{
						this.{11387}++;
					}
				}
				else if (this.{11387} <= 0)
				{
					this.{11380} = false;
				}
				else
				{
					this.{11387}--;
				}
			}
			if (this.{11380} || !{11372}.IsActive)
			{
				if ({11372}.TargetElapsedTime != timeSpan2)
				{
					{11372}.TargetElapsedTime = timeSpan2;
					return;
				}
			}
			else if ({11372}.TargetElapsedTime != timeSpan)
			{
				{11372}.TargetElapsedTime = timeSpan;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000037C0 File Offset: 0x000019C0
		public void EndUpdate()
		{
			this.{11381} = this.{11374}.Elapsed.TotalMilliseconds;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000037E8 File Offset: 0x000019E8
		public void BeginDraw(double {11373})
		{
			if (this.{11375}.IsRunning)
			{
				this.{11375}.Stop();
				this.{11384} = this.{11375}.Elapsed.TotalMilliseconds;
			}
			else
			{
				this.{11384} = {11373};
			}
			float num = 1000f / (float)this.{11384};
			this.FpsCounter.Push(num);
			if (num < 30f)
			{
				this.FpsCounter.Push(num);
			}
			this.{11375}.Reset();
			this.{11375}.Start();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003874 File Offset: 0x00001A74
		public void EndDraw()
		{
			this.{11382} = this.{11375}.Elapsed.TotalMilliseconds;
		}

		// Token: 0x04000067 RID: 103
		public bool CorrectionEnabled = true;

		// Token: 0x04000068 RID: 104
		public double LastTimeDiff;

		// Token: 0x04000069 RID: 105
		public float TimeSpeed = 1f;

		// Token: 0x0400006A RID: 106
		public bool ClampUpdateFrequency = true;

		// Token: 0x0400006B RID: 107
		private const double correctionThresholdMs = 500.0;

		// Token: 0x0400006C RID: 108
		private const double correctionMaxMs = 2000.0;

		// Token: 0x0400006D RID: 109
		private const double maxTimeDiff = 5000.0;

		// Token: 0x0400006E RID: 110
		private const double maxSpeedhackSpeed = 1.6;

		// Token: 0x0400006F RID: 111
		public double LastTicksInternalDiff;

		// Token: 0x04000070 RID: 112
		private Stopwatch {11374} = new Stopwatch();

		// Token: 0x04000071 RID: 113
		private Stopwatch {11375} = new Stopwatch();

		// Token: 0x04000072 RID: 114
		private long {11376};

		// Token: 0x04000073 RID: 115
		private long {11377};

		// Token: 0x04000074 RID: 116
		private long {11378};

		// Token: 0x04000075 RID: 117
		private int {11379};

		// Token: 0x04000076 RID: 118
		private bool {11380};

		// Token: 0x04000077 RID: 119
		private double {11381};

		// Token: 0x04000078 RID: 120
		private double {11382};

		// Token: 0x04000079 RID: 121
		private double {11383};

		// Token: 0x0400007A RID: 122
		private double {11384};

		// Token: 0x0400007B RID: 123
		private double {11385};

		// Token: 0x0400007C RID: 124
		private ValueGraph64 {11386} = new ValueGraph64(20);

		// Token: 0x0400007D RID: 125
		private int {11387};

		// Token: 0x0400007E RID: 126
		public float ElapsedUpdate;

		// Token: 0x0400007F RID: 127
		public float ElapsedUpdateSec;

		// Token: 0x04000080 RID: 128
		public readonly ValueGraph FpsCounter = new ValueGraph(30);

		// Token: 0x04000081 RID: 129
		public readonly ValueGraph UpsCounter = new ValueGraph(30);

		// Token: 0x04000082 RID: 130
		public int TargetFPS = 60;
	}
}
