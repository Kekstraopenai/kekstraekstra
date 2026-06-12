using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Collections;
using TheraEngine.Graphics;
using TheraEngine.Helpers;

namespace TheraEngine.Scene
{
	// Token: 0x0200004B RID: 75
	public class WindEnvironmentEffect
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000BF88 File Offset: 0x0000A188
		public WindEnvironmentEffect(Rectangle {12041}, Vector2 {12042}, int {12043})
		{
			this.{12048} = new Tlist<WindEnvironmentEffect.Renderer>();
			this.{12049} = new Stack<WindEnvironmentEffect.Renderer>();
			for (int i = 0; i < {12043}; i++)
			{
				this.{12049}.Push(new WindEnvironmentEffect.Renderer({12041}, {12042}));
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000BFD0 File Offset: 0x0000A1D0
		public void Update(ref FrameTime {12044}, Vector3 {12045}, Vector3 {12046})
		{
			if (this.{12049}.Count > 0 && Rand.Chanse(5f))
			{
				Vector2 vector = -{12046}.XZ() * 60f + Rand.NextRadialVector2(10f, 10f);
				Vector3 position = {12045};
				position.X += vector.X;
				position.Z += vector.Y;
				position.Y += Rand.Range(4f, 9f);
				WindEnvironmentEffect.Renderer renderer = this.{12049}.Pop();
				renderer.position = position;
				renderer.Time = 0f;
				this.{12048}.Add(renderer);
			}
			for (int i = 0; i < this.{12048}.Size; i++)
			{
				WindEnvironmentEffect.Renderer renderer2 = this.{12048}.Array[i];
				renderer2.position += {12046} * {12044}.secElapsed * 20.5f;
				renderer2.Time += {12044}.secElapsed;
				renderer2.UpdatePos({12046});
				if (renderer2.Time > 5f)
				{
					this.{12048}.FastRemoveAt(i);
					this.{12049}.Push(renderer2);
					i--;
				}
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000C12C File Offset: 0x0000A32C
		public void Render(ParticlesAndStaticMesh {12047})
		{
			{12047}.SetForRender(Matrix.Identity, Vector4.One);
			{12047}.BeginPass(true, true);
			for (int i = 0; i < this.{12048}.Size; i++)
			{
				this.{12048}.Array[i].geometry.Render();
			}
		}

		// Token: 0x0400015C RID: 348
		private Tlist<WindEnvironmentEffect.Renderer> {12048};

		// Token: 0x0400015D RID: 349
		private Stack<WindEnvironmentEffect.Renderer> {12049};

		// Token: 0x0400015E RID: 350
		private Rectangle {12050};

		// Token: 0x0200004C RID: 76
		private class Renderer
		{
			// Token: 0x06000216 RID: 534 RVA: 0x0000C180 File Offset: 0x0000A380
			public Renderer(Rectangle {12053}, Vector2 {12054})
			{
				this.geometry = BillboardParent_VPCT.CreatePlane(12f, 1f, 0f);
				this.geometry.SetCol(Color.White * 0.3f);
				this.geometry.SetUV({12053}, {12054});
				this.{12056} = new VertexPositionColorTexture[this.geometry.array.Length];
				Array.Copy(this.geometry.array, this.{12056}, this.{12056}.Length);
			}

			// Token: 0x06000217 RID: 535 RVA: 0x0000C20C File Offset: 0x0000A40C
			public void UpdatePos(Vector3 {12055})
			{
				Vector3 vector = Engine.GS.Camera.Position;
				Array.Copy(this.{12056}, this.geometry.array, this.{12056}.Length);
				this.geometry.Transform(Matrix.CreateFromYawPitchRoll(0f, MathF.Atan2({12055}.Z, {12055}.X), 0f) * Matrix.CreateTranslation(this.position));
			}

			// Token: 0x0400015F RID: 351
			public BillboardParent_VPCT geometry;

			// Token: 0x04000160 RID: 352
			public Vector3 position;

			// Token: 0x04000161 RID: 353
			public float Time;

			// Token: 0x04000162 RID: 354
			private VertexPositionColorTexture[] {12056};
		}
	}
}
