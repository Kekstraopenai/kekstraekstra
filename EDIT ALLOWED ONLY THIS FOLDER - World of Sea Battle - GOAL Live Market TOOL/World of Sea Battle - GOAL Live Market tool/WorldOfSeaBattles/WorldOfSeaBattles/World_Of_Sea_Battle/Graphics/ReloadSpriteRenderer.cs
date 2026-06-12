using System;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Grphics.Device;

namespace World_Of_Sea_Battle.Graphics
{
	// Token: 0x02000448 RID: 1096
	internal class ReloadSpriteRenderer
	{
		// Token: 0x060017A5 RID: 6053 RVA: 0x000CB5F8 File Offset: 0x000C97F8
		public static void Render(float {23391}, Vector2 {23392}, Color {23393})
		{
			Device gs = Engine.GS;
			{23392}.X -= 0.5f;
			{23392}.Y -= 0.5f;
			Device device = gs;
			Vector2 vector = {23392} - ReloadSpriteRenderer.origin;
			device.Draw(ReloadSpriteRenderer.back, vector);
			float num = 0f;
			while ({23391} > 0f)
			{
				ReloadSpriteRenderer.DrawFrame(gs, {23393}, num, Math.Min({23391} * 4f, 1f), {23392});
				num += 1.5707964f;
				{23391} -= 0.25f;
			}
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x000CB680 File Offset: 0x000C9880
		private static void DrawFrame(Device {23394}, Color {23395}, float {23396}, float {23397}, Vector2 {23398})
		{
			Rectangle rectangle = ReloadSpriteRenderer.start;
			rectangle.X += rectangle.Width * (int)Math.Floor((double)({23397} * 12f));
			{23394}.Draw(rectangle, {23398}, ReloadSpriteRenderer.origin, {23396}, 1f, {23395});
		}

		// Token: 0x0400160C RID: 5644
		private static readonly Rectangle back = new Rectangle(790, 202, 33, 33);

		// Token: 0x0400160D RID: 5645
		private static readonly Rectangle start = new Rectangle(824, 202, 33, 33);

		// Token: 0x0400160E RID: 5646
		private static readonly Vector2 origin = new Vector2(16.5f, 16.5f);
	}
}
