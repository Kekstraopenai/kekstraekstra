using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Account;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Graphics;
using TheraEngine.Graphics.Models;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Scene;
using TheraEngine.Scene.Lighting;
using UWContentPipelineExtensionRuntime.Tags;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x0200003C RID: 60
	internal sealed class ClientDrop : Drop
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000D1A4 File Offset: 0x0000B3A4
		public bool IsBeingLooted
		{
			get
			{
				{19243} {19243} = this.{16599};
				return {19243} != null && {19243}.IsLooting;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000D1B7 File Offset: 0x0000B3B7
		public new bool Available
		{
			get
			{
				return base.Available && this.forgetAnimation == 0f;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		protected override Tlist<Drop> ConnectedWhales
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000D1D7 File Offset: 0x0000B3D7
		public ClientDrop()
		{
			this.{16592} = new ModelTransformedScene();
			this.{16593} = new Tlist<ModelRenderer>(5);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000D218 File Offset: 0x0000B418
		public override void ClearResources()
		{
			this.{16592}.Clear();
			this.{16593}.Clear();
			this.forgetAnimation = 0f;
			this.{16596} = 0;
			this.{16599} = null;
			this.IsLooting = false;
			if (this.{16600} != null)
			{
				Global.Render.Pointlights.Remove(this.{16600});
			}
			this.{16600} = null;
			base.ClearResources();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000D288 File Offset: 0x0000B488
		private static DropModelData PickStyleBasedSmallBoat(Tlist<DropModelData> {16586})
		{
			if (!Global.Player.MapInfo.IsWorldmap)
			{
				return {16586}.Rand();
			}
			string path = Global.Player.NearPort.ModelData.Path;
			int targetStyle = path.Contains("_arab") ? 1 : (path.Contains("_asian") ? 2 : (path.Contains("_north") ? 3 : 4));
			return {16586}.Rand((DropModelData {16602}) => ({16602}.SourceName.Contains("Arab") ? 1 : ({16602}.SourceName.Contains("Asian") ? 2 : ({16602}.SourceName.Contains("North") ? 3 : 4))) == targetStyle, null);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000D314 File Offset: 0x0000B514
		protected override void FinalInitialize()
		{
			Sequence sequence = new Sequence(this.uID);
			this.{16592}.Transform.Yaw = sequence.Angle();
			this.{16592}.Transform.Translation = base.Position.X0Y();
			if (base.ModelType != DropModel.Fishing && base.ModelType != DropModel.WorldFortLoot)
			{
				float num = 0.5f;
				if (base.ModelType == DropModel.FishingShip)
				{
					num = 0.3f;
				}
				if (base.ModelType == DropModel.Whale)
				{
					WhaleController.SubModel subtype = this.Whale.Subtype;
					UWModel uwmodel;
					if (subtype != WhaleController.SubModel.Blue && subtype != WhaleController.SubModel.Megalodon)
					{
						if (this.Whale.Subtype != WhaleController.SubModel.Hump)
						{
							if (this.Whale.Subtype != WhaleController.SubModel.Cachalot)
							{
								throw new NotSupportedException();
							}
							uwmodel = LocalContent.Loaded.WhaleOrca;
						}
						else
						{
							uwmodel = LocalContent.Loaded.Whale;
						}
					}
					else
					{
						uwmodel = LocalContent.Loaded.SpermWhale;
					}
					UWModel uwmodel2 = uwmodel;
					this.{16592}.AddObject(new ModelRenderer(uwmodel2, uwmodel2.SkinningDataOrNull.AnimationClips.First<KeyValuePair<string, AnimationClip>>().Key, Math.Max(1f, 0.2f + 0.8f / this.Whale.Scale)), true);
				}
				else
				{
					Tlist<DropModelData> tlist = (base.ModelType == DropModel.IsleFarming && Global.Game.StaticSystem.WinterLevelWorld(base.Position) > 0.9f) ? LocalContent.Loaded.IcedShipsDropModels : LocalContent.Loaded.DropModels[base.ModelType];
					if (tlist.Size > 0)
					{
						DropModelData dropModelData = (base.ModelType == DropModel.FishingShip) ? ClientDrop.PickStyleBasedSmallBoat(tlist) : tlist.Rand();
						if (dropModelData.FixedPart != null)
						{
							this.{16592}.AddObject(new ModelRenderer(dropModelData.FixedPart), true);
						}
						foreach (ModelRenderer modelRenderer in from {16601} in dropModelData.FloatingParts
						select new ModelRenderer({16601}))
						{
							modelRenderer.LocalTransformOrNull = new Transform3D();
							this.{16592}.AddObject(modelRenderer, true);
							this.{16593}.Add(modelRenderer);
						}
						if (dropModelData.WorldDecorPart != null)
						{
							Global.Game.WorldInstance.AddDropStaticModel(dropModelData.WorldDecorPart, base.Position, this.{16592}.Transform.Yaw, num, false, false, null);
						}
					}
				}
				this.{16594} = (this.{16592}.Transform.MiddleScale = num);
			}
			this.{16595} = this.LoadFactor;
			if (base.ModelType == DropModel.Pumkin)
			{
				this.{16600} = new PointLight(base.Position.X0Y, Color.Green.ToVector3(), 1f, 100f);
				this.{16600}.DrawFlares = PointLightFlaresMode.Disable;
				Global.Render.Pointlights.Add(this.{16600});
			}
			DropModel modelType = base.ModelType;
			bool flag = modelType == DropModel.IsleFarming || modelType == DropModel.IsleTreasures;
			if (flag)
			{
				this.{16600} = new PointLight(base.Position.X0Y ^ 3f, Color.OrangeRed.ToVector3() * 0.3f, 1f, 14f);
				this.{16600}.DrawFlares = PointLightFlaresMode.Disable;
				Global.Render.Pointlights.Add(this.{16600});
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
		public bool IsInsideInteropZone()
		{
			if (this.Whale != null && this.Whale.State != WhaleController.Status.Stunned)
			{
				return false;
			}
			Vector2 value = (base.Method != NDropInteropMethod.PickUp) ? (Global.Player.Normal * Global.Player.NowSpeed * 0.8f) : Vector2.Zero;
			return (Global.Player.Position + value).DTest2(base.Position, (base.InteropRadius + Global.Player.UsedShip.StaticInfo.BSRadius * 0.8f + 0.5f) * (1f + Global.Player.UsedShip.LootInteropDistanceBonus));
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000D760 File Offset: 0x0000B960
		public void UpdateClient(ref FrameTime {16587}, out bool {16588})
		{
			this.{16592}.Transform.Translation.X = base.Position.X;
			this.{16592}.Transform.Translation.Z = base.Position.Y;
			if (base.ModelType == DropModel.FishingShip)
			{
				this.{16592}.Transform.Yaw = base.Ttl / 10000f;
			}
			if (this.{16600} != null)
			{
				this.{16600}.Position = (this.{16592}.Transform.Translation ^ 2f);
			}
			if (this.{16597}.Sample(ref {16587}))
			{
				if (base.ModelType != DropModel.Fishing && base.ModelType != DropModel.WorldFortLoot && base.ModelType != DropModel.IsleTreasures)
				{
					int num = this.{16596};
					this.{16596} = num + 1;
					if (num % 5 == 0)
					{
						StaticSystem staticSystem = Global.Game.StaticSystem;
						Vector2 vector = base.Position + Rand.NextVector2(-1f, 1f);
						staticSystem.AddOceanParticle(vector, 4f, false, false);
					}
				}
				DropModel modelType = base.ModelType;
				bool flag = modelType == DropModel.ShipDebris || modelType == DropModel.RuinsFarming;
				if (flag)
				{
					FXEngine.Template_VolumetricFog_Sample(this.{16592}.Transform.Translation + Rand.NextVector3(-1f, 1f).Normal() * 8f * new Vector3(1f, 0.1f, 1f), 0.2f, 4000f, 10f);
				}
			}
			if (this.ServerEffects.HasFlag(DropServerEffect.CanHaveFireBurning) && this.{16598}.Sample(ref {16587}))
			{
				Global.Game.WorldInstance.ContactFireArea(base.Position);
			}
			this.Update(ref {16587}, false, out {16588});
			Geometry.Evalute(ref this.{16595}, this.LoadFactor, {16587}.secElapsed);
			this.IsLooting = (Global.Player != null && !Global.Player.IsDestroyed && this.forgetAnimation == 0f && this.IsInsideInteropZone() && !Global.Player.CrewIsBusy && ClientDrop.nearDrop == this);
			if (this.IsLooting)
			{
				if (this.{16599} == null)
				{
					this.{16599} = new {19243}(this);
				}
				{19243} {19243} = this.{16599};
				if ({19243} != null)
				{
					{19243}.UpdateInput(ref {16587}, true);
				}
			}
			else
			{
				if (this.{16599} != null && this.{16599}.CanRemove)
				{
					this.{16599}.WhenDisembark();
					this.{16599} = null;
				}
				{19243} {19243}2 = this.{16599};
				if ({19243}2 != null)
				{
					{19243}2.UpdateInput(ref {16587}, false);
				}
			}
			if (this.IsLooting && InputHelper.IsClick(Global.Game.InteractiveWorldSystem.CurrentPickDropKey))
			{
				Global.Camera.CameraEffects.RunFocusEffect(new CameraFocusEffect(delegate()
				{
					if (InputHelper.NowInputState.IsDown(Global.Game.InteractiveWorldSystem.CurrentPickDropKey) && this.IsLooting && ClientDrop.nearDrop == this)
					{
						return new Vector3?(base.Position.X0Y());
					}
					return null;
				}, 0.2f, 0f));
			}
			if (this.forgetAnimation != 0f)
			{
				this.forgetAnimation += {16587}.secElapsed * 0.7f;
				if (this.forgetAnimation > 1f)
				{
					{16588} = true;
				}
			}
			if (this.Whale != null)
			{
				this.Whale.Update(this, {16587});
				if (this.Whale.VelocityAngle != 0f)
				{
					Geometry.AngularMovement(ref this.{16592}.Transform.Yaw, this.Whale.VelocityAngle - 1.5707964f, {16587}.secElapsed * 0.6f);
				}
				if (this.{16592}.GetModels.First().Animation.IsFinished)
				{
					this.{16592}.GetModels.First().Animation.TimeScale = ((this.Whale.State == WhaleController.Status.InBattle) ? 1.6f : ((this.Whale.State == WhaleController.Status.Stunned) ? 0.5f : 0.7f));
					this.{16592}.GetModels.First().Animation.StartClip(this.{16592}.GetModels.First().Model.SkinningDataOrNull.AnimationClips.First<KeyValuePair<string, AnimationClip>>().Value, null);
				}
				if (Rand.Chanse(500f * {16587}.secElapsed))
				{
					FXEngine.WaterParticleVolumteric(base.Position - Geometry.SubstructRotate(this.Whale.VelocityAngle, 3f * this.{16592}.Transform.MiddleScale * 2f), 2f * this.{16592}.Transform.MiddleScale * 2f, Vector3.Zero, Color.White * 0.5f, 2f, null);
					StaticSystem staticSystem2 = Global.Game.StaticSystem;
					Vector2 vector = base.Position;
					staticSystem2.AddOceanParticle(vector, this.{16592}.Transform.MiddleScale * 3.5f, false, false);
				}
				if (Vector2.DistanceSquared(base.Position, Global.Player.Position) < 144f)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_Whales, true);
				}
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000DC76 File Offset: 0x0000BE76
		public void BeginRemoveAnimation()
		{
			this.forgetAnimation = 0.001f;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000DC84 File Offset: 0x0000BE84
		public void Render()
		{
			DropModel modelType = base.ModelType;
			bool flag = modelType == DropModel.Fishing || modelType == DropModel.WorldFortLoot;
			if (flag)
			{
				return;
			}
			if (this.forgetAnimation == 0f)
			{
				if (this.{16592}.IsMainCameraVisible)
				{
					if (base.ModelType == DropModel.Whale)
					{
						this.{16592}.Transform.MiddleScale = 0.3f * this.Whale.Scale;
					}
					if (base.ModelType == DropModel.Whale)
					{
						this.{16592}.Transform.Translation.Y = CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, base.Position.X, base.Position.Y) + 0.25f * this.{16592}.Transform.MiddleScale / 0.5f + 0.2f * MathF.Sin(base.Ttl / 1000f * 2f) + (this.Whale.Subtype == WhaleController.SubModel.Hump) - 0.3f;
					}
					else
					{
						this.{16592}.Transform.Translation.Y = 0f;
					}
					Matrix matrix;
					this.{16592}.Transform.CreateWorldMatrix(out matrix);
					for (int i = 0; i < this.{16593}.Size; i++)
					{
						ModelRenderer modelRenderer = this.{16593}.Array[i];
						if (this.Available)
						{
							Vector3 center = modelRenderer.Model.CommonSphere.Center;
							Vector3 vector;
							Vector3.Transform(ref center, ref matrix, out vector);
							modelRenderer.LocalTransformOrNull.Translation.Y = CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, vector.X, vector.Z) / this.{16592}.Transform.MiddleScale;
						}
						else
						{
							Transform3D localTransformOrNull = modelRenderer.LocalTransformOrNull;
							localTransformOrNull.Translation.Y = localTransformOrNull.Translation.Y - Global.Game.GameTime.ElapsedDrawReal * 10f * 1000f / 4000f;
						}
					}
				}
				Global.Render.CommonShader.RenderObject(this.{16592}, base.ModelType != DropModel.Whale, 1f, true, 0f, false);
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000DEBC File Offset: 0x0000C0BC
		public void RenderStatic()
		{
			DropModel modelType = base.ModelType;
			bool flag = modelType == DropModel.RuinsFarming || modelType == DropModel.FishingShip || modelType - DropModel.Whale <= 1;
			if (flag)
			{
				return;
			}
			if (base.ModelType == DropModel.EmpireUnderwaterBox && Vector2.Distance(base.Position, Global.Player.Position) > 60f)
			{
				return;
			}
			if (this.forgetAnimation != 0f)
			{
				Global.Render.CommonShader.RenderObject(this.{16592}, base.ModelType != DropModel.Whale, 1f - this.forgetAnimation, true, 0f, false);
			}
			Vector3 value = base.Position.X0Y() + new Vector3(0f, 4f, 0f);
			if (Engine.GS.Camera.IsVisible(value, 15f))
			{
				if (base.ModelType == DropModel.Fishing)
				{
					Global.Render.ItemsShader.RenderWaterDecal(OtherTextures.DecalFish.Tex, base.Position, new Vector4(0.4f * (1f - this.forgetAnimation) * (0.5f + FXEngine.isNotDark * 0.5f)), 20f + 10f * Geometry.Saturate(this.{16595}), (float)(-(float)Global.Game.GameTotalTimeSec / 6.0 % 6.283185307179586));
					return;
				}
				Global.Render.ItemsShader.RenderCircle(value - new Vector3(0f, 100f, 0f), 3f, 9f, Color.Cyan * (0.04f + Global.Game.StaticSystem.GetSkyShader.DayOrNight * 0.025f) * (1f - this.forgetAnimation), GPUCircleType.SoftCircle, null);
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00003100 File Offset: 0x00001300
		internal void GBufferPass(IGBufferBuilder {16589}, CameraPositionInfo {16590})
		{
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000E090 File Offset: 0x0000C290
		public void Render2D()
		{
			if (Global.Player.IsDestroyed)
			{
				return;
			}
			if (this.forgetAnimation == 0f)
			{
				this.{16591}();
			}
			if (this.Whale != null && this.Whale.State != WhaleController.Status.Stunned)
			{
				HealthBarHelper.RenderWhale(this, (this.Whale.MaxHealth > 9000f) ? 0.95f : 0.8f);
			}
			{19243} {19243} = this.{16599};
			if ({19243} == null)
			{
				return;
			}
			{19243}.Draw();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000E108 File Offset: 0x0000C308
		private void {16591}()
		{
			if (base.ModelType == DropModel.Fishing && Global.Player.ResourcesOfHold.GetCount(36) == 0 && !Global.Camera.IsSpyglass)
			{
				return;
			}
			Rectangle rectangle = (base.ModelType == DropModel.Fishing && Global.Camera.IsSpyglass) ? ((this.LoadFactor > 0.8f) ? AtlasObjs.marker_fishing4 : ((this.LoadFactor > 0.55f) ? AtlasObjs.marker_fishing3 : ((this.LoadFactor > 0.3f) ? AtlasObjs.marker_fishing2 : AtlasObjs.marker_fishing1))) : ((base.ModelType == DropModel.Whale && this.Whale.State == WhaleController.Status.Stunned) ? AtlasObjs.marker_whale : (((base.ModelType == DropModel.RealShipLoot && Session.EngagingInPortBattle == PbsBatlleSide.None) || base.ModelType == DropModel.WorldFortLoot) ? ClientDrop.c_watchMarker : default(Rectangle)));
			if (rectangle.Width == 0)
			{
				return;
			}
			Vector3 vector = base.Position.X0Y() + new Vector3(0f, 4f, 0f);
			if (Engine.GS.Camera.IsVisible(vector, 3f))
			{
				Vector2 projection = Engine.GS.Camera.GetProjection(ref vector);
				float num = Vector3.Distance(vector, Engine.GS.Camera.Position);
				num = (1f - Geometry.Saturate(num / 260f)) * 0.666f + 0.333f;
				Device gs = Engine.GS;
				Vector2 vector2 = projection - rectangle.HalfWidthHeight() * num;
				Vector2 zero = Vector2.Zero;
				float {14558} = 0f;
				float {14559} = num;
				Color color = Color.White * (1.3f - Math.Max((base.ModelType == DropModel.Whale) ? 0.6f : 0.3f, num));
				gs.Draw(rectangle, vector2, zero, {14558}, {14559}, color);
			}
		}

		// Token: 0x04000159 RID: 345
		private static readonly Rectangle c_watchMarker = new Rectangle(1405, 1, 53, 58);

		// Token: 0x0400015A RID: 346
		public static ClientDrop nearDrop;

		// Token: 0x0400015B RID: 347
		private ModelTransformedScene {16592};

		// Token: 0x0400015C RID: 348
		private Tlist<ModelRenderer> {16593};

		// Token: 0x0400015D RID: 349
		public float forgetAnimation;

		// Token: 0x0400015E RID: 350
		private float {16594};

		// Token: 0x0400015F RID: 351
		private float {16595};

		// Token: 0x04000160 RID: 352
		private int {16596};

		// Token: 0x04000161 RID: 353
		private Timer {16597} = new Timer(300f);

		// Token: 0x04000162 RID: 354
		private Timer {16598} = new Timer(7000f);

		// Token: 0x04000163 RID: 355
		private {19243} {16599};

		// Token: 0x04000164 RID: 356
		private PointLight {16600};

		// Token: 0x04000165 RID: 357
		public float LoadFactor;

		// Token: 0x04000166 RID: 358
		public bool IsLooting;
	}
}
