using System;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Components.Architecture;
using TheraEngine.Helpers;
using TheraEngine.Scene.Lighting;

namespace TheraEngine.Components.Scene
{
	// Token: 0x02000107 RID: 263
	public class SceneManager : IUpdateableObject
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x000265DC File Offset: 0x000247DC
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x00026607 File Offset: 0x00024807
		public float WorldTime
		{
			get
			{
				float? useOverrideTime = this.UseOverrideTime;
				if (useOverrideTime == null)
				{
					return this.{14863};
				}
				return useOverrideTime.GetValueOrDefault();
			}
			set
			{
				this.{14863} = value;
				this.{14858}();
			}
		}

		// Token: 0x17000152 RID: 338
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x00026616 File Offset: 0x00024816
		public float TimeSpeed
		{
			set
			{
				this.{14862} = 60f / (1f / value * 3600f) / 1000f;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x00026637 File Offset: 0x00024837
		public float SunAlpha
		{
			get
			{
				return this.{14864};
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0002663F File Offset: 0x0002483F
		public float MoonAlpha
		{
			get
			{
				return this.{14865};
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00026647 File Offset: 0x00024847
		public SceneColorSet CurrentStyle
		{
			get
			{
				return this.{14867};
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00026650 File Offset: 0x00024850
		public Vector3 CycleLightDirection
		{
			get
			{
				Vector3? overrideLightDirection = this.OverrideLightDirection;
				if (overrideLightDirection == null)
				{
					return this.SunLightSource.LightDirectionForRender;
				}
				return overrideLightDirection.GetValueOrDefault();
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00026680 File Offset: 0x00024880
		public Vector3 CurrentLightDirection
		{
			get
			{
				Vector3? overrideLightDirection = this.OverrideLightDirection;
				if (overrideLightDirection != null)
				{
					return overrideLightDirection.GetValueOrDefault();
				}
				if (this.SunLightSource.LightDirectionForRender.Y >= -0.14f)
				{
					return -this.SunLightSource.LightDirectionForRender;
				}
				return this.SunLightSource.LightDirectionForRender;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x000266D8 File Offset: 0x000248D8
		public Vector3 CurrentLightDirectionStepped
		{
			get
			{
				Vector3? overrideLightDirection = this.OverrideLightDirection;
				if (overrideLightDirection != null)
				{
					return overrideLightDirection.GetValueOrDefault();
				}
				if (this.SunLightSource.LightDirectionForRender.Y >= -0.14f)
				{
					return -this.SunLightSource.LightDirection;
				}
				return this.SunLightSource.LightDirection;
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00026730 File Offset: 0x00024930
		public SceneManager(float {14854}, SceneColorSet {14855}, Func<Vector3> {14856})
		{
			this.{14868} = {14856};
			this.{14867} = {14855};
			this.TimeSpeed = {14854};
			this.MoonLightSource = new AmbientLightSource();
			this.SunLightSource = new AmbientLightSource();
			this.WorldTime = 12f;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00026798 File Offset: 0x00024998
		public void InitializeContent()
		{
			this.MoonLightSource.InitializeVisualization(6f);
			this.SunLightSource.InitializeVisualization(20f);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000267BC File Offset: 0x000249BC
		public void Update(ref FrameTime {14857})
		{
			this.{14863} += {14857}.msElapsed * this.{14862};
			if (this.{14863} > 24f)
			{
				this.{14863} -= 24f;
			}
			if (this.{14867}.FixedVisualTime != null)
			{
				this.{14863} = this.{14867}.FixedVisualTime.Value;
			}
			this.{14858}();
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00026830 File Offset: 0x00024A30
		private void {14858}()
		{
			float num;
			if (this.WorldTime > 6f && this.WorldTime < 21.5f)
			{
				num = -(this.WorldTime - 6f) / 15.5f * 3.1415927f;
			}
			else if (this.WorldTime < 6f)
			{
				num = 3.1415927f - (this.WorldTime / 6f * 6f / 8.5f + 0.29411766f) * 3.1415927f;
			}
			else
			{
				num = 3.1415927f - (this.WorldTime - 21.5f) / 2.5f * 2.5f / 8.5f * 3.1415927f;
			}
			Vector3 vector = -Vector3.Transform(Vector3.Up, Matrix.CreateRotationX((float)(Math.Round((double)(num * 100f)) / 100.0) - 1.5707964f));
			Vector3 vector2 = -Vector3.Transform(Vector3.Up, Matrix.CreateRotationX(num - 1.5707964f));
			vector = this.OverrideLightDirection.GetValueOrDefault(vector);
			vector2 = this.OverrideLightDirection.GetValueOrDefault(vector2);
			vector.Z += 0.5f * vector.Y;
			vector.Normalize();
			vector2.Z += 0.5f * vector2.Y;
			vector2.Normalize();
			this.SunLightSource.LightDirection = vector;
			this.SunLightSource.LightDirectionForRender = vector2;
			this.MoonLightSource.LightDirection = -vector;
			this.MoonLightSource.LightDirectionForRender = -vector2;
			if (this.{14868} != null)
			{
				Vector3 vector3 = -this.SunLightSource.LightDirectionForRender * new Vector3(2f, 1f, 2f);
				vector3.Normalize();
				Vector3 vector4 = this.{14868}();
				Vector3 renderPosition = default(Vector3);
				Vector3.Subtract(ref renderPosition, ref vector3, out renderPosition);
				Vector3.Multiply(ref renderPosition, 100f, out renderPosition);
				Vector3.Add(ref renderPosition, ref vector4, out renderPosition);
				Vector3 renderPosition2 = default(Vector3);
				vector3 = -this.MoonLightSource.LightDirectionForRender * new Vector3(2f, 1f, 2f);
				vector3.Normalize();
				Vector3.Subtract(ref renderPosition2, ref vector3, out renderPosition2);
				Vector3.Multiply(ref renderPosition2, 100f, out renderPosition2);
				Vector3.Add(ref renderPosition2, ref vector4, out renderPosition2);
				this.SunLightSource.RenderPosition = renderPosition;
				this.MoonLightSource.RenderPosition = renderPosition2;
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00026AA4 File Offset: 0x00024CA4
		public void Render(ParticlesAndStaticMesh {14859}, float {14860})
		{
			float amount = Geometry.Saturate((0.2f - this.SunLightSource.LightDirectionForRender.Y) / 0.2f);
			this.{14864} = {14860};
			this.{14865} = Math.Min({14860}, Geometry.Saturate(this.MoonLightSource.LightDirectionForRender.Y / 0.05f));
			if (this.{14864} != 0f)
			{
				Matrix {15449} = Matrix.CreateBillboard(this.SunLightSource.RenderPosition, Engine.GS.Camera.Position, Vector3.Up, new Vector3?(Vector3.Forward));
				{14859}.SetForRender({15449}, this.{14864} * Vector4.Lerp(new Vector4(255f, 255f, 234f, 255f) / 255f, new Vector4(300f, 150f, 150f, 255f) / 255f, amount));
				{14859}.BeginPass(true, false);
				this.SunLightSource.Render();
			}
			if (this.{14865} != 0f)
			{
				Matrix {15449}2 = Matrix.CreateBillboard(this.MoonLightSource.RenderPosition, Engine.GS.Camera.Position, Vector3.Up, new Vector3?(Vector3.Forward));
				{14859}.SetForRender({15449}2, Vector4.One * this.{14865} * new Vector4(1f, 1f, 0.9f, 0.8f));
				{14859}.BeginPass(true, false);
				this.MoonLightSource.Render();
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00026C35 File Offset: 0x00024E35
		public void SetStyle(SceneColorSet {14861})
		{
			if (this.{14867} != {14861})
			{
				this.{14867} = {14861};
				this.{14858}();
			}
		}

		// Token: 0x04000556 RID: 1366
		public const float horizon1 = 6f;

		// Token: 0x04000557 RID: 1367
		public const float horizon2 = 21.5f;

		// Token: 0x04000558 RID: 1368
		public float? UseOverrideTime;

		// Token: 0x04000559 RID: 1369
		public float CurrentCloudyLevel;

		// Token: 0x0400055A RID: 1370
		public float CurrentFogLevel;

		// Token: 0x0400055B RID: 1371
		private float {14862};

		// Token: 0x0400055C RID: 1372
		private float {14863};

		// Token: 0x0400055D RID: 1373
		private float {14864};

		// Token: 0x0400055E RID: 1374
		private float {14865};

		// Token: 0x0400055F RID: 1375
		public readonly AmbientLightSource MoonLightSource;

		// Token: 0x04000560 RID: 1376
		public readonly AmbientLightSource SunLightSource;

		// Token: 0x04000561 RID: 1377
		private Vector3 {14866} = new Vector3(1f, -1f, 1f).Normal();

		// Token: 0x04000562 RID: 1378
		public Vector3? OverrideLightDirection;

		// Token: 0x04000563 RID: 1379
		private SceneColorSet {14867};

		// Token: 0x04000564 RID: 1380
		private readonly Func<Vector3> {14868};
	}
}
