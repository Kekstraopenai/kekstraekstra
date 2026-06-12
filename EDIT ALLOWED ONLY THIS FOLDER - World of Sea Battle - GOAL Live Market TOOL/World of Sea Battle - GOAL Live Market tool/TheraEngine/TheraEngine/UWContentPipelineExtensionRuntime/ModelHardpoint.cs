using System;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;

namespace UWContentPipelineExtensionRuntime
{
	// Token: 0x0200000B RID: 11
	public class ModelHardpoint : IGenericQuadTreeItem
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002EE4 File Offset: 0x000010E4
		public Vector2 Position
		{
			get
			{
				return this.Transform.Translation.XZ();
			}
		}

		// Token: 0x04000021 RID: 33
		public WorldOfSeaBattleHardpointID HardpointID;

		// Token: 0x04000022 RID: 34
		public Matrix Transform;

		// Token: 0x04000023 RID: 35
		public string FullName;
	}
}
