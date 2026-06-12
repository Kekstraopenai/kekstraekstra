using System;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200025E RID: 606
	internal class {20283} : StackForm
	{
		// Token: 0x06000D97 RID: 3479 RVA: 0x000728A4 File Offset: 0x00070AA4
		public {20283}() : base(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			base.AddItem(new UiControl[]
			{
				new Label(new Vector2(13f, 15f), Fonts.Philosopher_14, Color.LightGray, Local.guild_rank_ristriction(Gameplay.MinRankEnteringGuild(false)), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			for (int i = 0; i < Session.Guild.Invites.Size; i++)
			{
				GuildInvite invite = Session.Guild.Invites.Array[i];
				Form form = new Form(Vector2.Zero, {20364}.c_item2, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form.AddChild(new Label(new Vector2(13f, 9f), Fonts.Arial_12, Color.White, invite.CachedName, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				form.EvClick += delegate(ClickUiEventArgs {20284})
				{
					new {17558}(new {17549}(invite.SID, invite.CachedName, new {17549}.OptionalAction[]
					{
						{17549}.OptionalAction.AcceptOrRejectInviteToGuild
					}));
				};
				if (!string.IsNullOrEmpty(invite.InviteMessage))
				{
					string text = invite.InviteMessage;
					if (text.Length > 50)
					{
						text = text.Substring(0, 50) + "...";
					}
					form.AddChild(new Label(new Vector2(13f, 29f), Fonts.Arial_9, Color.White * 0.6f, text, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					form.ToolTipState = new ToolTipState("", invite.InviteMessage, Array.Empty<ToolTipCharacteristics>());
				}
				stackForm.AddItem(new UiControl[]
				{
					form
				});
				if (stackForm.GetChildren.Size == 2)
				{
					base.AddItem(new UiControl[]
					{
						stackForm
					});
					stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				}
			}
			if (Session.Guild.Invites.Size == 0)
			{
				Form form2 = new Form(Vector2.Zero, {20364}.c_item2, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				stackForm.AddItem(new UiControl[]
				{
					form2
				});
				form2.AddChild(new Label(new Vector2(13f, 9f), Fonts.Arial_12, Color.White, Local.empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				form2.Opacity = 0.4f;
			}
			if (stackForm.GetChildren.Size > 0)
			{
				base.AddItem(new UiControl[]
				{
					stackForm
				});
			}
		}
	}
}
