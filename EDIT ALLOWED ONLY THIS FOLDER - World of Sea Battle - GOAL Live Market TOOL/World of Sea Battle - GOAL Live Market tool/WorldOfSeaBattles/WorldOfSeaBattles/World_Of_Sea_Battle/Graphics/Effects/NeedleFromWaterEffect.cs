using System;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004A9 RID: 1193
	internal sealed class NeedleFromWaterEffect : GameEffect
	{
		// Token: 0x06001A25 RID: 6693 RVA: 0x000E8F7C File Offset: 0x000E717C
		public NeedleFromWaterEffect(Vector2 {24518}, bool {24519}) : base(true)
		{
			this.{24522} = (float)({24519} ? 10000 : 7000);
			this.{24523} = {24518};
			this.{24524} = new ModelTransformedScene();
			this.{24524}.Transform.Translation.X = {24518}.X;
			this.{24524}.Transform.Translation.Z = {24518}.Y;
			this.{24525} = new Tlist<ModelRenderer>();
			for (int i = 0; i < 18; i++)
			{
				ModelRenderer modelRenderer = new ModelRenderer(LocalContent.Loaded.NeedleModel);
				float num = 0.34906587f * (float)i;
				Vector2 vector = Geometry.SubstructRotate(num, 50f);
				modelRenderer.LocalTransformOrNull = new Transform3D(new Vector3(vector.X, 0f, vector.Y), new Vector3(0f, num + 3.1415927f, 0f), Vector3.One * 1.5f);
				modelRenderer.Tag = new ValueTuple<Vector3, Vector2>(new Vector3(vector.X, 0f, vector.Y), Rand.NextVector2(-3f, 3f));
				this.{24524}.AddObject(modelRenderer, true);
				this.{24525}.Add(modelRenderer);
			}
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x000E90C8 File Offset: 0x000E72C8
		public override void Update(ref FrameTime {24520}, out bool {24521})
		{
			for (int i = 0; i < this.{24525}.Size; i++)
			{
				ModelRenderer modelRenderer = this.{24525}.Array[i];
				float num = (float)Math.Pow((double)((this.{24522} > 7000f) ? (1f - (this.{24522} - 7000f) / 3000f) : (this.{24522} / 7000f)), 0.25);
				modelRenderer.LocalTransformOrNull.Translation.Y = -25f + num * 25f;
				ValueTuple<Vector3, Vector2> valueTuple = (ValueTuple<Vector3, Vector2>)modelRenderer.Tag;
				modelRenderer.LocalTransformOrNull.Translation.X = valueTuple.Item1.X + (1f - num) * valueTuple.Item2.X;
				modelRenderer.LocalTransformOrNull.Translation.Z = valueTuple.Item1.Z + (1f - num) * valueTuple.Item2.Y;
				if (this.{24526} == i && Rand.Chanse(4f * {24520}.Factor))
				{
					FXEngine.CreateSingleWaterParticle2((modelRenderer.LocalTransformOrNull.Translation * 0.95f + this.{24524}.Transform.Translation) * new Vector3(1f, 0f, 1f), 3f, true, 0f);
				}
				this.{24526}++;
				if (this.{24526} >= this.{24525}.Size)
				{
					this.{24526} = 0;
				}
			}
			this.{24522} -= {24520}.msElapsed;
			{24521} = (this.{24522} < 1f);
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000E9284 File Offset: 0x000E7484
		public override void Render3D()
		{
			Engine.GS.SetDepthBuffer(DepthBuffer.ReadAndWrite);
			Global.Render.CommonShader.RenderObject(this.{24524}, false, 1f, false, 0f, false);
			Engine.GS.SetDepthBuffer(DepthBuffer.ReadOnly);
		}

		// Token: 0x04001889 RID: 6281
		private float {24522};

		// Token: 0x0400188A RID: 6282
		private Vector2 {24523};

		// Token: 0x0400188B RID: 6283
		private ModelTransformedScene {24524};

		// Token: 0x0400188C RID: 6284
		private Tlist<ModelRenderer> {24525};

		// Token: 0x0400188D RID: 6285
		private int {24526};
	}
}
