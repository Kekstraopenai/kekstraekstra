using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TheraEngine
{
	// Token: 0x0200001A RID: 26
	public class Curve
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003976 File Offset: 0x00001B76
		public Curve()
		{
			this.{11397} = 0f;
			this.{11399} = 0;
			this.{11398} = new List<CurvePoint>();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000399B File Offset: 0x00001B9B
		public Curve(CurvePoint {11394}) : this()
		{
			this.AddPoint({11394});
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000039AC File Offset: 0x00001BAC
		public void AddPoint(CurvePoint {11395})
		{
			if ({11395}.Position > this.{11397})
			{
				this.{11397} = {11395}.Position;
			}
			this.{11398}.Add({11395});
			this.{11398}.Sort(delegate(CurvePoint {11400}, CurvePoint {11401})
			{
				if ({11400}.Position > {11401}.Position)
				{
					return 2;
				}
				return 1;
			});
			this.{11399}++;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003A18 File Offset: 0x00001C18
		public float GetValue(float {11396})
		{
			if (this.{11399} == 0)
			{
				return 0f;
			}
			if (this.{11399} == 1)
			{
				return this.{11398}[0].Value;
			}
			float num = MathHelper.Clamp({11396}, 0.01f, this.{11397} - 0.01f) / this.{11397};
			CurvePoint curvePoint = default(CurvePoint);
			CurvePoint curvePoint2 = default(CurvePoint);
			for (int i = 0; i < this.{11399} - 1; i++)
			{
				curvePoint = this.{11398}[i];
				curvePoint2 = this.{11398}[i + 1];
				if ({11396} > curvePoint.Position && {11396} <= curvePoint2.Position)
				{
					break;
				}
			}
			float num2 = ({11396} - curvePoint.Position) / (curvePoint2.Position - curvePoint.Position);
			return curvePoint.Value * (num2 - 1f) + curvePoint2.Value * num2;
		}

		// Token: 0x04000085 RID: 133
		private float {11397};

		// Token: 0x04000086 RID: 134
		private List<CurvePoint> {11398};

		// Token: 0x04000087 RID: 135
		private int {11399};
	}
}
