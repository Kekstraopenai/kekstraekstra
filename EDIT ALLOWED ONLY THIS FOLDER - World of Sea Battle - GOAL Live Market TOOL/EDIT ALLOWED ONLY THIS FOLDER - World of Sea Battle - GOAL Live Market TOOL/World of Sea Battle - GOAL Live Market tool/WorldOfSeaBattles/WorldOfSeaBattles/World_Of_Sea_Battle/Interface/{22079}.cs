using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Common.Resources;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000384 RID: 900
	internal class {22079}
	{
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06001392 RID: 5010 RVA: 0x000A5928 File Offset: 0x000A3B28
		// (remove) Token: 0x06001393 RID: 5011 RVA: 0x000A5960 File Offset: 0x000A3B60
		public event Action SeeTargetChanged
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{22091};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{22091}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{22091};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{22091}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06001394 RID: 5012 RVA: 0x000A5998 File Offset: 0x000A3B98
		// (remove) Token: 0x06001395 RID: 5013 RVA: 0x000A59D0 File Offset: 0x000A3BD0
		public event Action AddNewShip
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{22092};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{22092}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{22092};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{22092}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06001396 RID: 5014 RVA: 0x000A5A08 File Offset: 0x000A3C08
		// (remove) Token: 0x06001397 RID: 5015 RVA: 0x000A5A40 File Offset: 0x000A3C40
		public event Action RemoveShip
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{22093};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{22093}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{22093};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{22093}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x000A5A75 File Offset: 0x000A3C75
		public void SelectAndSetAsViewCurrentShip()
		{
			this.See(Session.Account.Shipyard.CurrentRealShip);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x000A5A8C File Offset: 0x000A3C8C
		public void Add(PlayerShipDynamicInfo {22086})
		{
			Action action = this.{22092};
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x000A5A9E File Offset: 0x000A3C9E
		public void Remove(PlayerShipDynamicInfo {22087})
		{
			if (Session.Account.Shipyard.CurrentRealShip == {22087})
			{
				throw new InvalidOperationException();
			}
			Session.Account.Shipyard.RemoveFromList({22087});
			Action action = this.{22093};
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x000A5AD8 File Offset: 0x000A3CD8
		public void See(PlayerShipDynamicInfo {22088})
		{
			if (Session.Account.Shipyard.HasShip({22088}))
			{
				throw new InvalidOperationException("PortShipInfoHolder: not exists in list");
			}
			if (Session.Account.Shipyard.CurrentRealShip != {22088})
			{
				GameplayHelper.EnableNextHoldManagement = true;
				{22279} currentInstance = {22279}.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.RemoveFromContainer();
				}
			}
			if (Global.Player.UsedShipPlayer != {22088})
			{
				Session.Account.Shipyard.Select({22088});
				Global.Player.ForceUpdateShipEffects();
			}
			else
			{
				Global.Player.ForceUpdateShipEffects();
			}
			{22088}.TrophyShipNotification = false;
			Action action = this.{22091};
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x000A5B74 File Offset: 0x000A3D74
		public void ExchangeToOther(PlayerShipDynamicInfo {22089}, bool {22090})
		{
			PlayerShipDynamicInfo playerShipDynamicInfo = null;
			for (int i = 0; i < Session.Account.Shipyard.List.Count; i++)
			{
				if (Session.Account.Shipyard.List[i] == {22089})
				{
					playerShipDynamicInfo = Session.Account.Shipyard.List[i + ((i == 0) ? 1 : -1)];
					break;
				}
			}
			if (playerShipDynamicInfo == null)
			{
				throw new InvalidOperationException();
			}
			if ({22090})
			{
				this.See(playerShipDynamicInfo);
			}
			this.See(playerShipDynamicInfo);
		}

		// Token: 0x040011C6 RID: 4550
		[CompilerGenerated]
		private Action {22091};

		// Token: 0x040011C7 RID: 4551
		[CompilerGenerated]
		private Action {22092};

		// Token: 0x040011C8 RID: 4552
		[CompilerGenerated]
		private Action {22093};
	}
}
