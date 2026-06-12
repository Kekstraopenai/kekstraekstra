using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000263 RID: 611
	internal class {20293} : StackForm
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x00072FD8 File Offset: 0x000711D8
		public {20293}() : base(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			base.AddItem(new UiControl[]
			{
				this.{20294}()
			});
			base.AddItem(new UiControl[]
			{
				this.{20295}()
			});
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00073024 File Offset: 0x00071224
		private Form {20294}()
		{
			Form form = new Form(new Marker(0f, 0f, (float){20364}.ContentWidth, 100f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			LabelButton sortLabel = null;
			sortLabel = new LabelButton(new Vector2(13f, 41f), Local.GuildWindow_61, Fonts.Arial_12, Color.White * 0.75f, Color.White, delegate(ClickUiEventArgs {20308})
			{
				this.{20299} = !this.{20299};
				this.{20295}();
				sortLabel.Text = (this.{20299} ? Local.GuildWindow_61 : Local.GuildWindow_62);
			});
			form.AddChild(sortLabel);
			StackForm stackForm = new StackForm(new Vector2(237f, 55f), UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if (!Session.Guild.IsFlotilia)
			{
				CheckboxControl checkboxControl = new CheckboxControl(Vector2.Zero, CommonAtlas.vd_checkBox_26px_true, CommonAtlas.vd_checkBox_26px_false, Session.Guild.ActiveForNewPlayers, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.guild_active_recruit, Fonts.Philosopher_14, Color.LightGray).ExToolTip(new ToolTip(new ToolTipState("", Local.guild_active_recruit_tt, Array.Empty<ToolTipCharacteristics>())));
				checkboxControl.Opacity = (Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury ? 1f : 0.5f);
				checkboxControl.EvCheck += delegate(CheckboxCheckedEventArgs {20301})
				{
					if (!Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury)
					{
						new {17107}("", Local.GuildWindow_3(Local.guildright_tt_AllowEditGuildAndFortAndTreasury), "", null, true, null, new string[]
						{
							Local.close
						});
						{20301}.UndoCheck = true;
						return;
					}
					Global.Network.Send(new OnGuildMembersChanges(OnGuildMembersChanges.GAction.SetActiveForNewPlayers, 0U, ({20301}.NewValue > false) ? 1 : 0, ""));
					Session.Guild.ActiveForNewPlayers = {20301}.NewValue;
				};
				stackForm.AddItem(new UiControl[]
				{
					checkboxControl
				});
				stackForm.AddSpace(15f);
			}
			Button button = new Button(new Vector2(337f, 25f), CommonAtlas.basicButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.GuildWindow_63, Fonts.Arial_12, Color.White * 0.7f, false);
			button.EvClick += delegate(ClickUiEventArgs {20302})
			{
				if (Session.MyMemberInGuild.Rights.RankId == GuildAccessRightsInfo.FounderRights.RankId)
				{
					new {17312}(Local.guild_remove, delegate()
					{
						new {17312}(Local.guild_remove2, delegate()
						{
							{20364}.CurrentInstance.RemoveFromContainer();
							Global.Network.Send(new OnGuildMembersChanges(OnGuildMembersChanges.GAction.DestroyGuild, 0U, 0, ""));
						}, true);
					}, true);
					return;
				}
				new {17312}(Local.GuildWindow_66, delegate()
				{
					{20364}.CurrentInstance.RemoveFromContainer();
					Global.Network.Send(new OnGuildMembersChanges(OnGuildMembersChanges.GAction.LeaveGuild, 0U, 0, ""));
				}, false);
			};
			stackForm.AddItem(new UiControl[]
			{
				button
			});
			Button button2 = new Button(new Vector2(507f, 25f), CommonAtlas.basicButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.reset, Fonts.Arial_12, Color.White * 0.7f, false);
			button2.Pos = button2.Pos.SetWidth(button2.Pos.WH.X * 0.8f);
			button2.ToolTipState = new ToolTipState("", Local.guildMember_resetFields_tt, Array.Empty<ToolTipCharacteristics>());
			button2.EvClick += delegate(ClickUiEventArgs {20303})
			{
				if (!Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury)
				{
					new {17107}("", Local.GuildWindow_3(Local.guildright_tt_AllowEditGuildAndFortAndTreasury), "", null, true, null, new string[]
					{
						Local.close
					});
					return;
				}
				Global.Network.Send(new OnGuildMembersChanges(OnGuildMembersChanges.GAction.ResetFields, 0U, 0, ""));
			};
			button2.Opacity = (Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury ? 1f : 0.5f);
			stackForm.AddItem(new UiControl[]
			{
				button2
			});
			form.AddChild(stackForm);
			Form form2 = form;
			Vector2 {13342} = new Vector2(13f, 26f);
			CustomSpriteFont arial_ = Fonts.Arial_12;
			Color lightGray = Color.LightGray;
			string[] array = new string[5];
			array[0] = Local.online;
			array[1] = ": ";
			array[2] = Session.Guild.Members.Count((GuildMember {20304}) => {20304}.Cached.IsOnline).ToString();
			array[3] = " / ";
			array[4] = Session.Guild.Members.Size.ToString();
			form2.AddChild(new Label({13342}, arial_, lightGray, string.Concat(array), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Label(new Vector2(13f, 7f), Fonts.Philosopher_16Bold, Color.LightGray, string.Concat(new string[]
			{
				Local.members,
				": ",
				Session.Guild.Members.Size.ToString(),
				" / ",
				Session.Guild.MaxPlayers.ToString()
			}), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			return form;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00073404 File Offset: 0x00071604
		private StackForm {20295}()
		{
			if (this.{20300} == null)
			{
				this.{20300} = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			}
			this.{20300}.Clear();
			Tlist<GuildMember> tlist = Session.Guild.Members.Clone();
			tlist.SortTop(new Func<GuildMember, float>(this.{20297}));
			foreach (GuildMember {20296} in ((IEnumerable<GuildMember>)tlist))
			{
				this.{20300}.AddItem(new UiControl[]
				{
					{20293}.CreateMemberItem({20296})
				});
			}
			if (Session.Guild.MaxPlayers < GuildCommon.GuildMaxPlayersLim(Session.Guild.IsFlotilia))
			{
				this.{20300}.AddItem(new UiControl[]
				{
					new {20381}(GuildTemporaryEffect.Type.UpgradeStaff, 0, false)
				});
			}
			return this.{20300};
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000734E4 File Offset: 0x000716E4
		private static Form CreateMemberItem(GuildMember {20296})
		{
			bool isMe = {20296}.SID == Session.Account.SID;
			Form form = new Form(Vector2.Zero, isMe ? {20293}.c_meBack : {20293}.c_otherMemberBack, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form.AddChild(new Label(new Vector2(45f, 7f), Fonts.Arial_12, {20296}.Cached.HasReputationProblem ? Color.Lerp(Color.OrangeRed, Color.White, 0.4f) : Color.White, {20296}.Cached.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Label(new Vector2(45f, 26f), Fonts.Arial_12, Color.White * 0.6f, {20296}.Rights.DisplayName, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(CommonAtlas.c_ranks[(int)({20296}.RankId - 1)], new Marker(3f, 2f, ref CommonAtlas.c_ranks[0]));
			LiveLabel {12952} = new LiveLabel(Vector2.Zero, Fonts.Arial_12, Color.White * 0.4f, delegate(LiveLabel {20309})
			{
				{20309}.BasicColor = ({20296}.Cached.IsOnline ? Color.SoftLime : (Color.White * 0.4f));
				if (!{20296}.Cached.IsOnline)
				{
					string guildWindow_ = Local.GuildWindow_53;
					string str;
					if ({20296}.Cached.LastActivity.Year != 0)
					{
						LocalizedDateTime localizedDateTime = new LocalizedDateTime({20296}.Cached.LastActivity, false);
						localizedDateTime.UsePrefix = false;
						str = localizedDateTime.GetDate();
					}
					else
					{
						str = "-";
					}
					return guildWindow_ + str;
				}
				return Local.online;
			}, 1000);
			form.AddChildPos({12952}, PositionAlignment.LeftUp, PositionAlignment.Center, 220f);
			if ({20296}.Cached.ConquerorBadgesParticipation > 0f)
			{
				form.AddChildPos(new Label(Vector2.Zero, Fonts.Arial_12, Color.Lerp(Color.Gray, Color.Orange, 0.5f), Math.Round((double){20296}.Cached.ConquerorBadgesParticipation, 1).ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExToolTip(new ToolTip(new ToolTipState("", Local.guildMember_conquerorpart, Array.Empty<ToolTipCharacteristics>()))), PositionAlignment.LeftUp, PositionAlignment.Center, 410f);
			}
			if ({20296}.Cached.AchievementRating > 0)
			{
				form.AddChildPos(new Label(Vector2.Zero, Fonts.Arial_12, Color.Lerp(Color.Gray, Color.Cyan, 0.5f), {20296}.Cached.AchievementRating.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExToolTip(new ToolTip(new ToolTipState("", Local.common_rating, Array.Empty<ToolTipCharacteristics>()))), PositionAlignment.LeftUp, PositionAlignment.Center, 500f);
			}
			if ({20296}.Cached.Refill != null && !{20296}.Cached.Refill.IsEmpty)
			{
				form.AddChildPos(new Label(Vector2.Zero, Fonts.Arial_12, Color.Lerp(Color.Gray, Color.Lime, 0.5f), {20296}.Cached.Refill.GetTotalItemsCount().ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExToolTip(new ToolTip(0f, float.MaxValue, new ToolTipState(Local.guildMember_refill, (from {20305} in {20296}.Cached.Refill.ResourceInfo
				select new ToolTipCharacteristics({20305}.Info.Name, {20305}.Count.ToString() ?? "")).ToArray<ToolTipCharacteristics>(), 20, CommonAtlas.tt_arrowKeys_left, CommonAtlas.tt_arrowKeys_right, CommonAtlas.Texture.Tex, null))), PositionAlignment.LeftUp, PositionAlignment.Center, 590f);
			}
			if ({20296}.Cached.Withdraw != null && !{20296}.Cached.Withdraw.IsEmpty)
			{
				form.AddChildPos(new Label(Vector2.Zero, Fonts.Arial_12, Color.Lerp(Color.Gray, Color.Red, 0.5f), {20296}.Cached.Withdraw.GetTotalItemsCount().ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExToolTip(new ToolTip(0f, float.MaxValue, new ToolTipState(Local.guildMember_withdraw, (from {20306} in {20296}.Cached.Withdraw.ResourceInfo
				select new ToolTipCharacteristics({20306}.Info.Name, {20306}.Count.ToString() ?? "")).ToArray<ToolTipCharacteristics>(), 20, CommonAtlas.tt_arrowKeys_left, CommonAtlas.tt_arrowKeys_right, CommonAtlas.Texture.Tex, null))), PositionAlignment.LeftUp, PositionAlignment.Center, 680f);
			}
			form.ToolTip = new ToolTip(delegate(UiControl {20310})
			{
				TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_10, 1f);
				if ({20296}.Cached.HasReputationProblem)
				{
					textBlockBuilder.WriteLine(Local.member_reputation_problem, Color.OrangeRed);
				}
				{20296}.Rights.GuildMemberRightsToolTip(textBlockBuilder);
				if (!isMe)
				{
					textBlockBuilder.WriteLine(Local.GuildWindow_55, Color.White);
				}
				else if (Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury)
				{
					textBlockBuilder.WriteLine(Local.GuildWindow_56, Color.White);
					textBlockBuilder.WriteLine(Local.GuildWindow_57, Color.White);
				}
				return new ToolTipState(textBlockBuilder);
			});
			form.EvClick += delegate(ClickUiEventArgs {20311})
			{
				if (!isMe)
				{
					new {17558}(new {17549}({20296}.SID, {20296}.Cached.Name, new {17549}.OptionalAction[]
					{
						{17549}.OptionalAction.RemoveFromGuildOrChangeStatus
					}));
					return;
				}
				if (Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury)
				{
					if (Session.Guild.Mint == 0)
					{
						new {17312}(Local.GuildWindow_58);
						return;
					}
					new {21838}(Local.GuildWindow_59, Local.GuildWindow_60, delegate(int {20307})
					{
						Global.Network.Send(new OnChangeGuildMintOrTreasuryMsg(false, {20307}, Session.Account.SID));
					}, Session.Guild.Mint, null, new int?(0), null, null, null);
				}
			};
			return form;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x000739B0 File Offset: 0x00071BB0
		[NullableContext(1)]
		[CompilerGenerated]
		private float {20297}(GuildMember {20298})
		{
			if (this.{20299})
			{
				return (float){20298}.RankId;
			}
			if (!{20298}.Cached.IsOnline)
			{
				return -(float)DateTime.Now.Subtract({20298}.Cached.LastActivity.ToDateTime).TotalHours;
			}
			return 1f;
		}

		// Token: 0x04000CDA RID: 3290
		private static readonly Rectangle c_meBack = new Rectangle(884, 3622, 826, 52);

		// Token: 0x04000CDB RID: 3291
		private static readonly Rectangle c_otherMemberBack = new Rectangle(884, 3675, 826, 52);

		// Token: 0x04000CDC RID: 3292
		private bool {20299} = true;

		// Token: 0x04000CDD RID: 3293
		private StackForm {20300};
	}
}
