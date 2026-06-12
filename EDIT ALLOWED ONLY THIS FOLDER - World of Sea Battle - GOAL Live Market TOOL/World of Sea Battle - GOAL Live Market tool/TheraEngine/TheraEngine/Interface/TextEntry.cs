using System;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Content;

namespace TheraEngine.Interface
{
	// Token: 0x02000093 RID: 147
	internal class TextEntry
	{
		// Token: 0x0600037F RID: 895 RVA: 0x00013C53 File Offset: 0x00011E53
		public void Include(string {12830}, CustomSpriteFont {12831})
		{
			this.TextHasValue = true;
			this.Value = {12830};
			this.Font = {12831};
			this.TextSize = {12831}.Measure({12830});
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00013C77 File Offset: 0x00011E77
		public void Uinclude()
		{
			this.TextHasValue = false;
			this.Value = string.Empty;
			this.Font = null;
			this.TextSize = Vector2.Zero;
		}

		// Token: 0x040002E0 RID: 736
		public bool TextHasValue;

		// Token: 0x040002E1 RID: 737
		public CustomSpriteFont Font;

		// Token: 0x040002E2 RID: 738
		public string Value;

		// Token: 0x040002E3 RID: 739
		public Vector2 TextSize;
	}
}
