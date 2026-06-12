using System;
using TheraEngine;
using TheraEngine.Components.Architecture;
using TheraEngine.Core;

namespace World_Of_Sea_Battle.Scenes
{
	// Token: 0x02000039 RID: 57
	internal abstract class Scene : IUpdateableObject
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600019E RID: 414
		public abstract bool UseStaticWeather { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600019F RID: 415
		public abstract bool UseStaticCamera { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001A0 RID: 416
		public abstract bool CleanScene { get; }

		// Token: 0x060001A1 RID: 417
		public abstract void OnBegin();

		// Token: 0x060001A2 RID: 418
		public abstract void OnEnd();

		// Token: 0x060001A3 RID: 419
		public abstract void Initialize(ContentManager {16541});

		// Token: 0x060001A4 RID: 420
		public abstract void Render2D();

		// Token: 0x060001A5 RID: 421
		public abstract void Update(ref FrameTime {16542});
	}
}
