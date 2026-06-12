using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using ManualPacketSerialization;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002BC RID: 700
	internal sealed class {20816} : {17625}
	{
		// Token: 0x06000F74 RID: 3956 RVA: 0x00081AF0 File Offset: 0x0007FCF0
		public {20816}(OnLoadMercanaryOrders {20818}) : base(800f, {17625}.c_back3, {17604}.InGameWindow, {17625}.c_icPeople, new {17625}.DynamicTittle[]
		{
			Local.mercanary
		})
		{
			{20816}.CurrentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{20816}.CurrentInstance = null;
			};
			this.{20838} = new Tlist<MercanaryOrder>();
			this.{20838}.Add({20818}.Orders.Orders);
			this.{20819}();
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00081B80 File Offset: 0x0007FD80
		private void {20819}()
		{
			this.{20838}.SortTop(delegate(MercanaryOrder {20839})
			{
				if ({20839}.CreatorSid == Session.Account.SID)
				{
					return 100000f;
				}
				if (!Session.Account.MercanaryQuestsTracking.Contains({20839}.ServerID))
				{
					return (float){20839}.PaymentForHeadToCustomer;
				}
				return 90000f;
			});
			base.ComposeTabWithScroll(null, true, false, new Action<ListItemViewControl>[]
			{
				new Action<ListItemViewControl>(this.{20831})
			});
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00081BD5 File Offset: 0x0007FDD5
		private Form {20820}()
		{
			Form form = this.{20826}(Local.mercanary_create, "", true, CommonAtlas.transpPixel);
			form.EvClick += this.{20833};
			return form;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00081C00 File Offset: 0x0007FE00
		private void {20821}(bool {20822})
		{
			{20816}.<>c__DisplayClass8_0 CS$<>8__locals1 = new {20816}.<>c__DisplayClass8_0();
			CS$<>8__locals1.isGuild = {20822};
			{20816}.<>c__DisplayClass8_0 CS$<>8__locals2 = CS$<>8__locals1;
			Vector2 {14078} = default(Vector2);
			CS$<>8__locals2.stack = new StackForm({14078}, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Label label = new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Black * 0.9f, CS$<>8__locals1.isGuild ? Local.mercanary_guild_tag : Local.mercanary_nickname, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			TextBlockBuilder textBlockBuilder = TextBlockBuilder.CreateBlock(300f, CS$<>8__locals1.isGuild ? Local.mercanary_info_guild : Local.mercanary_info_player, Color.Black * 0.8f, Fonts.Arial_10, -3f);
			CS$<>8__locals1.parameterInput = new TextBox(Vector2.Zero, {17177}.c_textBoxBlack, Fonts.Philosopher_14, Color.Black, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.parameterInput.Pos = CS$<>8__locals1.parameterInput.Pos.SetWidth(CS$<>8__locals1.parameterInput.Pos.WH.X * 2f);
			CS$<>8__locals1.parameterInput.AttachMaxLengthModerator(CS$<>8__locals1.isGuild ? 3 : 25, null, Color.Transparent);
			CS$<>8__locals1.stack.AddItem(new UiControl[]
			{
				label,
				textBlockBuilder.Create(),
				CS$<>8__locals1.parameterInput
			});
			CS$<>8__locals1.stack.AddSpace(5f);
			Label label2 = new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Black * 0.9f, Local.mercanary_reward, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.stack.AddItem(new UiControl[]
			{
				label2
			});
			CS$<>8__locals1.priceInput = CS$<>8__locals1.<KeepCreatingDialog>g__TextBoxWithLeftBlock|0(delegate(TextBox {20840})
			{
				if ({20840}.ParseInt >= 30)
				{
					return Local.mercanary_customer_reward(MercanaryOrder.ToCustomer({20840}.ParseInt));
				}
				return Local.PortInputMonetsToBuyWindow_4(30);
			});
			CS$<>8__locals1.priceInput.Mask = TextBoxMaskType.Count;
			CS$<>8__locals1.stack.AddSpace(5f);
			Label label3 = new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Black * 0.9f, Local.mercanary_quantity, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			TextBlockBuilder textBlockBuilder2 = TextBlockBuilder.CreateBlock(300f, Local.mercanary_completion_info, Color.Black * 0.8f, Fonts.Arial_10, -3f);
			CS$<>8__locals1.stack.AddItem(new UiControl[]
			{
				label3,
				textBlockBuilder2.Create()
			});
			CS$<>8__locals1.countIn = CS$<>8__locals1.<KeepCreatingDialog>g__TextBoxWithLeftBlock|0(delegate(TextBox {20845})
			{
				if (CS$<>8__locals1.priceInput.ParseInt * {20845}.ParseInt > Session.Account.Monets.Value)
				{
					return Local.PortInputMonetsToBuyWindow_2;
				}
				if (CS$<>8__locals1.priceInput.ParseInt * {20845}.ParseInt > 0)
				{
					return string.Concat(new string[]
					{
						Local.cost,
						": ",
						(CS$<>8__locals1.priceInput.ParseInt * {20845}.ParseInt).ToString(),
						" ",
						Local.monets
					});
				}
				return " ";
			});
			CS$<>8__locals1.countIn.Mask = TextBoxMaskType.Count;
			CS$<>8__locals1.countIn.Text = "1";
			CS$<>8__locals1.makeAnon = new CheckboxControl(Vector2.Zero, CommonAtlas.vd_checkBox_26px_true, CommonAtlas.vd_checkBox_26px_false, true, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.makeAnon.SetText(Local.mercanary_make_anon, Fonts.Philosopher_14, Color.Black * 0.8f);
			CS$<>8__locals1.errLabel = new Label(Vector2.Zero, Fonts.Philosopher_14, Color.DarkRed, " ", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.customContinueButton = new Button(Vector2.Zero, {17625}.c_btLight_big, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.customContinueButton.SetText(Local.to_continue, Fonts.Philosopher_14, Color.Black * 0.9f, false);
			CS$<>8__locals1.customContinueButton.EvClick += delegate(ClickUiEventArgs {20846})
			{
				if (CS$<>8__locals1.priceInput.ParseInt < 30 || CS$<>8__locals1.parameterInput.Text.Length == 0)
				{
					return;
				}
				int parseInt = CS$<>8__locals1.priceInput.ParseInt;
				int parseInt2 = CS$<>8__locals1.countIn.ParseInt;
				IMPSerializable {8513};
				if (!CS$<>8__locals1.isGuild)
				{
					IMPSerializable impserializable = new MercanaryOrder.TargetPlayer(CS$<>8__locals1.parameterInput.Text, string.Empty);
					{8513} = impserializable;
				}
				else
				{
					IMPSerializable impserializable = new MercanaryOrder.TargetGuild(CS$<>8__locals1.parameterInput.Text);
					{8513} = impserializable;
				}
				MercanaryOrder {10567} = new MercanaryOrder(parseInt, parseInt2, {8513}, CS$<>8__locals1.makeAnon.IsChecked ? string.Empty : Session.Account.PlayerName);
				Global.Network.Send(new OnMercanaryOrderUpdates({10567}, OnMercanaryOrderUpdates.Update.CreateRequest));
				CS$<>8__locals1.customContinueButton.AllowMouseInput = false;
			};
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				CS$<>8__locals1.customContinueButton,
				CS$<>8__locals1.makeAnon
			});
			CS$<>8__locals1.stack.AddItem(new UiControl[]
			{
				CS$<>8__locals1.errLabel,
				stackForm
			});
			CS$<>8__locals1.dialog = new {17107}(Local.mercanary_create, Local.mercanary_info(10), string.Empty, delegate(int {20841})
			{
			}, true, null, Array.Empty<string>());
			UiControl stack = CS$<>8__locals1.stack;
			Marker pos = CS$<>8__locals1.stack.Pos;
			{14078} = CS$<>8__locals1.dialog.Pos.XY + new Vector2(55f, 270f);
			stack.Pos = pos.SetXY({14078});
			CS$<>8__locals1.dialog.AddChild(CS$<>8__locals1.stack);
			this.{20837} = delegate(OnMercanaryOrderUpdates {20847})
			{
				if ({20847}.updateType == OnMercanaryOrderUpdates.Update.CreateSuccess)
				{
					PlayerAccount account = Session.Account;
					account.Monets.Value = account.Monets.Value - {20847}.Instance.InitialCount * {20847}.Instance.PaymentForHead;
					CS$<>8__locals1.dialog.BlockAndClose();
				}
				else if ({20847}.updateType == OnMercanaryOrderUpdates.Update.CreateTargetError)
				{
					CS$<>8__locals1.errLabel.Text = Local.mercanary_target_err;
				}
				else if ({20847}.updateType == OnMercanaryOrderUpdates.Update.CreateTargetRankError)
				{
					CS$<>8__locals1.errLabel.Text = Local.mercanary_target_rank_err(15);
				}
				else if ({20847}.updateType == OnMercanaryOrderUpdates.Update.CreateSomeError)
				{
					CS$<>8__locals1.errLabel.Text = Local.mercanary_other_err;
				}
				else if ({20847}.updateType == OnMercanaryOrderUpdates.Update.CreateTargetErrorAlly)
				{
					CS$<>8__locals1.errLabel.Text = Local.mercanary_target_ally_err;
				}
				CS$<>8__locals1.customContinueButton.AllowMouseInput = true;
			};
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00082030 File Offset: 0x00080230
		public void WhenResponse(OnMercanaryOrderUpdates {20823})
		{
			if ({20823}.updateType == OnMercanaryOrderUpdates.Update.CreateSomeError || {20823}.updateType == OnMercanaryOrderUpdates.Update.CreateSuccess || {20823}.updateType == OnMercanaryOrderUpdates.Update.CreateTargetError || {20823}.updateType == OnMercanaryOrderUpdates.Update.CreateTargetRankError || {20823}.updateType == OnMercanaryOrderUpdates.Update.CreateTargetErrorAlly)
			{
				Action<OnMercanaryOrderUpdates> action = this.{20837};
				if (action != null)
				{
					action({20823});
				}
			}
			if ({20823}.updateType == OnMercanaryOrderUpdates.Update.CreateSuccess)
			{
				this.{20838}.Add({20823}.Instance);
				this.{20819}();
			}
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x000820A0 File Offset: 0x000802A0
		private Form {20824}(MercanaryOrder {20825})
		{
			{20816}.<>c__DisplayClass10_0 CS$<>8__locals1 = new {20816}.<>c__DisplayClass10_0();
			CS$<>8__locals1.order = {20825};
			CS$<>8__locals1.<>4__this = this;
			{20816}.<>c__DisplayClass10_0 CS$<>8__locals2 = CS$<>8__locals1;
			string title = CS$<>8__locals1.order.GetTitle();
			string str;
			if (CS$<>8__locals1.order.CreatorSid != Session.Account.SID)
			{
				str = string.Empty;
			}
			else
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.order.InitialCount - CS$<>8__locals1.order.RemainCount);
				defaultInterpolatedStringHandler.AppendLiteral(" / ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.order.InitialCount);
				str = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			CS$<>8__locals2.form = this.{20826}(title + str, "+" + CS$<>8__locals1.order.PaymentForHeadToCustomer.ToString() + " " + Local.monets, false, (CS$<>8__locals1.order.Target is MercanaryOrder.TargetGuild) ? {20816}.c_icon_guild : {20816}.c_icon_single);
			CS$<>8__locals1.form.EvClick += delegate(ClickUiEventArgs {20842})
			{
				if (CS$<>8__locals1.order.CreatorSid == Session.Account.SID)
				{
					return;
				}
				MercanaryOrder.TargetGuild targetGuild = CS$<>8__locals1.order.Target as MercanaryOrder.TargetGuild;
				if (targetGuild == null || Session.Guild == null || !(targetGuild.InitialTag == Session.Guild.Tag))
				{
					IMPSerializable target = CS$<>8__locals1.order.Target;
					MercanaryOrder.TargetPlayer player = target as MercanaryOrder.TargetPlayer;
					if (player == null || (!(player.InitialName == Session.Account.PlayerName) && (Session.Guild == null || !Session.Guild.Members.Any((GuildMember {20843}) => {20843}.Cached.Name == player.InitialName))))
					{
						bool flag = Session.Account.MercanaryQuestsTracking.Contains(CS$<>8__locals1.order.ServerID);
						if (!flag && Session.Account.MercanaryQuestsTracking.Size >= 10)
						{
							return;
						}
						CS$<>8__locals1.form.AllowMouseInput = false;
						CS$<>8__locals1.form.Opacity = 0.5f;
						Global.Network.Send(new OnMercanaryOrderUpdates(CS$<>8__locals1.order, OnMercanaryOrderUpdates.Update.TrackingStateChange));
						if (flag)
						{
							Session.Account.MercanaryQuestsTracking.Remove(CS$<>8__locals1.order.ServerID);
						}
						else
						{
							Session.Account.MercanaryQuestsTracking.Add(CS$<>8__locals1.order.ServerID);
						}
						CS$<>8__locals1.<>4__this.{20819}();
						return;
					}
				}
				new {17312}(Local.mercanary_cannot_take_myself);
			};
			if (CS$<>8__locals1.order.CreatorSid == Session.Account.SID)
			{
				CS$<>8__locals1.form.AddChild(new Label(new Vector2(70f, 45f), Fonts.Arial_12, Color.Lime * 0.7f, Local.mercanary_timeout + new LocalizedDateTime(CS$<>8__locals1.order.TimeoutDate.ToDateTime, false).GetDateAndTime(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			if (Session.Account.MercanaryQuestsTracking.Contains(CS$<>8__locals1.order.ServerID))
			{
				CS$<>8__locals1.form.AddChild(new Label(new Vector2(70f, 45f), Fonts.Arial_12, Color.Orange * 0.7f, Local.tracking, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			if (CS$<>8__locals1.order.CreatorSid == Session.Account.SID)
			{
				CS$<>8__locals1.form.ToolTipState = new ToolTipState(Local.mercanary_progress(CS$<>8__locals1.order.InitialCount - CS$<>8__locals1.order.RemainCount, CS$<>8__locals1.order.RemainCount), Local.mercanary_progress_tt(CS$<>8__locals1.order.RemainCount * CS$<>8__locals1.order.PaymentForHead), Array.Empty<ToolTipCharacteristics>());
			}
			else
			{
				string {12777} = string.IsNullOrEmpty(CS$<>8__locals1.order.OrderCreatorNameOrNull) ? Local.mercanary_creator_anon : Local.mercanary_creator_name(CS$<>8__locals1.order.OrderCreatorNameOrNull);
				if (Session.Account.MercanaryQuestsTracking.Contains(CS$<>8__locals1.order.ServerID))
				{
					CS$<>8__locals1.form.ToolTipState = new ToolTipState({12777}, Local.click_to_untrack, Array.Empty<ToolTipCharacteristics>());
				}
				else
				{
					CS$<>8__locals1.form.ToolTipState = new ToolTipState({12777}, (Session.Account.MercanaryQuestsTracking.Size >= 10) ? Local.TavernaCommonUi_16 : Local.click_to_track, Array.Empty<ToolTipCharacteristics>());
				}
			}
			return CS$<>8__locals1.form;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x000823A0 File Offset: 0x000805A0
		private Form {20826}(string {20827}, string {20828}, bool {20829}, Rectangle {20830})
		{
			Form form = new Form(Vector2.Zero, {20829} ? {18417}.c_pageItemU : {18417}.c_pageItem, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			form.AddChild(new Label(new Vector2(70f, 5f), Fonts.Arial_12, Color.Wheat * 0.8f, {20827}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Label(new Vector2(70f, 26f), Fonts.Arial_12, {20829} ? (new Color(200, 255, 200) * 0.9f) : (Color.White * 0.6f), {20828}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Form(new Vector2(3f, 3f), {20830}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
			return form;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x000824A8 File Offset: 0x000806A8
		[CompilerGenerated]
		private void {20831}(ListItemViewControl {20832})
		{
			BlocksStackFormControl blocksStackFormControl = new BlocksStackFormControl(Vector2.Zero, 2, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			blocksStackFormControl.AddItem(new UiControl[]
			{
				this.{20820}()
			});
			foreach (MercanaryOrder {20825} in ((IEnumerable<MercanaryOrder>)this.{20838}))
			{
				blocksStackFormControl.AddItem(new UiControl[]
				{
					this.{20824}({20825})
				});
			}
			{20832}.AddItem(new UiControl[]
			{
				blocksStackFormControl
			});
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00082538 File Offset: 0x00080738
		[CompilerGenerated]
		private void {20833}(ClickUiEventArgs {20834})
		{
			new {17107}(Local.mercanary_create, Local.mercanary_info(10), "", new Action<int>(this.{20835}), true, null, new string[]
			{
				Local.mercanary_order_guild,
				Local.mercanary_order_player,
				Local.to_back
			});
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0008258D File Offset: 0x0008078D
		[CompilerGenerated]
		private void {20835}(int {20836})
		{
			if ({20836} == 2)
			{
				return;
			}
			this.{20821}({20836} == 0);
		}

		// Token: 0x04000E2E RID: 3630
		private static readonly Rectangle c_icon_single = new Rectangle(614, 142, 67, 61);

		// Token: 0x04000E2F RID: 3631
		private static readonly Rectangle c_icon_guild = new Rectangle(605, 79, 61, 61);

		// Token: 0x04000E30 RID: 3632
		public static {20816} CurrentInstance;

		// Token: 0x04000E31 RID: 3633
		private Action<OnMercanaryOrderUpdates> {20837};

		// Token: 0x04000E32 RID: 3634
		private Tlist<MercanaryOrder> {20838};
	}
}
