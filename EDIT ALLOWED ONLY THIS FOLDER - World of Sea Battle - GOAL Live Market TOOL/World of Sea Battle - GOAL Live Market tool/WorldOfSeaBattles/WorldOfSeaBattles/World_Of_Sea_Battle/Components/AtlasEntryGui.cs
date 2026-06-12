using System;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Graphics;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x020004F4 RID: 1268
	internal static class AtlasEntryGui
	{
		// Token: 0x04001B42 RID: 6978
		public static Texture2DAtlas Texture;

		// Token: 0x04001B43 RID: 6979
		internal static readonly Rectangle whitePixel = new Rectangle(758, 158, 1, 1);

		// Token: 0x04001B44 RID: 6980
		internal static readonly Rectangle basicForm = new Rectangle(0, 0, 483, 704);

		// Token: 0x04001B45 RID: 6981
		internal static readonly Rectangle formLight = new Rectangle(485, 1, 465, 145);

		// Token: 0x04001B46 RID: 6982
		internal static readonly Rectangle textBox = new Rectangle(484, 148, 264, 36);

		// Token: 0x04001B47 RID: 6983
		internal static readonly Rectangle cbOn = new Rectangle(774, 148, 24, 24);

		// Token: 0x04001B48 RID: 6984
		internal static readonly Rectangle cbOff = new Rectangle(799, 148, 24, 24);

		// Token: 0x04001B49 RID: 6985
		internal static readonly Rectangle buttonTop = new Rectangle(484, 185, 134, 40);

		// Token: 0x04001B4A RID: 6986
		internal static readonly Rectangle btPlayPassive = new Rectangle(484, 353, 223, 116);

		// Token: 0x04001B4B RID: 6987
		internal static readonly Rectangle btPlayActive = new Rectangle(484, 236, 223, 116);

		// Token: 0x04001B4C RID: 6988
		internal static readonly Rectangle btExit = new Rectangle(484, 505, 59, 57);

		// Token: 0x04001B4D RID: 6989
		internal static readonly Rectangle authForm = new Rectangle(484, 565, 354, 65);

		// Token: 0x04001B4E RID: 6990
		internal static readonly Rectangle eyePasswordVisible = new Rectangle(824, 150, 26, 26);

		// Token: 0x04001B4F RID: 6991
		internal static readonly Rectangle eyePasswordHidden = new Rectangle(876, 150, 26, 26);

		// Token: 0x04001B50 RID: 6992
		internal static readonly Rectangle dropdownItemBox = new Rectangle(496, 630, 263, 37);

		// Token: 0x04001B51 RID: 6993
		internal static readonly Rectangle WhiteRectangle = new Rectangle(511, 469, 220, 60);
	}
}
