using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine.Input;
using TheraEngine.Interface.Eventing;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000D0 RID: 208
	public class ListItemViewControl : UiControl
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001D267 File Offset: 0x0001B467
		public int ChildCount
		{
			get
			{
				return this.{14037}.GetChildren.Size;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0001D279 File Offset: 0x0001B479
		public float CurrentScrollValue
		{
			get
			{
				return this.{14038}.CurrentScrollValue;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001D286 File Offset: 0x0001B486
		public float MaxScrollValue
		{
			get
			{
				return this.{14038}.MaxScrollValue;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0001D293 File Offset: 0x0001B493
		public Vector2 ContentSize
		{
			get
			{
				return this.{14037}.Pos.WH;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001D2A5 File Offset: 0x0001B4A5
		public ScrollBarControl ReferencedScrollBar
		{
			get
			{
				return this.{14039};
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001D2B0 File Offset: 0x0001B4B0
		public ListItemViewControl(Marker {14024}, ScrollBarControl {14025}, PositionAlignment {14026} = PositionAlignment.LeftUp, PositionAlignment {14027} = PositionAlignment.LeftUp) : base({14024}, {14026}, {14027}, Color.White, false)
		{
			this.SetScrollBar({14025});
			this.{14037} = new StackForm({14024}.XY, UiOrientation.Vertical, PositionAlignment.Both, PositionAlignment.Both);
			base.UseScissor = true;
			base.AddChild(this.{14037});
			this.{14037}.EvDynamicStorageStateChanged += this.{14036};
			this.{14038} = new Scroller(800f);
			this.{14038}.MaxScrollValue = 10000f;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001D332 File Offset: 0x0001B532
		internal void SetScrollBar(ScrollBarControl {14028})
		{
			this.{14039} = {14028};
			if ({14028} != null)
			{
				this.{14039}.EvChange += this.{14034};
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001D358 File Offset: 0x0001B558
		internal override void Update(ref FrameTime {14029}, ref int {14030})
		{
			base.Update(ref {14029}, ref {14030});
			if (base.InputMode != MouseInputMode.NoFocus)
			{
				int num = InputHelper.NowMouseState.ScrollValue - InputHelper.LastMouseState.ScrollValue;
				float num2 = (float)Math.Abs(num) * Scroller.ScrollSpeedFactor;
				if (num > 0)
				{
					this.{14038}.ScrollBack(num2);
				}
				if (num < 0)
				{
					this.{14038}.ScrollNext(num2);
				}
			}
			this.{14038}.Update({14029}.msElapsed);
			float num3 = (float)((int)this.{14038}.CurrentScrollValue);
			float num4 = (float)((int)(base.Pos.XY.Y - this.{14037}.Pos.XY.Y));
			if (num3 != num4)
			{
				this.{14037}.Pos = new Marker(this.{14037}.Pos.XY.X, base.Pos.XY.Y - num3, this.{14037}.Pos.WH.X, this.{14037}.Pos.WH.Y);
				this.{14040} = true;
				this.{14039}.CurrentScrollFactor = ((this.{14038}.MaxScrollValue == 0f) ? 0f : (num3 / this.{14038}.MaxScrollValue));
				this.{14040} = false;
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001BA77 File Offset: 0x00019C77
		internal override void Render()
		{
			base.Render();
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001D4A6 File Offset: 0x0001B6A6
		public void AddItem(params UiControl[] {14031})
		{
			this.{14037}.AddItem({14031});
			this.{14033}();
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001D4BA File Offset: 0x0001B6BA
		public void Clear()
		{
			this.{14037}.Clear();
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001D4C7 File Offset: 0x0001B6C7
		public void SetScrollValue(float {14032})
		{
			this.{14038}.CurrentScrollValue = {14032};
			this.{14038}.Stopful();
			this.{14033}();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001D4E6 File Offset: 0x0001B6E6
		public void ScrollToEnd()
		{
			this.SetScrollValue(float.MaxValue);
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000DD52 File Offset: 0x0000BF52
		protected override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001D4F3 File Offset: 0x0001B6F3
		protected internal override void MarkerChanged()
		{
			base.UseScissor = true;
			this.{14033}();
			base.MarkerChanged();
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001D508 File Offset: 0x0001B708
		private void {14033}()
		{
			float val = this.{14037}.Pos.WH.Y - base.Pos.WH.Y;
			this.{14038}.MaxScrollValue = Math.Max(0f, val);
			this.{14038}.Normalize();
			this.{14039}.CurrentScrollFactor = ((this.{14038}.MaxScrollValue == 0f) ? 0f : (this.{14038}.CurrentScrollValue / this.{14038}.MaxScrollValue));
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001D598 File Offset: 0x0001B798
		private void {14034}(ScrollBarChangeEventArgs {14035})
		{
			if (this.{14040})
			{
				return;
			}
			this.{14038}.CurrentScrollValue = this.{14038}.MaxScrollValue * {14035}.Sender.CurrentScrollFactor;
			this.{14038}.Stopful();
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001D5D0 File Offset: 0x0001B7D0
		protected override void CleanResources()
		{
			this.{14039}.EvChange -= this.{14034};
			this.{14039} = null;
			base.CleanResources();
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001D5F6 File Offset: 0x0001B7F6
		[CompilerGenerated]
		private void {14036}()
		{
			this.{14033}();
		}

		// Token: 0x04000437 RID: 1079
		private StackForm {14037};

		// Token: 0x04000438 RID: 1080
		private Scroller {14038};

		// Token: 0x04000439 RID: 1081
		private ScrollBarControl {14039};

		// Token: 0x0400043A RID: 1082
		private bool {14040};
	}
}
