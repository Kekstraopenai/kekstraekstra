using System;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Graphics;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x02000179 RID: 377
	public class OceanNormalMapGenerator : Shader
	{
		// Token: 0x060009ED RID: 2541 RVA: 0x00030867 File Offset: 0x0002EA67
		public OceanNormalMapGenerator() : base(Engine.OceanNormalMapGeneratorEffect)
		{
			this.{15430} = base.GetProperty("perlin");
			this.{15431} = base.GetProperty("time");
			this.{15432} = base.GetPass(0);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000308A3 File Offset: 0x0002EAA3
		public void Generate(Texture2D {15428}, float {15429})
		{
			this.{15430}.SetValue({15428});
			this.{15431}.SetValue({15429});
			ScreenPlaneRenderer.ApplyDefaultVertexShader_ShaderModel3();
			this.{15432}.Apply();
			ScreenPlaneRenderer.Render();
		}

		// Token: 0x0400070C RID: 1804
		private EffectParameter {15430};

		// Token: 0x0400070D RID: 1805
		private EffectParameter {15431};

		// Token: 0x0400070E RID: 1806
		private EffectPass {15432};
	}
}
