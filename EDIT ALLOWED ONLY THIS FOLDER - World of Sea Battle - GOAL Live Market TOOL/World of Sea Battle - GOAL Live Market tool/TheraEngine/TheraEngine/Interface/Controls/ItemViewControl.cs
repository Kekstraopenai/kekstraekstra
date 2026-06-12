using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000CD RID: 205
	public class ItemViewControl : UiControl
	{
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000572 RID: 1394 RVA: 0x0001CAD8 File Offset: 0x0001ACD8
		// (remove) Token: 0x06000573 RID: 1395 RVA: 0x0001CB10 File Offset: 0x0001AD10
		public event Action EvChangePage
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{13959};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{13959}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{13959};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{13959}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0001CB45 File Offset: 0x0001AD45
		public int GetItemsCount
		{
			get
			{
				return this.{13963}.Size;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001CB52 File Offset: 0x0001AD52
		public int GetPagesCount
		{
			get
			{
				return this.{13960}.GetPagesCount;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0001CB5F File Offset: 0x0001AD5F
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x0001CB6C File Offset: 0x0001AD6C
		public Rectangle BackTexturePath
		{
			get
			{
				return this.{13960}.BackgroundTexturePath;
			}
			set
			{
				this.{13960}.BackgroundTexturePath = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001CB7A File Offset: 0x0001AD7A
		public int GetCurrentPageIndex
		{
			get
			{
				return this.{13962};
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001CB84 File Offset: 0x0001AD84
		public ItemViewControl(Marker {13947}, Rectangle {13948}, PositionAlignment {13949} = PositionAlignment.LeftUp, PositionAlignment {13950} = PositionAlignment.LeftUp) : base({13947}, {13949}, {13950}, Color.White, false)
		{
			this.{13962} = -1;
			this.{13961} = 0f;
			this.{13963} = new Tlist<Form>();
			this.{13960} = new Tab({13947}, {13948}, PositionAlignment.Both, PositionAlignment.Both);
			base.AddChild(this.{13960});
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00016B89 File Offset: 0x00014D89
		internal override void Update(ref FrameTime {13951}, ref int {13952})
		{
			base.Update(ref {13951}, ref {13952});
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001BA77 File Offset: 0x00019C77
		internal override void Render()
		{
			base.Render();
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001CBDC File Offset: 0x0001ADDC
		public void AddItem(params Form[] {13953})
		{
			if ({13953} == null)
			{
				throw new ArgumentNullException("item");
			}
			int num = {13953}.Length;
			for (int i = 0; i < num; i++)
			{
				Form form = {13953}[i];
				if (form.Pos.WH.Y > base.Pos.WH.Y)
				{
					form.Pos = new Marker(form.Pos.XY.X, form.Pos.XY.Y, form.Pos.WH.X, base.Pos.WH.Y);
				}
				form.PositionAlignment_X = PositionAlignment.LeftUp;
				form.PositionAlignment_Y = PositionAlignment.LeftUp;
				this.{13963}.Add(form);
				this.{13961} += form.Pos.WH.Y;
				bool flag = this.{13960}.GetPagesCount == 0;
				if (this.{13961} > base.Pos.WH.Y || flag)
				{
					this.{13961} = 0f;
					this.{13960}.Add(new Form[]
					{
						new Form(base.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					if (flag)
					{
						this.{13960}.Select(this.{13960}.GetPagesCount - 1);
					}
				}
				Vector2 vector = new Vector2(base.Pos.XY.X, base.Pos.XY.Y + this.{13961});
				form.Pos = new Marker(ref vector, ref form.Pos.WH);
				this.{13960}.GetForms.Array[this.{13960}.GetPagesCount - 1].AddChild(form);
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001CD96 File Offset: 0x0001AF96
		public void RemoveItem(int {13954})
		{
			this.{13963}.RemoveAt({13954});
			this.{13958}();
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001CDAC File Offset: 0x0001AFAC
		public void RemoveItem(Form {13955})
		{
			int num = this.{13963}.IndexOf({13955});
			if (num == -1)
			{
				throw new InvalidOperationException();
			}
			this.RemoveItem(num);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001CDD8 File Offset: 0x0001AFD8
		public void ClearItems()
		{
			this.{13962} = -1;
			this.{13963}.Clear();
			this.{13960}.Clear();
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001CDF7 File Offset: 0x0001AFF7
		public void SelectPage(int {13956})
		{
			if ({13956} != this.{13962})
			{
				this.{13960}.Select({13956});
				this.{13962} = {13956};
				Action action = this.{13959};
				if (action == null)
				{
					return;
				}
				action();
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001CE28 File Offset: 0x0001B028
		public void SelectPageFromItem(Form {13957})
		{
			Tlist<UiControl> getChildren = this.{13960}.GetChildren;
			for (int i = 0; i < getChildren.Size; i++)
			{
				Tlist<UiControl> getChildren2 = getChildren.Array[i].GetChildren;
				int j = 0;
				while (j < getChildren2.Size)
				{
					if (getChildren2.Array[i] == {13957})
					{
						this.SelectPage(i);
						return;
					}
					i++;
				}
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001CE84 File Offset: 0x0001B084
		public void NextPageToEnd()
		{
			if (this.{13960}.SelectedIndex + 1 < this.{13960}.GetPagesCount)
			{
				this.SelectPage(this.{13960}.SelectedIndex + 1);
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001CEB3 File Offset: 0x0001B0B3
		public void BackPageToEnd()
		{
			if (this.{13960}.GetPagesCount > 0 && this.{13960}.SelectedIndex > 0)
			{
				this.SelectPage(this.{13960}.SelectedIndex - 1);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0000DD52 File Offset: 0x0000BF52
		protected override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001CEE4 File Offset: 0x0001B0E4
		protected internal override void MarkerChanged()
		{
			if (base.PositionAlignment_X == PositionAlignment.Both || base.PositionAlignment_Y == PositionAlignment.Both)
			{
				this.{13958}();
			}
			base.MarkerChanged();
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001CF04 File Offset: 0x0001B104
		private void {13958}()
		{
			Form[] array = this.{13963}.ToArray();
			this.ClearItems();
			for (int i = 0; i < array.Length; i++)
			{
				this.AddItem(new Form[]
				{
					array[i]
				});
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001CF43 File Offset: 0x0001B143
		protected override void CleanResources()
		{
			this.{13959} = null;
			this.{13963} = null;
			base.CleanResources();
		}

		// Token: 0x04000427 RID: 1063
		[CompilerGenerated]
		private Action {13959};

		// Token: 0x04000428 RID: 1064
		private Tab {13960};

		// Token: 0x04000429 RID: 1065
		private float {13961};

		// Token: 0x0400042A RID: 1066
		private int {13962};

		// Token: 0x0400042B RID: 1067
		private Tlist<Form> {13963};
	}
}
