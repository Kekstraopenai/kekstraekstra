using System;
using System.Runtime.CompilerServices;
using TheraEngine.Graphics.Models;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x020001AC RID: 428
	public class Material
	{
		// Token: 0x06000ABB RID: 2747 RVA: 0x00036088 File Offset: 0x00034288
		public Material(VirtualTexture {16005}, VirtualTexture {16006}, VirtualTexture {16007}, MaterialProperties {16008})
		{
			this.Albedo = {16005};
			this.MaterialTx = {16006};
			this.MaterialTx2 = {16007};
			this.Properties = {16008};
			this.Flags = 0;
		}

		// Token: 0x04000856 RID: 2134
		public static readonly Material Empty = new Material(new VirtualTexture("", null), new VirtualTexture("", null), new VirtualTexture("", null), new Lambert());

		// Token: 0x04000857 RID: 2135
		public VirtualTexture Albedo;

		// Token: 0x04000858 RID: 2136
		public VirtualTexture MaterialTx;

		// Token: 0x04000859 RID: 2137
		public VirtualTexture MaterialTx2;

		// Token: 0x0400085A RID: 2138
		public MaterialProperties Properties;

		// Token: 0x0400085B RID: 2139
		public int Flags;

		// Token: 0x0400085C RID: 2140
		public bool IsLoaded;

		// Token: 0x0400085D RID: 2141
		[Nullable(2)]
		public TerrainMaterialDescription Terrain;
	}
}
