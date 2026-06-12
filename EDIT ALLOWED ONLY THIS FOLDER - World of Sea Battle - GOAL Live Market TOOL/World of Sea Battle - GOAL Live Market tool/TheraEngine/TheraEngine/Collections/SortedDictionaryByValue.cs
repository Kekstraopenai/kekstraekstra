using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace TheraEngine.Collections
{
	// Token: 0x02000124 RID: 292
	public sealed class SortedDictionaryByValue<TKey, TVal>
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00028902 File Offset: 0x00026B02
		public int Count
		{
			get
			{
				return this._valueByKey.Count;
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0002890F File Offset: 0x00026B0F
		public SortedDictionaryByValue() : this(null)
		{
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00028918 File Offset: 0x00026B18
		public SortedDictionaryByValue([Nullable(new byte[]
		{
			2,
			0
		})] IComparer<TVal> comparer)
		{
			this._byValue = new SortedDictionary<TVal, SortedDictionaryByValue<TKey, TVal>.Node>(comparer);
			this._valueByKey = new Dictionary<TKey, TVal>();
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00028937 File Offset: 0x00026B37
		public bool ContainsKey(TKey key)
		{
			return this._valueByKey.ContainsKey(key);
		}

		// Token: 0x17000176 RID: 374
		public TVal this[TKey key]
		{
			get
			{
				return this._valueByKey[key];
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00028953 File Offset: 0x00026B53
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TVal value)
		{
			return this._valueByKey.TryGetValue(key, out value);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00028962 File Offset: 0x00026B62
		public void Add(TKey key, TVal value)
		{
			this._valueByKey.Add(key, value);
			this.AddNodeFor(key, value);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0002897C File Offset: 0x00026B7C
		public bool AddOrUpdate(TKey key, TVal value)
		{
			TVal tval;
			if (!this._valueByKey.TryGetValue(key, out tval))
			{
				this._valueByKey.Add(key, value);
				this.AddNodeFor(key, value);
				return true;
			}
			if (EqualityComparer<TVal>.Default.Equals(tval, value))
			{
				return false;
			}
			this._valueByKey[key] = value;
			this.RemoveNodeFor(key, tval);
			this.AddNodeFor(key, value);
			return false;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x000289E0 File Offset: 0x00026BE0
		public bool UpdateValue(TKey key, TVal newValue)
		{
			TVal tval;
			if (!this._valueByKey.TryGetValue(key, out tval))
			{
				return false;
			}
			if (EqualityComparer<TVal>.Default.Equals(tval, newValue))
			{
				return true;
			}
			this._valueByKey[key] = newValue;
			this.RemoveNodeFor(key, tval);
			this.AddNodeFor(key, newValue);
			return true;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00028A30 File Offset: 0x00026C30
		public bool TryRemove(TKey key)
		{
			TVal value;
			if (!this._valueByKey.TryGetValue(key, out value))
			{
				return false;
			}
			this._valueByKey.Remove(key);
			this.RemoveNodeFor(key, value);
			return true;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00028A65 File Offset: 0x00026C65
		public void Clear()
		{
			this._byValue.Clear();
			this._valueByKey.Clear();
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00028A7D File Offset: 0x00026C7D
		public IEnumerable<KeyValuePair<TKey, TVal>> EnumerateFromTop(int limit = 2147483647)
		{
			SortedDictionaryByValue<TKey, TVal>.<EnumerateFromTop>d__16 <EnumerateFromTop>d__ = new SortedDictionaryByValue<TKey, TVal>.<EnumerateFromTop>d__16(-2);
			<EnumerateFromTop>d__.<>4__this = this;
			<EnumerateFromTop>d__.<>3__limit = limit;
			return <EnumerateFromTop>d__;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00028A94 File Offset: 0x00026C94
		public void FillIndexMapTo(Dictionary<TKey, int> map)
		{
			int num = 0;
			foreach (KeyValuePair<TVal, SortedDictionaryByValue<TKey, TVal>.Node> keyValuePair in this._byValue)
			{
				TVal tval;
				SortedDictionaryByValue<TKey, TVal>.Node node;
				keyValuePair.Deconstruct(out tval, out node);
				Tlist<TKey> keys = node.Keys;
				int size = keys.Size;
				TKey[] array = keys.Array;
				for (int i = 0; i < size; i++)
				{
					map.Add(array[i], num++);
				}
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00028B28 File Offset: 0x00026D28
		private void AddNodeFor(TKey key, TVal value)
		{
			SortedDictionaryByValue<TKey, TVal>.Node node;
			if (!this._byValue.TryGetValue(value, out node))
			{
				node = new SortedDictionaryByValue<TKey, TVal>.Node();
				this._byValue.Add(value, node);
			}
			node.Keys.Add(key);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00028B68 File Offset: 0x00026D68
		private void RemoveNodeFor(TKey key, TVal value)
		{
			SortedDictionaryByValue<TKey, TVal>.Node node;
			if (this._byValue.TryGetValue(value, out node) && node.Keys.FastRemove(key) && node.Keys.Size == 0)
			{
				this._byValue.Remove(value);
			}
		}

		// Token: 0x040005AA RID: 1450
		private readonly SortedDictionary<TVal, SortedDictionaryByValue<TKey, TVal>.Node> _byValue;

		// Token: 0x040005AB RID: 1451
		private readonly Dictionary<TKey, TVal> _valueByKey;

		// Token: 0x02000125 RID: 293
		private sealed class Node
		{
			// Token: 0x040005AC RID: 1452
			public Tlist<TKey> Keys = new Tlist<TKey>(5);
		}
	}
}
