using System;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001B4 RID: 436
	internal class {19129} : {17625}
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0004DB9B File Offset: 0x0004BD9B
		public CalendarEventInfo EventInfo { get; }

		// Token: 0x060009E7 RID: 2535 RVA: 0x0004DBA4 File Offset: 0x0004BDA4
		public {19129}(CalendarEventInfo {19133}, Rectangle {19134}, params ValueTuple<string, string>[] {19135}) : base(800f, {17625}.c_back1, new {17604}
		{
			AddBackgroundParticles = true
		}, {19129}.titleIcon, new {17625}.DynamicTittle[]
		{
			{19133}.Name
		})
		{
			base.AddChild(new Image(base.Pos.Border(-35f).Offset(0f, 30f), OtherTextures.Images, {19134}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				Opacity = 0.2f
			});
			this.{19145} = {19135};
			this.EventInfo = {19133};
			this.{19136}();
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0004DC44 File Offset: 0x0004BE44
		private void {19136}()
		{
			Form form = this.{19137}();
			Form form2 = this.{19140}(2);
			float num = 70f + form.Pos.WH.Y + 8f;
			float num2 = Math.Max(base.Pos.WH.Y - num - 8f - form2.Pos.WH.Y - 35f, 50f);
			Form form3 = this.{19138}(num2);
			float num3 = num2 - form3.Pos.WH.Y;
			base.AddChildPos(form, PositionAlignment.Center, PositionAlignment.LeftUp, 70f);
			base.AddChildPos(form3, PositionAlignment.Center, PositionAlignment.LeftUp, num + num3 / 2f);
			base.AddChildPos(form2, PositionAlignment.Center, PositionAlignment.RightDown, 35f);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0004DD08 File Offset: 0x0004BF08
		private Form {19137}()
		{
			Form form = new Form(new Marker(0f, 0f, 720f, 0f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			float num = 0f;
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_14, 1f);
			textBlockBuilder.WriteLines(Local.universal_event_desc(this.EventInfo.Days.Length), Color.White, Fonts.Philosopher_14Bold, 720f, new float?(0f));
			TextBlockControl textBlockControl = textBlockBuilder.Create();
			textBlockControl.Pos = new Marker(0f, num, 720f, textBlockControl.Pos.WH.Y);
			form.AddChildPos(textBlockControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp, 50f, num, false);
			num += textBlockControl.Pos.WH.Y + 2f;
			string str = new string(' ', 5) + "☆ ";
			foreach (ValueTuple<string, string> valueTuple in this.{19145})
			{
				string item = valueTuple.Item1;
				string item2 = valueTuple.Item2;
				Label label = new Label(new Vector2(0f, num), Fonts.Philosopher_14, Color.White, str + item, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form.AddChildPos(label, PositionAlignment.LeftUp, PositionAlignment.LeftUp, 50f, num, false);
				if (!string.IsNullOrEmpty(item2))
				{
					Label label2 = new Label(new Vector2(label.Pos.End.X, label.Pos.XY.Y), Fonts.Philosopher_14, Color.Yellow, " [?]", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					form.AddChild(label2);
					label2.ToolTip = new ToolTip(new ToolTipState(item2, "", Array.Empty<ToolTipCharacteristics>()));
				}
				num += label.Size.Y + 2f;
			}
			if (this.EventInfo.GoldfishTrader.Count > 0)
			{
				TextBlockBuilder textBlockBuilder2 = new TextBlockBuilder(Fonts.Philosopher_14, 1f);
				textBlockBuilder2.WriteLines(Local.event_trader_desc_tip, Color.White, Fonts.Philosopher_14Bold, 720f, new float?(0f));
				TextBlockControl textBlockControl2 = textBlockBuilder2.Create();
				textBlockControl2.Pos = new Marker(0f, num, 720f, textBlockControl2.Pos.WH.Y);
				form.AddChildPos(textBlockControl2, PositionAlignment.LeftUp, PositionAlignment.LeftUp, 50f, num, false);
				num += textBlockControl2.Pos.WH.Y + 2f;
			}
			form.Pos = base.Pos.SetHeight(num);
			return form;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0004DFA4 File Offset: 0x0004C1A4
		private Form {19138}(float {19139})
		{
			float num = base.Pos.WH.X - 100f;
			Form form = new Form(new Marker(0f, 0f, num, {19139}), CommonAtlas.transpPixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			int num2 = (int)Math.Ceiling((double)((float)this.EventInfo.Days.Length / 7f));
			float num3 = Math.Min({19139} / (float)num2, num / 7f);
			float num4 = num3 * 0.1f;
			float num5 = num3 * 0.1f;
			num3 -= num4 * (float)(num2 - 1);
			float num6 = 0f;
			float num7 = 0f;
			int num8 = 0;
			foreach (DayInfo dayInfo in this.EventInfo.Days)
			{
				if (num8 != 0 && num8 % 7 == 0)
				{
					num7 += num4 + num3;
					num6 = 0f;
				}
				num8++;
				bool flag = this.EventInfo.IsDayClosed(Session.Account, dayInfo.DayNumber);
				bool flag2 = dayInfo.DayNumber == this.EventInfo.CurrentDayCached;
				Form form2 = new Form(new Marker(num6, num7, num3, num3), flag2 ? {19129}.currentDayIcon : {19129}.dayIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				if (dayInfo.DayNumber > this.EventInfo.CurrentDayCached)
				{
					form2.BasicColor = Color.White * 0.5f;
				}
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted(Local.day);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(dayInfo.DayNumber);
				ToolTip toolTip = new ToolTip(new ToolTipState(defaultInterpolatedStringHandler.ToStringAndClear(), "", Array.Empty<ToolTipCharacteristics>()));
				if (dayInfo.AlwaysVisible || this.EventInfo.CurrentDayCached >= dayInfo.DayNumber)
				{
					toolTip.CurrentContent.Builder.WriteLines(this.{19142}(dayInfo.DayNumber), Color.Wheat, toolTip.CurrentContent.Builder.defaultFont, 300f, new float?(0f));
				}
				form2.ToolTip = toolTip;
				form.AddChild(form2);
				if (flag)
				{
					form2.AddChildPos(new Form(Vector2.Zero, {19129}.mark, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AllowMouseInput = false
					}, PositionAlignment.RightDown, PositionAlignment.LeftUp, 0f);
				}
				num6 += num5 + num3;
			}
			form.AnimatedFocus = false;
			form.Pos = new Marker(ref form.Pos.XY, num6 - num5, num7 + num3);
			return form;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0004E230 File Offset: 0x0004C430
		private Form {19140}(int {19141})
		{
			Form form = new Form(new Marker(0f, 0f, base.Pos.WH.X - 100f, (float)(20 + 40 * {19141})).Border(10f), {19129}.textFrame, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_16, 10f);
			textBlockBuilder.WriteLines(this.{19142}(this.EventInfo.CurrentDayCached), Color.LightGoldenrodYellow, Fonts.Philosopher_16, 680f, new float?(0f));
			form.AddChild(textBlockBuilder.CreateCentroid(form.Pos.Center - new Vector2(0f, textBlockBuilder.Size.Y / 2f - 12f)));
			return form;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0004E30C File Offset: 0x0004C50C
		private string {19142}(int {19143})
		{
			int questDay = CalendarEvents.CurrentEvent.GetQuestDay(Session.Account, {19143});
			return Local.quest + ": " + this.EventInfo.GetQuestNameAndDesc(questDay).Item2;
		}

		// Token: 0x040008E0 RID: 2272
		private static readonly Rectangle mark = new Rectangle(3686, 1218, 32, 32);

		// Token: 0x040008E1 RID: 2273
		private static readonly Rectangle currentDayIcon = new Rectangle(3416, 1150, 100, 100);

		// Token: 0x040008E2 RID: 2274
		private static readonly Rectangle dayIcon = new Rectangle(3518, 1150, 100, 100);

		// Token: 0x040008E3 RID: 2275
		private static readonly Rectangle titleIcon = new Rectangle(3634, 1202, 48, 48);

		// Token: 0x040008E4 RID: 2276
		private static readonly Rectangle textFrame = new Rectangle(2933, 668, 597, 117);

		// Token: 0x040008E5 RID: 2277
		[CompilerGenerated]
		private readonly CalendarEventInfo {19144};

		// Token: 0x040008E6 RID: 2278
		[TupleElementNames(new string[]
		{
			"text",
			"tooltip"
		})]
		private readonly ValueTuple<string, string>[] {19145};
	}
}
