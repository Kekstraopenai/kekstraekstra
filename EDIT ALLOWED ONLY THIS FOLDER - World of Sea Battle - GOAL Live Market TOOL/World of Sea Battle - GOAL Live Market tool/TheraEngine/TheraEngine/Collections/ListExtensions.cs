using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TheraEngine.Collections
{
	// Token: 0x02000133 RID: 307
	public static class ListExtensions
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x0002AAEC File Offset: 0x00028CEC
		public static ref T GetPointer<[IsUnmanaged] T>(List<T> list) where T : struct, ValueType
		{
			return CollectionsMarshal.AsSpan<T>(list).GetPinnableReference();
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0002AB08 File Offset: 0x00028D08
		public static bool FastRemove<T>(this List<T> list, in T item)
		{
			int num = list.IndexOf(item);
			if (num >= 0)
			{
				list.FastRemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0002AB30 File Offset: 0x00028D30
		public static void FastRemoveAt<T>(this List<T> list, int index)
		{
			if (index != list.Count - 1)
			{
				list[index] = list[list.Count - 1];
			}
			list.RemoveAt(list.Count - 1);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0002AB60 File Offset: 0x00028D60
		public static void AddIfNotContains<T>(this List<T> list, in T item)
		{
			if (list.IndexOf(item) >= 0)
			{
				return;
			}
			list.Add(item);
		}
	}
}
