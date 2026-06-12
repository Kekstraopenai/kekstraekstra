using System;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Graphics;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x02000178 RID: 376
	public class HdrColorGradients : Shader
	{
		// Token: 0x060009EA RID: 2538 RVA: 0x00030600 File Offset: 0x0002E800
		public HdrColorGradients() : base(Engine.HdrColorGradiens)
		{
			this.{15420} = base.GetEffectBase.Parameters["texLuma"];
			this.{15421} = base.GetEffectBase.Parameters["texAvgLuma"];
			this.{15422} = base.GetEffectBase.Parameters["texPrevAvgLuma"];
			this.{15423} = base.GetEffectBase.Parameters["backbuffer"];
			this.{15424} = base.GetEffectBase.CurrentTechnique.Passes["Luma"];
			this.{15425} = base.GetEffectBase.CurrentTechnique.Passes["AvgLuma"];
			this.{15426} = base.GetEffectBase.CurrentTechnique.Passes["ColorGradients"];
			this.{15427} = base.GetEffectBase.CurrentTechnique.Passes["PreviousLuma"];
			this.{15417} = new RenderTarget(256, 256, SurfaceFormat.HalfSingle, DepthFormat.None, 0, true, "texLumaRt", true);
			this.{15418} = new RenderTarget(1, 1, SurfaceFormat.HalfSingle, DepthFormat.None, 0, true, "texAvgLumaRt", false);
			this.{15419} = new RenderTarget(1, 1, SurfaceFormat.HalfSingle, DepthFormat.None, 0, true, "texPrevAvgLumaRt", false);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00030754 File Offset: 0x0002E954
		private void {15413}(Texture2D {15414})
		{
			Engine.GS.SetColorBlendState(BlendState.Opaque);
			Engine.GS.SetRenderTarget(this.{15417});
			this.{15423}.SetValue({15414});
			ScreenPlaneRenderer.ApplyDefaultVertexShader_ShaderModel3();
			this.{15424}.Apply();
			ScreenPlaneRenderer.Render();
			Engine.GS.SetRenderTarget(this.{15418});
			this.{15420}.SetValue(this.{15417}.Resource);
			this.{15422}.SetValue(this.{15419}.Resource);
			ScreenPlaneRenderer.ApplyDefaultVertexShader_ShaderModel3();
			this.{15425}.Apply();
			ScreenPlaneRenderer.Render();
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000307F4 File Offset: 0x0002E9F4
		public void Process(Texture2D {15415}, RenderTarget {15416})
		{
			this.{15413}({15415});
			if ({15416} == null)
			{
				Engine.GS.ResetRenderTargets();
			}
			else
			{
				Engine.GS.SetRenderTarget({15416});
			}
			this.{15421}.SetValue(this.{15418}.Resource);
			ScreenPlaneRenderer.ApplyDefaultVertexShader();
			this.{15426}.Apply();
			ScreenPlaneRenderer.Render();
			RenderTarget renderTarget = this.{15418};
			this.{15418} = this.{15419};
			this.{15419} = renderTarget;
		}

		// Token: 0x04000701 RID: 1793
		private RenderTarget {15417};

		// Token: 0x04000702 RID: 1794
		private RenderTarget {15418};

		// Token: 0x04000703 RID: 1795
		private RenderTarget {15419};

		// Token: 0x04000704 RID: 1796
		private EffectParameter {15420};

		// Token: 0x04000705 RID: 1797
		private EffectParameter {15421};

		// Token: 0x04000706 RID: 1798
		private EffectParameter {15422};

		// Token: 0x04000707 RID: 1799
		private EffectParameter {15423};

		// Token: 0x04000708 RID: 1800
		private EffectPass {15424};

		// Token: 0x04000709 RID: 1801
		private EffectPass {15425};

		// Token: 0x0400070A RID: 1802
		private EffectPass {15426};

		// Token: 0x0400070B RID: 1803
		private EffectPass {15427};
	}
}
