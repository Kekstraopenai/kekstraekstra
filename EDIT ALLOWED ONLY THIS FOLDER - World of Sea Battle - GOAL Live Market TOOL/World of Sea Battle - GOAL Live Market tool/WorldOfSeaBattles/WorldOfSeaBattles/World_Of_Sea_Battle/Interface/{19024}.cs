using System;
using Common;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001A6 RID: 422
	internal sealed class {19024} : CustomUi
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x0004AD17 File Offset: 0x00048F17
		public {19024}() : base(false)
		{
			{19024}.CurrentInstance = this;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserUpdate(ref FrameTime {19025})
		{
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0004AD28 File Offset: 0x00048F28
		public static void ShowDiffCards(PassingMapDiffCard[] {19026})
		{
			if ({19026}.Length == 0)
			{
				return;
			}
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				PositionAlignment_X = PositionAlignment.Center,
				PositionAlignment_Y = PositionAlignment.Center
			};
			foreach (PassingMapDiffCard {3328} in {19026})
			{
				Form form = new Form(Vector2.Zero, new Rectangle(2331, 1192, 199, 268), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form.AddChild(TextBlockBuilder.CreateBlock(160f, {3328}.GetDescription(), Color.Wheat * 0.7f, Fonts.Philosopher_14, 0f).CreateCentroid(new Vector2(form.Pos.Center.X, 114f)));
				stackForm.AddItem(new UiControl[]
				{
					form
				});
				form.Opacity = 0f;
				new UiActionsSleep(form, (float)(stackForm.CountChild() * 400));
				new UiOpacityAnimation(form, 0f, 1f, 200f);
			}
			UiControl uiControl = stackForm;
			Marker pos = stackForm.Pos;
			Vector2 vector = Engine.GS.UIArea.WidthHeight() * 0.5f - stackForm.Pos.WH * 0.5f;
			uiControl.Pos = pos.SetXY(vector);
			stackForm.AddItemWithoutChangePosition(new Button(new Vector2(stackForm.Pos.Center.X - 124f, stackForm.Pos.End.Y), AtlasGameGui.basicBlueButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText("[" + Global.Settings.kb_Action.KeyToString + "] - " + Local.close, Fonts.Philosopher_16, Color.White * 0.8f, false));
			stackForm.UpdateComplete += delegate(UiControl {19027})
			{
				if ({19027}.AnimationsCount == 0 && Global.Settings.kb_Action.IsDown)
				{
					new UiOpacityAnimation({19027}, 1f, 0f, 1000f);
					new UiRemoveAction({19027});
				}
			};
		}

		// Token: 0x04000899 RID: 2201
		public static {19024} CurrentInstance;
	}
}
