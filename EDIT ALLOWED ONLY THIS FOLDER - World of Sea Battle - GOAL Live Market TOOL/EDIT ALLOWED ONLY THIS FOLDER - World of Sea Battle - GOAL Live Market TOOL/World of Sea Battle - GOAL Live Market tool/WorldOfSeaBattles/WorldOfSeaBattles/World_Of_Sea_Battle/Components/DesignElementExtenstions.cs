using System;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Graphics;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000508 RID: 1288
	public static class DesignElementExtenstions
	{
		// Token: 0x06001CCE RID: 7374 RVA: 0x001082C0 File Offset: 0x001064C0
		public static VirtualTexture DesignElementTexture(ShipDesignInfo {25375})
		{
			if ({25375}.Category == ShipDesignCategory.Decal1 || {25375}.Category == ShipDesignCategory.Decal2 || {25375}.Category == ShipDesignCategory.SailTexture)
			{
				return LocalContent.GetDecalForSail({25375}, null);
			}
			if ({25375}.Category == ShipDesignCategory.Flag)
			{
				return LocalContent.GetShipFlagTexture({25375});
			}
			if ({25375}.ApartIconTex != null)
			{
				return new VirtualTexture("", {25375}.ApartIconTex);
			}
			return null;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0010831C File Offset: 0x0010651C
		public static Image DesignElementTextureIcon(this ShipDesignInfo {25376}, Marker {25377})
		{
			if ({25376}.IconTextPath.Width != 0)
			{
				return new Image({25377}, OtherTextures.Icons, {25376}.IconTextPath, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			}
			VirtualTexture tex = DesignElementExtenstions.DesignElementTexture({25376});
			if (tex != null)
			{
				Image image = new Image({25377}, () => tex.Tex, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				if ({25376}.ApartIconTex == null)
				{
					image.AddChild(new Image({25377}, OtherTextures.Icons, new Rectangle(1792, 0, 128, 128), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				}
				return image;
			}
			return null;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x001083AC File Offset: 0x001065AC
		public static Image DesignElementTextureIcon(this ShipDesignInfo {25378}, Vector2 {25379}, float {25380})
		{
			return {25378}.DesignElementTextureIcon(new Marker(ref {25379}, {25380}, {25380}));
		}

		// Token: 0x04001C81 RID: 7297
		private const bool useShipDesignRealTextures = false;
	}
}
