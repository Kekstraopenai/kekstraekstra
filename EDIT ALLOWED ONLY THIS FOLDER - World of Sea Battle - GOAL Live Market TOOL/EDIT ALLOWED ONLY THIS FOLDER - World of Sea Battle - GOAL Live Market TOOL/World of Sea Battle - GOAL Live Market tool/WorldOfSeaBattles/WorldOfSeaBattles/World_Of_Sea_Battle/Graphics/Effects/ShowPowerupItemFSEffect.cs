using System;
using Common.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Grphics.Device;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.GameObjects;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004AC RID: 1196
	internal sealed class ShowPowerupItemFSEffect : ShipEffect
	{
		// Token: 0x06001A37 RID: 6711 RVA: 0x000E9641 File Offset: 0x000E7841
		public ShowPowerupItemFSEffect(Ship {24551}, Texture2D {24552}, Rectangle {24553}) : base({24551})
		{
			this.{24556} = {24552};
			this.{24557} = {24553};
			this.{24555} = 3500f;
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x000E9663 File Offset: 0x000E7863
		protected override bool Update(ref FrameTime {24554})
		{
			return {24554}.EvaluteTimerMs2(ref this.{24555});
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x000E9674 File Offset: 0x000E7874
		public override void Render2D()
		{
			ShipPartial getClient = ((IClientShip)this.ship).GetClient;
			if (getClient.IsVisible)
			{
				float num = this.{24555} / 3500f;
				float scale = num * num * (1f - num) * 6f / 0.88888f * getClient.Transparancy2D;
				float scaleFactor = 0.5f;
				Vector2 value = this.{24556}.Bounds.WidthHeight() * scaleFactor;
				Vector2 vector = getClient.Graphics2DPos - value / 2f - new Vector2(0f, 47f);
				if (this.ship is Npc)
				{
					vector.Y -= 40f;
				}
				Rectangle rectangle = new Marker(ref vector, ref value).ToRect();
				float num2 = num * num;
				Color value2 = new Color(1f, 1f, 1f, 1f - num2);
				Device gs = Engine.GS;
				Color color = Color.Black * 0.5f * scale;
				gs.Draw(AtlasObjs.whitepixel_1px, rectangle, color);
				Device gs2 = Engine.GS;
				Texture2D {14570} = this.{24556};
				color = value2 * scale;
				gs2.DrawCustomTexture({14570}, this.{24557}, rectangle, color);
			}
		}

		// Token: 0x04001893 RID: 6291
		private const float c_ttl = 3500f;

		// Token: 0x04001894 RID: 6292
		private float {24555};

		// Token: 0x04001895 RID: 6293
		private Texture2D {24556};

		// Token: 0x04001896 RID: 6294
		private Rectangle {24557};
	}
}
