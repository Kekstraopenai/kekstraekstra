using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Collections;
using TheraEngine.Graphics;
using TheraEngine.Helpers;

namespace TheraEngine.Assets.Sea
{
	// Token: 0x0200018C RID: 396
	public class OceanFoamRenderer
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x000332DF File Offset: 0x000314DF
		public int ParticlesCount
		{
			get
			{
				return this.{15718}.Size;
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000332EC File Offset: 0x000314EC
		public OceanFoamRenderer(float {15691}, float {15692})
		{
			this.{15705} = Engine.oceanFoam.Clone();
			this.{15706} = this.{15705}.CurrentTechnique.Passes[0];
			this.{15707} = this.{15705}.Parameters["TransformToView"];
			this.{15708} = this.{15705}.Parameters["wawePosition"];
			this.{15709} = this.{15705}.Parameters["height"];
			this.{15710} = this.{15705}.Parameters["SimTime"];
			this.{15711} = this.{15705}.Parameters["CommonColor"];
			this.{15712} = this.{15705}.Parameters["CameraPosition"];
			this.{15713} = this.{15705}.Parameters["FogStart"];
			this.{15714} = this.{15705}.Parameters["FogEnd"];
			this.{15715} = this.{15705}.Parameters["commonTexture"];
			this.{15716} = new SpriteBatch3D<VertexPositionGPUSimulation>(300);
			this.{15717} = new UWEPool<OceanFoamRenderer.Particle>(800);
			this.{15718} = new Tlist<OceanFoamRenderer.Particle>(this.{15717}.Capacity);
			this.FogStart = {15691};
			this.FogEnd = {15692};
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00033464 File Offset: 0x00031664
		public void AddParticle(OceanFoamParticleDescription {15693}, float {15694}, float {15695}, float {15696}, float {15697}, float {15698}, float {15699})
		{
			if (this.{15718}.Size >= 800)
			{
				return;
			}
			Vector3 position = Engine.GS.Camera.Position;
			Vector2 vector;
			vector.X = position.X;
			vector.Y = position.Z;
			Vector2 vector2;
			vector2.X = {15694};
			vector2.Y = {15695};
			float num;
			Vector2.DistanceSquared(ref vector, ref vector2, out num);
			if (num > (this.FogEnd + 1f) * (this.FogEnd + 1f))
			{
				return;
			}
			OceanFoamRenderer.Particle particle = this.{15717}.Pop();
			particle.Initialize({15693}, {15694}, {15695}, {15696}, {15697}, {15698}, {15699});
			this.{15718}.Add(particle);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00033514 File Offset: 0x00031714
		public void Render(Vector4 {15700}, Vector2 {15701}, float {15702}, Texture2D {15703})
		{
			if (this.{15718}.Size == 0)
			{
				return;
			}
			this.{15707}.SetValue(Engine.GS.Camera.ViewMultiplyProjection);
			this.{15712}.SetValue(Engine.GS.Camera.Position);
			this.{15713}.SetValue(this.FogStart);
			this.{15714}.SetValue(this.FogEnd);
			this.{15715}.SetValue({15703});
			this.{15711}.SetValue({15700});
			this.{15709}.SetValue({15702});
			this.{15708}.SetValue({15701});
			this.{15710}.SetValue((float)Engine.Game.GameTotalTimeSec);
			for (int i = 0; i < this.{15718}.Size; i++)
			{
				this.{15718}.Array[i].Present(this.{15716});
				if (this.{15716}.Count > 8000)
				{
					this.{15716}.Render(this.{15706});
					this.{15716}.Reset();
				}
			}
			if (this.{15716}.Count > 0)
			{
				this.{15716}.Render(this.{15706});
				this.{15716}.Reset();
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00033654 File Offset: 0x00031854
		public void Update(ref FrameTime {15704})
		{
			for (int i = 0; i < this.{15718}.Size; i++)
			{
				if (this.{15718}.Array[i].Update(ref {15704}))
				{
					this.{15717}.Add(this.{15718}.Array[i]);
					this.{15718}.FastRemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000336BA File Offset: 0x000318BA
		public void RemoveAllParticles()
		{
			this.{15717}.Return(this.{15718});
		}

		// Token: 0x040007B0 RID: 1968
		private Effect {15705};

		// Token: 0x040007B1 RID: 1969
		private EffectPass {15706};

		// Token: 0x040007B2 RID: 1970
		private EffectParameter {15707};

		// Token: 0x040007B3 RID: 1971
		private EffectParameter {15708};

		// Token: 0x040007B4 RID: 1972
		private EffectParameter {15709};

		// Token: 0x040007B5 RID: 1973
		private EffectParameter {15710};

		// Token: 0x040007B6 RID: 1974
		private EffectParameter {15711};

		// Token: 0x040007B7 RID: 1975
		private EffectParameter {15712};

		// Token: 0x040007B8 RID: 1976
		private EffectParameter {15713};

		// Token: 0x040007B9 RID: 1977
		private EffectParameter {15714};

		// Token: 0x040007BA RID: 1978
		private EffectParameter {15715};

		// Token: 0x040007BB RID: 1979
		private SpriteBatch3D<VertexPositionGPUSimulation> {15716};

		// Token: 0x040007BC RID: 1980
		private UWEPool<OceanFoamRenderer.Particle> {15717};

		// Token: 0x040007BD RID: 1981
		private Tlist<OceanFoamRenderer.Particle> {15718};

		// Token: 0x040007BE RID: 1982
		public float FogStart;

		// Token: 0x040007BF RID: 1983
		public float FogEnd;

		// Token: 0x040007C0 RID: 1984
		private const int maxCnt = 800;

		// Token: 0x0200018D RID: 397
		protected class Particle : IPoolObject
		{
			// Token: 0x06000A38 RID: 2616 RVA: 0x000336D0 File Offset: 0x000318D0
			public void Initialize(OceanFoamParticleDescription {15719}, float {15720}, float {15721}, float {15722}, float {15723}, float {15724}, float {15725})
			{
				this.{15729} = {15719}.meshes[Rand.RangeInt(0, {15719}.meshes.Length)];
				Matrix.CreateRotationY({15722}, out this.{15730});
				this.{15731} = {15722};
				this.{15732} = {15719}.totalLifeTime * Rand.Range(0.8f, 1.2f);
				this.{15739} = {15719}.SizeScale * Rand.Range(0.2f, 0.4f);
				this.{15733} = this.{15732};
				this.{15734} = this.{15732} * 0.9f;
				this.{15735} = this.{15732} * 0.4f;
				this.{15736}.X = {15720};
				this.{15736}.Y = {15721};
				this.{15737} = {15723};
				this.{15738} = {15724};
				this.{15740} = {15719}.directionalWave * {15725};
			}

			// Token: 0x06000A39 RID: 2617 RVA: 0x000337AC File Offset: 0x000319AC
			public bool Update(ref FrameTime {15726})
			{
				if (this.{15740} != 0f)
				{
					this.{15736} += Geometry.FastSinCos(-this.{15731}) * (this.{15740} * {15726}.secElapsed);
				}
				this.{15737} += {15726}.secElapsed * this.{15739};
				return (this.{15732} -= {15726}.msElapsed) < 0f;
			}

			// Token: 0x06000A3A RID: 2618 RVA: 0x0003382C File Offset: 0x00031A2C
			public void Present(SpriteBatch3D<VertexPositionGPUSimulation> {15727})
			{
				float num;
				if (this.{15732} > this.{15734})
				{
					num = 1f - (this.{15732} - this.{15734}) / (this.{15733} - this.{15734});
				}
				else if (this.{15732} < this.{15735})
				{
					num = this.{15732} / this.{15735};
				}
				else
				{
					num = 1f;
				}
				Vector4 color = new Vector4(num * this.{15738});
				for (int i = 0; i < this.{15729}.planeVerticesCount; i++)
				{
					this.{15729}.planeVertices[i].Color = color;
					this.{15729}.planeVertices[i].ObjectCenter.X = this.{15736}.X;
					this.{15729}.planeVertices[i].ObjectCenter.Z = this.{15736}.Y;
					Vector3 vector;
					Vector3.Multiply(ref this.{15729}.cacheLocalPositions[i], this.{15737} * 0.5f, out vector);
					Vector3.Transform(ref vector, ref this.{15730}, out this.{15729}.planeVertices[i].LocalPosition);
				}
				{15727}.Add(this.{15729}.planeVertices);
			}

			// Token: 0x06000A3B RID: 2619 RVA: 0x0000C282 File Offset: 0x0000A482
			void IPoolObject.{15728}()
			{
			}

			// Token: 0x040007C1 RID: 1985
			private OceanFoamParticleDescription.FoamMeshDescrtiption {15729};

			// Token: 0x040007C2 RID: 1986
			private Matrix {15730};

			// Token: 0x040007C3 RID: 1987
			private float {15731};

			// Token: 0x040007C4 RID: 1988
			private float {15732};

			// Token: 0x040007C5 RID: 1989
			private float {15733};

			// Token: 0x040007C6 RID: 1990
			private float {15734};

			// Token: 0x040007C7 RID: 1991
			private float {15735};

			// Token: 0x040007C8 RID: 1992
			private Vector2 {15736};

			// Token: 0x040007C9 RID: 1993
			private float {15737};

			// Token: 0x040007CA RID: 1994
			private float {15738};

			// Token: 0x040007CB RID: 1995
			private float {15739};

			// Token: 0x040007CC RID: 1996
			private float {15740};
		}
	}
}
