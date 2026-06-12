using System;
using Microsoft.Xna.Framework;
using TheraEngine.Helpers;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x0200019C RID: 412
	public class TextuePathRandomSelect
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00034A1C File Offset: 0x00032C1C
		public Rectangle[] All
		{
			get
			{
				return this.{15886};
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00034A24 File Offset: 0x00032C24
		public Rectangle Get
		{
			get
			{
				if (this.{15886}.Length == 0)
				{
					return Rectangle.Empty;
				}
				return Rand.Pick<Rectangle>(this.{15886});
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00034A40 File Offset: 0x00032C40
		public TextuePathRandomSelect(params Rectangle[] {15885})
		{
			this.{15886} = {15885};
		}

		// Token: 0x04000806 RID: 2054
		private Rectangle[] {15886};
	}
}
