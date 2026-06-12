using System;
using System.Collections;
using System.Collections.Generic;

namespace TheraEngine.Collections
{
	// Token: 0x02000113 RID: 275
	public class Map<T> : IEnumerable<T>, IEnumerable where T : class
	{
		// Token: 0x17000163 RID: 355
		public T this[int slotIndex]
		{
			get
			{
				if (slotIndex >= this.Array.Length || this.Array[slotIndex] == null)
				{
					return default(T);
				}
				return this.Array[slotIndex];
			}
			set
			{
				if (value != null)
				{
					if (slotIndex >= this.Array.Length)
					{
						System.Array.Resize<T>(ref this.Array, slotIndex + 1);
					}
					else if (this.Array[slotIndex] != null)
					{
						throw new InvalidOperationException();
					}
					this.Array[slotIndex] = value;
					return;
				}
				if (slotIndex >= this.Array.Length)
				{
					return;
				}
				if (this.Array.Length == slotIndex + 1)
				{
					int newSize = 0;
					for (int i = 0; i < this.Array.Length - 1; i++)
					{
						if (this.Array[i] != null)
						{
							newSize = i + 1;
						}
					}
					System.Array.Resize<T>(ref this.Array, newSize);
					return;
				}
				this.Array[slotIndex] = default(T);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0002752C File Offset: 0x0002572C
		public int Count
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.Array.Length; i++)
				{
					if (this.Array[i] != null)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00027566 File Offset: 0x00025766
		public Map()
		{
			this.Array = Map<T>.empty;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00027579 File Offset: 0x00025779
		public Map(T[] raw)
		{
			this.Array = raw;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00027588 File Offset: 0x00025788
		public void Clean()
		{
			this.Array = Map<T>.empty;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00027595 File Offset: 0x00025795
		[Obsolete("Can be iterated directly")]
		public IEnumerable<T> Iterate()
		{
			return this;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00027598 File Offset: 0x00025798
		public bool Remove(T item)
		{
			for (int i = 0; i < this.Array.Length; i++)
			{
				if (this.Array[i] == item)
				{
					this[i] = default(T);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000275E4 File Offset: 0x000257E4
		public IEnumerator<T> GetEnumerator()
		{
			Map<T>.<GetEnumerator>d__12 <GetEnumerator>d__ = new Map<T>.<GetEnumerator>d__12(0);
			<GetEnumerator>d__.<>4__this = this;
			return <GetEnumerator>d__;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000275F3 File Offset: 0x000257F3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400057A RID: 1402
		public T[] Array;

		// Token: 0x0400057B RID: 1403
		private static T[] empty = new T[0];
	}
}
