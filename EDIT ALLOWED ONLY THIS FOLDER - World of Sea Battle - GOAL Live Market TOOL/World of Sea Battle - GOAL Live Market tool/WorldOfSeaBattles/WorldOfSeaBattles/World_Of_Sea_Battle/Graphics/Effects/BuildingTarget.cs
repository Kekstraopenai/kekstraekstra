using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using ManualPacketSerialization;
using Microsoft.Xna.Framework;
using TheraEngine;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x0200049D RID: 1181
	public readonly struct BuildingTarget
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x000E67CF File Offset: 0x000E49CF
		public bool ShowOnlyInSpyglass
		{
			get
			{
				return this.AsDynamic == null || Vector2.DistanceSquared(Global.Player.Position, this.Center) > 90000f;
			}
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x000E67FC File Offset: 0x000E49FC
		private BuildingEnvIcon {24373}(BuildingEnvIcon {24374})
		{
			if (!InterestPointsManager.IsBuildingResearched(this.Center))
			{
				{24374} = new BuildingEnvIcon(EnvironmentGraphics.pathQuestion, {24374}.Scale, {24374}.CustomTex);
			}
			return {24374};
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x000E6824 File Offset: 0x000E4A24
		public BuildingTarget(TraderInSeaPlaceInfo {24375})
		{
			this.AsDynamic = null;
			this.AsTrader = null;
			this.AsFactory = null;
			this.AsPharos = null;
			this.AsQuest = null;
			this.AsPort = null;
			this.EnvironmentIcon = null;
			this.Radius = 33f;
			this.Center = {24375}.Position;
			this.AsTrader = {24375};
			this.EnvironmentIcon = new BuildingEnvIcon?(this.{24373}(new BuildingEnvIcon(EnvironmentGraphics.pathTraderInSea, 1f, null)));
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x000E68AC File Offset: 0x000E4AAC
		public BuildingTarget(FactoryPlaceIsleInfo {24376})
		{
			this.AsDynamic = null;
			this.AsTrader = null;
			this.AsFactory = null;
			this.AsPharos = null;
			this.AsQuest = null;
			this.AsPort = null;
			this.EnvironmentIcon = null;
			this.Radius = 33f;
			this.Center = {24376}.GlobalPosition;
			this.AsFactory = {24376};
			this.EnvironmentIcon = new BuildingEnvIcon?(this.{24373}(new BuildingEnvIcon(({24376}.Predefines.FirstOrDefault<FactoryType>() == FactoryType.Temp_PersonalIsle) ? EnvironmentGraphics.pathHome : EnvironmentGraphics.pathMine, 1f, null)));
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000E694C File Offset: 0x000E4B4C
		public BuildingTarget(IslePortPharosInfo {24377})
		{
			this.AsDynamic = null;
			this.AsTrader = null;
			this.AsFactory = null;
			this.AsPharos = null;
			this.AsQuest = null;
			this.AsPort = null;
			this.EnvironmentIcon = null;
			this.Radius = 20f;
			this.Center = Vector2.Lerp({24377}.Isle.ModelGlobalBSxz, {24377}.MapGlobalPosition, 0.3f);
			this.AsPharos = {24377};
			this.EnvironmentIcon = null;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x000E69D4 File Offset: 0x000E4BD4
		public BuildingTarget(QuestInfo {24378})
		{
			this.AsDynamic = null;
			this.AsTrader = null;
			this.AsFactory = null;
			this.AsPharos = null;
			this.AsQuest = null;
			this.AsPort = null;
			this.EnvironmentIcon = null;
			this.Radius = 20f;
			this.Center = {24378}.LocationPos;
			this.AsQuest = {24378};
			this.EnvironmentIcon = new BuildingEnvIcon?(new BuildingEnvIcon({24378}.Icon, 0.4f, OtherTextures.WorldMapUiElements));
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x000E6A5C File Offset: 0x000E4C5C
		public BuildingTarget(IslePortInfo {24379})
		{
			this.AsDynamic = null;
			this.AsTrader = null;
			this.AsFactory = null;
			this.AsPharos = null;
			this.AsQuest = null;
			this.AsPort = null;
			this.EnvironmentIcon = null;
			this.Radius = 40f;
			this.Center = {24379}.EntryPos;
			this.AsPort = {24379};
			this.EnvironmentIcon = new BuildingEnvIcon?(new BuildingEnvIcon(EnvironmentGraphics.pathPort, 1f, null));
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000E6AE0 File Offset: 0x000E4CE0
		public BuildingTarget(DynamicBuildCreatePacket {24380}, IsleInstance {24381})
		{
			this.AsDynamic = null;
			this.AsTrader = null;
			this.AsFactory = null;
			this.AsPharos = null;
			this.AsQuest = null;
			this.AsPort = null;
			this.EnvironmentIcon = null;
			this.Radius = {24381}.ModelGlobalBS.Radius;
			this.Center = {24381}.ModelGlobalBS.Center.XZ();
			this.AsDynamic = new DynamicBuildCreatePacket?({24380});
			this.EnvironmentIcon = null;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000E6B68 File Offset: 0x000E4D68
		[return: TupleElementNames(new string[]
		{
			"name",
			"desc"
		})]
		public ValueTuple<string, string> GetText()
		{
			if (this.AsDynamic != null)
			{
				DynamicBuildCreatePacket value = this.AsDynamic.Value;
				bool flag = value.Flags == WorldObjectID.WGuildFortTower || value.Flags == WorldObjectID.ArenaTower;
				string text = string.Empty;
				IMPSerializable visualData = value.VisualData;
				if (visualData is GuildBuildingVisualData)
				{
					GuildBuildingVisualData guildBuildingVisualData = (GuildBuildingVisualData)visualData;
					text = Gameplay.WorldMap.Ports[(int)guildBuildingVisualData.PortID].PortNameShort;
					PortCaptureStatus byPortId = Session.Game.WorldPorts.GetByPortId((int)guildBuildingVisualData.PortID);
					FractionID fractionID = (byPortId != null) ? byPortId.CapturerFraction : FractionID.None;
					if (fractionID != FractionID.None)
					{
						text = text + ", " + fractionID.GetName();
						if (!string.IsNullOrEmpty(guildBuildingVisualData.CapturedByGuild))
						{
							text = text + " [" + guildBuildingVisualData.CapturedByGuild + "]";
						}
					}
				}
				if (value.VisualData is WorldFortVisualData)
				{
					text = ((value.NowStrength == 0f) ? Local.destroyed : Local.worldEmpireFort_tt2);
				}
				return new ValueTuple<string, string>((value.VisualData is WorldFortVisualData) ? Local.worldEmpireFort : (flag ? Local.tower : Local.fort), text);
			}
			if (this.AsTrader != null)
			{
				return new ValueTuple<string, string>(Local.TraderInSea, string.Empty);
			}
			if (this.AsFactory != null)
			{
				FactoryType key = this.AsFactory.Predefines.FirstOrDefault(FactoryType.Temp_PersonalIsle);
				return new ValueTuple<string, string>(WosbCrafting.FactoriesInfo[key].Name, string.Empty);
			}
			if (this.AsPharos != null)
			{
				return new ValueTuple<string, string>(Local.WorldMapUi_pharosHeader, string.Empty);
			}
			if (this.AsPort != null)
			{
				return new ValueTuple<string, string>(this.AsPort.PortName, string.Empty);
			}
			if (this.AsQuest != null)
			{
				return new ValueTuple<string, string>(Local.quest, this.AsQuest.QuestShortName);
			}
			return new ValueTuple<string, string>(string.Empty, string.Empty);
		}

		// Token: 0x04001833 RID: 6195
		public readonly float Radius;

		// Token: 0x04001834 RID: 6196
		public readonly Vector2 Center;

		// Token: 0x04001835 RID: 6197
		public readonly DynamicBuildCreatePacket? AsDynamic;

		// Token: 0x04001836 RID: 6198
		[Nullable(2)]
		public readonly TraderInSeaPlaceInfo AsTrader;

		// Token: 0x04001837 RID: 6199
		[Nullable(2)]
		public readonly FactoryPlaceIsleInfo AsFactory;

		// Token: 0x04001838 RID: 6200
		[Nullable(2)]
		public readonly IslePortPharosInfo AsPharos;

		// Token: 0x04001839 RID: 6201
		[Nullable(2)]
		public readonly QuestInfo AsQuest;

		// Token: 0x0400183A RID: 6202
		[Nullable(2)]
		public readonly IslePortInfo AsPort;

		// Token: 0x0400183B RID: 6203
		public readonly BuildingEnvIcon? EnvironmentIcon;
	}
}
