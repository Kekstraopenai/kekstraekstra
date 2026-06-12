using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TheraEngine.Collections
{
	// Token: 0x02000132 RID: 306
	public class UnmanagedList<[IsUnmanaged] T> : IDisposable where T : struct, ValueType
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0002A903 File Offset: 0x00028B03
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0002A90B File Offset: 0x00028B0B
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0002A913 File Offset: 0x00028B13
		public UnmanagedList(int capacity)
		{
			if (capacity <= 0)
			{
				throw new ArgumentException("Capacity must be greater than zero.");
			}
			this.capacity = capacity;
			this.memory = Marshal.AllocHGlobal(sizeof(T) * capacity);
			this.count = 0;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0002A94C File Offset: 0x00028B4C
		public UnmanagedList(List<T> source)
		{
			this.capacity = source.Count;
			this.memory = Marshal.AllocHGlobal(sizeof(T) * this.capacity);
			this.count = source.Count;
			CollectionsMarshal.AsSpan<T>(source).CopyTo(this.AsSpan());
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0002A9A4 File Offset: 0x00028BA4
		~UnmanagedList()
		{
			this.Dispose(false);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0002A9D4 File Offset: 0x00028BD4
		public ref T GetElementReference(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new IndexOutOfRangeException("Index is out of range.");
			}
			return Unsafe.AsRef<T>(this.memory + (IntPtr)(index * sizeof(T)));
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002AA02 File Offset: 0x00028C02
		public ref TOut GetBlockReference<TOut>(int byteOffset)
		{
			return Unsafe.AsRef<TOut>(this.memory + (IntPtr)byteOffset);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002AA11 File Offset: 0x00028C11
		public unsafe void Add(in T item)
		{
			if (this.Count == this.Capacity)
			{
				throw new InvalidOperationException("The list is full.");
			}
			*this.GetElementReference(this.Count) = item;
			this.count++;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002AA51 File Offset: 0x00028C51
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0002AA60 File Offset: 0x00028C60
		private void Dispose(bool disposing)
		{
			if (this.memory != 0)
			{
				Marshal.FreeHGlobal(this.memory);
				this.memory = (IntPtr)0;
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0002AA7D File Offset: 0x00028C7D
		public Span<T> AsSpan()
		{
			return new Span<T>(this.memory, this.count);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0002AA90 File Offset: 0x00028C90
		public List<T> AsList()
		{
			List<T> list = new List<T>(this.count);
			UnmanagedList<T>.AddRange(list, this.AsSpan());
			return list;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0002AAB0 File Offset: 0x00028CB0
		private static void AddRange(List<T> list, ReadOnlySpan<T> items)
		{
			int num = list.Count;
			list.EnsureCapacity(num + items.Length);
			items.CopyTo(CollectionsMarshal.AsSpan<T>(list).Slice(num));
		}

		// Token: 0x040005DB RID: 1499
		private IntPtr memory;

		// Token: 0x040005DC RID: 1500
		private int capacity;

		// Token: 0x040005DD RID: 1501
		private int count;
	}
}
