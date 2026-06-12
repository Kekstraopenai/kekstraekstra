using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000B5 RID: 181
	public class PointedProgressBar : UiControl
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00017DA8 File Offset: 0x00015FA8
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x00017DB0 File Offset: 0x00015FB0
		public int MaxValue
		{
			get
			{
				return this.{13455};
			}
			set
			{
				if (this.{13455} != value)
				{
					this.{13455} = Math.Max(value, 0);
					if (this.{13456} > this.{13455})
					{
						this.{13456} = this.{13455};
					}
					this.RebuildProperties();
				}
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00017DE8 File Offset: 0x00015FE8
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x00017DF0 File Offset: 0x00015FF0
		public int Value
		{
			get
			{
				return this.{13456};
			}
			set
			{
				if (this.{13456} != value)
				{
					this.{13456} = (int)MathHelper.Clamp((float)value, 0f, (float)this.{13455});
					this.RebuildProperties();
				}
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00017E1B File Offset: 0x0001601B
		public PointedProgressBar(Vector2 {13448}, Rectangle {13449}, Rectangle {13450}, PositionAlignment {13451} = PositionAlignment.LeftUp, PositionAlignment {13452} = PositionAlignment.LeftUp) : base(new Marker(ref {13448}, ref {13449}), {13451}, {13452}, Color.White, false)
		{
			this.FilledPoint = {13449};
			this.UnfilledPoint = {13450};
			this.MaxValue = 10;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00016B89 File Offset: 0x00014D89
		internal override void Update(ref FrameTime {13453}, ref int {13454})
		{
			base.Update(ref {13453}, ref {13454});
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00017E4C File Offset: 0x0001604C
		internal override void Render()
		{
			Color color = UiControl.ComputeColor(base.GetBrightness(), base.GetOpcaity(), this.BasicColor);
			Vector2 xy = base.Pos.XY;
			for (int i = 0; i < this.{13456}; i++)
			{
				Engine.GS.Draw(this.FilledPoint, xy, color);
				xy.X += (float)this.FilledPoint.Width;
			}
			for (int j = 0; j < this.{13455} - this.{13456}; j++)
			{
				Engine.GS.Draw(this.UnfilledPoint, xy, color);
				xy.X += (float)this.FilledPoint.Width;
			}
			base.Render();
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000DD52 File Offset: 0x0000BF52
		protected override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00017F02 File Offset: 0x00016102
		protected internal override void MarkerChanged()
		{
			this.RebuildProperties();
			base.MarkerChanged();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00017F10 File Offset: 0x00016110
		protected virtual void RebuildProperties()
		{
			base.Pos = base.Pos.SetWidth((float)(this.{13455} * this.UnfilledPoint.Width));
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000179C6 File Offset: 0x00015BC6
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x04000385 RID: 901
		public Rectangle FilledPoint;

		// Token: 0x04000386 RID: 902
		public Rectangle UnfilledPoint;

		// Token: 0x04000387 RID: 903
		private int {13455};

		// Token: 0x04000388 RID: 904
		private int {13456};

		// Token: 0x04000389 RID: 905
		private Rectangle {13457};

		// Token: 0x0400038A RID: 906
		private Rectangle {13458};

		// Token: 0x0400038B RID: 907
		private Rectangle {13459};

		// Token: 0x0400038C RID: 908
		private Rectangle {13460};
	}
}
