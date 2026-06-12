using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Graphics;
using TheraEngine.Grphics.Device;
using TheraEngine.Renderer;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace WorldOfSeaBattles.Interface.DebugPanel
{
	// Token: 0x0200059D RID: 1437
	public class DebugRenderTargets
	{
		// Token: 0x0600213F RID: 8511 RVA: 0x00129354 File Offset: 0x00127554
		public static void ToggleVisible()
		{
			if (DebugRenderTargets.instance == null)
			{
				DebugRenderTargets.instance = new DebugRenderTargets();
			}
			DebugRenderTargets.instance.{26409} = !DebugRenderTargets.instance.{26409};
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00129380 File Offset: 0x00127580
		public static void Render()
		{
			if (DebugRenderTargets.instance == null || !DebugRenderTargets.instance.{26409})
			{
				return;
			}
			RenderTarget[] array = new RenderTarget[12];
			array[0] = Global.Render.rtMain3d;
			array[1] = Global.Render.rtSceneNormals;
			array[2] = Global.Render.rtSceneAlbedo;
			array[3] = Global.Render.rtSsEffects;
			array[4] = Global.Render.rtReflectionsBack;
			array[5] = Global.Render.rtReflectionsFront;
			array[6] = Global.Render.rtReflectionsFrontScene;
			array[7] = Global.Render.rtBokehScene;
			array[8] = Global.Render.rtBloomThreshold;
			array[9] = Global.Render.rtGilMap;
			array[10] = Global.Render.rtOceanNormals;
			int num = 11;
			CascadedShadowMap cascadedShadowHelper = Global.Render.CascadedShadowHelper;
			array[num] = ((cascadedShadowHelper != null) ? cascadedShadowHelper.Target : null);
			int num2 = 0;
			int num3 = 0;
			foreach (RenderTarget renderTarget in array)
			{
				if (renderTarget != null)
				{
					Rectangle rectangle = new Rectangle(num2 * 200, num3 * 200, 200, 200);
					Marker marker = new Marker(ref rectangle);
					Vector2 mouseToUI = Engine.GS.MouseToUI;
					if (marker.Collision(mouseToUI))
					{
						rectangle = new Rectangle(0, 0, 1366, 1366);
					}
					Device gs = Engine.GS;
					Texture2D tex = AtlasGameGui.Texture.Tex;
					Color color = Color.Gray;
					gs.DrawCustomTexture(tex, AtlasGameGui.rect_asset_whitepixel_1px, rectangle, color);
					Device gs2 = Engine.GS;
					Texture2D resource = renderTarget.Resource;
					RenderTarget renderTarget2 = renderTarget;
					color = Color.White;
					gs2.DrawCustomTexture(resource, renderTarget2.Bounds, rectangle, color);
					num2++;
					if (num2 >= 4)
					{
						num2 = 0;
						num3++;
					}
				}
			}
		}

		// Token: 0x0400203D RID: 8253
		private static DebugRenderTargets instance;

		// Token: 0x0400203E RID: 8254
		private bool {26409};
	}
}
