using System;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x0200019F RID: 415
	public struct VirtualTextureSource
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x00034B10 File Offset: 0x00032D10
		public VirtualTextureSource(string {15897}, VirtualSourceType {15898} = VirtualSourceType.FileSystem)
		{
			this.Uri = {15897};
			this.Source = {15898};
		}

		// Token: 0x0400080E RID: 2062
		public readonly string Uri;

		// Token: 0x0400080F RID: 2063
		public readonly VirtualSourceType Source;
	}
}
