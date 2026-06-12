using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Graphics;

namespace TheraEngine.ProcedureGeneration
{
	// Token: 0x0200006B RID: 107
	public class RenderToTexture : Generator
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000FCE5 File Offset: 0x0000DEE5
		public Texture2D Result
		{
			get
			{
				if (base.IsCompleted)
				{
					return this.{12408};
				}
				throw new InvalidOperationException("Generation is not completed");
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000FD00 File Offset: 0x0000DF00
		public RenderToTexture(Vector2 {12403}, int {12404})
		{
			this.{12406} = {12403};
			this.{12407} = {12404};
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000FD18 File Offset: 0x0000DF18
		public void BeginGeneration(Action {12405})
		{
			RenderTarget renderTarget = (this.{12407} != 0) ? new RenderTarget((int)this.{12406}.X, (int)this.{12406}.Y, SurfaceFormat.Rgba64, DepthFormat.Depth24Stencil8, this.{12407}, true, "ProceduralGeneratorTempTarget", false) : new RenderTarget((int)this.{12406}.X, (int)this.{12406}.Y, SurfaceFormat.Rgba64, DepthFormat.Depth24Stencil8, 0, true, "ProceduralGeneratorTempTarget", false);
			Engine.GS.SetRenderTarget(renderTarget);
			Engine.GS.ClearRenderTargetAndBuffer();
			{12405}();
			Engine.GS.ReturnRenderTarget();
			this.{12408} = renderTarget.Resource;
			base.End();
		}

		// Token: 0x04000243 RID: 579
		private Vector2 {12406};

		// Token: 0x04000244 RID: 580
		private int {12407};

		// Token: 0x04000245 RID: 581
		private Texture2D {12408};
	}
}
