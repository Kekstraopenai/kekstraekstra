using System;
using System.Linq;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Graphics;
using TheraEngine.Assets.Sea;
using TheraEngine.Collections;
using TheraEngine.Core;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Graphics.Effects;
using World_Of_Sea_Battle.Graphics.Shaders;
using World_Of_Sea_Battle.Scripts;

namespace World_Of_Sea_Battle.GameSystems
{
	// Token: 0x020004CB RID: 1227
	internal sealed class StaticSystem : GameSceneSystem
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x000F1874 File Offset: 0x000EFA74
		public float OpenWorldWinterFactor
		{
			get
			{
				return this.WinterLevelWorld((Global.Player == null) ? Engine.GS.Camera.Position.XZ() : Global.Player.Position);
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x000F18A3 File Offset: 0x000EFAA3
		public float StrongEffectAmount
		{
			get
			{
				return this.{24845}.CurrentSoftValue;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x000F18B0 File Offset: 0x000EFAB0
		public bool HasStrongFogEffect
		{
			get
			{
				return Global.Player != null && (Session.EventActionsPipeline.EventCategoryAmount(EActionCaterory.StrongFog) > 0f || (Global.Player.MapInfo.IsPassingUi && Session.CurrentPassingSession != null && Session.CurrentPassingSession.DiffCards.Contains(PassingMapDiffCard.StrongFog)));
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x000F1905 File Offset: 0x000EFB05
		public float GetZipperLighting
		{
			get
			{
				return this.{24834}.GetFinalLightingFromTinder;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x000F1912 File Offset: 0x000EFB12
		public float GetRainCurrentPower
		{
			get
			{
				if (!this.{24834}.IsEnabled)
				{
					return 0f;
				}
				return this.{24834}.CurrentPower;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x000F1932 File Offset: 0x000EFB32
		public WorldOfSeaBattleSkyRenderer GetSkyShader
		{
			get
			{
				return this.{24832};
			}
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x000F1968 File Offset: 0x000EFB68
		public override void Initialize(ContentManager {24813})
		{
			base.Initialize({24813});
			this.{24832} = new WorldOfSeaBattleSkyRenderer("Shaders//Sky//SkyRenderer", {24813});
			this.{24833} = new FogHorizontShader("Shaders//MetarialPass//VolumetricFog", {24813});
			this.{24834} = new RainScript();
			this.{24834}.IsEnabled = false;
			this.{24835} = new SnowScript();
			this.{24835}.IsEnabled = false;
			this.{24836} = new OceanFoamRenderer(140f, 210f);
			Global.Game.EvEntryToGame += delegate()
			{
			};
			this.{24837} = new OceanFoamParticleDescription(new TextuePathRandomSelect(new Rectangle[]
			{
				new Rectangle(3, 1300, 128, 128),
				new Rectangle(3, 1428, 128, 128),
				new Rectangle(3, 1556, 128, 128)
			}), AtlasObjs.Texture.Size, 4600f, 0f, 1f);
			this.{24838} = new OceanFoamParticleDescription(new TextuePathRandomSelect(new Rectangle[]
			{
				new Rectangle(406, 1303, 256, 256)
			}), AtlasObjs.Texture.Size, 1600f, 0f, 1f);
			this.{24840} = new OceanFoamParticleDescription(new TextuePathRandomSelect(new Rectangle[]
			{
				new Rectangle(143, 1296, 128, 128),
				new Rectangle(143, 1424, 128, 128)
			}), AtlasObjs.Texture.Size, 6000f, 1.2f, 1f);
			this.{24841} = new OceanFoamParticleDescription(new TextuePathRandomSelect(new Rectangle[]
			{
				new Rectangle(143, 1296, 128, 128),
				new Rectangle(143, 1424, 128, 128)
			}), AtlasObjs.Texture.Size, 6000f, 1.2f, 1f);
			this.{24839} = new OceanFoamParticleDescription(new TextuePathRandomSelect(new Rectangle[]
			{
				new Rectangle(232, 1034, 256, 256)
			}), AtlasObjs.Texture.Size, 1000f, 0f, 15f);
			this.{24843} = new FowlInstantiator(FowlInstantiator.Type.Bird, "Take 001", new UWModel[]
			{
				LocalContent.Loaded.Bird
			});
			this.{24844} = new FowlInstantiator(FowlInstantiator.Type.Fish, "", LocalContent.Loaded.Fish);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x000ED147 File Offset: 0x000EB347
		public override void On()
		{
			base.On();
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x000F1C48 File Offset: 0x000EFE48
		public override void Off()
		{
			this.{24836}.RemoveAllParticles();
			base.Off();
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x000F1C5C File Offset: 0x000EFE5C
		public override void Update(ref FrameTime {24814})
		{
			if (base.IsActive && this.{24830})
			{
				this.{24845}.Evalute(ref {24814}, this.HasStrongFogEffect);
				this.{24832}.Update(ref {24814});
				this.{24843}.Update(ref {24814});
				this.{24844}.Update(ref {24814});
				this.{24836}.Update(ref {24814});
				Global.RenderStats.OceanParticleRenderCount = this.{24836}.ParticlesCount;
				float openWorldWinterFactor = this.OpenWorldWinterFactor;
				float num = Geometry.Saturate((CommonGlobal.CurrentClientWeather.RainingLevelClient - 0.1f) / 0.5f);
				this.{24834}.TargetPower = num * (1f - openWorldWinterFactor);
				this.{24835}.TargetPower = num * openWorldWinterFactor;
				this.{24834}.IsEnabled = (this.{24834}.CurrentPower != 0f || this.{24834}.TargetPower != 0f);
				this.{24835}.IsEnabled = (this.{24835}.CurrentPower != 0f || this.{24835}.TargetPower != 0f);
				this.{24844}.IsEnabled = (num < 0.6f && Global.Settings.RendererSsaoAndRefractions && Global.Render.LogicallyOverloadingFactor < 0.3f && Session.TimeFromLastSendedCBDamageSec > 30f);
				if (this.{24842})
				{
					this.{24834}.CurrentPower = this.{24834}.TargetPower;
					this.{24835}.CurrentPower = this.{24835}.TargetPower;
					this.{24842} = false;
				}
				if (Global.Player != null && Global.Settings.EnableBasicEffects)
				{
					float num2 = (1f - this.{24832}.DayOrNight) * (1f - this.{24832}.DayOrNight);
					WeatherEngine currentClientWeather = CommonGlobal.CurrentClientWeather;
					Vector2 position = Global.Player.Position;
					float num3;
					currentClientWeather.ExtraFogEffect(position, Global.Player.MapInfo.IsWorldmap, out num3);
					num3 = Math.Max(num3, Global.Player.MapInfo.Weather.RainingLevelClient);
					num3 = Math.Max(num3, 2f * Global.Game.StaticSystem.StrongEffectAmount);
					float num4 = Math.Max(0f, num3 - 1f);
					if (Rand.Chanse(Math.Min(1.2f, num3) * 50f * {24814}.Factor))
					{
						Vector3 vector = Rand.NextVector3(-1f, 1f).Normal();
						Vector2 vector2 = vector.XZ().Normal();
						FXEngine.Template_VolumetricFog_Sample(Global.Player.Position3D + Rand.Range(60f, 300f) * new Vector3(vector2.X, (0.1f + 0.1f * num4) * vector.Y, vector2.Y), 0.2f + num4 * 0.15f, 4000f, 60f);
					}
					if (Rand.Chanse(num2 * 80f * (1f - num3) * {24814}.Factor))
					{
						Vector3 vector3 = Rand.NextVector3(-1f, 1f).Normal();
						Vector2 vector4 = vector3.XZ().Normal();
						FXEngine.Template_VolumetricFog_Sample(Global.Player.Position3D + Rand.Range(40f, 300f) * new Vector3(vector4.X, (0.1f + 0.1f * num4) * vector3.Y, vector4.Y), 0.2f + num4 * 0.15f, 4000f, 40f);
					}
				}
				if (Global.Player != null && {24814}.EvaluteTimerSec2(ref this.{24846}))
				{
					this.{24846} = 1f;
					WorldMapInfo mapInfo = Global.Player.MapInfo;
					Vector2 position = Global.Player.Position;
					Tlist<IsleInstance> visibleIsles = mapInfo.GetVisibleIsles(position);
					if (!this.{24847} && CommonGlobal.CurrentClientWeather.FogLevelClient > 0.1f && visibleIsles.Size > 0)
					{
						if (visibleIsles.Min((IsleInstance {24848}) => Vector2.Distance({24848}.GlobalPosition, Global.Player.Position) - {24848}.ModelGlobalBS.Radius) > 150f)
						{
							this.{24847} = true;
							this.{24846} = 600f;
							new WhalesFormWaterFSEffect(Global.Player);
						}
					}
					if (CommonGlobal.CurrentClientWeather.FogLevelClient == 0f)
					{
						this.{24847} = true;
					}
				}
			}
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x000F20D0 File Offset: 0x000F02D0
		public void Render3DSkyStatic(bool {24815}, bool {24816}, bool {24817})
		{
			if (this.{24830})
			{
				this.{24832}.Apply({24815}, {24816}, {24817});
				if ({24815})
				{
					this.{24833}.Render(false);
				}
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x000F20F8 File Offset: 0x000F02F8
		public void Render3DMain()
		{
			if (Global.Settings.EnableBasicEffects)
			{
				float {23619} = this.{24832}.MorningSunrise(Global.Render.GetSceneManager);
				this.{24833}.RenderHaze({23619});
			}
			Global.Render.CommonShader.RenderObject(this.{24843}.Scene, true, 1f, false, 0f, false);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x000F215A File Offset: 0x000F035A
		public void Render3D()
		{
			if (Global.Settings.RendererSsaoAndRefractions)
			{
				Global.Render.CommonShader.RenderObject(this.{24844}.Scene, true, 1f, false, 0f, false);
			}
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x000F2190 File Offset: 0x000F0390
		public void SSEffectsRender3DOceanParticles()
		{
			Color blue = Color.Blue;
			this.{24836}.Render(blue.ToVector4(), CommonGlobal.CurrentClientWeather.WavesPoisiton, CommonGlobal.CurrentClientWeather.WavesHeightClient, AtlasObjs.Texture.Tex);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000F21D3 File Offset: 0x000F03D3
		public void Render3DSnowOrRain()
		{
			Global.Game.GetScriptManager.Render();
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000F21E4 File Offset: 0x000F03E4
		public void Render2DItems()
		{
			bool flag = this.{24830};
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x000F21ED File Offset: 0x000F03ED
		public float WinterLevelWorld(Vector2 {24818})
		{
			if (Global.Player == null)
			{
				return 0f;
			}
			return Global.Player.MapInfo.OpenWorldWinterFactor({24818});
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x000F220D File Offset: 0x000F040D
		public void SetDefaultSky()
		{
			Global.Render.SetMap(Global.Player.MapInfo, this.{24832}.ChangeSky(Global.Player.MapInfo, false), true);
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x000F223C File Offset: 0x000F043C
		public void SetDecorateMap()
		{
			WorldMapInfo worldMap = Gameplay.WorldMap;
			this.{24831} = worldMap;
			this.{24830} = true;
			Global.Render.SetMap(worldMap, this.{24832}.ChangeSky(null, true), true);
			this.{24834}.TargetPower = 0f;
			this.{24834}.CurrentPower = 0f;
			this.{24834}.IsEnabled = false;
			this.{24835}.TargetPower = 0f;
			this.{24835}.CurrentPower = 0f;
			this.{24835}.IsEnabled = false;
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x000F22CE File Offset: 0x000F04CE
		public void SetMap(WorldMapInfo {24819})
		{
			this.{24831} = {24819};
			this.{24830} = true;
			Global.Render.SetMap({24819}, this.{24832}.ChangeSky({24819}, false), false);
			this.{24845}.SetValue(this.HasStrongFogEffect);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x000F2308 File Offset: 0x000F0508
		public void SetCurrentRainPower()
		{
			if (!this.{24830})
			{
				throw new InvalidOperationException();
			}
			this.{24842} = true;
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x000F2320 File Offset: 0x000F0520
		public void AddOceanParticle(in Vector2 {24820}, float {24821}, bool {24822}, bool {24823})
		{
			for (int i = 0; i < ({24823} ? 1 : 2); i++)
			{
				Vector2 vector;
				vector.X = {24820}.X + Rand.Range(-{24821} * 0.1f, {24821} * 0.1f);
				vector.Y = {24820}.Y + Rand.Range(-{24821} * 0.1f, {24821} * 0.1f);
				float {15697} = {24821} * Rand.Range(0.8f, 1.2f);
				this.{24836}.AddParticle({24822} ? this.{24838} : ({24823} ? this.{24839} : this.{24837}), vector.X, vector.Y, Rand.Angle(), {15697}, {24823} ? 2f : 1f, !{24823});
			}
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x000F23EC File Offset: 0x000F05EC
		public void AddOceanParticle(in Vector2 {24824}, float {24825}, OceanParticleType {24826}, float {24827}, float {24828}, float {24829})
		{
			this.{24836}.AddParticle(({24826} == OceanParticleType.Left) ? this.{24840} : this.{24841}, {24824}.X, {24824}.Y, Geometry.AxisNorm(1.5707964f - {24827} + 3.1415927f * (({24826} == OceanParticleType.Right) ? 1f : 0f)), {24825}, {24829}, {24828});
		}

		// Token: 0x040019A2 RID: 6562
		private bool {24830};

		// Token: 0x040019A3 RID: 6563
		private WorldMapInfo {24831};

		// Token: 0x040019A4 RID: 6564
		private WorldOfSeaBattleSkyRenderer {24832};

		// Token: 0x040019A5 RID: 6565
		private FogHorizontShader {24833};

		// Token: 0x040019A6 RID: 6566
		private RainScript {24834};

		// Token: 0x040019A7 RID: 6567
		private SnowScript {24835};

		// Token: 0x040019A8 RID: 6568
		private OceanFoamRenderer {24836};

		// Token: 0x040019A9 RID: 6569
		private OceanFoamParticleDescription {24837};

		// Token: 0x040019AA RID: 6570
		private OceanFoamParticleDescription {24838};

		// Token: 0x040019AB RID: 6571
		private OceanFoamParticleDescription {24839};

		// Token: 0x040019AC RID: 6572
		private OceanFoamParticleDescription {24840};

		// Token: 0x040019AD RID: 6573
		private OceanFoamParticleDescription {24841};

		// Token: 0x040019AE RID: 6574
		private bool {24842};

		// Token: 0x040019AF RID: 6575
		private FowlInstantiator {24843};

		// Token: 0x040019B0 RID: 6576
		private FowlInstantiator {24844};

		// Token: 0x040019B1 RID: 6577
		private SoftTrigger {24845} = new SoftTrigger(0f, 1f, 0.04f);

		// Token: 0x040019B2 RID: 6578
		private float {24846} = 15f;

		// Token: 0x040019B3 RID: 6579
		private bool {24847};
	}
}
