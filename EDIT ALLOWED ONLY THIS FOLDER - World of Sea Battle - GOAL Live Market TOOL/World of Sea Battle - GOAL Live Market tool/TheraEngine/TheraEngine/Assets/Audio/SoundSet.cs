using System;
using TheraEngine.Helpers;

namespace TheraEngine.Assets.Audio
{
	// Token: 0x020001BD RID: 445
	public class SoundSet : ISoundEffect
	{
		// Token: 0x06000AFC RID: 2812 RVA: 0x00036E7C File Offset: 0x0003507C
		public SoundSet(params Sound[] {16126})
		{
			if ((this.{16129} = {16126}.Length) == 0)
			{
				throw new ArgumentException();
			}
			this.{16128} = {16126};
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00036EAC File Offset: 0x000350AC
		public void Dispose()
		{
			if (this.{16128} != null)
			{
				for (int i = 0; i < this.{16128}.Length; i++)
				{
					this.{16128}[i].Dispose();
				}
				this.{16128} = null;
			}
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00036EE8 File Offset: 0x000350E8
		public void Play(float {16127} = 1f)
		{
			if (this.{16129} == 1)
			{
				this.{16128}[0].Play(1f);
				return;
			}
			int num;
			for (num = Rand.RangeInt(0, this.{16129}); num == this.{16130}; num = Rand.RangeInt(0, this.{16129}))
			{
			}
			this.{16128}[num].Play({16127});
			this.{16130} = num;
		}

		// Token: 0x040008A0 RID: 2208
		private Sound[] {16128};

		// Token: 0x040008A1 RID: 2209
		private int {16129};

		// Token: 0x040008A2 RID: 2210
		private int {16130};
	}
}
