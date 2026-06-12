using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Shaders.InternalShaders;
using TheraEngine.Assets.Shaders.PublicShaders;

namespace TheraEngine.Graphics
{
	// Token: 0x02000144 RID: 324
	public class LightSourceOcclusionTest : IDisposable
	{
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000917 RID: 2327 RVA: 0x0002C684 File Offset: 0x0002A884
		// (remove) Token: 0x06000918 RID: 2328 RVA: 0x0002C6B8 File Offset: 0x0002A8B8
		public static event Action<Exception> OcclusionQueryFail;

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0002C6EB File Offset: 0x0002A8EB
		public float LastResult
		{
			get
			{
				return this.{15030};
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0002C6F3 File Offset: 0x0002A8F3
		public bool HasResults
		{
			get
			{
				return this.{15034};
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0002C6FB File Offset: 0x0002A8FB
		public int LastPixels
		{
			get
			{
				return this.{15031};
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0002C703 File Offset: 0x0002A903
		public int Version
		{
			get
			{
				return this.{15035};
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0002C71C File Offset: 0x0002A91C
		public void BeginTest(Vector3 {15024}, bool {15025}, bool {15026}, ParticlesAndStaticMesh {15027} = null)
		{
			if (this.{15037})
			{
				return;
			}
			try
			{
				if (this.{15029} == null)
				{
					this.{15029} = new OcclusionQuery(Engine.GS.graphicsDevice);
				}
			}
			catch (Exception obj)
			{
				this.{15037} = true;
				Action<Exception> occlusionQueryFail = LightSourceOcclusionTest.OcclusionQueryFail;
				if (occlusionQueryFail != null)
				{
					occlusionQueryFail(obj);
				}
				return;
			}
			if (this.QuerySize != this.{15028})
			{
				this.{15028} = this.QuerySize;
				VertexPositionColor[] array = new VertexPositionColor[6];
				array[0].Position = new Vector3((float)(-(float)this.{15028} / 2), (float)(-(float)this.{15028} / 2), -1f);
				array[0].Color = Color.White;
				array[1].Position = new Vector3((float)(this.{15028} / 2), (float)(-(float)this.{15028} / 2), -1f);
				array[1].Color = Color.White;
				array[2].Position = new Vector3((float)(-(float)this.{15028} / 2), (float)(this.{15028} / 2), -1f);
				array[2].Color = Color.White;
				array[3].Position = new Vector3((float)(this.{15028} / 2), (float)(this.{15028} / 2), -1f);
				array[3].Color = Color.White;
				array[4].Position = new Vector3((float)(this.{15028} / 2), (float)(-(float)this.{15028} / 2), -1f);
				array[4].Color = Color.White;
				array[5].Position = new Vector3((float)(-(float)this.{15028} / 2), (float)(this.{15028} / 2), -1f);
				array[5].Color = Color.White;
				try
				{
					UserMesh userMesh = this.{15032};
					if (userMesh != null)
					{
						userMesh.Dispose();
					}
					this.{15032} = UserMesh.Create<VertexPositionColor>(array, 6);
				}
				catch (Exception obj2)
				{
					Action<Exception> occlusionQueryFail2 = LightSourceOcclusionTest.OcclusionQueryFail;
					if (occlusionQueryFail2 != null)
					{
						occlusionQueryFail2(obj2);
					}
					this.{15037} = true;
				}
			}
			if (this.{15033})
			{
				if (!this.{15029}.IsComplete)
				{
					return;
				}
				IRenderTarget currentOutput = Engine.GS.CurrentOutput;
				float num = (float)(this.{15028} * this.{15028});
				float num2 = (float)(this.{15029}.PixelCount / ((currentOutput == null) ? 1 : ((currentOutput.MultisamplersCount == 0) ? 1 : currentOutput.MultisamplersCount)));
				this.{15030} = MathHelper.Clamp(num2 / num, 0f, 1f);
				this.{15031} = (int)num2;
				this.{15034} = true;
				this.{15033} = false;
				this.{15035}++;
				if (this.{15036} == null)
				{
					this.{15036} = Stopwatch.StartNew();
					return;
				}
				this.{15036}.Restart();
				return;
			}
			else
			{
				if ({15026})
				{
					this.{15030} = 1f;
					this.{15031} = 1;
				}
				if (!{15025} || {15026})
				{
					return;
				}
				if ({15027} != null)
				{
					Matrix matrix = Matrix.CreateBillboard({15024}, Engine.GS.Camera.Position, Vector3.Up, null);
					Matrix matrix2 = Matrix.CreateScale(0.1f, 0.1f, 0f);
					InternalScreenPlaneShader screenPlaneShader = Engine.ScreenPlaneShader;
					Matrix matrix3 = matrix2 * matrix * Engine.GS.Camera.ViewMultiplyProjection;
					Vector4 vector = new Vector4(0f, 0f, 0f, 0f);
					screenPlaneShader.ApplyForIdentityObject(matrix3, vector);
				}
				else
				{
					Matrix viewMultiplyProjection = Engine.GS.Camera.ViewMultiplyProjection;
					Vector4 vector2;
					Vector4.Transform(ref {15024}, ref viewMultiplyProjection, out vector2);
					vector2.X = vector2.X / vector2.W * 0.5f + 0.5f;
					vector2.Y = vector2.Y / vector2.W * -0.5f + 0.5f;
					Matrix matrix4;
					Matrix.CreateTranslation(vector2.X * Engine.GS.CurrentOutputSize.X, vector2.Y * Engine.GS.CurrentOutputSize.Y, 0f, out matrix4);
					Matrix matrix5;
					Matrix.CreateOrthographicOffCenter(0f, Engine.GS.CurrentOutputSize.X, Engine.GS.CurrentOutputSize.Y, 0f, 0f, 1f, out matrix5);
					InternalScreenPlaneShader screenPlaneShader2 = Engine.ScreenPlaneShader;
					Matrix matrix3 = matrix4 * matrix5;
					Vector4 vector = new Vector4(0f, 0f, 0f, 0f);
					screenPlaneShader2.ApplyForIdentityObject(matrix3, vector);
				}
				this.{15029}.Begin();
				this.{15032}.Render();
				this.{15029}.End();
				this.{15033} = true;
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0002CBE0 File Offset: 0x0002ADE0
		public void CleanResults()
		{
			this.{15034} = false;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0002CBE9 File Offset: 0x0002ADE9
		public void Dispose()
		{
			UserMesh userMesh = this.{15032};
			if (userMesh != null)
			{
				userMesh.Dispose();
			}
			this.{15032} = null;
			OcclusionQuery occlusionQuery = this.{15029};
			if (occlusionQuery != null)
			{
				occlusionQuery.Dispose();
			}
			this.{15029} = null;
		}

		// Token: 0x04000622 RID: 1570
		private int {15028};

		// Token: 0x04000623 RID: 1571
		private OcclusionQuery {15029};

		// Token: 0x04000624 RID: 1572
		private float {15030};

		// Token: 0x04000625 RID: 1573
		private int {15031};

		// Token: 0x04000626 RID: 1574
		private UserMesh {15032};

		// Token: 0x04000627 RID: 1575
		private bool {15033};

		// Token: 0x04000628 RID: 1576
		private bool {15034};

		// Token: 0x04000629 RID: 1577
		private int {15035};

		// Token: 0x0400062A RID: 1578
		private Stopwatch {15036};

		// Token: 0x0400062B RID: 1579
		public object Tag;

		// Token: 0x0400062C RID: 1580
		public int QuerySize = 100;

		// Token: 0x0400062D RID: 1581
		public float UpdateIntervalMs;

		// Token: 0x0400062E RID: 1582
		private bool {15037};
	}
}
