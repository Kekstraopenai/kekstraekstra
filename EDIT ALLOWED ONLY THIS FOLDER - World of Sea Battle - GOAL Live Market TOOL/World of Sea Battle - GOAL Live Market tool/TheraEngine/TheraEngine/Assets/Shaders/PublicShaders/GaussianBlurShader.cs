using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x02000174 RID: 372
	public class GaussianBlurShader : Shader
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00030089 File Offset: 0x0002E289
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x00030094 File Offset: 0x0002E294
		public GaussianBlurShader.Mode CurrentMode
		{
			get
			{
				return this.{15374};
			}
			set
			{
				if (this.{15374} != value)
				{
					if (value != GaussianBlurShader.Mode.Vertical)
					{
						if (value != GaussianBlurShader.Mode.Horizontal)
						{
							throw new NotSupportedException();
						}
						base.GetEffectBase.CurrentTechnique = this.{15376};
					}
					else
					{
						base.GetEffectBase.CurrentTechnique = this.{15375};
					}
					this.{15374} = value;
				}
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x000300E8 File Offset: 0x0002E2E8
		public GaussianBlurShader() : base(Engine.GaussianEffect)
		{
			this.{15374} = GaussianBlurShader.Mode.Vertical;
			this.{15375} = base.GetEffectBase.Techniques["Vertical"];
			this.{15376} = base.GetEffectBase.Techniques["Horizontal"];
			this.{15377} = base.GetProperty("pixelSize");
			base.GetEffectBase.CurrentTechnique = this.{15375};
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00030160 File Offset: 0x0002E360
		public void SetTextureAndApply(Texture2D {15371}, bool {15372}, float {15373} = 1f)
		{
			this.{15377}.SetValue(new Vector2(1f / (float){15371}.Width * {15373}, 1f / (float){15371}.Height * {15373}));
			Engine.GS.graphicsDevice.SamplerStates.SetState(0, SamplerState.LinearMirror);
			Engine.GS.graphicsDevice.Textures[0] = {15371};
			base.GetEffectBase.CurrentTechnique.Passes[({15372} > false) ? 1 : 0].Apply();
		}

		// Token: 0x040006DB RID: 1755
		private GaussianBlurShader.Mode {15374};

		// Token: 0x040006DC RID: 1756
		private EffectTechnique {15375};

		// Token: 0x040006DD RID: 1757
		private EffectTechnique {15376};

		// Token: 0x040006DE RID: 1758
		private EffectParameter {15377};

		// Token: 0x02000175 RID: 373
		public enum Mode
		{
			// Token: 0x040006E0 RID: 1760
			Vertical,
			// Token: 0x040006E1 RID: 1761
			Horizontal
		}
	}
}
