using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Constants;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000271 RID: 625
	internal class {20338} : Form
	{
		// Token: 0x06000DE1 RID: 3553 RVA: 0x00074E9C File Offset: 0x0007309C
		public {20338}(FractionID {20343}, GuildQuests {20344}, float {20345}, float {20346}) : base(new Marker(0f, 0f, {20346}, 0f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.AnimatedFocus = false;
			this.{20359} = {20343};
			this.{20360} = {20344};
			this.{20361} = {20345};
			{20338}.<>c__DisplayClass5_0 CS$<>8__locals1;
			CS$<>8__locals1.pairList = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.itemPair = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			foreach (GuildQuestAction {20348} in GuildQuests.GetQuestsWithAction(new FractionID?({20343})))
			{
				CS$<>8__locals1.itemPair.AddItem(new UiControl[]
				{
					this.{20347}({20348})
				});
				{20338}.<.ctor>g__HandleCurrentItemPair|5_0(ref CS$<>8__locals1);
			}
			foreach (GuildQuestProgression {20350} in GuildQuests.GetQuestsWithProgress(new FractionID?({20343})))
			{
				CS$<>8__locals1.itemPair.AddItem(new UiControl[]
				{
					this.{20349}({20350})
				});
				{20338}.<.ctor>g__HandleCurrentItemPair|5_0(ref CS$<>8__locals1);
			}
			if (CS$<>8__locals1.itemPair.GetChildren.Size > 0)
			{
				CS$<>8__locals1.pairList.AddItem(new UiControl[]
				{
					CS$<>8__locals1.itemPair
				});
			}
			base.AddChild(CS$<>8__locals1.pairList);
			base.Pos = base.Pos.SetHeight(CS$<>8__locals1.pairList.Pos.WH.Y);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00075040 File Offset: 0x00073240
		private Form {20347}(GuildQuestAction {20348})
		{
			Form form = this.{20354}();
			int num = this.{20360}[{20348}];
			float reward = GuildQuests.GetReward({20348}, this.{20359}, 1, 1);
			float reward2 = GuildQuests.GetReward({20348}, this.{20359}, 2, 3);
			float reward3 = GuildQuests.GetReward({20348}, this.{20359}, 3, 5);
			string text;
			if (reward != reward3)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
				defaultInterpolatedStringHandler.AppendFormatted<float>(reward);
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted<float>(reward2);
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted<float>(reward3);
				text = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			else
			{
				text = reward.ToString();
			}
			string text2 = text;
			Form form2 = this.{20355}({20348}.GetDescription(this.{20362}.Contains({20348}) ? "" : (text2 + ((reward != reward3) ? (" " + Local.level_city_dep) : ""))));
			Label label = {20338}.MakeItemHeader({20348}.GetName());
			form2.AddChild(label);
			Color {13344} = (reward < 0f) ? new Color(255, 208, 191) : new Color(189, 255, 186);
			Label label2 = new Label(Vector2.Zero, Fonts.Philosopher_12, {13344}, ((reward < 0f) ? "" : "+") + text2.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			UiControl uiControl = label2;
			Vector2 vector = new Vector2(label.Pos.XY.X + label.Pos.WH.X + 10f, 14f);
			uiControl.Pos = new Marker(ref vector, ref label2.Pos.WH);
			form2.AddChild(label2);
			int num2 = num % 10;
			form2.AddChild(new Label(new Vector2(label.Pos.XY.X, 32f), Fonts.Arial_10, Color.White * 0.6f, (num2 >= 2 && num2 <= 4) ? Local.quest_got_n_times_2_4(num) : Local.quest_got_n_times_1(num), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChildPos(form2, PositionAlignment.Center, PositionAlignment.Center, 0f);
			return form;
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00075270 File Offset: 0x00073470
		private Form {20349}(GuildQuestProgression {20350})
		{
			Form form = this.{20354}();
			Form form2 = this.{20355}({20350}.GetDescription());
			ValueTuple<int, int> valueTuple = this.{20360}[{20350}];
			int item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			int requiredAmountFor = GuildQuests.GetRequiredAmountFor({20350}, this.{20360});
			int reward = GuildQuests.GetReward({20350});
			Label label = {20338}.MakeItemHeader({20350}.GetName());
			form2.AddChild(label);
			Color {13344} = new Color(189, 255, 186);
			Label label2 = new Label(Vector2.Zero, Fonts.Philosopher_12, {13344}, "+" + reward.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			UiControl uiControl = label2;
			Vector2 vector = new Vector2(label.Pos.XY.X + label.Pos.WH.X + 10f, 14f);
			Marker pos = label2.Pos;
			uiControl.Pos = new Marker(ref vector, ref pos.WH);
			form2.AddChild(label2);
			Form form3 = {20338}.MakeProgressBar(0f, (float)requiredAmountFor, (float)item);
			UiControl uiControl2 = form3;
			float {11535} = 30f;
			float {11536} = 40f;
			pos = form3.Pos;
			uiControl2.Pos = new Marker({11535}, {11536}, ref pos.WH);
			form2.AddChild(form3);
			Label label3 = new Label(new Vector2(form3.Pos.XY.X + form3.Pos.WH.X + 10f, 32f), Fonts.Philosopher_12, Color.White, StringHelper.GetValueOfK(item) + " / " + StringHelper.GetValueOfK(requiredAmountFor), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form2.AddChild(label3);
			int num = item2 % 10;
			form2.AddChild(new Label(new Vector2(label3.Pos.XY.X + label3.Pos.WH.X + 10f, 32f), Fonts.Arial_10, Color.White * 0.6f, (num >= 2 && num <= 4) ? Local.quest_got_n_times_2_4(item2) : Local.quest_got_n_times_1(item2), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChildPos(form2, PositionAlignment.Center, PositionAlignment.Center, 0f);
			return form;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00075498 File Offset: 0x00073698
		private static Form MakeProgressBar(float {20351}, float {20352}, float {20353})
		{
			Rectangle rectangle = new Rectangle(2298, 3140, 110, 4);
			Color {13186} = new Color(120, 120, 120);
			Color white = Color.White;
			float num = 1.1f;
			float {11535} = 0f;
			float {11536} = 0f;
			Vector2 vector = rectangle.WidthHeight() * num;
			Form form = new Form(new Marker({11535}, {11536}, ref vector), rectangle, {13186}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			form.AddChild(new Form(new Marker(0f, 0f, (float)rectangle.Width * num / ({20352} - {20351}) * {20353}, (float)rectangle.Height * num), rectangle, white, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			return form;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00075538 File Offset: 0x00073738
		private Form {20354}()
		{
			Vector2 zero = Vector2.Zero;
			Vector2 vector = new Vector2((float){20338}.c_back.Width * 1.014f, (float){20338}.c_back.Height * 1.15f) * this.{20361};
			return new Form(new Marker(ref zero, ref vector), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00075590 File Offset: 0x00073790
		private Form {20355}(string {20356})
		{
			Vector2 zero = Vector2.Zero;
			Vector2 vector = {20338}.c_back.WidthHeight() * this.{20361};
			return new Form(new Marker(ref zero, ref vector), {20338}.c_back, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				ToolTipState = new ToolTipState("", {20356}, Array.Empty<ToolTipCharacteristics>())
			};
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x000755E4 File Offset: 0x000737E4
		private static Label MakeItemHeader(string {20357})
		{
			return new Label(new Vector2(30f, 12f), Fonts.Philosopher_14, Color.White, {20357}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00075624 File Offset: 0x00073824
		[CompilerGenerated]
		internal static void <.ctor>g__HandleCurrentItemPair|5_0(ref {20338}.<>c__DisplayClass5_0 {20358})
		{
			if ({20358}.itemPair.GetChildren.Size == 2)
			{
				{20358}.pairList.AddItem(new UiControl[]
				{
					{20358}.itemPair
				});
				{20358}.itemPair = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			}
		}

		// Token: 0x04000D07 RID: 3335
		public static readonly Rectangle c_back = new Rectangle(156, 1636, 532, 84);

		// Token: 0x04000D08 RID: 3336
		private readonly FractionID {20359};

		// Token: 0x04000D09 RID: 3337
		private readonly GuildQuests {20360};

		// Token: 0x04000D0A RID: 3338
		private readonly float {20361};

		// Token: 0x04000D0B RID: 3339
		private readonly HashSet<GuildQuestAction> {20362} = new HashSet<GuildQuestAction>
		{
			GuildQuestAction.CooperatePb
		};
	}
}
