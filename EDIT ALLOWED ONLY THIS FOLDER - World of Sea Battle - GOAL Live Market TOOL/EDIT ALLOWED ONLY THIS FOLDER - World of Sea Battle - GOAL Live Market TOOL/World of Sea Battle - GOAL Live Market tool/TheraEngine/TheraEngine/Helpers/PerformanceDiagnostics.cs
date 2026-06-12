using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TheraEngine.Helpers
{
	// Token: 0x020000EE RID: 238
	public static class PerformanceDiagnostics
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x00021900 File Offset: 0x0001FB00
		public static Stopwatch GetSwAndStart(string {14436})
		{
			Stopwatch stopwatch;
			if (PerformanceDiagnostics.swPool.TryGetValue({14436}, out stopwatch))
			{
				return stopwatch;
			}
			stopwatch = new Stopwatch();
			PerformanceDiagnostics.swPool.Add({14436}, stopwatch);
			stopwatch.Start();
			return stopwatch;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00021938 File Offset: 0x0001FB38
		public static void CleanHistory(string {14437})
		{
			ValueGraph64 valueGraph;
			if (PerformanceDiagnostics.data.TryGetValue({14437}, out valueGraph))
			{
				PerformanceDiagnostics.data.Remove({14437});
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00021960 File Offset: 0x0001FB60
		public static void Append(string {14438}, Stopwatch {14439})
		{
			{14439}.Stop();
			PerformanceDiagnostics.Append({14438}, {14439}.Elapsed.TotalMilliseconds);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00021988 File Offset: 0x0001FB88
		public static void Append(string {14440}, double {14441})
		{
			ValueGraph64 valueGraph;
			if (!PerformanceDiagnostics.data.TryGetValue({14440}, out valueGraph))
			{
				valueGraph = new ValueGraph64(20);
				PerformanceDiagnostics.data[{14440}] = valueGraph;
			}
			valueGraph.Push({14441});
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000219BF File Offset: 0x0001FBBF
		public static IEnumerable<string> GetActiveCounters(int {14442} = 3)
		{
			PerformanceDiagnostics.<GetActiveCounters>d__6 <GetActiveCounters>d__ = new PerformanceDiagnostics.<GetActiveCounters>d__6(-2);
			<GetActiveCounters>d__.<>3__precision = {14442};
			return <GetActiveCounters>d__;
		}

		// Token: 0x040004C6 RID: 1222
		private static Dictionary<string, ValueGraph64> data = new Dictionary<string, ValueGraph64>();

		// Token: 0x040004C7 RID: 1223
		private static Dictionary<string, Stopwatch> swPool = new Dictionary<string, Stopwatch>();
	}
}
