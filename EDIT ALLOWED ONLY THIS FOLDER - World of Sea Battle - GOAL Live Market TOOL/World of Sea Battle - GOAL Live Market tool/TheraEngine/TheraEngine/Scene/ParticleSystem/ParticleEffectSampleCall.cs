using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x02000056 RID: 86
	public struct ParticleEffectSampleCall
	{
		// Token: 0x06000248 RID: 584 RVA: 0x0000D5E1 File Offset: 0x0000B7E1
		public ParticleEffectSampleCall(Vector3 {12170})
		{
			this.SamplePosition = {12170};
			this.SampleVelocityStart = Vector3.Zero;
			this.SampleVelocityEnd = Vector3.Zero;
			this.SampleVelocityChangePerSec = 0f;
			this.Stretch = Vector3.Zero;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000D616 File Offset: 0x0000B816
		public ParticleEffectSampleCall(Vector3 {12171}, Vector3 {12172})
		{
			this.SamplePosition = {12171};
			this.SampleVelocityStart = {12172};
			this.SampleVelocityEnd = {12172};
			this.SampleVelocityChangePerSec = 0f;
			this.Stretch = Vector3.Zero;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000D643 File Offset: 0x0000B843
		public ParticleEffectSampleCall(Vector3 {12173}, Vector3 {12174}, float {12175})
		{
			this.SamplePosition = {12173};
			this.SampleVelocityStart = {12174};
			this.SampleVelocityEnd = {12174};
			this.SampleVelocityChangePerSec = {12175};
			this.Stretch = Vector3.Zero;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000D66C File Offset: 0x0000B86C
		public ParticleEffectSampleCall(Vector3 {12176}, Vector3 {12177}, Vector3 {12178})
		{
			this.SamplePosition = {12176};
			this.SampleVelocityStart = {12177};
			this.SampleVelocityEnd = {12178};
			this.SampleVelocityChangePerSec = 0f;
			this.Stretch = Vector3.Zero;
		}

		// Token: 0x040001AE RID: 430
		public Vector3 SamplePosition;

		// Token: 0x040001AF RID: 431
		public Vector3 SampleVelocityStart;

		// Token: 0x040001B0 RID: 432
		public Vector3 SampleVelocityEnd;

		// Token: 0x040001B1 RID: 433
		public float SampleVelocityChangePerSec;

		// Token: 0x040001B2 RID: 434
		public Vector3 Stretch;
	}
}
