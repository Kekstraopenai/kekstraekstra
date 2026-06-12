using System;
using Microsoft.Xna.Framework;
using TheraEngine.Graphics;
using TheraEngine.Graphics.Models;
using TheraEngine.ProcedureGeneration.Generation3D;

namespace TheraEngine.Assets.Sky
{
	// Token: 0x0200016F RID: 367
	public class SkyBox : DisposableObject
	{
		// Token: 0x060009C6 RID: 2502 RVA: 0x0002FACA File Offset: 0x0002DCCA
		public SkyBox(bool {15299})
		{
			this.{15301} = SphereGenerator.Begin_VertexPositionColor(20, 10f, Color.White);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002FAE9 File Offset: 0x0002DCE9
		public SkyBox(UWModel {15300})
		{
			this.{15302} = {15300};
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002FAF8 File Offset: 0x0002DCF8
		public void Render()
		{
			if (this.{15302} == null)
			{
				this.{15301}.Render();
				return;
			}
			this.{15302}.OptimizedRenderAllBuffers();
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002FB19 File Offset: 0x0002DD19
		public override void Dispose()
		{
			UserIndexedMesh userIndexedMesh = this.{15301};
			if (userIndexedMesh != null)
			{
				userIndexedMesh.Dispose();
			}
			UWModel uwmodel = this.{15302};
			if (uwmodel != null)
			{
				uwmodel.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x040006B9 RID: 1721
		private UserIndexedMesh {15301};

		// Token: 0x040006BA RID: 1722
		private UWModel {15302};
	}
}
