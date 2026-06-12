using System;
using Microsoft.Xna.Framework.Audio;

namespace TheraEngine.Assets.Audio
{
	// Token: 0x020001B4 RID: 436
	public class Sound : SoundBase, ISoundEffect
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x000366BF File Offset: 0x000348BF
		public Sound(SoundEffect {16059}) : base({16059})
		{
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x000366C8 File Offset: 0x000348C8
		public void Play(float {16060} = 1f)
		{
			base.ListenStopped();
			SoundPlayer.ApplyBackground(base.PreloadVolume * {16060}, base.Next);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000366E3 File Offset: 0x000348E3
		public Sound SetVolume(float {16061})
		{
			this.Volume = {16061};
			return this;
		}
	}
}
