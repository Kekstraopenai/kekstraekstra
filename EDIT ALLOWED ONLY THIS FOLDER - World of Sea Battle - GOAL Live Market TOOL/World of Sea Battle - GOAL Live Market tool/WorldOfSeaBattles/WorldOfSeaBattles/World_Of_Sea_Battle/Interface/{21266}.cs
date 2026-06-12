using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002FC RID: 764
	internal class {21266} : {21102}
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x0008CF35 File Offset: 0x0008B135
		public static int[] Widths
		{
			get
			{
				return new int[]
				{
					77,
					77,
					87,
					95,
					95,
					100,
					103
				};
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0008CF48 File Offset: 0x0008B148
		public IReadOnlyList<{21111}> Ships
		{
			get
			{
				return this.{21286};
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0008CF50 File Offset: 0x0008B150
		public float Width
		{
			get
			{
				return this.unscaledMarker.WH.X;
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0008CF64 File Offset: 0x0008B164
		public {21266}(Marker {21271}, int[] {21272}, {21078} {21273}, bool {21274}) : base({21271})
		{
			base.Form = new Form({21271}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			float num = {21271}.XY.X + (float){21201}.OffsetX;
			if ({21272}.Length == 0)
			{
				{21272} = new int[7];
			}
			if ({21272}.Length != 7)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 1);
				defaultInterpolatedStringHandler.AppendLiteral("shipIds.Length must be ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(7);
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			for (int i = 0; i < {21272}.Length; i++)
			{
				int num2 = {21266}.Widths[i];
				if ({21272}[i] != 0)
				{
					{21111} item = new {21111}(new Marker(num, {21271}.XY.Y, (float)num2, {21271}.WH.Y), 100f, {21272}[i], {21273});
					this.{21286}.Add(item);
				}
				else
				{
					this.{21286}.Add(null);
				}
				num += (float)num2;
			}
			for (int j = {21272}.Length - 1; j >= 0; j--)
			{
				if (this.{21286}[j] != null)
				{
					base.Form.AddChild(this.{21286}[j].Form);
				}
			}
			float num3 = num - {21271}.XY.X + (float){21201}.OffsetX;
			UiControl form = base.Form;
			ref Marker ptr = ref base.Form.Pos;
			Vector2 vector = new Vector2(num3, base.Form.Pos.WH.Y);
			form.Pos = new Marker(ref ptr.XY, ref vector);
			this.unscaledMarker = {21271}.SetWidth(num3);
			this.{21287} = {21272};
			this.{21288} = {21274};
			if ({21274})
			{
				this.{21279}();
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0008D113 File Offset: 0x0008B313
		public void AddLine()
		{
			this.{21289} = new {21168}(this);
			base.Form.AddChild(this.{21289}.Form);
			this.{21289}.Form.MoveToBackLevel();
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0008D147 File Offset: 0x0008B347
		public void HideLineParts(bool {21275}, FractionID? {21276}, bool {21277})
		{
			{21168} {21168} = this.{21289};
			if ({21168} == null)
			{
				return;
			}
			{21168}.HideParts({21275}, {21276}, {21277});
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0008D15C File Offset: 0x0008B35C
		protected override void DoScale(float {21278})
		{
			foreach ({21111} {21111} in this.{21286})
			{
				if ({21111} != null)
				{
					{21111}.Offset = base.Offset;
					{21111}.Scale({21278});
				}
			}
			if (this.{21289} != null)
			{
				this.{21289}.Offset = base.Form.Pos.XY + Vector2.UnitX * 320f * {21278};
				this.{21289}.Scale({21278});
			}
			if (this.{21288})
			{
				this.{21279}();
			}
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0008D218 File Offset: 0x0008B418
		private void {21279}()
		{
			float scaleFactor = 20f * this.scaleFactor;
			float {14085} = 9f * this.scaleFactor;
			int {3506} = this.{21287}.First((int {21293}) => {21293} != 0);
			PlayerShipInfo playerShipInfo = Gameplay.PlayersInfo.FromID({3506});
			CommonAtlas.ShipClassIconStyle shipClassIconStyle = this.{21284}(playerShipInfo);
			Rectangle shipyardTableIcon = CommonAtlas.GetShipyardTableIcon(playerShipInfo.Class, shipClassIconStyle);
			string text = (shipClassIconStyle == CommonAtlas.ShipClassIconStyle.Empire) ? Local.imperial : playerShipInfo.Class.ToStringLocal(false, true);
			if (this.{21292} == null)
			{
				this.{21292} = new StackForm(Vector2.Zero, UiOrientation.HorizontalBottom, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				this.{21290} = new Image(Marker.Zero, CommonAtlas.Texture.Tex, shipyardTableIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				this.{21291} = new Label(Vector2.Zero, Fonts.Philosopher_16, Color.Wheat, text, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				base.Form.AddChild(this.{21292});
			}
			UiControl uiControl = this.{21290};
			Vector2 zero = Vector2.Zero;
			Vector2 vector = Vector2.One * scaleFactor;
			uiControl.Pos = new Marker(ref zero, ref vector);
			this.{21291}.ScaleOfCentr = 0.5f * this.scaleFactor;
			this.{21292}.Clear();
			this.{21292}.AddItem(new UiControl[]
			{
				this.{21291}
			});
			this.{21292}.AddSpace({14085});
			this.{21292}.AddItem(new UiControl[]
			{
				this.{21290}
			});
			this.{21292}.ToolTip = new ToolTip(500f, float.MaxValue, new ToolTipState(this.{21280}(playerShipInfo, text, shipClassIconStyle == CommonAtlas.ShipClassIconStyle.Empire)));
			Vector2 xy = base.Form.Pos.XY;
			float y = base.Form.Pos.WH.Y;
			float num = (float){21201}.OffsetX * this.scaleFactor;
			float num2 = 20f * this.scaleFactor;
			float num3 = 24f * this.scaleFactor;
			float {11526} = xy.X + num + num2 - num3 - this.{21292}.Pos.WH.X;
			float {11527} = xy.Y + y * 0.6f;
			this.{21292}.Pos = new Marker({11526}, {11527}, this.{21292}.Pos.WH.X, this.{21292}.Pos.WH.Y);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0008D490 File Offset: 0x0008B690
		private Composer {21280}(PlayerShipInfo {21281}, string {21282}, bool {21283})
		{
			Composer composer = new Composer(400f, 1f);
			string {12528} = string.Empty;
			string str = string.Empty;
			ComposerTextStyle {12529} = default(ComposerTextStyle);
			string str2 = string.Empty;
			ComposerTextStyle {12529}2 = default(ComposerTextStyle);
			string str3 = string.Empty;
			switch ({21281}.Class)
			{
			case ShipClass.Destroyer:
				{12528} = Local.ship_class_1_description;
				str = Local.ship_difficulty_very_high;
				{12529} = ComposerTextStyle.Red;
				str2 = Local.ship_difficulty_low;
				{12529}2 = ComposerTextStyle.Lime;
				str3 = Local.ship_role_fast;
				break;
			case ShipClass.Battleship:
				{12528} = Local.ship_class_2_description;
				str = Local.ship_difficulty_medium;
				{12529} = ComposerTextStyle.Orange;
				str2 = Local.ship_difficulty_high;
				{12529}2 = ComposerTextStyle.Red;
				str3 = Local.ship_role_war;
				break;
			case ShipClass.Hardship:
				{12528} = Local.ship_class_4_description;
				str = Local.ship_difficulty_medium;
				{12529} = ComposerTextStyle.Orange;
				str2 = Local.ship_difficulty_very_high;
				{12529}2 = ComposerTextStyle.Red;
				str3 = Local.ship_role_heavy;
				break;
			case ShipClass.CargoShip:
				{12528} = Local.ship_class_3_description;
				str = Local.ship_difficulty_high;
				{12529} = ComposerTextStyle.Red;
				str2 = Local.ship_difficulty_medium;
				{12529}2 = ComposerTextStyle.Orange;
				str3 = Local.ship_role_transport;
				break;
			case ShipClass.Mortar:
				{12528} = Local.ship_class_5_description;
				str = Local.ship_difficulty_low;
				{12529} = ComposerTextStyle.Lime;
				str2 = Local.ship_difficulty_high;
				{12529}2 = ComposerTextStyle.Red;
				str3 = Local.ship_role_mortar;
				break;
			default:
				throw new NotSupportedException();
			}
			if ({21283})
			{
				{12528} = Local.ship_class_7_description;
			}
			composer.AddHeader({21282} + " " + Local.ships, null);
			composer.AddSpace(18f);
			composer.AddText({12528}, ComposerTextStyle.Wheat, true);
			if (!{21283})
			{
				composer.AddSpace(18f);
				composer.AddText(Local.game_difficulty + " " + str, {12529}, true);
				composer.AddText(Local.battle_power + " " + str2, {12529}2, true);
				composer.AddText(Local.role_in_battle + " " + str3, ComposerTextStyle.Wheat, true);
			}
			return composer;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0008D67E File Offset: 0x0008B87E
		private CommonAtlas.ShipClassIconStyle {21284}(PlayerShipInfo {21285})
		{
			if ({21285}.Coolness == PlayerShipCoolness.Elite)
			{
				return CommonAtlas.ShipClassIconStyle.Gold;
			}
			if ({21285}.Coolness == PlayerShipCoolness.Unique)
			{
				return CommonAtlas.ShipClassIconStyle.Silver;
			}
			if ({21285}.Coolness != PlayerShipCoolness.Empire)
			{
				return CommonAtlas.ShipClassIconStyle.Yellow;
			}
			return CommonAtlas.ShipClassIconStyle.Empire;
		}

		// Token: 0x04000F5A RID: 3930
		private const int ShipCount = 7;

		// Token: 0x04000F5B RID: 3931
		private const float DefaultCardWidth = 100f;

		// Token: 0x04000F5C RID: 3932
		private const float IconSize = 20f;

		// Token: 0x04000F5D RID: 3933
		private const float IconSpacing = 9f;

		// Token: 0x04000F5E RID: 3934
		private const float IconOffset = 24f;

		// Token: 0x04000F5F RID: 3935
		private const float StackPosYFactor = 0.6f;

		// Token: 0x04000F60 RID: 3936
		private const int ToolTipWidth = 400;

		// Token: 0x04000F61 RID: 3937
		private const int ToolTipDelay = 500;

		// Token: 0x04000F62 RID: 3938
		private const float ToolTipMaxWidth = 3.4028235E+38f;

		// Token: 0x04000F63 RID: 3939
		private const float LineOffset = 20f;

		// Token: 0x04000F64 RID: 3940
		private const float OffsetXFix = 320f;

		// Token: 0x04000F65 RID: 3941
		private readonly List<{21111}> {21286} = new List<{21111}>();

		// Token: 0x04000F66 RID: 3942
		private readonly int[] {21287};

		// Token: 0x04000F67 RID: 3943
		private readonly bool {21288};

		// Token: 0x04000F68 RID: 3944
		private {21168} {21289};

		// Token: 0x04000F69 RID: 3945
		private Image {21290};

		// Token: 0x04000F6A RID: 3946
		private Label {21291};

		// Token: 0x04000F6B RID: 3947
		private StackForm {21292};
	}
}
