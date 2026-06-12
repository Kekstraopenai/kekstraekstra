using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Graphics;
using TheraEngine.Input;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000D5 RID: 213
	public class UnionFormControl : UiControl
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0001E8BC File Offset: 0x0001CABC
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x0001E8C4 File Offset: 0x0001CAC4
		public ExpandoTexturePath TexturePath
		{
			get
			{
				return this.{14136};
			}
			set
			{
				this.{14136} = value;
				this.{14137} = (value != null);
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001E8D7 File Offset: 0x0001CAD7
		public UnionFormControl(Marker {14127}, ExpandoTexturePath {14128}, Color {14129}, PositionAlignment {14130} = PositionAlignment.LeftUp, PositionAlignment {14131} = PositionAlignment.LeftUp) : base({14127}, {14130}, {14131}, {14129}, false)
		{
			this.TexturePath = {14128};
			this.{14138} = 1f;
			this.AnimatedFocus = true;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001E900 File Offset: 0x0001CB00
		internal override void Update(ref FrameTime {14132}, ref int {14133})
		{
			base.Update(ref {14132}, ref {14133});
			if ((this.AllowDragDrop && base.RenderToDepthMap && base.InputMode == MouseInputMode.Down) || (this.{14139} && InputHelper.NowMouseState.LeftPressed))
			{
				Vector2 vector = Engine.GS.MouseToUI - Engine.GS.MouseToUIPrev;
				Vector2.Add(ref this.{14140}, ref vector, out this.{14140});
				if (this.{14139})
				{
					Vector2 vector2;
					vector2.X = (float)((int)this.{14140}.X);
					this.{14140}.X = this.{14140}.X - vector2.X;
					vector2.Y = (float)((int)this.{14140}.Y);
					this.{14140}.Y = this.{14140}.Y - vector2.Y;
					base.Pos = base.Pos.Offset(vector2);
				}
				else if (this.{14140}.Length() > 3f)
				{
					this.{14140} = Vector2.Zero;
					this.{14139} = true;
				}
			}
			else
			{
				this.{14139} = false;
				this.{14140} = Vector2.Zero;
			}
			this.{14138} = ((base.InputMode == MouseInputMode.NoFocus && this.AnimatedFocus) ? 0.7411f : 1f);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001EA44 File Offset: 0x0001CC44
		internal override void Render()
		{
			if (this.Temp_FixTex != null && Engine.GS.CurrentTexture != this.Temp_FixTex)
			{
				Texture2D currentTexture = Engine.GS.CurrentTexture;
				Engine.GS.SetTexture(this.Temp_FixTex);
				this.{14136}.Render(base.Pos, UiControl.ComputeColor(base.GetBrightness(), base.GetOpcaity(), this.BasicColor * this.{14138}), this.overrideTexture);
				base.Render();
				Engine.GS.SetTexture(currentTexture);
				return;
			}
			this.{14136}.Render(base.Pos, UiControl.ComputeColor(base.GetBrightness(), base.GetOpcaity(), this.BasicColor * this.{14138}), this.overrideTexture);
			base.Render();
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001720D File Offset: 0x0001540D
		public new void AddChild(params UiControl[] {14134})
		{
			base.AddChild({14134});
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001724A File Offset: 0x0001544A
		public void RemoveChild(UiControl {14135})
		{
			base.RemoveAt({14135}, true);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00013C4B File Offset: 0x00011E4B
		public new void ClearAllChild()
		{
			base.ClearAllChild();
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000DD52 File Offset: 0x0000BF52
		protected override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000179C6 File Offset: 0x00015BC6
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x04000453 RID: 1107
		internal Texture2D overrideTexture;

		// Token: 0x04000454 RID: 1108
		private const float c_AddColor_NoFocus = 0.7411f;

		// Token: 0x04000455 RID: 1109
		private const float c_AddColor_Focus = 1f;

		// Token: 0x04000456 RID: 1110
		public bool AnimatedFocus;

		// Token: 0x04000457 RID: 1111
		public bool AllowDragDrop;

		// Token: 0x04000458 RID: 1112
		private ExpandoTexturePath {14136};

		// Token: 0x04000459 RID: 1113
		private bool {14137};

		// Token: 0x0400045A RID: 1114
		private float {14138};

		// Token: 0x0400045B RID: 1115
		private bool {14139};

		// Token: 0x0400045C RID: 1116
		private Vector2 {14140};

		// Token: 0x0400045D RID: 1117
		public Texture2D Temp_FixTex;
	}
}
