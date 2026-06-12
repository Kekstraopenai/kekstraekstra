using System;
using System.Collections.Generic;
using Common;
using Common.Account;
using Common.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020003E8 RID: 1000
	internal class {22746} : {17625}
	{
		// Token: 0x060015D4 RID: 5588 RVA: 0x000B7668 File Offset: 0x000B5868
		public {22746}(PlayerSlimProfileInfo {22748}) : base(606f, {17625}.c_back2, {17604}.InGameWindow, Rectangle.Empty, Array.Empty<{17625}.DynamicTittle>())
		{
			base.AddChild(new Rectangle(813, 3485, 119, 87), base.Pos);
			Image {12952} = new Image(new Marker(0f, 0f, 466f, 83f), AtlasGameGui.Texture.Tex, new Rectangle(1412, 1171, 916, 163), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Form form = new Form(new Marker(0f, 0f, 451f, 80f), new Rectangle(0, 440, 432, 96), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			base.AddChildPos({12952}, PositionAlignment.Center, PositionAlignment.LeftUp, -54f);
			base.AddChildPos(form, PositionAlignment.Center, PositionAlignment.LeftUp, -24f);
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_24, Color.Black * 0.95f, {22748}.PlayerName, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			if ({22748}.Title != CaptainTitle.None)
			{
				stackForm.AddItem(new UiControl[]
				{
					new Image(new Marker(0f, 0f, 32f, 32f), AtlasObjs.Texture.Tex, AtlasObjs.GetCaptainTitleIcon({22748}.Title), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}
			form.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.LeftUp, 14f);
			string {13345};
			if ({22748}.GuildTag.Length == 0)
			{
				{13345} = Local.ProfileWatchWindow_3a;
			}
			else
			{
				{13345} = Local.ProfileWatchWindow_3c(GuildAccessRightsInfo.AllRights[(int){22748}.GuildRankId].DisplayName, {22748}.GuildTag);
			}
			base.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, {22746}.lightTextCol_new, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 48f);
			{22746}.CurrentInstance = this;
			this.AnimatedFocus = false;
			base.EvRemoveFromContainer += delegate()
			{
				{22746}.CurrentInstance = null;
			};
			string profileWatchWindow_ = Local.ProfileWatchWindow_1;
			string str;
			if (!{22748}.OnlineNow)
			{
				LocalizedDateTime localizedDateTime = new LocalizedDateTime({22748}.LastActivity, false);
				localizedDateTime.UsePrefix = false;
				str = localizedDateTime.GetDate();
			}
			else
			{
				str = Local.online;
			}
			string {13345}2 = profileWatchWindow_ + str + ", " + Local.time_in_game({22748}.HoursInGame);
			base.AddChildPos(new Label(Vector2.Zero, Fonts.Arial_10, {22746}.lightTextCol_new * 0.7f, {13345}2, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 69f);
			ListItemViewControl listItemViewControl = this.ListHelper(new Marker(16f, 121f, 293f, 330f), this.ShipIcons({22748}));
			ListItemViewControl listItemViewControl2 = this.ListHelper(new Marker(332f, 121f, 293f, 330f), this.AchievementIcons({22748}));
			base.AddChild(new Label(new Vector2(listItemViewControl.Pos.Center.X, listItemViewControl.Pos.XY.Y - 15f), Fonts.Philosopher_16, Color.White * 0.8f, Local.ProfileWatchWindow_2, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			base.AddChild(new Label(new Vector2(listItemViewControl2.Pos.Center.X, listItemViewControl2.Pos.XY.Y - 15f), Fonts.Philosopher_16, {22746}.lightTextCol * 0.8f, Local.achievements, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000B7A0C File Offset: 0x000B5C0C
		private ListItemViewControl ListHelper(Marker {22749}, IEnumerable<UiControl> {22750})
		{
			Marker marker = new Marker({22749}.XY.X + {22749}.WH.X - (float)CommonAtlas.c_scrollPoint.Width, {22749}.XY.Y, (float)CommonAtlas.c_scrollUp.Width, {22749}.WH.Y);
			Marker pos = base.Pos;
			ScrollBarControl scrollBarControl = new ScrollBarControl(marker.Offset(pos.XY), CommonAtlas.c_scrollUp, CommonAtlas.c_scrollDown, CommonAtlas.c_scrollPoint, CommonAtlas.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			scrollBarControl.Opacity = 0.4f;
			pos = base.Pos;
			ListItemViewControl listItemViewControl = new ListItemViewControl({22749}.Offset(pos.XY).SetWidth({22749}.WH.X - (float)CommonAtlas.c_scrollPoint.Width), scrollBarControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			foreach (UiControl uiControl in {22750})
			{
				listItemViewControl.AddItem(new UiControl[]
				{
					uiControl
				});
			}
			base.AddChild(new UiControl[]
			{
				scrollBarControl,
				listItemViewControl
			});
			return listItemViewControl;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x000B7B40 File Offset: 0x000B5D40
		private Form {22751}(Texture2D {22752}, string {22753}, string {22754} = "")
		{
			Form form = new Form(Vector2.Zero, {22746}.c_item, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if ({22752} == null)
			{
				form.AddChild(new Form(new Marker(2f, 2f, 50f, 50f), new Rectangle(992, 20, 73, 73), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				});
			}
			else
			{
				form.AddChild(new Image(new Marker(2f, 2f, 50f, 50f), {22752}, {22752}.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			form.AddChild(new Label(new Vector2(60f, 5f), Fonts.Philosopher_14, Color.Gray, {22753}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			if (!string.IsNullOrEmpty({22754}))
			{
				form.AddChild(new Label(new Vector2(60f, 25f), Fonts.Philosopher_14, Color.Yellow * 0.5f, {22754}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			return form;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x000B7C2F File Offset: 0x000B5E2F
		private IEnumerable<UiControl> ShipIcons(PlayerSlimProfileInfo {22755})
		{
			{22746}.<ShipIcons>d__7 <ShipIcons>d__ = new {22746}.<ShipIcons>d__7(-2);
			<ShipIcons>d__.<>4__this = this;
			<ShipIcons>d__.<>3__profile = {22755};
			return <ShipIcons>d__;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x000B7C46 File Offset: 0x000B5E46
		private IEnumerable<UiControl> AchievementIcons(PlayerSlimProfileInfo {22756})
		{
			{22746}.<AchievementIcons>d__8 <AchievementIcons>d__ = new {22746}.<AchievementIcons>d__8(-2);
			<AchievementIcons>d__.<>4__this = this;
			<AchievementIcons>d__.<>3__profile = {22756};
			return <AchievementIcons>d__;
		}

		// Token: 0x040013B4 RID: 5044
		public static {22746} CurrentInstance;

		// Token: 0x040013B5 RID: 5045
		private static readonly Color lightTextCol = new Color(233, 216, 187);

		// Token: 0x040013B6 RID: 5046
		private static readonly Color lightTextCol_new = new Color(218, 202, 163);

		// Token: 0x040013B7 RID: 5047
		private static readonly Rectangle c_item = new Rectangle(987, 432, 272, 54);
	}
}
