using System;
using System.Collections.Generic;
using Common;
using Common.Game;
using Common.Packets;
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
	// Token: 0x02000276 RID: 630
	internal class {20381} : Form
	{
		// Token: 0x06000E04 RID: 3588 RVA: 0x00075EA4 File Offset: 0x000740A4
		public {20381}(GuildTemporaryEffect.Type {20385}, byte {20386}, bool {20387} = false) : base(new Marker(0f, 0f, 420f, 68f), {20381}.c_upgradeLineLight, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.AnimatedFocus = false;
			string text = "";
			if ({20385} == GuildTemporaryEffect.Type.UpgradeStaff)
			{
				string stringConstants_ = Local.StringConstants_4;
				string stringConstants_2 = Local.StringConstants_5;
				base.AddChild(new Label(12f, 8f, Fonts.Philosopher_14, Color.Black, stringConstants_, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				if (!string.IsNullOrEmpty(text))
				{
					base.AddChild(new Label(369f, 19f, Fonts.Philosopher_14, Color.Black, text, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
				}
				base.AddChild(TextBlockBuilder.CreateBlock(300f, stringConstants_2, Color.Black * 0.7f, Fonts.Arial_10, -3f).Create(Vector2.Zero, 12f, 31f));
				ValueTuple<int, int> valueTuple;
				bool flag = Session.Guild.IsUpgradeAvailable({20385}, {20386}, out valueTuple) && !{20387};
				ValueTuple<int, int> upgradeLevel = Session.Guild.GetUpgradeLevel({20385}, {20386});
				int item = upgradeLevel.Item1;
				int item2 = upgradeLevel.Item2;
				if ((valueTuple.Item2 == 0 && valueTuple.Item1 == 0) || item == item2)
				{
					foreach (GuildTemporaryEffect guildTemporaryEffect in ((IEnumerable<GuildTemporaryEffect>)Session.Guild.Effects))
					{
						if (guildTemporaryEffect.Case == {20385} && guildTemporaryEffect.ArgumentByte == {20386} && guildTemporaryEffect.RemainingTimeSec != -1.0)
						{
							base.AddChild(new Label(new Vector2(350f, 20f), Fonts.Philosopher_14, new Color(157, 102, 0) * 0.55f, StringHelper.TimeD(guildTemporaryEffect.RemainingTimeSec), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
						}
					}
				}
				if (item != item2)
				{
					base.AddChild(new Image(base.Pos.Border(-1f), AtlasPortGui.Texture.Tex, {21684}.c_itemTop, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						Opacity = 0.4f
					});
				}
				if (flag)
				{
					Button button = new Button(base.Pos.XY + new Vector2(345f, 11f), CommonAtlas.plusButtonYellow, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					button.EvClick += delegate(ClickUiEventArgs {20390})
					{
						{20381}.BuyUpgradeInterface({20385}, {20386});
					};
					base.AddChild(button);
					button.ToolTipState = new ToolTipState(Local.nextArenaUpPrice, "", new ToolTipCharacteristics[]
					{
						new ToolTipCharacteristics(Local.conquer_badges, valueTuple.Item2.ToString(), (valueTuple.Item2 <= Session.Guild.ConquerBadges) ? CharacteristicsColor.Lime : CharacteristicsColor.Orange),
						new ToolTipCharacteristics(Local.conquer_ingots, valueTuple.Item1.ToString(), (valueTuple.Item1 <= Session.Guild.Treasury) ? CharacteristicsColor.Lime : CharacteristicsColor.Orange),
						new ToolTipCharacteristics((valueTuple.Item2 <= Session.Guild.ConquerBadges && valueTuple.Item1 <= Session.Guild.Treasury) ? Local.PortRealShopPage_Buy : "", CharacteristicsColor.Wheat)
					});
				}
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00076220 File Offset: 0x00074420
		private static void BuyUpgradeInterface(GuildTemporaryEffect.Type {20388}, byte {20389})
		{
			if (!Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury)
			{
				new {17107}("", Local.GuildWindow_3(Local.guildright_tt_AllowEditGuildAndFortAndTreasury), "", null, true, null, new string[]
				{
					Local.close
				});
				return;
			}
			ValueTuple<int, int> valueTuple;
			if (!Session.Guild.IsUpgradeAvailable({20388}, {20389}, out valueTuple))
			{
				return;
			}
			if (valueTuple.Item2 > Session.Guild.ConquerBadges || valueTuple.Item1 > Session.Guild.Treasury)
			{
				return;
			}
			if ({20388} == GuildTemporaryEffect.Type.UpgradeStaff)
			{
				Global.Network.Send(new OnMakeGuildEffect({20388}, "", {20389}, false));
				return;
			}
			int effectDurationDays = Session.Guild.GetEffectDurationDays({20388}, Session.EventActionsPipeline);
			new {17312}((effectDurationDays == -1) ? Local.GuildWindow_80_b : Local.GuildWindow_80(effectDurationDays), delegate()
			{
				Global.Network.Send(new OnMakeGuildEffect({20388}, "", {20389}, false));
			}, null);
		}

		// Token: 0x04000D24 RID: 3364
		private static readonly Rectangle c_upgradeLineLight = new Rectangle(1712, 3608, 417, 64);
	}
}
