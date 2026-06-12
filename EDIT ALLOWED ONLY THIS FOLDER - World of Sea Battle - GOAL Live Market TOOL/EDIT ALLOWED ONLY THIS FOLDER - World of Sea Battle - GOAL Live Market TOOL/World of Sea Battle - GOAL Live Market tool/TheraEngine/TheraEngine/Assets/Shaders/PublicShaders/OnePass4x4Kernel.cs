using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x0200017A RID: 378
	public class OnePass4x4Kernel : Shader
	{
		// Token: 0x060009EF RID: 2543 RVA: 0x000308D4 File Offset: 0x0002EAD4
		public OnePass4x4Kernel() : base(Engine.OnePass4x4KernelEffect)
		{
			this.{15436} = base.GetProperty("tex");
			this.{15437} = base.GetProperty("texSize");
			this.{15438} = base.GetPass(0);
			this.{15439} = base.GetPass(1);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00030928 File Offset: 0x0002EB28
		public void ApplyPS(Texture2D {15433}, bool {15434}, float {15435})
		{
			this.{15436}.SetValue({15433});
			this.{15437}.SetValue(new Vector2((float){15433}.Width, (float){15433}.Height) / {15435});
			if ({15434})
			{
				this.{15439}.Apply();
				return;
			}
			this.{15438}.Apply();
		}

		// Token: 0x0400070F RID: 1807
		private EffectParameter {15436};

		// Token: 0x04000710 RID: 1808
		private EffectParameter {15437};

		// Token: 0x04000711 RID: 1809
		private EffectPass {15438};

		// Token: 0x04000712 RID: 1810
		private EffectPass {15439};
	}
}
