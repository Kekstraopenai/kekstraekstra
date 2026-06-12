using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Steamworks;
using Steamworks.Data;
using TheraEngine;
using World_Of_Sea_Battle;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Interface;

namespace WorldOfSeaBattles
{
	// Token: 0x0200056A RID: 1386
	public static class Steam
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x00121198 File Offset: 0x0011F398
		// (set) Token: 0x06002023 RID: 8227 RVA: 0x0012119F File Offset: 0x0011F39F
		public static bool IsActive { get; private set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x001211A7 File Offset: 0x0011F3A7
		// (set) Token: 0x06002025 RID: 8229 RVA: 0x001211AE File Offset: 0x0011F3AE
		public static bool IsDemo { get; private set; }

		// Token: 0x06002026 RID: 8230 RVA: 0x001211B8 File Offset: 0x0011F3B8
		public static void TryInit()
		{
			bool flag = Debugger.IsAttached && false;
			if (flag && !Debugging.SteamTesting)
			{
				return;
			}
			string path = flag ? "steam_appid.txt" : "appid";
			uint num;
			if (!File.Exists(path) || !uint.TryParse(File.ReadAllText(path), out num))
			{
				Steam.IsActive = false;
				return;
			}
			if (!flag && SteamClient.RestartAppIfNecessary(num))
			{
				Environment.Exit(0);
			}
			SteamClient.Init(num, true);
			Steam.IsActive = true;
			Steam.IsDemo = (num == 3140370U);
			Action<AppId, ulong, bool> value;
			if ((value = Steam.<>O.<0>__OnMicroTxnAuthorizationResponse) == null)
			{
				value = (Steam.<>O.<0>__OnMicroTxnAuthorizationResponse = new Action<AppId, ulong, bool>(Steam.OnMicroTxnAuthorizationResponse));
			}
			SteamUser.OnMicroTxnAuthorizationResponse += value;
			EventInfo @event = typeof(SteamUser).GetEvent("OnGetTicketForWebApiResponse", BindingFlags.Static | BindingFlags.NonPublic);
			Type eventHandlerType = @event.EventHandlerType;
			Type parameterType = eventHandlerType.GetMethod("Invoke").GetParameters()[0].ParameterType;
			ParameterExpression parameterExpression = Expression.Parameter(parameterType, "t");
			FieldInfo field = parameterType.GetField("Result", BindingFlags.Instance | BindingFlags.NonPublic);
			FieldInfo field2 = typeof(Steam).GetField("lastResult", BindingFlags.Static | BindingFlags.NonPublic);
			BinaryExpression body = Expression.Assign(Expression.Field(null, field2), Expression.Field(parameterExpression, field));
			Delegate @delegate = Expression.Lambda(eventHandlerType, body, new ParameterExpression[]
			{
				parameterExpression
			}).Compile();
			@event.GetAddMethod(true).Invoke(null, new object[]
			{
				@delegate
			});
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x00121306 File Offset: 0x0011F506
		private static void OnMicroTxnAuthorizationResponse(AppId {26159}, ulong {26160}, bool {26161})
		{
			if ({26161})
			{
				{21395}.CurrentInstance.WaitingProcessingMode = true;
				Global.Network.Send(new OnSteamOrderMsg({26160}));
				return;
			}
			{21395}.CurrentInstance.WaitingMode = false;
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x00003100 File Offset: 0x00001300
		public static void Update()
		{
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x00121338 File Offset: 0x0011F538
		public static void Shutdown()
		{
			if (Steam.IsActive)
			{
				try
				{
					AuthTicket authTicket = Steam.authTicket;
					if (authTicket != null)
					{
						authTicket.Cancel();
					}
				}
				catch
				{
				}
				try
				{
					SteamClient.Shutdown();
				}
				catch
				{
				}
				Steam.IsActive = false;
			}
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00121390 File Offset: 0x0011F590
		public static SteamId GetSteamID()
		{
			return SteamClient.SteamId;
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00121397 File Offset: 0x0011F597
		public static string GetCurrentGameLanguage()
		{
			return SteamApps.GameLanguage;
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x001213A0 File Offset: 0x0011F5A0
		public static Task<string> GetAuthTicketForWebApi()
		{
			Steam.<GetAuthTicketForWebApi>d__16 <GetAuthTicketForWebApi>d__;
			<GetAuthTicketForWebApi>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<GetAuthTicketForWebApi>d__.<>1__state = -1;
			<GetAuthTicketForWebApi>d__.<>t__builder.Start<Steam.<GetAuthTicketForWebApi>d__16>(ref <GetAuthTicketForWebApi>d__);
			return <GetAuthTicketForWebApi>d__.<>t__builder.Task;
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x001213DC File Offset: 0x0011F5DC
		public static void TestAchievements()
		{
			IEnumerable<Achievement> achievements = SteamUserStats.Achievements;
			foreach (Achievement achievement in achievements)
			{
				AchievementEnum key2;
				if (!Enum.TryParse<AchievementEnum>(achievement.Identifier, true, out key2))
				{
					throw new Exception("Unknown Steam achievement " + achievement.Identifier + ". This achievement removed from game?");
				}
			}
			foreach (KeyValuePair<AchievementEnum, AchievementDisplayInfo> keyValuePair in Gameplay.AchievementsByEnum)
			{
				AchievementEnum key2;
				AchievementDisplayInfo achievementDisplayInfo;
				keyValuePair.Deconstruct(out key2, out achievementDisplayInfo);
				AchievementEnum key = key2;
				if (!achievements.Any((Achievement {26169}) => {26169}.Identifier == key.ToString()))
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(113, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Unknown achievement ");
					defaultInterpolatedStringHandler.AppendFormatted<AchievementEnum>(key);
					defaultInterpolatedStringHandler.AppendLiteral(" in Game. You should add it to Steam https://partner.steamgames.com/apps/achievements/2948190");
					throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
				}
			}
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x001214F8 File Offset: 0x0011F6F8
		public static void GenerateAchievementsLocalization()
		{
			Steam.TestAchievements();
			FieldInfo nameKeyField = typeof(AchievementDisplayInfo).GetField("nameKey", BindingFlags.Instance | BindingFlags.NonPublic);
			FieldInfo descKeyField = typeof(AchievementDisplayInfo).GetField("descKey", BindingFlags.Instance | BindingFlags.NonPublic);
			string[] {494} = (from {26167} in Enum.GetValues<Locale>()
			select Path.Combine("CommonData", {26167}.GetInfo().Item2)).ToArray<string>();
			string[] {495} = Gameplay.AchievementsByEnum.SelectMany((KeyValuePair<AchievementEnum, AchievementDisplayInfo> {26170}) => new string[]
			{
				(string)nameKeyField.GetValue({26170}.Value),
				(string)descKeyField.GetValue({26170}.Value)
			}).ToArray<string>();
			IEnumerable<ValueTuple<string, Dictionary<string, string>>> valuesForAllLangs = Local.GetValuesForAllLangs({494}, {495});
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("\"lang\"");
			stringBuilder.AppendLine("{");
			foreach (ValueTuple<string, Dictionary<string, string>> valueTuple in valuesForAllLangs)
			{
				string item = valueTuple.Item1;
				Dictionary<string, string> item2 = valueTuple.Item2;
				if (item != null)
				{
					int length = item.Length;
					string text;
					if (length != 11)
					{
						if (length != 12)
						{
							goto IL_24D;
						}
						if (!(item == "local_rus.fx"))
						{
							goto IL_24D;
						}
						text = "russian";
					}
					else
					{
						char c = item[7];
						if (c <= 'e')
						{
							if (c != 'a')
							{
								if (c != 'e')
								{
									goto IL_24D;
								}
								if (!(item == "local_de.fx"))
								{
									goto IL_24D;
								}
								text = "german";
							}
							else
							{
								if (!(item == "local_ja.fx"))
								{
									goto IL_24D;
								}
								text = "japanese";
							}
						}
						else if (c != 'h')
						{
							switch (c)
							{
							case 'l':
								if (!(item == "local_pl.fx"))
								{
									goto IL_24D;
								}
								text = "polish";
								break;
							case 'm':
							case 'p':
							case 'q':
								goto IL_24D;
							case 'n':
								if (!(item == "local_en.fx"))
								{
									goto IL_24D;
								}
								text = "english";
								break;
							case 'o':
								if (!(item == "local_ko.fx"))
								{
									goto IL_24D;
								}
								text = "korean";
								break;
							case 'r':
								if (!(item == "local_fr.fx"))
								{
									goto IL_24D;
								}
								text = "french";
								break;
							case 's':
								if (!(item == "local_es.fx"))
								{
									goto IL_24D;
								}
								text = "latam";
								break;
							default:
								goto IL_24D;
							}
						}
						else
						{
							if (!(item == "local_ch.fx"))
							{
								goto IL_24D;
							}
							text = "schinese";
						}
					}
					string value = text;
					StringBuilder stringBuilder2 = stringBuilder;
					StringBuilder stringBuilder3 = stringBuilder2;
					StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(3, 1, stringBuilder2);
					appendInterpolatedStringHandler.AppendLiteral("\t\"");
					appendInterpolatedStringHandler.AppendFormatted(value);
					appendInterpolatedStringHandler.AppendLiteral("\"");
					stringBuilder3.AppendLine(ref appendInterpolatedStringHandler);
					stringBuilder.AppendLine("\t{");
					stringBuilder.AppendLine("\t\t\"Tokens\"");
					stringBuilder.AppendLine("\t\t{");
					int num = 0;
					foreach (KeyValuePair<AchievementEnum, AchievementDisplayInfo> keyValuePair in Gameplay.AchievementsByEnum)
					{
						AchievementEnum achievementEnum;
						AchievementDisplayInfo achievementDisplayInfo;
						keyValuePair.Deconstruct(out achievementEnum, out achievementDisplayInfo);
						int num2 = (int)achievementEnum;
						AchievementDisplayInfo obj = achievementDisplayInfo;
						string key = (string)nameKeyField.GetValue(obj);
						string key2 = (string)descKeyField.GetValue(obj);
						string text2;
						if (item2.TryGetValue(key, out text2))
						{
							text2 = text2.Replace("\"", "\\\"");
						}
						string text3;
						if (item2.TryGetValue(key2, out text3))
						{
							text3 = text3.Replace("\"", "\\\"");
						}
						if (num2 == 59)
						{
							text3 = string.Format(text3, 77);
						}
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 2);
						defaultInterpolatedStringHandler.AppendLiteral("NEW_ACHIEVEMENT_");
						defaultInterpolatedStringHandler.AppendFormatted<int>(1 + num / 32);
						defaultInterpolatedStringHandler.AppendLiteral("_");
						defaultInterpolatedStringHandler.AppendFormatted<int>(num % 32);
						string value2 = defaultInterpolatedStringHandler.ToStringAndClear();
						stringBuilder2 = stringBuilder;
						StringBuilder stringBuilder4 = stringBuilder2;
						appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(13, 2, stringBuilder2);
						appendInterpolatedStringHandler.AppendLiteral("\t\t\t\"");
						appendInterpolatedStringHandler.AppendFormatted(value2);
						appendInterpolatedStringHandler.AppendLiteral("_NAME\"\t\"");
						appendInterpolatedStringHandler.AppendFormatted(text2);
						appendInterpolatedStringHandler.AppendLiteral("\"");
						stringBuilder4.AppendLine(ref appendInterpolatedStringHandler);
						stringBuilder2 = stringBuilder;
						StringBuilder stringBuilder5 = stringBuilder2;
						appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(13, 2, stringBuilder2);
						appendInterpolatedStringHandler.AppendLiteral("\t\t\t\"");
						appendInterpolatedStringHandler.AppendFormatted(value2);
						appendInterpolatedStringHandler.AppendLiteral("_DESC\"\t\"");
						appendInterpolatedStringHandler.AppendFormatted(text3);
						appendInterpolatedStringHandler.AppendLiteral("\"");
						stringBuilder5.AppendLine(ref appendInterpolatedStringHandler);
						num++;
					}
					stringBuilder.AppendLine("\t\t}");
					stringBuilder.AppendLine("\t}");
					continue;
				}
				IL_24D:
				throw new Exception("Unknown lang " + item + ". You should add it to Steam https://partner.steamgames.com/apps/achievements/2948190 and here");
			}
			stringBuilder.AppendLine("}");
			File.WriteAllText("steam_achievements.vdf", stringBuilder.ToString());
			Engine.ShowTextFile("steam_achievements.vdf");
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00121A04 File Offset: 0x0011FC04
		public static void CheckAchievement(AchievementEnum {26162})
		{
			if (!Steam.IsActive)
			{
				return;
			}
			Achievement {26164} = SteamUserStats.Achievements.FirstOrDefault((Achievement {26171}) => {26171}.Identifier == {26162}.ToString());
			if (string.IsNullOrEmpty({26164}.ToString()))
			{
				return;
			}
			Steam.CheckAchievementInternal({26162}, {26164});
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00121A60 File Offset: 0x0011FC60
		public static void CheckAllAchievements()
		{
			if (!Steam.IsActive)
			{
				return;
			}
			try
			{
				foreach (Achievement {26164} in SteamUserStats.Achievements)
				{
					Steam.CheckAchievementInternal(Enum.Parse<AchievementEnum>({26164}.Identifier), {26164});
				}
			}
			catch (NullReferenceException {25356})
			{
				Helpers.SendError({25356}, "CheckAllAchievements", true, false);
			}
			Steam.CheckStats();
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x00121AE4 File Offset: 0x0011FCE4
		private static void CheckAchievementInternal(AchievementEnum {26163}, Achievement {26164})
		{
			if ({26163} == AchievementEnum.RangFive && Session.Account.Rang < 30)
			{
				SteamUserStats.SetStat("PlayerRank", Session.Account.Rang);
				return;
			}
			if (!{26164}.State && Session.Account.Achievements.Count({26163}) > 0)
			{
				{26164}.Trigger(true);
			}
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x00121B40 File Offset: 0x0011FD40
		private static void CheckStats()
		{
			SteamUserStats.SetStat("PlayerRank", Session.Account.Rang);
			SteamUserStats.SetStat("TotalBuildShips", Session.Account.Shipyard.List.Count);
			string name = "Reputation";
			FractionID? mapMyFraction = Session.Game.MapMyFraction;
			FractionID fractionID = FractionID.Pirate;
			SteamUserStats.SetStat(name, (int)((mapMyFraction.GetValueOrDefault() == fractionID & mapMyFraction != null) ? (Session.Account.Reputations.AverageRound * -1f) : Session.Account.Reputations.MaxReputation));
			SteamUserStats.SetStat("Workshops", Session.Account.ResourcesInPorts.CountWorkshops());
			SteamUserStats.SetStat("SailageLegends", Session.Account.Shipyard.SailageLegendsResearchedCount);
			SteamUserStats.SetStat("OpenedChests", Session.Account.OpenedChestsCount);
			SteamUserStats.SetStat("FogOfWar", Session.Account.FogOfWar.DiscoveredProgress() * 100f);
			SteamUserStats.SetStat("InvitedReferals", Session.Account.Analytics.InvitedReferals);
			SteamUserStats.SetStat("DailyQuests", Session.Account.Quests.EnumerateDisables().Count((QuestInfo {26168}) => {26168}.Company == QuestCompany.Daily && {26168}.TreasuryMapID == null && {26168}.CalendarQuestDay == null));
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x00121C98 File Offset: 0x0011FE98
		public static void ResetAchievements()
		{
			SteamUserStats.ResetAll(true);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x00121CA1 File Offset: 0x0011FEA1
		public static void OpenWebUrl(string {26165}, bool {26166} = false)
		{
			SteamFriends.OpenWebOverlay({26165}, {26166});
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x00121CAA File Offset: 0x0011FEAA
		public static string GetCountry()
		{
			return SteamUtils.IpCountry;
		}

		// Token: 0x04001F76 RID: 8054
		private static AuthTicket authTicket;

		// Token: 0x04001F77 RID: 8055
		private static Result lastResult;

		// Token: 0x0200056B RID: 1387
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001F78 RID: 8056
			public static Action<AppId, ulong, bool> <0>__OnMicroTxnAuthorizationResponse;
		}
	}
}
