using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x02000198 RID: 408
	public class ColorGradingHelper
	{
		// Token: 0x06000A6A RID: 2666 RVA: 0x000341CC File Offset: 0x000323CC
		public ColorGradingHelper(Texture2D {15834}, int {15835}, int {15836}, bool {15837}, Color {15838}, Color {15839})
		{
			this.CurrentLUT = new Texture3D(Engine.GS.graphicsDevice, {15835}, {15835}, {15836}, false, SurfaceFormat.Color);
			Vector3[] array = this.{15840}({15834}, {15835}, {15836});
			this.{15844} = {15838}.ToVector3();
			this.{15845} = {15839}.ToVector3();
			Color[] array2 = new Color[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new Color(array[i]);
			}
			this.CurrentLUT.SetData<Color>(array2);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00034264 File Offset: 0x00032464
		private Vector3[] {15840}(Texture2D {15841}, int {15842}, int {15843})
		{
			int num = {15841}.Width / {15842};
			new Color[0];
			Vector3[] array = new Vector3[{15842} * {15842} * {15843}];
			Color[] array2 = new Color[{15842} * {15842}];
			int num2 = 0;
			for (int i = 0; i < {15843}; i++)
			{
				{15841}.GetData<Color>(0, new Rectangle?(new Rectangle(i % num * {15842}, i / num * {15842}, {15842}, {15842})), array2, 0, array2.Length);
				for (int j = 0; j < array2.Length; j++)
				{
					array[num2++] = array2[j].ToVector3();
				}
			}
			return array;
		}

		// Token: 0x040007F2 RID: 2034
		private Vector3 {15844};

		// Token: 0x040007F3 RID: 2035
		private Vector3 {15845};

		// Token: 0x040007F4 RID: 2036
		private bool {15846};

		// Token: 0x040007F5 RID: 2037
		private Timer {15847} = new Timer(1000f);

		// Token: 0x040007F6 RID: 2038
		public readonly Texture3D CurrentLUT;
	}
}
