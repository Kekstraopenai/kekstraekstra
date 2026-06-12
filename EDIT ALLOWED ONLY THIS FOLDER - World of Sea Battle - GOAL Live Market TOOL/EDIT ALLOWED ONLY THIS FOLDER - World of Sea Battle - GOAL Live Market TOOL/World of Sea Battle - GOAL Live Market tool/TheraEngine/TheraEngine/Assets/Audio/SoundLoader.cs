using System;
using Microsoft.Xna.Framework.Audio;
using TheraEngine.Core;

namespace TheraEngine.Assets.Audio
{
	// Token: 0x020001B9 RID: 441
	public class SoundLoader : IDisposable
	{
		// Token: 0x06000AE9 RID: 2793 RVA: 0x00036A0F File Offset: 0x00034C0F
		public SoundLoader(string {16085}, ContentManager {16086})
		{
			this.{16098} = {16086};
			this.{16097} = {16085};
			this.{16099} = {16085} + '\\';
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00036A38 File Offset: 0x00034C38
		public void SetChilDir(string {16087})
		{
			this.{16099} = string.Concat(new object[]
			{
				this.{16097},
				'\\',
				{16087},
				'\\'
			});
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00036A6C File Offset: 0x00034C6C
		public Sound LoadSound(string {16088})
		{
			return new Sound(this.{16098}.Load<SoundEffect>(this.{16099} + {16088}));
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00036A8A File Offset: 0x00034C8A
		public Sound3D LoadSound3D(string {16089})
		{
			return new Sound3D(this.{16098}.Load<SoundEffect>(this.{16099} + {16089}));
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00036AA8 File Offset: 0x00034CA8
		public SoundSet LoadSoundSet(string {16090}, int {16091}, int {16092})
		{
			Sound[] array = new Sound[{16092}];
			for (int i = 0; i < {16092}; i++)
			{
				array[i] = this.LoadSound({16090} + (i + {16091}));
			}
			return new SoundSet(array);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00036AE8 File Offset: 0x00034CE8
		public Sound3DSet LoadSound3DSet(string {16093}, int {16094}, int {16095}, float {16096} = 1f)
		{
			Sound3D[] array = new Sound3D[{16095}];
			for (int i = 0; i < {16095}; i++)
			{
				array[i] = this.LoadSound3D({16093} + (i + {16094}));
				array[i].Volume = {16096};
			}
			return new Sound3DSet(array);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00036B2F File Offset: 0x00034D2F
		public void Dispose()
		{
			this.{16097} = null;
			this.{16099} = null;
			this.{16098} = null;
		}

		// Token: 0x04000898 RID: 2200
		private string {16097};

		// Token: 0x04000899 RID: 2201
		private ContentManager {16098};

		// Token: 0x0400089A RID: 2202
		private string {16099};
	}
}
