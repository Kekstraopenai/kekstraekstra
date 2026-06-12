using System;
using Microsoft.Xna.Framework;

namespace TheraEngine
{
	// Token: 0x02000025 RID: 37
	public struct Matrix3x3
	{
		// Token: 0x06000113 RID: 275 RVA: 0x000057B8 File Offset: 0x000039B8
		public Matrix3x3(float {11615}, float {11616}, float {11617}, float {11618}, float {11619}, float {11620}, float {11621}, float {11622}, float {11623})
		{
			this.M11 = {11615};
			this.M12 = {11616};
			this.M13 = {11617};
			this.M21 = {11618};
			this.M22 = {11619};
			this.M23 = {11620};
			this.M31 = {11621};
			this.M32 = {11622};
			this.M33 = {11623};
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000580C File Offset: 0x00003A0C
		public static Matrix3x3 Identity
		{
			get
			{
				return new Matrix3x3(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000584C File Offset: 0x00003A4C
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00005881 File Offset: 0x00003A81
		public Vector3 Backward
		{
			get
			{
				Vector3 result;
				result.X = this.M31;
				result.Y = this.M32;
				result.Z = this.M33;
				return result;
			}
			set
			{
				this.M31 = value.X;
				this.M32 = value.Y;
				this.M33 = value.Z;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000058A8 File Offset: 0x00003AA8
		// (set) Token: 0x06000118 RID: 280 RVA: 0x000058E0 File Offset: 0x00003AE0
		public Vector3 Down
		{
			get
			{
				Vector3 result;
				result.X = -this.M21;
				result.Y = -this.M22;
				result.Z = -this.M23;
				return result;
			}
			set
			{
				this.M21 = -value.X;
				this.M22 = -value.Y;
				this.M23 = -value.Z;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000590C File Offset: 0x00003B0C
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00005944 File Offset: 0x00003B44
		public Vector3 Forward
		{
			get
			{
				Vector3 result;
				result.X = -this.M31;
				result.Y = -this.M32;
				result.Z = -this.M33;
				return result;
			}
			set
			{
				this.M31 = -value.X;
				this.M32 = -value.Y;
				this.M33 = -value.Z;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005970 File Offset: 0x00003B70
		// (set) Token: 0x0600011C RID: 284 RVA: 0x000059A8 File Offset: 0x00003BA8
		public Vector3 Left
		{
			get
			{
				Vector3 result;
				result.X = -this.M11;
				result.Y = -this.M12;
				result.Z = -this.M13;
				return result;
			}
			set
			{
				this.M11 = -value.X;
				this.M12 = -value.Y;
				this.M13 = -value.Z;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000059D4 File Offset: 0x00003BD4
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00005A09 File Offset: 0x00003C09
		public Vector3 Right
		{
			get
			{
				Vector3 result;
				result.X = this.M11;
				result.Y = this.M12;
				result.Z = this.M13;
				return result;
			}
			set
			{
				this.M11 = value.X;
				this.M12 = value.Y;
				this.M13 = value.Z;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00005A30 File Offset: 0x00003C30
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00005A65 File Offset: 0x00003C65
		public Vector3 Up
		{
			get
			{
				Vector3 result;
				result.X = this.M21;
				result.Y = this.M22;
				result.Z = this.M23;
				return result;
			}
			set
			{
				this.M21 = value.X;
				this.M22 = value.Y;
				this.M23 = value.Z;
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005A8C File Offset: 0x00003C8C
		public static void Add(ref Matrix3x3 {11630}, ref Matrix3x3 {11631}, out Matrix3x3 {11632})
		{
			float m = {11630}.M11 + {11631}.M11;
			float m2 = {11630}.M12 + {11631}.M12;
			float m3 = {11630}.M13 + {11631}.M13;
			float m4 = {11630}.M21 + {11631}.M21;
			float m5 = {11630}.M22 + {11631}.M22;
			float m6 = {11630}.M23 + {11631}.M23;
			float m7 = {11630}.M31 + {11631}.M31;
			float m8 = {11630}.M32 + {11631}.M32;
			float m9 = {11630}.M33 + {11631}.M33;
			{11632}.M11 = m;
			{11632}.M12 = m2;
			{11632}.M13 = m3;
			{11632}.M21 = m4;
			{11632}.M22 = m5;
			{11632}.M23 = m6;
			{11632}.M31 = m7;
			{11632}.M32 = m8;
			{11632}.M33 = m9;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005B60 File Offset: 0x00003D60
		public static void Add(ref Matrix {11633}, ref Matrix3x3 {11634}, out Matrix3x3 {11635})
		{
			float m = {11633}.M11 + {11634}.M11;
			float m2 = {11633}.M12 + {11634}.M12;
			float m3 = {11633}.M13 + {11634}.M13;
			float m4 = {11633}.M21 + {11634}.M21;
			float m5 = {11633}.M22 + {11634}.M22;
			float m6 = {11633}.M23 + {11634}.M23;
			float m7 = {11633}.M31 + {11634}.M31;
			float m8 = {11633}.M32 + {11634}.M32;
			float m9 = {11633}.M33 + {11634}.M33;
			{11635}.M11 = m;
			{11635}.M12 = m2;
			{11635}.M13 = m3;
			{11635}.M21 = m4;
			{11635}.M22 = m5;
			{11635}.M23 = m6;
			{11635}.M31 = m7;
			{11635}.M32 = m8;
			{11635}.M33 = m9;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005C34 File Offset: 0x00003E34
		public static void Add(ref Matrix3x3 {11636}, ref Matrix {11637}, out Matrix3x3 {11638})
		{
			float m = {11636}.M11 + {11637}.M11;
			float m2 = {11636}.M12 + {11637}.M12;
			float m3 = {11636}.M13 + {11637}.M13;
			float m4 = {11636}.M21 + {11637}.M21;
			float m5 = {11636}.M22 + {11637}.M22;
			float m6 = {11636}.M23 + {11637}.M23;
			float m7 = {11636}.M31 + {11637}.M31;
			float m8 = {11636}.M32 + {11637}.M32;
			float m9 = {11636}.M33 + {11637}.M33;
			{11638}.M11 = m;
			{11638}.M12 = m2;
			{11638}.M13 = m3;
			{11638}.M21 = m4;
			{11638}.M22 = m5;
			{11638}.M23 = m6;
			{11638}.M31 = m7;
			{11638}.M32 = m8;
			{11638}.M33 = m9;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005D08 File Offset: 0x00003F08
		public static void Add(ref Matrix {11639}, ref Matrix {11640}, out Matrix3x3 {11641})
		{
			float m = {11639}.M11 + {11640}.M11;
			float m2 = {11639}.M12 + {11640}.M12;
			float m3 = {11639}.M13 + {11640}.M13;
			float m4 = {11639}.M21 + {11640}.M21;
			float m5 = {11639}.M22 + {11640}.M22;
			float m6 = {11639}.M23 + {11640}.M23;
			float m7 = {11639}.M31 + {11640}.M31;
			float m8 = {11639}.M32 + {11640}.M32;
			float m9 = {11639}.M33 + {11640}.M33;
			{11641}.M11 = m;
			{11641}.M12 = m2;
			{11641}.M13 = m3;
			{11641}.M21 = m4;
			{11641}.M22 = m5;
			{11641}.M23 = m6;
			{11641}.M31 = m7;
			{11641}.M32 = m8;
			{11641}.M33 = m9;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005DDC File Offset: 0x00003FDC
		public static void CreateCrossProduct(ref Vector3 {11642}, out Matrix3x3 {11643})
		{
			{11643}.M11 = 0f;
			{11643}.M12 = -{11642}.Z;
			{11643}.M13 = {11642}.Y;
			{11643}.M21 = {11642}.Z;
			{11643}.M22 = 0f;
			{11643}.M23 = -{11642}.X;
			{11643}.M31 = -{11642}.Y;
			{11643}.M32 = {11642}.X;
			{11643}.M33 = 0f;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005E58 File Offset: 0x00004058
		public static void CreateFromMatrix(ref Matrix {11644}, out Matrix3x3 {11645})
		{
			{11645}.M11 = {11644}.M11;
			{11645}.M12 = {11644}.M12;
			{11645}.M13 = {11644}.M13;
			{11645}.M21 = {11644}.M21;
			{11645}.M22 = {11644}.M22;
			{11645}.M23 = {11644}.M23;
			{11645}.M31 = {11644}.M31;
			{11645}.M32 = {11644}.M32;
			{11645}.M33 = {11644}.M33;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005ED4 File Offset: 0x000040D4
		public static Matrix3x3 CreateFromMatrix(Matrix {11646})
		{
			Matrix3x3 result;
			result.M11 = {11646}.M11;
			result.M12 = {11646}.M12;
			result.M13 = {11646}.M13;
			result.M21 = {11646}.M21;
			result.M22 = {11646}.M22;
			result.M23 = {11646}.M23;
			result.M31 = {11646}.M31;
			result.M32 = {11646}.M32;
			result.M33 = {11646}.M33;
			return result;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005F58 File Offset: 0x00004158
		public static void CreateScale(float {11647}, out Matrix3x3 {11648})
		{
			{11648} = new Matrix3x3
			{
				M11 = {11647},
				M22 = {11647},
				M33 = {11647}
			};
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005F8C File Offset: 0x0000418C
		public static Matrix3x3 CreateScale(float {11649})
		{
			return new Matrix3x3
			{
				M11 = {11649},
				M22 = {11649},
				M33 = {11649}
			};
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005FBC File Offset: 0x000041BC
		public static void CreateScale(ref Vector3 {11650}, out Matrix3x3 {11651})
		{
			{11651} = new Matrix3x3
			{
				M11 = {11650}.X,
				M22 = {11650}.Y,
				M33 = {11650}.Z
			};
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006000 File Offset: 0x00004200
		public static Matrix3x3 CreateScale(ref Vector3 {11652})
		{
			return new Matrix3x3
			{
				M11 = {11652}.X,
				M22 = {11652}.Y,
				M33 = {11652}.Z
			};
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006040 File Offset: 0x00004240
		public static void CreateScale(float {11653}, float {11654}, float {11655}, out Matrix3x3 {11656})
		{
			{11656} = new Matrix3x3
			{
				M11 = {11653},
				M22 = {11654},
				M33 = {11655}
			};
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006074 File Offset: 0x00004274
		public static Matrix3x3 CreateScale(float {11657}, float {11658}, float {11659})
		{
			return new Matrix3x3
			{
				M11 = {11657},
				M22 = {11658},
				M33 = {11659}
			};
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000060A4 File Offset: 0x000042A4
		public static void Invert(ref Matrix3x3 {11660}, out Matrix3x3 {11661})
		{
			float num = 1f / {11660}.Determinant();
			float m = ({11660}.M22 * {11660}.M33 - {11660}.M23 * {11660}.M32) * num;
			float m2 = ({11660}.M13 * {11660}.M32 - {11660}.M33 * {11660}.M12) * num;
			float m3 = ({11660}.M12 * {11660}.M23 - {11660}.M22 * {11660}.M13) * num;
			float m4 = ({11660}.M23 * {11660}.M31 - {11660}.M21 * {11660}.M33) * num;
			float m5 = ({11660}.M11 * {11660}.M33 - {11660}.M13 * {11660}.M31) * num;
			float m6 = ({11660}.M13 * {11660}.M21 - {11660}.M11 * {11660}.M23) * num;
			float m7 = ({11660}.M21 * {11660}.M32 - {11660}.M22 * {11660}.M31) * num;
			float m8 = ({11660}.M12 * {11660}.M31 - {11660}.M11 * {11660}.M32) * num;
			float m9 = ({11660}.M11 * {11660}.M22 - {11660}.M12 * {11660}.M21) * num;
			{11661}.M11 = m;
			{11661}.M12 = m2;
			{11661}.M13 = m3;
			{11661}.M21 = m4;
			{11661}.M22 = m5;
			{11661}.M23 = m6;
			{11661}.M31 = m7;
			{11661}.M32 = m8;
			{11661}.M33 = m9;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006218 File Offset: 0x00004418
		public static void AdaptiveInvert(ref Matrix3x3 {11662}, out Matrix3x3 {11663})
		{
			int num2;
			float num = 1f / {11662}.AdaptiveDeterminant(out num2);
			float m;
			float m2;
			float m3;
			float m4;
			float m5;
			float m6;
			float m7;
			float m8;
			float m9;
			switch (num2)
			{
			case 0:
				m = ({11662}.M22 * {11662}.M33 - {11662}.M23 * {11662}.M32) * num;
				m2 = ({11662}.M13 * {11662}.M32 - {11662}.M33 * {11662}.M12) * num;
				m3 = ({11662}.M12 * {11662}.M23 - {11662}.M22 * {11662}.M13) * num;
				m4 = ({11662}.M23 * {11662}.M31 - {11662}.M21 * {11662}.M33) * num;
				m5 = ({11662}.M11 * {11662}.M33 - {11662}.M13 * {11662}.M31) * num;
				m6 = ({11662}.M13 * {11662}.M21 - {11662}.M11 * {11662}.M23) * num;
				m7 = ({11662}.M21 * {11662}.M32 - {11662}.M22 * {11662}.M31) * num;
				m8 = ({11662}.M12 * {11662}.M31 - {11662}.M11 * {11662}.M32) * num;
				m9 = ({11662}.M11 * {11662}.M22 - {11662}.M12 * {11662}.M21) * num;
				break;
			case 1:
				m = {11662}.M22 * num;
				m2 = -{11662}.M12 * num;
				m3 = 0f;
				m4 = -{11662}.M21 * num;
				m5 = {11662}.M11 * num;
				m6 = 0f;
				m7 = 0f;
				m8 = 0f;
				m9 = 0f;
				break;
			case 2:
				m = 0f;
				m2 = 0f;
				m3 = 0f;
				m4 = 0f;
				m5 = {11662}.M33 * num;
				m6 = -{11662}.M23 * num;
				m7 = 0f;
				m8 = -{11662}.M32 * num;
				m9 = {11662}.M22 * num;
				break;
			case 3:
				m = {11662}.M33 * num;
				m2 = 0f;
				m3 = -{11662}.M13 * num;
				m4 = 0f;
				m5 = 0f;
				m6 = 0f;
				m7 = -{11662}.M31 * num;
				m8 = 0f;
				m9 = {11662}.M11 * num;
				break;
			case 4:
				m = 1f / {11662}.M11;
				m2 = 0f;
				m3 = 0f;
				m4 = 0f;
				m5 = 0f;
				m6 = 0f;
				m7 = 0f;
				m8 = 0f;
				m9 = 0f;
				break;
			case 5:
				m = 0f;
				m2 = 0f;
				m3 = 0f;
				m4 = 0f;
				m5 = 1f / {11662}.M22;
				m6 = 0f;
				m7 = 0f;
				m8 = 0f;
				m9 = 0f;
				break;
			case 6:
				m = 0f;
				m2 = 0f;
				m3 = 0f;
				m4 = 0f;
				m5 = 0f;
				m6 = 0f;
				m7 = 0f;
				m8 = 0f;
				m9 = 1f / {11662}.M33;
				break;
			default:
				m = 0f;
				m2 = 0f;
				m3 = 0f;
				m4 = 0f;
				m5 = 0f;
				m6 = 0f;
				m7 = 0f;
				m8 = 0f;
				m9 = 0f;
				break;
			}
			{11663}.M11 = m;
			{11663}.M12 = m2;
			{11663}.M13 = m3;
			{11663}.M21 = m4;
			{11663}.M22 = m5;
			{11663}.M23 = m6;
			{11663}.M31 = m7;
			{11663}.M32 = m8;
			{11663}.M33 = m9;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000065C0 File Offset: 0x000047C0
		public static Matrix3x3 operator *(Matrix3x3 {11664}, Matrix3x3 {11665})
		{
			float m = {11664}.M11 * {11665}.M11 + {11664}.M12 * {11665}.M21 + {11664}.M13 * {11665}.M31;
			float m2 = {11664}.M11 * {11665}.M12 + {11664}.M12 * {11665}.M22 + {11664}.M13 * {11665}.M32;
			float m3 = {11664}.M11 * {11665}.M13 + {11664}.M12 * {11665}.M23 + {11664}.M13 * {11665}.M33;
			float m4 = {11664}.M21 * {11665}.M11 + {11664}.M22 * {11665}.M21 + {11664}.M23 * {11665}.M31;
			float m5 = {11664}.M21 * {11665}.M12 + {11664}.M22 * {11665}.M22 + {11664}.M23 * {11665}.M32;
			float m6 = {11664}.M21 * {11665}.M13 + {11664}.M22 * {11665}.M23 + {11664}.M23 * {11665}.M33;
			float m7 = {11664}.M31 * {11665}.M11 + {11664}.M32 * {11665}.M21 + {11664}.M33 * {11665}.M31;
			float m8 = {11664}.M31 * {11665}.M12 + {11664}.M32 * {11665}.M22 + {11664}.M33 * {11665}.M32;
			float m9 = {11664}.M31 * {11665}.M13 + {11664}.M32 * {11665}.M23 + {11664}.M33 * {11665}.M33;
			Matrix3x3 result;
			result.M11 = m;
			result.M12 = m2;
			result.M13 = m3;
			result.M21 = m4;
			result.M22 = m5;
			result.M23 = m6;
			result.M31 = m7;
			result.M32 = m8;
			result.M33 = m9;
			return result;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000679C File Offset: 0x0000499C
		public static Matrix3x3 operator *(Matrix3x3 {11666}, float {11667})
		{
			Matrix3x3 result;
			Matrix3x3.Multiply(ref {11666}, {11667}, out result);
			return result;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000067B4 File Offset: 0x000049B4
		public static Matrix3x3 operator *(float {11668}, Matrix3x3 {11669})
		{
			Matrix3x3 result;
			Matrix3x3.Multiply(ref {11669}, {11668}, out result);
			return result;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000067CC File Offset: 0x000049CC
		public static void Multiply(ref Matrix3x3 {11670}, ref Matrix3x3 {11671}, out Matrix3x3 {11672})
		{
			float m = {11670}.M11 * {11671}.M11 + {11670}.M12 * {11671}.M21 + {11670}.M13 * {11671}.M31;
			float m2 = {11670}.M11 * {11671}.M12 + {11670}.M12 * {11671}.M22 + {11670}.M13 * {11671}.M32;
			float m3 = {11670}.M11 * {11671}.M13 + {11670}.M12 * {11671}.M23 + {11670}.M13 * {11671}.M33;
			float m4 = {11670}.M21 * {11671}.M11 + {11670}.M22 * {11671}.M21 + {11670}.M23 * {11671}.M31;
			float m5 = {11670}.M21 * {11671}.M12 + {11670}.M22 * {11671}.M22 + {11670}.M23 * {11671}.M32;
			float m6 = {11670}.M21 * {11671}.M13 + {11670}.M22 * {11671}.M23 + {11670}.M23 * {11671}.M33;
			float m7 = {11670}.M31 * {11671}.M11 + {11670}.M32 * {11671}.M21 + {11670}.M33 * {11671}.M31;
			float m8 = {11670}.M31 * {11671}.M12 + {11670}.M32 * {11671}.M22 + {11670}.M33 * {11671}.M32;
			float m9 = {11670}.M31 * {11671}.M13 + {11670}.M32 * {11671}.M23 + {11670}.M33 * {11671}.M33;
			{11672}.M11 = m;
			{11672}.M12 = m2;
			{11672}.M13 = m3;
			{11672}.M21 = m4;
			{11672}.M22 = m5;
			{11672}.M23 = m6;
			{11672}.M31 = m7;
			{11672}.M32 = m8;
			{11672}.M33 = m9;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000699C File Offset: 0x00004B9C
		public static void Multiply(ref Matrix3x3 {11673}, ref Matrix {11674}, out Matrix3x3 {11675})
		{
			float m = {11673}.M11 * {11674}.M11 + {11673}.M12 * {11674}.M21 + {11673}.M13 * {11674}.M31;
			float m2 = {11673}.M11 * {11674}.M12 + {11673}.M12 * {11674}.M22 + {11673}.M13 * {11674}.M32;
			float m3 = {11673}.M11 * {11674}.M13 + {11673}.M12 * {11674}.M23 + {11673}.M13 * {11674}.M33;
			float m4 = {11673}.M21 * {11674}.M11 + {11673}.M22 * {11674}.M21 + {11673}.M23 * {11674}.M31;
			float m5 = {11673}.M21 * {11674}.M12 + {11673}.M22 * {11674}.M22 + {11673}.M23 * {11674}.M32;
			float m6 = {11673}.M21 * {11674}.M13 + {11673}.M22 * {11674}.M23 + {11673}.M23 * {11674}.M33;
			float m7 = {11673}.M31 * {11674}.M11 + {11673}.M32 * {11674}.M21 + {11673}.M33 * {11674}.M31;
			float m8 = {11673}.M31 * {11674}.M12 + {11673}.M32 * {11674}.M22 + {11673}.M33 * {11674}.M32;
			float m9 = {11673}.M31 * {11674}.M13 + {11673}.M32 * {11674}.M23 + {11673}.M33 * {11674}.M33;
			{11675}.M11 = m;
			{11675}.M12 = m2;
			{11675}.M13 = m3;
			{11675}.M21 = m4;
			{11675}.M22 = m5;
			{11675}.M23 = m6;
			{11675}.M31 = m7;
			{11675}.M32 = m8;
			{11675}.M33 = m9;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006B6C File Offset: 0x00004D6C
		public static void Multiply(ref Matrix {11676}, ref Matrix3x3 {11677}, out Matrix3x3 {11678})
		{
			float m = {11676}.M11 * {11677}.M11 + {11676}.M12 * {11677}.M21 + {11676}.M13 * {11677}.M31;
			float m2 = {11676}.M11 * {11677}.M12 + {11676}.M12 * {11677}.M22 + {11676}.M13 * {11677}.M32;
			float m3 = {11676}.M11 * {11677}.M13 + {11676}.M12 * {11677}.M23 + {11676}.M13 * {11677}.M33;
			float m4 = {11676}.M21 * {11677}.M11 + {11676}.M22 * {11677}.M21 + {11676}.M23 * {11677}.M31;
			float m5 = {11676}.M21 * {11677}.M12 + {11676}.M22 * {11677}.M22 + {11676}.M23 * {11677}.M32;
			float m6 = {11676}.M21 * {11677}.M13 + {11676}.M22 * {11677}.M23 + {11676}.M23 * {11677}.M33;
			float m7 = {11676}.M31 * {11677}.M11 + {11676}.M32 * {11677}.M21 + {11676}.M33 * {11677}.M31;
			float m8 = {11676}.M31 * {11677}.M12 + {11676}.M32 * {11677}.M22 + {11676}.M33 * {11677}.M32;
			float m9 = {11676}.M31 * {11677}.M13 + {11676}.M32 * {11677}.M23 + {11676}.M33 * {11677}.M33;
			{11678}.M11 = m;
			{11678}.M12 = m2;
			{11678}.M13 = m3;
			{11678}.M21 = m4;
			{11678}.M22 = m5;
			{11678}.M23 = m6;
			{11678}.M31 = m7;
			{11678}.M32 = m8;
			{11678}.M33 = m9;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006D3C File Offset: 0x00004F3C
		public static void MultiplyTransposed(ref Matrix3x3 {11679}, ref Matrix3x3 {11680}, out Matrix3x3 {11681})
		{
			float m = {11679}.M11 * {11680}.M11 + {11679}.M21 * {11680}.M21 + {11679}.M31 * {11680}.M31;
			float m2 = {11679}.M11 * {11680}.M12 + {11679}.M21 * {11680}.M22 + {11679}.M31 * {11680}.M32;
			float m3 = {11679}.M11 * {11680}.M13 + {11679}.M21 * {11680}.M23 + {11679}.M31 * {11680}.M33;
			float m4 = {11679}.M12 * {11680}.M11 + {11679}.M22 * {11680}.M21 + {11679}.M32 * {11680}.M31;
			float m5 = {11679}.M12 * {11680}.M12 + {11679}.M22 * {11680}.M22 + {11679}.M32 * {11680}.M32;
			float m6 = {11679}.M12 * {11680}.M13 + {11679}.M22 * {11680}.M23 + {11679}.M32 * {11680}.M33;
			float m7 = {11679}.M13 * {11680}.M11 + {11679}.M23 * {11680}.M21 + {11679}.M33 * {11680}.M31;
			float m8 = {11679}.M13 * {11680}.M12 + {11679}.M23 * {11680}.M22 + {11679}.M33 * {11680}.M32;
			float m9 = {11679}.M13 * {11680}.M13 + {11679}.M23 * {11680}.M23 + {11679}.M33 * {11680}.M33;
			{11681}.M11 = m;
			{11681}.M12 = m2;
			{11681}.M13 = m3;
			{11681}.M21 = m4;
			{11681}.M22 = m5;
			{11681}.M23 = m6;
			{11681}.M31 = m7;
			{11681}.M32 = m8;
			{11681}.M33 = m9;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006F0C File Offset: 0x0000510C
		public static void MultiplyByTransposed(ref Matrix3x3 {11682}, ref Matrix3x3 {11683}, out Matrix3x3 {11684})
		{
			float m = {11682}.M11 * {11683}.M11 + {11682}.M12 * {11683}.M12 + {11682}.M13 * {11683}.M13;
			float m2 = {11682}.M11 * {11683}.M21 + {11682}.M12 * {11683}.M22 + {11682}.M13 * {11683}.M23;
			float m3 = {11682}.M11 * {11683}.M31 + {11682}.M12 * {11683}.M32 + {11682}.M13 * {11683}.M33;
			float m4 = {11682}.M21 * {11683}.M11 + {11682}.M22 * {11683}.M12 + {11682}.M23 * {11683}.M13;
			float m5 = {11682}.M21 * {11683}.M21 + {11682}.M22 * {11683}.M22 + {11682}.M23 * {11683}.M23;
			float m6 = {11682}.M21 * {11683}.M31 + {11682}.M22 * {11683}.M32 + {11682}.M23 * {11683}.M33;
			float m7 = {11682}.M31 * {11683}.M11 + {11682}.M32 * {11683}.M12 + {11682}.M33 * {11683}.M13;
			float m8 = {11682}.M31 * {11683}.M21 + {11682}.M32 * {11683}.M22 + {11682}.M33 * {11683}.M23;
			float m9 = {11682}.M31 * {11683}.M31 + {11682}.M32 * {11683}.M32 + {11682}.M33 * {11683}.M33;
			{11684}.M11 = m;
			{11684}.M12 = m2;
			{11684}.M13 = m3;
			{11684}.M21 = m4;
			{11684}.M22 = m5;
			{11684}.M23 = m6;
			{11684}.M31 = m7;
			{11684}.M32 = m8;
			{11684}.M33 = m9;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000070DC File Offset: 0x000052DC
		public static void Multiply(ref Matrix3x3 {11685}, float {11686}, out Matrix3x3 {11687})
		{
			{11687}.M11 = {11685}.M11 * {11686};
			{11687}.M12 = {11685}.M12 * {11686};
			{11687}.M13 = {11685}.M13 * {11686};
			{11687}.M21 = {11685}.M21 * {11686};
			{11687}.M22 = {11685}.M22 * {11686};
			{11687}.M23 = {11685}.M23 * {11686};
			{11687}.M31 = {11685}.M31 * {11686};
			{11687}.M32 = {11685}.M32 * {11686};
			{11687}.M33 = {11685}.M33 * {11686};
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007168 File Offset: 0x00005368
		public static void Negate(ref Matrix3x3 {11688}, out Matrix3x3 {11689})
		{
			{11689}.M11 = -{11688}.M11;
			{11689}.M12 = -{11688}.M12;
			{11689}.M13 = -{11688}.M13;
			{11689}.M21 = -{11688}.M21;
			{11689}.M22 = -{11688}.M22;
			{11689}.M23 = -{11688}.M23;
			{11689}.M31 = -{11688}.M31;
			{11689}.M32 = -{11688}.M32;
			{11689}.M33 = -{11688}.M33;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000071EC File Offset: 0x000053EC
		public static void Subtract(ref Matrix3x3 {11690}, ref Matrix3x3 {11691}, out Matrix3x3 {11692})
		{
			float m = {11690}.M11 - {11691}.M11;
			float m2 = {11690}.M12 - {11691}.M12;
			float m3 = {11690}.M13 - {11691}.M13;
			float m4 = {11690}.M21 - {11691}.M21;
			float m5 = {11690}.M22 - {11691}.M22;
			float m6 = {11690}.M23 - {11691}.M23;
			float m7 = {11690}.M31 - {11691}.M31;
			float m8 = {11690}.M32 - {11691}.M32;
			float m9 = {11690}.M33 - {11691}.M33;
			{11692}.M11 = m;
			{11692}.M12 = m2;
			{11692}.M13 = m3;
			{11692}.M21 = m4;
			{11692}.M22 = m5;
			{11692}.M23 = m6;
			{11692}.M31 = m7;
			{11692}.M32 = m8;
			{11692}.M33 = m9;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000072C0 File Offset: 0x000054C0
		public static void ToMatrix4X4(ref Matrix3x3 {11693}, out Matrix {11694})
		{
			{11694}.M11 = {11693}.M11;
			{11694}.M12 = {11693}.M12;
			{11694}.M13 = {11693}.M13;
			{11694}.M21 = {11693}.M21;
			{11694}.M22 = {11693}.M22;
			{11694}.M23 = {11693}.M23;
			{11694}.M31 = {11693}.M31;
			{11694}.M32 = {11693}.M32;
			{11694}.M33 = {11693}.M33;
			{11694}.M44 = 1f;
			{11694}.M14 = 0f;
			{11694}.M24 = 0f;
			{11694}.M34 = 0f;
			{11694}.M41 = 0f;
			{11694}.M42 = 0f;
			{11694}.M43 = 0f;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007388 File Offset: 0x00005588
		public static Matrix ToMatrix4X4(Matrix3x3 {11695})
		{
			Matrix result;
			result.M11 = {11695}.M11;
			result.M12 = {11695}.M12;
			result.M13 = {11695}.M13;
			result.M21 = {11695}.M21;
			result.M22 = {11695}.M22;
			result.M23 = {11695}.M23;
			result.M31 = {11695}.M31;
			result.M32 = {11695}.M32;
			result.M33 = {11695}.M33;
			result.M44 = 1f;
			result.M14 = 0f;
			result.M24 = 0f;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			return result;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007460 File Offset: 0x00005660
		public static Vector3 Transform(Vector3 {11696}, Matrix3x3 {11697})
		{
			float x = {11696}.X;
			float y = {11696}.Y;
			float z = {11696}.Z;
			Vector3 result;
			result.X = x * {11697}.M11 + y * {11697}.M21 + z * {11697}.M31;
			result.Y = x * {11697}.M12 + y * {11697}.M22 + z * {11697}.M32;
			result.Z = x * {11697}.M13 + y * {11697}.M23 + z * {11697}.M33;
			return result;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000074E8 File Offset: 0x000056E8
		public static void Transform(ref Vector3 {11698}, ref Matrix3x3 {11699}, out Vector3 {11700})
		{
			float x = {11698}.X;
			float y = {11698}.Y;
			float z = {11698}.Z;
			{11700}.X = x * {11699}.M11 + y * {11699}.M21 + z * {11699}.M31;
			{11700}.Y = x * {11699}.M12 + y * {11699}.M22 + z * {11699}.M32;
			{11700}.Z = x * {11699}.M13 + y * {11699}.M23 + z * {11699}.M33;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000756C File Offset: 0x0000576C
		public static void Transform(ref Vector3 {11701}, ref Matrix {11702}, out Vector3 {11703})
		{
			float x = {11701}.X;
			float y = {11701}.Y;
			float z = {11701}.Z;
			{11703}.X = x * {11702}.M11 + y * {11702}.M21 + z * {11702}.M31;
			{11703}.Y = x * {11702}.M12 + y * {11702}.M22 + z * {11702}.M32;
			{11703}.Z = x * {11702}.M13 + y * {11702}.M23 + z * {11702}.M33;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000075F0 File Offset: 0x000057F0
		public static Vector3 TransformTranspose(Vector3 {11704}, Matrix3x3 {11705})
		{
			float x = {11704}.X;
			float y = {11704}.Y;
			float z = {11704}.Z;
			Vector3 result;
			result.X = x * {11705}.M11 + y * {11705}.M12 + z * {11705}.M13;
			result.Y = x * {11705}.M21 + y * {11705}.M22 + z * {11705}.M23;
			result.Z = x * {11705}.M31 + y * {11705}.M32 + z * {11705}.M33;
			return result;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007678 File Offset: 0x00005878
		public static void TransformTranspose(ref Vector3 {11706}, ref Matrix3x3 {11707}, out Vector3 {11708})
		{
			float x = {11706}.X;
			float y = {11706}.Y;
			float z = {11706}.Z;
			{11708}.X = x * {11707}.M11 + y * {11707}.M12 + z * {11707}.M13;
			{11708}.Y = x * {11707}.M21 + y * {11707}.M22 + z * {11707}.M23;
			{11708}.Z = x * {11707}.M31 + y * {11707}.M32 + z * {11707}.M33;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000076FC File Offset: 0x000058FC
		public static void TransformTranspose(ref Vector3 {11709}, ref Matrix {11710}, out Vector3 {11711})
		{
			float x = {11709}.X;
			float y = {11709}.Y;
			float z = {11709}.Z;
			{11711}.X = x * {11710}.M11 + y * {11710}.M12 + z * {11710}.M13;
			{11711}.Y = x * {11710}.M21 + y * {11710}.M22 + z * {11710}.M23;
			{11711}.Z = x * {11710}.M31 + y * {11710}.M32 + z * {11710}.M33;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007780 File Offset: 0x00005980
		public static void Transpose(ref Matrix3x3 {11712}, out Matrix3x3 {11713})
		{
			float m = {11712}.M12;
			float m2 = {11712}.M13;
			float m3 = {11712}.M21;
			float m4 = {11712}.M23;
			float m5 = {11712}.M31;
			float m6 = {11712}.M32;
			{11713}.M11 = {11712}.M11;
			{11713}.M12 = m3;
			{11713}.M13 = m5;
			{11713}.M21 = m;
			{11713}.M22 = {11712}.M22;
			{11713}.M23 = m6;
			{11713}.M31 = m2;
			{11713}.M32 = m4;
			{11713}.M33 = {11712}.M33;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000780C File Offset: 0x00005A0C
		public static void Transpose(ref Matrix {11714}, out Matrix3x3 {11715})
		{
			float m = {11714}.M12;
			float m2 = {11714}.M13;
			float m3 = {11714}.M21;
			float m4 = {11714}.M23;
			float m5 = {11714}.M31;
			float m6 = {11714}.M32;
			{11715}.M11 = {11714}.M11;
			{11715}.M12 = m3;
			{11715}.M13 = m5;
			{11715}.M21 = m;
			{11715}.M22 = {11714}.M22;
			{11715}.M23 = m6;
			{11715}.M31 = m2;
			{11715}.M32 = m4;
			{11715}.M33 = {11714}.M33;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007898 File Offset: 0x00005A98
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{",
				this.M11.ToString(),
				", ",
				this.M12.ToString(),
				", ",
				this.M13.ToString(),
				"} {",
				this.M21.ToString(),
				", ",
				this.M22.ToString(),
				", ",
				this.M23.ToString(),
				"} {",
				this.M31.ToString(),
				", ",
				this.M32.ToString(),
				", ",
				this.M33.ToString(),
				"}"
			});
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000798C File Offset: 0x00005B8C
		public float Determinant()
		{
			return this.M11 * this.M22 * this.M33 + this.M12 * this.M23 * this.M31 + this.M13 * this.M21 * this.M32 - this.M31 * this.M22 * this.M13 - this.M32 * this.M23 * this.M11 - this.M33 * this.M21 * this.M12;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007A18 File Offset: 0x00005C18
		internal float AdaptiveDeterminant(out int {11716})
		{
			float num = this.M11 * this.M22 * this.M33 + this.M12 * this.M23 * this.M31 + this.M13 * this.M21 * this.M32 - this.M31 * this.M22 * this.M13 - this.M32 * this.M23 * this.M11 - this.M33 * this.M21 * this.M12;
			if (num != 0f)
			{
				{11716} = 0;
				return num;
			}
			num = this.M11 * this.M22 - this.M12 * this.M21;
			if (num != 0f)
			{
				{11716} = 1;
				return num;
			}
			num = this.M22 * this.M33 - this.M23 * this.M32;
			if (num != 0f)
			{
				{11716} = 2;
				return num;
			}
			num = this.M11 * this.M33 - this.M13 * this.M12;
			if (num != 0f)
			{
				{11716} = 3;
				return num;
			}
			if (this.M11 != 0f)
			{
				{11716} = 4;
				return this.M11;
			}
			if (this.M22 != 0f)
			{
				{11716} = 5;
				return this.M22;
			}
			if (this.M33 != 0f)
			{
				{11716} = 6;
				return this.M33;
			}
			{11716} = -1;
			return 0f;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007B78 File Offset: 0x00005D78
		public static void CreateQuaternion(ref Matrix3x3 {11717}, out Quaternion {11718})
		{
			float num = {11717}.M11 + {11717}.M22 + {11717}.M33;
			if (num >= 0f)
			{
				float num2 = (float)Math.Sqrt((double)num + 1.0) * 2f;
				float num3 = 1f / num2;
				{11718}.W = 0.25f * num2;
				{11718}.X = ({11717}.M23 - {11717}.M32) * num3;
				{11718}.Y = ({11717}.M31 - {11717}.M13) * num3;
				{11718}.Z = ({11717}.M12 - {11717}.M21) * num3;
				return;
			}
			if ({11717}.M11 > {11717}.M22 & {11717}.M11 > {11717}.M33)
			{
				float num4 = (float)Math.Sqrt(1.0 + (double){11717}.M11 - (double){11717}.M22 - (double){11717}.M33) * 2f;
				float num5 = 1f / num4;
				{11718}.W = ({11717}.M23 - {11717}.M32) * num5;
				{11718}.X = 0.25f * num4;
				{11718}.Y = ({11717}.M21 + {11717}.M12) * num5;
				{11718}.Z = ({11717}.M31 + {11717}.M13) * num5;
				return;
			}
			if ({11717}.M22 > {11717}.M33)
			{
				float num6 = (float)Math.Sqrt(1.0 + (double){11717}.M22 - (double){11717}.M11 - (double){11717}.M33) * 2f;
				float num7 = 1f / num6;
				{11718}.W = ({11717}.M31 - {11717}.M13) * num7;
				{11718}.X = ({11717}.M21 + {11717}.M12) * num7;
				{11718}.Y = 0.25f * num6;
				{11718}.Z = ({11717}.M32 + {11717}.M23) * num7;
				return;
			}
			float num8 = (float)Math.Sqrt(1.0 + (double){11717}.M33 - (double){11717}.M11 - (double){11717}.M22) * 2f;
			float num9 = 1f / num8;
			{11718}.W = ({11717}.M12 - {11717}.M21) * num9;
			{11718}.X = ({11717}.M31 + {11717}.M13) * num9;
			{11718}.Y = ({11717}.M32 + {11717}.M23) * num9;
			{11718}.Z = 0.25f * num8;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007DE0 File Offset: 0x00005FE0
		public static Quaternion CreateQuaternion(Matrix3x3 {11719})
		{
			Quaternion result;
			Matrix3x3.CreateQuaternion(ref {11719}, out result);
			return result;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007DF8 File Offset: 0x00005FF8
		public static void CreateFromQuaternion(ref Quaternion {11720}, out Matrix3x3 {11721})
		{
			float num = 2f * {11720}.X * {11720}.X;
			float num2 = 2f * {11720}.Y * {11720}.Y;
			float num3 = 2f * {11720}.Z * {11720}.Z;
			float num4 = 2f * {11720}.X * {11720}.Y;
			float num5 = 2f * {11720}.X * {11720}.Z;
			float num6 = 2f * {11720}.X * {11720}.W;
			float num7 = 2f * {11720}.Y * {11720}.Z;
			float num8 = 2f * {11720}.Y * {11720}.W;
			float num9 = 2f * {11720}.Z * {11720}.W;
			{11721}.M11 = 1f - num2 - num3;
			{11721}.M21 = num4 - num9;
			{11721}.M31 = num5 + num8;
			{11721}.M12 = num4 + num9;
			{11721}.M22 = 1f - num - num3;
			{11721}.M32 = num7 - num6;
			{11721}.M13 = num5 - num8;
			{11721}.M23 = num7 + num6;
			{11721}.M33 = 1f - num - num2;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007F2C File Offset: 0x0000612C
		public static Matrix3x3 CreateFromQuaternion(Quaternion {11722})
		{
			Matrix3x3 result;
			Matrix3x3.CreateFromQuaternion(ref {11722}, out result);
			return result;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007F44 File Offset: 0x00006144
		public static void CreateOuterProduct(ref Vector3 {11723}, ref Vector3 {11724}, out Matrix3x3 {11725})
		{
			{11725}.M11 = {11723}.X * {11724}.X;
			{11725}.M12 = {11723}.X * {11724}.Y;
			{11725}.M13 = {11723}.X * {11724}.Z;
			{11725}.M21 = {11723}.Y * {11724}.X;
			{11725}.M22 = {11723}.Y * {11724}.Y;
			{11725}.M23 = {11723}.Y * {11724}.Z;
			{11725}.M31 = {11723}.Z * {11724}.X;
			{11725}.M32 = {11723}.Z * {11724}.Y;
			{11725}.M33 = {11723}.Z * {11724}.Z;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007FFC File Offset: 0x000061FC
		public static Matrix3x3 CreateFromAxisAngle(Vector3 {11726}, float {11727})
		{
			Matrix3x3 result;
			Matrix3x3.CreateFromAxisAngle(ref {11726}, {11727}, out result);
			return result;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00008014 File Offset: 0x00006214
		public static void CreateFromAxisAngle(ref Vector3 {11728}, float {11729}, out Matrix3x3 {11730})
		{
			float num = {11728}.X * {11728}.X;
			float num2 = {11728}.Y * {11728}.Y;
			float num3 = {11728}.Z * {11728}.Z;
			float num4 = {11728}.X * {11728}.Y;
			float num5 = {11728}.X * {11728}.Z;
			float num6 = {11728}.Y * {11728}.Z;
			float num7 = (float)Math.Sin((double){11729});
			float num8 = 1f - (float)Math.Cos((double){11729});
			{11730}.M11 = 1f + num8 * (num - 1f);
			{11730}.M21 = -{11728}.Z * num7 + num8 * num4;
			{11730}.M31 = {11728}.Y * num7 + num8 * num5;
			{11730}.M12 = {11728}.Z * num7 + num8 * num4;
			{11730}.M22 = 1f + num8 * (num2 - 1f);
			{11730}.M32 = -{11728}.X * num7 + num8 * num6;
			{11730}.M13 = -{11728}.Y * num7 + num8 * num5;
			{11730}.M23 = {11728}.X * num7 + num8 * num6;
			{11730}.M33 = 1f + num8 * (num3 - 1f);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00008154 File Offset: 0x00006354
		public override bool Equals(object {11731})
		{
			if ({11731} == null)
			{
				return false;
			}
			if ({11731}.GetType() != base.GetType())
			{
				return false;
			}
			Matrix3x3 matrix3x = (Matrix3x3){11731};
			return matrix3x.M11 == this.M11 && matrix3x.M12 == this.M12 && matrix3x.M13 == this.M13 && matrix3x.M21 == this.M21 && matrix3x.M22 == this.M22 && matrix3x.M23 == this.M23 && matrix3x.M31 == this.M31 && matrix3x.M32 == this.M32 && matrix3x.M33 == this.M33;
		}

		// Token: 0x040000B2 RID: 178
		public float M11;

		// Token: 0x040000B3 RID: 179
		public float M12;

		// Token: 0x040000B4 RID: 180
		public float M13;

		// Token: 0x040000B5 RID: 181
		public float M21;

		// Token: 0x040000B6 RID: 182
		public float M22;

		// Token: 0x040000B7 RID: 183
		public float M23;

		// Token: 0x040000B8 RID: 184
		public float M31;

		// Token: 0x040000B9 RID: 185
		public float M32;

		// Token: 0x040000BA RID: 186
		public float M33;
	}
}
