using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace UWContentPipelineExtensionRuntime.Tags
{
	// Token: 0x0200000E RID: 14
	public class Keyframe
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002F36 File Offset: 0x00001136
		public Keyframe(int {11274}, TimeSpan {11275}, Matrix {11276})
		{
			this.Bone = {11274};
			this.Time = {11275};
			this.Transform = {11276};
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002EF6 File Offset: 0x000010F6
		private Keyframe()
		{
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002F53 File Offset: 0x00001153
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002F5B File Offset: 0x0000115B
		[ContentSerializer]
		public int Bone { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002F64 File Offset: 0x00001164
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002F6C File Offset: 0x0000116C
		[ContentSerializer]
		public TimeSpan Time { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002F75 File Offset: 0x00001175
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002F7D File Offset: 0x0000117D
		[ContentSerializer]
		public Matrix Transform { get; private set; }

		// Token: 0x0600003F RID: 63 RVA: 0x00002F86 File Offset: 0x00001186
		public static Keyframe Interpolate(int {11280}, TimeSpan {11281}, Matrix {11282}, Matrix {11283}, float {11284})
		{
			return new Keyframe({11280}, {11281}, {11282} * (1f - {11284}) + {11283} * {11284});
		}

		// Token: 0x0400004A RID: 74
		[CompilerGenerated]
		private int {11285};

		// Token: 0x0400004B RID: 75
		[CompilerGenerated]
		private TimeSpan {11286};

		// Token: 0x0400004C RID: 76
		[CompilerGenerated]
		private Matrix {11287};
	}
}
