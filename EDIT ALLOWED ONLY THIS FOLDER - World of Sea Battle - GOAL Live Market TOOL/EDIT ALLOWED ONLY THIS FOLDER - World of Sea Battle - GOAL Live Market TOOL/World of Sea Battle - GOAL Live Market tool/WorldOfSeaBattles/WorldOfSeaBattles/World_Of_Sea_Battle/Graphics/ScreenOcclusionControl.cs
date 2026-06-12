using System;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;

namespace World_Of_Sea_Battle.Graphics
{
	// Token: 0x02000435 RID: 1077
	internal static class ScreenOcclusionControl
	{
		// Token: 0x06001734 RID: 5940 RVA: 0x000C604F File Offset: 0x000C424F
		private static bool IsHitVolume(ref ScreenOcclusionControl.ssVolume {23128})
		{
			return Vector2.DistanceSquared({23128}.CenterOcclusion, Engine.GS.MouseToUI) < ({23128}.SizeOcclusion + 10f) * ({23128}.SizeOcclusion + 10f);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x000C6084 File Offset: 0x000C4284
		private static void TryAppendCache(Vector3 {23129}, float {23130}, object {23131})
		{
			if (Engine.GS.Camera.IsVisible({23129}, {23130}))
			{
				Vector2 vector = Engine.GS.Camera.GetProjection(ref {23129}) * Engine.Game.WindowSize / Engine.GS.CurrentOutputSize;
				{23129}.Y += {23130};
				Vector2 value = Engine.GS.Camera.GetProjection(ref {23129}) * Engine.Game.WindowSize / Engine.GS.CurrentOutputSize;
				ScreenOcclusionControl.ssVolume ssVolume = new ScreenOcclusionControl.ssVolume
				{
					CenterOcclusion = vector,
					SizeOcclusion = Vector2.Distance(vector, value),
					Object = {23131},
					Distance = Vector3.Distance(Engine.GS.Camera.Position, {23129})
				};
				if (ScreenOcclusionControl.IsHitVolume(ref ssVolume))
				{
					ScreenOcclusionControl.volumesCache.Add(ssVolume);
				}
			}
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x000C6170 File Offset: 0x000C4370
		public static ScreenOcclusionControl.Result SelectedVisualItem(Ship {23132})
		{
			Matrix matrix;
			{23132}.Transform.CreateWorldMatrix(out matrix);
			for (int i = 0; i < {23132}.UsedShip.Cannons.Items.Size; i++)
			{
				CannonCommon cannonCommon = {23132}.UsedShip.Cannons.Items.Array[i];
				ScreenOcclusionControl.TryAppendCache(cannonCommon.GetPosition(ref matrix), 0.2f, cannonCommon.Location);
			}
			foreach (CannonLocationInfo cannonLocationInfo in {23132}.UsedShip.StaticInfo.MortarPorts)
			{
				if ({23132}.UsedShip.Mortars[(int)cannonLocationInfo.SectionID] != null)
				{
					ScreenOcclusionControl.TryAppendCache(Vector3.Transform(cannonLocationInfo.Position, matrix), 0.3f, cannonLocationInfo);
				}
			}
			for (int k = 0; k < {23132}.UsedShip.StaticInfo.FalkonetPositions.Length; k++)
			{
				Vector3 {23129};
				Vector3.Transform(ref {23132}.UsedShip.StaticInfo.FalkonetPositions[k].LocalPosition, ref matrix, out {23129});
				ScreenOcclusionControl.TryAppendCache({23129}, 0.15f, {23132}.UsedShip.StaticInfo.FalkonetPositions[k]);
			}
			if (ScreenOcclusionControl.volumesCache.Size == 0)
			{
				return new ScreenOcclusionControl.Result
				{
					Reference = null,
					SSPosition = Vector2.Zero
				};
			}
			float num = float.MaxValue;
			object reference = null;
			Vector2 ssposition = Vector2.Zero;
			for (int l = 0; l < ScreenOcclusionControl.volumesCache.Size; l++)
			{
				ScreenOcclusionControl.ssVolume ssVolume = ScreenOcclusionControl.volumesCache.Array[l];
				if (ssVolume.Distance < num)
				{
					reference = ssVolume.Object;
					num = ssVolume.Distance;
					ssposition = ssVolume.CenterOcclusion;
				}
			}
			ScreenOcclusionControl.volumesCache.Clear();
			return new ScreenOcclusionControl.Result
			{
				Reference = reference,
				SSPosition = ssposition
			};
		}

		// Token: 0x0400154D RID: 5453
		public static readonly Vector4 DefaultShipElementOutlineColor = new Vector4(1.1f, 0.42f, 0f, 1f);

		// Token: 0x0400154E RID: 5454
		private static Tlist<ScreenOcclusionControl.ssVolume> volumesCache = new Tlist<ScreenOcclusionControl.ssVolume>();

		// Token: 0x02000436 RID: 1078
		private struct ssVolume
		{
			// Token: 0x0400154F RID: 5455
			public Vector2 CenterOcclusion;

			// Token: 0x04001550 RID: 5456
			public float SizeOcclusion;

			// Token: 0x04001551 RID: 5457
			public object Object;

			// Token: 0x04001552 RID: 5458
			public float Distance;
		}

		// Token: 0x02000437 RID: 1079
		public struct Result
		{
			// Token: 0x04001553 RID: 5459
			public object Reference;

			// Token: 0x04001554 RID: 5460
			public Vector2 SSPosition;
		}
	}
}
