using System;
using TheraEngine.Assets.Graphics;

namespace TheraEngine.Graphics.Models
{
	// Token: 0x02000156 RID: 342
	public class BlinnPhong : MaterialProperties
	{
		// Token: 0x0600096B RID: 2411 RVA: 0x0002DD3F File Offset: 0x0002BF3F
		public BlinnPhong(float {15133}, float {15134}, float {15135}, bool {15136}, MaterialRasterizeOptions {15137} = MaterialRasterizeOptions.DoublesidedDefault)
		{
			this.Reflectivity = {15135};
			this.SpecularIntensivity = {15134} * 1.5f;
			this.SpecularPower = {15133} / 1.5f;
			this.IsClothAnimated = {15136};
			this.RasterizerOptions = {15137};
		}
	}
}
