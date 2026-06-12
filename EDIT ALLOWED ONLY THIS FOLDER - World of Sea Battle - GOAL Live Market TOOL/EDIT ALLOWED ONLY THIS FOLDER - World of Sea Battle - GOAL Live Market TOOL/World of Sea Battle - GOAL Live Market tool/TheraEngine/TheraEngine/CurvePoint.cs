using System;

namespace TheraEngine
{
	// Token: 0x02000019 RID: 25
	public struct CurvePoint
	{
		// Token: 0x06000085 RID: 133 RVA: 0x0000390D File Offset: 0x00001B0D
		public CurvePoint(float {11390}, float {11391})
		{
			this.Position = {11390};
			this.Value = {11391};
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003920 File Offset: 0x00001B20
		public override bool Equals(object {11392})
		{
			if ({11392} == null)
			{
				return false;
			}
			if ({11392}.GetType() != base.GetType())
			{
				return false;
			}
			CurvePoint curvePoint = (CurvePoint){11392};
			return curvePoint.Position == this.Position && curvePoint.Value == this.Value;
		}

		// Token: 0x04000083 RID: 131
		public float Position;

		// Token: 0x04000084 RID: 132
		public float Value;
	}
}
