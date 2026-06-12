using System;
using System.Linq;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000260 RID: 608
	internal class {20285} : StackForm
	{
		// Token: 0x06000D9A RID: 3482 RVA: 0x00072B38 File Offset: 0x00070D38
		public {20285}() : base(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			if (!Session.MyMemberInGuild.Rights.AllowWatchMembersPositions)
			{
				base.AddItem(new UiControl[]
				{
					new Label(base.Pos.XY, Fonts.Philosopher_14, Color.White * 0.7f, Local.GuildWindow_3(Local.guildright_tt_AllowWatchMembersPositions), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				return;
			}
			this.{20286}();
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00072BAC File Offset: 0x00070DAC
		private void {20286}()
		{
			base.Clear();
			TextBox searchBox = new TextBox(Vector2.Zero, {20285}.c_searchBox, Fonts.Philosopher_16, Color.Wheat, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				DisableDepthFocusTest = true,
				DefaultText = Local.search,
				Text = (this.{20290} ?? string.Empty)
			};
			searchBox.AttachMaxLengthModerator(79, null, Color.Transparent);
			searchBox.EvTextChanged += delegate(string {20291})
			{
				if ({20291} == this.{20290})
				{
					return;
				}
				this.{20290} = {20291};
				this.{20286}();
			};
			if (this.{20290} != null)
			{
				searchBox.IsEnter = true;
				new UiActor(searchBox, delegate()
				{
					searchBox.IsEnter = true;
				});
			}
			base.AddItem(new UiControl[]
			{
				searchBox
			});
			foreach (GuildActionLogRecord guildActionLogRecord in Session.Guild.Log.Reverse<GuildActionLogRecord>())
			{
				string text = guildActionLogRecord.ToString();
				if (!string.IsNullOrEmpty(this.{20290}) && !text.Contains(this.{20290}, StringComparison.CurrentCultureIgnoreCase))
				{
					GuildMember member = Session.Guild.GetMember(guildActionLogRecord.SID);
					if (member == null || !member.Cached.Name.Contains(this.{20290}, StringComparison.CurrentCultureIgnoreCase))
					{
						continue;
					}
				}
				Form form = new Form(new Marker(0f, 0f, base.Pos.WH.X, 30f), {20364}.c_item2, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				if (guildActionLogRecord.SID == 0U)
				{
					{20285}.WriteLogLineTo(guildActionLogRecord, stackForm, new string[]
					{
						text
					});
				}
				else
				{
					string[] {20289} = guildActionLogRecord.ToString().Split(new string[]
					{
						"%"
					}, StringSplitOptions.None);
					{20285}.WriteLogLineTo(guildActionLogRecord, stackForm, {20289});
				}
				form.AddChildPos(stackForm, PositionAlignment.LeftUp, PositionAlignment.Center, 5f);
				base.AddItem(new UiControl[]
				{
					form
				});
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00072DEC File Offset: 0x00070FEC
		private static void WriteLogLineTo(in GuildActionLogRecord {20287}, StackForm {20288}, string[] {20289})
		{
			Label label = new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat * 0.9f, {20289}[0], PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{20288}.AddItem(new UiControl[]
			{
				label
			});
			if ({20289}.Length > 1)
			{
				GuildMember member = Session.Guild.GetMember({20287}.SID);
				string {13345} = (member == null) ? Local.guildLog_removedMember : member.Cached.Name;
				Label label2 = new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Gold * 0.9f, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				if (Session.Account.SID != {20287}.SID && member != null)
				{
					label2.EvRightButtonClick += delegate(ClickUiEventArgs {20292})
					{
						new {17558}(new {17549}(member.SID, member.Cached.Name, new {17549}.OptionalAction[]
						{
							{17549}.OptionalAction.RemoveFromGuildOrChangeStatus
						}));
					};
				}
				Label label3 = new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat * 0.9f, {20289}[1], PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				{20288}.AddItem(new UiControl[]
				{
					label2,
					label3
				});
			}
			Vector2 zero = Vector2.Zero;
			CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
			Color gray = Color.Gray;
			LocalizedDateTime localizedDateTime = new LocalizedDateTime({20287}.LogTime, false);
			localizedDateTime.UsePrefix = false;
			Label label4 = new Label(zero, philosopher_, gray, localizedDateTime.GetDateAndTimeWithoutYear(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{20288}.AddSpace(20f);
			{20288}.AddItem(new UiControl[]
			{
				label4
			});
		}

		// Token: 0x04000CD4 RID: 3284
		private static readonly Rectangle c_searchBox = new Rectangle(1529, 2266, 820, 37);

		// Token: 0x04000CD5 RID: 3285
		private const string NameKey = "%";

		// Token: 0x04000CD6 RID: 3286
		private string {20290};
	}
}
