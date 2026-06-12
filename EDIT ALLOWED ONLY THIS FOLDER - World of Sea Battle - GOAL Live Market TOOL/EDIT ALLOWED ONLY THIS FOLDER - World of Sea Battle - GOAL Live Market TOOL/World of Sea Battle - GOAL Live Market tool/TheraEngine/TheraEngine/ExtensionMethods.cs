using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;
using TheraEngine.Helpers;

namespace TheraEngine
{
	// Token: 0x02000021 RID: 33
	public static class ExtensionMethods
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00004080 File Offset: 0x00002280
		public static Vector2 WidthHeight(this Rectangle {11432})
		{
			Vector2 result;
			result.X = (float){11432}.Width;
			result.Y = (float){11432}.Height;
			return result;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000040AC File Offset: 0x000022AC
		public static Vector2 HalfWidthHeight(this Rectangle {11433})
		{
			Vector2 result;
			result.X = (float){11433}.Width / 2f;
			result.Y = (float){11433}.Height / 2f;
			return result;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000040E4 File Offset: 0x000022E4
		public static Vector2 HalfWidthHeightInt(this Rectangle {11434})
		{
			Vector2 result;
			result.X = (float)({11434}.Width / 2);
			result.Y = (float)({11434}.Height / 2);
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004112 File Offset: 0x00002312
		public static Rectangle SetY(this Rectangle {11435}, float {11436})
		{
			return new Rectangle({11435}.X, (int){11436}, {11435}.Width, {11435}.Height);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000412D File Offset: 0x0000232D
		public static Rectangle SetWidth(this Rectangle {11437}, float {11438})
		{
			return new Rectangle({11437}.X, {11437}.Y, (int){11438}, {11437}.Height);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004148 File Offset: 0x00002348
		public static Rectangle SetHeight(this Rectangle {11439}, float {11440})
		{
			return new Rectangle({11439}.X, {11439}.Y, {11439}.Width, (int){11440});
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004163 File Offset: 0x00002363
		public static Rectangle SetX(this Rectangle {11441}, float {11442})
		{
			return new Rectangle((int){11442}, {11441}.Y, {11441}.Width, {11441}.Height);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000417E File Offset: 0x0000237E
		public static Rectangle AddX(this Rectangle {11443}, float {11444})
		{
			return new Rectangle({11443}.X + (int){11444}, {11443}.Y, {11443}.Width, {11443}.Height);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000041A0 File Offset: 0x000023A0
		public static Rectangle AddY(this Rectangle {11445}, float {11446})
		{
			return new Rectangle({11445}.X, {11445}.Y + (int){11446}, {11445}.Width, {11445}.Height);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000041C2 File Offset: 0x000023C2
		public static Vector2 Normal(this Vector2 {11447})
		{
			{11447}.Normalize();
			return {11447};
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000041CC File Offset: 0x000023CC
		public static Vector3 Normal(this Vector3 {11448})
		{
			{11448}.Normalize();
			return {11448};
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000041D8 File Offset: 0x000023D8
		public static Vector2 YX(this Vector2 {11449})
		{
			Vector2 result;
			result.X = {11449}.Y;
			result.Y = {11449}.X;
			return result;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004200 File Offset: 0x00002400
		public static Vector2 XZ(this Vector3 {11450})
		{
			Vector2 result;
			result.X = {11450}.X;
			result.Y = {11450}.Z;
			return result;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004228 File Offset: 0x00002428
		public static Vector2 XY(this Vector3 {11451})
		{
			Vector2 result;
			result.X = {11451}.X;
			result.Y = {11451}.Y;
			return result;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004250 File Offset: 0x00002450
		public static Vector3 X0Y(this Vector2 {11452})
		{
			Vector3 result;
			result.X = {11452}.X;
			result.Y = 0f;
			result.Z = {11452}.Y;
			return result;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004284 File Offset: 0x00002484
		public static Vector2 XZNormal(this Vector3 {11453})
		{
			Vector2 result;
			result.X = {11453}.X;
			result.Y = {11453}.Z;
			result.Normalize();
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000042B3 File Offset: 0x000024B3
		public static Vector3 AddY(this Vector3 {11454}, float {11455})
		{
			{11454}.Y += {11455};
			return {11454};
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000042C4 File Offset: 0x000024C4
		public static Vector2 GetMinPos(this Tlist<Vector2> {11456})
		{
			if ({11456}.Size == 0)
			{
				throw new ArgumentException();
			}
			Vector2 result = {11456}.Array[0];
			for (int i = 1; i < {11456}.Size; i++)
			{
				Vector2.Min(ref result, ref {11456}.Array[i], out result);
			}
			return result;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004314 File Offset: 0x00002514
		public static Vector2 GetMaxPos(this Tlist<Vector2> {11457})
		{
			if ({11457}.Size == 0)
			{
				throw new ArgumentException();
			}
			Vector2 result = {11457}.Array[0];
			for (int i = 1; i < {11457}.Size; i++)
			{
				Vector2.Max(ref result, ref {11457}.Array[i], out result);
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004364 File Offset: 0x00002564
		public static Vector2 GetMiddlePos(this Tlist<Vector2> {11458})
		{
			if ({11458}.Size == 0)
			{
				throw new ArgumentException();
			}
			Vector2 value = {11458}.Array[0];
			for (int i = 1; i < {11458}.Size; i++)
			{
				value += {11458}.Array[i];
			}
			return value / (float){11458}.Size;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000043BF File Offset: 0x000025BF
		public static Vector2 GetNearVector(this Tlist<Vector2> {11459}, Vector2 {11460})
		{
			return {11459}.Array[{11459}.GetNearVectorIndex({11460})];
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000043D4 File Offset: 0x000025D4
		public static int GetNearVectorIndex(this Tlist<Vector2> {11461}, Vector2 {11462})
		{
			if ({11461}.Size == 0)
			{
				throw new ArgumentException();
			}
			float num = float.MaxValue;
			int result = 0;
			for (int i = 0; i < {11461}.Size; i++)
			{
				float num2;
				Vector2.DistanceSquared(ref {11462}, ref {11461}.Array[i], out num2);
				if (num2 < num)
				{
					num = num2;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004428 File Offset: 0x00002628
		public static Vector3 GetNearVector(this Tlist<Vector3> {11463}, Vector3 {11464})
		{
			if ({11463}.Size == 0)
			{
				throw new ArgumentException();
			}
			float num = float.MaxValue;
			Vector3 result = {11463}.Array[0];
			for (int i = 0; i < {11463}.Size; i++)
			{
				float num2;
				Vector3.DistanceSquared(ref {11464}, ref {11463}.Array[i], out num2);
				if (num2 < num)
				{
					num = num2;
					result = {11463}.Array[i];
				}
			}
			return result;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004490 File Offset: 0x00002690
		public static Vector3 GetMiddlePos(this Tlist<Vector3> {11465})
		{
			if ({11465}.Size == 0)
			{
				throw new ArgumentException();
			}
			Vector3 vector = {11465}.Array[0];
			for (int i = 1; i < {11465}.Size; i++)
			{
				vector += {11465}.Array[i];
			}
			return vector / (float){11465}.Size;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000044EC File Offset: 0x000026EC
		public static void Shuffle<T>(this T[] {11466})
		{
			int i = {11466}.Length;
			while (i > 1)
			{
				int num = Rand.RangeInt(0, i--);
				T t = {11466}[i];
				{11466}[i] = {11466}[num];
				{11466}[num] = t;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000452D File Offset: 0x0000272D
		public static Vector3 NormalizeLocal(this Vector3 {11467})
		{
			if ({11467}.X * {11467}.Y * {11467}.Z == 0f)
			{
				return {11467};
			}
			{11467}.Normalize();
			return {11467};
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004554 File Offset: 0x00002754
		public static Vector3 Saturate(this Vector3 {11468})
		{
			{11468}.X = Geometry.Saturate({11468}.X);
			{11468}.Y = Geometry.Saturate({11468}.Y);
			{11468}.Z = Geometry.Saturate({11468}.Z);
			return {11468};
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004590 File Offset: 0x00002790
		public static Color Multiply(this Color {11469}, Color {11470})
		{
			return new Color((float){11469}.R / 255f * ((float){11470}.R / 255f), (float){11469}.G / 255f * ((float){11470}.G / 255f), (float){11469}.B / 255f * ((float){11470}.B / 255f), (float){11469}.A / 255f * ((float){11470}.A / 255f));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004616 File Offset: 0x00002816
		public static Color MultiplyAlpha(this Color {11471}, float {11472})
		{
			{11471}.A = (byte)((float){11471}.A * {11472});
			return {11471};
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000462B File Offset: 0x0000282B
		public static Color Adjust(this Color {11473}, float {11474})
		{
			return new Color(Vector3.Lerp(new Vector3(Vector3.Dot({11473}.ToVector3(), new Vector3(0.3f, 0.59f, 0.11f))), {11473}.ToVector3(), {11474}));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004664 File Offset: 0x00002864
		public static string ToStringRound(this Vector2 {11475}, int {11476} = 1)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
			defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round((double){11475}.X, {11476}));
			defaultInterpolatedStringHandler.AppendLiteral("; ");
			defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round((double){11475}.Y, {11476}));
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000046B8 File Offset: 0x000028B8
		public static string ToStringRound(this Vector3 {11477}, int {11478} = 1)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
			defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round((double){11477}.X, {11478}));
			defaultInterpolatedStringHandler.AppendLiteral("; ");
			defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round((double){11477}.Y, {11478}));
			defaultInterpolatedStringHandler.AppendLiteral("; ");
			defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round((double){11477}.Z, {11478}));
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}
	}
}
