using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using UWContentPipelineExtensionRuntime.Tags;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x0200004E RID: 78
	public class IsleUnitsCloudRenderer
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600021C RID: 540 RVA: 0x000030FD File Offset: 0x000012FD
		private static IsleUnitsCloudRenderer.DebugMode Debug
		{
			get
			{
				return IsleUnitsCloudRenderer.DebugMode.None;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000125B2 File Offset: 0x000107B2
		// (set) Token: 0x0600021E RID: 542 RVA: 0x000125BA File Offset: 0x000107BA
		public bool EnablePerUnitVisibleTest { get; set; } = true;

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000125C3 File Offset: 0x000107C3
		// (set) Token: 0x06000220 RID: 544 RVA: 0x000125CB File Offset: 0x000107CB
		public float TracingMaxDiatnce { get; set; } = 7f;

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000125D4 File Offset: 0x000107D4
		// (set) Token: 0x06000222 RID: 546 RVA: 0x000125DC File Offset: 0x000107DC
		public float TracingMaxHeightDifference { get; set; } = 1f;

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000125E5 File Offset: 0x000107E5
		// (set) Token: 0x06000224 RID: 548 RVA: 0x000125ED File Offset: 0x000107ED
		public bool TracingRepletionCheck { get; set; } = true;

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000125F6 File Offset: 0x000107F6
		// (set) Token: 0x06000226 RID: 550 RVA: 0x000125FE File Offset: 0x000107FE
		public bool SelfCollisionsCheck { get; set; } = true;

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00012607 File Offset: 0x00010807
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0001260F File Offset: 0x0001080F
		public float StopChanseAfterAchievingPoint { get; set; } = 15f;

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00012618 File Offset: 0x00010818
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00012620 File Offset: 0x00010820
		[TupleElementNames(new string[]
		{
			"minMs",
			"maxMs"
		})]
		public ValueTuple<float, float> WalkingDurationLimit { [return: TupleElementNames(new string[]
		{
			"minMs",
			"maxMs"
		})] get; [param: TupleElementNames(new string[]
		{
			"minMs",
			"maxMs"
		})] set; } = new ValueTuple<float, float>(4000f, 9000f);

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00012629 File Offset: 0x00010829
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00012631 File Offset: 0x00010831
		public Func<UWModel, int, Texture2D> TextureCustomizer { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0001263A File Offset: 0x0001083A
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00012642 File Offset: 0x00010842
		public float UnitScale { get; set; } = 0.014f;

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0001264B File Offset: 0x0001084B
		private float ApproxUnitSize
		{
			get
			{
				return 5f * this.UnitScale;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00012659 File Offset: 0x00010859
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00012666 File Offset: 0x00010866
		public Transform3D Transform
		{
			get
			{
				return this.{16778}.Transform;
			}
			set
			{
				this.{16778}.Transform = value;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00012674 File Offset: 0x00010874
		public IsleUnitsCloudRenderer(int {16742}, Transform3D {16743}, Tlist<Vector3> {16744}, Func<UWModel> {16745})
		{
			if ({16742} > {16744}.Size)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(52, 2);
				defaultInterpolatedStringHandler.AppendLiteral("Requested countOfUnits ");
				defaultInterpolatedStringHandler.AppendFormatted<int>({16742});
				defaultInterpolatedStringHandler.AppendLiteral(" is more than pointCloudSize ");
				defaultInterpolatedStringHandler.AppendFormatted<int>({16744}.Size);
				throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			this.PointCloud = {16744};
			this.{16778} = new ModelTransformedScene
			{
				VisibleTestType = ModelSceneVisibleTest.Disable,
				Transform = {16743}
			};
			Tlist<Vector3> usedPoints = new Tlist<Vector3>();
			Func<Vector3, bool> <>9__0;
			for (int i = 0; i < {16742}; i++)
			{
				Func<Vector3, bool> filter;
				if ((filter = <>9__0) == null)
				{
					filter = (<>9__0 = ((Vector3 {16795}) => !usedPoints.Contains({16795})));
				}
				Vector3 {16790} = {16744}.Rand(filter, null);
				usedPoints.Add({16790});
				IsleUnitsCloudRenderer.Unit unit = new IsleUnitsCloudRenderer.Unit({16745}(), {16790}, this.UnitScale);
				this.{16777}.Add(unit);
				this.{16779}.Add(unit, new List<IsleUnitsCloudRenderer.Unit>());
				this.{16778}.AddObject(unit.Renderer, true);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00012800 File Offset: 0x00010A00
		private Vector3 {16746}(Transform3D {16747}, bool {16748} = true, int {16749} = 0)
		{
			Vector2 value = {16747}.Translation.XZ();
			float num = float.MaxValue;
			Vector3 value2 = default(Vector3);
			float tracingMaxHeightDifference = this.TracingMaxHeightDifference;
			float num2 = this.TracingMaxDiatnce * this.TracingMaxDiatnce;
			float num3 = this.TracingMaxDiatnce * 0.15f;
			float num4 = ({16749} == 0) ? -0.5f : (({16749} == 1) ? -0.3f : (({16749} == 2) ? 0f : (({16749} == 3) ? 0.7f : 1f)));
			for (int i = 0; i < this.PointCloud.Size; i++)
			{
				ref Vector3 ptr = ref this.PointCloud.Array[i];
				Vector2 value3 = ptr.XZ();
				float num5;
				Vector2.DistanceSquared(ref value, ref value3, out num5);
				if (num5 >= num3 * num3 && num5 <= num2)
				{
					Vector2 vector = (value - value3).Normal();
					if (Vector2.Dot({16747}.ForwardZ, {16748} ? vector : (-vector)) <= num4 && Math.Abs(ptr.Y - {16747}.Translation.Y) <= tracingMaxHeightDifference)
					{
						if (this.TracingRepletionCheck)
						{
							int num6 = 0;
							for (int j = 0; j < this.{16777}.Size; j++)
							{
								if (Vector2.DistanceSquared(this.{16777}[j].Renderer.LocalTransformOrNull.Translation.XZ(), value3) < this.ApproxUnitSize * this.ApproxUnitSize)
								{
									num6++;
								}
								if (this.{16777}[j].Target != null && Vector2.DistanceSquared(this.{16777}[j].Target.Position.XZ(), value3) < this.ApproxUnitSize * this.ApproxUnitSize)
								{
									num6++;
								}
							}
							if (num6 > 0)
							{
								num5 = 10000f;
							}
						}
						if (num5 < num)
						{
							num = num5;
							value2 = ptr;
						}
					}
				}
			}
			if (!(value2 == Vector3.Zero))
			{
				return value2 + ((IsleUnitsCloudRenderer.Debug == IsleUnitsCloudRenderer.DebugMode.StaticAndDeep) ? Vector3.Zero : new Vector3(Rand.Range(-0.3f, 0.3f), 0f, Rand.Range(-0.3f, 0.3f)));
			}
			if ({16749} >= 4)
			{
				return Vector3.Zero;
			}
			return this.{16746}({16747}, {16748}, {16749} + 1);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00012A60 File Offset: 0x00010C60
		public void Update(ref FrameTime {16750})
		{
			for (int i = 0; i < this.{16777}.Size; i++)
			{
				this.{16752}(this.{16777}[i], ref {16750});
			}
			if (this.SelfCollisionsCheck)
			{
				for (int j = 0; j < this.{16777}.Size - 1; j++)
				{
					IsleUnitsCloudRenderer.Unit unit = this.{16777}[j];
					List<IsleUnitsCloudRenderer.Unit> list = this.{16779}[unit];
					for (int k = j + 1; k < this.{16777}.Size; k++)
					{
						IsleUnitsCloudRenderer.Unit unit2 = this.{16777}[k];
						List<IsleUnitsCloudRenderer.Unit> list2 = this.{16779}[unit2];
						if (IsleUnitsCloudRenderer.<Update>g__AreColliding|54_0(unit, unit2))
						{
							if (!list.Contains(unit2) || !list2.Contains(unit))
							{
								list.Add(unit2);
								list2.Add(unit);
								Vector3? vector2;
								Vector3? vector = vector2 = this.TryGetCollisionsExitPoint(unit);
								if (vector2 != null)
								{
									unit.Target = new IsleUnitsCloudRenderer.WalkTarget(unit.Renderer.LocalTransformOrNull, vector.Value);
								}
								else
								{
									unit.Target = null;
								}
								vector = (vector2 = this.TryGetCollisionsExitPoint(unit2));
								if (vector2 != null)
								{
									unit2.Target = new IsleUnitsCloudRenderer.WalkTarget(unit2.Renderer.LocalTransformOrNull, vector.Value);
								}
								else
								{
									unit2.Target = null;
								}
							}
						}
						else
						{
							list.Remove(unit2);
							list2.Remove(unit);
						}
					}
				}
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00012BDC File Offset: 0x00010DDC
		public void Render3D()
		{
			if (this.EnablePerUnitVisibleTest || this.TextureCustomizer != null)
			{
				for (int i = 0; i < this.{16778}.GetModels.Size; i++)
				{
					this.{16778}.GetModels[i].LocalVisible = false;
				}
				for (int j = 0; j < this.{16778}.GetModels.Size; j++)
				{
					ModelRenderer modelRenderer = this.{16778}.GetModels[j];
					Vector3 vector = this.{16778}.Transform.Transform3X3(modelRenderer.LocalTransformOrNull.Translation);
					if (Engine.GS.Camera.IsVisible(vector, 2f))
					{
						modelRenderer.LocalVisible = true;
						if (this.TextureCustomizer != null)
						{
							Global.Render.CommonShader.SetSubstituteTexture(this.TextureCustomizer(modelRenderer.Model, j));
						}
						Global.Render.CommonShader.RenderObject(this.{16778}, true, 1f, false, 0f, false);
						if (this.TextureCustomizer != null)
						{
							Global.Render.CommonShader.SetSubstituteTexture(null);
						}
						modelRenderer.LocalVisible = false;
					}
				}
			}
			else
			{
				Global.Render.CommonShader.RenderObject(this.{16778}, true, 1f, false, 0f, false);
			}
			if (IsleUnitsCloudRenderer.Debug != IsleUnitsCloudRenderer.DebugMode.None)
			{
				Global.Render.ItemsShader.SetForRender(Matrix.Identity, Vector4.One);
				Global.Render.ItemsShader.BeginPass(false, false);
				Transform3D transform3D = new Transform3D();
				if (IsleUnitsCloudRenderer.Debug == IsleUnitsCloudRenderer.DebugMode.Dynamic)
				{
					IsleUnitsCloudRenderer.swapOrder++;
				}
				int num = (IsleUnitsCloudRenderer.Debug == IsleUnitsCloudRenderer.DebugMode.StaticAndDeep) ? 50 : 50;
				for (int k = IsleUnitsCloudRenderer.swapOrder % num; k < this.PointCloud.Size; k += num)
				{
					Vector3 translation = this.PointCloud[k];
					int num2 = (IsleUnitsCloudRenderer.Debug == IsleUnitsCloudRenderer.DebugMode.StaticAndDeep) ? 200 : 30;
					transform3D.Translation = translation;
					transform3D.Yaw = 0f;
					for (int l = 0; l < num2; l++)
					{
						Vector3 vector2 = this.{16746}(transform3D, true, 0);
						if (vector2 == Vector3.Zero)
						{
							break;
						}
						transform3D.Yaw = MathF.Atan2(vector2.Z - transform3D.Translation.Z, vector2.X - transform3D.Translation.X) - 1.5707964f;
						Vector3 position = this.{16778}.Transform.Transform3X3(transform3D.Translation) + new Vector3(0f, 1f + 0.01f * (float)l, 0f);
						Vector3 position2 = this.{16778}.Transform.Transform3X3(vector2) + new Vector3(0f, 1f + 0.01f * (float)l, 0f);
						Engine.GS.Render3DLine<VertexPositionColor>(new VertexPositionColor(position, Color.Blue), new VertexPositionColor(position2, Color.Wheat));
						transform3D.Translation = vector2;
					}
				}
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00012EF4 File Offset: 0x000110F4
		private Vector3? TryGetCollisionsExitPoint(IsleUnitsCloudRenderer.Unit {16751})
		{
			List<IsleUnitsCloudRenderer.Unit> list = this.{16779}[{16751}];
			if (!list.Any<IsleUnitsCloudRenderer.Unit>() || list.Count > 3)
			{
				return null;
			}
			Vector3? result = null;
			int count = list.Count;
			Vector2 value = {16751}.Renderer.LocalTransformOrNull.Translation.XZ();
			Vector2 value2 = (list[0].Renderer.LocalTransformOrNull.Translation.XZ() - value).Normal();
			Vector2 value3 = Vector2.Zero;
			Vector2 value4 = Vector2.Zero;
			Vector3 vector = this.{16746}({16751}.Renderer.LocalTransformOrNull, false, 0);
			float num = Vector2.Dot((vector.XZ() - value).Normal(), value2);
			float num2 = -1f;
			float num3 = -1f;
			if (count > 1)
			{
				value3 = (list[1].Renderer.LocalTransformOrNull.Translation.XZ() - value).Normal();
				num2 = Vector2.Dot((vector.XZ() - value).Normal(), value3);
			}
			if (count > 2)
			{
				value4 = (list[2].Renderer.LocalTransformOrNull.Translation.XZ() - value).Normal();
				num3 = Vector2.Dot((vector.XZ() - value).Normal(), value4);
			}
			bool flag = num < 0f && num2 < 0f && num3 < 0f;
			if (vector != Vector3.Zero && flag)
			{
				result = new Vector3?(vector);
			}
			else if ({16751}.PreviousTarget != null)
			{
				Vector2 value5 = {16751}.PreviousTarget.Position.XZ();
				flag = (Vector2.Dot((value5 - value).Normal(), value2) < 0f);
				if (flag && count > 1)
				{
					flag = (Vector2.Dot((value5 - value).Normal(), value3) < 0f);
				}
				if (flag && count > 2)
				{
					flag = (Vector2.Dot((value5 - value).Normal(), value4) < 0f);
				}
				if (flag)
				{
					result = new Vector3?({16751}.PreviousTarget.Position);
				}
			}
			return result;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00013127 File Offset: 0x00011327
		private void {16752}(IsleUnitsCloudRenderer.Unit {16753}, ref FrameTime {16754})
		{
			this.{16757}({16753}, ref {16754});
			this.{16760}({16753}, ref {16754});
			IsleUnitsCloudRenderer.<UpdateUnit>g__HandleUnitAnimation|58_4({16753});
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0001313F File Offset: 0x0001133F
		[CompilerGenerated]
		internal static bool <Update>g__AreColliding|54_0(IsleUnitsCloudRenderer.Unit {16755}, IsleUnitsCloudRenderer.Unit {16756})
		{
			return Vector2.DistanceSquared({16755}.Renderer.LocalTransformOrNull.Translation.XZ(), {16756}.Renderer.LocalTransformOrNull.Translation.XZ()) <= 4f;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0001317C File Offset: 0x0001137C
		[CompilerGenerated]
		private void {16757}(IsleUnitsCloudRenderer.Unit {16758}, ref FrameTime {16759})
		{
			if ({16758}.UpdateWalkingTime <= 0f)
			{
				Vector3? vector;
				if (!this.{16779}[{16758}].Any<IsleUnitsCloudRenderer.Unit>())
				{
					vector = new Vector3?(this.{16746}({16758}.Renderer.LocalTransformOrNull, true, 0));
					if (vector != Vector3.Zero)
					{
						goto IL_73;
					}
				}
				Vector3? vector2;
				vector = (vector2 = this.TryGetCollisionsExitPoint({16758}));
				if (vector2 == null)
				{
					{16758}.UpdateWalkingTime = Rand.Range(1000f, 2000f);
					goto IL_C8;
				}
				IL_73:
				{16758}.Target = new IsleUnitsCloudRenderer.WalkTarget({16758}.Renderer.LocalTransformOrNull, vector.Value);
				{16758}.UpdateWalkingTime = Rand.Range(this.WalkingDurationLimit.Item1, this.WalkingDurationLimit.Item2);
			}
			IL_C8:
			{16758}.UpdateWalkingTime -= {16759}.msElapsed;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00013264 File Offset: 0x00011464
		[CompilerGenerated]
		private void {16760}(IsleUnitsCloudRenderer.Unit {16761}, ref FrameTime {16762})
		{
			if ({16761}.Target == null)
			{
				return;
			}
			Transform3D localTransformOrNull = {16761}.Renderer.LocalTransformOrNull;
			Vector2 value = localTransformOrNull.Translation.XZ();
			Vector2 value2 = {16761}.Target.Position.XZ();
			Vector2 vector = value - value2;
			{16761}.MovingSpeed = {16761}.RandomValue * 1.75f * 1f;
			if (vector.LengthSquared() < 4f)
			{
				{16761}.MovingSpeed *= 0.66f;
			}
			Vector3 vector2 = IsleUnitsCloudRenderer.<UpdateUnit>g__GetTranslationVector|58_2({16761}, {16762}.msElapsed / 1000f);
			localTransformOrNull.Translation.X = localTransformOrNull.Translation.X + vector2.X;
			localTransformOrNull.Translation.Z = localTransformOrNull.Translation.Z + vector2.Y;
			localTransformOrNull.Translation.Y = vector2.Z;
			this.{16765}({16761});
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00013334 File Offset: 0x00011534
		[CompilerGenerated]
		internal static Vector3 <UpdateUnit>g__GetTranslationVector|58_2(IsleUnitsCloudRenderer.Unit {16763}, float {16764})
		{
			Transform3D localTransformOrNull = {16763}.Renderer.LocalTransformOrNull;
			Vector2 value = {16763}.Target.Position.XZ();
			Vector2 vector = (localTransformOrNull.Translation.XZ() - value).Normal();
			float num = MathF.Atan2(vector.Y, vector.X) + 1.5707964f;
			Geometry.AxisNormFast(ref num);
			Geometry.AngularMovement(ref localTransformOrNull.Yaw, num, {16764} * 3f * 1f);
			Vector2 vector2 = Geometry.SubstructRotate(localTransformOrNull.Yaw + 1.5707964f, {16764} * 2f * 1f);
			float num2 = 1f - Vector2.Distance(localTransformOrNull.Translation.XZ(), value) / {16763}.Target.InitialDistance;
			{16763}.MovingSpeed *= 1f - 0.9f * Geometry.InverseLerp(0.7853982f, 2.0943952f, Geometry.AxisDistance(localTransformOrNull.Yaw, num));
			return new Vector3(vector2.X * {16763}.MovingSpeed, vector2.Y * {16763}.MovingSpeed, {16763}.Target.InitialY + ({16763}.Target.Position.Y - {16763}.Target.InitialY) * num2);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00013474 File Offset: 0x00011674
		[CompilerGenerated]
		private void {16765}(IsleUnitsCloudRenderer.Unit {16766})
		{
			if (({16766}.Renderer.LocalTransformOrNull.Translation.XZ() - {16766}.Target.Position.XZ()).LengthSquared() < 0.25f)
			{
				if (Rand.Chanse(this.StopChanseAfterAchievingPoint))
				{
					{16766}.UpdateWalkingTime = (float)(Rand.Chanse(30f) ? 5000 : 12000);
				}
				else
				{
					{16766}.UpdateWalkingTime = -1f;
				}
				{16766}.Target = null;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000134FC File Offset: 0x000116FC
		[CompilerGenerated]
		internal static void <UpdateUnit>g__HandleUnitAnimation|58_4(IsleUnitsCloudRenderer.Unit {16767})
		{
			UnitAnimation unitAnimation = ({16767}.Target == null) ? UnitAnimation.Idle_1 : UnitAnimation.Walk;
			UnitAnimation unitAnimation2 = ({16767}.CurrentAnimation == UnitAnimation.Idle_2) ? UnitAnimation.Idle_1 : {16767}.CurrentAnimation;
			bool isFinished = {16767}.Renderer.Animation.IsFinished;
			if (unitAnimation != unitAnimation2 || isFinished)
			{
				UnitAnimation unitAnimation3 = unitAnimation;
				if (unitAnimation == UnitAnimation.Idle_1)
				{
					if ({16767}.UpdateWalkingTime < 3000f && {16767}.Target == null && {16767}.Animations.ContainsKey(UnitAnimation.Kneeling))
					{
						unitAnimation3 = UnitAnimation.Kneeling;
					}
					else if (Math.Sin(Global.Game.GameTotalTimeSec * 0.08 - (double){16767}.RandomValue) > -0.3 && {16767}.Animations.ContainsKey(UnitAnimation.Looking))
					{
						unitAnimation3 = UnitAnimation.Looking;
					}
					else
					{
						unitAnimation3 = Rand.Pick<UnitAnimation>(new UnitAnimation[]
						{
							UnitAnimation.Idle_1,
							UnitAnimation.Idle_2
						});
						if (unitAnimation3 == UnitAnimation.Idle_2 && !{16767}.Animations.ContainsKey(unitAnimation3))
						{
							unitAnimation3 = UnitAnimation.Idle_1;
						}
					}
				}
				{16767}.CurrentAnimation = unitAnimation3;
				{16767}.Renderer.Animation.StartClip({16767}.Animations[unitAnimation3], (isFinished && unitAnimation3 == UnitAnimation.Cover_down) ? new TimeSpan?(TimeSpan.FromSeconds(0.9)) : null);
			}
		}

		// Token: 0x040001C6 RID: 454
		public readonly Tlist<Vector3> PointCloud;

		// Token: 0x040001C7 RID: 455
		[CompilerGenerated]
		private bool {16768};

		// Token: 0x040001C8 RID: 456
		[CompilerGenerated]
		private float {16769};

		// Token: 0x040001C9 RID: 457
		[CompilerGenerated]
		private float {16770};

		// Token: 0x040001CA RID: 458
		[CompilerGenerated]
		private bool {16771};

		// Token: 0x040001CB RID: 459
		[CompilerGenerated]
		private bool {16772};

		// Token: 0x040001CC RID: 460
		[CompilerGenerated]
		private float {16773};

		// Token: 0x040001CD RID: 461
		[TupleElementNames(new string[]
		{
			"minMs",
			"maxMs"
		})]
		[CompilerGenerated]
		private ValueTuple<float, float> {16774};

		// Token: 0x040001CE RID: 462
		[CompilerGenerated]
		private Func<UWModel, int, Texture2D> {16775};

		// Token: 0x040001CF RID: 463
		[CompilerGenerated]
		private float {16776};

		// Token: 0x040001D0 RID: 464
		private const float genericUnitSpeed = 1f;

		// Token: 0x040001D1 RID: 465
		private const float minCollisionDistance = 2f;

		// Token: 0x040001D2 RID: 466
		private Tlist<IsleUnitsCloudRenderer.Unit> {16777} = new Tlist<IsleUnitsCloudRenderer.Unit>();

		// Token: 0x040001D3 RID: 467
		private ModelTransformedScene {16778};

		// Token: 0x040001D4 RID: 468
		private Dictionary<IsleUnitsCloudRenderer.Unit, List<IsleUnitsCloudRenderer.Unit>> {16779} = new Dictionary<IsleUnitsCloudRenderer.Unit, List<IsleUnitsCloudRenderer.Unit>>();

		// Token: 0x040001D5 RID: 469
		private static int swapOrder;

		// Token: 0x0200004F RID: 79
		private enum DebugMode
		{
			// Token: 0x040001D7 RID: 471
			None,
			// Token: 0x040001D8 RID: 472
			StaticAndDeep,
			// Token: 0x040001D9 RID: 473
			Dynamic
		}

		// Token: 0x02000050 RID: 80
		private class WalkTarget
		{
			// Token: 0x0600023E RID: 574 RVA: 0x0001362A File Offset: 0x0001182A
			public WalkTarget(Transform3D {16782}, Vector3 {16783})
			{
				this.Position = {16783};
				this.InitialY = {16782}.Translation.Y;
				this.InitialDistance = Vector2.Distance({16783}.XZ(), {16782}.Translation.XZ());
			}

			// Token: 0x040001DA RID: 474
			public Vector3 Position;

			// Token: 0x040001DB RID: 475
			public float InitialY;

			// Token: 0x040001DC RID: 476
			public float InitialDistance;
		}

		// Token: 0x02000051 RID: 81
		private class Unit
		{
			// Token: 0x17000064 RID: 100
			// (get) Token: 0x0600023F RID: 575 RVA: 0x00013666 File Offset: 0x00011866
			// (set) Token: 0x06000240 RID: 576 RVA: 0x0001366E File Offset: 0x0001186E
			public IsleUnitsCloudRenderer.WalkTarget Target
			{
				get
				{
					return this.{16794};
				}
				set
				{
					if (this.{16794} != null)
					{
						this.PreviousTarget = this.{16794};
					}
					this.{16794} = value;
				}
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000241 RID: 577 RVA: 0x0001368B File Offset: 0x0001188B
			// (set) Token: 0x06000242 RID: 578 RVA: 0x00013693 File Offset: 0x00011893
			public IsleUnitsCloudRenderer.WalkTarget PreviousTarget { get; private set; }

			// Token: 0x06000243 RID: 579 RVA: 0x0001369C File Offset: 0x0001189C
			public Unit(UWModel {16789}, Vector3 {16790}, float {16791})
			{
				this.Animations = (Dictionary<UnitAnimation, AnimationClip>){16789}.Tag;
				this.Renderer = new ModelRenderer({16789}, null, 0.6f * this.RandomValue)
				{
					LocalTransformOrNull = new Transform3D()
				};
				this.Renderer.LocalTransformOrNull.Translation = {16790};
				this.Renderer.LocalTransformOrNull.MiddleScale = Rand.Range(0.95f, 1.05f) * {16791};
			}

			// Token: 0x06000244 RID: 580 RVA: 0x00013736 File Offset: 0x00011936
			public override int GetHashCode()
			{
				return 108 + this.Renderer.GetHashCode();
			}

			// Token: 0x06000245 RID: 581 RVA: 0x00013748 File Offset: 0x00011948
			public override bool Equals(object {16792})
			{
				IsleUnitsCloudRenderer.Unit unit = (IsleUnitsCloudRenderer.Unit){16792};
				return unit != null && unit.GetHashCode() == this.GetHashCode();
			}

			// Token: 0x040001DD RID: 477
			public readonly ModelRenderer Renderer;

			// Token: 0x040001DE RID: 478
			[CompilerGenerated]
			private IsleUnitsCloudRenderer.WalkTarget {16793};

			// Token: 0x040001DF RID: 479
			public float UpdateWalkingTime;

			// Token: 0x040001E0 RID: 480
			public float RandomValue = Rand.Range(0.8f, 1.4f);

			// Token: 0x040001E1 RID: 481
			public float MovingSpeed = 1f;

			// Token: 0x040001E2 RID: 482
			public UnitAnimation CurrentAnimation;

			// Token: 0x040001E3 RID: 483
			public Dictionary<UnitAnimation, AnimationClip> Animations;

			// Token: 0x040001E4 RID: 484
			private IsleUnitsCloudRenderer.WalkTarget {16794};
		}
	}
}
