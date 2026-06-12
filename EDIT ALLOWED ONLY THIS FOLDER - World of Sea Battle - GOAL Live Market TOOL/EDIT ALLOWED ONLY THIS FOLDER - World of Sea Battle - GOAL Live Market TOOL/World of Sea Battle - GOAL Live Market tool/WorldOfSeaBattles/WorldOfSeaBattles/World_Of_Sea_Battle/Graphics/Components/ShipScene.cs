using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
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
using TheraEngine.Graphics;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;
using TheraEngine.PacketValues;
using TheraEngine.Scene;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics.Shaders;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x0200047C RID: 1148
	internal sealed class ShipScene
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x000DAB3C File Offset: 0x000D8D3C
		private bool crewVisible
		{
			get
			{
				return Global.Settings.CrewMode == LocalSettings.CrewDisplayMode.AllShips || (Global.Settings.CrewMode == LocalSettings.CrewDisplayMode.OnlyMyShip && this.{24026} is ShipCurrentPlayer) || {18139}.IsInBoardingUi(this.{24026}.uID);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x000DAB77 File Offset: 0x000D8D77
		public CrewConnector CrewInstancesByPlaces
		{
			get
			{
				return this.{24058};
			}
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x000DAB80 File Offset: 0x000D8D80
		static ShipScene()
		{
			for (int i = 0; i < ShipScene.randValues.Length; i++)
			{
				ShipScene.randValues[i] = HashHelper.greater(32235 + i);
			}
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000DABD8 File Offset: 0x000D8DD8
		public ShipScene()
		{
			this.{24037} = new ModelTransformedScene
			{
				VisibleTestType = ModelSceneVisibleTest.Disable
			};
			this.{24038} = new ModelTransformedScene
			{
				VisibleTestType = ModelSceneVisibleTest.Disable
			};
			this.{24029} = new ModelTransformedScene
			{
				VisibleTestType = ModelSceneVisibleTest.Disable
			};
			this.{24030} = new ModelTransformedScene
			{
				VisibleTestType = ModelSceneVisibleTest.Disable
			};
			this.{24033} = new ModelTransformedScene
			{
				VisibleTestType = ModelSceneVisibleTest.Disable
			};
			this.{24034} = new ModelTransformedScene
			{
				VisibleTestType = ModelSceneVisibleTest.Disable
			};
			this.{24035} = new ModelTransformedScene
			{
				VisibleTestType = ModelSceneVisibleTest.Disable
			};
			this.{24059} = new ModelTransformedScene
			{
				VisibleTestType = ModelSceneVisibleTest.Disable
			};
			this.{24039} = new ModelTransformedScene
			{
				VisibleTestType = ModelSceneVisibleTest.Disable
			};
			this.{24040} = new ModelTransformedScene
			{
				VisibleTestType = ModelSceneVisibleTest.Disable
			};
			this.{24041} = new ModelTransformedScene
			{
				VisibleTestType = ModelSceneVisibleTest.Disable
			};
			this.{24042} = new Tlist<ModelRenderer>();
			this.{24043} = new Tlist<ModelRenderer>();
			this.{24044} = new Tlist<ModelRenderer>();
			this.{24045} = new Tlist<bool>();
			this.{24046} = new Tlist<ShipScene.FalkonetRender>();
			this.{24047} = new Tlist<ShipScene.MortarRenderer>();
			this.{24048} = new Tlist<ModelRenderer>();
			this.{24049} = new Tlist<ModelRenderer>();
			this.{24050} = new Tlist<ModelRenderer>();
			this.{24060} = new InstancedModelRenderer();
			this.{24058} = new CrewConnector();
			this.{24057} = new Tlist<UnitScene>();
			this.{24036} = new Tlist<VirtualTexture>();
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000DAD68 File Offset: 0x000D8F68
		public void CleanResources()
		{
			this.{24058}.Clean();
			this.{24057}.Clear();
			this.{24063}.Clear();
			this.{24042}.Clear();
			this.{24043}.Clear();
			this.{24044}.Clear();
			this.{24045}.Clear();
			this.{24037}.Clear();
			this.{24038}.Clear();
			this.{24039}.Clear();
			this.{24040}.Clear();
			this.{24029}.Clear();
			this.{24030}.Clear();
			this.{24033}.Clear();
			this.{24034}.Clear();
			this.{24035}.Clear();
			this.{24036}.Clear();
			this.{24049}.Clear();
			this.{24041}.Clear();
			this.{24051} = null;
			this.{24052} = null;
			this.{24053} = null;
			this.{24048}.Clear();
			this.{24059}.Clear();
			this.{24046}.Clear();
			this.{24047}.Clear();
			this.{24064} = false;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x000DAE90 File Offset: 0x000D9090
		public void UpdateCustomPositions(Ship {23995})
		{
			if (this.{24052} != null)
			{
				Transform3D localTransformOrNull = this.{24052}.LocalTransformOrNull;
				Vector3 bowFigurePosition = {23995}.UsedShip.StaticInfo.BowFigurePosition;
				ShipPlayerBase shipPlayerBase = {23995} as ShipPlayerBase;
				localTransformOrNull.Translation = bowFigurePosition + ((shipPlayerBase != null) ? new Vector3(shipPlayerBase.UsedShipPlayer.BowFigurePosition.X, shipPlayerBase.UsedShipPlayer.BowFigurePosition.Y, 0f) : Vector3.Zero);
				Transform3D localTransformOrNull2 = this.{24052}.LocalTransformOrNull;
				Vector3 value = new Vector3(0.15f + 0.6f * {23995}.UsedShip.StaticInfo.CorpusHalfLength / Gameplay.ShipsStaticInfo.FromID(4).CorpusHalfLength);
				ShipPlayerBase shipPlayerBase2 = {23995} as ShipPlayerBase;
				localTransformOrNull2.Scales = value * ((shipPlayerBase2 != null) ? shipPlayerBase2.UsedShipPlayer.DesignBowFigureScale : 1f);
			}
			if (this.{24053} != null)
			{
				ShipPlayerBase shipPlayerBase3 = {23995} as ShipPlayerBase;
				if (shipPlayerBase3 != null)
				{
					this.{24053}.LocalTransformOrNull.Translation = new Vector3(shipPlayerBase3.UsedShipPlayer.BigLampPosition, 0f);
				}
			}
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x000DAFA4 File Offset: 0x000D91A4
		public void UpdateModelData(Ship {23996})
		{
			ShipScene.<>c__DisplayClass54_0 CS$<>8__locals1 = new ShipScene.<>c__DisplayClass54_0();
			CS$<>8__locals1.<>4__this = this;
			if (this.{24064})
			{
				this.CleanResources();
			}
			this.{24064} = true;
			this.{24026} = {23996};
			CS$<>8__locals1.shipInfo = {23996}.UsedShip.StaticInfo;
			this.{24037}.Transform = {23996}.Transform;
			this.{24038}.Transform = {23996}.Transform;
			this.{24039}.Transform = {23996}.Transform;
			this.{24040}.Transform = {23996}.Transform;
			this.{24029}.Transform = {23996}.Transform;
			this.{24030}.Transform = {23996}.Transform;
			this.{24033}.Transform = {23996}.Transform;
			this.{24034}.Transform = {23996}.Transform;
			this.{24035}.Transform = {23996}.Transform;
			this.{24041}.Transform = {23996}.Transform;
			this.{24027} = CS$<>8__locals1.shipInfo.Model.corpus;
			this.{24028} = CS$<>8__locals1.shipInfo.Model.corpusLow;
			Tlist<UWModel> arris = CS$<>8__locals1.shipInfo.Model.arris;
			Tlist<UWModel> mastRotateFrames = CS$<>8__locals1.shipInfo.Model.mastRotateFrames;
			UWModel unallocatdRoops = CS$<>8__locals1.shipInfo.Model.unallocatdRoops;
			Tlist<UWModel> flagpolls = CS$<>8__locals1.shipInfo.Model.flagpolls;
			Tlist<ShipModelMainFlag> mainFlags = CS$<>8__locals1.shipInfo.Model.mainFlags;
			float x = CS$<>8__locals1.shipInfo.CorpusShape.LocalCenter.X;
			float finalLength = CS$<>8__locals1.shipInfo.CorpusShape.FinalLength;
			float num = x - finalLength * 0.5f * 0.9f;
			this.{24059}.AddObject(LocalContent.Loaded.Cannons[1]);
			List<string> list = new List<string>
			{
				"Pickle",
				"La Salamandre",
				"Red Arrow",
				"Black Prince",
				"Devourer",
				"Falmouth",
				"Eagle",
				"La Creole",
				"La Sirene",
				"Neptuno",
				"Bellona",
				"Sovereign",
				"Ingermanland",
				"Poltava",
				"Surprise",
				"San Martin",
				"De Zeven Provincien",
				"Savannah"
			};
			this.{24037}.AddObject(this.{24027});
			if ((CalendarEvents.IsHalloween || CalendarEvents.IsNewYear) && !list.Contains({23996}.CraftFrom.ShipName))
			{
				foreach (Matrix matrix in ((IEnumerable<Matrix>)CS$<>8__locals1.shipInfo.SmallLights))
				{
					ModelRenderer modelRenderer = new ModelRenderer(CalendarEvents.IsHalloween ? LocalContent.Loaded.DropModels[DropModel.Pumkin][0].FloatingParts.First() : LocalContent.Loaded.NewYearTree);
					modelRenderer.LocalTransformOrNull = new Transform3D
					{
						Translation = matrix.Translation,
						MiddleScale = 0.15f * (0.4f + 0.6f * (3f - CS$<>8__locals1.shipInfo.InertionFactor))
					};
					float num2 = 3.1415927f;
					if (matrix.Translation.X < num)
					{
						num2 += 3.1415927f;
					}
					modelRenderer.LocalTransformOrNull.Yaw += num2;
					this.{24037}.AddObject(modelRenderer, true);
				}
			}
			this.{24038}.AddObject(unallocatdRoops);
			for (int i = 0; i < mastRotateFrames.Size; i++)
			{
				ModelRenderer {11959} = new ModelRenderer(mastRotateFrames.Array[i])
				{
					LocalTransformOrNull = new Transform3D()
				};
				this.{24038}.AddObject({11959}, true);
				this.{24039}.AddObject({11959}, true);
				this.{24044}.Add({11959});
				Tlist<bool> tlist = this.{24045};
				bool flag = false;
				tlist.Add(flag);
				this.{24043}.Add({11959});
			}
			this.{24038}.AddObject(CS$<>8__locals1.shipInfo.Model.masts);
			this.{24039}.AddObject(CS$<>8__locals1.shipInfo.Model.masts);
			this.{24040}.AddObject(CS$<>8__locals1.shipInfo.Model.masts);
			this.{24031} = new int[CS$<>8__locals1.shipInfo.SailHitboxes.Length];
			this.{24032} = new SailLocalRenderQuery[CS$<>8__locals1.shipInfo.SailHitboxes.Length];
			for (int j = 0; j < CS$<>8__locals1.shipInfo.SailHitboxes.Length; j++)
			{
				SailHitbox sailHitbox = CS$<>8__locals1.shipInfo.SailHitboxes[j];
				this.{24031}[j] = sailHitbox.SailStrengthIndex;
				this.{24032}[j] = new SailLocalRenderQuery();
				if (sailHitbox.IsSailAnimated)
				{
					this.{24061} = j;
					ModelRenderer modelRenderer2 = new ModelRenderer(sailHitbox.Model)
					{
						LocalTransformOrNull = new Transform3D()
					};
					this.{24029}.AddObject(modelRenderer2, true);
					if (sailHitbox.ModelRolled != null)
					{
						this.{24030}.AddObject(new ModelRenderer(sailHitbox.ModelRolled)
						{
							LocalTransformOrNull = modelRenderer2.LocalTransformOrNull
						}, true);
					}
					this.{24032}[j].RollData = ((AnimatedModelTag)sailHitbox.Model.Tag).SailInfo;
					this.{24044}.Add(modelRenderer2);
					Tlist<bool> tlist2 = this.{24045};
					bool flag = sailHitbox.Model.MeshParts[0].LocalSpaceBoundingBox.Max.X - sailHitbox.Model.MeshParts[0].LocalSpaceBoundingBox.Min.X < sailHitbox.Model.MeshParts[0].LocalSpaceBoundingBox.Max.Z - sailHitbox.Model.MeshParts[0].LocalSpaceBoundingBox.Min.Z;
					tlist2.Add(flag);
				}
				else
				{
					this.{24029}.AddObject(sailHitbox.Model);
					if (sailHitbox.ModelRolled != null)
					{
						this.{24030}.AddObject(new ModelRenderer(sailHitbox.ModelRolled), true);
					}
					this.{24032}[j].RollData = ((AnimatedModelTag)sailHitbox.Model.Tag).SailInfo;
				}
			}
			ShipPlayerBase shipPlayerBase = {23996} as ShipPlayerBase;
			ShipDesignInfo shipDesignInfo = ((shipPlayerBase != null) ? (shipPlayerBase.Client.GetPreview(ShipDesignCategory.BowFigure) ?? shipPlayerBase.UsedShipPlayer.GetDesignElement(5)) : ({23996} as ShipNpc).UsedShipNpc.BowFigure) ?? CS$<>8__locals1.shipInfo.PreinstalledBowFigure;
			if (shipDesignInfo != null && shipDesignInfo.ApartModel != null)
			{
				this.{24052} = new ModelRenderer(shipDesignInfo.ApartModel);
				this.{24052}.LocalTransformOrNull = new Transform3D();
				this.UpdateCustomPositions({23996});
				this.{24037}.AddObject(this.{24052}, true);
			}
			ShipPlayerBase shipPlayerBase2 = {23996} as ShipPlayerBase;
			if (shipPlayerBase2 != null)
			{
				ShipDesignInfo shipDesignInfo2 = shipPlayerBase2.Client.GetPreview(ShipDesignCategory.Satellite) ?? shipPlayerBase2.UsedShipPlayer.GetDesignElement(2);
				if (shipDesignInfo2 != null)
				{
					this.{24053} = new ModelRenderer(LocalContent.Loaded.ShipBigLamp);
					this.{24053}.LocalTransformOrNull = new Transform3D();
					this.UpdateCustomPositions({23996});
					this.{24037}.AddObject(this.{24053}, true);
					shipPlayerBase2.Client.CreateLampLight(shipDesignInfo2);
				}
				else
				{
					shipPlayerBase2.Client.CreateLampLight(null);
				}
			}
			if (CS$<>8__locals1.shipInfo.Model.wheelSteamShip != null)
			{
				this.{24054} = new ModelRenderer(CS$<>8__locals1.shipInfo.Model.wheelSteamShip)
				{
					LocalTransformOrNull = new Transform3D()
				};
				this.{24054}.Tag = CS$<>8__locals1.shipInfo.Model.steamWheelCenterPoint.CommonSphere.Center;
				this.{24037}.AddObject(this.{24054}, true);
			}
			for (int k = 0; k < arris.Size; k++)
			{
				ModelRenderer {11959}2 = new ModelRenderer(arris.Array[k])
				{
					LocalTransformOrNull = new Transform3D()
				};
				this.{24037}.AddObject({11959}2, true);
				this.{24042}.Add({11959}2);
			}
			for (int l = 0; l < flagpolls.Size; l++)
			{
				UWModel uwmodel = flagpolls[l];
				UWModel uwmodel2 = LocalContent.Loaded.Flagpoles.FindNear(ShipScene.<UpdateModelData>g__Ratio|54_3(uwmodel), new Func<UWModel, float>(ShipScene.<UpdateModelData>g__Ratio|54_3));
				float num3 = uwmodel2.MeshParts.First<MeshPartData>().LocalSpaceBoundingBox.Size.X - uwmodel.MeshParts.First<MeshPartData>().LocalSpaceBoundingBox.Size.X;
				num3 = ((num3 > 0f) ? (1f - num3 / uwmodel2.MeshParts.First<MeshPartData>().LocalSpaceBoundingBox.Size.X * 0.7f) : 1f);
				num3 = MathHelper.Lerp(num3, 1f, 2f - {23996}.UsedShip.StaticInfo.InertionFactor);
				Vector3 translation = new Vector3(uwmodel.MeshParts[0].LocalSpaceBoundingBox.Min.X, uwmodel.CommonSphere.Center.Y, uwmodel.CommonSphere.Center.Z);
				ModelRenderer {11959}3 = new ModelRenderer(uwmodel2)
				{
					LocalTransformOrNull = new Transform3D
					{
						Translation = translation,
						MiddleScale = 0.6f * num3
					}
				};
				this.{24048}.Add({11959}3);
				if (uwmodel.Drawcalls[0].Material.Albedo.AssetName == "Flagpoll2")
				{
					this.{24034}.AddObject({11959}3, true);
				}
				else
				{
					this.{24033}.AddObject({11959}3, true);
				}
			}
			for (int m = 0; m < CS$<>8__locals1.shipInfo.FalkonetPositions.Length; m++)
			{
				Tlist<ShipScene.FalkonetRender> tlist3 = this.{24046};
				ShipScene.FalkonetRender falkonetRender = new ShipScene.FalkonetRender(CS$<>8__locals1.shipInfo.FalkonetPositions[m]);
				tlist3.Add(falkonetRender);
			}
			foreach (CannonLocationInfo cannonLocationInfo in {23996}.UsedShip.StaticInfo.MortarPorts)
			{
				CannonGameInstance cannonGameInstance = {23996}.UsedShip.Mortars[(int)cannonLocationInfo.SectionID];
				if (cannonGameInstance != null)
				{
					Tlist<ShipScene.MortarRenderer> tlist4 = this.{24047};
					ShipScene.MortarRenderer mortarRenderer = new ShipScene.MortarRenderer(cannonLocationInfo, cannonGameInstance.Info, cannonLocationInfo.AvailableWithUpgrade);
					tlist4.Add(mortarRenderer);
				}
				else if (cannonLocationInfo.AvailableWithUpgrade && {23996}.UsedShip.HasExtraMortarUpgrade)
				{
					Tlist<ShipScene.MortarRenderer> tlist5 = this.{24047};
					ShipScene.MortarRenderer mortarRenderer = new ShipScene.MortarRenderer(cannonLocationInfo, null, cannonLocationInfo.AvailableWithUpgrade);
					tlist5.Add(mortarRenderer);
				}
			}
			ShipPlayerBase shipPlayerBase3 = {23996} as ShipPlayerBase;
			if (shipPlayerBase3 != null)
			{
				for (int num4 = 0; num4 < mainFlags.Size; num4++)
				{
					ShipDesignInfo shipDesignInfo3;
					if (num4 == 0)
					{
						shipDesignInfo3 = (shipPlayerBase3.Client.GetPreview(ShipDesignCategory.Flag) ?? shipPlayerBase3.UsedShipPlayer.GetDesignElement(0));
					}
					else
					{
						shipDesignInfo3 = shipPlayerBase3.UsedShipPlayer.GetDesignElement(7);
					}
					if (shipDesignInfo3 != null)
					{
						CS$<>8__locals1.<UpdateModelData>g__addFlag|0(mainFlags.Array[num4].FixedToBackFrame, mainFlags.Array[num4].SrcModel, LocalContent.GetShipFlagTexture(shipDesignInfo3));
					}
				}
			}
			else
			{
				ShipNpc shipNpc = (ShipNpc){23996};
				VirtualTexture npcFlag = LocalContent.GetNpcFlag(shipNpc);
				if (npcFlag != null)
				{
					for (int num5 = 0; num5 < mainFlags.Size; num5++)
					{
						CS$<>8__locals1.<UpdateModelData>g__addFlag|0(mainFlags.Array[num5].FixedToBackFrame, mainFlags.Array[num5].SrcModel, npcFlag);
						if (!shipNpc.UsedShipNpc.Information.Extras.IsReinforcedType && !shipNpc.UsedShipNpc.Information.Extras.IsEmpire)
						{
							break;
						}
					}
				}
			}
			this.{24060}.CleanCache();
			this.{24058}.Initialize({23996}, delegate(UnitScene {24080})
			{
				CS$<>8__locals1.<>4__this.{24057}.Add({24080});
				CS$<>8__locals1.<>4__this.{24041}.AddObject({24080}.Model, true);
			}, delegate(UnitScene {24081})
			{
				CS$<>8__locals1.<>4__this.{24057}.FastRemove({24081});
				CS$<>8__locals1.<>4__this.{24041}.Remove({24081}.Model);
			});
			foreach (ValueTuple<Vector3, float> {23650} in ((IEnumerable<ValueTuple<Vector3, float>>)CS$<>8__locals1.shipInfo.GunneryCrewPositions))
			{
				UnitScene unitScene = CrewConnector.CreateCrewUnitScene({23996}, UnitRole.Gunner, {23650}, this.{24058}.Shuffler);
				this.{24057}.Add(unitScene);
				this.{24041}.AddObject(unitScene.Model, true);
			}
			if (CS$<>8__locals1.shipInfo.Model.stuff != null)
			{
				this.{24037}.AddObject(this.{24051} = new ModelRenderer(CS$<>8__locals1.shipInfo.Model.stuff), true);
			}
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x000DBCC4 File Offset: 0x000D9EC4
		public void DrawCorpus(int {23997}, float {23998})
		{
			this.pickedLOD = {23997};
			bool flag = {23997} == 0 && !Renderer.ReflectionsAreBeingDrawn;
			if (this.{24051} != null)
			{
				this.{24051}.LocalVisible = (Global.Settings.HighDetailing && flag);
			}
			if (flag)
			{
				this.{24037}.SetModelData(0, this.{24027});
				foreach (ModelRenderer modelRenderer in ((IEnumerable<ModelRenderer>)this.{24042}))
				{
					modelRenderer.LocalVisible = true;
				}
				if (this.{24052} != null)
				{
					this.{24052}.LocalVisible = true;
				}
				if (this.{24053} != null)
				{
					this.{24053}.LocalVisible = true;
				}
				for (int i = 0; i < this.{24042}.Size; i++)
				{
					ModelRenderer modelRenderer2 = this.{24042}.Array[i];
					ModelRenderer {24005} = modelRenderer2;
					Vector3 one = Vector3.One;
					this.NewRotateFrameHelper({24005}, one, new float?(this.{24026}.physicsBody.ArrisRotation));
				}
			}
			else
			{
				this.{24037}.SetModelData(0, this.{24028});
				foreach (ModelRenderer modelRenderer3 in ((IEnumerable<ModelRenderer>)this.{24042}))
				{
					modelRenderer3.LocalVisible = false;
				}
				if (this.{24052} != null)
				{
					this.{24052}.LocalVisible = false;
				}
				if (this.{24053} != null)
				{
					this.{24053}.LocalVisible = false;
				}
			}
			Global.Render.CommonShader.RenderObject(this.{24037}, true, {23998}, false, 0f, false);
			if (this.{24053} != null)
			{
				this.{24053}.LocalVisible = false;
			}
			if (this.crewVisible && flag)
			{
				OpenWorldFlag owflag = this.GetOWFlag();
				this.{24020}(Math.Max(0f, Math.Min(Global.Game.GameTime.ElapsedDrawReal, Global.Game.GameTime.ElapsedUpdate * 5f)));
				float num = 0.5f + 0.5f * Geometry.Saturate(1.5f - Vector2.Distance(this.{24026}.Position, Engine.GS.Camera.Position.XZ()) / 30f);
				for (int j = 0; j < this.{24041}.CountModels; j++)
				{
					ModelRenderer modelRenderer4 = this.{24041}.GetModels[j];
					UnitScene unitScene = (UnitScene)modelRenderer4.Tag;
					modelRenderer4.LocalVisible = (unitScene.Transparancy >= 1f && HashHelper.greater(j) <= num);
					modelRenderer4.LocalRenderQuery = LocalContent.GetCrewTexture(owflag, unitScene).Albedo.Tex;
				}
				Texture2D currentReplaceMaterialTextre = Global.Render.CommonShader.CurrentReplaceMaterialTextre;
				Global.Render.CommonShader.RenderObject(this.{24041}, true, {23998}, false, 0f, false);
				Global.Render.CommonShader.SetSubstituteTexture(currentReplaceMaterialTextre);
			}
			if (!Renderer.ReflectionsAreBeingDrawn)
			{
				this.{24022}();
			}
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x000DBFCC File Offset: 0x000DA1CC
		public void DrawSailes(float {23999}, in Vector2 {24000})
		{
			this.{24062} = {23999};
			if (Renderer.ReflectionsAreBeingDrawn && Global.Render.reduceShipsInReflections && this.{24026} != Global.Player)
			{
				return;
			}
			if (this.{24026} != Global.Player || !Global.Camera.IsMinZoom || Global.Camera.IsFreeMode || {23999} == 1f)
			{
				if (!Renderer.ReflectionsAreBeingDrawn)
				{
					this.{24008}();
				}
				else
				{
					ShipPartial getClient = ((IClientShip)this.{24026}).GetClient;
					for (int i = 0; i < this.{24032}.Length; i++)
					{
						ModelRenderer modelRenderer = this.{24029}.GetModels[i];
						float currentSoftValueSmootherstep = getClient.sailingAnimation.Array[i].CurrentSoftValueSmootherstep;
						if (currentSoftValueSmootherstep > 0.99f && Renderer.ReflectionsAreBeingDrawn)
						{
							modelRenderer.LocalVisible = false;
						}
						if (this.{24030}.CountModels > 0)
						{
							this.{24030}.GetModels[i].LocalVisible = ((double)currentSoftValueSmootherstep > 0.99);
						}
					}
				}
				Global.Render.CommonShader.ConfigureAnimatedMeshInNextRenderObject({24000}, AnimationType.Sail);
				Global.Render.CommonShader.RenderObject(this.{24029}, false, {23999}, false, 0f, false);
				if (Renderer.ReflectionsAreBeingDrawn)
				{
					for (int j = 0; j < this.{24032}.Length; j++)
					{
						this.{24029}.GetModels[j].LocalVisible = true;
					}
				}
			}
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x000DC140 File Offset: 0x000DA340
		public void DrawFlags(float {24001})
		{
			if (this.{24035}.CountModels != 0 && !Renderer.ReflectionsAreBeingDrawn)
			{
				for (int i = 0; i < this.{24049}.Size; i++)
				{
					ModelRenderer modelRenderer = this.{24049}.Array[i];
					ValueTuple<Vector3, object> valueTuple = (ValueTuple<Vector3, object>)modelRenderer.Tag;
					AnimatedModelTag animatedModelTag = valueTuple.Item2 as AnimatedModelTag;
					modelRenderer.LocalTransformOrNull.CreateMultipivot(animatedModelTag.Pivot.Average, new Vector3(0f, this.{24026}.physicsBody.SailRotationBack, 0f), new Vector3(valueTuple.Item1.X, 0f, valueTuple.Item1.Z), new Vector3(0f, Global.Game.WorldInstance.LastWindAxis - this.{24026}.Rotation + 0.3f * (float)Math.Sin(Global.Game.GameTotalTimeSec), 0f));
				}
				for (int j = 0; j < this.{24050}.Size; j++)
				{
					ModelRenderer modelRenderer2 = this.{24050}.Array[j];
					modelRenderer2.LocalTransformOrNull.CreateCenterPivotRotation((Vector3)modelRenderer2.Tag, new Vector3(0f, Global.Game.WorldInstance.LastWindAxis - this.{24026}.Rotation + 0.2f * (float)Math.Sin(Global.Game.GameTotalTimeSec), 0f));
				}
				Global.Render.CommonShader.ConfigureAnimatedMeshInNextRenderObject(Vector2.Zero, AnimationType.None);
				for (int k = 0; k < this.{24036}.Size; k++)
				{
					ModelRenderer modelRenderer3 = this.{24035}.GetModels[k];
					modelRenderer3.Model = LocalContent.Loaded.Flag.Array[LocalContent.Loaded.Flag.Size - 1 - (526 * k + (int)(Global.Game.GameTotalTimeSec * 30.0)) % LocalContent.Loaded.Flag.Size];
					modelRenderer3.LocalVisible = true;
					modelRenderer3.LocalRenderQuery = this.{24036}.Array[k].Tex;
					Global.Render.CommonShader.RenderObject(this.{24035}, false, {24001}, false, 0f, false);
					modelRenderer3.LocalVisible = false;
				}
			}
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x000DC39C File Offset: 0x000DA59C
		public void DrawWeaponsAndPaddles(float {24002})
		{
			if (Renderer.ReflectionsAreBeingDrawn)
			{
				return;
			}
			bool flag = this.pickedLOD == 0;
			bool flag2 = this.pickedLOD <= 2 && this.{24026}.UsedShip.StaticInfo.PaddleModelInstances.Size > 0;
			if (!flag && !flag2)
			{
				return;
			}
			this.{24060}.CleanCache();
			if (flag)
			{
				bool flag3 = this.{24026} == Global.Player && this.{24026}.Position3D.DTest2(Engine.GS.Camera.Position, 15f);
				float currentSoftValueSmoothstep = ((IClientShip)this.{24026}).GetClient.DeckCoversOpening.CurrentSoftValueSmoothstep;
				float num = (1f - MathF.Pow(currentSoftValueSmoothstep, 1.3f)) * 1.5f;
				object obj = {22279}.IsActive ? ScreenOcclusionControl.SelectedVisualItem(this.{24026}).Reference : null;
				for (int i = 0; i < this.{24026}.UsedShip.Cannons.Items.Size; i++)
				{
					CannonCommon cannonCommon = this.{24026}.UsedShip.Cannons.Items.Array[i];
					if (!string.IsNullOrEmpty(cannonCommon.GameInfo.Model) && !cannonCommon.Location.Hidden)
					{
						UWModel uwmodel = (flag3 ? LocalContent.Loaded.Cannons : LocalContent.Loaded.CannonsLod)[(int)cannonCommon.GameInfo.ID];
						Matrix matrix = cannonCommon.Location.WorldLocal;
						Matrix matrix2;
						if (cannonCommon.GetRollbackAdditionalTransform((cannonCommon.Location.Side == CannonLocation.LeftSide || cannonCommon.Location.Side == CannonLocation.RightSide) ? num : 0f, out matrix2))
						{
							matrix *= matrix2;
						}
						if (Math.Abs(matrix.M11) < 0.1f && this.{24026}.CraftFrom.ID == 20)
						{
							matrix = Matrix.CreateScale(100f) * matrix;
						}
						this.{24060}.AddToCache(uwmodel, ref matrix);
						if ({22279}.IsActive && this.{24026} == Global.Player)
						{
							Transform3D transform = this.{24026}.Transform;
							Matrix matrix3;
							transform.CreateWorldMatrix(out matrix3);
							this.{24059}.Transform.Translation = Vector3.Transform((matrix2.Translation != Vector3.Zero) ? Vector3.Transform(cannonCommon.Location.Position, matrix2) : cannonCommon.Location.Position, matrix3);
							this.{24059}.Transform.Pitch = transform.Pitch;
							this.{24059}.Transform.Yaw = cannonCommon.Location.CurrentDirection.Y + transform.Yaw + 3.1415927f;
							this.{24059}.Transform.Roll = -transform.Roll;
							this.{24059}.Transform.MiddleScale = transform.MiddleScale;
							this.{24059}.SetModelData(0, uwmodel);
							Global.Render.CommonShader.RenderOutline(this.{24059}, (cannonCommon.Location == obj) ? ScreenOcclusionControl.DefaultShipElementOutlineColor : Color.Gray.ToVector4());
						}
					}
				}
				for (int j = 0; j < this.{24046}.Size; j++)
				{
					ShipScene.FalkonetRender falkonetRender = this.{24046}.Array[j];
					falkonetRender.SetTransform(this.{24026});
					for (int k = 0; k < 2; k++)
					{
						Matrix matrix4;
						falkonetRender.local[k].LocalTransformOrNull.CreateWorldMatrix(out matrix4);
						this.{24060}.AddToCache(falkonetRender.local[k].Model, ref matrix4);
						if ({22279}.IsActive && this.{24026} == Global.Player)
						{
							this.{24059}.Transform.CopyFrom(falkonetRender.local[k].LocalTransformOrNull);
							this.{24059}.Transform.MiddleScale *= 0.5f;
							this.{24059}.Transform.Multiply(this.{24026}.Transform);
							this.{24059}.SetModelData(0, falkonetRender.local[k].Model);
							Global.Render.CommonShader.RenderOutline(this.{24059}, (falkonetRender.Location == obj) ? ScreenOcclusionControl.DefaultShipElementOutlineColor : Color.Gray.ToVector4());
						}
					}
				}
				for (int l = 0; l < this.{24047}.Size; l++)
				{
					ShipScene.MortarRenderer mortarRenderer = this.{24047}.Array[l];
					mortarRenderer.SetTransform(this.{24026});
					Matrix matrix4;
					mortarRenderer.transform.CreateWorldMatrix(out matrix4);
					if (mortarRenderer.local != null)
					{
						this.{24060}.AddToCache(mortarRenderer.local, ref matrix4);
					}
					if (mortarRenderer.extraLafet != null)
					{
						this.{24060}.AddToCache(mortarRenderer.extraLafet, ref matrix4);
					}
					if ({22279}.IsActive && this.{24026} == Global.Player && mortarRenderer.local != null)
					{
						Matrix matrix5;
						this.{24026}.Transform.CreateWorldMatrix(out matrix5);
						this.{24059}.Transform.CopyFrom(mortarRenderer.transform);
						this.{24059}.Transform.MiddleScale *= 0.9f;
						this.{24059}.Transform.Multiply(this.{24026}.Transform);
						this.{24059}.SetModelData(0, mortarRenderer.local);
						Global.Render.CommonShader.RenderOutline(this.{24059}, (mortarRenderer.LocationInfo == obj) ? ScreenOcclusionControl.DefaultShipElementOutlineColor : Color.Gray.ToVector4());
					}
				}
				if (Global.Settings.HighDetailing)
				{
					for (int m = 0; m < this.{24026}.UsedShip.StaticInfo.DeckCoverModelInstances.Size; m++)
					{
						DeckCoverModelInfo deckCoverModelInfo = this.{24026}.UsedShip.StaticInfo.DeckCoverModelInstances.Array[m];
						float num2 = ShipScene.randValues[m] - 0.5f;
						float num3 = (deckCoverModelInfo.NearSectionId != null && !this.{24026}.UsedShip.Cannons.CannonsHavingFlagsBySectionId[deckCoverModelInfo.NearSectionId.Value]) ? 0f : Geometry.Desaturate(currentSoftValueSmoothstep, num2 * 0.7f);
						if (this.{24026}.UsedShip.StaticInfo.DeckCoverModelInstances.Array[m].FullOpen)
						{
							ShipScene.SetCreateRotationX(num3 * -1.8849558f, ref ShipScene.deckRotation);
						}
						else
						{
							ShipScene.SetCreateRotationX(num3 * -1.8849558f * 0.8f, ref ShipScene.deckRotation);
						}
						Matrix matrix6;
						Matrix.Multiply(ref ShipScene.deckRotation, ref deckCoverModelInfo.World, out matrix6);
						this.{24060}.AddToCache(this.{24026}.UsedShip.StaticInfo.Model.deckCoverModel, ref matrix6);
					}
				}
			}
			if (flag2)
			{
				ShipScene.tmepRotation = Matrix.Identity;
				FrameTime frameTime = new FrameTime(Global.Game.GameTime.ElapsedUpdateSec * 1000f, Global.Game.GameTime.ElapsedUpdateSec);
				this.{24056}.Evalute(ref frameTime, this.{24026}.physicsBody.LastPaddlesSpeedBonus > 1f);
				float currentSoftValue = this.{24056}.CurrentSoftValue;
				float num4 = 1f - this.{24026}.UsedShip.Crew.Effectivity(this.{24026}.UsedShip);
				if (num4 < 0.2f)
				{
					num4 = 0f;
				}
				for (int n = 0; n < this.{24026}.UsedShip.StaticInfo.PaddleModelInstances.Size; n++)
				{
					float angle = (float)(((double)((ShipScene.randValues[n] - 0.5f) * 0.3f) + 1.8 * Global.Game.GameTotalTimeSec * (double)Math.Sign(-this.{24026}.UsedShip.StaticInfo.PaddleModelInstances.Array[n].Forward.Z)) % 6.2831854820251465);
					float num5 = (HashHelper.greater(n) >= num4) ? currentSoftValue : 0f;
					Vector2 vector = Geometry.FastSinCos(angle);
					if (vector.Y < 0f)
					{
						vector.Y = (float)(Math.Pow((double)Math.Abs(vector.Y), 0.75) * (double)Math.Sign(vector.Y));
					}
					ShipScene.SetCreateRotationX(vector.X * num5 * 0.35f + 0.15f + (1f - num5) * 0.25f, ref ShipScene.tmepRotation);
					ShipScene.SetCreateRotationZAndOffset(vector.Y * 0.5f * -1.8849558f * num5 * num5, ref ShipScene.tmepRotation, new Vector3(0f, Math.Max(0f, vector.X * num5) * 0.5f - 1f + (1f - num5) * 3f, 0f));
					Matrix matrix7;
					Matrix.Multiply(ref ShipScene.tmepRotation, ref this.{24026}.UsedShip.StaticInfo.PaddleModelInstances.Array[n], out matrix7);
					this.{24060}.AddToCache(this.{24026}.UsedShip.StaticInfo.Model.paddleModel, ref matrix7);
					if (num5 * vector.X < -0.5f && Rand.Chanse(4f) && this.{24026} == Global.Player)
					{
						Vector3 translation = this.{24026}.UsedShip.StaticInfo.PaddleModelInstances.Array[n].Translation;
						translation.Z += (float)Math.Sign(this.{24026}.UsedShip.StaticInfo.PaddleModelInstances.Array[n].Forward.Z) * 2.5f;
						Matrix matrix8;
						this.{24026}.Transform.CreateWorldMatrix(out matrix8);
						Vector3 {11450};
						Vector3.Transform(ref translation, ref matrix8, out {11450});
						FXEngine.WaterParticleVolumteric({11450}.XZ(), 1f, Vector3.Zero, Color.White, -0.5f, null);
					}
				}
			}
			this.{24060}.BuildCache();
			bool rasterizerStateCullingEnable = Engine.GS.graphicsDevice.RasterizerStateCullingEnable;
			Engine.GS.graphicsDevice.RasterizerStateCullingEnable = true;
			Global.Render.CommonShader.RenderObjectInstanced(this.{24037}, this.{24060}, {24002});
			Engine.GS.graphicsDevice.RasterizerStateCullingEnable = rasterizerStateCullingEnable;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x000DCE80 File Offset: 0x000DB080
		public void DrawSteamWheel()
		{
			if (this.pickedLOD > 2 || !this.{24026}.UsedShip.StaticInfo.HasSteamWheel)
			{
				return;
			}
			float num = (this.{24026}.physicsBody.NowSpeed != 0f) ? (this.{24026}.physicsBody.NowSpeed * 0.001f) : 0f;
			if (this.{24026}.physicsBody.NowSpeed != 0f)
			{
				this.{24055} = num;
			}
			else
			{
				Geometry.Evalute(ref this.{24055}, 0f, 0.0002f);
			}
			float z = this.{24054}.LocalTransformOrNull.Roll + this.{24055} * Global.Game.GameTime.ElapsedDrawReal / 16.6f;
			if (this.{24055} != 0f)
			{
				Vector3 vector = (Vector3)this.{24054}.Tag;
				this.{24054}.LocalTransformOrNull.CreateCenterPivotRotation(new Vector3(vector.X, vector.Y, 0f), new Vector3(0f, 0f, z));
				if (Rand.Chanse(4f) && this.{24026} == Global.Player)
				{
					float num2 = 5f;
					Vector3 vector2 = new Vector3(vector.X, vector.Y, vector.Z - this.{24026}.UsedShip.StaticInfo.CorpusHalfWidth - num2);
					Vector3 vector3 = new Vector3(vector.X, vector.Y, vector.Z + this.{24026}.UsedShip.StaticInfo.CorpusHalfWidth + num2);
					Matrix matrix;
					this.{24026}.Transform.CreateWorldMatrix(out matrix);
					Vector3 {11450};
					Vector3.Transform(ref vector2, ref matrix, out {11450});
					Vector3 {11450}2;
					Vector3.Transform(ref vector3, ref matrix, out {11450}2);
					FXEngine.WaterParticleVolumteric({11450}.XZ(), 1.8f, Vector3.Zero, Color.White, -0.5f, null);
					FXEngine.WaterParticleVolumteric({11450}2.XZ(), 1.8f, Vector3.Zero, Color.White, -0.5f, null);
				}
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x000DD0A4 File Offset: 0x000DB2A4
		public void DrawFlagpoles(float {24003})
		{
			if (!Renderer.ReflectionsAreBeingDrawn && this.{24048}.Size > 0)
			{
				for (int i = 0; i < this.{24048}.Size; i++)
				{
					this.{24048}.Array[i].LocalTransformOrNull.Yaw = Global.Game.WorldInstance.LastWindAxis - this.{24026}.Rotation;
				}
				Material material = null;
				if (this.{24033}.CountModels > 0)
				{
					ShipOtherPlayer shipOtherPlayer = this.{24026} as ShipOtherPlayer;
					bool flag;
					if (shipOtherPlayer != null)
					{
						RemotePlayerDynamicInfo remoteInfo = shipOtherPlayer.RemoteInfo;
						if (remoteInfo != null && remoteInfo.FlagsTensityMode)
						{
							flag = true;
							goto IL_BA;
						}
					}
					flag = (this.{24026} == Global.Player && Session.Account.TensityMode);
					IL_BA:
					bool flag2 = flag;
					ShipNpc shipNpc = null;
					bool flag3 = false;
					ShipNpc shipNpc2 = this.{24026} as ShipNpc;
					if (shipNpc2 != null)
					{
						shipNpc = shipNpc2;
						flag3 = true;
					}
					OpenWorldFlag owflag = this.GetOWFlag();
					bool {25071} = flag2;
					FractionID {25072};
					if (this.{24026} != Global.Player)
					{
						if (!flag3)
						{
							ShipOtherPlayer shipOtherPlayer2 = this.{24026} as ShipOtherPlayer;
							{25072} = ((shipOtherPlayer2 != null) ? shipOtherPlayer2.GetClient.Guild.Fraction : FractionID.None);
						}
						else
						{
							{25072} = (shipNpc.UsedShipNpc.Information.Extras.IsEmpire ? FractionID.Empire : shipNpc.GetClient.Guild.Fraction);
						}
					}
					else
					{
						{25072} = Session.Game.MapMyFraction.GetValueOrDefault(FractionID.None);
					}
					material = LocalContent.WorldFlagTexture(owflag, {25071}, {25072}, this.{24026} is Npc, flag3 && shipNpc.UsedShipNpc.Information.Descritpion == NpcType.Fanatics, flag3 && shipNpc.IsPlayerCaper);
				}
				Global.Render.CommonShader.ConfigureAnimatedMeshInNextRenderObject(Vector2.Zero, AnimationType.Flagpoll);
				if (material != null)
				{
					Global.Render.CommonShader.SetSubstituteTexture(material.Albedo.Tex);
				}
				Global.Render.CommonShader.RenderObject(this.{24033}, false, {24003}, false, 0f, false);
				Global.Render.CommonShader.SetSubstituteTexture(null);
				Global.Render.CommonShader.RenderObject(this.{24034}, false, {24003}, false, 0f, false);
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x000DD2CC File Offset: 0x000DB4CC
		public void DrawMastsAndFrames(float {24004})
		{
			int num = this.pickedLOD;
			if (Renderer.ReflectionsAreBeingDrawn)
			{
				num += 2;
				num++;
			}
			if (num >= 4)
			{
				return;
			}
			bool flag = false;
			ShipPlayerBase shipPlayerBase = this.{24026} as ShipPlayerBase;
			if (num < 2)
			{
				Global.Render.CommonShader.RenderObject(this.{24038}, true, {24004}, false, 0f, false);
			}
			else if (num < 3)
			{
				Global.Render.CommonShader.RenderObject(this.{24039}, true, {24004}, false, 0f, false);
			}
			else if (num < 4)
			{
				Global.Render.CommonShader.RenderObject(this.{24040}, true, {24004}, false, 0f, false);
			}
			if (flag)
			{
				Global.Render.CommonShader.SetSubstituteTexture(null);
			}
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x000DD380 File Offset: 0x000DB580
		public void RenderTransparentUnits()
		{
			if (!this.crewVisible || Renderer.ReflectionsAreBeingDrawn)
			{
				return;
			}
			for (int i = 0; i < this.{24041}.CountModels; i++)
			{
				this.{24041}.GetModels[i].LocalVisible = false;
			}
			Texture2D currentReplaceMaterialTextre = Global.Render.CommonShader.CurrentReplaceMaterialTextre;
			OpenWorldFlag owflag = this.GetOWFlag();
			for (int j = 0; j < this.{24041}.CountModels; j++)
			{
				ModelRenderer modelRenderer = this.{24041}.GetModels[j];
				UnitScene unitScene = (UnitScene)modelRenderer.Tag;
				if (unitScene.Transparancy < 1f && unitScene.Transparancy > 0f)
				{
					modelRenderer.LocalVisible = true;
					modelRenderer.LocalRenderQuery = LocalContent.GetCrewTexture(owflag, unitScene).Albedo.Tex;
					Global.Render.CommonShader.RenderObject(this.{24041}, true, unitScene.Transparancy, false, 0f, false);
					modelRenderer.LocalVisible = false;
				}
			}
			Global.Render.CommonShader.SetSubstituteTexture(currentReplaceMaterialTextre);
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x000DD498 File Offset: 0x000DB698
		private void NewRotateFrameHelper(ModelRenderer {24005}, in Vector3 {24006}, float? {24007} = null)
		{
			PivotInfo pivot = ((AnimatedModelTag){24005}.Model.Tag).Pivot;
			if (pivot.IsFixed)
			{
				throw new InvalidOperationException();
			}
			float num;
			if ({24007} != null)
			{
				num = {24007}.Value;
			}
			else
			{
				Vector3 size = {24005}.Model.MeshParts[0].LocalSpaceBoundingBox.Size;
				num = ((size.X > size.Z) ? this.{24026}.physicsBody.SailRotationBack : ((pivot.Lower.X == this.{24026}.UsedShip.StaticInfo.Model.MastControlsMinMaxX.Y || pivot.Upper.X == this.{24026}.UsedShip.StaticInfo.Model.MastControlsMinMaxX.Y) ? this.{24026}.physicsBody.SailRotationForw : this.{24026}.physicsBody.SailRotation));
			}
			Matrix matrix = Matrix.CreateRotationY(num);
			Vector3 vector = Vector3.Transform(pivot.Normal, matrix);
			pivot.Lower + vector;
			float x = MathF.Atan2(vector.Y, vector.Z) - 1.5707964f;
			float num2 = MathF.Atan2(vector.Y, vector.X - pivot.Normal.X) - 1.5707964f;
			float x2 = {24005}.Model.Drawcalls.First<ModelPartShadercall>().Parts.First().LocalSpaceBoundingBox.Min.X;
			Vector3 vector2 = new Vector3(x2 - x2 * {24006}.X, 0f, 0f);
			vector2 = Vector3.Transform(vector2, Matrix.CreateRotationY(-num));
			Vector3 lower = pivot.Lower;
			Vector3 value = -lower;
			Vector3 vector3 = new Vector3(x, num, -num2);
			Matrix matrix2 = Matrix.CreateFromYawPitchRoll(-vector3.Y, -vector3.X, -vector3.Z);
			Vector3.Transform(ref value, ref matrix2, out value);
			{24005}.LocalTransformOrNull.Translation = value + lower + vector2;
			{24005}.LocalTransformOrNull.RotatesAll = vector3;
			{24005}.LocalTransformOrNull.Scales = {24006};
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x000DD6D8 File Offset: 0x000DB8D8
		private void {24008}()
		{
			for (int i = 0; i < this.{24044}.Size; i++)
			{
				ModelRenderer modelRenderer = this.{24044}.Array[i];
				ModelRenderer {24005} = modelRenderer;
				Vector3 one = Vector3.One;
				this.NewRotateFrameHelper({24005}, one, null);
			}
			Texture2D texture2D = null;
			Texture2D texture2D2 = null;
			ShipDesignInfo shipDesignInfo = null;
			Bits32 bits = default(Bits32);
			Bits32 bits2 = default(Bits32);
			ShipPartial getClient = ((IClientShip)this.{24026}).GetClient;
			float num = Vector2.Dot(CommonGlobal.CurrentClientWeather.WindXZNormal, Geometry.RotateVector2(this.{24026}.FastNormal, 1.5707964f));
			num = Math.Max(1f, this.{24026}.UsedShip.StaticInfo.CorpusHalfLength / 6.5f) * MathF.Pow(Math.Abs(num), 0.3f) * (float)Math.Sign(num);
			if (this.{24026}.UsedShip.StaticInfo.ID == 25)
			{
				num = 0f;
			}
			ShipPlayerBase shipPlayerBase = this.{24026} as ShipPlayerBase;
			if (shipPlayerBase != null)
			{
				ShipDesignInfo {16949} = shipPlayerBase.Client.GetPreview(ShipDesignCategory.Decal1) ?? shipPlayerBase.UsedShipPlayer.GetDesignElement(1);
				ShipDesignInfo shipDesignInfo2 = shipPlayerBase.Client.GetPreview(ShipDesignCategory.Decal2) ?? shipPlayerBase.UsedShipPlayer.GetDesignElement(3);
				shipPlayerBase.Client.CheckForFullDesign();
				texture2D = shipPlayerBase.Client.GetDecal({16949}, shipPlayerBase);
				texture2D2 = shipPlayerBase.Client.GetDecal(shipDesignInfo2, shipPlayerBase);
				bits.Value = ((shipPlayerBase.Client.GetPreview(ShipDesignCategory.Decal1) != null) ? int.MaxValue : shipPlayerBase.UsedShipPlayer.Decal1SelectedSailesBits.Value);
				bits2.Value = ((shipPlayerBase.Client.GetPreview(ShipDesignCategory.Decal2) != null) ? (shipDesignInfo2.WrapsPublicDesign ? int.MaxValue : this.{24026}.UsedShip.StaticInfo.AnimatedSailesBits.Value) : shipPlayerBase.UsedShipPlayer.Decal2SelectedSailesBits.Value);
				shipDesignInfo = (shipPlayerBase.Client.GetPreview(ShipDesignCategory.SailTexture) ?? shipPlayerBase.UsedShipPlayer.GetDesignElement(8));
			}
			else
			{
				ShipNpc shipNpc = this.{24026} as ShipNpc;
				if (shipNpc != null)
				{
					VirtualTexture npcSail = LocalContent.GetNpcSail(shipNpc);
					texture2D = ((npcSail != null) ? npcSail.Tex : null);
					NpcShipDynamicInfo usedShipNpc = shipNpc.UsedShipNpc;
					bits.Value = ((usedShipNpc.UseDecalBits != null) ? usedShipNpc.UseDecalBits.GetValueOrDefault().Value : int.MaxValue);
					NpcShipDynamicInfo usedShipNpc2 = shipNpc.UsedShipNpc;
					bits2.Value = ((usedShipNpc2.UseDecalBits != null) ? usedShipNpc2.UseDecalBits.GetValueOrDefault().Value : this.{24026}.UsedShip.StaticInfo.AnimatedSailesBits.Value);
				}
			}
			for (int j = 0; j < this.{24032}.Length; j++)
			{
				SailLocalRenderQuery sailLocalRenderQuery = this.{24032}[j];
				ModelRenderer modelRenderer2 = this.{24029}.GetModels[j];
				int num2 = this.{24031}[j];
				float num3 = this.{24026}.UsedShip.HitboxSailsStrength[num2];
				num3 = 1f - MathF.Pow(1f - num3, 0.85f);
				float currentSoftValueSmootherstep = getClient.sailingAnimation.Array[j].CurrentSoftValueSmootherstep;
				sailLocalRenderQuery.RollValue = currentSoftValueSmootherstep;
				sailLocalRenderQuery.DestructnessTexture = Materials.GetSailDestructEffect((this.{24026}.UsedShip.HitboxSailsStrength.Length == 0) ? 255 : ((int)(255f * num3)));
				sailLocalRenderQuery.Decal1ToSet = (bits[num2] ? texture2D : null);
				sailLocalRenderQuery.Decal2ToSet = (bits2[num2] ? texture2D2 : null);
				sailLocalRenderQuery.DecalsTransparancy = ((this.{24026} == Global.Player || !Global.Settings.HalfTransparentSailDesign) ? ((shipDesignInfo != null && shipDesignInfo.WrapsPublicDesign) ? 0.9f : 1f) : 0.5f);
				Texture2D customizedTexture;
				if (shipDesignInfo == null)
				{
					customizedTexture = null;
				}
				else
				{
					VirtualTexture decalForSail = LocalContent.GetDecalForSail(shipDesignInfo, modelRenderer2.Model.Drawcalls.Last<ModelPartShadercall>().Material.Albedo.AssetName);
					customizedTexture = ((decalForSail != null) ? decalForSail.Tex : null);
				}
				sailLocalRenderQuery.CustomizedTexture = customizedTexture;
				float y = MathHelper.Lerp(this.{24026}.physicsBody.LastWindEffectivity * 1.2f, 0f, Geometry.Saturate(-this.{24026}.physicsBody.NowSpeed / 3f));
				sailLocalRenderQuery.SailWindDirection = new Vector2(num, y);
				modelRenderer2.LocalRenderQuery = this.{24032}[j];
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x000DDB5C File Offset: 0x000DBD5C
		private static void SetCreateRotationX(float {24009}, ref Matrix {24010})
		{
			Vector2 vector = Geometry.FastSinCos({24009});
			{24010}.M22 = vector.X;
			{24010}.M23 = vector.Y;
			{24010}.M32 = -vector.Y;
			{24010}.M33 = vector.X;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x000DDBA4 File Offset: 0x000DBDA4
		private static void SetCreateRotationY(float {24011}, ref Matrix {24012})
		{
			Vector2 vector = Geometry.FastSinCos({24011});
			{24012}.M11 = vector.X;
			{24012}.M31 = vector.Y;
			{24012}.M13 = -vector.Y;
			{24012}.M33 = vector.X;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000DDBEC File Offset: 0x000DBDEC
		private static void SetCreateRotationZ(float {24013}, ref Matrix {24014})
		{
			Vector2 vector = Geometry.FastSinCos({24013});
			{24014}.M11 = vector.X;
			{24014}.M21 = vector.Y;
			{24014}.M12 = -vector.Y;
			{24014}.M22 = vector.X;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x000DDC34 File Offset: 0x000DBE34
		private static void SetCreateRotationZAndOffset(float {24015}, ref Matrix {24016}, Vector3 {24017})
		{
			Vector2 vector = Geometry.FastSinCos({24015});
			{24016}.M11 = vector.X;
			{24016}.M21 = vector.Y;
			{24016}.M12 = -vector.Y;
			{24016}.M22 = vector.X;
			{24016}.M41 = 0f;
			{24016}.M42 = 0f;
			{24016}.M43 = 0f;
			Vector3 vector2;
			Vector3.Transform(ref {24017}, ref {24016}, out vector2);
			{24016}.M41 = vector2.X;
			{24016}.M42 = vector2.Y;
			{24016}.M43 = vector2.Z;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x000DDCC8 File Offset: 0x000DBEC8
		public void GBuffer(IGBufferBuilder {24018}, bool {24019})
		{
			if (this.pickedLOD >= 3)
			{
				return;
			}
			float num = Vector3.DistanceSquared(this.{24026}.Transform.Translation, Engine.GS.Camera.Position);
			if ((num > 22500f && !{24019}) || num > 62500f || this.{24026}.UsedShip.IsInvisibilityBonusEnabled)
			{
				return;
			}
			if (!this.{24037}.CheckVisibilityAndRenderGBuffer({24018}, null))
			{
				return;
			}
			if (this.pickedLOD < 2 && this.{24026}.UsedShip.FirstSailHP > 0f)
			{
				for (int i = 0; i < this.{24029}.CountModels; i++)
				{
					this.{24029}.GetModels[i].LocalVisible = (((IClientShip)this.{24026}).GetClient.sailingAnimation.Array[i].CurrentSoftValue < 0.3f);
				}
				this.{24029}.CheckVisibilityAndRenderGBuffer({24018}, null);
				for (int j = 0; j < this.{24029}.CountModels; j++)
				{
					this.{24029}.GetModels[j].LocalVisible = true;
				}
			}
			if (this.pickedLOD == 0 && Renderer.CurrentShadowMapIndex == 0)
			{
				this.{24060}.Instantiate({24018}, this.{24037}, 1);
				this.{24038}.RenderToGBuffer({24018}, null);
			}
			if (this.pickedLOD == 0 && Renderer.CurrentShadowMapIndex == 1)
			{
				this.{24039}.RenderToGBuffer({24018}, null);
			}
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x000DDE3C File Offset: 0x000DC03C
		private void {24020}(float {24021})
		{
			bool {24092} = this.{24026}.UsedShip.Cannons.IsReloaded(CannonLocation.LeftSide);
			bool {24093} = this.{24026}.UsedShip.Cannons.IsReloaded(CannonLocation.RightSide);
			bool flag = false;
			bool {24095} = {18139}.IsInBoardingUi(this.{24026}.uID);
			Vector2 {24097} = new Vector2(this.{24026}.Transform.Pitch, this.{24026}.Transform.Roll);
			ShipCurrentPlayer shipCurrentPlayer = this.{24026} as ShipCurrentPlayer;
			if (shipCurrentPlayer != null)
			{
				if (shipCurrentPlayer.IsMendingBegin)
				{
					{24093} = ({24092} = false);
				}
				flag = (Session.TimeFromLastReceivedDamageSec < 3f);
			}
			this.{24058}.Update(this.{24026}, null);
			foreach (UnitScene unitScene in ((IEnumerable<UnitScene>)this.{24057}))
			{
				unitScene.Update({24021}, this.{24026}, {24092}, {24093}, flag, {24095}, !flag && this.{24026}.ClientWeaponsShooting.ShotIsProcessing, {24097}, this.{24057});
			}
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x000DDF64 File Offset: 0x000DC164
		private void {24022}()
		{
			Ship {24024} = null;
			if ({18139}.IsInBoardingUi(this.{24026}.uID))
			{
				int {24874} = (Global.Player.uID == this.{24026}.uID) ? {18139}.CurrentInstance.OpponentUid : Global.Player.uID;
				{24024} = Global.Game.WorldInstance.GetShipFromUID({24874});
			}
			else if (this.{24026} == Global.Player && Session.TimeFromLastSendedCBDamageSec < 15f)
			{
				{24024} = Global.Game.WorldInstance.GetShipFromUID(Session.LastSentCBDamageUID);
			}
			this.{24023}({24024});
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x000DDFFC File Offset: 0x000DC1FC
		private void {24023}(Ship {24024})
		{
			float? {24101} = null;
			if ({24024} != null)
			{
				Vector2 vector = {24024}.Position - this.{24026}.Position;
				if (vector.LengthSquared() < 4900f)
				{
					{24101} = new float?(Geometry.AxisNorm(MathF.Atan2(vector.Y, vector.X)));
				}
			}
			foreach (CrewConnector.Place place in ((IEnumerable<CrewConnector.Place>)this.{24058}.Places))
			{
				if (place.CrewOrNull != null)
				{
					place.CrewOrNull.CreateGunEffects({24101}, this.{24026});
				}
			}
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x000DE0B0 File Offset: 0x000DC2B0
		public void JoyEffectCrew()
		{
			if (this.crewVisible)
			{
				foreach (UnitScene unitScene in ((IEnumerable<UnitScene>)this.{24057}))
				{
					unitScene.CreateJoyEffect();
				}
			}
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000DE104 File Offset: 0x000DC304
		public void DrawEffectsAndDecals()
		{
			if (this.crewVisible && this.pickedLOD == 0)
			{
				Global.Render.ItemsShader.SetForRender(Matrix.Identity, Vector4.One);
				Global.Render.ItemsShader.BeginPass(true, true);
				foreach (UnitScene unitScene in ((IEnumerable<UnitScene>)this.{24057}))
				{
					if (unitScene.IsDeath)
					{
						BillboardParent_VPCT bloodDecal = unitScene.BloodDecal;
						if (bloodDecal != null)
						{
							bloodDecal.Render();
						}
					}
				}
			}
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x000DE1A0 File Offset: 0x000DC3A0
		public OpenWorldFlag GetOWFlag()
		{
			ShipOtherPlayer shipOtherPlayer = this.{24026} as ShipOtherPlayer;
			if (shipOtherPlayer != null)
			{
				return shipOtherPlayer.RemoteInfo.Flags;
			}
			if (this.{24026} == Global.Player)
			{
				return Session.Account.WorldFlag;
			}
			ShipNpc shipNpc = this.{24026} as ShipNpc;
			if (shipNpc != null)
			{
				return shipNpc.VisibleWorldFlags;
			}
			return OpenWorldFlag.Pirate;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x000DE1F7 File Offset: 0x000DC3F7
		[CompilerGenerated]
		internal static float <UpdateModelData>g__Ratio|54_3(UWModel {24025})
		{
			return {24025}.MeshParts[0].LocalSpaceBoundingBox.Size.X;
		}

		// Token: 0x04001762 RID: 5986
		internal int pickedLOD;

		// Token: 0x04001763 RID: 5987
		private Ship {24026};

		// Token: 0x04001764 RID: 5988
		private UWModel {24027};

		// Token: 0x04001765 RID: 5989
		private UWModel {24028};

		// Token: 0x04001766 RID: 5990
		private ModelTransformedScene {24029};

		// Token: 0x04001767 RID: 5991
		private ModelTransformedScene {24030};

		// Token: 0x04001768 RID: 5992
		private int[] {24031};

		// Token: 0x04001769 RID: 5993
		private SailLocalRenderQuery[] {24032};

		// Token: 0x0400176A RID: 5994
		private ModelTransformedScene {24033};

		// Token: 0x0400176B RID: 5995
		private ModelTransformedScene {24034};

		// Token: 0x0400176C RID: 5996
		private ModelTransformedScene {24035};

		// Token: 0x0400176D RID: 5997
		private Tlist<VirtualTexture> {24036};

		// Token: 0x0400176E RID: 5998
		private ModelTransformedScene {24037};

		// Token: 0x0400176F RID: 5999
		private ModelTransformedScene {24038};

		// Token: 0x04001770 RID: 6000
		private ModelTransformedScene {24039};

		// Token: 0x04001771 RID: 6001
		private ModelTransformedScene {24040};

		// Token: 0x04001772 RID: 6002
		private ModelTransformedScene {24041};

		// Token: 0x04001773 RID: 6003
		private Tlist<ModelRenderer> {24042};

		// Token: 0x04001774 RID: 6004
		private Tlist<ModelRenderer> {24043};

		// Token: 0x04001775 RID: 6005
		private Tlist<ModelRenderer> {24044};

		// Token: 0x04001776 RID: 6006
		private Tlist<bool> {24045};

		// Token: 0x04001777 RID: 6007
		private Tlist<ShipScene.FalkonetRender> {24046};

		// Token: 0x04001778 RID: 6008
		private Tlist<ShipScene.MortarRenderer> {24047};

		// Token: 0x04001779 RID: 6009
		private Tlist<ModelRenderer> {24048};

		// Token: 0x0400177A RID: 6010
		private Tlist<ModelRenderer> {24049};

		// Token: 0x0400177B RID: 6011
		private Tlist<ModelRenderer> {24050};

		// Token: 0x0400177C RID: 6012
		private ModelRenderer {24051};

		// Token: 0x0400177D RID: 6013
		private ModelRenderer {24052};

		// Token: 0x0400177E RID: 6014
		private ModelRenderer {24053};

		// Token: 0x0400177F RID: 6015
		private ModelRenderer {24054};

		// Token: 0x04001780 RID: 6016
		private float {24055};

		// Token: 0x04001781 RID: 6017
		private SoftTrigger {24056} = new SoftTrigger(0f, 1f, 0.3f);

		// Token: 0x04001782 RID: 6018
		private Tlist<UnitScene> {24057};

		// Token: 0x04001783 RID: 6019
		private CrewConnector {24058};

		// Token: 0x04001784 RID: 6020
		private ModelTransformedScene {24059};

		// Token: 0x04001785 RID: 6021
		private InstancedModelRenderer {24060};

		// Token: 0x04001786 RID: 6022
		private int {24061};

		// Token: 0x04001787 RID: 6023
		private float {24062};

		// Token: 0x04001788 RID: 6024
		private Tlist<ValueTuple<Vector3, float>> {24063} = new Tlist<ValueTuple<Vector3, float>>();

		// Token: 0x04001789 RID: 6025
		private bool {24064};

		// Token: 0x0400178A RID: 6026
		private static Matrix deckRotation = Matrix.Identity;

		// Token: 0x0400178B RID: 6027
		private static Matrix tmepRotation = Matrix.Identity;

		// Token: 0x0400178C RID: 6028
		private static float[] randValues = new float[180];

		// Token: 0x0400178D RID: 6029
		private ModelTransformedScene {24065};

		// Token: 0x0200047D RID: 1149
		internal class FalkonetRender
		{
			// Token: 0x06001928 RID: 6440 RVA: 0x000DE210 File Offset: 0x000DC410
			public FalkonetRender(FalkonetLocationInfo {24067})
			{
				this.Location = {24067};
				this.local = new ModelRenderer[2];
				this.local[0] = new ModelRenderer(LocalContent.Loaded.Falkonet1[0])
				{
					LocalTransformOrNull = new Transform3D
					{
						Translation = {24067}.LocalPosition
					}
				};
				this.local[1] = new ModelRenderer(LocalContent.Loaded.Falkonet1[1])
				{
					LocalTransformOrNull = new Transform3D
					{
						Translation = {24067}.LocalPosition + new Vector3(0f, 0.35900003f, 0f)
					}
				};
			}

			// Token: 0x06001929 RID: 6441 RVA: 0x000DE2B0 File Offset: 0x000DC4B0
			internal void SetTransform(Ship {24068})
			{
				Vector2 nowDirection;
				if ({24068} == Global.Player)
				{
					nowDirection = InGameSightUi.CannonSights.NowDirection;
					if (Session.SelectedFalkonetsInfo.FalkonetGoesAbove)
					{
						nowDirection.X += 0.6f;
					}
				}
				else
				{
					nowDirection = new Vector2(0f, {24068}.Rotation + 1.5707964f + 1.5707964f * (float)Math.Sign(this.Location.LocalPosition.Z));
				}
				this.local[0].LocalTransformOrNull.Yaw = nowDirection.Y - {24068}.Rotation + 3.1415927f;
				this.local[0].LocalTransformOrNull.Roll = 0f;
				this.local[1].LocalTransformOrNull.Yaw = nowDirection.Y - {24068}.Rotation + 3.1415927f;
				this.local[1].LocalTransformOrNull.Pitch = nowDirection.X * 0.6f + 0.1f;
			}

			// Token: 0x0400178E RID: 6030
			public ModelRenderer[] local;

			// Token: 0x0400178F RID: 6031
			public FalkonetLocationInfo Location;
		}

		// Token: 0x0200047E RID: 1150
		internal class MortarRenderer
		{
			// Token: 0x0600192A RID: 6442 RVA: 0x000DE3A8 File Offset: 0x000DC5A8
			public MortarRenderer(CannonLocationInfo {24072}, CannonGameInfo {24073}, bool {24074})
			{
				this.LocationInfo = {24072};
				this.transform = new Transform3D
				{
					Translation = this.LocationInfo.Position - ({24074} ? Vector3.Zero : new Vector3(0f, 0.3f, 0f)),
					Yaw = 1.5707964f + (({24072}.Side == CannonLocation.InFront) ? 0f : 3.1415927f)
				};
				this.local = (({24073} == null) ? null : LocalContent.Loaded.Cannons[(int){24073}.ID]);
				this.extraLafet = ({24074} ? LocalContent.Loaded.MortarUpgradeExtraLafet : null);
			}

			// Token: 0x0600192B RID: 6443 RVA: 0x00003100 File Offset: 0x00001300
			internal void SetTransform(Ship {24075})
			{
			}

			// Token: 0x04001790 RID: 6032
			public CannonLocationInfo LocationInfo;

			// Token: 0x04001791 RID: 6033
			public UWModel local;

			// Token: 0x04001792 RID: 6034
			public UWModel extraLafet;

			// Token: 0x04001793 RID: 6035
			public Transform3D transform;
		}
	}
}
