using System;
using System.Runtime.CompilerServices;
using Common;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200016E RID: 366
	internal class {18690} : {18637}
	{
		// Token: 0x06000874 RID: 2164 RVA: 0x00041D34 File Offset: 0x0003FF34
		public {18690}()
		{
			this.timeoutMs = 40000f;
			this.{18694} = new Button(new Vector2((float)(Engine.GS.UIArea.Width / 2 - AtlasGameGui.basicBlueButton.Width), (float)((int)((float)Engine.GS.UIArea.Height * 0.7f))), AtlasGameGui.basicBlueButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat, Local.onboarding_education_back2_bttext, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			stackForm.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_12, Color.Wheat, Local.onboarding_education_back2_bttext_2(Global.Settings.kb_KeyShowMouse.KeyToString), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			this.{18694}.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.Center, 0f);
			this.{18694}.EvClick += this.{18692};
			this.{18694}.Opacity = 0f;
			new UiActionsSleep(this.{18694}, 2000f);
			new UiOpacityAnimation(this.{18694}, 0f, 1f, 300f);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0004183F File Offset: 0x0003FA3F
		public override void Begin()
		{
			base.Begin();
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00041E76 File Offset: 0x00040076
		public override void Update(ref FrameTime {18691})
		{
			if (this.timeoutMs < 1000f && this.{18694} != null)
			{
				this.{18694}.RemoveFromContainer();
				this.{18694} = null;
			}
			base.Update(ref {18691});
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0000744B File Offset: 0x0000564B
		protected override string BuildText()
		{
			return "";
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00041EA6 File Offset: 0x000400A6
		public override void Dispose()
		{
			Button button = this.{18694};
			if (button != null)
			{
				button.RemoveFromContainer();
			}
			this.{18694} = null;
			base.Dispose();
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00041EC6 File Offset: 0x000400C6
		[CompilerGenerated]
		private void {18692}(ClickUiEventArgs {18693})
		{
			this.timeoutMs = 1000f;
			this.{18694}.IsVisible = false;
		}

		// Token: 0x04000787 RID: 1927
		private Button {18694};
	}
}
