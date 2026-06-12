using System;
using Microsoft.Xna.Framework;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020000A4 RID: 164
	public struct {17464}
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x00022D9B File Offset: 0x00020F9B
		public {17464}(Rectangle {17469}, string {17470})
		{
			this.ImageShrinkedSize = default(Vector2);
			this.Image = new ImageDecription(OtherTextures.Images, {17469});
			this.Text = {17470};
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00022DC1 File Offset: 0x00020FC1
		public {17464}(string {17471}, ImageDecription {17472} = default(ImageDecription))
		{
			this.ImageShrinkedSize = default(Vector2);
			this.Text = {17471};
			this.Image = {17472};
		}

		// Token: 0x04000399 RID: 921
		public readonly ImageDecription Image;

		// Token: 0x0400039A RID: 922
		public readonly string Text;

		// Token: 0x0400039B RID: 923
		public Vector2 ImageShrinkedSize;
	}
}
