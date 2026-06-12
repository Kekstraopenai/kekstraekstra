using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000385 RID: 901
	internal class {22094} : {17625}
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x000A5BF4 File Offset: 0x000A3DF4
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x000A5BFC File Offset: 0x000A3DFC
		protected override float winterStyleYDecorOffset { get; set; } = 10f;

		// Token: 0x060013A0 RID: 5024 RVA: 0x000A5C08 File Offset: 0x000A3E08
		public {22094}(Action<ValueTuple<object, {22094}.Mode>> {22098}, {22094}.Mode {22099}) : base(700f, {17625}.c_back2, {17604}.InGameWindowWithoutDeco, {17625}.c_icStreeringWheel, {22094}.GetPages({22099}).ToArray<{17625}.DynamicTittle>())
		{
			if ({22094}.CurrentInstance != null)
			{
				throw new InvalidOperationException();
			}
			{22094}.CurrentInstance = this;
			this.{22146} = {22098};
			base.EvRemoveFromContainer += delegate()
			{
				{22094}.CurrentInstance = null;
			};
			bool flag = {22099} == {22094}.Mode.SelectLighthouse || {22099} == {22094}.Mode.SelectRespawnInSea;
			if (flag)
			{
				base.AddChild(this.{22101}({22099}));
				return;
			}
			base.ComposeDynamicTab(new Func<Form>[]
			{
				new Func<Form>(this.{22132}),
				new Func<Form>(this.{22133})
			});
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x000A5CCC File Offset: 0x000A3ECC
		private static IEnumerable<{17625}.DynamicTittle> GetPages({22094}.Mode {22100})
		{
			{22094}.<GetPages>d__19 <GetPages>d__ = new {22094}.<GetPages>d__19(-2);
			<GetPages>d__.<>3__mode = {22100};
			return <GetPages>d__;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x000A5CDC File Offset: 0x000A3EDC
		private static bool IsTravelAllowed()
		{
			if (Global.Player.NearPortType == PortEnteringType.Port)
			{
				return true;
			}
			if (Global.Player.NearPortType != PortEnteringType.PersonalIsle)
			{
				return false;
			}
			PersonalIsleStatus currentPersonalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
			if (currentPersonalIsle == null || !currentPersonalIsle.AllowWorldTravel)
			{
				return false;
			}
			bool flag = Session.Account.PersonalIsles.Data.Any((PersonalIsleStatus {22174}) => {22174}.AllowWorldTravel && {22174} != Global.Game.ScenePort.CurrentPersonalIsle);
			bool flag2 = Gameplay.WorldMap.Ports.Any(new Func<IslePortInfo, bool>(Session.Game.CanTravelToPort));
			return flag || flag2;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x000A5D84 File Offset: 0x000A3F84
		private Form {22101}({22094}.Mode {22102})
		{
			{22094}.<>c__DisplayClass21_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			Form form = new Form(base.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			CS$<>8__locals1.mapAreaMultiplier = 0.75f;
			CS$<>8__locals1.portsToJump = new Tlist<{22094}.IconData>();
			CS$<>8__locals1.mapUiInfo = default({22094}.MapUiData);
			CS$<>8__locals1.isPI = (Global.Player.NearPortType == PortEnteringType.PersonalIsle);
			CS$<>8__locals1.thisEntryPos = (CS$<>8__locals1.isPI ? Global.Game.ScenePort.CurrentPersonalIsle.MooringPosition : Global.Player.NearPort.EntryPos);
			CS$<>8__locals1.thisInHazardZone = Gameplay.IsInHazardZone(CS$<>8__locals1.thisEntryPos, 0f);
			if ({22102} == {22094}.Mode.SelectBigTravel)
			{
				this.{22142}(CS$<>8__locals1.thisEntryPos, true, ref CS$<>8__locals1);
				string text = Local.PortSelectLighthouseWindiw_travelBig;
				string {22108} = "";
				if (Session.Account.Gold >= Gameplay.WorldTravelPrice(Session.Account, true))
				{
					text += Local.price_gold(Gameplay.WorldTravelPrice(Session.Account, true));
				}
				else
				{
					{22108} = Local.price_gold(Gameplay.WorldTravelPrice(Session.Account, true));
				}
				bool flag = Session.Account.BlockWorldBigTravelTimeSec == 0f;
				foreach (IslePortInfo islePortInfo in ((IEnumerable<IslePortInfo>)Gameplay.WorldMap.Ports))
				{
					if (Session.Game.CanTravelToPort(islePortInfo))
					{
						Tlist<{22094}.IconData> portsToJump = CS$<>8__locals1.portsToJump;
						{22094}.IconData iconData = new {22094}.IconData();
						iconData.Position = islePortInfo.EntryPos;
						iconData.Icon = {22094}.IconData.Texture.Port;
						iconData.Name = islePortInfo.PortName;
						iconData.Available = flag;
						iconData.Key = islePortInfo;
						iconData.TooltipHeader = (flag ? null : Local.port_fast_travel_unavaliable);
						iconData.TooltipText = (flag ? null : Local.port_fast_travel_limit_reached);
						portsToJump.Add(iconData);
					}
				}
				CS$<>8__locals1.mapUiInfo = this.GetMapUiData(CS$<>8__locals1.portsToJump);
				this.LoadData(form, CS$<>8__locals1.portsToJump, CS$<>8__locals1.mapUiInfo, (Session.Account.BlockWorldBigTravelTimeSec > 0f) ? Local.available_after(StringHelper.TimeDHM((double)Session.Account.BlockWorldBigTravelTimeSec, false)) : text, {22108}, {22102});
			}
			else if ({22102} == {22094}.Mode.SelectNearTravelPort)
			{
				{22094}.<>c__DisplayClass21_1 CS$<>8__locals2;
				CS$<>8__locals2.portsOther = new Tlist<{22094}.IconData>();
				this.{22134}(CS$<>8__locals1.thisEntryPos, ref CS$<>8__locals1, ref CS$<>8__locals2);
				if (Global.Player.NearPortType == PortEnteringType.Port)
				{
					this.{22142}(CS$<>8__locals1.thisEntryPos, false, ref CS$<>8__locals1);
				}
				string text2 = Local.PortSelectLighthouseWindiw_travel;
				string text3 = "";
				if (Session.Account.Gold >= Gameplay.WorldTravelPrice(Session.Account, false))
				{
					text2 += Local.price_gold(Gameplay.WorldTravelPrice(Session.Account, false));
				}
				else
				{
					text3 = Local.price_gold(Gameplay.WorldTravelPrice(Session.Account, false));
				}
				if (CS$<>8__locals1.portsToJump.Size == 0 && CS$<>8__locals2.portsOther.Size == 0)
				{
					text2 = "";
					text3 = Local.PortSelectLighthouseWindiw_r;
				}
				if (Session.CountOfCapers > 0)
				{
					if (text3.Length > 0)
					{
						text3 += Environment.NewLine;
					}
					text3 += Local.PortTravel_capturedShipInfo;
				}
				this.LoadData(form, CS$<>8__locals1.portsToJump.UnionBy(CS$<>8__locals2.portsOther, ({22094}.IconData {22175}) => {22175}.Key), CS$<>8__locals1.mapUiInfo, (!Session.Account.AllowNextWorldTravel) ? Local.available_after(StringHelper.TimeDHM((double)Session.Account.BlockWorldTravelTimeSec, false)) : text2, text3, {22102});
			}
			else if ({22102} == {22094}.Mode.SelectLighthouse)
			{
				Tlist<{22094}.IconData> tlist = new Tlist<{22094}.IconData>();
				foreach (IslePortPharosInfo islePortPharosInfo in ((IEnumerable<IslePortPharosInfo>)Global.Player.NearPort.ReferencedPharosesTransformed))
				{
					Tlist<{22094}.IconData> tlist2 = tlist;
					{22094}.IconData iconData = new {22094}.IconData();
					iconData.Position = islePortPharosInfo.MapGlobalPosition;
					iconData.Icon = {22094}.IconData.Texture.Lighthouse;
					iconData.Color = Color.Lime;
					iconData.Key = tlist.Size;
					tlist2.Add(iconData);
				}
				CS$<>8__locals1.mapUiInfo = this.GetMapUiData(tlist);
				this.LoadData(form, tlist, CS$<>8__locals1.mapUiInfo, Local.PortSelectLighthouseWindiw_1, null, {22102});
			}
			else
			{
				if ({22102} != {22094}.Mode.SelectRespawnInSea)
				{
					throw new NotSupportedException();
				}
				List<IslePortPharosInfo> list = Gameplay.WorldMap.GetAvailableRespawnsInSea(Global.Player.Position, 1000f, Session.Game.NearPortHasManyPvpShips, Session.Account).ToList<IslePortPharosInfo>();
				if (list.Count <= 1)
				{
					list = Gameplay.WorldMap.GetAvailableRespawnsInSea(Global.Player.Position, 1500f, Session.Game.NearPortHasManyPvpShips, Session.Account).ToList<IslePortPharosInfo>();
				}
				if (list.Count == 0)
				{
					list.Add(Gameplay.WorldMap.GetNearAvailableRespawnInSea(Global.Player.Position, Session.Game.NearPortHasManyPvpShips, Session.Account));
				}
				Tlist<{22094}.IconData> tlist3 = new Tlist<{22094}.IconData>();
				using (List<IslePortPharosInfo>.Enumerator enumerator3 = list.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						IslePortPharosInfo item = enumerator3.Current;
						bool flag2 = Gameplay.WorldMap.TradersInSea.Any((TraderInSeaPlaceInfo {22182}) => {22182}.Position == item.MapGlobalPosition);
						Tlist<{22094}.IconData> tlist4 = tlist3;
						{22094}.IconData iconData = new {22094}.IconData();
						iconData.Position = item.MapGlobalPosition;
						iconData.Icon = (flag2 ? {22094}.IconData.Texture.Village : {22094}.IconData.Texture.Lighthouse);
						iconData.Color = Color.Lime;
						iconData.Key = item;
						tlist4.Add(iconData);
					}
				}
				CS$<>8__locals1.mapUiInfo = this.GetMapUiData(tlist3);
				this.LoadData(form, tlist3, CS$<>8__locals1.mapUiInfo, "", null, {22102});
			}
			return form;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x000A6388 File Offset: 0x000A4588
		private {22094}.MapUiData GetMapUiData(IEnumerable<{22094}.IconData> {22103})
		{
			Vector2 minPos = new Tlist<Vector2>((from {22176} in {22103}
			select {22176}.Position).Concat(new <>z__ReadOnlySingleElementList<Vector2>(Global.Player.NearPort.EntryPos))).GetMinPos();
			Vector2 maxPos = new Tlist<Vector2>((from {22177} in {22103}
			select {22177}.Position).Concat(new <>z__ReadOnlySingleElementList<Vector2>(Global.Player.NearPort.EntryPos))).GetMaxPos();
			minPos.Y *= 1.1f;
			maxPos.Y = maxPos.Y * 1.2f + 300f;
			Vector2 vector = (minPos + maxPos) * 0.5f;
			float num = 0f;
			foreach (Vector2 value in (from {22178} in {22103}
			select {22178}.Position).Concat(new <>z__ReadOnlySingleElementList<Vector2>(Global.Player.NearPort.EntryPos)))
			{
				num = Math.Max(num, ((value - vector) * new Vector2(2.5f, 1f)).Length());
			}
			float num2 = num * 1.6f + (float)((num > 3000f) ? 2000 : 800);
			num2 = Math.Min(num2, 25000f);
			Marker marker = new Marker(base.Pos.XY.X + 3f, base.Pos.XY.Y + 80f, base.Pos.WH.X - 6f, base.Pos.WH.Y - 85f).Border(-16f);
			Rectangle mapTextureArea = {20413}.SelectArea(vector, num2, marker.WH.Y, marker.WH.X);
			return new {22094}.MapUiData
			{
				LightousesCenter = vector,
				VisibleMapSize = num2,
				ContentArea = marker,
				MapTextureArea = mapTextureArea
			};
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x000A65F8 File Offset: 0x000A47F8
		private void LoadData(Form {22104}, IEnumerable<{22094}.IconData> {22105}, {22094}.MapUiData {22106}, string {22107}, string {22108}, {22094}.Mode {22109})
		{
			{22104}.AddChild(new Image({22106}.ContentArea, () => OtherTextures.WorldMap.Tex, () => {22106}.MapTextureArea, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			{22104}.AddChild(new Form({22106}.ContentArea.Border(16f, 12f), {22094}.iconMask, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
			TextBlockBuilder textBlockBuilder = TextBlockBuilder.CreateBlock(base.Pos.WH.X - 40f, {22107}, Color.White * 0.5f, Fonts.Philosopher_14, -1f);
			if (!string.IsNullOrEmpty({22108}))
			{
				textBlockBuilder.WriteLine({22108}, Color.Orange);
			}
			{22104}.AddChild(textBlockBuilder.Create(base.Pos.XY + new Vector2(20f, 70f)));
			bool flag = false;
			Vector2 {22122};
			Rectangle rectangle;
			string {22111};
			string {22112};
			if ({22109} == {22094}.Mode.SelectRespawnInSea)
			{
				{22122} = Global.Player.Position;
				rectangle = {22094}.iconPlayer;
				{22111} = Local.youre_here;
				{22112} = null;
			}
			else if (Global.Player.NearPortType == PortEnteringType.PersonalIsle)
			{
				{22122} = Global.Game.ScenePort.CurrentPersonalIsle.MooringPosition;
				rectangle = {22094}.iconVillage2;
				{22111} = Global.Game.ScenePort.CurrentPersonalIsle.Name;
				{22112} = Local.youre_here;
			}
			else
			{
				{22122} = Global.Player.NearPort.EntryPos;
				rectangle = {22094}.iconCurrentPort;
				{22111} = Global.Player.NearPort.PortName;
				{22112} = Local.youre_here;
			}
			AnimatedButton animatedButton = {22094}.IconHelper({22122}, {22106}.VisibleMapSize, {22106}.ContentArea, {22106}.LightousesCenter, rectangle, rectangle, flag ? 0.33f : 0.66f);
			Image image = new Image(animatedButton.Pos.ScaleOfCenter(1.5f), CommonAtlas.Texture.Tex, CommonAtlas.whiteDot, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Wheat
			};
			image.MoveToFrontLevel();
			animatedButton.MoveToFrontLevel();
			{22104}.AddChild(new UiControl[]
			{
				image,
				animatedButton
			});
			this.AddText({22104}, {22111}, {22112}, flag, animatedButton, Color.Wheat, new Color?(Color.Wheat), true);
			using (IEnumerator<{22094}.IconData> enumerator = {22105}.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					{22094}.IconData item = enumerator.Current;
					float {22128} = ({22109} == {22094}.Mode.SelectRespawnInSea) ? 0.83f : (flag ? 0.5f : 0.85f);
					AnimatedButton animatedButton2 = {22094}.IconHelper(item.Position, {22106}.VisibleMapSize, {22106}.ContentArea, {22106}.LightousesCenter, item.GetInactiveTexture(), item.GetActiveTexture(), {22128});
					Form form = new Form(animatedButton2.Pos, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						BasicColor = Color.Transparent
					};
					animatedButton2.Opacity = (item.Available ? 1f : 0.2f);
					if (!string.IsNullOrEmpty(item.TooltipHeader) || !string.IsNullOrEmpty(item.TooltipText))
					{
						form.ToolTip = new ToolTip(new ToolTipState(item.TooltipHeader, item.TooltipText, Array.Empty<ToolTipCharacteristics>()));
					}
					form.AddChild(animatedButton2);
					{22104}.AddChild(form);
					animatedButton2.AllowMouseInput = item.Available;
					animatedButton2.EvClick += delegate(ClickUiEventArgs {22184})
					{
						this.{22146}(new ValueTuple<object, {22094}.Mode>(item.Key, {22109}));
						this.BlockAndClose();
					};
					this.AddText({22104}, item.Name, item.WarningText, flag, animatedButton2, item.Color * (item.Available ? 1f : 0.3f), null, false);
				}
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x000A6A78 File Offset: 0x000A4C78
		private void AddText(Form {22110}, string {22111}, string {22112}, bool {22113}, AnimatedButton {22114}, Color {22115}, Color? {22116} = null, bool {22117} = false)
		{
			CustomSpriteFont {13343} = {22113} ? Fonts.Arial_9 : Fonts.Philosopher_14Bold;
			CustomSpriteFont {13343}2 = {22113} ? Fonts.Arial_8 : Fonts.Arial_10;
			if (!string.IsNullOrEmpty({22111}))
			{
				if (({22111}.Length > 13 || {22113}) && {22111}.Contains(' '))
				{
					int num = {22111}.LastIndexOf(' ');
					{22111} = {22111}.Substring(0, num) + Environment.NewLine + {22111}.Substring(num + 1, {22111}.Length - num - 1);
				}
				Label label = new Label(new Vector2({22114}.Pos.Center.X, {22114}.Pos.Center.Y), {13343}, {22115}, {22111}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center();
				label.Shadowed = true;
				label.RenderToDepthMap = false;
				{22110}.AddChild(label);
				float {11549} = label.Pos.WH.X * 0.5f + {22114}.Pos.WH.X * 0.5f;
				label.Pos = label.Pos.Offset({11549}, 0f);
				if (!string.IsNullOrEmpty({22112}))
				{
					Label label2 = new Label(new Vector2(label.Pos.XY.X, label.Pos.End.Y - 2f), {13343}2, ({22116} == null) ? new Color(226, 166, 154) : {22116}.Value, {22112}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					if ({22117})
					{
						Image image = new Image(label2.Pos.ScaleOfCenter(1.15f), CommonAtlas.Texture.Tex, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							BasicColor = Color.Black,
							Opacity = 0.5f
						};
						image.MoveToFrontLevel();
						{22110}.AddChild(image);
					}
					label2.MoveToFrontLevel();
					{22110}.AddChild(label2);
				}
			}
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000A6C70 File Offset: 0x000A4E70
		public static void CreateMapWithMask(Form {22118}, Marker {22119}, Vector2 {22120}, float {22121})
		{
			Rectangle area = {20413}.SelectArea({22120}, {22121}, {22119}.WH.Y, {22119}.WH.X);
			UiControl[] array = new UiControl[2];
			array[0] = new Image({22119}, () => OtherTextures.WorldMap.Tex, () => area, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			array[1] = new Form({22119}.Border({22119}.WH.X * 0.018f, {22119}.WH.Y * 0.015f), {22094}.iconMask, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			{22118}.AddChild(array);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x000A6D28 File Offset: 0x000A4F28
		public static AnimatedButton IconHelper(Vector2 {22122}, float {22123}, Marker {22124}, Vector2 {22125}, Rectangle {22126}, Rectangle {22127}, float {22128})
		{
			{22128} *= 1.3f;
			Vector2 value;
			value.Y = -({22122}.X - {22125}.X);
			value.X = {22122}.Y - {22125}.Y;
			value = {22124}.Center + value / new Vector2({22123}, {22123}) * {22124}.WH.X - {22126}.WidthHeight() * {22128} * 0.5f;
			Vector2 vector = {22126}.WidthHeight() * {22128};
			return new AnimatedButton(new Marker(ref value, ref vector), {22126}, {22126}, {22127}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x000A6DD4 File Offset: 0x000A4FD4
		public static void MakeWorldTravel(bool {22129}, {22094}.Mode {22130}, object {22131})
		{
			{22094}.<>c__DisplayClass28_0 CS$<>8__locals1 = new {22094}.<>c__DisplayClass28_0();
			CS$<>8__locals1.needPay = {22129};
			CS$<>8__locals1.isBigTravel = ({22130} == {22094}.Mode.SelectBigTravel);
			if (CS$<>8__locals1.needPay && Session.Account.Gold < Gameplay.WorldTravelPrice(Session.Account, CS$<>8__locals1.isBigTravel))
			{
				return;
			}
			{22094}.<>c__DisplayClass28_0 CS$<>8__locals2 = CS$<>8__locals1;
			IslePortInfo islePortInfo = {22131} as IslePortInfo;
			Vector2 targetPos;
			if (islePortInfo == null)
			{
				PersonalIsleStatus personalIsleStatus = {22131} as PersonalIsleStatus;
				if (personalIsleStatus == null)
				{
					string str = "MakeWorldTravel ";
					Type type = {22131}.GetType();
					throw new NotSupportedException(str + ((type != null) ? type.ToString() : null));
				}
				targetPos = personalIsleStatus.MooringPosition;
			}
			else
			{
				targetPos = islePortInfo.EntryPos;
			}
			CS$<>8__locals2.targetPos = targetPos;
			byte b = Gameplay.WorldMap.Shallows.Get(CS$<>8__locals1.targetPos);
			if (b != 0)
			{
				if (Session.Account.Shipyard.List.Max((PlayerShipDynamicInfo {22179}) => {22179}.CraftFrom.Rank) < (int)b)
				{
					new {17312}(Local.shallow_error_travel(b));
					return;
				}
			}
			Global.Game.ScenePort.CheckSelectedShip(delegate
			{
				{22094}.<>c__DisplayClass28_1 CS$<>8__locals3 = new {22094}.<>c__DisplayClass28_1();
				CS$<>8__locals3.CS$<>8__locals1 = CS$<>8__locals1;
				{22094}.<>c__DisplayClass28_1 CS$<>8__locals4 = CS$<>8__locals3;
				Rectangle uiarea = Engine.GS.UIArea;
				CS$<>8__locals4.blocker = new Form(new Marker(ref uiarea), AtlasPortGui.whitePixel, PositionAlignment.Both, PositionAlignment.Both)
				{
					AnimatedFocus = false,
					BasicColor = Color.Black
				};
				new UiOpacityAnimation(CS$<>8__locals3.blocker, 0f, 1f, 2000f);
				new UiActor(CS$<>8__locals3.blocker, delegate()
				{
					CS$<>8__locals3.blocker.RenderToDepthMap = false;
					if (CS$<>8__locals3.CS$<>8__locals1.needPay)
					{
						Session.Account.Gold -= Gameplay.WorldTravelPrice(Session.Account, CS$<>8__locals3.CS$<>8__locals1.isBigTravel);
					}
					GameplayHelper.AutomoveResources();
					Global.Player.ApplyWorldTravel(CS$<>8__locals3.CS$<>8__locals1.targetPos, CS$<>8__locals3.CS$<>8__locals1.isBigTravel);
					Global.Settings.DeathController.BlockPortExitSec = 0f;
					Global.Network.Send(new OnApplyTravelMsg(CS$<>8__locals3.CS$<>8__locals1.targetPos, CS$<>8__locals3.CS$<>8__locals1.isBigTravel, CS$<>8__locals3.CS$<>8__locals1.needPay));
					Global.Game.ScenePort.UpdateGuiForViewShip();
					Global.Game.ScenePort.RefreshVisualScene();
					EducationHelper.MakeFlag(EducationOnboarding.WorldTravel, true);
				});
				new UiOpacityAnimation(CS$<>8__locals3.blocker, 1f, 0f, 2000f);
				new UiRemoveAction(CS$<>8__locals3.blocker);
			}, true, true);
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x000A6FC1 File Offset: 0x000A51C1
		[CompilerGenerated]
		private Form {22132}()
		{
			return this.{22101}({22094}.Mode.SelectNearTravelPort);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x000A6FCA File Offset: 0x000A51CA
		[CompilerGenerated]
		private Form {22133}()
		{
			return this.{22101}({22094}.Mode.SelectBigTravel);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x000A6FD4 File Offset: 0x000A51D4
		[CompilerGenerated]
		private void {22134}(Vector2 {22135}, ref {22094}.<>c__DisplayClass21_0 {22136}, ref {22094}.<>c__DisplayClass21_1 {22137})
		{
			if ({22136}.isPI)
			{
				int num = 3;
				IEnumerable<IslePortInfo> ports = Gameplay.WorldMap.Ports;
				Func<IslePortInfo, float> <>9__4;
				Func<IslePortInfo, float> keySelector;
				if ((keySelector = <>9__4) == null)
				{
					keySelector = (<>9__4 = ((IslePortInfo {22180}) => Vector2.DistanceSquared({22180}.EntryPos, {22135})));
				}
				foreach (IslePortInfo {22138} in ports.OrderBy(keySelector))
				{
					if (this.<Composer>g__CheckPort|21_2({22138}, {22137}.portsOther, false, ref {22136}) && --num == 0)
					{
						break;
					}
				}
				{22136}.mapUiInfo = this.GetMapUiData({22137}.portsOther);
			}
			else
			{
				foreach (IslePortInfo {22138}2 in ((IEnumerable<IslePortInfo>)Global.Player.NearPort.PortsToJump))
				{
					this.<Composer>g__CheckPort|21_2({22138}2, {22136}.portsToJump, false, ref {22136});
				}
				{22136}.mapUiInfo = this.GetMapUiData({22136}.portsToJump);
			}
			Rectangle mapTextureArea = {22136}.mapUiInfo.MapTextureArea;
			new Marker(ref mapTextureArea);
			IEnumerable<IslePortInfo> ports2 = Gameplay.WorldMap.Ports;
			Func<IslePortInfo, float> <>9__5;
			Func<IslePortInfo, float> keySelector2;
			if ((keySelector2 = <>9__5) == null)
			{
				keySelector2 = (<>9__5 = ((IslePortInfo {22181}) => Vector2.DistanceSquared({22181}.EntryPos, {22135})));
			}
			foreach (IslePortInfo islePortInfo in ports2.OrderBy(keySelector2))
			{
				if ({20413}.IsPortInRenderZone(islePortInfo.EntryPos, {22136}.mapUiInfo.LightousesCenter, {22136}.mapUiInfo.VisibleMapSize * {22136}.mapAreaMultiplier, {22136}.mapUiInfo.ContentArea.WH.Y, {22136}.mapUiInfo.ContentArea.WH.X))
				{
					this.<Composer>g__CheckPort|21_2(islePortInfo, {22137}.portsOther, true, ref {22136});
				}
			}
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x000A71C8 File Offset: 0x000A53C8
		[CompilerGenerated]
		private bool <Composer>g__CheckPort|21_2(IslePortInfo {22138}, Tlist<{22094}.IconData> {22139}, bool {22140} = false, ref {22094}.<>c__DisplayClass21_0 {22141})
		{
			if ({22138}.EntryPos == {22141}.thisEntryPos)
			{
				return false;
			}
			bool flag = {22138}.Type == PortType.PirateBay;
			bool flag2 = {22141}.thisInHazardZone == Gameplay.IsInHazardZone({22138}.EntryPos, 0f);
			bool flag3 = Session.Account.AllowNextWorldTravel && !flag && !{22140} && flag2;
			{22094}.IconData iconData = new {22094}.IconData();
			iconData.Position = {22138}.EntryPos;
			iconData.Icon = {22094}.IconData.Texture.Port;
			iconData.Name = {22138}.PortName;
			iconData.Available = flag3;
			iconData.Key = {22138};
			iconData.WarningText = ((flag3 && Global.Player.UsedShipPlayer.CraftFrom.Rank < {22138}.ShallowUpperRang) ? Local.shallow : null);
			iconData.TooltipHeader = (flag3 ? null : Local.port_fast_travel_unavaliable);
			iconData.TooltipText = (flag3 ? null : ((!Session.Account.AllowNextWorldTravel) ? (((int)Session.Account.UsedWorldTravelCount >= 1 + Session.Account.CaptainSkills[PDynamicAccountBonus.СWorldTravellingLevel]) ? Local.port_fast_travel_limit_reached : Local.n_left(StringHelper.TimeMMMSS((double)Session.Account.BlockWorldTravelTimeSec))) : (flag ? Local.unavaliable_for_pirates : ((!flag2) ? Local.beyond_hazard_zone : ({22140} ? Local.port_is_to_far : null)))));
			{22139}.AddIfNotContains(iconData);
			return true;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x000A7320 File Offset: 0x000A5520
		[CompilerGenerated]
		private void {22142}(Vector2 {22143}, bool {22144} = false, ref {22094}.<>c__DisplayClass21_0 {22145})
		{
			int num = {22144} ? 3 : 0;
			IEnumerable<PersonalIsleStatus> data = Session.Account.PersonalIsles.Data;
			Func<PersonalIsleStatus, float> <>9__7;
			Func<PersonalIsleStatus, float> keySelector;
			if ((keySelector = <>9__7) == null)
			{
				keySelector = (<>9__7 = ((PersonalIsleStatus {22183}) => Vector2.DistanceSquared({22183}.MooringPosition, {22143})));
			}
			foreach (PersonalIsleStatus personalIsleStatus in data.OrderBy(keySelector))
			{
				if (personalIsleStatus != Global.Game.ScenePort.CurrentPersonalIsle && ({22144} || {20413}.IsPortInRenderZone(personalIsleStatus.MooringPosition, {22145}.mapUiInfo.LightousesCenter, {22145}.mapUiInfo.VisibleMapSize * {22145}.mapAreaMultiplier, {22145}.mapUiInfo.ContentArea.WH.Y, {22145}.mapUiInfo.ContentArea.WH.X)))
				{
					bool thisInHazardZone = {22145}.thisInHazardZone;
					Vector2 mooringPosition = personalIsleStatus.MooringPosition;
					bool flag = thisInHazardZone == Gameplay.IsInHazardZone(mooringPosition, 0f);
					bool flag2 = {22144} ? (Session.Account.BlockWorldBigTravelTimeSec == 0f) : (Session.Account.AllowNextWorldTravel && flag);
					bool flag3 = personalIsleStatus.AllowWorldTravel && flag2;
					Tlist<{22094}.IconData> portsToJump = {22145}.portsToJump;
					{22094}.IconData iconData = new {22094}.IconData();
					iconData.Position = personalIsleStatus.Place.GlobalPosition;
					iconData.Icon = {22094}.IconData.Texture.Lighthouse;
					iconData.Name = (string.IsNullOrEmpty(personalIsleStatus.Name) ? Local.FcTemp_PersonalIsle : personalIsleStatus.Name);
					iconData.Available = flag3;
					iconData.Key = personalIsleStatus;
					iconData.TooltipHeader = (flag3 ? null : ({22144} ? Local.personal_isle_fast_travel_unavaliable : Local.port_fast_travel_unavaliable));
					iconData.TooltipText = (flag3 ? null : ((!personalIsleStatus.AllowWorldTravel) ? Local.port_fast_travel_build_lighthouse : ({22144} ? Local.personal_isle_fast_travel_limit_reached : ((!flag2) ? Local.port_fast_travel_limit_reached : ((!flag) ? Local.beyond_hazard_zone : null)))));
					portsToJump.AddIfNotContains(iconData);
					if (--num == 0)
					{
						break;
					}
				}
			}
		}

		// Token: 0x040011C9 RID: 4553
		public static {22094} CurrentInstance;

		// Token: 0x040011CA RID: 4554
		private static readonly Rectangle iconLighthouse1 = new Rectangle(421, 43, 39, 39);

		// Token: 0x040011CB RID: 4555
		private static readonly Rectangle iconLighthouse2 = new Rectangle(381, 43, 39, 39);

		// Token: 0x040011CC RID: 4556
		private static readonly Rectangle iconPort1 = new Rectangle(340, 117, 39, 39);

		// Token: 0x040011CD RID: 4557
		private static readonly Rectangle iconPort2 = new Rectangle(340, 77, 39, 39);

		// Token: 0x040011CE RID: 4558
		private static readonly Rectangle iconVillage1 = new Rectangle(494, 322, 39, 39);

		// Token: 0x040011CF RID: 4559
		private static readonly Rectangle iconVillage2 = new Rectangle(453, 322, 39, 39);

		// Token: 0x040011D0 RID: 4560
		private static readonly Rectangle iconPlayer = new Rectangle(907, 43, 44, 45);

		// Token: 0x040011D1 RID: 4561
		private static readonly Rectangle iconCurrentPort = new Rectangle(381, 83, 68, 73);

		// Token: 0x040011D2 RID: 4562
		private static readonly Rectangle iconMask = new Rectangle(817, 581, 175, 250);

		// Token: 0x040011D3 RID: 4563
		private readonly Action<ValueTuple<object, {22094}.Mode>> {22146};

		// Token: 0x040011D4 RID: 4564
		private const int personalIslesLimit = 3;

		// Token: 0x040011D5 RID: 4565
		[CompilerGenerated]
		private float {22147};

		// Token: 0x02000386 RID: 902
		public enum Mode
		{
			// Token: 0x040011D7 RID: 4567
			SelectLighthouse,
			// Token: 0x040011D8 RID: 4568
			SelectNearTravelPort,
			// Token: 0x040011D9 RID: 4569
			SelectBigTravel,
			// Token: 0x040011DA RID: 4570
			SelectRespawnInSea
		}

		// Token: 0x02000387 RID: 903
		[RequiredMember]
		private readonly struct IconData
		{
			// Token: 0x17000190 RID: 400
			// (get) Token: 0x060013B0 RID: 5040 RVA: 0x000A7548 File Offset: 0x000A5748
			// (set) Token: 0x060013B1 RID: 5041 RVA: 0x000A7550 File Offset: 0x000A5750
			[RequiredMember]
			public Vector2 Position { get; set; }

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x060013B2 RID: 5042 RVA: 0x000A7559 File Offset: 0x000A5759
			// (set) Token: 0x060013B3 RID: 5043 RVA: 0x000A7561 File Offset: 0x000A5761
			[RequiredMember]
			public {22094}.IconData.Texture Icon { get; set; }

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x060013B4 RID: 5044 RVA: 0x000A756A File Offset: 0x000A576A
			// (set) Token: 0x060013B5 RID: 5045 RVA: 0x000A7572 File Offset: 0x000A5772
			[RequiredMember]
			public object Key { get; set; }

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x060013B6 RID: 5046 RVA: 0x000A757B File Offset: 0x000A577B
			// (set) Token: 0x060013B7 RID: 5047 RVA: 0x000A7583 File Offset: 0x000A5783
			public string Name { get; set; }

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x060013B8 RID: 5048 RVA: 0x000A758C File Offset: 0x000A578C
			// (set) Token: 0x060013B9 RID: 5049 RVA: 0x000A7594 File Offset: 0x000A5794
			public Color Color { get; set; }

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x060013BA RID: 5050 RVA: 0x000A759D File Offset: 0x000A579D
			// (set) Token: 0x060013BB RID: 5051 RVA: 0x000A75A5 File Offset: 0x000A57A5
			public bool Available { get; set; }

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x060013BC RID: 5052 RVA: 0x000A75AE File Offset: 0x000A57AE
			// (set) Token: 0x060013BD RID: 5053 RVA: 0x000A75B6 File Offset: 0x000A57B6
			public string WarningText { get; set; }

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x060013BE RID: 5054 RVA: 0x000A75BF File Offset: 0x000A57BF
			// (set) Token: 0x060013BF RID: 5055 RVA: 0x000A75C7 File Offset: 0x000A57C7
			public string TooltipHeader { get; set; }

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x060013C0 RID: 5056 RVA: 0x000A75D0 File Offset: 0x000A57D0
			// (set) Token: 0x060013C1 RID: 5057 RVA: 0x000A75D8 File Offset: 0x000A57D8
			public string TooltipText { get; set; }

			// Token: 0x060013C2 RID: 5058 RVA: 0x000A75E4 File Offset: 0x000A57E4
			[Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
			[CompilerFeatureRequired("RequiredMembers")]
			public IconData()
			{
				this.Position = default(Vector2);
				this.Icon = {22094}.IconData.Texture.Lighthouse;
				this.Key = null;
				this.Name = string.Empty;
				this.Color = Color.Wheat * 0.8f;
				this.Available = true;
				this.WarningText = null;
				this.TooltipHeader = null;
				this.TooltipText = null;
			}

			// Token: 0x060013C3 RID: 5059 RVA: 0x000A7648 File Offset: 0x000A5848
			public Rectangle GetActiveTexture()
			{
				Rectangle result;
				switch (this.Icon)
				{
				case {22094}.IconData.Texture.Lighthouse:
					result = {22094}.iconLighthouse1;
					break;
				case {22094}.IconData.Texture.Port:
					result = {22094}.iconPort1;
					break;
				case {22094}.IconData.Texture.Village:
					result = {22094}.iconVillage1;
					break;
				default:
					result = Rectangle.Empty;
					break;
				}
				return result;
			}

			// Token: 0x060013C4 RID: 5060 RVA: 0x000A7690 File Offset: 0x000A5890
			public Rectangle GetInactiveTexture()
			{
				Rectangle result;
				switch (this.Icon)
				{
				case {22094}.IconData.Texture.Lighthouse:
					result = {22094}.iconLighthouse2;
					break;
				case {22094}.IconData.Texture.Port:
					result = {22094}.iconPort2;
					break;
				case {22094}.IconData.Texture.Village:
					result = {22094}.iconVillage2;
					break;
				default:
					result = Rectangle.Empty;
					break;
				}
				return result;
			}

			// Token: 0x040011DB RID: 4571
			[CompilerGenerated]
			private readonly Vector2 {22157};

			// Token: 0x040011DC RID: 4572
			[CompilerGenerated]
			private readonly {22094}.IconData.Texture {22158};

			// Token: 0x040011DD RID: 4573
			[CompilerGenerated]
			private readonly object {22159};

			// Token: 0x040011DE RID: 4574
			[CompilerGenerated]
			private readonly string {22160};

			// Token: 0x040011DF RID: 4575
			[CompilerGenerated]
			private readonly Color {22161};

			// Token: 0x040011E0 RID: 4576
			[CompilerGenerated]
			private readonly bool {22162};

			// Token: 0x040011E1 RID: 4577
			[CompilerGenerated]
			private readonly string {22163};

			// Token: 0x040011E2 RID: 4578
			[CompilerGenerated]
			private readonly string {22164};

			// Token: 0x040011E3 RID: 4579
			[CompilerGenerated]
			private readonly string {22165};

			// Token: 0x02000388 RID: 904
			public enum Texture
			{
				// Token: 0x040011E5 RID: 4581
				Lighthouse,
				// Token: 0x040011E6 RID: 4582
				Port,
				// Token: 0x040011E7 RID: 4583
				Village
			}
		}

		// Token: 0x02000389 RID: 905
		private readonly struct MapUiData
		{
			// Token: 0x17000199 RID: 409
			// (get) Token: 0x060013C5 RID: 5061 RVA: 0x000A76D7 File Offset: 0x000A58D7
			// (set) Token: 0x060013C6 RID: 5062 RVA: 0x000A76DF File Offset: 0x000A58DF
			public Vector2 LightousesCenter { get; set; }

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x060013C7 RID: 5063 RVA: 0x000A76E8 File Offset: 0x000A58E8
			// (set) Token: 0x060013C8 RID: 5064 RVA: 0x000A76F0 File Offset: 0x000A58F0
			public float VisibleMapSize { get; set; }

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x060013C9 RID: 5065 RVA: 0x000A76F9 File Offset: 0x000A58F9
			// (set) Token: 0x060013CA RID: 5066 RVA: 0x000A7701 File Offset: 0x000A5901
			public Marker ContentArea { get; set; }

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x060013CB RID: 5067 RVA: 0x000A770A File Offset: 0x000A590A
			// (set) Token: 0x060013CC RID: 5068 RVA: 0x000A7712 File Offset: 0x000A5912
			public Rectangle MapTextureArea { get; set; }

			// Token: 0x040011E8 RID: 4584
			[CompilerGenerated]
			private readonly Vector2 {22170};

			// Token: 0x040011E9 RID: 4585
			[CompilerGenerated]
			private readonly float {22171};

			// Token: 0x040011EA RID: 4586
			[CompilerGenerated]
			private readonly Marker {22172};

			// Token: 0x040011EB RID: 4587
			[CompilerGenerated]
			private readonly Rectangle {22173};
		}
	}
}
