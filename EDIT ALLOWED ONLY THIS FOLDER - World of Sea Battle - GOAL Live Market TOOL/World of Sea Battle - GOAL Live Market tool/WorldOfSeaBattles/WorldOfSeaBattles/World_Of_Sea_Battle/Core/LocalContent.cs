using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Account;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Core;
using TheraEngine.Graphics;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;
using TheraEngine.ProcedureGeneration.Generation3D;
using TheraEngine.Scene;
using UWContentPipelineExtensionRuntime.Tags;
using WorldOfSeaBattles;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.Core
{
	// Token: 0x020004D9 RID: 1241
	internal sealed class LocalContent
	{
		// Token: 0x06001B8E RID: 7054 RVA: 0x000F86A0 File Offset: 0x000F68A0
		public static VirtualTexture GetDecalForSail(ShipDesignInfo {25059}, [Nullable(2)] string {25060})
		{
			if ({25059}.WrapsPublicDesign)
			{
				return LocalContent.PublicDesignHelper({25059});
			}
			if ({25060} != null && !{25060}.Contains("Sail") && !{25060}.Contains("sail"))
			{
				return null;
			}
			if ({25059}.Category == ShipDesignCategory.SailTexture)
			{
				string texture = {25059}.Texture;
				if (texture[texture.Length - 1] == '_')
				{
					if ({25060} == null || {25060}.StartsWith("shipsail") || {25060}.StartsWith("unique"))
					{
						Material material = Materials.TexturesDatabase[{25059}.Texture + "main"];
						if (material != null)
						{
							return material.Albedo;
						}
						goto IL_103;
					}
					else
					{
						int num = {25060}.IndexOf('_') + 1;
						string str = {25060}.Substring(num, {25060}.Length - num);
						Material material2 = Materials.TexturesDatabase[{25059}.Texture + str];
						if (material2 != null)
						{
							return material2.Albedo;
						}
						goto IL_103;
					}
				}
			}
			if ({25059}.Texture.Length > 0)
			{
				Material material3 = Materials.TexturesDatabase[{25059}.Texture];
				if (material3 != null)
				{
					return material3.Albedo;
				}
			}
			IL_103:
			bool {25297} = true;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(80, 3);
			defaultInterpolatedStringHandler.AppendLiteral("GetDecalForSail: There is no design texture '");
			defaultInterpolatedStringHandler.AppendFormatted({25059}.Texture);
			defaultInterpolatedStringHandler.AppendLiteral("' ShipDesignInfo.ID: ");
			defaultInterpolatedStringHandler.AppendFormatted<short>({25059}.ID);
			defaultInterpolatedStringHandler.AppendLiteral(", targetName: ");
			defaultInterpolatedStringHandler.AppendFormatted({25060});
			Assert.Report({25297}, defaultInterpolatedStringHandler.ToStringAndClear());
			return null;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x000F8810 File Offset: 0x000F6A10
		public static bool IsFullDesign(int {25061})
		{
			return {25061} <= Gameplay.DesignsInfo.Count && (Gameplay.DesignsInfo.FromID({25061}).Category == ShipDesignCategory.ShipFullDesign || {25061} == 35 || {25061} == 55 || {25061} == 65 || {25061} == 66 || {25061} == 80);
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x000F885C File Offset: 0x000F6A5C
		public static bool DisallowWithFlora(string {25062})
		{
			return {25062} == "Dirt1" || {25062} == "Dirt2" || {25062} == "trees_north" || {25062} == "trees_south" || {25062} == "Trees_Unic" || {25062} == "tree_trunk";
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x000F88B8 File Offset: 0x000F6AB8
		public static Texture2D ShipFullDesignReplace(string {25063}, int {25064}, ModelPartShadercall {25065})
		{
			if (string.IsNullOrEmpty({25063}))
			{
				return null;
			}
			if ({25064} == 9999 || {25064} == 9998)
			{
				if ({25063} == "IcebergMap" && {25064} != 9999)
				{
					return Materials.TexturesDatabase["HellRock"].Albedo.Tex;
				}
				if ({25063} == "IsleRock8")
				{
					return Materials.TexturesDatabase[({25064} == 9999) ? "SnowRock" : "IsleRock3"].Albedo.Tex;
				}
				if (LocalContent.DisallowWithFlora({25063}))
				{
					return null;
				}
				return Materials.TexturesDatabase[{25063}].Albedo.Tex;
			}
			else
			{
				if (({25064} == 229 || {25064} == 230 || {25064} == 231 || {25064} == 232 || {25064} == 233 || {25064} == 435) && {25063}.Contains("Vants"))
				{
					return Materials.TexturesDatabase[{25063}].Albedo.Tex;
				}
				if ({25064} <= 66)
				{
					if ({25064} <= 55)
					{
						if ({25064} != 35)
						{
							if ({25064} == 55)
							{
								if ({25063}.Contains("sail"))
								{
									return Materials.TexturesDatabase["black_sail"].Albedo.Tex;
								}
								if ({25063} == "Vants3")
								{
									return Materials.TexturesDatabase["Vants3_dark"].Albedo.Tex;
								}
								if ({25063} == "Wood7")
								{
									return Materials.TexturesDatabase["Wood12"].Albedo.Tex;
								}
								if ({25063} == "Mast_1")
								{
									return Materials.TexturesDatabase["Wood11"].Albedo.Tex;
								}
								if ({25063} == "kaligula_floor")
								{
									return Materials.TexturesDatabase["Wood12"].Albedo.Tex;
								}
								Material material = Materials.TexturesDatabase["des_" + {25063}];
								if (material != null)
								{
									return material.Albedo.Tex;
								}
								goto IL_682;
							}
						}
						else
						{
							Material material = Materials.TexturesDatabase["des_" + {25063}];
							if (material != null)
							{
								return material.Albedo.Tex;
							}
							goto IL_682;
						}
					}
					else if ({25064} != 65)
					{
						if ({25064} == 66)
						{
							if ({25063} == "ship_qar_decor")
							{
								return Materials.TexturesDatabase["Wood16"].Albedo.Tex;
							}
							if ({25063} == "ship_santisima_hull2")
							{
								return Materials.TexturesDatabase["customization_dark_hull"].Albedo.Tex;
							}
							if ({25063} == "Wood6")
							{
								return Materials.TexturesDatabase["Wood14"].Albedo.Tex;
							}
							if ({25063} == "Wood14")
							{
								return Materials.TexturesDatabase["Wood18"].Albedo.Tex;
							}
							goto IL_682;
						}
					}
					else
					{
						if ({25063} == "ship_santisima_back_dec" || {25063} == "Wood13" || {25063} == "Wood14" || {25063} == "ship_qar_decor")
						{
							return Materials.TexturesDatabase["Wood16"].Albedo.Tex;
						}
						if ({25063} == "Wood6")
						{
							return Materials.TexturesDatabase["Wood13"].Albedo.Tex;
						}
						if ({25063} == "EmptyTexture")
						{
							return Materials.TexturesDatabase["des_emtpyTexReplace"].Albedo.Tex;
						}
						if ({25063} == "ship_santisima_hull2")
						{
							return Materials.TexturesDatabase["des_hull"].Albedo.Tex;
						}
						if ({25063} == "ship_santisima_mst")
						{
							return Materials.TexturesDatabase["Wood6"].Albedo.Tex;
						}
						goto IL_682;
					}
				}
				else if ({25064} <= 233)
				{
					if ({25064} != 80)
					{
						switch ({25064})
						{
						case 229:
							return Materials.TexturesDatabase["BloodyWood1"].Albedo.Tex;
						case 230:
							return Materials.TexturesDatabase["SnowRock"].Albedo.Tex;
						case 231:
							return Materials.TexturesDatabase["Magma1"].Albedo.Tex;
						case 232:
							return Materials.TexturesDatabase["Wood16"].Albedo.Tex;
						case 233:
							return Materials.TexturesDatabase["Dirt1"].Albedo.Tex;
						}
					}
					else
					{
						if ({25063} == "ship_santisima_back")
						{
							return Materials.TexturesDatabase["des_ship_santisima_back"].Albedo.Tex;
						}
						if ({25063} == "ship_santisima_hull2")
						{
							return Materials.TexturesDatabase["des_ship_santisima_hull2"].Albedo.Tex;
						}
						if ({25063} == "ship_santisima_decor")
						{
							return Materials.TexturesDatabase["des_ship_santisima_decor"].Albedo.Tex;
						}
						if ({25063} == "Wood13")
						{
							return Materials.TexturesDatabase["des_ship_santisima_wood13"].Albedo.Tex;
						}
						if ({25063} == "Wood14")
						{
							return Materials.TexturesDatabase["des_ship_santisima_wood13"].Albedo.Tex;
						}
						if ({25063} == "ship_santisima_back_dec")
						{
							return Materials.TexturesDatabase["Wood12"].Albedo.Tex;
						}
						if ({25063} == "BoatTex")
						{
							return Materials.TexturesDatabase["BoatTexWhite"].Albedo.Tex;
						}
						if ({25063} == "Empty0Texture")
						{
							return Materials.TexturesDatabase["des_ship_santisima_cname1"].Albedo.Tex;
						}
						goto IL_682;
					}
				}
				else if ({25064} != 435)
				{
					if ({25064} == 999)
					{
						string {15961};
						if (Global.Player.Client.UseDesignEditor != null && Global.Player.Client.UseDesignEditor.TryGetValue({25063}, out {15961}))
						{
							return Materials.TexturesDatabase[{15961}].Albedo.Tex;
						}
						goto IL_682;
					}
				}
				else
				{
					if ({25063}.Contains("figures"))
					{
						return Materials.TexturesDatabase[{25063}].Albedo.Tex;
					}
					return Materials.TexturesDatabase["gold_wood_design"].Albedo.Tex;
				}
				string {15961}2;
				if (Gameplay.DesignsInfo.FromID({25064}).ShipFullDesignTable.TryGetValue({25063}, out {15961}2))
				{
					return Materials.TexturesDatabase[{15961}2].Albedo.Tex;
				}
				IL_682:
				return Materials.TexturesDatabase[{25063}].Albedo.Tex;
			}
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x000F8F5C File Offset: 0x000F715C
		public static VirtualTexture GetShipFlagTexture(ShipDesignInfo {25066})
		{
			if ({25066}.WrapsPublicDesign)
			{
				return LocalContent.PublicDesignHelper({25066});
			}
			if ({25066}.Texture.Length <= 0)
			{
				return Materials.TexturesDatabase["desing_flag1"].Albedo;
			}
			Material material = Materials.TexturesDatabase[{25066}.Texture];
			if (material == null)
			{
				return null;
			}
			return material.Albedo;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x000F8FB8 File Offset: 0x000F71B8
		private static VirtualTexture PublicDesignHelper(ShipDesignInfo {25067})
		{
			if ({25067}.ApartIconTex != null)
			{
				return new VirtualTexture(string.Empty, {25067}.ApartIconTex);
			}
			Material material = Materials.TexturesDatabase[{25067}.Texture];
			if (material == null)
			{
				material = Materials.TexturesDatabase.AddTexture({25067}.Texture, ({25067}.ApartIconTex != null) ? new VirtualTexture(string.Empty, {25067}.ApartIconTex) : new VirtualTexture(Engine.FillerTexture, string.Empty, new VirtualTextureSource({25067}.Texture, VirtualSourceType.Web)), null);
			}
			if (material == null)
			{
				return null;
			}
			return material.Albedo;
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x000F9044 File Offset: 0x000F7244
		public static VirtualTexture GetNpcSail(ShipNpc {25068})
		{
			if ({25068}.UsedShipNpc.Information.Descritpion == NpcType.PortPatrol)
			{
				ShipDesignInfo shipDesignInfo;
				ShipDesignInfo shipDesignInfo2;
				LocalContent.GetFractionGeraldics(Session.Game.NearPortGuild.Fraction, out shipDesignInfo, out shipDesignInfo2);
				if (shipDesignInfo2 != null)
				{
					return LocalContent.GetDecalForSail(shipDesignInfo2, null);
				}
				return null;
			}
			else
			{
				if ({25068}.UsedShipNpc.SailDesign != null)
				{
					return LocalContent.GetDecalForSail({25068}.UsedShipNpc.SailDesign, null);
				}
				return null;
			}
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x000F90B0 File Offset: 0x000F72B0
		public static VirtualTexture GetNpcFlag(Npc {25069})
		{
			if ({25069}.UsedShipNpc.Information.Descritpion == NpcType.HeadHunter)
			{
				return null;
			}
			if ({25069}.MapInfo.IsEducationMap)
			{
				if ({25069}.IsPlayerCaper)
				{
					return null;
				}
				return Materials.TexturesDatabase["desing_flag11"].Albedo;
			}
			else
			{
				if ({25069}.UsedShipNpc.Information.Extras.IsEmpire)
				{
					ShipDesignInfo {25066};
					ShipDesignInfo shipDesignInfo;
					LocalContent.GetFractionGeraldics(FractionID.Empire, out {25066}, out shipDesignInfo);
					return LocalContent.GetShipFlagTexture({25066});
				}
				if ({25069}.UsedShipNpc.Information.Descritpion == NpcType.PortPatrol)
				{
					ShipDesignInfo shipDesignInfo;
					ShipDesignInfo shipDesignInfo2;
					LocalContent.GetFractionGeraldics(Session.Game.NearPortGuild.Fraction, out shipDesignInfo2, out shipDesignInfo);
					if (shipDesignInfo2 != null)
					{
						return LocalContent.GetShipFlagTexture(shipDesignInfo2);
					}
					return null;
				}
				else
				{
					if ({25069}.UsedShipNpc.Information.Extras.IsPirateFr1)
					{
						string[] array = new string[]
						{
							"rare_flag1",
							"rare_flag2",
							"desing_flag11",
							"desing_flag12",
							"desing_flag13"
						};
						return Materials.TexturesDatabase[array[{25069}.uID % array.Length]].Albedo;
					}
					if ({25069}.UsedShipNpc.Information.Extras.IsPirateFr2)
					{
						return Materials.TexturesDatabase["orden_flag"].Albedo;
					}
					if ({25069}.UsedShipNpc.Information.Extras.IsTrader)
					{
						ShipDesignInfo shipDesignInfo;
						ShipDesignInfo shipDesignInfo3;
						LocalContent.GetFractionGeraldics(FractionID.TradeUnion, out shipDesignInfo3, out shipDesignInfo);
						if (shipDesignInfo3 != null)
						{
							return LocalContent.GetShipFlagTexture(shipDesignInfo3);
						}
						return null;
					}
					else
					{
						if ({25069}.UsedShipNpc.Information.Descritpion == NpcType.Fanatics)
						{
							return Materials.TexturesDatabase["fanatics_flag"].Albedo;
						}
						if (!{25069}.MapInfo.IsEducationMap || !{25069}.IsPlayerCaper)
						{
							return null;
						}
						ShipDesignInfo shipDesignInfo;
						ShipDesignInfo shipDesignInfo4;
						LocalContent.GetFractionGeraldics(FractionID.TradeUnion, out shipDesignInfo4, out shipDesignInfo);
						if (shipDesignInfo4 != null)
						{
							return LocalContent.GetShipFlagTexture(shipDesignInfo4);
						}
						return null;
					}
				}
			}
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x000F9288 File Offset: 0x000F7488
		public static Material WorldFlagTexture(OpenWorldFlag {25070}, bool {25071}, FractionID {25072}, bool {25073}, bool {25074}, bool {25075})
		{
			if ({25074})
			{
				return Materials.TexturesDatabase["fp_fanatics"];
			}
			switch ({25070})
			{
			case OpenWorldFlag.Peaceful:
				return Materials.TexturesDatabase["fp_peace"];
			case OpenWorldFlag.Pirate:
				return Materials.TexturesDatabase["fp_pirate"];
			case OpenWorldFlag.Trader:
				if ({25075} || !{25073})
				{
					return Materials.TexturesDatabase["fp_peace"];
				}
				return Materials.TexturesDatabase["fp_trader"];
			case OpenWorldFlag.War:
				switch ({25072})
				{
				case FractionID.Pirate:
					if ({25071})
					{
						return Materials.TexturesDatabase["fp_wartensity_pirate"];
					}
					if (!{25071})
					{
						return Materials.TexturesDatabase["fp_war_pirate"];
					}
					break;
				case FractionID.Antilia:
					if ({25071})
					{
						return Materials.TexturesDatabase["fp_wartensity_antilia"];
					}
					if (!{25071})
					{
						return Materials.TexturesDatabase["fp_war_antilia"];
					}
					break;
				case FractionID.Espaniol:
					if ({25071})
					{
						return Materials.TexturesDatabase["fp_wartensity_espaniol"];
					}
					if (!{25071})
					{
						return Materials.TexturesDatabase["fp_war_espaniol"];
					}
					break;
				case FractionID.KaiAndSeveria:
					if ({25071})
					{
						return Materials.TexturesDatabase["fp_wartensity_kai_and_severia"];
					}
					if (!{25071})
					{
						return Materials.TexturesDatabase["fp_war_kai_and_severia"];
					}
					break;
				case FractionID.Empire:
					return Materials.TexturesDatabase["fp_war_empire"];
				case FractionID.TradeUnion:
					return Materials.TexturesDatabase["fp_war_trader"];
				}
				return {25073} ? new Material(new VirtualTexture(string.Empty, Engine.FillerTexture), null, null, new Lambert()) : Materials.TexturesDatabase["fp_war"];
			case OpenWorldFlag.Legendary:
				return Materials.TexturesDatabase["fp_legend"];
			case OpenWorldFlag.NoFlag:
				return Materials.TexturesDatabase["fp_caper"];
			case OpenWorldFlag.PeacefulDisallowed:
			case OpenWorldFlag.TraderDisallowed:
				return Materials.TexturesDatabase["fp_peace_dis"];
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x000F948C File Offset: 0x000F768C
		public static Material GetCrewTexture(OpenWorldFlag {25076}, UnitScene {25077})
		{
			if ({25077}.Role != UnitRole.Gunner && HashHelper.greater({25077}.RandomValue) > 0.7f)
			{
				return Materials.TexturesDatabase["Crew3"];
			}
			if ({25076} == OpenWorldFlag.War || {25076} == OpenWorldFlag.Legendary)
			{
				return Materials.TexturesDatabase["Crew1"];
			}
			if ({25076} != OpenWorldFlag.Pirate && {25076} != OpenWorldFlag.NoFlag)
			{
				return Materials.TexturesDatabase["Crew2"];
			}
			return Materials.TexturesDatabase["Crew3"];
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x000F9504 File Offset: 0x000F7704
		public static Texture2D GetIsleCrewTextureVariation(UWModel {25078}, int {25079})
		{
			string assetName = {25078}.Drawcalls[0].Material.Albedo.AssetName;
			string {15961} = assetName.Substring(0, assetName.Length - 1) + (HashHelper.greaterInt({25079}, 3) + 1).ToString();
			return Materials.TexturesDatabase[{15961}].Albedo.Tex;
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x000F9564 File Offset: 0x000F7764
		public UWModel GetRandomUnitModel(UnitRole {25080}, Sequence {25081})
		{
			switch ({25080})
			{
			case UnitRole.Gunner:
				return {25081}.Pick<UWModel>(this.UnitsCannonier);
			case UnitRole.Sailfish:
				return {25081}.Pick<UWModel>(this.UnitsSailor);
			case UnitRole.Musketeer:
				return {25081}.Pick<UWModel>(this.UnitsOfficer);
			case UnitRole.All:
				return {25081}.Pick<UWModel>(this.AllShipUnits);
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000F95C4 File Offset: 0x000F77C4
		public static UWModel GetIsleUnitModel(Isle {25082})
		{
			GameStaticSoundName? portEntrySoundStyle = Global.Game.SoundSystem.GetPortEntrySoundStyle({25082}.Statement.ModelData.Path);
			if (Rand.Chanse(20f))
			{
				return Rand.Pick<UWModel>(LocalContent.Loaded.AllShipUnits);
			}
			UWModel[] {14494} = (portEntrySoundStyle.GetValueOrDefault() == GameStaticSoundName.Port_Arabic) ? LocalContent.Loaded.ArabUnits : ((portEntrySoundStyle.GetValueOrDefault() == GameStaticSoundName.Port_Spain) ? LocalContent.Loaded.SpainUnits : ((portEntrySoundStyle.GetValueOrDefault() == GameStaticSoundName.Port_North) ? LocalContent.Loaded.NorthUnits : ((portEntrySoundStyle.GetValueOrDefault() == GameStaticSoundName.Port_Asian) ? LocalContent.Loaded.AsainUnits : ((portEntrySoundStyle.GetValueOrDefault() == GameStaticSoundName.Port_England) ? LocalContent.Loaded.EuropeUnits : ((portEntrySoundStyle.GetValueOrDefault() == GameStaticSoundName.Port_Pirates) ? LocalContent.Loaded.AllShipUnits : LocalContent.Loaded.AllPortUnits)))));
			if (portEntrySoundStyle.GetValueOrDefault() == GameStaticSoundName.Port_North || Rand.Chanse(70f))
			{
				return Rand.Pick<UWModel>({14494});
			}
			UWModel uwmodel;
			do
			{
				uwmodel = Rand.Pick<UWModel>(LocalContent.Loaded.AllPortUnits);
			}
			while (LocalContent.Loaded.AsainUnits.Contains(uwmodel));
			return uwmodel;
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000F96E4 File Offset: 0x000F78E4
		public static void GetFractionGeraldics(FractionID {25083}, out ShipDesignInfo {25084}, out ShipDesignInfo {25085})
		{
			ValueTuple<int, int> valueTuple = new ValueTuple<int, int>(-1, -1);
			LocalContent.Geraldics.TryGetValue({25083}, out valueTuple);
			{25084} = ((valueTuple.Item1 > 0) ? Gameplay.DesignsInfo[valueTuple.Item1] : null);
			{25085} = ((valueTuple.Item2 > 0) ? Gameplay.DesignsInfo[valueTuple.Item2] : null);
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x000F9744 File Offset: 0x000F7944
		public static void Initialize(ContentManager {25086})
		{
			LocalContent.Loaded = new LocalContent({25086});
			foreach (ShipDesignInfo shipDesignInfo in from {25092} in Gameplay.DesignsInfo
			where {25092}.Category == ShipDesignCategory.ShipFullDesign
			select {25092})
			{
				foreach (KeyValuePair<string, string> keyValuePair in shipDesignInfo.ShipFullDesignTable)
				{
					bool flag = Materials.TexturesDatabase[keyValuePair.Key] != null;
					Material material = Materials.TexturesDatabase[keyValuePair.Value];
					if (!flag || material == null)
					{
						throw new InvalidOperationException(string.Concat(new string[]
						{
							"Design ID ",
							shipDesignInfo.ID.ToString(),
							" has wrong texture: ",
							keyValuePair.Key,
							":",
							keyValuePair.Value
						}));
					}
				}
			}
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x000F9878 File Offset: 0x000F7A78
		private LocalContent(ContentManager {25087})
		{
			LocalContent.<>c__DisplayClass66_0 CS$<>8__locals1 = new LocalContent.<>c__DisplayClass66_0();
			CS$<>8__locals1.contentManager = {25087};
			base..ctor();
			this.WaterFrame = UWModel.CreateAll(null, CS$<>8__locals1.contentManager.Load<Model>(PathContent.dir_models + "wtframe"));
			this.AllDebris = new Dictionary<HitMaterialEffect, UWModel[]>();
			this.AllDebris.Add(HitMaterialEffect.Wood, CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("woodDebris1").ToArray());
			this.AllDebris.Add(HitMaterialEffect.Stone, CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("stoneDebris1").ToArray());
			this.AllDebris.Add(HitMaterialEffect.Sailes, CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("sailDebris").ToArray());
			this.Effect3 = RingAreaGenerator.Begin_VertexPositionColor(24, -0.5f, 0.25f, 1f, Color.Transparent, Color.Red * 0.7f, Color.Transparent, 0.9f);
			this.Whale = CS$<>8__locals1.<.ctor>g__LoadModel|0("whale");
			this.SpermWhale = CS$<>8__locals1.<.ctor>g__LoadModel|0("whale2");
			this.WhaleOrca = CS$<>8__locals1.<.ctor>g__LoadModel|0("whale3");
			this.DropModels = new Dictionary<DropModel, Tlist<DropModelData>>();
			this.IcedShipsDropModels = new Tlist<DropModelData>();
			foreach (object obj in Enum.GetValues(typeof(DropModel)))
			{
				DropModel dropModel = (DropModel)obj;
				if (dropModel != DropModel.Fishing && dropModel != DropModel.Whale && dropModel != DropModel.WorldFortLoot)
				{
					Tlist<DropModelData> tlist = new Tlist<DropModelData>(this.loadDropMeshes(CS$<>8__locals1.contentManager, dropModel));
					if (tlist.Size == 0)
					{
						this.DropModels.Add(dropModel, tlist);
					}
					else
					{
						this.DropModels.Add(dropModel, tlist);
					}
				}
			}
			for (int i = 0; i < 4; i++)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Iced_shipwreck_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(i + 1);
				string text = defaultInterpolatedStringHandler.ToStringAndClear();
				Tlist<DropModelData> icedShipsDropModels = this.IcedShipsDropModels;
				DropModelData dropModelData = new DropModelData(CS$<>8__locals1.contentManager.Load<Model>(Path.Combine(PathContent.dir_models, "loot", text)), text);
				icedShipsDropModels.Add(dropModelData);
			}
			this.Falkonet1 = new UWModel[]
			{
				CS$<>8__locals1.<.ctor>g__LoadModel|0("falkonet1_1"),
				CS$<>8__locals1.<.ctor>g__LoadModel|0("falkonet1_2")
			};
			this.Flagpoles = CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("Flagpoles");
			this.BoardingHook = new UWModel[]
			{
				CS$<>8__locals1.<.ctor>g__LoadModel|0("boardingHookIron")
			};
			this.WhaleHook = new ModelTransformedScene(CS$<>8__locals1.<.ctor>g__LoadModel|0("whaleHook"), Transform3D.Identity);
			this.SightLine = new ModelTransformedScene(CS$<>8__locals1.<.ctor>g__LoadModel|0("sight_line"), Transform3D.Identity);
			this.DebugBoxShapeDisplay = CS$<>8__locals1.<.ctor>g__LoadModel|0("debugShape");
			this.DebugSphereDisplay = new ModelTransformedScene(CS$<>8__locals1.<.ctor>g__LoadModel|0("debugSphere"), Transform3D.Identity);
			Model {15194} = CS$<>8__locals1.contentManager.Load<Model>(PathContent.dir_models + "pkegPack");
			this.powderKegPackByModelNumber = new ModelTransformedScene[]
			{
				new ModelTransformedScene(UWModel.Create(Materials.TexturesDatabase, {15194}, "model0", false), Transform3D.Identity),
				new ModelTransformedScene(UWModel.Create(Materials.TexturesDatabase, {15194}, "model1", false), Transform3D.Identity),
				new ModelTransformedScene(UWModel.Create(Materials.TexturesDatabase, {15194}, "model2", false), Transform3D.Identity),
				new ModelTransformedScene(UWModel.Create(Materials.TexturesDatabase, {15194}, "model3", false), Transform3D.Identity),
				new ModelTransformedScene(UWModel.Create(Materials.TexturesDatabase, {15194}, "model4", false), Transform3D.Identity),
				new ModelTransformedScene(UWModel.Create(Materials.TexturesDatabase, {15194}, "model5", false), Transform3D.Identity)
			};
			this.NeedleModel = CS$<>8__locals1.<.ctor>g__LoadModel|0("needle");
			this.ShipBigLamp = CS$<>8__locals1.<.ctor>g__LoadModel|0("shipBigLamp");
			this.NewYearTree = CS$<>8__locals1.<.ctor>g__LoadModel|0("Christmas_tree");
			this.EffectSphereModel = new ModelTransformedScene(UWModel.CreateAll(Materials.TexturesDatabase, CS$<>8__locals1.contentManager.Load<Model>(PathContent.dir_models + "explosionSphereEffect")), new Transform3D())
			{
				VisibleTestType = ModelSceneVisibleTest.ForAllScene
			};
			Tlist<UWModel> tlist2 = CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("Bouys");
			this.Buoys = new ModelTransformedScene[tlist2.Size];
			for (int j = 0; j < tlist2.Size; j++)
			{
				this.Buoys[j] = new ModelTransformedScene(tlist2.Array[j], new Transform3D())
				{
					VisibleTestType = ModelSceneVisibleTest.Disable
				};
			}
			this.CircleHoled = UWModel.CreateAll(null, CS$<>8__locals1.contentManager.Load<Model>(PathContent.dir_models + "circleHoled"));
			this.Bird = CS$<>8__locals1.<.ctor>g__LoadModel|0("bird");
			this.Fish = CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("fish").ToArray();
			this.UnitsSailor = (from {25097} in new int[]
			{
				1,
				2
			}
			select base.<.ctor>g__LoadModel|0("people\\Sailor_" + {25097}.ToString())).ToArray<UWModel>();
			this.UnitsOfficer = (from {25098} in new int[]
			{
				1,
				2
			}
			select base.<.ctor>g__LoadModel|0("people\\Officer_" + {25098}.ToString())).ToArray<UWModel>();
			this.UnitsCannonier = (from {25099} in new int[]
			{
				1,
				2
			}
			select base.<.ctor>g__LoadModel|0("people\\Cannonier_" + {25099}.ToString())).ToArray<UWModel>();
			UWModel uwmodel = CS$<>8__locals1.<.ctor>g__LoadModel|0("people_port\\Arab_male");
			UWModel uwmodel2 = CS$<>8__locals1.<.ctor>g__LoadModel|0("people_port\\Arab_female");
			UWModel uwmodel3 = CS$<>8__locals1.<.ctor>g__LoadModel|0("people_port\\Asian_male");
			UWModel uwmodel4 = CS$<>8__locals1.<.ctor>g__LoadModel|0("people_port\\Asian_female");
			UWModel uwmodel5 = CS$<>8__locals1.<.ctor>g__LoadModel|0("people_port\\Europe_male");
			UWModel uwmodel6 = CS$<>8__locals1.<.ctor>g__LoadModel|0("people_port\\Europe_female");
			UWModel uwmodel7 = CS$<>8__locals1.<.ctor>g__LoadModel|0("people_port\\North_male");
			UWModel uwmodel8 = CS$<>8__locals1.<.ctor>g__LoadModel|0("people_port\\North_female");
			UWModel uwmodel9 = CS$<>8__locals1.<.ctor>g__LoadModel|0("people_port\\Spain_male");
			UWModel uwmodel10 = CS$<>8__locals1.<.ctor>g__LoadModel|0("people_port\\Spain_female");
			this.ArabUnits = new UWModel[]
			{
				uwmodel,
				uwmodel,
				uwmodel2
			};
			this.AsainUnits = new UWModel[]
			{
				uwmodel3,
				uwmodel3,
				uwmodel4
			};
			this.EuropeUnits = new UWModel[]
			{
				uwmodel5,
				uwmodel5,
				uwmodel6
			};
			this.NorthUnits = new UWModel[]
			{
				uwmodel7,
				uwmodel7,
				uwmodel8
			};
			this.SpainUnits = new UWModel[]
			{
				uwmodel9,
				uwmodel9,
				uwmodel10
			};
			this.AllPortUnits = this.ArabUnits.Concat(this.AsainUnits).Concat(this.EuropeUnits).Concat(this.NorthUnits).Concat(this.SpainUnits).ToArray<UWModel>();
			this.AllShipUnits = this.UnitsSailor.Concat(this.UnitsOfficer).Concat(this.UnitsCannonier).ToArray<UWModel>();
			foreach (UWModel uwmodel11 in this.AllShipUnits.Concat(this.AllPortUnits))
			{
				Dictionary<UnitAnimation, AnimationClip> dictionary = new Dictionary<UnitAnimation, AnimationClip>();
				foreach (object obj2 in Enum.GetValues(typeof(UnitAnimation)))
				{
					UnitAnimation unitAnimation = (UnitAnimation)obj2;
					foreach (KeyValuePair<string, AnimationClip> keyValuePair in uwmodel11.SkinningDataOrNull.AnimationClips)
					{
						if (keyValuePair.Key.Contains(unitAnimation.ToString()) || (keyValuePair.Key.EndsWith("Idle") && unitAnimation == UnitAnimation.Idle_1))
						{
							dictionary.Add(unitAnimation, keyValuePair.Value);
						}
					}
				}
				uwmodel11.Tag = dictionary;
			}
			Model {15201} = CS$<>8__locals1.contentManager.Load<Model>(PathContent.dir_models + "map");
			this.WorldMap = new UWModel[3];
			this.WorldMap[0] = UWModel.Separate(Materials.TexturesDatabase, {15201}, "Static").Array[0];
			this.WorldMap[1] = UWModel.Separate(Materials.TexturesDatabase, {15201}, "MapMesh").Array[0];
			this.WorldMap[2] = UWModel.Separate(Materials.TexturesDatabase, {15201}, "FogMesh").Array[0];
			this.MapClocks = CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("Clocks2").ToArray();
			this.MapClocksEng = CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("Clocks2_eng").ToArray();
			this.DropdecorPassing = CS$<>8__locals1.<.ctor>g__LoadModel|0("dropdecorPassing");
			this.Flag = CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("flag");
			this.Shallow = new ModelTransformedScene(UWModel.CreateAll(Materials.TexturesDatabase, CS$<>8__locals1.contentManager.Load<Model>(PathContent.dir_models + "shallow")), new Transform3D())
			{
				VisibleTestType = ModelSceneVisibleTest.Disable
			};
			this.Cannons = new Dictionary<int, UWModel>();
			this.CannonsLod = new Dictionary<int, UWModel>();
			Tlist<UWModel> tlist3 = CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("weapons");
			using (IEnumerator<CannonGameInfo> enumerator4 = ((IEnumerable<CannonGameInfo>)Gameplay.CannonsGameInfo).GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					CannonGameInfo item = enumerator4.Current;
					if (!string.IsNullOrEmpty(item.Model))
					{
						UWModel uwmodel12 = tlist3.Find((UWModel {25100}) => {25100}.MeshName == item.Model);
						UWModel uwmodel13 = tlist3.Find((UWModel {25101}) => {25101}.MeshName == item.Model + "_lod");
						Dictionary<int, UWModel> cannons = this.Cannons;
						int id = (int)item.ID;
						UWModel uwmodel14 = uwmodel12;
						if (uwmodel14 == null)
						{
							throw new KeyNotFoundException("weapons.fbx must have " + item.Model);
						}
						cannons.Add(id, uwmodel14);
						this.CannonsLod.Add((int)item.ID, uwmodel13 ?? uwmodel12);
					}
				}
			}
			this.MortarUpgradeExtraLafet = tlist3.First((UWModel {25093}) => {25093}.MeshName == "mortupgrade");
			Tlist<UWModel> source = CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("BowFigures");
			using (IEnumerator<ShipDesignInfo> enumerator5 = (from {25094} in Gameplay.DesignsInfo
			where {25094}.Category == ShipDesignCategory.BowFigure
			select {25094}).GetEnumerator())
			{
				while (enumerator5.MoveNext())
				{
					ShipDesignInfo item = enumerator5.Current;
					item.ApartModel = source.FirstOrDefault((UWModel {25102}) => {25102}.MeshName == item.nameKey);
					if (item.ApartModel == null)
					{
						throw new KeyNotFoundException("There's not bow figure for key " + item.nameKey + " in BowFigures.fbx");
					}
				}
			}
			Tlist<UWModel> source2 = CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("EnvDecorElements");
			using (IEnumerator<ShipDesignInfo> enumerator5 = ((IEnumerable<ShipDesignInfo>)Gameplay.EnvDecorElementsInfo).GetEnumerator())
			{
				while (enumerator5.MoveNext())
				{
					ShipDesignInfo item = enumerator5.Current;
					item.ApartModel = source2.FirstOrDefault((UWModel {25103}) => {25103}.MeshName == item.nameKey);
					if (item.ApartModel == null)
					{
						throw new KeyNotFoundException("There's not envdecor for key " + item.nameKey + " in EnvDecorElements.fbx");
					}
				}
			}
			this.TESTProceduralModels = CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("TESTProceduralModels").Concat(CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("TESTProceduralFlora")).ToArray<UWModel>();
			foreach (ShipDesignInfo shipDesignInfo in ((IEnumerable<ShipDesignInfo>)Gameplay.DesignsInfo))
			{
				if (shipDesignInfo.Texture.Length > 0 && shipDesignInfo.Category != ShipDesignCategory.ShipFullDesign && Materials.TexturesDatabase[shipDesignInfo.Texture] == null && (shipDesignInfo.Category != ShipDesignCategory.SailTexture || LocalContent.GetDecalForSail(shipDesignInfo, null) == null))
				{
					throw new KeyNotFoundException("There's no texture for design " + shipDesignInfo.Texture);
				}
			}
			this.FloatingVerfy = CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("floatingVerfy").ToArray();
			this.IsleLootingDecoration = CS$<>8__locals1.<.ctor>g__LoadModel|0("isleLootingDeco");
			this.CoastWreckage = CS$<>8__locals1.<.ctor>g__LoadModelMeshes|1("coastWreckage");
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x000FA510 File Offset: 0x000F8710
		private IEnumerable<DropModelData> loadDropMeshes(ContentManager {25088}, DropModel {25089})
		{
			LocalContent.<loadDropMeshes>d__67 <loadDropMeshes>d__ = new LocalContent.<loadDropMeshes>d__67(-2);
			<loadDropMeshes>d__.<>4__this = this;
			<loadDropMeshes>d__.<>3__contentManager = {25088};
			<loadDropMeshes>d__.<>3__name = {25089};
			return <loadDropMeshes>d__;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x000FA52E File Offset: 0x000F872E
		private IEnumerable<DropModelData> FindModels(ContentManager {25090}, string {25091})
		{
			LocalContent.<FindModels>d__68 <FindModels>d__ = new LocalContent.<FindModels>d__68(-2);
			<FindModels>d__.<>3__contentManager = {25090};
			<FindModels>d__.<>3__nameKey = {25091};
			return <FindModels>d__;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x000FA548 File Offset: 0x000F8748
		// Note: this type is marked as 'beforefieldinit'.
		static LocalContent()
		{
			LinkedDictionrary<uint, int> linkedDictionrary = new LinkedDictionrary<uint, int>();
			linkedDictionrary[423U] = new ObservableTlist<int>(new int[]
			{
				73,
				82,
				83,
				118,
				59
			});
			linkedDictionrary[75U] = new ObservableTlist<int>(new int[]
			{
				82,
				83
			});
			linkedDictionrary[154U] = new ObservableTlist<int>(new int[]
			{
				141
			});
			linkedDictionrary[177U] = new ObservableTlist<int>(new int[]
			{
				142,
				143
			});
			linkedDictionrary[319U] = new ObservableTlist<int>(new int[]
			{
				142,
				143
			});
			linkedDictionrary[144U] = new ObservableTlist<int>(new int[]
			{
				150,
				151,
				208
			});
			linkedDictionrary[169U] = new ObservableTlist<int>(new int[]
			{
				177,
				178
			});
			linkedDictionrary[163U] = new ObservableTlist<int>(new int[]
			{
				180,
				184
			});
			linkedDictionrary[1640U] = new ObservableTlist<int>(new int[]
			{
				180,
				184
			});
			linkedDictionrary[1521U] = new ObservableTlist<int>(new int[]
			{
				182,
				183,
				63
			});
			linkedDictionrary[324U] = new ObservableTlist<int>(new int[]
			{
				194,
				83,
				209,
				362
			});
			linkedDictionrary[173U] = new ObservableTlist<int>(new int[]
			{
				200,
				83
			});
			linkedDictionrary[242U] = new ObservableTlist<int>(new int[]
			{
				212,
				213,
				83,
				312
			});
			linkedDictionrary[247U] = new ObservableTlist<int>(new int[]
			{
				224,
				225,
				83
			});
			linkedDictionrary[279U] = new ObservableTlist<int>(new int[]
			{
				52,
				83
			});
			linkedDictionrary[304U] = new ObservableTlist<int>(new int[]
			{
				266,
				267,
				83
			});
			linkedDictionrary[448U] = new ObservableTlist<int>(new int[]
			{
				266,
				267,
				83
			});
			linkedDictionrary[344U] = new ObservableTlist<int>(new int[]
			{
				282,
				83,
				304
			});
			linkedDictionrary[308U] = new ObservableTlist<int>(new int[]
			{
				298,
				299,
				83
			});
			linkedDictionrary[362U] = new ObservableTlist<int>(new int[]
			{
				298,
				299,
				83
			});
			linkedDictionrary[645U] = new ObservableTlist<int>(new int[]
			{
				295,
				305,
				245,
				246,
				247,
				43
			});
			linkedDictionrary[352U] = new ObservableTlist<int>(new int[]
			{
				295
			});
			linkedDictionrary[370U] = new ObservableTlist<int>(new int[]
			{
				296,
				297
			});
			linkedDictionrary[356U] = new ObservableTlist<int>(new int[]
			{
				300,
				301
			});
			linkedDictionrary[334U] = new ObservableTlist<int>(new int[]
			{
				302,
				303,
				83
			});
			linkedDictionrary[346U] = new ObservableTlist<int>(new int[]
			{
				242
			});
			linkedDictionrary[400U] = new ObservableTlist<int>(new int[]
			{
				309,
				310,
				83
			});
			linkedDictionrary[318U] = new ObservableTlist<int>(new int[]
			{
				311
			});
			linkedDictionrary[0U] = new ObservableTlist<int>(new int[]
			{
				199,
				138,
				140,
				280,
				281,
				226
			});
			LocalContent.GuildDesigns = linkedDictionrary;
			Dictionary<FractionID, ValueTuple<int, int>> dictionary = new Dictionary<FractionID, ValueTuple<int, int>>();
			dictionary[FractionID.Espaniol] = new ValueTuple<int, int>(437, 438);
			dictionary[FractionID.KaiAndSeveria] = new ValueTuple<int, int>(439, 440);
			dictionary[FractionID.Antilia] = new ValueTuple<int, int>(441, 442);
			dictionary[FractionID.TradeUnion] = new ValueTuple<int, int>(443, 444);
			dictionary[FractionID.Empire] = new ValueTuple<int, int>(445, -1);
			LocalContent.Geraldics = dictionary;
		}

		// Token: 0x04001A23 RID: 6691
		public static readonly LinkedDictionrary<uint, int> GuildDesigns;

		// Token: 0x04001A24 RID: 6692
		[TupleElementNames(new string[]
		{
			"flag",
			"sail"
		})]
		public static readonly Dictionary<FractionID, ValueTuple<int, int>> Geraldics;

		// Token: 0x04001A25 RID: 6693
		internal static LocalContent Loaded;

		// Token: 0x04001A26 RID: 6694
		public readonly UWModel[] FloatingVerfy;

		// Token: 0x04001A27 RID: 6695
		public readonly UWModel IsleLootingDecoration;

		// Token: 0x04001A28 RID: 6696
		public readonly UWModel WaterFrame;

		// Token: 0x04001A29 RID: 6697
		public readonly UWModel WaterFrame_TEST;

		// Token: 0x04001A2A RID: 6698
		public readonly Dictionary<HitMaterialEffect, UWModel[]> AllDebris;

		// Token: 0x04001A2B RID: 6699
		public readonly UserMesh Effect3;

		// Token: 0x04001A2C RID: 6700
		public readonly UWModel[] Falkonet1;

		// Token: 0x04001A2D RID: 6701
		public readonly UWModel MortarUpgradeExtraLafet;

		// Token: 0x04001A2E RID: 6702
		public readonly Dictionary<DropModel, Tlist<DropModelData>> DropModels;

		// Token: 0x04001A2F RID: 6703
		public readonly Tlist<DropModelData> IcedShipsDropModels;

		// Token: 0x04001A30 RID: 6704
		public readonly UWModel[] BoardingHook;

		// Token: 0x04001A31 RID: 6705
		public readonly ModelTransformedScene SightLine;

		// Token: 0x04001A32 RID: 6706
		public readonly UWModel DebugBoxShapeDisplay;

		// Token: 0x04001A33 RID: 6707
		public readonly ModelTransformedScene DebugSphereDisplay;

		// Token: 0x04001A34 RID: 6708
		public readonly ModelTransformedScene[] powderKegPackByModelNumber;

		// Token: 0x04001A35 RID: 6709
		public readonly UWModel NeedleModel;

		// Token: 0x04001A36 RID: 6710
		public readonly UWModel ShipBigLamp;

		// Token: 0x04001A37 RID: 6711
		public readonly ModelTransformedScene EffectSphereModel;

		// Token: 0x04001A38 RID: 6712
		public readonly ModelTransformedScene[] Buoys;

		// Token: 0x04001A39 RID: 6713
		public readonly UWModel CircleHoled;

		// Token: 0x04001A3A RID: 6714
		public readonly UWModel Bird;

		// Token: 0x04001A3B RID: 6715
		public readonly UWModel Whale;

		// Token: 0x04001A3C RID: 6716
		public readonly UWModel SpermWhale;

		// Token: 0x04001A3D RID: 6717
		public readonly UWModel WhaleOrca;

		// Token: 0x04001A3E RID: 6718
		public readonly UWModel[] Fish;

		// Token: 0x04001A3F RID: 6719
		public readonly UWModel[] UnitsSailor;

		// Token: 0x04001A40 RID: 6720
		public readonly UWModel[] UnitsCannonier;

		// Token: 0x04001A41 RID: 6721
		public readonly UWModel[] UnitsOfficer;

		// Token: 0x04001A42 RID: 6722
		public readonly UWModel[] AllShipUnits;

		// Token: 0x04001A43 RID: 6723
		public readonly UWModel[] WorldMap;

		// Token: 0x04001A44 RID: 6724
		public readonly UWModel[] ArabUnits;

		// Token: 0x04001A45 RID: 6725
		public readonly UWModel[] AsainUnits;

		// Token: 0x04001A46 RID: 6726
		public readonly UWModel[] EuropeUnits;

		// Token: 0x04001A47 RID: 6727
		public readonly UWModel[] NorthUnits;

		// Token: 0x04001A48 RID: 6728
		public readonly UWModel[] SpainUnits;

		// Token: 0x04001A49 RID: 6729
		public readonly UWModel[] AllPortUnits;

		// Token: 0x04001A4A RID: 6730
		public readonly ModelTransformedScene Shallow;

		// Token: 0x04001A4B RID: 6731
		public readonly UWModel DropdecorPassing;

		// Token: 0x04001A4C RID: 6732
		public readonly Tlist<UWModel> Flag;

		// Token: 0x04001A4D RID: 6733
		public readonly Tlist<UWModel> CoastWreckage;

		// Token: 0x04001A4E RID: 6734
		public readonly ModelTransformedScene WhaleHook;

		// Token: 0x04001A4F RID: 6735
		public readonly UWModel[] MapClocks;

		// Token: 0x04001A50 RID: 6736
		public readonly UWModel[] MapClocksEng;

		// Token: 0x04001A51 RID: 6737
		public readonly Tlist<UWModel> Flagpoles;

		// Token: 0x04001A52 RID: 6738
		public readonly UWModel NewYearTree;

		// Token: 0x04001A53 RID: 6739
		public readonly UWModel[] TESTProceduralModels;

		// Token: 0x04001A54 RID: 6740
		public readonly Dictionary<int, UWModel> Cannons;

		// Token: 0x04001A55 RID: 6741
		public readonly Dictionary<int, UWModel> CannonsLod;
	}
}
