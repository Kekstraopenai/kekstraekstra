using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Graphics;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Components.Architecture;
using TheraEngine.Components.Scene;
using TheraEngine.Core;
using TheraEngine.Graphics;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Renderer;
using TheraEngine.Scene.Lighting;
using TheraEngine.Scene.ParticleSystem;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics.Shaders;
using World_Of_Sea_Battle.Interface;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Graphics
{
	// Token: 0x02000449 RID: 1097
	internal sealed class Renderer
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x000329D4 File Offset: 0x00030BD4
		public static float CameraNearPlane
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x000CB71D File Offset: 0x000C991D
		private static float _CameraFarPlane
		{
			get
			{
				if (!{18560}.closed && {18560}.visibility != {18560}.VisibleDistance.Normal)
				{
					return 8000f;
				}
				if (Global.Settings == null || !Global.Settings.LongViewDistance)
				{
					return 700f;
				}
				return 900f;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x00089530 File Offset: 0x00087730
		public static float SsaaScale
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x000CB751 File Offset: 0x000C9951
		public static float FogStart_Isles
		{
			get
			{
				if (!Global.Settings.LongViewDistance)
				{
					return 100f;
				}
				return 120f;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x000CB76A File Offset: 0x000C996A
		public static float FogEnd_Isles
		{
			get
			{
				return 550f;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x000CB771 File Offset: 0x000C9971
		private bool shadows
		{
			get
			{
				return Global.Settings.ShadowsQuality > LocalSettings.RendererShadows.Off;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x000CB780 File Offset: 0x000C9980
		internal bool mrtNormals
		{
			get
			{
				return Global.Settings.RendererSsaoAndRefractions;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x000CB780 File Offset: 0x000C9980
		private bool ssao
		{
			get
			{
				return Global.Settings.RendererSsaoAndRefractions;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x000CB78C File Offset: 0x000C998C
		private bool antialiasingTxaa
		{
			get
			{
				return Global.Settings.AntialiasingQuality == LocalSettings.RendererAntialias.Txaa;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x000CB79B File Offset: 0x000C999B
		private bool antialiasingSsaa
		{
			get
			{
				return Global.Settings.AntialiasingQuality == LocalSettings.RendererAntialias.Fsaa;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x000CB7AA File Offset: 0x000C99AA
		private bool msaa
		{
			get
			{
				return !this.mrtNormals && this.antialiasingSsaa;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x000CB7BC File Offset: 0x000C99BC
		private bool sceneReflections
		{
			get
			{
				return Global.Settings.RendererDynamicReflections;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x000CB7C8 File Offset: 0x000C99C8
		private bool sceneFullReflectionsAA
		{
			get
			{
				return !this.antialiasingTxaa;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x000CB7D3 File Offset: 0x000C99D3
		private bool hqOceanNormals
		{
			get
			{
				return Global.Settings.EnableBasicEffects;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x000CB7DF File Offset: 0x000C99DF
		private bool heatMap
		{
			get
			{
				return this.mrtNormals && Global.Settings.HeatDeformations;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x000CB7F5 File Offset: 0x000C99F5
		private bool improvedPostprocess
		{
			get
			{
				return Global.Settings.ImprovedPostProcess;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x000CB801 File Offset: 0x000C9A01
		private bool colorGrading
		{
			get
			{
				return Global.Settings.PostprocessorType > LocalSettings.Postprocessor.Basic;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x000CB810 File Offset: 0x000C9A10
		private bool lspoEnable
		{
			get
			{
				return InputHelper.GraphicsTestKeyDown && false;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x000CB81C File Offset: 0x000C9A1C
		private float OverlayGameLogo
		{
			get
			{
				return Math.Max(this.UiMode == InterfaceMode.Off, 1f - Global.Game.GetInterfaceManager.Host.Opacity);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x000030FD File Offset: 0x000012FD
		public bool shadowsOnlyCascade3
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x000CB847 File Offset: 0x000C9A47
		public bool reduceShipsInReflections
		{
			get
			{
				return this.PhysicallyOverloadingFactor > 0.7f;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x000CB847 File Offset: 0x000C9A47
		public bool reduce3dEffects
		{
			get
			{
				return this.PhysicallyOverloadingFactor > 0.7f;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x000CB856 File Offset: 0x000C9A56
		public SceneManager GetSceneManager
		{
			get
			{
				return this.{23444};
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x000CB85E File Offset: 0x000C9A5E
		// (set) Token: 0x060017C1 RID: 6081 RVA: 0x000CB866 File Offset: 0x000C9A66
		public float LogicallyOverloadingFactor { get; private set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x000CB86F File Offset: 0x000C9A6F
		public float PhysicallyOverloadingFactor
		{
			get
			{
				return Geometry.Saturate((60f - Global.Game.GameTime.FpsCounter.Avg) / 30f);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x000CB896 File Offset: 0x000C9A96
		public float FogLevel
		{
			get
			{
				return Global.Render.GetSceneManager.CurrentFogLevel;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x000CB8A7 File Offset: 0x000C9AA7
		public float CloudeLevel
		{
			get
			{
				return Global.Render.GetSceneManager.CurrentCloudyLevel;
			}
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x000CB8B8 File Offset: 0x000C9AB8
		public void LoadContentAndInitialize(ContentManager {23400})
		{
			Global.Camera = new GameCamera();
			Engine.GS.Camera = Global.Camera;
			this.ShadowCascades = new ShadowMappingEngine[]
			{
				new ShadowMappingEngine(ShadowMapEngineOptions.Default, 0, 10f, 20f, 10f, 0f),
				new ShadowMappingEngine(ShadowMapEngineOptions.Default, 0, 20f, 100f, 20f, 0f),
				new ShadowMappingEngine(ShadowMapEngineOptions.Default, 0, 100f, 500f, 60f, 0f)
			};
			this.CascadedShadowHelper = new CascadedShadowMap(this.ShadowCascades);
			Global.Game.EvSizeChanged += this.{23430};
			this.UpdateRenderTargets();
			this.{23444} = new SceneManager(1f, SceneColorSet.Normal, () => Global.Camera.Position);
			this.{23445} = new LoadingScreenRenderer();
			this.ItemsShader = new ParticlesAndStaticMesh();
			this.ItemsShader.ManualSetFog(140f, 210f);
			this.PostProcess = new WorldOfSeaBattlePostProcess();
			this.OceanNormalMapGenerator = new OceanNormalMapGenerator();
			this.Pointlights = new PointLightArrayHolder(8, 50f);
			this.txFogOfWar = new Texture2D(Engine.GS.graphicsDevice, 36, 36, false, SurfaceFormat.Color);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x000CBA14 File Offset: 0x000C9C14
		public void InitializeFromLoadContentThread()
		{
			this.{23447} = new SoftTrigger(0f, 1f, 1.5f);
			this.CommonShader = new CommonWorldShader(Global.Game.Content);
			this.GlobalIlluminationMapBuilder = new GSSDO();
			this.GlobalIlluminationMapBuilder.LessStart = 130f;
			this.GlobalIlluminationMapBuilder.LessEnd = 350f;
			this.AmbientLightOcclusionTest = new LightSourceOcclusionTest();
			this.LightsOcclusion = new DynamicOcclusionSystem(this.{23459});
			this.SpyglassEffect = new SpyglassShader();
			this.HdrColorGradients = new HdrColorGradients();
			Global.Game.EvEndLoading += this.{23432};
			this.WorldMapRegionEffect = Global.Game.Content.Load<Effect>("Shaders\\PostProcess\\WorldMapRegions");
			this.DenoiseEffect = Global.Game.Content.Load<Effect>("Shaders\\PostProcess\\Denoise");
			this.{23440} = Global.Game.Content.Load<Effect>("Shaders\\2D\\MRTHelper");
			this.{23441} = Global.Game.Content.Load<Effect>("Shaders\\PostProcess\\PostProcessPass2");
			this.MinimapCircleStencil = Global.Game.Content.Load<Effect>("Shaders\\2D\\CircleStencil");
			this.{23442} = Global.Game.Content.Load<Effect>("Shaders\\Filters\\BokehBlur");
			this.{23443} = Global.Game.Content.Load<Effect>("Shaders\\PostProcess\\CAS");
			this.oceanSourceNormal = Global.Game.Content.Load<Texture2D>("Textures\\rwocean\\term");
			this.{23448} = new GaussianBlurShader();
			Texture2D texture2D = Global.Game.Content.Load<Texture2D>("Textures\\grading_test");
			Texture2D texture2D2 = Global.Game.Content.Load<Texture2D>("Textures\\grading_reshade_basic");
			this.{23456} = new ColorGradingHelper(texture2D, 64, 64, false, Color.Red, Color.Green);
			this.{23457} = new ColorGradingHelper(texture2D2, 64, 64, false, Color.Red, Color.Green);
			texture2D.Dispose();
			texture2D2.Dispose();
			this.LightFlareRender = new LightFlareRender(new Rectangle(1958, 2247, 128, 128), AtlasObjs.transparent_1px, new Rectangle(1957, 1565, 164, 164), new Rectangle(1565, 1959, 256, 256), new Rectangle(2068, 1252, 311, 311), new Rectangle(2122, 1565, 128, 128));
			this.ParticleManager2D = new ParticleManager2D(3000, 1000);
			this.ParticleManager3D = new ParticleManager3D(9000, 210f);
			Global.Game.EvChangeScene += this.{23433};
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x000CBCD7 File Offset: 0x000C9ED7
		public void InitializeLoadingScreen(ContentManager {23401})
		{
			this.{23445}.Initialize({23401});
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x000CBCE5 File Offset: 0x000C9EE5
		public void UploadLoadingScreen()
		{
			this.{23445}.Upload();
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x000CBCF4 File Offset: 0x000C9EF4
		public void Update(ref FrameTime {23402}, GameSceneName {23403})
		{
			SpyglassUi.Update({23402});
			Renderer.CameraFarPlane = Renderer._CameraFarPlane;
			this.ParticleManager2D.Update(ref {23402});
			this.ParticleManager3D.Update(CommonGlobal.CurrentClientWeather.WindDirection, ref {23402});
			if (this.{23455}.Sample(ref {23402}) && Global.Game.IsActive)
			{
				Materials.TexturesDatabase.UnloadUnused(500);
			}
			this.PostProcess.Update(ref {23402});
			this.PostProcess.BloodScreenIntensity = 0f;
			this.{23444}.OverrideLightDirection = null;
			if (Global.Game.GetCurrentSceneName == GameSceneName.Game && Global.Player != null)
			{
				float hpFactor = Global.Player.UsedShip.HpFactor;
				float num = (float)Math.Sin(Global.Game.GameTotalTimeSec * 2.4) * 0.4f + 0.6f;
				if (hpFactor < 0.25f)
				{
					this.PostProcess.BloodScreenIntensity = Geometry.Saturate({19215}.IsOpen ? 0f : (Math.Min(1f, 1f - hpFactor / 0.25f * (hpFactor / 0.25f)) * 0.75f * num));
				}
				MapForPassingInfo mapForPassingInfo = Global.Player.MapInfo as MapForPassingInfo;
				if (mapForPassingInfo != null && mapForPassingInfo.VisualEffects == 1)
				{
					this.{23444}.OverrideLightDirection = new Vector3?(new Vector3(-0.5f, -0.2f, 0f).Normal());
					this.{23444}.MoonLightSource.InitializeUv(AtlasObjs.moonTextureRed, AtlasObjs.Texture.Size);
				}
				else
				{
					this.{23444}.MoonLightSource.InitializeUv(AtlasObjs.moonTexture, AtlasObjs.Texture.Size);
				}
			}
			if ({23403} != GameSceneName.Loading)
			{
				this.CommonShader.Update(ref {23402});
				if (Global.Settings.kb_Screenshoot.IsClick)
				{
					this.{23446} = true;
				}
				if (Global.Settings.kb_FreeMode.IsClick)
				{
					if (Global.Game.GetCurrentSceneName == GameSceneName.Entry)
					{
						this.UiMode = ((this.UiMode == InterfaceMode.Default) ? InterfaceMode.Off : InterfaceMode.Default);
					}
					else
					{
						this.UiMode = EnumHelper.RollValue<InterfaceMode>(this.UiMode);
					}
				}
				if ((Global.Settings.kb_Map.IsDown || Global.Settings.kb_KeyShowMouse.IsDown) && this.UiMode == InterfaceMode.Off)
				{
					this.UiMode = InterfaceMode.Default;
				}
			}
			if ({23403} == GameSceneName.Game)
			{
				this.LogicallyOverloadingFactor = 0.4f * (1f - this.ParticleManager3D.CurrentPerformanceFactor);
				float num2 = (float)Global.Game.WorldInstance.EnumerateShips(true, true, true).Count((Ship {23461}) => ({23461} as IClientShip).GetClient.IsVisibleWithOcclusionQueryAndCorpusTransparancy);
				float num3 = 5f;
				float num4 = 14f;
				float val = Geometry.Saturate((num2 - num3) / (num4 - num3));
				this.LogicallyOverloadingFactor = Math.Max(this.LogicallyOverloadingFactor, val);
			}
			else
			{
				this.LogicallyOverloadingFactor = 0f;
			}
			this.LODDistanceForIsles = MathHelper.Lerp(150f, 110f, this.LogicallyOverloadingFactor);
			this.LODDistanceForShips = MathHelper.Lerp(60f, 50f, this.LogicallyOverloadingFactor);
			this.LODDistanceForShips_Lod2 = MathHelper.Lerp(90f, 80f, this.LogicallyOverloadingFactor);
			this.LODDistanceForShips_Lod3 = MathHelper.Lerp(150f, 110f, this.LogicallyOverloadingFactor);
			this.{23447}.Evalute(ref {23402}, Global.Game.GetInterfaceManager.MaxFillingTheScreenFactor > 0.47f * Global.Game.AdaptiveUiExtraScale && Global.Game.GetCurrentSceneName != GameSceneName.Entry && {21544}.CurrentInstance == null && !{19413}.IsOpen);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x000CC0A0 File Offset: 0x000CA2A0
		private void {23404}()
		{
			if (OtherTextures.GameLogoOverlay.LoadQuery && Debugging.EnableOverlay)
			{
				Engine.GS.SetTexture(OtherTextures.GameLogoOverlay.Tex);
				float scaleFactor = 0.8f;
				Vector2 value = OtherTextures.GameLogoOverlay.Tex.Bounds.WidthHeight() * scaleFactor;
				Device gs = Engine.GS;
				Rectangle bounds = OtherTextures.GameLogoOverlay.Tex.Bounds;
				Vector2 vector = Engine.GS.UIArea.WidthHeight() - value;
				Rectangle rectangle = new Marker(ref vector, ref value).ToRect();
				Color color = Color.White * this.OverlayGameLogo;
				gs.Draw(bounds, rectangle, color);
			}
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x000CC158 File Offset: 0x000CA358
		public void GameDraw(Scene {23405}, GameSceneName {23406}, Renderable2DCollection {23407})
		{
			this.{23460}++;
			if ({23406} == GameSceneName.Loading)
			{
				this.{23445}.TryDraw();
				return;
			}
			if ({23405} != null)
			{
				if (this.shadows)
				{
					Engine.GS.SetColorBlendState(BlendState.Opaque);
					Engine.GS.SetDepthBuffer(DepthBuffer.ReadAndWrite);
					Vector3 currentLightDirectionStepped = this.{23444}.CurrentLightDirectionStepped;
					Vector2 {12361} = (this.antialiasingTxaa && this.{23453} < 0.5f) ? (Renderer.triTxaa[this.{23449}] * ((0.5f - this.{23453}) / 0.5f)) : Vector2.Zero;
					Engine.GS.graphicsDevice.RasterizerState.MultiSampleAntiAlias = false;
					Engine.GS.graphicsDevice.RasterizerStateCullingEnable = false;
					for (int i = 0; i < 3; i++)
					{
						ShadowMappingEngine shadowMappingEngine = this.ShadowCascades[i];
						shadowMappingEngine.UseAlphaMaterials = (i <= 1);
						Renderer.CurrentShadowMapIndex = i;
						if (i == 0)
						{
							if (Global.Player != null)
							{
								float corpusHalfLength = Global.Player.UsedShip.StaticInfo.CorpusHalfLength;
								shadowMappingEngine.BeginDrawing(currentLightDirectionStepped, Global.Player.Position3D, {12361}, Vector3.Zero ^ 4f, 0.5f + corpusHalfLength * 0.06f);
								Global.Game.WorldInstance.RenderToGBufferCurrentPlayer(shadowMappingEngine);
							}
						}
						else
						{
							shadowMappingEngine.BeginDrawing(currentLightDirectionStepped, Engine.GS.Camera.Position, {12361}, default(Vector3), 1f);
							if (i == 1)
							{
								Global.Game.WorldInstance.RenderToGBufferCurrentPlayer(shadowMappingEngine);
							}
							if (i == 2 && this.shadowsOnlyCascade3)
							{
								Global.Game.WorldInstance.RenderToGBufferCurrentPlayer(shadowMappingEngine);
							}
							Global.Game.WorldInstance.Render3DToGBuffer(shadowMappingEngine, true);
							Global.Game.WorldInstance.Render3DToGBuffer(shadowMappingEngine, false);
							Global.Game.ScenePort.Render3DMainSceneGBuffer(shadowMappingEngine);
							foreach (IGraphicsElement graphicsElement in ((IEnumerable<IGraphicsElement>)Global.Game.Scenes))
							{
								graphicsElement.RenderGBuffer(shadowMappingEngine);
							}
						}
					}
					this.CascadedShadowHelper.Construct();
				}
				this.{23444}.CurrentCloudyLevel = Math.Min(1f, CommonGlobal.CurrentClientWeather.RainingLevelClient + CommonGlobal.CurrentClientWeather.FogLevelClientOfExtra * 0.6f);
				this.{23444}.CurrentFogLevel = Math.Max((1f - Global.Game.StaticSystem.GetSkyShader.DayOrNight) * 0.4f + 0.1f, Math.Max(Global.Game.StaticSystem.StrongEffectAmount, CommonGlobal.CurrentClientWeather.FogLevelClient));
				this.{23444}.CurrentCloudyLevel = Math.Max(0.72f * Global.Game.StaticSystem.StrongEffectAmount, this.{23444}.CurrentCloudyLevel);
				this.ItemsShader.SetBasicProperties(Engine.GS.Camera, AtlasObjs.Texture, this.{23444}, Global.Game.StaticSystem.GetSkyShader);
				this.CommonShader.PrepareForRenderCommon();
				Engine.GS.SetRenderTarget(this.rtOceanNormals);
				this.OceanNormalMapGenerator.Generate(this.oceanSourceNormal, (float)(0.03 * Global.Game.GameTotalTimeSec));
				Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
				Engine.GS.SetRenderTarget(this.rtSsEffects);
				Engine.GS.SetRasterizerStateOptions(false, false);
				Engine.GS.ClearRenderTarget();
				Engine.GS.SetColorBlendState(BlendState.AlphaBlend);
				Global.Game.StaticSystem.SSEffectsRender3DOceanParticles();
				if (this.heatMap)
				{
					this.ItemsShader.DepthBufferTexture.SetValue(this.rtSceneNormals.Resource);
					this.ItemsShader.SetForRender(Matrix.Identity, Vector4.One);
					this.ItemsShader.RenderVolumtericParticlesHeatMap.Apply();
					this.ParticleManager3D.RenderToHeatMap();
				}
				if (this.shadows)
				{
					this.CommonShader.SetShadowGBuffer();
				}
				if (this.{23453} < 1f && this.antialiasingTxaa)
				{
					this.{23449}++;
					if (this.{23449} == Renderer.triTxaa.Length)
					{
						this.{23449} = 0;
					}
					this.CommonShader.TXAAKernel.SetValue(Renderer.triTxaa[this.{23449}] / this.rtSceneAlbedo.Size);
					this.CommonShader.TXAAKernelNrm.SetValue(Renderer.triTxaa[this.{23449}]);
				}
				if (this.mrtNormals)
				{
					Engine.GS.SetRenderTarget(new MultipliedRenderTarget(new RenderTarget[]
					{
						this.rtSceneAlbedo,
						this.rtSceneNormals
					}));
					Engine.GS.ClearRenderTargetAndBuffer();
					Engine.GS.SetRasterizerStateOptions(false, false);
					this.{23438}(true);
					if (this.ssao)
					{
						this.GlobalIlluminationMapBuilder.BuildMap(this.rtGilMap, Global.Camera.GetCameraPositionInfo(), this.rtSceneNormals, this.{23444}.CurrentLightDirection, (this.antialiasingTxaa && this.{23453} < 1f) ? this.{23449} : -1);
						this.GlobalIlluminationMapBuilder.PostProcessMap(this.rtGilMap, this.rtGilMapCopy, !this.antialiasingTxaa || this.{23453} >= 1f);
					}
					Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
					Engine.GS.SetRenderTarget(this.rtMain3d);
					if (!{18560}.closed)
					{
						Engine.GS.ClearRenderTarget(Color.Gray);
					}
					ScreenPlaneRenderer.DrawWithDefaultShader(this.rtSceneAlbedo.Resource, SamplerState.PointClamp, null);
					Engine.GS.SetRenderTarget(this.rtSceneAlbedo);
					if (this.ssao)
					{
						Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
						Engine.GS.SetColorBlendState(BlendState.AlphaBlend);
						ScreenPlaneRenderer.DrawWithDefaultShader(this.rtGilMap.Resource, (this.rtGilMap.Resource.Width == this.rtSceneAlbedo.Resource.Width) ? SamplerState.PointClamp : SamplerState.LinearClamp, null);
						Engine.GS.SetColorBlendState(BlendState.Opaque);
					}
					Engine.GS.SetDepthBuffer(DepthBuffer.ReadAndWrite);
					Engine.GS.graphicsDevice.RasterizerStateCullingEnable = true;
					this.CommonShader.RenderOcean(this.rtMain3d, false);
					Engine.GS.graphicsDevice.RasterizerStateCullingEnable = false;
					if (!this.antialiasingSsaa)
					{
						this.{23437}();
						Engine.GS.SetRenderTarget(this.rtMain3d);
						Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
						Global.Game.StaticSystem.Render3DSkyStatic(true, true, false);
						if (this.antialiasingTxaa)
						{
							this.{23413}(true, false);
						}
						else
						{
							ScreenPlaneRenderer.DrawWithDefaultShader(this.rtSceneAlbedo.Resource, SamplerState.PointClamp, null);
						}
					}
					else
					{
						this.{23437}();
						Engine.GS.SetRenderTarget(this.rtMain3d);
						Engine.GS.ClearDepthBuffer();
						Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
						Global.Game.StaticSystem.Render3DSkyStatic(true, true, false);
						ScreenPlaneRenderer.DrawWithDefaultShader(this.rtSceneAlbedo.Resource, SamplerState.LinearClamp, null);
					}
				}
				else
				{
					this.{23434}();
					if (this.antialiasingTxaa)
					{
						Engine.GS.SetColorBlendState(BlendState.Opaque);
						Engine.GS.SetRenderTarget(this.rtSceneAlbedo);
						Engine.GS.ClearRenderTargetAndBuffer();
						Engine.GS.SetColorBlendState(BlendState.AlphaBlend);
						Engine.GS.SetDepthBuffer(DepthBuffer.ReadAndWrite);
						Engine.GS.graphicsDevice.RasterizerStateCullingEnable = true;
						this.CommonShader.RenderOcean(null, true);
						Engine.GS.graphicsDevice.RasterizerStateCullingEnable = false;
						Engine.GS.SetColorBlendState(BlendState.Opaque);
						this.{23438}(false);
						this.{23437}();
						Engine.GS.SetRenderTarget(this.rtMain3d);
						if (!{18560}.closed)
						{
							Engine.GS.ClearRenderTarget(Color.Gray);
						}
						Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
						Global.Game.StaticSystem.Render3DSkyStatic(true, true, false);
						this.{23413}(true, false);
					}
					else
					{
						Engine.GS.SetColorBlendState(BlendState.Opaque);
						Engine.GS.SetRenderTarget(this.rtMain3d);
						if (!{18560}.closed)
						{
							Engine.GS.ClearRenderTarget(Color.Gray);
						}
						Engine.GS.ClearDepthBuffer();
						Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
						Global.Game.StaticSystem.Render3DSkyStatic(true, true, false);
						Engine.GS.SetDepthBuffer(DepthBuffer.ReadAndWrite);
						Engine.GS.SetRasterizerStateOptions(this.msaa, true);
						this.CommonShader.RenderOcean(null, true);
						Engine.GS.SetRasterizerStateOptions(false, false);
						this.{23438}(false);
						this.{23437}();
					}
				}
				if (this.antialiasingTxaa)
				{
					this.CommonShader.TXAAKernel.SetValue(Vector2.Zero);
				}
				if ((this.antialiasingTxaa && this.{23453} < 0.77f) || this.antialiasingSsaa)
				{
					this.{23409}();
				}
				Engine.GS.SetColorBlendState(BlendState.Opaque);
				Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
				if (this.improvedPostprocess)
				{
					Engine.GS.SetRenderTarget(this.rtBokehScene2);
					this.{23442}.Parameters[0].SetValue(new Vector2(0.708f, this.rtBokehScene2.Size.Y / this.rtBokehScene2.Size.X));
					this.{23442}.Parameters[1].SetValue(this.rtMain3d.Resource);
					ScreenPlaneRenderer.ApplyDefaultVertexShader();
					this.{23442}.CurrentTechnique.Passes[0].Apply();
					ScreenPlaneRenderer.Render();
					Engine.GS.SetRenderTarget(this.rtBokehScene);
					this.{23442}.Parameters[1].SetValue(this.rtBokehScene2.Resource);
					ScreenPlaneRenderer.ApplyDefaultVertexShader();
					this.{23442}.CurrentTechnique.Passes[0].Apply();
					ScreenPlaneRenderer.Render();
					Engine.GS.SetRenderTarget(this.rtBloomThreshold);
					this.{23441}.Parameters[6].SetValue(this.rtMain3d.Resource);
					float value = 0.5f + Geometry.Saturate(Global.Game.StaticSystem.GetSkyShader.DayOrNight - Global.Game.StaticSystem.GetSkyShader.Sunrise * 0.5f) * 0.2f - this.CloudeLevel * 0.3f * (1f - Global.Game.StaticSystem.OpenWorldWinterFactor) - 0.1f * Math.Max(0f, Vector3.Dot(Engine.GS.Camera.Direction, this.{23444}.CurrentLightDirection));
					this.{23441}.Parameters[8].SetValue(value);
					ScreenPlaneRenderer.ApplyDefaultVertexShader();
					this.{23441}.CurrentTechnique.Passes[17].Apply();
					ScreenPlaneRenderer.Render();
					Engine.GS.SetRenderTarget(this.rtBokehScene2);
					this.{23442}.Parameters[0].SetValue(2.655f);
					this.{23442}.Parameters[1].SetValue(this.rtBloomThreshold.Resource);
					ScreenPlaneRenderer.ApplyDefaultVertexShader();
					this.{23442}.CurrentTechnique.Passes[0].Apply();
					ScreenPlaneRenderer.Render();
					Engine.GS.SetRenderTarget(this.rtBloomThreshold);
					this.{23442}.Parameters[1].SetValue(this.rtBokehScene2.Resource);
					ScreenPlaneRenderer.ApplyDefaultVertexShader();
					this.{23442}.CurrentTechnique.Passes[0].Apply();
					ScreenPlaneRenderer.Render();
					float num = (this.UiMode != InterfaceMode.Off) ? Math.Min(1f, (this.{23447}.CurrentSoftValueSmoothstep - 0.01f) / 0.98f) : 0f;
					if (num > 0f)
					{
						Engine.GS.SetColorBlendState(BlendState.AlphaBlend);
						Engine.GS.SetRenderTarget(this.rtMain3d);
						this.{23441}.Parameters[6].SetValue(this.rtBokehScene.Resource);
						this.{23441}.Parameters[7].SetValue(num);
						ScreenPlaneRenderer.ApplyDefaultVertexShader();
						this.{23441}.CurrentTechnique.Passes[16].Apply();
						ScreenPlaneRenderer.Render();
						Engine.GS.SetColorBlendState(BlendState.Opaque);
					}
				}
				bool flag = this.{23446};
				this.{23446} = false;
				bool flag2 = flag || Global.Camera.IsSpyglass;
				bool flag3 = !flag2 && Global.Settings.PostprocessorType == LocalSettings.Postprocessor.Tonemap2AndHdr;
				if (this.lspoEnable)
				{
					for (int j = 0; j < 6; j++)
					{
						Engine.GS.SetRenderTarget(this.rtBokehScene);
						this.{23448}.CurrentMode = GaussianBlurShader.Mode.Horizontal;
						this.{23448}.SetTextureAndApply((j == 0) ? this.rtMain3d.Resource : this.rtBokehScene2.Resource, true, 12f);
						ScreenPlaneRenderer.ApplyDefaultVertexShader();
						ScreenPlaneRenderer.Render();
						Engine.GS.SetRenderTarget(this.rtBokehScene2);
						this.{23448}.CurrentMode = GaussianBlurShader.Mode.Vertical;
						this.{23448}.SetTextureAndApply(this.rtBokehScene.Resource, true, 12f);
						ScreenPlaneRenderer.ApplyDefaultVertexShader();
						ScreenPlaneRenderer.Render();
						if (j == 3)
						{
							Engine.GS.SetRenderTarget(this.rtBokehSceneShort);
							ScreenPlaneRenderer.DrawWithDefaultShader(this.rtBokehScene2.Resource, null, null);
						}
					}
				}
				if (flag2 || flag3)
				{
					Engine.GS.SetRenderTarget(this.rtUiTemp);
					Engine.GS.ClearRenderTarget();
				}
				else
				{
					Engine.GS.ResetRenderTargets();
				}
				this.{23441}.Parameters[0].SetValue(new Vector2(1f + Global.Settings.GammaSetting * 0.5f + this.PostProcess.ColorMultiplier.X - 1f + (Global.Settings.BrightNight ? (0.2f * (1f - Global.Game.StaticSystem.GetSkyShader.DayOrNight)) : 0f), Geometry.Saturate((1f - this.CloudeLevel * 0.5f) * Global.Game.StaticSystem.GetSkyShader.DayOrNight)));
				this.{23441}.Parameters[2].SetValue(this.rtMain3d.Resource);
				this.{23441}.Parameters[3].SetValue(this.rtSsEffects.Resource);
				if (this.improvedPostprocess)
				{
					this.{23441}.Parameters[4].SetValue(this.rtBloomThreshold.Resource);
				}
				this.{23441}.Parameters[5].SetValue(this.PostProcess.BloodScreenIntensity);
				if (this.lspoEnable)
				{
					this.{23441}.Parameters[6].SetValue(this.rtBokehScene.Resource);
					this.{23441}.Parameters["blurShort"].SetValue(this.rtBokehSceneShort.Resource);
					this.{23441}.Parameters["lspo"].SetValue(new Vector3(0f, -0.1f, 1.4f + 0.3f * Math.Max(1f - Global.Game.StaticSystem.GetSkyShader.DayOrNight, CommonGlobal.CurrentClientWeather.RainingLevelClient)));
				}
				if (this.colorGrading)
				{
					this.{23441}.Parameters[1].SetValue(((Global.Settings.PostprocessorType == LocalSettings.Postprocessor.Tonemap1) ? this.{23456} : this.{23457}).CurrentLUT);
				}
				ScreenPlaneRenderer.ApplyDefaultVertexShader_ShaderModel3();
				int num2 = 0;
				if (this.heatMap)
				{
					num2++;
				}
				if (this.lspoEnable)
				{
					num2 += 2;
				}
				if (this.colorGrading)
				{
					num2 += 4;
				}
				if (this.improvedPostprocess)
				{
					num2 += 8;
				}
				this.{23441}.CurrentTechnique.Passes[num2].Apply();
				ScreenPlaneRenderer.Render();
				if (flag3)
				{
					this.HdrColorGradients.Process(this.rtUiTemp.Resource, null);
				}
				if (Global.Camera.IsSpyglass)
				{
					Engine.GS.Begin2D(true);
					foreach (IGraphicsElement graphicsElement2 in ((IEnumerable<IGraphicsElement>)Global.Game.Scenes))
					{
						graphicsElement2.Render2D();
					}
					Global.Game.GetCurrentScene.Render2D();
					Engine.GS.SetTexture(AtlasObjs.Texture);
					this.PostProcess.RenderFrontLayer();
					Engine.GS.End2D();
				}
				else if (this.UiMode != InterfaceMode.Off)
				{
					Engine.GS.Render2DProperties.DefaultSamplerState = Renderer.ssUseMipLod;
					Engine.GS.Begin2D(true);
					Global.Game.GetCurrentScene.Render2D();
					Engine.GS.SetRasterizerStateUiScissor();
					{23407}.Render2D();
					Engine.GS.SetTexture(AtlasGameGui.Texture);
					this.ParticleManager2D.Render();
					this.PostProcess.RenderFrontLayer();
					float num3 = 1f - this.PostProcess.ColorMultiplier.X;
					if (num3 > 0f)
					{
						Engine.GS.SetTexture(CommonAtlas.Texture);
						Device gs = Engine.GS;
						Rectangle uiarea = Engine.GS.UIArea;
						Color color = Color.Black * num3;
						gs.Draw(CommonAtlas.whitePixel, uiarea, color);
					}
					if (this.OverlayGameLogo > 0f)
					{
						this.{23404}();
					}
					Engine.GS.End2D();
					Engine.GS.Render2DProperties.DefaultSamplerState = SamplerState.LinearClamp;
				}
				else if (this.OverlayGameLogo > 0f)
				{
					Engine.GS.Begin2D(true);
					this.{23404}();
					Engine.GS.End2D();
				}
				if (this.UiMode != InterfaceMode.Default && Debugging.EnableOverlay)
				{
					Engine.GS.Begin2D(true);
					Engine.GS.SetFont(Fonts.Philosopher_14);
					Device gs2 = Engine.GS;
					string keyToString = Global.Settings.kb_FreeMode.KeyToString;
					Vector2 vector = new Vector2(5f, 5f);
					Color color = Color.White;
					gs2.DrawString(keyToString, vector, color);
					Engine.GS.End2D();
				}
				if (flag)
				{
					ScreenShootManager.ScreenShoot(this.rtUiTemp, "Screenshoots\\", ".png");
					{19994}.Me({19988}.Okay, Local.Renderer_0, Array.Empty<object>());
					Engine.GS.ResetRenderTargets();
					ScreenPlaneRenderer.DrawWithDefaultShader(this.rtUiTemp.Resource, null, null);
				}
				else if (Global.Camera.IsSpyglass)
				{
					Engine.GS.SetColorBlendState(BlendState.Opaque);
					Engine.GS.ResetRenderTargets();
					ScreenPlaneRenderer.ApplyDefaultVertexShader();
					this.SpyglassEffect.ApplyPS(this.rtUiTemp.Resource, OtherTextures.WaterDropsNormalmap);
					ScreenPlaneRenderer.Render();
					Engine.GS.SetColorBlendState(BlendState.AlphaBlend);
					Engine.GS.Begin2D(true);
					SpyglassUi.Render();
					Engine.GS.End2D();
				}
				if (this.antialiasingTxaa)
				{
					RenderTarget renderTarget = this.rtSceneAlbedo1;
					this.rtSceneAlbedo1 = this.rtSceneAlbedo0;
					this.rtSceneAlbedo0 = this.rtSceneAlbedo;
					this.rtSceneAlbedo = renderTarget;
				}
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x000CD560 File Offset: 0x000CB760
		private void {23408}()
		{
			if (Global.Game.WorldInstance.IsActive)
			{
				Global.Game.WorldInstance.Render3DShips();
				Global.Game.WorldInstance.Render3DNormal();
				Global.Game.ScenePort.Render3DMainScene();
				foreach (IGraphicsElement graphicsElement in ((IEnumerable<IGraphicsElement>)Global.Game.Scenes))
				{
					graphicsElement.Render3D();
				}
				if (!Renderer.ReflectionsAreBeingDrawn)
				{
					Global.Game.StaticSystem.Render3D();
				}
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x000CD604 File Offset: 0x000CB804
		private void {23409}()
		{
			Engine.GS.SetColorBlendState(BlendState.Opaque);
			this.{23410}(this.rtMain3d, this.rtUiTemp);
			Engine.GS.SetRenderTarget(this.rtMain3d);
			ScreenPlaneRenderer.DrawWithDefaultShader(this.rtUiTemp.Resource, null, null);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x000CD65C File Offset: 0x000CB85C
		private void {23410}(RenderTarget {23411}, RenderTarget {23412})
		{
			Engine.GS.SetRenderTarget({23412});
			Engine.GS.graphicsDevice.SamplerStates.SetState(0, SamplerState.PointClamp);
			Engine.GS.graphicsDevice.Textures[0] = {23411}.Resource;
			ScreenPlaneRenderer.ApplyDefaultVertexShader_ShaderModel3();
			this.{23443}.Parameters["pixelSize"].SetValue({23411}.PixelSize);
			this.{23443}.Parameters["Sharpening"].SetValue(0.6f);
			this.{23443}.CurrentTechnique.Passes[0].Apply();
			ScreenPlaneRenderer.Render();
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x000CD710 File Offset: 0x000CB910
		private void {23413}(bool {23414}, bool {23415})
		{
			if ({23415})
			{
				Engine.GS.SetDepthBuffer(DepthBuffer.ReadAndWrite);
				this.{23440}.Parameters[1].SetValue(this.rtSceneNormals.Resource);
			}
			else
			{
				Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
			}
			this.{23440}.Parameters[0].SetValue(this.rtSceneAlbedo.Resource);
			this.{23440}.Parameters[2].SetValue(Renderer.CameraFarPlane);
			this.{23440}.Parameters[3].SetValue(Engine.GS.Camera.ProjMatrix);
			ScreenPlaneRenderer.ApplyDefaultVertexShader_ShaderModel3();
			if ({23414})
			{
				this.{23451}.X = -Global.Camera.Rotation.Y * 0.5f;
				this.{23451}.Y = Global.Camera.Rotation.X;
				this.{23440}.Parameters[4].SetValue(this.rtSceneAlbedo0.Resource);
				this.{23440}.Parameters[5].SetValue(this.rtSceneAlbedo1.Resource);
				Vector2 value;
				this.{23440}.Parameters[6].SetValue(value = (this.{23450}[0] - this.{23451}) / 0.9599311f);
				Vector2 value2;
				this.{23440}.Parameters[7].SetValue(value2 = (this.{23450}[1] - this.{23451}) / 0.9599311f);
				this.{23453} = Geometry.Saturate(((value + value2) * Global.Game.WindowSize).Length() * 0.2f + (this.{23452} - Global.Camera.Position).Length() * 9.5f - 0.25f);
				if (Global.Game.GameTime.FpsCounter.Avg < 31f)
				{
					this.{23453} = 1f;
				}
				this.{23440}.Parameters[8].SetValue(this.{23453});
				this.{23450}[1] = this.{23450}[0];
				this.{23450}[0] = this.{23451};
				this.{23452} = Global.Camera.Position;
				if (this.{23453} >= 0.77f)
				{
					this.{23440}.Parameters["fxaaScreenSize"].SetValue(this.rtSceneAlbedo.Size);
					this.{23440}.CurrentTechnique.Passes[{23415} ? 3 : 4].Apply();
				}
				else
				{
					this.{23440}.CurrentTechnique.Passes[{23415} ? 2 : 0].Apply();
				}
			}
			else
			{
				if (!{23415})
				{
					throw new Exception();
				}
				this.{23440}.CurrentTechnique.Passes[1].Apply();
			}
			ScreenPlaneRenderer.Render();
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x000CDA34 File Offset: 0x000CBC34
		public void SetMap(WorldMapInfo {23416}, SkyTextures {23417}, bool {23418})
		{
			this.{23444}.SetStyle({23417}.Style);
			this.CommonShader.ChangeMap({23416}, {23418});
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x000CDA54 File Offset: 0x000CBC54
		public void CleanSupportTargets()
		{
			IRenderTarget currentOutput = Engine.GS.CurrentOutput;
			ShadowMappingEngine[] shadowCascades = this.ShadowCascades;
			for (int i = 0; i < shadowCascades.Length; i++)
			{
				shadowCascades[i].TryCleanBuffer();
			}
			Engine.GS.SetRenderTarget(currentOutput);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x000CDA94 File Offset: 0x000CBC94
		private RenderTarget {23419}(RenderTarget {23420}, int {23421}, int {23422}, SurfaceFormat {23423}, DepthFormat {23424}, int {23425}, bool {23426}, string {23427}, bool {23428} = false)
		{
			if ({23420} == null || {23420}.Size.X != (float){23421} || {23420}.Size.Y != (float){23422} || {23420}.PixelFormat != {23423} || {23420}.DepthBufferFormat != {23424} || {23420}.Resource.MultiSampleCount != {23425} || {23420}.MipMaps != {23428})
			{
				if ({23420} != null)
				{
					{23420}.Dispose();
				}
				return new RenderTarget({23421}, {23422}, {23423}, {23424}, {23425}, {23426}, {23427}, {23428});
			}
			return {23420};
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x000CDB10 File Offset: 0x000CBD10
		public void UpdateRenderTargets()
		{
			Vector2 windowSize = Engine.Game.WindowSize;
			if (this.CommonShader != null)
			{
				this.CommonShader.SetEyeGBuffer(null);
			}
			if (this.mrtNormals)
			{
				Vector2 vector = windowSize;
				if (this.antialiasingSsaa)
				{
					vector *= Renderer.SsaaScale;
				}
				int {23421} = (int)vector.X;
				int {23422} = (int)vector.Y;
				this.rtSceneAlbedo = this.{23419}(this.rtSceneAlbedo, {23421}, {23422}, SurfaceFormat.Rgba64, DepthFormat.Depth24, 0, false, "rtSceneAlbedo", false);
				this.rtSceneNormals = this.{23419}(this.rtSceneNormals, {23421}, {23422}, SurfaceFormat.Rgba64, DepthFormat.Depth24, 0, false, "rtSceneNormals", false);
				if (this.antialiasingTxaa)
				{
					this.rtSceneAlbedo0 = this.{23419}(this.rtSceneAlbedo0, (int)windowSize.X, (int)windowSize.Y, SurfaceFormat.Rgba64, DepthFormat.Depth24, 0, false, "rtSceneAlbedo0", false);
					this.rtSceneAlbedo1 = this.{23419}(this.rtSceneAlbedo1, (int)windowSize.X, (int)windowSize.Y, SurfaceFormat.Rgba64, DepthFormat.Depth24, 0, false, "rtSceneAlbedo1", false);
				}
				else
				{
					RenderTarget renderTarget = this.rtSceneAlbedo0;
					if (renderTarget != null)
					{
						renderTarget.Dispose();
					}
					RenderTarget renderTarget2 = this.rtSceneAlbedo1;
					if (renderTarget2 != null)
					{
						renderTarget2.Dispose();
					}
					this.rtSceneAlbedo0 = null;
					this.rtSceneAlbedo1 = null;
				}
			}
			else
			{
				RenderTarget renderTarget3 = this.rtSceneNormals;
				if (renderTarget3 != null)
				{
					renderTarget3.Dispose();
				}
				this.rtSceneNormals = null;
				if (this.antialiasingTxaa)
				{
					this.rtSceneAlbedo = this.{23419}(this.rtSceneAlbedo, (int)windowSize.X, (int)windowSize.Y, SurfaceFormat.Rgba64, DepthFormat.Depth24, 0, false, "rtSceneAlbedo0", false);
					this.rtSceneAlbedo0 = this.{23419}(this.rtSceneAlbedo0, (int)windowSize.X, (int)windowSize.Y, SurfaceFormat.Rgba64, DepthFormat.Depth24, 0, false, "rtSceneAlbedo0", false);
					this.rtSceneAlbedo1 = this.{23419}(this.rtSceneAlbedo1, (int)windowSize.X, (int)windowSize.Y, SurfaceFormat.Rgba64, DepthFormat.Depth24, 0, false, "rtSceneAlbedo1", false);
				}
				else
				{
					RenderTarget renderTarget4 = this.rtSceneAlbedo;
					if (renderTarget4 != null)
					{
						renderTarget4.Dispose();
					}
					RenderTarget renderTarget5 = this.rtSceneAlbedo0;
					if (renderTarget5 != null)
					{
						renderTarget5.Dispose();
					}
					RenderTarget renderTarget6 = this.rtSceneAlbedo1;
					if (renderTarget6 != null)
					{
						renderTarget6.Dispose();
					}
					this.rtSceneAlbedo = null;
					this.rtSceneAlbedo0 = null;
					this.rtSceneAlbedo1 = null;
				}
			}
			if (this.ssao)
			{
				Vector2 vector2 = windowSize;
				int {23421}2 = (int)vector2.X;
				int {23422}2 = (int)vector2.Y;
				this.rtGilMap = this.{23419}(this.rtGilMap, {23421}2, {23422}2, SurfaceFormat.Color, DepthFormat.None, 0, true, "rtGilMap", false);
				this.rtGilMapCopy = this.{23419}(this.rtGilMapCopy, {23421}2, {23422}2, SurfaceFormat.Color, DepthFormat.None, 0, true, "rtGilMapCopy", false);
			}
			else
			{
				RenderTarget renderTarget7 = this.rtGilMap;
				if (renderTarget7 != null)
				{
					renderTarget7.Dispose();
				}
				RenderTarget renderTarget8 = this.rtGilMapCopy;
				if (renderTarget8 != null)
				{
					renderTarget8.Dispose();
				}
				this.rtGilMap = null;
				this.rtGilMapCopy = null;
			}
			if (this.sceneReflections)
			{
				Vector2 vector3 = windowSize / 2f;
				int {23421}3 = (int)vector3.X;
				int {23422}3 = (int)vector3.Y;
				this.rtReflectionsFrontScene = this.{23419}(this.rtReflectionsFrontScene, {23421}3, {23422}3, SurfaceFormat.Color, DepthFormat.Depth16, this.sceneFullReflectionsAA ? 4 : 0, true, "rtReflectionsFrontScene", true);
			}
			else
			{
				RenderTarget renderTarget9 = this.rtReflectionsFrontScene;
				if (renderTarget9 != null)
				{
					renderTarget9.Dispose();
				}
				this.rtReflectionsFrontScene = null;
			}
			if (this.rtReflectionsFront == null)
			{
				this.rtReflectionsFront = new RenderTarget(164, 164, SurfaceFormat.Color, DepthFormat.None, 0, true, "rtReflectionsFront", true);
				this.rtReflectionsBack = new RenderTarget(128, 128, SurfaceFormat.Color, DepthFormat.None, 0, true, "rtReflectionsBack", true);
			}
			this.rtMain3d = this.{23419}(this.rtMain3d, (int)windowSize.X, (int)windowSize.Y, SurfaceFormat.Rgba64, DepthFormat.Depth24, this.msaa ? 4 : 0, false, "rtMain3d", false);
			this.rtSsEffects = this.{23419}(this.rtSsEffects, (int)windowSize.X / 2, (int)windowSize.Y / 2, SurfaceFormat.Color, DepthFormat.None, 0, true, "rtSsEffects", false);
			this.rtUiTemp = this.{23419}(this.rtUiTemp, (int)windowSize.X, (int)windowSize.Y, SurfaceFormat.Color, DepthFormat.None, 0, true, "rtUiTemp", false);
			this.rtOceanNormals = this.{23419}(this.rtOceanNormals, this.hqOceanNormals ? 512 : 256, this.hqOceanNormals ? 512 : 256, SurfaceFormat.Color, DepthFormat.None, 0, true, "rtOceanNormals", true);
			this.rtBloomThreshold = this.{23419}(this.rtBloomThreshold, (int)windowSize.X / 3, (int)windowSize.Y / 3, SurfaceFormat.Color, DepthFormat.None, 0, true, "rtBloomThreshold", false);
			this.rtBokehScene = this.{23419}(this.rtBokehScene, (int)windowSize.X / 3, (int)windowSize.Y / 3, SurfaceFormat.Color, DepthFormat.None, 0, true, "rtBokehScene", false);
			this.rtBokehScene2 = this.{23419}(this.rtBokehScene2, (int)windowSize.X / 3, (int)windowSize.Y / 3, SurfaceFormat.Color, DepthFormat.None, 0, true, "rtBokehScene2", false);
			this.rtBokehSceneShort = this.{23419}(this.rtBokehSceneShort, (int)windowSize.X / 3, (int)windowSize.Y / 3, SurfaceFormat.Color, DepthFormat.None, 0, true, "rtBokehScene3", false);
			if (Global.Settings.Loaded)
			{
				ShadowMappingEngine[] shadowCascades;
				if (this.shadows)
				{
					shadowCascades = this.ShadowCascades;
					for (int i = 0; i < shadowCascades.Length; i++)
					{
						shadowCascades[i].SetShadowMapResolution((Global.Settings.ShadowsQuality == LocalSettings.RendererShadows.Medium) ? 920 : 1366);
					}
					return;
				}
				shadowCascades = this.ShadowCascades;
				for (int i = 0; i < shadowCascades.Length; i++)
				{
					shadowCascades[i].SetShadowMapResolution(0);
				}
			}
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x000CE06F File Offset: 0x000CC26F
		public static float UiOpacityToFocus(Marker {23429})
		{
			return 1f - Geometry.InverseLerp(0f, 0.3f, {23429}.Distance(Engine.GS.MouseToUI) / (float)Engine.GS.UIArea.Width);
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x000CE1CF File Offset: 0x000CC3CF
		[CompilerGenerated]
		private void {23430}(Vector2 {23431})
		{
			this.UpdateRenderTargets();
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x000CE1D8 File Offset: 0x000CC3D8
		[CompilerGenerated]
		private void {23432}()
		{
			this.{23444}.InitializeContent();
			this.{23444}.SunLightSource.InitializeUv(AtlasObjs.sunTexture, AtlasObjs.Texture.Size);
			this.{23444}.MoonLightSource.InitializeUv(AtlasObjs.moonTexture, AtlasObjs.Texture.Size);
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x000CE22E File Offset: 0x000CC42E
		[CompilerGenerated]
		private void {23433}()
		{
			this.ParticleManager3D.RemoveAll();
			this.ParticleManager2D.RemoveAll();
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x000CE248 File Offset: 0x000CC448
		[CompilerGenerated]
		private void {23434}()
		{
			Engine.GS.SetColorBlendState(BlendState.Opaque);
			Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
			if (this.sceneReflections || Global.Settings.HighDetailing)
			{
				this.CommonShader.SetReflectionBasis(((GameCamera)Engine.GS.Camera).CreateReflectionBasisMatrix(true));
			}
			if (!this.sceneReflections || this.{23460} % 3 == 0)
			{
				Engine.GS.SetRenderTarget(this.rtReflectionsBack);
				((GameCamera)Engine.GS.Camera).QueryParaboloidRender(delegate
				{
					Global.Game.StaticSystem.Render3DSkyStatic(false, false, true);
				}, false, false);
				Engine.GS.SetRenderTarget(this.rtReflectionsFront);
				((GameCamera)Engine.GS.Camera).QueryParaboloidRender(new Action(this.{23435}), true, false);
			}
			if (this.sceneReflections)
			{
				Engine.GS.SetRenderTarget(this.rtReflectionsFrontScene);
				Engine.GS.ClearRenderTargetAndBuffer();
				Renderer.ReflectionsAreBeingDrawn = true;
				((GameCamera)Engine.GS.Camera).QueryParaboloidRender(new Action(this.{23436}), true, true);
			}
			Renderer.ReflectionsAreBeingDrawn = false;
			this.CommonShader.EndRenderToReflectionMap();
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x000CE38B File Offset: 0x000CC58B
		[CompilerGenerated]
		private void {23435}()
		{
			if (!this.sceneReflections)
			{
				this.CommonShader.SetReflectionMatrix();
			}
			Global.Game.StaticSystem.Render3DSkyStatic(false, false, true);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x000CE3B4 File Offset: 0x000CC5B4
		[CompilerGenerated]
		private void {23436}()
		{
			this.CommonShader.SetReflectionMatrix();
			this.CommonShader.BeginRenderToReflectionMap();
			Global.Game.StaticSystem.Render3DSkyStatic(false, false, true);
			if (Global.Game.GetCurrentSceneName == GameSceneName.Game || Global.Game.GetCurrentSceneName == GameSceneName.Port)
			{
				Engine.GS.SetRasterizerStateOptionAA(this.sceneFullReflectionsAA);
				Engine.GS.SetDepthBuffer(DepthBuffer.ReadAndWrite);
				Global.Game.WorldInstance.Render3DShips();
				Engine.GS.graphicsDevice.RasterizerStateCullingEnable = true;
				Global.Game.WorldInstance.Render3DNormal();
				Global.Game.ScenePort.Render3DMainScene();
				foreach (IGraphicsElement graphicsElement in ((IEnumerable<IGraphicsElement>)Global.Game.Scenes))
				{
					graphicsElement.Render3D();
				}
				Engine.GS.graphicsDevice.RasterizerStateCullingEnable = false;
				Engine.GS.SetColorBlendState(BlendState.AlphaBlend);
				Engine.GS.SetDepthBuffer(DepthBuffer.ReadOnly);
				this.ParticleManager3D.Render(this.ItemsShader, 140f, 210f, AtlasObjs.Texture.Size, delegate(ParticlesAndStaticMesh {23462})
				{
					{23462}.VolumtericParticlesWithFog();
				}, new Vector2?(new Vector2(2f, 8f)));
				if (this.PhysicallyOverloadingFactor < 0.5f)
				{
					Engine.GS.Begin2D(true);
					Engine.GS.SetTexture(AtlasObjs.Texture);
					Engine.GS.ResetScissor();
					foreach (IsleFlares isleFlares in ((IEnumerable<IsleFlares>)this.{23454}))
					{
						float lastResult = this.LightsOcclusion.Tests[isleFlares.UniqueKey].LastResult;
						if (lastResult > 0f)
						{
							isleFlares.Draw(this.LightFlareRender, lastResult);
						}
					}
					Engine.GS.End2D();
				}
				Engine.GS.SetColorBlendState(BlendState.Opaque);
				Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
				Engine.GS.SetRasterizerStateOptionAA(false);
			}
			if (Global.Game.GetCurrentSceneName == GameSceneName.Entry)
			{
				Engine.GS.SetRasterizerStateOptionAA(this.sceneFullReflectionsAA);
				Engine.GS.SetDepthBuffer(DepthBuffer.ReadAndWrite);
				Global.Game.WorldInstance.Render3DShips();
				Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
				Engine.GS.SetRasterizerStateOptionAA(false);
			}
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x000CE64C File Offset: 0x000CC84C
		[CompilerGenerated]
		private void {23437}()
		{
			if (this.antialiasingTxaa && this.{23453} < 0.3f)
			{
				this.ItemsShader.TXAAKernel.SetValue(Renderer.triTxaa[this.{23449}] / this.rtSceneAlbedo.Size);
			}
			if (!{18560}.closed)
			{
				Global.Render.ItemsShader.ManualSetFog(10000f, 10000f);
			}
			Engine.GS.SetColorBlendState(BlendState.AlphaBlend);
			Engine.GS.SetDepthBuffer(DepthBuffer.ReadAndWrite);
			Engine.GS.SetRasterizerStateOptions(this.msaa, false);
			Global.Game.WorldInstance.Render3DTransparantMesh();
			Engine.GS.SetRasterizerStateOptions(false, false);
			Engine.GS.SetDepthBuffer(DepthBuffer.ReadOnly);
			if (Global.Game.WorldInstance.IsActive)
			{
				Global.Game.WorldInstance.Render3DStaticItems();
			}
			Global.Game.StaticSystem.Render3DMain();
			Engine.GS.SetRasterizerStateOptions(false, false);
			Global.Game.StaticSystem.Render3DSnowOrRain();
			Global.Game.ScenePort.Render3DStatic();
			foreach (IGraphicsElement graphicsElement in ((IEnumerable<IGraphicsElement>)Global.Game.Scenes))
			{
				graphicsElement.Render3DStatic();
			}
			Engine.GS.SetRasterizerStateOptions(false, false);
			this.ParticleManager3D.Render(this.ItemsShader, 140f, 210f, AtlasObjs.Texture.Size, delegate(ParticlesAndStaticMesh {23463})
			{
				{23463}.VolumtericParticlesWithFog();
			}, null);
			Global.Game.WorldInstance.Render3DCannonBalls();
			Engine.GS.SetDepthBuffer(DepthBuffer.ReadAndWrite);
			Global.Game.WorldInstance.Render3DTransparantMeshCurrentPlayer();
			this.ItemsShader.TXAAKernel.SetValue(Vector2.Zero);
			this.AmbientLightOcclusionTest.BeginTest((this.{23444}.CycleLightDirection.Y < 0f) ? this.{23444}.MoonLightSource.RenderPosition : this.{23444}.SunLightSource.RenderPosition, true, false, null);
			Global.Game.InterestPoints.DrawOcclusionTest();
			if (Global.Settings.EnableBasicEffects)
			{
				this.{23454}.Size = 0;
				this.{23459}.Size = 0;
				Vector3 direction = Global.Camera.Direction;
				foreach (IsleFlares isleFlares in Global.Game.WorldInstance.EnumerateLampLights())
				{
					Tlist<DynamicOcclusionSystem.Request> tlist = this.{23459};
					DynamicOcclusionSystem.Request request = new DynamicOcclusionSystem.Request(isleFlares.Position, isleFlares.UniqueKey, (isleFlares.Size == IsleFlaresSize.Ship) ? 10 : 20);
					tlist.Add(request);
					if (Vector3.Dot(isleFlares.Position - Global.Camera.Position, direction) > 0.1f)
					{
						this.{23454}.Add(isleFlares);
					}
				}
				this.LightsOcclusion.Draw(this.ItemsShader);
				if (Engine.GS.Is2DBatchActive)
				{
					Assert.Report(true, "Renderer:Is2DBatchActive");
					Engine.GS.End2D();
				}
				Engine.GS.Begin2D(true);
				Engine.GS.SetTexture(AtlasObjs.Texture);
				foreach (IsleFlares isleFlares2 in ((IEnumerable<IsleFlares>)this.{23454}))
				{
					float lastResult = this.LightsOcclusion.Tests[isleFlares2.UniqueKey].LastResult;
					if (lastResult > 0f)
					{
						isleFlares2.Draw(this.LightFlareRender, lastResult);
					}
				}
				this.LightFlareRender.RenderAmbientLight(this.AmbientLightOcclusionTest, this.{23444}, Global.Game.StaticSystem.GetSkyShader);
				this.Pointlights.DrawFlares(this.LightFlareRender);
				Engine.GS.End2D();
			}
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x000CEA70 File Offset: 0x000CCC70
		[CompilerGenerated]
		private void {23438}(bool {23439})
		{
			this.CommonShader.Setparaboloid(this.rtReflectionsFrontScene ?? this.rtReflectionsFront, this.rtReflectionsBack, this.rtReflectionsFront);
			Engine.GS.SetColorBlendState(BlendState.Opaque);
			Engine.GS.SetDepthBuffer(DepthBuffer.ReadAndWrite);
			Engine.GS.SetRasterizerStateOptions(this.msaa, false);
			this.{23408}();
			Engine.GS.SetRasterizerStateOptions(false, false);
			if ({23439})
			{
				this.{23434}();
			}
			this.CommonShader.SetEyeGBuffer(this.rtSceneNormals);
			this.CommonShader.SetOceanEffectMap(this.rtSsEffects, null);
		}

		// Token: 0x0400160F RID: 5647
		public const float CameraNearPlaneForReflectionBuffer = 1f;

		// Token: 0x04001610 RID: 5648
		public static float CameraFarPlane = Renderer._CameraFarPlane;

		// Token: 0x04001611 RID: 5649
		public const float WorldMapDecorationsFarDist = 350f;

		// Token: 0x04001612 RID: 5650
		public static readonly int[] ShadowCascadeResolution = new int[]
		{
			16,
			72,
			250
		};

		// Token: 0x04001613 RID: 5651
		public const float ShadowDistanceToLightSource = 150f;

		// Token: 0x04001614 RID: 5652
		public const float ShadowProjFarPlane = 260f;

		// Token: 0x04001615 RID: 5653
		public const float ShadowNearPlane = 10f;

		// Token: 0x04001616 RID: 5654
		public const float ParticlesFogStart = 140f;

		// Token: 0x04001617 RID: 5655
		public const float ParticlesFogEnd = 210f;

		// Token: 0x04001618 RID: 5656
		public const float AO_FogStart = 130f;

		// Token: 0x04001619 RID: 5657
		public const float AO_FogEnd = 350f;

		// Token: 0x0400161A RID: 5658
		public const float PointLightExtraDistance = 50f;

		// Token: 0x0400161B RID: 5659
		private const float LODDistanceForIsles_max = 150f;

		// Token: 0x0400161C RID: 5660
		private const float LODDistanceForShips_max = 60f;

		// Token: 0x0400161D RID: 5661
		private const float LODDistanceForShips_Lod2_max = 90f;

		// Token: 0x0400161E RID: 5662
		private const float LODDistanceForShips_Lod3_max = 150f;

		// Token: 0x0400161F RID: 5663
		private const float LODDistanceForIsles_min = 110f;

		// Token: 0x04001620 RID: 5664
		private const float LODDistanceForShips_min = 50f;

		// Token: 0x04001621 RID: 5665
		private const float LODDistanceForShips_Lod2_min = 80f;

		// Token: 0x04001622 RID: 5666
		private const float LODDistanceForShips_Lod3_min = 110f;

		// Token: 0x04001623 RID: 5667
		public const float ShipModelCannonsHQViewDist = 15f;

		// Token: 0x04001624 RID: 5668
		private static readonly Vector2[] triTxaa = new Vector2[]
		{
			new Vector2(0.666f, 0f),
			new Vector2(-0.5f, 0.666f),
			new Vector2(-0.5f, -0.666f)
		};

		// Token: 0x04001625 RID: 5669
		private static SamplerState ssUseMipLod = new SamplerState
		{
			AddressU = TextureAddressMode.Clamp,
			AddressV = TextureAddressMode.Clamp,
			AddressW = TextureAddressMode.Clamp,
			Filter = TextureFilter.Linear,
			MipMapLevelOfDetailBias = -0.4f
		};

		// Token: 0x04001626 RID: 5670
		public float LODDistanceForIsles = 150f;

		// Token: 0x04001627 RID: 5671
		public float LODDistanceForShips = 60f;

		// Token: 0x04001628 RID: 5672
		public float LODDistanceForShips_Lod2 = 90f;

		// Token: 0x04001629 RID: 5673
		public float LODDistanceForShips_Lod3 = 150f;

		// Token: 0x0400162A RID: 5674
		public RenderTarget rtSceneAlbedo;

		// Token: 0x0400162B RID: 5675
		public RenderTarget rtSceneAlbedo0;

		// Token: 0x0400162C RID: 5676
		public RenderTarget rtSceneAlbedo1;

		// Token: 0x0400162D RID: 5677
		public RenderTarget rtSceneNormals;

		// Token: 0x0400162E RID: 5678
		public RenderTarget rtGilMap;

		// Token: 0x0400162F RID: 5679
		public RenderTarget rtGilMapCopy;

		// Token: 0x04001630 RID: 5680
		public RenderTarget rtReflectionsFrontScene;

		// Token: 0x04001631 RID: 5681
		public RenderTarget rtReflectionsFront;

		// Token: 0x04001632 RID: 5682
		public RenderTarget rtReflectionsBack;

		// Token: 0x04001633 RID: 5683
		public RenderTarget rtMain3d;

		// Token: 0x04001634 RID: 5684
		public RenderTarget rtOceanNormals;

		// Token: 0x04001635 RID: 5685
		public RenderTarget rtSsEffects;

		// Token: 0x04001636 RID: 5686
		public RenderTarget rtBokehScene;

		// Token: 0x04001637 RID: 5687
		public RenderTarget rtBokehScene2;

		// Token: 0x04001638 RID: 5688
		public RenderTarget rtBokehSceneShort;

		// Token: 0x04001639 RID: 5689
		public RenderTarget rtBloomThreshold;

		// Token: 0x0400163A RID: 5690
		public RenderTarget rtUiTemp;

		// Token: 0x0400163B RID: 5691
		public Texture2D txFogOfWar;

		// Token: 0x0400163C RID: 5692
		public ShadowMappingEngine[] ShadowCascades;

		// Token: 0x0400163D RID: 5693
		public CascadedShadowMap CascadedShadowHelper;

		// Token: 0x0400163E RID: 5694
		public CommonWorldShader CommonShader;

		// Token: 0x0400163F RID: 5695
		public ParticlesAndStaticMesh ItemsShader;

		// Token: 0x04001640 RID: 5696
		public GSSDO GlobalIlluminationMapBuilder;

		// Token: 0x04001641 RID: 5697
		public WorldOfSeaBattlePostProcess PostProcess;

		// Token: 0x04001642 RID: 5698
		public LightSourceOcclusionTest AmbientLightOcclusionTest;

		// Token: 0x04001643 RID: 5699
		public LightFlareRender LightFlareRender;

		// Token: 0x04001644 RID: 5700
		public SpyglassShader SpyglassEffect;

		// Token: 0x04001645 RID: 5701
		public OceanNormalMapGenerator OceanNormalMapGenerator;

		// Token: 0x04001646 RID: 5702
		public HdrColorGradients HdrColorGradients;

		// Token: 0x04001647 RID: 5703
		public Effect WorldMapRegionEffect;

		// Token: 0x04001648 RID: 5704
		public Effect MinimapCircleStencil;

		// Token: 0x04001649 RID: 5705
		public Effect DenoiseEffect;

		// Token: 0x0400164A RID: 5706
		private Effect {23440};

		// Token: 0x0400164B RID: 5707
		private Effect {23441};

		// Token: 0x0400164C RID: 5708
		private Effect {23442};

		// Token: 0x0400164D RID: 5709
		private Effect {23443};

		// Token: 0x0400164E RID: 5710
		private SceneManager {23444};

		// Token: 0x0400164F RID: 5711
		private LoadingScreenRenderer {23445};

		// Token: 0x04001650 RID: 5712
		private bool {23446};

		// Token: 0x04001651 RID: 5713
		public Texture2D oceanSourceNormal;

		// Token: 0x04001652 RID: 5714
		private SoftTrigger {23447};

		// Token: 0x04001653 RID: 5715
		private GaussianBlurShader {23448};

		// Token: 0x04001654 RID: 5716
		private int {23449};

		// Token: 0x04001655 RID: 5717
		private Vector2[] {23450} = new Vector2[Renderer.triTxaa.Length - 1];

		// Token: 0x04001656 RID: 5718
		private Vector2 {23451};

		// Token: 0x04001657 RID: 5719
		private Vector3 {23452};

		// Token: 0x04001658 RID: 5720
		private float {23453};

		// Token: 0x04001659 RID: 5721
		private Tlist<IsleFlares> {23454} = new Tlist<IsleFlares>(50);

		// Token: 0x0400165A RID: 5722
		private Timer {23455} = new Timer(1000f);

		// Token: 0x0400165B RID: 5723
		private ColorGradingHelper {23456};

		// Token: 0x0400165C RID: 5724
		private ColorGradingHelper {23457};

		// Token: 0x0400165D RID: 5725
		[CompilerGenerated]
		private float {23458};

		// Token: 0x0400165E RID: 5726
		public static bool ReflectionsAreBeingDrawn;

		// Token: 0x0400165F RID: 5727
		public static int CurrentShadowMapIndex;

		// Token: 0x04001660 RID: 5728
		public InterfaceMode UiMode;

		// Token: 0x04001661 RID: 5729
		public ParticleManager2D ParticleManager2D;

		// Token: 0x04001662 RID: 5730
		public ParticleManager3D ParticleManager3D;

		// Token: 0x04001663 RID: 5731
		public PointLightArrayHolder Pointlights;

		// Token: 0x04001664 RID: 5732
		public DynamicOcclusionSystem LightsOcclusion;

		// Token: 0x04001665 RID: 5733
		private Tlist<DynamicOcclusionSystem.Request> {23459} = new Tlist<DynamicOcclusionSystem.Request>();

		// Token: 0x04001666 RID: 5734
		private int {23460};
	}
}
