using System;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Core;

namespace TheraEngine.Assets.Shaders
{
	// Token: 0x02000170 RID: 368
	public abstract class Shader : DisposableObject
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x0002FB43 File Offset: 0x0002DD43
		public Effect GetEffectBase
		{
			get
			{
				return this.{15312};
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002FB4B File Offset: 0x0002DD4B
		public Shader(string {15306}, ContentManager {15307}) : this({15307}.Load<Effect>({15306}))
		{
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002FB5C File Offset: 0x0002DD5C
		public Shader(Effect {15308})
		{
			this.{15312} = {15308};
			this.{15314} = this.{15312}.CurrentTechnique.Passes.Count;
			this.{15313} = new EffectPass[this.{15314}];
			for (int i = 0; i < this.{15314}; i++)
			{
				this.{15313}[i] = this.{15312}.CurrentTechnique.Passes[i];
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002FBD1 File Offset: 0x0002DDD1
		public EffectParameter GetProperty(string {15309})
		{
			return this.{15312}.Parameters[{15309}];
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002FBE4 File Offset: 0x0002DDE4
		public EffectPass GetPass(string {15310})
		{
			for (int i = 0; i < this.{15314}; i++)
			{
				if (string.Equals(this.{15313}[i].Name, {15310}))
				{
					return this.{15313}[i];
				}
			}
			throw new InvalidOperationException("шейдер не содержит определения для " + {15310});
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0002FC30 File Offset: 0x0002DE30
		public EffectPass GetPass(int {15311})
		{
			return this.{15313}[{15311}];
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0002FC3A File Offset: 0x0002DE3A
		public override void Dispose()
		{
			this.{15312}.Dispose();
			this.{15312} = null;
			this.{15313} = null;
			base.Dispose();
		}

		// Token: 0x040006BB RID: 1723
		private Effect {15312};

		// Token: 0x040006BC RID: 1724
		private EffectPass[] {15313};

		// Token: 0x040006BD RID: 1725
		private int {15314};
	}
}
