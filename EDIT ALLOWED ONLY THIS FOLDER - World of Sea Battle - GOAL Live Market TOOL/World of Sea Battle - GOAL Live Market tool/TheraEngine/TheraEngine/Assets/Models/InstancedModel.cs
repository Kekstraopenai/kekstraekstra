using System;
using TheraEngine.Graphics.Models;

namespace TheraEngine.Assets.Models
{
	// Token: 0x02000196 RID: 406
	public class InstancedModel
	{
		// Token: 0x06000A61 RID: 2657 RVA: 0x0003408C File Offset: 0x0003228C
		public InstancedModel(UWModel {15807}, UWModel {15808})
		{
			this.Model = {15807};
			this.ModelLod1 = {15808};
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00002EF6 File Offset: 0x000010F6
		public InstancedModel()
		{
		}

		// Token: 0x040007EB RID: 2027
		public UWModel Model;

		// Token: 0x040007EC RID: 2028
		public UWModel ModelLod1;
	}
}
