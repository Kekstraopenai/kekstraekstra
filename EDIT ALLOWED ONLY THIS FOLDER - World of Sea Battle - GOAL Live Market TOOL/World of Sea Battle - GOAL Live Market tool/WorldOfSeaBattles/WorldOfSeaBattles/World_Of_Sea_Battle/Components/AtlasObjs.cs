using System;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Graphics;
using TheraEngine.Scene.ParticleSystem;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x020004F7 RID: 1271
	internal static class AtlasObjs
	{
		// Token: 0x06001C43 RID: 7235 RVA: 0x001020AC File Offset: 0x001002AC
		public static Rectangle GetCaptainTitleIcon(CaptainTitle {25278})
		{
			if ({25278} <= CaptainTitle.Achievement4)
			{
				if ({25278} == CaptainTitle.None)
				{
					return Rectangle.Empty;
				}
				if ({25278} == CaptainTitle.Achievement2)
				{
					return new Rectangle(260, 594, 64, 64);
				}
				if ({25278} == CaptainTitle.Achievement4)
				{
					return new Rectangle(390, 594, 64, 64);
				}
			}
			else if ({25278} <= CaptainTitle.Achievement1)
			{
				if ({25278} == CaptainTitle.Achievement3)
				{
					return new Rectangle(325, 594, 64, 64);
				}
				if ({25278} == CaptainTitle.Achievement1)
				{
					return new Rectangle(130, 594, 64, 64);
				}
			}
			else
			{
				if ({25278} == CaptainTitle.Achievement5)
				{
					return new Rectangle(455, 594, 64, 64);
				}
				switch ({25278})
				{
				case CaptainTitle.HaveRank30:
					return new Rectangle(0, 594, 64, 64);
				case CaptainTitle.Have30Ships:
					return new Rectangle(65, 594, 64, 64);
				case CaptainTitle.Have2PersonalIsles:
					return new Rectangle(195, 594, 64, 64);
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x001021B8 File Offset: 0x001003B8
		public static void GetPathForCannonBall(int {25279}, out Rectangle {25280})
		{
			switch ({25279})
			{
			case 1:
			case 6:
			case 8:
			case 11:
			case 19:
			case 20:
				{25280} = new Rectangle(1, 1, 164, 164);
				return;
			case 2:
				{25280} = new Rectangle(166, 1, 164, 164);
				return;
			case 3:
				{25280} = new Rectangle(661, 1, 164, 164);
				return;
			case 4:
				{25280} = new Rectangle(492, 1, 164, 164);
				return;
			case 5:
				{25280} = new Rectangle(992, 1, 164, 164);
				return;
			case 7:
				{25280} = new Rectangle(1413, 146, 164, 164);
				return;
			case 9:
				{25280} = new Rectangle(166, 1, 164, 164);
				return;
			case 10:
				{25280} = new Rectangle(992, 1, 164, 164);
				return;
			case 12:
				{25280} = AtlasObjs.Particles.p_Flame.GetPath();
				return;
			case 13:
			case 18:
				{25280} = new Rectangle(212, 767, 164, 164);
				return;
			case 14:
				{25280} = new Rectangle(377, 767, 164, 164);
				return;
			case 16:
				{25280} = new Rectangle(707, 767, 164, 164);
				return;
			case 17:
				{25280} = AtlasObjs.whitepixel_1px;
				return;
			case 21:
				{25280} = new Rectangle(588, 2075, 164, 164);
				return;
			}
			Assert.Report(true, "GetPathForCannonBall id " + {25279}.ToString());
			{25280} = new Rectangle(707, 767, 164, 164);
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x001023DC File Offset: 0x001005DC
		public static Rectangle GetNpcIcon(NpcIcon {25281})
		{
			Rectangle result;
			switch ({25281})
			{
			case NpcIcon.Star:
				result = new Rectangle(218, 935, 96, 96);
				break;
			case NpcIcon.Legendary2l:
				result = new Rectangle(509, 935, 96, 96);
				break;
			case NpcIcon.Legendary3l:
				result = new Rectangle(606, 935, 96, 96);
				break;
			case NpcIcon.Blades:
				result = new Rectangle(315, 935, 96, 96);
				break;
			case NpcIcon.Skull:
				result = new Rectangle(412, 935, 96, 96);
				break;
			default:
				result = default(Rectangle);
				break;
			}
			return result;
		}

		// Token: 0x04001B78 RID: 7032
		public static Texture2DAtlas Texture;

		// Token: 0x04001B79 RID: 7033
		public static readonly float MarkerSightingRedScaleX = 0.4722222f;

		// Token: 0x04001B7A RID: 7034
		public static readonly float MarkerSightingRedScaleY = 0.5f;

		// Token: 0x04001B7B RID: 7035
		internal static readonly Rectangle sunTexture = new Rectangle(1633, 1636, 254, 254);

		// Token: 0x04001B7C RID: 7036
		internal static readonly Rectangle moonTexture = new Rectangle(1851, 1993, 247, 247);

		// Token: 0x04001B7D RID: 7037
		internal static readonly Rectangle moonTextureRed = new Rectangle(1858, 2377, 247, 247);

		// Token: 0x04001B7E RID: 7038
		internal static readonly Rectangle marker_treasury = new Rectangle(1520, 0, 60, 77);

		// Token: 0x04001B7F RID: 7039
		internal static readonly Rectangle marker_empty = new Rectangle(1578, 167, 60, 77);

		// Token: 0x04001B80 RID: 7040
		internal static readonly Rectangle marker_sightingRed = new Rectangle(2268, 2475, 144, 156);

		// Token: 0x04001B81 RID: 7041
		internal static readonly Rectangle marker_sightingRedFree = new Rectangle(2120, 2475, 144, 156);

		// Token: 0x04001B82 RID: 7042
		internal static readonly Rectangle marker_fishing4 = new Rectangle(1578, 247, 60, 77);

		// Token: 0x04001B83 RID: 7043
		internal static readonly Rectangle marker_fishing3 = new Rectangle(1639, 247, 60, 77);

		// Token: 0x04001B84 RID: 7044
		internal static readonly Rectangle marker_fishing2 = new Rectangle(1700, 247, 60, 77);

		// Token: 0x04001B85 RID: 7045
		internal static readonly Rectangle marker_fishing1 = new Rectangle(1761, 247, 60, 77);

		// Token: 0x04001B86 RID: 7046
		internal static readonly Rectangle marker_whale = new Rectangle(1823, 247, 60, 77);

		// Token: 0x04001B87 RID: 7047
		internal static readonly Rectangle marker_isleDrop = new Rectangle(1459, 0, 60, 77);

		// Token: 0x04001B88 RID: 7048
		internal static readonly Rectangle c_shieldIcon = new Rectangle(465, 320, 51, 51);

		// Token: 0x04001B89 RID: 7049
		internal static readonly Rectangle c_pbLabelIcon = new Rectangle(517, 424, 51, 51);

		// Token: 0x04001B8A RID: 7050
		internal static readonly Rectangle c_shipCapturedOtherIcon = new Rectangle(413, 320, 51, 51);

		// Token: 0x04001B8B RID: 7051
		internal static readonly Rectangle c_shipCapturedByMeIcon = new Rectangle(413, 372, 51, 51);

		// Token: 0x04001B8C RID: 7052
		internal static readonly Rectangle c_marchingMode = new Rectangle(465, 424, 51, 51);

		// Token: 0x04001B8D RID: 7053
		internal static readonly Rectangle с_smugglingQuestIcon = new Rectangle(516, 476, 51, 51);

		// Token: 0x04001B8E RID: 7054
		internal static readonly Rectangle p_pvpMarkerBlack = new Rectangle(456, 372, 51, 51);

		// Token: 0x04001B8F RID: 7055
		internal static readonly Rectangle p_pvpMarkerRed = new Rectangle(517, 372, 51, 51);

		// Token: 0x04001B90 RID: 7056
		internal static readonly Rectangle c_shipMarkerByMinimap_red = new Rectangle(119, 486, 15, 15);

		// Token: 0x04001B91 RID: 7057
		internal static readonly Rectangle c_shipMarkerByMinimap_green = new Rectangle(119, 435, 15, 15);

		// Token: 0x04001B92 RID: 7058
		internal static readonly Rectangle c_shipMarkerByMinimap_targetRed = new Rectangle(137, 476, 30, 30);

		// Token: 0x04001B93 RID: 7059
		internal static readonly Rectangle star_icon = new Rectangle(0, 268, 34, 34);

		// Token: 0x04001B94 RID: 7060
		internal static readonly Rectangle star_iconViolet = new Rectangle(35, 268, 34, 34);

		// Token: 0x04001B95 RID: 7061
		internal static readonly Rectangle rect_gm_skull_nocolor_32px = new Rectangle(0, 215, 32, 32);

		// Token: 0x04001B96 RID: 7062
		internal static readonly Rectangle rect_gm_skull_blue_32px = new Rectangle(38, 215, 32, 32);

		// Token: 0x04001B97 RID: 7063
		internal static readonly Rectangle rect_gm_skull_yellow_32px = new Rectangle(76, 215, 32, 32);

		// Token: 0x04001B98 RID: 7064
		internal static readonly Rectangle rect_gm_skull_red_32px = new Rectangle(76, 183, 32, 32);

		// Token: 0x04001B99 RID: 7065
		internal static readonly Rectangle rect_gm_skull_npcGroup_32px = new Rectangle(116, 183, 32, 32);

		// Token: 0x04001B9A RID: 7066
		internal static readonly Rectangle rect_gm_skull_player_32px = new Rectangle(115, 215, 32, 32);

		// Token: 0x04001B9B RID: 7067
		internal static readonly Rectangle rect_gm_trader = new Rectangle(42, 181, 32, 32);

		// Token: 0x04001B9C RID: 7068
		internal static readonly Rectangle cd_sprite_red_32px = new Rectangle(330, 169, 32, 32);

		// Token: 0x04001B9D RID: 7069
		internal static readonly Rectangle transparent_1px = new Rectangle(1, 253, 1, 1);

		// Token: 0x04001B9E RID: 7070
		internal static readonly Rectangle whitepixel_1px = new Rectangle(8, 257, 1, 1);

		// Token: 0x04001B9F RID: 7071
		internal static readonly Rectangle rect_cannonBallGradientWhite = new Rectangle(1, 167, 1, 26);

		// Token: 0x04001BA0 RID: 7072
		internal static readonly Rectangle cannonBallFlare = new Rectangle(1992, 0, 164, 52);

		// Token: 0x04001BA1 RID: 7073
		internal static readonly Rectangle rect_game_rainParticle = new Rectangle(664, 1302, 256, 256);

		// Token: 0x04001BA2 RID: 7074
		internal static readonly Rectangle rect_game_snowParticle = new Rectangle(566, 1101, 183, 195);

		// Token: 0x04001BA3 RID: 7075
		public const int cd_sprite_red_32px_count = 28;

		// Token: 0x020004F8 RID: 1272
		public static class Particles
		{
			// Token: 0x04001BA4 RID: 7076
			public static readonly RandomParticleTextureSet p_Fireworks = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(849, 1629, 164, 164),
				new Rectangle(1014, 1629, 164, 164),
				new Rectangle(1179, 1628, 164, 164),
				new Rectangle(1344, 1628, 164, 164)
			});

			// Token: 0x04001BA5 RID: 7077
			public static readonly ParticleSpriteTexture sprite_Explosion = new ParticleSpriteTexture(new Rectangle(3052, 0, 128, 128), 16, true);

			// Token: 0x04001BA6 RID: 7078
			public static readonly ParticleSpriteTexture sprite_ExplosionSmoked = new ParticleSpriteTexture(new Rectangle(3182, 0, 128, 128), 16, true);

			// Token: 0x04001BA7 RID: 7079
			public static readonly ParticleSpriteTexture sprite_Watrsplash = new ParticleSpriteTexture(new Rectangle(2121, 2266, 172, 172), 6, true);

			// Token: 0x04001BA8 RID: 7080
			public static readonly RandomParticleTextureSet smallDebris = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(2122, 2009, 235, 235)
			});

			// Token: 0x04001BA9 RID: 7081
			public static readonly RandomParticleTextureSet p_ExponentialLight = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(1, 2250, 256, 256)
			});

			// Token: 0x04001BAA RID: 7082
			public static readonly RandomParticleTextureSet p_Cloud = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(1960, 1734, 256, 256),
				new Rectangle(2216, 1734, 256, 256),
				new Rectangle(2472, 1734, 256, 256)
			});

			// Token: 0x04001BAB RID: 7083
			public static readonly RandomParticleTextureSet p_Cloud_contrast = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(1, 1794, 128, 128),
				new Rectangle(1, 1922, 128, 128),
				new Rectangle(1, 2050, 128, 128)
			});

			// Token: 0x04001BAC RID: 7084
			public static readonly RandomParticleTextureSet p_FireSparkOrange = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(258, 2179, 157, 157),
				new Rectangle(258, 2349, 157, 157)
			});

			// Token: 0x04001BAD RID: 7085
			public static readonly RandomParticleTextureSet p_FireSparkVioletBlue = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(422, 2179, 157, 157),
				new Rectangle(422, 2349, 157, 157)
			});

			// Token: 0x04001BAE RID: 7086
			public static readonly RandomParticleTextureSet p_DarkExplosSmoke = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(2600, 2073, 171, 171),
				new Rectangle(2772, 2073, 171, 171)
			});

			// Token: 0x04001BAF RID: 7087
			public static readonly RandomParticleTextureSet p_LightExplosSmoke = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(2944, 2087, 157, 157)
			});

			// Token: 0x04001BB0 RID: 7088
			public static readonly RandomParticleTextureSet p_LiteSmoke = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(259, 1795, 128, 128),
				new Rectangle(259, 1924, 128, 128),
				new Rectangle(259, 2053, 128, 128)
			});

			// Token: 0x04001BB1 RID: 7089
			public static readonly RandomParticleTextureSet p_TwistSmoke = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(388, 1795, 128, 128),
				new Rectangle(388, 1924, 128, 128)
			});

			// Token: 0x04001BB2 RID: 7090
			public static readonly RandomParticleTextureSet p_Light = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(517, 1795, 128, 128)
			});

			// Token: 0x04001BB3 RID: 7091
			public static readonly RandomParticleTextureSet p_DarkDot = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(1, 2225, 24, 24)
			});

			// Token: 0x04001BB4 RID: 7092
			public static readonly RandomParticleTextureSet p_Flame = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(775, 1795, 128, 128),
				new Rectangle(775, 1924, 128, 128),
				new Rectangle(775, 2053, 128, 128),
				new Rectangle(775, 2182, 128, 128),
				new Rectangle(775, 2311, 128, 128),
				new Rectangle(775, 2440, 128, 128)
			});

			// Token: 0x04001BB5 RID: 7093
			public static readonly RandomParticleTextureSet p_Circle = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(904, 1795, 128, 128)
			});

			// Token: 0x04001BB6 RID: 7094
			public static readonly RandomParticleTextureSet p_SparkRed = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(1033, 1795, 128, 128)
			});

			// Token: 0x04001BB7 RID: 7095
			public static readonly RandomParticleTextureSet p_SprayUpper = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(1165, 1795, 254, 254),
				new Rectangle(906, 2147, 254, 254)
			});

			// Token: 0x04001BB8 RID: 7096
			public static readonly RandomParticleTextureSet p_SprayCommon = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(1164, 2051, 254, 254),
				new Rectangle(1164, 2308, 254, 254)
			});

			// Token: 0x04001BB9 RID: 7097
			public static readonly RandomParticleTextureSet p_Blood = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(910, 1930, 231, 214)
			});

			// Token: 0x04001BBA RID: 7098
			public static readonly RandomParticleTextureSet p_sparks = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(2363, 2009, 235, 235)
			});

			// Token: 0x04001BBB RID: 7099
			public static readonly RandomParticleTextureSet p_BloodOnFloor = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(405, 1561, 214, 214),
				new Rectangle(620, 1561, 214, 214)
			});

			// Token: 0x04001BBC RID: 7100
			public static readonly RandomParticleTextureSet p_SparksMuch = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(2384, 1301, 429, 429)
			});

			// Token: 0x04001BBD RID: 7101
			public static readonly RandomParticleTextureSet p_Lighting = new RandomParticleTextureSet(new Rectangle[]
			{
				new Rectangle(928, 1305, 314, 315)
			});
		}
	}
}
