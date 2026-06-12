using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Graphics;
using TheraEngine.Core;
using WorldOfSeaBattles;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x020004FC RID: 1276
	internal static class OtherTextures
	{
		// Token: 0x06001C53 RID: 7251 RVA: 0x0010464C File Offset: 0x0010284C
		public static void Load(ContentManager {25296})
		{
			Texture2D {15908} = {25296}.Load<Texture2D>("filler.fx");
			OtherTextures.StartConfugirator = new VirtualTexture({15908}, "StartConfigurator", PathContent.dir_textures + "StartConfigurator");
			OtherTextures.ToolTipTexture = {25296}.Load<Texture2D>(PathContent.dir_textures + "ToolTip");
			OtherTextures.Images = {25296}.Load<Texture2D>(PathContent.dir_textures + "Images");
			OtherTextures.WorldMapUiElements = {25296}.Load<Texture2D>(PathContent.dir_textures + "WorldMap");
			OtherTextures.WorldMap = new VirtualTexture({15908}, "worldmap", PathContent.dir_textures + "map");
			OtherTextures.WorldMapClosed = new VirtualTexture({15908}, "worldmap", PathContent.dir_textures + "map_closed");
			OtherTextures.WorldMapMini = {25296}.Load<Texture2D>(PathContent.dir_textures + "map_levels");
			OtherTextures.ChatAtl = {25296}.Load<Texture2D>(PathContent.dir_textures + "ChatAtl");
			OtherTextures.GameLogoOverlay = new VirtualTexture({15908}, "gamelogo", PathContent.dir_textures + "OverlayGameLogo");
			OtherTextures.DecalWinterIcebergs = new VirtualTexture({15908}, "decalice", PathContent.dir_textures + "rwocean\\icebdecal");
			OtherTextures.DecalFish = new VirtualTexture({15908}, "decalice", PathContent.dir_textures + "rwocean\\fishdecal");
			OtherTextures.WhaleHarpoonSight = new VirtualTexture({15908}, "decalice", PathContent.dir_textures + "rwocean\\whaleHarpoonSight");
			OtherTextures.DecalFirearea = new VirtualTexture({15908}, "decalice", PathContent.dir_textures + "rwocean\\firedecal");
			OtherTextures.Icons = {25296}.Load<Texture2D>(PathContent.dir_textures + "Icons");
			OtherTextures.FractionIcons = {25296}.Load<Texture2D>(PathContent.dir_textures + "FractionIcons");
			OtherTextures.WorldMapMiniArena = new VirtualTexture({15908}, "map_levels_arena", PathContent.dir_textures + "map_levels_arena");
			OtherTextures.WaterDropsNormalmap = {25296}.Load<Texture2D>("Shaders\\Textures\\waterdrops");
			OtherTextures.WindTrailTexture = {25296}.Load<Texture2D>("Shaders\\Textures\\windtrail");
			OtherTextures.HazeLayerTex = {25296}.Load<Texture2D>("Textures\\rwocean\\haze");
		}

		// Token: 0x04001C48 RID: 7240
		public static VirtualTexture StartConfugirator;

		// Token: 0x04001C49 RID: 7241
		public static Texture2D ToolTipTexture;

		// Token: 0x04001C4A RID: 7242
		public static Texture2D Images;

		// Token: 0x04001C4B RID: 7243
		public static Texture2D Icons;

		// Token: 0x04001C4C RID: 7244
		public static Texture2D FractionIcons;

		// Token: 0x04001C4D RID: 7245
		public static Texture2D WorldMapUiElements;

		// Token: 0x04001C4E RID: 7246
		public static VirtualTexture WorldMap;

		// Token: 0x04001C4F RID: 7247
		public static VirtualTexture WorldMapClosed;

		// Token: 0x04001C50 RID: 7248
		public static readonly Rectangle WorldMapSize = new Rectangle(0, 0, 2404, 1996);

		// Token: 0x04001C51 RID: 7249
		public static Texture2D WorldMapMini;

		// Token: 0x04001C52 RID: 7250
		public static VirtualTexture WorldMapMiniArena;

		// Token: 0x04001C53 RID: 7251
		public static Texture2D ChatAtl;

		// Token: 0x04001C54 RID: 7252
		public static VirtualTexture GameLogoOverlay;

		// Token: 0x04001C55 RID: 7253
		public static VirtualTexture DecalWinterIcebergs;

		// Token: 0x04001C56 RID: 7254
		public static VirtualTexture DecalFish;

		// Token: 0x04001C57 RID: 7255
		public static VirtualTexture WhaleHarpoonSight;

		// Token: 0x04001C58 RID: 7256
		public static VirtualTexture DecalFirearea;

		// Token: 0x04001C59 RID: 7257
		public static Texture2D WaterDropsNormalmap;

		// Token: 0x04001C5A RID: 7258
		public static Texture2D WindTrailTexture;

		// Token: 0x04001C5B RID: 7259
		public static Texture2D HazeLayerTex;
	}
}
