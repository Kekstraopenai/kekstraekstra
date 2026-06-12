using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace TheraEngine.Helpers
{
	// Token: 0x020000F3 RID: 243
	public class Rand
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00022163 File Offset: 0x00020363
		public static Sequence Instance
		{
			get
			{
				Rand.Check();
				return Rand.random;
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00022170 File Offset: 0x00020370
		private static void Check()
		{
			if (Rand.random == null)
			{
				object obj = Rand.globalSyncRoot;
				lock (obj)
				{
					Rand.random = new Sequence(Rand.global.Next());
				}
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000221C4 File Offset: 0x000203C4
		public static int Sign()
		{
			Rand.Check();
			return Rand.random.GetSign();
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000221D5 File Offset: 0x000203D5
		public static float Float()
		{
			Rand.Check();
			return Rand.random.Float();
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000221E6 File Offset: 0x000203E6
		public static int Int()
		{
			Rand.Check();
			return Rand.random.Int();
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000221F7 File Offset: 0x000203F7
		public static bool Boolean()
		{
			Rand.Check();
			return Rand.random.Boolean();
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000221C4 File Offset: 0x000203C4
		public static int GetSign()
		{
			Rand.Check();
			return Rand.random.GetSign();
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00022208 File Offset: 0x00020408
		public static float Range(float {14482}, float {14483})
		{
			Rand.Check();
			return Rand.random.Range({14482}, {14483});
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0002221B File Offset: 0x0002041B
		public static int RangeInt(int {14484}, int {14485})
		{
			Rand.Check();
			return Rand.random.RangeInt({14484}, {14485});
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0002222E File Offset: 0x0002042E
		public static float Angle()
		{
			Rand.Check();
			return Rand.random.Angle();
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0002223F File Offset: 0x0002043F
		public static bool Chanse(float {14486})
		{
			Rand.Check();
			return Rand.random.Chanse({14486});
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00022251 File Offset: 0x00020451
		public static float GetGaussianAbs()
		{
			Rand.Check();
			return Rand.random.FloatPow2();
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00022262 File Offset: 0x00020462
		public static Vector2 NextVector2(float {14487} = -1f, float {14488} = 1f)
		{
			Rand.Check();
			return Rand.random.NextVector2({14487}, {14488});
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00022275 File Offset: 0x00020475
		public static Vector2 NextRadialVector2(float {14489}, float {14490})
		{
			Rand.Check();
			return Rand.random.NextRadialVector2({14489}, {14490});
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00022288 File Offset: 0x00020488
		public static Vector3 NextVector3(float {14491} = -1f, float {14492} = 1f)
		{
			Rand.Check();
			return Rand.random.NextVector3(-1f, 1f);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x000222A3 File Offset: 0x000204A3
		public static Vector2 NextNormal()
		{
			Rand.Check();
			return Rand.random.NextNormal();
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000222B4 File Offset: 0x000204B4
		public static Vector2 DiskVector2(float {14493} = 0f)
		{
			Rand.Check();
			return Rand.random.DiskVector2({14493});
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000222C6 File Offset: 0x000204C6
		public static T Pick<T>(params T[] {14494})
		{
			Rand.Check();
			return Rand.random.Pick<T>({14494});
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x000222D8 File Offset: 0x000204D8
		public static T PickWeighted<T>(Func<T, float> {14495}, params T[] {14496})
		{
			Rand.Check();
			return Rand.random.PickWeighted<T>({14495}, {14496});
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000222EB File Offset: 0x000204EB
		public static T PickWeighted<T>([TupleElementNames(new string[]
		{
			"item",
			"weight"
		})] params ValueTuple<T, float>[] {14497})
		{
			Rand.Check();
			return Rand.random.PickWeighted<T>({14497});
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000222FD File Offset: 0x000204FD
		public static IEnumerable<T> PickMany<T>(int {14498}, params T[] {14499})
		{
			Rand.Check();
			return Rand.random.PickMany<T>({14498}, {14499});
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00022310 File Offset: 0x00020510
		public static Color GetColor
		{
			get
			{
				return new Color(new Vector3(Rand.Range(0.25f, 1f), Rand.Range(0.25f, 1f), Rand.Range(0.25f, 1f)));
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00022349 File Offset: 0x00020549
		public static int Round(float {14500})
		{
			Rand.Check();
			return Rand.random.Round({14500});
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0002235B File Offset: 0x0002055B
		public static Color Color(float {14501})
		{
			Rand.Check();
			return Rand.random.Color({14501});
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00022370 File Offset: 0x00020570
		public static ulong NextUlong()
		{
			Rand.Check();
			ulong num = (ulong)Rand.random.Int();
			ulong num2 = (ulong)Rand.random.Int();
			return num << 32 | num2;
		}

		// Token: 0x040004DC RID: 1244
		private static Random global = new Random();

		// Token: 0x040004DD RID: 1245
		private static object globalSyncRoot = new object();

		// Token: 0x040004DE RID: 1246
		[ThreadStatic]
		private static Sequence random;
	}
}
