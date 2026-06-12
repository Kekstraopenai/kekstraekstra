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
using TheraEngine.Graphics;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x02000057 RID: 87
	internal sealed class ShipFloodingDecoration : IPoolObject
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00015FD5 File Offset: 0x000141D5
		public Vector2 Position2D
		{
			get
			{
				return this.{16892}.Translation.XZ();
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00015FE7 File Offset: 0x000141E7
		public ShipFloodingDecoration()
		{
			this.{16906} = new Tlist<ModelTransformedScene>();
			this.{16899} = new Tlist<MicroburningEffect>();
			this.{16907} = new SailLocalRenderQuery();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00016010 File Offset: 0x00014210
		public void Create(int {16873}, Vector2 {16874}, int {16875})
		{
			this.ShipUID = {16875};
			ShipStaticInfo {16879} = Gameplay.ShipsStaticInfo.FromID({16873});
			this.InitializeShipInfo(false, new Transform3D(new Vector3({16874}.X, 0f, {16874}.Y), new Vector3(0f, Rand.Angle(), 0f), new Vector3(0.3f)), {16879}, (int {16909}) => 255f, (int {16910}) => true);
			this.{16892}.Translation.Y = this.{16882}(0.31578946f);
			this.{16892}.RotatesAll = this.{16884}(0.31578946f);
			this.{16896} = default(Vector2);
			this.{16893} = default(Vector2);
			this.{16903} = 12f;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00016104 File Offset: 0x00014304
		public void Create(Ship {16876})
		{
			this.{16904} = ({16876}.UsedShip.FirstHP.DestroyedByFloodingFlags ? 0f : Rand.Range(500f, 900f));
			this.{16905} = {16876}.UsedShip.StaticInfo.CorpusHalfLength;
			this.ShipUID = {16876}.uID;
			this.InitializeShipInfo(Vector2.Distance({16876}.Position, Global.Player.Position) < 170f, {16876}.Transform, ({16876}.uID == Global.Player.uID) ? Session.Account.Shipyard.CurrentRealShip.CraftFrom.StaticInfo : {16876}.UsedShip.StaticInfo, (int {16911}) => {16876}.UsedShip.HitboxSailsStrength[{16911}], (int {16912}) => {16876}.OpenSailesFromIndices[{16912}]);
			this.{16896} = {16876}.physicsBody.VelocityPerSec / 60f;
			this.{16893} = ((IClientShip){16876}).GetClient.GetSailStartValue;
			this.{16899} = {16876}.UsedShip.FirstHP.aliveMicroburning.Clone();
			this.{16908} = ((IClientShip){16876}).GetClient.GetDesignReplaceId();
			this.{16903} = 0f;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0001628C File Offset: 0x0001448C
		private void InitializeShipInfo(bool {16877}, Transform3D {16878}, ShipStaticInfo {16879}, Func<int, float> {16880}, Func<int, bool> {16881})
		{
			this.{16902} = {16877};
			this.{16895} = {16879};
			this.{16897} = {16878}.Translation.Y;
			this.{16898} = {16878}.RotatesAll;
			this.{16892} = new Transform3D({16878});
			this.{16901} = new ModelTransformedScene
			{
				Transform = this.{16892},
				VisibleTestType = ModelSceneVisibleTest.ForAllScene
			};
			this.{16901}.AddObject({16879}.Model.corpusLow);
			if ({16877})
			{
				for (int i = 0; i < {16879}.MastHitboxes.Length; i++)
				{
					ModelTransformedScene modelTransformedScene = new ModelTransformedScene
					{
						Transform = new Transform3D(this.{16892}),
						VisibleTestType = ModelSceneVisibleTest.Disable
					};
					modelTransformedScene.AddObject(new ModelRenderer({16879}.MastHitboxes[i].Model)
					{
						LocalTransformOrNull = new Transform3D()
					}, true);
					this.{16906}.Add(modelTransformedScene);
				}
				this.{16900} = new ModelTransformedScene[0];
			}
			else
			{
				this.{16900} = new ModelTransformedScene[0];
			}
			this.{16894} = new Vector3((float)Rand.Sign(), (float)Rand.Sign(), (float)Rand.Sign());
			this.{16891} = new Timer(2000f);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000163B4 File Offset: 0x000145B4
		public void ClearResources()
		{
			this.{16891}.Reset();
			this.{16892} = null;
			this.{16906}.Clear();
			this.{16902} = false;
			this.{16901} = null;
			this.{16900} = null;
			this.{16894} = Vector3.Zero;
			this.{16893} = Vector2.Zero;
			this.{16903} = 0f;
			this.{16899}.Clear();
			this.{16904} = 0f;
			this.{16908} = 0;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00016434 File Offset: 0x00014634
		private float {16882}(float {16883})
		{
			float num = MathF.Pow({16883}, 0.666f);
			return this.{16897} - ((num < 0.5f) ? (num * (1f - num) * 2f) : (1f - num * (1f - num) * 2f)) * 4f;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00016488 File Offset: 0x00014688
		private Vector3 {16884}(float {16885})
		{
			return this.{16898} + this.{16894} * 1.5f * {16885};
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000164AC File Offset: 0x000146AC
		public bool Update(ref FrameTime {16886})
		{
			this.{16903} += {16886}.secElapsed;
			if ({16886}.EvaluteTimerMs2(ref this.{16904}))
			{
				Vector2 vector = Rand.NextRadialVector2(this.{16905} * 0.3f, this.{16905} * 0.6f);
				FXEngine.ShipSmallCrewExplosionEffect(this.{16892}.Translation + new Vector3(vector.X, 0f, vector.Y), this.{16905});
			}
			Transform3D transform3D = this.{16892};
			transform3D.Translation.X = transform3D.Translation.X + this.{16896}.X * {16886}.msElapsed / 16f;
			Transform3D transform3D2 = this.{16892};
			transform3D2.Translation.Z = transform3D2.Translation.Z + this.{16896}.Y * {16886}.msElapsed / 16f;
			this.{16896}.X = this.{16896}.X * 0.99f;
			this.{16896}.Y = this.{16896}.Y * 0.99f;
			int num = 0;
			foreach (ModelTransformedScene modelTransformedScene in ((IEnumerable<ModelTransformedScene>)this.{16906}))
			{
				modelTransformedScene.Transform.Translation = this.{16901}.Transform.Translation;
				for (int i = 0; i < modelTransformedScene.CountModels; i++)
				{
					BoundingBox localSpaceBoundingBox = modelTransformedScene.GetModels[i].Model.MeshParts[0].LocalSpaceBoundingBox;
					Transform3D localTransformOrNull = modelTransformedScene.GetModels[i].LocalTransformOrNull;
					Vector3 {14820} = new Vector3(0.5f * (localSpaceBoundingBox.Min.X + localSpaceBoundingBox.Max.X), localSpaceBoundingBox.Min.Y + 1f, 0f);
					float num2 = this.{16903} / 3f * Math.Min(1f, this.{16903} / 3f);
					float num3 = (0.8f + HashHelper.greater(234124 + num) * 0.5f) * ((Math.Abs(localTransformOrNull.Pitch) > 1.4137167f) ? 0.03f : 1f);
					float num4 = localTransformOrNull.Pitch + {16886}.secElapsed * (float)(HashHelper.greaterBoolNoLongRow(num + this.ShipUID) ? -1 : 1) * 0.15f * num2 * num3;
					if (Math.Abs(localTransformOrNull.Pitch) < 1.4137167f && Math.Abs(num4) > 1.4137167f)
					{
						Vector3 vector2 = modelTransformedScene.Transform.Transform3X3(localTransformOrNull.Transform3X3(localSpaceBoundingBox.Min));
						Vector3 value = modelTransformedScene.Transform.Transform3X3(localTransformOrNull.Transform3X3(localSpaceBoundingBox.Max));
						Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.Slamming_Small, vector2, 0.25f, false);
						for (int j = 0; j < 6; j++)
						{
							FXEngine.NewWaterSplash(Vector3.Lerp(vector2, value, (float)j / 6f).XZ(), Rand.Range(0.7f, 1.5f), false);
						}
					}
					if (Math.Abs(num4) > 2.5132742f)
					{
						num4 = (float)Math.Sign(num4) * 3.1415927f * 0.8f;
					}
					localTransformOrNull.CreateCenterPivotRotation({14820}, new Vector3(num4, 0f, 0f));
					float num5 = num2 * 2f;
					localTransformOrNull.Translation.Y = -num2 * 0.2f + Geometry.Saturate(num5 * (1f - num5) * 4f) * 2f;
				}
				num++;
			}
			if (this.{16891}.Sample(ref {16886}))
			{
				float {24821} = Rand.Range(this.{16895}.BSRadius * 0.66f, this.{16895}.BSRadius * 1f);
				StaticSystem staticSystem = Global.Game.StaticSystem;
				Vector2 vector3 = this.{16892}.Translation.XZ();
				staticSystem.AddOceanParticle(vector3, {24821}, false, false);
			}
			float num6 = this.{16903} / 38f;
			float num7 = Geometry.Tanstep(num6);
			this.{16892}.Translation.Y = this.{16882}(num7);
			this.{16892}.RotatesAll = this.{16884}(num7);
			Vector3 vector4 = CommonGlobal.CurrentClientWeather.NormalOnlyHelper(Global.Player.MapInfo, this.{16892}.Translation.X, this.{16892}.Translation.Z);
			this.{16892}.Pitch += vector4.Z * 0.5f;
			this.{16892}.Roll += vector4.X * 0.15f;
			Geometry.AxisNorm(ref this.{16892}.Yaw);
			Geometry.AxisNorm(ref this.{16892}.Pitch);
			Geometry.AxisNorm(ref this.{16892}.Roll);
			FXEngine.SampleShipMicroburning(null, this.{16899}, this.{16892});
			return num6 > 1f || Vector2.Distance(Global.Player.Position, this.Position2D) > 350f;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000169DC File Offset: 0x00014BDC
		public void Render()
		{
			if (this.{16908} != 0)
			{
				Global.Render.CommonShader.SetOrResetTextureReplaceDesign(new int?(this.{16908}));
			}
			Global.Render.CommonShader.RenderObject(this.{16901}, true, 1f, false, 0f, false);
			if (this.{16901}.IsMainCameraVisible)
			{
				for (int i = 0; i < this.{16906}.Size; i++)
				{
					Global.Render.CommonShader.RenderObject(this.{16906}.Array[i], false, 1f, false, 0f, false);
				}
			}
			if (this.{16908} != 0)
			{
				Global.Render.CommonShader.SetOrResetTextureReplaceDesign(null);
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00016A99 File Offset: 0x00014C99
		public void RenderTransparentGeometry()
		{
			bool isMainCameraVisible = this.{16901}.IsMainCameraVisible;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00003100 File Offset: 0x00001300
		internal void GBufferPass(IGBufferBuilder {16887}, CameraPositionInfo {16888})
		{
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00016AA8 File Offset: 0x00014CA8
		[CompilerGenerated]
		private Transform3D {16889}(Vector3 {16890})
		{
			float num = float.MaxValue;
			ModelTransformedScene modelTransformedScene = null;
			foreach (ModelTransformedScene modelTransformedScene2 in ((IEnumerable<ModelTransformedScene>)this.{16906}))
			{
				float num2 = Vector3.DistanceSquared(modelTransformedScene2.GetModels[0].Model.CommonSphere.Center, {16890});
				if (num2 < num)
				{
					num = num2;
					modelTransformedScene = modelTransformedScene2;
				}
			}
			if (modelTransformedScene != null)
			{
				return modelTransformedScene.GetModels[0].LocalTransformOrNull;
			}
			return null;
		}

		// Token: 0x04000207 RID: 519
		private const float c_TimeAnimation_ms = 8000f;

		// Token: 0x04000208 RID: 520
		private const float c_EffectTimer = 2000f;

		// Token: 0x04000209 RID: 521
		private const float minTimeS = 12f;

		// Token: 0x0400020A RID: 522
		private const float maxTimeS = 25f;

		// Token: 0x0400020B RID: 523
		private const float excludeTimeS = 38f;

		// Token: 0x0400020C RID: 524
		private Timer {16891};

		// Token: 0x0400020D RID: 525
		private Transform3D {16892};

		// Token: 0x0400020E RID: 526
		private Vector2 {16893};

		// Token: 0x0400020F RID: 527
		private Vector3 {16894};

		// Token: 0x04000210 RID: 528
		private ShipStaticInfo {16895};

		// Token: 0x04000211 RID: 529
		private Vector2 {16896};

		// Token: 0x04000212 RID: 530
		private float {16897};

		// Token: 0x04000213 RID: 531
		private Vector3 {16898};

		// Token: 0x04000214 RID: 532
		private Tlist<MicroburningEffect> {16899};

		// Token: 0x04000215 RID: 533
		private ModelTransformedScene[] {16900};

		// Token: 0x04000216 RID: 534
		private ModelTransformedScene {16901};

		// Token: 0x04000217 RID: 535
		private bool {16902};

		// Token: 0x04000218 RID: 536
		private float {16903};

		// Token: 0x04000219 RID: 537
		private float {16904};

		// Token: 0x0400021A RID: 538
		private float {16905};

		// Token: 0x0400021B RID: 539
		private Tlist<ModelTransformedScene> {16906};

		// Token: 0x0400021C RID: 540
		private SailLocalRenderQuery {16907};

		// Token: 0x0400021D RID: 541
		private int {16908};

		// Token: 0x0400021E RID: 542
		public int ShipUID;
	}
}
