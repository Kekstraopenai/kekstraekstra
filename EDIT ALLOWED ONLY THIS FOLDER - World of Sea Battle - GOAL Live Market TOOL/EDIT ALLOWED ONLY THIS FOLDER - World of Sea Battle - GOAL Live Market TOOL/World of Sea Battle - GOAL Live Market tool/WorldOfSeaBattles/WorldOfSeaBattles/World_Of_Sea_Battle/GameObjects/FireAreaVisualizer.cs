using System;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Helpers;
using TheraEngine.Scene.ParticleSystem;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x02000041 RID: 65
	internal sealed class FireAreaVisualizer : FireArea, IPoolObject
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x0000F458 File Offset: 0x0000D658
		public override bool Update(ref FrameTime {16630})
		{
			this.{16633} = CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, this.Position.X, this.Position.Y);
			if (this.{16634} == -1f)
			{
				this.{16634} = 0.1f;
			}
			else
			{
				Geometry.Evalute(ref this.{16634}, this.CurrentRadius, {16630}.secElapsed * 3f * 2f * Geometry.Saturate(0.2f + 0.5f * Math.Abs(this.{16634} - this.CurrentRadius)));
			}
			this.{16635} += {16630}.secElapsed;
			if (Rand.Chanse(3.1415927f * this.CurrentRadius * this.CurrentRadius * 0.08f * {16630}.Factor * 1.5f))
			{
				Vector2 vector = Rand.NextVector2(-1f, 1f) * Rand.Range(0f, this.CurrentRadius - 2f) + this.Position;
				FXEngine.SampleFlameAndSmoke(new Vector3(vector.X, this.{16633} - 0.5f, vector.Y), Rand.Range(3.5f, 4f), false, false, false, new Vector3?(Vector3.Down), 0.6f);
				if (Rand.Chanse(3f / this.CurrentRadius * 40f / 1.3f))
				{
					FXEngine.TemplatePowderSmoke(new ParticleEffectSampleCall(new Vector3(vector.X, this.{16633} - 0.5f, vector.Y), Vector3.Up * 4f), this.CurrentRadius, 500f, 3000f, 0f, false, FXEngine.PowderParticleType.Dark, 0.55f, null);
				}
			}
			if (base.Update(ref {16630}))
			{
				Global.Game.StaticSystem.AddOceanParticle(this.Position, this.{16634} * 0.7f, false, false);
				return true;
			}
			return false;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000F65C File Offset: 0x0000D85C
		public void Render()
		{
			float scaleFactor = (this.TtlToEnd < 2000f) ? (this.TtlToEnd / 2000f) : 1f;
			Global.Render.ItemsShader.RenderWaterDecal(OtherTextures.DecalFirearea.Tex, this.Position, Color.White.ToVector4() * scaleFactor * 0.55f, Math.Max(1f, this.{16634} * 1.8f - 1f), this.{16635} / 5f);
			Global.Render.ItemsShader.RenderWaterDecal(OtherTextures.DecalFirearea.Tex, this.Position, Color.White.ToVector4() * scaleFactor * 0.55f, Math.Max(1f, this.{16634} * 1.8f - 1f), -this.{16635} / 5f);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000F753 File Offset: 0x0000D953
		void IPoolObject.{16631}()
		{
			this.{16635} = 0f;
			this.{16634} = -1f;
		}

		// Token: 0x0400017A RID: 378
		private Timer {16632} = new Timer(1500f);

		// Token: 0x0400017B RID: 379
		private float {16633};

		// Token: 0x0400017C RID: 380
		private float {16634} = -1f;

		// Token: 0x0400017D RID: 381
		private float {16635};
	}
}
