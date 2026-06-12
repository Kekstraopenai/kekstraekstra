using System;
using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Script;
using TheraEngine.Assets.Script.ScriptTypes;
using TheraEngine.Collections;
using TheraEngine.Graphics;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Scripts
{
	// Token: 0x02000016 RID: 22
	internal class PrecipitationScript : UpdateRenderScript
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003E0F File Offset: 0x0000200F
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003E17 File Offset: 0x00002017
		public float CurrentPower
		{
			get
			{
				return this.currentPower;
			}
			set
			{
				this.currentPower = MathHelper.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003E2F File Offset: 0x0000202F
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003E37 File Offset: 0x00002037
		public float TargetPower
		{
			get
			{
				return this.{16244};
			}
			set
			{
				this.{16244} = MathHelper.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003E50 File Offset: 0x00002050
		public PrecipitationScript(ScriptManager {16229}, Rectangle {16230}, Vector2 {16231}, float {16232}, float {16233}, int {16234}, bool {16235}) : base({16229})
		{
			this.{16250} = {16231};
			this.{16241} = new UWEPool<PrecipitationScript.PrecipitationParticle>(5000);
			this.{16242} = new Tlist<PrecipitationScript.PrecipitationParticle>(this.{16241}.Capacity);
			this.{16243} = new SpriteBatch3D<VertexPositionColorTexture>();
			this.{16245} = new Vector3(0f, -{16232}, 0f);
			this.{16246} = {16233};
			this.{16247} = {16234};
			this.{16251} = {16235};
			this.IsEnabled = false;
			this.{16249} = new BillboardParent_VPCT();
			this.{16249}.SetUV({16230}, AtlasObjs.Texture.Size);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003EF4 File Offset: 0x000020F4
		protected override void Initialize(out ScriptClass {16236}, out string {16237})
		{
			{16236} = ScriptClass.LoopScript;
			{16237} = "PrecipitationScript";
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003F00 File Offset: 0x00002100
		protected override void Render()
		{
			if (this.{16243}.Count != 0)
			{
				Global.Render.ItemsShader.SetForRender(Matrix.Identity, Vector4.One);
				Global.Render.ItemsShader.BeginPass(true, false);
				this.{16243}.Render(null);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003F50 File Offset: 0x00002150
		protected override void Update(ref FrameTime {16238})
		{
			Vector3 position = Engine.GS.Camera.Position;
			if (this.currentPower != this.{16244})
			{
				Geometry.Evalute(ref this.currentPower, this.{16244}, {16238}.secElapsed * 0.05f);
				this.currentPower = MathHelper.Clamp(this.currentPower, 0f, 1f);
			}
			if (!Global.Settings.EnableBasicEffects)
			{
				this.currentPower = 0f;
			}
			float num = this.currentPower * Geometry.Saturate((CommonGlobal.CurrentClientWeather.RainingLevelClient - 0.7f) / 0.3f);
			if (num != 0f)
			{
				float num2 = 1.5f * (float)this.{16247} * num * {16238}.secElapsed + this.{16248};
				int num3 = (int)num2;
				this.{16248} = num2 - (float)num3;
				for (int i = 0; i < num3; i++)
				{
					this.{16239}(ref position);
				}
			}
			this.{16243}.Reset();
			if (this.{16242}.Size != 0)
			{
				Vector3 vector = CommonGlobal.CurrentClientWeather.WindDirection * ({16238}.secElapsed * 15f);
				Vector2 vector2 = new Vector2(position.X, position.Z);
				for (int j = 0; j < this.{16242}.Size; j++)
				{
					PrecipitationScript.PrecipitationParticle precipitationParticle = this.{16242}.Array[j];
					bool flag;
					precipitationParticle.Update({16238}.secElapsed, ref vector, ref position, out flag);
					if (flag)
					{
						this.{16242}.FastRemoveAt(j);
						this.{16241}.Add(precipitationParticle);
						j--;
					}
					else
					{
						precipitationParticle.AddVertices(ref vector2, this.{16243}, this.{16249}, ref this.{16250}, this.{16251});
					}
				}
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004108 File Offset: 0x00002308
		private void {16239}(ref Vector3 {16240})
		{
			PrecipitationScript.PrecipitationParticle precipitationParticle = this.{16241}.Pop();
			Vector3 vector = new Vector3({16240}.X + Rand.Range(-100f, 100f), {16240}.Y + Rand.Range(25f, 16.666666f) + 3f, {16240}.Z + Rand.Range(-100f, 100f));
			precipitationParticle.Initialize(ref vector, ref this.{16245}, this.{16246});
			this.{16242}.Add(precipitationParticle);
		}

		// Token: 0x04000030 RID: 48
		private const int maxCount = 5000;

		// Token: 0x04000031 RID: 49
		private const float farPlane = 100f;

		// Token: 0x04000032 RID: 50
		private const float timeLess = 0.2f;

		// Token: 0x04000033 RID: 51
		private const float nearPlane = 4f;

		// Token: 0x04000034 RID: 52
		private UWEPool<PrecipitationScript.PrecipitationParticle> {16241};

		// Token: 0x04000035 RID: 53
		private Tlist<PrecipitationScript.PrecipitationParticle> {16242};

		// Token: 0x04000036 RID: 54
		private SpriteBatch3D<VertexPositionColorTexture> {16243};

		// Token: 0x04000037 RID: 55
		protected float currentPower;

		// Token: 0x04000038 RID: 56
		private float {16244};

		// Token: 0x04000039 RID: 57
		private Vector3 {16245};

		// Token: 0x0400003A RID: 58
		private float {16246};

		// Token: 0x0400003B RID: 59
		private int {16247};

		// Token: 0x0400003C RID: 60
		private float {16248};

		// Token: 0x0400003D RID: 61
		private BillboardParent_VPCT {16249};

		// Token: 0x0400003E RID: 62
		private Vector2 {16250};

		// Token: 0x0400003F RID: 63
		private bool {16251};

		// Token: 0x02000017 RID: 23
		protected class PrecipitationParticle : IPoolObject
		{
			// Token: 0x06000081 RID: 129 RVA: 0x00004194 File Offset: 0x00002394
			public void Initialize(ref Vector3 {16252}, ref Vector3 {16253}, float {16254})
			{
				this.{16264} = {16252};
				this.{16265} = {16253};
				this.{16266} = false;
				this.{16267} = 0.2f;
				this.{16270} = 1f;
				this.{16268} = (this.{16269} = Rand.Range(0.66f, 1.33f));
				this.{16271} = Rand.Angle();
				this.{16272} = {16254} * (float)Rand.Sign();
			}

			// Token: 0x06000082 RID: 130 RVA: 0x00004210 File Offset: 0x00002410
			public void Update(float {16255}, ref Vector3 {16256}, ref Vector3 {16257}, out bool {16258})
			{
				float num = -2f;
				this.{16270} = 0.8f;
				if (this.{16266})
				{
					this.{16267} = -1f;
				}
				else
				{
					this.{16266} = (this.{16264}.Y < num);
					Vector3.Add(ref this.{16264}, ref {16256}, out this.{16264});
					Vector3.Multiply(ref this.{16265}, {16255}, out PrecipitationScript.PrecipitationParticle.lastSpeed);
					Vector3.Add(ref this.{16264}, ref PrecipitationScript.PrecipitationParticle.lastSpeed, out this.{16264});
				}
				this.{16271} += {16255} * this.{16272};
				Vector3.DistanceSquared(ref {16257}, ref this.{16264}, out PrecipitationScript.PrecipitationParticle.distance);
				{16258} = (PrecipitationScript.PrecipitationParticle.distance > 22500f || this.{16267} < 0f);
			}

			// Token: 0x06000083 RID: 131 RVA: 0x000042D8 File Offset: 0x000024D8
			public void AddVertices(ref Vector2 {16259}, SpriteBatch3D<VertexPositionColorTexture> {16260}, BillboardParent_VPCT {16261}, ref Vector2 {16262}, bool {16263})
			{
				if (PrecipitationScript.PrecipitationParticle.distance > 10000f || PrecipitationScript.PrecipitationParticle.distance < 16f)
				{
					return;
				}
				{16261}.SetPos({16262}.X * this.{16268}, {16262}.Y * this.{16269});
				if (this.{16272} != 0f)
				{
					Matrix.CreateRotationZ(this.{16271}, out PrecipitationScript.PrecipitationParticle.tempMatrix1);
					{16261}.Transform(PrecipitationScript.PrecipitationParticle.tempMatrix1);
				}
				{16261}.SetCol(new Color(this.{16270}, this.{16270}, this.{16270}, this.{16270}));
				Matrix.CreateTranslation(ref this.{16264}, out PrecipitationScript.PrecipitationParticle.tempMatrix0);
				Vector2 vector = new Vector2(this.{16264}.X, this.{16264}.Z);
				Matrix.CreateRotationY(-Geometry.GetRotate({16259}, vector) + 1.5707964f, out PrecipitationScript.PrecipitationParticle.tempMatrix1);
				{16261}.Transform(PrecipitationScript.PrecipitationParticle.tempMatrix1);
				{16261}.Transform(PrecipitationScript.PrecipitationParticle.tempMatrix0);
				{16260}.Add({16261}.array);
				if ({16263})
				{
					VertexPositionColorTexture[] array = {16261}.array;
					int num = 0;
					array[num].Position.X = array[num].Position.X + 3f;
					VertexPositionColorTexture[] array2 = {16261}.array;
					int num2 = 0;
					array2[num2].Position.Z = array2[num2].Position.Z + 3f;
					VertexPositionColorTexture[] array3 = {16261}.array;
					int num3 = 1;
					array3[num3].Position.X = array3[num3].Position.X + 3f;
					VertexPositionColorTexture[] array4 = {16261}.array;
					int num4 = 1;
					array4[num4].Position.Z = array4[num4].Position.Z + 3f;
					VertexPositionColorTexture[] array5 = {16261}.array;
					int num5 = 2;
					array5[num5].Position.X = array5[num5].Position.X + 3f;
					VertexPositionColorTexture[] array6 = {16261}.array;
					int num6 = 2;
					array6[num6].Position.Z = array6[num6].Position.Z + 3f;
					VertexPositionColorTexture[] array7 = {16261}.array;
					int num7 = 3;
					array7[num7].Position.X = array7[num7].Position.X + 3f;
					VertexPositionColorTexture[] array8 = {16261}.array;
					int num8 = 3;
					array8[num8].Position.Z = array8[num8].Position.Z + 3f;
					VertexPositionColorTexture[] array9 = {16261}.array;
					int num9 = 4;
					array9[num9].Position.X = array9[num9].Position.X + 3f;
					VertexPositionColorTexture[] array10 = {16261}.array;
					int num10 = 4;
					array10[num10].Position.Z = array10[num10].Position.Z + 3f;
					VertexPositionColorTexture[] array11 = {16261}.array;
					int num11 = 5;
					array11[num11].Position.X = array11[num11].Position.X + 3f;
					VertexPositionColorTexture[] array12 = {16261}.array;
					int num12 = 5;
					array12[num12].Position.Z = array12[num12].Position.Z + 3f;
					{16260}.Add({16261}.array);
					VertexPositionColorTexture[] array13 = {16261}.array;
					int num13 = 0;
					array13[num13].Position.X = array13[num13].Position.X - 6f;
					VertexPositionColorTexture[] array14 = {16261}.array;
					int num14 = 0;
					array14[num14].Position.Z = array14[num14].Position.Z - 6f;
					VertexPositionColorTexture[] array15 = {16261}.array;
					int num15 = 1;
					array15[num15].Position.X = array15[num15].Position.X - 6f;
					VertexPositionColorTexture[] array16 = {16261}.array;
					int num16 = 1;
					array16[num16].Position.Z = array16[num16].Position.Z - 6f;
					VertexPositionColorTexture[] array17 = {16261}.array;
					int num17 = 2;
					array17[num17].Position.X = array17[num17].Position.X - 6f;
					VertexPositionColorTexture[] array18 = {16261}.array;
					int num18 = 2;
					array18[num18].Position.Z = array18[num18].Position.Z - 6f;
					VertexPositionColorTexture[] array19 = {16261}.array;
					int num19 = 3;
					array19[num19].Position.X = array19[num19].Position.X - 6f;
					VertexPositionColorTexture[] array20 = {16261}.array;
					int num20 = 3;
					array20[num20].Position.Z = array20[num20].Position.Z - 6f;
					VertexPositionColorTexture[] array21 = {16261}.array;
					int num21 = 4;
					array21[num21].Position.X = array21[num21].Position.X - 6f;
					VertexPositionColorTexture[] array22 = {16261}.array;
					int num22 = 4;
					array22[num22].Position.Z = array22[num22].Position.Z - 6f;
					VertexPositionColorTexture[] array23 = {16261}.array;
					int num23 = 5;
					array23[num23].Position.X = array23[num23].Position.X - 6f;
					VertexPositionColorTexture[] array24 = {16261}.array;
					int num24 = 5;
					array24[num24].Position.Z = array24[num24].Position.Z - 6f;
					{16260}.Add({16261}.array);
					VertexPositionColorTexture[] array25 = {16261}.array;
					int num25 = 0;
					array25[num25].Position.X = array25[num25].Position.X + 6f;
					VertexPositionColorTexture[] array26 = {16261}.array;
					int num26 = 1;
					array26[num26].Position.X = array26[num26].Position.X + 6f;
					VertexPositionColorTexture[] array27 = {16261}.array;
					int num27 = 2;
					array27[num27].Position.X = array27[num27].Position.X + 6f;
					VertexPositionColorTexture[] array28 = {16261}.array;
					int num28 = 3;
					array28[num28].Position.X = array28[num28].Position.X + 6f;
					VertexPositionColorTexture[] array29 = {16261}.array;
					int num29 = 4;
					array29[num29].Position.X = array29[num29].Position.X + 6f;
					VertexPositionColorTexture[] array30 = {16261}.array;
					int num30 = 5;
					array30[num30].Position.X = array30[num30].Position.X + 6f;
					{16260}.Add({16261}.array);
					VertexPositionColorTexture[] array31 = {16261}.array;
					int num31 = 0;
					array31[num31].Position.Z = array31[num31].Position.Z + 6f;
					VertexPositionColorTexture[] array32 = {16261}.array;
					int num32 = 1;
					array32[num32].Position.Z = array32[num32].Position.Z + 6f;
					VertexPositionColorTexture[] array33 = {16261}.array;
					int num33 = 2;
					array33[num33].Position.Z = array33[num33].Position.Z + 6f;
					VertexPositionColorTexture[] array34 = {16261}.array;
					int num34 = 3;
					array34[num34].Position.Z = array34[num34].Position.Z + 6f;
					VertexPositionColorTexture[] array35 = {16261}.array;
					int num35 = 4;
					array35[num35].Position.Z = array35[num35].Position.Z + 6f;
					VertexPositionColorTexture[] array36 = {16261}.array;
					int num36 = 5;
					array36[num36].Position.Z = array36[num36].Position.Z + 6f;
					VertexPositionColorTexture[] array37 = {16261}.array;
					int num37 = 0;
					array37[num37].Position.X = array37[num37].Position.X - 6f;
					VertexPositionColorTexture[] array38 = {16261}.array;
					int num38 = 1;
					array38[num38].Position.X = array38[num38].Position.X - 6f;
					VertexPositionColorTexture[] array39 = {16261}.array;
					int num39 = 2;
					array39[num39].Position.X = array39[num39].Position.X - 6f;
					VertexPositionColorTexture[] array40 = {16261}.array;
					int num40 = 3;
					array40[num40].Position.X = array40[num40].Position.X - 6f;
					VertexPositionColorTexture[] array41 = {16261}.array;
					int num41 = 4;
					array41[num41].Position.X = array41[num41].Position.X - 6f;
					VertexPositionColorTexture[] array42 = {16261}.array;
					int num42 = 5;
					array42[num42].Position.X = array42[num42].Position.X - 6f;
					{16260}.Add({16261}.array);
				}
			}

			// Token: 0x06000084 RID: 132 RVA: 0x00003100 File Offset: 0x00001300
			public void ClearResources()
			{
			}

			// Token: 0x04000040 RID: 64
			private static Vector3 up = Vector3.Up;

			// Token: 0x04000041 RID: 65
			private static Vector3 forward = Vector3.Forward;

			// Token: 0x04000042 RID: 66
			private static float distance;

			// Token: 0x04000043 RID: 67
			private static Matrix tempMatrix0;

			// Token: 0x04000044 RID: 68
			private static Matrix tempMatrix1;

			// Token: 0x04000045 RID: 69
			private static Matrix tempMatrix2;

			// Token: 0x04000046 RID: 70
			private static Vector3 lastSpeed;

			// Token: 0x04000047 RID: 71
			private Vector3 {16264};

			// Token: 0x04000048 RID: 72
			private Vector3 {16265};

			// Token: 0x04000049 RID: 73
			private bool {16266};

			// Token: 0x0400004A RID: 74
			private float {16267};

			// Token: 0x0400004B RID: 75
			private float {16268};

			// Token: 0x0400004C RID: 76
			private float {16269};

			// Token: 0x0400004D RID: 77
			private float {16270};

			// Token: 0x0400004E RID: 78
			private float {16271};

			// Token: 0x0400004F RID: 79
			private float {16272};
		}
	}
}
