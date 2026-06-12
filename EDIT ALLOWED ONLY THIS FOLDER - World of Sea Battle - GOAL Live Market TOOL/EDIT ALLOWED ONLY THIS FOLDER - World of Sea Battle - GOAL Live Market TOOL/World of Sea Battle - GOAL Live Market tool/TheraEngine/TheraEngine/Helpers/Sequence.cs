using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace TheraEngine.Helpers
{
	// Token: 0x020000F0 RID: 240
	public class Sequence
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00021BAB File Offset: 0x0001FDAB
		public int Version
		{
			get
			{
				return this.{14478};
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00021BB3 File Offset: 0x0001FDB3
		public Sequence()
		{
			this.{14477} = new Random();
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00021BC6 File Offset: 0x0001FDC6
		public Sequence(int {14456})
		{
			this.{14477} = new Random({14456});
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00021BDA File Offset: 0x0001FDDA
		public float Float()
		{
			this.{14478}++;
			return (float)this.{14477}.NextDouble();
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00021BF6 File Offset: 0x0001FDF6
		public int Int()
		{
			this.{14478}++;
			return this.{14477}.Next();
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00021C11 File Offset: 0x0001FE11
		public bool Boolean()
		{
			this.{14478}++;
			return this.{14477}.Next() > 1073741823;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00021C33 File Offset: 0x0001FE33
		public int GetSign()
		{
			this.{14478}++;
			if (this.{14477}.Next() <= 1073741823)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00021C58 File Offset: 0x0001FE58
		public float Range(float {14457}, float {14458})
		{
			this.{14478}++;
			return (float)this.{14477}.NextDouble() * ({14458} - {14457}) + {14457};
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00021C7A File Offset: 0x0001FE7A
		public int RangeInt(int {14459}, int {14460})
		{
			this.{14478}++;
			return this.{14477}.Next({14459}, {14460});
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00021C97 File Offset: 0x0001FE97
		public float Angle()
		{
			return this.Range(-3.1415927f, 3.1415927f);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00021CA9 File Offset: 0x0001FEA9
		public bool Chanse(float {14461})
		{
			this.{14478}++;
			return this.{14477}.NextDouble() * 100.0 <= (double){14461};
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00021CD5 File Offset: 0x0001FED5
		public float FloatPow2()
		{
			float num = this.Float();
			return num * num;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00021CDF File Offset: 0x0001FEDF
		public Vector2 NextVector2(float {14462} = -1f, float {14463} = 1f)
		{
			this.{14478}++;
			return new Vector2((float)this.{14477}.NextDouble() * ({14463} - {14462}) + {14462}, (float)this.{14477}.NextDouble() * ({14463} - {14462}) + {14462});
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00021D18 File Offset: 0x0001FF18
		public Vector2 NextRadialVector2(float {14464}, float {14465})
		{
			return Geometry.SubstructRotate(this.Angle(), this.Range({14464}, {14465}));
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00021D30 File Offset: 0x0001FF30
		public Vector3 NextVector3(float {14466} = -1f, float {14467} = 1f)
		{
			this.{14478}++;
			return new Vector3((float)this.{14477}.NextDouble() * ({14467} - {14466}) + {14466}, (float)this.{14477}.NextDouble() * ({14467} - {14466}) + {14466}, (float)this.{14477}.NextDouble() * ({14467} - {14466}) + {14466});
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00021D86 File Offset: 0x0001FF86
		public Vector2 NextNormal()
		{
			return Geometry.lutSincos[this.RangeInt(0, Geometry.lutSincos.Length)];
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00021DA0 File Offset: 0x0001FFA0
		public Vector2 DiskVector2(float {14468} = 0f)
		{
			Vector2 result;
			float num;
			do
			{
				this.{14478}++;
				result = new Vector2((float)this.{14477}.NextDouble() * 2f - 1f, (float)this.{14477}.NextDouble() * 2f - 1f);
				num = result.LengthSquared();
			}
			while (num < {14468} || num > 1f);
			return result;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00021E07 File Offset: 0x00020007
		public T Pick<T>(params T[] {14469})
		{
			this.{14478}++;
			return {14469}[this.{14477}.Next(0, {14469}.Length)];
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00021E2C File Offset: 0x0002002C
		public T PickWeighted<T>(Func<T, float> {14470}, params T[] {14471})
		{
			float num = {14471}.Sum({14470});
			if (num <= 0f)
			{
				throw new InvalidOperationException("Summary weight is zero or less");
			}
			float num2 = this.Range(0f, num);
			for (int i = 0; i < {14471}.Length; i++)
			{
				num2 -= {14470}({14471}[i]);
				if (num2 <= 0f)
				{
					return {14471}[i];
				}
			}
			return {14471}.Last<T>();
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00021E98 File Offset: 0x00020098
		public T PickWeighted<T>([TupleElementNames(new string[]
		{
			"item",
			"weight"
		})] params ValueTuple<T, float>[] {14472})
		{
			float num = {14472}.Sum(([TupleElementNames(new string[]
			{
				"item",
				"weight"
			})] ValueTuple<T, float> {14479}) => {14479}.Item2);
			if (num <= 0f)
			{
				throw new InvalidOperationException("Summary weight is zero or less");
			}
			float num2 = this.Range(0f, num);
			for (int i = 0; i < {14472}.Length; i++)
			{
				num2 -= {14472}[i].Item2;
				if (num2 <= 0f)
				{
					return {14472}[i].Item1;
				}
			}
			return {14472}.Last<ValueTuple<T, float>>().Item1;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00021F28 File Offset: 0x00020128
		public IEnumerable<T> PickMany<T>(int {14473}, params T[] {14474})
		{
			Sequence.<PickMany>d__23<T> <PickMany>d__ = new Sequence.<PickMany>d__23<T>(-2);
			<PickMany>d__.<>4__this = this;
			<PickMany>d__.<>3__numberOfItems = {14473};
			<PickMany>d__.<>3__items = {14474};
			return <PickMany>d__;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00021F48 File Offset: 0x00020148
		public int Round(float {14475})
		{
			float num = MathF.Truncate({14475});
			float num2 = {14475} - num;
			int num3 = (this.Float() < num2) ? 1 : 0;
			return (int)Math.Round((double)num) + num3;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00021F74 File Offset: 0x00020174
		public Color Color(float {14476})
		{
			float num = this.Range(0f, 1f);
			float num2 = this.Range(0f, 1f);
			float num3 = this.Range(0f, 1f);
			float num4 = Math.Max(num, Math.Max(num2, num3));
			float r = num / num4 * {14476};
			num2 = num2 / num4 * {14476};
			num3 = num3 / num4 * {14476};
			return new Color(r, num2, num3);
		}

		// Token: 0x040004CE RID: 1230
		private Random {14477};

		// Token: 0x040004CF RID: 1231
		private int {14478};
	}
}
