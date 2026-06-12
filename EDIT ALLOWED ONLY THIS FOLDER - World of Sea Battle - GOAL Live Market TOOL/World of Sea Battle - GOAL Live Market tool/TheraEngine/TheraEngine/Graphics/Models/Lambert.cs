using System;
using TheraEngine.Assets.Graphics;

namespace TheraEngine.Graphics.Models
{
	// Token: 0x0200015B RID: 347
	public class Lambert : BlinnPhong
	{
		// Token: 0x06000970 RID: 2416 RVA: 0x0002DDDC File Offset: 0x0002BFDC
		public Lambert() : base(4f, 0f, 0.1f, false, MaterialRasterizeOptions.DoublesidedDefault)
		{
		}
	}
}
