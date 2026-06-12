using System;
using TheraEngine.Components.Architecture;
using TheraEngine.Scene;

namespace World_Of_Sea_Battle.Graphics.Shaders
{
	// Token: 0x0200045C RID: 1116
	public interface IGraphicsElement : IDisposable, IUpdateableObject
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06001859 RID: 6233
		bool RemoveWhenSceneChanged { get; }

		// Token: 0x0600185A RID: 6234
		void Render2D();

		// Token: 0x0600185B RID: 6235
		void Render3D();

		// Token: 0x0600185C RID: 6236
		void Render3DStatic();

		// Token: 0x0600185D RID: 6237
		void RenderGBuffer(IGBufferBuilder {23633});
	}
}
