using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Interface.Animation
{
	// Token: 0x020000E0 RID: 224
	public sealed class UiMarkerAndOpacityAnimation : UiAction
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x0001F29C File Offset: 0x0001D49C
		public UiMarkerAndOpacityAnimation(UiControl {14249}, float {14250}, float {14251}, Marker {14252}, Marker {14253}, float {14254}, UiAmimationCurve {14255} = UiAmimationCurve.Linear)
		{
			this.{14262} = 1000f / {14254};
			this.{14263} = {14254};
			this.{14258} = {14250};
			this.{14259} = {14251};
			this.{14260} = {14252};
			this.{14261} = {14253};
			if ({14249}.AnimationsCount == 0)
			{
				{14249}.Opacity = {14250};
				{14249}.Pos = {14252};
			}
			{14249}.AddAction(this);
			this.{14265} = {14255};
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001F310 File Offset: 0x0001D510
		protected internal override bool Update(ref FrameTime {14256}, UiControl {14257})
		{
			this.{14263} -= {14256}.msElapsed;
			if (this.{14263} < 1f)
			{
				{14257}.Opacity = this.{14259};
				{14257}.Pos = this.{14261};
				return true;
			}
			this.{14264} += this.{14262} * {14256}.secElapsed;
			float num = (this.{14265} == UiAmimationCurve.SquaredToEnd) ? (this.{14264} * this.{14264}) : ((this.{14265} == UiAmimationCurve.SquaredToBegin) ? MathF.Pow(this.{14264}, 0.3f) : this.{14264});
			num = MathHelper.Clamp(num, 0f, 1f);
			{14257}.Opacity = this.{14258} + (this.{14259} - this.{14258}) * num;
			Vector2 vector = this.{14260}.XY + (this.{14261}.XY - this.{14260}.XY) * num;
			Vector2 vector2 = this.{14260}.WH + (this.{14261}.WH - this.{14260}.WH) * num;
			{14257}.Pos = new Marker(ref vector, ref vector2);
			if (this.IntegerTransforms)
			{
				{14257}.Pos = new Marker((float)((int){14257}.Pos.XY.X), (float)((int){14257}.Pos.XY.Y), (float)((int){14257}.Pos.WH.X), (float)((int){14257}.Pos.WH.Y));
			}
			return false;
		}

		// Token: 0x0400047B RID: 1147
		private float {14258};

		// Token: 0x0400047C RID: 1148
		private float {14259};

		// Token: 0x0400047D RID: 1149
		private Marker {14260};

		// Token: 0x0400047E RID: 1150
		private Marker {14261};

		// Token: 0x0400047F RID: 1151
		private float {14262};

		// Token: 0x04000480 RID: 1152
		private float {14263};

		// Token: 0x04000481 RID: 1153
		private float {14264};

		// Token: 0x04000482 RID: 1154
		private UiAmimationCurve {14265};

		// Token: 0x04000483 RID: 1155
		public bool IntegerTransforms = true;
	}
}
