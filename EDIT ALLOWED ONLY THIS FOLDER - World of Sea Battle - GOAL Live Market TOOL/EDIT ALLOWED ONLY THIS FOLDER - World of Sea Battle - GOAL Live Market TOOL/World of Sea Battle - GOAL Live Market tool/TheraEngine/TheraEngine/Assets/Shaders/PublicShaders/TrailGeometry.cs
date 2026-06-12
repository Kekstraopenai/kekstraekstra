using System;
using Microsoft.Xna.Framework;
using TheraEngine.Graphics;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x02000184 RID: 388
	public class TrailGeometry
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x0003279C File Offset: 0x0003099C
		public TrailGeometry(float {15625})
		{
			this.{15633} = new TrailGeometryFace();
			this.{15634} = new TrailGeometryFace();
			this.{15635} = new TrailGeometryFace();
			this.{15636} = new TrailGeometryFace();
			this.{15633}.SetPos({15625} * 0.5f, 1f);
			this.{15634}.SetPos({15625} * 0.5f, 1f);
			this.{15635}.SetPos({15625} * 0.5f, 1f);
			this.{15636}.SetPos({15625} * 0.5f, 1f);
			this.{15633}.Transform(Matrix.CreateTranslation({15625} * 0.25f, 0f, 0f));
			this.{15634}.Transform(Matrix.CreateTranslation({15625} * 0.25f, 0f, 0f));
			this.{15635}.Transform(Matrix.CreateTranslation({15625} * 0.25f, 0f, 0f));
			this.{15636}.Transform(Matrix.CreateTranslation({15625} * 0.25f, 0f, 0f));
			this.{15635}.Transform(Matrix.CreateTranslation({15625} * 0.5f, 0f, 0f));
			this.{15636}.Transform(Matrix.CreateTranslation({15625} * 0.5f, 0f, 0f));
			this.{15634}.Transform(Matrix.CreateRotationX(1.5707964f));
			this.{15636}.Transform(Matrix.CreateRotationX(1.5707964f));
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00032928 File Offset: 0x00030B28
		public void SetElementData(Vector4 {15626}, Vector3 {15627}, float {15628}, float {15629}, Vector4 {15630}, float {15631})
		{
			for (int i = 0; i < this.{15633}.array.Length; i++)
			{
				this.{15633}.array[i].BallCurrentPosition = {15627};
				this.{15633}.array[i].BallStartPosition = {15626};
				this.{15633}.array[i].FarDistance = {15628};
				this.{15633}.array[i].CurveMultiplier = {15629};
				this.{15633}.array[i].EndAnimationFactor = {15630};
				this.{15633}.array[i].BallColorEffect = {15631};
				this.{15634}.array[i].BallCurrentPosition = {15627};
				this.{15634}.array[i].BallStartPosition = {15626};
				this.{15634}.array[i].FarDistance = {15628};
				this.{15634}.array[i].CurveMultiplier = {15629};
				this.{15634}.array[i].EndAnimationFactor = {15630};
				this.{15634}.array[i].BallColorEffect = {15631};
				this.{15635}.array[i].BallCurrentPosition = {15627};
				this.{15635}.array[i].BallStartPosition = {15626};
				this.{15635}.array[i].FarDistance = {15628};
				this.{15635}.array[i].CurveMultiplier = {15629};
				this.{15635}.array[i].EndAnimationFactor = {15630};
				this.{15635}.array[i].BallColorEffect = {15631};
				this.{15636}.array[i].BallCurrentPosition = {15627};
				this.{15636}.array[i].BallStartPosition = {15626};
				this.{15636}.array[i].FarDistance = {15628};
				this.{15636}.array[i].CurveMultiplier = {15629};
				this.{15636}.array[i].EndAnimationFactor = {15630};
				this.{15633}.array[i].BallColorEffect = {15631};
			}
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00032B88 File Offset: 0x00030D88
		public void QueueBatch(SpriteBatch3D<TrailShader.Vertex> {15632})
		{
			{15632}.Add(this.{15633}.array);
			{15632}.Add(this.{15634}.array);
			{15632}.Add(this.{15635}.array);
			{15632}.Add(this.{15636}.array);
		}

		// Token: 0x04000785 RID: 1925
		private TrailGeometryFace {15633};

		// Token: 0x04000786 RID: 1926
		private TrailGeometryFace {15634};

		// Token: 0x04000787 RID: 1927
		private TrailGeometryFace {15635};

		// Token: 0x04000788 RID: 1928
		private TrailGeometryFace {15636};
	}
}
