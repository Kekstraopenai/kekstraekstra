using System;
using Microsoft.Xna.Framework;

namespace TheraEngine
{
	// Token: 0x02000026 RID: 38
	public struct Index2D : IEquatable<Index2D>
	{
		// Token: 0x06000150 RID: 336 RVA: 0x0000820C File Offset: 0x0000640C
		public Index2D(Vector2 {11736}, float {11737})
		{
			this.X = (int)Math.Floor((double)({11736}.X / {11737}));
			this.Y = (int)Math.Floor((double)({11736}.Y / {11737}));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008238 File Offset: 0x00006438
		public Index2D(int {11738}, int {11739})
		{
			this.X = {11738};
			this.Y = {11739};
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00008248 File Offset: 0x00006448
		public override int GetHashCode()
		{
			return this.X * 1023 + this.Y;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00008260 File Offset: 0x00006460
		public override bool Equals(object {11740})
		{
			Index2D index2D = (Index2D){11740};
			return index2D.X == this.X && index2D.Y == this.Y;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008292 File Offset: 0x00006492
		public override string ToString()
		{
			return "X: " + this.X.ToString() + ", Y: " + this.Y.ToString();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000082B9 File Offset: 0x000064B9
		public static bool operator ==(Index2D {11741}, Index2D {11742})
		{
			return {11741}.X == {11742}.X && {11741}.Y == {11742}.Y;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000082D9 File Offset: 0x000064D9
		public static bool operator !=(Index2D {11743}, Index2D {11744})
		{
			return {11743}.X != {11744}.X || {11743}.Y != {11744}.Y;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000082FC File Offset: 0x000064FC
		public Index2D Clamp(in Index2D {11745}, in Index2D {11746})
		{
			Index2D result;
			result.X = Math.Min(Math.Max(this.X, {11745}.X), {11746}.X);
			result.Y = Math.Min(Math.Max(this.Y, {11745}.Y), {11746}.Y);
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008350 File Offset: 0x00006550
		public bool Equals(Index2D {11747})
		{
			return {11747}.X == this.X && {11747}.Y == this.Y;
		}

		// Token: 0x040000BB RID: 187
		public int X;

		// Token: 0x040000BC RID: 188
		public int Y;
	}
}
