using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using TheraEngine.Input;
using TheraEngine.Interface.Eventing;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000B7 RID: 183
	public class ProgressSelectBar : ProgressBar
	{
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060004B1 RID: 1201 RVA: 0x00018334 File Offset: 0x00016534
		// (remove) Token: 0x060004B2 RID: 1202 RVA: 0x0001836C File Offset: 0x0001656C
		public event Action<ProgressBarChangeEventArgs> EvChange
		{
			[CompilerGenerated]
			add
			{
				Action<ProgressBarChangeEventArgs> action = this.{13541};
				Action<ProgressBarChangeEventArgs> action2;
				do
				{
					action2 = action;
					Action<ProgressBarChangeEventArgs> value2 = (Action<ProgressBarChangeEventArgs>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ProgressBarChangeEventArgs>>(ref this.{13541}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ProgressBarChangeEventArgs> action = this.{13541};
				Action<ProgressBarChangeEventArgs> action2;
				do
				{
					action2 = action;
					Action<ProgressBarChangeEventArgs> value2 = (Action<ProgressBarChangeEventArgs>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ProgressBarChangeEventArgs>>(ref this.{13541}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x000183A1 File Offset: 0x000165A1
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x000183A9 File Offset: 0x000165A9
		public override float MaxValue
		{
			get
			{
				return base.MaxValue;
			}
			set
			{
				base.MaxValue = value;
				Action<ProgressBarChangeEventArgs> action = this.{13541};
				if (action == null)
				{
					return;
				}
				action(new ProgressBarChangeEventArgs(this, this.Value / this.MaxValue));
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000183D5 File Offset: 0x000165D5
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x000183DD File Offset: 0x000165DD
		public override float Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
				Action<ProgressBarChangeEventArgs> action = this.{13541};
				if (action == null)
				{
					return;
				}
				action(new ProgressBarChangeEventArgs(this, this.Value / this.MaxValue));
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00018409 File Offset: 0x00016609
		internal bool Triggered
		{
			get
			{
				return this.{13543};
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00018411 File Offset: 0x00016611
		public ProgressSelectBar(Marker {13522}, Rectangle {13523}, Rectangle {13524}, Rectangle {13525}, Color {13526}, PositionAlignment {13527} = PositionAlignment.LeftUp, PositionAlignment {13528} = PositionAlignment.LeftUp) : base({13522}, {13523}, {13524}, {13526}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.PointerTexturePath = {13525};
			this.RebuildProperties();
			base.EvLostMouseFocus += this.{13540};
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00018440 File Offset: 0x00016640
		public ProgressSelectBar(Vector2 {13529}, Rectangle {13530}, Rectangle {13531}, Rectangle {13532}, PositionAlignment {13533} = PositionAlignment.LeftUp, PositionAlignment {13534} = PositionAlignment.LeftUp) : this(new Marker(ref {13529}, ref {13530}), {13530}, {13531}, {13532}, Color.White, {13533}, {13534})
		{
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00018460 File Offset: 0x00016660
		internal override void Update(ref FrameTime {13535}, ref int {13536})
		{
			this.{13543} = (this.{13543} || base.InputMode == MouseInputMode.Down);
			base.Update(ref {13535}, ref {13536});
			if (!InputHelper.NowMouseState.LeftPressed)
			{
				this.{13543} = false;
			}
			if (base.InputMode == MouseInputMode.Down || this.{13543})
			{
				float num = MathHelper.Clamp(Engine.GS.MouseToUI.X, base.Pos.XY.X, base.Pos.XY.X + base.Pos.WH.X) - base.Pos.XY.X;
				num /= base.Pos.WH.X;
				if (this.NumberOfSteps >= 2)
				{
					num = MathF.Round(num * (float)(this.NumberOfSteps - 1)) / (float)(this.NumberOfSteps - 1);
				}
				this.Value = this.MaxValue * num;
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00018554 File Offset: 0x00016754
		internal override void Render()
		{
			base.Render();
			Color color = UiControl.ComputeColor(base.GetBrightness(), base.GetOpcaity(), this.BasicColor);
			Engine.GS.Draw(this.PointerTexturePath, this.{13542}, color);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00018598 File Offset: 0x00016798
		protected override void RebuildProperties()
		{
			base.RebuildProperties();
			this.{13542} = new Vector2((float)((int)(base.Pos.XY.X + (base.Pos.WH.X - (float)this.PointerTexturePath.Width) * (this.Value / this.MaxValue))), (float)((int)(base.Pos.XY.Y + base.Pos.WH.Y / 2f - (float)this.PointerTexturePath.Height / 2f)));
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00018630 File Offset: 0x00016830
		protected override void CleanResources()
		{
			this.{13541} = null;
			base.CleanResources();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001863F File Offset: 0x0001683F
		public ProgressSelectBar ExpansionSetVals(float {13537}, float {13538})
		{
			this.MaxValue = {13538};
			this.Value = {13537};
			return this;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00018650 File Offset: 0x00016850
		public ProgressSelectBar ExpansionChangeEvent(Action<ProgressBarChangeEventArgs> {13539})
		{
			this.EvChange += {13539};
			return this;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001865C File Offset: 0x0001685C
		[CompilerGenerated]
		private void {13540}()
		{
			if (InputHelper.NowMouseState.LeftPressed)
			{
				if (Engine.GS.MouseToUI.X <= base.Pos.XY.X)
				{
					this.Value = 0f;
					return;
				}
				if (Engine.GS.MouseToUI.X >= base.Pos.XY.X + base.Pos.WH.X)
				{
					this.Value = this.MaxValue;
				}
			}
		}

		// Token: 0x04000396 RID: 918
		[CompilerGenerated]
		private Action<ProgressBarChangeEventArgs> {13541};

		// Token: 0x04000397 RID: 919
		public Rectangle PointerTexturePath;

		// Token: 0x04000398 RID: 920
		public int NumberOfSteps;

		// Token: 0x04000399 RID: 921
		private Vector2 {13542};

		// Token: 0x0400039A RID: 922
		private bool {13543};
	}
}
