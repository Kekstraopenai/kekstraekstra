using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000502 RID: 1282
	internal class GameplayHelper
	{
		// Token: 0x06001CA3 RID: 7331 RVA: 0x00105BB8 File Offset: 0x00103DB8
		public static int MoveResources({21517} {25335}, bool {25336} = true)
		{
			if ({25335} == {21517}.ResToStorageManual || {25335} == {21517}.ResToStorageAuto)
			{
				int num = 0;
				foreach (ResourceInfo resourceInfo in ((IEnumerable<ResourceInfo>)Gameplay.ItemsInfo))
				{
					if ({25335} != {21517}.ResToStorageAuto || Global.Settings.AutomoveToStorageMendRes || (resourceInfo.ID != 1 && resourceInfo.ID != 3 && resourceInfo.ID != 36))
					{
						int num2 = Global.Player.ResourcesOfHold.GetCount((int)resourceInfo.ID);
						foreach (QuestRunningProgress questRunningProgress in ((IEnumerable<QuestRunningProgress>)Session.Account.Quests.ProgressRunningQuests))
						{
							QuestTransferOrder questTransferOrder = questRunningProgress.Info.Steps.Array[0] as QuestTransferOrder;
							if (questTransferOrder != null && questTransferOrder.ResourceID == (int)resourceInfo.ID)
							{
								num2 = Math.Max(0, num2 - questTransferOrder.ResourceCount.Value);
							}
						}
						if (num2 != 0)
						{
							Session.Account.NearPortStorage.AddOrRemove((int)resourceInfo.ID, num2);
							Global.Player.ResourcesOfHold.AddOrRemove((int)resourceInfo.ID, -num2);
							num += num2;
						}
					}
				}
				Global.Player.UpdateCapacity();
				if (num != 0 && {25336})
				{
					Global.Game.SoundSystem.PlaySound(GameStaticSoundName.ToHold, 0.03f, 1f);
				}
				return num;
			}
			if ({25335} == {21517}.ResToShip)
			{
				int totalItemsCount = Session.Account.NearPortStorage.GetTotalItemsCount();
				Global.Player.ResourcesOfHold.Add(Session.Account.NearPortStorage);
				Session.Account.NearPortStorage.Clean();
				Global.Player.UpdateCapacity();
				return totalItemsCount;
			}
			if ({25335} == {21517}.ArsenalToStorage)
			{
				Session.Account.CBallsAtStorage.Add(Global.Player.UsedShipPlayer.BallsOfHold);
				Global.Player.UsedShipPlayer.BallsOfHold.Clean();
				Session.Account.PowderKegsAtStorage.Add(Global.Player.UsedShipPlayer.PowderKegsOfHold);
				Global.Player.UsedShipPlayer.PowderKegsOfHold.Clean();
				Global.Player.UpdateCapacity();
				return 1;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00105E18 File Offset: 0x00104018
		public static void MakePortEnteringActions(bool {25337})
		{
			if (Global.Settings.Automending)
			{
				int num = Gameplay.MendingCostGoldTotal(Session.Account.Shipyard.CurrentRealShip, Session.Account, false);
				if ((Global.Player.UsedShip.FirstHP.Summary < Global.Player.UsedShip.MaxHp || !Global.Player.UsedShip.IsSailesFull) && (float)Session.Account.Gold > (float)num * 2f)
				{
					GameplayHelper.AutomendingApply();
					{19994}.MeAndLogbook({19988}.Info, Local.PortAllInterface_0(num), null);
					Session.Account.Gold -= num;
				}
			}
			Session.Account.CannonsAtStorage.Add(Session.Account.CannonsInHold);
			Session.Account.CannonsInHold.Clean();
			foreach (CannonGameInstance cannonGameInstance in Global.Player.UsedShip.Mortars.Iterate())
			{
				if (cannonGameInstance.RemainReserve == 0)
				{
					Global.Player.UsedShipPlayer.TakeOffMortar(Session.Account, cannonGameInstance);
				}
			}
			foreach (GSILocalEnumerablePair<UnitInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<UnitInfo>>)Session.Account.SpecialUnitsInHold.UnitInfo))
			{
				for (int i = 0; i < gsilocalEnumerablePair.Count; i++)
				{
					Session.Account.SpecialUnitsAtStorage.Add(gsilocalEnumerablePair.Info);
				}
			}
			Session.Account.SpecialUnitsInHold.Clean();
			if (Global.Player.UsedShip.StaticInfo.IsBalloon)
			{
				Global.Player.UsedShip.Crew.RemoveAllSpecialUnits(Session.Account);
			}
			WosbTreasuryMaps.DetachMaps(Session.Account);
			foreach (PlayerShipDynamicInfo playerShipDynamicInfo in Session.Account.Shipyard.List)
			{
				if (playerShipDynamicInfo.CannonsShouldMoveToStorage != null)
				{
					Session.Account.CannonsAtStorage.Add(playerShipDynamicInfo.CannonsShouldMoveToStorage);
					{19994}.MeAndLogbook({19988}.Info, Local.PortAllInterface_23(playerShipDynamicInfo.CannonsShouldMoveToStorage.GetTotalItemsCount()), null);
					playerShipDynamicInfo.CannonsShouldMoveToStorage = null;
				}
			}
			if (Global.Player.NearPortType == PortEnteringType.Port)
			{
				PlayerAccountPortsItems resourcesInPorts = Session.Account.ResourcesInPorts;
				Decorator game = Session.Game;
				resourcesInPorts.CheckWorkshops(game);
			}
			for (int j = 0; j < 3; j++)
			{
				PowerupItemInfo powerupItemInfo = Global.Player.UsedShipPlayer.PowerupItemSlots[j];
				if (powerupItemInfo != null && powerupItemInfo.DisallowInLowRangShips != null)
				{
					int rank = Global.Player.UsedShipPlayer.CraftFrom.Rank;
					int? disallowInLowRangShips = powerupItemInfo.DisallowInLowRangShips;
					if (rank > disallowInLowRangShips.GetValueOrDefault() & disallowInLowRangShips != null)
					{
						Global.Player.UsedShipPlayer.PowerupItemSlots[j] = null;
					}
				}
			}
			GameplayHelper.OperateHold();
			if (Global.Network.IsCacheSynchronized && Session.EventActionsPipeline.EventCategoryAmount(EActionCaterory.BTender) <= 0f)
			{
				foreach (GSI gsi in Session.Account.EnumerateAllResources(true, true, true, true))
				{
					gsi[25] = 0;
				}
			}
			GameplayHelper.CheckShipAndTakeActions();
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x001061C8 File Offset: 0x001043C8
		public static void OperateHold()
		{
			bool flag = Global.Player.NearPortType == PortEnteringType.Port || Global.Player.NearPortType == PortEnteringType.PersonalIsle;
			SavedShipEquipment savedShipEquipment;
			if (Global.Settings.SavedEquipment.TryFind((SavedShipEquipment {25343}) => {25343}.ShipInfoId == Global.Player.CraftFrom.ID && {25343}.Active, out savedShipEquipment))
			{
				savedShipEquipment.Apply(flag, true);
				return;
			}
			if (Global.Settings.AutomoveToStorage && flag)
			{
				int num = GameplayHelper.MoveResources({21517}.ResToStorageAuto, true);
				if (num != 0)
				{
					{19994}.MeAndLogbook({19988}.Info, Local.PortAllInterface_1(num), null);
				}
			}
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00106264 File Offset: 0x00104464
		public static void CheckCannons()
		{
			if (Global.Player.UsedShip.Cannons.Check(Session.Account))
			{
				{19994}.MeAndLogbook({19988}.Info, Local.wespons_checked, null);
			}
			foreach (CannonLocationInfo cannonLocationInfo in Global.Player.UsedShip.StaticInfo.MortarPorts)
			{
				CannonGameInstance cannonGameInstance = Global.Player.UsedShipPlayer.Mortars[(int)cannonLocationInfo.SectionID];
				if (cannonGameInstance != null && (cannonGameInstance.Info.Poundage.GetValueOrDefault(0) > Global.Player.UsedShipPlayer.CraftFrom.MaxMortarPoundage.GetValueOrDefault(0) || cannonLocationInfo.IsBlocked(Global.Player.UsedShip)))
				{
					Global.Player.UsedShipPlayer.TakeOffMortar(Session.Account, cannonLocationInfo);
				}
			}
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00106340 File Offset: 0x00104540
		public static void CheckDesigns()
		{
			if (Global.Player.UsedShip.StaticInfo.IsBalloon)
			{
				foreach (ShipDesignInfo shipDesignInfo in Global.Player.UsedShipPlayer.RemoveDesignElementsByCategory((ShipDesignCategory {25344}) => {25344} != ShipDesignCategory.ShipFullDesign))
				{
					GSI desingElementsAtStorage = Session.Account.DesingElementsAtStorage;
					int id = (int)shipDesignInfo.ID;
					int num = desingElementsAtStorage[id];
					desingElementsAtStorage[id] = num + 1;
				}
			}
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x001063E8 File Offset: 0x001045E8
		public static void CheckShipAndTakeActions()
		{
			GameplayHelper.AutoDropOffCrew();
			GameplayHelper.CheckUpgrades();
			GameplayHelper.CheckCannons();
			GameplayHelper.CheckUnreceivedSalary();
			GameplayHelper.CheckDesigns();
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x00106404 File Offset: 0x00104604
		public static void CheckUnreceivedSalary()
		{
			float elapsedTime = Session.Account.TimeAtSeaSec;
			bool flag;
			RTI salary = Global.Player.UsedShip.Crew.GetSpecialRequiredSalary(Session.Account, elapsedTime, out flag);
			if (salary.Value > 0)
			{
				if (Global.Settings.AutoPaySalarySpecialUnits && Session.Account.Gold > salary.Value)
				{
					Session.Account.Gold -= salary.Value;
					{19994}.MeAndLogbook({19988}.Info, Local.lbe_specialcrewpay(salary), null);
					return;
				}
				{17312} {17312} = new {17312}(Local.special_unit_salary_ask(salary.Value) + (flag ? (" " + Local.special_unit_salary_reduced) : ""), delegate(int {25355})
				{
					if ({25355} == 0)
					{
						Session.Account.Gold -= salary.Value;
						{19994}.MeAndLogbook({19988}.Info, Local.lbe_specialcrewpay(salary), null);
						return;
					}
					Global.Player.UsedShip.Crew.SpecialSalaryWasDenied(elapsedTime);
				}, new {17443}[]
				{
					new {17443}(Local.make_pay, (salary.Value > Session.Account.Gold) ? Local.gold_not_enough : "", {17312}.cIconAccept, salary.Value > Session.Account.Gold, 0f),
					new {17443}(Local.to_deny, Local.special_unit_salary_deny_tt, {17312}.cIconReject, false, 0f)
				});
				if (!Global.Settings.AutoPaySalarySpecialUnits)
				{
					{17312}.AddChildPos(new CheckboxControl(Vector2.Zero, CommonAtlas.vd_checkBox_26px_true, CommonAtlas.vd_checkBox_26px_false, false, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.PortEnteringSettingsWindow_16_b, Fonts.Philosopher_14, Color.Wheat * 0.7f).ExCheckEvent(delegate(CheckboxCheckedEventArgs {25345})
					{
						Global.Settings.AutoPaySalarySpecialUnits = {25345}.NewValue;
					}), PositionAlignment.Center, PositionAlignment.LeftUp, 255f);
				}
			}
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x001065F8 File Offset: 0x001047F8
		public static void CheckUpgrades()
		{
			InstalledShipUpgradeSlot[] array = Global.Player.UsedShipPlayer.Upgrades.GetUpgrades().ToArray<InstalledShipUpgradeSlot>();
			int num = 0;
			foreach (InstalledShipUpgradeSlot installedShipUpgradeSlot in array)
			{
				num++;
				bool flag = installedShipUpgradeSlot.Info.Info.IsUpgradeAvailableIn(Global.Player.UsedShipPlayer);
				if ((installedShipUpgradeSlot.Info.Info.CategoryUi != ShipUpgradeCategory.Sailes && installedShipUpgradeSlot.Info.Info.CategoryUi != ShipUpgradeCategory.Modification && !{22195}.CanBeInstalled(installedShipUpgradeSlot.Info.Info, true)) || !flag)
				{
					if (flag)
					{
						Session.Account.Shipyard.AddUpgrade(Global.Player.UsedShipPlayer.Upgrades.TakeOffUpgrade(Global.Player.UsedShipPlayer, installedShipUpgradeSlot.SlotIndex), Global.Player.UsedShipPlayer);
					}
				}
				else
				{
					int num2 = 4 - (Global.Player.UsedShipPlayer.MaxUpgradesCount - installedShipUpgradeSlot.SlotIndex);
					UpgradeSlotsResearch upResearch = Session.Account.Shipyard.GetUpResearch((int)Global.Player.CraftFrom.ID);
					if (num > Global.Player.UsedShipPlayer.MaxUpgradesCount || ((num2 == 1 || num2 == 2 || num2 == 3) && !upResearch.IsSlotOpened(num2)))
					{
						Session.Account.Shipyard.AddUpgrade(Global.Player.UsedShipPlayer.Upgrades.TakeOffUpgrade(Global.Player.UsedShipPlayer, installedShipUpgradeSlot.SlotIndex), Global.Player.UsedShipPlayer);
					}
				}
			}
			Global.Player.UsedShipPlayer.FirstHP.Summary = Math.Min(Global.Player.UsedShipPlayer.FirstHP.Summary, Global.Player.UsedShip.MaxHp);
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x001067D4 File Offset: 0x001049D4
		public static void AutomendingApply()
		{
			if (Global.Player.IsDestroyed)
			{
				Global.Player.Respawn(Global.Player.GetShipPositionInfo, RespawnHealthAmount.Full);
			}
			Global.Player.RestoreHp(Global.Player.UsedShip.MaxHp);
			Global.Player.RestoreSailes(1000f);
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0010682C File Offset: 0x00104A2C
		public static void AutoDropOffCrew()
		{
			if (Global.Network.IsCacheSynchronized)
			{
				int num = 0;
				while (Global.Player.UsedShip.Crew.Special.Size > Global.Player.UsedShip.Crew.MaxSpecialCrew(Session.Account))
				{
					Session.Account.SpecialUnitsAtStorage.Add(Global.Player.UsedShip.Crew.Special.Last());
					Global.Player.UsedShip.Crew.Special.Size--;
					num++;
				}
				if (Global.Player.UsedShip.Crew.Count > Global.Player.UsedShip.CrewPlaces)
				{
					GSI gsi = Global.Player.UsedShip.Crew.CutRandomExtraUnits(Global.Player.UsedShip.CrewPlaces);
					Session.Account.UnitsAtStorage.Add(gsi);
					num += gsi.GetTotalItemsCount();
				}
				if (num > 0)
				{
					{19994}.MeAndLogbook({19988}.Info, Local.PortAllInterface_2(num), null);
				}
			}
			if (Session.Account.Gold < 100 && Global.Player.UsedShip.Crew.Count == 0 && Session.Account.UnitsAtStorage.IsEmpty)
			{
				Global.Player.UsedShip.Crew.Add(Gameplay.UnitsInfo.FromID(2), 5, false);
			}
			foreach (UnitInfo unitInfo in Global.Player.UsedShipPlayer.Crew.CutEndOfServiceSpecial())
			{
				{19994}.MeAndLogbook({19988}.InfoRed, Local.PortAllInterface_2b(unitInfo.Name), null);
			}
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x00106A0C File Offset: 0x00104C0C
		public static void AutomoveResources()
		{
			int num = 0;
			foreach (PlayerShipDynamicInfo playerShipDynamicInfo in Session.Account.Shipyard.List)
			{
				if (playerShipDynamicInfo != Global.Player.UsedShip && !playerShipDynamicInfo.PrivateResourcesOfHold.IsEmpty)
				{
					foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)playerShipDynamicInfo.PrivateResourcesOfHold.ResourceInfo))
					{
						GSI nearPortStorage = Session.Account.NearPortStorage;
						int id = (int)gsilocalEnumerablePair.Info.ID;
						nearPortStorage[id] += gsilocalEnumerablePair.Count;
						num += gsilocalEnumerablePair.Count;
					}
					playerShipDynamicInfo.PrivateResourcesOfHold.Clean();
				}
			}
			if (num > 0)
			{
				{19994}.MeAndLogbook({19988}.Info, Local.lbe_resMoved(num), null);
			}
			GSI gsi = new GSI();
			foreach (GSILocalEnumerablePair<PowderKegInfo> gsilocalEnumerablePair2 in ((IEnumerable<GSILocalEnumerablePair<PowderKegInfo>>)Global.Player.UsedShipPlayer.PowderKegsOfHold.PowderKegInfo))
			{
				if (Global.Player.UsedShipPlayer.CraftFrom.Rank > gsilocalEnumerablePair2.Info.AvailableSinceRank.GetValueOrDefault(100))
				{
					gsi.Exs((int)gsilocalEnumerablePair2.Info.ID, gsilocalEnumerablePair2.Count);
				}
			}
			Global.Player.UsedShipPlayer.PowderKegsOfHold.Remove(gsi, 1, false);
			Session.Account.PowderKegsAtStorage.Add(gsi);
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x00106C00 File Offset: 0x00104E00
		public static void PortLeave(Action {25338}, bool {25339}, bool {25340})
		{
			GameplayHelper.CheckShipAndTakeActions();
			GameplayHelper.AutomoveResources();
			bool flag = Global.Player.NearPortType == PortEnteringType.Port;
			bool flag2 = Global.Player.NearPortType == PortEnteringType.PersonalIsle;
			if ({25339} && flag)
			{
				foreach (QuestRunningProgress questRunningProgress in ((IEnumerable<QuestRunningProgress>)Session.Account.Quests.ProgressRunningQuests))
				{
					QuestTransferOrder questTransferOrder = questRunningProgress.Info.FirstStep as QuestTransferOrder;
					if (questTransferOrder != null && questRunningProgress.Info.Company == QuestCompany.War && (Global.Player.ResourcesOfHold[(int)questTransferOrder.ResInfo.ID] < questTransferOrder.ResourceCount.Value | {25340}))
					{
						new {17312}(Local.transferQuestError(questTransferOrder.ResInfo.Name));
						return;
					}
				}
				foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)Session.Account.NearPortStorage.ResourceInfo))
				{
					if (gsilocalEnumerablePair.Info.CannotBeStoredInPort && gsilocalEnumerablePair.Info.ID != 8)
					{
						new {17312}(Local.PortAllInterface_22(gsilocalEnumerablePair.Info.Name));
						return;
					}
				}
				if ((Global.Player.NearPortType != PortEnteringType.Port || Global.Player.NearPort.Type != PortType.PirateBay) && !Session.Game.NearPortAllowBondmanTrade)
				{
					if (!Session.Account.StorageRent.Any((ActiveStrageRent {25346}) => (int){25346}.PortID == Global.Player.NearPort.PortID))
					{
						Action<int> <>9__3;
						foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair2 in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)Session.Account.NearPortStorage.ResourceInfo))
						{
							if (gsilocalEnumerablePair2.Info.CannotBeStoredInPort && gsilocalEnumerablePair2.Info.ID == 8 && gsilocalEnumerablePair2.Count > 25)
							{
								string portAllInterface_22_b = Local.PortAllInterface_22_b;
								Action<int> {17375};
								if (({17375} = <>9__3) == null)
								{
									{17375} = (<>9__3 = delegate(int {25353})
									{
										if ({25353} == 2)
										{
											return;
										}
										if ({25353} == 0)
										{
											GSI resourcesOfHold = Global.Player.ResourcesOfHold;
											resourcesOfHold[8] = resourcesOfHold[8] + (Session.Account.NearPortStorage[8] - 25);
										}
										Session.Account.NearPortStorage[8] = 25;
										GameplayHelper.PortLeave({25338}, {25339}, {25340});
									});
								}
								new {17312}(portAllInterface_22_b, {17375}, new string[]
								{
									Local.bondman_move_ship,
									Local.HoldsUiCommon_1b,
									Local.to_back
								});
								return;
							}
						}
					}
				}
			}
			if ({25339} && Global.Player.NearPortType != PortEnteringType.Miniport)
			{
				if (!{25340})
				{
					foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair3 in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)Global.Player.UsedShipPlayer.PrivateResourcesOfHold.ResourceInfo))
					{
						if (gsilocalEnumerablePair3.Info.CantTransferWithPeacefulFlag && Session.Account.WorldFlag.Mapback() == OpenWorldFlag.Peaceful)
						{
							new {17312}(Local.ResourceOrItemToolTipHelper_4_k(gsilocalEnumerablePair3.Info.Name));
							return;
						}
					}
				}
				foreach (AllyStateTransfer allyStateTransfer in ((IEnumerable<AllyStateTransfer>)Session.LastMinimapAndGroupUpdate.allies))
				{
					if (allyStateTransfer.ShipClass == 255 && allyStateTransfer.IsOneMap)
					{
						CapturedShipShortInfo capturedShipInfo = allyStateTransfer.CapturedShipInfo;
						if (!capturedShipInfo.FlagsForgotten && allyStateTransfer.CapturedShipInfo.HoldLoadWeight > CapturedShipIntance.GetMaxHoldLoad(Gameplay.NpcsInfo.FromID(allyStateTransfer.CapturedShipInfoId).BasedOn, Session.Account.WorldFlag))
						{
							new {17312}(Local.captured_ship_hold_overload(allyStateTransfer.FetchName(null)));
							return;
						}
					}
				}
			}
			if ({25339})
			{
				if (!{25340} && flag2)
				{
					byte rank = (byte)Global.Player.UsedShipPlayer.CraftFrom.Rank;
					WorldBitmap shallows = Global.Player.MapInfo.Shallows;
					Vector2 position = Global.Player.Position;
					if (rank < shallows.Get(position))
					{
						WorldBitmap shallows2 = Global.Player.MapInfo.Shallows;
						position = Global.Player.Position;
						new {17312}(Local.shallow_error(shallows2.Get(position)));
						return;
					}
				}
				if (!{25340} && flag && Global.Player.UsedShipPlayer.CraftFrom.Rank < Global.Player.NearPort.ShallowUpperRang)
				{
					new {17312}(Local.shallow_error(Global.Player.NearPort.ShallowUpperRang), delegate(int {25347})
					{
						if ({25347} == 0)
						{
							new {22094}(delegate(ValueTuple<object, {22094}.Mode> {25348})
							{
								{22094}.MakeWorldTravel(true, {25348}.Item2, {25348}.Item1);
							}, {22094}.Mode.SelectNearTravelPort);
						}
					}, new {17443}[]
					{
						new {17443}(Local.open_travel, "", {17312}.cIconSpyglass, false, 0f),
						new {17443}(Local.close_window, "", {17312}.cIconReject, false, 0f)
					});
					return;
				}
				if (flag && Session.Account.WarehouseOverloadTest(Global.Player.NearPort, null))
				{
					new {17312}(Local.PortAllInterface_4, delegate(int {25349})
					{
						if ({25349} == 0 && {17745}.CurrentInstance == null)
						{
							new {17745}();
						}
						if ({25349} == 1)
						{
							{22467}.OpenStorageRent();
						}
					}, new string[]
					{
						Local.storage_d.Replace(": ", ""),
						Local.rent,
						Local.close
					});
					return;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			PlayerShipDynamicInfo usedShipPlayer = Global.Player.UsedShipPlayer;
			if (usedShipPlayer.FirstHP.Summary == 0f && (flag2 || flag))
			{
				stringBuilder.AppendLine(Local.PortAllInterface_5);
			}
			else if (Session.Account.Gold < 0 & {25339})
			{
				stringBuilder.AppendLine(Local.gold_minus);
			}
			else if (usedShipPlayer.ClientTimeToRestoreIntegrity > 0f)
			{
				stringBuilder.AppendLine(Local.PortAllInterface_6);
			}
			else if (flag && usedShipPlayer.IntegrityIsDestroyed && !Session.Account.WorldFlag.IsPeaceMode() && flag)
			{
				stringBuilder.AppendLine(Local.PortAllInterface_12_B);
			}
			else if (usedShipPlayer.Crew.Count == 0 && (flag2 || flag))
			{
				stringBuilder.AppendLine(Local.PortAllInterface_17);
			}
			else if ((flag || flag2) && usedShipPlayer.GetItemsMass() > usedShipPlayer.Capacity && Session.Account.WorldFlag == OpenWorldFlag.Peaceful && !Session.Account.IsPeaceActivated)
			{
				stringBuilder.AppendLine(Local.PortAllInterface_13c);
			}
			else if ((flag || flag2) && usedShipPlayer.GetItemsMass() > usedShipPlayer.Capacity * 2.5f)
			{
				stringBuilder.AppendLine(Local.PortAllInterface_13b);
			}
			if (stringBuilder.Length > 0)
			{
				new {17312}(stringBuilder.ToString().Replace(Environment.NewLine, ";"));
				return;
			}
			if ((flag2 || flag) && !{25340} && !Global.Player.UsedShip.StaticInfo.IsBalloon)
			{
				if ({25339})
				{
					int num = 0;
					foreach (QuestRunningProgress questRunningProgress2 in ((IEnumerable<QuestRunningProgress>)Session.Account.Quests.ProgressRunningQuests))
					{
						foreach (string value in QuestHelper.GetQuestFailedRistrictions(questRunningProgress2.Info))
						{
							if (num++ == 0)
							{
								stringBuilder.AppendLine(Local.PortAllInterface_11);
							}
							stringBuilder.AppendLine(value);
						}
					}
				}
				if (usedShipPlayer.GetItemsMass() > usedShipPlayer.Capacity)
				{
					stringBuilder.AppendLine(Local.PortAllInterface_13);
				}
				if (usedShipPlayer.Cannons.Items.Size == 0)
				{
					stringBuilder.AppendLine(Local.PortAllInterface_14);
				}
				if (usedShipPlayer.BallsOfHold.IsEmpty)
				{
					stringBuilder.AppendLine(Local.PortAllInterface_15);
				}
				else if (usedShipPlayer.BallsOfHold[1] + usedShipPlayer.BallsOfHold[2] < 100 && Session.Account.Rang <= 6)
				{
					stringBuilder.AppendLine(Local.PortAllInterface_15_low);
				}
				else if ((float)usedShipPlayer.Crew.CountOfSailors < (float)usedShipPlayer.NeedSailors * 0.4f && (flag2 || flag) && !Session.EducState_CrewRatioToolTipShown)
				{
					Session.EducState_CrewRatioToolTipShown = true;
					stringBuilder.AppendLine(Local.PortAllInterface_17b);
				}
				else if ((float)usedShipPlayer.Crew.CountOfBoardingUnits < (float)usedShipPlayer.CrewPlaces * 0.3f && (flag2 || flag) && Session.Account.Rang <= 10 && !Session.EducState_CrewRatioToolTipShown)
				{
					Session.EducState_CrewRatioToolTipShown = true;
					stringBuilder.AppendLine(Local.PortAllInterface_17с);
				}
				else
				{
					if (usedShipPlayer.Mortars.Count > 0)
					{
						if (usedShipPlayer.Mortars.Iterate().All((CannonGameInstance {25350}) => {25350}.Info.Feature != CannonFeature.PowderKegMortar))
						{
							if (usedShipPlayer.BallsOfHold.CannonBallInfo.All((GSILocalEnumerablePair<CannonBallInfo> {25351}) => {25351}.Info.AmmoType != CannonAmmoType.MortarBall))
							{
								stringBuilder.AppendLine(Local.PortAllInterface_18);
								goto IL_A1B;
							}
						}
					}
					if (usedShipPlayer.Mortars.Count > 0)
					{
						if (usedShipPlayer.Mortars.Iterate().Any((CannonGameInstance {25352}) => {25352}.Info.Feature == CannonFeature.PowderKegMortar) && usedShipPlayer.PowderKegsOfHold[MortarShot.PowderKegMortarType] <= 2)
						{
							stringBuilder.AppendLine(Local.PortAllInterface_18_powderKeg);
							goto IL_A1B;
						}
					}
					if (Session.Account.Rang < 4 && Global.Player.ResourcesOfHold[1] == 0 && (EducationHelper.MakeTravelAvailable || Session.Account.NearPortStorage[1] > 0))
					{
						stringBuilder.AppendLine(Local.PortAllInterface_19);
					}
				}
				IL_A1B:
				string text;
				string text2;
				if (!Session.Game.CanEnterNearPortWithFlags(Session.Account.WorldFlag, out text, null) && Session.Game.CanPickFlagsInNearPort(Session.Account.WorldFlag, false, out text2))
				{
					stringBuilder.AppendLine(Local.PortAllInterface_flagsErr(Session.Account.WorldFlag.ToStringLocalShort()));
				}
				if (stringBuilder.Length > 0)
				{
					new {17312}(Local.PortAllInterface_21 + Environment.NewLine + stringBuilder.ToString(), delegate(int {25354})
					{
						if ({25354} == 0)
						{
							{25338}();
						}
					}, new {17443}[]
					{
						new {17443}(Local.PortAllInterface_9, "", {17312}.cIconReject, false, 0f),
						new {17443}(Local.PortAllInterface_10, "", {17312}.cIconAccept, false, 0f)
					});
					return;
				}
			}
			{25338}();
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0010775C File Offset: 0x0010595C
		public static bool IsDailyQuestVisible(QuestInfo {25341})
		{
			int num;
			return EducationHelper.DailyQuestsVisible && {25341}.MinimalRank <= Session.Account.Rang && (!{25341}.NeedDailyQuestsTier2 || EducationHelper.DailyQuestsTier2Visible) && {25341}.TreasuryMapID == null && !Session.Account.Quests.CheckDisable({25341}.ID, out num);
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x001077BC File Offset: 0x001059BC
		public static void AutostartDailyQuestsAndShowMessages(PlayerAccount {25342})
		{
			int currentQuestDay = CalendarEvents.CurrentEvent.GetCurrentQuestDay({25342});
			foreach (QuestRunningProgress questRunningProgress in ((IEnumerable<QuestRunningProgress>)Session.Account.Quests.ProgressRunningQuests))
			{
				if (questRunningProgress.Info.CalendarQuestDay != null)
				{
					int? calendarQuestDay = questRunningProgress.Info.CalendarQuestDay;
					int num = currentQuestDay;
					if (!(calendarQuestDay.GetValueOrDefault() == num & calendarQuestDay != null))
					{
						Global.Network.Send(new OnGetQuestMsg(questRunningProgress.QuestID, QuestAction.Undo, -1, null));
					}
				}
			}
			foreach (QuestInfo questInfo in ((IEnumerable<QuestInfo>)Gameplay.QuestsInfo))
			{
				if (questInfo.Company == QuestCompany.Daily && GameplayHelper.IsDailyQuestVisible(questInfo) && (questInfo.CalendarQuestDay == null || questInfo.CalendarQuestDay.Value == currentQuestDay) && Session.Account.Quests.TrySearchProgress(questInfo.ID) == null)
				{
					Global.Network.Send(new OnGetQuestMsg(questInfo.ID, QuestAction.Get, -1, null));
				}
			}
		}

		// Token: 0x04001C6C RID: 7276
		public static bool EnableNextHoldManagement;
	}
}
