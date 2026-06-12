using System;

namespace TheraEngine.Collections
{
	// Token: 0x0200010F RID: 271
	public class ObservableTlist<Y> : Tlist<Y>
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x00027248 File Offset: 0x00025448
		public ObservableTlist()
		{
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00027250 File Offset: 0x00025450
		public ObservableTlist(Y[] array) : base(array)
		{
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00027259 File Offset: 0x00025459
		public ObservableTlist(Tlist<Y> array) : base(array.Size)
		{
			array.CopyTo(ref array.Array, array.Size);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00027279 File Offset: 0x00025479
		[Obsolete("You can't modify collection", true)]
		public new void Add(in Y value)
		{
			base.Add(value);
		}
	}
}
