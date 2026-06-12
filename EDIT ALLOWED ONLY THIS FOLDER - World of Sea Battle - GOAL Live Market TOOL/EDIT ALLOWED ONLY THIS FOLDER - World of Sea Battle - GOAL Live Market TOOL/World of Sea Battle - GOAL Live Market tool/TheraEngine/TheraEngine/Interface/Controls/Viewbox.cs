using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000D6 RID: 214
	public class Viewbox : ListItemViewControl
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x0001EB10 File Offset: 0x0001CD10
		public Viewbox(Marker {14146}, Rectangle {14147}, Rectangle {14148}, Rectangle {14149}, Rectangle {14150}) : base({14146}, null, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.{14154} = new ScrollBarControl(new Marker({14146}.End.X, {14146}.XY.Y, (float){14147}.Width, {14146}.WH.Y), {14147}, {14148}, {14149}, {14150}, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.SetScrollBar(this.{14154});
			this.{14155} = base.GetParent;
			base.EvRemoveFromContainer += this.{14153};
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001EB94 File Offset: 0x0001CD94
		internal override void Update(ref FrameTime {14151}, ref int {14152})
		{
			if (this.{14155} != base.GetParent || this.{14154}.GetParent == null)
			{
				base.GetParent.AddChild(this.{14154});
				this.{14155} = base.GetParent;
			}
			base.Update(ref {14151}, ref {14152});
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001EBE4 File Offset: 0x0001CDE4
		protected internal override void MarkerChanged()
		{
			this.{14154}.Pos = new Marker(base.Pos.End.X, base.Pos.XY.Y, this.{14154}.Pos.WH.X, base.Pos.WH.Y);
			base.MarkerChanged();
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001EC4F File Offset: 0x0001CE4F
		[CompilerGenerated]
		private void {14153}()
		{
			this.{14154}.RemoveFromContainer();
		}

		// Token: 0x0400045E RID: 1118
		private ScrollBarControl {14154};

		// Token: 0x0400045F RID: 1119
		private UiControl {14155};
	}
}
