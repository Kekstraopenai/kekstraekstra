using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Graphics;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x0200017E RID: 382
	public class ScreenChromaticAberration : Shader
	{
		// Token: 0x06000A05 RID: 2565 RVA: 0x00031487 File Offset: 0x0002F687
		public ScreenChromaticAberration() : base(Engine.SsChromaticAberrationEffect)
		{
			this.{15514} = base.GetProperty("halfPixel");
			this.{15513} = base.GetPass("Main");
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000314B8 File Offset: 0x0002F6B8
		public void DrawScreenPlaneRenderer(Texture2D {15512})
		{
			Engine.Game.GraphicsDevice.Textures[0] = {15512};
			this.{15514}.SetValue(new Vector2(-0.5f / (float)({15512}.Width / 2), 0.5f / (float)({15512}.Height / 2)));
			this.{15513}.Apply();
			ScreenPlaneRenderer.Render();
		}

		// Token: 0x04000754 RID: 1876
		private EffectPass {15513};

		// Token: 0x04000755 RID: 1877
		private EffectParameter {15514};
	}
}
