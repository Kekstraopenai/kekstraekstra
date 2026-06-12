using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using TheraEngine.Reactive;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000403 RID: 1027
	internal sealed class {22887} : CustomUi
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x000BBB4C File Offset: 0x000B9D4C
		private static float itemSize
		{
			get
			{
				return 29f;
			}
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x000BBB53 File Offset: 0x000B9D53
		public bool IsFiltersChanged()
		{
			return !{22887}.Info.All(([TupleElementNames(new string[]
			{
				"caption",
				"atlWorldMapPath",
				"iconScale"
			})] KeyValuePair<{22887}.Id, ValueTuple<Func<string>, Rectangle, float>> {22903}) => {22887}.Statuses[{22903}.Key].Value == !{22887}.NotSelectedByDefault.Contains({22903}.Key));
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x000BBB81 File Offset: 0x000B9D81
		private static float adaptiveItemsSize
		{
			get
			{
				return {22887}.itemSize - (float)((Global.Game.WindowSize.Y < 900f) ? 3 : 1);
			}
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x000BBBA4 File Offset: 0x000B9DA4
		public {22887}(float {22890}, IEnumerable<PortLiveTrading> {22891}) : base(new Marker(0f, 0f, {22890}, (float){22887}.Info.Count * {22887}.adaptiveItemsSize + {22887}.itemSize * 0.5f), new Rectangle(1077, 339, 130, 197), PositionAlignment.LeftUp, PositionAlignment.RightDown, Color.White, false)
		{
			this.AnimatedFocus = false;
			if ({22887}.Statuses == null)
			{
				{22887}.Statuses = new Dictionary<{22887}.Id, Reactive<bool>>();
				foreach (KeyValuePair<{22887}.Id, ValueTuple<Func<string>, Rectangle, float>> keyValuePair in {22887}.Info)
				{
					{22887}.Statuses.Add(keyValuePair.Key, new Reactive<bool>(!Global.Settings.NotSelectedWorldMapicons.Contains(keyValuePair.Key)));
				}
			}
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddSpace({22887}.itemSize * 0.25f);
			foreach (KeyValuePair<{22887}.Id, ValueTuple<Func<string>, Rectangle, float>> keyValuePair2 in {22887}.Info)
			{
				stackForm.AddItem(new UiControl[]
				{
					new {22887}.Item(keyValuePair2.Key, new Marker(0f, 0f, {22890}, {22887}.adaptiveItemsSize), {22887}.Statuses[keyValuePair2.Key])
				});
			}
			base.AddChild(stackForm);
			if ({22891}.Count<PortLiveTrading>() >= 0)
			{
				StackForm stackForm2 = new StackForm(default(Vector2), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				GSI gsi = new GSI();
				foreach (PortLiveTrading portLiveTrading in {22891})
				{
					gsi.Add(portLiveTrading.PortInstance.LiveTrading);
				}
				stackForm2.AddItem(new UiControl[]
				{
					new Label(default(Vector2), Fonts.Philosopher_14, Color.White * 0.75f, Local.map_info_trade, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						Shadowed = true
					}
				});
				using (IEnumerator<GSILocalEnumerablePair<ResourceInfo>> enumerator3 = ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)gsi.ResourceInfo).GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						GSILocalEnumerablePair<ResourceInfo> item = enumerator3.Current;
						float num = (float){22891}.Sum((PortLiveTrading {22904}) => {22904}.CurrentCount[(int)item.Info.ID]);
						int num2 = {22891}.Sum((PortLiveTrading {22905}) => {22905}.PortInstance.LiveTrading[(int)item.Info.ID]);
						float num3 = num / (float)num2;
						Color {13344} = Color.Lerp(Color.Red, Color.Lime, Geometry.InverseLerp(0f, 2f, num3));
						StackForm stackForm3 = new StackForm(default(Vector2), UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						stackForm3.AddItem(new UiControl[]
						{
							new Image(new Marker(0f, 0f, 20f, 20f), item.Info.IconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
						stackForm3.AddSpace(2f);
						stackForm3.AddItem(new UiControl[]
						{
							new Label(default(Vector2), Fonts.Philosopher_14, {13344}, item.Info.Name + " " + Math.Round((double)(num3 * 100f)).ToString() + "%", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							{
								Shadowed = true
							}
						});
						stackForm2.AddItem(new UiControl[]
						{
							stackForm3
						});
					}
				}
				stackForm2.Pos = stackForm2.Pos.SetXY(stackForm.Pos.End.X + 20f, stackForm.Pos.End.Y - stackForm2.Pos.Height);
				Form form = new Form(stackForm2.Pos.Border(15f), new Rectangle(305, 140, 204, 123), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form.AddChildPos(stackForm2, PositionAlignment.Center, PositionAlignment.Center, 0f);
				base.AddChild(form);
			}
			base.EvRemoveFromContainer += delegate()
			{
				Global.Settings.NotSelectedWorldMapicons.Clear();
				foreach (KeyValuePair<{22887}.Id, Reactive<bool>> keyValuePair3 in {22887}.Statuses)
				{
					if (!keyValuePair3.Value.Value)
					{
						Tlist<{22887}.Id> notSelectedWorldMapicons = Global.Settings.NotSelectedWorldMapicons;
						{22887}.Id key = keyValuePair3.Key;
						notSelectedWorldMapicons.Add(key);
					}
				}
			};
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserUpdate(ref FrameTime {22892})
		{
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x000BC060 File Offset: 0x000BA260
		// Note: this type is marked as 'beforefieldinit'.
		static {22887}()
		{
			Dictionary<{22887}.Id, ValueTuple<Func<string>, Rectangle, float>> dictionary = new Dictionary<{22887}.Id, ValueTuple<Func<string>, Rectangle, float>>();
			dictionary[{22887}.Id.Center] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_Center, {22913}.c_playerMarker, 0.8f);
			dictionary[{22887}.Id.CustomMarker] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_CustomMarker, {22913}.c_iconMarker, 1f);
			dictionary[{22887}.Id.LineDisplay] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_LineDisplay, new Rectangle(975, 285, 49, 49), 0.8f);
			dictionary[{22887}.Id.Port] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_Port, {22913}.c_iconPort, 1f);
			dictionary[{22887}.Id.PortsWithResources] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_PortsWithResources, {22913}.c_iconPort_Res, 1f);
			dictionary[{22887}.Id.PortsWithWorkshops] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_PortsWithWorkshops, {22913}.c_iconPort_Res, 1f);
			dictionary[{22887}.Id.PortsWithStorages] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_PortsWithStorages, {22913}.c_iconPort_Res, 1f);
			dictionary[{22887}.Id.QuestMarker] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_QuestMarker, {22913}.c_quest_battle, 1.1f);
			dictionary[{22887}.Id.TraderInSea] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_TraderInSea, {22913}.c_iconTrader, 1.3f);
			dictionary[{22887}.Id.Factory] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_Factory, {22913}.c_iconMine, 1.1f);
			dictionary[{22887}.Id.FactoryCaptured] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_FactoryCaptured, {22913}.c_iconMineGreen, 1.1f);
			dictionary[{22887}.Id.GlobalEvent] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_GlobalEvent, {22913}.c_event_redSkull, 1.1f);
			dictionary[{22887}.Id.WaterHazardLevel] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_WaterHazardLevel, new Rectangle(1025, 285, 49, 49), 0.9f);
			dictionary[{22887}.Id.WorldActivity] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_WorldActivity, {22913}.c_lootIsle, 1.3f);
			dictionary[{22887}.Id.FortsAndTowers] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_FortsAndTowers, {22913}.c_towerIconDefault, 1f);
			dictionary[{22887}.Id.GroupMembers] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_GroupMembers, {22913}.c_allyMarker, 1f);
			dictionary[{22887}.Id.GuildMembers] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_GuildMembers, {22913}.c_allyMarker2, 1f);
			dictionary[{22887}.Id.WeatherEffects] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_WeatherEffects, {22913}.c_weatherArea, 0.9f);
			dictionary[{22887}.Id.QuestTargets] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_QuestTargets, {22913}.c_targetEnemy, 1f);
			dictionary[{22887}.Id.PbRespawns] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_PbRespawns, {22913}.c_enterpb_mark_dim, 0.8f);
			dictionary[{22887}.Id.AllyFractionPorts] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_AllyFractionPorts, {22913}.c_fort_my, 1f);
			dictionary[{22887}.Id.TradingRoutes] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_TradingRoutes, {22913}.c_event_yellowCaravan, 1f);
			dictionary[{22887}.Id.Tool_SelectAll] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_Tool_SelectAll, new Rectangle(227, 0, 27, 27), 1f);
			dictionary[{22887}.Id.Tool_UnselectAll] = new ValueTuple<Func<string>, Rectangle, float>(() => Local.map_legend_Tool_UnselectAll, new Rectangle(227, 28, 27, 27), 1f);
			{22887}.Info = dictionary;
			Tlist<{22887}.Id> tlist = new Tlist<{22887}.Id>();
			{22887}.Id id = {22887}.Id.PbRespawns;
			tlist.Add(id);
			{22887}.Id id2 = {22887}.Id.AllyFractionPorts;
			tlist.Add(id2);
			{22887}.Id id3 = {22887}.Id.PortsWithResources;
			tlist.Add(id3);
			{22887}.Id id4 = {22887}.Id.PortsWithStorages;
			tlist.Add(id4);
			{22887}.Id id5 = {22887}.Id.PortsWithWorkshops;
			tlist.Add(id5);
			{22887}.NotSelectedByDefault = tlist;
		}

		// Token: 0x04001413 RID: 5139
		[TupleElementNames(new string[]
		{
			"caption",
			"atlWorldMapPath",
			"iconScale"
		})]
		private static readonly Dictionary<{22887}.Id, ValueTuple<Func<string>, Rectangle, float>> Info;

		// Token: 0x04001414 RID: 5140
		public static Tlist<{22887}.Id> NotSelectedByDefault;

		// Token: 0x04001415 RID: 5141
		public static Dictionary<{22887}.Id, Reactive<bool>> Statuses;

		// Token: 0x02000404 RID: 1028
		public enum Id : byte
		{
			// Token: 0x04001417 RID: 5143
			CustomMarker,
			// Token: 0x04001418 RID: 5144
			LineDisplay,
			// Token: 0x04001419 RID: 5145
			Port,
			// Token: 0x0400141A RID: 5146
			QuestMarker,
			// Token: 0x0400141B RID: 5147
			TraderInSea,
			// Token: 0x0400141C RID: 5148
			removed_1,
			// Token: 0x0400141D RID: 5149
			removed_2,
			// Token: 0x0400141E RID: 5150
			Factory,
			// Token: 0x0400141F RID: 5151
			FactoryCaptured,
			// Token: 0x04001420 RID: 5152
			GlobalEvent,
			// Token: 0x04001421 RID: 5153
			WaterHazardLevel,
			// Token: 0x04001422 RID: 5154
			WorldActivity,
			// Token: 0x04001423 RID: 5155
			FortsAndTowers,
			// Token: 0x04001424 RID: 5156
			GroupMembers,
			// Token: 0x04001425 RID: 5157
			GuildMembers,
			// Token: 0x04001426 RID: 5158
			WeatherEffects,
			// Token: 0x04001427 RID: 5159
			QuestTargets,
			// Token: 0x04001428 RID: 5160
			PbRespawns,
			// Token: 0x04001429 RID: 5161
			AllyFractionPorts,
			// Token: 0x0400142A RID: 5162
			PortsWithResources,
			// Token: 0x0400142B RID: 5163
			PortsWithWorkshops,
			// Token: 0x0400142C RID: 5164
			PortsWithStorages,
			// Token: 0x0400142D RID: 5165
			Tool_SelectAll,
			// Token: 0x0400142E RID: 5166
			Tool_UnselectAll,
			// Token: 0x0400142F RID: 5167
			Center,
			// Token: 0x04001430 RID: 5168
			TradingRoutes
		}

		// Token: 0x02000405 RID: 1029
		private class Item : CustomUi
		{
			// Token: 0x06001655 RID: 5717 RVA: 0x000BC48C File Offset: 0x000BA68C
			public Item({22887}.Id {22896}, Marker {22897}, Reactive<bool> {22898})
			{
				{22887}.Item.<>c__DisplayClass0_0 CS$<>8__locals1 = new {22887}.Item.<>c__DisplayClass0_0();
				CS$<>8__locals1.id = {22896};
				CS$<>8__locals1.status = {22898};
				base..ctor({22897}, Rectangle.Empty, PositionAlignment.Both, PositionAlignment.Both, Color.White, false);
				float num = {22887}.itemSize * {22887}.Info[CS$<>8__locals1.id].Item3 * 1.1f;
				Label label = new Label(Vector2.Zero, Fonts.Philosopher_16, Color.Transparent, {22887}.Info[CS$<>8__locals1.id].Item1(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Image icon = new Image(new Marker(0f, 0f, num, num), OtherTextures.WorldMapUiElements, {22887}.Info[CS$<>8__locals1.id].Item2, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				base.AddChildPos(icon, PositionAlignment.LeftUp, PositionAlignment.Center, {22887}.itemSize * 0.3f);
				base.AddChildPos(label, PositionAlignment.Center, PositionAlignment.Center, (float)((int)({22887}.itemSize * 1.3f)));
				icon.Pos = icon.Pos.Offset(-icon.Pos.WH.X * ({22887}.Info[CS$<>8__locals1.id].Item3 - 1f) * 0.5f, 0f);
				if (CS$<>8__locals1.id == {22887}.Id.Tool_SelectAll || CS$<>8__locals1.id == {22887}.Id.Tool_UnselectAll)
				{
					label.BasicColor = new Color(144, 171, 144);
					base.EvClick += delegate(ClickUiEventArgs {22900})
					{
						if (CS$<>8__locals1.id == {22887}.Id.Tool_UnselectAll)
						{
							using (Dictionary<{22887}.Id, Reactive<bool>>.Enumerator enumerator = {22887}.Statuses.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									KeyValuePair<{22887}.Id, Reactive<bool>> keyValuePair = enumerator.Current;
									keyValuePair.Value.Value = false;
								}
								return;
							}
						}
						foreach (KeyValuePair<{22887}.Id, Reactive<bool>> keyValuePair2 in {22887}.Statuses)
						{
							keyValuePair2.Value.Value = !{22887}.NotSelectedByDefault.Contains(keyValuePair2.Key);
						}
					};
				}
				else
				{
					{22887}.Id id = CS$<>8__locals1.id;
					bool flag = id <= {22887}.Id.LineDisplay || id == {22887}.Id.Center;
					if (flag)
					{
						label.BasicColor = new Color(175, 144, 171);
					}
					else
					{
						base.UpdateComplete += delegate(UiControl {22902})
						{
							label.BasicColor = (CS$<>8__locals1.status.Value ? new Color(178, 162, 139) : new Color(103, 113, 124));
							icon.Opacity = (CS$<>8__locals1.status.Value ? 1f : 0.4f);
						};
					}
				}
				base.EvClick += delegate(ClickUiEventArgs {22901})
				{
					if (CS$<>8__locals1.id == {22887}.Id.LineDisplay || CS$<>8__locals1.id == {22887}.Id.CustomMarker)
					{
						return;
					}
					CS$<>8__locals1.status.Value = !CS$<>8__locals1.status.Value;
				};
			}

			// Token: 0x06001656 RID: 5718 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserBackRender()
			{
			}

			// Token: 0x06001657 RID: 5719 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserFrontRender()
			{
			}

			// Token: 0x06001658 RID: 5720 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserUpdate(ref FrameTime {22899})
			{
			}
		}
	}
}
