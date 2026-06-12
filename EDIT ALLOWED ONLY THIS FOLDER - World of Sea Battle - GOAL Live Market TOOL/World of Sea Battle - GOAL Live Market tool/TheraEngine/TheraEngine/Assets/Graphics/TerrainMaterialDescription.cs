using System;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x020001AE RID: 430
	public class TerrainMaterialDescription
	{
		// Token: 0x06000ABD RID: 2749 RVA: 0x000360E8 File Offset: 0x000342E8
		public TerrainMaterialDescription(int {16015}, TerrainShadingMode {16016}, VirtualTexture {16017}, VirtualTexture {16018}, VirtualTexture {16019}, VirtualTexture {16020})
		{
			if ({16016} == TerrainShadingMode.TripleTexture && {16020} == null)
			{
				throw new ArgumentNullException("Triple terrain: texture slot (3) not set");
			}
			this.Material = {16017};
			this.ID = {16015};
			this.ShadingMode = {16016};
			this.TextureSlot1 = {16018};
			this.TextureSlot2 = {16019};
			this.TextureSlot3 = {16020};
		}

		// Token: 0x04000861 RID: 2145
		public readonly int ID;

		// Token: 0x04000862 RID: 2146
		public TerrainShadingMode ShadingMode;

		// Token: 0x04000863 RID: 2147
		public VirtualTexture TextureSlot1;

		// Token: 0x04000864 RID: 2148
		public VirtualTexture TextureSlot2;

		// Token: 0x04000865 RID: 2149
		public VirtualTexture TextureSlot3;

		// Token: 0x04000866 RID: 2150
		public VirtualTexture Material;
	}
}
