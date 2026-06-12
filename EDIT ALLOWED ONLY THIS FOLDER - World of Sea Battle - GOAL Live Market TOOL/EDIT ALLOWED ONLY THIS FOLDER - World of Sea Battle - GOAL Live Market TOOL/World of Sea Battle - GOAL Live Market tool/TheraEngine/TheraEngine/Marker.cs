using System;
using Microsoft.Xna.Framework;

namespace TheraEngine
{
	// Token: 0x02000024 RID: 36
	public struct Marker
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004840 File Offset: 0x00002A40
		public Vector2 Center
		{
			get
			{
				Marker.v2.X = this.XY.X + this.WH.X / 2f;
				Marker.v2.Y = this.XY.Y + this.WH.Y / 2f;
				return Marker.v2;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000048A0 File Offset: 0x00002AA0
		public Vector2 End
		{
			get
			{
				return this.XY + this.WH;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000048B3 File Offset: 0x00002AB3
		public Vector2 HalfSize
		{
			get
			{
				return this.WH * 0.5f;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000048C5 File Offset: 0x00002AC5
		public static Marker Zero
		{
			get
			{
				return new Marker(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000048E0 File Offset: 0x00002AE0
		public static Marker OneUnit
		{
			get
			{
				return new Marker(0f, 0f, 1f, 1f);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000048FB File Offset: 0x00002AFB
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004908 File Offset: 0x00002B08
		public float Width
		{
			get
			{
				return this.WH.X;
			}
			set
			{
				this.WH.X = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004916 File Offset: 0x00002B16
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004923 File Offset: 0x00002B23
		public float Height
		{
			get
			{
				return this.WH.Y;
			}
			set
			{
				this.WH.Y = value;
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004931 File Offset: 0x00002B31
		public Marker(float {11520})
		{
			this = new Marker(0f, 0f, {11520}, {11520});
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004948 File Offset: 0x00002B48
		public Marker(in Vector2 {11521}, in Vector2 {11522})
		{
			this.XY.X = {11521}.X;
			this.XY.Y = {11521}.Y;
			this.WH.X = {11522}.X;
			this.WH.Y = {11522}.Y;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004999 File Offset: 0x00002B99
		public Marker(in Vector2 {11523}, float {11524}, float {11525})
		{
			this.XY.X = {11523}.X;
			this.XY.Y = {11523}.Y;
			this.WH.X = {11524};
			this.WH.Y = {11525};
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000049D5 File Offset: 0x00002BD5
		public Marker(float {11526}, float {11527}, float {11528}, float {11529})
		{
			this.XY.X = {11526};
			this.XY.Y = {11527};
			this.WH.X = {11528};
			this.WH.Y = {11529};
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004A08 File Offset: 0x00002C08
		public Marker(in Vector2 {11530}, in Rectangle {11531})
		{
			this.XY.X = {11530}.X;
			this.XY.Y = {11530}.Y;
			this.WH.X = (float){11531}.Width;
			this.WH.Y = (float){11531}.Height;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004A5B File Offset: 0x00002C5B
		public Marker(float {11532}, float {11533}, in Rectangle {11534})
		{
			this.XY.X = {11532};
			this.XY.Y = {11533};
			this.WH.X = (float){11534}.Width;
			this.WH.Y = (float){11534}.Height;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004A99 File Offset: 0x00002C99
		public Marker(float {11535}, float {11536}, in Vector2 {11537})
		{
			this.XY.X = {11535};
			this.XY.Y = {11536};
			this.WH.X = {11537}.X;
			this.WH.Y = {11537}.Y;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public Marker(in Marker {11538}, in Vector2 {11539}, in Vector2 {11540})
		{
			this.XY.X = {11538}.XY.X + {11539}.X;
			this.XY.Y = {11538}.XY.Y + {11539}.Y;
			this.WH.X = {11540}.X;
			this.WH.Y = {11540}.Y;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004B44 File Offset: 0x00002D44
		public Marker(in Marker {11541}, float {11542}, float {11543}, float {11544}, float {11545})
		{
			this.XY.X = {11541}.XY.X + {11542};
			this.XY.Y = {11541}.XY.Y + {11543};
			this.WH.X = {11544};
			this.WH.Y = {11545};
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004B9C File Offset: 0x00002D9C
		public Marker(in Rectangle {11546})
		{
			this.XY.X = (float){11546}.X;
			this.XY.Y = (float){11546}.Y;
			this.WH.X = (float){11546}.Width;
			this.WH.Y = (float){11546}.Height;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public Rectangle ToRect()
		{
			Marker.rectRet.X = (int)this.XY.X;
			Marker.rectRet.Y = (int)this.XY.Y;
			Marker.rectRet.Width = (int)this.WH.X;
			Marker.rectRet.Height = (int)this.WH.Y;
			return Marker.rectRet;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004C60 File Offset: 0x00002E60
		public Rectangle ToRectRound()
		{
			Marker.rectRet.X = (int)Math.Round((double)this.XY.X);
			Marker.rectRet.Y = (int)Math.Round((double)this.XY.Y);
			Marker.rectRet.Width = (int)Math.Round((double)this.WH.X);
			Marker.rectRet.Height = (int)Math.Round((double)this.WH.Y);
			return Marker.rectRet;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004CE4 File Offset: 0x00002EE4
		public Marker Offset(in Vector2 {11547})
		{
			return new Marker(this.XY.X + {11547}.X, this.XY.Y + {11547}.Y, this.WH.X, this.WH.Y);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004D30 File Offset: 0x00002F30
		public Marker Offset(in Marker {11548})
		{
			return new Marker(this.XY.X + {11548}.XY.X, this.XY.Y + {11548}.XY.Y, this.WH.X, this.WH.Y);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004D86 File Offset: 0x00002F86
		public Marker Offset(float {11549}, float {11550})
		{
			return new Marker(this.XY.X + {11549}, this.XY.Y + {11550}, this.WH.X, this.WH.Y);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004DBD File Offset: 0x00002FBD
		public Marker Resize(in Vector2 {11551})
		{
			return new Marker(this.XY.X, this.XY.Y, {11551}.X, {11551}.Y);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004DE6 File Offset: 0x00002FE6
		public Marker Resize(float {11552}, float {11553})
		{
			return new Marker(this.XY.X, this.XY.Y, this.WH.X + {11552}, this.WH.Y + {11553});
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004E1D File Offset: 0x0000301D
		public Marker GenerateTranslation(in Vector2 {11554}, in Vector2 {11555})
		{
			return new Marker(this.XY.X + {11554}.X, this.XY.Y + {11554}.Y, {11555}.X, {11555}.Y);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004E54 File Offset: 0x00003054
		public Marker GenerateTranslation(in Vector2 {11556}, in Rectangle {11557})
		{
			return new Marker(this.XY.X + {11556}.X, this.XY.Y + {11556}.Y, (float){11557}.Width, (float){11557}.Height);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004E8D File Offset: 0x0000308D
		public Marker GenerateTranslation(float {11558}, float {11559}, float {11560}, float {11561})
		{
			return new Marker(this.XY.X + {11558}, this.XY.Y + {11559}, {11560}, {11561});
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004EB1 File Offset: 0x000030B1
		public Marker SetX(float {11562})
		{
			return new Marker({11562}, this.XY.Y, this.WH.X, this.WH.Y);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004EDA File Offset: 0x000030DA
		public Marker SetY(float {11563})
		{
			return new Marker(this.XY.X, {11563}, this.WH.X, this.WH.Y);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004F03 File Offset: 0x00003103
		public Marker SetXY(in Vector2 {11564})
		{
			return new Marker({11564}.X, {11564}.Y, this.WH.X, this.WH.Y);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004F2C File Offset: 0x0000312C
		public Marker SetXY(float {11565}, float {11566})
		{
			return new Marker({11565}, {11566}, this.WH.X, this.WH.Y);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004F4B File Offset: 0x0000314B
		[Obsolete("Use UiControl.PosWidth")]
		public Marker SetWidth(float {11567})
		{
			return new Marker(this.XY.X, this.XY.Y, {11567}, this.WH.Y);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004F74 File Offset: 0x00003174
		[Obsolete("Use UiControl.PosHeight")]
		public Marker SetHeight(float {11568})
		{
			return new Marker(this.XY.X, this.XY.Y, this.WH.X, {11568});
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004F4B File Offset: 0x0000314B
		public Marker WithWidth(float {11569})
		{
			return new Marker(this.XY.X, this.XY.Y, {11569}, this.WH.Y);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004F74 File Offset: 0x00003174
		public Marker WithHeight(float {11570})
		{
			return new Marker(this.XY.X, this.XY.Y, this.WH.X, {11570});
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004FA0 File Offset: 0x000031A0
		public static Marker operator +(in Marker {11571}, in Vector2 {11572})
		{
			return new Marker({11571}.XY.X + {11572}.X, {11571}.XY.Y + {11572}.Y, {11571}.WH.X, {11571}.WH.Y);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004FEC File Offset: 0x000031EC
		public static Marker operator -(in Marker {11573}, in Vector2 {11574})
		{
			return new Marker({11573}.XY.X - {11574}.X, {11573}.XY.Y - {11574}.Y, {11573}.WH.X, {11573}.WH.Y);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005038 File Offset: 0x00003238
		public static bool operator ==(Marker {11575}, Marker {11576})
		{
			return {11575}.XY.X == {11576}.XY.X && {11575}.XY.Y == {11576}.XY.Y && {11575}.WH.X == {11576}.WH.X && {11575}.WH.Y == {11576}.WH.Y;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000050A8 File Offset: 0x000032A8
		public static bool operator !=(Marker {11577}, Marker {11578})
		{
			return {11577}.XY.X != {11578}.XY.X || {11577}.XY.Y != {11578}.XY.Y || {11577}.WH.X != {11578}.WH.X || {11577}.WH.Y != {11578}.WH.Y;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005118 File Offset: 0x00003318
		public override bool Equals(object {11579})
		{
			if ({11579}.GetType() != base.GetType())
			{
				return false;
			}
			Marker marker = (Marker){11579};
			return marker.XY.X == this.XY.X && marker.XY.Y == this.XY.Y && marker.WH.X == this.WH.X && marker.WH.Y == this.WH.Y;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000051AE File Offset: 0x000033AE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000051C0 File Offset: 0x000033C0
		public static void Intersection(ref Marker {11580}, ref Marker {11581}, out Marker {11582})
		{
			float num = Math.Max({11580}.XY.X, {11581}.XY.X);
			float num2 = Math.Min({11580}.XY.X + {11580}.WH.X, {11581}.XY.X + {11581}.WH.X);
			float num3 = Math.Max({11580}.XY.Y, {11581}.XY.Y);
			float num4 = Math.Min({11580}.XY.Y + {11580}.WH.Y, {11581}.XY.Y + {11581}.WH.Y);
			if (num2 >= num && num4 >= num3)
			{
				{11582} = new Marker(num, num3, num2 - num, num4 - num3);
				return;
			}
			{11582} = default(Marker);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005290 File Offset: 0x00003490
		public static void Union(ref Marker {11583}, ref Marker {11584}, out Marker {11585})
		{
			float num = Math.Min({11583}.XY.X, {11584}.XY.X);
			float num2 = Math.Max({11583}.XY.X + {11583}.WH.X, {11584}.XY.X + {11584}.WH.X);
			float num3 = Math.Min({11583}.XY.Y, {11584}.XY.Y);
			float num4 = Math.Max({11583}.XY.Y + {11583}.WH.Y, {11584}.XY.Y + {11584}.WH.Y);
			{11585} = new Marker(num, num3, num2 - num, num4 - num3);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005350 File Offset: 0x00003550
		public bool Collision(in Vector2 {11586})
		{
			return {11586}.X > this.XY.X && {11586}.Y > this.XY.Y && {11586}.X < this.XY.X + this.WH.X && {11586}.Y < this.XY.Y + this.WH.Y;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000053C4 File Offset: 0x000035C4
		public void Collision(ref Vector2 {11587}, out bool {11588})
		{
			{11588} = ({11587}.X > this.XY.X && {11587}.Y > this.XY.Y && {11587}.X < this.XY.X + this.WH.X && {11587}.Y < this.XY.Y + this.WH.Y);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000543A File Offset: 0x0000363A
		public static Vector2 Helper_GetVelocity(ref Marker {11589}, ref Marker {11590})
		{
			return new Vector2({11589}.XY.X - {11590}.XY.X, {11589}.XY.Y - {11590}.XY.Y);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000546F File Offset: 0x0000366F
		public static Marker FromCentrScreen(Vector2 {11591}, Rectangle {11592})
		{
			return new Marker((float)((int)({11591}.X / 2f - (float)({11592}.Width / 2))), (float)((int)({11591}.Y / 2f - (float)({11592}.Height / 2))), ref {11592});
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000054A8 File Offset: 0x000036A8
		public static Marker FromCentrScreen(Marker {11593}, Rectangle {11594})
		{
			Vector2 vector = {11593}.WH / 2f;
			vector.X = (float)((int)vector.X);
			vector.Y = (float)((int)vector.Y);
			Vector2 vector2 = {11593}.XY + vector - new Vector2((float)({11594}.Width / 2), (float)({11594}.Height / 2));
			return new Marker(ref vector2, ref {11594});
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005516 File Offset: 0x00003716
		public static Marker FromCentrScreen(Vector2 {11595}, Vector2 {11596})
		{
			return new Marker((float)((int)({11595}.X / 2f - {11596}.X / 2f)), (float)((int)({11595}.Y / 2f - {11596}.Y / 2f)), ref {11596});
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005558 File Offset: 0x00003758
		public Marker ScaleWidth(float {11597})
		{
			Marker result;
			result.XY = this.XY;
			result.WH = this.WH;
			result.WH.X = result.WH.X * {11597};
			return result;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005594 File Offset: 0x00003794
		public Marker ScaleSize(float {11598})
		{
			Marker result;
			result.XY = this.XY;
			result.WH = this.WH * {11598};
			return result;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000055C4 File Offset: 0x000037C4
		public Marker Scale(float {11599})
		{
			Marker result;
			result.XY = this.XY * {11599};
			result.WH = this.WH * {11599};
			return result;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000055F8 File Offset: 0x000037F8
		public Marker ScaleOfCenter(float {11600})
		{
			Marker result;
			result.XY = this.XY - this.WH * ({11600} - 1f) * 0.5f;
			result.WH = this.WH * {11600};
			return result;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005648 File Offset: 0x00003848
		public Marker Border(float {11601})
		{
			Vector2 vector = this.XY - new Vector2({11601});
			Vector2 vector2 = this.WH + new Vector2({11601} * 2f);
			return new Marker(ref vector, ref vector2);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005688 File Offset: 0x00003888
		public Marker Border(float {11602}, float {11603})
		{
			Vector2 vector = this.XY - new Vector2({11602}, {11603});
			Vector2 vector2 = this.WH + new Vector2({11602} * 2f, {11603} * 2f);
			return new Marker(ref vector, ref vector2);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000056D0 File Offset: 0x000038D0
		public Marker PlaceLeft(Marker {11604})
		{
			return new Marker(this.XY.X - {11604}.WH.X, this.Center.Y - {11604}.WH.Y / 2f, {11604}.WH.X, {11604}.WH.Y);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000572C File Offset: 0x0000392C
		public override string ToString()
		{
			return this.ToRect().ToString();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005750 File Offset: 0x00003950
		public float Distance(Vector2 {11605})
		{
			Rectangle rectangle = this.ToRect();
			float num = Math.Clamp({11605}.X, (float)rectangle.Left, (float)rectangle.Right);
			float num2 = Math.Clamp({11605}.Y, (float)rectangle.Top, (float)rectangle.Bottom);
			float num3 = {11605}.X - num;
			float num4 = {11605}.Y - num2;
			return MathF.Sqrt(num3 * num3 + num4 * num4);
		}

		// Token: 0x040000AE RID: 174
		private static Vector2 v2;

		// Token: 0x040000AF RID: 175
		private static Rectangle rectRet;

		// Token: 0x040000B0 RID: 176
		public Vector2 XY;

		// Token: 0x040000B1 RID: 177
		public Vector2 WH;
	}
}
