using System;
using System.Collections.Generic;

namespace TheraEngine.Graphics.Models
{
	// Token: 0x02000163 RID: 355
	internal class OffsetSort : IComparer<MeshPartData>
	{
		// Token: 0x06000983 RID: 2435 RVA: 0x0002E49B File Offset: 0x0002C69B
		int IComparer<MeshPartData>.{15162}(MeshPartData {15163}, MeshPartData {15164})
		{
			return Comparer<int>.Default.Compare({15163}.vertexOffset, {15164}.vertexOffset);
		}
	}
}
