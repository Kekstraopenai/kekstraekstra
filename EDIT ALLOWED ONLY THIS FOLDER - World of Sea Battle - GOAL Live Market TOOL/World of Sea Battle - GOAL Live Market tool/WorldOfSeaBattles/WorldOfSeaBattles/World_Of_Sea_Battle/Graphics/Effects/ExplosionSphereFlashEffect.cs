using System;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004A5 RID: 1189
	internal sealed class ExplosionSphereFlashEffect : GameEffect
	{
		// Token: 0x06001A13 RID: 6675 RVA: 0x000E8928 File Offset: 0x000E6B28
		public ExplosionSphereFlashEffect(Vector3 {24477}, float {24478}, Color {24479}, float {24480} = 1f) : base(true)
		{
			this.{24486} = {24479};
			this.{24483} = 600f;
			this.{24485} = {24480};
			this.{24484} = new Transform3D({24477}, Vector3.Zero, new Vector3({24478}, {24478}, {24478}));
			this.transparancy = 1f;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x000E897C File Offset: 0x000E6B7C
		public override void Update(ref FrameTime {24481}, out bool {24482})
		{
			Transform3D transform3D = this.{24484};
			transform3D.Scales.X = transform3D.Scales.X + {24481}.secElapsed * 7f * this.{24485};
			Transform3D transform3D2 = this.{24484};
			transform3D2.Scales.Y = transform3D2.Scales.Y + {24481}.secElapsed * 7f * this.{24485};
			Transform3D transform3D3 = this.{24484};
			transform3D3.Scales.Z = transform3D3.Scales.Z + {24481}.secElapsed * 7f * this.{24485};
			this.{24484}.Yaw = Geometry.AxisNorm(this.{24484}.Yaw + {24481}.secElapsed * 0.5f);
			this.{24483} -= {24481}.msElapsed;
			{24482} = (this.{24483} < 1f);
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000E8A48 File Offset: 0x000E6C48
		public override void Render3D()
		{
			Vector4 {15450} = (this.{24486} * this.transparancy * ((this.{24483} < 300f) ? (this.{24483} / 300f) : 1f)).ToVector4();
			Global.Render.ItemsShader.SetForRender(this.{24484}.CreateWorldMatrix(), {15450});
			Global.Render.ItemsShader.BeginPass(true, true);
			LocalContent.Loaded.EffectSphereModel.Transform.CopyFrom(this.{24484});
			LocalContent.Loaded.EffectSphereModel.RenderWithShader(null, null, null, null);
		}

		// Token: 0x04001872 RID: 6258
		private static readonly Rectangle circleTexturePath = new Rectangle(1728, 331, 512, 512);

		// Token: 0x04001873 RID: 6259
		private const float c_ttl = 600f;

		// Token: 0x04001874 RID: 6260
		private const float c_lostStart = 300f;

		// Token: 0x04001875 RID: 6261
		private float {24483};

		// Token: 0x04001876 RID: 6262
		private Transform3D {24484};

		// Token: 0x04001877 RID: 6263
		private float {24485};

		// Token: 0x04001878 RID: 6264
		public float transparancy;

		// Token: 0x04001879 RID: 6265
		private Color {24486};
	}
}
