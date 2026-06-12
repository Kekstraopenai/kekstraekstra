using System;
using TheraEngine.Collections;

namespace TheraEngine.Components.Architecture
{
	// Token: 0x0200010B RID: 267
	public class Renderable2DCollection : Tlist<IRenderable2D>, IRenderable2D
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x00026D78 File Offset: 0x00024F78
		public void Render2D()
		{
			for (int i = 0; i < this.Size; i++)
			{
				this.Array[i].Render2D();
			}
		}
	}
}
