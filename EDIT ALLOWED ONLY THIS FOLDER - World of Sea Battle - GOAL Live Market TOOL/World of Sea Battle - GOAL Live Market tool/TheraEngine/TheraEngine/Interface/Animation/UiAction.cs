using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TheraEngine.Interface.Animation
{
	// Token: 0x020000DA RID: 218
	public abstract class UiAction
	{
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060005F7 RID: 1527 RVA: 0x0001F014 File Offset: 0x0001D214
		// (remove) Token: 0x060005F8 RID: 1528 RVA: 0x0001F04C File Offset: 0x0001D24C
		public event Action<UiControl> Completed
		{
			[CompilerGenerated]
			add
			{
				Action<UiControl> action = this.{14184};
				Action<UiControl> action2;
				do
				{
					action2 = action;
					Action<UiControl> value2 = (Action<UiControl>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<UiControl>>(ref this.{14184}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<UiControl> action = this.{14184};
				Action<UiControl> action2;
				do
				{
					action2 = action;
					Action<UiControl> value2 = (Action<UiControl>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<UiControl>>(ref this.{14184}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x060005F9 RID: 1529
		protected internal abstract bool Update(ref FrameTime {14181}, UiControl {14182});

		// Token: 0x060005FA RID: 1530 RVA: 0x0001F081 File Offset: 0x0001D281
		internal void OnCompleted(UiControl {14183})
		{
			Action<UiControl> action = this.{14184};
			if (action == null)
			{
				return;
			}
			action({14183});
		}

		// Token: 0x0400046B RID: 1131
		[CompilerGenerated]
		private Action<UiControl> {14184};
	}
}
