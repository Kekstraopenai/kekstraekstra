using System;
using System.Collections.Generic;
using Common.Account;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Graphics;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x0200049C RID: 1180
	internal sealed class EnvironmentGraphics
	{
		// Token: 0x060019D1 RID: 6609 RVA: 0x000E5454 File Offset: 0x000E3654
		public void Render()
		{
			if (EnvironmentGraphics.drawIcon == null)
			{
				EnvironmentGraphics.drawIcon = BillboardParent_VPCT.CreatePlane(6f, 6f, 0f);
				EnvironmentGraphics.drawIcon.Transform(Matrix.CreateRotationX(1.5707964f));
				EnvironmentGraphics.mapBorderLineRender = new BillboardParent_VPCT();
				EnvironmentGraphics.mapBorderLineRender.SetPos((float)EnvironmentGraphics.p_mapLayoutLine.Width * 0.24f, (float)EnvironmentGraphics.p_mapLayoutLine.Height * 0.16f);
				EnvironmentGraphics.mapBorderLineRender.SetUV(EnvironmentGraphics.p_mapLayoutLine, AtlasObjs.Texture.Size);
				EnvironmentGraphics.mapBorderLineRender.Transform(Matrix.CreateRotationX(-1.5707964f));
			}
			Engine.GS.Camera.Position.XZ();
			bool flag = Session.CurrentArenaSession != null && (!{18560}.closed || Global.Player.MapInfo is MapArenaInfo || Global.Player.MapInfo is MapForPassingInfo);
			if (Global.Player.MapInfo.IsEducationMap && {18593}.CurrentInstance != null && {18593}.CurrentInstance.FollowTaskPosition != null)
			{
				Vector2 value = {18593}.CurrentInstance.FollowTaskPosition.Value;
				float {24360} = 14f;
				Color color = Color.Black;
				EnvironmentGraphics.DrawBillboardIcon(value, {24360}, EnvironmentGraphics.pathFlag, color, false, false, null);
			}
			if (flag)
			{
				foreach (Vector2 vector in new Vector2[]
				{
					Session.CurrentArenaSession.MyBasePosition,
					Session.CurrentArenaSession.EnemyBasePosition
				})
				{
					Vector3 {15453} = new Vector3(vector.X, -0.5f, vector.Y);
					float num = 37f;
					Global.Render.ItemsShader.RenderCircle({15453}, num, num + 1f, Color.White * 0.1f, GPUCircleType.HardWithBorder, null);
					Global.Render.ItemsShader.RenderCircle({15453}, num, num + 1f, Color.Cyan * 0.1f, GPUCircleType.Deliquescent, null);
				}
				return;
			}
			if (!Global.Player.IsPortEntry)
			{
				if (Global.Game.InteractiveWorldSystem.ContainsGuildFort != null)
				{
					PbsBuildingPlaceInfo pbsBuildingPlaceInfo = Gameplay.WorldMap.Ports.Array[(int)Global.Game.InteractiveWorldSystem.VisibleGuildFortData.PortID].PbSystem[(int)Global.Game.InteractiveWorldSystem.VisibleGuildFortData.PlaceIDInPort];
					InteractiveWorldSystem.FortInteropMode containsGuildFortMode = Global.Game.InteractiveWorldSystem.ContainsGuildFortMode;
					bool flag2 = containsGuildFortMode - InteractiveWorldSystem.FortInteropMode.RobbingFirstTime <= 2;
					if (flag2)
					{
						Vector2 entryPointPier = pbsBuildingPlaceInfo.EntryPointPier;
						float {24360}2 = 14f;
						Color color = Color.Black * 0.5f;
						EnvironmentGraphics.DrawBillboardIcon(entryPointPier, {24360}2, EnvironmentGraphics.pathBlades, color, false, false, null);
					}
					EnvironmentGraphics.DrawInteropZoneCircle(pbsBuildingPlaceInfo.EntryPointPier, pbsBuildingPlaceInfo.EntryPointPerRadius * 0.9f, Color.Yellow);
				}
				if (Global.Player.MapInfo.Ports.Size > 0)
				{
					IslePortInfo nearPort = Global.Player.NearPort;
					if (nearPort.EntryPos.DTest2(Global.Player.Position, nearPort.GlobalEntryRadius + 210f + 200f))
					{
						new Vector3(nearPort.EntryPos.X, -0.5f, nearPort.EntryPos.Y);
						float globalEntryRadius = nearPort.GlobalEntryRadius;
					}
					if (!{18560}.closed && {18560}.visibility == {18560}.VisibleDistance.HighNoOcean)
					{
						foreach (IslePortInfo islePortInfo in ((IEnumerable<IslePortInfo>)Global.Player.MapInfo.Ports))
						{
							Vector3 {15453}2 = new Vector3(islePortInfo.EntryPos.X, 5f, islePortInfo.EntryPos.Y);
							Global.Render.ItemsShader.RenderCircle({15453}2, 405f, 410f, Color.Red, GPUCircleType.HardWithBorder, null);
							Global.Render.ItemsShader.RenderCircle({15453}2, 645f, 650f, Color.Red, GPUCircleType.HardWithBorder, null);
							if (islePortInfo.AllowCapture)
							{
								Vector2 value2 = (islePortInfo.FortPosition + islePortInfo.EntryPos) * 0.5f;
								Vector2 value3 = Geometry.RotateVector2((islePortInfo.FortPosition - islePortInfo.EntryPos).Normal(), 1.5707964f);
								for (int j = -25; j < 25; j++)
								{
									Global.Render.ItemsShader.RenderCircle((value2 + value3 * (float)j * 25f).X0Y(), 1f, 4f, Color.Red, GPUCircleType.SoftCircle, null);
								}
							}
						}
					}
				}
				foreach (QuestRunningProgress questRunningProgress in ((IEnumerable<QuestRunningProgress>)Session.Account.Quests.ProgressRunningQuests))
				{
					if (questRunningProgress.CurrentStep is QuestReturnBack)
					{
						Vector2 locationPos = questRunningProgress.Info.LocationPos;
						float {24360}3 = 14f;
						Color color = Color.White;
						EnvironmentGraphics.DrawBillboardIcon(locationPos, {24360}3, EnvironmentGraphics.pathFlag, color, false, false, null);
						EnvironmentGraphics.DrawInteropZoneCircle(questRunningProgress.Info.LocationPos, (float)QuestInfo.GiveQuestInSeaRadius.Value, Color.Lime * 0.6f);
					}
					QuestTransferOrder questTransferOrder = questRunningProgress.Info.FirstStep as QuestTransferOrder;
					if (questTransferOrder != null && questTransferOrder.TargetAsPort == null)
					{
						Vector2 targetPosition = questTransferOrder.TargetPosition;
						float {24360}4 = 14f;
						Color color = Color.Cyan * 0.5f;
						EnvironmentGraphics.DrawBillboardIcon(targetPosition, {24360}4, EnvironmentGraphics.pathFlag, color, false, false, null);
					}
				}
				foreach (PlayerWorldActivityStatus playerWorldActivityStatus in ((IEnumerable<PlayerWorldActivityStatus>)Session.Account.WorldActivities))
				{
					if (playerWorldActivityStatus.TimeToFinishSec == 0f)
					{
						Vector2 position = playerWorldActivityStatus.Position;
						float {24360}5 = 14f;
						Color color = Color.White;
						EnvironmentGraphics.DrawBillboardIcon(position, {24360}5, EnvironmentGraphics.pathFlag, color, false, false, null);
					}
				}
				foreach (ShipNpc shipNpc in ((IEnumerable<ShipNpc>)Global.Game.WorldInstance.NpcList))
				{
					if (shipNpc.IsActiveWanderingTrader)
					{
						Vector2 position2 = shipNpc.Position;
						float {24360}6 = 14f;
						Color color = Color.White;
						EnvironmentGraphics.DrawBillboardIcon(position2, {24360}6, EnvironmentGraphics.pathTraderInSea, color, false, false, null);
					}
				}
				for (int k = 0; k < Global.Game.InterestPoints.VisibleObjetcs.Size; k++)
				{
					EnvironmentGraphics.DrawIconFor(ref Global.Game.InterestPoints.VisibleObjetcs.Array[k]);
				}
				if (Session.EngagingInPortBattle != PbsBatlleSide.None && Global.Player.ProtectionSafeZoneTimeout > 0f && !Global.Player.IsProtectionSafeZoneTimeoutNotFrozen)
				{
					foreach (KeyValuePair<PbsBatlleSide, PbsAvanpostShortInfo> keyValuePair in Session.EngagingInPortBattlePort.PbRespawn)
					{
						EnvironmentGraphics.DrawInteropZoneCircle(keyValuePair.Value.Center, 50f, Color.White);
					}
				}
				if (Global.Player.MapInfo.IsWorldmap)
				{
					Vector2 vector2 = default(Vector2);
					Vector2 value4 = new Vector2((float)Math.Sign(Global.Player.Position.X), (float)Math.Sign(Global.Player.Position.Y));
					Vector2 vector3 = new Vector2(Math.Abs(Global.Player.Position.X), Math.Abs(Global.Player.Position.Y));
					if (vector3.X / Global.Player.MapInfo.MapSize.X < vector3.Y / Global.Player.MapInfo.MapSize.Y)
					{
						vector2 = value4 * new Vector2(vector3.X, Global.Player.MapInfo.MapSize.Y * 0.5f);
					}
					else
					{
						vector2 = value4 * new Vector2(Global.Player.MapInfo.MapSize.X * 0.5f, vector3.Y);
					}
					EnvironmentGraphics.DrawZoneEndIcon(vector2, EnvironmentGraphics.pathWarning, 180f);
				}
				else
				{
					Vector3 position3 = Engine.GS.Camera.Position;
					Vector3 position3D = Global.Player.Position3D;
					float num2 = position3D.Y + 1f;
					if (Global.Player.MapInfo.IsCircularShape)
					{
						Vector2 vector4 = Global.Player.Position;
						float num3 = vector4.Length();
						if (Global.Player.MapInfo.MapSize.X * 0.5f - num3 < 210f)
						{
							float num4 = MathF.Atan2(Global.Player.Position.Y, Global.Player.Position.X);
							Vector2 vector5 = Geometry.SubstructRotate(num4, Global.Player.MapInfo.MapSize.X * 0.5f);
							Global.Render.ItemsShader.SetForRender(Matrix.CreateRotationY(-num4 - 1.5707964f) * Matrix.CreateTranslation(vector5.X, num2, vector5.Y), Vector4.One);
							Global.Render.ItemsShader.BeginPass(true, true);
							EnvironmentGraphics.mapBorderLineRender.Render();
							Global.Render.ItemsShader.SetForRender(Matrix.Identity, Vector4.One);
							Global.Render.ItemsShader.BeginPass(true, true);
						}
					}
					else
					{
						if (position3.X > Global.Player.MapInfo.MapSize.X / 2f - 210f)
						{
							Global.Render.ItemsShader.SetForRender(Matrix.CreateRotationY(-1.5707964f) * Matrix.CreateTranslation(Global.Player.MapInfo.MapSize.X / 2f, num2, position3D.Z), Vector4.One);
							Global.Render.ItemsShader.BeginPass(true, true);
							EnvironmentGraphics.mapBorderLineRender.Render();
						}
						if (position3.X < -Global.Player.MapInfo.MapSize.X / 2f + 210f)
						{
							Global.Render.ItemsShader.SetForRender(Matrix.CreateRotationY(1.5707964f) * Matrix.CreateTranslation(-Global.Player.MapInfo.MapSize.X / 2f, num2, position3D.Z), Vector4.One);
							Global.Render.ItemsShader.BeginPass(true, true);
							EnvironmentGraphics.mapBorderLineRender.Render();
						}
						if (position3.Z > Global.Player.MapInfo.MapSize.Y / 2f - 210f)
						{
							Global.Render.ItemsShader.SetForRender(Matrix.CreateRotationY(-3.1415927f) * Matrix.CreateTranslation(position3D.X, num2 + 0.01f, Global.Player.MapInfo.MapSize.Y / 2f), Vector4.One);
							Global.Render.ItemsShader.BeginPass(true, true);
							EnvironmentGraphics.mapBorderLineRender.Render();
						}
						if (position3.Z < -Global.Player.MapInfo.MapSize.Y / 2f + 210f)
						{
							Global.Render.ItemsShader.SetForRender(Matrix.CreateTranslation(position3D.X, num2 + 0.01f, -Global.Player.MapInfo.MapSize.Y / 2f), Vector4.One);
							Global.Render.ItemsShader.BeginPass(true, true);
							EnvironmentGraphics.mapBorderLineRender.Render();
						}
					}
				}
				if (Session.Account.WorldFlag == OpenWorldFlag.Trader && Global.Player.NearAquatoria != null && Global.Player.MapInfo.IsWorldmap)
				{
					Vector2? vector6 = this.TraceAquatoriaEndPos(Global.Player.NearPort, 120f);
					if (vector6 != null)
					{
						Vector2 vector4 = vector6.Value;
						EnvironmentGraphics.DrawZoneEndIcon(vector4, EnvironmentGraphics.pathWarning, 150f);
					}
				}
			}
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000E610C File Offset: 0x000E430C
		private Vector2? TraceAquatoriaEndPos(IslePortInfo {24343}, float {24344})
		{
			float num = 16f;
			float num2 = float.MaxValue;
			Vector2? result = null;
			int num3 = 0;
			while ((float)num3 <= num)
			{
				Vector2 position = Global.Player.Position;
				Vector2 {24347} = position + Geometry.RotateVector2(Vector2.UnitX, 6.2831855f * (float)num3 / num - 1.5707964f) * {24344};
				Vector2? vector = this.TraceAquatoriaEndPos({24343}, position, {24347}, 16);
				if (vector != null)
				{
					float num4 = Vector2.DistanceSquared(position, vector.Value);
					if (num4 * 1.05f < num2)
					{
						result = new Vector2?(vector.Value);
						num2 = num4;
					}
				}
				num3++;
			}
			return result;
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x000E61B8 File Offset: 0x000E43B8
		private Vector2? TraceAquatoriaEndPos(IslePortInfo {24345}, Vector2 {24346}, Vector2 {24347}, int {24348} = 24)
		{
			int i = 0;
			while (i < {24348})
			{
				Vector2 vector = ({24346} + {24347}) / 2f;
				IslePortInfo nearAquatoria = Global.Player.MapInfo.GetNearAquatoria({24346});
				IslePortInfo nearAquatoria2 = Global.Player.MapInfo.GetNearAquatoria({24347});
				IslePortInfo nearAquatoria3 = Global.Player.MapInfo.GetNearAquatoria(vector);
				if (nearAquatoria == nearAquatoria2 && i == 0)
				{
					return null;
				}
				if (nearAquatoria3 == {24345})
				{
					{24346} = vector;
				}
				else if (nearAquatoria3 != {24345})
				{
					{24347} = vector;
				}
				if (Vector2.DistanceSquared({24346}, {24347}) < 0.01f)
				{
					Vector2 value = ({24346} + {24347}) / 2f;
					if (Global.Player.MapInfo.GetNearAquatoria(value) == {24345})
					{
						return new Vector2?(value);
					}
					return new Vector2?((nearAquatoria == {24345}) ? {24346} : {24347});
				}
				else
				{
					i++;
				}
			}
			return new Vector2?(({24346} + {24347}) / 2f);
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000E62A8 File Offset: 0x000E44A8
		private static void DrawInteropZoneCircle(Vector2 {24349}, float {24350}, Color {24351})
		{
			Global.Render.ItemsShader.RenderCircle(new Vector3({24349}.X, -1f, {24349}.Y), {24350}, {24350} + 1.2f, {24351} * 0.15f, GPUCircleType.SoftCircle, LocalContent.Loaded.CircleHoled);
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x000E62F8 File Offset: 0x000E44F8
		private static void DrawZoneEndIcon(in Vector2 {24352}, float {24353}, Rectangle {24354})
		{
			if ({24352} == Global.Player.Position)
			{
				return;
			}
			Vector2 vector = {24352} + (Global.Player.Position - {24352}).Normal() * {24353};
			float num = Vector2.Distance(vector, Global.Camera.Position.XZ());
			float num2 = 180f;
			if (num > num2)
			{
				return;
			}
			Vector2 {24359} = vector;
			float {24360} = 7f;
			Color color = Color.White * 0.7f * (1f - num / num2);
			EnvironmentGraphics.DrawBillboardIcon({24359}, {24360}, {24354}, color, false, true, null);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x000E639C File Offset: 0x000E459C
		private static void DrawZoneEndIcon(in Vector2 {24355}, Rectangle {24356}, float {24357} = 180f)
		{
			float num = Vector2.Distance({24355}, Global.Camera.Position.XZ());
			if (num > {24357})
			{
				return;
			}
			Vector2 {24359} = {24355};
			float {24360} = 7f;
			Color color = Color.White * 0.5f * (1f - num / {24357});
			EnvironmentGraphics.DrawBillboardIcon({24359}, {24360}, {24356}, color, false, true, null);
			Vector2 {24359}2 = {24355};
			float {24360}2 = 7f;
			color = Color.White * 0.5f * (1f - num / {24357});
			EnvironmentGraphics.DrawBillboardIcon({24359}2, {24360}2, {24356}, color, false, true, null);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x000E6434 File Offset: 0x000E4634
		private static void DrawIconFor(ref BuildingTarget {24358})
		{
			if ({24358}.EnvironmentIcon == null)
			{
				return;
			}
			Vector2 xz = Global.Camera.Position.XZ;
			Vector2 vector = {24358}.Center;
			if (Global.Camera.IsSpyglass)
			{
				vector += (xz - vector) * 0.15f;
			}
			if ({24358}.AsFactory != null)
			{
				EnvironmentGraphics.DrawInteropZoneCircle({24358}.AsFactory.InteropZone.C, {24358}.AsFactory.InteropZone.R, Color.White);
			}
			Vector2 {24359} = vector;
			float {24360} = (float)(({24358}.AsPort != null) ? 60 : 15);
			ref BuildingEnvIcon ptr = ref {24358}.EnvironmentIcon.Value;
			Color color = Color.White * 0.8f;
			EnvironmentGraphics.DrawBillboardIcon({24359}, {24360}, ptr.Path, color, false, {24358}.EnvironmentIcon.Value.Scale < 1f, {24358}.EnvironmentIcon.Value.CustomTex);
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000E6520 File Offset: 0x000E4720
		private static void DrawBillboardIcon(Vector2 {24359}, float {24360}, in Rectangle {24361}, in Color {24362}, bool {24363} = false, bool {24364} = false, Texture2D {24365} = null)
		{
			Vector2 xz = Global.Camera.Position.XZ;
			if (Vector2.DistanceSquared({24359}, xz) > (Renderer.CameraFarPlane - 100f) * (Renderer.CameraFarPlane - 100f))
			{
				return;
			}
			float num = Vector2.Distance({24359}, xz);
			Vector3 objectPosition;
			objectPosition.X = {24359}.X;
			objectPosition.Y = {24360};
			objectPosition.Z = {24359}.Y;
			if ({24365} != null)
			{
				Global.Render.ItemsShader.Texture.SetValue({24365});
			}
			EnvironmentGraphics.drawIcon.SetUV({24361}, ({24365} == null) ? AtlasObjs.Texture.Size : {24365}.Bounds.WidthHeight());
			float num2 = {24364} ? 0.8f : 2f;
			ParticlesAndStaticMesh itemsShader = Global.Render.ItemsShader;
			Matrix {15449} = Matrix.CreateScale(num2, -num2, num2) * Matrix.CreateRotationZ(1.5707964f) * Matrix.CreateBillboard(objectPosition, Global.Camera.Position, Vector3.Up, null);
			Vector4 {15450};
			if (!{24363})
			{
				Color color = {24362};
				{15450} = color.ToVector4() * (0.5f * Math.Min(1f, (Renderer.CameraFarPlane - 100f - num) / 50f));
			}
			else
			{
				Color color = {24362};
				{15450} = color.ToVector4();
			}
			itemsShader.SetForRender({15449}, {15450});
			Global.Render.ItemsShader.BeginPass(true, false);
			EnvironmentGraphics.drawIcon.Render();
			if ({24365} != null)
			{
				Global.Render.ItemsShader.Texture.SetValue(AtlasObjs.Texture.Tex);
			}
		}

		// Token: 0x04001827 RID: 6183
		private static BillboardParent_VPCT drawIcon;

		// Token: 0x04001828 RID: 6184
		private static BillboardParent_VPCT mapBorderLineRender;

		// Token: 0x04001829 RID: 6185
		internal const float questInSeaViewDist = 700f;

		// Token: 0x0400182A RID: 6186
		internal static readonly Rectangle pathBlades = new Rectangle(998, 236, 412, 409);

		// Token: 0x0400182B RID: 6187
		internal static readonly Rectangle pathFlag = new Rectangle(1347, 973, 213, 213);

		// Token: 0x0400182C RID: 6188
		internal static readonly Rectangle pathMine = new Rectangle(1413, 429, 212, 213);

		// Token: 0x0400182D RID: 6189
		internal static readonly Rectangle pathWarning = new Rectangle(1560, 973, 213, 212);

		// Token: 0x0400182E RID: 6190
		internal static readonly Rectangle pathHome = new Rectangle(1133, 995, 212, 213);

		// Token: 0x0400182F RID: 6191
		internal static readonly Rectangle pathTraderInSea = new Rectangle(1346, 645, 330, 327);

		// Token: 0x04001830 RID: 6192
		internal static readonly Rectangle pathPort = new Rectangle(998, 645, 348, 348);

		// Token: 0x04001831 RID: 6193
		internal static readonly Rectangle p_mapLayoutLine = new Rectangle(1711, 1270, 302, 132);

		// Token: 0x04001832 RID: 6194
		internal static readonly Rectangle pathQuestion = new Rectangle(2720, 1011, 197, 197);
	}
}
