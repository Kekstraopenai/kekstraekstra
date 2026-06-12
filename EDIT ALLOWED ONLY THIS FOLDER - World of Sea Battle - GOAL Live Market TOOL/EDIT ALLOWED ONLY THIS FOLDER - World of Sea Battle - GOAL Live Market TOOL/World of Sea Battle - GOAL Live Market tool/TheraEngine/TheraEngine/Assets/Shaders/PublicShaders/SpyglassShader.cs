using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x02000182 RID: 386
	public class SpyglassShader : Shader
	{
		// Token: 0x06000A18 RID: 2584 RVA: 0x00032458 File Offset: 0x00030658
		public SpyglassShader() : base(Engine.SpyglassEffect)
		{
			this.{15607} = base.GetProperty("sourceMap");
			this.{15608} = base.GetProperty("materialMap");
			this.{15609} = base.GetProperty("sourceMapSize");
			this.{15610} = base.GetPass(0);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000324B0 File Offset: 0x000306B0
		public void ApplyPS(Texture2D {15605}, Texture2D {15606})
		{
			this.{15608}.SetValue({15606});
			this.{15607}.SetValue({15605});
			this.{15609}.SetValue(new Vector2((float){15605}.Width, (float){15605}.Height));
			this.{15610}.Apply();
		}

		// Token: 0x04000781 RID: 1921
		private EffectParameter {15607};

		// Token: 0x04000782 RID: 1922
		private EffectParameter {15608};

		// Token: 0x04000783 RID: 1923
		private EffectParameter {15609};

		// Token: 0x04000784 RID: 1924
		private EffectPass {15610};
	}
}
