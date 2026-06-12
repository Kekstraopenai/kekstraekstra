using System;
using System.Collections.Generic;
using Common;
using Common.Account;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000371 RID: 881
	internal sealed class {21949} : {17625}
	{
		// Token: 0x0600130C RID: 4876 RVA: 0x000A1AFC File Offset: 0x0009FCFC
		public {21949}() : base((float)({21949}.c_path.Width * 2), {21949}.c_path, {17604}.InGameWindowWithoutDeco, Rectangle.Empty, Array.Empty<{17625}.DynamicTittle>())
		{
			base.Pos = base.Pos.SetHeight(base.Pos.WH.Y + 65f);
			this.{21960} = Session.Game.CanChangeFlagsInPort(Session.Account.WorldFlagEnteredToPort);
			base.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_16, Color.White * 0.8f, this.{21960} ? Local.PortSelectFlagsWindow_0 : Local.change_flag_ristriction_enemy, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 15f);
			this.{21950}();
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x000A1BBC File Offset: 0x0009FDBC
		private void {21950}()
		{
			StackForm stackForm = this.{21958};
			if (stackForm != null)
			{
				stackForm.RemoveFromContainer();
			}
			this.{21958} = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{21958}.BorderThickness = 2f;
			this.{21958}.AddItem(new UiControl[]
			{
				this.CreateFlagCard(new OpenWorldFlag?(OpenWorldFlag.Peaceful))
			});
			this.{21958}.AddItem(new UiControl[]
			{
				this.CreateFlagCard(new OpenWorldFlag?(OpenWorldFlag.Trader))
			});
			if (Session.Game.MapMyFraction == null)
			{
				this.{21958}.AddItem(new UiControl[]
				{
					this.CreateFlagCard(new OpenWorldFlag?(OpenWorldFlag.Pirate))
				});
			}
			this.{21958}.AddItem(new UiControl[]
			{
				this.CreateFlagCard(new OpenWorldFlag?(OpenWorldFlag.War))
			});
			this.{21958}.AddItem(new UiControl[]
			{
				this.CreateFlagCard(new OpenWorldFlag?(OpenWorldFlag.NoFlag))
			});
			if (Session.Account.LegendaryFlagDaysLeft > 0)
			{
				this.{21958}.AddItem(new UiControl[]
				{
					this.CreateFlagCard(new OpenWorldFlag?(OpenWorldFlag.Legendary))
				});
			}
			else
			{
				this.{21958}.AddItem(new UiControl[]
				{
					this.CreateFlagCard(null)
				});
			}
			base.AddChildPos(this.{21958}, PositionAlignment.Center, PositionAlignment.LeftUp, 65f);
			Form form = this.{21959};
			if (form != null)
			{
				form.RemoveFromContainer();
			}
			this.{21959} = new Form(new Marker(0f, 0f, 400f, 60f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			this.{21959}.AddChild(new Image(new Marker(0f, 0f, 126f, 85f), AtlasPortGui.Texture.Tex, new Rectangle(854, 586, 126, 85), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			this.{21959}.AddChild(new Label(new Vector2(126f, 24f), Fonts.Philosopher_16, new Color(233, 195, 120) * 0.85f, (Session.Account.HoldProtectionSubscriptionSec > 0f) ? ("0% (" + Local.PortRealShopPage_135 + ")") : (Math.Round((double)(100f * Session.Account.WorldFlag.DropResAmount(Global.Player))).ToString() + "% " + Local.Current("PortSelectFlagsWindow_dropinf_0")), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			this.{21959}.AddChild(new Label(new Vector2(126f, 46f), Fonts.Philosopher_14, new Color(233, 195, 120) * 0.6f, Local.PortSelectFlagsWindow_dropinf, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			base.AddChildPos(this.{21959}, PositionAlignment.Center, PositionAlignment.LeftUp, 295f);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x000A1EA0 File Offset: 0x000A00A0
		private Form CreateFlagCard(OpenWorldFlag? {21951})
		{
			{21949}.<>c__DisplayClass6_0 CS$<>8__locals1 = new {21949}.<>c__DisplayClass6_0();
			CS$<>8__locals1.flag = {21951};
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.form = new Form(new Marker(0f, 0f, 170f, 235f), {17625}.c_verticalCardGray, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Color {13344} = Color.Gray;
			OpenWorldFlag? flag = CS$<>8__locals1.flag;
			OpenWorldFlag openWorldFlag = Session.Account.WorldFlag;
			if (flag.GetValueOrDefault() == openWorldFlag & flag != null)
			{
				CS$<>8__locals1.form.TexturePath = {17625}.c_verticalCard;
				CS$<>8__locals1.form.AddChild(new Form(CS$<>8__locals1.form.Pos, {17625}.c_verticalCard, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				});
				{13344} = Color.White;
			}
			Action<TextBlockBuilder> {21957} = null;
			string text = string.Empty;
			if (CS$<>8__locals1.flag != null && Session.Game.CanPickFlagsInNearPort(CS$<>8__locals1.flag.Value, false, out text))
			{
				if (!Session.Account.IsPeaceActivated && ((CS$<>8__locals1.flag.Value == OpenWorldFlag.Peaceful && Session.Account.WantedLevelDisallowsPeacefulFlag) || (CS$<>8__locals1.flag.Value == OpenWorldFlag.Trader && Session.Account.WantedLevelDisallowsPeacefulFlag)))
				{
					{21957} = delegate(TextBlockBuilder {21961})
					{
						{21961}.WriteImage(CommonAtlas.Texture.Tex, {19970}.c_wantedLevelMarker, 1f, null);
					};
					text = Local.not_available_blackmark(StringHelper.TimeDHM((double)Session.Account.CurrentBlackMarkTime, true));
				}
				else
				{
					text = string.Empty;
				}
			}
			if (!this.{21960} && CS$<>8__locals1.flag != null && Session.Account.WorldFlag.IsPeaceMode() != CS$<>8__locals1.flag.Value.IsPeaceMode())
			{
				text = " ";
			}
			flag = CS$<>8__locals1.flag;
			openWorldFlag = Session.Account.WorldFlag;
			if (flag.GetValueOrDefault() == openWorldFlag & flag != null)
			{
				CS$<>8__locals1.form.TexturePath = {17625}.c_verticalCard;
				CS$<>8__locals1.form.AddChild(new Form(CS$<>8__locals1.form.Pos, {17625}.c_verticalCard, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				});
				{13344} = Color.White;
			}
			else if (string.IsNullOrEmpty(text) && CS$<>8__locals1.flag != null)
			{
				CS$<>8__locals1.form.AddChild(new AnimatedButton(CS$<>8__locals1.form.Pos, Rectangle.Empty, Rectangle.Empty, new Rectangle(813, 3573, 67, 92), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				CS$<>8__locals1.form.EvClick += delegate(ClickUiEventArgs {21962})
				{
					{21962}.Sender.ToolTip.CloseIfIsOpen();
					Session.Account.WorldFlag = CS$<>8__locals1.flag.Value;
					Session.Account.IsPeaceActivated = false;
					EducationHelper.MakeEducationFlagWhenQuestActive(EducationOnboarding.ChangeFlags, true);
					new UiActor({21962}.Sender, new Action(CS$<>8__locals1.<>4__this.{21950}));
				};
			}
			CS$<>8__locals1.form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, {13344}, (CS$<>8__locals1.flag != null) ? CS$<>8__locals1.flag.Value.ToStringLocalFull() : "?", PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 13f);
			UiControl form = CS$<>8__locals1.form;
			Marker {13193} = new Marker(0f, 0f, CS$<>8__locals1.form.Pos.WH.X - 6f, 52f);
			Rectangle {13194};
			if (CS$<>8__locals1.flag == null)
			{
				{13194} = new Rectangle(2467, 310, 197, 69);
			}
			else
			{
				OpenWorldFlag value = CS$<>8__locals1.flag.Value;
				GuildCommon guild = Session.Guild;
				{13194} = CommonAtlas.GetWorldFlagPrerender(value, (guild != null) ? guild.Fraction : FractionID.None);
			}
			form.AddChildPos(new Form({13193}, {13194}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			}, PositionAlignment.Center, PositionAlignment.LeftUp, 36f);
			if (CS$<>8__locals1.flag != null)
			{
				{21949}.<>c__DisplayClass6_1 CS$<>8__locals2;
				CS$<>8__locals2.tb = new TextBlockBuilder(Fonts.Arial_10, 1f);
				foreach (string {21963} in {21949}.GetFeatures(CS$<>8__locals1.flag.Value, false))
				{
					CS$<>8__locals1.<CreateFlagCard>g__AppendHelper|4({21963}, new Color(128, 254, 116) * 0.70980394f, ref CS$<>8__locals2);
				}
				foreach (string {21963}2 in {21949}.GetDisadvantages(CS$<>8__locals1.flag.Value))
				{
					CS$<>8__locals1.<CreateFlagCard>g__AppendHelper|4({21963}2, new Color(254, 162, 122) * 0.70980394f, ref CS$<>8__locals2);
				}
				CS$<>8__locals1.form.AddChild(CS$<>8__locals2.tb.CreateCentroid(new Vector2(CS$<>8__locals1.form.Pos.Center.X, 90f)));
				CS$<>8__locals1.form.ToolTip = this.GetToolTip(CS$<>8__locals1.flag.Value, text, {21957});
			}
			if (!string.IsNullOrEmpty(text))
			{
				CS$<>8__locals1.form.Opacity = 0.5f;
				flag = CS$<>8__locals1.flag;
				openWorldFlag = OpenWorldFlag.Peaceful;
				if ((flag.GetValueOrDefault() == openWorldFlag & flag != null) && DonationSystem.ShowPeacefulFlagToolTip)
				{
					CS$<>8__locals1.form.AddChildPos(new Button(Vector2.Zero, {17625}.c_btSlate_mid, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						DisableDepthFocusTest = true,
						Brightness = 5f
					}.SetText(Local.PortRealShopPage_127 + "...", Fonts.Arial_10, Color.White, false).ExClick(delegate(ClickUiEventArgs {21966})
					{
						CS$<>8__locals1.<>4__this.BlockAndClose();
						Global.Game.ScenePort.realShopHandler(null, null);
						{20881}.RedirectToSubscriptionsPage(1);
					}), PositionAlignment.Center, PositionAlignment.RightDown, 5f);
				}
			}
			flag = CS$<>8__locals1.flag;
			openWorldFlag = OpenWorldFlag.Peaceful;
			if ((flag.GetValueOrDefault() == openWorldFlag & flag != null) && Session.Account.PeaceTimeSec != 0.0)
			{
				CS$<>8__locals1.form.AddChildPos(new CheckboxControl(Vector2.Zero, CommonAtlas.vd_checkBox_26px_true, CommonAtlas.vd_checkBox_26px_false, Session.Account.IsPeaceActivated, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.PortRealShopPage_127, Fonts.Arial_10, Color.Wheat).ExCheckEvent(delegate(CheckboxCheckedEventArgs {21967})
				{
					Session.Account.IsPeaceActivated = {21967}.NewValue;
					if ({21967}.NewValue)
					{
						Session.Account.WorldFlag = OpenWorldFlag.Peaceful;
					}
					else
					{
						Session.Game.CheckFlag();
					}
					CS$<>8__locals1.<>4__this.{21950}();
				}), PositionAlignment.Center, PositionAlignment.RightDown, 15f);
			}
			if (CS$<>8__locals1.flag.GetValueOrDefault() == OpenWorldFlag.War && Session.Game.MapMyFraction.GetValueOrDefault() != FractionID.TradeUnion)
			{
				CheckboxControl checkboxControl = new CheckboxControl(Vector2.Zero, CommonAtlas.vd_checkBox_26px_true, CommonAtlas.vd_checkBox_26px_false, Session.Account.TensityMode, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.tensity_mode.Replace(" ", Environment.NewLine), Fonts.Arial_10, Color.Wheat).ExCheckEvent(delegate(CheckboxCheckedEventArgs {21968})
				{
					Session.Account.TensityMode = {21968}.NewValue;
					CS$<>8__locals1.<>4__this.{21950}();
				});
				if (!string.IsNullOrEmpty(text))
				{
					checkboxControl.AllowMouseInput = false;
				}
				checkboxControl.ToolTipState = new ToolTipState("", Local.flag_war_tensityMode, Array.Empty<ToolTipCharacteristics>());
				CS$<>8__locals1.form.AddChildPos(checkboxControl, PositionAlignment.Center, PositionAlignment.RightDown, 15f);
			}
			return CS$<>8__locals1.form;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x000A2558 File Offset: 0x000A0758
		internal static IEnumerable<string> GetFeatures(OpenWorldFlag {21952}, bool {21953} = false)
		{
			{21949}.<GetFeatures>d__7 <GetFeatures>d__ = new {21949}.<GetFeatures>d__7(-2);
			<GetFeatures>d__.<>3__flag = {21952};
			<GetFeatures>d__.<>3__newPlayer = {21953};
			return <GetFeatures>d__;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x000A256F File Offset: 0x000A076F
		internal static IEnumerable<string> GetDisadvantages(OpenWorldFlag {21954})
		{
			{21949}.<GetDisadvantages>d__8 <GetDisadvantages>d__ = new {21949}.<GetDisadvantages>d__8(-2);
			<GetDisadvantages>d__.<>3__flag = {21954};
			return <GetDisadvantages>d__;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000A2580 File Offset: 0x000A0780
		private ToolTip GetToolTip(OpenWorldFlag {21955}, string {21956}, Action<TextBlockBuilder> {21957})
		{
			string openWorldFlagText = CommonEnums.GetOpenWorldFlagText({21955});
			ToolTipState toolTipState = new ToolTipState({21955}.ToStringLocalFull(), openWorldFlagText, Array.Empty<ToolTipCharacteristics>());
			if (!string.IsNullOrEmpty({21956}))
			{
				toolTipState.AppendText("", Color.Orange, true, false);
				toolTipState.AppendText({21956}, Color.Orange, true, false);
			}
			if ({21957} != null)
			{
				{21957}(toolTipState.Builder);
			}
			return new ToolTip(toolTipState);
		}

		// Token: 0x0400115F RID: 4447
		internal static readonly Rectangle c_path = new Rectangle(2408, 40, 523, 160);

		// Token: 0x04001160 RID: 4448
		private StackForm {21958};

		// Token: 0x04001161 RID: 4449
		private Form {21959};

		// Token: 0x04001162 RID: 4450
		private bool {21960};
	}
}
