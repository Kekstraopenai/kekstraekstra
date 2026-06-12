using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Helpers
{
	// Token: 0x020000ED RID: 237
	public struct Negoentropy
	{
		// Token: 0x06000660 RID: 1632 RVA: 0x00021845 File Offset: 0x0001FA45
		public Negoentropy(Vector2 {14431}, float {14432}, float {14433})
		{
			this.Counter = {14431};
			this.MinEntropy = {14432};
			this.MaxEntropy = {14433};
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0002185C File Offset: 0x0001FA5C
		public bool Sample(Sequence {14434}, float {14435})
		{
			bool flag;
			if (this.Counter.Y != 0f)
			{
				float num = this.Counter.X / this.Counter.Y;
				flag = (num <= this.MaxEntropy * {14435} && (num < this.MinEntropy * {14435} || {14434}.Chanse({14435})));
			}
			else
			{
				flag = {14434}.Chanse({14435});
			}
			this.Counter.Y = this.Counter.Y + 1f;
			if (flag)
			{
				this.Counter.X = this.Counter.X + 1f;
			}
			return flag;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000218F1 File Offset: 0x0001FAF1
		public void Reset()
		{
			this.Counter = default(Vector2);
		}

		// Token: 0x040004C3 RID: 1219
		public readonly float MaxEntropy;

		// Token: 0x040004C4 RID: 1220
		public readonly float MinEntropy;

		// Token: 0x040004C5 RID: 1221
		public Vector2 Counter;
	}
}
