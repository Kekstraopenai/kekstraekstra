using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x02000185 RID: 389
	public class TrailShader
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x00032BDC File Offset: 0x00030DDC
		public TrailShader()
		{
			this.{15642} = Engine.trailShaderEffect.Clone();
			this.{15643} = this.{15642}.CurrentTechnique.Passes[0];
			this.{15644} = this.{15642}.Parameters["materialTexture"];
			this.{15645} = this.{15642}.Parameters["ViewProj"];
			this.{15646} = this.{15642}.Parameters["CameraPosition"];
			this.{15647} = this.{15642}.Parameters["FogStart"];
			this.{15648} = this.{15642}.Parameters["FogEnd"];
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00032CA2 File Offset: 0x00030EA2
		public void SetTexture(Texture2D {15637})
		{
			if ({15637} == null)
			{
				throw new ArgumentNullException("smokeEffectTexture");
			}
			this.{15644}.SetValue({15637});
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00032CBE File Offset: 0x00030EBE
		public void BeginEffect(Matrix {15638}, Vector3 {15639}, float {15640}, float {15641})
		{
			this.{15645}.SetValue({15638});
			this.{15646}.SetValue({15639});
			this.{15647}.SetValue({15640});
			this.{15648}.SetValue({15641});
			this.{15643}.Apply();
		}

		// Token: 0x04000789 RID: 1929
		private Effect {15642};

		// Token: 0x0400078A RID: 1930
		private EffectPass {15643};

		// Token: 0x0400078B RID: 1931
		private EffectParameter {15644};

		// Token: 0x0400078C RID: 1932
		private EffectParameter {15645};

		// Token: 0x0400078D RID: 1933
		private EffectParameter {15646};

		// Token: 0x0400078E RID: 1934
		private EffectParameter {15647};

		// Token: 0x0400078F RID: 1935
		private EffectParameter {15648};

		// Token: 0x02000186 RID: 390
		public struct Vertex : IVertexType
		{
			// Token: 0x170001BA RID: 442
			// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00032CFC File Offset: 0x00030EFC
			VertexDeclaration IVertexType.VertexDeclaration
			{
				get
				{
					return TrailShader.Vertex.vertexDeclaration;
				}
			}

			// Token: 0x04000790 RID: 1936
			public Vector3 Position;

			// Token: 0x04000791 RID: 1937
			public Vector4 BallStartPosition;

			// Token: 0x04000792 RID: 1938
			public Vector3 BallCurrentPosition;

			// Token: 0x04000793 RID: 1939
			public float FarDistance;

			// Token: 0x04000794 RID: 1940
			public float CurveMultiplier;

			// Token: 0x04000795 RID: 1941
			public Vector4 EndAnimationFactor;

			// Token: 0x04000796 RID: 1942
			public float BallColorEffect;

			// Token: 0x04000797 RID: 1943
			private static readonly VertexDeclaration vertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
				new VertexElement(12, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 0),
				new VertexElement(28, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 1),
				new VertexElement(40, VertexElementFormat.Single, VertexElementUsage.TextureCoordinate, 2),
				new VertexElement(44, VertexElementFormat.Single, VertexElementUsage.TextureCoordinate, 3),
				new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 4),
				new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.TextureCoordinate, 5)
			});
		}
	}
}
