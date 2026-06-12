using System;
using Microsoft.Xna.Framework;
using TheraEngine.Graphics;

namespace TheraEngine.Assets.Audio
{
	// Token: 0x020001B2 RID: 434
	public interface ISoundEffect3D
	{
		// Token: 0x06000AD0 RID: 2768
		void Play(Camera {16047}, Vector3 {16048}, float {16049} = 1f, SoundOptions {16050} = SoundOptions.None);

		// Token: 0x06000AD1 RID: 2769
		void Dispose();
	}
}
