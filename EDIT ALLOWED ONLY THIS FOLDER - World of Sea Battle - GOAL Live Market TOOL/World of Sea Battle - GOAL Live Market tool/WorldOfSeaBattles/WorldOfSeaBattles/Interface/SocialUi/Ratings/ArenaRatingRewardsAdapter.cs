using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Interface;

namespace WorldOfSeaBattles.Interface.SocialUi.Ratings
{
	// Token: 0x02000571 RID: 1393
	internal class ArenaRatingRewardsAdapter : IRatingRewardsProvider
	{
		// Token: 0x06002042 RID: 8258 RVA: 0x00121EAA File Offset: 0x001200AA
		public IEnumerable<ArenaPlaceReward> GetRewards(int {26176}, int {26177})
		{
			return from {26184} in ArenaRatingGameplay.GetArenaRewards({26176}, {26177})
			select new ArenaPlaceReward({26184}.StartPlaceIndex, {26184}.EndPlaceIndexInclusive, {26184}.Reward, {26184}.RewardId);
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x000070D7 File Offset: 0x000052D7
		public bool ShouldDisplayRewardIcon(RatingType {26178}, float {26179}, ArenaPlaceReward {26180})
		{
			return true;
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00121ED8 File Offset: 0x001200D8
		public Form GetRewardIconForm(RatingRewardId {26181})
		{
			Rectangle rectangle;
			switch ({26181})
			{
			case RatingRewardId.Gold:
				rectangle = {22782}.c_place1;
				break;
			case RatingRewardId.Silver:
				rectangle = {22782}.c_place2;
				break;
			case RatingRewardId.Bronze:
				rectangle = {22782}.c_place3;
				break;
			default:
				throw new NotSupportedException();
			}
			Rectangle {13190} = rectangle;
			return new Form(Vector2.Zero, {13190}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x00121F28 File Offset: 0x00120128
		public ToolTipState CreateToolTipState(RatingRewardId {26182}, ComplexBonus {26183})
		{
			string text;
			switch ({26182})
			{
			case RatingRewardId.Gold:
				text = Local.tournament_place_1;
				break;
			case RatingRewardId.Silver:
				text = Local.tournament_place_2;
				break;
			case RatingRewardId.Bronze:
				text = Local.tournament_place_3;
				break;
			default:
				throw new NotSupportedException();
			}
			string {12528} = text;
			Composer composer = new Composer(350f, 0f);
			composer.AddText({12528}, new ComposerTextStyle(Color.White, false, Fonts.Philosopher_16, null), true);
			composer.AddSpace(10f);
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{19779}.ComplexRewardDisplayHelper({26183}, false, stackForm, new ComposerTextStyle(Color.White * 0.9f, false, null, null), null);
			composer.AddCustom(stackForm, PositionAlignment.Center);
			return new ToolTipState(composer);
		}
	}
}
