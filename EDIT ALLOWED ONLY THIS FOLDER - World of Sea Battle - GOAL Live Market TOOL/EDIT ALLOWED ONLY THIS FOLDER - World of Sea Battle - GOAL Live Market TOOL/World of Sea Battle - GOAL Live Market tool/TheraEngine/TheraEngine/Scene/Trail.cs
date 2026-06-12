using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Helpers;

namespace TheraEngine.Scene
{
	// Token: 0x02000049 RID: 73
	public class Trail : IDisposable
	{
		// Token: 0x06000209 RID: 521 RVA: 0x0000B928 File Offset: 0x00009B28
		public Trail(Vector3 {12007}, float {12008}, int {12009}, float {12010}, GraphicsDevice {12011}, float {12012})
		{
			this.{12029} = {12007};
			this.{12028} = {12008};
			this.{12027} = {12009};
			this.{12031} = {12010};
			this.{12033} = {12012};
			this.{12022} = new DynamicVertexBuffer({12011}, VertexPositionColorTexture.VertexDeclaration, this.{12027} * 2, BufferUsage.None);
			this.{12023} = new IndexBuffer({12011}, IndexElementSize.SixteenBits, (this.{12027} - 1) * 6, BufferUsage.WriteOnly);
			this.{12024} = new VertexPositionColorTexture[this.{12027} * 2];
			this.{12025} = new float[this.{12027}];
			this.{12013}();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000B9DC File Offset: 0x00009BDC
		private void {12013}()
		{
			short[] array = new short[(this.{12027} - 1) * 6];
			for (int i = 0; i < this.{12027} - 1; i++)
			{
				array[i * 6] = (short)(i * 2);
				array[1 + i * 6] = (short)(1 + i * 2);
				array[2 + i * 6] = (short)(2 + i * 2);
				array[3 + i * 6] = (short)(1 + i * 2);
				array[4 + i * 6] = (short)(3 + i * 2);
				array[5 + i * 6] = (short)(2 + i * 2);
			}
			this.{12023}.SetData<short>(array);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000BA64 File Offset: 0x00009C64
		public void Update(Vector3 {12014}, float {12015})
		{
			Vector3 direction = Engine.GS.Camera.Direction;
			if (this.{12026} == 0)
			{
				this.{12024}[0].Position = this.{12029} + Vector3.Left;
				this.{12024}[0].TextureCoordinate = new Vector2(0f, 0f);
				this.{12024}[1].Position = this.{12029} + Vector3.Right;
				this.{12024}[1].TextureCoordinate = new Vector2(0f, 1f);
				this.{12026} = 1;
			}
			Vector3 value = {12014} - this.{12029};
			float num = value.Length();
			if (num > this.{12028})
			{
				Vector3 vector = value / num;
				Vector3 value2 = Vector3.Cross(direction, vector);
				int num2 = this.{12026};
				if (num2 >= this.{12027} - 1)
				{
					this.{12020}();
				}
				else
				{
					this.{12026}++;
				}
				this.{12034}++;
				this.{12024}[num2 * 2].Position = {12014} + value2 * this.{12031};
				this.{12024}[num2 * 2].TextureCoordinate = new Vector2((float)(this.{12034} % 2), 0f);
				this.{12024}[num2 * 2 + 1].Position = {12014} - value2 * this.{12031};
				this.{12024}[num2 * 2 + 1].TextureCoordinate = new Vector2((float)(this.{12034} % 2), 1f);
				this.{12025}[num2] = this.{12033};
				this.{12029} = {12014};
			}
			else if (num > this.{12032})
			{
				Vector3 vector2 = value / num;
				Vector3 vector3 = Vector3.Cross(direction, vector2);
				int num3 = this.{12026};
				this.{12024}[num3 * 2].Position = {12014} + vector3 * this.{12031};
				this.{12024}[num3 * 2].Color = Color.White * {12015};
				this.{12024}[num3 * 2 + 1].Position = {12014} - vector3 * this.{12031};
				this.{12024}[num3 * 2 + 1].Color = Color.White * {12015};
				this.{12025}[num3] = this.{12033};
				if (num3 >= 2)
				{
					Vector3 {11467} = this.{12024}[(num3 - 1) * 2].Position - this.{12024}[(num3 - 2) * 2].Position;
					Vector3 vector4 = Vector3.Cross(direction, {11467}.NormalizeLocal());
					vector4 += (1f - Geometry.Saturate(Vector3.Dot(vector4, vector3))) * vector3;
					vector4.Normalize();
					this.{12024}[(num3 - 1) * 2].Position = this.{12029} + vector4 * this.{12031};
					this.{12024}[(num3 - 1) * 2 + 1].Position = this.{12029} - vector4 * this.{12031};
				}
			}
			for (int i = 0; i < this.{12025}.Length; i++)
			{
				this.{12025}[i] = Math.Max(0f, this.{12025}[i] - Engine.Game.GameTime.ElapsedDrawReal);
				this.{12024}[i * 2].Color = Color.White * (this.{12025}[i] / this.{12033} * {12015});
				this.{12024}[i * 2 + 1].Color = Color.White * (this.{12025}[i] / this.{12033} * {12015});
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000BE7E File Offset: 0x0000A07E
		private float {12016}(float {12017})
		{
			return ({12017} + 1f) / 3f;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000BE8D File Offset: 0x0000A08D
		private float {12018}(float {12019})
		{
			return {12019} * 3f - 1f;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000BE9C File Offset: 0x0000A09C
		private void {12020}()
		{
			for (int i = 0; i < this.{12027} - 1; i++)
			{
				this.{12024}[i * 2] = this.{12024}[i * 2 + 2];
				this.{12024}[i * 2 + 1] = this.{12024}[i * 2 + 3];
				this.{12025}[i] = this.{12025}[i + 1];
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000BF0C File Offset: 0x0000A10C
		public void Draw(GraphicsDevice {12021})
		{
			this.{12022}.SetData<VertexPositionColorTexture>(this.{12024});
			{12021}.SetVertexBuffer(this.{12022});
			{12021}.Indices = this.{12023};
			{12021}.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, this.{12026} * 2, 0, (this.{12027} - 1) * 2);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000BF5E File Offset: 0x0000A15E
		public void Dispose()
		{
			this.{12022}.Dispose();
			this.{12023}.Dispose();
		}

		// Token: 0x0400014D RID: 333
		private DynamicVertexBuffer {12022};

		// Token: 0x0400014E RID: 334
		private IndexBuffer {12023};

		// Token: 0x0400014F RID: 335
		private VertexPositionColorTexture[] {12024};

		// Token: 0x04000150 RID: 336
		private float[] {12025};

		// Token: 0x04000151 RID: 337
		private int {12026};

		// Token: 0x04000152 RID: 338
		private int {12027};

		// Token: 0x04000153 RID: 339
		private float {12028};

		// Token: 0x04000154 RID: 340
		private Vector3 {12029};

		// Token: 0x04000155 RID: 341
		private int {12030} = 4;

		// Token: 0x04000156 RID: 342
		private float {12031} = 1f;

		// Token: 0x04000157 RID: 343
		private float {12032} = 0.01f;

		// Token: 0x04000158 RID: 344
		private float {12033};

		// Token: 0x04000159 RID: 345
		private int {12034};
	}
}
