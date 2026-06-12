using System;
using System.Collections.Generic;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Collections;
using TheraEngine.Graphics;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using TheraEngine.Scene.ParticleSystem;
using UWPhysicsWOSLib;
using UWPhysicsWOSLib.Collision;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x0200003F RID: 63
	internal sealed class ClientMortarShot : MortarShot
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public ClientMortarShot()
		{
			this.{16616} = new BillboardParent_VPCT();
			this.{16618} = new Tlist<ParticleSystem3D>();
			this.{16617} = new Vector3[6];
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		protected override void FinalInitialize()
		{
			this.CollisionWithObject = false;
			this.{16613} = this.CurrentPosition;
			if (this.Shot.Feature == CannonFeature.HeavyMortar && this.Shot.BallInfo.ID == 6)
			{
				Tlist<ParticleSystem3D> tlist = this.{16618};
				ParticleSystem3D particleSystem3D = FXEngine.CreateTracerForCannonBall(this.CurrentPosition, 2, CannonBallExtraEffects.None);
				tlist.Add(particleSystem3D);
				this.{16618}.Array[0].ParticleEffect.Array[0].SingleInitialSize = new Range1D(0.8f, 1.6f);
				this.{16618}.Array[0].ParticleEffect.Array[0].SingleTtl = new Range1D(600f, 1000f);
			}
			short id = this.Shot.BallInfo.ID;
			if (this.Shot.BallInfo.ID == 8)
			{
				for (int i = 0; i < this.{16617}.Length; i++)
				{
					Tlist<ParticleSystem3D> tlist2 = this.{16618};
					ParticleSystem3D particleSystem3D = FXEngine.CreateTracerForCannonBall(this.CurrentPosition, 2, CannonBallExtraEffects.None);
					tlist2.Add(particleSystem3D);
				}
			}
			this.{16615} = Matrix.CreateFromYawPitchRoll(Rand.Angle(), Rand.Angle(), Rand.Angle());
			this.{16616}.SetPos(1f, 1f);
			this.{16616}.SetCol(Color.Lerp(Color.Gray, Color.White, Global.Game.StaticSystem.GetSkyShader.DayOrNight));
			if (this.Shot.BallInfo.ID != 20)
			{
				this.{16603}();
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000E565 File Offset: 0x0000C765
		public override void ClearResources()
		{
			this.{16612}();
			this.{16619} = false;
			this.{16620} = false;
			this.{16614} = 0f;
			base.ClearResources();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000E58C File Offset: 0x0000C78C
		private void {16603}()
		{
			if (this.{16620})
			{
				return;
			}
			this.{16620} = true;
			Global.Game.SoundSystem.Play3DSound((this.Shot.Feature == CannonFeature.PowderKegMortar) ? GameDynamicSoundName.MortarsGunKeg : ((Vector3.Distance(this.Shot.StartPosition, Engine.GS.Camera.Position) > 150f) ? GameDynamicSoundName.MortarsGunFar : GameDynamicSoundName.MortarsGun), this.Shot.StartPosition, 1f, false);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000E608 File Offset: 0x0000C808
		public bool Update(Tlist<Isle> {16604}, ref FrameTime {16605})
		{
			if (!base.IsWaitingSupermortar)
			{
				this.{16603}();
			}
			for (int i = 0; i < this.{16617}.Length; i++)
			{
				float scaleFactor = 25f * this.{16614} / 5f;
				Vector3 value = ClientMortarShot.kernel[i % ClientMortarShot.kernel.Length];
				Vector3.Transform(ref value, ref this.{16615}, out value);
				value.Y *= Geometry.Saturate(this.CurrentPosition.Y / 30f);
				this.{16617}[i] = this.CurrentPosition + value * scaleFactor;
			}
			int num = 0;
			foreach (ParticleSystem3D particleSystem3D in ((IEnumerable<ParticleSystem3D>)this.{16618}))
			{
				particleSystem3D.CountPerSecound = 50f;
				if (this.{16618}.Size == 1)
				{
					particleSystem3D.Center = this.CurrentPosition;
				}
				else
				{
					particleSystem3D.Center = this.{16617}[num++];
					particleSystem3D.SubframeAlgorithmParticlesPerUnit = 1f - Math.Max(0f, (5f - this.{16614}) / 5f) * 0.5f;
				}
			}
			if (this.SenderObjectUID != -1)
			{
				WorldMapInfo mapInfo = Global.Player.MapInfo;
				for (int j = 0; j < {16604}.Size; j++)
				{
					Isle isle = {16604}.Array[j];
					BoundingSphere sphere = new BoundingSphere(this.CurrentPosition, 2f);
					if (isle.Statement.ModelGlobalBS.Intersects(sphere))
					{
						Matrix matrix;
						Matrix.Invert(ref isle.Statement.GlobalTransformWorld, out matrix);
						CollisionTestResult collisionTestResult;
						CollisionCore.SphereWithTriangleShape(ref sphere, isle.Statement.ModelData.MeshShape, false, ref isle.Statement.GlobalTransformWorld, ref matrix, CollisionTestQuality.Full, out collisionTestResult);
						if (collisionTestResult.IsCollide)
						{
							FXEngine.CreateMassiveExplosion(collisionTestResult.CollideCentr, false, false, false, false);
							Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.MortarHitShip, this.CurrentPosition, 1f, false);
							this.CollisionWithObject = true;
							this.CollisionIndex = {16604}.Array[j].DynamicObjectUID.GetValueOrDefault(-1);
							this.{16612}();
							return true;
						}
					}
				}
			}
			if (this.UpdateBase(ref {16605}))
			{
				this.{16612}();
				return true;
			}
			return false;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000E878 File Offset: 0x0000CA78
		public override bool UpdateBase(ref FrameTime {16606})
		{
			this.{16614} += {16606}.secElapsed;
			if ((this.Shot.BallInfo.ID == 6 || this.Shot.BallInfo.ID == 11 || this.Shot.BallInfo.ID == 19) && Rand.Chanse(90f * {16606}.Factor))
			{
				FXEngine.SampleFumesSmoke(this.CurrentPosition, 0.8f, 0.4f, 1f);
			}
			if (this.Shot.BallInfo.ID == 20 && !base.IsWaitingSupermortar)
			{
				FXEngine.SampleFumesSmoke(this.CurrentPosition, 1.5f, 0.4f, 1f);
			}
			float num = Vector3.Distance(this.Shot.StartPosition, this.CurrentPosition);
			float num2 = MathHelper.Lerp(this.Shot.WeaponDistance.X, this.Shot.WeaponDistance.Y, this.Shot.Direction.Z);
			if (num > (num2 - 50f) / 2f && !this.{16619} && num2 > 110f)
			{
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.MortarShotTrail, this.CurrentPosition, 0.4f, false);
				this.{16619} = true;
			}
			this.{16613} = this.CurrentPosition;
			if (base.UpdateBase(ref {16606}))
			{
				if (this.{16618}.Size > 1)
				{
					Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.Slamming_Small, this.CurrentPosition, 1f, false);
					using (IEnumerator<ParticleSystem3D> enumerator = ((IEnumerable<ParticleSystem3D>)this.{16618}).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ParticleSystem3D particleSystem3D = enumerator.Current;
							FXEngine.NewWaterSplash(particleSystem3D.Center.XZ(), 1f, true);
						}
						goto IL_1FC;
					}
				}
				FXEngine.NewMassiveSplash(this.CurrentPosition.XZ(), (this.Shot.BallInfo.ID == 20) ? 2.5f : 1.6f);
				FXEngine.CreateMassiveExplosionSmokeOnly(this.CurrentPosition, false);
				IL_1FC:
				foreach (Ship ship in Global.Game.WorldInstance.EnumerateShips(true, true, true))
				{
					Vector2 vector = this.CurrentPosition.XZ();
					if (ship.DistanceToHitbox(vector) < 1f)
					{
						Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.MortarHitShip, this.CurrentPosition, 1f, false);
						break;
					}
				}
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.MortarHitWater, this.CurrentPosition, 1f, false);
				this.{16612}();
				if (this.Shot.Feature == CannonFeature.PowderKegMortar)
				{
					Global.Game.WorldInstance.MakeBigFireArea(this.CurrentPosition.XZ(), PowderKegInfo.FirePowderKegFireZone(true));
				}
				return true;
			}
			return false;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000EB60 File Offset: 0x0000CD60
		public void Draw3D()
		{
			if (this.Shot.BallInfo.ID == 19)
			{
				ModelTransformedScene modelTransformedScene = LocalContent.Loaded.powderKegPackByModelNumber[0];
				modelTransformedScene.Transform.Translation = this.CurrentPosition;
				modelTransformedScene.Transform.RotatesAll = HashHelper.SphericalVectorFromLerp(this.{16614} * (0.3f + 0.2f * (float)(this.uID % 2)) + (float)this.uID * 0.2f);
				Global.Render.CommonShader.RenderObject(modelTransformedScene, false, 1f, false, 0f, false);
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		public void DrawStatic()
		{
			if (this.Shot.BallInfo.ID == 20)
			{
				Global.Render.ItemsShader.RenderCircle(base.GetFinishPosition().X0Y, 7f, 9f, Color.OrangeRed * 0.1f, GPUCircleType.HardCircle, null);
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000EC54 File Offset: 0x0000CE54
		public void AddGeometry(ref Vector3 {16607}, SpriteBatch3D<VertexPositionColorTexture> {16608})
		{
			if (this.Shot.BallInfo.ID == 19 || base.IsWaitingSupermortar)
			{
				return;
			}
			if (this.Shot.BallInfo.ID == 8)
			{
				for (int i = 0; i < this.{16617}.Length; i++)
				{
					this.DrawLocal(this.{16617}[i], ref {16607}, {16608});
				}
				return;
			}
			this.DrawLocal(this.CurrentPosition, ref {16607}, {16608});
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		private void DrawLocal(Vector3 {16609}, ref Vector3 {16610}, SpriteBatch3D<VertexPositionColorTexture> {16611})
		{
			Vector3 up = Vector3.Up;
			Matrix {14957};
			Matrix.CreateBillboard(ref {16609}, ref {16610}, ref up, null, out {14957});
			float num = (this.Shot.BallInfo.ID == 8) ? 0.5f : 1f;
			this.{16616}.SetPos(num, num);
			this.{16616}.Transform({14957});
			Rectangle {14947};
			AtlasObjs.GetPathForCannonBall((int)this.Shot.BallInfo.ID, out {14947});
			this.{16616}.SetUV({14947}, AtlasObjs.Texture.Size);
			{16611}.Add(this.{16616}.array);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000ED68 File Offset: 0x0000CF68
		private void {16612}()
		{
			for (int i = 0; i < this.{16618}.Size; i++)
			{
				this.{16618}.Array[i].Remove();
			}
			this.{16618}.Clear();
		}

		// Token: 0x0400016A RID: 362
		public bool CollisionWithObject;

		// Token: 0x0400016B RID: 363
		public int CollisionIndex;

		// Token: 0x0400016C RID: 364
		private Vector3 {16613};

		// Token: 0x0400016D RID: 365
		private float {16614};

		// Token: 0x0400016E RID: 366
		private Matrix {16615};

		// Token: 0x0400016F RID: 367
		private BillboardParent_VPCT {16616};

		// Token: 0x04000170 RID: 368
		private Vector3[] {16617};

		// Token: 0x04000171 RID: 369
		private Tlist<ParticleSystem3D> {16618};

		// Token: 0x04000172 RID: 370
		private bool {16619};

		// Token: 0x04000173 RID: 371
		private bool {16620};

		// Token: 0x04000174 RID: 372
		private static Vector3[] kernel = new Vector3[]
		{
			new Vector3(-0.556641f, -0.037109f, -0.654297f),
			new Vector3(0.173828f, 0.111328f, 0.064453f),
			new Vector3(0.001953f, 0.082031f, -0.060547f),
			new Vector3(-0.078125f, 0.013672f, -0.314453f),
			new Vector3(0.117188f, -0.140625f, -0.199219f),
			new Vector3(-0.251953f, -0.558594f, 0.082031f)
		};
	}
}
