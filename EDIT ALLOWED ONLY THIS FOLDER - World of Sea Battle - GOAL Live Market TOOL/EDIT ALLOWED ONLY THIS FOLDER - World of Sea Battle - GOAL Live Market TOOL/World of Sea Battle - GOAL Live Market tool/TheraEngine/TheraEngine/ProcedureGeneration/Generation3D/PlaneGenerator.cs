using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Graphics;

namespace TheraEngine.ProcedureGeneration.Generation3D
{
	// Token: 0x0200006C RID: 108
	public class PlaneGenerator
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		public static void Begin_VertexPositionGPUSimulation(Vector4 {12409}, float {12410}, float {12411}, Vector3 {12412}, int {12413}, Vector4 {12414}, out VertexPositionGPUSimulation[] {12415}, out short[] {12416})
		{
			Vector2 vector = new Vector2(-{12410} / 2f, -{12411} / 2f);
			{12415} = new VertexPositionGPUSimulation[{12413} * {12413}];
			for (float num = 0f; num < (float){12413}; num += 1f)
			{
				float x = {12409}.X + ({12409}.Z - {12409}.X) * (num / (float)({12413} - 1));
				float x2 = vector.X + {12410} * (num / (float)({12413} - 1));
				for (float num2 = 0f; num2 < (float){12413}; num2 += 1f)
				{
					float y = {12409}.Y + ({12409}.W - {12409}.Y) * (num2 / (float)({12413} - 1));
					float z = vector.Y + {12411} * (num2 / (float)({12413} - 1));
					{12415}[(int)(num * (float){12413} + num2)] = new VertexPositionGPUSimulation(new Vector3(x2, 0f, z), {12412}, new Vector2(x, y), {12414});
				}
			}
			{12416} = MeshHelper.CreateTriangleList({12413});
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000FEBC File Offset: 0x0000E0BC
		public static UserIndexedMesh Begin_VertexPositionTexture(Vector4 {12417}, float {12418}, float {12419}, Vector3 {12420}, int {12421})
		{
			Vector2 vector = new Vector2(-{12418} / 2f - {12420}.X, -{12419} / 2f - {12420}.Z);
			VertexPositionTexture[] array = new VertexPositionTexture[{12421} * {12421}];
			for (float num = 0f; num < (float){12421}; num += 1f)
			{
				float x = {12417}.X + ({12417}.Z - {12417}.X) * (num / (float)({12421} - 1));
				float x2 = vector.X + {12418} * (num / (float)({12421} - 1));
				for (float num2 = 0f; num2 < (float){12421}; num2 += 1f)
				{
					float y = {12417}.Y + ({12417}.W - {12417}.Y) * (num2 / (float)({12421} - 1));
					float z = vector.Y + {12419} * (num2 / (float)({12421} - 1));
					array[(int)(num * (float){12421} + num2)] = new VertexPositionTexture(new Vector3(x2, {12420}.Y, z), new Vector2(x, y));
				}
			}
			short[] array2 = MeshHelper.CreateTriangleList({12421});
			{12421} = array.Length;
			return UserIndexedMesh.Create<VertexPositionTexture>(array, {12421}, array2, array2.Length);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
		public static UserIndexedMesh Begin_VertexPositionColor(float {12422}, float {12423}, Vector3 {12424}, int {12425}, Color {12426})
		{
			Vector2 vector = new Vector2(-{12422} / 2f - {12424}.X, -{12423} / 2f - {12424}.Z);
			VertexPositionColor[] array = new VertexPositionColor[{12425} * {12425}];
			for (float num = 0f; num < (float){12425}; num += 1f)
			{
				float x = vector.X + {12422} * (num / (float)({12425} - 1));
				for (float num2 = 0f; num2 < (float){12425}; num2 += 1f)
				{
					float z = vector.Y + {12423} * (num2 / (float)({12425} - 1));
					array[(int)(num * (float){12425} + num2)] = new VertexPositionColor(new Vector3(x, {12424}.Y, z), {12426});
				}
			}
			short[] array2 = MeshHelper.CreateTriangleList({12425});
			{12425} = array.Length;
			return UserIndexedMesh.Create<VertexPositionColor>(array, {12425}, array2, array2.Length);
		}
	}
}
