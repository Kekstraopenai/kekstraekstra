using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Core;
using UWContentPipelineExtensionRuntime;
using UWContentPipelineExtensionRuntime.Tags;

namespace TheraEngine.Graphics.Models
{
	// Token: 0x02000167 RID: 359
	public class UWModel : DisposableObject
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x0002E6E4 File Offset: 0x0002C8E4
		public static UWModel CreateAll(InstancedMaterialDictionary {15187}, Model {15188})
		{
			int count = {15188}.Meshes.Count;
			int[] array = new int[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = i;
			}
			return new UWModel({15187}, {15188}, array);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0002E71C File Offset: 0x0002C91C
		public static UWModel TryCreate(InstancedMaterialDictionary {15189}, Model {15190}, string {15191}, int {15192} = 1)
		{
			int num = {15190}.Meshes.Count((ModelMesh {15236}) => {15236}.Name.Contains({15191}));
			if (num > {15192})
			{
				throw new InvalidOperationException({15191});
			}
			if (num > 0)
			{
				return UWModel.Create({15189}, {15190}, {15191}, false);
			}
			return null;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002E770 File Offset: 0x0002C970
		public static UWModel Create(InstancedMaterialDictionary {15193}, Model {15194}, string {15195}, bool {15196} = false)
		{
			int count = {15194}.Meshes.Count;
			Tlist<int> tlist = new Tlist<int>(count);
			for (int i = 0; i < count; i++)
			{
				if ({15194}.Meshes[i].Name.Contains({15195}))
				{
					tlist.Add(i);
				}
			}
			if ({15196} && tlist.Size == 0)
			{
				return new UWModel();
			}
			return new UWModel({15193}, {15194}, tlist.ToArray());
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002E7DC File Offset: 0x0002C9DC
		public static UWModel CreateWithout(InstancedMaterialDictionary {15197}, Model {15198}, params string[] {15199})
		{
			int count = {15198}.Meshes.Count;
			Tlist<int> tlist = new Tlist<int>(count);
			for (int i = 0; i < count; i++)
			{
				int num = 0;
				foreach (string value in {15199})
				{
					if ({15198}.Meshes[i].Name.Contains(value))
					{
						num++;
					}
				}
				if (num == 0)
				{
					tlist.Add(i);
				}
			}
			return UWModel.CreateRange({15197}, {15198}, tlist.ToArray());
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0002E860 File Offset: 0x0002CA60
		public static Tlist<UWModel> Separate(InstancedMaterialDictionary {15200}, Model {15201}, string {15202})
		{
			int count = {15201}.Meshes.Count;
			Tlist<int> tlist = new Tlist<int>(count);
			for (int i = 0; i < count; i++)
			{
				if (string.IsNullOrEmpty({15202}) || {15201}.Meshes[i].Name.Contains({15202}))
				{
					tlist.Add(i);
				}
			}
			Tlist<UWModel> tlist2 = new Tlist<UWModel>();
			for (int j = 0; j < tlist.Size; j++)
			{
				Tlist<UWModel> tlist3 = tlist2;
				UWModel uwmodel = UWModel.CreateAlone({15200}, {15201}, tlist.Array[j]);
				tlist3.Add(uwmodel);
			}
			return tlist2;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002E8EB File Offset: 0x0002CAEB
		public static Tlist<UWModel> Separate(InstancedMaterialDictionary {15203}, Model {15204})
		{
			return UWModel.Separate({15203}, {15204}, "");
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002E8F9 File Offset: 0x0002CAF9
		public static UWModel CreateAlone(InstancedMaterialDictionary {15205}, Model {15206}, int {15207})
		{
			return new UWModel({15205}, {15206}, new int[]
			{
				{15207}
			});
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002E90C File Offset: 0x0002CB0C
		public static UWModel[] CreateStartOf(InstancedMaterialDictionary {15208}, Model {15209}, int {15210})
		{
			new int[{15209}.Meshes.Count - {15210}];
			UWModel[] array = new UWModel[{15209}.Meshes.Count - {15210}];
			for (int i = 0; i < {15209}.Meshes.Count - {15210}; i++)
			{
				array[i] = new UWModel({15208}, {15209}, new int[]
				{
					{15210} + i
				});
			}
			return array;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0002E96E File Offset: 0x0002CB6E
		public static UWModel CreateRange(InstancedMaterialDictionary {15211}, Model {15212}, params int[] {15213})
		{
			return new UWModel({15211}, {15212}, {15213});
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0002E978 File Offset: 0x0002CB78
		public static UWModel CreateIdentity()
		{
			return new UWModel();
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0002E97F File Offset: 0x0002CB7F
		public static ModelHardpoint[] ExtractAllHardpoints(Model {15214})
		{
			return UWModel.GetModelTag({15214}).Tag.Hardpoints;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0002E994 File Offset: 0x0002CB94
		private static UWModelTagCompiled GetModelTag(Model {15215})
		{
			if ({15215}.Tag == null || !({15215}.Tag is UWModelTagCompiled))
			{
				try
				{
					File.Delete(ContentManager.PrevLoadedAssetFullPath);
				}
				catch
				{
				}
				throw new InvalidOperationException("Decompression error: error init UWEngineTag");
			}
			return {15215}.Tag as UWModelTagCompiled;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0002E9F0 File Offset: 0x0002CBF0
		private UWModelMeshPartTag {15216}(ModelMeshPart {15217})
		{
			if ({15217}.Tag == null || !({15217}.Tag is UWModelMeshPartTag))
			{
				throw new InvalidOperationException("Decompression error: error init UWEngineTag");
			}
			return {15217}.Tag as UWModelMeshPartTag;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0002EA20 File Offset: 0x0002CC20
		private Material {15218}(InstancedMaterialDictionary {15219}, UWModelTag {15220}, UWModelMeshPartTag {15221}, string {15222})
		{
			if ({15219} != null && {15220}.IsSupportedMaterials)
			{
				if ({15221}.TextureInfo == null || string.IsNullOrEmpty({15221}.TextureInfo.AssetName))
				{
					return null;
				}
				string assetName = {15221}.TextureInfo.AssetName;
				if (assetName.StartsWith("TERRAIN"))
				{
					if (assetName.Length == "TERRAIN".Length)
					{
						throw new InvalidOperationException("Тег Terrain-based должен иметь вид 'TERRAIN{0}'");
					}
					int {15962};
					if (!int.TryParse(assetName.Substring("TERRAIN".Length, assetName.Length - "TERRAIN".Length), out {15962}))
					{
						throw new InvalidOperationException("Тег Terrain-based должен иметь вид 'TERRAIN{0}'");
					}
					return {15219}[{15962}];
				}
				else
				{
					Material material = {15219}[{15221}.TextureInfo.AssetName];
					if (material != null)
					{
						{15219}.RegisterUseTexture({15221}.TextureInfo.AssetName, Path.GetFileName({15222}));
						return material;
					}
				}
			}
			return null;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002EB00 File Offset: 0x0002CD00
		public UWModel()
		{
			this.MeshParts = new MeshPartData[0];
			this.PrimitiveCount = 0;
			this.VertexCount = 0;
			this.{15232} = new DeviceStreamContext[0];
			this.{15233} = new VertexBufferBinding[0];
			this.Drawcalls = new ModelPartShadercall[0];
			this.OnInit();
			this.ModelName = "Procedural generated";
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0002EB64 File Offset: 0x0002CD64
		public UWModel(VertexBuffer {15223}, IndexBuffer {15224}, Material {15225}, ModelGeometryType {15226})
		{
			this.MeshParts = new MeshPartData[]
			{
				new MeshPartData({15223}, {15224})
			};
			this.PrimitiveCount = this.MeshParts[0].PrimitiveCount;
			this.VertexCount = this.MeshParts[0].verticesCount;
			this.{15232} = new DeviceStreamContext[]
			{
				new DeviceStreamContext
				{
					IndexBuffer = {15224},
					VertexBuffer = {15223},
					Sets = new Tlist<MeshPartData>().AddFirst(this.MeshParts[0])
				}
			};
			this.{15233} = new VertexBufferBinding[]
			{
				new VertexBufferBinding({15223})
			};
			this.Drawcalls = new ModelPartShadercall[]
			{
				new ModelPartShadercall({15225}, new Tlist<MeshPartData>().AddFirst(this.MeshParts[0]), {15226})
			};
			this.OnInit();
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002EC41 File Offset: 0x0002CE41
		private static bool IsTreeInstance(ModelHardpoint {15227})
		{
			return {15227}.HardpointID == WorldOfSeaBattleHardpointID.HPTree || {15227}.HardpointID == WorldOfSeaBattleHardpointID.HPHFlora || {15227}.HardpointID == WorldOfSeaBattleHardpointID.HPSFlora;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0002EC64 File Offset: 0x0002CE64
		protected UWModel(InstancedMaterialDictionary {15228}, Model {15229}, params int[] {15230})
		{
			if ({15230}.Length == 0)
			{
				throw new ArgumentException("Probably model is empty or has no required meshes");
			}
			UWModelTagCompiled modelTag = UWModel.GetModelTag({15229});
			this.SkinningDataOrNull = modelTag.Tag.SkinningAnimation;
			this.ModelName = modelTag.AssetName;
			int num = {15230}.Length;
			int num2 = Math.Min({15230}.Length, {15229}.Meshes.Count);
			if (num2 == 0)
			{
				throw new ArgumentException("UWModel " + this.ModelName + " created empty: there's no meshes found or key is wrong");
			}
			if ({15230}[0] == 1 && {15229}.Meshes.Count == 1)
			{
				num2 = 0;
			}
			int num3 = 0;
			for (int i = 0; i < num2; i++)
			{
				int index = {15230}[i];
				num3 += {15229}.Meshes[index].MeshParts.Count;
				if ({15229}.Meshes[index].MeshParts.Count == 0)
				{
					throw new ArgumentException("UWModel " + this.ModelName + " has empty mesh " + {15229}.Meshes[index].Name);
				}
			}
			this.ExternalHardpoints = (from {15234} in modelTag.Tag.Hardpoints
			where !UWModel.IsTreeInstance({15234})
			select {15234}).ToArray<ModelHardpoint>();
			this.ExternalHardpointsFlora = (from {15235} in modelTag.Tag.Hardpoints
			where UWModel.IsTreeInstance({15235})
			select {15235}).ToArray<ModelHardpoint>();
			Tlist<MeshPartData> tlist = new Tlist<MeshPartData>(num3);
			int num4 = 0;
			BoundingSphere boundingSphere = default(BoundingSphere);
			UWModel.drawcalls.Clear();
			for (int j = 0; j < num2; j++)
			{
				ModelMesh modelMesh = {15229}.Meshes[{15230}[j]];
				this.MeshName = modelMesh.Name;
				if (boundingSphere.Radius == 0f)
				{
					boundingSphere = modelMesh.BoundingSphere;
				}
				else
				{
					boundingSphere = BoundingSphere.CreateMerged(this.CommonSphere, modelMesh.BoundingSphere);
				}
				int count = modelMesh.MeshParts.Count;
				for (int k = 0; k < count; k++)
				{
					ModelMeshPart modelMeshPart = modelMesh.MeshParts[k];
					UWModelMeshPartTag uwmodelMeshPartTag = this.{15216}(modelMeshPart);
					MeshPartData meshPartData = new MeshPartData(modelMeshPart, uwmodelMeshPartTag);
					if (this.CommonSphere.Radius == 0f)
					{
						this.CommonSphere = uwmodelMeshPartTag.LocalBoundingSphere;
					}
					else
					{
						this.CommonSphere = BoundingSphere.CreateMerged(this.CommonSphere, uwmodelMeshPartTag.LocalBoundingSphere);
					}
					tlist.Add(meshPartData);
					this.PrimitiveCount += meshPartData.PrimitiveCount;
					this.VertexCount += meshPartData.verticesCount;
					if ({15228} != null && uwmodelMeshPartTag.TextureInfo != null)
					{
						string assetName = uwmodelMeshPartTag.TextureInfo.AssetName;
						if (UWModel.drawcalls.ContainsKey(assetName))
						{
							UWModel.drawcalls[assetName].Item2.Add(meshPartData);
						}
						else
						{
							Material material = this.{15218}({15228}, modelTag.Tag, uwmodelMeshPartTag, this.ModelName);
							Tlist<MeshPartData> tlist2 = new Tlist<MeshPartData>(10);
							tlist2.Add(meshPartData);
							UWModel.drawcalls.Add(assetName, new ValueTuple<Material, Tlist<MeshPartData>>(material ?? Material.Empty, tlist2));
							num4++;
						}
					}
				}
			}
			if (Math.Abs(boundingSphere.Radius - this.CommonSphere.Radius) > this.CommonSphere.Radius * 0.4f && this.CommonSphere.Radius != 0f)
			{
				throw new Exception();
			}
			this.Drawcalls = new ModelPartShadercall[num4];
			int num5 = 0;
			this.HaveTransparentMaterials = false;
			foreach (KeyValuePair<string, ValueTuple<Material, Tlist<MeshPartData>>> keyValuePair in UWModel.drawcalls)
			{
				this.HaveTransparentMaterials = (this.HaveTransparentMaterials || keyValuePair.Value.Item1.Properties.RasterizerOptions > MaterialRasterizeOptions.DoublesidedDefault);
				this.HaveDisabledGbufferMaterials = (this.HaveDisabledGbufferMaterials || keyValuePair.Value.Item1.Properties.RasterizerOptions == MaterialRasterizeOptions.AlphaMaterialWithoutShadow);
				this.Drawcalls[num5++] = new ModelPartShadercall(keyValuePair.Value.Item1, keyValuePair.Value.Item2, modelTag.Tag.AllowColor ? ModelGeometryType.VertexPositionColorTexture : ModelGeometryType.VertexPositionTexture);
			}
			Array.Sort<ModelPartShadercall>(this.Drawcalls, new MaterialRasterizerSort());
			this.MeshParts = tlist.ToArray();
			this.{15231}();
			this.OnInit();
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0002F104 File Offset: 0x0002D304
		private void {15231}()
		{
			Dictionary<VertexBuffer, Tlist<MeshPartData>> dictionary = new Dictionary<VertexBuffer, Tlist<MeshPartData>>();
			for (int i = 0; i < this.MeshParts.Length; i++)
			{
				MeshPartData meshPartData = this.MeshParts[i];
				Tlist<MeshPartData> tlist;
				if (dictionary.TryGetValue(meshPartData.XnaVertexBuffer, out tlist))
				{
					tlist.Add(meshPartData);
				}
				else
				{
					dictionary.Add(meshPartData.XnaVertexBuffer, new Tlist<MeshPartData>(new MeshPartData[]
					{
						meshPartData
					}));
				}
			}
			this.{15232} = new DeviceStreamContext[dictionary.Count];
			this.{15233} = new VertexBufferBinding[dictionary.Count];
			int num = 0;
			foreach (KeyValuePair<VertexBuffer, Tlist<MeshPartData>> keyValuePair in dictionary)
			{
				this.{15232}[num] = new DeviceStreamContext
				{
					IndexBuffer = keyValuePair.Value.Array[0].XnaIndexBuffer,
					VertexBuffer = keyValuePair.Key,
					Sets = keyValuePair.Value
				};
				this.{15233}[num] = new VertexBufferBinding(keyValuePair.Key);
				num++;
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0000C282 File Offset: 0x0000A482
		protected virtual void OnInit()
		{
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0002F228 File Offset: 0x0002D428
		public void OptimizedRenderAllBuffers()
		{
			Engine.GS.Render3DCompressedMesh(this.{15232}, this.{15233}, true);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0002F244 File Offset: 0x0002D444
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"TheraEngine Model:  Drawcalls by meterial: ",
				this.Drawcalls.Length.ToString(),
				" Drawcalls by vertex buffer: ",
				this.{15233}.Length.ToString(),
				" from parts count: ",
				this.MeshParts.Length.ToString()
			});
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002F2B0 File Offset: 0x0002D4B0
		public void DisposeWithSourceData()
		{
			if (base.IsDisposed)
			{
				return;
			}
			MeshPartData[] meshParts = this.MeshParts;
			for (int i = 0; i < meshParts.Length; i++)
			{
				meshParts[i].DisposeWithSourceData();
			}
			this.Dispose();
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0002F2E9 File Offset: 0x0002D4E9
		public override void Dispose()
		{
			if (this.MeshParts != null)
			{
				Array.Clear(this.MeshParts, 0, this.MeshParts.Length);
			}
			this.{15232} = null;
			this.{15233} = null;
			base.Dispose();
		}

		// Token: 0x0400068C RID: 1676
		private static Dictionary<string, ValueTuple<Material, Tlist<MeshPartData>>> drawcalls = new Dictionary<string, ValueTuple<Material, Tlist<MeshPartData>>>(1000);

		// Token: 0x0400068D RID: 1677
		public readonly string ModelName;

		// Token: 0x0400068E RID: 1678
		public readonly string MeshName;

		// Token: 0x0400068F RID: 1679
		public readonly MeshPartData[] MeshParts;

		// Token: 0x04000690 RID: 1680
		public readonly ModelPartShadercall[] Drawcalls;

		// Token: 0x04000691 RID: 1681
		public readonly bool HaveTransparentMaterials;

		// Token: 0x04000692 RID: 1682
		public readonly bool HaveDisabledGbufferMaterials;

		// Token: 0x04000693 RID: 1683
		public readonly ModelHardpoint[] ExternalHardpoints;

		// Token: 0x04000694 RID: 1684
		public readonly ModelHardpoint[] ExternalHardpointsFlora;

		// Token: 0x04000695 RID: 1685
		public readonly BoundingSphere CommonSphere;

		// Token: 0x04000696 RID: 1686
		public readonly int PrimitiveCount;

		// Token: 0x04000697 RID: 1687
		public readonly int VertexCount;

		// Token: 0x04000698 RID: 1688
		private DeviceStreamContext[] {15232};

		// Token: 0x04000699 RID: 1689
		private VertexBufferBinding[] {15233};

		// Token: 0x0400069A RID: 1690
		public readonly SkinningData SkinningDataOrNull;
	}
}
