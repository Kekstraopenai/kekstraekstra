using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace UWContentPipelineExtensionRuntime.Tags
{
	// Token: 0x02000010 RID: 16
	public class SkinningData
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002FE2 File Offset: 0x000011E2
		public SkinningData(Dictionary<string, AnimationClip> {11300}, List<Matrix> {11301}, List<Matrix> {11302}, List<int> {11303})
		{
			this.AnimationClips = {11300};
			this.BindPose = {11301};
			this.InverseBindPose = {11302};
			this.SkeletonHierarchy = {11303};
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002EF6 File Offset: 0x000010F6
		private SkinningData()
		{
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003007 File Offset: 0x00001207
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000300F File Offset: 0x0000120F
		[ContentSerializer]
		public Dictionary<string, AnimationClip> AnimationClips { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003018 File Offset: 0x00001218
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00003020 File Offset: 0x00001220
		[ContentSerializer]
		public List<Matrix> BindPose { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003029 File Offset: 0x00001229
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00003031 File Offset: 0x00001231
		[ContentSerializer]
		public List<Matrix> InverseBindPose { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000303A File Offset: 0x0000123A
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00003042 File Offset: 0x00001242
		[ContentSerializer]
		public List<int> SkeletonHierarchy { get; private set; }

		// Token: 0x0400004F RID: 79
		[CompilerGenerated]
		private Dictionary<string, AnimationClip> {11308};

		// Token: 0x04000050 RID: 80
		[CompilerGenerated]
		private List<Matrix> {11309};

		// Token: 0x04000051 RID: 81
		[CompilerGenerated]
		private List<Matrix> {11310};

		// Token: 0x04000052 RID: 82
		[CompilerGenerated]
		private List<int> {11311};
	}
}
