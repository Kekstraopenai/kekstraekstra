using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001A9 RID: 425
	internal sealed class {19037} : {17068}
	{
		// Token: 0x060009AE RID: 2478 RVA: 0x0004B628 File Offset: 0x00049828
		private {19037}(ArenaPersonalResults {19052}, string {19053} = null)
		{
			Vector2 vector = Engine.GS.UIArea.HalfWidthHeightInt() - new Vector2((float){19037}.back.Width, (float){19037}.back.Height) / 2f;
			base..ctor(new Marker(ref vector, ref {19037}.back), {19037}.back, {17068}.BlockingWay.BackgroundClosing, false);
			{19037}.CurrentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{19037}.CurrentInstance = null;
			};
			base.AddChild(new Form(base.Pos.XY - new Vector2(58f, 68f), ({19052}.Result == ArenaBattleResult.YourComandFail) ? {19037}.header_fail : {19037}.header_win, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_18, 1f);
			textBlockBuilder.Write(Session.Account.PlayerName, Color.White * 0.75f);
			textBlockBuilder.Write(" (" + Global.Player.UsedShipPlayer.CraftFrom.ShipName + ")", Color.White * 0.5f);
			TextBlockControl textBlockControl = textBlockBuilder.Create(base.Pos.XY + new Vector2(409f, 144f));
			UiControl uiControl = textBlockControl;
			Marker pos = textBlockControl.Pos;
			vector = -textBlockControl.Pos.WH * 0.5f;
			uiControl.Pos = pos.Offset(vector);
			base.AddChild(new UiControl[]
			{
				new Label(base.Pos.XY + new Vector2(411f, 57f), Fonts.Philosopher_24, Color.White, {19053} ?? {19052}.Result.ToStrLocal(), PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center(),
				textBlockControl
			});
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.BattleResults, 0.03f, 1f);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0004B83C File Offset: 0x00049A3C
		public {19037}(CompletedPassingMapStatistics {19054}) : this(new ArenaPersonalResults
		{
			ArenaRatingChange = 0,
			Bonus = new Bonus(0f, 0),
			Result = ArenaBattleResult.YourComandWin
		}, Local.BattleResultsWindow_3 + {19054}.WavesPassed.ToString())
		{
			StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(45f, 196f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.AddItems(true, null, {19054}.Showings, 1, stackForm, null);
			base.AddChild(stackForm);
			ArenaPlayerShowing arenaPlayerShowing = {19054}.Showings.FirstOrDefault((ArenaPlayerShowing {19085}) => {19085}.PlayerName == Session.Account.PlayerName);
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_16, 1f);
			Tlist<Image> tlist = new Tlist<Image>();
			if (arenaPlayerShowing.ComputedServerReward.GoldBonus != 0)
			{
				Tlist<Image> tlist2 = tlist;
				Image image = {19037}.MakeIconWithCount({18826}.ArenaRewardType.Gold, arenaPlayerShowing.ComputedServerReward.GoldBonus);
				tlist2.Add(image);
			}
			if (arenaPlayerShowing.ComputedServerReward.XpBonus != 0)
			{
				Tlist<Image> tlist3 = tlist;
				Image image = {19037}.MakeIconWithCount({18826}.ArenaRewardType.Xp, arenaPlayerShowing.ComputedServerReward.XpBonus);
				tlist3.Add(image);
			}
			textBlockBuilder.WriteLine(Local.BattleResultsWindow_4 + new TimeSpan((long)(10000000f * {19054}.PassingTimeSec)).ToString("hh\\:mm\\:ss"), Color.White * 0.4f);
			foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){19054}.TotalSpecialRewards.ResourceInfo))
			{
				textBlockBuilder.Write("• " + gsilocalEnumerablePair.Info.Name + ": " + gsilocalEnumerablePair.Count.ToString(), Color.White * 0.4f);
			}
			this.{19072}(textBlockBuilder.CreateCentroid(), tlist.ToArray<Image>());
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0004BA48 File Offset: 0x00049C48
		public {19037}(ArenaMode? {19055}, ArenaPersonalResults {19056}, Tlist<ArenaPlayerShowing> {19057}, Tlist<ArenaPlayerShowing> {19058}, string {19059} = null, string {19060} = null, int {19061} = 0, int {19062} = 0, int {19063} = 0, byte? {19064} = null, string {19065} = null) : this({19056}, {19065})
		{
			ScrollBarControl scrollBarControl = new ScrollBarControl(new Marker(0f, 0f, 1f, 1f), CommonAtlas.transpPixel, CommonAtlas.transpPixel, CommonAtlas.transpPixel, CommonAtlas.transpPixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Vector2 vector = base.Pos.XY + new Vector2(38f, 196f);
			ListItemViewControl listItemViewControl = new ListItemViewControl(new Marker(ref vector, 766f, 298f), scrollBarControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.AddItems(false, {19055}, {19057}, 0, stackForm, {19059});
			this.AddItems(false, {19055}, {19058}, 1, stackForm2, {19060});
			if ({19064} != null)
			{
				stackForm.AddItem(new UiControl[]
				{
					StackForm.QuickHorizontal(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Arial_12, Color.Pink, "-1 ", PositionAlignment.LeftUp, PositionAlignment.LeftUp),
						new Form(Vector2.Zero, new Rectangle(2229, 1598, 24, 24), PositionAlignment.LeftUp, PositionAlignment.LeftUp),
						new Label(Vector2.Zero, Fonts.Arial_12, Color.Pink, " " + Local.remainLives({19064}.Value, 3), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					})
				});
			}
			StackForm stackForm3 = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm3.AddItem(new UiControl[]
			{
				stackForm,
				stackForm2
			});
			listItemViewControl.AddItem(new UiControl[]
			{
				stackForm3
			});
			base.AddChild(new UiControl[]
			{
				scrollBarControl,
				listItemViewControl
			});
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_12, 0f);
			Tlist<Image> tlist = new Tlist<Image>();
			int goldBonus = {19056}.Bonus.GoldBonus;
			if (goldBonus != 0)
			{
				Tlist<Image> tlist2 = tlist;
				Image image = {19037}.MakeIconWithCount({18826}.ArenaRewardType.Gold, goldBonus);
				tlist2.Add(image);
				if ({19056}.Bonus.GoldBonus > 0)
				{
					{19994}.Logbook(Local.gold + " " + {19056}.Bonus.GoldBonus.ToString(), LBFlags.L0);
				}
			}
			if ({19063} > 0)
			{
				Tlist<Image> tlist3 = tlist;
				Image image = {19037}.MakeIconWithCount({18826}.ArenaRewardType.Marks, {19063});
				tlist3.Add(image);
				{19994}.Logbook(Gameplay.ItemsInfo.FromID(37).Name + " +" + {19063}.ToString(), LBFlags.L1);
			}
			int xpBonus = {19056}.Bonus.XpBonus;
			if (xpBonus != 0)
			{
				Tlist<Image> tlist4 = tlist;
				Image image = {19037}.MakeIconWithCount({18826}.ArenaRewardType.Xp, xpBonus);
				tlist4.Add(image);
				{19994}.Logbook(Local.xp_plus(xpBonus), LBFlags.L0);
			}
			if ({19061} > 0)
			{
				Tlist<Image> tlist5 = tlist;
				Image image = {19037}.MakeIconWithCount({18826}.ArenaRewardType.PbConquerorBadges, {19061});
				tlist5.Add(image);
			}
			if ({19062} != 0)
			{
				Tlist<Image> tlist6 = tlist;
				vector = Vector2.Zero;
				Vector2 vector2 = Vector2.One * (float){19037}.rewardIconSize;
				Image image = {19037}.MakeIconWithCount(new Image(new Marker(ref vector, ref vector2), Gameplay.ItemsInfo[35].IconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp), {19061});
				tlist6.Add(image);
			}
			if ({19056}.ArenaRatingChange != 0)
			{
				Tlist<Image> tlist7 = tlist;
				Image image = {19037}.MakeIconWithCount({18826}.ArenaRewardType.ArenaPoints, {19056}.ArenaRatingChange);
				tlist7.Add(image);
			}
			if (goldBonus < 0)
			{
				textBlockBuilder.WriteLine(Local.BattleResultsWindow_12, Color.OrangeRed);
			}
			this.{19072}(textBlockBuilder.CreateCentroid(), tlist.ToArray<Image>());
			foreach (ArenaPlayerShowing arenaPlayerShowing in ((IEnumerable<ArenaPlayerShowing>){19057}))
			{
				if (arenaPlayerShowing.PlayerName == Session.Account.PlayerName)
				{
					{19994}.Logbook(Local.battle_info((int)arenaPlayerShowing.GetStat(ArenaShowingProperty.SendDamage), (int)arenaPlayerShowing.GetStat(ArenaShowingProperty.Kills)), LBFlags.L0);
				}
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0004BE0C File Offset: 0x0004A00C
		private static Image MakeIconWithCount({18826}.ArenaRewardType {19066}, int {19067})
		{
			return {19037}.MakeIconWithCount({18826}.RewardIconHelper({19066}, {19037}.rewardIconSize), {19067});
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0004BE1F File Offset: 0x0004A01F
		private static Image MakeIconWithCount(Image {19068}, int {19069})
		{
			if ({19068} != null)
			{
				{19037}.AddRewardCount({19068}, {19069});
			}
			return {19068};
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0004BE2C File Offset: 0x0004A02C
		private static void AddRewardCount(Image {19070}, int {19071})
		{
			{19070}.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14Bold, {19037}.rewardCountTextColor, {19071}.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.RightDown, 0f, -26f, false);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0004BE5F File Offset: 0x0004A05F
		private void {19072}(TextBlockControl {19073}, Image[] {19074})
		{
			base.AddChildPos({19073}, PositionAlignment.Center, PositionAlignment.LeftUp, 10f, 470f, false);
			base.AddChildPos({18826}.MakeRewardsBlock(this.{19082}, this.{19083}, {19074}), PositionAlignment.Center, PositionAlignment.RightDown, 0f, 48f, false);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0004BE9C File Offset: 0x0004A09C
		private void AddItems(bool {19075}, ArenaMode? {19076}, Tlist<ArenaPlayerShowing> {19077}, int {19078}, StackForm {19079}, string {19080} = null)
		{
			Form form = new Form(new Marker(0f, 0f, 382f, 23f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			bool flag = {19076} != null && {19076}.Value.GetInfo().WinKretery == ArenaWinKretery.LootCount;
			form.AddChild(new Label(new Vector2(0f, 0f), Fonts.Arial_12, Color.White * 0.4f, Local.name, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Label(new Vector2(177f, -1f), Fonts.Arial_12, Color.White * 0.4f, Local.xp_s, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Label(new Vector2(237f, 0f), Fonts.Arial_12, Color.White * 0.4f, Local.ResourceOrItemToolTipHelper_14, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Label(new Vector2(287f, 0f), Fonts.Arial_12, Color.White * 0.4f, (!{19075} && {19076} == null) ? Local.BattleResultsWindow_13_b : Local.BattleResultsWindow_13, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			if (flag)
			{
				form.AddChild(new Label(new Vector2(337f, 0f), Fonts.Arial_12, Color.White * 0.4f, Local.BattleResultsWindow_Title_CargoPoints, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			{19079}.AddItem(new UiControl[]
			{
				form
			});
			int num = 0;
			foreach (ArenaPlayerShowing arenaPlayerShowing in ((IEnumerable<ArenaPlayerShowing>){19077}))
			{
				Form form2 = new Form(new Marker(0f, 0f, 382f, 23f), AtlasGameGui.rect_asset_whitepixel_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					BasicColor = (((num + {19078}) % 2 == 0) ? (Color.SkyBlue * 0.1f) : (Color.Black * 0.1f))
				};
				Color value = (arenaPlayerShowing.PlayerName == Session.Account.PlayerName) ? Color.Gold : Color.White;
				form2.AddChild(new Label(new Vector2(0f, 3f), Fonts.Arial_12, value * 0.5f, arenaPlayerShowing.PlayerName, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				form2.AddChild(new Label(new Vector2(177f, 3f), Fonts.Arial_12, value * 0.9f, arenaPlayerShowing.ComputedServerReward.XpBonus.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				form2.AddChild(new Label(new Vector2(237f, 3f), Fonts.Arial_12, value * 0.9f, StringHelper.GetValueOfK((int)Math.Round((double)(arenaPlayerShowing.GetStat(ArenaShowingProperty.SendDamage) + arenaPlayerShowing.GetStat(ArenaShowingProperty.SendDamageBuldings)))), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				if ({19076} == null)
				{
					form2.AddChild(new Label(new Vector2(287f, 3f), Fonts.Arial_12, value * 0.9f, arenaPlayerShowing.GetStat(ArenaShowingProperty.Kills).ToString() + " / " + arenaPlayerShowing.GetStat(ArenaShowingProperty.Death).ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				}
				else
				{
					form2.AddChild(new Label(new Vector2(287f, 3f), Fonts.Arial_12, value * 0.9f, arenaPlayerShowing.GetStat(ArenaShowingProperty.Kills).ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				}
				if (flag)
				{
					form2.AddChild(new Label(new Vector2(337f, 3f), Fonts.Arial_12, value * 0.9f, arenaPlayerShowing.GetStat(ArenaShowingProperty.CargoPoints).ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				}
				{19079}.AddItem(new UiControl[]
				{
					form2
				});
				num++;
				TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_14, 0f);
				textBlockBuilder.WriteLine(Local.BattleResultsWindow_14, Color.SkyBlue * 0.9f);
				textBlockBuilder.Write(Math.Round((double)arenaPlayerShowing.GetStat(ArenaShowingProperty.SendDamage)).ToString() ?? "", Color.Wheat * 0.8f, 250f, false);
				if (flag)
				{
					textBlockBuilder.WriteLine(Local.BattleResultsWindow_ToolTip_CargoPoints1, Color.SkyBlue * 0.9f);
					textBlockBuilder.Write(arenaPlayerShowing.GetStat(ArenaShowingProperty.LootedCargoPoints).ToString() ?? "", Color.Wheat * 0.8f, 250f, false);
					textBlockBuilder.WriteLine(Local.BattleResultsWindow_ToolTip_CargoPoints2, Color.SkyBlue * 0.9f);
					textBlockBuilder.Write(arenaPlayerShowing.GetStat(ArenaShowingProperty.CargoPoints).ToString() ?? "", Color.Wheat * 0.8f, 250f, false);
				}
				if ({19076} == null || {19076}.Value.GetInfo().WinKretery == ArenaWinKretery.DestroyFort)
				{
					textBlockBuilder.WriteLine(Local.BattleResultsWindow_15, Color.SkyBlue * 0.9f);
					textBlockBuilder.Write(Math.Round((double)arenaPlayerShowing.GetStat(ArenaShowingProperty.SendDamageBuldings)).ToString() ?? "", Color.Wheat * 0.8f, 250f, false);
				}
				textBlockBuilder.WriteLine(Local.BattleResultsWindow_16, Color.SkyBlue * 0.9f);
				textBlockBuilder.Write(arenaPlayerShowing.GetStat(ArenaShowingProperty.Kills).ToString() ?? "", Color.Wheat * 0.8f, 250f, false);
				if (!{19075})
				{
					textBlockBuilder.WriteLine(Local.BattleResultsWindow_18, Color.Gray * 0.9f);
					textBlockBuilder.Write(Math.Round((double)arenaPlayerShowing.GetStat(ArenaShowingProperty.SendDamageEquip)).ToString() ?? "", Color.Wheat * 0.8f, 250f, false);
				}
				textBlockBuilder.WriteLine(Local.BattleResultsWindow_17, Color.Gray * 0.9f);
				textBlockBuilder.Write(Math.Round((double)arenaPlayerShowing.GetStat(ArenaShowingProperty.ReceivedDamage)).ToString() ?? "", Color.Wheat * 0.8f, 250f, false);
				textBlockBuilder.WriteLine(Local.BattleResultsWindow_21, Color.Yellow * 0.6f);
				textBlockBuilder.Write(arenaPlayerShowing.GetStat(ArenaShowingProperty.Death).ToString() ?? "", Color.Wheat * 0.8f, 250f, false);
				textBlockBuilder.Write("          .", Color.Wheat * 0.0001f);
				textBlockBuilder.WriteLines(Local.BattleResultsWindow_23, Color.Gray * 0.9f, Fonts.Philosopher_14, 330f, new float?((float)-1));
				form2.ToolTipState = new ToolTipState(textBlockBuilder);
			}
			if (!string.IsNullOrEmpty({19080}))
			{
				foreach (string {13345} in {19080}.Split(new string[]
				{
					Environment.NewLine
				}, StringSplitOptions.RemoveEmptyEntries))
				{
					Form form3 = new Form(new Marker(0f, 0f, 382f, 23f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					form3.AddChild(new Label(Vector2.Zero, Fonts.Arial_12, Color.White * 0.5f, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					{19079}.AddItem(new UiControl[]
					{
						form3
					});
				}
			}
			float num2 = Math.Min(1f, 3f / (float){19079}.CountChild());
			int num3 = 0;
			foreach (UiControl uiControl in ((IEnumerable<UiControl>){19079}.GetChildren))
			{
				uiControl.Opacity = 0f;
				new UiActionsSleep(uiControl, 400f + (float)(num3 * 300) * num2);
				new UiOpacityAnimation(uiControl, 0f, 1f, 300f);
				if (uiControl.Brightness == 1f)
				{
					new UiBrightnessAnimation(uiControl, 1f, 2f, 200f);
					new UiBrightnessAnimation(uiControl, 2f, 1f, 300f);
				}
				num3++;
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00030234 File Offset: 0x0002E434
		protected override void UserUpdate(ref FrameTime {19081})
		{
			base.UserUpdate(ref {19081});
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0004C758 File Offset: 0x0004A958
		protected override void UserBackRender()
		{
			if (this.{19084} = (Engine.GS.CurrentTexture != AtlasGameGui.Texture.Tex))
			{
				Engine.GS.SetTexture(AtlasGameGui.Texture);
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0004C798 File Offset: 0x0004A998
		protected override void UserFrontRender()
		{
			if (this.{19084})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x040008A6 RID: 2214
		public static {19037} CurrentInstance;

		// Token: 0x040008A7 RID: 2215
		public static readonly Rectangle header_red = new Rectangle(1412, 1171, 916, 163);

		// Token: 0x040008A8 RID: 2216
		public static readonly Rectangle header_win = new Rectangle(0, 1804, 930, 177);

		// Token: 0x040008A9 RID: 2217
		public static readonly Rectangle header_fail = new Rectangle(0, 1626, 930, 177);

		// Token: 0x040008AA RID: 2218
		public static readonly Rectangle back = new Rectangle(1410, 1337, 818, 626);

		// Token: 0x040008AB RID: 2219
		public static readonly Rectangle c_xpIconTop = new Rectangle(2229, 1574, 23, 23);

		// Token: 0x040008AC RID: 2220
		public static readonly Rectangle c_xpIconMiddle = new Rectangle(2253, 1574, 23, 23);

		// Token: 0x040008AD RID: 2221
		public static readonly Rectangle c_xpIconDown = new Rectangle(2277, 1574, 23, 23);

		// Token: 0x040008AE RID: 2222
		private static readonly int rewardIconSize = 38;

		// Token: 0x040008AF RID: 2223
		private static readonly Color rewardCountTextColor = new Color(165, 138, 71);

		// Token: 0x040008B0 RID: 2224
		private readonly int {19082} = 40;

		// Token: 0x040008B1 RID: 2225
		private readonly int {19083} = 8;

		// Token: 0x040008B2 RID: 2226
		private bool {19084};
	}
}
