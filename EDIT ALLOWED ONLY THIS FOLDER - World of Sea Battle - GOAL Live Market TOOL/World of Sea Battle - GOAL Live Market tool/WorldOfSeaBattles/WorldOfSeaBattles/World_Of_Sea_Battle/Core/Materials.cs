using System;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Graphics;
using TheraEngine.Core;
using TheraEngine.Graphics.Models;
using WorldOfSeaBattles;

namespace World_Of_Sea_Battle.Core
{
	// Token: 0x020004E2 RID: 1250
	internal sealed class Materials
	{
		// Token: 0x06001BC6 RID: 7110 RVA: 0x000FB02C File Offset: 0x000F922C
		public static void InitializeTexturesDatabase(ContentManager {25136})
		{
			Materials.materialsLoadInfo = new MaterialLoadInfo({25136}, PathContent.model_textures, true)
			{
				NextTexturesDir = "lod0",
				DetailMapsDirName = "DetailMaps"
			};
			Materials.TexturesDatabase = new InstancedMaterialDictionary(Materials.materialsLoadInfo, Materials.modelTexturesNames, Materials.Terrains, Array.Empty<ValueTuple<string, string>>());
			Materials.sailDestructTextures = new VirtualTexture[16];
			for (int i = 0; i < Materials.sailDestructTextures.Length; i++)
			{
				Materials.sailDestructTextures[i] = Materials.TexturesDatabase["sdf_" + ((i + 1 < 10) ? ("0" + (i + 1).ToString()) : (i + 1).ToString())].Albedo;
			}
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x000FB0E8 File Offset: 0x000F92E8
		public static Texture2D GetSailDestructEffect(int {25137})
		{
			int num = Math.Max(0, 15 - {25137} / 16);
			return Materials.sailDestructTextures[num].Tex;
		}

		// Token: 0x04001A76 RID: 6774
		private static readonly InstancedMaterialDictionary.LoadMethod[] modelTexturesNames = new InstancedMaterialDictionary.LoadMethod[]
		{
			new InstancedMaterialDictionary.LoadMethod("Flora_1", new Flora(MaterialRasterizeOptions.DoublesidedDefault, true), false),
			new InstancedMaterialDictionary.LoadMethod("Flora_2", new Flora(MaterialRasterizeOptions.DoublesidedDefault, true), false),
			new InstancedMaterialDictionary.LoadMethod("tree_trunk", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("moss_2_flora", new Flora(MaterialRasterizeOptions.DoublesidedDefault, true), false),
			new InstancedMaterialDictionary.LoadMethod("Decals_castle", new Flora(MaterialRasterizeOptions.DoublesidedDefault, false), false),
			new InstancedMaterialDictionary.LoadMethod("moss_2", new Flora(MaterialRasterizeOptions.DoublesidedDefault, false), false),
			new InstancedMaterialDictionary.LoadMethod("modelEffects", new Lambert(), false),
			new InstancedMaterialDictionary.LoadMethod("Trees_Unic", new Flora(MaterialRasterizeOptions.DoublesidedDefault, true), false),
			new InstancedMaterialDictionary.LoadMethod("fauna", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("Beton3", new Surface
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Weapons", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard,
				Reflectivity = 1.4f
			}, false),
			new InstancedMaterialDictionary.LoadMethod("New_weapons", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard,
				Reflectivity = 1.4f
			}, false),
			new InstancedMaterialDictionary.LoadMethod("mortairs", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\floor_textures\\Block1", new WetWood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\floor_textures\\floor_peers", new WetWood(), false),
			new InstancedMaterialDictionary.LoadMethod("BloodyWood1", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("shallow", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.AlphaMaterialWithoutShadow
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Break1", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("Break2", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("Break3", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("copper", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Barrels", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Dirt1", new Wood
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Gold1", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Grass1", new Wood
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("GrassRock1", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("IsleRockMoss", new Cloth(false)
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("IsleDirt", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("IsleRock1", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("IsleRock2", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("IsleRock3", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("IsleRock5", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("IsleRock7", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("IsleRock8", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Metal1", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("Magma1", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Magma2", new Wood
			{
				FlowAnimated = true,
				RasterizerOptions = MaterialRasterizeOptions.AlphaMaterialWithoutShadow
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Water", new Plastic
			{
				FlowAnimated = true
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Huts", new Flora(MaterialRasterizeOptions.AlphaMaterial, false), false),
			new InstancedMaterialDictionary.LoadMethod("Wood6", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood7", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood8", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood10", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood11", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood12", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood13", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood14", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood16", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood17", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood18", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood19", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood20", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood21", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood22", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood23", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood25", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood27", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood28", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood29", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood30", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood31", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Wood32", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Pebbles", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("StoneFloor", new Stone
			{
				SpecularIntensivity = 0.55f
			}, false),
			new InstancedMaterialDictionary.LoadMethod("StoneFloor2", new Stone
			{
				SpecularIntensivity = 0.55f
			}, false),
			new InstancedMaterialDictionary.LoadMethod("HellRock", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("IcebergMap", new Surface
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("SnowGrass", new Wood
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("SnowRock", new Wood
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("GrassLand2", new Cloth(false)
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("SnowGrassLand1", new Cloth(false)
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("SnowPartial", new Cloth(false)
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("WhiteWood2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("EmptyTexture", new Wood
			{
				RasterizerOptions = MaterialRasterizeOptions.AlphaMaterialWithoutShadow
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Empty0Texture", new Wood
			{
				RasterizerOptions = MaterialRasterizeOptions.AlphaMaterialWithoutShadow
			}, false),
			new InstancedMaterialDictionary.LoadMethod("glass1", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Crew1", new Cloth(false)
			{
				SpecularIntensivity = 0f
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Crew2", new Cloth(false)
			{
				SpecularIntensivity = 0f
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Crew3", new Cloth(false)
			{
				SpecularIntensivity = 0f
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Charters\\People_1_1", new Cloth(false)
			{
				SpecularIntensivity = 0f
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Charters\\People_1_2", new Cloth(false)
			{
				SpecularIntensivity = 0f
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Charters\\People_1_3", new Cloth(false)
			{
				SpecularIntensivity = 0f
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Charters\\People_2_1", new Cloth(false)
			{
				SpecularIntensivity = 0f
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Charters\\People_2_2", new Cloth(false)
			{
				SpecularIntensivity = 0f
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Charters\\People_2_3", new Cloth(false)
			{
				SpecularIntensivity = 0f
			}, false),
			new InstancedMaterialDictionary.LoadMethod("staff", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("figures1", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("figures2", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("reef", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("windows", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("windows_dark", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("SandDirty", new Cloth(false)
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("bouys", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Whales\\Whale", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Whales\\Orca", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Whales\\Sperm_Whale", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\navyPrivFlag", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail2_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail2_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail2_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail2_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail3_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail3_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail3_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail3_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail4_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail4_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail4_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail4_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail5_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail5_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail5_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail5_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail6_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail6_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail6_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail6_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail7_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail7_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail7_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail7_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail8_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail8_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail8_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail8_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail9_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail9_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail9_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail9_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail10_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail10_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail10_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail10_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail11_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail11_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail11_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail11_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail12_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail12_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail12_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail12_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\unique_sail_balloon", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\unique_sail_1", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shipsail_dthe", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shipsail_fs2", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shipsail_fs4", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shipsail_hlb", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shipsail_tpc", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shipsail_wht", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shipsail_gl1", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shipsail_gl2", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shipsail_gl3_c", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shipsail_gl4_c", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shipsail_gl5_c", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shipsail_gl6_c", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\cgs_sail", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shipsail_dred", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Octo_sail", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Santa_Unic_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Santa_Unic_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Santa_Unic_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Santa_Unic_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Santa_Unic2_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Santa_Unic2_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Santa_Unic2_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Santa_Unic2_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail13_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail13_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail13_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail13_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_grecebw_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_grecebw_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_grecebw_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_grecebw_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_UDragon_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_UDragon_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_UDragon_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_UDragon_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_UFish_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_UFish_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_UFish_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_UFish_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_USkull_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_USkull_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_USkull_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_USkull_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_YR_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_YR_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_YR_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_YR_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Clever_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Clever_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Clever_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Clever_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Anchor_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Anchor_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Anchor_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Anchor_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_RBW_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_RBW_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_RBW_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_RBW_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_WB_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_WB_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_WB_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_WB_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_BrownSkull_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_BrownSkull_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_BrownSkull_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_BrownSkull_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_maori_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_maori_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_maori_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_maori_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Palm_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Palm_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Palm_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Palm_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_RFishes_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_RFishes_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_RFishes_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_RFishes_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_spb_1_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_spb_1_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_spb_1_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_spb_1_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_sprut_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_sprut_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_sprut_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_sprut_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_seahorse_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_seahorse_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_seahorse_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_seahorse_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_manta_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_manta_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_manta_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_manta_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_wlion_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_wlion_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_wlion_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_wlion_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_izumrud_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_izumrud_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_izumrud_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_izumrud_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Horse_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Horse_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Horse_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_Horse_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_swords_main", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_swords_mini", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_swords_side", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Sail_swords_triangle", new SailSloth(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\ship_qar_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_qar_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\fc_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\brig_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship1_detail", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_pickle_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_pickle_deck", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_kech_deck", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\ship_kech_decor1", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\ship_kech_decor2", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\ship_kech_decor3", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_kech_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_kech_kab", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Shen", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Mast_1", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_konsta", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_flute_hull_smug", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_flute_deck", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_pwilliam", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\ship_pwilliam_dec2", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\ship_pwilliam_dec3", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_pwilliam_back", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_lacreole_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_black", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_black_gunport", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_black_light", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_falmouth_decor1", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_falmouth_decor2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_falmouthtrader_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_blackw_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_essex_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\ship_fr_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_fr_decor3", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_fr_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\floor_textures\\lecfcrt_deck", new WetWood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\lecfcrt_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\hms_victory_back", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\hms_victory_back_white", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\floor_textures\\hmsvictory_deck", new WetWood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\hmsvictory_gunport", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\hmsvictory_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_sirena_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\sirena_decor", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\ship_sirena_gerb", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\sirena_bronze", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Tranec", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\BoatTex", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\BoatTexWhite", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Cspanel", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\firestorm_backdec", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\firestorm_decor_1", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\firestorm_hull_1", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\firestorm_hull_2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\firestorm_hull_bw", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Vants", new Cloth(false), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Vants_dark", new Cloth(false), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Vants2", new Cloth(false), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Vants2_dark", new Cloth(false), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Vants3", new Cloth(false), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Vants3_dark", new Cloth(false), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Vants4", new Flora(MaterialRasterizeOptions.AlphaMaterial, false), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Vants5", new Cloth(false), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\kaligula_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\floor_textures\\kaligula_floor", new WetWood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\floor_textures\\kaligula_floor_light", new WetWood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\kaligula_castleCross", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\grid.alpha", new Flora(MaterialRasterizeOptions.AlphaMaterial, false), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_devouer_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\cgs_hull2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\dfish_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\dfish_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\dfish_mast", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_santisima_back", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_santisima_back_black", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_santisima_deck", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_santisima_hull2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\anson_detail", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\anson_detail_2", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_nep_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_nep_decor", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_stel_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\ship_stel_figure", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_stel_hull_decor", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_stpav_hull_decor", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\ship_kbk_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_kbk_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_kbk_roof", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_kbk_roof2", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_const_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_const_hull2", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_sanspariel_decor2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_sov_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\ship_sov_hull_alpha", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\requin", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\ship_redsail_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\VantsRedsail", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\yacht_decor", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\inger_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\lacor_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\lacor_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\iron_cherepica", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\customization_dark_hull", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\adventure_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\floor_textures\\gray_deck", new WetWood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\12apostle_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\12_Korma_Decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\mordaunt_Decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\12_sighn", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\San_martin_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Poltava", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Poltava_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\7prov", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\gold_wood_design", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\7_prov_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Decals_Ships", new Lambert
			{
				RasterizerOptions = MaterialRasterizeOptions.AlphaMaterialWithoutShadow
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\La_royale_HULL", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\LaRoyal_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Octo", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Octo_2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Kwee_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\firestorm_decor_2", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\des_ship_redsail_hull_white", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\des_ship_devouer_hull_red", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\des_ship_const_hull2_striped", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\des_ship_const_hull_orange", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Balloon_1", new Cloth(false), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Const_mast", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\santisima_deck", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Empire_1_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Empire_1_Decor", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\floor_textures\\Empire_floor", new WetWood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\floor_textures\\Empire_floor_2", new WetWood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Unite_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\surprise_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\FW_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\StLouisDecor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\StLouisHull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Azov_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Vasa_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Vasa_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Vasa_alpha", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\Vasa_hull_hist", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Vasa_decor_hist", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Vasa_alpha_hist", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Decor_southamption", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\3Hier_Decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Montan_decor", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\rare_flag1", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\rare_flag2", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\rare_flag3", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\orden_flag", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\orden_sail", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\fanatics_sail", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\fanatics_flag", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_01", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_02", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_03", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_04", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_05", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_06", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_07", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_08", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_09", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_10", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_11", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_12", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_13", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_14", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_15", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\effects\\sdf_16", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Flagpoll2", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\black_sail", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\caper_flag", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\trader_flag", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\empire_flag", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\empire_flag_oct", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\Empire_vimpel", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("Buildings\\church", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("Buildings\\mramor1", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Buildings\\mramor2", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("Buildings\\skulls", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("Buildings\\Castle_wall", new Wood
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Buildings\\Columb_statue", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("Buildings\\Dragon_isl", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("Buildings\\Pillar_nozul", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("Buildings\\Gold_lions", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Drop\\drop1", new BlinnPhong(56f, 1f, 1f, false, MaterialRasterizeOptions.AlphaMaterial), false),
			new InstancedMaterialDictionary.LoadMethod("Drop\\drop2", new BlinnPhong(56f, 1f, 1f, false, MaterialRasterizeOptions.AlphaMaterial), false),
			new InstancedMaterialDictionary.LoadMethod("Drop\\drop3_1", new BlinnPhong(56f, 1f, 1f, false, MaterialRasterizeOptions.AlphaMaterial), false),
			new InstancedMaterialDictionary.LoadMethod("Drop\\drop3_2", new BlinnPhong(56f, 1f, 1f, false, MaterialRasterizeOptions.AlphaMaterial), false),
			new InstancedMaterialDictionary.LoadMethod("Drop\\drop4", new BlinnPhong(56f, 1f, 1f, false, MaterialRasterizeOptions.AlphaMaterial), false),
			new InstancedMaterialDictionary.LoadMethod("Drop\\drop5", new BlinnPhong(56f, 1f, 1f, false, MaterialRasterizeOptions.AlphaMaterial), false),
			new InstancedMaterialDictionary.LoadMethod("Port\\atlas_tudor", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Port\\buildings_diff", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Port\\castle_diff", new Wood
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Port\\propps_diff", new Cloth(false)
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Port\\rare_building_diff", new Cloth(false), false),
			new InstancedMaterialDictionary.LoadMethod("Port\\medlight", new Plastic
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Port\\containers", new Wood
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Port\\house", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Port\\port1", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Port\\port2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Port\\port3", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Port\\lazurit_roof", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Port\\domna", new Stone(), false),
			new InstancedMaterialDictionary.LoadMethod("Port\\factory_place", new Cloth(false), false),
			new InstancedMaterialDictionary.LoadMethod("Port\\fort_new", new Wood
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Port\\nort_house", new Wood
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Port\\arab", new Wood
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Port\\gold_roof", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("Port\\RusNorth", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Port\\Maya", new Stone
			{
				RasterizerOptions = MaterialRasterizeOptions.SingleSidedHard
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Charters\\nobleman", new Cloth(false), false),
			new InstancedMaterialDictionary.LoadMethod("MapScene\\ms_table", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("MapScene\\ms_map", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("MapScene\\Candle", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("MapScene\\Astro", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("MapScene\\Chern", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("MapScene\\Pipe", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("MapScene\\Kompas", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("MapScene\\Mushket", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("MapScene\\Sextant", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("MapScene\\Dublon", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("MapScene\\Coins", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("MapScene\\Clock_2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("MapScene\\Clock_2_eng", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\public\\", new Wood(), true),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shop_sailes\\", new Cloth(true), true),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shop_flags\\", new Cloth(true), true),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\shop_flags_arena\\", new Cloth(true), true),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\public_guilds\\", new Cloth(true), true),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\fractions\\", new Cloth(true), true),
			new InstancedMaterialDictionary.LoadMethod("Sailes\\flagpolls\\", new Cloth(true), true),
			new InstancedMaterialDictionary.LoadMethod("public\\7b9d03bf\\des_ship_pwilliam", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\7b9d03bf\\des_ship_pwilliam_back_pw", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\7b9d03bf\\des_Vants3", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\8efc5d75\\des_ship_stel_hull", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("public\\74b8261a\\des_emtpyTexReplace", new Cloth(true), false),
			new InstancedMaterialDictionary.LoadMethod("public\\74b8261a\\des_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\06606749\\des_ship_santisima_back", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\06606749\\des_ship_santisima_hull2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\06606749\\des_ship_santisima_wood13", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\06606749\\des_ship_santisima_cname1", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\d112_ship_santisima_hull2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\d113_ship_santisima_hull2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\d114_hms_victory_back", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\d114_hmsvictory_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\d116_firestorm_decor_1", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Poltava2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Word1", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Sov1", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Poltava3", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\lacor_hull2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\San_martin_decor2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_falmouthtrader_hull2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\santisima_deck2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_santisima_back2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_santisima_deck2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_santisima_hull3", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_qar_decor_2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("WhiteWood333", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\brig_hull_helloween", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_const_hull2_orange", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_decor_Empire", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_Empire", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_helloween", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Mast_1_red", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_flute_deck_red", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_flute_hull_red", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\adventure_decor_red", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Vants2_red", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Vants3_red", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Vants4_dark", new Flora(MaterialRasterizeOptions.AlphaMaterial, false), false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Decals_Ships_Dark", new Lambert
			{
				RasterizerOptions = MaterialRasterizeOptions.AlphaMaterialWithoutShadow
			}, false),
			new InstancedMaterialDictionary.LoadMethod("Ships\\decor\\Decals_Ships_red", new Lambert
			{
				RasterizerOptions = MaterialRasterizeOptions.AlphaMaterialWithoutShadow
			}, false),
			new InstancedMaterialDictionary.LoadMethod("public\\Octo_dark", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Octo_decor_brass", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Wood8_pink", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Wood8_green", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Shen_white", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\LaRoyal_dec_white", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\La_royale_HULL_pink", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Figures-2_desighn", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Shen_green", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Octo_blood", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Octo_hull_blood", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_sov2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_sov_hull_alpha2", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Vants2_grey", new Cloth(false), false),
			new InstancedMaterialDictionary.LoadMethod("public\\7_prov_decor_silver", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\7prov2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Wood24", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\firestorm_decor_3", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\firestorm_decor_4", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\BoatTex_gray", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\firestorm_hull_1_Bice", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\firestorm_hull_1_Wice", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\firestorm_hull_2_Bice", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\firestorm_hull_2_Wice", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\firestorm_hull_bw_Bice", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\firestorm_hull_bw_Wice", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_kech_kab_ice", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stpav_hull_decor_Color", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stpav_hull_decor_Dragon", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stpav_hull_decor_Sev", new Surface(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_Empire_Color", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_Dragon", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_Sev", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_1", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_3", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_4", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_5", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_6", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_7", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_bone", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Azov_decor_silver", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Azov_decor_1", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Empire_2_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Empire_2_Decor", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Empire_3_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Empire_3_Decor", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Empire_4_hull", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Empire_4_Decor", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\12apostle_decor1", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\12_Korma_Decor1", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\12_Korma_Decor_Silver", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\12_Korma_Decor_Dark", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\hmsvictory_hull_12_decor1", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\brig_hull_Brown", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\brig_hull_Red", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\brig_hull_Blue", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_kech_hull_Brown", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_kech_hull_Red", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_kech_hull_White", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Wood22_Blue", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Wood22_turq", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Wood22_White", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\12apostle_decor_blue", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\hmsvictory_hull_5", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\hmsvictory_hull_6", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\hmsvictory_hull_8", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\hms_victory_back_darkblue", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\hmsvictory_hull_darkblue", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\adventure_decor_silver", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_sov_hull_adventure_decor_1", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\hmsvictory_hull_blue", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\hms_victory_back_blue", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_essex_hull_lwood", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Decor_thouthamption_izumrud", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Unite_hull_izumrud", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\brig_hull_Iberia_2", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\3Hier_Decor_silver", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_3hier_1", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\hmsvictory_hull_Barco", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\hmsvictory_gunport_dark", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_kech_kab_grey", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_sanspariel_decor3", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_stel_hull_pariel", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\dfish_hull_sprut", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\dfish_mast_sprut", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\dfish_decor_sprut", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_lacreole_hull_WGray", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_lacreole_hull_Yellow", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\requin_Yellow", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_santisima_hull5", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_santisima_back5", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_qar_decor_4", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Mast_1_emp", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\anson_detail_emp", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\anson_detail_2_emp", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_falmouthtrader_hull_3", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\San_martin_decor3", new Plastic(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Octo_bone", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Octo_2_bone", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_pwilliam_back_Yellow", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\ship_pwilliam_Yellow", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Octo_blue", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Octo_2_blue", new Wood(), false),
			new InstancedMaterialDictionary.LoadMethod("public\\Decals2_Ships", new Lambert
			{
				RasterizerOptions = MaterialRasterizeOptions.AlphaMaterialWithoutShadow
			}, false)
		};

		// Token: 0x04001A77 RID: 6775
		private static readonly BlinnPhong terrainMaterial = new BlinnPhong(64f, 0.1f, 0.5f, false, MaterialRasterizeOptions.SingleSidedHard);

		// Token: 0x04001A78 RID: 6776
		internal static readonly TerrainLoadMethod[] Terrains = new TerrainLoadMethod[]
		{
			new TerrainLoadMethod(1, TerrainShadingMode.DoubleTextures, "IsleRock2", "IsleRockMoss", "", "", Materials.terrainMaterial),
			new TerrainLoadMethod(2, TerrainShadingMode.DoubleTextures, "IsleRock1", "IsleRock1", "", "", Materials.terrainMaterial),
			new TerrainLoadMethod(8, TerrainShadingMode.TripleTexture, "IsleRock3", "IsleRock3", "Pebbles", "", Materials.terrainMaterial),
			new TerrainLoadMethod(14, TerrainShadingMode.DoubleTextures, "HellRock", "IsleRock3", "", "", Materials.terrainMaterial),
			new TerrainLoadMethod(6, TerrainShadingMode.DoubleTextures, "SnowRock", "SnowGrassLand1", "", "", Materials.terrainMaterial),
			new TerrainLoadMethod(7, TerrainShadingMode.TripleTexture, "SnowRock", "SnowGrass", "IsleRock7", "", Materials.terrainMaterial),
			new TerrainLoadMethod(11, TerrainShadingMode.TripleTexture, "IsleRockMoss", "Grass1", "Dirt1", "", Materials.terrainMaterial),
			new TerrainLoadMethod(3, TerrainShadingMode.TripleTexture, "IsleRockMoss", "GrassRock1", "IsleRockMoss", "", Materials.terrainMaterial),
			new TerrainLoadMethod(9, TerrainShadingMode.TripleTexture, "IsleRock7", "Grass1", "IsleDirt", "", Materials.terrainMaterial),
			new TerrainLoadMethod(12, TerrainShadingMode.TripleTexture, "IsleRock3", "HellRock", "Pebbles", "", Materials.terrainMaterial),
			new TerrainLoadMethod(13, TerrainShadingMode.TripleTexture, "IsleRock7", "Grass1", "IsleDirt", "", Materials.terrainMaterial),
			new TerrainLoadMethod(15, TerrainShadingMode.TripleTexture, "IsleRock5", "Grass1", "IsleDirt", "", Materials.terrainMaterial),
			new TerrainLoadMethod(16, TerrainShadingMode.DoubleTextures, "IsleRock8", "IsleRock8", "", "", Materials.terrainMaterial),
			new TerrainLoadMethod(17, TerrainShadingMode.TripleTexture, "IsleRock7", "Grass1", "IsleDirt", "", Materials.terrainMaterial),
			new TerrainLoadMethod(18, TerrainShadingMode.TripleTexture, "IsleRock7", "Grass1", "Pebbles", "", Materials.terrainMaterial),
			new TerrainLoadMethod(19, TerrainShadingMode.TripleTexture, "IsleRock5", "GrassRock1", "Dirt1", "", Materials.terrainMaterial),
			new TerrainLoadMethod(20, TerrainShadingMode.TripleTexture, "SnowGrass", "SnowGrassLand1", "IsleRock7", "", Materials.terrainMaterial),
			new TerrainLoadMethod(21, TerrainShadingMode.TripleTexture, "IsleRock7", "IsleRock2", "Pebbles", "", Materials.terrainMaterial),
			new TerrainLoadMethod(22, TerrainShadingMode.TripleTexture, "SnowGrass", "SnowGrassLand1", "IsleRock7", "", Materials.terrainMaterial),
			new TerrainLoadMethod(23, TerrainShadingMode.TripleTexture, "IsleRock3", "IsleRock3", "Pebbles", "", Materials.terrainMaterial),
			new TerrainLoadMethod(24, TerrainShadingMode.TripleTexture, "IsleRock3", "IsleRock3", "Pebbles", "", Materials.terrainMaterial),
			new TerrainLoadMethod(25, TerrainShadingMode.DoubleTextures, "IsleRock7", "IsleRock7", "", "", Materials.terrainMaterial),
			new TerrainLoadMethod(26, TerrainShadingMode.TripleTexture, "IsleRock1", "SandDirty", "Dirt1", "", Materials.terrainMaterial),
			new TerrainLoadMethod(27, TerrainShadingMode.TripleTexture, "IsleRock7", "SnowPartial", "Pebbles", "", Materials.terrainMaterial),
			new TerrainLoadMethod(28, TerrainShadingMode.TripleTexture, "IsleRock7", "SnowPartial", "Pebbles", "", Materials.terrainMaterial)
		};

		// Token: 0x04001A79 RID: 6777
		public static InstancedMaterialDictionary TexturesDatabase;

		// Token: 0x04001A7A RID: 6778
		private static MaterialLoadInfo materialsLoadInfo;

		// Token: 0x04001A7B RID: 6779
		private static VirtualTexture[] sailDestructTextures;
	}
}
