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
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200033F RID: 831
	internal sealed class {21596} : {21684}
	{
		// Token: 0x06001213 RID: 4627 RVA: 0x000983AC File Offset: 0x000965AC
		internal static Form CreateCrewUnitBasicForm(UnitInfo {21597}, out Color {21598})
		{
			{21598} = Color.LightGray;
			Form form = new Form(Vector2.Zero, {21684}.c_itemNewUnit, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{20431}.Set(form, {21597}, 0, null);
			if ({21597}.Type == UnitType.Sailor)
			{
				Form form2 = form;
				float {13335} = 75f;
				float {13336} = 40f;
				CustomSpriteFont arial_ = Fonts.Arial_10;
				Color {13338} = {21598} * 0.77f;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted(Local.weapons_speed_bycrew);
				defaultInterpolatedStringHandler.AppendLiteral(": ");
				defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round((double)(100f * Global.Player.UsedShip.Crew.Effectivity(Global.Player.UsedShip))));
				defaultInterpolatedStringHandler.AppendLiteral("%");
				form2.AddChild(new Label({13335}, {13336}, arial_, {13338}, defaultInterpolatedStringHandler.ToStringAndClear(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			form.AddChild(new Image({21596}.iconPosition, {21597}.Icon, {21597}.Icon.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChildPos(new Label(0f, 0f, Fonts.Philosopher_14, {21598}, {21597}.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, 75f);
			return form;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x000984CF File Offset: 0x000966CF
		protected override float MainListMaxHeight
		{
			get
			{
				return 580f;
			}
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000984D8 File Offset: 0x000966D8
		public {21596}() : base(Local.crew, "", "")
		{
			{21596}.CurrentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{21596}.CurrentInstance = null;
			};
			this.TwoColumnsSelectionForm = (this.HaveFilter = true);
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			string portEquipUnitsFilterShipWindow_ = Local.PortEquipUnitsFilterShipWindow_1;
			dictionary[portEquipUnitsFilterShipWindow_] = 0;
			string key = SpecialUnitClass.Pirates.ToStringLocal();
			dictionary[key] = 1;
			string key2 = SpecialUnitClass.Sailors.ToStringLocal();
			dictionary[key2] = 2;
			string key3 = SpecialUnitClass.Combats.ToStringLocal();
			dictionary[key3] = 3;
			string key4 = SpecialUnitClass.Adventurers.ToStringLocal();
			dictionary[key4] = 4;
			string favorite_units = Local.favorite_units;
			dictionary[favorite_units] = 5;
			this.FilterMenu = dictionary;
			if (Global.Player.UsedShipPlayer.Crew.Count < Global.Player.UsedShipPlayer.CrewPlaces)
			{
				if (Session.Account.UnitsAtStorage.GetTotalItemsCount() > 5)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_EquipUnitsFromPort, true);
				}
				else
				{
					EducationHelper.MakeFlag(EducationOnboarding.OpenUnitsWindowWithoutUnits, true);
				}
			}
			Global.Network.Send(default(OnGetCrewPortLimits));
			this.{21599}();
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00098624 File Offset: 0x00096824
		protected override void UserBackRender()
		{
			if (this.{21630} != null)
			{
				Engine.GS.SetTexture(AtlasGameGui.Texture.Tex);
				{18139}.sortedRenderQuery.Clear();
				this.{21630}.PrepareDraw({18139}.sortedRenderQuery);
				{18139}.sortedRenderQuery.SortTop(({18187}.VisualGroupConnection {21632}) => Vector3.Distance({21632}.ComputedPosition3D, Engine.GS.Camera.Position));
				foreach ({18187}.VisualGroupConnection visualGroupConnection in ((IEnumerable<{18187}.VisualGroupConnection>){18139}.sortedRenderQuery))
				{
					visualGroupConnection.Draw(HealthBarStyle.Lime, null);
				}
				Engine.GS.SetTexture(AtlasPortGui.Texture.Tex);
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x000986E8 File Offset: 0x000968E8
		private void {21599}()
		{
			StackForm stackForm = this.{21629};
			if (stackForm != null)
			{
				stackForm.RemoveFromContainer();
			}
			this.{21629} = new StackForm(base.Pos.XY + new Vector2(0f, 78f), UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{21629}.AddSpace(14f);
			this.{21629}.AddItem(new UiControl[]
			{
				new Button(Vector2.Zero, {21684}.c_yellowButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					PosWidth = 120f
				}.SetText(Local.PortEquipUnitsShipWindow_16, Fonts.Arial_10, Color.White * 0.8f, false).ExClick(new Action<ClickUiEventArgs>(this.{21621}))
			});
			this.{21629}.AddSpace((float)(137 - {21684}.c_yellowButton.Width));
			this.{21629}.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Gray, Local.TradePortInterface_24, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			StackForm stackForm2 = this.{21629};
			UiControl[] array = new UiControl[1];
			int num = 0;
			Vector2 zero = Vector2.Zero;
			CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
			Color {13344} = (Global.Player.UsedShipPlayer.Crew.Count != Global.Player.UsedShipPlayer.CrewPlaces) ? Color.Orange : Color.White;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted<int>(Global.Player.UsedShipPlayer.Crew.Count);
			defaultInterpolatedStringHandler.AppendLiteral("/");
			defaultInterpolatedStringHandler.AppendFormatted<int>(Global.Player.UsedShipPlayer.CrewPlaces);
			array[num] = new Label(zero, philosopher_, {13344}, defaultInterpolatedStringHandler.ToStringAndClear(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm2.AddItem(array);
			this.{21629}.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Gray, Local.and, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			StackForm stackForm3 = this.{21629};
			UiControl[] array2 = new UiControl[1];
			int num2 = 0;
			Vector2 zero2 = Vector2.Zero;
			CustomSpriteFont philosopher_2 = Fonts.Philosopher_14;
			Color gray = Color.Gray;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler2.AppendFormatted<int>(Session.Account.UnitsAtStorage.GetTotalItemsCount());
			array2[num2] = new Label(zero2, philosopher_2, gray, defaultInterpolatedStringHandler2.ToStringAndClear(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm3.AddItem(array2);
			this.{21629}.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Gray, " " + Local.port_units.ToLower(), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			base.AddChild(this.{21629});
			if (Global.Player.UsedShip.Crew.Count <= 0)
			{
				this.{21630} = null;
				base.UpdateTitleLabel(Local.ClientDrop_0, new Color?(Color.Orange));
				return;
			}
			this.{21630} = new {18187}(Global.Player, new BoardingCombatInstance(Global.Player), Global.Player.Client.Scene.CrewInstancesByPlaces);
			if (Global.Player.UsedShipPlayer.Crew.CountOfSailors < Global.Player.UsedShipPlayer.NeedSailors)
			{
				base.UpdateTitleLabel(Local.equip_units_window_not_enough_sailor, new Color?(Color.Orange));
				return;
			}
			if (Global.Player.UsedShipPlayer.Crew.CountOfBoardingUnits < Global.Player.UsedShipPlayer.NeedBoarding)
			{
				base.UpdateTitleLabel(Local.equip_units_window_not_enough_boardin, new Color?(Color.Orange));
				return;
			}
			base.UpdateTitleLabel(Local.crew, null);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00098A4C File Offset: 0x00096C4C
		private void {21600}(Form {21601}, Vector2 {21602}, int {21603}, string {21604})
		{
			{21601}.AddChild(new ProgressBar({21602}, {21684}.c_sectionProgressBar_front, {21684}.c_sectionProgressBar_back, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				MaxValue = 4f,
				Value = (float){21603}
			});
			Label {13204} = new Label({21602} + new Vector2(43f, -3f), Fonts.Arial_9, Color.Gray, {21604}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{21601}.AddChild({13204});
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00098ABB File Offset: 0x00096CBB
		protected override float GetMainListHeight()
		{
			return this.MainListMaxHeight;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00098AC3 File Offset: 0x00096CC3
		protected override IEnumerable<UiControl> CreateDesignComponents()
		{
			{21596}.<CreateDesignComponents>d__14 <CreateDesignComponents>d__ = new {21596}.<CreateDesignComponents>d__14(-2);
			<CreateDesignComponents>d__.<>4__this = this;
			return <CreateDesignComponents>d__;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00098AD4 File Offset: 0x00096CD4
		private UiControl {21605}(int {21606})
		{
			Form form = new Form(Vector2.Zero, {21684}.c_itemNewUnit, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.BasicColor = Color.Black * 0.3f;
			form.AddChild(new Form(new Marker(5f, 3f, 60f, 60f), new Rectangle(980, 1447, 101, 101), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				FirstOpacity = 0.7f,
				AnimatedFocus = false
			});
			form.EvClick += this.{21625};
			return form;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00098B64 File Offset: 0x00096D64
		private UiControl {21607}(SpecialUnitInstance {21608})
		{
			Form form = new Form(Vector2.Zero, {21684}.c_itemNewUnit, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.BasicColor = Color.Black * 0.3f;
			form.ToolTip = new ToolTip({21608}.ToolTip());
			Image {13204} = new Image({21596}.iconPosition, {21608}.GetInfo.Icon, {21608}.GetInfo.Icon.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChild({13204});
			Label {12952} = new Label(Vector2.Zero, Fonts.Philosopher_14, Color.LightGray, {21608}.GetInfo.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos({12952}, PositionAlignment.LeftUp, PositionAlignment.Center, 75f);
			CustomSpriteFont arial_ = Fonts.Arial_9;
			string bonusDescriptionShort = {21608}.GetInfo.GetBonusDescriptionShort(arial_, 390);
			Label {12956} = new Label(Vector2.Zero, arial_, Color.Lerp(new Color(105, 173, 117), Color.Wheat, 0.5f) * 0.8f, bonusDescriptionShort, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos({12956}, PositionAlignment.LeftUp, PositionAlignment.RightDown, 75f, 8f, false);
			form.AddChild(new Label(new Vector2(75f, 8f), arial_, Color.LightYellow * 0.6f, StringHelper.TimeDHM((double)({21608}.LeftContractTimeHours * 3600f), true), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			Button button = new Button(Vector2.Zero, {21684}.c_smallButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button.SetText(Local.to_port_units, Fonts.Arial_12, Color.LightGray, false);
			button.FirstOpacity = 0f;
			button.UpdateComplete += delegate(UiControl {21643})
			{
				{21643}.FirstOpacity = (form.InputMode > MouseInputMode.NoFocus);
			};
			button.EvClick += delegate(ClickUiEventArgs {21644})
			{
				Session.Account.SpecialUnitsAtStorage.Add({21608});
				Global.Player.UsedShip.Crew.Remove({21608}.GetInfo, 1);
				this.UpdateBlocks(false);
			};
			form.AddChildPos(button, PositionAlignment.RightDown, PositionAlignment.LeftUp, 10f, 10f, false);
			return form;
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00098D84 File Offset: 0x00096F84
		private Form {21609}()
		{
			ShipBonus[] array = Global.Player.UsedShip.Crew.Effects.EnumerateEffects().ToArray<ShipBonus>();
			Vector2 zero = Vector2.Zero;
			Form form = new Form(new Marker(ref zero, ref {21684}.c_selectMenuItem_small).SetHeight(180f), new Rectangle(99999, 99999, 0, 0), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Form {13204} = this.{21610}(Local.ship_stats_2);
			form.AddChild({13204});
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_10, 0f);
			textBlockBuilder.WriteLine(Local.PortEquipUnitsShipWindow_14(Global.Player.UsedShipPlayer.Crew.SummaryCapacity(Global.Player)), Color.Gray);
			foreach (ShipBonus shipBonus in array)
			{
				ShipBonus shipBonus2 = new ShipBonus(shipBonus.Type, MathF.Round(shipBonus.Value, (shipBonus.Value <= 10f) ? 1 : 0));
				if (shipBonus2.Type != ShipBonusEffect.BSpecialUnitEndConflicts)
				{
					textBlockBuilder.WriteLines(shipBonus2.ToString(), Color.Gray, textBlockBuilder.defaultFont, base.Pos.WH.X - 50f, new float?(0f));
				}
			}
			if (Global.Player.UsedShip.Crew.HasConflicts)
			{
				textBlockBuilder.WriteLine(Local.PortEquipUnitsShipWindow_conflicts, Color.Orange);
			}
			int num = Global.Player.UsedShip.Crew.Special.Sum((SpecialUnitInstance {21634}) => {21634}.PayPerHour(Global.Player.UsedShipPlayer.CraftFrom.Rank));
			int num2 = Session.Account.CaptainSkills[PDynamicAccountBonus.PReduceSalaryOfSpecialUnits];
			num = (int)((float)num * (1f - (float)num2 / 100f));
			if (num > 0)
			{
				TextBlockBuilder textBlockBuilder2 = textBlockBuilder;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 3);
				defaultInterpolatedStringHandler.AppendFormatted(Local.special_crew_salary.TrimEnd());
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(num);
				defaultInterpolatedStringHandler.AppendFormatted((num2 > 0) ? Local.consider_bonus2 : string.Empty);
				textBlockBuilder2.WriteLine(defaultInterpolatedStringHandler.ToStringAndClear(), Color.Gray);
			}
			form.AddChild(textBlockBuilder.Create(new Vector2(4f, 43f)));
			textBlockBuilder.WriteLine(Local.PortEquipUnitsShipWindow_15(Global.Player.UsedShip.ExtraCrewLimit), Color.SkyBlue * 0.5f, Fonts.Philosopher_14);
			form.AllowMouseInput = false;
			return form;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0009900C File Offset: 0x0009720C
		private Form {21610}(string {21611})
		{
			Form form = new Form(new Vector2(1f, 5f), new Rectangle(615, 204, 475, 39), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			form.FirstOpacity = 0.5f;
			form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, Color.White, {21611}, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.Center, 0f);
			return form;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0009907C File Offset: 0x0009727C
		private void {21612}(UnitInfo {21613}, int {21614}, int {21615})
		{
			if (Global.Player.UsedShipPlayer.Crew.GetCount({21613}) == 0 && (Session.Account.UnitsAtStorage.GetCount((int){21613}.ID) == 0 || {21615} == 0))
			{
				return;
			}
			if ({21613}.Type == UnitType.Special)
			{
				throw new NotSupportedException();
			}
			new {21884}(Local.crew, delegate({21883} {21645}, int {21646})
			{
				bool flag = Session.Account.EducationQuest.HasFlag(EducationOnboarding.GetCrewToNewShip) && Session.Account.EducationQuest.HasFlag(EducationOnboarding.BuildNextShip);
				if ({21645} == {21883}.Ship)
				{
					Session.Account.UnitsAtStorage.AddOrRemove((int){21613}.ID, -{21646});
					Global.Player.UsedShipPlayer.Crew.Add({21613}, {21646}, false);
					if (Global.Player.UsedShipPlayer.CraftFrom.ID != 2)
					{
						EducationHelper.MakeFlag(EducationOnboarding.GetCrewToNewShip, true);
					}
				}
				if ({21645} == {21883}.Port)
				{
					Global.Player.UsedShipPlayer.Crew.Remove({21613}, {21646});
					Session.Account.UnitsAtStorage.AddOrRemove((int){21613}.ID, {21646});
				}
				if (flag)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_UnitsChangesAndRatio, true);
				}
				this.UpdateBlocks(false);
			}, Session.Account.UnitsAtStorage.GetCount((int){21613}.ID), Global.Player.UsedShipPlayer.Crew.GetCount({21613}), int.MaxValue, {21884}.NameHold.Units, {21615}, 0);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00099140 File Offset: 0x00097340
		protected override IEnumerable<UiControl> CreateElementsToSelection(object {21616})
		{
			{21596}.<CreateElementsToSelection>d__20 <CreateElementsToSelection>d__ = new {21596}.<CreateElementsToSelection>d__20(-2);
			<CreateElementsToSelection>d__.<>4__this = this;
			<CreateElementsToSelection>d__.<>3__parameter = {21616};
			return <CreateElementsToSelection>d__;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00099158 File Offset: 0x00097358
		private Form {21617}(UnitInfo {21618}, int {21619})
		{
			Func<SpecialUnitInstance, bool> <>9__3;
			string[] array = (from {21638} in Session.Account.Shipyard.List.Where(delegate(PlayerShipDynamicInfo {21647})
			{
				if ({21647} != Global.Player.UsedShipPlayer)
				{
					IEnumerable<SpecialUnitInstance> special = {21647}.Crew.Special;
					Func<SpecialUnitInstance, bool> predicate;
					if ((predicate = <>9__3) == null)
					{
						predicate = (<>9__3 = ((SpecialUnitInstance {21648}) => (short){21648}.ID == {21618}.ID));
					}
					return special.Any(predicate);
				}
				return false;
			})
			select {21638}.ShipNameVisible).ToArray<string>();
			bool flag = {21619} > 0 || array.Length != 0;
			Color {13338} = ({21619} == 0 && array.Length != 0) ? Color.Gray : new Color(255, 220, 186);
			Form form = new Form(Vector2.Zero, {21684}.c_itemNewUnit, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.Pos = form.Pos.SetWidth(form.Pos.WH.X * 0.58f);
			form.Pos = form.Pos.SetHeight(form.Pos.WH.Y * 1.4f);
			form.AddChild(new Form(form.Pos, ({21619} > 0) ? {21684}.c_itemNewUnit_orange : {21684}.c_itemNewUnit, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Image(new Marker(6f, 6f, 77f, 77f), {21618}.Icon, {21618}.Icon.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Label(90f, 28f, Fonts.Philosopher_14, {13338}, flag ? (({21619} == 1) ? {21618}.Name : ("(" + {21619}.ToString() + ") " + {21618}.Name)) : "?", PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new CheckboxControl(new Vector2(73f, 10f), new Rectangle(62, 97, 21, 21), new Rectangle(40, 97, 21, 21), Global.Settings.FavoriteSpecialUnits.Contains((byte){21618}.ID), PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExCheckEvent(delegate(CheckboxCheckedEventArgs {21649})
			{
				byte b;
				if ({21649}.NewValue)
				{
					Tlist<byte> favoriteSpecialUnits = Global.Settings.FavoriteSpecialUnits;
					b = (byte){21618}.ID;
					favoriteSpecialUnits.AddIfNotContains(b);
					return;
				}
				Tlist<byte> favoriteSpecialUnits2 = Global.Settings.FavoriteSpecialUnits;
				b = (byte){21618}.ID;
				favoriteSpecialUnits2.Remove(b);
			}));
			if (!flag)
			{
				form.Opacity = 0.5f;
			}
			else
			{
				CustomSpriteFont arial_ = Fonts.Arial_9;
				form.AddChild(new Label(90f, 47f, arial_, Color.Wheat * 0.5f, {21618}.GetBonusDescriptionShort(arial_, 180), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				if ({21619} > 0)
				{
					Action<object> <>9__7;
					LabelButton deleteBt = new LabelButton(new Vector2(90f, 63f), Local.deleteSpecialUnit, Fonts.Arial_10, Color.Gray, Color.Gold, delegate(ClickUiEventArgs {21650})
					{
						ValueTuple<int, SpecialUnitInstance[]> valueTuple = Session.Account.SpecialUnitsAtStorage.Select((int){21618}.ID);
						int item = valueTuple.Item1;
						SpecialUnitInstance[] item2 = valueTuple.Item2;
						Tlist<{17473}.Item> tlist = new Tlist<{17473}.Item>();
						foreach (SpecialUnitInstance specialUnitInstance in item2)
						{
							Tlist<{17473}.Item> tlist2 = tlist;
							{17473}.Item item3 = new {17473}.Item(specialUnitInstance, {21618}.Name + " - " + StringHelper.TimeDHM((double)(specialUnitInstance.LeftContractTimeHours * 3600f), true), true, default(ImageDecription), null, null);
							tlist2.Add(item3);
						}
						if (item > 0)
						{
							Tlist<{17473}.Item> tlist3 = tlist;
							object item4 = {21618};
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
							defaultInterpolatedStringHandler.AppendFormatted<int>(item);
							defaultInterpolatedStringHandler.AppendLiteral("x ");
							defaultInterpolatedStringHandler.AppendFormatted({21618}.Name);
							defaultInterpolatedStringHandler.AppendLiteral(" - ");
							defaultInterpolatedStringHandler.AppendFormatted(StringHelper.TimeDHM(129600.0, true));
							{17473}.Item item3 = new {17473}.Item(item4, defaultInterpolatedStringHandler.ToStringAndClear(), true, default(ImageDecription), null, null);
							tlist3.Add(item3);
						}
						Action<object> {17476};
						if (({17476} = <>9__7) == null)
						{
							{17476} = (<>9__7 = delegate(object {21651})
							{
								new {17312}(Local.deleteSpecialUnit + " " + {21618}.Name + "?", delegate()
								{
									SpecialUnitInstance specialUnitInstance2 = {21651} as SpecialUnitInstance;
									if (specialUnitInstance2 != null)
									{
										Session.Account.SpecialUnitsAtStorage.Remove(specialUnitInstance2);
									}
									else
									{
										Session.Account.SpecialUnitsAtStorage.Remove({21618}, false);
									}
									this.ReopenSelectionForm();
								}, delegate()
								{
								});
							});
						}
						new {17473}({17476}, tlist.ToArray());
					}).ExToolTip(new ToolTip(new ToolTipState("", Local.deleteSpecialUnitTt, Array.Empty<ToolTipCharacteristics>())));
					form.AddChild(deleteBt);
					form.UpdateComplete += delegate(UiControl {21654})
					{
						deleteBt.IsVisible = ({21654}.InputMode > MouseInputMode.NoFocus);
					};
					form.ToolTip = new ToolTip({21618}.ToolTip());
					Func<SpecialUnitInstance, bool> <>9__10;
					Action<object> <>9__11;
					form.EvClickEmptiness += delegate(ClickUiEventArgs {21655})
					{
						if (deleteBt.InputMode != MouseInputMode.NoFocus)
						{
							return;
						}
						IEnumerable<SpecialUnitInstance> special = Global.Player.UsedShipPlayer.Crew.Special;
						Func<SpecialUnitInstance, bool> predicate;
						if ((predicate = <>9__10) == null)
						{
							predicate = (<>9__10 = ((SpecialUnitInstance {21652}) => (short){21652}.ID == {21618}.ID));
						}
						if (special.Any(predicate))
						{
							{19994}.Me({19988}.Info, Local.already_aboard, Array.Empty<object>());
							return;
						}
						if (Global.Player.UsedShip.Crew.MaxSpecialCrew(Session.Account) + (({21618}.Bonus.Type == ShipBonusEffect.BSpecialUnitEndConflicts) ? 1 : 0) > Global.Player.UsedShipPlayer.Crew.Special.Size)
						{
							ValueTuple<int, SpecialUnitInstance[]> valueTuple = Session.Account.SpecialUnitsAtStorage.Select((int){21618}.ID);
							int item = valueTuple.Item1;
							SpecialUnitInstance[] item2 = valueTuple.Item2;
							if (item + item2.Length > 1)
							{
								Tlist<{17473}.Item> tlist = new Tlist<{17473}.Item>();
								foreach (SpecialUnitInstance specialUnitInstance in item2)
								{
									Tlist<{17473}.Item> tlist2 = tlist;
									{17473}.Item item3 = new {17473}.Item(specialUnitInstance, {21618}.Name + " - " + StringHelper.TimeDHM((double)(specialUnitInstance.LeftContractTimeHours * 3600f), true), true, default(ImageDecription), null, null);
									tlist2.Add(item3);
								}
								if (item > 0)
								{
									Tlist<{17473}.Item> tlist3 = tlist;
									object item4 = {21618};
									DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
									defaultInterpolatedStringHandler.AppendFormatted<int>(item);
									defaultInterpolatedStringHandler.AppendLiteral("x ");
									defaultInterpolatedStringHandler.AppendFormatted({21618}.Name);
									defaultInterpolatedStringHandler.AppendLiteral(" - ");
									defaultInterpolatedStringHandler.AppendFormatted(StringHelper.TimeDHM(129600.0, true));
									{17473}.Item item3 = new {17473}.Item(item4, defaultInterpolatedStringHandler.ToStringAndClear(), true, default(ImageDecription), null, null);
									tlist3.Add(item3);
								}
								Action<object> {17476};
								if (({17476} = <>9__11) == null)
								{
									{17476} = (<>9__11 = delegate(object {21653})
									{
										SpecialUnitInstance specialUnitInstance3 = {21653} as SpecialUnitInstance;
										if (specialUnitInstance3 != null)
										{
											Global.Player.UsedShip.Crew.Add(specialUnitInstance3);
											Session.Account.SpecialUnitsAtStorage.Remove(specialUnitInstance3);
										}
										else
										{
											Global.Player.UsedShip.Crew.Add(new SpecialUnitInstance((int){21618}.ID));
											Session.Account.SpecialUnitsAtStorage.Remove({21618}, false);
										}
										this.ReopenSelectionForm();
										this.UpdateBlocks(false);
									});
								}
								new {17473}({17476}, tlist.ToArray());
								return;
							}
							if (item2.Length != 0)
							{
								SpecialUnitInstance specialUnitInstance2 = item2[0];
								Global.Player.UsedShip.Crew.Add(specialUnitInstance2);
								Session.Account.SpecialUnitsAtStorage.Remove(specialUnitInstance2);
							}
							else
							{
								Global.Player.UsedShip.Crew.Add(new SpecialUnitInstance((int){21618}.ID));
								Session.Account.SpecialUnitsAtStorage.Remove({21618}, false);
							}
							this.ReopenSelectionForm();
							this.UpdateBlocks(false);
						}
					};
				}
				else
				{
					ToolTipState toolTipState = {21618}.ToolTip();
					toolTipState.AppendText(" ", Color.White, true, false);
					toolTipState.AppendText(Local.specialUnitOnOtherShips, Color.White, true, false);
					int num = 10;
					foreach (string {12795} in array)
					{
						toolTipState.AppendText({12795}, Color.White * 0.75f, true, false);
						if (num-- == 0)
						{
							toolTipState.AppendText("...", Color.White * 0.75f, true, false);
							break;
						}
					}
					form.ToolTip = new ToolTip(toolTipState);
				}
			}
			return form;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0009954F File Offset: 0x0009774F
		public void ReceiveResponse(OnGetCrewPortLimits {21620})
		{
			this.{21631} = {21620}.result;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00099580 File Offset: 0x00097780
		[CompilerGenerated]
		private void {21621}(ClickUiEventArgs {21622})
		{
			Session.Account.UnitsAtStorage.Add(Global.Player.UsedShipPlayer.Crew.Raw);
			foreach (SpecialUnitInstance {2170} in ((IEnumerable<SpecialUnitInstance>)Global.Player.UsedShipPlayer.Crew.Special))
			{
				Session.Account.SpecialUnitsAtStorage.Add({2170});
			}
			Global.Player.UsedShipPlayer.Crew.RemoveAll();
			base.UpdateBlocks(false);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00099624 File Offset: 0x00097824
		[CompilerGenerated]
		private void {21623}(ClickUiEventArgs {21624})
		{
			base.OpenSelectionForm(0);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00099624 File Offset: 0x00097824
		[CompilerGenerated]
		private void {21625}(ClickUiEventArgs {21626})
		{
			base.OpenSelectionForm(0);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00099634 File Offset: 0x00097834
		[CompilerGenerated]
		private void {21627}(ClickUiEventArgs {21628})
		{
			foreach (PlayerShipDynamicInfo playerShipDynamicInfo in Session.Account.Shipyard.List)
			{
				foreach (SpecialUnitInstance specialUnitInstance in ((IEnumerable<SpecialUnitInstance>)playerShipDynamicInfo.Crew.Special.Clone()))
				{
					Session.Account.SpecialUnitsAtStorage.Add(specialUnitInstance);
					playerShipDynamicInfo.Crew.Remove(specialUnitInstance.GetInfo, 1);
				}
			}
			base.ReopenSelectionForm();
			base.UpdateBlocks(false);
		}

		// Token: 0x0400106A RID: 4202
		public static {21596} CurrentInstance;

		// Token: 0x0400106B RID: 4203
		private static readonly Marker iconPosition = new Marker(8f, 6f, 55f, 55f);

		// Token: 0x0400106C RID: 4204
		private StackForm {21629};

		// Token: 0x0400106D RID: 4205
		private {18187} {21630};

		// Token: 0x0400106E RID: 4206
		private GSI {21631};
	}
}
