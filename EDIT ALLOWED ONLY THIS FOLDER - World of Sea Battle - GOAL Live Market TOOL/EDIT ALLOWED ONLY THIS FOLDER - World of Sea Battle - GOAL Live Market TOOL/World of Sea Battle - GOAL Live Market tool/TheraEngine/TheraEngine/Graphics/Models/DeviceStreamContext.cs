using System;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Collections;

namespace TheraEngine.Graphics.Models
{
	// Token: 0x02000162 RID: 354
	public class DeviceStreamContext
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x0002E47F File Offset: 0x0002C67F
		public override string ToString()
		{
			return "DeviceStreamContext: Sets count: " + this.Sets.Size.ToString();
		}

		// Token: 0x04000681 RID: 1665
		public VertexBuffer VertexBuffer;

		// Token: 0x04000682 RID: 1666
		public IndexBuffer IndexBuffer;

		// Token: 0x04000683 RID: 1667
		public Tlist<MeshPartData> Sets;
	}
}
