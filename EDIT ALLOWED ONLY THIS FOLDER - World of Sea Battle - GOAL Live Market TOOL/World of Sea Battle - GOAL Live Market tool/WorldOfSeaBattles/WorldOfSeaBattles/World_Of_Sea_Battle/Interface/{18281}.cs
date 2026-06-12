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
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using TheraEngine.PacketValues;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000127 RID: 295
	internal class {18281} : {17625}
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x000359E3 File Offset: 0x00033BE3
		private string TraderName { get; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x000359EB File Offset: 0x00033BEB
		private int CurrencyItemId { get; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x000359F3 File Offset: 0x00033BF3
		private string CurrencyName
		{
			get
			{
				return Gameplay.ItemsInfo[this.CurrencyItemId].Name;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00035A0A File Offset: 0x00033C0A
		private int TotalCurrency
		{
			get
			{
				if (!Global.Player.IsPortEntry)
				{
					return 0;
				}
				return Session.Account.NearPortStorage[this.CurrencyItemId] + Session.Account.TreasuryMaps[this.CurrencyItemId];
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00035A45 File Offset: 0x00033C45
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x00035A4D File Offset: 0x00033C4D
		protected override float winterStyleYDecorOffset { get; set; } = 6f;

		// Token: 0x060006D2 RID: 1746 RVA: 0x00035A58 File Offset: 0x00033C58
		public {18281}(int {18286}, string {18287}, TraderOffer[] {18288})
		{
			List<TraderOffer[]> source;
			base..ctor(700f, {17625}.c_back3, {17604}.InGameWindowBlockShip, {17625}.c_icTreasury, {18281}.GetPages({18288}, out source));
			this.CurrencyItemId = {18286};
			this.TraderName = {18287};
			base.AllowMouseInput = true;
			base.ComposeTabWithScroll(new Action<StackForm>(this.AddHeader), false, true, (from {18306} in source
			select delegate(ListItemViewControl {18321})
			{
				this.AddItems({18321}, {18306});
			}).ToArray<Action<ListItemViewControl>>());
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00035AD4 File Offset: 0x00033CD4
		private static {17625}.DynamicTittle[] GetPages(TraderOffer[] {18289}, out List<TraderOffer[]> {18290})
		{
			{18281}.<>c__DisplayClass17_0 CS$<>8__locals1;
			CS$<>8__locals1.titles = new List<{17625}.DynamicTittle>();
			{18290} = new List<TraderOffer[]>();
			{18281}.<GetPages>g__TryAdd|17_0(Local.PirateTrader_All, {18289}, ref {18290}, ref CS$<>8__locals1);
			{18281}.<GetPages>g__TryAdd|17_0(Local.PirateTrader_Valuable, (from {18315} in {18289}
			where {18315}.Res != null || {18315}.Gold.Value != 0
			select {18315}).ToArray<TraderOffer>(), ref {18290}, ref CS$<>8__locals1);
			{18281}.<GetPages>g__TryAdd|17_0(Local.PirateTrader_Weapon, (from {18316} in {18289}
			where {18316}.CannonId != -1
			select {18316}).ToArray<TraderOffer>(), ref {18290}, ref CS$<>8__locals1);
			{18281}.<GetPages>g__TryAdd|17_0(Local.PirateTrader_Design, (from {18317} in {18289}
			where {18317}.DesignId != -1
			select {18317}).ToArray<TraderOffer>(), ref {18290}, ref CS$<>8__locals1);
			return CS$<>8__locals1.titles.ToArray();
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00035BB4 File Offset: 0x00033DB4
		protected void AddHeader(StackForm {18291})
		{
			LiveLabel liveLabel = new LiveLabel(Vector2.Zero, Fonts.Philosopher_14, Color.White, new Func<string>(this.{18311}), 50);
			Form form = new Form(liveLabel.Pos.Offset(0f, -20f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChild(liveLabel);
			{18291}.AddItem(new UiControl[]
			{
				form
			});
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00035C1C File Offset: 0x00033E1C
		protected void AddItems(ListItemViewControl {18292}, params TraderOffer[] {18293})
		{
			if (PlatformTuning.DisableShop)
			{
				{18293} = (from {18318} in {18293}
				where !{18318}.PriceInMonets
				select {18318}).ToArray<TraderOffer>();
			}
			if (!CalendarEvents.CurrentEvent.IsActive)
			{
				{18293} = (from {18319} in {18293}
				where !{18319}.PaidSkipQuest
				select {18319}).ToArray<TraderOffer>();
			}
			for (int i = 0; i < {18293}.Length; i += 2)
			{
				StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm.AddItem(new UiControl[]
				{
					this.{18294}({18293}[i])
				});
				stackForm.AddSpace(1f);
				if (i + 1 < {18293}.Length)
				{
					stackForm.AddItem(new UiControl[]
					{
						this.{18294}({18293}[i + 1])
					});
				}
				{18292}.AddItem(new UiControl[]
				{
					stackForm
				});
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00035D10 File Offset: 0x00033F10
		private Form {18294}(TraderOffer {18295})
		{
			Form form = new Form(new Marker(0f, 0f, 350f, 62f), {18281}.c_pageItem, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = true
			};
			int num = 58;
			string[] array = {20431}.SeparateNames({18295}.GetName(1));
			string text;
			if (!{18295}.PriceInMonets)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
				defaultInterpolatedStringHandler.AppendFormatted(Local.price);
				defaultInterpolatedStringHandler.AppendLiteral(": ");
				defaultInterpolatedStringHandler.AppendFormatted<RTI>({18295}.Price);
				text = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			else
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(3, 3);
				defaultInterpolatedStringHandler2.AppendFormatted(Local.cost);
				defaultInterpolatedStringHandler2.AppendLiteral(" ");
				defaultInterpolatedStringHandler2.AppendFormatted(Local.monets);
				defaultInterpolatedStringHandler2.AppendLiteral(": ");
				defaultInterpolatedStringHandler2.AppendFormatted<RTI>({18295}.Price);
				text = defaultInterpolatedStringHandler2.ToStringAndClear();
			}
			string {13345} = text;
			Label priceLabel = new Label(new Vector2(70f, 26f), Fonts.Arial_12, Color.White * 0.6f, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChild({18281}.GetIcon(new Marker(4f, 3f, (float)num, (float)num), {18295}));
			form.AddChild(new Form(new Marker(4f, 3f, (float)num, (float)num), {18281}.c_pageItemMaskIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
			form.AddChild(new Label(new Vector2(70f, 5f), Fonts.Arial_12, Color.Wheat * 0.8f, array[0], PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(priceLabel);
			form.EvClick += delegate(ClickUiEventArgs {18322})
			{
				this.{18296}({18295});
			};
			form.UpdateComplete += delegate(UiControl {18323})
			{
				bool flag = (!{18295}.PriceInMonets && {18295}.Price.Value > this.TotalCurrency) || ({18295}.PriceInMonets && {18295}.Price.Value > Session.Account.Monets.Value);
				priceLabel.BasicColor = (flag ? (Color.Orange * 0.6f) : (Color.White * 0.6f));
			};
			if (array.Length > 1)
			{
				form.ToolTipState = new ToolTipState("", array[1], Array.Empty<ToolTipCharacteristics>());
			}
			if ({18295}.CannonId != -1)
			{
				{20431}.CannonToolTIp(Gameplay.CannonsGameInfo.FromID({18295}.CannonId), form, false, false, null);
			}
			if ({18295}.DesignId != -1)
			{
				ShipDesignInfo shipDesignInfo = Gameplay.DesignsInfo[{18295}.DesignId];
				form.ToolTipState = new ToolTipState(shipDesignInfo.Name, Local.PortRealShopPage_52, Array.Empty<ToolTipCharacteristics>());
				if (Session.Account.HasDesign({18295}.DesignId))
				{
					form.AddChild(new Label(new Vector2(300f, 25f), Fonts.Arial_12, Color.Green * 0.8f, "✔", PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				}
			}
			if ({18295}.PaidSkipQuest)
			{
				form.ToolTipState = new ToolTipState("", Local.paid_skip_quest_tt, Array.Empty<ToolTipCharacteristics>());
			}
			return form;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00035FF4 File Offset: 0x000341F4
		private void {18296}(TraderOffer {18297})
		{
			if ({18297}.PaidSkipQuest)
			{
				{18281}.BuySkipQuest({18297});
				return;
			}
			if ({18297}.DesignId != -1)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted(Local.shop);
				defaultInterpolatedStringHandler.AppendLiteral(", -");
				defaultInterpolatedStringHandler.AppendFormatted<RTI>({18297}.Price);
				Action<int> <>9__1;
				{20755}.BuyButton {20761} = new {20755}.BuyButton(defaultInterpolatedStringHandler.ToStringAndClear(), {18297}.PriceInMonets ? (Session.Account.Monets.Value < {18297}.Price.Value) : ({18297}.Price.Value < this.TotalCurrency), delegate()
				{
					if ({18297}.DesignId != -1 && Session.Account.HasDesign({18297}.DesignId))
					{
						string {17385} = Local.PirateTrader_AlreadyHas({18297}.GetName(1));
						Action<int> {17386};
						if (({17386} = <>9__1) == null)
						{
							{17386} = (<>9__1 = delegate(int {18324})
							{
								if ({18324} == 0)
								{
									this.{18298}({18297});
								}
							});
						}
						{17443}[] array = new {17443}[2];
						int num = 0;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(3, 2);
						defaultInterpolatedStringHandler2.AppendFormatted(Local.shop);
						defaultInterpolatedStringHandler2.AppendLiteral(", -");
						defaultInterpolatedStringHandler2.AppendFormatted<RTI>({18297}.Price);
						array[num] = new {17443}(defaultInterpolatedStringHandler2.ToStringAndClear(), string.Empty, {17312}.cIconAccept, false, 0f);
						array[1] = new {17443}(Local.close, string.Empty, {17312}.cIconReject, false, 0f);
						new {17312}({17385}, {17386}, array);
						return;
					}
					this.{18298}({18297});
				});
				{20755}.StartFitting(Gameplay.DesignsInfo[{18297}.DesignId], this, {20761});
				return;
			}
			this.{18298}({18297});
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00036104 File Offset: 0x00034304
		private void {18298}(TraderOffer {18299})
		{
			if ({18299}.PriceInMonets && Session.Account.Monets.Value < {18299}.Price.Value)
			{
				this.BlockAndClose();
				Global.Game.ScenePort.realShopHandler(null, null);
				{20881}.ShowBuyMonetsToolTip({18299}.Price.Value);
				return;
			}
			if (!{18299}.PriceInMonets && this.TotalCurrency < {18299}.Price.Value)
			{
				return;
			}
			if ({18299}.DesignId != -1)
			{
				this.{18300}({18299}, 1);
				return;
			}
			new {21838}({18299}.GetName(1), Local.shop, delegate(int {18325})
			{
				this.{18300}({18299}, {18325});
			}, Math.Min(({18299}.DesignId != -1) ? 1 : int.MaxValue, this.TotalCurrency / {18299}.Price.Value), null, null, null, null, null);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00036240 File Offset: 0x00034440
		private void {18300}(in TraderOffer {18301}, int {18302})
		{
			ValueTuple<string, int, int, int> valueTuple = {18301}.Apply(Session.Account, false, {18302}, new int?(this.CurrencyItemId));
			string item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			int item3 = valueTuple.Item3;
			int item4 = valueTuple.Item4;
			LogbookController logbook = Global.Settings.Logbook;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
			defaultInterpolatedStringHandler.AppendLiteral("+");
			defaultInterpolatedStringHandler.AppendFormatted<int>(item2);
			defaultInterpolatedStringHandler.AppendLiteral(" ");
			defaultInterpolatedStringHandler.AppendFormatted(item);
			defaultInterpolatedStringHandler.AppendLiteral(" (");
			defaultInterpolatedStringHandler.AppendFormatted(this.TraderName);
			defaultInterpolatedStringHandler.AppendLiteral(")");
			logbook.Write(defaultInterpolatedStringHandler.ToStringAndClear(), LBFlags.L1);
			if (item3 > 0)
			{
				Global.Network.NetClient.Send(new OnPremiumAddMsg(OnPremiumAddMsg.Subscription.PremiumDays, item3, 0));
			}
			if (item4 > 0)
			{
				Global.Network.NetClient.Send(new OnPremiumAddMsg(OnPremiumAddMsg.Subscription.LegendaryFlagDays, item4, 0));
			}
			if (item3 > 0 || item4 > 0)
			{
				Global.Game.ScenePort.MakeAccSync();
			}
			if ({18301}.DesignId > 0)
			{
				this.RefreshCurrentDynamicTabPage();
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00036358 File Offset: 0x00034558
		private unsafe static void BuySkipQuest(TraderOffer {18303})
		{
			Bits16 bits = *Session.Account.Events.GetEvent(CalendarEvents.CurrentEvent.Type);
			List<int> possibleDays = new List<int>();
			int num = Math.Min(CalendarEvents.CurrentEvent.CurrentDayCached, CalendarEvents.CurrentEvent.Duration);
			for (int i = 1; i <= num; i++)
			{
				if (!bits[i])
				{
					possibleDays.Add(i);
				}
			}
			if (possibleDays.Count == 0)
			{
				new {17312}(Local.paid_skip_no_days);
				return;
			}
			new {17107}(Local.paid_skip_quest, Local.paid_skip_quest_tt, "", 2, delegate(int {18326})
			{
				if ({18326} >= possibleDays.Count || {18303}.Price.Value > Session.Account.Monets.Value)
				{
					return;
				}
				int num2 = possibleDays[{18326}];
				int questDay = CalendarEvents.CurrentEvent.GetQuestDay(Session.Account, num2);
				QuestEngine quests = Session.Account.Quests;
				Decorator game = Session.Game;
				quests.SkipCalendarQuest(game, num2, questDay);
				PlayerAccount account = Session.Account;
				account.Monets.Value = account.Monets.Value - {18303}.Price.Value;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
				defaultInterpolatedStringHandler.AppendLiteral("-");
				defaultInterpolatedStringHandler.AppendFormatted<int>({18303}.Price.Value);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(Local.monets);
				defaultInterpolatedStringHandler.AppendLiteral(" (");
				defaultInterpolatedStringHandler.AppendFormatted(Local.paid_skip_quest);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				{19994}.Logbook(defaultInterpolatedStringHandler.ToStringAndClear(), LBFlags.L0);
				Global.Network.Send(new OnGetQuestMsg(0, QuestAction.Get, num2, null));
				{19994}.MeAndLogbook({19988}.Okay, Local.GameAccount_5(Gameplay.QuestsInfo.First(delegate(QuestInfo {18327})
				{
					int? calendarQuestDay = {18327}.CalendarQuestDay;
					int questDay = questDay;
					return calendarQuestDay.GetValueOrDefault() == questDay & calendarQuestDay != null;
				}).GetName(Session.Account)), null);
			}, true, null, possibleDays.Select(delegate(int {18320})
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted(Local.day);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted<int>({18320});
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}).Concat(new <>z__ReadOnlySingleElementList<string>(Local.to_back)).ToArray<string>());
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00036454 File Offset: 0x00034654
		public static Image GetIcon(Marker {18304}, in TraderOffer {18305})
		{
			if ({18305}.PaidSkipQuest)
			{
				return new Image({18304}, CommonAtlas.Texture.Tex, CommonAtlas.goldIconMany64, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			}
			if ({18305}.Res != null && {18305}.Res.Value.Type == StorageAssetEnum.Powerup_DisplayOnly)
			{
				return new Image({18304}, Gameplay.PowerupItems[{18305}.Res.Value.Id.Value].Icon, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			}
			if ({18305}.Res != null)
			{
				return new Image({18304}, Gameplay.GetResource({18305}.Res.Value.Id.Value, {18305}.Res.Value.Type).getIconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			}
			if ({18305}.Monets.Value != 0)
			{
				return new Image({18304}, CommonAtlas.Texture.Tex, new Rectangle(827, 534, 38, 38), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			}
			if ({18305}.Gold.Value != 0)
			{
				return new Image({18304}, CommonAtlas.Texture.Tex, CommonAtlas.goldIconMany64, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			}
			if ({18305}.DesignId != -1)
			{
				ShipDesignInfo shipDesignInfo = Gameplay.DesignsInfo[{18305}.DesignId];
				if (shipDesignInfo.ApartIconTex != null)
				{
					return new Image({18304}, shipDesignInfo.ApartIconTexAlt ?? shipDesignInfo.ApartIconTex, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				}
				return shipDesignInfo.DesignElementTextureIcon({18304});
			}
			else
			{
				if ({18305}.CannonId != -1)
				{
					CannonGameInfo cannonGameInfo = Gameplay.CannonsGameInfo[{18305}.CannonId];
					if (cannonGameInfo.IconTexture != null)
					{
						return new Image({18304}, cannonGameInfo.IconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					}
				}
				if ({18305}.PremiumDays > 0)
				{
					return new Image({18304}, AtlasPortGui.Texture.Tex, new Rectangle(2557, 1261, 64, 64), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				}
				if ({18305}.LegendaryFlagDays.Value > 0)
				{
					return new Image({18304}, CommonAtlas.Texture.Tex, new Rectangle(2467, 379, 68, 70), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				}
				if ({18305}.AllShipsXp.Value > 0)
				{
					return new Image({18304}, CommonAtlas.Texture.Tex, CommonAtlas.shipXpIconBig, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				}
				return new Image({18304}, AtlasGameGui.Texture.Tex, new Rectangle(2647, 394, 47, 47), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00036715 File Offset: 0x00034915
		[CompilerGenerated]
		internal static void <GetPages>g__TryAdd|17_0(string {18307}, TraderOffer[] {18308}, ref List<TraderOffer[]> {18309}, ref {18281}.<>c__DisplayClass17_0 {18310})
		{
			if ({18308}.Length != 0)
			{
				{18310}.titles.Add({18307});
				{18309}.Add({18308});
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00036734 File Offset: 0x00034934
		[CompilerGenerated]
		private string {18311}()
		{
			return this.CurrencyName + ": " + StringHelper.GetValueOfK(this.TotalCurrency);
		}

		// Token: 0x04000616 RID: 1558
		private static readonly Rectangle c_pageItem = new Rectangle(813, 2315, 374, 64);

		// Token: 0x04000617 RID: 1559
		private static readonly Rectangle c_pageItemMaskIcon = new Rectangle(751, 2318, 61, 61);

		// Token: 0x04000618 RID: 1560
		[CompilerGenerated]
		private readonly string {18312};

		// Token: 0x04000619 RID: 1561
		[CompilerGenerated]
		private readonly int {18313};

		// Token: 0x0400061A RID: 1562
		[CompilerGenerated]
		private float {18314};
	}
}
