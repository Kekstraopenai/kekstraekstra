using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x02000187 RID: 391
	public class VolumetricSightShader
	{
		// Token: 0x06000A28 RID: 2600 RVA: 0x00032D98 File Offset: 0x00030F98
		public VolumetricSightShader()
		{
			this.{15655} = Engine.volumetricSightEffect;
			this.{15656} = this.{15655}.CurrentTechnique.Passes[0];
			this.{15657} = this.{15655}.Parameters["materialTexture"];
			this.{15658} = this.{15655}.Parameters["ViewProj"];
			this.{15659} = this.{15655}.Parameters["World"];
			this.{15660} = this.{15655}.Parameters["gunData"];
			this.{15661} = this.{15655}.Parameters["reductionFactor"];
			this.{15662} = this.{15655}.Parameters["Color"];
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00032E74 File Offset: 0x00031074
		public void SetTexture(Texture2D {15649})
		{
			if ({15649} == null)
			{
				throw new ArgumentNullException("texture");
			}
			this.{15657}.SetValue({15649});
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00032E90 File Offset: 0x00031090
		public void DrawEffect(Matrix {15650}, Matrix {15651}, Vector2 {15652}, float {15653}, Vector4 {15654})
		{
			this.{15658}.SetValue({15650});
			this.{15659}.SetValue({15651});
			this.{15660}.SetValue({15652});
			this.{15661}.SetValue({15653});
			this.{15662}.SetValue({15654});
			this.{15656}.Apply();
		}

		// Token: 0x04000798 RID: 1944
		private Effect {15655};

		// Token: 0x04000799 RID: 1945
		private EffectPass {15656};

		// Token: 0x0400079A RID: 1946
		private EffectParameter {15657};

		// Token: 0x0400079B RID: 1947
		private EffectParameter {15658};

		// Token: 0x0400079C RID: 1948
		private EffectParameter {15659};

		// Token: 0x0400079D RID: 1949
		private EffectParameter {15660};

		// Token: 0x0400079E RID: 1950
		private EffectParameter {15661};

		// Token: 0x0400079F RID: 1951
		private EffectParameter {15662};
	}
}
