using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;

namespace TheraEngine.Helpers
{
	// Token: 0x020000E8 RID: 232
	public class HashHelper
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x0001FE38 File Offset: 0x0001E038
		public static int RoundRandHash(float {14354}, int {14355})
		{
			float num = MathF.Truncate({14354});
			float num2 = {14354} - num;
			int num3 = (HashHelper.greater({14355}) < num2) ? 1 : 0;
			return (int)Math.Round((double)num) + num3;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001FE64 File Offset: 0x0001E064
		public static int NextSize(bool {14356}, int {14357})
		{
			if (!{14356})
			{
				return {14357} * 2;
			}
			return {14357} + {14357} / 2;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001FE72 File Offset: 0x0001E072
		private static float wrap2(float {14358})
		{
			{14358} %= 1f;
			return (1f - {14358}) * {14358} * 4f;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001FE8C File Offset: 0x0001E08C
		public static Vector3 SphericalVectorFromLerp(float {14359})
		{
			Vector3 result = new Vector3(HashHelper.wrap2({14359}) - 0.5f, HashHelper.wrap2({14359} + 0.16666667f) - 0.5f, HashHelper.wrap2({14359} + 0.33333334f) - 0.5f);
			result.Normalize();
			return result;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001FED8 File Offset: 0x0001E0D8
		public static float NoiseCurve(float {14360})
		{
			return MathHelper.Lerp(HashHelper.greater((int)Math.Floor((double){14360})), HashHelper.greater((int)Math.Ceiling((double){14360})), {14360} % 1f) * 2f - 1f;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001FF0C File Offset: 0x0001E10C
		public static float greater(int {14361})
		{
			return (float)((double){14361} * (((double){14361} * 24.232 + (double)({14361} * {14361}) % HashHelper.simpleDeviator1) % HashHelper.simpleDeviator2) % 1.0);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001FF39 File Offset: 0x0001E139
		public static float greater(uint {14362})
		{
			return HashHelper.greater((int){14362});
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001FF44 File Offset: 0x0001E144
		public static int greaterInt(int {14363}, int {14364})
		{
			return (((double)(HashHelper.greater({14363}) * 35689.094f) % HashHelper.bigDeviator1).GetHashCode() & int.MaxValue) % {14364};
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001FF74 File Offset: 0x0001E174
		public static bool greaterBool(int {14365})
		{
			return HashHelper.greater({14365} * (({14365} % 2 == 0) ? -4 : 1)) >= 0.5f;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001FF91 File Offset: 0x0001E191
		public static bool greaterBoolNoLongRow(int {14366})
		{
			return HashHelper.grater_bits[{14366} % HashHelper.grater_bits.Length];
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001FFA4 File Offset: 0x0001E1A4
		public static Vector2 Noise2(int {14367})
		{
			Vector2 result;
			result.X = HashHelper.greater({14367}) * 2f - 1f;
			result.Y = HashHelper.greater({14367} + 1000031) * 2f - 1f;
			if (result.Length() > 1f)
			{
				return HashHelper.Noise2({14367} + 1);
			}
			return result;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00020004 File Offset: 0x0001E204
		public static void shuffleGreater<T>(Tlist<T> {14368}, int {14369})
		{
			int i = {14368}.Size;
			while (i > 1)
			{
				int num = HashHelper.greaterInt({14369} + i * {14369} + (17 + i) * ({14369} + 1), --i + 1);
				T t = {14368}.Array[i];
				{14368}.Array[i] = {14368}.Array[num];
				{14368}.Array[num] = t;
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0002006C File Offset: 0x0001E26C
		public static string FileMD5(string {14370})
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				using (FileStream fileStream = File.OpenRead({14370}))
				{
					byte[] array = md.ComputeHash(fileStream);
					StringBuilder stringBuilder = new StringBuilder();
					foreach (byte b in array)
					{
						stringBuilder.Append(b.ToString("x2"));
					}
					result = stringBuilder.ToString();
				}
			}
			return result;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000200FC File Offset: 0x0001E2FC
		public static int SrtingHashMd5Based(string {14371})
		{
			return BitConverter.ToInt32(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes({14371})), 0);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00020119 File Offset: 0x0001E319
		public static long SrtingLongHashMd5Based(string {14372})
		{
			return BitConverter.ToInt64(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes({14372})), 0);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00020138 File Offset: 0x0001E338
		public static string Md5String(string {14373})
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				byte[] bytes = Encoding.ASCII.GetBytes({14373});
				byte[] array = md.ComputeHash(bytes);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("X2"));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000201B8 File Offset: 0x0001E3B8
		public static int[] Permutation(int {14374}, int {14375}, int {14376})
		{
			if ({14375} > {14376} || {14375} == 0)
			{
				throw new ArgumentException();
			}
			int[] array = new int[{14376}];
			for (int i = 0; i < {14376}; i++)
			{
				array[i] = i;
			}
			int j = {14376};
			while (j > 1)
			{
				int num = HashHelper.greaterInt({14374} + j * {14374} + (17 + j) * ({14374} + 1), --j + 1);
				int num2 = array[j];
				array[j] = array[num];
				array[num] = num2;
			}
			Array.Resize<int>(ref array, {14375});
			return array;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00020225 File Offset: 0x0001E425
		public static byte FloatToByte(float {14377})
		{
			if ({14377} <= 0f)
			{
				return 0;
			}
			if ({14377} >= 1f)
			{
				return byte.MaxValue;
			}
			return (byte)(1f + {14377} * 253f);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00020250 File Offset: 0x0001E450
		public static int AffineShuffle(int {14378}, int {14379}, int {14380})
		{
			if ({14380} <= 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = ({14378} * 7 + 3) % {14380};
			int num2 = ({14378} * 11 + 5) % {14380};
			while (HashHelper.Gcd(num, {14380}) != 1)
			{
				num = (num + 1) % {14380};
			}
			return (int)(((long)num * (long){14379} + (long)num2) % (long){14380});
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0002029B File Offset: 0x0001E49B
		public static int Gcd(int {14381}, int {14382})
		{
			{14381} = Math.Abs({14381});
			int num;
			for ({14382} = Math.Abs({14382}); {14382} != 0; {14382} = num)
			{
				num = {14381} % {14382};
				{14381} = {14382};
			}
			return {14381};
		}

		// Token: 0x040004AF RID: 1199
		internal static int MaxArrayLength = 2146435071;

		// Token: 0x040004B0 RID: 1200
		private static double simpleDeviator1 = 2.71828182846;

		// Token: 0x040004B1 RID: 1201
		private static double simpleDeviator2 = 1.73205080757;

		// Token: 0x040004B2 RID: 1202
		private static double bigDeviator1 = 11.2694276696;

		// Token: 0x040004B3 RID: 1203
		private static bool[] grater_bits = new bool[]
		{
			true,
			false,
			true,
			false,
			false,
			true,
			false,
			true,
			true,
			false
		};
	}
}
