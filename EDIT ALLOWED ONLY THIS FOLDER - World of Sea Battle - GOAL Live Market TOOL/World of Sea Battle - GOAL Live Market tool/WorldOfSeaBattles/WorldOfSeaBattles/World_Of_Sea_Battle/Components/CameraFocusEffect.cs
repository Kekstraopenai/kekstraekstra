using System;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x0200054A RID: 1354
	internal class CameraFocusEffect
	{
		// Token: 0x06001ED1 RID: 7889 RVA: 0x00114B14 File Offset: 0x00112D14
		public CameraFocusEffect(Func<Vector3?> {25822}, float {25823} = 0.5f, float {25824} = 0f)
		{
			this.Target = {25822};
			this.Power = {25823};
			this.LongFocusingOffset = {25824};
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00114B64 File Offset: 0x00112D64
		public Vector3? Update(ref FrameTime {25825})
		{
			if (Global.Player == null)
			{
				return null;
			}
			Func<Vector3?> target = this.Target;
			Vector3? vector = (target != null) ? target() : null;
			if (vector != null)
			{
				this.{25826} = vector.Value;
			}
			else
			{
				this.Target = null;
			}
			this.{25827}.SpeedPerSec = 0.4f;
			this.{25827}.Evalute(ref {25825}, vector != null);
			Vector3 vector2 = this.LongFocusingOffset * Global.Camera.Direction * Geometry.Saturate(1f - Global.Camera.ZoomFactor);
			vector2.Y = Math.Min(0f, vector2.Y);
			Vector3 position = Global.Camera.Position;
			if (vector == null && this.{25827}.CurrentSoftValueSmoothstep == 0f)
			{
				return null;
			}
			Vector3 value = -(position - this.{25826} + vector2) * this.Power * this.{25827}.CurrentSoftValueSmoothstep;
			Vector3 value2 = value * 0.3f + this.{25829} * 0.3f + this.{25830} * 0.2f + this.{25831} * 0.1f;
			this.{25831} = this.{25830};
			this.{25830} = this.{25829};
			this.{25829} = value;
			return new Vector3?(value2);
		}

		// Token: 0x04001E60 RID: 7776
		public Func<Vector3?> Target;

		// Token: 0x04001E61 RID: 7777
		public readonly float Power = 0.5f;

		// Token: 0x04001E62 RID: 7778
		public readonly float LongFocusingOffset;

		// Token: 0x04001E63 RID: 7779
		private Vector3 {25826};

		// Token: 0x04001E64 RID: 7780
		private SoftTrigger {25827} = new SoftTrigger(0f, 1f, 0.3f);

		// Token: 0x04001E65 RID: 7781
		private Vector3 {25828};

		// Token: 0x04001E66 RID: 7782
		private Vector3 {25829};

		// Token: 0x04001E67 RID: 7783
		private Vector3 {25830};

		// Token: 0x04001E68 RID: 7784
		private Vector3 {25831};
	}
}
