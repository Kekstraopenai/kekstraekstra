using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Data;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using WorldOfSeaBattles.Interface.PortPagesUi.Trade;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000282 RID: 642
	internal static class {20431}
	{
		// Token: 0x06000E20 RID: 3616 RVA: 0x00076F04 File Offset: 0x00075104
		public static void Set(UiControl {20432}, IStorageAsset {20433}, int {20434} = 0, string {20435} = null)
		{
			if ({20434} > 0)
			{
				ResourceInfo resourceInfo = {20433} as ResourceInfo;
				if (resourceInfo != null && (resourceInfo.MediumCost.Value < 50 || WosbCrafting.Workshop.Count((WosbCrafting.Recepie {20491}) => {20491}.Output == {20433}) == 0))
				{
					{20434} = 0;
				}
			}
			{20432}.ToolTip = new ToolTip(100f, float.MaxValue, null);
			{20432}.ToolTip.PromisedContent = ((UiControl {20492}) => {20431}.PreviewHandler({20433}, {20434} > 0, false, {20435}));
			if ({20434} > 0)
			{
				Func<WosbCrafting.Recepie, bool> <>9__3;
				Action<TextBlockBuilder> <>9__4;
				Action<ValueTuple<int, int>> <>9__5;
				{20432}.EvClick += delegate(ClickUiEventArgs {20493})
				{
					IEnumerable<WosbCrafting.Recepie> workshop = WosbCrafting.Workshop;
					Func<WosbCrafting.Recepie, bool> predicate;
					if ((predicate = <>9__3) == null)
					{
						predicate = (<>9__3 = ((WosbCrafting.Recepie {20494}) => {20494}.Output == {20433}));
					}
					WosbCrafting.Recepie recepie = workshop.First(predicate);
					CraftingRecipe craftingRecipe;
					if (!({20433} is ResourceInfo))
					{
						craftingRecipe = recepie.Craft;
					}
					else
					{
						(craftingRecipe = new CraftingRecipe(recepie.Craft.InputItems, recepie.Craft.InputMoney)).ReduceCraftCost = {21338}.GetCraftReduce(recepie);
					}
					CraftingRecipe craftingRecipe2 = craftingRecipe;
					bool flag = recepie.NeedsFactoryInPort != null && !Session.Account.ResourcesInPorts.IsFactoryOpened(Global.Player.NearPort.PortID, recepie.NeedsFactoryInPort.Value);
					if (flag)
					{
						Global.Game.ScenePort.workshopHandler(null);
						return;
					}
					if ({17177}.CurrentInstance == null)
					{
						new {17177}(false);
					}
					{17177}.CurrentInstance.CleanTabs();
					{17177} currentInstance = {17177}.CurrentInstance;
					string getName = {20433}.getName;
					bool {17191} = false;
					string {17192} = "";
					Action<TextBlockBuilder> {17193};
					if (({17193} = <>9__4) == null)
					{
						{17193} = (<>9__4 = delegate(TextBlockBuilder {20495})
						{
							{20431}.StorageAssetTb({20433}, {20495}, false, true);
						});
					}
					CraftingRecipe {17194} = craftingRecipe2;
					RTIf craftHours = recepie.CraftHours;
					int value = recepie.OutputItemCount.Value;
					bool {17197} = false;
					Action<ValueTuple<int, int>> {17198};
					if (({17198} = <>9__5) == null)
					{
						{17198} = (<>9__5 = delegate([TupleElementNames(new string[]
						{
							"resCount",
							"btIndex"
						})] ValueTuple<int, int> {20496})
						{
							Session.Account.AddOrRemoveItemsCountInHold({20433}, {20496}.Item1);
						});
					}
					bool {17199} = false;
					string {17200} = null;
					object {17201};
					if (!flag)
					{
						{17201} = null;
					}
					else
					{
						({17201} = new string[1])[0] = Local.not_having_factory;
					}
					currentInstance.SetData(getName, {17191}, {17192}, {17193}, {17194}, craftHours, value, {17197}, {17198}, {17199}, {17200}, {17201}, {20434}, true, flag ? 0 : int.MaxValue, false, -1f);
				};
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00076FC0 File Offset: 0x000751C0
		public static ToolTipState ToolTip(this IStorageAsset {20436})
		{
			Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
			string {12778} = {20431}.GenericItemConnection(tlist, {20436});
			return new ToolTipState({20436}.getName, {12778}, tlist.ToArray());
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00076FF0 File Offset: 0x000751F0
		public static ToolTipState PreviewHandler(IStorageAsset {20437}, bool {20438} = false, bool {20439} = false, string {20440} = null)
		{
			Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
			string {12778} = {20431}.GenericItemConnection(tlist, {20437});
			if ({20438})
			{
				Tlist<ToolTipCharacteristics> tlist2 = tlist;
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_53, CharacteristicsColor.LimeBold);
				tlist2.Add(toolTipCharacteristics);
			}
			if (!string.IsNullOrEmpty({20440}))
			{
				Tlist<ToolTipCharacteristics> tlist3 = tlist;
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics({20440}, CharacteristicsColor.LimeBold);
				tlist3.Add(toolTipCharacteristics);
			}
			int num = {20439} ? Global.Player.UsedShipPlayer.GetItemsCountInHold({20437}) : 0;
			string getName = {20437}.getName;
			string str;
			if (num < 1000)
			{
				str = "";
			}
			else
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 1);
				defaultInterpolatedStringHandler.AppendLiteral(" (");
				defaultInterpolatedStringHandler.AppendFormatted<int>(num);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				str = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			return new ToolTipState(getName + str, {12778}, tlist.ToArray());
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x000770A8 File Offset: 0x000752A8
		public static void StorageAssetTb(IStorageAsset {20441}, TextBlockBuilder {20442}, bool {20443}, bool {20444})
		{
			Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
			string {20466} = {20431}.GenericItemConnection(tlist, {20441});
			if (!{20443})
			{
				{20466} = "";
			}
			if (!{20444})
			{
				tlist.Clear();
			}
			{20431}.DataToTB({20466}, tlist, {20442});
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x000770E0 File Offset: 0x000752E0
		public static ToolTipState ToolTip(this CannonGameInfo {20445}, bool {20446})
		{
			Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
			string {12778} = {20431}.CannonConnection(tlist, {20445}, {20446});
			return new ToolTipState(({20445}.Class == CannonClass.Mortar) ? {20445}.Name : ({20445}.Name + " - " + {20445}.Category.GetName(false)), {12778}, tlist.ToArray());
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00077138 File Offset: 0x00075338
		public static void ToolTip(this CannonGameInfo {20447}, TextBlockBuilder {20448}, bool {20449})
		{
			Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
			{20431}.DataToTB({20431}.CannonConnection(tlist, {20447}, {20449}), tlist, {20448});
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0007715C File Offset: 0x0007535C
		public static ToolTipState ToolTip(this PowerupItemInfo {20450}, bool {20451}, params ToolTipCharacteristics[] {20452})
		{
			Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
			string {12778} = {20431}.PowerupItemConnection(tlist, {20450}, {20451});
			tlist.Add({20452});
			return new ToolTipState({20450}.Name, {12778}, tlist.ToArray());
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00077194 File Offset: 0x00075394
		public static void ToolTip(this PowerupItemInfo {20453}, TextBlockBuilder {20454}, bool {20455}, params ToolTipCharacteristics[] {20456})
		{
			Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
			string {20466} = {20431}.PowerupItemConnection(tlist, {20453}, {20455});
			tlist.Add({20456});
			{20431}.DataToTB({20466}, tlist, {20454});
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x000771C0 File Offset: 0x000753C0
		public static ToolTipState ToolTip(this SpecialUnitInstance {20457})
		{
			Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
			string {12778} = {20431}.GenericItemConnection(tlist, {20457}.GetInfo);
			Tlist<ToolTipCharacteristics> tlist2 = tlist;
			ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.special_crew_salary, {20457}.PayPerHour(Global.Player.UsedShipPlayer.CraftFrom.Rank).ToString() + " " + Local.gold2, CharacteristicsColor.Wheat);
			tlist2.Add(toolTipCharacteristics);
			Tlist<ToolTipCharacteristics> tlist3 = tlist;
			toolTipCharacteristics = new ToolTipCharacteristics(Local.special_unit_contract, StringHelper.TimeDHM((double)({20457}.LeftContractTimeHours * 3600f), true), CharacteristicsColor.Wheat);
			tlist3.Add(toolTipCharacteristics);
			Tlist<ToolTipCharacteristics> tlist4 = tlist;
			toolTipCharacteristics = new ToolTipCharacteristics(Local.special_unit_contract_tt, CharacteristicsColor.Wheat);
			tlist4.Add(toolTipCharacteristics);
			return new ToolTipState({20457}.GetInfo.Name, {12778}, tlist.ToArray());
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00077278 File Offset: 0x00075478
		private static string CannonConnection(Tlist<ToolTipCharacteristics> {20458}, CannonGameInfo {20459}, bool {20460})
		{
			CharacteristicsColor characteristicsColor = CharacteristicsColor.Gray;
			CharacteristicsColor {12729} = ({20459}.Class == CannonClass.HeavyCannon) ? CharacteristicsColor.Lime : characteristicsColor;
			CharacteristicsColor {12729}2 = ({20459}.Class == CannonClass.DistanceCannon) ? CharacteristicsColor.Lime : characteristicsColor;
			CharacteristicsColor {12729}3 = ({20459}.Class == CannonClass.LiteCannon) ? CharacteristicsColor.Lime : characteristicsColor;
			if ({20459}.Feature == CannonFeature.Firegun)
			{
				string resourceOrItemToolTipHelper_ = Local.ResourceOrItemToolTipHelper_14;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted<float>({20459}.Penetration);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(Local.ResourceOrItemToolTipHelper_20);
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(resourceOrItemToolTipHelper_, defaultInterpolatedStringHandler.ToStringAndClear(), characteristicsColor);
				{20458}.Add(toolTipCharacteristics);
				toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_50, ((int)({20459}.ReloadTime / 1000f * 12.5f * 1.5f)).ToString() + Local.StringConstants_80, characteristicsColor);
				{20458}.Add(toolTipCharacteristics);
			}
			else if ({20459}.Class == CannonClass.Mortar)
			{
				string resourceOrItemToolTipHelper_2 = Local.ResourceOrItemToolTipHelper_14;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(0, 2);
				defaultInterpolatedStringHandler2.AppendFormatted<int>({20459}.PenetrationMortar(Global.Player.UsedShipPlayer.CraftFrom));
				defaultInterpolatedStringHandler2.AppendFormatted(({20459}.Feature == CannonFeature.Default) ? "" : "*");
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(resourceOrItemToolTipHelper_2, defaultInterpolatedStringHandler2.ToStringAndClear(), {12729});
				{20458}.Add(toolTipCharacteristics);
			}
			else
			{
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_5(""), {20431}.CannonPenetration({20459}), {12729});
				{20458}.Add(toolTipCharacteristics);
			}
			if ({20459}.Feature == CannonFeature.AlwaysBombshell)
			{
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_bs, characteristicsColor);
				{20458}.Add(toolTipCharacteristics);
			}
			if ({20459}.Class == CannonClass.Mortar)
			{
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_21, ({20459}.ReloadTimeMortar(Global.Player.UsedShipPlayer.CraftFrom) / 1000).ToString() + Local.StringConstants_80, characteristicsColor);
				{20458}.Add(toolTipCharacteristics);
				ValueTuple<int, int> valueTuple = {20459}.DistanceMortar(Global.Player.UsedShipPlayer.CraftFrom);
				string {12727} = Local.distance.TrimEnd() + ": ";
				int num = valueTuple.Item1;
				string str = num.ToString();
				string str2 = "-";
				num = valueTuple.Item2;
				toolTipCharacteristics = new ToolTipCharacteristics({12727}, str + str2 + num.ToString(), characteristicsColor);
				{20458}.Add(toolTipCharacteristics);
				toolTipCharacteristics = new ToolTipCharacteristics(Local.splash_radius, {20459}.SplashRadiusMortar.ToString() ?? "", characteristicsColor);
				{20458}.Add(toolTipCharacteristics);
				toolTipCharacteristics = new ToolTipCharacteristics(Local.reduction, {20459}.Scatter.ToString() + Local.StringConstants_80, characteristicsColor);
				{20458}.Add(toolTipCharacteristics);
				if ({20459}.MortarNeedsPreparation)
				{
					toolTipCharacteristics = new ToolTipCharacteristics(Local.shot_preparation, Math.Round((double)({20459}.MortarPreparationTimeMs(Global.Player.UsedShipPlayer.CraftFrom) / 1000f), 1).ToString() + Local.StringConstants_80, characteristicsColor);
					{20458}.Add(toolTipCharacteristics);
				}
				toolTipCharacteristics = new ToolTipCharacteristics(Local.damage_buildings, "x2", characteristicsColor);
				{20458}.Add(toolTipCharacteristics);
			}
			else
			{
				ToolTipCharacteristics toolTipCharacteristics;
				if ({20459}.Feature != CannonFeature.Firegun)
				{
					toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_21, ({20459}.ReloadTime / 1000f).ToString() + Local.StringConstants_80, {12729}3);
					{20458}.Add(toolTipCharacteristics);
				}
				toolTipCharacteristics = new ToolTipCharacteristics(Local.distance.TrimEnd() + ": ", {20459}.MaxDistance.ToString(), {12729}2);
				{20458}.Add(toolTipCharacteristics);
				toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_22, ((int){20459}.MaxAxisDegree).ToString(), characteristicsColor);
				{20458}.Add(toolTipCharacteristics);
				toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_23, {20459}.Scatter.ToString(), characteristicsColor);
				{20458}.Add(toolTipCharacteristics);
			}
			if ({20459}.Class == CannonClass.Bombardier)
			{
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_25, CharacteristicsColor.Blue);
				{20458}.Add(toolTipCharacteristics);
			}
			if ({20459}.Class == CannonClass.Special)
			{
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_27, CharacteristicsColor.Blue);
				{20458}.Add(toolTipCharacteristics);
			}
			if (PortTradePage_SyncProtected.CurrentInstance != null && {20459}.Class != CannonClass.Mortar)
			{
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.caliber, {20459}.Category.GetNameWithRang(), Global.Player.CraftFrom.IsCannonsAllowed({20459}.Category) ? CharacteristicsColor.Lime : CharacteristicsColor.Orange);
				{20458}.Add(toolTipCharacteristics);
			}
			if ({20460})
			{
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.PortVisaulEquipmentInterface_b, CharacteristicsColor.LimeBold);
				{20458}.Add(toolTipCharacteristics);
			}
			if (WosbTrading.HasHighTradingTax({20459}) != null && !{22279}.IsActive)
			{
				string higher_tax = Local.higher_tax;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(4, 1);
				defaultInterpolatedStringHandler3.AppendLiteral(" (");
				defaultInterpolatedStringHandler3.AppendFormatted<float?>(WosbTrading.HasHighTradingTax({20459}) * (float)100, "F0");
				defaultInterpolatedStringHandler3.AppendLiteral("%)");
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(higher_tax + defaultInterpolatedStringHandler3.ToStringAndClear(), CharacteristicsColor.Gray);
				{20458}.Add(toolTipCharacteristics);
			}
			if ({20459}.Feature == CannonFeature.Firegun)
			{
				return Local.ResourceOrItemToolTipHelper_42_2;
			}
			if ({20459}.Class == CannonClass.Mortar && Global.Player.UsedShip.StaticInfo.MortarPorts.Length != 0 && {20459}.Poundage != null && {20459}.Poundage.Value > Global.Player.UsedShipPlayer.CraftFrom.MaxMortarPoundage.GetValueOrDefault(1000))
			{
				return Local.cannon_poundage_tooltip2(Global.Player.UsedShipPlayer.CraftFrom.MaxMortarPoundage.Value);
			}
			if ({20459}.Class == CannonClass.Mortar)
			{
				if ({20459}.Feature == CannonFeature.Default)
				{
					return Local.mortar_tt;
				}
				if ({20459}.Feature != CannonFeature.PowderKegMortar)
				{
					return Local.mortar_tt_specific_1;
				}
				return Local.mortar_tt_specific_2(Gameplay.PowderKegsInfo.FromID(MortarShot.PowderKegMortarType).Name);
			}
			else
			{
				if ({20459}.Class == CannonClass.LiteCannon)
				{
					return Local.cannons_type_light;
				}
				if ({20459}.Class == CannonClass.DistanceCannon)
				{
					return Local.cannons_type_dist;
				}
				if ({20459}.Class == CannonClass.HeavyCannon)
				{
					return Local.cannons_type_heavy;
				}
				if ({20459}.Class != CannonClass.Bombardier)
				{
					return string.Empty;
				}
				return Local.cannons_type_bomb;
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0007786C File Offset: 0x00075A6C
		public static string GenericItemConnection(Tlist<ToolTipCharacteristics> {20461}, IStorageAsset {20462})
		{
			ResourceInfo t = {20462} as ResourceInfo;
			if (t != null)
			{
				if (!t.IsMap)
				{
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.weight_is(""), t.Mass.ToString(), CharacteristicsColor.Gray);
					{20461}.Add(toolTipCharacteristics);
					toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_3(""), t.MediumCost.ToString(), CharacteristicsColor.Gray);
					{20461}.Add(toolTipCharacteristics);
				}
				if (Global.Player.IsPortEntry && Global.Player.NearPortType == PortEnteringType.Port && Session.CurrentPortAuctionResources[(int)t.ID] > 0 && Session.Account.IsEducationInProgress(EducationOnboarding.AuctionBuyOrder, true, false))
				{
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.count_on_auction, Session.CurrentPortAuctionResources[(int)t.ID].ToString() + Local.pcs, CharacteristicsColor.Gray);
					{20461}.Add(toolTipCharacteristics);
				}
				if (t.FoodValue(Global.Player) > 0f)
				{
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_4_b(""), t.FoodValue(Global.Player).ToString(), CharacteristicsColor.Lime);
					{20461}.Add(toolTipCharacteristics);
				}
				if (t.DestroyInHold > 0f)
				{
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_4, CharacteristicsColor.Orange);
					{20461}.Add(toolTipCharacteristics);
				}
				if (t.DestroyWhenDamage > 0f)
				{
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_4_c, CharacteristicsColor.Orange);
					{20461}.Add(toolTipCharacteristics);
				}
				if (t.CannotBeStoredInPort)
				{
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_4_d, CharacteristicsColor.Orange);
					{20461}.Add(toolTipCharacteristics);
				}
				if (t.DropMode == ResoruceDropMode.DisallowDrop)
				{
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_4_e, CharacteristicsColor.Orange);
					{20461}.Add(toolTipCharacteristics);
				}
				if (t.DropMode == ResoruceDropMode.DisallowDropAndBoarding)
				{
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_4_e1, CharacteristicsColor.Orange);
					{20461}.Add(toolTipCharacteristics);
				}
				if (t.IsTradingItem && t.ID != 25)
				{
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(t.TradingItemEconomyRegion.ToStringLocal() + ": ", CharacteristicsColor.Wheat);
					{20461}.Add(toolTipCharacteristics);
					Tlist<WorldMapRegionInfo> regionsInfo = Gameplay.WorldMap.RegionsInfo;
					Func<WorldMapRegionInfo, bool> <>9__0;
					Func<WorldMapRegionInfo, bool> cretery;
					if ((cretery = <>9__0) == null)
					{
						cretery = (<>9__0 = ((WorldMapRegionInfo {20497}) => {20497}.ItemsEconomyReigion == t.TradingItemEconomyRegion));
					}
					foreach (WorldMapRegionInfo worldMapRegionInfo in regionsInfo.Where(cretery))
					{
						toolTipCharacteristics = new ToolTipCharacteristics("- " + worldMapRegionInfo.Name, CharacteristicsColor.Wheat);
						{20461}.Add(toolTipCharacteristics);
					}
				}
				if (t.CantTransferWithPeacefulFlag)
				{
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_4_k(""), CharacteristicsColor.Orange);
					{20461}.Add(toolTipCharacteristics);
				}
				if (WosbTrading.HasHighTradingTax({20462}) != null)
				{
					string higher_tax = Local.higher_tax;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
					defaultInterpolatedStringHandler.AppendLiteral(" (");
					defaultInterpolatedStringHandler.AppendFormatted<float?>(WosbTrading.HasHighTradingTax({20462}) * (float)100, "F0");
					defaultInterpolatedStringHandler.AppendLiteral("%)");
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(higher_tax + defaultInterpolatedStringHandler.ToStringAndClear(), CharacteristicsColor.Gray);
					{20461}.Add(toolTipCharacteristics);
				}
				if ({20462}.ID == 69 && CalendarEvents.IsActiveExtended)
				{
					TimeSpan t2 = LocalizedDateTime.NowServerTime - CalendarEvents.CurrentEvent.StartDate;
					double totalSeconds = (TimeSpan.FromDays((double)(CalendarEvents.CurrentEvent.Duration + 6)) - t2).TotalSeconds;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.goldfish_expired_tt(StringHelper.TimeD(totalSeconds)), CharacteristicsColor.Orange);
					{20461}.Add(toolTipCharacteristics);
				}
				string text = t.Description;
				if (t.IsTradingItem)
				{
					text = text + ". " + Local.trading_item_generic_tt;
				}
				return text;
			}
			CannonBallInfo cannonBallInfo = {20462} as CannonBallInfo;
			if (cannonBallInfo != null)
			{
				ToolTipCharacteristics toolTipCharacteristics;
				if (cannonBallInfo.AmmoType == CannonAmmoType.MortarBall)
				{
					if (cannonBallInfo.MortarBallCommonDamageToSailes > 0f)
					{
						toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_6(""), CharacteristicsColor.Gray, 2, 3);
						{20461}.Add(toolTipCharacteristics);
					}
					if (cannonBallInfo.MortarBallCommonCrewDamage > 0f)
					{
						toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_7(""), CharacteristicsColor.Gray, 1, 3);
						{20461}.Add(toolTipCharacteristics);
					}
				}
				else if (cannonBallInfo.AmmoType == CannonAmmoType.FalkonetBall)
				{
					toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_18(""), cannonBallInfo.Penetration.ToString(), CharacteristicsColor.Gray);
					{20461}.Add(toolTipCharacteristics);
				}
				else
				{
					if (cannonBallInfo.Penetration != 0f)
					{
						string {12727} = Local.ResourceOrItemToolTipHelper_5("");
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(1, 1);
						defaultInterpolatedStringHandler2.AppendLiteral("+");
						defaultInterpolatedStringHandler2.AppendFormatted<float>(cannonBallInfo.Penetration);
						toolTipCharacteristics = new ToolTipCharacteristics({12727}, defaultInterpolatedStringHandler2.ToStringAndClear(), CharacteristicsColor.Lime);
						{20461}.Add(toolTipCharacteristics);
					}
					string {12727}2 = Local.ResourceOrItemToolTipHelper_41("");
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(1, 1);
					defaultInterpolatedStringHandler3.AppendFormatted<double>(Math.Round((double)(cannonBallInfo.DamageFactor * 100f)));
					defaultInterpolatedStringHandler3.AppendLiteral("%");
					toolTipCharacteristics = new ToolTipCharacteristics({12727}2, defaultInterpolatedStringHandler3.ToStringAndClear(), CharacteristicsColor.Gray);
					{20461}.Add(toolTipCharacteristics);
					toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_6(""), CharacteristicsColor.Gray, (cannonBallInfo.DamageToSailes > 10f) ? 3 : ((cannonBallInfo.DamageToSailes > 7f) ? 2 : 1), 3);
					{20461}.Add(toolTipCharacteristics);
					toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_7(""), CharacteristicsColor.Gray, (cannonBallInfo.DamageToCrew > 2.5f) ? 3 : ((cannonBallInfo.DamageToCrew > 0.5f) ? 2 : ((cannonBallInfo.DamageToCrew > 0f) ? 1 : 0)), 3);
					{20461}.Add(toolTipCharacteristics);
					if (cannonBallInfo.DistanceFactor < 1f)
					{
						string resourceOrItemToolTipHelper_ = Local.ResourceOrItemToolTipHelper_12;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(1, 1);
						defaultInterpolatedStringHandler4.AppendFormatted<int>(-(100 - (int)(cannonBallInfo.DistanceFactor * 100f)));
						defaultInterpolatedStringHandler4.AppendLiteral("%");
						toolTipCharacteristics = new ToolTipCharacteristics(resourceOrItemToolTipHelper_, defaultInterpolatedStringHandler4.ToStringAndClear(), CharacteristicsColor.Orange);
						{20461}.Add(toolTipCharacteristics);
					}
				}
				toolTipCharacteristics = new ToolTipCharacteristics(Local.weight_is(""), cannonBallInfo.MassKg.ToString(), CharacteristicsColor.Gray);
				{20461}.Add(toolTipCharacteristics);
				return cannonBallInfo.Description;
			}
			PowderKegInfo powderKegInfo = {20462} as PowderKegInfo;
			if (powderKegInfo != null)
			{
				string {12727}3 = (powderKegInfo.KegsCount > 1) ? Local.ResourceOrItemToolTipHelper_13 : Local.ResourceOrItemToolTipHelper_14;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler5 = new DefaultInterpolatedStringHandler(0, 1);
				defaultInterpolatedStringHandler5.AppendFormatted<int>(powderKegInfo.DamageL);
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics({12727}3, defaultInterpolatedStringHandler5.ToStringAndClear(), CharacteristicsColor.Gray);
				{20461}.Add(toolTipCharacteristics);
				toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_15(""), powderKegInfo.ReloadSec.ToString() + Local.StringConstants_80, CharacteristicsColor.Gray);
				{20461}.Add(toolTipCharacteristics);
				toolTipCharacteristics = new ToolTipCharacteristics(Local.weight_is(""), powderKegInfo.Mass.ToString(), CharacteristicsColor.Gray);
				{20461}.Add(toolTipCharacteristics);
				if (powderKegInfo.AvailableSinceRank != null)
				{
					toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_16(powderKegInfo.AvailableSinceRank.Value), (Global.Player.CraftFrom.Rank <= powderKegInfo.AvailableSinceRank.Value) ? CharacteristicsColor.Lime : CharacteristicsColor.Orange);
					{20461}.Add(toolTipCharacteristics);
				}
				return powderKegInfo.Description;
			}
			CannonGameInfo cannonGameInfo = {20462} as CannonGameInfo;
			if (cannonGameInfo != null)
			{
				return {20431}.CannonConnection({20461}, cannonGameInfo, false);
			}
			if ({20462} is ProceduralShipInfo)
			{
				ProceduralShipInfo proceduralShipInfo = (ProceduralShipInfo){20462};
				if (proceduralShipInfo.ModificationInfo != null)
				{
					foreach (ShipBonus shipBonus in proceduralShipInfo.ModificationInfo.GetEffects(proceduralShipInfo.ShipInfo))
					{
						ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(shipBonus.ToString(), CharacteristicsColor.Gray);
						{20461}.Add(toolTipCharacteristics);
					}
				}
				return "";
			}
			UnitInfo unitInfo = {20462} as UnitInfo;
			if (unitInfo == null)
			{
				throw new NotSupportedException(({20462} == null) ? "null" : {20462}.getType.ToString());
			}
			if (unitInfo.Type != UnitType.Special)
			{
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.boarding_stats, CharacteristicsColor.Wheat);
				{20461}.Add(toolTipCharacteristics);
				toolTipCharacteristics = new ToolTipCharacteristics(Local.PortEquipUnitsShipWindow_capacity + ": ", unitInfo.Capacity.ToString(), CharacteristicsColor.Gray);
				{20461}.Add(toolTipCharacteristics);
				toolTipCharacteristics = new ToolTipCharacteristics(Local.PortEquipUnitsShipWindow_health + ": ", unitInfo.Health.ToString(), CharacteristicsColor.Gray);
				{20461}.Add(toolTipCharacteristics);
				toolTipCharacteristics = new ToolTipCharacteristics(Local.PortEquipUnitsShipWindow_boardDamage + ": ", (unitInfo.SampleDamage == 0f) ? "-" : unitInfo.SampleDamage.ToString(), CharacteristicsColor.Gray);
				{20461}.Add(toolTipCharacteristics);
			}
			else
			{
				ToolTipCharacteristics toolTipCharacteristics;
				if (unitInfo.Bonus.Value > 0f && unitInfo.Bonus.Type != ShipBonusEffect.Display_ProtectedOfManTheCrew)
				{
					toolTipCharacteristics = new ToolTipCharacteristics(" ", CharacteristicsColor.Gray);
					{20461}.Add(toolTipCharacteristics);
					toolTipCharacteristics = new ToolTipCharacteristics(unitInfo.Bonus.ToString(), CharacteristicsColor.Lime);
					{20461}.Add(toolTipCharacteristics);
				}
				if (unitInfo.BonusPerSailor.Value > 0f)
				{
					toolTipCharacteristics = new ToolTipCharacteristics(" ", CharacteristicsColor.Gray);
					{20461}.Add(toolTipCharacteristics);
					toolTipCharacteristics = new ToolTipCharacteristics(Local.UnitBonus_2 + " ", CharacteristicsColor.Lime);
					{20461}.Add(toolTipCharacteristics);
					toolTipCharacteristics = new ToolTipCharacteristics(unitInfo.BonusPerSailor.ToString(), CharacteristicsColor.Lime);
					{20461}.Add(toolTipCharacteristics);
				}
				if (unitInfo.BonusPerBoardingUnit.Value > 0f)
				{
					toolTipCharacteristics = new ToolTipCharacteristics(" ", CharacteristicsColor.Gray);
					{20461}.Add(toolTipCharacteristics);
					toolTipCharacteristics = new ToolTipCharacteristics(Local.UnitBonus_3 + " ", CharacteristicsColor.Lime);
					{20461}.Add(toolTipCharacteristics);
					toolTipCharacteristics = new ToolTipCharacteristics(unitInfo.BonusPerBoardingUnit.ToString(), CharacteristicsColor.Lime);
					{20461}.Add(toolTipCharacteristics);
				}
				SpecialUnitClass[] array;
				if (WosbCrew.SpecialUnitsConflicts.TryGetValue(unitInfo.SpecialUnitClass, out array))
				{
					foreach (SpecialUnitClass {446} in array)
					{
						toolTipCharacteristics = new ToolTipCharacteristics(Local.special_unit_conflicts_tt(unitInfo.SpecialUnitClass.ToStringLocal(), {446}.ToStringLocal()), CharacteristicsColor.Blue);
						{20461}.Add(toolTipCharacteristics);
					}
				}
				if (unitInfo.Bonus.Type == ShipBonusEffect.BSpecialUnitEndConflicts)
				{
					toolTipCharacteristics = new ToolTipCharacteristics(Local.not_taking_place, CharacteristicsColor.Lime);
					{20461}.Add(toolTipCharacteristics);
				}
				toolTipCharacteristics = new ToolTipCharacteristics(unitInfo.SpecialUnitClass.ToStringLocal(), CharacteristicsColor.Gray);
				{20461}.Add(toolTipCharacteristics);
			}
			if (unitInfo.Type == UnitType.Special)
			{
				return Local.special_unit_tt;
			}
			return unitInfo.Text;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00078350 File Offset: 0x00076550
		private static string PowerupItemConnection(Tlist<ToolTipCharacteristics> {20463}, PowerupItemInfo {20464}, bool {20465})
		{
			CharacteristicsColor {12729} = CharacteristicsColor.Gray;
			if ({20464}.Cooldown.Value > 0)
			{
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_0, {20464}.Cooldown.ToString() + Local.StringConstants_80, {12729});
				{20463}.Add(toolTipCharacteristics);
			}
			if ({20464}.WorkTime.Value > 0)
			{
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_1, {20464}.WorkTime.ToString() + Local.StringConstants_80, {12729});
				{20463}.Add(toolTipCharacteristics);
			}
			if ({20464}.WorkTime.Value > 0 && {20464}.AllowInterrupt)
			{
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_51, Local.ResourceOrItemToolTipHelper_52, {12729});
				{20463}.Add(toolTipCharacteristics);
			}
			if ({20465})
			{
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.quanity, Session.Account.PowerupItemsAtStorage.GetCount({20464}.Index).ToString(), CharacteristicsColor.Wheat);
				{20463}.Add(toolTipCharacteristics);
			}
			if ({20464}.DisallowInLowRangShips != null)
			{
				int rank = Global.Player.UsedShipPlayer.CraftFrom.Rank;
				int? disallowInLowRangShips = {20464}.DisallowInLowRangShips;
				if (rank > disallowInLowRangShips.GetValueOrDefault() & disallowInLowRangShips != null)
				{
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.ResourceOrItemToolTipHelper_16({20464}.DisallowInLowRangShips), CharacteristicsColor.Orange);
					{20463}.Add(toolTipCharacteristics);
				}
			}
			if ({20464}.EffectsWhenInstalled.Size > 0)
			{
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.PowerupItems_EffectsWhenInstalled, CharacteristicsColor.Orange);
				{20463}.Add(toolTipCharacteristics);
				foreach (ShipBonus shipBonus in ((IEnumerable<ShipBonus>){20464}.EffectsWhenInstalled))
				{
					string[] array = shipBonus.ToString().Split(Array.Empty<char>());
					string {12727} = string.Join(" ", RuntimeHelpers.GetSubArray<string>(array, new Range(0, new Index(1, true))));
					string[] array2 = array;
					toolTipCharacteristics = new ToolTipCharacteristics({12727}, array2[array2.Length - 1], {12729});
					{20463}.Add(toolTipCharacteristics);
				}
			}
			return {20464}.Description;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00078564 File Offset: 0x00076764
		private static void DataToTB(string {20466}, Tlist<ToolTipCharacteristics> {20467}, TextBlockBuilder {20468})
		{
			{20466} = {20466}.Replace("#", "");
			{20468}.WriteLines(TextBlockBuilder.CreateBlock(300f, {20466}, Color.Wheat * 0.6f, Fonts.Arial_10, 1f));
			foreach (ToolTipCharacteristics toolTipCharacteristics in ((IEnumerable<ToolTipCharacteristics>){20467}))
			{
				toolTipCharacteristics.WriteTo({20468}, Fonts.Arial_10, Fonts.Arial_10Bold, true);
			}
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x000785F4 File Offset: 0x000767F4
		public static void ToolTip(this CraftingRecipe {20469}, TextBlockBuilder {20470}, int {20471} = 1)
		{
			if ({20469}.InputItems.IsEmpty)
			{
				{20470}.WriteLine(Local.ResourceOrItemToolTipHelper_39, Color.LightGray);
			}
			else
			{
				{20470}.WriteLine(Local.ResourceOrItemToolTipHelper_40, Color.LightGray);
			}
			if (!{20469}.InputItems.IsEmpty)
			{
				{20469}.InputItems.Clone((float){20471} * {20469}.InputItemsMul, RoundMode.Round).WriteToolTipData_ResourcesOnly({20470}, Session.Account.NearPortStorage, true);
			}
			if ({20469}.InputMoney.Value != 0)
			{
				int num = {20469}.InputMoney.Value;
				if ({20471} != 1)
				{
					num *= {20471};
				}
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendLiteral("• ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(num);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(Local.gold2);
				{20470}.WriteLine(defaultInterpolatedStringHandler.ToStringAndClear(), (Session.Account.Gold >= num) ? Color.Lime : Color.OrangeRed);
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x000786E0 File Offset: 0x000768E0
		private static string CannonPenetration(CannonGameInfo {20472})
		{
			return {20472}.Penetration.ToString() + (({20472}.Feature == CannonFeature.DoubleShot) ? " x2" : (({20472}.Feature == CannonFeature.TripleShot) ? " x3" : (({20472}.Feature == CannonFeature.EightShot) ? " x8" : "")));
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00078738 File Offset: 0x00076938
		public static void CannonToolTIp(CannonGameInfo {20473}, UiControl {20474}, bool {20475} = false, bool {20476} = false, [Nullable(2)] CannonGameInstance {20477} = null)
		{
			if ({20473}.Class != CannonClass.Mortar)
			{
				float reloadTime = {20473}.ReloadTime;
			}
			else
			{
				{20473}.ReloadTimeMortar(Global.Player.UsedShipPlayer.CraftFrom);
			}
			if ({20473}.Feature != CannonFeature.Firegun)
			{
				if ({20473}.Class != CannonClass.Mortar)
				{
					Local.ResourceOrItemToolTipHelper_5({20431}.CannonPenetration({20473}));
				}
				else
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 2);
					defaultInterpolatedStringHandler.AppendFormatted(Local.ResourceOrItemToolTipHelper_14);
					defaultInterpolatedStringHandler.AppendFormatted<int>({20473}.PenetrationMortar(Global.Player.UsedShipPlayer.CraftFrom));
					defaultInterpolatedStringHandler.ToStringAndClear();
				}
			}
			else
			{
				Local.ResourceOrItemToolTipHelper_14 + {20473}.Penetration.ToString() + " " + Local.ResourceOrItemToolTipHelper_20;
			}
			Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
			{20431}.CannonConnection(tlist, {20473}, {20476});
			if ({20477} != null && {20477}.Info.Class == CannonClass.Mortar)
			{
				Tlist<ToolTipCharacteristics> tlist2 = tlist;
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.mortar_resource({20477}.RemainReserve), CharacteristicsColor.Gray);
				tlist2.Add(toolTipCharacteristics);
			}
			{20474}.ToolTip = {20431}.ToolTipWithBigImage({20473}.Name, {20473}.IconTexture, ({20473}.Class == CannonClass.Mortar) ? 1f : 0.8f, {20473}, (from {20490} in tlist
			where !{20490}.Left.Contains(Local.higher_tax)
			select {20490}).ToArray<ToolTipCharacteristics>());
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00078884 File Offset: 0x00076A84
		public static void BigIconPreview(UiControl {20478}, Texture2D {20479}, int {20480} = 192, bool {20481} = false, string {20482} = "", params string[] {20483})
		{
			{20478}.ToolTip = new ToolTip(delegate(UiControl {20498})
			{
				ToolTipState toolTipState = new ToolTipState(new TextBlockBuilder(Fonts.Arial_10, 0f));
				toolTipState.AddElement(new Image(new Marker(0f, 0f, (float){20480}, (float){20480}), {20479}, {20479}.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				int num = 0;
				foreach (string text in {20483})
				{
					if (!string.IsNullOrEmpty(text))
					{
						toolTipState.AddElement(new Label(new Vector2(0f, (float)({20480} + 3 + num++ * ({20481} ? 12 : 20))), {20481} ? Fonts.Arial_10 : Fonts.Arial_12, ({20481} && num == 1) ? Color.Wheat : (Color.Wheat * 0.5f), text, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					}
				}
				if (!string.IsNullOrEmpty({20482}))
				{
					toolTipState.AddElement(TextBlockBuilder.CreateBlock((float)({20480} + 30), {20482}, Color.Wheat * 0.4f, {20481} ? Fonts.Arial_10 : Fonts.Arial_12, -1f).Create(new Vector2(0f, (float)({20480} + 3 + num++ * ({20481} ? 12 : 20)))));
				}
				return toolTipState;
			});
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x000788D3 File Offset: 0x00076AD3
		private static ToolTip ToolTipWithBigImage(string {20484}, Texture2D {20485}, float {20486}, [Nullable(2)] CannonGameInfo {20487}, params ToolTipCharacteristics[] {20488})
		{
			return new ToolTip(delegate(UiControl {20499})
			{
				Composer composer = new Composer(200f, -1f);
				composer.FontStrategy.HeaderFont = Fonts.Philosopher_14;
				composer.AddHeader({20484}, null);
				Image image = new Image(new Marker(0f, 0f, 180f, 180f * {20486}), {20485}, new Rectangle(0f, (float){20485}.Height * (1f - {20486}) * 0.75f, (float){20485}.Width, (float){20485}.Height * {20486}, false), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				image.AddChild(new Image(image.Pos.Border(5f), CommonAtlas.Texture.Tex, new Rectangle(2779, 3786, 120, 91), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				if ({20487} != null)
				{
					StackForm stackForm = new StackForm(new Vector2(5f, 20f), UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					stackForm.AddItem(new UiControl[]
					{
						new Image(new Marker(0f, 0f, 20f, 20f), CommonAtlas.Texture.Tex, CommonAtlas.GetCannonClassIcon({20487}.Class, {20487}.Feature == CannonFeature.Firegun), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							BasicColor = new Color(1f, 1f, 1f, 0.5f)
						}
					});
					stackForm.AddSpace(5f);
					if ({20487}.Class != CannonClass.Mortar)
					{
						stackForm.AddItem(new UiControl[]
						{
							new Label(default(Vector2), Fonts.Arial_10Bold, Color.White * 0.8f, {20487}.Class.ToStringLocal(), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
					}
					image.AddChild(stackForm);
				}
				composer.AddUi(image);
				composer.AddSpace(4f);
				foreach (ToolTipCharacteristics toolTipCharacteristics in {20488})
				{
					composer.AddColumnsText(toolTipCharacteristics.Left, toolTipCharacteristics.Right, new ComposerTextStyle(toolTipCharacteristics.ColorToColor, false, Fonts.Arial_10, null));
				}
				return new ToolTipState(composer);
			});
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00078910 File Offset: 0x00076B10
		public static string[] SeparateNames(string {20489})
		{
			int num = {20489}.IndexOf('(');
			int num2 = {20489}.LastIndexOf(')');
			if (num != -1 && num2 > num)
			{
				return new string[]
				{
					{20489}.Substring(0, num),
					{20489}.Substring(num + 1, num2 - num - 1)
				};
			}
			return new string[]
			{
				{20489}
			};
		}
	}
}
