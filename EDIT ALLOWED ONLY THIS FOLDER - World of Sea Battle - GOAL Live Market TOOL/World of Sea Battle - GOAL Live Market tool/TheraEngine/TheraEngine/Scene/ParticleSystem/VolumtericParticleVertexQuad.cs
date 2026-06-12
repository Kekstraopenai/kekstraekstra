using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Scene.ParticleSystem
{
	// Token: 0x02000054 RID: 84
	internal class VolumtericParticleVertexQuad
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000CE18 File Offset: 0x0000B018
		public VolumtericParticleVertexQuad()
		{
			this.array = new VolumtericParticleVertex[4];
			this.array[0].Position = new Vector3(-0.5f, 0.5f, 0f);
			this.array[1].Position = new Vector3(0.5f, 0.5f, 0f);
			this.array[2].Position = new Vector3(-0.5f, -0.5f, 0f);
			this.array[3].Position = new Vector3(0.5f, -0.5f, 0f);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000CECC File Offset: 0x0000B0CC
		public void SetData(ref Vector4 {12132}, ref Vector3 {12133}, ref Vector3 {12134})
		{
			for (int i = 0; i < 4; i++)
			{
				this.array[i].Color = {12132};
				this.array[i].ParticleCenter = {12133};
				this.array[i].SizeAndRotation = {12134};
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000CF2C File Offset: 0x0000B12C
		public void SetTracerStretch(ref Vector3 {12135})
		{
			for (int i = 0; i < 4; i++)
			{
				this.array[i].Stretch = {12135};
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000CF5C File Offset: 0x0000B15C
		public void SetUV(ref Rectangle {12136}, ref Vector2 {12137})
		{
			Vector2 texture;
			texture.X = (float){12136}.X / {12137}.X;
			texture.Y = (float){12136}.Y / {12137}.Y;
			this.array[0].Texture = texture;
			texture.X = ((float){12136}.X + (float){12136}.Width) / {12137}.X;
			this.array[1].Texture = texture;
			texture.X = (float){12136}.X / {12137}.X;
			texture.Y = ((float){12136}.Y + (float){12136}.Height) / {12137}.Y;
			this.array[2].Texture = texture;
			texture.X = ((float){12136}.X + (float){12136}.Width) / {12137}.X;
			texture.Y = ((float){12136}.Y + (float){12136}.Height) / {12137}.Y;
			this.array[3].Texture = texture;
		}

		// Token: 0x04000197 RID: 407
		public VolumtericParticleVertex[] array;
	}
}
