using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Common;
using Common.Game;
using Common.Packets;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using WorldOfSeaBattles.Interface.BuildingsUi;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000255 RID: 597
	public class {20217} : Form
	{
		// Token: 0x06000D67 RID: 3431 RVA: 0x00070BD0 File Offset: 0x0006EDD0
		public {20217}() : base(new Marker(0f, 0f, (float){20364}.ContentWidth, 1000f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.AnimatedFocus = false;
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				this.{20219}()
			});
			stackForm.AddSpace(20f);
			stackForm.AddItem(new UiControl[]
			{
				this.{20220}()
			});
			stackForm.AddSpace(20f);
			stackForm.AddItem(new UiControl[]
			{
				this.{20221}()
			});
			base.AddChildPos(stackForm, PositionAlignment.LeftUp, PositionAlignment.LeftUp, 10f);
			UiControl uiControl = this.{20229}();
			base.AddChildPos(uiControl, PositionAlignment.RightDown, PositionAlignment.LeftUp, 25f, 10f, false);
			base.Pos = base.Pos.SetHeight(Math.Max(stackForm.Pos.WH.Y, uiControl.Pos.WH.Y));
			this.UpdateImage();
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00070CD3 File Offset: 0x0006EED3
		public void UpdateImage()
		{
			if (Session.LoadedGuildIconTexture == null)
			{
				return;
			}
			this.{20250}.Texture = Session.LoadedGuildIconTexture;
			this.{20250}.TexturePath = Session.LoadedGuildIconTexture.Bounds;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00070D04 File Offset: 0x0006EF04
		private UiControl {20218}()
		{
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Form form = new Form(new Marker(0f, 0f, 128f, 30f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_24, Color.White, Session.Guild.Tag, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.Center, 0f);
			stackForm.AddItem(new UiControl[]
			{
				form
			});
			this.{20250} = new Image(new Marker(0f, 0f, 128f, 128f), CommonAtlas.Texture.Tex, {20217}.c_guildEmptyIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{20250}.EvClick += this.{20234};
			stackForm.AddItem(new UiControl[]
			{
				this.{20250}
			});
			return stackForm;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00070DE0 File Offset: 0x0006EFE0
		private UiControl {20219}()
		{
			{20217}.<>c__DisplayClass16_0 CS$<>8__locals1;
			CS$<>8__locals1.info = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.defaultFont = Fonts.Arial_10;
			CS$<>8__locals1.defInterval = 0;
			{20217}.<CreateInfo>g__AddDescAndEdit|16_0(Session.Guild.Name, Local.GuildWindow_14, 1, Gcc.GuildNameLimits.Item1, Gcc.GuildNameLimits.Item2, false, ref CS$<>8__locals1);
			{20217}.<CreateInfo>g__AddDescAndEdit|16_0(Session.Guild.Description, Local.GuildWindow_16, 2, 0, 187, false, ref CS$<>8__locals1);
			{20217}.<CreateInfo>g__AddDescAndEdit|16_0(Session.Guild.InternalInformation, Local.GuildWindow_18, 4, 0, 250, false, ref CS$<>8__locals1);
			{20217}.<CreateInfo>g__AddDescAndEdit|16_0((Session.Guild.CurrentAnnouncment.Item1.Length > 40) ? (Session.Guild.CurrentAnnouncment.Item1.Substring(0, 39) + "...") : Session.Guild.CurrentAnnouncment.Item1, Local.guild_announcment, 5, 0, 250, true, ref CS$<>8__locals1);
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				this.{20218}()
			});
			stackForm.AddSpace(20f);
			stackForm.AddItem(new UiControl[]
			{
				CS$<>8__locals1.info
			});
			return stackForm;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00070F20 File Offset: 0x0006F120
		private UiControl {20220}()
		{
			Tlist<Form> tlist = new Tlist<Form>();
			Color {20225} = new Color(219, 40, 0);
			if (Session.Guild.Fraction.IsNation() && Session.Guild.Members.Size > 15 && Session.Guild.Members.Size < GuildCommon.MinMembersForNation(Session.Game.WorldPorts.FractionBonusHelper(Session.Guild.Fraction)))
			{
				Tlist<Form> tlist2 = tlist;
				Form form = this.CreateItemList(Local.low_members_alert(Session.Guild.Fraction.GetName(), 15), Color.Yellow, null, null, null);
				tlist2.Add(form);
			}
			foreach (GuildTemporaryEffect guildTemporaryEffect in ((IEnumerable<GuildTemporaryEffect>)Session.Guild.Effects))
			{
				if (guildTemporaryEffect.Case == GuildTemporaryEffect.Type.ChangeFractionRistriction)
				{
					Tlist<Form> tlist3 = tlist;
					Form form = this.CreateItemList(Local.GuildWindow_SyncProtected_14, Color.Yellow, new double?(guildTemporaryEffect.RemainingTimeSec), null, null);
					tlist3.Add(form);
				}
				if (guildTemporaryEffect.Case == GuildTemporaryEffect.Type.HqOrLicenseInPort && Session.Guild.Fraction == FractionID.TradeUnion)
				{
					Tlist<Form> tlist4 = tlist;
					Form form = this.CreateItemList("⚖" + Local.trade_hq + ": " + Gameplay.WorldMap.Ports.Array[(int)guildTemporaryEffect.ArgumentByte].PortNameShort, Color.Orange, new double?(guildTemporaryEffect.RemainingTimeSec), null, null);
					tlist4.Add(form);
				}
				if (guildTemporaryEffect.Case == GuildTemporaryEffect.Type.RemoveGuildNextSeason)
				{
					Tlist<Form> tlist5 = tlist;
					Form form = this.CreateItemList(Local.GuildWindow_SyncProtected_21, {20225}, new double?(guildTemporaryEffect.RemainingTimeSec), null, null);
					tlist5.Add(form);
				}
				if (guildTemporaryEffect.Case == GuildTemporaryEffect.Type.DisputeVoteReceived)
				{
					Tlist<Form> tlist6 = tlist;
					Form form = this.CreateItemList(Local.guild_dispute_effect(guildTemporaryEffect.Argument), {20225}, new double?(guildTemporaryEffect.RemainingTimeSec), null, null);
					tlist6.Add(form);
				}
				if (guildTemporaryEffect.Case == GuildTemporaryEffect.Type.BlockChangeWindowAgain)
				{
					Tlist<Form> tlist7 = tlist;
					Form form = this.CreateItemList(Local.GuildWindow_SyncProtected_22, Color.YellowGreen, new double?(guildTemporaryEffect.RemainingTimeSec), null, null);
					tlist7.Add(form);
				}
				if (guildTemporaryEffect.Case == GuildTemporaryEffect.Type.BlockPortCaptureAgain)
				{
					Tlist<Form> tlist8 = tlist;
					Form form = this.CreateItemList(Local.block_capture_again(Gameplay.WorldMap.Ports[(int)guildTemporaryEffect.ArgumentByte].PortNameShort), Color.YellowGreen, new double?(guildTemporaryEffect.RemainingTimeSec), null, null);
					tlist8.Add(form);
				}
				if (guildTemporaryEffect.Case == GuildTemporaryEffect.Type.FounderHasReputationProblem)
				{
					Tlist<Form> tlist9 = tlist;
					Form form = this.CreateItemList(Local.Info_FounderHasReputationProblem, {20225}, null, null, null);
					tlist9.Add(form);
				}
				if (guildTemporaryEffect.Case == GuildTemporaryEffect.Type.VassalPortMarker)
				{
					Tlist<Form> tlist10 = tlist;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
					defaultInterpolatedStringHandler.AppendFormatted(Local.GuildWindow_VassalPaymentEffect);
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted(Gameplay.WorldMap.Ports.Array[(int)guildTemporaryEffect.ArgumentByte].PortNameShort);
					defaultInterpolatedStringHandler.AppendLiteral(" - ");
					defaultInterpolatedStringHandler.AppendFormatted<float>(Session.Guild.ColonizeTax * 100f, "F0");
					defaultInterpolatedStringHandler.AppendLiteral("%");
					Form form = this.CreateItemList(defaultInterpolatedStringHandler.ToStringAndClear(), Color.YellowGreen, new double?(guildTemporaryEffect.RemainingTimeSec), null, null);
					tlist10.Add(form);
				}
				if (guildTemporaryEffect.Case == GuildTemporaryEffect.Type.DisputeProtection)
				{
					Tlist<Form> tlist11 = tlist;
					Form form = this.CreateItemList(Local.GuildWindow_DisputeProtectionEffect, Color.YellowGreen, new double?(guildTemporaryEffect.RemainingTimeSec), null, null);
					tlist11.Add(form);
				}
			}
			if (Session.Guild.Effects.Size == 0)
			{
				Tlist<Form> tlist12 = tlist;
				Form form = this.CreateItemList(Local.GuildWindow_SyncProtected_19, Color.Gray, null, null, null);
				tlist12.Add(form);
			}
			return this.CreateStack(Local.GuildWindow_SyncProtected_13, tlist);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00071364 File Offset: 0x0006F564
		private UiControl {20221}()
		{
			Tlist<Form> tlist = new Tlist<Form>();
			foreach (GSILocalPair gsilocalPair in ((IEnumerable<GSILocalPair>)Session.Guild.ReceivedTitles))
			{
				GuildTitle guildTitle = Gameplay.GuildTitles[gsilocalPair.ID];
				Tlist<Form> tlist2 = tlist;
				Form form = this.CreateItemList(guildTitle.Name, Color.Yellow, null, guildTitle.GetSmallIcon(), new int?(gsilocalPair.Count));
				tlist2.Add(form);
			}
			if (Session.Guild.ReceivedTitles.All((GSILocalPair {20251}) => {20251}.Count == 0))
			{
				Tlist<Form> tlist3 = tlist;
				Form form = this.CreateItemList(Local.no_titles, Color.Gray, null, null, null);
				tlist3.Add(form);
			}
			return this.CreateStack(Local.recieved_titles, tlist);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00071468 File Offset: 0x0006F668
		private UiControl CreateStack(string {20222}, IEnumerable<Form> {20223})
		{
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_16Bold, Color.LightGray, " " + {20222}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			foreach (Form form in {20223})
			{
				stackForm.AddItem(new UiControl[]
				{
					form
				});
			}
			Form form2 = new Form(new Marker(0f, 0f, 509f, stackForm.Pos.WH.Y), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form2.AnimatedFocus = false;
			form2.AddChild(stackForm);
			return form2;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00071530 File Offset: 0x0006F730
		private Form CreateItemList(string {20224}, Color {20225}, double? {20226} = null, Image {20227} = null, int? {20228} = null)
		{
			{20225} = Color.Lerp({20225}, Color.Gray, 0.4f);
			Rectangle c_item = {20364}.c_item4;
			Form form = new Form(new Marker(0f, 0f, 560f, (float)c_item.Height), c_item, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			float num = 0f;
			if ({20227} != null)
			{
				form.AddChildPos({20227}, PositionAlignment.LeftUp, PositionAlignment.Center, 10f, 0f, false);
				num = {20227}.Pos.WH.X;
			}
			form.AddChild(new Label(new Vector2(15f + num, 6f), Fonts.Arial_12, {20225}, {20224}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			if ({20226} != null)
			{
				form.AddChild(new Label(new Vector2(350f, 6f), Fonts.Arial_12, Color.Gray, StringHelper.TimeD({20226}.Value), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			if ({20228} != null)
			{
				Form form2 = form;
				Vector2 {13342} = new Vector2(350f, 6f);
				CustomSpriteFont arial_ = Fonts.Arial_12;
				Color gray = Color.Gray;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
				defaultInterpolatedStringHandler.AppendLiteral("x");
				defaultInterpolatedStringHandler.AppendFormatted<int>({20228}.Value);
				form2.AddChild(new Label({13342}, arial_, gray, defaultInterpolatedStringHandler.ToStringAndClear(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			return form;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00071668 File Offset: 0x0006F868
		private UiControl {20229}()
		{
			{20217}.<>c__DisplayClass21_0 CS$<>8__locals1 = new {20217}.<>c__DisplayClass21_0();
			CS$<>8__locals1.stack = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BorderThickness = -3f
			};
			CS$<>8__locals1.<CreateRight>g__AddNode|0(Local.GuildWindow_23, {20217}.c_node_orange, Color.Orange, (object {20252}) => StringHelper.BigValueHelper(Session.Guild.Treasury), Local.gamewiki_treasury_text2, false, new Rectangle?({20217}.c_node_orange_addButton), delegate(ClickUiEventArgs {20253})
			{
				if (Global.Game.GetCurrentSceneName != GameSceneName.Port)
				{
					new {17312}(Local.GuildWindow_26B);
					return;
				}
				if (Session.Account.NearPortStorage.GetCount(24) == 0)
				{
					new {17312}(Local.GuildWindow_26);
					return;
				}
				new {21838}(Local.GuildWindow_27, Local.to_continue, delegate(int {20254})
				{
					Session.Account.NearPortStorage.AddOrRemove(24, -{20254});
					Global.Network.Send(new OnChangeGuildMintOrTreasuryMsg(true, {20254}, 0U));
				}, Session.Account.NearPortStorage.GetCount(24), delegate(int {20255})
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 2);
					defaultInterpolatedStringHandler.AppendFormatted(Local.in_storage);
					defaultInterpolatedStringHandler.AppendFormatted<int>(Session.Account.NearPortStorage.GetCount(24) - {20255});
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}, new int?(1), null, null, null);
			});
			CS$<>8__locals1.<CreateRight>g__AddNode|0(Local.GuildWindow_28, {20217}.c_node_taxes, Color.Orange, (object {20256}) => StringHelper.BigValueHelper(Session.Guild.Mint), Session.Guild.IsFlotilia ? Local.flotilia_limit : Local.gamewiki_treasury_text3, !Session.Guild.HasMintAndSalary, null, null);
			CS$<>8__locals1.<CreateRight>g__AddNode|0(Local.GuildWindow_salary, {20217}.c_node_gold, Color.Yellow * 0.8f, (object {20257}) => StringHelper.BigValueHelper(Session.GuildUnreceivedSalary), Session.Guild.IsFlotilia ? Local.flotilia_limit : Local.GuildWindow_salary_tt, !Session.Guild.HasMintAndSalary, null, null);
			CS$<>8__locals1.<CreateRight>g__AddNode|0(Local.conquer_badges, {20217}.c_node_cyan, Color.Cyan, (object {20258}) => Session.Guild.ConquerBadges.ToString(), Local.gamewiki_guildsgrow_text1, false, new Rectangle?({20217}.c_node_cyan_addButton), delegate(ClickUiEventArgs {20259})
			{
				string str = Local.GuildWindow_33(Gameplay.ExchangeMarkToConquerorBadges.Value, Gameplay.ExchangeMarkToConquerorBadges_PiratePort.Value);
				if (Global.Player.IsPortEntry)
				{
					new {17312}(str + Local.GuildWindow_33_a(Session.Account.NearPortStorage[37]), delegate(int {20260})
					{
						if ({20260} == 1)
						{
							return;
						}
						int price = (Global.Player.NearPort.Type == PortType.PirateBay) ? Gameplay.ExchangeMarkToConquerorBadges_PiratePort.Value : Gameplay.ExchangeMarkToConquerorBadges.Value;
						new {21838}(Local.conquer_badges, Local.shop, delegate(int {20276})
						{
							if ({20276} * price > Session.Account.NearPortStorage[37])
							{
								return;
							}
							GSI nearPortStorage = Session.Account.NearPortStorage;
							nearPortStorage[37] = nearPortStorage[37] - price * {20276};
							Global.Network.Send(new OnExchangeWarmarkToConOrd({20276}));
						}, Session.Account.NearPortStorage[37] / price, (int {20277}) => Local.price_warmarks({20277} * price), new int?(1), null, null, null);
					}, new string[]
					{
						Local.GuildWindow_36,
						Local.GuildWindow_37
					});
					return;
				}
				new {17312}(str + Local.GuildWindow_33_b, delegate(int {20261})
				{
				}, new string[]
				{
					Local.close
				});
			});
			CS$<>8__locals1.hasBonuses = WosbPbs.FractionHasPBBonus(Session.Guild.Fraction, Session.Game.NumCapturedPortsMyFraction);
			CS$<>8__locals1.errorIcon = CS$<>8__locals1.<CreateRight>g__AddNode|0(Local.GuildWindow_38, {20217}.c_node_green, Color.SeaGreen, (object {20262}) => Session.Game.NumCapturedPorts.ToString(), (Session.GuildCacheServer_CapturingRistriction != CapturePortsRistrictionType.None) ? (Local.capture_ports_limit_h + ": " + Session.GuildCacheServer_CapturingRistriction.ToolTipText()) : (CS$<>8__locals1.hasBonuses ? Local.fraction_has_pb_bonuses : string.Empty), false, new Rectangle?({20217}.c_node_green_error), null).Item2;
			CS$<>8__locals1.errorIcon.IsVisible = false;
			CS$<>8__locals1.errorIcon.UpdateComplete += delegate(UiControl {20275})
			{
				if (Session.GuildCacheServer_CapturingRistriction != CapturePortsRistrictionType.None)
				{
					CS$<>8__locals1.errorIcon.TexturePath = {20217}.c_node_green_error;
					CS$<>8__locals1.errorIcon.IsVisible = true;
					return;
				}
				if (CS$<>8__locals1.hasBonuses)
				{
					CS$<>8__locals1.errorIcon.TexturePath = {20217}.c_node_arrow_up;
					CS$<>8__locals1.errorIcon.IsVisible = true;
					return;
				}
				CS$<>8__locals1.errorIcon.IsVisible = false;
			};
			if (!Session.Guild.IsFlotilia && Session.Guild.Fraction.CanCapturePorts())
			{
				StackForm stack = CS$<>8__locals1.stack;
				UiControl[] array = new UiControl[1];
				array[0] = {20217}.<CreateRight>g__CreatePBSetting|21_9(Local.pbsstatus_windowInfo(""), Session.Guild.PbsWindow.ToStringFull(null, false), new ToolTipState(TextBlockBuilder.CreateBlock(300f, Local.click_to_change, Color.White, Fonts.Philosopher_14Bold, 1f)), Session.Guild.HasEffect(GuildTemporaryEffect.Type.BlockChangeWindowAgain), delegate(ClickUiEventArgs {20263})
				{
					if (!Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury)
					{
						new {17312}(Local.GuildWindow_3(Local.guildright_tt_AllowEditGuildAndFortAndTreasury));
						return;
					}
					new ChangePbsWindowDialog();
				});
				stack.AddItem(array);
				CS$<>8__locals1.stack.AddSpace(20f);
				if (Session.Guild.PbCaptureMode == PbCaptureMode.Server_Nothing)
				{
					Session.Guild.PbCaptureMode = PbCaptureMode.Capture;
					Global.Network.Send(new OnGuildMembersChanges(OnGuildMembersChanges.GAction.PbCaptureModeChanged, 0U, 0, ""));
				}
				PbCaptureMode[] values = (from {20264} in Enum.GetValues<PbCaptureMode>()
				where {20264} != PbCaptureMode.Server_Nothing
				select {20264}).ToArray<PbCaptureMode>();
				int currentWinActionIndex = Array.IndexOf<PbCaptureMode>(values, Session.Guild.PbCaptureMode);
				TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_12, 0f);
				foreach (PbCaptureMode pbCaptureMode in values)
				{
					textBlockBuilder.WriteLine("[" + pbCaptureMode.GetName() + "]", Color.Wheat, Fonts.Philosopher_14Bold);
					textBlockBuilder.WriteLines(pbCaptureMode.GetDesc(), Color.Wheat, Fonts.Arial_10, 300f, new float?(0f));
					textBlockBuilder.WriteSpaceLine(10f);
				}
				textBlockBuilder.WriteSpaceLine(10f);
				textBlockBuilder.WriteLine(Local.click_to_change, Color.White, Fonts.Philosopher_14Bold);
				CS$<>8__locals1.stack.AddItem(new UiControl[]
				{
					{20217}.<CreateRight>g__CreatePBSetting|21_9(Local.guild_win_action(""), Session.Guild.PbCaptureMode.GetName(), new ToolTipState(textBlockBuilder), false, delegate(ClickUiEventArgs {20279})
					{
						if (!Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury)
						{
							new {17312}(Local.GuildWindow_3(Local.guildright_tt_AllowEditGuildAndFortAndTreasury));
							return;
						}
						int currentWinActionIndex = currentWinActionIndex;
						currentWinActionIndex++;
						if (currentWinActionIndex >= values.Length)
						{
							currentWinActionIndex = 0;
						}
						PbCaptureMode {9410} = values[currentWinActionIndex];
						Global.Network.Send(new OnGuildMembersChanges(OnGuildMembersChanges.GAction.PbCaptureModeChanged, 0U, (byte){9410}, ""));
					})
				});
				CS$<>8__locals1.stack.AddSpace(30f);
			}
			Form form = new Form(CS$<>8__locals1.stack.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChild(new Form(new Marker(-15f, -30f, 419f, 570f), {20217}.c_nodes_back, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
			form.AddChild(CS$<>8__locals1.stack);
			return form;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00071B90 File Offset: 0x0006FD90
		private static void ShowTextInput(int {20230}, int {20231}, int {20232}, string {20233})
		{
			TextBox.Moderator <>9__2;
			new {17312}(delegate(string {20280})
			{
				if (Gcc.ModerateText({20280}, {20231}, {20232}) == null)
				{
					Global.Network.Send(new OnChangeGuldTextProperty({20230}, {20280}));
				}
			}, {20232}, Local.put_text, {20233}, delegate(TextBox {20281})
			{
				TextBox.Moderator {13698};
				if (({13698} = <>9__2) == null)
				{
					{13698} = (<>9__2 = delegate(ref string {20282})
					{
						return Gcc.ModerateText({20282}, {20231}, {20232});
					});
				}
				{20281}.AttachModerator({13698}, Color.Yellow);
			});
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00071D34 File Offset: 0x0006FF34
		[CompilerGenerated]
		private void {20234}(ClickUiEventArgs {20235})
		{
			if (!Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury)
			{
				new {17312}(Local.GuildWindow_3(Local.guildright_tt_AllowEditGuildAndFortAndTreasury));
				return;
			}
			Thread thread = new Thread(new ParameterizedThreadStart(this.{20236}));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00071D80 File Offset: 0x0006FF80
		[NullableContext(2)]
		[CompilerGenerated]
		private void {20236}(object {20237})
		{
			string text = Engine.PlatformTools.OpenFileDialog(null, "PNG Image (*.png)|*.png|JPG Image (*.jpg)|*.jpg");
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					byte[] array;
					Texture2D texture2D;
					bool flag;
					if (VisualHelper.FileToJpeg(text, out array, out texture2D, out flag, 7000))
					{
						Session.Guild.UpdateImage(array);
						Session.LoadedGuildIconTexture = VisualHelper.LoadTexture2DFromBytes(array, int.MaxValue, false);
						this.UpdateImage();
					}
					else if (flag)
					{
						new {17312}(Local.GuildWindow_21);
					}
					Global.Network.Send(new OnGuildImage(array));
				}
				catch
				{
					new {17312}(Local.GuildWindow_22);
				}
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00071E20 File Offset: 0x00070020
		[CompilerGenerated]
		internal static void <CreateInfo>g__AddDescAndEdit|16_0(string {20238}, string {20239}, int {20240}, int {20241}, int {20242}, bool {20243} = false, ref {20217}.<>c__DisplayClass16_0 {20244})
		{
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Arial_9, Color.Gray * 0.66f, {20239} + " ", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			stackForm.AddItem(new UiControl[]
			{
				new LabelButton(Vector2.Zero, Local.GuildWindow_13, Fonts.Arial_9, Color.Gold * 0.66f, Color.White, delegate(ClickUiEventArgs {20265})
				{
					if (!Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury)
					{
						new {17312}(Local.GuildWindow_3(Local.guildright_tt_AllowEditGuildAndFortAndTreasury));
						return;
					}
					int id = {20240};
					int minLength = {20241};
					int maxLength = {20242};
					string {20233};
					if ({20240} != 1)
					{
						if ({20240} != 2)
						{
							if ({20240} != 4)
							{
								if ({20240} != 5)
								{
									throw new NotSupportedException();
								}
								{20233} = Session.Guild.CurrentAnnouncment.Item1;
							}
							else
							{
								{20233} = Session.Guild.InternalInformation;
							}
						}
						else
						{
							{20233} = Session.Guild.Description;
						}
					}
					else
					{
						{20233} = Session.Guild.Name;
					}
					{20217}.ShowTextInput(id, minLength, maxLength, {20233});
				})
			});
			if ({20243})
			{
				stackForm.AddItem(new UiControl[]
				{
					new LabelButton(Vector2.Zero, Local.guildright_tt_Delete, Fonts.Arial_9, Color.Gold * 0.66f, Color.White, delegate(ClickUiEventArgs {20266})
					{
						if (!Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury)
						{
							new {17312}(Local.GuildWindow_3(Local.guildright_tt_AllowEditGuildAndFortAndTreasury));
							return;
						}
						Global.Network.Send(new OnChangeGuldTextProperty({20240}, ""));
					})
				});
			}
			stackForm.TexturePath = CommonAtlas.whitePixel;
			stackForm.BasicColor = Color.DarkCyan * 0.1f;
			{20244}.info.AddItem(new UiControl[]
			{
				stackForm
			});
			TextBlockControl textBlockControl = TextBlockBuilder.CreateBlock(360f, {20238}, Color.White * 0.8f, ({20240} == 1) ? Fonts.Philosopher_14 : {20244}.defaultFont, (float){20244}.defInterval).Create();
			Form form = new Form(textBlockControl.Pos.SetWidth(360f).Border(4f), {20217}.c_innerTextForm, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos(textBlockControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp, 4f);
			{20244}.info.AddItem(new UiControl[]
			{
				form
			});
			{20244}.info.AddSpace(12f);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00071FF8 File Offset: 0x000701F8
		[CompilerGenerated]
		internal static UiControl <CreateRight>g__CreatePBSetting|21_9(string {20245}, string {20246}, ToolTipState {20247}, bool {20248}, Action<ClickUiEventArgs> {20249})
		{
			Color color = Color.White * 0.75f;
			Color white = Color.White;
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Arial_12, color, {20245}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm2.EvClick += {20249};
			stackForm2.AllowMouseInput = false;
			stackForm2.UpdateComplete += delegate(UiControl {20278})
			{
				if ({20248})
				{
					{20278}.Opacity = 0.5f;
					{20278}.AllowMouseInput = false;
					return;
				}
				{20278}.Opacity = 1f;
				{20278}.AllowMouseInput = true;
			};
			stackForm2.ToolTip = new ToolTip({20247});
			stackForm2.AddItem(new UiControl[]
			{
				new LabelButton(Vector2.Zero, {20246}, Fonts.Philosopher_14Bold, color, white, null)
			});
			stackForm2.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Arial_9, Color.Orange, " [?]", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			stackForm.AddItem(new UiControl[]
			{
				stackForm2
			});
			return stackForm;
		}

		// Token: 0x04000CA6 RID: 3238
		public static readonly Rectangle c_node_taxes = new Rectangle(2807, 3000, 207, 67);

		// Token: 0x04000CA7 RID: 3239
		private static readonly Rectangle c_guildEmptyIcon = new Rectangle(2807, 2714, 128, 128);

		// Token: 0x04000CA8 RID: 3240
		private static readonly Rectangle c_innerTextForm = new Rectangle(1033, 3485, 299, 135);

		// Token: 0x04000CA9 RID: 3241
		private static readonly Rectangle c_nodes_back = new Rectangle(2006, 3243, 169, 196);

		// Token: 0x04000CAA RID: 3242
		private static readonly Rectangle c_node_orange = new Rectangle(2807, 2925, 207, 67);

		// Token: 0x04000CAB RID: 3243
		private static readonly Rectangle c_node_cyan = new Rectangle(2807, 2850, 207, 67);

		// Token: 0x04000CAC RID: 3244
		private static readonly Rectangle c_node_green = new Rectangle(2807, 3075, 207, 67);

		// Token: 0x04000CAD RID: 3245
		private static readonly Rectangle c_node_gold = new Rectangle(2807, 2646, 207, 67);

		// Token: 0x04000CAE RID: 3246
		private static readonly Rectangle c_node_orange_addButton = new Rectangle(2807, 3143, 103, 50);

		// Token: 0x04000CAF RID: 3247
		private static readonly Rectangle c_node_cyan_addButton = new Rectangle(2911, 3143, 103, 50);

		// Token: 0x04000CB0 RID: 3248
		private static readonly Rectangle c_node_green_error = new Rectangle(2862, 3194, 103, 50);

		// Token: 0x04000CB1 RID: 3249
		public static readonly Rectangle c_node_arrow_up = new Rectangle(2862, 3245, 103, 50);

		// Token: 0x04000CB2 RID: 3250
		private Image {20250};
	}
}
