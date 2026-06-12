using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000A8 RID: 168
	public struct ImageDecription
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x00017254 File Offset: 0x00015454
		public ImageDecription(Texture2D {13216})
		{
			this.Tex = {13216};
			this.Path = {13216}.Bounds;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00017269 File Offset: 0x00015469
		public ImageDecription(Texture2D {13217}, Rectangle {13218})
		{
			this.Tex = {13217};
			this.Path = {13218};
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00017279 File Offset: 0x00015479
		public static implicit operator ImageDecription(Texture2D {13219})
		{
			return new ImageDecription({13219});
		}

		// Token: 0x04000360 RID: 864
		public readonly Texture2D Tex;

		// Token: 0x04000361 RID: 865
		public readonly Rectangle Path;
	}
}
