using System;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000280 RID: 640
	internal class {20413}
	{
		// Token: 0x06000E1A RID: 3610 RVA: 0x00076A68 File Offset: 0x00074C68
		public static Vector2 WorldPosToRenderInternal(in Vector2 {20414}, out int {20415}, in Marker {20416}, bool {20417}, float {20418} = 0.5f)
		{
			Vector2 vector;
			vector.X = 1f - (Global.Player.MapInfo.MapSize.X / 2f + {20414}.X) / Global.Player.MapInfo.MapSize.X;
			vector.Y = (Global.Player.MapInfo.MapSize.Y / 2f + {20414}.Y) / Global.Player.MapInfo.MapSize.Y;
			{20415} = 0;
			if (vector.X < -{20418} || vector.X > 1f + {20418} || vector.Y < -{20418} || vector.Y > 1f + {20418})
			{
				{20415} = 2;
			}
			if ({20417})
			{
				Vector2 vector2 = vector - new Vector2(0.5f);
				float num = Math.Max(Math.Abs(vector2.X), Math.Abs(vector2.Y));
				float num2 = 1f + Math.Max(0f, num - 0.5f);
				vector.X = Geometry.Saturate(vector2.X / num2 + 0.5f);
				vector.Y = Geometry.Saturate(vector2.Y / num2 + 0.5f);
			}
			if ({20415} != 2 && (vector.X == 0f || vector.Y == 0f || vector.X == 1f || vector.Y == 1f))
			{
				{20415} = 1;
			}
			Vector2 result;
			result.Y = {20416}.XY.Y + vector.X * ({20416}.WH.Y - 1f);
			result.X = {20416}.XY.X + vector.Y * ({20416}.WH.X - 1f);
			return result;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00076C44 File Offset: 0x00074E44
		public static Rectangle SelectArea(Vector2 {20419}, float {20420}, float {20421}, float {20422})
		{
			float num = {20421} / {20422};
			Rectangle rectangle = new Rectangle(118, 112, 3159, 2574);
			Vector2 vector = new Vector2(Gameplay.WorldMapSizeXY.Y, Gameplay.WorldMapSizeXY.X) / {20420};
			int num2;
			Vector2 vector2 = {20413}.WorldPosToRenderInternal({20419}, out num2, {20413}.mapMarker, true, 0.5f);
			vector2.X -= 0.5f / vector.X;
			vector2.Y -= 0.5f / vector.Y * num;
			return new Marker((float)rectangle.X + vector2.X * (float)rectangle.Width, (float)rectangle.Y + vector2.Y * (float)rectangle.Height, (float)rectangle.Width / vector.X, (float)rectangle.Height / vector.Y * num).ToRect();
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00076D28 File Offset: 0x00074F28
		public static bool IsPortInRenderZone(Vector2 {20423}, Vector2 {20424}, float {20425}, float {20426}, float {20427})
		{
			float num = {20426} / {20427};
			Rectangle rectangle = new Rectangle(118, 112, 3159, 2574);
			Vector2 vector = new Vector2(Gameplay.WorldMapSizeXY.Y, Gameplay.WorldMapSizeXY.X) / {20425};
			int num2;
			Vector2 vector2 = {20413}.WorldPosToRenderInternal({20424}, out num2, {20413}.mapMarker, true, 0.5f);
			Vector2 vector3 = {20413}.WorldPosToRenderInternal({20423}, out num2, {20413}.mapMarker, true, 0.5f);
			vector2.X -= 0.5f / vector.X;
			vector2.Y -= 0.5f / vector.Y * num;
			Vector2 vector4 = new Vector2((float)rectangle.X + vector3.X * (float)rectangle.Width, (float)rectangle.Y + vector3.Y * (float)rectangle.Height);
			Vector2 vector5 = new Vector2((float)rectangle.X + vector2.X * (float)rectangle.Width, (float)rectangle.Y + vector2.Y * (float)rectangle.Height);
			Vector2 vector6 = new Vector2((float)rectangle.Width / vector.X, (float)rectangle.Height / vector.Y * num);
			return vector4.X >= vector5.X && vector4.X <= vector5.X + vector6.X && vector4.Y >= vector5.Y && vector4.Y <= vector5.Y + vector6.Y;
		}

		// Token: 0x04000D37 RID: 3383
		private static Marker mapMarker = new Marker(0f, 0f, 2f, 2f);
	}
}
