using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Collections;
using TheraEngine.Components.Architecture;
using TheraEngine.Graphics;
using TheraEngine.Helpers;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x02000058 RID: 88
	public class ParticleManager3D : DisposableObject
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000D6AE File Offset: 0x0000B8AE
		public int Count
		{
			get
			{
				return this.{12210}.Size + this.{12211}.Size;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000D6C7 File Offset: 0x0000B8C7
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
		public int MaxCount
		{
			get
			{
				return this.{12214};
			}
			set
			{
				if (value > 32767 || value < 1)
				{
					throw new ArgumentException();
				}
				if (value > this.{12214} || this.{12209} == null)
				{
					this.{12209} = new UWEPool<Particle3D>(value - this.Count + 1);
				}
				this.{12214} = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000D71C File Offset: 0x0000B91C
		public float CurrentPerformanceFactor
		{
			get
			{
				return this.{12213};
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000D724 File Offset: 0x0000B924
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000D72C File Offset: 0x0000B92C
		public float ClipDistance { get; set; }

		// Token: 0x06000254 RID: 596 RVA: 0x0000D738 File Offset: 0x0000B938
		public ParticleManager3D(int {12187}, float {12188})
		{
			this.ClipDistance = {12188};
			this.{12214} = {12187};
			this.{12212} = new SpriteBatch3D<VolumtericParticleVertex>(8000);
			this.{12209} = new UWEPool<Particle3D>({12187});
			this.{12210} = new Tlist<Particle3D>({12187});
			this.{12211} = new Tlist<Particle3D>({12187});
			this.{12215} = new CountingSort<Particle3D>(0, 10002, {12187} / 2);
			this.{12217} = new CountingSort<Particle3D>(0, 10002, {12187} / 2);
			this.{12216} = new CountingSort<Particle3D>(0, 1002, {12187} / 2);
			this.particleSystems = new UpdateableObjCollection();
			this.{12213} = 1f;
			this.{12218} = new Vector3[64];
			for (int i = 0; i < 64; i++)
			{
				this.{12218}[i] = Rand.NextVector3(-1f, 1f);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000D814 File Offset: 0x0000BA14
		public void Update(Vector3 {12189}, ref FrameTime {12190})
		{
			int num = this.{12214} / 10;
			float num2 = 1f - MathHelper.Clamp((float)(this.Count - num) / (float)this.{12214}, 0f, 1f);
			this.{12213} = 0.7f * num2 + 0.3f;
			{12189} = ({12189}.XZNormal() * {12190}.secElapsed * 0.6f).X0Y();
			this.UpdateParticlesHelper(this.{12210}, ref {12190}, {12189});
			this.UpdateParticlesHelper(this.{12211}, ref {12190}, {12189});
			this.particleSystems.Update(ref {12190});
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		private void UpdateParticlesHelper(Tlist<Particle3D> {12191}, ref FrameTime {12192}, Vector3 {12193})
		{
			for (int i = 0; i < {12191}.Size; i++)
			{
				Particle3D particle3D = {12191}.Array[i];
				bool flag;
				particle3D.Update(ref {12192}, ref {12193}, out flag);
				if (flag)
				{
					{12191}.FastRemoveAt(i);
					i--;
					this.{12209}.Add(particle3D);
				}
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000D900 File Offset: 0x0000BB00
		public void Render(ParticlesAndStaticMesh {12194}, float {12195}, float {12196}, Vector2 {12197}, Action<ParticlesAndStaticMesh> {12198} = null, Vector2? {12199} = null)
		{
			{12194}.SetForRender(Matrix.Identity, Vector4.One);
			float num = this.ClipDistance * this.{12213};
			if ({12199} != null)
			{
				float {12204} = ({12199} != null) ? {12199}.GetValueOrDefault().X : 0f;
				float {12205} = ({12199} != null) ? {12199}.GetValueOrDefault().Y : float.MaxValue;
				{12194}.ManualSetFog({12195}, {12196});
				if ({12198} == null)
				{
					{12194}.BeginPass(true, true);
				}
				else
				{
					{12198}({12194});
				}
				ParticleManager3D.PrepareDraw(ref {12197}, this.{12217}, this.{12210}, num, {12204}, {12205});
				this.MakeDraw(this.{12217}.LastResult);
				return;
			}
			if (this.{12211}.Size > 0)
			{
				{12194}.ManualSetFog({12195} * 4f, {12196} * 4f);
				if ({12198} == null)
				{
					{12194}.BeginPass(true, true);
				}
				else
				{
					{12198}({12194});
				}
				ParticleManager3D.PrepareDraw(ref {12197}, this.{12216}, this.{12211}, num * 4f, 0f, float.MaxValue);
				this.MakeDraw(this.{12216}.LastResult);
			}
			if (this.{12210}.Size > 0)
			{
				{12194}.ManualSetFog({12195}, {12196});
				if ({12198} == null)
				{
					{12194}.BeginPass(true, true);
				}
				else
				{
					{12198}({12194});
				}
				ParticleManager3D.PrepareDraw(ref {12197}, this.{12215}, this.{12210}, num, 0f, float.MaxValue);
				this.MakeDraw(this.{12215}.LastResult);
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000DA80 File Offset: 0x0000BC80
		private static void PrepareDraw(ref Vector2 {12200}, CountingSort<Particle3D> {12201}, Tlist<Particle3D> {12202}, float {12203}, float {12204} = 0f, float {12205} = 3.4028235E+38f)
		{
			int maxKey = {12201}.MaxKey;
			for (int i = 0; i < {12202}.Size; i++)
			{
				Particle3D particle3D = {12202}.Array[i];
				if (particle3D.my_radius > {12204} && particle3D.my_radius < {12205})
				{
					Vector3.Distance(ref Engine.GS.Camera.Position, ref particle3D.my_position, out particle3D.SortingCachedDistance);
					if (particle3D.SortingCachedDistance < {12203})
					{
						int key = (int)((double){12201}.MaxKey * (1.0 - Math.Pow((double)(particle3D.SortingCachedDistance / {12203}), 0.9)));
						particle3D.PrepareVertices(ref {12200});
						{12201}.Append(key, particle3D);
					}
				}
			}
			{12201}.Sort();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000DB34 File Offset: 0x0000BD34
		private void MakeDraw(Tlist<Particle3D> {12206})
		{
			for (int i = 0; i < {12206}.Size; i++)
			{
				Particle3D particle3D = {12206}.Array[i];
				this.{12212}.UnsafeAddQuadBased(particle3D.parent.array);
				if (this.{12212}.Count > 8000)
				{
					this.{12212}.Render(null);
					this.{12212}.Reset();
				}
			}
			if (this.{12212}.Count > 0)
			{
				this.{12212}.Render(null);
				this.{12212}.Reset();
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000DBBF File Offset: 0x0000BDBF
		public void RenderToHeatMap()
		{
			this.MakeDraw(this.{12215}.LastResult);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
		internal void SampleInternal(ref ParticleEffectSampleCall {12207}, ref ParticleEffect3DTemplate {12208})
		{
			if (this.Count >= this.{12214})
			{
				return;
			}
			float num = this.ClipDistance * this.{12213};
			float num2;
			Vector3.Distance(ref Engine.GS.Camera.Position, ref {12207}.SamplePosition, out num2);
			if (num2 > num * 4f)
			{
				return;
			}
			if (num2 > num && {12208}.SingleInitialSize.Start < 20f)
			{
				return;
			}
			Tlist<Particle3D> tlist = (num2 > num) ? this.{12211} : this.{12210};
			int num3 = Math.Max((int)((float){12208}.GeneratorCountPerSample * this.{12213}), 1);
			for (int i = 0; i < num3; i++)
			{
				if (this.Count >= this.{12214})
				{
					return;
				}
				Vector3 vector = {12207}.SamplePosition;
				if ({12208}.GeneratorRandomOffset != 0f)
				{
					Vector3 value = vector;
					Vector3[] array = this.{12218};
					int num4 = this.{12219};
					this.{12219} = num4 + 1;
					vector = value + array[num4] * {12208}.GeneratorRandomOffset;
					if (this.{12219} == this.{12218}.Length)
					{
						this.{12219} = 0;
					}
				}
				Particle3D particle3D = this.{12209}.Pop();
				if (particle3D != null)
				{
					particle3D.SortingCachedDistance = num2;
					particle3D.InternalInit(ref {12208}, ref {12207}, ref vector);
					tlist.Add(particle3D);
				}
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000DD15 File Offset: 0x0000BF15
		public void RemoveAll()
		{
			this.particleSystems.Clear();
			this.{12209}.Return(this.{12210});
			this.{12209}.Return(this.{12211});
		}

		// Token: 0x040001C5 RID: 453
		private const int distanceKeysCount = 10000;

		// Token: 0x040001C6 RID: 454
		private UWEPool<Particle3D> {12209};

		// Token: 0x040001C7 RID: 455
		private Tlist<Particle3D> {12210};

		// Token: 0x040001C8 RID: 456
		private Tlist<Particle3D> {12211};

		// Token: 0x040001C9 RID: 457
		internal UpdateableObjCollection particleSystems;

		// Token: 0x040001CA RID: 458
		private SpriteBatch3D<VolumtericParticleVertex> {12212};

		// Token: 0x040001CB RID: 459
		private float {12213};

		// Token: 0x040001CC RID: 460
		private int {12214};

		// Token: 0x040001CD RID: 461
		private CountingSort<Particle3D> {12215};

		// Token: 0x040001CE RID: 462
		private CountingSort<Particle3D> {12216};

		// Token: 0x040001CF RID: 463
		private CountingSort<Particle3D> {12217};

		// Token: 0x040001D0 RID: 464
		private Vector3[] {12218};

		// Token: 0x040001D1 RID: 465
		private int {12219};

		// Token: 0x040001D2 RID: 466
		private static ValueGraph avgDrawTime = new ValueGraph(10);

		// Token: 0x040001D3 RID: 467
		[CompilerGenerated]
		private float {12220};
	}
}
