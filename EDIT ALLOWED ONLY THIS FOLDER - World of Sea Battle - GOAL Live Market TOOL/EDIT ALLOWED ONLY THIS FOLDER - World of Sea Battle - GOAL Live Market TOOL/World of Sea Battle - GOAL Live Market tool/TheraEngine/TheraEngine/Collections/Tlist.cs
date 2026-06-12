using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using Microsoft.Xna.Framework;
using TheraEngine.Helpers;

namespace TheraEngine.Collections
{
	// Token: 0x0200012E RID: 302
	public class Tlist<T> : IEnumerable<T>, IEnumerable, ITlist, ICollection<T>
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00029237 File Offset: 0x00027437
		public Span<T> AsSpan
		{
			get
			{
				return new Span<T>(this.Array, 0, this.Size);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0002924C File Offset: 0x0002744C
		public Span<byte> AsMemory
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (Tlist<T>.TypeDesc.IsManaged)
				{
					throw new InvalidOperationException("Type T is not unmanaged type");
				}
				Span<T> asSpan = this.AsSpan;
				return new Span<byte>(Unsafe.AsPointer<byte>(Unsafe.As<T, byte>(asSpan.GetPinnableReference())), checked(asSpan.Length * Unsafe.SizeOf<T>()));
			}
		}

		// Token: 0x17000180 RID: 384
		public T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Array[index];
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.Array[index] = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x000292B7 File Offset: 0x000274B7
		Array ITlist.GetArray
		{
			get
			{
				return this.Array;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x000292BF File Offset: 0x000274BF
		int ITlist.GetSize
		{
			get
			{
				return this.Size;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x000292BF File Offset: 0x000274BF
		int ICollection<!0>.Count
		{
			get
			{
				return this.Size;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0000E21A File Offset: 0x0000C41A
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000292C7 File Offset: 0x000274C7
		public Tlist() : this(0)
		{
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000292D0 File Offset: 0x000274D0
		public Tlist(int initialCapacity)
		{
			if (initialCapacity == 0)
			{
				this.Array = Tlist<T>.emptyArray;
				return;
			}
			this.Array = new T[initialCapacity];
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000292F4 File Offset: 0x000274F4
		public Tlist(T[] collection) : this(collection.Length)
		{
			int num = collection.Length;
			this.Size = num;
			System.Array.Copy(collection, this.Array, num);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00029322 File Offset: 0x00027522
		public static Tlist<T> FromParams(params T[] values)
		{
			return new Tlist<T>(values);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002932A File Offset: 0x0002752A
		public static Tlist<T> CreateUnsafe(T[] values)
		{
			return new Tlist<T>(0)
			{
				Size = values.Length,
				Array = values
			};
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00029344 File Offset: 0x00027544
		public Tlist(IEnumerable<T> collection) : this(0)
		{
			ICollection<T> collection2 = collection as ICollection<!0>;
			if (collection2 != null)
			{
				if (collection2.Count > 0)
				{
					this.Array = new T[Math.Max(6, collection2.Count)];
					collection2.CopyTo(this.Array, 0);
					this.Size = collection2.Count;
					return;
				}
			}
			else
			{
				foreach (T t in collection)
				{
					if (this.Array.Length == 0)
					{
						this.Array = new T[6];
					}
					this.Add(t);
				}
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000293F0 File Offset: 0x000275F0
		public void Add(T[] item)
		{
			if (this.Array.Length <= this.Size + item.Length)
			{
				this.Grow(this.Size + item.Length);
			}
			System.Array.Copy(item, 0, this.Array, this.Size, item.Length);
			this.Size += item.Length;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00029448 File Offset: 0x00027648
		public void AddRange(params T[] item)
		{
			if (this.Array.Length <= this.Size + item.Length)
			{
				this.Grow(this.Size + item.Length);
			}
			System.Array.Copy(item, 0, this.Array, this.Size, item.Length);
			this.Size += item.Length;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000294A0 File Offset: 0x000276A0
		public void Add(IEnumerable<T> items)
		{
			foreach (T t in items)
			{
				this.Add(t);
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000294EC File Offset: 0x000276EC
		public void Add(Tlist<T> item)
		{
			if (this.Array.Length <= this.Size + item.Size)
			{
				this.Grow(this.Size + item.Size);
			}
			System.Array.Copy(item.Array, 0, this.Array, this.Size, item.Size);
			this.Size += item.Size;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00029554 File Offset: 0x00027754
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(Tlist<T> item, int startIndex, int count)
		{
			if (this.Array.Length <= this.Size + count)
			{
				this.Grow(this.Size + count);
			}
			System.Array.Copy(item.Array, startIndex, this.Array, this.Size, count);
			this.Size += count;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000295A8 File Offset: 0x000277A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(in T item)
		{
			if (this.Size == this.Array.Length)
			{
				this.nextSize();
			}
			T[] array = this.Array;
			int size = this.Size;
			this.Size = size + 1;
			array[size] = item;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000295ED File Offset: 0x000277ED
		public void AddIfNotContains(in T item)
		{
			if (System.Array.IndexOf<T>(this.Array, item, 0, this.Size) != -1)
			{
				return;
			}
			this.Add(item);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00029612 File Offset: 0x00027812
		public Tlist<T> AddFirst(in T item)
		{
			this.Add(item);
			return this;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002961C File Offset: 0x0002781C
		public void Clear()
		{
			if (this.Size > 0)
			{
				if (Tlist<T>.TypeDesc.IsManaged)
				{
					System.Array.Clear(this.Array, 0, this.Size);
				}
				this.Size = 0;
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0002964C File Offset: 0x0002784C
		public int IndexOf(in T item)
		{
			return System.Array.IndexOf<T>(this.Array, item, 0, this.Size);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00029668 File Offset: 0x00027868
		public unsafe int IndexOfAvx(T item)
		{
			if (!Avx2.IsSupported)
			{
				throw new NotSupportedException();
			}
			if (Tlist<T>.TypeDesc.IsManaged)
			{
				throw new InvalidOperationException("T is managed type");
			}
			if (Unsafe.SizeOf<T>() == 8)
			{
				long num = *Unsafe.As<T, long>(ref item);
				long* ptr = (long*)Unsafe.AsPointer<T>(ref this.Array[0]);
				Vector256<long> right = Avx2.BroadcastScalarToVector256(&num);
				for (int i = 0; i <= this.Size * 8 - 40; i += 32)
				{
					for (int j = i; j < i + 8; j++)
					{
						int num2 = Avx2.MoveMask(Avx2.CompareEqual(Avx.LoadVector256(ptr + j), right).AsByte<long>());
						if (num2 != 0)
						{
							return j + (int)Bmi1.TrailingZeroCount((uint)num2);
						}
					}
				}
				return -1;
			}
			throw new NotSupportedException("Sizeof T is not supported");
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002972C File Offset: 0x0002792C
		public void Insert(int index, in T item)
		{
			if (index > this.Size)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this.Size == this.Array.Length)
			{
				this.nextSize();
			}
			if (index < this.Size)
			{
				System.Array.Copy(this.Array, index, this.Array, index + 1, this.Size - index);
			}
			this.Array[index] = item;
			this.Size++;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000297A4 File Offset: 0x000279A4
		public bool Remove(Func<T, bool> item)
		{
			for (int i = 0; i < this.Size; i++)
			{
				if (item(this.Array[i]))
				{
					this.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000297E0 File Offset: 0x000279E0
		public bool Remove(in T item)
		{
			int num = this.IndexOf(item);
			if (num < 0)
			{
				return false;
			}
			this.RemoveAt(num);
			return true;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00029804 File Offset: 0x00027A04
		public void RemoveAt(int index)
		{
			if (index >= this.Size)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.Size--;
			if (index < this.Size)
			{
				System.Array.Copy(this.Array, index + 1, this.Array, index, this.Size - index);
			}
			this.Array[this.Size] = Tlist<T>.defaultT;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0002986C File Offset: 0x00027A6C
		public bool FastRemove(in T item)
		{
			int num = this.IndexOf(item);
			if (num < 0)
			{
				return false;
			}
			this.FastRemoveAt(num);
			return true;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00029890 File Offset: 0x00027A90
		public void FastRemoveAt(int index)
		{
			if (index >= this.Size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.Size--;
			if (index < this.Size)
			{
				this.Array[index] = this.Array[this.Size];
			}
			this.Array[this.Size] = Tlist<T>.defaultT;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000298FB File Offset: 0x00027AFB
		public T Pop()
		{
			this.Size--;
			T result = this.Array[this.Size];
			this.Array[this.Size] = Tlist<T>.defaultT;
			return result;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00029934 File Offset: 0x00027B34
		public void RemoveTo(int entryIndex)
		{
			if (entryIndex == this.Size - 1)
			{
				this.Clear();
				return;
			}
			System.Array.Copy(this.Array, entryIndex + 1, this.Array, 0, this.Size - entryIndex - 1);
			this.Size -= entryIndex + 1;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00029984 File Offset: 0x00027B84
		public T[] ToArray()
		{
			if (this.Size == 0)
			{
				return Tlist<T>.emptyArray;
			}
			T[] array = new T[this.Size];
			System.Array.Copy(this.Array, array, this.Size);
			return array;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000299BE File Offset: 0x00027BBE
		public void CopyTo(ref T[] target, int count)
		{
			if (count > this.Size)
			{
				throw new InvalidOperationException();
			}
			System.Array.Copy(this.Array, target, count);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x000299E0 File Offset: 0x00027BE0
		public void Foreach(Action<T> action)
		{
			for (int i = 0; i < this.Size; i++)
			{
				action(this.Array[i]);
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00029A10 File Offset: 0x00027C10
		public void Foreach(Action<T> action, int startIndex, int count)
		{
			if (startIndex + count > this.Size)
			{
				throw new ArgumentException();
			}
			for (int i = 0; i < count; i++)
			{
				action(this.Array[startIndex + i]);
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00029A4E File Offset: 0x00027C4E
		public void Sort(IComparer<T> comparer)
		{
			System.Array.Sort<T>(this.Array, 0, this.Size, comparer);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00029A64 File Offset: 0x00027C64
		public void Sort(Comparison<T> comparison)
		{
			if (this.Size > 0)
			{
				IComparer<T> comparer = new FunctorComparer<T>(comparison);
				System.Array.Sort<T>(this.Array, 0, this.Size, comparer);
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00029A9C File Offset: 0x00027C9C
		public void SortTop(Func<T, float> keys)
		{
			if (this.Size <= 10)
			{
				for (int i = 1; i < this.Size; i++)
				{
					float num = keys(this.Array[i]);
					int num2 = i;
					while (num2 > 0 && keys(this.Array[num2 - 1]) < num)
					{
						T[] array = this.Array;
						int num3 = num2;
						ref T ptr = ref this.Array[num2 - 1];
						T t = array[num3];
						array[num3] = ptr;
						ptr = t;
						num2--;
					}
				}
				return;
			}
			if (this.sortingKeys == null)
			{
				this.sortingKeys = new TlistSortingKeys<T>();
			}
			if (this.sortingKeys.cFloat.Length < this.Size)
			{
				this.sortingKeys.cFloat = new float[Math.Max(this.sortingKeys.cFloat.Length * 2, this.Size + 10)];
			}
			for (int j = 0; j < this.Size; j++)
			{
				this.sortingKeys.cFloat[j] = -keys(this.Array[j]);
			}
			System.Array.Sort<float, T>(this.sortingKeys.cFloat, this.Array, 0, this.Size);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00029BDC File Offset: 0x00027DDC
		public void SortTop(Func<T, float> keys, float stepSize)
		{
			this.SortTop((T x) => (int)(keys(x) * stepSize));
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00029C10 File Offset: 0x00027E10
		public void SortTop(Func<T, int> keys)
		{
			if (this.Size > 0)
			{
				if (this.sortingKeys == null)
				{
					this.sortingKeys = new TlistSortingKeys<T>();
				}
				if (this.sortingKeys.cInt.Length < this.Size)
				{
					this.sortingKeys.cInt = new int[Math.Max(this.sortingKeys.cInt.Length * 2, this.Size + 10)];
					this.sortingKeys.clone = new T[Math.Max(this.sortingKeys.clone.Length * 2, this.Size + 10)];
				}
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < this.Size; i++)
				{
					int num3 = keys(this.Array[i]);
					this.sortingKeys.cInt[i] = -num3;
					num = Math.Max(num, num3);
					num2 = Math.Min(num2, num3);
				}
				num -= num2;
				if (num > this.Size * 6)
				{
					System.Array.Sort<int, T>(this.sortingKeys.cInt, this.Array, 0, this.Size);
					return;
				}
				if (num2 < 0)
				{
					for (int j = 0; j < this.Size; j++)
					{
						this.sortingKeys.cInt[j] += num2;
					}
				}
				num++;
				System.Array.Copy(this.Array, this.sortingKeys.clone, this.Size);
				if (this.sortingKeys.counters == null || num > this.sortingKeys.counters.Length)
				{
					this.sortingKeys.counters = new int[num];
				}
				else
				{
					System.Array.Clear(this.sortingKeys.counters, 0, this.sortingKeys.counters.Length);
				}
				for (int k = 0; k < this.Size; k++)
				{
					this.sortingKeys.counters[-this.sortingKeys.cInt[k]]++;
				}
				for (int l = 1; l < this.sortingKeys.counters.Length; l++)
				{
					this.sortingKeys.counters[l] = this.sortingKeys.counters[l] + this.sortingKeys.counters[l - 1];
				}
				for (int m = this.Size - 1; m >= 0; m--)
				{
					int num4 = -this.sortingKeys.cInt[m];
					int num5 = this.sortingKeys.counters[num4] - 1;
					this.Array[this.Size - num5 - 1] = this.sortingKeys.clone[m];
					this.sortingKeys.counters[num4]--;
				}
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00029EB8 File Offset: 0x000280B8
		public void SortTopVector(params Func<T, int>[] parametres)
		{
			this.Sort(delegate(T x, T y)
			{
				int i = 0;
				while (i < parametres.Length)
				{
					int num = parametres[i](x);
					int num2 = parametres[i](y);
					if (num != num2)
					{
						if (num <= num2)
						{
							return 1;
						}
						return -1;
					}
					else
					{
						i++;
					}
				}
				return 0;
			});
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00029EE4 File Offset: 0x000280E4
		public Tlist<T> Clone()
		{
			Tlist<T> tlist = new Tlist<T>(this.Size);
			tlist.Size = this.Size;
			if (this.Size > 0)
			{
				System.Array.Copy(this.Array, tlist.Array, this.Size);
			}
			return tlist;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00029F2A File Offset: 0x0002812A
		public Tlist<T> CloneOrEmptyReadonly()
		{
			if (this.Size == 0)
			{
				return Tlist<T>.EmptyReadonly;
			}
			return this.Clone();
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00029F40 File Offset: 0x00028140
		public T FindNear(float to, Func<T, float> keySelector)
		{
			if (this.Size == 0)
			{
				return Tlist<T>.defaultT;
			}
			T result = this.Array[0];
			float num = Math.Abs(keySelector(this.Array[0]) - to);
			for (int i = 1; i < this.Size; i++)
			{
				float num2 = Math.Abs(keySelector(this.Array[i]) - to);
				if (num2 < num)
				{
					num = num2;
					result = this.Array[i];
				}
			}
			return result;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00029FC4 File Offset: 0x000281C4
		public T FindNear(Vector2 to, Func<T, Vector2> keySelector)
		{
			if (this.Size == 0)
			{
				return Tlist<T>.defaultT;
			}
			T result = this.Array[0];
			float num = Vector2.DistanceSquared(keySelector(this.Array[0]), to);
			for (int i = 1; i < this.Size; i++)
			{
				float num2 = Vector2.DistanceSquared(keySelector(this.Array[i]), to);
				if (num2 < num)
				{
					num = num2;
					result = this.Array[i];
				}
			}
			return result;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0002A044 File Offset: 0x00028244
		public T FindNear(Vector3 to, Func<T, Vector3> keySelector)
		{
			if (this.Size == 0)
			{
				return Tlist<T>.defaultT;
			}
			T result = this.Array[0];
			float num = Vector3.DistanceSquared(keySelector(this.Array[0]), to);
			for (int i = 1; i < this.Size; i++)
			{
				float num2 = Vector3.DistanceSquared(keySelector(this.Array[i]), to);
				if (num2 < num)
				{
					num = num2;
					result = this.Array[i];
				}
			}
			return result;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0002A0C4 File Offset: 0x000282C4
		public T FindNear(Vector2 to, Func<T, Vector2> keySelector, float minDistance)
		{
			if (this.Size == 0)
			{
				return Tlist<T>.defaultT;
			}
			T result = Tlist<T>.defaultT;
			float num = minDistance * minDistance;
			for (int i = 0; i < this.Size; i++)
			{
				float num2 = Vector2.DistanceSquared(keySelector(this.Array[i]), to);
				if (num2 < num)
				{
					num = num2;
					result = this.Array[i];
				}
			}
			return result;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0002A128 File Offset: 0x00028328
		public void Shuffle()
		{
			int i = this.Size;
			while (i > 1)
			{
				int num = TheraEngine.Helpers.Rand.RangeInt(0, i--);
				T t = this.Array[i];
				this.Array[i] = this.Array[num];
				this.Array[num] = t;
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0002A180 File Offset: 0x00028380
		public void Shuffle(Sequence customRandom)
		{
			int i = this.Size;
			while (i > 1)
			{
				int num = customRandom.RangeInt(0, i--);
				T t = this.Array[i];
				this.Array[i] = this.Array[num];
				this.Array[num] = t;
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0002A1D9 File Offset: 0x000283D9
		public T Rand()
		{
			if (this.Size == 0)
			{
				return Tlist<T>.defaultT;
			}
			return this.Array[TheraEngine.Helpers.Rand.RangeInt(0, this.Size)];
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0002A200 File Offset: 0x00028400
		public T Rand(Sequence customRandom)
		{
			if (this.Size == 0)
			{
				return Tlist<T>.defaultT;
			}
			return this.Array[customRandom.RangeInt(0, this.Size)];
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0002A228 File Offset: 0x00028428
		public T Rand(Func<T, bool> filter, Sequence customRandom = null)
		{
			if (this.Size == 0)
			{
				return Tlist<T>.defaultT;
			}
			int num = 0;
			T t;
			for (;;)
			{
				t = this.Array[(customRandom != null) ? customRandom.RangeInt(0, this.Size) : TheraEngine.Helpers.Rand.RangeInt(0, this.Size)];
				if (filter(t))
				{
					break;
				}
				num++;
				if (num > this.Size * 3)
				{
					goto Block_4;
				}
			}
			return t;
			Block_4:
			Tlist<T> tlist = new Tlist<T>();
			for (int i = 0; i < this.Size; i++)
			{
				if (filter(this.Array[i]))
				{
					tlist.Add(this.Array[i]);
				}
			}
			if (customRandom == null)
			{
				return tlist.Rand();
			}
			return tlist.Rand(customRandom);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0002A2D9 File Offset: 0x000284D9
		public void Trim()
		{
			if (this.Array.Length == this.Size)
			{
				return;
			}
			if (this.Size == 0)
			{
				this.Array = Tlist<T>.emptyArray;
				return;
			}
			System.Array.Resize<T>(ref this.Array, this.Size);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0002A314 File Offset: 0x00028514
		public void EnsureCapacitry(int minSize)
		{
			if (this.Size >= minSize)
			{
				return;
			}
			T[] array = new T[minSize];
			if (this.Size > 0)
			{
				System.Array.Copy(this.Array, 0, array, 0, this.Size);
			}
			this.Array = array;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0002A358 File Offset: 0x00028558
		public T MaxItem(Func<T, float> selector)
		{
			if (this.Size == 0)
			{
				return Tlist<T>.defaultT;
			}
			int num = 0;
			float num2 = selector(this.Array[0]);
			for (int i = 1; i < this.Size; i++)
			{
				float num3 = selector(this.Array[i]);
				if (num3 > num2)
				{
					num = i;
					num2 = num3;
				}
			}
			return this.Array[num];
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0002A3C0 File Offset: 0x000285C0
		public T Find(Func<T, bool> predicate)
		{
			for (int i = 0; i < this.Size; i++)
			{
				if (predicate(this.Array[i]))
				{
					return this.Array[i];
				}
			}
			return Tlist<T>.defaultT;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0002A404 File Offset: 0x00028604
		public bool TryFind(Func<T, bool> predicate, out T item)
		{
			for (int i = 0; i < this.Size; i++)
			{
				item = this.Array[i];
				if (predicate(item))
				{
					return true;
				}
			}
			item = Tlist<T>.defaultT;
			return false;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0002A450 File Offset: 0x00028650
		public int FindIndex(Predicate<T> predicate)
		{
			return System.Array.FindIndex<T>(this.Array, 0, this.Size, predicate);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0002A465 File Offset: 0x00028665
		public T First()
		{
			if (this.Size == 0)
			{
				throw new InvalidOperationException("There aren't elements");
			}
			return this.Array[0];
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0002A488 File Offset: 0x00028688
		public T First(Predicate<T> predicate)
		{
			for (int i = 0; i < this.Size; i++)
			{
				if (predicate(this.Array[i]))
				{
					return this.Array[i];
				}
			}
			throw new InvalidOperationException("There aren't elements");
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0002A4D1 File Offset: 0x000286D1
		public T Last()
		{
			if (this.Size == 0)
			{
				throw new InvalidOperationException("There aren't elements");
			}
			return this.Array[this.Size - 1];
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0002A4F9 File Offset: 0x000286F9
		public bool Contains(T item)
		{
			return System.Array.IndexOf<T>(this.Array, item, 0, this.Size) != -1;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0002A514 File Offset: 0x00028714
		public IEnumerable<T> Where(Func<T, bool> cretery)
		{
			Tlist<T>.<Where>d__79 <Where>d__ = new Tlist<T>.<Where>d__79(-2);
			<Where>d__.<>4__this = this;
			<Where>d__.<>3__cretery = cretery;
			return <Where>d__;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0002A52C File Offset: 0x0002872C
		public bool Contains(Func<T, bool> method)
		{
			for (int i = 0; i < this.Size; i++)
			{
				if (method(this.Array[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0002A564 File Offset: 0x00028764
		public int Count(Func<T, bool> comparison)
		{
			int num = 0;
			for (int i = 0; i < this.Size; i++)
			{
				if (comparison(this.Array[i]))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0002A5A0 File Offset: 0x000287A0
		public void RemoveRange(int index, int count)
		{
			if (count > 0)
			{
				int size = this.Size;
				this.Size -= count;
				if (index < this.Size)
				{
					System.Array.Copy(this.Array, index + count, this.Array, index, this.Size - index);
				}
				if (Tlist<T>.TypeDesc.IsManaged)
				{
					System.Array.Clear(this.Array, this.Size, count);
				}
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0002A60C File Offset: 0x0002880C
		public void RemoveAll(Predicate<T> predicate)
		{
			for (int i = 0; i < this.Size; i++)
			{
				if (predicate(this.Array[i]))
				{
					this.FastRemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0002A649 File Offset: 0x00028849
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new TlistEnumerator<T>(this);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0002A649 File Offset: 0x00028849
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new TlistEnumerator<T>(this);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0002A658 File Offset: 0x00028858
		private void nextSize()
		{
			T[] array = new T[Math.Max(6, HashHelper.NextSize(Tlist<T>.TypeDesc.IsMemoryCritical, this.Array.Length))];
			if (this.Size > 0)
			{
				System.Array.Copy(this.Array, 0, array, 0, this.Size);
			}
			this.Array = array;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002A6AC File Offset: 0x000288AC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Grow(int minimum)
		{
			minimum = Math.Max(6, minimum);
			T[] array = new T[HashHelper.NextSize(Tlist<T>.TypeDesc.IsMemoryCritical, Math.Max(minimum, this.Array.Length))];
			if (this.Size > 0)
			{
				System.Array.Copy(this.Array, 0, array, 0, this.Size);
			}
			this.Array = array;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002A709 File Offset: 0x00028909
		void ICollection<!0>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002A713 File Offset: 0x00028913
		void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
		{
			System.Array.Copy(this.Array, 0, array, arrayIndex, this.Size);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0002A729 File Offset: 0x00028929
		bool ICollection<!0>.Remove(T item)
		{
			return this.Remove(item);
		}

		// Token: 0x040005C9 RID: 1481
		public static readonly Tlist<T> EmptyReadonly = new Tlist<T>(0)
		{
			Array = new T[0],
			Size = 0
		};

		// Token: 0x040005CA RID: 1482
		private static readonly T defaultT = default(T);

		// Token: 0x040005CB RID: 1483
		private static readonly T[] emptyArray = new T[0];

		// Token: 0x040005CC RID: 1484
		private static readonly TlistTypeWrapper<T> TypeDesc = new TlistTypeWrapper<T>();

		// Token: 0x040005CD RID: 1485
		public T[] Array;

		// Token: 0x040005CE RID: 1486
		public int Size;

		// Token: 0x040005CF RID: 1487
		private const int cMinInitialSize = 6;

		// Token: 0x040005D0 RID: 1488
		private TlistSortingKeys<T> sortingKeys;
	}
}
