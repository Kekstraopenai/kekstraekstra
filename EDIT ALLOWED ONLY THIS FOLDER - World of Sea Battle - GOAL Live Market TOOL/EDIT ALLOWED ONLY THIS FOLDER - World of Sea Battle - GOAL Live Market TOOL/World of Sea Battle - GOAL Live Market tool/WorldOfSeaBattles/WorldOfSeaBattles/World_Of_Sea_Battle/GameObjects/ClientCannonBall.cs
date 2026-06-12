using System;
using System.Collections.Generic;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Graphics;
using TheraEngine.Helpers;
using TheraEngine.Scene.Lighting;
using TheraEngine.Scene.ParticleSystem;
using UWPhysicsWOSLib;
using UWPhysicsWOSLib.Collision;
using UWPhysicsWOSLib.Shapes;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x0200003B RID: 59
	internal sealed class ClientCannonBall : CannonBall
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000C2EC File Offset: 0x0000A4EC
		public Vector3 Velo
		{
			get
			{
				return this.lastVelocity;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000C2F4 File Offset: 0x0000A4F4
		public ClientCannonBall()
		{
			this.{16575} = new BillboardParent_VPCT();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000C318 File Offset: 0x0000A518
		public override void ClearResources()
		{
			this.HitboxType = HitboxType.Corpus;
			this.IsDestructed = false;
			this.KindOfCollision = CannonBall.HitType.None;
			this.{16578} = false;
			this.{16579} = 0f;
			this.{16584} = 0f;
			this.IsFromBuilding = false;
			this.TargetUIDBomberShell = -1;
			this.HasNotEffects = false;
			this.{16585} = 0f;
			this.RemoveEffects();
			base.ClearResources();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000C384 File Offset: 0x0000A584
		protected override void FinalInitialize()
		{
			AtlasObjs.GetPathForCannonBall((int)base.BallInfo.ID, out this.{16577});
			if (base.BallInfo.AmmoType == CannonAmmoType.FalkonetBall)
			{
				if (!base.BallInfo.IsBoardingHook)
				{
					this.{16576} = FXEngine.CreateTrailForFalkonetBall(this.Sphere, (int)base.BallInfo.ID);
				}
				this.{16580} = (base.BallInfo.IsBoardingHook ? 0f : 1.5f);
				if (base.BallInfo.ID == 14)
				{
					this.{16580} *= 0.5f;
				}
			}
			else
			{
				if (!this.HasNotEffects)
				{
					bool isFromBuilding = this.IsFromBuilding;
					if (base.BallInfo.ID != 12)
					{
						this.{16576} = FXEngine.CreateTracerForCannonBall(this.Sphere, (int)base.BallInfo.ID, this.Effects);
						this.{16583} = new PointLight(this.Sphere + this.StartMomentNormal * 2f, FXEngine.GunLightColor, 1f, 24f)
						{
							DrawFlares = PointLightFlaresMode.Disable
						};
						Global.Render.Pointlights.Add(this.{16583});
					}
				}
				if (base.BallInfo.ID == 12)
				{
					this.{16580} = 0f;
				}
				else if (base.BallInfo.ID == 4)
				{
					this.{16580} = 0.5f;
				}
				else
				{
					this.{16580} = 0.6f;
				}
				if (this.IsFromBuilding)
				{
					this.{16580} *= 1.1f;
				}
				if (this.{16580} > 0f)
				{
					Vector3 startMomentNormal = this.StartMomentNormal;
					startMomentNormal.Y *= 0.66f;
					startMomentNormal.Normalize();
					Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID(base.SenderUID);
					Vector2? vector = (shipFromUID != null) ? new Vector2?(shipFromUID.physicsBody.VelocityPerSec / 60f) : null;
					FXEngine.GunEffect(this.Sphere, startMomentNormal, vector.GetValueOrDefault(Vector2.Zero), true, false, (this.ShotMode == CannonsAttackMode.SingleCannon) ? 0.2f : ((shipFromUID != null && shipFromUID.UsedShip.StaticInfo.LeftSidePorts.Length > 20) ? 1.2f : ((shipFromUID != null && shipFromUID.UsedShip.StaticInfo.LeftSidePorts.Length > 40) ? 1.5f : 1.1f)), shipFromUID);
					if (shipFromUID != null)
					{
						Player player = shipFromUID as Player;
						if (player != null && player.UsedShipPlayer.HavePowderItemsEffect && !this.HasNotEffects)
						{
							FXEngine.PowderGunEffect(this.Sphere, startMomentNormal);
						}
					}
				}
			}
			this.{16575}.SetPos(1f, 1f);
			this.{16575}.SetUV(this.{16577}, AtlasObjs.Texture.Size);
			this.{16582} = Color.Lerp(Color.Gray, Color.White, FXEngine.isNotDark);
			this.{16585} = (float)(this.IsFromBuilding ? 1200 : 450);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000C688 File Offset: 0x0000A888
		public void Update(ref FrameTime {16567})
		{
			if (this.{16579} > 500f && this.{16583} != null)
			{
				Global.Render.Pointlights.Remove(this.{16583});
				this.{16583} = null;
			}
			else if (this.{16583} != null)
			{
				this.{16583}.Intensivity = 1f - this.{16579} / 500f;
			}
			if (base.BallInfo.ID == 4 && !base.IsHiddenBomb)
			{
				this.{16580} += {16567}.secElapsed * 0.5f;
			}
			if (this.{16584} > 0f)
			{
				{16567}.EvaluteTimerMs(ref this.{16584});
				if (this.{16576} != null)
				{
					this.{16576}.IsEnabled = (this.{16584} > 0f);
				}
			}
			if (base.BallInfo.AmmoType == CannonAmmoType.FalkonetBall)
			{
				Ship {4419} = base.BallInfo.IsBoardingHook ? Global.Game.WorldInstance.GetShipFromUID(base.SenderUID) : null;
				base.UpdateFalkonetBall(ref {16567}, {4419});
			}
			else
			{
				base.UpdateCannonBall(ref {16567});
			}
			this.{16579} += {16567}.msElapsed;
			base.LifetimeCheck(Global.Player.MapInfo, out this.KindOfCollision, CommonGlobal.CurrentClientWeather);
			if (this.KindOfCollision != CannonBall.HitType.None)
			{
				this.CreateEffects(this.KindOfCollision);
				if (this.Effects.HasFlag(CannonBallExtraEffects.BombershellBoos))
				{
					if (this.KindOfCollision == CannonBall.HitType.BombExplosion)
					{
						this.IsDestructed = true;
					}
					else if (this.KindOfCollision == CannonBall.HitType.Collision && this.HitboxType != HitboxType.Mast)
					{
						Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID(this.TargetUIDBomberShell);
						if (shipFromUID != null)
						{
							base.OnBombCollision(shipFromUID);
						}
						else
						{
							base.OnBombCollision(Global.Player.MapInfo);
						}
					}
					else if (this.HitboxType != HitboxType.Mast)
					{
						base.OnBombCollision(Global.Player.MapInfo);
					}
					this.{16580} = 0f;
				}
				else
				{
					this.IsDestructed = true;
				}
			}
			this.CheckCollisionWithPlayer();
			if (this.{16576} != null)
			{
				this.{16576}.Center = this.Sphere - this.lastVelocity.Normal() * 1.2f;
				this.{16576}.PermStretch = this.lastVelocity.Normal() * 2.4f;
				this.{16576}.UseSubframeAlgorithm = true;
				if (this.lastVelocity.X + this.lastVelocity.Y + this.lastVelocity.Z != 0f)
				{
					this.{16576}.SubframeAlgorithmParticlesPerUnit = MathHelper.Lerp(1.1f, 0.6f, Math.Abs(Vector3.Dot(Engine.GS.Camera.Direction, this.lastVelocity.Normal())));
				}
			}
			if (base.BallInfo.ID == 12 && this.{16581}.Sample(ref {16567}) && this.{16585} > 0f)
			{
				Ship shipFromUID2 = Global.Game.WorldInstance.GetShipFromUID(base.SenderUID);
				if (shipFromUID2 != null)
				{
					if (!shipFromUID2.UsedShip.Cannons.HavingFireguns)
					{
						goto IL_383;
					}
					using (IEnumerator<CannonCommon> enumerator = ((IEnumerable<CannonCommon>)shipFromUID2.UsedShip.Cannons.Fireguns).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CannonCommon cannonCommon = enumerator.Current;
							FXEngine.SampleFiregunEffects(cannonCommon.GetPosition(shipFromUID2.Transform), cannonCommon.GameInfo.MaxDistance, this, shipFromUID2);
						}
						goto IL_383;
					}
				}
				if (this.IsFromBuilding)
				{
					FXEngine.SampleFiregunEffects(base.StartPosition, 120f, this, null);
				}
			}
			IL_383:
			{16567}.EvaluteTimerMs(ref this.{16585});
			if (!this.{16578})
			{
				Vector3 vector = Global.Camera.Position - this.Sphere;
				float num = 5f;
				if (vector.LengthSquared() < num * num && Vector3.Dot(Global.Camera.Direction, this.lastVelocity.Normal()) < 0f)
				{
					if (vector.Y < -0.1f)
					{
						Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.CannonballFlyLow, this.Sphere, 0.5f, false);
					}
					else if (vector.Y > 0.2f)
					{
						Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.CannonballFlyHight, this.Sphere, 0.5f, false);
					}
					else
					{
						Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.CannonballFlyMiddle, this.Sphere, 0.5f, false);
					}
					this.{16578} = true;
				}
			}
			if (this.Effects.HasFlag(CannonBallExtraEffects.BombershellBoos) && Rand.Chanse(10f * {16567}.Factor) && !base.IsHiddenBomb && this.{16584} == 0f)
			{
				FXEngine.CreateSparkParticle(this.Sphere, Vector3.Up, Vector3.Up * 2f);
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		public void AddGeometry(ref Vector3 {16568}, SpriteBatch3D<VertexPositionColorTexture> {16569})
		{
			if (this.{16580} == 0f)
			{
				return;
			}
			Vector3 sphere = this.Sphere;
			Matrix matrix;
			Matrix.CreateBillboard(ref sphere, ref {16568}, ref ClientCannonBall.v3up, null, out matrix);
			if (base.BallInfo.ID == 3)
			{
				matrix = Matrix.CreateRotationY((float)HashHelper.greaterInt(this.uID, 3) * this.{16579} / 500f * (float)(HashHelper.greaterBool(this.uID * 11) ? 1 : -1)) * Matrix.CreateRotationZ((float)HashHelper.greaterInt(this.uID, 3) * this.{16579} / 500f * (float)(HashHelper.greaterBool(this.uID * 11) ? 1 : -1)) * matrix;
			}
			if (base.BallInfo.ID == 4)
			{
				matrix = Matrix.CreateRotationZ((float)HashHelper.greaterInt(this.uID, 3) * this.{16579} / 1000f * (float)(HashHelper.greaterBool(this.uID * 11) ? 1 : -1)) * matrix;
			}
			float x;
			Vector3.Distance(ref {16568}, ref sphere, out x);
			float num = 0.7f + ((base.BallInfo.ID == 4) ? 0.1f : (MathF.Sqrt(x) / 25f));
			this.{16575}.SetPos(this.{16580} * num, this.{16580} * num);
			this.{16575}.Transform(matrix);
			this.{16575}.SetCol(this.{16582} * ((this.{16584} > 0f) ? Geometry.Saturate(1f - this.{16584} / 400f) : 1f));
			{16569}.Add(this.{16575}.array);
			if (base.BallInfo.ID == 4)
			{
				for (int i = 0; i < 3; i++)
				{
					Vector3 value = sphere;
					value += new Vector3(HashHelper.greater(i + this.uID) - 0.5f, 0f, HashHelper.greater(1000 + i + this.uID) - 0.5f) * this.{16579} / 1000f * 4f;
					Matrix.CreateBillboard(ref value, ref {16568}, ref ClientCannonBall.v3up, null, out matrix);
					matrix = Matrix.CreateRotationZ(0.7f * (float)i + this.{16579} / 1200f * (float)(HashHelper.greaterBool((this.uID + i) * 11) ? -1 : 1)) * matrix;
					this.{16575}.SetPos(this.{16580} * num * 1.3f, this.{16580} * num * 1.3f);
					this.{16575}.Transform(matrix);
					{16569}.Add(this.{16575}.array);
				}
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000CE48 File Offset: 0x0000B048
		public void CreateEffects(CannonBall.HitType {16570})
		{
			this.RemoveEffects();
			if ({16570} == CannonBall.HitType.HitsWithWater)
			{
				if (base.BallInfo.AmmoType == CannonAmmoType.FalkonetBall)
				{
					FXEngine.FalkonetHitsWithWater(this.Sphere, (int)base.BallInfo.ID);
				}
				if (base.BallInfo[CannonBallInfoEffects.FireArea])
				{
					Global.Game.WorldInstance.ContactFireArea(this.Sphere.XZ());
					FXEngine.SampleFlameAndSmoke(this.Sphere, 1f, true, false, true, null, 1f);
				}
			}
			if ({16570} != CannonBall.HitType.Collision)
			{
				FXEngine.HitCannonBallExtraEffects({16570}, this);
			}
			if ({16570} != CannonBall.HitType.HitsWithWater && base.BallInfo.ID == 18)
			{
				FXEngine.FireworkExplosion(this.Sphere, true);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000CEF6 File Offset: 0x0000B0F6
		public void RemoveEffects()
		{
			if (this.{16576} != null)
			{
				this.{16576}.Remove();
				this.{16576} = null;
			}
			if (this.{16583} != null)
			{
				Global.Render.Pointlights.Remove(this.{16583});
				this.{16583} = null;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000CF38 File Offset: 0x0000B138
		public void CheckCollisionWithPlayer()
		{
			if (this.{16584} > 0f || Global.Player.uID == base.SenderUID || base.BallInfo[CannonBallInfoEffects.Overpenetration])
			{
				return;
			}
			Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID(base.SenderUID);
			ShipOtherPlayer shipOtherPlayer = shipFromUID as ShipOtherPlayer;
			if (shipOtherPlayer == null || !shipOtherPlayer.MakeTransparentForMe)
			{
				ShipNpc shipNpc = shipFromUID as ShipNpc;
				if (shipNpc == null || !shipNpc.MakeTransparentForMe)
				{
					ShipAndBallCollisionTestResult shipAndBallCollisionTestResult;
					Global.Player.CannonBallCollisionTest(this, out shipAndBallCollisionTestResult);
					if (shipAndBallCollisionTestResult.BasicResult.IsCollide && (shipAndBallCollisionTestResult.NodeType == HitboxType.Corpus || shipAndBallCollisionTestResult.NodeType == HitboxType.Mast || shipAndBallCollisionTestResult.NodeType == HitboxType.Sail))
					{
						this.{16584} = 300f;
					}
					return;
				}
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		public bool CheckCollisionWithBuildings(out CannonBall.HitType {16571}, out bool {16572}, out int {16573}, Tlist<Isle> {16574})
		{
			{16572} = false;
			{16573} = -1;
			{16571} = CannonBall.HitType.None;
			if (this.IsFromBuilding)
			{
				return false;
			}
			new BoundingSphere(this.Sphere, 0.5f + 1.5f * this.lastVelocity.Length());
			for (int i = 0; i < {16574}.Size; i++)
			{
				Isle isle = {16574}.Array[i];
				if (Vector3.DistanceSquared(isle.Statement.ModelGlobalBS.Center, this.Sphere) < (isle.Statement.ModelGlobalBS.Radius + 1f) * (isle.Statement.ModelGlobalBS.Radius + 1f))
				{
					Matrix matrix;
					Matrix.Invert(ref isle.Statement.GlobalTransformWorld, out matrix);
					CollisionTestResult collisionTestResult;
					CollisionCore.RayWithMesh(new LineShape(this.Sphere - this.lastVelocity, this.Sphere + this.lastVelocity, 1f), isle.Statement.ModelData.MeshShape, ref isle.Statement.GlobalTransformWorld, ref matrix, out collisionTestResult);
					{16572} = true;
					if (collisionTestResult.IsCollide)
					{
						this.HitPoint = collisionTestResult.CollideCentr;
						this.HitMaterial = HitMaterialEffect.Stone;
						{16571} = CannonBall.HitType.Collision;
						{16573} = {16574}.Array[i].DynamicObjectUID.GetValueOrDefault(-1);
						this.HasBuildingDamage = ({16573} > 0);
						FXEngine.HitCannonBallExtraEffects(CannonBall.HitType.Collision, this);
						if (base.SenderUID == Global.Player.uID && {16573} > 0)
						{
							CannonCommon cannonCommon = Global.Player.UsedShip.Cannons.FindByLocation((int)this.SourceCannonLocationId);
							if (cannonCommon != null)
							{
								cannonCommon.ClientHitEffectMs = CannonsController.SightHitEffectDurationMs;
							}
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000142 RID: 322
		private static Vector3 v3up = Vector3.Up;

		// Token: 0x04000143 RID: 323
		private BillboardParent_VPCT {16575};

		// Token: 0x04000144 RID: 324
		private ParticleSystem3D {16576};

		// Token: 0x04000145 RID: 325
		private Rectangle {16577};

		// Token: 0x04000146 RID: 326
		private bool {16578};

		// Token: 0x04000147 RID: 327
		private float {16579};

		// Token: 0x04000148 RID: 328
		private float {16580};

		// Token: 0x04000149 RID: 329
		private Timer {16581} = new Timer(30f);

		// Token: 0x0400014A RID: 330
		private Color {16582};

		// Token: 0x0400014B RID: 331
		public Vector3 HitPoint;

		// Token: 0x0400014C RID: 332
		public SpecificDamageFlags DamageFlags;

		// Token: 0x0400014D RID: 333
		public bool IsDestructed;

		// Token: 0x0400014E RID: 334
		public CannonBall.HitType KindOfCollision;

		// Token: 0x0400014F RID: 335
		public float HitDamage;

		// Token: 0x04000150 RID: 336
		public HitboxType HitboxType;

		// Token: 0x04000151 RID: 337
		public HitMaterialEffect HitMaterial;

		// Token: 0x04000152 RID: 338
		public bool IsFromBuilding;

		// Token: 0x04000153 RID: 339
		public bool HasNotEffects;

		// Token: 0x04000154 RID: 340
		public bool HasBuildingDamage;

		// Token: 0x04000155 RID: 341
		public int TargetUIDBomberShell;

		// Token: 0x04000156 RID: 342
		private PointLight {16583};

		// Token: 0x04000157 RID: 343
		private float {16584};

		// Token: 0x04000158 RID: 344
		private float {16585};
	}
}
