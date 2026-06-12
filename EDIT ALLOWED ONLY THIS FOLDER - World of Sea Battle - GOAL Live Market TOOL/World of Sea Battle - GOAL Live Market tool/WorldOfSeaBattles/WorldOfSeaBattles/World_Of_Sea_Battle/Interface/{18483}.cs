using System;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000146 RID: 326
	internal static class {18483}
	{
		// Token: 0x06000778 RID: 1912 RVA: 0x0003B5AC File Offset: 0x000397AC
		public static void Render(Vector2 {18484})
		{
			if (Global.Game.GameTime.FpsCounter.Avg < 20f)
			{
				Engine.GS.SetFont(Fonts.F_m14_ThinBold);
				{18484} += new Vector2(0f, 14f);
				string text = "UPD: " + {18483}.cpuTime.ToString();
				Device gs = Engine.GS;
				string {14599} = text;
				Color color = Color.Black;
				gs.DrawString({14599}, {18484}, color);
				Device gs2 = Engine.GS;
				string {14599}2 = text;
				Vector2 vector = {18484} + new Vector2(1f, -1f);
				color = Color.White;
				gs2.DrawString({14599}2, vector, color);
				{18484} += new Vector2(0f, 14f);
				text = "DRW: " + {18483}.gpuTime.ToString();
				Device gs3 = Engine.GS;
				string {14599}3 = text;
				color = Color.Black;
				gs3.DrawString({14599}3, {18484}, color);
				Device gs4 = Engine.GS;
				string {14599}4 = text;
				vector = {18484} + new Vector2(1f, -1f);
				color = Color.White;
				gs4.DrawString({14599}4, vector, color);
				{18484} += new Vector2(0f, 14f);
				text = "TDIFF: " + ((int)Global.Game.GameTime.LastTimeDiff).ToString();
				Device gs5 = Engine.GS;
				string {14599}5 = text;
				color = Color.Black;
				gs5.DrawString({14599}5, {18484}, color);
				Device gs6 = Engine.GS;
				string {14599}6 = text;
				vector = {18484} + new Vector2(1f, -1f);
				color = Color.White;
				gs6.DrawString({14599}6, vector, color);
			}
			{18483}.middleCpuTime.Push(Global.Game.GameTime.TimeUpdate);
			{18483}.middleGpuTime.Push(Global.Game.GameTime.TimeDraw);
			{18483}.countResults++;
			if ({18483}.countResults > 100)
			{
				{18483}.cpuTime = MathF.Round({18483}.middleCpuTime.AvgAndClean(), 3);
				{18483}.gpuTime = MathF.Round({18483}.middleGpuTime.AvgAndClean(), 3);
				{18483}.countResults = 0;
			}
		}

		// Token: 0x040006A4 RID: 1700
		private static ValueCounter middleCpuTime = new ValueCounter();

		// Token: 0x040006A5 RID: 1701
		private static ValueCounter middleGpuTime = new ValueCounter();

		// Token: 0x040006A6 RID: 1702
		private static float cpuTime;

		// Token: 0x040006A7 RID: 1703
		private static float gpuTime;

		// Token: 0x040006A8 RID: 1704
		private static int countResults;
	}
}
