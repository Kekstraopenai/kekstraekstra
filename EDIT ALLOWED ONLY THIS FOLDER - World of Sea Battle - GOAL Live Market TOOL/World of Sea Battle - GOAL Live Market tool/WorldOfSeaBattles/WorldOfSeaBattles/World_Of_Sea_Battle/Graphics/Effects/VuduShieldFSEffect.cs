using System;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Scene.ParticleSystem;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004AE RID: 1198
	internal sealed class VuduShieldFSEffect : ShipEffect
	{
		// Token: 0x06001A3F RID: 6719 RVA: 0x000E9BD4 File Offset: 0x000E7DD4
		public VuduShieldFSEffect(Ship {24573}, Color {24574}, float {24575}) : base({24573})
		{
			this.{24587} = {24574};
			this.{24578} = {24575};
			this.{24577} = this.{24578};
			this.{24579} = Rand.Angle();
			this.{24580} = Rand.Angle();
			this.{24582} = {24573}.UsedShip.StaticInfo.BSRadius / VuduShieldFSEffect.itemSizeOver2.X * 27f * 2f;
			this.{24583} = VuduShieldFSEffect.itemSizeOver2 * this.{24582};
			this.{24584} = new Vector3(0f, {24573}.UsedShip.StaticInfo.BSRadius * 0.32f, 0f);
			this.{24585} = new Vector3(this.{24584}.Y, 0f, 0f);
			this.{24586} = new Vector3(0f, 0f, this.{24584}.Y);
			this.{24581} = 1f;
			Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.Vudushield, {24573}.Position3D, 1f, false);
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x000E9CF0 File Offset: 0x000E7EF0
		protected override bool Update(ref FrameTime {24576})
		{
			if (Global.Player.IsPortEntry)
			{
				return true;
			}
			this.{24579} += 1.0471976f * {24576}.secElapsed;
			this.{24580} -= 0.69813174f * {24576}.secElapsed;
			this.{24577} -= {24576}.msElapsed;
			if ({24576}.EvaluteTimerMs2(ref this.{24581}))
			{
				FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall(this.ship.Position3D, Vector3.Up + Rand.NextVector3(-1f, 1f) * new Vector3(8f, 0f, 8f), -1f), 8f, 250f, 2000f, 0.6f, false, FXEngine.PowderParticleType.GrayDim, 1f, null);
				this.{24581} = 300f;
			}
			return this.{24577} < 1f;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x000E9DE8 File Offset: 0x000E7FE8
		public override void Render2D()
		{
			Vector3 vector = this.ship.Position3D + this.{24584};
			if (((IClientShip)this.ship).GetClient.IsVisibleWithOcclusionQueryAndCorpusTransparancy && Engine.GS.Camera.IsVisible(vector, 2f))
			{
				Vector2 projection = Engine.GS.Camera.GetProjection(vector);
				float num = Engine.GS.Camera.GetSizeFactor(vector, this.{24583}.X * 0.5f) * 4f;
				Color value = this.{24587} * ((this.{24577} > this.{24578} - 500f) ? ((this.{24578} - this.{24577}) / 500f) : ((this.{24577} < 1500f) ? (this.{24577} / 1500f) : 1f));
				Device gs = Engine.GS;
				float {14558} = this.{24579};
				float {14559} = this.{24582} * num;
				Color color = value * 0.2f;
				gs.Draw(VuduShieldFSEffect.itemPath, projection, VuduShieldFSEffect.itemSizeOver2, {14558}, {14559}, color);
				Device gs2 = Engine.GS;
				float {14558}2 = this.{24580};
				float {14559}2 = this.{24582} * num;
				color = value * 0.1f;
				gs2.Draw(VuduShieldFSEffect.itemPath, projection, VuduShieldFSEffect.itemSizeOver2, {14558}2, {14559}2, color);
			}
		}

		// Token: 0x040018A2 RID: 6306
		private const float c_startDuration = 500f;

		// Token: 0x040018A3 RID: 6307
		private const float c_endDuration = 1500f;

		// Token: 0x040018A4 RID: 6308
		private const float c_rotationSpeed1 = 1.0471976f;

		// Token: 0x040018A5 RID: 6309
		private const float c_rotationSpeed2 = 0.69813174f;

		// Token: 0x040018A6 RID: 6310
		private static readonly Rectangle itemPath = new Rectangle(2671, 130, 256, 256);

		// Token: 0x040018A7 RID: 6311
		private static readonly Vector2 itemSizeOver2 = new Vector2((float)VuduShieldFSEffect.itemPath.Width / 2f, (float)VuduShieldFSEffect.itemPath.Height / 2f);

		// Token: 0x040018A8 RID: 6312
		private float {24577};

		// Token: 0x040018A9 RID: 6313
		private float {24578};

		// Token: 0x040018AA RID: 6314
		private float {24579};

		// Token: 0x040018AB RID: 6315
		private float {24580};

		// Token: 0x040018AC RID: 6316
		private float {24581};

		// Token: 0x040018AD RID: 6317
		private float {24582};

		// Token: 0x040018AE RID: 6318
		private Vector2 {24583};

		// Token: 0x040018AF RID: 6319
		private Vector3 {24584};

		// Token: 0x040018B0 RID: 6320
		private Vector3 {24585};

		// Token: 0x040018B1 RID: 6321
		private Vector3 {24586};

		// Token: 0x040018B2 RID: 6322
		private Color {24587};
	}
}
