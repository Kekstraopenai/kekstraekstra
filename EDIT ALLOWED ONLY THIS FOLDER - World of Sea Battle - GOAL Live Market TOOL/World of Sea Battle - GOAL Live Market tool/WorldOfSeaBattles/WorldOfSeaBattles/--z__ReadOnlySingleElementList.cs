using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x020005D2 RID: 1490
[CompilerGenerated]
internal sealed class <>z__ReadOnlySingleElementList<T> : IEnumerable, ICollection, IList, IEnumerable<!0>, IReadOnlyCollection<!0>, IReadOnlyList<!0>, ICollection<!0>, IList<!0>
{
	// Token: 0x060021F7 RID: 8695 RVA: 0x0012EDF3 File Offset: 0x0012CFF3
	public <>z__ReadOnlySingleElementList(T {26675})
	{
		this._item = {26675};
	}

	// Token: 0x060021F8 RID: 8696 RVA: 0x0012EE02 File Offset: 0x0012D002
	[return: Nullable(1)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return new <>z__ReadOnlySingleElementList<T>.Enumerator(this._item);
	}

	// Token: 0x17000331 RID: 817
	// (get) Token: 0x060021F9 RID: 8697 RVA: 0x000070D7 File Offset: 0x000052D7
	int ICollection.Count
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000332 RID: 818
	// (get) Token: 0x060021FA RID: 8698 RVA: 0x000030FD File Offset: 0x000012FD
	bool ICollection.IsSynchronized
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000333 RID: 819
	// (get) Token: 0x060021FB RID: 8699 RVA: 0x0012ED6C File Offset: 0x0012CF6C
	object ICollection.SyncRoot
	{
		[return: Nullable(1)]
		get
		{
			return this;
		}
	}

	// Token: 0x060021FC RID: 8700 RVA: 0x0012EE0F File Offset: 0x0012D00F
	void ICollection.CopyTo([Nullable(1)] Array {26676}, int {26677})
	{
		{26676}.SetValue(this._item, {26677});
	}

	// Token: 0x17000334 RID: 820
	object IList.this[int {26678}]
	{
		[return: Nullable(2)]
		get
		{
			if ({26678} != 0)
			{
				throw new IndexOutOfRangeException();
			}
			return this._item;
		}
		[param: Nullable(2)]
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x17000335 RID: 821
	// (get) Token: 0x060021FF RID: 8703 RVA: 0x000070D7 File Offset: 0x000052D7
	bool IList.IsFixedSize
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000336 RID: 822
	// (get) Token: 0x06002200 RID: 8704 RVA: 0x000070D7 File Offset: 0x000052D7
	bool IList.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06002201 RID: 8705 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	int IList.Add([Nullable(2)] object {26681})
	{
		throw new NotSupportedException();
	}

	// Token: 0x06002202 RID: 8706 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void IList.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06002203 RID: 8707 RVA: 0x0012EE39 File Offset: 0x0012D039
	bool IList.Contains([Nullable(2)] object {26682})
	{
		return EqualityComparer<T>.Default.Equals(this._item, (T)((object){26682}));
	}

	// Token: 0x06002204 RID: 8708 RVA: 0x0012EE51 File Offset: 0x0012D051
	int IList.IndexOf([Nullable(2)] object {26683})
	{
		if (!EqualityComparer<T>.Default.Equals(this._item, (T)((object){26683})))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06002205 RID: 8709 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void IList.Insert(int {26684}, [Nullable(2)] object {26685})
	{
		throw new NotSupportedException();
	}

	// Token: 0x06002206 RID: 8710 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void IList.Remove([Nullable(2)] object {26686})
	{
		throw new NotSupportedException();
	}

	// Token: 0x06002207 RID: 8711 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void IList.RemoveAt(int {26687})
	{
		throw new NotSupportedException();
	}

	// Token: 0x06002208 RID: 8712 RVA: 0x0012EE02 File Offset: 0x0012D002
	[return: Nullable(new byte[]
	{
		1,
		0
	})]
	IEnumerator<T> IEnumerable<!0>.GetEnumerator()
	{
		return new <>z__ReadOnlySingleElementList<T>.Enumerator(this._item);
	}

	// Token: 0x17000337 RID: 823
	// (get) Token: 0x06002209 RID: 8713 RVA: 0x000070D7 File Offset: 0x000052D7
	int IReadOnlyCollection<!0>.Count
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000338 RID: 824
	T IReadOnlyList<!0>.this[int {26688}]
	{
		get
		{
			if ({26688} != 0)
			{
				throw new IndexOutOfRangeException();
			}
			return this._item;
		}
	}

	// Token: 0x17000339 RID: 825
	// (get) Token: 0x0600220B RID: 8715 RVA: 0x000070D7 File Offset: 0x000052D7
	int ICollection<!0>.Count
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x1700033A RID: 826
	// (get) Token: 0x0600220C RID: 8716 RVA: 0x000070D7 File Offset: 0x000052D7
	bool ICollection<!0>.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x0600220D RID: 8717 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void ICollection<!0>.Add(T {26689})
	{
		throw new NotSupportedException();
	}

	// Token: 0x0600220E RID: 8718 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void ICollection<!0>.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x0600220F RID: 8719 RVA: 0x0012EE7F File Offset: 0x0012D07F
	bool ICollection<!0>.Contains(T {26690})
	{
		return EqualityComparer<T>.Default.Equals(this._item, {26690});
	}

	// Token: 0x06002210 RID: 8720 RVA: 0x0012EE92 File Offset: 0x0012D092
	void ICollection<!0>.CopyTo([Nullable(new byte[]
	{
		1,
		0
	})] T[] {26691}, int {26692})
	{
		{26691}[{26692}] = this._item;
	}

	// Token: 0x06002211 RID: 8721 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	bool ICollection<!0>.Remove(T {26693})
	{
		throw new NotSupportedException();
	}

	// Token: 0x1700033B RID: 827
	T IList<!0>.this[int {26694}]
	{
		get
		{
			if ({26694} != 0)
			{
				throw new IndexOutOfRangeException();
			}
			return this._item;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x06002214 RID: 8724 RVA: 0x0012EEA1 File Offset: 0x0012D0A1
	int IList<!0>.IndexOf(T {26697})
	{
		if (!EqualityComparer<T>.Default.Equals(this._item, {26697}))
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06002215 RID: 8725 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void IList<!0>.Insert(int {26698}, T {26699})
	{
		throw new NotSupportedException();
	}

	// Token: 0x06002216 RID: 8726 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void IList<!0>.RemoveAt(int {26700})
	{
		throw new NotSupportedException();
	}

	// Token: 0x04002132 RID: 8498
	[CompilerGenerated]
	private readonly T _item;

	// Token: 0x020005D3 RID: 1491
	private sealed class Enumerator : IDisposable, IEnumerator, IEnumerator<T>
	{
		// Token: 0x06002217 RID: 8727 RVA: 0x0012EEB9 File Offset: 0x0012D0B9
		public Enumerator(T {26702})
		{
			this.System.Collections.Generic.IEnumerator<T>.Current = {26702};
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x0012EEC8 File Offset: 0x0012D0C8
		object IEnumerator.Current
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x0012EED5 File Offset: 0x0012D0D5
		T IEnumerator<!0>.Current
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0012EEE0 File Offset: 0x0012D0E0
		bool IEnumerator.MoveNext()
		{
			return !this._moveNextCalled && (this._moveNextCalled = true);
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x0012EF01 File Offset: 0x0012D101
		void IEnumerator.Reset()
		{
			this._moveNextCalled = false;
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x00003100 File Offset: 0x00001300
		void IDisposable.Dispose()
		{
		}

		// Token: 0x04002133 RID: 8499
		[CompilerGenerated]
		private readonly T _item;

		// Token: 0x04002134 RID: 8500
		[CompilerGenerated]
		private bool _moveNextCalled;
	}
}
