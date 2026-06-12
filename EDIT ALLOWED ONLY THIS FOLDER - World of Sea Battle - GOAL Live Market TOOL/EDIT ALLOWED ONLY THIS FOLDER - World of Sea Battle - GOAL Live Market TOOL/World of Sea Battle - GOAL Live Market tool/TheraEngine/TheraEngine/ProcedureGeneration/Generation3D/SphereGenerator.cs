using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Graphics;

namespace TheraEngine.ProcedureGeneration.Generation3D
{
	// Token: 0x0200006F RID: 111
	public class SphereGenerator
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x000104AC File Offset: 0x0000E6AC
		public static UserIndexedMesh Begin_VertexPositionColor(int {12460}, float {12461}, Color {12462})
		{
			int num = {12460} * {12460};
			VertexPositionColor[] array = new VertexPositionColor[num];
			new Vector3(0f, 0f, 0f);
			Vector3 position = new Vector3(Math.Abs({12461}), 0f, 0f);
			for (int i = 0; i < {12460}; i++)
			{
				float num2 = 6.2831855f / (float){12460};
				for (int j = 0; j < {12460}; j++)
				{
					float num3 = 6.2831855f / (float){12460};
					Matrix matrix = Matrix.CreateRotationZ((float)j * num3);
					Matrix matrix2 = Matrix.CreateRotationY((float)i * num2);
					Vector3 position2 = Vector3.Transform(Vector3.Transform(position, matrix), matrix2);
					array[i + j * {12460}] = new VertexPositionColor(position2, {12462});
				}
			}
			int num4 = num * 6;
			short[] array2 = new short[num4];
			int num5 = 0;
			for (int k = 0; k < {12460}; k++)
			{
				for (int l = 0; l < {12460}; l++)
				{
					object obj = (k == {12460} - 1) ? 0 : (k + 1);
					int num6 = (l == {12460} - 1) ? 0 : (l + 1);
					short num7 = (short)(k * {12460} + l);
					object obj2 = obj;
					short num8 = obj2 * {12460} + l;
					short num9 = (short)(k * {12460} + num6);
					short num10 = obj2 * {12460} + num6;
					array2[num5++] = num7;
					array2[num5++] = num8;
					array2[num5++] = num9;
					array2[num5++] = num9;
					array2[num5++] = num8;
					array2[num5++] = num10;
				}
			}
			return UserIndexedMesh.Create<VertexPositionColor>(array, num, array2, num4);
		}
	}
}
