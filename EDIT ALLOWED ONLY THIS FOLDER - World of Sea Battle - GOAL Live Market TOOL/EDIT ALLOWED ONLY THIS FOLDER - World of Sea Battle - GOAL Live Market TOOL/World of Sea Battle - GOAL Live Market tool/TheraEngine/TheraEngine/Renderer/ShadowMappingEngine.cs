using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Graphics;
using TheraEngine.Scene;

namespace TheraEngine.Renderer
{
	// Token: 0x02000066 RID: 102
	public class ShadowMappingEngine : IGBufferBuilder
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000F251 File Offset: 0x0000D451
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000F259 File Offset: 0x0000D459
		public Matrix LightViewProj { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000F262 File Offset: 0x0000D462
		public bool MaterialAnalyzingEnable
		{
			get
			{
				return this.UseAlphaMaterials;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000F26A File Offset: 0x0000D46A
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000F272 File Offset: 0x0000D472
		public CameraPositionInfo CurrentPassCamera { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000F27B File Offset: 0x0000D47B
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000F283 File Offset: 0x0000D483
		public Vector2 DeformCenter { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000F28C File Offset: 0x0000D48C
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000F294 File Offset: 0x0000D494
		public Matrix TextureScaleBias { get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000F29D File Offset: 0x0000D49D
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000F2A5 File Offset: 0x0000D4A5
		public float MinBias { get; private set; }

		// Token: 0x06000293 RID: 659 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		public ShadowMappingEngine(ShadowMapEngineOptions {12351}, int {12352}, float {12353}, float {12354}, float {12355}, float {12356})
		{
			this.mode = {12351};
			this.shadowNearPlane = {12353};
			this.rendererFarPlane = {12354};
			this.overcomingDistance = {12355};
			this.yPlane = {12356};
			if ({12352} != 0)
			{
				this.SetShadowMapResolution({12352});
			}
			this.{12357}();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000F300 File Offset: 0x0000D500
		private void {12357}()
		{
			this.{12376} = Engine.GBuffer;
			this.{12377} = this.{12376}.Parameters["mViewProj"];
			this.{12378} = this.{12376}.Parameters["mWorld"];
			this.{12379} = this.{12376}.Parameters["clipPlanes"];
			this.{12380} = this.{12376}.Parameters["alphaTexture"];
			this.{12381} = this.{12376}.Parameters["deformCenter"];
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		public void SetShadowMapResolution(int {12358})
		{
			if (this.shadowMapResolution == {12358})
			{
				return;
			}
			this.shadowMapResolution = {12358};
			RenderTarget shadowMap = this.ShadowMap;
			if (shadowMap != null)
			{
				shadowMap.Dispose();
			}
			this.ShadowMap = null;
			if ({12358} != 0)
			{
				this.ShadowMap = new RenderTarget({12358}, {12358}, (this.mode == ShadowMapEngineOptions.Variance) ? SurfaceFormat.Rg32 : SurfaceFormat.HalfSingle, DepthFormat.Depth24, 0, true, "ShadowBuffer", false);
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000F3FE File Offset: 0x0000D5FE
		public void TryCleanBuffer()
		{
			if (this.ShadowMap != null)
			{
				Engine.GS.SetRenderTarget(this.ShadowMap);
				Engine.GS.ClearRenderTargetAndDepthBuffer(ShadowMappingEngine.c_clean_color);
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000F428 File Offset: 0x0000D628
		public virtual void BeginDrawing(Vector3 {12359}, Vector3 {12360}, Vector2 {12361}, Vector3 {12362} = default(Vector3), float {12363} = 1f)
		{
			Vector3 direction = Engine.GS.Camera.Direction;
			float num = MathF.Sqrt(Math.Max(0f, {12360}.Y - this.yPlane)) * 0.5f;
			{12360}.X += direction.X * this.overcomingDistance * num;
			{12360}.Z += direction.Z * this.overcomingDistance * num;
			{12360}.Y = this.yPlane;
			float num2 = this.rendererFarPlane + 50f;
			Vector3 vector = {12360} - {12359} * num2 * 0.5f;
			float num3 = this.rendererFarPlane * 0.8f;
			Matrix matrix = Matrix.CreateLookAt(vector + {12362}, {12360} + {12362}, Vector3.Up);
			Matrix matrix2 = Matrix.CreateOrthographic(num3 * {12363}, num3 * {12363}, this.shadowNearPlane, num2);
			Matrix matrix3 = matrix2;
			Vector3 zero = Vector3.Zero;
			matrix2 = matrix3 * ShadowMappingEngine.ComputeRoundMatrix(matrix, matrix2, zero, this.ShadowMap.Size.X);
			if ({12361}.X != 0f || {12361}.Y != 0f)
			{
				matrix2 *= Matrix.CreateTranslation({12361}.X / this.ShadowMap.Size.X, {12361}.Y / this.ShadowMap.Size.Y, 0f);
			}
			this.LightViewProj = matrix * matrix2;
			this.{12377}.SetValue(this.LightViewProj);
			this.{12379}.SetValue(new Vector2(this.shadowNearPlane, num2));
			this.CurrentPassCamera = new CameraPositionInfo(matrix, matrix2, vector, num2);
			if (this.mode == ShadowMapEngineOptions.Spherical)
			{
				{12360} = Engine.GS.Camera.Position;
				{12360}.X = MathF.Round({12360}.X / 4f) * 4f;
				{12360}.Y = MathF.Round({12360}.Y / 4f) * 4f;
				{12360}.Z = MathF.Round({12360}.Z / 4f) * 4f;
				Vector4 vector2 = Vector4.Transform(new Vector4({12360}, 1f), this.LightViewProj);
				this.DeformCenter = new Vector2(-vector2.X, -vector2.Y);
				this.{12381}.SetValue(this.DeformCenter);
			}
			float num4 = 0.5f + 0.5f / this.ShadowMap.Size.X;
			this.TextureScaleBias = new Matrix(0.5f, 0f, 0f, 0f, 0f, -0.5f, 0f, 0f, 0f, 0f, 0f, 0f, num4, num4, 0f, 1f);
			this.MinBias = MathF.Sqrt(num3) / 27000f * ((num3 > 200f) ? 2f : ((num3 > 20f) ? 1.5f : 2.5f));
			Engine.GS.SetRenderTarget(this.ShadowMap);
			Engine.GS.ClearRenderTargetAndDepthBuffer(ShadowMappingEngine.c_clean_color);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000F764 File Offset: 0x0000D964
		public void ApplyPass(ref Matrix {12364}, bool {12365})
		{
			this.{12378}.SetValue({12364});
			this.{12382} = ({12365} ? 4 : 0) + ((this.mode == ShadowMapEngineOptions.Default) ? 2 : 0);
			this.{12376}.CurrentTechnique.Passes[this.{12382}].Apply();
			this.{12383} = this.{12382};
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		public void RestartPassMaterialAnalyze(Texture2D {12366})
		{
			int num = this.{12382} + (({12366} != null) ? 1 : 0);
			bool flag = {12366} != this.{12380}.GetValueTexture2D();
			this.{12380}.SetValue({12366});
			if (num != this.{12383} || flag)
			{
				this.{12376}.CurrentTechnique.Passes[num].Apply();
				this.{12383} = num;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000F831 File Offset: 0x0000DA31
		public virtual void EndDraw()
		{
			Engine.GS.ReturnRenderTarget();
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000F840 File Offset: 0x0000DA40
		private static Matrix ComputeRoundMatrix(in Matrix {12367}, in Matrix {12368}, in Vector3 {12369}, float {12370})
		{
			Vector3 vector = Vector3.Transform({12369}, {12367});
			vector = Vector3.Transform(vector, {12368});
			Vector2 vector2;
			vector2.X = vector.X * 0.5f + 0.5f;
			vector2.Y = vector.Y * -0.5f + 0.5f;
			Vector2 vector3;
			vector3.X = vector2.X * {12370};
			vector3.Y = vector2.Y * {12370};
			Vector2 vector4;
			vector4.X = MathF.Round(vector3.X);
			vector4.Y = MathF.Round(vector3.Y);
			Vector2 vector5;
			vector5.X = vector3.X - vector4.X;
			vector5.Y = vector3.Y - vector4.Y;
			return Matrix.CreateTranslation((1f - vector5.X) / {12370} * 2f, vector5.Y / {12370} * 2f, 0f);
		}

		// Token: 0x04000220 RID: 544
		private const bool c_map_smoothed = false;

		// Token: 0x04000221 RID: 545
		private const bool c_use_alpha_materials_default = true;

		// Token: 0x04000222 RID: 546
		private const bool c_rounding = true;

		// Token: 0x04000223 RID: 547
		private const float c_rounding_step = 4f;

		// Token: 0x04000224 RID: 548
		private static readonly Color c_clean_color = Color.White;

		// Token: 0x04000225 RID: 549
		public RenderTarget ShadowMap;

		// Token: 0x04000226 RID: 550
		public bool UseAlphaMaterials = true;

		// Token: 0x04000227 RID: 551
		[CompilerGenerated]
		private Matrix {12371};

		// Token: 0x04000228 RID: 552
		[CompilerGenerated]
		private CameraPositionInfo {12372};

		// Token: 0x04000229 RID: 553
		[CompilerGenerated]
		private Vector2 {12373};

		// Token: 0x0400022A RID: 554
		[CompilerGenerated]
		private Matrix {12374};

		// Token: 0x0400022B RID: 555
		[CompilerGenerated]
		private float {12375};

		// Token: 0x0400022C RID: 556
		protected int shadowMapResolution;

		// Token: 0x0400022D RID: 557
		protected readonly float shadowNearPlane;

		// Token: 0x0400022E RID: 558
		protected readonly float rendererFarPlane;

		// Token: 0x0400022F RID: 559
		protected readonly float overcomingDistance;

		// Token: 0x04000230 RID: 560
		protected readonly float yPlane;

		// Token: 0x04000231 RID: 561
		protected ShadowMapEngineOptions mode;

		// Token: 0x04000232 RID: 562
		private Effect {12376};

		// Token: 0x04000233 RID: 563
		private EffectParameter {12377};

		// Token: 0x04000234 RID: 564
		private EffectParameter {12378};

		// Token: 0x04000235 RID: 565
		private EffectParameter {12379};

		// Token: 0x04000236 RID: 566
		private EffectParameter {12380};

		// Token: 0x04000237 RID: 567
		private EffectParameter {12381};

		// Token: 0x04000238 RID: 568
		private int {12382};

		// Token: 0x04000239 RID: 569
		private int {12383};
	}
}
