using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Graphics
{
	// Token: 0x02000143 RID: 323
	public interface IRenderTarget
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000915 RID: 2325
		RenderTargetBinding[] Targets { get; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000916 RID: 2326
		int MultisamplersCount { get; }
	}
}
