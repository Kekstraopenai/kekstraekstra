using System;
using Common;
using ManualPacketSerialization;
using ManualPacketSerialization.Externs;
using TheraEngine.Collections;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000523 RID: 1315
	public class SavedShipEquipmentCollection : IMPSerializable
	{
		// Token: 0x06001D66 RID: 7526 RVA: 0x0010B1C0 File Offset: 0x001093C0
		public void Boxing(WriterExtern {25536})
		{
			{25536}.WriteTlistImps(this.Items);
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x0010B1CE File Offset: 0x001093CE
		public void Unboxing(WriterExtern {25537})
		{
			{25537}.ReadTlistImps(out this.Items);
		}

		// Token: 0x04001CDE RID: 7390
		public Tlist<SavedShipEquipment> Items = new Tlist<SavedShipEquipment>();
	}
}
