using System;
using Microsoft.Xna.Framework;
using TheraEngine.Helpers;

namespace TheraEngine.Interface.Animation
{
	// Token: 0x020000E1 RID: 225
	public sealed class UiMarkerAnimation : UiAction
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x0001F4A7 File Offset: 0x0001D6A7
		public UiMarkerAnimation(UiControl {14273}, Marker {14274}, float {14275}) : this({14273}, {14273}.Pos, {14274}, {14275})
		{
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001F4B8 File Offset: 0x0001D6B8
		public UiMarkerAnimation(UiControl {14276}, Marker {14277}, Marker {14278}, float {14279})
		{
			this.{14284} = 1000f / {14279};
			this.{14285} = {14279};
			this.{14282} = {14277};
			this.{14283} = {14278};
			{14276}.AddAction(this);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001F4EC File Offset: 0x0001D6EC
		protected internal override bool Update(ref FrameTime {14280}, UiControl {14281})
		{
			this.{14285} -= {14280}.msElapsed;
			if (this.{14285} < 1f)
			{
				{14281}.Pos = this.{14283};
				return true;
			}
			this.{14286} += this.{14284} * {14280}.secElapsed;
			float scaleFactor = Geometry.Smoothstep(MathHelper.Clamp(this.{14286}, 0f, 1f));
			Vector2 vector = this.{14282}.XY + (this.{14283}.XY - this.{14282}.XY) * scaleFactor;
			Vector2 vector2 = this.{14282}.WH + (this.{14283}.WH - this.{14282}.WH) * scaleFactor;
			{14281}.Pos = new Marker(ref vector, ref vector2);
			return false;
		}

		// Token: 0x04000484 RID: 1156
		private Marker {14282};

		// Token: 0x04000485 RID: 1157
		private Marker {14283};

		// Token: 0x04000486 RID: 1158
		private float {14284};

		// Token: 0x04000487 RID: 1159
		private float {14285};

		// Token: 0x04000488 RID: 1160
		private float {14286};
	}
}
