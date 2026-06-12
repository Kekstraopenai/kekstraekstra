using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Grphics.Device;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x02000499 RID: 1177
	internal class ShipServerPositionDisplay
	{
		// Token: 0x060019C8 RID: 6600 RVA: 0x000E4F40 File Offset: 0x000E3140
		private static bool IntersectWithSphere(float {24312}, float {24313}, float {24314}, float {24315}, float {24316}, float {24317}, float {24318})
		{
			{24312} -= {24316};
			{24313} -= {24317};
			{24314} -= {24316};
			{24315} -= {24317};
			float num = {24314} - {24312};
			float num2 = {24315} - {24313};
			float num3 = num * num + num2 * num2;
			float num4 = 2f * ({24312} * num + {24313} * num2);
			float num5 = {24312} * {24312} + {24313} * {24313} - {24318} * {24318};
			if (-num4 < 0f)
			{
				return num5 < 0f;
			}
			if (-num4 < 2f * num3)
			{
				return 4f * num3 * num5 - num4 * num4 < 0f;
			}
			return num3 + num4 + num5 < 0f;
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x000E4FD4 File Offset: 0x000E31D4
		private static bool Tester(Vector2 {24319}, Vector2 {24320})
		{
			foreach (IsleInstance isleInstance in ((IEnumerable<IsleInstance>)Global.Player.MapInfo.GetContainedIsles({24319})))
			{
				foreach (FenceSphere fenceSphere in ((IEnumerable<FenceSphere>)isleInstance.TransformedFenceSpheres))
				{
					if (ShipServerPositionDisplay.IntersectWithSphere({24319}.X, {24319}.Y, {24320}.X, {24320}.Y, fenceSphere.C.X, fenceSphere.C.Y, fenceSphere.R))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x000E509C File Offset: 0x000E329C
		public void Display(Vector3 {24321}, Vector3 {24322}, Vector3 {24323})
		{
			if (!this.Enabled)
			{
				return;
			}
			ShipServerPositionDisplay.RenderDot_VisibleOnly({24321}, Color.Lime, 8);
			ShipServerPositionDisplay.RenderDot_VisibleOnly({24322}, Color.Yellow, 8);
			ShipServerPositionDisplay.RenderDot_VisibleOnly({24323}, Color.Blue, 8);
			ShipServerPositionDisplay.RenderDot_VisibleOnly(this.Position, Color.Red, 8);
			for (int i = 0; i < this.{24334}.Length; i++)
			{
				ShipServerPositionDisplay.RenderDot(this.{24334}[i], Color.Red, 16);
			}
			for (int j = 0; j < this.{24335}.Length; j++)
			{
				ShipServerPositionDisplay.<>c__DisplayClass7_0 CS$<>8__locals1;
				CS$<>8__locals1.npc = this.{24335}[j];
				if (Engine.GS.Camera.IsVisible(CS$<>8__locals1.npc.Pos, 1f))
				{
					ShipServerPositionDisplay.RenderDot(CS$<>8__locals1.npc.Pos, Color.Violet, 16);
					ShipServerPositionDisplay.RenderDot(ShipServerPositionDisplay.<Display>g__PosWithOffset|7_0(6f, 0f, ref CS$<>8__locals1), CS$<>8__locals1.npc.CanForward ? Color.LimeGreen : Color.Red, 4);
					ShipServerPositionDisplay.RenderDot(ShipServerPositionDisplay.<Display>g__PosWithOffset|7_0(3f, 0f, ref CS$<>8__locals1), CS$<>8__locals1.npc.CanForwardNear ? Color.LimeGreen : Color.Red, 4);
					ShipServerPositionDisplay.RenderDot(ShipServerPositionDisplay.<Display>g__PosWithOffset|7_0(3f, 45f, ref CS$<>8__locals1), CS$<>8__locals1.npc.CanRight ? Color.LimeGreen : Color.Red, 4);
					ShipServerPositionDisplay.RenderDot(ShipServerPositionDisplay.<Display>g__PosWithOffset|7_0(3f, -45f, ref CS$<>8__locals1), CS$<>8__locals1.npc.CanLeft ? Color.LimeGreen : Color.Red, 4);
					if (CS$<>8__locals1.npc.IsManeuver)
					{
						for (int k = 0; k < 10; k++)
						{
							float scaleFactor = 1f + (float)k * 0.5f;
							float maneuverAngle = CS$<>8__locals1.npc.ManeuverAngle;
							Vector3 vector = CS$<>8__locals1.npc.Pos + scaleFactor * new Vector3(MathF.Cos(maneuverAngle), 0f, MathF.Sin(maneuverAngle));
							Vector2 projectionSmoothed = Engine.GS.Camera.GetProjectionSmoothed(ref vector);
							Rectangle rectangle = new Rectangle
							{
								X = (int)projectionSmoothed.X - 6,
								Y = (int)projectionSmoothed.Y - 6,
								Width = 3,
								Height = 3
							};
							Device gs = Engine.GS;
							Color yellow = Color.Yellow;
							gs.Draw(AtlasObjs.whitepixel_1px, rectangle, yellow);
						}
					}
				}
			}
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x000E5318 File Offset: 0x000E3518
		private static void RenderDot(Vector3 {24324}, Color {24325}, int {24326} = 16)
		{
			Vector2 projectionSmoothed = Engine.GS.Camera.GetProjectionSmoothed(ref {24324});
			Rectangle rectangle = new Rectangle
			{
				X = (int)projectionSmoothed.X - {24326} / 2,
				Y = (int)projectionSmoothed.Y - {24326} / 2,
				Width = {24326},
				Height = {24326}
			};
			Engine.GS.Draw(AtlasObjs.whitepixel_1px, rectangle, {24325});
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000E5388 File Offset: 0x000E3588
		private static void RenderDot_VisibleOnly(Vector3 {24327}, Color {24328}, int {24329} = 16)
		{
			if (Engine.GS.Camera.IsVisible({24327}, 1f))
			{
				ShipServerPositionDisplay.RenderDot({24327}, {24328}, {24329});
			}
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x000E53AA File Offset: 0x000E35AA
		public void Set(OnDebugPosDisplay {24330})
		{
			this.{24334} = {24330}.CannonBalls;
			this.Position = {24330}.Pos;
			this.{24335} = {24330}.Npcs;
			this.Enabled = true;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x000E53EC File Offset: 0x000E35EC
		[CompilerGenerated]
		internal static Vector3 <Display>g__PosWithOffset|7_0(float {24331}, float {24332}, ref ShipServerPositionDisplay.<>c__DisplayClass7_0 {24333})
		{
			return {24333}.npc.Pos + {24331} * new Vector3(MathF.Cos({24333}.npc.Rotation + {24332}), 0f, MathF.Sin({24333}.npc.Rotation + {24332}));
		}

		// Token: 0x0400181E RID: 6174
		public bool Enabled;

		// Token: 0x0400181F RID: 6175
		public Vector3 Position;

		// Token: 0x04001820 RID: 6176
		private Vector3[] {24334};

		// Token: 0x04001821 RID: 6177
		private OnDebugPosDisplay.NpcData[] {24335};

		// Token: 0x04001822 RID: 6178
		private PathFinder {24336} = new PathFinder();
	}
}
