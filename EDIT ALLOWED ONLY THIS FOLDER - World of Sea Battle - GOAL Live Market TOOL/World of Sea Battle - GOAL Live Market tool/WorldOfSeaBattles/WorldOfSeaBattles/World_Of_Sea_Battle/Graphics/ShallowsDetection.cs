using System;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;

namespace World_Of_Sea_Battle.Graphics
{
	// Token: 0x0200044C RID: 1100
	public class ShallowsDetection
	{
		// Token: 0x060017E6 RID: 6118 RVA: 0x000CEB57 File Offset: 0x000CCD57
		public ShallowsDetection()
		{
			this.{23467} = new float[8];
			this.{23468} = new bool[8];
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x000CEB77 File Offset: 0x000CCD77
		public bool AnyBlockings
		{
			get
			{
				return this.{23469};
			}
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x000CEB80 File Offset: 0x000CCD80
		public void Update(ref FrameTime {23464})
		{
			ShallowsDetection.FenceDetected = false;
			if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
			{
				return;
			}
			Array.Clear(this.{23467}, 0, this.{23467}.Length);
			Array.Clear(this.{23468}, 0, this.{23468}.Length);
			this.{23469} = false;
			this.{23470} = default(Vector2);
			Vector3 position3D = Global.Player.Position3D;
			Vector3 position3D2 = Global.Player.Position3D;
			for (int i = 0; i < Global.Game.WorldInstance.MapVisibleObjectLayer.Size; i++)
			{
				Isle isle = Global.Game.WorldInstance.MapVisibleObjectLayer.Array[i];
				float radius = isle.Statement.ModelGlobalBS.Radius;
				if (Vector3.DistanceSquared(isle.Statement.ModelGlobalBS.Center, position3D2) <= (radius + 65f) * (radius + 65f) && isle.Statement.TransformedShallowsPoints != null && isle.Statement.GroupID != WorldObjectID.WPort && isle.Statement.ModelGlobalBS.Radius <= 400f)
				{
					float num = 60f;
					float num2 = 30f;
					for (int j = 0; j < isle.Statement.TransformedShallowsPoints.Size; j++)
					{
						Vector3 vector = isle.Statement.TransformedShallowsPoints.Array[j].X0Y();
						float num3;
						Vector3.DistanceSquared(ref position3D, ref vector, out num3);
						if (num3 < num * num)
						{
							Vector2 vector2;
							vector2.X = vector.X - Global.Player.Position.X;
							vector2.Y = vector.Z - Global.Player.Position.Y;
							vector2.Normalize();
							float num4 = 1f - MathHelper.Clamp((MathF.Sqrt(num3) - num2) / (num - num2), 0f, 1f);
							int num5 = (int)MathHelper.Clamp(MathHelper.Clamp((MathF.Atan2(vector2.Y, vector2.X) + 3.1415927f) / 6.2831855f, 0f, 1f) * 8f, 0f, 7f);
							if (!this.{23468}[num5])
							{
								this.{23467}[num5] = num4;
								this.{23468}[num5] = true;
							}
							else
							{
								this.{23467}[num5] = Math.Max(this.{23467}[num5], num4);
							}
							if (Vector2.Dot(Global.Player.Normal, vector2) > 0.5f)
							{
								this.{23470} += vector2 * num4 * num4;
							}
						}
					}
				}
			}
			Vector2 value = Engine.GS.Camera.Direction.XZNormal();
			if (this.{23470}.Length() > 0.1f)
			{
				this.{23470}.Normalize();
				if (Vector2.Dot(Global.Player.Normal, this.{23470}) > 0.84f && Vector2.Dot(Global.Player.Normal, value) < 0.4f && Global.Player.NowSpeed > 5f)
				{
					ShallowsDetection.FenceDetected = true;
				}
			}
			if (!ShallowsDetection.FenceDetected && Vector2.Dot(Global.Player.Normal, value) < 0.4f)
			{
				foreach (Ship ship in Global.Game.WorldInstance.EnumerateShips(true, true, false))
				{
					ShipPositionInfo shipPositionInfo = ship.ReconstructPosition(1500f, ship.GetShipPositionInfo);
					if (Vector2.Dot((ship.Position - Global.Player.Position).Normal(), Global.Player.Normal) > 0.8f && Global.Player.Position.DTest2(shipPositionInfo.Position, Global.Player.UsedShip.StaticInfo.CorpusHalfLength + ship.UsedShip.StaticInfo.CorpusHalfLength + 20f) && !Global.Player.Position.DTest2(shipPositionInfo.Position, Global.Player.UsedShip.StaticInfo.CorpusHalfLength + ship.UsedShip.StaticInfo.CorpusHalfLength))
					{
						ShallowsDetection.FenceDetected = true;
					}
				}
			}
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x000CF000 File Offset: 0x000CD200
		private bool {23465}()
		{
			float num = 0f;
			for (int i = 0; i < this.{23467}.Length; i++)
			{
				num += (this.{23468}[i] ? 1f : 0f) / (float)this.{23467}.Length;
			}
			return num >= 0.75f;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x000070D7 File Offset: 0x000052D7
		public bool CheckForAllowSpeedUp(int {23466})
		{
			return true;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x000CF054 File Offset: 0x000CD254
		public void Render2D()
		{
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x000CF064 File Offset: 0x000CD264
		public void Render3DStatics()
		{
			if (Global.Player.IsInShallowWater > 0f)
			{
				WorldBitmap shallows = Global.Player.MapInfo.Shallows;
				Vector2 position = Global.Player.Position;
				Vector2 vector = shallows.FindGradient(position, delegate(byte {23471})
				{
					if (Global.Player.UsedShipPlayer.CraftFrom.Rank >= (int){23471})
					{
						return 0f;
					}
					return 1f;
				});
				if (vector.LengthSquared() != 0f)
				{
					float num = MathF.Atan2(vector.Y, vector.X);
					float num2 = Global.Player.UsedShip.StaticInfo.BSRadius * 0.5f;
					Global.Render.ItemsShader.RenderSector(new Vector3(Global.Player.Position.X, -100f, Global.Player.Position.Y), num2, num2 + 0.5f, num, 0.35342917f, Color.DarkGray, Color.Orange * 0.5f, false);
					Global.Render.ItemsShader.RenderSector(new Vector3(Global.Player.Position.X, -100f, Global.Player.Position.Y), num2, num2 + 0.5f, num - 0.70685834f, 0.35342917f, Color.DarkGray, Color.Orange * 0.5f, false);
					Global.Render.ItemsShader.RenderSector(new Vector3(Global.Player.Position.X, -100f, Global.Player.Position.Y), num2, num2 + 0.5f, num + 0.70685834f, 0.35342917f, Color.DarkGray, Color.Orange * 0.5f, false);
				}
			}
		}

		// Token: 0x04001671 RID: 5745
		private float[] {23467};

		// Token: 0x04001672 RID: 5746
		private bool[] {23468};

		// Token: 0x04001673 RID: 5747
		public static bool FenceDetected;

		// Token: 0x04001674 RID: 5748
		private bool {23469};

		// Token: 0x04001675 RID: 5749
		private Vector2 {23470};
	}
}
