using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TheraEngine.ProcedureGeneration
{
	// Token: 0x0200006A RID: 106
	public abstract class Generator
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060002A9 RID: 681 RVA: 0x0000FC40 File Offset: 0x0000DE40
		// (remove) Token: 0x060002AA RID: 682 RVA: 0x0000FC78 File Offset: 0x0000DE78
		public event Action<Generator> Completed
		{
			[CompilerGenerated]
			add
			{
				Action<Generator> action = this.{12399};
				Action<Generator> action2;
				do
				{
					action2 = action;
					Action<Generator> value2 = (Action<Generator>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Generator>>(ref this.{12399}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Generator> action = this.{12399};
				Action<Generator> action2;
				do
				{
					action2 = action;
					Action<Generator> value2 = (Action<Generator>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Generator>>(ref this.{12399}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000FCAD File Offset: 0x0000DEAD
		public bool IsCompleted
		{
			get
			{
				return this.{12400};
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000FCB5 File Offset: 0x0000DEB5
		protected void End()
		{
			if (this.{12400})
			{
				throw new Exception();
			}
			this.{12400} = true;
			Action<Generator> action = this.{12399};
			if (action != null)
			{
				action(this);
			}
			this.{12399} = null;
		}

		// Token: 0x04000241 RID: 577
		[CompilerGenerated]
		private Action<Generator> {12399};

		// Token: 0x04000242 RID: 578
		private bool {12400};
	}
}
