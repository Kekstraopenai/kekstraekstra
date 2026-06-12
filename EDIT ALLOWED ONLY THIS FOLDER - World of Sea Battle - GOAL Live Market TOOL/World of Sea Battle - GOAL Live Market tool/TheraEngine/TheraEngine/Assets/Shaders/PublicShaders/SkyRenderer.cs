using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Sky;
using TheraEngine.Components.Scene;
using TheraEngine.Core;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x02000180 RID: 384
	public class SkyRenderer
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0003158C File Offset: 0x0002F78C
		public SkyTextures CurrentSky
		{
			get
			{
				return this.currentSky;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00031594 File Offset: 0x0002F794
		public float DayOrNight
		{
			get
			{
				return this.{15599};
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0003159C File Offset: 0x0002F79C
		public Vector3 CurrentFogColor
		{
			get
			{
				return this.{15600};
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x000315A4 File Offset: 0x0002F7A4
		public Vector3 CurrentDiffuseColor
		{
			get
			{
				return this.{15601};
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x000315AC File Offset: 0x0002F7AC
		public Vector3 CurrentSpecularColor
		{
			get
			{
				return this.{15602};
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x000315B4 File Offset: 0x0002F7B4
		public float Sunrise
		{
			get
			{
				return this.{15603};
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x000315BC File Offset: 0x0002F7BC
		public float MorningSunrise(SceneManager {15543})
		{
			return this.{15603} * ({15543}.CycleLightDirection.X < 0f);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000315D8 File Offset: 0x0002F7D8
		public SkyRenderer(string {15544}, ContentManager {15545}, UWModel {15546}, UWModel {15547})
		{
			this.{15588} = {15545}.Load<Effect>("Shaders\\Sky\\SkyRenderer");
			this.{15589} = this.{15588}.Parameters["View"];
			this.{15590} = this.{15588}.Parameters["Projection"];
			this.{15591} = this.{15588}.Parameters["environment1"];
			this.{15592} = this.{15588}.Parameters["environment2"];
			this.{15594} = this.{15588}.Parameters["layerSettings"];
			this.{15593} = this.{15588}.Parameters["alphaValue"];
			this.{15595} = this.{15588}.Parameters["mixCloudLayerColor"];
			this.{15596} = this.{15588}.Parameters["looseDirection"];
			this.ep_colorMultiplier = this.{15588}.Parameters["colorMultiplier"];
			this.{15597} = new SkyBox({15547});
			this.{15598} = new SkyBox({15546});
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00031708 File Offset: 0x0002F908
		public void Update(ref FrameTime {15548}, float {15549}, SceneManager {15550})
		{
			if (this.currentSky == null)
			{
				return;
			}
			Vector3 cycleLightDirection = {15550}.CycleLightDirection;
			SceneColorSet sceneColorSet = this.currentSky.Style;
			this.CloudyLevelToStyleFog = ((this.AdditionalStyle == null) ? this.currentSky.StormDecalMultiplier : MathHelper.Lerp(this.currentSky.StormDecalMultiplier, this.AdditionalStyle.StormDecalMultiplier, this.AdditionalStyleWeight));
			float num = {15550}.CurrentCloudyLevel * this.CloudyLevelToStyleFog;
			if (this.AdditionalStyle != null && this.AdditionalStyleWeight > 0f)
			{
				sceneColorSet = SceneColorSet.Interpolate(sceneColorSet, this.AdditionalStyle.Style, this.AdditionalStyleWeight);
			}
			Vector3 vector = sceneColorSet.SunDiffuseColorDefault * (new Vector3(216f, 155f, 102f) / 255f);
			Vector3 vector2 = sceneColorSet.SunDiffuseColorDefault * (new Vector3(226f, 190f, 150f) / 255f);
			float num2 = -0.2f;
			if (cycleLightDirection.Y < num2)
			{
				this.{15599} = 0f;
				this.{15600} = sceneColorSet.FogNightColor;
				this.{15601} = sceneColorSet.MoonDiffuseColorDefault;
				this.{15602} = sceneColorSet.MoonSpecularColorDefault * 0.5f;
				this.{15603} = 0f;
			}
			else if (cycleLightDirection.Y < 0f)
			{
				float num3 = (cycleLightDirection.Y - num2) / (0f - num2);
				this.{15599} = num3 * 0.5f;
				this.{15600} = Vector3.Lerp(sceneColorSet.FogNightColor, sceneColorSet.FogSunriseColor * new Vector3(1.1f, 0.9f, 1f), num3);
				this.{15601} = Vector3.Lerp(sceneColorSet.MoonDiffuseColorDefault, vector, num3);
				this.{15602} = Vector3.Lerp(sceneColorSet.SunSpecularColorDefault, vector, num3);
				this.{15603} = num3;
			}
			else if (cycleLightDirection.Y < 0.35f)
			{
				float num4 = (cycleLightDirection.Y - 0f) / 0.35f;
				this.{15599} = 0.5f + num4 * 0.5f;
				this.{15600} = Vector3.Lerp(sceneColorSet.FogSunriseColor * new Vector3(1.1f, 0.9f, 1f), sceneColorSet.FogSunriseColor, num4);
				this.{15601} = Vector3.Lerp(vector, vector2, num4);
				this.{15602} = Vector3.Lerp(vector, vector2, num4);
				this.{15603} = 1f;
			}
			else if (cycleLightDirection.Y < 0.5f)
			{
				float num5 = (cycleLightDirection.Y - 0.35f) / 0.15f;
				this.{15599} = 1f;
				this.{15600} = Vector3.Lerp(sceneColorSet.FogSunriseColor, sceneColorSet.FogDayColor, num5);
				this.{15601} = Vector3.Lerp(vector2, sceneColorSet.SunDiffuseColorDefault, num5);
				this.{15602} = Vector3.Lerp(vector2, sceneColorSet.SunSpecularColorDefault, num5);
				this.{15603} = MathF.Sqrt(1f - num5);
			}
			else
			{
				float amount = Geometry.Saturate((cycleLightDirection.Y - 0.5f) / 0.19999999f);
				this.{15599} = 1f;
				this.{15600} = Vector3.Lerp(sceneColorSet.FogDayColor, sceneColorSet.FogDayUpColor, amount);
				this.{15601} = sceneColorSet.SunDiffuseColorDefault;
				this.{15602} = sceneColorSet.SunSpecularColorDefault;
				this.{15603} = 0f;
			}
			this.{15602} *= 1f - this.{15603} * 0.5f;
			float num6 = Geometry.Saturate((Math.Abs(cycleLightDirection.Y + 0.127f) - 0.03f) / 0.06f);
			this.{15604} = (1f - num) * num6;
			this.{15601} = Vector3.Lerp(this.{15601}, this.{15601} * new Vector3(0.6f, 0.7f, 0.75f), num) * num6 * 1.2f;
			this.{15602} *= (1f - num) * Geometry.Saturate(num6 * 2f - 1f);
			this.{15600} = Vector3.Lerp(this.{15600}, sceneColorSet.FogStormColor * (0.7f + 0.3f * this.{15599}), num);
			this.{15600} *= 0.9f;
			this.{15595}.SetValue(new Vector4(this.{15600}, 0.6f * Math.Max(0f, (num - 0.5f) / 0.5f)));
			this.CommonLightColor = new Vector3(15f, 20f, 35f) / 200f + this.CurrentFogColor * 0.6f + new Vector3(100f, 75f, 75f) * (1f - num6 + this.{15603} * 0.5f + (1f - this.{15599}) * 0.5f + 0.2f) * 0.25f / 255f;
			this.CommonLightColor *= 0.82f;
			this.CloudyLevelToStyleFog *= this.CloudyLevelToStyleFog * num;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00031C70 File Offset: 0x0002FE70
		public void Draw(SceneManager {15551}, bool {15552}, bool {15553}, SkyRenderer.StarSkyDrawMode {15554}, in Vector3 {15555}, ParticlesAndStaticMesh {15556} = null)
		{
			if (this.currentSky == null)
			{
				return;
			}
			bool flag = true;
			Vector3 cycleLightDirection = {15551}.CycleLightDirection;
			Texture2D night = this.currentSky.Night;
			Texture2D nightMask = this.currentSky.NightMask;
			Texture2D sunrise = this.currentSky.Sunrise1;
			Texture2D texture2D = (cycleLightDirection.X < 0f) ? this.currentSky.Sunrise2Rev : this.currentSky.Sunrise2;
			Texture2D day = this.currentSky.Day;
			Texture2D stormDecal = this.currentSky.StormDecal;
			Texture2D stars = this.currentSky.Stars;
			Texture2D startsTurbulence = this.currentSky.StartsTurbulence;
			float num = (this.AdditionalStyle == null) ? this.currentSky.StormDecalMinimum : MathHelper.Lerp(this.currentSky.StormDecalMinimum, this.AdditionalStyle.StormDecalMinimum, this.AdditionalStyleWeight);
			float num2 = (this.AdditionalStyle == null) ? this.currentSky.StormDecalMultiplier : MathHelper.Lerp(this.currentSky.StormDecalMultiplier, this.AdditionalStyle.StormDecalMultiplier, this.AdditionalStyleWeight);
			float num3 = num + (1f - num) * {15551}.CurrentCloudyLevel * num2;
			num3 = MathF.Pow(num3, 0.66f);
			if (this.AdditionalStyle != null && this.AdditionalStyleWeight > 0.5f)
			{
				night = this.AdditionalStyle.Night;
				nightMask = this.AdditionalStyle.NightMask;
				sunrise = this.AdditionalStyle.Sunrise1;
				texture2D = ((cycleLightDirection.X < 0f) ? this.AdditionalStyle.Sunrise2Rev : this.AdditionalStyle.Sunrise2);
				day = this.AdditionalStyle.Day;
			}
			this.{15589}.SetValue(Engine.GS.Camera.viewMatrix);
			this.{15590}.SetValue(Engine.GS.Camera.ProjMatrix);
			this.{15593}.SetValue((!{15553}) ? 1 : 0);
			float num4 = this.{15557}(cycleLightDirection);
			Engine.GS.SetColorBlendState(BlendState.Opaque);
			float num6;
			if (cycleLightDirection.Y < -0.3f)
			{
				float num5 = num4 - 0.05f;
				this.{15567}({15552}, night, false, 1f, num5, 1f);
				Engine.GS.SetColorBlendState(BlendState.Additive);
				this.{15574}({15552}, 1f, {15554} == SkyRenderer.StarSkyDrawMode.Blurry, startsTurbulence, stars, num5);
				Engine.GS.SetColorBlendState(BlendState.AlphaBlend);
				if ({15556} != null)
				{
					{15551}.Render({15556}, this.{15604});
				}
				if (flag && nightMask != null)
				{
					this.{15581}({15552}, nightMask, num5, 1f);
				}
				Engine.GS.SetColorBlendState(BlendState.Opaque);
				num6 = 0f;
			}
			else if (cycleLightDirection.Y < 0f)
			{
				this.{15559}({15552}, night, sunrise, -0.3f, 0f, cycleLightDirection.Y, num4);
				Engine.GS.SetColorBlendState(BlendState.Additive);
				float num7 = 1f - (cycleLightDirection.Y - -0.3f) / 0.3f;
				this.{15574}({15552}, num7, {15554} == SkyRenderer.StarSkyDrawMode.Blurry, startsTurbulence, stars, num4);
				Engine.GS.SetColorBlendState(BlendState.AlphaBlend);
				if ({15556} != null)
				{
					{15551}.Render({15556}, this.{15604});
				}
				if (flag && nightMask != null)
				{
					this.{15581}({15552}, nightMask, num4, MathF.Pow(num7, 3f));
				}
				Engine.GS.SetColorBlendState(BlendState.Opaque);
				num6 = 0.5f * ((cycleLightDirection.Y - -0.3f) / 0.3f);
			}
			else
			{
				if (cycleLightDirection.Y < 0.35f)
				{
					this.{15559}({15552}, sunrise, texture2D, 0f, 0.35f, cycleLightDirection.Y, num4);
					num6 = 0.5f + 0.5f * ((cycleLightDirection.Y - 0f) / 0.35f);
				}
				else if (cycleLightDirection.Y < 0.5f)
				{
					this.{15559}({15552}, texture2D, day, 0.35f, 0.5f, cycleLightDirection.Y, num4);
					num6 = 1f;
				}
				else
				{
					this.{15567}({15552}, day, false, 1f, num4, 1f);
					num6 = 1f;
				}
				Engine.GS.SetColorBlendState(BlendState.AlphaBlend);
				if ({15556} != null)
				{
					{15551}.Render({15556}, this.{15604});
				}
			}
			Engine.GS.SetColorBlendState(BlendState.AlphaBlend);
			if (num3 > 0f)
			{
				Engine.GS.SetColorBlendState(BlendState.AlphaBlend);
				this.{15596}.SetValue({15555});
				num4 += (float)(0.0015 * Engine.Game.GameTotalTimeSec % 1.0);
				if (this.AdditionalStyleWeight > 0f && this.AdditionalStyle != null && this.AdditionalStyle.StormDecal != stormDecal)
				{
					this.{15567}({15552}, stormDecal, true, num3 * (1f - this.AdditionalStyleWeight), num4, 0.5f + num6 * 0.5f);
					this.{15567}({15552}, this.AdditionalStyle.StormDecal, true, num3 * this.AdditionalStyleWeight, num4, 0.5f + num6 * 0.5f);
					return;
				}
				this.{15567}({15552}, stormDecal, true, num3, num4, 0.5f + num6 * 0.5f);
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x000321A4 File Offset: 0x000303A4
		private float {15557}(Vector3 {15558})
		{
			if ({15558}.X != 0f)
			{
				return -(float)(Math.Atan2((double){15558}.X, (double){15558}.Y) / 3.141592653589793) * 0.5f - 0.16f;
			}
			return -(float)(Math.Atan2((double){15558}.Z, (double){15558}.Y) / 3.141592653589793) * 0.5f + 0.08f;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00032218 File Offset: 0x00030418
		private void {15559}(bool {15560}, Texture2D {15561}, Texture2D {15562}, float {15563}, float {15564}, float {15565}, float {15566})
		{
			this.{15591}.SetValue({15561});
			this.{15592}.SetValue({15562});
			this.{15594}.SetValue(new Vector3(({15565} - {15563}) / ({15564} - {15563}), {15566}, 1f));
			this.{15588}.CurrentTechnique.Passes[4].Apply();
			this.{15586}({15560});
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00032284 File Offset: 0x00030484
		private void {15567}(bool {15568}, Texture2D {15569}, bool {15570}, float {15571}, float {15572}, float {15573} = 1f)
		{
			this.{15591}.SetValue({15569});
			this.{15592}.SetValue({15569});
			this.{15594}.SetValue(new Vector3({15571}, {15572}, {15573}));
			this.{15588}.CurrentTechnique.Passes[({15570} > false) ? 1 : 0].Apply();
			this.{15586}({15568});
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000322E4 File Offset: 0x000304E4
		private void {15574}(bool {15575}, float {15576}, bool {15577}, Texture2D {15578}, Texture2D {15579}, float {15580})
		{
			if (!{15575})
			{
				return;
			}
			this.{15594}.SetValue(new Vector3(1f, {15580}, {15576} * 0.7f));
			this.{15588}.Parameters["time"].SetValue((float)(0.00099 * Engine.Game.GameTotalTimeSec % 1.0));
			this.{15588}.Parameters["spe"].SetValue(new Vector2((float)Engine.Game.GameTotalTimeSec * 0.01f, (float)Math.Sin(Engine.Game.GameTotalTimeSec * 3.0) * 0.0035f * 2f));
			this.{15591}.SetValue({15578});
			this.{15592}.SetValue({15579});
			this.{15588}.CurrentTechnique.Passes[{15577} ? 3 : 2].Apply();
			this.{15586}({15575});
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000323E4 File Offset: 0x000305E4
		private void {15581}(bool {15582}, Texture2D {15583}, float {15584}, float {15585} = 1f)
		{
			this.{15591}.SetValue({15583});
			this.{15594}.SetValue(new Vector3({15585}, {15584}, 1f));
			this.{15588}.CurrentTechnique.Passes["pass5"].Apply();
			this.{15586}({15582});
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0003243B File Offset: 0x0003063B
		private void {15586}(bool {15587})
		{
			if ({15587})
			{
				this.{15598}.Render();
				return;
			}
			this.{15597}.Render();
		}

		// Token: 0x04000762 RID: 1890
		public const float x0 = -0.3f;

		// Token: 0x04000763 RID: 1891
		public const float x1 = 0f;

		// Token: 0x04000764 RID: 1892
		public const float x2 = 0.35f;

		// Token: 0x04000765 RID: 1893
		public const float x3 = 0.5f;

		// Token: 0x04000766 RID: 1894
		public Vector3 CommonLightColor;

		// Token: 0x04000767 RID: 1895
		public float CloudyLevelToStyleFog;

		// Token: 0x04000768 RID: 1896
		private Effect {15588};

		// Token: 0x04000769 RID: 1897
		private EffectParameter {15589};

		// Token: 0x0400076A RID: 1898
		private EffectParameter {15590};

		// Token: 0x0400076B RID: 1899
		private EffectParameter {15591};

		// Token: 0x0400076C RID: 1900
		private EffectParameter {15592};

		// Token: 0x0400076D RID: 1901
		private EffectParameter {15593};

		// Token: 0x0400076E RID: 1902
		private EffectParameter {15594};

		// Token: 0x0400076F RID: 1903
		private EffectParameter {15595};

		// Token: 0x04000770 RID: 1904
		private EffectParameter {15596};

		// Token: 0x04000771 RID: 1905
		public EffectParameter ep_colorMultiplier;

		// Token: 0x04000772 RID: 1906
		protected SkyTextures currentSky;

		// Token: 0x04000773 RID: 1907
		private SkyBox {15597};

		// Token: 0x04000774 RID: 1908
		private SkyBox {15598};

		// Token: 0x04000775 RID: 1909
		private float {15599};

		// Token: 0x04000776 RID: 1910
		private Vector3 {15600};

		// Token: 0x04000777 RID: 1911
		private Vector3 {15601};

		// Token: 0x04000778 RID: 1912
		private Vector3 {15602};

		// Token: 0x04000779 RID: 1913
		private float {15603};

		// Token: 0x0400077A RID: 1914
		private float {15604};

		// Token: 0x0400077B RID: 1915
		public SkyTextures AdditionalStyle;

		// Token: 0x0400077C RID: 1916
		public float AdditionalStyleWeight;

		// Token: 0x02000181 RID: 385
		public enum StarSkyDrawMode
		{
			// Token: 0x0400077E RID: 1918
			Default,
			// Token: 0x0400077F RID: 1919
			WithEffects,
			// Token: 0x04000780 RID: 1920
			Blurry
		}
	}
}
