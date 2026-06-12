using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Graphics
{
	// Token: 0x0200013E RID: 318
	public class CameraPositionInfo
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x0002C2AC File Offset: 0x0002A4AC
		public CameraPositionInfo(Matrix {14993}, Matrix {14994}, Vector3 {14995}, float {14996})
		{
			this.ViewMatrix = {14993};
			this.ProjMatrix = {14994};
			this.ViewMultiplyProjMatrix = {14993} * {14994};
			this.CameraPosition = {14995};
			this.CameraFarPlane = {14996};
			this.ViewDirection = new Vector3(-{14993}.M13, -{14993}.M23, -{14993}.M33);
			this.frustum = new BoundingFrustum(this.ViewMultiplyProjMatrix);
		}

		// Token: 0x0400060C RID: 1548
		public readonly Matrix ViewMatrix;

		// Token: 0x0400060D RID: 1549
		public readonly Matrix ProjMatrix;

		// Token: 0x0400060E RID: 1550
		public readonly Matrix ViewMultiplyProjMatrix;

		// Token: 0x0400060F RID: 1551
		public readonly Vector3 CameraPosition;

		// Token: 0x04000610 RID: 1552
		public readonly float CameraFarPlane;

		// Token: 0x04000611 RID: 1553
		public readonly Vector3 ViewDirection;

		// Token: 0x04000612 RID: 1554
		public readonly BoundingFrustum frustum;
	}
}
