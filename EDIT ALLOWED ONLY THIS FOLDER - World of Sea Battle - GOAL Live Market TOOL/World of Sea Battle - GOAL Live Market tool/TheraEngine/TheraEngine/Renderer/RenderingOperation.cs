using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Graphics.Models;

namespace TheraEngine.Renderer
{
	// Token: 0x02000064 RID: 100
	public struct RenderingOperation
	{
		// Token: 0x04000215 RID: 533
		public Matrix World;

		// Token: 0x04000216 RID: 534
		public UWModel Model;

		// Token: 0x04000217 RID: 535
		public Texture2D OverrideTexture;

		// Token: 0x04000218 RID: 536
		public float Transparancy;

		// Token: 0x04000219 RID: 537
		public object UserContext;

		// Token: 0x0400021A RID: 538
		public bool ApiDisableShadow;

		// Token: 0x0400021B RID: 539
		public bool ApiDisableAniamtedMaterials;
	}
}
