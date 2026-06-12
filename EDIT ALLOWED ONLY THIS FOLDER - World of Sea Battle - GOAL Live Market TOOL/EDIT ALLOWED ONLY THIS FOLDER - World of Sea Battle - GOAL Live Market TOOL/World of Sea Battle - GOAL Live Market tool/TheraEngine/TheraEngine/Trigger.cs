using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TheraEngine
{
	// Token: 0x02000032 RID: 50
	public abstract class Trigger
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600019C RID: 412 RVA: 0x00008EF4 File Offset: 0x000070F4
		// (remove) Token: 0x0600019D RID: 413 RVA: 0x00008F2C File Offset: 0x0000712C
		public event Action<bool> Change
		{
			[CompilerGenerated]
			add
			{
				Action<bool> action = this.{11829};
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref this.{11829}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<bool> action = this.{11829};
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref this.{11829}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00008F61 File Offset: 0x00007161
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00008F69 File Offset: 0x00007169
		public virtual bool Value
		{
			get
			{
				return this.{11830};
			}
			protected set
			{
				if (this.{11830} != value)
				{
					Action<bool> action = this.{11829};
					if (action != null)
					{
						action(value);
					}
					this.{11830} = value;
				}
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008F8D File Offset: 0x0000718D
		public Trigger(bool {11828})
		{
			this.{11830} = {11828};
		}

		// Token: 0x040000E2 RID: 226
		[CompilerGenerated]
		private Action<bool> {11829};

		// Token: 0x040000E3 RID: 227
		private bool {11830};
	}
}
