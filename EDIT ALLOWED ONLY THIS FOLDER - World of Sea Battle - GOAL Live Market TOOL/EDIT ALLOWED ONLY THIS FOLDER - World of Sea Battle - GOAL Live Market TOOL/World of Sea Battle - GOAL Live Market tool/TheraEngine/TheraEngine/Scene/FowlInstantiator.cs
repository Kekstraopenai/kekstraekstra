using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;

namespace TheraEngine.Scene
{
	// Token: 0x02000036 RID: 54
	public class FowlInstantiator
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00009304 File Offset: 0x00007504
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000930C File Offset: 0x0000750C
		public bool IsEnabled { get; set; } = true;

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00009315 File Offset: 0x00007515
		public ModelTransformedScene Scene
		{
			get
			{
				return this.{11853};
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00009320 File Offset: 0x00007520
		public FowlInstantiator(FowlInstantiator.Type {11848}, string {11849}, params UWModel[] {11850})
		{
			this.{11856} = new Tlist<FowlInstantiator.Instance>();
			this.{11853} = new ModelTransformedScene();
			this.{11858} = {11848};
			this.{11854} = {11850};
			this.{11855} = {11849};
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009370 File Offset: 0x00007570
		public void Update(ref FrameTime {11851})
		{
			float y = HashHelper.SphericalVectorFromLerp(142f + (float)Engine.Game.GameTotalTimeSec * 0.1f).Y;
			this.{11853}.VisibleTestType = ModelSceneVisibleTest.Disable;
			this.{11857} = Geometry.Saturate(this.{11857} + {11851}.secElapsed * y / 10f);
			if (this.IsEnabled && (float)this.{11856}.Size < 20f * this.{11857} && Rand.Chanse({11851}.Factor * ((this.{11858} == FowlInstantiator.Type.Fish) ? 0.3f : 2f)))
			{
				Vector2 vector = Rand.Chanse(40f) ? Vector2.Zero : (Rand.NextRadialVector2(130f, 150f) * ((this.{11858} == FowlInstantiator.Type.Bird) ? 1f : 0.2f));
				int num = (this.{11858} == FowlInstantiator.Type.Bird) ? 1 : Rand.RangeInt(2, 4);
				for (int i = 0; i < num; i++)
				{
					Vector3 position = Engine.Game.GraphicsSystem.Camera.Position;
					position.X += vector.X;
					position.Z += vector.Y;
					if (this.{11858} == FowlInstantiator.Type.Bird)
					{
						position.Y += 10f;
					}
					else
					{
						position.Y = Rand.Range(-2.5f, -1.5f);
					}
					FowlInstantiator.Instance instance = new FowlInstantiator.Instance(this.{11858}, Rand.Pick<UWModel>(this.{11854}), this.{11855}, Vector3.Forward, position);
					this.{11853}.AddObject(instance.renderer, true);
					this.{11856}.Add(instance);
				}
			}
			int num2 = (this.{11858} == FowlInstantiator.Type.Bird) ? 200 : 60;
			for (int j = 0; j < this.{11856}.Size; j++)
			{
				FowlInstantiator.Instance instance2 = this.{11856}.Array[j];
				instance2.Update(ref {11851});
				if (Vector3.DistanceSquared(instance2.renderer.LocalTransformOrNull.Translation, Engine.Game.GraphicsSystem.Camera.Position) > (float)(num2 * num2))
				{
					this.{11856}.FastRemoveAt(j);
					j--;
					this.{11853}.Remove(instance2.renderer);
				}
			}
		}

		// Token: 0x040000F4 RID: 244
		[CompilerGenerated]
		private bool {11852};

		// Token: 0x040000F5 RID: 245
		private ModelTransformedScene {11853};

		// Token: 0x040000F6 RID: 246
		private UWModel[] {11854};

		// Token: 0x040000F7 RID: 247
		private string {11855};

		// Token: 0x040000F8 RID: 248
		private Tlist<FowlInstantiator.Instance> {11856};

		// Token: 0x040000F9 RID: 249
		private float {11857} = 0.5f;

		// Token: 0x040000FA RID: 250
		private FowlInstantiator.Type {11858};

		// Token: 0x02000037 RID: 55
		public enum Type
		{
			// Token: 0x040000FC RID: 252
			Bird,
			// Token: 0x040000FD RID: 253
			Fish
		}

		// Token: 0x02000038 RID: 56
		private class Instance
		{
			// Token: 0x060001B2 RID: 434 RVA: 0x000095C8 File Offset: 0x000077C8
			public Instance(FowlInstantiator.Type {11864}, UWModel {11865}, string {11866}, Vector3 {11867}, Vector3 {11868})
			{
				this.{11874} = {11864};
				this.{11870} = {11867};
				this.renderer = new ModelRenderer({11865}, {11866}, 1f);
				this.renderer.LocalTransformOrNull = new Transform3D();
				this.renderer.LocalTransformOrNull.Translation = {11868};
				this.renderer.LocalTransformOrNull.MiddleScale = (({11864} == FowlInstantiator.Type.Bird) ? 0.004f : 0.5f);
				this.{11875} = {11868}.Y;
				this.{11871} = Rand.Range(0.7f, 1.3f);
			}

			// Token: 0x060001B3 RID: 435 RVA: 0x00009660 File Offset: 0x00007860
			public void Update(ref FrameTime {11869})
			{
				this.renderer.LocalTransformOrNull.Yaw = MathF.Atan2(this.{11870}.Z, this.{11870}.X) + 1.5707964f;
				this.renderer.LocalTransformOrNull.Roll = -1.5707964f;
				this.renderer.LocalTransformOrNull.Pitch = MathF.Atan(this.{11870}.Y);
				if (this.{11874} == FowlInstantiator.Type.Bird)
				{
					this.renderer.LocalTransformOrNull.Translation += this.{11870} * {11869}.secElapsed * 13f * this.{11871};
				}
				else
				{
					this.renderer.LocalTransformOrNull.Translation += this.{11870} * {11869}.secElapsed * 2f * this.{11871};
				}
				if (this.{11874} == FowlInstantiator.Type.Bird && this.renderer.LocalTransformOrNull.Translation.Y < 5f)
				{
					this.{11870}.Y = Math.Max(0f, this.{11870}.Y);
					this.{11872}.Y = Math.Max(0.1f, this.{11872}.Y);
				}
				if (this.{11874} == FowlInstantiator.Type.Fish)
				{
					this.renderer.LocalTransformOrNull.Translation.Y = this.{11875} + (float)Math.Sin(Engine.Game.GameTotalTimeSec * (double)this.{11871} * 0.5) * 0.5f;
				}
				if (this.{11873} > 0f)
				{
					{11869}.EvaluteTimerMs(ref this.{11873});
					Vector2 vector = this.{11870}.XZ();
					Geometry.RotateVector2Fast(ref vector, {11869}.secElapsed * this.{11872}.X, out vector);
					this.{11870}.Y = this.{11870}.Y + {11869}.secElapsed * this.{11872}.Y;
					this.{11870}.X = vector.X;
					this.{11870}.Z = vector.Y;
					this.{11870}.Normalize();
					if (this.{11874} == FowlInstantiator.Type.Fish)
					{
						this.{11870}.Y = 0f;
						return;
					}
				}
				else
				{
					this.{11873} = Rand.Range(2000f, 11000f);
					this.{11872} = new Vector2(Rand.Range(-1.2f, 1.2f), Rand.Range(-0.1f, 0.1f));
					if (this.{11874} == FowlInstantiator.Type.Fish)
					{
						this.{11872} *= 0.3f;
					}
				}
			}

			// Token: 0x040000FE RID: 254
			public ModelRenderer renderer;

			// Token: 0x040000FF RID: 255
			private Vector3 {11870};

			// Token: 0x04000100 RID: 256
			private float {11871};

			// Token: 0x04000101 RID: 257
			private Vector2 {11872};

			// Token: 0x04000102 RID: 258
			private float {11873};

			// Token: 0x04000103 RID: 259
			private FowlInstantiator.Type {11874};

			// Token: 0x04000104 RID: 260
			private float {11875};
		}
	}
}
