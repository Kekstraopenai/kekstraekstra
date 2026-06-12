using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UWEngine.Core;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x0200019D RID: 413
	public class Texture2DAtlas : DisposableObject
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00034A4F File Offset: 0x00032C4F
		public Texture2D Tex
		{
			get
			{
				return this.texture;
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00034A57 File Offset: 0x00032C57
		private Texture2DAtlas(Texture2D {15888})
		{
			this.texture = {15888};
			if ({15888} != null)
			{
				this.Size = new Vector2((float){15888}.Width, (float){15888}.Height);
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00034A82 File Offset: 0x00032C82
		public static Texture2DAtlas Load(string {15889}, GameCore {15890})
		{
			return new Texture2DAtlas({15890}.Content.Load<Texture2D>({15889}))
			{
				AssetName = {15889}
			};
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00034A9C File Offset: 0x00032C9C
		public static Texture2DAtlas FromPNG(Texture2D {15891}, string {15892}, int {15893}, int {15894})
		{
			Texture2DAtlas result;
			using (FileStream fileStream = File.OpenRead({15892}))
			{
				result = new Texture2DAtlas(Texture2D.FromStream(Engine.Game.GraphicsDevice, fileStream, {15893}, {15894}, false))
				{
					AssetName = {15892}
				};
			}
			return result;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00034AF0 File Offset: 0x00032CF0
		public override void Dispose()
		{
			Texture2D texture2D = this.texture;
			if (texture2D != null)
			{
				texture2D.Dispose();
			}
			this.texture = null;
			base.Dispose();
		}

		// Token: 0x04000807 RID: 2055
		internal Texture2D texture;

		// Token: 0x04000808 RID: 2056
		public string AssetName;

		// Token: 0x04000809 RID: 2057
		public readonly Vector2 Size;
	}
}
