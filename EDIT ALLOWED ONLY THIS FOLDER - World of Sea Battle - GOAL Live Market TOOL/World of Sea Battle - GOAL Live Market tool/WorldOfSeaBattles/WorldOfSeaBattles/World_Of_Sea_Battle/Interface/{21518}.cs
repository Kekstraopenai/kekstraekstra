using System;
using System.Linq;
using Common;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000330 RID: 816
	internal class {21518} : {17068}
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x000070D7 File Offset: 0x000052D7
		protected override bool CanBeWindow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x000967A4 File Offset: 0x000949A4
		public {21518}()
		{
			Rectangle uiarea = Engine.GS.UIArea;
			base..ctor(Marker.FromCentrScreen(new Marker(ref uiarea), new Rectangle(0, 260, 400, 604)), new Rectangle(0, 260, 500, 680), {17068}.BlockingWay.BackgroundClosing, false);
			ScrollBarControl scrollBarControl = new ScrollBarControl(new Marker(base.Pos.XY.X + base.Pos.WH.X - (float)AtlasPortGui.scrollBar_Pointer.Width - 10f, base.Pos.XY.Y + 80f, (float)AtlasPortGui.scrollBar_Pointer.Width, base.Pos.WH.Y - 100f), Rectangle.Empty, Rectangle.Empty, AtlasPortGui.scrollBar_Pointer, AtlasPortGui.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			ListItemViewControl listItemViewControl = new ListItemViewControl(new Marker(base.Pos.XY.X + 20f, base.Pos.XY.Y + 80f, base.Pos.WH.X - scrollBarControl.Pos.WH.X, base.Pos.WH.Y - 100f), scrollBarControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(new UiControl[]
			{
				scrollBarControl,
				listItemViewControl
			});
			base.AddChild(new Label(base.Pos.XY + new Vector2(200f, 47f), Fonts.Philosopher_16, Color.LightGray, Local.GameSettingsWindow_23, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			this.{21531} = new StackForm(base.Pos.XY, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			listItemViewControl.AddItem(new UiControl[]
			{
				this.{21531}
			});
			this.AddCheckbox(this.{21531}, Local.PortEnteringSettingsWindow_5, Global.Settings.ShowSailesInPort, delegate(bool {21535})
			{
				Global.Settings.ShowSailesInPort = {21535};
			}, () => "", null, null);
			this.AddCheckbox(this.{21531}, Local.PortEnteringSettingsWindow_3, Global.Settings.Automending, delegate(bool {21536})
			{
				Global.Settings.Automending = {21536};
			}, () => "", new ToolTipState("", Local.PortEnteringSettingsWindow_4, Array.Empty<ToolTipCharacteristics>()), null);
			this.AddCheckbox(this.{21531}, Local.PortEnteringSettingsWindow_16, Global.Settings.AutoPaySalarySpecialUnits, delegate(bool {21537})
			{
				Global.Settings.AutoPaySalarySpecialUnits = {21537};
			}, () => "", null, null);
			this.{21532} = this.AddCheckbox(this.{21531}, Local.PortEnteringSettingsWindow_6, Global.Settings.AutomoveToStorage, delegate(bool {21538})
			{
				Global.Settings.AutomoveToStorage = {21538};
			}, () => "", new ToolTipState("", Local.PortEnteringSettingsWindow_7, Array.Empty<ToolTipCharacteristics>()), null);
			this.{21533} = this.AddCheckbox(this.{21531}, Local.PortEnteringSettingsWindow_8, !Global.Settings.AutomoveToStorageMendRes, delegate(bool {21539})
			{
				Global.Settings.AutomoveToStorageMendRes = !{21539};
			}, () => "", new ToolTipState("", Local.PortEnteringSettingsWindow_9, Array.Empty<ToolTipCharacteristics>()), null);
			if (Global.Settings.SavedEquipment.Any((SavedShipEquipment {21540}) => {21540}.Active && {21540}.ShipInfoId == Global.Player.CraftFrom.ID))
			{
				this.{21532}.DisabledMode = new Rectangle?(AtlasPortGui.vd_checkBox_26px_dis);
				this.{21533}.DisabledMode = new Rectangle?(AtlasPortGui.vd_checkBox_26px_dis);
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00096BE8 File Offset: 0x00094DE8
		private CheckboxControl AddCheckbox(StackForm {21519}, string {21520}, bool {21521}, Action<bool> {21522}, Func<string> {21523}, ToolTipState {21524} = null, Action {21525} = null)
		{
			CheckboxControl control = new CheckboxControl(new Vector2(19f, 0f), AtlasPortGui.vd_checkBox_18px_true, AtlasPortGui.vd_checkBox_18px_false, {21521}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			control.EvCheck += delegate(CheckboxCheckedEventArgs {21541})
			{
				{21522}({21541}.NewValue);
			};
			StackForm stackForm = new StackForm(new Vector2(0f, 0f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				control
			});
			stackForm.AddItem(new UiControl[]
			{
				new Label(new Vector2(0f, 0f), Fonts.Philosopher_14, Color.LightGray * 0.8f, {21520}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			stackForm.AddItem(new UiControl[]
			{
				new LiveLabel(new Vector2(0f, 0f), Fonts.Philosopher_14, Color.Yellow, {21523}, 50).ExClick(delegate(ClickUiEventArgs {21542})
				{
					Action clickExtraText = {21525};
					if (clickExtraText == null)
					{
						return;
					}
					clickExtraText();
				})
			});
			{21519}.AddItem(new UiControl[]
			{
				stackForm
			});
			stackForm.EvClickEmptiness += delegate(ClickUiEventArgs {21543})
			{
				control.ImitateClick(false);
			};
			if ({21524} != null)
			{
				stackForm.ToolTip = new ToolTip({21524});
			}
			return control;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00030234 File Offset: 0x0002E434
		protected override void UserUpdate(ref FrameTime {21526})
		{
			base.UserUpdate(ref {21526});
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00096D2B File Offset: 0x00094F2B
		protected override void UserBackRender()
		{
			this.{21534} = (Engine.GS.CurrentTexture != AtlasPortGui.Texture.Tex);
			if (this.{21534})
			{
				Engine.GS.SetTexture(AtlasPortGui.Texture);
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00096D63 File Offset: 0x00094F63
		protected override void UserFrontRender()
		{
			if (this.{21534})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x0400102E RID: 4142
		private static readonly RTI min = new RTI(40);

		// Token: 0x0400102F RID: 4143
		private StackForm {21527};

		// Token: 0x04001030 RID: 4144
		private StackForm {21528};

		// Token: 0x04001031 RID: 4145
		private TextBlockControl {21529};

		// Token: 0x04001032 RID: 4146
		private TextBlockControl {21530};

		// Token: 0x04001033 RID: 4147
		private StackForm {21531};

		// Token: 0x04001034 RID: 4148
		private CheckboxControl {21532};

		// Token: 0x04001035 RID: 4149
		private CheckboxControl {21533};

		// Token: 0x04001036 RID: 4150
		private bool {21534};
	}
}
