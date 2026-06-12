using System;
using Microsoft.Xna.Framework;
using TheraEngine.Components.Architecture;

namespace TheraEngine.Graphics
{
	// Token: 0x0200013C RID: 316
	public abstract class Camera : IUpdateableObject
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0002BD74 File Offset: 0x00029F74
		public Matrix ProjMatrix
		{
			get
			{
				return this.projMatrix;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0002BD7C File Offset: 0x00029F7C
		public Matrix ViewMatrix
		{
			get
			{
				return this.viewMatrix;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0002BD84 File Offset: 0x00029F84
		// (set) Token: 0x060008F7 RID: 2295 RVA: 0x0002BD8C File Offset: 0x00029F8C
		public Vector3 Rotation
		{
			get
			{
				return this.rotates;
			}
			set
			{
				this.rotates = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x0002BD95 File Offset: 0x00029F95
		public Vector3 Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0002BD9D File Offset: 0x00029F9D
		public Matrix ViewMultiplyProjection
		{
			get
			{
				return this.viewMultiplyProj;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0002BDA5 File Offset: 0x00029FA5
		public float FarPlane
		{
			get
			{
				return this.{14985};
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0002BDAD File Offset: 0x00029FAD
		public float NearPlane
		{
			get
			{
				return this.{14986};
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0002BDB5 File Offset: 0x00029FB5
		public BoundingFrustum BoundingFrustum
		{
			get
			{
				return this.{14987};
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0002BDC0 File Offset: 0x00029FC0
		public Camera(float {14966}, float {14967}, float {14968})
		{
			Camera <>4__this = this;
			Engine.Game.EvSizeChanged += delegate(Vector2 {14988})
			{
				if (<>4__this.{14986} != 0f)
				{
					<>4__this.BuildProjMatrix(<>4__this.{14986}, <>4__this.{14985}, {14968});
				}
			};
			this.BuildProjMatrix({14966}, {14967}, {14968});
		}

		// Token: 0x060008FE RID: 2302
		public abstract void Update(ref FrameTime {14969});

		// Token: 0x060008FF RID: 2303 RVA: 0x0002BE25 File Offset: 0x0002A025
		protected virtual void BuildViewMatrix(Matrix {14970})
		{
			this.viewMatrix = {14970};
			Matrix.Multiply(ref {14970}, ref this.vistestProj, out this.viewMultiplyProj);
			this.{14987} = new BoundingFrustum(this.viewMultiplyProj);
			Matrix.Multiply(ref {14970}, ref this.projMatrix, out this.viewMultiplyProj);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0002BE68 File Offset: 0x0002A068
		protected void BuildProjMatrix(float {14971}, float {14972}, float {14973})
		{
			this.{14985} = {14972};
			this.{14986} = {14971};
			float fieldOfView = MathHelper.ToRadians({14973});
			this.projMatrix = Matrix.CreatePerspectiveFieldOfView(fieldOfView, Engine.Game.GetAspectRatio, this.{14986}, this.{14985});
			this.vistestProj = Matrix.CreatePerspectiveFieldOfView(fieldOfView, Engine.Game.GetAspectRatio, this.{14986}, this.{14985} * 0.9f);
			if (this.viewMatrix != Matrix.Identity)
			{
				this.BuildViewMatrix(this.viewMatrix);
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0002BEF4 File Offset: 0x0002A0F4
		public bool IsVisible(in Vector3 {14974}, float {14975})
		{
			if ({14975} == 0f)
			{
				return this.{14987}.Contains({14974}) > ContainmentType.Disjoint;
			}
			BoundingSphere boundingSphere = new BoundingSphere({14974}, {14975});
			ContainmentType containmentType;
			this.{14987}.Contains(ref boundingSphere, out containmentType);
			return containmentType > ContainmentType.Disjoint;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0002BF40 File Offset: 0x0002A140
		public Vector2 GetProjectionSmoothed(ref Vector3 {14976})
		{
			Vector4 vector = Vector4.Transform({14976}, this.viewMultiplyProj);
			if (vector.W == 0f)
			{
				vector.W = 1E-07f;
			}
			Camera.vector2.X = vector.X / vector.W;
			Camera.vector2.Y = vector.Y / vector.W;
			Vector2 vector2 = Engine.GS.UIArea.HalfWidthHeight();
			Vector2 result;
			result.X = Camera.vector2.X * vector2.X + vector2.X;
			result.Y = -Camera.vector2.Y * vector2.Y + vector2.Y;
			return result;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0002BFF8 File Offset: 0x0002A1F8
		public Vector2 GetProjection(Vector3 {14977}, Matrix {14978})
		{
			Vector4 vector = Vector4.Transform({14977}, {14978});
			if (vector.W == 0f)
			{
				vector.W = 1E-07f;
			}
			Camera.vector2.X = vector.X / vector.W;
			Camera.vector2.Y = vector.Y / vector.W;
			Vector2 vector2 = Engine.GS.UIArea.HalfWidthHeight();
			Vector2 result;
			result.X = (float)((int)Math.Round((double)(Camera.vector2.X * vector2.X))) + vector2.X;
			result.Y = (float)((int)Math.Round((double)(-(double)Camera.vector2.Y * vector2.Y))) + vector2.Y;
			return result;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0002C0B4 File Offset: 0x0002A2B4
		public void GetProjection(ref Vector3 {14979}, out Vector2 {14980})
		{
			Vector4 vector = Vector4.Transform({14979}, this.viewMultiplyProj);
			if (vector.W == 0f)
			{
				vector.W = 1E-07f;
			}
			Camera.vector2.X = vector.X / vector.W;
			Camera.vector2.Y = vector.Y / vector.W;
			Vector2 vector2 = Engine.GS.UIArea.HalfWidthHeight();
			{14980}.X = (float)((int)Math.Round((double)(Camera.vector2.X * vector2.X))) + vector2.X;
			{14980}.Y = (float)((int)Math.Round((double)(-(double)Camera.vector2.Y * vector2.Y))) + vector2.Y;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0002C178 File Offset: 0x0002A378
		public Vector2 GetProjection(ref Vector3 {14981})
		{
			Vector2 result;
			this.GetProjection(ref {14981}, out result);
			return result;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0002C190 File Offset: 0x0002A390
		public Vector2 GetProjection(Vector3 {14982})
		{
			Vector2 result;
			this.GetProjection(ref {14982}, out result);
			return result;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0002C1A8 File Offset: 0x0002A3A8
		public float GetSizeFactor(Vector3 {14983}, float {14984})
		{
			Vector2 projectionSmoothed = this.GetProjectionSmoothed(ref {14983});
			Vector3 vector = this.direction;
			{14983} += Vector3.Transform(Vector3.Right, Matrix.CreateRotationX(this.rotates.X) * Matrix.CreateRotationY(-this.rotates.Y));
			Vector2 projectionSmoothed2 = this.GetProjectionSmoothed(ref {14983});
			float num;
			Vector2.Distance(ref projectionSmoothed, ref projectionSmoothed2, out num);
			return num / {14984};
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0002C214 File Offset: 0x0002A414
		public Matrix CreateRotationMatrix()
		{
			return Matrix.CreateFromYawPitchRoll(-this.rotates.Y - this.AxisOffset.Y, this.rotates.X + this.AxisOffset.X, 0f);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0002C24F File Offset: 0x0002A44F
		public CameraPositionInfo GetCameraPositionInfo()
		{
			return new CameraPositionInfo(this.viewMatrix, this.projMatrix, this.Position, this.{14985});
		}

		// Token: 0x040005FD RID: 1533
		protected readonly Vector3 cameraTarget = new Vector3(0f, 0f, -1f);

		// Token: 0x040005FE RID: 1534
		public Vector3 Position;

		// Token: 0x040005FF RID: 1535
		public Vector3 AxisOffset;

		// Token: 0x04000600 RID: 1536
		private float {14985};

		// Token: 0x04000601 RID: 1537
		private float {14986};

		// Token: 0x04000602 RID: 1538
		protected internal Matrix viewMatrix;

		// Token: 0x04000603 RID: 1539
		protected internal Vector3 rotates;

		// Token: 0x04000604 RID: 1540
		protected Matrix projMatrix;

		// Token: 0x04000605 RID: 1541
		protected Matrix vistestProj;

		// Token: 0x04000606 RID: 1542
		protected Matrix viewMultiplyProj;

		// Token: 0x04000607 RID: 1543
		protected Vector3 direction;

		// Token: 0x04000608 RID: 1544
		private static Vector2 vector2;

		// Token: 0x04000609 RID: 1545
		private BoundingFrustum {14987};
	}
}
