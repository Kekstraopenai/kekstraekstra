using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using WorldOfSeaBattles.Interface.SocialUi.Ratings;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020003EC RID: 1004
	internal sealed class {22782} : {17625}
	{
		// Token: 0x060015F0 RID: 5616 RVA: 0x000B8104 File Offset: 0x000B6304
		public {22782}(bool {22784} = false) : base(820f, {17625}.c_back3, {17604}.InGameWindow, {17625}.c_icPeople, new {17625}.DynamicTittle[]
		{
			Local.achievements,
			Local.page_rating,
			Local.action_arenaTorunament_name_short,
			Local.arena_rating
		})
		{
			base.AddChild(new UiControl[]
			{
				new InterfaceBackgroundParticles(base.Pos.Border(-3f), ParticlesBackgroundPattern.SparksToUp, ParticlesBackgroundCount.Many, 0.05f, 0.2f, 2.5f, Global.Render.ParticleManager2D, new Rectangle[]
				{
					CommonAtlas.backgroundSpark
				})
				{
					Opacity = 0.05f
				},
				new InterfaceBackgroundParticles(base.Pos.Border(-3f), ParticlesBackgroundPattern.SparksToUp, ParticlesBackgroundCount.Little, 0.7f, 1.4f, 0.5f, Global.Render.ParticleManager2D, new Rectangle[]
				{
					CommonAtlas.backgroundSpark2
				})
				{
					Opacity = 0.03f
				}
			});
			{22782}.CurrentInstance = this;
			base.ComposeTabWithScroll(null, true, true, new Action<ListItemViewControl>[]
			{
				new Action<ListItemViewControl>(this.{22787}),
				new Action<ListItemViewControl>(this.{22801}),
				new Action<ListItemViewControl>(this.{22803}),
				new Action<ListItemViewControl>(this.{22805})
			});
			bool ratingsLoaded = false;
			base.OnTitleItemSelected += delegate({17625}.RoutingEventArgs<int> {22817})
			{
				if ({22817}.Payload != 0 && !ratingsLoaded)
				{
					Global.Network.Send(new OnGetRatingQueryMsg(Session.Account.SID));
					ratingsLoaded = true;
				}
			};
			base.EvRemoveFromContainer += delegate()
			{
				{22782}.CurrentInstance = null;
			};
			if ({22784})
			{
				base.ForceItemSelected(2);
			}
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x000B82DF File Offset: 0x000B64DF
		public void OpenArenaTournamentPage(Action {22785})
		{
			base.ForceItemSelected(2);
			base.EvRemoveFromContainer += {22785};
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000B82F0 File Offset: 0x000B64F0
		public void QueryResponse(OnLoadOneRatingOrTournament {22786})
		{
			this.{22811}.RemoveAll((RatingInformation {22818}) => {22818}.Rating == {22786}.Rating.Rating);
			this.{22811}.Add({22786}.Rating);
			Action refreshCurrentDynamicTabPage = this.RefreshCurrentDynamicTabPage;
			if (refreshCurrentDynamicTabPage == null)
			{
				return;
			}
			refreshCurrentDynamicTabPage();
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000B8348 File Offset: 0x000B6548
		private void {22787}(ListItemViewControl {22788})
		{
			int count = Gameplay.AchievementsDisplayInfo.Count;
			SortedDictionary<AchievementInterfaceCategory, Tlist<AchievementDisplayInfo>> sortedDictionary = new SortedDictionary<AchievementInterfaceCategory, Tlist<AchievementDisplayInfo>>();
			for (int i = 0; i < count; i++)
			{
				AchievementDisplayInfo achievementDisplayInfo = Gameplay.AchievementsDisplayInfo.FromIndex(i);
				if (!PlatformTuning.DisableShop || achievementDisplayInfo.AchievementEnum != AchievementEnum.LuckyOne)
				{
					if (sortedDictionary.ContainsKey(achievementDisplayInfo.UiCategory))
					{
						sortedDictionary[achievementDisplayInfo.UiCategory].Add(achievementDisplayInfo);
					}
					else
					{
						Tlist<AchievementDisplayInfo> tlist = new Tlist<AchievementDisplayInfo>();
						tlist.Add(achievementDisplayInfo);
						sortedDictionary.Add(achievementDisplayInfo.UiCategory, tlist);
					}
				}
			}
			{22782}.<>c__DisplayClass15_0 CS$<>8__locals1;
			CS$<>8__locals1.blocks = new BlocksStackFormControl(base.Pos.XY, 3, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.blocks.MaxWidth = base.Pos.WH.X;
			foreach (KeyValuePair<AchievementInterfaceCategory, Tlist<AchievementDisplayInfo>> keyValuePair in sortedDictionary)
			{
				AchievementInterfaceCategory achievementInterfaceCategory;
				Tlist<AchievementDisplayInfo> tlist2;
				keyValuePair.Deconstruct(out achievementInterfaceCategory, out tlist2);
				AchievementInterfaceCategory {450} = achievementInterfaceCategory;
				Tlist<AchievementDisplayInfo> tlist3 = tlist2;
				tlist3.SortTop((AchievementDisplayInfo {22814}) => {22814}.CommonRatingWeight);
				int {22808} = (from {22815} in tlist3.ToList<AchievementDisplayInfo>()
				select Math.Min(1, Session.Account.Achievements.Count({22815}.AchievementEnum))).Sum();
				{22782}.<LoadAchievementsPage>g__AddHeaderSeparator|15_0({450}.ToStringLocal(), {22808}, tlist3.Size, ref CS$<>8__locals1);
				for (int j = 0; j < tlist3.Size; j++)
				{
					AchievementDisplayInfo ach = tlist3.Array[j];
					int num = Session.Account.Achievements.Count(ach.AchievementEnum);
					Form form = new Form(Vector2.Zero, new Rectangle(0, 3619, 264, 81), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					bool isRewardDone = (ach.IsSingleGive && num > 0) || (ach.AchievementEnum == AchievementEnum.RangFive && num >= Gameplay.RangsInfo.Count / 5);
					form.AddChild(new UiControl[]
					{
						new Image(new Marker(4f, 2f, 76f, 82f), ach.Icon, ach.Icon.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp),
						new Label(new Vector2(82f, 20f), Fonts.Philosopher_14, new Color(173, 168, 143), ach.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp),
						new Label(new Vector2(82f, 40f), Fonts.Philosopher_14, (num == 0) ? Color.Gray : Color.Wheat, num.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					if (!WosbAchievements.GetReward(ach.AchievementEnum).Item1.IsEmpty)
					{
						form.AddChild(new Form(new Vector2(240f, -4f), isRewardDone ? CommonAtlas.rewardDoneIcon : CommonAtlas.rewardIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							ToolTip = new ToolTip(delegate(UiControl {22819})
							{
								ToolTipState toolTipState = new ToolTipState(Fonts.Arial_10);
								toolTipState.AppendText(isRewardDone ? Local.Reward_done : Local.Reward_available, Color.Gray, false, false);
								return toolTipState;
							})
						});
					}
					if (num == 0)
					{
						form.Brightness = 0.6f;
					}
					form.ToolTip = new ToolTip(delegate(UiControl {22820})
					{
						ToolTipState toolTipState = new ToolTipState(Fonts.Arial_10);
						toolTipState.AppendText(ach.Description, Color.Gray, false, false);
						if (ach.AchievementEnum == AchievementEnum.LuckyOne)
						{
							toolTipState.AppendText(Local.achiv_53_state(Session.Account.OpenedChestsCount), Color.Gray, true, false);
						}
						toolTipState.AppendText(Local.common_rating + ": " + ((ach.CommonRatingWeight > 0) ? "+" : "") + ach.CommonRatingWeight.ToString(), Color.Wheat, true, false);
						if (ach.IsSingleGive)
						{
							toolTipState.AppendText(Local.TavernaCommonUi_26, Color.Wheat, true, false);
						}
						ValueTuple<GSI, string> reward = WosbAchievements.GetReward(ach.AchievementEnum);
						if (!string.IsNullOrEmpty(reward.Item2) || !reward.Item1.IsEmpty)
						{
							toolTipState.AppendText(Local.TavernaCommonUi_27, Color.LightYellow, false, false);
							if (!string.IsNullOrEmpty(reward.Item2))
							{
								toolTipState.AppendText(reward.Item2, Color.LightYellow, false, false);
							}
							foreach (GSILocalEnumerablePair<ResourceInfo> {5413} in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)reward.Item1.ResourceInfo))
							{
								toolTipState.AppendText({5413}.ToStringNC(false), Color.LightYellow, false, false);
							}
						}
						return toolTipState;
					});
					Form form2 = new Form(new Marker(0f, 0f, 264f, 84f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					form2.AddChildPos(form, PositionAlignment.LeftUp, PositionAlignment.Center, 0f);
					CS$<>8__locals1.blocks.AddItem(new UiControl[]
					{
						form2
					});
				}
				CS$<>8__locals1.blocks.AddItem(new UiControl[]
				{
					new Form(new Marker(0f, 0f, CS$<>8__locals1.blocks.MaxWidth + 1f, 30f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}
			{22788}.AddItem(new UiControl[]
			{
				CS$<>8__locals1.blocks
			});
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x000B8780 File Offset: 0x000B6980
		private void {22789}(ListItemViewControl {22790}, RatingType {22791})
		{
			RatingType type = {22791};
			RatingType type2 = {22791};
			bool flag = type2 - RatingType.ArenaTournamentHigh <= 1;
			bool flag2 = flag;
			type2 = {22791};
			flag = (type2 - RatingType.ArenaRating <= 1);
			bool flag3 = flag;
			CustomSpriteFont arial_ = Fonts.Arial_12;
			Vector2 {13189} = default(Vector2);
			Form form = new Form({13189}, {22782}.c_item, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Cyan
			};
			form.PosHeight *= 1.4f;
			float num = 200f;
			Marker pos;
			if (flag2)
			{
				StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm.AddItem(new UiControl[]
				{
					this.{22798}(Local.current_rating, delegate
					{
						this.{22812} = RatingType.ArenaTournamentHigh;
					})
				});
				stackForm.AddItem(new UiControl[]
				{
					this.{22798}(Local.prev_rating, delegate
					{
						this.{22812} = RatingType.PrevArenaTournamentHigh;
					})
				});
				form.AddChildPos(stackForm, PositionAlignment.RightDown, PositionAlignment.Center, 0f);
				UiControl uiControl = stackForm;
				pos = stackForm.Pos;
				{13189} = new Vector2(num, 0f);
				Vector2 vector = new Vector2(stackForm.Pos.Width, stackForm.Pos.Height);
				uiControl.Pos = new Marker(ref pos, ref {13189}, ref vector);
				using (IEnumerator<Button> enumerator = stackForm.GetChildren.OfType<Button>().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Button button = enumerator.Current;
						button.Brightness = ((button.Text == Local.current_rating) ? ((this.{22812} == RatingType.ArenaTournamentHigh) ? 1f : 0.5f) : ((this.{22812} == RatingType.PrevArenaTournamentHigh) ? 1f : 0.5f));
					}
					goto IL_309;
				}
			}
			if (flag3)
			{
				StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm2.AddItem(new UiControl[]
				{
					this.{22798}(Local.current_rating, delegate
					{
						this.{22813} = RatingType.ArenaRating;
					})
				});
				stackForm2.AddItem(new UiControl[]
				{
					this.{22798}(Local.prev_rating, delegate
					{
						this.{22813} = RatingType.PrevArenaRating;
					})
				});
				form.AddChildPos(stackForm2, PositionAlignment.RightDown, PositionAlignment.Center, 0f);
				UiControl uiControl2 = stackForm2;
				pos = stackForm2.Pos;
				{13189} = new Vector2(num, 0f);
				Vector2 vector = new Vector2(stackForm2.Pos.Width, stackForm2.Pos.Height);
				uiControl2.Pos = new Marker(ref pos, ref {13189}, ref vector);
				foreach (Button button2 in stackForm2.GetChildren.OfType<Button>())
				{
					button2.Brightness = ((button2.Text == Local.current_rating) ? ((this.{22813} == RatingType.ArenaRating) ? 1f : 0.5f) : ((this.{22813} == RatingType.PrevArenaRating) ? 1f : 0.5f));
				}
			}
			IL_309:
			UiControl uiControl3 = form;
			pos = form.Pos;
			uiControl3.PosWidth = pos.Width + num;
			if (!this.{22811}.Any((RatingInformation {22821}) => {22821}.Rating == {22791}))
			{
				form.AddChild(new Label(new Vector2(5f, 8f), arial_, Color.White * 0.6f, Local.TavernaCommonUi_myRating(0, "-"), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				{22790}.AddItem(new UiControl[]
				{
					form
				});
				return;
			}
			RatingInformation ratingInformation = this.{22811}.Find((RatingInformation {22822}) => {22822}.Rating == {22791});
			Tlist<VisibleRatingLabel> tlist = ratingInformation.Top100.Clone();
			string {13345} = ({22791} == RatingType.AchievementRating) ? Local.rating_common_tt : (({22791} == RatingType.ArenaRating || {22791} == RatingType.PrevArenaRating) ? Local.rating_arena_tt : (({22791} == RatingType.ArenaTournamentHigh || {22791} == RatingType.PrevArenaTournamentHigh) ? Local.rating_arena_tournament : ""));
			form.AddChild(new Label(new Vector2(5f, 8f), Fonts.Philosopher_14Bold, Color.White * 0.6f, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Label(new Vector2(5f, 28f), arial_, Color.White * 0.6f, Local.TavernaCommonUi_myRating((ratingInformation.MyRating == 0) ? "-" : ratingInformation.MyRating.ToString(), (ratingInformation.MyPosition == -1) ? "-" : ratingInformation.MyPosition.ToString()), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			{22790}.AddItem(new UiControl[]
			{
				form
			});
			if (flag2)
			{
				int[] atpointsLeagues = ArenaTournamentGameplay.ATPointsLeagues;
				for (int i = 0; i < atpointsLeagues.Length; i++)
				{
					int item = atpointsLeagues[i];
					if (tlist.Any((VisibleRatingLabel {22823}) => {22823}.LastPoints > (float)item) && tlist.Any((VisibleRatingLabel {22824}) => {22824}.LastPoints < (float)item))
					{
						Tlist<VisibleRatingLabel> tlist2 = tlist;
						VisibleRatingLabel visibleRatingLabel = default(VisibleRatingLabel);
						visibleRatingLabel.AccuntSID = 0U;
						visibleRatingLabel.LastPoints = (float)item;
						visibleRatingLabel.PlayerName = Local.tournament_separator;
						tlist2.Add(visibleRatingLabel);
					}
				}
				tlist.SortTop((VisibleRatingLabel {22816}) => {22816}.LastPoints);
			}
			IRatingRewardsProvider rewardsProvider = this.{22796}({22791});
			int num2 = 0;
			for (int j = 0; j < tlist.Size; j++)
			{
				VisibleRatingLabel visibleRatingLabel2 = tlist.Array[j];
				{13189} = default(Vector2);
				Form form2 = new Form({13189}, {22782}.c_item, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					BasicColor = ((visibleRatingLabel2.AccuntSID == Session.Account.SID) ? Color.Blue : Color.Lerp(Color.White, Color.Red, (float)j / 100f))
				};
				form2.AddChild(new Label(new Vector2(5f, 8f), arial_, Color.White * 0.6f, (j + 1).ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				form2.AddChild(new Label(new Vector2(55f, 8f), arial_, Color.White, ((int)visibleRatingLabel2.LastPoints).ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				form2.AddChild(new Label(new Vector2(130f, 8f), arial_, Color.White * 0.8f, visibleRatingLabel2.PlayerName, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				if (flag2 && visibleRatingLabel2.AccuntSID == Session.Account.SID && ratingInformation.Rating != RatingType.PrevArenaTournamentHigh)
				{
					StackForm stackForm3 = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					for (int k = 0; k < ratingInformation.ExtraParameter; k++)
					{
						stackForm3.AddItem(new UiControl[]
						{
							new Image(Vector2.Zero, AtlasGameGui.Texture.Tex, new Rectangle(2229, 1598, 24, 24), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
					}
					for (int l = 0; l < 3 - ratingInformation.ExtraParameter; l++)
					{
						stackForm3.AddItem(new UiControl[]
						{
							new Image(Vector2.Zero, AtlasGameGui.Texture.Tex, new Rectangle(2254, 1598, 24, 24), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
					}
					stackForm3.Opacity = 0.7f;
					form2.AddChildPos(stackForm3, PositionAlignment.RightDown, PositionAlignment.Center, 170f);
				}
				if (visibleRatingLabel2.AccuntSID != 0U && rewardsProvider != null)
				{
					using (IEnumerator<ArenaPlaceReward> enumerator2 = this.GetRewards({22791}, ratingInformation, rewardsProvider, visibleRatingLabel2).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ArenaPlaceReward reward = enumerator2.Current;
							if (num2 >= reward.StartPlaceIndex && num2 <= reward.EndPlaceIndexInclusive && rewardsProvider.ShouldDisplayRewardIcon({22791}, visibleRatingLabel2.LastPoints, reward))
							{
								Form rewardIconForm = rewardsProvider.GetRewardIconForm(reward.RewardId);
								rewardIconForm.ToolTip = new ToolTip((UiControl {22825}) => rewardsProvider.CreateToolTipState(reward.RewardId, reward.Reward));
								form2.AddChildPos(rewardIconForm, PositionAlignment.RightDown, PositionAlignment.Center, 110f);
							}
						}
					}
					num2++;
				}
				{22790}.AddItem(new UiControl[]
				{
					form2
				});
			}
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x000B9064 File Offset: 0x000B7264
		private IEnumerable<ArenaPlaceReward> GetRewards(RatingType {22792}, RatingInformation {22793}, IRatingRewardsProvider {22794}, VisibleRatingLabel {22795})
		{
			return {22794}.GetRewards((int){22795}.LastPoints, {22793}.TotalPlayersCount);
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x000B907C File Offset: 0x000B727C
		private IRatingRewardsProvider {22796}(RatingType {22797})
		{
			IRatingRewardsProvider result;
			if ({22797} - RatingType.ArenaTournamentHigh > 1)
			{
				if ({22797} - RatingType.ArenaRating > 1)
				{
					result = null;
				}
				else
				{
					result = new ArenaRatingRewardsAdapter();
				}
			}
			else
			{
				result = new ArenaTournamentRewardsAdapter();
			}
			return result;
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x000B90AC File Offset: 0x000B72AC
		private Button {22798}(string {22799}, Action {22800})
		{
			Button button = new Button(new Marker(0f, 0f, 210f, 32f), {22782}.c_ratingButton, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button.SetText({22799}, Fonts.Philosopher_14, Color.Black, false);
			button.EvClick += delegate(ClickUiEventArgs {22826})
			{
				Action onClick = {22800};
				if (onClick != null)
				{
					onClick();
				}
				Action refreshCurrentDynamicTabPage = this.RefreshCurrentDynamicTabPage;
				if (refreshCurrentDynamicTabPage == null)
				{
					return;
				}
				refreshCurrentDynamicTabPage();
			};
			return button;
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x000B91ED File Offset: 0x000B73ED
		[CompilerGenerated]
		private void {22801}(ListItemViewControl {22802})
		{
			this.{22789}({22802}, RatingType.AchievementRating);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000B91F7 File Offset: 0x000B73F7
		[CompilerGenerated]
		private void {22803}(ListItemViewControl {22804})
		{
			this.{22789}({22804}, this.{22812});
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000B9206 File Offset: 0x000B7406
		[CompilerGenerated]
		private void {22805}(ListItemViewControl {22806})
		{
			this.{22789}({22806}, this.{22813});
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x000B9218 File Offset: 0x000B7418
		[CompilerGenerated]
		internal static void <LoadAchievementsPage>g__AddHeaderSeparator|15_0(string {22807}, int {22808}, int {22809}, ref {22782}.<>c__DisplayClass15_0 {22810})
		{
			Color color = Color.White * 0.6f;
			Form form = new Form(new Marker(0f, 0f, {22810}.blocks.MaxWidth + 1f, 40f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_16, 1f);
			textBlockBuilder.Write({22807} + " ", color);
			textBlockBuilder.Write({22808}.ToString(), Color.SoftLime);
			textBlockBuilder.Write(" / ", color);
			textBlockBuilder.Write({22809}.ToString(), ({22808} == {22809}) ? Color.SoftLime : color);
			form.AddChild(textBlockBuilder.Create(new Vector2(3f, 3f)));
			{22810}.blocks.AddItem(new UiControl[]
			{
				form
			});
		}

		// Token: 0x040013C9 RID: 5065
		public static {22782} CurrentInstance;

		// Token: 0x040013CA RID: 5066
		public static Rectangle c_page_background = new Rectangle(1879, 3227, 299, 219);

		// Token: 0x040013CB RID: 5067
		public static readonly Rectangle c_live = new Rectangle(226, 156, 32, 32);

		// Token: 0x040013CC RID: 5068
		public static readonly Rectangle c_item = new Rectangle(1, 363, 595, 39);

		// Token: 0x040013CD RID: 5069
		public static readonly Rectangle c_place1 = new Rectangle(254, 403, 34, 34);

		// Token: 0x040013CE RID: 5070
		public static readonly Rectangle c_place2 = new Rectangle(289, 403, 34, 34);

		// Token: 0x040013CF RID: 5071
		public static readonly Rectangle c_place3 = new Rectangle(324, 403, 34, 34);

		// Token: 0x040013D0 RID: 5072
		public static readonly Rectangle c_placeL = new Rectangle(359, 403, 34, 34);

		// Token: 0x040013D1 RID: 5073
		public static readonly Rectangle c_ratingButton = new Rectangle(1232, 0, 166, 45);

		// Token: 0x040013D2 RID: 5074
		private Tlist<RatingInformation> {22811} = new Tlist<RatingInformation>();

		// Token: 0x040013D3 RID: 5075
		private RatingType {22812} = RatingType.ArenaTournamentHigh;

		// Token: 0x040013D4 RID: 5076
		private RatingType {22813} = RatingType.ArenaRating;
	}
}
