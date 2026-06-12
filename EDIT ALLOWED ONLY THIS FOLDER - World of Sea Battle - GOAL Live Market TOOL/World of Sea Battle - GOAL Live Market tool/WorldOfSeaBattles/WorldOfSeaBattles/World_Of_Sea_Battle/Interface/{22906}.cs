using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200040A RID: 1034
	public class {22906}
	{
		// Token: 0x0600167D RID: 5757 RVA: 0x000BC9D0 File Offset: 0x000BABD0
		public static {22906}.BestOrders GetBestTradingWays(Tlist<PortLiveTrading> {22907}, float {22908} = 4000f, float {22909} = 1000000f)
		{
			{22906}.<>c__DisplayClass1_0 CS$<>8__locals1 = new {22906}.<>c__DisplayClass1_0();
			CS$<>8__locals1.maxHold = {22909};
			CS$<>8__locals1.data = new {22906}.BestOrders();
			CS$<>8__locals1.myPort = Global.Player.NearPort;
			PortLiveTrading portLiveTrading = {22907}.FirstOrDefault((PortLiveTrading {22910}) => {22910}.PortInstance == CS$<>8__locals1.myPort);
			if (portLiveTrading == null)
			{
				return CS$<>8__locals1.data;
			}
			foreach (PortLiveTrading portLiveTrading2 in ((IEnumerable<PortLiveTrading>){22907}))
			{
				if (portLiveTrading != portLiveTrading2 && portLiveTrading2.PortInstance != null)
				{
					IslePortInfo portInstance = portLiveTrading2.PortInstance;
					if (Vector2.Distance(portInstance.EntryPos, CS$<>8__locals1.myPort.EntryPos) <= {22908} || CS$<>8__locals1.myPort.PortsToJump.Contains(portInstance) || portInstance.PortsToJump.Contains(CS$<>8__locals1.myPort))
					{
						CS$<>8__locals1.<GetBestTradingWays>g__AddPair|1(portLiveTrading, portLiveTrading2);
						CS$<>8__locals1.<GetBestTradingWays>g__AddPair|1(portLiveTrading2, portLiveTrading);
					}
				}
			}
			return CS$<>8__locals1.data;
		}

		// Token: 0x0200040B RID: 1035
		[TupleElementNames(new string[]
		{
			"from",
			"to",
			"res",
			"count",
			"profit"
		})]
		public class BestOrders : Tlist<ValueTuple<IslePortInfo, IslePortInfo, ResourceInfo, int, float>>
		{
		}
	}
}
