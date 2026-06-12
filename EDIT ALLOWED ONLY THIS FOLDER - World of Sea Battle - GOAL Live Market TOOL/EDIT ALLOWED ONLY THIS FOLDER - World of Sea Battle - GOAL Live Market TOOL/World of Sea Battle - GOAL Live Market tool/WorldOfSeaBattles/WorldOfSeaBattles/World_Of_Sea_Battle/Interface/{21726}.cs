using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
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
	// Token: 0x0200034E RID: 846
	internal sealed class {21726} : {21684}
	{
		// Token: 0x06001280 RID: 4736 RVA: 0x0009BEA0 File Offset: 0x0009A0A0
		public {21726}() : base(Global.Player.UsedShipPlayer.AllowAnyMending ? Local.mending : Local.ItemPanelGui_6, "", "")
		{
			this.{21748} = new Button(base.Pos.XY + new Vector2(base.Pos.WH.X - (float)AtlasPortGui.buttonGray.Width - 20f, 418f), AtlasPortGui.buttonGray, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.PortMendingShipWindow_2, Fonts.Philosopher_14, Color.White * 0.7f, false);
			this.{21748}.EvClick += this.{21734};
			this.UpdateTotalValue();
			foreach (CheckboxControl checkboxControl in this.{21744})
			{
				if (checkboxControl != null)
				{
					checkboxControl.EvCheck += this.{21736};
				}
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0009BF94 File Offset: 0x0009A194
		private UiControl AddLine(string {21727}, string {21728}, ValueTuple<GSI, RTI> {21729}, out CheckboxControl {21730})
		{
			Vector2 zero = Vector2.Zero;
			Image image = new Image(new Marker(ref zero, base.Pos.WH.X - 30f, 55f), CommonAtlas.Texture.Tex, {22452}.Pattern_Item_Big, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Image image2 = image;
			CheckboxControl {13296};
			{21730} = ({13296} = new CheckboxControl(image.Pos.XY + new Vector2(15f, 12f), AtlasPortGui.vd_checkBox_26px_true, AtlasPortGui.vd_checkBox_26px_false, {21727}, Fonts.Philosopher_14, new Color(217, 255, 142), true, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			image2.AddChild({13296});
			if (!string.IsNullOrEmpty({21728}))
			{
				image.AddChild(new Label(image.Pos.XY + new Vector2(43f, 34f), Fonts.Arial_9, Color.Wheat * 0.6f, {21728}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			StackForm stackForm = new StackForm(image.Pos.XY + new Vector2(300f, 12f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.WriteCost(stackForm, {21729}, false);
			image.AddChild(stackForm);
			return image;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0009C0BC File Offset: 0x0009A2BC
		private void WriteCost(StackForm {21731}, ValueTuple<GSI, RTI> {21732}, bool {21733})
		{
			if ({21732}.Item2.Value > 0)
			{
				Form form = new Form(new Marker(0f, 0f, 72f, 32f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form.AddChild(new Image(new Marker(0f, 0f, 32f, 32f), CommonAtlas.Texture.Tex, CommonAtlas.goldIconSingle32, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				form.AddChild(new Label(new Vector2(36f, 6f), Fonts.Philosopher_14Bold, {21733} ? (((Session.Account.Gold >= {21732}.Item2.Value) ? Color.LimeGreen : Color.OrangeRed) * 0.5f) : (Color.SkyBlue * 0.5f), {21732}.Item2.Value.ToString() ?? "", PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				{21731}.AddItem(new UiControl[]
				{
					form
				});
				form.ToolTip = new ToolTip(new ToolTipState(Local.gold_s, null, Array.Empty<ToolTipCharacteristics>()));
			}
			foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){21732}.Item1.ResourceInfo))
			{
				Form form2 = new Form(new Marker(0f, 0f, 72f, 32f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form2.AddChild(new Image(new Marker(0f, 0f, 32f, 32f), gsilocalEnumerablePair.Info.IconTexture, gsilocalEnumerablePair.Info.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				form2.AddChild(new Label(new Vector2(36f, 6f), Fonts.Philosopher_14Bold, {21733} ? (((Session.Account.NearPortStorage.GetCount((int)gsilocalEnumerablePair.Info.ID) >= gsilocalEnumerablePair.Count) ? Color.LimeGreen : Color.OrangeRed) * 0.5f) : (Color.SkyBlue * 0.5f), gsilocalEnumerablePair.Count.ToString() ?? "", PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				{21731}.AddItem(new UiControl[]
				{
					form2
				});
				{20431}.Set(form2, gsilocalEnumerablePair.Info, 0, null);
			}
			if ({21732}.Item2.Value == 0 && {21732}.Item1.IsEmpty)
			{
				Form form3 = new Form(new Marker(0f, 0f, 72f, 32f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form3.AddChild(new Label(new Vector2(36f, 6f), Fonts.Philosopher_14Bold, Color.Gray, Local.PortMendingShipWindow_5, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				{21731}.AddItem(new UiControl[]
				{
					form3
				});
			}
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0009C3BC File Offset: 0x0009A5BC
		private ValueTuple<GSI, RTI> UpdateTotalValue()
		{
			ValueTuple<GSI, RTI> valueTuple = new ValueTuple<GSI, RTI>(new GSI(), 0);
			int num = 0;
			for (int i = 0; i < this.{21744}.Length; i++)
			{
				if (this.{21744}[i] != null && this.{21744}[i].IsChecked)
				{
					valueTuple.Item1.Add(this.{21745}[i].Item1);
					valueTuple.Item2.Value = valueTuple.Item2.Value + this.{21745}[i].Item2.Value;
					num++;
				}
			}
			StackForm stackForm = this.{21746};
			if (stackForm != null)
			{
				stackForm.RemoveFromContainer();
			}
			this.{21746} = new StackForm(this.{21749}.Pos.XY + new Vector2(18f, 26f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.WriteCost(this.{21746}, valueTuple, true);
			this.{21749}.AddChild(this.{21746});
			this.{21747} = (num > 0 && Session.Account.NearPortStorage.CanRemove(valueTuple.Item1) && Session.Account.Gold >= valueTuple.Item2.Value);
			this.{21748}.RemoveFromContainer();
			this.{21748}.Opacity = (this.{21747} ? 1f : 0.5f);
			UiControl uiControl = this.{21748};
			Marker pos = this.{21748}.Pos;
			Vector2 vector = this.{21746}.Pos.XY + new Vector2(280f, 0f);
			uiControl.Pos = pos.SetXY(vector);
			this.{21748}.IsVisible = (num > 0);
			this.{21746}.IsVisible = (num > 0);
			this.{21749}.AddChild(this.{21748});
			return valueTuple;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0009C593 File Offset: 0x0009A793
		protected override IEnumerable<UiControl> CreateDesignComponents()
		{
			{21726}.<CreateDesignComponents>d__13 <CreateDesignComponents>d__ = new {21726}.<CreateDesignComponents>d__13(-2);
			<CreateDesignComponents>d__.<>4__this = this;
			return <CreateDesignComponents>d__;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0009C5A4 File Offset: 0x0009A7A4
		[CompilerGenerated]
		private void {21734}(ClickUiEventArgs {21735})
		{
			if (!this.{21747})
			{
				return;
			}
			ValueTuple<GSI, RTI> valueTuple = this.UpdateTotalValue();
			Session.Account.NearPortStorage.Remove(valueTuple.Item1, 1, false);
			Session.Account.Gold -= valueTuple.Item2.Value;
			if (this.{21744}[0] != null && this.{21744}[0].IsChecked)
			{
				if (Global.Player.IsDestroyed)
				{
					Global.Player.Respawn(Global.Player.GetShipPositionInfo, RespawnHealthAmount.About33Percent);
				}
				Global.Player.RestoreHp(float.MaxValue);
				Global.Player.RestoreSailes(100f);
			}
			if (this.{21744}[3] != null && this.{21744}[3].IsChecked)
			{
				Global.Player.RestoreSailes(100f);
			}
			base.BlockAndClose();
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0009C67B File Offset: 0x0009A87B
		[CompilerGenerated]
		private void {21736}(CheckboxCheckedEventArgs {21737})
		{
			this.UpdateTotalValue();
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0009C684 File Offset: 0x0009A884
		[CompilerGenerated]
		private void {21738}(ClickUiEventArgs {21739})
		{
			if (Session.Account.Shipyard.List.Count == 1)
			{
				new {17312}(Local.PortMendingShipWindow_17);
				return;
			}
			ValueTuple<GSI, int> decraft = Global.Player.UsedShipPlayer.CraftFrom.GetDecraft(Session.Game.ShipDecraftBonus);
			Action <>9__2;
			new {17107}(Local.PortMendingShipWindow_16, Local.PortMendingShipWindow_18, "", delegate(int {21751})
			{
				if ({21751} == 0)
				{
					string decraftShipConfirum = Local.DecraftShipConfirum;
					Action {17372};
					if (({17372} = <>9__2) == null)
					{
						{17372} = (<>9__2 = delegate()
						{
							{19994}.Logbook(Local.lbe_ship_destroyed(Global.Player.UsedShipPlayer.CraftFrom.ShipName), LBFlags.L2);
							Session.Account.TakeOffAllEquipment(Global.Player.UsedShipPlayer, true);
							Session.Account.NearPortStorage.Add(decraft.Item1);
							PlayerAccount account = Session.Account;
							account.Monets.Value = account.Monets.Value + decraft.Item2;
							foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)decraft.Item1.ResourceInfo))
							{
								{19994}.Logbook(gsilocalEnumerablePair.Info.Name + "+ " + gsilocalEnumerablePair.Count.ToString(), LBFlags.L1);
							}
							if (decraft.Item2 > 0)
							{
								string str = Local.Monets2.TrimEnd(':');
								string str2 = "+ ";
								int item = decraft.Item2;
								{19994}.Logbook(str + str2 + item.ToString(), LBFlags.L1);
							}
							PlayerShipDynamicInfo currentRealShip = Session.Account.Shipyard.CurrentRealShip;
							Global.Game.ScenePort.ShipsHolder.ExchangeToOther(currentRealShip, true);
							Global.Game.ScenePort.ShipsHolder.Remove(currentRealShip);
							this.BlockAndClose();
						});
					}
					new {17312}(decraftShipConfirum, {17372}, delegate()
					{
					});
				}
			}, true, new CraftingRecipe(decraft.Item1.Clone().Exs(252, decraft.Item2)), new string[]
			{
				Local.CommonItemCraftUi_5,
				Local.to_back2
			});
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0009C74A File Offset: 0x0009A94A
		[CompilerGenerated]
		private void {21740}(ClickUiEventArgs {21741})
		{
			new {17177}(false);
			{17177}.CurrentInstance.AddTabs(new Action<int>(this.{21742}), 0, new string[]
			{
				Local.PortMendingShipWindow_24_a,
				Local.PortMendingShipWindow_24
			});
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0009C780 File Offset: 0x0009A980
		[CompilerGenerated]
		private void {21742}(int {21743})
		{
			float num = (Global.Player.NearPortType == PortEnteringType.PersonalIsle) ? Global.Game.ScenePort.CurrentPersonalIsle.CapitalRestoreDiscount : 0f;
			int num2 = Global.Player.UsedShipPlayer.CraftFrom.MaxIntegrity - Global.Player.UsedShipPlayer.Integrity;
			ValueTuple<GSI, RTI> valueTuple = Gameplay.CapitalRestoreCost(Global.Player.UsedShipPlayer, {21743} == 1, (1f - Session.Game.CapitalRestoreDiscount) * (1f - num));
			float duration = Gameplay.RestoreCapitalIntegrityDuration((int)Global.Player.UsedShipPlayer.CraftFrom.ID, num2) * (1f - Session.Game.CapitalRestoreDurationBonus);
			{17177}.CurrentInstance.SetData(Local.PortMendingShipWindow_20, false, Environment.NewLine + " " + Environment.NewLine + Local.integrity_restore_msg(num2, duration), null, new CraftingRecipe(valueTuple.Item1, valueTuple.Item2.Value), 0f, 1, true, delegate([TupleElementNames(new string[]
			{
				"resCount",
				"btIndex"
			})] ValueTuple<int, int> {21752})
			{
				Global.Player.UsedShipPlayer.ClientTimeToRestoreIntegrity = duration * 60f;
				Global.Game.ScenePort.UpdateGuiForViewShip();
				{17177}.CurrentInstance.AllowMouseInput = false;
				{17177}.CurrentInstance.BlockAndClose();
				this.BlockAndClose();
			}, false, null, null, 1, true, int.MaxValue, false, -1f);
		}

		// Token: 0x040010CD RID: 4301
		private CheckboxControl[] {21744};

		// Token: 0x040010CE RID: 4302
		private ValueTuple<GSI, RTI>[] {21745};

		// Token: 0x040010CF RID: 4303
		private StackForm {21746};

		// Token: 0x040010D0 RID: 4304
		private bool {21747};

		// Token: 0x040010D1 RID: 4305
		private Button {21748};

		// Token: 0x040010D2 RID: 4306
		private Form {21749};

		// Token: 0x040010D3 RID: 4307
		private int {21750};
	}
}
