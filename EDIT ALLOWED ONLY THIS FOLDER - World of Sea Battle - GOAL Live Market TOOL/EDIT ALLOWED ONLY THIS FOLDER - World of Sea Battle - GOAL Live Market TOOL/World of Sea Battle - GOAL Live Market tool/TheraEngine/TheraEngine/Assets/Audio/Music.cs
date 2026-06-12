using System;
using Microsoft.Xna.Framework.Media;

namespace TheraEngine.Assets.Audio
{
	// Token: 0x020001B3 RID: 435
	public class Music : ISoundEffect
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00036613 File Offset: 0x00034813
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0003661B File Offset: 0x0003481B
		public float Volume
		{
			get
			{
				return this.{16056};
			}
			set
			{
				this.{16056} = value;
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00036624 File Offset: 0x00034824
		public Music(Song {16053})
		{
			this.{16055} = {16053};
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00036648 File Offset: 0x00034848
		public void Play(float {16054} = 1f)
		{
			if (!this.{16057})
			{
				return;
			}
			if (MediaPlayer.State != MediaState.Stopped)
			{
				MediaPlayer.Stop();
			}
			try
			{
				MediaPlayer.Play(this.{16055});
				this.{16057} = true;
			}
			catch
			{
				this.{16057} = false;
			}
			Engine.Game.isMusicPlaying = true;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000366A4 File Offset: 0x000348A4
		public void Stop()
		{
			if (MediaPlayer.State != MediaState.Stopped)
			{
				MediaPlayer.Stop();
			}
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000366B2 File Offset: 0x000348B2
		public void Dispose()
		{
			this.{16055}.Dispose();
		}

		// Token: 0x04000889 RID: 2185
		private Song {16055};

		// Token: 0x0400088A RID: 2186
		private float {16056} = 1f;

		// Token: 0x0400088B RID: 2187
		private bool {16057} = true;
	}
}
