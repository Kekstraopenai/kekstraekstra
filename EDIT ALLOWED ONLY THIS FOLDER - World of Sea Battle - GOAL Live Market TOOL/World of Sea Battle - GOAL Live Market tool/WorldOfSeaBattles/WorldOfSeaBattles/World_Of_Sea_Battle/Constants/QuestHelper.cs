using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Constants
{
	// Token: 0x020004B3 RID: 1203
	internal static class QuestHelper
	{
		// Token: 0x06001A54 RID: 6740 RVA: 0x000EA810 File Offset: 0x000E8A10
		public static ValueTuple<Rectangle, string> GetquestPortrait(QuestInfo {24621})
		{
			if ({24621}.Portraits.Size == 0)
			{
				return new ValueTuple<Rectangle, string>(default(Rectangle), "");
			}
			switch ({24621}.Portraits.Array[HashHelper.greaterInt({24621}.ID + CommonGlobal.LoadRandomValue, {24621}.Portraits.Size)])
			{
			case QuestPortrait.Filibuster:
				return new ValueTuple<Rectangle, string>(new Rectangle(184, 1052, 91, 91), Local.StringConstants_162);
			case QuestPortrait.Villager:
				return new ValueTuple<Rectangle, string>(new Rectangle(92, 1144, 91, 91), Local.StringConstants_164);
			case QuestPortrait.TraderMale:
				return new ValueTuple<Rectangle, string>(new Rectangle(0, 1144, 91, 91), Local.StringConstants_163);
			case QuestPortrait.TraderFemale:
				return new ValueTuple<Rectangle, string>(new Rectangle(276, 1052, 91, 91), Local.StringConstants_165);
			case QuestPortrait.Governor:
				return new ValueTuple<Rectangle, string>(new Rectangle(184, 1144, 91, 91), Local.questCharter_governor);
			case QuestPortrait.Officier:
				return new ValueTuple<Rectangle, string>(new Rectangle(92, 1052, 91, 91), Local.StringConstants_161);
			case QuestPortrait.Admiral:
				return new ValueTuple<Rectangle, string>(new Rectangle(92, 1052, 91, 91), Local.questCharter_admiral);
			case QuestPortrait.StartQuestMan:
				return new ValueTuple<Rectangle, string>(new Rectangle(276, 1144, 91, 91), Local.questCharter_magnus);
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x000EA97A File Offset: 0x000E8B7A
		public static IEnumerable<string> GetQuestRistrictions(QuestInfo {24622}, bool {24623} = false)
		{
			QuestHelper.<GetQuestRistrictions>d__1 <GetQuestRistrictions>d__ = new QuestHelper.<GetQuestRistrictions>d__1(-2);
			<GetQuestRistrictions>d__.<>3__q = {24622};
			<GetQuestRistrictions>d__.<>3__importantOnly = {24623};
			return <GetQuestRistrictions>d__;
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000EA991 File Offset: 0x000E8B91
		public static IEnumerable<string> GetQuestFailedRistrictions(QuestInfo {24624})
		{
			QuestHelper.<GetQuestFailedRistrictions>d__2 <GetQuestFailedRistrictions>d__ = new QuestHelper.<GetQuestFailedRistrictions>d__2(-2);
			<GetQuestFailedRistrictions>d__.<>3__q = {24624};
			return <GetQuestFailedRistrictions>d__;
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000EA9A1 File Offset: 0x000E8BA1
		public static IEnumerable<ValueTuple<string, bool>> GetQuestChallenges(QuestInfo {24625}, QuestRunningProgress {24626} = null)
		{
			QuestHelper.<GetQuestChallenges>d__3 <GetQuestChallenges>d__ = new QuestHelper.<GetQuestChallenges>d__3(-2);
			<GetQuestChallenges>d__.<>3__q = {24625};
			<GetQuestChallenges>d__.<>3__progress = {24626};
			return <GetQuestChallenges>d__;
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000EA9B8 File Offset: 0x000E8BB8
		private static string HardnessLevelToStr(int {24627})
		{
			string text = " ";
			for (int i = 0; i < {24627}; i++)
			{
				text += "⚔";
			}
			return text;
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x000EA9E4 File Offset: 0x000E8BE4
		public static ToolTipState GetQuestToolTip(bool {24628}, QuestInfo {24629})
		{
			Composer composer = new Composer(300f, 0f);
			composer.FontStrategy.DefaultTextFont = Fonts.Arial_10;
			composer.FontStrategy.DefaultTextBoldFont = Fonts.Arial_10Bold;
			composer.Space = 0f;
			composer.AddHeader({24629}.GetName(Session.Account) + QuestHelper.HardnessLevelToStr({24629}.HardnessLevel), null);
			composer.AddSpace(5f);
			composer.TextBackgroundPath = new ImageDecription?(new ImageDecription(CommonAtlas.Texture.Tex, new Rectangle(261, 172, 28, 1)));
			int i;
			if (Session.Account.Quests.CheckIsActive({24629}.ID))
			{
				composer.AddText(Local.quest_tt_active, ComposerTextStyle.Blue, true);
			}
			else if (Session.Account.Quests.CheckDisable({24629}.ID, out i))
			{
				composer.AddText(Local.quest_tt_completed, ComposerTextStyle.Blue, true);
			}
			else
			{
				composer.AddText((!{24628}) ? Local.quest_tt_what_to_do1 : Local.quest_tt_what_to_do2, ComposerTextStyle.LimeBold, true);
			}
			if ({24629}.TextToolTipOrNull != null)
			{
				composer.AddSpace(5f);
				ComposerTextStyle {12529} = new ComposerTextStyle(ComposerTextStyle.Wheat.Color * 0.8f, false, null, new Color?(Color.Black * 0.3f));
				composer.AddText({24629}.TextToolTipOrNull, {12529}, true);
				composer.AddSpace(5f);
			}
			string[] array = QuestHelper.GetQuestRistrictions({24629}, false).ToArray<string>();
			string[] source = QuestHelper.GetQuestFailedRistrictions({24629}).ToArray<string>();
			if (array.Length != 0)
			{
				foreach (string text in array)
				{
					composer.AddText("☠ " + text, source.Contains(text) ? ComposerTextStyle.Warning : ComposerTextStyle.WheatBold, true);
				}
			}
			composer.AddSpace(5f);
			composer.AddText(Local.StringConstants_169, ComposerTextStyle.BareWhite, true);
			foreach (string str in QuestHelper.GetQuestRewardsTextWorldMap({24629}))
			{
				composer.AddText("◈ " + str, ComposerTextStyle.BareWhite, true);
			}
			if (Debugging.DebugInfo)
			{
				ComplexBonus bonus = {24629}.GetBonus(Session.Guild, Session.Account);
				Composer composer2 = composer;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
				defaultInterpolatedStringHandler.AppendLiteral("d_gold: ");
				defaultInterpolatedStringHandler.AppendFormatted<RTI>(bonus.Gold);
				composer2.AddText(defaultInterpolatedStringHandler.ToStringAndClear(), ComposerTextStyle.Gray, true);
				Composer composer3 = composer;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(6, 1);
				defaultInterpolatedStringHandler2.AppendLiteral("d_xp: ");
				defaultInterpolatedStringHandler2.AppendFormatted<RTI>(bonus.Xp);
				composer3.AddText(defaultInterpolatedStringHandler2.ToStringAndClear(), ComposerTextStyle.Gray, true);
				Composer composer4 = composer;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(9, 1);
				defaultInterpolatedStringHandler3.AppendLiteral("d_items: ");
				defaultInterpolatedStringHandler3.AppendFormatted<int>(bonus.Items.GetTotalItemsCount());
				composer4.AddText(defaultInterpolatedStringHandler3.ToStringAndClear(), ComposerTextStyle.Gray, true);
			}
			if ({24628})
			{
				composer.AddText(Local.quest_tt_undo, ComposerTextStyle.Wheat, true);
				composer.AddText(Local.click_for_marker, ComposerTextStyle.Wheat, true);
			}
			return new ToolTipState(composer);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000EAD34 File Offset: 0x000E8F34
		public static IEnumerable<string> GetQuestRewardsTextWorldMap(QuestInfo {24630})
		{
			QuestHelper.<GetQuestRewardsTextWorldMap>d__6 <GetQuestRewardsTextWorldMap>d__ = new QuestHelper.<GetQuestRewardsTextWorldMap>d__6(-2);
			<GetQuestRewardsTextWorldMap>d__.<>3__q = {24630};
			return <GetQuestRewardsTextWorldMap>d__;
		}
	}
}
