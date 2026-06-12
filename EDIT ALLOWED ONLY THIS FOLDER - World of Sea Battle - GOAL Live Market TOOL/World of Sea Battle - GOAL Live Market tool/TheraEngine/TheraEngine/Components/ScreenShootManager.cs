using System;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Graphics;

namespace TheraEngine.Components
{
	// Token: 0x02000102 RID: 258
	public static class ScreenShootManager
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x00025408 File Offset: 0x00023608
		public static void TextureToPng(Texture2D {14792}, int {14793}, int {14794}, string {14795})
		{
			using (FileStream fileStream = File.Create({14795}))
			{
				{14792}.SaveAsPng(fileStream, {14793}, {14794});
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00025444 File Offset: 0x00023644
		public static void ScreenShoot(RenderTarget {14796}, string {14797}, string {14798})
		{
			ScreenShootManager.internalTarget = Engine.GS.CreateScreeRenderTragte("ScreenShot");
			Engine.GS.SetRenderTarget(ScreenShootManager.internalTarget);
			Engine.GS.SetTexture({14796}.Resource);
			Engine.GS.Begin2D(false);
			Engine.GS.Draw({14796}.Bounds, {14796}.Bounds);
			Engine.GS.End2D();
			Engine.GS.ReturnRenderTarget();
			new Thread(delegate(object {14799})
			{
				try
				{
					while (!Directory.Exists({14797}))
					{
						Directory.CreateDirectory({14797});
					}
					string str = {14797} + "/wosb_" + DateTime.Now.ToString("yyy_MM_dd__hh_mm_ss");
					while (File.Exists(str + {14798}))
					{
						str += "n";
					}
					ScreenShootManager.TextureToPng(ScreenShootManager.internalTarget.Resource, (int)ScreenShootManager.internalTarget.Size.X, (int)ScreenShootManager.internalTarget.Size.Y, str + {14798});
					ScreenShootManager.internalTarget.Dispose();
				}
				catch (Exception)
				{
				}
			}).Start();
		}

		// Token: 0x04000539 RID: 1337
		private static RenderTarget internalTarget;
	}
}
