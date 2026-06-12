using System;
using TheraEngine.Graphics.Models;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x020001AA RID: 426
	public struct TerrainLoadMethod
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x00036038 File Offset: 0x00034238
		public TerrainLoadMethod(int {15994}, TerrainShadingMode {15995}, string {15996}, string {15997}, string {15998}, string {15999}, MaterialProperties {16000})
		{
			this.terrainID = {15994};
			this.mode = {15995};
			this.slot1 = {15996};
			this.slot2 = {15997};
			this.slot3 = {15998};
			this.slot4 = {15999};
			this.properties = {16000};
			this.DisableFlora = false;
			this.DisableTerrain = false;
		}

		// Token: 0x04000848 RID: 2120
		public int terrainID;

		// Token: 0x04000849 RID: 2121
		public string slot1;

		// Token: 0x0400084A RID: 2122
		public string slot2;

		// Token: 0x0400084B RID: 2123
		public string slot3;

		// Token: 0x0400084C RID: 2124
		public string slot4;

		// Token: 0x0400084D RID: 2125
		public MaterialProperties properties;

		// Token: 0x0400084E RID: 2126
		public TerrainShadingMode mode;

		// Token: 0x0400084F RID: 2127
		public bool DisableFlora;

		// Token: 0x04000850 RID: 2128
		public bool DisableTerrain;
	}
}
