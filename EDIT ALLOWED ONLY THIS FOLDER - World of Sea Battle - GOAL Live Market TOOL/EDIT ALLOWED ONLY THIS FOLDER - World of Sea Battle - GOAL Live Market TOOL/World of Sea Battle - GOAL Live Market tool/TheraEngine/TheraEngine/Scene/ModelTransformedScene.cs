using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Graphics;
using TheraEngine.Graphics.Models;

namespace TheraEngine.Scene
{
	// Token: 0x02000043 RID: 67
	public class ModelTransformedScene
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000AE64 File Offset: 0x00009064
		public Tlist<ModelRenderer> GetModels
		{
			get
			{
				return this.Models;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000AE6C File Offset: 0x0000906C
		public bool HaveAnimationsApprox
		{
			get
			{
				return this.Models.Size >= 1 && this.Models.Array[0].Animation != null;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000AE93 File Offset: 0x00009093
		public bool IsMainCameraVisible
		{
			get
			{
				if (this.VisibleTestType == ModelSceneVisibleTest.Disable)
				{
					throw new InvalidOperationException("VisibleTestType set as disable");
				}
				return this.{11976};
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000AEAE File Offset: 0x000090AE
		public float SingleSize
		{
			get
			{
				return this.{11977}.Radius * this.Transform.MiddleScale;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000AEC7 File Offset: 0x000090C7
		public BoundingSphere CombinedModelSpaceBS
		{
			get
			{
				return this.{11977};
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000AECF File Offset: 0x000090CF
		public int CountModels
		{
			get
			{
				return this.Models.Size;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000AEDC File Offset: 0x000090DC
		public bool VisibleForAnyGBuffer
		{
			get
			{
				return this.{11975}.Elapsed.TotalMilliseconds < 60.0;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000AF07 File Offset: 0x00009107
		public ModelTransformedScene()
		{
			this.Models = new Tlist<ModelRenderer>(10);
			this.Transform = new Transform3D();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000AF39 File Offset: 0x00009139
		public ModelTransformedScene(ModelRenderer {11952}, Transform3D {11953}) : this()
		{
			this.AddObject({11952}, true);
			this.Transform = {11953};
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000AF50 File Offset: 0x00009150
		public ModelTransformedScene(UWModel {11954}, Transform3D {11955}) : this()
		{
			this.AddObject({11954});
			this.Transform = {11955};
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000AF68 File Offset: 0x00009168
		public ModelRenderer AddObject(UWModel {11956})
		{
			ModelRenderer modelRenderer = new ModelRenderer({11956});
			this.AddObject(modelRenderer, true);
			return modelRenderer;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000AF88 File Offset: 0x00009188
		public void AddObject(Tlist<UWModel> {11957})
		{
			for (int i = 0; i < {11957}.Size; i++)
			{
				this.AddObject(new ModelRenderer({11957}.Array[i]), true);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000AFBC File Offset: 0x000091BC
		public void AddObject(UWModel[] {11958})
		{
			for (int i = 0; i < {11958}.Length; i++)
			{
				this.AddObject(new ModelRenderer({11958}[i]), true);
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000AFE6 File Offset: 0x000091E6
		public void AddObject(ModelRenderer {11959}, bool {11960} = true)
		{
			if ({11960})
			{
				this.{11977} = BoundingSphere.CreateMerged(this.{11977}, {11959}.Model.CommonSphere);
			}
			this.Models.Add({11959});
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B014 File Offset: 0x00009214
		public void SetModelData(int {11961}, UWModel {11962})
		{
			this.Models.Array[{11961}].Model = {11962};
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000B02C File Offset: 0x0000922C
		public ModelRenderer GetByTag(object {11963})
		{
			foreach (ModelRenderer modelRenderer in ((IEnumerable<ModelRenderer>)this.Models))
			{
				if (modelRenderer.Tag != null && modelRenderer.Tag.Equals({11963}))
				{
					return modelRenderer;
				}
			}
			return null;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000B090 File Offset: 0x00009290
		public void Remove(ModelRenderer {11964})
		{
			this.Models.FastRemove({11964});
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000B0A0 File Offset: 0x000092A0
		public void Clear()
		{
			this.Models.Clear();
			this.{11977} = default(BoundingSphere);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000B0BC File Offset: 0x000092BC
		public void UpdateMainCameraVisibility()
		{
			Vector3 vector;
			this.Transform.Transform3X3(ref this.{11977}.Center, out vector);
			this.{11976} = Engine.GS.Camera.IsVisible(vector, this.SingleSize);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000B100 File Offset: 0x00009300
		public void UpdateMainCameraVisibility(ref Matrix {11965})
		{
			Vector3 vector;
			Vector3.Transform(ref this.{11977}.Center, ref {11965}, out vector);
			this.{11976} = Engine.GS.Camera.IsVisible(vector, this.SingleSize);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000B140 File Offset: 0x00009340
		public void Render(ISceneObject3DParent {11966})
		{
			if (this.Models.Size == 0)
			{
				return;
			}
			this.Transform.CreateWorldMatrix(out ModelTransformedScene.worldB);
			if (this.VisibleTestType != ModelSceneVisibleTest.Disable)
			{
				Camera camera = Engine.GS.Camera;
				Vector3 vector = Vector3.Transform(this.{11977}.Center, ModelTransformedScene.worldB);
				this.{11976} = camera.IsVisible(vector, this.SingleSize);
				if (!this.{11976})
				{
					return;
				}
			}
			bool flag = false;
			for (int i = 0; i < this.Models.Size; i++)
			{
				ModelRenderer modelRenderer = this.Models.Array[i];
				if (modelRenderer.LocalVisible)
				{
					if (modelRenderer.LocalTransformOrNull != null)
					{
						Matrix matrix = modelRenderer.LocalTransformOrNull.CreateWorldMatrix() * ModelTransformedScene.worldB;
						if (modelRenderer.ModelTransformPreview != null)
						{
							matrix = modelRenderer.ModelTransformPreview.Value * matrix;
						}
						{11966}.SetWorld(ref matrix, modelRenderer, this);
						flag = false;
					}
					else
					{
						if (!flag || modelRenderer.LocalRenderQuery != null)
						{
							{11966}.SetWorld(ref ModelTransformedScene.worldB, modelRenderer, this);
						}
						flag = true;
					}
					modelRenderer.Render({11966});
				}
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000B254 File Offset: 0x00009454
		public bool CheckVisibilityAndRenderGBuffer(IGBufferBuilder {11967}, Func<ModelPartShadercall, bool> {11968} = null)
		{
			if (this.VisibleTestType == ModelSceneVisibleTest.Disable)
			{
				this.RenderToGBuffer({11967}, {11968});
				return true;
			}
			if (!this.{11976})
			{
				return false;
			}
			Matrix matrix = this.Transform.CreateWorldMatrix();
			BoundingSphere boundingSphere;
			this.{11977}.Transform(ref matrix, out boundingSphere);
			if (Vector3.DistanceSquared(boundingSphere.Center, {11967}.CurrentPassCamera.CameraPosition) < ({11967}.CurrentPassCamera.CameraFarPlane + boundingSphere.Radius * 0.7f) * ({11967}.CurrentPassCamera.CameraFarPlane + boundingSphere.Radius * 0.7f))
			{
				ContainmentType containmentType;
				{11967}.CurrentPassCamera.frustum.Contains(ref boundingSphere, out containmentType);
				if (containmentType != ContainmentType.Disjoint)
				{
					this.RenderToGBuffer({11967}, {11968});
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000B304 File Offset: 0x00009504
		public void RenderToGBuffer(IGBufferBuilder {11969}, Func<ModelPartShadercall, bool> {11970} = null)
		{
			this.{11975}.Restart();
			if (this.Models.Size == 0)
			{
				return;
			}
			this.Transform.CreateWorldMatrix(out ModelTransformedScene.worldB);
			bool flag = false;
			for (int i = 0; i < this.Models.Size; i++)
			{
				ModelRenderer modelRenderer = this.Models.Array[i];
				if (modelRenderer.LocalVisible)
				{
					if (modelRenderer.LocalTransformOrNull != null)
					{
						modelRenderer.LocalTransformOrNull.CreateWorldMatrix(out ModelTransformedScene.worldC);
						Matrix.Multiply(ref ModelTransformedScene.worldC, ref ModelTransformedScene.worldB, out ModelTransformedScene.worldC);
						if (modelRenderer.ModelTransformPreview != null)
						{
							ModelTransformedScene.worldC = modelRenderer.ModelTransformPreview.Value * ModelTransformedScene.worldC;
						}
						{11969}.ApplyPass(ref ModelTransformedScene.worldC, false);
						flag = false;
					}
					else
					{
						if (!flag || modelRenderer.LocalRenderQuery != null)
						{
							{11969}.ApplyPass(ref ModelTransformedScene.worldB, false);
						}
						flag = true;
					}
					modelRenderer.RenderToGBuffer({11969}, {11970});
				}
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000B3F8 File Offset: 0x000095F8
		public void RenderWithShader(EffectParameter {11971}, EffectParameter {11972}, EffectParameter {11973}, EffectPass {11974})
		{
			this.Transform.CreateWorldMatrix(out ModelTransformedScene.worldB);
			bool flag = false;
			for (int i = 0; i < this.Models.Size; i++)
			{
				ModelRenderer modelRenderer = this.Models.Array[i];
				if (modelRenderer.LocalVisible)
				{
					if ({11971} != null)
					{
						if (modelRenderer.LocalTransformOrNull != null)
						{
							Matrix matrix = modelRenderer.LocalTransformOrNull.CreateWorldMatrix() * ModelTransformedScene.worldB;
							if (modelRenderer.ModelTransformPreview != null)
							{
								matrix = modelRenderer.ModelTransformPreview.Value * matrix;
							}
							{11971}.SetValue(matrix);
							if ({11972} != null)
							{
								Matrix value;
								Matrix.Invert(ref matrix, out value);
								Matrix.Transpose(ref value, out value);
								{11972}.SetValue(value);
							}
							flag = false;
						}
						else
						{
							if (!flag)
							{
								{11971}.SetValue(ModelTransformedScene.worldB);
								if ({11972} != null)
								{
									Matrix value2;
									Matrix.Invert(ref ModelTransformedScene.worldB, out value2);
									Matrix.Transpose(ref value2, out value2);
									{11972}.SetValue(value2);
								}
							}
							flag = true;
						}
					}
					modelRenderer.QuickRender({11973}, {11974});
				}
			}
		}

		// Token: 0x04000130 RID: 304
		public object Tag;

		// Token: 0x04000131 RID: 305
		private static Matrix worldB;

		// Token: 0x04000132 RID: 306
		private static Matrix worldC;

		// Token: 0x04000133 RID: 307
		public Transform3D Transform;

		// Token: 0x04000134 RID: 308
		protected Tlist<ModelRenderer> Models;

		// Token: 0x04000135 RID: 309
		public ModelSceneVisibleTest VisibleTestType = ModelSceneVisibleTest.ForAllScene;

		// Token: 0x04000136 RID: 310
		private Stopwatch {11975} = Stopwatch.StartNew();

		// Token: 0x04000137 RID: 311
		private bool {11976};

		// Token: 0x04000138 RID: 312
		private BoundingSphere {11977};
	}
}
