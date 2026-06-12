using System;
using System.Runtime.CompilerServices;
using Common;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Effects;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000086 RID: 134
	public class {17228} : Form
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0001F1FE File Offset: 0x0001D3FE
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0001F206 File Offset: 0x0001D406
		public CheckboxControl CheckboxControl { get; private set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0001F20F File Offset: 0x0001D40F
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0001F217 File Offset: 0x0001D417
		public Vector2 FavoriteIconPosition { get; private set; } = new Vector2(30f, 60f);

		// Token: 0x060003B4 RID: 948 RVA: 0x0001F220 File Offset: 0x0001D420
		public {17228}(Vector2 {17233}, PlayerShipInfo {17234}) : base(new Marker(ref {17233}, ref {17228}.c_form), {17228}.c_form, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.Ship = {17234};
			this.AnimatedFocus = false;
			int num = 90;
			Color {13344} = Color.Lerp(new Color(204, 184, 118), Color.White, 0.3f);
			base.AddChild(this.{17238} = new Label(new Vector2((float)num, 17f) + base.Pos.XY, Fonts.Philosopher_14, {13344}, {17234}.ShipName, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			Marker marker;
			Marker marker2;
			if (!{17234}.StaticInfo.IsBalloon)
			{
				marker = new Marker((float)(num - 1), 35f, 25f, 25f);
				marker2 = base.Pos;
				base.AddChild(new Form(marker.Offset(marker2.XY), CommonAtlas.GetShipClassIcon({17234}), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				});
			}
			base.AddChild(new Label(new Vector2((float)(num + 30), 39f) + base.Pos.XY, Fonts.Philosopher_12, new Color(185, 215, 229) * 0.9f, StringHelper.ToRoman({17234}.Rank), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			marker2 = new Marker(9f - (float){17234}.Rank * 0.8f, -14f, 82f, 82f);
			marker = base.Pos;
			base.AddChild(new Image(marker2.Offset(marker.XY), {17234}.IconTextureWhitespace, {17234}.IconShipyardTextureInnerRectangle, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001F3D4 File Offset: 0x0001D5D4
		public {17228} AddStatusIcons(PlayerShipDynamicInfo {17235})
		{
			this.{17238}.Text = {17235}.ShipNameVisible;
			this.UsedShipInfo = {17235};
			Vector2 value = new Vector2(156f, 50f);
			Vector2 value2 = new Vector2(173f, 50f);
			Vector2 value3 = new Vector2(191f, 50f);
			Vector2 value4 = new Vector2(-10f, -10f);
			if ({17235}.Cannons.Count > 0)
			{
				base.AddChild(new Form(value + value4, {17242}.c_weapons, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				});
			}
			if ({17235}.BallsOfHold.NamesCount + {17235}.PowderKegsOfHold.NamesCount > 0)
			{
				base.AddChild(new Form(value2 + value4, {17242}.c_equip, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				});
			}
			if ({17235}.GetDesignElement(2) != null)
			{
				base.AddChild(new Form(value3 + value4, {17242}.c_lghts_noColor, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					BasicColor = ShipCustomLightEffect.GetLightColor((int){17235}.GetDesignElement(2).ID),
					AnimatedFocus = false
				});
			}
			Vector2 vector = new Vector2(2f, 2f);
			Vector2 vector2 = new Vector2(72f, 73f);
			Form form = new Form(new Marker(ref vector, ref vector2), Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			form.RenderComplete += delegate(UiControl {17241})
			{
				float num = ({17235}.CraftFrom.MaxIntegrity == -1) ? 1f : ((float){17235}.Integrity / (float){17235}.CraftFrom.MaxIntegrity);
				Color color = Color.Lerp(Color.White, Color.Red, 1f - num) * {17241}.GetOpcaity();
				int num2 = (int)(73f * num);
				Rectangle rectangle = new Rectangle(403, 1351 + (73 - num2), 72, num2);
				Device gs = Engine.GS;
				Vector2 vector3 = new Vector2(0f, (float)(73 - num2)) + {17241}.Pos.XY;
				gs.Draw(rectangle, vector3, color);
			};
			base.AddChild(form);
			if ({17235}.ClientTimeToRestoreIntegrity > 0f)
			{
				base.AddChildPos(new LiveLabel(Vector2.Zero, Fonts.Philosopher_14, Color.LightGray, () => StringHelper.TimeMMMSS((double){17235}.ClientTimeToRestoreIntegrity), 100)
				{
					Shadowed = true
				}, PositionAlignment.LeftUp, PositionAlignment.Center, 21f);
			}
			if ({17235}.TrophyShipNotification)
			{
				Label label = new Label(Vector2.Zero, Fonts.Arial_10, Color.White, Local.trophy, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Marker marker = label.Pos.Border(2f);
				vector = base.Pos.XY + new Vector2(90f, -5f);
				Form form2 = new Form(marker.SetXY(vector).ScaleWidth(2f), new Rectangle(394, 1228, 113, 19), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form2.AddChildPos(label, PositionAlignment.LeftUp, PositionAlignment.Center, 2f);
				base.AddChild(form2);
			}
			return this;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001F678 File Offset: 0x0001D878
		public void AddFavoriteCheckbox()
		{
			this.CheckboxControl = new CheckboxControl(this.FavoriteIconPosition + base.Pos.XY, {17228}.FavoriteActiveIcon, {17228}.FavoriteDectiveIcon, Global.Settings.FavoriteShips.Contains((byte)this.Ship.ID), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.CheckboxControl.ExCheckEvent(new Action<CheckboxCheckedEventArgs>(this.{17236}));
			base.AddChild(this.CheckboxControl);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001F6F4 File Offset: 0x0001D8F4
		public bool IsCheckboxClicked()
		{
			Vector2 mouseToUI = Engine.GS.MouseToUI;
			Rectangle rectangle = new Rectangle((int)(this.FavoriteIconPosition.X + base.Pos.XY.X), (int)(this.FavoriteIconPosition.Y + base.Pos.XY.Y), {17228}.FavoriteActiveIcon.X, {17228}.FavoriteActiveIcon.Y);
			return this.CheckboxControl != null && rectangle.Contains((int)mouseToUI.X, (int)mouseToUI.Y);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001F7D8 File Offset: 0x0001D9D8
		[CompilerGenerated]
		private void {17236}(CheckboxCheckedEventArgs {17237})
		{
			byte b;
			if ({17237}.NewValue)
			{
				Tlist<byte> favoriteShips = Global.Settings.FavoriteShips;
				b = (byte)this.Ship.ID;
				favoriteShips.AddIfNotContains(b);
				return;
			}
			Tlist<byte> favoriteShips2 = Global.Settings.FavoriteShips;
			b = (byte)this.Ship.ID;
			favoriteShips2.Remove(b);
		}

		// Token: 0x040002FB RID: 763
		public static readonly Rectangle c_form = new Rectangle(403, 1514, 265, 79);

		// Token: 0x040002FC RID: 764
		public static readonly Rectangle FavoriteActiveIcon = new Rectangle(659, 206, 21, 21);

		// Token: 0x040002FD RID: 765
		public static readonly Rectangle FavoriteDectiveIcon = new Rectangle(637, 206, 21, 21);

		// Token: 0x040002FE RID: 766
		public PlayerShipInfo Ship;

		// Token: 0x040002FF RID: 767
		public PlayerShipDynamicInfo UsedShipInfo;

		// Token: 0x04000300 RID: 768
		private Label {17238};

		// Token: 0x04000301 RID: 769
		[CompilerGenerated]
		private CheckboxControl {17239};

		// Token: 0x04000302 RID: 770
		[CompilerGenerated]
		private Vector2 {17240};
	}
}
