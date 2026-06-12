using System;
using TheraEngine;

namespace World_Of_Sea_Battle.Graphics
{
	// Token: 0x02000455 RID: 1109
	internal interface IInGameSightUI
	{
		// Token: 0x06001821 RID: 6177
		void Update(ref FrameTime {23539}, bool {23540});

		// Token: 0x06001822 RID: 6178
		void Render2D(float {23541});

		// Token: 0x06001823 RID: 6179
		void Render3D(float {23542});

		// Token: 0x06001824 RID: 6180
		void Reset();
	}
}
