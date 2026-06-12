using System;
using Microsoft.Xna.Framework;
using TheraEngine.Helpers;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x0200004D RID: 77
	internal class Particle2D : IPoolObject
	{
		// Token: 0x06000218 RID: 536 RVA: 0x0000C282 File Offset: 0x0000A482
		public void ClearResources()
		{
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000C284 File Offset: 0x0000A484
		public void Initialize(ref ParticlePattern2D {12057}, ref Vector2 {12058}, ref Vector2 {12059}, ref Color {12060})
		{
			this.firstCentrPos = {12058};
			this.firstCentrPos.X = this.firstCentrPos.X + {12057}.RandomOffsetX.Sample;
			this.firstCentrPos.Y = this.firstCentrPos.Y + {12057}.RandomOffsetY.Sample;
			this.firstSize = {12057}.Size.Sample;
			this.{12070} = {12057}.TimeToStartLife.Sample;
			this.{12069} = {12057}.LifeTime.Sample;
			this.{12064} = this.{12069} + this.{12070};
			this.{12066} = {12059};
			if ({12057}.RandomVelocityFactor != 0f)
			{
				Vector2 vector = Geometry.RotateVector2({12059}, Rand.Angle());
				this.{12066}.X = this.{12066}.X * (1f - {12057}.RandomVelocityFactor) + vector.X * {12057}.RandomVelocityFactor;
				this.{12066}.Y = this.{12066}.Y * (1f - {12057}.RandomVelocityFactor) + vector.Y * {12057}.RandomVelocityFactor;
			}
			if ({12057}.DepthEffect)
			{
				this.{12066} *= this.firstSize;
				this.{12065} = MathF.Atan2(this.{12066}.Y, this.{12066}.X) + 1.5707964f;
				this.{12073} = true;
				this.{12072} = {12060} * (0.5f + 0.5f * (1f - this.firstSize / {12057}.Size.End));
			}
			else
			{
				this.{12072} = {12060};
				this.{12065} = {12057}.StartRotationAngle.Sample;
				this.{12073} = false;
				Geometry.AxisNorm(ref this.{12065});
			}
			this.{12067} = {12057}.SizeSpeed.Sample;
			this.{12068} = {12057}.RotationVelocity.Sample;
			this.texturePath = {12057}.TexturePath;
			this.{12071}.X = (float)this.texturePath.Width / 2f + {12057}.OriginOffset.Sample;
			this.{12071}.Y = (float)this.texturePath.Height / 2f + {12057}.OriginOffset.Sample;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		public void Update(ref FrameTime {12061}, out bool {12062})
		{
			this.firstCentrPos.X = this.firstCentrPos.X + this.{12066}.X * {12061}.secElapsed;
			this.firstCentrPos.Y = this.firstCentrPos.Y + this.{12066}.Y * {12061}.secElapsed;
			this.firstSize += this.{12067} * {12061}.secElapsed;
			this.{12064} -= {12061}.msElapsed;
			float num = this.{12068} * {12061}.secElapsed;
			this.{12065} += this.{12068} * {12061}.secElapsed;
			if (this.{12073})
			{
				this.{12066}.X = (float)((double)this.{12066}.X * Math.Cos((double)(-(double)num)) + (double)this.{12066}.Y * Math.Sin((double)(-(double)num)));
				this.{12066}.Y = (float)((double)this.{12066}.Y * Math.Cos((double)(-(double)num)) - (double)this.{12066}.X * Math.Sin((double)(-(double)num)));
			}
			Geometry.AxisNorm(ref this.{12065});
			{12062} = (this.{12064} < 1f);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C60C File Offset: 0x0000A80C
		public void Render(float {12063} = 1f)
		{
			Color color = (this.{12064} < this.{12069}) ? (this.{12072} * ({12063} * Math.Max(this.{12064}, 0f) / this.{12069})) : (this.{12072} * ((1f - (this.{12064} - this.{12069}) / this.{12070}) * {12063}));
			Engine.GS.Draw(this.texturePath, this.firstCentrPos, this.{12071}, this.{12065}, this.firstSize, color);
		}

		// Token: 0x04000163 RID: 355
		public Vector2 firstCentrPos;

		// Token: 0x04000164 RID: 356
		public float firstSize;

		// Token: 0x04000165 RID: 357
		private float {12064};

		// Token: 0x04000166 RID: 358
		private float {12065};

		// Token: 0x04000167 RID: 359
		private Vector2 {12066};

		// Token: 0x04000168 RID: 360
		private float {12067};

		// Token: 0x04000169 RID: 361
		private float {12068};

		// Token: 0x0400016A RID: 362
		public Rectangle texturePath;

		// Token: 0x0400016B RID: 363
		private float {12069};

		// Token: 0x0400016C RID: 364
		private float {12070};

		// Token: 0x0400016D RID: 365
		private Vector2 {12071};

		// Token: 0x0400016E RID: 366
		private Color {12072};

		// Token: 0x0400016F RID: 367
		private bool {12073};
	}
}
