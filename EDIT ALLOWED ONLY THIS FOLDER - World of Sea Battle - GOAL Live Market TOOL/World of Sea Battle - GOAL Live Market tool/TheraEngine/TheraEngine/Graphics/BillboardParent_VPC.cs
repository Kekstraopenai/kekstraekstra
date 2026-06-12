using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Graphics
{
	// Token: 0x02000139 RID: 313
	public class BillboardParent_VPC : BillboardParent<VertexPositionColor>
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x0002B247 File Offset: 0x00029447
		public BillboardParent_VPC()
		{
			this.SetCol(BillboardParent<VertexPositionColor>.whiteColor);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0002B25A File Offset: 0x0002945A
		public void SetPos(float {14905}, float {14906})
		{
			this.SetPos(ref {14905}, ref {14906});
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002B266 File Offset: 0x00029466
		public void SetPos(Vector3 {14907}, Vector3 {14908}, Vector3 {14909}, Vector3 {14910})
		{
			this.SetPos(ref {14907}, ref {14908}, ref {14909}, ref {14910});
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0002B278 File Offset: 0x00029478
		public void SetPos(ref float {14911}, ref float {14912})
		{
			BillboardParent<VertexPositionColor>._cs3_1.X = -{14911} / 2f;
			BillboardParent<VertexPositionColor>._cs3_1.Y = -{14912} / 2f;
			BillboardParent<VertexPositionColor>._cs3_1.Z = 0f;
			BillboardParent<VertexPositionColor>._cs3_2.X = -{14911} / 2f;
			BillboardParent<VertexPositionColor>._cs3_2.Y = {14912} / 2f;
			BillboardParent<VertexPositionColor>._cs3_2.Z = 0f;
			BillboardParent<VertexPositionColor>._cs3_3.X = {14911} / 2f;
			BillboardParent<VertexPositionColor>._cs3_3.Y = {14912} / 2f;
			BillboardParent<VertexPositionColor>._cs3_3.Z = 0f;
			BillboardParent<VertexPositionColor>._cs3_4.X = {14911} / 2f;
			BillboardParent<VertexPositionColor>._cs3_4.Y = -{14912} / 2f;
			BillboardParent<VertexPositionColor>._cs3_4.Z = 0f;
			this.SetPos(ref BillboardParent<VertexPositionColor>._cs3_1, ref BillboardParent<VertexPositionColor>._cs3_2, ref BillboardParent<VertexPositionColor>._cs3_3, ref BillboardParent<VertexPositionColor>._cs3_4);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0002B370 File Offset: 0x00029570
		public void SetPos(ref Vector3 {14913}, ref Vector3 {14914}, ref Vector3 {14915}, ref Vector3 {14916})
		{
			this.array[0].Position = {14914};
			this.array[1].Position = {14915};
			this.array[2].Position = {14913};
			this.array[3] = this.array[2];
			this.array[4] = this.array[1];
			this.array[5].Position = {14916};
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0002B40C File Offset: 0x0002960C
		public void SetCol(Color {14917})
		{
			this.array[0].Color = {14917};
			this.array[1].Color = {14917};
			this.array[2].Color = {14917};
			this.array[3] = this.array[2];
			this.array[4] = this.array[1];
			this.array[5].Color = {14917};
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0002B494 File Offset: 0x00029694
		public void SetCol(Color {14918}, Color {14919}, Color {14920}, Color {14921})
		{
			this.array[0].Color = {14919};
			this.array[1].Color = {14921};
			this.array[2].Color = {14918};
			this.array[3] = this.array[2];
			this.array[4] = this.array[1];
			this.array[5].Color = {14920};
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0002B51C File Offset: 0x0002971C
		public void Transform(Matrix {14922})
		{
			Vector3.Transform(ref this.array[0].Position, ref {14922}, out this.array[0].Position);
			Vector3.Transform(ref this.array[1].Position, ref {14922}, out this.array[1].Position);
			Vector3.Transform(ref this.array[2].Position, ref {14922}, out this.array[2].Position);
			this.array[3] = this.array[2];
			this.array[4] = this.array[1];
			Vector3.Transform(ref this.array[5].Position, ref {14922}, out this.array[5].Position);
		}
	}
}
