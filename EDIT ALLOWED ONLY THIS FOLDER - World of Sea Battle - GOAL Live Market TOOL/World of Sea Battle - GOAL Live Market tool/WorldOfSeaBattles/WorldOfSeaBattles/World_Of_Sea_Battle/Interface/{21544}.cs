using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000333 RID: 819
	internal sealed class {21544} : {21684}
	{
		// Token: 0x060011E0 RID: 4576 RVA: 0x00096E2C File Offset: 0x0009502C
		public {21544}() : base(Local.design, Local.PortEquipDesignsShipWindow_3, "")
		{
			{21544}.CurrentInstance = this;
			Global.Player.UpdateSailClotting();
			base.EvRemoveFromContainer += delegate()
			{
				{21544}.CurrentInstance = null;
				if (Global.Player != null)
				{
					Global.Player.UpdateSailClotting();
					Global.Player.Client.ExampleSailTexturePreview = null;
				}
			};
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00096E83 File Offset: 0x00095083
		protected override IEnumerable<UiControl> CreateDesignComponents()
		{
			{21544}.<CreateDesignComponents>d__4 <CreateDesignComponents>d__ = new {21544}.<CreateDesignComponents>d__4(-2);
			<CreateDesignComponents>d__.<>4__this = this;
			return <CreateDesignComponents>d__;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00096E94 File Offset: 0x00095094
		private void GetRelatedOffers(List<UiControl> {21545}, ShipDesignCategory {21546})
		{
			if (PlatformTuning.DisableShop)
			{
				return;
			}
			if ({21546} == ShipDesignCategory.Flag2)
			{
				{21546} = ShipDesignCategory.Flag;
			}
			Tlist<ShipDesignInfo> tlist = new Tlist<ShipDesignInfo>(from {21567} in Gameplay.DesignsInfo
			where {21567}.Category == {21546} && {21567}.InShop && Session.Account.DesingElementsAtStorage[(int){21567}.ID] == 0 && ({21567}.Category != ShipDesignCategory.ShipFullDesign || {21567}.AssociatedShip == Global.Player.UsedShipPlayer.CraftFrom)
			select {21567});
			tlist.Shuffle();
			tlist.Size = Math.Min(3, tlist.Size);
			using (IEnumerator<ShipDesignInfo> enumerator = ((IEnumerable<ShipDesignInfo>)tlist).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ShipDesignInfo item = enumerator.Current;
					Form form = new Form(Vector2.Zero, {21684}.c_selectMenuItem_small, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					form.AddChild(item.DesignElementTextureIcon(new Marker(3f, 3f, 78f, 78f)));
					form.AddChild(new Label(88f, 3f, Fonts.Philosopher_14, Color.White * 0.5f, Local.PortEquipDesignsShipWindow_10.Trim(new char[]
					{
						'[',
						']'
					}) + " " + {20431}.SeparateNames(item.Name)[0], PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					form.AddChild(new Button(new Vector2(88f, 24f), {21684}.c_yellowButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {21568})
					{
						this.RemoveFromContainer();
						Global.Game.ScenePort.realShopHandler(null, null);
						{20881}.RedirectToDesignsPage({21546});
						{20881}.StartFitting(item);
					}).SetText(Local.PortEquipDesignsShipWindow_9, Fonts.Philosopher_14, Color.LightGray, false));
					{21545}.Add(form);
				}
			}
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0009704C File Offset: 0x0009524C
		protected override UiControl GetHeadForSelectionForm(object {21547})
		{
			if ({21547} is ShipDesignCategory)
			{
				ShipDesignCategory category = (ShipDesignCategory){21547};
				Form form = new Form(Vector2.Zero, {21684}.c_selectMenuItem_small, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form.Pos = form.Pos.SetHeight(form.Pos.WH.Y * 0.5f);
				form.AddChild(new Label(5f, 10f, Fonts.Philosopher_14, Color.White * 0.5f, Local.in_storage + category.ToStrLocal(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				Func<ShipDesignCategory, bool> <>9__1;
				Button button = new Button(new Vector2(370f, 8f), {21684}.c_yellowButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.PortEquipDesignsShipWindow_RemoveAllButton, Fonts.Arial_10, Color.White * 0.8f, false).ExClick(delegate(ClickUiEventArgs {21569})
				{
					foreach (PlayerShipDynamicInfo playerShipDynamicInfo in Session.Account.Shipyard.List)
					{
						Func<ShipDesignCategory, bool> {2731};
						if (({2731} = <>9__1) == null)
						{
							{2731} = (<>9__1 = ((ShipDesignCategory {21570}) => ShipDesignInfo.AliasEquals(category, {21570})));
						}
						foreach (ShipDesignInfo shipDesignInfo in playerShipDynamicInfo.RemoveDesignElementsByCategory({2731}))
						{
							Session.Account.DesingElementsAtStorage.AddOrRemove((int)shipDesignInfo.ID, 1);
						}
					}
					{21569}.Sender.ToolTip.CloseIfIsOpen();
					this.UpdateBlocks(false);
					this.RefreshSelectionList({21547});
					Global.Game.ScenePort.UpdateGuiForViewShip();
				});
				button.AntiMissclick = true;
				button.ToolTipState = new ToolTipState("", Local.PortEquipDesignsShipWindow_RemoveAll, Array.Empty<ToolTipCharacteristics>());
				form.AddChild(button);
				return form;
			}
			return null;
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00097181 File Offset: 0x00095381
		protected override IEnumerable<UiControl> CreateElementsToSelection(object {21548})
		{
			{21544}.<CreateElementsToSelection>d__7 <CreateElementsToSelection>d__ = new {21544}.<CreateElementsToSelection>d__7(-2);
			<CreateElementsToSelection>d__.<>4__this = this;
			<CreateElementsToSelection>d__.<>3__arg = {21548};
			return <CreateElementsToSelection>d__;
		}

		// Token: 0x04001046 RID: 4166
		public static {21544} CurrentInstance;

		// Token: 0x02000334 RID: 820
		internal class PlaceHolderView : Form
		{
			// Token: 0x060011E7 RID: 4583 RVA: 0x00097198 File Offset: 0x00095398
			public PlaceHolderView(Vector2 {21552}, ShipDesignCategory {21553}, {21544} {21554}) : base({21552}, new Rectangle(2139, 322, 449, 55), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				{21544}.PlaceHolderView <>4__this = this;
				this.{21561} = {21554};
				this.{21560} = {21553};
				this.LoadElementInfo();
				base.EvClickEmptiness += delegate(ClickUiEventArgs {21562})
				{
					<>4__this.{21561}.OpenSelectionForm({21553});
				};
			}

			// Token: 0x060011E8 RID: 4584 RVA: 0x00097204 File Offset: 0x00095404
			public void LoadElementInfo()
			{
				base.ClearAllChild();
				base.ToolTip = null;
				ShipDesignInfo designElement = Global.Player.UsedShipPlayer.GetDesignElement((int)this.{21560});
				if (designElement != null && !ShipDesignInfo.AliasEquals(this.{21560}, ShipDesignCategory.Flag) && !ShipDesignInfo.AliasEquals(this.{21560}, ShipDesignCategory.MastColor) && !ShipDesignInfo.AliasEquals(this.{21560}, ShipDesignCategory.ShipFullDesign) && !ShipDesignInfo.AliasEquals(this.{21560}, ShipDesignCategory.SailTexture))
				{
					Button button = new Button(base.Pos.XY + new Vector2((float)(66 + {21684}.c_smallButton.Width + 5), 28f), {21684}.c_yellowButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					button.SetText((ShipDesignInfo.AliasEquals(this.{21560}, ShipDesignCategory.Satellite) || ShipDesignInfo.AliasEquals(this.{21560}, ShipDesignCategory.BowFigure)) ? Local.PortEquipDesignsShipWindow_0 : Local.PortEquipDesignsShipWindow_1, Fonts.Arial_10, Color.LightGray, false);
					button.EvClick += this.{21555};
					base.AddChild(button);
				}
				Marker pos;
				Marker pos2;
				if (designElement == null)
				{
					string {13345} = (this.{21560} == ShipDesignCategory.Decal1) ? Local.PortRealShopPage_49 : ((this.{21560} == ShipDesignCategory.Decal2) ? Local.PortRealShopPage_50 : this.{21560}.ToStrLocal());
					UiControl[] array = new UiControl[2];
					array[0] = new Label(base.Pos.XY + new Vector2(67f, 8f), Fonts.Philosopher_16, Color.White * 0.25f, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						RenderToDepthMap = false
					};
					int num = 1;
					pos = {21544}.PlaceHolderView.p_holder_iconView;
					pos2 = base.Pos;
					array[num] = new Form(pos.Offset(pos2.XY), AtlasPortGui.DesignElementIcon(this.{21560}), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						RenderToDepthMap = false
					};
					base.AddChild(array);
					return;
				}
				Button button2 = new Button(base.Pos.XY + new Vector2(66f, 28f), {21684}.c_smallButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button2.SetText(Local.take_off, Fonts.Arial_10, Color.LightGray, false);
				button2.EvClick += this.{21558};
				ShipDesignInfo {25376} = designElement;
				pos2 = {21544}.PlaceHolderView.p_holder_iconView;
				pos = base.Pos;
				Image image = {25376}.DesignElementTextureIcon(pos2.Offset(pos.XY));
				if (image != null)
				{
					image.RenderToDepthMap = false;
					base.AddChild(image);
				}
				string[] array2 = {20431}.SeparateNames(designElement.Name);
				Label label = new Label(base.Pos.XY + new Vector2(69f, 3f), Fonts.Philosopher_16, Color.Wheat * 0.6f, array2[0], PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					RenderToDepthMap = false
				};
				base.AddChild(new UiControl[]
				{
					label,
					button2
				});
				base.ToolTipState = new ToolTipState(designElement.Name, designElement.GetAnnotation(), Array.Empty<ToolTipCharacteristics>());
				if (array2.Length > 1)
				{
					pos = label.Pos;
					base.AddChild(new Label(new Vector2(pos.End.X - 2f, label.Pos.XY.Y + 6f), Fonts.Arial_10, Color.Wheat * 0.6f, "(...)", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						RenderToDepthMap = false
					});
				}
			}

			// Token: 0x060011EA RID: 4586 RVA: 0x00097562 File Offset: 0x00095762
			[CompilerGenerated]
			private void {21555}(ClickUiEventArgs {21556})
			{
				Global.Game.ScenePort.IsVisibleMainUi = false;
				this.{21561}.IsVisible = false;
				{22409}.Create(this.{21560}, true).EvRemoveFromContainer += this.{21557};
			}

			// Token: 0x060011EB RID: 4587 RVA: 0x0009759D File Offset: 0x0009579D
			[CompilerGenerated]
			private void {21557}()
			{
				Global.Game.ScenePort.IsVisibleMainUi = true;
				this.{21561}.IsVisible = true;
			}

			// Token: 0x060011EC RID: 4588 RVA: 0x000975BC File Offset: 0x000957BC
			[CompilerGenerated]
			private void {21558}(ClickUiEventArgs {21559})
			{
				if (base.ToolTip != null)
				{
					base.ToolTip.CloseIfIsOpen();
				}
				ShipDesignInfo shipDesignInfo = Global.Player.UsedShipPlayer.RemoveDesignElement((int)this.{21560});
				Session.Account.DesingElementsAtStorage.AddOrRemove((int)shipDesignInfo.ID, 1);
				this.{21561}.UpdateBlocks(false);
				this.LoadElementInfo();
				Global.Game.ScenePort.UpdateGuiForViewShip();
			}

			// Token: 0x04001047 RID: 4167
			private static readonly Marker p_holder_iconView = new Marker(1f, 1f, 53f, 53f);

			// Token: 0x04001048 RID: 4168
			private readonly ShipDesignCategory {21560};

			// Token: 0x04001049 RID: 4169
			private readonly {21544} {21561};
		}
	}
}
