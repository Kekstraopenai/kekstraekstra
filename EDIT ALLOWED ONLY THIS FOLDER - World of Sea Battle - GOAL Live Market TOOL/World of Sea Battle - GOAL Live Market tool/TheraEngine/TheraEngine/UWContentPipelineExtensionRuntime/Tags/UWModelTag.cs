using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Content;

namespace UWContentPipelineExtensionRuntime.Tags
{
	// Token: 0x02000013 RID: 19
	public class UWModelTag
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000309B File Offset: 0x0000129B
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000030A3 File Offset: 0x000012A3
		[ContentSerializer]
		public string ModelName { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000030AC File Offset: 0x000012AC
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000030B4 File Offset: 0x000012B4
		[ContentSerializer]
		public bool AllowColor { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000030BD File Offset: 0x000012BD
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000030C5 File Offset: 0x000012C5
		[ContentSerializer]
		public bool IsSupportedMaterials { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000030CE File Offset: 0x000012CE
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000030D6 File Offset: 0x000012D6
		[ContentSerializer]
		public ModelHardpoint[] Hardpoints { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000030DF File Offset: 0x000012DF
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000030E7 File Offset: 0x000012E7
		[ContentSerializer]
		public SkinningData SkinningAnimation { get; private set; }

		// Token: 0x06000063 RID: 99 RVA: 0x000030F0 File Offset: 0x000012F0
		public UWModelTag(string {11335}, bool {11336}, bool {11337}, List<ModelHardpoint> {11338}, SkinningData {11339}, float {11340})
		{
			this.ModelName = {11335};
			this.AllowColor = {11336};
			this.IsSupportedMaterials = {11337};
			this.Hardpoints = {11338}.ToArray();
			this.SkinningAnimation = {11339};
			this.Scale = {11340};
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002EF6 File Offset: 0x000010F6
		private UWModelTag()
		{
		}

		// Token: 0x04000058 RID: 88
		[CompilerGenerated]
		private string {11341};

		// Token: 0x04000059 RID: 89
		[CompilerGenerated]
		private bool {11342};

		// Token: 0x0400005A RID: 90
		[CompilerGenerated]
		private bool {11343};

		// Token: 0x0400005B RID: 91
		[CompilerGenerated]
		private ModelHardpoint[] {11344};

		// Token: 0x0400005C RID: 92
		[CompilerGenerated]
		private SkinningData {11345};

		// Token: 0x0400005D RID: 93
		[ContentSerializer]
		public float Scale;
	}
}
