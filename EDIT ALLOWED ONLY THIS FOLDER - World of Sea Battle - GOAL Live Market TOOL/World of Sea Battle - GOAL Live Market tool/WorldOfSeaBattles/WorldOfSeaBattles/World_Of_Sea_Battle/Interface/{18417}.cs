using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Data;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000139 RID: 313
	internal class {18417} : {17625}
	{
		// Token: 0x0600073B RID: 1851 RVA: 0x000391B4 File Offset: 0x000373B4
		private {18417}(string {18425}, bool {18426}, [Nullable(2)] {17625}.DynamicTittle[] {18427} = null)
		{
			float {17636} = (float)({18426} ? 450 : 774);
			Rectangle c_back = {17625}.c_back3;
			{17604} inGameWindowBlockShip = {17604}.InGameWindowBlockShip;
			Rectangle c_icTreasury = {17625}.c_icTreasury;
			{17625}.DynamicTittle[] {17640} = {18427};
			if ({18427} == null)
			{
				({17640} = new {17625}.DynamicTittle[1])[0] = {18425};
			}
			base..ctor({17636}, c_back, inGameWindowBlockShip, c_icTreasury, {17640});
			Global.Network.Send(new OnTraderInSeaWindowStatus(true));
			{18417}.CurrentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				Global.Network.Send(new OnTraderInSeaWindowStatus(false));
				{18417}.CurrentInstance = null;
			};
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0003923E File Offset: 0x0003743E
		private static IEnumerable<{17625}.DynamicTittle> FetchTitles(bool {18428}, bool {18429})
		{
			{18417}.<FetchTitles>d__13 <FetchTitles>d__ = new {18417}.<FetchTitles>d__13(-2);
			<FetchTitles>d__.<>3__enableArenaUpgrades = {18428};
			<FetchTitles>d__.<>3__enableBuyingAmmo = {18429};
			return <FetchTitles>d__;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00039258 File Offset: 0x00037458
		public {18417}(bool {18430}, bool {18431}) : this(null, false, {18417}.FetchTitles({18430}, {18431}).ToArray<{17625}.DynamicTittle>())
		{
			Tlist<Action<ListItemViewControl>> tlist = new Tlist<Action<ListItemViewControl>>();
			if ({18430})
			{
				Tlist<Action<ListItemViewControl>> tlist2 = tlist;
				Action<ListItemViewControl> action = new Action<ListItemViewControl>(this.{18451});
				tlist2.Add(action);
			}
			if ({18431})
			{
				Global.Network.Send(new OnTraderInSeaOpenMsg(null, 0, OnTraderInSeaOpenMsg.Extra.None));
				Tlist<Action<ListItemViewControl>> tlist3 = tlist;
				Action<ListItemViewControl> action = new Action<ListItemViewControl>(this.{18434});
				tlist3.Add(action);
			}
			base.ComposeTabWithScroll(null, true, true, tlist.ToArray<Action<ListItemViewControl>>());
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x000392DC File Offset: 0x000374DC
		public {18417}(TraderInSeaPlaceInfo {18432}, int {18433}) : this(({18432} == null) ? Local.TraderInSea : {18432}.Name, {18432} != null && {18432}.Type == TraderInSeaType.Altar, null)
		{
			{18417} <>4__this = this;
			if ({18432} != null)
			{
				Global.Camera.CameraEffects.RunFocusEffect(new CameraFocusEffect(delegate()
				{
					if ({18417}.CurrentInstance != null)
					{
						return new Vector3?({18432}.Position.X0Y() + Vector3.Up * 4f);
					}
					return null;
				}, 0.3f, 0f));
			}
			this.{18458} = {18433};
			TraderInSeaType traderInSeaType = ({18432} == null) ? TraderInSeaType.TraderInSea : {18432}.Type;
			if (traderInSeaType != TraderInSeaType.Altar && {18432} != null && Session.Account.Rang > 10)
			{
				StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(473f, 20f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Button button = new Button(Vector2.Zero, CommonAtlas.basicButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm.AddItem(new UiControl[]
				{
					button
				});
				stackForm.DisableDepthFocusTest = true;
				base.AddChild(stackForm);
				button.SetText(Local.PortVisualScene_4, Fonts.Philosopher_14, Color.White * 0.8f, false);
				button.EvClick += delegate(ClickUiEventArgs {18461})
				{
					<>4__this.BlockAndClose();
					if (Global.Player.MapInfo.IsWorldmap && !Global.Player.IsDestroyed && Global.Player.CheckBattleTimerAndSpeed())
					{
						new {18981}();
					}
				};
			}
			if (traderInSeaType == TraderInSeaType.Altar)
			{
				StackForm stackForm2 = new StackForm(base.Pos.XY + new Vector2(24f, 94f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				StackForm stackForm3 = new StackForm(Vector2.Zero, UiOrientation.HorizontalBottom, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm3.AddItem(new UiControl[]
				{
					new Image(new Marker(0f, 0f, 28f, 28f), Gameplay.ItemsInfo.FromID(15).IconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				StackForm stackForm4 = stackForm3;
				UiControl[] array = new UiControl[1];
				array[0] = new LiveLabel(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat * 0.7f, () => " " + Local.altar_up + Global.Player.ResourcesOfHold.GetCount(15).ToString(), 100);
				stackForm4.AddItem(array);
				stackForm2.AddItem(new UiControl[]
				{
					stackForm3
				});
				base.AddChild(stackForm2);
				Rectangle rectangle = new Rectangle(0, 2015, 383, 188);
				Form form = new Form(Marker.FromCentrScreen(base.Pos, rectangle).Offset(0f, 60f), rectangle, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				base.AddChild(form);
				form.AddChild(TextBlockBuilder.CreateBlock(300f, Local.altar_tt, Color.Black * 0.7f, Fonts.Philosopher_14, 0f).CreateCentroid(new Vector2(form.Pos.Center.X, form.Pos.XY.Y + 35f)));
				form.AddChild(new LabelButton(new Vector2(form.Pos.Center.X, form.Pos.XY.Y + 125f), Local.GuildWindow_36 + " 1", Fonts.Philosopher_16, Color.Black * 0.9f, Color.DarkRed, delegate(ClickUiEventArgs {18462})
				{
					if (<>4__this.{18453} == 0f && Global.Player.ResourcesOfHold.GetCount(15) > 0)
					{
						<>4__this.{18455} = {18462}.Sender;
						<>4__this.{18453} = {18417}.c_escudoTime;
						<>4__this.{18454} = OnOpenChestMsg.Flags.UseSingleCoinAltar;
					}
				}).Center());
				form.AddChild(new LabelButton(new Vector2(form.Pos.Center.X, form.Pos.XY.Y + 148f), Local.GuildWindow_36 + " 5", Fonts.Philosopher_16, Color.Black * 0.9f, Color.DarkRed, delegate(ClickUiEventArgs {18463})
				{
					if (<>4__this.{18453} == 0f && Global.Player.ResourcesOfHold.GetCount(15) >= 5)
					{
						<>4__this.{18455} = {18463}.Sender;
						<>4__this.{18453} = {18417}.c_escudoTime / 5f;
						<>4__this.{18454} = OnOpenChestMsg.Flags.UseFiveCoinsAltar;
					}
				}).Center().ExToolTip(new ToolTip(new ToolTipState("", Local.altar_tt_mode5, Array.Empty<ToolTipCharacteristics>()))));
			}
			else
			{
				Global.Network.Send(new OnTraderInSeaOpenMsg(null, {18433}, OnTraderInSeaOpenMsg.Extra.None));
				base.ComposeTabWithScroll(null, true, true, new Action<ListItemViewControl>[]
				{
					new Action<ListItemViewControl>(this.{18434})
				});
			}
			EducationHelper.MakeFlag(EducationOnboarding.OpenTraderInSea, true);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00039724 File Offset: 0x00037924
		private void {18434}(ListItemViewControl {18435})
		{
			{18417}.<>c__DisplayClass16_0 CS$<>8__locals1 = new {18417}.<>c__DisplayClass16_0();
			CS$<>8__locals1.<>4__this = this;
			if (!this.{18457})
			{
				return;
			}
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			StackForm stackForm3 = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			TraderInSeaPoint.Offer[] array = (from {18459} in Session.LastTraderInSeaOffers.Enumerate()
			where {18459}.IsPlayerBuy
			select {18459}).ToArray<TraderInSeaPoint.Offer>();
			TraderInSeaPoint.Offer[] array2 = (from {18460} in Session.LastTraderInSeaOffers.Enumerate()
			where !{18460}.IsPlayerBuy
			select {18460}).ToArray<TraderInSeaPoint.Offer>();
			if (array2.Length == 0)
			{
				array2 = array.Take(array.Length / 2).ToArray<TraderInSeaPoint.Offer>();
				array = array.Skip(array.Length / 2).ToArray<TraderInSeaPoint.Offer>();
			}
			else
			{
				stackForm2.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.Philosopher_16, Color.White * 0.8f, Local.shop, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				stackForm3.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.Philosopher_16, Color.White * 0.8f, Local.sell, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}
			foreach (TraderInSeaPoint.Offer {18448} in array)
			{
				bool flag;
				stackForm2.AddItem(new UiControl[]
				{
					this.{18447}({18448}, out flag)
				});
			}
			StackForm stackForm4 = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			StackForm stackForm5 = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			foreach (TraderInSeaPoint.Offer {18448}2 in array2)
			{
				bool flag2;
				UiControl uiControl = this.{18447}({18448}2, out flag2);
				if (flag2)
				{
					stackForm5.AddItem(new UiControl[]
					{
						uiControl
					});
				}
				else
				{
					stackForm4.AddItem(new UiControl[]
					{
						uiControl
					});
				}
			}
			stackForm3.AddItem(new UiControl[]
			{
				stackForm4
			});
			if (stackForm5.GetChildren.Size > 0)
			{
				stackForm3.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.Philosopher_16, Color.White * 0.8f, Local.trader_shipmentToEarn, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				stackForm3.AddItem(new UiControl[]
				{
					stackForm5
				});
			}
			if (Global.Player.MapInfo.IsWorldmap)
			{
				CS$<>8__locals1.esc = Gameplay.ItemsInfo.FromID(15);
				stackForm2.AddItem(new UiControl[]
				{
					this.{18439}(new ImageDecription(CS$<>8__locals1.esc.IconTexture), 0, Local.trader_refreshByEsc, Local.trader_refreshByEsc2(WosbTrading.RefreshTraderOffersByEscudoPrice.Value), delegate
					{
						if (Global.Player.ResourcesOfHold[15] < WosbTrading.RefreshTraderOffersByEscudoPrice.Value)
						{
							new {17312}(Local.item_not_enough(CS$<>8__locals1.esc.Name));
							return;
						}
						CS$<>8__locals1.<>4__this.AllowMouseInput = false;
						GSI resourcesOfHold = Global.Player.ResourcesOfHold;
						resourcesOfHold[15] = resourcesOfHold[15] - WosbTrading.RefreshTraderOffersByEscudoPrice.Value;
						Global.Network.Send(new OnTraderInSeaOpenMsg(null, CS$<>8__locals1.<>4__this.{18458}, OnTraderInSeaOpenMsg.Extra.RefreshByEscudo));
						{19994}.MeAndLogbook({19988}.Minus, string.Concat(new string[]
						{
							"-",
							WosbTrading.RefreshTraderOffersByEscudoPrice.Value.ToString(),
							" ",
							Gameplay.ItemsInfo.FromID(15).Name,
							" ",
							Local.lb_spent
						}), null);
					}, false, default(ImageDecription))
				});
				if (this.{18458} == 0)
				{
					OpenWorldFlag[] array4 = new OpenWorldFlag[3];
					RuntimeHelpers.InitializeArray(array4, fieldof(<PrivateImplementationDetails>.A0D673A85CA2CEE9AFDD1A9BAC40D741D2F8F94C97C59385CDD5B8891D35F237).FieldHandle);
					OpenWorldFlag[] array5 = array4;
					for (int i = 0; i < array5.Length; i++)
					{
						{18417}.<>c__DisplayClass16_1 CS$<>8__locals2 = new {18417}.<>c__DisplayClass16_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						CS$<>8__locals2.flag = array5[i];
						if ((Session.Game.MapMyFraction != null || CS$<>8__locals2.flag != OpenWorldFlag.War) && (Session.Game.MapMyFraction == null || CS$<>8__locals2.flag != OpenWorldFlag.Pirate) && (CS$<>8__locals2.flag != OpenWorldFlag.NoFlag || Session.Game.CanPickNoFlag))
						{
							string text = (CS$<>8__locals2.flag == OpenWorldFlag.NoFlag) ? Local.trader_setPirateFlag_noFlag(CS$<>8__locals2.flag.ToStringLocalFull()) : Local.trader_setPirateFlag(CS$<>8__locals2.flag.ToStringLocalFull());
							Rectangle worldFlagPrerender = CommonAtlas.GetWorldFlagPrerender(CS$<>8__locals2.flag, Session.Game.MapMyFraction.GetValueOrDefault(FractionID.None));
							Form form = this.{18439}(new ImageDecription(CommonAtlas.Texture.Tex, new Rectangle(worldFlagPrerender.X + 12, worldFlagPrerender.Y + 1, 67, 67)), 0, text, Local.trader_setPirateFlag_desc, delegate
							{
								if (Session.CurrentCrewJob != null)
								{
									return;
								}
								string text = text;
								float {25179} = 10000f;
								Action {25180};
								if (({25180} = CS$<>8__locals2.<>9__4) == null)
								{
									{25180} = (CS$<>8__locals2.<>9__4 = delegate()
									{
										CS$<>8__locals2.CS$<>8__locals1.<>4__this.AllowMouseInput = false;
										Global.Network.Send(new OnTraderInSeaOpenMsg(null, CS$<>8__locals2.CS$<>8__locals1.<>4__this.{18458}, OnTraderInSeaOpenMsg.Extra.SetFlags)
										{
											extraPayload = (byte)CS$<>8__locals2.flag
										});
									});
								}
								Session.CurrentCrewJob = new ApplyingEffectCache(text, {25179}, {25180}, () => {18417}.CurrentInstance != null);
							}, false, default(ImageDecription));
							OpenWorldFlag openWorldFlag = Session.Account.WorldFlag.Mapback();
							bool flag = openWorldFlag == OpenWorldFlag.Peaceful || openWorldFlag == OpenWorldFlag.Trader;
							if (!flag)
							{
								form.Opacity = 0.5f;
								form.AllowMouseInput = false;
							}
							stackForm2.AddItem(new UiControl[]
							{
								form
							});
						}
					}
				}
			}
			stackForm.AddItem(new UiControl[]
			{
				stackForm2,
				stackForm3
			});
			{18435}.AddItem(new UiControl[]
			{
				stackForm
			});
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00039C18 File Offset: 0x00037E18
		internal void ReceivedResponse(OnTraderInSeaOpenMsg {18436})
		{
			base.AllowMouseInput = true;
			Session.LastTraderInSeaOffers = {18436}.dataOrNull;
			this.{18457} = true;
			Action refreshCurrentDynamicTabPage = this.RefreshCurrentDynamicTabPage;
			if (refreshCurrentDynamicTabPage == null)
			{
				return;
			}
			refreshCurrentDynamicTabPage();
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00039C43 File Offset: 0x00037E43
		internal void ReceivedResponse(OnMakeArenaUpgrade {18437})
		{
			base.AllowMouseInput = true;
			Action refreshCurrentDynamicTabPage = this.RefreshCurrentDynamicTabPage;
			if (refreshCurrentDynamicTabPage == null)
			{
				return;
			}
			refreshCurrentDynamicTabPage();
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00039C5C File Offset: 0x00037E5C
		internal void StmResponse(OnTraderInSeaOperatingMsg {18438})
		{
			Action refreshCurrentDynamicTabPage = this.RefreshCurrentDynamicTabPage;
			if (refreshCurrentDynamicTabPage != null)
			{
				refreshCurrentDynamicTabPage();
			}
			this.{18457} = true;
			base.AllowMouseInput = true;
			base.Opacity = 1f;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00039C88 File Offset: 0x00037E88
		private Form {18439}(ImageDecription {18440}, int {18441}, string {18442}, string {18443}, Action {18444}, bool {18445} = false, ImageDecription {18446} = default(ImageDecription))
		{
			Form form = new Form(Vector2.Zero, {18445} ? {18417}.c_pageItemU : {18417}.c_pageItem, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			if ({18440}.Tex != null)
			{
				form.AddChild(new Image({18417}.p_pageItem_iconPosition, {18440}.Tex, new Rectangle({18440}.Path.X, {18440}.Path.Y, {18440}.Path.Height, {18440}.Path.Height), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			else
			{
				form.AddChild(new Image({18417}.p_pageItem_iconPosition, AtlasGameGui.Texture.Tex, new Rectangle(2647, 394, 47, 47), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			form.AddChild(new Form({18417}.p_pageItem_iconPosition, {18417}.c_pageItemMaskIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
			form.AddChild(new Label(new Vector2(70f, 5f), Fonts.Arial_12, Color.Wheat * 0.8f, {18442}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			if ({18446}.Tex != null)
			{
				form.AddChild(new Image(new Marker(70f, 25f, 20f, 20f), {18446}.Tex, {18446}.Path, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					Opacity = 0.7f
				});
				form.AddChild(new Label(new Vector2(90f, 26f), Fonts.Arial_12, {18445} ? (new Color(200, 255, 200) * 0.9f) : (Color.White * 0.6f), {18443}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				if ({18445})
				{
					form.AddChild(new Label(new Vector2(140f, 26f), Fonts.Arial_12, new Color(200, 255, 200) * 0.5f, Local.profitable, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				}
			}
			else
			{
				form.AddChild(new Label(new Vector2(70f, 26f), Fonts.Arial_12, Color.White * 0.6f, {18443}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			if ({18441} > 0)
			{
				form.AddChild(new Label(new Vector2(70f, 44f), Fonts.Arial_12, Color.White * 0.5f, {18441}.ToString() + Local.pcs, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			form.EvClick += delegate(ClickUiEventArgs {18464})
			{
				{18444}();
			};
			return form;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00039F10 File Offset: 0x00038110
		private UiControl {18447}(TraderInSeaPoint.Offer {18448}, out bool {18449})
		{
			int num = ({18448}.Item.getType == StorageAssetEnum.SimpleItem) ? Gameplay.ItemsInfo.FromID((int){18448}.Item.ID).MediumCost.Value : (({18448}.Item.getType == StorageAssetEnum.PowderKeg) ? Gameplay.PowderKegsInfo.FromID((int){18448}.Item.ID).ShopCostGold.Value : (({18448}.Item.getType == StorageAssetEnum.Ammo) ? Gameplay.BallsInfo.FromID((int){18448}.Item.ID).ShopCostGold.Value : (({18448}.Item.getType == StorageAssetEnum.Unit_DiplayOnly) ? Gameplay.UnitsInfo.FromID((int){18448}.Item.ID).CostGold.Value : 0)));
			if ({18448}.Item.getType == StorageAssetEnum.SimpleItem && {18448}.Item.ID == 1)
			{
				num++;
			}
			string pricePrefix = ({18448}.PriceID == 0) ? Local.gold : {18448}.PriceRes.Name;
			int priceWithSign = {18448}.PriceWithSign(Session.Account);
			{18449} = ({18448}.PriceID == 0 && num != 0 && ((priceWithSign > 0) ? (priceWithSign > num) : (-priceWithSign < num)));
			bool isProfitableCopy = {18449};
			Action<int> <>9__7;
			Func<int, string> <>9__8;
			Action<int> <>9__9;
			Func<int, string> <>9__10;
			Form form = this.{18439}(new ImageDecription({18448}.Item.getIconTexture), {18448}.CountAbs, (({18448}.Item.getType == StorageAssetEnum.Unit_DiplayOnly) ? (Local.PortEquipUnitsShipWindow_8 + ": ") : "") + {18448}.Item.getName, ({18448}.PriceID == 0) ? Math.Abs(priceWithSign).ToString() : Local.exchange_traderInSea(Math.Abs(priceWithSign), {18448}.PriceRes.Name), delegate
			{
				if ({18448}.Item.getType == StorageAssetEnum.Unit_DiplayOnly)
				{
					int num2 = Math.Min({18448}.CountAbs, Global.Player.UsedShip.CrewPlaces - Global.Player.UsedShipPlayer.Crew.Count);
					if (num2 > 0)
					{
						string getName = {18448}.Item.getName;
						string portEquipUnitsShipWindow_ = Local.PortEquipUnitsShipWindow_8;
						Action<int> {21859};
						if (({21859} = <>9__7) == null)
						{
							{21859} = (<>9__7 = delegate(int {18466})
							{
								base.<ItemSamplerLocal>g__shopComplete|0({18466});
							});
						}
						int {21860} = num2;
						Func<int, string> {21861};
						if (({21861} = <>9__8) == null)
						{
							{21861} = (<>9__8 = ((int {18467}) => ((priceWithSign > 0) ? "+" : "") + (priceWithSign * {18467}).ToString() + " " + pricePrefix));
						}
						new {21838}(getName, portEquipUnitsShipWindow_, {21859}, {21860}, {21861}, null, null, null, null);
						return;
					}
				}
				else
				{
					int num3 = {18448}.CountAbs;
					if (priceWithSign > 0)
					{
						num3 = Math.Min(Global.Player.UsedShipPlayer.GetItemsCountInHold({18448}.Item), num3);
						if (num3 == 0)
						{
							new {17312}(Local.item_not_enough({18448}.Item.getName));
						}
					}
					else if ({18448}.PriceID != 0)
					{
						num3 = Math.Min(Global.Player.UsedShipPlayer.GetItemsCountInHold({18448}.PriceRes) / -priceWithSign, num3);
						if (num3 == 0)
						{
							new {17312}(Local.item_not_enough({18448}.PriceRes.Name));
						}
					}
					else
					{
						num3 = Math.Min(Session.Account.Gold / -priceWithSign, num3);
						if (num3 == 0)
						{
							new {17312}(Local.gold_not_enough);
						}
					}
					if (num3 != 0)
					{
						string getName2 = {18448}.Item.getName;
						string {21858} = (priceWithSign > 0) ? Local.sell : Local.shop;
						Action<int> {21859}2;
						if (({21859}2 = <>9__9) == null)
						{
							{21859}2 = (<>9__9 = delegate(int {18468})
							{
								base.<ItemSamplerLocal>g__shopComplete|0({18468});
								if (isProfitableCopy)
								{
									EducationHelper.MakeFlag(EducationOnboarding.BuyGoodItemsOnTraderInSea, true);
								}
							});
						}
						int {21860}2 = num3;
						Func<int, string> {21861}2;
						if (({21861}2 = <>9__10) == null)
						{
							{21861}2 = (<>9__10 = ((int {18469}) => string.Concat(new string[]
							{
								(priceWithSign > 0) ? "+" : "",
								StringHelper.BigValueHelper(priceWithSign * {18469}),
								" ",
								pricePrefix,
								", ",
								Local.weight_is(((float){18469} * {18448}.Item.getStorageMass).ToString() + " / " + Global.Player.UsedShipPlayer.FreeCapacity.ToString())
							})));
						}
						new {21838}(getName2, {21858}, {21859}2, {21860}2, {21861}2, null, null, null, null);
					}
				}
			}, {18449}, ({18448}.PriceID == 0) ? new ImageDecription(CommonAtlas.Texture.Tex, CommonAtlas.goldIconSingle32) : new ImageDecription({18448}.PriceRes.IconTexture));
			form.AddChild(new LiveLabel(form.Pos.XY + new Vector2(245f, 23f), Fonts.Philosopher_16, Color.Wheat * 0.25f, delegate()
			{
				if (Global.Player.UsedShipPlayer.GetItemsCountInHold({18448}.Item) != 0)
				{
					return Global.Player.UsedShipPlayer.GetItemsCountInHold({18448}.Item).ToString();
				}
				return Local.no_in_hold;
			}, 100));
			{20431}.Set(form, {18448}.Item, 0, null);
			return form;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0003A21C File Offset: 0x0003841C
		protected override void UserUpdate(ref FrameTime {18450})
		{
			base.UserUpdate(ref {18450});
			if ({18450}.EvaluteTimerMs2(ref this.{18453}))
			{
				Global.Network.Send(new OnOpenChestMsg(Array.Empty<byte>(), DateTime.Now, this.{18454}, 0, 0, 0, 0, 0));
			}
			if (this.{18458} > 0 && Global.Game.InteractiveWorldSystem.ContainsWanderingTradersInSea.Size == 0)
			{
				this.BlockAndClose();
			}
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0003A28C File Offset: 0x0003848C
		protected override void UserFrontRender()
		{
			if (this.{18453} != 0f && this.{18455} != null)
			{
				Device gs = Engine.GS;
				Vector2 vector = new Vector2(this.{18455}.Pos.Center.X - (float)(CommonAtlas.progress1_frontOrange.Width / 2), this.{18455}.Pos.XY.Y + this.{18455}.Pos.WH.Y + 30f);
				gs.DrawProgressbar(CommonAtlas.progress1_frontOrange, vector, 1f - this.{18453} / {18417}.c_escudoTime);
			}
			base.UserFrontRender();
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0003A3C0 File Offset: 0x000385C0
		[CompilerGenerated]
		private void {18451}(ListItemViewControl {18452})
		{
			StackForm stackForm = this.{18456};
			if (stackForm != null)
			{
				stackForm.RemoveFromContainer();
			}
			base.AllowMouseInput = true;
			int price = Gameplay.NextArenaUpgradePrice(Global.Player);
			this.{18456} = new StackForm(base.Pos.XY + new Vector2(70f, 29f), UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{18456}.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_14, ((price <= Global.Settings.GamemodeDoublones.Value) ? Color.White : Color.Orange) * 0.8f, Local.nextArenaUpPrice + price.ToString() + " ", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			this.{18456}.AddItem(new UiControl[]
			{
				new Image(new Marker(0f, 0f, 22f, 22f), CommonAtlas.Texture.Tex, CommonAtlas.doublonesIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			{18452}.AddItem(new UiControl[]
			{
				this.{18456}
			});
			int num = 0;
			BlocksStackFormControl blocksStackFormControl = new BlocksStackFormControl(Vector2.Zero, 2, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			using (IEnumerator<ArenaUpgradeInfo> enumerator = ((IEnumerable<ArenaUpgradeInfo>)Gameplay.ArenaUpgrades).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ArenaUpgradeInfo upgrade = enumerator.Current;
					num++;
					ValueTuple<int, int> q = upgrade.GetQuantity(Global.Player);
					if (upgrade.Probability >= 1f || q.Item1 != 0 || HashHelper.greater(CommonGlobal.LoadRandomValue + DateTime.Now.Minute + num) >= upgrade.Probability)
					{
						Form form = this.{18439}(default(ImageDecription), 0, upgrade.Effect.ToString(), "", delegate
						{
							if (q.Item1 < q.Item2 && price <= Global.Settings.GamemodeDoublones.Value)
							{
								LocalSettings settings = Global.Settings;
								settings.GamemodeDoublones.Value = settings.GamemodeDoublones.Value - price;
								Global.Network.Send(new OnMakeArenaUpgrade((byte)upgrade.ID, Global.Player.uID));
								this.AllowMouseInput = false;
							}
						}, upgrade.Probability < 1f, default(ImageDecription));
						form.AddChild(new PointedProgressBar(new Vector2(70f, 26f), CommonAtlas.progressBar16FillPoint, CommonAtlas.progressBar16UnFillPoint, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							MaxValue = q.Item2,
							Value = q.Item1
						});
						string str = "c_redIcon_shiled";
						if (upgrade.Effect.Type == ShipBonusEffect.ServMendingStrength)
						{
							str = "c_redIcon_table";
						}
						if (upgrade.Effect.Type == ShipBonusEffect.BCannonBallFosforEffects)
						{
							str = "c_redIcon_ball";
						}
						if (upgrade.Effect.Type == ShipBonusEffect.PBallDamage)
						{
							str = "c_redIcon_cannon";
						}
						if (upgrade.Effect.Type == ShipBonusEffect.MSpeed || upgrade.Effect.Type == ShipBonusEffect.PSpeed)
						{
							str = "c_redIcon_sailes";
						}
						if (upgrade.Effect.Type == ShipBonusEffect.PPowerupItemsReload)
						{
							str = "c_redIcon_sharp";
						}
						Texture2D {13281} = CommonGlobal.CommonDataContent.Load<Texture2D>("_lzcommon\\Items\\Skills\\" + str);
						form.AddChild(new Image({18417}.p_pageItem_iconPosition, {13281}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
						blocksStackFormControl.AddItem(new UiControl[]
						{
							form
						});
					}
				}
			}
			{18452}.AddItem(new UiControl[]
			{
				blocksStackFormControl
			});
		}

		// Token: 0x0400066E RID: 1646
		public static readonly Rectangle c_pageItem = new Rectangle(813, 2315, 374, 64);

		// Token: 0x0400066F RID: 1647
		public static readonly Rectangle c_pageItemU = new Rectangle(813, 2380, 374, 64);

		// Token: 0x04000670 RID: 1648
		public static readonly Rectangle c_pageItemMaskIcon = new Rectangle(751, 2318, 61, 61);

		// Token: 0x04000671 RID: 1649
		private static readonly Marker p_pageItem_iconPosition = new Marker(4f, 3f, 61f, 61f);

		// Token: 0x04000672 RID: 1650
		private static readonly float c_escudoTime = 1500f;

		// Token: 0x04000673 RID: 1651
		public static {18417} CurrentInstance;

		// Token: 0x04000674 RID: 1652
		private float {18453};

		// Token: 0x04000675 RID: 1653
		private OnOpenChestMsg.Flags {18454};

		// Token: 0x04000676 RID: 1654
		private UiControl {18455};

		// Token: 0x04000677 RID: 1655
		private StackForm {18456};

		// Token: 0x04000678 RID: 1656
		private bool {18457};

		// Token: 0x04000679 RID: 1657
		private int {18458};
	}
}
