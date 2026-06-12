using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Graphics;
using TheraEngine.Helpers;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x02000173 RID: 371
	public class DownsampleAndBlurGBuffer : Shader
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x0002FCCC File Offset: 0x0002DECC
		public DownsampleAndBlurGBuffer() : base(Engine.DownsampleAndBlurGBufferEffect)
		{
			this.{15349} = base.GetProperty("gbufferTexture");
			this.{15350} = base.GetProperty("gbufferTexture1");
			this.{15351} = base.GetProperty("gbufferTexture2");
			this.{15352} = base.GetProperty("OceanViewProjShadow");
			this.{15353} = base.GetProperty("OceanViewProjCamera");
			this.{15354} = base.GetProperty("OceanShadowAmount");
			this.{15357} = base.GetProperty("temporalAAJitter");
			this.{15356} = base.GetProperty("OceanShadowTXAACameraSpeed");
			this.{15355} = base.GetProperty("OceanShadowCameraPos");
			this.{15359} = base.GetProperty("pixelSize");
			this.{15360} = base.GetProperty("textureSize");
			this.{15358} = base.GetProperty("OceanShadowTxaaTex");
			this.{15361} = base.GetProperty("passTransparancyForTxaa");
			this.{15362} = base.GetPass("ColorReplace");
			this.{15363} = base.GetPass("ConstructShadow");
			this.{15364} = base.GetPass("BlurGaussianFirstPass");
			this.{15365} = base.GetPass("BlurGaussianSecoundPass");
			this.{15366} = base.GetPass("BlurPyramidalFirstPass");
			this.{15367} = base.GetPass("BlurPyramidalSecoundPass");
			this.{15368} = base.GetPass("OceanShadowMap");
			this.{15369} = base.GetPass("OceanShadowMap2");
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002FE4C File Offset: 0x0002E04C
		private void {15323}(RenderTarget {15324})
		{
			this.{15349}.SetValue({15324}.Resource);
			this.{15350}.SetValue(null);
			this.{15351}.SetValue(null);
			this.{15359}.SetValue({15324}.PixelSize);
			this.{15360}.SetValue({15324}.Size);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002FEA4 File Offset: 0x0002E0A4
		public void ColorReplace(RenderTarget {15325}, RenderTarget {15326})
		{
			this.{15323}({15325});
			Engine.GS.SetRenderTarget({15326});
			this.{15362}.Apply();
			ScreenPlaneRenderer.Render();
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002FEC8 File Offset: 0x0002E0C8
		internal void ColorReplaceManual(RenderTarget {15327})
		{
			this.{15323}({15327});
			this.{15362}.Apply();
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002FEDC File Offset: 0x0002E0DC
		internal void CombineShadow(params RenderTarget[] {15328})
		{
			this.{15323}({15328}[0]);
			this.{15350}.SetValue({15328}[1].Resource);
			this.{15351}.SetValue({15328}[2].Resource);
			this.{15363}.Apply();
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002FF18 File Offset: 0x0002E118
		public void Blur(RenderTarget {15329}, RenderTarget {15330}, LinearMethodShadowBlurType {15331})
		{
			this.Blur({15329}, {15330}, {15329}, {15331});
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002FF24 File Offset: 0x0002E124
		internal void Blur_FirstPass_UserRender(RenderTarget {15332}, LinearMethodShadowBlurType {15333})
		{
			this.{15323}({15332});
			if ({15333} == LinearMethodShadowBlurType.Gaussian)
			{
				this.{15364}.Apply();
				return;
			}
			if ({15333} != LinearMethodShadowBlurType.Pyramidal)
			{
				throw new NotSupportedException();
			}
			this.{15366}.Apply();
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002FF53 File Offset: 0x0002E153
		internal void Blur_SecoundPass_UserRender(RenderTarget {15334}, LinearMethodShadowBlurType {15335})
		{
			this.{15323}({15334});
			if ({15335} == LinearMethodShadowBlurType.Gaussian)
			{
				this.{15365}.Apply();
				return;
			}
			if ({15335} != LinearMethodShadowBlurType.Pyramidal)
			{
				throw new NotSupportedException();
			}
			this.{15367}.Apply();
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002FF82 File Offset: 0x0002E182
		public void Blur(RenderTarget {15336}, RenderTarget {15337}, RenderTarget {15338}, LinearMethodShadowBlurType {15339})
		{
			Engine.GS.SetRenderTarget({15337});
			this.Blur_FirstPass_UserRender({15336}, {15339});
			ScreenPlaneRenderer.Render();
			Engine.GS.SetRenderTarget({15338});
			this.Blur_SecoundPass_UserRender({15337}, {15339});
			ScreenPlaneRenderer.Render();
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0002FFB8 File Offset: 0x0002E1B8
		public void BuildOceanShadowMap(RenderTarget {15340}, Matrix {15341}, Matrix {15342}, Vector3 {15343}, float {15344}, bool {15345}, bool {15346})
		{
			this.{15352}.SetValue({15342});
			this.{15353}.SetValue({15341});
			this.{15354}.SetValue({15344});
			this.{15361}.SetValue({15345} ? 0.4f : 1f);
			this.{15357}.SetValue({15346} ? (Rand.DiskVector2(0.33f) * 0.0005f) : Vector2.Zero);
			this.{15355}.SetValue({15343});
			this.{15323}({15340});
			this.{15368}.Apply();
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0003004E File Offset: 0x0002E24E
		public void OffsetShadowMapOcean(Texture2D {15347}, Vector2 {15348})
		{
			this.{15358}.SetValue({15347});
			this.{15356}.SetValue({15348});
			ScreenPlaneRenderer.ApplyDefaultVertexShader();
			this.{15369}.Apply();
			ScreenPlaneRenderer.Render();
			this.{15358}.SetValue(null);
		}

		// Token: 0x040006C6 RID: 1734
		private EffectParameter {15349};

		// Token: 0x040006C7 RID: 1735
		private EffectParameter {15350};

		// Token: 0x040006C8 RID: 1736
		private EffectParameter {15351};

		// Token: 0x040006C9 RID: 1737
		private EffectParameter {15352};

		// Token: 0x040006CA RID: 1738
		private EffectParameter {15353};

		// Token: 0x040006CB RID: 1739
		private EffectParameter {15354};

		// Token: 0x040006CC RID: 1740
		private EffectParameter {15355};

		// Token: 0x040006CD RID: 1741
		private EffectParameter {15356};

		// Token: 0x040006CE RID: 1742
		private EffectParameter {15357};

		// Token: 0x040006CF RID: 1743
		private EffectParameter {15358};

		// Token: 0x040006D0 RID: 1744
		private EffectParameter {15359};

		// Token: 0x040006D1 RID: 1745
		private EffectParameter {15360};

		// Token: 0x040006D2 RID: 1746
		private EffectParameter {15361};

		// Token: 0x040006D3 RID: 1747
		private EffectPass {15362};

		// Token: 0x040006D4 RID: 1748
		private EffectPass {15363};

		// Token: 0x040006D5 RID: 1749
		private EffectPass {15364};

		// Token: 0x040006D6 RID: 1750
		private EffectPass {15365};

		// Token: 0x040006D7 RID: 1751
		private EffectPass {15366};

		// Token: 0x040006D8 RID: 1752
		private EffectPass {15367};

		// Token: 0x040006D9 RID: 1753
		private EffectPass {15368};

		// Token: 0x040006DA RID: 1754
		private EffectPass {15369};
	}
}
