using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020000AA RID: 170
	internal class {17507} : CustomUi
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x000230DC File Offset: 0x000212DC
		public {17507}(params Button[] {17509}) : base(true)
		{
			{17507} {17507} = {17507}.currentInstance;
			if ({17507} != null)
			{
				{17507}.RemoveFromContainer();
			}
			{17507}.currentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{17507}.currentInstance = null;
			};
			this.Load({17509});
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00023134 File Offset: 0x00021334
		protected void Load(params Button[] {17510})
		{
			Button button = {17510}.First<Button>();
			this.{17512} = Engine.GS.MouseToUI;
			float num = 0f;
			foreach (Button button2 in {17510})
			{
				num = Math.Max(num, Fonts.Arial_12.Measure(button2.Text).X + (float)((button2.CountChild() > 0) ? 40 : 0));
			}
			float num2 = Math.Max(button.Pos.WH.X * 1.1f, num + 25f);
			Vector2 value;
			if (this.{17512}.X > (float)(Engine.GS.UIArea.Width / 2))
			{
				value.X = this.{17512}.X - 10f - num2;
			}
			else
			{
				value.X = this.{17512}.X + 10f;
			}
			if (this.{17512}.Y > (float)(Engine.GS.UIArea.Height / 2))
			{
				value.Y = this.{17512}.Y - (button.Pos.WH.Y * 1.2f - 4f) * (float){17510}.Length;
			}
			else
			{
				value.Y = this.{17512}.Y + 10f;
			}
			Vector2 value2 = new Vector2(0f, button.Pos.WH.Y * 1.2f - 4f);
			value.Y -= button.Pos.WH.Y * 1.2f;
			base.AddChild(new Form(new Marker(this.{17512}.X - 100f, this.{17512}.Y - 100f, 200f, 200f), CommonAtlas.whiteDot, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Black * 0.35f,
				AnimatedFocus = false
			});
			for (int i = 0; i < {17510}.Length; i++)
			{
				Button item = {17510}[i];
				item.EvClick += delegate(ClickUiEventArgs {17515})
				{
					item.AllowMouseInput = false;
				};
				UiControl item2 = item;
				Vector2 vector = item.Pos.WH * new Vector2(1f, 1.2f);
				Marker marker = new Marker(ref value, ref vector);
				item2.Pos = marker.SetWidth(num2);
				item.SetText(item.Text, Fonts.Arial_12, item.TextColor, false);
				value += value2;
				base.AddChild(item);
			}
			foreach (UiControl uiControl in ((IEnumerable<UiControl>)base.GetChildren))
			{
				UiControl {14276} = uiControl;
				Vector2 vector = this.{17512} - button.Pos.WH * 0.5f;
				Marker marker = uiControl.Pos;
				new UiMarkerAnimation({14276}, new Marker(ref vector, ref marker.WH), uiControl.Pos, 300f);
			}
			new UiOpacityAnimation(this, 0f, 1f, 300f);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000234A8 File Offset: 0x000216A8
		protected override void UserBackRender()
		{
			if (this.{17514} = (Engine.GS.CurrentTexture != CommonAtlas.Texture.Tex))
			{
				Engine.GS.SetTexture(CommonAtlas.Texture);
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000234E8 File Offset: 0x000216E8
		protected override void UserFrontRender()
		{
			Device gs = Engine.GS;
			Rectangle rectangle = new Rectangle(1053, 0, 16, 16);
			Vector2 vector = this.{17512} - new Vector2(8f, 8f);
			gs.Draw(rectangle, vector);
			if (this.{17514})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00023540 File Offset: 0x00021740
		protected override void UserUpdate(ref FrameTime {17511})
		{
			if (InputHelper.LeftWasClicked && (double)this.{17513} > 0.5)
			{
				base.RemoveFromContainer();
			}
			this.{17513} += {17511}.secElapsed;
		}

		// Token: 0x040003B0 RID: 944
		public static readonly Rectangle c_Button_default = new Rectangle(1071, 0, 154, 26);

		// Token: 0x040003B1 RID: 945
		public static readonly Rectangle c_Button_person = new Rectangle(1071, 27, 154, 26);

		// Token: 0x040003B2 RID: 946
		public static readonly Rectangle c_Button_guild = new Rectangle(1071, 54, 154, 26);

		// Token: 0x040003B3 RID: 947
		public static readonly Rectangle c_Button_default_dis = new Rectangle(1071, 81, 154, 26);

		// Token: 0x040003B4 RID: 948
		public static readonly Rectangle c_Button_person_dis = new Rectangle(1071, 108, 154, 26);

		// Token: 0x040003B5 RID: 949
		public static readonly Rectangle c_Button_guild_dis = new Rectangle(1071, 135, 154, 26);

		// Token: 0x040003B6 RID: 950
		public static readonly Rectangle c_Button_message = new Rectangle(1071, 162, 154, 26);

		// Token: 0x040003B7 RID: 951
		public static readonly Rectangle c_Button_message_dis = new Rectangle(1071, 189, 154, 26);

		// Token: 0x040003B8 RID: 952
		public static {17507} currentInstance;

		// Token: 0x040003B9 RID: 953
		private Vector2 {17512};

		// Token: 0x040003BA RID: 954
		private float {17513};

		// Token: 0x040003BB RID: 955
		private bool {17514};
	}
}
