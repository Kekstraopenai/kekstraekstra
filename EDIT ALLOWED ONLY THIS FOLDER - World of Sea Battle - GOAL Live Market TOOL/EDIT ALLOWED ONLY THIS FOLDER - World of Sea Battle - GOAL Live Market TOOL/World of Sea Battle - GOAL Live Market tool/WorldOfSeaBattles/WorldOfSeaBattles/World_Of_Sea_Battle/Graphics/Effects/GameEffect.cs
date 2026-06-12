using System;
using TheraEngine;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004A7 RID: 1191
	internal abstract class GameEffect
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x000070D7 File Offset: 0x000052D7
		public virtual bool IsAlive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x000E8E1A File Offset: 0x000E701A
		public GameEffect(bool {24503} = true)
		{
			if ({24503})
			{
				Global.Game.WorldInstance.AddFlashEffect(this);
			}
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000E8E35 File Offset: 0x000E7035
		public virtual void Update(ref FrameTime {24504}, out bool {24505})
		{
			{24505} = false;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00003100 File Offset: 0x00001300
		public virtual void Render3D()
		{
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00003100 File Offset: 0x00001300
		public virtual void Render2D()
		{
		}
	}
}
