using System;

namespace TheraEngine.Assets.Script.ScriptTypes
{
	// Token: 0x02000194 RID: 404
	public abstract class ActionScript : Script
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x00034039 File Offset: 0x00032239
		public ActionScript(ScriptManager {15798}) : base({15798})
		{
		}

		// Token: 0x06000A59 RID: 2649
		protected internal abstract void Action(ref FrameTime {15799});

		// Token: 0x06000A5A RID: 2650 RVA: 0x00034042 File Offset: 0x00032242
		internal override void Update_method(ref FrameTime {15800})
		{
			this._isApply = true;
			this.Action(ref {15800});
			this._isApply = false;
			base.Delete();
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0000C282 File Offset: 0x0000A482
		internal override void Render_method()
		{
		}
	}
}
