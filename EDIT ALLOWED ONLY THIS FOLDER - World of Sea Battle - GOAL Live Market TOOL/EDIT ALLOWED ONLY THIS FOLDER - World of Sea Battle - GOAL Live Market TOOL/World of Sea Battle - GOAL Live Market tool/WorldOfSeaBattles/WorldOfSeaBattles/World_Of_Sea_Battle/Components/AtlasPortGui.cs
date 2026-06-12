using System;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Graphics;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x020004F9 RID: 1273
	internal static class AtlasPortGui
	{
		// Token: 0x06001C48 RID: 7240 RVA: 0x00102F9C File Offset: 0x0010119C
		public static Rectangle DesignElementIcon(ShipDesignCategory {25282})
		{
			switch ({25282})
			{
			case ShipDesignCategory.Flag:
			case ShipDesignCategory.Flag2:
				return new Rectangle(0, 1263, 64, 64);
			case ShipDesignCategory.Decal1:
			case ShipDesignCategory.Decal2:
			case ShipDesignCategory.ShipFullDesign:
			case ShipDesignCategory.MastColor:
			case ShipDesignCategory.SailTexture:
				return new Rectangle(65, 1263, 64, 64);
			case ShipDesignCategory.Satellite:
				return new Rectangle(130, 1263, 64, 64);
			case ShipDesignCategory.BowFigure:
				return new Rectangle(195, 1263, 64, 64);
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x04001BBE RID: 7102
		public static Texture2DAtlas Texture;

		// Token: 0x04001BBF RID: 7103
		internal static readonly ExpandoTexturePath ToolTipTexture = ExpandoTexturePath.CreateBox(new Rectangle(0, 180, 128, 128), new Rectangle(0, 308, 128, 128), new Rectangle(128, 180, 128, 128), new Rectangle(128, 308, 128, 128));

		// Token: 0x04001BC0 RID: 7104
		internal static readonly Rectangle cSmallProgressBarBack = new Rectangle(81, 0, 54, 13);

		// Token: 0x04001BC1 RID: 7105
		internal static readonly Rectangle cSmallProgressBarFrontYel = new Rectangle(136, 0, 54, 13);

		// Token: 0x04001BC2 RID: 7106
		internal static readonly Rectangle cSmallProgressBarBack_Tall = new Rectangle(191, 0, 60, 12);

		// Token: 0x04001BC3 RID: 7107
		internal static readonly Rectangle cSmallProgressBarFrontWhite_Tall = new Rectangle(349, 34, 60, 12);

		// Token: 0x04001BC4 RID: 7108
		internal static readonly Rectangle cSmallProgressBarFrontGreen_Tall = new Rectangle(252, 0, 60, 12);

		// Token: 0x04001BC5 RID: 7109
		internal static readonly Rectangle cSmallProgressBarFrontSky_Tall = new Rectangle(313, 0, 60, 12);

		// Token: 0x04001BC6 RID: 7110
		internal static readonly Rectangle dynamicTexture_big_for_fullscreen = new Rectangle(2491, 1331, 444, 444);

		// Token: 0x04001BC7 RID: 7111
		internal static readonly Rectangle c_overlayNotAvaialable = new Rectangle(2586, 76, 75, 75);

		// Token: 0x04001BC8 RID: 7112
		internal static readonly Rectangle buttonBlue158 = new Rectangle(0, 166, 158, 37);

		// Token: 0x04001BC9 RID: 7113
		internal static readonly Rectangle buttonYellow158 = new Rectangle(1076, 153, 158, 37);

		// Token: 0x04001BCA RID: 7114
		internal static readonly Rectangle buttonGray = new Rectangle(984, 246, 158, 37);

		// Token: 0x04001BCB RID: 7115
		internal static readonly Rectangle buttonBlueBack = new Rectangle(376, 166, 158, 37);

		// Token: 0x04001BCC RID: 7116
		internal static readonly Rectangle buttonYellow = new Rectangle(400, 205, 144, 38);

		// Token: 0x04001BCD RID: 7117
		internal static readonly Rectangle whitePixel = new Rectangle(478, 16, 1, 1);

		// Token: 0x04001BCE RID: 7118
		internal static readonly Rectangle transpPixel = new Rectangle(481, 61, 1, 1);

		// Token: 0x04001BCF RID: 7119
		internal static readonly Rectangle toollist_main = new Rectangle(0, 215, 198, 36);

		// Token: 0x04001BD0 RID: 7120
		internal static readonly Rectangle toollist_item = new Rectangle(199, 215, 198, 36);

		// Token: 0x04001BD1 RID: 7121
		internal static readonly Rectangle vd_checkBox_26px_true = new Rectangle(26, 0, 26, 26);

		// Token: 0x04001BD2 RID: 7122
		internal static readonly Rectangle vd_checkBox_26px_false = new Rectangle(0, 0, 26, 26);

		// Token: 0x04001BD3 RID: 7123
		internal static readonly Rectangle vd_checkBox_26px_dis = new Rectangle(52, 0, 26, 26);

		// Token: 0x04001BD4 RID: 7124
		internal static readonly Rectangle vd_checkBox_18px_true = new Rectangle(18, 27, 18, 18);

		// Token: 0x04001BD5 RID: 7125
		internal static readonly Rectangle vd_checkBox_18px_false = new Rectangle(0, 27, 18, 18);

		// Token: 0x04001BD6 RID: 7126
		internal static readonly Rectangle vd_textBox_167x24px = new Rectangle(86, 0, 167, 24);

		// Token: 0x04001BD7 RID: 7127
		internal static readonly Rectangle vd_toolList_167x24px_item = new Rectangle(86, 50, 167, 24);

		// Token: 0x04001BD8 RID: 7128
		internal static readonly Rectangle main_btExit = new Rectangle(0, 0, 240, 60);

		// Token: 0x04001BD9 RID: 7129
		internal static readonly Rectangle main_shipInfoBox = new Rectangle(1525, 1, 320, 435);

		// Token: 0x04001BDA RID: 7130
		internal static readonly Rectangle main_Ships = new Rectangle(540, 0, 920, 164);

		// Token: 0x04001BDB RID: 7131
		internal static readonly Rectangle main_btEquipment = new Rectangle(250, 0, 172, 25);

		// Token: 0x04001BDC RID: 7132
		internal static readonly Rectangle main_btRemount = new Rectangle(250, 25, 172, 25);

		// Token: 0x04001BDD RID: 7133
		internal static readonly Rectangle main_bar_btStorage = new Rectangle(840, 164, 248, 72);

		// Token: 0x04001BDE RID: 7134
		internal static readonly Rectangle main_bar_btShopping = new Rectangle(840, 236, 248, 72);

		// Token: 0x04001BDF RID: 7135
		internal static readonly Rectangle main_bar_btTaverna = new Rectangle(840, 308, 248, 72);

		// Token: 0x04001BE0 RID: 7136
		internal static readonly Rectangle box_remountShip = new Rectangle(0, 780, 400, 300);

		// Token: 0x04001BE1 RID: 7137
		internal static readonly Rectangle box_remountShip_item1 = new Rectangle(0, 1080, 392, 46);

		// Token: 0x04001BE2 RID: 7138
		internal static readonly Rectangle box_remountShip_item2 = new Rectangle(0, 1126, 392, 46);

		// Token: 0x04001BE3 RID: 7139
		internal static readonly Rectangle box_remountShip_item3 = new Rectangle(0, 1172, 392, 46);

		// Token: 0x04001BE4 RID: 7140
		internal static readonly Rectangle box_remountShip_item4 = new Rectangle(0, 1218, 392, 46);

		// Token: 0x04001BE5 RID: 7141
		internal static readonly Rectangle box_remountShip_item5 = new Rectangle(0, 1264, 392, 46);

		// Token: 0x04001BE6 RID: 7142
		internal static readonly Rectangle box_remountShip_noItems = new Rectangle(0, 1310, 220, 100);

		// Token: 0x04001BE7 RID: 7143
		internal static readonly Rectangle helpBoxPath = new Rectangle(1, 97, 50, 50);

		// Token: 0x04001BE8 RID: 7144
		internal static readonly Rectangle scrollBar_Up = new Rectangle(254, 20, 28, 28);

		// Token: 0x04001BE9 RID: 7145
		internal static readonly Rectangle scrollBar_Down = new Rectangle(283, 20, 28, 28);

		// Token: 0x04001BEA RID: 7146
		internal static readonly Rectangle scrollBar_Pointer = new Rectangle(312, 21, 28, 41);

		// Token: 0x04001BEB RID: 7147
		internal static readonly Rectangle backgroundSpark = new Rectangle(505, 760, 126, 126);

		// Token: 0x04001BEC RID: 7148
		internal static readonly Rectangle backgroundSpark2 = new Rectangle(634, 761, 64, 64);

		// Token: 0x04001BED RID: 7149
		internal static readonly Rectangle scrollBarNewFiller = new Rectangle(803, 106, 10, 46);

		// Token: 0x04001BEE RID: 7150
		internal static readonly Rectangle scrollBarNewPointer = new Rectangle(814, 106, 10, 46);

		// Token: 0x04001BEF RID: 7151
		internal static readonly Rectangle cSelectorPickedNeutral = new Rectangle(2648, 622, 237, 30);

		// Token: 0x04001BF0 RID: 7152
		internal static readonly Rectangle cSelectorPicked = new Rectangle(2689, 590, 237, 30);

		// Token: 0x04001BF1 RID: 7153
		internal static readonly Rectangle cSelectorUnpicked = new Rectangle(2927, 590, 237, 30);
	}
}
