using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x0200050A RID: 1290
	public static class GuildTitleExtenstions
	{
		// Token: 0x06001CD5 RID: 7381 RVA: 0x00108408 File Offset: 0x00106608
		public static Image GetSmallIcon(this GuildTitle {25381})
		{
			ValueTuple<Rectangle, Rectangle> valueTuple;
			if (GuildTitleExtenstions.icons.TryGetValue({25381}.IconName, out valueTuple))
			{
				return new Image(GuildTitleExtenstions.smallIconDefaultMarker, OtherTextures.FractionIcons, valueTuple.Item2, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					InheritOpacity = true
				};
			}
			return null;
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0010844C File Offset: 0x0010664C
		public static Image GetBigIcon(this GuildTitle {25382})
		{
			ValueTuple<Rectangle, Rectangle> valueTuple;
			if (GuildTitleExtenstions.icons.TryGetValue({25382}.IconName, out valueTuple))
			{
				return new Image(GuildTitleExtenstions.bigIconDefaultMarker, OtherTextures.FractionIcons, valueTuple.Item1, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					InheritOpacity = true
				};
			}
			return null;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x00108490 File Offset: 0x00106690
		// Note: this type is marked as 'beforefieldinit'.
		static GuildTitleExtenstions()
		{
			Dictionary<string, ValueTuple<Rectangle, Rectangle>> dictionary = new Dictionary<string, ValueTuple<Rectangle, Rectangle>>();
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted<FractionID>(FractionID.Pirate);
			defaultInterpolatedStringHandler.AppendLiteral("_");
			defaultInterpolatedStringHandler.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Gold);
			dictionary.Add(defaultInterpolatedStringHandler.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(0, 0, 512, 512), new Rectangle(0, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler2.AppendFormatted<FractionID>(FractionID.Pirate);
			defaultInterpolatedStringHandler2.AppendLiteral("_");
			defaultInterpolatedStringHandler2.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Silver);
			dictionary.Add(defaultInterpolatedStringHandler2.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(0, 513, 512, 512), new Rectangle(65, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler3.AppendFormatted<FractionID>(FractionID.Pirate);
			defaultInterpolatedStringHandler3.AppendLiteral("_");
			defaultInterpolatedStringHandler3.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Bronze);
			dictionary.Add(defaultInterpolatedStringHandler3.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(0, 1026, 512, 512), new Rectangle(130, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler4.AppendFormatted<FractionID>(FractionID.Pirate);
			defaultInterpolatedStringHandler4.AppendLiteral("_");
			defaultInterpolatedStringHandler4.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Copper);
			dictionary.Add(defaultInterpolatedStringHandler4.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(0, 1539, 512, 512), new Rectangle(195, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler5 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler5.AppendFormatted<FractionID>(FractionID.TradeUnion);
			defaultInterpolatedStringHandler5.AppendLiteral("_");
			defaultInterpolatedStringHandler5.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Gold);
			dictionary.Add(defaultInterpolatedStringHandler5.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(513, 0, 512, 512), new Rectangle(513, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler6 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler6.AppendFormatted<FractionID>(FractionID.TradeUnion);
			defaultInterpolatedStringHandler6.AppendLiteral("_");
			defaultInterpolatedStringHandler6.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Silver);
			dictionary.Add(defaultInterpolatedStringHandler6.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(513, 513, 512, 512), new Rectangle(578, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler7 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler7.AppendFormatted<FractionID>(FractionID.TradeUnion);
			defaultInterpolatedStringHandler7.AppendLiteral("_");
			defaultInterpolatedStringHandler7.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Bronze);
			dictionary.Add(defaultInterpolatedStringHandler7.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(513, 1026, 512, 512), new Rectangle(643, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler8 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler8.AppendFormatted<FractionID>(FractionID.TradeUnion);
			defaultInterpolatedStringHandler8.AppendLiteral("_");
			defaultInterpolatedStringHandler8.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Copper);
			dictionary.Add(defaultInterpolatedStringHandler8.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(513, 1539, 512, 512), new Rectangle(708, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler9 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler9.AppendFormatted<FractionID>(FractionID.Antilia);
			defaultInterpolatedStringHandler9.AppendLiteral("_");
			defaultInterpolatedStringHandler9.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Gold);
			dictionary.Add(defaultInterpolatedStringHandler9.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(1026, 0, 512, 512), new Rectangle(1026, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler10 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler10.AppendFormatted<FractionID>(FractionID.Antilia);
			defaultInterpolatedStringHandler10.AppendLiteral("_");
			defaultInterpolatedStringHandler10.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Silver);
			dictionary.Add(defaultInterpolatedStringHandler10.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(1026, 513, 512, 512), new Rectangle(1091, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler11 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler11.AppendFormatted<FractionID>(FractionID.Antilia);
			defaultInterpolatedStringHandler11.AppendLiteral("_");
			defaultInterpolatedStringHandler11.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Bronze);
			dictionary.Add(defaultInterpolatedStringHandler11.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(1026, 1026, 512, 512), new Rectangle(1156, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler12 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler12.AppendFormatted<FractionID>(FractionID.Antilia);
			defaultInterpolatedStringHandler12.AppendLiteral("_");
			defaultInterpolatedStringHandler12.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Copper);
			dictionary.Add(defaultInterpolatedStringHandler12.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(1026, 1539, 512, 512), new Rectangle(1221, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler13 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler13.AppendFormatted<FractionID>(FractionID.Espaniol);
			defaultInterpolatedStringHandler13.AppendLiteral("_");
			defaultInterpolatedStringHandler13.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Gold);
			dictionary.Add(defaultInterpolatedStringHandler13.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(1539, 0, 512, 512), new Rectangle(1539, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler14 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler14.AppendFormatted<FractionID>(FractionID.Espaniol);
			defaultInterpolatedStringHandler14.AppendLiteral("_");
			defaultInterpolatedStringHandler14.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Silver);
			dictionary.Add(defaultInterpolatedStringHandler14.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(1539, 513, 512, 512), new Rectangle(1604, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler15 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler15.AppendFormatted<FractionID>(FractionID.Espaniol);
			defaultInterpolatedStringHandler15.AppendLiteral("_");
			defaultInterpolatedStringHandler15.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Bronze);
			dictionary.Add(defaultInterpolatedStringHandler15.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(1539, 1026, 512, 512), new Rectangle(1669, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler16 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler16.AppendFormatted<FractionID>(FractionID.Espaniol);
			defaultInterpolatedStringHandler16.AppendLiteral("_");
			defaultInterpolatedStringHandler16.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Copper);
			dictionary.Add(defaultInterpolatedStringHandler16.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(1539, 1539, 512, 512), new Rectangle(1734, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler17 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler17.AppendFormatted<FractionID>(FractionID.KaiAndSeveria);
			defaultInterpolatedStringHandler17.AppendLiteral("_");
			defaultInterpolatedStringHandler17.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Gold);
			dictionary.Add(defaultInterpolatedStringHandler17.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(2052, 0, 512, 512), new Rectangle(2052, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler18 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler18.AppendFormatted<FractionID>(FractionID.KaiAndSeveria);
			defaultInterpolatedStringHandler18.AppendLiteral("_");
			defaultInterpolatedStringHandler18.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Silver);
			dictionary.Add(defaultInterpolatedStringHandler18.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(2052, 513, 512, 512), new Rectangle(2117, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler19 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler19.AppendFormatted<FractionID>(FractionID.KaiAndSeveria);
			defaultInterpolatedStringHandler19.AppendLiteral("_");
			defaultInterpolatedStringHandler19.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Bronze);
			dictionary.Add(defaultInterpolatedStringHandler19.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(2052, 1026, 512, 512), new Rectangle(2182, 2052, 64, 64)));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler20 = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler20.AppendFormatted<FractionID>(FractionID.KaiAndSeveria);
			defaultInterpolatedStringHandler20.AppendLiteral("_");
			defaultInterpolatedStringHandler20.AppendFormatted<GuildTitlePlace>(GuildTitlePlace.Copper);
			dictionary.Add(defaultInterpolatedStringHandler20.ToStringAndClear(), new ValueTuple<Rectangle, Rectangle>(new Rectangle(2052, 1539, 512, 512), new Rectangle(2247, 2052, 64, 64)));
			GuildTitleExtenstions.icons = dictionary;
			Vector2 zero = Vector2.Zero;
			GuildTitleExtenstions.smallIconDefaultMarker = new Marker(ref zero, 20f, 20f);
			zero = Vector2.Zero;
			GuildTitleExtenstions.bigIconDefaultMarker = new Marker(ref zero, 512f, 512f);
		}

		// Token: 0x04001C83 RID: 7299
		[TupleElementNames(new string[]
		{
			"big",
			"small"
		})]
		private static Dictionary<string, ValueTuple<Rectangle, Rectangle>> icons;

		// Token: 0x04001C84 RID: 7300
		private static Marker smallIconDefaultMarker;

		// Token: 0x04001C85 RID: 7301
		private static Marker bigIconDefaultMarker;
	}
}
