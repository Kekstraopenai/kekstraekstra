using System;
using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001E5 RID: 485
	internal sealed class {19390} : CustomUi
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x000563DC File Offset: 0x000545DC
		public {19390}(Action {19399}, float {19400}, string {19401}, string {19402}, bool {19403} = true) : base(true)
		{
			{19390}.CurrentInstance = this;
			if (Global.Player.IsMendingBegin)
			{
				Global.Player.StopMending(true);
			}
			if (Global.Player != null)
			{
				Global.Player.ResetSpeedAndRotation();
			}
			this.BasicColor = Color.Transparent;
			this.TexturePath = AtlasGameGui.rect_asset_whitepixel_1px;
			this.{19408} = {19399};
			this.{19409} = {19400};
			this.{19410} = {19400};
			if ({19403})
			{
				base.AddChild(new Label(Engine.GS.UIArea.HalfWidthHeightInt() - new Vector2(0f, 60f), Fonts.Philosopher_16, Color.White * 0.6f, {19402}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			}
			base.AddChild(new UiControl[]
			{
				new Label(Engine.GS.UIArea.HalfWidthHeightInt() - new Vector2(0f, 30f), Fonts.Philosopher_16, Color.White, {19401}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center(),
				this.{19411} = new Label(Engine.GS.UIArea.HalfWidthHeightInt(), Fonts.Philosopher_24, Color.White, "15", PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center(),
				new Label(Engine.GS.UIArea.HalfWidthHeightInt() + new Vector2(0f, 30f), Fonts.Philosopher_16, Color.White, Local.ExitScreen_2, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center()
			});
			base.EvRemoveFromContainer += delegate()
			{
				{19390}.CurrentInstance = null;
			};
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0005657D File Offset: 0x0005477D
		public {19390}(Action {19404}, float {19405}, bool {19406} = true) : this({19404}, {19405}, Local.ExitScreen_1, Local.ExitScreen_0, {19406})
		{
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00056594 File Offset: 0x00054794
		protected override void UserUpdate(ref FrameTime {19407})
		{
			this.{19409} -= {19407}.msElapsed;
			this.{19411}.Text = ((int)Math.Ceiling((double)(this.{19409} / 1000f))).ToString();
			if (!this.{19412} && !InputHelper.NowMouseState.LeftPressed)
			{
				this.{19412} = true;
			}
			if ((InputHelper.NowInputState.DownKeys.Length != 0 && InputHelper.NowInputState.DownKeys[0] != Keys.LeftControl) || (this.{19412} && (InputHelper.NowMouseState.LeftPressed || InputHelper.NowMouseState.RightPressed)))
			{
				base.RemoveFromContainer();
				return;
			}
			if (this.{19409} < 1f)
			{
				base.RemoveFromContainer();
				Action action = this.{19408};
				if (action == null)
				{
					return;
				}
				action();
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0005665F File Offset: 0x0005485F
		protected override void UserBackRender()
		{
			this.BasicColor.A = (byte)((int)(127.5f * (1f - this.{19409} / this.{19410})));
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x040009C2 RID: 2498
		public static {19390} CurrentInstance;

		// Token: 0x040009C3 RID: 2499
		private Action {19408};

		// Token: 0x040009C4 RID: 2500
		private float {19409};

		// Token: 0x040009C5 RID: 2501
		private float {19410};

		// Token: 0x040009C6 RID: 2502
		private Label {19411};

		// Token: 0x040009C7 RID: 2503
		private bool {19412};
	}
}
