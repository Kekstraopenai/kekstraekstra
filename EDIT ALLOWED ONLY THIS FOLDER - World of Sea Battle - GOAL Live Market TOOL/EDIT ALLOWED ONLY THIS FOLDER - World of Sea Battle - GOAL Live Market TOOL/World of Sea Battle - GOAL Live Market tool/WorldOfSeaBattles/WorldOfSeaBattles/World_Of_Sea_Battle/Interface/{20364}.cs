using System;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000274 RID: 628
	internal sealed class {20364} : {17625}
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0007586D File Offset: 0x00073A6D
		public static int ContentWidth
		{
			get
			{
				return 820;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00075874 File Offset: 0x00073A74
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x0007587C File Offset: 0x00073A7C
		public GuildQuests QuestsCache { get; private set; }

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00075888 File Offset: 0x00073A88
		public {20364}()
		{
			float {17636} = 840f;
			Rectangle c_back = {17625}.c_back1;
			{17604} inGameWindow = {17604}.InGameWindow;
			Rectangle c_icShield = {17625}.c_icShield;
			{17625}.DynamicTittle[] array = new {17625}.DynamicTittle[6];
			array[0] = (Session.Guild.IsFlotilia ? Local.flotilia : Local.guild);
			array[1] = new {17625}.DynamicTittle(delegate()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted(Local.GuildWindow_9);
				defaultInterpolatedStringHandler.AppendLiteral(" (");
				defaultInterpolatedStringHandler.AppendFormatted<int>(Session.Guild.Members.Count((GuildMember {20375}) => {20375}.Cached.IsOnline));
				defaultInterpolatedStringHandler.AppendLiteral(")");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			});
			array[2] = new {17625}.DynamicTittle(delegate()
			{
				if (Session.Guild.Invites.Size != 0)
				{
					return Local.GuildWindow_9b + " (" + Session.Guild.Invites.Size.ToString() + ")";
				}
				return Local.GuildWindow_9b;
			});
			array[3] = Local.GuildWindow_10;
			array[4] = Local.GuildWindow_81;
			array[5] = Local.guild_quests;
			base..ctor({17636}, c_back, inGameWindow, c_icShield, array);
			{20364}.CurrentInstance = this;
			this.AnimatedFocus = false;
			GameScene.IncreaseGameInput();
			this.Reload();
			base.EvRemoveFromContainer += delegate()
			{
				GameScene.DecreaseGameInput();
				{20364}.CurrentInstance = null;
			};
			if (Global.Player.IsPortEntry)
			{
				Global.Game.ScenePort.MakeAccSync();
			}
			Global.Network.Send(new OnGuildWindowRequest(CapturePortsRistrictionType.None, 0f, new Tlist<GuildMemberCacheUpdate>(), null, 0));
			{17177} currentInstance = {17177}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.RemoveFromContainer();
			}
			EducationHelper.MakeFlag(EducationOnboarding.OpenGuildAndFractions, true);
			EducationHelper.MakeFlag(EducationOnboarding.BeInGuild, false);
			if (Global.Settings.SavedGuildAnnouncmentVersion != Session.Guild.CurrentAnnouncment.Item2)
			{
				Global.Settings.SavedGuildAnnouncmentVersion = Session.Guild.CurrentAnnouncment.Item2;
				if (!string.IsNullOrEmpty(Session.Guild.CurrentAnnouncment.Item1))
				{
					new {17107}(Local.guild_announcment, Session.Guild.CurrentAnnouncment.Item1, "", delegate(int {20376})
					{
					}, true, null, new string[]
					{
						Local.close
					});
				}
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00075A94 File Offset: 0x00073C94
		public void Reload()
		{
			if (this.RefreshCurrentDynamicTabPage == null)
			{
				Action<StackForm> {17653} = null;
				bool {17654} = true;
				bool {17655} = true;
				Action<ListItemViewControl>[] array = new Action<ListItemViewControl>[6];
				array[0] = new Action<ListItemViewControl>(this.{20368});
				array[1] = delegate(ListItemViewControl {20377})
				{
					{20377}.AddItem(new UiControl[]
					{
						new {20293}()
					});
				};
				array[2] = delegate(ListItemViewControl {20378})
				{
					{20378}.AddItem(new UiControl[]
					{
						new {20283}()
					});
				};
				array[3] = delegate(ListItemViewControl {20379})
				{
					{20379}.AddItem(new UiControl[]
					{
						new {20312}()
					});
				};
				array[4] = delegate(ListItemViewControl {20380})
				{
					{20380}.AddItem(new UiControl[]
					{
						new {20285}()
					});
				};
				array[5] = new Action<ListItemViewControl>(this.{20370});
				base.ComposeTabWithScroll({17653}, {17654}, {17655}, array);
				this.UpdateImage();
				return;
			}
			Action refreshCurrentDynamicTabPage = this.RefreshCurrentDynamicTabPage;
			if (refreshCurrentDynamicTabPage == null)
			{
				return;
			}
			refreshCurrentDynamicTabPage();
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00075B75 File Offset: 0x00073D75
		public void UpdateQuests(GuildQuests {20366})
		{
			this.QuestsCache = {20366};
			Session.Guild.CachedSupplyMissionCount = Geometry.SaturateByte({20366}[GuildQuestProgression.SupplyMission].Item2);
			if (this.selectedTitle == 6)
			{
				base.ForceItemSelected(6);
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00075BAA File Offset: 0x00073DAA
		public void UpdateImage()
		{
			{20217} {20217} = this.{20373};
			if ({20217} == null)
			{
				return;
			}
			{20217}.UpdateImage();
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00075BBC File Offset: 0x00073DBC
		protected override void UserBackRender()
		{
			base.UserBackRender();
			if (!Session.Guild.IsFlotilia)
			{
				Device gs = Engine.GS;
				Rectangle fractionFlagPrerender = CommonAtlas.GetFractionFlagPrerender(Session.Guild.Fraction);
				Rectangle rectangle = base.Pos.Offset(0f, 5f).ScaleSize(0.5f).ToRect();
				Color color = Color.White * 0.1f;
				gs.Draw(fractionFlagPrerender, rectangle, color);
			}
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00075C39 File Offset: 0x00073E39
		protected override void UserUpdate(ref FrameTime {20367})
		{
			base.UserUpdate(ref {20367});
			Global.Settings.SavedGuildVersion = Session.Guild.Version;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00075D14 File Offset: 0x00073F14
		[CompilerGenerated]
		private void {20368}(ListItemViewControl {20369})
		{
			{20369}.AddItem(new UiControl[]
			{
				this.{20373} = new {20217}()
			});
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00075D40 File Offset: 0x00073F40
		[CompilerGenerated]
		private void {20370}(ListItemViewControl {20371})
		{
			{20371}.AddItem(new UiControl[]
			{
				this.{20374} = new {20363}()
			});
		}

		// Token: 0x04000D0F RID: 3343
		public static {20364} CurrentInstance;

		// Token: 0x04000D10 RID: 3344
		public static readonly Rectangle c_item2 = new Rectangle(976, 3795, 405, 58);

		// Token: 0x04000D11 RID: 3345
		public static readonly Rectangle c_item4 = new Rectangle(1993, 2305, 300, 35);

		// Token: 0x04000D12 RID: 3346
		public static readonly Rectangle c_btRemove = new Rectangle(2310, 2306, 35, 35);

		// Token: 0x04000D13 RID: 3347
		public static readonly Rectangle c_btWait = new Rectangle(2382, 2306, 35, 35);

		// Token: 0x04000D14 RID: 3348
		public static readonly Rectangle c_btAccept = new Rectangle(2346, 2306, 35, 35);

		// Token: 0x04000D15 RID: 3349
		private static readonly Rectangle c_hqIcon = new Rectangle(548, 72, 54, 78);

		// Token: 0x04000D16 RID: 3350
		private static readonly Rectangle c_item_i = new Rectangle(976, 3854, 405, 58);

		// Token: 0x04000D17 RID: 3351
		[CompilerGenerated]
		private GuildQuests {20372};

		// Token: 0x04000D18 RID: 3352
		private {20217} {20373};

		// Token: 0x04000D19 RID: 3353
		private {20363} {20374};
	}
}
