using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001A0 RID: 416
	internal sealed class {18981} : {17625}
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x00049A50 File Offset: 0x00047C50
		public {18981}() : base(720f, {17625}.c_back1, new {17604}
		{
			AddBackgroundParticles = true
		}, Rectangle.Empty, Array.Empty<{17625}.DynamicTittle>())
		{
			{18981}.CurrentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{18981}.CurrentInstance = null;
			};
			base.AddBigHeadBar(AtlasGameGui.Texture.Tex, {19037}.header_win, Local.PassingMapsJoinGui_0(Session.Account.PassingMapScrolls), Color.White, -20f, 35f);
			ResourceInfo resourceInfo = Gameplay.ItemsInfo.FromID(35);
			Marker marker = base.Pos;
			base.AddChild(new Label(new Vector2(marker.Center.X, base.Pos.XY.Y + 34f), Fonts.Arial_10, Color.Wheat * 0.7f, resourceInfo.Description.Replace("#", ""), PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			marker = new Marker(282f, 13f, 461f, 558f);
			Marker marker2 = base.Pos;
			base.AddChild(new Form(marker.Offset(marker2.XY), new Rectangle(1878, 3226, 301, 221), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			marker2 = new Marker(282f, 13f, 461f, 558f);
			marker = base.Pos;
			base.AddChild(new Form(marker2.Offset(marker.XY), new Rectangle(1878, 3226, 301, 221), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			base.ComposeGraphicItems(2, (from {19001} in Gameplay.MapsForPassing
			where {19001}.ID != 7 && {19001}.ID != 8
			select {19001}).Select(new Func<MapForPassingInfo, {17625}.ComposeGraphicItemsParam>(this.{18989})).ToArray<{17625}.ComposeGraphicItemsParam>());
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00049C53 File Offset: 0x00047E53
		private IEnumerable<ValueTuple<string, bool>> GetRistrictions(MapForPassingInfo {18982})
		{
			{18981}.<GetRistrictions>d__8 <GetRistrictions>d__ = new {18981}.<GetRistrictions>d__8(-2);
			<GetRistrictions>d__.<>3__map = {18982};
			return <GetRistrictions>d__;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00049C64 File Offset: 0x00047E64
		private void {18983}({17625}.GridElement {18984}, MapForPassingInfo {18985})
		{
			{18981}.<>c__DisplayClass9_0 CS$<>8__locals1 = new {18981}.<>c__DisplayClass9_0();
			CS$<>8__locals1.map = {18985};
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.element = {18984};
			{17625}.GridElement.ShowSelectedItem = CS$<>8__locals1.element;
			StackForm stackForm = this.{19000};
			if (stackForm != null)
			{
				stackForm.RemoveFromContainer();
			}
			this.{19000} = new StackForm(new Vector2(base.ContentArea.End.X - 244f, base.ContentArea.XY.Y + 15f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(this.{19000});
			if (this.{18999} != CS$<>8__locals1.map)
			{
				this.{18998} = 1;
			}
			this.{18999} = CS$<>8__locals1.map;
			CS$<>8__locals1.diffAppendix = ((this.{18998} == 1) ? "+" : ((this.{18998} == 2) ? "++" : "+++"));
			this.{19000}.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_14, new Color(171, 183, 204) * 0.5f, CS$<>8__locals1.map.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			this.{19000}.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_12, new Color(171, 183, 204) * 0.5f, Local.passing_map_diff + ":", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			for (int i = 0; i < 3; i++)
			{
				Button button = new Button(Vector2.Zero, new Rectangle(1549, 777, 205, 33), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button.SetText((i == 0) ? Local.PassingMapsJoinGui_8 : ((i == 1) ? Local.PassingMapsJoinGui_9 : Local.PassingMapsJoinGui_10), Fonts.Philosopher_12, Color.White * 0.9f, true);
				int d = i + 1;
				IEnumerable<byte> openedPassingMaps = Session.Account.OpenedPassingMaps;
				Func<byte, bool> predicate;
				if ((predicate = CS$<>8__locals1.<>9__3) == null)
				{
					predicate = (CS$<>8__locals1.<>9__3 = ((byte {19008}) => (short){19008} == CS$<>8__locals1.map.ID));
				}
				if (openedPassingMaps.Count(predicate) != 2 && i == 2)
				{
					button.AddChild(new Form(button.Pos, new Rectangle(1785, 463, 205, 33), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false,
						RenderToDepthMap = false
					});
				}
				else
				{
					button.AddChild(new Form(button.Pos, new Rectangle(1554, 581 + i * 31, 184, 30), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false,
						RenderToDepthMap = false
					});
					button.EvClick += delegate(ClickUiEventArgs {19013})
					{
						CS$<>8__locals1.<>4__this.{18998} = d;
						CS$<>8__locals1.<>4__this.{18983}(CS$<>8__locals1.element, CS$<>8__locals1.<>4__this.{18999});
					};
					button.ToolTipState = new ToolTipState("", Local.passingMapDiffTooltip, Array.Empty<ToolTipCharacteristics>());
				}
				if (i + 1 == this.{18998})
				{
					button.AddChild(new Form(button.Pos, new Rectangle(1549, 743, 205, 33), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false,
						RenderToDepthMap = false
					});
				}
				this.{19000}.AddItem(new UiControl[]
				{
					button
				});
			}
			this.{19000}.AddSpace(5f);
			this.{19000}.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_12, new Color(171, 183, 204) * 0.5f, Local.PassingMapsJoinGui_3, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			float num;
			foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)PassingMapDiffCardHelper.GetRewards(CS$<>8__locals1.map, this.{18998}, out num).ResourceInfo))
			{
				Form form = new Form(new Marker(0f, 0f, 36f, 36f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Form form2 = form;
				UiControl[] array = new UiControl[3];
				int num2 = 0;
				Vector2 zero = Vector2.Zero;
				array[num2] = new Image(new Marker(ref zero, 36f, 36f), gsilocalEnumerablePair.Info.IconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				array[1] = new Label(new Vector2(39f, 1f), Fonts.Arial_10, Color.White * 0.6f, gsilocalEnumerablePair.Info.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				array[2] = new Label(new Vector2(39f, 21f), Fonts.Arial_10Bold, Color.Gold, gsilocalEnumerablePair.Count.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form2.AddChild(array);
				this.{19000}.AddSpace(1f);
				this.{19000}.AddItem(new UiControl[]
				{
					form
				});
			}
			CS$<>8__locals1.<OnClick>g__makeOutstandingIcon|0(CommonAtlas.goldIconMany64, Local.gold_and_xp, "");
			this.{19000}.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_12, Color.White * 0.5f, Local.PassingMapsJoinGui_6 + ((CS$<>8__locals1.map.ID == 5) ? "?" : ((int)((float)CS$<>8__locals1.map.MiddleСonsumptionCBalls * (0.7f + 0.2f * (float)this.{18998}))).ToString()), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			this.{19000}.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_12, (CS$<>8__locals1.map.ScrollsCost(this.{18998}).Value > Session.Account.PassingMapScrolls) ? Color.OrangeRed : Color.White, Local.PassingMapsJoinGui_7(CS$<>8__locals1.map.ScrollsCost(this.{18998}).Value), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			Button button2 = new Button(Vector2.Zero, new Rectangle(1549, 777, 205, 33), PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.PassingMapsJoinGui_11, Fonts.Philosopher_12, Color.White * 0.8f, false);
			this.{19000}.AddItem(new UiControl[]
			{
				button2
			});
			if (CS$<>8__locals1.map.ScrollsCost(this.{18998}).Value <= Session.Account.PassingMapScrolls)
			{
				if (!this.GetRistrictions(CS$<>8__locals1.map).Any((ValueTuple<string, bool> {19005}) => {19005}.Item2))
				{
					button2.EvClick += delegate(ClickUiEventArgs {19012})
					{
						CS$<>8__locals1.<>4__this.{18987}();
					};
					return;
				}
			}
			button2.Opacity = 0.3f;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0004A340 File Offset: 0x00048540
		private void {18986}()
		{
			if (Global.Player.IsPortEntry)
			{
				Global.Game.ScenePort.CheckSelectedShip(new Action(this.{18993}), false, false);
			}
			else
			{
				if (!Global.Player.CheckBattleTimerAndSpeed())
				{
					return;
				}
				{18945}.TryShowAcceptingMode(Local.NewWaveScreen_1, Local.ExitScreen_2, 5000f, new Action(this.{18996}), true);
			}
			base.RemoveFromContainer();
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0004A3B0 File Offset: 0x000485B0
		private void {18987}()
		{
			if (Session.Account.Quests.ProgressRunningQuests.Any((QuestRunningProgress {19006}) => {19006}.Info.ShipEffects.Length != 0))
			{
				new {17312}(Local.not_available_quest);
				return;
			}
			if (Session.Account.PassingMapScrolls < this.{18999}.ScrollsCost(this.{18998}).Value)
			{
				return;
			}
			if (!Global.Player.CheckBattleTimerAndSpeed())
			{
				return;
			}
			if (this.{18999}.PlayersCount != 1)
			{
				new {17312}(Local.PassingMapsJoinGui_17, new Action(this.{18997}), false);
				return;
			}
			this.{18986}();
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0004A45E File Offset: 0x0004865E
		protected override void UserUpdate(ref FrameTime {18988})
		{
			base.UserUpdate(ref {18988});
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0004A5C4 File Offset: 0x000487C4
		[CompilerGenerated]
		private {17625}.ComposeGraphicItemsParam {18989}(MapForPassingInfo {18990})
		{
			bool available = true;
			return new {17625}.ComposeGraphicItemsParam({18990}, {18990}.Name, OtherTextures.Images, {18981}.textures[(int)({18990}.ID - 1)], new Action<{17625}.GridElement>(this.{18991}), delegate(Form {19007})
			{
				ValueTuple<string, bool>[] source = this.GetRistrictions({18990}).ToArray<ValueTuple<string, bool>>();
				if (available)
				{
					if (!source.Any((ValueTuple<string, bool> {19002}) => {19002}.Item2))
					{
						goto IL_67;
					}
				}
				{19007}.AddChild(new Image(new Vector2(18f), OtherTextures.Images, {18981}.blocked, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				IL_67:
				if ({18990}.PlayersCount != 1)
				{
					{19007}.AddChild(new UiControl[]
					{
						new Form({19007}.Pos.XY + new Vector2(24f, 22f), {18981}.Patter_SocIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							AnimatedFocus = false
						},
						new Label({19007}.Pos.XY + new Vector2((float)(23 + {18981}.Patter_SocIcon.Width + 4), 23f), Fonts.Philosopher_14, Color.White, {18990}.PlayersCount.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
				}
				Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>((from {19003} in source
				select new ToolTipCharacteristics({19003}.Item1, {19003}.Item2 ? CharacteristicsColor.Orange : CharacteristicsColor.Wheat)).ToArray<ToolTipCharacteristics>());
				if (source.All((ValueTuple<string, bool> {19004}) => !{19004}.Item2))
				{
					Tlist<ToolTipCharacteristics> tlist2 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.press_to_open, CharacteristicsColor.Lime);
					tlist2.Add(toolTipCharacteristics);
				}
				{19007}.ExToolTip(new ToolTip(new ToolTipState({18990}.Name, {18990}.Introduction, tlist.ToArray())));
			});
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0004A636 File Offset: 0x00048836
		[CompilerGenerated]
		private void {18991}({17625}.GridElement {18992})
		{
			this.{18983}({18992}, (MapForPassingInfo){18992}.Id);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0004A64A File Offset: 0x0004884A
		[CompilerGenerated]
		private void {18993}()
		{
			{18945}.TryShowAcceptingMode(Local.NewWaveScreen_1, Local.ExitScreen_2, 5000f, new Action(this.{18994}), true);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0004A66E File Offset: 0x0004886E
		[CompilerGenerated]
		private void {18994}()
		{
			Global.Game.ScenePort.FastExitWithoutCheckShip(new Action(this.{18995}), this.{18999}.Name);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0004A698 File Offset: 0x00048898
		[CompilerGenerated]
		private void {18995}()
		{
			GSI treasuryMaps = Session.Account.TreasuryMaps;
			treasuryMaps[35] = treasuryMaps[35] - this.{18999}.ScrollsCost(this.{18998}).Value;
			Global.Network.Send(new OnPassingMapOpenQuery((byte)this.{18999}.ID, this.{18998}));
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0004A700 File Offset: 0x00048900
		[CompilerGenerated]
		private void {18996}()
		{
			GSI treasuryMaps = Session.Account.TreasuryMaps;
			treasuryMaps[35] = treasuryMaps[35] - this.{18999}.ScrollsCost(this.{18998}).Value;
			Global.Network.Send(new OnPassingMapOpenQuery((byte)this.{18999}.ID, this.{18998}));
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0004A768 File Offset: 0x00048968
		[CompilerGenerated]
		private void {18997}()
		{
			this.{18986}();
		}

		// Token: 0x0400087B RID: 2171
		internal static {18981} CurrentInstance;

		// Token: 0x0400087C RID: 2172
		private static readonly Rectangle[] textures = new Rectangle[]
		{
			new Rectangle(0, 250, 205, 124),
			new Rectangle(206, 250, 205, 124),
			new Rectangle(412, 250, 205, 124),
			new Rectangle(618, 375, 205, 124),
			new Rectangle(206, 125, 205, 124),
			new Rectangle(618, 250, 205, 124),
			default(Rectangle),
			default(Rectangle),
			new Rectangle(0, 375, 205, 124),
			new Rectangle(206, 375, 205, 124),
			new Rectangle(412, 375, 205, 124),
			new Rectangle(618, 125, 205, 124)
		};

		// Token: 0x0400087D RID: 2173
		private static readonly Rectangle blocked = new Rectangle(0, 0, 205, 124);

		// Token: 0x0400087E RID: 2174
		private static readonly Rectangle Patter_SocIcon = new Rectangle(2518, 3869, 19, 19);

		// Token: 0x0400087F RID: 2175
		private int {18998} = 1;

		// Token: 0x04000880 RID: 2176
		private MapForPassingInfo {18999};

		// Token: 0x04000881 RID: 2177
		private StackForm {19000};
	}
}
