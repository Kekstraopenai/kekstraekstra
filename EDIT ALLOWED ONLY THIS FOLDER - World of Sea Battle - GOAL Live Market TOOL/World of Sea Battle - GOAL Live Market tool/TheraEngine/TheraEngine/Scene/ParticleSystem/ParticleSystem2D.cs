using System;
using Microsoft.Xna.Framework;
using TheraEngine.Components.Architecture;
using TheraEngine.Helpers;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x02000051 RID: 81
	public class ParticleSystem2D : DisposableObject, IUpdateableObject
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000CB82 File Offset: 0x0000AD82
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000CB8A File Offset: 0x0000AD8A
		public float ElapsedTimePerParticleCreate
		{
			get
			{
				return this.{12126};
			}
			set
			{
				if (value <= 0f)
				{
					throw new ArgumentException();
				}
				this.{12126} = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000CBA1 File Offset: 0x0000ADA1
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000CBAF File Offset: 0x0000ADAF
		public float CountPerSecound
		{
			get
			{
				return 1000f / this.{12126};
			}
			set
			{
				if (value <= 0f)
				{
					throw new ArgumentException();
				}
				this.{12126} = 1000f / value;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		public ParticleSystem2D(Vector2 {12117}, float {12118}, ParticlePattern2D {12119}, Color {12120}, Range1D {12121}, Range1D {12122}, ParticleManager2D {12123})
		{
			this.Centr = {12117};
			this.Radius = {12118};
			this.ParticlePattern = {12119};
			this.ParticlesColor = {12120};
			this.DirectionX = {12121};
			this.DirectionY = {12122};
			this.{12126} = -1f;
			this.IsEnabled = true;
			this.{12128} = {12123};
			{12123}.particleSystems.Add(this);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000CC34 File Offset: 0x0000AE34
		void IUpdateableObject.{12124}(ref FrameTime {12125})
		{
			if (this.{12126} == -1f)
			{
				throw new InvalidOperationException("(g) Свойство не задано");
			}
			if (this.IsEnabled)
			{
				this.{12127} -= {12125}.msElapsed;
			}
			while (this.{12127} < 0f)
			{
				this.{12127} += this.{12126};
				Vector2 vector = new Vector2(this.Centr.X + Rand.Range(-this.Radius, this.Radius), this.Centr.Y + Rand.Range(-this.Radius, this.Radius));
				Vector2 vector2 = new Vector2(this.DirectionX.Sample, this.DirectionY.Sample);
				if (this.OutputLayer != null)
				{
					this.OutputLayer.Add(this.ParticlePattern, ref vector, ref vector2, ref this.ParticlesColor);
				}
				else
				{
					this.{12128}.Add(this.ParticlePattern, ref vector, ref vector2, ref this.ParticlesColor);
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000CD3F File Offset: 0x0000AF3F
		public void Unbind()
		{
			this.{12128}.particleSystems.RemoveAt(this);
			this.{12129} = true;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000CD5A File Offset: 0x0000AF5A
		public void Remove()
		{
			if (this.{12129})
			{
				return;
			}
			this.{12128}.particleSystems.RemoveAt(this);
			this.{12128} = null;
			this.IsEnabled = false;
			this.{12129} = true;
		}

		// Token: 0x04000184 RID: 388
		public Vector2 Centr;

		// Token: 0x04000185 RID: 389
		public float Radius;

		// Token: 0x04000186 RID: 390
		public ParticlePattern2D ParticlePattern;

		// Token: 0x04000187 RID: 391
		public Color ParticlesColor;

		// Token: 0x04000188 RID: 392
		public Range1D DirectionX;

		// Token: 0x04000189 RID: 393
		public Range1D DirectionY;

		// Token: 0x0400018A RID: 394
		public bool IsEnabled;

		// Token: 0x0400018B RID: 395
		public ParticlesLayerRender OutputLayer;

		// Token: 0x0400018C RID: 396
		private float {12126};

		// Token: 0x0400018D RID: 397
		private float {12127};

		// Token: 0x0400018E RID: 398
		private ParticleManager2D {12128};

		// Token: 0x0400018F RID: 399
		private bool {12129};
	}
}
