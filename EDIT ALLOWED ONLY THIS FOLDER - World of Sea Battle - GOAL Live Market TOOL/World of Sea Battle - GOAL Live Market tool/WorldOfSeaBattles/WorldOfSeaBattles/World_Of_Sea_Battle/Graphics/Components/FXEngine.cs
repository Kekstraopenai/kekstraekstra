using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Scene.ParticleSystem;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics.Effects;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x02000472 RID: 1138
	internal sealed class FXEngine
	{
		// Token: 0x060018B1 RID: 6321 RVA: 0x000D51E4 File Offset: 0x000D33E4
		private static void CallWarfogHelper(Vector3 {23744})
		{
			if (Rand.Chanse(50f) && FXEngine.warfogHelper.IsAdd({23744}.XZ()))
			{
				FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23744} + Vector3.Up * 10000f), 13f, 4000f, 70000f, 1.1f, false, FXEngine.PowderParticleType.GrayDim, 0.7f, null);
				float reduceColorSec = FXEngine.template_PowderSmoke.ReduceColorSec;
				float singleWindFactor = FXEngine.template_PowderSmoke.SingleWindFactor;
				FXEngine.template_PowderSmoke.ReduceColorSec = 0.01f;
				FXEngine.template_PowderSmoke.SingleWindFactor = 0.3f;
				FXEngine.template_PowderSmoke.SingleColor = FXEngine.powderSmokeParticleColor * 0.2f;
				FXEngine.template_PowderSmoke.Apply(new ParticleEffectSampleCall({23744} + Vector3.Up * 2f));
				FXEngine.template_PowderSmoke.ReduceColorSec = reduceColorSec;
				FXEngine.template_PowderSmoke.SingleWindFactor = singleWindFactor;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x000D52E0 File Offset: 0x000D34E0
		public static Vector3 GunLightColor
		{
			get
			{
				return Color.Lerp(Color.Orange, Color.LightYellow, 0.5f).ToVector3() * (0.6f - 0.3f * FXEngine.isNotDark);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x000D5320 File Offset: 0x000D3520
		public static float isNotDark
		{
			get
			{
				return Math.Min(Global.Game.StaticSystem.GetSkyShader.DayOrNight, 1f - 0.5f * Global.Render.CloudeLevel * (float)((Global.Player != null && Global.Player.MapInfo.Style == WorldMapStyle.Snow) ? 0 : 1));
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x000D537B File Offset: 0x000D357B
		internal static Color darkParticleColor
		{
			get
			{
				return Color.Lerp(Color.Lerp(FXEngine.nightDarkParticle, FXEngine.dayDarkParticle, FXEngine.isNotDark), FXEngine.smokeHues[Rand.RangeInt(0, FXEngine.smokeHues.Length)], 0.05f);
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x000D53B4 File Offset: 0x000D35B4
		internal static Vector4 powderSmokeParticleColor
		{
			get
			{
				return Vector4.Lerp(new Vector4(114.1f, 117.6f, 116.9f, 178.5f) / 255f, new Vector4(208f, 216f, 213f, 255f) / 255f, FXEngine.isNotDark);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x000D5411 File Offset: 0x000D3611
		internal static Color waterParticleColor
		{
			get
			{
				return Color.Lerp(Color.Lerp(Color.DarkGray, Color.LightGray, FXEngine.isNotDark), Color.LightCyan, 0.4f);
			}
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x000D5438 File Offset: 0x000D3638
		private static float DilateSize(float {23745}, Vector3 {23746})
		{
			float x;
			Vector3.Distance(ref Engine.GS.Camera.Position, ref {23746}, out x);
			{23745} += Math.Max(0f, MathF.Log(x) - 1f) * 0.17f;
			return {23745};
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x000D5480 File Offset: 0x000D3680
		public static void CreateShipWaves(Ship {23747}, ref FrameTime {23748})
		{
			Vector2 vector = {23747}.Normal * ({23747}.UsedShip.StaticInfo.FrontWaveCut * 0.75f + {23747}.NowSpeed / 20f);
			Vector2 value = {23747}.Position + vector;
			float num = ({23747}.UsedShip.StaticInfo.CorpusShape.FinalLength + {23747}.UsedShip.StaticInfo.CorpusShape.FinalWidth) * 0.1f;
			float rotation = {23747}.Rotation;
			float num2 = 0.03f + {23747}.NowSpeed / 40f;
			Global.Game.StaticSystem.AddOceanParticle(value, num * 1.1f, OceanParticleType.Right, rotation - 0.07f, num2 * 2.5f, 1f);
			Global.Game.StaticSystem.AddOceanParticle(value, num * 1.1f, OceanParticleType.Left, rotation + 0.07f, num2 * 2.5f, 1f);
			Global.Game.StaticSystem.AddOceanParticle(value, 4f, false, false);
			num2 = 0.12f + {23747}.NowSpeed / 55f;
			value -= vector * 2f;
			Global.Game.StaticSystem.AddOceanParticle(value, num * 1.2f, OceanParticleType.Right, rotation, num2, 1f);
			Global.Game.StaticSystem.AddOceanParticle(value, num * 1.2f, OceanParticleType.Left, rotation, num2, 1f);
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x000D55F0 File Offset: 0x000D37F0
		public static void CreateShipWavesParticles(Ship {23749}, ref FrameTime {23750})
		{
			Color waterParticleColor = FXEngine.waterParticleColor;
			float num = ({23749}.UsedShip.StaticInfo.CorpusHalfLength * 1.2f + 2.2f) * 0.6f;
			float {23752} = ({23749}.NowSpeed > 0f) ? ({23749}.UsedShip.StaticInfo.FrontWaveCut + 0.35f) : {23749}.UsedShip.StaticInfo.BackWaveCut;
			float num2 = Math.Min({23749}.physicsBody.NowSpeed, 15f) * 0.12f;
			FXEngine.WaterParticleVolumtericOneSide({23749}.Position, {23752}, num, {23749}.physicsBody.SupportUnits[0], {23749}.Rotation, waterParticleColor, num2);
			FXEngine.WaterParticleVolumtericOneSide({23749}.Position, {23752}, num, {23749}.physicsBody.SupportUnits[1], {23749}.Rotation, waterParticleColor, num2);
			if ({23749}.physicsBody.LastDriftAmount > 0f)
			{
				for (int i = 0; i < 7; i++)
				{
					FXEngine.WaterParticleHelper(Vector2.Lerp({23749}.Position + {23749}.Normal * {23749}.UsedShip.StaticInfo.FrontWaveCut, {23749}.Position + {23749}.Normal * {23749}.UsedShip.StaticInfo.BackWaveCut, (float)i / 6f), num * 0.2f, new Vector3(0f, 2f, 0f), {23749}.physicsBody.LastDriftAmount, 0.25f, waterParticleColor * 0.91f, num2);
				}
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x000D5780 File Offset: 0x000D3980
		private static void WaterParticleVolumtericOneSide(Vector2 {23751}, float {23752}, float {23753}, ShipPhysics.SupportUnit {23754}, float {23755}, Color {23756}, float {23757})
		{
			float {23765} = -{23754}.LastD + 0.0175f + 0.6f;
			float num = (float)Math.Sign({23754}.LocalPosition.Y);
			Vector2 vector = FXEngine.localTothrowP(new Vector2(0f, num), Vector2.Zero, 9f, {23755}) * 0.3f;
			Geometry.RotateVector2Fast(ref vector, Rand.Range(-0.2f, 0.2f), out vector);
			FXEngine.WaterParticleHelper(FXEngine.localTothrowP(new Vector2(1f, 0.1f), {23751}, {23752}, {23755}), {23753} * 0.15f, new Vector3(vector.X, 1f, vector.Y), {23765}, 0.05f, {23756} * 0.84f, {23757});
			FXEngine.WaterParticleHelper(FXEngine.localTothrowP(new Vector2(1f, 0.1f * num), {23751}, {23752}, {23755}), {23753} * 0.2f, new Vector3(vector.X * 1.2f, 2f, vector.Y * 1.2f), {23765}, 0.25f, {23756} * 0.91f, {23757});
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x000D589C File Offset: 0x000D3A9C
		private static Vector2 localTothrowP(Vector2 {23758}, Vector2 {23759}, float {23760}, float {23761})
		{
			{23758}.Normalize();
			Geometry.RotateVector2Fast(ref {23758}, {23761}, out {23758});
			return {23758} * {23760} + {23759};
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x000D58BC File Offset: 0x000D3ABC
		private static void WaterParticleHelper(Vector2 {23762}, float {23763}, Vector3 {23764}, float {23765}, float {23766}, Color {23767}, float {23768})
		{
			if ({23765} < {23766})
			{
				return;
			}
			float num = Math.Min(1f, ({23765} - {23766}) / 0.4f);
			{23763} *= 1f + Math.Max(0f, Math.Min(1f, ({23765} - {23766} - 0.3f) / 0.6f));
			if (Rand.Range(0f, 0.5f) < num && Rand.Chanse(75f))
			{
				FXEngine.WaterParticleVolumteric({23762}, {23763}, {23764} * new Vector3(0.5f, 1f, 0.5f), {23767}, {23768}, null);
			}
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x000D5960 File Offset: 0x000D3B60
		public static void WaterParticleVolumteric(Vector2 {23769}, float {23770}, Vector3 {23771}, Color {23772}, float {23773}, float? {23774} = null)
		{
			if (Global.Player == null)
			{
				return;
			}
			Vector3 value = new Vector3({23769}.X, {23774} ?? CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, {23769}.X, {23769}.Y), {23769}.Y);
			FXEngine.template_ShipWaterParticle.SingleTtlToShow = new Range1D(50f, 70f);
			FXEngine.template_ShipWaterParticle.SingleInitialSize.Start = {23770} * 0.9f * 1.2f;
			FXEngine.template_ShipWaterParticle.SingleInitialSize.End = {23770} * 1.1f * 1.2f;
			FXEngine.template_ShipWaterParticle.SingleColor = {23772}.ToVector4() * 3f;
			FXEngine.template_ShipWaterParticle.Apply(new ParticleEffectSampleCall(value - Vector3.Up * 0.3f, {23771} * {23773} * new Vector3(1.1f, 1f, 1.1f) + Vector3.Up * 1f, Vector3.Down * 5f * (1f + {23773})));
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000D5AA0 File Offset: 0x000D3CA0
		public static void NewMassiveSplash(Vector2 {23775}, float {23776})
		{
			if (Global.Player == null)
			{
				return;
			}
			Global.Game.StaticSystem.AddOceanParticle({23775}, {23776} * 0.7f * 3f, false, true);
			for (int i = 0; i < 3; i++)
			{
				Global.Game.StaticSystem.AddOceanParticle({23775}, {23776} * 2.6f * 2.3f, false, false);
			}
			Vector3 value = new Vector3({23775}.X, CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, {23775}.X, {23775}.Y), {23775}.Y);
			Color waterParticleColor = FXEngine.waterParticleColor;
			FXEngine.template_WaterParticleVSplash.SingleColor = waterParticleColor.ToVector4();
			FXEngine.template_WaterParticleVSplash.ReduceColorSec = -0.2f;
			for (int j = 0; j < 5; j++)
			{
				Vector3 vector = (j == 0) ? Vector3.Up : (Vector3.Up + Rand.NextVector3(-1f, 1f) * 0.5f);
				vector = vector.Normal();
				for (int k = 0; k < 15; k++)
				{
					float num = (k < 5) ? ((float)k) : (5f + MathF.Pow((float)(k - 5), 0.6f));
					float num2 = {23776} * (1.5f - 0.15f * num) * 0.5f * ((j == 0) ? 2f : 1f);
					FXEngine.template_WaterParticleVSplash.SingleInitialSize = new Range1D(num2, num2);
					FXEngine.template_WaterParticleVSplash.SingleSizeVelocity = Math.Max(0.4f, 0.8f - num * 0.08f);
					FXEngine.template_WaterParticleVSplash.Apply(new ParticleEffectSampleCall(value + vector.XZ().X0Y(), vector * (1f + num * 0.17f) * {23776} * 2f * ((j == 0) ? 1.5f : 1f), Vector3.Down * 3f * {23776} * 1.5f));
				}
			}
			FXEngine.template_WaterParticleVSplash.SingleSizeVelocity = 0.8f;
			FXEngine.template_WaterParticleVSplash.ReduceColorSec = 0f;
			for (int l = 0; l < 2; l++)
			{
				FXEngine.template_ShipWaterParticle.SingleTtlToShow = new Range1D(50f, 70f);
				FXEngine.template_ShipWaterParticle.SingleInitialSize.Start = {23776} * 0.9f * 2f;
				FXEngine.template_ShipWaterParticle.SingleInitialSize.End = {23776} * 1.1f * 2f;
				FXEngine.template_ShipWaterParticle.SingleColor = waterParticleColor.ToVector4() * 3f;
				float singleSizeVelocity = FXEngine.template_ShipWaterParticle.SingleSizeVelocity;
				FXEngine.template_ShipWaterParticle.SingleSizeVelocity = 3f;
				FXEngine.template_ShipWaterParticle.Apply(new ParticleEffectSampleCall(value - Vector3.Up * 0.3f, Vector3.Up * 3f, Vector3.Down * 5f));
				FXEngine.template_ShipWaterParticle.SingleSizeVelocity = singleSizeVelocity;
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000D5DBC File Offset: 0x000D3FBC
		public static void NewWaterSplash(Vector2 {23777}, float {23778}, bool {23779})
		{
			if (Global.Player == null)
			{
				return;
			}
			Global.Game.StaticSystem.AddOceanParticle({23777}, {23778} * 0.7f, false, true);
			Global.Game.StaticSystem.AddOceanParticle({23777}, {23778} * 2.6f, false, false);
			Color waterParticleColor = FXEngine.waterParticleColor;
			Vector3 vector = new Vector3({23777}.X, CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, {23777}.X, {23777}.Y), {23777}.Y);
			if ({23779})
			{
				FXEngine.template_WaterParticleVSplash.SingleColor = waterParticleColor.ToVector4();
				FXEngine.template_WaterParticleVSplash.SingleBrightness = 1.4f;
				for (int i = 0; i < 6; i++)
				{
					float num = {23778} * (1.5f - 0.15f * (float)i) * 0.1f;
					FXEngine.template_WaterParticleVSplash.SingleInitialSize = new Range1D(num, num);
					FXEngine.template_WaterParticleVSplash.SingleSizeVelocity = 0.8f - (float)i * 0.08f;
					FXEngine.template_WaterParticleVSplash.Apply(new ParticleEffectSampleCall(vector, Vector3.Up * (1f + (float)i * 0.2f) * {23778} * 1.5f, Vector3.Down * 3f * {23778} * 1.5f));
				}
				FXEngine.template_WaterParticleVSplash.SingleSizeVelocity = 0.8f;
			}
			for (int j = 0; j < 2; j++)
			{
				FXEngine.template_ShipWaterParticle.SingleTtlToShow = new Range1D(50f, 70f);
				FXEngine.template_ShipWaterParticle.SingleInitialSize.Start = {23778} * 0.9f * 1f;
				FXEngine.template_ShipWaterParticle.SingleInitialSize.End = {23778} * 1.1f * 1f;
				FXEngine.template_ShipWaterParticle.SingleColor = waterParticleColor.ToVector4() * 3f;
				float singleSizeVelocity = FXEngine.template_ShipWaterParticle.SingleSizeVelocity;
				FXEngine.template_ShipWaterParticle.SingleSizeVelocity = 2f;
				FXEngine.template_ShipWaterParticle.Apply(new ParticleEffectSampleCall(vector - Vector3.Up * 0.3f, Vector3.Up * 3f, Vector3.Down * 5f));
				FXEngine.template_ShipWaterParticle.SingleSizeVelocity = singleSizeVelocity;
			}
			float singleWindFactor = FXEngine.template_PowderSmoke.SingleWindFactor;
			FXEngine.template_PowderSmoke.SingleWindFactor = 0f;
			for (int k = 0; k < 1; k++)
			{
				FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall(vector, Vector3.Up, Vector3.Down * 3f), {23778} * 1.1f, 500f, 1600f, 1f, false, FXEngine.PowderParticleType.Gray, 1.5f, null);
			}
			FXEngine.template_PowderSmoke.SingleWindFactor = singleWindFactor;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x000D6088 File Offset: 0x000D4288
		public static void CreateSingleWaterParticle2(Vector3 {23780}, float {23781}, bool {23782}, float {23783} = 0f)
		{
			FXEngine.template_WaterParticleVSplash.SingleColor = (({23783} == 0f) ? (FXEngine.waterParticleColor * 0.9f) : (FXEngine.waterParticleColor * 0.4f)).ToVector4() * 0.7f;
			FXEngine.template_WaterParticleVSplash.SingleBrightness = 1.4f;
			Vector3 {12176} = new Vector3({23780}.X, {23782} ? {23780}.Y : ({23781} * 0.25f + CommonGlobal.CurrentClientWeather.HeightOnlyHelper((Global.Player == null) ? Gameplay.WorldMap : Global.Player.MapInfo, {23780}.X, {23780}.Y)), {23780}.Z);
			FXEngine.template_WaterParticleVSplash.SingleInitialSize = new Range1D({23781}, {23781});
			FXEngine.template_WaterParticleVSplash.Apply(new ParticleEffectSampleCall({12176}, Vector3.Up, Vector3.Down * 3f * {23783}));
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x000D6178 File Offset: 0x000D4378
		public static void WaterParticleLight(Vector3 {23784}, float {23785}, bool {23786}, bool {23787})
		{
			{23785} *= 2f;
			FXEngine.template_WaterParticleVSplash.SingleColor = FXEngine.waterParticleColor.ToVector4() * 0.12f;
			FXEngine.template_WaterParticleVSplash.SingleBrightness = 1.2f;
			Vector3 value = new Vector3({23784}.X, {23786} ? {23784}.Y : ({23785} * 0.25f + CommonGlobal.CurrentClientWeather.HeightOnlyHelper((Global.Player == null) ? Gameplay.WorldMap : Global.Player.MapInfo, {23784}.X, {23784}.Y)), {23784}.Z);
			FXEngine.template_WaterParticleVSplash.SingleInitialSize = new Range1D({23785}, {23785});
			float singleSizeVelocity = FXEngine.template_WaterParticleVSplash.SingleSizeVelocity;
			Range1D singleTtl = FXEngine.template_WaterParticleVSplash.SingleTtl;
			FXEngine.template_WaterParticleVSplash.SingleSizeVelocity = 0f;
			FXEngine.template_WaterParticleVSplash.SingleTtl = new Range1D(1000f, 2000f);
			FXEngine.template_WaterParticleVSplash.Apply(new ParticleEffectSampleCall(value - CommonGlobal.CurrentClientWeather.WindDirection, ({23787} ? (CommonGlobal.CurrentClientWeather.WindDirection * 6f) : Vector3.Zero) - Vector3.Up * 0.2f, Vector3.Zero));
			FXEngine.template_WaterParticleVSplash.SingleSizeVelocity = singleSizeVelocity;
			FXEngine.template_WaterParticleVSplash.SingleTtl = singleTtl;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x000D62D0 File Offset: 0x000D44D0
		public static void GunEffectSmall2(Vector3 {23788}, Vector3 {23789}, Vector2 {23790}, bool {23791})
		{
			float num = Vector3.Dot({23789}, Engine.GS.Camera.Direction);
			int num2 = (num < -0.2f) ? 2 : ((num > 0.8f) ? 1 : 0);
			{23788} += {23789} * 0.5f;
			Vector3 vector = new Vector3({23790}.X, 0f, {23790}.Y) * 60f;
			if ({23791})
			{
				FXEngine.template_ExplosionParticle.IntialAxisIfRandomDisabled = ((Geometry.RotateVector2({23789}.XZ(), -Engine.GS.Camera.Rotation.Y).X > 0f) ? -1.5707964f : 1.5707964f);
				FXEngine.template_ExplosionParticle.SingleBrightness = 1.9f;
				FXEngine.TemplateExplosionEffect_simple(AtlasObjs.Particles.sprite_Explosion, new ParticleEffectSampleCall({23788}, {23789} * 0.4f + vector), 0.2f, Vector4.One, 2f, false);
				FXEngine.TemplateExplosionEffect_simple(AtlasObjs.Particles.sprite_Explosion, new ParticleEffectSampleCall({23788} + {23789} * 0.2f, {23789} * 0.5f + vector), 0.4f, Vector4.One, 2f, false);
				FXEngine.template_ExplosionParticle.SingleBrightness = 1f;
			}
			for (int i = num2; i < 4; i++)
			{
				FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23788}, 1.5f * {23789} * (0.5f + (float)i) + vector * 1.1f, -2f), 0.3f, (float)(550 + i * 100), (float)(2000 + i * 500), (float)(1 + i) * 0.2f, false, FXEngine.PowderParticleType.Gray, 0.7f, null);
			}
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x000D6490 File Offset: 0x000D4690
		public static void PowderGunEffect(Vector3 {23792}, Vector3 {23793})
		{
			FXEngine.TemplateExplosionEffect_simple(AtlasObjs.Particles.p_sparks, new ParticleEffectSampleCall({23792} + {23793} * 0.5f, {23793} * 3f), 1f, new Vector4(1f, 0.8f, 0.2f, 0.5f), 15f, false);
			FXEngine.TemplateExplosionEffect_simple(AtlasObjs.Particles.p_sparks, new ParticleEffectSampleCall({23792} + {23793} * 1f, {23793} * 5f), 1f, new Vector4(1f, 0.8f, 0.2f, 0.5f), 15f, false);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x000D653C File Offset: 0x000D473C
		public static void GunEffect(Vector3 {23794}, Vector3 {23795}, Vector2 {23796}, bool {23797}, bool {23798} = false, float {23799} = 1f, Ship {23800} = null)
		{
			Player player = {23800} as Player;
			bool flag;
			if (player != null)
			{
				ShipDesignInfo designElement = player.UsedShipPlayer.GetDesignElement(4);
				short? num = (designElement != null) ? new short?(designElement.ID) : null;
				flag = (((num != null) ? new int?((int)num.GetValueOrDefault()) : null).GetValueOrDefault() == 468);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if ({23795}.Y < -0.1f)
			{
				{23795}.Y = -0.1f;
				{23795}.Normalize();
			}
			Vector3 value = new Vector3({23796}.X, 0f, {23796}.Y) * 60f;
			float num2 = Vector3.Dot({23795}, Engine.GS.Camera.Direction);
			float num3 = Vector3.DistanceSquared({23794}, Engine.GS.Camera.Position);
			int num4 = Math.Max((num2 < -0.2f) ? 2 : ((num2 > 0.8f) ? 1 : 0), (num3 > 8100f) ? 2 : ((num3 > 2025f) ? 1 : 0));
			if ({23797} && !{23798} && Rand.Chanse(10f))
			{
				FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23794}, {23795} * 12f, -3f), 1f, 500f, 15000f, 1.3f, false, FXEngine.PowderParticleType.Gray, 0.7f, null);
			}
			if ({23798})
			{
				FXEngine.TemplateExplosionEffect_lightOnly({23794} + {23795}, 3f, false);
			}
			float num5 = Rand.Range(0.8f, 1.2f);
			{23794} += {23795} * 0.25f;
			{23795} *= num5;
			Vector2 vector = {23795}.XZ();
			float num6 = 2f;
			int num7 = {23798} ? 2 : 0;
			if ({23797})
			{
				FXEngine.template_ExplosionParticle.IntialAxisIfRandomDisabled = ((Geometry.RotateVector2({23795}.XZ(), -Engine.GS.Camera.Rotation.Y).X > 0f) ? -1.5707964f : 1.5707964f);
				FXEngine.template_ExplosionParticle.SingleBrightness = 1.4f;
				Color.LightYellow.ToVector4();
				for (int i = num4 * 2; i < 8; i++)
				{
					if (!Rand.Chanse(50f * {23799}))
					{
						FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23794}, {23795} * (0.75f + (float)i * 1.5f) + value, -num6), 0.4f + 0.2f * (float)i, (float)((10 - i) * 50), 300f, 1f, false, FXEngine.PowderParticleType.Explosion, 0.7f, flag2 ? new Vector4?(Color.LightCyan.ToVector4()) : null);
					}
				}
				FXEngine.template_ExplosionParticle.SingleBrightness = 1f;
			}
			for (int j = num4 * 2; j < 8; j++)
			{
				if (!Rand.Chanse(66f * {23799}))
				{
					FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23794}, {23795} * (4f + (float)j * 0.9f) * num6 + value, -num6 / num5 / num5), 0.6f + (float)num7, (float)(550 + j * 50), (float)(1000 + j * 500), (!{23798}) ? ((float)(1 + j) * 0.7f) : ((float)(1 + j)), {23798}, FXEngine.PowderParticleType.Gray, 0.7f, null);
				}
			}
			FXEngine.CallWarfogHelper({23794});
			FXEngine.CallWarfogHelper({23794} + {23795} * 7f);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000D68D8 File Offset: 0x000D4AD8
		public static void HitCrewBoardingDamage(Vector3 {23801})
		{
			FXEngine.TemplateExplosionEffect_decal(AtlasObjs.Particles.smallDebris, {23801}, Vector3.Up * 2f, 0.2f, Vector4.One * 0.7f, 3f, 1.2f);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000D6912 File Offset: 0x000D4B12
		public static void CreateTemplateDamageMarker(Vector3 {23802}, Color {23803})
		{
			FXEngine.template_DamageMarker.SingleColor = {23803}.ToVector4();
			FXEngine.template_DamageMarker.Apply(new ParticleEffectSampleCall({23802}));
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000D6935 File Offset: 0x000D4B35
		public static void CreateCrewBlood(Vector3 {23804})
		{
			FXEngine.template_CrewBlood.Apply(new ParticleEffectSampleCall({23804}));
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x000D6948 File Offset: 0x000D4B48
		public static void HitCannonBallExtraEffects(CannonBall.HitType {23805}, ClientCannonBall {23806})
		{
			if ({23806}.HasNotEffects)
			{
				return;
			}
			switch ({23805})
			{
			case CannonBall.HitType.HitsWithWater:
				if ({23806}.BallInfo.ID == 12)
				{
					FXEngine.SampleFumesSmoke({23806}.Sphere, 5f, 1f, 1f);
					return;
				}
				if ({23806}.BallInfo[CannonBallInfoEffects.FireArea])
				{
					Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.CannonBallFireHitWater, {23806}.Sphere, 0.6f, false);
				}
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.Slamming_Big, {23806}.Sphere, 0.6f, false);
				FXEngine.NewWaterSplash({23806}.Sphere.XZ(), 1f, true);
				return;
			case CannonBall.HitType.BombExplosion:
				if ({23806}.BallInfo.ID != 18)
				{
					FXEngine.TemplateExplosionEffect_simple(AtlasObjs.Particles.p_DarkExplosSmoke, new ParticleEffectSampleCall({23806}.Sphere), 10f, new Vector4(0.5f, 0.5f, 0.5f, 1f), 2f, true);
					if (Rand.Chanse(30f))
					{
						new ExplosionWaveFlashEffect({23806}.Sphere, 1f, Color.White, 0.5f);
					}
					Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.PowderKegExplosion, {23806}.Sphere, 0.5f, false);
					return;
				}
				break;
			case CannonBall.HitType.Collision:
			{
				bool flag = {23806}.HitMaterial == HitMaterialEffect.Stone;
				bool flag2 = !flag && {23806}.BallInfo.ID != 12 && {23806}.BallInfo.ID != 21;
				bool flag3 = {23806}.HitboxType == HitboxType.Corpus && {23806}.BallInfo.ID != 21 && {23806}.BallInfo.ID != 3 && {23806}.BallInfo.ID != 4 && {23806}.BallInfo.ID != 12 && {23806}.HitDamage > 0f;
				bool flag4 = ({23806}.HasBuildingDamage && {23806}.HitMaterial == HitMaterialEffect.Stone && Rand.Chanse(30f)) || {23806}.DamageFlags.HasFlag(SpecificDamageFlags.CreateMicroburning);
				{23806}.DamageFlags.HasFlag(SpecificDamageFlags.CannonCritical);
				bool flag5 = {23806}.HasBuildingDamage && {23806}.HitMaterial == HitMaterialEffect.Stone && ({23806}.BallInfo.ID == 2 || {23806}.BallInfo.ID == 9);
				bool flag6 = Rand.Chanse(50f) && {23806}.BallInfo.ID != 21 && {23806}.BallInfo.ID != 12 && {23806}.HitboxType != HitboxType.Mast && (!flag || Rand.Chanse(50f)) && {23806}.HitDamage > 0f;
				Vector3 value = Global.Game.GraphicsSystem.Camera.Position - {23806}.HitPoint;
				float num = 1f + Geometry.Saturate((value.Length() - 50f) / 200f);
				Vector3 vector = {23806}.HitPoint;
				value.Normalize();
				vector += value * 0.3f;
				Vector3 vector2 = new Vector3(-{23806}.StartMomentNormal.X, Rand.Range(1.7f, 2.7f), -{23806}.StartMomentNormal.Z).Normal();
				if (flag2)
				{
					FXEngine.TemplateExplosionEffect_decal(AtlasObjs.Particles.smallDebris, vector, vector2 * new Vector3(2f, 0f, 2f) + Vector3.Up * 3f, 1.5f, Vector4.One, 8f, 3.5f);
				}
				if ({23806}.BallInfo.ID == 21)
				{
					FXEngine.TemplateExplosionEffect_simple(AtlasObjs.Particles.p_sparks, new ParticleEffectSampleCall(vector), 6f * num, new Vector4(0.5f, 0.5f, 0.5f, 1f), 1f, false);
				}
				if (flag5)
				{
					FXEngine.SampleFlameAndSmoke(vector, num, true, false, true, null, 1f);
				}
				if (flag6)
				{
					FXEngine.OnHitDebrisEffect({23806}.HitMaterial, vector, vector2 * Rand.Range(2f, 3f));
				}
				if (flag4)
				{
					FXEngine.TemplateExplosionEffect_simple(AtlasObjs.Particles.p_DarkExplosSmoke, new ParticleEffectSampleCall(vector), 12f * num, new Vector4(0.5f, 0.5f, 0.5f, 1f), 1f, false);
				}
				else if (flag3)
				{
					FXEngine.TemplateExplosionEffect_simple({23806}.DamageFlags.HasFlag(SpecificDamageFlags.SternDamage) ? AtlasObjs.Particles.sprite_ExplosionSmoked : AtlasObjs.Particles.sprite_Explosion, new ParticleEffectSampleCall(vector + value * 0.05f), ({23806}.DamageFlags.HasFlag(SpecificDamageFlags.SternDamage) ? 3f : 0.7f) * num, Vector4.One, (float)(({23806}.BallInfo.ID == 7) ? 2 : 1), false);
				}
				if ({23806}.BallInfo.ID == 21)
				{
					Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.SnowImpact, vector, 0.6f, false);
					return;
				}
				if ({23806}.BallInfo.ID != 12)
				{
					Global.Game.SoundSystem.Play3DSound(flag ? GameDynamicSoundName.ConcreteImpact : GameDynamicSoundName.WoodImpact, vector, 0.6f, false);
					return;
				}
				break;
			}
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000D6E90 File Offset: 0x000D5090
		public static void FalkonetExplosion(Vector3 {23807}, Ship {23808}, int {23809}, HitboxType {23810})
		{
			CannonBallInfo cannonBallInfo = Gameplay.BallsInfo.FromID({23809});
			FXEngine.TemplateExplosionEffect_simple(({23810} == HitboxType.Corpus) ? AtlasObjs.Particles.sprite_Explosion : AtlasObjs.Particles.sprite_ExplosionSmoked, new ParticleEffectSampleCall({23807}), 1.5f, Vector4.One, 1.5f, false);
			int num = Rand.Round((cannonBallInfo.CountParts == 1) ? 0.5f : 1f);
			if (num > 0)
			{
				FXEngine.OnHitDebrisEffectHemisphere(({23810} == HitboxType.Sail) ? HitMaterialEffect.Sailes : HitMaterialEffect.Wood, {23807}, (float)num);
			}
			if ({23809} == 14)
			{
				FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23807}, ({23808} == null) ? Vector3.Zero : ({23808}.physicsBody.VelocityPerSec.X0Y() / 60f * 10f + Rand.NextVector2(-1f, 1f).Normal().X0Y() * 2f)), 1.5f, 100f, 6000f, 6f, false, FXEngine.PowderParticleType.Dark, 1f, null);
			}
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000D6F90 File Offset: 0x000D5190
		public static void FalkonetHitsWithWater(Vector3 {23811}, int {23812})
		{
			FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23811} + Rand.NextVector3(-0.5f, 0.5f)), 2.5f, 100f, 900f, 4f, false, FXEngine.PowderParticleType.GrayDim, 1f, null);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x000D6FE0 File Offset: 0x000D51E0
		public static void FireworkExplosion(Vector3 {23813}, bool {23814})
		{
			FXEngine.TemplateExplosionEffect_lightOnly({23813}, 5f, false);
			FXEngine.template_ExplosionParticle.SingleBrightness = 2f;
			for (int i = 0; i < 3; i++)
			{
				FXEngine.TemplateExplosionEffect_decal(AtlasObjs.Particles.p_Fireworks, {23813} + Rand.NextVector3(-1f, 1f) * 1.5f, Vector3.Zero, Rand.Range(2f, 4f), Vector4.One, 0f, 20f);
			}
			FXEngine.template_ExplosionParticle.SingleBrightness = 1f;
			if ({23814})
			{
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.CannonsGunFar_Assist, {23813}, 0.5f, false);
			}
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x000D708C File Offset: 0x000D528C
		public static void OnHitDebrisEffectHemisphere(HitMaterialEffect {23815}, Vector3 {23816}, float {23817})
		{
			int num = 0;
			while ((float)num < {23817})
			{
				Vector3 {24859} = new Vector3(Rand.Range(-1f, 1f), Rand.Range(0f, 1.4f), Rand.Range(-1f, 1f));
				{24859}.Normalize();
				Global.Game.WorldInstance.CreateDebris({23816} + Rand.NextVector3(-0.5f, 0.5f), {24859}, {23815});
				num++;
			}
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x000D7108 File Offset: 0x000D5308
		public static void OnHitDebrisEffect(HitMaterialEffect {23818}, Vector3 {23819}, Vector3 {23820})
		{
			int num = Rand.RangeInt(1, 3);
			short num2 = 0;
			while ((int)num2 < num)
			{
				Global.Game.WorldInstance.CreateDebris({23819}, {23820}, {23818});
				num2 += 1;
			}
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x000D713C File Offset: 0x000D533C
		private static void TemplateExplosionEffect_simple(IParticleTextureSampler {23821}, ParticleEffectSampleCall {23822}, float {23823}, Vector4 {23824}, float {23825}, bool {23826} = true)
		{
			FXEngine.template_ExplosionParticle.SingleTexturePath = {23821};
			FXEngine.template_ExplosionParticle.SingleInitialSize.Start = {23823} * 0.9f;
			FXEngine.template_ExplosionParticle.SingleInitialSize.End = {23823} * 0.9f;
			FXEngine.template_ExplosionParticle.SingleAngularVelocity = (({23821} == AtlasObjs.Particles.sprite_Explosion) ? 0f : 0.2f);
			FXEngine.template_ExplosionParticle.SingleCreateRandomInitialAxis = ({23821} != AtlasObjs.Particles.sprite_Explosion);
			FXEngine.template_ExplosionParticle.SingleSizeVelocity = ((!{23821}.IsSprite) ? 4.2f : 1.2f);
			FXEngine.template_ExplosionParticle.SingleColor = {23824};
			FXEngine.template_ExplosionParticle.SingleTtl.Start = 800f;
			FXEngine.template_ExplosionParticle.SingleTtl.End = 800f;
			FXEngine.template_ExplosionParticle.SingleBrightness = {23825};
			FXEngine.template_ExplosionParticle.Apply({23822});
			FXEngine.template_ExplosionParticle.SingleBrightness = 1f;
			if ({23826})
			{
				FXEngine.template_ExplosionParticleLightpass.SingleInitialSize.Start = {23823} * 2f;
				FXEngine.template_ExplosionParticleLightpass.SingleInitialSize.End = {23823} * 2.3f;
				FXEngine.template_ExplosionParticleLightpass.SingleColor = new Color(254, 198, 70, 255).ToVector4();
				FXEngine.template_ExplosionParticleLightpass.Apply({23822});
			}
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x000D7290 File Offset: 0x000D5490
		private static void TemplateExplosionEffect_decal(IParticleTextureSampler {23827}, Vector3 {23828}, Vector3 {23829}, float {23830}, Vector4 {23831}, float {23832}, float {23833} = 4.2f)
		{
			FXEngine.template_ExplosionParticle.SingleTexturePath = {23827};
			FXEngine.template_ExplosionParticle.SingleInitialSize.Start = {23830} * 0.9f;
			FXEngine.template_ExplosionParticle.SingleInitialSize.End = {23830} * 1.1f;
			FXEngine.template_ExplosionParticle.SingleAngularVelocity = 0f;
			FXEngine.template_ExplosionParticle.SingleCreateRandomInitialAxis = true;
			FXEngine.template_ExplosionParticle.SingleColor = {23831};
			FXEngine.template_ExplosionParticle.SingleTtl.Start = 900f;
			FXEngine.template_ExplosionParticle.SingleTtl.End = 1000f;
			FXEngine.template_ExplosionParticle.SingleSizeVelocity = {23833};
			FXEngine.template_ExplosionParticle.SingleGravityOrFlyVelocityPerSecAddToVelocity = {23832};
			FXEngine.template_ExplosionParticle.Apply(new ParticleEffectSampleCall({23828}, {23829}, -0.5f));
			FXEngine.template_ExplosionParticle.SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x000D7360 File Offset: 0x000D5560
		private static void TemplateExplosionEffect_lightOnly(Vector3 {23834}, float {23835}, bool {23836} = false)
		{
			FXEngine.template_ExplosionParticleLightpass.SingleInitialSize.Start = {23835} * 2f;
			FXEngine.template_ExplosionParticleLightpass.SingleInitialSize.End = {23835} * 2.3f;
			FXEngine.template_ExplosionParticleLightpass.SingleColor = ({23836} ? Color.SkyBlue : new Color(244, 205, 107, 112)).ToVector4() * 0.5f;
			FXEngine.template_ExplosionParticleLightpass.Apply(new ParticleEffectSampleCall({23834}));
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x000D73E4 File Offset: 0x000D55E4
		public static void TemplatePowderSmoke(ParticleEffectSampleCall {23837}, float {23838}, float {23839}, float {23840}, float {23841}, bool {23842}, FXEngine.PowderParticleType {23843}, float {23844} = 0.7f, Vector4? {23845} = null)
		{
			if ({23843} == FXEngine.PowderParticleType.Gray || {23843} == FXEngine.PowderParticleType.GrayDim)
			{
				FXEngine.TemplatePowderSmokeCustom({23837}, {23838}, {23839}, {23840}, {23841}, ({23845} ?? FXEngine.powderSmokeParticleColor) * (({23844} == 0.7f) ? 0.6f : {23844}), ({23843} == FXEngine.PowderParticleType.GrayDim) ? 1f : 1.3f, AtlasObjs.Particles.p_Cloud, {23842} ? 3 : 1);
				return;
			}
			if ({23843} == FXEngine.PowderParticleType.Explosion)
			{
				FXEngine.TemplatePowderSmokeCustom({23837}, {23838}, {23839}, {23840}, {23841}, {23845} ?? (new Vector4(255f, 222f, 150f, 255f) / 255f * 1.4f), 3f, AtlasObjs.Particles.p_Cloud_contrast, {23842} ? 3 : 1);
				return;
			}
			FXEngine.TemplatePowderSmokeCustom({23837}, {23838}, {23839}, {23840}, {23841}, ({23845} ?? FXEngine.darkParticleColor.ToVector4()) * {23844}, 1f, AtlasObjs.Particles.p_Cloud, {23842} ? 3 : 1);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x000D7500 File Offset: 0x000D5700
		public static void TemplatePowderSmokeCustom(ParticleEffectSampleCall {23846}, float {23847}, float {23848}, float {23849}, float {23850}, Vector4 {23851}, float {23852}, RandomParticleTextureSet {23853} = null, int {23854} = 1)
		{
			FXEngine.template_PowderSmoke.GeneratorCountPerSample = {23854};
			FXEngine.template_PowderSmoke.GeneratorRandomOffset = (({23854} > 1) ? ({23847} / 4f) : 0.1f);
			FXEngine.template_PowderSmoke.SingleInitialSize.Start = {23847} * 0.9f;
			FXEngine.template_PowderSmoke.SingleInitialSize.End = {23847} * 1.1f;
			FXEngine.template_PowderSmoke.SingleTtlToShow.Start = {23848};
			FXEngine.template_PowderSmoke.SingleTtlToShow.End = {23848};
			FXEngine.template_PowderSmoke.SingleTtl.Start = {23849} * 0.8f;
			FXEngine.template_PowderSmoke.SingleTtl.End = {23849} * 1.2f;
			FXEngine.template_PowderSmoke.SingleSizeVelocity = 1.5f * {23850};
			FXEngine.template_PowderSmoke.SingleColor = {23851};
			FXEngine.template_PowderSmoke.SingleBrightness = {23852};
			FXEngine.template_PowderSmoke.SingleTexturePath = ({23853} ?? AtlasObjs.Particles.p_Cloud);
			FXEngine.template_PowderSmoke.ReduceColorSec = (({23849} < 10000f) ? -0.2f : -0.1f);
			FXEngine.template_PowderSmoke.Apply({23846});
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x000D7618 File Offset: 0x000D5818
		public static void SampleFumesSmoke(Vector3 {23855}, float {23856} = 1f, float {23857} = 1f, float {23858} = 1f)
		{
			FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23855}, Vector3.Up * 2f), 2f * {23856}, 250f, 5000f * {23857}, 0.6f, false, FXEngine.PowderParticleType.GrayDim, {23858}, null);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x000D7664 File Offset: 0x000D5864
		public static void SampleFumesSmokeLong(Vector3 {23859}, float {23860} = 1f)
		{
			FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23859}), 2f * {23860}, 250f, 16000f, 0.6f, true, FXEngine.PowderParticleType.GrayDim, 0.7f, null);
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x000D76A4 File Offset: 0x000D58A4
		public static ParticleSystem3D CreateTrailForFalkonetBall(Vector3 {23861}, int {23862})
		{
			int num = {23862} - 13 + 1;
			FXEngine.template_CompressableTrail.SingleInitialSize.Start = ((num == 4) ? 1f : 0.6f);
			FXEngine.template_CompressableTrail.SingleInitialSize.End = ((num == 4) ? 1f : 0.6f);
			FXEngine.template_CompressableTrail.SingleBrightness = 3f;
			FXEngine.template_CompressableTrail.SingleTexturePath = ((num == 2) ? AtlasObjs.Particles.p_LiteSmoke : ((num == 4) ? AtlasObjs.Particles.p_sparks : AtlasObjs.Particles.p_FireSparkOrange));
			FXEngine.template_CompressableTrail.SingleColor = ((num == 4) ? (Color.SeaGreen.ToVector4() * 0.5f) : ((num == 2) ? Color.Gray.ToVector4() : (({23862} == 18) ? Color.Orange.ToVector4() : (Vector4.One * 0.5f))));
			FXEngine.template_CompressableTrail.SingleTtl = ((num == 2 || num == 4) ? new Range1D(120f, 300f) : new Range1D(50f, 100f));
			Range1D singleTtlToShow = FXEngine.template_CompressableTrail.SingleTtlToShow;
			FXEngine.template_CompressableTrail.SingleTtlToShow = new Range1D(0f, 0f);
			ParticleSystem3D particleSystem3D = new ParticleSystem3D({23861}, 0f, true, Global.Render.ParticleManager3D, new ParticleEffect3DTemplate[]
			{
				FXEngine.template_CompressableTrail
			});
			if (num != 4)
			{
				FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23861}), 0.5f, 70f, (float)((num == 4) ? 40 : 300), 0.5f, false, FXEngine.PowderParticleType.Dark, 0.7f, null);
				FXEngine.template_PowderSmoke.SingleColor = FXEngine.template_PowderSmoke.SingleColor * 0.6f;
				particleSystem3D.ParticleEffect.Add(FXEngine.template_PowderSmoke);
			}
			particleSystem3D.CountPerSecound = 60f;
			particleSystem3D.UseSubframeAlgorithm = true;
			particleSystem3D.SubframeAlgorithmParticlesPerUnit = 1.5f;
			FXEngine.template_CompressableTrail.SingleTtlToShow = singleTtlToShow;
			return particleSystem3D;
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x000D789C File Offset: 0x000D5A9C
		public static void SampleFiregunEffects(Vector3 {23863}, float {23864}, ClientCannonBall {23865}, Ship {23866})
		{
			Player player = {23866} as Player;
			bool flag;
			if (player != null)
			{
				ShipDesignInfo designElement = player.UsedShipPlayer.GetDesignElement(4);
				short? num = (designElement != null) ? new short?(designElement.ID) : null;
				flag = (((num != null) ? new int?((int)num.GetValueOrDefault()) : null).GetValueOrDefault() == 468);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			Vector3 value = ({23866} == null) ? ({23865}.StartMomentNormal * new Vector3(1f, 0f, 1f)) : new Vector3({23866}.Normal.X, Math.Min(0f, {23865}.StartMomentNormal.Y * 0.5f), {23866}.Normal.Y);
			if ({23866} == null)
			{
				value.Y = -0.05f;
			}
			FXEngine.template_Flame.SingleTexturePath = AtlasObjs.Particles.p_Flame;
			FXEngine.template_Flame.SingleInitialSize.Start = 1.5f;
			FXEngine.template_Flame.SingleInitialSize.End = 2f;
			FXEngine.template_Flame.SingleColor = (flag2 ? new Vector4(0.1f, 0.2f, 1.5f, 0.1f) : new Vector4(0.6f, 0.6f, 0.6f, 0.3f));
			FXEngine.template_Flame.SingleSizeVelocity = 3f;
			FXEngine.template_Flame.SingleTtl = new Range1D(1500f, 1800f);
			FXEngine.template_Flame.SingleTtlToShow = new Range1D(0f, 50f);
			FXEngine.template_Flame.SingleGravityOrFlyVelocityPerSecAddToVelocity = 5f;
			FXEngine.template_Flame.Apply(new ParticleEffectSampleCall({23863}, (Vector3.Up * 0.1f + value) * {23864}, -0.5f));
			if (Rand.Chanse(33f))
			{
				float singleBrightness = FXEngine.template_Flame.SingleBrightness;
				FXEngine.template_Flame.SingleInitialSize.Start = FXEngine.template_Flame.SingleInitialSize.Start + 2f;
				FXEngine.template_Flame.SingleInitialSize.End = FXEngine.template_Flame.SingleInitialSize.End + 2f;
				FXEngine.template_Flame.SingleTexturePath = AtlasObjs.Particles.p_Cloud;
				FXEngine.template_Flame.SingleSizeVelocity = 1f;
				FXEngine.template_Flame.SingleColor = FXEngine.darkParticleColor.ToVector4() * 0.5f + new Vector4(0.05f);
				FXEngine.template_Flame.SingleBrightness = 1f;
				FXEngine.template_Flame.Apply(new ParticleEffectSampleCall({23863}, Vector3.Up * 5f + (Vector3.Up * 0.1f + value) * {23864} / 5f * 4f, -0.5f));
				FXEngine.template_Flame.SingleBrightness = singleBrightness;
				FXEngine.template_Flame.SingleTexturePath = AtlasObjs.Particles.p_Flame;
			}
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x000D7B90 File Offset: 0x000D5D90
		public static ParticleSystem3D CreateTracerForCannonBall(Vector3 {23867}, int {23868}, CannonBallExtraEffects {23869})
		{
			FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23867}), 0.5f, 70f, 250f, 0.5f, false, FXEngine.PowderParticleType.Gray, 0.7f, null);
			ParticleEffect3DTemplate particleEffect3DTemplate = FXEngine.template_PowderSmoke;
			particleEffect3DTemplate.SingleColor *= new Vector4(0.32000002f, 0.32000002f, 0.32000002f, 0.4f) * 0.7f;
			particleEffect3DTemplate.SingleTexturePath = AtlasObjs.Particles.p_SprayCommon;
			FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23867}), 0.4f, 20f, 80f, -0.5f, false, FXEngine.PowderParticleType.Explosion, 0.7f, null);
			ParticleEffect3DTemplate particleEffect3DTemplate2 = FXEngine.template_PowderSmoke;
			if ({23869}.HasFlag(CannonBallExtraEffects.PhosphorBoost))
			{
				FXEngine.mulColor(ref particleEffect3DTemplate2.SingleColor, Color.Lerp(Color.SeaGreen, Color.Black, 0.2f));
				FXEngine.mulColor(ref particleEffect3DTemplate.SingleColor, Color.SeaGreen);
				particleEffect3DTemplate.SingleTexturePath = AtlasObjs.Particles.p_Cloud;
			}
			else if ({23868} == 2 || {23868} == 9)
			{
				FXEngine.mulColor(ref particleEffect3DTemplate2.SingleColor, Color.OrangeRed);
			}
			else if ({23868} == 7)
			{
				FXEngine.mulColor(ref particleEffect3DTemplate2.SingleColor, Color.SkyBlue);
			}
			else
			{
				particleEffect3DTemplate2.SingleColor *= 0.1f;
			}
			if ({23868} == 4)
			{
				particleEffect3DTemplate2.SingleColor *= 0.5f;
				particleEffect3DTemplate.SingleInitialSize = new Range1D(1f, 1.5f);
			}
			ParticleSystem3D particleSystem3D;
			if (Vector3.DistanceSquared({23867}, Engine.GS.Camera.Position) < 25600f)
			{
				particleSystem3D = new ParticleSystem3D({23867}, 0f, true, Global.Render.ParticleManager3D, new ParticleEffect3DTemplate[]
				{
					particleEffect3DTemplate2,
					particleEffect3DTemplate
				});
			}
			else
			{
				particleSystem3D = new ParticleSystem3D({23867}, 0f, true, Global.Render.ParticleManager3D, new ParticleEffect3DTemplate[]
				{
					particleEffect3DTemplate2
				});
			}
			particleSystem3D.CountPerSecound = 1f;
			return particleSystem3D;
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x000D7DA0 File Offset: 0x000D5FA0
		private static void mulColor(ref Vector4 {23870}, Color {23871})
		{
			Vector3 vector = {23871}.ToVector3();
			{23870}.X = {23870}.X * 0.5f + vector.X * 0.5f;
			{23870}.Y = {23870}.Y * 0.5f + vector.Y * 0.5f;
			{23870}.Z = {23870}.Z * 0.5f + vector.Z * 0.5f;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x000D7E14 File Offset: 0x000D6014
		public static void CreateSparkParticle(Vector3 {23872}, Vector3 {23873}, Vector3 {23874})
		{
			float num = FXEngine.DilateSize(0.4f, {23872});
			FXEngine.template_CompressableTrail.SingleInitialSize.Start = num * 0.9f;
			FXEngine.template_CompressableTrail.SingleInitialSize.End = num * 1.1f;
			FXEngine.template_CompressableTrail.SingleColor = Vector4.One * 0.15f;
			FXEngine.template_CompressableTrail.SingleTexturePath = AtlasObjs.Particles.p_SparkRed;
			FXEngine.template_CompressableTrail.SingleTtl = new Range1D(1500f, 2000f);
			FXEngine.template_CompressableTrail.SingleBrightness = 10f;
			FXEngine.template_CompressableTrail.Apply(new ParticleEffectSampleCall({23872}, {23873}, {23874}));
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x000D7EBC File Offset: 0x000D60BC
		public static void SampleFlameAndSmoke(Vector3 {23875}, float {23876}, bool {23877}, bool {23878}, bool {23879}, Vector3? {23880} = null, float {23881} = 1f)
		{
			FXEngine.template_Flame.SingleInitialSize.Start = {23876};
			FXEngine.template_Flame.SingleInitialSize.End = {23876};
			FXEngine.template_Flame.SingleColor = new Vector4(0.6f, 0.6f, 0.6f, 0.3f) * {23881};
			FXEngine.template_Flame.SingleSizeVelocity = -1f;
			FXEngine.template_Flame.SingleTtl = new Range1D(1000f, 1200f);
			FXEngine.template_Flame.SingleTtlToShow = new Range1D(400f, 420f);
			FXEngine.template_Flame.SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f;
			FXEngine.template_Flame.Apply(new ParticleEffectSampleCall({23875}, {23877} ? (Vector3.Up * (1.5f + {23876} * 0.5f)) : (Vector3.Up + {23880}.GetValueOrDefault(Vector3.Zero)), {23877} ? (Vector3.Up * 5f) : (Vector3.Up * 3f)));
			if ({23879} && Vector3.DistanceSquared({23875}, Engine.GS.Camera.Position) < 32400f)
			{
				for (int i = 0; i < 3; i++)
				{
					FXEngine.CreateSparkParticle({23875} + Rand.NextVector3(-1f, 1f) * ({23876} * 0.25f), (Vector3.Up * 2f + Rand.NextVector3(-1f, 1f)) * 5f, (Vector3.Up * 2f + Rand.NextVector3(-1f, 1f)) * 4f);
				}
			}
			if ({23878})
			{
				FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23875} + new Vector3(0f, {23876} / 2f, 0f), Vector3.Up * 3f + {23880}.GetValueOrDefault(Vector3.Zero)), {23876} * 0.5f, 450f, (float)(Rand.Chanse(10f) ? 6000 : 2000), 1f, false, FXEngine.PowderParticleType.Dark, 0.7f, null);
			}
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x000D8108 File Offset: 0x000D6308
		public static void SampleShipSmoke(float {23882}, Ship {23883})
		{
			Vector2 vector;
			vector.X = Rand.Range(-{23883}.UsedShip.StaticInfo.CorpusHalfLength, {23883}.UsedShip.StaticInfo.CorpusHalfLength) * 0.2f;
			vector.Y = Rand.Range(-{23883}.UsedShip.StaticInfo.CorpusHalfWidth, {23883}.UsedShip.StaticInfo.CorpusHalfWidth) * 0.2f;
			vector = Vector2.Transform(vector, Matrix.CreateRotationZ({23883}.Rotation));
			FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23883}.Position3D + new Vector3(vector.X, -2f, vector.Y), Vector3.Up * 3f + {23883}.physicsBody.VelocityPerSec.X0Y() / 60f * 25f), {23883}.UsedShip.StaticInfo.CorpusHalfLength * 0.1f, 450f, 1500f + 3500f * {23882}, 1.5f, false, FXEngine.PowderParticleType.Dark, 0.7f, null);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x000D8230 File Offset: 0x000D6430
		public static void SampleShipBurning(Ship {23884}, float {23885})
		{
			float num = ({23885} < 0.3f) ? ({23885} / 0.3f) : 1f;
			float num2 = ({23885} < 0.5f) ? ({23885} / 0.5f) : 1f;
			float num3 = ({23885} > 0.5f) ? (({23885} - 0.5f) / 0.5f) : 0f;
			Vector3 x0Y = {23884}.physicsBody.VelocityPerSec.X0Y;
			if (!((IClientShip){23884}).GetClient.IsVisible)
			{
				num3 = 0f;
			}
			if (num > 0f)
			{
				foreach (MastHitbox mastHitbox in {23884}.UsedShip.StaticInfo.MastHitboxes)
				{
					Vector3 {23889} = {23884}.Transform.Transform3X3(mastHitbox.Shape.MinP);
					FXEngine.FlareUpPoint(num, 1f, true, {23889}, {23884}, ref x0Y);
				}
			}
			if (num2 > 0f)
			{
				foreach (MastHitbox mastHitbox2 in {23884}.UsedShip.StaticInfo.MastHitboxes)
				{
					for (int j = 0; j < 3; j++)
					{
						Vector3 {23889}2 = {23884}.Transform.Transform3X3(Vector3.Lerp(mastHitbox2.Shape.MinP, mastHitbox2.Shape.MaxP, (float)(j + 1) / 3f));
						FXEngine.FlareUpPoint(num2, 0.5f, true, {23889}2, {23884}, ref x0Y);
					}
				}
			}
			if (num3 > 0f)
			{
				foreach (ValueTuple<Vector3, float> valueTuple in ((IEnumerable<ValueTuple<Vector3, float>>){23884}.UsedShip.StaticInfo.GunneryCrewPositions))
				{
					if (Rand.Chanse(50f))
					{
						Vector3 {23889}3 = {23884}.Transform.Transform3X3(valueTuple.Item1);
						FXEngine.FlareUpPoint(num3, 0.7f, false, {23889}3, {23884}, ref x0Y);
					}
				}
				if (Rand.Chanse(3f))
				{
					FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23884}.Position3D, Vector3.Up * 5f + x0Y), {23884}.UsedShip.StaticInfo.CorpusHalfLength * 1.2f, 450f, 20500f, 5f, false, FXEngine.PowderParticleType.Dark, 0.7f, null);
				}
			}
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x000D8488 File Offset: 0x000D6688
		private static void FlareUpPoint(float {23886}, float {23887}, bool {23888}, Vector3 {23889}, Ship {23890}, ref Vector3 {23891})
		{
			if (Rand.Chanse(7f + {23886} * 4f))
			{
				float num = {23890}.UsedShip.StaticInfo.CorpusHalfLength * 0.5f * (0.5f + 0.5f * {23886});
				FXEngine.SampleFlameAndSmoke({23889}, num * {23887}, true, {23888} && Rand.Chanse({23887} * 100f), {23887} > 0.99f && Rand.Chanse(20f), new Vector3?({23891}), 1f);
			}
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x000D8514 File Offset: 0x000D6714
		public static void CreateMassiveExplosion(Vector3 {23892}, bool {23893}, bool {23894} = true, bool {23895} = false, bool {23896} = true)
		{
			if (Global.Player == null)
			{
				return;
			}
			if ({23896})
			{
				float num = CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, {23892}.X, {23892}.Z);
				{23892}.Y = num + 0.5f;
			}
			if ({23894})
			{
				new ExplosionWaveFlashEffect({23892}, (float)({23893} ? 4 : 1), Color.White * 0.5f, 1f);
				StaticSystem staticSystem = Global.Game.StaticSystem;
				Vector2 vector = {23892}.XZ();
				staticSystem.AddOceanParticle(vector, 16f, false, true);
			}
			FXEngine.TemplateExplosionEffect_lightOnly({23892} + new Vector3(0f, 2f, 0f), 20f, false);
			FXEngine.CreateMassiveExplosionSmokeOnly({23892}, {23893});
			if ({23895})
			{
				for (int i = 0; i < 16; i++)
				{
					Vector3 {24859} = new Vector3(Rand.Range(-1f, 1f), Rand.Range(0.7f, 1.3f), Rand.Range(-1f, 1f));
					{24859}.Normalize();
					Global.Game.WorldInstance.CreateDebris({23892} + Rand.NextVector3(-0.5f, 0.5f), {24859}, HitMaterialEffect.Wood);
				}
			}
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x000D8640 File Offset: 0x000D6840
		public static void CreateMassiveExplosionSmokeOnly(Vector3 {23897}, bool {23898})
		{
			int num = 13;
			for (int i = 0; i < num; i++)
			{
				Vector3 value = Geometry.FastSinCos(6.2831855f / (float)num * (float)i).X0Y();
				FXEngine.GunEffect({23898} ? ({23897} + value * 4f) : {23897}, Rand.Range(0.6f, 1.1f) * value, Vector2.Zero, false, true, 1.3f, null);
			}
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x000D86B0 File Offset: 0x000D68B0
		public static void TowerDestructionFirstEffect(Vector3 {23899})
		{
			FXEngine.CreateMassiveExplosion({23899}, true, true, false, true);
			FXEngine.CreateMassiveExplosion({23899}, true, true, false, true);
			for (int i = 0; i < 10; i++)
			{
				Vector3 value = Rand.NextRadialVector2(3f, 8f).X0Y();
				FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23899} + value, Vector3.Zero), 10f, 500f, 5000f, 3f, false, FXEngine.PowderParticleType.Dark, 0.7f, null);
				FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23899} + value + new Vector3(0f, 5f, 0f), Vector3.Zero), 10f, 2000f, 5000f, 3f, false, FXEngine.PowderParticleType.Dark, 0.7f, null);
				FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall({23899} + value + new Vector3(0f, 10f, 0f), Vector3.Zero), 10f, 4000f, 5000f, 3f, false, FXEngine.PowderParticleType.Dark, 0.7f, null);
			}
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x000D87DC File Offset: 0x000D69DC
		public static void TowerDestruct(IsleInstance {23900})
		{
			FXEngine.TowerDestructionFirstEffect(new Vector3({23900}.GlobalPosition.X, 0f, {23900}.GlobalPosition.Y));
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.AmbientTowerDestruct, 0.03f, 1f);
			Vector3 center = {23900}.ModelGlobalBS.Center;
			if (Vector3.Distance(center, Engine.GS.Camera.Position) < 220f)
			{
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.TowerDestruct, 0.5f * (Engine.GS.Camera.Position + center), 1f, false);
			}
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x000D8888 File Offset: 0x000D6A88
		public static void SampleShipMicroburning([Nullable(2)] Ship {23901}, Tlist<MicroburningEffect> {23902}, Transform3D {23903})
		{
			if ({23901} != null && {23901}.UsedShip.IsInvisibilityBonusEnabled)
			{
				return;
			}
			Vector3 value = ({23901} == null) ? Vector3.Zero : {23901}.physicsBody.VelocityPerSec.X0Y;
			for (int i = 0; i < {23902}.Size; i++)
			{
				Vector3 localPositionClient = {23902}.Array[i].LocalPositionClient;
				if (Rand.Chanse(35f))
				{
					Vector3 vector = {23903}.Transform3X3(localPositionClient * 0.98f);
					if (vector.Y > -1f)
					{
						FXEngine.SampleFlameAndSmoke(vector, 1f, false, Rand.Chanse(20f), false, new Vector3?(value - new Vector3(0f, 0.5f, 0f)), 1f);
					}
				}
			}
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x000D8950 File Offset: 0x000D6B50
		public static void ShipSmallCrewExplosionEffect(Vector3 {23904}, float {23905})
		{
			FXEngine.CreateMassiveExplosion({23904}, false, false, true, true);
			Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.ShipExplosion, {23904}, 1f, false);
			for (int i = 0; i < 8; i++)
			{
				FXEngine.TemplateExplosionEffect_decal(AtlasObjs.Particles.sprite_Explosion, {23904} + Rand.NextVector3(-{23905} * 0.5f, {23905} * 0.5f), Vector3.Zero, {23905} * 0.3f, Vector4.One, 0f, 4.2f);
			}
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000D89CC File Offset: 0x000D6BCC
		public static void ShipDeath(Ship {23906})
		{
			if (!{23906}.UsedShip.FirstHP.DestroyedByFloodingFlags)
			{
				FXEngine.CreateMassiveExplosion({23906}.Position3D, true, true, true, true);
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.ShipExplosion, {23906}.Position3D, 1f, false);
			}
			Global.Game.WorldInstance.CreateFloodingDecoration({23906});
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x000D8A28 File Offset: 0x000D6C28
		public static void CreateCollisionEffect(Ship {23907}, ref SingleShipCollisionData {23908}, bool {23909})
		{
			Vector2 normal = {23907}.Normal;
			Vector3 collisionPoint = {23908}.CollisionPoint;
			FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall(collisionPoint), {23907}.UsedShip.StaticInfo.BSRadius * 0.25f, 400f, 2000f, 1f, false, FXEngine.PowderParticleType.Dark, 0.7f, null);
			Global.Game.SoundSystem.Play3DSound(({23908}.Damage > Global.Player.UsedShip.MaxHp * 0.1f) ? GameDynamicSoundName.WoodImpactLong : GameDynamicSoundName.WoodImpact, collisionPoint, {23908}.SoftCollision ? 0.3f : 0.6f, false);
			if (!{23908}.SoftCollision)
			{
				FXEngine.OnHitDebrisEffectHemisphere(HitMaterialEffect.Wood, collisionPoint, 1f);
			}
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00003100 File Offset: 0x00001300
		public static void ExecuteParticlesSpecialEffect(NpcVisualEffect {23910}, Ship {23911})
		{
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x000D8AE0 File Offset: 0x000D6CE0
		public static void CreateEnvironmentLightParticle(Vector3 {23912})
		{
			ParticleEffect3DTemplate template_EnvLightParticle = FXEngine.Template_EnvLightParticle;
			template_EnvLightParticle.SingleBrightness = 1f;
			template_EnvLightParticle.Apply(new ParticleEffectSampleCall({23912}, Rand.NextVector3(-1f, 1f) * Rand.Range(0.8f, 1.5f)));
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x000D8B30 File Offset: 0x000D6D30
		public static void Template_VolumetricFog_Sample(Vector3 {23913}, float {23914}, float {23915} = 4000f, float {23916} = 10f)
		{
			float num;
			if (!Global.Player.MapInfo.IsWorldmap)
			{
				num = 0f;
			}
			else
			{
				WorldMapInfo mapInfo = Global.Player.MapInfo;
				Vector2 position = Global.Player.Position;
				num = mapInfo.OutsideSeamLevel(position);
			}
			float scaleFactor = num;
			FXEngine.Template_VolumetricFog.SingleTtl = new Range1D({23915} * 0.9f, {23915} * 1.1f);
			FXEngine.Template_VolumetricFog.SingleInitialSize = new Range1D({23916} * 0.8f, {23916} * 1.2f);
			FXEngine.Template_VolumetricFog.SingleColor = new Vector4(Vector3.Lerp(Global.Game.StaticSystem.GetSkyShader.CurrentFogColor, Color.SkyBlue.ToVector3(), 0.4f) * 1.1f, 1f) * {23914};
			Vector3 vector = CommonGlobal.CurrentClientWeather.WindDirection * scaleFactor * 15f;
			FXEngine.Template_VolumetricFog.Apply(new ParticleEffectSampleCall({23913} - vector / 2f, vector));
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x000D8C38 File Offset: 0x000D6E38
		public static void Template_ColoredSmoke(Vector3 {23917}, Vector4 {23918}, float {23919}, float {23920} = 4000f)
		{
			FXEngine.Template_VolumetricFog.SingleTtl = new Range1D({23920} * 0.9f, {23920} * 1.1f);
			FXEngine.Template_VolumetricFog.SingleInitialSize = new Range1D({23919} * 0.8f, {23919} * 1.2f);
			FXEngine.Template_VolumetricFog.SingleColor = {23918};
			FXEngine.Template_VolumetricFog.Apply(new ParticleEffectSampleCall({23917}, Vector3.Zero));
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000D8CA0 File Offset: 0x000D6EA0
		public static void CreateLighting(Vector2 {23921}, bool {23922})
		{
			if (Global.Game.IsActive)
			{
				if ({23922})
				{
					Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.WoodImpact, {23921}.X0Y(), 1f, false);
				}
				Global.Game.SoundSystem.PlaySound(GameStaticSoundName.Thunder, 0.03f, 1f);
			}
			float num = Vector2.Distance({23921}, Engine.GS.Camera.Position.XZ()) / 4f;
			FXEngine.Template_Lighting.SingleTtlToShow = new Range1D(20f, 50f);
			FXEngine.Template_Lighting.SingleInitialSize = new Range1D(30f + num, 30f + num);
			FXEngine.Template_Lighting.Apply(new ParticleEffectSampleCall(new Vector3({23921}.X, 14f + num * 0.5f, {23921}.Y), Vector3.Zero));
			FXEngine.Template_Lighting.SingleTtlToShow = new Range1D(320f, 350f);
			FXEngine.Template_Lighting.Apply(new ParticleEffectSampleCall(new Vector3({23921}.X, 13f + num * 0.5f, {23921}.Y), Vector3.Zero));
			for (int i = 0; i < 5; i++)
			{
				FXEngine.TemplateExplosionEffect_lightOnly(({23921} + Rand.NextVector2(-5f, 5f)).X0Y(), 40f, !{23922});
			}
			for (int j = 0; j < 3; j++)
			{
				FXEngine.TemplateExplosionEffect_lightOnly(({23921} + Rand.NextVector2(-5f, 5f)).X0Y() + new Vector3(0f, FXEngine.Template_Lighting.SingleInitialSize.Start, 0f), 100f, true);
			}
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x000D8E50 File Offset: 0x000D7050
		public static ParticleSystem3D CreateParticleSystemMendingKit(Ship {23923}, Vector3 {23924}, Color {23925}, IParticleTextureSampler {23926})
		{
			FXEngine.template_CompressableTrail.SingleInitialSize.Start = 1f;
			FXEngine.template_CompressableTrail.SingleInitialSize.End = 1f;
			FXEngine.template_CompressableTrail.SingleColor = {23925}.ToVector4();
			FXEngine.template_CompressableTrail.SingleTexturePath = {23926};
			FXEngine.template_CompressableTrail.SingleTtl = new Range1D(1200f, 1400f);
			FXEngine.template_CompressableTrail.SingleBrightness = 2f;
			return new ParticleSystem3D({23924}, {23923}.UsedShip.StaticInfo.BSRadius * 0.2f, false, Global.Render.ParticleManager3D, new ParticleEffect3DTemplate[]
			{
				FXEngine.template_CompressableTrail
			})
			{
				CountPerSecound = 12f
			};
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000D8F0D File Offset: 0x000D710D
		public static void DebugParticleCreate(Vector3 {23927})
		{
			FXEngine.Template_debugParticle.Apply(new ParticleEffectSampleCall({23927}));
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x000D8F20 File Offset: 0x000D7120
		// Note: this type is marked as 'beforefieldinit'.
		static FXEngine()
		{
			ParticleEffect3DTemplate particleEffect3DTemplate = new ParticleEffect3DTemplate
			{
				GeneratorCountPerSample = 1,
				GeneratorRandomOffset = 0.25f,
				SingleCreateRandomInitialAxis = false,
				SingleAngularVelocity = 0f,
				SingleBrightness = 1f,
				SingleColor = Color.White.ToVector4(),
				SingleGravityOrFlyVelocityPerSecAddToVelocity = 3f,
				SingleInitialSize = default(Range1D),
				SingleIsSqueredShow = true,
				SingleIsSqueredHide = true,
				SingleSizeVelocity = 0.5f,
				SingleSizeVelocityChange = -0.3f,
				SingleTexturePath = AtlasObjs.Particles.p_SprayUpper,
				SingleTtl = new Range1D(900f, 1100f),
				SingleTtlToShow = new Range1D(100f, 140f),
				SingleWindFactor = 0.05f
			};
			FXEngine.template_ShipWaterParticle = particleEffect3DTemplate;
			particleEffect3DTemplate = new ParticleEffect3DTemplate
			{
				GeneratorCountPerSample = 1,
				GeneratorRandomOffset = 0f,
				SingleCreateRandomInitialAxis = true,
				SingleAngularVelocity = 0.3f,
				SingleBrightness = 1f,
				SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f,
				SingleInitialSize = default(Range1D),
				SingleIsSqueredShow = true,
				SingleIsSqueredHide = true,
				SingleSizeVelocity = 0.8f,
				SingleSizeVelocityChange = -0.3f,
				SingleTexturePath = AtlasObjs.Particles.p_SprayCommon,
				SingleTtl = new Range1D(2000f, 3000f),
				SingleTtlToShow = new Range1D(100f, 140f),
				SingleColor = Color.White.ToVector4(),
				SingleWindFactor = 0.05f
			};
			FXEngine.template_WaterParticleVSplash = particleEffect3DTemplate;
			particleEffect3DTemplate = new ParticleEffect3DTemplate
			{
				GeneratorCountPerSample = 1,
				GeneratorRandomOffset = 0f,
				SingleBrightness = 1f,
				SingleColor = Vector4.One,
				SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f,
				SingleInitialSize = default(Range1D),
				SingleIsSqueredShow = true,
				SingleSizeVelocityChange = -1f,
				SingleTtl = new Range1D(0f, 0f),
				SingleTtlToShow = new Range1D(100f, 140f),
				SingleWindFactor = 0.2f
			};
			FXEngine.template_ExplosionParticle = particleEffect3DTemplate;
			particleEffect3DTemplate = new ParticleEffect3DTemplate
			{
				GeneratorCountPerSample = 1,
				GeneratorRandomOffset = 0f,
				SingleBrightness = 2.3f,
				SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f,
				SingleInitialSize = default(Range1D),
				SingleIsSqueredShow = false,
				SingleSizeVelocity = 3.7f,
				SingleSizeVelocityChange = -2f,
				SingleTtl = new Range1D(800f, 800f),
				SingleTtlToShow = new Range1D(50f, 50f),
				SingleWindFactor = 0.2f,
				SingleTexturePath = AtlasObjs.Particles.p_ExponentialLight,
				SingleAngularVelocity = 0f,
				SingleCreateRandomInitialAxis = false
			};
			FXEngine.template_ExplosionParticleLightpass = particleEffect3DTemplate;
			particleEffect3DTemplate = new ParticleEffect3DTemplate
			{
				SingleBrightness = 1.1f,
				SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f,
				SingleInitialSize = default(Range1D),
				SingleIsSqueredShow = false,
				SingleSizeVelocityChange = -0.3f,
				SingleTtl = new Range1D(0f, 0f),
				SingleTtlToShow = new Range1D(0f, 0f),
				SingleWindFactor = 0.9f,
				SingleAngularVelocity = 0.14f,
				SingleCreateRandomInitialAxis = true
			};
			FXEngine.template_PowderSmoke = particleEffect3DTemplate;
			particleEffect3DTemplate = new ParticleEffect3DTemplate
			{
				GeneratorCountPerSample = 1,
				GeneratorRandomOffset = 0f,
				SingleBrightness = 1f,
				SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f,
				SingleInitialSize = new Range1D(1f, 1f),
				SingleIsSqueredShow = false,
				SingleSizeVelocity = 2.3f,
				SingleSizeVelocityChange = 0f,
				SingleTtl = new Range1D(1450f, 1450f),
				SingleTtlToShow = new Range1D(150f, 150f),
				SingleWindFactor = 0f,
				SingleTexturePath = AtlasObjs.Particles.p_Circle,
				SingleAngularVelocity = 0f,
				SingleCreateRandomInitialAxis = false
			};
			FXEngine.template_DamageMarker = particleEffect3DTemplate;
			particleEffect3DTemplate = new ParticleEffect3DTemplate
			{
				GeneratorCountPerSample = 1,
				GeneratorRandomOffset = 0f,
				SingleBrightness = 3f,
				SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f,
				SingleInitialSize = new Range1D(0.3f, 0.3f),
				SingleIsSqueredShow = false,
				SingleSizeVelocity = 1.3f,
				SingleSizeVelocityChange = 0f,
				SingleTtl = new Range1D(1250f, 1250f),
				SingleTtlToShow = new Range1D(150f, 150f),
				SingleWindFactor = 0f,
				SingleTexturePath = AtlasObjs.Particles.p_Blood,
				SingleAngularVelocity = 0f,
				SingleCreateRandomInitialAxis = false,
				SingleColor = Vector4.One * 0.3f
			};
			FXEngine.template_CrewBlood = particleEffect3DTemplate;
			particleEffect3DTemplate = new ParticleEffect3DTemplate
			{
				GeneratorCountPerSample = 1,
				GeneratorRandomOffset = 0f,
				SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f,
				SingleInitialSize = default(Range1D),
				SingleIsSqueredShow = true,
				SingleSizeVelocity = -1f,
				SingleSizeVelocityChange = 0f,
				SingleTtlToShow = new Range1D(100f, 140f),
				SingleWindFactor = 0.3f,
				SingleAngularVelocity = 0.4f,
				SingleCreateRandomInitialAxis = true
			};
			FXEngine.template_CompressableTrail = particleEffect3DTemplate;
			particleEffect3DTemplate = new ParticleEffect3DTemplate
			{
				GeneratorCountPerSample = 2,
				GeneratorRandomOffset = 0.25f,
				SingleBrightness = 3.2f,
				SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f,
				SingleInitialSize = default(Range1D),
				SingleIsSqueredShow = false,
				SingleSizeVelocity = -1.3f,
				SingleSizeVelocityChange = 0f,
				SingleTtl = new Range1D(1400f, 1600f),
				SingleTtlToShow = new Range1D(400f, 420f),
				SingleWindFactor = 0.4f,
				SingleAngularVelocity = 0.6f,
				SingleCreateRandomInitialAxis = true,
				SingleTexturePath = AtlasObjs.Particles.p_Flame,
				SingleColor = Vector4.One
			};
			FXEngine.template_Flame = particleEffect3DTemplate;
			particleEffect3DTemplate = new ParticleEffect3DTemplate
			{
				SingleBrightness = 1.1f,
				SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f,
				SingleIsSqueredShow = false,
				SingleSizeVelocityChange = -0.3f,
				SingleTtlToShow = new Range1D(1000f, 1500f),
				SingleWindFactor = 0.5f,
				SingleAngularVelocity = 0.14f,
				SingleCreateRandomInitialAxis = true,
				SingleSizeVelocity = 0f,
				GeneratorRandomOffset = 0f,
				GeneratorCountPerSample = 1,
				SingleTexturePath = AtlasObjs.Particles.p_Cloud
			};
			FXEngine.Template_VolumetricFog = particleEffect3DTemplate;
			particleEffect3DTemplate = new ParticleEffect3DTemplate
			{
				GeneratorCountPerSample = 1,
				GeneratorRandomOffset = 0f,
				SingleBrightness = 2f,
				SingleColor = Vector4.One,
				SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f,
				SingleInitialSize = new Range1D(0.1f, 0.3f),
				SingleIsSqueredShow = false,
				SingleSizeVelocity = 0f,
				SingleSizeVelocityChange = 0f,
				SingleTtl = new Range1D(1400f, 1500f),
				SingleTtlToShow = new Range1D(300f, 300f),
				SingleWindFactor = 5f,
				SingleTexturePath = AtlasObjs.Particles.p_DarkDot,
				SingleAngularVelocity = 0.7f,
				SingleCreateRandomInitialAxis = true
			};
			FXEngine.Template_EnvLightParticle = particleEffect3DTemplate;
			particleEffect3DTemplate = new ParticleEffect3DTemplate
			{
				GeneratorCountPerSample = 1,
				GeneratorRandomOffset = 1f,
				SingleBrightness = 2f,
				SingleColor = Vector4.One,
				SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f,
				SingleIsSqueredShow = true,
				SingleSizeVelocity = 0f,
				SingleSizeVelocityChange = 0f,
				SingleTtl = new Range1D(300f, 400f),
				SingleTtlToShow = new Range1D(20f, 50f),
				SingleWindFactor = 0f,
				SingleTexturePath = AtlasObjs.Particles.p_Lighting,
				SingleAngularVelocity = 0f,
				SingleCreateRandomInitialAxis = false,
				IntialAxisIfRandomDisabled = 0f,
				ReduceColorSec = 0.1f,
				SingleIsSqueredHide = true
			};
			FXEngine.Template_Lighting = particleEffect3DTemplate;
			particleEffect3DTemplate = new ParticleEffect3DTemplate
			{
				GeneratorCountPerSample = 1,
				GeneratorRandomOffset = 0f,
				SingleBrightness = 2f,
				SingleColor = Color.White.ToVector4(),
				SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f,
				SingleInitialSize = new Range1D(5f, 5f),
				SingleIsSqueredShow = false,
				SingleSizeVelocity = 0.7f,
				SingleSizeVelocityChange = 0f,
				SingleTtl = new Range1D(1400f, 1500f),
				SingleTtlToShow = new Range1D(300f, 300f),
				SingleWindFactor = 1f,
				SingleTexturePath = new RandomParticleTextureSet(new Rectangle[]
				{
					new Rectangle(910, 2146, 252, 252)
				}),
				SingleAngularVelocity = 0.7f,
				SingleCreateRandomInitialAxis = true
			};
			FXEngine.Template_gammaPPart = particleEffect3DTemplate;
			particleEffect3DTemplate = new ParticleEffect3DTemplate
			{
				GeneratorCountPerSample = 1,
				GeneratorRandomOffset = 0f,
				SingleBrightness = 1f,
				SingleColor = Color.Red.ToVector4(),
				SingleGravityOrFlyVelocityPerSecAddToVelocity = 0f,
				SingleInitialSize = new Range1D(1f, 1f),
				SingleIsSqueredShow = false,
				SingleSizeVelocity = 0.7f,
				SingleSizeVelocityChange = 0f,
				SingleTtl = new Range1D(5000f, 5000f),
				SingleTtlToShow = new Range1D(0f, 0f),
				SingleWindFactor = 0f,
				SingleTexturePath = new RandomParticleTextureSet(new Rectangle[]
				{
					AtlasObjs.whitepixel_1px
				}),
				SingleAngularVelocity = 0f,
				SingleCreateRandomInitialAxis = true
			};
			FXEngine.Template_debugParticle = particleEffect3DTemplate;
		}

		// Token: 0x0400171D RID: 5917
		private const float warfogParticleTtlSec = 70f;

		// Token: 0x0400171E RID: 5918
		private static FXEngine.WarfogDensityManager warfogHelper = new FXEngine.WarfogDensityManager(13f, 52.5f);

		// Token: 0x0400171F RID: 5919
		public static ParticlePattern2D EntrySceneParticleEffect = new ParticlePattern2D
		{
			LifeTime = new Range1D(1500f, 3000f),
			RotationVelocity = new Range1D(1f, 1f),
			SizeSpeed = new Range1D(0.2f, 0.3f),
			Size = new Range1D(0.2f, 1.4f),
			TexturePath = AtlasGameGui.rect_lightParticle,
			TimeToStartLife = new Range1D(300f, 500f),
			DepthEffect = true
		};

		// Token: 0x04001720 RID: 5920
		public static ParticlePattern2D ArenaJoinGuiParticleEffect = new ParticlePattern2D
		{
			LifeTime = new Range1D(1000f, 1000f),
			OriginOffset = new Range1D(0f, 0f),
			RandomOffsetX = new Range1D(0f, 0f),
			RandomOffsetY = new Range1D(0f, 0f),
			RandomVelocityFactor = 1f,
			RotationVelocity = new Range1D(0f, 0f),
			Size = new Range1D(1f, 3f),
			SizeSpeed = new Range1D(0f, 0f),
			StartRotationAngle = new Range1D(0f, 0f),
			TexturePath = new Rectangle(284, 27, 16, 16),
			TimeToStartLife = new Range1D(1000f, 1300f)
		};

		// Token: 0x04001721 RID: 5921
		public static Range1D EntrySceneParticleXSpeed = new Range1D(6f, 50f);

		// Token: 0x04001722 RID: 5922
		public static Range1D EntrySceneParticleYSpeed = new Range1D(-50f, -6f);

		// Token: 0x04001723 RID: 5923
		private static readonly Color dayDarkParticle = new Color(75, 75, 75, 255);

		// Token: 0x04001724 RID: 5924
		private static readonly Color nightDarkParticle = new Color(45, 45, 45, 255);

		// Token: 0x04001725 RID: 5925
		private static readonly Color[] smokeHues = new Color[]
		{
			Color.Red,
			Color.Violet,
			Color.Pink,
			Color.Brown,
			Color.Orange,
			Color.DarkOrange,
			Color.Cyan,
			Color.DarkCyan,
			Color.DarkRed
		};

		// Token: 0x04001726 RID: 5926
		private static ParticleEffect3DTemplate template_ShipWaterParticle;

		// Token: 0x04001727 RID: 5927
		private static ParticleEffect3DTemplate template_WaterParticleVSplash;

		// Token: 0x04001728 RID: 5928
		private static ParticleEffect3DTemplate template_ExplosionParticle;

		// Token: 0x04001729 RID: 5929
		private static ParticleEffect3DTemplate template_ExplosionParticleLightpass;

		// Token: 0x0400172A RID: 5930
		private static ParticleEffect3DTemplate template_PowderSmoke;

		// Token: 0x0400172B RID: 5931
		private static ParticleEffect3DTemplate template_DamageMarker;

		// Token: 0x0400172C RID: 5932
		private static ParticleEffect3DTemplate template_CrewBlood;

		// Token: 0x0400172D RID: 5933
		private static ParticleEffect3DTemplate template_CompressableTrail;

		// Token: 0x0400172E RID: 5934
		private static ParticleEffect3DTemplate template_Flame;

		// Token: 0x0400172F RID: 5935
		private static ParticleEffect3DTemplate Template_VolumetricFog;

		// Token: 0x04001730 RID: 5936
		private static readonly ParticleEffect3DTemplate Template_EnvLightParticle;

		// Token: 0x04001731 RID: 5937
		private static ParticleEffect3DTemplate Template_Lighting;

		// Token: 0x04001732 RID: 5938
		private static readonly ParticleEffect3DTemplate Template_gammaPPart;

		// Token: 0x04001733 RID: 5939
		private static readonly ParticleEffect3DTemplate Template_debugParticle;

		// Token: 0x02000473 RID: 1139
		private class WarfogDensityManager
		{
			// Token: 0x060018EF RID: 6383 RVA: 0x000D9C79 File Offset: 0x000D7E79
			public WarfogDensityManager(float {23930}, float {23931})
			{
				this.{23933} = new Dictionary<Index2D, Stopwatch>(1000);
				this.{23934} = {23930};
				this.{23935} = {23931};
			}

			// Token: 0x060018F0 RID: 6384 RVA: 0x000D9CA0 File Offset: 0x000D7EA0
			public bool IsAdd(Vector2 {23932})
			{
				Index2D key = new Index2D({23932}, this.{23934});
				Stopwatch stopwatch;
				if (!this.{23933}.TryGetValue(key, out stopwatch))
				{
					this.{23933}.Add(key, Stopwatch.StartNew());
					return true;
				}
				if (stopwatch.Elapsed.TotalSeconds > (double)this.{23935})
				{
					this.{23933}[key].Restart();
					return true;
				}
				return false;
			}

			// Token: 0x04001734 RID: 5940
			private Dictionary<Index2D, Stopwatch> {23933};

			// Token: 0x04001735 RID: 5941
			private float {23934};

			// Token: 0x04001736 RID: 5942
			private float {23935};
		}

		// Token: 0x02000474 RID: 1140
		public enum PowderParticleType
		{
			// Token: 0x04001738 RID: 5944
			Gray,
			// Token: 0x04001739 RID: 5945
			Explosion,
			// Token: 0x0400173A RID: 5946
			Dark,
			// Token: 0x0400173B RID: 5947
			GrayDim
		}
	}
}
