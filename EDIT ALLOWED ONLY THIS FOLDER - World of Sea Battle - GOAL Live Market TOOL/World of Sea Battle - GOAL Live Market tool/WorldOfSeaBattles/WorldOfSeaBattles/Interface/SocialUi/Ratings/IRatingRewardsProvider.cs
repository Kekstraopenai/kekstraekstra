using System;
using System.Collections.Generic;
using Common.Game;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;

namespace WorldOfSeaBattles.Interface.SocialUi.Ratings
{
	// Token: 0x02000575 RID: 1397
	internal interface IRatingRewardsProvider
	{
		// Token: 0x06002052 RID: 8274
		IEnumerable<ArenaPlaceReward> GetRewards(int {26194}, int {26195});

		// Token: 0x06002053 RID: 8275
		bool ShouldDisplayRewardIcon(RatingType {26196}, float {26197}, ArenaPlaceReward {26198});

		// Token: 0x06002054 RID: 8276
		Form GetRewardIconForm(RatingRewardId {26199});

		// Token: 0x06002055 RID: 8277
		ToolTipState CreateToolTipState(RatingRewardId {26200}, ComplexBonus {26201});
	}
}
