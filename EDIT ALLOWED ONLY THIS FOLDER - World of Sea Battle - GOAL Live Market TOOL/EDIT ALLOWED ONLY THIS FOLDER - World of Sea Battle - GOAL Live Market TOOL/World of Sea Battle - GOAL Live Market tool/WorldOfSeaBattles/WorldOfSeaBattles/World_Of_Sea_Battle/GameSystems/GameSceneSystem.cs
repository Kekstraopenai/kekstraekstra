using System;
using TheraEngine;
using TheraEngine.Components.Architecture;
using TheraEngine.Core;

namespace World_Of_Sea_Battle.GameSystems
{
	// Token: 0x020004B8 RID: 1208
	internal abstract class GameSceneSystem : IUpdateableObject
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x000EB953 File Offset: 0x000E9B53
		public bool IsActive
		{
			get
			{
				return this.{24689};
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x000EB95B File Offset: 0x000E9B5B
		public GameSceneSystem()
		{
			this.{24689} = false;
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x000EB96A File Offset: 0x000E9B6A
		public virtual void On()
		{
			if (this.{24689})
			{
				throw new InvalidOperationException("Scene On/off error");
			}
			this.{24689} = true;
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x000EB986 File Offset: 0x000E9B86
		public virtual void Off()
		{
			if (!this.{24689})
			{
				throw new InvalidOperationException("Scene On/off error");
			}
			this.{24689} = false;
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00003100 File Offset: 0x00001300
		public virtual void Initialize(ContentManager {24687})
		{
		}

		// Token: 0x06001A84 RID: 6788
		public abstract void Update(ref FrameTime {24688});

		// Token: 0x040018E6 RID: 6374
		private bool {24689};
	}
}
