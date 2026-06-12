using System;
using Microsoft.Xna.Framework.Audio;

namespace TheraEngine.Assets.Audio
{
	// Token: 0x020001BA RID: 442
	public class SoundLoaderParallel : IDisposable
	{
		// Token: 0x06000AF0 RID: 2800 RVA: 0x00036B46 File Offset: 0x00034D46
		public SoundLoaderParallel(string {16101})
		{
			this.{16114} = {16101};
			this.{16115} = {16101} + '\\';
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00036B68 File Offset: 0x00034D68
		public void SetChilDir(string {16102})
		{
			this.{16115} = string.Concat(new object[]
			{
				this.{16114},
				'\\',
				{16102},
				'\\'
			});
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00036B9C File Offset: 0x00034D9C
		public Sound LoadSound(string {16103}, float {16104} = 1f)
		{
			Sound sound = new Sound(Engine.Game.Content.Load<SoundEffect>(this.{16115} + {16103}));
			if ({16104} != 1f)
			{
				sound.Volume = {16104};
			}
			return sound;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00036BDA File Offset: 0x00034DDA
		public Sound3D LoadSound3D(string {16105})
		{
			return new Sound3D(Engine.Game.Content.Load<SoundEffect>(this.{16115} + {16105}));
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00036BFC File Offset: 0x00034DFC
		public SoundSet LoadSoundSet(string {16106}, int {16107}, int {16108}, float {16109} = 1f)
		{
			Sound[] array = new Sound[{16108}];
			for (int i = 0; i < {16108}; i++)
			{
				array[i] = this.LoadSound({16106} + (i + {16107}), 1f);
				array[i].Volume = {16109};
			}
			return new SoundSet(array);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00036C48 File Offset: 0x00034E48
		public Sound3DSet LoadSound3DSet(string {16110}, int {16111}, int {16112}, float {16113} = 1f)
		{
			Sound3D[] array = new Sound3D[{16112}];
			for (int i = 0; i < {16112}; i++)
			{
				array[i] = this.LoadSound3D({16110} + (i + {16111}));
				array[i].Volume = {16113};
			}
			return new Sound3DSet(array);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00036C8F File Offset: 0x00034E8F
		public void Dispose()
		{
			this.{16114} = null;
			this.{16115} = null;
		}

		// Token: 0x0400089B RID: 2203
		private string {16114};

		// Token: 0x0400089C RID: 2204
		private string {16115};
	}
}
