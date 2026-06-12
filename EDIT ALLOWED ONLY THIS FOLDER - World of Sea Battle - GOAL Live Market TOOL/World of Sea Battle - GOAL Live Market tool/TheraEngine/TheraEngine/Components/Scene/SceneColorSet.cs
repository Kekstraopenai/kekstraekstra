using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Components.Scene
{
	// Token: 0x02000106 RID: 262
	public class SceneColorSet
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x00025FBF File Offset: 0x000241BF
		private static Vector3 adjust(Vector3 {14844}, float {14845})
		{
			return Vector3.Lerp(new Vector3(Vector3.Dot({14844}, new Vector3(0.3f, 0.59f, 0.11f))), {14844}, {14845});
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00025FE8 File Offset: 0x000241E8
		public static SceneColorSet Interpolate(SceneColorSet {14846}, SceneColorSet {14847}, float {14848})
		{
			return new SceneColorSet
			{
				FogDayColor = Vector3.Lerp({14846}.FogDayColor, {14847}.FogDayColor, {14848}),
				FogDayUpColor = Vector3.Lerp({14846}.FogDayUpColor, {14847}.FogDayUpColor, {14848}),
				FogNightColor = Vector3.Lerp({14846}.FogNightColor, {14847}.FogNightColor, {14848}),
				FogStormColor = Vector3.Lerp({14846}.FogStormColor, {14847}.FogStormColor, {14848}),
				FogSunriseColor = Vector3.Lerp({14846}.FogSunriseColor, {14847}.FogSunriseColor, {14848}),
				SunDiffuseColorDefault = Vector3.Lerp({14846}.SunDiffuseColorDefault, {14847}.SunDiffuseColorDefault, {14848}),
				SunSpecularColorDefault = Vector3.Lerp({14846}.SunSpecularColorDefault, {14847}.SunSpecularColorDefault, {14848}),
				MoonDiffuseColorDefault = Vector3.Lerp({14846}.MoonDiffuseColorDefault, {14847}.MoonDiffuseColorDefault, {14848}),
				MoonSpecularColorDefault = Vector3.Lerp({14846}.MoonSpecularColorDefault, {14847}.MoonSpecularColorDefault, {14848})
			};
		}

		// Token: 0x04000548 RID: 1352
		public static SceneColorSet DarkMountains = new SceneColorSet
		{
			FogDayColor = new Vector3(0.38f, 0.5f, 0.54f),
			FogDayUpColor = new Vector3(0.38f, 0.5f, 0.54f),
			FogNightColor = Vector3.Zero,
			FogStormColor = new Vector3(0.38f, 0.5f, 0.54f),
			FogSunriseColor = Vector3.Zero,
			SunDiffuseColorDefault = new Vector3(195f, 210f, 250f) * 0.5f / 255f,
			SunSpecularColorDefault = new Vector3(255f, 255f, 190f) * 0.3f / 255f,
			MoonDiffuseColorDefault = Vector3.Zero,
			MoonSpecularColorDefault = Vector3.Zero,
			FixedVisualTime = new float?((float)13)
		};

		// Token: 0x04000549 RID: 1353
		public static SceneColorSet Normal = new SceneColorSet
		{
			FogDayColor = new Vector3(0.53f, 0.737f, 0.908f) * 0.9f,
			FogDayUpColor = new Vector3(0.46f, 0.67f, 0.81f),
			FogNightColor = new Vector3(0.09f, 0.182f, 0.28f),
			FogStormColor = new Vector3(0.115f, 0.195f, 0.286f),
			FogSunriseColor = new Vector3(0.274f, 0.317f, 0.378f),
			SunDiffuseColorDefault = new Vector3(260f, 245f, 225f) / 255f * 0.9f,
			SunSpecularColorDefault = new Vector3(255f, 255f, 190f) / 255f,
			MoonDiffuseColorDefault = new Vector3(154f, 145f, 164f) / 255f * 0.8f,
			MoonSpecularColorDefault = new Vector3(156f, 167f, 173f) / 155f
		};

		// Token: 0x0400054A RID: 1354
		public static SceneColorSet Winter = new SceneColorSet
		{
			FogDayColor = new Vector3(0.53f, 0.737f, 0.908f) * 0.88f,
			FogDayUpColor = new Vector3(0.46f, 0.67f, 0.81f) * 0.9f,
			FogNightColor = new Vector3(0.09f, 0.182f, 0.28f) * 1.1f,
			FogStormColor = new Vector3(0.7f, 0.737f, 0.9f) * 0.6f,
			FogSunriseColor = new Vector3(0.274f, 0.317f, 0.378f),
			SunDiffuseColorDefault = new Vector3(260f, 245f, 225f) / 255f * 0.9f,
			SunSpecularColorDefault = new Vector3(255f, 255f, 190f) / 255f,
			MoonDiffuseColorDefault = new Vector3(154f, 145f, 164f) / 255f * 0.8f,
			MoonSpecularColorDefault = new Vector3(156f, 167f, 173f) / 155f
		};

		// Token: 0x0400054B RID: 1355
		public static SceneColorSet Atmospheric = new SceneColorSet
		{
			FogDayColor = new Vector3(0.62f, 0.66f, 0.64f),
			FogDayUpColor = new Vector3(0.62f, 0.66f, 0.64f),
			FogNightColor = new Vector3(0.09f, 0.182f, 0.28f) * new Vector3(1.1f, 0.9f, 1f),
			FogStormColor = new Vector3(0.195f, 0.26f, 0.298f) * new Vector3(1.1f, 0.9f, 1f),
			FogSunriseColor = new Vector3(0.274f, 0.317f, 0.378f) * new Vector3(1.1f, 0.9f, 1f),
			SunDiffuseColorDefault = new Vector3(260f, 245f, 225f) / 255f * 0.8f,
			SunSpecularColorDefault = new Vector3(255f, 255f, 190f) / 255f,
			MoonDiffuseColorDefault = new Vector3(154f, 145f, 164f) / 255f * 0.8f,
			MoonSpecularColorDefault = new Vector3(156f, 167f, 173f) / 155f
		};

		// Token: 0x0400054C RID: 1356
		public Vector3 FogDayUpColor;

		// Token: 0x0400054D RID: 1357
		public Vector3 FogDayColor;

		// Token: 0x0400054E RID: 1358
		public Vector3 FogNightColor;

		// Token: 0x0400054F RID: 1359
		public Vector3 FogStormColor;

		// Token: 0x04000550 RID: 1360
		public Vector3 FogSunriseColor;

		// Token: 0x04000551 RID: 1361
		public Vector3 SunDiffuseColorDefault;

		// Token: 0x04000552 RID: 1362
		public Vector3 MoonDiffuseColorDefault;

		// Token: 0x04000553 RID: 1363
		public Vector3 SunSpecularColorDefault;

		// Token: 0x04000554 RID: 1364
		public Vector3 MoonSpecularColorDefault;

		// Token: 0x04000555 RID: 1365
		public float? FixedVisualTime;
	}
}
