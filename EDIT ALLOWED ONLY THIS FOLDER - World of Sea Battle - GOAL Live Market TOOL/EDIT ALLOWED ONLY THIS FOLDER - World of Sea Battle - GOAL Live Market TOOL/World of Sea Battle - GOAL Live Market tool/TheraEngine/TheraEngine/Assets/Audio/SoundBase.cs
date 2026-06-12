using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using TheraEngine.Collections;

namespace TheraEngine.Assets.Audio
{
	// Token: 0x020001B8 RID: 440
	public abstract class SoundBase : DisposableObject
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0003682F File Offset: 0x00034A2F
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x00036838 File Offset: 0x00034A38
		public virtual float Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				this.volume = MathHelper.Clamp(value, 0f, 1f);
				float preloadVolume = this.PreloadVolume;
				this.ListenStopped();
				try
				{
					for (int i = 0; i < this.{16082}.Size; i++)
					{
						this.{16082}.Array[i].Volume = preloadVolume;
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x000368A8 File Offset: 0x00034AA8
		protected float PreloadVolume
		{
			get
			{
				float num = this.MultiplySoundManagerVolume ? MathHelper.Clamp(SoundManager.Volume, 0f, 1f) : 1f;
				return this.volume * num;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x000368E1 File Offset: 0x00034AE1
		public bool IsPlay
		{
			get
			{
				this.ListenStopped();
				return this.{16082}.Size != 0;
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000368F8 File Offset: 0x00034AF8
		public SoundBase(SoundEffect {16080})
		{
			this.AssetName = {16080}.Name;
			this.{16081} = new Stack<SoundEffectInstance>(2);
			this.{16082} = new Tlist<SoundEffectInstance>(2);
			if ({16080} == null)
			{
				throw new ArgumentNullException();
			}
			this.Effect = {16080};
			this.volume = 1f;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00036954 File Offset: 0x00034B54
		protected void ListenStopped()
		{
			for (int i = 0; i < this.{16082}.Size; i++)
			{
				if (this.{16082}.Array[i].State == SoundState.Stopped)
				{
					this.{16081}.Push(this.{16082}.Array[i]);
					this.{16082}.FastRemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x000369B4 File Offset: 0x00034BB4
		protected SoundEffectInstance Next
		{
			get
			{
				SoundEffectInstance result;
				if (this.{16081}.Count == 0)
				{
					result = this.Effect.CreateInstance();
				}
				else
				{
					result = this.{16081}.Pop();
				}
				this.{16082}.Add(result);
				return result;
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000369F6 File Offset: 0x00034BF6
		public override void Dispose()
		{
			SoundEffect effect = this.Effect;
			if (effect != null)
			{
				effect.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x04000892 RID: 2194
		public readonly SoundEffect Effect;

		// Token: 0x04000893 RID: 2195
		public string AssetName;

		// Token: 0x04000894 RID: 2196
		public bool MultiplySoundManagerVolume = true;

		// Token: 0x04000895 RID: 2197
		private Stack<SoundEffectInstance> {16081};

		// Token: 0x04000896 RID: 2198
		private Tlist<SoundEffectInstance> {16082};

		// Token: 0x04000897 RID: 2199
		protected float volume;
	}
}
