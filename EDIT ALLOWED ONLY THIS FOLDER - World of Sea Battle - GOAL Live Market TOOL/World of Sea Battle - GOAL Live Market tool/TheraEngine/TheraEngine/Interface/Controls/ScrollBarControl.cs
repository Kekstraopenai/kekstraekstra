using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using TheraEngine.Grphics.Device;
using TheraEngine.Input;
using TheraEngine.Interface.Eventing;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000D1 RID: 209
	public class ScrollBarControl : UiControl
	{
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060005AC RID: 1452 RVA: 0x0001D600 File Offset: 0x0001B800
		// (remove) Token: 0x060005AD RID: 1453 RVA: 0x0001D638 File Offset: 0x0001B838
		public event Action<ScrollBarChangeEventArgs> EvChange
		{
			[CompilerGenerated]
			add
			{
				Action<ScrollBarChangeEventArgs> action = this.{14064};
				Action<ScrollBarChangeEventArgs> action2;
				do
				{
					action2 = action;
					Action<ScrollBarChangeEventArgs> value2 = (Action<ScrollBarChangeEventArgs>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ScrollBarChangeEventArgs>>(ref this.{14064}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ScrollBarChangeEventArgs> action = this.{14064};
				Action<ScrollBarChangeEventArgs> action2;
				do
				{
					action2 = action;
					Action<ScrollBarChangeEventArgs> value2 = (Action<ScrollBarChangeEventArgs>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ScrollBarChangeEventArgs>>(ref this.{14064}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0001D66D File Offset: 0x0001B86D
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0001D67A File Offset: 0x0001B87A
		public Rectangle Button1TexturePath
		{
			get
			{
				return this.{14067}.TexturePath;
			}
			set
			{
				this.{14067}.TexturePath = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001D688 File Offset: 0x0001B888
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0001D695 File Offset: 0x0001B895
		public Rectangle Button2TexturePath
		{
			get
			{
				return this.{14068}.TexturePath;
			}
			set
			{
				this.{14068}.TexturePath = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001D6A3 File Offset: 0x0001B8A3
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0001D6AB File Offset: 0x0001B8AB
		public float CurrentScrollFactor
		{
			get
			{
				return this.{14065};
			}
			set
			{
				if (this.{14065} != value)
				{
					this.{14065} = MathHelper.Clamp(value, 0f, 1f);
					Action<ScrollBarChangeEventArgs> action = this.{14064};
					if (action == null)
					{
						return;
					}
					action(new ScrollBarChangeEventArgs(this, this.{14065}));
				}
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
		public ScrollBarControl(Marker {14054}, Rectangle {14055}, Rectangle {14056}, Rectangle {14057}, Rectangle {14058}, UiOrientation {14059} = UiOrientation.Vertical, PositionAlignment {14060} = PositionAlignment.LeftUp, PositionAlignment {14061} = PositionAlignment.LeftUp) : base({14054}, {14060}, {14061}, Color.White, false)
		{
			this.PointerPath = {14057};
			this.Direction = {14059};
			this.{14069} = {14058};
			this.{14067} = new Button({14054}.XY, {14055}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{14068} = new Button(({14059} == UiOrientation.Horizontal) ? new Vector2(base.Pos.XY.X + base.Pos.WH.X - (float){14056}.Width, base.Pos.XY.Y) : new Vector2(base.Pos.XY.X, base.Pos.XY.Y + base.Pos.WH.Y - (float){14056}.Height), {14056}, PositionAlignment.RightDown, PositionAlignment.RightDown);
			base.AddChild(new UiControl[]
			{
				this.{14067},
				this.{14068}
			});
			this.{14065} = 0f;
			this.{14066} = false;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001D7F4 File Offset: 0x0001B9F4
		internal override void Update(ref FrameTime {14062}, ref int {14063})
		{
			this.{14066} = (this.{14066} || base.InputMode == MouseInputMode.Down);
			base.Update(ref {14062}, ref {14063});
			if (!InputHelper.NowMouseState.LeftPressed)
			{
				this.{14066} = false;
			}
			if (base.InputMode == MouseInputMode.Down || this.{14066})
			{
				float num2;
				if (this.Direction == UiOrientation.Horizontal)
				{
					float num = this.{14067}.Pos.XY.X + this.{14067}.Pos.WH.X;
					float x = this.{14068}.Pos.XY.X;
					num2 = (MathHelper.Clamp(Engine.GS.MouseToUI.X, num, x) - num) / (x - num);
				}
				else
				{
					float num3 = this.{14067}.Pos.XY.Y + this.{14067}.Pos.WH.Y;
					float y = this.{14068}.Pos.XY.Y;
					num2 = (MathHelper.Clamp(Engine.GS.MouseToUI.Y, num3, y) - num3) / (y - num3);
				}
				if (num2 != this.{14065})
				{
					this.{14065} = num2;
					Action<ScrollBarChangeEventArgs> action = this.{14064};
					if (action == null)
					{
						return;
					}
					action(new ScrollBarChangeEventArgs(this, this.{14065}));
				}
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001D944 File Offset: 0x0001BB44
		internal override void Render()
		{
			float opcaity = base.GetOpcaity();
			Color color = UiControl.ComputeColor(base.GetBrightness(), opcaity, this.BasicColor);
			Rectangle rectangle;
			Color color2;
			if (this.Direction == UiOrientation.Horizontal)
			{
				Device gs = Engine.GS;
				rectangle = new Rectangle((int)(base.Pos.XY.X + 5f), (int)(base.Pos.XY.Y + base.Pos.WH.Y / 2f - 2f), (int)(base.Pos.WH.X - 10f), 4);
				color2 = Color.Black * (0.6f * opcaity);
				gs.Draw(this.{14069}, rectangle, color2);
			}
			Device gs2 = Engine.GS;
			rectangle = base.Pos.ToRect();
			color2 = Color.Black * (0.4f * opcaity * opcaity);
			gs2.Draw(this.{14069}, rectangle, color2);
			base.Render();
			if (this.Direction == UiOrientation.Horizontal)
			{
				float num = (float)this.PointerPath.Width / 2f;
				Vector2 vector;
				vector.X = this.{14067}.Pos.XY.X + this.{14067}.Pos.WH.X;
				vector.Y = base.Pos.XY.Y;
				vector.X += (this.{14068}.Pos.XY.X - (this.{14067}.Pos.XY.X + this.{14067}.Pos.WH.X) - num * 2f) * this.{14065};
				vector.X = (float)((int)vector.X);
				vector.Y = (float)((int)vector.Y);
				Engine.GS.Draw(this.PointerPath, vector, color);
				return;
			}
			float num2 = (float)this.PointerPath.Height / 2f;
			Vector2 vector2;
			vector2.X = base.Pos.XY.X;
			vector2.Y = this.{14067}.Pos.XY.Y + this.{14067}.Pos.WH.Y;
			vector2.Y += (this.{14068}.Pos.XY.Y - (this.{14067}.Pos.XY.Y + this.{14067}.Pos.WH.Y) - num2 * 2f) * this.{14065};
			vector2.X = (float)((int)vector2.X);
			vector2.Y = (float)((int)vector2.Y);
			Engine.GS.Draw(this.PointerPath, vector2, color);
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000DD52 File Offset: 0x0000BF52
		protected override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001DC23 File Offset: 0x0001BE23
		protected override void CleanResources()
		{
			this.{14064} = null;
			base.CleanResources();
		}

		// Token: 0x0400043B RID: 1083
		[CompilerGenerated]
		private Action<ScrollBarChangeEventArgs> {14064};

		// Token: 0x0400043C RID: 1084
		public readonly Rectangle PointerPath;

		// Token: 0x0400043D RID: 1085
		public readonly UiOrientation Direction;

		// Token: 0x0400043E RID: 1086
		private float {14065};

		// Token: 0x0400043F RID: 1087
		private bool {14066};

		// Token: 0x04000440 RID: 1088
		private Button {14067};

		// Token: 0x04000441 RID: 1089
		private Button {14068};

		// Token: 0x04000442 RID: 1090
		private Rectangle {14069};
	}
}
