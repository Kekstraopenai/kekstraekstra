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
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002C2 RID: 706
	internal abstract class {20849} : CustomUi
	{
		// Token: 0x06000F90 RID: 3984 RVA: 0x00082ABC File Offset: 0x00080CBC
		public {20849}(bool {20851}) : base(false)
		{
			this.AnimatedFocus = false;
			if ({20851})
			{
				this.TexturePath = new Rectangle(1274, 1413, 400, 206);
				this.BasicColor = Color.White;
				Form form = new Form(new Marker(0f, -45f, (float)Engine.GS.UIArea.Width, (float)({20849}.c_topBlackGradient.Height * (1920 / {20849}.c_topBlackGradient.Width))), {20849}.c_topBlackGradient, PositionAlignment.Both, PositionAlignment.LeftUp);
				form.AnimatedFocus = false;
				Form {13204} = form;
				this.headLine = form;
				base.AddChild({13204});
				this.headLine.AddChild(this.{20855} = new LabelButton(new Vector2((float)(Engine.GS.UIArea.Width / 2), 24f), Local.PortPage_0, Fonts.Philosopher_14, Color.Gray, Color.White, new Action<ClickUiEventArgs>(this.{20853}))
				{
					DisableDepthFocusTest = false,
					PositionAlignment_X = PositionAlignment.Center
				}.Center());
				{19970} currentInstance = {19970}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.MoveToFrontLevel();
				}
			}
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UIButton, 0.03f, 1f);
			new UiOpacityAnimation(this, 0f, 1f, 190f);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00082C0D File Offset: 0x00080E0D
		protected void ClosePage()
		{
			Global.Game.ScenePort.mainHandler(null);
			this.{20855}.AllowMouseInput = false;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserUpdate(ref FrameTime {20852})
		{
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00082C48 File Offset: 0x00080E48
		[CompilerGenerated]
		private void {20853}(ClickUiEventArgs {20854})
		{
			this.ClosePage();
		}

		// Token: 0x04000E47 RID: 3655
		public static readonly Rectangle c_topBlackGradient = new Rectangle(791, 1795, 960, 47);

		// Token: 0x04000E48 RID: 3656
		private UiControl {20855};

		// Token: 0x04000E49 RID: 3657
		protected Form headLine;
	}
}
