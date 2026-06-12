using System;

namespace TheraEngine.Assets.Script
{
	// Token: 0x02000192 RID: 402
	public abstract class Script
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00033EC1 File Offset: 0x000320C1
		public ScriptClass ScriptClass
		{
			get
			{
				return this.{15794};
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00033EC9 File Offset: 0x000320C9
		public ScriptManager GetParent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00033ED4 File Offset: 0x000320D4
		internal Script(ScriptManager {15790})
		{
			{15790}._internalList.Add(this);
			this._parent = {15790};
			this.IsEnabled = true;
			string text;
			this.Initialize(out this.{15794}, out text);
		}

		// Token: 0x06000A51 RID: 2641
		protected abstract void Initialize(out ScriptClass {15791}, out string {15792});

		// Token: 0x06000A52 RID: 2642
		internal abstract void Update_method(ref FrameTime {15793});

		// Token: 0x06000A53 RID: 2643
		internal abstract void Render_method();

		// Token: 0x06000A54 RID: 2644 RVA: 0x00033F10 File Offset: 0x00032110
		public void Delete()
		{
			if (this._isApply)
			{
				throw new InvalidOperationException("Во время выполнения нельзя удалять скрипт!");
			}
			for (int i = 0; i < this._parent._internalList.Size; i++)
			{
				if (this._parent._internalList.Array[i] == this)
				{
					this._parent._internalList.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x040007E4 RID: 2020
		public bool IsEnabled;

		// Token: 0x040007E5 RID: 2021
		private ScriptClass {15794};

		// Token: 0x040007E6 RID: 2022
		private float {15795};

		// Token: 0x040007E7 RID: 2023
		internal ScriptManager _parent;

		// Token: 0x040007E8 RID: 2024
		internal bool _isApply;
	}
}
