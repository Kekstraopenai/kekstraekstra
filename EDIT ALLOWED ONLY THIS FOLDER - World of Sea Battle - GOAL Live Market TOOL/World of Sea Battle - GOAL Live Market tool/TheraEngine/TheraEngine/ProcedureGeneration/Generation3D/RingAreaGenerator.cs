using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Graphics;

namespace TheraEngine.ProcedureGeneration.Generation3D
{
	// Token: 0x0200006D RID: 109
	public class RingAreaGenerator
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x000100A0 File Offset: 0x0000E2A0
		public static UserMesh Begin_VertexPositionColor(int {12427}, float {12428}, float {12429}, float {12430}, Color {12431}, Color {12432}, Color {12433}, float {12434})
		{
			SpriteBatch3D<VertexPositionColor> spriteBatch3D = new SpriteBatch3D<VertexPositionColor>({12427} * 3 * 2);
			Vector2[] array = RingAreaGenerator.GetVertices({12427}, {12434}).ToArray<Vector2>();
			int num = 0;
			for (int i = 0; i < {12427}; i++)
			{
				Vector2 vector = array[num++];
				Vector2 vector2;
				if (num == {12427})
				{
					vector2 = array[0];
				}
				else
				{
					vector2 = array[num];
				}
				BillboardParent_VPC billboardParent_VPC = new BillboardParent_VPC();
				billboardParent_VPC.SetPos(new Vector3(vector.X, {12429}, vector.Y), new Vector3(vector.X, {12430}, vector.Y), new Vector3(vector2.X, {12430}, vector2.Y), new Vector3(vector2.X, {12429}, vector2.Y));
				billboardParent_VPC.SetCol({12432}, {12433}, {12432}, {12433});
				BillboardParent_VPC billboardParent_VPC2 = new BillboardParent_VPC();
				billboardParent_VPC2.SetPos(new Vector3(vector.X, {12428}, vector.Y), new Vector3(vector.X, {12429}, vector.Y), new Vector3(vector2.X, {12429}, vector2.Y), new Vector3(vector2.X, {12428}, vector2.Y));
				billboardParent_VPC2.SetCol({12431}, {12432}, {12431}, {12432});
				spriteBatch3D.Add(billboardParent_VPC.array);
				spriteBatch3D.Add(billboardParent_VPC2.array);
			}
			return spriteBatch3D.CreateMesh();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000101FC File Offset: 0x0000E3FC
		public static UserMesh Begin_VertexPositionColorTexture(int {12435}, float {12436}, float {12437}, float {12438}, Color {12439}, Color {12440}, Color {12441}, float {12442}, Rectangle {12443}, Vector2 {12444})
		{
			SpriteBatch3D<VertexPositionColorTexture> spriteBatch3D = new SpriteBatch3D<VertexPositionColorTexture>({12435} * 3 * 2);
			Vector2[] array = RingAreaGenerator.GetVertices({12435}, {12442}).ToArray<Vector2>();
			int num = 0;
			for (int i = 0; i < {12435}; i++)
			{
				Vector2 vector = array[num++];
				Vector2 vector2;
				if (num == {12435})
				{
					vector2 = array[0];
				}
				else
				{
					vector2 = array[num];
				}
				BillboardParent_VPCT billboardParent_VPCT = new BillboardParent_VPCT();
				billboardParent_VPCT.SetPos(new Vector3(vector.X, {12437}, vector.Y), new Vector3(vector.X, {12438}, vector.Y), new Vector3(vector2.X, {12438}, vector2.Y), new Vector3(vector2.X, {12437}, vector2.Y));
				billboardParent_VPCT.SetCol({12440}, {12441}, {12440}, {12441});
				billboardParent_VPCT.SetUV(ref {12443}, ref {12444});
				BillboardParent_VPCT billboardParent_VPCT2 = new BillboardParent_VPCT();
				billboardParent_VPCT2.SetPos(new Vector3(vector.X, {12436}, vector.Y), new Vector3(vector.X, {12437}, vector.Y), new Vector3(vector2.X, {12437}, vector2.Y), new Vector3(vector2.X, {12436}, vector2.Y));
				billboardParent_VPCT2.SetCol({12439}, {12440}, {12439}, {12440});
				billboardParent_VPCT.SetUV(ref {12443}, ref {12444});
				spriteBatch3D.Add(billboardParent_VPCT.array);
				spriteBatch3D.Add(billboardParent_VPCT2.array);
			}
			return spriteBatch3D.CreateMesh();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0001036B File Offset: 0x0000E56B
		private static IEnumerable<Vector2> GetVertices(int {12445}, float {12446})
		{
			RingAreaGenerator.<GetVertices>d__2 <GetVertices>d__ = new RingAreaGenerator.<GetVertices>d__2(-2);
			<GetVertices>d__.<>3__count = {12445};
			<GetVertices>d__.<>3__clirce_radius = {12446};
			return <GetVertices>d__;
		}
	}
}
