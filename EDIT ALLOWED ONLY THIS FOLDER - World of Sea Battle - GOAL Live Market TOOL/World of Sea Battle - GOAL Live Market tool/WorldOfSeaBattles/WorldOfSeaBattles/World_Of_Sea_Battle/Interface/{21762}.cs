using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000353 RID: 851
	internal abstract class {21762} : {17068}
	{
		// Token: 0x0600129C RID: 4764 RVA: 0x0009D05C File Offset: 0x0009B25C
		public {21762}() : base(new Marker((float)(Engine.GS.UIArea.Width / 2 - {21762}.pMain.Width / 2), (float)(Engine.GS.UIArea.Height / 2 - {21762}.pMain.Height), ref {21762}.pMain), {21762}.pMain, {17068}.BlockingWay.BackgroundClosing, false)
		{
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0009D0BC File Offset: 0x0009B2BC
		protected void WriteEmptyLine()
		{
			this.switchersCount += 1f;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0009D0D0 File Offset: 0x0009B2D0
		protected ProgressSelectBar AddNodeSwitcherToEnd(string {21763}, int {21764}, int {21765}, Action<int> {21766}, bool {21767} = false)
		{
			Vector2 value = base.Pos.XY + new Vector2(14f, 24f);
			Vector2 value2 = new Vector2(0f, 30f);
			float num = this.switchersCount;
			this.switchersCount = num + 1f;
			Vector2 vector = value + value2 * num;
			base.AddChild(new Label(vector, Fonts.Arial_12, Color.Gray, {21763}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			ProgressSelectBar select = new ProgressSelectBar(vector + new Vector2(130f, -5f), {21762}.cSelectFront, {21762}.cSelectBack, {21762}.cPointer, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Form form = new Form(select.Pos, AtlasPortGui.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Transparent
			};
			form.UpdateComplete += delegate(UiControl {21771})
			{
				{21771}.BasicColor = (({21771}.InputMode != MouseInputMode.NoFocus) ? (Color.Yellow * 0.1f) : Color.Transparent);
			};
			select.MaxValue = (float)(({21765} == 0) ? 1 : {21765});
			select.Value = (float){21764};
			form.AddChild(select);
			base.AddChild(form);
			this.{21769} = new TextBox(vector + new Vector2(130f + select.Pos.WH.X + 10f, -8f), {21762}.Pattern_TextBoxShort, Fonts.Arial_12, Color.White, AtlasPortGui.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{21769}.Text = {21764}.ToString();
			base.AddChild(this.{21769});
			if ({21765} != 0)
			{
				this.{21769}.EvTextChanged += delegate(string {21772})
				{
					int num2 = 0;
					if (string.IsNullOrEmpty(this.{21769}.Text) || int.TryParse(this.{21769}.Text, out num2))
					{
						if (num2 < 0)
						{
							num2 = 0;
						}
						select.Value = (float)num2;
						{21766}(Math.Min(num2, {21765}));
						this.{21769}.FontColor = Color.White;
						return;
					}
					this.{21769}.FontColor = Color.OrangeRed;
				};
			}
			if ({21765} != 0)
			{
				select.EvChange += delegate(ProgressBarChangeEventArgs {21773})
				{
					int obj = (int)MathHelper.Clamp({21773}.Sender.Value, 1f, (float){21765});
					{21766}(obj);
					if (!this.{21769}.IsEnter || this.{21769}.Text != "")
					{
						this.{21769}.Text = obj.ToString();
					}
				};
			}
			if ({21767})
			{
				{21766}({21764});
			}
			return select;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0009D2DA File Offset: 0x0009B4DA
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(AtlasPortGui.Texture);
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x000201CC File Offset: 0x0001E3CC
		protected override void UserFrontRender()
		{
			Engine.GS.ReturnBackTexture();
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0009D2EC File Offset: 0x0009B4EC
		protected override void UserUpdate(ref FrameTime {21768})
		{
			if (this.acceptButton != null && InputHelper.IsClick(Keys.Enter) && base.AllowMouseInput)
			{
				this.acceptButton.ImitateClick(false);
			}
			if (!this.{21770} && !InputHelper.NowMouseState.LeftPressed && !InputHelper.NowMouseState.RightPressed)
			{
				if (this.{21769} != null)
				{
					this.{21769}.IsEnter = true;
				}
				this.{21770} = true;
			}
			base.UserUpdate(ref {21768});
		}

		// Token: 0x040010DF RID: 4319
		public static readonly Rectangle Pattern_TextBox = new Rectangle(1810, 160, 194, 31);

		// Token: 0x040010E0 RID: 4320
		public static readonly Rectangle Pattern_TextBoxShort = new Rectangle(1810, 160, 118, 31);

		// Token: 0x040010E1 RID: 4321
		public static readonly Rectangle Pattern_DownbarButton = new Rectangle(2005, 160, 135, 32);

		// Token: 0x040010E2 RID: 4322
		protected static readonly Vector2 Downbar_Button1Position = new Vector2(351f, 117f);

		// Token: 0x040010E3 RID: 4323
		protected static readonly Vector2 Downbar_Button2Position = new Vector2(210f, 117f);

		// Token: 0x040010E4 RID: 4324
		private static readonly Rectangle pMain = new Rectangle(1810, 0, 500, 160);

		// Token: 0x040010E5 RID: 4325
		public static readonly Rectangle cSelectBack = new Rectangle(207, 139, 214, 26);

		// Token: 0x040010E6 RID: 4326
		public static readonly Rectangle cSelectFront = new Rectangle(207, 116, 214, 26);

		// Token: 0x040010E7 RID: 4327
		public static readonly Rectangle cPointer = new Rectangle(187, 145, 16, 16);

		// Token: 0x040010E8 RID: 4328
		protected float switchersCount;

		// Token: 0x040010E9 RID: 4329
		protected Button acceptButton;

		// Token: 0x040010EA RID: 4330
		private TextBox {21769};

		// Token: 0x040010EB RID: 4331
		private bool {21770};
	}
}
