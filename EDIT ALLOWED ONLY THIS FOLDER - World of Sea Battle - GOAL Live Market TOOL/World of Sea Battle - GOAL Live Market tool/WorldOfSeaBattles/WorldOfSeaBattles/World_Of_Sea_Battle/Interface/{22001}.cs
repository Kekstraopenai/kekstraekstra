using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200037A RID: 890
	internal sealed class {22001} : CustomUi
	{
		// Token: 0x06001335 RID: 4917 RVA: 0x000A30B8 File Offset: 0x000A12B8
		public {22001}() : base(new Marker(4f, (float)(Engine.GS.UIArea.Height - 467 + 22), 300f, 384f), new Rectangle(510, 0, 128, 128), PositionAlignment.LeftUp, PositionAlignment.RightDown, Color.White * 0.6f, false)
		{
			{22001}.CurrentInstance = this;
			this.AnimatedFocus = false;
			this.{22023} = Session.Account.Shipyard.CurrentRealShip;
			Global.Game.ScenePort.ShipsHolder.SeeTargetChanged += this.{22002};
			base.EvRemoveFromContainer += this.{22009};
			this.{22022} = new Tlist<AnimatedButton>();
			AnimatedButton bt_strength = new AnimatedButton(base.Pos.XY + new Vector2(3f, 42f), {22001}.buttons[0], {22001}.buttons[0], {22001}.buttonsF[0], PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			AnimatedButton animatedButton = new AnimatedButton(base.Pos.XY + new Vector2(3f, 150f), {22001}.buttons[1], {22001}.buttons[1], {22001}.buttonsF[1], PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			AnimatedButton animatedButton2 = new AnimatedButton(base.Pos.XY + new Vector2(3f, 195f), {22001}.buttons[2], {22001}.buttons[2], {22001}.buttonsF[2], PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			AnimatedButton animatedButton3 = new AnimatedButton(base.Pos.XY + new Vector2(3f, 240f), {22001}.buttons[3], {22001}.buttons[3], {22001}.buttonsF[3], PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{22026} = new AnimatedButton(base.Pos.XY + new Vector2(3f, 285f), {22001}.buttons[4], {22001}.buttons[4], {22001}.buttonsF[4], PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			AnimatedButton animatedButton4 = new AnimatedButton(base.Pos.XY + new Vector2(3f, 330f), {22001}.buttons[5], {22001}.buttons[5], {22001}.buttonsF[5], PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			animatedButton4.UpdateComplete += delegate(UiControl {22041})
			{
				{22041}.Opacity = (EducationHelper.MakeInvisibleShipButtons ? 0.3f : 1f);
			};
			animatedButton2.UpdateComplete += delegate(UiControl {22042})
			{
				{22042}.Opacity = ((EducationHelper.MakeInvisibleShipButtons || {18807}.CurrentInstance != null) ? 0.3f : 1f);
			};
			this.{22026}.UpdateComplete += delegate(UiControl {22043})
			{
				{22043}.Opacity = (EducationHelper.MakeInvisibleShipButtons_Upgrade ? 0.3f : 1f);
			};
			bt_strength.UpdateComplete += delegate(UiControl {22055})
			{
				bt_strength.Opacity = ((this.{22023}.HpFactor >= 1f && EducationHelper.MakeInvisibleShipButtons) ? 0.3f : 1f);
				bt_strength.SecoundTexturePath = ((this.{22023}.AllowAnyMending || (this.{22023}.IntegrityIsDestroyed && this.{22023}.ClientTimeToRestoreIntegrity == 0f)) ? {22001}.strengthBtLighted : {22001}.buttons[0]);
			};
			bt_strength.ExClick(delegate(ClickUiEventArgs {22044})
			{
				if (Global.Player.UsedShipPlayer.ClientTimeToRestoreIntegrity == 0f)
				{
					new {21726}().EvRemoveFromContainer += delegate()
					{
						if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
						{
							Global.Game.ScenePort.UpdateGuiForViewShip();
						}
					};
				}
			});
			animatedButton.ExClick(delegate(ClickUiEventArgs {22045})
			{
				if ({17745}.CurrentInstance == null)
				{
					new {17745}();
				}
				EducationHelper.MakeFlag(EducationOnboarding.OpenHoldFromPort, false);
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_OpenHold, true);
			});
			animatedButton.UpdateComplete += delegate(UiControl {22046})
			{
				if (Global.Settings.kb_OpenHold.IsClick && GameScene.GameHasInputFocus && !TextBox.IsThereInput && !KeyInputControl.IsInputElements)
				{
					{22046}.ImitateClick(false);
				}
			};
			animatedButton2.ExClick(new Action<ClickUiEventArgs>(this.{22010}));
			animatedButton3.ExClick(delegate(ClickUiEventArgs {22047})
			{
				new {21596}();
			});
			this.{22026}.ExClick(delegate(ClickUiEventArgs {22048})
			{
				new {22195}();
			});
			animatedButton4.ExClick(delegate(ClickUiEventArgs {22049})
			{
				new {21544}();
			});
			this.{22022}.Add(bt_strength);
			this.{22022}.Add(animatedButton);
			this.{22022}.Add(animatedButton2);
			this.{22022}.Add(animatedButton3);
			this.{22022}.Add(this.{22026});
			this.{22022}.Add(animatedButton4);
			this.{22025} = new PointedProgressBar(Vector2.Zero, new Rectangle(788, 760, 12, 11), new Rectangle(801, 760, 12, 11), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{22025}.IsVisible = false;
			base.AddChild(this.{22025});
			this.{22003}(this.{22023});
			bt_strength.UpdateComplete += this.{22012};
			this.{22026}.UpdateComplete += delegate(UiControl {22050})
			{
				{22050}.IsVisible = (Global.Player != null && !Global.Player.UsedShip.StaticInfo.IsBalloon);
			};
			using (IEnumerator<AnimatedButton> enumerator = ((IEnumerable<AnimatedButton>)this.{22022}).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					{22001}.<>c__DisplayClass12_1 CS$<>8__locals2 = new {22001}.<>c__DisplayClass12_1();
					CS$<>8__locals2.item = enumerator.Current;
					{22001}.<>c__DisplayClass12_2 CS$<>8__locals3 = new {22001}.<>c__DisplayClass12_2();
					CS$<>8__locals3.CS$<>8__locals1 = CS$<>8__locals2;
					CS$<>8__locals3.form = new AnimatedButton(new Marker(CS$<>8__locals3.CS$<>8__locals1.item.Pos.XY.X - 3f, CS$<>8__locals3.CS$<>8__locals1.item.Pos.XY.Y - 1f, 299f, 49f), AtlasPortGui.transpPixel, AtlasPortGui.transpPixel, {21684}.c_itemTop, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					CS$<>8__locals3.form.AddChild(CS$<>8__locals3.CS$<>8__locals1.item);
					CS$<>8__locals3.CS$<>8__locals1.item.UpdateComplete += delegate(UiControl {22057})
					{
						CS$<>8__locals3.CS$<>8__locals1.item.ForceFocusedState = (CS$<>8__locals3.form.InputMode == MouseInputMode.Focused);
					};
					CS$<>8__locals3.form.EvClickEmptiness += delegate(ClickUiEventArgs {22056})
					{
						if (CS$<>8__locals3.CS$<>8__locals1.item.IsVisible)
						{
							CS$<>8__locals3.CS$<>8__locals1.item.ImitateClick(false);
						}
					};
					base.AddChild(CS$<>8__locals3.form);
					if (CS$<>8__locals3.CS$<>8__locals1.item == this.{22026})
					{
						CS$<>8__locals3.<.ctor>g__AttachBlinkingCondition|57(() => Session.Account.IsEducationInProgress(EducationOnboarding.InstallUpgrade, false, false) || Session.Account.IsEducationInProgress(EducationOnboarding.InstallUpgradeForNonStartShip, false, false) || Session.Account.IsEducationInProgress(EducationOnboarding.InstallUpgradeForHold, false, false));
					}
					if (CS$<>8__locals3.CS$<>8__locals1.item == animatedButton)
					{
						CS$<>8__locals3.<.ctor>g__AttachBlinkingCondition|57(() => Session.Account.IsEducationInProgress(EducationOnboarding.GivePowerupItems, false, false) || Session.Account.IsEducationInProgress(EducationOnboarding.BuildStorage, false, false));
					}
					if (CS$<>8__locals3.CS$<>8__locals1.item == animatedButton3)
					{
						CS$<>8__locals3.<.ctor>g__AttachBlinkingCondition|57(() => Session.Account.IsEducationInProgress(EducationOnboarding.GetCrewToNewShip, false, false));
					}
				}
			}
			this.{22027} = new Tlist<{22001}.StatusDisplay>();
			Color {22036} = new Color(183, 193, 204);
			Color wheat = new Color(168, 154, 148);
			Color gray = new Color(112, 123, 131);
			Tlist<{22001}.StatusDisplay> tlist = this.{22027};
			{22001}.StatusDisplay statusDisplay = new {22001}.StatusDisplay(Local.strength, {22036}, new Vector2(52f, this.{22022}.Array[0].Pos.XY.Y + 4f) - base.Pos.XY, new Func<string>(this.{22014}), delegate()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted<int>((int)this.{22023}.FirstHP.Summary);
				defaultInterpolatedStringHandler.AppendLiteral(" / ");
				defaultInterpolatedStringHandler.AppendFormatted<int>((int)this.{22023}.MaxHpWithoutTempEffects);
				return new ValueTuple<string, Color>(defaultInterpolatedStringHandler.ToStringAndClear(), wheat);
			}, delegate()
			{
				if (!EducationHelper.MakeInvisibleShipButtons)
				{
					return 1f;
				}
				return 0.3f;
			});
			tlist.Add(statusDisplay);
			Tlist<{22001}.StatusDisplay> tlist2 = this.{22027};
			statusDisplay = new {22001}.StatusDisplay(Local.PortShipInfoGui_3, gray, new Vector2(52f, this.{22022}.Array[0].Pos.XY.Y + 21f + 4f) - base.Pos.XY, () => null, () => new ValueTuple<string, Color>((this.{22023}.CraftFrom.MaxIntegrity == -1) ? "∞" : ((this.{22023}.ClientTimeToRestoreIntegrity > 0f) ? StringHelper.TimeMMMSS((double)this.{22023}.ClientTimeToRestoreIntegrity) : ""), (this.{22023}.CraftFrom.MaxIntegrity != -1 && this.{22023}.Integrity <= 3) ? Color.OrangeRed : gray), delegate()
			{
				if (!EducationHelper.MakeInvisibleShipButtons)
				{
					return 1f;
				}
				return 0.3f;
			});
			tlist2.Add(statusDisplay);
			Tlist<{22001}.StatusDisplay> tlist3 = this.{22027};
			statusDisplay = new {22001}.StatusDisplay(Local.armor, gray, new Vector2(52f, this.{22022}.Array[0].Pos.XY.Y + 42f + 4f) - base.Pos.XY, new Func<string>(this.{22015}), delegate()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
				defaultInterpolatedStringHandler.AppendFormatted<float>(this.{22023}.ArmorWithoutTempEffects, "F1");
				return new ValueTuple<string, Color>(defaultInterpolatedStringHandler.ToStringAndClear(), wheat);
			}, () => 1f);
			tlist3.Add(statusDisplay);
			Tlist<{22001}.StatusDisplay> tlist4 = this.{22027};
			statusDisplay = new {22001}.StatusDisplay(Local.speed, gray, new Vector2(52f, this.{22022}.Array[0].Pos.XY.Y + 63f + 4f) - base.Pos.XY, new Func<string>(this.{22016}), delegate()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted<float>(this.{22023}.SpeedWithoutTempEffects * PlayerShipInfo.Temp_displaySpeedRefactoring, "F1");
				defaultInterpolatedStringHandler.AppendLiteral(" - ");
				defaultInterpolatedStringHandler.AppendFormatted<float>((this.{22023}.SpeedWithoutTempEffects + this.{22023}.MarchingModeSpeed) * PlayerShipInfo.Temp_displaySpeedRefactoring, "F1");
				return new ValueTuple<string, Color>(defaultInterpolatedStringHandler.ToStringAndClear(), wheat);
			}, () => 1f);
			tlist4.Add(statusDisplay);
			Tlist<{22001}.StatusDisplay> tlist5 = this.{22027};
			statusDisplay = new {22001}.StatusDisplay(Local.PortСraftShipWindow_8, gray, new Vector2(52f, this.{22022}.Array[0].Pos.XY.Y + 84f + 4f) - base.Pos.XY, new Func<string>(this.{22017}), delegate()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
				defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round((double)this.{22023}.Mobility));
				return new ValueTuple<string, Color>(defaultInterpolatedStringHandler.ToStringAndClear(), wheat);
			}, () => 1f);
			tlist5.Add(statusDisplay);
			Tlist<{22001}.StatusDisplay> tlist6 = this.{22027};
			statusDisplay = new {22001}.StatusDisplay(Local.PortShipInfoGui_5, {22036}, new Vector2(52f, this.{22022}.Array[1].Pos.XY.Y + 4f) - base.Pos.XY, () => null, delegate()
			{
				float itemsMass = this.{22023}.GetItemsMass();
				float capacity = this.{22023}.Capacity;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted<float>(itemsMass);
				defaultInterpolatedStringHandler.AppendLiteral(" / ");
				defaultInterpolatedStringHandler.AppendFormatted<float>(capacity);
				return new ValueTuple<string, Color>(defaultInterpolatedStringHandler.ToStringAndClear(), (itemsMass >= capacity) ? Color.OrangeRed : wheat);
			}, () => 1f);
			tlist6.Add(statusDisplay);
			Tlist<{22001}.StatusDisplay> tlist7 = this.{22027};
			statusDisplay = new {22001}.StatusDisplay(Local.ammo, gray, new Vector2(52f, this.{22022}.Array[1].Pos.XY.Y + 21f + 4f) - base.Pos.XY, () => null, delegate()
			{
				string item;
				if (this.{22023}.StaticInfo.Ports.Length != 0)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
					defaultInterpolatedStringHandler.AppendFormatted<int>(this.{22023}.BallsOfHold.GetTotalItemsCount());
					item = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				else
				{
					item = "-";
				}
				return new ValueTuple<string, Color>(item, (this.{22023}.BallsOfHold.GetTotalItemsCount() < 10 && this.{22023}.StaticInfo.Ports.Length != 0) ? Color.OrangeRed : gray);
			}, () => 1f);
			tlist7.Add(statusDisplay);
			Tlist<{22001}.StatusDisplay> tlist8 = this.{22027};
			statusDisplay = new {22001}.StatusDisplay(Local.weapons_d, {22036}, new Vector2(52f, this.{22022}.Array[2].Pos.XY.Y + 4f) - base.Pos.XY, () => null, delegate()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.{22023}.Cannons.Count);
				defaultInterpolatedStringHandler.AppendLiteral(" / ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.{22023}.StaticInfo.Ports.Count((CannonLocationInfo {22051}) => !{22051}.IsBlocked(Global.Player.UsedShipPlayer)));
				string str = defaultInterpolatedStringHandler.ToStringAndClear();
				string str2;
				if (this.{22023}.Mortars.Count <= 0)
				{
					str2 = "";
				}
				else
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(4, 2);
					defaultInterpolatedStringHandler2.AppendLiteral(", ");
					defaultInterpolatedStringHandler2.AppendFormatted(Local.mortars);
					defaultInterpolatedStringHandler2.AppendLiteral(": ");
					defaultInterpolatedStringHandler2.AppendFormatted<int>(this.{22023}.Mortars.Count);
					str2 = defaultInterpolatedStringHandler2.ToStringAndClear();
				}
				return new ValueTuple<string, Color>(str + str2, (this.{22023}.Cannons.Count < this.{22023}.StaticInfo.Ports.Count((CannonLocationInfo {22052}) => !{22052}.IsBlocked(Global.Player.UsedShipPlayer)) || this.{22023}.Mortars.Count < this.{22023}.StaticInfo.MortarPorts.Count(new Func<CannonLocationInfo, bool>(this.{22018}))) ? Color.OrangeRed : wheat);
			}, delegate()
			{
				if (!EducationHelper.MakeInvisibleShipButtons)
				{
					return 1f;
				}
				return 0.3f;
			});
			tlist8.Add(statusDisplay);
			Tlist<{22001}.StatusDisplay> tlist9 = this.{22027};
			statusDisplay = new {22001}.StatusDisplay(Local.PortСraftShipWindow_14, gray, new Vector2(52f, this.{22022}.Array[2].Pos.XY.Y + 21f + 4f) - base.Pos.XY, () => null, delegate()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.{22023}.StaticInfo.FalkonetPositions.Length);
				return new ValueTuple<string, Color>(defaultInterpolatedStringHandler.ToStringAndClear(), wheat);
			}, delegate()
			{
				if (!EducationHelper.MakeInvisibleShipButtons)
				{
					return 1f;
				}
				return 0.3f;
			});
			tlist9.Add(statusDisplay);
			Tlist<{22001}.StatusDisplay> tlist10 = this.{22027};
			statusDisplay = new {22001}.StatusDisplay(Local.crew, {22036}, new Vector2(52f, this.{22022}.Array[3].Pos.XY.Y + 4f) - base.Pos.XY, new Func<string>(this.{22020}), delegate()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.{22023}.Crew.Count);
				defaultInterpolatedStringHandler.AppendLiteral(" / ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.{22023}.CrewPlaces);
				return new ValueTuple<string, Color>(defaultInterpolatedStringHandler.ToStringAndClear(), (this.{22023}.Crew.Count < this.{22023}.CrewPlaces) ? Color.OrangeRed : wheat);
			}, () => 1f);
			tlist10.Add(statusDisplay);
			Func<int> freePlacesForSpecialUnits = new Func<int>(this.{22021});
			Tlist<{22001}.StatusDisplay> tlist11 = this.{22027};
			statusDisplay = new {22001}.StatusDisplay("", {22036}, new Vector2(52f, this.{22022}.Array[3].Pos.XY.Y + 4f + 21f) - base.Pos.XY, () => null, delegate()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.{22023}.Crew.Special.Size);
				defaultInterpolatedStringHandler.AppendLiteral(" / ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(Global.Player.UsedShip.Crew.MaxSpecialCrew(Session.Account));
				return new ValueTuple<string, Color>(defaultInterpolatedStringHandler.ToStringAndClear(), (freePlacesForSpecialUnits() > 0) ? Color.OrangeRed : Color.Transparent);
			}, delegate()
			{
				if (this.{22023}.StaticInfo.IsBalloon)
				{
					return 0f;
				}
				if (freePlacesForSpecialUnits() > 0 && Session.Account.SpecialUnitsAtStorage.TotalCount < 5)
				{
					return 0f;
				}
				return 0.7f;
			});
			tlist11.Add(statusDisplay);
			Tlist<{22001}.StatusDisplay> tlist12 = this.{22027};
			statusDisplay = new {22001}.StatusDisplay(Local.upgrades, {22036}, new Vector2(52f, this.{22022}.Array[4].Pos.XY.Y + 4f) - base.Pos.XY, () => null, delegate()
			{
				string item;
				if (!this.{22023}.CraftFrom.StaticInfo.IsBalloon)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler.AppendFormatted<int>(this.{22023}.Upgrades.InstalledCount);
					defaultInterpolatedStringHandler.AppendLiteral(" / ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(this.{22023}.MaxUpgradesCount);
					item = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				else
				{
					item = "-";
				}
				Color item2;
				if (this.{22023}.CraftFrom.ID != 2 && !this.{22023}.StaticInfo.IsBalloon)
				{
					if (!this.{22023}.Upgrades.GetUpgrades().Any((InstalledShipUpgradeSlot {22053}) => {22053}.Info.Info.CategoryUi == ShipUpgradeCategory.Sailes))
					{
						item2 = Color.OrangeRed;
						goto IL_12B;
					}
				}
				item2 = (this.{22023}.Upgrades.GetUpgrades().Any((InstalledShipUpgradeSlot {22054}) => {22054}.Info.Strength == 0f && {22054}.Info.Info.WearType > UpgradeStrengthWear.None) ? Color.Orange : wheat);
				IL_12B:
				return new ValueTuple<string, Color>(item, item2);
			}, delegate()
			{
				if (!EducationHelper.MakeInvisibleShipButtons_Upgrade)
				{
					return 1f;
				}
				return 0.3f;
			});
			tlist12.Add(statusDisplay);
			Tlist<{22001}.StatusDisplay> tlist13 = this.{22027};
			statusDisplay = new {22001}.StatusDisplay(Local.design, {22036}, new Vector2(52f, this.{22022}.Array[5].Pos.XY.Y + 4f) - base.Pos.XY, () => null, delegate()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.{22023}.CountInstallerDesignElements());
				defaultInterpolatedStringHandler.AppendLiteral(" / ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.{22023}.CraftFrom.StaticInfo.IsBalloon ? 1 : (PlayerShipDynamicInfo.DesignElementsCount - 1));
				return new ValueTuple<string, Color>(defaultInterpolatedStringHandler.ToStringAndClear(), wheat);
			}, delegate()
			{
				if (!EducationHelper.MakeInvisibleShipButtons)
				{
					return 1f;
				}
				return 0.3f;
			});
			tlist13.Add(statusDisplay);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000A4004 File Offset: 0x000A2204
		private void {22002}()
		{
			this.{22003}(Session.Account.Shipyard.CurrentRealShip);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000A401C File Offset: 0x000A221C
		private void {22003}(PlayerShipDynamicInfo {22004})
		{
			this.{22023} = {22004};
			Form form = this.{22024};
			if (form != null)
			{
				form.RemoveFromContainer();
			}
			Form form2 = new Form(base.Pos.XY + new Vector2(0f, -13f), new Rectangle(503, 609, 295, 58), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form2.AnimatedFocus = false;
			form2.Opacity = 1f;
			Form {13204} = form2;
			this.{22024} = form2;
			base.AddChild({13204});
			this.{22024}.Pos = this.{22024}.Pos.ScaleSize(1.02f);
			this.{22024}.AddChild(new Image(new Marker(8f, -4f, 36f, 36f).Offset(base.Pos.XY), CommonAtlas.Texture.Tex, CommonAtlas.GetShipClassIcon({22004}.CraftFrom.Class, CommonAtlas.ShipClassIconStyle.Blue), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			LiveLabel nameLabelR = new LiveLabel(base.Pos.XY + new Vector2(53f, 2f), Fonts.Philosopher_18, Color.White * 0.7f, () => {22004}.ShipNameVisible + " (" + StringHelper.ToRoman({22004}.CraftFrom.Rank) + ")", 300);
			Action<string> <>9__6;
			nameLabelR.EvClick += delegate(ClickUiEventArgs {22060})
			{
				Action<string> {17401};
				if (({17401} = <>9__6) == null)
				{
					{17401} = (<>9__6 = delegate(string {22061})
					{
						{22004}.CustomName = {22061};
					});
				}
				new {17312}({17401}, 17, Local.PortShipInfoGui_0, null, null);
			};
			LiveLabel nameLabel = new LiveLabel(base.Pos.XY + new Vector2(53f, 29f), Fonts.Arial_10, Color.White * 0.4f, delegate()
			{
				if (!string.IsNullOrEmpty({22004}.CustomName))
				{
					return "(" + {22004}.CraftFrom.ShipName + ")";
				}
				return "";
			}, 300);
			nameLabel.UpdateComplete += delegate(UiControl {22062})
			{
				nameLabel.BasicColor = Color.White * ((nameLabel.InputMode == MouseInputMode.Focused) ? 0.8f : 0.4f);
			};
			Label label = new Label(Vector2.Zero, Fonts.Philosopher_18, Color.White * 0.2f, "✎", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			label.UpdateComplete += delegate(UiControl {22063})
			{
				Marker pos = {22063}.Pos;
				Vector2 vector = new Vector2(nameLabelR.Pos.End.X + 2f, nameLabelR.Pos.XY.Y + 4f);
				{22063}.Pos = pos.SetXY(vector);
			};
			label.EvClick += delegate(ClickUiEventArgs {22064})
			{
				nameLabelR.ImitateClick(false);
			};
			this.{22024}.AddChild(new UiControl[]
			{
				nameLabel,
				nameLabelR,
				label
			});
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000A4274 File Offset: 0x000A2474
		protected override void UserUpdate(ref FrameTime {22005})
		{
			if (this.{22023}.CraftFrom.MaxIntegrity == -1)
			{
				this.{22025}.IsVisible = false;
			}
			else
			{
				this.{22025}.IsVisible = (this.{22023}.ClientTimeToRestoreIntegrity == 0f);
				this.{22025}.MaxValue = this.{22023}.CraftFrom.MaxIntegrity;
				this.{22025}.Value = this.{22023}.Integrity;
				UiControl uiControl = this.{22025};
				Marker pos = this.{22025}.Pos;
				Vector2 vector = this.{22022}.Array[0].Pos.XY + new Vector2(287f - this.{22025}.Pos.WH.X, 29f);
				uiControl.Pos = pos.SetXY(vector);
			}
			float statFine = Session.Account.Shipyard.GetStatFine(this.{22023}.CraftFrom);
			string text;
			if (statFine != 0f)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
				defaultInterpolatedStringHandler.AppendLiteral("-");
				defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round((double)(statFine * 100f)));
				defaultInterpolatedStringHandler.AppendLiteral("%");
				text = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			else
			{
				text = null;
			}
			this.{22028} = text;
			base.Opacity = (({19086}.CurrentMenu != null || {22279}.CurrentInstance != null || {22409}.CurrentInstance != null || {20755}.CurrentInstance != null || {20501}.CurrentInstance != null) ? 0.1f : 1f);
			base.AllowMouseInput = (base.Opacity == 1f);
			if (Global.Render.UiMode == InterfaceMode.Cinematografic)
			{
				base.Opacity *= 0.9f;
			}
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000A4424 File Offset: 0x000A2624
		private static void LeftAlign(string {22006}, Vector2 {22007}, Color {22008})
		{
			Vector2 vector = Fonts.Philosopher_14.Measure({22006});
			{22007}.X -= vector.X;
			Engine.GS.DrawString({22006}, {22007}, {22008});
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x000A4460 File Offset: 0x000A2660
		protected override void UserFrontRender()
		{
			float opcaity = base.GetOpcaity();
			Vector2 value = base.Pos.XY * new Vector2(1f, 0f) + base.Pos.XY;
			foreach ({22001}.StatusDisplay statusDisplay in ((IEnumerable<{22001}.StatusDisplay>)this.{22027}))
			{
				Engine.GS.SetFont(Fonts.Philosopher_14);
				float scale = statusDisplay.GetOwnOpacity() * opcaity;
				Vector2 value2 = value + statusDisplay.Position;
				ValueTuple<string, Color> valueTuple = statusDisplay.GetValueAndColor();
				string item = valueTuple.Item1;
				Color item2 = valueTuple.Item2;
				string text = statusDisplay.GetPrefix();
				Device gs = Engine.GS;
				string statName = statusDisplay.StatName;
				Color color = statusDisplay.StatColor * scale;
				gs.DrawString(statName, value2, color);
				{22001}.LeftAlign(item, value2 + new Vector2(242f, 0f), item2 * scale);
				if (!string.IsNullOrEmpty(text))
				{
					value2 = value + statusDisplay.Position + new Vector2(Engine.GS.Font.Measure(statusDisplay.StatName).X + 2f, 3f);
					Engine.GS.SetFont(Fonts.Arial_10);
					Device gs2 = Engine.GS;
					string {14599} = text;
					color = Color.Pink * scale;
					gs2.DrawString({14599}, value2, color);
				}
			}
			if (!string.IsNullOrEmpty(this.{22028}))
			{
				Vector2 vector = base.Pos.XY + new Vector2(base.Pos.WH.X + 4f, base.Pos.WH.Y - 40f);
				Device gs3 = Engine.GS;
				Rectangle rectangle = new Rectangle(2801, 329, 213, 29);
				Rectangle rectangle2 = new Marker(vector.X - 8f, vector.Y - 8f, 370f, 55f).ToRect();
				Color color = Color.Black * opcaity;
				gs3.Draw(rectangle, rectangle2, color);
				Engine.GS.SetFont(Fonts.Philosopher_14);
				string[] array = Local.ship_not_researched_fine(this.{22028}).Split(": ", StringSplitOptions.None);
				Device gs4 = Engine.GS;
				string {14599}2 = array[0] + Environment.NewLine + ((array.Length > 1) ? array[1] : "");
				color = Color.Pink * opcaity;
				gs4.DrawString({14599}2, vector, color);
			}
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000A48C4 File Offset: 0x000A2AC4
		[CompilerGenerated]
		private void {22009}()
		{
			try
			{
				{22001}.CurrentInstance = null;
				if (Global.Game.ScenePort.ShipsHolder != null)
				{
					Global.Game.ScenePort.ShipsHolder.SeeTargetChanged -= this.{22002};
				}
			}
			catch
			{
			}
			{22279} currentInstance = {22279}.CurrentInstance;
			if (currentInstance == null)
			{
				return;
			}
			currentInstance.RemoveFromContainer();
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000A492C File Offset: 0x000A2B2C
		[CompilerGenerated]
		private void {22010}(ClickUiEventArgs {22011})
		{
			if (this.{22023}.StaticInfo.Ports.Length == 0 || {18807}.CurrentInstance != null)
			{
				return;
			}
			if ({22279}.CurrentInstance == null)
			{
				new {22279}();
				if (Session.Account.EducationQuest.HasFlag(EducationOnboarding.CraftCannon) || Session.Account.EducationQuest.HasFlag(EducationOnboarding.BuildNextShip))
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_SwitchCannons, true);
				}
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000A4990 File Offset: 0x000A2B90
		[CompilerGenerated]
		private void {22012}(UiControl {22013})
		{
			if (Global.Player == null)
			{
				return;
			}
			if (!Global.Game.ScenePort.IsAbleToMendingShip || this.{22023}.ClientTimeToRestoreIntegrity > 0f)
			{
				Color basicColor = Color.Lerp(Color.OrangeRed, Color.White, 0.6f) * 0.5f;
				{22013}.BasicColor = basicColor;
				{22013}.AllowMouseInput = false;
				return;
			}
			{22013}.BasicColor = Color.White;
			{22013}.AllowMouseInput = true;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000A4A08 File Offset: 0x000A2C08
		[CompilerGenerated]
		private string {22014}()
		{
			return this.{22028};
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000A4A08 File Offset: 0x000A2C08
		[CompilerGenerated]
		private string {22015}()
		{
			return this.{22028};
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000A4A08 File Offset: 0x000A2C08
		[CompilerGenerated]
		private string {22016}()
		{
			return this.{22028};
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000A4A08 File Offset: 0x000A2C08
		[CompilerGenerated]
		private string {22017}()
		{
			return this.{22028};
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000A4A10 File Offset: 0x000A2C10
		[CompilerGenerated]
		private bool {22018}(CannonLocationInfo {22019})
		{
			return !{22019}.IsBlocked(this.{22023});
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000A4A08 File Offset: 0x000A2C08
		[CompilerGenerated]
		private string {22020}()
		{
			return this.{22028};
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000A4A21 File Offset: 0x000A2C21
		[CompilerGenerated]
		private int {22021}()
		{
			return Global.Player.UsedShip.Crew.MaxSpecialCrew(Session.Account) - this.{22023}.Crew.Special.Size;
		}

		// Token: 0x0400117C RID: 4476
		public static {22001} CurrentInstance;

		// Token: 0x0400117D RID: 4477
		private static readonly Rectangle[] buttons = new Rectangle[]
		{
			new Rectangle(318, 1070, 47, 48),
			new Rectangle(318, 1119, 47, 48),
			new Rectangle(318, 1168, 47, 48),
			new Rectangle(318, 1217, 47, 48),
			new Rectangle(318, 1266, 47, 48),
			new Rectangle(318, 1315, 47, 48),
			new Rectangle(318, 1364, 47, 48)
		};

		// Token: 0x0400117E RID: 4478
		private static readonly Rectangle[] buttonsF = new Rectangle[]
		{
			new Rectangle(365, 1070, 47, 48),
			new Rectangle(365, 1119, 47, 48),
			new Rectangle(365, 1168, 47, 48),
			new Rectangle(365, 1217, 47, 48),
			new Rectangle(365, 1266, 47, 48),
			new Rectangle(365, 1315, 47, 48),
			new Rectangle(365, 1364, 47, 48)
		};

		// Token: 0x0400117F RID: 4479
		private static readonly Rectangle strengthBtLighted = new Rectangle(414, 1070, 47, 48);

		// Token: 0x04001180 RID: 4480
		private Tlist<AnimatedButton> {22022};

		// Token: 0x04001181 RID: 4481
		private PlayerShipDynamicInfo {22023};

		// Token: 0x04001182 RID: 4482
		private Form {22024};

		// Token: 0x04001183 RID: 4483
		private PointedProgressBar {22025};

		// Token: 0x04001184 RID: 4484
		private AnimatedButton {22026};

		// Token: 0x04001185 RID: 4485
		private Tlist<{22001}.StatusDisplay> {22027};

		// Token: 0x04001186 RID: 4486
		private string {22028};

		// Token: 0x0200037B RID: 891
		private readonly struct StatusDisplay
		{
			// Token: 0x06001347 RID: 4935 RVA: 0x000A4A52 File Offset: 0x000A2C52
			public StatusDisplay(string {22035}, Color {22036}, Vector2 {22037}, Func<string> {22038}, Func<ValueTuple<string, Color>> {22039}, Func<float> {22040})
			{
				this.StatName = {22035};
				this.StatColor = {22036};
				this.Position = {22037};
				this.GetPrefix = {22038};
				this.GetValueAndColor = {22039};
				this.GetOwnOpacity = {22040};
			}

			// Token: 0x04001187 RID: 4487
			public readonly string StatName;

			// Token: 0x04001188 RID: 4488
			public readonly Color StatColor;

			// Token: 0x04001189 RID: 4489
			public readonly Vector2 Position;

			// Token: 0x0400118A RID: 4490
			public readonly Func<string> GetPrefix;

			// Token: 0x0400118B RID: 4491
			public readonly Func<ValueTuple<string, Color>> GetValueAndColor;

			// Token: 0x0400118C RID: 4492
			public readonly Func<float> GetOwnOpacity;
		}
	}
}
