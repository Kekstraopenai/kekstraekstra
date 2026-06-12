using System;
using TheraEngine.Core;

namespace TheraEngine.Graphics.Models
{
	// Token: 0x02000154 RID: 340
	public class MaterialLoadInfo
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x0002DD0B File Offset: 0x0002BF0B
		public MaterialLoadInfo(ContentManager {15125}, string {15126}, bool {15127})
		{
			if (string.IsNullOrEmpty({15126}))
			{
				throw new ArgumentNullException();
			}
			if ({15125} == null)
			{
				throw new ArgumentNullException();
			}
			this.TexturesRootDir = {15126};
			this.Content = {15125};
			this.LoadMaterials = {15127};
		}

		// Token: 0x04000666 RID: 1638
		public string TexturesRootDir;

		// Token: 0x04000667 RID: 1639
		public readonly ContentManager Content;

		// Token: 0x04000668 RID: 1640
		public readonly bool LoadMaterials;

		// Token: 0x04000669 RID: 1641
		public string NextTexturesDir;

		// Token: 0x0400066A RID: 1642
		public string DetailMapsDirName;
	}
}
