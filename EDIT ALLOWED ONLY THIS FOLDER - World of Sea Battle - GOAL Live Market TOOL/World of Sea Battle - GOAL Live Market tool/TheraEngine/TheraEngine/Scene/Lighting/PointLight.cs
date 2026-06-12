using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Scene.Lighting
{
	// Token: 0x0200005F RID: 95
	public class PointLight
	{
		// Token: 0x06000276 RID: 630 RVA: 0x0000EDB8 File Offset: 0x0000CFB8
		public PointLight(Vector3 {12302}, Vector3 {12303}, float {12304}, float {12305})
		{
			this.Position = {12302};
			this.Color = {12303};
			this.Intensivity = {12304};
			this.Radius = {12305};
			this.DrawFlares = PointLightFlaresMode.Default;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000EE05 File Offset: 0x0000D005
		public PointLight()
		{
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000EE24 File Offset: 0x0000D024
		public void SetFor(EffectParameter {12306}, float {12307} = 1f)
		{
			{12306}.StructureMembers["PositionAndRadius"].SetValue(new Vector4(this.Position, this.Radius));
			{12306}.StructureMembers["ColorMulIntensivity"].SetValue(this.Color * (this.Intensivity * {12307}));
		}

		// Token: 0x040001FE RID: 510
		public Vector3 Position;

		// Token: 0x040001FF RID: 511
		public Vector3 Color;

		// Token: 0x04000200 RID: 512
		public float Intensivity;

		// Token: 0x04000201 RID: 513
		public float Radius;

		// Token: 0x04000202 RID: 514
		public PointLightFlaresMode DrawFlares;

		// Token: 0x04000203 RID: 515
		public float OlccusionaryFlaresOpacity = 1f;

		// Token: 0x04000204 RID: 516
		public float CentralFlareScale = 1f;

		// Token: 0x04000205 RID: 517
		public object Tag;

		// Token: 0x04000206 RID: 518
		internal float CachedDistance;
	}
}
