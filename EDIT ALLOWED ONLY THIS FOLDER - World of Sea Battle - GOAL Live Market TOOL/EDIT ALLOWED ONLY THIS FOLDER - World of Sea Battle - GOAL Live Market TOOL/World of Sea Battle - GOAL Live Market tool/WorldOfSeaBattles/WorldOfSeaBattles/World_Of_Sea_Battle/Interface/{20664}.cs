using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002A7 RID: 679
	internal sealed class {20664} : {17625}
	{
		// Token: 0x06000EEE RID: 3822 RVA: 0x0007D8D8 File Offset: 0x0007BAD8
		private static {17625}.DynamicTittle[] GetTitles()
		{
			if (!Global.Player.NearPort.AllowCapture)
			{
				return new {17625}.DynamicTittle[]
				{
					new {17625}.DynamicTittle(Local.port_guild_hall)
				};
			}
			return new {17625}.DynamicTittle[]
			{
				new {17625}.DynamicTittle(Local.port_guild_hall),
				new {17625}.DynamicTittle(Local.supply_rating)
			};
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0007D938 File Offset: 0x0007BB38
		public {20664}() : base(820f, {17625}.c_back1, {17604}.InGameWindow, {17625}.c_icShield, {20664}.GetTitles())
		{
			{20664}.numFormatInfo.NumberGroupSeparator = " ";
			{20664}.numFormatInfo.NumberDecimalDigits = 0;
			{20664}.Instance = this;
			base.EvRemoveFromContainer += delegate()
			{
				GameScene.DecreaseGameInput();
				{20664}.Instance = null;
			};
			this.{20702} = Session.Game.NearPortStatus.Dev;
			base.ComposeTabWithScroll(null, true, true, new Action<ListItemViewControl>[]
			{
				new Action<ListItemViewControl>(this.{20666}),
				new Action<ListItemViewControl>(this.{20677})
			});
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0007D9EE File Offset: 0x0007BBEE
		public void Update()
		{
			{20664}.Instance.ForceItemSelected(this.selectedTitle);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0007DA00 File Offset: 0x0007BC00
		public void UpdateSupplyInfo(ScoreDictionary<string> {20665})
		{
			this.{20703} = {20665};
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0007DA0C File Offset: 0x0007BC0C
		private void {20666}(ListItemViewControl {20667})
		{
			{20664}.<>c__DisplayClass9_0 CS$<>8__locals1 = new {20664}.<>c__DisplayClass9_0();
			CS$<>8__locals1.<>4__this = this;
			float progressToNextLevel = this.{20702}.ProgressToNextLevel;
			int portLevel = this.{20702}.PortLevel;
			CS$<>8__locals1.maxLevel = this.{20702}.MaxLevel;
			{20667}.AddItem(new UiControl[]
			{
				CS$<>8__locals1.<TownHallMainTab>g__MakeLevelsProgressBar|10(portLevel, progressToNextLevel)
			});
			{20664}.<>c__DisplayClass9_0 CS$<>8__locals2 = CS$<>8__locals1;
			Decorator game = Session.Game;
			CS$<>8__locals2.port = game.NearPortStatus;
			CS$<>8__locals1.possible = Session.Game.GetPortBonusCriterias(CS$<>8__locals1.port);
			CS$<>8__locals1.bonuses = new Tlist<{20664}.BonusStatus>(from {20721} in Gameplay.PortLevelBonuses.Values.SelectMany((ObservableTlist<PortLevelBonus> {20711}) => {20711})
			where {20721}.Type != PortLevelBonusType.ResourceProduction || CS$<>8__locals1.port.PortInstance.PbsManufacture != null
			select {20721} into {20722}
			where !CS$<>8__locals1.port.PortInstance.IsArabPort || {20722}.Criteria.HasFlag(PortBonusCriteria.TraderOwnerAndTrusted)
			select {20722} into {20723}
			where CS$<>8__locals1.port.PortInstance.Type != PortType.PirateBay || {20723}.Criteria.IsPirateCategory()
			select {20723} into {20724}
			select new {20664}.BonusStatus({20724}, CS$<>8__locals1.port.Dev, CS$<>8__locals1.possible));
			Form form = new Form(Vector2.Zero, new Rectangle(2495, 861, 374, 56), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Label label = new Label(Vector2.Zero, Fonts.Philosopher_16, Color.Wheat, CS$<>8__locals1.port.CapturerFraction.GetName() + " " + (CS$<>8__locals1.port.IsCapturedByGuild ? ("[" + CS$<>8__locals1.port.CapturedBy + "]") : ""), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos(label, PositionAlignment.LeftUp, PositionAlignment.Center, 20f, 2f, false);
			label.Pos = label.Pos.Offset(0f, 2f);
			base.AddChildPos(form, PositionAlignment.RightDown, PositionAlignment.LeftUp, 15f);
			Rectangle {20670} = new Rectangle(2338, 1902, 32, 32);
			Rectangle {20670}2 = new Rectangle(2371, 1902, 32, 32);
			Rectangle {20670}3 = new Rectangle(2404, 1902, 32, 32);
			CS$<>8__locals1.stack = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.stack.AddItem(new UiControl[]
			{
				this.{20668}(Local.bonuses_for_everyone, {20670}, 1)
			});
			CS$<>8__locals1.stack.AddSpace(4f);
			foreach ({20664}.BonusStatus bonusStatus in ((IEnumerable<{20664}.BonusStatus>)CS$<>8__locals1.bonuses))
			{
				if ((bonusStatus.Bonus.Criteria.HasFlag(PortBonusCriteria.All) && Session.Game.GetPortBonusMaxLevel(PortBonusCriteria.All, CS$<>8__locals1.port) > 0) || (bonusStatus.Bonus.Criteria.HasFlag(PortBonusCriteria.AllWhenEmpire) && Session.Game.GetPortBonusMaxLevel(PortBonusCriteria.AllWhenEmpire, CS$<>8__locals1.port) > 0) || (bonusStatus.Bonus.Criteria.HasFlag(PortBonusCriteria.AllWhenPirateOwner) && Session.Game.GetPortBonusMaxLevel(PortBonusCriteria.AllWhenPirateOwner, CS$<>8__locals1.port) > 0))
				{
					CS$<>8__locals1.stack.AddSpace(1f);
					CS$<>8__locals1.stack.AddItem(new UiControl[]
					{
						this.{20672}(bonusStatus)
					});
				}
			}
			CS$<>8__locals1.stack.AddSpace(4f);
			CS$<>8__locals1.stack.AddItem(new UiControl[]
			{
				this.{20668}(Local.available_bonuses, {20670}2, 0)
			});
			CS$<>8__locals1.<TownHallMainTab>g__AddItems|5(({20664}.BonusStatus {20712}) => {20712}.UiCategory == 0, 1f);
			game = Session.Game;
			if (game.CanOpenTradeHqInNearPort)
			{
				PortDevelopment portDevelopment = this.{20702};
				PortLevelBonusType {7912} = PortLevelBonusType.TradersCantOpenHq;
				game = Session.Game;
				if (portDevelopment.GetAvailableBonus({7912}, game, Session.Game.NearPortStatus, null) == 0f)
				{
					{20667}.AddItem(new UiControl[]
					{
						this.{20674}()
					});
				}
			}
			CS$<>8__locals1.stack.AddSpace(4f);
			CS$<>8__locals1.stack.AddItem(new UiControl[]
			{
				this.{20668}(Local.possible_bonuses, {20670}2, 2)
			});
			CS$<>8__locals1.<TownHallMainTab>g__AddItems|5(({20664}.BonusStatus {20713}) => {20713}.UiCategory == 1, 1f);
			CS$<>8__locals1.bonuses.SortTop(delegate({20664}.BonusStatus {20714})
			{
				if ({20714}.Bonus.Criteria.IsAllCategory())
				{
					return 10;
				}
				if ({20714}.Bonus.Criteria.IsNationCategory())
				{
					return 9;
				}
				if ({20714}.Bonus.Criteria.IsTraderCategory())
				{
					return 8;
				}
				if (!{20714}.Bonus.Criteria.IsPirateCategory())
				{
					return 6;
				}
				return 7;
			});
			CS$<>8__locals1.stack.AddSpace(4f);
			CS$<>8__locals1.stack.AddItem(new UiControl[]
			{
				this.{20668}(Local.unavailable_bonuses, {20670}3, 2)
			});
			CS$<>8__locals1.<TownHallMainTab>g__AddItems|5(({20664}.BonusStatus {20715}) => {20715}.UiCategory == 2, 0.66f);
			{20667}.AddItem(new UiControl[]
			{
				CS$<>8__locals1.stack
			});
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0007DF18 File Offset: 0x0007C118
		private static float firstColumnWidth
		{
			get
			{
				return 400f;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0007DF1F File Offset: 0x0007C11F
		private static Color nameBackColor
		{
			get
			{
				return new Color(125, 125, 125);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0007DF2C File Offset: 0x0007C12C
		private static Rectangle nameBackRect
		{
			get
			{
				return new Rectangle(2303, 1936, 566, 44);
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0007DF44 File Offset: 0x0007C144
		private Form {20668}(string {20669}, Rectangle {20670}, int {20671})
		{
			int portLevel = this.{20702}.PortLevel;
			int maxLevel = this.{20702}.MaxLevel;
			int num = 132;
			Form form = new Form(new Marker(0f, 0f, {20664}.firstColumnWidth + (float)(num * 3), 50f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			StackForm stackForm = {20664}.<ComposeHeader>g__MakeHeader|17_0({20669}, Color.Wheat, (int){20664}.firstColumnWidth, new Rectangle?({20670}), false);
			bool flag = Global.Player.NearPort.AllowCapture && portLevel < maxLevel;
			form.AddChildPos(stackForm, PositionAlignment.LeftUp, PositionAlignment.RightDown, {20664}.borders, 0f, false);
			if ({20671} > 0)
			{
				int {20681} = num - 20;
				float num2 = (float)(num / 2) + {20664}.borders * 3f;
				stackForm = {20664}.<ComposeHeader>g__MakeHeader|17_0(({20671} > 1) ? Local.condition : "", new Color(160, 200, 145), {20681}, null, true);
				form.AddChildPos(stackForm, PositionAlignment.LeftUp, PositionAlignment.RightDown, {20664}.firstColumnWidth + num2 - stackForm.Pos.HalfSize.X, 0f, false);
				stackForm = {20664}.<ComposeHeader>g__MakeHeader|17_0(Local.current_port_level, new Color(136, 176, 212), {20681}, null, true);
				form.AddChildPos(stackForm, PositionAlignment.LeftUp, PositionAlignment.RightDown, {20664}.firstColumnWidth + (float)num + num2 - stackForm.Pos.HalfSize.X, 0f, false);
				if (flag)
				{
					stackForm = {20664}.<ComposeHeader>g__MakeHeader|17_0(Local.next_port_level, Color.LightGray, {20681}, null, true);
					form.AddChildPos(stackForm, PositionAlignment.LeftUp, PositionAlignment.RightDown, {20664}.firstColumnWidth + (float)(num * 2) + num2 - stackForm.Pos.HalfSize.X, 0f, false);
				}
			}
			return form;
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0007E124 File Offset: 0x0007C324
		private UiControl {20672}(in {20664}.BonusStatus {20673})
		{
			{20664}.<>c__DisplayClass18_0 CS$<>8__locals1;
			CS$<>8__locals1.type = {20673}.Bonus.Type;
			CS$<>8__locals1.showFirstColumn = {20673}.ShowFirstColumn;
			if (CS$<>8__locals1.type == PortLevelBonusType.ResourceProduction && Global.Player.NearPort.PbsManufacture == null)
			{
				return new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			}
			if (CS$<>8__locals1.type == PortLevelBonusType.ShipCraftingInLowerRankPort && Global.Player.NearPort.BuildShipRangs == 1)
			{
				return new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			}
			CS$<>8__locals1.item = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.otherColumnWidths = 132;
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_12, 0f);
			string text = CS$<>8__locals1.type.GetBonusText();
			if (CS$<>8__locals1.type == PortLevelBonusType.ResourceProduction)
			{
				text = text + " (" + Global.Player.NearPort.PbsManufacture.Name + ")";
			}
			if (CS$<>8__locals1.type == PortLevelBonusType.ShipCraftingInLowerRankPort)
			{
				text = Local.ShipCraftingInLowerRankPort_full(Global.Player.NearPort.BuildShipRangs - 1);
			}
			float {11528} = CS$<>8__locals1.showFirstColumn ? {20664}.firstColumnWidth : ({20664}.firstColumnWidth + (float)CS$<>8__locals1.otherColumnWidths);
			textBlockBuilder.WriteLines(text, Color.LightGray * 0.9f, Fonts.Arial_12, {20664}.firstColumnWidth - 10f, new float?(0f));
			TextBlockControl textBlockControl = textBlockBuilder.Create();
			Form form = new Form(new Marker(0f, 0f, {11528}, textBlockControl.Pos.WH.Y + {20664}.borders * 2f), {20664}.nameBackRect, {20664}.nameBackColor, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form.AddChildPos(textBlockControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp, {20664}.borders * 2f, {20664}.borders, false);
			CS$<>8__locals1.item.AddItem(new UiControl[]
			{
				form
			});
			CS$<>8__locals1.maxHeight = 28;
			CS$<>8__locals1.valueBackRect = new Rectangle(2306, 1936, 566, 44);
			CS$<>8__locals1.valuePieceVerticalPadding = 1;
			Color {20685} = new Color(188, 209, 174);
			Color {20686} = new Color(200, 220, 135);
			Color {20696} = new Color(188, 168, 178);
			Color {20696}2 = new Color(205, 210, 250);
			Color {20695} = new Color(148, 187, 206);
			if (CS$<>8__locals1.showFirstColumn)
			{
				string {20684} = "";
				string text2 = "";
				PortBonusCriteria collisionCriteria = {20673}.CollisionCriteria;
				if (!{20673}.IsPossible)
				{
					if ({20673}.Bonus.Criteria.HasFlag(PortBonusCriteria.PirateTrustedSupply) || {20673}.Bonus.Criteria.HasFlag(PortBonusCriteria.PirateOwner) || {20673}.Bonus.Criteria.HasFlag(PortBonusCriteria.AllWhenPirateOwner))
					{
						{20684} = FractionID.Pirate.GetName();
						text2 = Local.townhall_access_be_pirate;
					}
					else if ({20673}.Bonus.Criteria.HasFlag(PortBonusCriteria.TraderOwnerAndTrusted) && Global.Player.NearPort.IsArabPort)
					{
						{20684} = FractionID.TradeUnion.GetName();
						text2 = Local.townhall_access_trader_owner;
					}
					else if ({20673}.Bonus.Criteria.HasFlag(PortBonusCriteria.TradeHqInNation))
					{
						{20684} = Local.trade_hq;
						text2 = Local.townhall_access_trader_available;
					}
					else
					{
						{20684} = Local.fractions;
						text2 = Local.townhall_access_be_fraction;
					}
				}
				else
				{
					bool flag = collisionCriteria == PortBonusCriteria.NationSupply || collisionCriteria == PortBonusCriteria.PirateTrustedSupply;
					if (flag)
					{
						{20684} = Local.townhall_access_supply;
						text2 = Local.townhall_access_ally({20673}.MaxAvailableLevel);
					}
					if (collisionCriteria == PortBonusCriteria.NeutralNationReputation)
					{
						{20684} = Local.reputation;
						text2 = (({20673}.MaxAvailableLevel == 0) ? Local.townhall_access_player_no_rep(Session.Game.NearPortStatus.CapturerFraction.GetName()) : Local.townhall_access_player(Session.Game.NearPortStatus.CapturerFraction.GetName(), {20673}.MaxAvailableLevel));
					}
					if (collisionCriteria == PortBonusCriteria.NeutralPirateReputation)
					{
						{20684} = Local.reputation;
						text2 = (({20673}.MaxAvailableLevel == 0) ? Local.townhall_access_playerpirate_no_rep : Local.townhall_access_playerpirate({20673}.MaxAvailableLevel));
					}
					if (collisionCriteria == PortBonusCriteria.TradeHqInNation)
					{
						{20684} = Local.trade_hq;
						text2 = Local.townhall_access_trader_available;
					}
				}
				if (!{20673}.IsPossible)
				{
					Form form2 = {20664}.<MakeBonusItem>g__MakeFloatValuePiece|18_0({20684}, {20685}, {20686}, Fonts.Arial_10, ref CS$<>8__locals1);
					CS$<>8__locals1.item.AddItem(new UiControl[]
					{
						form2
					});
				}
				else
				{
					{20664}.<MakeBonusItem>g__AddTextHelper|18_3(Color.Orange * 0.7f, Color.Lerp(Color.Orange, Color.DimGray, 0.6f), {20673}.AvailableAmount, ref CS$<>8__locals1);
				}
				if (!string.IsNullOrEmpty(text2))
				{
					CS$<>8__locals1.item.ToolTipState = new ToolTipState("", text2, Array.Empty<ToolTipCharacteristics>());
				}
			}
			{20664}.<MakeBonusItem>g__AddTextHelper|18_3({20695}, {20696}2, {20673}.NowLevelAmount, ref CS$<>8__locals1);
			if ({20673}.ShowNextLevel)
			{
				{20664}.<MakeBonusItem>g__AddTextHelper|18_3(Color.LightGray, {20696}, {20673}.NextLevelAmount, ref CS$<>8__locals1);
			}
			Form form3 = new Form(CS$<>8__locals1.item.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form3.AddChild(CS$<>8__locals1.item);
			form3.UpdateComplete += delegate(UiControl {20716})
			{
				{20716}.Brightness = (float)(({20716}.InputMode == MouseInputMode.NoFocus) ? 1 : 3);
			};
			return form3;
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0007E69C File Offset: 0x0007C89C
		private Form {20674}()
		{
			{20664}.<>c__DisplayClass19_0 CS$<>8__locals1 = new {20664}.<>c__DisplayClass19_0();
			CS$<>8__locals1.<>4__this = this;
			Form form = new Form(new Marker(0f, 0f, base.Pos.WH.X, 50f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.button = new Button(new Marker(0f, 0f, 350f, 30f), {17625}.c_btGray_mid, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.button.SetText(Local.guild_openhq, Fonts.Philosopher_14, Color.White, false);
			CS$<>8__locals1.button.EvClick += delegate(ClickUiEventArgs {20718})
			{
				int tradeHqPrice = CS$<>8__locals1.<>4__this.{20702}.GetTradeHqPrice();
				bool {17457} = Session.Guild.ConquerBadges < tradeHqPrice || Session.Guild.Treasury < tradeHqPrice;
				string {17385} = Local.guild_openhq_question(Global.Player.NearPort.PortName);
				Action<int> {17386};
				if (({17386} = CS$<>8__locals1.<>9__2) == null)
				{
					{17386} = (CS$<>8__locals1.<>9__2 = delegate(int {20719})
					{
						if ({20719} != 0)
						{
							return;
						}
						CS$<>8__locals1.button.AllowMouseInput = false;
						CS$<>8__locals1.button.Opacity = 0.5f;
						Global.Network.Send(new OnOpenTradeHq(CS$<>8__locals1.<>4__this.{20702}.PortID));
					});
				}
				{17443}[] array = new {17443}[2];
				int num = 0;
				string yes = Local.yes;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
				defaultInterpolatedStringHandler.AppendLiteral("-");
				defaultInterpolatedStringHandler.AppendFormatted<int>(tradeHqPrice);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(Local.conquer_badges_and_ingots);
				array[num] = new {17443}(yes, defaultInterpolatedStringHandler.ToStringAndClear(), {17312}.cIconMoney, {17457}, 0f);
				array[1] = new {17443}(Local.undo, string.Empty, {17312}.cIconReject, false, 0f);
				new {17312}({17385}, {17386}, array);
			};
			if (Session.Guild.HasHqOrLicense((int)this.{20702}.PortID))
			{
				CS$<>8__locals1.<MakeOpenHqButton>g__AddError|1(Local.guild_openhq_alreadyopened);
			}
			else if (!Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury)
			{
				CS$<>8__locals1.<MakeOpenHqButton>g__AddError|1(Local.GuidFortManagingUi_right1);
			}
			form.AddChildPos(CS$<>8__locals1.button, PositionAlignment.LeftUp, PositionAlignment.Center, 0f);
			return form;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0007E798 File Offset: 0x0007C998
		private void {20675}(ListItemViewControl {20676})
		{
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				{20664}.<SupplyRatingTab>g__MakeItem|20_0(Local.nicknames, Local.supply_points_added, Color.Gray)
			});
			if (this.{20703} != null)
			{
				foreach (KeyValuePair<string, float> keyValuePair in from {20717} in this.{20703}.BaseDictionary
				orderby {20717}.Value descending
				select {20717})
				{
					stackForm.AddItem(new UiControl[]
					{
						{20664}.<SupplyRatingTab>g__MakeItem|20_0(keyValuePair.Key, ((int)keyValuePair.Value).ToString("n", {20664}.numFormatInfo), Color.White)
					});
				}
			}
			{20676}.AddItem(new UiControl[]
			{
				stackForm
			});
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0007E8B8 File Offset: 0x0007CAB8
		[CompilerGenerated]
		private void {20677}(ListItemViewControl {20678})
		{
			Global.Network.Send(default(OnPortSupplyInfoRequest));
			this.{20675}({20678});
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0007E8E4 File Offset: 0x0007CAE4
		[CompilerGenerated]
		internal static Form <TownHallMainTab>g__MakeWarning|9_11()
		{
			int num = 20;
			int num2 = 830;
			Form form = new Form(new Marker(0f, 0f, (float)num2, (float)({17625}.c_seamlessbackUp.Height + {17625}.c_seamlessbackDown.Height - num)), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form.AddChild(new Image(Vector2.Zero, CommonAtlas.Texture.Tex, {17625}.c_seamlessbackUp, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Image(new Marker(0f, (float)({17625}.c_seamlessbackUp.Height - num), form.Pos.WH.X, (float){17625}.c_seamlessbackDown.Height), CommonAtlas.Texture.Tex, {17625}.c_seamlessbackDown, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Image(new Marker(0f, 0f, ref form.Pos.WH), CommonAtlas.Texture.Tex, new Rectangle(1295, 554, 397, 25), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_14, 0f);
			textBlockBuilder.WriteLines(Local.guild_level_not_enough_for_bonuses, Color.Salmon, Fonts.Philosopher_14, (float)(num2 - num), new float?(0f));
			form.AddChildPos(textBlockBuilder.Create(), PositionAlignment.Center, PositionAlignment.Center, 0f);
			return form;
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0007EA34 File Offset: 0x0007CC34
		[CompilerGenerated]
		internal static StackForm <ComposeHeader>g__MakeHeader|17_0(string {20679}, Color {20680}, int {20681}, Rectangle? {20682} = null, bool {20683} = true)
		{
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if ({20682} != null)
			{
				int num = 18;
				Button button = new Button(Vector2.Zero, {20682}.Value, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button.PosWidth = (float)num;
				button.PosHeight = (float)num;
				stackForm.AddItem(new UiControl[]
				{
					button
				});
				int num2 = 10;
				stackForm.AddSpace((float)num2);
				{20681} -= (int)(button.Pos.WH.X + (float)num2);
			}
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_14, 0f);
			textBlockBuilder.WriteLines({20679}, {20680}, textBlockBuilder.defaultFont, (float)({20681} - 20), new float?((float)-5));
			TextBlockControl textBlockControl = {20683} ? textBlockBuilder.CreateCentroid() : textBlockBuilder.Create();
			stackForm.AddItem(new UiControl[]
			{
				textBlockControl
			});
			return stackForm;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0007EB0C File Offset: 0x0007CD0C
		[CompilerGenerated]
		internal static Form <MakeBonusItem>g__MakeFloatValuePiece|18_0(string {20684}, Color {20685}, Color {20686}, [Nullable(2)] CustomSpriteFont {20687} = null, ref {20664}.<>c__DisplayClass18_0 {20688})
		{
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder({20687} ?? Fonts.Arial_12, 0f);
			textBlockBuilder.WriteLines({20684}, {20685}, textBlockBuilder.defaultFont, (float){20688}.otherColumnWidths, new float?(0f));
			TextBlockControl textBlockControl = textBlockBuilder.Create();
			Form form = {20664}.<MakeBonusItem>g__MakeValuePieceBack|18_2(({20684} != "-") ? {20686} : {20686}.Multiply(Color.Gray), ref {20688});
			form.AddChildPos(textBlockControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp, {20664}.borders * 2f + (float)({20688}.otherColumnWidths / 2) - textBlockControl.Pos.HalfSize.X + 2f, {20664}.borders, false);
			return form;
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0007EBB7 File Offset: 0x0007CDB7
		[CompilerGenerated]
		internal static Form <MakeBonusItem>g__MakeBoolValuePiece|18_1(bool {20689}, Color {20690}, Color {20691}, ref {20664}.<>c__DisplayClass18_0 {20692})
		{
			return {20664}.<MakeBonusItem>g__MakeFloatValuePiece|18_0({20689} ? "✔" : ({20692}.showFirstColumn ? "x" : "-"), {20690}, {20691}, null, ref {20692});
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0007EBE0 File Offset: 0x0007CDE0
		[CompilerGenerated]
		internal static Form <MakeBonusItem>g__MakeValuePieceBack|18_2(Color {20693}, ref {20664}.<>c__DisplayClass18_0 {20694})
		{
			Form form = new Form(new Marker(0f, 0f, (float){20694}.otherColumnWidths, (float){20694}.maxHeight), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			form.AddChildPos(new Form(new Marker(0f, 0f, (float){20694}.otherColumnWidths, (float)({20694}.maxHeight - {20694}.valuePieceVerticalPadding * 2)), {20694}.valueBackRect, {20693}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			}, PositionAlignment.LeftUp, PositionAlignment.Center, 0f);
			return form;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0007EC64 File Offset: 0x0007CE64
		[CompilerGenerated]
		internal static void <MakeBonusItem>g__AddTextHelper|18_3(Color {20695}, Color {20696}, float {20697}, ref {20664}.<>c__DisplayClass18_0 {20698})
		{
			if ({20698}.type.IsBoolean())
			{
				{20698}.item.AddItem(new UiControl[]
				{
					{20664}.<MakeBonusItem>g__MakeBoolValuePiece|18_1({20697} > 0f, {20695}, {20696}, ref {20698})
				});
				return;
			}
			if ({20698}.type.IsPercent() && {20698}.type != PortLevelBonusType.ResourceProduction)
			{
				StackForm item = {20698}.item;
				UiControl[] array = new UiControl[1];
				int num = 0;
				string {20684};
				if (!({20697} > 0f | {20698}.showFirstColumn))
				{
					{20684} = "-";
				}
				else
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
					defaultInterpolatedStringHandler.AppendFormatted<float>({20697} * 100f, "F0");
					defaultInterpolatedStringHandler.AppendLiteral("%");
					{20684} = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				array[num] = {20664}.<MakeBonusItem>g__MakeFloatValuePiece|18_0({20684}, {20695}, {20696}, null, ref {20698});
				item.AddItem(array);
				return;
			}
			{20698}.item.AddItem(new UiControl[]
			{
				{20664}.<MakeBonusItem>g__MakeFloatValuePiece|18_0(({20697} > 0f | {20698}.showFirstColumn) ? (StringHelper.GetValueOfK((int){20697}) ?? "") : "-", {20695}, {20696}, null, ref {20698})
			});
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0007ED64 File Offset: 0x0007CF64
		[CompilerGenerated]
		internal static Form <SupplyRatingTab>g__MakeItem|20_0(string {20699}, string {20700}, Color {20701})
		{
			Rectangle rectangle = new Rectangle(1, 363, 595, 39);
			Form form = new Form(new Marker(0f, 0f, (float)rectangle.Width * 1.36f, (float)rectangle.Height), rectangle, Color.LightSkyBlue, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, {20701}, {20699}, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, 10f, 0f, false);
			form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, {20701}, {20700}, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.RightDown, PositionAlignment.Center, form.Pos.WH.X / 2f, 0f, false);
			return form;
		}

		// Token: 0x04000DC6 RID: 3526
		public static {20664} Instance;

		// Token: 0x04000DC7 RID: 3527
		private const int windowWidth = 820;

		// Token: 0x04000DC8 RID: 3528
		private static NumberFormatInfo numFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();

		// Token: 0x04000DC9 RID: 3529
		private PortDevelopment {20702};

		// Token: 0x04000DCA RID: 3530
		private ScoreDictionary<string> {20703};

		// Token: 0x04000DCB RID: 3531
		private static float borders = 5f;

		// Token: 0x020002A8 RID: 680
		private readonly struct BonusStatus
		{
			// Token: 0x17000138 RID: 312
			// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0007EE1D File Offset: 0x0007D01D
			public bool IsReduced
			{
				get
				{
					return this.IsPossible && this.MaxAvailableLevel < this.Dev.PortLevel && this.AvailableAmount == 0f;
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0007EE49 File Offset: 0x0007D049
			public bool ShowNextLevel
			{
				get
				{
					return this.Dev.PortLevel < this.Dev.MaxLevel;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0007EE63 File Offset: 0x0007D063
			public bool ShowFirstColumn
			{
				get
				{
					return this.UiCategory != 0 || this.IsReduced || this.AvailableAmount < this.NowLevelAmount;
				}
			}

			// Token: 0x06000F06 RID: 3846 RVA: 0x0007EE88 File Offset: 0x0007D088
			public BonusStatus(PortLevelBonus {20707}, PortDevelopment {20708}, IEnumerable<PortBonusCriteria> {20709})
			{
				this.Dev = {20708};
				this.Bonus = {20707};
				this.IsPossible = {20709}.Any((PortBonusCriteria {20710}) => {20707}.Criteria.HasFlag({20710}));
				Decorator game = Session.Game;
				PortLevelBonus bonus = {20707};
				Decorator game2 = Session.Game;
				this.MaxAvailableLevel = game.GetPortBonusMaxLevel({20709}, bonus, game2.NearPortStatus, out this.CollisionCriteria);
				PortLevelBonusType type = {20707}.Type;
				game2 = Session.Game;
				this.AvailableAmount = {20708}.GetAvailableBonus(type, game2, Session.Game.NearPortStatus, {20707});
				this.NowLevelAmount = {20707}.GetValue({20708}.PortLevel, true);
				this.NextLevelAmount = {20707}.GetValue(Math.Min({20708}.PortLevel + 1, {20708}.MaxLevel), true);
				if ({20707}.Type == PortLevelBonusType.ResourceProduction)
				{
					this.AvailableAmount = (this.NowLevelAmount = (float)Gameplay.GetPbsFactoryAmount({20708}.PortLevel, false, Global.Player.NearPort.PbsManufacture));
					this.NextLevelAmount = (float)Gameplay.GetPbsFactoryAmount(Math.Min({20708}.MaxLevel, {20708}.PortLevel + 1), false, Global.Player.NearPort.PbsManufacture);
				}
				this.UiCategory = ((this.MaxAvailableLevel > 0) ? 0 : (this.IsPossible ? 1 : 2));
				if (this.CollisionCriteria == PortBonusCriteria.TradeHqInNation && this.MaxAvailableLevel == 0)
				{
					this.UiCategory = 1;
					this.IsPossible = false;
				}
			}

			// Token: 0x04000DCC RID: 3532
			public readonly PortLevelBonus Bonus;

			// Token: 0x04000DCD RID: 3533
			public readonly bool IsPossible;

			// Token: 0x04000DCE RID: 3534
			public readonly int MaxAvailableLevel;

			// Token: 0x04000DCF RID: 3535
			public readonly float AvailableAmount;

			// Token: 0x04000DD0 RID: 3536
			public readonly float NowLevelAmount;

			// Token: 0x04000DD1 RID: 3537
			public readonly float NextLevelAmount;

			// Token: 0x04000DD2 RID: 3538
			public readonly PortBonusCriteria CollisionCriteria;

			// Token: 0x04000DD3 RID: 3539
			public readonly PortDevelopment Dev;

			// Token: 0x04000DD4 RID: 3540
			public readonly int UiCategory;
		}
	}
}
