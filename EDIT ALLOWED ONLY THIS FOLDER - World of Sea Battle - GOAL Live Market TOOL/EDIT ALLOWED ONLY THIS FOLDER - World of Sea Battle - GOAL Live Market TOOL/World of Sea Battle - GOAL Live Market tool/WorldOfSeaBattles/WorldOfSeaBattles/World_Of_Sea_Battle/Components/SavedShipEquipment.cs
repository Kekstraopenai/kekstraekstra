using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using ManualPacketSerialization;
using ManualPacketSerialization.Externs;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000524 RID: 1316
	public class SavedShipEquipment : IMPSerializable
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0010B1EF File Offset: 0x001093EF
		public bool IsFullEmpty
		{
			get
			{
				return this.SavedHold == null && this.SavedAmmo == null && this.SavedCrew == null && this.SavedSpecialCrew == null && this.SavedUpgradesIds == null && this.SavedPowerups == null;
			}
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x00003A7C File Offset: 0x00001C7C
		public SavedShipEquipment()
		{
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0010B224 File Offset: 0x00109424
		public SavedShipEquipment(int {25546}, PlayerShipDynamicInfo {25547}, bool {25548}, bool {25549}, bool {25550}, bool {25551}, bool {25552}, bool {25553})
		{
			this.ShipInfoId = {25547}.CraftFrom.ID;
			this.SlotIndex = (byte){25546};
			if ({25548})
			{
				this.SavedHold = {25547}.PrivateResourcesOfHold.Clone();
			}
			if ({25549})
			{
				this.SavedAmmo = new SavedShipEquipment.AmmoPreset({25547}.BallsOfHold.Clone(), {25547}.PowderKegsOfHold.Clone());
			}
			if ({25550})
			{
				this.SavedCrew = {25547}.Crew.Raw.Clone();
				this.SavedSpecialCrew = new GSI();
				foreach (SpecialUnitInstance specialUnitInstance in ((IEnumerable<SpecialUnitInstance>){25547}.Crew.Special))
				{
					GSI savedSpecialCrew = this.SavedSpecialCrew;
					int num = (int)specialUnitInstance.ID;
					int num2 = savedSpecialCrew[num];
					savedSpecialCrew[num] = num2 + 1;
				}
			}
			if ({25551})
			{
				this.SavedUpgradesIds = new GSI();
				foreach (ShipUpgradeInfo shipUpgradeInfo in {25547}.Upgrades.EnumerateInstalledUps())
				{
					GSI savedUpgradesIds = this.SavedUpgradesIds;
					int num2 = (int)shipUpgradeInfo.ID;
					int num = savedUpgradesIds[num2];
					savedUpgradesIds[num2] = num + 1;
				}
			}
			if ({25552})
			{
				this.SavedPowerups = new GSI();
				foreach (PowerupItemInfo powerupItemInfo in {25547}.PowerupItemSlots.EnumerateInstalledItems())
				{
					GSI savedPowerups = this.SavedPowerups;
					int num = powerupItemInfo.Index;
					int num2 = savedPowerups[num];
					savedPowerups[num] = num2 + 1;
				}
			}
			if ({25553})
			{
				this.SavedCannons = new GSI();
				foreach (CannonCommon cannonCommon in ((IEnumerable<CannonCommon>){25547}.Cannons.Items))
				{
					this.SavedCannons[(int)cannonCommon.Location.SectionID] = (int)cannonCommon.GameInfo.ID;
				}
			}
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0010B458 File Offset: 0x00109658
		public void Boxing(WriterExtern {25554})
		{
			{25554}.WriteByte(254);
			{25554}.WriteStruct<short>(this.ShipInfoId);
			{25554}.WriteByte(this.SlotIndex);
			{25554}.Write<SavedShipEquipment.AmmoPreset>(this.SavedAmmo);
			{25554}.Write<GSI>(this.SavedHold);
			{25554}.Write<GSI>(this.SavedCrew);
			{25554}.Write<GSI>(this.SavedSpecialCrew);
			{25554}.Write<GSI>(this.SavedUpgradesIds);
			{25554}.Write<GSI>(this.SavedPowerups);
			{25554}.Write<GSI>(this.SavedCannons);
			{25554}.Write(this.Active);
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0010B4E8 File Offset: 0x001096E8
		public void Unboxing(WriterExtern {25555})
		{
			if ({25555}.PeekByte() == 254)
			{
				{25555}.ReadByte();
				{25555}.ReadStruct<short>(out this.ShipInfoId);
				this.SlotIndex = {25555}.ReadByte();
				{25555}.ReadIMPS<SavedShipEquipment.AmmoPreset>(out this.SavedAmmo);
				{25555}.ReadIMPS<GSI>(out this.SavedHold);
				{25555}.ReadIMPS<GSI>(out this.SavedCrew);
				{25555}.ReadIMPS<GSI>(out this.SavedSpecialCrew);
				{25555}.ReadIMPS<GSI>(out this.SavedUpgradesIds);
				{25555}.ReadIMPS<GSI>(out this.SavedPowerups);
				{25555}.ReadIMPS<GSI>(out this.SavedCannons);
				{25555}.ReadBoolean(out this.Active);
				return;
			}
			{25555}.ReadStruct<short>(out this.ShipInfoId);
			this.SlotIndex = {25555}.ReadByte();
			{25555}.ReadIMPS<GSI>(out this.SavedHold);
			{25555}.ReadIMPS<GSI>(out this.SavedCrew);
			{25555}.ReadIMPS<GSI>(out this.SavedSpecialCrew);
			{25555}.ReadIMPS<GSI>(out this.SavedUpgradesIds);
			{25555}.ReadIMPS<GSI>(out this.SavedPowerups);
			{25555}.ReadIMPS<GSI>(out this.SavedCannons);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0010B5E8 File Offset: 0x001097E8
		public void Apply(bool {25556}, bool {25557})
		{
			bool flag = false;
			if (this.SavedAmmo != null)
			{
				flag |= !SavedShipEquipment.OperatePattern(Session.Account.CBallsAtStorage, Global.Player.UsedShipPlayer.BallsOfHold, this.SavedAmmo.Ammo);
				flag |= !SavedShipEquipment.OperatePattern(Session.Account.PowderKegsAtStorage, Global.Player.UsedShipPlayer.PowderKegsOfHold, this.SavedAmmo.PowderKegs);
			}
			if (this.SavedCrew != null)
			{
				Session.Account.UnitsAtStorage.Add(Global.Player.UsedShipPlayer.Crew.Raw);
				Global.Player.UsedShipPlayer.Crew.RemoveRegularUnits();
				foreach (GSILocalEnumerablePair<UnitInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<UnitInfo>>)this.SavedCrew.UnitInfo))
				{
					int count = gsilocalEnumerablePair.Count;
					int num = Session.Account.UnitsAtStorage[(int)gsilocalEnumerablePair.Info.ID];
					if (count > num)
					{
						flag = true;
					}
					int num2 = Math.Min(num, count);
					num2 = Math.Min(num2, Global.Player.UsedShipPlayer.CrewPlaces - Global.Player.UsedShipPlayer.Crew.Count);
					if (num2 > 0)
					{
						Global.Player.UsedShipPlayer.Crew.Add(gsilocalEnumerablePair.Info, num2, false);
						Session.Account.UnitsAtStorage.AddOrRemove((int)gsilocalEnumerablePair.Info.ID, -num2);
					}
				}
			}
			if (this.SavedSpecialCrew != null)
			{
				Global.Player.UsedShipPlayer.Crew.RemoveAllSpecialUnits(Session.Account);
				foreach (GSILocalPair gsilocalPair in ((IEnumerable<GSILocalPair>)this.SavedSpecialCrew))
				{
					ValueTuple<int, SpecialUnitInstance[]> valueTuple = Session.Account.SpecialUnitsAtStorage.Select(gsilocalPair.ID);
					int item4 = valueTuple.Item1;
					SpecialUnitInstance[] item2 = valueTuple.Item2;
					if (item2.Length != 0)
					{
						Session.Account.SpecialUnitsAtStorage.Remove(item2.First<SpecialUnitInstance>());
						Global.Player.UsedShipPlayer.Crew.Add(item2.First<SpecialUnitInstance>());
					}
					else if (item4 > 0)
					{
						Session.Account.SpecialUnitsAtStorage.Remove(Gameplay.UnitsInfo[gsilocalPair.ID], false);
						Global.Player.UsedShipPlayer.Crew.Add(new SpecialUnitInstance(gsilocalPair.ID));
					}
				}
			}
			if (this.SavedPowerups != null)
			{
				this.SavedPowerups.CleanRemovedPowerupItems();
				Global.Player.UsedShipPlayer.PowerupItemSlots.TakeOffAll();
				int num3 = 0;
				foreach (GSILocalPair gsilocalPair2 in ((IEnumerable<GSILocalPair>)this.SavedPowerups))
				{
					Global.Player.UsedShipPlayer.PowerupItemSlots[num3++] = Gameplay.PowerupItems[gsilocalPair2.ID];
				}
			}
			bool flag2 = Global.Player.UsedShip.HpFactor >= 1f;
			if (this.SavedUpgradesIds != null && {25556})
			{
				foreach (InstalledShipUpgradeSlot installedShipUpgradeSlot in Global.Player.UsedShipPlayer.Upgrades.GetUpgrades().ToArray<InstalledShipUpgradeSlot>())
				{
					if (installedShipUpgradeSlot.Info.Info.CategoryUi != ShipUpgradeCategory.Modification)
					{
						Session.Account.Shipyard.AddUpgrade(Global.Player.UsedShipPlayer.Upgrades.TakeOffUpgrade(Global.Player.UsedShipPlayer, installedShipUpgradeSlot.SlotIndex), Global.Player.UsedShipPlayer);
					}
				}
				int num4 = 0;
				using (IEnumerator<GSILocalPair> enumerator2 = ((IEnumerable<GSILocalPair>)this.SavedUpgradesIds).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						GSILocalPair item = enumerator2.Current;
						ShipUpgradeInstance item3;
						if (Session.Account.Shipyard.StoredUpgrades.TryRemove((byte)Global.Player.CraftFrom.ID, (ShipUpgradeInstance {25568}) => (int){25568}.ID == item.ID, out item3))
						{
							if (item3.Info.CategoryUi == ShipUpgradeCategory.Sailes)
							{
								if (Global.Player.UsedShipPlayer.Upgrades.GetSlot(0).Info.IsNull)
								{
									Global.Player.UsedShipPlayer.Upgrades.InstallStoredUpgrade(Global.Player.UsedShipPlayer, item3, 0);
								}
								else
								{
									Session.Account.Shipyard.StoredUpgrades.Add((byte)Global.Player.CraftFrom.ID, item3);
								}
							}
							else
							{
								while (!Global.Player.UsedShipPlayer.Upgrades.GetSlot(1 + num4).Info.IsNull)
								{
									num4++;
								}
								Global.Player.UsedShipPlayer.Upgrades.InstallStoredUpgrade(Global.Player.UsedShipPlayer, item3, 1 + num4++);
							}
						}
						else
						{
							flag = true;
						}
					}
				}
			}
			if (flag2)
			{
				Global.Player.RestoreHp(Global.Player.UsedShip.MaxHp);
			}
			if (this.SavedHold != null && {25556})
			{
				flag |= !SavedShipEquipment.OperatePattern(Session.Account.NearPortStorage, Global.Player.ResourcesOfHold, this.SavedHold);
			}
			if (this.SavedCannons != null && {25556})
			{
				Global.Player.UsedShipPlayer.TakeOffAllCannons(Session.Account, (CannonCommon {25567}) => true);
				foreach (GSILocalPair gsilocalPair3 in ((IEnumerable<GSILocalPair>)this.SavedCannons))
				{
					int id = gsilocalPair3.ID;
					int count2 = gsilocalPair3.Count;
					CannonLocationInfo cannonLocationInfo = (id <= Global.Player.UsedShip.StaticInfo.Ports.Length) ? Global.Player.UsedShip.StaticInfo.Ports[id - 1] : null;
					if (cannonLocationInfo != null && Global.Player.UsedShipPlayer.Cannons.FindByLocation((int)cannonLocationInfo.SectionID) == null)
					{
						if (Session.Account.CannonsAtStorage[count2] == 0)
						{
							flag = true;
							break;
						}
						GSI cannonsAtStorage = Session.Account.CannonsAtStorage;
						int i = count2;
						int num5 = cannonsAtStorage[i];
						cannonsAtStorage[i] = num5 - 1;
						Global.Player.UsedShipPlayer.Cannons.Install(cannonLocationInfo, Gameplay.CannonsGameInfo[count2]);
					}
				}
			}
			GameplayHelper.CheckCannons();
			GameplayHelper.CheckUpgrades();
			GameplayHelper.AutoDropOffCrew();
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0010BD2C File Offset: 0x00109F2C
		private static bool OperatePattern(GSI {25558}, GSI {25559}, GSI {25560})
		{
			{25558}.Add({25559});
			{25559}.Clean();
			bool result = true;
			foreach (GSILocalPair gsilocalPair in ((IEnumerable<GSILocalPair>){25560}))
			{
				int count = gsilocalPair.Count;
				int num = {25558}[gsilocalPair.ID];
				if (count > num)
				{
					result = false;
				}
				{25559}.AddOrRemove(gsilocalPair.ID, Math.Min(num, count));
				{25558}.AddOrRemove(gsilocalPair.ID, -Math.Min(num, count));
			}
			return result;
		}

		// Token: 0x04001CDF RID: 7391
		public short ShipInfoId;

		// Token: 0x04001CE0 RID: 7392
		public byte SlotIndex;

		// Token: 0x04001CE1 RID: 7393
		[Nullable(2)]
		public SavedShipEquipment.AmmoPreset SavedAmmo;

		// Token: 0x04001CE2 RID: 7394
		[Nullable(2)]
		public GSI SavedHold;

		// Token: 0x04001CE3 RID: 7395
		[Nullable(2)]
		public GSI SavedCrew;

		// Token: 0x04001CE4 RID: 7396
		[Nullable(2)]
		public GSI SavedSpecialCrew;

		// Token: 0x04001CE5 RID: 7397
		[Nullable(2)]
		public GSI SavedUpgradesIds;

		// Token: 0x04001CE6 RID: 7398
		[Nullable(2)]
		public GSI SavedPowerups;

		// Token: 0x04001CE7 RID: 7399
		[Nullable(2)]
		public GSI SavedCannons;

		// Token: 0x04001CE8 RID: 7400
		public bool Active;

		// Token: 0x02000525 RID: 1317
		public class AmmoPreset : IMPSerializable
		{
			// Token: 0x06001D70 RID: 7536 RVA: 0x00003A7C File Offset: 0x00001C7C
			public AmmoPreset()
			{
			}

			// Token: 0x06001D71 RID: 7537 RVA: 0x0010BDC4 File Offset: 0x00109FC4
			public AmmoPreset(GSI {25563}, GSI {25564})
			{
				this.Ammo = {25563};
				this.PowderKegs = {25564};
			}

			// Token: 0x06001D72 RID: 7538 RVA: 0x0010BDDA File Offset: 0x00109FDA
			public void Boxing(WriterExtern {25565})
			{
				{25565}.Write<GSI>(this.Ammo);
				{25565}.Write<GSI>(this.PowderKegs);
			}

			// Token: 0x06001D73 RID: 7539 RVA: 0x0010BDF4 File Offset: 0x00109FF4
			public void Unboxing(WriterExtern {25566})
			{
				{25566}.ReadIMPS<GSI>(out this.Ammo);
				{25566}.ReadIMPS<GSI>(out this.PowderKegs);
			}

			// Token: 0x04001CE9 RID: 7401
			public GSI Ammo;

			// Token: 0x04001CEA RID: 7402
			public GSI PowderKegs;
		}
	}
}
