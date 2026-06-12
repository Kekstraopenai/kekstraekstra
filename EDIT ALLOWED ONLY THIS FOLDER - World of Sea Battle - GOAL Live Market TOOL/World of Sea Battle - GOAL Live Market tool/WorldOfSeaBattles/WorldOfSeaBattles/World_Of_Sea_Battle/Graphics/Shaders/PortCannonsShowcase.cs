using System;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components.Architecture;
using TheraEngine.Scene;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics.Shaders
{
	// Token: 0x0200045D RID: 1117
	public class PortCannonsShowcase : IGraphicsElement, IDisposable, IUpdateableObject
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x000070D7 File Offset: 0x000052D7
		public bool RemoveWhenSceneChanged
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x000D3518 File Offset: 0x000D1718
		public void Dispose()
		{
			Tlist<IGraphicsElement> scenes = Global.Game.Scenes;
			IGraphicsElement graphicsElement = this;
			scenes.Remove(graphicsElement);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x00003100 File Offset: 0x00001300
		public void Render2D()
		{
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00003100 File Offset: 0x00001300
		public void Render3D()
		{
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00003100 File Offset: 0x00001300
		public void Render3DStatic()
		{
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00003100 File Offset: 0x00001300
		public void RenderGBuffer(IGBufferBuilder {23634})
		{
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00003100 File Offset: 0x00001300
		public void Update(ref FrameTime {23635})
		{
		}
	}
}
