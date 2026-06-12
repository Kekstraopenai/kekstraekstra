using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Components.Scene
{
	// Token: 0x02000105 RID: 261
	public class QuadColorValueStyle
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x00025F56 File Offset: 0x00024156
		public QuadColorValueStyle(Vector3 {14837}, Vector3 {14838}, Vector3 {14839}, Vector3 {14840})
		{
			this.NormalDay = {14837};
			this.NormalNight = {14838};
			this.CloudyDay = {14839};
			this.CloudyNight = {14840};
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00025F7C File Offset: 0x0002417C
		public void GetColor(ref float {14841}, ref float {14842}, out Vector3 {14843})
		{
			Vector3 vector;
			Vector3.Lerp(ref this.NormalNight, ref this.NormalDay, {14841}, out vector);
			Vector3 vector2;
			Vector3.Lerp(ref this.CloudyNight, ref this.CloudyDay, {14841}, out vector2);
			Vector3.Lerp(ref vector, ref vector2, {14842}, out {14843});
		}

		// Token: 0x04000544 RID: 1348
		public Vector3 NormalDay;

		// Token: 0x04000545 RID: 1349
		public Vector3 NormalNight;

		// Token: 0x04000546 RID: 1350
		public Vector3 CloudyDay;

		// Token: 0x04000547 RID: 1351
		public Vector3 CloudyNight;
	}
}
