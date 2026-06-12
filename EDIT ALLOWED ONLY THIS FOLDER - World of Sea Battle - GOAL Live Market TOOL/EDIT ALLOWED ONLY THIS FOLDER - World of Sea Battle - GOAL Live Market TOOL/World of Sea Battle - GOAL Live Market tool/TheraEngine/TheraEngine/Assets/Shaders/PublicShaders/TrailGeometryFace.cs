using System;
using Microsoft.Xna.Framework;
using TheraEngine.Graphics;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x02000183 RID: 387
	public class TrailGeometryFace : BillboardParent<TrailShader.Vertex>
	{
		// Token: 0x06000A1A RID: 2586 RVA: 0x000324FE File Offset: 0x000306FE
		internal TrailGeometryFace()
		{
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00032506 File Offset: 0x00030706
		internal void SetPos(float {15611}, float {15612})
		{
			this.SetPos(ref {15611}, ref {15612});
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00032512 File Offset: 0x00030712
		internal void SetPos(Vector3 {15613}, Vector3 {15614}, Vector3 {15615}, Vector3 {15616})
		{
			this.SetPos(ref {15613}, ref {15614}, ref {15615}, ref {15616});
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00032524 File Offset: 0x00030724
		internal void SetPos(ref float {15617}, ref float {15618})
		{
			BillboardParent<TrailShader.Vertex>._cs3_1.X = -{15617} / 2f;
			BillboardParent<TrailShader.Vertex>._cs3_1.Y = -{15618} / 2f;
			BillboardParent<TrailShader.Vertex>._cs3_1.Z = 0f;
			BillboardParent<TrailShader.Vertex>._cs3_2.X = -{15617} / 2f;
			BillboardParent<TrailShader.Vertex>._cs3_2.Y = {15618} / 2f;
			BillboardParent<TrailShader.Vertex>._cs3_2.Z = 0f;
			BillboardParent<TrailShader.Vertex>._cs3_3.X = {15617} / 2f;
			BillboardParent<TrailShader.Vertex>._cs3_3.Y = {15618} / 2f;
			BillboardParent<TrailShader.Vertex>._cs3_3.Z = 0f;
			BillboardParent<TrailShader.Vertex>._cs3_4.X = {15617} / 2f;
			BillboardParent<TrailShader.Vertex>._cs3_4.Y = -{15618} / 2f;
			BillboardParent<TrailShader.Vertex>._cs3_4.Z = 0f;
			this.SetPos(ref BillboardParent<TrailShader.Vertex>._cs3_1, ref BillboardParent<TrailShader.Vertex>._cs3_2, ref BillboardParent<TrailShader.Vertex>._cs3_3, ref BillboardParent<TrailShader.Vertex>._cs3_4);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0003261C File Offset: 0x0003081C
		internal void SetPos(ref Vector3 {15619}, ref Vector3 {15620}, ref Vector3 {15621}, ref Vector3 {15622})
		{
			this.array[0].Position = {15620};
			this.array[1].Position = {15621};
			this.array[2].Position = {15619};
			this.array[3] = this.array[2];
			this.array[4] = this.array[1];
			this.array[5].Position = {15622};
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000326B8 File Offset: 0x000308B8
		internal void Transform(Matrix {15623})
		{
			Vector3.Transform(ref this.array[0].Position, ref {15623}, out this.array[0].Position);
			Vector3.Transform(ref this.array[1].Position, ref {15623}, out this.array[1].Position);
			Vector3.Transform(ref this.array[2].Position, ref {15623}, out this.array[2].Position);
			this.array[3] = this.array[2];
			this.array[4] = this.array[1];
			Vector3.Transform(ref this.array[5].Position, ref {15623}, out this.array[5].Position);
		}
	}
}
