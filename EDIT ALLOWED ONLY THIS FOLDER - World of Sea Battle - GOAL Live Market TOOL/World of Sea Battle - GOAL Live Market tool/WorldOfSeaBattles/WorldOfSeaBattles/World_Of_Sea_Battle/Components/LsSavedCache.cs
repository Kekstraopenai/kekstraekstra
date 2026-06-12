using System;
using Common;
using ManualPacketSerialization;
using ManualPacketSerialization.Externs;
using TheraEngine.Collections;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000514 RID: 1300
	internal class LsSavedCache : IMPSerializable
	{
		// Token: 0x06001D14 RID: 7444 RVA: 0x00109DAC File Offset: 0x00107FAC
		public LsSavedCache()
		{
			this.Cache = new Tlist<LSCacheItem>();
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x00109DCC File Offset: 0x00107FCC
		public bool HasCache(uint {25463})
		{
			for (int i = 0; i < this.Cache.Size; i++)
			{
				if (this.Cache.Array[i].TargetSID == {25463})
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x00109E07 File Offset: 0x00108007
		public void Boxing(WriterExtern {25464})
		{
			{25464}.WriteTlistImps(this.Cache);
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x00109E15 File Offset: 0x00108015
		public void Unboxing(WriterExtern {25465})
		{
			{25465}.ReadTlistImps(out this.Cache);
		}

		// Token: 0x04001CB2 RID: 7346
		public const int MessagesLimit = 350;

		// Token: 0x04001CB3 RID: 7347
		public Tlist<LSCacheItem> Cache = new Tlist<LSCacheItem>();
	}
}
