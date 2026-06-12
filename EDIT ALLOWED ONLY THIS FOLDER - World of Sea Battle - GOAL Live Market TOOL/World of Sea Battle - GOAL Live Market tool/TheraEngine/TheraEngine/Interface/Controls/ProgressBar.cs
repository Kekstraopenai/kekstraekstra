using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine.Grphics.Device;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000B6 RID: 182
	public class ProgressBar : UiControl
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00017F44 File Offset: 0x00016144
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00017F4C File Offset: 0x0001614C
		public virtual float MaxValue
		{
			get
			{
				return this.{13499};
			}
			set
			{
				if (value == 0f && Debugger.IsAttached)
				{
					throw new ArgumentException("ProgressBarControl.MaxValue got 0");
				}
				if (this.{13499} != value)
				{
					this.{13499} = MathHelper.Max(value, 0f);
					if (this.{13500} > this.{13499} && !this.CanOverload)
					{
						this.{13500} = this.{13499};
					}
					this.RebuildProperties();
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00017FB5 File Offset: 0x000161B5
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x00017FC0 File Offset: 0x000161C0
		public virtual float Value
		{
			get
			{
				return this.{13500};
			}
			set
			{
				if (this.{13500} != value)
				{
					if (this.CanOverload)
					{
						this.{13500} = Math.Max(value, 0f);
					}
					else
					{
						this.{13500} = MathHelper.Clamp(value, 0f, this.{13499});
					}
					this.RebuildProperties();
				}
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0001800E File Offset: 0x0001620E
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x00018016 File Offset: 0x00016216
		public bool CanOverload { get; set; }

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001801F File Offset: 0x0001621F
		public ProgressBar(Vector2 {13480}, Rectangle {13481}, Rectangle {13482}, Color {13483}, PositionAlignment {13484} = PositionAlignment.LeftUp, PositionAlignment {13485} = PositionAlignment.LeftUp) : base(new Marker(ref {13480}, ref {13481}), {13484}, {13485}, {13483}, false)
		{
			this.ActiveTexturePath = {13481};
			this.PassiveTexturePath = {13482};
			this.MaxValue = 100f;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00018050 File Offset: 0x00016250
		public ProgressBar(Marker {13486}, Rectangle {13487}, Rectangle {13488}, Color {13489}, PositionAlignment {13490} = PositionAlignment.LeftUp, PositionAlignment {13491} = PositionAlignment.LeftUp) : base({13486}, {13490}, {13491}, {13489}, false)
		{
			this.ActiveTexturePath = {13487};
			this.PassiveTexturePath = {13488};
			this.MaxValue = 100f;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00018079 File Offset: 0x00016279
		public ProgressBar(Vector2 {13492}, Rectangle {13493}, PositionAlignment {13494} = PositionAlignment.LeftUp, PositionAlignment {13495} = PositionAlignment.LeftUp) : this(new Marker(ref {13492}, ref {13493}), {13493}, Rectangle.Empty, Color.White, {13494}, {13495})
		{
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00016B89 File Offset: 0x00014D89
		internal override void Update(ref FrameTime {13496}, ref int {13497})
		{
			base.Update(ref {13496}, ref {13497});
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00018098 File Offset: 0x00016298
		internal override void Render()
		{
			Color color = UiControl.ComputeColor(base.GetBrightness(), base.GetOpcaity(), this.BasicColor);
			Color color2 = UiControl.ComputeColor(base.GetBrightness(), base.GetOpcaity(), Color.White);
			if (this.{13499} != 0f)
			{
				Engine.GS.Draw(this.{13503}, this.{13501}, color);
				if (this.PassiveTexturePath == Rectangle.Empty)
				{
					Device gs = Engine.GS;
					Color gray = Color.Gray;
					gs.Draw(this.{13504}, this.{13502}, gray);
				}
				else
				{
					Engine.GS.Draw(this.{13504}, this.{13502}, color2);
				}
			}
			base.Render();
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000DD52 File Offset: 0x0000BF52
		protected override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00018149 File Offset: 0x00016349
		protected internal override void MarkerChanged()
		{
			this.RebuildProperties();
			base.MarkerChanged();
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00018158 File Offset: 0x00016358
		protected virtual void RebuildProperties()
		{
			float num = Math.Min(1f, this.{13500} / this.{13499});
			Vector4 vector = new Vector4(base.Pos.XY.X, base.Pos.XY.Y, base.Pos.WH.X * num, base.Pos.WH.Y);
			Vector4 vector2 = new Vector4(vector.X + vector.Z, vector.Y, base.Pos.WH.X * (1f - num), vector.W);
			Rectangle rectangle = (this.PassiveTexturePath == Rectangle.Empty) ? this.ActiveTexturePath : this.PassiveTexturePath;
			Vector4 vector3 = new Vector4((float)this.ActiveTexturePath.X, (float)this.ActiveTexturePath.Y, (float)this.ActiveTexturePath.Width * num, (float)this.ActiveTexturePath.Height);
			Vector4 vector4 = new Vector4((float)rectangle.X + (float)rectangle.Width * num, (float)rectangle.Y, (float)rectangle.Width * (1f - num), (float)rectangle.Height);
			this.{13501} = new Rectangle((int)vector.X, (int)vector.Y, (int)vector.Z, (int)vector.W);
			this.{13502} = new Rectangle((int)vector2.X, (int)vector2.Y, (int)vector2.Z, (int)vector2.W);
			this.{13503} = new Rectangle((int)vector3.X, (int)vector3.Y, (int)vector3.Z, (int)vector3.W);
			this.{13504} = new Rectangle((int)vector4.X, (int)vector4.Y, (int)vector4.Z, (int)vector4.W);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000179C6 File Offset: 0x00015BC6
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x0400038D RID: 909
		public Rectangle ActiveTexturePath;

		// Token: 0x0400038E RID: 910
		public Rectangle PassiveTexturePath;

		// Token: 0x0400038F RID: 911
		[CompilerGenerated]
		private bool {13498};

		// Token: 0x04000390 RID: 912
		private float {13499};

		// Token: 0x04000391 RID: 913
		private float {13500};

		// Token: 0x04000392 RID: 914
		private Rectangle {13501};

		// Token: 0x04000393 RID: 915
		private Rectangle {13502};

		// Token: 0x04000394 RID: 916
		private Rectangle {13503};

		// Token: 0x04000395 RID: 917
		private Rectangle {13504};
	}
}
