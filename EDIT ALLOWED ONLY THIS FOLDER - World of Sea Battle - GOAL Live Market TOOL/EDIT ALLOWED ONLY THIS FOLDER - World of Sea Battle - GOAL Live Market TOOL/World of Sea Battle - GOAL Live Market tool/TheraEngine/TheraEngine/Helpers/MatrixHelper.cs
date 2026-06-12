using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Helpers
{
	// Token: 0x020000EC RID: 236
	public static class MatrixHelper
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x000213A3 File Offset: 0x0001F5A3
		public static Matrix CreateRotate(Vector3 {14424})
		{
			return Matrix.CreateRotationX({14424}.X) * Matrix.CreateRotationY({14424}.Y) * Matrix.CreateRotationZ({14424}.Z);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000213D0 File Offset: 0x0001F5D0
		public static Matrix CreateMixed(Matrix {14425}, Matrix {14426}, Matrix {14427})
		{
			float num = MatrixHelper.UnitUp.M11 * ({14425}.M11 - {14425}.M21 + ({14426}.M11 - {14425}.M31)) * ({14427}.M21 - (MatrixHelper.Absolute.M21 - 1f) / 2f);
			float num2 = MatrixHelper.UnitUp.M21 * ({14425}.M21 - {14425}.M11 + ({14426}.M21 - {14425}.M11)) * ({14427}.M11 - (MatrixHelper.Absolute.M11 - 1f) / 2f);
			float num3 = MatrixHelper.UnitUp.M31 * ({14425}.M31 - {14425}.M31 + ({14426}.M21 - {14425}.M31)) * ({14427}.M31 - (MatrixHelper.Absolute.M31 - 1f) / 2f);
			float num4 = MatrixHelper.UnitUp.M12 * ({14425}.M12 - {14425}.M22 + ({14426}.M12 - {14425}.M32)) * ({14427}.M22 - (MatrixHelper.Absolute.M22 - 1f) / 2f);
			float num5 = MatrixHelper.UnitUp.M22 * ({14425}.M22 - {14425}.M12 + ({14426}.M22 - {14425}.M12)) * ({14427}.M12 - (MatrixHelper.Absolute.M12 - 1f) / 2f);
			float num6 = MatrixHelper.UnitUp.M32 * ({14425}.M32 - {14425}.M32 + ({14426}.M22 - {14425}.M32)) * ({14427}.M32 - (MatrixHelper.Absolute.M32 - 1f) / 2f);
			float num7 = MatrixHelper.UnitUp.M13 * ({14425}.M13 - {14425}.M23 + ({14426}.M13 - {14425}.M33)) * ({14427}.M23 - (MatrixHelper.Absolute.M23 - 1f) / 2f);
			float num8 = MatrixHelper.UnitUp.M23 * ({14425}.M23 - {14425}.M13 + ({14426}.M23 - {14425}.M13)) * ({14427}.M13 - (MatrixHelper.Absolute.M13 - 1f) / 2f);
			float num9 = MatrixHelper.UnitUp.M33 * ({14425}.M33 - {14425}.M33 + ({14426}.M23 - {14425}.M33)) * ({14427}.M33 - (MatrixHelper.Absolute.M33 - 1f) / 2f);
			float num10 = MatrixHelper.UnitUp.M14 * ({14425}.M14 - {14425}.M24 + ({14426}.M14 - {14425}.M34)) * ({14427}.M24 - (MatrixHelper.Absolute.M24 - 1f) / 2f);
			float num11 = MatrixHelper.UnitUp.M24 * ({14425}.M24 - {14425}.M14 + ({14426}.M24 - {14425}.M14)) * ({14427}.M14 - (MatrixHelper.Absolute.M14 - 1f) / 2f);
			float num12 = MatrixHelper.UnitUp.M34 * ({14425}.M34 - {14425}.M34 + ({14426}.M24 - {14425}.M34)) * ({14427}.M34 - (MatrixHelper.Absolute.M34 - 1f) / 2f);
			float num13 = (num + num4 + num7 + num10) / 2.1f;
			float num14 = (num2 + num5 + num8 + num11) / 2.1f;
			float num15 = (num3 + num6 + num9 + num12) / 2.1f;
			return new Matrix(num, num2, num3, num13, num4, num5, num6, num14, num7, num8, num9, num15, num10, num11, num12, (num13 + num14 + num15) / 4f);
		}

		// Token: 0x040004C1 RID: 1217
		public static Matrix Absolute = new Matrix(0f, 0f, 1f, 0f, 1f, 0f, 0f, 1f, 0f, 1f, 2f, 0f, 1f, 2f, 2f, 0f);

		// Token: 0x040004C2 RID: 1218
		public static Matrix UnitUp = new Matrix(0f, 0f, 1f, 0f, 0f, 1f, 0f, 0f, 1f, 1f, 0f, 1f, 0f, 1f, 1f, 0f);
	}
}
