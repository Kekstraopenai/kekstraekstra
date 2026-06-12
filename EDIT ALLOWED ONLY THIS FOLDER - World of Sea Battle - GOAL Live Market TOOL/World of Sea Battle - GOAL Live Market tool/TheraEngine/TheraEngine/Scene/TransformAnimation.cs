using System;
using TheraEngine.Components;
using TheraEngine.Components.Architecture;

namespace TheraEngine.Scene
{
	// Token: 0x0200004A RID: 74
	public abstract class TransformAnimation : IUpdateableObject
	{
		// Token: 0x06000211 RID: 529 RVA: 0x0000BF76 File Offset: 0x0000A176
		public TransformAnimation(Transform3D {12036})
		{
			this.Transform = {12036};
		}

		// Token: 0x06000212 RID: 530
		public abstract void Update(ref FrameTime {12037});

		// Token: 0x0400015A RID: 346
		public Transform3D Transform;

		// Token: 0x0400015B RID: 347
		public bool IsAnimationComplete;
	}
}
