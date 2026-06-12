using System;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Graphics;
using TheraEngine.Graphics;

namespace TheraEngine.Assets.Sea
{
	// Token: 0x0200018A RID: 394
	public class OceanFoamParticleDescription
	{
		// Token: 0x06000A30 RID: 2608 RVA: 0x00033090 File Offset: 0x00031290
		public OceanFoamParticleDescription(TextuePathRandomSelect {15684}, Vector2 {15685}, float {15686}, float {15687}, float {15688})
		{
			Rectangle[] all = {15684}.All;
			int num = all.Length;
			this.meshes = new OceanFoamParticleDescription.FoamMeshDescrtiption[num];
			this.totalLifeTime = {15686};
			this.directionalWave = {15687};
			this.SizeScale = {15688};
			for (int i = 0; i < num; i++)
			{
				OceanFoamParticleDescription.FoamMeshDescrtiption foamMeshDescrtiption = this.meshes[i] = new OceanFoamParticleDescription.FoamMeshDescrtiption();
				Vector4 vector = MeshHelper.TexturePathToUVSpace(all[i], {15685});
				foamMeshDescrtiption.planeVertices = new VertexPositionGPUSimulation[]
				{
					new VertexPositionGPUSimulation(new Vector3(-1f, 0f, 1f), Vector3.Zero, new Vector2(vector.X, vector.Y), Vector4.One),
					new VertexPositionGPUSimulation(new Vector3(1f, 0f, 1f), Vector3.Zero, new Vector2(vector.Z, vector.Y), Vector4.One),
					new VertexPositionGPUSimulation(new Vector3(-1f, 0f, -1f), Vector3.Zero, new Vector2(vector.X, vector.W), Vector4.One),
					new VertexPositionGPUSimulation(new Vector3(-1f, 0f, -1f), Vector3.Zero, new Vector2(vector.X, vector.W), Vector4.One),
					new VertexPositionGPUSimulation(new Vector3(1f, 0f, 1f), Vector3.Zero, new Vector2(vector.Z, vector.Y), Vector4.One),
					new VertexPositionGPUSimulation(new Vector3(1f, 0f, -1f), Vector3.Zero, new Vector2(vector.Z, vector.W), Vector4.One)
				};
				foamMeshDescrtiption.texturePath = all[i];
				foamMeshDescrtiption.planeVerticesCount = foamMeshDescrtiption.planeVertices.Length;
				foamMeshDescrtiption.cacheLocalPositions = new Vector3[foamMeshDescrtiption.planeVerticesCount];
				for (int j = 0; j < foamMeshDescrtiption.planeVerticesCount; j++)
				{
					foamMeshDescrtiption.cacheLocalPositions[j] = foamMeshDescrtiption.planeVertices[j].LocalPosition;
				}
			}
		}

		// Token: 0x040007A8 RID: 1960
		internal OceanFoamParticleDescription.FoamMeshDescrtiption[] meshes;

		// Token: 0x040007A9 RID: 1961
		internal float totalLifeTime;

		// Token: 0x040007AA RID: 1962
		internal float directionalWave;

		// Token: 0x040007AB RID: 1963
		internal float SizeScale;

		// Token: 0x0200018B RID: 395
		internal class FoamMeshDescrtiption
		{
			// Token: 0x040007AC RID: 1964
			public VertexPositionGPUSimulation[] planeVertices;

			// Token: 0x040007AD RID: 1965
			public Vector3[] cacheLocalPositions;

			// Token: 0x040007AE RID: 1966
			public int planeVerticesCount;

			// Token: 0x040007AF RID: 1967
			public Rectangle texturePath;
		}
	}
}
