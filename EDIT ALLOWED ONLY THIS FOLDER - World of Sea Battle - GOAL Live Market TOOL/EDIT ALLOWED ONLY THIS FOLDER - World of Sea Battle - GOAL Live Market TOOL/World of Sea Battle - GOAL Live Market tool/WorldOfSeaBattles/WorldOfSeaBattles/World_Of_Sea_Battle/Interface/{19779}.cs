using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000212 RID: 530
	internal sealed class {19779} : {17625}
	{
		// Token: 0x06000C04 RID: 3076 RVA: 0x00061200 File Offset: 0x0005F400
		[NullableContext(2)]
		public static QuestInfo TryGetEducationQuestToShowWhenOpen()
		{
			QuestInfo[] runningEdQuests = (from {19833} in Session.Account.Quests.ProgressRunningQuests
			where {19833}.Info.Company == QuestCompany.Education
			select {19833} into {19834}
			select {19834}.Info).ToArray<QuestInfo>();
			if (runningEdQuests.Length == 0)
			{
				return null;
			}
			if (runningEdQuests.Length == 1)
			{
				return runningEdQuests[0];
			}
			if (!runningEdQuests.All((QuestInfo {19847}) => {19847}.Group == runningEdQuests.First<QuestInfo>().Group))
			{
				return null;
			}
			return runningEdQuests.First<QuestInfo>();
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000612BC File Offset: 0x0005F4BC
		public {19779}(bool {19783}, EventActionBase {19784} = null, [Nullable(2)] QuestInfo {19785} = null) : base(923f, new Rectangle(1797, 2522, 839, 580), ({19783} || {19785} != null) ? {17604}.InGameWindowBlockShip : {17604}.InGameWindow, Rectangle.Empty, Array.Empty<{17625}.DynamicTittle>())
		{
			EducationHelper.MakeFlag(EducationOnboarding.InteropQuestUnit, false);
			EducationHelper.MakeFlag(EducationOnboarding.OpenWorldMap, false);
			if ({19785} != null)
			{
				this.{19829} = {19785};
			}
			else if ({19783})
			{
				this.{19832} = true;
				Global.Player.ResetSpeedAndRotation();
				this.{19829} = Global.Game.InteractiveWorldSystem.ContainsWorldQuests.First();
			}
			else
			{
				this.{19829} = {19784};
			}
			if (this.{19829} == null)
			{
				this.{19829} = ({19779}.TryGetEducationQuestToShowWhenOpen() ?? this.{19830});
			}
			this.{19826} = new Vector2(85f, 40f);
			this.{19827} = new Vector2(502f, 40f);
			this.{19828} = 360f;
			{19779}.CurrentInstance = this;
			base.EvRemoveFromContainer += this.{19822};
			this.{19786}();
			this.{19789}(this.{19829});
			for (int i = 0; i < Session.Account.ReadNotificationsEventActions.Size; i++)
			{
				int aid = Session.Account.ReadNotificationsEventActions.Array[i];
				if (Session.EventActionsPipeline.CurrentActions.Count((EventActionBase {19848}) => {19848}.AID == aid) == 0)
				{
					Session.Account.ReadNotificationsEventActions.FastRemoveAt(i);
					i--;
				}
			}
			Session.UnreadLogbookFlag = false;
			Session.Account.UpdateEducationFlags(EducationOnboarding.OpenLogbook);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00061468 File Offset: 0x0005F668
		private void {19786}()
		{
			{19779}.<>c__DisplayClass24_0 CS$<>8__locals1 = new {19779}.<>c__DisplayClass24_0();
			CS$<>8__locals1.<>4__this = this;
			if (this.{19824} == null)
			{
				Vector2 vector = this.{19826} + base.Pos.XY + new Vector2(0f, 7f);
				this.{19824} = new Viewbox(new Marker(ref vector, this.{19828} + 10f, 560f), Rectangle.Empty, Rectangle.Empty, Rectangle.Empty, Rectangle.Empty);
			}
			CS$<>8__locals1.stack = new StackForm(this.{19824}.Pos.XY, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			float currentScrollValue = this.{19824}.CurrentScrollValue;
			this.{19824}.Clear();
			this.{19824}.AddItem(new UiControl[]
			{
				CS$<>8__locals1.stack
			});
			base.AddChild(this.{19824});
			CS$<>8__locals1.<LoadStructure>g__CreateHeader|0(Local.last_notes, {19779}.c_ic_records, delegate
			{
				CS$<>8__locals1.<>4__this.{19789}(CS$<>8__locals1.<>4__this.{19830});
			}, () => CS$<>8__locals1.<>4__this.{19829} == CS$<>8__locals1.<>4__this.{19830}, () => false, null);
			if (Session.Account.TreasuryMaps.Any((GSILocalPair {19835}) => {19835}.ID != 35))
			{
				CS$<>8__locals1.<LoadStructure>g__CreateHeader|0(Local.treasury_maps, {19779}.c_ic_maps, delegate
				{
					CS$<>8__locals1.<>4__this.{19789}(CS$<>8__locals1.<>4__this.{19831});
				}, () => CS$<>8__locals1.<>4__this.{19829} == CS$<>8__locals1.<>4__this.{19831}, () => false, null);
			}
			if (Session.EventActionsPipeline.CurrentActions.Size > 0)
			{
				CS$<>8__locals1.stack.AddSpace(26f);
				List<EventActionBase> list = new List<EventActionBase>();
				using (IEnumerator<EventActionBase> enumerator = ((IEnumerable<EventActionBase>)Session.EventActionsPipeline.CurrentActions).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EventActionBase gameEvent = enumerator.Current;
						if (!(gameEvent.Name == Local.removed))
						{
							EABehavior1 eabehavior = gameEvent.Behavior as EABehavior1;
							if (eabehavior != null && eabehavior.IsSeasonEvent)
							{
								list.Add(gameEvent);
							}
							else
							{
								eabehavior = (gameEvent.Behavior as EABehavior1);
								if (eabehavior == null || eabehavior.ShouldShowInLogbook)
								{
									float num = (float)gameEvent.EndDateTimeServer.ToDateTime.Subtract(LocalizedDateTime.NowServerTime).TotalSeconds;
									int days = gameEvent.EndDateTimeServer.ToDateTime.Subtract(gameEvent.StartDateTimeServer.ToDateTime).Days;
									num = Math.Max(60f, num);
									CS$<>8__locals1.<LoadStructure>g__CreateHeader|0(gameEvent.Name, {19779}.c_ic_event, delegate
									{
										CS$<>8__locals1.<>4__this.{19789}(gameEvent);
									}, () => CS$<>8__locals1.<>4__this.{19829} == gameEvent, () => gameEvent.CanHaveUnreadMarker && !Session.Account.ReadNotificationsEventActions.Contains(gameEvent.AID), (days < 30) ? new float?(num) : null);
								}
							}
						}
					}
				}
				using (IEnumerator<EventActionBase> enumerator = ((IEnumerable<EventActionBase>)Session.EventActionsPipeline.WaitActions).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EventActionBase gameEvent = enumerator.Current;
						if (!(gameEvent.Name == Local.removed))
						{
							EABehavior1 eabehavior2 = gameEvent.Behavior as EABehavior1;
							if (eabehavior2 != null && eabehavior2.Category == EActionCaterory.BTender && LocalizedDateTime.NowServerTime.Subtract(gameEvent.StartDateTimeServer.ToDateTime).TotalDays >= 3.0)
							{
								CS$<>8__locals1.<LoadStructure>g__CreateHeader|0(gameEvent.Name, {19779}.c_ic_event, delegate
								{
									CS$<>8__locals1.<>4__this.{19789}(gameEvent);
								}, () => CS$<>8__locals1.<>4__this.{19829} == gameEvent, () => false, null);
							}
						}
					}
				}
				if (list.Count > 0)
				{
					EventActionBase firstEvent = list[0];
					DateTime firstSeasonStartDate = CommonGameConfig.CurrentSettings.FirstSeasonStartDate;
					DateTime toDateTime = firstEvent.EndDateTimeServer.ToDateTime;
					int {278} = this.{19819}(firstSeasonStartDate, toDateTime);
					string name = Local.SeasonX(StringHelper.ToRoman({278}));
					string desc = string.Empty;
					foreach (EventActionBase eventActionBase in list)
					{
						desc += eventActionBase.Description;
						desc = desc + Environment.NewLine + " ";
						desc = desc + Environment.NewLine + " ";
					}
					desc += Environment.NewLine;
					desc = desc + Local.TavernaCommonUi_22 + new LocalizedDateTime(firstEvent.StartDateTimeServer, false).GetDateAndTimeWithoutYear();
					desc += Environment.NewLine;
					desc = desc + Local.TavernaCommonUi_23 + new LocalizedDateTime(firstEvent.EndDateTimeServer, false).GetDateAndTimeWithoutYear();
					CS$<>8__locals1.<LoadStructure>g__CreateHeader|0(name, {19779}.c_ic_event, delegate
					{
						CS$<>8__locals1.<>4__this.LoadRightPage(name, desc, null, "", null, null, null, null, Array.Empty<ValueTuple<string, Action>>());
					}, () => CS$<>8__locals1.<>4__this.{19829} == firstEvent, () => false, null);
				}
			}
			QuestRunningProgress[] array = (from {19836} in Session.Account.Quests.ProgressRunningQuests
			where {19836}.Info.Company == QuestCompany.Education
			select {19836}).ToArray<QuestRunningProgress>();
			QuestRunningProgress[] array2 = (from {19837} in Session.Account.Quests.ProgressRunningQuests
			where {19837}.Info.Company != QuestCompany.Daily && {19837}.Info.Company != QuestCompany.Education
			select {19837}).ToArray<QuestRunningProgress>();
			if (array.Length != 0 || array2.Length != 0)
			{
				CS$<>8__locals1.stack.AddSpace(26f);
				QuestRunningProgress[] array3 = array;
				for (int i = 0; i < array3.Length; i++)
				{
					QuestRunningProgress p = array3[i];
					QuestInfo info = p.Info;
					CS$<>8__locals1.<LoadStructure>g__CreateHeader|0(p.Info.GetName(Session.Account), {19779}.c_ic_quest, delegate
					{
						CS$<>8__locals1.<>4__this.{19789}(p.Info);
					}, () => CS$<>8__locals1.<>4__this.{19829} == p.Info, () => false, null);
				}
				array3 = array2;
				for (int i = 0; i < array3.Length; i++)
				{
					QuestRunningProgress p = array3[i];
					QuestInfo info2 = p.Info;
					CS$<>8__locals1.<LoadStructure>g__CreateHeader|0(p.Info.GetName(Session.Account), {19779}.c_ic_quest, delegate
					{
						CS$<>8__locals1.<>4__this.{19789}(p.Info);
					}, () => CS$<>8__locals1.<>4__this.{19829} == p.Info, () => false, (p.Info.LimitedTimeMinutes <= 0) ? null : new float?(p.TimeSecLeft));
				}
			}
			if (Global.Settings.TrackingNotes.Size > 0)
			{
				CS$<>8__locals1.stack.AddSpace(26f);
				using (IEnumerator<LogbookTrackingNote> enumerator3 = ((IEnumerable<LogbookTrackingNote>)Global.Settings.TrackingNotes).GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						LogbookTrackingNote item = enumerator3.Current;
						ValueTuple<string, string, bool, string> text = item.GetText();
						CS$<>8__locals1.<LoadStructure>g__CreateHeader|0(text.Item1, {19779}.c_ic_records, delegate
						{
							CS$<>8__locals1.<>4__this.{19789}(item);
						}, () => CS$<>8__locals1.<>4__this.{19829} == item, () => false, null);
					}
				}
			}
			if (Global.Game.InteractiveWorldSystem.ContainsWorldQuests.Size > 0)
			{
				CS$<>8__locals1.stack.AddSpace(26f);
				using (IEnumerator<QuestInfo> enumerator4 = ((IEnumerable<QuestInfo>)Global.Game.InteractiveWorldSystem.ContainsWorldQuests).GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						QuestInfo q = enumerator4.Current;
						CS$<>8__locals1.<LoadStructure>g__CreateHeader|0(q.GetName(Session.Account), {19779}.c_ic_records, delegate
						{
							CS$<>8__locals1.<>4__this.{19789}(q);
						}, () => CS$<>8__locals1.<>4__this.{19829} == q, () => false, null);
					}
				}
			}
			if (EducationHelper.DailyQuestsVisible)
			{
				CS$<>8__locals1.stack.AddSpace(26f);
				int currentQuestDay = CalendarEvents.CurrentEvent.GetCurrentQuestDay(Session.Account);
				using (IEnumerator<QuestInfo> enumerator4 = (from {19838} in Gameplay.QuestsInfo
				orderby ({19838}.CalendarQuestDay == null) ? 1 : 0
				select {19838}).GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						QuestInfo q = enumerator4.Current;
						if (GameplayHelper.IsDailyQuestVisible(q) || Session.Account.Quests.TrySearchProgress(q.ID) != null)
						{
							string str = "";
							if ((q.CalendarQuestDay == null || q.CalendarQuestDay.Value == currentQuestDay) && q.LocationPort == null && q.Company != QuestCompany.Education)
							{
								bool flag = Session.Account.Quests.TrySearchProgress(q.ID) != null;
								CS$<>8__locals1.<LoadStructure>g__CreateHeader|0(q.GetName(Session.Account) + str, (q.CalendarQuestDay != null) ? {19779}.c_ic_advent : (flag ? {19779}.c_ic_quest : {19779}.c_ic_daily), delegate
								{
									CS$<>8__locals1.<>4__this.{19789}(q);
								}, () => CS$<>8__locals1.<>4__this.{19829} == q, () => false, null);
							}
						}
					}
				}
			}
			this.{19824}.SetScrollValue(currentScrollValue);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00062114 File Offset: 0x00060314
		private Form {19787}(UiControl {19788})
		{
			Form form = new Form(new Marker(0f, 0f, this.{19828}, {19788}.Pos.WH.Y), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			form.AddChildPos({19788}, PositionAlignment.Center, PositionAlignment.Center, 0f);
			return form;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00062164 File Offset: 0x00060364
		private void {19789}(object {19790})
		{
			this.{19829} = {19790};
			this.{19786}();
			if ({19790} == this.{19831})
			{
				this.{19791}();
			}
			if ({19790} == this.{19830})
			{
				this.{19792}();
				return;
			}
			EventActionBase eventActionBase = {19790} as EventActionBase;
			if (eventActionBase != null)
			{
				{19779}.<>c__DisplayClass26_1 CS$<>8__locals2 = new {19779}.<>c__DisplayClass26_1();
				Session.Account.ReadNotificationsEventActions.AddIfNotContains(eventActionBase.AID);
				string text = eventActionBase.Description;
				EABehavior1 eabehavior = eventActionBase.Behavior as EABehavior1;
				if (eabehavior != null && eabehavior.Category == EActionCaterory.BArenaTournament)
				{
					text = Local.action_arenaTorunament_ingame_desc(eabehavior.ArgumentInt, 3);
				}
				text = text + Environment.NewLine + " ";
				text = text + Environment.NewLine + " ";
				text += Environment.NewLine;
				int num = text.IndexOf("http");
				CS$<>8__locals2.url = "";
				if (num >= 0)
				{
					int num2 = text.IndexOf(' ', num);
					{19779}.<>c__DisplayClass26_1 CS$<>8__locals3 = CS$<>8__locals2;
					string text2 = text;
					int num3 = num;
					CS$<>8__locals3.url = text2.Substring(num3, ((num2 == -1) ? text.Length : num2) - num3);
					text = text.Replace(CS$<>8__locals2.url, "");
				}
				else
				{
					EABehavior1 eabehavior2 = eventActionBase.Behavior as EABehavior1;
					if (eabehavior2 == null || eabehavior2.ShowStartAndEndDateTime)
					{
						text = text + Local.TavernaCommonUi_22 + new LocalizedDateTime(eventActionBase.StartDateTimeServer, false).GetDateAndTimeWithoutYear();
						text += Environment.NewLine;
						text = text + Local.TavernaCommonUi_23 + new LocalizedDateTime(eventActionBase.EndDateTimeServer, false).GetDateAndTimeWithoutYear();
					}
				}
				if (Session.EventActionsPipeline.WaitActions.IndexOf(eventActionBase) != -1)
				{
					text = text + Environment.NewLine + " ";
					text += Environment.NewLine;
					text += Local.TavernaCommonUi_24;
				}
				this.LoadRightPage(eventActionBase.Name, text, null, "", null, null, null, null, Array.Empty<ValueTuple<string, Action>>());
				if (CS$<>8__locals2.url.Length > 0)
				{
					Button button = new Button(Vector2.Zero, {17177}.c_craftBr_Passive, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					button.SetText(Local.url_button, Fonts.Philosopher_14, Color.Black * 0.8f, false);
					button.EvClick += delegate(ClickUiEventArgs {19862})
					{
						Helpers.ExecuteBrowser(CS$<>8__locals2.url, true);
					};
					this.{19825}.AddItem(new UiControl[]
					{
						button
					});
					return;
				}
			}
			else
			{
				QuestInfo q = {19790} as QuestInfo;
				if (q != null)
				{
					string {19794} = q.TextDialog(Session.Account);
					ValueTuple<Rectangle, string> valueTuple = QuestHelper.GetquestPortrait(q);
					string[] limitsFailed = QuestHelper.GetQuestFailedRistrictions(q).ToArray<string>();
					QuestRunningProgress progress = Session.Account.Quests.TrySearchProgress(q.ID);
					Action<CheckboxCheckedEventArgs> <>9__4;
					this.LoadRightPage(q.GetName(Session.Account), {19794}, new Rectangle?(valueTuple.Item1), "", delegate(TextBlockBuilder {19863})
					{
						int num4 = 325;
						string[] array = QuestHelper.GetQuestRistrictions(q, false).ToArray<string>();
						if (array.Length != 0)
						{
							foreach (string text3 in array)
							{
								{19863}.WriteLine("⚔ " + text3, limitsFailed.Contains(text3) ? {19779}.redTextColor : ({19779}.blackTextColor * 1f), Fonts.Philosopher_14Bold);
							}
						}
						ValueTuple<string, bool>[] array3 = QuestHelper.GetQuestChallenges(q, progress).ToArray<ValueTuple<string, bool>>();
						if (array3.Length != 0 && progress != null)
						{
							{19863}.WriteLine(Local.QuestChallenges, {19779}.blackTextColor * 0.6f);
							foreach (ValueTuple<string, bool> valueTuple2 in array3)
							{
								if (progress == null)
								{
									{19863}.WriteLine("✔ " + valueTuple2.Item1, {19779}.blackTextColor * 0.6f);
								}
								else
								{
									{19863}.WriteLine((valueTuple2.Item2 ? "✔" : "x") + " " + valueTuple2.Item1 + (valueTuple2.Item2 ? "" : Local.QuestChallenges_fail), {19779}.blackTextColor * 0.6f);
								}
							}
						}
						QuestEducationFlag questEducationFlag = q.FirstStep as QuestEducationFlag;
						if (questEducationFlag != null)
						{
							{19863}.WriteLine(" ", Color.White);
							foreach (QuestEducationFlag.Item item in ((IEnumerable<QuestEducationFlag.Item>)questEducationFlag.Conditions))
							{
								bool flag = Session.Account.EducationQuest[item.Marker];
								{19863}.WriteLines((flag ? "✔" : "⚔") + " " + QuestStepHeader.GetTextWhatToDo(Global.Player, item, true), flag ? {19779}.greenTextColor : {19779}.blackTextColor, flag ? {19863}.defaultFont : (({19863}.defaultFont == Fonts.Arial_12) ? Fonts.Philosopher_14Bold : Fonts.Philosopher_14Bold), (float)num4, new float?(0f));
								{19863}.WriteSpaceLine(3f);
							}
							if (Session.Account.EducationQuest.HasFlags(questEducationFlag.Mask))
							{
								if (q.Steps.Contains((QuestStepHeader {19839}) => {19839} is QuestFollowToPortHeader))
								{
									{19863}.WriteLines("⚔ " + Local.q_returnPort, {19779}.blackTextColor, Fonts.Philosopher_14Bold, (float)num4, new float?(0f));
									return;
								}
							}
						}
						else if (progress != null)
						{
							{19863}.WriteLines(progress.CurrentStep.GetTextWhatToDo(Global.Player, progress), {19779}.redTextColor * 1.2f, {19863}.defaultFont, (float)num4, new float?(0f));
							QuestTransferOrder questTransferOrder = progress.CurrentStep as QuestTransferOrder;
							if (questTransferOrder != null && Global.Player.UsedShipPlayer.PrivateResourcesOfHold[questTransferOrder.ResourceID] < questTransferOrder.ResourceCount.Value)
							{
								{19863}.WriteLine("! " + Local.TradePortInterface_24 + Global.Player.ResourcesOfHold[(int)questTransferOrder.ResInfo.ID].ToString(), {19779}.redTextColor * 1.3f);
								{19863}.WriteLine("! " + Local.TransferQuestTt, {19779}.redTextColor * 1.3f);
							}
						}
					}, delegate(StackForm {19864})
					{
						if (progress != null)
						{
							UiControl[] array = new UiControl[1];
							int num4 = 0;
							CheckboxControl {12626} = new CheckboxControl(Vector2.Zero, CommonAtlas.checkboxPencil_true, CommonAtlas.checkboxPencil_false, !Global.Settings.DisableTrackingForQuests.Contains(q.ID), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							{
								BasicColor = {19779}.blackTextColor
							}.SetText(Local.track, Fonts.Philosopher_14, {19779}.blackTextColor);
							Action<CheckboxCheckedEventArgs> {12627};
							if (({12627} = <>9__4) == null)
							{
								{12627} = (<>9__4 = delegate(CheckboxCheckedEventArgs {19860})
								{
									if ({19860}.NewValue)
									{
										Global.Settings.DisableTrackingForQuests.Remove(q.ID);
										return;
									}
									Global.Settings.DisableTrackingForQuests.AddIfNotContains(q.ID);
								});
							}
							array[num4] = {12626}.ExCheckEvent({12627});
							{19864}.AddItem(array);
						}
					}, (q.TreasuryMapID != null) ? null : q.GetBonus(Session.Guild, Session.Account), q, Array.Empty<ValueTuple<string, Action>>());
					if (q.Company != QuestCompany.Education)
					{
						StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						Button button2 = new Button(Vector2.Zero, {17177}.c_craftBr_Passive, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						button2.SetText((progress == null) ? ((limitsFailed.Length == 0) ? Local.accept : Local.close) : Local.TavernaCommonUi_7, Fonts.Philosopher_14, Color.Black * 0.8f, false);
						stackForm.AddItem(new UiControl[]
						{
							button2
						});
						int currentQuestDay = CalendarEvents.CurrentEvent.GetCurrentQuestDay(Session.Account);
						if (q.CalendarQuestDay != null)
						{
							int? calendarQuestDay = q.CalendarQuestDay;
							int num3 = currentQuestDay;
							if ((calendarQuestDay.GetValueOrDefault() == num3 & calendarQuestDay != null) && progress != null)
							{
								button2.Opacity = 0.3f;
								button2.ToolTipState = new ToolTipState(Local.EventQuestUndo, "", Array.Empty<ToolTipCharacteristics>());
								goto IL_4B5;
							}
						}
						button2.EvClick += delegate(ClickUiEventArgs {19865})
						{
							if (progress == null)
							{
								if (limitsFailed.Length != 0)
								{
									{19779}.CurrentInstance.BlockAndClose();
									return;
								}
								if (q.CountToLimit && Session.Account.Quests.RunningQuestsCount >= Session.Account.Quests.LimitParallelQuests(Session.Account))
								{
									new {17312}(Local.TavernaCommonUi_16);
									return;
								}
								Global.Network.Send(new OnGetQuestMsg(q.ID, QuestAction.Get, -1, null));
								if (q.Company != QuestCompany.Daily)
								{
									{19779}.CurrentInstance.BlockAndClose();
								}
							}
							else
							{
								if (q.Company == QuestCompany.War && Session.Guild.ConquerBadges == 0)
								{
									new {17312}(Local.GuildWindow_50);
									return;
								}
								QuestTransferOrder questTransferOrder = q.FirstStep as QuestTransferOrder;
								if (questTransferOrder != null)
								{
									if (!Global.Player.IsPortEntry || Global.Player.ResourcesOfHold[questTransferOrder.ResourceID] < questTransferOrder.ResourceCount.Value)
									{
										new {17312}(Local.TavernaUndoTransferQError);
										return;
									}
									Global.Game.ScenePort.MakeAccSync();
									Global.Network.Send(new OnGetQuestMsg(q.ID, QuestAction.Undo, -1, null));
									this.{19789}(this.{19830});
								}
								else
								{
									Global.Network.Send(new OnGetQuestMsg(q.ID, QuestAction.Undo, -1, null));
									this.{19789}(this.{19830});
								}
							}
							{19865}.Sender.AllowMouseInput = false;
						};
						IL_4B5:
						this.{19825}.AddItem(new UiControl[]
						{
							stackForm
						});
						if (progress != null && q.LimitedTimeMinutes != -1)
						{
							this.{19825}.AddItem(new UiControl[]
							{
								new LiveLabel(Vector2.Zero, Fonts.Arial_12, {19779}.redTextColor, null, delegate(object {19866})
								{
									string left_time = Local.left_time;
									string str = StringHelper.TimeMMMSS((double)progress.TimeSecLeft);
									string str2;
									if (progress.TimeSecLeft != 0f)
									{
										str2 = "";
									}
									else
									{
										DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 2);
										defaultInterpolatedStringHandler.AppendLiteral(" (");
										defaultInterpolatedStringHandler.AppendFormatted(Local.reward);
										defaultInterpolatedStringHandler.AppendLiteral(" -");
										defaultInterpolatedStringHandler.AppendFormatted<float>(QuestEngine.FineForFailedTime * 100f, "F0");
										defaultInterpolatedStringHandler.AppendLiteral("%)");
										str2 = defaultInterpolatedStringHandler.ToStringAndClear();
									}
									return left_time + str + str2;
								}, 500)
							});
						}
						if (this.{19832} && Global.Game.InteractiveWorldSystem.ContainsWorldQuests.Contains(q) && progress == null)
						{
							Button button3 = new Button(Vector2.Zero, {17177}.c_craftBr_Passive, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
							button3.SetText(Local.TavernaCommonUi_7, Fonts.Philosopher_14, Color.Black * 0.8f, false);
							stackForm.AddItem(new UiControl[]
							{
								button3
							});
							button3.EvClick += delegate(ClickUiEventArgs {19861})
							{
								Global.Network.Send(new OnGetQuestMsg(q.ID, QuestAction.Undo, -1, null));
								{19779} currentInstance = {19779}.CurrentInstance;
								if (currentInstance == null)
								{
									return;
								}
								currentInstance.BlockAndClose();
							};
							return;
						}
					}
				}
				else
				{
					LogbookTrackingNote note = {19790} as LogbookTrackingNote;
					if (note != null)
					{
						ValueTuple<string, string, bool, string> data = note.GetText();
						this.LoadRightPage(data.Item1, data.Item2, null, "", delegate(TextBlockBuilder {19867})
						{
							if (!string.IsNullOrEmpty(data.Item4))
							{
								{19867}.WriteLines(data.Item4, {19779}.redTextColor, Fonts.Arial_10Bold, 300f, new float?(0f));
							}
						}, delegate(StackForm {19840})
						{
						}, null, null, new ValueTuple<string, Action>[]
						{
							new ValueTuple<string, Action>(Local.PortUpgradeShipWindow_10, delegate()
							{
								Global.Settings.TrackingNotes.Remove(note);
								this.{19789}(this.{19830});
							})
						});
					}
				}
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x000627F4 File Offset: 0x000609F4
		private void {19791}()
		{
			StackForm stackForm = this.{19825};
			if (stackForm != null)
			{
				stackForm.RemoveFromContainer();
			}
			this.{19825} = new StackForm(this.{19827} + base.Pos.XY, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{19807}();
			base.AddChild(this.{19825});
			ResourceInfo[] allMaps = WosbTreasuryMaps.AllMaps;
			for (int i = 0; i < allMaps.Length; i++)
			{
				{19779}.<>c__DisplayClass27_0 CS$<>8__locals1 = new {19779}.<>c__DisplayClass27_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.item = allMaps[i];
				if (CS$<>8__locals1.item.ID != 71 || CalendarEvents.IsNewYear)
				{
					StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					Image image = new Image(new Marker(48f), CS$<>8__locals1.item.IconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					stackForm2.AddItem(new UiControl[]
					{
						image
					});
					{20431}.Set(image, CS$<>8__locals1.item, 0, null);
					StackForm stackForm3 = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					stackForm3.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Philosopher_14Bold, {19779}.blackTextColor, CS$<>8__locals1.item.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					int num = Session.Account.TreasuryMaps[(int)CS$<>8__locals1.item.ID];
					int need2 = WosbTreasuryMaps.CountToComplete((int)CS$<>8__locals1.item.ID);
					int need = need2;
					int num2 = num;
					StackForm stackForm4 = stackForm3;
					UiControl[] array = new UiControl[1];
					int num3 = 0;
					Vector2 zero = Vector2.Zero;
					CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
					Color {13344} = {19779}.blackTextColor;
					string {13345};
					if (need <= 0)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 2);
						defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
						defaultInterpolatedStringHandler.AppendFormatted(Local.pcs);
						{13345} = defaultInterpolatedStringHandler.ToStringAndClear();
					}
					else
					{
						{13345} = Local.treasury_maps_progress(num2, need);
					}
					array[num3] = new Label(zero, philosopher_, {13344}, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					stackForm4.AddItem(array);
					stackForm2.AddSpace(5f);
					stackForm2.AddItem(new UiControl[]
					{
						stackForm3
					});
					if (num2 < need || need <= 0)
					{
						goto IL_292;
					}
					QuestInfo q = WosbTreasuryMaps.GetQuestForMap((int)CS$<>8__locals1.item.ID, Session.Account);
					if (q == null)
					{
						goto IL_292;
					}
					StackForm stackForm5 = {19779}.PenButtonHelper(Local.to_open, delegate
					{
						if (Global.Player.IsPortEntry)
						{
							Global.Game.ScenePort.MakeAccSync();
						}
						GSI treasuryMaps = Session.Account.TreasuryMaps;
						int id = (int)CS$<>8__locals1.item.ID;
						treasuryMaps[id] -= need;
						Global.Network.Send(new OnGetQuestTreasuryMap((int)CS$<>8__locals1.item.ID));
						CS$<>8__locals1.<>4__this.UpdateQuestsUi();
						CS$<>8__locals1.<>4__this.{19829} = q;
					});
					stackForm2.AddSpace(Math.Max(1f, 250f - stackForm2.Pos.WH.X));
					stackForm2.AddItem(new UiControl[]
					{
						stackForm5
					});
					IL_328:
					this.{19825}.AddSpace(10f);
					this.{19825}.AddItem(new UiControl[]
					{
						stackForm2
					});
					goto IL_34E;
					IL_292:
					QuestRunningProgress progress;
					if (Session.Account.Quests.ProgressRunningQuests.TryFind(delegate(QuestRunningProgress {19868})
					{
						int? treasuryMapID = {19868}.Info.TreasuryMapID;
						int id = (int)CS$<>8__locals1.item.ID;
						return treasuryMapID.GetValueOrDefault() == id & treasuryMapID != null;
					}, out progress))
					{
						StackForm stackForm6 = {19779}.PenButtonHelper(Local.to_show, delegate
						{
							CS$<>8__locals1.<>4__this.{19829} = progress.Info;
							CS$<>8__locals1.<>4__this.UpdateQuestsUi();
						});
						stackForm2.AddSpace(Math.Max(1f, 250f - stackForm2.Pos.WH.X));
						stackForm2.AddItem(new UiControl[]
						{
							stackForm6
						});
						goto IL_328;
					}
					goto IL_328;
				}
				IL_34E:;
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00062B5C File Offset: 0x00060D5C
		private void {19792}()
		{
			{19779}.<>c__DisplayClass28_0 CS$<>8__locals1 = new {19779}.<>c__DisplayClass28_0();
			CS$<>8__locals1.<>4__this = this;
			StackForm stackForm = this.{19825};
			if (stackForm != null)
			{
				stackForm.RemoveFromContainer();
			}
			this.{19825} = new StackForm(this.{19827} + base.Pos.XY, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{19807}();
			base.AddChild(this.{19825});
			CS$<>8__locals1.internalViewbox = new Viewbox(new Marker(ref this.{19825}.Pos.XY, this.{19828} + 20f, 530f), Rectangle.Empty, Rectangle.Empty, Rectangle.Empty, Rectangle.Empty);
			CS$<>8__locals1.<LoadNotesPage>g__appendMessages|0(string.Empty);
			CS$<>8__locals1.internalViewbox.ScrollToEnd();
			Form form = this.{19787}(new Form(Vector2.Zero, new Rectangle(96, 115, 46, 12), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false,
				Opacity = 0.3f
			});
			form.IsVisible = false;
			form.UpdateComplete += delegate(UiControl {19870})
			{
				{19870}.IsVisible = (CS$<>8__locals1.internalViewbox.CurrentScrollValue != 0f);
			};
			this.{19825}.AddItem(new UiControl[]
			{
				form
			});
			this.{19825}.AddItem(new UiControl[]
			{
				CS$<>8__locals1.internalViewbox
			});
			TextBox textBox = new TextBox(new Marker(0f, 0f, 300f, (float){17177}.c_textBoxBlack.Height), {17177}.c_textBoxBlack, Fonts.Philosopher_14, {19779}.blackTextColor, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				DefaultText = Local.search
			};
			this.{19825}.AddItem(new UiControl[]
			{
				textBox
			});
			textBox.EvTextChanged += delegate(string {19871})
			{
				CS$<>8__locals1.internalViewbox.Clear();
				base.<LoadNotesPage>g__appendMessages|0({19871});
				CS$<>8__locals1.internalViewbox.ScrollToEnd();
			};
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00062D04 File Offset: 0x00060F04
		private void LoadRightPage(string {19793}, string {19794}, Rectangle? {19795}, string {19796}, Action<TextBlockBuilder> {19797}, Action<StackForm> {19798}, ComplexBonus {19799}, [Nullable(2)] QuestInfo {19800}, params ValueTuple<string, Action>[] {19801})
		{
			StackForm stackForm = this.{19825};
			if (stackForm != null)
			{
				stackForm.RemoveFromContainer();
			}
			this.{19825} = new StackForm(this.{19827} + base.Pos.XY, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{19807}();
			base.AddChild(this.{19825});
			Form form = new Form(new Marker(0f, 0f, this.{19828}, 46f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_18, {19779}.blackTextColor * 1.1f, {19793}, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.Center, 0f);
			this.{19825}.AddItem(new UiControl[]
			{
				form
			});
			string[] array = ((({19795} != null && {19795}.Value.Width > 0) ? "" : "    ") + {19794}.Replace("$", Environment.NewLine + " ")).Split(new string[]
			{
				Environment.NewLine
			}, StringSplitOptions.RemoveEmptyEntries);
			bool flag = {19794}.Length > (({19801}.Length == 0) ? 700 : 500) || {19794}.Contains("$");
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(flag ? Fonts.Arial_12 : Fonts.Philosopher_14, (float)(flag ? -2 : -1));
			if ({19795} != null && {19795}.Value.Width > 0)
			{
				textBlockBuilder.WriteImage(CommonAtlas.Texture.Tex, {19795}.Value, 0.7f, null);
				if (array.Length == 0)
				{
					Helpers.SendError(new Exception("blocks.Length == 0, text: " + {19794} + ", header: " + {19793}), "LoadRightPage", false, false);
				}
				string[] array2 = (array.Length == 0) ? new string[0] : array[0].SplitSmart(new char[]
				{
					' '
				});
				bool flag2 = true;
				for (int i = 0; i < array2.Length; i++)
				{
					if (!flag2 && textBlockBuilder.CurrentLocalPoint.X + textBlockBuilder.defaultFont.Measure(array2[i]).X > this.{19828})
					{
						flag2 = true;
						i--;
						textBlockBuilder.WriteLine("", {19779}.blackTextColor, textBlockBuilder.defaultFont);
					}
					else
					{
						string str = array2[i].EndsWithCjk() ? string.Empty : " ";
						if (!flag2)
						{
							textBlockBuilder.Write(array2[i] + str, {19779}.blackTextColor, textBlockBuilder.defaultFont, null, false);
						}
						else
						{
							textBlockBuilder.Write(array2[i] + str, {19779}.blackTextColor, textBlockBuilder.defaultFont, new float?((textBlockBuilder.Size.Y > (float){19795}.Value.Height * 0.7f + 15f) ? 0f : ((float){19795}.Value.Width * 0.7f + 10f)), false);
						}
						flag2 = false;
					}
				}
				using (IEnumerator<string> enumerator = array.Skip(1).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string {13571} = enumerator.Current;
						textBlockBuilder.WriteLines({13571}, {19779}.blackTextColor, textBlockBuilder.defaultFont, this.{19828}, new float?(0f));
					}
					goto IL_391;
				}
			}
			foreach (string {13571}2 in array)
			{
				textBlockBuilder.WriteLines({13571}2, {19779}.blackTextColor, textBlockBuilder.defaultFont, this.{19828}, new float?(0f));
			}
			IL_391:
			if ({19797} != null)
			{
				{19797}(textBlockBuilder);
			}
			this.{19825}.AddItem(new UiControl[]
			{
				textBlockBuilder.Create()
			});
			if ({19798} != null)
			{
				{19798}(this.{19825});
			}
			this.{19825}.AddSpace(15f);
			if (!string.IsNullOrEmpty({19796}))
			{
				this.{19825}.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.Philosopher_14, {19779}.blackTextColor, {19796}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				this.{19825}.AddSpace(15f);
			}
			if ({19799} != null)
			{
				this.{19825}.AddSpace(5f);
				{19779}.ComplexRewardDisplayHelper({19799}, Session.Account.IsPremium && ({19800} == null || ({19800}.Company != QuestCompany.Education && !{19800}.ShareRewardWithGoup)), this.{19825}, new ComposerTextStyle({19779}.blackTextColor, false, null, null), {19800});
			}
			if ({19801}.Length != 0)
			{
				foreach (ValueTuple<string, Action> valueTuple in {19801})
				{
					this.{19825}.AddItem(new UiControl[]
					{
						{19779}.PenButtonHelper(valueTuple.Item1, valueTuple.Item2)
					});
				}
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x000631F4 File Offset: 0x000613F4
		private static StackForm PenButtonHelper(string {19802}, Action {19803})
		{
			LabelButton labelButton = new LabelButton(Vector2.Zero, {19802}, Fonts.Arial_12, {17177}.textColor, Color.Green, null);
			labelButton.EvClick += delegate(ClickUiEventArgs {19872})
			{
				Action click = {19803};
				if (click == null)
				{
					return;
				}
				click();
			};
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				new Form(Vector2.Zero, {17107}.c_scroll, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				}
			});
			stackForm.AddItem(new UiControl[]
			{
				labelButton
			});
			return stackForm;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00063282 File Offset: 0x00061482
		private IEnumerable<TextBlockBuilder> CreateMessages(float {19804}, float {19805}, IEnumerable<LogbookController.LogbookMessage> {19806})
		{
			{19779}.<CreateMessages>d__31 <CreateMessages>d__ = new {19779}.<CreateMessages>d__31(-2);
			<CreateMessages>d__.<>3__widthLimit = {19804};
			<CreateMessages>d__.<>3__heightLimit = {19805};
			<CreateMessages>d__.<>3__messages = {19806};
			return <CreateMessages>d__;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x000632A0 File Offset: 0x000614A0
		private void {19807}()
		{
			new UiOpacityAnimation(this.{19825}, 0f, 0.7f, 250f);
			new UiOpacityAnimation(this.{19825}, 0.7f, 1f, 600f);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x000632D8 File Offset: 0x000614D8
		protected override void UserUpdate(ref FrameTime {19808})
		{
			if (Global.Settings.kb_OpenLogbook.IsClick)
			{
				this.BlockAndClose();
			}
			base.UserUpdate(ref {19808});
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x000632F8 File Offset: 0x000614F8
		public void UpdateQuestsUi()
		{
			if (this.IsClosedByHand)
			{
				return;
			}
			object {19790} = this.{19829};
			this.{19829} = null;
			this.{19789}({19790});
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00063324 File Offset: 0x00061524
		public void UpdateEventActionsUi()
		{
			if (this.IsClosedByHand)
			{
				return;
			}
			object {19790} = this.{19829};
			this.{19829} = null;
			this.{19789}({19790});
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00063350 File Offset: 0x00061550
		public static void ComplexRewardDisplayHelper(ComplexBonus {19809}, bool {19810}, StackForm {19811}, ComposerTextStyle {19812}, QuestInfo {19813} = null)
		{
			Composer composer = new Composer(320f, 0f);
			composer.FontStrategy.DefaultTextFont = Fonts.Arial_12;
			composer.FontStrategy.DefaultTextBoldFont = Fonts.Arial_12;
			foreach (ComplexBonus.Annotation annotation in {19809}.DisplayText(1f, {19810}))
			{
				string text = annotation.Text;
				Texture2D {19818} = CommonAtlas.Texture.Tex;
				Rectangle {19816} = default(Rectangle);
				if (annotation.Key == ComplexBonus.AnnotationKey.Gold)
				{
					{19816} = CommonAtlas.goldIconMany64;
				}
				else if (annotation.Key == ComplexBonus.AnnotationKey.GoldForGuild)
				{
					{19816} = CommonAtlas.goldIconMany64;
				}
				else if (annotation.Key == ComplexBonus.AnnotationKey.Monets)
				{
					{19816} = CommonAtlas.monetsIcon;
				}
				else if (annotation.Key == ComplexBonus.AnnotationKey.Xp)
				{
					{19816} = CommonAtlas.xpIcon;
				}
				else if (annotation.Key == ComplexBonus.AnnotationKey.XpForShips)
				{
					{19816} = CommonAtlas.shipXpIcon;
				}
				else if (annotation.Key == ComplexBonus.AnnotationKey.ConquerorBadges)
				{
					{19816} = CommonAtlas.conquerorBadgeIcon;
				}
				else if (annotation.Key == ComplexBonus.AnnotationKey.EducationFlag)
				{
					{19816} = new Rectangle(381, 0, 42, 42);
				}
				else if (annotation.Key == ComplexBonus.AnnotationKey.RandomDesign || annotation.Key == ComplexBonus.AnnotationKey.SpecificDesign)
				{
					{19816} = new Rectangle(381, 0, 42, 42);
				}
				else if (annotation.Key == ComplexBonus.AnnotationKey.LegendaryFlag)
				{
					{19816} = new Rectangle(268, 66, 32, 32);
				}
				else if (annotation.Key == ComplexBonus.AnnotationKey.Achievement)
				{
					{19816} = CommonAtlas.doublonesIcon;
				}
				else if (annotation.Key == ComplexBonus.AnnotationKey.UnknownUnit)
				{
					{19816} = CommonAtlas.unknownUnit;
				}
				else if (annotation.Key == ComplexBonus.AnnotationKey.Rank)
				{
					{19816} = CommonAtlas.rewardIcon;
				}
				else if (annotation.Key == ComplexBonus.AnnotationKey.HoldProtection)
				{
					{19816} = CommonAtlas.rewardIcon;
				}
				else if (annotation.Key == ComplexBonus.AnnotationKey.ArenaCurrency)
				{
					{19816} = CommonAtlas.arenaCurrencyIcon;
				}
				else if (annotation.Key == ComplexBonus.AnnotationKey.CraftTime)
				{
					{19816} = CommonAtlas.craftTimeIcon_transp;
				}
				else if (annotation.CustomIcon != null)
				{
					{19818} = annotation.CustomIcon;
					{19816} = annotation.CustomIcon.Bounds;
				}
				{19779}.RewardBlockHelper(composer, text, {19816}, {19812}, {19818});
			}
			if ({19813} != null)
			{
				PlayerReputations.ChangeReputation reputationChangeForQuest = FractionAPI.GetReputationChangeForQuest(Session.Game, {19813});
				if (reputationChangeForQuest.Fraction == FractionID.None && reputationChangeForQuest.Amount != 0f)
				{
					Composer {19814} = composer;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
					defaultInterpolatedStringHandler.AppendFormatted(Local.reputation);
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted(StringHelper.pn(reputationChangeForQuest.Amount));
					defaultInterpolatedStringHandler.AppendLiteral(" (");
					defaultInterpolatedStringHandler.AppendFormatted(Local.fraction_by_choice);
					defaultInterpolatedStringHandler.AppendLiteral(")");
					{19779}.RewardBlockHelper({19814}, defaultInterpolatedStringHandler.ToStringAndClear(), CommonAtlas.xpIcon, {19812}, CommonAtlas.Texture.Tex);
					composer.OpenHorizontal();
					composer.AddSpace(3f);
					string {13304} = (Session.Account.FractionToRepChange == FractionID.None) ? Local.choose_fraction : (Session.Account.FractionToRepChange.GetName() + " (" + Local.click_to_change + ")");
					composer.AddCustom(new LabelButton(Vector2.Zero, {13304}, Fonts.Philosopher_14Bold, Color.Gray, Color.Black, delegate(ClickUiEventArgs {19845})
					{
						new {17107}(Local.choose_fraction, Local.choose_fraction_to_add_rep, string.Empty, delegate(int {19846})
						{
							FractionID fractionID;
							switch ({19846})
							{
							case 0:
								fractionID = FractionID.Antilia;
								break;
							case 1:
								fractionID = FractionID.Espaniol;
								break;
							case 2:
								fractionID = FractionID.KaiAndSeveria;
								break;
							case 3:
								fractionID = FractionID.None;
								break;
							default:
								throw new NotSupportedException();
							}
							FractionID fractionToRepChange = fractionID;
							Session.Account.FractionToRepChange = fractionToRepChange;
							Global.Network.NetClient.Send(new OnFractionToRepChangeSwitch(Session.Account.FractionToRepChange));
							{19779} currentInstance = {19779}.CurrentInstance;
							if (currentInstance == null)
							{
								return;
							}
							currentInstance.UpdateQuestsUi();
						}, false, null, new string[]
						{
							FractionID.Antilia.GetName(),
							FractionID.Espaniol.GetName(),
							FractionID.KaiAndSeveria.GetName(),
							Local.no_fractio_for_rep
						});
					}), PositionAlignment.Center);
					composer.CloseHorizontal();
				}
				else if (reputationChangeForQuest.Amount != 0f)
				{
					Composer {19814}2 = composer;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(5, 4);
					defaultInterpolatedStringHandler2.AppendFormatted(Local.reputation);
					defaultInterpolatedStringHandler2.AppendLiteral(" ");
					defaultInterpolatedStringHandler2.AppendFormatted(reputationChangeForQuest.Fraction.GetName());
					defaultInterpolatedStringHandler2.AppendLiteral(" ");
					defaultInterpolatedStringHandler2.AppendFormatted(StringHelper.pn(reputationChangeForQuest.Amount));
					defaultInterpolatedStringHandler2.AppendLiteral(" (");
					defaultInterpolatedStringHandler2.AppendFormatted(Local.reputation_no_fine);
					defaultInterpolatedStringHandler2.AppendLiteral(")");
					{19779}.RewardBlockHelper({19814}2, defaultInterpolatedStringHandler2.ToStringAndClear(), CommonAtlas.xpIcon, {19812}, CommonAtlas.Texture.Tex);
				}
			}
			{19811}.AddItem(new UiControl[]
			{
				composer.ComposeInStack(null)
			});
			{19811}.AddSpace(10f);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0006378C File Offset: 0x0006198C
		private static void RewardBlockHelper(Composer {19814}, string {19815}, Rectangle {19816}, ComposerTextStyle {19817}, Texture2D {19818} = null)
		{
			CustomSpriteFont philosopher_14Bold = Fonts.Philosopher_14Bold;
			float {12551} = 20f;
			{19814}.OpenHorizontal();
			{19814}.AddSpace(3f);
			{19814}.AddImageAndText(" ", {19818} ?? CommonAtlas.Texture.Tex, {19816}, {12551}, {19817}, false);
			{19814}.AddText({19815}, {19817}, false);
			{19814}.CloseHorizontal();
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x000637EC File Offset: 0x000619EC
		private int {19819}(DateTime {19820}, DateTime {19821})
		{
			DateTime date = {19820}.Date;
			DateTime date2 = {19821}.Date;
			if (date2 < date)
			{
				return 1;
			}
			int num = (date2.Year - date.Year) * 12 + (date2.Month - date.Month);
			if (date2.Day < date.Day)
			{
				num--;
			}
			return num / 3 + 1;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0006395A File Offset: 0x00061B5A
		[CompilerGenerated]
		private void {19822}()
		{
			if (this.IsClosedByHand)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_FirstPortEntryAfterTaverna, true);
			}
			{19779}.CurrentInstance = null;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00063974 File Offset: 0x00061B74
		[CompilerGenerated]
		internal static Form <LoadNotesPage>g__CreateReputationProgressBar|28_5(FractionID {19823})
		{
			Relation relation = Session.Account.Reputations.NeutralStatusWith({19823});
			Color {13186} = (relation == Relation.Neutral) ? new Color(199, 181, 148) : (((relation == Relation.Ally) ? new Color(160, 190, 133) : new Color(240, 133, 127)) * 0.5f);
			Color {13186}2 = (relation == Relation.Neutral) ? new Color(86, 75, 63) : ((relation == Relation.Ally) ? new Color(102, 117, 88) : new Color(139, 66, 66));
			Rectangle rectangle = new Rectangle(2176, 3140, 120, 14);
			int num = 140;
			Form form = new Form(new Marker(0f, 0f, 420f, 10f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			Label label = new Label(Vector2.Zero, Fonts.Philosopher_14, {19779}.blackTextColor, {19823}.GetName() + ": ", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChild(label);
			float {11527} = label.Pos.WH.Y / 2f - (float)(rectangle.Height / 2);
			form.AddChild(new Form(new Marker((float)num, {11527}, (float)rectangle.Width, (float)rectangle.Height), rectangle, {13186}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			float num2 = Session.Account.Reputations[{19823}];
			float num3 = 0.005f * (100f + num2);
			form.AddChild(new Form(new Marker((float)num, {11527}, (float)rectangle.Width * num3, (float)rectangle.Height), rectangle.SetWidth((float)rectangle.Width * num3), {13186}2, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			int num4 = num + rectangle.Width;
			form.AddChild(new Label(new Vector2((float)(num4 + 12), 0f), Fonts.Philosopher_14Bold, {19779}.blackTextColor, MathF.Round(num2).ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Label(new Vector2((float)(num4 + 47), 0f), Fonts.Philosopher_14, {19779}.blackTextColor, (relation == Relation.Ally) ? Local.friend_fraction : ((relation == Relation.Enemy) ? Local.enemy_fraction : Local.neutral_fraction), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			return form;
		}

		// Token: 0x04000B0A RID: 2826
		public static Rectangle c_paperHeader = new Rectangle(2249, 2360, 565, 48);

		// Token: 0x04000B0B RID: 2827
		public static bool DisablaeTrackingForDaily = true;

		// Token: 0x04000B0C RID: 2828
		public static {19779} CurrentInstance;

		// Token: 0x04000B0D RID: 2829
		private static readonly Color blackTextColor = Color.Black * 0.60784316f;

		// Token: 0x04000B0E RID: 2830
		private static readonly Color redTextColor = new Color(120, 25, 25);

		// Token: 0x04000B0F RID: 2831
		private static readonly Color greenTextColor = new Color(58, 122, 58);

		// Token: 0x04000B10 RID: 2832
		private static readonly Rectangle c_ic_records = new Rectangle(2098, 3103, 18, 18);

		// Token: 0x04000B11 RID: 2833
		private static readonly Rectangle c_ic_maps = new Rectangle(2098, 3122, 18, 18);

		// Token: 0x04000B12 RID: 2834
		private static readonly Rectangle c_ic_event = new Rectangle(2117, 3103, 18, 18);

		// Token: 0x04000B13 RID: 2835
		private static readonly Rectangle c_ic_daily = new Rectangle(2136, 3103, 18, 18);

		// Token: 0x04000B14 RID: 2836
		private static readonly Rectangle c_ic_guild = new Rectangle(2155, 3103, 18, 18);

		// Token: 0x04000B15 RID: 2837
		private static readonly Rectangle c_ic_quest = new Rectangle(2174, 3103, 18, 18);

		// Token: 0x04000B16 RID: 2838
		private static readonly Rectangle c_ic_advent = new Rectangle(2117, 3122, 18, 18);

		// Token: 0x04000B17 RID: 2839
		private Viewbox {19824};

		// Token: 0x04000B18 RID: 2840
		private StackForm {19825};

		// Token: 0x04000B19 RID: 2841
		private Vector2 {19826};

		// Token: 0x04000B1A RID: 2842
		private Vector2 {19827};

		// Token: 0x04000B1B RID: 2843
		private float {19828};

		// Token: 0x04000B1C RID: 2844
		private object {19829};

		// Token: 0x04000B1D RID: 2845
		private readonly object {19830} = new object();

		// Token: 0x04000B1E RID: 2846
		private readonly object {19831} = new object();

		// Token: 0x04000B1F RID: 2847
		private bool {19832};
	}
}
