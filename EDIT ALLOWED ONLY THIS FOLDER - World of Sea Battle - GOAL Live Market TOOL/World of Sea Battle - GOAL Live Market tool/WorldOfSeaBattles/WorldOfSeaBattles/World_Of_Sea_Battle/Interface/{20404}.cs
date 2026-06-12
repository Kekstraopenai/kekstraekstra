using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200027C RID: 636
	internal class {20404}
	{
		// Token: 0x06000E13 RID: 3603 RVA: 0x0007667C File Offset: 0x0007487C
		public static void CreateImageMenu(Form {20405}, Rectangle {20406}, Func<int, bool> {20407}, float {20408}, Vector2 {20409}, params {20397}[] {20410})
		{
			int num = 0;
			int num2 = 0;
			float num3 = 20f;
			Tlist<UiControl> allForms = new Tlist<UiControl>();
			for (int i = 0; i < {20410}.Length; i++)
			{
				{20397} {20397} = {20410}[i];
				float num4 = {20397}.Centroid ? ((float)(-(float){20397}.Path.Width / 2)) : ((num % 2 == 0) ? ((float)(-(float){20397}.Path.Width) - {20408}) : {20408});
				float num5 = (float)(num / 2) * ((float){20397}.Path.Height + {20408} + num3);
				float num6 = MathF.Ceiling(((float){20397}.Path.Height + {20408} + num3) * (float)({20410}.Length + {20410}.Length % 2) / 2f);
				Vector2 value;
				value.X = {20405}.Pos.Center.X + num4 + (float)({20397}.Path.Width / 2);
				value.Y = {20405}.Pos.Center.Y - num6 / 2f + num5 + 10f;
				Vector2 value2;
				value2.X = {20405}.Pos.Center.X + num4;
				value2.Y = {20405}.Pos.Center.Y - num6 / 2f + num5 + num3;
				int t = num2;
				Label label;
				{20405}.AddChild(label = new Label(value + {20409}, Fonts.Philosopher_16, Color.White * 0.6f, {20397}.Header, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
				Form form;
				{20405}.AddChild(form = new Form(value2 + {20409}, {20397}.Path, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					DisableDepthFocusTest = true
				}.ExClick(delegate(ClickUiEventArgs {20411})
				{
					if ({20407}(t))
					{
						foreach (UiControl uiControl2 in ((IEnumerable<UiControl>)allForms))
						{
							uiControl2.RemoveFromContainer();
						}
						allForms.Clear();
					}
				}));
				Tlist<UiControl> allForms3 = allForms;
				UiControl uiControl = form;
				allForms3.Add(uiControl);
				Tlist<UiControl> allForms2 = allForms;
				uiControl = label;
				allForms2.Add(uiControl);
				if (!string.IsNullOrEmpty({20397}.Description))
				{
					Form descForm = new Form(form.Pos, {20406}, Color.Black * 0.5f, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					descForm.AddChild(TextBlockBuilder.CreateBlock((float)({20397}.Path.Width - 10), {20397}.Description, Color.White * 0.8f, Fonts.Arial_10, -3f).CreateCentroid(new Vector2(descForm.Pos.Center.X, descForm.Pos.XY.Y + 5f)));
					form.AddChild(descForm);
					descForm.UpdateComplete += delegate(UiControl {20412})
					{
						descForm.IsVisible = (form.InputMode == MouseInputMode.Focused);
					};
				}
				num2++;
				num++;
				if ({20397}.Centroid)
				{
					num++;
				}
			}
		}
	}
}
