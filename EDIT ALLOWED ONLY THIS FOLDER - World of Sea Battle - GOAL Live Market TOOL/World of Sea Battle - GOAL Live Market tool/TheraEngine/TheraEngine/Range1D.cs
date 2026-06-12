using System;
using TheraEngine.Helpers;

namespace TheraEngine
{
	// Token: 0x02000028 RID: 40
	public struct Range1D
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000852C File Offset: 0x0000672C
		public float Sample
		{
			get
			{
				if (this.Start == this.End)
				{
					return this.Start;
				}
				if (this.End < this.Start)
				{
					return Rand.Range(this.End, this.Start);
				}
				return Rand.Range(this.Start, this.End);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000857F File Offset: 0x0000677F
		public Range1D(float {11755}, float {11756})
		{
			this.Start = {11755};
			this.End = {11756};
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008590 File Offset: 0x00006790
		public override bool Equals(object {11757})
		{
			if ({11757} == null)
			{
				return false;
			}
			if ({11757}.GetType() != base.GetType())
			{
				return false;
			}
			Range1D range1D = (Range1D){11757};
			return range1D.Start == this.Start && range1D.End == this.End;
		}

		// Token: 0x040000BF RID: 191
		public float Start;

		// Token: 0x040000C0 RID: 192
		public float End;
	}
}
