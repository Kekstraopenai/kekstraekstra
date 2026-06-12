using System;
using System.Runtime.CompilerServices;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Helpers;
using TheraEngine.Scene.Lighting;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004AA RID: 1194
	internal sealed class ShipCustomLightEffect : ShipEffect
	{
		// Token: 0x06001A28 RID: 6696 RVA: 0x000E92D4 File Offset: 0x000E74D4
		public static Color GetLightColor(int {24529})
		{
			if ({24529} == 7)
			{
				return Color.Gold;
			}
			if ({24529} == 8)
			{
				return Color.BlueViolet;
			}
			if ({24529} == 9)
			{
				return Color.Wheat;
			}
			if ({24529} == 10)
			{
				return Color.SkyBlue;
			}
			if ({24529} == 11)
			{
				return Color.LightGreen;
			}
			if ({24529} == 12)
			{
				return Color.LightYellow;
			}
			if ({24529} == 404)
			{
				return new Color(128, 255, 255);
			}
			if ({24529} != 423)
			{
				return Color.OrangeRed;
			}
			return Color.LightYellow;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000E9354 File Offset: 0x000E7554
		public ShipCustomLightEffect(Ship {24530}, ShipDesignInfo {24531}) : base({24530})
		{
			if ({24531} == null)
			{
				throw new ArgumentNullException("src");
			}
			this.{24536} = {24531};
			this.{24535} = new PointLight(Vector3.Zero, ShipCustomLightEffect.GetLightColor((int){24531}.ID).ToVector3(), 0.5f, 6f);
			this.{24535}.OlccusionaryFlaresOpacity = 0f;
			this.{24535}.CentralFlareScale = 1f;
			Global.Render.Pointlights.Add(this.{24535});
			base.EndLife += this.{24534};
			this.{24533}();
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000E93F8 File Offset: 0x000E75F8
		protected override bool Update(ref FrameTime {24532})
		{
			if (this.ship == Global.Player && Rand.Chanse(10f * {24532}.Factor))
			{
				FXEngine.Template_ColoredSmoke(this.{24535}.Position, new Vector4(this.{24535}.Color, 1f) * 0.05f, 1.5f, 1000f);
			}
			this.{24533}();
			return false;
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x000E9468 File Offset: 0x000E7668
		private void {24533}()
		{
			if (this.{24535} != null)
			{
				this.{24535}.Intensivity = 0.6f * (0.4f + 0.6f * (1f - Global.Game.StaticSystem.GetSkyShader.DayOrNight));
				Vector3 value = this.ship.Transform.Transform3X3(new Vector3(((Player)this.ship).UsedShipPlayer.BigLampPosition, 0f));
				this.{24535}.Position = value + new Vector3(0f, 0.15f, 0f);
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00003100 File Offset: 0x00001300
		public override void Render3D()
		{
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000E950C File Offset: 0x000E770C
		[CompilerGenerated]
		private void {24534}()
		{
			Global.Render.Pointlights.Remove(this.{24535});
		}

		// Token: 0x0400188E RID: 6286
		private PointLight {24535};

		// Token: 0x0400188F RID: 6287
		private ShipDesignInfo {24536};
	}
}
