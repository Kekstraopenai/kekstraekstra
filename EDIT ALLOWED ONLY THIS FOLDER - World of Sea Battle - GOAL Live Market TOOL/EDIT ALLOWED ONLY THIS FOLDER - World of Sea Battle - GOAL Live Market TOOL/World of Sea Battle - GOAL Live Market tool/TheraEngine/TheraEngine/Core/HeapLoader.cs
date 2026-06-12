using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Xna.Framework.Content;

namespace TheraEngine.Core
{
	// Token: 0x020000FB RID: 251
	public class HeapLoader : ContentManager
	{
		// Token: 0x06000719 RID: 1817 RVA: 0x000244FC File Offset: 0x000226FC
		public HeapLoader(IServiceProvider {14736}) : base({14736})
		{
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00024510 File Offset: 0x00022710
		public HeapLoader(IServiceProvider {14737}, string {14738}) : base({14737}, {14738})
		{
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00024525 File Offset: 0x00022725
		public Stream OpenStreamPublic(string {14739})
		{
			return this.OpenStream({14739});
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00024530 File Offset: 0x00022730
		protected override Stream OpenStream(string {14740})
		{
			if ({14740}.Contains("_lz") && !string.IsNullOrEmpty(base.RootDirectory))
			{
				string[] array = {14740}.Split(new char[]
				{
					'\\',
					'/'
				}, StringSplitOptions.RemoveEmptyEntries);
				string text = array[0] + ".wosb";
				ZipFile zipFile;
				if (!this.{14741}.TryGetValue(text, out zipFile))
				{
					zipFile = new ZipFile(Path.Combine(base.RootDirectory, text), null);
					zipFile.Password = "wosb1010";
					this.{14741}.Add(text, zipFile);
				}
				int num = zipFile.FindEntry(Path.Combine(array.Skip(1).ToArray<string>()).Replace('\\', '/') + ".xnb", true);
				if (num != -1)
				{
					return zipFile.GetInputStream((long)num);
				}
				throw new FileNotFoundException({14740});
			}
			else
			{
				if ({14740}.Contains(".json"))
				{
					return File.OpenRead({14740});
				}
				return this.stream ?? base.OpenStream({14740});
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00024624 File Offset: 0x00022824
		public void ContentLoadingComplete()
		{
			foreach (KeyValuePair<string, ZipFile> keyValuePair in this.{14741})
			{
				((IDisposable)keyValuePair.Value).Dispose();
			}
			this.{14741}.Clear();
		}

		// Token: 0x04000509 RID: 1289
		private Dictionary<string, ZipFile> {14741} = new Dictionary<string, ZipFile>();

		// Token: 0x0400050A RID: 1290
		public static readonly string AppRoot = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

		// Token: 0x0400050B RID: 1291
		public MemoryStream stream;
	}
}
