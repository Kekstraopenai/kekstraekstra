using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Collections;

namespace TheraEngine.Scene.Lighting
{
	// Token: 0x02000060 RID: 96
	public class PointLightArrayHolder
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0000EE7F File Offset: 0x0000D07F
		public PointLightArrayHolder(int {12310}, float {12311})
		{
			this.{12322} = {12310};
			this.{12323} = {12311};
			this.{12319} = new Tlist<PointLight>({12310} * 2);
			this.{12320} = new Tlist<PointLight>({12310} * 2);
			this.CommonIntensivity = 1f;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000EEBC File Offset: 0x0000D0BC
		public void Add(PointLight {12312})
		{
			this.{12319}.Add({12312});
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000EECB File Offset: 0x0000D0CB
		public void Remove(PointLight {12313})
		{
			this.{12319}.FastRemove({12313});
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000EEDC File Offset: 0x0000D0DC
		public void SetForRenderTheObject(EffectParameter {12314}, EffectParameter {12315}, ModelTransformedScene {12316}, ref Matrix {12317})
		{
			if (this.{12319}.Size == 0 || this.CommonIntensivity == 0f)
			{
				{12315}.SetValue(0);
				return;
			}
			BoundingSphere boundingSphere;
			{12316}.CombinedModelSpaceBS.Transform(ref {12317}, out boundingSphere);
			this.{12320}.Size = 0;
			for (int i = 0; i < this.{12319}.Size; i++)
			{
				PointLight pointLight = this.{12319}.Array[i];
				float num;
				Vector3.DistanceSquared(ref pointLight.Position, ref boundingSphere.Center, out num);
				float num2 = this.{12323} + boundingSphere.Radius + pointLight.Radius;
				if (num < num2 * num2)
				{
					pointLight.CachedDistance = num;
					this.{12320}.Add(pointLight);
				}
			}
			this.{12320}.SortTop((PointLight {12324}) => -{12324}.CachedDistance);
			int num3 = Math.Min(this.{12320}.Size, this.{12322});
			{12315}.SetValue(num3);
			for (int j = 0; j < num3; j++)
			{
				this.{12320}.Array[j].SetFor({12314}.Elements[j], this.CommonIntensivity);
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000F018 File Offset: 0x0000D218
		public void DrawFlares(LightFlareRender {12318})
		{
			for (int i = 0; i < this.{12319}.Size; i++)
			{
				PointLight pointLight = this.{12319}.Array[i];
				if (pointLight.DrawFlares != PointLightFlaresMode.Disable)
				{
					{12318}.RenderPointlight(pointLight);
				}
			}
		}

		// Token: 0x04000207 RID: 519
		private readonly Tlist<PointLight> {12319};

		// Token: 0x04000208 RID: 520
		private readonly Tlist<PointLight> {12320};

		// Token: 0x04000209 RID: 521
		private Tlist<PointLight> {12321};

		// Token: 0x0400020A RID: 522
		private readonly int {12322};

		// Token: 0x0400020B RID: 523
		private float {12323};

		// Token: 0x0400020C RID: 524
		public float CommonIntensivity;
	}
}
