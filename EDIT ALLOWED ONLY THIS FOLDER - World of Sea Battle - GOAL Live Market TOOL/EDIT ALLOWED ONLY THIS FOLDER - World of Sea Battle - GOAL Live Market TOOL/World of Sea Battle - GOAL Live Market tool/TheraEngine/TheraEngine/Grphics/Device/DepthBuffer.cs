using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Grphics.Device
{
	// Token: 0x020000F4 RID: 244
	public class DepthBuffer
	{
		// Token: 0x040004DF RID: 1247
		public DepthStencilState DepthState;

		// Token: 0x040004E0 RID: 1248
		public static readonly DepthBuffer WithoutDepth = new DepthBuffer
		{
			DepthState = DepthStencilState.None
		};

		// Token: 0x040004E1 RID: 1249
		public static readonly DepthBuffer ReadOnly = new DepthBuffer
		{
			DepthState = DepthStencilState.DepthRead
		};

		// Token: 0x040004E2 RID: 1250
		public static readonly DepthBuffer ReadAndWrite = new DepthBuffer
		{
			DepthState = DepthStencilState.Default
		};
	}
}
