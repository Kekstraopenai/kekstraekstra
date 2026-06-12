using System;
using TheraEngine.Collections;

namespace TheraEngine.Reactive
{
	// Token: 0x02000068 RID: 104
	public class Reactive<T> where T : IEquatable<T>
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000F978 File Offset: 0x0000DB78
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000F9BC File Offset: 0x0000DBBC
		public T Value
		{
			get
			{
				object obj = this.syncRoot;
				T result;
				lock (obj)
				{
					result = this.value;
				}
				return result;
			}
			set
			{
				bool flag = false;
				object obj = this.syncRoot;
				lock (obj)
				{
					if (!object.Equals(value, this.value))
					{
						this.value = value;
						flag = true;
					}
				}
				if (flag)
				{
					this.Pulse(value);
				}
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000FA24 File Offset: 0x0000DC24
		public Reactive(T {12390})
		{
			this.value = {12390};
			this.observers = null;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000FA48 File Offset: 0x0000DC48
		private void Pulse(in T {12391})
		{
			if (this.observers != null)
			{
				for (int i = 0; i < this.observers.Size; i++)
				{
					this.observers.Array[i].action({12391});
				}
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public ReactSubscribtion<T> Subscribe(Action<T> {12392}, bool {12393} = false)
		{
			if (this.observers == null)
			{
				this.observers = new Tlist<ReactSubscribtion<T>>();
			}
			ReactSubscribtion<T> result = new ReactSubscribtion<T>(this.observers, {12392});
			this.observers.Add(result);
			if ({12393})
			{
				{12392}(this.value);
			}
			return result;
		}

		// Token: 0x0400023C RID: 572
		private object syncRoot = new object();

		// Token: 0x0400023D RID: 573
		private T value;

		// Token: 0x0400023E RID: 574
		private Tlist<ReactSubscribtion<T>> observers;
	}
}
