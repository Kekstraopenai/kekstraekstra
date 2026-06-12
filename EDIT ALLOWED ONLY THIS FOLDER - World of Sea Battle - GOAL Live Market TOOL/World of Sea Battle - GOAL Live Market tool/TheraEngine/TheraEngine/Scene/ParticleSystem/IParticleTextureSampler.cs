using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x02000052 RID: 82
	public interface IParticleTextureSampler
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600023A RID: 570
		bool IsSprite { get; }

		// Token: 0x0600023B RID: 571
		void GetPath(float {12130}, out Rectangle {12131});
	}
}
