using System;

namespace TheraEngine.Assets.Script.ScriptTypes
{
	// Token: 0x02000195 RID: 405
	public abstract class UpdateRenderScript : Script
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x00034039 File Offset: 0x00032239
		public UpdateRenderScript(ScriptManager {15802}) : base({15802})
		{
		}

		// Token: 0x06000A5D RID: 2653
		protected internal abstract void Update(ref FrameTime {15803});

		// Token: 0x06000A5E RID: 2654
		protected internal abstract void Render();

		// Token: 0x06000A5F RID: 2655 RVA: 0x0003405F File Offset: 0x0003225F
		internal override void Update_method(ref FrameTime {15804})
		{
			this._isApply = true;
			this.Update(ref {15804});
			this._isApply = false;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00034076 File Offset: 0x00032276
		internal override void Render_method()
		{
			this._isApply = true;
			this.Render();
			this._isApply = false;
		}
	}
}
