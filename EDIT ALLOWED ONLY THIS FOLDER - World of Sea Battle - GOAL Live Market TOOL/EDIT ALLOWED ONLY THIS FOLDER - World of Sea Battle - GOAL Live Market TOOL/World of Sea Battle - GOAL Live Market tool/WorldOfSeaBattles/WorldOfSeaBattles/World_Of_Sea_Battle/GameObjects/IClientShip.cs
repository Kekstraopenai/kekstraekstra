using System;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x0200005E RID: 94
	internal interface IClientShip
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002DA RID: 730
		bool MakeTransparentForMe { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002DB RID: 731
		ShipPartial GetClient { get; }

		// Token: 0x060002DC RID: 732
		void Render2D();
	}
}
