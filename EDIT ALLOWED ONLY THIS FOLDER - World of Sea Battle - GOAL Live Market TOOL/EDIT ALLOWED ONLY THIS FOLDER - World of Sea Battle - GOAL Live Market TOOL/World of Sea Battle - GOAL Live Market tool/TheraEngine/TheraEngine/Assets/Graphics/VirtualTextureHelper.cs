using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x020001A1 RID: 417
	internal class VirtualTextureHelper
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x00034C08 File Offset: 0x00032E08
		public static void Run(VirtualTexture {15920}, in VirtualTextureSource {15921})
		{
			object obj = VirtualTextureHelper.syncRoot;
			lock (obj)
			{
				VirtualTextureHelper.QueuedFiles.Enqueue(new ValueTuple<VirtualTexture, VirtualTextureSource>({15920}, {15921}));
				if (!VirtualTextureHelper.isRun)
				{
					VirtualTextureHelper.StartThread();
				}
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00034C64 File Offset: 0x00032E64
		private static void StartThread()
		{
			VirtualTextureHelper.isRun = true;
			ThreadPool.QueueUserWorkItem(delegate(object {15924})
			{
				for (;;)
				{
					object obj = VirtualTextureHelper.syncRoot;
					VirtualTexture item;
					VirtualTextureSource item2;
					lock (obj)
					{
						if (VirtualTextureHelper.QueuedFiles.Count == 0)
						{
							VirtualTextureHelper.isRun = false;
							break;
						}
						ValueTuple<VirtualTexture, VirtualTextureSource> valueTuple = VirtualTextureHelper.QueuedFiles.Dequeue();
						item = valueTuple.Item1;
						item2 = valueTuple.Item2;
					}
					VirtualTextureHelper.LoadTexture(item2, item);
				}
			});
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00034C94 File Offset: 0x00032E94
		private static void LoadTexture(VirtualTextureSource {15922}, VirtualTexture {15923})
		{
			VirtualTextureHelper.<LoadTexture>d__7 <LoadTexture>d__;
			<LoadTexture>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<LoadTexture>d__.source = {15922};
			<LoadTexture>d__.tex = {15923};
			<LoadTexture>d__.<>1__state = -1;
			<LoadTexture>d__.<>t__builder.Start<VirtualTextureHelper.<LoadTexture>d__7>(ref <LoadTexture>d__);
		}

		// Token: 0x04000816 RID: 2070
		public static Queue<ValueTuple<VirtualTexture, MemoryStream, VirtualTextureSource>> Output = new Queue<ValueTuple<VirtualTexture, MemoryStream, VirtualTextureSource>>();

		// Token: 0x04000817 RID: 2071
		public static object OutputSyncRoot = new object();

		// Token: 0x04000818 RID: 2072
		private static Queue<ValueTuple<VirtualTexture, VirtualTextureSource>> QueuedFiles = new Queue<ValueTuple<VirtualTexture, VirtualTextureSource>>();

		// Token: 0x04000819 RID: 2073
		private static bool isRun;

		// Token: 0x0400081A RID: 2074
		private static object syncRoot = new object();
	}
}
