using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;
using TheraEngine.Grphics.Device;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000D3 RID: 211
	public class StackForm : UiControl
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x0001DC32 File Offset: 0x0001BE32
		public static StackForm Stack(Vector2 {14074}, UiOrientation {14075}, params UiControl[] {14076})
		{
			StackForm stackForm = new StackForm({14074}, {14075}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem({14076});
			return stackForm;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001DC44 File Offset: 0x0001BE44
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0001DC4C File Offset: 0x0001BE4C
		public UiOrientation SortMode
		{
			get
			{
				return this.{14095};
			}
			set
			{
				if (this.{14095} != value)
				{
					this.{14095} = value;
					this.{14089}();
				}
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001DC64 File Offset: 0x0001BE64
		public StackForm(Vector2 {14078}, UiOrientation {14079}, PositionAlignment {14080} = PositionAlignment.LeftUp, PositionAlignment {14081} = PositionAlignment.LeftUp)
		{
			Vector2 zero = Vector2.Zero;
			base..ctor(new Marker(ref {14078}, ref zero), {14080}, {14081}, Color.White, false);
			this.{14095} = {14079};
			base.EvSingleChildWasRemoved += this.{14092};
			base.SomethingWasRemoved += this.{14094};
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001DCBA File Offset: 0x0001BEBA
		public static StackForm QuickHorizontal(params UiControl[] {14082})
		{
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem({14082});
			return stackForm;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00016B89 File Offset: 0x00014D89
		internal override void Update(ref FrameTime {14083}, ref int {14084})
		{
			base.Update(ref {14083}, ref {14084});
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001DCD0 File Offset: 0x0001BED0
		internal override void Render()
		{
			if (this.TexturePath.Width > 0)
			{
				Device gs = Engine.GS;
				Rectangle rectangle = base.Pos.ToRect();
				Color color = UiControl.ComputeColor(base.GetBrightness(), base.GetOpcaity(), this.BasicColor);
				gs.Draw(this.TexturePath, rectangle, color);
			}
			base.Render();
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001DD2C File Offset: 0x0001BF2C
		public void AddSpace(float {14085})
		{
			if (this.{14095} == UiOrientation.ExpansiveSize)
			{
				base.Pos = base.Pos.SetHeight(base.Pos.WH.Y + {14085});
				return;
			}
			UiOrientation uiOrientation = this.{14095};
			bool flag = uiOrientation - UiOrientation.Vertical <= 1;
			bool flag2 = flag;
			this.AddItem(new UiControl[]
			{
				new Form(new Marker(0f, 0f, flag2 ? 1f : {14085}, (!flag2) ? 1f : {14085}), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					DisableDepthFocusTest = true,
					RenderToDepthMap = false
				}
			});
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001DDC8 File Offset: 0x0001BFC8
		public void AddItem(params UiControl[] {14086})
		{
			foreach (UiControl uiControl in {14086})
			{
				uiControl.PositionAlignment_X = PositionAlignment.LeftUp;
				uiControl.PositionAlignment_Y = PositionAlignment.LeftUp;
				if (uiControl.IsDynamicStorage)
				{
					uiControl.EvDynamicStorageStateChanged += this.{14089};
				}
			}
			if ({14086}.Length > 1 && this.{14095} != UiOrientation.ExpansiveSize)
			{
				base.AddChild({14086});
				this.{14089}();
				return;
			}
			this.{14090}({14086}[0]);
			base.AddChild({14086});
			base.OnDynamicStorageStateChanged();
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001680A File Offset: 0x00014A0A
		public void AddItemWithoutChangePosition(UiControl {14087})
		{
			base.AddChild({14087});
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001724A File Offset: 0x0001544A
		public void RemoveAt(UiControl {14088})
		{
			base.RemoveAt({14088}, true);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001DE44 File Offset: 0x0001C044
		public void Clear()
		{
			this.{14096} = true;
			base.ClearAllChild();
			this.{14096} = false;
			ref Marker ptr = ref base.Pos;
			Vector2 zero = Vector2.Zero;
			base.Pos = new Marker(ref ptr.XY, ref zero);
			base.OnDynamicStorageStateChanged();
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000E21A File Offset: 0x0000C41A
		protected override bool AllowResize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000DD52 File Offset: 0x0000BF52
		internal override bool IsDynamicStorage
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001DE8C File Offset: 0x0001C08C
		private void {14089}()
		{
			if (this.{14096} || this.{14095} == UiOrientation.ExpansiveSize)
			{
				return;
			}
			ref Marker ptr = ref base.Pos;
			Vector2 zero = Vector2.Zero;
			base.Pos = new Marker(ref ptr.XY, ref zero);
			Tlist<UiControl> getChildren = base.GetChildren;
			for (int i = 0; i < getChildren.Size; i++)
			{
				if (!this.removeQueueReferences.Contains(getChildren.Array[i]))
				{
					this.{14090}(getChildren.Array[i]);
				}
			}
			base.OnDynamicStorageStateChanged();
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001DF10 File Offset: 0x0001C110
		private void {14090}(UiControl {14091})
		{
			Vector2 wh = {14091}.Pos.WH;
			wh.X += 2f * this.BorderThickness;
			wh.Y += 2f * this.BorderThickness;
			if (this.{14095} == UiOrientation.ExpansiveSize)
			{
				Marker pos = {14091}.Pos;
				Marker marker = base.Pos;
				{14091}.Pos = pos.Offset(marker.XY);
				marker = base.Pos;
				marker = marker.SetWidth(Math.Max(base.Pos.WH.X, {14091}.Pos.End.X - base.Pos.XY.X));
				base.Pos = marker.SetHeight(Math.Max(base.Pos.WH.Y, {14091}.Pos.End.Y - base.Pos.XY.Y));
				return;
			}
			Marker marker2;
			if (this.{14095} != UiOrientation.Vertical && this.{14095} != UiOrientation.VerticalCentroid)
			{
				marker2 = new Marker(base.Pos.XY.X + base.Pos.WH.X, base.Pos.XY.Y, {14091}.Pos.WH.X, {14091}.Pos.WH.Y);
				Marker marker;
				if (this.{14095} == UiOrientation.HorizontalBottom)
				{
					marker = base.Pos;
					base.Pos = new Marker(ref marker, 0f, -Math.Max(0f, wh.Y - base.Pos.WH.Y), base.Pos.WH.X + wh.X, Math.Max(base.Pos.WH.Y, wh.Y));
					{14091}.Pos = marker2.SetY(base.Pos.XY.Y + base.Pos.WH.Y - wh.Y);
					Tlist<UiControl> getChildren = base.GetChildren;
					for (int i = 0; i < getChildren.Size; i++)
					{
						UiControl uiControl = getChildren.Array[i];
						UiControl uiControl2 = uiControl;
						marker = uiControl.Pos;
						uiControl2.Pos = marker.SetY(base.Pos.XY.Y + base.Pos.WH.Y - uiControl.Pos.WH.Y);
					}
					return;
				}
				if (this.{14095} == UiOrientation.HorizontalCentroid)
				{
					marker = base.Pos;
					base.Pos = new Marker(ref marker, 0f, -Math.Max(0f, wh.Y - base.Pos.WH.Y), base.Pos.WH.X + wh.X, Math.Max(base.Pos.WH.Y, wh.Y));
					marker = base.Pos;
					{14091}.Pos = marker2.SetY(marker.Center.Y - wh.Y / 2f);
					Tlist<UiControl> getChildren2 = base.GetChildren;
					for (int j = 0; j < getChildren2.Size; j++)
					{
						UiControl uiControl3 = getChildren2.Array[j];
						UiControl uiControl4 = uiControl3;
						marker = uiControl3.Pos;
						uiControl4.Pos = marker.SetY(base.Pos.Center.Y - uiControl3.Pos.WH.Y / 2f);
					}
					return;
				}
				marker = base.Pos;
				base.Pos = new Marker(ref marker, 0f, 0f, base.Pos.WH.X + wh.X, Math.Max(base.Pos.WH.Y, wh.Y));
			}
			else
			{
				marker2 = new Marker(base.Pos.XY.X, base.Pos.XY.Y + base.Pos.WH.Y, {14091}.Pos.WH.X, {14091}.Pos.WH.Y);
				Marker marker = base.Pos;
				base.Pos = new Marker(ref marker, 0f, 0f, Math.Max(base.Pos.WH.X, wh.X), base.Pos.WH.Y + wh.Y);
				if (this.{14095} != UiOrientation.Vertical)
				{
					marker = base.Pos;
					marker2 = marker2.SetX(marker.Center.X - marker2.WH.X / 2f);
					Tlist<UiControl> getChildren3 = base.GetChildren;
					for (int k = 0; k < getChildren3.Size; k++)
					{
						UiControl uiControl5 = getChildren3.Array[k];
						UiControl uiControl6 = uiControl5;
						marker = uiControl5.Pos;
						uiControl6.Pos = marker.SetX(base.Pos.Center.X - uiControl5.Pos.WH.X / 2f);
					}
				}
			}
			if (this.BorderThickness > 0f)
			{
				marker2 = marker2.Offset(this.BorderThickness, this.BorderThickness);
			}
			{14091}.Pos = marker2;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000179C6 File Offset: 0x00015BC6
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001E481 File Offset: 0x0001C681
		[CompilerGenerated]
		private void {14092}(UiControl {14093})
		{
			if ({14093}.IsDynamicStorage)
			{
				{14093}.EvDynamicStorageStateChanged -= this.{14089};
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001E49D File Offset: 0x0001C69D
		[CompilerGenerated]
		private void {14094}()
		{
			this.{14089}();
		}

		// Token: 0x0400044A RID: 1098
		public Rectangle TexturePath;

		// Token: 0x0400044B RID: 1099
		public float BorderThickness;

		// Token: 0x0400044C RID: 1100
		private UiOrientation {14095};

		// Token: 0x0400044D RID: 1101
		private bool {14096};
	}
}
