using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using UWContentPipelineExtensionRuntime.Tags;

namespace TheraEngine.Core
{
	// Token: 0x020000FC RID: 252
	public sealed class ContentManager
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x0002469E File Offset: 0x0002289E
		public Stream OpenStreamPublic(string {14743})
		{
			return this.{14756}.OpenStreamPublic({14743});
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x000246AC File Offset: 0x000228AC
		public IServiceProvider ContentService
		{
			get
			{
				return this.{14756}.ServiceProvider;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x000246B9 File Offset: 0x000228B9
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x000246C6 File Offset: 0x000228C6
		public string RootDir
		{
			get
			{
				return this.{14756}.RootDirectory;
			}
			set
			{
				this.{14756}.RootDirectory = value;
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000246D4 File Offset: 0x000228D4
		public ContentManager(HeapLoader {14745})
		{
			this.{14756} = {14745};
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0000C282 File Offset: 0x0000A482
		private void {14746}(string {14747})
		{
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000246FC File Offset: 0x000228FC
		public T TryLoadOrNull<T>(string {14748}) where T : class
		{
			if ({14748}.Contains("_lz"))
			{
				try
				{
					return this.Load<T>({14748});
				}
				catch (ContentLoadException)
				{
				}
				return default(T);
			}
			if (!File.Exists(Path.Combine(this.{14756}.RootDirectory, {14748}) + ".xnb"))
			{
				return default(T);
			}
			return this.Load<T>({14748});
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00024774 File Offset: 0x00022974
		public T LoadFromMemory<T>(string {14749}, MemoryStream {14750}, bool {14751} = true)
		{
			object obj = this.{14757};
			T result;
			lock (obj)
			{
				{14750}.Position = 0L;
				this.{14756}.stream = {14750};
				T {14754} = this.{14756}.Load<T>({14749});
				this.{14756}.stream = null;
				if ({14751})
				{
					{14750}.Dispose();
				}
				result = this.{14753}<T>({14754}, {14749});
			}
			return result;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000247F0 File Offset: 0x000229F0
		public T Load<T>(string {14752})
		{
			if (!{14752}.Contains("_lz"))
			{
				{14752} = Path.Combine(Environment.CurrentDirectory, this.{14756}.RootDirectory) + (({14752}[0] == '\\' || {14752}[0] == Path.DirectorySeparatorChar) ? {14752} : ("\\" + {14752}));
			}
			ContentManager.CurrentLoadingAsset = {14752};
			ContentManager.PrevLoadedAssetFullPath = Path.Combine(this.RootDir, {14752}, ".xnb");
			bool flag = false;
			object obj = this.{14757};
			T {14754};
			lock (obj)
			{
				if (flag)
				{
					object syncRender = Engine.Game.SyncRender;
					lock (syncRender)
					{
						{14754} = this.{14756}.Load<T>({14752});
						goto IL_D2;
					}
				}
				{14754} = this.{14756}.Load<T>({14752});
			}
			IL_D2:
			return this.{14753}<T>({14754}, {14752});
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000248F4 File Offset: 0x00022AF4
		private T {14753}<T>(T {14754}, string {14755})
		{
			if (typeof(T) == typeof(Model))
			{
				Model model = (Model)((object){14754});
				if (model.Tag != null)
				{
					UWModelTag uwmodelTag = model.Tag as UWModelTag;
					if (uwmodelTag != null)
					{
						model.Tag = new UWModelTagCompiled
						{
							AssetName = {14755},
							Tag = uwmodelTag
						};
					}
				}
				foreach (ModelMesh modelMesh in model.Meshes)
				{
					if (modelMesh.Effects.Count <= 0)
					{
						if (!modelMesh.MeshParts.Any((ModelMeshPart {14759}) => {14759}.Effect != null))
						{
							continue;
						}
					}
					throw new Exception("Model contains effects. Check ContentManager and Content pipeline");
				}
			}
			Texture2D texture2D = {14754} as Texture2D;
			return {14754};
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000249EC File Offset: 0x00022BEC
		public void ContentLoadingComplete()
		{
			this.{14756}.ContentLoadingComplete();
		}

		// Token: 0x0400050C RID: 1292
		public static volatile string CurrentLoadingAsset;

		// Token: 0x0400050D RID: 1293
		public static volatile string PrevLoadedAssetFullPath;

		// Token: 0x0400050E RID: 1294
		private static ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

		// Token: 0x0400050F RID: 1295
		private HeapLoader {14756};

		// Token: 0x04000510 RID: 1296
		private object {14757} = new object();

		// Token: 0x04000511 RID: 1297
		private string {14758} = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890_\\//.: ";
	}
}
