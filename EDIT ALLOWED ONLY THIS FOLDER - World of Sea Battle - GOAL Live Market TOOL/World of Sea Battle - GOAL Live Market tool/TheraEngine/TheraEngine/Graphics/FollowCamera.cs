using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Graphics
{
	// Token: 0x02000141 RID: 321
	public class FollowCamera : Camera
	{
		// Token: 0x06000910 RID: 2320 RVA: 0x0002C56B File Offset: 0x0002A76B
		public FollowCamera(float {15010}, float {15011}, float {15012}) : base({15010}, {15011}, {15012})
		{
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0000C282 File Offset: 0x0000A482
		public override void Update(ref FrameTime {15013})
		{
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0002C580 File Offset: 0x0002A780
		protected virtual void BuildPropertiesFollowCamera(CameraBuildOptions {15014})
		{
			Vector3 value = Vector3.Transform(this.cameraTarget, base.CreateRotationMatrix());
			Vector3 vector = this.TargetObjectPosition + this.OffsetOfTarget;
			Vector3 vector2 = vector - value * Math.Max(this.Zoom, 1f);
			vector2 += this.SpecialOffset;
			this.Position = vector2;
			Matrix {14970};
			if ({15014} == CameraBuildOptions.ReflectionInZero)
			{
				{14970} = Matrix.CreateLookAt(vector2 * new Vector3(1f, -1f, 1f), vector * new Vector3(1f, -1f, 1f), Vector3.Down) * Matrix.CreateRotationZ(-this.rotates.Z);
			}
			else
			{
				{14970} = Matrix.CreateLookAt(vector2, vector, Vector3.Up) * Matrix.CreateRotationZ(this.rotates.Z);
			}
			this.BuildViewMatrix({14970});
			this.direction = (vector - vector2).Normal();
		}

		// Token: 0x0400061C RID: 1564
		public Vector3 TargetObjectPosition;

		// Token: 0x0400061D RID: 1565
		public Vector3 OffsetOfTarget;

		// Token: 0x0400061E RID: 1566
		public Vector3 SpecialOffset;

		// Token: 0x0400061F RID: 1567
		public float Zoom;

		// Token: 0x04000620 RID: 1568
		public bool LimitizeMaxZoom = true;
	}
}
