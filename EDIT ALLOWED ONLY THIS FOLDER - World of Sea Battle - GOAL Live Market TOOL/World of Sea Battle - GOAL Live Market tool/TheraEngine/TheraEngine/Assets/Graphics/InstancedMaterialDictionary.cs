using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Collections;
using TheraEngine.Graphics.Models;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x020001A7 RID: 423
	public class InstancedMaterialDictionary
	{
		// Token: 0x170001D4 RID: 468
		[Nullable(2)]
		public Material this[string {15961}]
		{
			[return: Nullable(2)]
			get
			{
				string text;
				if (this.{15977}.TryGetValue({15961}, out text))
				{
					{15961} = text;
				}
				Material material;
				if (this.{15975}.TryGetValue({15961}, out material))
				{
					material.Flags = 1;
					return material;
				}
				return null;
			}
		}

		// Token: 0x170001D5 RID: 469
		[Nullable(2)]
		public Material this[int {15962}]
		{
			[NullableContext(2)]
			get
			{
				Material result;
				if (this.{15976}.TryGetValue({15962}, out result))
				{
					return result;
				}
				throw new KeyNotFoundException("terrainId " + {15962}.ToString() + " is not found");
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0000C282 File Offset: 0x0000A482
		internal void RegisterUseTexture(string {15963}, string {15964})
		{
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000357F4 File Offset: 0x000339F4
		public InstancedMaterialDictionary(MaterialLoadInfo {15965}, InstancedMaterialDictionary.LoadMethod[] {15966}, TerrainLoadMethod[] {15967}, params ValueTuple<string, string>[] {15968})
		{
			this.{15975} = new Dictionary<string, Material>({15966}.Length * 10);
			this.{15974} = new Dictionary<string, Texture2D>({15966}.Length * 10);
			this.{15976} = new Dictionary<int, Material>({15967}.Length * 10);
			this.{15977} = new Dictionary<string, string>({15966}.Length * 10);
			foreach (ValueTuple<string, string> valueTuple in {15968})
			{
				this.{15977}.Add(valueTuple.Item1, valueTuple.Item2);
			}
			Texture2D fillerTexture = Engine.FillerTexture;
			Tlist<Exception> tlist = new Tlist<Exception>();
			string path = Path.Combine({15965}.Content.RootDir, {15965}.TexturesRootDir, {15965}.NextTexturesDir);
			string path2 = Path.Combine({15965}.TexturesRootDir, {15965}.NextTexturesDir);
			int j = 0;
			while (j < {15966}.Length)
			{
				InstancedMaterialDictionary.LoadMethod loadMethod = {15966}[j];
				if (loadMethod.ScanFolderMode)
				{
					using (IEnumerator<FileInfo> enumerator = new DirectoryInfo(Path.Combine(path, loadMethod.TexturePath)).EnumerateFiles().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							FileInfo fileInfo = enumerator.Current;
							Path.Combine(path2, loadMethod.TexturePath, fileInfo.Name);
							string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);
							VirtualTexture virtualTexture = new VirtualTexture(fillerTexture, fileNameWithoutExtension, new VirtualTextureSource(Path.Combine(path2, loadMethod.TexturePath, fileNameWithoutExtension), VirtualSourceType.FileSystem));
							this.{15975}.Add(virtualTexture.AssetName, new Material(virtualTexture, null, null, loadMethod.Material));
						}
						goto IL_314;
					}
					goto IL_190;
				}
				goto IL_190;
				IL_314:
				j++;
				continue;
				IL_190:
				for (int k = 0; k < j; k++)
				{
					if (loadMethod.TexturePath == {15966}[k].TexturePath)
					{
						Tlist<Exception> tlist2 = tlist;
						Exception ex = new InvalidOperationException("Texture " + loadMethod.TexturePath + " added twice");
						tlist2.Add(ex);
					}
				}
				if (!File.Exists(Path.Combine(path, loadMethod.TexturePath) + ".xnb"))
				{
					Tlist<Exception> tlist3 = tlist;
					Exception ex = new FileNotFoundException(loadMethod.TexturePath);
					tlist3.Add(ex);
					goto IL_314;
				}
				string fileName = Path.GetFileName(loadMethod.TexturePath);
				VirtualTexture virtualTexture2 = new VirtualTexture(fillerTexture, fileName, new VirtualTextureSource(Path.Combine(path2, loadMethod.TexturePath), VirtualSourceType.FileSystem));
				if ({15965}.LoadMaterials)
				{
					VirtualTexture {16006} = null;
					VirtualTexture {16007} = null;
					if (File.Exists(Path.Combine(path, loadMethod.PathMaterial1) + ".xnb"))
					{
						{16006} = new VirtualTexture(fillerTexture, fileName, new VirtualTextureSource(Path.Combine(path2, loadMethod.PathMaterial1), VirtualSourceType.FileSystem));
					}
					if (File.Exists(Path.Combine(path, loadMethod.PathMaterial2) + ".xnb"))
					{
						{16007} = new VirtualTexture(fillerTexture, fileName, new VirtualTextureSource(Path.Combine(path2, loadMethod.PathMaterial2), VirtualSourceType.FileSystem));
					}
					this.{15975}.Add(virtualTexture2.AssetName, new Material(virtualTexture2, {16006}, {16007}, loadMethod.Material));
					goto IL_314;
				}
				this.{15975}.Add(virtualTexture2.AssetName, new Material(virtualTexture2, null, null, loadMethod.Material));
				goto IL_314;
			}
			if (tlist.Size > 0)
			{
				new AggregateException(tlist.ToArray());
			}
			foreach (TerrainLoadMethod terrainLoadMethod in {15967})
			{
				this.{15976}.Add(terrainLoadMethod.terrainID, new Material(null, null, null, terrainLoadMethod.properties)
				{
					Terrain = new TerrainMaterialDescription(terrainLoadMethod.terrainID, terrainLoadMethod.mode, this[terrainLoadMethod.slot1].MaterialTx ?? this[terrainLoadMethod.slot2].MaterialTx, this[terrainLoadMethod.slot1].Albedo, this[terrainLoadMethod.slot2].Albedo, string.IsNullOrEmpty(terrainLoadMethod.slot3) ? null : this[terrainLoadMethod.slot3].Albedo)
				});
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00035C18 File Offset: 0x00033E18
		public List<string> MakeTexturesReport(Func<string, bool> {15969} = null)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, Material> keyValuePair in this.{15975})
			{
				if (keyValuePair.Value.Flags == 0)
				{
					if ({15969} == null || {15969}(keyValuePair.Key))
					{
						list.Add("Unused asset: " + keyValuePair.Key);
					}
					else
					{
						list.Add("Used only for design: " + keyValuePair.Key);
					}
				}
			}
			string[] array = new string[0];
			foreach (KeyValuePair<string, HashSet<string>> keyValuePair2 in this.{15978})
			{
				HashSet<string> value = keyValuePair2.Value;
				if (value.Count == 1)
				{
					foreach (string str in value)
					{
						if ({15969} == null || {15969}(keyValuePair2.Key))
						{
							list.Add("Used only for design and: " + keyValuePair2.Key + ", from " + str);
						}
						else
						{
							list.Add("Used only for: " + keyValuePair2.Key + ", from " + str);
						}
					}
				}
				if (array.Length != 0 && Array.IndexOf<string>(array, keyValuePair2.Key) != -1)
				{
					foreach (string str2 in value)
					{
						list.Add("Lookup query for " + keyValuePair2.Key + ": " + str2);
					}
				}
			}
			list.Add("");
			foreach (KeyValuePair<string, Material> keyValuePair3 in this.{15975})
			{
				HashSet<string> source;
				string text;
				if (!this.{15978}.TryGetValue(keyValuePair3.Key, out source))
				{
					text = ({15969}(keyValuePair3.Key) ? "None" : "Used in designs / code");
				}
				else
				{
					text = source.Aggregate((string {15985}, string {15986}) => {15985} + ", " + {15986});
				}
				string str3 = text;
				list.Add(Path.GetFileName(keyValuePair3.Key) + ": " + str3);
			}
			return list;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00035F10 File Offset: 0x00034110
		public void UnloadUnused(int {15970})
		{
			foreach (KeyValuePair<string, Material> keyValuePair in this.{15975})
			{
				VirtualTexture albedo = keyValuePair.Value.Albedo;
				int accessCounter = albedo.accessCounter;
				albedo.accessCounter = accessCounter + 1;
				if (accessCounter > {15970} && keyValuePair.Value.Albedo.AllowUnload)
				{
					keyValuePair.Value.Albedo.Unload();
				}
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00035FA0 File Offset: 0x000341A0
		public Material AddTexture(string {15971}, VirtualTexture {15972}, MaterialProperties {15973} = null)
		{
			Material material = new Material({15972}, null, null, {15973} ?? new Lambert());
			this.{15975}.Add({15971}, material);
			return material;
		}

		// Token: 0x0400083A RID: 2106
		public const string MaterialExtension = ".material";

		// Token: 0x0400083B RID: 2107
		public const string MaterialExtension2 = ".material2";

		// Token: 0x0400083C RID: 2108
		private Dictionary<string, Texture2D> {15974};

		// Token: 0x0400083D RID: 2109
		private Dictionary<string, Material> {15975};

		// Token: 0x0400083E RID: 2110
		[Nullable(new byte[]
		{
			0,
			2
		})]
		private Dictionary<int, Material> {15976};

		// Token: 0x0400083F RID: 2111
		private Dictionary<string, string> {15977};

		// Token: 0x04000840 RID: 2112
		private Dictionary<string, HashSet<string>> {15978} = new Dictionary<string, HashSet<string>>();

		// Token: 0x020001A8 RID: 424
		public struct LoadMethod
		{
			// Token: 0x06000AB6 RID: 2742 RVA: 0x00035FD0 File Offset: 0x000341D0
			public LoadMethod(string {15982}, MaterialProperties {15983}, bool {15984} = false)
			{
				this.TexturePath = {15982};
				this.Material = {15983};
				this.PathMaterial1 = this.TexturePath + ".material";
				this.PathMaterial2 = this.TexturePath + ".material2";
				this.ScanFolderMode = {15984};
			}

			// Token: 0x04000841 RID: 2113
			public string TexturePath;

			// Token: 0x04000842 RID: 2114
			public string PathMaterial1;

			// Token: 0x04000843 RID: 2115
			public string PathMaterial2;

			// Token: 0x04000844 RID: 2116
			public MaterialProperties Material;

			// Token: 0x04000845 RID: 2117
			public bool ScanFolderMode;
		}
	}
}
