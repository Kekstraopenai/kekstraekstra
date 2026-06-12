using System;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Grphics.Device;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004A3 RID: 1187
	internal class CriticalDamageFlashEffect : ShipEffect
	{
		// Token: 0x06001A0A RID: 6666 RVA: 0x000E85D6 File Offset: 0x000E67D6
		public CriticalDamageFlashEffect(Ship {24452}, Vector3 {24453}, bool {24454}) : base({24452})
		{
			this.{24456} = {24453};
			this.{24457} = 1500f;
			this.{24458} = Color.White;
			this.{24459} = ({24454} ? CriticalDamageFlashEffect.c_cannon : CriticalDamageFlashEffect.c_corpus);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000E8611 File Offset: 0x000E6811
		protected override bool Update(ref FrameTime {24455})
		{
			this.{24457} -= {24455}.msElapsed;
			return this.{24457} < 0f;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x000E8634 File Offset: 0x000E6834
		public override void Render2D()
		{
			Vector3 {14982} = this.{24456} + this.ship.Position3D;
			if (Engine.GS.Camera.IsVisible({14982}, 1f))
			{
				Vector2 projection = Engine.GS.Camera.GetProjection({14982});
				Device gs = Engine.GS;
				Vector2 vector = new Vector2((float)(this.{24459}.Width / 2), (float)(this.{24459}.Height / 2));
				float {14558} = 0f;
				float {14559} = 0.5f + 0.5f * (1f - this.{24457} / 1500f);
				Color color = this.{24458} * (this.{24457} / 1500f);
				gs.Draw(this.{24459}, projection, vector, {14558}, {14559}, color);
			}
			base.Render2D();
		}

		// Token: 0x04001867 RID: 6247
		private static readonly Rectangle c_corpus = new Rectangle(1325, 0, 64, 64);

		// Token: 0x04001868 RID: 6248
		private static readonly Rectangle c_cannon = new Rectangle(1325, 66, 64, 64);

		// Token: 0x04001869 RID: 6249
		private Vector3 {24456};

		// Token: 0x0400186A RID: 6250
		private const float cttl = 1500f;

		// Token: 0x0400186B RID: 6251
		private float {24457};

		// Token: 0x0400186C RID: 6252
		private Color {24458};

		// Token: 0x0400186D RID: 6253
		private Rectangle {24459};
	}
}
