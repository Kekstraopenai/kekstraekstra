using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Grphics.Device;
using TheraEngine.Input;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics.Shaders;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle
{
	// Token: 0x02000012 RID: 18
	public class Debugging
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000030DF File Offset: 0x000012DF
		// (set) Token: 0x06000056 RID: 86 RVA: 0x000030E6 File Offset: 0x000012E6
		public static bool EnableOverlay { get; set; } = true;

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000030EE File Offset: 0x000012EE
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000030F5 File Offset: 0x000012F5
		public static bool DebugInfo { get; set; } = false;

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool DisplayShipPhysics
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool DisplayOcclisonQueryPosition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool DisplayNpcFences
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool DisplayWaterHeight
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool DisplayShallowPoints
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool DisplayCpuWater
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool DisplayDropOnIslePositions
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool SessionMapEditor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool SessionMapEditorTowers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool EnableWaterShipParamsEditor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool CrewDebnugging
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool ShowDropIslePositions
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool ShipWaterlineByArrowEditor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool ForceProdServers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000030FD File Offset: 0x000012FD
		public static bool SteamTesting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003100 File Offset: 0x00001300
		private static void NKeyPressed()
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003104 File Offset: 0x00001304
		public static void Update()
		{
			if (InputHelper.IsClick(Keys.N))
			{
				Debugging.NKeyPressed();
			}
			if (Debugging.ShipWaterlineByArrowEditor)
			{
				float num = 0f;
				if (InputHelper.IsClick(Keys.Up))
				{
					num = 0.05f;
				}
				if (InputHelper.IsClick(Keys.Down))
				{
					num = -0.05f;
				}
				if (num != 0f)
				{
					Global.Player.UsedShip.StaticInfo.WaterlineOffset += num;
					{19988} {19997} = {19988}.Info;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 1);
					defaultInterpolatedStringHandler.AppendLiteral("WaterlineOffset: ");
					defaultInterpolatedStringHandler.AppendFormatted<float>(Global.Player.UsedShip.StaticInfo.WaterlineOffset, "F2");
					{19994}.Me({19997}, defaultInterpolatedStringHandler.ToStringAndClear(), Array.Empty<object>());
				}
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000031BC File Offset: 0x000013BC
		public static void Draw2D()
		{
			if (Debugging.DisplayWaterHeight)
			{
				for (int i = -25; i <= 25; i++)
				{
					for (int j = -25; j <= 25; j++)
					{
						Vector2 {11452} = Engine.GS.Camera.Position.XZ() + new Vector2((float)(i * 6), (float)(j * 6));
						float num = CommonGlobal.CurrentClientWeather.WavesHeightIntrenal(Global.Player.MapInfo, {11452});
						Vector3 vector = {11452}.X0Y() + new Vector3(0f, num * 10f, 0f);
						if (Vector3.Dot(Engine.GS.Camera.Position - vector, Engine.GS.Camera.Direction) < 0f)
						{
							Vector2 projection = Engine.GS.Camera.GetProjection(ref vector);
							Device gs = Engine.GS;
							Vector2 zero = Vector2.Zero;
							gs.Draw(AtlasObjs.whitepixel_1px, projection, zero, 0f, 30f / (1f + 0.025f * Vector3.Distance(Engine.GS.Camera.Position, vector)));
						}
					}
				}
			}
			if (Debugging.CrewDebnugging)
			{
				foreach (CrewConnector.Place place in ((IEnumerable<CrewConnector.Place>)Global.Player.Client.Scene.CrewInstancesByPlaces.Places))
				{
					Vector3 vector2 = Global.Player.Transform.Transform3X3(place.PosAtShip.Item1);
					if (Vector3.Dot(Engine.GS.Camera.Position - vector2, Engine.GS.Camera.Direction) < 0f)
					{
						Vector2 projection2 = Engine.GS.Camera.GetProjection(ref vector2);
						Device gs2 = Engine.GS;
						Vector2 zero = Vector2.Zero;
						gs2.Draw(AtlasObjs.whitepixel_1px, projection2, zero, 0f, 25f / (1f + 0.025f * Vector3.Distance(Engine.GS.Camera.Position, vector2)));
						if (place.CrewOrNull != null && place.CrewOrNull.walkTarget != null)
						{
							vector2 = Global.Player.Transform.Transform3X3(place.CrewOrNull.Model.LocalTransformOrNull.Translation);
							projection2 = Engine.GS.Camera.GetProjection(ref vector2);
							Vector3 vector3 = Global.Player.Transform.Transform3X3(place.CrewOrNull.walkTarget.Value);
							Vector2 projection3 = Engine.GS.Camera.GetProjection(ref vector3);
							Engine.GS.Line2D(AtlasObjs.whitepixel_1px, projection2, projection3, Color.Red, 2);
						}
					}
				}
			}
			if (Debugging.DisplayShallowPoints)
			{
				foreach (Isle isle in ((IEnumerable<Isle>)Global.Game.WorldInstance.MapObjectLayer))
				{
					if (isle.IsVisibleByDist && isle.Statement.TransformedShallowsPoints != null)
					{
						foreach (Vector2 vector4 in ((IEnumerable<Vector2>)isle.Statement.TransformedShallowsPoints))
						{
							Vector3 vector5 = new Vector3(vector4.X, 0f, vector4.Y);
							if (Vector3.Dot(Engine.GS.Camera.Position - vector5, Engine.GS.Camera.Direction) < 0f)
							{
								Vector2 projection4 = Engine.GS.Camera.GetProjection(ref vector5);
								Device gs3 = Engine.GS;
								Vector2 zero = Vector2.Zero;
								gs3.Draw(AtlasObjs.whitepixel_1px, projection4, zero, 0f, 120f / (1f + 0.025f * Vector3.Distance(Engine.GS.Camera.Position, vector5)));
							}
						}
					}
				}
			}
			if (Debugging.DisplayDropOnIslePositions)
			{
				foreach (Vector2 vector6 in ((IEnumerable<Vector2>)Gameplay.WorldMap.PositionsForDropInIsle))
				{
					Vector3 vector7 = new Vector3(vector6.X, 0f, vector6.Y);
					if (Vector3.Dot(Engine.GS.Camera.Position - vector7, Engine.GS.Camera.Direction) < 0f)
					{
						Vector2 projection5 = Engine.GS.Camera.GetProjection(ref vector7);
						Device gs4 = Engine.GS;
						Vector2 zero = Vector2.Zero;
						gs4.Draw(AtlasObjs.whitepixel_1px, projection5, zero, 0f, 120f / (1f + 0.025f * Vector3.Distance(Engine.GS.Camera.Position, vector7)));
					}
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003720 File Offset: 0x00001920
		public static void ShowIngamePollWindow()
		{
			new {22659}();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003728 File Offset: 0x00001928
		internal static void Draw3D()
		{
			if (Debugging.DisplayNpcFences)
			{
				Tlist<Isle> mapObjectLayer = Global.Game.WorldInstance.MapObjectLayer;
				for (int i = 0; i < mapObjectLayer.Size; i++)
				{
					foreach (FenceSphere fenceSphere in ((IEnumerable<FenceSphere>)mapObjectLayer.Array[i].Statement.TransformedFenceSpheres))
					{
						LocalContent.Loaded.DebugSphereDisplay.Transform.Translation = new Vector3(fenceSphere.C.X, 3f, fenceSphere.C.Y);
						LocalContent.Loaded.DebugSphereDisplay.Transform.MiddleScale = fenceSphere.R;
						Global.Render.CommonShader.RenderObject(LocalContent.Loaded.DebugSphereDisplay, false, 1f, false, 0f, false);
					}
				}
			}
			if (Debugging.DisplayCpuWater && Global.Player != null)
			{
				for (int j = 0; j < 100; j++)
				{
					for (int k = 0; k < 100; k++)
					{
						Vector3 vector = Global.Player.Position3D + new Vector3((float)(j - 50), 0f, (float)(k - 50));
						vector.Y = CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, vector.X, vector.Z);
						LocalContent.Loaded.DebugSphereDisplay.Transform.Translation = vector;
						LocalContent.Loaded.DebugSphereDisplay.Transform.MiddleScale = 0.5f;
						Global.Render.CommonShader.RenderObject(LocalContent.Loaded.DebugSphereDisplay, false, 1f, false, 0f, false);
					}
				}
			}
			if (Debugging.DisplayShipPhysics && Global.Player != null)
			{
				foreach (ShipPhysics.SupportUnit supportUnit in Global.Player.physicsBody.SupportUnits)
				{
					LocalContent.Loaded.DebugSphereDisplay.Transform.Translation = new Vector3(supportUnit.GlobalPosLast.X, supportUnit.CurrentHeight, supportUnit.GlobalPosLast.Y);
					LocalContent.Loaded.DebugSphereDisplay.Transform.MiddleScale = 1.5f;
					Global.Render.CommonShader.RenderObject(LocalContent.Loaded.DebugSphereDisplay, false, 1f, false, 0f, false);
				}
			}
			if (Debugging.DisplayOcclisonQueryPosition)
			{
				foreach (Ship ship in Global.Game.WorldInstance.EnumerateShips(true, true, true))
				{
					Vector3 queryPosition = ((IClientShip)ship).GetClient.queryPosition;
					LocalContent.Loaded.DebugSphereDisplay.Transform.Translation = queryPosition;
					LocalContent.Loaded.DebugSphereDisplay.Transform.MiddleScale = 3f;
					Global.Render.CommonShader.RenderObject(LocalContent.Loaded.DebugSphereDisplay, false, 1f, false, 0f, false);
				}
			}
		}

		// Token: 0x04000023 RID: 35
		public const bool IsTestClient = false;

		// Token: 0x04000026 RID: 38
		private static Tlist<long[]> tlist = new Tlist<long[]>();
	}
}
