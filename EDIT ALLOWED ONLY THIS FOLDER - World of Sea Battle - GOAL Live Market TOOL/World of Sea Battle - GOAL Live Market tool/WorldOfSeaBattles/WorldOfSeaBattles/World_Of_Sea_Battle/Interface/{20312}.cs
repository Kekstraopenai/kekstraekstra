using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000267 RID: 615
	internal class {20312} : StackForm
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00073EB6 File Offset: 0x000720B6
		private static bool EnablePolitics
		{
			get
			{
				return !Session.Guild.IsFlotilia;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00073EC5 File Offset: 0x000720C5
		private static FractionID MyFraction
		{
			get
			{
				return Session.Guild.Fraction;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x00073ED1 File Offset: 0x000720D1
		private static GuildAccessRightsInfo MyRights
		{
			get
			{
				return Session.MyMemberInGuild.Rights;
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00073EE0 File Offset: 0x000720E0
		public {20312}() : base(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.BorderThickness = 10f;
			base.AddItem(new UiControl[]
			{
				{20312}.CreateFractionSelector()
			});
			base.AddItem(new UiControl[]
			{
				{20312}.CreateOptions()
			});
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00073F30 File Offset: 0x00072130
		private static {20105} CreateFractionSelector()
		{
			FractionID[] source = new FractionID[]
			{
				FractionID.Pirate,
				FractionID.TradeUnion,
				FractionID.None
			}.Concat(FractionDecorator.Nations).ToArray<FractionID>();
			if (!source.Contains({20312}.MyFraction))
			{
				source = source.Append({20312}.MyFraction).ToArray<FractionID>();
			}
			{20105} fractionSelector = new {20105}(Vector2.Zero, source.ToArray<FractionID>());
			fractionSelector.OnFractionChanged = delegate(FractionID {20331})
			{
				GuildTemporaryEffect guildTemporaryEffect = Session.Guild.Effects.FirstOrDefault((GuildTemporaryEffect {20323}) => {20323}.Case == GuildTemporaryEffect.Type.ChangeFractionRistriction);
				double num = (guildTemporaryEffect == null) ? 0.0 : guildTemporaryEffect.RemainingTimeSec;
				if ({20331} != {20312}.MyFraction && num == 0.0 && {20312}.EnablePolitics)
				{
					List<string> requirements = new List<string>();
					if ({20331}.IsNation())
					{
						requirements.Add(Local.guildChangeFractionToNation(25));
						requirements.Add(Local.guildChangeFractionNeedMoreMembers(GuildCommon.MinMembersForNation(Session.Game.WorldPorts.FractionBonusHelper({20331}))));
					}
					if ({20312}.MyFraction.IsNation() && {20331}.IsNation())
					{
						requirements.Add(Local.guildChangeFractionToOtherNation(5000));
					}
					else
					{
						requirements.Add(Local.guildChangeFractionToPirates);
					}
					fractionSelector.SetButtonEnabled(true);
					fractionSelector.SetButtonText(Local.GuildWindow_SyncProtected_5);
					fractionSelector.Button.ToolTipState = new ToolTipState("", string.Join(" ", requirements), Array.Empty<ToolTipCharacteristics>());
					Action<int> <>9__5;
					fractionSelector.Button.EvClick += delegate(ClickUiEventArgs {20335})
					{
						if (!{20312}.MyRights.AllowEditPolitics)
						{
							new {17312}(Local.GuildWindow_3(Local.guildright_tt_AllowEditPolitics));
							return;
						}
						string text = Local.guildChangeFraction(FractionAPI.ChangeAgainRestrictionDays({20312}.MyFraction, {20331}));
						string name = {20331}.GetName();
						string {17134} = text;
						string {17135} = string.Join(" ", requirements);
						Action<int> {17136};
						if (({17136} = <>9__5) == null)
						{
							{17136} = (<>9__5 = delegate(int {20332})
							{
								if ({20332} != 0)
								{
									return;
								}
								string {17365};
								if (!Session.Guild.CanChangeFraction(Session.Game.WorldPorts, {20331}, out {17365}))
								{
									new {17312}({17365});
									return;
								}
								Global.Network.Send(new OnGuildFractionSetMsg({20331}, false));
							});
						}
						new {17107}(name, {17134}, {17135}, {17136}, true, null, new string[]
						{
							Local.to_continue,
							Local.undo
						});
					};
					return;
				}
				if ({20331} == {20312}.MyFraction)
				{
					fractionSelector.SetButtonEnabled(false);
					fractionSelector.SetButtonText(Local.current_fraction);
					return;
				}
				if (num > 0.0)
				{
					fractionSelector.SetButtonEnabled(false);
					fractionSelector.SetButtonText(Local.guildFractionCantBeChanged + " " + StringHelper.TimeD(num));
					return;
				}
				if (!Session.Guild.IsFlotilia)
				{
					fractionSelector.SetButtonEnabled(true);
					fractionSelector.SetButtonText(Local.know_more);
					fractionSelector.Button.EvClick += delegate(ClickUiEventArgs {20324})
					{
						{18702}.TryToOpenExactArticle(Local.gamewiki_guildsfractions);
					};
					return;
				}
				if ({20331}.IsNation())
				{
					fractionSelector.SetButtonEnabled(false);
					fractionSelector.SetButtonText(Local.flotilia_limit);
					return;
				}
				fractionSelector.SetButtonEnabled(true);
				fractionSelector.SetButtonText(Local.upgrade_flotilia);
				Action<int> <>9__6;
				fractionSelector.Button.EvClick += delegate(ClickUiEventArgs {20333})
				{
					if (!{20312}.MyRights.AllowEditGuildAndFortAndTreasury)
					{
						new {17107}("", Local.GuildWindow_3(Local.guildright_tt_AllowEditGuildAndFortAndTreasury), "", null, true, null, new string[]
						{
							Local.close
						});
						return;
					}
					int num2 = GuildCommon.MinimalCaptainRank(false);
					bool flag = Session.Guild.Members.Size < GuildCommon.MinMembersToUpdateFlotilia && !FractionAPI.IgnoreMembersCountRestriction;
					bool flag2 = Session.Account.Rang < num2;
					RTI rti = Session.Guild.GuildUpgradeCostIngots();
					string {17385} = flag ? Local.upgrade_flotilia_members_error : (flag2 ? Local.upgrade_flotilia_rank_error(num2) : Local.upgrade_flotilia_text);
					Action<int> {17386};
					if (({17386} = <>9__6) == null)
					{
						{17386} = (<>9__6 = delegate(int {20334})
						{
							if ({20334} == 0)
							{
								Global.Network.Send(new OnGuildMembersChanges(OnGuildMembersChanges.GAction.UpgradeFlotilia, 0U, (byte){20331}, Session.Guild.Name));
							}
						});
					}
					{17443}[] array = new {17443}[2];
					int num3 = 0;
					{17443} {17443};
					if (rti.Value <= Session.Guild.Treasury)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
						defaultInterpolatedStringHandler.AppendFormatted(Local.yes);
						defaultInterpolatedStringHandler.AppendLiteral(", -");
						defaultInterpolatedStringHandler.AppendFormatted(StringHelper.BigValueHelper(rti.Value));
						defaultInterpolatedStringHandler.AppendLiteral(" ");
						defaultInterpolatedStringHandler.AppendFormatted(Local.ingots);
						{17443} = new {17443}(defaultInterpolatedStringHandler.ToStringAndClear(), "", {17312}.cIconMoney, flag || flag2, 0f);
					}
					else
					{
						{17443} = new {17443}("-" + StringHelper.BigValueHelper(rti.Value) + " " + Local.ingots, Local.gold_ingots_not_enough, {17312}.cIconMoney, true, 0f);
					}
					array[num3] = {17443};
					array[1] = new {17443}(Local.no, string.Empty, {17312}.cIconReject, false, 0f);
					new {17312}({17385}, {17386}, array);
				};
			};
			fractionSelector.SelectFraction({20312}.MyFraction);
			return fractionSelector;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00073FC4 File Offset: 0x000721C4
		private static StackForm CreateOptions()
		{
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BorderThickness = 3f
			};
			if (Session.Guild.IsFlotilia)
			{
				stackForm.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.Philosopher_16, Color.White * 0.9f, Local.flotilia_limit, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}
			StackForm stackForm2 = stackForm;
			UiControl[] array = new UiControl[1];
			array[0] = new LiveLabel(Vector2.Zero, Fonts.Philosopher_16Bold, Color.White * 0.8f, () => Local.GuildWindow_SyncProtected_7(Session.Guild.TrustedGuilds.Size, Session.Guild.MaxTrustedGuilds), 500);
			stackForm2.AddItem(array);
			using (IEnumerator<GuildRuleToOther> enumerator = ((IEnumerable<GuildRuleToOther>)Session.Guild.TrustedGuilds).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GuildRuleToOther right = enumerator.Current;
					stackForm.AddItem(new UiControl[]
					{
						new {20312}.GuildRuleUi({20364}.c_item2, right, delegate()
						{
							Global.Network.Send(new OnGuildRulesChanged(OnGuildRulesChanged.Target.TrustedGuildRemove, right.Tag));
						})
					});
				}
			}
			Button button = new Button(Vector2.Zero, CommonAtlas.basicButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText("+", Fonts.Philosopher_14, Color.White, false);
			button.Opacity = ((Session.Guild.TrustedGuilds.Size >= Session.Guild.MaxTrustedGuilds) ? 0.5f : 1f);
			stackForm.AddItem(new UiControl[]
			{
				button
			});
			button.EvClick += delegate(ClickUiEventArgs {20325})
			{
				if (Session.Guild.TrustedGuilds.Size >= Session.Guild.MaxTrustedGuilds)
				{
					return;
				}
				new {20143}({20143}.Mode.Select, (GuildRatingInfo.Item {20326}) => {20326}.Tag != Session.Guild.Tag && {20326}.Fraction == {20312}.MyFraction).OnGuildSelected += delegate(GuildDisplayInfo {20327})
				{
					if (!{20312}.MyRights.AllowEditPolitics)
					{
						new {17312}(Local.GuildWindow_3(Local.guildright_tt_AllowEditPolitics));
						return;
					}
					Global.Network.Send(new OnGuildRulesChanged(OnGuildRulesChanged.Target.TrustedGuildAdd, {20327}.Tag));
				};
			};
			button.ToolTipState = new ToolTipState("", Local.GuildWindow_SyncProtected_6(120), Array.Empty<ToolTipCharacteristics>());
			FractionDisputeType disputeType = Session.Guild.Fraction.DisputeType();
			bool flag = disputeType == FractionDisputeType.Kingship && !FractionDisputeHelper.KingshipTitleAllowVoting((int)Session.Guild.PotentialTitleId);
			int myVotingWeight = FractionDisputeHelper.GetVotingWeight(Session.Guild.Fraction, Session.Guild.Members.Size, Session.GuildCacheServer_Rating);
			stackForm.AddItem(new UiControl[]
			{
				new Form(new Marker(0f, 0f, 10f, 10f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			stackForm.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_16Bold, Color.White * 0.8f, Local.guild_dispute, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_10, 0f);
			textBlockBuilder.WriteLines(TextBlockBuilder.CreateBlock(300f, flag ? Local.kingshipDisallowedByTitle : ((myVotingWeight <= 0) ? Local.noPointsForVoting : disputeType.GetDesc()), Color.White * 0.5f, Fonts.Arial_10, 0f));
			stackForm.AddItem(new UiControl[]
			{
				textBlockBuilder.Create()
			});
			if (disputeType != FractionDisputeType.None && !flag && myVotingWeight > 0)
			{
				Button button2 = new Button(Vector2.Zero, CommonAtlas.basicButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.guild_dispute_button, Fonts.Philosopher_14, Color.White, false);
				Action<GuildDisplayInfo> <>9__8;
				button2.EvClick += delegate(ClickUiEventArgs {20336})
				{
					if (!Session.MyMemberInGuild.Rights.AllowEditPolitics)
					{
						new {17312}(Local.GuildWindow_3(Local.guildright_tt_AllowEditPolitics));
						return;
					}
					if (Session.Guild.Effects.Any((GuildTemporaryEffect {20328}) => {20328}.Case == GuildTemporaryEffect.Type.DisputeProtection))
					{
						new {17312}(Local.selfDisputeProtection);
						return;
					}
					{20143} {20143} = new {20143}({20143}.Mode.Select, (GuildRatingInfo.Item {20329}) => Session.Guild.Tag != {20329}.Tag && {20329}.DisputeProtectionDays == 0 && FractionDisputeHelper.CanMakeDispute(Session.Guild, {20329}.Fraction));
					Action<GuildDisplayInfo> {20146};
					if (({20146} = <>9__8) == null)
					{
						{20146} = (<>9__8 = delegate(GuildDisplayInfo {20337})
						{
							if ({20337}.DisputeVotes.Any((GuildTemporaryEffect {20330}) => {20330}.Argument == Session.Guild.Tag))
							{
								new {17312}(Local.already_voted);
								return;
							}
							int necessaryVotingWeight = FractionDisputeHelper.GetNecessaryVotingWeight({20337}.Fraction, {20337}.MembersCount, (int){20337}.GuildRating);
							FractionDisputeType disputeType = disputeType;
							string {17356};
							if (disputeType != FractionDisputeType.Voting)
							{
								if (disputeType != FractionDisputeType.Kingship)
								{
									throw new NotSupportedException();
								}
								{17356} = Local.dispute_guild_info_kingship({20337}.DisputePoints, necessaryVotingWeight, {20337}.Tag);
							}
							else
							{
								{17356} = Local.dispute_guild_info_voting({20337}.DisputePoints, necessaryVotingWeight, {20337}.Tag, myVotingWeight);
							}
							{17312}.AskPriceGuild({17356}, Local.vote_bt, Session.Guild.MakeDisputePrice, 0, delegate
							{
								Global.Network.Send(new OnMakeGuildEffect(GuildTemporaryEffect.Type.DisputeVoteReceived, {20337}.Tag, 0, false));
							});
						});
					}
					{20143}.OnGuildSelected += {20146};
				};
				stackForm.AddItem(new UiControl[]
				{
					button2
				});
			}
			if (!{20312}.EnablePolitics)
			{
				stackForm.Opacity = 0.3f;
				stackForm.AllowMouseInput = false;
			}
			return stackForm;
		}

		// Token: 0x02000268 RID: 616
		private class GuildRuleUi : CustomUi
		{
			// Token: 0x06000DC1 RID: 3521 RVA: 0x00074358 File Offset: 0x00072558
			public GuildRuleUi(Rectangle {20316}, GuildRuleToOther {20317}, Action {20318})
			{
				Vector2 zero = Vector2.Zero;
				base..ctor(new Marker(ref zero, ref {20316}), {20316}, PositionAlignment.LeftUp, PositionAlignment.LeftUp, Color.White, false);
				{20312}.GuildRuleUi <>4__this = this;
				this.{20321} = {20317};
				string text = "[" + {20317}.Tag + "]";
				if ({20317}.IsConfirmed)
				{
					text += " ✔";
				}
				else
				{
					text = text + " (" + Local.unconfirmed.ToLower() + ")";
				}
				base.AddChild(new Label(base.Pos.XY + new Vector2(15f, 16f), Fonts.Philosopher_14, Color.Lime * 0.7f, text, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				this.{20320} = new Button(base.Pos.XY + new Vector2(233f, 12f), {20364}.c_btRemove, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				this.{20320}.EvClick += delegate(ClickUiEventArgs {20322})
				{
					if ({20317}.HasAnyTimeout)
					{
						return;
					}
					if (!Session.MyMemberInGuild.Rights.AllowEditPolitics)
					{
						new {17312}(Local.GuildWindow_3(Local.guildright_tt_AllowEditPolitics));
						return;
					}
					Action remove = {20318};
					if (remove != null)
					{
						remove();
					}
					<>4__this.{20320}.AllowMouseInput = false;
				};
				base.AddChild(this.{20320});
			}

			// Token: 0x06000DC2 RID: 3522 RVA: 0x00074490 File Offset: 0x00072690
			protected override void UserUpdate(ref FrameTime {20319})
			{
				float opacity = (base.InputMode == MouseInputMode.NoFocus) ? 0.1f : 1f;
				if (this.{20320} != null)
				{
					this.{20320}.Opacity = opacity;
				}
				this.{20320}.TexturePath = (this.{20321}.HasAnyTimeout ? {20364}.c_btWait : {20364}.c_btRemove);
			}

			// Token: 0x06000DC3 RID: 3523 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserBackRender()
			{
			}

			// Token: 0x06000DC4 RID: 3524 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserFrontRender()
			{
			}

			// Token: 0x04000CED RID: 3309
			private readonly Button {20320};

			// Token: 0x04000CEE RID: 3310
			private readonly GuildRuleToOther {20321};
		}
	}
}
