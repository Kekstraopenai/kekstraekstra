using System;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Graphics;
using TheraEngine.Graphics;
using TheraEngine.Graphics.Models;

namespace TheraEngine.Scene
{
	// Token: 0x02000040 RID: 64
	public interface ISceneObject3DParent
	{
		// Token: 0x060001D1 RID: 465
		void SetAnimationData(AnimationUnit {11929});

		// Token: 0x060001D2 RID: 466
		void SetWorld(ref Matrix {11930}, ModelRenderer {11931}, ModelTransformedScene {11932});

		// Token: 0x060001D3 RID: 467
		bool SetForPart(ModelPartShadercall {11933});

		// Token: 0x060001D4 RID: 468
		void ApplyPass(ModelGeometryType {11934});
	}
}
