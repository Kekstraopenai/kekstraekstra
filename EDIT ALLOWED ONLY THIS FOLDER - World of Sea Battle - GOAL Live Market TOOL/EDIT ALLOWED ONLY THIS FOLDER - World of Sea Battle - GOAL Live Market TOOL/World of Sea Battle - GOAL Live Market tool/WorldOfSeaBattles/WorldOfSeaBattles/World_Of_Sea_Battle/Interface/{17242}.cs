using System;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Graphics.Effects;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000088 RID: 136
	[Obsolete("Will be removed")]
	public class {17242} : Form
	{
		// Token: 0x060003BD RID: 957 RVA: 0x0001F8FC File Offset: 0x0001DAFC
		public {17242}(Vector2 {17245}, PlayerShipInfo {17246}) : base(new Marker(ref {17245}, ref {17242}.c_form), {17242}.c_form, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.AnimatedFocus = false;
			Marker marker = new Marker(4f, 4f, 84f, 84f);
			Marker marker2 = base.Pos;
			base.AddChild(new Image(marker.Offset(marker2.XY), {17246}.IconTexture, {17246}.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			base.AddChild(new Label(new Vector2(94f, 20f) + base.Pos.XY, Fonts.Philosopher_14, new Color(185, 215, 229) * 1.1f, {17246}.ShipName, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			base.AddChild(new Label(new Vector2(137f, 47f) + base.Pos.XY, Fonts.Philosopher_16, new Color(95, 142, 198), StringHelper.ToRoman({17246}.Rank), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			marker2 = new Marker(91f, 38f, 40f, 40f);
			marker2 = marker2.Border(-3f);
			marker = base.Pos;
			base.AddChild(new Form(marker2.Offset(marker.XY), CommonAtlas.GetShipClassIcon({17246}), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001FA70 File Offset: 0x0001DC70
		public void AddStatusIcons(PlayerShipDynamicInfo {17247})
		{
			Vector2 {13189} = new Vector2(171f, 50f);
			Vector2 {13189}2 = new Vector2(188f, 50f);
			Vector2 {13189}3 = new Vector2(206f, 50f);
			if ({17247}.Cannons.Count > 0)
			{
				base.AddChild(new Form({13189}, {17242}.c_weapons, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				});
			}
			if ({17247}.GetDesignElement(2) != null)
			{
				base.AddChild(new Form({13189}2, {17242}.c_lghts_noColor, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					BasicColor = ShipCustomLightEffect.GetLightColor((int){17247}.GetDesignElement(2).ID),
					AnimatedFocus = false
				});
			}
			if ({17247}.BallsOfHold.NamesCount + {17247}.PowderKegsOfHold.NamesCount > 0)
			{
				base.AddChild(new Form({13189}3, {17242}.c_equip, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				});
			}
		}

		// Token: 0x04000304 RID: 772
		public static readonly Rectangle c_form = new Rectangle(853, 1000, 243, 91);

		// Token: 0x04000305 RID: 773
		public static readonly Rectangle c_weapons = new Rectangle(853, 1092, 17, 16);

		// Token: 0x04000306 RID: 774
		public static readonly Rectangle c_lghts = new Rectangle(871, 1092, 17, 16);

		// Token: 0x04000307 RID: 775
		public static readonly Rectangle c_lghts_noColor = new Rectangle(929, 1092, 17, 16);

		// Token: 0x04000308 RID: 776
		public static readonly Rectangle c_equip = new Rectangle(889, 1092, 17, 16);

		// Token: 0x04000309 RID: 777
		public static readonly Rectangle c_equipMortarBall = new Rectangle(947, 1092, 17, 16);

		// Token: 0x0400030A RID: 778
		public static readonly Rectangle c_holdIcon = new Rectangle(965, 1092, 17, 16);

		// Token: 0x0400030B RID: 779
		public static readonly Rectangle c_integrityDot = new Rectangle(907, 1100, 10, 7);

		// Token: 0x0400030C RID: 780
		public static readonly Rectangle c_integrityDotEmpty = new Rectangle(918, 1100, 10, 7);
	}
}
