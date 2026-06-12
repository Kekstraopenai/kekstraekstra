using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace World_Of_Sea_Battle.Graphics.Shaders
{
	// Token: 0x02000469 RID: 1129
	public class SkyAndWeather
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x000D4609 File Offset: 0x000D2809
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x000D4611 File Offset: 0x000D2811
		public float SunsetOrSunriseLevel { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x000D461A File Offset: 0x000D281A
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x000D4622 File Offset: 0x000D2822
		public float IsDay { get; private set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x000D4389 File Offset: 0x000D2589
		public float FogMinLevel
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x000D462B File Offset: 0x000D282B
		public float FogMinDistance
		{
			get
			{
				return this.FogIntensity;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x000D462B File Offset: 0x000D282B
		public float FogMaxDistance
		{
			get
			{
				return this.FogIntensity;
			}
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x000D4633 File Offset: 0x000D2833
		[return: TupleElementNames(new string[]
		{
			"texture",
			"transparancy"
		})]
		public IEnumerable<ValueTuple<Texture2D, float>> GetSkyLayers()
		{
			return new SkyAndWeather.<GetSkyLayers>d__23(-2);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x00003100 File Offset: 0x00001300
		public void DrawSky()
		{
		}

		// Token: 0x040016F2 RID: 5874
		public float DayCycleTime;

		// Token: 0x040016F3 RID: 5875
		public float TimeSpeed;

		// Token: 0x040016F4 RID: 5876
		[CompilerGenerated]
		private float {23685};

		// Token: 0x040016F5 RID: 5877
		[CompilerGenerated]
		private float {23686};

		// Token: 0x040016F6 RID: 5878
		public Vector3 CurrentFogColor;

		// Token: 0x040016F7 RID: 5879
		public Vector3 CurrentDiffuseLightColor;

		// Token: 0x040016F8 RID: 5880
		public Vector4 CurrentSpecularLightColor;

		// Token: 0x040016F9 RID: 5881
		public float WindPower;

		// Token: 0x040016FA RID: 5882
		public float FogIntensity;

		// Token: 0x040016FB RID: 5883
		public float CloudyIntensity;

		// Token: 0x040016FC RID: 5884
		public float RainingIntensity;
	}
}
