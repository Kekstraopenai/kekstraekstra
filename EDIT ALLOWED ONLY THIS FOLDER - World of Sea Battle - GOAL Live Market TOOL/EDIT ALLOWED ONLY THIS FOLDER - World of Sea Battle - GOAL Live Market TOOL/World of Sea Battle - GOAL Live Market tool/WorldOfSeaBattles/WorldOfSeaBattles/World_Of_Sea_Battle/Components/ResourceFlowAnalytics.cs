using System;
using System.Collections.Generic;
using Common.Game;
using Common.Packets;
using Common.Resources;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000520 RID: 1312
	public class ResourceFlowAnalytics
	{
		// Token: 0x06001D54 RID: 7508 RVA: 0x0010AC30 File Offset: 0x00108E30
		private GSI {25513}()
		{
			GSI gsi = Session.Account.CannonsAtStorage.Clone();
			gsi.Add(Session.Account.CannonsInHold);
			foreach (CannonGameInstance cannonGameInstance in ((IEnumerable<CannonGameInstance>)Session.Account.UsedMortarsAtStorage))
			{
				GSI gsi2 = gsi;
				int infoID = (int)cannonGameInstance.InfoID;
				int num = gsi2[infoID];
				gsi2[infoID] = num + 1;
			}
			return gsi;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0010ACB8 File Offset: 0x00108EB8
		public void Sync()
		{
			GSI gsi = this.{25513}();
			GSI allResources = Session.Account.GetAllResources();
			int gold = Session.Account.Gold;
			if (this.{25518} == null)
			{
				this.{25518} = gsi;
				this.{25519} = allResources;
				this.{25517} = gold;
				return;
			}
			foreach (CannonGameInfo cannonGameInfo in ((IEnumerable<CannonGameInfo>)Gameplay.CannonsGameInfo))
			{
				int num = this.{25518}[(int)cannonGameInfo.ID];
				int num2 = gsi[(int)cannonGameInfo.ID];
				this.{25514}.AddDelta((int)cannonGameInfo.ID, num2 - num);
			}
			foreach (ResourceInfo resourceInfo in ((IEnumerable<ResourceInfo>)Gameplay.ItemsInfo))
			{
				int num3 = this.{25519}[(int)resourceInfo.ID];
				int num4 = allResources[(int)resourceInfo.ID];
				this.{25515}.AddDelta((int)resourceInfo.ID, num4 - num3);
			}
			this.{25516}.AddDelta(1, gold - this.{25517});
			this.{25518} = gsi;
			this.{25519} = allResources;
			this.{25517} = gold;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0010AE10 File Offset: 0x00109010
		public OnAnalyticsResourcesSaldo? Peek()
		{
			if (this.{25514}.HasDelta || this.{25515}.HasDelta || this.{25516}.HasDelta)
			{
				OnAnalyticsResourcesSaldo value = new OnAnalyticsResourcesSaldo(this.{25515}.Positive, this.{25515}.Negative, this.{25514}.Positive, this.{25514}.Negative, this.{25516}.Positive[1], this.{25516}.Negative[1]);
				this.{25515} = new ResourceFlowAnalytics.Saldo();
				this.{25514} = new ResourceFlowAnalytics.Saldo();
				this.{25516} = new ResourceFlowAnalytics.Saldo();
				return new OnAnalyticsResourcesSaldo?(value);
			}
			return null;
		}

		// Token: 0x04001CD4 RID: 7380
		private ResourceFlowAnalytics.Saldo {25514} = new ResourceFlowAnalytics.Saldo();

		// Token: 0x04001CD5 RID: 7381
		private ResourceFlowAnalytics.Saldo {25515} = new ResourceFlowAnalytics.Saldo();

		// Token: 0x04001CD6 RID: 7382
		private ResourceFlowAnalytics.Saldo {25516} = new ResourceFlowAnalytics.Saldo();

		// Token: 0x04001CD7 RID: 7383
		private int {25517};

		// Token: 0x04001CD8 RID: 7384
		private GSI {25518};

		// Token: 0x04001CD9 RID: 7385
		private GSI {25519};

		// Token: 0x02000521 RID: 1313
		private class Saldo
		{
			// Token: 0x170002B7 RID: 695
			// (get) Token: 0x06001D58 RID: 7512 RVA: 0x0010AEF0 File Offset: 0x001090F0
			public bool HasDelta
			{
				get
				{
					return !this.Positive.IsEmpty || !this.Negative.IsEmpty;
				}
			}

			// Token: 0x06001D59 RID: 7513 RVA: 0x0010AF10 File Offset: 0x00109110
			public void AddDelta(int {25520}, int {25521})
			{
				if ({25521} > 0)
				{
					GSI gsi = this.Positive;
					gsi[{25520}] += {25521};
				}
				if ({25521} < 0)
				{
					GSI gsi = this.Negative;
					gsi[{25520}] += Math.Abs({25521});
				}
			}

			// Token: 0x04001CDA RID: 7386
			public GSI Positive = new GSI();

			// Token: 0x04001CDB RID: 7387
			public GSI Negative = new GSI();
		}
	}
}
