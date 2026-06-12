using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace World_Of_Sea_Battle.GameSystems
{
	// Token: 0x020004B9 RID: 1209
	public static class GraphicsResourcesCache
	{
		// Token: 0x06001A85 RID: 6789 RVA: 0x000EB9A4 File Offset: 0x000E9BA4
		public static T Lookup<T>(object {24690}, Func<T> {24691}) where T : GraphicsResource
		{
			GraphicsResource graphicsResource;
			T t;
			if (GraphicsResourcesCache.disposableAssets.TryGetValue({24690}, out graphicsResource))
			{
				t = (graphicsResource as T);
				if (t != null)
				{
					if (!graphicsResource.IsDisposed)
					{
						return t;
					}
					GraphicsResourcesCache.disposableAssets.Remove({24690});
				}
			}
			t = {24691}();
			GraphicsResourcesCache.disposableAssets.Add({24690}, graphicsResource);
			return t;
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000EBA00 File Offset: 0x000E9C00
		public static bool Remove(object {24692})
		{
			GraphicsResource graphicsResource;
			if (GraphicsResourcesCache.disposableAssets.Remove({24692}, out graphicsResource))
			{
				graphicsResource.Dispose();
				return true;
			}
			return false;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x000EBA28 File Offset: 0x000E9C28
		public static void DisposeAll()
		{
			foreach (GraphicsResource graphicsResource in GraphicsResourcesCache.disposableAssets.Values)
			{
				graphicsResource.Dispose();
			}
			GraphicsResourcesCache.disposableAssets.Clear();
		}

		// Token: 0x040018E7 RID: 6375
		private const string worldMapFogInt = "worldMapFogInt";

		// Token: 0x040018E8 RID: 6376
		private static Dictionary<object, GraphicsResource> disposableAssets = new Dictionary<object, GraphicsResource>();
	}
}
