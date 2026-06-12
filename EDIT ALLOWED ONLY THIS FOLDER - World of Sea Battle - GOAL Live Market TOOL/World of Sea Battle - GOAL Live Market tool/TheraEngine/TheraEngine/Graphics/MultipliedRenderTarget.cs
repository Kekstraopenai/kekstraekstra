using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Graphics
{
	// Token: 0x02000147 RID: 327
	public struct MultipliedRenderTarget : IRenderTarget
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0002CD0C File Offset: 0x0002AF0C
		RenderTargetBinding[] IRenderTarget.Targets
		{
			get
			{
				return this.{15043};
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002CD14 File Offset: 0x0002AF14
		public MultipliedRenderTarget(params RenderTarget[] {15042})
		{
			int num = {15042}.Length;
			this.{15043} = new RenderTargetBinding[num];
			for (int i = 0; i < num; i++)
			{
				this.{15043}[i] = {15042}[i]._set[0];
			}
			this.{15044} = {15042}[0].MultisamplersCount;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0002CD65 File Offset: 0x0002AF65
		public int MultisamplersCount
		{
			get
			{
				return this.{15044};
			}
		}

		// Token: 0x04000633 RID: 1587
		private RenderTargetBinding[] {15043};

		// Token: 0x04000634 RID: 1588
		private int {15044};
	}
}
