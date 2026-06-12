using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using UWContentPipelineExtensionRuntime.Tags;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x02000483 RID: 1155
	public sealed class UnitScene
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x000DE659 File Offset: 0x000DC859
		public BillboardParent_VPCT BloodDecal
		{
			get
			{
				return this.{24115};
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x000DE661 File Offset: 0x000DC861
		private bool isStand
		{
			get
			{
				return this.{24111} == UnitAnimation.Idle_1 || this.{24111} == UnitAnimation.Idle_2;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x000DE677 File Offset: 0x000DC877
		public Vector3 WorldPlace
		{
			get
			{
				return this.{24110}.Transform3X3(this.Model.LocalTransformOrNull.Translation);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x000DE694 File Offset: 0x000DC894
		public Vector3 WorldBodyCenter
		{
			get
			{
				return this.{24110}.Transform3X3(this.Model.LocalTransformOrNull.Translation + new Vector3(0f, 0.5f * (this.Model.Model.MeshParts[0].LocalSpaceBoundingBox.Max.Y - this.Model.Model.MeshParts[0].LocalSpaceBoundingBox.Min.Y) * this.Model.LocalTransformOrNull.MiddleScale, 0f));
			}
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x000DE72C File Offset: 0x000DC92C
		public UnitScene(ModelRenderer {24085}, UnitRole {24086}, Transform3D {24087})
		{
			this.Model = {24085};
			this.Role = {24086};
			this.Transparancy = 1f;
			this.{24110} = {24087};
			this.{24114} = (Dictionary<UnitAnimation, AnimationClip>){24085}.Model.Tag;
			this.{24105}(UnitAnimation.Idle_1, null);
			if (Rand.Chanse(40f))
			{
				this.{24117} = 10000f;
			}
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x000DE7B8 File Offset: 0x000DC9B8
		private bool {24088}(Ship {24089})
		{
			return {24089}.UsedShip.Crew.CountOfSailors < {24089}.UsedShip.NeedSailors && Geometry.Saturate(2f * (float){24089}.UsedShip.Crew.CountOfSailors / (float){24089}.UsedShip.NeedSailors) < HashHelper.greater(this.RandomValue);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000DE81C File Offset: 0x000DCA1C
		public void Update(float {24090}, Ship {24091}, bool {24092}, bool {24093}, bool {24094}, bool {24095}, bool {24096}, Vector2 {24097}, Tlist<UnitScene> {24098})
		{
			UnitScene.<>c__DisplayClass31_0 CS$<>8__locals1;
			CS$<>8__locals1.allUnits = {24098};
			if (this.Role == UnitRole.Gunner && this.{24088}({24091}))
			{
				this.Transparancy = 0f;
				return;
			}
			UnitAnimation unitAnimation = UnitAnimation.Idle_1;
			if (this.IsDeath)
			{
				unitAnimation = UnitAnimation.Death;
			}
			else if (this.Role == UnitRole.Gunner && this.Model.LocalTransformOrNull.Translation.Z > 0f && !{24092})
			{
				unitAnimation = UnitAnimation.Reload_cannon;
			}
			else if (this.Role == UnitRole.Gunner && this.Model.LocalTransformOrNull.Translation.Z < 0f && !{24093})
			{
				unitAnimation = UnitAnimation.Reload_cannon;
			}
			else if ((this.Role == UnitRole.Musketeer || this.Dirty) && ({24094} || ({24095} && this.Role == UnitRole.Gunner)))
			{
				unitAnimation = UnitAnimation.Cover_down;
			}
			else if (this.Role == UnitRole.Sailfish && this.Dirty && {24096})
			{
				unitAnimation = UnitAnimation.Looking;
			}
			else if (this.{24112} != null)
			{
				unitAnimation = this.{24112}.Value;
				if (this.Model.Animation.IsFinished)
				{
					this.{24112} = null;
				}
			}
			this.{24117} -= {24090};
			float num = {24095} ? 1.3f : 0.9f;
			bool flag = !{24091}.IsRunningMarchingMode;
			Vector2 value = this.Model.LocalTransformOrNull.Translation.XZ();
			if (this.walkTarget == null && this.Role != UnitRole.Gunner && unitAnimation == UnitAnimation.Idle_1 && this.Dirty && this.{24117} < 0f && HashHelper.greater(this.RandomValue) > (flag ? 0.1f : 0.4f))
			{
				this.{24117} = 0f;
				Tlist<ValueTuple<Vector3, float>> tlist = {24091}.VisualCrewPositions(1);
				float num2 = float.MaxValue;
				for (int i = 0; i < tlist.Size; i++)
				{
					ValueTuple<Vector3, float> valueTuple = tlist.Array[i];
					Vector2 value2 = valueTuple.Item1.XZ();
					float num3 = Vector2.DistanceSquared(value, value2);
					if (num3 >= 1f)
					{
						Vector2 value3 = (value - value2).Normal();
						if (Vector2.Dot(this.Model.LocalTransformOrNull.ForwardZ, value3) <= (this.{24118} ? 0.4f : -0.5f) && Math.Abs(valueTuple.Item1.Y - this.Model.LocalTransformOrNull.Translation.Y) <= 0.65f && num3 <= 25f)
						{
							this.{24118} = false;
							if (num3 < num2)
							{
								num2 = num3;
								this.walkTarget = new Vector3?(valueTuple.Item1 + new Vector3(Rand.Range(-0.4f, 0.4f), 0f, Rand.Range(-0.4f, 0.4f)));
								this.{24116} = MathF.Sqrt(num3);
								this.{24119} = this.Model.LocalTransformOrNull.Translation.Y;
							}
						}
					}
				}
				if (this.walkTarget == null && this.{24118})
				{
					this.{24117} = 5000f;
				}
				this.{24118} = (this.walkTarget == null);
			}
			if (this.walkTarget != null && !this.IsDeath)
			{
				Vector2 value4 = this.walkTarget.Value.XZ();
				Vector2 {11447} = value - value4;
				float num4 = Vector2.Distance(value, value4);
				float num5 = 1f - num4 / this.{24116};
				Vector2 vector = {11447}.Normal();
				float getvalue = MathF.Atan2(vector.Y, vector.X) + 1.5707964f;
				float num6 = 0.85f + HashHelper.greater(this.RandomValue) * 0.3f;
				if ({11447}.LengthSquared() < 4f)
				{
					num6 *= 0.66f;
				}
				Geometry.AxisNormFast(ref getvalue);
				Geometry.AngularMovement(ref this.Model.LocalTransformOrNull.Yaw, getvalue, {24090} / 1000f * 4f * num);
				unitAnimation = UnitAnimation.Walk;
				Vector2 vector2 = Geometry.SubstructRotate(this.Model.LocalTransformOrNull.Yaw + 1.5707964f, {24090} / 1000f * 2f * num);
				Transform3D localTransformOrNull = this.Model.LocalTransformOrNull;
				localTransformOrNull.Translation.X = localTransformOrNull.Translation.X + vector2.X * num6;
				Transform3D localTransformOrNull2 = this.Model.LocalTransformOrNull;
				localTransformOrNull2.Translation.Z = localTransformOrNull2.Translation.Z + vector2.Y * num6;
				this.Model.LocalTransformOrNull.Translation.Y = this.{24119} + (this.walkTarget.Value.Y - this.{24119}) * num5;
				if ({11447}.LengthSquared() < 0.25f)
				{
					if (Rand.Chanse(10f))
					{
						this.{24117} = 5000f;
					}
					this.walkTarget = null;
				}
			}
			this.{24105}(unitAnimation, {24091});
			if (this.IsDeath)
			{
				this.Transparancy = 1f;
				if (this.{24120} != null)
				{
					float num7 = MathHelper.Lerp(2.5f, 3.5f, HashHelper.greater(this.RandomValue));
					this.{24121} += {24090} / 1000f * num7;
					Vector3 value5 = new Vector3(-this.{24120}.Value.X * num7 * 5f, -this.{24121} * 2f, -this.{24120}.Value.Y * num7 * 5f);
					Vector3 vector3 = {24091}.Transform.Transform3X3(this.Model.LocalTransformOrNull.Translation);
					this.Model.LocalTransformOrNull.Translation += value5 * {24090} / 1000f;
					Vector3 vector4 = {24091}.Transform.Transform3X3(this.Model.LocalTransformOrNull.Translation);
					WeatherEngine currentClientWeather = CommonGlobal.CurrentClientWeather;
					WorldMapInfo mapInfo = Global.Player.MapInfo;
					Vector2 vector5 = vector3.XZ();
					float num8 = currentClientWeather.WavesHeight(mapInfo, vector5);
					if (vector3.Y > num8 && vector4.Y < num8)
					{
						FXEngine.NewWaterSplash(vector4.XZ(), 0.5f, true);
					}
					if (vector4.Y < num8 - 3f)
					{
						this.Transparancy = 0f;
					}
				}
				else if (this.{24113} == null)
				{
					this.Transparancy = 0f;
				}
				else
				{
					this.Transparancy = Math.Max(0f, (2f - (float)this.{24113}.Elapsed.TotalSeconds) / 2f);
				}
				if (this.{24115} != null)
				{
					float num9 = 0.5f + (float)(this.RandomValue % 10) / 50f;
					this.{24115}.SetPos(num9, num9);
					this.{24115}.Transform(Matrix.CreateRotationX(1.5707964f));
					this.{24115}.SetCol(Color.White * this.Transparancy * 0.6f);
					this.{24104}();
				}
				return;
			}
			this.Transparancy = 1f;
			if ({24091}.UsedShip.FirstHP.Summary <= 1f)
			{
				this.Transparancy *= Math.Max(0f, 1f - {24091}.UsedShip.FirstHP.FloodingFactor * 4f);
			}
			if (this.Transparancy > 0f)
			{
				float num10 = HashHelper.NoiseCurve((float)(100 + this.RandomValue) + (float)(Global.Game.GameTotalTimeSec / 2.0));
				float num11 = HashHelper.NoiseCurve((float)(12412 + this.RandomValue) + (float)(Global.Game.GameTotalTimeSec / 2.0));
				Vector2 vector6;
				Geometry.RotateVector2Fast(ref {24097}, this.Model.LocalTransformOrNull.Yaw, out vector6);
				this.Model.LocalTransformOrNull.Pitch = -vector6.X + num10 * 0.05f;
				this.Model.LocalTransformOrNull.Roll = vector6.Y + num11 * 0.05f;
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x000DF050 File Offset: 0x000DD250
		public void GunEffect(Vector2 {24099}, Ship {24100})
		{
			Vector3 value = Vector3.Transform(Vector3.Transform(new Vector3(-0.2f, -0.1181f, 2.3813f), this.Model.LocalTransformOrNull.CreateWorldMatrix()), {24100}.Transform.CreateWorldMatrix());
			Vector3 vector = {24099}.X0Y();
			FXEngine.GunEffectSmall2(value - vector * 0.4f, vector, {24100}.physicsBody.VelocityPerSec / 60f, true);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x000DF0CC File Offset: 0x000DD2CC
		public void CreateGunEffects(float? {24101}, Ship {24102})
		{
			if (!this.IsDeath)
			{
				if ({24101} != null)
				{
					Geometry.AngularMovement(ref this.Model.LocalTransformOrNull.Yaw, Geometry.AxisNormFast({24101}.Value - {24102}.Rotation - 1.5707964f), Global.Game.GameTime.ElapsedDrawReal / 1000f);
					if (this.Role != UnitRole.Musketeer && this.Role == UnitRole.Sailfish)
					{
						this.{24112} = new UnitAnimation?(UnitAnimation.Looking);
						return;
					}
				}
				else if (this.isStand)
				{
					Geometry.AngularMovement(ref this.Model.LocalTransformOrNull.Yaw, HashHelper.NoiseCurve((float)this.RandomValue + (float)(Global.Game.GameTotalTimeSec / 40.0)) * 3.1415927f, Global.Game.GameTime.ElapsedDrawReal / 1000f);
				}
			}
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x000DF1A9 File Offset: 0x000DD3A9
		public void CreateJoyEffect()
		{
			if (!this.IsDeath && this.isStand)
			{
				this.{24112} = new UnitAnimation?(UnitAnimation.Victory);
			}
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x000DF1C8 File Offset: 0x000DD3C8
		public void OnDeath(Ship {24103})
		{
			if (this.IsDeath)
			{
				return;
			}
			this.IsDeath = true;
			this.{24120} = null;
			IClientShip clientShip = (IClientShip){24103};
			if (clientShip.GetClient.LastBallCollisionNormal.Z < 1000f && Rand.Chanse(60f) && !{24103}.IsInBoarding && Session.TimeFromLastReceivedDamageSec < 1f)
			{
				this.{24120} = new Vector2?(Geometry.RotateVector2(clientShip.GetClient.LastBallCollisionNormal.XY(), -{24103}.Rotation + 3.1415927f) * (float)Rand.GetSign());
			}
			if (this.{24113} == null)
			{
				this.{24113} = Stopwatch.StartNew();
			}
			else
			{
				this.{24113}.Restart();
			}
			FXEngine.CreateCrewBlood(this.WorldBodyCenter);
			this.{24115} = BillboardParent_VPCT.CreatePlane(0.5f, 0.5f, 0f);
			this.{24115}.SetUV(AtlasObjs.Particles.p_BloodOnFloor.GetPath(), AtlasObjs.Texture.Size);
			this.{24115}.SetCol(Color.White * 0.6f);
			this.{24104}();
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x000DF2EB File Offset: 0x000DD4EB
		public void OnHide()
		{
			this.{24113} = null;
			this.IsDeath = true;
			this.{24115} = null;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x000DF302 File Offset: 0x000DD502
		public void OnAlive()
		{
			this.{24113} = null;
			this.IsDeath = false;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x000DF314 File Offset: 0x000DD514
		private void {24104}()
		{
			this.{24115}.Transform(new Transform3D(this.WorldPlace + new Vector3(0f, 0.05f, 0f) + 0.2f * HashHelper.SphericalVectorFromLerp((float)this.RandomValue) * new Vector3(1f, 0f, 1f), new Vector3(0f, (float)this.RandomValue / 6.7f, 0f), Vector3.One).CreateWorldMatrix());
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x000DF3AC File Offset: 0x000DD5AC
		private void {24105}(UnitAnimation {24106}, Ship {24107})
		{
			UnitAnimation unitAnimation = (this.{24111} == UnitAnimation.Idle_2) ? UnitAnimation.Idle_1 : this.{24111};
			bool isFinished = this.Model.Animation.IsFinished;
			if ({24106} != unitAnimation || isFinished)
			{
				UnitAnimation unitAnimation2 = {24106};
				if ({24106} == UnitAnimation.Idle_1)
				{
					if (Math.Sin(Global.Game.GameTotalTimeSec * 0.01 + (double)this.RandomValue) > (({24107} != null && ({24107}.IsRunningMarchingMode || {24107}.NowSpeed == 0f)) ? 0.2 : 0.5) && this.{24117} < -3000f)
					{
						unitAnimation2 = UnitAnimation.Kneeling;
					}
					else if (Math.Sin(Global.Game.GameTotalTimeSec * 0.08 - (double)this.RandomValue) > -0.3 && this.{24114}.ContainsKey(UnitAnimation.Looking))
					{
						unitAnimation2 = UnitAnimation.Looking;
					}
					else
					{
						unitAnimation2 = Rand.Pick<UnitAnimation>(new UnitAnimation[]
						{
							UnitAnimation.Idle_1,
							UnitAnimation.Idle_2
						});
						if (this.Role == UnitRole.Musketeer && Rand.Chanse(80f))
						{
							unitAnimation2 = UnitAnimation.Idle_1;
						}
					}
				}
				this.{24111} = unitAnimation2;
				this.Model.Animation.StartClip(this.{24114}[unitAnimation2], (isFinished && unitAnimation2 == UnitAnimation.Cover_down) ? new TimeSpan?(TimeSpan.FromSeconds(0.9)) : null);
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x000DF518 File Offset: 0x000DD718
		[CompilerGenerated]
		internal static bool <Update>g__isCollision|31_0(Vector2 {24108}, ref UnitScene.<>c__DisplayClass31_0 {24109})
		{
			return {24109}.allUnits.Any((UnitScene {24122}) => Vector2.DistanceSquared({24122}.Model.LocalTransformOrNull.Translation.XZ(), {24108}) < 4f);
		}

		// Token: 0x040017A7 RID: 6055
		private static Tlist<Vector3> tempList = new Tlist<Vector3>(100);

		// Token: 0x040017A8 RID: 6056
		private const bool animationBugs = true;

		// Token: 0x040017A9 RID: 6057
		public ModelRenderer Model;

		// Token: 0x040017AA RID: 6058
		public UnitRole Role;

		// Token: 0x040017AB RID: 6059
		public bool IsDeath;

		// Token: 0x040017AC RID: 6060
		public float Transparancy;

		// Token: 0x040017AD RID: 6061
		private Transform3D {24110};

		// Token: 0x040017AE RID: 6062
		public bool Dirty = Rand.Chanse(50f);

		// Token: 0x040017AF RID: 6063
		public int RandomValue = Rand.RangeInt(0, 10000);

		// Token: 0x040017B0 RID: 6064
		private UnitAnimation {24111};

		// Token: 0x040017B1 RID: 6065
		private UnitAnimation? {24112};

		// Token: 0x040017B2 RID: 6066
		private Stopwatch {24113};

		// Token: 0x040017B3 RID: 6067
		private Dictionary<UnitAnimation, AnimationClip> {24114};

		// Token: 0x040017B4 RID: 6068
		private BillboardParent_VPCT {24115};

		// Token: 0x040017B5 RID: 6069
		public Vector3? walkTarget;

		// Token: 0x040017B6 RID: 6070
		private float {24116};

		// Token: 0x040017B7 RID: 6071
		private float {24117};

		// Token: 0x040017B8 RID: 6072
		private bool {24118};

		// Token: 0x040017B9 RID: 6073
		private float {24119};

		// Token: 0x040017BA RID: 6074
		private Vector2? {24120};

		// Token: 0x040017BB RID: 6075
		private float {24121};
	}
}
