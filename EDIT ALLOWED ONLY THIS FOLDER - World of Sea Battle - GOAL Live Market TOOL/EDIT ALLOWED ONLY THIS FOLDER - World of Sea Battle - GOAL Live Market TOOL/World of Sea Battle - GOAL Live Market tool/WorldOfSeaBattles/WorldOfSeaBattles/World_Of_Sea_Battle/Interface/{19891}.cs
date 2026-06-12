using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using ManualPacketSerialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Graphics.Shaders;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200022B RID: 555
	internal sealed class {19891} : CustomUi
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x000654F7 File Offset: 0x000636F7
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x000654FE File Offset: 0x000636FE
		public static {19891} CurrentInstance { get; private set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00065506 File Offset: 0x00063706
		private bool isRotating
		{
			get
			{
				return Global.Player.MapInfo.IsWorldmap && Global.Settings.MinimapCompas;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x00065525 File Offset: 0x00063725
		private Vector2 OccluderPos
		{
			get
			{
				if (!Global.Camera.IsFreeMode)
				{
					return Global.Player.Position;
				}
				return Global.Camera.Position.XZ();
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0006554D File Offset: 0x0006374D
		private static float UiScale
		{
			get
			{
				return Global.Settings.MinimapScale;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00065559 File Offset: 0x00063759
		private static int Size
		{
			get
			{
				return 190;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00065560 File Offset: 0x00063760
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x00065568 File Offset: 0x00063768
		public Form EmpireLootForm { get; private set; }

		// Token: 0x06000C85 RID: 3205 RVA: 0x00065574 File Offset: 0x00063774
		public {19891}() : base(new Marker((float)Engine.GS.UIArea.Width - (float){19891}.Size * {19891}.UiScale - 15f, (float)Engine.GS.UIArea.Height - (float){19891}.Size * {19891}.UiScale - 10f, (float){19891}.Size, (float){19891}.Size).ScaleSize({19891}.UiScale), Rectangle.Empty, PositionAlignment.RightDown, PositionAlignment.RightDown, Color.White, false)
		{
			{19891} <>4__this = this;
			{19891}.CurrentInstance = this;
			{20391}.WhenInit(this, "minimap");
			this.AnimatedFocus = false;
			base.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(null, Local.MinimapGui_0, Array.Empty<ToolTipCharacteristics>()));
			base.EvClick += delegate(ClickUiEventArgs {19964})
			{
				if (!Global.Settings.EnableDragDrop)
				{
					{22913}.TryToOpen();
				}
			};
			this.{19947} = new Tlist<{19891}.GroupIconAnimation>(7);
			base.EvRemoveFromContainer += delegate()
			{
				{19891}.CurrentInstance = null;
			};
			this.{19948} = {19891}.savedZoom;
			ShipCurrentPlayer player = Global.Player;
			this.{19956} = ((player != null) ? player.NearAquatoria : null);
			Rectangle activePath = new Rectangle(823, 134, 320, 43);
			Rectangle rolledPath = new Rectangle(823, 179, 320, 43);
			bool rolledMode = false;
			this.EmpireLootForm = new Form(Vector2.Zero, activePath, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false,
				IsVisible = false,
				PositionAlignment_X = (base.PositionAlignment_Y = PositionAlignment.RightDown)
			};
			this.EmpireLootForm.Pos = this.EmpireLootForm.Pos.Scale(0.9f);
			UiControl empireLootForm = this.EmpireLootForm;
			Marker pos = this.EmpireLootForm.Pos;
			Vector2 vector = new Vector2((float)Engine.GS.UIArea.Width - this.EmpireLootForm.PosWidth, base.Pos.XY.Y - 50f);
			empireLootForm.Pos = pos.SetXY(vector);
			this.EmpireLootForm.AddChildPos(new LiveLabel(Vector2.Zero, Fonts.Philosopher_14, Color.Black * 0.9f, delegate(LiveLabel {19965})
			{
				if (Global.Game.InteractiveWorldSystem.EmpireLootNotification == null)
				{
					<>4__this.EmpireLootForm.IsVisible = false;
					return " ";
				}
				<>4__this.EmpireLootForm.IsVisible = true;
				float x = Vector2.Distance(Global.Game.InteractiveWorldSystem.EmpireLootNotification.Position, Global.Player.Position);
				if (!rolledMode)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
					defaultInterpolatedStringHandler.AppendFormatted<float>(MathF.Ceiling(x));
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted(Local.empire_loot);
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}
				return string.Empty;
			}, 1), PositionAlignment.LeftUp, PositionAlignment.Center, 34f);
			this.EmpireLootForm.EvClick += delegate(ClickUiEventArgs {19966})
			{
				rolledMode = !rolledMode;
				<>4__this.EmpireLootForm.TexturePath = (rolledMode ? rolledPath : activePath);
			};
			base.RemoveWithThis(new UiControl[]
			{
				this.EmpireLootForm
			});
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00065820 File Offset: 0x00063A20
		protected override void UserUpdate(ref FrameTime {19894})
		{
			for (int i = 0; i < this.{19947}.Size; i++)
			{
				if (this.{19947}.Array[i].Update(ref {19894}))
				{
					this.{19947}.RemoveAt(i);
					i--;
				}
			}
			if (Global.Player.MapInfo.IsEducationMap)
			{
				this.{19948} = 0.7f;
			}
			else
			{
				float min = 4f;
				float max = 14f;
				if (base.InputMode == MouseInputMode.Focused)
				{
					{19891}.savedZoom = MathHelper.Clamp({19891}.savedZoom + (float)(InputHelper.NowMouseState.ScrollValue - InputHelper.LastMouseState.ScrollValue) * 0.005f, min, max);
				}
				float num = Geometry.InverseLerp(0.3f, 1.2f, Geometry.Smoothstep(Global.Player.NowSpeed / Global.Player.UsedShip.Speed));
				num = 1.5f * (num - 0.5f) * 2f;
				if (num > 0f)
				{
					num /= 2f;
				}
				num -= 1f;
				this.{19948} = MathHelper.Clamp({19891}.savedZoom - num, min, max);
			}
			base.IsVisible = (Global.Render.UiMode == InterfaceMode.Default);
			{19894}.EvaluteTimerMs(ref this.{19957});
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0006595A File Offset: 0x00063B5A
		protected override void UserBackRender()
		{
			this.{19899}();
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00065964 File Offset: 0x00063B64
		protected override void UserFrontRender()
		{
			Engine.GS.SetScissor(this.{19951}.ToRect(), true);
			float num = WosbVisibility.VisibleDistance(Global.Player, Global.Game.StaticSystem.GetSkyShader.DayOrNight, 0f);
			float num2 = {19891}.UiScale / 1.4f;
			float num3 = num2 * 0.36f * 1.1f;
			this.{19953} = (this.isRotating ? (-Global.Player.Rotation) : 0f);
			int num4;
			this.{19955} = ((!Global.Player.MapInfo.IsWorldmap) ? base.Pos.Center : this.{19895}(this.OccluderPos, out num4, true));
			this.{19914}();
			this.{19915}();
			this.{19916}(num2);
			this.{19918}(num, num3);
			this.{19921}(num, num3, num2);
			this.{19925}(num, num2);
			this.{19928}();
			this.{19929}();
			this.{19930}();
			this.{19931}();
			this.{19932}();
			if (Global.Player.MapInfo.IsWorldmap)
			{
				this.{19933}();
			}
			this.{19934}();
			this.{19935}(num);
			this.{19942}();
			if (InputHelper.NowInputState.IsDown(Keys.LeftControl) && Global.Player.MapInfo.IsWorldmap)
			{
				Device gs = Engine.GS;
				Vector2 vector = base.Pos.XY + new Vector2(33f, 33f);
				gs.Draw({19891}.scrollTooltip, vector, this.{19954});
			}
			Engine.GS.ResetScissor();
			this.{19939}();
			this.{19938}();
			this.{19940}();
			if (Global.Player.MapInfo.IsWorldmap)
			{
				IslePortInfo nearAquatoria = Global.Player.NearAquatoria;
				if (this.{19956} != nearAquatoria)
				{
					this.{19957} = 14000f;
					this.{19956} = nearAquatoria;
				}
				if (this.{19957} > 0f)
				{
					Relation nearPortRelation = Session.Game.NearPortRelation;
					float num5 = 1f;
					num5 *= Geometry.InverseLerp(0f, 7000f, this.{19957});
					Engine.GS.SetFont(Fonts.Arial_10Bold);
					Device gs2 = Engine.GS;
					string {14610} = (nearAquatoria == null) ? Local.neutral_waters : nearAquatoria.PortNameShort;
					Vector2 vector = new Vector2(base.Pos.Center.X, base.Pos.Center.Y + 19f);
					Color color = ((nearAquatoria == null || nearPortRelation == Relation.Neutral) ? Color.LightGray : ((nearPortRelation == Relation.Enemy) ? Color.Pink : Color.SoftLime)) * num5;
					gs2.DrawStringCentered({14610}, vector, color);
				}
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00065C14 File Offset: 0x00063E14
		private Vector2 {19895}(Vector2 {19896}, out int {19897}, bool {19898})
		{
			if (!Global.Player.MapInfo.IsWorldmap)
			{
				return {20413}.WorldPosToRenderInternal({19896}, out {19897}, this.{19951}, {19898}, 0.5f / (7f / this.{19948}));
			}
			{19896} -= this.OccluderPos;
			{19896} *= this.{19948};
			Vector2 vector = {20413}.WorldPosToRenderInternal({19896}, out {19897}, this.{19951}, false, 0.5f / (7f / this.{19948}));
			if ({19898})
			{
				Vector2 vector2 = vector - this.{19951}.Center;
				float num = vector2.LengthSquared();
				float num2 = this.{19951}.WH.X * 0.5f * 0.9f;
				if (num > num2 * num2)
				{
					{19897} = Math.Max({19897}, (this.{19948} < 4f) ? 2 : 1);
					vector2 = vector2.Normal() * num2;
					vector = this.{19951}.Center + vector2;
				}
			}
			return vector;
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00065D0C File Offset: 0x00063F0C
		private void {19899}()
		{
			Rectangle rectangle = new Rectangle(9, 7, 173, 173);
			float uiScale = {19891}.UiScale;
			this.{19951}.XY.X = base.Pos.XY.X + (float)rectangle.X * uiScale;
			this.{19951}.XY.Y = base.Pos.XY.Y + (float)rectangle.Y * uiScale;
			this.{19951}.WH.X = (float)rectangle.Width * uiScale;
			this.{19951}.WH.Y = (float)rectangle.Height * uiScale;
			Rectangle rectangle2 = this.{19951}.ToRect();
			rectangle2.X--;
			rectangle2.Y--;
			rectangle2.Width += 2;
			rectangle2.Height += 2;
			rectangle2.X += rectangle2.Width / 2;
			rectangle2.Y += rectangle2.Height / 2;
			Color color = Color.White * 0.9f;
			if (Global.Player.MapInfo.IsWorldmap)
			{
				Vector2 vector = this.OccluderPos;
				int num;
				Vector2 vector2 = ({20413}.WorldPosToRenderInternal(vector, out num, this.{19951}, false, 0.5f) - this.{19951}.XY) / this.{19951}.WH;
				vector2.X -= 0.5f / this.{19948};
				vector2.Y -= 0.5f / this.{19948};
				Marker marker = new Marker(0f, 0f, 3164f, 2576f);
				Marker marker2 = new Marker(marker.XY.X + vector2.X * marker.WH.X, marker.XY.Y + vector2.Y * marker.WH.Y, marker.WH.X / this.{19948}, marker.WH.Y / this.{19948});
				Global.Render.MinimapCircleStencil.Parameters[0].SetValue(new Vector4(marker2.XY.X / (float)OtherTextures.WorldMapMini.Width, marker2.XY.Y / (float)OtherTextures.WorldMapMini.Height, marker2.End.X / (float)OtherTextures.WorldMapMini.Width, marker2.End.Y / (float)OtherTextures.WorldMapMini.Height));
				float {14587} = this.isRotating ? (-Global.Player.Rotation) : 0f;
				Engine.GS.End2D();
				Engine.GS.ResetScissor();
				Engine.GS.Begin2D(true, Global.Render.MinimapCircleStencil, null);
				Engine.GS.SetTexture(OtherTextures.WorldMapMini);
				Device gs = Engine.GS;
				Rectangle bounds = OtherTextures.WorldMapMini.Bounds;
				vector = new Vector2((float)(OtherTextures.WorldMapMini.Width / 2), (float)(OtherTextures.WorldMapMini.Height / 2));
				gs.Draw(bounds, rectangle2, vector, {14587}, color);
			}
			else
			{
				Global.Render.MinimapCircleStencil.Parameters[0].SetValue(new Vector4(0f, 0f, 1f, 1f));
				Engine.GS.End2D();
				Engine.GS.ResetScissor();
				if (ProceduralMinimapHelper.PrevMapInfo != Global.Player.MapInfo || ProceduralMinimapHelper.MapTexture == null)
				{
					ProceduralMinimapHelper.Draw(211, Global.Player.MapInfo);
				}
				Engine.GS.Begin2D(true, Global.Render.MinimapCircleStencil, null);
				Engine.GS.SetTexture(OtherTextures.WorldMapMiniArena.Tex);
				Device gs2 = Engine.GS;
				Rectangle bounds = OtherTextures.WorldMapMiniArena.Tex.Bounds;
				Vector2 vector = new Vector2((float)(OtherTextures.WorldMapMiniArena.Tex.Width / 2), (float)(OtherTextures.WorldMapMiniArena.Tex.Height / 2));
				gs2.Draw(bounds, rectangle2, vector, 0f, color);
				Engine.GS.SetTexture(ProceduralMinimapHelper.MapTexture.Resource);
				Device gs3 = Engine.GS;
				bounds = ProceduralMinimapHelper.MapTexture.Resource.Bounds;
				vector = ProceduralMinimapHelper.MapTexture.Resource.Bounds.WidthHeight() / 2f;
				gs3.Draw(bounds, rectangle2, vector, 0f, color);
			}
			Engine.GS.End2D();
			Engine.GS.Begin2D(true);
			Engine.GS.SetTexture(AtlasGameGui.Texture);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000661C0 File Offset: 0x000643C0
		private void {19900}(Vector2 {19901}, in Rectangle {19902}, float {19903}, Color {19904}, Texture2D {19905} = null, float {19906} = 1f, string {19907} = null, bool {19908} = false, bool {19909} = true, bool {19910} = true)
		{
			if (Global.Player.MapInfo.IsCircularShape)
			{
				{19901} *= 0.96f;
			}
			int num;
			Vector2 vector = this.{19895}({19901}, out num, {19909} || Global.Player.MapInfo.IsCircularShape);
			if (this.{19953} != 0f)
			{
				vector = Geometry.RotateVector2(vector, this.{19955}, this.{19953});
			}
			if (num == 2 && !{19908})
			{
				return;
			}
			if (num == 1 && Global.Player.MapInfo.IsWorldmap && ({19902} == {19891}.traderIcon || {19902} == {19891}.pharosIcon))
			{
				return;
			}
			if (num != 0 && !{19908} && Global.Player.MapInfo.IsWorldmap)
			{
				{19904} *= 0.4f;
			}
			{19906} *= 1.15f;
			if ({19905} != null)
			{
				Device gs = Engine.GS;
				Rectangle rectangle = new Marker(vector.X - 11f * {19906}, vector.Y - 11f * {19906}, 22f * {19906}, 22f * {19906}).ToRect();
				gs.DrawCustomTexture({19905}, {19902}, rectangle, {19904});
				return;
			}
			Vector2 vector2;
			vector2.X = (float)({19902}.Width / 2);
			vector2.Y = (float)({19902}.Height / 2);
			Engine.GS.Draw({19902}, vector, vector2, {19903}, {19906}, {19904});
			if (!string.IsNullOrEmpty({19907}) && num == 0)
			{
				Engine.GS.SetFont(Fonts.Arial_10Bold);
				Device gs2 = Engine.GS;
				Vector2 vector3 = vector + new Vector2(0f, vector2.Y + 5f);
				Color color = new Color(228, 197, 158) * ((float){19904}.A / 255f);
				gs2.DrawStringCentered({19907}, vector3, color);
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00066397 File Offset: 0x00064597
		public void SetPositions(Tlist<AllyStateTransfer> {19911}, Tlist<EnemyStateTransfer> {19912})
		{
			this.{19949} = {19911};
			this.{19950} = {19912};
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x000663A8 File Offset: 0x000645A8
		public void BeginGroupIconAnimation(int {19913})
		{
			if ({19913} == -1)
			{
				throw new InvalidOperationException();
			}
			Tlist<{19891}.GroupIconAnimation> tlist = this.{19947};
			{19891}.GroupIconAnimation groupIconAnimation = new {19891}.GroupIconAnimation({19913});
			tlist.Add(groupIconAnimation);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x000663D4 File Offset: 0x000645D4
		// Note: this type is marked as 'beforefieldinit'.
		static {19891}()
		{
			Dictionary<ShipClass, Rectangle> dictionary = new Dictionary<ShipClass, Rectangle>();
			dictionary[ShipClass.Battleship] = new Rectangle(388, 876, 14, 14);
			dictionary[ShipClass.CargoShip] = new Rectangle(415, 876, 14, 14);
			dictionary[ShipClass.Destroyer] = new Rectangle(402, 876, 14, 14);
			dictionary[ShipClass.Hardship] = new Rectangle(415, 876, 14, 14);
			dictionary[ShipClass.Mortar] = new Rectangle(455, 876, 14, 14);
			{19891}.shipClassIcons_green = dictionary;
			Dictionary<ShipClass, Rectangle> dictionary2 = new Dictionary<ShipClass, Rectangle>();
			dictionary2[ShipClass.Battleship] = new Rectangle(388, 896, 14, 14);
			dictionary2[ShipClass.CargoShip] = new Rectangle(415, 896, 14, 14);
			dictionary2[ShipClass.Destroyer] = new Rectangle(402, 896, 14, 14);
			dictionary2[ShipClass.Hardship] = new Rectangle(415, 896, 14, 14);
			dictionary2[ShipClass.Mortar] = new Rectangle(455, 896, 14, 14);
			{19891}.shipClassIcons_red = dictionary2;
			{19891}.targetNpcByQuest = new Rectangle(403, 952, 24, 24);
			{19891}.targetPoint = new Rectangle(428, 952, 24, 24);
			{19891}.targetPointGray = new Rectangle(378, 952, 24, 24);
			{19891}.blinking = new Rectangle(45, 846, 32, 32);
			{19891}.scrollTooltip = new Rectangle(2327, 862, 128, 128);
			{19891}.main = new Rectangle(2457, 859, 235, 235);
			{19891}.main_compas = new Rectangle(0, 153, 405, 404);
			{19891}.main_compas_clean = new Rectangle(406, 153, 405, 404);
			{19891}.main_top = new Rectangle(2693, 859, 165, 165);
			{19891}.main_top_directionline = new Rectangle(25, 802, 12, 186);
			{19891}.windDir = new Rectangle(2697, 1095, 20, 232);
			{19891}.enemyPortIcon = new Rectangle(2, 1983, 64, 64);
			{19891}.portIcon = new Rectangle(68, 1983, 64, 64);
			{19891}.playerDeathIcon = new Rectangle(134, 1983, 64, 64);
			{19891}.playerIcon = new Rectangle(200, 1983, 64, 64);
			{19891}.grayPlayerIcon = new Rectangle(266, 1983, 64, 64);
			{19891}.pharosIcon = new Rectangle(728, 1983, 130, 130);
			{19891}.traderIcon = new Rectangle(398, 1983, 64, 64);
			{19891}.isleIcon = new Rectangle(464, 1983, 64, 64);
			{19891}.capturedIsleIcon = new Rectangle(530, 1983, 64, 64);
			{19891}.mineIcon = new Rectangle(596, 1983, 64, 64);
			{19891}.capturedMineIcon = new Rectangle(662, 1983, 64, 64);
			{19891}.playerEnemyIcon = new Rectangle(2, 2049, 64, 64);
			{19891}.playerNeutralIcon = new Rectangle(68, 2049, 64, 64);
			{19891}.playerAllyIcon = new Rectangle(134, 2049, 64, 64);
			{19891}.npcEnemyIcon = new Rectangle(200, 2049, 64, 64);
			{19891}.npcNeutralIcon = new Rectangle(266, 2049, 64, 64);
			{19891}.npcAllyIcon = new Rectangle(727, 2115, 64, 64);
			{19891}.lootIsleIcon = new Rectangle(332, 2049, 64, 64);
			{19891}.fishIcon = new Rectangle(398, 2049, 64, 64);
			{19891}.whaleIcon = new Rectangle(464, 2049, 64, 64);
			{19891}.shipDropIcon = new Rectangle(530, 2049, 64, 64);
			{19891}.boxDropIcon = new Rectangle(596, 2049, 64, 64);
			{19891}.fishingShipIcon = new Rectangle(662, 2049, 64, 64);
			{19891}.orangeSquareIcon = new Rectangle(266, 2115, 64, 64);
			{19891}.orangeCircleIcon = new Rectangle(332, 2115, 64, 64);
			{19891}.pirateEvent = new Rectangle(283, 883, 37, 28);
			{19891}.specialPlaceIcon = new Rectangle(354, 832, 40, 40);
			{19891}.viewDirection = new Rectangle(1934, 442, 280, 280);
			{19891}.markerIcon = new Rectangle(374, 915, 29, 29);
			{19891}.markerDistanceBackground = new Rectangle(395, 849, 55, 23);
			{19891}.zone = new Rectangle(565, 875, 64, 64);
			{19891}.zoneShallow = new Rectangle(632, 875, 64, 64);
			{19891}.arenaRespGreen = new Rectangle(2562, 1468, 35, 34);
			{19891}.arenaRespRed = new Rectangle(2562, 1433, 35, 34);
			{19891}.arena_tower_red = new Rectangle(2, 2115, 64, 64);
			{19891}.arena_tower_red_destr = new Rectangle(68, 2115, 64, 64);
			{19891}.arena_tower_green = new Rectangle(134, 2115, 64, 64);
			{19891}.arena_tower_green_destr = new Rectangle(200, 2115, 64, 64);
			{19891}.arena_tower_focus_my = new Rectangle(524, 900, 32, 32);
			{19891}.arena_tower_focus_enemy = new Rectangle(524, 934, 32, 32);
			{19891}.red_tower_resp = new Rectangle(502, 916, 19, 19);
			{19891}.windIcon = new Rectangle(90, 885, 62, 50);
			{19891}.arenaZone = new Rectangle(2229, 1623, 180, 180);
			{19891}.arenaZonePoint = new Rectangle(2254, 1598, 24, 24);
			{19891}.p_arenaMapLayer = new Vector2(9f, 7f);
			{19891}.savedZoom = 7f;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00066A54 File Offset: 0x00064C54
		[CompilerGenerated]
		private void {19914}()
		{
			if (Global.Player.MapInfo.WindEnable && Global.Settings.WindArrowMode == 1)
			{
				float num = MathF.Atan2(CommonGlobal.CurrentClientWeather.WindDirection.Z, CommonGlobal.CurrentClientWeather.WindDirection.X);
				Vector2 vector = Geometry.RotateVector2(this.OccluderPos - CommonGlobal.CurrentClientWeather.WindDirection.XZ() * 900f, this.OccluderPos, this.{19953});
				Vector2 vector2 = Geometry.RotateVector2(this.OccluderPos + CommonGlobal.CurrentClientWeather.WindDirection.XZ() * 900f, this.OccluderPos, this.{19953});
				int num2;
				vector = this.{19895}(vector, out num2, true);
				vector2 = this.{19895}(vector2, out num2, true);
				double num3 = 4.0;
				float num4 = 0.3f;
				int num5 = 0;
				while ((double)num5 < num3)
				{
					float num6 = (float)((Global.Game.GameTotalTimeSec / 11.0 + (double)num5 / num3) % 1.0);
					Vector2 vector3 = Vector2.Lerp(vector, vector2, num6);
					float scale = Geometry.Saturate(num6 / num4) * Geometry.Saturate((1f - num6) / num4);
					Device gs = Engine.GS;
					Vector2 vector4 = {19891}.windIcon.WidthHeight() * 0.5f;
					float {14558} = this.{19953} + num;
					float {14559} = 1.6f;
					Color color = Color.Lerp(Global.Game.WorldInstance.LastWindColor, Color.SkyBlue, 0.3f) * scale;
					gs.Draw({19891}.windIcon, vector3, vector4, {14558}, {14559}, color);
					num5++;
				}
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00066C04 File Offset: 0x00064E04
		[CompilerGenerated]
		private void {19915}()
		{
			if (Session.LastDeathPosition != null)
			{
				this.{19900}(Session.LastDeathPosition.Value, {19891}.playerDeathIcon, 0f, Color.White, null, 0.4f, null, false, true, true);
			}
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00066C48 File Offset: 0x00064E48
		[CompilerGenerated]
		private void {19916}(float {19917})
		{
			float num = WosbVisibility.MinimapLootVisibleDistance * Global.Player.UsedShip.MinimapLootVisibleDistanceFactor;
			float num2 = WosbVisibility.MinimapIslesAndFishVisibleDistance * Global.Player.UsedShip.MinimapLootVisibleDistanceFactor;
			foreach (ClientDrop clientDrop in ((IEnumerable<ClientDrop>)Global.Game.WorldInstance.DropList))
			{
				Vector2 position = clientDrop.Position;
				if (clientDrop.VisibleOnMinimap && Vector2.DistanceSquared(position, Global.Player.Position) < num2 * num2)
				{
					if (clientDrop.ModelType == DropModel.Fishing)
					{
						this.{19900}(position, {19891}.fishIcon, 0f, this.{19954}, null, {19917} * 0.35f, null, false, true, true);
					}
					else if (clientDrop.ModelType == DropModel.Whale)
					{
						this.{19900}(position, {19891}.whaleIcon, 0f, this.{19954}, null, {19917} * 0.35f, null, false, true, true);
					}
					else if (clientDrop.ModelType == DropModel.IsleFarming)
					{
						this.{19900}(position, {19891}.lootIsleIcon, 0f, this.{19954}, null, {19917} * 0.33f, null, false, true, true);
					}
					else if (clientDrop.ModelType == DropModel.FishingShip)
					{
						this.{19900}(position, {19891}.fishingShipIcon, 0f, this.{19954}, null, {19917} * 0.34f, null, false, true, true);
					}
					else if (Vector2.DistanceSquared(position, Global.Player.Position) < num * num)
					{
						Rectangle rectangle = (clientDrop.Method == NDropInteropMethod.PickUp || clientDrop.Method == NDropInteropMethod.MiningByCrew) ? {19891}.boxDropIcon : {19891}.shipDropIcon;
						this.{19900}(position, rectangle, 0f, this.{19954}, null, {19917} * 0.35f, null, false, true, true);
					}
				}
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00066E20 File Offset: 0x00065020
		[CompilerGenerated]
		private void {19918}(float {19919}, float {19920})
		{
			foreach (Ship ship in Global.Game.WorldInstance.EnumerateShips(true, true, false))
			{
				Vector2 position = ship.Position;
				Vector2.DistanceSquared(position, Global.Player.Position);
				float num = {19919} * {19919};
				ShipNpc shipNpc = ship as ShipNpc;
				if (shipNpc != null)
				{
					if ((shipNpc.StatusColor != HealthBarStyle.Lime && shipNpc.UsedShip.IsInvisibilityBonusEnabled) || shipNpc.Client.IsCorusTransparentNow || shipNpc.MakeTransparentForMe)
					{
						continue;
					}
					Rectangle rectangle = (shipNpc.CurrentAgressionTargetUID == Global.Player.uID) ? {19891}.npcEnemyIcon : ((shipNpc.Client.StatusColor == HealthBarStyle.Lime) ? {19891}.npcAllyIcon : ((shipNpc.AgressionToCurrentPlayer != NpcAgression.No) ? {19891}.npcEnemyIcon : ((shipNpc.Client.StatusColor == HealthBarStyle.Red) ? {19891}.npcEnemyIcon : {19891}.npcNeutralIcon)));
					this.{19900}(position, rectangle, ship.Rotation + this.{19953}, this.{19954}, null, {19920}, null, false, true, true);
				}
				else
				{
					ShipOtherPlayer shipOtherPlayer = ship as ShipOtherPlayer;
					if (shipOtherPlayer != null)
					{
						if (shipOtherPlayer.UsedShip.IsInvisibilityBonusEnabled && shipOtherPlayer.Client.IsCorusTransparentNow && !Session.IsShipContainsPlayerGroup(shipOtherPlayer.uID))
						{
							continue;
						}
						Vector2 {19901} = position;
						Rectangle rectangle2 = (shipOtherPlayer.StatusColor == HealthBarStyle.Lime) ? {19891}.playerAllyIcon : ((shipOtherPlayer.StatusColor == HealthBarStyle.Red) ? {19891}.playerEnemyIcon : {19891}.playerNeutralIcon);
						this.{19900}({19901}, rectangle2, ship.Rotation + this.{19953}, this.{19954} * (shipOtherPlayer.MakeTransparentForMe ? 0.5f : 1f), null, {19920}, null, false, true, true);
					}
				}
				float blinkingMinimapEffect = ((IClientShip)ship).GetClient.BlinkingMinimapEffect;
				if (blinkingMinimapEffect > 0f)
				{
					this.{19900}(position, {19891}.blinking, ship.Rotation + this.{19953}, this.{19954} * blinkingMinimapEffect, null, {19920}, null, false, true, true);
				}
			}
			if (!Global.Game.StaticSystem.HasStrongFogEffect && !Global.Player.MapInfo.IsEnableArenaUi)
			{
				float num2 = WosbVisibility.ShipSilhouetteDistanceMinimap(Global.Player, Global.Game.StaticSystem.GetSkyShader.DayOrNight, CommonGlobal.CurrentClientWeather.FogLevelClient);
				using (IEnumerator<ShipSilhouettesManager.LocalObject> enumerator2 = ((IEnumerable<ShipSilhouettesManager.LocalObject>)Global.Game.WorldInstance.ShipSilhouettes.SceneObjects).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ShipSilhouettesManager.LocalObject pos = enumerator2.Current;
						if ((this.{19950} == null || !this.{19950}.Any((EnemyStateTransfer {19967}) => {19967}.ShipUID == pos.uID)) && (this.{19949} == null || !this.{19949}.Any((AllyStateTransfer {19968}) => {19968}.uID == pos.uID)))
						{
							Vector2 vector = pos.transform.Translation.XZ();
							float num3 = 1f - Geometry.InverseLerp(num2 * 0.6f, num2, Vector2.Distance(vector, Global.Player.Position));
							num3 = MathF.Sqrt(num3);
							this.{19900}(vector, {19891}.npcNeutralIcon, pos.transform.Yaw + this.{19953}, this.{19954} * num3, null, {19920}, null, false, true, true);
						}
					}
				}
			}
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x000671D0 File Offset: 0x000653D0
		[CompilerGenerated]
		private void {19921}(float {19922}, float {19923}, float {19924})
		{
			if (this.{19949} != null)
			{
				foreach (AllyStateTransfer allyStateTransfer in ((IEnumerable<AllyStateTransfer>)this.{19949}))
				{
					if (allyStateTransfer.IsOneMap && allyStateTransfer.Health > 0)
					{
						Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID(allyStateTransfer.uID);
						if (shipFromUID == null || Vector2.DistanceSquared(shipFromUID.Position, Global.Player.Position) >= {19922} * {19922})
						{
							CompressedShipPositionInfo position = allyStateTransfer.Position;
							Vector2 position2 = position.Position;
							position = allyStateTransfer.Position;
							this.{19900}(position2, {19891}.playerAllyIcon, position.Rotation + this.{19953}, this.{19954}, null, {19923}, null, true, true, true);
						}
					}
				}
			}
			if (this.{19950} != null)
			{
				foreach (EnemyStateTransfer enemyStateTransfer in ((IEnumerable<EnemyStateTransfer>)this.{19950}))
				{
					if (enemyStateTransfer.Icon == EnemyIcon.ArenaEnemyLoot)
					{
						CompressedShipPositionInfo position = enemyStateTransfer.Position;
						this.{19900}(position.Position, {19891}.targetPointGray, this.{19953}, this.{19954}, null, {19924} * (Global.Player.MapInfo.IsWorldmap ? 1.1f : 0.9f), null, true, true, true);
					}
					else if (enemyStateTransfer.Icon == EnemyIcon.QuestPoint)
					{
						CompressedShipPositionInfo position = enemyStateTransfer.Position;
						this.{19900}(position.Position, {19891}.targetPoint, this.{19953}, this.{19954}, null, {19924} * (Global.Player.MapInfo.IsWorldmap ? 1.1f : 0.9f), null, true, true, true);
					}
					else
					{
						Ship shipFromUID2 = Global.Game.WorldInstance.GetShipFromUID(enemyStateTransfer.ShipUID);
						if (shipFromUID2 == null || Vector2.DistanceSquared(shipFromUID2.Position, Global.Player.Position) >= {19922} * {19922} || (enemyStateTransfer.Icon == EnemyIcon.QuestNpc && Global.Player.MapInfo.IsWorldmap) || enemyStateTransfer.Icon == EnemyIcon.QuestPoint)
						{
							CompressedShipPositionInfo position = enemyStateTransfer.Position;
							Vector2 position3 = position.Position;
							if (enemyStateTransfer.Icon == EnemyIcon.QuestNpc)
							{
								if (Global.Player.MapInfo.IsWorldmap)
								{
									this.{19900}(position3, {19891}.targetNpcByQuest, this.{19953}, this.{19954}, null, {19924} * 0.9f, null, true, true, true);
								}
								else
								{
									Vector2 {19901} = position3;
									position = enemyStateTransfer.Position;
									this.{19900}({19901}, {19891}.npcEnemyIcon, position.Rotation + this.{19953}, this.{19954}, null, {19923}, null, false, true, true);
								}
							}
							else
							{
								Vector2 {19901}2 = position3;
								position = enemyStateTransfer.Position;
								this.{19900}({19901}2, {19891}.playerEnemyIcon, position.Rotation + this.{19953}, this.{19954}, null, {19923}, null, false, true, true);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x000674D0 File Offset: 0x000656D0
		[CompilerGenerated]
		private void {19925}(float {19926}, float {19927})
		{
			foreach (ClientCannonBall clientCannonBall in ((IEnumerable<ClientCannonBall>)Global.Game.WorldInstance.CannonBalls))
			{
				Vector2 vector = clientCannonBall.Sphere.XZ();
				if (clientCannonBall.uID % 2 == 0 && Vector2.DistanceSquared(vector, Global.Player.Position) < {19926} * {19926})
				{
					float num = Geometry.Saturate(clientCannonBall.CurrentDistance / clientCannonBall.CurrentFunctionData.SightDistance);
					float scale = num * (1f - num) * 4f;
					if (clientCannonBall.CurrentFunctionData.SightDistance == 1f)
					{
						scale = 1f;
					}
					this.{19900}(vector, {19891}.arena_tower_red, 0f, this.{19954} * scale, null, {19927} * 0.09f, null, false, true, true);
				}
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x000675C0 File Offset: 0x000657C0
		[CompilerGenerated]
		private void {19928}()
		{
			foreach (ValueTuple<Isle, DynamicBuildCreatePacket, float> valueTuple in ((IEnumerable<ValueTuple<Isle, DynamicBuildCreatePacket, float>>)Global.Game.InteractiveWorldSystem.BuildingsCache))
			{
				Vector2 position = valueTuple.Item2.Position;
				IMPSerializable visualData = valueTuple.Item2.VisualData;
				if (visualData is GuildBuildingVisualData)
				{
					GuildBuildingVisualData guildBuildingVisualData = (GuildBuildingVisualData)visualData;
					if (Session.EngagingInPortBattle != PbsBatlleSide.None)
					{
						Vector2 {19901} = position;
						Rectangle rectangle = (Session.EngagingInPortBattle == PbsBatlleSide.Attacker ^ guildBuildingVisualData.StatusIcon == 5) ? ((valueTuple.Item2.NowStrength == 0f) ? {19891}.arena_tower_red_destr : {19891}.arena_tower_red) : ((valueTuple.Item2.NowStrength == 0f) ? {19891}.arena_tower_green_destr : {19891}.arena_tower_green);
						this.{19900}({19901}, rectangle, 0f, this.{19954}, null, 0.3f, null, false, true, true);
						if (valueTuple.Item3 < 10f)
						{
							Vector2 {19901}2 = position;
							rectangle = ((Session.EngagingInPortBattle == PbsBatlleSide.Attacker ^ guildBuildingVisualData.StatusIcon == 5) ? {19891}.arena_tower_focus_enemy : {19891}.arena_tower_focus_my);
							this.{19900}({19901}2, rectangle, 0f, this.{19954} * (0.5f + 0.5f * (float)Math.Sin(Global.Game.GameTotalTimeSec * 3.0)), null, 1f, null, false, true, true);
							continue;
						}
						continue;
					}
				}
				visualData = valueTuple.Item2.VisualData;
				if (visualData is ArenaOrPassingVisualData)
				{
					ArenaOrPassingVisualData arenaOrPassingVisualData = (ArenaOrPassingVisualData)visualData;
					if (Global.Player.MapInfo.IsPassingUi)
					{
						Vector2 {19901}3 = position;
						Rectangle rectangle = (valueTuple.Item2.NowStrength == 0f) ? {19891}.arena_tower_red_destr : {19891}.arena_tower_red;
						this.{19900}({19901}3, rectangle, 0f, Color.White, null, 0.3f, null, false, true, true);
					}
					else if (Global.Player.MapInfo.IsEnableArenaUi && Session.CurrentArenaSession != null && Session.CurrentArenaSession.ModeInfo.ModeEnum != ArenaMode.Collecting)
					{
						Rectangle rectangle2;
						if (arenaOrPassingVisualData.ArenaTeam == Session.CurrentArenaSession.MyTeamID)
						{
							if (valueTuple.Item2.NowStrength == 0f)
							{
								rectangle2 = {19891}.arena_tower_green_destr;
							}
							else
							{
								rectangle2 = {19891}.arena_tower_green;
							}
						}
						else if (valueTuple.Item2.NowStrength == 0f)
						{
							rectangle2 = {19891}.arena_tower_red_destr;
						}
						else
						{
							rectangle2 = {19891}.arena_tower_red;
						}
						this.{19900}(position, rectangle2, 0f, Color.White, null, 0.3f, null, false, true, true);
						if (valueTuple.Item3 < 10f)
						{
							Vector2 {19901}4 = position;
							Rectangle rectangle = (arenaOrPassingVisualData.ArenaTeam == Session.CurrentArenaSession.MyTeamID) ? {19891}.arena_tower_focus_my : {19891}.arena_tower_focus_enemy;
							this.{19900}({19901}4, rectangle, 0f, this.{19954} * (0.5f + 0.5f * (float)Math.Sin(Global.Game.GameTotalTimeSec * 3.0)), null, 1f, null, false, true, true);
						}
					}
				}
				else
				{
					visualData = valueTuple.Item2.VisualData;
					if (visualData is WorldFortVisualData)
					{
						WorldFortVisualData worldFortVisualData = (WorldFortVisualData)visualData;
						if (Session.Account.FogOfWar.IsOpened(valueTuple.Item2.Position, 1) && valueTuple.Item2.NowStrength > 0f)
						{
							this.{19900}(position, {22913}.c_fort_emire, 0f, this.{19954}, OtherTextures.WorldMapUiElements, 0.9f, null, false, true, true);
						}
					}
				}
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00067958 File Offset: 0x00065B58
		[CompilerGenerated]
		private void {19929}()
		{
			if (Session.EngagingInPortBattle != PbsBatlleSide.None)
			{
				Dictionary<PbsBatlleSide, PbsAvanpostShortInfo> pbRespawn = Session.EngagingInPortBattlePort.PbRespawn;
				this.{19900}(pbRespawn[PbsBatlleSide.Attacker].Center, {19891}.red_tower_resp, 0f, (Session.EngagingInPortBattle == PbsBatlleSide.Attacker) ? Color.Lime : Color.Red, null, 1f, null, false, true, true);
				this.{19900}(pbRespawn[PbsBatlleSide.Defender].Center, {19891}.red_tower_resp, 0f, (Session.EngagingInPortBattle == PbsBatlleSide.Defender) ? Color.Lime : Color.Red, null, 1f, null, false, true, true);
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x000679EC File Offset: 0x00065BEC
		[CompilerGenerated]
		private void {19930}()
		{
			if (Global.Player.MapInfo.IsEnableArenaUi && Session.CurrentArenaSession != null)
			{
				this.{19900}(Session.CurrentArenaSession.MyBasePosition, {19891}.arenaRespGreen, 0f, this.{19954}, null, 1f, null, false, true, true);
				this.{19900}(Session.CurrentArenaSession.EnemyBasePosition, {19891}.arenaRespRed, 0f, this.{19954}, null, 1f, null, false, true, true);
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00067A68 File Offset: 0x00065C68
		[CompilerGenerated]
		private void {19931}()
		{
			if ((!Global.Player.MapInfo.IsEducationMap || ({18593}.CurrentInstance != null && {18593}.CurrentInstance.IsEntryToPortTask)) && Global.Player.MapInfo.Ports.Size > 0)
			{
				bool flag = {18593}.CurrentInstance != null && {18593}.CurrentInstance.IsEntryToPortTask;
				for (int i = 0; i < Global.Player.MapInfo.Ports.Size; i++)
				{
					IslePortInfo islePortInfo = Global.Player.MapInfo.Ports.Array[i];
					if (Vector2.DistanceSquared(islePortInfo.EntryPos, Global.Player.Position) < 25000000f)
					{
						string text;
						bool flag2 = Session.Game.CanEnterNearPortWithFlags(Session.Account.WorldFlag, out text, islePortInfo);
						if (EducationHelper.MakeTransparentPortsOnMinimap && !Session.Account.ResourcesInPorts.PortHasResources(islePortInfo.PortID, true))
						{
							flag2 = false;
						}
						bool flag3 = islePortInfo == Global.Player.NearPort && Session.Game.NearPortRelation == Relation.Enemy;
						Rectangle rectangle = flag3 ? {19891}.enemyPortIcon : {19891}.portIcon;
						Color {19904} = flag ? (this.{19954} * (0.6f + 0.4f * (float)Math.Sin(Global.Game.GameTotalTimeSec * 4.0))) : (this.{19954} * (flag2 ? 1f : 0.3f));
						float {19906} = (flag3 ? 0.45f : 0.5f) * (Global.Player.MapInfo.IsEducationMap ? 1.2f : 1f);
						string {19907} = Global.Player.MapInfo.IsEducationMap ? string.Empty : islePortInfo.PortNameShort;
						this.{19900}(islePortInfo.EntryPos, rectangle, 0f, {19904}, null, {19906}, {19907}, flag, true, true);
						foreach (IslePortPharosInfo islePortPharosInfo in ((IEnumerable<IslePortPharosInfo>)islePortInfo.ReferencedPharosesTransformed))
						{
							if (Session.Account.FogOfWar.IsOpened(islePortPharosInfo.MapGlobalPosition, 1))
							{
								this.{19900}(islePortPharosInfo.MapGlobalPosition, {19891}.pharosIcon, 0f, this.{19954} * 0.8f, null, 0.3f, null, false, true, true);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00067CE0 File Offset: 0x00065EE0
		[CompilerGenerated]
		private void {19932}()
		{
			if ({18593}.CurrentInstance != null)
			{
				Vector2? followTaskPosition = {18593}.CurrentInstance.FollowTaskPosition;
				if (followTaskPosition != null)
				{
					this.{19900}(followTaskPosition.Value, {19891}.markerIcon, 0f, this.{19954}, null, 1f, null, false, true, true);
				}
			}
			if (Global.Player.MapInfo.IsWorldmap)
			{
				foreach (Vector3 vector in Gameplay.WorldHazardZones)
				{
					Vector2 xy = vector.XY;
					Vector2 vector2 = Global.Player.Position;
					float rotate = Geometry.GetRotate(xy, vector2);
					float num = 0.1f;
					for (int j = 0; j < 25; j++)
					{
						float rotate2 = rotate + ((float)j / 25f * num * 2f - num) * 6.2831855f;
						Vector2 {19896} = xy + Geometry.SubstructRotate(rotate2, vector.Z);
						Rectangle {11433} = {19891}.arenaZonePoint;
						int num2;
						Vector2 v = this.{19895}({19896}, out num2, true);
						if (num2 == 0)
						{
							if (this.{19953} != 0f)
							{
								v = Geometry.RotateVector2(v, this.{19955}, this.{19953});
							}
							Device gs = Engine.GS;
							vector2 = {11433}.HalfWidthHeight();
							float {14558} = (float)j;
							float {14559} = 0.6f;
							Color color = Color.White * 0.75f;
							gs.Draw({11433}, v, vector2, {14558}, {14559}, color);
						}
					}
				}
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00067E48 File Offset: 0x00066048
		[CompilerGenerated]
		private void {19933}()
		{
			EABehavior1 actionBehavior = Session.EventActionsPipeline.GetActionBehavior1(EActionCaterory.RandomPvpArena);
			if (actionBehavior == null)
			{
				return;
			}
			Vector2 position = actionBehavior.Position;
			int argumentInt = actionBehavior.ArgumentInt;
			int num = 16;
			int num2 = 360 / num;
			for (int i = 0; i < num; i++)
			{
				float rotate = (float)(num2 * i) * 3.1415927f / 180f;
				float num3 = 0.21428573f;
				Vector2 {19901} = position + Geometry.SubstructRotate(rotate, (float)argumentInt) * new Vector2(1f - 0.5f * num3, 1f + 0.5f * num3);
				this.{19900}({19901}, {19891}.arenaZonePoint, 0f, Color.Pink * 0.6f, null, 0.7f, null, false, true, true);
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00067F14 File Offset: 0x00066114
		[CompilerGenerated]
		private void {19934}()
		{
			foreach (TraderInSeaPlaceInfo traderInSeaPlaceInfo in ((IEnumerable<TraderInSeaPlaceInfo>)Global.Player.MapInfo.TradersInSea))
			{
				if (Vector2.DistanceSquared(traderInSeaPlaceInfo.Position, Global.Player.Position) < 3359672.8f && Session.Account.FogOfWar.IsOpened(traderInSeaPlaceInfo.Position, 1))
				{
					this.{19900}(traderInSeaPlaceInfo.Position, {19891}.traderIcon, 0f, this.{19954}, null, 0.32f, null, false, true, true);
				}
			}
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00067FC0 File Offset: 0x000661C0
		[CompilerGenerated]
		private void {19935}(float {19936})
		{
			using (IEnumerator<FactoryPlaceIsleInfo> enumerator = ((IEnumerable<FactoryPlaceIsleInfo>)Global.Player.MapInfo.Factories).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FactoryPlaceIsleInfo mine = enumerator.Current;
					if (Vector2.DistanceSquared(mine.GlobalPosition, Global.Player.Position) < 3359672.8f && Session.Account.FogOfWar.IsOpened(mine.GlobalPosition, 1) && Global.Player.MapInfo.IsInsideMap(mine.GlobalPosition, true) && !mine.IsHidden)
					{
						if (mine.Predefines.Array[0] == FactoryType.Temp_PersonalIsle)
						{
							bool flag = Session.Account.PersonalIsles.Data.Any((PersonalIsleStatus {19969}) => (int){19969}.PlaceIndex == mine.FcID && {19969}.AllowWorldTravel);
							Rectangle rectangle = flag ? {19891}.capturedIsleIcon : {19891}.isleIcon;
							if (flag || Vector2.DistanceSquared(mine.GlobalPosition, Global.Player.Position) < {19936} * {19936})
							{
								this.{19900}(mine.GlobalPosition, rectangle, 0f, this.{19954}, null, 0.4f, null, false, true, true);
							}
						}
						else
						{
							Rectangle rectangle2 = (Session.Account.Buildings.Get(mine.FcID) != null) ? {19891}.capturedMineIcon : {19891}.mineIcon;
							this.{19900}(mine.GlobalPosition, rectangle2, 0f, this.{19954}, null, 0.3f, null, false, true, true);
						}
					}
				}
			}
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00068184 File Offset: 0x00066384
		[CompilerGenerated]
		private void {19937}()
		{
			if (this.{19949} != null)
			{
				for (int i = 0; i < this.{19947}.Size; i++)
				{
					{19891}.GroupIconAnimation groupIconAnimation = this.{19947}.Array[i];
					foreach (AllyStateTransfer allyStateTransfer in ((IEnumerable<AllyStateTransfer>)this.{19949}))
					{
						if (allyStateTransfer.uID == groupIconAnimation.IconPositionIndex)
						{
							if (!allyStateTransfer.IsOneMap)
							{
								break;
							}
							CompressedShipPositionInfo position = allyStateTransfer.Position;
							int num;
							Vector2 {19962} = this.{19895}(position.Position, out num, true);
							if (num == 0)
							{
								groupIconAnimation.Render({19962});
								break;
							}
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0006823C File Offset: 0x0006643C
		[CompilerGenerated]
		private void {19938}()
		{
			if (Session.WorldMapMarkerPosition != null && Global.Player.MapInfo.IsWorldmap)
			{
				this.{19900}(Session.WorldMapMarkerPosition.Value, {19891}.markerIcon, 0f, Color.White, null, 0.8f, null, true, true, true);
				int num;
				Vector2 vector = this.{19895}(Session.WorldMapMarkerPosition.Value, out num, true);
				if (this.{19953} != 0f)
				{
					vector = Geometry.RotateVector2(vector, this.{19955}, this.{19953});
				}
				Vector2 vector2 = vector;
				Vector2 center = base.Pos.Center;
				Engine.GS.Line2D(new Rectangle(483, 2, 5, 1), center, vector2, new Color(250, 107, 117) * 0.5f, 3);
				if (Vector2.Distance(vector2, center) > base.Pos.WH.X * 0.3f)
				{
					Vector2 value = (center + vector2) * 0.5f;
					Device gs = Engine.GS;
					Vector2 vector3 = value - {19891}.markerDistanceBackground.HalfWidthHeight();
					gs.Draw({19891}.markerDistanceBackground, vector3);
					Engine.GS.SetFont(Fonts.Arial_10Bold);
					Device gs2 = Engine.GS;
					string {14610} = ((int)Vector2.Distance(Global.Player.Position, Session.WorldMapMarkerPosition.Value)).ToString();
					Color color = new Color(255, 174, 168);
					gs2.DrawStringCentered({14610}, value, color);
				}
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x000683BC File Offset: 0x000665BC
		[CompilerGenerated]
		private void {19939}()
		{
			Device gs = Engine.GS;
			Vector2 vector = {19891}.main_top.WidthHeight() * 0.51f;
			float {14558} = 0f;
			float {14559} = {19891}.UiScale * 1.15f;
			Color color = this.{19954} * (Global.Player.MapInfo.IsWorldmap ? 1f : 0.5f);
			gs.Draw({19891}.main_top, this.{19955}, vector, {14558}, {14559}, color);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00068430 File Offset: 0x00066630
		[CompilerGenerated]
		private void {19940}()
		{
			Rectangle {11433};
			if (!Global.Player.MapInfo.IsWorldmap)
			{
				{11433} = {19891}.main_compas_clean;
			}
			else
			{
				{11433} = {19891}.main_compas;
			}
			Device gs = Engine.GS;
			Vector2 vector = {11433}.HalfWidthHeight();
			gs.Draw({11433}, this.{19955}, vector, this.{19953}, {19891}.UiScale * 0.55f, this.{19954});
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00068490 File Offset: 0x00066690
		[CompilerGenerated]
		private void {19941}()
		{
			Device gs = Engine.GS;
			Vector2 vector = {19891}.main_compas.HalfWidthHeight() * 0.58f;
			gs.Draw({19891}.main, this.{19955}, vector, 0f, {19891}.UiScale * 0.8f, this.{19954});
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000684E0 File Offset: 0x000666E0
		[CompilerGenerated]
		private void {19942}()
		{
			float {19903} = this.isRotating ? 0f : Global.Player.Rotation;
			Vector2 occluderPos = this.OccluderPos;
			Rectangle rectangle = (Session.Account.WorldFlag.IsPeaceMode() || Global.Player.AllowEnteringPort) ? {19891}.playerIcon : {19891}.grayPlayerIcon;
			this.{19900}(occluderPos, rectangle, {19903}, this.{19954}, null, 0.4f, null, false, true, false);
			this.{19900}(this.OccluderPos, {19891}.viewDirection, Engine.GS.Camera.Rotation.Y - 1.5707964f + (this.isRotating ? (-Global.Player.Rotation) : 0f), this.{19954}, null, 0.7f, null, false, true, false);
			if (this.isRotating)
			{
				Rectangle rectangle2 = new Rectangle({19891}.main_top_directionline.X, {19891}.main_top_directionline.Y + 45, {19891}.main_top_directionline.Width, {19891}.main_top_directionline.Height - 45);
				Device gs = Engine.GS;
				Vector2 vector = base.Pos.Center - new Vector2((float)(rectangle2.Width / 2), (float)rectangle2.Height);
				gs.Draw(rectangle2, vector, this.{19954});
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00068624 File Offset: 0x00066824
		[CompilerGenerated]
		private void {19943}(Rectangle {19944}, Vector2 {19945}, float {19946})
		{
			if (Global.Player.MapInfo.IsCircularShape)
			{
				{19945} *= 0.96f;
			}
			Vector2 zero = Vector2.Zero;
			int num;
			Vector2 value = {20413}.WorldPosToRenderInternal(zero, out num, this.{19951}, false, 0.5f);
			Vector2 vector = new Vector2({19946}, 0f);
			float num2 = Vector2.Distance(value, {20413}.WorldPosToRenderInternal(vector, out num, this.{19951}, false, 0.5f));
			num2 *= 2f * this.{19948};
			int num3;
			Vector2 vector2 = this.{19895}({19945}, out num3, true);
			if (num3 == 2)
			{
				return;
			}
			if (this.{19953} != 0f)
			{
				vector2 = Geometry.RotateVector2(vector2, this.{19955}, this.{19953});
			}
			Marker marker = new Marker(vector2.X, vector2.Y, num2, num2);
			marker.XY -= marker.WH * 0.5f;
			Device gs = Engine.GS;
			zero = Vector2.Zero;
			float {14558} = 0f;
			float {14559} = marker.WH.X / (float){19944}.Width;
			Color color = Color.White * 0.8f;
			gs.Draw({19944}, marker.XY, zero, {14558}, {14559}, color);
		}

		// Token: 0x04000B74 RID: 2932
		private Tlist<{19891}.GroupIconAnimation> {19947};

		// Token: 0x04000B75 RID: 2933
		private static readonly Dictionary<ShipClass, Rectangle> shipClassIcons_green;

		// Token: 0x04000B76 RID: 2934
		private static readonly Dictionary<ShipClass, Rectangle> shipClassIcons_red;

		// Token: 0x04000B77 RID: 2935
		public static readonly Rectangle targetNpcByQuest;

		// Token: 0x04000B78 RID: 2936
		public static readonly Rectangle targetPoint;

		// Token: 0x04000B79 RID: 2937
		public static readonly Rectangle targetPointGray;

		// Token: 0x04000B7A RID: 2938
		public static readonly Rectangle blinking;

		// Token: 0x04000B7B RID: 2939
		public static readonly Rectangle scrollTooltip;

		// Token: 0x04000B7C RID: 2940
		public static readonly Rectangle main;

		// Token: 0x04000B7D RID: 2941
		public static readonly Rectangle main_compas;

		// Token: 0x04000B7E RID: 2942
		public static readonly Rectangle main_compas_clean;

		// Token: 0x04000B7F RID: 2943
		public static readonly Rectangle main_top;

		// Token: 0x04000B80 RID: 2944
		public static readonly Rectangle main_top_directionline;

		// Token: 0x04000B81 RID: 2945
		public static readonly Rectangle windDir;

		// Token: 0x04000B82 RID: 2946
		public static readonly Rectangle enemyPortIcon;

		// Token: 0x04000B83 RID: 2947
		public static readonly Rectangle portIcon;

		// Token: 0x04000B84 RID: 2948
		public static readonly Rectangle playerDeathIcon;

		// Token: 0x04000B85 RID: 2949
		public static readonly Rectangle playerIcon;

		// Token: 0x04000B86 RID: 2950
		public static readonly Rectangle grayPlayerIcon;

		// Token: 0x04000B87 RID: 2951
		public static readonly Rectangle pharosIcon;

		// Token: 0x04000B88 RID: 2952
		public static readonly Rectangle traderIcon;

		// Token: 0x04000B89 RID: 2953
		public static readonly Rectangle isleIcon;

		// Token: 0x04000B8A RID: 2954
		public static readonly Rectangle capturedIsleIcon;

		// Token: 0x04000B8B RID: 2955
		public static readonly Rectangle mineIcon;

		// Token: 0x04000B8C RID: 2956
		public static readonly Rectangle capturedMineIcon;

		// Token: 0x04000B8D RID: 2957
		public static readonly Rectangle playerEnemyIcon;

		// Token: 0x04000B8E RID: 2958
		public static readonly Rectangle playerNeutralIcon;

		// Token: 0x04000B8F RID: 2959
		public static readonly Rectangle playerAllyIcon;

		// Token: 0x04000B90 RID: 2960
		public static readonly Rectangle npcEnemyIcon;

		// Token: 0x04000B91 RID: 2961
		public static readonly Rectangle npcNeutralIcon;

		// Token: 0x04000B92 RID: 2962
		public static readonly Rectangle npcAllyIcon;

		// Token: 0x04000B93 RID: 2963
		public static readonly Rectangle lootIsleIcon;

		// Token: 0x04000B94 RID: 2964
		public static readonly Rectangle fishIcon;

		// Token: 0x04000B95 RID: 2965
		public static readonly Rectangle whaleIcon;

		// Token: 0x04000B96 RID: 2966
		public static readonly Rectangle shipDropIcon;

		// Token: 0x04000B97 RID: 2967
		public static readonly Rectangle boxDropIcon;

		// Token: 0x04000B98 RID: 2968
		public static readonly Rectangle fishingShipIcon;

		// Token: 0x04000B99 RID: 2969
		public static readonly Rectangle orangeSquareIcon;

		// Token: 0x04000B9A RID: 2970
		public static readonly Rectangle orangeCircleIcon;

		// Token: 0x04000B9B RID: 2971
		public static readonly Rectangle pirateEvent;

		// Token: 0x04000B9C RID: 2972
		public static readonly Rectangle specialPlaceIcon;

		// Token: 0x04000B9D RID: 2973
		public static readonly Rectangle viewDirection;

		// Token: 0x04000B9E RID: 2974
		public static readonly Rectangle markerIcon;

		// Token: 0x04000B9F RID: 2975
		public static readonly Rectangle markerDistanceBackground;

		// Token: 0x04000BA0 RID: 2976
		public static readonly Rectangle zone;

		// Token: 0x04000BA1 RID: 2977
		public static readonly Rectangle zoneShallow;

		// Token: 0x04000BA2 RID: 2978
		public static readonly Rectangle arenaRespGreen;

		// Token: 0x04000BA3 RID: 2979
		public static readonly Rectangle arenaRespRed;

		// Token: 0x04000BA4 RID: 2980
		public static readonly Rectangle arena_tower_red;

		// Token: 0x04000BA5 RID: 2981
		public static readonly Rectangle arena_tower_red_destr;

		// Token: 0x04000BA6 RID: 2982
		public static readonly Rectangle arena_tower_green;

		// Token: 0x04000BA7 RID: 2983
		public static readonly Rectangle arena_tower_green_destr;

		// Token: 0x04000BA8 RID: 2984
		public static readonly Rectangle arena_tower_focus_my;

		// Token: 0x04000BA9 RID: 2985
		public static readonly Rectangle arena_tower_focus_enemy;

		// Token: 0x04000BAA RID: 2986
		public static readonly Rectangle red_tower_resp;

		// Token: 0x04000BAB RID: 2987
		public static readonly Rectangle windIcon;

		// Token: 0x04000BAC RID: 2988
		public static readonly Rectangle arenaZone;

		// Token: 0x04000BAD RID: 2989
		public static readonly Rectangle arenaZonePoint;

		// Token: 0x04000BAE RID: 2990
		private static readonly Vector2 p_arenaMapLayer;

		// Token: 0x04000BAF RID: 2991
		private static float savedZoom;

		// Token: 0x04000BB0 RID: 2992
		private float {19948};

		// Token: 0x04000BB1 RID: 2993
		private Tlist<AllyStateTransfer> {19949};

		// Token: 0x04000BB2 RID: 2994
		private Tlist<EnemyStateTransfer> {19950};

		// Token: 0x04000BB3 RID: 2995
		private Marker {19951};

		// Token: 0x04000BB4 RID: 2996
		private float {19952};

		// Token: 0x04000BB5 RID: 2997
		private float {19953};

		// Token: 0x04000BB6 RID: 2998
		private Color {19954} = Color.White;

		// Token: 0x04000BB7 RID: 2999
		private Vector2 {19955};

		// Token: 0x04000BB8 RID: 3000
		private IslePortInfo {19956};

		// Token: 0x04000BB9 RID: 3001
		private float {19957};

		// Token: 0x04000BBA RID: 3002
		[CompilerGenerated]
		private Form {19958};

		// Token: 0x0200022C RID: 556
		private class GroupIconAnimation
		{
			// Token: 0x06000CA4 RID: 3236 RVA: 0x00068755 File Offset: 0x00066955
			public GroupIconAnimation(int {19960})
			{
				this.IconPositionIndex = {19960};
			}

			// Token: 0x06000CA5 RID: 3237 RVA: 0x00068764 File Offset: 0x00066964
			public bool Update(ref FrameTime {19961})
			{
				this.{19963} += {19961}.msElapsed;
				return this.{19963} > 1000f;
			}

			// Token: 0x06000CA6 RID: 3238 RVA: 0x00068788 File Offset: 0x00066988
			public void Render(Vector2 {19962})
			{
				float scale = MathF.Sqrt(1f - this.{19963} / 1000f);
				Color color = Color.White * scale;
				float {14559} = 0.2f + this.{19963} / 1000f * 1.8f;
				Vector2 vector;
				vector.X = (float)({19891}.GroupIconAnimation.c_animatedEffect.Width / 2);
				vector.Y = (float)({19891}.GroupIconAnimation.c_animatedEffect.Height / 2);
				Engine.GS.Draw({19891}.GroupIconAnimation.c_animatedEffect, {19962}, vector, 0f, {14559}, color);
			}

			// Token: 0x04000BBB RID: 3003
			private static readonly Rectangle c_animatedEffect = new Rectangle(405, 916, 32, 32);

			// Token: 0x04000BBC RID: 3004
			private const float maxTTL = 1000f;

			// Token: 0x04000BBD RID: 3005
			private float {19963};

			// Token: 0x04000BBE RID: 3006
			public readonly int IconPositionIndex;
		}
	}
}
