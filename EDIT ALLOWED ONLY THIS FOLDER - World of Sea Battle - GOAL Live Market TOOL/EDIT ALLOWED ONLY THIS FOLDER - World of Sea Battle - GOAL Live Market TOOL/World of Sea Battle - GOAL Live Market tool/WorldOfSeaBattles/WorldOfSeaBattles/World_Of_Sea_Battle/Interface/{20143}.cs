using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000244 RID: 580
	internal sealed class {20143} : {17625}
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000D27 RID: 3367 RVA: 0x0006D804 File Offset: 0x0006BA04
		// (remove) Token: 0x06000D28 RID: 3368 RVA: 0x0006D83C File Offset: 0x0006BA3C
		public event Action<GuildDisplayInfo> OnGuildSelected
		{
			[CompilerGenerated]
			add
			{
				Action<GuildDisplayInfo> action = this.{20181};
				Action<GuildDisplayInfo> action2;
				do
				{
					action2 = action;
					Action<GuildDisplayInfo> value2 = (Action<GuildDisplayInfo>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<GuildDisplayInfo>>(ref this.{20181}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<GuildDisplayInfo> action = this.{20181};
				Action<GuildDisplayInfo> action2;
				do
				{
					action2 = action;
					Action<GuildDisplayInfo> value2 = (Action<GuildDisplayInfo>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<GuildDisplayInfo>>(ref this.{20181}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0006D871 File Offset: 0x0006BA71
		private bool HasFilter
		{
			get
			{
				return !string.IsNullOrEmpty(this.{20187});
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0006D884 File Offset: 0x0006BA84
		public {20143}({20143}.Mode {20148}, Func<GuildRatingInfo.Item, bool> {20149} = null) : base(840f, {17625}.c_back2, {17604}.InGameWindow, {17625}.c_icShield, new {17625}.DynamicTittle[]
		{
			Local.fractions,
			Local.GuildsWindow_0,
			({20148} == {20143}.Mode.CreateOrJoin) ? Local.create_guild : ((Session.Guild.Fraction == FractionID.None) ? Local.flotilias : Session.Guild.Fraction.GetName())
		})
		{
			{20143}.CurrentInstance = this;
			this.{20183} = {20148};
			this.{20186} = {20149};
			base.AddChild(new Form(new Marker(0f, 0f, base.Pos.WH.X, 474f).Offset(base.Pos.XY), {17625}.c_verticalBlackGradient, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
			GameScene.IncreaseGameInput();
			base.EvRemoveFromContainer += this.{20164};
			Global.Network.Send(new OnGuildRatingActionMsg(0, null, null, 0));
			base.ComposeTabWithScroll(null, true, true, new Action<ListItemViewControl>[]
			{
				new Action<ListItemViewControl>(this.{20165}),
				new Action<ListItemViewControl>(this.{20167}),
				({20148} == {20143}.Mode.CreateOrJoin) ? new Action<ListItemViewControl>(this.{20169}) : new Action<ListItemViewControl>(this.{20171})
			});
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0006D9F4 File Offset: 0x0006BBF4
		private void {20150}(ListItemViewControl {20151})
		{
			{20143}.<>c__DisplayClass29_0 CS$<>8__locals1 = new {20143}.<>c__DisplayClass29_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.list = {20151};
			if (this.{20184} == null || this.{20184}.Data.Size == 0)
			{
				Global.Network.Send(new OnGuildRatingActionMsg(1, null, null, 0));
			}
			this.{20191} = CS$<>8__locals1.list;
			if (this.{20188} != null)
			{
				if (this.{20188}.Value.InfoOrNull == null)
				{
					{20143}.InternalDialog(CS$<>8__locals1.list, Local.GuildsWindow_17, Local.to_back, delegate
					{
						CS$<>8__locals1.<>4__this.{20187} = null;
						CS$<>8__locals1.<>4__this.ForceItemSelected(1);
					});
				}
				else
				{
					{20143}.<>c__DisplayClass29_1 CS$<>8__locals2 = new {20143}.<>c__DisplayClass29_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					CS$<>8__locals2.guild = this.{20188}.Value.InfoOrNull;
					StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					stackForm.AddItem(new UiControl[]
					{
						this.{20163}()
					});
					stackForm.AddItem(new UiControl[]
					{
						new Form(new Marker(0f, 0f, 63f, 0f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					Form form = new Form(new Marker(0f, 0f, 280f, 67f), new Rectangle(394, 1309, 111, 18), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					};
					form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_24, Color.SkyBlue, CS$<>8__locals2.guild.Tag + (CS$<>8__locals2.guild.IsFlotilia ? (" (" + Local.flotilia + ")") : ""), PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.Center, 0f);
					form.AddChildPos(new Label(Vector2.Zero, Fonts.Arial_12, Color.SkyBlue * 0.7f, CS$<>8__locals2.guild.GuildName, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.RightDown, -4f);
					stackForm.AddItem(new UiControl[]
					{
						form
					});
					CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
					{
						stackForm
					});
					CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
					{
						new Form(new Marker(0f, 0f, 3f, 3f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					Form form2 = new Form(Vector2.Zero, new Rectangle(0, 2738, 490, 32), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					CustomSpriteFont philosopher_ = Fonts.Philosopher_16;
					form2.AddChildPos(new Label(Vector2.Zero, philosopher_, new Color(255, 192, 147), CS$<>8__locals2.guild.GuildRating.ToString() ?? "", PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, 253f);
					form2.AddChildPos(new Label(Vector2.Zero, philosopher_, new Color(210, 255, 206), CS$<>8__locals2.guild.CapturedPorts.ToString() ?? "", PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, 346f);
					form2.AddChildPos(new Label(Vector2.Zero, philosopher_, new Color(161, 163, 177), CS$<>8__locals2.guild.MembersCount.ToString() ?? "", PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, 433f);
					CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
					{
						form2
					});
					CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
					{
						new Form(new Marker(0f, 0f, 15f, 15f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					CS$<>8__locals2.textColor = Color.White;
					int num = 700;
					BlocksStackFormControl blocksStackFormControl = new BlocksStackFormControl(Vector2.Zero, 100, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						MaxWidth = (float)num
					};
					blocksStackFormControl.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Philosopher_14, CS$<>8__locals2.textColor, Local.GuildsWindow_18(""), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					blocksStackFormControl.AddItem(new UiControl[]
					{
						{20143}.ContactHelper(CS$<>8__locals2.guild.FounderContact)
					});
					CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
					{
						blocksStackFormControl
					});
					blocksStackFormControl = new BlocksStackFormControl(Vector2.Zero, 100, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						MaxWidth = (float)num
					};
					blocksStackFormControl.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Philosopher_14, CS$<>8__locals2.textColor, Local.guildwindow_admirals, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					if (CS$<>8__locals2.guild.ContactsFirstSuppors.Size == 0)
					{
						blocksStackFormControl.AddItem(new UiControl[]
						{
							new Label(Vector2.Zero, Fonts.Philosopher_14, CS$<>8__locals2.textColor, "-", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
					}
					else
					{
						foreach (PlayerContact {20157} in ((IEnumerable<PlayerContact>)CS$<>8__locals2.guild.ContactsFirstSuppors))
						{
							blocksStackFormControl.AddItem(new UiControl[]
							{
								{20143}.ContactHelper({20157})
							});
						}
					}
					CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
					{
						blocksStackFormControl
					});
					Relation relation = (Session.Guild == null) ? Relation.Neutral : FractionAPI.StatusWith(Session.Guild.Fraction, CS$<>8__locals2.guild.Fraction);
					CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Philosopher_14Bold, ((relation == Relation.Neutral) ? Color.White : ((relation == Relation.Ally) ? Color.Lime : Color.Orange)) * 0.7f, Local.fraction + ": " + CS$<>8__locals2.guild.Fraction.GetName(), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					if (CS$<>8__locals2.guild.Fraction == FractionID.TradeUnion)
					{
						CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
						{
							TextBlockBuilder.CreateBlock((float)num, Local.GuildsWindow_233(CS$<>8__locals2.guild.TradeUnionHqCount), CS$<>8__locals2.textColor, Fonts.Philosopher_14, 0f).Create()
						});
					}
					CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
					{
						TextBlockBuilder.CreateBlock((float)num, Local.GuildsWindow_20(string.IsNullOrEmpty(CS$<>8__locals2.guild.Description) ? "-" : CS$<>8__locals2.guild.Description), CS$<>8__locals2.textColor * 0.5f, Fonts.Philosopher_14, 0f).Create()
					});
					CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
					{
						TextBlockBuilder.CreateBlock((float)num, Local.GuildsWindow_29 + ((CS$<>8__locals2.guild.TrustedGuilds.Size == 0) ? "-" : {20143}.ListToString(CS$<>8__locals2.guild.TrustedGuilds)), CS$<>8__locals2.textColor, Fonts.Philosopher_14, 0f).Create(default(Vector2))
					});
					if (CS$<>8__locals2.guild.ActiveForNewPlayers)
					{
						CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
						{
							new Label(Vector2.Zero, Fonts.Philosopher_14, Color.LightGreen, "+" + Local.guild_active_recruit_full, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
					}
					if (CS$<>8__locals2.guild.DisputeProtectionSec > 0.0)
					{
						CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
						{
							new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Orange * 0.7f, Local.GuildWindow_DisputeProtectionEffect + ", " + StringHelper.TimeD(CS$<>8__locals2.guild.DisputeProtectionSec), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
					}
					foreach (GuildTemporaryEffect guildTemporaryEffect in ((IEnumerable<GuildTemporaryEffect>)CS$<>8__locals2.guild.DisputeVotes))
					{
						CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
						{
							new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Orange * 0.7f, Local.guild_dispute_effect(guildTemporaryEffect.Argument) + ", " + StringHelper.TimeD(guildTemporaryEffect.RemainingTimeSec), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
					}
					if (CS$<>8__locals2.guild.DisputeVotes.Size > 0)
					{
						CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
						{
							new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Orange * 0.7f, Local.dispute_status(CS$<>8__locals2.guild.DisputePoints, FractionDisputeHelper.GetNecessaryVotingWeight(CS$<>8__locals2.guild.Fraction, CS$<>8__locals2.guild.MembersCount, (int)CS$<>8__locals2.guild.GuildRating)), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
					}
					if (CS$<>8__locals2.guild.Fraction != FractionID.None)
					{
						CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
						{
							new Label(Vector2.Zero, Fonts.Philosopher_14, CS$<>8__locals2.textColor, Local.accomplished_missions, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
						int childCount = CS$<>8__locals2.CS$<>8__locals1.list.ChildCount;
						foreach (GuildQuestAction guildQuestAction in GuildQuests.GetQuestsWithAction(new FractionID?(CS$<>8__locals2.guild.Fraction)))
						{
							int num2 = CS$<>8__locals2.guild.Quests[guildQuestAction];
							if (num2 > 0)
							{
								CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
								{
									CS$<>8__locals2.<RatingPageLoader>g__MakeQuestItem|5(guildQuestAction.GetName(), num2)
								});
							}
						}
						foreach (GuildQuestProgression guildQuestProgression in GuildQuests.GetQuestsWithProgress(new FractionID?(CS$<>8__locals2.guild.Fraction)))
						{
							int item = CS$<>8__locals2.guild.Quests[guildQuestProgression].Item2;
							if (item > 0)
							{
								CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
								{
									CS$<>8__locals2.<RatingPageLoader>g__MakeQuestItem|5(guildQuestProgression.GetName(), item)
								});
							}
						}
						if (childCount == CS$<>8__locals2.CS$<>8__locals1.list.ChildCount)
						{
							CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
							{
								new Label(Vector2.Zero, Fonts.Philosopher_14, CS$<>8__locals2.textColor * 0.5f, Local.no_accomplished_missions_yet, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							});
						}
					}
					if (this.{20183} != {20143}.Mode.ViewOnly)
					{
						if (this.{20183} == {20143}.Mode.CreateOrJoin && Session.Account.Rang < Gameplay.MinRankEnteringGuild(CS$<>8__locals2.guild.IsFlotilia))
						{
							CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
							{
								new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Orange, Local.guild_rank_ristriction(Gameplay.MinRankEnteringGuild(false)), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							});
						}
						else
						{
							{20143}.<>c__DisplayClass29_2 CS$<>8__locals3 = new {20143}.<>c__DisplayClass29_2();
							CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals2;
							CS$<>8__locals3.selectButton = new Button(Vector2.Zero, CommonAtlas.basicButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
							CS$<>8__locals3.selectButton.SetText((this.{20183} == {20143}.Mode.Select) ? Local.select : Local.GuildsWindow_23, Fonts.Arial_10, Color.White * 0.9f, false);
							bool flag = true;
							if (this.{20183} != {20143}.Mode.Select)
							{
								bool flag2 = Session.Game.WorldPorts.FractionBonusHelper(CS$<>8__locals3.CS$<>8__locals2.guild.Fraction);
								Relation relation2 = Session.Account.Reputations.NeutralStatusWith(CS$<>8__locals3.CS$<>8__locals2.guild.Fraction);
								if (CS$<>8__locals3.CS$<>8__locals2.guild.Fraction.IsNation() && (flag2 ? (relation2 == Relation.Enemy) : (relation2 > Relation.Ally)))
								{
									string name = CS$<>8__locals3.CS$<>8__locals2.guild.Fraction.GetName();
									if (flag2)
									{
										CS$<>8__locals3.selectButton.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(Local.not_available, Local.guild_window_low_reputation_non_enemy(name), Array.Empty<ToolTipCharacteristics>()));
									}
									else
									{
										CS$<>8__locals3.selectButton.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(Local.not_available, Local.guild_window_low_reputation(name), Array.Empty<ToolTipCharacteristics>()));
									}
									flag = false;
								}
								else if (CS$<>8__locals3.CS$<>8__locals2.guild.Fraction == FractionID.TradeUnion && Session.Account.Reputations.MaxReputation < -33f)
								{
									CS$<>8__locals3.selectButton.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(Local.not_available, Local.guild_window_low_reputation_trader, Array.Empty<ToolTipCharacteristics>()));
									flag = false;
								}
							}
							if (flag)
							{
								CS$<>8__locals3.selectButton.EvClick += delegate(ClickUiEventArgs {20204})
								{
									if (CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.{20183} != {20143}.Mode.CreateOrJoin)
									{
										CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.{20181}(CS$<>8__locals3.CS$<>8__locals2.guild);
										CS$<>8__locals3.selectButton.AllowMouseInput = false;
										CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.BlockAndClose();
										return;
									}
									if (Session.Guild != null)
									{
										return;
									}
									Action<string> {17401};
									if (({17401} = CS$<>8__locals3.<>9__7) == null)
									{
										{17401} = (CS$<>8__locals3.<>9__7 = delegate(string {20205})
										{
											if (CS$<>8__locals3.CS$<>8__locals2.guild.Fraction == FractionID.Pirate)
											{
												new {17312}(Local.guild_window_enter_pirate, delegate()
												{
													CS$<>8__locals3.<RatingPageLoader>g__SendApplication|8({20205});
												}, false);
												return;
											}
											base.<RatingPageLoader>g__SendApplication|8({20205});
										});
									}
									new {17312}({17401}, 250, Local.GuildsWindow_24, null, null);
								};
							}
							else
							{
								CS$<>8__locals3.selectButton.TextColor *= 0.5f;
							}
							CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.list.AddItem(new UiControl[]
							{
								CS$<>8__locals3.selectButton
							});
						}
					}
				}
				this.{20188} = null;
				return;
			}
			bool flag3;
			if (this.{20190} != null)
			{
				FractionID? fractionID = this.{20190};
				GuildCommon guild = Session.Guild;
				FractionID? fractionID2 = (guild != null) ? new FractionID?(guild.Fraction) : null;
				flag3 = (fractionID.GetValueOrDefault() == fractionID2.GetValueOrDefault() & fractionID != null == (fractionID2 != null));
			}
			else
			{
				flag3 = false;
			}
			this.{20193} = flag3;
			if (base.GetPressedTabButtonText() == Local.GuildsWindow_0)
			{
				this.{20190} = null;
			}
			if (this.{20190} != null && this.{20189}.Infos != null)
			{
				FractionID value = this.{20190}.Value;
				StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				if (value != FractionID.None)
				{
					stackForm2.AddItem(new UiControl[]
					{
						new Image(new Marker(0f, 0f, 150f, 150f), FractionAPI.GetFractionIconWithBack(value), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
				}
				StackForm stackForm3 = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm3.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.Philosopher_24, Color.Wheat, value.GetName(), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				stackForm3.AddItem(new UiControl[]
				{
					{20143}.<RatingPageLoader>g__MakeDescriptionTextBlock|29_12(value.GetShortDescription())
				});
				stackForm3.AddSpace(10f);
				if (value != FractionID.TradeUnion)
				{
					FractionStatisitcs info = this.{20189}.Infos[value];
					if (info.TotalPortsIds.Value != 0L)
					{
						string[] value2 = (from {20207} in Gameplay.WorldMap.Ports
						where info.TotalPortsIds[{20207}.PortID]
						select {20207} into {20194}
						select {20194}.PortNameShort).ToArray<string>();
						stackForm3.AddItem(new UiControl[]
						{
							{20143}.<RatingPageLoader>g__MakeDescriptionTextBlock|29_12(Local.fraction_owned_territories(value.GetName()))
						});
						stackForm3.AddItem(new UiControl[]
						{
							{20143}.<RatingPageLoader>g__MakeDescriptionTextBlock|29_12(string.Join(", ", value2))
						});
					}
					else
					{
						stackForm3.AddItem(new UiControl[]
						{
							{20143}.<RatingPageLoader>g__MakeDescriptionTextBlock|29_12(Local.fraction_without_territories(value.GetName()))
						});
					}
				}
				stackForm3.AddItem(new UiControl[]
				{
					{20143}.<RatingPageLoader>g__MakeDescriptionTextBlock|29_12(value.DisputeType().GetDesc())
				});
				stackForm2.AddSpace(20f);
				stackForm2.AddItem(new UiControl[]
				{
					stackForm3
				});
				CS$<>8__locals1.list.AddItem(new UiControl[]
				{
					stackForm2
				});
				CS$<>8__locals1.list.AddItem(new UiControl[]
				{
					new Form(new Marker(0f, 0f, 10f, 10f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}
			StackForm stackForm4 = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Marker marker = new Marker(0f, 0f, (float){17625}.c_btGray_big.Width * 0.85f, (float){17625}.c_btGray_big.Height);
			stackForm4.AddSpace(3f);
			TextBox textBox2 = new TextBox(Vector2.Zero, new Rectangle(1529, 2228, 143, 37), Fonts.Philosopher_16, Color.Wheat * 1.1f, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				DisableDepthFocusTest = true,
				DefaultText = Local.search,
				Text = (this.{20187} ?? string.Empty)
			};
			textBox2.AttachMaxLengthModerator(3, null, Color.Transparent);
			textBox2.EvTextChanged += delegate(string {20196})
			{
				CS$<>8__locals1.<>4__this.{20187} = {20196};
				CS$<>8__locals1.<>4__this.ForceItemSelected(1);
			};
			stackForm4.AddItem(new UiControl[]
			{
				textBox2
			});
			if (false)
			{
				CheckboxControl {12952} = new CheckboxControl(Vector2.Zero, CommonAtlas.checkboxPencil_true, CommonAtlas.checkboxPencil_false, {20143}.cisWorldSeparationEnable, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExCheckEvent(delegate(CheckboxCheckedEventArgs {20197})
				{
					{20143}.cisWorldSeparationEnable = {20197}.NewValue;
					CS$<>8__locals1.<>4__this.Response(null);
				}).SetText(Local.regions, Fonts.Arial_12, Color.White);
				Form form3 = new Form(marker, {17625}.c_btGray_big, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					BasicColor = Color.Gray
				};
				form3.AddChildPos({12952}, PositionAlignment.Center, PositionAlignment.Center, 0f);
				stackForm4.AddItem(new UiControl[]
				{
					form3
				});
			}
			else
			{
				{20143}.cisWorldSeparationEnable = false;
			}
			stackForm4.AddItem(new UiControl[]
			{
				new Button(marker, {17625}.c_btGray_big, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					Tag = {20143}.OrderBy.Rating
				}.SetText(Local.order_rating, Fonts.Arial_12, Color.Gray, false)
			});
			stackForm4.AddItem(new UiControl[]
			{
				new Button(marker, {17625}.c_btGray_big, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					Tag = {20143}.OrderBy.Ports
				}.SetText(Local.order_ports, Fonts.Arial_12, Color.Gray, false)
			});
			stackForm4.AddItem(new UiControl[]
			{
				new Button(marker, {17625}.c_btGray_big, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					Tag = {20143}.OrderBy.NewGuilds
				}.SetText(Local.order_newGuilds, Fonts.Arial_12, Color.Gray, false)
			});
			if (this.{20190} == null)
			{
				stackForm4.AddItem(new UiControl[]
				{
					new Button(marker, {17625}.c_btGray_big, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						Tag = {20143}.OrderBy.Flotilia
					}.SetText(Local.flotilia, Fonts.Arial_12, Color.Gray, false)
				});
			}
			ToolTipState {12676} = new ToolTipState(Local.GuildsWindow_0, Local.GuildsWindow_rtt1, Array.Empty<ToolTipCharacteristics>());
			stackForm4.FirstChild().ToolTip = new ToolTip({12676});
			stackForm4.GetChildren[1].ToolTip = new ToolTip({12676});
			if (this.{20187} != null)
			{
				textBox2.IsEnter = true;
				new UiActor(textBox2, delegate()
				{
					textBox2.IsEnter = true;
				});
			}
			foreach (UiControl uiControl in ((IEnumerable<UiControl>)stackForm4.GetChildren))
			{
				Button button = uiControl as Button;
				if (button != null)
				{
					{20143}.OrderBy tag = ({20143}.OrderBy)button.Tag;
					if (tag == this.{20185})
					{
						button.TexturePath = {17625}.c_btLight_big;
						button.TextColor = Color.Black * 0.85f;
					}
					else
					{
						button.EvClick += delegate(ClickUiEventArgs {20208})
						{
							CS$<>8__locals1.<>4__this.{20185} = tag;
							CS$<>8__locals1.<>4__this.Response(null);
						};
					}
				}
			}
			CS$<>8__locals1.list.AddItem(new UiControl[]
			{
				stackForm4
			});
			if (this.{20184} == null)
			{
				return;
			}
			if (Session.Account.Rang < Gameplay.MinRankEnteringGuild(false))
			{
				CS$<>8__locals1.list.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.F_m14_Ghotic, Color.White * 0.6f, Local.guild_rank_ristriction(Gameplay.MinRankEnteringGuild(false)), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}
			CS$<>8__locals1.displayCount = 0;
			if ({20143}.cisWorldSeparationEnable && !this.HasFilter)
			{
				{20143}.<>c__DisplayClass29_7 CS$<>8__locals7;
				CS$<>8__locals7.cisInTop = (LocaleInfo.Current.Id == Locale.Ru);
				CS$<>8__locals7.header = false;
				List<GuildRatingInfo.Item> list = new List<GuildRatingInfo.Item>();
				foreach (GuildRatingInfo.Item item2 in ((IEnumerable<GuildRatingInfo.Item>)this.{20184}.Data))
				{
					if (!CS$<>8__locals7.header)
					{
						CS$<>8__locals1.<RatingPageLoader>g__AddHeader|17(true, ref CS$<>8__locals7);
					}
					if (item2.ProbablyCISGuild)
					{
						CS$<>8__locals1.<RatingPageLoader>g__AppendGuild|2(item2);
					}
					else
					{
						list.Add(item2);
					}
				}
				if (list.Count > 0)
				{
					CS$<>8__locals1.<RatingPageLoader>g__AddHeader|17(false, ref CS$<>8__locals7);
				}
				using (List<GuildRatingInfo.Item>.Enumerator enumerator7 = list.GetEnumerator())
				{
					while (enumerator7.MoveNext())
					{
						GuildRatingInfo.Item {20201} = enumerator7.Current;
						CS$<>8__locals1.<RatingPageLoader>g__AppendGuild|2({20201});
					}
					goto IL_15EA;
				}
			}
			HashSet<GuildRatingInfo.Item> hashSet = new HashSet<GuildRatingInfo.Item>();
			foreach (GuildRatingInfo.Item item3 in ((IEnumerable<GuildRatingInfo.Item>)this.{20184}.Data))
			{
				if (!this.HasFilter || CS$<>8__locals1.<RatingPageLoader>g__CheckFilter|1(item3.Tag))
				{
					CS$<>8__locals1.<RatingPageLoader>g__AppendGuild|2(item3);
					hashSet.Add(item3);
				}
			}
			foreach (GuildRatingInfo.Item item4 in ((IEnumerable<GuildRatingInfo.Item>)this.{20184}.Data))
			{
				if (!hashSet.Contains(item4) && (!this.HasFilter || CS$<>8__locals1.<RatingPageLoader>g__CheckFilter|1(item4.Name)))
				{
					CS$<>8__locals1.<RatingPageLoader>g__AppendGuild|2(item4);
				}
			}
			IL_15EA:
			if (this.HasFilter && CS$<>8__locals1.displayCount == 0)
			{
				{20143}.InternalDialog(CS$<>8__locals1.list, Local.GuildsWindow_28, Local.search, delegate
				{
					if (CS$<>8__locals1.<>4__this.{20182})
					{
						return;
					}
					if (!CS$<>8__locals1.<>4__this.HasFilter || CS$<>8__locals1.<>4__this.{20187}.Length != 3)
					{
						new {17312}(Local.GuildsWindow_2);
						return;
					}
					CS$<>8__locals1.<>4__this.{20182} = true;
					Global.Network.Send(new OnSearchOrInviteGuildQuery(CS$<>8__locals1.<>4__this.{20187}, false, ""));
				});
			}
			new UiActor(CS$<>8__locals1.list, delegate()
			{
				CS$<>8__locals1.list.SetScrollValue(CS$<>8__locals1.<>4__this.{20192});
				CS$<>8__locals1.<>4__this.{20192} = 0f;
			});
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0006F0A4 File Offset: 0x0006D2A4
		private void {20152}(ListItemViewControl {20153})
		{
			{20143}.<>c__DisplayClass30_0 CS$<>8__locals1 = new {20143}.<>c__DisplayClass30_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.list = {20153};
			if (this.{20189}.Infos == null)
			{
				return;
			}
			CS$<>8__locals1.ownFractionPage = null;
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				CS$<>8__locals1.<FractionsPageLoader>g__MakeFractionItem|0(FractionID.Espaniol),
				CS$<>8__locals1.<FractionsPageLoader>g__MakeFractionItem|0(FractionID.Antilia),
				CS$<>8__locals1.<FractionsPageLoader>g__MakeFractionItem|0(FractionID.KaiAndSeveria),
				CS$<>8__locals1.<FractionsPageLoader>g__MakeFractionItem|0(FractionID.TradeUnion),
				CS$<>8__locals1.<FractionsPageLoader>g__MakeFractionItem|0(FractionID.Pirate)
			});
			CS$<>8__locals1.list.AddItem(new UiControl[]
			{
				stackForm
			});
			Label label = new Label(Vector2.Zero, Fonts.Philosopher_14, Color.White * 0.6f, Local.season_end_in(this.{20189}.DaysToSeasonEnd), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Vector2 zero = Vector2.Zero;
			Form form = new Form(new Marker(ref zero, base.Pos.WH.X, label.Pos.WH.Y), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos(label, PositionAlignment.Center, PositionAlignment.RightDown, 0f, -10f, false);
			CS$<>8__locals1.list.AddItem(new UiControl[]
			{
				form
			});
			if (this.{20183} == {20143}.Mode.Select)
			{
				UiControl ownFractionPage = CS$<>8__locals1.ownFractionPage;
				if (ownFractionPage == null)
				{
					return;
				}
				ownFractionPage.ImitateClick(false);
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0006F1EC File Offset: 0x0006D3EC
		public static void ChangeStatus(CreateGuildRequestStatus {20154})
		{
			if ({20143}.CurrentInstance == null)
			{
				return;
			}
			if ({20154} == CreateGuildRequestStatus.Completed)
			{
				{20143}.CurrentInstance.RemoveFromContainer();
				return;
			}
			if ({20154} == CreateGuildRequestStatus.TagUnavailable)
			{
				new {17312}(Local.GuildsWindow_15);
			}
			if ({20154} == CreateGuildRequestStatus.InternalError)
			{
				new {17312}(Local.GuildsWindow_16);
			}
			{20143}.CurrentInstance.AllowMouseInput = true;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0006F238 File Offset: 0x0006D438
		public void Response(OnSearchGuildQueryResult {20155})
		{
			ListItemViewControl listItemViewControl = this.{20191};
			this.{20192} = ((listItemViewControl != null) ? listItemViewControl.CurrentScrollValue : 0f);
			this.{20182} = false;
			this.{20188} = new OnSearchGuildQueryResult?({20155});
			this.RefreshCurrentDynamicTabPage();
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0006F274 File Offset: 0x0006D474
		public void Response(OnGuildRatingActionMsg? {20156})
		{
			if ({20156} != null)
			{
				if ({20156}.Value.PortionId <= 1)
				{
					this.{20184} = {20156}.Value.Value;
				}
				else
				{
					this.{20184}.Data.Add({20156}.Value.Value.Data);
				}
			}
			this.{20184}.Data.SortTop(new Func<GuildRatingInfo.Item, float>(this.{20177}));
			if (this.{20190} != null || base.GetPressedTabButtonText() == Local.GuildsWindow_0)
			{
				this.RefreshCurrentDynamicTabPage();
			}
			if ({20156} == null || {20156}.Value.PortionId != 0)
			{
				return;
			}
			this.{20189} = {20156}.Value;
			if (this.{20190} == null)
			{
				this.RefreshCurrentDynamicTabPage();
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0006F354 File Offset: 0x0006D554
		private static LabelButton ContactHelper(PlayerContact {20157})
		{
			LabelButton labelButton = new LabelButton(Vector2.Zero, "[" + {20157}.Name.Replace("\n", "") + "] ", Fonts.Philosopher_14, Color.Gray, Color.Gold, null);
			labelButton.EvClick += delegate(ClickUiEventArgs {20215})
			{
				new {17558}(new {17549}({20157}.SID, {20157}.Name, Array.Empty<{17549}.OptionalAction>()));
			};
			return labelButton;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0006F3C4 File Offset: 0x0006D5C4
		private static string ListToString(Tlist<string> {20158})
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < {20158}.Size; i++)
			{
				stringBuilder.Append({20158}.Array[i]);
				if (i != {20158}.Size - 1)
				{
					stringBuilder.Append(", ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0006F414 File Offset: 0x0006D614
		private static void InternalDialog(ListItemViewControl {20159}, string {20160}, string {20161}, Action {20162})
		{
			{20159}.AddItem(new UiControl[]
			{
				new Form(new Marker(0f, 0f, 20f, 20f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			{20159}.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_14, Color.White * 0.5f, {20160}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			{20159}.AddItem(new UiControl[]
			{
				new Form(new Marker(0f, 0f, 20f, 20f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			{20159}.AddItem(new UiControl[]
			{
				new Button(Vector2.Zero, {17625}.c_btLight_big, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.search, Fonts.Philosopher_14, Color.Black, false).ExClick(delegate(ClickUiEventArgs {20216})
				{
					{20162}();
				})
			});
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0006F504 File Offset: 0x0006D704
		private Button {20163}()
		{
			Button button = new Button(Vector2.Zero + new Vector2(0f, 80f), {17625}.c_btLight_big, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button.SetText(Local.to_back, Fonts.Philosopher_14, Color.Black * 0.7f, false);
			button.EvClick += this.{20179};
			return button;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0006F644 File Offset: 0x0006D844
		[CompilerGenerated]
		private void {20164}()
		{
			if (this.IsClosedByHand)
			{
				EducationHelper.MakeFlag(EducationOnboarding.OpenGuildAndFractions, true);
			}
			GameScene.DecreaseGameInput();
			{20143}.CurrentInstance = null;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0006F661 File Offset: 0x0006D861
		[CompilerGenerated]
		private void {20165}(ListItemViewControl {20166})
		{
			this.{20152}({20166});
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0006F66A File Offset: 0x0006D86A
		[CompilerGenerated]
		private void {20167}(ListItemViewControl {20168})
		{
			this.{20150}({20168});
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0006F673 File Offset: 0x0006D873
		[CompilerGenerated]
		private void {20169}(ListItemViewControl {20170})
		{
			new {20124}(this).Loader({20170});
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0006F681 File Offset: 0x0006D881
		[CompilerGenerated]
		private void {20171}(ListItemViewControl {20172})
		{
			this.{20190} = new FractionID?(Session.Guild.Fraction);
			this.{20150}({20172});
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0006F69F File Offset: 0x0006D89F
		[CompilerGenerated]
		internal static TextBlockControl <RatingPageLoader>g__MakeDescriptionTextBlock|29_12(string {20173})
		{
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_14, 0f);
			textBlockBuilder.WriteLines({20173}, Color.Wheat * 0.8f, Fonts.Philosopher_14, 550f, new float?((float)-5));
			return textBlockBuilder.Create();
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0006F6E0 File Offset: 0x0006D8E0
		[CompilerGenerated]
		internal static StackForm <FractionsPageLoader>g__MakeInfoPair|30_4(string {20174}, string {20175})
		{
			int num = Fonts.CurrentLanguageIsCjk ? 80 : 17;
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_12, -1f);
			textBlockBuilder.WriteLine({20174}, Color.Wheat, Fonts.Philosopher_16);
			textBlockBuilder.WriteLines({20175}, Color.Wheat * 0.8f, Fonts.Arial_10, (float)num, new float?((float)-4));
			stackForm.AddItem(new UiControl[]
			{
				textBlockBuilder.CreateCentroid()
			});
			return stackForm;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0006F768 File Offset: 0x0006D968
		[CompilerGenerated]
		internal static Rectangle <FractionsPageLoader>g__GetFractionBackground|30_5(FractionID {20176})
		{
			switch ({20176})
			{
			case FractionID.Pirate:
				return {20143}.fraction_back_black;
			case FractionID.Antilia:
				return {20143}.fraction_back_purple;
			case FractionID.Espaniol:
				return {20143}.fraction_back_red;
			case FractionID.KaiAndSeveria:
				return {20143}.fraction_back_blue;
			case FractionID.TradeUnion:
				return {20143}.fraction_back_orange;
			}
			return {20143}.fraction_back_black;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0006F7C4 File Offset: 0x0006D9C4
		[CompilerGenerated]
		private float {20177}(GuildRatingInfo.Item {20178})
		{
			if (this.{20185} != {20143}.OrderBy.Rating)
			{
				float num;
				if (this.{20185} != {20143}.OrderBy.Ports)
				{
					if (this.{20185} != {20143}.OrderBy.NewGuilds)
					{
						if (this.{20185} != {20143}.OrderBy.Flotilia)
						{
							throw new NotSupportedException();
						}
						num = (float)(({20178}.IsFlotilia ? 10000 : 0) + {20178}.RatingPoints);
					}
					else
					{
						num = (float)(({20178}.IsNewGuild ? 10000 : 0) + {20178}.RatingPoints);
					}
				}
				else
				{
					num = (float)((ushort){20178}.CapturedPorts * 10000 + {20178}.RatingPoints);
				}
				return num;
			}
			if (!{20178}.IsFlotilia)
			{
				return (float)(100 + {20178}.RatingPoints) - (float)(({20178}.PotentialTitleId == 0) ? byte.MaxValue : {20178}.PotentialTitleId) / 100f;
			}
			return (float){20178}.MembersCount;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0006F87B File Offset: 0x0006DA7B
		[CompilerGenerated]
		private void {20179}(ClickUiEventArgs {20180})
		{
			this.{20187} = null;
			this.RefreshCurrentDynamicTabPage();
		}

		// Token: 0x04000C68 RID: 3176
		public static {20143} CurrentInstance;

		// Token: 0x04000C69 RID: 3177
		private static readonly Rectangle c_ratingItem = new Rectangle(0, 2845, 703, 33);

		// Token: 0x04000C6A RID: 3178
		private static readonly Rectangle c_ratingItemHighlighted = new Rectangle(119, 3082, 702, 33);

		// Token: 0x04000C6B RID: 3179
		private static readonly Rectangle fraction_back_red = new Rectangle(2981, 1793, 230, 711);

		// Token: 0x04000C6C RID: 3180
		private static readonly Rectangle fraction_back_purple = new Rectangle(3212, 1793, 230, 711);

		// Token: 0x04000C6D RID: 3181
		private static readonly Rectangle fraction_back_blue = new Rectangle(3443, 1793, 230, 711);

		// Token: 0x04000C6E RID: 3182
		private static readonly Rectangle fraction_back_orange = new Rectangle(3015, 2505, 230, 711);

		// Token: 0x04000C6F RID: 3183
		private static readonly Rectangle fraction_back_black = new Rectangle(3246, 2505, 230, 711);

		// Token: 0x04000C70 RID: 3184
		[CompilerGenerated]
		private Action<GuildDisplayInfo> {20181};

		// Token: 0x04000C71 RID: 3185
		private bool {20182};

		// Token: 0x04000C72 RID: 3186
		private readonly {20143}.Mode {20183};

		// Token: 0x04000C73 RID: 3187
		private GuildRatingInfo {20184};

		// Token: 0x04000C74 RID: 3188
		private {20143}.OrderBy {20185};

		// Token: 0x04000C75 RID: 3189
		private Func<GuildRatingInfo.Item, bool> {20186};

		// Token: 0x04000C76 RID: 3190
		private string {20187};

		// Token: 0x04000C77 RID: 3191
		private OnSearchGuildQueryResult? {20188};

		// Token: 0x04000C78 RID: 3192
		private OnGuildRatingActionMsg {20189};

		// Token: 0x04000C79 RID: 3193
		private FractionID? {20190};

		// Token: 0x04000C7A RID: 3194
		private static bool cisWorldSeparationEnable = true;

		// Token: 0x04000C7B RID: 3195
		private ListItemViewControl {20191};

		// Token: 0x04000C7C RID: 3196
		private float {20192};

		// Token: 0x04000C7D RID: 3197
		private bool {20193};

		// Token: 0x02000245 RID: 581
		public enum Mode
		{
			// Token: 0x04000C7F RID: 3199
			ViewOnly,
			// Token: 0x04000C80 RID: 3200
			Select,
			// Token: 0x04000C81 RID: 3201
			CreateOrJoin
		}

		// Token: 0x02000246 RID: 582
		private enum OrderBy
		{
			// Token: 0x04000C83 RID: 3203
			Rating,
			// Token: 0x04000C84 RID: 3204
			Ports,
			// Token: 0x04000C85 RID: 3205
			NewGuilds,
			// Token: 0x04000C86 RID: 3206
			Flotilia
		}
	}
}
