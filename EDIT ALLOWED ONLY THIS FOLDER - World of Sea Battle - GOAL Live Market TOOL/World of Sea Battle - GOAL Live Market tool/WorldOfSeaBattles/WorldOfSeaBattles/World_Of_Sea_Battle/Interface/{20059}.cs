using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200023B RID: 571
	internal sealed class {20059} : CustomUi
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x0006B0DF File Offset: 0x000692DF
		private float changeSpeedAnimationMax
		{
			get
			{
				return 300f;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0006B0E6 File Offset: 0x000692E6
		private float summaryHp
		{
			get
			{
				return Global.Player.UsedShip.FirstHP.Summary - Global.Player.UsedShip.FirstHP.TemporalStrength;
			}
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0006B114 File Offset: 0x00069314
		public {20059}()
		{
			Vector2 vector = new Vector2(5f, (float)(Engine.GS.UIArea.Height - {20059}.c_main.Height - 5));
			base..ctor(new Marker(ref vector, ref {20059}.c_main), {20059}.c_main, PositionAlignment.LeftUp, PositionAlignment.RightDown, Color.White, false);
			{20059}.CurrentInstance = this;
			this.AnimatedFocus = false;
			{20391}.WhenInit(this, "playerShipGui");
			this.{20095} = new Tlist<{20059}.HitscanArrow>(10);
			base.EvRemoveFromContainer += delegate()
			{
				{20059}.CurrentInstance = null;
			};
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0006B1B4 File Offset: 0x000693B4
		protected override void UserUpdate(ref FrameTime {20060})
		{
			if (Math.Abs(this.{20094} - this.summaryHp) > 0.1f)
			{
				this.{20093} = 500f;
			}
			this.{20094} = this.summaryHp;
			if (this.{20092} < this.summaryHp)
			{
				this.{20092} = this.summaryHp;
			}
			else if (this.{20093} == 0f)
			{
				Geometry.Evalute(ref this.{20092}, this.summaryHp, Global.Player.UsedShip.MaxHp * {20060}.secElapsed * 0.15f);
			}
			{20060}.EvaluteTimerMs(ref this.{20093});
			for (int i = 0; i < this.{20095}.Size; i++)
			{
				if (this.{20095}.Array[i].Update(ref {20060}))
				{
					this.{20095}.FastRemoveAt(i);
					i--;
				}
			}
			if (ShallowsDetection.FenceDetected)
			{
				this.{20081}(Global.Player.Rotation + 3.1415927f, {20059}.c_hitFence);
			}
			if (Global.Render.UiMode == InterfaceMode.Default)
			{
				this.TexturePath = {20059}.c_main;
				base.Opacity = 1f;
			}
			else
			{
				this.TexturePath = {20059}.c_main_clean;
			}
			if (this.{20097} != Global.Player.FirstController.LinearStateCode)
			{
				this.{20097} = Global.Player.FirstController.LinearStateCode;
				this.{20096} = this.changeSpeedAnimationMax;
			}
			{20060}.EvaluteTimerMs(ref this.{20096});
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0006B324 File Offset: 0x00069524
		private void {20061}(Vector2 {20062}, Rectangle {20063}, float {20064}, string {20065})
		{
			{20064} = Math.Min(1f, {20064});
			Marker marker = new Marker(ref {20062}, ref {20063});
			Marker marker2 = new Marker(ref {20062}, ref {20063}).SetWidth({20064} * (float){20063}.Width);
			Device gs = Engine.GS;
			Rectangle rectangle = new Rectangle({20063}.X, {20063}.Y, (int)marker2.WH.X, {20063}.Height);
			Rectangle rectangle2 = marker2.ToRect();
			Color white = Color.White;
			gs.Draw(rectangle, rectangle2, white);
			if ((({20064} < 0.2f) ? 1f : (({20064} > 0.8f) ? 0f : (({20064} - 0.2f) / 0.6f))) > 0f)
			{
				Device gs2 = Engine.GS;
				rectangle = new Rectangle({20059}.hpbar_top_red.X, {20059}.hpbar_top_red.Y, (int)marker2.WH.X, {20059}.hpbar_top_red.Height);
				rectangle2 = marker2.ToRect();
				white = Color.White;
				gs2.Draw(rectangle, rectangle2, white);
			}
			Engine.GS.SetFont(Fonts.Philosopher_14Bold);
			Device gs3 = Engine.GS;
			Vector2 vector = marker.Center + new Vector2(0f, 1f);
			white = Color.White;
			gs3.DrawStringCentered({20065}, vector, white);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0006B46C File Offset: 0x0006966C
		public static Vector2 DrawNotification(string {20066}, bool {20067}, float {20068} = 1f, Rectangle? {20069} = null)
		{
			Vector2 vector = new Vector2((float)(Engine.GS.UIArea.Width / 2 - {20059}.cNotif.Width / 2), (float)(Engine.GS.UIArea.Height - ((Global.Game.GetCurrentSceneName == GameSceneName.Port) ? 120 : 190)));
			Device gs = Engine.GS;
			Color color = Color.White * {20068};
			gs.Draw({20059}.cNotif, vector, color);
			Engine.GS.SetFont({20067} ? Fonts.Philosopher_16Bold : Fonts.Philosopher_16);
			Device gs2 = Engine.GS;
			Vector2 vector2 = new Vector2((float)(Engine.GS.UIArea.Width / 2), vector.Y + 42f);
			color = Color.White * {20068};
			gs2.DrawStringCentered({20066}, vector2, color);
			if ({20069} != null)
			{
				Vector2 vector3 = new Vector2((float)(Engine.GS.UIArea.Width / 2 - {20069}.Value.Width / 2), vector.Y - (float){20069}.Value.Height);
				Device gs3 = Engine.GS;
				Rectangle value = {20069}.Value;
				color = Color.White * {20068};
				gs3.Draw(value, vector3, color);
			}
			return vector;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0006B5A8 File Offset: 0x000697A8
		public static void DrawNotificationWithProgress(string {20070}, bool {20071}, float {20072}, bool {20073})
		{
			Vector2 value = {20059}.DrawNotification({20070}, {20071}, 1f, null);
			Device gs = Engine.GS;
			Vector2 vector = value + new Vector2(60f, -44f);
			gs.Draw({20059}.cNotifOverlight, vector);
			Device gs2 = Engine.GS;
			Rectangle rectangle = {20073} ? {20059}.cNotifProgressBackRed : {20059}.cNotifProgressBackYellow;
			vector = value + new Vector2(113f, 25f);
			gs2.Draw(rectangle, vector);
			Device gs3 = Engine.GS;
			vector = value + new Vector2(113f, 25f);
			gs3.DrawProgressbar({20059}.cNotifProgress, vector, {20072});
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0006B650 File Offset: 0x00069850
		protected override void UserFrontRender()
		{
			if (Global.Player.IsDestroyed)
			{
				return;
			}
			Vector2 vector2;
			Color color;
			if (Session.CurrentCrewJob != null && Global.Render.UiMode == InterfaceMode.Default)
			{
				{20059}.DrawNotificationWithProgress(Session.CurrentCrewJob.Text, false, Session.CurrentCrewJob.ProgressFactor, false);
				if ({22913}.CurrentInstance == null)
				{
					base.MoveToFrontLevel();
				}
			}
			else if (InGameSightUi.PowderKegSights.ToolTipVisibility > 0f)
			{
				float toolTipVisibility = InGameSightUi.PowderKegSights.ToolTipVisibility;
				float convertedTimig = InGameSightUi.PowderKegSights.ConvertedTimig;
				if (Session.SelectedPowderKegsInfo.isFirebrand)
				{
					bool flag;
					Ship ship = InGameSightUi.PowderKegSights.FirebrandTarget(out flag);
					if (flag)
					{
						{20059}.DrawNotification(Local.too_close, true, toolTipVisibility, null);
					}
					else if (ship != null)
					{
						if (!InGameSightUi.PowderKegSights.AllowToRunFirebrand)
						{
							{20059}.DrawNotification(Local.only_from_board, true, toolTipVisibility, null);
						}
						else
						{
							{20059}.DrawNotification(((IClientShip)Global.Game.InterestPoints.ShipInSight).GetClient.GetName2(), true, toolTipVisibility, null);
						}
					}
					else
					{
						{20059}.DrawNotification(Local.has_no_target, true, toolTipVisibility, null);
					}
				}
				else
				{
					Vector2 vector = {20059}.DrawNotification(Session.SelectedPowderKegsInfo.IsInvisibleMine ? Local.Sight_5(Math.Round((double)(convertedTimig / 60000f))) : (Local.Sight_6 + ((int)(convertedTimig / 1000f)).ToString() + Local.StringConstants_80), false, toolTipVisibility, null);
					if (!Session.SelectedPowderKegsInfo.IsInvisibleMine)
					{
						Engine.GS.SetFont(Fonts.Arial_10);
						Device gs = Engine.GS;
						string sight_ = Local.Sight_7;
						vector2 = new Vector2((float)(Engine.GS.UIArea.Width / 2), vector.Y + 64f);
						color = Color.LightGray * toolTipVisibility;
						gs.DrawStringCentered(sight_, vector2, color);
					}
				}
			}
			else if (InGameSightUi.MortarSights.IsActive)
			{
				{20059}.DrawNotification(Local.mortarSightTt, true, 1f, null);
			}
			else if (Global.Player.DestructByTiltAmount > 0f)
			{
				{20059}.DrawNotificationWithProgress(Local.PlayerShipGui_1, false, Global.Player.DestructByTiltAmount, true);
			}
			else if (Global.Game.SceneGame.MouseState == 0 && !string.IsNullOrEmpty(Global.Game.InteractiveWorldSystem.ActualInteropMessageString))
			{
				if (Global.Game.InteractiveWorldSystem.ActualInteropMessageString == "port")
				{
					if (Global.Player.AllowEnteringPort || Global.Game.InteractiveWorldSystem.ContainsLighthouseEnteringZone)
					{
						RTI rti = Session.ServerWorldStatus.NearPortMooringCharge.Value;
						string str = "[";
						string keyToString = Global.Settings.kb_Action.KeyToString;
						string str2 = "] ";
						string str4;
						if (rti.Value != 0)
						{
							string payCharge = Local.payCharge;
							string str3 = ": ";
							RTI rti2 = rti;
							str4 = payCharge + str3 + rti2.ToString();
						}
						else
						{
							str4 = Local.PlayerShipGui_7;
						}
						{20059}.DrawNotification(str + keyToString + str2 + str4, rti.Value > 0, 1f, null);
					}
					else if (Global.Player.GetBattleTimer > 0f)
					{
						{20059}.DrawNotification(Local.PlayerShipGui_8(Global.Player.GetBattleTimerSec), false, 0.5f, null);
					}
				}
				else if (Global.Game.InteractiveWorldSystem.InteropIsBoarding)
				{
					Rectangle value = new Rectangle(1586, 110, 67, 67);
					Vector2 vector3;
					if (Session.WReload.BoardingHookReloadingLeftSec > 0f)
					{
						vector3 = {20059}.DrawNotification(Global.Game.InteractiveWorldSystem.ActualInteropMessageString, true, 0.5f, new Rectangle?(value));
					}
					else
					{
						vector3 = {20059}.DrawNotification(Global.Game.InteractiveWorldSystem.ActualInteropMessageString, true, 1f, new Rectangle?(value));
					}
					if (Global.Game.InteractiveWorldSystem.BoardingModeUnitsCountProblem)
					{
						Engine.GS.SetFont(Fonts.Arial_10Bold);
						Device gs2 = Engine.GS;
						string boarding_problem_count = Local.boarding_problem_count;
						vector2 = new Vector2((float)(Engine.GS.UIArea.Width / 2), vector3.Y + 42f + 22f);
						color = new Color(224, 98, 113);
						gs2.DrawStringCenteredShadow(boarding_problem_count, vector2, color, 0.8f);
					}
				}
				else if (Global.Game.InteractiveWorldSystem.InteropIsWhaleHarpoon)
				{
					Vector2 vector4 = {20059}.DrawNotification(Global.Game.InteractiveWorldSystem.ActualInteropMessageString, true, (Session.WReload.BoardingHookReloadingLeftSec > 0f) ? 0.5f : 1f, null);
					if (Global.Game.InteractiveWorldSystem.WhaleNoHarpoonProblem)
					{
						Engine.GS.SetFont(Fonts.Arial_10Bold);
						Device gs3 = Engine.GS;
						string no_whale_harpoon = Local.no_whale_harpoon;
						vector2 = new Vector2((float)(Engine.GS.UIArea.Width / 2), vector4.Y + 42f + 22f);
						color = new Color(224, 98, 113);
						gs3.DrawStringCenteredShadow(no_whale_harpoon, vector2, color, 0.8f);
					}
				}
				else if (!string.IsNullOrEmpty(Global.Game.InteractiveWorldSystem.ShowArenaLootTip))
				{
					Vector2 vector5 = {20059}.DrawNotification(Global.Game.InteractiveWorldSystem.ActualInteropMessageString, true, 1f, null);
					Engine.GS.SetFont(Fonts.Arial_10Bold);
					Device gs4 = Engine.GS;
					string showArenaLootTip = Global.Game.InteractiveWorldSystem.ShowArenaLootTip;
					vector2 = new Vector2((float)(Engine.GS.UIArea.Width / 2), vector5.Y + 42f + 22f);
					color = new Color(224, 98, 113);
					gs4.DrawStringCenteredShadow(showArenaLootTip, vector2, color, 0.8f);
				}
				else
				{
					{20059}.DrawNotification(Global.Game.InteractiveWorldSystem.ActualInteropMessageString, false, 1f, null);
				}
			}
			Vector2 value2 = new Vector2(15f, 16f);
			Vector2 value3 = new Vector2(108f, 55f);
			Vector2 value4 = new Vector2(214f, 55f);
			int num = (int)((float){20059}.hpbar_main_top.Width * Geometry.Saturate(this.summaryHp / Global.Player.UsedShip.MaxHp));
			int {20090} = (int)((float){20059}.hpbar_main_top.Width * Geometry.Saturate((this.{20092} - this.summaryHp) / Global.Player.UsedShip.MaxHp));
			int num2 = (int)((float){20059}.hpbar_main_top.Width * Geometry.Saturate(Global.Player.UsedShip.FirstHP.TemporalStrength / Global.Player.UsedShip.MaxHp));
			Vector2 vector6 = value2 + base.Pos.XY;
			{20059}.<UserFrontRender>g__DrawBar|42_0(vector6, 0, num, {20059}.hpbar_main_top);
			{20059}.<UserFrontRender>g__DrawBar|42_0(vector6, num, num2, {20059}.hpbar_main_throttle);
			{20059}.<UserFrontRender>g__DrawBar|42_0(vector6, num + num2, {20090}, {20059}.hpbar_main_light);
			int num3 = (int)Math.Max(3f, Global.Player.UsedShip.MaxHp / 500f);
			float num4 = (float)({20059}.hpbar_main_top.Width / num3);
			for (int i = 1; i < num3; i++)
			{
				Device gs5 = Engine.GS;
				Rectangle rectangle = new Rectangle((int)(vector6.X + (float)i * num4 - (float)({20059}.hpbar_separator.Width / 2)), (int)vector6.Y, {20059}.hpbar_separator.Width, {20059}.hpbar_separator.Height);
				color = Color.White;
				gs5.Draw({20059}.hpbar_separator, rectangle, color);
			}
			int num5 = (int)Global.Player.UsedShip.MaxHp;
			int value5 = Math.Min(num5, (int)Math.Ceiling((double)Global.Player.UsedShip.FirstHP.Summary));
			string text;
			if (Global.Player.UsedShip.FirstHP.FloodingFactor <= 0f)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted<int>(value5);
				defaultInterpolatedStringHandler.AppendLiteral(" / ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(num5);
				text = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			else
			{
				text = "-";
			}
			string text2 = text;
			if (Global.Player.UsedShip.MaxHpWithoutTempEffects != Global.Player.UsedShip.MaxHp)
			{
				int num6 = (int)(Global.Player.UsedShip.MaxHp - Global.Player.UsedShip.MaxHpWithoutTempEffects);
				string text3 = num6.ToString() ?? "";
				if (num6 > 0)
				{
					text3 = "+" + text3;
				}
				text2 = text2 + " (" + text3 + ")";
			}
			Engine.GS.SetFont(Fonts.Philosopher_14Bold);
			Device gs6 = Engine.GS;
			string {14610} = text2;
			vector2 = vector6 + {20059}.hpbar_main_top.HalfWidthHeight() + new Vector2(0f, 2f);
			color = Color.White;
			gs6.DrawStringCentered({14610}, vector2, color);
			float firstSailHP = Global.Player.UsedShip.FirstSailHP;
			this.{20061}(base.Pos.XY + value3, {20059}.hpbar_sail_top, firstSailHP, ((int)Math.Round((double)(firstSailHP * 100f))).ToString() + "%");
			int count = Global.Player.UsedShip.Crew.Count;
			int crewPlaces = Global.Player.UsedShip.CrewPlaces;
			this.{20061}(base.Pos.XY + value4, {20059}.hpbar_crew_top, (float)count / (float)crewPlaces, count.ToString() + "/" + crewPlaces.ToString() + ((count > crewPlaces) ? "+" : ""));
			string text4 = MathF.Round(Global.Player.NowSpeed * PlayerShipInfo.Temp_displaySpeedRefactoring, (Math.Abs(Global.Player.physicsBody.PrevMaxSpeedTakingWindAndPaddles - Global.Player.NowSpeed) < 2f) ? 1 : 0).ToString();
			Engine.GS.SetFont(Fonts.Philosopher_18);
			Device gs7 = Engine.GS;
			string {14599} = text4;
			vector2 = base.Pos.XY + new Vector2(43f, 49f);
			color = Color.White * 0.8f;
			gs7.DrawString({14599}, vector2, color);
			float num7 = this.{20096} / this.changeSpeedAnimationMax;
			{20059}.DrawSailSpeedIcon(Global.Player, new Marker(8f, 37f, 34f, 47f).Offset(base.Pos.XY), 1f, false);
			Engine.GS.SetFont(Fonts.Philosopher_14);
			for (int j = 0; j < this.{20095}.Size; j++)
			{
				this.{20095}.Array[j].Render();
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0006C168 File Offset: 0x0006A368
		private void {20074}(Vector2 {20075}, Rectangle {20076}, string {20077}, bool {20078} = false)
		{
			Color value = Color.White;
			if ({20078})
			{
				value *= 0.75f + 0.25f * (float)Math.Sin(Global.Game.GameTotalTimeSec * 5.0);
			}
			Engine.GS.Draw({20076}, {20075}, value);
			Device gs = Engine.GS;
			Vector2 vector = {20075} + new Vector2(12f, 17f);
			Color skyBlue = Color.SkyBlue;
			gs.DrawString({20077}, vector, skyBlue);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0006C1E8 File Offset: 0x0006A3E8
		public void HitscanUpdate(Vector2 {20079}, Rectangle {20080})
		{
			Vector2 vector = Global.Player.Position - {20079};
			float {20082} = MathF.Atan2(vector.Y, vector.X);
			this.{20081}({20082}, {20080});
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0006C220 File Offset: 0x0006A420
		private void {20081}(float {20082}, Rectangle {20083})
		{
			foreach ({20059}.HitscanArrow hitscanArrow in ((IEnumerable<{20059}.HitscanArrow>)this.{20095}))
			{
				if (hitscanArrow.TexturePath.X == {20083}.X && hitscanArrow.TexturePath.Y == {20083}.Y && hitscanArrow.TryRefresh({20082}))
				{
					return;
				}
			}
			Tlist<{20059}.HitscanArrow> tlist = this.{20095};
			{20059}.HitscanArrow hitscanArrow2 = new {20059}.HitscanArrow({20082}, {20083});
			tlist.Add(hitscanArrow2);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0006C2AC File Offset: 0x0006A4AC
		public void PushSnowSprite()
		{
			int num = this.{20095}.Count(({20059}.HitscanArrow {20104}) => {20104}.TexturePath == {20059}.c_hitSnow);
			if (Rand.Chanse(1f / (1f + (float)num / 4f) * 100f))
			{
				Tlist<{20059}.HitscanArrow> tlist = this.{20095};
				{20059}.HitscanArrow hitscanArrow = new {20059}.HitscanArrow(Rand.Angle(), {20059}.c_hitSnow)
				{
					FixedPos = new Vector2?(Rand.NextVector2(0.1f, 0.9f) * Engine.GS.UIArea.WidthHeight())
				};
				tlist.Add(hitscanArrow);
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0006C350 File Offset: 0x0006A550
		public static void DrawSailSpeedIcon(Ship {20084}, Marker {20085}, float {20086} = 1f, bool {20087} = false)
		{
			Rectangle rectangle = {20087} ? new Rectangle(1523, 919, 24, 32) : new Rectangle(1523, 789, 48, 64);
			Rectangle rectangle2 = {20087} ? new Rectangle(1549, 919, 23, 32) : new Rectangle(1572, 789, 48, 64);
			Rectangle rectangle3 = {20087} ? new Rectangle(1574, 919, 24, 32) : new Rectangle(1621, 789, 48, 64);
			Rectangle rectangle4 = {20087} ? new Rectangle(1600, 919, 24, 32) : new Rectangle(1523, 854, 48, 64);
			Rectangle rectangle5 = {20087} ? new Rectangle(1626, 919, 23, 32) : new Rectangle(1572, 854, 48, 64);
			Rectangle rectangle6 = new Rectangle(1621, 854, 48, 64);
			Rectangle {11433} = new Rectangle(6, 6, 39, 53);
			Rectangle rectangle7;
			switch ({20084}.FirstController.LinearStateCode)
			{
			case 0:
				rectangle7 = rectangle2;
				break;
			case 1:
				rectangle7 = rectangle3;
				break;
			case 2:
				rectangle7 = rectangle4;
				break;
			case 3:
				rectangle7 = rectangle5;
				break;
			case 4:
				rectangle7 = rectangle;
				break;
			default:
				throw new NotSupportedException();
			}
			Rectangle {11433}2 = rectangle7;
			{20086} *= {20085}.WH.X / (float)rectangle.Width;
			Device gs = Engine.GS;
			Vector2 vector = {20085}.Center;
			Vector2 vector2 = {11433}2.HalfWidthHeight();
			gs.Draw({11433}2, vector, vector2, 0f, {20086});
			if (!{20087})
			{
				float num = {20084}.NowSpeed / ({20084}.MaxExtraMarchSpeed + {20084}.UsedShip.Speed);
				num = Geometry.Saturate(num);
				Color color = ({20084}.NowSpeed > {20084}.UsedShip.Speed) ? (({20084}.FirstController.AxisStateCode != 0) ? new Color(211, 198, 86) : new Color(151, 211, 99)) : new Color(165, 182, 255);
				num = Geometry.InverseLerp(0.05f, 0.95f, num);
				int num2 = (int)Math.Round((double)((float)rectangle6.Height * num));
				Device gs2 = Engine.GS;
				rectangle7 = new Rectangle(rectangle6.X, rectangle6.Y + rectangle6.Height - num2, rectangle6.Width, num2);
				vector = {20085}.Center + new Vector2(0f, (float)(rectangle6.Height - num2)) * {20086} + new Vector2(-3f, -4f);
				vector2 = {11433}.HalfWidthHeight();
				gs2.Draw(rectangle7, vector, vector2, 0f, {20086}, color);
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0006C88C File Offset: 0x0006AA8C
		[CompilerGenerated]
		internal static void <UserFrontRender>g__DrawBar|42_0(Vector2 {20088}, int {20089}, int {20090}, Rectangle {20091})
		{
			{20090} = Math.Min({20090}, {20091}.Width);
			Device gs = Engine.GS;
			Rectangle rectangle = new Rectangle({20091}.X + {20089}, {20091}.Y, {20090}, {20091}.Height);
			Rectangle rectangle2 = new Rectangle((int){20088}.X + {20089}, (int){20088}.Y, {20090}, {20091}.Height);
			Color white = Color.White;
			gs.Draw(rectangle, rectangle2, white);
		}

		// Token: 0x04000C24 RID: 3108
		public static {20059} CurrentInstance;

		// Token: 0x04000C25 RID: 3109
		public static Rectangle cNotif = new Rectangle(278, 1390, 441, 99);

		// Token: 0x04000C26 RID: 3110
		public static Rectangle c_hitRed = new Rectangle(2220, 582, 96, 74);

		// Token: 0x04000C27 RID: 3111
		public static Rectangle c_hitFence = new Rectangle(2317, 567, 115, 89);

		// Token: 0x04000C28 RID: 3112
		public static Rectangle c_hitSnow = new Rectangle(3559, 1083, 240, 234);

		// Token: 0x04000C29 RID: 3113
		public static Rectangle c_hitWarning = new Rectangle(2215, 505, 92, 74);

		// Token: 0x04000C2A RID: 3114
		private static readonly Rectangle c_main = new Rectangle(1210, 605, 312, 89);

		// Token: 0x04000C2B RID: 3115
		private static readonly Rectangle c_main_clean = new Rectangle(1428, 984, 312, 89);

		// Token: 0x04000C2C RID: 3116
		private static readonly Rectangle sail_blue = new Rectangle(1491, 695, 31, 37);

		// Token: 0x04000C2D RID: 3117
		private static readonly Rectangle sail_march = new Rectangle(1459, 695, 31, 37);

		// Token: 0x04000C2E RID: 3118
		private static readonly Rectangle sail_march_seamlesssea = new Rectangle(1089, 832, 31, 37);

		// Token: 0x04000C2F RID: 3119
		private static readonly Rectangle sail_red = new Rectangle(1427, 695, 31, 37);

		// Token: 0x04000C30 RID: 3120
		private static readonly Rectangle hpbar_separator = new Rectangle(1550, 582, 8, 18);

		// Token: 0x04000C31 RID: 3121
		private static readonly Rectangle hpbar_main_top = new Rectangle(1523, 605, 277, 18);

		// Token: 0x04000C32 RID: 3122
		private static readonly Rectangle hpbar_main_light = new Rectangle(1523, 654, 277, 18);

		// Token: 0x04000C33 RID: 3123
		private static readonly Rectangle hpbar_main_throttle = new Rectangle(1523, 673, 277, 18);

		// Token: 0x04000C34 RID: 3124
		private static readonly Rectangle hpbar_sail_top = new Rectangle(1523, 624, 78, 14);

		// Token: 0x04000C35 RID: 3125
		private static readonly Rectangle hpbar_crew_top = new Rectangle(1523, 639, 78, 14);

		// Token: 0x04000C36 RID: 3126
		private static readonly Rectangle hpbar_top_red = new Rectangle(1602, 624, 78, 14);

		// Token: 0x04000C37 RID: 3127
		private static readonly Rectangle cNotifProgressBackRed = new Rectangle(1211, 116, 210, 6);

		// Token: 0x04000C38 RID: 3128
		private static readonly Rectangle cNotifProgressBackYellow = new Rectangle(1211, 104, 210, 6);

		// Token: 0x04000C39 RID: 3129
		private static readonly Rectangle cNotifProgress = new Rectangle(1211, 110, 210, 6);

		// Token: 0x04000C3A RID: 3130
		private static readonly Rectangle cNotifOverlight = new Rectangle(708, 819, 321, 143);

		// Token: 0x04000C3B RID: 3131
		private static readonly Rectangle c_portInfo = new Rectangle(345, 94, 283, 57);

		// Token: 0x04000C3C RID: 3132
		private static readonly Rectangle c_portInfoAnch = new Rectangle(815, 388, 283, 57);

		// Token: 0x04000C3D RID: 3133
		private float {20092};

		// Token: 0x04000C3E RID: 3134
		private float {20093};

		// Token: 0x04000C3F RID: 3135
		private float {20094};

		// Token: 0x04000C40 RID: 3136
		private Tlist<{20059}.HitscanArrow> {20095};

		// Token: 0x04000C41 RID: 3137
		private float {20096};

		// Token: 0x04000C42 RID: 3138
		private int {20097};

		// Token: 0x0200023C RID: 572
		private class HitscanArrow
		{
			// Token: 0x06000D03 RID: 3331 RVA: 0x0006C8F5 File Offset: 0x0006AAF5
			public HitscanArrow(float {20100}, Rectangle {20101})
			{
				this.Intensity = 1f;
				this.SourceAxis = {20100};
				this.TexturePath = {20101};
			}

			// Token: 0x06000D04 RID: 3332 RVA: 0x0006C916 File Offset: 0x0006AB16
			public bool TryRefresh(float {20102})
			{
				if (Geometry.AxisDistance(this.SourceAxis, {20102}) < MathHelper.ToRadians(20f))
				{
					this.Intensity = 1f;
					return true;
				}
				return false;
			}

			// Token: 0x06000D05 RID: 3333 RVA: 0x0006C93E File Offset: 0x0006AB3E
			public bool Update(ref FrameTime {20103})
			{
				this.Intensity -= {20103}.secElapsed / 2f;
				return this.Intensity <= 0f;
			}

			// Token: 0x06000D06 RID: 3334 RVA: 0x0006C96C File Offset: 0x0006AB6C
			public void Render()
			{
				float num = (this.FixedPos != null) ? 1.25f : 0.9f;
				float num2 = this.SourceAxis - Engine.GS.Camera.Rotation.Y + 1.5707964f;
				float distance = 150f * num;
				Vector2 vector = Geometry.SubstructRotate(num2 + 1.5707964f, distance);
				Vector2 {11447} = new Vector2(0f, -1f);
				Vector2 defaultValue = Engine.GS.UIArea.HalfWidthHeightInt() + vector;
				float num3 = (this.FixedPos != null) ? 0.9f : Geometry.Saturate(-2f * Vector2.Dot(vector.Normal(), {11447}.Normal()));
				Device gs = Engine.GS;
				Vector2 valueOrDefault = this.FixedPos.GetValueOrDefault(defaultValue);
				Vector2 vector2 = this.TexturePath.WidthHeight() / 2f * num;
				float {14558} = (this.FixedPos == null) ? num2 : this.SourceAxis;
				float {14559} = num * 1.4f;
				Color color = Color.White * (this.Intensity * num3);
				gs.Draw(this.TexturePath, valueOrDefault, vector2, {14558}, {14559}, color);
			}

			// Token: 0x04000C43 RID: 3139
			public float SourceAxis;

			// Token: 0x04000C44 RID: 3140
			public float Intensity;

			// Token: 0x04000C45 RID: 3141
			public Rectangle TexturePath;

			// Token: 0x04000C46 RID: 3142
			public Vector2? FixedPos;
		}
	}
}
