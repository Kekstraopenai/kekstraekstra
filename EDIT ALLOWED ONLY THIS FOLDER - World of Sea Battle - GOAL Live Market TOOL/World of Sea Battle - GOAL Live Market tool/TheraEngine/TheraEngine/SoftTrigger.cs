using System;
using TheraEngine.Helpers;

namespace TheraEngine
{
	// Token: 0x0200002C RID: 44
	public class SoftTrigger
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00008A20 File Offset: 0x00006C20
		public float CurrentSoftValueSmoothstep
		{
			get
			{
				return Geometry.Smoothstep(this.CurrentSoftValue);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00008A2D File Offset: 0x00006C2D
		public float CurrentSoftValueSmootherstep
		{
			get
			{
				return Geometry.SmootherstepTh(this.CurrentSoftValue);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00008A3A File Offset: 0x00006C3A
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00008A42 File Offset: 0x00006C42
		public float SpeedPerSec
		{
			get
			{
				return this.{11800};
			}
			set
			{
				this.{11800} = value;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008A4B File Offset: 0x00006C4B
		public SoftTrigger(float {11791}, float {11792}, float {11793})
		{
			if ({11791} > {11792} || {11793} <= 0f)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.{11798} = {11791};
			this.{11799} = {11792};
			this.{11800} = {11793};
			this.CurrentSoftValue = {11791};
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008A81 File Offset: 0x00006C81
		public void Reset()
		{
			this.CurrentSoftValue = this.{11798};
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008A8F File Offset: 0x00006C8F
		public void SetMax()
		{
			this.CurrentSoftValue = this.{11799};
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008A9D File Offset: 0x00006C9D
		public bool IsChangesAvailable(bool {11794})
		{
			return ({11794} ? this.{11799} : this.{11798}) != this.CurrentSoftValue;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008ABC File Offset: 0x00006CBC
		public bool Evalute(ref FrameTime {11795}, bool {11796})
		{
			float num = {11796} ? this.{11799} : this.{11798};
			Geometry.Evalute(ref this.CurrentSoftValue, num, {11795}.secElapsed * this.{11800});
			return num == this.CurrentSoftValue;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008AFD File Offset: 0x00006CFD
		public void SetValue(bool {11797})
		{
			this.CurrentSoftValue = ({11797} ? this.{11799} : this.{11798});
		}

		// Token: 0x040000CF RID: 207
		public float CurrentSoftValue;

		// Token: 0x040000D0 RID: 208
		private float {11798};

		// Token: 0x040000D1 RID: 209
		private float {11799};

		// Token: 0x040000D2 RID: 210
		private float {11800};
	}
}
