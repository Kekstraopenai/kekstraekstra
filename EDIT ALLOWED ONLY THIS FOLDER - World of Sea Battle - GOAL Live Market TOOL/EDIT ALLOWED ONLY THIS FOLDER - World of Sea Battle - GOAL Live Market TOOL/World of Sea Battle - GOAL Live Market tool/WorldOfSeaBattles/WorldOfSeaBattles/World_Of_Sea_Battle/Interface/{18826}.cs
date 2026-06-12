using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using WorldOfSeaBattles.Interface.GamemodesUi;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000188 RID: 392
	internal sealed class {18826} : {17625}
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0004542C File Offset: 0x0004362C
		private static int padding
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x00045430 File Offset: 0x00043630
		private int spacing
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00045434 File Offset: 0x00043634
		private Vector2 contentStartOffset
		{
			get
			{
				return new Vector2(308f, 194f);
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00045448 File Offset: 0x00043648
		public {18826}() : base((float){18826}.windowWidth, {18826}.back, {17604}.InGameWindow, Rectangle.Empty, Array.Empty<{17625}.DynamicTittle>())
		{
			this.TextureHost = AtlasGameGui.Texture.Tex;
			EducationHelper.MakeFlag(EducationOnboarding.GameTT_ArenaWindow, true);
			{18826}.CurrentInstance = this;
			this.AnimatedFocus = false;
			base.EvRemoveFromContainer += delegate()
			{
				{18826}.CurrentInstance = null;
			};
			base.AddChildPos({18826}.<.ctor>g__MakeRatingInfo|54_2(), PositionAlignment.LeftUp, PositionAlignment.LeftUp, 82f, 110f, false);
			this.{18848}(Local.arena, 0);
			this.{18848}(Local.PortVisualScene_2, 1);
			base.ComposeTab(new Form[]
			{
				this.{18839}(),
				this.{18842}()
			});
			EducationHelper.MakeFlag(EducationOnboarding.GameTT_ArenaWindow, true);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00045554 File Offset: 0x00043754
		public static Form MakeRewardsBlock(int {18827}, int {18828}, params Image[] {18829})
		{
			Form form = new Form(new Marker(0f, 0f, {18826}.itemMarker.Width * 3f, {18826}.itemMarker.Height * 0.195f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			if ({18829}.Length != 0)
			{
				form.AddChildPos({18826}.MakeSeparatorLine({18826}.rewardsIcon), PositionAlignment.Center, PositionAlignment.LeftUp, 0f);
				StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm.AddSpace((float){18827});
				foreach (Image image in {18829})
				{
					if (image.TexturePath == AtlasGameGui.rect_asset_transparent_1px)
					{
						image.RemoveFromContainer();
						stackForm.AddItem(new UiControl[]
						{
							new Label(default(Vector2), Fonts.Arial_12, Color.Wheat * 0.5f, Local.no_rewards, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
					}
					else
					{
						stackForm.AddItem(new UiControl[]
						{
							image
						});
					}
					stackForm.AddSpace((float){18827});
				}
				form.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.RightDown, 0f, (float){18828} * 1.3f, false);
			}
			return form;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00045670 File Offset: 0x00043870
		public static Image RewardIconHelper({18826}.ArenaRewardType {18830}, int {18831} = 32)
		{
			float {11535} = 0f;
			float {11536} = 0f;
			Vector2 vector = Vector2.One * (float){18831};
			Marker {13271} = new Marker({11535}, {11536}, ref vector);
			Image result;
			switch ({18830})
			{
			case {18826}.ArenaRewardType.Gold:
				result = new Image({13271}, AtlasGameGui.Texture.Tex, {18826}.reward_coin, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					ToolTipState = new ToolTipState(Local.gold, "", Array.Empty<ToolTipCharacteristics>())
				};
				break;
			case {18826}.ArenaRewardType.Marks:
				result = new Image({13271}, AtlasGameGui.Texture.Tex, {18826}.reward_mark, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					ToolTipState = new ToolTipState(Local.res_37_name, "", Array.Empty<ToolTipCharacteristics>())
				};
				break;
			case {18826}.ArenaRewardType.Xp:
				result = new Image({13271}, AtlasGameGui.Texture.Tex, {18826}.reward_xp, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					ToolTipState = new ToolTipState(Local.xp_s.TrimEnd(':'), "", Array.Empty<ToolTipCharacteristics>())
				};
				break;
			case {18826}.ArenaRewardType.ArenaPoints:
				result = new Image({13271}, AtlasGameGui.Texture.Tex, {18826}.reward_points, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					ToolTipState = new ToolTipState(Local.arena_rating + ", " + Local.arena_currency, "", Array.Empty<ToolTipCharacteristics>())
				};
				break;
			case {18826}.ArenaRewardType.PbConquerorBadges:
				result = new Image({13271}, CommonAtlas.Texture.Tex, CommonAtlas.conquerorBadgeIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					ToolTipState = new ToolTipState(Local.conquer_badges, "", Array.Empty<ToolTipCharacteristics>())
				};
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x000457E0 File Offset: 0x000439E0
		private static UiControl MakeSeparatorLine(Rectangle {18832})
		{
			int num = 20;
			Form form = new Form(new Marker(0f, 0f, (float){18826}.separatorLine.Width, (float){18826}.separatorLine.Height).ScaleSize(0.34f), {18826}.separatorLine, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			form.AddChildPos(new Image(new Marker(0f, 0f, (float)num, (float)num), AtlasGameGui.Texture.Tex, {18832}, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.Center, 0f);
			return form;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00045868 File Offset: 0x00043A68
		private Form {18833}()
		{
			Form {13204} = new Form({18826}.itemMarker, {18826}.itemBack, Color.Black, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				Opacity = 0.5f,
				AnimatedFocus = false
			};
			Form form = new Form({18826}.itemMarker, new Rectangle(0, 0, 1, 1), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			form.AddChild({13204});
			return form;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000458C4 File Offset: 0x00043AC4
		private Form {18834}(ArenaMode {18835}, string {18836}, params Image[] {18837})
		{
			bool flag = {18837}.Length != 0;
			Form form = this.{18833}();
			CustomSpriteFont philosopher_ = Fonts.Philosopher_12;
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(philosopher_, 0f);
			textBlockBuilder.WriteLines({18836}, flag ? this.{18865} : new Color(140, 125, 110), philosopher_, {18826}.itemMarker.Width - (float){18826}.padding * 3f, new float?(0f));
			TextBlockControl textBlockControl = flag ? textBlockBuilder.Create() : textBlockBuilder.CreateCentroid();
			form.AddChildPos(textBlockControl, PositionAlignment.LeftUp, flag ? PositionAlignment.LeftUp : PositionAlignment.Center, flag ? ((float){18826}.padding) : ((float){18826}.padding * 2f), (float){18826}.padding, false);
			if ({18835} == ArenaMode.DuelRating)
			{
				form.AddChildPos(new Button(default(Vector2), new Rectangle(2579, 71, 192, 34), PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.open_rating, Fonts.Philosopher_12, Color.LightSkyBlue, false).ExClick(delegate(ClickUiEventArgs {18876})
				{
					new {22782}(true);
				}), PositionAlignment.Center, PositionAlignment.LeftUp, textBlockControl.Pos.Height + (float)({18826}.padding * 3));
			}
			form.AddChildPos({18826}.MakeRewardsBlock(6, {18826}.padding, {18837}), PositionAlignment.Center, PositionAlignment.RightDown, 0f);
			return form;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00045A14 File Offset: 0x00043C14
		private Form MakeArenaModeTab(ArenaMode? {18838})
		{
			{18826}.<>c__DisplayClass60_0 CS$<>8__locals1 = new {18826}.<>c__DisplayClass60_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.mode = {18838};
			float {11535} = 0f;
			float {11536} = 0f;
			Vector2 vector = Vector2.One * 32f;
			Marker {13271} = new Marker({11535}, {11536}, ref vector);
			Image[] array2;
			if (CS$<>8__locals1.mode != null)
			{
				ArenaMode? mode = CS$<>8__locals1.mode;
				ArenaMode arenaMode = ArenaMode.DuelTraning;
				if (!(mode.GetValueOrDefault() == arenaMode & mode != null))
				{
					Image[] array = new Image[4];
					array[0] = {18826}.RewardIconHelper({18826}.ArenaRewardType.Gold, 32);
					array[1] = {18826}.RewardIconHelper({18826}.ArenaRewardType.Marks, 32);
					array[2] = {18826}.RewardIconHelper({18826}.ArenaRewardType.Xp, 32);
					array2 = array;
					array[3] = {18826}.RewardIconHelper({18826}.ArenaRewardType.ArenaPoints, 32);
				}
				else
				{
					(array2 = new Image[1])[0] = new Image({13271}, AtlasGameGui.Texture.Tex, AtlasGameGui.rect_asset_transparent_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				}
			}
			else
			{
				array2 = new Image[0];
			}
			Image[] {18837} = array2;
			Form form = new Form(new Marker(0f, 0f, {18826}.itemMarker.Width * 2f + (float)this.spacing + this.contentStartOffset.X, {18826}.itemMarker.Height + this.contentStartOffset.Y), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			ArenaMode valueOrDefault = CS$<>8__locals1.mode.GetValueOrDefault();
			string {18836};
			if (CS$<>8__locals1.mode != null)
			{
				{18826}.<>c__DisplayClass60_0 CS$<>8__locals2 = CS$<>8__locals1;
				{18836} = ((CS$<>8__locals2.mode != null) ? CS$<>8__locals2.mode.GetValueOrDefault().ToStringDescription() : null);
			}
			else
			{
				{18836} = Local.arena_modes_desc;
			}
			form.AddChildPos(this.{18834}(valueOrDefault, {18836}, {18837}), PositionAlignment.LeftUp, PositionAlignment.LeftUp, this.contentStartOffset.X, this.contentStartOffset.Y, false);
			form.AddChildPos(CS$<>8__locals1.<MakeArenaModeTab>g__MakeStatsBlock|1((CS$<>8__locals1.mode == null) ? null : new {18826}.ModeStats?(new {18826}.ModeStats
			{
				NowBattles = Session.ArenaGames.CountRunGames((int)CS$<>8__locals1.mode.Value),
				PlayersInQueue = Session.ArenaGames.CountWaitGames((int)CS$<>8__locals1.mode.Value),
				ShipRanks = {18826}.<MakeArenaModeTab>g__GetRanks|60_0(CS$<>8__locals1.mode)
			})), PositionAlignment.LeftUp, PositionAlignment.LeftUp, this.contentStartOffset.X + {18826}.itemMarker.Width + (float)this.spacing, this.contentStartOffset.Y, false);
			return form;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00045C48 File Offset: 0x00043E48
		private Form {18839}()
		{
			{18826}.<>c__DisplayClass61_0 CS$<>8__locals1 = new {18826}.<>c__DisplayClass61_0();
			CS$<>8__locals1.<>4__this = this;
			Vector2 vector = base.Pos.XY + new Vector2(-22f, -20f);
			Marker pos = base.Pos;
			Form form = new Form(new Marker(ref vector, ref pos.WH), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			CS$<>8__locals1.host = new Tab(base.Pos, Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChild(CS$<>8__locals1.host);
			CS$<>8__locals1.host.Add(new Form[]
			{
				this.MakeArenaModeTab(null)
			});
			CS$<>8__locals1.host.Select(0);
			CustomSpriteFont philosopher_ = Fonts.Philosopher_12;
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(philosopher_, 0f);
			TextBlockBuilder textBlockBuilder2 = textBlockBuilder;
			string arena_desc = Local.arena_desc;
			Color {13572} = Color.LightGray * 0.6f;
			CustomSpriteFont {13573} = philosopher_;
			pos = base.Pos;
			textBlockBuilder2.WriteLines(arena_desc, {13572}, {13573}, pos.Width - 300f, new float?(0f));
			form.AddChildPos(textBlockBuilder.Create(), PositionAlignment.LeftUp, PositionAlignment.LeftUp, 240f, 126f, false);
			float {11535} = 0f;
			float {11536} = 0f;
			vector = Vector2.One * 26f;
			new Marker({11535}, {11536}, ref vector);
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				CS$<>8__locals1.<MakeArenaPage>g__createModeSelectBt|0(ArenaMode.DuelRating, {18826}.tornament_icon)
			});
			stackForm.AddItem(new UiControl[]
			{
				CS$<>8__locals1.<MakeArenaPage>g__createModeSelectBt|0(ArenaMode.WallVsWall, {18826}.mass_icon)
			});
			stackForm.AddItem(new UiControl[]
			{
				CS$<>8__locals1.<MakeArenaPage>g__createModeSelectBt|0(ArenaMode.Collecting, {18826}.hunt_icon)
			});
			stackForm.AddSpace(385f - stackForm.Pos.WH.Y);
			stackForm.AddItem(new UiControl[]
			{
				CS$<>8__locals1.<MakeArenaPage>g__createModeSelectBt|0(ArenaMode.DuelTraning, {18826}.training_icon)
			});
			form.AddChildPos(stackForm, PositionAlignment.LeftUp, PositionAlignment.LeftUp, 54f, this.contentStartOffset.Y + (float)({18826}.sbwTabIdle.Height / 2) - 4f, false);
			this.{18867} = CS$<>8__locals1.<MakeArenaPage>g__MakeRunButton|2();
			this.{18868} = new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Lerp(new Color(131, 18, 26), Color.White, 0.1f), "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos(this.{18867}, PositionAlignment.RightDown, PositionAlignment.RightDown, 16f, 26f, false);
			form.AddChild(this.{18868});
			this.{18868}.UpdateComplete += delegate(UiControl {18886})
			{
				string text = {18826}.ErrorText(CS$<>8__locals1.<>4__this.{18869});
				if (!string.IsNullOrEmpty(text))
				{
					CS$<>8__locals1.<>4__this.{18868}.Text = text;
					UiControl uiControl = CS$<>8__locals1.<>4__this.{18868};
					Marker pos2 = CS$<>8__locals1.<>4__this.{18868}.Pos;
					Vector2 vector2 = CS$<>8__locals1.<>4__this.Pos.XY + new Vector2(CS$<>8__locals1.<>4__this.Pos.WH.X - CS$<>8__locals1.<>4__this.{18868}.PosWidth - 25f, 606f);
					uiControl.Pos = pos2.SetXY(vector2);
					CS$<>8__locals1.<>4__this.{18867}.IsVisible = false;
					return;
				}
				CS$<>8__locals1.<>4__this.{18868}.Text = string.Empty;
				CS$<>8__locals1.<>4__this.{18867}.IsVisible = (CS$<>8__locals1.<>4__this.{18869} != null);
			};
			return form;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00045ED8 File Offset: 0x000440D8
		private string {18840}(ArenaMode {18841})
		{
			int num = Session.ArenaGames.CountWaitGames((int){18841});
			if ({18841} != ArenaMode.DuelRating)
			{
				return num.ToString();
			}
			if (num <= 0)
			{
				return "0";
			}
			return "+";
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00045F0C File Offset: 0x0004410C
		private Form {18842}()
		{
			{18826}.<>c__DisplayClass63_0 CS$<>8__locals1 = new {18826}.<>c__DisplayClass63_0();
			CS$<>8__locals1.<>4__this = this;
			this.{18874} = new Form(base.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Form form = this.{18874};
			CS$<>8__locals1.categoryCount = 3;
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_12, 0f);
			textBlockBuilder.WriteLines(Local.arena_shop_desc, Color.LightGray * 0.6f, textBlockBuilder.defaultFont, base.Pos.Width - 300f, new float?((float)-1));
			form.AddChildPos(textBlockBuilder.Create(), PositionAlignment.LeftUp, PositionAlignment.LeftUp, 240f, 116f, false);
			int num = 360;
			this.{18871} = new ScrollBarControl(new Marker(0f, 0f, 30f, (float)num), CommonAtlas.whitePixel, CommonAtlas.whitePixel, AtlasGameGui.scrollBarNewPointer, CommonAtlas.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{18871}.DisableDepthFocusTest = true;
			Form {12956} = new Form(new Marker(0f, 0f, 10f, (float)num), AtlasGameGui.scrollBarNewFiller, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos({12956}, PositionAlignment.RightDown, PositionAlignment.LeftUp, 20f, 214f, false);
			form.AddChildPos(this.{18871}, PositionAlignment.RightDown, PositionAlignment.LeftUp, 0f, 214f, false);
			form.AddChildPos({18826}.<MakeShopPage>g__MakeArenaPoints|63_7(), PositionAlignment.LeftUp, PositionAlignment.RightDown, 72f, 44f, false);
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Marker {13079} = new Marker(0f, 0f, 16f, 16f);
			string[] array = new string[]
			{
				Local.PirateTrader_All,
				Local.PirateTrader_Valuable,
				Local.PirateTrader_Design
			};
			for (int i = 0; i < CS$<>8__locals1.categoryCount; i++)
			{
				int index = i;
				StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Button button = new Button({13079}, {18826}.filterBtnOnIcon, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					Tag = index
				};
				stackForm2.EvClick += delegate(ClickUiEventArgs {18901})
				{
					CS$<>8__locals1.<>4__this.{18872} = index;
					CS$<>8__locals1.<MakeShopPage>g__UpdateRadioButtons|0();
					CS$<>8__locals1.<MakeShopPage>g__UpdateItemsDisplay|2();
				};
				this.{18873}.Add(button);
				Label label = new Label(0f, 0f, Fonts.Arial_12, Color.Gray, array[i], PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm2.AddItem(new UiControl[]
				{
					button
				});
				stackForm2.AddSpace(10f);
				stackForm2.AddItem(new UiControl[]
				{
					label
				});
				stackForm.AddItem(new UiControl[]
				{
					stackForm2
				});
				if (i != 2)
				{
					stackForm.AddSpace(35f);
				}
			}
			CS$<>8__locals1.<MakeShopPage>g__UpdateRadioButtons|0();
			CS$<>8__locals1.<MakeShopPage>g__UpdateItemsDisplay|2();
			form.AddChildPos(stackForm, PositionAlignment.RightDown, PositionAlignment.RightDown, 35f, 45f, false);
			return form;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000461D8 File Offset: 0x000443D8
		private static string ShipRanksInfo(ArenaModeSettings {18843})
		{
			string result;
			if ({18843}.RangRitriction.Item1 != 0 && {18843}.RangRitriction.Item2 != 0)
			{
				ValueTuple<int, int> rangRitriction = {18843}.RangRitriction;
				string str = rangRitriction.Item1.ToString();
				string str2 = "-";
				rangRitriction = {18843}.RangRitriction;
				result = str + str2 + rangRitriction.Item2.ToString();
			}
			else if ({18843}.RangRitriction.Item2 != 0)
			{
				result = Local.ArenaJoinGui_30({18843}.RangRitriction.Item2);
			}
			else if ({18843}.RangRitriction.Item1 != 0)
			{
				result = Local.ArenaJoinGui_31({18843}.RangRitriction.Item1);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0004628C File Offset: 0x0004448C
		private static string ErrorText(ArenaMode? {18844})
		{
			if (EducationHelper.MakeInvisibleArenaAndModesPortButtons)
			{
				return Local.ArenaJoinGui_rankError(EducationHelper.ArenaAvailableSinceRank);
			}
			if (Session.Account.Quests.ProgressRunningQuests.Any((QuestRunningProgress {18878}) => {18878}.Info.ShipEffects.Length != 0))
			{
				return Local.not_available_quest;
			}
			if (Global.Player.UsedShip.FirstHP.Summary / Global.Player.UsedShip.MaxHp < 0.66f)
			{
				return Local.ArenaJoinGui_34;
			}
			if (Global.Player.UsedShip.StaticInfo.IsBalloon)
			{
				return Local.error_balloon;
			}
			if (Global.Player.UsedShipPlayer.CraftFrom.DisllowedOnArena)
			{
				return Local.not_available + " (" + Global.Player.UsedShipPlayer.CraftFrom.ShipName + ")";
			}
			if ({18844} == null)
			{
				return null;
			}
			ArenaModeSettings info = {18844}.Value.GetInfo();
			if (Session.Account.ArenaPenaltySec > 0f && !info.IsDuetl)
			{
				return Local.ArenaJoinGui_15 + StringHelper.TimeMMMSS((double)Session.Account.ArenaPenaltySec);
			}
			if ({18844}.GetValueOrDefault() == ArenaMode.DuelRating && Session.EventActionsPipeline.EventCategoryAmount(EActionCaterory.BArenaAllowTournamentGame) == 0f)
			{
				EventActionBase eventActionBase = Session.EventActionsPipeline.WaitActions.FirstOrDefault(delegate(EventActionBase {18879})
				{
					EABehavior1 eabehavior = {18879}.Behavior as EABehavior1;
					return eabehavior != null && eabehavior.Category == EActionCaterory.BArenaAllowTournamentGame;
				});
				if (eventActionBase == null)
				{
					return Local.not_available;
				}
				string str = new LocalizedDateTime(eventActionBase.StartDateTimeServer, false).PreferredTime.ToString("HH.mm");
				LocalizedDateTime localizedDateTime = new LocalizedDateTime(eventActionBase.EndDateTimeServer, false);
				string str2 = localizedDateTime.PreferredTime.ToString("HH.mm") + localizedDateTime.RequiredPrefix;
				return Local.ArenaJoinGui_16 + str + " - " + str2;
			}
			else
			{
				if ((info.RangRitriction.Item1 != 0 && Global.Player.UsedShipPlayer.CraftFrom.Rank < info.RangRitriction.Item1) || (info.RangRitriction.Item2 != 0 && Global.Player.UsedShipPlayer.CraftFrom.Rank > info.RangRitriction.Item2))
				{
					return Local.ArenaJoinGui_32({18826}.ShipRanksInfo(info));
				}
				if (info.OnlyGroup && Session.Group == null)
				{
					return Local.ArenaJoinGui_33;
				}
				if ({18844}.GetValueOrDefault() == ArenaMode.DuelRating)
				{
					if (Global.Player.UsedShipPlayer.Upgrades.InstalledCount < 4)
					{
						return Local.ArenaJoinGui_35;
					}
					int argumentInt = (Session.EventActionsPipeline.CurrentActions.Find(delegate(EventActionBase {18880})
					{
						EABehavior1 eabehavior = {18880}.Behavior as EABehavior1;
						return eabehavior != null && eabehavior.Category == EActionCaterory.BArenaTournament;
					}).Behavior as EABehavior1).ArgumentInt;
					if (argumentInt != Global.Player.UsedShipPlayer.CraftFrom.Rank)
					{
						return Local.ArenaJoinGui_32(argumentInt);
					}
				}
				return "";
			}
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00046598 File Offset: 0x00044798
		private void {18845}(ArenaMode {18846})
		{
			Global.Game.ScenePort.CheckSelectedShip(delegate
			{
				if (!string.IsNullOrEmpty({18826}.ErrorText(new ArenaMode?({18846}))))
				{
					return;
				}
				Session.CachedUiArenaMode = new ArenaMode?({18846});
				PlayerAccount {9655} = Global.Player.IsPortEntry ? Session.Account : null;
				Global.Network.Send(new OnJoinOrLeaveArena({18846}, true, {9655}));
				this.BlockAndClose();
				new {18807}();
			}, false, false);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x000465D8 File Offset: 0x000447D8
		protected override void UserUpdate(ref FrameTime {18847})
		{
			base.UserUpdate(ref {18847});
			if (InputHelper.NowMouseState.ScrollValue != InputHelper.LastMouseState.ScrollValue)
			{
				int num = InputHelper.NowMouseState.ScrollValue - InputHelper.LastMouseState.ScrollValue;
				this.{18871}.CurrentScrollFactor -= (float)num / 500f;
				this.{18871}.CurrentScrollFactor = Math.Clamp(this.{18871}.CurrentScrollFactor, 0f, 1f);
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00046658 File Offset: 0x00044858
		protected override void UserBackRender()
		{
			base.UserBackRender();
			Device gs = Engine.GS;
			Vector2 vector = base.Pos.XY + new Vector2(62f, 85f);
			Color color = Color.White * base.Opacity;
			gs.Draw({18826}.c_flag, vector, color);
			Device gs2 = Engine.GS;
			vector = base.Pos.XY + new Vector2(-18f, -50f);
			color = Color.White * base.Opacity;
			gs2.Draw({19037}.header_red, vector, color);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00046A74 File Offset: 0x00044C74
		[CompilerGenerated]
		private void {18848}(string {18849}, int {18850})
		{
			int num = 14;
			int num2 = 34;
			if ({18850} > 0)
			{
				base.AddChild(new Label(new Vector2(base.Pos.XY.X + (float)({18826}.windowWidth / 2) + (float)num, base.Pos.XY.Y + (float)num2), Fonts.Philosopher_24, Color.Gray, " ◈ ", PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			LabelButton labelButton = new LabelButton(Vector2.Zero, {18849}, Fonts.Philosopher_24, Color.Gray, Color.Wheat, delegate(ClickUiEventArgs {18881})
			{
				this.ForceItemSelected({18850});
			});
			labelButton.Pos = new Marker(base.Pos.XY.X + (float)({18826}.windowWidth / 2) + (float)num + (({18850} == 0) ? (-labelButton.PosWidth) : ((float)(num * 3))), base.Pos.XY.Y + (float)num2, ref labelButton.Pos.WH);
			labelButton.MoveToFrontLevel();
			base.AddChild(labelButton);
			labelButton.UpdateComplete += delegate(UiControl {18882})
			{
				((LabelButton){18882}).DefaultColor = (({18850} == this.selectedTitle) ? Color.Wheat : Color.Gray);
			};
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00046B9C File Offset: 0x00044D9C
		[CompilerGenerated]
		internal static Form <.ctor>g__MakeRatingInfo|54_2()
		{
			Form form = new Form(new Marker(0f, 0f, 100f, 100f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14Bold, Color.White, Local.rating, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 0f);
			form.AddChildPos(new LiveLabel(Vector2.Zero, Fonts.Philosopher_24Bold, Color.Gold, () => Session.Account.ArenaRating.ToString(), 500), PositionAlignment.Center, PositionAlignment.LeftUp, 0f, 18f, false);
			return form;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00046C40 File Offset: 0x00044E40
		[CompilerGenerated]
		internal static ValueTuple<int, int>[] <MakeArenaModeTab>g__GetRanks|60_0(ArenaMode? {18851})
		{
			int num;
			int num2;
			if ({18851} == null)
			{
				num = 0;
				num2 = 0;
			}
			else
			{
				ValueTuple<int, int> rangRitriction = {18851}.Value.GetInfo().RangRitriction;
				num = rangRitriction.Item1;
				num2 = rangRitriction.Item2;
			}
			if (num == 0)
			{
				num = 1;
				if (num2 == 0)
				{
					num2 = 7;
				}
			}
			ValueTuple<int, int>[] array = new ValueTuple<int, int>[num2 - num + 1];
			for (int i = 0; i <= num2 - num; i++)
			{
				int item = 0;
				array[i] = new ValueTuple<int, int>(num + i, item);
			}
			return array;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00046CBC File Offset: 0x00044EBC
		[CompilerGenerated]
		internal static Form <MakeArenaModeTab>g__MakeStatLine|60_2(string {18852}, int {18853}, Color {18854}, CustomSpriteFont {18855}, int {18856} = 20)
		{
			Form form = new Form(new Marker(0f, 0f, {18826}.itemMarker.Width - (float){18826}.padding * 1.7f, (float){18856}), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			form.AddChildPos(new Label(Vector2.Zero, {18855}, {18854}, {18852}, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, (float){18826}.padding * 0.1f, 0f, false);
			form.AddChildPos(new Label(Vector2.Zero, {18855}, {18854}, {18853}.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.RightDown, PositionAlignment.Center, 0f);
			return form;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00046D50 File Offset: 0x00044F50
		[CompilerGenerated]
		internal static Form <MakeArenaModeTab>g__MakeStatLineLive|60_3(string {18857}, Func<string> {18858}, Color {18859}, CustomSpriteFont {18860}, int {18861} = 20)
		{
			Form form = new Form(new Marker(0f, 0f, {18826}.itemMarker.Width - (float){18826}.padding * 1.7f, (float){18861}), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			form.AddChildPos(new Label(Vector2.Zero, {18860}, {18859}, {18857}, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, (float){18826}.padding * 0.1f, 0f, false);
			form.AddChildPos(new LiveLabel(Vector2.Zero, {18860}, {18859}, () => {18858}(), 100), PositionAlignment.RightDown, PositionAlignment.Center, 0f);
			return form;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00046DF4 File Offset: 0x00044FF4
		[CompilerGenerated]
		internal static bool <MakeShopPage>g__IsOfferInFilterCategory|63_1(TraderOffer {18862}, int {18863})
		{
			bool result;
			if ({18863} != 1)
			{
				result = ({18863} != 2 || {18862}.DesignId != -1);
			}
			else
			{
				result = ({18862}.Res != null || {18862}.Gold.Value != 0);
			}
			return result;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00046E48 File Offset: 0x00045048
		[CompilerGenerated]
		internal static StackForm <MakeShopPage>g__MakeRatingRestrictionInfo|63_4(int {18864})
		{
			int num = 8;
			int num2 = 30;
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Form form = new Form(new Marker(0f, 0f, (float)num2, (float)num2), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos(new Image(new Marker(0f, 0f, (float)num2, (float)num2), AtlasGameGui.Texture.Tex, {18826}.c_lock, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 0f, (float)(-(float)num2) * 0.2f, false);
			stackForm.AddItem(new UiControl[]
			{
				form
			});
			stackForm.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_14, Color.White, Local.rating, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			stackForm.AddSpace((float)num);
			stackForm.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_14Bold, new Color(162, 102, 88), {18864}.ToString() + "+", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			return stackForm;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00046F48 File Offset: 0x00045148
		[CompilerGenerated]
		internal static StackForm <MakeShopPage>g__MakeArenaPoints|63_7()
		{
			int num = 22;
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			LiveLabel arenaPointsText = new LiveLabel(Vector2.Zero, Fonts.Philosopher_14Bold, Color.SandyBrown, () => Session.Account.ArenaCurrency.Value.ToString(), 500);
			stackForm.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_14, Color.White, Local.your_balance + ": ", PositionAlignment.LeftUp, PositionAlignment.LeftUp),
				arenaPointsText
			});
			Form iconForm = new Form(new Marker(0f, 0f, (float)num, (float)num), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			arenaPointsText.UpdateComplete += delegate(UiControl {18908})
			{
				UiControl iconForm = iconForm;
				float x = arenaPointsText.Pos.XY.X;
				Marker pos = arenaPointsText.Pos;
				float {11535} = x + pos.Width + 8f;
				float y = iconForm.Pos.XY.Y;
				pos = iconForm.Pos;
				iconForm.Pos = new Marker({11535}, y, ref pos.WH);
			};
			iconForm.AddChildPos(new Image(new Marker(0f, 0f, (float)num, (float)num), AtlasGameGui.Texture.Tex, {18826}.c_points, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 0f, (float)(-(float)num) * 0.1f, false);
			stackForm.AddItem(new UiControl[]
			{
				iconForm
			});
			return stackForm;
		}

		// Token: 0x040007F2 RID: 2034
		public static {18826} CurrentInstance;

		// Token: 0x040007F3 RID: 2035
		private static int windowWidth = 806;

		// Token: 0x040007F4 RID: 2036
		private static readonly Rectangle c_arenaIcon = new Rectangle(1410, 1005, 95, 95);

		// Token: 0x040007F5 RID: 2037
		private static readonly Rectangle c_buttonsAll = new Rectangle(650, 1002, 758, 274);

		// Token: 0x040007F6 RID: 2038
		private static readonly Rectangle c_buttonMode = new Rectangle(741, 1511, 218, 50);

		// Token: 0x040007F7 RID: 2039
		private static readonly Rectangle c_buttonModePicked = new Rectangle(741, 1562, 218, 50);

		// Token: 0x040007F8 RID: 2040
		private static readonly Rectangle c_buttonCountMask = new Rectangle(960, 1511, 30, 31);

		// Token: 0x040007F9 RID: 2041
		private static readonly Rectangle c_oneBoxHighlight = new Rectangle(722, 1277, 237, 233);

		// Token: 0x040007FA RID: 2042
		private static readonly Rectangle c_pvpRangForm = new Rectangle(2229, 1469, 149, 104);

		// Token: 0x040007FB RID: 2043
		private static readonly Rectangle c_dayilyMission = new Rectangle(1149, 1808, 260, 68);

		// Token: 0x040007FC RID: 2044
		private static readonly Rectangle c_flag = new Rectangle(2231, 1623, 137, 115);

		// Token: 0x040007FD RID: 2045
		private static readonly Rectangle c_lock = new Rectangle(2304, 1748, 46, 50);

		// Token: 0x040007FE RID: 2046
		private static readonly Rectangle c_points = new Rectangle(2229, 1739, 65, 65);

		// Token: 0x040007FF RID: 2047
		private static readonly Rectangle back = new Rectangle(3037, 1080, 498, 393);

		// Token: 0x04000800 RID: 2048
		private static readonly Rectangle sbwTabIdle = new Rectangle(812, 223, 321, 48);

		// Token: 0x04000801 RID: 2049
		private static readonly Rectangle sbwTabPressed = new Rectangle(812, 272, 321, 48);

		// Token: 0x04000802 RID: 2050
		private static readonly Rectangle bttn_to_battle = new Rectangle(3148, 811, 652, 163);

		// Token: 0x04000803 RID: 2051
		private static readonly Rectangle tornament_icon = new Rectangle(3473, 994, 42, 42);

		// Token: 0x04000804 RID: 2052
		private static readonly Rectangle mass_icon = new Rectangle(3516, 994, 42, 42);

		// Token: 0x04000805 RID: 2053
		private static readonly Rectangle hunt_icon = new Rectangle(3559, 994, 42, 42);

		// Token: 0x04000806 RID: 2054
		private static readonly Rectangle training_icon = new Rectangle(3602, 994, 42, 42);

		// Token: 0x04000807 RID: 2055
		private static readonly Color colorDesc = new Color(125, 163, 193);

		// Token: 0x04000808 RID: 2056
		private static Rectangle itemBack = new Rectangle(3561, 936, 1, 1);

		// Token: 0x04000809 RID: 2057
		private static Rectangle itemEmptyIcon = new Rectangle(859, 1982, 151, 192);

		// Token: 0x0400080A RID: 2058
		private static Rectangle separatorLine = new Rectangle(3148, 975, 652, 18);

		// Token: 0x0400080B RID: 2059
		private static Rectangle rewardsIcon = new Rectangle(3473, 1037, 24, 24);

		// Token: 0x0400080C RID: 2060
		private static Rectangle battlesIcon = new Rectangle(3762, 994, 38, 38);

		// Token: 0x0400080D RID: 2061
		private static Rectangle playersIcon = new Rectangle(3723, 994, 38, 38);

		// Token: 0x0400080E RID: 2062
		private static Rectangle shipsIcon = new Rectangle(3684, 994, 38, 38);

		// Token: 0x0400080F RID: 2063
		private static Rectangle reward_coin = new Rectangle(3129, 994, 85, 85);

		// Token: 0x04000810 RID: 2064
		private static Rectangle reward_mark = new Rectangle(3215, 994, 85, 85);

		// Token: 0x04000811 RID: 2065
		private static Rectangle reward_xp = new Rectangle(3301, 994, 85, 85);

		// Token: 0x04000812 RID: 2066
		private static Rectangle reward_points = new Rectangle(3387, 994, 85, 85);

		// Token: 0x04000813 RID: 2067
		private static Rectangle filterBtnOnIcon = new Rectangle(3498, 1037, 32, 32);

		// Token: 0x04000814 RID: 2068
		private static Rectangle filterBtnOffIcon = new Rectangle(3531, 1037, 32, 32);

		// Token: 0x04000815 RID: 2069
		private static Marker itemMarker = new Marker(230f, 0f, 240f, 360f);

		// Token: 0x04000816 RID: 2070
		private readonly Color {18865} = new Color(148, 148, 148);

		// Token: 0x04000817 RID: 2071
		private Tlist<Button> {18866} = new Tlist<Button>();

		// Token: 0x04000818 RID: 2072
		private UiControl {18867};

		// Token: 0x04000819 RID: 2073
		private Label {18868};

		// Token: 0x0400081A RID: 2074
		private ArenaMode? {18869};

		// Token: 0x0400081B RID: 2075
		private ArenaShopLogic {18870} = new ArenaShopLogic();

		// Token: 0x0400081C RID: 2076
		private ScrollBarControl {18871};

		// Token: 0x0400081D RID: 2077
		private int {18872};

		// Token: 0x0400081E RID: 2078
		private List<Button> {18873} = new List<Button>();

		// Token: 0x0400081F RID: 2079
		private Form {18874};

		// Token: 0x04000820 RID: 2080
		private StackForm {18875};

		// Token: 0x02000189 RID: 393
		public enum ArenaRewardType
		{
			// Token: 0x04000822 RID: 2082
			Gold,
			// Token: 0x04000823 RID: 2083
			Marks,
			// Token: 0x04000824 RID: 2084
			Xp,
			// Token: 0x04000825 RID: 2085
			ArenaPoints,
			// Token: 0x04000826 RID: 2086
			PbConquerorBadges
		}

		// Token: 0x0200018A RID: 394
		internal struct ModeStats
		{
			// Token: 0x04000827 RID: 2087
			public int NowBattles;

			// Token: 0x04000828 RID: 2088
			public int PlayersInQueue;

			// Token: 0x04000829 RID: 2089
			public ValueTuple<int, int>[] ShipRanks;
		}
	}
}
