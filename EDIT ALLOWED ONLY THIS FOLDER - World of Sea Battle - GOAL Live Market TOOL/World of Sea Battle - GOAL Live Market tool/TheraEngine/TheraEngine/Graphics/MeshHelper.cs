using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Graphics
{
	// Token: 0x02000145 RID: 325
	public static class MeshHelper
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x0002CC1C File Offset: 0x0002AE1C
		public static short[] CreateTriangleList(int {15038})
		{
			short[] array = new short[({15038} - 1) * ({15038} - 1) * 6];
			int num = array.Length / 3;
			int num2 = 0;
			for (int i = 0; i < {15038} - 1; i++)
			{
				for (int j = 0; j < {15038} - 1; j++)
				{
					int num3 = i * {15038} + j;
					int num4 = num3 + 1;
					int num5 = num3 + {15038};
					int num6 = num5 + 1;
					array[num2++] = (short)num3;
					array[num2++] = (short)num5;
					array[num2++] = (short)num6;
					array[num2++] = (short)num3;
					array[num2++] = (short)num6;
					array[num2++] = (short)num4;
				}
			}
			return array;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0002CCB4 File Offset: 0x0002AEB4
		public static Vector4 TexturePathToUVSpace(Rectangle {15039}, Vector2 {15040})
		{
			return new Vector4((float){15039}.X / {15040}.X, (float){15039}.Y / {15040}.Y, (float)({15039}.X + {15039}.Width) / {15040}.X, (float)({15039}.Y + {15039}.Height) / {15040}.Y);
		}
	}
}
