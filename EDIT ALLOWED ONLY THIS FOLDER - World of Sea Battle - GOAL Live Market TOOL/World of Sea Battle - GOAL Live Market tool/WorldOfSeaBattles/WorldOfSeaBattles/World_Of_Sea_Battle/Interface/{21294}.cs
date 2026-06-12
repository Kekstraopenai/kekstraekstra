using System;
using System.Linq;
using TheraEngine;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002FE RID: 766
	public class {21294} : Form
	{
		// Token: 0x060010E3 RID: 4323 RVA: 0x0008D6B4 File Offset: 0x0008B8B4
		public {21294}(Marker {21298}, float {21299}, params Button[] {21300}) : base({21298}, PositionAlignment.Center, PositionAlignment.Center)
		{
			this.AnimatedFocus = false;
			float num = {21298}.Width - {21299} * (float)({21300}.Length - 1);
			float num2 = {21300}.Sum((Button {21301}) => {21301}.Font.Measure({21301}.Text).X + 100f);
			float num3 = 0f;
			foreach (Button button in {21300})
			{
				float num4 = button.Font.Measure(button.Text).X + 100f;
				float {11528} = num * (num4 / num2);
				button.Pos = new Marker(num3, 0f, {11528}, button.Pos.Height);
				num3 += {21299} + button.PosWidth;
				base.AddChild(button);
			}
		}
	}
}
