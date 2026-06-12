using System;
using Microsoft.Xna.Framework;
using TheraEngine.Helpers;

namespace TheraEngine
{
	// Token: 0x0200002B RID: 43
	public sealed class Scroller
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00008898 File Offset: 0x00006A98
		// (set) Token: 0x0600016D RID: 365 RVA: 0x000088B0 File Offset: 0x00006AB0
		public float CurrentScrollValue
		{
			get
			{
				return MathHelper.Clamp(this.{11784}, 0f, this.MaxScrollValue);
			}
			set
			{
				this.{11784} = MathHelper.Clamp(value, 0f, this.MaxScrollValue);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000088C9 File Offset: 0x00006AC9
		public float NextValue
		{
			get
			{
				return this.{11785};
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000088D1 File Offset: 0x00006AD1
		public Scroller(float {11780})
		{
			if ({11780} > 0f)
			{
				this.{11786} = {11780} / 1000f;
				return;
			}
			throw new ArgumentOutOfRangeException("singlePerSecond");
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000088FC File Offset: 0x00006AFC
		public void Update(float {11781})
		{
			if (this.{11784} != this.{11785})
			{
				Geometry.Evalute(ref this.{11784}, this.{11785}, this.{11786} * {11781} * (1f + Math.Abs(this.{11784} - this.{11785}) / 50f));
				this.{11784} = Math.Min(this.{11784}, this.MaxScrollValue);
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008966 File Offset: 0x00006B66
		public void ScrollNext(float {11782})
		{
			this.{11785} = MathHelper.Clamp(this.{11784} + {11782}, 0f, this.MaxScrollValue);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00008986 File Offset: 0x00006B86
		public void ScrollBack(float {11783})
		{
			this.{11785} = MathHelper.Clamp(this.{11784} - {11783}, 0f, this.MaxScrollValue);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000089A6 File Offset: 0x00006BA6
		public void Stopful()
		{
			this.{11785} = this.{11784};
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000089B4 File Offset: 0x00006BB4
		public void Stopforward()
		{
			this.{11784} = this.{11785};
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000089C2 File Offset: 0x00006BC2
		public void Reset()
		{
			this.{11785} = 0f;
			this.{11784} = 0f;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000089DA File Offset: 0x00006BDA
		public void Normalize()
		{
			this.{11785} = MathHelper.Clamp(this.{11785}, 0f, this.MaxScrollValue);
			this.{11784} = MathHelper.Clamp(this.{11784}, 0f, this.MaxScrollValue);
		}

		// Token: 0x040000CA RID: 202
		public static float ScrollSpeedFactor = 1f;

		// Token: 0x040000CB RID: 203
		public float MaxScrollValue;

		// Token: 0x040000CC RID: 204
		private float {11784};

		// Token: 0x040000CD RID: 205
		private float {11785};

		// Token: 0x040000CE RID: 206
		private readonly float {11786};
	}
}
