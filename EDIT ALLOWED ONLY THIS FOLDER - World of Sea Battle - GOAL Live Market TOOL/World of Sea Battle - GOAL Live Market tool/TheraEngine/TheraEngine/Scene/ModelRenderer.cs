using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Graphics;
using TheraEngine.Components;
using TheraEngine.Graphics.Models;
using UWContentPipelineExtensionRuntime.Tags;

namespace TheraEngine.Scene
{
	// Token: 0x02000041 RID: 65
	public class ModelRenderer
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x0000AB1D File Offset: 0x00008D1D
		public ModelRenderer()
		{
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000AB2C File Offset: 0x00008D2C
		public ModelRenderer(UWModel {11939})
		{
			this.Model = {11939};
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000AB44 File Offset: 0x00008D44
		public ModelRenderer(UWModel {11940}, string {11941}, float {11942} = 1f)
		{
			this.Model = {11940};
			if ({11940}.SkinningDataOrNull != null)
			{
				this.Animation = new AnimationUnit({11940}.SkinningDataOrNull);
				this.Animation.StartClip(({11941} == null) ? new List<AnimationClip>({11940}.SkinningDataOrNull.AnimationClips.Values)[0] : {11940}.SkinningDataOrNull.AnimationClips[{11941}], null);
				this.Animation.TimeScale = {11942};
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		public void Render(ISceneObject3DParent {11943})
		{
			if (this.Model.Drawcalls.Length == 0)
			{
				return;
			}
			if (this.Animation != null)
			{
				this.Animation.Update(TimeSpan.FromMilliseconds((double)Engine.Game.GameTime.ElapsedDrawReal), true);
				{11943}.SetAnimationData(this.Animation);
			}
			bool rasterizerStateCullingEnable = Engine.GS.graphicsDevice.RasterizerStateCullingEnable;
			for (int i = 0; i < this.Model.Drawcalls.Length; i++)
			{
				ModelPartShadercall modelPartShadercall = this.Model.Drawcalls[i];
				if ({11943}.SetForPart(modelPartShadercall))
				{
					{11943}.ApplyPass(modelPartShadercall.GeometryType);
					Engine.GS.graphicsDevice.RasterizerStateCullingEnable = (modelPartShadercall.Material.Properties.RasterizerOptions == MaterialRasterizeOptions.SingleSidedHard);
					modelPartShadercall.Render();
				}
			}
			Engine.GS.graphicsDevice.RasterizerStateCullingEnable = rasterizerStateCullingEnable;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000ACA4 File Offset: 0x00008EA4
		public void RenderToGBuffer(IGBufferBuilder {11944}, Func<ModelPartShadercall, bool> {11945} = null)
		{
			if ({11944}.MaterialAnalyzingEnable && this.Model.HaveTransparentMaterials)
			{
				int num = this.Model.Drawcalls.Length;
				for (int i = 0; i < num; i++)
				{
					ModelPartShadercall modelPartShadercall = this.Model.Drawcalls[i];
					if (({11945} == null || {11945}(modelPartShadercall)) && modelPartShadercall.Material.Properties.RasterizerOptions != MaterialRasterizeOptions.AlphaMaterialWithoutShadow)
					{
						if (modelPartShadercall.Material.Albedo == null)
						{
							{11944}.RestartPassMaterialAnalyze(null);
							modelPartShadercall.Render();
						}
						else
						{
							Texture2D tex = modelPartShadercall.Material.Albedo.Tex;
							if (tex.Format == SurfaceFormat.Dxt3 || tex.Format == SurfaceFormat.Dxt5 || tex.Format == SurfaceFormat.Color)
							{
								{11944}.RestartPassMaterialAnalyze(tex);
								modelPartShadercall.Render();
							}
							else
							{
								{11944}.RestartPassMaterialAnalyze(null);
								modelPartShadercall.Render();
							}
						}
					}
				}
				return;
			}
			if (this.Model.HaveDisabledGbufferMaterials || {11945} != null)
			{
				int num2 = this.Model.Drawcalls.Length;
				for (int j = 0; j < num2; j++)
				{
					ModelPartShadercall modelPartShadercall2 = this.Model.Drawcalls[j];
					if (({11945} == null || {11945}(modelPartShadercall2)) && modelPartShadercall2.Material.Properties.RasterizerOptions != MaterialRasterizeOptions.AlphaMaterialWithoutShadow)
					{
						modelPartShadercall2.Render();
					}
				}
				return;
			}
			this.Model.OptimizedRenderAllBuffers();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000ADF4 File Offset: 0x00008FF4
		public void QuickRender(EffectParameter {11946}, EffectPass {11947})
		{
			if ({11946} == null)
			{
				if ({11947} != null)
				{
					{11947}.Apply();
				}
				this.Model.OptimizedRenderAllBuffers();
				return;
			}
			for (int i = 0; i < this.Model.Drawcalls.Length; i++)
			{
				ModelPartShadercall modelPartShadercall = this.Model.Drawcalls[i];
				{11946}.SetValue(modelPartShadercall.Material.Albedo.Tex);
				if ({11947} != null)
				{
					{11947}.Apply();
				}
				modelPartShadercall.Render();
			}
		}

		// Token: 0x04000126 RID: 294
		public object Tag;

		// Token: 0x04000127 RID: 295
		public AnimationUnit Animation;

		// Token: 0x04000128 RID: 296
		public UWModel Model;

		// Token: 0x04000129 RID: 297
		public Transform3D LocalTransformOrNull;

		// Token: 0x0400012A RID: 298
		public object LocalRenderQuery;

		// Token: 0x0400012B RID: 299
		public Matrix? ModelTransformPreview;

		// Token: 0x0400012C RID: 300
		public bool LocalVisible = true;
	}
}
