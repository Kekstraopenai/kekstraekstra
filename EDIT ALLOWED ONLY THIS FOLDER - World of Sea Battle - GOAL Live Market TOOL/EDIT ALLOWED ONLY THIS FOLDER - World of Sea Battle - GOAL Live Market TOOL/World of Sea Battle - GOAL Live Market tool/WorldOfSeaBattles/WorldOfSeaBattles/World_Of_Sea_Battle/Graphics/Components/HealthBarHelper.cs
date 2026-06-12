using System;
using System.Runtime.CompilerServices;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x0200048F RID: 1167
	internal sealed class HealthBarHelper
	{
		// Token: 0x06001992 RID: 6546 RVA: 0x00003100 File Offset: 0x00001300
		public static void Draw(bool {24221}, Vector3 {24222}, bool {24223})
		{
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x000E28C4 File Offset: 0x000E0AC4
		public static void DrawForShip(Vector2 {24224}, HealthBarStyle {24225}, float {24226}, float {24227}, float {24228}, Vector2 {24229})
		{
			{24224}.Y += 2f;
			HealthBarHelper.Draw(false, true, {24224}, {24225}, {24226}, {24229}, {24227}, new Vector2(1f), {24228});
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000E28FC File Offset: 0x000E0AFC
		public static void Draw(bool {24230}, bool {24231}, Vector2 {24232}, HealthBarStyle {24233}, float {24234}, Vector2 {24235}, float {24236}, Vector2 {24237}, float {24238} = 0f)
		{
			if (Global.Render.UiMode == InterfaceMode.Cinematografic)
			{
				return;
			}
			Color color = Color.White * {24236};
			Rectangle rectangle2;
			Vector2 vector;
			Rectangle rectangle4;
			Rectangle rectangle5;
			if ({24231})
			{
				Rectangle rectangle;
				if ({24233} != HealthBarStyle.Blue)
				{
					if ({24233} != HealthBarStyle.Lime)
					{
						if ({24233} != HealthBarStyle.Red)
						{
							if ({24233} != HealthBarStyle.Gray)
							{
								throw new NotSupportedException();
							}
							rectangle = new Rectangle(119, 454, 98, 15);
						}
						else
						{
							rectangle = new Rectangle(0, 486, 98, 15);
						}
					}
					else
					{
						rectangle = new Rectangle(0, 435, 98, 15);
					}
				}
				else
				{
					rectangle = new Rectangle(0, 452, 98, 15);
				}
				rectangle2 = rectangle;
				vector = new Vector2((float)(rectangle2.Width / 2), (float)(rectangle2.Height / 2));
				Rectangle rectangle3 = new Rectangle(2, 2509, 244, 38);
				rectangle4 = ({24230} ? new Rectangle(0, 386, 98, 15) : new Rectangle(0, 503, 98, 15));
				rectangle5 = ({24230} ? new Rectangle(0, 386, 98, 15) : new Rectangle(0, 469, 98, 15));
				Device gs = Engine.GS;
				Vector2 vector2 = vector * 244f / 98f;
				float {14564} = 0f;
				Vector2 vector3 = {24237} * 98f / 244f * new Vector2(1f, 1.2f);
				gs.Draw(rectangle3, {24232}, vector2, {14564}, vector3, color);
			}
			else
			{
				Rectangle rectangle6;
				if ({24233} != HealthBarStyle.Red)
				{
					if ({24233} != HealthBarStyle.Lime)
					{
						if ({24233} != HealthBarStyle.Gray)
						{
							throw new NotSupportedException();
						}
						rectangle6 = new Rectangle(2104, 211, 90, 21);
					}
					else
					{
						rectangle6 = new Rectangle(2104, 167, 90, 21);
					}
				}
				else
				{
					rectangle6 = new Rectangle(2104, 189, 90, 21);
				}
				rectangle2 = rectangle6;
				vector = new Vector2((float)(rectangle2.Width / 2), (float)(rectangle2.Height / 2));
				Rectangle rectangle3 = new Rectangle(2104, 145, 90, 21);
				rectangle4 = ({24230} ? new Rectangle(2196, 233, 90, 21) : new Rectangle(2196, 211, 90, 21));
				rectangle5 = ({24230} ? new Rectangle(2196, 233, 90, 21) : new Rectangle(2196, 211, 90, 21));
				Engine.GS.Draw(rectangle3, {24232}, vector, 0f, {24237}, color);
			}
			int num = (int)((float)rectangle2.Width * {24234});
			int num2 = (int)((float)rectangle2.Width * {24235}.X);
			int num3 = (int)((float)rectangle2.Width * {24235}.Y);
			Rectangle rectangle7 = rectangle2;
			rectangle7.Width = num;
			Engine.GS.Draw(rectangle7, {24232}, vector, 0f, {24237}, color);
			if (num2 > 0)
			{
				rectangle7 = rectangle4;
				rectangle7.X += num;
				rectangle7.Width = num2;
				Device gs2 = Engine.GS;
				Vector2 vector2 = vector - new Vector2((float)num, 0f);
				gs2.Draw(rectangle7, {24232}, vector2, 0f, {24237}, color);
			}
			if (num3 > 0)
			{
				rectangle7 = rectangle5;
				rectangle7.X += num + num2;
				rectangle7.Width = num3;
				Device gs3 = Engine.GS;
				Vector2 vector2 = vector - new Vector2((float)(num + num2), 0f);
				gs3.Draw(rectangle7, {24232}, vector2, 0f, {24237}, color);
			}
			if ({24238} > 0f && {24231})
			{
				Device gs4 = Engine.GS;
				Rectangle rectangle8 = new Rectangle(0, 401, 98, 15);
				float {14564}2 = 0f;
				Color color2 = Color.White * {24238} * Geometry.Saturate({24236} / 0.5f);
				gs4.Draw(rectangle8, {24232}, vector, {14564}2, {24237}, color2);
			}
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000E2C98 File Offset: 0x000E0E98
		public static void RenderTextDouble(Vector2 {24239}, string {24240}, string {24241}, Color {24242}, Color? {24243})
		{
			Engine.GS.SetFont(Fonts.Arial_12);
			Vector2 vector = Fonts.Arial_12.Measure({24240} + {24241}) * 0.5f;
			Geometry.IntVector(ref vector);
			Vector2 vector2;
			vector2.X = {24239}.X - vector.X;
			vector2.Y = {24239}.Y - vector.Y + 4f;
			if (Global.Render.UiMode == InterfaceMode.Cinematografic)
			{
				Color color = {24243} ?? Color.LightGray;
				color = Color.Lerp(color, Color.White, 0.2f) * MathF.Sqrt((float){24242}.A / 255f);
				Engine.GS.DrawString({24240}, vector2, color);
				if (!string.IsNullOrEmpty({24241}))
				{
					vector2.X += (float)((int)Fonts.Arial_12.Measure({24240}).X + 1);
					Device gs = Engine.GS;
					Color color2 = color * 0.8f;
					gs.DrawString({24241}, vector2, color2);
					return;
				}
			}
			else
			{
				Device gs2 = Engine.GS;
				Color color2 = {24242} * 0.7f;
				gs2.DrawString({24240}, vector2, color2);
				if (!string.IsNullOrEmpty({24241}))
				{
					vector2.X += (float)((int)Fonts.Arial_12.Measure({24240}).X + 1);
					Device gs3 = Engine.GS;
					color2 = {24242} * 0.35f;
					gs3.DrawString({24241}, vector2, color2);
				}
			}
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000E2E10 File Offset: 0x000E1010
		public static HealthBarStyle GetStyle(Relation {24244})
		{
			HealthBarStyle result;
			if ({24244} != Relation.Ally)
			{
				if ({24244} != Relation.Enemy)
				{
					result = HealthBarStyle.Gray;
				}
				else
				{
					result = HealthBarStyle.Red;
				}
			}
			else
			{
				result = HealthBarStyle.Lime;
			}
			return result;
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x000E2E34 File Offset: 0x000E1034
		public static Vector2? RenderTowerStrengthAtlGameGui(HealthBarStyle {24245}, IsleInstance {24246}, float {24247}, float {24248}, bool {24249} = false, float? {24250} = null, string {24251} = "", float {24252} = 0.8f, bool {24253} = false)
		{
			{24247} = MathF.Ceiling({24247});
			WorldObjectID groupID = {24246}.GroupID;
			bool flag = groupID - WorldObjectID.ArenaFort <= 1 || groupID == WorldObjectID.WorldFort;
			Vector2 vector = flag ? {24246}.ModelGlobalBSxz : {24246}.GlobalPosition;
			Vector3 vector2 = new Vector3(vector.X, {24250} ?? {24246}.ModelGlobalBS.Radius, vector.Y);
			float num = Vector3.Distance(vector2, Engine.GS.Camera.Position);
			if (num < 500f && Engine.GS.Camera.IsVisible(vector2, 5f))
			{
				float scale = (num > 150f) ? (1f - (num - 150f) / 350f) : 1f;
				Vector2 projection = Engine.GS.Camera.GetProjection(ref vector2);
				projection.X = (float)((int)projection.X);
				projection.Y = (float)((int)projection.Y);
				if (!{24249})
				{
					float num2 = {24247} / {24248};
					Vector2 vector3 = new Vector2(0.5f + Math.Min(1f, 25f / num)) * {24252};
					if (!string.IsNullOrEmpty({24251}))
					{
						vector3 *= new Vector2(1.9f, 1.4f);
					}
					Rectangle rectangle = ({24245} == HealthBarStyle.Gray) ? HealthBarHelper.p_progress_up_grey : (({24245} == HealthBarStyle.Lime) ? HealthBarHelper.p_progress_up_green : HealthBarHelper.p_progress_up_red);
					Color color = Color.White * scale;
					int num3 = (int)((float)HealthBarHelper.p_progress_back.Width * num2);
					Device gs = Engine.GS;
					Rectangle rectangle2 = new Rectangle(num3 + HealthBarHelper.p_progress_back.X, HealthBarHelper.p_progress_back.Y, HealthBarHelper.p_progress_back.Width - num3, HealthBarHelper.p_progress_back.Height);
					Vector2 vector4 = projection + new Vector2((float)num3, 0f) * vector3;
					gs.Draw(rectangle2, vector4, HealthBarHelper.p_progressCenter, 0f, vector3, color);
					Device gs2 = Engine.GS;
					rectangle2 = new Rectangle(rectangle.X, rectangle.Y, (int)((float)rectangle.Width * num2), rectangle.Height);
					gs2.Draw(rectangle2, projection, HealthBarHelper.p_progressCenter, 0f, vector3, color);
					string text = {24251} + ((!string.IsNullOrEmpty({24251}) && {24253}) ? "" : ((int){24247}).ToString());
					Vector2 value = Fonts.Arial_10.Measure(text);
					Engine.GS.SetFont(Fonts.Arial_12);
					Device gs3 = Engine.GS;
					string {14626} = text;
					Vector2 {14627} = projection - value * vector3 * 0.5f * 1.2f + new Vector2(1f, 1f);
					Color color2 = Color.White * scale;
					gs3.DrawString({14626}, {14627}, color2, 0f, Vector2.Zero, vector3.Y * 1.2f);
				}
				return new Vector2?(projection);
			}
			return null;
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x000E3144 File Offset: 0x000E1344
		public static void RenderWhale(Drop {24254}, float {24255} = 0.8f)
		{
			Vector3 vector = {24254}.Position.X0Y() + new Vector3(0f, 4f, 0f);
			float num = Vector3.Distance(vector, Engine.GS.Camera.Position);
			if (num < 500f && Engine.GS.Camera.IsVisible(vector, 5f))
			{
				float num2 = 1f - Geometry.InverseLerp(70f, 300f, num);
				num2 *= 0.6f;
				Vector2 projection = Engine.GS.Camera.GetProjection(ref vector);
				projection.X = (float)((int)projection.X);
				projection.Y = (float)((int)projection.Y);
				float num3 = {24254}.Whale.Health / Math.Max(1f, {24254}.Whale.MaxHealth);
				Vector2 vector2 = new Vector2(0.5f + Math.Min(1f, 25f / num)) * {24255};
				if ({24254}.Whale.MaxHealth < 10000f)
				{
					vector2.X *= 0.8f;
				}
				vector2.Y *= 1.3f;
				Rectangle rectangle = new Rectangle(0, 453, 98, 13);
				Rectangle rectangle2 = new Rectangle(0, 418, 98, 15);
				Color color = Color.White * num2;
				int num4 = (int)((float)rectangle2.Width * num3);
				Device gs = Engine.GS;
				Rectangle rectangle3 = new Rectangle(num4 + rectangle2.X, rectangle2.Y, rectangle2.Width - num4, rectangle2.Height);
				Vector2 vector3 = projection + new Vector2((float)num4, 0f) * vector2;
				gs.Draw(rectangle3, vector3, HealthBarHelper.p_progressCenter, 0f, vector2, color);
				Device gs2 = Engine.GS;
				rectangle3 = new Rectangle(rectangle.X, rectangle.Y, (int)((float)rectangle.Width * num3), rectangle.Height);
				gs2.Draw(rectangle3, projection, HealthBarHelper.p_progressCenter, 0f, vector2, color);
				float num5 = {24254}.Whale.Health;
				if (num5 <= 1f && {24254}.Whale.State != WhaleController.Status.Stunned)
				{
					num5 = 1f;
				}
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted<float>(num5, "F0");
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted<float>({24254}.Whale.MaxHealth, "F0");
				string text = defaultInterpolatedStringHandler.ToStringAndClear();
				Vector2 value = Fonts.Arial_10.Measure(text);
				Engine.GS.SetFont(Fonts.Arial_12);
				Device gs3 = Engine.GS;
				string {14626} = text;
				Vector2 {14627} = projection - value * vector2 * 0.5f * 1.2f + new Vector2(1f, 1f);
				Color color2 = Color.White * num2;
				gs3.DrawString({14626}, {14627}, color2, 0f, Vector2.Zero, vector2.X * 1.25f);
			}
		}

		// Token: 0x040017F8 RID: 6136
		public static readonly Rectangle p_progress_up_green = new Rectangle(2104, 167, 90, 21);

		// Token: 0x040017F9 RID: 6137
		public static readonly Rectangle p_progress_up_red = new Rectangle(2104, 189, 90, 21);

		// Token: 0x040017FA RID: 6138
		public static readonly Rectangle p_progress_up_grey = new Rectangle(2104, 211, 90, 21);

		// Token: 0x040017FB RID: 6139
		public static readonly Rectangle p_progress_back = new Rectangle(2104, 145, 90, 21);

		// Token: 0x040017FC RID: 6140
		private static readonly Vector2 p_progressCenter = new Vector2((float)(HealthBarHelper.p_progress_back.Width / 2), (float)(HealthBarHelper.p_progress_back.Height / 2));
	}
}
