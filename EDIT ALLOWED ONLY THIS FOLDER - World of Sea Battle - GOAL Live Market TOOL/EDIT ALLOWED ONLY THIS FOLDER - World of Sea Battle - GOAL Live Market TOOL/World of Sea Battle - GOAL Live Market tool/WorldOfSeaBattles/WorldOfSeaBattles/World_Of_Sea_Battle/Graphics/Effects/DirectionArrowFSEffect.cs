using System;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Graphics;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004A4 RID: 1188
	internal sealed class DirectionArrowFSEffect : ShipEffect
	{
		// Token: 0x06001A0E RID: 6670 RVA: 0x000E8728 File Offset: 0x000E6928
		public DirectionArrowFSEffect(Ship {24463}, Func<Ship, bool> {24464}, Vector2 {24465}) : base({24463})
		{
			this.{24471} = {24464};
			this.{24472} = {24465};
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000E873F File Offset: 0x000E693F
		protected override bool Update(ref FrameTime {24466})
		{
			return this.{24471}(this.ship) || Global.Player.IsPortEntry;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000E8760 File Offset: 0x000E6960
		public override void Render3D()
		{
			Vector2 {24467} = this.{24472} - Global.Player.Position;
			{24467}.Normalize();
			DirectionArrowFSEffect.Render({24467}, new Rectangle(2428, 406, 128, 128), 0.6f, 1.4f);
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x000E87B4 File Offset: 0x000E69B4
		public static void Render(Vector2 {24467}, Rectangle {24468}, float {24469}, float {24470})
		{
			if (Global.Game.GetCurrentSceneName == GameSceneName.Game)
			{
				DirectionArrowFSEffect.parent.SetUV({24468}, AtlasObjs.Texture.Size);
				float num = Vector2.Dot({24467}, Global.Player.Normal);
				num = 0.75f + 0.25f * Math.Abs(num);
				Transform3D transform3D = new Transform3D();
				transform3D.Translation = new Vector3(Global.Player.Position3D.X + {24467}.X * Global.Player.UsedShip.StaticInfo.BSRadius * 0.75f * num, Global.Player.Position3D.Y + 0.5f, Global.Player.Position3D.Z + {24467}.Y * Global.Player.UsedShip.StaticInfo.BSRadius * 0.75f * num);
				transform3D.Scales = new Vector3({24470});
				transform3D.Yaw = MathF.Atan2({24467}.Y, {24467}.X);
				Global.Render.ItemsShader.SetForRender(transform3D.CreateWorldMatrix(), new Vector4({24469}));
				Global.Render.ItemsShader.BeginPass(true, true);
				DirectionArrowFSEffect.parent.Render();
			}
		}

		// Token: 0x0400186E RID: 6254
		public static readonly Rectangle c_arrowBig = new Rectangle(2090, 844, 178, 178);

		// Token: 0x0400186F RID: 6255
		private Func<Ship, bool> {24471};

		// Token: 0x04001870 RID: 6256
		private Vector2 {24472};

		// Token: 0x04001871 RID: 6257
		private static BillboardParent_VPCT parent = BillboardParent_VPCT.CreatePlane(1f, 1f, 0f);
	}
}
