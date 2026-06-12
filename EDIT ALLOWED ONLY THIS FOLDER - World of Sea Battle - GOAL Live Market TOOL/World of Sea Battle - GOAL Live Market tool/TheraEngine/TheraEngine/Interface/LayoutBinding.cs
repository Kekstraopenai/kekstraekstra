using System;

namespace TheraEngine.Interface
{
	// Token: 0x02000099 RID: 153
	public readonly struct LayoutBinding
	{
		// Token: 0x0600038F RID: 911 RVA: 0x00014010 File Offset: 0x00012210
		public LayoutBinding(PositionAlignment {12865}, PositionAlignment {12866}, float {12867}, float {12868}, bool {12869})
		{
			this.xMark = {12865};
			this.yMark = {12866};
			this.borderX = {12867};
			this.borderY = {12868};
			this.updateCentroid = {12869};
		}

		// Token: 0x040002F7 RID: 759
		public readonly PositionAlignment xMark;

		// Token: 0x040002F8 RID: 760
		public readonly PositionAlignment yMark;

		// Token: 0x040002F9 RID: 761
		public readonly float borderX;

		// Token: 0x040002FA RID: 762
		public readonly float borderY;

		// Token: 0x040002FB RID: 763
		public readonly bool updateCentroid;
	}
}
