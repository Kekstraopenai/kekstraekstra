using System;
using Common;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000363 RID: 867
	internal sealed class {21884} : {21762}
	{
		// Token: 0x060012D6 RID: 4822 RVA: 0x0009F688 File Offset: 0x0009D888
		public {21884}(string {21893}, Action<{21883}, int> {21894}, int {21895}, int {21896}, int {21897}, {21884}.NameHold {21898} = {21884}.NameHold.Default, int {21899} = -1, int {21900} = 0)
		{
			{21884}.<>c__DisplayClass6_0 CS$<>8__locals1 = new {21884}.<>c__DisplayClass6_0();
			CS$<>8__locals1.hold = {21898};
			CS$<>8__locals1.toShipSelectionLimit = {21899};
			CS$<>8__locals1.complete = {21894};
			base..ctor();
			CS$<>8__locals1.<>4__this = this;
			{21884}.<>c__DisplayClass6_1 CS$<>8__locals2 = new {21884}.<>c__DisplayClass6_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			CS$<>8__locals2.blockEvent = false;
			{21895} = Math.Min({21897}, {21895});
			{21896} = Math.Min({21897}, {21896});
			CS$<>8__locals2.maxAtStrorageForDisplay = {21895};
			if (CS$<>8__locals2.CS$<>8__locals1.toShipSelectionLimit != -1)
			{
				{21895} = Math.Min(CS$<>8__locals2.CS$<>8__locals1.toShipSelectionLimit, {21895});
			}
			this.{21902} = new RTI({21896});
			this.{21903} = new RTI({21895});
			this.{21904} = new RTI({21896} + {21895});
			this.{21905} = new RTI({21896});
			this.{21906} = new RTI({21895});
			Vector2 vector = base.Pos.XY + new Vector2(14f, 24f) + new Vector2(0f, 30f);
			CS$<>8__locals2.supportText = new Label(new Vector2(vector.X, base.Pos.XY.Y) + new Vector2(15f, 18f), Fonts.Arial_12, Color.DarkGray * 0.8f, Local.PortNumerticInputBasicWindow_3, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals2.operatingInfo = new Label(new Vector2(vector.X, base.Pos.XY.Y) + new Vector2(15f, 123f), Fonts.Arial_12, Color.White, "0", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals2.supportText.IsVisible = false;
			CS$<>8__locals2.select = new ProgressSelectBar(vector + new Vector2(120f, -5f), {21762}.cSelectFront, {21762}.cSelectBack, {21762}.cPointer, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Form form = new Form(CS$<>8__locals2.select.Pos, AtlasPortGui.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Transparent
			};
			form.UpdateComplete += delegate(UiControl {21907})
			{
				{21907}.BasicColor = (({21907}.InputMode != MouseInputMode.NoFocus) ? (Color.Yellow * 0.1f) : Color.Transparent);
			};
			CS$<>8__locals2.select.MaxValue = 1f;
			CS$<>8__locals2.select.Value = 1f - (float){21896} / (float)this.{21904}.Value;
			form.AddChild(CS$<>8__locals2.select);
			base.AddChild(form);
			CS$<>8__locals2.atShipLabel = new Label(vector + new Vector2(15f, -23f), Fonts.Arial_12, Color.White, " ", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals2.atStorageLabel = new Label(vector + new Vector2(135f + CS$<>8__locals2.select.Pos.WH.X + 15f, 0f), Fonts.Arial_12, Color.White, " ", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals2.atShipLabel.UpdateComplete += delegate(UiControl {21909})
			{
				CS$<>8__locals2.supportText.IsVisible = (CS$<>8__locals2.atShipLabel.InputMode != MouseInputMode.NoFocus || CS$<>8__locals2.atStorageLabel.InputMode > MouseInputMode.NoFocus);
			};
			CS$<>8__locals2.atStorageLabel.UpdateComplete += delegate(UiControl {21910})
			{
				CS$<>8__locals2.supportText.IsVisible = (CS$<>8__locals2.atShipLabel.InputMode != MouseInputMode.NoFocus || CS$<>8__locals2.atStorageLabel.InputMode > MouseInputMode.NoFocus);
			};
			{21884}.<>c__DisplayClass6_1 CS$<>8__locals3 = CS$<>8__locals2;
			Vector2 vector2 = vector + new Vector2(15f, -7f);
			Vector2 vector3 = new Vector2(80f, (float){21762}.Pattern_TextBoxShort.Height);
			CS$<>8__locals3.value = new TextBox(new Marker(ref vector2, ref vector3), {21762}.Pattern_TextBoxShort, Fonts.Arial_12, Color.White, AtlasPortGui.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals2.value.Text = this.{21905}.Value.ToString();
			this.acceptButton = new Button(base.Pos.XY + {21762}.Downbar_Button1Position, {21762}.Pattern_DownbarButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals2.updateLabels = delegate()
			{
				CS$<>8__locals2.atShipLabel.Text = ((CS$<>8__locals2.CS$<>8__locals1.hold == {21884}.NameHold.Units) ? (Local.on_ship + ": ") : Local.hold);
				CS$<>8__locals2.atShipLabel.BasicColor = Color.Lerp(Color.White, Color.DimGray, CS$<>8__locals2.select.Value);
				CS$<>8__locals2.atStorageLabel.Text = ((CS$<>8__locals2.CS$<>8__locals1.hold == {21884}.NameHold.Tres) ? Local.GuildWindow_23 : ((CS$<>8__locals2.CS$<>8__locals1.hold == {21884}.NameHold.Units) ? Local.PortNumerticInputBasicWindow_5 : Local.storage_d)) + (CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21906}.Value + ((CS$<>8__locals2.CS$<>8__locals1.toShipSelectionLimit == -1) ? 0 : Math.Max(0, CS$<>8__locals2.maxAtStrorageForDisplay - CS$<>8__locals2.CS$<>8__locals1.toShipSelectionLimit))).ToString();
				CS$<>8__locals2.atStorageLabel.BasicColor = Color.Lerp(Color.DimGray, Color.White, CS$<>8__locals2.select.Value);
				if (CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21905}.Value == CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21902}.Value)
				{
					CS$<>8__locals2.operatingInfo.Text = Local.PortNumerticInputBasicWindow_6;
					CS$<>8__locals2.operatingInfo.BasicColor = Color.LightGray;
				}
				else if (CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21905}.Value > CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21902}.Value)
				{
					CS$<>8__locals2.operatingInfo.Text = (CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21905}.Value - CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21902}.Value).ToString() + ((CS$<>8__locals2.CS$<>8__locals1.hold == {21884}.NameHold.Units) ? Local.PortNumerticInputBasicWindow_7 : Local.PortNumerticInputBasicWindow_8);
					CS$<>8__locals2.operatingInfo.BasicColor = Color.Cyan;
				}
				else if (CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21906}.Value > CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21903}.Value)
				{
					CS$<>8__locals2.operatingInfo.Text = (CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21906}.Value - CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21903}.Value).ToString() + ((CS$<>8__locals2.CS$<>8__locals1.hold == {21884}.NameHold.Tres) ? Local.PortNumerticInputBasicWindow_9 : ((CS$<>8__locals2.CS$<>8__locals1.hold == {21884}.NameHold.Units) ? Local.tp_port_units : Local.to_hold));
					CS$<>8__locals2.operatingInfo.BasicColor = Color.Gold;
				}
				CS$<>8__locals2.value.Text = CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21905}.Value.ToString();
				if (CS$<>8__locals2.CS$<>8__locals1.<>4__this.acceptButton != null)
				{
					CS$<>8__locals2.CS$<>8__locals1.<>4__this.acceptButton.Opacity = ((CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21905}.Value == CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21902}.Value) ? 0.5f : 1f);
				}
			};
			CS$<>8__locals2.atShipLabel.EvClick += delegate(ClickUiEventArgs {21911})
			{
				if (CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21906}.Value > 0)
				{
					{21884} <>4__this = CS$<>8__locals2.CS$<>8__locals1.<>4__this;
					int value = <>4__this.{21905}.Value;
					<>4__this.{21905}.Value = value + 1;
					{21884} <>4__this2 = CS$<>8__locals2.CS$<>8__locals1.<>4__this;
					value = <>4__this2.{21906}.Value;
					<>4__this2.{21906}.Value = value - 1;
					CS$<>8__locals2.select.Value = 1f - (float)CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21905}.Value / (float)CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21904}.Value;
				}
			};
			CS$<>8__locals2.atStorageLabel.EvClick += delegate(ClickUiEventArgs {21912})
			{
				if (CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21905}.Value > 0)
				{
					{21884} <>4__this = CS$<>8__locals2.CS$<>8__locals1.<>4__this;
					int value = <>4__this.{21905}.Value;
					<>4__this.{21905}.Value = value - 1;
					{21884} <>4__this2 = CS$<>8__locals2.CS$<>8__locals1.<>4__this;
					value = <>4__this2.{21906}.Value;
					<>4__this2.{21906}.Value = value + 1;
					CS$<>8__locals2.select.Value = 1f - (float)CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21905}.Value / (float)CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21904}.Value;
				}
			};
			CS$<>8__locals2.updateLabels();
			CS$<>8__locals2.select.EvChange += delegate(ProgressBarChangeEventArgs {21913})
			{
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21905}.Value = (int)MathHelper.Clamp(MathF.Round((1f - {21913}.NewValue) * (float)CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21904}.Value), 0f, (float)CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21904}.Value);
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21906}.Value = CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21904}.Value - CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21905}.Value;
				CS$<>8__locals2.updateLabels();
			};
			CS$<>8__locals2.value.EvTextChanged += delegate(string {21914})
			{
				if (CS$<>8__locals2.blockEvent)
				{
					return;
				}
				CS$<>8__locals2.blockEvent = true;
				int num;
				if (int.TryParse(CS$<>8__locals2.value.Text, out num))
				{
					if (num < 0)
					{
						num = 0;
					}
					CS$<>8__locals2.value.FontColor = Color.White;
					float value = 1f - Geometry.Saturate((float)num / (float)CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21904}.Value);
					CS$<>8__locals2.select.Value = value;
				}
				else
				{
					CS$<>8__locals2.value.FontColor = Color.OrangeRed;
				}
				CS$<>8__locals2.blockEvent = false;
			};
			if ({21900} != 0)
			{
				CS$<>8__locals2.select.Value += (float){21900} / (float)this.{21904}.Value;
			}
			base.AddChild(new UiControl[]
			{
				this.acceptButton,
				CS$<>8__locals2.atShipLabel,
				CS$<>8__locals2.atStorageLabel,
				CS$<>8__locals2.operatingInfo,
				CS$<>8__locals2.supportText,
				CS$<>8__locals2.value
			});
			this.acceptButton.SetText(Local.move, Fonts.Arial_12, Color.White, false);
			this.acceptButton.EvClick += delegate(ClickUiEventArgs {21908})
			{
				if (CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21905}.Value != CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21902}.Value)
				{
					if (CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21905}.Value > CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21902}.Value)
					{
						CS$<>8__locals2.CS$<>8__locals1.complete({21883}.Ship, CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21905}.Value - CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21902}.Value);
					}
					else if (CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21906}.Value > CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21903}.Value)
					{
						CS$<>8__locals2.CS$<>8__locals1.complete({21883}.Port, CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21906}.Value - CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21903}.Value);
					}
				}
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.BlockAndClose();
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.acceptButton.AllowMouseInput = false;
			};
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0009E4AB File Offset: 0x0009C6AB
		protected override void UserUpdate(ref FrameTime {21901})
		{
			base.UserUpdate(ref {21901});
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0009E4B4 File Offset: 0x0009C6B4
		protected override void UserBackRender()
		{
			base.UserBackRender();
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0009E4BC File Offset: 0x0009C6BC
		protected override void UserFrontRender()
		{
			base.UserFrontRender();
		}

		// Token: 0x0400112A RID: 4394
		private readonly RTI {21902};

		// Token: 0x0400112B RID: 4395
		private readonly RTI {21903};

		// Token: 0x0400112C RID: 4396
		private readonly RTI {21904};

		// Token: 0x0400112D RID: 4397
		private RTI {21905};

		// Token: 0x0400112E RID: 4398
		private RTI {21906};

		// Token: 0x02000364 RID: 868
		public enum NameHold
		{
			// Token: 0x04001130 RID: 4400
			Default,
			// Token: 0x04001131 RID: 4401
			Tres,
			// Token: 0x04001132 RID: 4402
			Units
		}
	}
}
