using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200020E RID: 526
	internal sealed class {19717} : CustomUi
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0005EEAC File Offset: 0x0005D0AC
		private static Vector2 oldSquareButtonSize
		{
			get
			{
				return new Vector2(44f, 45f) * 0.95f;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0005EEC7 File Offset: 0x0005D0C7
		private static Vector2 oldRectButtonSize
		{
			get
			{
				return new Vector2(59f, 45f) * 0.95f;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0005EEE2 File Offset: 0x0005D0E2
		private static float headWidth
		{
			get
			{
				return (float)({19717}.c_quest_head.Width + 10);
			}
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0005EEF4 File Offset: 0x0005D0F4
		public {19717}() : base(false)
		{
			if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
			{
				this.TexturePath = Rectangle.Empty;
			}
			if (Session.Account.Rang <= 9)
			{
				if (Session.Account.Quests.ProgressRunningQuests.Count((QuestRunningProgress {19764}) => {19764}.Info.Company == QuestCompany.Education) == 1)
				{
					{19717}.questsAreVisible = true;
				}
			}
			this.{19753} = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(this.{19753});
			{19717}.CurrentInstance = this;
			{19717}.IsRedPing = true;
			this.AnimatedFocus = false;
			base.EvRemoveFromContainer += delegate()
			{
				{19717}.CurrentInstance = null;
			};
			this.{19756} = new Tlist<AnimatedButton>();
			GameSceneName getCurrentSceneName = Global.Game.GetCurrentSceneName;
			this.{19752} = new AnimatedButton(Vector2.Zero, {19717}.c_bt_event, {19717}.c_bt_event, {19717}.c_bt_event_f, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			UiControl uiControl = this.{19752};
			Marker pos = this.{19752}.Pos;
			Vector2 vector = {19717}.oldSquareButtonSize;
			uiControl.Pos = pos.Resize(vector);
			this.{19753}.AddItem(new UiControl[]
			{
				this.{19752}
			});
			this.{19752}.EvClick += delegate(ClickUiEventArgs {19765})
			{
				{19127}.Open();
			};
			this.{19752}.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(Local.events, null, Array.Empty<ToolTipCharacteristics>()));
			this.button_GameWiki = new AnimatedButton(Vector2.Zero, {19717}.c_bt_wiki, {19717}.c_bt_wiki, {19717}.c_bt_wiki_f, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			UiControl uiControl2 = this.button_GameWiki;
			pos = this.button_GameWiki.Pos;
			vector = {19717}.oldSquareButtonSize;
			uiControl2.Pos = pos.Resize(vector);
			this.{19753}.AddItem(new UiControl[]
			{
				this.button_GameWiki
			});
			this.button_GameWiki.EvClick += delegate(ClickUiEventArgs {19766})
			{
				if ({18702}.CurrentInstance == null)
				{
					new {18702}(false, null);
				}
			};
			this.button_GameWiki.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(Local.EducationPlanWindow_Title, null, Array.Empty<ToolTipCharacteristics>()));
			this.{19751} = new AnimatedButton(Vector2.Zero, {19717}.c_bt_rating, {19717}.c_bt_rating, {19717}.c_bt_rating_f, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			UiControl uiControl3 = this.{19751};
			pos = this.{19751}.Pos;
			vector = {19717}.oldSquareButtonSize;
			uiControl3.Pos = pos.Resize(vector);
			AnimatedButton animatedButton = new AnimatedButton(Vector2.Zero, {19717}.c_bt_guild_rating, {19717}.c_bt_guild_rating, {19717}.c_bt_guild_rating_f, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			UiControl uiControl4 = animatedButton;
			pos = animatedButton.Pos;
			vector = {19717}.oldSquareButtonSize;
			uiControl4.Pos = pos.Resize(vector);
			this.{19750} = new AnimatedButton(Vector2.Zero, {19717}.c_bt_people, {19717}.c_bt_people, {19717}.c_bt_people_f, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			UiControl uiControl5 = this.{19750};
			pos = this.{19750}.Pos;
			vector = {19717}.oldSquareButtonSize;
			uiControl5.Pos = pos.Resize(vector);
			this.{19749} = new AnimatedButton(Vector2.Zero, {19717}.c_bt_guild, {19717}.c_bt_guild, {19717}.c_bt_guild_f, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			UiControl uiControl6 = this.{19749};
			pos = this.{19749}.Pos;
			vector = {19717}.oldSquareButtonSize;
			uiControl6.Pos = pos.Resize(vector);
			this.{19748} = new AnimatedButton(Vector2.Zero, {19717}.c_bt_quest, {19717}.c_bt_quest, {19717}.c_bt_quest_f, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			UiControl uiControl7 = this.{19748};
			pos = this.{19748}.Pos;
			vector = {19717}.oldSquareButtonSize;
			uiControl7.Pos = pos.Resize(vector);
			this.{19753}.AddItem(new UiControl[]
			{
				this.{19748},
				this.{19750},
				this.{19751},
				this.{19749},
				animatedButton
			});
			this.{19747} = new Form(Vector2.Zero, {19717}.c_bt_pingOnlineFps, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			UiControl uiControl8 = this.{19747};
			pos = this.{19747}.Pos;
			vector = {19717}.oldRectButtonSize;
			uiControl8.Pos = pos.Resize(vector);
			Label label = new Label(new Vector2(23f, 15f), Fonts.Arial_8, Color.Wheat * 0.6f, Global.GetCurrentServer().Id.StartsWith("ru") ? "ping" : Global.GetCurrentServer().Id, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			label.Pos = label.Pos.Offset(-label.Pos.Width, 0f);
			this.{19747}.AddChild(label);
			this.{19747}.AddChild(new LiveLabel(new Vector2(26f, 5f), Fonts.Arial_8, Color.White * 0.4f, () => Global.Game.GameTime.FpsCounter.AvgInt.ToString() ?? "", 200));
			this.{19747}.AddChild(new LiveLabel(new Vector2(26f, 15f), Fonts.Arial_8, Color.White * 0.4f, delegate(LiveLabel {19767})
			{
				float num = Session.LastPing;
				{19767}.BasicColor = ((num > 300f) ? Color.Orange : Color.White) * 0.4f;
				num = ((num > 1f) ? ((float)((int)num)) : ((float)((int)(num * 100f)) / 100f));
				return Math.Min(999f, num).ToString();
			}, 200));
			this.{19747}.AddChild(new LiveLabel(new Vector2(26f, 27f), Fonts.Arial_8, Color.White * 0.4f, () => Session.LastOnline.ToString(), 1000));
			this.{19753}.AddItem(new UiControl[]
			{
				this.{19747}
			});
			this.{19751}.EvClick += delegate(ClickUiEventArgs {19768})
			{
				if ({22782}.CurrentInstance == null)
				{
					new {22782}(false);
				}
			};
			this.{19751}.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(Local.ratings_achi, null, Array.Empty<ToolTipCharacteristics>()));
			this.{19750}.EvClick += delegate(ClickUiEventArgs {19769})
			{
				if ({22585}.CurrentInstance == null && !Global.Player.MapInfo.IsEducationMap)
				{
					new {22585}();
				}
			};
			this.{19750}.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(Local.FirendsWindow_3, null, Array.Empty<ToolTipCharacteristics>()));
			this.{19749}.EvClick += delegate(ClickUiEventArgs {19770})
			{
				if (Session.Guild != null && {20364}.CurrentInstance == null)
				{
					new {20364}();
				}
				if (Session.Guild == null)
				{
					new {20143}({20143}.Mode.CreateOrJoin, null);
				}
			};
			animatedButton.EvClick += delegate(ClickUiEventArgs {19771})
			{
				if ({20143}.CurrentInstance == null)
				{
					new {20143}((Session.Guild == null) ? {20143}.Mode.CreateOrJoin : {20143}.Mode.ViewOnly, null);
				}
			};
			this.{19749}.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(Local.guild, null, Array.Empty<ToolTipCharacteristics>()));
			animatedButton.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(Local.GuildsWindow_0, null, Array.Empty<ToolTipCharacteristics>()));
			this.{19748}.EvClick += delegate(ClickUiEventArgs {19772})
			{
				new {19779}(false, null, null);
			};
			this.{19748}.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(Local.GameSettingsWindow_104b, null, Array.Empty<ToolTipCharacteristics>()));
			UiControl uiControl9 = this.{19753};
			pos = this.{19753}.Pos;
			vector = new Vector2((float)Engine.GS.UIArea.Width - this.{19753}.Pos.WH.X - 3f, 2f);
			uiControl9.Pos = pos.SetXY(vector);
			this.{19753}.PositionAlignment_X = PositionAlignment.RightDown;
			foreach (UiControl uiControl10 in ((IEnumerable<UiControl>)this.{19753}.GetChildren))
			{
				AnimatedButton animatedButton2 = uiControl10 as AnimatedButton;
				if (animatedButton2 != null)
				{
					this.{19756}.Add(animatedButton2);
				}
			}
			Form form = new Form(this.{19752}.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(Local.events_locked(7), null, Array.Empty<ToolTipCharacteristics>()));
			Form {13204} = form;
			this.{19754} = form;
			base.AddChild({13204});
			Session.EducState_IsInitialMessageShown = true;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0005F778 File Offset: 0x0005D978
		protected override void UserUpdate(ref FrameTime {19718})
		{
			if (EducationHelper.MakeInvisibleLeftUpPanel)
			{
				for (int i = 0; i < this.{19756}.Size; i++)
				{
					this.{19756}.Array[i].Opacity = 0.3f;
				}
				if (!Session.Account.EducationQuest.HasFlag(EducationOnboarding.InteropQuestUnit))
				{
					this.{19748}.AllowMouseInput = false;
					this.{19748}.Opacity = 0.5f;
				}
				else
				{
					this.{19748}.AllowMouseInput = true;
					this.{19748}.Opacity = 1f;
				}
				this.button_GameWiki.Opacity = ((Global.Settings.UnreadGamepedia.Size > 0 || Session.Account.Rang >= 2) ? 1f : 0.3f);
				if (Session.Account.IsEducationInProgress(EducationOnboarding.GetAchievment, true, false))
				{
					this.{19751}.Opacity = 1f;
				}
			}
			else
			{
				for (int j = 0; j < this.{19756}.Size; j++)
				{
					this.{19756}.Array[j].Opacity = 1f;
				}
			}
			this.{19749}.Opacity = ((Session.Guild != null) ? 1f : 0.3f);
			if (this.{19758}.Sample(ref {19718}) && (this.{19757} == null || this.{19757}.AnimationsCount == 0) && !Global.Camera.IsSpyglass)
			{
				this.{19722}();
			}
			if (GameScene.GameHasInputFocus && {19779}.CurrentInstance == null && Global.Settings.kb_OpenLogbook.IsClick && {18560}.closed)
			{
				{22478} currentInstance = {22478}.CurrentInstance;
				if (currentInstance != null && !currentInstance.IsInput && this.{19748}.AllowMouseInput && !Global.Player.MapInfo.IsEnableArenaUi)
				{
					new {19779}(false, null, null);
				}
			}
			base.IsVisible = (Global.Render.UiMode != InterfaceMode.Off);
			if (Global.Render.UiMode == InterfaceMode.Cinematografic)
			{
				base.Opacity = Renderer.UiOpacityToFocus(this.{19753}.Pos);
			}
			else
			{
				base.Opacity = 1f;
			}
			float opacity = this.{19753}.Opacity;
			Geometry.Evalute(ref opacity, (Global.Game.GetCurrentSceneName == GameSceneName.Port || Global.Game.IsMouseVisible) ? 1f : 0.8f, {19718}.secElapsed * 2f);
			this.{19753}.Opacity = opacity;
			if (this.{19757} != null)
			{
				this.{19757}.IsVisible = ({22279}.CurrentInstance == null);
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0005F9F8 File Offset: 0x0005DBF8
		protected override void UserBackRender()
		{
			if (this.{19763} = (Engine.GS.CurrentTexture != AtlasGameGui.Texture.Tex))
			{
				Engine.GS.SetTexture(AtlasGameGui.Texture.Tex);
			}
			this.{19755}.Y = 0f;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0005FA50 File Offset: 0x0005DC50
		protected override void UserFrontRender()
		{
			if (Global.Game.GetCurrentSceneName == GameSceneName.Game)
			{
				Device gs = Engine.GS;
				Rectangle rectangle = {19717}.IsRedPing ? AtlasGameGui.rect_redConnect : AtlasGameGui.rect_greenConnect;
				Vector2 vector = this.{19747}.Pos.XY + new Vector2(43f, 17f);
				gs.Draw(rectangle, vector);
				{18483}.Render(this.{19747}.Pos.XY + new Vector2(-80f, 50f));
			}
			if (CalendarEvents.CurrentEvent.IsActive)
			{
				this.{19752}.IsVisible = true;
				this.{19754}.IsVisible = true;
				if (Session.Account.Rang >= 7)
				{
					if (!CalendarEvents.CurrentEvent.IsFirstOpened(Session.Account))
					{
						if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
						{
							{19127}.Open();
						}
						else
						{
							this.{19719}(this.{19752}, 1);
						}
					}
					this.{19752}.BasicColor = Color.White;
					this.{19752}.AllowMouseInput = true;
					this.{19754}.MoveToBackLevel();
				}
				else
				{
					this.{19752}.BasicColor = Color.White * 0.5f;
					this.{19752}.AllowMouseInput = false;
					this.{19754}.MoveToFrontLevel();
				}
			}
			else
			{
				this.{19752}.IsVisible = false;
				this.{19754}.IsVisible = false;
			}
			if (this.{19763})
			{
				Engine.GS.ReturnBackTexture();
			}
			int num = Session.EventActionsPipeline.CurrentActions.Count((EventActionBase {19773}) => !Session.Account.ReadNotificationsEventActions.Contains({19773}.AID) && {19773}.Name != "removed" && {19773}.CanHaveUnreadMarker);
			if (Session.UnreadLogbookFlag)
			{
				num += 100;
			}
			int size = Session.Account.Quests.ProgressRunningQuests.Size;
			if (num + size > 0)
			{
				this.{19719}(this.{19748}, num);
			}
			if (Global.Settings.UnreadGamepedia.Size > 0 && Global.Settings.EnableGameTooltips)
			{
				this.{19719}(this.button_GameWiki, Global.Settings.UnreadGamepedia.Size);
			}
			if (Session.Guild != null && Session.Guild.Version != Global.Settings.SavedGuildVersion)
			{
				this.{19719}(this.{19749}, 100);
			}
			if (Session.FriendsRequests.Size > 0)
			{
				this.{19719}(this.{19750}, Session.FriendsRequests.Size);
			}
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0005FCB0 File Offset: 0x0005DEB0
		private void {19719}(AnimatedButton {19720}, int {19721})
		{
			float scale = base.GetOpcaity() * (0.85f + 0.15f * (float)Math.Sin(Global.Game.GameTotalTimeSec * 4.0));
			Device gs = Engine.GS;
			Texture2D chatAtl = OtherTextures.ChatAtl;
			Rectangle notificationPath = {22478}.GetNotificationPath({19721});
			Vector2 vector = {19720}.Pos.XY + new Vector2(4f, 5f);
			Color color = Color.White * scale;
			gs.DrawCustomTexture(chatAtl, notificationPath, vector, color);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0005FD34 File Offset: 0x0005DF34
		private void {19722}()
		{
			Vector2 {14078} = new Vector2((float)Engine.GS.UIArea.Width - {19717}.headWidth, 70f);
			StackForm stackForm = this.{19757};
			if (stackForm != null)
			{
				stackForm.Clear();
			}
			if (this.{19757} == null)
			{
				this.{19757} = new StackForm({14078}, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					BorderThickness = -3f
				};
				base.AddChild(this.{19757});
			}
			else
			{
				this.{19757}.Pos = this.{19757}.Pos.SetXY({14078});
			}
			if (this.{19757}.Opacity == 0f)
			{
				new UiOpacityAnimation(this.{19757}, 1f, 300f);
			}
			if (Session.SecToServerReboot > 0f)
			{
				this.AppendQuestInfo(Local.server_update, Local.server_update_tt, Local.till_server_update(StringHelper.TimeMMMSS((double)Session.SecToServerReboot)), true, null, false, true, null, 0);
			}
			if (Session.EngagingInPortBattleWorld != PbsBatlleSide.None && Global.Player.MapInfo.IsWorldmap)
			{
				Form form = this.AppendQuestInfo(Local.port_battle, " " + Environment.NewLine + " ", StringHelper.TimeMMMSS((double)Session.PortBattleInfo.newTimeoutValue) + "  ", Session.PortBattleInfo.newTimeoutValue < 240f, new Action<TextBlockBuilder>(this.{19739}), false, false, null, 30);
				if (form != null)
				{
					StackForm stackForm2 = new StackForm(form.Pos.XY + new Vector2(30f, 13f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					int num = Session.EngagingInPortBattlePort.PbSystem.Count((KeyValuePair<int, PbsBuildingPlaceInfo> {19774}) => {19774}.Value.GroupID == WorldObjectID.WGuildFortTower);
					if (num > 10)
					{
						stackForm2.BorderThickness = -4f;
					}
					if (num > 15)
					{
						stackForm2.BorderThickness = -7f;
					}
					for (int i = 0; i < num - (int)Session.PortBattleInfo.NumLiveTowers - (int)Session.PortBattleInfo.NumCapturedTowers; i++)
					{
						stackForm2.AddItem(new UiControl[]
						{
							new Form(Vector2.Zero, {19717}.cTowerDest, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							{
								AnimatedFocus = false
							}
						});
					}
					for (int j = 0; j < (int)Session.PortBattleInfo.NumCapturedTowers; j++)
					{
						stackForm2.AddItem(new UiControl[]
						{
							new Form(Vector2.Zero, {19717}.cTowerCaptured, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							{
								AnimatedFocus = false
							}
						});
					}
					for (int k = 0; k < (int)Session.PortBattleInfo.NumLiveTowers; k++)
					{
						stackForm2.AddItem(new UiControl[]
						{
							new Form(Vector2.Zero, {19717}.cTowerLive, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							{
								AnimatedFocus = false
							}
						});
					}
					stackForm2.AddItem(new UiControl[]
					{
						new Form(Vector2.Zero, (Session.PortBattleInfo.NumLiveForts > 0) ? {19717}.cFortLive : {19717}.cFortDest, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							AnimatedFocus = false
						}
					});
					form.AddChild(stackForm2);
				}
			}
			if (Session.IsInRandomPvpZone)
			{
				string text = "";
				string str = text;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
				defaultInterpolatedStringHandler.AppendFormatted(Local.randomPvpArena_info_label_1);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(Session.RandomPvpZoneRewardAmount);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(Local.gold_postfix);
				text = str + defaultInterpolatedStringHandler.ToStringAndClear() + Environment.NewLine;
				string str2 = text;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler2.AppendFormatted(Local.randomPvpArena_info_label_2);
				defaultInterpolatedStringHandler2.AppendLiteral(" ");
				defaultInterpolatedStringHandler2.AppendFormatted<int>(Session.RandomPvpZoneKills);
				text = str2 + defaultInterpolatedStringHandler2.ToStringAndClear() + Environment.NewLine;
				string str3 = text;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(3, 3);
				defaultInterpolatedStringHandler3.AppendFormatted(Local.randomPvpArena_info_label_3);
				defaultInterpolatedStringHandler3.AppendLiteral(" +");
				defaultInterpolatedStringHandler3.AppendFormatted<int>(Session.RandomPvpZoneRewardStep);
				defaultInterpolatedStringHandler3.AppendLiteral(" ");
				defaultInterpolatedStringHandler3.AppendFormatted(Local.per_second_postfix);
				text = str3 + defaultInterpolatedStringHandler3.ToStringAndClear() + Environment.NewLine;
				string str4 = text;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(3, 3);
				defaultInterpolatedStringHandler4.AppendFormatted(Local.randomPvpArena_info_label_4);
				defaultInterpolatedStringHandler4.AppendLiteral(" +");
				defaultInterpolatedStringHandler4.AppendFormatted<short>(10000);
				defaultInterpolatedStringHandler4.AppendLiteral(" ");
				defaultInterpolatedStringHandler4.AppendFormatted(Local.gold_postfix);
				text = str4 + defaultInterpolatedStringHandler4.ToStringAndClear();
				this.AppendQuestInfo(Local.action_randomPvpArena_name, text, null, false, null, false, false, null, 0);
			}
			if (Global.Player.MapInfo.IsWorldmap && Session.Account.EducationQuest.HasFlag(EducationOnboarding.OpenLogbook))
			{
				using (IEnumerator<QuestRunningProgress> enumerator = ((IEnumerable<QuestRunningProgress>)Session.Account.Quests.ProgressRunningQuests).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						QuestRunningProgress q = enumerator.Current;
						QuestInfo info = q.Info;
						if (!Global.Settings.DisableTrackingForQuests.Contains(info.ID))
						{
							this.AppendQuestInfo(info.GetName(Session.Account), q.CurrentStep.GetTextWhatToDo(Global.Player, q), (q.Info.LimitedTimeMinutes == -1) ? "" : ((Global.Player.MapInfo.IsEvaluteQuestsTime ? "" : "|| ") + StringHelper.TimeMMMSS((double)q.TimeSecLeft)), q.TimeSecLeft < 180f, delegate(TextBlockBuilder {19777})
							{
								QuestTransferOrder questTransferOrder = q.CurrentStep as QuestTransferOrder;
								if (questTransferOrder != null && Global.Player.ResourcesOfHold[(int)questTransferOrder.ResInfo.ID] < questTransferOrder.ResourceCount.Value)
								{
									{19777}.WriteLine(Local.TradePortInterface_24 + Global.Player.ResourcesOfHold[(int)questTransferOrder.ResInfo.ID].ToString(), Color.OrangeRed);
								}
							}, q.CurrentStep.Target != null && q.CurrentStep.Target.ShowOnMinimap, false, delegate(ClickUiEventArgs {19778})
							{
								if ({19779}.CurrentInstance == null)
								{
									new {19779}(false, null, info);
								}
							}, 0);
						}
					}
				}
				if (Session.LastMinimapAndGroupUpdate.trackingMercanaryOrders.Size > 0)
				{
					string text2 = "";
					foreach (MercanaryOrderTrackingInfo mercanaryOrderTrackingInfo in ((IEnumerable<MercanaryOrderTrackingInfo>)Session.LastMinimapAndGroupUpdate.trackingMercanaryOrders))
					{
						text2 = text2 + mercanaryOrderTrackingInfo.ToString() + Environment.NewLine;
					}
					this.AppendQuestInfo(Local.mercanary, text2, null, false, null, false, false, null, 0);
				}
			}
			if (Session.CurrentArenaSession != null)
			{
				string {19725} = (Session.CurrentArenaSession.ModeInfo.TimeLimitMin != int.MaxValue) ? StringHelper.TimeMMMSS((double)Session.CurrentArenaSession.RemainTimeSec) : null;
				if ((Session.CurrentArenaSession.ModeInfo.WinKretery == ArenaWinKretery.KillAll || Session.CurrentArenaSession.ModeInfo.WinKretery == ArenaWinKretery.KillCount) && !Session.CurrentArenaSession.ModeInfo.IsDuetl)
				{
					this.AppendQuestInfo(Local.kill_all, string.Empty, {19725}, false, new Action<TextBlockBuilder>(this.{19741}), false, true, null, 0);
				}
				if (Session.CurrentArenaSession.ModeInfo.WinKretery == ArenaWinKretery.LootCount)
				{
					this.AppendQuestInfo((Session.CurrentArenaSession.RemainLoot == 0) ? Local.give_loots : Local.give_loots_count(Session.CurrentArenaSession.RemainLoot), string.Empty, {19725}, false, new Action<TextBlockBuilder>(this.{19743}), false, true, null, 0);
				}
			}
			foreach (LogbookTrackingNote logbookTrackingNote in ((IEnumerable<LogbookTrackingNote>)Global.Settings.TrackingNotes))
			{
				ValueTuple<string, string, bool, string> text3 = logbookTrackingNote.GetText();
				if (text3.Item3)
				{
					this.AppendQuestInfo(text3.Item1, text3.Item2, "", false, null, false, false, null, 0);
				}
			}
			{18593} currentInstance = {18593}.CurrentInstance;
			if (!string.IsNullOrEmpty((currentInstance != null) ? currentInstance.CurrentQuestPortMode : null))
			{
				this.AppendQuestInfo(Local.education_quest_1, {18593}.CurrentInstance.CurrentQuestPortMode, "", false, null, false, false, null, 0);
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00060568 File Offset: 0x0005E768
		private Form AppendQuestInfo(string {19723}, string {19724}, string {19725}, bool {19726} = false, Action<TextBlockBuilder> {19727} = null, bool {19728} = false, bool {19729} = false, Action<ClickUiEventArgs> {19730} = null, int {19731} = 0)
		{
			if (!{19717}.questsAreVisible && this.{19757}.GetChildren.Size >= 1 && !{19729})
			{
				return null;
			}
			Color whiteSmoke = Color.WhiteSmoke;
			int num = this.{19757}.GetChildren.Count((UiControl {19775}) => {19775} is Form);
			if (num >= 5)
			{
				if (num > 5)
				{
					return null;
				}
				Rectangle rectangle = {19717}.c_quest_head.SetHeight(33f);
				Form form = new Form(new Marker(0f, 0f, {19717}.headWidth, (float)rectangle.Height), rectangle, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					RenderToDepthMap = false
				};
				form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, whiteSmoke, "...", PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, 33f);
				this.{19757}.AddItem(new UiControl[]
				{
					form
				});
				return null;
			}
			else
			{
				Form form2 = new Form(new Marker(0f, 0f, {19717}.headWidth, (float){19717}.c_quest_head.Height), {19728} ? {19717}.c_quest_head_redSight : {19717}.c_quest_head, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					RenderToDepthMap = false
				};
				if (this.{19757}.GetChildren.Size == 0)
				{
					Button button = new Button(new Marker(0f, 0f, 21f, 21f), AtlasGameGui.newToolList_item, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					button.SetText(Global.Settings.kb_OpenQuestPanel.KeyToString, Fonts.Philosopher_14Bold, Color.WhiteSmoke, false);
					form2.AddChildPos(button, PositionAlignment.LeftUp, PositionAlignment.Center, {19717}.headWidth - 28f);
					button.UpdateComplete += delegate(UiControl {19776})
					{
						if (Global.Settings.kb_OpenQuestPanel.IsClick && GameScene.GameHasInputFocus)
						{
							{22478} currentInstance = {22478}.CurrentInstance;
							if (currentInstance != null && !currentInstance.IsInput)
							{
								{19776}.ImitateClick(false);
							}
						}
					};
					button.EvClick += this.{19745};
				}
				this.{19757}.AddItem(new UiControl[]
				{
					form2
				});
				if (!{19729} && !{19717}.questsAreVisible && this.{19757}.GetChildren.Size >= 1)
				{
					form2.TexturePath = {19717}.c_quest_head_rolledUp;
					return null;
				}
				float {287} = form2.Pos.WH.X * 0.75f;
				CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
				string {13345} = StringHelper.TrimByLength(philosopher_, {19723}, {287});
				form2.AddChildPos(new Label(Vector2.Zero, philosopher_, whiteSmoke, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick({19730}), PositionAlignment.LeftUp, PositionAlignment.Center, 33f);
				TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_10, -2f);
				if (!string.IsNullOrEmpty({19724}))
				{
					textBlockBuilder.WriteLines({19724}, whiteSmoke * 0.9f, textBlockBuilder.defaultFont, (float)({19717}.c_quest_head.Width - 45), new float?(0f));
				}
				if (!string.IsNullOrEmpty({19725}))
				{
					textBlockBuilder.WriteLines({19725}, {19726} ? new Color(214, 124, 121) : new Color(178, 206, 144), Fonts.Philosopher_14, (float)({19717}.c_quest_head.Width - 45), new float?(0f));
				}
				if (textBlockBuilder.blocks.Size > 0 || {19727} != null)
				{
					if ({19727} != null)
					{
						{19727}(textBlockBuilder);
					}
					StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					Form form3 = new Form(new Marker(0f, 0f, (float){19717}.c_quest_body.Width, textBlockBuilder.Size.Y + 15f + (float){19731}), {19717}.c_quest_body, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					};
					form3.AddChildPos(textBlockBuilder.Create(Vector2.Zero), PositionAlignment.LeftUp, PositionAlignment.Center, 33f);
					form3.ExClick({19730});
					stackForm.AddSpace(1f);
					stackForm.AddItem(new UiControl[]
					{
						form3
					});
					this.{19757}.AddItem(new UiControl[]
					{
						stackForm
					});
					return form3;
				}
				return form2;
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00060958 File Offset: 0x0005EB58
		private void {19732}(bool {19733})
		{
			{19717}.questsAreVisible = {19733};
			this.{19757}.RemoveAnimations();
			new UiMarkerAndOpacityAnimation(this.{19757}, 1f, 0f, this.{19757}.Pos, this.{19757}.Pos.Offset((float)({19717}.questsAreVisible ? 0 : 10), 0f), 300f, UiAmimationCurve.Linear);
			new UiActionsSleep(this, 300f);
			new UiActor(this, new Action(this.{19722}));
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UISwitch, 0.03f, 1f);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000609FC File Offset: 0x0005EBFC
		private void {19734}(TextBlockBuilder {19735}, int {19736}, int {19737}, string {19738} = null)
		{
			{19735}.Write({19736}.ToString(), {19717}.c_greenScoreColor, Fonts.Philosopher_14Bold, null, false);
			{19735}.Write(" ⚔ ", Color.LightGray, Fonts.Philosopher_14Bold, null, false);
			{19735}.Write({19737}.ToString(), {19717}.c_redScoreColor, Fonts.Philosopher_14Bold, null, false);
			if ({19738} != null)
			{
				{19735}.Write(" " + {19738}, Color.LightGray, Fonts.Philosopher_14Bold, null, false);
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00060E08 File Offset: 0x0005F008
		[CompilerGenerated]
		private void {19739}(TextBlockBuilder {19740})
		{
			{19740}.Write("", Color.White, 60f, false);
			if (Session.PortBattleInfo.timeBoosted)
			{
				{19740}.WriteImage(AtlasGameGui.Texture.Tex, {19717}.c_timeBoostedMarker, 1f, null);
			}
			else if (Session.PortBattleInfo.timeLimitAchieved)
			{
				{19740}.WriteImage(AtlasGameGui.Texture.Tex, {19717}.c_timeoutMarker, 1f, null);
			}
			{19740}.WriteLine("", Color.White);
			short {19736} = (Session.EngagingInPortBattle == PbsBatlleSide.Attacker) ? Session.PortBattleInfo.RespAvailableAttacker : Session.PortBattleInfo.RespAvailableDefender;
			short {19737} = (Session.EngagingInPortBattle == PbsBatlleSide.Defender) ? Session.PortBattleInfo.RespAvailableAttacker : Session.PortBattleInfo.RespAvailableDefender;
			this.{19734}({19740}, (int){19736}, (int){19737}, null);
			{19740}.Write("  " + Local.remain, Color.Gray * 0.5f, Fonts.Arial_10Bold, null, false);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00060F16 File Offset: 0x0005F116
		[CompilerGenerated]
		private void {19741}(TextBlockBuilder {19742})
		{
			{19742}.WriteLine("", Color.White);
			this.{19734}({19742}, Session.CurrentArenaSession.ScoreMyComand, Session.CurrentArenaSession.ScoreEnemyComand, Local.kills_notation);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00060F48 File Offset: 0x0005F148
		[CompilerGenerated]
		private void {19743}(TextBlockBuilder {19744})
		{
			{19744}.WriteLine("", Color.White);
			this.{19734}({19744}, Session.CurrentArenaSession.ScoreMyComand, Session.CurrentArenaSession.ScoreEnemyComand, Local.given_notation);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00060F7A File Offset: 0x0005F17A
		[CompilerGenerated]
		private void {19745}(ClickUiEventArgs {19746})
		{
			{19717}.questsAreVisible = !{19717}.questsAreVisible;
			this.{19732}({19717}.questsAreVisible);
		}

		// Token: 0x04000ABC RID: 2748
		public static bool IsRedPing = true;

		// Token: 0x04000ABD RID: 2749
		public static {19717} CurrentInstance;

		// Token: 0x04000ABE RID: 2750
		private const float c_UpdateTextsTimer_ms = 300f;

		// Token: 0x04000ABF RID: 2751
		private static readonly Color c_greenScoreColor = new Color(69, 209, 85);

		// Token: 0x04000AC0 RID: 2752
		private static readonly Color c_redScoreColor = new Color(219, 171, 79);

		// Token: 0x04000AC1 RID: 2753
		private static readonly Rectangle cTowerLive = new Rectangle(2700, 0, 22, 22);

		// Token: 0x04000AC2 RID: 2754
		private static readonly Rectangle cTowerDest = new Rectangle(2723, 0, 22, 22);

		// Token: 0x04000AC3 RID: 2755
		private static readonly Rectangle cTowerCaptured = new Rectangle(2730, 23, 22, 22);

		// Token: 0x04000AC4 RID: 2756
		private static readonly Rectangle cFortLive = new Rectangle(2746, 0, 29, 22);

		// Token: 0x04000AC5 RID: 2757
		private static readonly Rectangle cFortDest = new Rectangle(2700, 23, 29, 22);

		// Token: 0x04000AC6 RID: 2758
		private static readonly Rectangle main = new Rectangle(1940, 310, 394, 86);

		// Token: 0x04000AC7 RID: 2759
		private static readonly Rectangle c_bt_wiki = new Rectangle(7, 557, 86, 86);

		// Token: 0x04000AC8 RID: 2760
		private static readonly Rectangle c_bt_quest = new Rectangle(193, 557, 86, 86);

		// Token: 0x04000AC9 RID: 2761
		private static readonly Rectangle c_bt_people = new Rectangle(287, 557, 86, 86);

		// Token: 0x04000ACA RID: 2762
		private static readonly Rectangle c_bt_guild = new Rectangle(379, 557, 86, 86);

		// Token: 0x04000ACB RID: 2763
		private static readonly Rectangle c_bt_guild_rating = new Rectangle(706, 557, 86, 86);

		// Token: 0x04000ACC RID: 2764
		private static readonly Rectangle c_bt_rating = new Rectangle(611, 557, 86, 86);

		// Token: 0x04000ACD RID: 2765
		private static readonly Rectangle c_bt_event = new Rectangle(50, 734, 86, 86);

		// Token: 0x04000ACE RID: 2766
		private static readonly Rectangle c_bt_wiki_f = new Rectangle(7, 644, 86, 86);

		// Token: 0x04000ACF RID: 2767
		private static readonly Rectangle c_bt_quest_f = new Rectangle(193, 644, 86, 86);

		// Token: 0x04000AD0 RID: 2768
		private static readonly Rectangle c_bt_guild_rating_f = new Rectangle(706, 644, 86, 86);

		// Token: 0x04000AD1 RID: 2769
		private static readonly Rectangle c_bt_people_f = new Rectangle(287, 644, 86, 86);

		// Token: 0x04000AD2 RID: 2770
		private static readonly Rectangle c_bt_guild_f = new Rectangle(379, 644, 86, 86);

		// Token: 0x04000AD3 RID: 2771
		private static readonly Rectangle c_bt_rating_f = new Rectangle(611, 644, 86, 86);

		// Token: 0x04000AD4 RID: 2772
		private static readonly Rectangle c_bt_event_f = new Rectangle(137, 734, 86, 87);

		// Token: 0x04000AD5 RID: 2773
		private static readonly Rectangle c_bt_pingOnlineFps = new Rectangle(476, 557, 122, 86);

		// Token: 0x04000AD6 RID: 2774
		private static readonly Rectangle c_bt_pingOnlineFps_f = new Rectangle(476, 644, 122, 86);

		// Token: 0x04000AD7 RID: 2775
		private static readonly Rectangle c_actionForm = new Rectangle(2285, 397, 208, 102);

		// Token: 0x04000AD8 RID: 2776
		private static readonly Rectangle c_actionForm_s = new Rectangle(2285, 397, 208, 77);

		// Token: 0x04000AD9 RID: 2777
		private static readonly Rectangle c_timeoutMarker = new Rectangle(2494, 482, 14, 17);

		// Token: 0x04000ADA RID: 2778
		private static readonly Rectangle c_timeBoostedMarker = new Rectangle(2494, 464, 14, 17);

		// Token: 0x04000ADB RID: 2779
		private static readonly Rectangle c_arenaWaitBattlesNotice = new Rectangle(1871, 481, 61, 44);

		// Token: 0x04000ADC RID: 2780
		private static readonly Rectangle c_quest_head = new Rectangle(1944, 393, 249, 41);

		// Token: 0x04000ADD RID: 2781
		private static readonly Rectangle c_quest_head_redSight = new Rectangle(927, 79, 249, 41);

		// Token: 0x04000ADE RID: 2782
		private static readonly Rectangle c_quest_head_rolledUp = new Rectangle(2521, 761, 249, 41);

		// Token: 0x04000ADF RID: 2783
		private static readonly Rectangle c_quest_body = new Rectangle(1759, 728, 249, 54);

		// Token: 0x04000AE0 RID: 2784
		private static readonly Rectangle c_random_pvp_background = new Rectangle(1428, 365, 1, 1);

		// Token: 0x04000AE1 RID: 2785
		private static readonly Vector2 p_header = new Vector2(13f, 10f);

		// Token: 0x04000AE2 RID: 2786
		private static readonly Vector2 p_tasTextStart = new Vector2(13f, 34f);

		// Token: 0x04000AE3 RID: 2787
		private static readonly Vector2 p_time = new Vector2(13f, 78f);

		// Token: 0x04000AE4 RID: 2788
		private Form {19747};

		// Token: 0x04000AE5 RID: 2789
		private AnimatedButton {19748};

		// Token: 0x04000AE6 RID: 2790
		public AnimatedButton button_GameWiki;

		// Token: 0x04000AE7 RID: 2791
		private AnimatedButton {19749};

		// Token: 0x04000AE8 RID: 2792
		private AnimatedButton {19750};

		// Token: 0x04000AE9 RID: 2793
		private AnimatedButton {19751};

		// Token: 0x04000AEA RID: 2794
		private AnimatedButton {19752};

		// Token: 0x04000AEB RID: 2795
		private StackForm {19753};

		// Token: 0x04000AEC RID: 2796
		private Form {19754};

		// Token: 0x04000AED RID: 2797
		private Vector2 {19755};

		// Token: 0x04000AEE RID: 2798
		private static bool questsAreVisible = true;

		// Token: 0x04000AEF RID: 2799
		private Tlist<AnimatedButton> {19756};

		// Token: 0x04000AF0 RID: 2800
		private StackForm {19757};

		// Token: 0x04000AF1 RID: 2801
		private Timer {19758} = new Timer(900f);

		// Token: 0x04000AF2 RID: 2802
		private StackForm {19759};

		// Token: 0x04000AF3 RID: 2803
		private Label {19760};

		// Token: 0x04000AF4 RID: 2804
		private Label {19761};

		// Token: 0x04000AF5 RID: 2805
		private Label {19762};

		// Token: 0x04000AF6 RID: 2806
		private bool {19763};
	}
}
