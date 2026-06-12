using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TheraEngine.Utilites.Threading
{
	// Token: 0x02000135 RID: 309
	public class HighResolutionTimer
	{
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060008CB RID: 2251 RVA: 0x0002ACF4 File Offset: 0x00028EF4
		// (remove) Token: 0x060008CC RID: 2252 RVA: 0x0002AD2C File Offset: 0x00028F2C
		public event Action Tick
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{14899};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{14899}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{14899};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{14899}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0002AD61 File Offset: 0x00028F61
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x0002AD69 File Offset: 0x00028F69
		public bool UseHighPriorityThread { get; set; }

		// Token: 0x060008CF RID: 2255 RVA: 0x0002AD72 File Offset: 0x00028F72
		public HighResolutionTimer() : this(1.0)
		{
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002AD83 File Offset: 0x00028F83
		public HighResolutionTimer(double {14896})
		{
			this.IntervalMs = {14896};
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002AD94 File Offset: 0x00028F94
		public void Start()
		{
			if (this.IsRunning)
			{
				return;
			}
			this.IsRunning = true;
			this.{14901} = new Thread(new ThreadStart(this.{14898}));
			if (this.UseHighPriorityThread)
			{
				this.{14901}.Priority = ThreadPriority.Highest;
			}
			this.{14901}.Start();
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002ADEB File Offset: 0x00028FEB
		public void Stop(bool {14897} = true)
		{
			this.IsRunning = false;
			if ({14897} && Thread.CurrentThread != this.{14901})
			{
				this.{14901}.Join();
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002AE14 File Offset: 0x00029014
		private void {14898}()
		{
			double num = 1000.0 / (double)Stopwatch.Frequency;
			IL_108:
			while (this.IsRunning)
			{
				long timestamp = Stopwatch.GetTimestamp();
				Action action = this.{14899};
				if (action != null)
				{
					action();
				}
				for (;;)
				{
					long num2 = Stopwatch.GetTimestamp() - timestamp;
					double num3 = this.IntervalMs - (double)num2 * num;
					if (num3 < 0.0)
					{
						goto IL_108;
					}
					if (num3 < 0.0001)
					{
						break;
					}
					if (num3 < 0.01)
					{
						Thread.SpinWait(5);
					}
					else if (num3 < 1.0)
					{
						Thread.SpinWait(100);
					}
					else if (num3 < 2.0)
					{
						Thread.SpinWait(500);
					}
					else if (num3 < 5.0)
					{
						Thread.Sleep(1);
					}
					else if (num3 < 10.0)
					{
						Thread.Sleep(2);
					}
					else if (num3 < 20.0)
					{
						Thread.Sleep(5);
					}
					else if (num3 < 50.0)
					{
						Thread.Sleep(10);
					}
				}
				Thread.SpinWait(1);
			}
		}

		// Token: 0x040005E2 RID: 1506
		public static readonly double Frequency = (double)Stopwatch.Frequency;

		// Token: 0x040005E3 RID: 1507
		public static bool IsHighResolution = Stopwatch.IsHighResolution;

		// Token: 0x040005E4 RID: 1508
		[CompilerGenerated]
		private Action {14899};

		// Token: 0x040005E5 RID: 1509
		public double IntervalMs;

		// Token: 0x040005E6 RID: 1510
		[CompilerGenerated]
		private bool {14900};

		// Token: 0x040005E7 RID: 1511
		public volatile bool IsRunning;

		// Token: 0x040005E8 RID: 1512
		private Thread {14901};
	}
}
