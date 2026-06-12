using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x02000057 RID: 87
	public struct ParticleEffect3DTemplate
	{
		// Token: 0x0600024C RID: 588 RVA: 0x0000D699 File Offset: 0x0000B899
		public void Apply(ParticleEffectSampleCall {12179}, ParticleManager3D {12180})
		{
			{12180}.SampleInternal(ref {12179}, ref this);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000D6A4 File Offset: 0x0000B8A4
		public void Apply(ref ParticleEffectSampleCall {12181}, ParticleManager3D {12182})
		{
			{12182}.SampleInternal(ref {12181}, ref this);
		}

		// Token: 0x040001B3 RID: 435
		public Range1D SingleInitialSize;

		// Token: 0x040001B4 RID: 436
		public float SingleAngularVelocity;

		// Token: 0x040001B5 RID: 437
		public float SingleSizeVelocity;

		// Token: 0x040001B6 RID: 438
		public float SingleSizeVelocityChange;

		// Token: 0x040001B7 RID: 439
		public float SingleGravityOrFlyVelocityPerSecAddToVelocity;

		// Token: 0x040001B8 RID: 440
		public float SingleWindFactor;

		// Token: 0x040001B9 RID: 441
		public bool SingleCreateRandomInitialAxis;

		// Token: 0x040001BA RID: 442
		public float SingleBrightness;

		// Token: 0x040001BB RID: 443
		public IParticleTextureSampler SingleTexturePath;

		// Token: 0x040001BC RID: 444
		public Vector4 SingleColor;

		// Token: 0x040001BD RID: 445
		public Range1D SingleTtlToShow;

		// Token: 0x040001BE RID: 446
		public bool SingleIsSqueredShow;

		// Token: 0x040001BF RID: 447
		public bool SingleIsSqueredHide;

		// Token: 0x040001C0 RID: 448
		public Range1D SingleTtl;

		// Token: 0x040001C1 RID: 449
		public float ReduceColorSec;

		// Token: 0x040001C2 RID: 450
		public float IntialAxisIfRandomDisabled;

		// Token: 0x040001C3 RID: 451
		public int GeneratorCountPerSample;

		// Token: 0x040001C4 RID: 452
		public float GeneratorRandomOffset;
	}
}
