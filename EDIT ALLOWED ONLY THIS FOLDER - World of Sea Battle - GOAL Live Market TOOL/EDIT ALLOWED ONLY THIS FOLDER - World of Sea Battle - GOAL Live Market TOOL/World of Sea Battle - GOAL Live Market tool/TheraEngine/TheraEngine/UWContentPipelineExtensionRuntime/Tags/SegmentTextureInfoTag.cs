using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Content;

namespace UWContentPipelineExtensionRuntime.Tags
{
	// Token: 0x0200000D RID: 13
	public class SegmentTextureInfoTag
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002EFE File Offset: 0x000010FE
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002F06 File Offset: 0x00001106
		[ContentSerializer]
		public string AssetName { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002F0F File Offset: 0x0000110F
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002F17 File Offset: 0x00001117
		[ContentSerializer]
		public string TextureEntryKey { get; private set; }

		// Token: 0x06000035 RID: 53 RVA: 0x00002F20 File Offset: 0x00001120
		public SegmentTextureInfoTag(string {11267}, string {11268})
		{
			this.AssetName = {11267};
			this.TextureEntryKey = {11268};
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002EF6 File Offset: 0x000010F6
		private SegmentTextureInfoTag()
		{
		}

		// Token: 0x04000048 RID: 72
		[CompilerGenerated]
		private string {11269};

		// Token: 0x04000049 RID: 73
		[CompilerGenerated]
		private string {11270};
	}
}
