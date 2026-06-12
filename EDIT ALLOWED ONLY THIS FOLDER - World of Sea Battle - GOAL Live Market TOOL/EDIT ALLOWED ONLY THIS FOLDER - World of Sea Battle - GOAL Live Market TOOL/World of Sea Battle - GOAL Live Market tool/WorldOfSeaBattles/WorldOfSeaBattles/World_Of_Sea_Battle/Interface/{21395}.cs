using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using WorldOfSeaBattles;
using WorldOfSeaBattles.Components.Apis;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200031A RID: 794
	internal sealed class {21395} : {17068}
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0009195A File Offset: 0x0008FB5A
		private static int minAmountOfMonets
		{
			get
			{
				if (Global.Settings.PreferredCurrency != Currency.Rub)
				{
					return 100;
				}
				return 40;
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00091970 File Offset: 0x0008FB70
		public static void Open(int {21397} = 0)
		{
			if (PlatformTuning.DisableShop)
			{
				return;
			}
			{21395} currentInstance = {21395}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.RemoveFromContainer();
			}
			({21395}.CurrentInstance = new {21395}({21397})).EvRemoveFromContainer += delegate()
			{
				{21395}.CurrentInstance = null;
			};
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000919C8 File Offset: 0x0008FBC8
		private {21395}(int {21398} = 0)
		{
			{21395}.<>c__DisplayClass10_0 CS$<>8__locals1 = new {21395}.<>c__DisplayClass10_0();
			CS$<>8__locals1.initialValueIfNotEnough = {21398};
			Rectangle uiarea = Engine.GS.UIArea;
			base..ctor(Marker.FromCentrScreen(new Marker(ref uiarea), new Marker(0f, 0f, (float){18807}.main.Width * 2.5f, (float){18807}.main.Height * 2.5f).ToRect()), {18807}.main, {17068}.BlockingWay.BackgroundClosing, false);
			CS$<>8__locals1.<>4__this = this;
			{21395}.<>c__DisplayClass10_1 CS$<>8__locals2 = new {21395}.<>c__DisplayClass10_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			Global.Network.Send(new OnAnalyticsEventClient(OnAnalyticsEventClient.Type.OpenMonetsRefil, string.Empty, 1));
			if (CS$<>8__locals2.CS$<>8__locals1.initialValueIfNotEnough > 0)
			{
				CS$<>8__locals2.CS$<>8__locals1.initialValueIfNotEnough = Math.Max(CS$<>8__locals2.CS$<>8__locals1.initialValueIfNotEnough, {21395}.minAmountOfMonets);
			}
			base.AddChildPos(new Form(new Marker(0f, 0f, 470f, 45f), {21395}.c_backGradient, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			}, PositionAlignment.Center, PositionAlignment.LeftUp, 249f);
			base.AddChildPos(new Form(new Marker(0f, 0f, 470f, 50f), {21395}.c_backGradient, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			}, PositionAlignment.Center, PositionAlignment.LeftUp, 407f);
			base.AddChild(new Label(new Vector2(base.Pos.Center.X, base.Pos.XY.Y + 228f), Fonts.Philosopher_18, Color.White * 0.9f, Local.PortRealShopPage_64_2, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			if (CS$<>8__locals2.CS$<>8__locals1.initialValueIfNotEnough > 0)
			{
				base.AddChild(new Label(new Vector2(base.Pos.Center.X, base.Pos.XY.Y + 228f - 25f), Fonts.Philosopher_14, Color.White * 0.7f, "(" + Local.PortInputMonetsToBuyWindow_2 + ")", PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			}
			CS$<>8__locals2.textBox = new TextBox(new Marker(base.Pos.Center.X - (float)({18807}.c_undoButton.Width / 2), base.Pos.XY.Y + 255f, 189f, 32f), {18807}.c_undoButton, Fonts.Philosopher_16, Color.White, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals2.textBox.DefaultText = Local.quanity;
			CS$<>8__locals2.textBox.AttachMaxLengthModerator(6, null, Color.Transparent);
			if (CS$<>8__locals2.CS$<>8__locals1.initialValueIfNotEnough > 0)
			{
				CS$<>8__locals2.textBox.Text = CS$<>8__locals2.CS$<>8__locals1.initialValueIfNotEnough.ToString();
			}
			new UiActionsSleep(this, 100f);
			new UiActor(this, delegate()
			{
				CS$<>8__locals2.textBox.IsEnter = true;
			});
			CS$<>8__locals2.continueButton = new Button(new Vector2(base.Pos.Center.X - (float)({18807}.c_undoButton.Width / 2) + 62f, base.Pos.XY.Y + 415f), {18807}.c_undoButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals2.continueButton.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(null, Local.PortInputMonetsToBuyWindow_3, Array.Empty<ToolTipCharacteristics>()));
			CS$<>8__locals2.copyLinkBtPath = new Rectangle(785, 2543, 37, 35);
			CS$<>8__locals2.copyLinkButton = new Button(new Vector2(CS$<>8__locals2.continueButton.Pos.End.X + 11f, CS$<>8__locals2.continueButton.Pos.XY.Y), CS$<>8__locals2.copyLinkBtPath, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals2.copyLinkButton.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(null, Local.PortInputMonetsToBuyWindow_linkCopy, Array.Empty<ToolTipCharacteristics>()));
			CS$<>8__locals2.copyLinkButton.EvClick += delegate(ClickUiEventArgs {21408})
			{
				{21395}.<>c__DisplayClass10_1.<<-ctor>b__1>d <<-ctor>b__1>d;
				<<-ctor>b__1>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<-ctor>b__1>d.<>4__this = CS$<>8__locals2;
				<<-ctor>b__1>d.<>1__state = -1;
				<<-ctor>b__1>d.<>t__builder.Start<{21395}.<>c__DisplayClass10_1.<<-ctor>b__1>d>(ref <<-ctor>b__1>d);
			};
			CS$<>8__locals2.textBox.EvTextChanged += delegate(string {21409})
			{
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.WaitingMode = (CS$<>8__locals2.CS$<>8__locals1.<>4__this.WaitingProcessingMode = false);
				CS$<>8__locals2.copyLinkButton.TexturePath = CS$<>8__locals2.copyLinkBtPath;
			};
			CS$<>8__locals2.availableRegions = PlatformTuning.AvailablePayRegions.ToArray<PlatformTuning.PayRegion>();
			CS$<>8__locals2.currentRegion = Global.Settings.PreferredPayRegion;
			if (string.IsNullOrEmpty(CS$<>8__locals2.currentRegion) || !CS$<>8__locals2.availableRegions.Any((PlatformTuning.PayRegion {21410}) => {21410}.payRegion == CS$<>8__locals2.currentRegion))
			{
				CS$<>8__locals2.currentRegion = Local.select;
				Global.Settings.PreferredCurrency = PlatformTuning.MatchDefaultRegion.payInCurrency;
			}
			CS$<>8__locals2.countrySelector = new Label(base.Pos.XY + new Vector2(230f, 419f), Fonts.Philosopher_14, Color.Wheat, Local.region + ": " + CS$<>8__locals2.currentRegion, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals2.countrySelector.SetUnderlineDecoration(CommonAtlas.whitePixel, CommonAtlas.Texture.Tex);
			CS$<>8__locals2.countrySelector.EvClick += delegate(ClickUiEventArgs {21411})
			{
				Action<object> {17476};
				if (({17476} = CS$<>8__locals2.<>9__11) == null)
				{
					{17476} = (CS$<>8__locals2.<>9__11 = delegate(object {21412})
					{
						PlatformTuning.PayRegion payRegion = PlatformTuning.AllPayRegions.First((PlatformTuning.PayRegion {21421}) => {21421}.payRegion == {21412}.ToString());
						Global.Settings.PreferredCurrency = payRegion.payInCurrency;
						Global.Settings.PreferredPayRegion = payRegion.payRegion;
						CS$<>8__locals2.countrySelector.Text = Local.region + ": " + Global.Settings.PreferredPayRegion;
						CS$<>8__locals2.CS$<>8__locals1.<>4__this.WaitingMode = (CS$<>8__locals2.CS$<>8__locals1.<>4__this.WaitingProcessingMode = false);
						base.<.ctor>g__UpdateText|5();
					});
				}
				IEnumerable<PlatformTuning.PayRegion> allPayRegions = PlatformTuning.AllPayRegions;
				Func<PlatformTuning.PayRegion, {17473}.Item> selector;
				if ((selector = CS$<>8__locals2.<>9__12) == null)
				{
					selector = (CS$<>8__locals2.<>9__12 = delegate(PlatformTuning.PayRegion {21413})
					{
						if (CS$<>8__locals2.availableRegions.Any((PlatformTuning.PayRegion {21422}) => {21422}.payRegion == {21413}.payRegion))
						{
							return new {17473}.Item({21413}.payRegion, {21413}.payRegion, true, default(ImageDecription), null, null);
						}
						string value = PlatformTuning.GetPlatform().ToString();
						object payRegion = {21413}.payRegion;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(4, 3);
						defaultInterpolatedStringHandler4.AppendFormatted({21413}.payRegion);
						defaultInterpolatedStringHandler4.AppendLiteral(" (");
						defaultInterpolatedStringHandler4.AppendFormatted(Local.not_available);
						defaultInterpolatedStringHandler4.AppendLiteral(" ");
						defaultInterpolatedStringHandler4.AppendFormatted(value);
						defaultInterpolatedStringHandler4.AppendLiteral(")");
						return new {17473}.Item(payRegion, defaultInterpolatedStringHandler4.ToStringAndClear(), true, default(ImageDecription), null, null)
						{
							Enable = false
						};
					});
				}
				new {17473}({17476}, allPayRegions.Select(selector).Where(delegate({17473}.Item {21404})
				{
					bool isActive = Steam.IsActive;
					return {21404}.Enable;
				}).ToArray<{17473}.Item>());
			};
			base.AddChild(CS$<>8__locals2.countrySelector);
			CS$<>8__locals2.<.ctor>g__UpdateText|5();
			LiveLabel liveLabel = new LiveLabel(new Vector2(CS$<>8__locals2.textBox.Pos.End.X + 5f, CS$<>8__locals2.textBox.Pos.XY.Y + 4f), Fonts.Philosopher_16, Color.White * 0.7f, delegate(LiveLabel {21414})
			{
				base.<.ctor>g__UpdateText|5();
				int num5;
				if (CS$<>8__locals2.textBox.Text.Length > 0 && (!int.TryParse(CS$<>8__locals2.textBox.Text, out num5) || num5 < {21395}.minAmountOfMonets))
				{
					{21414}.BasicColor = Color.Orange * 0.6f;
					return Local.PortInputMonetsToBuyWindow_4({21395}.minAmountOfMonets);
				}
				if (MonetsPackageBonus.GetExtraMonetsAmount(CS$<>8__locals2.textBox.ParseInt, Session.Account) > 0)
				{
					{21414}.BasicColor = Color.White * 0.5f;
					return Local.monets_bonus_2(MonetsPackageBonus.GetExtraMonetsForPayroll(CS$<>8__locals2.textBox.ParseInt, Session.Account));
				}
				return string.Empty;
			}, 25);
			CS$<>8__locals2.continueButton.EvClick += delegate(ClickUiEventArgs {21415})
			{
				int num5;
				if (!int.TryParse(CS$<>8__locals2.textBox.Text, out num5) || num5 < {21395}.minAmountOfMonets)
				{
					return;
				}
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.{21399}(num5);
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.WaitingMode = true;
				base.<.ctor>g__UpdateText|5();
			};
			CS$<>8__locals2.continueButton.UpdateComplete += delegate(UiControl {21416})
			{
				if (InputHelper.IsClick(Keys.Enter) && CS$<>8__locals2.continueButton.AllowMouseInput)
				{
					{21416}.ImitateClick(false);
				}
			};
			base.EvClick += delegate(ClickUiEventArgs {21406})
			{
				Marker marker = new Marker(73f, 97f, 193f, 91f).Border(30f).Scale(2.5f).Offset(CS$<>8__locals2.CS$<>8__locals1.<>4__this.Pos.XY);
				Vector2 mouseToUI = Engine.GS.MouseToUI;
				if (!marker.Collision(mouseToUI))
				{
					CS$<>8__locals2.CS$<>8__locals1.<>4__this.BlockAndClose();
				}
			};
			int[] array = DonationSystem.QuickRefilPrices;
			bool flag = array.Any((int {21405}) => {21405} >= 10000);
			if (CS$<>8__locals2.CS$<>8__locals1.initialValueIfNotEnough != 0)
			{
				int[] source = new int[]
				{
					100,
					500,
					1000,
					1500,
					2000,
					3000,
					4000,
					5000,
					6000,
					7000,
					10000,
					15000
				};
				int firstButton = source.FirstOrDefault((int {21407}) => {21407} > CS$<>8__locals2.CS$<>8__locals1.initialValueIfNotEnough);
				int num = source.FirstOrDefault((int {21423}) => {21423} > firstButton);
				array = new int[]
				{
					firstButton,
					num
				};
			}
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			int num2 = 0;
			int[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				int item = array2[i];
				if (item != 0)
				{
					int num3 = 92;
					float num4 = 0.5f;
					Rectangle rectangle = this.{21402}[num2++];
					Form form = new Form(new Marker(0f, 0f, (float)(num3 + ((array.Length == 2) ? 20 : 0)), (float)(num3 + 15)), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					};
					form.AddChildPos(new Form(new Marker(0f, 0f, (float)rectangle.Width * num4, (float)rectangle.Height * num4), rectangle, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					}, PositionAlignment.Center, PositionAlignment.LeftUp, 0f);
					TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_14, 0f);
					TextBlockBuilder textBlockBuilder2 = textBlockBuilder;
					string {13555};
					if (!flag)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
						defaultInterpolatedStringHandler.AppendLiteral("  ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(item);
						defaultInterpolatedStringHandler.AppendLiteral(" M");
						{13555} = defaultInterpolatedStringHandler.ToStringAndClear();
					}
					else
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(2, 1);
						defaultInterpolatedStringHandler2.AppendLiteral("  ");
						defaultInterpolatedStringHandler2.AppendFormatted<int>(item);
						{13555} = defaultInterpolatedStringHandler2.ToStringAndClear();
					}
					textBlockBuilder2.Write({13555}, Color.White * 0.5f);
					int extraMonetsAmount = MonetsPackageBonus.GetExtraMonetsAmount(item, Session.Account);
					if (extraMonetsAmount > 0)
					{
						TextBlockBuilder textBlockBuilder3 = textBlockBuilder;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(3, 1);
						defaultInterpolatedStringHandler3.AppendLiteral(" +");
						defaultInterpolatedStringHandler3.AppendFormatted<int>(extraMonetsAmount);
						defaultInterpolatedStringHandler3.AppendLiteral("%");
						textBlockBuilder3.Write(defaultInterpolatedStringHandler3.ToStringAndClear(), new Color(207, 217, 112));
						form.ToolTipState = new ToolTipState("", Local.monets_bonus_tt(item, extraMonetsAmount), Array.Empty<ToolTipCharacteristics>());
					}
					form.AddChildPos(textBlockBuilder.Create(Vector2.Zero), PositionAlignment.Center, PositionAlignment.LeftUp, (float)rectangle.Width * num4);
					form.AnimatedFocus = false;
					form.UpdateComplete += delegate(UiControl {21424})
					{
						{21424}.Opacity = (({21424}.InputMode == MouseInputMode.Focused || CS$<>8__locals2.textBox.ParseInt >= item) ? 1f : 0.7f);
					};
					form.EvClick += delegate(ClickUiEventArgs {21425})
					{
						CS$<>8__locals2.textBox.Text = item.ToString();
					};
					stackForm.AddItem(new UiControl[]
					{
						form
					});
				}
			}
			base.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.LeftUp, 312f);
			base.AddChild(new UiControl[]
			{
				liveLabel,
				CS$<>8__locals2.textBox,
				CS$<>8__locals2.continueButton,
				CS$<>8__locals2.copyLinkButton
			});
			if (Session.Account.CreatedByStandalone && Steam.IsActive)
			{
				base.AddChildPos(TextBlockBuilder.CreateBlock(400f, Local.steam_tax_disclaimer, Color.LightGray, Fonts.Philosopher_14, -2f).CreateCentroid(), PositionAlignment.Center, PositionAlignment.LeftUp, 470f);
			}
			foreach (UiControl uiControl in ((IEnumerable<UiControl>)base.GetChildren))
			{
				uiControl.Brightness = 1.4f;
			}
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x000923EC File Offset: 0x000905EC
		private void {21399}(int {21400})
		{
			PaymentRequest.ProcessPaymentUseSettings({21400});
			Global.Network.Send(new OnAnalyticsEventClient(OnAnalyticsEventClient.Type.OpenMonetsWebPayment, string.Empty, 1));
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserUpdate(ref FrameTime {21401})
		{
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0009240F File Offset: 0x0009060F
		protected override void UserBackRender()
		{
			this.{21403} = (Engine.GS.CurrentTexture != CommonAtlas.Texture.Tex);
			if (this.{21403})
			{
				Engine.GS.SetTexture(CommonAtlas.Texture);
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00092447 File Offset: 0x00090647
		protected override void UserFrontRender()
		{
			if (this.{21403})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x04000FD1 RID: 4049
		public static {21395} CurrentInstance;

		// Token: 0x04000FD2 RID: 4050
		private static readonly Rectangle c_toolList = new Rectangle(764, 2849, 330, 38);

		// Token: 0x04000FD3 RID: 4051
		private static readonly Rectangle c_toolListItem = new Rectangle(764, 2888, 305, 29);

		// Token: 0x04000FD4 RID: 4052
		private static readonly Rectangle c_backGradient = new Rectangle(1049, 2449, 227, 22);

		// Token: 0x04000FD5 RID: 4053
		private Rectangle[] {21402} = new Rectangle[]
		{
			new Rectangle(0, 1453, 128, 128),
			new Rectangle(129, 1453, 128, 128),
			new Rectangle(258, 1453, 128, 128),
			new Rectangle(0, 1582, 128, 128)
		};

		// Token: 0x04000FD6 RID: 4054
		public bool WaitingMode;

		// Token: 0x04000FD7 RID: 4055
		public bool WaitingProcessingMode;

		// Token: 0x04000FD8 RID: 4056
		private bool {21403};
	}
}
