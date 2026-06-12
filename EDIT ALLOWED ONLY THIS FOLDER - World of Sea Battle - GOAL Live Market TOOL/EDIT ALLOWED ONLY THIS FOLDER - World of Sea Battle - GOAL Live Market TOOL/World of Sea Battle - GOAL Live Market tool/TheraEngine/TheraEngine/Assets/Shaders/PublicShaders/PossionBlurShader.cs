using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x0200017D RID: 381
	public class PossionBlurShader : Shader
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x000313CA File Offset: 0x0002F5CA
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x000313D2 File Offset: 0x0002F5D2
		public float Power
		{
			get
			{
				return this.{15511};
			}
			set
			{
				if (MathHelper.Clamp(value, 0f, 100f) != value)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.{15511} = value;
				this.{15510}.SetValue(this.{15511});
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00031408 File Offset: 0x0002F608
		public PossionBlurShader() : base(Engine.PoissonEffect)
		{
			this.{15508} = base.GetEffectBase.Techniques[0];
			this.{15509} = base.GetEffectBase.Techniques[1];
			this.{15510} = base.GetProperty("EffectPower");
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0003145F File Offset: 0x0002F65F
		public void SetIsHighQuality(bool {15507})
		{
			if ({15507})
			{
				base.GetEffectBase.CurrentTechnique = this.{15509};
				return;
			}
			base.GetEffectBase.CurrentTechnique = this.{15508};
		}

		// Token: 0x04000750 RID: 1872
		private EffectTechnique {15508};

		// Token: 0x04000751 RID: 1873
		private EffectTechnique {15509};

		// Token: 0x04000752 RID: 1874
		private EffectParameter {15510};

		// Token: 0x04000753 RID: 1875
		private float {15511};
	}
}
