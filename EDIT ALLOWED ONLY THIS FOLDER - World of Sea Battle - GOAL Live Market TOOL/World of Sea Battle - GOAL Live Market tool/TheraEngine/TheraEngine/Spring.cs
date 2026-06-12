using System;

namespace TheraEngine
{
	// Token: 0x0200002D RID: 45
	public class Spring
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00008B16 File Offset: 0x00006D16
		public Spring(float {11802} = 0f)
		{
			this.CurrentPosition = {11802};
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008B48 File Offset: 0x00006D48
		public void Update(float {11803}, float {11804})
		{
			float num = {11804} - this.CurrentPosition;
			float num2 = this.stiffness * num;
			float num3 = -this.damping * this.{11805};
			float num4 = (num2 + num3) / this.mass;
			this.{11805} += num4 * {11803};
			this.CurrentPosition += this.{11805} * {11803};
		}

		// Token: 0x040000D3 RID: 211
		public float CurrentPosition;

		// Token: 0x040000D4 RID: 212
		public float mass = 1f;

		// Token: 0x040000D5 RID: 213
		public float stiffness = 100f;

		// Token: 0x040000D6 RID: 214
		public float damping = 10f;

		// Token: 0x040000D7 RID: 215
		private float {11805};
	}
}
