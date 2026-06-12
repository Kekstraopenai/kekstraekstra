using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using ManualPacketSerialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Assets.Graphics;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics.Effects;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Graphics
{
	// Token: 0x0200045A RID: 1114
	internal class TargetInfoRenderer
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x000D1CEF File Offset: 0x000CFEEF
		private static Color TextColor
		{
			get
			{
				return new Color(234, 219, 171) * 0.9f;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x000D1D0F File Offset: 0x000CFF0F
		private static float ValueYOffset
		{
			get
			{
				return 25f;
			}
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x000D1D18 File Offset: 0x000CFF18
		public void Draw(bool {23562})
		{
			Ship shipInSight = Global.Game.InterestPoints.ShipInSight;
			BuildingTarget? objectAtSight = Global.Game.InterestPoints.ObjectAtSight;
			SpyglassUi.Render2DMarkers();
			if ({23562})
			{
				this.SpyglassDrawExtraInfo(shipInSight, objectAtSight);
				return;
			}
			this.{23613} = new Marker((float)(Engine.GS.UIArea.Width / 2 - TargetInfoRenderer.c_base.Width / 2), 2f, ref TargetInfoRenderer.c_base);
			Vector2 vector = this.{23613}.Offset(0f, 26f).XY;
			if (shipInSight != null)
			{
				vector += new Vector2(15f, 0f);
				Engine.GS.SetFont(Fonts.Arial_12);
				Color value = Color.White;
				if (((IClientShip)shipInSight).MakeTransparentForMe)
				{
					value *= 0.77f;
				}
				this.{23571}(0.92f, TargetInfoRenderer.c_base, 0f, value);
				this.RenderShipStats(vector, shipInSight, {23562});
				return;
			}
			if (objectAtSight != null)
			{
				vector += new Vector2(60f, 0f);
				Engine.GS.SetFont(Fonts.Arial_12);
				float {23572} = 0.92f;
				Rectangle {23573} = TargetInfoRenderer.c_base;
				float {23574} = 0f;
				Color white = Color.White;
				this.{23571}({23572}, {23573}, {23574}, white);
				this.RenderBuildingStats(vector, objectAtSight.Value);
			}
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x000D1E6C File Offset: 0x000D006C
		[NullableContext(2)]
		public void SpyglassDrawExtraInfo(Ship {23563}, BuildingTarget? {23564})
		{
			if ({23563} != null)
			{
				string item = TargetInfoRenderer.GetShipName({23563}).Item1;
				Rectangle value = Rectangle.Empty;
				ImageDecription? {23554} = null;
				string {23553} = string.Empty;
				ShipNpc shipNpc = {23563} as ShipNpc;
				if (shipNpc != null)
				{
					if (shipNpc.IsPlayerCaper)
					{
						{23553} = Local.npc_Captured(string.Empty);
					}
					else
					{
						value = AtlasObjs.GetNpcIcon(shipNpc.UsedShipNpc.Information.Extras.Icon);
						{23553} = shipNpc.UsedShipNpc.Information.Extras.Description(new FractionID?(shipNpc.Client.Guild.Fraction));
						VirtualTexture npcFlag = LocalContent.GetNpcFlag(shipNpc);
						if (npcFlag != null)
						{
							{23554} = new ImageDecription?(new ImageDecription(npcFlag.Tex, npcFlag.Tex.Bounds));
						}
					}
				}
				else
				{
					ShipOtherPlayer shipOtherPlayer = {23563} as ShipOtherPlayer;
					if (shipOtherPlayer != null)
					{
						RemotePlayerDynamicInfo remotePlayerDynamicInfo = shipOtherPlayer.UsedShipPlayer as RemotePlayerDynamicInfo;
						{23553} = (shipOtherPlayer.MakeTransparentForMe ? Local.cannot_attack : string.Empty);
						VirtualTexture albedo = LocalContent.WorldFlagTexture(remotePlayerDynamicInfo.Flags, remotePlayerDynamicInfo.FlagsTensityMode, shipOtherPlayer.Client.Guild.Fraction, false, false, false).Albedo;
						Rectangle bounds = albedo.Tex.Bounds;
						{23554} = new ImageDecription?(new ImageDecription(albedo.Tex, bounds.SetWidth((float)bounds.Width * 0.4f)));
					}
				}
				GSI {23556};
				bool {23557};
				this.{23578}(out {23556}, out {23557});
				SpyglassUi.RenderInfoPanel(item, {23553}, {23554}, new Rectangle?(value), {23556}, {23557});
				return;
			}
			if ({23564} != null)
			{
				BuildingTarget value2 = {23564}.Value;
				ValueTuple<string, string> text = value2.GetText();
				string item2 = text.Item1;
				string item3 = text.Item2;
				ImageDecription? {23554}2 = null;
				value2 = {23564}.Value;
				if (value2.EnvironmentIcon != null)
				{
					value2 = {23564}.Value;
					Texture2D {13217} = value2.EnvironmentIcon.Value.CustomTex ?? AtlasObjs.Texture.Tex;
					value2 = {23564}.Value;
					{23554}2 = new ImageDecription?(new ImageDecription({13217}, value2.EnvironmentIcon.Value.Path));
				}
				else
				{
					value2 = {23564}.Value;
					IMPSerializable impserializable = (value2.AsDynamic != null) ? value2.AsDynamic.GetValueOrDefault().VisualData : null;
					if (impserializable is GuildBuildingVisualData)
					{
						GuildBuildingVisualData guildBuildingVisualData = (GuildBuildingVisualData)impserializable;
						PortCaptureStatus byPortId = Session.Game.WorldPorts.GetByPortId((int)guildBuildingVisualData.PortID);
						FractionID fractionID = (byPortId != null) ? byPortId.CapturerFraction : FractionID.None;
						if (fractionID != FractionID.None)
						{
							ShipDesignInfo shipDesignInfo;
							ShipDesignInfo shipDesignInfo2;
							LocalContent.GetFractionGeraldics(fractionID, out shipDesignInfo, out shipDesignInfo2);
							if (shipDesignInfo != null)
							{
								{23554}2 = new ImageDecription?(new ImageDecription(DesignElementExtenstions.DesignElementTexture(shipDesignInfo).Tex));
							}
						}
					}
				}
				GSI {23556}2;
				bool {23557}2;
				this.{23578}(out {23556}2, out {23557}2);
				SpyglassUi.RenderInfoPanel(item2, item3, {23554}2, null, {23556}2, {23557}2);
			}
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x000D2154 File Offset: 0x000D0354
		public void RenderShipStats(Vector2 {23565}, Ship {23566}, bool {23567})
		{
			Vector2 vector = {23565};
			float num = Vector2.Distance(Global.Player.Position, {23566}.Position);
			Vector2 value = (Global.Player.Position - {23566}.Position).Normal();
			float num2 = MathHelper.Lerp(Global.Player.UsedShip.StaticInfo.CorpusHalfWidth, Global.Player.UsedShip.StaticInfo.CorpusHalfLength, MathF.Pow(Math.Abs(Vector2.Dot(value, Global.Player.FastNormal)), 2f));
			float num3 = MathHelper.Lerp({23566}.UsedShip.StaticInfo.CorpusHalfWidth, {23566}.UsedShip.StaticInfo.CorpusHalfLength, MathF.Pow(Math.Abs(Vector2.Dot(value, {23566}.FastNormal)), 2f));
			float num4 = Math.Max(1f, num - (num2 + num3) * 0.9f);
			bool availableForBoardingByPlayer = ((IClientShip){23566}).GetClient.AvailableForBoardingByPlayer;
			float num5 = 1f;
			float num6 = 0f;
			CannonLocation? lastActiveNearBoard = InGameSightUi.CannonSights.LastActiveNearBoard;
			if (lastActiveNearBoard != null)
			{
				foreach (CannonCommon cannonCommon in ((IEnumerable<CannonCommon>)Global.Player.UsedShip.Cannons.Items))
				{
					CannonLocation side = cannonCommon.Location.Side;
					CannonLocation? cannonLocation = lastActiveNearBoard;
					if (side == cannonLocation.GetValueOrDefault() & cannonLocation != null)
					{
						num6 = Math.Max(num6, cannonCommon.GameInfo.MaxDistance);
					}
				}
				num6 = num6 * Gameplay.BallsInfo.FromID(Global.Settings.SelectedCannonBalls[lastActiveNearBoard.Value]).DistanceFactor + Global.Player.UsedShip.BallDistanceBonusValue;
			}
			bool {23590} = num6 > 0f && num4 > num6 + 2f;
			TargetInfoRenderer.RenderDistance(ref {23565}, TargetInfoRenderer.c_spyglass, num4, {23590}, num5);
			if (Global.Game.InterestPoints.CurrentTargetResearchLevel >= 1f)
			{
				if (!{23567})
				{
					ValueTuple<string, string> shipName = TargetInfoRenderer.GetShipName({23566});
					string item = shipName.Item1;
					string item2 = shipName.Item2;
					this.{23584}(item, item2);
				}
				TargetInfoRenderer.RenderShield(ref {23565}, TargetInfoRenderer.c_shield, {23566}, num5);
				string text = {23566}.UsedShip.MakeCrewInvisible ? "?" : {23566}.UsedShip.Crew.Count.ToString();
				string text2 = {23566}.UsedShip.CrewPlaces.ToString();
				Rectangle {23597} = TargetInfoRenderer.c_crew;
				string {23598} = text;
				ShipNpc shipNpc = {23566} as ShipNpc;
				string {23599};
				if (shipNpc != null)
				{
					NpcShipDynamicInfo usedShipNpc = shipNpc.UsedShipNpc;
					if (usedShipNpc != null)
					{
						NpcInfo information = usedShipNpc.Information;
						if (information != null && information.Extras.HideMaxCrewCount)
						{
							{23599} = string.Empty;
							goto IL_2CB;
						}
					}
				}
				{23599} = text2;
				IL_2CB:
				TargetInfoRenderer.RenderCrew(ref {23565}, {23597}, {23598}, {23599}, num5);
				TargetInfoRenderer.RenderSail(ref {23565}, TargetInfoRenderer.c_sail, {23566}, num5);
				if (availableForBoardingByPlayer)
				{
					Vector2 vector2 = vector;
					vector2.X -= 40f;
					TargetInfoRenderer.RenderBoarding(ref vector2, TargetInfoRenderer.c_boarding, num5);
				}
				GSI {23582};
				bool {23583};
				if (!{23567} && this.{23578}(out {23582}, out {23583}))
				{
					this.{23581}({23582}, {23583});
					return;
				}
			}
			else
			{
				this.{23568}(vector);
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x000D2498 File Offset: 0x000D0698
		private void {23568}(Vector2 {23569})
		{
			float num = 1.3f;
			float {11438} = Global.Game.InterestPoints.CurrentTargetResearchLevel * (float)TargetInfoRenderer.c_preparationProgress.Width;
			Vector2 vector = {23569} + new Vector2((float)(-25 + TargetInfoRenderer.c_base.Width) - (float)TargetInfoRenderer.c_preparationProgress.Width * num, -10f);
			Device gs = Engine.GS;
			Rectangle rectangle = TargetInfoRenderer.c_preparationProgress.SetWidth({11438});
			Vector2 zero = Vector2.Zero;
			gs.Draw(rectangle, vector, zero, 0f, num);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x000D2520 File Offset: 0x000D0720
		[return: TupleElementNames(new string[]
		{
			"name",
			"extra"
		})]
		private static ValueTuple<string, string> GetShipName(Ship {23570})
		{
			string name2AndTag = ((IClientShip){23570}).GetClient.GetName2AndTag();
			string text = "";
			bool flag = Vector2.Distance(Global.Player.Position, {23570}.Position) < WosbVisibility.VisibleDistanceNicknames(Global.Player);
			return new ValueTuple<string, string>(flag ? name2AndTag : "?", flag ? text : string.Empty);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x000D2584 File Offset: 0x000D0784
		private Vector2 {23571}(float {23572}, Rectangle {23573}, float {23574}, in Color {23575})
		{
			Vector2 result = this.{23613}.XY + new Vector2(0f, {23574});
			Device gs = Engine.GS;
			Vector2 vector = new Vector2((float)(-(float){23573}.Width / 2) * (1f - {23572}), 0f);
			gs.Draw({23573}, result, vector, 0f, {23572}, {23575});
			return result;
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x000D25E4 File Offset: 0x000D07E4
		public void RenderBuildingStats(Vector2 {23576}, BuildingTarget {23577})
		{
			Vector2 {23569} = {23576};
			float num = Vector2.Distance(Global.Player.Position, {23577}.Center);
			float {23589} = Math.Max(1f, num - Global.Player.UsedShip.StaticInfo.CorpusHalfWidth - {23577}.Radius * 0.66f);
			if (!Global.Camera.IsSpyglass)
			{
				this.{23584}({23577}.GetText().Item1, null);
			}
			TargetInfoRenderer.RenderDistance(ref {23576}, TargetInfoRenderer.c_spyglass, {23589}, false, 1.2f);
			if (Global.Game.InterestPoints.CurrentTargetResearchLevel >= 1f || !Global.Camera.IsSpyglass)
			{
				if ({23577}.AsDynamic != null)
				{
					DynamicBuildCreatePacket value = {23577}.AsDynamic.Value;
					string {23598} = (value.UnitsCount > 0) ? value.UnitsCount.ToString() : "-";
					string {23599} = (value.MaxUnitsCount > 0) ? value.MaxUnitsCount.ToString() : "";
					TargetInfoRenderer.RenderCrew(ref {23576}, TargetInfoRenderer.c_crew, {23598}, {23599}, 1.2f);
				}
				GSI {23582};
				bool flag;
				if (!Global.Camera.IsSpyglass && this.{23578}(out {23582}, out flag))
				{
					this.{23581}({23582}, false);
					return;
				}
			}
			else
			{
				this.{23568}({23569});
			}
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x000D2728 File Offset: 0x000D0928
		private bool {23578}(out GSI {23579}, out bool {23580})
		{
			Ship shipInSight = Global.Game.InterestPoints.ShipInSight;
			BuildingTarget? objectAtSight = Global.Game.InterestPoints.ObjectAtSight;
			if (shipInSight != null)
			{
				IClientShip clientShip = shipInSight as IClientShip;
				if (clientShip != null && (shipInSight is Npc || Session.Account.CaptainSkills[PDynamicAccountBonus.BShowHoldFill] > 0))
				{
					{23579} = clientShip.GetClient.ItemsInHoldExemplary;
					{23580} = false;
					return true;
				}
			}
			IMPSerializable impserializable;
			if (objectAtSight == null)
			{
				impserializable = null;
			}
			else
			{
				ref BuildingTarget ptr = ref objectAtSight.GetValueOrDefault();
				impserializable = ((ptr.AsDynamic != null) ? ptr.AsDynamic.GetValueOrDefault().VisualData : null);
			}
			IMPSerializable impserializable2 = impserializable;
			if (impserializable2 is WorldFortVisualData)
			{
				WorldFortVisualData worldFortVisualData = (WorldFortVisualData)impserializable2;
				{23579} = worldFortVisualData.NowReward;
				{23580} = true;
				return true;
			}
			{23579} = null;
			{23580} = false;
			return false;
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x000D27E8 File Offset: 0x000D09E8
		private void {23581}(GSI {23582}, bool {23583})
		{
			Vector2 xy = this.{23613}.Offset(this.{23613}.WH.X * 0.95f + 3f, 3f).XY;
			Vector2 vector = xy;
			int num = 0;
			foreach (GSILocalPair gsilocalPair in ((IEnumerable<GSILocalPair>){23582}))
			{
				if (TargetInfoRenderer.ShowResoruce(gsilocalPair.ID) && gsilocalPair.ID != 253)
				{
					if (num == 3)
					{
						vector = xy + new Vector2(0f, 30f);
					}
					float scale = 0.85f - 0.066f * (float)num;
					Vector2 vector2 = new Vector2(26f);
					Marker marker = new Marker(ref vector, ref vector2);
					if (gsilocalPair.ID == 255)
					{
						Device gs = Engine.GS;
						Texture2D tex = CommonAtlas.Texture.Tex;
						Rectangle rectangle = marker.ToRect();
						Color color = Color.White * scale;
						gs.DrawCustomTexture(tex, CommonAtlas.goldIconMany64, rectangle, color);
					}
					else
					{
						ResourceInfo resourceInfo = Gameplay.ItemsInfo[gsilocalPair.ID];
						Device gs2 = Engine.GS;
						Texture2D iconTexture = resourceInfo.IconTexture;
						Rectangle rectangle = resourceInfo.IconTexture.Bounds;
						Rectangle rectangle2 = marker.ToRect();
						Color color = Color.White * scale;
						gs2.DrawCustomTexture(iconTexture, rectangle, rectangle2, color);
					}
					if ({23583} || gsilocalPair.ID == 19)
					{
						Engine.GS.SetFont(Fonts.Arial_9);
						Device gs3 = Engine.GS;
						string valueOfK = StringHelper.GetValueOfK(gsilocalPair.Count);
						vector2 = marker.Center + new Vector2(0f, 11f);
						Color color = Color.Wheat;
						gs3.DrawStringCenteredShadow(valueOfK, vector2, color, 0.8f);
					}
					num++;
					vector.X += 30f;
					if (num >= 6)
					{
						break;
					}
				}
			}
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000D29E4 File Offset: 0x000D0BE4
		private void {23584}(string {23585}, string {23586})
		{
			CustomSpriteFont font = Engine.GS.Font;
			Engine.GS.SetFont(Fonts.Philosopher_14);
			if (!string.IsNullOrEmpty({23586}))
			{
				Vector2 vector = Fonts.Philosopher_14.Measure({23585});
				Vector2 vector2 = Fonts.Philosopher_14.Measure({23586});
				float num = (vector.X + vector2.X > (float)TargetInfoRenderer.c_base.Width * 0.88f) ? ((float)TargetInfoRenderer.c_base.Width * 0.88f / (vector.X + vector2.X)) : 1f;
				Device gs = Engine.GS;
				Vector2 {14627} = this.{23613}.XY + new Vector2(this.{23613}.WH.X / 2f - (vector.X / 2f + vector2.X / 2f) * num, 7f);
				Color color = new Color(255, 234, 201);
				gs.DrawString({23585}, {14627}, color, 0f, Vector2.Zero, num);
				Device gs2 = Engine.GS;
				Vector2 {14627}2 = this.{23613}.XY + new Vector2(this.{23613}.WH.X / 2f + (vector.X / 2f - vector2.X / 2f) * num, 7f);
				color = new Color(255, 234, 201) * 0.5f;
				gs2.DrawString({23586}, {14627}2, color, 0f, Vector2.Zero, num);
			}
			else
			{
				Device gs3 = Engine.GS;
				Vector2 vector3 = this.{23613}.XY + new Vector2(this.{23613}.WH.X / 2f, 7f);
				Color color = new Color(255, 234, 201);
				gs3.DrawStringXCentered({23585}, vector3, color, 1f);
			}
			Engine.GS.SetFont(font);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x000D2BDC File Offset: 0x000D0DDC
		private static void RenderDistance(ref Vector2 {23587}, Rectangle {23588}, float {23589}, bool {23590}, float {23591} = 1f)
		{
			Vector2 value = TargetInfoRenderer.NextInfoPos(45f, {23588}, ref {23587}, {23591});
			Engine.GS.Draw({23588}, value);
			value += new Vector2((float){23588}.Width / 2f, TargetInfoRenderer.ValueYOffset);
			Color color = {23590} ? Color.Lerp(TargetInfoRenderer.TextColor, Color.Orange, 0.5f) : TargetInfoRenderer.TextColor;
			Device gs = Engine.GS;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<float>({23589}, "0");
			gs.DrawStringXCentered(defaultInterpolatedStringHandler.ToStringAndClear(), value, color, 1f);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x000D2C74 File Offset: 0x000D0E74
		private static void RenderShield(ref Vector2 {23592}, Rectangle {23593}, Ship {23594}, float {23595} = 1f)
		{
			Vector2 value = TargetInfoRenderer.NextInfoPos(45f, {23593}, ref {23592}, {23595});
			Engine.GS.Draw({23593}, value);
			value += new Vector2((float){23593}.Width / 2f, TargetInfoRenderer.ValueYOffset);
			Device gs = Engine.GS;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<float>({23594}.UsedShip.Armor, "0.0");
			string {14617} = defaultInterpolatedStringHandler.ToStringAndClear();
			Color color = ({23594}.UsedShip.Armor > 5f) ? Color.OrangeRed : TargetInfoRenderer.TextColor;
			gs.DrawStringXCentered({14617}, value, color, 1f);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000D2D14 File Offset: 0x000D0F14
		private static void RenderCrew(ref Vector2 {23596}, Rectangle {23597}, string {23598}, string {23599}, float {23600} = 1f)
		{
			Vector2 value = TargetInfoRenderer.NextInfoPos(60f, {23597}, ref {23596}, {23600});
			Engine.GS.Draw({23597}, value);
			value += new Vector2((float){23597}.Width / 2f, TargetInfoRenderer.ValueYOffset);
			Color color;
			if (string.IsNullOrEmpty({23599}))
			{
				Device gs = Engine.GS;
				color = TargetInfoRenderer.TextColor;
				gs.DrawStringXCentered({23598}, value, color, 1f);
				return;
			}
			Vector2 vector;
			if (string.IsNullOrEmpty({23599}))
			{
				float x = Engine.GS.Font.Measure({23598}).X;
				Device gs2 = Engine.GS;
				vector = value + new Vector2(-x / 2f, 0f);
				color = TargetInfoRenderer.TextColor;
				gs2.DrawString({23598}, vector, color);
				return;
			}
			float x2 = Engine.GS.Font.Measure({23598} + "/" + {23599}).X;
			Device gs3 = Engine.GS;
			vector = value + new Vector2(-x2 / 2f, 0f);
			int num;
			color = ((int.TryParse({23598}, out num) && num > Global.Player.UsedShip.Crew.Count) ? Color.OrangeRed : TargetInfoRenderer.TextColor);
			gs3.DrawString({23598}, vector, color);
			Device gs4 = Engine.GS;
			string {14599} = "/" + {23599};
			vector = value + new Vector2(-x2 / 2f + Engine.GS.Font.Measure({23598}).X, 0f);
			color = TargetInfoRenderer.TextColor * 0.7f;
			gs4.DrawString({14599}, vector, color);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x000D2EA0 File Offset: 0x000D10A0
		private static void RenderSail(ref Vector2 {23601}, Rectangle {23602}, Ship {23603}, float {23604} = 1f)
		{
			Vector2 vector = TargetInfoRenderer.NextInfoPos(45f, {23602}, ref {23601}, {23604});
			Engine.GS.Draw({23602}, vector);
			{20059}.DrawSailSpeedIcon({23603}, new Marker(vector.X + 3f, vector.Y + 1f, 19f, 25f), 1f, true);
			vector += new Vector2((float){23602}.Width / 2f + 5f, TargetInfoRenderer.ValueYOffset);
			Device gs = Engine.GS;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
			defaultInterpolatedStringHandler.AppendFormatted<float>({23603}.UsedShip.FirstSailHP * 100f, "0");
			defaultInterpolatedStringHandler.AppendLiteral("%");
			string {14617} = defaultInterpolatedStringHandler.ToStringAndClear();
			Color textColor = TargetInfoRenderer.TextColor;
			gs.DrawStringXCentered({14617}, vector, textColor, 1f);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x000D2F74 File Offset: 0x000D1174
		private static void RenderBoarding(ref Vector2 {23605}, Rectangle {23606}, float {23607} = 1f)
		{
			Vector2 vector = TargetInfoRenderer.NextInfoPos(45f, {23606}, ref {23605}, {23607});
			Color color = Color.White * (0.7f + 0.3f * (float)Math.Sin(Global.Game.GameTotalTimeSec * 8.0));
			Engine.GS.Draw({23606}, vector, color);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x000D2FD0 File Offset: 0x000D11D0
		private static Vector2 NextInfoPos(float {23608}, Rectangle {23609}, ref Vector2 {23610}, float {23611} = 1f)
		{
			Vector2 result = {23610} + new Vector2(({23608} - (float){23609}.Width) / 2f, 0f);
			{23610} += new Vector2({23608}, 0f) * {23611};
			return result;
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x000D3024 File Offset: 0x000D1224
		public static bool ShowResoruce(int {23612})
		{
			return {23612} == 255 || {23612} == 255 || {23612} == 253 || ({23612} != 20 && {23612} != 68 && (!Gameplay.ItemsInfo[{23612}].IsMap || {23612} == 72));
		}

		// Token: 0x040016AC RID: 5804
		private static readonly Rectangle c_base = new Rectangle(2329, 1096, 227, 95);

		// Token: 0x040016AD RID: 5805
		private static readonly Rectangle c_base_extended = new Rectangle(2860, 859, 227, 126);

		// Token: 0x040016AE RID: 5806
		private static readonly Rectangle c_preparationProgress = new Rectangle(2533, 1193, 158, 10);

		// Token: 0x040016AF RID: 5807
		private static readonly Rectangle c_spyglass = new Rectangle(2301, 1142, 27, 27);

		// Token: 0x040016B0 RID: 5808
		private static readonly Rectangle c_shield = new Rectangle(2273, 1142, 27, 27);

		// Token: 0x040016B1 RID: 5809
		private static readonly Rectangle c_crew = new Rectangle(2245, 1142, 27, 27);

		// Token: 0x040016B2 RID: 5810
		private static readonly Rectangle c_sail = new Rectangle(2216, 1142, 27, 27);

		// Token: 0x040016B3 RID: 5811
		private static readonly Rectangle c_boarding = new Rectangle(2302, 1112, 25, 29);

		// Token: 0x040016B4 RID: 5812
		private Marker {23613};
	}
}
