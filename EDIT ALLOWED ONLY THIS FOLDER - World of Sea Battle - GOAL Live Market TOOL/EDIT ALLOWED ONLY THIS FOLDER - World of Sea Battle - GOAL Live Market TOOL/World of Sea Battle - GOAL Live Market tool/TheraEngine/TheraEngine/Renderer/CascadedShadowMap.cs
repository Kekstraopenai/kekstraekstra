using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Graphics;

namespace TheraEngine.Renderer
{
	// Token: 0x02000062 RID: 98
	public class CascadedShadowMap
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000F06D File Offset: 0x0000D26D
		public CascadedShadowMap(params ShadowMappingEngine[] {12326})
		{
			this.{12336} = {12326};
			this.{12337} = new DownsampleAndBlurGBuffer();
			this.{12327}();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000F090 File Offset: 0x0000D290
		private void {12327}()
		{
			RenderTarget shadowMap = this.{12336}[0].ShadowMap;
			if (shadowMap == null && this.Target != null)
			{
				this.Target.Dispose();
				this.Target = null;
				return;
			}
			if (shadowMap != null && (this.Target == null || this.Target.Size.X != shadowMap.Size.X))
			{
				RenderTarget target = this.Target;
				if (target != null)
				{
					target.Dispose();
				}
				this.Target = new RenderTarget(shadowMap.Resource.Width, shadowMap.Resource.Height, SurfaceFormat.Rgba64, DepthFormat.None, 0, true, "constructedShadow", false);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000F130 File Offset: 0x0000D330
		public void Construct()
		{
			this.{12327}();
			Engine.GS.SetRenderTarget(this.Target);
			this.{12337}.CombineShadow((from {12339} in this.{12336}
			select {12339}.ShadowMap).ToArray<RenderTarget>());
			ScreenPlaneRenderer.Render();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000F194 File Offset: 0x0000D394
		public void BuildOceanShadowMap(RenderTarget {12328}, RenderTarget {12329}, float {12330}, int {12331}, Vector2 {12332}, float {12333}, bool {12334}, bool {12335})
		{
			ShadowMappingEngine shadowMappingEngine = this.{12336}[{12331}];
			CameraPositionInfo currentPassCamera = shadowMappingEngine.CurrentPassCamera;
			this.{12337}.BuildOceanShadowMap(shadowMappingEngine.ShadowMap, Engine.GS.Camera.ViewMultiplyProjection, currentPassCamera.ViewMultiplyProjMatrix, currentPassCamera.CameraPosition, {12333}, {12335}, {12334});
			if (this.{12338} == null)
			{
				this.{12338} = BillboardParent_VPCT.CreatePlane(10000f, 10000f, {12330});
			}
			this.{12338}.Render();
			if ({12329} != null)
			{
				Engine.GS.SetColorBlendState(BlendState.Opaque);
				Engine.GS.SetRenderTarget({12329});
				this.{12337}.OffsetShadowMapOcean({12328}.Resource, {12332});
			}
		}

		// Token: 0x0400020F RID: 527
		public RenderTarget Target;

		// Token: 0x04000210 RID: 528
		private ShadowMappingEngine[] {12336};

		// Token: 0x04000211 RID: 529
		private DownsampleAndBlurGBuffer {12337};

		// Token: 0x04000212 RID: 530
		private BillboardParent_VPCT {12338};
	}
}
