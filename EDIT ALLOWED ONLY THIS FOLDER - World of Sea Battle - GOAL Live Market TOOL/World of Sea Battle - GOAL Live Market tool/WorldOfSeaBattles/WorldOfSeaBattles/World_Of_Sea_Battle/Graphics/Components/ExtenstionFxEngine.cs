using System;
using TheraEngine.Scene.ParticleSystem;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x02000471 RID: 1137
	public static class ExtenstionFxEngine
	{
		// Token: 0x060018B0 RID: 6320 RVA: 0x000D51CE File Offset: 0x000D33CE
		public static void Apply(this ParticleEffect3DTemplate {23742}, ParticleEffectSampleCall {23743})
		{
			{23742}.Apply(ref {23743}, Global.Render.ParticleManager3D);
		}
	}
}
