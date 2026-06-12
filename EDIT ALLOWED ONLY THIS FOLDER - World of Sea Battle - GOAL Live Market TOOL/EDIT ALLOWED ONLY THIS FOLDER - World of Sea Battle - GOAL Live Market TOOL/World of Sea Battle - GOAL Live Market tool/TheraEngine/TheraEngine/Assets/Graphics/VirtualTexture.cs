using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x020001A0 RID: 416
	public class VirtualTexture
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0000E21A File Offset: 0x0000C41A
		internal bool AllowUnload
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00034B20 File Offset: 0x00032D20
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x00034B59 File Offset: 0x00032D59
		public Texture2D Tex
		{
			get
			{
				this.accessCounter = 0;
				if (this.{15916} != null)
				{
					return this.{15916};
				}
				if (!this.{15917})
				{
					VirtualTextureHelper.Run(this, this.{15918});
					this.{15917} = true;
				}
				return this.{15919};
			}
			set
			{
				this.{15917} = true;
				this.{15916} = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00034B69 File Offset: 0x00032D69
		public bool LoadQuery
		{
			get
			{
				if (this.{15916} != null)
				{
					return true;
				}
				if (!this.{15917})
				{
					VirtualTextureHelper.Run(this, this.{15918});
					this.{15917} = true;
				}
				return false;
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00034B91 File Offset: 0x00032D91
		public VirtualTexture(Texture2D {15908}, string {15909}, string {15910})
		{
			this.{15919} = {15908};
			this.{15918} = new VirtualTextureSource({15910}, VirtualSourceType.FileSystem);
			this.AssetName = {15909};
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00034BB4 File Offset: 0x00032DB4
		public VirtualTexture(Texture2D {15911}, string {15912}, VirtualTextureSource {15913})
		{
			this.{15919} = {15911};
			this.{15918} = {15913};
			this.AssetName = {15912};
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00034BD1 File Offset: 0x00032DD1
		public VirtualTexture(string {15914}, Texture2D {15915})
		{
			this.Tex = {15915};
			this.AssetName = {15914};
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00034BE7 File Offset: 0x00032DE7
		public void Unload()
		{
			Texture2D texture2D = this.{15916};
			if (texture2D != null)
			{
				texture2D.Dispose();
			}
			this.{15916} = null;
			this.{15917} = false;
		}

		// Token: 0x04000810 RID: 2064
		public string AssetName;

		// Token: 0x04000811 RID: 2065
		private Texture2D {15916};

		// Token: 0x04000812 RID: 2066
		private bool {15917};

		// Token: 0x04000813 RID: 2067
		internal int accessCounter;

		// Token: 0x04000814 RID: 2068
		private VirtualTextureSource {15918};

		// Token: 0x04000815 RID: 2069
		private Texture2D {15919};
	}
}
