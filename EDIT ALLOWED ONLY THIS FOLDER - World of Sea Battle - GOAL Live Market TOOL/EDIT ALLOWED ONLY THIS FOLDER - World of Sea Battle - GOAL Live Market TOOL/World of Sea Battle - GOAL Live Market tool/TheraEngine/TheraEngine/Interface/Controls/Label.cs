using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Content;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000AC RID: 172
	public class Label : UiControl
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x000174DB File Offset: 0x000156DB
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x000174E8 File Offset: 0x000156E8
		public string Text
		{
			get
			{
				return this.{13368}.Value;
			}
			set
			{
				TextEntry textEntry = this.{13368};
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				textEntry.Value = value;
				if (this.{13368}.Font != null)
				{
					this.{13368}.Value = this.{13368}.Font.Validate(this.{13368}.Value);
					this.isMarkerChangedCall = true;
					this.{13362}(true);
					this.isMarkerChangedCall = false;
				}
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00017558 File Offset: 0x00015758
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x00017568 File Offset: 0x00015768
		public CustomSpriteFont Font
		{
			get
			{
				return this.{13368}.Font;
			}
			set
			{
				if (this.{13368}.Font != value)
				{
					this.{13368}.Font = value;
					if (!string.IsNullOrEmpty(this.{13368}.Value))
					{
						this.{13368}.Value = this.{13368}.Font.Validate(this.{13368}.Value);
						this.isMarkerChangedCall = true;
						this.{13362}(true);
						this.isMarkerChangedCall = false;
					}
				}
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000175DC File Offset: 0x000157DC
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x000175E4 File Offset: 0x000157E4
		public float ScaleOfCentr
		{
			get
			{
				return this.{13369};
			}
			set
			{
				this.{13369} = MathHelper.Clamp(value, 0f, 50f);
				ref Marker ptr = ref base.Pos;
				Vector2 vector = this.{13368}.TextSize * this.{13369};
				base.Pos = new Marker(ref ptr.XY, ref vector);
				this.{13364}();
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0001763F File Offset: 0x0001583F
		public Vector2 Size
		{
			get
			{
				return this.{13368}.TextSize;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0001764C File Offset: 0x0001584C
		public LabelTextDecoration Decoration
		{
			get
			{
				return this.{13371};
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00017654 File Offset: 0x00015854
		public Label(float {13335}, float {13336}, CustomSpriteFont {13337}, Color {13338}, string {13339}, PositionAlignment {13340} = PositionAlignment.LeftUp, PositionAlignment {13341} = PositionAlignment.LeftUp) : this(new Vector2((float)((int){13335}), (float)((int){13336})), {13337}, {13338}, {13339}, {13340}, {13341})
		{
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00017670 File Offset: 0x00015870
		public Label(Vector2 {13342}, CustomSpriteFont {13343}, Color {13344}, string {13345}, PositionAlignment {13346} = PositionAlignment.LeftUp, PositionAlignment {13347} = PositionAlignment.LeftUp)
		{
			Vector2 zero = Vector2.Zero;
			base..ctor(new Marker(ref {13342}, ref zero), {13346}, {13347}, {13344}, false);
			this.{13369} = 1f;
			this.{13368} = new TextEntry();
			this.{13368}.Value = {13343}.Validate({13345});
			this.{13368}.Font = {13343};
			this.isMarkerChangedCall = true;
			this.{13362}(true);
			this.isMarkerChangedCall = false;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000176E4 File Offset: 0x000158E4
		public Label(Vector2 {13348}, float {13349}, CustomSpriteFont[] {13350}, CustomSpriteFont {13351}, Color {13352}, string {13353}, PositionAlignment {13354} = PositionAlignment.LeftUp, PositionAlignment {13355} = PositionAlignment.LeftUp) : this({13348}, {13351}, {13352}, {13353}, {13354}, {13355})
		{
			int num = Array.FindIndex<CustomSpriteFont>({13350}, (CustomSpriteFont {13374}) => {13374} == {13351});
			if (num == -1)
			{
				num = {13350}.Length - 1;
			}
			for (int i = num; i >= 0; i--)
			{
				this.Font = {13350}[i];
				this.{13362}(true);
				if (this.{13368}.TextSize.X <= {13349})
				{
					return;
				}
			}
			this.Font = {13350}[0];
			this.{13362}(true);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00016B89 File Offset: 0x00014D89
		internal override void Update(ref FrameTime {13356}, ref int {13357})
		{
			base.Update(ref {13356}, ref {13357});
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00017774 File Offset: 0x00015974
		internal override void Render()
		{
			if (this.{13368}.TextHasValue)
			{
				Color {13367} = UiControl.ComputeColor(base.GetBrightness(), base.GetOpcaity(), this.BasicColor);
				Engine.GS.SetFont(this.{13368}.Font);
				if (this.Shadowed)
				{
					Engine.GS.DrawStringShadowed(this.{13368}.Value, this.{13370}, {13367});
				}
				else
				{
					Engine.GS.DrawString(this.{13368}.Value, this.{13370}, {13367}, 0f, Vector2.Zero, this.{13369});
				}
				this.{13365}(this.{13371}, {13367});
			}
			base.Render();
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00017828 File Offset: 0x00015A28
		public Label Center()
		{
			Vector2 vector = base.Pos.XY - base.Pos.WH / 2f;
			base.Pos = new Marker(ref vector, ref base.Pos.WH);
			return this;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00017878 File Offset: 0x00015A78
		public Label CenterX()
		{
			Vector2 vector = base.Pos.XY - new Vector2(base.Pos.WH.X / 2f, 0f);
			base.Pos = new Marker(ref vector, ref base.Pos.WH);
			return this;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000178D2 File Offset: 0x00015AD2
		public Label SetStrikethroughDecoration(Rectangle {13358}, Texture2D {13359})
		{
			this.{13372} = {13358};
			this.{13371} = LabelTextDecoration.Strikethrough;
			this.{13373} = {13359};
			return this;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000178EA File Offset: 0x00015AEA
		public Label SetUnderlineDecoration(Rectangle {13360}, Texture2D {13361})
		{
			this.{13372} = {13360};
			this.{13371} = LabelTextDecoration.Underline;
			this.{13373} = {13361};
			return this;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000E21A File Offset: 0x0000C41A
		protected override bool AllowResize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00017902 File Offset: 0x00015B02
		protected internal override void MarkerChanged()
		{
			base.MarkerChanged();
			this.{13362}(false);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00017914 File Offset: 0x00015B14
		private void {13362}(bool {13363})
		{
			if ({13363})
			{
				this.{13368}.Include(this.{13368}.Value, this.{13368}.Font);
			}
			ref Marker ptr = ref base.Pos;
			Vector2 vector = this.{13368}.TextSize * this.{13369};
			base.Pos = new Marker(ref ptr.XY, ref vector);
			this.{13364}();
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00017980 File Offset: 0x00015B80
		private void {13364}()
		{
			this.{13370} = base.Pos.Center - this.{13368}.TextSize * this.{13369} / 2f;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000179C6 File Offset: 0x00015BC6
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000179D0 File Offset: 0x00015BD0
		[CompilerGenerated]
		private void {13365}(LabelTextDecoration {13366}, Color {13367})
		{
			if ({13366} == LabelTextDecoration.Strikethrough || {13366} == LabelTextDecoration.Underline)
			{
				int num = (this.Font.MaxCharSize.Y >= 22f) ? 2 : 1;
				if ({13366} == LabelTextDecoration.Strikethrough)
				{
					Engine.GS.Line2D(this.{13373}, this.{13372}, this.{13370} + new Vector2(-2f, this.{13368}.TextSize.Y / 2f - (float)(num / 2) - 1f), this.{13370} + new Vector2(this.{13368}.TextSize.X + 2f, this.{13368}.TextSize.Y / 2f - (float)(num / 2) - 1f), {13367}, num);
					return;
				}
				if ({13366} == LabelTextDecoration.Underline)
				{
					Engine.GS.Line2D(this.{13373}, this.{13372}, this.{13370} + new Vector2(-2f, this.{13368}.TextSize.Y + 1f), this.{13370} + new Vector2(this.{13368}.TextSize.X + 2f, this.{13368}.TextSize.Y + 1f), {13367}, num);
				}
			}
		}

		// Token: 0x04000369 RID: 873
		public bool Shadowed;

		// Token: 0x0400036A RID: 874
		private TextEntry {13368};

		// Token: 0x0400036B RID: 875
		private float {13369};

		// Token: 0x0400036C RID: 876
		private Vector2 {13370};

		// Token: 0x0400036D RID: 877
		private LabelTextDecoration {13371};

		// Token: 0x0400036E RID: 878
		private Rectangle {13372};

		// Token: 0x0400036F RID: 879
		private Texture2D {13373};
	}
}
