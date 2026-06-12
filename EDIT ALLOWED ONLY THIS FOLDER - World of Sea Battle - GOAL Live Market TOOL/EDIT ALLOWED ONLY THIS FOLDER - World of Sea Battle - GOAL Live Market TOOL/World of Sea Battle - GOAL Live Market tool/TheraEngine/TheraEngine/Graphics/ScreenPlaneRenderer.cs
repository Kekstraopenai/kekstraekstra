using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Shaders.InternalShaders;

namespace TheraEngine.Graphics
{
	// Token: 0x0200014A RID: 330
	public class ScreenPlaneRenderer
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x0002D4CC File Offset: 0x0002B6CC
		internal static void Initialize()
		{
			ScreenPlaneRenderer.renderer = new ScreenPlaneBuffer();
			ScreenPlaneRenderer.renderer.Build();
			Engine.Game.EvSizeChanged += delegate(Vector2 {15078})
			{
				ScreenPlaneRenderer.renderer.Build();
			};
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0002D50C File Offset: 0x0002B70C
		public static void ApplyDefaultVertexShader()
		{
			InternalScreenPlaneShader screenPlaneShader = Engine.ScreenPlaneShader;
			Vector2 currentOutputSize = Engine.GS.CurrentOutputSize;
			screenPlaneShader.SetHalfPixel(currentOutputSize);
			Engine.ScreenPlaneShader.pass_VS.Apply();
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0002D540 File Offset: 0x0002B740
		public static void ApplyDefaultVertexShader_ShaderModel3()
		{
			InternalScreenPlaneShader screenPlaneShader = Engine.ScreenPlaneShader;
			Vector2 currentOutputSize = Engine.GS.CurrentOutputSize;
			screenPlaneShader.SetHalfPixel(currentOutputSize);
			Engine.ScreenPlaneShader.pass_VS_3_0.Apply();
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0002D574 File Offset: 0x0002B774
		public static void DrawWithDefaultShader(Texture2D {15074}, SamplerState {15075} = null, Vector2? {15076} = null)
		{
			Vector2 vector;
			if ({15076} != null)
			{
				InternalScreenPlaneShader screenPlaneShader = Engine.ScreenPlaneShader;
				vector = {15076}.Value;
				screenPlaneShader.SetCustomHalfPixel(vector);
			}
			InternalScreenPlaneShader screenPlaneShader2 = Engine.ScreenPlaneShader;
			vector = Engine.GS.CurrentOutputSize;
			screenPlaneShader2.SetHalfPixel(vector);
			Engine.GS.graphicsDevice.SamplerStates.SetState(0, {15075} ?? SamplerState.PointClamp);
			Engine.GS.graphicsDevice.Textures[0] = {15074};
			Engine.ScreenPlaneShader.pass_VS.Apply();
			Engine.ScreenPlaneShader.pass_PS.Apply();
			ScreenPlaneRenderer.Render();
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0002D610 File Offset: 0x0002B810
		public static void ApplyDefaultPixelAndVertexShader(Texture2D {15077})
		{
			InternalScreenPlaneShader screenPlaneShader = Engine.ScreenPlaneShader;
			Vector2 currentOutputSize = Engine.GS.CurrentOutputSize;
			screenPlaneShader.SetHalfPixel(currentOutputSize);
			Engine.GS.graphicsDevice.SamplerStates.SetState(0, SamplerState.PointClamp);
			Engine.GS.graphicsDevice.Textures[0] = {15077};
			Engine.ScreenPlaneShader.pass_VS.Apply();
			Engine.ScreenPlaneShader.pass_PS.Apply();
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0002D682 File Offset: 0x0002B882
		public static void Render()
		{
			ScreenPlaneRenderer.renderer.SendToGPU();
		}

		// Token: 0x04000648 RID: 1608
		private static ScreenPlaneBuffer renderer;
	}
}
