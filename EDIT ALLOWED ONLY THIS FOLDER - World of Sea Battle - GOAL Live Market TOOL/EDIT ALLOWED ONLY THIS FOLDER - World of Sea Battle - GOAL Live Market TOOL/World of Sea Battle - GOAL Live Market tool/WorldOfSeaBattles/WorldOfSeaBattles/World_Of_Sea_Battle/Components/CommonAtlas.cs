using System;
using System.Collections.Generic;
using Common.Account;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Graphics;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x020004FA RID: 1274
	internal static class CommonAtlas
	{
		// Token: 0x06001C4A RID: 7242 RVA: 0x001034FF File Offset: 0x001016FF
		public static Rectangle GetShipClassIcon(PlayerShipInfo {25283})
		{
			return CommonAtlas.GetShipClassIcon({25283}.Class, ({25283}.Coolness == PlayerShipCoolness.Elite) ? CommonAtlas.ShipClassIconStyle.Gold : (({25283}.Coolness == PlayerShipCoolness.Unique) ? CommonAtlas.ShipClassIconStyle.Silver : (({25283}.Coolness == PlayerShipCoolness.Empire) ? CommonAtlas.ShipClassIconStyle.Empire : CommonAtlas.ShipClassIconStyle.Blue)));
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00103534 File Offset: 0x00101734
		public static Rectangle GetShipClassIcon(ShipClass {25284}, CommonAtlas.ShipClassIconStyle {25285} = CommonAtlas.ShipClassIconStyle.Blue)
		{
			if ({25285} == CommonAtlas.ShipClassIconStyle.Empire)
			{
				return new Rectangle(0, 1878, 66, 66);
			}
			Rectangle[] array;
			if ({25285} != CommonAtlas.ShipClassIconStyle.Blue)
			{
				if ({25285} != CommonAtlas.ShipClassIconStyle.Silver)
				{
					if ({25285} != CommonAtlas.ShipClassIconStyle.Closed)
					{
						if ({25285} != CommonAtlas.ShipClassIconStyle.Gold)
						{
							throw new NotSupportedException();
						}
						array = CommonAtlas.shipClasses_miniGold;
					}
					else
					{
						array = CommonAtlas.shipClasses_miniClosed;
					}
				}
				else
				{
					array = CommonAtlas.shipClasses_miniSilver;
				}
			}
			else
			{
				array = CommonAtlas.shipClasses_miniBlue;
			}
			Rectangle[] array2 = array;
			switch ({25284})
			{
			case ShipClass.Destroyer:
				return array2[1];
			case ShipClass.Battleship:
				return array2[0];
			case ShipClass.Hardship:
				return array2[4];
			case ShipClass.CargoShip:
				return array2[2];
			case ShipClass.Mortar:
				return array2[5];
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x001035D0 File Offset: 0x001017D0
		public static Rectangle GetShipyardTableIcon(ShipClass {25286}, CommonAtlas.ShipClassIconStyle {25287})
		{
			if ({25287} == CommonAtlas.ShipClassIconStyle.Empire)
			{
				return CommonAtlas.shipClasses_miniYellow[6];
			}
			switch ({25286})
			{
			case ShipClass.Destroyer:
				return CommonAtlas.shipClasses_miniYellow[1];
			case ShipClass.Battleship:
				return CommonAtlas.shipClasses_miniYellow[0];
			case ShipClass.Hardship:
				return CommonAtlas.shipClasses_miniYellow[4];
			case ShipClass.CargoShip:
				return CommonAtlas.shipClasses_miniYellow[2];
			case ShipClass.Mortar:
				return CommonAtlas.shipClasses_miniYellow[5];
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0010364C File Offset: 0x0010184C
		public static Rectangle GetCannonClassIcon(CannonClass {25288}, bool {25289})
		{
			Rectangle result;
			switch ({25288})
			{
			case CannonClass.LiteCannon:
				result = new Rectangle(0, 837, 25, 25);
				break;
			case CannonClass.DistanceCannon:
				result = new Rectangle(26, 837, 25, 25);
				break;
			case CannonClass.HeavyCannon:
				result = new Rectangle(52, 837, 25, 25);
				break;
			case CannonClass.Bombardier:
				result = new Rectangle(78, 837, 25, 25);
				break;
			case CannonClass.Special:
				result = ({25289} ? new Rectangle(104, 837, 25, 25) : new Rectangle(130, 770, 25, 25));
				break;
			case CannonClass.Mortar:
				result = Rectangle.Empty;
				break;
			default:
				throw new NotSupportedException();
			}
			return result;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00103700 File Offset: 0x00101900
		public static Image GetCannonClassIconForToolTip(CannonClass {25290}, bool {25291})
		{
			Rectangle cannonClassIcon = CommonAtlas.GetCannonClassIcon({25290}, {25291});
			return new Image(new Vector2(-1f, -4f), CommonAtlas.Texture.Tex, cannonClassIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00103738 File Offset: 0x00101938
		public static Rectangle GetFractionFlagPrerender(FractionID {25292})
		{
			Rectangle result;
			switch ({25292})
			{
			case FractionID.Pirate:
				result = new Rectangle(2781, 271, 100, 67);
				break;
			case FractionID.Antilia:
				result = new Rectangle(2554, 203, 100, 67);
				break;
			case FractionID.Espaniol:
				result = new Rectangle(2655, 203, 100, 67);
				break;
			case FractionID.KaiAndSeveria:
				result = new Rectangle(2453, 203, 100, 67);
				break;
			case FractionID.Empire:
				result = new Rectangle(2756, 203, 100, 67);
				break;
			case FractionID.TradeUnion:
				result = new Rectangle(2680, 271, 100, 67);
				break;
			default:
				result = CommonAtlas.transpPixel;
				break;
			}
			return result;
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x001037F4 File Offset: 0x001019F4
		public static Rectangle GetWorldWarFlagPrerender(FractionID {25293})
		{
			Rectangle result;
			switch ({25293})
			{
			case FractionID.Pirate:
				result = new Rectangle(2130, 520, 197, 69);
				break;
			case FractionID.Antilia:
				result = new Rectangle(2667, 410, 197, 69);
				break;
			case FractionID.Espaniol:
				result = new Rectangle(2454, 519, 197, 69);
				break;
			case FractionID.KaiAndSeveria:
				result = new Rectangle(2628, 927, 197, 69);
				break;
			case FractionID.Empire:
				result = new Rectangle(2667, 340, 197, 69);
				break;
			case FractionID.TradeUnion:
				result = new Rectangle(2130, 590, 197, 69);
				break;
			default:
				result = new Rectangle(2269, 380, 197, 69);
				break;
			}
			return result;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x001038D8 File Offset: 0x00101AD8
		public static Rectangle GetWorldFlagPrerender(OpenWorldFlag {25294}, FractionID {25295})
		{
			Rectangle result;
			switch ({25294})
			{
			case OpenWorldFlag.Peaceful:
			case OpenWorldFlag.PeacefulDisallowed:
				result = new Rectangle(2011, 450, 197, 69);
				break;
			case OpenWorldFlag.Pirate:
				result = new Rectangle(2407, 450, 197, 69);
				break;
			case OpenWorldFlag.Trader:
			case OpenWorldFlag.TraderDisallowed:
				result = new Rectangle(2649, 1000, 197, 69);
				break;
			case OpenWorldFlag.War:
				result = CommonAtlas.GetWorldWarFlagPrerender({25295});
				break;
			case OpenWorldFlag.Legendary:
				result = new Rectangle(2467, 380, 197, 69);
				break;
			case OpenWorldFlag.NoFlag:
				result = new Rectangle(2269, 310, 197, 69);
				break;
			default:
				throw new NotSupportedException();
			}
			return result;
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x001039A0 File Offset: 0x00101BA0
		// Note: this type is marked as 'beforefieldinit'.
		static CommonAtlas()
		{
			Dictionary<CannonBallInfoEffects, Rectangle> dictionary = new Dictionary<CannonBallInfoEffects, Rectangle>();
			dictionary[CannonBallInfoEffects.CanMakeAnyBurnings] = new Rectangle(1, 403, 22, 22);
			dictionary[CannonBallInfoEffects.FireArea] = new Rectangle(24, 403, 22, 22);
			dictionary[CannonBallInfoEffects.Bombershell] = new Rectangle(47, 403, 22, 22);
			dictionary[CannonBallInfoEffects.CanMakeCannonsDamage] = new Rectangle(70, 403, 22, 22);
			dictionary[CannonBallInfoEffects.ImprovedDamageBuildings] = new Rectangle(93, 403, 22, 22);
			CommonAtlas.CannonBallExtraEffectsIcons = dictionary;
		}

		// Token: 0x04001BF2 RID: 7154
		public static Texture2DAtlas Texture;

		// Token: 0x04001BF3 RID: 7155
		public static readonly Rectangle topVensil = new Rectangle(1, 272, 442, 90);

		// Token: 0x04001BF4 RID: 7156
		public static readonly Rectangle dynamicBackground = new Rectangle(2518, 3612, 256, 256);

		// Token: 0x04001BF5 RID: 7157
		public static readonly Rectangle dynamicBackground2Particled = new Rectangle(1454, 3728, 256, 256);

		// Token: 0x04001BF6 RID: 7158
		public static readonly Rectangle backgroundSpark = new Rectangle(2552, 3870, 126, 126);

		// Token: 0x04001BF7 RID: 7159
		public static readonly Rectangle backgroundSpark2 = new Rectangle(2681, 3871, 64, 64);

		// Token: 0x04001BF8 RID: 7160
		public static readonly Rectangle topScroll = new Rectangle(0, 440, 430, 95);

		// Token: 0x04001BF9 RID: 7161
		public static readonly Rectangle backgroundDecor = new Rectangle(3534, 587, 420, 233);

		// Token: 0x04001BFA RID: 7162
		public static readonly Rectangle c_yellowNotifMark = new Rectangle(58, 113, 22, 14);

		// Token: 0x04001BFB RID: 7163
		public static readonly Rectangle goldIconSingle64 = new Rectangle(1232, 94, 64, 64);

		// Token: 0x04001BFC RID: 7164
		public static readonly Rectangle goldIconSingle32 = new Rectangle(1232, 159, 32, 32);

		// Token: 0x04001BFD RID: 7165
		public static readonly Rectangle goldIconSingleWithBackground40 = new Rectangle(1359, 135, 40, 40);

		// Token: 0x04001BFE RID: 7166
		public static readonly Rectangle goldIconMany64 = new Rectangle(1265, 159, 64, 64);

		// Token: 0x04001BFF RID: 7167
		public static readonly Rectangle doublonesIcon = new Rectangle(339, 46, 28, 28);

		// Token: 0x04001C00 RID: 7168
		public static readonly Rectangle unknownUnit = new Rectangle(235, 66, 32, 32);

		// Token: 0x04001C01 RID: 7169
		public static readonly Rectangle monetsIcon = new Rectangle(0, 1453, 128, 128);

		// Token: 0x04001C02 RID: 7170
		public static readonly Rectangle rewardIcon = new Rectangle(1230, 192, 34, 34);

		// Token: 0x04001C03 RID: 7171
		public static readonly Rectangle rewardDoneIcon = new Rectangle(1330, 189, 34, 34);

		// Token: 0x04001C04 RID: 7172
		public static readonly Rectangle arrowBlack_left = new Rectangle(146, 155, 35, 26);

		// Token: 0x04001C05 RID: 7173
		public static readonly Rectangle arrowBlack_right = new Rectangle(182, 155, 35, 26);

		// Token: 0x04001C06 RID: 7174
		public static readonly Rectangle tt_arrowKeys_left = new Rectangle(464, 53, 40, 70);

		// Token: 0x04001C07 RID: 7175
		public static readonly Rectangle tt_arrowKeys_right = new Rectangle(505, 53, 40, 70);

		// Token: 0x04001C08 RID: 7176
		public static readonly Rectangle leftArrowKey30 = new Rectangle(464, 53, 40, 70);

		// Token: 0x04001C09 RID: 7177
		public static readonly Rectangle rightArrowKey30 = new Rectangle(31, 57, 30, 30);

		// Token: 0x04001C0A RID: 7178
		public static readonly Rectangle newToolList_main = new Rectangle(637, 0, 231, 41);

		// Token: 0x04001C0B RID: 7179
		public static readonly Rectangle newToolList_item = new Rectangle(637, 42, 231, 35);

		// Token: 0x04001C0C RID: 7180
		public static readonly Rectangle newToolList_item_cutted = new Rectangle(637, 44, 231, 18);

		// Token: 0x04001C0D RID: 7181
		public static readonly Rectangle c_scrollUp = new Rectangle(2822, 1705, 20, 20);

		// Token: 0x04001C0E RID: 7182
		public static readonly Rectangle c_scrollDown = new Rectangle(2822, 1726, 20, 20);

		// Token: 0x04001C0F RID: 7183
		public static readonly Rectangle c_scrollPoint = new Rectangle(2843, 1705, 20, 37);

		// Token: 0x04001C10 RID: 7184
		public static readonly Rectangle plusButtonYellow = new Rectangle(106, 0, 48, 48);

		// Token: 0x04001C11 RID: 7185
		public static readonly Rectangle plusButtonGray = new Rectangle(106, 49, 48, 48);

		// Token: 0x04001C12 RID: 7186
		public static readonly Rectangle whitePixel = new Rectangle(0, 0, 1, 1);

		// Token: 0x04001C13 RID: 7187
		public static readonly Rectangle whiteDot = new Rectangle(261, 158, 28, 28);

		// Token: 0x04001C14 RID: 7188
		public static readonly Rectangle pBlood = new Rectangle(64, 80, 24, 24);

		// Token: 0x04001C15 RID: 7189
		public static readonly Rectangle pLight = new Rectangle(0, 88, 16, 16);

		// Token: 0x04001C16 RID: 7190
		public static readonly Rectangle transpPixel = new Rectangle(10, 0, 1, 1);

		// Token: 0x04001C17 RID: 7191
		public static readonly Rectangle craftTimeIcon = new Rectangle(1359, 94, 40, 40);

		// Token: 0x04001C18 RID: 7192
		public static readonly Rectangle craftTimeIcon_transp = new Rectangle(1358, 237, 40, 40);

		// Token: 0x04001C19 RID: 7193
		public static readonly Rectangle bigProgressBarGradient = new Rectangle(0, 128, 295, 26);

		// Token: 0x04001C1A RID: 7194
		public static readonly Rectangle bigProgressBarGradient_point = new Rectangle(296, 128, 27, 27);

		// Token: 0x04001C1B RID: 7195
		public static readonly Rectangle xpIcon = new Rectangle(301, 66, 32, 32);

		// Token: 0x04001C1C RID: 7196
		public static readonly Rectangle shipXpIcon = new Rectangle(301, 0, 32, 32);

		// Token: 0x04001C1D RID: 7197
		public static readonly Rectangle shipXpIconBig = new Rectangle(1333, 477, 64, 64);

		// Token: 0x04001C1E RID: 7198
		public static readonly Rectangle conquerorBadgeIcon = new Rectangle(503, 0, 52, 52);

		// Token: 0x04001C1F RID: 7199
		public static readonly Rectangle progress1_back = new Rectangle(1785, 504, 216, 19);

		// Token: 0x04001C20 RID: 7200
		public static readonly Rectangle progress1_frontGreen = new Rectangle(1785, 524, 216, 19);

		// Token: 0x04001C21 RID: 7201
		public static readonly Rectangle progress1_frontOrange = new Rectangle(1785, 544, 216, 19);

		// Token: 0x04001C22 RID: 7202
		public static readonly Rectangle progress1_frontBlue = new Rectangle(1785, 564, 216, 19);

		// Token: 0x04001C23 RID: 7203
		public static readonly Rectangle progressBar16FillPoint = new Rectangle(18, 109, 19, 18);

		// Token: 0x04001C24 RID: 7204
		public static readonly Rectangle progressBar16UnFillPoint = new Rectangle(38, 109, 19, 18);

		// Token: 0x04001C25 RID: 7205
		public static readonly Rectangle vd_checkBox_26px_true = new Rectangle(46, 0, 26, 26);

		// Token: 0x04001C26 RID: 7206
		public static readonly Rectangle vd_checkBox_26px_false = new Rectangle(20, 0, 26, 26);

		// Token: 0x04001C27 RID: 7207
		public static readonly Rectangle vd_checkBox_26px_disable = new Rectangle(20, 26, 26, 26);

		// Token: 0x04001C28 RID: 7208
		public static readonly Rectangle checkboxPencil_true = new Rectangle(74, 27, 25, 25);

		// Token: 0x04001C29 RID: 7209
		public static readonly Rectangle checkboxPencil_false = new Rectangle(48, 27, 25, 25);

		// Token: 0x04001C2A RID: 7210
		public static readonly Rectangle basicButton = new Rectangle(0, 223, 158, 37);

		// Token: 0x04001C2B RID: 7211
		public static readonly Rectangle c_textCadreLeft = new Rectangle(425, 0, 76, 17);

		// Token: 0x04001C2C RID: 7212
		public static readonly Rectangle c_textCadreRight = new Rectangle(425, 18, 76, 17);

		// Token: 0x04001C2D RID: 7213
		public static readonly Rectangle c_line_green = new Rectangle(394, 1268, 113, 19);

		// Token: 0x04001C2E RID: 7214
		public static readonly Rectangle c_line_green_light = new Rectangle(394, 1188, 113, 19);

		// Token: 0x04001C2F RID: 7215
		public static readonly Rectangle c_line_yellow = new Rectangle(394, 1228, 113, 19);

		// Token: 0x04001C30 RID: 7216
		public static readonly Rectangle c_line_red = new Rectangle(394, 1248, 113, 19);

		// Token: 0x04001C31 RID: 7217
		public static readonly Rectangle shadow = new Rectangle(3415, 536, 345, 42);

		// Token: 0x04001C32 RID: 7218
		public static readonly Rectangle WinterWindowSnowLong = new Rectangle(3135, 3913, 960, 60);

		// Token: 0x04001C33 RID: 7219
		public static readonly Rectangle WinterWindowSnowShort = new Rectangle(3134, 3974, 480, 60);

		// Token: 0x04001C34 RID: 7220
		public static readonly Rectangle HalloweenWindowDecorLong = new Rectangle(3135, 4035, 960, 60);

		// Token: 0x04001C35 RID: 7221
		public static readonly Rectangle HalloweenWindowDecorShort = new Rectangle(3615, 3974, 480, 60);

		// Token: 0x04001C36 RID: 7222
		public static readonly Rectangle auctionOrderBackground = new Rectangle(610, 330, 120, 70);

		// Token: 0x04001C37 RID: 7223
		public static readonly Rectangle arenaCurrencyIcon = new Rectangle(907, 91, 65, 65);

		// Token: 0x04001C38 RID: 7224
		public static readonly Rectangle[] c_ranks = new Rectangle[]
		{
			new Rectangle(1720, 3167, 42, 50),
			new Rectangle(1763, 3167, 42, 50),
			new Rectangle(1806, 3167, 42, 50),
			new Rectangle(1849, 3167, 42, 50),
			new Rectangle(1892, 3167, 42, 50),
			new Rectangle(1935, 3167, 42, 50),
			new Rectangle(1978, 3167, 42, 50)
		};

		// Token: 0x04001C39 RID: 7225
		public static readonly Rectangle[] captainIcons = new Rectangle[]
		{
			new Rectangle(0, 689, 64, 64),
			new Rectangle(65, 689, 64, 64),
			new Rectangle(130, 689, 64, 64),
			new Rectangle(195, 689, 64, 64),
			new Rectangle(260, 689, 64, 64),
			new Rectangle(325, 689, 64, 64),
			new Rectangle(390, 689, 64, 64),
			new Rectangle(455, 689, 64, 64),
			new Rectangle(520, 689, 64, 64),
			new Rectangle(564, 623, 64, 64),
			new Rectangle(564, 558, 64, 64),
			new Rectangle(564, 493, 64, 64),
			new Rectangle(565, 428, 64, 64),
			new Rectangle(498, 409, 64, 64)
		};

		// Token: 0x04001C3A RID: 7226
		public static readonly Rectangle[] shipClasses_miniBlue = new Rectangle[]
		{
			new Rectangle(0, 930, 66, 66),
			new Rectangle(67, 930, 66, 66),
			new Rectangle(134, 930, 66, 66),
			new Rectangle(201, 930, 66, 66),
			new Rectangle(268, 930, 66, 66),
			new Rectangle(335, 930, 66, 66)
		};

		// Token: 0x04001C3B RID: 7227
		public static readonly Rectangle[] shipClasses_miniSilver = new Rectangle[]
		{
			new Rectangle(0, 1945, 66, 66),
			new Rectangle(67, 1945, 66, 66),
			new Rectangle(134, 1945, 66, 66),
			new Rectangle(201, 1945, 66, 66),
			new Rectangle(268, 1945, 66, 66),
			new Rectangle(335, 1945, 66, 66)
		};

		// Token: 0x04001C3C RID: 7228
		public static readonly Rectangle[] shipClasses_miniGold = new Rectangle[]
		{
			new Rectangle(0, 863, 66, 66),
			new Rectangle(67, 863, 66, 66),
			new Rectangle(134, 863, 66, 66),
			new Rectangle(201, 863, 66, 66),
			new Rectangle(268, 863, 66, 66),
			new Rectangle(335, 863, 66, 66)
		};

		// Token: 0x04001C3D RID: 7229
		public static readonly Rectangle[] shipClasses_miniClosed = new Rectangle[]
		{
			new Rectangle(0, 1382, 66, 66),
			new Rectangle(67, 1382, 66, 66),
			new Rectangle(134, 1382, 66, 66),
			new Rectangle(201, 1382, 66, 66),
			new Rectangle(268, 1382, 66, 66),
			new Rectangle(335, 1382, 66, 66)
		};

		// Token: 0x04001C3E RID: 7230
		public static readonly Rectangle[] shipClasses_miniViolet = new Rectangle[]
		{
			new Rectangle(0, 1878, 66, 66),
			new Rectangle(67, 1878, 66, 66),
			new Rectangle(134, 1878, 66, 66),
			new Rectangle(201, 1878, 66, 66),
			new Rectangle(268, 1878, 66, 66),
			new Rectangle(335, 1878, 66, 66)
		};

		// Token: 0x04001C3F RID: 7231
		public static readonly Rectangle[] shipClasses_miniYellow = new Rectangle[]
		{
			new Rectangle(10, 1830, 32, 32),
			new Rectangle(50, 1830, 32, 32),
			new Rectangle(90, 1830, 32, 32),
			new Rectangle(130, 1830, 32, 32),
			new Rectangle(170, 1830, 32, 32),
			new Rectangle(210, 1830, 32, 32),
			new Rectangle(250, 1830, 32, 32)
		};

		// Token: 0x04001C40 RID: 7232
		public static Dictionary<CannonBallInfoEffects, Rectangle> CannonBallExtraEffectsIcons;

		// Token: 0x020004FB RID: 1275
		public enum ShipClassIconStyle
		{
			// Token: 0x04001C42 RID: 7234
			Blue,
			// Token: 0x04001C43 RID: 7235
			Silver,
			// Token: 0x04001C44 RID: 7236
			Gold,
			// Token: 0x04001C45 RID: 7237
			Empire,
			// Token: 0x04001C46 RID: 7238
			Closed,
			// Token: 0x04001C47 RID: 7239
			Yellow
		}
	}
}
