using System;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;
using TheraEngine.Components.Architecture;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x02000050 RID: 80
	public class ParticlesLayerRender : IUpdateableObject
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000C86A File Offset: 0x0000AA6A
		public int Count
		{
			get
			{
				return this.{12106}.Size;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000C877 File Offset: 0x0000AA77
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000C87F File Offset: 0x0000AA7F
		public int MaxCount
		{
			get
			{
				return this.{12105};
			}
			set
			{
				if (value > 32767 || value < 1)
				{
					throw new ArgumentException();
				}
				this.{12105} = value;
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000C89A File Offset: 0x0000AA9A
		public ParticlesLayerRender(int {12095})
		{
			this.MaxCount = {12095};
			this.{12106} = new Tlist<Particle2D>(this.{12105});
			this.{12107} = new UWEPool<Particle2D>(this.{12105});
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000C8CC File Offset: 0x0000AACC
		public void Update(ref FrameTime {12096})
		{
			for (int i = 0; i < this.{12106}.Size; i++)
			{
				bool flag;
				this.{12106}.Array[i].Update(ref {12096}, out flag);
				if (flag)
				{
					this.{12107}.Add(this.{12106}.Array[i]);
					this.{12106}.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000C938 File Offset: 0x0000AB38
		internal void BoundCheck(Marker {12097})
		{
			Vector2 vector = {12097}.XY + {12097}.WH;
			for (int i = 0; i < this.{12106}.Size; i++)
			{
				Particle2D particle2D = this.{12106}.Array[i];
				if (particle2D.firstCentrPos.X + 0.5f * particle2D.firstSize * (float)particle2D.texturePath.Width < {12097}.XY.X || particle2D.firstCentrPos.Y + 0.5f * particle2D.firstSize * (float)particle2D.texturePath.Height < {12097}.XY.Y || particle2D.firstCentrPos.X - 0.5f * particle2D.firstSize * (float)particle2D.texturePath.Width > vector.X || particle2D.firstCentrPos.Y - 0.5f * particle2D.firstSize * (float)particle2D.texturePath.Height > vector.Y)
				{
					this.{12107}.Add(particle2D);
					this.{12106}.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000CA60 File Offset: 0x0000AC60
		internal void Transform(Vector2 {12098}, Vector2 {12099})
		{
			for (int i = 0; i < this.{12106}.Size; i++)
			{
				Particle2D particle2D = this.{12106}.Array[i];
				particle2D.firstCentrPos.X = particle2D.firstCentrPos.X * {12099}.X + {12098}.X * particle2D.firstSize;
				particle2D.firstCentrPos.Y = particle2D.firstCentrPos.Y * {12099}.Y + {12098}.Y * particle2D.firstSize;
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000CAE8 File Offset: 0x0000ACE8
		public void Render(float {12100})
		{
			for (int i = 0; i < this.{12106}.Size; i++)
			{
				this.{12106}.Array[i].Render({12100});
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000CB20 File Offset: 0x0000AD20
		public void Add(ParticlePattern2D {12101}, ref Vector2 {12102}, ref Vector2 {12103}, ref Color {12104})
		{
			if (this.{12106}.Size > this.{12105})
			{
				return;
			}
			Particle2D particle2D = this.{12107}.Pop();
			if (particle2D == null)
			{
				particle2D = new Particle2D();
			}
			particle2D.Initialize(ref {12101}, ref {12102}, ref {12103}, ref {12104});
			this.{12106}.Add(particle2D);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000CB6F File Offset: 0x0000AD6F
		public void RemoveAll()
		{
			this.{12107}.Return(this.{12106});
		}

		// Token: 0x04000181 RID: 385
		private int {12105};

		// Token: 0x04000182 RID: 386
		private Tlist<Particle2D> {12106};

		// Token: 0x04000183 RID: 387
		private UWEPool<Particle2D> {12107};
	}
}
