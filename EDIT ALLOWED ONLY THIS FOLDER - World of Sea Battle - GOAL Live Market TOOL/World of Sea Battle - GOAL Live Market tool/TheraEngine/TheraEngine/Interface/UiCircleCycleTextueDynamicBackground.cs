using System;
using Microsoft.Xna.Framework;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;

namespace TheraEngine.Interface
{
	// Token: 0x02000097 RID: 151
	public class UiCircleCycleTextueDynamicBackground : UiControl
	{
		// Token: 0x06000388 RID: 904 RVA: 0x00013D2B File Offset: 0x00011F2B
		public UiCircleCycleTextueDynamicBackground(Rectangle {12847}, float {12848}, float {12849}) : base(Marker.OneUnit, PositionAlignment.LeftUp, Color.White)
		{
			this.texturePath = {12847};
			this.{12854} = {12848};
			this.{12855} = {12849};
			this.{12852}();
			base.UseScissor = true;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00013D60 File Offset: 0x00011F60
		internal override void Update(ref FrameTime {12850}, ref int {12851})
		{
			this.{12858} = Geometry.AxisNorm(this.{12858} + {12850}.secElapsed * this.{12855});
			this.{12852}();
			base.Update(ref {12850}, ref {12851});
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00013D90 File Offset: 0x00011F90
		private void {12852}()
		{
			Vector2 vector = Geometry.SubstructRotate(this.{12858}, new Vector2((float)this.texturePath.Width, (float)this.texturePath.Height).Length() * 0.5f * this.{12854});
			Vector2 vector2;
			vector2.X = (float)this.texturePath.X + (float)this.texturePath.Width / 2.6f + vector.X;
			vector2.Y = (float)this.texturePath.Y + (float)this.texturePath.Height / 2.6f + vector.Y;
			this.{12853}.X = (int)vector2.X;
			this.{12853}.Y = (int)vector2.Y;
			this.{12853}.Width = (int)((float)this.texturePath.Width * this.{12854} * 0.5f);
			this.{12853}.Height = (int)((float)this.texturePath.Height * this.{12854} * 0.5f);
			this.{12857} = new Vector2(base.Pos.WH.X / (float)this.{12853}.Width, base.Pos.WH.Y / (float)this.{12853}.Height);
			this.{12856} = -new Vector2((vector2.X - (float)this.{12853}.X) * this.{12857}.X, (vector2.Y - (float)this.{12853}.Y) * this.{12857}.Y);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00013F38 File Offset: 0x00012138
		internal override void Render()
		{
			Rectangle rectangle;
			rectangle.X = (int)(base.Pos.XY.X + this.{12856}.X);
			rectangle.Y = (int)(base.Pos.XY.Y + this.{12856}.Y);
			rectangle.Width = (int)(base.Pos.WH.X + this.{12857}.X);
			rectangle.Height = (int)(base.Pos.WH.Y + this.{12857}.Y);
			Device gs = Engine.GS;
			Color color = this.BasicColor * base.GetOpcaity();
			gs.Draw(this.{12853}, rectangle, color);
			base.Render();
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000E21A File Offset: 0x0000C41A
		protected override bool AllowResize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00014001 File Offset: 0x00012201
		protected internal override void MarkerChanged()
		{
			base.UseScissor = true;
			base.MarkerChanged();
		}

		// Token: 0x040002F0 RID: 752
		public Rectangle texturePath;

		// Token: 0x040002F1 RID: 753
		private Rectangle {12853};

		// Token: 0x040002F2 RID: 754
		private float {12854};

		// Token: 0x040002F3 RID: 755
		private float {12855};

		// Token: 0x040002F4 RID: 756
		private Vector2 {12856};

		// Token: 0x040002F5 RID: 757
		private Vector2 {12857};

		// Token: 0x040002F6 RID: 758
		private float {12858};
	}
}
