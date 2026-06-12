using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020003D1 RID: 977
	internal sealed class {22623} : CustomUi
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x000B36F4 File Offset: 0x000B18F4
		private static Marker bigIconSize
		{
			get
			{
				return new Marker(0f, 0f, 26f, 26f);
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x000B3710 File Offset: 0x000B1910
		public static void Update(Tlist<AllyStateTransfer> {22624})
		{
			if ({22624}.Any((AllyStateTransfer {22655}) => {22655}.IsOneGroup))
			{
				if ({22623}.CurrentInstance == null)
				{
					{22623}.CurrentInstance = new {22623}();
				}
				{22623}.CurrentInstance.UpdateGroupViewData({22624});
				return;
			}
			{22623} currentInstance = {22623}.CurrentInstance;
			if (currentInstance == null)
			{
				return;
			}
			currentInstance.RemoveFromContainer();
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x000B3770 File Offset: 0x000B1970
		public {22623}() : base(false)
		{
			{20391}.WhenInit(this, "groupUi");
			this.AnimatedFocus = false;
			this.{22628} = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{22629} = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(new UiControl[]
			{
				this.{22628},
				this.{22629}
			});
			base.EvRemoveFromContainer += delegate()
			{
				{22623}.CurrentInstance = null;
			};
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x000B3800 File Offset: 0x000B1A00
		private void UpdateGroupViewData(Tlist<AllyStateTransfer> {22625})
		{
			AllyStateTransfer[] dataToDisplay = (from {22656} in {22625}
			where {22656}.IsOneGroup && ({22656}.ShipClass != byte.MaxValue || {22656}.uID != -1 || Global.Player.IsPortEntry)
			select {22656}).ToArray<AllyStateTransfer>();
			if (dataToDisplay.Length == 0)
			{
				{22623}.isMinimized = false;
			}
			this.{22628}.Clear();
			this.{22629}.Clear();
			if (Session.Group != null)
			{
				{22623}.GroupViewItem groupViewItem = new {22623}.GroupViewItem(Vector2.Zero, new GroupMemberInfo(Global.Player.uID, Session.Account.SID, Session.Account.PlayerName), null);
				this.{22628}.AddItem(new UiControl[]
				{
					groupViewItem
				});
			}
			if (!{22623}.isMinimized)
			{
				foreach (AllyStateTransfer allyStateTransfer in dataToDisplay)
				{
					GroupMemberInfo {22636} = new GroupMemberInfo(allyStateTransfer.uID, (Session.Group != null) ? Session.Group.TryFindAccountSID(allyStateTransfer.uID) : 0U, allyStateTransfer.FetchName(Session.Group));
					{22623}.GroupViewItem groupViewItem2 = new {22623}.GroupViewItem(Vector2.Zero, {22636}, new AllyStateTransfer?(allyStateTransfer));
					groupViewItem2.SetHp(allyStateTransfer);
					((this.{22628}.CountChild() > 9) ? this.{22629} : this.{22628}).AddItem(new UiControl[]
					{
						groupViewItem2
					});
				}
			}
			if (dataToDisplay.Length >= 2 || {22623}.isMinimized)
			{
				Button button = new Button(Vector2.Zero, {22623}.isMinimized ? new Rectangle(2704, 749, 212, 26) : new Rectangle(2704, 776, 212, 26), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				string {13090};
				if (!{22623}.isMinimized)
				{
					{13090} = "";
				}
				else
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
					defaultInterpolatedStringHandler.AppendLiteral("     ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(dataToDisplay.Length);
					defaultInterpolatedStringHandler.AppendFormatted("☠");
					{13090} = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				Action <>9__3;
				Button button2 = button.SetText({13090}, Fonts.Arial_10, Color.White * 0.7f, true).ExClick(delegate(ClickUiEventArgs {22658})
				{
					{22623}.isMinimized = !{22623}.isMinimized;
					UiControl <>4__this = this;
					Action {14195};
					if (({14195} = <>9__3) == null)
					{
						{14195} = (<>9__3 = delegate()
						{
							this.UpdateGroupViewData(new Tlist<AllyStateTransfer>(dataToDisplay));
						});
					}
					new UiActor(<>4__this, {14195});
				});
				this.{22628}.AddItem(new UiControl[]
				{
					button2
				});
				if ({22623}.isMinimized)
				{
					if (dataToDisplay.Any((AllyStateTransfer {22657}) => {22657}.ShipClass == byte.MaxValue && ({22657}.CapturedShipInfo.FlagsForgotten || {22657}.CapturedShipInfo.FlagsAlmostForgotten || {22657}.CapturedShipInfo.FlagsOverload)))
					{
						button2.AddChildPos(new Form(Vector2.Zero, new Rectangle(2407, 723, 27, 26), PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, 150f);
					}
				}
			}
			Vector2 value = new Vector2(5f, 115f);
			this.{22628}.Pos = this.{22628}.Pos.SetXY(value);
			UiControl uiControl = this.{22629};
			Marker pos = this.{22629}.Pos;
			Vector2 vector = value + new Vector2(this.{22628}.Pos.WH.X - 35f, 0f);
			uiControl.Pos = pos.SetXY(vector);
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x000B3B37 File Offset: 0x000B1D37
		protected override void UserUpdate(ref FrameTime {22626})
		{
			base.IsVisible = ({22913}.CurrentInstance == null && Global.Render.UiMode == InterfaceMode.Default);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x000B3B56 File Offset: 0x000B1D56
		protected override void UserBackRender()
		{
			this.{22627} = (Engine.GS.CurrentTexture != CommonAtlas.Texture.Tex);
			if (this.{22627})
			{
				Engine.GS.SetTexture(CommonAtlas.Texture.Tex);
			}
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x000B3B93 File Offset: 0x000B1D93
		protected override void UserFrontRender()
		{
			if (this.{22627})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x0400133D RID: 4925
		private static readonly Rectangle c_item = new Rectangle(2491, 749, 212, 26);

		// Token: 0x0400133E RID: 4926
		private static readonly Rectangle c_item_animated = new Rectangle(2491, 776, 212, 26);

		// Token: 0x0400133F RID: 4927
		private static readonly Rectangle c_itemHp = new Rectangle(2330, 528, 118, 4);

		// Token: 0x04001340 RID: 4928
		private static readonly Rectangle c_agressiveMode = new Rectangle(2567, 731, 19, 17);

		// Token: 0x04001341 RID: 4929
		private static readonly Rectangle c_neutralMode = new Rectangle(2587, 731, 19, 17);

		// Token: 0x04001342 RID: 4930
		private static readonly Rectangle c_passiveMode = new Rectangle(2607, 731, 19, 17);

		// Token: 0x04001343 RID: 4931
		private static readonly Rectangle c_waitMode = new Rectangle(2627, 731, 19, 17);

		// Token: 0x04001344 RID: 4932
		private static readonly Rectangle c_avanpostIcon = new Rectangle(2547, 731, 19, 17);

		// Token: 0x04001345 RID: 4933
		private static readonly Rectangle c_overloadIcon = new Rectangle(2366, 591, 42, 43);

		// Token: 0x04001346 RID: 4934
		private static readonly Rectangle c_leave = new Rectangle(2452, 704, 54, 44);

		// Token: 0x04001347 RID: 4935
		private static readonly Rectangle c_forgottenAlert = new Rectangle(2409, 591, 42, 43);

		// Token: 0x04001348 RID: 4936
		private static readonly Rectangle c_almostForgotten = new Rectangle(2409, 635, 42, 43);

		// Token: 0x04001349 RID: 4937
		private static readonly Rectangle c_anchorIcon = new Rectangle(2409, 679, 42, 43);

		// Token: 0x0400134A RID: 4938
		public static {22623} CurrentInstance;

		// Token: 0x0400134B RID: 4939
		public static bool isMinimized = false;

		// Token: 0x0400134C RID: 4940
		private bool {22627};

		// Token: 0x0400134D RID: 4941
		private StackForm {22628};

		// Token: 0x0400134E RID: 4942
		private StackForm {22629};

		// Token: 0x020003D2 RID: 978
		private class GroupViewItem : CustomUi
		{
			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06001559 RID: 5465 RVA: 0x000B3CF8 File Offset: 0x000B1EF8
			// (set) Token: 0x0600155A RID: 5466 RVA: 0x000B3D00 File Offset: 0x000B1F00
			public int MemberUID { get; set; }

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x0600155B RID: 5467 RVA: 0x000B3D09 File Offset: 0x000B1F09
			// (set) Token: 0x0600155C RID: 5468 RVA: 0x000B3D11 File Offset: 0x000B1F11
			public uint AccountSID { get; set; }

			// Token: 0x0600155D RID: 5469 RVA: 0x000B3D1C File Offset: 0x000B1F1C
			public GroupViewItem(Vector2 {22635}, GroupMemberInfo {22636}, AllyStateTransfer? {22637})
			{
				{22623}.GroupViewItem.<>c__DisplayClass13_0 CS$<>8__locals1 = new {22623}.GroupViewItem.<>c__DisplayClass13_0();
				CS$<>8__locals1.data = {22637};
				CS$<>8__locals1.memberInfo = {22636};
				base..ctor(new Marker(ref {22635}, ref {22623}.c_item).SetWidth((float)({22623}.c_item.Width + 40)), Rectangle.Empty, PositionAlignment.RightDown, PositionAlignment.LeftUp, Color.White, false);
				CS$<>8__locals1.<>4__this = this;
				this.{22644} = new Form(base.Pos.SetWidth((float){22623}.c_item.Width), {22623}.c_item, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				base.AddChild(this.{22644});
				this.AnimatedFocus = false;
				Vector2 vector = {22635} + new Vector2(9f, 2f);
				this.{22641} = new Label(vector, Fonts.Arial_10, new Color(170, 181, 179) * 1.2f, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				if (CS$<>8__locals1.memberInfo != null)
				{
					this.SetContent(CS$<>8__locals1.memberInfo);
				}
				if (CS$<>8__locals1.data != null && Global.Player.MapInfo.IsWorldmap)
				{
					Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
					AllyStateTransfer value;
					ToolTipCharacteristics toolTipCharacteristics;
					if (CS$<>8__locals1.data.Value.ShipClass == 255 && CS$<>8__locals1.data.Value.uID != -1)
					{
						base.Pos = base.Pos.SetHeight(52f);
						this.{22644}.Pos = this.{22644}.Pos.SetHeight(base.Pos.WH.Y);
						this.{22644}.TexturePath = new Rectangle(2618, 667, 212, 52);
						StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(8f, 28f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							RenderToDepthMap = false
						};
						CannonBallInfo cannonBallInfo = Gameplay.BallsInfo.FromID((int)CS$<>8__locals1.data.Value.CapturedShipInfo.PickedAmmoId);
						stackForm.AddItem(new UiControl[]
						{
							new Image(new Marker(0f, 0f, 18f, 18f), cannonBallInfo.IconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
						stackForm.AddSpace(2f);
						StackForm stackForm2 = stackForm;
						UiControl[] array = new UiControl[1];
						int num = 0;
						Vector2 zero = Vector2.Zero;
						CustomSpriteFont arial_ = Fonts.Arial_12;
						Color {13344} = (CS$<>8__locals1.data.Value.CapturedShipInfo.AmmoInHold == 0) ? Color.OrangeRed : ((CS$<>8__locals1.data.Value.CapturedShipInfo.AmmoInHold < 300) ? Color.Orange : Color.Wheat);
						value = CS$<>8__locals1.data.Value;
						array[num] = new Label(zero, arial_, {13344}, value.CapturedShipInfo.AmmoInHold.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						stackForm2.AddItem(array);
						NpcInfo npcInfo = Gameplay.NpcsInfo.FromID(CS$<>8__locals1.data.Value.CapturedShipInfoId);
						if (npcInfo.MortarShipModification)
						{
							stackForm.AddSpace(5f);
							stackForm.AddItem(new UiControl[]
							{
								new Form(Vector2.Zero, {17242}.c_equipMortarBall, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
								{
									AnimatedFocus = false
								}
							});
							stackForm.AddSpace(2f);
							StackForm stackForm3 = stackForm;
							UiControl[] array2 = new UiControl[1];
							int num2 = 0;
							Vector2 zero2 = Vector2.Zero;
							CustomSpriteFont arial_2 = Fonts.Arial_12;
							Color {13344}2 = (CS$<>8__locals1.data.Value.CapturedShipInfo.MortarShotsAmmoInHold == 0) ? Color.OrangeRed : ((CS$<>8__locals1.data.Value.CapturedShipInfo.MortarShotsAmmoInHold < 7) ? Color.Orange : Color.Wheat);
							value = CS$<>8__locals1.data.Value;
							array2[num2] = new Label(zero2, arial_2, {13344}2, value.CapturedShipInfo.MortarShotsAmmoInHold.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
							stackForm3.AddItem(array2);
						}
						value = CS$<>8__locals1.data.Value;
						if (value.CapturedShipInfo.FlagsAlmostForgotten)
						{
							value = CS$<>8__locals1.data.Value;
							if (!value.CapturedShipInfo.FlagsForgotten)
							{
								base.AddChild(new Label(base.Pos.XY + new Vector2(133f, 28f), Fonts.Arial_10Bold, Color.Orange * 0.7f, Local.captured_ship_too_far, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
							}
						}
						base.AddChild(stackForm);
						Tlist<ToolTipCharacteristics> tlist2 = tlist;
						toolTipCharacteristics = new ToolTipCharacteristics(Local.speed, "~" + Math.Round((double)npcInfo.ShipProperties.MaxBasicSpeed, 1).ToString());
						tlist2.Add(toolTipCharacteristics);
						Tlist<ToolTipCharacteristics> tlist3 = tlist;
						toolTipCharacteristics = new ToolTipCharacteristics(Local.StringConstants_86, "~" + Math.Round((double)(npcInfo.DamagePerShot * 60000f / npcInfo.ReloadCannonTime)).ToString() + " /" + Local.StringConstants_66_B);
						tlist3.Add(toolTipCharacteristics);
						Tlist<ToolTipCharacteristics> tlist4 = tlist;
						toolTipCharacteristics = new ToolTipCharacteristics("[" + Global.Settings.kb_SendGroupCommand.KeyToString + "] " + Local.GameSettingsWindow_98, CharacteristicsColor.WheatBold);
						tlist4.Add(toolTipCharacteristics);
					}
					Tlist<ToolTipCharacteristics> tlist5 = tlist;
					toolTipCharacteristics = new ToolTipCharacteristics(Local.ChatBoxGui_20, CharacteristicsColor.LimeBold);
					tlist5.Add(toolTipCharacteristics);
					this.{22641}.ToolTipState = new ToolTipState("", "", tlist.ToArray());
					value = CS$<>8__locals1.data.Value;
					this.{22645} = value.IsWaitingTrainingArena;
				}
				base.AddChild(this.{22641});
				Action<object> action = delegate(object {22653})
				{
					if (CS$<>8__locals1.<>4__this.{22643})
					{
						return;
					}
					if (CS$<>8__locals1.data != null && CS$<>8__locals1.data.Value.ShipClass == 255)
					{
						int num3 = Session.LastMinimapAndGroupUpdate.allies.Count((AllyStateTransfer {22648}) => {22648}.ShipClass == byte.MaxValue && {22648}.uID == -1);
						Tlist<{17473}.Item> tlist6 = new Tlist<{17473}.Item>();
						if (CS$<>8__locals1.data.Value.uID == -1)
						{
							{22623}.GroupViewItem.<>c__DisplayClass13_1 CS$<>8__locals2 = new {22623}.GroupViewItem.<>c__DisplayClass13_1();
							CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
							{22623}.GroupViewItem.<>c__DisplayClass13_1 CS$<>8__locals3 = CS$<>8__locals2;
							PlayerPersonalIsles personalIsles = Session.Account.PersonalIsles;
							AllyStateTransfer value2 = CS$<>8__locals1.data.Value;
							Vector2 position = value2.Position.Position;
							CS$<>8__locals3.isle = personalIsles.GetNearTo(position);
							CS$<>8__locals2.isOtherIsle = (CS$<>8__locals2.isle != Global.Game.ScenePort.CurrentPersonalIsle);
							Tlist<{17473}.Item> tlist7 = tlist6;
							{17473}.Item item = new {17473}.Item(null, Local.captured_ship_from_isle, Global.Player.IsPortEntry && Global.Player.NearPortType == PortEnteringType.PersonalIsle, default(ImageDecription), null, delegate()
							{
								if (CS$<>8__locals2.isOtherIsle)
								{
									new {17312}(Local.captured_ship_from_isle_error_place(CS$<>8__locals2.isle.Name));
									return;
								}
								if (Session.LastMinimapAndGroupUpdate.allies.Count((AllyStateTransfer {22649}) => {22649}.ShipClass == byte.MaxValue && {22649}.uID != -1) >= Session.Account.CapturedNpcLimit)
								{
									new {17312}(Local.captured_ship_from_isle_error(Session.Account.CapturedNpcLimit));
									return;
								}
								Global.Network.Send(new OnNpcAsCaperActionMsg(CS$<>8__locals2.CS$<>8__locals1.data.Value.CapturedShipInfoId, OnNpcAsCaperActionMsg.Type.GetFromIsle, null));
							});
							tlist7.Add(item);
						}
						else
						{
							Tlist<{17473}.Item> tlist8 = tlist6;
							object {17486} = null;
							string hold = Local.hold;
							bool {17488};
							if (!Global.Player.UsedShip.StaticInfo.IsBalloon)
							{
								AllyStateTransfer value2 = CS$<>8__locals1.data.Value;
								{17488} = !value2.CapturedShipInfo.FlagsForgotten;
							}
							else
							{
								{17488} = false;
							}
							ImageDecription {17489} = default(ImageDecription);
							ToolTipState {17490} = null;
							Action {17491};
							if (({17491} = CS$<>8__locals1.<>9__2) == null)
							{
								{17491} = (CS$<>8__locals1.<>9__2 = delegate()
								{
									Global.Network.Send(new OnNpcAsCaperActionMsg(CS$<>8__locals1.data.Value.uID, OnNpcAsCaperActionMsg.Type.RequestStatusAndStop, null));
								});
							}
							{17473}.Item item = new {17473}.Item({17486}, hold, {17488}, {17489}, {17490}, {17491});
							tlist8.Add(item);
							Tlist<{17473}.Item> tlist9 = tlist6;
							object {17486}2 = null;
							string {17487};
							if (Global.Player.IsPortEntry)
							{
								AllyStateTransfer value2 = CS$<>8__locals1.data.Value;
								if (!value2.CapturedShipInfo.FlagsForgotten)
								{
									{17487} = Local.CommonItemCraftUi_5;
									goto IL_1D7;
								}
							}
							{17487} = Local.HoldsUiCommon_1b;
							IL_1D7:
							bool {17488}2 = true;
							ImageDecription {17489}2 = default(ImageDecription);
							ToolTipState {17490}2 = null;
							Action {17491}2;
							if (({17491}2 = CS$<>8__locals1.<>9__3) == null)
							{
								{17491}2 = (CS$<>8__locals1.<>9__3 = delegate()
								{
									if (!Global.Player.IsPortEntry || CS$<>8__locals1.data.Value.CapturedShipInfo.FlagsForgotten)
									{
										string {17371} = Local.captured_ship_unbind + "?";
										Action {17372};
										if (({17372} = CS$<>8__locals1.<>9__12) == null)
										{
											{17372} = (CS$<>8__locals1.<>9__12 = delegate()
											{
												Global.Network.Send(new OnNpcAsCaperActionMsg(CS$<>8__locals1.data.Value.uID, OnNpcAsCaperActionMsg.Type.Unbind, null));
											});
										}
										new {17312}({17371}, {17372}, delegate()
										{
										});
										return;
									}
									if (CS$<>8__locals1.data.Value.CapturedShipInfo.HoldLoadWeight > 0f)
									{
										string destruy_captured_ship_ask = Local.destruy_captured_ship_ask;
										Action {17372}2;
										if (({17372}2 = CS$<>8__locals1.<>9__10) == null)
										{
											{17372}2 = (CS$<>8__locals1.<>9__10 = delegate()
											{
												Global.Network.Send(new OnNpcAsCaperActionMsg(CS$<>8__locals1.data.Value.uID, OnNpcAsCaperActionMsg.Type.RequestStatusAndStop, null));
											});
										}
										Action {17373};
										if (({17373} = CS$<>8__locals1.<>9__11) == null)
										{
											{17373} = (CS$<>8__locals1.<>9__11 = delegate()
											{
												{18214}.OpenDestroyNpsDialog(CS$<>8__locals1.data.Value);
											});
										}
										new {17312}(destruy_captured_ship_ask, {17372}2, {17373});
										return;
									}
									{18214}.OpenDestroyNpsDialog(CS$<>8__locals1.data.Value);
								});
							}
							item = new {17473}.Item({17486}2, {17487}, {17488}2, {17489}2, {17490}2, {17491}2);
							tlist9.Add(item);
							Tlist<{17473}.Item> tlist10 = tlist6;
							object {17486}3 = null;
							string portMendingShipWindow_ = Local.PortMendingShipWindow_2;
							bool {17488}3;
							if (Global.Player.IsPortEntry)
							{
								AllyStateTransfer value2 = CS$<>8__locals1.data.Value;
								if (!value2.CapturedShipInfo.FlagsForgotten && CS$<>8__locals1.data.Value.Health != 255)
								{
									{17488}3 = (CS$<>8__locals1.data.Value.Health > 0);
									goto IL_26C;
								}
							}
							{17488}3 = false;
							IL_26C:
							ImageDecription {17489}3 = default(ImageDecription);
							ToolTipState {17490}3 = null;
							Action {17491}3;
							if (({17491}3 = CS$<>8__locals1.<>9__4) == null)
							{
								{17491}3 = (CS$<>8__locals1.<>9__4 = delegate()
								{
									new {17177}(false);
									{17177} currentInstance = {17177}.CurrentInstance;
									string {17190} = "";
									bool {17191} = false;
									string {17192} = Local.restore_captured_ship_1(CS$<>8__locals1.data.Value.CapturedShipInfo.RepairsCount);
									Action<TextBlockBuilder> {17193} = delegate(TextBlockBuilder {22650})
									{
									};
									CraftingRecipe {17194} = new CraftingRecipe(Gameplay.MendingCapturedShipPrice(Gameplay.NpcsInfo.FromID(CS$<>8__locals1.data.Value.CapturedShipInfoId), (int)CS$<>8__locals1.data.Value.CapturedShipInfo.RepairsCount));
									RTIf {17195} = 0f;
									int {17196} = 1;
									bool {17197} = true;
									Action<ValueTuple<int, int>> {17198};
									if (({17198} = CS$<>8__locals1.<>9__15) == null)
									{
										{17198} = (CS$<>8__locals1.<>9__15 = delegate([TupleElementNames(new string[]
										{
											"resCount",
											"btIndex"
										})] ValueTuple<int, int> {22654})
										{
											Global.Network.Send(new OnNpcAsCaperActionMsg(CS$<>8__locals1.data.Value.uID, OnNpcAsCaperActionMsg.Type.RestoreShip, null));
										});
									}
									currentInstance.SetData({17190}, {17191}, {17192}, {17193}, {17194}, {17195}, {17196}, {17197}, {17198}, false, null, null, 1, true, int.MaxValue, false, -1f);
								});
							}
							item = new {17473}.Item({17486}3, portMendingShipWindow_, {17488}3, {17489}3, {17490}3, {17491}3);
							tlist10.Add(item);
							Tlist<{17473}.Item> tlist11 = tlist6;
							object {17486}4 = null;
							string add_crew = Local.add_crew;
							bool {17488}4;
							if (Global.Player.IsPortEntry && CS$<>8__locals1.data.Value.CapturedShipInfo.NotHavingCrewCount != 0)
							{
								AllyStateTransfer value2 = CS$<>8__locals1.data.Value;
								{17488}4 = !value2.CapturedShipInfo.FlagsForgotten;
							}
							else
							{
								{17488}4 = false;
							}
							ImageDecription {17489}4 = default(ImageDecription);
							ToolTipState {17490}4 = null;
							Action {17491}4;
							if (({17491}4 = CS$<>8__locals1.<>9__5) == null)
							{
								{17491}4 = (CS$<>8__locals1.<>9__5 = delegate()
								{
									RTI rti = Gameplay.UnitsInfo.First().InternalPrice * (int)CS$<>8__locals1.data.Value.CapturedShipInfo.NotHavingCrewCount;
									string {17352} = Local.captured_ship_crew_add(CS$<>8__locals1.data.Value.CapturedShipInfo.NotHavingCrewCount);
									RTI {17353} = rti;
									Action {17354};
									if (({17354} = CS$<>8__locals1.<>9__16) == null)
									{
										{17354} = (CS$<>8__locals1.<>9__16 = delegate()
										{
											Global.Network.Send(new OnNpcAsCaperActionMsg(CS$<>8__locals1.data.Value.uID, OnNpcAsCaperActionMsg.Type.RefillUnits, null));
										});
									}
									{17312}.AskPrice({17352}, {17353}, {17354}, true);
								});
							}
							item = new {17473}.Item({17486}4, add_crew, {17488}4, {17489}4, {17490}4, {17491}4);
							tlist11.Add(item);
							Tlist<{17473}.Item> tlist12 = tlist6;
							object {17486}5 = null;
							string {17487}2 = Local.captured_ship_to_isle(num3, 3);
							bool {17488}5;
							if (Global.Player.IsPortEntry && Global.Player.NearPortType == PortEnteringType.PersonalIsle)
							{
								AllyStateTransfer value2 = CS$<>8__locals1.data.Value;
								if (!value2.CapturedShipInfo.FlagsForgotten)
								{
									{17488}5 = (num3 < 3);
									goto IL_375;
								}
							}
							{17488}5 = false;
							IL_375:
							ImageDecription {17489}5 = default(ImageDecription);
							ToolTipState {17490}5 = new ToolTipState("", Local.captured_ship_to_isle_tt, Array.Empty<ToolTipCharacteristics>());
							Action {17491}5;
							if (({17491}5 = CS$<>8__locals1.<>9__6) == null)
							{
								{17491}5 = (CS$<>8__locals1.<>9__6 = delegate()
								{
									Global.Network.Send(new OnNpcAsCaperActionMsg(CS$<>8__locals1.data.Value.uID, OnNpcAsCaperActionMsg.Type.StoreToIsle, null));
								});
							}
							item = new {17473}.Item({17486}5, {17487}2, {17488}5, {17489}5, {17490}5, {17491}5);
							tlist12.Add(item);
							CapturedShipIntance.NpcMode[] values = Enum.GetValues<CapturedShipIntance.NpcMode>();
							for (int i = 0; i < values.Length; i++)
							{
								CapturedShipIntance.NpcMode mode = values[i];
								ValueTuple<string, string> valueTuple;
								if (mode != CapturedShipIntance.NpcMode.Agressive)
								{
									if (mode != CapturedShipIntance.NpcMode.Neutral)
									{
										if (mode != CapturedShipIntance.NpcMode.Passive)
										{
											if (mode != CapturedShipIntance.NpcMode.Wait)
											{
												throw new NotSupportedException();
											}
											valueTuple = new ValueTuple<string, string>(Local.captured_ship_mode_wait, Local.captured_ship_mode_wait_info);
										}
										else
										{
											valueTuple = new ValueTuple<string, string>(Local.captured_ship_mode_passive, Local.captured_ship_mode_passive_info);
										}
									}
									else
									{
										valueTuple = new ValueTuple<string, string>(Local.captured_ship_mode_neutral, Local.captured_ship_mode_neutral_info);
									}
								}
								else
								{
									valueTuple = new ValueTuple<string, string>(Local.captured_ship_mode_agressive, Local.captured_ship_mode_agressive_info);
								}
								ValueTuple<string, string> valueTuple2 = valueTuple;
								Tlist<{17473}.Item> tlist13 = tlist6;
								item = new {17473}.Item(null, ((CS$<>8__locals1.data.Value.CapturedShipInfo.BehaviorMode == mode) ? "✔ " : "") + valueTuple2.Item1, CS$<>8__locals1.data.Value.CapturedShipInfo.BehaviorMode != mode, default(ImageDecription), new ToolTipState(valueTuple2.Item1, valueTuple2.Item2, Array.Empty<ToolTipCharacteristics>()), delegate()
								{
									OnNpcAsCaperActionMsg.Type type;
									if (mode != CapturedShipIntance.NpcMode.Agressive)
									{
										if (mode != CapturedShipIntance.NpcMode.Neutral)
										{
											if (mode != CapturedShipIntance.NpcMode.Passive)
											{
												if (mode != CapturedShipIntance.NpcMode.Wait)
												{
													throw new NotSupportedException();
												}
												type = OnNpcAsCaperActionMsg.Type.WaitMode;
											}
											else
											{
												type = OnNpcAsCaperActionMsg.Type.PassiveMode;
											}
										}
										else
										{
											type = OnNpcAsCaperActionMsg.Type.NeutralMode;
										}
									}
									else
									{
										type = OnNpcAsCaperActionMsg.Type.AgressiveMode;
									}
									OnNpcAsCaperActionMsg.Type {10088} = type;
									Global.Network.Send(new OnNpcAsCaperActionMsg(CS$<>8__locals1.data.Value.uID, {10088}, null));
								});
								tlist13.Add(item);
							}
						}
						new {17473}(delegate(object {22651})
						{
						}, tlist6.ToArray());
						return;
					}
					if (CS$<>8__locals1.<>4__this.MemberUID != -1)
					{
						new {17558}(new {17549}(CS$<>8__locals1.<>4__this.AccountSID, CS$<>8__locals1.memberInfo.Name, Array.Empty<{17549}.OptionalAction>()));
					}
				};
				base.EvClick += action;
				base.EvRightButtonClick += action;
				if (this.{22643})
				{
					this.{22641}.BasicColor = new Color(170, 181, 136);
				}
				if (CS$<>8__locals1.data != null && CS$<>8__locals1.data.Value.ShipClass == 255)
				{
					Rectangle {13190} = (CS$<>8__locals1.data.Value.CapturedShipInfo.BehaviorMode == CapturedShipIntance.NpcMode.Agressive) ? {22623}.c_agressiveMode : ((CS$<>8__locals1.data.Value.CapturedShipInfo.BehaviorMode == CapturedShipIntance.NpcMode.Neutral) ? {22623}.c_neutralMode : ((CS$<>8__locals1.data.Value.CapturedShipInfo.BehaviorMode == CapturedShipIntance.NpcMode.Passive) ? {22623}.c_passiveMode : ((CS$<>8__locals1.data.Value.CapturedShipInfo.BehaviorMode == CapturedShipIntance.NpcMode.Wait) ? {22623}.c_waitMode : Rectangle.Empty)));
					base.AddChild(new Form(base.Pos.XY + new Vector2(152f, 28f), {13190}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					});
					Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID(CS$<>8__locals1.data.Value.CapturedShipInfo.TargetUID);
					if (shipFromUID != null)
					{
						ShipOtherPlayer shipOtherPlayer = shipFromUID as ShipOtherPlayer;
						string str2;
						if (shipOtherPlayer != null)
						{
							string str;
							if (string.IsNullOrEmpty(shipOtherPlayer.Client.Guild.Tag))
							{
								str = "";
							}
							else
							{
								str = (shipOtherPlayer.HideNickname ? "[???] " : ("[" + shipOtherPlayer.Client.Guild.Tag + "] "));
							}
							str2 = str + shipOtherPlayer.Client.GetName2();
						}
						else
						{
							str2 = "(NPS) " + ((NpcShipDynamicInfo)shipFromUID.UsedShip).Information.NpcName;
						}
						base.AddChild(new Label(vector + new Vector2(60f, 27f), Fonts.Arial_10, Color.Wheat, "⚔ " + str2, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					}
				}
				StackForm stackForm4 = new StackForm(base.Pos.XY + new Vector2(150f, 3f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					PositionAlignment_X = base.PositionAlignment_X,
					PositionAlignment_Y = base.PositionAlignment_Y
				};
				base.AddChild(stackForm4);
				if (CS$<>8__locals1.data != null && CS$<>8__locals1.data.Value.ShipClass == 255)
				{
					AllyStateTransfer value = CS$<>8__locals1.data.Value;
					if (value.CapturedShipInfo.FlagsForgotten)
					{
						stackForm4.AddItem(new UiControl[]
						{
							new Form({22623}.bigIconSize, {22623}.c_forgottenAlert, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							{
								AnimatedFocus = false
							}.ExToolTip(new ToolTip(new ToolTipState("", Local.captured_ship_forgotten, Array.Empty<ToolTipCharacteristics>())))
						});
					}
					else
					{
						value = CS$<>8__locals1.data.Value;
						if (value.CapturedShipInfo.FlagsAlmostForgotten)
						{
							stackForm4.AddItem(new UiControl[]
							{
								new Form({22623}.bigIconSize, {22623}.c_almostForgotten, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
								{
									AnimatedFocus = false
								}.ExToolTip(new ToolTip(new ToolTipState("", Local.captured_ship_forgotten_almost, Array.Empty<ToolTipCharacteristics>())))
							});
						}
						else
						{
							value = CS$<>8__locals1.data.Value;
							if (value.CapturedShipInfo.FlagsOverload)
							{
								stackForm4.AddItem(new UiControl[]
								{
									new Form({22623}.bigIconSize, {22623}.c_overloadIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
									{
										AnimatedFocus = false
									}.ExToolTip(new ToolTip(new ToolTipState("", Local.captured_ship_hold_overload(this.{22641}.Text), Array.Empty<ToolTipCharacteristics>())))
								});
							}
						}
					}
				}
				if (CS$<>8__locals1.memberInfo.UID == Global.Player.uID && !Global.Player.MapInfo.IsEnableArenaUi)
				{
					Button button = new Button(new Marker(0f, 0f, ref {22623}.c_leave).ScaleSize(0.66f), {22623}.c_leave, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					stackForm4.AddItem(new UiControl[]
					{
						button
					});
					button.EvClick += delegate(ClickUiEventArgs {22652})
					{
						Global.Network.Send(new OnGMRemoveMsg(Global.Player.uID));
					};
				}
				if (CS$<>8__locals1.data != null)
				{
					AllyStateTransfer value = CS$<>8__locals1.data.Value;
					if (value.IsActiveAvanpostShip)
					{
						stackForm4.AddItem(new UiControl[]
						{
							new Form(Vector2.Zero, {22623}.c_avanpostIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							{
								AnimatedFocus = false
							}
						});
					}
				}
				if (CS$<>8__locals1.data != null)
				{
					AllyStateTransfer value = CS$<>8__locals1.data.Value;
					if (value.IsInsidePort && CS$<>8__locals1.data.Value.ShipClass != 255)
					{
						stackForm4.AddItem(new UiControl[]
						{
							new Form({22623}.bigIconSize, {22623}.c_anchorIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							{
								AnimatedFocus = false
							}
						});
					}
				}
			}

			// Token: 0x0600155E RID: 5470 RVA: 0x000B4790 File Offset: 0x000B2990
			protected override void UserUpdate(ref FrameTime {22638})
			{
				if (this.{22643} && Global.Player.UsedShip != null)
				{
					this.{22642} = (Global.Player.IsDestroyed ? 0f : (Global.Player.UsedShip.FirstHP.Summary / Global.Player.UsedShip.MaxHp));
				}
				if ({18807}.CurrentInstance != null && (Session.CachedUiArenaMode ?? ArenaMode.DuelRating) == ArenaMode.DuelTraning)
				{
					base.Opacity = (this.{22645} ? 1f : 0.4f);
				}
			}

			// Token: 0x0600155F RID: 5471 RVA: 0x000B4828 File Offset: 0x000B2A28
			protected override void UserBackRender()
			{
				if (base.InputMode == MouseInputMode.Focused)
				{
					Device gs = Engine.GS;
					Rectangle rectangle = this.{22644}.Pos.ToRect();
					gs.Draw({22623}.c_item, rectangle);
				}
			}

			// Token: 0x06001560 RID: 5472 RVA: 0x000B4864 File Offset: 0x000B2A64
			protected override void UserFrontRender()
			{
				float opcaity = base.GetOpcaity();
				Rectangle c_itemHp = {22623}.c_itemHp;
				c_itemHp.Width = (int)((float){22623}.c_itemHp.Width * this.{22642});
				Rectangle {11437};
				{11437}.X = (int)(base.Pos.XY.X + 8f);
				{11437}.Y = (int)(base.Pos.XY.Y + 18f);
				{11437}.Width = c_itemHp.Width;
				{11437}.Height = c_itemHp.Height;
				Device gs = Engine.GS;
				Rectangle rectangle = {11437}.SetWidth((float){22623}.c_itemHp.Width);
				Color color = Color.Black * 0.25f * opcaity;
				gs.Draw({22623}.c_itemHp, rectangle, color);
				Device gs2 = Engine.GS;
				Color value;
				if (this.{22642} >= 0.3f)
				{
					value = Color.White;
				}
				else
				{
					color = Color.Orange;
					value = new Color(color.ToVector4() * new Vector4(1f, 1f, 1f, 0.5f));
				}
				color = value * opcaity;
				gs2.Draw(c_itemHp, {11437}, color);
			}

			// Token: 0x06001561 RID: 5473 RVA: 0x000B4988 File Offset: 0x000B2B88
			public void SetContent(GroupMemberInfo {22639})
			{
				this.{22641}.Text = {22639}.Name;
				this.{22642} = 1f;
				this.MemberUID = {22639}.UID;
				this.AccountSID = {22639}.AccountSID;
				this.{22643} = ({22639}.UID == Global.Player.uID);
			}

			// Token: 0x06001562 RID: 5474 RVA: 0x000B49E4 File Offset: 0x000B2BE4
			public void SetHp(AllyStateTransfer {22640})
			{
				this.{22642} = (float){22640}.Health / 255f;
				base.Opacity = (({22640}.IsOneMap || ({22640}.uID == -1 && Global.Player.IsPortEntry)) ? 1f : 0.5f);
			}

			// Token: 0x0400134F RID: 4943
			private Label {22641};

			// Token: 0x04001350 RID: 4944
			private float {22642};

			// Token: 0x04001351 RID: 4945
			private bool {22643};

			// Token: 0x04001352 RID: 4946
			private Form {22644};

			// Token: 0x04001353 RID: 4947
			private bool {22645};

			// Token: 0x04001354 RID: 4948
			[CompilerGenerated]
			private int {22646};

			// Token: 0x04001355 RID: 4949
			[CompilerGenerated]
			private uint {22647};
		}
	}
}
