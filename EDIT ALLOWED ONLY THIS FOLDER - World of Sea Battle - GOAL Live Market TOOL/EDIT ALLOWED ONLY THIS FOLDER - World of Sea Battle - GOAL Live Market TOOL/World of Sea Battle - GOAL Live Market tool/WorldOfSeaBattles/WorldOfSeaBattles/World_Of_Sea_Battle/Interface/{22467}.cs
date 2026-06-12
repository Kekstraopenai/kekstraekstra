using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using TheraEngine;
using TheraEngine.Collections;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020003BC RID: 956
	public static class {22467}
	{
		// Token: 0x060014D2 RID: 5330 RVA: 0x000AF628 File Offset: 0x000AD828
		public static void OpenStorageBuildOrUpgrade()
		{
			{22467}.<>c__DisplayClass0_0 CS$<>8__locals1 = new {22467}.<>c__DisplayClass0_0();
			CS$<>8__locals1.currentLevel = Session.Account.ResourcesInPorts.GetStorageLevel(Global.Player.NearPort.PortID);
			if (CS$<>8__locals1.currentLevel >= WosbStorages.Port.Length - 1)
			{
				return;
			}
			CS$<>8__locals1.nextStorage = WosbStorages.Port[CS$<>8__locals1.currentLevel + 1];
			if (CS$<>8__locals1.currentLevel == 0)
			{
				int num = Session.Account.MaxWarehousesCount - Session.Account.ResourcesInPorts.CountBuiltWarehouses();
				if (num <= 0)
				{
					new {17312}(Local.HoldInterfaceCommon_15(Session.Account.MaxWarehousesCount));
					return;
				}
				CS$<>8__locals1.<OpenStorageBuildOrUpgrade>g__OpenCraftWindow|0(Local.build_warehouse(WosbStorages.Port[1].Capacity.Value / 1000, num));
				return;
			}
			else
			{
				string text = Local.upgrade_warehouse(WosbStorages.Port[CS$<>8__locals1.currentLevel].Capacity.Value / 1000, CS$<>8__locals1.nextStorage.Capacity.Value / 1000);
				if (Session.Account.NearPortStorage[33] > 0 && CS$<>8__locals1.currentLevel < WosbStorages.Port.Length - 2)
				{
					new {17312}(Local.askInsuranePaper_a(Session.Account.NearPortStorage[33]), delegate()
					{
						base.<OpenStorageBuildOrUpgrade>g__MakeBuildOrUpgrade|1(false);
					}, delegate()
					{
						CS$<>8__locals1.<OpenStorageBuildOrUpgrade>g__OpenCraftWindow|0(text);
					});
					return;
				}
				CS$<>8__locals1.<OpenStorageBuildOrUpgrade>g__OpenCraftWindow|0(text);
				return;
			}
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x000AF7D8 File Offset: 0x000AD9D8
		public static void OpenStorageRemoving()
		{
			IslePortInfo nearPort = Global.Player.NearPort;
			int num = WosbStorages.Port.First<StorageLevelInfo>().Capacity.Value + Session.Account.StorageRent.Sum(delegate(ActiveStrageRent {22476})
			{
				if ((int){22476}.PortID != nearPort.PortID)
				{
					return 0;
				}
				return {22476}.CapacityBonus;
			});
			if (!Session.Game.IsExtraWorkshopAvailable(nearPort, false, true) && Session.Account.ResourcesInPorts.WriteAccess(nearPort.PortID).OpenedFactory.Size >= Session.Game.MaxCountOfWorkshops)
			{
				new {17312}(Local.dest_strage_error2);
				return;
			}
			if (Session.Account.NearPortStorage.ComputeMass<ResourceInfo>() > (float)num)
			{
				new {17312}(Local.dest_strage_error1(num / 1000));
				return;
			}
			new {17312}(Local.HoldInterfaceCommon_14 + "?", delegate()
			{
				Session.Account.ResourcesInPorts.WriteAccess(nearPort.PortID).StorageLevel = 0;
				{17745} currentInstance = {17745}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.ExternalUpdate();
			}, delegate()
			{
			});
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x000AF8F0 File Offset: 0x000ADAF0
		public static void OpenStorageRent()
		{
			List<string> list = (from {22471} in WosbStorages.RentOptions
			select {22467}.<OpenStorageRent>g__GetText|2_0({22471}.Item1.Value, {22471}.Item2.Value)).ToList<string>();
			list.Add(Local.close);
			new {17107}(Local.storage_rent, Local.storage_rent_tt(14), Local.storage_rent_tt_bondman, delegate(int {22472})
			{
				if ({22472} >= WosbStorages.RentOptions.Length)
				{
					return;
				}
				ValueTuple<RTI, RTI> option = WosbStorages.RentOptions[{22472}];
				{17312}.AskPrice(Local.storage_rent_accept(option.Item1.Value / 1000), option.Item2, delegate
				{
					{19994}.Logbook(Local.lbe_bought_general(Local.storage_rent, StringHelper.BigValueHelper(option.Item2.Value) + " " + Local.gold2), LBFlags.L0);
					Tlist<ActiveStrageRent> storageRent = Session.Account.StorageRent;
					ActiveStrageRent activeStrageRent = new ActiveStrageRent((byte){22472}, (byte)Global.Player.NearPort.PortID, 1209600.0);
					storageRent.Add(activeStrageRent);
					{17745} currentInstance = {17745}.CurrentInstance;
					if (currentInstance == null)
					{
						return;
					}
					currentInstance.ExternalUpdate();
				}, true);
			}, true, null, list.ToArray());
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x000AF97C File Offset: 0x000ADB7C
		[CompilerGenerated]
		internal static string <OpenStorageRent>g__GetText|2_0(int {22468}, int {22469})
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
			defaultInterpolatedStringHandler.AppendLiteral("+");
			defaultInterpolatedStringHandler.AppendFormatted<int>({22468} / 1000);
			defaultInterpolatedStringHandler.AppendFormatted(Local.tonn);
			defaultInterpolatedStringHandler.AppendLiteral(" ");
			string value = defaultInterpolatedStringHandler.ToStringAndClear();
			ActiveStrageRent activeStrageRent;
			if (Session.Account.StorageRent.TryFind((ActiveStrageRent {22477}) => (int){22477}.PortID == Global.Player.NearPort.PortID && {22477}.CapacityBonus == {22468}, out activeStrageRent))
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(5, 3);
				defaultInterpolatedStringHandler2.AppendLiteral("X");
				defaultInterpolatedStringHandler2.AppendFormatted(value);
				defaultInterpolatedStringHandler2.AppendLiteral(" (");
				defaultInterpolatedStringHandler2.AppendFormatted(Local.remain);
				defaultInterpolatedStringHandler2.AppendLiteral(" ");
				defaultInterpolatedStringHandler2.AppendFormatted(StringHelper.TimeD(activeStrageRent.RemainSec));
				defaultInterpolatedStringHandler2.AppendLiteral(")");
				return defaultInterpolatedStringHandler2.ToStringAndClear();
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(5, 3);
			defaultInterpolatedStringHandler3.AppendFormatted(value);
			defaultInterpolatedStringHandler3.AppendLiteral(" (-");
			defaultInterpolatedStringHandler3.AppendFormatted(StringHelper.BigValueHelper({22469}));
			defaultInterpolatedStringHandler3.AppendLiteral(" ");
			defaultInterpolatedStringHandler3.AppendFormatted(Local.gold2);
			defaultInterpolatedStringHandler3.AppendLiteral(")");
			return defaultInterpolatedStringHandler3.ToStringAndClear();
		}
	}
}
