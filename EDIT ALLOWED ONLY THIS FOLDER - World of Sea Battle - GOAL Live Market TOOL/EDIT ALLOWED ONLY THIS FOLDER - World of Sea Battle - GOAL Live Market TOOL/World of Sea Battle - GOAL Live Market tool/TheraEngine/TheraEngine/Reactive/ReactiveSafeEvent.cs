using System;

namespace TheraEngine.Reactive
{
	// Token: 0x02000069 RID: 105
	public class ReactiveSafeEvent<T>
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x0000FADC File Offset: 0x0000DCDC
		public void Subscribe(Action<T> {12394})
		{
			if ({12394} == null)
			{
				throw new ArgumentNullException("handler");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				this._handlers = (Action<T>)Delegate.Combine(this._handlers, {12394});
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000FB3C File Offset: 0x0000DD3C
		public void Unsubscribe(Action<T> {12395})
		{
			if ({12395} == null)
			{
				throw new ArgumentNullException("handler");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				this._handlers = (Action<T>)Delegate.Remove(this._handlers, {12395});
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000FB9C File Offset: 0x0000DD9C
		public void Push(T {12396})
		{
			object @lock = this._lock;
			Action<T> handlers;
			lock (@lock)
			{
				handlers = this._handlers;
			}
			if (handlers != null)
			{
				handlers({12396});
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000FBE8 File Offset: 0x0000DDE8
		public void Clean()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this._handlers = null;
			}
		}

		// Token: 0x0400023F RID: 575
		private readonly object _lock = new object();

		// Token: 0x04000240 RID: 576
		private Action<T> _handlers;
	}
}
