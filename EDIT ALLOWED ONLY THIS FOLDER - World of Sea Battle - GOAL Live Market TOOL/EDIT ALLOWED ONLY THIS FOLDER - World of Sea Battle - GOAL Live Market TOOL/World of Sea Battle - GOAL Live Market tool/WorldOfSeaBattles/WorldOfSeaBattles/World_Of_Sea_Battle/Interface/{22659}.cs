using System;
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
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020003D9 RID: 985
	internal sealed class {22659} : {17625}
	{
		// Token: 0x06001584 RID: 5508 RVA: 0x000B5548 File Offset: 0x000B3748
		public {22659}() : base(470f, {17625}.c_back3, {17604}.InGameWindow, {17625}.c_icBook, Array.Empty<{17625}.DynamicTittle>())
		{
			base.PosHeight *= 1.2f;
			base.PosWidth *= 1.2f;
			base.AddChildPos(new Form(new Marker(0f, 0f, 508f, 103f), CommonAtlas.topVensil, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			}, PositionAlignment.Center, PositionAlignment.LeftUp, -25f);
			{22659}.CurrentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{22659}.CurrentInstance = null;
			};
			this.{22671} = new Form(base.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			base.AddChild(this.{22671});
			this.{22660}();
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x000B562C File Offset: 0x000B382C
		private void {22660}()
		{
			{22659}.<>c__DisplayClass3_0 CS$<>8__locals1 = new {22659}.<>c__DisplayClass3_0();
			CS$<>8__locals1.<>4__this = this;
			this.{22671}.ClearAllChild();
			this.{22666}(Local.ingame_poll_header);
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.BorderThickness = -1f;
			CS$<>8__locals1.picked = -1;
			for (int i = 0; i < 10; i++)
			{
				{22659}.<>c__DisplayClass3_1 CS$<>8__locals2 = new {22659}.<>c__DisplayClass3_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.n = i + 1;
				{22659}.<>c__DisplayClass3_1 CS$<>8__locals3 = CS$<>8__locals2;
				Vector2 zero = Vector2.Zero;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
				defaultInterpolatedStringHandler.AppendLiteral("  ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals2.n);
				defaultInterpolatedStringHandler.AppendLiteral("  ");
				CS$<>8__locals3.label = new LabelButton(zero, defaultInterpolatedStringHandler.ToStringAndClear(), Fonts.Philosopher_18, Color.Gray, Color.Gold, null);
				CS$<>8__locals2.label.UpdateComplete += delegate(UiControl {22672})
				{
					CS$<>8__locals2.label.DefaultColor = ((CS$<>8__locals2.CS$<>8__locals1.picked == CS$<>8__locals2.n) ? Color.Gold : Color.Gray);
				};
				CS$<>8__locals2.label.EvClick += delegate(ClickUiEventArgs {22673})
				{
					CS$<>8__locals2.CS$<>8__locals1.picked = CS$<>8__locals2.n;
				};
				Form form = new Form(Vector2.Zero, new Rectangle(907, 44, 44, 44), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form.Pos = form.Pos.Scale(1.1f);
				form.BasicColor = Color.Black;
				form.AddChildPos(CS$<>8__locals2.label, PositionAlignment.Center, PositionAlignment.Center, 0f);
				stackForm.AddItem(new UiControl[]
				{
					form
				});
			}
			this.{22671}.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.LeftUp, 150f);
			this.{22668}(delegate
			{
				CS$<>8__locals1.<>4__this.{22661}(CS$<>8__locals1.picked);
			}, Local.to_continue);
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x000B57C0 File Offset: 0x000B39C0
		private void {22661}(int {22662})
		{
			this.{22671}.ClearAllChild();
			Global.Network.Send(new OnAnalyticsEventClient(OnAnalyticsEventClient.Type.Other, "rating_" + {22662}.ToString(), 1));
			this.{22671}.AddChildPos(new Form(new Marker(0f, 0f, 660f, 63f).Scale(0.58f), new Rectangle(566, 4033, 660, 63), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false,
				BasicColor = new Color(130, 140, 157)
			}, PositionAlignment.Center, PositionAlignment.LeftUp, 20f);
			this.{22671}.AddChildPos(new Form(new Marker(0f, 0f, 660f, 63f).Scale(0.58f), new Rectangle(149, 797, 660, 63), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false,
				BasicColor = new Color(130, 140, 157)
			}, PositionAlignment.Center, PositionAlignment.LeftUp, 80f);
			this.{22666}(Local.ingame_poll_thx);
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.VerticalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if ({22662} > 3)
			{
				if ({22662} <= 6)
				{
					StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					if (LocaleInfo.Current.Id == Locale.Ru)
					{
						stackForm2.AddItem(new UiControl[]
						{
							this.{22663}("VK", Local.launcher_vk_ref)
						});
						stackForm2.AddItem(new UiControl[]
						{
							this.{22663}("Telegram", Local.launcher_telegram_ref)
						});
					}
					else
					{
						stackForm2.AddItem(new UiControl[]
						{
							this.{22663}("Discord", Local.launcher_discord_ref)
						});
						stackForm2.AddItem(new UiControl[]
						{
							this.{22663}("Youtube", Local.launcher_youtube_ref)
						});
					}
					if (stackForm2.CountChild() > 0)
					{
						stackForm.AddItem(new UiControl[]
						{
							new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat, Local.ingame_poll_smm, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
					}
					stackForm.AddItem(new UiControl[]
					{
						stackForm2
					});
				}
				else
				{
					TextBlockBuilder textBlockBuilder = TextBlockBuilder.CreateBlock(520f, Local.IngamePollWindow_support_us_1, Color.Gray, Fonts.Philosopher_14, -3f);
					TextBlockBuilder textBlockBuilder2 = TextBlockBuilder.CreateBlock(400f, Local.IngamePollWindow_support_us_2, new Color(155, 127, 104), Fonts.Philosopher_14, -3f);
					TextBlockControl textBlockControl = textBlockBuilder.CreateCentroid();
					stackForm.AddItem(new UiControl[]
					{
						textBlockControl
					});
					TextBlockControl textBlockControl2 = textBlockBuilder2.CreateCentroid();
					stackForm.AddItem(new UiControl[]
					{
						textBlockControl2
					});
					StackForm stackForm3 = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					PlatformType platform = PlatformTuning.GetPlatform();
					bool flag = platform == PlatformType.VKPlay || (platform != PlatformType.Steam && LocaleInfo.Current.Id == Locale.Ru);
					bool flag2 = platform == PlatformType.Standalone || platform - PlatformType.Steam <= 1;
					bool flag3 = flag2;
					flag2 = (platform == PlatformType.Standalone || platform == PlatformType.MSStore);
					bool flag4 = flag2;
					if (flag)
					{
						stackForm3.AddItem(new UiControl[]
						{
							this.{22663}(Local.ingame_poll_vkp, Local.vkplay_ref)
						});
					}
					if (flag3)
					{
						stackForm3.AddItem(new UiControl[]
						{
							this.{22663}(Local.ingame_poll_steam, Local.steam_ref)
						});
					}
					if (flag4)
					{
						stackForm3.AddItem(new UiControl[]
						{
							this.{22663}(Local.ingame_poll_msstore, Local.msstore_ref)
						});
					}
					stackForm.AddSpace(10f);
					stackForm.AddItem(new UiControl[]
					{
						stackForm3
					});
				}
			}
			this.{22671}.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.LeftUp, 120f);
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x000B5B68 File Offset: 0x000B3D68
		private Button {22663}(string {22664}, string {22665})
		{
			Button button = new Button(Vector2.Zero, new Rectangle(0, 4006, 562, 84), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button.Pos = button.Pos.Scale(0.6f).ScaleWidth(1.08f);
			button.SetText({22664}, Fonts.Philosopher_14, Color.Black, false);
			button.EvClick += delegate(ClickUiEventArgs {22674})
			{
				Helpers.ExecuteBrowser({22665}, false);
				Global.Network.Send(new OnAnalyticsEventClient(OnAnalyticsEventClient.Type.Other, "rating_ref_click", 1));
			};
			return button;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x000B5BEC File Offset: 0x000B3DEC
		private void {22666}(string {22667})
		{
			this.{22671}.AddChild(TextBlockBuilder.CreateBlock(400f, {22667}, Color.Wheat * 0.8f, Fonts.Philosopher_16, 0f).CreateCentroid(this.{22671}.Pos.Center.X, this.{22671}.Pos.XY.Y + 55f));
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000B5C60 File Offset: 0x000B3E60
		private void {22668}(Action {22669}, string {22670})
		{
			Button button = new Button(Vector2.Zero, {17625}.c_btGray_big, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button.Pos = button.Pos.ScaleWidth(1.4f);
			button.SetText({22670}, Fonts.Philosopher_14, Color.Wheat, false);
			this.{22671}.AddChildPos(button, PositionAlignment.Center, PositionAlignment.LeftUp, 300f);
			button.EvClick += delegate(ClickUiEventArgs {22675})
			{
				{22669}();
			};
		}

		// Token: 0x04001377 RID: 4983
		public static {22659} CurrentInstance;

		// Token: 0x04001378 RID: 4984
		private Form {22671};
	}
}
