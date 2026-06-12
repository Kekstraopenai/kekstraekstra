using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Common;
using Common.Data;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using TheraEngine.Scene.Lighting;
using TheraEngine.Scene.ParticleSystem;
using UWContentPipelineExtensionRuntime;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x02000046 RID: 70
	internal class Isle : IDisposable
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000FABA File Offset: 0x0000DCBA
		public ModelTransformedScene SceneObject
		{
			get
			{
				return this.{16687};
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000FAC2 File Offset: 0x0000DCC2
		public FloatingIsleType IsFloating
		{
			get
			{
				return this.Statement.ModelData.IsFloating;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
		public Isle(IsleInstance {16657}, int {16658})
		{
			this.Statement = {16657};
			this.replacebleTerrainXfxID = {16658};
			this.{16687} = new ModelTransformedScene({16657}.ModelData.Model, new Transform3D({16657}.GlobalTransform));
			this.{16697} = ({16657}.ModelGlobalBS.Radius > 130f);
			this.{16693} = new Tlist<Vector3>();
			if (this.{16697})
			{
				this.{16696} = new Transform3D();
				this.{16696}.CopyFrom({16657}.GlobalTransform);
			}
			this.ToggleVisibleDistance();
			if (!{18560}.closed)
			{
				this.farDistMultiplier = 100f;
			}
			if ({16657}.ModelData.Flags.Size > 0)
			{
				this.{16688} = new ModelTransformedScene
				{
					Transform = this.{16687}.Transform,
					VisibleTestType = ModelSceneVisibleTest.Disable
				};
				for (int i = 0; i < {16657}.ModelData.Flags.Size; i++)
				{
					this.{16688}.AddObject(new ModelRenderer({16657}.ModelData.Flags.Array[i])
					{
						LocalTransformOrNull = new Transform3D()
					}, true);
				}
			}
			this.{16682} = new Tlist<Vector3>(0);
			foreach (ModelHardpoint modelHardpoint in {16657}.ModelData.Model.ExternalHardpoints)
			{
				Vector3 {5192} = this.{16687}.Transform.Transform3X3(modelHardpoint.Transform.Translation);
				if (modelHardpoint.HardpointID == WorldOfSeaBattleHardpointID.HPIsleFume)
				{
					this.{16682}.Add({5192});
				}
				if (modelHardpoint.HardpointID == WorldOfSeaBattleHardpointID.HPAnchoredShip)
				{
					Tlist<ShipPositionInfo> tlist = this.{16683};
					ShipPositionInfo shipPositionInfo = new ShipPositionInfo({5192}, MathF.Atan2(modelHardpoint.Transform.Forward.Z, modelHardpoint.Transform.Forward.X) + this.{16687}.Transform.Yaw + 1.5707964f);
					tlist.Add(shipPositionInfo);
				}
			}
			this.{16686} = new Tlist<ModelRenderer>(0);
			foreach (UWModel {11956} in ((IEnumerable<UWModel>){16657}.ModelData.DesturctionParts))
			{
				Tlist<ModelRenderer> tlist2 = this.{16686};
				ModelRenderer modelRenderer = this.{16687}.AddObject({11956});
				tlist2.Add(modelRenderer);
			}
			this.{16686}.Shuffle();
			FactoryPlaceIsleInfo factoryPlaceIsleInfo = {16657} as FactoryPlaceIsleInfo;
			if (factoryPlaceIsleInfo != null)
			{
				this.InnerModels = new IsleInnerModelsHelper(factoryPlaceIsleInfo, this.{16687});
			}
			this.{16684} = new Tlist<ValueTuple<Vector3, ModelRenderer>>(0);
			this.{16685} = new Tlist<ModelRenderer>(0);
			foreach (KeyValuePair<string, UWModel> keyValuePair in {16657}.ModelData.Connections)
			{
				if (keyValuePair.Key.Contains("RX"))
				{
					Tlist<ValueTuple<Vector3, ModelRenderer>> tlist3 = this.{16684};
					ValueTuple<Vector3, ModelRenderer> valueTuple = new ValueTuple<Vector3, ModelRenderer>(new Vector3(1f, 0f, 0f), this.{16687}.AddObject(keyValuePair.Value));
					tlist3.Add(valueTuple);
				}
				if (keyValuePair.Key.Contains("RY"))
				{
					Tlist<ValueTuple<Vector3, ModelRenderer>> tlist4 = this.{16684};
					ValueTuple<Vector3, ModelRenderer> valueTuple = new ValueTuple<Vector3, ModelRenderer>(new Vector3(0f, 1f, 0f), this.{16687}.AddObject(keyValuePair.Value));
					tlist4.Add(valueTuple);
				}
				if (keyValuePair.Key.Contains("Lamp"))
				{
					Tlist<IsleFlares> tlist5 = this.globalLampLights;
					Vector3 {16649} = this.{16687}.Transform.Transform3X3(keyValuePair.Value.CommonSphere.Center);
					IsleFlaresSize {16650} = IsleFlaresSize.Small;
					Vector3 vector = keyValuePair.Value.CommonSphere.Center;
					IsleFlares isleFlares = new IsleFlares({16649}, {16650}, vector.GetHashCode(), 1f);
					tlist5.Add(isleFlares);
				}
				if (keyValuePair.Key.Contains("ConnectionPointLight"))
				{
					Vector3 vector2 = this.{16687}.Transform.Transform3X3(keyValuePair.Value.CommonSphere.Center);
					PointLight pointLight = new PointLight(vector2, new Vector3(255f, 246f, 229f) / 255f, 3f, 20f);
					pointLight.DrawFlares = PointLightFlaresMode.Disable;
					this.{16690}.Add(pointLight);
					Global.Render.Pointlights.Add(pointLight);
					if (keyValuePair.Key.Contains("ConnectionPointLightForLighthouse"))
					{
						pointLight.Intensivity = 0.5f;
						pointLight.Radius *= 1.1f;
					}
					else
					{
						Tlist<IsleFlares> tlist6 = this.globalLampLights;
						IsleFlares isleFlares = new IsleFlares(vector2, IsleFlaresSize.Big, vector2.GetHashCode(), 1f);
						tlist6.Add(isleFlares);
					}
				}
				if (keyValuePair.Key.Contains("ConnectionBuildingMort"))
				{
					Tlist<ModelRenderer> tlist7 = this.{16685};
					ModelRenderer modelRenderer = this.{16687}.AddObject(keyValuePair.Value);
					tlist7.Add(modelRenderer);
				}
				if (keyValuePair.Key.Contains("ConnectionIsleFume"))
				{
					Tlist<Vector3> tlist8 = this.{16682};
					Vector3 vector = this.{16687}.Transform.Transform3X3(keyValuePair.Value.CommonSphere.Center);
					tlist8.Add(vector);
				}
				if (keyValuePair.Key.Contains("ConnectionWaterfall"))
				{
					Tlist<Vector3> tlist9 = this.{16693};
					Vector3 vector = this.{16687}.Transform.Transform3X3(keyValuePair.Value.CommonSphere.Center);
					tlist9.Add(vector);
				}
			}
			foreach (ValueTuple<Vector3, ModelRenderer> valueTuple2 in ((IEnumerable<ValueTuple<Vector3, ModelRenderer>>)this.{16684}))
			{
				valueTuple2.Item2.LocalTransformOrNull = new Transform3D();
			}
			foreach (ModelRenderer modelRenderer2 in ((IEnumerable<ModelRenderer>)this.{16685}))
			{
				modelRenderer2.LocalTransformOrNull = new Transform3D();
			}
			this.CheckProceduralPreset(false);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0001015C File Offset: 0x0000E35C
		public void CheckProceduralPreset(bool {16659} = false)
		{
			IsleInstance statement = this.Statement;
			GeneratorPreset {11759};
			if (!string.IsNullOrEmpty((statement != null) ? statement.Preset : null) && Gameplay.ProceduralGeneratorPresets.TryGetValue(this.Statement.Preset, out {11759}))
			{
				this.{16679} = true;
				this.{16678} = new Ref<GeneratorPreset>({11759});
				if ({16659})
				{
					this.{16680} = true;
					this.Rebuild(this.{16678});
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000101C4 File Offset: 0x0000E3C4
		public void Dispose()
		{
			this.{16699} = true;
			if (this.CustomLoadedFlagTexture != null && this.CustomLoadedFlagTexture.AssetName == "dispose")
			{
				this.CustomLoadedFlagTexture.Tex.Dispose();
				this.CustomLoadedFlagTexture = null;
			}
			this.{16682} = null;
			this.{16693} = null;
			Tlist<ValueTuple<Vector3, ModelRenderer>> tlist = this.{16684};
			if (tlist != null)
			{
				tlist.Clear();
			}
			this.{16684} = null;
			Tlist<IsleFlares> tlist2 = this.globalLampLights;
			if (tlist2 != null)
			{
				tlist2.Clear();
			}
			this.globalLampLights = null;
			Tlist<ModelRenderer> tlist3 = this.{16685};
			if (tlist3 != null)
			{
				tlist3.Clear();
			}
			this.{16685} = null;
			Tlist<ModelRenderer> tlist4 = this.{16686};
			if (tlist4 != null)
			{
				tlist4.Clear();
			}
			this.{16686} = null;
			SmartInstancing proceduralObjects = this.ProceduralObjects;
			if (proceduralObjects != null)
			{
				proceduralObjects.Dispose();
			}
			this.ProceduralObjects = null;
			InstancedModelRenderer instancedModelRenderer = this.{16700};
			if (instancedModelRenderer != null)
			{
				instancedModelRenderer.Dispose();
			}
			this.{16700} = null;
			this.UnitsCloudObjects = null;
			if (this.{16690} != null)
			{
				foreach (PointLight {12313} in ((IEnumerable<PointLight>)this.{16690}))
				{
					Global.Render.Pointlights.Remove({12313});
				}
				this.{16690}.Clear();
				this.{16690} = null;
			}
			this.{16687}.Clear();
			ModelTransformedScene modelTransformedScene = this.{16688};
			if (modelTransformedScene != null)
			{
				modelTransformedScene.Clear();
			}
			this.GuildTowerAttackTimerMs = 0f;
			this.{16670}();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00010344 File Offset: 0x0000E544
		public void ToggleVisibleDistance()
		{
			this.{16698} = (this.{16697} && Global.Settings.LongViewDistance && {18560}.closed);
			if (this.{16698})
			{
				this.farDistMultiplier = 10f;
				return;
			}
			this.farDistMultiplier = 1f;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00010392 File Offset: 0x0000E592
		public void AddGetHp(Func<float> {16660})
		{
			this.{16689} = {16660};
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0001039C File Offset: 0x0000E59C
		public void Rebuild(Ref<GeneratorPreset> {16661})
		{
			if (this.Statement.ModelData.Model.ExternalHardpointsFlora.Length == 0)
			{
				return;
			}
			if ({16661}.Value.Name == "zero" || {16661}.Value.Layers == null)
			{
				return;
			}
			this.{16678} = {16661};
			ThreadPool.QueueUserWorkItem(delegate(object {16702})
			{
				try
				{
					Procedural procedural = new Procedural(this.Statement.ModelData.Model.ModelName, this.Statement.ModelData.Model.ExternalHardpointsFlora, this.Statement.AdditionalScale, this.Statement.GlobalTransform.CreateWorldMatrix());
					int num = 0;
					Ref<GeneratorLayer>[] layers = {16661}.Value.Layers;
					for (int i = 0; i < layers.Length; i++)
					{
						Ref<GeneratorLayer> item = layers[i];
						num++;
						if (!string.IsNullOrEmpty(item.Value.ModelKey) && item.Value.ModelKey.Length > 3 && item.Value.Scale.Start > 0f && item.Value.Scale.End >= item.Value.Scale.Start && LocalContent.Loaded.TESTProceduralModels.Any((UWModel {16703}) => {16703}.MeshName.Contains(item.Value.ModelKey)))
						{
							procedural.Append(item.Value, {16661}.Value, num);
						}
					}
					if (this.{16699})
					{
						procedural.Scene.Dispose();
					}
					else
					{
						SmartInstancing proceduralObjects = this.ProceduralObjects;
						if (proceduralObjects != null)
						{
							proceduralObjects.Dispose();
						}
						this.ProceduralObjects = procedural.Scene;
					}
				}
				catch (Exception ex)
				{
					string {20001} = "PROCEDURAL GENERATOR ERROR: " + ex.Message;
					if (!{18560}.closed)
					{
						{19994}.MeAndLogbook({19988}.InfoRed, {20001}, null);
					}
					if (Debugger.IsAttached)
					{
						Helpers.SendError(ex, "Procedural editor", true, false);
					}
				}
			});
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00010424 File Offset: 0x0000E624
		public void Update(ref FrameTime {16662}, ref Vector2 {16663})
		{
			Vector3.Distance(ref this.Statement.ModelGlobalBS.Center, ref Engine.GS.Camera.Position, out this.{16691});
			this.{16692} = Math.Max(0f, this.{16691} - this.Statement.ModelGlobalBS.Radius * 1.1f);
			if (this.{16679})
			{
				if (this.{16692} < 400f && !this.{16680})
				{
					this.{16680} = true;
					this.Rebuild(this.{16678});
				}
				if (this.{16692} > Renderer.CameraFarPlane && this.{16680})
				{
					this.{16680} = false;
					SmartInstancing proceduralObjects = this.ProceduralObjects;
					if (proceduralObjects != null)
					{
						proceduralObjects.Dispose();
					}
					this.ProceduralObjects = null;
				}
			}
			if (this.Statement.UnitsPositionCloud.Size > 0)
			{
				if (this.{16692} > 100f || Global.Settings.CrewMode != LocalSettings.CrewDisplayMode.AllShips)
				{
					this.UnitsCloudObjects = null;
				}
				else if (this.{16692} < 50f && this.UnitsCloudObjects == null)
				{
					this.UnitsCloudObjects = new IsleUnitsCloudRenderer((this.Statement.GroupID == WorldObjectID.WPort) ? (this.Statement.ModelData.Path.Contains("big") ? 100 : 70) : 10, this.Statement.GlobalTransform, this.Statement.UnitsPositionCloud, new Func<UWModel>(this.{16671}));
					this.UnitsCloudObjects.EnablePerUnitVisibleTest = true;
					IsleUnitsCloudRenderer unitsCloudObjects = this.UnitsCloudObjects;
					Func<UWModel, int, Texture2D> textureCustomizer;
					if ((textureCustomizer = Isle.<>O.<0>__GetIsleCrewTextureVariation) == null)
					{
						textureCustomizer = (Isle.<>O.<0>__GetIsleCrewTextureVariation = new Func<UWModel, int, Texture2D>(LocalContent.GetIsleCrewTextureVariation));
					}
					unitsCloudObjects.TextureCustomizer = textureCustomizer;
				}
			}
			if (this.{16692} > Renderer.CameraFarPlane * this.farDistMultiplier)
			{
				this.IsVisibleByDist = false;
				return;
			}
			IsleUnitsCloudRenderer unitsCloudObjects2 = this.UnitsCloudObjects;
			if (unitsCloudObjects2 != null)
			{
				unitsCloudObjects2.Update(ref {16662});
			}
			if (this.Statement.ModelData.Binding != WorldBindingType.None)
			{
				if (this.{16692} < 300f && this.Statement.ModelData.Binding == WorldBindingType.Fog && Rand.Chanse(10f) && Global.Player != null)
				{
					FXEngine.Template_VolumetricFog_Sample((this.Statement.ModelGlobalBSxz + Rand.DiskVector2(0f) * this.Statement.ModelGlobalBS.Radius).X0Y(), 0.5f, 4000f, 15f * (1f + this.Statement.ModelGlobalBS.Radius / 50f));
				}
				if (this.{16692} < 200f && this.Statement.ModelData.Binding == WorldBindingType.RiverMarker && Rand.Chanse(15f * {16662}.Factor))
				{
					StaticSystem staticSystem = Global.Game.StaticSystem;
					Vector2 vector = this.Statement.ModelGlobalBSxz + Rand.DiskVector2(0f) * this.Statement.ModelGlobalBS.Radius;
					staticSystem.AddOceanParticle(vector, this.Statement.ModelGlobalBS.Radius * 0.3f + 3f, false, false);
				}
				if ({18560}.closed)
				{
					this.IsVisibleByDist = false;
					return;
				}
			}
			if ({18560}.closed && (this.Statement.ModelData.IsUnderwaterDecoration || this.Statement.ModelGlobalBS.Radius < 27f) && this.{16692} > Renderer.FogEnd_Isles)
			{
				this.IsVisibleByDist = false;
				return;
			}
			if (this.Statement.ModelData.IsUnderwaterDecoration && !Global.Settings.RendererSsaoAndRefractions)
			{
				this.IsVisibleByDist = false;
				return;
			}
			if (Global.Game.GetCurrentSceneName == GameSceneName.Port && this.{16692} > Renderer.CameraFarPlane * 0.6f)
			{
				this.IsVisibleByDist = false;
				return;
			}
			FactoryPlaceIsleInfo factoryPlaceIsleInfo = this.Statement as FactoryPlaceIsleInfo;
			if (factoryPlaceIsleInfo != null && factoryPlaceIsleInfo.IsHidden)
			{
				this.IsVisibleByDist = false;
				return;
			}
			this.IsVisibleByDist = true;
			if (this.{16692} < 250f && Global.Game.GetCurrentSceneName != GameSceneName.Port && Global.Player != null)
			{
				bool isWorldmap = Global.Player.MapInfo.IsWorldmap;
			}
			this.{16670}();
			if (this.{16687}.IsMainCameraVisible)
			{
				if (this.InnerModels == null || this.InnerModels.Place.Predefines.Array[0] == FactoryType.Temp_PersonalIsle || this.{16687}.GetByTag("ConnectionLevel2").LocalVisible || this.{16693}.Size > 0)
				{
					Isle.EvaluteFumeSmoke(ref this.{16681}, ref {16662}, this.{16682}, this, this.{16693});
				}
				if (this.Statement.ModelData.IsFloating != FloatingIsleType.Impossible && Global.Player != null && {18560}.closed)
				{
					float num = 0.2f * ((this.Statement.ModelData.IsFloating == FloatingIsleType.HalfFloating) ? 0.5f : 1f);
					Vector3 vector2;
					float num2;
					CommonGlobal.CurrentClientWeather.NormalAndHeightHelper(Global.Player.MapInfo, this.{16687}.Transform.Translation.X, this.{16687}.Transform.Translation.Z, out vector2, out num2);
					this.{16687}.Transform.Translation.Y = num2 * num;
					this.{16687}.Transform.Roll = -vector2.X * num;
					this.{16687}.Transform.Pitch = -vector2.Z * num;
				}
				if (this.{16700} == null && this.Statement.TransformedShallowsPoints != null && Global.Player != null && Global.Settings.HighDetailing)
				{
					Vector2 value = Global.Player.MapInfo.IsWorldmap ? Global.Player.MapInfo.GetNearPort(this.Statement.GlobalPosition).EntryPos : Vector2.Zero;
					if (Vector2.Distance(this.Statement.GlobalPosition, value) < 1000f)
					{
						float scaleFactor = 2f;
						Tlist<UWModel> coastWreckage = LocalContent.Loaded.CoastWreckage;
						this.{16700} = new InstancedModelRenderer();
						Matrix matrix = Matrix.Invert(this.Statement.GlobalTransformWorld);
						foreach (Vector2 vector3 in ((IEnumerable<Vector2>)this.Statement.TransformedShallowsPoints))
						{
							float value2 = Vector2.Distance(vector3, value);
							float num3 = 1f - Geometry.InverseLerp(300f, 600f, value2);
							num3 *= Geometry.InverseLerp(20f, 70f, value2);
							Vector3 vector4 = Vector3.Transform(vector3.X0Y(), matrix);
							for (int i = 0; i < (int)(16f * num3); i++)
							{
								vector4 += Rand.NextVector2(-1f, 1f).X0Y() * 7.5f * scaleFactor;
								Matrix matrix2 = Matrix.CreateScale(Rand.Range(1f, 2.2f) / this.Statement.AdditionalScale) * Matrix.CreateRotationY(Rand.Angle()) * Matrix.CreateTranslation(vector4.X, vector4.Y, vector4.Z);
								this.{16700}.AddToCache(coastWreckage.Rand(), ref matrix2);
							}
						}
					}
				}
				IslePortInfo islePortInfo = this.Statement as IslePortInfo;
				if (islePortInfo != null)
				{
					IslePortInfo islePortInfo2 = islePortInfo;
					ShipCurrentPlayer player = Global.Player;
					if (islePortInfo2 == ((player != null) ? player.NearPort : null) && Session.ServerWorldStatus.NearPortHasDisorder)
					{
						int num4 = 0;
						foreach (KeyValuePair<PortTipConnection, ObservableTlist<Vector3>> keyValuePair in islePortInfo.VisualTips)
						{
							PortTipConnection key = keyValuePair.Key;
							bool flag = key <= PortTipConnection.Workshop;
							if (flag)
							{
								foreach (Vector3 value3 in ((IEnumerable<Vector3>)keyValuePair.Value))
								{
									num4++;
									if (Rand.Chanse(20f))
									{
										Vector3 vector5 = value3 - new Vector3(0f, 10f, 0f);
										vector5.Y = Math.Max(vector5.Y, 2.5f);
										FXEngine.SampleFlameAndSmoke(vector5 + HashHelper.SphericalVectorFromLerp((float)num4).XZ().X0Y() * 5f, 2.5f, false, true, false, null, 1f);
									}
								}
							}
						}
					}
				}
				if (this.{16700} != null && Global.Player != null)
				{
					if (!Global.Settings.HighDetailing)
					{
						this.{16700}.Dispose();
						this.{16700} = null;
					}
					else
					{
						this.{16700}.ModifyMatrices(new InstancedModelRenderer.RefAction<Matrix>(this.{16674}), (Global.Game.GameTime.FpsCounter.Avg > 90f) ? 9 : 5);
					}
				}
			}
			IsleInnerModelsHelper innerModels = this.InnerModels;
			if (innerModels != null)
			{
				innerModels.Update(this.{16687}, this.{16692});
			}
			if (this.{16689} != null && this.{16686}.Size > 0)
			{
				float num5 = this.{16689}();
				int num6 = (int)Math.Round((double)((float)this.{16686}.Size * num5));
				for (int j = 0; j < this.{16686}.Size; j++)
				{
					this.{16686}.Array[j].LocalVisible = (j < num6);
				}
			}
			if (this.{16684}.Size > 0)
			{
				Vector3 value4 = new Vector3((float)(Global.Game.GameTotalTimeSec * 3.1415927410125732 / 3.0), (float)Math.Sin(Global.Game.GameTotalTimeSec * 1.5), 0f);
				foreach (ValueTuple<Vector3, ModelRenderer> valueTuple in ((IEnumerable<ValueTuple<Vector3, ModelRenderer>>)this.{16684}))
				{
					valueTuple.Item2.LocalTransformOrNull.CreateCenterPivotRotation(valueTuple.Item2.Model.CommonSphere.Center, valueTuple.Item1 * value4);
				}
			}
			if (this.{16685}.Size > 0 && this.DynamicObjectUID != null)
			{
				ValueTuple<Isle, DynamicBuildCreatePacket, float> isleByUID = Global.Game.InteractiveWorldSystem.GetIsleByUID(this.DynamicObjectUID.Value);
				if (isleByUID.Item1 != null)
				{
					int num7 = 0;
					foreach (float num8 in Gameplay.DyanmicBuildingsTable.Array[(int)isleByUID.Item2.ModelID].VisualHelperMortars(this.{16687}.Transform.CreateWorldMatrix(), Global.Game.WorldInstance.EnumerateAllPlayers(), (Player {16701}) => true, WosbPbs.TowerDesription[InitialDynamicBuildingId.FortTowerMortar].DeadZone, WosbPbs.TowerDesription[InitialDynamicBuildingId.FortTowerMortar].MaxDistance))
					{
						ModelRenderer modelRenderer = this.{16685}.Array[num7++];
						Geometry.AngularMovement(ref modelRenderer.LocalTransformOrNull.Yaw, num8 + 1.5707964f, {16662}.secElapsed);
						modelRenderer.LocalTransformOrNull.CreateCenterPivotRotation(modelRenderer.Model.CommonSphere.Center, new Vector3(0f, modelRenderer.LocalTransformOrNull.Yaw, 0f));
					}
				}
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0001105C File Offset: 0x0000F25C
		public static void EvaluteFumeSmoke(ref float {16664}, ref FrameTime {16665}, Tlist<Vector3> {16666}, Isle {16667}, Tlist<Vector3> {16668})
		{
			{16664} += {16665}.msElapsed;
			if ({16664} > 250f)
			{
				{16664} = 0f;
				for (int i = 0; i < {16666}.Size; i++)
				{
					if (Rand.Chanse(90f))
					{
						FXEngine.SampleFumesSmoke({16666}.Array[i], 0.5f, 0.6f, 1f);
					}
				}
				if ({16667} != null)
				{
					foreach (Vector3 {14815} in ((IEnumerable<Vector3>){16667}.Statement.ModelData.VulcanoSpawns))
					{
						Vector3 vector = {16667}.{16687}.Transform.Transform3X3({14815});
						if (Engine.GS.Camera.IsVisible(vector, 10f) && Rand.Chanse(50f * {16665}.Factor))
						{
							FXEngine.SampleFlameAndSmoke(vector, 20f, true, true, false, null, 1f);
							for (int j = 0; j < 1; j++)
							{
								FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall(vector, (Vector3.Up + Rand.NextVector3(-1f, 1f).Normal()) * 10f), 30f, 1000f, 5000f, 10f, false, FXEngine.PowderParticleType.Gray, 1f, null);
								FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall(vector, (Vector3.Up + Rand.NextVector3(-1f, 1f).Normal()) * 15f), 50f, 1000f, 6000f, 10f, false, FXEngine.PowderParticleType.Dark, 1f, null);
								FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall(vector, (Rand.NextVector3(-1f, 1f).Normal() - Vector3.Up) * 10f), 30f, 1000f, 10000f, 2f, false, FXEngine.PowderParticleType.Dark, 0.7f, null);
							}
							if (Rand.Chanse(5f * {16665}.Factor))
							{
								MortarShotParameters {24921} = new MortarShotParameters(new Vector2(100f, 150f), vector, new Vector3(Rand.Range(-1f, 1f), Rand.Range(-1f, 1f), 1f), Gameplay.BallsInfo.FromID(6), CannonFeature.HeavyMortar);
								Global.Game.WorldInstance.CreateMortarShot(-1, -1, {24921});
							}
						}
					}
				}
				for (int k = 0; k < {16668}.Size; k++)
				{
					Vector3 vector2 = {16668}.Array[k];
					FXEngine.CreateSingleWaterParticle2((vector2 + (Engine.GS.Camera.Position - vector2).Normal()) * new Vector3(1f, 0f, 1f) + 2f * Rand.NextVector3(-1f, 1f), Rand.Range(5f, 10f), false, 0f);
					for (int l = 0; l < 20; l++)
					{
						FXEngine.CreateSingleWaterParticle2((vector2 + 0.5f * (Engine.GS.Camera.Position - vector2).Normal()) * new Vector3(1f, 0f, 1f) + 2f * Rand.NextVector3(-1f, 1f) + new Vector3(0f, vector2.Y - 2f, 0f), Rand.Range(1f, 2f), true, 8f);
					}
				}
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00011464 File Offset: 0x0000F664
		public void Render3D()
		{
			if (Renderer.ReflectionsAreBeingDrawn && this.Statement.ModelData.IsUnderwaterDecoration)
			{
				return;
			}
			if (this.replacebleTerrainXfxID == 22 || this.replacebleTerrainXfxID == 28)
			{
				Global.Render.CommonShader.SetOrResetTextureReplaceDesign(new int?(9999));
			}
			if (this.replacebleTerrainXfxID == 24)
			{
				Global.Render.CommonShader.SetOrResetTextureReplaceDesign(new int?(9998));
			}
			UWModel {11962} = (this.{16692} > (Renderer.ReflectionsAreBeingDrawn ? (Global.Render.LODDistanceForIsles / 4f) : Global.Render.LODDistanceForIsles)) ? this.Statement.ModelData.ModelLod1 : this.Statement.ModelData.Model;
			this.{16687}.SetModelData(0, {11962});
			if (this.{16698} && this.{16692} + this.Statement.ModelGlobalBS.Radius > Renderer.CameraFarPlane * 0.85f && {18560}.closed)
			{
				float num = this.{16692} - Renderer.CameraFarPlane * 0.85f + this.Statement.ModelGlobalBS.Radius;
				Vector2 vector = (Engine.GS.Camera.Position.XZ() - this.{16687}.Transform.Translation.XZ()).Normal();
				Transform3D transform = this.{16687}.Transform;
				transform.Translation.X = transform.Translation.X + vector.X * num * 0.92f;
				Transform3D transform2 = this.{16687}.Transform;
				transform2.Translation.Z = transform2.Translation.Z + vector.Y * num * 0.92f;
				float num2 = 1f / (1f + (num + num * num * 0.001f) / 1000f);
				this.{16687}.Transform.Translation.Y = this.{16687}.Transform.Translation.Y * num2 - this.Statement.ModelGlobalBS.Radius * 0.04f * (1f - num2);
				this.{16687}.Transform.MiddleScale *= num2;
			}
			if (this.Editor_SceneObjectTexture != null)
			{
				Global.Render.CommonShader.SetSubstituteTexture(this.Editor_SceneObjectTexture);
				Global.Render.CommonShader.RenderIsle(this.{16687}, this.Statement.ModelData.IsFloating > FloatingIsleType.Impossible, this.replacebleTerrainXfxID);
				Global.Render.CommonShader.SetSubstituteTexture(null);
			}
			else
			{
				Global.Render.CommonShader.RenderIsle(this.{16687}, this.Statement.ModelData.IsFloating > FloatingIsleType.Impossible, this.replacebleTerrainXfxID);
			}
			if (this.{16698})
			{
				this.{16687}.Transform.CopyFrom(this.{16696});
			}
			if (this.replacebleTerrainXfxID == 22 || this.replacebleTerrainXfxID == 24 || this.replacebleTerrainXfxID == 28)
			{
				Global.Render.CommonShader.SetOrResetTextureReplaceDesign(null);
			}
			if (this.{16687}.IsMainCameraVisible && !Renderer.ReflectionsAreBeingDrawn)
			{
				if (this.ProceduralObjects != null)
				{
					this.ProceduralObjects.AlwaysMinLod = (Global.Settings.FloraQuality <= LocalSettings.FloraQualityMode.Normal);
					this.ProceduralObjects.MinThrottling = ((Global.Settings.FloraQuality == LocalSettings.FloraQualityMode.Low) ? 0.33f : 0f);
					this.ProceduralObjects.Update(Engine.GS.Camera.Position, this.Statement.GlobalTransform.CreateWorldMatrix());
					Global.Render.CommonShader.RenderObjectInstanced(this.{16687}, this.ProceduralObjects, 1f);
				}
				if (this.{16700} != null)
				{
					Global.Render.CommonShader.RenderObjectInstanced(this.{16687}, this.{16700}, 1f);
				}
				if (this.UnitsCloudObjects != null)
				{
					this.UnitsCloudObjects.Render3D();
				}
				Global.RenderStats.IsleRenderCount++;
				if (this.{16688} != null)
				{
					for (int i = 0; i < this.{16688}.CountModels; i++)
					{
						ModelRenderer modelRenderer = this.{16688}.GetModels[i];
						modelRenderer.LocalTransformOrNull.CreateCenterPivotRotation(modelRenderer.Model.MeshParts[0].LocalSpaceBoundingBox.Min, new Vector3(0f, Global.Game.WorldInstance.LastWindAxis - this.{16687}.Transform.Yaw, 0f));
					}
					Global.Render.CommonShader.ConfigureAnimatedMeshInNextRenderObject(Vector2.Zero, AnimationType.Flagpoll);
					if (this.CustomLoadedFlagTexture != null)
					{
						Global.Render.CommonShader.SetSubstituteTexture(this.CustomLoadedFlagTexture.Tex);
						Global.Render.CommonShader.RenderObject(this.{16688}, false, 1f, false, 0f, false);
						Global.Render.CommonShader.SetSubstituteTexture(null);
					}
					else
					{
						Global.Render.CommonShader.RenderObject(this.{16688}, false, 1f, false, 0f, false);
					}
					Global.Render.CommonShader.ConfigureAnimatedMeshInNextRenderObject(Vector2.Zero, AnimationType.Flora);
				}
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00003100 File Offset: 0x00001300
		public void Render3DStatic()
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000119A4 File Offset: 0x0000FBA4
		public void CheckVisibilityAndRenderToGBuffer(IGBufferBuilder {16669})
		{
			if (!this.{16687}.IsMainCameraVisible)
			{
				return;
			}
			this.{16687}.CheckVisibilityAndRenderGBuffer({16669}, (this.replacebleTerrainXfxID == 22 || this.replacebleTerrainXfxID == 24 || this.replacebleTerrainXfxID == 28 || this.replacebleTerrainXfxID == 25) ? new Func<ModelPartShadercall, bool>(this.{16676}) : null);
			SmartInstancing proceduralObjects = this.ProceduralObjects;
			if (proceduralObjects == null)
			{
				return;
			}
			proceduralObjects.Draw({16669}, this.{16687});
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00011A1C File Offset: 0x0000FC1C
		private void {16670}()
		{
			if (this.{16695})
			{
				this.{16695} = false;
				foreach (Ship {24935} in ((IEnumerable<Ship>)this.{16694}))
				{
					Global.Game.WorldInstance.RemoveDecoration({24935});
				}
				this.{16694}.Clear();
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00011A8C File Offset: 0x0000FC8C
		[CompilerGenerated]
		private UWModel {16671}()
		{
			return LocalContent.GetIsleUnitModel(this);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00011A94 File Offset: 0x0000FC94
		[NullableContext(1)]
		[CompilerGenerated]
		private bool {16672}(PlayerShipInfo {16673})
		{
			return {16673}.CanBeUsedForNpc && {16673}.Rank >= Gameplay.WorldMap.GetNearPort(this.Statement.GlobalPosition).BuildShipRangs;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		[CompilerGenerated]
		private void {16674}(ref Matrix {16675})
		{
			Vector3 translation = {16675}.Translation;
			Vector3.Transform(ref translation, ref this.Statement.GlobalTransformWorld, out translation);
			{16675}.M42 = CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, translation.X, translation.Z) / this.Statement.GlobalTransform.Scales.Y - this.Statement.GlobalTransform.Translation.Y / this.Statement.GlobalTransform.Scales.Y;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00011B58 File Offset: 0x0000FD58
		[CompilerGenerated]
		private bool {16676}(ModelPartShadercall {16677})
		{
			if (this.replacebleTerrainXfxID == 25)
			{
				return {16677}.Material.Terrain == null;
			}
			return {16677}.Material.Albedo == null || !LocalContent.DisallowWithFlora({16677}.Material.Albedo.AssetName);
		}

		// Token: 0x0400018B RID: 395
		private const float c_effectParticleTimer = 250f;

		// Token: 0x0400018C RID: 396
		private const float ProceduralActivateDistToCamera = 400f;

		// Token: 0x0400018D RID: 397
		private const float UnitsCloudActivateDistToCamera = 50f;

		// Token: 0x0400018E RID: 398
		public readonly IsleInstance Statement;

		// Token: 0x0400018F RID: 399
		public int? DynamicObjectUID;

		// Token: 0x04000190 RID: 400
		public bool IsVisibleByDist;

		// Token: 0x04000191 RID: 401
		public VirtualTexture CustomLoadedFlagTexture;

		// Token: 0x04000192 RID: 402
		public Texture2D Editor_SceneObjectTexture;

		// Token: 0x04000193 RID: 403
		private Ref<GeneratorPreset> {16678};

		// Token: 0x04000194 RID: 404
		private bool {16679};

		// Token: 0x04000195 RID: 405
		private bool {16680};

		// Token: 0x04000196 RID: 406
		private float {16681};

		// Token: 0x04000197 RID: 407
		private Tlist<Vector3> {16682};

		// Token: 0x04000198 RID: 408
		private Tlist<ShipPositionInfo> {16683} = new Tlist<ShipPositionInfo>();

		// Token: 0x04000199 RID: 409
		private Tlist<ValueTuple<Vector3, ModelRenderer>> {16684};

		// Token: 0x0400019A RID: 410
		private Tlist<ModelRenderer> {16685};

		// Token: 0x0400019B RID: 411
		private Tlist<ModelRenderer> {16686};

		// Token: 0x0400019C RID: 412
		private ModelTransformedScene {16687};

		// Token: 0x0400019D RID: 413
		private ModelTransformedScene {16688};

		// Token: 0x0400019E RID: 414
		private Func<float> {16689};

		// Token: 0x0400019F RID: 415
		internal int replacebleTerrainXfxID;

		// Token: 0x040001A0 RID: 416
		public Tlist<IsleFlares> globalLampLights = new Tlist<IsleFlares>();

		// Token: 0x040001A1 RID: 417
		private Tlist<PointLight> {16690} = new Tlist<PointLight>();

		// Token: 0x040001A2 RID: 418
		private float {16691};

		// Token: 0x040001A3 RID: 419
		private float {16692};

		// Token: 0x040001A4 RID: 420
		private Tlist<Vector3> {16693};

		// Token: 0x040001A5 RID: 421
		private Tlist<Ship> {16694} = new Tlist<Ship>();

		// Token: 0x040001A6 RID: 422
		private bool {16695};

		// Token: 0x040001A7 RID: 423
		private Transform3D {16696};

		// Token: 0x040001A8 RID: 424
		private bool {16697};

		// Token: 0x040001A9 RID: 425
		private bool {16698};

		// Token: 0x040001AA RID: 426
		internal float farDistMultiplier = 1f;

		// Token: 0x040001AB RID: 427
		public SmartInstancing ProceduralObjects;

		// Token: 0x040001AC RID: 428
		public IsleUnitsCloudRenderer UnitsCloudObjects;

		// Token: 0x040001AD RID: 429
		public IsleInnerModelsHelper InnerModels;

		// Token: 0x040001AE RID: 430
		public float GuildTowerAttackTimerMs;

		// Token: 0x040001AF RID: 431
		public bool AggressiveNow;

		// Token: 0x040001B0 RID: 432
		private bool {16699};

		// Token: 0x040001B1 RID: 433
		private InstancedModelRenderer {16700};

		// Token: 0x02000047 RID: 71
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040001B2 RID: 434
			public static Func<UWModel, int, Texture2D> <0>__GetIsleCrewTextureVariation;
		}
	}
}
