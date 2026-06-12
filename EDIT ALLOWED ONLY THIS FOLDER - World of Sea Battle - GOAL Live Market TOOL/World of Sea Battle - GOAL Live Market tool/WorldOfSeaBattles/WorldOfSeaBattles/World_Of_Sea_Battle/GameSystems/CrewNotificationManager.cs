using System;
using System.Collections.Generic;
using Common;
using Common.Data;
using Common.Game;
using Common.Packets;
using Common.Resources;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.GameSystems
{
	// Token: 0x020004C1 RID: 1217
	internal class CrewNotificationManager
	{
		// Token: 0x06001AA6 RID: 6822 RVA: 0x000EE065 File Offset: 0x000EC265
		private static void RunText(params string[] {24754})
		{
			if (CrewNotificationManager.blockText)
			{
				return;
			}
			CrewNotificationManager.blockText = true;
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00003100 File Offset: 0x00001300
		private static void RunTextForce(params string[] {24755})
		{
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x000EE075 File Offset: 0x000EC275
		private static ShipCurrentPlayer player
		{
			get
			{
				return Global.Player;
			}
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x000EE07C File Offset: 0x000EC27C
		public static void Initialize()
		{
			CrewNotificationManager.randomPhrases.Add(EducationHelper.GetSupportPhrases());
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x000EE090 File Offset: 0x000EC290
		public static void RegularUpdate()
		{
			CrewNotificationManager.blockText = false;
			if (!CrewNotificationManager.hasCorpusDamage && CrewNotificationManager.player.UsedShip.FirstHP.BigHoleTime > 0f)
			{
				{19994}.Me({19988}.InfoRed, Local.PlayerShipGui_0, Array.Empty<object>());
				Global.Game.SoundSystem.PlayVoice(VoiceSoundEffect.CorpusHole);
			}
			CrewNotificationManager.hasCorpusDamage = (CrewNotificationManager.player.UsedShip.FirstHP.BigHoleTime > 0f);
			FrameTime frameTime = new FrameTime(1000f, 1f);
			frameTime.EvaluteTimerMs(ref CrewNotificationManager.lowHpVoiceBlocker);
			if (frameTime.EvaluteTimerMs2(ref CrewNotificationManager.planSailVoiceEffect) && CrewNotificationManager.sailVoiceBlocker == 0f)
			{
				Global.Game.SoundSystem.PlayVoice(CrewNotificationManager.player.IsMarchingMode ? VoiceSoundEffect.AllSailes : (Rand.Chanse(50f) ? VoiceSoundEffect.ToWind : VoiceSoundEffect.RaiseSailes));
				CrewNotificationManager.sailVoiceBlocker = 35000f;
			}
			frameTime.EvaluteTimerMs(ref CrewNotificationManager.sailVoiceBlocker);
			if (CrewNotificationManager.player.NowSpeed == 0f)
			{
				CrewNotificationManager.longStayTime += frameTime.msElapsed;
			}
			else
			{
				CrewNotificationManager.longStayTime = 0f;
			}
			if (CrewNotificationManager.player.physicsBody.WindingDot < -0.2f && CrewNotificationManager.player.NowSpeed > 3f && CrewNotificationManager.player.UsedShip.StaticInfo.PaddleModelInstances.Size == 0 && CrewNotificationManager.player.MapInfo.WindEnable)
			{
				CrewNotificationManager.againstWind += frameTime.msElapsed;
			}
			else
			{
				CrewNotificationManager.againstWind = Math.Min(CrewNotificationManager.againstWind, 0f);
			}
			if (CrewNotificationManager.againstWind > 8000f && CrewNotificationManager.againstWind < 10000f)
			{
				CrewNotificationManager.againstWind = -20000f;
			}
			if (CrewNotificationManager.player.IsMarchingMode)
			{
				CrewNotificationManager.speed3walking += frameTime.msElapsed;
			}
			if ((CrewNotificationManager.player.FirstController.LinearStateCode == 1 || CrewNotificationManager.player.FirstController.LinearStateCode == 2) && CrewNotificationManager.player.UsedShip.Cannons.IsReloaded(InGameSightUi.CannonSights.LastActiveNearBoard.GetValueOrDefault(CannonLocation.LeftSide)) && Global.Game.InterestPoints.ShipInSight != null && CrewNotificationManager.speed3walking > 8000f && !CrewNotificationManager.player.IsEntryToPortZoneContains)
			{
				Global.Game.SoundSystem.PlayVoice(Rand.Chanse(50f) ? VoiceSoundEffect.PrepareCannons1 : VoiceSoundEffect.PrepareCannons2);
				CrewNotificationManager.speed3walking = 0f;
			}
			foreach (CrewNotificationManager.RandomPhrase randomPhrase in ((IEnumerable<CrewNotificationManager.RandomPhrase>)CrewNotificationManager.randomPhrases))
			{
				randomPhrase.CurrentTimeoutSec = Math.Max(0f, randomPhrase.CurrentTimeoutSec - 1f);
				if (randomPhrase.CurrentTimeoutSec == 0f && randomPhrase.OccurrenceCriteria())
				{
					if (Rand.Chanse(randomPhrase.Chanse))
					{
						randomPhrase.CurrentTimeoutSec = randomPhrase.TimeoutAfterOccurenceSec;
						CrewNotificationManager.RunText(new string[]
						{
							Rand.Pick<string>(randomPhrase.Phrases)
						});
					}
					else
					{
						randomPhrase.CurrentTimeoutSec = randomPhrase.TimeoutAfterTryingSec;
					}
				}
			}
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x000EE3C0 File Offset: 0x000EC5C0
		public static void ForwardKey()
		{
			if (CrewNotificationManager.longStayTime > 20000f)
			{
				Global.Game.SoundSystem.PlayVoice(VoiceSoundEffect.LiftAnchor);
				return;
			}
			if (!CrewNotificationManager.player.IsMarchingMode)
			{
				CrewNotificationManager.planSailVoiceEffect = 700f;
			}
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x000EE3F8 File Offset: 0x000EC5F8
		public static void BackwardKey()
		{
			if (CrewNotificationManager.planSailVoiceEffect == 0f && CrewNotificationManager.sailVoiceBlocker == 0f && Rand.Chanse(50f))
			{
				Global.Game.SoundSystem.PlayVoice(VoiceSoundEffect.FreeSailes);
				CrewNotificationManager.sailVoiceBlocker = 25000f;
			}
			CrewNotificationManager.planSailVoiceEffect = 0f;
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00003100 File Offset: 0x00001300
		public static void WhenChatMention()
		{
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x000EE450 File Offset: 0x000EC650
		public static void WhenDamageReceive(float {24756})
		{
			if (CrewNotificationManager.player.UsedShip.FirstHP.Summary - {24756} < CrewNotificationManager.player.UsedShip.MaxHp * 0.33f && CrewNotificationManager.lowHpVoiceBlocker == 0f && Global.Game.SoundSystem.PlayVoice(Rand.Chanse(50f) ? VoiceSoundEffect.LowHp1 : VoiceSoundEffect.LowHp2))
			{
				CrewNotificationManager.lowHpVoiceBlocker = 25000f;
			}
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00003100 File Offset: 0x00001300
		public static void WhenFire()
		{
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x000EE4C1 File Offset: 0x000EC6C1
		public static void WhenAmmoChanged(int {24757})
		{
			Global.Game.SoundSystem.PlayVoice(VoiceSoundEffect.Charge);
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x000EE4D5 File Offset: 0x000EC6D5
		public static void WhenLowAmmo()
		{
			Global.Game.SoundSystem.PlayVoice(VoiceSoundEffect.LowAmmo);
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x000EE4E9 File Offset: 0x000EC6E9
		public static void WhenRunQuest(QuestInfo {24758})
		{
			{19779} currentInstance = {19779}.CurrentInstance;
			if (currentInstance == null)
			{
				return;
			}
			currentInstance.UpdateQuestsUi();
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000EE4E9 File Offset: 0x000EC6E9
		public static void WhenQuestProgress(QuestInfo {24759}, QuestRunningProgress {24760}, bool {24761})
		{
			{19779} currentInstance = {19779}.CurrentInstance;
			if (currentInstance == null)
			{
				return;
			}
			currentInstance.UpdateQuestsUi();
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x000EE4FC File Offset: 0x000EC6FC
		public static void WhenFailQuest(QuestInfo {24762})
		{
			{19994}.MeAndLogbook({19988}.Big_Gray, Local.GameAccount_1({24762}.GetName(Session.Account)), null);
			{19779} currentInstance = {19779}.CurrentInstance;
			if (currentInstance == null)
			{
				return;
			}
			currentInstance.UpdateQuestsUi();
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x000EE538 File Offset: 0x000EC738
		public static void WhenCompleteQuest(QuestEngine.CompletedQuestArgs {24763})
		{
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.Money2, 0.03f, 1f);
			if ({24763}.Info.Company != QuestCompany.Education)
			{
				{19994}.MeAndLogbook({19988}.Big_Gray, Local.GameAccount_5({24763}.Info.GetName(Session.Account)), new LBFlags?(LBFlags.L1));
			}
			{19779} currentInstance = {19779}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.UpdateQuestsUi();
			}
			if (!{24763}.Info.ShareRewardWithGoup)
			{
				foreach (ComplexBonus.Annotation annotation in {24763}.Info.GetBonus(Session.Guild, Session.Account).DisplayText({24763}.FinalBonusMultiplier, false))
				{
					if (annotation.Key == ComplexBonus.AnnotationKey.Gold || annotation.Key == ComplexBonus.AnnotationKey.GoldForGuild)
					{
						{19994}.MeAndLogbook({19988}.Gold, annotation.Text, null);
					}
					if (annotation.Key == ComplexBonus.AnnotationKey.Monets)
					{
						{19994}.MeAndLogbook({19988}.Big_Monets, annotation.Text, null);
					}
					if (annotation.Key == ComplexBonus.AnnotationKey.Xp || annotation.Key == ComplexBonus.AnnotationKey.XpForShips)
					{
						{19994}.MeAndLogbook({19988}.GiveScrolls, annotation.Text, null);
					}
					if (annotation.Key == ComplexBonus.AnnotationKey.RandomDesign || annotation.Key == ComplexBonus.AnnotationKey.SpecificDesign)
					{
						{19994}.MeAndLogbook({19988}.GiveScrolls, annotation.Text, null);
					}
					if (annotation.Key == ComplexBonus.AnnotationKey.Cannon)
					{
						{19994}.MeAndLogbook({19988}.GiveResources, annotation.Text, null);
					}
					if (annotation.Key == ComplexBonus.AnnotationKey.Item || annotation.Key == ComplexBonus.AnnotationKey.CraftTime)
					{
						{19994}.MeAndLogbook({19988}.GiveResources, annotation.Text, null);
					}
					if (annotation.Key == ComplexBonus.AnnotationKey.Unit || annotation.Key == ComplexBonus.AnnotationKey.UnknownUnit)
					{
						{19994}.MeAndLogbook({19988}.GiveCrew, annotation.Text, null);
					}
				}
			}
			if ({24763}.Info.Company == QuestCompany.Education && {24763}.Info.HasContinue && Global.Player.IsPortEntry && {19779}.CurrentInstance == null && {19779}.TryGetEducationQuestToShowWhenOpen() != null)
			{
				new {19779}(false, null, null);
			}
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x000EE4E9 File Offset: 0x000EC6E9
		public static void WhenQuestsInteractiveDisablesRemoved()
		{
			{19779} currentInstance = {19779}.CurrentInstance;
			if (currentInstance == null)
			{
				return;
			}
			currentInstance.UpdateQuestsUi();
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000EE758 File Offset: 0x000EC958
		public static void WhenNewDraftOpened()
		{
			{19994}.MeAndLogbook({19988}.Big_Gray, Local.notif_open_nextship(Session.Account.Shipyard.CurrentRealShip.CraftFrom.Class.ToStringLocal(false, false)), null);
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000EE79C File Offset: 0x000EC99C
		public static void WhenKillSomeone(OnFightBonusMsg {24764})
		{
			if (CrewNotificationManager.player.UsedShip.FirstHP.Summary / CrewNotificationManager.player.UsedShip.MaxHp > 0.3f)
			{
				CrewNotificationManager.player.Client.Scene.JoyEffectCrew();
				if (Session.TimeFromLastKill == null || (Session.TimeFromLastKill.Elapsed.TotalSeconds > 40.0 && Rand.Chanse(40f)))
				{
					Global.Game.SoundSystem.PlayVoice(VoiceSoundEffect.WellDone);
				}
				else if (CrewNotificationManager.player.UsedShip.Cannons.Items.Count((CannonCommon {24779}) => !{24779}.IsReloaded) > 0 && Session.TimeFromLastSendedCBDamage != null && Session.TimeFromLastSendedCBDamage.Elapsed.TotalSeconds < 20.0 && CrewNotificationManager.player.UsedShip.HpFactor > 0.1f)
				{
					Global.Game.SoundSystem.PlaySound(GameStaticSoundName.V_Victory, 0.03f, 1f);
				}
			}
			if ({24764}.Bonus.XpBonus > 0)
			{
				{19994}.MeAndLogbook({19988}.GiveScrolls, Local.GameAccount_killXp({24764}.Bonus.XpBonus, {24764}.Name), null);
			}
			if ({24764}.Bonus.GoldBonus > 0)
			{
				{19994}.MeAndLogbook({19988}.Gold, Local.gold + " +" + {24764}.Bonus.GoldBonus.ToString(), null);
			}
			if ({24764}.MarksBonus > 0)
			{
				{19994}.MeAndLogbook({19988}.GiveResources, Local.GameAccount_killMarks + {24764}.MarksBonus.ToString(), null);
			}
			if ({24764}.BonusPirateMonet > 0)
			{
				{19994}.MeAndLogbook({19988}.GiveResources, Local.res_72_name + " +" + {24764}.BonusPirateMonet.ToString(), null);
			}
			string text = "-";
			if ({24764}.KillCase == KillCase.PlayerAlly)
			{
				text = Local.ally.TrimEnd();
			}
			else if ({24764}.KillCase == KillCase.PlayerByGroup)
			{
				text = Local.black_mark_rea_group;
			}
			else if ({24764}.KillCase == KillCase.PlayerShipDiff)
			{
				text = Local.black_mark_rea_diff;
			}
			else if ({24764}.KillCase == KillCase.PlayerNeutral)
			{
				text = Local.black_mark_rea_tf;
			}
			else if ({24764}.KillCase == KillCase.PlayerEvent)
			{
				text = Local.black_mark_rea_event;
			}
			if ({24764}.BlackMark)
			{
				{19994}.MeAndLogbook({19988}.RedFlag, Local.black_mark_received(text), null);
				return;
			}
			if ({24764}.MarksBonus == 0 && {24764}.BonusPirateMonet == 0 && {24764}.Bonus.IsEmpty && !Global.Player.MapInfo.IsEducationMap)
			{
				{19994}.MeAndLogbook({19988}.Info, Local.no_reward({24764}.Name) + " (" + text + ")", null);
			}
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000EEA74 File Offset: 0x000ECC74
		public static void WhenDropGet(DropBonusInfo {24765}, bool {24766}, bool {24767})
		{
			Global.Game.SoundSystem.PlaySound(({24765}.GoldBonus > 0 && {24765}.Resources.IsEmpty) ? GameStaticSoundName.Money1 : GameStaticSoundName.GiveDrop, 0.03f, 1f);
			if ({24765}.BonusXp != 0)
			{
				{19994}.MeAndLogbook({19988}.GiveScrolls, Local.bonus_xp + {24765}.BonusXp.ToString(), null);
			}
			if (!{24765}.MineTrapInside)
			{
				{19994}.Logbook(Local.lbe_given, LBFlags.L0);
			}
			if ({24765}.GoldBonus != 0)
			{
				{19994}.MeAndLogbook({19988}.Gold, Local.gold_plus({24765}.GoldBonus), null);
			}
			foreach (GSILocalEnumerablePair<ResourceInfo> {5413} in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){24765}.Resources.ResourceInfo))
			{
				{19994}.MeAndLogbook({19988}.GiveResources, {5413}.ToStringNC(false), null);
			}
			foreach (GSILocalEnumerablePair<CannonGameInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<CannonGameInfo>>){24765}.Cannons.CannonGameInfo))
			{
				{19994}.MeAndLogbook({19988}.GiveResources, gsilocalEnumerablePair.Info.Name + " +" + gsilocalEnumerablePair.Count.ToString(), null);
			}
			if ({24765}.PowerupItemIDindex != 255)
			{
				{19994}.MeAndLogbook({19988}.GiveScrolls, "+" + Gameplay.PowerupItems.Array[(int){24765}.PowerupItemIDindex].Name, null);
			}
			foreach (GSILocalEnumerablePair<PowderKegInfo> {5413}2 in ((IEnumerable<GSILocalEnumerablePair<PowderKegInfo>>){24765}.PowderKegs.PowderKegInfo))
			{
				{19994}.MeAndLogbook({19988}.GiveResources, {5413}2.ToStringNC(false), null);
			}
			foreach (GSILocalEnumerablePair<CannonBallInfo> {5413}3 in ((IEnumerable<GSILocalEnumerablePair<CannonBallInfo>>){24765}.Ammo.CannonBallInfo))
			{
				{19994}.MeAndLogbook({19988}.GiveResources, {5413}3.ToStringNC(false), null);
			}
			if (!{24765}.Crew.IsEmpty)
			{
				int num = 0;
				foreach (GSILocalEnumerablePair<UnitInfo> gsilocalEnumerablePair2 in ((IEnumerable<GSILocalEnumerablePair<UnitInfo>>){24765}.Crew.UnitInfo))
				{
					if (gsilocalEnumerablePair2.Info.Type == UnitType.Special)
					{
						{19994}.MeAndLogbook({19988}.GiveCrew, gsilocalEnumerablePair2.Info.Name + " " + Local.on_ship, null);
					}
					else
					{
						num++;
					}
				}
				GSI gsi = Global.Player.UsedShip.Crew.AddHelper({24765}.Crew, Global.Player);
				if (gsi.IsEmpty && num > 0)
				{
					{19994}.Me({19988}.Info, Rand.Pick<string>(new string[]
					{
						Local.GameAccount_16a,
						Local.GameAccount_16b
					}), Array.Empty<object>());
				}
				else
				{
					foreach (GSILocalEnumerablePair<UnitInfo> gsilocalEnumerablePair3 in ((IEnumerable<GSILocalEnumerablePair<UnitInfo>>)gsi.UnitInfo))
					{
						{19994}.MeAndLogbook({19988}.GiveCrew, string.Concat(new string[]
						{
							"+",
							gsilocalEnumerablePair3.Count.ToString(),
							" ",
							gsilocalEnumerablePair3.Info.Name,
							" ",
							Local.lb_in_team
						}), null);
					}
				}
			}
			if ({24766})
			{
				{19994}.Me({19988}.Okay, Local.luck, Array.Empty<object>());
			}
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x000EEEA0 File Offset: 0x000ED0A0
		public static void WhenPassingMapWaveBonus(GSI {24768})
		{
			{19994}.Logbook(Local.lbe_wave_passed, LBFlags.L0);
			foreach (GSILocalEnumerablePair<ResourceInfo> {5413} in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){24768}.ResourceInfo))
			{
				{19994}.MeAndLogbook({19988}.GiveResources, {5413}.ToStringNC(false), null);
			}
		}

		// Token: 0x04001962 RID: 6498
		private static bool blockText;

		// Token: 0x04001963 RID: 6499
		private static bool hasCorpusDamage;

		// Token: 0x04001964 RID: 6500
		private static float lowHpVoiceBlocker;

		// Token: 0x04001965 RID: 6501
		private static float planSailVoiceEffect;

		// Token: 0x04001966 RID: 6502
		private static float sailVoiceBlocker;

		// Token: 0x04001967 RID: 6503
		private static float longStayTime;

		// Token: 0x04001968 RID: 6504
		private static float againstWind;

		// Token: 0x04001969 RID: 6505
		private static float speed3walking;

		// Token: 0x0400196A RID: 6506
		private static Tlist<CrewNotificationManager.RandomPhrase> randomPhrases = new Tlist<CrewNotificationManager.RandomPhrase>();

		// Token: 0x020004C2 RID: 1218
		public class RandomPhrase
		{
			// Token: 0x06001ABD RID: 6845 RVA: 0x000EEF20 File Offset: 0x000ED120
			public RandomPhrase(float {24774}, float {24775}, float {24776}, Func<bool> {24777}, params string[] {24778})
			{
				this.Chanse = {24774};
				this.TimeoutAfterTryingSec = {24775};
				this.TimeoutAfterOccurenceSec = {24776};
				this.OccurrenceCriteria = {24777};
				this.Phrases = {24778};
			}

			// Token: 0x0400196B RID: 6507
			public float Chanse;

			// Token: 0x0400196C RID: 6508
			public float TimeoutAfterTryingSec;

			// Token: 0x0400196D RID: 6509
			public float TimeoutAfterOccurenceSec;

			// Token: 0x0400196E RID: 6510
			public Func<bool> OccurrenceCriteria;

			// Token: 0x0400196F RID: 6511
			public string[] Phrases;

			// Token: 0x04001970 RID: 6512
			public float CurrentTimeoutSec;
		}
	}
}
