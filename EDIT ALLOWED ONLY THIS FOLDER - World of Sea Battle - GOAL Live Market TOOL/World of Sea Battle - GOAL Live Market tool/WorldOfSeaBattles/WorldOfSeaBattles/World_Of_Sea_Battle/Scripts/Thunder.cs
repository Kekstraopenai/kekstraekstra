using System;
using Microsoft.Xna.Framework;

namespace World_Of_Sea_Battle.Scripts
{
	// Token: 0x02000018 RID: 24
	internal sealed class Thunder
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004935 File Offset: 0x00002B35
		public float GetLighting
		{
			get
			{
				return MathHelper.Clamp(this.Power * (this.{16278} * this.{16278}), 0f, 1f);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000495A File Offset: 0x00002B5A
		public Thunder(float {16275}, float {16276})
		{
			this.Power = {16275};
			this.Duration = {16276};
			this.{16279} = {16276};
			this.{16278} = 0f;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004984 File Offset: 0x00002B84
		public bool Update(float {16277})
		{
			this.{16279} -= {16277};
			this.{16278} += {16277} / 100f;
			if (this.{16278} > 1f)
			{
				this.{16278} -= 2f;
			}
			return this.{16279} < 1f;
		}

		// Token: 0x04000050 RID: 80
		public readonly float Power;

		// Token: 0x04000051 RID: 81
		public readonly float Duration;

		// Token: 0x04000052 RID: 82
		private float {16278};

		// Token: 0x04000053 RID: 83
		private float {16279};
	}
}
