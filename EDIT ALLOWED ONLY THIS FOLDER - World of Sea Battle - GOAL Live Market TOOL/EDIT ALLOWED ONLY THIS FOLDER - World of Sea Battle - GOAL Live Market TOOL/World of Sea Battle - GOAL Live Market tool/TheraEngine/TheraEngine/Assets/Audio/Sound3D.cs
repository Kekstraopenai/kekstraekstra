using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using TheraEngine.Graphics;

namespace TheraEngine.Assets.Audio
{
	// Token: 0x020001B6 RID: 438
	public class Sound3D : SoundBase, ISoundEffect3D
	{
		// Token: 0x06000ADB RID: 2779 RVA: 0x000366BF File Offset: 0x000348BF
		public Sound3D(SoundEffect {16063}) : base({16063})
		{
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000366F0 File Offset: 0x000348F0
		public void Play(Camera {16064}, Vector3 {16065}, float {16066} = 1f, SoundOptions {16067} = SoundOptions.None)
		{
			base.ListenStopped();
			if ({16066} > 1f)
			{
				throw new ArgumentException("volumeMultiplier");
			}
			float num;
			Vector3.Distance(ref {16064}.Position, ref {16065}, out num);
			float {16120} = SoundManager.Volume * this.volume * {16066} * 0.9f;
			SoundPlayer.Apply3D(this.Effect, {16120}, base.Next, {16064}, ref {16065}, {16067});
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000366E3 File Offset: 0x000348E3
		public Sound3D SetVolume(float {16068})
		{
			this.Volume = {16068};
			return this;
		}
	}
}
