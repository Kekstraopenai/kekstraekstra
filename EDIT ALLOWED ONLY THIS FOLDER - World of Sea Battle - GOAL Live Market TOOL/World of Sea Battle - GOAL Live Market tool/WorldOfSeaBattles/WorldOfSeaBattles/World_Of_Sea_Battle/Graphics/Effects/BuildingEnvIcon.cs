using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x0200049B RID: 1179
	public readonly struct BuildingEnvIcon
	{
		// Token: 0x060019D0 RID: 6608 RVA: 0x000E543D File Offset: 0x000E363D
		public BuildingEnvIcon(Rectangle {24340}, float {24341}, Texture2D {24342} = null)
		{
			this.Path = {24340};
			this.Scale = {24341};
			this.CustomTex = {24342};
		}

		// Token: 0x04001824 RID: 6180
		public readonly Rectangle Path;

		// Token: 0x04001825 RID: 6181
		public readonly float Scale;

		// Token: 0x04001826 RID: 6182
		[Nullable(2)]
		public readonly Texture2D CustomTex;
	}
}
