using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Graphics;

namespace TheraEngine.Scene.Lighting
{
	// Token: 0x0200005C RID: 92
	public class AmbientLightSource : DisposableObject
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000E2A0 File Offset: 0x0000C4A0
		public void InitializeVisualization(float {12258})
		{
			this.ViewSize = {12258};
			this.{12261} = new BillboardParent_VPCT();
			this.{12261}.SetCol(Color.White);
			this.{12261}.SetPos(this.ViewSize, this.ViewSize);
			UserMesh userMesh = this.{12262};
			if (userMesh != null)
			{
				userMesh.Dispose();
			}
			this.{12262} = UserMesh.Create<VertexPositionColorTexture>(this.{12261}.array, this.{12261}.array.Length);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000E31C File Offset: 0x0000C51C
		public void InitializeUv(Rectangle {12259}, Vector2 {12260})
		{
			if (this.CurrentTexturePath != {12259})
			{
				this.CurrentTexturePath = {12259};
				this.{12261}.SetUV(ref {12259}, ref {12260});
				UserMesh userMesh = this.{12262};
				if (userMesh != null)
				{
					userMesh.Dispose();
				}
				this.{12262} = UserMesh.Create<VertexPositionColorTexture>(this.{12261}.array, this.{12261}.array.Length);
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000E381 File Offset: 0x0000C581
		public void Render()
		{
			this.{12262}.Render();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000E38E File Offset: 0x0000C58E
		public override void Dispose()
		{
			UserMesh userMesh = this.{12262};
			if (userMesh != null)
			{
				userMesh.Dispose();
			}
			this.{12262} = null;
			base.Dispose();
		}

		// Token: 0x040001E8 RID: 488
		public Vector3 RenderPosition;

		// Token: 0x040001E9 RID: 489
		public Rectangle CurrentTexturePath;

		// Token: 0x040001EA RID: 490
		public bool IsEnabled;

		// Token: 0x040001EB RID: 491
		public Vector3 LightDirection;

		// Token: 0x040001EC RID: 492
		public Vector3 LightDirectionForRender;

		// Token: 0x040001ED RID: 493
		public float ViewSize;

		// Token: 0x040001EE RID: 494
		private BillboardParent_VPCT {12261};

		// Token: 0x040001EF RID: 495
		private UserMesh {12262};
	}
}
