using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x02000199 RID: 409
	public class ExpandoTextureLinePath
	{
		// Token: 0x06000A6C RID: 2668 RVA: 0x00002EF6 File Offset: 0x000010F6
		private ExpandoTextureLinePath()
		{
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000342F6 File Offset: 0x000324F6
		public static ExpandoTextureLinePath CreateLine(Rectangle {15848}, Rectangle {15849})
		{
			return new ExpandoTextureLinePath
			{
				{15852} = {15848}.Location,
				{15853} = new Point({15849}.X + {15849}.Width, {15849}.Y),
				{15854} = {15848}.Height
			};
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00034334 File Offset: 0x00032534
		public void Render(Marker {15850}, Color {15851})
		{
			Vector2 vector = {15850}.WH * 0.5f;
			Point point = new Point((int)vector.X, (int)vector.Y);
			Rectangle rectangle = new Rectangle(this.{15852}.X, this.{15852}.Y, point.X, (int){15850}.WH.Y);
			Rectangle rectangle2 = new Rectangle(this.{15853}.X - point.X, this.{15853}.Y, point.X, (int){15850}.WH.Y);
			Vector2 xy = {15850}.XY;
			Vector2 vector2 = new Vector2({15850}.XY.X + vector.X, {15850}.XY.Y);
			Engine.GS.Draw(rectangle, xy, {15851});
			Engine.GS.Draw(rectangle2, vector2, {15851});
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00034418 File Offset: 0x00032618
		public float Height
		{
			get
			{
				return (float)this.{15854};
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00034421 File Offset: 0x00032621
		public Point Start
		{
			get
			{
				return this.{15852};
			}
		}

		// Token: 0x040007F7 RID: 2039
		private Point {15852};

		// Token: 0x040007F8 RID: 2040
		private Point {15853};

		// Token: 0x040007F9 RID: 2041
		private int {15854};
	}
}
