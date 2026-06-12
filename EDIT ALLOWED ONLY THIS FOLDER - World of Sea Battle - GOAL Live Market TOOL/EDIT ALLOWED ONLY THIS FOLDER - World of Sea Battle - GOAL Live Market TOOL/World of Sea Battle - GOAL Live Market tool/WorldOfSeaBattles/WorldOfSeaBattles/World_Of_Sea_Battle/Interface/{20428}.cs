using System;
using Microsoft.Xna.Framework;
using TheraEngine.Interface;
using World_Of_Sea_Battle.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000281 RID: 641
	public static class {20428}
	{
		// Token: 0x06000E1F RID: 3615 RVA: 0x00076ECC File Offset: 0x000750CC
		public static Composer AddSeparatorWosb(this Composer {20429}, string {20430})
		{
			return {20429}.AddSeparator(new Rectangle(699, 112, 97, 17), new Rectangle(699, 112, 97, 17), CommonAtlas.Texture.Tex, {20430}.ToUpper());
		}
	}
}
