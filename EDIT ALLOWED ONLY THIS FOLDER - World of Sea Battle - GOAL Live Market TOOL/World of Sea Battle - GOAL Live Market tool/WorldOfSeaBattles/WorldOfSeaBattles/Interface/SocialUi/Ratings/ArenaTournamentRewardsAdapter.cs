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
	// Token: 0x02000573 RID: 1395
	internal class ArenaTournamentRewardsAdapter : IRatingRewardsProvider
	{
		// Token: 0x0600204A RID: 8266 RVA: 0x00122017 File Offset: 0x00120217
		public IEnumerable<ArenaPlaceReward> GetRewards(int {26185}, int {26186})
		{
			return from {26193} in ArenaTournamentGameplay.GetRewards({26186})
			select new ArenaPlaceReward({26193}.StartPlaceIndex, {26193}.EndPlaceIndexInclusive, {26193}.Reward, {26193}.RewardId);
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x00122043 File Offset: 0x00120243
		public bool ShouldDisplayRewardIcon(RatingType {26187}, float {26188}, ArenaPlaceReward {26189})
		{
			return {26189}.RewardId != RatingRewardId.LuckyPlace || {26188} >= ArenaTournamentGameplay.MinRatingToHaveLuckyPlace;
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x0012205C File Offset: 0x0012025C
		public Form GetRewardIconForm(RatingRewardId {26190})
		{
			Rectangle rectangle;
			switch ({26190})
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
			case RatingRewardId.LuckyPlace:
				rectangle = {22782}.c_placeL;
				break;
			default:
				throw new NotSupportedException();
			}
			Rectangle {13190} = rectangle;
			return new Form(Vector2.Zero, {13190}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x001220B8 File Offset: 0x001202B8
		public ToolTipState CreateToolTipState(RatingRewardId {26191}, ComplexBonus {26192})
		{
			string text;
			switch ({26191})
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
			case RatingRewardId.LuckyPlace:
				text = Local.tournament_place_4;
				break;
			default:
				throw new NotSupportedException();
			}
			string {12528} = text;
			Composer composer = new Composer(350f, 0f);
			composer.AddText({12528}, new ComposerTextStyle(Color.White, false, Fonts.Philosopher_16, null), true);
			composer.AddText(Local.TavernaCommonUi_32, ComposerTextStyle.Gray, true);
			composer.AddSpace(10f);
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{19779}.ComplexRewardDisplayHelper({26192}, false, stackForm, new ComposerTextStyle(Color.White * 0.9f, false, null, null), null);
			composer.AddCustom(stackForm, PositionAlignment.Center);
			return new ToolTipState(composer);
		}
	}
}
