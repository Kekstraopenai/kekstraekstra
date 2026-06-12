using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Input
{
	// Token: 0x020000E7 RID: 231
	public class MouseInput
	{
		// Token: 0x0600062E RID: 1582 RVA: 0x0001FDE7 File Offset: 0x0001DFE7
		internal void SetData(MouseInput {14349})
		{
			this.RightPressed = {14349}.RightPressed;
			this.LeftPressed = {14349}.LeftPressed;
			this.ScrollValue = {14349}.ScrollValue;
			this.Position = {14349}.Position;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001FE19 File Offset: 0x0001E019
		internal void SetData(bool {14350}, bool {14351}, int {14352}, Vector2 {14353})
		{
			this.RightPressed = {14350};
			this.LeftPressed = {14351};
			this.ScrollValue = {14352};
			this.Position = {14353};
		}

		// Token: 0x040004AB RID: 1195
		public bool RightPressed;

		// Token: 0x040004AC RID: 1196
		public bool LeftPressed;

		// Token: 0x040004AD RID: 1197
		public int ScrollValue;

		// Token: 0x040004AE RID: 1198
		public Vector2 Position;
	}
}
