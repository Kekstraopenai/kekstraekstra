using System;
using Common;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Graphics;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004A6 RID: 1190
	internal sealed class ExplosionWaveFlashEffect : GameEffect
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x000E8B10 File Offset: 0x000E6D10
		public ExplosionWaveFlashEffect(Vector3 {24491}, float {24492}, Color {24493}, float {24494} = 1f) : base(true)
		{
			this.{24497} = 1100f;
			this.{24500} = {24494};
			this.{24501} = {24493};
			this.{24499} = new Transform3D({24491}, Vector3.Zero, new Vector3({24492}, 1f, {24492}));
			this.{24499}.Translation.Y = 0.5f + CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, this.{24499}.Translation.X, this.{24499}.Translation.Z);
			this.{24498} = BillboardParent_VPCT.CreatePlane(1f, 1f, 0f);
			this.{24498}.SetUV(ExplosionWaveFlashEffect.circleTexturePath, AtlasObjs.Texture.Size);
			this.transparancy = 0.4f;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000E8BE4 File Offset: 0x000E6DE4
		public override void Update(ref FrameTime {24495}, out bool {24496})
		{
			if (Global.Player != null)
			{
				Transform3D transform3D = this.{24499};
				transform3D.Scales.X = transform3D.Scales.X + {24495}.secElapsed * 45f * this.{24500};
				Transform3D transform3D2 = this.{24499};
				transform3D2.Scales.Z = transform3D2.Scales.Z + {24495}.secElapsed * 45f * this.{24500};
				this.{24499}.Yaw = Geometry.AxisNorm(this.{24499}.Yaw + {24495}.secElapsed * 0.5f);
				this.{24499}.Translation.Y = 0.75f + CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, this.{24499}.Translation.X, this.{24499}.Translation.Z);
			}
			this.{24497} -= {24495}.msElapsed;
			{24496} = (this.{24497} < 1f);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000E8CE0 File Offset: 0x000E6EE0
		public override void Render3D()
		{
			Vector4 {15450} = (this.{24501} * this.transparancy * ((this.{24497} < 400f) ? (this.{24497} / 400f) : 1f)).ToVector4();
			Global.Render.ItemsShader.SetForRender(this.{24499}.CreateWorldMatrix(), {15450});
			Global.Render.ItemsShader.BeginPass(true, true);
			this.{24498}.Render();
			this.{24499}.Yaw = -this.{24499}.Yaw;
			Transform3D transform3D = this.{24499};
			transform3D.Translation.Y = transform3D.Translation.Y + 0.001f;
			Global.Render.ItemsShader.SetForRender(this.{24499}.CreateWorldMatrix(), {15450});
			Global.Render.ItemsShader.BeginPass(true, true);
			this.{24498}.Render();
			this.{24499}.Yaw = -this.{24499}.Yaw;
			Transform3D transform3D2 = this.{24499};
			transform3D2.Translation.Y = transform3D2.Translation.Y - 0.001f;
		}

		// Token: 0x0400187A RID: 6266
		private static readonly Rectangle circleTexturePath = new Rectangle(1728, 331, 512, 512);

		// Token: 0x0400187B RID: 6267
		private const float c_ttl = 1100f;

		// Token: 0x0400187C RID: 6268
		private const float c_lostStart = 400f;

		// Token: 0x0400187D RID: 6269
		private float {24497};

		// Token: 0x0400187E RID: 6270
		private BillboardParent_VPCT {24498};

		// Token: 0x0400187F RID: 6271
		private Transform3D {24499};

		// Token: 0x04001880 RID: 6272
		private float {24500};

		// Token: 0x04001881 RID: 6273
		public float transparancy;

		// Token: 0x04001882 RID: 6274
		private Color {24501};
	}
}
