using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
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
	// Token: 0x020003B6 RID: 950
	internal sealed class {22452} : {17625}
	{
		// Token: 0x060014C0 RID: 5312 RVA: 0x000AECAC File Offset: 0x000ACEAC
		public {22452}(int {22456}, PowerupItemInfo {22457}, Action<PowerupItemInfo> {22458} = null)
		{
			{22452}.<>c__DisplayClass2_0 CS$<>8__locals1 = new {22452}.<>c__DisplayClass2_0();
			CS$<>8__locals1.slotIndex = {22456};
			CS$<>8__locals1.pick = {22458};
			base..ctor(640f, {17625}.c_back3, {17604}.InGameWindowWithoutDeco, {17625}.c_icStreeringWheel, new {17625}.DynamicTittle[]
			{
				Local.PowerupItemsInfoWindow_0(CS$<>8__locals1.slotIndex + 1)
			});
			CS$<>8__locals1.<>4__this = this;
			{22452}.CurrentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{22452}.CurrentInstance = null;
			};
			CS$<>8__locals1.sortedItems = new LinkedDictionrary<string, PowerupItemInfo>();
			foreach (PowerupItemInfo powerupItemInfo in ((IEnumerable<PowerupItemInfo>)Gameplay.PowerupItems))
			{
				if (!powerupItemInfo.HideFromCraftUi)
				{
					CS$<>8__locals1.sortedItems.Add(powerupItemInfo.UiCategory, powerupItemInfo);
				}
			}
			base.ComposeTabWithScroll(null, true, false, new Action<ListItemViewControl>[]
			{
				delegate(ListItemViewControl {22460})
				{
					foreach (KeyValuePair<string, ObservableTlist<PowerupItemInfo>> keyValuePair in CS$<>8__locals1.sortedItems)
					{
						{22460}.AddItem(new UiControl[]
						{
							new Label(Vector2.Zero, Fonts.Philosopher_14, Color.SkyBlue * 0.6f, keyValuePair.Key, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
						BlocksStackFormControl blocksStackFormControl = new BlocksStackFormControl(Vector2.Zero, 2, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						using (IEnumerator<PowerupItemInfo> enumerator3 = ((IEnumerable<PowerupItemInfo>)keyValuePair.Value).GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								{22452}.<>c__DisplayClass2_1 CS$<>8__locals2 = new {22452}.<>c__DisplayClass2_1();
								CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
								CS$<>8__locals2.powerupItem = enumerator3.Current;
								if (!PowerupItemsGenerator.SkillsIds.Contains(CS$<>8__locals2.powerupItem.Index))
								{
									Form form = new Form(Vector2.Zero, {22452}.Pattern_Item_Big, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
									form.ToolTip = new ToolTip((UiControl {22461}) => CS$<>8__locals2.powerupItem.ToolTip(false, new ToolTipCharacteristics[]
									{
										(CS$<>8__locals2.powerupItem.DisallowInLowRangShips != null && Global.Player.UsedShipPlayer.CraftFrom.Rank > CS$<>8__locals2.powerupItem.DisallowInLowRangShips.Value) ? new ToolTipCharacteristics("", CharacteristicsColor.WheatBold) : ((CS$<>8__locals2.powerupItem.CraftOrShopOrNull != null && Session.Account.PowerupItemsAtStorage[CS$<>8__locals2.powerupItem.Index] == 0) ? new ToolTipCharacteristics(Local.click_to_buy, CharacteristicsColor.WheatBold) : ((Session.ActivePowerupItemSlots[CS$<>8__locals2.CS$<>8__locals1.slotIndex] == null || Session.ActivePowerupItemSlots[CS$<>8__locals2.CS$<>8__locals1.slotIndex] != CS$<>8__locals2.powerupItem) ? new ToolTipCharacteristics(Local.click_for_install, CharacteristicsColor.LimeBold) : new ToolTipCharacteristics(Local.installed, CharacteristicsColor.LimeBold)))
									}));
									if (Session.Account.IsEducationInProgress(EducationOnboarding.GivePowerupItems, false, false) && Session.Account.PowerupItemsAtStorage[CS$<>8__locals2.powerupItem.Index] > 0)
									{
										form.Brightness -= 0.1f;
										form.BrightnessBlinkingMode = true;
									}
									Form selectedHighlight = new Form(new Vector2(56f, 20f), new Rectangle(988, 487, 149, 24), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
									{
										AnimatedFocus = false,
										IsVisible = false
									};
									form.AddChild(selectedHighlight);
									form.AddChild(new Label(new Vector2(57f, 6f), Fonts.Arial_10, Color.Wheat * 0.8f, CS$<>8__locals2.powerupItem.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
									{
										RenderToDepthMap = false
									});
									form.AddChild(new LiveLabel(new Vector2(57f, 23f), Fonts.Arial_10, Color.Wheat * 0.5f, delegate()
									{
										int count = Session.Account.PowerupItemsAtStorage.GetCount(CS$<>8__locals2.powerupItem.Index);
										if (count == 0)
										{
											return "";
										}
										if (count >= Session.Account.PowerupItemsLimitation)
										{
											return count.ToString() + " / " + Session.Account.PowerupItemsLimitation.ToString() + Local.pcs;
										}
										return count.ToString() + Local.pcs;
									}, 100)
									{
										RenderToDepthMap = false
									});
									form.AddChild(new Image(new Marker(2f, 2f, 50f, 50f), CS$<>8__locals2.powerupItem.Icon, CS$<>8__locals2.powerupItem.Icon.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
									{
										RenderToDepthMap = false
									});
									if (CS$<>8__locals2.powerupItem.CraftOrShopOrNull != null)
									{
										Button craftBt = new Button(new Vector2(265f, 6f), new Rectangle(672, 1301, 42, 42), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
										form.UpdateComplete += delegate(UiControl {22464})
										{
											selectedHighlight.IsVisible = (Session.ActivePowerupItemSlots[CS$<>8__locals2.CS$<>8__locals1.slotIndex] == CS$<>8__locals2.powerupItem);
											craftBt.Opacity = ((CS$<>8__locals2.powerupItem.DisallowInLowRangShips != null && Global.Player.UsedShipPlayer.CraftFrom.Rank > CS$<>8__locals2.powerupItem.DisallowInLowRangShips.Value) ? 0f : ((CS$<>8__locals2.powerupItem.CraftOrShopOrNull.IsAvailableFull(Session.Account) && Session.Account.PowerupItemsAtStorage[CS$<>8__locals2.powerupItem.Index] < Session.Account.PowerupItemsLimitation) ? 1f : 0.3f));
											{22464}.FirstOpacity = ((Session.Account.PowerupItemsAtStorage[CS$<>8__locals2.powerupItem.Index] > 0) ? 1f : 0.45f);
										};
										craftBt.EvClick += delegate(ClickUiEventArgs {22462})
										{
											{22452}.OpenPowerupItemCraft(CS$<>8__locals2.powerupItem);
										};
										craftBt.ExToolTip(new ToolTip(new ToolTipState(Local.shop, "", Array.Empty<ToolTipCharacteristics>())));
										form.AddChild(craftBt);
									}
									if (CS$<>8__locals2.powerupItem.DisallowInLowRangShips == null || Global.Player.UsedShipPlayer.CraftFrom.Rank <= CS$<>8__locals2.powerupItem.DisallowInLowRangShips.Value)
									{
										form.EvClickEmptiness += delegate(ClickUiEventArgs {22463})
										{
											if (Session.Account.PowerupItemsAtStorage[CS$<>8__locals2.powerupItem.Index] > 0)
											{
												CS$<>8__locals2.CS$<>8__locals1.pick(CS$<>8__locals2.powerupItem);
												CS$<>8__locals2.CS$<>8__locals1.<>4__this.BlockAndClose();
												return;
											}
											if (CS$<>8__locals2.powerupItem.CraftOrShopOrNull != null)
											{
												{22452}.OpenPowerupItemCraft(CS$<>8__locals2.powerupItem);
											}
										};
									}
									blocksStackFormControl.AddItem(new UiControl[]
									{
										form
									});
								}
							}
						}
						{22460}.AddItem(new UiControl[]
						{
							blocksStackFormControl
						});
					}
				}
			});
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x000AEDB8 File Offset: 0x000ACFB8
		public static void OpenPowerupItemCraft(PowerupItemInfo {22459})
		{
			int {17204} = Math.Max(0, Session.Account.PowerupItemsLimitation - Session.Account.PowerupItemsAtStorage.GetCount({22459}.Index));
			if ({17177}.CurrentInstance == null)
			{
				new {17177}(false);
			}
			CraftingRecipe {17194} = new CraftingRecipe({22459}.CraftOrShopOrNull.InputItems, {22459}.CraftOrShopOrNull.InputMoney)
			{
				ReduceCraftCost = Session.Game.PowerupItemCraftDiscount
			};
			{17177}.CurrentInstance.SetData({22459}.Name, false, "", delegate(TextBlockBuilder {22465})
			{
				{22459}.ToolTip({22465}, false, Array.Empty<ToolTipCharacteristics>());
			}, {17194}, 0f, 1, false, delegate([TupleElementNames(new string[]
			{
				"resCount",
				"btIndex"
			})] ValueTuple<int, int> {22466})
			{
				Session.Account.PowerupItemsAtStorage.AddOrRemove({22459}.Index, {22466}.Item1);
				EducationHelper.MakeFlag(EducationOnboarding.GivePowerupItems, false);
			}, false, null, null, 1, true, {17204}, true, -1f);
		}

		// Token: 0x040012BD RID: 4797
		public static {22452} CurrentInstance;

		// Token: 0x040012BE RID: 4798
		public static readonly Rectangle Pattern_Item_Big = new Rectangle(988, 432, 306, 54);
	}
}
