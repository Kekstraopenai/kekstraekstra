using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001E0 RID: 480
	internal static class {19381}
	{
		// Token: 0x06000ACC RID: 2764 RVA: 0x00055DC0 File Offset: 0x00053FC0
		private static void RunExiting()
		{
			if (Global.Game.GetCurrentSceneName == GameSceneName.Game)
			{
				new {19390}(delegate()
				{
					Session.SafeExitFlags = true;
					Global.Game.ExitFromGame();
				}, 29999f, true);
				return;
			}
			if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
			{
				new {19390}(delegate()
				{
					Session.SafeExitFlags = true;
					Global.Game.ExitFromGame();
				}, 4999f, false);
				return;
			}
			Global.Game.ExitFromGame();
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00055E49 File Offset: 0x00054049
		private static void OpenBonusCodeWindow()
		{
			new {17312}(delegate(string {19386})
			{
				if ({19386}.Length == 0)
				{
					return;
				}
				Action action = delegate()
				{
					new {17312}(Local.bonus_codes_window_wrong_code);
				};
				Global.Network.NetClient.Send(new OnBonusCodeEntered(Session.Account.SID, {19386}));
			}, 30, Local.bonus_codes_window_entry_box, null, null);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00055E7C File Offset: 0x0005407C
		public static void TryShow()
		{
			List<{19086}.Item> list = new List<{19086}.Item>();
			list.Add(new {19086}.Item(new Rectangle(1712, 3220, 64, 64), Local.to_continue, delegate()
			{
			}, null, true, {19086}.ItemStyle.Default, null));
			list.Add(new {19086}.Item(new Rectangle(1777, 3350, 64, 64), Local.EscapeInterface_3, delegate()
			{
				new {19413}();
			}, null, true, {19086}.ItemStyle.Default, null));
			list.Add(new {19086}.Item(new Rectangle(1712, 3415, 64, 64), Local.EscapeInterface_6, delegate()
			{
				Helpers.ExecuteBrowser(PlatformTuning.HelpdeskLink, false);
			}, null, true, {19086}.ItemStyle.Default, null));
			list.Add(new {19086}.Item(new Rectangle(1712, 3480, 64, 64), Local.EscapeInterface_8, delegate()
			{
				Helpers.OpenReferalWebPage();
			}, null, Global.Game.GetCurrentSceneName != GameSceneName.Entry && PlatformTuning.InviteFriendsLinkVisible, {19086}.ItemStyle.Default, null));
			list.Add(new {19086}.Item(new Rectangle(1777, 3415, 64, 64), Local.my_profile, delegate()
			{
				Global.Network.Send(new OnProfileQueryMsg(Session.Account.SID, null));
			}, null, Global.Game.GetCurrentSceneName != GameSceneName.Entry, {19086}.ItemStyle.Default, null));
			List<{19086}.Item> list2 = list;
			Rectangle {19118} = new Rectangle(1777, 3480, 64, 64);
			string escapeInterface_ = Local.EscapeInterface_9;
			Action {19120};
			if (({19120} = {19381}.<>O.<0>__RunExiting) == null)
			{
				{19120} = ({19381}.<>O.<0>__RunExiting = new Action({19381}.RunExiting));
			}
			list2.Add(new {19086}.Item({19118}, escapeInterface_, {19120}, null, true, {19086}.ItemStyle.Default, null));
			list.Add(new {19086}.Item(Rectangle.Empty, string.Empty, null, null, true, {19086}.ItemStyle.Default, null));
			list.Add({19381}.SimpleLink(new Rectangle(1713, 3674, 64, 64), "Discord", Local.launcher_discord_ref, true));
			list.Add({19381}.SimpleLink(new Rectangle(1713, 3739, 64, 64), "VK", Local.launcher_vk_ref, true));
			list.Add({19381}.SimpleLink(new Rectangle(1713, 3869, 64, 64), "Youtube", Local.launcher_youtube_ref, true));
			list.Add({19381}.SimpleLink(new Rectangle(1713, 3934, 64, 64), "Telegram", Local.launcher_telegram_ref, true));
			if (!PlatformTuning.DisableShop)
			{
				list.Add(new {19086}.Item(new Rectangle(1842, 3480, 64, 64), Local.bonus_codes_button, delegate()
				{
					{19381}.OpenBonusCodeWindow();
				}, null, Global.Game.GetCurrentSceneName != GameSceneName.Entry, {19086}.ItemStyle.Link, null));
			}
			{19086} {19086} = new {19086}(list.ToArray());
			UiControl uiControl = {19086}.GetChildren.ElementAt(1);
			UiControl uiControl2 = {19086}.GetChildren.Last();
			Vector2 vector = new Vector2(uiControl.Pos.XY.X + 20f, uiControl2.Pos.XY.Y + 50f);
			Label label = new Label(vector, Fonts.Arial_10, new Color(1f, 1f, 1f), Local.User_agreement, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {19387})
			{
				Helpers.ExecuteBrowser(Local.launcher_rules_ref, false);
			}).SetUnderlineDecoration(CommonAtlas.whitePixel, CommonAtlas.Texture.Tex);
			Label label2 = new Label(vector + new Vector2(0f, label.PosHeight * 1.4f), Fonts.Arial_10, new Color(1f, 1f, 1f), Local.report_a_bug, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {19388})
			{
				Helpers.ExecuteBrowser(PlatformTuning.HelpdeskLink, false);
			}).SetUnderlineDecoration(CommonAtlas.whitePixel, CommonAtlas.Texture.Tex);
			label.Opacity = (label2.Opacity = 0.5f);
			{19086}.AddChild(new UiControl[]
			{
				label,
				label2
			});
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x000562C8 File Offset: 0x000544C8
		private static {19086}.Item SimpleLink(Rectangle {19382}, string {19383}, string {19384}, bool {19385} = true)
		{
			return new {19086}.Item({19382}, {19383}, delegate()
			{
				Helpers.ExecuteBrowser({19384}, false);
			}, null, {19385} && !string.IsNullOrWhiteSpace({19384}), {19086}.ItemStyle.Link, null);
		}

		// Token: 0x020001E1 RID: 481
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040009B1 RID: 2481
			public static Action <0>__RunExiting;
		}
	}
}
