using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x0200004F RID: 79
	public struct ParticlePattern2D
	{
		// Token: 0x04000175 RID: 373
		public Range1D Size;

		// Token: 0x04000176 RID: 374
		public Range1D SizeSpeed;

		// Token: 0x04000177 RID: 375
		public Range1D LifeTime;

		// Token: 0x04000178 RID: 376
		public Range1D RotationVelocity;

		// Token: 0x04000179 RID: 377
		public Range1D StartRotationAngle;

		// Token: 0x0400017A RID: 378
		public Range1D RandomOffsetX;

		// Token: 0x0400017B RID: 379
		public Range1D RandomOffsetY;

		// Token: 0x0400017C RID: 380
		public float RandomVelocityFactor;

		// Token: 0x0400017D RID: 381
		public Rectangle TexturePath;

		// Token: 0x0400017E RID: 382
		public Range1D OriginOffset;

		// Token: 0x0400017F RID: 383
		public Range1D TimeToStartLife;

		// Token: 0x04000180 RID: 384
		public bool DepthEffect;
	}
}
