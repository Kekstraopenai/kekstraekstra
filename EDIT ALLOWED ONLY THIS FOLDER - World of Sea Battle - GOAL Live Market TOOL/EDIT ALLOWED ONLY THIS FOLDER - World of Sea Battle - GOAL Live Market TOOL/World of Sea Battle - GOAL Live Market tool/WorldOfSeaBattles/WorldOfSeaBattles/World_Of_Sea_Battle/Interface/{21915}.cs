using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using Common.Data;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
	// Token: 0x02000368 RID: 872
	internal sealed class {21915} : {21684}
	{
		// Token: 0x060012E7 RID: 4839 RVA: 0x000A026C File Offset: 0x0009E46C
		public {21915}(int {21917}) : base(Local.savedEquip_slot({21917} + 1), "", "")
		{
			{21915} <>4__this = this;
			this.{21930} = {21917};
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.VerticalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			SavedShipEquipment existingSave = Global.Settings.GetEquipmentSave({21917}, Global.Player.UsedShipPlayer);
			StackForm stackForm2 = new StackForm(default(Vector2), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Button button = new Button(default(Vector2), AtlasPortGui.buttonGray, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				PosWidth = 130f
			}.SetText(Local.savedEquip_applynow, Fonts.Philosopher_14, Color.White * 0.9f, false).ExClick(delegate(ClickUiEventArgs {21935})
			{
				existingSave.Apply(true, false);
				{17745} currentInstance = {17745}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.ExternalUpdate();
			});
			stackForm2.AddItem(new UiControl[]
			{
				button
			});
			if (!Session.Game.IsInPortOrIsleWithStorage)
			{
				button.AllowMouseInput = false;
				button.Opacity = 0.5f;
			}
			stackForm2.AddSpace(10f);
			Func<SavedShipEquipment, bool> <>9__5;
			Action <>9__3;
			stackForm2.AddItem(new UiControl[]
			{
				new Button(default(Vector2), AtlasPortGui.buttonGray, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					PosWidth = 130f
				}.SetText(Local.savedEquip_resave, Fonts.Philosopher_14, Color.White * 0.9f, false).ExClick(delegate(ClickUiEventArgs {21931})
				{
					string {17371} = Local.savedEquip_resave + "?";
					Action {17372};
					if (({17372} = <>9__3) == null)
					{
						{17372} = (<>9__3 = delegate()
						{
							Tlist<SavedShipEquipment> savedEquipment = Global.Settings.SavedEquipment;
							Func<SavedShipEquipment, bool> item;
							if ((item = <>9__5) == null)
							{
								item = (<>9__5 = ((SavedShipEquipment {21932}) => {21932}.ShipInfoId == Global.Player.CraftFrom.ID && (int){21932}.SlotIndex == {21917}));
							}
							savedEquipment.Remove(item);
							<>4__this.BlockAndClose();
							new {21915}({21917});
						});
					}
					new {17312}({17371}, {17372}, delegate()
					{
					});
				})
			});
			stackForm2.AddSpace(10f);
			Func<SavedShipEquipment, bool> <>9__8;
			Action <>9__6;
			stackForm2.AddItem(new UiControl[]
			{
				new Button(default(Vector2), AtlasPortGui.buttonGray, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					PosWidth = 130f
				}.SetText(Local.PortUpgradeShipWindow_10, Fonts.Philosopher_14, Color.White * 0.9f, false).ExClick(delegate(ClickUiEventArgs {21933})
				{
					string {17371} = Local.PortUpgradeShipWindow_10 + "?";
					Action {17372};
					if (({17372} = <>9__6) == null)
					{
						{17372} = (<>9__6 = delegate()
						{
							Tlist<SavedShipEquipment> savedEquipment = Global.Settings.SavedEquipment;
							Func<SavedShipEquipment, bool> item;
							if ((item = <>9__8) == null)
							{
								item = (<>9__8 = ((SavedShipEquipment {21934}) => {21934}.ShipInfoId == Global.Player.CraftFrom.ID && (int){21934}.SlotIndex == {21917}));
							}
							savedEquipment.Remove(item);
							<>4__this.BlockAndClose();
						});
					}
					new {17312}({17371}, {17372}, delegate()
					{
					});
				})
			});
			stackForm.AddItem(new UiControl[]
			{
				stackForm2
			});
			stackForm.AllowMouseInput = (existingSave != null);
			stackForm.Opacity = (stackForm.AllowMouseInput ? 1f : 0.3f);
			base.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.LeftUp, 45f);
			base.UpdateBlocks(true);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000A04B0 File Offset: 0x0009E6B0
		protected override ScrollBarControl CreateMainFormScroll()
		{
			return new ScrollBarControl(new Marker(base.PosWidth - 43f, 260f - base.PosHeight / 2f + 7f, 21f, base.Pos.Height - 22f), Rectangle.Empty, Rectangle.Empty, new Rectangle(548, 261, 20, 37), AtlasPortGui.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x000A0529 File Offset: 0x0009E729
		protected override IEnumerable<UiControl> CreateDesignComponents()
		{
			{21915}.<CreateDesignComponents>d__3 <CreateDesignComponents>d__ = new {21915}.<CreateDesignComponents>d__3(-2);
			<CreateDesignComponents>d__.<>4__this = this;
			return <CreateDesignComponents>d__;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x000A053C File Offset: 0x0009E73C
		public static bool UiAndProblemsHelper(StackForm {21918}, SavedShipEquipment {21919}, bool {21920} = true)
		{
			{21915}.<>c__DisplayClass4_0 CS$<>8__locals1;
			CS$<>8__locals1.tooltips = {21920};
			CS$<>8__locals1.hasProblems = false;
			Color wheat = Color.Wheat;
			CS$<>8__locals1.itemColor = Color.Gray;
			CS$<>8__locals1.itemError = Color.Orange;
			{21918}.AddItem(new UiControl[]
			{
				new Label(default(Vector2), Fonts.Philosopher_14, wheat, Local.hold, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			if ({21919}.SavedHold == null)
			{
				{21918}.AddItem(new UiControl[]
				{
					new Label(default(Vector2), Fonts.Arial_10, CS$<>8__locals1.itemColor, Local.savedEquip_noAction, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}
			else
			{
				foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){21919}.SavedHold.ResourceInfo))
				{
					PortEnteringType nearPortType = Global.Player.NearPortType;
					bool flag = nearPortType == PortEnteringType.Port || nearPortType == PortEnteringType.PersonalIsle;
					UiControl[] array = new UiControl[1];
					int num = 0;
					bool {21925} = flag && gsilocalEnumerablePair.Count > Session.Account.NearPortStorage[(int)gsilocalEnumerablePair.Info.ID] + Global.Player.ResourcesOfHold[(int)gsilocalEnumerablePair.Info.ID];
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
					defaultInterpolatedStringHandler.AppendFormatted<int>(gsilocalEnumerablePair.Count);
					defaultInterpolatedStringHandler.AppendLiteral("x ");
					defaultInterpolatedStringHandler.AppendFormatted(gsilocalEnumerablePair.Info.Name);
					array[num] = {21915}.<UiAndProblemsHelper>g__Item|4_0({21925}, defaultInterpolatedStringHandler.ToStringAndClear(), gsilocalEnumerablePair.Info.IconTexture, gsilocalEnumerablePair.Info, ref CS$<>8__locals1);
					{21918}.AddItem(array);
				}
				if ({21919}.SavedHold.IsEmpty)
				{
					{21918}.AddItem(new UiControl[]
					{
						new Label(default(Vector2), Fonts.Arial_10, CS$<>8__locals1.itemColor, "-", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
				}
			}
			{21918}.AddItem(new UiControl[]
			{
				new Label(default(Vector2), Fonts.Philosopher_14, wheat, Local.ammo, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			if ({21919}.SavedAmmo == null)
			{
				{21918}.AddItem(new UiControl[]
				{
					new Label(default(Vector2), Fonts.Arial_10, CS$<>8__locals1.itemColor, Local.savedEquip_noAction, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}
			else
			{
				foreach (GSILocalEnumerablePair<CannonBallInfo> gsilocalEnumerablePair2 in ((IEnumerable<GSILocalEnumerablePair<CannonBallInfo>>){21919}.SavedAmmo.Ammo.CannonBallInfo))
				{
					UiControl[] array2 = new UiControl[1];
					int num2 = 0;
					bool {21925}2 = gsilocalEnumerablePair2.Count > Session.Account.CBallsAtStorage[(int)gsilocalEnumerablePair2.Info.ID] + Global.Player.UsedShipPlayer.BallsOfHold[(int)gsilocalEnumerablePair2.Info.ID];
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(2, 2);
					defaultInterpolatedStringHandler2.AppendFormatted<int>(gsilocalEnumerablePair2.Count);
					defaultInterpolatedStringHandler2.AppendLiteral("x ");
					defaultInterpolatedStringHandler2.AppendFormatted(gsilocalEnumerablePair2.Info.Name);
					array2[num2] = {21915}.<UiAndProblemsHelper>g__Item|4_0({21925}2, defaultInterpolatedStringHandler2.ToStringAndClear(), gsilocalEnumerablePair2.Info.IconTexture, gsilocalEnumerablePair2.Info, ref CS$<>8__locals1);
					{21918}.AddItem(array2);
				}
				if ({21919}.SavedAmmo.Ammo.IsEmpty)
				{
					{21918}.AddItem(new UiControl[]
					{
						new Label(default(Vector2), Fonts.Arial_10, CS$<>8__locals1.itemColor, "-", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
				}
				foreach (GSILocalEnumerablePair<PowderKegInfo> gsilocalEnumerablePair3 in ((IEnumerable<GSILocalEnumerablePair<PowderKegInfo>>){21919}.SavedAmmo.PowderKegs.PowderKegInfo))
				{
					UiControl[] array3 = new UiControl[1];
					int num3 = 0;
					bool {21925}3 = gsilocalEnumerablePair3.Count > Session.Account.PowderKegsAtStorage[(int)gsilocalEnumerablePair3.Info.ID] + Global.Player.UsedShipPlayer.PowderKegsOfHold[(int)gsilocalEnumerablePair3.Info.ID];
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(2, 2);
					defaultInterpolatedStringHandler3.AppendFormatted<int>(gsilocalEnumerablePair3.Count);
					defaultInterpolatedStringHandler3.AppendLiteral("x ");
					defaultInterpolatedStringHandler3.AppendFormatted(gsilocalEnumerablePair3.Info.Name);
					array3[num3] = {21915}.<UiAndProblemsHelper>g__Item|4_0({21925}3, defaultInterpolatedStringHandler3.ToStringAndClear(), gsilocalEnumerablePair3.Info.IconTexture, gsilocalEnumerablePair3.Info, ref CS$<>8__locals1);
					{21918}.AddItem(array3);
				}
				if ({21919}.SavedAmmo.PowderKegs.IsEmpty)
				{
					{21918}.AddItem(new UiControl[]
					{
						new Label(default(Vector2), Fonts.Arial_10, CS$<>8__locals1.itemColor, "-", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
				}
			}
			{21918}.AddItem(new UiControl[]
			{
				new Label(default(Vector2), Fonts.Philosopher_14, wheat, Local.crew, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			if ({21919}.SavedCrew == null)
			{
				{21918}.AddItem(new UiControl[]
				{
					new Label(default(Vector2), Fonts.Arial_10, CS$<>8__locals1.itemColor, Local.savedEquip_noAction, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}
			else
			{
				foreach (GSILocalPair gsilocalPair in ((IEnumerable<GSILocalPair>){21919}.SavedCrew))
				{
					UiControl[] array4 = new UiControl[1];
					int num4 = 0;
					bool {21925}4 = gsilocalPair.Count > Session.Account.UnitsAtStorage[gsilocalPair.ID] + Global.Player.UsedShipPlayer.Crew.Raw[gsilocalPair.ID];
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(2, 2);
					defaultInterpolatedStringHandler4.AppendFormatted<int>(gsilocalPair.Count);
					defaultInterpolatedStringHandler4.AppendLiteral("x ");
					defaultInterpolatedStringHandler4.AppendFormatted(Gameplay.UnitsInfo[gsilocalPair.ID].Name);
					array4[num4] = {21915}.<UiAndProblemsHelper>g__Item|4_0({21925}4, defaultInterpolatedStringHandler4.ToStringAndClear(), Gameplay.UnitsInfo[gsilocalPair.ID].Icon, Gameplay.UnitsInfo[gsilocalPair.ID], ref CS$<>8__locals1);
					{21918}.AddItem(array4);
				}
				using (IEnumerator<GSILocalPair> enumerator4 = ((IEnumerable<GSILocalPair>){21919}.SavedSpecialCrew).GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						GSILocalPair item = enumerator4.Current;
						{21918}.AddItem(new UiControl[]
						{
							{21915}.<UiAndProblemsHelper>g__Item|4_0(item.Count > Session.Account.SpecialUnitsAtStorage.Count(item.ID) + Global.Player.UsedShipPlayer.Crew.Special.Count((SpecialUnitInstance {21938}) => (int){21938}.ID == item.ID), Gameplay.UnitsInfo[item.ID].Name ?? "", Gameplay.UnitsInfo[item.ID].Icon, Gameplay.UnitsInfo[item.ID], ref CS$<>8__locals1)
						});
					}
				}
				if ({21919}.SavedCrew.IsEmpty && {21919}.SavedSpecialCrew.IsEmpty)
				{
					{21918}.AddItem(new UiControl[]
					{
						new Label(default(Vector2), Fonts.Arial_10, CS$<>8__locals1.itemColor, "-", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
				}
			}
			{21918}.AddItem(new UiControl[]
			{
				new Label(default(Vector2), Fonts.Philosopher_14, wheat, Local.places_powerupitems, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			if ({21919}.SavedPowerups == null)
			{
				{21918}.AddItem(new UiControl[]
				{
					new Label(default(Vector2), Fonts.Arial_10, CS$<>8__locals1.itemColor, Local.savedEquip_noAction, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}
			else
			{
				foreach (GSILocalEnumerablePair<PowerupItemInfo> gsilocalEnumerablePair4 in ((IEnumerable<GSILocalEnumerablePair<PowerupItemInfo>>){21919}.SavedPowerups.PowerupItemInfo))
				{
					{21918}.AddItem(new UiControl[]
					{
						{21915}.<UiAndProblemsHelper>g__Item|4_0(false, gsilocalEnumerablePair4.Info.Name ?? "", gsilocalEnumerablePair4.Info.Icon, gsilocalEnumerablePair4.Info, ref CS$<>8__locals1)
					});
				}
				if ({21919}.SavedPowerups.IsEmpty)
				{
					{21918}.AddItem(new UiControl[]
					{
						new Label(default(Vector2), Fonts.Arial_10, CS$<>8__locals1.itemColor, "-", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
				}
			}
			{21918}.AddItem(new UiControl[]
			{
				new Label(default(Vector2), Fonts.Philosopher_14, wheat, Local.upgrades, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			if ({21919}.SavedUpgradesIds == null)
			{
				{21918}.AddItem(new UiControl[]
				{
					new Label(default(Vector2), Fonts.Arial_10, CS$<>8__locals1.itemColor, Local.savedEquip_noAction, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}
			else
			{
				foreach (GSILocalPair gsilocalPair2 in ((IEnumerable<GSILocalPair>){21919}.SavedUpgradesIds))
				{
					{21918}.AddItem(new UiControl[]
					{
						{21915}.<UiAndProblemsHelper>g__Item|4_0(false, Gameplay.ShipUpgradesInfo[gsilocalPair2.ID].Name ?? "", Gameplay.ShipUpgradesInfo[gsilocalPair2.ID].IconTexture, Gameplay.ShipUpgradesInfo[gsilocalPair2.ID], ref CS$<>8__locals1)
					});
				}
				if ({21919}.SavedUpgradesIds.IsEmpty)
				{
					{21918}.AddItem(new UiControl[]
					{
						new Label(default(Vector2), Fonts.Arial_10, CS$<>8__locals1.itemColor, "-", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
				}
			}
			{21918}.AddItem(new UiControl[]
			{
				new Label(default(Vector2), Fonts.Philosopher_14, wheat, Local.weapons_d, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			if ({21919}.SavedCannons == null)
			{
				{21918}.AddItem(new UiControl[]
				{
					new Label(default(Vector2), Fonts.Arial_10, CS$<>8__locals1.itemColor, Local.savedEquip_noAction, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}
			else
			{
				ScoreDictionary<int> scoreDictionary = new ScoreDictionary<int>();
				foreach (GSILocalPair gsilocalPair3 in ((IEnumerable<GSILocalPair>){21919}.SavedCannons))
				{
					scoreDictionary.AddScore(gsilocalPair3.Count, 1f);
				}
				using (Dictionary<int, float>.Enumerator enumerator6 = scoreDictionary.BaseDictionary.GetEnumerator())
				{
					while (enumerator6.MoveNext())
					{
						KeyValuePair<int, float> item = enumerator6.Current;
						UiControl[] array5 = new UiControl[1];
						int num5 = 0;
						bool {21925}5 = item.Value > (float)(Session.Account.CannonsAtStorage[item.Key] + Global.Player.UsedShipPlayer.Cannons.Items.Count((CannonCommon {21939}) => (int){21939}.GameInfo.ID == item.Key));
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler5 = new DefaultInterpolatedStringHandler(2, 2);
						defaultInterpolatedStringHandler5.AppendFormatted<float>(item.Value);
						defaultInterpolatedStringHandler5.AppendLiteral("x ");
						defaultInterpolatedStringHandler5.AppendFormatted(Gameplay.CannonsGameInfo[item.Key].Name);
						array5[num5] = {21915}.<UiAndProblemsHelper>g__Item|4_0({21925}5, defaultInterpolatedStringHandler5.ToStringAndClear(), Gameplay.CannonsGameInfo[item.Key].IconTexture, Gameplay.CannonsGameInfo[item.Key], ref CS$<>8__locals1);
						{21918}.AddItem(array5);
					}
				}
				if ({21919}.SavedCannons.IsEmpty)
				{
					{21918}.AddItem(new UiControl[]
					{
						new Label(default(Vector2), Fonts.Arial_10, CS$<>8__locals1.itemColor, "-", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
				}
			}
			return CS$<>8__locals1.hasProblems;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x000A11E4 File Offset: 0x0009F3E4
		[CompilerGenerated]
		private bool {21921}(SavedShipEquipment {21922})
		{
			return (int){21922}.SlotIndex == this.{21930} && {21922}.Active && {21922}.ShipInfoId == Global.Player.CraftFrom.ID;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x000A1218 File Offset: 0x0009F418
		[CompilerGenerated]
		private void {21923}(CheckboxCheckedEventArgs {21924})
		{
			foreach (SavedShipEquipment savedShipEquipment in ((IEnumerable<SavedShipEquipment>)Global.Settings.SavedEquipment))
			{
				if (savedShipEquipment.ShipInfoId == Global.Player.CraftFrom.ID)
				{
					savedShipEquipment.Active = ((int)savedShipEquipment.SlotIndex == this.{21930} && {21924}.NewValue);
				}
			}
			if ({21924}.NewValue)
			{
				Global.Settings.AutomoveToStorage = false;
			}
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000A12AC File Offset: 0x0009F4AC
		[CompilerGenerated]
		internal static StackForm <UiAndProblemsHelper>g__Item|4_0(bool {21925}, string {21926}, Texture2D {21927}, object {21928}, ref {21915}.<>c__DisplayClass4_0 {21929})
		{
			{21929}.hasProblems = ({21929}.hasProblems || {21925});
			StackForm stackForm = new StackForm(default(Vector2), UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				new Image(new Marker(0f, 0f, 20f, 20f), {21927}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			stackForm.AddSpace(5f);
			stackForm.AddItem(new UiControl[]
			{
				new Label(default(Vector2), Fonts.Arial_10, {21925} ? {21929}.itemError : {21929}.itemColor, {21926}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			if ({21929}.tooltips)
			{
				CannonGameInfo cannonGameInfo = {21928} as CannonGameInfo;
				if (cannonGameInfo != null)
				{
					{20431}.CannonToolTIp(cannonGameInfo, stackForm, false, false, null);
				}
				else
				{
					IStorageAsset storageAsset = {21928} as IStorageAsset;
					if (storageAsset != null)
					{
						{20431}.Set(stackForm, storageAsset, 0, null);
					}
					else
					{
						PowerupItemInfo powerupItemInfo = {21928} as PowerupItemInfo;
						if (powerupItemInfo != null)
						{
							stackForm.ToolTipState = powerupItemInfo.ToolTip(false, Array.Empty<ToolTipCharacteristics>());
						}
						else
						{
							ShipUpgradeInfo shipUpgradeInfo = {21928} as ShipUpgradeInfo;
							if (shipUpgradeInfo != null)
							{
								stackForm.ToolTipState = {22195}.MakeToolTip(shipUpgradeInfo);
							}
						}
					}
				}
			}
			return stackForm;
		}

		// Token: 0x04001143 RID: 4419
		private int {21930};
	}
}
