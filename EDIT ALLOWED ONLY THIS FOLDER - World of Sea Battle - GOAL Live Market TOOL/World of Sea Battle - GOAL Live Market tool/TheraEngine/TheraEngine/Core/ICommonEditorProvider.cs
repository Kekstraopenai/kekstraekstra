using System;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Scene;

namespace TheraEngine.Core
{
	// Token: 0x020000FF RID: 255
	public interface ICommonEditorProvider
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600072F RID: 1839
		InstancedMaterialDictionary EngineEditor_LoadedMaterials { get; }

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000730 RID: 1840
		// (remove) Token: 0x06000731 RID: 1841
		event Action EngineEditor_LoadingComplete;

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000732 RID: 1842
		Tlist<ModelTransformedScene> CurrentStaticScene { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000733 RID: 1843
		Tlist<LocationId> GetLocations { get; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000734 RID: 1844
		float GetCurrentSceneSize { get; }
	}
}
