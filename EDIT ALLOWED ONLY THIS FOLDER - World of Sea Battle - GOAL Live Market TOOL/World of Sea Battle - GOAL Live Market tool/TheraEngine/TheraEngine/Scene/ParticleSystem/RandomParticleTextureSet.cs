using System;
using Microsoft.Xna.Framework;
using TheraEngine.Helpers;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x0200005B RID: 91
	public class RandomParticleTextureSet : IParticleTextureSampler
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000E21A File Offset: 0x0000C41A
		public bool IsSprite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000E21D File Offset: 0x0000C41D
		public RandomParticleTextureSet(params Rectangle[] {12253})
		{
			this.{12256} = {12253};
			this.{12257} = {12253}.Length;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000E235 File Offset: 0x0000C435
		public void GetPath(float {12254}, out Rectangle {12255})
		{
			if (this.{12257} == 1)
			{
				{12255} = this.{12256}[0];
				return;
			}
			{12255} = this.{12256}[Rand.RangeInt(0, this.{12257})];
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000E270 File Offset: 0x0000C470
		public Rectangle GetPath()
		{
			if (this.{12257} == 1)
			{
				return this.{12256}[0];
			}
			return this.{12256}[Rand.RangeInt(0, this.{12257})];
		}

		// Token: 0x040001E6 RID: 486
		private Rectangle[] {12256};

		// Token: 0x040001E7 RID: 487
		private int {12257};
	}
}
