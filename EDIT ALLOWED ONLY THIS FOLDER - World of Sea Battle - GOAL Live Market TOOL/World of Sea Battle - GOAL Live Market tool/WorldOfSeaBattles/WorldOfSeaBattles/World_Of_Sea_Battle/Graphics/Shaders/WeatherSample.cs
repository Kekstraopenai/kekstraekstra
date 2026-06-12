using System;
using ManualPacketSerialization;
using ManualPacketSerialization.Externs;
using Microsoft.Xna.Framework;

namespace World_Of_Sea_Battle.Graphics.Shaders
{
	// Token: 0x02000467 RID: 1127
	public struct WeatherSample : IMPSerializable
	{
		// Token: 0x06001881 RID: 6273 RVA: 0x00003100 File Offset: 0x00001300
		void IMPSerializable.{23671}(WriterExtern {23672})
		{
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x00003100 File Offset: 0x00001300
		void IMPSerializable.{23673}(WriterExtern {23674})
		{
		}

		// Token: 0x040016E0 RID: 5856
		public Vector2 SamplePoint;

		// Token: 0x040016E1 RID: 5857
		public float IsDay;

		// Token: 0x040016E2 RID: 5858
		public float IsSunrise;

		// Token: 0x040016E3 RID: 5859
		public float FogAmount;

		// Token: 0x040016E4 RID: 5860
		public float DarkSmokeAmount;

		// Token: 0x040016E5 RID: 5861
		public float DownfallAmont;

		// Token: 0x040016E6 RID: 5862
		public float OceanWavesAmount;
	}
}
