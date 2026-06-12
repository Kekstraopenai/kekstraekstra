using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200006A RID: 106
	internal abstract class {17068} : CustomUi
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0001B76D File Offset: 0x0001996D
		protected override bool CanBeWindow
		{
			get
			{
				return this.{17100} != {17068}.BlockingWay.BackgroundBlocking;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (set) Token: 0x06000345 RID: 837 RVA: 0x0001B77B File Offset: 0x0001997B
		protected bool EnableBackgroundNow
		{
			set
			{
				this.background.IsVisible = value;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001B78C File Offset: 0x0001998C
		private static Rectangle scaleR(Rectangle {17078}, float {17079})
		{
			float num = {17079} / (float){17078}.Width;
			return new Rectangle({17078}.X, {17078}.Y, (int)((float){17078}.Width * num), (int)((float){17078}.Height * num));
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0001B7C8 File Offset: 0x000199C8
		public {17068}(Rectangle {17080}, float {17081}, {17068}.BlockingWay {17082}, bool {17083}) : this(Marker.FromCentrScreen(Engine.GS.UIArea.WidthHeight(), {17068}.scaleR({17080}, {17081})), {17080}, {17082}, {17083})
		{
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001B7F0 File Offset: 0x000199F0
		public {17068}(Marker {17084}, Rectangle {17085}, {17068}.BlockingWay {17086}, bool {17087}) : base({17084}, {17085}, PositionAlignment.Center, PositionAlignment.Center, Color.White, false)
		{
			{17068} <>4__this = this;
			if (Global.Player == null)
			{
				{17087} = false;
			}
			Global.Game.inactivityDuration = 0f;
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UIButton, 0.03f, 1f);
			this.AnimatedFocus = false;
			this.{17100} = {17086};
			this.{17101} = {17087};
			if ({17087})
			{
				Global.Player.ResetSpeedAndRotation();
			}
			if ({17086} != {17068}.BlockingWay.NoBackground)
			{
				if ({17087})
				{
					GameScene.IncreaseGameInput();
				}
				if ({17086} != {17068}.BlockingWay.BackgroundClosingTransparentNoInputChaning)
				{
					Global.Game.SceneGame.IncreaseMouse();
				}
				this.background = new Image(new Marker(0f, 0f, (float)Engine.GS.UIArea.Width, (float)Engine.GS.UIArea.Height), CommonAtlas.Texture.Tex, new Rectangle(569, 3791, 402, 207), PositionAlignment.Both, PositionAlignment.Both);
				if ({17086} == {17068}.BlockingWay.BackgroundClosingTransparent || {17086} == {17068}.BlockingWay.BackgroundClosingTransparentNoInputChaning)
				{
					this.background.BasicColor = Color.Transparent;
				}
				else
				{
					this.background.BasicColor = {17068}.c_backColor;
				}
				if ({17086} == {17068}.BlockingWay.BackgroundClosing || {17086} == {17068}.BlockingWay.BackgroundClosingTransparent || {17086} == {17068}.BlockingWay.BackgroundClosingTransparentNoInputChaning)
				{
					this.background.EvClick += delegate(ClickUiEventArgs {17104})
					{
						if (!<>4__this.AllowMouseInput && Session.CurrentCrewJob != null)
						{
							return;
						}
						for (int i = 0; i < <>4__this.{17099}.Size; i++)
						{
							if (<>4__this.{17099}.Array[i].InputMode != MouseInputMode.NoFocus)
							{
								return;
							}
						}
						<>4__this.BlockAndClose();
					};
				}
				new UiOpacityAnimation(this.background, 0.0001f, 1f, 260f);
			}
			base.MoveToFrontLevel();
			base.EvRemoveFromContainer += delegate()
			{
				if ({17086} != {17068}.BlockingWay.NoBackground)
				{
					if ({17087})
					{
						GameScene.DecreaseGameInput();
					}
					if ({17086} != {17068}.BlockingWay.BackgroundClosingTransparentNoInputChaning)
					{
						Global.Game.SceneGame.DecreaseMouse();
					}
				}
				if (<>4__this.background != null)
				{
					<>4__this.background.RemoveFromContainer();
				}
				<>4__this.background = null;
			};
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0001B9DF File Offset: 0x00019BDF
		public void RemoveAutoBackground()
		{
			if (this.background != null)
			{
				this.background.RemoveFromContainer();
			}
			this.background = null;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0001B9FC File Offset: 0x00019BFC
		protected void InitializePages(Tab {17088}, bool {17089}, Marker {17090}, params KeyValuePair<Form, string>[] {17091})
		{
			if (this.{17103} != null)
			{
				this.{17103}.RemoveFromContainer();
			}
			this.{17103} = new StackForm(base.Pos.XY + new Vector2({17090}.XY.X + 4f, 20f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			int num = 0;
			for (int i = 0; i < {17091}.Length; i++)
			{
				KeyValuePair<Form, string> keyValuePair = {17091}[i];
				if ({17088}.GetPagesCount == 0)
				{
					{17088}.AddAndSelect(keyValuePair.Key);
				}
				else
				{
					{17088}.Add(new Form[]
					{
						keyValuePair.Key
					});
				}
				int cachedIndex = num;
				this.{17103}.AddItem(new UiControl[]
				{
					new LabelButton(Vector2.Zero, keyValuePair.Value, {17089} ? Fonts.Philosopher_18 : Fonts.F_m14_ThinBold, Color.Gray, Color.White, delegate(ClickUiEventArgs {17105})
					{
						{17088}.Select(cachedIndex);
					})
				});
				this.{17103}.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, {17089} ? Fonts.Philosopher_18 : Fonts.F_m14_ThinBold, Color.DimGray, " | ", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				num++;
			}
			base.AddChild(this.{17103});
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001BB80 File Offset: 0x00019D80
		protected void InitializePagesAddCustomAction(string {17092}, bool {17093}, Action {17094})
		{
			this.{17103}.AddItem(new UiControl[]
			{
				new LabelButton(Vector2.Zero, {17092}, {17093} ? Fonts.Philosopher_18 : Fonts.F_m14_ThinBold, Color.Gray, Color.White, delegate(ClickUiEventArgs {17106})
				{
					{17094}();
				})
			});
			this.{17103}.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, {17093} ? Fonts.Philosopher_18 : Fonts.F_m14_ThinBold, Color.DimGray, " | ", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001BC18 File Offset: 0x00019E18
		public void BlockAndClose()
		{
			if (this.IsClosedByHand)
			{
				return;
			}
			this.IsClosedByHand = true;
			base.AllowMouseInput = false;
			int num = this.QuickClosing ? 70 : 260;
			base.RemoveAnimations();
			new UiMarkerAndOpacityAnimation(this, 1f, 0f, base.Pos, base.Pos, (float)num, UiAmimationCurve.Linear);
			if (this.background != null)
			{
				new UiOpacityAnimation(this.background, 1f, 0.0001f, (float)num);
			}
			new UiActor(this, new Action(base.RemoveFromContainer));
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UITabSwitch, 0.03f, 1f);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0001BCC2 File Offset: 0x00019EC2
		public void CombineCloserLayer(UiControl {17095})
		{
			this.{17099}.Add({17095});
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001BCD4 File Offset: 0x00019ED4
		protected override void UserUpdate(ref FrameTime {17096})
		{
			if (InputHelper.IsClick(Keys.Escape) && this.CanBeWindow && base.IsTopmostCustomUi)
			{
				this.BlockAndClose();
			}
			if (!this.{17101} && Global.Player != null && this.{17100} != {17068}.BlockingWay.BackgroundClosingTransparent && this.{17100} != {17068}.BlockingWay.BackgroundClosingTransparentNoInputChaning && GameScene.GameHasInputFocus)
			{
				this.{17102}.Evalute(ref {17096}, Global.Player.NowSpeed > 2f);
				this.BasicColor = Color.White * MathHelper.Lerp(1f, 0.6f, this.{17102}.CurrentSoftValueSmoothstep);
				if (this.background != null)
				{
					this.background.BasicColor = Color.Lerp({17068}.c_backColor, Color.Transparent, this.{17102}.CurrentSoftValueSmoothstep);
				}
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0001BDA8 File Offset: 0x00019FA8
		protected ListItemViewControl ListHelper(Marker {17097}, IEnumerable<UiControl> {17098})
		{
			Marker marker = new Marker({17097}.XY.X + {17097}.WH.X - (float)CommonAtlas.c_scrollPoint.Width, {17097}.XY.Y, (float)CommonAtlas.c_scrollUp.Width, {17097}.WH.Y);
			Marker pos = base.Pos;
			ScrollBarControl scrollBarControl = new ScrollBarControl(marker.Offset(pos.XY), CommonAtlas.c_scrollUp, CommonAtlas.c_scrollDown, CommonAtlas.c_scrollPoint, CommonAtlas.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			pos = base.Pos;
			ListItemViewControl listItemViewControl = new ListItemViewControl({17097}.Offset(pos.XY).SetWidth({17097}.WH.X - (float)CommonAtlas.c_scrollPoint.Width), scrollBarControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			foreach (UiControl uiControl in {17098})
			{
				listItemViewControl.AddItem(new UiControl[]
				{
					uiControl
				});
			}
			base.AddChild(new UiControl[]
			{
				scrollBarControl,
				listItemViewControl
			});
			return listItemViewControl;
		}

		// Token: 0x0400028A RID: 650
		private static readonly Color c_backColor = Color.White * 0.7f;

		// Token: 0x0400028B RID: 651
		private Tlist<UiControl> {17099} = new Tlist<UiControl>();

		// Token: 0x0400028C RID: 652
		private {17068}.BlockingWay {17100};

		// Token: 0x0400028D RID: 653
		private bool {17101};

		// Token: 0x0400028E RID: 654
		protected UiControl background;

		// Token: 0x0400028F RID: 655
		private SoftTrigger {17102} = new SoftTrigger(0f, 1f, 0.6f);

		// Token: 0x04000290 RID: 656
		public bool IsClosedByHand;

		// Token: 0x04000291 RID: 657
		protected bool QuickClosing;

		// Token: 0x04000292 RID: 658
		private StackForm {17103};

		// Token: 0x0200006B RID: 107
		public enum BlockingWay
		{
			// Token: 0x04000294 RID: 660
			NoBackground,
			// Token: 0x04000295 RID: 661
			BackgroundBlocking,
			// Token: 0x04000296 RID: 662
			BackgroundClosing,
			// Token: 0x04000297 RID: 663
			BackgroundClosingTransparent,
			// Token: 0x04000298 RID: 664
			BackgroundClosingTransparentNoInputChaning
		}
	}
}
