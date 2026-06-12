using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;

namespace TheraEngine.Interface
{
	// Token: 0x0200009C RID: 156
	public abstract class CustomUi : Form
	{
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000408 RID: 1032 RVA: 0x00015F34 File Offset: 0x00014134
		// (remove) Token: 0x06000409 RID: 1033 RVA: 0x00015F6C File Offset: 0x0001416C
		public event Action EvClickBackLockScreen
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{13038};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{13038}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{13038};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{13038}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000E21A File Offset: 0x0000C41A
		protected virtual bool CanBeWindow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00015FA1 File Offset: 0x000141A1
		public bool IsTopmostCustomUi
		{
			get
			{
				if (base.GetParent == null)
				{
					return true;
				}
				return base.GetParent.GetChildren.LastOrDefault(delegate(UiControl {13040})
				{
					CustomUi customUi = {13040} as CustomUi;
					return customUi != null && customUi.CanBeWindow;
				}) == this;
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00015FE0 File Offset: 0x000141E0
		public CustomUi(bool {13022})
		{
			Rectangle uiarea = Engine.GS.UIArea;
			base..ctor(new Marker(ref uiarea), Rectangle.Empty, Color.Transparent, PositionAlignment.Both, PositionAlignment.Both);
			this.AnimatedFocus = false;
			base.RenderToDepthMap = {13022};
			this.{13029}();
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00016034 File Offset: 0x00014234
		public CustomUi(Marker {13023}, Rectangle {13024}, PositionAlignment {13025}, PositionAlignment {13026}, Color {13027}, bool {13028} = false) : base({13023}, {13024}, {13027}, {13025}, {13026})
		{
			if ({13028})
			{
				Rectangle uiarea = Engine.GS.UIArea;
				Form form = new Form(new Marker(ref uiarea), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					PositionAlignment_X = PositionAlignment.Both,
					PositionAlignment_Y = PositionAlignment.Both
				};
				form.EvClick += this.{13035};
				form.AddChild(this);
				this.RemoveWithThis(new UiControl[]
				{
					form
				});
			}
			this.{13029}();
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000160BD File Offset: 0x000142BD
		private void {13029}()
		{
			base.EvRemoveFromContainer += this.{13037};
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000160D1 File Offset: 0x000142D1
		internal override void Update(ref FrameTime {13030}, ref int {13031})
		{
			base.Update(ref {13030}, ref {13031});
			this.UserUpdate(ref {13030});
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000160E4 File Offset: 0x000142E4
		internal override void Render()
		{
			this.UserBackRender();
			if (this.TexturePath.Width > 0)
			{
				Device gs = Engine.GS;
				Rectangle rectangle = base.Pos.ToRect();
				Color color = UiControl.ComputeColor(base.GetBrightness(), base.GetOpcaity(), this.BasicColor * this.textureAddColor);
				gs.Draw(this.TexturePath, rectangle, color);
			}
			this.UserMiddleRender();
			base.RenderChilds();
			this.UserFrontRender();
		}

		// Token: 0x06000411 RID: 1041
		protected abstract void UserUpdate(ref FrameTime {13032});

		// Token: 0x06000412 RID: 1042
		protected abstract void UserBackRender();

		// Token: 0x06000413 RID: 1043 RVA: 0x0000C282 File Offset: 0x0000A482
		public virtual void UserMiddleRender()
		{
		}

		// Token: 0x06000414 RID: 1044
		protected abstract void UserFrontRender();

		// Token: 0x06000415 RID: 1045 RVA: 0x00016160 File Offset: 0x00014360
		public void RemoveWithThis(params UiControl[] {13033})
		{
			for (int i = 0; i < {13033}.Length; i++)
			{
				this.{13039}.Add({13033}[i]);
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001618F File Offset: 0x0001438F
		protected void RenderControl(UiControl {13034})
		{
			{13034}.Render();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00016197 File Offset: 0x00014397
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001619F File Offset: 0x0001439F
		[CompilerGenerated]
		private void {13035}(ClickUiEventArgs {13036})
		{
			Action action = this.{13038};
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000161B4 File Offset: 0x000143B4
		[CompilerGenerated]
		private void {13037}()
		{
			for (int i = 0; i < this.{13039}.Size; i++)
			{
				this.{13039}.Array[i].RemoveFromContainer();
			}
			this.{13039}.Clear();
		}

		// Token: 0x04000327 RID: 807
		[CompilerGenerated]
		private Action {13038};

		// Token: 0x04000328 RID: 808
		private Tlist<UiControl> {13039} = new Tlist<UiControl>(10);
	}
}
