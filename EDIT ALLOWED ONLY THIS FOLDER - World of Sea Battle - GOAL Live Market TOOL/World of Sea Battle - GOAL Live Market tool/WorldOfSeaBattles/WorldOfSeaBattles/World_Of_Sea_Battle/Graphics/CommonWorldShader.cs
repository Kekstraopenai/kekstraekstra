using System;
using System.IO;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Assets.Graphics;
using TheraEngine.Assets.Sea;
using TheraEngine.Assets.Shaders;
using TheraEngine.Components;
using TheraEngine.Components.Architecture;
using TheraEngine.Components.Scene;
using TheraEngine.Core;
using TheraEngine.Graphics;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using TheraEngine.Renderer;
using TheraEngine.Scene;
using TheraEngine.Scene.Lighting;
using WorldOfSeaBattles;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Graphics
{
	// Token: 0x02000439 RID: 1081
	internal sealed class CommonWorldShader : Shader, ISceneObject3DParent, IUpdateableObject
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x000C6376 File Offset: 0x000C4576
		public int? CurrentDesignReplace
		{
			get
			{
				return this.{23330};
			}
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x000C6380 File Offset: 0x000C4580
		public CommonWorldShader(ContentManager {23134}) : base("Shaders//MetarialPass//CommonWorldShader", {23134})
		{
			base.GetProperty("CameraFarPlane").SetValue(Renderer.CameraFarPlane);
			this.TXAAKernel = base.GetProperty("TXAAKernel");
			this.TXAAKernelNrm = base.GetProperty("TXAAKernelNrm");
			this.{23237} = base.GetProperty("ViewProj");
			this.{23264} = base.GetProperty("projTemp");
			this.{23238} = base.GetProperty("ViewMatrix");
			this.{23239} = base.GetProperty("ReflectionVWP");
			this.{23241} = base.GetProperty("CameraPosition");
			this.{23240} = base.GetProperty("CameraDirection");
			this.{23242} = base.GetProperty("CentralLightSourceNormal");
			this.{23243} = base.GetProperty("ForFogLightSourceNormal");
			this.{23244} = base.GetProperty("LightSourceDiffuseColor");
			this.{23245} = base.GetProperty("LightSourceSpecularColor");
			this.{23246} = base.GetProperty("CommonLightColor");
			this.{23247} = base.GetProperty("CommonFogColor");
			this.{23248} = base.GetProperty("GameTime");
			this.{23250} = base.GetProperty("wawePosition");
			this.{23251} = base.GetProperty("height");
			this.{23252} = base.GetProperty("PointLightsActiveCount");
			this.{23253} = base.GetProperty("PointLights");
			this.{23254} = base.GetProperty("FlashScissoringObjectsEnabled");
			this.{23283} = base.GetProperty("MaterialProperties");
			this.{23255} = base.GetProperty("ParaboloidBasis");
			this.{23234} = base.GetProperty("OutputSize");
			this.{23249} = base.GetProperty("MinShadowCascadeIs1");
			this.{23235} = base.GetProperty("KeyOPressed");
			this.{23236} = base.GetProperty("KeyPPressed");
			this.{23256} = base.GetProperty("OceanWorld");
			this.{23262} = base.GetProperty("World");
			this.{23263} = base.GetProperty("WorldInverseTranspose");
			this.ObjectCommonTransparancy = base.GetProperty("ObjectCommonTransparancy");
			this.{23265} = base.GetProperty("IsleFog_Start");
			this.{23266} = base.GetProperty("IsleFog_End");
			this.{23267} = base.GetProperty("IsleClipPlane");
			this.{23268} = base.GetProperty("ClothAnimationOffsets");
			this.{23269} = base.GetProperty("ClothWildPower");
			this.{23270} = base.GetProperty("ClothAnimationType1");
			this.{23271} = base.GetProperty("ClothAnimationType2");
			this.{23272} = base.GetProperty("ClothAnimationType3");
			this.{23273} = base.GetProperty("SailBoxMin");
			this.{23274} = base.GetProperty("SailBoxMax");
			this.{23275} = base.GetProperty("SailRollDir");
			this.{23276} = base.GetProperty("SailRollPos");
			this.{23277} = base.GetProperty("SailWindDirection");
			this.{23278} = base.GetProperty("IsTerrain");
			this.{23279} = base.GetProperty("IsTripleTerrain");
			this.{23280} = base.GetProperty("Weather");
			this.{23260} = base.GetProperty("FogLerpFactor");
			this.{23261} = base.GetProperty("ScatterIntesity");
			this.{23281} = base.GetProperty("od_reflectionColorMul");
			this.{23282} = base.GetProperty("od_refractionColor");
			this.{23257} = base.GetProperty("od_fresnelPower");
			this.{23258} = base.GetProperty("od_extraSharpness");
			this.{23259} = base.GetProperty("od_progressiveFresnel");
			this.{23287} = base.GetProperty("_OceanNormalMap1");
			this.{23288} = base.GetProperty("_OceanNormalMap2");
			this.{23284} = base.GetProperty("_OceanEffectMap");
			this.{23285} = base.GetProperty("_OceanEffectMapVS");
			this.{23286} = base.GetProperty("OceanEffectMapPixSize");
			this.{23290} = base.GetProperty("_ObjectTexture1");
			this.{23291} = base.GetProperty("_ObjectTexture2");
			this.{23292} = base.GetProperty("_ObjectTexture3");
			this.{23293} = base.GetProperty("_ObjectTexture4");
			this.{23294} = base.GetProperty("_ObjectTexture5");
			this.{23295} = base.GetProperty("_AOMap");
			this.{23299} = base.GetProperty("_ShipDestrMap");
			this.{23289} = base.GetProperty("_SceneRefraction");
			this.{23300} = base.GetProperty("SailLayer");
			this.{23302} = base.GetProperty("OutlineShaderColor");
			this.{23304} = base.GetProperty("OutlineObjRadius");
			this.{23296} = base.GetProperty("_ParaboloidFront");
			this.{23298} = base.GetProperty("_ParaboloidBack");
			this.{23297} = base.GetProperty("_ParaboloidFrontNoScene");
			this.{23301} = base.GetProperty("FoamDynamicCut");
			this.{23303} = base.GetProperty("SimulationTime");
			this.{23232} = base.GetProperty("Bones");
			this.{23233} = base.GetProperty("XCutAnimation");
			this.{23305} = base.GetProperty("OceanDeadZoneDistance");
			this.{23306} = base.GetProperty("ShadowMap0");
			this.{23307} = base.GetProperty("GBuffer2_DepthAndNormals");
			this.{23308} = base.GetProperty("ShadowCascades");
			this.{23320} = base.GetEffectBase.Techniques["OceanMainVersion3"];
			this.{23321} = base.GetEffectBase.Techniques["ObjectMain"];
			this.{23315} = this.{23320}.Passes["OceanLQ"];
			this.{23316} = this.{23320}.Passes["OceanHQ"];
			this.{23317} = this.{23320}.Passes["OceanHQRefraction"];
			this.{23318} = this.{23320}.Passes["OceanHQShadow"];
			this.{23319} = this.{23320}.Passes["OceanHQRefractionShadow"];
			this.{23314} = this.{23321}.Passes["Outline"];
			base.GetProperty("_FoamMap").SetValue({23134}.Load<Texture2D>(PathContent.dir_ocean_textures + "foam_hlsl"));
			base.GetProperty("_FoamInt").SetValue({23134}.Load<Texture2D>(PathContent.dir_ocean_textures + "foam_intensivity"));
			this.{23312} = new Texture2D(Engine.GS.graphicsDevice, 4, 4, false, SurfaceFormat.Color);
			Color color = new Color(128, 128, 255);
			this.{23312}.SetData<Color>(new Color[]
			{
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color
			});
			this.{23313} = new Texture2D(Engine.GS.graphicsDevice, 4, 4, false, SurfaceFormat.Color);
			color = new Color(0, 0, 0, 0);
			this.{23313}.SetData<Color>(new Color[]
			{
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color,
				color
			});
			base.GetEffectBase.CurrentTechnique = this.{23321};
			this.ObjectCommonTransparancy.SetValue(1);
			base.GetProperty("_ShipDestrEffect").SetValue({23134}.Load<Texture2D>("ModelTextures\\Lod0\\ShipDestrDecal"));
			base.GetProperty("_NoiseTex").SetValue(Engine.SimpleNoise);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000C6BF2 File Offset: 0x000C4DF2
		public void ChangeMap(WorldMapInfo {23135}, bool {23136})
		{
			short id = {23135}.ID;
			this.{23328} = {23135};
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00003100 File Offset: 0x00001300
		public void TestMethod11()
		{
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x000C6C02 File Offset: 0x000C4E02
		private float {23137}(float {23138})
		{
			{19994}.Me({19988}.Info, {23138}.ToString(), Array.Empty<object>());
			return {23138};
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x000C6C1C File Offset: 0x000C4E1C
		public void Update(ref FrameTime {23139})
		{
			if (CommonWorldShader.GraphicsEngineerMode && InputHelper.IsClick(Keys.G))
			{
				this.{23208}();
			}
			if (CommonWorldShader.OceanWawesEditor && InputHelper.IsClick(Keys.G))
			{
				this.{23209}();
			}
			if (CommonWorldShader.StyleditorMode && InputHelper.IsClick(Keys.G))
			{
				this.{23207}();
			}
			if (CommonWorldShader.SSDOParamsEditor && InputHelper.IsClick(Keys.G))
			{
				this.{23206}();
			}
			if (InputHelper.IsClick(Keys.G))
			{
				CommonWorldShader.DebugTextureShow = !CommonWorldShader.DebugTextureShow;
			}
			if (Debugging.EnableWaterShipParamsEditor && InputHelper.IsClick(Keys.O))
			{
				ShipPhysics.TestShipParamsEnable = true;
				StackForm stackForm = new StackForm(new Vector2(700f, 50f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				StackForm stackForm2 = stackForm;
				UiControl[] array = new UiControl[1];
				array[0] = CommonWorldShader.ColorEditor("Water, Gravit, Deriv,: ", () => new Vector3(ShipPhysics.TestShipParams.D_WaterPres, ShipPhysics.TestShipParams.D_GracPres, ShipPhysics.TestShipParams.D_Deriv), delegate(Vector3 {23337})
				{
					ShipPhysics.TestShipParams.D_WaterPres = {23337}.X;
					ShipPhysics.TestShipParams.D_GracPres = {23337}.Y;
					ShipPhysics.TestShipParams.D_Deriv = {23337}.Z;
				}, 10f);
				stackForm2.AddItem(array);
				StackForm stackForm3 = stackForm;
				UiControl[] array2 = new UiControl[1];
				array2[0] = CommonWorldShader.ColorEditor("D_MaxDeriv: ", () => new Vector3(ShipPhysics.TestShipParams.D_MaxDeriv, 0f, 0f), delegate(Vector3 {23338})
				{
					ShipPhysics.TestShipParams.D_MaxDeriv = {23338}.X;
				}, 10f);
				stackForm3.AddItem(array2);
			}
			this.{23303}.SetValue((float)Global.Game.GameTotalTimeSec);
			double gameTotalTimeSec = Global.Game.GameTotalTimeSec;
			this.{23322} += (0.4f * {23139}.secElapsed * 0.015f * MathHelper.Max(CommonGlobal.CurrentClientWeather.ClientWindPowerEffect / 30f, 0.4f) * 0.25f + {23139}.secElapsed * 0.02f) * 0.3f;
			if (this.{23322} > 64f)
			{
				this.{23322} -= 64f;
			}
			this.{23323}.X = this.{23323}.X + ({23139}.secElapsed * CommonGlobal.CurrentClientWeather.ClientWindPowerEffect * 0.3f + 0.25f) * 0.25f * {23139}.msElapsed / 16.6666f;
			this.{23323}.Y = this.{23323}.Y + ({23139}.secElapsed * CommonGlobal.CurrentClientWeather.ClientWindPowerEffect * 0.45f + 0.35f) * 0.25f * {23139}.msElapsed / 16.6666f;
			if (this.{23323}.X > 99887f)
			{
				this.{23323}.X = 0f;
			}
			if (this.{23323}.Y > 99887f)
			{
				this.{23323}.Y = 5f;
			}
			this.{23324} = (0.4f + CommonGlobal.CurrentClientWeather.ClientWindPowerEffect / 120f) * 0.26f;
			this.{23235}.SetValue(InputHelper.NowInputState.IsDown(Keys.O));
			this.{23236}.SetValue(InputHelper.NowInputState.IsDown(Keys.P));
			if (Global.Player != null && Global.Player.MapInfo.IsEnableArenaUi && Session.CurrentArenaSession != null && Session.CurrentArenaSession.ModeInfo.EnableDangerZone)
			{
				this.{23305}.SetValue(Session.CurrentArenaSession.DeadZoneDistance);
				return;
			}
			this.{23305}.SetValue(float.MaxValue);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x000C6F84 File Offset: 0x000C5184
		public void SetCustomLightData(Vector3 {23140}, Vector3 {23141}, Vector3 {23142}, Vector3 {23143}, PointLight {23144})
		{
			this.{23244}.SetValue({23140});
			this.{23245}.SetValue({23141});
			this.{23247}.SetValue(new Vector4({23142}, 1f));
			this.{23246}.SetValue(new Vector3(15f, 20f, 35f) / 220f + {23142} * 0.6f);
			this.{23242}.SetValue(-{23143});
			if ({23144} != null)
			{
				Global.Render.Pointlights.Add({23144});
				this.{23336} = Global.Render.Pointlights.CommonIntensivity;
				Global.Render.Pointlights.CommonIntensivity = 1f;
			}
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x000C7048 File Offset: 0x000C5248
		public void ResetCustomLightData(PointLight {23145})
		{
			Global.Render.Pointlights.Remove({23145});
			Global.Render.Pointlights.CommonIntensivity = this.{23336};
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x000C7070 File Offset: 0x000C5270
		public void DrawPayerLighter(bool {23146})
		{
			if ({23146})
			{
				float amount = 1f - Math.Max(Global.Game.StaticSystem.GetSkyShader.DayOrNight, Global.Game.StaticSystem.GetSkyShader.Sunrise);
				this.{23244}.SetValue(Global.Game.StaticSystem.GetSkyShader.CurrentDiffuseColor * MathHelper.Lerp(1f, 0.9f, amount));
				this.{23246}.SetValue(Global.Game.StaticSystem.GetSkyShader.CommonLightColor * MathHelper.Lerp(1f, 1.5f, amount));
				return;
			}
			this.{23244}.SetValue(Global.Game.StaticSystem.GetSkyShader.CurrentDiffuseColor);
			this.{23246}.SetValue(Global.Game.StaticSystem.GetSkyShader.CommonLightColor);
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x000C7160 File Offset: 0x000C5360
		public void PrepareForRenderCommon()
		{
			this.OperationCommonSetCameraState();
			this.{23244}.SetValue(Global.Game.StaticSystem.GetSkyShader.CurrentDiffuseColor);
			this.{23245}.SetValue(Global.Game.StaticSystem.GetSkyShader.CurrentSpecularColor);
			this.{23247}.SetValue(new Vector4(Global.Game.StaticSystem.GetSkyShader.CurrentFogColor, Global.Game.StaticSystem.GetSkyShader.DayOrNight));
			this.{23246}.SetValue(Global.Game.StaticSystem.GetSkyShader.CommonLightColor);
			this.{23242}.SetValue(-Global.Render.GetSceneManager.CurrentLightDirection);
			this.{23243}.SetValue(Global.Render.GetSceneManager.SunLightSource.LightDirectionForRender);
			this.{23248}.SetValue(this.{23322});
			float num = 0f;
			this.{23250}.SetValue(CommonGlobal.CurrentClientWeather.WavesPoisiton);
			this.{23251}.SetValue(CommonGlobal.CurrentClientWeather.WavesHeightClient * (1f - num * 1.5f));
			this.{23258}.SetValue((Global.Settings.SharpWater ? 1.9f : (1f + Global.Render.CloudeLevel * 0.4f)) * (1f - 0.6f * num));
			this.{23259}.SetValue((Global.Settings.ProgressiveFresnel > false) ? 1 : 0);
			this.{23269}.SetValue(this.{23324});
			float fogLevel = Global.Render.FogLevel;
			float num2 = Renderer.FogStart_Isles * (1f - fogLevel * 2.5f);
			float num3 = Renderer.FogEnd_Isles * (1f - 0.25f * fogLevel) * (1f - 0.33f * Global.Game.StaticSystem.GetSkyShader.CloudyLevelToStyleFog);
			if (Global.Camera.IsSpyglass)
			{
				num2 += 50f;
				num3 += 100f;
			}
			if (!{18560}.closed && {18560}.visibility != {18560}.VisibleDistance.Normal)
			{
				num2 = 10000f;
				num3 = 10000f;
			}
			base.GetProperty("fogSettings").SetValue(new Vector4(0.04f, 0f, 1f, 2f));
			float num4 = Global.Game.StaticSystem.GetSkyShader.DayOrNight * (1f - CommonGlobal.CurrentClientWeather.RainingLevelClient * 0.8f);
			float z = Geometry.Saturate(Global.Render.FogLevel / 0.5f + Global.Game.StaticSystem.OpenWorldWinterFactor * 0.1f);
			float num5 = Global.Game.StaticSystem.GetSkyShader.Sunrise * (1f - Global.Render.CloudeLevel);
			this.{23265}.SetValue(num2);
			this.{23266}.SetValue(num3);
			this.{23267}.SetValue(Renderer.CameraFarPlane - 30f);
			Global.Render.Pointlights.CommonIntensivity = 0.7f + 0.3f * Global.Game.StaticSystem.GetSkyShader.DayOrNight;
			float num6 = Global.Game.StaticSystem.GetRainCurrentPower;
			num6 = MathF.Max(num6, Global.Game.StaticSystem.OpenWorldWinterFactor * 0.6f);
			this.{23301}.SetValue(new Vector2((1f - num6) * 0.4f, 0.5f + 0.5f * num5 + 0.41399997f * (1f - num6)));
			this.{23280}.SetValue(new Vector4(num4, CommonGlobal.CurrentClientWeather.RainingLevelClient, z, Global.Game.StaticSystem.OpenWorldWinterFactor));
			float y = Global.Render.GetSceneManager.SunLightSource.LightDirectionForRender.Y;
			this.{23260}.SetValue(this.FogLerpFactorLastValue = new Vector2(Geometry.Saturate(Math.Abs(y) / 0.15f), Geometry.Saturate(4f * MathF.Pow(1f + y, 4f) * num4) * 0.5f));
			this.{23261}.SetValue((1f - 0.5f * Global.Render.CloudeLevel) * Geometry.Saturate(Math.Abs(Global.Render.GetSceneManager.CurrentLightDirection.Y) * 10f) * (Global.Game.StaticSystem.GetSkyShader.DayOrNight * 0.5f + 0.5f) * (1f - 0.5f * Global.Game.StaticSystem.GetSkyShader.Sunrise));
			this.{23281}.SetValue(Vector3.Lerp(Vector3.Lerp(Vector3.Lerp(new Vector3(0.9f, 0.9f, 0.8f), new Vector3(0.65f, 0.71f, 0.79f), num4), new Vector3(1f, 1f, 0.9f), Global.Render.CloudeLevel), Vector3.One, 0.95f * num5));
			float num7 = (Global.Player == null) ? 0.5f : Global.Player.MapInfo.GetDeepnessLevel(Engine.GS.Camera.Position.XZ());
			num7 = (num7 - 0.5f) / 0.5f;
			this.{23282}.SetValue(Vector3.Lerp(new Vector3(0f, 0.01f, 0.002f), new Vector3(0f, 0.02f, 0.037f), num4 * (1f - 0.5f * num7)));
			this.{23257}.SetValue(Global.Settings.LightWater ? 3f : 7f);
			this.{23291}.SetValue(null);
			this.{23292}.SetValue(null);
			this.{23293}.SetValue(null);
			Global.Render.ItemsShader.SetOceanData(CommonGlobal.CurrentClientWeather.WavesPoisiton, CommonGlobal.CurrentClientWeather.WavesHeightClient);
			this.{23234}.SetValue(Global.Game.WindowSize);
			this.{23275}.SetValue(Vector4.Zero);
			this.{23295}.SetValue((Global.Settings.RendererSsaoAndRefractions && Global.Render.rtGilMap != null) ? Global.Render.rtGilMap.Resource : this.{23313});
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x000C77E4 File Offset: 0x000C59E4
		public void OperationCommonSetCameraState()
		{
			this.{23237}.SetValue(Engine.GS.Camera.ViewMultiplyProjection);
			this.{23264}.SetValue(Engine.GS.Camera.ProjMatrix);
			this.{23238}.SetValue(Engine.GS.Camera.ViewMatrix);
			this.{23241}.SetValue(Engine.GS.Camera.Position);
			this.{23240}.SetValue(Engine.GS.Camera.Direction);
			Global.Render.ItemsShader.SetBasicProperties(Engine.GS.Camera, AtlasObjs.Texture, Global.Render.GetSceneManager, Global.Game.StaticSystem.GetSkyShader);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000C78AC File Offset: 0x000C5AAC
		public void SetShadowGBuffer()
		{
			this.{23306}.SetValue(Global.Render.CascadedShadowHelper.Target.Resource);
			for (int i = 0; i < 3; i++)
			{
				ShadowMappingEngine shadowMappingEngine = Global.Render.ShadowCascades[i];
				EffectParameter effectParameter = this.{23308}.Elements[i];
				effectParameter.StructureMembers[0].SetValue(shadowMappingEngine.ShadowMap.Size);
				if (i < 2 && Global.Render.shadowsOnlyCascade3)
				{
					effectParameter.StructureMembers[1].SetValue(Matrix.CreateTranslation(1000000f, 0f, 1000000f));
				}
				else
				{
					effectParameter.StructureMembers[1].SetValue(shadowMappingEngine.LightViewProj);
				}
				effectParameter.StructureMembers[2].SetValue(shadowMappingEngine.MinBias);
				effectParameter.StructureMembers[3].SetValue((i == 0) ? Vector3.UnitX : ((i == 1) ? Vector3.UnitY : Vector3.UnitZ));
			}
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x000C79B5 File Offset: 0x000C5BB5
		public void SetEyeGBuffer(RenderTarget {23147})
		{
			this.{23307}.SetValue(({23147} == null) ? null : {23147}.Resource);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000C79D0 File Offset: 0x000C5BD0
		public void RenderOcean(RenderTarget {23148}, bool {23149} = false)
		{
			if (!{18560}.closed && {18560}.visibility == {18560}.VisibleDistance.HighNoOcean)
			{
				return;
			}
			this.{23288}.SetValue(Global.Render.oceanSourceNormal);
			bool flag = Global.Render.rtSceneNormals != null;
			this.{23289}.SetValue(flag ? (({23148} != null) ? {23148}.Resource : null) : null);
			base.GetEffectBase.CurrentTechnique = this.{23320};
			bool flag2 = Global.Settings.ShadowsQuality > LocalSettings.RendererShadows.Off;
			EffectPass effectPass;
			if (flag)
			{
				effectPass = (flag2 ? this.{23319} : this.{23317});
			}
			else if (flag2)
			{
				effectPass = this.{23318};
			}
			else
			{
				effectPass = (Global.Settings.EnableBasicEffects ? this.{23316} : this.{23315});
			}
			this.{23287}.SetValue(Global.Render.rtOceanNormals.Resource);
			Vector3 position = Engine.GS.Camera.Position;
			float num = 10.799999f;
			Vector3 vector = position + Engine.GS.Camera.Direction * 25f;
			float xPosition = MathF.Round(vector.X / num) * num;
			float zPosition = MathF.Round(vector.Z / num) * num;
			float radians = MathF.Round(-Engine.GS.Camera.Rotation.Y);
			this.{23256}.SetValue(Matrix.CreateScale((float)(({18560}.visibility == {18560}.VisibleDistance.HighWithOcean) ? 10 : 1)) * Matrix.CreateRotationY(radians) * Matrix.CreateTranslation(xPosition, 0f, zPosition));
			effectPass.Apply();
			LocalContent.Loaded.WaterFrame.OptimizedRenderAllBuffers();
			base.GetEffectBase.CurrentTechnique = this.{23321};
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000C7B78 File Offset: 0x000C5D78
		public void ConfigureAnimatedMeshInNextRenderObject(Vector2 {23150}, AnimationType {23151})
		{
			this.{23270}.SetValue(false);
			this.{23271}.SetValue(false);
			this.{23272}.SetValue(false);
			if ({23151} == AnimationType.Flora)
			{
				this.{23270}.SetValue(true);
			}
			if ({23151} == AnimationType.Flagpoll)
			{
				this.{23271}.SetValue(true);
			}
			if ({23151} == AnimationType.Sail)
			{
				this.{23272}.SetValue(true);
			}
			this.{23268}.SetValue(this.{23323} + {23150});
			Texture2D sailDestructEffect = Materials.GetSailDestructEffect(255);
			this.{23293}.SetValue(sailDestructEffect);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000C7C0C File Offset: 0x000C5E0C
		private EffectPass {23152}(CommonWorldShader.TargetObject {23153}, bool {23154}, bool {23155})
		{
			if (!Global.Render.mrtNormals && !{23154} && !{23155})
			{
				return this.{23321}.Passes[(int)(28 + {23153})];
			}
			int num = (int)({23153} * CommonWorldShader.TargetObject.Skinning);
			if ({23154})
			{
				num++;
			}
			if ({23155})
			{
				num += 2;
			}
			return this.{23321}.Passes[num];
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x000C7C63 File Offset: 0x000C5E63
		private bool {23156}(ModelTransformedScene {23157})
		{
			return Global.Settings.ShadowsQuality != LocalSettings.RendererShadows.Off && !Renderer.ReflectionsAreBeingDrawn && {23157}.VisibleForAnyGBuffer;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x000C7C80 File Offset: 0x000C5E80
		public void RenderObject(ModelTransformedScene {23158}, bool {23159} = false, float {23160} = 1f, bool {23161} = false, float {23162} = 0f, bool {23163} = false)
		{
			if ({23158}.CountModels == 0)
			{
				return;
			}
			if ({23158}.VisibleTestType != ModelSceneVisibleTest.Disable)
			{
				if (!Renderer.ReflectionsAreBeingDrawn)
				{
					{23158}.UpdateMainCameraVisibility();
				}
				if (!{23158}.IsMainCameraVisible)
				{
					return;
				}
			}
			bool {23154} = !{23161} && this.{23156}({23158});
			this.{23326} = this.{23152}(({23158}.HaveAnimationsApprox && !{23163}) ? CommonWorldShader.TargetObject.Skinning : CommonWorldShader.TargetObject.Object, {23154}, Global.Settings.HighDetailing);
			this.{23327} = ({23159} ? this.{23326} : this.{23152}(CommonWorldShader.TargetObject.Sail, {23154}, Global.Settings.HighDetailing));
			this.{23332} = {23159};
			if ({23162} != 0f)
			{
				this.{23233}.SetValue({23162});
			}
			if ({23160} != 1f)
			{
				this.ObjectCommonTransparancy.SetValue({23160});
			}
			{23158}.Render(this);
			if ({23162} != 0f)
			{
				this.{23233}.SetValue(0);
			}
			if ({23160} != 1f)
			{
				this.ObjectCommonTransparancy.SetValue(1);
			}
			this.{23332} = false;
			if (this.{23334})
			{
				this.{23189}(null, null, null, 0f);
				this.{23329} = null;
				this.{23334} = false;
			}
			if (this.{23335})
			{
				this.{23329} = null;
				this.{23335} = false;
			}
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x000C7DB0 File Offset: 0x000C5FB0
		public void RenderObjectInstanced(ModelTransformedScene {23164}, InstancedModelRenderer {23165}, float {23166} = 1f)
		{
			bool {23154} = this.{23156}({23164});
			this.{23326} = this.{23152}(CommonWorldShader.TargetObject.Instancing, {23154}, Global.Settings.HighDetailing);
			this.{23327} = this.{23152}(CommonWorldShader.TargetObject.InstancingGreen, {23154}, Global.Settings.HighDetailing);
			if ({23166} != 1f)
			{
				this.ObjectCommonTransparancy.SetValue({23166});
			}
			{23165}.Instantiate(this, {23164}, 1);
			if ({23166} != 1f)
			{
				this.ObjectCommonTransparancy.SetValue(1);
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000C7E28 File Offset: 0x000C6028
		public void RenderObjectInstanced(ModelTransformedScene {23167}, SmartInstancing {23168}, float {23169} = 1f)
		{
			bool {23154} = this.{23156}({23167});
			this.{23326} = this.{23152}(CommonWorldShader.TargetObject.Instancing, {23154}, Global.Settings.HighDetailing);
			this.{23327} = this.{23152}(CommonWorldShader.TargetObject.InstancingGreen, {23154}, Global.Settings.HighDetailing);
			if ({23169} != 1f)
			{
				this.ObjectCommonTransparancy.SetValue({23169});
			}
			{23168}.Draw(this, {23167}, 1);
			if ({23169} != 1f)
			{
				this.ObjectCommonTransparancy.SetValue(1);
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x000C7EA0 File Offset: 0x000C60A0
		public void RenderObjectFast(ModelTransformedScene {23170})
		{
			this.{23326} = this.{23152}(CommonWorldShader.TargetObject.Environment, false, {23170} != LocalContent.Loaded.Shallow && Global.Settings.HighDetailing);
			this.{23327} = this.{23326};
			{23170}.Render(this);
			if (this.{23335})
			{
				this.{23329} = null;
				this.{23335} = false;
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x000C7F00 File Offset: 0x000C6100
		public void RenderIsle(ModelTransformedScene {23171}, bool {23172}, int {23173} = -1)
		{
			if (!Renderer.ReflectionsAreBeingDrawn)
			{
				{23171}.UpdateMainCameraVisibility();
			}
			if ({23171}.VisibleTestType != ModelSceneVisibleTest.Disable && !{23171}.IsMainCameraVisible)
			{
				return;
			}
			bool {23154} = this.{23156}({23171});
			this.{23326} = this.{23152}(CommonWorldShader.TargetObject.Environment, {23154}, Global.Settings.HighDetailing);
			this.{23327} = (this.{23332} ? this.{23326} : this.{23152}(CommonWorldShader.TargetObject.Green, {23154}, Global.Settings.HighDetailing));
			if ({23172})
			{
				this.{23327} = this.{23326};
			}
			this.{23331} = {23173};
			this.{23333} = true;
			{23171}.Render(this);
			this.{23331} = -1;
			this.{23333} = false;
			if (this.{23335})
			{
				this.{23329} = null;
				this.{23335} = false;
			}
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x000C7FBC File Offset: 0x000C61BC
		public void RenderWorldMap(ModelTransformedScene {23174}, Texture2D {23175}, Texture2D {23176}, Texture2D {23177}, float {23178})
		{
			this.{23329} = {23175};
			this.{23326} = this.{23321}.Passes["WorldMapPlane"];
			this.{23327} = this.{23326};
			this.{23291}.SetValue({23176});
			this.{23292}.SetValue({23177});
			base.GetEffectBase.Parameters["MapScaleInterpUv"].SetValue({23178});
			this.{23333} = true;
			{23174}.Render(this);
			this.{23333} = false;
			if (this.{23335})
			{
				this.{23329} = null;
				this.{23335} = false;
			}
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000C8058 File Offset: 0x000C6258
		public void RenderOutline(ModelTransformedScene {23179}, Vector4 {23180})
		{
			this.{23304}.SetValue(new Vector4({23179}.CombinedModelSpaceBS.Center, {23179}.SingleSize));
			this.{23302}.SetValue({23180});
			{23179}.RenderWithShader(this.{23262}, this.{23263}, null, this.{23314});
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000C80AB File Offset: 0x000C62AB
		public void SetMinShadowCascadeIs1(bool {23181})
		{
			this.{23249}.SetValue({23181});
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000C80B9 File Offset: 0x000C62B9
		public void SetReflectionMatrix()
		{
			this.{23239}.SetValue(Engine.GS.Camera.ViewMatrix * Engine.GS.Camera.ProjMatrix);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x000C80E9 File Offset: 0x000C62E9
		public void BeginRenderToReflectionMap()
		{
			this.OperationCommonSetCameraState();
			this.{23254}.SetValue(true);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x000C80FD File Offset: 0x000C62FD
		public void SetReflectionBasis(Matrix {23182})
		{
			this.{23255}.SetValue({23182} * Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(Global.Camera.CurrentFov), Global.Game.GetAspectRatio, Renderer.CameraNearPlane, Renderer.CameraFarPlane));
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x000C8138 File Offset: 0x000C6338
		public void EndRenderToReflectionMap()
		{
			this.OperationCommonSetCameraState();
			this.{23254}.SetValue(false);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x000C814C File Offset: 0x000C634C
		internal void Setparaboloid(RenderTarget {23183}, RenderTarget {23184}, RenderTarget {23185})
		{
			this.{23296}.SetValue({23183}.Resource);
			this.{23298}.SetValue({23184}.Resource);
			this.{23297}.SetValue({23185}.Resource);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000C8181 File Offset: 0x000C6381
		public void SetOceanEffectMap(RenderTarget {23186}, RenderTarget {23187})
		{
			if ({23186} == null)
			{
				this.{23284}.SetValue(null);
				return;
			}
			this.{23284}.SetValue({23186}.Resource);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x000C81A4 File Offset: 0x000C63A4
		public void SetShipDestrDecal(Texture2D {23188} = null)
		{
			this.{23299}.SetValue({23188} ?? this.{23313});
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x000C81BC File Offset: 0x000C63BC
		private void {23189}(Texture2D {23190}, Texture2D {23191}, Texture2D {23192}, float {23193})
		{
			if ({23191} != null && !{23191}.IsDisposed)
			{
				this.{23291}.SetValue({23191});
			}
			if ({23192} != null && !{23192}.IsDisposed)
			{
				this.{23292}.SetValue({23192});
			}
			this.{23300}.SetValue(this.{23310} = new Vector2(({23191} != null) ? {23193} : 0f, ({23192} != null) ? {23193} : 0f));
			if ({23190} != null)
			{
				if (this.{23311} != {23190})
				{
					this.{23293}.SetValue({23190});
					this.{23311} = {23190};
					return;
				}
			}
			else
			{
				Texture2D sailDestructEffect = Materials.GetSailDestructEffect(255);
				if (this.{23311} != sailDestructEffect)
				{
					this.{23293}.SetValue(sailDestructEffect);
					this.{23311} = sailDestructEffect;
				}
			}
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x000C826F File Offset: 0x000C646F
		public void SetOrResetTextureReplaceDesign(int? {23194})
		{
			this.{23330} = {23194};
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x000C8278 File Offset: 0x000C6478
		public void SetSubstituteTexture(Texture2D {23195})
		{
			this.{23329} = {23195};
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x000C8281 File Offset: 0x000C6481
		public Texture2D CurrentReplaceMaterialTextre
		{
			get
			{
				return this.{23329};
			}
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x000C828C File Offset: 0x000C648C
		void ISceneObject3DParent.{23196}(ref Matrix {23197}, ModelRenderer {23198}, ModelTransformedScene {23199})
		{
			this.{23262}.SetValue({23197});
			Matrix value;
			Matrix.Invert(ref {23197}, out value);
			Matrix.Transpose(ref value, out value);
			this.{23263}.SetValue(value);
			if (!Renderer.ReflectionsAreBeingDrawn)
			{
				Global.Render.Pointlights.SetForRenderTheObject(this.{23253}, this.{23252}, {23199}, ref {23197});
			}
			if ({23198} != null && {23198}.LocalRenderQuery != null)
			{
				SailLocalRenderQuery sailLocalRenderQuery = {23198}.LocalRenderQuery as SailLocalRenderQuery;
				if (sailLocalRenderQuery != null)
				{
					this.{23189}(sailLocalRenderQuery.DestructnessTexture, sailLocalRenderQuery.Decal1ToSet, sailLocalRenderQuery.Decal2ToSet, sailLocalRenderQuery.DecalsTransparancy);
					if (sailLocalRenderQuery.CustomizedTexture != null)
					{
						this.{23329} = sailLocalRenderQuery.CustomizedTexture;
					}
					this.{23275}.SetValue(new Vector4(sailLocalRenderQuery.RollData.Normal, sailLocalRenderQuery.RollValue));
					this.{23276}.SetValue(new Vector4(sailLocalRenderQuery.RollData.RollPosition, sailLocalRenderQuery.RollData.CurvatureRoll));
					this.{23277}.SetValue(sailLocalRenderQuery.SailWindDirection);
					this.{23334} = true;
					return;
				}
				Texture2D texture2D = {23198}.LocalRenderQuery as Texture2D;
				if (texture2D != null)
				{
					this.{23329} = texture2D;
					this.{23335} = true;
				}
			}
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x000C83C0 File Offset: 0x000C65C0
		bool ISceneObject3DParent.{23200}(ModelPartShadercall {23201})
		{
			this.{23309}.X = {23201}.Material.Properties.SpecularIntensivity;
			this.{23309}.Y = {23201}.Material.Properties.SpecularPower;
			this.{23309}.Z = {23201}.Material.Properties.Reflectivity;
			this.{23309}.W = ({23201}.Material.Properties.FlowAnimated ? ((float)(-(float)Global.Game.GameTotalTimeSec * 0.02 % 1.0)) : 0f);
			this.{23283}.SetValue(this.{23309});
			if (this.{23329} == null)
			{
				this.{23278}.SetValue({23201}.Material.Terrain != null);
				if ({23201}.Material.Terrain != null)
				{
					if (this.{23331} == 25)
					{
						return false;
					}
					TerrainMaterialDescription terrain = {23201}.Material.Terrain;
					if (this.{23331} != -1)
					{
						terrain = Materials.TexturesDatabase[this.{23331}].Terrain;
					}
					this.{23279}.SetValue(terrain.ShadingMode == TerrainShadingMode.TripleTexture);
					this.{23291}.SetValue(terrain.TextureSlot1.Tex);
					this.{23290}.SetValue(terrain.TextureSlot2.Tex);
					this.{23292}.SetValue((terrain.TextureSlot3 == null) ? null : terrain.TextureSlot3.Tex);
				}
				else if ({23201}.Material.Albedo == null)
				{
					this.{23290}.SetValue(null);
				}
				else if (this.{23330} != null)
				{
					Texture2D texture2D = LocalContent.ShipFullDesignReplace({23201}.Material.Albedo.AssetName, this.{23330}.Value, {23201});
					if (texture2D == null)
					{
						return false;
					}
					this.{23290}.SetValue(texture2D ?? {23201}.Material.Albedo.Tex);
				}
				else
				{
					this.{23290}.SetValue({23201}.Material.Albedo.Tex);
				}
			}
			else
			{
				this.{23278}.SetValue(false);
				this.{23290}.SetValue(this.{23329});
			}
			bool flag = !this.{23332} && {23201}.Material.Properties.IsClothAnimated;
			this.{23325} = (flag ? this.{23327} : this.{23326});
			if (flag)
			{
				if (!this.{23333})
				{
					BoundingBox localSpaceBoundingBox = {23201}.Parts.Array[0].LocalSpaceBoundingBox;
					this.{23273}.SetValue(localSpaceBoundingBox.Min);
					this.{23274}.SetValue(localSpaceBoundingBox.Max);
				}
				Texture2D texture2D2;
				if (this.{23329} != null)
				{
					texture2D2 = null;
				}
				else
				{
					VirtualTexture materialTx = {23201}.Material.MaterialTx;
					texture2D2 = ((materialTx != null) ? materialTx.Tex : null);
				}
				Texture2D texture2D3 = texture2D2;
				this.{23294}.SetValue(texture2D3 ?? this.{23312});
			}
			else
			{
				Texture2D texture2D4;
				if ({23201}.Material.Terrain == null)
				{
					if (this.{23329} != null)
					{
						texture2D4 = null;
					}
					else
					{
						VirtualTexture materialTx2 = {23201}.Material.MaterialTx;
						texture2D4 = ((materialTx2 != null) ? materialTx2.Tex : null);
					}
				}
				else
				{
					VirtualTexture material = {23201}.Material.Terrain.Material;
					texture2D4 = ((material != null) ? material.Tex : null);
				}
				Texture2D texture2D5 = texture2D4;
				this.{23294}.SetValue(texture2D5 ?? this.{23312});
			}
			return true;
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x000C870A File Offset: 0x000C690A
		void ISceneObject3DParent.{23202}(ModelGeometryType {23203})
		{
			if ({23203} == ModelGeometryType.VertexPositionColor)
			{
				throw new NotSupportedException();
			}
			this.{23325}.Apply();
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x000C8720 File Offset: 0x000C6920
		void ISceneObject3DParent.{23204}(AnimationUnit {23205})
		{
			this.{23232}.SetValue({23205}.GetSkinTransforms());
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x000C8734 File Offset: 0x000C6934
		private void {23206}()
		{
			StackForm stackForm = new StackForm(new Vector2(1000f, 50f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			EffectParameter settings = Global.Render.GlobalIlluminationMapBuilder.Settings;
			EffectParameter settingsW = Global.Render.GlobalIlluminationMapBuilder.SettingsW;
			EffectParameter settingsRadius = Global.Render.GlobalIlluminationMapBuilder.SettingsRadius;
			EffectParameter settingsBlur = Global.Render.GlobalIlluminationMapBuilder.SettingsBlur;
			Rectangle rect_gui_progressbar_dark_basic_164x = AtlasGameGui.rect_gui_progressbar_dark_basic_164x24;
			Rectangle rect_gui_progressbar_dark_active_164x = AtlasGameGui.rect_gui_progressbar_dark_active_164x24;
			Rectangle rect_gui_progressbar_dark_cursor_6x = AtlasGameGui.rect_gui_progressbar_dark_cursor_6x24;
			Form form = new Form(new Marker(0f, 0f, 500f, 192f), AtlasGameGui.rect_asset_whitepixel_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Black * 0.3f
			};
			Label {13204} = new Label(new Vector2(3f, 3f), Fonts.Arial_10, Color.Yellow, "SSDO (1, 2, 3) ", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChild({13204});
			stackForm.AddItem(new UiControl[]
			{
				form
			});
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x000C8824 File Offset: 0x000C6A24
		private void {23207}()
		{
			StackForm stackForm = new StackForm(new Vector2(700f, 50f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			SceneColorSet styleSet = SceneColorSet.Normal;
			stackForm.AddItem(new UiControl[]
			{
				CommonWorldShader.ColorEditor("Fog: Day", () => styleSet.FogDayColor, delegate(Vector3 {23340})
				{
					styleSet.FogDayColor = {23340};
				}, 1f)
			});
			stackForm.AddItem(new UiControl[]
			{
				CommonWorldShader.ColorEditor("Fog: Day Up", () => styleSet.FogDayUpColor, delegate(Vector3 {23341})
				{
					styleSet.FogDayUpColor = {23341};
				}, 1f)
			});
			stackForm.AddItem(new UiControl[]
			{
				CommonWorldShader.ColorEditor("Fog: Night", () => styleSet.FogNightColor, delegate(Vector3 {23342})
				{
					styleSet.FogNightColor = {23342};
				}, 1f)
			});
			stackForm.AddItem(new UiControl[]
			{
				CommonWorldShader.ColorEditor("Fog: Storm", () => styleSet.FogStormColor, delegate(Vector3 {23343})
				{
					styleSet.FogStormColor = {23343};
				}, 1f)
			});
			stackForm.AddItem(new UiControl[]
			{
				CommonWorldShader.ColorEditor("Fog: Sunrise", () => styleSet.FogSunriseColor, delegate(Vector3 {23344})
				{
					styleSet.FogSunriseColor = {23344};
				}, 1f)
			});
			stackForm.AddItem(new UiControl[]
			{
				CommonWorldShader.ColorEditor("Sun diffuse", () => styleSet.SunDiffuseColorDefault, delegate(Vector3 {23345})
				{
					styleSet.SunDiffuseColorDefault = {23345};
				}, 1f)
			});
			stackForm.AddItem(new UiControl[]
			{
				CommonWorldShader.ColorEditor("Moon diffuse", () => styleSet.MoonDiffuseColorDefault, delegate(Vector3 {23346})
				{
					styleSet.MoonDiffuseColorDefault = {23346};
				}, 1f)
			});
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x000C89D4 File Offset: 0x000C6BD4
		private void {23208}()
		{
			StackForm stackForm = new StackForm(new Vector2(50f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			new StackForm(new Vector2(500f, 50f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				this.{23214}(base.GetEffectBase.Parameters["newRefractionSettings2"], 3f, 0f)
			});
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x000C8A40 File Offset: 0x000C6C40
		private void {23209}()
		{
			StackForm stackForm = new StackForm(new Vector2(1000f, 50f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			EffectParameter property = base.GetProperty("waves");
			for (int i = 0; i < 7; i++)
			{
				Rectangle rect_gui_progressbar_dark_basic_164x = AtlasGameGui.rect_gui_progressbar_dark_basic_164x24;
				Rectangle rect_gui_progressbar_dark_active_164x = AtlasGameGui.rect_gui_progressbar_dark_active_164x24;
				Rectangle rect_gui_progressbar_dark_cursor_6x = AtlasGameGui.rect_gui_progressbar_dark_cursor_6x24;
				EffectParameter effectParameter = property.Elements[i];
				EffectParameter waveDir = effectParameter.StructureMembers["direction"];
				EffectParameter waveHeight = effectParameter.StructureMembers["height"];
				EffectParameter waveLength = effectParameter.StructureMembers["length"];
				EffectParameter waveSpeed = effectParameter.StructureMembers["speed"];
				OceanWaveManager.Wave cpuInst = OceanWaveManager.waves[i];
				Form form = new Form(new Marker(0f, 0f, 500f, 120f), AtlasGameGui.rect_asset_whitepixel_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					BasicColor = Color.Black * 0.3f
				};
				Label {13204} = new Label(new Vector2(3f, 3f), Fonts.Arial_10, Color.Yellow, "Wave " + i.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form.AddChild({13204});
				Form form2 = form;
				ProgressSelectBar progressSelectBar = new ProgressSelectBar(new Vector2(336f, 0f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Vector2 valueVector = waveDir.GetValueVector2();
				Vector2 zero = Vector2.Zero;
				form2.AddChild(progressSelectBar.ExpansionSetVals(Geometry.GetRotate(valueVector, zero) + 3.1415927f, 6.2831855f).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23347})
				{
					waveDir.SetValue(cpuInst.direction = Geometry.SubstructRotate({23347}.NewValue * 6.2831855f - 3.1415927f, 1f));
				}));
				form.AddChild(new ProgressSelectBar(new Vector2(336f, 24f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals(waveHeight.GetValueSingle(), 10f).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23348})
				{
					waveHeight.SetValue(cpuInst.height = {23348}.NewValue * 10f);
				}));
				form.AddChild(new ProgressSelectBar(new Vector2(336f, 48f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals(waveLength.GetValueSingle(), 1f).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23349})
				{
					waveLength.SetValue(cpuInst.length = {23349}.NewValue);
				}));
				form.AddChild(new ProgressSelectBar(new Vector2(336f, 72f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals(waveSpeed.GetValueSingle() * waveLength.GetValueSingle(), 2f).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23350})
				{
					waveSpeed.SetValue(cpuInst.speed = {23350}.NewValue * 2f / waveLength.GetValueSingle());
				}));
				stackForm.AddItem(new UiControl[]
				{
					form
				});
			}
			Button button = new Button(Vector2.Zero, AtlasGameGui.rect_gui_button_newcyan162x50, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button.SetText("EXPORT", Fonts.Philosopher_14, Color.White, false);
			stackForm.AddItem(new UiControl[]
			{
				button
			});
			button.EvClick += delegate(ClickUiEventArgs {23339})
			{
				using (TextWriter textWriter = File.CreateText("output.txt"))
				{
					textWriter.Write("//Autogenerated " + DateTime.Now.ToString());
					for (int j = 0; j < 7; j++)
					{
						OceanWaveManager.Wave wave = OceanWaveManager.waves[j];
						textWriter.WriteLine(string.Concat(new string[]
						{
							"{ normalize(float2(",
							wave.direction.X.ToString().Replace(',', '.'),
							", ",
							wave.direction.Y.ToString().Replace(',', '.'),
							")), ",
							wave.length.ToString().Replace(',', '.'),
							", ",
							wave.height.ToString().Replace(',', '.'),
							", ",
							wave.speed.ToString().Replace(',', '.'),
							"}, "
						}));
					}
				}
				Engine.ShowTextFile("output.txt");
			};
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x000C8D54 File Offset: 0x000C6F54
		private static Form ColorEditor(string {23210}, Func<Vector3> {23211}, Action<Vector3> {23212}, float {23213} = 1f)
		{
			Rectangle rect_gui_progressbar_dark_basic_164x = AtlasGameGui.rect_gui_progressbar_dark_basic_164x24;
			Rectangle rect_gui_progressbar_dark_active_164x = AtlasGameGui.rect_gui_progressbar_dark_active_164x24;
			Rectangle rect_gui_progressbar_dark_cursor_6x = AtlasGameGui.rect_gui_progressbar_dark_cursor_6x24;
			Form form = new Form(new Marker(0f, 0f, 500f, 72f), AtlasGameGui.rect_asset_whitepixel_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Black * 0.3f
			};
			Label s8 = new Label(new Vector2(3f, 3f), Fonts.Arial_10, Color.Yellow, {23210}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChild(s8);
			form.AddChild(new UiControl[]
			{
				new ProgressSelectBar(new Vector2(336f, 0f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals({23211}().X, {23213}).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23351})
				{
					{23212}(CommonWorldShader.change(CommonWorldShader.setLabel(s8, {23211}()), 0, {23351}.NewValue * {23213}));
				}),
				new ProgressSelectBar(new Vector2(336f, 24f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals({23211}().Y, {23213}).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23352})
				{
					{23212}(CommonWorldShader.change(CommonWorldShader.setLabel(s8, {23211}()), 1, {23352}.NewValue * {23213}));
				}),
				new ProgressSelectBar(new Vector2(336f, 48f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals({23211}().Z, {23213}).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23353})
				{
					{23212}(CommonWorldShader.change(CommonWorldShader.setLabel(s8, {23211}()), 2, {23353}.NewValue * {23213}));
				})
			});
			return form;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x000C8EE8 File Offset: 0x000C70E8
		private Form {23214}(EffectParameter {23215}, float {23216}, float {23217} = 0f)
		{
			Rectangle rect_gui_progressbar_dark_basic_164x = AtlasGameGui.rect_gui_progressbar_dark_basic_164x24;
			Rectangle rect_gui_progressbar_dark_active_164x = AtlasGameGui.rect_gui_progressbar_dark_active_164x24;
			Rectangle rect_gui_progressbar_dark_cursor_6x = AtlasGameGui.rect_gui_progressbar_dark_cursor_6x24;
			if ({23215}.ParameterClass == EffectParameterClass.Scalar && {23215}.ParameterType == EffectParameterType.Single)
			{
				Form form = new Form(new Marker(0f, 0f, 500f, 24f), AtlasGameGui.rect_asset_whitepixel_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form.BasicColor = Color.Black * 0.3f;
				form.AddChild(new Label(new Vector2(3f, 3f), Fonts.Arial_10, Color.Yellow, {23215}.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				Label value = new Label(new Vector2(250f, 3f), Fonts.Arial_10, Color.Cyan, Math.Round((double){23215}.GetValueSingle(), 5).ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form.AddChild(value);
				ProgressSelectBar {13204} = new ProgressSelectBar(new Vector2(336f, 0f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals({23215}.GetValueSingle(), {23216}).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23354})
				{
					{23215}.SetValue({23354}.NewValue * ({23216} - {23217}) + {23217});
					value.Text = Math.Round((double){23215}.GetValueSingle(), 5).ToString();
				});
				form.AddChild({13204});
				return form;
			}
			if ({23215}.ParameterClass == EffectParameterClass.Vector && {23215}.ColumnCount == 2)
			{
				Form form2 = new Form(new Marker(0f, 0f, 500f, 48f), AtlasGameGui.rect_asset_whitepixel_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form2.BasicColor = Color.Black * 0.3f;
				form2.AddChild(new Label(new Vector2(3f, 3f), Fonts.Arial_10, Color.Yellow, {23215}.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				Label value = new Label(new Vector2(250f, 3f), Fonts.Arial_10, Color.Cyan, Math.Round((double){23215}.GetValueVector2().X, 4).ToString() + ", " + Math.Round((double){23215}.GetValueVector2().Y, 4).ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form2.AddChild(value);
				ProgressSelectBar {13204}2 = new ProgressSelectBar(new Vector2(336f, 0f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals({23215}.GetValueVector2().X, {23216}).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23355})
				{
					Vector2 valueVector = {23215}.GetValueVector2();
					{23215}.SetValue(new Vector2({23355}.NewValue * {23216}, valueVector.Y));
					value.Text = Math.Round((double){23215}.GetValueVector2().X, 4).ToString() + ", " + Math.Round((double){23215}.GetValueVector2().Y, 4).ToString();
				});
				form2.AddChild({13204}2);
				{13204}2 = new ProgressSelectBar(new Vector2(336f, 24f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals({23215}.GetValueVector2().X, {23216}).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23356})
				{
					Vector2 valueVector = {23215}.GetValueVector2();
					{23215}.SetValue(new Vector2(valueVector.X, {23356}.NewValue * {23216}));
					value.Text = Math.Round((double){23215}.GetValueVector2().X, 4).ToString() + ", " + Math.Round((double){23215}.GetValueVector2().Y, 4).ToString();
				});
				form2.AddChild({13204}2);
				return form2;
			}
			if ({23215}.ParameterClass == EffectParameterClass.Vector && {23215}.ColumnCount == 3)
			{
				Form form3 = new Form(new Marker(0f, 0f, 500f, 72f), AtlasGameGui.rect_asset_whitepixel_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					BasicColor = Color.Black * 0.3f
				};
				form3.AddChild(new Label(new Vector2(3f, 3f), Fonts.Arial_10, Color.Yellow, {23215}.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				Label value = new Label(new Vector2(250f, 3f), Fonts.Arial_10, Color.Cyan, string.Concat(new string[]
				{
					Math.Round((double){23215}.GetValueVector3().X, 3).ToString(),
					", ",
					Math.Round((double){23215}.GetValueVector3().Y, 3).ToString(),
					", ",
					Math.Round((double){23215}.GetValueVector3().Z, 3).ToString()
				}), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form3.AddChild(value);
				ProgressSelectBar {13204}3 = new ProgressSelectBar(new Vector2(336f, 0f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals({23215}.GetValueVector3().X, {23216}).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23357})
				{
					Vector3 valueVector = {23215}.GetValueVector3();
					{23215}.SetValue(new Vector3({23357}.NewValue * {23216}, valueVector.Y, valueVector.Z));
					value.Text = string.Concat(new string[]
					{
						Math.Round((double){23215}.GetValueVector3().X, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector3().Y, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector3().Z, 3).ToString()
					});
				});
				form3.AddChild({13204}3);
				{13204}3 = new ProgressSelectBar(new Vector2(336f, 24f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals({23215}.GetValueVector3().Y, {23216}).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23358})
				{
					Vector3 valueVector = {23215}.GetValueVector3();
					{23215}.SetValue(new Vector3(valueVector.X, {23358}.NewValue * {23216}, valueVector.Z));
					value.Text = string.Concat(new string[]
					{
						Math.Round((double){23215}.GetValueVector3().X, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector3().Y, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector3().Z, 3).ToString()
					});
				});
				form3.AddChild({13204}3);
				{13204}3 = new ProgressSelectBar(new Vector2(336f, 48f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals({23215}.GetValueVector3().Z, {23216}).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23359})
				{
					Vector3 valueVector = {23215}.GetValueVector3();
					{23215}.SetValue(new Vector3(valueVector.X, valueVector.Y, {23359}.NewValue * {23216}));
					value.Text = string.Concat(new string[]
					{
						Math.Round((double){23215}.GetValueVector3().X, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector3().Y, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector3().Z, 3).ToString()
					});
				});
				form3.AddChild({13204}3);
				return form3;
			}
			if ({23215}.ParameterClass == EffectParameterClass.Vector && {23215}.ColumnCount == 4)
			{
				Form form4 = new Form(new Marker(0f, 0f, 500f, 96f), AtlasGameGui.rect_asset_whitepixel_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					BasicColor = Color.Black * 0.3f
				};
				form4.AddChild(new Label(new Vector2(3f, 3f), Fonts.Arial_10, Color.Yellow, {23215}.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				Label value = new Label(new Vector2(250f, 3f), Fonts.Arial_10, Color.Cyan, string.Concat(new string[]
				{
					Math.Round((double){23215}.GetValueVector4().X, 3).ToString(),
					", ",
					Math.Round((double){23215}.GetValueVector4().Y, 3).ToString(),
					", ",
					Math.Round((double){23215}.GetValueVector4().Z, 3).ToString(),
					", ",
					Math.Round((double){23215}.GetValueVector4().W, 3).ToString()
				}), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form4.AddChild(value);
				ProgressSelectBar {13204}4 = new ProgressSelectBar(new Vector2(336f, 0f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals({23215}.GetValueVector4().X, {23216}).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23360})
				{
					Vector4 valueVector = {23215}.GetValueVector4();
					{23215}.SetValue(new Vector4({23360}.NewValue * {23216}, valueVector.Y, valueVector.Z, valueVector.W));
					value.Text = string.Concat(new string[]
					{
						Math.Round((double){23215}.GetValueVector4().X, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector4().Y, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector4().Z, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector4().W, 3).ToString()
					});
				});
				form4.AddChild({13204}4);
				{13204}4 = new ProgressSelectBar(new Vector2(336f, 24f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals({23215}.GetValueVector4().Y, {23216}).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23361})
				{
					Vector4 valueVector = {23215}.GetValueVector4();
					{23215}.SetValue(new Vector4(valueVector.X, {23361}.NewValue * {23216}, valueVector.Z, valueVector.W));
					value.Text = string.Concat(new string[]
					{
						Math.Round((double){23215}.GetValueVector4().X, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector4().Y, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector4().Z, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector4().W, 3).ToString()
					});
				});
				form4.AddChild({13204}4);
				{13204}4 = new ProgressSelectBar(new Vector2(336f, 48f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals({23215}.GetValueVector4().Z, {23216}).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23362})
				{
					Vector4 valueVector = {23215}.GetValueVector4();
					{23215}.SetValue(new Vector4(valueVector.X, valueVector.Y, {23362}.NewValue * {23216}, valueVector.W));
					value.Text = string.Concat(new string[]
					{
						Math.Round((double){23215}.GetValueVector4().X, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector4().Y, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector4().Z, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector4().W, 3).ToString()
					});
				});
				form4.AddChild({13204}4);
				{13204}4 = new ProgressSelectBar(new Vector2(336f, 72f), rect_gui_progressbar_dark_active_164x, rect_gui_progressbar_dark_basic_164x, rect_gui_progressbar_dark_cursor_6x, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals({23215}.GetValueVector4().W, {23216}).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {23363})
				{
					Vector4 valueVector = {23215}.GetValueVector4();
					{23215}.SetValue(new Vector4(valueVector.X, valueVector.Y, valueVector.Z, {23363}.NewValue * {23216}));
					value.Text = string.Concat(new string[]
					{
						Math.Round((double){23215}.GetValueVector4().X, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector4().Y, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector4().Z, 3).ToString(),
						", ",
						Math.Round((double){23215}.GetValueVector4().W, 3).ToString()
					});
				});
				form4.AddChild({13204}4);
				return form4;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x000C97FC File Offset: 0x000C79FC
		private float {23218}(Label {23219}, float {23220})
		{
			{23219}.Text = {23220}.ToString();
			return {23220};
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x000C980C File Offset: 0x000C7A0C
		private static Vector3 setLabel(Label {23221}, Vector3 {23222})
		{
			{23221}.Text = {23222}.ToString();
			return {23222};
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x000C9822 File Offset: 0x000C7A22
		private static Vector2 setLabel(Label {23223}, Vector2 {23224})
		{
			{23223}.Text = {23224}.ToString();
			return {23224};
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x000C9838 File Offset: 0x000C7A38
		private static Vector3 change(Vector3 {23225}, int {23226}, float {23227})
		{
			if ({23226} == 0)
			{
				return new Vector3({23227}, {23225}.Y, {23225}.Z);
			}
			if ({23226} == 1)
			{
				return new Vector3({23225}.X, {23227}, {23225}.Z);
			}
			return new Vector3({23225}.X, {23225}.Y, {23227});
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x000C9884 File Offset: 0x000C7A84
		private Vector2 {23228}(Vector2 {23229}, int {23230}, float {23231})
		{
			if ({23230} == 0)
			{
				return new Vector2({23231}, {23229}.Y);
			}
			return new Vector2({23229}.X, {23231});
		}

		// Token: 0x0400155A RID: 5466
		public static readonly bool GraphicsEngineerMode;

		// Token: 0x0400155B RID: 5467
		public static readonly bool StyleditorMode;

		// Token: 0x0400155C RID: 5468
		public static readonly bool OceanWawesEditor;

		// Token: 0x0400155D RID: 5469
		public static readonly bool SSDOParamsEditor;

		// Token: 0x0400155E RID: 5470
		public const bool DebugTexture = false;

		// Token: 0x0400155F RID: 5471
		public static bool DebugTextureShow;

		// Token: 0x04001560 RID: 5472
		public EffectParameter TXAAKernel;

		// Token: 0x04001561 RID: 5473
		public EffectParameter TXAAKernelNrm;

		// Token: 0x04001562 RID: 5474
		public EffectParameter ObjectCommonTransparancy;

		// Token: 0x04001563 RID: 5475
		private EffectParameter {23232};

		// Token: 0x04001564 RID: 5476
		private EffectParameter {23233};

		// Token: 0x04001565 RID: 5477
		private EffectParameter {23234};

		// Token: 0x04001566 RID: 5478
		private EffectParameter {23235};

		// Token: 0x04001567 RID: 5479
		private EffectParameter {23236};

		// Token: 0x04001568 RID: 5480
		private EffectParameter {23237};

		// Token: 0x04001569 RID: 5481
		private EffectParameter {23238};

		// Token: 0x0400156A RID: 5482
		private EffectParameter {23239};

		// Token: 0x0400156B RID: 5483
		private EffectParameter {23240};

		// Token: 0x0400156C RID: 5484
		private EffectParameter {23241};

		// Token: 0x0400156D RID: 5485
		private EffectParameter {23242};

		// Token: 0x0400156E RID: 5486
		private EffectParameter {23243};

		// Token: 0x0400156F RID: 5487
		private EffectParameter {23244};

		// Token: 0x04001570 RID: 5488
		private EffectParameter {23245};

		// Token: 0x04001571 RID: 5489
		private EffectParameter {23246};

		// Token: 0x04001572 RID: 5490
		private EffectParameter {23247};

		// Token: 0x04001573 RID: 5491
		private EffectParameter {23248};

		// Token: 0x04001574 RID: 5492
		private EffectParameter {23249};

		// Token: 0x04001575 RID: 5493
		private EffectParameter {23250};

		// Token: 0x04001576 RID: 5494
		private EffectParameter {23251};

		// Token: 0x04001577 RID: 5495
		private EffectParameter {23252};

		// Token: 0x04001578 RID: 5496
		private EffectParameter {23253};

		// Token: 0x04001579 RID: 5497
		private EffectParameter {23254};

		// Token: 0x0400157A RID: 5498
		private EffectParameter {23255};

		// Token: 0x0400157B RID: 5499
		private EffectParameter {23256};

		// Token: 0x0400157C RID: 5500
		private EffectParameter {23257};

		// Token: 0x0400157D RID: 5501
		private EffectParameter {23258};

		// Token: 0x0400157E RID: 5502
		private EffectParameter {23259};

		// Token: 0x0400157F RID: 5503
		private EffectParameter {23260};

		// Token: 0x04001580 RID: 5504
		private EffectParameter {23261};

		// Token: 0x04001581 RID: 5505
		private EffectParameter {23262};

		// Token: 0x04001582 RID: 5506
		private EffectParameter {23263};

		// Token: 0x04001583 RID: 5507
		private EffectParameter {23264};

		// Token: 0x04001584 RID: 5508
		private EffectParameter {23265};

		// Token: 0x04001585 RID: 5509
		private EffectParameter {23266};

		// Token: 0x04001586 RID: 5510
		private EffectParameter {23267};

		// Token: 0x04001587 RID: 5511
		private EffectParameter {23268};

		// Token: 0x04001588 RID: 5512
		private EffectParameter {23269};

		// Token: 0x04001589 RID: 5513
		private EffectParameter {23270};

		// Token: 0x0400158A RID: 5514
		private EffectParameter {23271};

		// Token: 0x0400158B RID: 5515
		private EffectParameter {23272};

		// Token: 0x0400158C RID: 5516
		private EffectParameter {23273};

		// Token: 0x0400158D RID: 5517
		private EffectParameter {23274};

		// Token: 0x0400158E RID: 5518
		private EffectParameter {23275};

		// Token: 0x0400158F RID: 5519
		private EffectParameter {23276};

		// Token: 0x04001590 RID: 5520
		private EffectParameter {23277};

		// Token: 0x04001591 RID: 5521
		private EffectParameter {23278};

		// Token: 0x04001592 RID: 5522
		private EffectParameter {23279};

		// Token: 0x04001593 RID: 5523
		private EffectParameter {23280};

		// Token: 0x04001594 RID: 5524
		private EffectParameter {23281};

		// Token: 0x04001595 RID: 5525
		private EffectParameter {23282};

		// Token: 0x04001596 RID: 5526
		private EffectParameter {23283};

		// Token: 0x04001597 RID: 5527
		private EffectParameter {23284};

		// Token: 0x04001598 RID: 5528
		private EffectParameter {23285};

		// Token: 0x04001599 RID: 5529
		private EffectParameter {23286};

		// Token: 0x0400159A RID: 5530
		private EffectParameter {23287};

		// Token: 0x0400159B RID: 5531
		private EffectParameter {23288};

		// Token: 0x0400159C RID: 5532
		private EffectParameter {23289};

		// Token: 0x0400159D RID: 5533
		private EffectParameter {23290};

		// Token: 0x0400159E RID: 5534
		private EffectParameter {23291};

		// Token: 0x0400159F RID: 5535
		private EffectParameter {23292};

		// Token: 0x040015A0 RID: 5536
		private EffectParameter {23293};

		// Token: 0x040015A1 RID: 5537
		private EffectParameter {23294};

		// Token: 0x040015A2 RID: 5538
		private EffectParameter {23295};

		// Token: 0x040015A3 RID: 5539
		private EffectParameter {23296};

		// Token: 0x040015A4 RID: 5540
		private EffectParameter {23297};

		// Token: 0x040015A5 RID: 5541
		private EffectParameter {23298};

		// Token: 0x040015A6 RID: 5542
		private EffectParameter {23299};

		// Token: 0x040015A7 RID: 5543
		private EffectParameter {23300};

		// Token: 0x040015A8 RID: 5544
		private EffectParameter {23301};

		// Token: 0x040015A9 RID: 5545
		private EffectParameter {23302};

		// Token: 0x040015AA RID: 5546
		private EffectParameter {23303};

		// Token: 0x040015AB RID: 5547
		private EffectParameter {23304};

		// Token: 0x040015AC RID: 5548
		private EffectParameter {23305};

		// Token: 0x040015AD RID: 5549
		private EffectParameter {23306};

		// Token: 0x040015AE RID: 5550
		private EffectParameter {23307};

		// Token: 0x040015AF RID: 5551
		private EffectParameter {23308};

		// Token: 0x040015B0 RID: 5552
		private Vector4 {23309};

		// Token: 0x040015B1 RID: 5553
		private Vector2 {23310};

		// Token: 0x040015B2 RID: 5554
		private Texture2D {23311};

		// Token: 0x040015B3 RID: 5555
		private Texture2D {23312};

		// Token: 0x040015B4 RID: 5556
		private Texture2D {23313};

		// Token: 0x040015B5 RID: 5557
		private EffectPass {23314};

		// Token: 0x040015B6 RID: 5558
		private EffectPass {23315};

		// Token: 0x040015B7 RID: 5559
		private EffectPass {23316};

		// Token: 0x040015B8 RID: 5560
		private EffectPass {23317};

		// Token: 0x040015B9 RID: 5561
		private EffectPass {23318};

		// Token: 0x040015BA RID: 5562
		private EffectPass {23319};

		// Token: 0x040015BB RID: 5563
		private EffectTechnique {23320};

		// Token: 0x040015BC RID: 5564
		private EffectTechnique {23321};

		// Token: 0x040015BD RID: 5565
		private float {23322};

		// Token: 0x040015BE RID: 5566
		private Vector2 {23323};

		// Token: 0x040015BF RID: 5567
		private float {23324};

		// Token: 0x040015C0 RID: 5568
		private EffectPass {23325};

		// Token: 0x040015C1 RID: 5569
		private EffectPass {23326};

		// Token: 0x040015C2 RID: 5570
		private EffectPass {23327};

		// Token: 0x040015C3 RID: 5571
		private WorldMapInfo {23328};

		// Token: 0x040015C4 RID: 5572
		private Texture2D {23329};

		// Token: 0x040015C5 RID: 5573
		private int? {23330};

		// Token: 0x040015C6 RID: 5574
		private int {23331};

		// Token: 0x040015C7 RID: 5575
		private bool {23332};

		// Token: 0x040015C8 RID: 5576
		private bool {23333};

		// Token: 0x040015C9 RID: 5577
		private bool {23334};

		// Token: 0x040015CA RID: 5578
		private bool {23335};

		// Token: 0x040015CB RID: 5579
		public Vector2 FogLerpFactorLastValue;

		// Token: 0x040015CC RID: 5580
		private float {23336};

		// Token: 0x0200043A RID: 1082
		private enum TargetObject
		{
			// Token: 0x040015CE RID: 5582
			Object,
			// Token: 0x040015CF RID: 5583
			Sail,
			// Token: 0x040015D0 RID: 5584
			Green,
			// Token: 0x040015D1 RID: 5585
			Environment,
			// Token: 0x040015D2 RID: 5586
			Skinning,
			// Token: 0x040015D3 RID: 5587
			Instancing,
			// Token: 0x040015D4 RID: 5588
			InstancingGreen
		}
	}
}
