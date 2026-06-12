using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020003A2 RID: 930
	public sealed class {22279} : CustomUi
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x000AA7CF File Offset: 0x000A89CF
		public static bool IsActive
		{
			get
			{
				return {22279}.CurrentInstance != null;
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x000AA7DC File Offset: 0x000A89DC
		public {22279}() : base(false)
		{
			this.{22330} = new {22279}.DeckStatement();
			{22279}.CurrentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{22279}.CurrentInstance = null;
				{22370}.TryClose();
			};
			this.AnimatedFocus = false;
			this.{22329} = new Tlist<{22279}.CannonSlot>(Global.Player.UsedShip.StaticInfo.Ports.Length);
			this.{22293}(Global.Player.UsedShip.StaticInfo.LeftSidePorts);
			this.{22293}(Global.Player.UsedShip.StaticInfo.RightSidePorts);
			this.{22293}(Global.Player.UsedShip.StaticInfo.FrontSidePorts);
			this.{22293}(Global.Player.UsedShip.StaticInfo.BackSidePorts);
			this.{22293}((from {22351} in Global.Player.UsedShip.StaticInfo.MortarPorts
			where !{22351}.AvailableWithUpgrade || Global.Player.UsedShip.HasExtraMortarUpgrade
			select {22351}).ToArray<CannonLocationInfo>());
			this.{22328} = new StackForm(new Vector2(5f, (float)(Engine.GS.UIArea.Height - 302)), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.RightDown)
			{
				BorderThickness = 1f
			};
			Color color = new Color(211, 234, 218);
			Rectangle {22303} = new Rectangle(1450, 184, 35, 31);
			Rectangle {22303}2 = new Rectangle(1486, 184, 35, 31);
			Rectangle {22303}3 = new Rectangle(1522, 184, 35, 31);
			Button button = new Button(Vector2.Zero, {22279}.button_TakeCannonsGray, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button.SetText(Local.PortVisaulEquipmentInterface_3, Fonts.Philosopher_14, color * 0.9f, false);
			button.EvClick += this.{22304};
			this.{22328}.AddSpace(5f);
			this.{22328}.AddItem(new UiControl[]
			{
				button
			});
			if (Global.Player.UsedShip.Mortars.Count > 0)
			{
				Button button2 = new Button(Vector2.Zero, {22279}.button_TakeCannonsGray, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button2.SetText(Local.PortVisaulEquipmentInterface_takeOffMortars, Fonts.Philosopher_14, color, false);
				this.{22328}.AddItem(new UiControl[]
				{
					button2
				});
				button2.EvClick += this.{22308};
				{22279}.<.ctor>g__AddIcon|17_2(button2, {22303}3);
				this.{22328}.Pos = this.{22328}.Pos.Offset(0f, (float)(-(float){22279}.button_TakeCannonsGray.Height));
			}
			Button button3 = new Button(Vector2.Zero, {22279}.button_TakeCannons, PositionAlignment.RightDown, PositionAlignment.RightDown);
			button3.SetText(Local.close, Fonts.Philosopher_14, color, false);
			button3.ExClick(new Action<ClickUiEventArgs>(this.{22310}));
			{22279}.<.ctor>g__AddIcon|17_2(button3, {22303}2);
			Button button4 = new Button(Vector2.Zero, {22279}.button_TakeCannonsGray, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Button button5 = new Button(Vector2.Zero, {22279}.button_TakeCannonsGray, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button4.SetText(Local.PortVisaulEquipmentInterface_1, Fonts.Philosopher_14, color, false);
			button5.SetText(Local.PortVisaulEquipmentInterface_2, Fonts.Philosopher_14, color, false);
			this.{22328}.AddItem(new UiControl[]
			{
				button3,
				button4,
				button5
			});
			{22279}.<.ctor>g__AddIcon|17_2(button4, {22303});
			{22279}.<.ctor>g__AddIcon|17_2(button5, {22303});
			button4.EvClick += this.{22312};
			button5.EvClick += this.{22314};
			if (Global.Player.UsedShip.StaticInfo.FrontSidePorts.Length != 0)
			{
				Button button6 = new Button(Vector2.Zero, {22279}.button_TakeCannonsGray, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button6.SetText(Local.PortVisaulEquipmentInterface_takeOffFront, Fonts.Philosopher_14, color, false);
				this.{22328}.AddItem(new UiControl[]
				{
					button6
				});
				button6.EvClick += this.{22316};
				{22279}.<.ctor>g__AddIcon|17_2(button6, {22303});
			}
			if (Global.Player.UsedShip.StaticInfo.BackSidePorts.Length != 0)
			{
				Button button7 = new Button(Vector2.Zero, {22279}.button_TakeCannonsGray, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button7.SetText(Local.PortVisaulEquipmentInterface_takeOffBack, Fonts.Philosopher_14, color, false);
				this.{22328}.AddItem(new UiControl[]
				{
					button7
				});
				button7.EvClick += this.{22318};
				{22279}.<.ctor>g__AddIcon|17_2(button7, {22303});
			}
			Button button8 = new Button(Vector2.Zero, {22279}.button_TakeCannonsGray, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button8.SetText(Local.PortVisaulEquipmentInterface_c, Fonts.Philosopher_14, color, false);
			button8.EvClick += this.{22320};
			{22279}.<.ctor>g__AddIcon|17_2(button8, {22303});
			this.{22328}.AddItem(new UiControl[]
			{
				button8
			});
			base.AddChild(this.{22328});
			if (Global.Camera.Zoom > 15f)
			{
				Global.Camera.RunFocusInObjectAnimation();
			}
			base.MoveToFrontLevel();
			this.UpdateEquipmentInfo();
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x000AACEC File Offset: 0x000A8EEC
		private void {22280}(CannonLocation {22281})
		{
			Global.Player.UsedShipPlayer.TakeOffAllCannons(Session.Account, (CannonCommon {22363}) => {22363}.Location.Side == {22281});
			foreach ({22279}.CannonSlot cannonSlot in ((IEnumerable<{22279}.CannonSlot>)this.{22329}))
			{
				if (cannonSlot.Location.Side == {22281} && cannonSlot.Location.Mode != CannonLocationMode.Mortar)
				{
					cannonSlot.UpdateSlot();
				}
			}
			Global.Game.ScenePort.UpdateGuiForViewShip();
			this.UpdateEquipmentInfo();
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x000AAD9C File Offset: 0x000A8F9C
		private void {22282}()
		{
			Global.Player.UsedShipPlayer.TakeOffAllMortars(Session.Account);
			foreach ({22279}.CannonSlot cannonSlot in ((IEnumerable<{22279}.CannonSlot>)this.{22329}))
			{
				if (cannonSlot.Location.Mode == CannonLocationMode.Mortar)
				{
					cannonSlot.UpdateSlot();
				}
			}
			Global.Game.ScenePort.UpdateGuiForViewShip();
			this.UpdateEquipmentInfo();
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x000AAE20 File Offset: 0x000A9020
		private void {22283}(out float {22284}, out float {22285})
		{
			float num = 1f + Global.Player.UsedShipPlayer.BallDamageBonusToAll;
			float reloadMul = 1f - Global.Player.UsedShipPlayer.CannonsReloadSpeedBonus;
			if (!this.{22331})
			{
				num = 1f;
				reloadMul = 1f;
			}
			CannonBallInfo ball = Gameplay.BallsInfo.FromID(1);
			float num2 = num * Math.Max(Global.Player.UsedShipPlayer.Cannons.Items.Sum(delegate(CannonCommon {22364})
			{
				if ({22364}.Location.Side != CannonLocation.RightSide)
				{
					return 0f;
				}
				return ({22364}.GameInfo.Penetration + ball.Penetration - 2.1f) * ball.DamageFactor;
			}), Global.Player.UsedShipPlayer.Cannons.Items.Sum(delegate(CannonCommon {22365})
			{
				if ({22365}.Location.Side != CannonLocation.LeftSide)
				{
					return 0f;
				}
				return ({22365}.GameInfo.Penetration + ball.Penetration - 2.1f) * ball.DamageFactor;
			}));
			{22284} = num2;
			if (Global.Player.UsedShipPlayer.Cannons.Items.Count((CannonCommon {22354}) => {22354}.Location.Side == CannonLocation.LeftSide || {22354}.Location.Side == CannonLocation.RightSide) > 0)
			{
				float num3 = (from {22355} in Global.Player.UsedShipPlayer.Cannons.Items
				where {22355}.Location.Side == CannonLocation.LeftSide || {22355}.Location.Side == CannonLocation.RightSide
				select {22355}).Average((CannonCommon {22366}) => reloadMul * {22366}.GameInfo.ReloadTime) + CommonShotRenderer<object>.TotalShotDurationMs(Global.Player.UsedShip.StaticInfo);
				{22285} = num2 * (60000f / num3);
				return;
			}
			{22285} = 0f;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x000AAF90 File Offset: 0x000A9190
		private void {22286}(out float {22287}, out int {22288}, out int {22289}, out float {22290}, out float {22291}, out float {22292})
		{
			IEnumerable<ValueTuple<CannonLocationInfo, CannonGameInfo>> source = Global.Player.UsedShip.StaticInfo.FetchMortars(true, Global.Player.UsedShip);
			IEnumerable<ValueTuple<CannonLocationInfo, CannonGameInfo>> source2 = Global.Player.UsedShip.StaticInfo.FetchMortars(false, Global.Player.UsedShip);
			Func<CannonGameInstance, float> sumFunc = (CannonGameInstance {22356}) => (float)(({22356} == null) ? 0 : {22356}.Info.PenetrationMortar(Global.Player.UsedShipPlayer.CraftFrom));
			{22287} = Math.Max(source.Sum(([TupleElementNames(new string[]
			{
				"location",
				"mortar"
			})] ValueTuple<CannonLocationInfo, CannonGameInfo> {22367}) => sumFunc(Global.Player.UsedShip.Mortars[(int){22367}.Item1.SectionID])), source2.Sum(([TupleElementNames(new string[]
			{
				"location",
				"mortar"
			})] ValueTuple<CannonLocationInfo, CannonGameInfo> {22368}) => sumFunc(Global.Player.UsedShip.Mortars[(int){22368}.Item1.SectionID])));
			CannonGameInstance[] array = Global.Player.UsedShip.Mortars.ToArray<CannonGameInstance>();
			int num;
			if (array.Length != 0)
			{
				num = (int)array.Min((CannonGameInstance {22357}) => {22357}.RemainReserve);
			}
			else
			{
				num = 0;
			}
			{22288} = num;
			int num2;
			if (array.Length != 0)
			{
				num2 = (int)array.Max((CannonGameInstance {22358}) => {22358}.RemainReserve);
			}
			else
			{
				num2 = 0;
			}
			{22289} = num2;
			ValueTuple<float, float, float, float, float, float, float> mortarsAverageParameters = Global.Player.UsedShipPlayer.GetMortarsAverageParameters(Gameplay.BallsInfo.FromID(6));
			{22290} = mortarsAverageParameters.Item1;
			{22291} = mortarsAverageParameters.Item2;
			{22292} = mortarsAverageParameters.Item3;
			if (this.{22331})
			{
				{22287} *= 1f + Global.Player.UsedShip.MortarDamagBonus;
				{22290} *= 1f - Global.Player.UsedShip.MortarDeadzoneDecrease;
				{22291} += Global.Player.UsedShip.MortarMaxDistanceBonus;
				{22292} *= 1f - Global.Player.UsedShip.MortarReloadBonus;
			}
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x000AB150 File Offset: 0x000A9350
		public void UpdateEquipmentInfo()
		{
			StackForm stackForm = this.{22326};
			if (stackForm != null)
			{
				stackForm.RemoveFromContainer();
			}
			StackForm stackForm2 = this.{22327};
			if (stackForm2 != null)
			{
				stackForm2.RemoveFromContainer();
			}
			this.{22326} = new StackForm(new Vector2(0f, this.{22328}.Pos.XY.Y), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.RightDown);
			this.{22327} = new StackForm(new Vector2((float)Engine.GS.UIArea.Width, (float)(Engine.GS.UIArea.Height - {22478}.DefaultHeight - 10)), UiOrientation.VerticalCentroid, PositionAlignment.RightDown, PositionAlignment.RightDown);
			this.{22327}.AddItem(new UiControl[]
			{
				new Label(new Vector2(3f, 3f), Fonts.Philosopher_14Bold, Color.LightGray, Local.installed.ToUpper() + ":", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					Shadowed = true
				}
			});
			Color color = new Color(255, 242, 216) * 0.83137256f;
			if (Global.Player.UsedShip.Mortars.Count > 0)
			{
				float num;
				int num2;
				int num3;
				float value;
				float value2;
				float num4;
				this.{22286}(out num, out num2, out num3, out value, out value2, out num4);
				Form form = new Form(Vector2.Zero, new Rectangle(1298, 392, 212, 96), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form.PosHeight -= 10f;
				TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_12, -3f);
				textBlockBuilder.WriteLine(Local.StringConstants_98 + ((int)num).ToString(), color);
				TextBlockBuilder textBlockBuilder2 = textBlockBuilder;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
				defaultInterpolatedStringHandler.AppendFormatted(Local.StringConstants_85.TrimEnd(' '));
				defaultInterpolatedStringHandler.AppendLiteral(": ");
				defaultInterpolatedStringHandler.AppendFormatted<float>(num4 / 1000f, "F0");
				textBlockBuilder2.WriteLine(defaultInterpolatedStringHandler.ToStringAndClear(), color);
				TextBlockBuilder textBlockBuilder3 = textBlockBuilder;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(3, 3);
				defaultInterpolatedStringHandler2.AppendFormatted(Local.distance.TrimEnd(' '));
				defaultInterpolatedStringHandler2.AppendLiteral(": ");
				defaultInterpolatedStringHandler2.AppendFormatted<float>(value, "F0");
				defaultInterpolatedStringHandler2.AppendLiteral("-");
				defaultInterpolatedStringHandler2.AppendFormatted<float>(value2, "F0");
				textBlockBuilder3.WriteLine(defaultInterpolatedStringHandler2.ToStringAndClear(), color);
				textBlockBuilder.WriteLine(Local.mortar_resource_short(num2.ToString() + "-" + num3.ToString()), (num2 <= 50) ? Color.Yellow : color);
				form.AddChildPos(textBlockBuilder.Create(Vector2.Zero), PositionAlignment.LeftUp, PositionAlignment.Center, 9f);
				this.{22327}.AddItem(new UiControl[]
				{
					form
				});
			}
			IGrouping<CannonGameInfo, CannonCommon>[] array = (from {22359} in Global.Player.UsedShipPlayer.Cannons.Items
			group {22359} by {22359}.GameInfo into {22360}
			orderby {22360}.Count<CannonCommon>() descending
			select {22360}).ToArray<IGrouping<CannonGameInfo, CannonCommon>>();
			Tlist<ValueTuple<CannonGameInfo, int>> tlist = new Tlist<ValueTuple<CannonGameInfo, int>>();
			foreach (IGrouping<CannonGameInfo, CannonCommon> grouping in array)
			{
				Tlist<ValueTuple<CannonGameInfo, int>> tlist2 = tlist;
				ValueTuple<CannonGameInfo, int> valueTuple = new ValueTuple<CannonGameInfo, int>(grouping.Key, grouping.Count<CannonCommon>());
				tlist2.Add(valueTuple);
			}
			float value3;
			float value4;
			this.{22283}(out value3, out value4);
			Form form2 = new Form(Vector2.Zero, new Rectangle(1085, 392, 212, 66), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			TextBlockBuilder textBlockBuilder4 = new TextBlockBuilder(Fonts.Arial_12, -3f);
			TextBlockBuilder textBlockBuilder5 = textBlockBuilder4;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(0, 2);
			defaultInterpolatedStringHandler3.AppendFormatted(Local.board_power);
			defaultInterpolatedStringHandler3.AppendFormatted<float>(value3, "F0");
			textBlockBuilder5.WriteLine(defaultInterpolatedStringHandler3.ToStringAndClear(), color);
			TextBlockBuilder textBlockBuilder6 = textBlockBuilder4;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(0, 2);
			defaultInterpolatedStringHandler4.AppendFormatted(Local.board_dpm);
			defaultInterpolatedStringHandler4.AppendFormatted<float>(value4, "F0");
			textBlockBuilder6.WriteLine(defaultInterpolatedStringHandler4.ToStringAndClear(), color);
			textBlockBuilder4.WriteLine("", color);
			form2.AddChildPos(textBlockBuilder4.Create(Vector2.Zero), PositionAlignment.LeftUp, PositionAlignment.Center, 9f);
			form2.AddChild(new CheckboxControl(new Vector2(8f, 41f), AtlasPortGui.vd_checkBox_18px_true, AtlasPortGui.vd_checkBox_18px_false, this.{22331}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.take_effect, Fonts.Arial_12, color * 0.6f).ExCheckEvent(new Action<CheckboxCheckedEventArgs>(this.{22322})));
			this.{22327}.AddItem(new UiControl[]
			{
				form2
			});
			foreach (ValueTuple<CannonGameInfo, int> valueTuple2 in ((IEnumerable<ValueTuple<CannonGameInfo, int>>)tlist))
			{
				Form form3 = new Form(Vector2.Zero, new Rectangle(1085, 392, 212, 66), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form3.PosHeight = 48f;
				TextBlockBuilder textBlockBuilder7 = new TextBlockBuilder(Fonts.Arial_10, -3f);
				form3.AddChildPos(new Image(new Marker(0f, 0f, 40f, 40f), valueTuple2.Item1.IconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, 8f);
				textBlockBuilder7.WriteLine(valueTuple2.Item1.Name, color * 0.8f);
				textBlockBuilder7.WriteLine(valueTuple2.Item2.ToString() + Local.pcs, color * 0.8f, Fonts.Arial_10Bold);
				form3.AddChildPos(textBlockBuilder7.Create(Vector2.Zero), PositionAlignment.LeftUp, PositionAlignment.Center, 52f);
				{20431}.CannonToolTIp(valueTuple2.Item1, form3, false, false, null);
				this.{22327}.AddItem(new UiControl[]
				{
					form3
				});
			}
			this.{22326}.Pos = this.{22326}.Pos.Offset(5f, -this.{22326}.Pos.WH.Y);
			this.{22327}.Pos = this.{22327}.Pos.Offset(-5f - this.{22327}.Pos.WH.X, -this.{22327}.Pos.WH.Y);
			base.AddChild(new UiControl[]
			{
				this.{22326},
				this.{22327}
			});
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x000AB7E0 File Offset: 0x000A99E0
		private void {22293}(CannonLocationInfo[] {22294})
		{
			for (int i = 0; i < {22294}.Length; i++)
			{
				CannonLocationInfo cannonLocationInfo = {22294}[i];
				if (!cannonLocationInfo.IsBlocked(Global.Player.UsedShipPlayer))
				{
					{22279}.CannonSlot icon = new {22279}.CannonSlot(cannonLocationInfo);
					this.{22329}.Add(icon);
					icon.UpdateSlot();
					icon.VisualizeForm.EvClick += delegate(ClickUiEventArgs {22369})
					{
						{22370}.Open(icon.Location);
					};
					base.AddChild(icon.VisualizeForm);
				}
			}
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x000AB870 File Offset: 0x000A9A70
		public void SetCannons({22279}.SetCannonMode {22295}, CannonGameInstance {22296}, CannonLocationInfo {22297})
		{
			if ({22295} == {22279}.SetCannonMode.RemoveOne)
			{
				if ({22297}.Mode == CannonLocationMode.Mortar)
				{
					Global.Player.UsedShipPlayer.TakeOffMortar(Session.Account, {22297});
				}
				else
				{
					Global.Player.UsedShipPlayer.TakeOffCannon(Session.Account, {22297});
				}
			}
			else if ({22295} == {22279}.SetCannonMode.One)
			{
				if ({22297}.Mode == CannonLocationMode.Mortar)
				{
					foreach (CannonLocationInfo cannonLocationInfo in Global.Player.UsedShip.StaticInfo.MortarPorts)
					{
						CannonGameInstance cannonGameInstance = Global.Player.UsedShip.Mortars[(int)cannonLocationInfo.SectionID];
						if (cannonGameInstance != null && cannonGameInstance.Info.Feature != {22296}.Info.Feature)
						{
							Global.Player.UsedShipPlayer.TakeOffMortar(Session.Account, cannonLocationInfo);
							{19994}.Me({19988}.Info, Local.mortar_differ_types, Array.Empty<object>());
						}
					}
					Global.Player.UsedShipPlayer.SetMortar(Session.Account, {22297}, {22296});
				}
				else if ({22297}.Mode == CannonLocationMode.Special)
				{
					CannonCommon cannonCommon = Global.Player.UsedShipPlayer.Cannons.Items.FirstOrDefault((CannonCommon {22361}) => {22361}.Location.Side == CannonLocation.InFront);
					if (cannonCommon != null && cannonCommon.GameInfo.ID != (short){22296}.InfoID)
					{
						Global.Player.UsedShipPlayer.TakeOffAllCannons(Session.Account, (CannonCommon {22362}) => {22362}.Location.Side == CannonLocation.InFront);
						{19994}.Me({19988}.Info, Local.firegun_differ_types, Array.Empty<object>());
					}
					Global.Player.UsedShipPlayer.SetCannon(Session.Account, {22297}, {22296}.Info);
				}
				else
				{
					Global.Player.UsedShipPlayer.SetCannon(Session.Account, {22297}, {22296}.Info);
				}
			}
			else
			{
				int val = Session.Account.CannonsAtStorage[(int){22296}.Info.ID];
				int num = Math.Min(Global.Player.UsedShip.StaticInfo.Ports.Length, val);
				int deckIndex = this.{22330}.GetDeckIndex({22297});
				foreach ({22279}.CannonSlot cannonSlot in ((IEnumerable<{22279}.CannonSlot>)this.{22329}))
				{
					if (num == 0)
					{
						break;
					}
					if (cannonSlot.Location.Mode == CannonLocationMode.Default && (({22295} == {22279}.SetCannonMode.All && Global.Player.UsedShipPlayer.Cannons.FindByLocation((int)cannonSlot.Location.SectionID) == null) || ({22295} == {22279}.SetCannonMode.Deck && this.{22330}.GetDeckIndex(cannonSlot.Location) == deckIndex) || ({22295} == {22279}.SetCannonMode.Board && cannonSlot.Location.Side == {22297}.Side)))
					{
						Global.Player.UsedShipPlayer.SetCannon(Session.Account, cannonSlot.Location, {22296}.Info);
						num--;
					}
				}
			}
			foreach ({22279}.CannonSlot cannonSlot2 in ((IEnumerable<{22279}.CannonSlot>)this.{22329}))
			{
				cannonSlot2.UpdateSlot();
			}
			{22370}.TryClose();
			this.UpdateEquipmentInfo();
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.Equip1, 0.03f, 1f);
			Global.Game.ScenePort.UpdateGuiForViewShip();
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x000ABBF4 File Offset: 0x000A9DF4
		public void SetHighlight(int {22298} = -1, CannonLocationInfo {22299} = null, CannonLocation? {22300} = null)
		{
			this.{22324} = (({22299} == null) ? {22298} : (({22299}.Mode == CannonLocationMode.Mortar) ? -1 : this.{22330}.GetDeckIndex({22299})));
			this.{22325} = {22300};
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x000ABC21 File Offset: 0x000A9E21
		public void ResetHighlight()
		{
			this.{22324} = -1;
			this.{22325} = null;
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x000ABC38 File Offset: 0x000A9E38
		protected override void UserUpdate(ref FrameTime {22301})
		{
			for (int i = 0; i < this.{22329}.Size; i++)
			{
				this.{22329}.Array[i].Update(ref {22301});
			}
			if (InputHelper.IsClick(Keys.Escape))
			{
				if ({22370}.IsOpened)
				{
					{22370}.TryClose();
					return;
				}
				base.RemoveFromContainer();
			}
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x000ABC8C File Offset: 0x000A9E8C
		protected override void UserFrontRender()
		{
			Engine.GS.SetFont(Fonts.Philosopher_14Bold);
			if (this.{22324} != -1)
			{
				if (this.{22324} == -2)
				{
					for (int i = 0; i < this.{22329}.Size; i++)
					{
						{22279}.CannonSlot cannonSlot = this.{22329}.Array[i];
						if (cannonSlot.IsEmpty && cannonSlot.Location.Mode == CannonLocationMode.Default)
						{
							Vector3 {14815} = cannonSlot.Location.Position;
							{14815} = Global.Player.Transform.Transform3X3({14815});
							Device gs = Engine.GS;
							Rectangle rectangle = new Rectangle(640, 0, 59, 59);
							Vector2 vector = Engine.GS.Camera.GetProjection(ref {14815}) - new Vector2(29f, 14f) - new Vector2(0f, 10f);
							Color color = Color.White * {22279}.CannonSlot.GetOpacity(cannonSlot.Location);
							gs.Draw(rectangle, vector, color);
						}
					}
				}
				else
				{
					Tlist<CannonLocationInfo> deckFromIndex = this.{22330}.GetDeckFromIndex(this.{22324});
					for (int j = 0; j < deckFromIndex.Size; j++)
					{
						CannonLocationInfo cannonLocationInfo = deckFromIndex.Array[j];
						Vector3 {14815}2 = cannonLocationInfo.Position;
						{14815}2 = Global.Player.Transform.Transform3X3({14815}2);
						Device gs2 = Engine.GS;
						Rectangle rectangle = new Rectangle(640, 0, 59, 59);
						Vector2 vector = Engine.GS.Camera.GetProjection(ref {14815}2) - new Vector2(29f, 14f) - new Vector2(0f, 10f);
						Color color = Color.White * {22279}.CannonSlot.GetOpacity(cannonLocationInfo);
						gs2.Draw(rectangle, vector, color);
					}
				}
			}
			if (this.{22325} != null)
			{
				foreach ({22279}.CannonSlot cannonSlot2 in ((IEnumerable<{22279}.CannonSlot>)this.{22329}))
				{
					if (cannonSlot2.Location.Side == this.{22325}.Value)
					{
						Vector3 {14815}3 = cannonSlot2.Location.Position;
						{14815}3 = Global.Player.Transform.Transform3X3({14815}3);
						Device gs3 = Engine.GS;
						Rectangle rectangle = new Rectangle(640, 0, 59, 59);
						Vector2 vector = Engine.GS.Camera.GetProjection(ref {14815}3) - new Vector2(29f, 14f) - new Vector2(0f, 10f);
						Color color = Color.White * {22279}.CannonSlot.GetOpacity(cannonSlot2.Location);
						gs3.Draw(rectangle, vector, color);
					}
				}
			}
			if (!{22370}.IsOpened && base.InputMode != MouseInputMode.NoFocus)
			{
				ScreenOcclusionControl.Result result = ScreenOcclusionControl.SelectedVisualItem(Global.Player);
				CannonLocationInfo cannonLocationInfo2 = result.Reference as CannonLocationInfo;
				if (cannonLocationInfo2 != null)
				{
					CannonGameInstance {20477} = (cannonLocationInfo2.Mode == CannonLocationMode.Mortar) ? Global.Player.UsedShip.Mortars[(int)cannonLocationInfo2.SectionID] : null;
					CannonGameInfo cannonGameInfo;
					if (cannonLocationInfo2.Mode != CannonLocationMode.Mortar)
					{
						CannonCommon cannonCommon = Global.Player.UsedShip.Cannons.FindByLocation((int)cannonLocationInfo2.SectionID);
						cannonGameInfo = ((cannonCommon != null) ? cannonCommon.GameInfo : null);
					}
					else
					{
						CannonGameInstance cannonGameInstance = Global.Player.UsedShip.Mortars[(int)cannonLocationInfo2.SectionID];
						cannonGameInfo = ((cannonGameInstance != null) ? cannonGameInstance.Info : null);
					}
					CannonGameInfo cannonGameInfo2 = cannonGameInfo;
					if (base.ToolTip == null || base.ToolTip.Tag != cannonLocationInfo2)
					{
						ToolTip toolTip = base.ToolTip;
						if (toolTip != null)
						{
							toolTip.CloseIfIsOpen();
						}
						if (cannonGameInfo2 != null)
						{
							{20431}.CannonToolTIp(cannonGameInfo2, this, false, false, {20477});
							base.ToolTip.Tag = cannonLocationInfo2;
						}
					}
					if (!InputHelper.NowMouseState.LeftPressed)
					{
						return;
					}
					using (IEnumerator<{22279}.CannonSlot> enumerator = ((IEnumerable<{22279}.CannonSlot>)this.{22329}).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							{22279}.CannonSlot cannonSlot3 = enumerator.Current;
							if (cannonSlot3.Location == cannonLocationInfo2 && !cannonSlot3.Location.IsStatic)
							{
								{22370}.Open(cannonSlot3.Location);
							}
						}
						return;
					}
				}
				FalkonetLocationInfo falkonetLocationInfo = result.Reference as FalkonetLocationInfo;
				if (falkonetLocationInfo == null)
				{
					ToolTip toolTip2 = base.ToolTip;
					if (toolTip2 != null)
					{
						toolTip2.CloseIfIsOpen();
					}
					base.ToolTip = null;
					return;
				}
				if (base.ToolTip == null || base.ToolTip.Tag != falkonetLocationInfo)
				{
					ToolTip toolTip3 = base.ToolTip;
					if (toolTip3 != null)
					{
						toolTip3.CloseIfIsOpen();
					}
					base.ToolTipState = new ToolTipState("", Local.falconet_tooltip, Array.Empty<ToolTipCharacteristics>());
					base.ToolTip.Tag = falkonetLocationInfo;
					return;
				}
			}
			else
			{
				ToolTip toolTip4 = base.ToolTip;
				if (toolTip4 != null)
				{
					toolTip4.CloseIfIsOpen();
				}
				base.ToolTip = null;
			}
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x000AC1C6 File Offset: 0x000AA3C6
		[CompilerGenerated]
		internal static void <.ctor>g__AddIcon|17_2(Button {22302}, Rectangle {22303})
		{
			{22302}.AddChild(new Form({22302}.Pos.XY, {22303}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x000AC1E8 File Offset: 0x000AA3E8
		[CompilerGenerated]
		private void {22304}(ClickUiEventArgs {22305})
		{
			new {17473}(delegate(object {22352})
			{
			}, new {17473}.Item[]
			{
				new {17473}.Item(null, Local.all_cannons, true, default(ImageDecription), null, new Action(this.{22306})),
				new {17473}.Item(null, Local.all_mortars, true, default(ImageDecription), null, new Action(this.{22307}))
			});
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x000AC274 File Offset: 0x000AA474
		[CompilerGenerated]
		private void {22306}()
		{
			using (IEnumerator<PlayerShipDynamicInfo> enumerator = Session.Account.Shipyard.List.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					enumerator.Current.TakeOffAllCannons(Session.Account, (CannonCommon {22353}) => true);
				}
			}
			foreach ({22279}.CannonSlot cannonSlot in ((IEnumerable<{22279}.CannonSlot>)this.{22329}))
			{
				cannonSlot.UpdateSlot();
			}
			Global.Game.ScenePort.UpdateGuiForViewShip();
			this.UpdateEquipmentInfo();
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x000AC33C File Offset: 0x000AA53C
		[CompilerGenerated]
		private void {22307}()
		{
			foreach (PlayerShipDynamicInfo playerShipDynamicInfo in Session.Account.Shipyard.List)
			{
				playerShipDynamicInfo.TakeOffAllMortars(Session.Account);
			}
			foreach ({22279}.CannonSlot cannonSlot in ((IEnumerable<{22279}.CannonSlot>)this.{22329}))
			{
				cannonSlot.UpdateSlot();
			}
			Global.Game.ScenePort.UpdateGuiForViewShip();
			this.UpdateEquipmentInfo();
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x000AC3E4 File Offset: 0x000AA5E4
		[CompilerGenerated]
		private void {22308}(ClickUiEventArgs {22309})
		{
			this.{22282}();
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x000AC3EC File Offset: 0x000AA5EC
		[CompilerGenerated]
		private void {22310}(ClickUiEventArgs {22311})
		{
			base.RemoveFromContainer();
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x000AC3F4 File Offset: 0x000AA5F4
		[CompilerGenerated]
		private void {22312}(ClickUiEventArgs {22313})
		{
			this.{22280}(CannonLocation.LeftSide);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x000AC3FD File Offset: 0x000AA5FD
		[CompilerGenerated]
		private void {22314}(ClickUiEventArgs {22315})
		{
			this.{22280}(CannonLocation.RightSide);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x000AC406 File Offset: 0x000AA606
		[CompilerGenerated]
		private void {22316}(ClickUiEventArgs {22317})
		{
			this.{22280}(CannonLocation.InFront);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x000AC40F File Offset: 0x000AA60F
		[CompilerGenerated]
		private void {22318}(ClickUiEventArgs {22319})
		{
			this.{22280}(CannonLocation.InBack);
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x000AC418 File Offset: 0x000AA618
		[CompilerGenerated]
		private void {22320}(ClickUiEventArgs {22321})
		{
			this.{22280}(CannonLocation.LeftSide);
			this.{22280}(CannonLocation.RightSide);
			this.{22280}(CannonLocation.InFront);
			this.{22280}(CannonLocation.InBack);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x000AC436 File Offset: 0x000AA636
		[CompilerGenerated]
		private void {22322}(CheckboxCheckedEventArgs {22323})
		{
			this.{22331} = {22323}.NewValue;
			this.UpdateEquipmentInfo();
		}

		// Token: 0x04001253 RID: 4691
		public static {22279} CurrentInstance;

		// Token: 0x04001254 RID: 4692
		private static readonly Rectangle button_TakeCannons = new Rectangle(1240, 153, 209, 31);

		// Token: 0x04001255 RID: 4693
		private static readonly Rectangle button_TakeCannonsGray = new Rectangle(1240, 184, 209, 31);

		// Token: 0x04001256 RID: 4694
		private static readonly Rectangle c_cannon3DSelection = new Rectangle(730, 0, 277, 104);

		// Token: 0x04001257 RID: 4695
		private int {22324} = -1;

		// Token: 0x04001258 RID: 4696
		private CannonLocation? {22325};

		// Token: 0x04001259 RID: 4697
		private StackForm {22326};

		// Token: 0x0400125A RID: 4698
		private StackForm {22327};

		// Token: 0x0400125B RID: 4699
		private StackForm {22328};

		// Token: 0x0400125C RID: 4700
		private Tlist<{22279}.CannonSlot> {22329};

		// Token: 0x0400125D RID: 4701
		private {22279}.DeckStatement {22330};

		// Token: 0x0400125E RID: 4702
		private bool {22331};

		// Token: 0x020003A3 RID: 931
		private class CannonSlot
		{
			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x06001451 RID: 5201 RVA: 0x000AC44A File Offset: 0x000AA64A
			public CannonLocationInfo Location { get; }

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x06001452 RID: 5202 RVA: 0x000AC452 File Offset: 0x000AA652
			public Form VisualizeForm { get; }

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x06001453 RID: 5203 RVA: 0x000AC45A File Offset: 0x000AA65A
			public bool IsEmpty
			{
				get
				{
					return this.{22344} == null;
				}
			}

			// Token: 0x06001454 RID: 5204 RVA: 0x000AC468 File Offset: 0x000AA668
			public CannonSlot(CannonLocationInfo {22333})
			{
				this.Location = {22333};
				this.VisualizeForm = new Form(new Marker(0f, 0f, (float){22279}.CannonSlot.formBoundPath.Width, (float){22279}.CannonSlot.formBoundPath.Height), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				this.VisualizeForm.AddChild(this.{22343} = new Image({22279}.CannonSlot.visualizeFormIconPos, AtlasGameGui.Texture.Tex, default(Rectangle), PositionAlignment.Both, PositionAlignment.Both));
				this.VisualizeForm.AddChild(new Form(this.VisualizeForm.Pos, {22279}.CannonSlot.formBoundPath, PositionAlignment.Both, PositionAlignment.Both)
				{
					AnimatedFocus = false
				});
				this.UpdateSlot();
				this.{22337}();
				this.VisualizeForm.ToolTipState = new ToolTipState(null, Local.PortVisaulEquipmentInterface_0, Array.Empty<ToolTipCharacteristics>());
			}

			// Token: 0x06001455 RID: 5205 RVA: 0x000AC53E File Offset: 0x000AA73E
			private static Rectangle GetPath(CannonLocationMode {22334}, bool {22335})
			{
				if ({22335})
				{
					return {22279}.CannonSlot.emptyPlace_mortar;
				}
				if ({22334} != CannonLocationMode.Special)
				{
					return {22279}.CannonSlot.emptyPlace;
				}
				return {22279}.CannonSlot.emptyPlace_fireGun;
			}

			// Token: 0x06001456 RID: 5206 RVA: 0x000AC558 File Offset: 0x000AA758
			public void Update(ref FrameTime {22336})
			{
				this.{22337}();
			}

			// Token: 0x06001457 RID: 5207 RVA: 0x000AC560 File Offset: 0x000AA760
			private void {22337}()
			{
				Vector3 position = this.Location.Position;
				float num = 0.6f;
				position.Y += num;
				if (this.Location.Mode == CannonLocationMode.Mortar)
				{
					position.Y += 0.8f;
				}
				else
				{
					float num2 = 0.9f;
					position.Z += num2 * (float)((this.Location.Side == CannonLocation.LeftSide) ? 1 : ((this.Location.Side == CannonLocation.RightSide) ? -1 : 0));
					position.X += num2 * (float)((this.Location.Side == CannonLocation.InFront) ? 1 : ((this.Location.Side == CannonLocation.InBack) ? -1 : 0));
				}
				Global.Player.Transform.Transform3X3(ref position, out this.{22345});
				Vector2 projection = Global.Camera.GetProjection(ref this.{22345});
				if (float.IsNaN(projection.X) || float.IsNaN(projection.Y))
				{
					return;
				}
				float num3 = Vector3.Distance(this.{22345}, Global.Camera.Position);
				float scaleFactor = Math.Max(0.1f, Math.Min(1f, 6f / num3) * 1.2f * ((this.Location.Mode == CannonLocationMode.Special) ? 1.7f : 1f));
				Vector2 value = new Vector2((float){22279}.CannonSlot.formBoundPath.Width, (float){22279}.CannonSlot.formBoundPath.Height);
				UiControl visualizeForm = this.VisualizeForm;
				Vector2 vector = projection - value * 0.5f * scaleFactor;
				Vector2 vector2 = value * scaleFactor;
				visualizeForm.Pos = new Marker(ref vector, ref vector2);
				this.VisualizeForm.Opacity = MathF.Sqrt(Geometry.Saturate(({22279}.CannonSlot.GetOpacity(this.Location) - 0.75f) / 0.25f));
				this.VisualizeForm.RenderToDepthMap = (this.VisualizeForm.Opacity > 0.5f);
				this.VisualizeForm.AllowMouseInput = (this.VisualizeForm.Opacity > 0.5f);
			}

			// Token: 0x06001458 RID: 5208 RVA: 0x000AC76C File Offset: 0x000AA96C
			public static float GetOpacity(CannonLocationInfo {22338})
			{
				if ({22338}.Mode == CannonLocationMode.Mortar)
				{
					float val = Vector2.Dot((Global.Player.Transform.Transform3X3({22338}.Position).XZ - Global.Player.Position).Normal, (Engine.GS.Camera.Position.XZ - Global.Player.Position).Normal);
					return 0.5f + MathF.Pow(Math.Max(Math.Max(0f, val), -Global.Camera.Direction.Y * 1.5f + 0.1f), 0.25f) * 0.5f;
				}
				Vector2 value = Global.Camera.Direction.XZNormal();
				Vector2 value2 = Geometry.SubstructRotate(Global.Player.Rotation + (({22338}.Side == CannonLocation.InFront) ? 1.5707964f : (({22338}.Side == CannonLocation.InBack) ? -1.5707964f : (({22338}.Side == CannonLocation.LeftSide) ? 3.1415927f : 0f))) + 1.5707964f, 1f);
				value2.Normalize();
				float num = Vector2.Dot(value, value2);
				return 0.75f + num * 0.25f;
			}

			// Token: 0x06001459 RID: 5209 RVA: 0x000AC8AC File Offset: 0x000AAAAC
			public void UpdateSlot()
			{
				CannonGameInfo {22340};
				if (this.Location.Mode != CannonLocationMode.Mortar)
				{
					CannonCommon cannonCommon = Global.Player.UsedShip.Cannons.FindByLocation((int)this.Location.SectionID);
					{22340} = ((cannonCommon != null) ? cannonCommon.GameInfo : null);
				}
				else
				{
					CannonGameInstance cannonGameInstance = Global.Player.UsedShip.Mortars[(int)this.Location.SectionID];
					{22340} = ((cannonGameInstance != null) ? cannonGameInstance.Info : null);
				}
				this.{22339}({22340});
			}

			// Token: 0x0600145A RID: 5210 RVA: 0x000AC928 File Offset: 0x000AAB28
			private void {22339}(CannonGameInfo {22340})
			{
				this.{22344} = {22340};
				if ({22340} == null)
				{
					this.VisualizeForm.IsVisible = true;
					this.{22343}.Texture = AtlasPortGui.Texture.Tex;
					this.{22343}.TexturePath = {22279}.CannonSlot.GetPath(this.Location.Mode, this.Location.Mode == CannonLocationMode.Mortar);
					this.{22343}.ToolTip = null;
					return;
				}
				this.VisualizeForm.IsVisible = false;
				this.{22343}.Texture = {22340}.IconTexture;
				this.{22343}.TexturePath = {22340}.IconTexture.Bounds;
			}

			// Token: 0x0400125F RID: 4703
			private static readonly Rectangle formBoundPath = new Rectangle(1571, 0, 66, 66);

			// Token: 0x04001260 RID: 4704
			private static readonly Rectangle emptyPlace = new Rectangle(1639, 1, 64, 64);

			// Token: 0x04001261 RID: 4705
			private static readonly Rectangle emptyPlace_mortar = new Rectangle(1588, 161, 64, 64);

			// Token: 0x04001262 RID: 4706
			private static readonly Rectangle emptyPlace_fireGun = new Rectangle(1639, 66, 64, 64);

			// Token: 0x04001263 RID: 4707
			private static readonly Marker visualizeFormIconPos = new Marker(1f, 1f, 64f, 64f);

			// Token: 0x04001264 RID: 4708
			[CompilerGenerated]
			private readonly CannonLocationInfo {22341};

			// Token: 0x04001265 RID: 4709
			[CompilerGenerated]
			private readonly Form {22342};

			// Token: 0x04001266 RID: 4710
			private Image {22343};

			// Token: 0x04001267 RID: 4711
			private CannonGameInfo {22344};

			// Token: 0x04001268 RID: 4712
			private Vector3 {22345};
		}

		// Token: 0x020003A4 RID: 932
		private class DeckStatement
		{
			// Token: 0x0600145C RID: 5212 RVA: 0x000ACA4C File Offset: 0x000AAC4C
			public DeckStatement()
			{
				this.{22348} = new Tlist<CannonLocationInfo>(Global.Player.UsedShip.StaticInfo.FrontSidePorts);
				this.{22349} = new Tlist<CannonLocationInfo>(Global.Player.UsedShip.StaticInfo.BackSidePorts);
				this.{22350} = new Dictionary<short, int>();
				for (int i = 0; i < Global.Player.UsedShip.StaticInfo.Decks.Size; i++)
				{
					foreach (CannonLocationInfo cannonLocationInfo in ((IEnumerable<CannonLocationInfo>)Global.Player.UsedShip.StaticInfo.Decks.Array[i]))
					{
						this.{22350}.Add(cannonLocationInfo.SectionID, i);
					}
				}
			}

			// Token: 0x0600145D RID: 5213 RVA: 0x000ACB2C File Offset: 0x000AAD2C
			public Tlist<CannonLocationInfo> GetDeckFromIndex(int {22346})
			{
				if ({22346} == 1000)
				{
					return this.{22349};
				}
				if ({22346} == 1001)
				{
					return this.{22348};
				}
				return Global.Player.UsedShip.StaticInfo.Decks.Array[{22346}];
			}

			// Token: 0x0600145E RID: 5214 RVA: 0x000ACB67 File Offset: 0x000AAD67
			public int GetDeckIndex(CannonLocationInfo {22347})
			{
				if ({22347}.Side == CannonLocation.InBack)
				{
					return 1000;
				}
				if ({22347}.Side == CannonLocation.InFront)
				{
					return 1001;
				}
				return this.{22350}[{22347}.SectionID];
			}

			// Token: 0x04001269 RID: 4713
			private Tlist<CannonLocationInfo> {22348};

			// Token: 0x0400126A RID: 4714
			private Tlist<CannonLocationInfo> {22349};

			// Token: 0x0400126B RID: 4715
			private Dictionary<short, int> {22350};
		}

		// Token: 0x020003A5 RID: 933
		public enum SetCannonMode
		{
			// Token: 0x0400126D RID: 4717
			One,
			// Token: 0x0400126E RID: 4718
			All,
			// Token: 0x0400126F RID: 4719
			Deck,
			// Token: 0x04001270 RID: 4720
			Board,
			// Token: 0x04001271 RID: 4721
			RemoveOne
		}
	}
}
