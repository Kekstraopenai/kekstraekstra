using System;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;
using TheraEngine.Components.Architecture;
using TheraEngine.Helpers;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x0200005A RID: 90
	public class ParticleSystem3D : IUpdateableObject
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000DE4C File Offset: 0x0000C04C
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000DE54 File Offset: 0x0000C054
		public float ElapsedTimePerParticleCreate
		{
			get
			{
				return this.{12245};
			}
			set
			{
				if (value <= 0f)
				{
					throw new ArgumentException();
				}
				this.{12245} = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000DE6B File Offset: 0x0000C06B
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000DE79 File Offset: 0x0000C079
		public float CountPerSecound
		{
			get
			{
				return 1000f / this.{12245};
			}
			set
			{
				if (value <= 0f)
				{
					throw new ArgumentException();
				}
				this.{12245} = 1000f / value;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000DE98 File Offset: 0x0000C098
		public ParticleSystem3D(Vector3 {12238}, float {12239}, bool {12240}, ParticleManager3D {12241}, params ParticleEffect3DTemplate[] {12242})
		{
			this.Center = {12238};
			this.Radius = {12239};
			this.ParticleEffect = new Tlist<ParticleEffect3DTemplate>({12242});
			this.EnableCurvedTrail = {12240};
			this.{12250} = this.Center;
			this.{12245} = -1f;
			this.IsEnabled = true;
			this.{12247} = {12241};
			{12241}.particleSystems.Add(this);
			this.{12249} = Rand.Range(0f, 100f);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000DF18 File Offset: 0x0000C118
		void IUpdateableObject.{12243}(ref FrameTime {12244})
		{
			if (this.{12245} == -1f)
			{
				throw new InvalidOperationException("(g) Свойство не задано");
			}
			if (this.IsEnabled)
			{
				this.{12246} -= {12244}.msElapsed;
			}
			if (this.UseSubframeAlgorithm)
			{
				Vector3 vector = this.{12250};
				Vector3 center = this.Center;
				this.{12251} += Vector3.Distance(vector, center);
				float num = 1f / this.SubframeAlgorithmParticlesPerUnit;
				int num2 = 0;
				while (this.{12251} > num)
				{
					this.{12251} -= num;
					num2++;
				}
				for (int i = 0; i < num2; i++)
				{
					Vector3 {12171} = Vector3.Lerp(center, vector, (float)i / (float)num2);
					if (this.Radius > 0f)
					{
						{12171}.X += Rand.Range(-this.Radius, this.Radius);
						{12171}.Y += Rand.Range(-this.Radius, this.Radius);
						{12171}.Z += Rand.Range(-this.Radius, this.Radius);
					}
					ParticleEffectSampleCall {12179} = new ParticleEffectSampleCall({12171}, (this.EnableCurvedTrail ? HashHelper.SphericalVectorFromLerp(this.{12249} + 0.25f * {12171}.Length()) : Vector3.Zero) + this.PermVelocity);
					{12179}.Stretch = this.PermStretch;
					for (int j = 0; j < this.ParticleEffect.Size; j++)
					{
						this.ParticleEffect.Array[j].Apply({12179}, this.{12247});
					}
				}
			}
			else
			{
				while (this.{12246} < 0f)
				{
					this.{12246} += this.{12245};
					Vector3 center2 = this.Center;
					if (this.Radius > 0f)
					{
						center2.X += Rand.Range(-this.Radius, this.Radius);
						center2.Y += Rand.Range(-this.Radius, this.Radius);
						center2.Z += Rand.Range(-this.Radius, this.Radius);
					}
					ParticleEffectSampleCall {12179}2 = new ParticleEffectSampleCall(center2, (this.EnableCurvedTrail ? HashHelper.SphericalVectorFromLerp(this.{12249} + 0.25f * center2.Length()) : Vector3.Zero) + this.PermVelocity);
					{12179}2.Stretch = this.PermStretch;
					for (int k = 0; k < this.ParticleEffect.Size; k++)
					{
						this.ParticleEffect.Array[k].Apply({12179}2, this.{12247});
					}
				}
			}
			this.{12250} = this.Center;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000E1DE File Offset: 0x0000C3DE
		public void Remove()
		{
			if (this.{12248})
			{
				throw new InvalidOperationException("Система уже удалена");
			}
			this.{12247}.particleSystems.RemoveAt(this);
			this.{12247} = null;
			this.IsEnabled = false;
			this.{12248} = true;
		}

		// Token: 0x040001D6 RID: 470
		public Vector3 Center;

		// Token: 0x040001D7 RID: 471
		public float Radius;

		// Token: 0x040001D8 RID: 472
		public Tlist<ParticleEffect3DTemplate> ParticleEffect;

		// Token: 0x040001D9 RID: 473
		public bool EnableCurvedTrail;

		// Token: 0x040001DA RID: 474
		public bool IsEnabled;

		// Token: 0x040001DB RID: 475
		public Vector3 PermVelocity;

		// Token: 0x040001DC RID: 476
		public Vector3 PermStretch;

		// Token: 0x040001DD RID: 477
		public bool UseSubframeAlgorithm;

		// Token: 0x040001DE RID: 478
		public float SubframeAlgorithmParticlesPerUnit;

		// Token: 0x040001DF RID: 479
		private float {12245};

		// Token: 0x040001E0 RID: 480
		private float {12246};

		// Token: 0x040001E1 RID: 481
		private ParticleManager3D {12247};

		// Token: 0x040001E2 RID: 482
		private bool {12248};

		// Token: 0x040001E3 RID: 483
		private float {12249};

		// Token: 0x040001E4 RID: 484
		private Vector3 {12250};

		// Token: 0x040001E5 RID: 485
		private float {12251};
	}
}
