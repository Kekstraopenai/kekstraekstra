using System;
using Microsoft.Xna.Framework;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Scene;

namespace TheraEngine.Assets.Animations
{
	// Token: 0x0200016E RID: 366
	public class TransformRigidAnimation : TransformAnimation
	{
		// Token: 0x060009C2 RID: 2498 RVA: 0x0002F845 File Offset: 0x0002DA45
		public TransformRigidAnimation(float {15279}, Transform3D {15280}, Transform3D {15281}) : this({15279}, {15280}, {15281}.Translation, {15281}.RotatesAll, {15281}.Scales)
		{
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002F864 File Offset: 0x0002DA64
		public TransformRigidAnimation(float {15282}, Transform3D {15283}, Vector3 {15284}, Vector3 {15285}, Vector3 {15286}) : base({15283})
		{
			this.{15296} = {15282};
			this.{15293} = {15284};
			this.{15294} = {15285};
			this.{15295} = {15286};
			Vector3 rotatesAll = {15283}.RotatesAll;
			Vector3 vector;
			Vector3.Subtract(ref {15284}, ref {15283}.Translation, out vector);
			Vector3.Subtract(ref {15285}, ref rotatesAll, out rotatesAll);
			Vector3 vector2;
			Vector3.Subtract(ref {15286}, ref {15283}.Scales, out vector2);
			Vector3.Multiply(ref vector, 1f / {15282}, out this.{15290});
			Vector3.Multiply(ref rotatesAll, 1f / {15282}, out this.{15291});
			Vector3.Multiply(ref vector2, 1f / {15282}, out this.{15292});
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002F904 File Offset: 0x0002DB04
		public override void Update(ref FrameTime {15287})
		{
			if (this.IsAnimationComplete)
			{
				return;
			}
			if (this.{15296} > {15287}.msElapsed)
			{
				this.{15288}({15287}.msElapsed);
				this.{15296} -= {15287}.msElapsed;
				return;
			}
			this.{15288}(this.{15296});
			this.IsAnimationComplete = true;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0002F95C File Offset: 0x0002DB5C
		private void {15288}(float {15289})
		{
			if ({15289} < 0f)
			{
				throw new Exception();
			}
			Transform3D transform = this.Transform;
			transform.Translation.X = transform.Translation.X + this.{15290}.X * {15289};
			Transform3D transform2 = this.Transform;
			transform2.Translation.Y = transform2.Translation.Y + this.{15290}.Y * {15289};
			Transform3D transform3 = this.Transform;
			transform3.Translation.Z = transform3.Translation.Z + this.{15290}.Z * {15289};
			this.Transform.Pitch += this.{15291}.X * {15289};
			this.Transform.Yaw += this.{15291}.Y * {15289};
			this.Transform.Roll += this.{15291}.Z * {15289};
			Geometry.AxisNorm(ref this.Transform.Pitch);
			Geometry.AxisNorm(ref this.Transform.Yaw);
			Geometry.AxisNorm(ref this.Transform.Roll);
			Transform3D transform4 = this.Transform;
			transform4.Scales.X = transform4.Scales.X + this.{15292}.X * {15289};
			Transform3D transform5 = this.Transform;
			transform5.Scales.Y = transform5.Scales.Y + this.{15292}.Y * {15289};
			Transform3D transform6 = this.Transform;
			transform6.Scales.Z = transform6.Scales.Z + this.{15292}.Z * {15289};
		}

		// Token: 0x040006B2 RID: 1714
		private Vector3 {15290};

		// Token: 0x040006B3 RID: 1715
		private Vector3 {15291};

		// Token: 0x040006B4 RID: 1716
		private Vector3 {15292};

		// Token: 0x040006B5 RID: 1717
		private Vector3 {15293};

		// Token: 0x040006B6 RID: 1718
		private Vector3 {15294};

		// Token: 0x040006B7 RID: 1719
		private Vector3 {15295};

		// Token: 0x040006B8 RID: 1720
		private float {15296};
	}
}
