using System;
using System.Collections.Generic;
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
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002C9 RID: 713
	internal sealed class {20881} : {20856}, IPortPage, IComparer<ShipDesignInfo>
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x000070D7 File Offset: 0x000052D7
		protected override bool makeBackGray
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0008319A File Offset: 0x0008139A
		public static {20881} CurrentInstance
		{
			get
			{
				return {20881}.currentInstance;
			}
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000831A1 File Offset: 0x000813A1
		public static void RedirectToSubscriptionsPage(int {20883} = -1)
		{
			if ({20881}.currentInstance != null)
			{
				{20881}.highlightIdPremiumPage = {20883};
				{20881}.currentInstance.ForceItemSelected(3);
				{20881}.highlightIdPremiumPage = -1;
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000831C1 File Offset: 0x000813C1
		public static void RedirectToDesignsPage(ShipDesignCategory {20884})
		{
			{20881} {20881} = {20881}.currentInstance;
			if ({20881} == null)
			{
				return;
			}
			{20881}.ForceItemSelected(({20884} == ShipDesignCategory.BowFigure || {20884} == ShipDesignCategory.ShipFullDesign) ? 2 : 1);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x000831E0 File Offset: 0x000813E0
		private static {20856}.UserContents[] data(DonationSystem.PacketOffer {20885})
		{
			{20856}.UserContents[] array = new {20856}.UserContents[4];
			array[0] = new {20856}.UserContents(Local.PortRealShopPage_0, delegate(StackForm {21017})
			{
				{20881}.MakeMainPage({21017}, {20885});
			});
			array[1] = new {20856}.UserContents(Local.design, delegate(StackForm {20990})
			{
				{20881}.pageDesigns({20990}, false);
			});
			array[2] = new {20856}.UserContents(Local.bowFigures, delegate(StackForm {20991})
			{
				{20881}.pageDesigns({20991}, true);
			});
			int num = 3;
			string subs = Local.subs;
			Action<StackForm> {20876};
			if (({20876} = {20881}.<>O.<0>__MakePremiumPage) == null)
			{
				{20876} = ({20881}.<>O.<0>__MakePremiumPage = new Action<StackForm>({20881}.MakePremiumPage));
			}
			array[num] = new {20856}.UserContents(subs, {20876});
			return array;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x000832AC File Offset: 0x000814AC
		private static string multToStr(float {20886})
		{
			if ({20886} < 1f)
			{
				return "-" + ((int)Math.Round((double)((1f - {20886}) * 100f))).ToString() + "%";
			}
			return null;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x000832EE File Offset: 0x000814EE
		public static {20881} Open(DonationSystem.PacketOffer {20887} = null)
		{
			if (PlatformTuning.DisableShop)
			{
				return null;
			}
			{20881} {20881} = {20881}.currentInstance;
			if ({20881} != null)
			{
				{20881}.RemoveFromContainer();
			}
			return {20881}.currentInstance = new {20881}({20887});
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00083318 File Offset: 0x00081518
		private {20881}(DonationSystem.PacketOffer {20888} = null) : base({20881}.data({20888}))
		{
			this.AnimatedFocus = false;
			Vector2 vector = base.Pos.XY + new Vector2(580f, 24f);
			Form form = new Form(new Marker(ref vector, 418.3333f, 36.666664f), new Rectangle(786, 531, 502, 44), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			base.AddChild(form);
			LiveLabel liveLabel = new LiveLabel(Vector2.Zero, Fonts.Philosopher_16, new Color(216, 196, 80), () => Local.Monets2 + " " + Session.Account.Monets.Value.ToString(), 250);
			form.AddChildPos(liveLabel, PositionAlignment.LeftUp, PositionAlignment.Center, 64f);
			liveLabel.Pos = liveLabel.Pos.Offset(0f, 2f);
			vector = Vector2.Zero;
			AnimatedButton animatedButton = new AnimatedButton(new Marker(ref vector, 182f, 28f), {20881}.c_button_anim1, {20881}.c_button_anim2, {20881}.c_button_focus, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {20992})
			{
				{21395}.Open(0);
			});
			animatedButton.SetText(Local.PortRealShopPage_64, Fonts.F_m14_ThinBold, Color.White, false);
			form.AddChildPos(animatedButton, PositionAlignment.RightDown, PositionAlignment.Center, 10f);
			animatedButton.Pos = animatedButton.Pos.Offset(0f, 1f);
			if (Global.Player.IsPortEntry)
			{
				Global.Game.ScenePort.MakeAccSync();
			}
			Global.Player.UpdateSailClotting();
			base.EvRemoveFromContainer += delegate()
			{
				{20881}.currentInstance = null;
				try
				{
					Global.Player.UpdateSailClotting();
				}
				catch
				{
				}
			};
			EducationHelper.MakeFlag(EducationOnboarding.OpenRealShop, false);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000834F4 File Offset: 0x000816F4
		private static void MakeMainPage(StackForm {20889}, DonationSystem.PacketOffer {20890})
		{
			if (EducationHelper.ShowRealShopToolTip)
			{
				{20889}.AddSpace(12f);
				StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm.AddSpace(7f);
				stackForm.AddItem(new UiControl[]
				{
					TextBlockBuilder.CreateBlockSpecial(800f, Local.real_shop_new_player, Color.LightGray, Color.Gold, Fonts.Philosopher_16, Fonts.Philosopher_16, 0).Create()
				});
				{20889}.AddItem(new UiControl[]
				{
					stackForm
				});
				{20889}.AddSpace(12f);
			}
			StackForm stackForm2 = new StackForm({20889}.Pos.XY, UiOrientation.ExpansiveSize, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Form form;
			if (DonationSystem.Packets[0].CheckForDisplay())
			{
				{20881}.<>c__DisplayClass33_0 CS$<>8__locals1 = new {20881}.<>c__DisplayClass33_0();
				CS$<>8__locals1.chest1 = {20881}.BigBlockHelper(DonationSystem.Packets[0], 0f, 0f, 188f, {20881}.RectangleSize.Size2x2, null, false, null, null, null);
				form = new Form(CS$<>8__locals1.chest1.Pos.XY + new Vector2(4f, 4f), new Rectangle(3375, 1125, 306, 400), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				CS$<>8__locals1.chest1.AddChild(form);
				form.MoveToBackLevel();
				stackForm2.AddItem(new UiControl[]
				{
					CS$<>8__locals1.chest1
				});
				CS$<>8__locals1.chest1.ToolTipState = null;
				CS$<>8__locals1.chest1.ToolTip = new ToolTip((UiControl {20993}) => DonationSystem.Packets[0].RichTooltip());
				CS$<>8__locals1.options = new StackForm(CS$<>8__locals1.chest1.Pos.XY + new Vector2(5f, 240f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Button button = CS$<>8__locals1.<MakeMainPage>g__CreateOptionButton|5();
				button.EvClick += delegate(ClickUiEventArgs {20994})
				{
					if (Session.Account.NearPortStorage[43] >= 10)
					{
						DonationSystem.OpenChest(OnOpenChestMsg.Flags.OpenSingleChest, true, DonationSystem.Packets[0].GetFinalPrice());
					}
				};
				button.ToolTip = new ToolTip(delegate(UiControl {21018})
				{
					CS$<>8__locals1.chest1.ToolTip.CloseIfIsOpen();
					Composer composer = new Composer(300f, -1f);
					Composer composer2 = composer;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
					defaultInterpolatedStringHandler.AppendFormatted(Local.PortRealShopPage_3_b);
					defaultInterpolatedStringHandler.AppendFormatted(Environment.NewLine);
					defaultInterpolatedStringHandler.AppendFormatted<int>(Session.Account.NearPortStorage[43]);
					defaultInterpolatedStringHandler.AppendLiteral(" / 10");
					composer2.AddText(defaultInterpolatedStringHandler.ToStringAndClear(), ComposerTextStyle.Wheat, false);
					if (Session.Account.NearPortStorage[43] >= 10)
					{
						composer.AddSeparatorWosb("⚓");
						composer.AddText(Local.press_to_open, ComposerTextStyle.LimeBold, true);
					}
					return new ToolTipState(composer);
				});
				button.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, Color.SkyBlue, Local.PortRealShopPage_buyChestForKeys, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, 13f);
				Button button2 = CS$<>8__locals1.<MakeMainPage>g__CreateOptionButton|5();
				button2.EvClick += delegate(ClickUiEventArgs {20995})
				{
					if (DonationSystem.Packets[1].GetFinalPrice().Value > Session.Account.Monets.Value)
					{
						{20881}.ShowBuyMonetsToolTip(DonationSystem.Packets[1].GetFinalPrice().Value);
						return;
					}
					DonationSystem.Packets[1].Click();
				};
				button2.ToolTip = new ToolTip(delegate(UiControl {21019})
				{
					CS$<>8__locals1.chest1.ToolTip.CloseIfIsOpen();
					return DonationSystem.Packets[1].RichTooltip();
				});
				StackForm stackForm3 = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm3.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat, Local.open_3_tt + " ", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				stackForm3.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Gold, DonationSystem.Packets[1].GetFinalPrice().ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				button2.AddChildPos(stackForm3, PositionAlignment.LeftUp, PositionAlignment.Center, 13f);
				CS$<>8__locals1.options.Pos = CS$<>8__locals1.options.Pos.Offset(0f, -CS$<>8__locals1.options.Pos.WH.Y);
				CS$<>8__locals1.chest1.AddChild(CS$<>8__locals1.options);
			}
			if (DonationSystem.Packets[25].CheckForDisplay())
			{
				Form form2 = {20881}.BigBlockHelper(DonationSystem.Packets[25], 2f, 1f, 42f, {20881}.RectangleSize.Size2x1, null, false, null, null, null);
				form = new Form(form2.Pos.XY + new Vector2(4f, 4f), new Rectangle(2099, 1637, 306, 148), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form2.AddChild(form);
				form.MoveToBackLevel();
				stackForm2.AddItem(new UiControl[]
				{
					form2
				});
				form2.ToolTipState = null;
				form2.ToolTip = new ToolTip((UiControl {20996}) => DonationSystem.Packets[25].RichTooltip());
			}
			if (DonationSystem.Packets[26].CheckForDisplay())
			{
				Form form3 = {20881}.BigBlockHelper(DonationSystem.Packets[26], 2f, 0f, 42f, {20881}.RectangleSize.Size2x1, null, false, null, null, null);
				form = new Form(form3.Pos.XY + new Vector2(4f, 4f), new Rectangle(3370, 976, 306, 148), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form3.AddChild(form);
				form.MoveToBackLevel();
				stackForm2.AddItem(new UiControl[]
				{
					form3
				});
				form3.ToolTipState = null;
				form3.ToolTip = new ToolTip((UiControl {20997}) => DonationSystem.Packets[26].RichTooltip());
			}
			if (DonationSystem.Packets[28].CheckForDisplay())
			{
				Form form4 = {20881}.BigBlockHelper(DonationSystem.Packets[28], 4f, 1f, 42f, {20881}.RectangleSize.Size2x1, null, false, null, null, null);
				form = new Form(form4.Pos.XY + new Vector2(4f, 4f), new Rectangle(1944, 1359, 306, 148), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form4.AddChild(form);
				form.MoveToBackLevel();
				stackForm2.AddItem(new UiControl[]
				{
					form4
				});
				form4.ToolTipState = null;
				form4.ToolTip = new ToolTip((UiControl {20998}) => DonationSystem.Packets[28].RichTooltip());
			}
			Form form5 = {20881}.BigBlockHelper(DonationSystem.Packets[2], 4f, 0f, -30f, {20881}.RectangleSize.Size2x1, null, false, null, null, null);
			form5.AddChild(new Button(form5.Pos.XY + new Vector2(12f, 83f), AtlasPortGui.buttonYellow, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.PortRealShopPage_13({20881}.placeInPortCost_r.Value), Fonts.Philosopher_12, Color.Gold * 0.8f, false).ExClick(delegate(ClickUiEventArgs {20999})
			{
				DonationSystem.BuyPlaceInPort(true);
			}));
			form5.AddChild(new Button(form5.Pos.XY + new Vector2(156f, 83f), AtlasPortGui.buttonYellow, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetLiveText(() => Local.PortRealShopPage_14(Gameplay.PlaceForShipCostVSkull(Session.Account)), Fonts.Philosopher_12, Color.White * 0.8f, false).ExClick(delegate(ClickUiEventArgs {21000})
			{
				DonationSystem.BuyPlaceInPort(false);
			}));
			form = new Form(form5.Pos, new Rectangle(2737, 724, 172, 82), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form5.AddChild(form);
			form.MoveToBackLevel();
			stackForm2.AddItem(new UiControl[]
			{
				form5
			});
			if (!DonationSystem.Packets[28].CheckForDisplay())
			{
				Form form6 = {20881}.BigBlockHelper(DonationSystem.Packets[3], 4f, 1f, 0f, {20881}.RectangleSize.Size2x1, null, false, null, null, null);
				stackForm2.AddItem(new UiControl[]
				{
					form6
				});
				Form form7 = new Form(form6.Pos, new Rectangle(2765, 1036, 186, 93), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form6.AddChild(form7);
				form7.MoveToBackLevel();
			}
			if ((!DonationSystem.SwitchStartPackets && {20890} != DonationSystem.Packets[4]) || {20890} == DonationSystem.Packets[10])
			{
				stackForm2.AddItem(new UiControl[]
				{
					{20881}.BigBlockHelper(DonationSystem.Packets[10], 3f, 2f, 0f, {20881}.RectangleSize.SizeHxM, new Rectangle?({20881}.c_anchSilver), {20890} == DonationSystem.Packets[10], Local.PortRealShop_packetTt, null, null)
				});
			}
			else
			{
				stackForm2.AddItem(new UiControl[]
				{
					{20881}.BigBlockHelper(DonationSystem.Packets[4], 3f, 2f, 0f, {20881}.RectangleSize.SizeHxM, new Rectangle?({20881}.c_anchSilver), {20890} == DonationSystem.Packets[4], Local.PortRealShopPage_Buy, null, null)
				});
			}
			if ((!DonationSystem.SwitchStartPackets && {20890} != DonationSystem.Packets[6]) || {20890} == DonationSystem.Packets[8])
			{
				stackForm2.AddItem(new UiControl[]
				{
					{20881}.BigBlockHelper(DonationSystem.Packets[8], 4.5f, 2f, 0f, {20881}.RectangleSize.SizeHxM, new Rectangle?({20881}.c_anchOrange), {20890} == DonationSystem.Packets[8], Local.PortRealShop_packetTt, null, null)
				});
			}
			else
			{
				stackForm2.AddItem(new UiControl[]
				{
					{20881}.BigBlockHelper(DonationSystem.Packets[6], 4.5f, 2f, 0f, {20881}.RectangleSize.SizeHxM, new Rectangle?({20881}.c_anchOrange), {20890} == DonationSystem.Packets[6], Local.PortRealShop_packetTt, null, null)
				});
			}
			if ((!DonationSystem.SwitchStartPackets && {20890} != DonationSystem.Packets[9]) || {20890} == DonationSystem.Packets[7])
			{
				stackForm2.AddItem(new UiControl[]
				{
					{20881}.BigBlockHelper(DonationSystem.Packets[7], 3f, 3.33f, 0f, {20881}.RectangleSize.SizeHxM, new Rectangle?({20881}.c_anchGold), {20890} == DonationSystem.Packets[7], Local.PortRealShop_packetTt, null, null)
				});
			}
			else
			{
				stackForm2.AddItem(new UiControl[]
				{
					{20881}.BigBlockHelper(DonationSystem.Packets[9], 3f, 3.33f, 0f, {20881}.RectangleSize.SizeHxM, new Rectangle?({20881}.c_anchGold), {20890} == DonationSystem.Packets[9], Local.PortRealShop_packetTt, null, null)
				});
			}
			stackForm2.AddItem(new UiControl[]
			{
				{20881}.BigBlockHelper(DonationSystem.Packets[5], 4.5f, 3.33f, 0f, {20881}.RectangleSize.SizeHxM, new Rectangle?({20881}.c_anchViolet), {20890} == DonationSystem.Packets[5], Local.PortRealShop_packetTt, null, null)
			});
			stackForm2.AddItem(new UiControl[]
			{
				{20881}.LineWithiconHelper(DonationSystem.Packets[11], 0f, 2f, new Rectangle(2492, 1261, 64, 64), {20881}.RectangleSize.Size3xH, false, null, false, 375f)
			});
			stackForm2.AddItem(new UiControl[]
			{
				{20881}.LineWithiconHelper(DonationSystem.Packets[13], 0f, 3.34f, new Rectangle(2622, 1261, 64, 64), {20881}.RectangleSize.Size3xH, false, null, false, 375f)
			});
			Form form8 = {20881}.LineWithiconHelper(DonationSystem.Packets[17], 3f, 4.67f, new Rectangle(2882, 1261, 64, 64), {20881}.RectangleSize.BlockInside, false, null, true, 375f);
			stackForm2.AddItem(new UiControl[]
			{
				form8
			});
			Form form9 = {20881}.LineWithiconHelper(DonationSystem.Packets[18], 4.5f, 4.67f, new Rectangle(2557, 1261, 64, 64), {20881}.RectangleSize.BlockInside, false, null, true, 375f);
			stackForm2.AddItem(new UiControl[]
			{
				form9
			});
			Form form10 = DonationSystem.Packets[20].CheckForDisplay() ? {20881}.LineWithiconHelper(DonationSystem.Packets[20], 0f, 2.676f, new Rectangle(2936, 1678, 64, 64), {20881}.RectangleSize.Size3xH, false, Local.PortRealShopPage_139, false, 375f) : {20881}.LineWithiconHelper(DonationSystem.Packets[21], 0f, 2.676f, new Rectangle(2936, 1678, 64, 64), {20881}.RectangleSize.Size3xH, false, Local.PortRealShopPage_139, false, 375f);
			form10.AddChild(new LiveLabel(form10.Pos.XY + new Vector2(85f, 78f), Fonts.Philosopher_12, Color.GreenYellow * 0.8f, delegate()
			{
				if (Session.Account.TemporaryFreeResTravel.Value <= 0)
				{
					return "";
				}
				return Local.PortRealShopPage_145(Session.Account.TemporaryFreeResTravel.Value);
			}, 100));
			stackForm2.AddItem(new UiControl[]
			{
				form10
			});
			if (DonationSystem.Packets[23].CheckForDisplay())
			{
				stackForm2.AddItem(new UiControl[]
				{
					{20881}.LineWithiconHelper(DonationSystem.Packets[23], 0f, 4.002f, new Rectangle(3003, 1527, 64, 64), {20881}.RectangleSize.Size3xH, false, null, true, 375f)
				});
			}
			if (DonationSystem.Packets[24].CheckForDisplay())
			{
				stackForm2.AddItem(new UiControl[]
				{
					{20881}.LineWithiconHelper(DonationSystem.Packets[24], 0f, 4.6740003f, new Rectangle(2938, 1527, 64, 64), {20881}.RectangleSize.Size3xH, false, null, true, 375f)
				});
			}
			if (DonationSystem.Packets[27].CheckForDisplay())
			{
				stackForm2.AddItem(new UiControl[]
				{
					{20881}.LineWithiconHelper(DonationSystem.Packets[27], 4.5f, 5.334f, new Rectangle(2936, 1778, 64, 64), {20881}.RectangleSize.BlockInside, false, null, true, 150f)
				});
			}
			stackForm2.AddSpace(30f);
			{20889}.AddItem(new UiControl[]
			{
				stackForm2
			});
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00084280 File Offset: 0x00082480
		private static void pageDesigns(StackForm {20891}, bool {20892})
		{
			LinkedDictionrary<ShipDesignCategory, ShipDesignInfo> linkedDictionrary = new LinkedDictionrary<ShipDesignCategory, ShipDesignInfo>(500);
			foreach (ShipDesignInfo shipDesignInfo in ((IEnumerable<ShipDesignInfo>)Gameplay.DesignsInfo))
			{
				if ({20892} ? (shipDesignInfo.Category == ShipDesignCategory.BowFigure || shipDesignInfo.Category == ShipDesignCategory.ShipFullDesign) : (shipDesignInfo.Category != ShipDesignCategory.BowFigure && shipDesignInfo.Category != ShipDesignCategory.ShipFullDesign))
				{
					linkedDictionrary.Add(shipDesignInfo.Category, shipDesignInfo);
				}
			}
			BlocksStackFormControl blocksStackFormControl = new BlocksStackFormControl(Vector2.Zero, 4, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				MaxWidth = 975f
			};
			{20891}.AddItem(new UiControl[]
			{
				blocksStackFormControl
			});
			Dictionary<string, Tlist<ShipDesignInfo>> dictionary = new Dictionary<string, Tlist<ShipDesignInfo>>();
			dictionary.Add(ShipDesignCategory.Satellite.ToStrLocal(), linkedDictionrary[ShipDesignCategory.Satellite]);
			dictionary.Add(ShipDesignCategory.BowFigure.ToStrLocal(), linkedDictionrary[ShipDesignCategory.BowFigure]);
			for (int i = 1; i <= 7; i++)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
				defaultInterpolatedStringHandler.AppendFormatted(Local.StringConstants_174);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(StringHelper.ToRoman(i));
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(Local.StringConstants_115.TrimStart());
				string key = defaultInterpolatedStringHandler.ToStringAndClear();
				dictionary[key] = new Tlist<ShipDesignInfo>();
			}
			foreach (ShipDesignInfo shipDesignInfo2 in ((IEnumerable<ShipDesignInfo>)linkedDictionrary[ShipDesignCategory.ShipFullDesign]))
			{
				if (!(shipDesignInfo2.Name == Local.removed) && shipDesignInfo2.AssociatedShip != null)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(2, 3);
					defaultInterpolatedStringHandler2.AppendFormatted(Local.StringConstants_174);
					defaultInterpolatedStringHandler2.AppendLiteral(" ");
					defaultInterpolatedStringHandler2.AppendFormatted(StringHelper.ToRoman(shipDesignInfo2.AssociatedShip.Rank));
					defaultInterpolatedStringHandler2.AppendLiteral(" ");
					defaultInterpolatedStringHandler2.AppendFormatted(Local.StringConstants_115.TrimStart());
					string key2 = defaultInterpolatedStringHandler2.ToStringAndClear();
					dictionary[key2].Add(shipDesignInfo2);
				}
			}
			dictionary.Add(ShipDesignCategory.SailTexture.ToStrLocal(), linkedDictionrary[ShipDesignCategory.SailTexture]);
			dictionary.Add(ShipDesignCategory.Decal1.ToStrLocal(), linkedDictionrary[ShipDesignCategory.Decal1]);
			dictionary.Add(ShipDesignCategory.Decal2.ToStrLocal(), linkedDictionrary[ShipDesignCategory.Decal2]);
			dictionary.Add(ShipDesignCategory.Flag.ToStrLocal(), linkedDictionrary[ShipDesignCategory.Flag]);
			dictionary.Add(ShipDesignCategory.MastColor.ToStrLocal(), linkedDictionrary[ShipDesignCategory.MastColor]);
			if (!{20892})
			{
				HashSet<ShipDesignInfo> hashSet = new HashSet<ShipDesignInfo>();
				foreach (ShipDesignInfo item in from {21001} in WosbTrading.PirateTrader
				where {21001}.DesignId > 0
				select {21001} into {21002}
				select Gameplay.DesignsInfo[{21002}.DesignId])
				{
					hashSet.Add(item);
				}
				foreach (ShipDesignInfo item2 in from {21003} in WosbTrading.ArenaTrader
				where {21003}.Item2.DesignId > 0
				select {21003} into {21004}
				select Gameplay.DesignsInfo[{21004}.Item2.DesignId])
				{
					hashSet.Add(item2);
				}
				dictionary.Add(Local.unique, new Tlist<ShipDesignInfo>(hashSet.ToArray<ShipDesignInfo>()));
			}
			int num = 0;
			foreach (KeyValuePair<string, Tlist<ShipDesignInfo>> keyValuePair in dictionary)
			{
				if (num > 0 && keyValuePair.Value.Size > 0)
				{
					Composer composer = new Composer(500f, -2f);
					composer.AddSpace(4f);
					composer.FontStrategy.DefaultTextFont = Fonts.Philosopher_18;
					composer.AddText(keyValuePair.Key, ComposerTextStyle.Gray, true);
					composer.FontStrategy.DefaultTextFont = Fonts.Philosopher_14;
					if (keyValuePair.Key == Local.unique)
					{
						composer.AddText(Local.shop_ingame_designs, ComposerTextStyle.Gray, true);
					}
					composer.AddSpace(4f);
					StackForm stackForm = composer.ComposeInStack(new Vector2?(new Vector2(4f, 0f)));
					Form form = new Form(new Marker(0f, 0f, 975f, stackForm.Pos.Height), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					};
					form.AddChild(stackForm);
					blocksStackFormControl.AddItem(new UiControl[]
					{
						form
					});
				}
				if (keyValuePair.Value.Size > 0 && keyValuePair.Value.First().Category == ShipDesignCategory.Satellite)
				{
					BlocksStackFormControl blocksStackFormControl2 = blocksStackFormControl;
					UiControl[] array = new UiControl[1];
					array[0] = {20881}.AddContentBlock({20881}.textSheet.AllToTop, 0f, 0f, 1f, 0.37f, new Rectangle(2447, 531, 237, 88), Local.PortRealShopPage_47, new string[]
					{
						Local.PortRealShopPage_48
					}, 0, delegate(RTI {21005})
					{
						new {20732}();
					}, delegate(Form {21006})
					{
					}, null, null);
					blocksStackFormControl2.AddItem(array);
				}
				num++;
				if (keyValuePair.Value.Size > 0 && keyValuePair.Value.First().Category == ShipDesignCategory.ShipFullDesign)
				{
					keyValuePair.Value.Sort({20881}.CurrentInstance);
					keyValuePair.Value.SortTop(delegate(ShipDesignInfo {21007})
					{
						if ({21007}.AssociatedShip != Global.Player.UsedShipPlayer.CraftFrom)
						{
							return (int){21007}.CostTier;
						}
						return 1000;
					});
				}
				else
				{
					keyValuePair.Value.Sort({20881}.CurrentInstance);
				}
				int j = 0;
				while (j < keyValuePair.Value.Size)
				{
					{20881}.<>c__DisplayClass34_0 CS$<>8__locals1 = new {20881}.<>c__DisplayClass34_0();
					CS$<>8__locals1.info = keyValuePair.Value.Array[j];
					string[] array2 = {20431}.SeparateNames(CS$<>8__locals1.info.Name);
					CS$<>8__locals1.name = CS$<>8__locals1.info.NameShort;
					CS$<>8__locals1.description = ((array2.Length > 1) ? array2[1] : string.Empty);
					CS$<>8__locals1.debugHidden = false;
					if (CS$<>8__locals1.info.InShop || !(keyValuePair.Key != Local.unique))
					{
						goto IL_727;
					}
					if ({20881}.debugViewMode && !string.IsNullOrEmpty(CS$<>8__locals1.info.Texture) && !CS$<>8__locals1.info.Name.Contains(Local.design_private_d) && !CS$<>8__locals1.info.Name.Contains(Local.design_private_flag) && !CS$<>8__locals1.info.Name.Contains(Local.design_private_guildflag) && !CS$<>8__locals1.info.Name.Contains(Local.design_private_guildsail) && !CS$<>8__locals1.info.Name.Contains(Local.design_private_ship))
					{
						CS$<>8__locals1.debugHidden = true;
						goto IL_727;
					}
					IL_8AD:
					j++;
					continue;
					IL_727:
					if ({20881}.debugViewMode)
					{
						{20881}.<>c__DisplayClass34_0 CS$<>8__locals2 = CS$<>8__locals1;
						string name = CS$<>8__locals1.name;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(3, 1);
						defaultInterpolatedStringHandler3.AppendLiteral(" ##");
						defaultInterpolatedStringHandler3.AppendFormatted<short>(CS$<>8__locals1.info.ID);
						CS$<>8__locals2.name = name + defaultInterpolatedStringHandler3.ToStringAndClear();
					}
					ValueTuple<RTI, RTI, GSI> designElementCost = DonationSystem.GetDesignElementCost(CS$<>8__locals1.info);
					CS$<>8__locals1.goldCost = designElementCost.Item1;
					CS$<>8__locals1.monetsCost = designElementCost.Item2;
					CS$<>8__locals1.resCost = designElementCost.Item3;
					bool flag = CS$<>8__locals1.info.Category == ShipDesignCategory.ShipFullDesign && CS$<>8__locals1.info.AssociatedShip != Global.Player.UsedShipPlayer.CraftFrom;
					CS$<>8__locals1.isDisallowed = (keyValuePair.Key == Local.unique);
					Form form2 = {20881}.AddContentBlock({20881}.textSheet.Default, 0f, 0f, 1f, 0.37f, new Rectangle(2209, 531, 237, 88), " ", null, 0, delegate(RTI {21020})
					{
						{20881}.StartFitting(CS$<>8__locals1.info);
					}, delegate(Form {21021})
					{
						bool flag2 = CS$<>8__locals1.name.Length > 19;
						Marker {25377} = new Marker(6f, 7f, 74f, 74f);
						if (CS$<>8__locals1.info.Category == ShipDesignCategory.Flag)
						{
							{25377} = new Marker(6f, 15f, 74f, 59f);
						}
						Image image = CS$<>8__locals1.info.DesignElementTextureIcon({25377});
						if (image != null)
						{
							{21021}.AddChild(image);
						}
						int num2 = 84;
						bool currentLanguageIsCjk = Fonts.CurrentLanguageIsCjk;
						int num3 = currentLanguageIsCjk ? -8 : 1;
						TextBlockControl textBlockControl = TextBlockBuilder.CreateBlock(130f, CS$<>8__locals1.name, CS$<>8__locals1.debugHidden ? Color.OrangeRed : (Color.White * 0.75f), flag2 ? Fonts.Arial_9 : Fonts.Arial_12, (float)num3).Create({21021}.Pos.XY + new Vector2((float)num2, 12f));
						{21021}.AddChild(textBlockControl);
						float num4 = (float)(currentLanguageIsCjk ? 45 : 40) - textBlockControl.PosHeight;
						if (CS$<>8__locals1.isDisallowed)
						{
							return;
						}
						if (CS$<>8__locals1.resCost != null && !CS$<>8__locals1.resCost.IsEmpty)
						{
							Vector2 {13342} = {21021}.Pos.XY + new Vector2((float)num2, 59f - num4);
							CustomSpriteFont arial_ = Fonts.Arial_12;
							Color {13344} = Color.Gold * 0.5f;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(1, 2);
							defaultInterpolatedStringHandler4.AppendFormatted<int>(CS$<>8__locals1.resCost.GetTotalItemsCount());
							defaultInterpolatedStringHandler4.AppendLiteral(" ");
							defaultInterpolatedStringHandler4.AppendFormatted(CS$<>8__locals1.resCost.ResourceInfo.First<GSILocalEnumerablePair<ResourceInfo>>().Info.Name);
							{21021}.AddChild(new Label({13342}, arial_, {13344}, defaultInterpolatedStringHandler4.ToStringAndClear(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
						}
						else if (CS$<>8__locals1.goldCost.Value > 0 || (CS$<>8__locals1.monetsCost.Value > 0 && DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems) == 1f))
						{
							Vector2 {13342}2 = {21021}.Pos.XY + new Vector2((float)num2, 59f - num4);
							CustomSpriteFont arial_2 = Fonts.Arial_12;
							Color {13344}2 = ((CS$<>8__locals1.goldCost.Value > 0) ? Color.Orange : Color.Gold) * 0.5f;
							string {13345};
							if (CS$<>8__locals1.goldCost.Value <= 0)
							{
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler5 = new DefaultInterpolatedStringHandler(1, 2);
								defaultInterpolatedStringHandler5.AppendFormatted<int>(DonationSystem.GetRoundedPriceWithDiscount(CS$<>8__locals1.monetsCost.Value, DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems)));
								defaultInterpolatedStringHandler5.AppendLiteral(" ");
								defaultInterpolatedStringHandler5.AppendFormatted(Local.monets);
								{13345} = defaultInterpolatedStringHandler5.ToStringAndClear();
							}
							else
							{
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler6 = new DefaultInterpolatedStringHandler(1, 2);
								defaultInterpolatedStringHandler6.AppendFormatted<RTI>(CS$<>8__locals1.goldCost);
								defaultInterpolatedStringHandler6.AppendLiteral(" ");
								defaultInterpolatedStringHandler6.AppendFormatted(Local.gold2);
								{13345} = defaultInterpolatedStringHandler6.ToStringAndClear();
							}
							{21021}.AddChild(new Label({13342}2, arial_2, {13344}2, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
						}
						else if (CS$<>8__locals1.monetsCost.Value > 0)
						{
							Vector2 {20962} = {21021}.Pos.XY + new Vector2((float)num2, 50f - num4);
							CustomSpriteFont arial_3 = Fonts.Arial_10;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler7 = new DefaultInterpolatedStringHandler(1, 2);
							defaultInterpolatedStringHandler7.AppendFormatted<int>(CS$<>8__locals1.monetsCost.Value);
							defaultInterpolatedStringHandler7.AppendLiteral(" ");
							defaultInterpolatedStringHandler7.AppendFormatted(Local.monets);
							string {20964} = defaultInterpolatedStringHandler7.ToStringAndClear();
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler8 = new DefaultInterpolatedStringHandler(1, 2);
							defaultInterpolatedStringHandler8.AppendFormatted<int>(DonationSystem.GetRoundedPriceWithDiscount(CS$<>8__locals1.monetsCost.Value, DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems)));
							defaultInterpolatedStringHandler8.AppendLiteral(" ");
							defaultInterpolatedStringHandler8.AppendFormatted(Local.monets);
							{20881}.DisplayPriceWithDiscount({21021}, {20962}, arial_3, {20964}, defaultInterpolatedStringHandler8.ToStringAndClear(), new Vector2(0f, -2.5f), ((CS$<>8__locals1.goldCost.Value > 0) ? Color.Orange : Color.Gold) * 0.5f);
						}
						string portRealShopPage_ = Local.PortRealShopPage_52;
						string {20979} = string.IsNullOrEmpty(CS$<>8__locals1.description) ? CS$<>8__locals1.info.GetAnnotation() : (CS$<>8__locals1.info.GetAnnotation() ?? "");
						float discount = DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems);
						Func<string> {20981};
						if (!string.IsNullOrEmpty(CS$<>8__locals1.description))
						{
							if (({20981} = CS$<>8__locals1.<>9__9) == null)
							{
								{20981} = (CS$<>8__locals1.<>9__9 = (() => CS$<>8__locals1.description));
							}
						}
						else
						{
							{20981} = null;
						}
						{21021}.ToolTip = {20881}.MakeGenericToolTip(portRealShopPage_, {20979}, discount, {20981});
					}, (CS$<>8__locals1.monetsCost.Value > 0 && !CS$<>8__locals1.isDisallowed) ? {20881}.multToStr(DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems)) : null, null);
					blocksStackFormControl.AddItem(new UiControl[]
					{
						form2
					});
					if (CS$<>8__locals1.isDisallowed)
					{
						form2.AllowMouseInput = false;
					}
					if (flag | CS$<>8__locals1.isDisallowed)
					{
						form2.Opacity = 0.55f;
						goto IL_8AD;
					}
					goto IL_8AD;
				}
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00084BEC File Offset: 0x00082DEC
		private static void MakePremiumPage(StackForm {20893})
		{
			{20893}.SortMode = UiOrientation.ExpansiveSize;
			Form form = {20881}.AddContentBlock({20881}.textSheet.DescriptionToTop, 0f, 0f, 4f, 0.5f, new Rectangle(1998, 1141, 967, 118), "", new string[0], 0, delegate(RTI {21008})
			{
			}, null, null, null);
			form.AddChild(new Label(form.Pos.XY + new Vector2(25f, 36f), Fonts.Philosopher_14Bold, new Color(170, 190, 224), Local.premiumInfo.Replace("$", Environment.NewLine), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new LiveLabel(form.Pos.XY + new Vector2(25f, 16f), Fonts.Philosopher_14Bold, Color.LightGreen, null, delegate(object {21009})
			{
				if (!Session.Account.IsPremium)
				{
					return "";
				}
				string str = "☆";
				string str2;
				if (!Session.Account.IsEndlessPremium)
				{
					LocalizedDateTime localizedDateTime = new LocalizedDateTime(Session.Account.PremiumEnds, false);
					localizedDateTime.UsePrefix = false;
					str2 = Local.PortRealShopPage_66(localizedDateTime.GetDate());
				}
				else
				{
					str2 = Local.Current("you_have_infinite_prem");
				}
				return str + str2;
			}, 1000));
			{20893}.AddItem(new UiControl[]
			{
				form
			});
			Tlist<Form> tlist = new Tlist<Form>();
			int num = 0;
			foreach (DonationSystem.PremiumOffer premiumOffer in DonationSystem.PremiumOffers)
			{
				Form form2 = {20881}.BigBlockHelperOld(premiumOffer, {20881}.c_block_3xH, (float)(num % 2 * 3), 0.8f + 0.66f * (float)(num - num % 2) / 2f, "", 0f, null, false, Session.Account.IsPremium ? Local.PortRealShop_premiumTt : Local.PortRealShopPage_Buy);
				tlist.Add(form2);
				form2.UpdateComplete += delegate(UiControl {21010})
				{
					if (Session.Account.IsEndlessPremium)
					{
						{21010}.Opacity = 0.2f;
						{21010}.AllowMouseInput = false;
					}
				};
				float num2 = premiumOffer.Discount() - (float)premiumOffer.DisplayOwnDiscount / 100f;
				if (num2 != 1f)
				{
					{20881}.AddDiscountString(tlist.Array[tlist.Size - 1], "-" + Math.Round((double)(100f - 100f * num2)).ToString() + "%", 0f, 0f);
				}
				num++;
			}
			for (int j = 0; j < tlist.Size; j++)
			{
				Form form3 = new Form(tlist.Array[j].Pos, (j != tlist.Size - 1) ? new Rectangle(2580, 983, 236, 52) : new Rectangle(2580, 929, 237, 53), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				tlist.Array[j].AddChild(form3);
				form3.MoveToBackLevel();
				{20893}.AddItem(new UiControl[]
				{
					tlist.Array[j]
				});
			}
			bool currentLanguageIsCjk = Fonts.CurrentLanguageIsCjk;
			int num3 = currentLanguageIsCjk ? 10 : -10;
			Vector2 value = currentLanguageIsCjk ? new Vector2(16f, 55f) : new Vector2(150f, 37f);
			Form form4 = {20881}.BigBlockHelper(DonationSystem.PremiumPagePackets[0], 0f, 2.8f, (float)num3, {20881}.RectangleSize.Size2x1, null, false, null, null, () => {20881}.GetTooltipTextDependsOn(Session.Account.PeaceTimeSec != 0.0));
			{20893}.AddItem(new UiControl[]
			{
				form4
			});
			Form form5 = new Form(form4.Pos, new Rectangle(2578, 724, 158, 76), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form4.AddChild(form5);
			form5.MoveToBackLevel();
			form4.AddChild(new LiveLabel(form4.Pos.XY + value, Fonts.Arial_12, Color.LightGreen, null, delegate(object {21011})
			{
				if (Session.Account.PeaceTimeSec != 0.0)
				{
					return StringHelper.TimeDHM(Session.Account.PeaceTimeSec, false);
				}
				return "  ";
			}, 300));
			if ({20881}.highlightIdPremiumPage == 1)
			{
				{20881}.MakeHighlighted(form4);
			}
			Form form6 = {20881}.BigBlockHelper(DonationSystem.PremiumPagePackets[1], 2f, 2.8f, (float)num3, {20881}.RectangleSize.Size2x1, null, false, null, null, () => {20881}.GetTooltipTextDependsOn(Session.Account.TradingSubscriptionSec != 0f));
			form5 = new Form(form6.Pos, new Rectangle(2580, 1036, 184, 92), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form6.AddChild(form5);
			form5.MoveToBackLevel();
			form6.AddChild(new LiveLabel(form6.Pos.XY + value, Fonts.Arial_12, Color.LightGreen, null, delegate(object {21012})
			{
				if (Session.Account.TradingSubscriptionSec != 0f)
				{
					return StringHelper.TimeDHM((double)Session.Account.TradingSubscriptionSec, false);
				}
				return "  ";
			}, 300));
			{20893}.AddItem(new UiControl[]
			{
				form6
			});
			if ({20881}.highlightIdPremiumPage == 2)
			{
				{20881}.MakeHighlighted(form6);
			}
			Form holdProtection = {20881}.BigBlockHelper(DonationSystem.PremiumPagePackets[2], 4f, 2.8f, (float)num3, {20881}.RectangleSize.Size2x1, null, false, null, null, () => {20881}.GetTooltipTextDependsOn(Session.Account.HoldProtectionSubscriptionSec != 0f));
			holdProtection.AddChild(new LiveLabel(holdProtection.Pos.XY + new Vector2(17f, 37f), Fonts.Philosopher_12, Color.SkyBlue, delegate()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 3);
				defaultInterpolatedStringHandler.AppendFormatted((holdProtection.InputMode == MouseInputMode.NoFocus) ? string.Empty : (Local.price + ": "));
				defaultInterpolatedStringHandler.AppendFormatted<int>(DonationSystem.HoldSubscriptionPriceScrolls.Value);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(Local.res_35_name_of);
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}, 16));
			holdProtection.AddChild(new LiveLabel(holdProtection.Pos.XY + value, Fonts.Arial_12, Color.LightGreen, null, delegate(object {21013})
			{
				if (Session.Account.HoldProtectionSubscriptionSec != 0f)
				{
					return StringHelper.TimeDHM((double)Session.Account.HoldProtectionSubscriptionSec, false);
				}
				return "  ";
			}, 300));
			Func<float> holdProtectionAvilAfter = () => Math.Max(Session.Account.HoldProtectionBlockBuySec, Session.Account.HoldProtectionSubscriptionSec);
			holdProtection.AddChild(new LiveLabel(holdProtection.Pos.XY + new Vector2(17f, 130f), Fonts.Arial_10, Color.Orange, null, delegate(object {21022})
			{
				if (holdProtectionAvilAfter() != 0f)
				{
					return Local.available_after(StringHelper.TimeDHM((double)holdProtectionAvilAfter(), false));
				}
				return "  ";
			}, 300));
			holdProtection.UpdateComplete += delegate(UiControl {21023})
			{
				{21023}.AllowMouseInput = (holdProtectionAvilAfter() == 0f);
			};
			{20893}.AddItem(new UiControl[]
			{
				holdProtection
			});
			{20893}.AddSpace(30f);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x000852AC File Offset: 0x000834AC
		private static string GetTooltipTextDependsOn(bool {20894})
		{
			if (!{20894})
			{
				return Local.PortRealShopPage_Buy;
			}
			return Local.PortRealShopPage_Extend;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x000852BC File Offset: 0x000834BC
		private static void ShopPremiumAction(DonationSystem.PremiumOffer {20895})
		{
			if ({20895}.GetFinalPrice().Value > Session.Account.Monets.Value)
			{
				{20881}.ShowBuyMonetsToolTip({20895}.GetFinalPrice().Value);
				return;
			}
			new {17312}(Local.PortRealShopPage_68({20895}.GetFinalPrice()), delegate()
			{
				PlayerAccount account = Session.Account;
				account.Monets.Value = account.Monets.Value - {20895}.GetFinalPrice().Value;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
				defaultInterpolatedStringHandler.AppendLiteral("prem_");
				defaultInterpolatedStringHandler.AppendFormatted<int>({20895}.Days.Value);
				Global.Shopstat(defaultInterpolatedStringHandler.ToStringAndClear(), {20895}.GetFinalPrice().Value);
				object obj;
				if ({20895}.Days.Value <= 1000)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(1, 2);
					defaultInterpolatedStringHandler2.AppendFormatted(Local.premium);
					defaultInterpolatedStringHandler2.AppendLiteral(" ");
					defaultInterpolatedStringHandler2.AppendFormatted<int>({20895}.Days.Value);
					obj = defaultInterpolatedStringHandler2.ToStringAndClear();
				}
				else
				{
					obj = Local.PortRealShopPage_62;
				}
				object obj2 = obj;
				new {19197}(Local.lbe_given4(obj2));
				{19994}.Logbook(Local.lbe_real_shop(obj2, Session.Account.Monets.Value), LBFlags.L2);
				Global.Network.Send(new OnPremiumAddMsg(OnPremiumAddMsg.Subscription.PremiumDays, {20895}.Days.Value, {20895}.GetFinalPrice().Value));
			}, null);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0008533C File Offset: 0x0008353C
		public static void ShowBuyMonetsToolTip(int {20896})
		{
			if (PlatformTuning.DisableShop)
			{
				return;
			}
			{20881}.queueToolTip = delegate()
			{
				{21395}.Open({20896} - Session.Account.Monets.Value);
			};
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00085370 File Offset: 0x00083570
		public static void StartFitting(ShipDesignInfo {20897})
		{
			if ({20881}.currentInstance == null)
			{
				return;
			}
			ValueTuple<RTI, RTI, GSI> designElementCost = DonationSystem.GetDesignElementCost({20897});
			RTI costGold = designElementCost.Item1;
			RTI costMonets = designElementCost.Item2;
			GSI costRes = designElementCost.Item3;
			string text;
			if (costRes.IsEmpty)
			{
				text = ((costGold.Value > 0) ? Local.PortRealShopPage_72b(costGold.Value) : Local.PortRealShopPage_72(DonationSystem.GetRoundedPriceWithDiscount(costMonets.Value, DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems))));
			}
			else
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
				defaultInterpolatedStringHandler.AppendLiteral("-");
				defaultInterpolatedStringHandler.AppendFormatted<int>(costRes.GetTotalItemsCount());
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(costRes.ResourceInfo.First<GSILocalEnumerablePair<ResourceInfo>>().Info.Name);
				text = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			string {20769} = text;
			{20755}.StartFitting({20897}, {20881}.currentInstance, new {20755}.BuyButton({20769}, true, delegate()
			{
				if ({20881}.BuyItem({20897}, costMonets, costGold, costRes) && {20897}.Category != ShipDesignCategory.Satellite && ({20897}.Category != ShipDesignCategory.ShipFullDesign || {20897}.AssociatedShip == Session.Account.Shipyard.CurrentRealShip.CraftFrom))
				{
					ShipDesignInfo shipDesignInfo = Session.Account.Shipyard.CurrentRealShip.SetDesignElement({20897}, (int){20897}.Category);
					if (shipDesignInfo != null)
					{
						Session.Account.DesingElementsAtStorage.AddOrRemove((int)shipDesignInfo.ID, 1);
					}
					Session.Account.DesingElementsAtStorage.AddOrRemove((int){20897}.ID, -1);
					Global.Game.ScenePort.UpdateGuiForViewShip();
				}
			}));
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0008549C File Offset: 0x0008369C
		private static bool BuyItem(ShipDesignInfo {20898}, RTI {20899}, RTI {20900}, GSI {20901})
		{
			if ({20901} != null && !{20901}.IsEmpty)
			{
				string name = {20901}.ResourceInfo.First<GSILocalEnumerablePair<ResourceInfo>>().Info.Name;
				if (!Session.Account.NearPortStorage.TryRemove({20901}))
				{
					new {17312}(Local.item_not_enough_storage(name));
					return false;
				}
				{19988} {20000} = {19988}.GiveDoubloons;
				object name2 = {20898}.Name;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted<int>({20901}.GetTotalItemsCount());
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(name);
				{19994}.MeAndLogbook({20000}, Local.lbe_real_shop_2(name2, defaultInterpolatedStringHandler.ToStringAndClear()), new LBFlags?(LBFlags.L2));
			}
			else if ({20900}.Value > 0)
			{
				if ({20900}.Value > Session.Account.Gold)
				{
					new {17312}(Local.gold_not_enough);
					return false;
				}
				Session.Account.Gold -= {20900}.Value;
				{19988} {20000}2 = {19988}.GiveDoubloons;
				object name3 = {20898}.Name;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler2.AppendFormatted<int>({20900}.Value);
				defaultInterpolatedStringHandler2.AppendLiteral(" ");
				defaultInterpolatedStringHandler2.AppendFormatted(Local.gold2);
				{19994}.MeAndLogbook({20000}2, Local.lbe_real_shop_2(name3, defaultInterpolatedStringHandler2.ToStringAndClear()), new LBFlags?(LBFlags.L2));
			}
			else
			{
				int roundedPriceWithDiscount = DonationSystem.GetRoundedPriceWithDiscount({20899}.Value, DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems));
				if (roundedPriceWithDiscount > Session.Account.Monets.Value)
				{
					{20881}.ShowBuyMonetsToolTip(roundedPriceWithDiscount);
					return false;
				}
				PlayerAccount account = Session.Account;
				account.Monets.Value = account.Monets.Value - roundedPriceWithDiscount;
				Global.Shopstat("design " + {20898}.Name, {20899}.Value);
				{19994}.MeAndLogbook({19988}.GiveDoubloons, Local.lbe_real_shop({20898}.Name, Session.Account.Monets.Value), new LBFlags?(LBFlags.L2));
			}
			Session.Account.DesingElementsAtStorage.AddOrRemove((int){20898}.ID, 1);
			return true;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0008567C File Offset: 0x0008387C
		private static Form AddContentBlock({20881}.textSheet {20902}, float {20903}, float {20904}, float {20905}, float {20906}, Rectangle {20907}, string {20908}, string[] {20909}, RTI {20910}, Action<RTI> {20911}, Action<Form> {20912} = null, string {20913} = null, string {20914} = null)
		{
			{20881}.<>c__DisplayClass43_0 CS$<>8__locals1 = new {20881}.<>c__DisplayClass43_0();
			CS$<>8__locals1.click = {20911};
			CS$<>8__locals1.cost = {20910};
			{20881}.<>c__DisplayClass43_0 CS$<>8__locals2 = CS$<>8__locals1;
			Vector2 vector = {20881}.getSize({20903}, {20904});
			Vector2 size = {20881}.getSize({20905}, {20906});
			CS$<>8__locals2.contentBlock = new Form(new Marker(ref vector, ref size), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			float num = CS$<>8__locals1.contentBlock.Pos.WH.X / (float){20907}.Width;
			Form contentBlock = CS$<>8__locals1.contentBlock;
			UiControl[] array = new UiControl[3];
			int num2 = 0;
			Marker pos = CS$<>8__locals1.contentBlock.Pos;
			array[num2] = new Form(new Marker(ref pos.XY, CS$<>8__locals1.contentBlock.Pos.WH.X, (float)((int)((float){20907}.Height * num))), {20907}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			array[1] = new Label(CS$<>8__locals1.contentBlock.Pos.XY + new Vector2(5f, 6f), Fonts.Philosopher_16, Color.Black, {20908}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			array[2] = new Label(CS$<>8__locals1.contentBlock.Pos.XY + new Vector2(5f, 5f), Fonts.Philosopher_16, Color.LightGray, {20908}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			contentBlock.AddChild(array);
			if (CS$<>8__locals1.cost.Value != 0)
			{
				CS$<>8__locals1.contentBlock.AddChild(new Label(CS$<>8__locals1.contentBlock.Pos.XY + new Vector2(5f, CS$<>8__locals1.contentBlock.Pos.WH.Y - 5f - 20f), Fonts.Philosopher_14Bold, Color.Gold * 0.66f, CS$<>8__locals1.cost.Value.ToString() + Local.CaptainSkillsInfoWindow_6, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			if (CS$<>8__locals1.click != null)
			{
				CS$<>8__locals1.contentBlock.EvClick += delegate(ClickUiEventArgs {21024})
				{
					CS$<>8__locals1.click(CS$<>8__locals1.cost);
				};
				if (CS$<>8__locals1.cost.Value != 0)
				{
					CS$<>8__locals1.contentBlock.ToolTip = {20881}.MakeGenericToolTip(Local.PortRealShopPage_Buy, {20914}, DonationSystem.GetDiscount(EActionCaterory.PDiscountOnShopItems), null);
				}
			}
			if ({20912} != null)
			{
				{20912}(CS$<>8__locals1.contentBlock);
			}
			if ({20909} != null && {20909}.Length != 0)
			{
				TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_10, 1f);
				foreach (string {13561} in {20909})
				{
					textBlockBuilder.WriteLine({13561}, Color.DarkGray);
				}
				TextBlockControl textBlockControl = textBlockBuilder.Create();
				UiControl uiControl = textBlockControl;
				vector = CS$<>8__locals1.contentBlock.Pos.XY + new Vector2(5f, ({20902} == {20881}.textSheet.AllToTop || {20902} == {20881}.textSheet.DescriptionToTop) ? 34f : (CS$<>8__locals1.contentBlock.Pos.WH.Y - 5f - textBlockControl.Pos.WH.Y - 25f));
				pos = textBlockControl.Pos;
				uiControl.Pos = new Marker(ref vector, ref pos.WH);
				CS$<>8__locals1.contentBlock.AddChild(textBlockControl);
			}
			CS$<>8__locals1.contentBlock.EvGotMouseFocus += delegate()
			{
				CS$<>8__locals1.contentBlock.Brightness = 1.4f;
			};
			CS$<>8__locals1.contentBlock.EvLostMouseFocus += delegate()
			{
				CS$<>8__locals1.contentBlock.Brightness = 1f;
			};
			if ({20913} != null)
			{
				{20881}.AddDiscountString(CS$<>8__locals1.contentBlock, {20913}, 0f, 0f);
			}
			if (CS$<>8__locals1.click != null)
			{
				Form form = new Form(CS$<>8__locals1.contentBlock.Pos, new Rectangle(3125, 1531, 315, 303), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					RenderToDepthMap = false
				};
				form.UpdateComplete += delegate(UiControl {21014})
				{
					{21014}.IsVisible = ({21014}.GetParent != null && {21014}.GetParent.InputMode == MouseInputMode.Focused);
				};
				CS$<>8__locals1.contentBlock.AddChild(form);
			}
			return CS$<>8__locals1.contentBlock;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00085A54 File Offset: 0x00083C54
		private static Vector2 getSize(float {20915}, float {20916})
		{
			return new Vector2((float)((int)(238f * {20915})), (float)((int)(238f * {20916})));
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00085A70 File Offset: 0x00083C70
		private static Form BigBlockHelper(DonationSystem.PacketOffer {20917}, float {20918}, float {20919}, float {20920}, {20881}.RectangleSize {20921}, Rectangle? {20922} = null, bool {20923} = false, string {20924} = null, string {20925} = null, Func<string> {20926} = null)
		{
			if ({20924} == null && {20926} == null)
			{
				{20924} = Local.PortRealShopPage_Buy;
			}
			{20925} = ({20917}.OverrideToolTipText ?? {20925});
			RTI finalPrice = {20917}.GetFinalPrice();
			Rectangle back = {20881}.GetRectForSize({20921});
			return {20881}.GenericBlockHelper(back, {20918}, {20919}, {20917}, delegate(Form {21025})
			{
				{21025}.AddChild(new Label({21025}.Pos.XY + new Vector2(16f, 11f), Fonts.Philosopher_18, new Color(236, 233, 234), {20917}.InnerText(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				if (finalPrice.Value != 0)
				{
					if ({20917}.Discount() != 1f)
					{
						{20881}.AddDiscountString({21025}, {20881}.multToStr({20917}.Discount()), 8f, 8f);
					}
					{21025}.AddChild(new LiveLabel({21025}.Pos.XY + new Vector2(17f, 37f), Fonts.Philosopher_12, new Color(238, 221, 108), delegate()
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 3);
						defaultInterpolatedStringHandler.AppendFormatted(({21025}.InputMode == MouseInputMode.NoFocus) ? string.Empty : (Local.price + ": "));
						defaultInterpolatedStringHandler.AppendFormatted<int>(finalPrice.Value);
						defaultInterpolatedStringHandler.AppendLiteral(" ");
						defaultInterpolatedStringHandler.AppendFormatted(Local.monets);
						return defaultInterpolatedStringHandler.ToStringAndClear();
					}, 16));
				}
				string[] array = {20917}.InnerText2().Split('$', StringSplitOptions.None);
				float num = 0f;
				foreach (string {13596} in array)
				{
					TextBlockBuilder textBlockBuilder = TextBlockBuilder.CreateBlockSpecial({21025}.Pos.WH.X - 30f, {13596}, new Color(164, 168, 170), Color.LightYellow, (back == {20881}.c_block_2x2) ? Fonts.Arial_12 : Fonts.Arial_10, Fonts.Philosopher_14Bold, 0);
					{21025}.AddChild(textBlockBuilder.Create({21025}.Pos.XY + new Vector2(17f, 70f + {20920} + num)));
					num += textBlockBuilder.Size.Y;
				}
				if ({20922} != null)
				{
					{21025}.AddChild(new Form({21025}.Pos.XY + new Vector2({21025}.Pos.WH.X - (float){20922}.Value.Width, 20f), {20922}.Value, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					});
				}
			}, {20924}, {20925}, {20923}, {20926});
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00085B00 File Offset: 0x00083D00
		private static Form BigBlockHelperOld(DonationSystem.PremiumOffer {20927}, Rectangle {20928}, float {20929}, float {20930}, string {20931}, float {20932}, Rectangle? {20933} = null, bool {20934} = false, string {20935} = null)
		{
			bool infinitePrem = {20927}.Days.Value > 3600;
			string name = infinitePrem ? (Local.PortRealShopPage_62 ?? "") : (({20927}.Days.Value <= 4) ? Local.PortRealShopPage_57B({20927}.Days) : Local.PortRealShopPage_57C({20927}.Days));
			{20927}.Apply = delegate(DonationSystem.Offer {21015})
			{
				{20881}.ShopPremiumAction((DonationSystem.PremiumOffer){21015});
			};
			return {20881}.GenericBlockHelper({20928}, {20929}, {20930}, {20927}, delegate(Form {21026})
			{
				bool flag = infinitePrem;
				if (flag)
				{
					Locale id = LocaleInfo.Current.Id;
					bool flag2 = id == Locale.De || id == Locale.Pl;
					flag = flag2;
				}
				CustomSpriteFont {13343} = flag ? Fonts.Philosopher_16 : Fonts.Philosopher_18;
				Vector2 vector = new Vector2(16f, 11f);
				{21026}.AddChild(new Label({21026}.Pos.XY + vector, {13343}, new Color(236, 233, 234), name, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				vector += new Vector2(0f, 26f);
				if (infinitePrem)
				{
					{21026}.AddChild(new Label({21026}.Pos.XY + vector, {13343}, new Color(236, 233, 234), Local.timporary, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					vector += new Vector2(0f, 26f);
				}
				int value = {20927}.GetFinalPrice().Value;
				if ({20927}.DisplayOwnDiscount == 0)
				{
					Vector2 {13342} = {21026}.Pos.XY + vector;
					CustomSpriteFont philosopher_ = Fonts.Philosopher_12;
					Color {13344} = new Color(238, 221, 108);
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
					defaultInterpolatedStringHandler.AppendFormatted<int>(value);
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted(Local.monets);
					{21026}.AddChild(new Label({13342}, philosopher_, {13344}, defaultInterpolatedStringHandler.ToStringAndClear(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				}
				else
				{
					Vector2 {20951} = {21026}.Pos.XY + vector;
					CustomSpriteFont philosopher_2 = Fonts.Philosopher_12;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(1, 2);
					defaultInterpolatedStringHandler2.AppendFormatted<int>((int)Math.Ceiling((double)((float)value / (1f - (float){20927}.DisplayOwnDiscount / 100f))));
					defaultInterpolatedStringHandler2.AppendLiteral(" ");
					defaultInterpolatedStringHandler2.AppendFormatted(Local.monets);
					string {20953} = defaultInterpolatedStringHandler2.ToStringAndClear();
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(1, 2);
					defaultInterpolatedStringHandler3.AppendFormatted<int>(value);
					defaultInterpolatedStringHandler3.AppendLiteral(" ");
					defaultInterpolatedStringHandler3.AppendFormatted(Local.monets);
					{20881}.DisplayPriceWithDiscount({21026}, {20951}, philosopher_2, {20953}, defaultInterpolatedStringHandler3.ToStringAndClear());
				}
				TextBlockBuilder textBlockBuilder = TextBlockBuilder.CreateBlockSpecial({21026}.Pos.WH.X - 15f, {20931}.Replace("$", Environment.NewLine), new Color(164, 168, 170), Color.LightYellow, ({20928} == {20881}.c_block_2x2) ? Fonts.Arial_12 : Fonts.Arial_10, Fonts.Philosopher_14Bold, 0);
				{21026}.AddChild(textBlockBuilder.Create({21026}.Pos.XY + new Vector2(17f, 70f + {20932})));
				if ({20933} != null)
				{
					{21026}.AddChild(new Form({21026}.Pos.XY + new Vector2({21026}.Pos.WH.X - (float){20933}.Value.Width, 20f), {20933}.Value, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					});
				}
			}, {20935}, null, {20934}, null);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00085C08 File Offset: 0x00083E08
		private static Form LineWithiconHelper(DonationSystem.PacketOffer {20936}, float {20937}, float {20938}, Rectangle {20939}, {20881}.RectangleSize {20940}, bool {20941} = false, string {20942} = null, bool {20943} = false, float {20944} = 375f)
		{
			if ({20942} == null)
			{
				{20942} = Local.PortRealShopPage_Buy;
			}
			RTI finalPrice = {20936}.GetFinalPrice();
			return {20881}.GenericBlockHelper({20881}.GetRectForSize({20940}), {20937}, {20938}, {20936}, delegate(Form {21027})
			{
				{21027}.AddChild(new Form({21027}.Pos.XY + new Vector2(15f, 15f), {20939}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				});
				TextBlockBuilder textBlockBuilder = ({20936}.InnerText().Length > 65) ? TextBlockBuilder.CreateBlockSpecial({20944}, {20936}.InnerText(), new Color(187, 174, 135), Color.LightYellow, Fonts.Philosopher_14, Fonts.Philosopher_14Bold, 0) : TextBlockBuilder.CreateBlock({20944}, {20936}.InnerText(), new Color(187, 174, 135), Fonts.Philosopher_14, 1f);
				{21027}.AddChild(textBlockBuilder.Create({21027}.Pos.XY + new Vector2(84f, 13f)));
				if (finalPrice.Value != 0)
				{
					if ({20936}.Discount() != 1f)
					{
						Form x = {21027};
						Vector2 {20956} = {21027}.Pos.XY + new Vector2(85f, 13f + textBlockBuilder.Size.Y - 1f);
						CustomSpriteFont philosopher_ = Fonts.Philosopher_12;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
						defaultInterpolatedStringHandler.AppendFormatted<int>({20936}.Price.Value);
						defaultInterpolatedStringHandler.AppendLiteral(" ");
						defaultInterpolatedStringHandler.AppendFormatted(Local.monets);
						string {20958} = defaultInterpolatedStringHandler.ToStringAndClear();
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(1, 2);
						defaultInterpolatedStringHandler2.AppendFormatted<int>(finalPrice.Value);
						defaultInterpolatedStringHandler2.AppendLiteral(" ");
						defaultInterpolatedStringHandler2.AppendFormatted(Local.monets);
						{20881}.DisplayPriceWithDiscount(x, {20956}, philosopher_, {20958}, defaultInterpolatedStringHandler2.ToStringAndClear(), {20943} ? new Vector2(0f, -2.5f) : new Vector2(4f, 0f));
					}
					else
					{
						{21027}.AddChild(new LiveLabel({21027}.Pos.XY + new Vector2(85f, 13f + textBlockBuilder.Size.Y - 1f), Fonts.Philosopher_12, new Color(238, 221, 108), delegate()
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(1, 3);
							defaultInterpolatedStringHandler3.AppendFormatted(({21027}.InputMode == MouseInputMode.NoFocus) ? string.Empty : (Local.price + ": "));
							defaultInterpolatedStringHandler3.AppendFormatted<int>(finalPrice.Value);
							defaultInterpolatedStringHandler3.AppendLiteral(" ");
							defaultInterpolatedStringHandler3.AppendFormatted(Local.monets);
							return defaultInterpolatedStringHandler3.ToStringAndClear();
						}, 16));
					}
				}
				string text = {20881}.multToStr({20936}.Discount());
				if (text != null)
				{
					{20881}.AddDiscountString({21027}, text, 0f, 0f);
				}
			}, {20942}, null, {20941}, null);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00085C7C File Offset: 0x00083E7C
		private static Rectangle GetRectForSize({20881}.RectangleSize {20945})
		{
			Rectangle result = {20881}.c_block_2x2;
			switch ({20945})
			{
			case {20881}.RectangleSize.Size2x1:
				result = {20881}.c_block_2x1;
				break;
			case {20881}.RectangleSize.SizeHxM:
				result = {20881}.c_block_HxM;
				break;
			case {20881}.RectangleSize.Size3xH:
				result = {20881}.c_block_3xH;
				break;
			case {20881}.RectangleSize.BlockInside:
				result = {20881}.c_blockInside;
				break;
			}
			return result;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00085CC8 File Offset: 0x00083EC8
		private static void AddDiscountString(Form {20946}, string {20947}, float {20948} = 0f, float {20949} = 0f)
		{
			Vector2 value = {20946}.Pos.XY + {20946}.Pos.WH - {20881}.c_discountMarker.WidthHeight() + new Vector2({20948}, {20949});
			{20946}.AddChild({20881}.c_discountMarker, new Marker(ref value, ref {20881}.c_discountMarker));
			{20946}.AddChild(new Label(value + new Vector2(48f, 48f), Fonts.Philosopher_14Bold, Color.Pink, {20947}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00085D56 File Offset: 0x00083F56
		private static void DisplayPriceWithDiscount(Form {20950}, Vector2 {20951}, CustomSpriteFont {20952}, string {20953}, string {20954})
		{
			{20881}.DisplayPriceWithDiscount({20950}, {20951}, {20952}, {20953}, {20954}, new Vector2(4f, 0f));
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00085D74 File Offset: 0x00083F74
		private static void DisplayPriceWithDiscount(Form {20955}, Vector2 {20956}, CustomSpriteFont {20957}, string {20958}, string {20959}, Vector2 {20960})
		{
			Color {20967} = new Color(238, 221, 108);
			{20881}.DisplayPriceWithDiscount({20955}, {20956}, {20957}, {20958}, {20959}, {20960}, {20967});
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00085DA4 File Offset: 0x00083FA4
		private static void DisplayPriceWithDiscount(Form {20961}, Vector2 {20962}, CustomSpriteFont {20963}, string {20964}, string {20965}, Vector2 {20966}, Color {20967})
		{
			Label label = new Label({20962}, {20963}, Color.Gray, {20964}.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			label.SetStrikethroughDecoration(AtlasPortGui.whitePixel, AtlasPortGui.Texture.Tex);
			{20961}.AddChild(label);
			{20961}.AddChild(new Label({20962} + new Vector2(({20966}.X != 0f) ? (label.Size.X + {20966}.X) : 0f, ({20966}.Y != 0f) ? (label.Size.Y + {20966}.Y) : 0f) + {20966}, {20963}, {20967}, {20965}.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00085E5C File Offset: 0x0008405C
		private static Form GenericBlockHelper(Rectangle {20968}, float {20969}, float {20970}, DonationSystem.Offer {20971}, Action<Form> {20972}, string {20973} = null, string {20974} = null, bool {20975} = false, Func<string> {20976} = null)
		{
			int num = 159;
			Form form = new Form(new Marker({20969} * (float)num, {20970} * (float)num, ref {20968}), {20968}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			if ({20972} != null)
			{
				{20972}(form);
			}
			if ({20971}.Apply != null)
			{
				AnimatedButton wb = new AnimatedButton(form.Pos, Rectangle.Empty, Rectangle.Empty, new Rectangle(3125, 1531, 315, 303), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form.AddChild(wb);
				form.ToolTip = {20881}.MakeGenericToolTip({20973}, {20974}, {20971}.Discount(), {20976});
				Action<ClickUiEventArgs> <>9__1;
				Action <>9__3;
				wb.EvClick += delegate(ClickUiEventArgs {21029})
				{
					if ({20971}.GetFinalPrice().Value > Session.Account.Monets.Value && {20971} != DonationSystem.PremiumPagePackets[2])
					{
						{20971}.Click();
						return;
					}
					if ({20881}.currentBackForm != null && {20971} == DonationSystem.PremiumPagePackets[2])
					{
						new UiOpacityAnimation({20881}.currentBackForm, 1f, 0f, 200f);
						new UiRemoveAction({20881}.currentBackForm);
						return;
					}
					{20881}.currentBackForm = new Form({20881}.currentInstance.Pos.SetWidth((float){20856}.contentWidth), AtlasPortGui.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						BasicColor = new Color(5, 5, 5) * 0.5f,
						AnimatedFocus = false
					};
					new UiOpacityAnimation({20881}.currentBackForm, 0f, 1f, 200f);
					Form form;
					StackForm stackForm = form.GetParent as StackForm;
					if (stackForm != null)
					{
						{20881}.currentBackForm.Pos = {20881}.currentBackForm.Pos.SetXY(0f, 0f);
						stackForm.AddItem(new UiControl[]
						{
							{20881}.currentBackForm
						});
						form = {20881}.currentBackForm;
						Button {12628} = new Button(new Vector2(form.Pos.Center.X - (float)({20881}.c_buyButton.Width / 2), form.Pos.End.Y), {20881}.c_buyButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.shop, Fonts.Philosopher_14, Color.White * 0.9f, false);
						Action<ClickUiEventArgs> {12629};
						if (({12629} = <>9__1) == null)
						{
							{12629} = (<>9__1 = delegate(ClickUiEventArgs {21028})
							{
								{20971}.Click();
								if ({20971}.GetFinalPrice().Value > 250 || {20971} == DonationSystem.PremiumPagePackets[2])
								{
									{20881}.currentBackForm.ImitateClick(true);
								}
							});
						}
						form.AddChild({12628}.ExClick({12629}));
						{20881}.currentBackForm.EvClickEmptiness += delegate(ClickUiEventArgs {21016})
						{
							new UiOpacityAnimation({20881}.currentBackForm, 1f, 0f, 200f);
							new UiRemoveAction({20881}.currentBackForm);
						};
						UiControl uiControl = {20881}.currentBackForm;
						Action {12894};
						if (({12894} = <>9__3) == null)
						{
							{12894} = (<>9__3 = delegate()
							{
								form.AllowMouseInput = true;
								{20881}.currentBackForm = null;
								wb.FirstTexturePath = Rectangle.Empty;
							});
						}
						uiControl.EvRemoveFromContainer += {12894};
						form.MoveToFrontLevel();
						form.AllowMouseInput = false;
						wb.FirstTexturePath = wb.FocusedTexturePath;
						return;
					}
					throw new NotSupportedException();
				};
			}
			if ({20975})
			{
				{20881}.MakeHighlighted(form);
			}
			return form;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00085F80 File Offset: 0x00084180
		public static void TryCloseCurrentBackForm()
		{
			if ({20881}.currentBackForm == null || {20881}.currentBackForm.AnimationsCount > 0)
			{
				return;
			}
			new UiOpacityAnimation({20881}.currentBackForm, 1f, 0f, 1000f);
			new UiRemoveAction({20881}.currentBackForm);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00085FBC File Offset: 0x000841BC
		private static void MakeHighlighted(Form {20977})
		{
			{20977}.Brightness = 10f;
			new UiBlinkingAnimation({20977}, 1f, 0.8f);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00085FDC File Offset: 0x000841DC
		private static ToolTip MakeGenericToolTip(string {20978}, string {20979}, float {20980}, Func<string> {20981} = null)
		{
			if ({20980} < 1f)
			{
				{20979} = Local.PortRealShop_discountTt + ". " + {20979};
			}
			return new ToolTip(delegate(UiControl {21030})
			{
				Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
				if ({20981} != null)
				{
					Tlist<ToolTipCharacteristics> tlist2 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics({20981}(), CharacteristicsColor.LimeBold);
					tlist2.Add(toolTipCharacteristics);
				}
				if (!string.IsNullOrEmpty({20978}))
				{
					Tlist<ToolTipCharacteristics> tlist3 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics({20978}, ({20978} == Local.PortRealShopPage_52) ? CharacteristicsColor.WheatBold : CharacteristicsColor.LimeBold);
					tlist3.Add(toolTipCharacteristics);
				}
				return new ToolTipState("", {20979}, tlist.ToArray());
			});
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00086038 File Offset: 0x00084238
		protected override void UserUpdate(ref FrameTime {20982})
		{
			base.UserUpdate(ref {20982});
			Action action = {20881}.queueToolTip;
			if (action != null)
			{
				action();
			}
			{20881}.queueToolTip = null;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00038278 File Offset: 0x00036478
		protected override void UserBackRender()
		{
			base.UserBackRender();
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00038280 File Offset: 0x00036480
		protected override void UserFrontRender()
		{
			base.UserFrontRender();
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00086058 File Offset: 0x00084258
		int IComparer<ShipDesignInfo>.{20983}(ShipDesignInfo {20984}, ShipDesignInfo {20985})
		{
			string name = {20984}.Name;
			string name2 = {20985}.Name;
			if ({20984}.Category == ShipDesignCategory.ShipFullDesign && {20985}.Category == ShipDesignCategory.ShipFullDesign)
			{
				return name2.CompareTo(name);
			}
			if ({20984}.Category == ShipDesignCategory.BowFigure && {20985}.Category == ShipDesignCategory.BowFigure)
			{
				return {20984}.InShopByGold.Value.CompareTo({20985}.InShopByGold.Value);
			}
			if (name.IndexOf(' ') != -1 && name2.IndexOf(' ') != -1)
			{
				string[] array = name.Split(' ', StringSplitOptions.None);
				string[] array2 = name2.Split(' ', StringSplitOptions.None);
				int num;
				int value;
				if (array[0] == array2[0] && int.TryParse(array[1], out num) && int.TryParse(array2[1], out value))
				{
					return num.CompareTo(value);
				}
			}
			return name.CompareTo(name2);
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x000030FD File Offset: 0x000012FD
		bool IPortPage.CreateChatUi
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x000030FD File Offset: 0x000012FD
		bool IPortPage.CreateShipStatUi
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x000862D7 File Offset: 0x000844D7
		[CompilerGenerated]
		private void {20986}(CheckboxCheckedEventArgs {20987})
		{
			{20881}.debugViewMode = {20987}.NewValue;
			base.Refresh();
		}

		// Token: 0x04000E5D RID: 3677
		private static readonly RTI placeInPortCost_r = new RTI(75);

		// Token: 0x04000E5E RID: 3678
		private static Button backExaplePreviewButton;

		// Token: 0x04000E5F RID: 3679
		private static Button backExaplePreviewButton_shop;

		// Token: 0x04000E60 RID: 3680
		private static Form currentBackForm;

		// Token: 0x04000E61 RID: 3681
		private static readonly Rectangle c_button_anim1 = new Rectangle(452, 205, 182, 38);

		// Token: 0x04000E62 RID: 3682
		private static readonly Rectangle c_button_anim2 = new Rectangle(452, 244, 182, 38);

		// Token: 0x04000E63 RID: 3683
		private static readonly Rectangle c_button_focus = new Rectangle(452, 283, 182, 38);

		// Token: 0x04000E64 RID: 3684
		private static readonly Rectangle c_block_2x2 = new Rectangle(2023, 628, 316, 316);

		// Token: 0x04000E65 RID: 3685
		private static readonly Rectangle c_block_2x1 = new Rectangle(2023, 945, 316, 158);

		// Token: 0x04000E66 RID: 3686
		private static readonly Rectangle c_block_HxM = new Rectangle(2340, 929, 237, 210);

		// Token: 0x04000E67 RID: 3687
		private static readonly Rectangle c_block_3xH = new Rectangle(2340, 822, 474, 106);

		// Token: 0x04000E68 RID: 3688
		private static readonly Rectangle c_blockInside = new Rectangle(2253, 1368, 237, 106);

		// Token: 0x04000E69 RID: 3689
		private static readonly Rectangle c_anchOrange = new Rectangle(2566, 626, 74, 96);

		// Token: 0x04000E6A RID: 3690
		private static readonly Rectangle c_anchGold = new Rectangle(2341, 628, 74, 96);

		// Token: 0x04000E6B RID: 3691
		private static readonly Rectangle c_anchViolet = new Rectangle(2416, 628, 74, 96);

		// Token: 0x04000E6C RID: 3692
		private static readonly Rectangle c_anchSilver = new Rectangle(2491, 628, 74, 96);

		// Token: 0x04000E6D RID: 3693
		private static readonly Rectangle c_anchBlack = new Rectangle(2341, 725, 74, 96);

		// Token: 0x04000E6E RID: 3694
		private static readonly Rectangle c_discountMarker = new Rectangle(2416, 725, 79, 76);

		// Token: 0x04000E6F RID: 3695
		public static readonly Rectangle c_buyButton = new Rectangle(2644, 684, 161, 39);

		// Token: 0x04000E70 RID: 3696
		private static {20881} currentInstance;

		// Token: 0x04000E71 RID: 3697
		private static int highlightIdPremiumPage = -1;

		// Token: 0x04000E72 RID: 3698
		private static Action queueToolTip;

		// Token: 0x04000E73 RID: 3699
		private static bool debugViewMode;

		// Token: 0x04000E74 RID: 3700
		private const int sizePage = 954;

		// Token: 0x020002CA RID: 714
		private class ShopCompletedui : CustomUi
		{
			// Token: 0x06000FD1 RID: 4049 RVA: 0x000862EA File Offset: 0x000844EA
			public ShopCompletedui() : base(false)
			{
			}

			// Token: 0x06000FD2 RID: 4050 RVA: 0x000862F3 File Offset: 0x000844F3
			protected override void UserUpdate(ref FrameTime {20988})
			{
				this.{20989} += {20988}.secElapsed;
				if (this.{20989} > 3f)
				{
					base.RemoveFromContainer();
				}
			}

			// Token: 0x06000FD3 RID: 4051 RVA: 0x0008631C File Offset: 0x0008451C
			protected override void UserBackRender()
			{
				Device gs = Engine.GS;
				Vector2 vector = Engine.GS.MouseToUI + new Vector2(36f, -40f);
				Color color = Color.White * Math.Min(1f, 2f * (this.{20989} * (1f - this.{20989} / 3f) * 1.333f));
				gs.Draw({20881}.ShopCompletedui.c_path, vector, color);
			}

			// Token: 0x06000FD4 RID: 4052 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserFrontRender()
			{
			}

			// Token: 0x04000E75 RID: 3701
			private static readonly Rectangle c_path = new Rectangle(2161, 571, 47, 48);

			// Token: 0x04000E76 RID: 3702
			private float {20989};
		}

		// Token: 0x020002CB RID: 715
		private enum textSheet
		{
			// Token: 0x04000E78 RID: 3704
			Default,
			// Token: 0x04000E79 RID: 3705
			AllToTop,
			// Token: 0x04000E7A RID: 3706
			DescriptionToTop
		}

		// Token: 0x020002CC RID: 716
		public enum RectangleSize
		{
			// Token: 0x04000E7C RID: 3708
			Size2x2,
			// Token: 0x04000E7D RID: 3709
			Size2x1,
			// Token: 0x04000E7E RID: 3710
			SizeHxM,
			// Token: 0x04000E7F RID: 3711
			Size3xH,
			// Token: 0x04000E80 RID: 3712
			BlockInside
		}

		// Token: 0x020002CD RID: 717
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000E81 RID: 3713
			public static Action<StackForm> <0>__MakePremiumPage;
		}
	}
}
