using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x020005D1 RID: 1489
[CompilerGenerated]
internal sealed class <>z__ReadOnlyArray<T> : IEnumerable, ICollection, IList, IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>
{
	// Token: 0x060021D7 RID: 8663 RVA: 0x0012ED46 File Offset: 0x0012CF46
	public <>z__ReadOnlyArray(T[] {26648})
	{
		this._items = {26648};
	}

	// Token: 0x060021D8 RID: 8664 RVA: 0x0012ED55 File Offset: 0x0012CF55
	[return: Nullable(1)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this._items.GetEnumerator();
	}

	// Token: 0x17000326 RID: 806
	// (get) Token: 0x060021D9 RID: 8665 RVA: 0x0012ED62 File Offset: 0x0012CF62
	int ICollection.Count
	{
		get
		{
			return this._items.Length;
		}
	}

	// Token: 0x17000327 RID: 807
	// (get) Token: 0x060021DA RID: 8666 RVA: 0x000030FD File Offset: 0x000012FD
	bool ICollection.IsSynchronized
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000328 RID: 808
	// (get) Token: 0x060021DB RID: 8667 RVA: 0x0012ED6C File Offset: 0x0012CF6C
	object ICollection.SyncRoot
	{
		[return: Nullable(1)]
		get
		{
			return this;
		}
	}

	// Token: 0x060021DC RID: 8668 RVA: 0x0012ED6F File Offset: 0x0012CF6F
	void ICollection.CopyTo([Nullable(1)] Array {26649}, int {26650})
	{
		this._items.CopyTo({26649}, {26650});
	}

	// Token: 0x17000329 RID: 809
	object IList.this[int {26651}]
	{
		[return: Nullable(2)]
		get
		{
			return this._items[{26651}];
		}
		[param: Nullable(2)]
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x1700032A RID: 810
	// (get) Token: 0x060021DF RID: 8671 RVA: 0x000070D7 File Offset: 0x000052D7
	bool IList.IsFixedSize
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700032B RID: 811
	// (get) Token: 0x060021E0 RID: 8672 RVA: 0x000070D7 File Offset: 0x000052D7
	bool IList.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060021E1 RID: 8673 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	int IList.Add([Nullable(2)] object {26654})
	{
		throw new NotSupportedException();
	}

	// Token: 0x060021E2 RID: 8674 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void IList.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x060021E3 RID: 8675 RVA: 0x0012ED91 File Offset: 0x0012CF91
	bool IList.Contains([Nullable(2)] object {26655})
	{
		return this._items.Contains({26655});
	}

	// Token: 0x060021E4 RID: 8676 RVA: 0x0012ED9F File Offset: 0x0012CF9F
	int IList.IndexOf([Nullable(2)] object {26656})
	{
		return this._items.IndexOf({26656});
	}

	// Token: 0x060021E5 RID: 8677 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void IList.Insert(int {26657}, [Nullable(2)] object {26658})
	{
		throw new NotSupportedException();
	}

	// Token: 0x060021E6 RID: 8678 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void IList.Remove([Nullable(2)] object {26659})
	{
		throw new NotSupportedException();
	}

	// Token: 0x060021E7 RID: 8679 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void IList.RemoveAt(int {26660})
	{
		throw new NotSupportedException();
	}

	// Token: 0x060021E8 RID: 8680 RVA: 0x0012EDAD File Offset: 0x0012CFAD
	[return: Nullable(new byte[]
	{
		1,
		0
	})]
	IEnumerator<T> IEnumerable<!0>.GetEnumerator()
	{
		return this._items.GetEnumerator();
	}

	// Token: 0x1700032C RID: 812
	// (get) Token: 0x060021E9 RID: 8681 RVA: 0x0012ED62 File Offset: 0x0012CF62
	int IReadOnlyCollection<!0>.Count
	{
		get
		{
			return this._items.Length;
		}
	}

	// Token: 0x1700032D RID: 813
	T IReadOnlyList<!0>.this[int {26661}]
	{
		get
		{
			return this._items[{26661}];
		}
	}

	// Token: 0x1700032E RID: 814
	// (get) Token: 0x060021EB RID: 8683 RVA: 0x0012ED62 File Offset: 0x0012CF62
	int ICollection<!0>.Count
	{
		get
		{
			return this._items.Length;
		}
	}

	// Token: 0x1700032F RID: 815
	// (get) Token: 0x060021EC RID: 8684 RVA: 0x000070D7 File Offset: 0x000052D7
	bool ICollection<!0>.IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060021ED RID: 8685 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void ICollection<!0>.Add(T {26662})
	{
		throw new NotSupportedException();
	}

	// Token: 0x060021EE RID: 8686 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void ICollection<!0>.Clear()
	{
		throw new NotSupportedException();
	}

	// Token: 0x060021EF RID: 8687 RVA: 0x0012EDC8 File Offset: 0x0012CFC8
	bool ICollection<!0>.Contains(T {26663})
	{
		return this._items.Contains({26663});
	}

	// Token: 0x060021F0 RID: 8688 RVA: 0x0012EDD6 File Offset: 0x0012CFD6
	void ICollection<!0>.CopyTo([Nullable(new byte[]
	{
		1,
		0
	})] T[] {26664}, int {26665})
	{
		this._items.CopyTo({26664}, {26665});
	}

	// Token: 0x060021F1 RID: 8689 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	bool ICollection<!0>.Remove(T {26666})
	{
		throw new NotSupportedException();
	}

	// Token: 0x17000330 RID: 816
	T IList<!0>.this[int {26667}]
	{
		get
		{
			return this._items[{26667}];
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x060021F4 RID: 8692 RVA: 0x0012EDE5 File Offset: 0x0012CFE5
	int IList<!0>.IndexOf(T {26670})
	{
		return this._items.IndexOf({26670});
	}

	// Token: 0x060021F5 RID: 8693 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void IList<!0>.Insert(int {26671}, T {26672})
	{
		throw new NotSupportedException();
	}

	// Token: 0x060021F6 RID: 8694 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
	void IList<!0>.RemoveAt(int {26673})
	{
		throw new NotSupportedException();
	}

	// Token: 0x04002131 RID: 8497
	[CompilerGenerated]
	private readonly T[] _items;
}
