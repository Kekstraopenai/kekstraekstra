using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Content;

namespace UWContentPipelineExtensionRuntime.Tags
{
	// Token: 0x0200000F RID: 15
	public class AnimationClip
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002FAA File Offset: 0x000011AA
		public AnimationClip(TimeSpan {11290}, List<Keyframe> {11291})
		{
			this.Duration = {11290};
			this.Keyframes = {11291};
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002EF6 File Offset: 0x000010F6
		private AnimationClip()
		{
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002FC0 File Offset: 0x000011C0
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002FC8 File Offset: 0x000011C8
		[ContentSerializer]
		public TimeSpan Duration { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002FD1 File Offset: 0x000011D1
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002FD9 File Offset: 0x000011D9
		[ContentSerializer]
		public List<Keyframe> Keyframes { get; private set; }

		// Token: 0x0400004D RID: 77
		[CompilerGenerated]
		private TimeSpan {11294};

		// Token: 0x0400004E RID: 78
		[CompilerGenerated]
		private List<Keyframe> {11295};
	}
}
