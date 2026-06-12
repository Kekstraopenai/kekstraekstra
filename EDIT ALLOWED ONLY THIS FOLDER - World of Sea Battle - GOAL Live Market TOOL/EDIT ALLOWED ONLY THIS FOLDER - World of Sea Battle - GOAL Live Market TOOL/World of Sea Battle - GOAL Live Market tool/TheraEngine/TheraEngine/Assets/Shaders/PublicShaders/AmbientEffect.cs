using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x02000171 RID: 369
	public abstract class AmbientEffect : Shader
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0002FC5B File Offset: 0x0002DE5B
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x0002FC63 File Offset: 0x0002DE63
		public float LessStart
		{
			get
			{
				return this.{15319};
			}
			set
			{
				this.{15319} = value;
				this.{15321}.SetValue(this.{15319});
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0002FC7D File Offset: 0x0002DE7D
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x0002FC85 File Offset: 0x0002DE85
		public float LessEnd
		{
			get
			{
				return this.{15320};
			}
			set
			{
				this.{15320} = value;
				this.{15322}.SetValue(this.{15320});
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002FC9F File Offset: 0x0002DE9F
		public AmbientEffect(Effect {15318}) : base({15318})
		{
			this.{15321} = base.GetProperty("FogStart");
			this.{15322} = base.GetProperty("FogEnd");
		}

		// Token: 0x040006BE RID: 1726
		private float {15319};

		// Token: 0x040006BF RID: 1727
		private float {15320};

		// Token: 0x040006C0 RID: 1728
		private EffectParameter {15321};

		// Token: 0x040006C1 RID: 1729
		private EffectParameter {15322};

		// Token: 0x040006C2 RID: 1730
		public bool IsActive;
	}
}
