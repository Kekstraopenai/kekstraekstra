using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000067 RID: 103
	internal class {17053}
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0001B4AD File Offset: 0x000196AD
		static {17053}()
		{
			if (!Debugger.IsAttached)
			{
				{17053}.RunProtection();
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001B4BB File Offset: 0x000196BB
		public static void Exit()
		{
			{17053}.gameExited = true;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0001B4C4 File Offset: 0x000196C4
		public static void CheckProcess()
		{
			string processName = Process.GetCurrentProcess().ProcessName;
			Process[] processes = Process.GetProcesses();
			bool flag = false;
			foreach (Process process in processes)
			{
				if (process.ProcessName == processName)
				{
					if (flag)
					{
						try
						{
							process.Kill();
						}
						catch
						{
						}
					}
					flag = true;
				}
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001B52C File Offset: 0x0001972C
		public static ulong Sign(ulong {17054})
		{
			return {17053}.FinalSign({17054});
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0001B534 File Offset: 0x00019734
		private static void RunProtection()
		{
			{17053}.<>c__DisplayClass6_0 CS$<>8__locals1 = new {17053}.<>c__DisplayClass6_0();
			CS$<>8__locals1.threads = new List<Thread>();
			for (int i = 0; i < 2; i++)
			{
				CS$<>8__locals1.<RunProtection>g__RunThread|0();
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001B564 File Offset: 0x00019764
		private static void DestroyProcess()
		{
			Process.GetCurrentProcess().Close();
			Environment.Exit(0);
			throw new AccessViolationException();
		}

		// Token: 0x06000334 RID: 820
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetCurrentProcess();

		// Token: 0x06000335 RID: 821
		[DllImport("kernel32.dll")]
		private static extern bool IsDebuggerPresent();

		// Token: 0x06000336 RID: 822
		[DllImport("kernel32.dll")]
		private static extern bool CheckRemoteDebuggerPresent(IntPtr {17055}, ref bool {17056});

		// Token: 0x06000337 RID: 823
		[DllImport("ntdll.dll", SetLastError = true)]
		private static extern int NtQueryInformationProcess(IntPtr {17057}, int {17058}, ref int {17059}, int {17060}, IntPtr {17061});

		// Token: 0x06000338 RID: 824 RVA: 0x0001B57C File Offset: 0x0001977C
		public static bool IsBeingDebugged()
		{
			if ({17053}.IsDebuggerPresent())
			{
				return true;
			}
			bool flag = false;
			if ({17053}.CheckRemoteDebuggerPresent({17053}.GetCurrentProcess(), ref flag) && flag)
			{
				return true;
			}
			int num = 0;
			if ({17053}.NtQueryInformationProcess({17053}.GetCurrentProcess(), 7, ref num, 4, IntPtr.Zero) == 0 && num != 0)
			{
				return true;
			}
			string[] array = new string[]
			{
				"dnspy",
				"devenv",
				"x64dbg",
				"ollydbg",
				"ilspy",
				"dotpeek"
			};
			foreach (Process process in Process.GetProcesses())
			{
				try
				{
					string text = process.ProcessName.ToLower();
					foreach (string value in array)
					{
						if (text.Contains(value))
						{
							return true;
						}
					}
				}
				catch
				{
				}
			}
			return false;
		}

		// Token: 0x06000339 RID: 825
		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string {17062});

		// Token: 0x0600033A RID: 826
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetProcAddress(IntPtr {17063}, string {17064});

		// Token: 0x0600033B RID: 827
		[DllImport("Therashield3.dll", CallingConvention = 2, CharSet = CharSet.Ansi)]
		private static extern ulong FinalSign(ulong {17065});

		// Token: 0x0600033C RID: 828
		[DllImport("Therashield3.dll", CallingConvention = 2, CharSet = CharSet.Ansi)]
		private static extern int CheckThreads(int {17066});

		// Token: 0x04000286 RID: 646
		private static bool gameExited;
	}
}
