using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x02000059 RID: 89
	public class ParticleSpriteTexture : IParticleTextureSampler
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000DD52 File Offset: 0x0000BF52
		public bool IsSprite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000DD58 File Offset: 0x0000BF58
		public ParticleSpriteTexture(Rectangle {12224}, int {12225}, bool {12226} = true)
		{
			this.{12230} = {12225};
			this.{12229} = new Rectangle[{12225}];
			for (int i = 0; i < {12225}; i++)
			{
				this.{12229}[i] = {12224};
				Rectangle[] array = this.{12229};
				int num = i;
				array[num].X = array[num].X + 2;
				Rectangle[] array2 = this.{12229};
				int num2 = i;
				array2[num2].Y = array2[num2].Y + 2;
				Rectangle[] array3 = this.{12229};
				int num3 = i;
				array3[num3].Width = array3[num3].Width - 4;
				Rectangle[] array4 = this.{12229};
				int num4 = i;
				array4[num4].Height = array4[num4].Height - 4;
				if ({12226})
				{
					{12224}.Y += {12224}.Height;
				}
				else
				{
					{12224}.X += {12224}.Height;
				}
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000DE1C File Offset: 0x0000C01C
		public void GetPath(float {12227}, out Rectangle {12228})
		{
			{12228} = this.{12229}[Math.Min(this.{12230} - 1, (int)Math.Floor((double)({12227} * (float)this.{12230})))];
		}

		// Token: 0x040001D4 RID: 468
		private Rectangle[] {12229};

		// Token: 0x040001D5 RID: 469
		private int {12230};
	}
}
