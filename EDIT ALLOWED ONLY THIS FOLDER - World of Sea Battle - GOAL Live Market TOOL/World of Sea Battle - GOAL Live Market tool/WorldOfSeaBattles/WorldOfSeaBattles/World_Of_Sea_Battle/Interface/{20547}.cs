using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
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
	// Token: 0x02000294 RID: 660
	internal sealed class {20547} : CustomUi
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0007ABF7 File Offset: 0x00078DF7
		private int countDesignElementsTt
		{
			get
			{
				return Session.Account.EnvDecorElementsAtStorage.GetTotalItemsCount() + this.{20584}.InstalledDecor.Size;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0007AC19 File Offset: 0x00078E19
		public bool DecorMode
		{
			get
			{
				return {20501}.CurrentInstance != null;
			}
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0007AC24 File Offset: 0x00078E24
		public {20547}(PersonalIsleStatus {20549}) : base(new Marker((float)(Engine.GS.UIArea.Width / 2 - 400), 0f, 800f, 85f), {20547}.c_path, PositionAlignment.Center, PositionAlignment.LeftUp, Color.White, false)
		{
			{19717} currentInstance = {19717}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.MoveToFrontLevel();
			}
			this.AnimatedFocus = false;
			this.{20584} = {20549};
			{20547}.CurrentInstance = this;
			this.{20550}();
			base.EvRemoveFromContainer += delegate()
			{
				{20547}.CurrentInstance = null;
			};
			if (string.IsNullOrEmpty(this.{20584}.Name))
			{
				new {17312}(new Action<string>(this.{20561}), 30, Local.PortShipInfoGui_0b, null, null);
			}
			else
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_FirstBuildingOnPersonalIsle, true);
			}
			List<{17473}.Item> buttons = new List<{17473}.Item>
			{
				new {17473}.Item(0, Local.personal_isle_name, true, default(ImageDecription), null, null),
				new {17473}.Item(1, Local.personal_isle_leave, true, default(ImageDecription), null, null),
				new {17473}.Item(2, Local.personal_isle_useBondman, true, default(ImageDecription), null, null),
				new {17473}.Item(3, Local.personal_isle_useCrew, true, default(ImageDecription), null, null)
			};
			if (!PlatformTuning.DisableShop)
			{
				buttons.Add(new {17473}.Item(4, Local.personal_isle_forMonets, true, default(ImageDecription), null, null));
			}
			Button button = new Button(Vector2.Zero, {20547}.c_button2, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {20598})
			{
				new {17473}(new Action<object>(this.{20563}), buttons.ToArray());
			});
			button.AddChildPos(new Label(Vector2.Zero, Fonts.Arial_12, new Color(255, 200, 150), Local.manage, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 7f);
			base.AddChildPos(button, PositionAlignment.Center, PositionAlignment.LeftUp, 0f);
			button.Pos = button.Pos.Offset(-160f, 0f);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0007AE5C File Offset: 0x0007905C
		private void {20550}()
		{
			StackForm stackForm = this.{20585};
			if (stackForm != null)
			{
				stackForm.RemoveFromContainer();
			}
			StackForm stackForm2 = new StackForm(new Vector2((float)(Engine.GS.UIArea.Center.X - 100), 5f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm2.BorderThickness = -4f;
			stackForm2.AddItem(new UiControl[]
			{
				new Label(default(Vector2), Fonts.Philosopher_24, new Color(204, 207, 186) * 1.1f, string.IsNullOrEmpty(this.{20584}.Name) ? " " : this.{20584}.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			{20547}.<>c__DisplayClass13_0 CS$<>8__locals1;
			CS$<>8__locals1.line2 = new StackForm(default(Vector2), UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{20547}.<updateTb>g__AddBlock|13_0(new Marker(0f, 0f, 26f, 26f), CommonAtlas.craftTimeIcon_transp, Math.Round((double)this.{20584}.IsleCraftTime.Value, 1).ToString() + Local.PersonalIsleStatusUi_1(((this.{20584}.CraftTimePerHour > 0f) ? "+" : "") + this.{20584}.CraftTimePerHour.ToString()), (this.{20584}.IsleCraftTime.Value < (float)this.{20584}.IsleCraftTimeLimit / 4f) ? new Color(223, 135, 112) : new Color(206, 216, 149), Local.personal_isle_craftTime, Local.personal_isle_craftTime_tt, ref CS$<>8__locals1);
			{20547}.<updateTb>g__AddBlock|13_0(new Marker(0f, 0f, 13f, 26f), new Rectangle(1358, 278, 28, 55), this.{20584}.IsleCraftTimeLimit.ToString(), Color.Wheat, Local.persinal_isle_population, Local.persinal_isle_population_tt, ref CS$<>8__locals1);
			stackForm2.AddItem(new UiControl[]
			{
				CS$<>8__locals1.line2
			});
			base.AddChild(this.{20585} = stackForm2);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0007B08C File Offset: 0x0007928C
		protected override void UserUpdate(ref FrameTime {20551})
		{
			if (this.{20586}.Sample(ref {20551}) && (this.{20585} == null || this.{20585}.InputMode == MouseInputMode.NoFocus))
			{
				this.{20550}();
			}
			if (Global.Player.NearPortType == PortEnteringType.Port)
			{
				base.RemoveFromContainer();
				return;
			}
			this.{20584} = Global.Game.ScenePort.CurrentPersonalIsle;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0007B0EC File Offset: 0x000792EC
		public static void OpenFactoryWindow()
		{
			if (!{20547}.CheckWarehouse({20547}.CurrentInstance.{20584}))
			{
				return;
			}
			PersonalIsleStatus personalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
			string currentRes = Local.PersonalIsleStatusUi_2(WosbCrafting.FactoriesInfo[personalIsle.OpenedFactory].Levels[0].DisplayOutputRes.Name);
			Action<int> <>9__2;
			Func<ValueTuple<FactoryType, FactoryMineLivelInfo>, string> <>9__3;
			new {17107}(Local.personal_isle_b6_name, currentRes, Local.personal_isle_6_inside, delegate(int {20601})
			{
				if ({20601} == 0)
				{
					string personal_isle_b6_name = Local.personal_isle_b6_name;
					string currentRes = currentRes;
					string {17135} = Local.personal_isle_6_change(personalIsle.ChangeFactoryOrWorkshopPriceCT);
					Action<int> {17136};
					if (({17136} = <>9__2) == null)
					{
						{17136} = (<>9__2 = delegate(int {20602})
						{
							PersonalIsleStatus personalIsle;
							if ({20602} >= personalIsle.AllowedFactoriesInfo.Length)
							{
								return;
							}
							if ((float)personalIsle.ChangeFactoryOrWorkshopPriceCT > personalIsle.IsleCraftTime.Value)
							{
								new {17107}(Local.personal_isle_craftTime_not_enough(Math.Floor((double)personalIsle.IsleCraftTime.Value), personalIsle.ChangeFactoryOrWorkshopPriceCT), "");
								return;
							}
							personalIsle.OpenedFactory = personalIsle.AllowedFactories[{20602}];
							personalIsle = personalIsle;
							personalIsle.IsleCraftTime.Value = personalIsle.IsleCraftTime.Value - (float)personalIsle.ChangeFactoryOrWorkshopPriceCT;
						});
					}
					bool {17137} = true;
					CraftingRecipe {17138} = null;
					IEnumerable<ValueTuple<FactoryType, FactoryMineLivelInfo>> allowedFactoriesInfo = personalIsle.AllowedFactoriesInfo;
					Func<ValueTuple<FactoryType, FactoryMineLivelInfo>, string> selector;
					if ((selector = <>9__3) == null)
					{
						selector = (<>9__3 = ((ValueTuple<FactoryType, FactoryMineLivelInfo> {20603}) => Local.MineUi_21((int)((float){20603}.Item2.ProducedResCount * (1f + personalIsle.FactoryEffectivityBonus)), {20603}.Item2.DisplayOutputRes.Name)));
					}
					new {17107}(personal_isle_b6_name, currentRes, {17135}, {17136}, {17137}, {17138}, allowedFactoriesInfo.Select(selector).Concat(new string[]
					{
						Local.close
					}).ToArray<string>());
					return;
				}
				if ({20601} == 1)
				{
					personalIsle.IsFactoryEnabled = !personalIsle.IsFactoryEnabled;
					return;
				}
			}, true, null, new string[]
			{
				Local.change_factory,
				(!personalIsle.IsFactoryEnabled) ? Local.PersonalIsleStatusUi_3 : Local.PersonalIsleStatusUi_4,
				Local.close
			}).AddChildPos(new CheckboxControl(Vector2.Zero, CommonAtlas.vd_checkBox_26px_true, CommonAtlas.vd_checkBox_26px_false, personalIsle.DisableFactoryWhenLowTime, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.PersonalIsleStatusUi_5(personalIsle.DisableFactoryWhenTimeLessThan), Fonts.Philosopher_14, Color.Black * 0.8f).ExCheckEvent(delegate(CheckboxCheckedEventArgs {20604})
			{
				personalIsle.DisableFactoryWhenLowTime = {20604}.NewValue;
			}), PositionAlignment.Center, PositionAlignment.RightDown, 250f);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0007B21C File Offset: 0x0007941C
		public static void OpenWorkshopWindow()
		{
			if (!{20547}.CheckWarehouse({20547}.CurrentInstance.{20584}))
			{
				return;
			}
			PersonalIsleStatus personalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
			new {17177}(false);
			{17177}.CurrentInstance.SetData(personalIsle.PickedWorkshopInfo.NeedsFactoryInPort.Value.ToStringLocal(), false, "", null, new CraftingRecipe(personalIsle.PickedWorkshopInfo.Craft.InputItems)
			{
				ReduceCraftCost = WosbCrafting.PersonalIsleWorkshopDiscount
			}, personalIsle.PickedWorkshopInfo.CraftHours.Value, 1, false, delegate([TupleElementNames(new string[]
			{
				"resCount",
				"btIndex"
			})] ValueTuple<int, int> {20605})
			{
				PersonalIsleStatus personalIsle = personalIsle;
				personalIsle.IsleCraftTime.Value = personalIsle.IsleCraftTime.Value - (float){20605}.Item1 * personalIsle.PickedWorkshopInfo.CraftHours.Value;
				GSI nearPortStorage = Session.Account.NearPortStorage;
				int id = (int)personalIsle.PickedWorkshopInfo.Output.ID;
				nearPortStorage[id] += {20605}.Item1;
			}, false, null, null, 1, true, int.MaxValue, false, personalIsle.IsleCraftTime.Value);
			{17177}.CurrentInstance.AddChildPos(new Button(Vector2.Zero, {17625}.c_btLight_small, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.change, Fonts.Arial_10, Color.Black * 0.85f, false).ExClick(delegate(ClickUiEventArgs {20588})
			{
				{20547}.ChangeWorkshop();
			}), PositionAlignment.Center, PositionAlignment.LeftUp, 50f);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0007B35C File Offset: 0x0007955C
		private static void ChangeWorkshop()
		{
			{17177} currentInstance = {17177}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.RemoveFromContainer();
			}
			PersonalIsleStatus personalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
			Predicate<WosbCrafting.Recepie> <>9__3;
			new {17107}(Local.personal_isle_b6_name, Local.PersonalIsleStatusUi_6(personalIsle.PickedWorkshopInfo.NeedsFactoryInPort.Value.ToStringLocal()), Local.personal_isle_3_inside(personalIsle.ChangeFactoryOrWorkshopPriceCT), delegate(int {20606})
			{
				if ({20606} >= WosbCrafting.PersonalIsleWorkshops.Length - 1)
				{
					return;
				}
				PersonalIsleStatus personalIsle;
				if ((float)personalIsle.ChangeFactoryOrWorkshopPriceCT > personalIsle.IsleCraftTime.Value)
				{
					new {17107}(Local.personal_isle_craftTime_not_enough(Math.Floor((double)personalIsle.IsleCraftTime.Value), personalIsle.ChangeFactoryOrWorkshopPriceCT), "");
					return;
				}
				WosbCrafting.Recepie[] personalIsleWorkshops = WosbCrafting.PersonalIsleWorkshops;
				Predicate<WosbCrafting.Recepie> match;
				if ((match = <>9__3) == null)
				{
					match = (<>9__3 = delegate(WosbCrafting.Recepie {20607})
					{
						FactoryType? needsFactoryInPort = {20607}.NeedsFactoryInPort;
						FactoryType? needsFactoryInPort2 = personalIsle.PickedWorkshopInfo.NeedsFactoryInPort;
						return needsFactoryInPort.GetValueOrDefault() == needsFactoryInPort2.GetValueOrDefault() & needsFactoryInPort != null == (needsFactoryInPort2 != null);
					});
				}
				int num = Array.FindIndex<WosbCrafting.Recepie>(personalIsleWorkshops, match);
				personalIsle.PickedWorkshop = WosbCrafting.PersonalIsleWorkshops[(num <= {20606}) ? ({20606} + 1) : {20606}].NeedsFactoryInPort.Value;
				personalIsle = personalIsle;
				personalIsle.IsleCraftTime.Value = personalIsle.IsleCraftTime.Value - (float)personalIsle.ChangeFactoryOrWorkshopPriceCT;
			}, true, null, (from {20589} in WosbCrafting.PersonalIsleWorkshops
			select {20589}.NeedsFactoryInPort.Value.ToStringLocal() into {20608}
			where {20608} != personalIsle.PickedWorkshopInfo.NeedsFactoryInPort.Value.ToStringLocal()
			select {20608}).Concat(new string[]
			{
				Local.close
			}).ToArray<string>());
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0007B434 File Offset: 0x00079634
		public static void OpenPubWindow()
		{
			PersonalIsleStatus personalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
			Action<int> <>9__1;
			new {17107}("", Local.PersonalIsleStatusUi_7, "", delegate(int {20609})
			{
				if ({20609} == 0)
				{
					bool flag = false;
					if ({20547}.questLimiter == null || {20547}.questLimiter.Elapsed.TotalMinutes > 5.0)
					{
						{20547}.questLimiter = Stopwatch.StartNew();
						bool flag2 = Session.Account.Quests.TryReopenCategory(Global.Player.NearPort, Global.Player, QuestCompany.Battle);
						bool flag3 = Session.Account.Quests.TryReopenCategory(Global.Player.NearPort, Global.Player, QuestCompany.Trading);
						bool flag4 = Session.Account.Quests.TryReopenCategory(Global.Player.NearPort, Global.Player, QuestCompany.Coastal);
						if (flag2 || flag3 || flag4)
						{
							flag = true;
							string {17133} = "";
							string personalIsleStatusUi_ = Local.PersonalIsleStatusUi_8;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
							defaultInterpolatedStringHandler.AppendFormatted(Local.PortCompanyWindow_16);
							defaultInterpolatedStringHandler.AppendLiteral(": ");
							defaultInterpolatedStringHandler.AppendFormatted<int>(Session.Account.Quests.VisibleQuestsCount);
							new {17107}({17133}, personalIsleStatusUi_, defaultInterpolatedStringHandler.ToStringAndClear(), delegate(int {20590})
							{
							}, true, null, new string[]
							{
								Local.to_back2
							});
						}
					}
					if (!flag)
					{
						new {17107}("", Local.PortCompanyWindow_19, "", delegate(int {20591})
						{
						}, true, null, new string[]
						{
							Local.to_back2
						});
					}
				}
				if ({20609} == 1)
				{
					string recruit = Local.recruit;
					string recruit_text = Local.recruit_text;
					string {17135} = "";
					Action<int> {17136};
					if (({17136} = <>9__1) == null)
					{
						{17136} = (<>9__1 = delegate(int {20610})
						{
							if ({20610} == 4)
							{
								return;
							}
							PersonalIsleStatus personalIsle;
							int num = ({20610} == 0) ? personalIsle.RecruitSpecialUnitCT : personalIsle.RecruitSpecialUnitCTTrageted;
							if ((float)num > personalIsle.IsleCraftTime.Value)
							{
								new {17107}(Local.personal_isle_craftTime_not_enough(Math.Floor((double)personalIsle.IsleCraftTime.Value), num), "");
								return;
							}
							SpecialUnitClass? {3954} = ({20610} == 1) ? new SpecialUnitClass?(SpecialUnitClass.Combats) : (({20610} == 2) ? new SpecialUnitClass?(SpecialUnitClass.Sailors) : (({20610} == 3) ? new SpecialUnitClass?(SpecialUnitClass.Pirates) : null));
							Decorator game = Session.Game;
							UnitInfo personalIsleSpecialUnit = WosbCrew.GetPersonalIsleSpecialUnit(game, {3954}, Global.Player);
							{19994}.Me({19988}.Info, Local.lbe_newunit(personalIsleSpecialUnit.Name), Array.Empty<object>());
							{19994}.Logbook(Local.lbe_newunit(personalIsleSpecialUnit.Name), LBFlags.L1);
							Session.Account.SpecialUnitsAtStorage.Add(personalIsleSpecialUnit);
							personalIsle = personalIsle;
							personalIsle.IsleCraftTime.Value = personalIsle.IsleCraftTime.Value - (float)num;
							Global.Game.ScenePort.MakeAccSync();
						});
					}
					new {17107}(recruit, recruit_text, {17135}, {17136}, true, null, new string[]
					{
						Local.PersonalIsle_recruit_bt(personalIsle.RecruitSpecialUnitCT),
						Local.PersonalIsle_recruit_bt_target1(personalIsle.RecruitSpecialUnitCTTrageted),
						Local.PersonalIsle_recruit_bt_target2(personalIsle.RecruitSpecialUnitCTTrageted),
						Local.PersonalIsle_recruit_bt_target3(personalIsle.RecruitSpecialUnitCTTrageted),
						Local.to_back
					});
				}
			}, true, null, new string[]
			{
				Local.PersonalIsleStatusUi_9,
				Local.recruit,
				Local.close
			});
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0007B4A0 File Offset: 0x000796A0
		public static void OpenDokWindow()
		{
			if (!{20547}.CheckWarehouse({20547}.CurrentInstance.{20584}))
			{
				return;
			}
			new {17107}("", Local.personal_isle_b1_tt, "", delegate(int {20592})
			{
			}, true, null, new string[]
			{
				Local.close
			});
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0007B503 File Offset: 0x00079703
		public static bool CheckWarehouse(PersonalIsleStatus {20552})
		{
			if ({20552}.HasOverload)
			{
				new {17312}(Local.PortAllInterface_4a);
				return false;
			}
			return true;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0007B51C File Offset: 0x0007971C
		public static void OpenBuildBuildingWindow(PortTipConnection {20553}, PersonalIsleStatus.InternalBuilding {20554}, string {20555}, string {20556}, Action {20557})
		{
			PersonalIsleStatus personalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
			if ({20553} == PortTipConnection.RealShop || personalIsle.InternalBuildings.Contains({20554}))
			{
				if ({20557} == null)
				{
					new {17107}({20555}, {20556}, "", delegate(int {20593})
					{
					}, true, null, new string[]
					{
						Local.to_back
					});
					return;
				}
				{20557}();
				return;
			}
			else
			{
				string text = {20556} + ". " + Local.next_build_cost;
				int num = personalIsle.BonusCraftTimeLimitForBuilding({20554});
				if (num > 0)
				{
					string str = text;
					string newLine = Environment.NewLine;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
					defaultInterpolatedStringHandler.AppendFormatted(Local.persinal_isle_population);
					defaultInterpolatedStringHandler.AppendLiteral(" +");
					defaultInterpolatedStringHandler.AppendFormatted<int>(num);
					text = str + newLine + defaultInterpolatedStringHandler.ToStringAndClear();
				}
				new {17177}(false);
				{17177}.CurrentInstance.SetData({20555}, false, text, delegate(TextBlockBuilder {20594})
				{
				}, personalIsle.NextBuildingPrice(Session.Account), 0f, 1, true, delegate([TupleElementNames(new string[]
				{
					"resCount",
					"btIndex"
				})] ValueTuple<int, int> {20611})
				{
					if ({20611}.Item1 > 0)
					{
						personalIsle.InternalBuildings.Add({20554});
						{17177} currentInstance = {17177}.CurrentInstance;
						if (currentInstance != null)
						{
							currentInstance.BlockAndClose();
						}
						{19994}.Logbook(Local.lbe_built_pib({20555}), LBFlags.L1);
					}
				}, false, null, null, 1, true, int.MaxValue, false, -1f);
				if ({20554} == PersonalIsleStatus.InternalBuilding.Factory)
				{
					DropdownControl<FactoryType> dropdownControl = new DropdownControl<FactoryType>(new Marker(0f, 0f, ref CommonAtlas.newToolList_main), CommonAtlas.newToolList_main, CommonAtlas.newToolList_item, Fonts.Arial_10Bold, (from {20595} in personalIsle.AllowedFactoriesInfo
					select new SelItem<FactoryType>({20595}.Item1, Local.MineUi_21({20595}.Item2.ProducedResCount, {20595}.Item2.DisplayOutputRes.Name))).ToArray<SelItem<FactoryType>>());
					dropdownControl.EvChangeItem += delegate(SelItem<FactoryType> {20612})
					{
						personalIsle.OpenedFactory = {20612}.Value;
					};
					{17177}.CurrentInstance.AddChildPos(dropdownControl, PositionAlignment.Center, PositionAlignment.LeftUp, 170f);
					return;
				}
				if ({20554} == PersonalIsleStatus.InternalBuilding.Workshop)
				{
					DropdownControl<FactoryType> dropdownControl2 = new DropdownControl<FactoryType>(new Marker(0f, 0f, ref CommonAtlas.newToolList_main), CommonAtlas.newToolList_main, CommonAtlas.newToolList_item, Fonts.Arial_10Bold, (from {20596} in WosbCrafting.PersonalIsleWorkshops
					select new SelItem<FactoryType>({20596}.NeedsFactoryInPort.Value, {20596}.NeedsFactoryInPort.Value.ToStringLocal())).ToArray<SelItem<FactoryType>>());
					dropdownControl2.EvChangeItem += delegate(SelItem<FactoryType> {20613})
					{
						personalIsle.PickedWorkshop = {20613}.Value;
					};
					{17177}.CurrentInstance.AddChildPos(dropdownControl2, PositionAlignment.Center, PositionAlignment.LeftUp, 170f);
				}
				return;
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0007B7AC File Offset: 0x000799AC
		public static bool CheckDecorBonusIsBlockingAndShowMessage(out string {20558})
		{
			foreach (PersonalIsleInstalledDecorItem personalIsleInstalledDecorItem in ((IEnumerable<PersonalIsleInstalledDecorItem>)Global.Game.ScenePort.CurrentPersonalIsle.InstalledDecor))
			{
				if ({20547}.CheckDecorBonusIsBlockingAndShowMessage(personalIsleInstalledDecorItem.Info, out {20558}))
				{
					return true;
				}
			}
			{20558} = string.Empty;
			return false;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0007B820 File Offset: 0x00079A20
		public static bool CheckDecorBonusIsBlockingAndShowMessage(ShipDesignInfo {20559}, out string {20560})
		{
			PlayerCaptainSkillsStorage captainSkills = Session.Account.CaptainSkills;
			Decorator game = Session.Game;
			if (!captainSkills.CanBeResetted(game, {20559}, 0, out {20560}))
			{
				new {17312}(Local.isle_decor_blocking_bonus({20559}.Name, {20560}));
				return true;
			}
			PersonalIsleStatus currentPersonalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
			if ({20559}.AccountBonus.Item1 == PDynamicAccountBonus.BCurrentIsleAddVulcanoOreFactory && currentPersonalIsle.OpenedFactory == FactoryType.IsleVulkanoOreFactory && currentPersonalIsle.InternalBuildings.Contains(PersonalIsleStatus.InternalBuilding.Factory))
			{
				new {17312}(Local.skill_cantberesetted_9);
				return true;
			}
			if ({20559}.AccountBonus.Item1 == PDynamicAccountBonus.CCurrentIsleBonusBuildingsLimit)
			{
				if ((float)currentPersonalIsle.InstalledDecor.Count((PersonalIsleInstalledDecorItem {20597}) => {20597}.Info.AccountBonus.Item2 > 0f) > (float)currentPersonalIsle.BuildingsWithBonusLimit - {20559}.AccountBonus.Item2)
				{
					new {17312}(Local.skill_cantberesetted_10((float)currentPersonalIsle.BuildingsWithBonusLimit - {20559}.AccountBonus.Item2));
					return true;
				}
			}
			{20560} = string.Empty;
			return false;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0007B978 File Offset: 0x00079B78
		[CompilerGenerated]
		private void {20561}(string {20562})
		{
			if ({20562}.Length > 0)
			{
				this.{20584}.Name = {20562};
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0007B990 File Offset: 0x00079B90
		[CompilerGenerated]
		private void {20563}(object {20564})
		{
			if ((int){20564} == 0)
			{
				new {17312}(new Action<string>(this.{20565}), 30, Local.PortShipInfoGui_0, null, null);
				return;
			}
			if ((int){20564} == 1)
			{
				string text;
				if ({20547}.CheckDecorBonusIsBlockingAndShowMessage(out text))
				{
					return;
				}
				new {17312}((!this.{20584}.StorageResources.IsEmpty) ? Local.personal_isle_leave_res_warning : Local.personal_isle_leave_accept, new Action(this.{20567}), delegate()
				{
				});
				return;
			}
			else if ((int){20564} == 2)
			{
				if (this.{20584}.IsleCraftTime.Value >= (float)this.{20584}.IsleCraftTimeLimit)
				{
					return;
				}
				if (Global.Player.ResourcesOfHold[8] == 0)
				{
					new {17312}(Local.item_not_enough(Gameplay.ItemsInfo.FromID(8).Name));
					return;
				}
				new {21838}(Local.personal_isle_useBondman, Local.to_continue, new Action<int>(this.{20570}), Math.Min(Global.Player.ResourcesOfHold[8], (int)Math.Ceiling((double)(((float)this.{20584}.IsleCraftTimeLimit - this.{20584}.IsleCraftTime.Value) / (float)this.{20584}.BonusCraftTimeForBondman))), new Func<int, string>(this.{20572}), new int?(1), null, null, null);
				return;
			}
			else
			{
				if ((int){20564} != 3)
				{
					if ((int){20564} == 4)
					{
						if (this.{20584}.IsleCraftTime.Value >= (float)this.{20584}.IsleCraftTimeLimit)
						{
							return;
						}
						new {17312}(Local.personal_isle_forMonets_ask(DonationSystem.priceFullCraftTimeOnIsle), new Action(this.{20576}), delegate()
						{
						});
					}
					return;
				}
				if (this.{20584}.IsleCraftTime.Value >= (float)this.{20584}.IsleCraftTimeLimit)
				{
					return;
				}
				if (Session.Account.UnitsAtStorage.IsEmpty)
				{
					new {17312}(Local.personal_isle_useCrew_err);
					return;
				}
				new {17473}(new Action<object>(this.{20574}), (from {20587} in Session.Account.UnitsAtStorage.UnitInfo
				select new {17473}.Item({20587}.Info, {20587}.Info.Name, true, new ImageDecription({20587}.Info.Icon), null, null)).ToArray<{17473}.Item>());
				return;
			}
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0007B978 File Offset: 0x00079B78
		[CompilerGenerated]
		private void {20565}(string {20566})
		{
			if ({20566}.Length > 0)
			{
				this.{20584}.Name = {20566};
			}
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0007BC05 File Offset: 0x00079E05
		[CompilerGenerated]
		private void {20567}()
		{
			{18945}.TryShowAcceptingMode(Local.personal_isle_leave, Local.ExitScreen_2, 10000f, new Action(this.{20568}), true);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0007BC2C File Offset: 0x00079E2C
		[CompilerGenerated]
		private void {20568}()
		{
			foreach (PlayerShipDynamicInfo playerShipDynamicInfo in Session.Account.Shipyard.List)
			{
				if (playerShipDynamicInfo != Global.Player.UsedShipPlayer)
				{
					Global.Player.ResourcesOfHold.Add(playerShipDynamicInfo.PrivateResourcesOfHold);
					playerShipDynamicInfo.PrivateResourcesOfHold.Clean();
				}
			}
			Global.Game.ScenePort.FastExitWithoutCheckShip(new Action(this.{20569}), Local.worldmap);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0007BCC8 File Offset: 0x00079EC8
		[CompilerGenerated]
		private void {20569}()
		{
			Global.Network.Send(new OnPersonalIsleRequest(OnPersonalIsleRequest.Operation.DestroyIsle, this.{20584}.PlaceIndex, 0, 0f));
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0007BCF0 File Offset: 0x00079EF0
		[CompilerGenerated]
		private void {20570}(int {20571})
		{
			GSI resourcesOfHold = Global.Player.ResourcesOfHold;
			resourcesOfHold[8] = resourcesOfHold[8] - {20571};
			this.{20584}.IsleCraftTime.Value = Math.Min((float)this.{20584}.IsleCraftTimeLimit, this.{20584}.IsleCraftTime.Value + (float)(this.{20584}.BonusCraftTimeForBondman * {20571}));
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0007BD58 File Offset: 0x00079F58
		[CompilerGenerated]
		private string {20572}(int {20573})
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
			defaultInterpolatedStringHandler.AppendFormatted(Local.personal_isle_craftTime2);
			defaultInterpolatedStringHandler.AppendLiteral(" +");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.{20584}.BonusCraftTimeForBondman * {20573});
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0007BDA4 File Offset: 0x00079FA4
		[CompilerGenerated]
		private void {20574}(object {20575})
		{
			UnitInfo unitInfo = {20575} as UnitInfo;
			new {21838}(Local.personal_isle_full(unitInfo.Name), Local.to_continue, delegate(int {20599})
			{
				GSI unitsAtStorage = Session.Account.UnitsAtStorage;
				int id = (int)unitInfo.ID;
				unitsAtStorage[id] -= {20599};
				this.{20584}.IsleCraftTime.Value = Math.Min((float)this.{20584}.IsleCraftTimeLimit, this.{20584}.IsleCraftTime.Value + this.{20584}.BonusCraftTimeForCrew(unitInfo) * (float){20599});
			}, Math.Min(Session.Account.UnitsAtStorage[(int)unitInfo.ID], (int)Math.Ceiling((double)(((float)this.{20584}.IsleCraftTimeLimit - this.{20584}.IsleCraftTime.Value) / this.{20584}.BonusCraftTimeForCrew(unitInfo)))), delegate(int {20600})
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
				defaultInterpolatedStringHandler.AppendFormatted(Local.personal_isle_craftTime2);
				defaultInterpolatedStringHandler.AppendLiteral(" +");
				defaultInterpolatedStringHandler.AppendFormatted<float>(this.{20584}.BonusCraftTimeForCrew(unitInfo) * (float){20600}, "F2");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}, null, null, null, null);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0007BE70 File Offset: 0x0007A070
		[CompilerGenerated]
		private void {20576}()
		{
			if (DonationSystem.priceFullCraftTimeOnIsle.Value > Session.Account.Monets.Value)
			{
				Global.Game.ScenePort.realShopHandler(null, null);
				{20881}.ShowBuyMonetsToolTip(DonationSystem.priceFullCraftTimeOnIsle.Value);
				return;
			}
			PlayerAccount account = Session.Account;
			account.Monets.Value = account.Monets.Value - DonationSystem.priceFullCraftTimeOnIsle.Value;
			this.{20584}.IsleCraftTime.Value = (float)this.{20584}.IsleCraftTimeLimit;
			Global.Shopstat("personal_isle_ct", DonationSystem.priceFullCraftTimeOnIsle.Value);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0007BF18 File Offset: 0x0007A118
		[CompilerGenerated]
		internal static void <updateTb>g__AddBlock|13_0(Marker {20577}, Rectangle {20578}, string {20579}, Color {20580}, string {20581}, string {20582}, ref {20547}.<>c__DisplayClass13_0 {20583})
		{
			StackForm stackForm = new StackForm(default(Vector2), UiOrientation.HorizontalBottom, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				new Image({20577}, CommonAtlas.Texture.Tex, {20578}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			stackForm.AddSpace(4f);
			stackForm.AddItem(new UiControl[]
			{
				new Label(default(Vector2), Fonts.Philosopher_14, {20580}, {20579}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			if (!string.IsNullOrEmpty({20581}) || !string.IsNullOrEmpty({20582}))
			{
				stackForm.ToolTipState = new ToolTipState({20581}, {20582}, Array.Empty<ToolTipCharacteristics>());
			}
			{20583}.line2.AddItem(new UiControl[]
			{
				stackForm
			});
			{20583}.line2.AddSpace(12f);
		}

		// Token: 0x04000D79 RID: 3449
		public static {20547} CurrentInstance;

		// Token: 0x04000D7A RID: 3450
		public static readonly Rectangle c_path = new Rectangle(1019, 1085, 483, 75);

		// Token: 0x04000D7B RID: 3451
		public static readonly Rectangle c_button2 = new Rectangle(1019, 1199, 99, 70);

		// Token: 0x04000D7C RID: 3452
		public static readonly Rectangle c_buttonWithBack = new Rectangle(1019, 1161, 125, 37);

		// Token: 0x04000D7D RID: 3453
		private PersonalIsleStatus {20584};

		// Token: 0x04000D7E RID: 3454
		private StackForm {20585};

		// Token: 0x04000D7F RID: 3455
		private Timer {20586} = new Timer(100f);

		// Token: 0x04000D80 RID: 3456
		private static Stopwatch questLimiter;
	}
}
