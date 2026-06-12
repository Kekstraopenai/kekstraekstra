using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Graphics;

namespace TheraEngine.Scene
{
	// Token: 0x02000039 RID: 57
	public interface IGBufferBuilder
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001B4 RID: 436
		bool MaterialAnalyzingEnable { get; }

		// Token: 0x060001B5 RID: 437
		void ApplyPass(ref Matrix {11876}, bool {11877});

		// Token: 0x060001B6 RID: 438
		void RestartPassMaterialAnalyze(Texture2D {11878});

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001B7 RID: 439
		CameraPositionInfo CurrentPassCamera { get; }
	}
}
