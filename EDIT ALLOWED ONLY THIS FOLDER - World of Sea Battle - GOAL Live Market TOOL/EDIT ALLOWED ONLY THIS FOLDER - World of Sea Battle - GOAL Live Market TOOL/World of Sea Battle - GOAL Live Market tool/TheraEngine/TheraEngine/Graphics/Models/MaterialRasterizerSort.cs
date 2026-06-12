using System;
using System.Collections.Generic;

namespace TheraEngine.Graphics.Models
{
	// Token: 0x02000164 RID: 356
	internal class MaterialRasterizerSort : IComparer<ModelPartShadercall>
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x0002E4B3 File Offset: 0x0002C6B3
		int IComparer<ModelPartShadercall>.{15165}(ModelPartShadercall {15166}, ModelPartShadercall {15167})
		{
			return ({15166}.Material.Properties.RasterizerOptions != {15167}.Material.Properties.RasterizerOptions) ? 1 : 0;
		}
	}
}
