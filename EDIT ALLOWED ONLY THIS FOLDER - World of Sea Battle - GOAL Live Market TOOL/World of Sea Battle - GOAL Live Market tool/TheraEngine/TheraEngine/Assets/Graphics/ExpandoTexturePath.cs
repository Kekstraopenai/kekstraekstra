using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x0200019A RID: 410
	public class ExpandoTexturePath
	{
		// Token: 0x06000A71 RID: 2673 RVA: 0x0003442C File Offset: 0x0003262C
		public static ExpandoTexturePath CreateBox(Rectangle {15855}, Rectangle {15856}, Rectangle {15857}, Rectangle {15858})
		{
			return new ExpandoTexturePath
			{
				{15862} = {15855}.Location,
				{15863} = new Point({15856}.X, {15856}.Y + {15856}.Height),
				{15864} = new Point({15857}.X + {15857}.Width, {15857}.Y),
				{15865} = new Point({15858}.X + {15858}.Width, {15858}.Y + {15858}.Height)
			};
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x000344AC File Offset: 0x000326AC
		public void Render(Marker {15859}, Color {15860}, Texture2D {15861} = null)
		{
			Vector2 vector = {15859}.WH * 0.5f;
			Point point = new Point((int)vector.X, (int)vector.Y);
			Rectangle rectangle = new Rectangle(this.{15862}.X, this.{15862}.Y, point.X, point.Y);
			Rectangle rectangle2 = new Rectangle(this.{15863}.X, this.{15863}.Y - point.Y, point.X, point.Y);
			Rectangle rectangle3 = new Rectangle(this.{15864}.X - point.X, this.{15864}.Y, point.X, point.Y);
			Rectangle rectangle4 = new Rectangle(this.{15865}.X - point.X, this.{15865}.Y - point.Y, point.X, point.Y);
			Vector2 xy = {15859}.XY;
			Vector2 vector2 = new Vector2({15859}.XY.X, {15859}.XY.Y + vector.Y);
			Vector2 vector3 = new Vector2({15859}.XY.X + vector.X, {15859}.XY.Y);
			Vector2 vector4 = new Vector2({15859}.XY.X + vector.X, {15859}.XY.Y + vector.Y);
			if ({15861} == null)
			{
				Engine.GS.Draw(rectangle, xy, {15860});
				Engine.GS.Draw(rectangle2, vector2, {15860});
				Engine.GS.Draw(rectangle3, vector3, {15860});
				Engine.GS.Draw(rectangle4, vector4, {15860});
				return;
			}
			int num = (int)({15859}.WH.Y - (float)((this.{15862}.Y + this.{15865}.Y) * 2));
			num += (int)((float){15861}.Height / 3.5f);
			if (num > 0)
			{
				int num2 = num / 2;
				rectangle.Height -= num2;
				rectangle3.Height -= num2;
				rectangle2.Y += num2;
				rectangle2.Height -= num2;
				vector2.Y += (float)num2;
				rectangle4.Y += num2;
				rectangle4.Height -= num2;
				vector4.Y += (float)num2;
				Rectangle rectangle5 = new Rectangle(this.{15862}.X, {15861}.Height / 2 - num2, point.X, num2 * 2);
				Rectangle rectangle6 = new Rectangle(this.{15864}.X - point.X, {15861}.Height / 2 - num2, point.X, num2 * 2);
				Vector2 vector5 = new Vector2({15859}.XY.X, {15859}.XY.Y + vector.Y - (float)num2);
				Vector2 vector6 = new Vector2({15859}.XY.X + vector.X, {15859}.XY.Y + vector.Y - (float)num2);
				Engine.GS.DrawCustomTexture({15861}, rectangle5, vector5, {15860});
				Engine.GS.DrawCustomTexture({15861}, rectangle6, vector6, {15860});
			}
			Engine.GS.DrawCustomTexture({15861}, rectangle, xy, {15860});
			Engine.GS.DrawCustomTexture({15861}, rectangle2, vector2, {15860});
			Engine.GS.DrawCustomTexture({15861}, rectangle3, vector3, {15860});
			Engine.GS.DrawCustomTexture({15861}, rectangle4, vector4, {15860});
		}

		// Token: 0x040007FA RID: 2042
		private Point {15862};

		// Token: 0x040007FB RID: 2043
		private Point {15863};

		// Token: 0x040007FC RID: 2044
		private Point {15864};

		// Token: 0x040007FD RID: 2045
		private Point {15865};
	}
}
