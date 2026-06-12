using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
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
	// Token: 0x02000396 RID: 918
	internal sealed class {22195} : {21684}
	{
		// Token: 0x060013F1 RID: 5105 RVA: 0x000A7B93 File Offset: 0x000A5D93
		public {22195}() : base(Local.upgrades, Local.PortUpgradeShipWindow_1, "")
		{
			this.HaveSearchBar = false;
			this.OnSearchEnter = new Action<string>(this.{22222});
			EducationHelper.MakeFlag(EducationOnboarding.OpenUpgradesWindow, true);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x000A7BCC File Offset: 0x000A5DCC
		private UiControl {22196}(int {22197}, bool {22198})
		{
			Form form = new Form(Vector2.Zero, {21684}.c_itemNew, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if ({22197} == 1 && (Session.Account.IsEducationInProgress(EducationOnboarding.InstallUpgrade, false, false) || (Session.Account.IsEducationInProgress(EducationOnboarding.InstallUpgradeForNonStartShip, false, false) && Global.Player.UsedShip.StaticInfo.ID != 2)))
			{
				form.BrightnessBlinkingMode = true;
			}
			bool flag = true;
			bool flag2 = true;
			int num = 4 - (Global.Player.UsedShipPlayer.MaxUpgradesCount - {22197});
			if (num == 1 || num == 2 || num == 3)
			{
				UpgradeSlotsResearch research = Session.Account.Shipyard.GetUpResearch((int)Global.Player.CraftFrom.ID);
				flag = research.IsSlotOpened(num);
				Composer composer = new Composer(300f, -3f);
				if (num == 3)
				{
					int num2 = Session.Account.Shipyard.CountOfCompletlyResearchedClasses();
					composer.AddText(Local.extra_up_with_fine, ComposerTextStyle.Gray, true);
					if (num2 < 5)
					{
						composer.AddSpace(4f);
						Composer composer2 = composer;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
						defaultInterpolatedStringHandler.AppendFormatted(Local.researched_ship_branches);
						defaultInterpolatedStringHandler.AppendLiteral(": ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
						defaultInterpolatedStringHandler.AppendLiteral(" / ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(5);
						composer2.AddText(defaultInterpolatedStringHandler.ToStringAndClear(), new ComposerTextStyle(ComposerTextStyle.Gray.Color, false, Fonts.Arial_10, null), true);
						flag2 = false;
					}
				}
				if (!flag)
				{
					form.AddChild(new Form({22195}.p_item_Icon, new Rectangle(2586, 76, 75, 75), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false,
						RenderToDepthMap = false
					});
				}
				if (!flag && flag2)
				{
					if (num != 3)
					{
						composer.AddText(Local.extra_up_slot_name, ComposerTextStyle.Wheat, true);
					}
					composer.AddSpace(4f);
					if ((int)research.MaxOpenedSlotIndex != num - 1)
					{
						form.Opacity = 0.66f;
					}
					form.BasicColor *= 0.5f;
					ValueTuple<int, GSI> req = Gameplay.OpenExtraUpgradeSlotPrice(Global.Player.CraftFrom, num);
					int num3 = Math.Min(research.GainedXp, req.Item1);
					composer.AddText("• " + Local.PortUpgradeShipWindow_24(Math.Min(num3, req.Item1), req.Item1), (num3 >= req.Item1) ? ComposerTextStyle.Lime : ComposerTextStyle.Gray, true);
					foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)req.Item2.ResourceInfo))
					{
						Composer composer3 = composer;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(7, 3);
						defaultInterpolatedStringHandler2.AppendLiteral("• ");
						defaultInterpolatedStringHandler2.AppendFormatted(gsilocalEnumerablePair.Info.Name);
						defaultInterpolatedStringHandler2.AppendLiteral(": ");
						defaultInterpolatedStringHandler2.AppendFormatted<int>(Session.Account.NearPortStorage[(int)gsilocalEnumerablePair.Info.ID]);
						defaultInterpolatedStringHandler2.AppendLiteral(" / ");
						defaultInterpolatedStringHandler2.AppendFormatted<int>(gsilocalEnumerablePair.Count);
						composer3.AddText(defaultInterpolatedStringHandler2.ToStringAndClear(), (Session.Account.NearPortStorage[(int)gsilocalEnumerablePair.Info.ID] >= gsilocalEnumerablePair.Count) ? ComposerTextStyle.Lime : ComposerTextStyle.Gray, true);
					}
					if (num == (int)(research.MaxOpenedSlotIndex + 1) && num3 >= req.Item1 && Session.Account.NearPortStorage.CanRemove(req.Item2))
					{
						Action <>9__2;
						composer.AddUi(new Button(default(Vector2), {21684}.c_yellowButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.to_open, Fonts.Arial_10, Color.Wheat, false).ExClick(delegate(ClickUiEventArgs {22239})
						{
							string extra_up_slot_ask = Local.extra_up_slot_ask;
							Action {17372};
							if (({17372} = <>9__2) == null)
							{
								{17372} = (<>9__2 = delegate()
								{
									if (Session.Account.NearPortStorage.TryRemove(req.Item2))
									{
										research.MakeUpgrade(req.Item1);
										this.CloseAllSelectionForm();
										this.UpdateBlocks(false);
									}
								});
							}
							new {17312}(extra_up_slot_ask, {17372}, delegate()
							{
							});
						}));
					}
				}
				StackForm {12952} = composer.ComposeInStack(null);
				form.AddChildPos({12952}, PositionAlignment.LeftUp, PositionAlignment.Center, 133f);
			}
			if (flag)
			{
				form.AddChild(new Form({22195}.p_item_Icon, {22198} ? new Rectangle(1551, 720, 163, 162) : new Rectangle(950, 1424, 162, 162), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					RenderToDepthMap = false
				});
				form.EvClick += delegate(ClickUiEventArgs {22238})
				{
					this.OpenSelectionForm(new {22195}.SelectionFormParameter({22198}, {22197}, ""));
				};
				form.ToolTipState = new ToolTipState(null, ({22198} ? Local.PortUpgradeShipWindow_3_sail : Local.PortUpgradeShipWindow_3) + ". " + Local.click_for_install, Array.Empty<ToolTipCharacteristics>());
			}
			if (num == 3)
			{
				form.ToolTipState = new ToolTipState("", "", new ToolTipCharacteristics[]
				{
					new ToolTipCharacteristics(Local.extra_up_with_fine_info_1, CharacteristicsColor.Gray),
					new ToolTipCharacteristics(Local.extra_up_with_fine_info_2(5), CharacteristicsColor.Gray),
					new ToolTipCharacteristics((flag || flag2) ? "" : Local.extra_up_with_fine_info_req, CharacteristicsColor.Wheat)
				});
			}
			return form;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x000A8170 File Offset: 0x000A6370
		private UiControl {22199}(InstalledShipUpgradeSlot {22200})
		{
			ShipUpgradeInstance installedUp = {22200}.Info;
			Form form = new Form(Vector2.Zero, {21684}.c_itemNew, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{22195}.WriteBasicData(installedUp.Info, form, new float?(installedUp.Strength), {22200}.SlotIndex == Global.Player.UsedShipPlayer.MaxUpgradesCount - 1);
			if (installedUp.Info.CategoryUi != ShipUpgradeCategory.Modification)
			{
				Button button = new Button(new Marker(340f, 10f, ref {21684}.c_smallButton).Scale(1.1f), {21684}.c_smallButton, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button.SetText(Local.to_hold, Fonts.Arial_12, Color.LightGray, false);
				button.FirstOpacity = 0f;
				button.UpdateComplete += delegate(UiControl {22240})
				{
					{22240}.FirstOpacity = (form.InputMode > MouseInputMode.NoFocus);
				};
				button.EvClick += delegate(ClickUiEventArgs {22241})
				{
					bool {22206} = Global.Player.UsedShipPlayer.HpFactor > 0.999f;
					if (installedUp.Info.HasEffect(ShipBonusEffect.MExtraUpgradePlaces))
					{
						foreach (InstalledShipUpgradeSlot installedShipUpgradeSlot in Global.Player.UsedShipPlayer.Upgrades.GetUpgrades().ToArray<InstalledShipUpgradeSlot>())
						{
							if (installedShipUpgradeSlot.Info.Info.CategoryUi != ShipUpgradeCategory.Modification && installedShipUpgradeSlot.Info.Info.CategoryUi != ShipUpgradeCategory.Sailes)
							{
								ShipUpgradeInstance {2063} = Global.Player.UsedShipPlayer.Upgrades.TakeOffUpgrade(Global.Player.UsedShipPlayer, installedShipUpgradeSlot.SlotIndex);
								Session.Account.Shipyard.AddUpgrade({2063}, Global.Player.UsedShipPlayer);
							}
						}
						Global.Player.UsedShipPlayer.Upgrades.OrderSlots();
						new {17312}(Local.PortUpgradeShipWindow_30);
						this.{22205}({22206});
						return;
					}
					Session.Account.Shipyard.AddUpgrade(Global.Player.UsedShipPlayer.Upgrades.TakeOffUpgrade(Global.Player.UsedShipPlayer, {22200}.SlotIndex), Global.Player.UsedShipPlayer);
					this.{22205}({22206});
				};
				button.ToolTipState = new ToolTipState(null, Local.PortUpgradeShipWindow_14, Array.Empty<ToolTipCharacteristics>());
				form.AddChild(button);
				if (installedUp.Info.WearType != UpgradeStrengthWear.None && Global.Game.GetCurrentSceneName == GameSceneName.Port)
				{
					Button button2 = new Button(new Marker(340f, 37f, ref {21684}.c_smallButton).Scale(1.1f), {21684}.c_smallButton, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					button2.FirstOpacity = 0f;
					button2.UpdateComplete += delegate(UiControl {22242})
					{
						{22242}.FirstOpacity = (form.InputMode > MouseInputMode.NoFocus);
					};
					button2.SetText(Local.mending, Fonts.Arial_12, Color.LightGray, false);
					button2.EvClick += delegate(ClickUiEventArgs {22243})
					{
						CraftingRecipe repairCost = installedUp.Info.GetRepairCost(installedUp.Strength, Session.Account, true);
						if (repairCost.IsAvailableFull(Session.Account))
						{
							repairCost.Apply(Session.Account);
							Global.Player.UsedShipPlayer.Upgrades.RestoreStrength(Global.Player.UsedShipPlayer, {22200}.SlotIndex);
							Global.Game.ScenePort.UpdateGuiForViewShip();
							this.UpdateBlocks(false);
						}
					};
					button2.ToolTip = new ToolTip(delegate(UiControl {22244})
					{
						TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_12, 1f);
						if (!string.IsNullOrEmpty(installedUp.Info.ToolTipOrEmpty))
						{
							textBlockBuilder.WriteLines(installedUp.Info.ToolTipOrEmpty, Color.Wheat * 0.5f, Fonts.Arial_10, 300f, new float?(0f));
						}
						CraftingRecipe repairCost = installedUp.Info.GetRepairCost(installedUp.Strength, Session.Account, true);
						repairCost.ToolTip(textBlockBuilder, 1);
						if (repairCost.IsAvailableFull(Session.Account))
						{
							textBlockBuilder.WriteLine(" ", Color.White);
							textBlockBuilder.WriteLine(Local.PortUpgradeShipWindow_5, Color.Gray);
						}
						textBlockBuilder.WriteLine(Local.upgrade_repair_info1, Color.Gray);
						textBlockBuilder.WriteLine(Local.upgrade_repair_info2, Color.Gray);
						return new ToolTipState(textBlockBuilder);
					});
					if (installedUp.Strength > (float)installedUp.Info.WearAmount.Value * 0.75f)
					{
						button2.AllowMouseInput = false;
						button2.Opacity = 0.4f;
					}
					form.AddChild(button2);
				}
			}
			return form;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x000A83B4 File Offset: 0x000A65B4
		protected override IEnumerable<UiControl> CreateDesignComponents()
		{
			{22195}.<CreateDesignComponents>d__15 <CreateDesignComponents>d__ = new {22195}.<CreateDesignComponents>d__15(-2);
			<CreateDesignComponents>d__.<>4__this = this;
			return <CreateDesignComponents>d__;
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x000A83C4 File Offset: 0x000A65C4
		protected override IEnumerable<UiControl> CreateElementsToSelection(object {22201})
		{
			{22195}.<CreateElementsToSelection>d__16 <CreateElementsToSelection>d__ = new {22195}.<CreateElementsToSelection>d__16(-2);
			<CreateElementsToSelection>d__.<>4__this = this;
			<CreateElementsToSelection>d__.<>3__parameter = {22201};
			return <CreateElementsToSelection>d__;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x000A83DC File Offset: 0x000A65DC
		private void {22202}(ShipUpgradeInfo {22203}, bool {22204})
		{
			if ({22204})
			{
				Global.Player.RestoreHp(Global.Player.UsedShip.MaxHp);
			}
			if ({22203}.HasEffect(ShipBonusEffect.MExtraUpgradePlaces))
			{
				GameplayHelper.CheckUpgrades();
			}
			if ({22203}.HasEffect(ShipBonusEffect.MPReduceCannonsQuantity))
			{
				GameplayHelper.CheckCannons();
			}
			if ({22203}.HasEffect(ShipBonusEffect.MExtraPlaces))
			{
				GameplayHelper.AutoDropOffCrew();
			}
			EducationHelper.MakeFlag(EducationOnboarding.InstallUpgrade, true);
			if (Global.Player.UsedShipPlayer.CraftFrom.ID != 2)
			{
				EducationHelper.MakeFlag(EducationOnboarding.InstallUpgradeForNonStartShip, true);
			}
			if ({22203}.GetEffects(Global.Player.UsedShipPlayer.CraftFrom).Any((ShipBonus {22236}) => {22236}.Type == ShipBonusEffect.MCapacity || {22236}.Type == ShipBonusEffect.PCapacity))
			{
				EducationHelper.MakeFlag(EducationOnboarding.InstallUpgradeForHold, true);
			}
			Global.Game.ScenePort.UpdateGuiForViewShip();
			base.CloseAllSelectionForm();
			base.UpdateBlocks(false);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x000A84B7 File Offset: 0x000A66B7
		private void {22205}(bool {22206})
		{
			Global.Player.RestoreHp({22206} ? Global.Player.UsedShip.MaxHp : 0f);
			GameplayHelper.AutoDropOffCrew();
			Global.Game.ScenePort.UpdateGuiForViewShip();
			base.UpdateBlocks(false);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x000A84F8 File Offset: 0x000A66F8
		public static bool CanBeInstalled(ShipUpgradeInfo {22207}, bool {22208} = false)
		{
			if (!{22208} && {22207}.CategoryUi == ShipUpgradeCategory.Modification && Global.Player.UsedShipPlayer.Upgrades.HavingModification != null)
			{
				new {17312}(Local.modification_already_installed);
				return false;
			}
			foreach (ShipBonus shipBonus in {22207}.GetEffects(Global.Player.UsedShipPlayer.CraftFrom))
			{
				if (shipBonus.Value > 0f && Global.Player.UsedShipPlayer.Upgrades.GetQuantityOfEffect(shipBonus.Type, false) - (({22208} > false) ? 1 : 0) >= 2)
				{
					new {17312}(Local.PortUpgradeShipWindow_2);
					return false;
				}
			}
			if ({22207}.IsMortarUpgrade && Global.Player.UsedShipPlayer.Upgrades.MortarUpgradesCount - (({22208} > false) ? 1 : 0) >= 2)
			{
				new {17312}(Local.PortUpgradeShipWindow_2b);
				return false;
			}
			return true;
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x000A85F0 File Offset: 0x000A67F0
		private static void WriteBasicData(ShipUpgradeInfo {22209}, Form {22210}, float? {22211}, bool {22212})
		{
			{22210}.AddChild(new Image({22195}.p_item_Icon.Offset({22210}.Pos.XY), {22209}.IconTexture, {22209}.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			Label {13204} = new Label({22210}.Pos.XY + new Vector2(135f, 15f), Fonts.Arial_12, Color.LightGray * 0.9f, {22209}.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{22210}.AddChild({13204});
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_9, 0f);
			bool flag = false;
			ShipBonus[] array = {22209}.GetEffects(Global.Player.UsedShipPlayer.CraftFrom).ToArray<ShipBonus>();
			int num = 0;
			foreach (ShipBonus shipBonus in array)
			{
				if (textBlockBuilder.CurrentLocalPoint.X > 180f || (!flag && ((array.Length == 2 && num == 1) || shipBonus.Value < 0f)))
				{
					textBlockBuilder.WriteLine("", Color.White);
					flag = true;
				}
				textBlockBuilder.Write(shipBonus.ToString() + "   ", Color.Gray);
				num++;
			}
			if ({22212})
			{
				ShipBonus[] array3 = Global.Player.UsedShipPlayer.Upgrades.CurrentExtraUpgradeFine(Global.Player.UsedShipPlayer);
				textBlockBuilder.WriteLine("", Color.White);
				foreach (ShipBonus shipBonus2 in array3)
				{
					textBlockBuilder.Write(shipBonus2.ToString() + "   ", Color.Orange);
				}
			}
			if ({22209}.WearType != UpgradeStrengthWear.None)
			{
				textBlockBuilder.WriteSpaceLine(Math.Max(3f, 39f - textBlockBuilder.Size.Y));
				TextBlockBuilder textBlockBuilder2 = textBlockBuilder;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
				defaultInterpolatedStringHandler.AppendFormatted(Local.wear);
				defaultInterpolatedStringHandler.AppendLiteral(": ");
				defaultInterpolatedStringHandler.AppendFormatted<int>((int)({22211} ?? ((float){22209}.WearAmount.Value)));
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted({22209}.WearType.ToStringLocal());
				textBlockBuilder2.WriteLine(defaultInterpolatedStringHandler.ToStringAndClear(), ({22211} != null && {22211}.Value == 0f) ? Color.OrangeRed : {22195}.salateColor);
				if ({22211} != null)
				{
					{22210}.AddChild(new ProgressBar({22210}.Pos.XY + new Vector2(37f, 77f), AtlasPortGui.cSmallProgressBarFrontYel, AtlasPortGui.cSmallProgressBarBack, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						MaxValue = (float){22209}.WearAmount.Value,
						Value = {22211}.Value
					});
				}
			}
			{22210}.AddChild(textBlockBuilder.Create({22210}.Pos.XY + new Vector2(135f, 37f)));
			{22210}.ToolTip = new ToolTip({22195}.GenericToolTip({22209}, "", false));
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x000A8918 File Offset: 0x000A6B18
		private static void WriteBasicDataMini(ShipUpgradeInfo {22213}, Form {22214}, float? {22215}, bool {22216}, bool {22217})
		{
			Image image = new Image({22195}.p_item_Icon_mini.Offset({22214}.Pos.XY).Border(-2f), {22213}.IconTexture, {22213}.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{22214}.AddChild(image);
			Label label = new Label({22214}.Pos.XY + new Vector2(76f, 9f), Fonts.Arial_12, {22217} ? Color.LightGray : Color.Gray, {22213}.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{22214}.AddChild(label);
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_9, 0f);
			ShipBonus[] array = {22213}.GetEffects(Global.Player.UsedShipPlayer.CraftFrom).ToArray<ShipBonus>();
			int num = 0;
			foreach (ShipBonus shipBonus in array)
			{
				Color color = ({22216} || (Global.Player.UsedShipPlayer.Upgrades.GetQuantityOfEffect(shipBonus.Type, false) < 2 && (!{22213}.IsMortarUpgrade || Global.Player.UsedShipPlayer.Upgrades.MortarUpgradesCount < 2))) ? Color.Gray : (Color.OrangeRed * 0.7f);
				if (textBlockBuilder.CurrentLocalPoint.X >= (float)((array.Length > 4) ? 320 : 180) || (array.Length == 2 && num == 1))
				{
					textBlockBuilder.WriteLine("", color);
				}
				textBlockBuilder.Write(shipBonus.ToString() + "   ", color);
				num++;
			}
			{22214}.AddChild(textBlockBuilder.Create({22214}.Pos.XY + new Vector2(76f, 27f)));
			if ({22217} || {22216})
			{
				Label {13204} = new Label(label.Pos.XY + new Vector2(label.Pos.WH.X + 4f, 0f), label.Font, {22217} ? new Color(142, 170, 83) : Color.LightYellow, "(" + ({22217} ? Local.in_storage_2 : Local.installed).ToLower() + ")", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				{22214}.AddChild({13204});
			}
			{22214}.ToolTip = new ToolTip({22195}.GenericToolTip({22213}, {22217} ? Local.click_for_install : "", !{22216} && !{22217}));
			if ({22213}.WearType != UpgradeStrengthWear.None)
			{
				ToolTipState currentContent = {22214}.ToolTip.CurrentContent;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
				defaultInterpolatedStringHandler.AppendFormatted(Local.wear);
				defaultInterpolatedStringHandler.AppendLiteral(": ");
				defaultInterpolatedStringHandler.AppendFormatted<int>((int)({22215} ?? ((float){22213}.WearAmount.Value)));
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted({22213}.WearType.ToStringLocal());
				currentContent.AppendText(defaultInterpolatedStringHandler.ToStringAndClear(), Color.Wheat, false, false);
				if ({22215} != null)
				{
					float {11535} = 0f;
					float {11536} = 0f;
					Vector2 vector = AtlasPortGui.cSmallProgressBarFrontYel.WidthHeight() * 0.9f;
					image.AddChildPos(new ProgressBar(new Marker({11535}, {11536}, ref vector), AtlasPortGui.cSmallProgressBarFrontYel, AtlasPortGui.cSmallProgressBarBack, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						MaxValue = (float){22213}.WearAmount.Value,
						Value = {22215}.Value
					}, PositionAlignment.Center, PositionAlignment.RightDown, 5f);
				}
			}
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x000A8CC4 File Offset: 0x000A6EC4
		private static ToolTipState GenericToolTip(ShipUpgradeInfo {22218}, string {22219}, bool {22220})
		{
			string name = {22218}.Name;
			string {12778} = (({22218}.CategoryUi == ShipUpgradeCategory.Modification) ? Local.modification_tt : "") + {22218}.ToolTipOrEmpty;
			ToolTipCharacteristics[] {12779};
			if (!string.IsNullOrEmpty({22219}))
			{
				({12779} = new ToolTipCharacteristics[1])[0] = new ToolTipCharacteristics({22219}, CharacteristicsColor.Lime);
			}
			else
			{
				{12779} = new ToolTipCharacteristics[0];
			}
			ToolTipState toolTipState = new ToolTipState(name, {12778}, {12779});
			if ({22220})
			{
				foreach (ShipBonus shipBonus in {22218}.GetEffects(Global.Player.UsedShipPlayer.CraftFrom))
				{
					toolTipState.AppendText(shipBonus.ToString(), Color.Wheat, false, false);
				}
				toolTipState.AppendText("", Color.Wheat, false, false);
				CraftingRecipe craft = {22218}.GetCraft(Global.Player.UsedShipPlayer.CraftFrom, Session.Account, true);
				if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
				{
					craft.ToolTip(toolTipState.Builder, 1);
					if (craft.IsAvailableFull(Session.Account))
					{
						toolTipState.Builder.WriteLine(Local.click_for_install, Color.SoftLime);
					}
				}
			}
			if ({22218}.CategoryUi == ShipUpgradeCategory.Sailes)
			{
				float num = 1f + Global.Player.UsedShip.MarchingModeSpeedBonusSail;
				float marchingModeSpeed = Global.Player.UsedShip.MarchingModeSpeed;
				float num2 = 1f + {22218}.GetEffects(Global.Player.UsedShipPlayer.CraftFrom).First<ShipBonus>().Value / 100f;
				toolTipState.Builder.WriteLine(Local.sail_upgrade_tt_shipSpeed + Math.Round((double)(Global.Player.UsedShip.Speed * PlayerShipInfo.Temp_displaySpeedRefactoring), 1).ToString(), Color.Gray, Fonts.Arial_10);
				toolTipState.Builder.WriteLine(string.Concat(new string[]
				{
					Local.sail_upgrade_tt_marchingSpeed,
					Math.Round((double)(marchingModeSpeed / num * PlayerShipInfo.Temp_displaySpeedRefactoring), 1).ToString(),
					" + ",
					((int)((num2 - 1f) * 100f)).ToString(),
					"% = ",
					Math.Round((double)(marchingModeSpeed / num * num2 * PlayerShipInfo.Temp_displaySpeedRefactoring), 1).ToString()
				}), Color.Gray, Fonts.Arial_10);
				toolTipState.Builder.WriteLine(Local.sail_upgrade_tt_total + Math.Round((double)((Global.Player.UsedShip.Speed + marchingModeSpeed / num * num2) * PlayerShipInfo.Temp_displaySpeedRefactoring), 1).ToString(), Color.Gray, Fonts.Arial_10);
			}
			return toolTipState;
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x000A8F74 File Offset: 0x000A7174
		public static ToolTipState MakeToolTip(ShipUpgradeInfo {22221})
		{
			return new ToolTipState({22221}.Name, "", (from {22237} in {22221}.GetEffects(Global.Player.UsedShipPlayer.CraftFrom)
			select new ToolTipCharacteristics({22237}.ToString(), CharacteristicsColor.Wheat)).ToArray<ToolTipCharacteristics>());
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x000A8FCF File Offset: 0x000A71CF
		protected override void UserBackRender()
		{
			if (Global.Game.GetCurrentSceneName == GameSceneName.Game)
			{
				Engine.GS.SetTexture(AtlasPortGui.Texture.Tex);
			}
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x000A8FF2 File Offset: 0x000A71F2
		protected override void UserFrontRender()
		{
			if (Global.Game.GetCurrentSceneName == GameSceneName.Game)
			{
				Engine.GS.SetTexture(AtlasGameGui.Texture.Tex);
			}
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x000A910E File Offset: 0x000A730E
		[CompilerGenerated]
		private void {22222}(string {22223})
		{
			if (this.{22227} == {22223})
			{
				return;
			}
			this.{22227} = {22223};
			base.CloseAllSelectionForm();
			base.OpenSelectionForm(new {22195}.SelectionFormParameter(false, -1, {22223}));
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x000A913F File Offset: 0x000A733F
		[CompilerGenerated]
		private void {22224}(ClickUiEventArgs {22225})
		{
			base.OpenSelectionForm(new {22195}.SelectionFormParameter(false, -1, ""));
		}

		// Token: 0x04001214 RID: 4628
		private static readonly Marker p_item_Icon = new Marker(23f, 11f, 83f, 83f);

		// Token: 0x04001215 RID: 4629
		private static readonly Marker p_item_Icon_mini = new Marker(0f, 0f, 61f, 61f);

		// Token: 0x04001216 RID: 4630
		private static readonly Rectangle c_improvementEmptySlot = new Rectangle(1531, 991, 57, 57);

		// Token: 0x04001217 RID: 4631
		private static readonly Rectangle c_improvement1 = new Rectangle(1589, 991, 57, 57);

		// Token: 0x04001218 RID: 4632
		private static readonly Rectangle c_newDeleteButton = new Rectangle(1501, 1247, 34, 34);

		// Token: 0x04001219 RID: 4633
		private static readonly Rectangle c_buyForMarksButton = new Rectangle(1536, 1247, 34, 34);

		// Token: 0x0400121A RID: 4634
		private static readonly Rectangle c_buyForMarksButtonLocked = new Rectangle(1571, 1247, 34, 34);

		// Token: 0x0400121B RID: 4635
		private static readonly Rectangle c_newItemBackground = new Rectangle(1489, 1284, 523, 61);

		// Token: 0x0400121C RID: 4636
		private static readonly Color salateColor = new Color(94, 96, 78) * 1.6f;

		// Token: 0x0400121D RID: 4637
		private Button {22226};

		// Token: 0x0400121E RID: 4638
		private string {22227};

		// Token: 0x02000397 RID: 919
		private struct SelectionFormParameter
		{
			// Token: 0x06001402 RID: 5122 RVA: 0x000A9158 File Offset: 0x000A7358
			public SelectionFormParameter(bool {22231}, int {22232}, string {22233})
			{
				this.isSail = {22231};
				this.dstSlotIndex = {22232};
				this.search = {22233};
			}

			// Token: 0x0400121F RID: 4639
			public bool isSail;

			// Token: 0x04001220 RID: 4640
			public int dstSlotIndex;

			// Token: 0x04001221 RID: 4641
			public string search;
		}
	}
}
