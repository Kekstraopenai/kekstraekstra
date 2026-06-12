using System;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000279 RID: 633
	internal static class {20391}
	{
		// Token: 0x06000E0B RID: 3595 RVA: 0x00076384 File Offset: 0x00074584
		private static Vector2 ScalePosToScreen(Vector2 {20392})
		{
			return new Vector2((float)Engine.GS.UIArea.X + {20392}.X * (float)Engine.GS.UIArea.Width, (float)Engine.GS.UIArea.Y + {20392}.Y * (float)Engine.GS.UIArea.Height);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x000763E8 File Offset: 0x000745E8
		private static Vector2 ClampScreenPos(Vector2 {20393})
		{
			return new Vector2(({20393}.X - (float)Engine.GS.UIArea.X) / (float)Engine.GS.UIArea.Width, ({20393}.Y - (float)Engine.GS.UIArea.Y) / (float)Engine.GS.UIArea.Height);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0007644C File Offset: 0x0007464C
		public static void WhenInit(UiControl {20394}, string {20395})
		{
			{20391}.<>c__DisplayClass2_0 CS$<>8__locals1 = new {20391}.<>c__DisplayClass2_0();
			CS$<>8__locals1.key = {20395};
			CS$<>8__locals1.ui = {20394};
			Vector2 {20392};
			Vector2? vector = Global.Settings.DraggablesPositions.TryGetValue(CS$<>8__locals1.key, out {20392}) ? new Vector2?({20391}.ScalePosToScreen({20392})) : null;
			if (vector != null)
			{
				UiControl ui = CS$<>8__locals1.ui;
				Vector2 value = vector.Value;
				ui.Pos = new Marker(ref value, ref CS$<>8__locals1.ui.Pos.WH);
			}
			CS$<>8__locals1.ui.EvRemoveFromContainer += CS$<>8__locals1.<WhenInit>g__CachedSave|0;
			CS$<>8__locals1.ui.EvLostMouseFocus += CS$<>8__locals1.<WhenInit>g__CachedSave|0;
			CS$<>8__locals1.parentClass = (CS$<>8__locals1.ui as Form);
			if (CS$<>8__locals1.parentClass != null)
			{
				CS$<>8__locals1.ui.UpdateComplete += delegate(UiControl {20396})
				{
					if (CS$<>8__locals1.parentClass.AllowDragDrop = Global.Settings.EnableDragDrop)
					{
						base.<WhenInit>g__CachedValidate|1();
					}
				};
			}
		}
	}
}
