using System;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Grphics.Device;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x02000494 RID: 1172
	public class ProgressiveHealthBar
	{
		// Token: 0x060019B8 RID: 6584 RVA: 0x000E4798 File Offset: 0x000E2998
		public void Draw(in Marker {24290}, Rectangle {24291}, Rectangle {24292}, float {24293}, float {24294})
		{
			int num = (int)Math.Ceiling((double)({24294} / 250f));
			float num2 = {24290}.WH.X / (float)num;
			float num3 = 0f;
			for (int i = 0; i < num; i++)
			{
				Device gs = Engine.GS;
				Rectangle rectangle = new Rectangle((int)({24290}.XY.X + num3), (int){24290}.XY.Y, (int)num2, (int){24290}.WH.Y);
				gs.Draw({24292}, rectangle);
				num3 += num2;
			}
		}
	}
}
