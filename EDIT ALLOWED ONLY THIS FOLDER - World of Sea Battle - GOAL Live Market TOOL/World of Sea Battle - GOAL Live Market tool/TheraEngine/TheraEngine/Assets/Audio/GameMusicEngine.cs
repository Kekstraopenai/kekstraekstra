using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using TheraEngine.Components.Architecture;
using TheraEngine.Helpers;

namespace TheraEngine.Assets.Audio
{
	// Token: 0x020001B0 RID: 432
	public class GameMusicEngine : IUpdateableObject
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00036467 File Offset: 0x00034667
		public Music CurrentTrack
		{
			get
			{
				return this.{16040};
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0003646F File Offset: 0x0003466F
		public bool IsPlaying
		{
			get
			{
				return this.{16043} && this.{16040} != null;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00036484 File Offset: 0x00034684
		public float FadeVolumeMultiplier
		{
			get
			{
				return Math.Min((this.{16044} == 0f) ? 1f : (this.{16044} / 10000f), 0.3f + 0.7f * (this.{16045} / 5000f));
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000364C3 File Offset: 0x000346C3
		public GameMusicEngine()
		{
			this.{16039} = new Dictionary<object, Music[]>();
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000364D6 File Offset: 0x000346D6
		public void AddSet(object {16033}, params Music[] {16034})
		{
			this.{16039}.Add({16033}, {16034});
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000364E8 File Offset: 0x000346E8
		public void SetNextTrackFromSet(object {16035}, float {16036}, float {16037})
		{
			Music[] {14494} = this.{16039}[{16035}];
			this.{16040} = Rand.Pick<Music>({14494});
			this.CurrentTrackTag = {16035};
			this.{16041} = {16036};
			this.{16042} = {16037};
			this.{16043} = false;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0003652A File Offset: 0x0003472A
		public void ScheduleDying()
		{
			if (this.{16044} > 0f || this.{16040} == null)
			{
				return;
			}
			this.{16044} = 10000f;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00036550 File Offset: 0x00034750
		public void Update(ref FrameTime {16038})
		{
			if (!Engine.Game.IsActive)
			{
				return;
			}
			if (this.{16040} == null)
			{
				return;
			}
			if (MediaPlayer.State == MediaState.Stopped)
			{
				if (!this.{16043})
				{
					this.{16041} -= {16038}.msElapsed;
					if (this.{16041} <= 0f)
					{
						this.{16045} = 0f;
						this.{16040}.Play(1f);
						this.{16043} = true;
						return;
					}
				}
				else
				{
					this.{16042} -= {16038}.msElapsed;
					if (this.{16042} <= 0f)
					{
						this.{16040} = null;
						return;
					}
				}
			}
			else
			{
				if ({16038}.EvaluteTimerMs2(ref this.{16044}))
				{
					MediaPlayer.Stop();
				}
				this.{16045} += {16038}.secElapsed;
			}
		}

		// Token: 0x0400087E RID: 2174
		private const float fadeTime = 10000f;

		// Token: 0x0400087F RID: 2175
		private const float startTime = 5000f;

		// Token: 0x04000880 RID: 2176
		public static bool IsEnabled;

		// Token: 0x04000881 RID: 2177
		private Dictionary<object, Music[]> {16039};

		// Token: 0x04000882 RID: 2178
		private Music {16040};

		// Token: 0x04000883 RID: 2179
		private float {16041};

		// Token: 0x04000884 RID: 2180
		private float {16042};

		// Token: 0x04000885 RID: 2181
		private bool {16043};

		// Token: 0x04000886 RID: 2182
		private float {16044};

		// Token: 0x04000887 RID: 2183
		private float {16045};

		// Token: 0x04000888 RID: 2184
		public object CurrentTrackTag;
	}
}
