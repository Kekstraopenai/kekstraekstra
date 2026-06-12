using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Common;
using Common.Account;
using Common.Game;
using Common.Resources;
using ManualPacketSerialization;
using ManualPacketSerialization.Externs;
using TheraEngine;
using TheraEngine.Collections;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000512 RID: 1298
	public class LogbookTrackingNote : IMPSerializable
	{
		// Token: 0x06001D0B RID: 7435 RVA: 0x0010987B File Offset: 0x00107A7B
		public LogbookTrackingNote(LogbookTrackingNote.Type {25448}, params string[] {25449})
		{
			this.{25461} = {25448};
			this.{25462} = new Tlist<string>({25449});
			this.GameVersion = Version.GameVersion.BuildingNumber;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x00003A7C File Offset: 0x00001C7C
		public LogbookTrackingNote()
		{
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x001098A6 File Offset: 0x00107AA6
		public bool IsShipTracking(int {25450})
		{
			return this.{25461} == LogbookTrackingNote.Type.ShipCrafting && this.{25462}[0] == {25450}.ToString();
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x001098CC File Offset: 0x00107ACC
		public bool IsTradingRoutePart(int {25451}, IslePortInfo {25452}, IslePortInfo {25453})
		{
			return this.{25461} == LogbookTrackingNote.Type.SavedTradingRoute && this.{25462}[1] == {25451}.ToString() && this.{25462}[2] == {25452}.PortID.ToString() && this.{25462}[3] == {25453}.PortID.ToString();
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x00109938 File Offset: 0x00107B38
		[return: TupleElementNames(new string[]
		{
			"title",
			"text",
			"showInLeftUp",
			"importantInfoForLogbook"
		})]
		public ValueTuple<string, string, bool, string> GetText()
		{
			LogbookTrackingNote.Type type = this.{25461};
			if (type == LogbookTrackingNote.Type.ShipCrafting)
			{
				PlayerShipInfo playerShipInfo = Gameplay.PlayersInfo[int.Parse(this.{25462}[0])];
				PlayerShipInfo playerShipInfo2 = playerShipInfo;
				Decorator game = Session.Game;
				ref ValueTuple<GSI, RTI, float> craftPrice = playerShipInfo2.GetCraftPrice(game, Session.EventActionsPipeline);
				StringBuilder stringBuilder = new StringBuilder();
				foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)craftPrice.Item1.ResourceInfo))
				{
					if (playerShipInfo.ID == 70)
					{
						stringBuilder.AppendLine(string.Concat(new string[]
						{
							gsilocalEnumerablePair.Info.Name,
							": ",
							Session.Account.TreasuryMaps[72].ToString(),
							" / ",
							gsilocalEnumerablePair.Count.ToString()
						}));
					}
					else
					{
						game = Session.Game;
						if (game.IsInPortOrIsleWithStorage)
						{
							string text = string.Concat(new string[]
							{
								gsilocalEnumerablePair.Info.Name,
								": ",
								Session.Account.NearPortStorage[(int)gsilocalEnumerablePair.Info.ID].ToString(),
								" / ",
								gsilocalEnumerablePair.Count.ToString()
							});
							int num = gsilocalEnumerablePair.Count - Session.Account.NearPortStorage[(int)gsilocalEnumerablePair.Info.ID];
							if (num <= 0)
							{
								text = "✔" + text;
							}
							else
							{
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
								defaultInterpolatedStringHandler.AppendFormatted(text);
								defaultInterpolatedStringHandler.AppendLiteral(" (");
								defaultInterpolatedStringHandler.AppendFormatted(Local.PortСraftShipWindow_25);
								defaultInterpolatedStringHandler.AppendFormatted<int>(num);
								defaultInterpolatedStringHandler.AppendLiteral(")");
								text = defaultInterpolatedStringHandler.ToStringAndClear();
							}
							stringBuilder.AppendLine(text);
						}
						else
						{
							stringBuilder.AppendLine(gsilocalEnumerablePair.Info.Name + ": " + gsilocalEnumerablePair.Count.ToString());
						}
					}
				}
				int value;
				int value2;
				if (playerShipInfo.Coolness == PlayerShipCoolness.Default && Session.Account.Shipyard.IsRankResearched(playerShipInfo.Rank, playerShipInfo.Class, out value, out value2) != ShipResearchStatus.Green)
				{
					StringBuilder stringBuilder2 = stringBuilder;
					StringBuilder stringBuilder3 = stringBuilder2;
					StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(2, 3, stringBuilder2);
					appendInterpolatedStringHandler.AppendFormatted(Local.xp_s);
					appendInterpolatedStringHandler.AppendLiteral(" ");
					appendInterpolatedStringHandler.AppendFormatted<int>(value2);
					appendInterpolatedStringHandler.AppendLiteral("/");
					appendInterpolatedStringHandler.AppendFormatted<int>(value);
					stringBuilder3.AppendLine(ref appendInterpolatedStringHandler);
				}
				if (stringBuilder.Length > 2)
				{
					stringBuilder.Remove(stringBuilder.Length - 2, 2);
				}
				string shipName = playerShipInfo.ShipName;
				string item = stringBuilder.ToString();
				game = Session.Game;
				return new ValueTuple<string, string, bool, string>(shipName, item, game.IsInPortOrIsleWithStorage, string.Empty);
			}
			if (type != LogbookTrackingNote.Type.SavedTradingRoute)
			{
				throw new NotSupportedException();
			}
			IslePortInfo islePortInfo = Gameplay.WorldMap.Ports[int.Parse(this.{25462}[2])];
			IslePortInfo islePortInfo2 = Gameplay.WorldMap.Ports[int.Parse(this.{25462}[3])];
			return new ValueTuple<string, string, bool, string>(Local.gamewiki_trading + " " + islePortInfo2.PortNameShort, Local.clerk_trading_route_logbook(StringHelper.BigValueHelper(int.Parse(this.{25462}[0])), Gameplay.ItemsInfo[int.Parse(this.{25462}[1])].Name, islePortInfo.PortNameShort, islePortInfo2.PortNameShort), true, Local.clerk_trading_route_outdate);
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x00109CF4 File Offset: 0x00107EF4
		public void Boxing(WriterExtern {25454})
		{
			{25454}.WriteByte((byte)this.{25461});
			{25454}.WriteTlist(this.{25462});
			{25454}.WriteStruct<int>(this.GameVersion);
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x00109D1B File Offset: 0x00107F1B
		public void Unboxing(WriterExtern {25455})
		{
			this.{25461} = (LogbookTrackingNote.Type){25455}.ReadByte();
			{25455}.ReadTlist(out this.{25462});
			{25455}.ReadStruct<int>(out this.GameVersion);
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x00109D41 File Offset: 0x00107F41
		public static LogbookTrackingNote CreateShiptracking(int {25456})
		{
			return new LogbookTrackingNote(LogbookTrackingNote.Type.ShipCrafting, new string[]
			{
				{25456}.ToString()
			});
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x00109D5C File Offset: 0x00107F5C
		public static LogbookTrackingNote CreateTradingRouteTracking(ResourceInfo {25457}, int {25458}, IslePortInfo {25459}, IslePortInfo {25460})
		{
			return new LogbookTrackingNote(LogbookTrackingNote.Type.SavedTradingRoute, new string[]
			{
				{25458}.ToString(),
				{25457}.ID.ToString(),
				{25459}.PortID.ToString(),
				{25460}.PortID.ToString()
			});
		}

		// Token: 0x04001CAC RID: 7340
		private LogbookTrackingNote.Type {25461};

		// Token: 0x04001CAD RID: 7341
		private Tlist<string> {25462};

		// Token: 0x04001CAE RID: 7342
		public int GameVersion;

		// Token: 0x02000513 RID: 1299
		public enum Type
		{
			// Token: 0x04001CB0 RID: 7344
			ShipCrafting,
			// Token: 0x04001CB1 RID: 7345
			SavedTradingRoute
		}
	}
}
