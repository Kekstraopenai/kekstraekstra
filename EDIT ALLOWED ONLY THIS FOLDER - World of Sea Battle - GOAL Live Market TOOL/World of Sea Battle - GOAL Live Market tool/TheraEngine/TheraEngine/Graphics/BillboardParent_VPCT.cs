using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Graphics
{
	// Token: 0x0200013A RID: 314
	public class BillboardParent_VPCT : BillboardParent<VertexPositionColorTexture>
	{
		// Token: 0x060008E6 RID: 2278 RVA: 0x0002B5FD File Offset: 0x000297FD
		public BillboardParent_VPCT()
		{
			this.SetCol(BillboardParent<VertexPositionColorTexture>.whiteColor);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0002B610 File Offset: 0x00029810
		public BillboardParent_VPCT(Vector3 {14929}, Vector3 {14930}, Vector3 {14931}, Vector3 {14932}, Rectangle {14933}, Vector2 {14934}) : this()
		{
			this.SetPos(ref {14929}, ref {14930}, ref {14931}, ref {14932});
			this.SetUV(ref {14933}, ref {14934});
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0002B630 File Offset: 0x00029830
		public void SetPos(float {14935}, float {14936})
		{
			this.SetPos(ref {14935}, ref {14936});
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0002B63C File Offset: 0x0002983C
		public void SetPos(Vector3 {14937}, Vector3 {14938}, Vector3 {14939}, Vector3 {14940})
		{
			this.SetPos(ref {14937}, ref {14938}, ref {14939}, ref {14940});
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002B64C File Offset: 0x0002984C
		public void SetPos(ref float {14941}, ref float {14942})
		{
			BillboardParent<VertexPositionColorTexture>._cs3_1.X = -{14941} / 2f;
			BillboardParent<VertexPositionColorTexture>._cs3_1.Y = -{14942} / 2f;
			BillboardParent<VertexPositionColorTexture>._cs3_1.Z = 0f;
			BillboardParent<VertexPositionColorTexture>._cs3_2.X = -{14941} / 2f;
			BillboardParent<VertexPositionColorTexture>._cs3_2.Y = {14942} / 2f;
			BillboardParent<VertexPositionColorTexture>._cs3_2.Z = 0f;
			BillboardParent<VertexPositionColorTexture>._cs3_3.X = {14941} / 2f;
			BillboardParent<VertexPositionColorTexture>._cs3_3.Y = {14942} / 2f;
			BillboardParent<VertexPositionColorTexture>._cs3_3.Z = 0f;
			BillboardParent<VertexPositionColorTexture>._cs3_4.X = {14941} / 2f;
			BillboardParent<VertexPositionColorTexture>._cs3_4.Y = -{14942} / 2f;
			BillboardParent<VertexPositionColorTexture>._cs3_4.Z = 0f;
			this.SetPos(ref BillboardParent<VertexPositionColorTexture>._cs3_1, ref BillboardParent<VertexPositionColorTexture>._cs3_2, ref BillboardParent<VertexPositionColorTexture>._cs3_3, ref BillboardParent<VertexPositionColorTexture>._cs3_4);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0002B744 File Offset: 0x00029944
		public void SetPos(ref Vector3 {14943}, ref Vector3 {14944}, ref Vector3 {14945}, ref Vector3 {14946})
		{
			this.array[0].Position = {14944};
			this.array[1].Position = {14945};
			this.array[2].Position = {14943};
			this.array[3] = this.array[2];
			this.array[4] = this.array[1];
			this.array[5].Position = {14946};
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0002B7DE File Offset: 0x000299DE
		public void SetUV(Rectangle {14947}, Vector2 {14948})
		{
			this.SetUV(ref {14947}, ref {14948});
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002B7EC File Offset: 0x000299EC
		public void SetUV(ref Rectangle {14949}, ref Vector2 {14950})
		{
			BillboardParent<VertexPositionColorTexture>._cs2_1.X = (float){14949}.X / {14950}.X;
			BillboardParent<VertexPositionColorTexture>._cs2_1.Y = (float){14949}.Y / {14950}.Y;
			this.array[0].TextureCoordinate = BillboardParent<VertexPositionColorTexture>._cs2_1;
			BillboardParent<VertexPositionColorTexture>._cs2_1.X = ((float){14949}.X + (float){14949}.Width) / {14950}.X;
			this.array[1].TextureCoordinate = BillboardParent<VertexPositionColorTexture>._cs2_1;
			BillboardParent<VertexPositionColorTexture>._cs2_1.X = (float){14949}.X / {14950}.X;
			BillboardParent<VertexPositionColorTexture>._cs2_1.Y = ((float){14949}.Y + (float){14949}.Height) / {14950}.Y;
			this.array[2].TextureCoordinate = BillboardParent<VertexPositionColorTexture>._cs2_1;
			this.array[3] = this.array[2];
			this.array[4] = this.array[1];
			BillboardParent<VertexPositionColorTexture>._cs2_1.X = ((float){14949}.X + (float){14949}.Width) / {14950}.X;
			BillboardParent<VertexPositionColorTexture>._cs2_1.Y = ((float){14949}.Y + (float){14949}.Height) / {14950}.Y;
			this.array[5].TextureCoordinate = BillboardParent<VertexPositionColorTexture>._cs2_1;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002B94C File Offset: 0x00029B4C
		public void SetUV(Vector4 {14951})
		{
			BillboardParent<VertexPositionColorTexture>._cs2_1.X = {14951}.X;
			BillboardParent<VertexPositionColorTexture>._cs2_1.Y = {14951}.Y;
			this.array[0].TextureCoordinate = BillboardParent<VertexPositionColorTexture>._cs2_1;
			BillboardParent<VertexPositionColorTexture>._cs2_1.X = {14951}.Z;
			this.array[1].TextureCoordinate = BillboardParent<VertexPositionColorTexture>._cs2_1;
			BillboardParent<VertexPositionColorTexture>._cs2_1.X = {14951}.X;
			BillboardParent<VertexPositionColorTexture>._cs2_1.Y = {14951}.W;
			this.array[2].TextureCoordinate = BillboardParent<VertexPositionColorTexture>._cs2_1;
			this.array[3] = this.array[2];
			this.array[4] = this.array[1];
			BillboardParent<VertexPositionColorTexture>._cs2_1.X = {14951}.Z;
			BillboardParent<VertexPositionColorTexture>._cs2_1.Y = {14951}.W;
			this.array[5].TextureCoordinate = BillboardParent<VertexPositionColorTexture>._cs2_1;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002BA54 File Offset: 0x00029C54
		public void SetCol(Color {14952})
		{
			this.array[0].Color = {14952};
			this.array[1].Color = {14952};
			this.array[2].Color = {14952};
			this.array[3] = this.array[2];
			this.array[4] = this.array[1];
			this.array[5].Color = {14952};
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002BADC File Offset: 0x00029CDC
		public void SetCol(Color {14953}, Color {14954}, Color {14955}, Color {14956})
		{
			this.array[0].Color = {14954};
			this.array[1].Color = {14956};
			this.array[2].Color = {14953};
			this.array[3] = this.array[2];
			this.array[4] = this.array[1];
			this.array[5].Color = {14955};
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002BB64 File Offset: 0x00029D64
		public void Transform(Matrix {14957})
		{
			Vector3.Transform(ref this.array[0].Position, ref {14957}, out this.array[0].Position);
			Vector3.Transform(ref this.array[1].Position, ref {14957}, out this.array[1].Position);
			Vector3.Transform(ref this.array[2].Position, ref {14957}, out this.array[2].Position);
			this.array[3] = this.array[2];
			this.array[4] = this.array[1];
			Vector3.Transform(ref this.array[5].Position, ref {14957}, out this.array[5].Position);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0002BC48 File Offset: 0x00029E48
		public void Transform(ref Matrix {14958})
		{
			Vector3.Transform(ref this.array[0].Position, ref {14958}, out this.array[0].Position);
			Vector3.Transform(ref this.array[1].Position, ref {14958}, out this.array[1].Position);
			Vector3.Transform(ref this.array[2].Position, ref {14958}, out this.array[2].Position);
			this.array[3] = this.array[2];
			this.array[4] = this.array[1];
			Vector3.Transform(ref this.array[5].Position, ref {14958}, out this.array[5].Position);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002BD28 File Offset: 0x00029F28
		public static BillboardParent_VPCT CreatePlane(float {14959}, float {14960}, float {14961})
		{
			float num = {14959} / 2f;
			float num2 = {14960} / 2f;
			BillboardParent_VPCT billboardParent_VPCT = new BillboardParent_VPCT();
			billboardParent_VPCT.SetPos(new Vector3(-num, {14961}, -num2), new Vector3(num, {14961}, -num2), new Vector3(num, {14961}, num2), new Vector3(-num, {14961}, num2));
			return billboardParent_VPCT;
		}
	}
}
