using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Graphics;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x020004F5 RID: 1269
	internal static class AtlasGameGui
	{
		// Token: 0x06001C39 RID: 7225 RVA: 0x00101C90 File Offset: 0x000FFE90
		private static IEnumerable<Rectangle> GetCircleProgress()
		{
			return new AtlasGameGui.<GetCircleProgress>d__2(-2);
		}

		// Token: 0x04001B52 RID: 6994
		public static Texture2DAtlas Texture;

		// Token: 0x04001B53 RID: 6995
		public static readonly Rectangle[] circleProgress = AtlasGameGui.GetCircleProgress().ToArray<Rectangle>();

		// Token: 0x04001B54 RID: 6996
		public static readonly Rectangle venselNew = new Rectangle(0, 1522, 561, 103);

		// Token: 0x04001B55 RID: 6997
		public static readonly Rectangle rect_lightParticle = new Rectangle(6, 504, 21, 21);

		// Token: 0x04001B56 RID: 6998
		public static readonly Rectangle newToolList_main = new Rectangle(951, 0, 231, 41);

		// Token: 0x04001B57 RID: 6999
		public static readonly Rectangle newToolList_item = new Rectangle(951, 42, 231, 35);

		// Token: 0x04001B58 RID: 7000
		public static readonly Rectangle basicBlueButton = new Rectangle(817, 446, 248, 57);

		// Token: 0x04001B59 RID: 7001
		public static readonly Rectangle rect_gui_progressbar_dark_basic_164x24 = new Rectangle(828, 533, 164, 24);

		// Token: 0x04001B5A RID: 7002
		public static readonly Rectangle rect_gui_progressbar_dark_active_164x24 = new Rectangle(828, 557, 164, 24);

		// Token: 0x04001B5B RID: 7003
		public static readonly Rectangle rect_gui_progressbar_dark_cursor_6x24 = new Rectangle(821, 557, 6, 24);

		// Token: 0x04001B5C RID: 7004
		public static readonly Rectangle rect_gui_selectlist_bar_dark_164x24px = new Rectangle(500, 1335, 164, 24);

		// Token: 0x04001B5D RID: 7005
		public static readonly Rectangle rect_gui_selectlist_item_dark_164x24px = new Rectangle(500, 1363, 164, 24);

		// Token: 0x04001B5E RID: 7006
		public static readonly Rectangle rect_gui_button_wood_active_146x26px = new Rectangle(1405, 0, 146, 26);

		// Token: 0x04001B5F RID: 7007
		public static readonly Rectangle rect_gui_button_wood_deactive_146x26px = new Rectangle(1405, 26, 146, 26);

		// Token: 0x04001B60 RID: 7008
		public static readonly Rectangle rect_gui_button_newcyan162x50 = new Rectangle(1247, 1757, 162, 50);

		// Token: 0x04001B61 RID: 7009
		public static readonly Rectangle rect_gui_button_newcyan162x50_lighted = new Rectangle(1084, 1757, 162, 50);

		// Token: 0x04001B62 RID: 7010
		public static readonly Rectangle rect_image_rudder_298px = new Rectangle(2125, 1, 298, 298);

		// Token: 0x04001B63 RID: 7011
		public static readonly Rectangle rect_asset_transparent_1px = new Rectangle(493, 2, 1, 1);

		// Token: 0x04001B64 RID: 7012
		public static readonly Rectangle rect_asset_whitepixel_1px = new Rectangle(486, 2, 1, 1);

		// Token: 0x04001B65 RID: 7013
		public static readonly Rectangle rect_greenConnect = new Rectangle(1802, 952, 14, 14);

		// Token: 0x04001B66 RID: 7014
		public static readonly Rectangle rect_redConnect = new Rectangle(1827, 952, 14, 14);

		// Token: 0x04001B67 RID: 7015
		public static readonly Rectangle soundButton_state1 = new Rectangle(1760, 843, 41, 41);

		// Token: 0x04001B68 RID: 7016
		public static readonly Rectangle soundButton_state2 = new Rectangle(1760, 885, 41, 41);

		// Token: 0x04001B69 RID: 7017
		public static readonly Rectangle portBattle_wait = new Rectangle(2199, 72, 44, 50);

		// Token: 0x04001B6A RID: 7018
		public static readonly Rectangle portBattle_process = new Rectangle(2244, 72, 44, 50);

		// Token: 0x04001B6B RID: 7019
		public static readonly Rectangle portBattle_besieged = new Rectangle(2289, 72, 44, 50);

		// Token: 0x04001B6C RID: 7020
		public static readonly Rectangle fort_guildTagForm = new Rectangle(2115, 234, 80, 51);

		// Token: 0x04001B6D RID: 7021
		public static readonly Rectangle miniwindow_scrollBtUp = new Rectangle(1600, 0, 21, 21);

		// Token: 0x04001B6E RID: 7022
		public static readonly Rectangle miniwindow_scrollBtDown = new Rectangle(1625, 0, 21, 21);

		// Token: 0x04001B6F RID: 7023
		public static readonly Rectangle miniwindow_scrollPoint = new Rectangle(1650, 0, 21, 30);

		// Token: 0x04001B70 RID: 7024
		public static readonly Rectangle smallTooltipWindow = new Rectangle(1800, 50, 217, 291);

		// Token: 0x04001B71 RID: 7025
		public static readonly Rectangle scrollBarNewPointer = new Rectangle(314, 51, 10, 46);

		// Token: 0x04001B72 RID: 7026
		public static readonly Rectangle scrollBarNewFiller = new Rectangle(303, 51, 10, 46);
	}
}
