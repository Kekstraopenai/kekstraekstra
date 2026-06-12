using System;
using Microsoft.Xna.Framework;
using TheraEngine.Graphics;
using TheraEngine.Helpers;

namespace TheraEngine.Assets.Audio
{
	// Token: 0x020001B7 RID: 439
	public class Sound3DSet : ISoundEffect3D
	{
		// Token: 0x06000ADE RID: 2782 RVA: 0x00036754 File Offset: 0x00034954
		public Sound3DSet(params Sound3D[] {16070})
		{
			if ((this.{16076} = {16070}.Length) == 0)
			{
				throw new ArgumentException();
			}
			this.{16075} = {16070};
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00036784 File Offset: 0x00034984
		public void Dispose()
		{
			if (this.{16075} != null)
			{
				for (int i = 0; i < this.{16075}.Length; i++)
				{
					this.{16075}[i].Dispose();
				}
				this.{16075} = null;
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000367C0 File Offset: 0x000349C0
		public void Play(Camera {16071}, Vector3 {16072}, float {16073} = 1f, SoundOptions {16074} = SoundOptions.None)
		{
			if (this.{16076} == 1)
			{
				this.{16075}[0].Play({16071}, {16072}, {16073}, SoundOptions.None);
				return;
			}
			int num = Rand.RangeInt(0, this.{16076});
			while (this.{16076} > 2 && num == this.{16077})
			{
				num = Rand.RangeInt(0, this.{16076});
			}
			this.{16075}[num].Play({16071}, {16072}, {16073}, {16074});
			this.{16077} = num;
		}

		// Token: 0x0400088F RID: 2191
		private Sound3D[] {16075};

		// Token: 0x04000890 RID: 2192
		private int {16076};

		// Token: 0x04000891 RID: 2193
		private int {16077};
	}
}
