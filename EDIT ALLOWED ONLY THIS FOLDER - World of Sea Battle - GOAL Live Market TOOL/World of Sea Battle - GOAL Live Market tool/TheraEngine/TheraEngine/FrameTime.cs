using System;

namespace TheraEngine
{
	// Token: 0x02000022 RID: 34
	public readonly struct FrameTime
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004729 File Offset: 0x00002929
		public float Factor
		{
			get
			{
				return this.msElapsed / 16.6666f;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004737 File Offset: 0x00002937
		public FrameTime(float {11481}, float {11482})
		{
			this.msElapsed = {11481};
			this.secElapsed = {11482};
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004747 File Offset: 0x00002947
		public void EvaluteTimerMs(ref float {11483})
		{
			{11483} = Math.Max(0f, {11483} - this.msElapsed);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000475E File Offset: 0x0000295E
		public void EvaluteTimerSec(ref float {11484})
		{
			{11484} = Math.Max(0f, {11484} - this.secElapsed);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004775 File Offset: 0x00002975
		public bool EvaluteTimerSec2(ref float {11485})
		{
			if ({11485} == 0f)
			{
				return false;
			}
			{11485} -= this.secElapsed;
			if ({11485} <= 0f)
			{
				{11485} = 0f;
				return true;
			}
			return false;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000047A0 File Offset: 0x000029A0
		public bool EvaluteTimerMs2(ref float {11486})
		{
			if ({11486} == 0f)
			{
				return false;
			}
			{11486} -= this.msElapsed;
			if ({11486} <= 0f)
			{
				{11486} = 0f;
				return true;
			}
			return false;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000047CC File Offset: 0x000029CC
		public void Multiply(ref float {11487}, float {11488})
		{
			float num = this.secElapsed / 0.016666668f;
			if (num == 0f || (num > 0.999f && num < 1.001f))
			{
				{11487} *= {11488};
				return;
			}
			{11487} *= MathF.Pow({11488}, num);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004811 File Offset: 0x00002A11
		public static FrameTime operator *(FrameTime {11489}, float {11490})
		{
			return new FrameTime({11489}.msElapsed * {11490}, {11489}.secElapsed * {11490});
		}

		// Token: 0x040000AB RID: 171
		public static FrameTime Identity = new FrameTime(0f, 0f);

		// Token: 0x040000AC RID: 172
		public readonly float msElapsed;

		// Token: 0x040000AD RID: 173
		public readonly float secElapsed;
	}
}
