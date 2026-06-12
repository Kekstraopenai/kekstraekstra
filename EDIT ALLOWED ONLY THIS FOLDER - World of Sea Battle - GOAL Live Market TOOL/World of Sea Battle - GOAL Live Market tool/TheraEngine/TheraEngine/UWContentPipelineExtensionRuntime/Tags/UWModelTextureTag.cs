using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Content;

namespace UWContentPipelineExtensionRuntime.Tags
{
	// Token: 0x02000014 RID: 20
	public class UWModelTextureTag
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000312A File Offset: 0x0000132A
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003132 File Offset: 0x00001332
		[ContentSerializer]
		public string AssetName { get; private set; }

		// Token: 0x06000067 RID: 103 RVA: 0x0000313B File Offset: 0x0000133B
		public UWModelTextureTag(string {11348})
		{
			this.AssetName = {11348};
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002EF6 File Offset: 0x000010F6
		private UWModelTextureTag()
		{
		}

		// Token: 0x0400005E RID: 94
		[CompilerGenerated]
		private string {11349};
	}
}
