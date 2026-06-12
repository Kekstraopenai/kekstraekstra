using System;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;
using TheraEngine.Components.Architecture;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x0200004E RID: 78
	public class ParticleManager2D : IUpdateableObject
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000C69F File Offset: 0x0000A89F
		public int Count
		{
			get
			{
				return this.{12091}.Size;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000C6AC File Offset: 0x0000A8AC
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		public int MaxCount
		{
			get
			{
				return this.{12089};
			}
			set
			{
				if (value > 32767 || value < 1)
				{
					throw new ArgumentException();
				}
				this.{12089} = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000C6CF File Offset: 0x0000A8CF
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000C6D7 File Offset: 0x0000A8D7
		public int MaxSquereCount
		{
			get
			{
				return this.{12090};
			}
			set
			{
				if (value > 32767 || value < 1)
				{
					throw new ArgumentException();
				}
				this.{12090} = value;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000C6F4 File Offset: 0x0000A8F4
		public ParticleManager2D(int {12078}, int {12079})
		{
			this.MaxCount = {12078};
			this.MaxSquereCount = {12079};
			this.{12091} = new Tlist<Particle2D>(this.{12089});
			this.{12092} = new UWEPool<Particle2D>(this.{12089});
			this.particleSystems = new UpdateableObjCollection();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000C744 File Offset: 0x0000A944
		public void Update(ref FrameTime {12080})
		{
			for (int i = 0; i < this.{12091}.Size; i++)
			{
				bool flag;
				this.{12091}.Array[i].Update(ref {12080}, out flag);
				if (flag)
				{
					this.{12092}.Add(this.{12091}.Array[i]);
					this.{12091}.RemoveAt(i);
					i--;
				}
			}
			this.particleSystems.Update(ref {12080});
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000C7BC File Offset: 0x0000A9BC
		public void Render()
		{
			for (int i = 0; i < this.{12091}.Size; i++)
			{
				this.{12091}.Array[i].Render(1f);
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000C7F6 File Offset: 0x0000A9F6
		public void Add(ParticlePattern2D {12081}, Vector2 {12082}, Vector2 {12083}, ref Color {12084})
		{
			this.Add({12081}, ref {12082}, ref {12083}, ref {12084});
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000C808 File Offset: 0x0000AA08
		public void Add(ParticlePattern2D {12085}, ref Vector2 {12086}, ref Vector2 {12087}, ref Color {12088})
		{
			if (this.{12091}.Size > this.{12089})
			{
				return;
			}
			Particle2D particle2D = this.{12092}.Pop();
			if (particle2D == null)
			{
				particle2D = new Particle2D();
			}
			particle2D.Initialize(ref {12085}, ref {12086}, ref {12087}, ref {12088});
			this.{12091}.Add(particle2D);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000C857 File Offset: 0x0000AA57
		public void RemoveAll()
		{
			this.{12092}.Return(this.{12091});
		}

		// Token: 0x04000170 RID: 368
		private int {12089};

		// Token: 0x04000171 RID: 369
		private int {12090};

		// Token: 0x04000172 RID: 370
		private Tlist<Particle2D> {12091};

		// Token: 0x04000173 RID: 371
		private UWEPool<Particle2D> {12092};

		// Token: 0x04000174 RID: 372
		internal UpdateableObjCollection particleSystems;
	}
}
