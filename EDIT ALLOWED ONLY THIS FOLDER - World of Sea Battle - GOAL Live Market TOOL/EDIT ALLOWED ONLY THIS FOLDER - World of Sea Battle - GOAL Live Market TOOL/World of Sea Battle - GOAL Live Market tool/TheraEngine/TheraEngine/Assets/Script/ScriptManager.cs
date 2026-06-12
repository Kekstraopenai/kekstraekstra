using System;
using TheraEngine.Assets.Script.ScriptTypes;
using TheraEngine.Collections;
using TheraEngine.Components.Architecture;

namespace TheraEngine.Assets.Script
{
	// Token: 0x02000193 RID: 403
	public class ScriptManager : IUpdateableObject
	{
		// Token: 0x06000A55 RID: 2645 RVA: 0x00033F74 File Offset: 0x00032174
		public void Update(ref FrameTime {15796})
		{
			if (!this.Enabled)
			{
				return;
			}
			for (int i = 0; i < this._internalList.Size; i++)
			{
				Script script = this._internalList.Array[i];
				if (script.IsEnabled)
				{
					script.Update_method(ref {15796});
					if (script is ActionScript)
					{
						i--;
					}
				}
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00033FCC File Offset: 0x000321CC
		public void Render()
		{
			if (!this.Enabled)
			{
				return;
			}
			for (int i = 0; i < this._internalList.Size; i++)
			{
				if (this._internalList.Array[i].IsEnabled)
				{
					this._internalList.Array[i].Render_method();
				}
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0003401E File Offset: 0x0003221E
		public ScriptManager()
		{
			this._internalList = new Tlist<Script>(5);
		}

		// Token: 0x040007E9 RID: 2025
		internal Tlist<Script> _internalList;

		// Token: 0x040007EA RID: 2026
		public bool Enabled = true;
	}
}
