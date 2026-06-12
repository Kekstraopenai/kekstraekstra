using System;
using Common;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001B2 RID: 434
	internal class {19127}
	{
		// Token: 0x060009E0 RID: 2528 RVA: 0x0004D8F4 File Offset: 0x0004BAF4
		private static {17625} CreateNew(CalendarEventInfo {19128})
		{
			{19129} result;
			switch ({19128}.Type)
			{
			case CalendarEvent.Anniversary:
				result = new {19129}({19128}, new Rectangle(0, 1715, 480, 270), new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>(Local.anniversary_reward_1, null),
					new ValueTuple<string, string>(Local.anniversary_reward_2, null),
					new ValueTuple<string, string>(Local.anniversary_reward_3, null),
					new ValueTuple<string, string>(Local.anniversary_reward_4, Local.anniversary_reward_4_tooltip),
					new ValueTuple<string, string>(Local.anniversary_reward_5, null)
				});
				break;
			case CalendarEvent.Halloween:
				result = new {19129}({19128}, new Rectangle(0, 1715, 480, 270), new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>(Local.halloween_reward_1, null),
					new ValueTuple<string, string>(Local.halloween_reward_2, null),
					new ValueTuple<string, string>(Local.halloween_reward_3, null),
					new ValueTuple<string, string>(Local.halloween_reward_4, Local.halloween_reward_4_tooltip),
					new ValueTuple<string, string>(Local.halloween_reward_5, null)
				});
				break;
			case CalendarEvent.NewYear:
				result = new {19129}({19128}, new Rectangle(0, 1715, 480, 270), new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>(Local.newyear_reward_1, null),
					new ValueTuple<string, string>(Local.newyear_reward_2, null),
					new ValueTuple<string, string>(Local.newyear_reward_3, null),
					new ValueTuple<string, string>(Local.newyear_reward_4, null),
					new ValueTuple<string, string>(Local.newyear_reward_5, Local.newyear_reward_5_tooltip),
					new ValueTuple<string, string>(Local.newyear_reward_6, null)
				});
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0004DAC4 File Offset: 0x0004BCC4
		public static void Open()
		{
			{19127}.Close();
			CalendarEventInfo activeEvent = CalendarEvents.CurrentEvent;
			if (activeEvent.IsActive)
			{
				bool flag = false;
				if (!activeEvent.IsFirstOpened(Session.Account))
				{
					activeEvent.FirstOpen(Session.Account);
					flag = true;
				}
				activeEvent.TryAddCurrentDayReward(Session.Account);
				if (flag)
				{
					new {19197}(activeEvent.FirstOpenText).EvRemoveFromContainer += delegate()
					{
						{19127}.instance = {19127}.CreateNew(activeEvent);
					};
				}
				else
				{
					{19127}.instance = {19127}.CreateNew(activeEvent);
				}
				Global.Network.Send(new OnOpenCalendarEventWindow());
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0004DB78 File Offset: 0x0004BD78
		public static void Close()
		{
			{17625} {17625} = {19127}.instance;
			if ({17625} == null)
			{
				return;
			}
			{17625}.RemoveFromContainer();
		}

		// Token: 0x040008DE RID: 2270
		private static {17625} instance;
	}
}
