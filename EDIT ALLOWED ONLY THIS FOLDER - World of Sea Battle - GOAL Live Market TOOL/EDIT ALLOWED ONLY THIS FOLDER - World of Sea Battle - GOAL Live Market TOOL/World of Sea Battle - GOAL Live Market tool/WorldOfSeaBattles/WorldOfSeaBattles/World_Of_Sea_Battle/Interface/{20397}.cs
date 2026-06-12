using System;
using Microsoft.Xna.Framework;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200027B RID: 635
	public struct {20397}
	{
		// Token: 0x06000E12 RID: 3602 RVA: 0x0007665D File Offset: 0x0007485D
		public {20397}(Rectangle {20401}, string {20402}, string {20403} = null)
		{
			this.Path = {20401};
			this.Header = {20402};
			this.Description = {20403};
			this.Centroid = false;
		}

		// Token: 0x04000D2C RID: 3372
		public Rectangle Path;

		// Token: 0x04000D2D RID: 3373
		public string Header;

		// Token: 0x04000D2E RID: 3374
		public string Description;

		// Token: 0x04000D2F RID: 3375
		public bool Centroid;
	}
}
