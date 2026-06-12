using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface.Eventing;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000D4 RID: 212
	public class Tab : UiControl
	{
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060005CC RID: 1484 RVA: 0x0001E4A8 File Offset: 0x0001C6A8
		// (remove) Token: 0x060005CD RID: 1485 RVA: 0x0001E4E0 File Offset: 0x0001C6E0
		public event Action<ChangeTabFormEventArgs> EvChangeForm
		{
			[CompilerGenerated]
			add
			{
				Action<ChangeTabFormEventArgs> action = this.{14117};
				Action<ChangeTabFormEventArgs> action2;
				do
				{
					action2 = action;
					Action<ChangeTabFormEventArgs> value2 = (Action<ChangeTabFormEventArgs>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ChangeTabFormEventArgs>>(ref this.{14117}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ChangeTabFormEventArgs> action = this.{14117};
				Action<ChangeTabFormEventArgs> action2;
				do
				{
					action2 = action;
					Action<ChangeTabFormEventArgs> value2 = (Action<ChangeTabFormEventArgs>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ChangeTabFormEventArgs>>(ref this.{14117}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0001E515 File Offset: 0x0001C715
		public int SelectedIndex
		{
			get
			{
				return this.{14119};
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001E51D File Offset: 0x0001C71D
		public int GetPagesCount
		{
			get
			{
				return this.{14118}.Size;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0001E52A File Offset: 0x0001C72A
		public Form SelectedForm
		{
			get
			{
				if (this.{14119} != -1)
				{
					return this.{14118}.Array[this.{14119}];
				}
				return null;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0001E549 File Offset: 0x0001C749
		public Tlist<Form> GetForms
		{
			get
			{
				return this.{14118};
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001E551 File Offset: 0x0001C751
		public Tab(Marker {14103}, Rectangle {14104}, PositionAlignment {14105} = PositionAlignment.LeftUp, PositionAlignment {14106} = PositionAlignment.LeftUp) : base({14103}, {14105}, {14106}, Color.White, false)
		{
			this.BackgroundTexturePath = {14104};
			this.{14118} = new Tlist<Form>(10);
			this.{14119} = -1;
			this.{14120} = true;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00016B89 File Offset: 0x00014D89
		internal override void Update(ref FrameTime {14107}, ref int {14108})
		{
			base.Update(ref {14107}, ref {14108});
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001E588 File Offset: 0x0001C788
		internal override void Render()
		{
			if (this.BackgroundTexturePath != Rectangle.Empty)
			{
				Device gs = Engine.GS;
				Rectangle rectangle = base.Pos.ToRect();
				Color color = UiControl.ComputeColor(base.GetBrightness(), base.GetOpcaity(), this.BasicColor);
				gs.Draw(this.BackgroundTexturePath, rectangle, color);
			}
			base.Render();
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001E5E8 File Offset: 0x0001C7E8
		public void Add(params Form[] {14109})
		{
			foreach (Form form in {14109})
			{
				if (form.GetParent != null)
				{
					form.RemoveFromContainer();
				}
				else
				{
					form.ShutdownFocus();
				}
				form.Pos = base.Pos;
				this.{14118}.Add(form);
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001E636 File Offset: 0x0001C836
		public void AddAndSelect(Form {14110})
		{
			this.Add(new Form[]
			{
				{14110}
			});
			this.Select(this.{14118}.Size - 1);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001E65C File Offset: 0x0001C85C
		public void RemoveAt(int {14111})
		{
			if ({14111} == -1)
			{
				return;
			}
			if ({14111} == this.{14119})
			{
				base.ClearAllChild();
			}
			else
			{
				this.{14118}.Array[this.{14119}].ShutdownFocus();
			}
			this.{14118}.RemoveAt(this.{14119});
			if (this.{14118}.Size == 0)
			{
				this.{14119} = -1;
				return;
			}
			this.Select(Math.Max(this.{14119} - 1, 0));
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001E6D0 File Offset: 0x0001C8D0
		public void RemoveAt(Form {14112})
		{
			if ({14112} == null)
			{
				throw new ArgumentNullException();
			}
			this.RemoveAt(this.{14118}.IndexOf({14112}));
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001E6EE File Offset: 0x0001C8EE
		public void Clear()
		{
			this.{14115}();
			this.{14118}.Clear();
			base.ClearAllChild();
			this.{14119} = -1;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001E710 File Offset: 0x0001C910
		public void Select(int {14113})
		{
			if (this.{14119} != {14113})
			{
				Form selectedForm = this.SelectedForm;
				base.ClearAllChild();
				if ({14113} != -1)
				{
					this.{14118}.Array[{14113}].PositionAlignment_X = PositionAlignment.Both;
					this.{14118}.Array[{14113}].PositionAlignment_Y = PositionAlignment.Both;
					this.{14118}.Array[{14113}].Pos = base.Pos;
					base.AddChild(this.{14118}.Array[{14113}]);
				}
				this.{14119} = {14113};
				Action<ChangeTabFormEventArgs> action = this.{14117};
				if (action == null)
				{
					return;
				}
				action(new ChangeTabFormEventArgs(this, this.SelectedForm, selectedForm, {14113}));
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001E7B0 File Offset: 0x0001C9B0
		public void Select(Form {14114})
		{
			if ({14114} == null)
			{
				throw new ArgumentNullException();
			}
			for (int i = 0; i < this.{14118}.Size; i++)
			{
				if (this.{14118}.Array[i] == {14114})
				{
					this.Select(i);
					return;
				}
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001E7F4 File Offset: 0x0001C9F4
		public void NextToCycle()
		{
			if (this.{14119} < this.{14118}.Size - 1)
			{
				this.Select(this.{14119} + 1);
				return;
			}
			this.Select(0);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001E821 File Offset: 0x0001CA21
		public void BackToCycle()
		{
			if (this.{14119} > 0)
			{
				this.Select(this.{14119} - 1);
				return;
			}
			this.Select(this.GetPagesCount - 1);
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000DD52 File Offset: 0x0000BF52
		protected override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001E849 File Offset: 0x0001CA49
		internal override void ShutdownFocus()
		{
			base.ShutdownFocus();
			this.{14115}();
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001E858 File Offset: 0x0001CA58
		private void {14115}()
		{
			if (this.{14120})
			{
				for (int i = 0; i < this.{14118}.Size; i++)
				{
					if (i != this.{14119})
					{
						this.{14118}.Array[i].ShutdownFocus();
					}
				}
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001E89E File Offset: 0x0001CA9E
		public Form GetPage(int {14116})
		{
			return this.{14118}.Array[{14116}];
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001E8AD File Offset: 0x0001CAAD
		protected override void CleanResources()
		{
			this.{14117} = null;
			base.CleanResources();
		}

		// Token: 0x0400044E RID: 1102
		[CompilerGenerated]
		private Action<ChangeTabFormEventArgs> {14117};

		// Token: 0x0400044F RID: 1103
		public Rectangle BackgroundTexturePath;

		// Token: 0x04000450 RID: 1104
		private Tlist<Form> {14118};

		// Token: 0x04000451 RID: 1105
		private int {14119};

		// Token: 0x04000452 RID: 1106
		private bool {14120};
	}
}
