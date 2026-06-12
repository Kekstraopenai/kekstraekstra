using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Graphics;
using World_Of_Sea_Battle.Components;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x02000042 RID: 66
	internal sealed class GunLightInReflection : IPoolObject
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x0000F790 File Offset: 0x0000D990
		public GunLightInReflection()
		{
			this.parent = BillboardParent_VPCT.CreatePlane(4f, 4f, 0f);
			this.parent.SetUV(new Rectangle(152, 1572, 200, 200), AtlasObjs.Texture.Size);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00003100 File Offset: 0x00001300
		public void ClearResources()
		{
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000F7EB File Offset: 0x0000D9EB
		public void Initialize(Vector3 startPosition)
		{
			this.ttl = 0f;
			this.position = startPosition;
			this.size = 5f;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000F80A File Offset: 0x0000DA0A
		public bool Update(ref FrameTime frameTime)
		{
			this.ttl += frameTime.msElapsed;
			this.size += frameTime.secElapsed;
			return this.ttl > 1000f;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000F840 File Offset: 0x0000DA40
		public void Render(SpriteBatch3D<VertexPositionColorTexture> batch)
		{
			Vector3 vector = Engine.GS.Camera.Position;
			Matrix matrix;
			Matrix.CreateBillboard(ref this.position, ref vector, ref GunLightInReflection.v3up, null, out matrix);
			this.parent.SetPos(this.size, this.size);
			this.parent.Transform(ref matrix);
			this.parent.SetCol(Color.White * MathF.Sqrt((this.ttl < 100f) ? (this.ttl / 100f) : (1f - (this.ttl - 100f) / 900f)));
			batch.Add(this.parent.array);
		}

		// Token: 0x0400017E RID: 382
		private BillboardParent_VPCT parent;

		// Token: 0x0400017F RID: 383
		private Vector3 position;

		// Token: 0x04000180 RID: 384
		private float size;

		// Token: 0x04000181 RID: 385
		private float ttl;

		// Token: 0x04000182 RID: 386
		private static Vector3 v3up = Vector3.Down;
	}
}
