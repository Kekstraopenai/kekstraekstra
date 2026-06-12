using System;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Components.Scene;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x0200017F RID: 383
	public class SkyTextures
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x0003151C File Offset: 0x0002F71C
		public SkyTextures(Texture2D {15527}, Texture2D {15528}, Texture2D {15529}, Texture2D {15530}, Texture2D {15531}, Texture2D {15532}, Texture2D {15533}, Texture2D {15534}, Texture2D {15535}, SceneColorSet {15536}, float {15537}, float {15538})
		{
			this.Night = {15527};
			this.NightMask = {15528};
			this.Sunrise1 = {15529};
			this.Sunrise2 = {15530};
			this.Sunrise2Rev = {15531};
			this.Day = {15532};
			this.StormDecal = {15533};
			this.Style = {15536};
			this.StormDecalMultiplier = {15537};
			this.StormDecalMinimum = {15538};
			this.Stars = {15534};
			this.StartsTurbulence = {15535};
		}

		// Token: 0x04000756 RID: 1878
		public readonly Texture2D Night;

		// Token: 0x04000757 RID: 1879
		public readonly Texture2D NightMask;

		// Token: 0x04000758 RID: 1880
		public readonly Texture2D Sunrise1;

		// Token: 0x04000759 RID: 1881
		public readonly Texture2D Sunrise2;

		// Token: 0x0400075A RID: 1882
		public readonly Texture2D Sunrise2Rev;

		// Token: 0x0400075B RID: 1883
		public readonly Texture2D Day;

		// Token: 0x0400075C RID: 1884
		public readonly Texture2D StormDecal;

		// Token: 0x0400075D RID: 1885
		public readonly Texture2D Stars;

		// Token: 0x0400075E RID: 1886
		public readonly Texture2D StartsTurbulence;

		// Token: 0x0400075F RID: 1887
		public readonly SceneColorSet Style;

		// Token: 0x04000760 RID: 1888
		public readonly float StormDecalMultiplier;

		// Token: 0x04000761 RID: 1889
		public readonly float StormDecalMinimum;
	}
}
