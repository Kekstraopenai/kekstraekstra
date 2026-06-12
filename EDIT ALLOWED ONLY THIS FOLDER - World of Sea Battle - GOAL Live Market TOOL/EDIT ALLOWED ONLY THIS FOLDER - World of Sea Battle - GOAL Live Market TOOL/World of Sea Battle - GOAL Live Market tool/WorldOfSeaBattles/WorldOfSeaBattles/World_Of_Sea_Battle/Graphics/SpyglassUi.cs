using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Graphics;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Graphics.Effects;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Graphics
{
	// Token: 0x02000457 RID: 1111
	internal class SpyglassUi
	{
		// Token: 0x06001836 RID: 6198 RVA: 0x000D0A1C File Offset: 0x000CEC1C
		public static void Render()
		{
			Engine.GS.SetTexture(AtlasObjs.Texture);
			Rectangle {11433} = new Rectangle(790, 202, 33, 33);
			Device gs = Engine.GS;
			Vector2 vector = Engine.GS.UIArea.HalfWidthHeight() - {11433}.HalfWidthHeight();
			Color color = Color.White * 0.4f;
			gs.Draw({11433}, vector, color);
			if (Global.Render.UiMode != InterfaceMode.Off)
			{
				InGameSightUi.CurrentInstance.DrawInformationAboutLastTarget(true);
			}
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00003100 File Offset: 0x00001300
		internal static void Update(FrameTime {23551})
		{
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x000D0AA0 File Offset: 0x000CECA0
		public static void Render2DMarkers()
		{
			if (Session.WorldMapMarkerPosition != null && Global.Camera.IsSpyglass)
			{
				Vector2 vector = Session.WorldMapMarkerPosition.Value;
				Vector3 vector2 = vector.X0Y ^ 10f;
				Camera camera = Global.Camera;
				Vector3 position = Global.Camera.Position;
				Vector3 vector3 = vector2 - Global.Camera.Position;
				vector3 = position + vector3.Normal * 100f;
				if (camera.IsVisible(vector3, 1f))
				{
					Vector2 projection = Global.Camera.GetProjection(vector2);
					Vector2 vector4 = Engine.GS.UIArea.HalfWidthHeight();
					float num = Math.Max(Math.Abs(projection.X - vector4.X) / vector4.X, Math.Abs(projection.Y - vector4.Y) / vector4.Y);
					num = 1f - MathF.Sqrt(num);
					Rectangle c_iconMarker = {22913}.c_iconMarker;
					Device gs = Engine.GS;
					Texture2D worldMapUiElements = OtherTextures.WorldMapUiElements;
					Rectangle rectangle = new Rectangle(projection.X - 20f, projection.Y - 20f, 40f, 40f, false);
					Color color = Color.White * num;
					gs.DrawCustomTexture(worldMapUiElements, c_iconMarker, rectangle, color);
					float value = Vector2.Distance(Global.Player.Position, Session.WorldMapMarkerPosition.Value);
					Engine.GS.SetFont(Fonts.Philosopher_14);
					Device gs2 = Engine.GS;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
					defaultInterpolatedStringHandler.AppendFormatted<float>(value, "F0");
					string {14590} = defaultInterpolatedStringHandler.ToStringAndClear();
					vector = projection + new Vector2(0f, 27f);
					color = Color.Pink * num;
					gs2.DrawStringCenteredShadow({14590}, vector, color, 0.8f);
				}
			}
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x000D0C6C File Offset: 0x000CEE6C
		public static void RenderInfoPanel(string {23552}, string {23553}, ImageDecription? {23554}, Rectangle? {23555}, GSI {23556}, bool {23557})
		{
			bool flag = Global.Game.InterestPoints.CurrentTargetResearchLevel >= 1f;
			Player player = Global.Game.InterestPoints.ShipInSight as Player;
			int num;
			if (player == null)
			{
				Npc npc = Global.Game.InterestPoints.ShipInSight as Npc;
				num = ((npc != null && npc.UsedShipNpc.Information.Extras.AllowCapture(Global.Player.MapInfo)) ? npc.UsedShipNpc.Information.BasedOn.Rank : 0);
			}
			else
			{
				num = player.UsedShipPlayer.CraftFrom.Rank;
			}
			int num2 = num;
			string text = (num2 > 0) ? Local.StringConstants_114(StringHelper.ToRoman(num2)) : string.Empty;
			Rectangle {11433} = new Rectangle(3123, 0, 60, 46);
			Vector2 vector = Vector2.Zero;
			if (Global.Game.InterestPoints.ShipInSight != null)
			{
				vector = ((IClientShip)Global.Game.InterestPoints.ShipInSight).GetClient.Graphics2DPos - new Vector2(0f, 80f);
			}
			Vector2 vector2;
			if (Global.Game.InterestPoints.ObjectAtSight != null && Global.Game.InterestPoints.ObjectAtSight.Value.AsPort == null)
			{
				Camera camera = Engine.GS.Camera;
				vector2 = Global.Game.InterestPoints.ObjectAtSight.Value.Center;
				vector = camera.GetProjection(vector2.X0Y + new Vector3(0f, Global.Game.InterestPoints.ObjectAtSight.Value.Radius, 0f));
			}
			if (vector.X != 0f)
			{
				vector.Y = Math.Max(30f, vector.Y);
				Device gs = Engine.GS;
				vector2 = {11433}.HalfWidthHeight();
				gs.Draw({11433}, vector, vector2, 0f, 0.66f);
			}
			Rectangle rectangle = new Rectangle(3186, 0, 593, 719);
			int num3 = (Global.Game.InterestPoints.ShipInSight is Player) ? 30 : 40;
			int num4 = 22;
			SpyglassUi.<>c__DisplayClass6_0 CS$<>8__locals1;
			CS$<>8__locals1.resourceHeight = 30;
			CS$<>8__locals1.resourceIconSize = 28;
			int num5 = 9;
			int num6 = 100 + ((!string.IsNullOrEmpty({23553})) ? 25 : 0) + ((!string.IsNullOrEmpty(text)) ? 15 : 0);
			int num7;
			if ({23556} != null)
			{
				num7 = (2 + Math.Min(num5, {23556}.Count((GSILocalPair {23561}) => TargetInfoRenderer.ShowResoruce({23561}.ID)))) * CS$<>8__locals1.resourceHeight;
			}
			else
			{
				num7 = 0;
			}
			int num8 = num6 + num7;
			ShipNpc shipNpc = null;
			ShipNpc shipNpc2 = Global.Game.InterestPoints.ShipInSight as ShipNpc;
			if (shipNpc2 != null && shipNpc2.TradingRoute != null && !shipNpc2.IsPlayerCaper)
			{
				shipNpc = shipNpc2;
				num8 += 80;
				num8 += 30 * shipNpc.TradingRoute.ResourceInfos.Length;
			}
			else
			{
				num8 += 30;
			}
			num8 = Math.Max(140, num8);
			CS$<>8__locals1.pos = new Vector2(20f, 20f);
			float num9 = 0.48f;
			Vector2 pos = CS$<>8__locals1.pos;
			Marker marker = new Marker(ref CS$<>8__locals1.pos, (float)rectangle.Width * num9, (float)num8);
			Device gs2 = Engine.GS;
			Rectangle rectangle2 = rectangle.SetHeight((float)(num8 / 2) / num9);
			vector2 = CS$<>8__locals1.pos - new Vector2(19f, 15f);
			Rectangle rectangle3 = new Marker(ref vector2, (float)rectangle.Width * num9, (float)(num8 / 2)).ToRect();
			gs2.Draw(rectangle2, rectangle3);
			Device gs3 = Engine.GS;
			rectangle2 = rectangle.SetHeight((float)(num8 / 2) / num9).SetY((float)(rectangle.Y + rectangle.Height) - (float)(num8 / 2) / num9);
			vector2 = CS$<>8__locals1.pos - new Vector2(19f, 15f) + new Vector2(0f, (float)(num8 / 2));
			rectangle3 = new Marker(ref vector2, (float)rectangle.Width * num9, (float)(num8 / 2)).ToRect();
			gs3.Draw(rectangle2, rectangle3);
			CustomSpriteFont font = Engine.GS.Font;
			CustomSpriteFont philosopher_ = Fonts.Philosopher_18;
			CustomSpriteFont arial_ = Fonts.Arial_10;
			Color value = new Color(255, 234, 201);
			value * 0.5f;
			Vector2 vector3 = philosopher_.Measure({23552});
			if ({23555} != null && {23555}.Value.Width > 0 && flag)
			{
				Device gs4 = Engine.GS;
				Texture2D tex = AtlasObjs.Texture.Tex;
				rectangle2 = {23555}.Value;
				rectangle3 = new Marker(CS$<>8__locals1.pos.X + vector3.X, CS$<>8__locals1.pos.Y, (float)num4, (float)num4).ToRect();
				Color color = Color.White;
				gs4.DrawCustomTexture(tex, rectangle2, rectangle3, color);
			}
			Engine.GS.SetFont(philosopher_);
			Engine.GS.DrawString(flag ? {23552} : "?", CS$<>8__locals1.pos, value, 0f, Vector2.Zero, 1f);
			CS$<>8__locals1.pos.Y = CS$<>8__locals1.pos.Y + (vector3.Y - 5f);
			if (flag)
			{
				Engine.GS.SetFont(arial_);
				Device gs5 = Engine.GS;
				vector2 = new Vector2(CS$<>8__locals1.pos.X, CS$<>8__locals1.pos.Y);
				Color color = Color.Wheat;
				gs5.DrawString({23553}, vector2, color);
				if (!string.IsNullOrEmpty(text))
				{
					CS$<>8__locals1.pos.Y = CS$<>8__locals1.pos.Y + 15f;
					Device gs6 = Engine.GS;
					string {14599} = text;
					vector2 = new Vector2(CS$<>8__locals1.pos.X, CS$<>8__locals1.pos.Y);
					color = Color.Wheat * 0.5f;
					gs6.DrawString({14599}, vector2, color);
				}
			}
			CS$<>8__locals1.pos.Y = CS$<>8__locals1.pos.Y + 28f;
			Rectangle rectangle4 = new Rectangle(3139, 726, 310, 84);
			float num10 = 0.8f;
			Vector2 vector4 = CS$<>8__locals1.pos + new Vector2(50f * num9, 6f);
			Engine.GS.SetFont(Fonts.Arial_10);
			if (Global.Game.InterestPoints.ShipInSight != null)
			{
				Device gs7 = Engine.GS;
				rectangle2 = new Marker(marker.Center.X - (float)(rectangle4.Width / 2) * num10 - 10f, CS$<>8__locals1.pos.Y, marker.Width * num10, 50f).ToRect();
				gs7.Draw(rectangle4, rectangle2);
				SpyglassUi.targetInfoRenderer.RenderShipStats(vector4, Global.Game.InterestPoints.ShipInSight, true);
				CS$<>8__locals1.pos.Y = CS$<>8__locals1.pos.Y + 63f;
			}
			if (Global.Game.InterestPoints.ObjectAtSight != null)
			{
				Device gs8 = Engine.GS;
				rectangle2 = new Marker(marker.Center.X - (float)(rectangle4.Width / 2) * num10 - 10f, CS$<>8__locals1.pos.Y, marker.Width * num10, 50f).ToRect();
				gs8.Draw(rectangle4, rectangle2);
				SpyglassUi.targetInfoRenderer.RenderBuildingStats(vector4, Global.Game.InterestPoints.ObjectAtSight.Value);
				CS$<>8__locals1.pos.Y = CS$<>8__locals1.pos.Y + 63f;
			}
			if ({23556} != null && !{23556}.IsEmpty && flag)
			{
				Engine.GS.SetFont(Fonts.Arial_10);
				Device gs9 = Engine.GS;
				string resoures_in_sight = Local.resoures_in_sight;
				Color color = Color.Wheat;
				gs9.DrawString(resoures_in_sight, CS$<>8__locals1.pos, color);
				CS$<>8__locals1.pos.Y = CS$<>8__locals1.pos.Y + 20f;
				int num11 = 0;
				foreach (GSILocalPair gsilocalPair in ((IEnumerable<GSILocalPair>){23556}))
				{
					if (TargetInfoRenderer.ShowResoruce(gsilocalPair.ID))
					{
						if (--num5 == 0)
						{
							ImageDecription {23558} = new ImageDecription(CommonAtlas.Texture.Tex, new Rectangle(381, 0, 42, 42));
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
							defaultInterpolatedStringHandler.AppendFormatted(Local.PortСraftShipWindow_25);
							defaultInterpolatedStringHandler.AppendLiteral(" ");
							defaultInterpolatedStringHandler.AppendFormatted<int>({23556}.Count<GSILocalPair>() - num11);
							SpyglassUi.<RenderInfoPanel>g__DrawRes|6_1({23558}, defaultInterpolatedStringHandler.ToStringAndClear(), ref CS$<>8__locals1);
							break;
						}
						if (gsilocalPair.ID == 255)
						{
							SpyglassUi.<RenderInfoPanel>g__DrawRes|6_1(new ImageDecription(CommonAtlas.Texture.Tex, CommonAtlas.goldIconMany64), Local.many_gold, ref CS$<>8__locals1);
						}
						else if (gsilocalPair.ID == 253)
						{
							ShipNpc shipNpc3 = Global.Game.InterestPoints.ShipInSight as ShipNpc;
							if (shipNpc3 != null && !shipNpc3.IsPlayerCaper)
							{
								SpyglassUi.<RenderInfoPanel>g__DrawRes|6_1(new ImageDecription(CommonAtlas.Texture.Tex, new Rectangle(1042, 217, 96, 96)), Local.cannons_as_loot, ref CS$<>8__locals1);
							}
						}
						else
						{
							ResourceInfo resourceInfo = Gameplay.ItemsInfo[gsilocalPair.ID];
							string text2;
							if (!{23557} && gsilocalPair.ID != 19)
							{
								text2 = resourceInfo.Name;
							}
							else
							{
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(2, 2);
								defaultInterpolatedStringHandler2.AppendFormatted(resourceInfo.Name);
								defaultInterpolatedStringHandler2.AppendLiteral(": ");
								defaultInterpolatedStringHandler2.AppendFormatted<int>(gsilocalPair.Count);
								text2 = defaultInterpolatedStringHandler2.ToStringAndClear();
							}
							string {23559} = text2;
							SpyglassUi.<RenderInfoPanel>g__DrawRes|6_1(resourceInfo.IconTexture, {23559}, ref CS$<>8__locals1);
						}
						num11++;
					}
				}
				InterestPointsManager interestPoints = Global.Game.InterestPoints;
				object obj;
				if (interestPoints.ObjectAtSight == null)
				{
					obj = null;
				}
				else
				{
					ref BuildingTarget ptr = ref interestPoints.ObjectAtSight.GetValueOrDefault();
					obj = ((ptr.AsDynamic != null) ? ptr.AsDynamic.GetValueOrDefault().VisualData : null);
				}
				if (obj is WorldFortVisualData)
				{
					SpyglassUi.<RenderInfoPanel>g__DrawRes|6_1(new ImageDecription(CommonAtlas.Texture.Tex, new Rectangle(156, 65, 60, 60)), "Награда растет со временем", ref CS$<>8__locals1);
				}
			}
			if ({23554} != null && flag)
			{
				Rectangle path = {23554}.Value.Path;
				float num12 = (float)num3 / (float)path.Height;
				Marker marker2 = new Marker(pos.X + (float)rectangle.Width * num9 - 19f, pos.Y - 5f, (float)path.Width * num12, (float)path.Height * num12);
				Device gs10 = Engine.GS;
				Texture2D tex2 = {23554}.Value.Tex;
				rectangle2 = marker2.ToRect();
				Color color = Color.White;
				gs10.DrawCustomTexture(tex2, path, rectangle2, color);
			}
			if (shipNpc != null)
			{
				CS$<>8__locals1.pos.Y = CS$<>8__locals1.pos.Y + 10f;
				Engine.GS.SetFont(Fonts.Philosopher_14);
				Device gs11 = Engine.GS;
				string tradingRouteName = shipNpc.TradingRoute.Type.GetTradingRouteName();
				Vector2 pos2 = CS$<>8__locals1.pos;
				Color color = Color.Wheat;
				gs11.DrawString(tradingRouteName, pos2, color, 0f, Vector2.Zero, 0.87f);
				CS$<>8__locals1.pos.Y = CS$<>8__locals1.pos.Y + 20f;
				if (!Session.Account.OpenedTradingRoutes[shipNpc.TradingRoute.Id])
				{
					Engine.GS.SetFont(Fonts.Arial_10);
					Device gs12 = Engine.GS;
					string trading_route_not_open = Local.trading_route_not_open;
					Vector2 pos3 = CS$<>8__locals1.pos;
					color = Color.Wheat * 0.6f;
					gs12.DrawStringClipped(trading_route_not_open, pos3, color, marker.Width - 50f, 1f);
				}
				else
				{
					Engine.GS.SetFont(Fonts.Arial_10);
					Device gs13 = Engine.GS;
					string tradingRouteDescription = shipNpc.TradingRoute.Type.GetTradingRouteDescription();
					Vector2 pos4 = CS$<>8__locals1.pos;
					color = Color.Wheat * 0.6f;
					gs13.DrawStringClipped(tradingRouteDescription, pos4, color, marker.Width - 50f, 1f);
					CS$<>8__locals1.pos.Y = CS$<>8__locals1.pos.Y + 40f;
					if (shipNpc.TradingRoute.ResourceInfos != null && shipNpc.TradingRoute.ResourceInfos.Length != 0)
					{
						Device gs14 = Engine.GS;
						string resource_info_available = Local.resource_info_available;
						color = Color.Wheat;
						gs14.DrawString(resource_info_available, CS$<>8__locals1.pos, color);
						CS$<>8__locals1.pos.Y = CS$<>8__locals1.pos.Y + 20f;
						foreach (ResourceInfo resourceInfo2 in shipNpc.TradingRoute.ResourceInfos)
						{
							ResourceInfo resourceInfo3 = Gameplay.ItemsInfo[(int)resourceInfo2.ID];
							Device gs15 = Engine.GS;
							Texture2D iconTexture = resourceInfo3.IconTexture;
							rectangle2 = new Rectangle(0, 0, resourceInfo3.IconTexture.Width, resourceInfo3.IconTexture.Height);
							rectangle3 = new Rectangle(CS$<>8__locals1.pos.X, CS$<>8__locals1.pos.Y, 28f, 28f, false);
							color = Color.White;
							gs15.DrawCustomTexture(iconTexture, rectangle2, rectangle3, color);
							Device gs16 = Engine.GS;
							string name = resourceInfo3.Name;
							vector2 = new Vector2(CS$<>8__locals1.pos.X + 36f, CS$<>8__locals1.pos.Y + 6f);
							color = Color.Wheat;
							gs16.DrawString(name, vector2, color);
							CS$<>8__locals1.pos.Y = CS$<>8__locals1.pos.Y + 30f;
						}
					}
					else
					{
						Device gs17 = Engine.GS;
						string resource_info_varied = Local.resource_info_varied;
						Vector2 pos5 = CS$<>8__locals1.pos;
						color = Color.Wheat;
						gs17.DrawString(resource_info_varied, pos5, color, 0f, Vector2.One, 0.9f);
					}
				}
			}
			ShipOtherPlayer shipOtherPlayer = Global.Game.InterestPoints.ShipInSight as ShipOtherPlayer;
			if (shipOtherPlayer != null && !shipOtherPlayer.MakeTransparentForMe)
			{
				Decorator game = Session.Game;
				bool flag2;
				bool flag3;
				bool flag4;
				FractionAPI.WillGetBlackMark(game, (shipOtherPlayer.StatusColor == HealthBarStyle.Lime) ? Relation.Ally : ((shipOtherPlayer.StatusColor == HealthBarStyle.Red) ? Relation.Enemy : Relation.Neutral), shipOtherPlayer, shipOtherPlayer.RemoteInfo.Flags, shipOtherPlayer.Client.Guild.IsNull || shipOtherPlayer.Client.Guild.IsFlotilia, Session.EventActionsPipeline.EventCategoryAmount(EActionCaterory.PXpAndMarksBoostFromPvp) > 0f, shipOtherPlayer.AggressorFlag, shipOtherPlayer.HasWantedLevel, out flag2, out flag3, out flag4);
				if (shipOtherPlayer.AggressorFlag)
				{
					flag4 = false;
				}
				CS$<>8__locals1.pos.Y = CS$<>8__locals1.pos.Y + 30f;
				if (flag4)
				{
					Engine.GS.SetFont(Fonts.Arial_10Bold);
					Device gs18 = Engine.GS;
					string {14626} = StringHelper.SplitAroundMiddleSpace(Local.blacK_mark_alert);
					Vector2 pos6 = CS$<>8__locals1.pos;
					Color color = Color.Pink * 0.8f;
					gs18.DrawString({14626}, pos6, color, 0f, Vector2.One, 0.9f);
				}
				else if (shipOtherPlayer.HasWantedLevel)
				{
					Engine.GS.SetFont(Fonts.Arial_10Bold);
					Device gs19 = Engine.GS;
					string worldMapEvent_wanted_no_fr = Local.worldMapEvent_wanted_no_fr;
					Vector2 pos7 = CS$<>8__locals1.pos;
					Color color = Color.SkyBlue * 0.8f;
					gs19.DrawStringClipped(worldMapEvent_wanted_no_fr, pos7, color, 270f, 0.9f);
				}
			}
			ShipNpc shipNpc4 = Global.Game.InterestPoints.ShipInSight as ShipNpc;
			if (shipNpc4 != null && shipNpc4.IsShieldActive)
			{
				Engine.GS.SetFont(Fonts.Philosopher_12);
				Device gs20 = Engine.GS;
				string active_npc_shield = Local.active_npc_shield;
				Vector2 {14622} = CS$<>8__locals1.pos + new Vector2(-10f, 50f);
				Color color = Color.SkyBlue;
				gs20.DrawStringClipped(active_npc_shield, {14622}, color, 270f, 1f);
			}
			Engine.GS.SetFont(font);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x000D1C20 File Offset: 0x000CFE20
		[CompilerGenerated]
		internal static void <RenderInfoPanel>g__DrawRes|6_1(ImageDecription {23558}, string {23559}, ref SpyglassUi.<>c__DisplayClass6_0 {23560})
		{
			Device gs = Engine.GS;
			Texture2D tex = {23558}.Tex;
			Rectangle rectangle = new Rectangle({23560}.pos.X, {23560}.pos.Y, (float){23560}.resourceIconSize, (float){23560}.resourceIconSize, false);
			Color color = Color.White;
			gs.DrawCustomTexture(tex, {23558}.Path, rectangle, color);
			Device gs2 = Engine.GS;
			Vector2 vector = new Vector2({23560}.pos.X + 36f, {23560}.pos.Y + 6f);
			color = Color.Wheat * 0.8f;
			gs2.DrawString({23559}, vector, color);
			{23560}.pos.Y = {23560}.pos.Y + (float){23560}.resourceHeight;
		}

		// Token: 0x040016A4 RID: 5796
		private static float activeTimeSec;

		// Token: 0x040016A5 RID: 5797
		private static float prevCameraAngle;

		// Token: 0x040016A6 RID: 5798
		private static TargetInfoRenderer targetInfoRenderer = new TargetInfoRenderer();
	}
}
