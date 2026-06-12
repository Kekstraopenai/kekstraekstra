using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Core;

namespace TheraEngine.Assets.Shaders.InternalShaders
{
	// Token: 0x02000188 RID: 392
	internal class InternalScreenPlaneShader : Shader
	{
		// Token: 0x06000A2B RID: 2603 RVA: 0x00032EE8 File Offset: 0x000310E8
		public InternalScreenPlaneShader(ContentManager {15665}, string {15666}) : base({15666}, {15665})
		{
			this.{15672} = base.GetProperty("halfPixel");
			this.{15673} = base.GetProperty("io_matrix");
			this.{15674} = base.GetProperty("io_color");
			this.pass_VS = base.GetPass("Main");
			this.pass_VS_3_0 = base.GetPass("Main_3_0");
			this.pass_PS = base.GetPass("PS");
			this.{15671} = base.GetPass("IdentityObject");
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00032F74 File Offset: 0x00031174
		public void SetHalfPixel(in Vector2 {15667})
		{
			this.{15672}.SetValue(Vector2.Zero);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00032F74 File Offset: 0x00031174
		public void SetCustomHalfPixel(in Vector2 {15668})
		{
			this.{15672}.SetValue(Vector2.Zero);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00032F86 File Offset: 0x00031186
		public void ApplyForIdentityObject(in Matrix {15669}, in Vector4 {15670})
		{
			this.{15673}.SetValue({15669});
			this.{15674}.SetValue({15670});
			this.{15671}.Apply();
		}

		// Token: 0x040007A0 RID: 1952
		internal EffectPass pass_VS;

		// Token: 0x040007A1 RID: 1953
		internal EffectPass pass_VS_3_0;

		// Token: 0x040007A2 RID: 1954
		internal EffectPass pass_PS;

		// Token: 0x040007A3 RID: 1955
		private EffectPass {15671};

		// Token: 0x040007A4 RID: 1956
		private EffectParameter {15672};

		// Token: 0x040007A5 RID: 1957
		private EffectParameter {15673};

		// Token: 0x040007A6 RID: 1958
		private EffectParameter {15674};
	}
}
