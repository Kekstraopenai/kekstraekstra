using System;
using TheraEngine;
using TheraEngine.Interface;
using World_Of_Sea_Battle.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000402 RID: 1026
	internal sealed class {22885} : CustomUi
	{
		// Token: 0x06001649 RID: 5705 RVA: 0x000BBB3C File Offset: 0x000B9D3C
		public {22885}() : base(false)
		{
			this.AnimatedFocus = false;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserUpdate(ref FrameTime {22886})
		{
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x000201BB File Offset: 0x0001E3BB
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(CommonAtlas.Texture);
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}
	}
}
