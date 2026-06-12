using System;
using TheraEngine.Assets.Graphics;

namespace TheraEngine.Graphics.Models
{
	// Token: 0x0200015F RID: 351
	public class Plastic : BlinnPhong
	{
		// Token: 0x06000974 RID: 2420 RVA: 0x0002DE40 File Offset: 0x0002C040
		public Plastic() : base(16f, 1.8f, 1f, false, MaterialRasterizeOptions.DoublesidedDefault)
		{
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0002DE59 File Offset: 0x0002C059
		public Plastic(MaterialRasterizeOptions {15145}) : base(16f, 1.8f, 1f, false, {15145})
		{
		}
	}
}
