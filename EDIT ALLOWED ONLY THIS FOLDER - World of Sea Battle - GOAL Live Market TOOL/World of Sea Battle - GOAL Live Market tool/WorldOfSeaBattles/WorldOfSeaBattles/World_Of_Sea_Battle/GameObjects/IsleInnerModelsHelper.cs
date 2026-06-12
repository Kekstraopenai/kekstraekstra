using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Account;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x0200004B RID: 75
	public class IsleInnerModelsHelper
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00011DDD File Offset: 0x0000FFDD
		public FactoryPlaceIsleInfo Place
		{
			get
			{
				return this.{16717};
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00011DE8 File Offset: 0x0000FFE8
		public IsleInnerModelsHelper(FactoryPlaceIsleInfo {16706}, ModelTransformedScene {16707})
		{
			this.{16717} = {16706};
			using (Dictionary<string, UWModel>.Enumerator enumerator = {16706}.ModelData.Connections.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, UWModel> item = enumerator.Current;
					if (({16706}.Predefines.First() == FactoryType.Temp_PersonalIsle) ? IsleInnerModelsHelper.PersonalIsleConnectionsBase.Values.Any((string {16727}) => item.Key.Contains({16727})) : item.Key.Contains("Level"))
					{
						{16707}.AddObject(new ModelRenderer(item.Value)
						{
							LocalVisible = false,
							Tag = item.Key
						}, true);
					}
				}
			}
			foreach (KeyValuePair<string, UWModel> keyValuePair in {16706}.ModelData.Connections)
			{
				if (keyValuePair.Key.Contains("ConnectionPoinConnectionPointLightForLighthousetLight"))
				{
					this.{16721} = {16707}.Transform.Transform3X3(keyValuePair.Value.CommonSphere.Center);
				}
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00011F58 File Offset: 0x00010158
		public void Update(ModelTransformedScene {16708}, float {16709})
		{
			if (Global.Player == null)
			{
				return;
			}
			for (int i = 1; i < {16708}.GetModels.Size; i++)
			{
				ModelRenderer modelRenderer = {16708}.GetModels.Array[i];
				if (!(modelRenderer.Tag is ShipDesignInfo))
				{
					modelRenderer.LocalVisible = false;
				}
			}
			if (this.{16717}.Predefines.Array[0] == FactoryType.Temp_PersonalIsle)
			{
				PersonalIsleStatus personalIsleStatus = Session.Account.PersonalIsles.Data.Find(new Func<PersonalIsleStatus, bool>(this.{16715}));
				if (personalIsleStatus != null)
				{
					Dictionary<PersonalIsleStatus.InternalBuilding, string> dictionary = (((Session.Account.PersonalIsleLimit >= 3) ? 2 : 1) == 1) ? IsleInnerModelsHelper.PersonalIsleConnectionsL1 : IsleInnerModelsHelper.PersonalIsleConnectionsL2;
					foreach (PersonalIsleStatus.InternalBuilding key in ((IEnumerable<PersonalIsleStatus.InternalBuilding>)personalIsleStatus.InternalBuildings))
					{
						string {11963};
						if (dictionary.TryGetValue(key, out {11963}))
						{
							{16708}.GetByTag({11963}).LocalVisible = true;
						}
					}
					if (this.GlobalLampLghts.Size == 0 && personalIsleStatus.InternalBuildings.Contains(PersonalIsleStatus.InternalBuilding.Lighthouse))
					{
						this.{16710}(personalIsleStatus);
					}
					int num = personalIsleStatus.InstalledDecor.Sum((PersonalIsleInstalledDecorItem {16722}) => (int){16722}.PlaceIndex);
					if (this.{16719} != personalIsleStatus.InstalledDecor.Size || this.{16720} != num)
					{
						this.{16710}(personalIsleStatus);
						this.{16712}({16708}, personalIsleStatus);
						this.{16719} = personalIsleStatus.InstalledDecor.Size;
						this.{16720} = num;
						return;
					}
				}
				else if (this.{16719} != 0)
				{
					{16708}.GetModels.RemoveAll((ModelRenderer {16723}) => {16723}.Tag is ShipDesignInfo);
					this.{16719} = 0;
					return;
				}
			}
			else
			{
				PlayerBuildingState playerBuildingState = Session.Account.Buildings.Get(this.{16717}.FcID);
				FactoryType? factoryType = null;
				if (playerBuildingState == null)
				{
					{16708}.GetByTag("ConnectionLevel1").LocalVisible = false;
					{16708}.GetByTag("ConnectionLevel2").LocalVisible = false;
					if ({16708}.GetByTag("ConnectionLevelBuilding") != null)
					{
						{16708}.GetByTag("ConnectionLevelBuilding").LocalVisible = true;
					}
					factoryType = null;
				}
				else
				{
					{16708}.GetByTag("ConnectionLevel1").LocalVisible = true;
					{16708}.GetByTag("ConnectionLevel2").LocalVisible = (playerBuildingState.LevelIndex >= 2);
					if ({16708}.GetByTag("ConnectionLevelBuilding") != null)
					{
						{16708}.GetByTag("ConnectionLevelBuilding").LocalVisible = false;
					}
					factoryType = new FactoryType?(playerBuildingState.Type);
				}
				FactoryType? factoryType2 = factoryType;
				FactoryType? factoryType3 = this.{16718};
				if (!(factoryType2.GetValueOrDefault() == factoryType3.GetValueOrDefault() & factoryType2 != null == (factoryType3 != null)) && {16709} < 300f)
				{
					for (int j = 0; j < 20; j++)
					{
						FXEngine.SampleFumesSmoke(this.{16717}.ModelGlobalBSxz.X0Y() + Rand.NextVector3(-1f, 1f) * new Vector3(1f, 0.5f, 1f) * Rand.Range(1f, this.{16717}.ModelGlobalBS.Radius * 0.8f), 4f, 1f, 1f);
					}
				}
				this.{16718} = factoryType;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000122D0 File Offset: 0x000104D0
		private void {16710}(PersonalIsleStatus {16711})
		{
			this.GlobalLampLghts.Clear();
			if ({16711}.InternalBuildings.Contains(PersonalIsleStatus.InternalBuilding.Lighthouse))
			{
				Tlist<IsleFlares> globalLampLghts = this.GlobalLampLghts;
				IsleFlares isleFlares = new IsleFlares(this.{16721}, IsleFlaresSize.Small, this.{16721}.GetHashCode(), 1f);
				globalLampLghts.Add(isleFlares);
			}
			foreach (PersonalIsleInstalledDecorItem personalIsleInstalledDecorItem in ((IEnumerable<PersonalIsleInstalledDecorItem>){16711}.InstalledDecor))
			{
				if (personalIsleInstalledDecorItem.Info.nameKey.Contains("light"))
				{
					Vector3 {16649} = {16711}.Place.GlobalTransform.Transform3X3(personalIsleInstalledDecorItem.GlobalPosition({16711}));
					Tlist<IsleFlares> globalLampLghts2 = this.GlobalLampLghts;
					IsleFlares isleFlares = new IsleFlares({16649}, IsleFlaresSize.Small, {16649}.GetHashCode(), 1f);
					globalLampLghts2.Add(isleFlares);
				}
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000123B8 File Offset: 0x000105B8
		private void {16712}(ModelTransformedScene {16713}, PersonalIsleStatus {16714})
		{
			{16713}.GetModels.RemoveAll((ModelRenderer {16724}) => {16724}.Tag is ShipDesignInfo);
			foreach (PersonalIsleInstalledDecorItem personalIsleInstalledDecorItem in ((IEnumerable<PersonalIsleInstalledDecorItem>){16714}.InstalledDecor))
			{
				{16713}.AddObject(new ModelRenderer(personalIsleInstalledDecorItem.Info.ApartModel)
				{
					LocalTransformOrNull = new Transform3D
					{
						Translation = personalIsleInstalledDecorItem.GlobalPosition({16714})
					},
					Tag = personalIsleInstalledDecorItem.Info
				}, true);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00012468 File Offset: 0x00010668
		// Note: this type is marked as 'beforefieldinit'.
		static IsleInnerModelsHelper()
		{
			Dictionary<PersonalIsleStatus.InternalBuilding, string> dictionary = new Dictionary<PersonalIsleStatus.InternalBuilding, string>();
			dictionary[PersonalIsleStatus.InternalBuilding.Dok] = "ConnectionDok";
			dictionary[PersonalIsleStatus.InternalBuilding.Shipbuilder] = "ConnectionShip";
			dictionary[PersonalIsleStatus.InternalBuilding.Workshop] = "ConnectionWorkshop";
			dictionary[PersonalIsleStatus.InternalBuilding.BigStorage] = "ConnectionStorage";
			dictionary[PersonalIsleStatus.InternalBuilding.Avanpost] = "ConnectionTower";
			dictionary[PersonalIsleStatus.InternalBuilding.Factory] = "ConnectionFactory";
			dictionary[PersonalIsleStatus.InternalBuilding.Pub] = "ConnectionTrader";
			dictionary[PersonalIsleStatus.InternalBuilding.Lighthouse] = "ConnectionLighthouse";
			IsleInnerModelsHelper.PersonalIsleConnectionsBase = dictionary;
			IsleInnerModelsHelper.PersonalIsleConnectionsL1 = new Dictionary<PersonalIsleStatus.InternalBuilding, string>(from {16725} in IsleInnerModelsHelper.PersonalIsleConnectionsBase
			select new KeyValuePair<PersonalIsleStatus.InternalBuilding, string>({16725}.Key, {16725}.Value + "_level1"));
			IsleInnerModelsHelper.PersonalIsleConnectionsL2 = new Dictionary<PersonalIsleStatus.InternalBuilding, string>(from {16726} in IsleInnerModelsHelper.PersonalIsleConnectionsBase
			select new KeyValuePair<PersonalIsleStatus.InternalBuilding, string>({16726}.Key, {16726}.Value + "_level2"));
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00012527 File Offset: 0x00010727
		[NullableContext(1)]
		[CompilerGenerated]
		private bool {16715}(PersonalIsleStatus {16716})
		{
			return {16716}.PlaceIndex == (byte)this.{16717}.FcID;
		}

		// Token: 0x040001B8 RID: 440
		public static Dictionary<PersonalIsleStatus.InternalBuilding, string> PersonalIsleConnectionsBase;

		// Token: 0x040001B9 RID: 441
		public static Dictionary<PersonalIsleStatus.InternalBuilding, string> PersonalIsleConnectionsL1;

		// Token: 0x040001BA RID: 442
		public static Dictionary<PersonalIsleStatus.InternalBuilding, string> PersonalIsleConnectionsL2;

		// Token: 0x040001BB RID: 443
		private FactoryPlaceIsleInfo {16717};

		// Token: 0x040001BC RID: 444
		private FactoryType? {16718};

		// Token: 0x040001BD RID: 445
		private int {16719} = -1;

		// Token: 0x040001BE RID: 446
		private int {16720} = -1;

		// Token: 0x040001BF RID: 447
		private Vector3 {16721};

		// Token: 0x040001C0 RID: 448
		public Tlist<IsleFlares> GlobalLampLghts = new Tlist<IsleFlares>();
	}
}
