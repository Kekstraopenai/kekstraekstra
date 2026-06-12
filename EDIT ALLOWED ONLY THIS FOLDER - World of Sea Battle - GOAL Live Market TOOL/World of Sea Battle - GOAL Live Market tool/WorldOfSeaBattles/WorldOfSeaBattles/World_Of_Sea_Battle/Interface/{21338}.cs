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
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000308 RID: 776
	internal sealed class {21338} : {20856}, IPortPage
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x0008E8FF File Offset: 0x0008CAFF
		// (set) Token: 0x0600110F RID: 4367 RVA: 0x0008E906 File Offset: 0x0008CB06
		public static {21338} CurrentInstance { get; private set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x000030FD File Offset: 0x000012FD
		public bool CreateChatUi
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x000030FD File Offset: 0x000012FD
		public bool CreateShipStatUi
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0008E90E File Offset: 0x0008CB0E
		public {21338}() : base({21338}.GetData())
		{
			{21338}.CurrentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{21338}.CurrentInstance = null;
				{17177} currentInstance = {17177}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.BlockAndClose();
			};
			EducationHelper.MakeFlag(EducationOnboarding.OpenWorkshop, true);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0008E950 File Offset: 0x0008CB50
		public static float GetCraftReduce(WosbCrafting.Recepie {21340})
		{
			if (!{21340}.OutputIsGold && {21340}.Output.getType == StorageAssetEnum.SimpleItem)
			{
				if ({21340}.Output.ID == 14 || {21340}.Output.ID == 16)
				{
					if (Session.Account.CaptainSkills[PDynamicAccountBonus.CRoleSmithevel] == 0)
					{
						return 0f;
					}
					return 0.1f;
				}
				else if ({21340}.Output.ID == 29 || {21340}.Output.ID == 27)
				{
					if (Session.Account.CaptainSkills[PDynamicAccountBonus.CRolePlankingLevel] == 0)
					{
						return 0f;
					}
					return 0.07f;
				}
			}
			return 0f;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0008E9F4 File Offset: 0x0008CBF4
		private static {20856}.UserContents[] GetData()
		{
			if (CalendarEvents.IsActive)
			{
				WosbCrafting.Workshop.First(delegate(WosbCrafting.Recepie {21362})
				{
					ResourceInfo resourceInfo = {21362}.Output as ResourceInfo;
					return resourceInfo != null && resourceInfo.ID == 36;
				}).Craft.InputMoney = 25;
			}
			{20856}.UserContents[] array = new {20856}.UserContents[3];
			int num = 0;
			string portWorkshopPage_ = Local.PortWorkshopPage_0;
			Action<StackForm> {20876};
			if (({20876} = {21338}.<>O.<0>__PageCraft) == null)
			{
				{20876} = ({21338}.<>O.<0>__PageCraft = new Action<StackForm>({21338}.PageCraft));
			}
			array[num] = new {20856}.UserContents(portWorkshopPage_, {20876});
			int num2 = 1;
			string portWorkshopPage_2 = Local.PortWorkshopPage_1;
			Action<StackForm> {20876}2;
			if (({20876}2 = {21338}.<>O.<1>__PageRecycle) == null)
			{
				{20876}2 = ({21338}.<>O.<1>__PageRecycle = new Action<StackForm>({21338}.PageRecycle));
			}
			array[num2] = new {20856}.UserContents(portWorkshopPage_2, {20876}2);
			int num3 = 2;
			string portWorkshopPage_0_b = Local.PortWorkshopPage_0_b;
			Action<StackForm> {20876}3;
			if (({20876}3 = {21338}.<>O.<2>__Create) == null)
			{
				{20876}3 = ({21338}.<>O.<2>__Create = new Action<StackForm>({21314}.Create));
			}
			array[num3] = new {20856}.UserContents(portWorkshopPage_0_b, {20876}3);
			return array;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0008EACC File Offset: 0x0008CCCC
		public static void OpenCraft(WosbCrafting.Recepie {21341})
		{
			{21338}.OpenWorkshopItemCraft({21341}, false, "", false);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0008EADC File Offset: 0x0008CCDC
		private static void OpenWorkshopItemCraft(WosbCrafting.Recepie {21342}, bool {21343}, string {21344}, bool {21345} = true)
		{
			string text = {21342}.OutputIsGold ? Gameplay.ItemsInfo.FromID(24).Name : ({21342}.Output.getName + {21344});
			ResourceInfo resourceInfo = {21342}.Output as ResourceInfo;
			if (resourceInfo != null && resourceInfo.ID == 67)
			{
				text = Gameplay.ItemsInfo.FromID(68).Name;
			}
			CraftingRecipe craftingRecipe = new CraftingRecipe({21342}.Craft.InputItems, {21342}.Craft.InputMoney.Value)
			{
				ReduceCraftCost = {21338}.GetCraftReduce({21342})
			};
			RTIf craftHours = {21342}.CraftHours;
			if ({17177}.CurrentInstance == null)
			{
				new {17177}(true);
			}
			if ({21345})
			{
				{17177}.CurrentInstance.CleanTabs();
				ResourceInfo resourceInfo2 = {21342}.Output as ResourceInfo;
				if (resourceInfo2 != null)
				{
					if (resourceInfo2.ID == 68)
					{
						{17177}.CurrentInstance.AddTabs(delegate(int {21371})
						{
							if ({21371} == 0)
							{
								{21338}.OpenWorkshopItemCraft({21342}, {21343}, {21344}, false);
								return;
							}
							{21338}.OpenWorkshopItemCraft(WosbCrafting.Workshop.First(delegate(WosbCrafting.Recepie {21363})
							{
								ResourceInfo resourceInfo3 = {21363}.Output as ResourceInfo;
								return resourceInfo3 != null && resourceInfo3.ID == 67;
							}), {21343}, {21344}, false);
						}, 0, new string[]
						{
							Local.create,
							Local.CommonItemCraftUi_5
						});
						return;
					}
					if (resourceInfo2.ID == 24)
					{
						{17177}.CurrentInstance.AddTabs(delegate(int {21372})
						{
							if ({21372} == 0)
							{
								{21338}.OpenWorkshopItemCraft({21342}, {21343}, {21344}, false);
								return;
							}
							{21338}.OpenWorkshopItemCraft(WosbCrafting.Workshop.First((WosbCrafting.Recepie {21364}) => {21364}.OutputIsGold), {21343}, {21344}, false);
						}, 0, new string[]
						{
							Local.create,
							Local.CommonItemCraftUi_5
						});
						return;
					}
				}
			}
			bool flag = false;
			{17177} currentInstance = {17177}.CurrentInstance;
			string {17190} = text;
			bool {17191} = false;
			string {17192} = "";
			Action<TextBlockBuilder> {17193} = delegate(TextBlockBuilder {21373})
			{
				ResourceInfo resourceInfo3 = {21342}.Output as ResourceInfo;
				if (resourceInfo3 != null && resourceInfo3.ID == 67)
				{
					{21373}.WriteLine(Local.decraft_draft_tt1, Color.Wheat);
					{21373}.WriteLine(Local.decraft_draft_tt2 + ":", Color.Wheat);
					return;
				}
				if ({21342}.OutputIsGold)
				{
					{21373}.WriteLine(Local.gold_from_ingot + ":", Color.Wheat);
					return;
				}
				{20431}.StorageAssetTb({21342}.Output, {21373}, true, false);
			};
			CraftingRecipe {17194} = craftingRecipe;
			RTIf {17195} = craftHours;
			int value = {21342}.OutputItemCount.Value;
			bool {17197} = false;
			Action<ValueTuple<int, int>> {17198} = delegate([TupleElementNames(new string[]
			{
				"resCount",
				"btIndex"
			})] ValueTuple<int, int> {21374})
			{
				if ({21342}.OutputIsGold)
				{
					Session.Account.Gold += {21374}.Item1;
				}
				else
				{
					Session.Account.AddOrRemoveItemsCountInHold({21342}.Output, {21374}.Item1);
				}
				if (!{21342}.ExtraOutputRes.IsEmpty)
				{
					Session.Account.NearPortStorage.Add({21342}.ExtraOutputRes, {21374}.Item1 / {21342}.OutputItemCount.Value);
				}
			};
			bool {17199} = false;
			string {17200} = null;
			object {17201};
			if (!{21343})
			{
				{17201} = null;
			}
			else
			{
				({17201} = new string[1])[0] = Local.not_having_factory;
			}
			currentInstance.SetData({17190}, {17191}, {17192}, {17193}, {17194}, {17195}, value, {17197}, {17198}, {17199}, {17200}, {17201}, 1, true, {21343} ? 0 : int.MaxValue, false, -1f);
			if (craftingRecipe == null)
			{
				{17177}.CurrentInstance.AddDownText(Local.PortWorkshopPage_17);
			}
			if (flag)
			{
				IStorageAsset asset = {21342}.Output;
				if (asset != null)
				{
					Image image = new Image(new Marker(0f, 0f, 48f, 48f), asset.getIconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					image.ToolTipState = asset.ToolTip();
					{17177}.CurrentInstance.AddChildPos(image, PositionAlignment.Center, PositionAlignment.LeftUp, 50f);
					{17177}.CurrentInstance.AddChildPos(new LiveLabel(default(Vector2), Fonts.F_m14_ThinBold, new Color(3, 98, 3), (LiveLabel {21375}) => Session.Account.GetItemsCountInStorage(asset).ToString() + " " + Local.in_storage_2.ToLower(), 100), PositionAlignment.Center, PositionAlignment.LeftUp, 100f);
				}
			}
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0008EDA4 File Offset: 0x0008CFA4
		private static void PageCraft(StackForm {21346})
		{
			{21346}.SortMode = UiOrientation.ExpansiveSize;
			Vector2 vector = default(Vector2);
			float num = 0f;
			LinkedDictionrary<string, WosbCrafting.Recepie> linkedDictionrary = new LinkedDictionrary<string, WosbCrafting.Recepie>();
			for (int i = 0; i < WosbCrafting.Workshop.Length; i++)
			{
				WosbCrafting.Recepie recepie = WosbCrafting.Workshop[i];
				if (recepie.OutputIsGold || recepie.Output.getName != "removed")
				{
					linkedDictionrary.Add(recepie.uiCat, recepie);
				}
			}
			PlayerAccount acc = Session.Account;
			UiControl[] array = new UiControl[1];
			int num2 = 0;
			Vector2 vector2 = new Vector2(5f, 5f);
			array[num2] = new Image(new Marker(ref vector2, 36f, 36f), CommonAtlas.Texture.Tex, CommonAtlas.craftTimeIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{21346}.AddItem(array);
			LiveLabel liveLabel = new LiveLabel(new Vector2(45f, 15f), Fonts.Philosopher_14, Color.White * 0.7f, delegate(LiveLabel {21376})
			{
				float num3 = acc.AvailCraftTime.Value / (float)acc.CraftTimeLimit;
				{21376}.BasicColor = ((num3 > 0.6f) ? Color.White : ((num3 > 0.4f) ? Color.Lerp(Color.LightGray, Color.Yellow, 0.4f) : ((num3 > 0.2f) ? Color.Lerp(Color.White, Color.Orange, 0.5f) : Color.Lerp(Color.White, Color.OrangeRed, 0.5f))));
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(8, 4);
				defaultInterpolatedStringHandler4.AppendFormatted(Local.craft_time);
				defaultInterpolatedStringHandler4.AppendLiteral(": ");
				defaultInterpolatedStringHandler4.AppendFormatted<int>((int)acc.AvailCraftTime.Value);
				defaultInterpolatedStringHandler4.AppendLiteral(" / ");
				defaultInterpolatedStringHandler4.AppendFormatted<int>(acc.CraftTimeLimit);
				defaultInterpolatedStringHandler4.AppendLiteral(" (");
				defaultInterpolatedStringHandler4.AppendFormatted(Local.PortWorkshopPage_4(acc.CraftTimeUpdate));
				defaultInterpolatedStringHandler4.AppendLiteral(")");
				return defaultInterpolatedStringHandler4.ToStringAndClear();
			}, 200);
			{21346}.AddItem(new UiControl[]
			{
				liveLabel
			});
			liveLabel.ToolTipState = new ToolTipState(Local.PortWorkshopPage_6_a, Local.PortWorkshopPage_6_b.Replace("$", Environment.NewLine), Array.Empty<ToolTipCharacteristics>());
			if (!PlatformTuning.DisableShop)
			{
				Button button = new Button(new Vector2(500f, 4f), new Rectangle(40, 61, 34, 35), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Label {13105} = new Label(new Vector2(button.Pos.End.X, button.Pos.Center.Y - 7f), Fonts.Philosopher_14, new Color(193, 171, 125) * 0.6f, Local.PortRealShopPage_131, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button.AddChild({13105});
				button.ToolTipState = new ToolTipState(Local.PortRealShopPage_131, Local.learn_more, Array.Empty<ToolTipCharacteristics>());
				button.EvClick += delegate(ClickUiEventArgs {21365})
				{
					{21338} currentInstance = {21338}.CurrentInstance;
					if (currentInstance != null)
					{
						currentInstance.RemoveFromContainer();
					}
					Global.Game.ScenePort.realShopHandler(null, null);
					{20881}.RedirectToSubscriptionsPage(2);
				};
				button.IsVisible = false;
				button.UpdateComplete += delegate(UiControl {21366})
				{
					{21366}.IsVisible = DonationSystem.ShowBuyCraftTimeToolTip;
				};
				{21346}.AddItem(new UiControl[]
				{
					button
				});
			}
			vector.Y += 0.77f;
			bool flag = true;
			foreach (KeyValuePair<string, ObservableTlist<WosbCrafting.Recepie>> keyValuePair in linkedDictionrary)
			{
				if (vector.X != 0f)
				{
					vector.Y += 1f;
				}
				vector.X = 0f;
				Vector2 {20858} = new Vector2(0f, vector.Y * (float){20856}.Pattern_Item3Rows.Height + num);
				string {20859} = keyValuePair.Key + ((Debugging.DebugInfo && vector.Y < 1f) ? " - sv_Debug" : "");
				string {20860};
				if (!flag)
				{
					{20860} = string.Empty;
				}
				else
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
					defaultInterpolatedStringHandler.AppendFormatted<int>(Session.Account.ResourcesInPorts.WriteAccess(Global.Player.NearPort.PortID).OpenedFactory.Size);
					defaultInterpolatedStringHandler.AppendLiteral(" / 2");
					{20860} = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				Form form = {20856}.CreateHeadForm({20858}, {20859}, {20860});
				{21346}.AddItem(new UiControl[]
				{
					form
				});
				num += 48f;
				using (IEnumerator<WosbCrafting.Recepie> enumerator2 = ((IEnumerable<WosbCrafting.Recepie>)keyValuePair.Value).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						{21338}.<>c__DisplayClass14_1 CS$<>8__locals2 = new {21338}.<>c__DisplayClass14_1();
						CS$<>8__locals2.info = enumerator2.Current;
						{21338}.<>c__DisplayClass14_2 CS$<>8__locals3 = new {21338}.<>c__DisplayClass14_2();
						CS$<>8__locals3.CS$<>8__locals1 = CS$<>8__locals2;
						ResourceInfo resourceInfo = CS$<>8__locals3.CS$<>8__locals1.info.Output as ResourceInfo;
						if ((resourceInfo == null || resourceInfo.ID != 67) && !CS$<>8__locals3.CS$<>8__locals1.info.OutputIsGold)
						{
							CS$<>8__locals3.dontHavePortFactory = (CS$<>8__locals3.CS$<>8__locals1.info.NeedsFactoryInPort != null && !Session.Account.ResourcesInPorts.IsFactoryOpened(Global.Player.NearPort.PortID, CS$<>8__locals3.CS$<>8__locals1.info.NeedsFactoryInPort.Value) && !Debugging.DebugInfo);
							CS$<>8__locals3.extraOutput = "";
							if (!CS$<>8__locals3.CS$<>8__locals1.info.ExtraOutputRes.IsEmpty)
							{
								{21338}.<>c__DisplayClass14_2 CS$<>8__locals4 = CS$<>8__locals3;
								string extraOutput = CS$<>8__locals3.extraOutput;
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(2, 1);
								defaultInterpolatedStringHandler2.AppendLiteral(" x");
								defaultInterpolatedStringHandler2.AppendFormatted<int>(CS$<>8__locals3.CS$<>8__locals1.info.OutputRes.GetTotalItemsCount());
								CS$<>8__locals4.extraOutput = extraOutput + defaultInterpolatedStringHandler2.ToStringAndClear();
								foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)CS$<>8__locals3.CS$<>8__locals1.info.ExtraOutputRes.ResourceInfo))
								{
									{21338}.<>c__DisplayClass14_2 CS$<>8__locals5 = CS$<>8__locals3;
									string extraOutput2 = CS$<>8__locals3.extraOutput;
									DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(5, 2);
									defaultInterpolatedStringHandler3.AppendLiteral(" + ");
									defaultInterpolatedStringHandler3.AppendFormatted(gsilocalEnumerablePair.Info.Name);
									defaultInterpolatedStringHandler3.AppendLiteral(" x");
									defaultInterpolatedStringHandler3.AppendFormatted<int>(gsilocalEnumerablePair.Count);
									CS$<>8__locals5.extraOutput = extraOutput2 + defaultInterpolatedStringHandler3.ToStringAndClear();
								}
							}
							{20856}.GenericColor {20868} = (CS$<>8__locals3.CS$<>8__locals1.info.NeedsFactoryInPort != null && !CS$<>8__locals3.dontHavePortFactory) ? {20856}.GenericColor.LightLime : {20856}.GenericColor.No;
							Form form2 = {20856}.GenericItemForm(CS$<>8__locals3.CS$<>8__locals1.info.Output.getName + CS$<>8__locals3.extraOutput, delegate
							{
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(0, 2);
								defaultInterpolatedStringHandler4.AppendFormatted<int>(Session.Account.GetItemsCountInStorage(CS$<>8__locals3.CS$<>8__locals1.info.Output));
								defaultInterpolatedStringHandler4.AppendFormatted(Local.pcs);
								return defaultInterpolatedStringHandler4.ToStringAndClear();
							}, CS$<>8__locals3.CS$<>8__locals1.info.Output.getIconTexture, {20868}, null, null);
							if (CS$<>8__locals3.CS$<>8__locals1.info.NeedsFactoryInPort != null && CS$<>8__locals3.CS$<>8__locals1.info.uiCat == Local.craft_comp)
							{
								{21338}.AddOpenOrCloseFactoryApi(form2, CS$<>8__locals3.CS$<>8__locals1.info.NeedsFactoryInPort.Value);
							}
							if (!string.IsNullOrEmpty(CS$<>8__locals3.CS$<>8__locals1.info.Annotation))
							{
								form2.AddChild(new Label(192f, 40f, Fonts.Philosopher_14, Color.Wheat * 0.35f, CS$<>8__locals3.CS$<>8__locals1.info.Annotation, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
							}
							form2.Pos = form2.Pos.SetXY(vector.X * (float){20856}.Pattern_Item3Rows.Width, vector.Y * (float){20856}.Pattern_Item3Rows.Height + num);
							{21346}.AddItem(new UiControl[]
							{
								form2
							});
							form2.EvClickEmptiness += delegate(ClickUiEventArgs {21377})
							{
								{21338}.OpenWorkshopItemCraft(CS$<>8__locals3.CS$<>8__locals1.info, CS$<>8__locals3.dontHavePortFactory, CS$<>8__locals3.extraOutput, true);
							};
							if ((CS$<>8__locals3.CS$<>8__locals1.info.HideInLowRank && EducationHelper.MakeInvisibleSomeCraftsAndWeapons) || (CS$<>8__locals3.dontHavePortFactory && CS$<>8__locals3.CS$<>8__locals1.info.NeedsFactoryInPort == null))
							{
								form2.Opacity = 0.5f;
							}
							IStorageAsset output = CS$<>8__locals3.CS$<>8__locals1.info.Output;
							if (output != null)
							{
								form2.ToolTipState = output.ToolTip();
							}
							vector.X += 1f;
							if (vector.X == 3f)
							{
								vector.X = 0f;
								vector.Y += 1f;
							}
						}
					}
				}
				if (flag)
				{
					foreach (ValueTuple<FactoryType, ImageDecription, string> valueTuple in new ValueTuple<FactoryType, ImageDecription, string>[]
					{
						new ValueTuple<FactoryType, ImageDecription, string>(FactoryType.PortTopWeapons, new ImageDecription(CommonAtlas.Texture.Tex, new Rectangle(1042, 217, 96, 96)), Local.tt_PortTopWeapons)
					})
					{
						Form form3 = {20856}.GenericItemForm(valueTuple.Item1.ToStringLocal(), () => string.Empty, valueTuple.Item2, Session.Account.ResourcesInPorts.IsFactoryOpened(Global.Player.NearPort.PortID, valueTuple.Item1) ? {20856}.GenericColor.LightLime : {20856}.GenericColor.No, null, null);
						{21338}.AddOpenOrCloseFactoryApi(form3, valueTuple.Item1);
						form3.Pos = form3.Pos.SetXY(vector.X * (float){20856}.Pattern_Item3Rows.Width, vector.Y * (float){20856}.Pattern_Item3Rows.Height + num);
						{21346}.AddItem(new UiControl[]
						{
							form3
						});
						form3.ToolTipState = new ToolTipState(valueTuple.Item1.ToStringLocal(), valueTuple.Item3, Array.Empty<ToolTipCharacteristics>());
						vector.X += 1f;
						if (vector.X == 3f)
						{
							vector.X = 0f;
							vector.Y += 1f;
						}
					}
				}
				flag = false;
			}
			if (vector.X != 0f)
			{
				vector.Y += 1f;
			}
			vector.X = 0f;
			if (EducationHelper.ShowPowerupItemsPage)
			{
				Form form4 = {20856}.CreateHeadForm(new Vector2(0f, vector.Y * (float){20856}.Pattern_Item3Rows.Height + num), Local.places_powerupitems + " (" + Local.PortWorkshopPage_tgtt + ")", null);
				{21346}.AddItem(new UiControl[]
				{
					form4
				});
				num += 48f;
				LinkedDictionrary<string, PowerupItemInfo> linkedDictionrary2 = new LinkedDictionrary<string, PowerupItemInfo>();
				foreach (PowerupItemInfo powerupItemInfo in ((IEnumerable<PowerupItemInfo>)Gameplay.PowerupItems))
				{
					if (!powerupItemInfo.HideFromCraftUi && powerupItemInfo.CraftOrShopOrNull != null)
					{
						linkedDictionrary2.Add(powerupItemInfo.UiCategory, powerupItemInfo);
					}
				}
				foreach (KeyValuePair<string, ObservableTlist<PowerupItemInfo>> keyValuePair2 in linkedDictionrary2)
				{
					if (vector.X != 0f)
					{
						vector.Y += 1f;
					}
					vector.X = 0f;
					using (IEnumerator<PowerupItemInfo> enumerator4 = ((IEnumerable<PowerupItemInfo>)keyValuePair2.Value).GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							PowerupItemInfo info = enumerator4.Current;
							Form form5 = {20856}.GenericItemForm(info.Name, delegate
							{
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(0, 2);
								defaultInterpolatedStringHandler4.AppendFormatted<int>(Session.Account.PowerupItemsAtStorage[info.Index]);
								defaultInterpolatedStringHandler4.AppendFormatted(Local.pcs);
								return defaultInterpolatedStringHandler4.ToStringAndClear();
							}, info.Icon, {20856}.GenericColor.No, delegate
							{
								{22452}.OpenPowerupItemCraft(info);
							}, new Color?(Color.LightGray * 0.7f));
							form5.Pos = form5.Pos.SetXY(vector.X * (float){20856}.Pattern_Item3Rows.Width, vector.Y * (float){20856}.Pattern_Item3Rows.Height + num);
							{21346}.AddItem(new UiControl[]
							{
								form5
							});
							form5.EvClick += delegate(ClickUiEventArgs {21378})
							{
								{22452}.OpenPowerupItemCraft(info);
							};
							form5.ToolTip = new ToolTip((UiControl {21379}) => info.ToolTip(false, Array.Empty<ToolTipCharacteristics>()));
							vector.X += 1f;
							if (vector.X == 3f)
							{
								vector.X = 0f;
								vector.Y += 1f;
							}
						}
					}
				}
			}
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0008FA24 File Offset: 0x0008DC24
		private static void PageRecycle(StackForm {21347})
		{
			{21338}.<>c__DisplayClass15_0 CS$<>8__locals1 = new {21338}.<>c__DisplayClass15_0();
			CS$<>8__locals1.form = {21347};
			CS$<>8__locals1.form.SortMode = UiOrientation.ExpansiveSize;
			CS$<>8__locals1.writeIndex = default(Vector2);
			CS$<>8__locals1.yOff = 0f;
			CS$<>8__locals1.acc = Session.Account;
			CS$<>8__locals1.coal = Gameplay.ItemsInfo.FromID(12);
			StackForm form = CS$<>8__locals1.form;
			UiControl[] array = new UiControl[1];
			int num = 0;
			Vector2 vector = new Vector2(5f, 5f);
			array[num] = new Image(new Marker(ref vector, 36f, 36f), CS$<>8__locals1.coal.IconTexture, CS$<>8__locals1.coal.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddItem(array);
			CS$<>8__locals1.form.AddItem(new UiControl[]
			{
				new LiveLabel(new Vector2(45f, 13f), Fonts.Philosopher_14, Color.White * 0.7f, null, delegate(object {21380})
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
					defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.coal.Name);
					defaultInterpolatedStringHandler.AppendLiteral(": ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.acc.NearPortStorage[(int)CS$<>8__locals1.coal.ID]);
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}, 200)
			});
			{21338}.<>c__DisplayClass15_0 CS$<>8__locals2 = CS$<>8__locals1;
			CS$<>8__locals2.writeIndex.Y = CS$<>8__locals2.writeIndex.Y + 0.77f;
			CS$<>8__locals1.writeIndex.X = 0f;
			Form form2 = {20856}.CreateHeadForm(new Vector2(0f, CS$<>8__locals1.writeIndex.Y * (float){20856}.Pattern_Item3Rows.Height + CS$<>8__locals1.yOff), Local.PortWorkshopPage_7, null);
			CS$<>8__locals1.yOff += form2.Pos.WH.Y;
			CS$<>8__locals1.form.AddItem(new UiControl[]
			{
				form2
			});
			foreach (GSILocalEnumerablePair<CannonGameInfo> gsilocalEnumerablePair in from {21367} in Session.Account.CannonsAtStorage.CannonGameInfo
			orderby {21367}.Info.CostAsGold.Value
			select {21367})
			{
				CS$<>8__locals1.<PageRecycle>g__CreateItem|1(gsilocalEnumerablePair.Info, null);
			}
			foreach (CannonGameInstance cannonGameInstance in from {21368} in Session.Account.UsedMortarsAtStorage
			orderby {21368}.Info.CostAsGold.Value
			select {21368})
			{
				CS$<>8__locals1.<PageRecycle>g__CreateItem|1(cannonGameInstance.Info, cannonGameInstance);
			}
			ValueTuple<int, int>[] array2 = new ValueTuple<int, int>[]
			{
				new ValueTuple<int, int>(23, 4),
				new ValueTuple<int, int>(30, 11)
			};
			for (int i = 0; i < array2.Length; i++)
			{
				ValueTuple<int, int> valueTuple = array2[i];
				int resId = valueTuple.Item1;
				int resResult = valueTuple.Item2;
				if (Session.Account.NearPortStorage[resId] > 0)
				{
					ResourceInfo info = Gameplay.ItemsInfo.FromID(resId);
					Form form3 = {20856}.GenericItemForm(info.Name, delegate
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 2);
						defaultInterpolatedStringHandler.AppendFormatted<int>(Session.Account.NearPortStorage[resId]);
						defaultInterpolatedStringHandler.AppendFormatted(Local.pcs);
						return defaultInterpolatedStringHandler.ToStringAndClear();
					}, info.IconTexture, {20856}.GenericColor.Cyan, delegate
					{
						{21338}.OpenRecyclingWindow(info, resResult);
					}, null);
					form3.Pos = form3.Pos.SetXY(CS$<>8__locals1.writeIndex.X * (float){20856}.Pattern_Item3Rows.Width, CS$<>8__locals1.writeIndex.Y * (float){20856}.Pattern_Item3Rows.Height + CS$<>8__locals1.yOff);
					{20431}.Set(form3, info, 0, null);
					CS$<>8__locals1.form.AddItem(new UiControl[]
					{
						form3
					});
					{21338}.<>c__DisplayClass15_0 CS$<>8__locals5 = CS$<>8__locals1;
					CS$<>8__locals5.writeIndex.X = CS$<>8__locals5.writeIndex.X + 1f;
					if (CS$<>8__locals1.writeIndex.X == 3f)
					{
						CS$<>8__locals1.writeIndex.X = 0f;
						{21338}.<>c__DisplayClass15_0 CS$<>8__locals6 = CS$<>8__locals1;
						CS$<>8__locals6.writeIndex.Y = CS$<>8__locals6.writeIndex.Y + 1f;
					}
				}
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0008FE5C File Offset: 0x0008E05C
		private static void OpenRecyclingWindow(ResourceInfo {21348}, int {21349})
		{
			ResourceInfo outResInfo = Gameplay.ItemsInfo.FromID({21349});
			int value = Gameplay.ItemsInfo.FromID(12).MediumCost.Value;
			int num = ({21348}.MediumCost.Value + value) / outResInfo.MediumCost.Value;
			RTIf min = (float)num * 0.7f;
			RTIf max = (float)num * 1.3f;
			int num2 = Math.Min(Session.Account.NearPortStorage[(int){21348}.ID], Session.Account.NearPortStorage[12]);
			new {21838}((num2 == 0) ? Local.coal_not_enough2 : (Local.coal_at_storage + Session.Account.NearPortStorage[12].ToString()), Local.recycle, delegate(int {21384})
			{
				int num3 = (int)Rand.Range(min.Value * (float){21384}, max.Value * (float){21384});
				GSI nearPortStorage = Session.Account.NearPortStorage;
				int num4 = {21349};
				nearPortStorage[num4] += num3;
				nearPortStorage = Session.Account.NearPortStorage;
				nearPortStorage[12] = nearPortStorage[12] - {21384};
				nearPortStorage = Session.Account.NearPortStorage;
				num4 = (int){21348}.ID;
				nearPortStorage[num4] -= {21384};
				if ({21384} > 0)
				{
					{19988} {20000} = {19988}.GiveResources;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
					defaultInterpolatedStringHandler.AppendFormatted(Local.PortWorkshopPage_1);
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted({21348}.Name);
					defaultInterpolatedStringHandler.AppendLiteral(" -");
					defaultInterpolatedStringHandler.AppendFormatted<int>({21384});
					{19994}.MeAndLogbook({20000}, defaultInterpolatedStringHandler.ToStringAndClear(), new LBFlags?(LBFlags.L1));
				}
				if ({21384} > 0)
				{
					{19988} {20000}2 = {19988}.GiveResources;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(2, 2);
					defaultInterpolatedStringHandler2.AppendFormatted(Gameplay.ItemsInfo.FromID(12).Name);
					defaultInterpolatedStringHandler2.AppendLiteral(" -");
					defaultInterpolatedStringHandler2.AppendFormatted<int>({21384});
					{19994}.MeAndLogbook({20000}2, defaultInterpolatedStringHandler2.ToStringAndClear(), new LBFlags?(LBFlags.L1));
				}
				if (num3 > 0)
				{
					{19988} {20000}3 = {19988}.GiveResources;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(2, 2);
					defaultInterpolatedStringHandler3.AppendFormatted(outResInfo.Name);
					defaultInterpolatedStringHandler3.AppendLiteral(" +");
					defaultInterpolatedStringHandler3.AppendFormatted<int>(num3);
					{19994}.MeAndLogbook({20000}3, defaultInterpolatedStringHandler3.ToStringAndClear(), new LBFlags?(LBFlags.L1));
				}
			}, num2, (int {21385}) => Local.portWorkshop_OpenRecyclingWindowCountResultSpecific({21385}, outResInfo.Name, {21338}.CountHelper(min.Value, max.Value, {21385})), null, null, null, null);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0008FFA4 File Offset: 0x0008E1A4
		private static int GetNecessaryCoal(CannonGameInfo {21350}, CannonGameInstance {21351})
		{
			int num = {21350}.CostAsGold.Value;
			if ({21351} != null)
			{
				num = (int)((float)num * (0.1f + 0.9f * {21351}.MortarResourceFactor));
			}
			return (int)MathF.Max(1f, MathF.Round(0.1f * (float)num / (float)Gameplay.ItemsInfo.FromID(12).MediumCost.Value));
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00090010 File Offset: 0x0008E210
		private static void OpenRecyclingWindow(CannonGameInfo {21352}, CannonGameInstance {21353})
		{
			int num = {21352}.CostAsGold.Value;
			if ({21353} != null)
			{
				num = (int)((float)num * (0.1f + 0.9f * {21353}.MortarResourceFactor));
			}
			CannonCategory cannonCategory;
			if ({21352}.Class == CannonClass.Mortar)
			{
				if ({21352}.Poundage.GetValueOrDefault() != 6 && {21352}.Poundage.GetValueOrDefault() != 7)
				{
					if ({21352}.Poundage.GetValueOrDefault() != 8 && {21352}.Poundage.GetValueOrDefault() != 9 && {21352}.Feature == CannonFeature.Default)
					{
						if ({21352}.Poundage.GetValueOrDefault() != 10 && {21352}.Poundage.GetValueOrDefault() != 11)
						{
							throw new InvalidOperationException();
						}
						cannonCategory = CannonCategory.Heavy;
					}
					else
					{
						cannonCategory = CannonCategory.Medium;
					}
				}
				else
				{
					cannonCategory = CannonCategory.Light;
				}
			}
			else
			{
				cannonCategory = {21352}.Category;
			}
			CannonCategory cannonCategory2 = cannonCategory;
			float {21359} = 0f;
			float {21359}2 = 0f;
			float {21359}3 = 0f;
			switch (cannonCategory2)
			{
			case CannonCategory.Light:
				{21359} = 0.2f * (float)num;
				break;
			case CannonCategory.Medium:
				{21359} = 0.1f * (float)num;
				{21359}2 = 0.25f * (float)num;
				break;
			case CannonCategory.Heavy:
				{21359} = 0.1f * (float)num;
				{21359}3 = 0.2f * (float)num;
				break;
			}
			int coal = {21338}.GetNecessaryCoal({21352}, {21353});
			ValueTuple<float, float> valueTuple = {21338}.<OpenRecyclingWindow>g__GetRes|18_0({21359}, 4);
			float ironMin = valueTuple.Item1;
			float ironMax = valueTuple.Item2;
			valueTuple = {21338}.<OpenRecyclingWindow>g__GetRes|18_0({21359}2, 11);
			float copperMin = valueTuple.Item1;
			float copperMax = valueTuple.Item2;
			valueTuple = {21338}.<OpenRecyclingWindow>g__GetRes|18_0({21359}3, 14);
			float bronzeMin = valueTuple.Item1;
			float bronzeMax = valueTuple.Item2;
			int val = ({21353} != null) ? 1 : Session.Account.CannonsAtStorage[(int){21352}.ID];
			int num2 = Math.Min(Session.Account.NearPortStorage[12] / coal, val);
			new {21838}((num2 == 0) ? Local.coal_not_enough2 : (Local.coal_at_storage + Session.Account.NearPortStorage[12].ToString()), Local.recycle, delegate(int {21386})
			{
				int num3 = coal * {21386};
				int num4 = Rand.RangeInt((int)(ironMin * (float){21386}), (int)(ironMax * (float){21386}));
				int num5 = Rand.RangeInt((int)(copperMin * (float){21386}), (int)(copperMax * (float){21386}));
				int num6 = Rand.RangeInt((int)(bronzeMin * (float){21386}), (int)(bronzeMax * (float){21386}));
				GSI gsi = Session.Account.NearPortStorage;
				gsi[4] = gsi[4] + num4;
				gsi = Session.Account.NearPortStorage;
				gsi[11] = gsi[11] + num5;
				gsi = Session.Account.NearPortStorage;
				gsi[14] = gsi[14] + num6;
				gsi = Session.Account.NearPortStorage;
				gsi[12] = gsi[12] - num3;
				if ({21353} == null)
				{
					gsi = Session.Account.CannonsAtStorage;
					int id = (int){21352}.ID;
					gsi[id] -= {21386};
				}
				else
				{
					Session.Account.UsedMortarsAtStorage.Remove({21353});
				}
				if ({21386} > 0)
				{
					{19988} {20000} = {19988}.GiveResources;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
					defaultInterpolatedStringHandler.AppendFormatted(Local.PortWorkshopPage_1);
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted({21352}.Name);
					defaultInterpolatedStringHandler.AppendLiteral(" -");
					defaultInterpolatedStringHandler.AppendFormatted<int>({21386});
					{19994}.MeAndLogbook({20000}, defaultInterpolatedStringHandler.ToStringAndClear(), new LBFlags?(LBFlags.L1));
					if (num3 > 0)
					{
						{19988} {20000}2 = {19988}.GiveResources;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(2, 2);
						defaultInterpolatedStringHandler2.AppendFormatted(Gameplay.ItemsInfo.FromID(12).Name);
						defaultInterpolatedStringHandler2.AppendLiteral(" -");
						defaultInterpolatedStringHandler2.AppendFormatted<int>(num3);
						{19994}.MeAndLogbook({20000}2, defaultInterpolatedStringHandler2.ToStringAndClear(), new LBFlags?(LBFlags.L1));
					}
					if (num4 > 0)
					{
						{19988} {20000}3 = {19988}.GiveResources;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(2, 2);
						defaultInterpolatedStringHandler3.AppendFormatted(Gameplay.ItemsInfo.FromID(4).Name);
						defaultInterpolatedStringHandler3.AppendLiteral(" +");
						defaultInterpolatedStringHandler3.AppendFormatted<int>(num4);
						{19994}.MeAndLogbook({20000}3, defaultInterpolatedStringHandler3.ToStringAndClear(), new LBFlags?(LBFlags.L1));
					}
					if (num5 > 0)
					{
						{19988} {20000}4 = {19988}.GiveResources;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(2, 2);
						defaultInterpolatedStringHandler4.AppendFormatted(Gameplay.ItemsInfo.FromID(11).Name);
						defaultInterpolatedStringHandler4.AppendLiteral(" +");
						defaultInterpolatedStringHandler4.AppendFormatted<int>(num5);
						{19994}.MeAndLogbook({20000}4, defaultInterpolatedStringHandler4.ToStringAndClear(), new LBFlags?(LBFlags.L1));
					}
					if (num6 > 0)
					{
						{19988} {20000}5 = {19988}.GiveResources;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler5 = new DefaultInterpolatedStringHandler(2, 2);
						defaultInterpolatedStringHandler5.AppendFormatted(Gameplay.ItemsInfo.FromID(14).Name);
						defaultInterpolatedStringHandler5.AppendLiteral(" +");
						defaultInterpolatedStringHandler5.AppendFormatted<int>(num6);
						{19994}.MeAndLogbook({20000}5, defaultInterpolatedStringHandler5.ToStringAndClear(), new LBFlags?(LBFlags.L1));
					}
				}
			}, num2, (int {21387}) => {21338}.<OpenRecyclingWindow>g__textHelper|18_1(new ValueTuple<int, string>[]
			{
				new ValueTuple<int, string>(12, "-" + StringHelper.GetValueOfK(coal * {21387})),
				new ValueTuple<int, string>(4, "+" + {21338}.CountHelper(ironMin, ironMax, {21387})),
				new ValueTuple<int, string>(11, "+" + {21338}.CountHelper(copperMin, copperMax, {21387})),
				new ValueTuple<int, string>(14, "+" + {21338}.CountHelper(bronzeMin, bronzeMax, {21387}))
			}), null, null, null, null);
			{19994} currentInstance = {19994}.CurrentInstance;
			if (currentInstance == null)
			{
				return;
			}
			currentInstance.MoveToFrontLevel();
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000902C4 File Offset: 0x0008E4C4
		private static string CountHelper(float {21354}, float {21355}, int {21356})
		{
			{21354} *= (float){21356};
			{21355} *= (float){21356};
			if (Math.Floor((double){21354}) == Math.Floor((double){21355}))
			{
				return Math.Ceiling((double){21354}).ToString();
			}
			return StringHelper.GetValueOfK((int)Math.Floor((double){21354})) + "-" + StringHelper.GetValueOfK((int)Math.Floor((double){21355}));
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00090324 File Offset: 0x0008E524
		private static void AddOpenOrCloseFactoryApi(Form {21357}, FactoryType {21358})
		{
			{21338}.<>c__DisplayClass20_0 CS$<>8__locals1 = new {21338}.<>c__DisplayClass20_0();
			CS$<>8__locals1.factoryType = {21358};
			CS$<>8__locals1.uiElement = {21357};
			bool flag = Session.Account.ResourcesInPorts.IsFactoryOpened(Global.Player.NearPort.PortID, CS$<>8__locals1.factoryType);
			CS$<>8__locals1.hasQuest = (() => Session.Account.IsEducationInProgress(EducationOnboarding.OpenSpecificWorkshop, false, false) || Session.Account.IsEducationInProgress(EducationOnboarding.MakeBulkByQuest, false, false));
			if (CS$<>8__locals1.factoryType == FactoryType.PortBulks && CS$<>8__locals1.hasQuest())
			{
				CS$<>8__locals1.uiElement.UpdateComplete += delegate(UiControl {21388})
				{
					{21388}.BrightnessBlinkingMode = CS$<>8__locals1.hasQuest();
				};
			}
			if (!flag)
			{
				CS$<>8__locals1.uiElement.AddChild(new Form(new Marker(7f, 5f, 60f, 60f), AtlasPortGui.c_overlayNotAvaialable, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				});
				if (Session.Account.ResourcesInPorts.WriteAccess(Global.Player.NearPort.PortID).OpenedFactory.Size < Session.Game.MaxCountOfWorkshops)
				{
					{21338}.<>c__DisplayClass20_1 CS$<>8__locals2 = new {21338}.<>c__DisplayClass20_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					{21338}.<>c__DisplayClass20_1 CS$<>8__locals3 = CS$<>8__locals2;
					Marker marker = new Marker(131f, 37f, 180f, 26f);
					Marker marker2 = CS$<>8__locals2.CS$<>8__locals1.uiElement.Pos;
					CS$<>8__locals3.openButton = new Button(marker.Offset(marker2.XY), {21684}.c_yellowButton, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					if (CS$<>8__locals2.CS$<>8__locals1.factoryType != FactoryType.PortTopWeapons)
					{
						CS$<>8__locals2.openButton.ToolTipState = new ToolTipState("", Local.build_port_workshop_tt, Array.Empty<ToolTipCharacteristics>());
					}
					CS$<>8__locals2.openButton.SetText(Local.build2, Fonts.Arial_10, Color.White * 0.8f, false);
					CS$<>8__locals2.openButton.EvClick += delegate(ClickUiEventArgs {21389})
					{
						ValueTuple<int, int> valueTuple = WosbCrafting.FactoryInPortBuildPrice(Global.Player.NearPort, Session.Account, CS$<>8__locals2.CS$<>8__locals1.factoryType);
						RTI priceGold2 = new RTI(valueTuple.Item1);
						RTI priceCraftTime2 = new RTI(valueTuple.Item2);
						RTI priceCraftTime = priceCraftTime2;
						RTI priceGold = priceGold2;
						if (Session.Account.ResourcesInPorts.WriteAccess(Global.Player.NearPort.PortID).OpenedFactory.Size > 0 && !Session.Game.IsExtraWorkshopAvailable(Global.Player.NearPort, true, true))
						{
							new {17107}(Local.open_factory_ristriction, Local.open_factory_ristriction_tt);
							return;
						}
						string {17133} = "";
						string build_port_workshop_tt = Local.build_port_workshop_tt;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 7);
						defaultInterpolatedStringHandler.AppendFormatted(Local.cost);
						defaultInterpolatedStringHandler.AppendFormatted(Environment.NewLine);
						defaultInterpolatedStringHandler.AppendFormatted(Local.gold);
						defaultInterpolatedStringHandler.AppendLiteral(": ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(priceGold.Value);
						defaultInterpolatedStringHandler.AppendFormatted(Environment.NewLine);
						defaultInterpolatedStringHandler.AppendFormatted(Local.craft_time);
						defaultInterpolatedStringHandler.AppendLiteral(": ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(priceCraftTime.Value);
						new {17107}({17133}, build_port_workshop_tt, defaultInterpolatedStringHandler.ToStringAndClear(), delegate(int {21393})
						{
							if ({21393} == 1)
							{
								return;
							}
							if ((float)priceCraftTime.Value > Session.Account.AvailCraftTime.Value)
							{
								new {17107}(Local.gold_ct_not_enough, "");
								return;
							}
							if (priceGold.Value > Session.Account.Gold)
							{
								new {17107}(Local.gold_not_enough, "");
								return;
							}
							Session.Account.Gold -= priceGold.Value;
							Session.UsedCraftTimeSync += (float)priceCraftTime.Value;
							PlayerAccount account = Session.Account;
							account.AvailCraftTime.Value = account.AvailCraftTime.Value - (float)priceCraftTime.Value;
							Session.Account.ResourcesInPorts.WriteAccess(Global.Player.NearPort.PortID).OpenedFactory.Add(CS$<>8__locals2.CS$<>8__locals1.factoryType);
							{21338}.CurrentInstance.RefreshCurrentDynamicTabPage();
							{17177} currentInstance = {17177}.CurrentInstance;
							if (currentInstance != null)
							{
								currentInstance.BlockAndClose();
							}
							Global.Game.ScenePort.MakeAccSync();
							EducationHelper.MakeFlag(EducationOnboarding.OpenSpecificWorkshop, true);
						}, true, null, new string[]
						{
							Local.to_continue,
							Local.to_back
						});
					};
					CS$<>8__locals2.CS$<>8__locals1.uiElement.AddChild(CS$<>8__locals2.openButton);
					CS$<>8__locals2.CS$<>8__locals1.uiElement.UpdateComplete += delegate(UiControl {21392})
					{
						CS$<>8__locals2.openButton.Opacity = ((CS$<>8__locals2.CS$<>8__locals1.uiElement.InputMode != MouseInputMode.NoFocus) ? 1f : 0.2f);
					};
					return;
				}
			}
			else
			{
				{21338}.<>c__DisplayClass20_3 CS$<>8__locals4 = new {21338}.<>c__DisplayClass20_3();
				CS$<>8__locals4.CS$<>8__locals3 = CS$<>8__locals1;
				EducationHelper.MakeFlag(EducationOnboarding.OpenSpecificWorkshop, true);
				{21338}.<>c__DisplayClass20_3 CS$<>8__locals5 = CS$<>8__locals4;
				Marker marker2 = new Marker(276f, 31f, 34f, 34f);
				Marker marker = CS$<>8__locals4.CS$<>8__locals3.uiElement.Pos;
				CS$<>8__locals5.destroyButton = new Button(marker2.Offset(marker.XY), {21684}.c_smallButton, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				CS$<>8__locals4.destroyButton.SetText("X", Fonts.Arial_10Bold, Color.White * 0.8f, false);
				CS$<>8__locals4.destroyButton.EvClick += delegate(ClickUiEventArgs {21390})
				{
					{17177} currentInstance = {17177}.CurrentInstance;
					if (currentInstance != null)
					{
						currentInstance.RemoveFromContainer();
					}
					string {17133} = "";
					string portWorkshopPage_destroyFactory = Local.PortWorkshopPage_destroyFactory;
					string {17135} = "";
					Action<int> {17136};
					if (({17136} = CS$<>8__locals4.CS$<>8__locals3.<>9__7) == null)
					{
						{17136} = (CS$<>8__locals4.CS$<>8__locals3.<>9__7 = delegate(int {21391})
						{
							if ({21391} == 1)
							{
								return;
							}
							Session.Account.ResourcesInPorts.WriteAccess(Global.Player.NearPort.PortID).OpenedFactory.FastRemove(CS$<>8__locals4.CS$<>8__locals3.factoryType);
							{21338}.CurrentInstance.RefreshCurrentDynamicTabPage();
							{17177} currentInstance2 = {17177}.CurrentInstance;
							if (currentInstance2 == null)
							{
								return;
							}
							currentInstance2.BlockAndClose();
						});
					}
					new {17107}({17133}, portWorkshopPage_destroyFactory, {17135}, {17136}, true, null, new string[]
					{
						Local.yes,
						Local.to_back
					});
				};
				CS$<>8__locals4.CS$<>8__locals3.uiElement.AddChild(CS$<>8__locals4.destroyButton);
				CS$<>8__locals4.CS$<>8__locals3.uiElement.UpdateComplete += delegate(UiControl {21394})
				{
					CS$<>8__locals4.destroyButton.IsVisible = (CS$<>8__locals4.CS$<>8__locals3.uiElement.InputMode > MouseInputMode.NoFocus);
				};
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00090634 File Offset: 0x0008E834
		[CompilerGenerated]
		[return: TupleElementNames(new string[]
		{
			"min",
			"max"
		})]
		internal static ValueTuple<float, float> <OpenRecyclingWindow>g__GetRes|18_0(float {21359}, int {21360})
		{
			if ({21359} == 0f)
			{
				return new ValueTuple<float, float>(0f, 0f);
			}
			{21359} /= (float)Gameplay.ItemsInfo.FromID({21360}).MediumCost.Value;
			float num = MathF.Round({21359} * 0.85f);
			float y = MathF.Round({21359} * 1.15f);
			return new ValueTuple<float, float>(MathF.Max(1f, num), MathF.Max(num + 2f, y));
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000906B0 File Offset: 0x0008E8B0
		[CompilerGenerated]
		internal static string <OpenRecyclingWindow>g__textHelper|18_1([TupleElementNames(new string[]
		{
			"id",
			"count"
		})] params ValueTuple<int, string>[] {21361})
		{
			return string.Join(", ", from {21369} in {21361}
			where {21369}.Item2 != "+0" && {21369}.Item2 != "0-0" && {21369}.Item2 != "0" && {21369}.Item2 != "-0"
			select {21369} into {21370}
			select Gameplay.ItemsInfo[{21370}.Item1].Name + " " + {21370}.Item2);
		}

		// Token: 0x04000F91 RID: 3985
		public static readonly Color TextColor = new Color(212, 186, 151);

		// Token: 0x02000309 RID: 777
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000F93 RID: 3987
			public static Action<StackForm> <0>__PageCraft;

			// Token: 0x04000F94 RID: 3988
			public static Action<StackForm> <1>__PageRecycle;

			// Token: 0x04000F95 RID: 3989
			public static Action<StackForm> <2>__Create;
		}
	}
}
