using System;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;

namespace World_Of_Sea_Battle.Core
{
	// Token: 0x020004E7 RID: 1255
	internal sealed class ArenaCacheItem
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x000FFA8B File Offset: 0x000FDC8B
		public int ScoreEnemyComand
		{
			get
			{
				if (this.MyTeamID != ArenaComandID.Team1)
				{
					return this.Team1Score;
				}
				return this.Team2Score;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x000FFAA2 File Offset: 0x000FDCA2
		public int ScoreMyComand
		{
			get
			{
				if (this.MyTeamID != ArenaComandID.Team1)
				{
					return this.Team2Score;
				}
				return this.Team1Score;
			}
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x000FFABC File Offset: 0x000FDCBC
		public ArenaCacheItem(MapArenaInfo {25162}, float {25163}, float {25164}, ArenaMode {25165}, ArenaComandID {25166}, int[] {25167})
		{
			this.ModeInfo = {25165}.GetInfo();
			this.MyTeamID = {25166};
			this.RemainTimeSec = (float)(({25165}.GetInfo().TimeLimitMin == int.MaxValue) ? 0 : ({25165}.GetInfo().TimeLimitMin * 60));
			this.PowerupItemUseLimit = new int[3];
			for (int i = 0; i < this.PowerupItemUseLimit.Length; i++)
			{
				this.PowerupItemUseLimit[i] = ({25165}.GetInfo().LimitedPowerupItems ? 4 : 100000);
			}
			this.ArenaAllyPlayersUid = {25167};
			ValueTuple<Vector2, Vector2> respawns = {25162}.GetRespawns({25165});
			this.MyBasePosition = (({25166} == ArenaComandID.Team1) ? respawns.Item1 : respawns.Item2);
			this.EnemyBasePosition = (({25166} == ArenaComandID.Team2) ? respawns.Item1 : respawns.Item2);
			this.DeadZoneDistance = {25162}.MapSize.X / 2f + 50f;
		}

		// Token: 0x04001A8D RID: 6797
		public readonly ArenaModeSettings ModeInfo;

		// Token: 0x04001A8E RID: 6798
		public ArenaComandID MyTeamID;

		// Token: 0x04001A8F RID: 6799
		public int Team1Score;

		// Token: 0x04001A90 RID: 6800
		public int Team2Score;

		// Token: 0x04001A91 RID: 6801
		public int RemainLoot;

		// Token: 0x04001A92 RID: 6802
		public Vector2 MyBasePosition;

		// Token: 0x04001A93 RID: 6803
		public Vector2 EnemyBasePosition;

		// Token: 0x04001A94 RID: 6804
		public float DeadZoneDistance;

		// Token: 0x04001A95 RID: 6805
		public bool DeadZoneIsReducingNow;

		// Token: 0x04001A96 RID: 6806
		public int[] PowerupItemUseLimit;

		// Token: 0x04001A97 RID: 6807
		public int[] ArenaAllyPlayersUid;

		// Token: 0x04001A98 RID: 6808
		public float RemainTimeSec;
	}
}
