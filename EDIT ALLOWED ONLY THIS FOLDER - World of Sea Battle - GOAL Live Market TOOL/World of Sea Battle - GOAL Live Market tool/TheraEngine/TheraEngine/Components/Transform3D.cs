using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Components
{
	// Token: 0x02000104 RID: 260
	public class Transform3D
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x000255B4 File Offset: 0x000237B4
		public Vector2 ForwardX
		{
			get
			{
				return new Vector2(MathF.Cos(this.Yaw), MathF.Sin(this.Yaw));
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x000255D1 File Offset: 0x000237D1
		public Vector2 ForwardZ
		{
			get
			{
				return new Vector2(MathF.Cos(this.Yaw + 1.5707964f), MathF.Sin(this.Yaw + 1.5707964f));
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x000255FA File Offset: 0x000237FA
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x00025628 File Offset: 0x00023828
		public float MiddleScale
		{
			get
			{
				return (this.Scales.X + this.Scales.Y + this.Scales.Z) / 3f;
			}
			set
			{
				this.Scales.Z = value;
				this.Scales.Y = value;
				this.Scales.X = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0002565D File Offset: 0x0002385D
		public bool IsPositiveScale
		{
			get
			{
				return this.Scales.X * this.Scales.Y * this.Scales.Z > 0f;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00025689 File Offset: 0x00023889
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x000256A2 File Offset: 0x000238A2
		public Vector3 RotatesAll
		{
			get
			{
				return new Vector3(this.Pitch, this.Yaw, this.Roll);
			}
			set
			{
				this.Pitch = value.X;
				this.Yaw = value.Y;
				this.Roll = value.Z;
			}
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000256C8 File Offset: 0x000238C8
		public Transform3D() : this(Transform3D.zero, Transform3D.zero, Transform3D.one)
		{
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000256E0 File Offset: 0x000238E0
		public Transform3D(Transform3D {14806})
		{
			this.Translation = {14806}.Translation;
			this.Scales = {14806}.Scales;
			this.Pitch = {14806}.Pitch;
			this.Yaw = {14806}.Yaw;
			this.Roll = {14806}.Roll;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0002572F File Offset: 0x0002392F
		public Transform3D(Vector3 {14807}, Vector3 {14808}, Vector3 {14809})
		{
			this.Translation = {14807};
			this.Scales = {14809};
			this.Pitch = {14808}.X;
			this.Yaw = {14808}.Y;
			this.Roll = {14808}.Z;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0002576C File Offset: 0x0002396C
		public Matrix CreateWorldMatrix()
		{
			Matrix result;
			this.CreateWorldMatrix(out result);
			return result;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00025784 File Offset: 0x00023984
		public void CreateWorldMatrix(out Matrix {14810})
		{
			float x = -this.Roll * 0.5f;
			float num = MathF.Sin(x);
			float num2 = MathF.Cos(x);
			float x2 = -this.Pitch * 0.5f;
			float num3 = MathF.Sin(x2);
			float num4 = MathF.Cos(x2);
			float x3 = -this.Yaw * 0.5f;
			float num5 = MathF.Sin(x3);
			float num6 = MathF.Cos(x3);
			Vector4 vector;
			vector.X = num6 * num3 * num2 + num5 * num4 * num;
			vector.Y = num5 * num4 * num2 - num6 * num3 * num;
			vector.Z = num6 * num4 * num - num5 * num3 * num2;
			vector.W = num6 * num4 * num2 + num5 * num3 * num;
			float num7 = vector.X * vector.X;
			float num8 = vector.Y * vector.Y;
			float num9 = vector.Z * vector.Z;
			float num10 = vector.X * vector.Y;
			float num11 = vector.Z * vector.W;
			float num12 = vector.Z * vector.X;
			float num13 = vector.Y * vector.W;
			float num14 = vector.Y * vector.Z;
			float num15 = vector.X * vector.W;
			Matrix matrix;
			matrix.M11 = 1f - 2f * (num8 + num9);
			matrix.M12 = 2f * (num10 + num11);
			matrix.M13 = 2f * (num12 - num13);
			matrix.M21 = 2f * (num10 - num11);
			matrix.M22 = 1f - 2f * (num9 + num7);
			matrix.M23 = 2f * (num14 + num15);
			matrix.M31 = 2f * (num12 + num13);
			matrix.M32 = 2f * (num14 - num15);
			matrix.M33 = 1f - 2f * (num8 + num7);
			{14810}.M11 = this.Scales.X * matrix.M11;
			{14810}.M12 = this.Scales.X * matrix.M12;
			{14810}.M13 = this.Scales.X * matrix.M13;
			{14810}.M21 = this.Scales.Y * matrix.M21;
			{14810}.M22 = this.Scales.Y * matrix.M22;
			{14810}.M23 = this.Scales.Y * matrix.M23;
			{14810}.M31 = this.Scales.Z * matrix.M31;
			{14810}.M32 = this.Scales.Z * matrix.M32;
			{14810}.M33 = this.Scales.Z * matrix.M33;
			{14810}.M14 = 0f;
			{14810}.M24 = 0f;
			{14810}.M34 = 0f;
			{14810}.M41 = this.Translation.X;
			{14810}.M42 = this.Translation.Y;
			{14810}.M43 = this.Translation.Z;
			{14810}.M44 = 1f;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00025AB4 File Offset: 0x00023CB4
		public void CreateWorldMatrixInvTransposed(out Matrix {14811})
		{
			Matrix matrix;
			this.CreateWorldMatrix(out matrix);
			Matrix.Invert(ref matrix, out matrix);
			Matrix.Transpose(ref matrix, out {14811});
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00025ADC File Offset: 0x00023CDC
		public Matrix CreateWorldMatrixRotationAndScaleOnly()
		{
			Matrix result;
			this.CreateWorldMatrixRotationAndScaleOnly(out result);
			return result;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00025AF4 File Offset: 0x00023CF4
		public void CreateWorldMatrixRotationAndScaleOnly(out Matrix {14812})
		{
			Matrix.CreateFromYawPitchRoll(-this.Yaw, -this.Pitch, -this.Roll, out {14812});
			Matrix matrix;
			Matrix.CreateScale(ref this.Scales, out matrix);
			Matrix.Multiply(ref {14812}, ref matrix, out {14812});
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00025B34 File Offset: 0x00023D34
		public void CreateInvertedfulWorld(out Matrix {14813})
		{
			Vector3 vector;
			vector.X = -this.Translation.X;
			vector.Y = -this.Translation.Y;
			vector.Z = -this.Translation.Z;
			Matrix.CreateTranslation(ref vector, out {14813});
			Vector3 vector2;
			vector2.X = 1f / this.Scales.X;
			vector2.Y = 1f / this.Scales.Y;
			vector2.Z = 1f / this.Scales.Z;
			Matrix matrix;
			Matrix.CreateScale(ref vector2, out matrix);
			Matrix.Multiply(ref {14813}, ref matrix, out {14813});
			Matrix.CreateFromYawPitchRoll(this.Yaw, this.Pitch, this.Roll, out matrix);
			Matrix.Multiply(ref {14813}, ref matrix, out {14813});
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00025C00 File Offset: 0x00023E00
		public Quaternion GetQuaternion()
		{
			Quaternion result;
			Quaternion.CreateFromYawPitchRoll(-this.Yaw, -this.Pitch, -this.Roll, out result);
			return result;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00025C2A File Offset: 0x00023E2A
		public void GetQuaternion(out Quaternion {14814})
		{
			Quaternion.CreateFromYawPitchRoll(this.Yaw, this.Pitch, this.Roll, out {14814});
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00025C44 File Offset: 0x00023E44
		public Vector3 Transform3X3(Vector3 {14815})
		{
			Matrix matrix;
			this.CreateWorldMatrix(out matrix);
			Vector3.Transform(ref {14815}, ref matrix, out {14815});
			return {14815};
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00025C68 File Offset: 0x00023E68
		public void Transform3X3(ref Vector3 {14816}, out Vector3 {14817})
		{
			Matrix matrix;
			this.CreateWorldMatrix(out matrix);
			Vector3.Transform(ref {14816}, ref matrix, out {14817});
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00025C88 File Offset: 0x00023E88
		public Vector3 InvTransform3X3(Vector3 {14818})
		{
			Matrix matrix;
			this.CreateInvertedfulWorld(out matrix);
			Vector3.Transform(ref {14818}, ref matrix, out {14818});
			return {14818};
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00025CA9 File Offset: 0x00023EA9
		public void CopyFrom(Transform3D {14819})
		{
			this.Translation = {14819}.Translation;
			this.Yaw = {14819}.Yaw;
			this.Pitch = {14819}.Pitch;
			this.Roll = {14819}.Roll;
			this.Scales = {14819}.Scales;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00025CE8 File Offset: 0x00023EE8
		public void CreateCenterPivotRotation(Vector3 {14820}, Vector3 {14821})
		{
			Vector3 value = -{14820};
			Matrix matrix = Matrix.CreateFromYawPitchRoll(-{14821}.Y, -{14821}.X, -{14821}.Z);
			Vector3.Transform(ref value, ref matrix, out value);
			this.Translation = value + {14820};
			this.RotatesAll = {14821};
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00025D38 File Offset: 0x00023F38
		public void CreateCenterPivotRotationAndScale(Vector3 {14822}, Vector3 {14823}, Vector3 {14824})
		{
			Vector3 value = -{14822} * {14824};
			Matrix matrix = Matrix.CreateFromYawPitchRoll(-{14823}.Y, -{14823}.X, -{14823}.Z);
			Vector3.Transform(ref value, ref matrix, out value);
			this.Translation = value + {14822};
			this.RotatesAll = {14823};
			this.Scales = {14824};
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00025D94 File Offset: 0x00023F94
		public void CreateMultipivot(Vector3 {14825}, Vector3 {14826}, Vector3 {14827}, Vector3 {14828})
		{
			Vector3 value = -{14825};
			Matrix matrix = Matrix.CreateFromYawPitchRoll(-{14826}.Y, -{14826}.X, -{14826}.Z);
			Matrix.CreateFromYawPitchRoll({14826}.Y, {14826}.X, {14826}.Z);
			Vector3.Transform(ref value, ref matrix, out value);
			Vector3 value2 = Vector3.Transform({14827}, matrix) + value + {14825} - {14827};
			this.CreateCenterPivotRotation({14827}, {14828});
			this.Translation += value2;
			this.Translation.Y = 0f;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00025E2C File Offset: 0x0002402C
		public static void CreateCenterPivotRotation(ref Vector3 {14829}, ref Vector3 {14830}, out Matrix {14831})
		{
			Vector3 value = -{14829};
			Matrix matrix = Matrix.CreateFromYawPitchRoll(-{14830}.Y, -{14830}.X, -{14830}.Z);
			Vector3.Transform(ref value, ref matrix, out value);
			Vector3 vector = value + {14829};
			Vector3 vector2 = {14830};
			Matrix.CreateFromYawPitchRoll(-vector2.Y, -vector2.X, -vector2.Z, out {14831});
			Matrix matrix2;
			Matrix.CreateTranslation(ref vector, out matrix2);
			Matrix.Multiply(ref {14831}, ref matrix2, out {14831});
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00025EAC File Offset: 0x000240AC
		public void Multiply(Transform3D {14832})
		{
			Matrix matrix;
			{14832}.CreateWorldMatrix(out matrix);
			Vector3.Transform(ref this.Translation, ref matrix, out this.Translation);
			this.Yaw += {14832}.Yaw;
			this.Pitch += {14832}.Pitch;
			this.Roll += {14832}.Roll;
			this.MiddleScale *= {14832}.MiddleScale;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00025F20 File Offset: 0x00024120
		public static Transform3D Identity
		{
			get
			{
				return new Transform3D(Vector3.Zero, Vector3.Zero, Vector3.One);
			}
		}

		// Token: 0x0400053C RID: 1340
		public static readonly Transform3D ReadonlyNormalTransform = Transform3D.Identity;

		// Token: 0x0400053D RID: 1341
		public Vector3 Translation;

		// Token: 0x0400053E RID: 1342
		public float Pitch;

		// Token: 0x0400053F RID: 1343
		public float Yaw;

		// Token: 0x04000540 RID: 1344
		public float Roll;

		// Token: 0x04000541 RID: 1345
		public Vector3 Scales;

		// Token: 0x04000542 RID: 1346
		private static readonly Vector3 zero = Vector3.Zero;

		// Token: 0x04000543 RID: 1347
		private static readonly Vector3 one = Vector3.One;
	}
}
