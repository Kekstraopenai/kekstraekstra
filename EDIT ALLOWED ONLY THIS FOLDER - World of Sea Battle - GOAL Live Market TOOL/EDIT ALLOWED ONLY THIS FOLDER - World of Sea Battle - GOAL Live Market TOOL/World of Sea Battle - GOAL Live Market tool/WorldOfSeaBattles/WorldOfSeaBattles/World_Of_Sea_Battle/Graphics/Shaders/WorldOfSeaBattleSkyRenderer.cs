using System;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Components.Architecture;
using TheraEngine.Components.Scene;
using TheraEngine.Core;
using TheraEngine.Graphics.Models;
using WorldOfSeaBattles;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics.Shaders
{
	// Token: 0x0200046D RID: 1133
	internal sealed class WorldOfSeaBattleSkyRenderer : SkyRenderer, IUpdateableObject
	{
		// Token: 0x060018A4 RID: 6308 RVA: 0x000D48FC File Offset: 0x000D2AFC
		public WorldOfSeaBattleSkyRenderer(string {23712}, ContentManager {23713}) : base({23712}, {23713}, UWModel.CreateAll(null, {23713}.Load<Model>("Models\\sky_clipped")), UWModel.CreateAll(null, {23713}.Load<Model>("Models\\sky")))
		{
			this.Reload({23713});
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x000D4930 File Offset: 0x000D2B30
		public void Reload(ContentManager {23714})
		{
			if (this.{23721} != null)
			{
				foreach (SkyTextures skyTextures in this.{23721})
				{
					skyTextures.Day.Dispose();
					skyTextures.Night.Dispose();
					Texture2D nightMask = skyTextures.NightMask;
					if (nightMask != null)
					{
						nightMask.Dispose();
					}
					skyTextures.StormDecal.Dispose();
					skyTextures.Sunrise1.Dispose();
					skyTextures.Sunrise2.Dispose();
					skyTextures.Sunrise2Rev.Dispose();
					skyTextures.Stars.Dispose();
					skyTextures.StartsTurbulence.Dispose();
				}
			}
			Texture2D {15533} = {23714}.Load<Texture2D>(PathContent.dir_sky_textures + "new\\\\dec_clouds");
			Texture2D {15533}2 = {23714}.Load<Texture2D>(PathContent.dir_sky_textures + "new\\\\dec_clouds_winter");
			Texture2D {15533}3 = {23714}.Load<Texture2D>(PathContent.dir_sky_textures + "new\\\\dec_clouds_tight");
			Texture2D {15533}4 = {23714}.Load<Texture2D>(PathContent.dir_sky_textures + "new\\\\dec_foggy");
			Texture2D {15527} = {23714}.Load<Texture2D>(PathContent.dir_sky_textures + "new\\\\e0");
			Texture2D {15528} = {23714}.Load<Texture2D>(PathContent.dir_sky_textures + "new\\\\e0_uplayer");
			Texture2D {15527}2 = {23714}.Load<Texture2D>(PathContent.dir_sky_textures + "new\\\\e0_alt");
			Texture2D {15528}2 = null;
			Texture2D {15529} = {23714}.Load<Texture2D>(PathContent.dir_sky_textures + "new\\\\e1");
			Texture2D {15530} = {23714}.Load<Texture2D>(PathContent.dir_sky_textures + "new\\\\e2");
			Texture2D {15531} = {23714}.Load<Texture2D>(PathContent.dir_sky_textures + "hq\\\\e2_reverse");
			Texture2D {15532} = {23714}.Load<Texture2D>(PathContent.dir_sky_textures + "hq\\\\e3");
			Texture2D {15532}2 = {23714}.Load<Texture2D>(PathContent.dir_sky_textures + "hq\\\\e3_vers2");
			Texture2D {15534} = {23714}.Load<Texture2D>(PathContent.dir_sky_textures + "hq\\\\map_stars");
			Texture2D {15535} = {23714}.Load<Texture2D>(PathContent.dir_sky_textures + "new\\\\star_turbulence");
			this.{23721} = new SkyTextures[]
			{
				new SkyTextures({15527}, {15528}, {15529}, {15530}, {15531}, {15532}, {15533}4, {15534}, {15535}, SceneColorSet.Normal, 1f, 0f),
				new SkyTextures({15527}2, {15528}2, {15529}, {15530}, {15531}, {15532}2, {15533}2, {15534}, {15535}, SceneColorSet.Winter, 0.7f, 0f),
				new SkyTextures({15527}2, {15528}2, {15529}, {15530}, {15531}, {15532}2, {15533}4, {15534}, {15535}, SceneColorSet.Atmospheric, 1f, 0.15f),
				new SkyTextures({15527}, {15528}, {15529}, {15530}, {15531}, {15532}2, {15533}, {15534}, {15535}, SceneColorSet.DarkMountains, 1f, 0f),
				new SkyTextures({15527}, {15528}, {15529}, {15530}, {15531}, {15532}, {15533}3, {15534}, {15535}, SceneColorSet.Normal, 1f, 0f)
			};
			this.currentSky = this.{23721}[0];
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x000D4BF4 File Offset: 0x000D2DF4
		public void Update(ref FrameTime {23715})
		{
			this.ep_colorMultiplier.SetValue(new Vector3(0.98f, 0.97f, 0.98f));
			base.Update(ref {23715}, Global.Game.StaticSystem.GetZipperLighting, Global.Render.GetSceneManager);
			if (Global.Player != null && Global.Player.MapInfo.IsWorldmap)
			{
				SceneColorSet.Winter.FogNightColor = new Vector3(0.09f, 0.182f, 0.28f) * 1.1f;
				SceneColorSet.Winter.FogDayUpColor = new Vector3(0.46f, 0.67f, 0.81f) * 0.9f;
				SceneColorSet.Winter.FogStormColor = new Vector3(0.7f, 0.737f, 0.9f) * 0.6f;
				this.AdditionalStyle = ((Global.Game.StaticSystem.OpenWorldWinterFactor > 0f) ? this.{23721}[1] : this.{23721}[2]);
				this.AdditionalStyleWeight = Math.Max(CommonGlobal.CurrentClientWeather.FogLevelClientOfExtra * 0.45f, Global.Game.StaticSystem.OpenWorldWinterFactor);
				return;
			}
			if (Global.Game.StaticSystem.StrongEffectAmount > 0f)
			{
				this.AdditionalStyle = this.{23721}[4];
				this.AdditionalStyleWeight = Global.Game.StaticSystem.StrongEffectAmount;
				return;
			}
			this.AdditionalStyleWeight = 0f;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x000D4D74 File Offset: 0x000D2F74
		public void Apply(bool {23716}, bool {23717}, bool {23718})
		{
			SceneManager getSceneManager = Global.Render.GetSceneManager;
			SkyRenderer.StarSkyDrawMode {15554} = Global.Settings.EnableBasicEffects ? SkyRenderer.StarSkyDrawMode.WithEffects : SkyRenderer.StarSkyDrawMode.Default;
			Vector3 vector;
			if (Global.Player != null)
			{
				WeatherEngine currentClientWeather = CommonGlobal.CurrentClientWeather;
				Vector2 position = Global.Player.Position;
				vector = currentClientWeather.StormDirection(position);
			}
			else
			{
				vector = Vector3.Up;
			}
			Vector3 vector2 = vector;
			base.Draw(getSceneManager, {23717}, {23718}, {15554}, vector2, {23716} ? Global.Render.ItemsShader : null);
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x000D4DDB File Offset: 0x000D2FDB
		public SkyTextures ChangeSky(WorldMapInfo {23719}, bool {23720})
		{
			this.currentSky = ({23720} ? this.{23721}[1] : this.{23721}[{23719}.Style - WorldMapStyle.Default]);
			return this.currentSky;
		}

		// Token: 0x0400170E RID: 5902
		private SkyTextures[] {23721};
	}
}
