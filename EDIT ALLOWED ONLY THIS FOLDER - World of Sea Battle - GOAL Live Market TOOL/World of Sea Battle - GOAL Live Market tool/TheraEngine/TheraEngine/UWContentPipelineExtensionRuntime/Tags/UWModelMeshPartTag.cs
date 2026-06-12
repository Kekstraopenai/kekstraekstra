using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace UWContentPipelineExtensionRuntime.Tags
{
	// Token: 0x02000011 RID: 17
	public class UWModelMeshPartTag
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000304B File Offset: 0x0000124B
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00003053 File Offset: 0x00001253
		[ContentSerializer]
		public SegmentTextureInfoTag TextureInfo { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000305C File Offset: 0x0000125C
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00003064 File Offset: 0x00001264
		[ContentSerializer]
		public BoundingSphere LocalBoundingSphere { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000306D File Offset: 0x0000126D
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00003075 File Offset: 0x00001275
		[ContentSerializer]
		public BoundingBox LocalBoundingBox { get; private set; }

		// Token: 0x06000056 RID: 86 RVA: 0x0000307E File Offset: 0x0000127E
		public UWModelMeshPartTag(SegmentTextureInfoTag {11318}, BoundingSphere {11319}, BoundingBox {11320})
		{
			this.TextureInfo = {11318};
			this.LocalBoundingSphere = {11319};
			this.LocalBoundingBox = {11320};
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EF6 File Offset: 0x000010F6
		private UWModelMeshPartTag()
		{
		}

		// Token: 0x04000053 RID: 83
		[CompilerGenerated]
		private SegmentTextureInfoTag {11321};

		// Token: 0x04000054 RID: 84
		[CompilerGenerated]
		private BoundingSphere {11322};

		// Token: 0x04000055 RID: 85
		[CompilerGenerated]
		private BoundingBox {11323};
	}
}
