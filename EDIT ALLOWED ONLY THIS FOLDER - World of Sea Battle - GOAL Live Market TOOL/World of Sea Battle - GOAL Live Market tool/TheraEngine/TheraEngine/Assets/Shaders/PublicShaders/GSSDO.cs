using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Graphics;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x02000177 RID: 375
	public sealed class GSSDO : AmbientEffect
	{
		// Token: 0x060009E5 RID: 2533 RVA: 0x000301EC File Offset: 0x0002E3EC
		internal GSSDO(Effect {15379}) : base({15379})
		{
			this.{15393} = base.GetProperty("randNormalTexture");
			this.{15394} = base.GetProperty("CamFarPlane");
			this.{15395} = base.GetProperty("CamViewMatrix");
			this.{15396} = base.GetProperty("CamProjMatrix");
			this.{15397} = base.GetProperty("CamPosition");
			this.{15398} = base.GetProperty("CamDirection");
			this.{15399} = base.GetProperty("LightDirection");
			this.Settings = base.GetProperty("Settings");
			this.SettingsW = base.GetProperty("SettingsW");
			this.SettingsRadius = base.GetProperty("SettingsRadius");
			this.SettingsBlur = base.GetProperty("SettingsBlur");
			this.{15400} = base.GetProperty("blend_harmonicsMap");
			this.{15401} = base.GetProperty("blend_harmonicsMapSize");
			this.{15404} = base.GetPass("BuildMap");
			this.{15405} = base.GetPass("BuildMapTxaaOptimized_0");
			this.{15406} = base.GetPass("BuildMapTxaaOptimized_1");
			this.{15407} = base.GetPass("BuildMapTxaaOptimized_2");
			this.{15408} = base.GetPass("BlurWithDataHorizHQ");
			this.{15409} = base.GetPass("BlurWithDataVertHQ");
			this.{15410} = base.GetPass("BlurWithDataHorizLQ");
			this.{15411} = base.GetPass("BlurWithDataVertLQ");
			this.WaterDOPass = base.GetPass("BuildMapWaterDO");
			this.{15402} = base.GetProperty("GBuffer2_DepthAndNormals");
			this.{15403} = base.GetProperty("GBuffer2_DepthAndNormals_Size");
			this.{15393}.SetValue(Engine.RandomNormals);
			base.LessStart = 100f;
			base.LessEnd = 150f;
			this.Quality = SHDAOQuality.HighQuality;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00030431 File Offset: 0x0002E631
		public GSSDO() : this(Engine.GSSDO)
		{
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00030440 File Offset: 0x0002E640
		public void BuildMap(RenderTarget {15380}, CameraPositionInfo {15381}, RenderTarget {15382}, Vector3 {15383}, int {15384} = -1)
		{
			Engine.GS.SetRenderTarget({15380});
			this.{15399}.SetValue({15383});
			this.{15394}.SetValue({15381}.CameraFarPlane);
			this.{15395}.SetValue({15381}.ViewMatrix);
			this.{15396}.SetValue({15381}.ProjMatrix);
			this.{15397}.SetValue({15381}.CameraPosition);
			this.{15398}.SetValue({15381}.ViewDirection);
			this.{15402}.SetValue({15382}.Resource);
			this.{15403}.SetValue({15382}.Size);
			Engine.GS.SetColorBlendState(BlendState.Opaque);
			ScreenPlaneRenderer.ApplyDefaultVertexShader_ShaderModel3();
			if ({15384} != -1)
			{
				if ({15384} == 0)
				{
					this.{15405}.Apply();
				}
				else if ({15384} == 1)
				{
					this.{15406}.Apply();
				}
				else if ({15384} == 2)
				{
					this.{15407}.Apply();
				}
			}
			else
			{
				this.{15404}.Apply();
			}
			ScreenPlaneRenderer.Render();
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0003053C File Offset: 0x0002E73C
		public void PostProcessMap(RenderTarget {15385}, RenderTarget {15386}, bool {15387})
		{
			Engine.GS.SetColorBlendState(BlendState.Opaque);
			this.{15388}({15387} ? this.{15408} : this.{15410}, ref {15385}, ref {15386});
			this.{15388}({15387} ? this.{15409} : this.{15411}, ref {15385}, ref {15386});
			Engine.GS.SetColorBlendState(BlendState.AlphaBlend);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000305A0 File Offset: 0x0002E7A0
		private void {15388}(EffectPass {15389}, ref RenderTarget {15390}, ref RenderTarget {15391})
		{
			RenderTarget renderTarget = {15390};
			RenderTarget renderTarget2 = {15391};
			Engine.GS.SetRenderTarget(renderTarget2);
			this.{15400}.SetValue(renderTarget.Resource);
			this.{15401}.SetValue(renderTarget.Size);
			ScreenPlaneRenderer.ApplyDefaultVertexShader_ShaderModel3();
			{15389}.Apply();
			ScreenPlaneRenderer.Render();
			Engine.GS.ReturnRenderTarget();
			{15391} = renderTarget;
			{15390} = renderTarget2;
		}

		// Token: 0x040006E6 RID: 1766
		private Vector3[] {15392} = new Vector3[]
		{
			new Vector3(1f, 0f, 0f).Normal(),
			new Vector3(0f, 1f, 0f).Normal(),
			new Vector3(0f, 0f, 1f).Normal()
		};

		// Token: 0x040006E7 RID: 1767
		private EffectParameter {15393};

		// Token: 0x040006E8 RID: 1768
		private EffectParameter {15394};

		// Token: 0x040006E9 RID: 1769
		private EffectParameter {15395};

		// Token: 0x040006EA RID: 1770
		private EffectParameter {15396};

		// Token: 0x040006EB RID: 1771
		private EffectParameter {15397};

		// Token: 0x040006EC RID: 1772
		private EffectParameter {15398};

		// Token: 0x040006ED RID: 1773
		private EffectParameter {15399};

		// Token: 0x040006EE RID: 1774
		private EffectParameter {15400};

		// Token: 0x040006EF RID: 1775
		private EffectParameter {15401};

		// Token: 0x040006F0 RID: 1776
		private EffectParameter {15402};

		// Token: 0x040006F1 RID: 1777
		private EffectParameter {15403};

		// Token: 0x040006F2 RID: 1778
		private EffectPass {15404};

		// Token: 0x040006F3 RID: 1779
		private EffectPass {15405};

		// Token: 0x040006F4 RID: 1780
		private EffectPass {15406};

		// Token: 0x040006F5 RID: 1781
		private EffectPass {15407};

		// Token: 0x040006F6 RID: 1782
		private EffectPass {15408};

		// Token: 0x040006F7 RID: 1783
		private EffectPass {15409};

		// Token: 0x040006F8 RID: 1784
		private EffectPass {15410};

		// Token: 0x040006F9 RID: 1785
		private EffectPass {15411};

		// Token: 0x040006FA RID: 1786
		public EffectPass WaterDOPass;

		// Token: 0x040006FB RID: 1787
		public EffectParameter Settings;

		// Token: 0x040006FC RID: 1788
		public EffectParameter SettingsW;

		// Token: 0x040006FD RID: 1789
		public EffectParameter SettingsRadius;

		// Token: 0x040006FE RID: 1790
		public EffectParameter SettingsBlur;

		// Token: 0x040006FF RID: 1791
		public SHDAOQuality Quality;

		// Token: 0x04000700 RID: 1792
		private int {15412};
	}
}
