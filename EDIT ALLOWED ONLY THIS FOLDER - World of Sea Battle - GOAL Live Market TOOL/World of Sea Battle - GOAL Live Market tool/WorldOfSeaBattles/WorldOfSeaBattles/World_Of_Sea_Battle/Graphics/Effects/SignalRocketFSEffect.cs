using System;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Scene.ParticleSystem;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004AD RID: 1197
	internal sealed class SignalRocketFSEffect : ShipEffect
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x000E97B6 File Offset: 0x000E79B6
		private static float totalLifetime
		{
			get
			{
				return 7500f;
			}
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000E97C0 File Offset: 0x000E79C0
		public SignalRocketFSEffect(Ship {24560}, Color {24561}) : base({24560})
		{
			this.{24563} = {24561};
			this.{24568} = {24560}.Position3D;
			this.{24569} = {24560}.physicsBody.VelocityPerSec / 60f;
			this.{24564} = SignalRocketFSEffect.totalLifetime;
			this.{24565} = {24560}.UsedShip.StaticInfo.BSRadius / SignalRocketFSEffect.itemSizeOver2.X * 100f;
			this.{24566} = SignalRocketFSEffect.itemSizeOver2 * this.{24565};
			this.{24567} = new Vector3(0f, {24560}.UsedShip.StaticInfo.CorpusShape.FinalHeight * 0.3f * 0.5f, 0f);
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000E9880 File Offset: 0x000E7A80
		protected override bool Update(ref FrameTime {24562})
		{
			if (Rand.Chanse(60f * {24562}.Factor))
			{
				FXEngine.TemplatePowderSmokeCustom(new ParticleEffectSampleCall(this.{24568} + this.{24567}), 0.2f, 200f, 1200f, 0.4f, Color.Lerp(this.{24563}, Color.LightGray, 0.5f).ToVector4(), 3f, null, 1);
			}
			this.{24567}.Y = this.{24567}.Y + {24562}.secElapsed * MathHelper.Max(0f, (30f - this.{24567}.Y) / 10f) * 4f;
			this.{24567}.X = this.{24567}.X + this.{24569}.X * {24562}.msElapsed / 16f;
			this.{24567}.Z = this.{24567}.Z + this.{24569}.Y * {24562}.msElapsed / 16f;
			return {24562}.EvaluteTimerMs2(ref this.{24564});
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000E998C File Offset: 0x000E7B8C
		public override void Render2D()
		{
			if (((IClientShip)this.ship).GetClient.IsVisible)
			{
				Vector3 vector = this.{24568} + this.{24567};
				Vector2 projection = Engine.GS.Camera.GetProjection(vector);
				float scale = (this.{24564} > SignalRocketFSEffect.totalLifetime - 500f) ? ((SignalRocketFSEffect.totalLifetime - this.{24564}) / 500f) : ((this.{24564} < 1000f) ? (this.{24564} / 1000f) : 1f);
				Color color = Color.Lerp(this.{24563}, new Color(255, 255, 255, 0), 0.5f) * scale;
				float num = Engine.GS.Camera.GetSizeFactor(vector, this.{24566}.X * 0.5f) * 0.6f;
				Device gs = Engine.GS;
				float {14558} = 0f;
				float {14559} = this.{24565} * num * (1f + 0.5f * HashHelper.SphericalVectorFromLerp(this.{24564} * 0.2f).X);
				Color color2 = new Color(255, 255, 255, 200) * 0.5f * scale;
				gs.Draw(SignalRocketFSEffect.itemPath, projection, SignalRocketFSEffect.itemSizeOver2, {14558}, {14559}, color2);
				Engine.GS.Draw(SignalRocketFSEffect.itemPath, projection, SignalRocketFSEffect.itemSizeOver2, 0f, this.{24565} * num * (1f + 0.5f * HashHelper.SphericalVectorFromLerp(this.{24564} * 0.4f).Y), color);
				Engine.GS.Draw(SignalRocketFSEffect.itemPath, projection, SignalRocketFSEffect.itemSizeOver2, 0f, this.{24565} * num, color);
				Engine.GS.Draw(SignalRocketFSEffect.itemPath, projection, SignalRocketFSEffect.itemSizeOver2, 0f, this.{24565} * num, color);
			}
		}

		// Token: 0x04001897 RID: 6295
		private const float c_startDuration = 500f;

		// Token: 0x04001898 RID: 6296
		private const float c_endDuration = 1000f;

		// Token: 0x04001899 RID: 6297
		private static readonly Rectangle itemPath = new Rectangle(1992, 1023, 343, 226);

		// Token: 0x0400189A RID: 6298
		private static readonly Vector2 itemSizeOver2 = new Vector2((float)SignalRocketFSEffect.itemPath.Width / 2f, (float)SignalRocketFSEffect.itemPath.Height / 2f);

		// Token: 0x0400189B RID: 6299
		private Color {24563};

		// Token: 0x0400189C RID: 6300
		private float {24564};

		// Token: 0x0400189D RID: 6301
		private float {24565};

		// Token: 0x0400189E RID: 6302
		private Vector2 {24566};

		// Token: 0x0400189F RID: 6303
		private Vector3 {24567};

		// Token: 0x040018A0 RID: 6304
		private Vector3 {24568};

		// Token: 0x040018A1 RID: 6305
		private Vector2 {24569};
	}
}
