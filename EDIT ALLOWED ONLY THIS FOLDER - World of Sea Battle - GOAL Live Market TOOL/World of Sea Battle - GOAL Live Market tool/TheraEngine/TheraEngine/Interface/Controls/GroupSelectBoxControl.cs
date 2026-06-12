using System;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Interface.Eventing;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000CC RID: 204
	public class GroupSelectBoxControl<TItem> : UiControl
	{
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600055E RID: 1374 RVA: 0x0001C5AC File Offset: 0x0001A7AC
		// (remove) Token: 0x0600055F RID: 1375 RVA: 0x0001C5E4 File Offset: 0x0001A7E4
		public event Action<SelItem<TItem>> EvChangeItem;

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0001C619 File Offset: 0x0001A819
		public int SelectedIndex
		{
			get
			{
				return this.selectedIndex;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0001C621 File Offset: 0x0001A821
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0001C62C File Offset: 0x0001A82C
		public Rectangle NormalItemTexturePath
		{
			get
			{
				return this.normalItemTexturePath;
			}
			set
			{
				Tlist<UiControl> getChildren = base.GetChildren;
				for (int i = 0; i < getChildren.Size; i++)
				{
					Button button = (Button)getChildren.Array[i];
					if (i != this.selectedIndex)
					{
						button.TexturePath = value;
						button.Pos = new Marker(ref button.Pos.XY, ref value);
					}
				}
				this.normalItemTexturePath = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001C691 File Offset: 0x0001A891
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0001C69C File Offset: 0x0001A89C
		public Rectangle SelectesItemTexturePath
		{
			get
			{
				return this.selectedItemTexturePath;
			}
			set
			{
				if (this.selectedIndex != -1)
				{
					Button button = (Button)base.GetChildren.Array[this.selectedIndex];
					button.TexturePath = value;
					button.Pos = new Marker(ref button.Pos.XY, ref value);
				}
				this.selectedItemTexturePath = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0001C6F1 File Offset: 0x0001A8F1
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0001C6F9 File Offset: 0x0001A8F9
		public UiOrientation SortDirection
		{
			get
			{
				return this.sortDirection;
			}
			set
			{
				if (this.sortDirection != value)
				{
					this.sortDirection = value;
					this.RebuildButtons();
				}
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001C714 File Offset: 0x0001A914
		public GroupSelectBoxControl(Vector2 {13925}, Rectangle {13926}, Rectangle {13927}, SelItem<TItem>[] {13928}, CustomSpriteFont {13929}, UiOrientation {13930} = UiOrientation.Horizontal, PositionAlignment {13931} = PositionAlignment.LeftUp, PositionAlignment {13932} = PositionAlignment.LeftUp, bool {13933} = false)
		{
			Vector2 zero = Vector2.Zero;
			base..ctor(new Marker(ref {13925}, ref zero), {13931}, {13932}, Color.White, false);
			this.IsLeftTextAlignment = {13933};
			this.sortDirection = {13930};
			this.normalItemTexturePath = {13926};
			this.selectedItemTexturePath = {13927};
			this.Font = {13929};
			this.selectedIndex = -1;
			this.variants = new Tlist<SelItem<TItem>>();
			this.SetVariants({13928});
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00016B89 File Offset: 0x00014D89
		internal override void Update(ref FrameTime {13934}, ref int {13935})
		{
			base.Update(ref {13934}, ref {13935});
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001BA77 File Offset: 0x00019C77
		internal override void Render()
		{
			base.Render();
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001C780 File Offset: 0x0001A980
		public void Select(int {13936})
		{
			SelItem<TItem> obj = null;
			if ({13936} == -1)
			{
				if (this.selectedIndex != -1)
				{
					((Button)base.GetChildren.Array[this.selectedIndex]).TexturePath = this.normalItemTexturePath;
				}
			}
			else
			{
				obj = this.variants.Array[{13936}];
				if (this.selectedIndex != -1)
				{
					((Button)base.GetChildren.Array[this.selectedIndex]).TexturePath = this.normalItemTexturePath;
				}
				((Button)base.GetChildren.Array[{13936}]).TexturePath = this.selectedItemTexturePath;
			}
			this.selectedIndex = {13936};
			Action<SelItem<TItem>> evChangeItem = this.EvChangeItem;
			if (evChangeItem == null)
			{
				return;
			}
			evChangeItem(obj);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001C830 File Offset: 0x0001AA30
		public void SelectByItem(TItem {13937})
		{
			for (int i = 0; i < this.variants.Size; i++)
			{
				TItem value = this.variants.Array[i].Value;
				if (value.Equals({13937}))
				{
					this.Select(i);
					return;
				}
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001C884 File Offset: 0x0001AA84
		public void SetVariants(SelItem<TItem>[] {13938})
		{
			int {13936};
			if ({13938}.Length < this.variants.Size || {13938}.Length == 0)
			{
				{13936} = -1;
			}
			else
			{
				{13936} = 0;
			}
			this.variants = new Tlist<SelItem<TItem>>({13938});
			this.RebuildButtons();
			this.Select({13936});
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001C8C8 File Offset: 0x0001AAC8
		public TItem GetSelectedItemOrDefault()
		{
			if (this.selectedIndex != -1)
			{
				return this.variants.Array[this.selectedIndex].Value;
			}
			return default(TItem);
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000DD52 File Offset: 0x0000BF52
		protected override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001C900 File Offset: 0x0001AB00
		private void RebuildButtons()
		{
			Vector2 xy = base.Pos.XY;
			base.ClearAllChild();
			for (int i = 0; i < this.variants.Size; i++)
			{
				SelItem<TItem> selItem = this.variants.Array[i];
				Rectangle rectangle = (i == this.selectedIndex) ? this.selectedItemTexturePath : this.normalItemTexturePath;
				Vector2 vector = this.Font.Measure(selItem.Text);
				Marker marker = new Marker(xy.X, xy.Y, Math.Max(vector.X + 8f, (float)rectangle.Width), (float)rectangle.Height);
				Button button = new Button(marker, rectangle, this.BasicColor, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button.Tag = i;
				button.SetText(selItem.Text, this.Font, Color.White * 0.9f, this.IsLeftTextAlignment);
				button.EvClick += delegate(ClickUiEventArgs {13939})
				{
					this.Select((int){13939}.Sender.Tag);
				};
				base.AddChild(button);
				if (this.sortDirection == UiOrientation.Horizontal)
				{
					xy.X += marker.WH.X;
				}
				else
				{
					xy.Y += marker.WH.Y;
				}
			}
			if (this.sortDirection == UiOrientation.Vertical)
			{
				xy.X += (float)this.selectedItemTexturePath.Width;
			}
			else
			{
				xy.Y += (float)this.selectedItemTexturePath.Height;
			}
			ref Marker ptr = ref base.Pos;
			Vector2 vector2 = xy - base.Pos.XY;
			base.Pos = new Marker(ref ptr.XY, ref vector2);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001CAAA File Offset: 0x0001ACAA
		protected override void CleanResources()
		{
			this.EvChangeItem = null;
			this.variants = null;
			base.CleanResources();
		}

		// Token: 0x04000420 RID: 1056
		public CustomSpriteFont Font;

		// Token: 0x04000421 RID: 1057
		public bool IsLeftTextAlignment;

		// Token: 0x04000422 RID: 1058
		private Tlist<SelItem<TItem>> variants;

		// Token: 0x04000423 RID: 1059
		private int selectedIndex;

		// Token: 0x04000424 RID: 1060
		private Rectangle normalItemTexturePath;

		// Token: 0x04000425 RID: 1061
		private Rectangle selectedItemTexturePath;

		// Token: 0x04000426 RID: 1062
		private UiOrientation sortDirection;
	}
}
