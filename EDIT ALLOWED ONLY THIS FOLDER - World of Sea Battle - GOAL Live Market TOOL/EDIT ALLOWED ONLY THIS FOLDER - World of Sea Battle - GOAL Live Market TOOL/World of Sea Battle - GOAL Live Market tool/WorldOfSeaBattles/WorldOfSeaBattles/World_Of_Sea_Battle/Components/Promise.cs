using System;
using TheraEngine.Interface;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x0200051B RID: 1307
	public class Promise<T> where T : UiControl
	{
		// Token: 0x06001D4A RID: 7498 RVA: 0x0010AA82 File Offset: 0x00108C82
		public Promise(Func<T> {25506})
		{
			this.Status = Promise<T>.PStatus.Scheduled;
			this.Task = {25506};
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0010AA98 File Offset: 0x00108C98
		public void ConinueWith(Func<T, bool> {25507}, Action {25508})
		{
			if (this.Status != Promise<T>.PStatus.Scheduled && this.Status != Promise<T>.PStatus.Running)
			{
				throw new InvalidOperationException();
			}
			this.completionFunc = (Action<T>)Delegate.Combine(this.completionFunc, new Action<T>(delegate(T {25511})
			{
				{25508}();
			}));
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x0010AAEC File Offset: 0x00108CEC
		public Promise<T2> ContinueWith<T2>(Func<T, bool> {25509}, Func<T2> {25510}) where T2 : UiControl
		{
			if (this.Status != Promise<T>.PStatus.Scheduled && this.Status != Promise<T>.PStatus.Running)
			{
				throw new InvalidOperationException();
			}
			Promise<T2> promise = new Promise<T2>({25510});
			this.completionFunc = (Action<T>)Delegate.Combine(this.completionFunc, new Action<T>(delegate(T {25512})
			{
				if ({25509}({25512}))
				{
					promise.Run();
				}
			}));
			return promise;
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x0010AB54 File Offset: 0x00108D54
		public void Run()
		{
			if (this.Status != Promise<T>.PStatus.Scheduled)
			{
				return;
			}
			this.initiator = Global.Game.GetCurrentSceneName;
			this.Status = Promise<T>.PStatus.Running;
			if (this.initiator != Global.Game.GetCurrentSceneName || Global.Player == null)
			{
				this.Status = Promise<T>.PStatus.Cacnelled;
				return;
			}
			T ui = this.Task();
			ui.EvRemoveFromContainer += delegate()
			{
				this.Status = Promise<T>.PStatus.Completed;
				Action<T> action = this.completionFunc;
				if (action == null)
				{
					return;
				}
				action(ui);
			};
		}

		// Token: 0x04001CC6 RID: 7366
		public Func<T> Task;

		// Token: 0x04001CC7 RID: 7367
		public Promise<T>.PStatus Status;

		// Token: 0x04001CC8 RID: 7368
		private GameSceneName initiator;

		// Token: 0x04001CC9 RID: 7369
		private Action<T> completionFunc;

		// Token: 0x0200051C RID: 1308
		public enum PStatus
		{
			// Token: 0x04001CCB RID: 7371
			Scheduled,
			// Token: 0x04001CCC RID: 7372
			Cacnelled,
			// Token: 0x04001CCD RID: 7373
			Running,
			// Token: 0x04001CCE RID: 7374
			Completed
		}
	}
}
