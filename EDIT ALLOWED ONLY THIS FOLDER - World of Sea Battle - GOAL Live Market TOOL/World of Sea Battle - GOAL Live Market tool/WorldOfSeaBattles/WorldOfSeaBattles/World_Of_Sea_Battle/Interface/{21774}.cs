using System;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000356 RID: 854
	internal sealed class {21774} : {21762}
	{
		// Token: 0x060012A9 RID: 4777 RVA: 0x0009D58C File Offset: 0x0009B78C
		public {21774}(int {21799}, UnitInfo {21800}, int {21801}, int {21802}, bool {21803}, Action<int> {21804}, bool {21805} = true)
		{
			{21774}.<>c__DisplayClass2_0 CS$<>8__locals1 = new {21774}.<>c__DisplayClass2_0();
			CS$<>8__locals1.info = {21800};
			CS$<>8__locals1.countInPort = {21799};
			CS$<>8__locals1.toStorage = {21803};
			CS$<>8__locals1.applyCost = {21805};
			CS$<>8__locals1.complete = {21804};
			base..ctor();
			CS$<>8__locals1.<>4__this = this;
			{21774}.<>c__DisplayClass2_1 CS$<>8__locals2 = new {21774}.<>c__DisplayClass2_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			PortEnteringType nearPortType = Global.Player.NearPortType;
			Decorator game = Session.Game;
			int crewAverageLimit = WosbCrew.GetCrewAverageLimit(nearPortType, game);
			{21774}.<>c__DisplayClass2_1 CS$<>8__locals3 = CS$<>8__locals2;
			UnitInfo info = CS$<>8__locals2.CS$<>8__locals1.info;
			PortEnteringType nearPortType2 = Global.Player.NearPortType;
			int countInPort = CS$<>8__locals2.CS$<>8__locals1.countInPort;
			game = Session.Game;
			CS$<>8__locals3.costGold = WosbCrew.GetCrewHirePrice(info, nearPortType2, countInPort, game);
			if (Session.EventActionsPipeline.EventCategoryAmount(EActionCaterory.Epidemy) > 0f)
			{
				{21774}.<>c__DisplayClass2_1 CS$<>8__locals4 = CS$<>8__locals2;
				CS$<>8__locals4.costGold.Value = CS$<>8__locals4.costGold.Value * 2;
			}
			Label label = new Label(base.Pos.XY + new Vector2(39f, 123f), Fonts.Arial_12, Color.Gold, CS$<>8__locals2.costGold.Value.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			UiControl[] array = new UiControl[2];
			array[0] = label;
			int num = 1;
			Marker marker = new Marker(13f, 120f, 24f, 24f);
			Marker marker2 = base.Pos;
			array[num] = new Image(marker.Offset(marker2.XY), CommonAtlas.Texture.Tex, CommonAtlas.goldIconSingle32, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(array);
			{21802} = Math.Min({21802}, CS$<>8__locals2.CS$<>8__locals1.countInPort);
			{21801} = Math.Min({21801}, {21802});
			this.{21824} = new RTI({21801});
			this.{21825} = new RTI({21802});
			LiveLabel liveLabel = new LiveLabel(base.Pos.XY + new Vector2(14f, 14f), Fonts.Philosopher_14, Color.Wheat, delegate()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
				defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals2.CS$<>8__locals1.info.Name);
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted(Local.available);
				defaultInterpolatedStringHandler.AppendLiteral(": ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals2.CS$<>8__locals1.countInPort - CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21824}.Value);
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}, 50);
			{21774}.<>c__DisplayClass2_1 CS$<>8__locals5 = CS$<>8__locals2;
			marker2 = liveLabel.Pos;
			CS$<>8__locals5.quantityBar = new ProgressBar(new Vector2(marker2.End.X + 5f, base.Pos.XY.Y + 14f + 3f), new Rectangle(2141, 161, 180, 13), AtlasPortGui.transpPixel, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals2.quantityBar.MaxValue = (float)crewAverageLimit;
			CS$<>8__locals2.quantityBar.Value = (float)(CS$<>8__locals2.CS$<>8__locals1.countInPort - this.{21824}.Value);
			base.AddChild(new UiControl[]
			{
				liveLabel,
				CS$<>8__locals2.quantityBar,
				new Label(liveLabel.Pos.XY + new Vector2(0f, 25f), Fonts.Arial_10, Color.DimGray * 0.7f, Local.units_limit_tt, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			int num2 = CS$<>8__locals2.CS$<>8__locals1.toStorage ? 70 : 40;
			this.acceptButton = new Button(base.Pos.XY + {21762}.Downbar_Button1Position, {21762}.Pattern_DownbarButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			UiControl acceptButton = this.acceptButton;
			marker2 = this.acceptButton.Pos;
			marker2 = marker2.Offset((float)(-(float)num2), 0f);
			acceptButton.Pos = marker2.SetWidth(this.acceptButton.Pos.WH.X + (float)num2);
			base.AddChild(this.acceptButton);
			base.WriteEmptyLine();
			this.switchersCount += 0.5f;
			base.AddNodeSwitcherToEnd(Local.quanity, {21801}, {21802}, delegate(int {21826})
			{
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21824}.Value = {21826};
				int num3 = CS$<>8__locals2.costGold.Value * CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21824}.Value;
				CS$<>8__locals2.quantityBar.Value = (float)(CS$<>8__locals2.CS$<>8__locals1.countInPort - CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21824}.Value);
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.acceptButton.SetInnerContent(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.Arial_12, Color.White, Local.PortEquipUnitsShipWindow_8 + (CS$<>8__locals2.CS$<>8__locals1.toStorage ? Local.tp_port_units : "") + " ", PositionAlignment.LeftUp, PositionAlignment.LeftUp),
					new Image(new Marker(0f, 0f, 24f, 24f).Offset(CS$<>8__locals2.CS$<>8__locals1.<>4__this.Pos.XY), CommonAtlas.Texture.Tex, CommonAtlas.goldIconSingle32, PositionAlignment.LeftUp, PositionAlignment.LeftUp),
					new Label(Vector2.Zero, Fonts.Arial_12, (Session.Account.Gold < num3) ? Color.OrangeRed : Color.Gold, StringHelper.BigValueHelper(num3), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}, true);
			this.acceptButton.EvClick += delegate(ClickUiEventArgs {21827})
			{
				if (CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21824}.Value <= CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21825}.Value)
				{
					if (CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21824}.Value > 0)
					{
						int num3 = CS$<>8__locals2.costGold.Value * CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21824}.Value;
						if (Session.Account.Gold < num3)
						{
							return;
						}
						if (CS$<>8__locals2.CS$<>8__locals1.applyCost)
						{
							Session.Account.Gold -= num3;
						}
						CS$<>8__locals2.CS$<>8__locals1.complete(CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21824}.Value);
					}
					CS$<>8__locals2.CS$<>8__locals1.<>4__this.BlockAndClose();
				}
			};
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0009D944 File Offset: 0x0009BB44
		public {21774}(TradeOrderCommon {21806}, bool {21807}, [TupleElementNames(new string[]
		{
			"count",
			"byMonets"
		})] Action<ValueTuple<int, bool>> {21808})
		{
			{21774}.<>c__DisplayClass3_0 CS$<>8__locals1 = new {21774}.<>c__DisplayClass3_0();
			CS$<>8__locals1.order = {21806};
			CS$<>8__locals1.isBuying = {21807};
			CS$<>8__locals1.complete = {21808};
			base..ctor();
			CS$<>8__locals1.<>4__this = this;
			base.AddChild(new Label(base.Pos.XY + new Vector2(14f, 14f), Fonts.Philosopher_16, Color.Gray, CS$<>8__locals1.order.ItemInfo.getName, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			CS$<>8__locals1.pickedGold = CS$<>8__locals1.order.Price.IsGold;
			CS$<>8__locals1.costAdd = (CS$<>8__locals1.isBuying ? "-" : "+");
			CS$<>8__locals1.showCost = new Label(base.Pos.XY + new Vector2(39f, 123f), Fonts.Arial_12, Color.Gold, "0", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(CS$<>8__locals1.showCost);
			Marker marker = new Marker(13f, 120f, 24f, 24f);
			Marker marker2 = base.Pos;
			Image image = new Image(marker.Offset(marker2.XY), CommonAtlas.Texture.Tex, CommonAtlas.goldIconSingle32, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			marker2 = new Marker(13f, 120f, 24f, 24f);
			marker = base.Pos;
			Image image2 = new Image(marker2.Offset(marker.XY), CommonAtlas.Texture.Tex, CommonAtlas.monetsIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(new UiControl[]
			{
				image,
				image2
			});
			image.UpdateComplete += delegate(UiControl {21828})
			{
				{21828}.IsVisible = CS$<>8__locals1.pickedGold;
			};
			image2.UpdateComplete += delegate(UiControl {21829})
			{
				{21829}.IsVisible = !CS$<>8__locals1.pickedGold;
			};
			CS$<>8__locals1.order.CurrentCount;
			this.{21825} = new RTI(CS$<>8__locals1.order.CurrentCount);
			this.{21824} = new RTI(this.{21825}.Value);
			CS$<>8__locals1.maxCountSelling = 0;
			if (!CS$<>8__locals1.isBuying)
			{
				CS$<>8__locals1.maxCountSelling = Session.Account.GetItemsCountInStorage(CS$<>8__locals1.order.ItemInfo);
				this.{21824}.Value = Math.Min(this.{21824}.Value, CS$<>8__locals1.maxCountSelling.Value);
			}
			CS$<>8__locals1.showCost.Text = " ";
			CS$<>8__locals1.<.ctor>g__UpdateText|2();
			base.WriteEmptyLine();
			this.acceptButton = new Button(base.Pos.XY + {21762}.Downbar_Button1Position, {21762}.Pattern_DownbarButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(this.acceptButton);
			base.AddNodeSwitcherToEnd(Local.quanity, this.{21824}.Value, CS$<>8__locals1.order.CurrentCount, delegate(int {21830})
			{
				CS$<>8__locals1.<>4__this.{21824}.Value = {21830};
				base.<.ctor>g__UpdateText|2();
				if (!CS$<>8__locals1.isBuying)
				{
					CS$<>8__locals1.<>4__this.acceptButton.AllowMouseInput = (CS$<>8__locals1.<>4__this.{21824}.Value <= CS$<>8__locals1.maxCountSelling.Value);
					CS$<>8__locals1.<>4__this.acceptButton.Opacity = (CS$<>8__locals1.<>4__this.acceptButton.AllowMouseInput ? 1f : 0.4f);
				}
			}, true);
			if (CS$<>8__locals1.isBuying && CS$<>8__locals1.order.Price.IsGold && WosbTrading.AllowTradeByMonents(CS$<>8__locals1.order.ItemInfo) && !PlatformTuning.DisableShop)
			{
				CheckboxControl checkboxControl = new CheckboxControl(new Vector2(230f, 120f) + base.Pos.XY, AtlasPortGui.vd_checkBox_26px_true, AtlasPortGui.vd_checkBox_26px_false, false, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				checkboxControl.SetText(Local.for_monets, Fonts.Arial_12, Color.White * 0.5f);
				checkboxControl.ToolTipState = new ToolTipState("", Local.but_lot_for_monets, Array.Empty<ToolTipCharacteristics>());
				checkboxControl.EvCheck += delegate(CheckboxCheckedEventArgs {21831})
				{
					CS$<>8__locals1.pickedGold = !{21831}.NewValue;
					base.<.ctor>g__UpdateText|2();
				};
				base.AddChild(checkboxControl);
			}
			this.acceptButton.SetText(CS$<>8__locals1.isBuying ? Local.shop : Local.sell, Fonts.Arial_12, Color.White, false);
			this.acceptButton.EvClick += delegate(ClickUiEventArgs {21832})
			{
				if (CS$<>8__locals1.<>4__this.{21824}.Value <= CS$<>8__locals1.<>4__this.{21825}.Value && (CS$<>8__locals1.isBuying || CS$<>8__locals1.<>4__this.{21824}.Value <= CS$<>8__locals1.maxCountSelling.Value))
				{
					if (CS$<>8__locals1.<>4__this.{21824}.Value > 0)
					{
						if (CS$<>8__locals1.isBuying)
						{
							int num = CS$<>8__locals1.pickedGold ? ((int)MathF.Ceiling(CS$<>8__locals1.order.Price.CostPerUnit * (float)CS$<>8__locals1.<>4__this.{21824}.Value)) : CS$<>8__locals1.order.Price.TotalCostConvertedToMonets(CS$<>8__locals1.<>4__this.{21824}.Value);
							if (CS$<>8__locals1.pickedGold ? (Session.Account.Gold < num) : (Session.Account.Monets.Value < num))
							{
								if (!CS$<>8__locals1.pickedGold)
								{
									{21395}.Open(num - Session.Account.Monets.Value);
								}
								return;
							}
						}
						CS$<>8__locals1.complete(new ValueTuple<int, bool>(CS$<>8__locals1.<>4__this.{21824}.Value, !CS$<>8__locals1.pickedGold));
					}
					CS$<>8__locals1.<>4__this.BlockAndClose();
				}
			};
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0009DD1C File Offset: 0x0009BF1C
		public {21774}(string {21809}, float {21810}, GSI {21811}, int {21812}, int {21813}, bool {21814}, Action<int> {21815}, bool {21816} = false)
		{
			{21774} <>4__this = this;
			if ({21810} == 0f && {21811}.IsEmpty)
			{
				throw new ArgumentException();
			}
			ResourceInfo resInfo = null;
			if (!{21811}.IsEmpty)
			{
				resInfo = Gameplay.ItemsInfo.FromID({21811}.RandomName());
			}
			string costAdd = {21814} ? "-" : "+";
			Label showCost = new Label(base.Pos.XY + new Vector2(39f, 123f), Fonts.Arial_12, Color.Gold, "0", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(showCost);
			if ({21810} > 0f)
			{
				Marker marker = new Marker(13f, 120f, 24f, 24f);
				Marker marker2 = base.Pos;
				base.AddChild(new Image(marker.Offset(marker2.XY), CommonAtlas.Texture.Tex, CommonAtlas.goldIconSingle32, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			else
			{
				Marker marker2 = new Marker(13f, 120f, 24f, 24f);
				Marker marker = base.Pos;
				base.AddChild(new Image(marker2.Offset(marker.XY), resInfo.IconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			{21812} = Math.Min({21812}, {21813});
			this.{21824} = new RTI({21812});
			this.{21825} = new RTI({21813});
			showCost.Text = " ";
			if ({21814} && (float)Session.Account.Gold < {21810} * (float)this.{21824}.Value)
			{
				showCost.BasicColor = Color.OrangeRed;
			}
			base.WriteEmptyLine();
			base.AddNodeSwitcherToEnd(Local.quanity, {21812}, {21813}, delegate(int {21833})
			{
				<>4__this.{21824}.Value = {21833};
				float num = ({21810} + (float){21811}.GetTotalItemsCount()) * (float)<>4__this.{21824}.Value;
				showCost.Text = costAdd + StringHelper.BigValueHelper((int)Math.Floor((double)num)) + "x " + (({21810} == 0f) ? resInfo.Name : Local.gold2);
				if ({21814} && (({21810} > 0f) ? ((float)Session.Account.Gold < num) : ((float)Session.Account.NearPortStorage.GetCount((int)resInfo.ID) < num)))
				{
					showCost.BasicColor = Color.OrangeRed;
					return;
				}
				showCost.BasicColor = Color.Gold;
			}, true);
			this.acceptButton = new Button(base.Pos.XY + {21762}.Downbar_Button1Position, {21762}.Pattern_DownbarButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(this.acceptButton);
			this.acceptButton.SetText({21814} ? ({21816} ? Local.PortEquipUnitsShipWindow_8 : Local.shop) : Local.sell, Fonts.Arial_12, Color.White, false);
			this.acceptButton.EvClick += delegate(ClickUiEventArgs {21834})
			{
				if (<>4__this.{21824}.Value <= <>4__this.{21825}.Value)
				{
					if (<>4__this.{21824}.Value > 0)
					{
						float num = ({21810} + (float){21811}.GetTotalItemsCount()) * (float)<>4__this.{21824}.Value;
						if ({21814} && (({21810} > 0f) ? ((float)Session.Account.Gold < num) : ((float)Session.Account.NearPortStorage.GetCount((int)resInfo.ID) < num)))
						{
							return;
						}
						{21815}(<>4__this.{21824}.Value);
					}
					<>4__this.BlockAndClose();
				}
			};
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0009DFBC File Offset: 0x0009C1BC
		public {21774}(ResourceInfo {21817}, bool {21818}, PortLiveTrading {21819}, int {21820}, Action<int, bool> {21821}, int? {21822} = null)
		{
			{21774}.<>c__DisplayClass5_0 CS$<>8__locals1 = new {21774}.<>c__DisplayClass5_0();
			CS$<>8__locals1.liveTrading = {21819};
			CS$<>8__locals1.staticCost = {21820};
			CS$<>8__locals1.resInfo = {21817};
			CS$<>8__locals1.isShop = {21818};
			CS$<>8__locals1.completeIsStorage = {21821};
			base..ctor();
			CS$<>8__locals1.<>4__this = this;
			Label showCost = new Label(base.Pos.XY + new Vector2(39f, 123f), Fonts.Arial_12, Color.Gold, "0", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(showCost);
			base.AddChild(new Image(new Marker(13f, 120f, 24f, 24f).Offset(base.Pos.XY), CommonAtlas.Texture.Tex, CommonAtlas.goldIconSingle32, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			bool isInPort = Global.Player.IsPortEntry;
			int num = int.MaxValue;
			if (CS$<>8__locals1.isShop)
			{
				if (CS$<>8__locals1.liveTrading == null)
				{
					num = (Math.Max(0, Session.Account.Gold) - 1) / CS$<>8__locals1.staticCost;
				}
				else
				{
					int num2;
					int num3;
					CS$<>8__locals1.liveTrading.MaxOperationSize((int)CS$<>8__locals1.resInfo.ID, Session.EventActionsPipeline, (float)Math.Max(0, Session.Account.Gold), out num2, out num3);
					num = num2;
				}
				if (!isInPort && CS$<>8__locals1.resInfo != null)
				{
					num = Math.Min(num, (int)(Global.Player.UsedShip.Capacity / CS$<>8__locals1.resInfo.Mass));
				}
			}
			else
			{
				if (CS$<>8__locals1.liveTrading != null)
				{
					int num4;
					int num5;
					CS$<>8__locals1.liveTrading.MaxOperationSize((int)CS$<>8__locals1.resInfo.ID, Session.EventActionsPipeline, (float)Math.Max(0, Session.Account.Gold), out num4, out num5);
					num = num5;
				}
				num = Math.Min(num, isInPort ? Session.Account.NearPortStorage.GetCount((int)CS$<>8__locals1.resInfo.ID) : Global.Player.ResourcesOfHold.GetCount((int)CS$<>8__locals1.resInfo.ID));
			}
			if ({21822} != null)
			{
				num = Math.Min(num, {21822}.Value);
			}
			int num6 = Math.Min(100, num);
			this.{21824} = new RTI(num6);
			this.{21825} = new RTI(num);
			Label weightLabel = new Label(base.Pos.XY + new Vector2(374f, 80f), Fonts.Arial_12, Color.Gray * 0.6f, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(weightLabel);
			base.WriteEmptyLine();
			base.AddNodeSwitcherToEnd(Local.quanity, num6, num, delegate(int {21836})
			{
				CS$<>8__locals1.<>4__this.{21824}.Value = {21836};
				int num7 = (CS$<>8__locals1.liveTrading == null) ? (CS$<>8__locals1.<>4__this.{21824}.Value * CS$<>8__locals1.staticCost) : CS$<>8__locals1.liveTrading.TotalPrice((int)CS$<>8__locals1.resInfo.ID, CS$<>8__locals1.isShop ? (-CS$<>8__locals1.<>4__this.{21824}.Value) : CS$<>8__locals1.<>4__this.{21824}.Value, Session.EventActionsPipeline);
				showCost.Text = (CS$<>8__locals1.isShop ? "-" : "+") + StringHelper.BigValueHelper(num7) + " " + Local.gold2;
				if (CS$<>8__locals1.isShop && Session.Account.Gold < num7)
				{
					showCost.BasicColor = Color.OrangeRed;
				}
				else
				{
					showCost.BasicColor = Color.Gold;
				}
				weightLabel.Text = Local.weight_is(StringHelper.BigValueHelper((int)(CS$<>8__locals1.resInfo.Mass * (float)CS$<>8__locals1.<>4__this.{21824}.Value)));
			}, true);
			this.acceptButton = new Button(base.Pos.XY + {21762}.Downbar_Button1Position, {21762}.Pattern_DownbarButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(this.acceptButton);
			this.acceptButton.SetText(CS$<>8__locals1.isShop ? (Local.shop + (isInPort ? (" " + Local.to_hold) : "")) : Local.sell, Fonts.Arial_12, Color.White, false);
			this.acceptButton.EvClick += delegate(ClickUiEventArgs {21837})
			{
				if (CS$<>8__locals1.<>4__this.{21824}.Value <= CS$<>8__locals1.<>4__this.{21825}.Value)
				{
					if (CS$<>8__locals1.<>4__this.{21824}.Value > 0)
					{
						int num7 = (CS$<>8__locals1.liveTrading == null) ? (CS$<>8__locals1.<>4__this.{21824}.Value * CS$<>8__locals1.staticCost) : CS$<>8__locals1.liveTrading.TotalPrice((int)CS$<>8__locals1.resInfo.ID, CS$<>8__locals1.isShop ? (-CS$<>8__locals1.<>4__this.{21824}.Value) : CS$<>8__locals1.<>4__this.{21824}.Value, Session.EventActionsPipeline);
						if (CS$<>8__locals1.isShop && Session.Account.Gold < num7)
						{
							return;
						}
						CS$<>8__locals1.completeIsStorage(CS$<>8__locals1.<>4__this.{21824}.Value, isInPort);
					}
					CS$<>8__locals1.<>4__this.BlockAndClose();
				}
			};
			if (CS$<>8__locals1.isShop & isInPort)
			{
				Button button = new Button(base.Pos.XY + {21762}.Downbar_Button1Position - new Vector2((float){21762}.Pattern_DownbarButton.Width, 0f), {21762}.Pattern_DownbarButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button.SetText(Local.PortNumerticInputBasicWindow_1, Fonts.Arial_12, Color.White, false);
				button.EvClick += delegate(ClickUiEventArgs {21835})
				{
					if (CS$<>8__locals1.<>4__this.{21824}.Value <= CS$<>8__locals1.<>4__this.{21825}.Value)
					{
						if (CS$<>8__locals1.<>4__this.{21824}.Value > 0)
						{
							int num7 = (CS$<>8__locals1.liveTrading == null) ? (CS$<>8__locals1.<>4__this.{21824}.Value * CS$<>8__locals1.staticCost) : CS$<>8__locals1.liveTrading.TotalPrice((int)CS$<>8__locals1.resInfo.ID, CS$<>8__locals1.isShop ? (-CS$<>8__locals1.<>4__this.{21824}.Value) : CS$<>8__locals1.<>4__this.{21824}.Value, Session.EventActionsPipeline);
							if (CS$<>8__locals1.isShop && Session.Account.Gold < num7)
							{
								return;
							}
							CS$<>8__locals1.completeIsStorage(CS$<>8__locals1.<>4__this.{21824}.Value, false);
						}
						CS$<>8__locals1.<>4__this.BlockAndClose();
					}
				};
				base.AddChild(button);
			}
			if (CS$<>8__locals1.liveTrading != null)
			{
				Label label = new Label(base.Pos.XY + new Vector2(14f, 14f), Fonts.Philosopher_14Bold, Color.Gray, CS$<>8__locals1.resInfo.Name + ":", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				base.AddChild(label);
				base.AddChild(new LiveLabel(label.Pos.XY + new Vector2(label.Pos.WH.X + 5f, 1f), Fonts.Arial_12, Color.Gray * 0.8f, () => Local.TradePortInterface_60 + (CS$<>8__locals1.liveTrading.CurrentCount.GetCount((int)CS$<>8__locals1.resInfo.ID) + (CS$<>8__locals1.isShop ? (-CS$<>8__locals1.<>4__this.{21824}.Value) : CS$<>8__locals1.<>4__this.{21824}.Value)).ToString(), 1));
			}
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0009E4AB File Offset: 0x0009C6AB
		protected override void UserUpdate(ref FrameTime {21823})
		{
			base.UserUpdate(ref {21823});
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0009E4B4 File Offset: 0x0009C6B4
		protected override void UserBackRender()
		{
			base.UserBackRender();
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0009E4BC File Offset: 0x0009C6BC
		protected override void UserFrontRender()
		{
			base.UserFrontRender();
		}

		// Token: 0x040010F2 RID: 4338
		private RTI {21824};

		// Token: 0x040010F3 RID: 4339
		private readonly RTI {21825};
	}
}
