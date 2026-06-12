using System;
using Common;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.Scripts
{
	// Token: 0x02000019 RID: 25
	internal sealed class RainScript : PrecipitationScript
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000049DF File Offset: 0x00002BDF
		public float GetFinalLightingFromTinder
		{
			get
			{
				if (!this.IsEnabled)
				{
					return 0f;
				}
				return this.{16283};
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000049F8 File Offset: 0x00002BF8
		public RainScript() : base(Global.Game.GetScriptManager, AtlasObjs.rect_game_rainParticle, new Vector2(10f, 10f), 35f, 0f, 700, false)
		{
			this.{16282} = new Tlist<Thunder>();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004A44 File Offset: 0x00002C44
		protected override void Update(ref FrameTime {16280})
		{
			base.Update(ref {16280});
			if (Rand.Chanse((this.currentPower - 0.7f) / 0.3f * {16280}.secElapsed * 2.5f) && Global.Player != null && CommonGlobal.CurrentClientWeather.FogLevelClient > 0.8f)
			{
				this.{16281}();
			}
			this.{16283} = 0f;
			for (int i = 0; i < this.{16282}.Size; i++)
			{
				Thunder thunder = this.{16282}.Array[i];
				if (thunder.Update({16280}.msElapsed))
				{
					this.{16282}.FastRemoveAt(i);
					i--;
				}
				else
				{
					this.{16283} = MathHelper.Clamp(this.{16283} + thunder.GetLighting, 0f, 1f);
				}
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004B10 File Offset: 0x00002D10
		private void {16281}()
		{
			float {16276} = Rand.Range(300f, 1000f);
			float {16275} = Rand.Range(1f, 3f);
			Tlist<Thunder> tlist = this.{16282};
			Thunder thunder = new Thunder({16275}, {16276});
			tlist.Add(thunder);
			Vector2 vector;
			do
			{
				vector = Rand.NextRadialVector2(200f, 250f);
			}
			while (Vector2.Dot(vector, Engine.GS.Camera.Direction.XZ()) <= 0f);
			FXEngine.CreateLighting(Global.Player.Position + vector, false);
		}

		// Token: 0x04000054 RID: 84
		private Tlist<Thunder> {16282};

		// Token: 0x04000055 RID: 85
		private float {16283};
	}
}
