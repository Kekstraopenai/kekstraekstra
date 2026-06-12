using System;
using Microsoft.Xna.Framework;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Scripts
{
	// Token: 0x0200001A RID: 26
	internal sealed class SnowScript : PrecipitationScript
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00004B99 File Offset: 0x00002D99
		public SnowScript() : base(Global.Game.GetScriptManager, AtlasObjs.rect_game_snowParticle, new Vector2(1f), 9f, 0.5f, 300, false)
		{
		}
	}
}
