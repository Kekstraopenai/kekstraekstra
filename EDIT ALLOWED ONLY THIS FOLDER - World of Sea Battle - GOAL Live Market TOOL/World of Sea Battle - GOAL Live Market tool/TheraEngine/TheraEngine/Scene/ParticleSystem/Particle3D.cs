using System;
using Microsoft.Xna.Framework;
using TheraEngine.Helpers;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x02000055 RID: 85
	internal class Particle3D : IPoolObject
	{
		// Token: 0x06000242 RID: 578 RVA: 0x0000D064 File Offset: 0x0000B264
		public Particle3D()
		{
			this.parent = new VolumtericParticleVertexQuad();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000D077 File Offset: 0x0000B277
		public void ClearResources()
		{
			this.{12160} = false;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000D080 File Offset: 0x0000B280
		public void InternalInit(ref ParticleEffect3DTemplate {12138}, ref ParticleEffectSampleCall {12139}, ref Vector3 {12140})
		{
			this.{12145} = {12138};
			this.{12146} = this.{12145}.SingleTtlToShow.Sample;
			this.{12147} = this.{12145}.SingleTtl.Sample;
			this.my_position = {12140};
			this.my_radius = {12138}.SingleInitialSize.Sample * 1.1f;
			this.{12154} = this.{12147} + this.{12146};
			this.{12153} = this.{12154};
			this.{12155} = (this.{12145}.SingleCreateRandomInitialAxis ? Rand.Angle() : {12138}.IntialAxisIfRandomDisabled);
			this.{12150} = {12139}.SampleVelocityStart;
			this.{12151} = {12139}.SampleVelocityEnd;
			this.{12149} = {12139}.SampleVelocityChangePerSec;
			this.{12156} = {12139}.SampleVelocityStart;
			this.{12157} = {12138}.SingleSizeVelocity;
			this.{12158} = {12138}.SingleAngularVelocity * Rand.Range(0.8f, 1.2f) * (float)Rand.Sign();
			this.{12159} = (this.{12157} > 0f);
			if (!this.{12145}.SingleTexturePath.IsSprite)
			{
				this.{12145}.SingleTexturePath.GetPath(0f, out this.{12148});
			}
			else
			{
				this.{12148}.Width = 0;
			}
			this.parent.SetTracerStretch(ref {12139}.Stretch);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000D1E8 File Offset: 0x0000B3E8
		public void Update(ref FrameTime {12141}, ref Vector3 {12142}, out bool {12143})
		{
			float num = this.{12145}.SingleWindFactor * (1f + this.my_radius * 0.3f);
			this.my_position.X = this.my_position.X + {12142}.X * num;
			this.my_position.Z = this.my_position.Z + {12142}.Z * num;
			this.my_position.X = this.my_position.X + this.{12156}.X * {12141}.secElapsed;
			this.my_position.Y = this.my_position.Y + this.{12156}.Y * {12141}.secElapsed;
			this.my_position.Z = this.my_position.Z + this.{12156}.Z * {12141}.secElapsed;
			if (this.{12149} == 0f)
			{
				Vector3.Lerp(ref this.{12151}, ref this.{12150}, this.{12153} / this.{12154}, out this.{12156});
				this.{12156}.Y = this.{12156}.Y - this.{12145}.SingleGravityOrFlyVelocityPerSecAddToVelocity * {12141}.secElapsed;
			}
			else
			{
				this.{12156}.X = this.{12156}.X + this.{12156}.X * this.{12149} * {12141}.secElapsed;
				this.{12156}.Y = this.{12156}.Y + (this.{12156}.Y * this.{12149} * {12141}.secElapsed - this.{12145}.SingleGravityOrFlyVelocityPerSecAddToVelocity * {12141}.secElapsed);
				this.{12156}.Z = this.{12156}.Z + this.{12156}.Z * this.{12149} * {12141}.secElapsed;
			}
			this.my_radius += this.{12157} * {12141}.secElapsed;
			this.{12157} += this.{12157} * this.{12145}.SingleSizeVelocityChange * {12141}.secElapsed;
			this.{12155} += this.{12158} * {12141}.secElapsed;
			this.{12153} -= {12141}.msElapsed;
			{12143} = (this.{12153} < 0f || this.my_radius <= 0f);
			if (this.{12145}.ReduceColorSec != 0f)
			{
				this.{12145}.SingleColor = this.{12145}.SingleColor * MathHelper.Lerp(1f, MathF.Pow(1f + this.{12145}.ReduceColorSec, 0.016666668f), {12141}.Factor);
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000D470 File Offset: 0x0000B670
		public void PrepareVertices(ref Vector2 {12144})
		{
			if (!this.{12160})
			{
				if (this.{12145}.SingleTexturePath.IsSprite)
				{
					this.{12145}.SingleTexturePath.GetPath(1f - this.{12153} / this.{12154}, out this.{12148});
				}
				this.parent.SetUV(ref this.{12148}, ref {12144});
				this.{12160} = !this.{12145}.SingleTexturePath.IsSprite;
			}
			Vector4 vector;
			if (this.{12153} < this.{12147})
			{
				float num = Math.Max(this.{12153}, 0f) / this.{12147};
				if (this.{12145}.SingleIsSqueredHide)
				{
					num = MathF.Sqrt(num);
				}
				vector = this.{12145}.SingleColor * num;
			}
			else
			{
				float num2 = 1f - (this.{12153} - this.{12147}) / this.{12146};
				if (this.{12145}.SingleIsSqueredShow)
				{
					num2 = MathF.Sqrt(num2);
				}
				vector = this.{12145}.SingleColor * num2;
			}
			Vector3 vector2 = new Vector3(this.my_radius, this.{12155}, this.{12145}.SingleBrightness);
			this.parent.SetData(ref vector, ref this.my_position, ref vector2);
		}

		// Token: 0x04000198 RID: 408
		internal float SortingCachedDistance;

		// Token: 0x04000199 RID: 409
		private ParticleEffect3DTemplate {12145};

		// Token: 0x0400019A RID: 410
		private float {12146};

		// Token: 0x0400019B RID: 411
		private float {12147};

		// Token: 0x0400019C RID: 412
		private Rectangle {12148};

		// Token: 0x0400019D RID: 413
		private float {12149};

		// Token: 0x0400019E RID: 414
		private Vector3 {12150};

		// Token: 0x0400019F RID: 415
		private Vector3 {12151};

		// Token: 0x040001A0 RID: 416
		internal Vector3 my_position;

		// Token: 0x040001A1 RID: 417
		private Vector3 {12152};

		// Token: 0x040001A2 RID: 418
		internal float my_radius;

		// Token: 0x040001A3 RID: 419
		private float {12153};

		// Token: 0x040001A4 RID: 420
		private float {12154};

		// Token: 0x040001A5 RID: 421
		private float {12155};

		// Token: 0x040001A6 RID: 422
		private Vector3 {12156};

		// Token: 0x040001A7 RID: 423
		private float {12157};

		// Token: 0x040001A8 RID: 424
		private float {12158};

		// Token: 0x040001A9 RID: 425
		private bool {12159};

		// Token: 0x040001AA RID: 426
		private bool {12160};

		// Token: 0x040001AB RID: 427
		internal readonly VolumtericParticleVertexQuad parent;

		// Token: 0x040001AC RID: 428
		private static Vector3 v3Up = new Vector3(0f, 1f, 0f);

		// Token: 0x040001AD RID: 429
		private static Vector3 v3Forward = new Vector3(0f, 0f, -1f);
	}
}
