using System;
using System.Linq;
using TheraEngine.Collections;

namespace TheraEngine
{
	// Token: 0x0200001E RID: 30
	public class EventCounter
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00003EA0 File Offset: 0x000020A0
		public EventCounter(float {11419}, float {11420})
		{
			this.{11423} = {11419};
			this.{11425} = DateTime.UtcNow;
			this.{11424} = {11420};
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003ED0 File Offset: 0x000020D0
		public int Count()
		{
			DateTime utcNow = DateTime.UtcNow;
			this.{11421}(utcNow);
			return this.{11426}.Sum((EventCounter.Event {11431}) => {11431}.Count);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003F18 File Offset: 0x00002118
		public void AddEvent()
		{
			DateTime utcNow = DateTime.UtcNow;
			this.{11421}(utcNow);
			if (this.{11426}.Size > 0 && utcNow.Subtract(this.{11426}.Array[this.{11426}.Size - 1].UtcTime).TotalMinutes < (double)this.{11424})
			{
				EventCounter.Event[] array = this.{11426}.Array;
				int num = this.{11426}.Size - 1;
				array[num].Count = array[num].Count + 1;
				return;
			}
			Tlist<EventCounter.Event> tlist = this.{11426};
			EventCounter.Event @event = new EventCounter.Event(utcNow, 1);
			tlist.Add(@event);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003FB8 File Offset: 0x000021B8
		public void Clean()
		{
			this.{11426}.Clear();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003FC8 File Offset: 0x000021C8
		private void {11421}(in DateTime {11422})
		{
			if ({11422}.Subtract(this.{11425}).TotalMinutes > (double)this.{11424})
			{
				DateTime t = {11422} - TimeSpan.FromMinutes((double)this.{11423});
				int num = 0;
				while (num < this.{11426}.Size && this.{11426}.Array[num].UtcTime < t)
				{
					num++;
				}
				if (num > 0)
				{
					this.{11426}.RemoveRange(0, num);
				}
				this.{11425} = {11422};
			}
		}

		// Token: 0x040000A3 RID: 163
		private readonly float {11423};

		// Token: 0x040000A4 RID: 164
		private readonly float {11424};

		// Token: 0x040000A5 RID: 165
		private DateTime {11425};

		// Token: 0x040000A6 RID: 166
		private Tlist<EventCounter.Event> {11426} = new Tlist<EventCounter.Event>(10);

		// Token: 0x0200001F RID: 31
		private struct Event
		{
			// Token: 0x060000A9 RID: 169 RVA: 0x0000405B File Offset: 0x0000225B
			public Event(DateTime {11429}, int {11430})
			{
				this.UtcTime = {11429};
				this.Count = {11430};
			}

			// Token: 0x040000A7 RID: 167
			public readonly DateTime UtcTime;

			// Token: 0x040000A8 RID: 168
			public int Count;
		}
	}
}
