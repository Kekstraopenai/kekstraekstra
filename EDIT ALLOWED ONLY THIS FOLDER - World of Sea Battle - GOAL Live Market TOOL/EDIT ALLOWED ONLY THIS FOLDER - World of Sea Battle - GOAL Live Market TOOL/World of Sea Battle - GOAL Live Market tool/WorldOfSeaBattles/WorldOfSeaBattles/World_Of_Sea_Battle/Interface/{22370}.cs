using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020003AB RID: 939
	public class {22370} : CustomUi
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x000ACD12 File Offset: 0x000AAF12
		public static bool IsOpened
		{
			get
			{
				return {22370}.Instance != null;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x000ACD1C File Offset: 0x000AAF1C
		private static int Height
		{
			get
			{
				return 350;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x000ACD23 File Offset: 0x000AAF23
		private bool HasFilter
		{
			get
			{
				return this.{22393}.Mode == CannonLocationMode.Default;
			}
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x000ACD34 File Offset: 0x000AAF34
		public {22370}(CannonLocationInfo {22372}) : base(true)
		{
			this.{22393} = {22372};
			CannonGameInfo cannonGameInfo;
			if ({22372}.Mode != CannonLocationMode.Mortar)
			{
				CannonCommon cannonCommon = Global.Player.UsedShip.Cannons.FindByLocation((int){22372}.SectionID);
				cannonGameInfo = ((cannonCommon != null) ? cannonCommon.GameInfo : null);
			}
			else
			{
				CannonGameInstance cannonGameInstance = Global.Player.UsedShip.Mortars[(int){22372}.SectionID];
				cannonGameInfo = ((cannonGameInstance != null) ? cannonGameInstance.Info : null);
			}
			this.{22394} = cannonGameInfo;
			Global.Game.SceneGame.IncreaseMouse();
			base.EvRemoveFromContainer += delegate()
			{
				Global.Game.SceneGame.DecreaseMouse();
			};
			Form form = new Form(base.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				DisableDepthFocusTest = false,
				AnimatedFocus = false
			};
			form.MoveToBackLevel();
			form.EvClick += delegate(ClickUiEventArgs {22396})
			{
				{22370}.TryClose();
			};
			base.AddChild(form);
			this.{22392} = Engine.GS.MouseToUI;
			if (this.{22392}.Y > (float)(Engine.GS.UIArea.Height / 2))
			{
				this.{22392}.Y = this.{22392}.Y - (float){22370}.Height;
			}
			if (this.HasFilter)
			{
				IEnumerable<CannonCategory> source = {22370}.cannonsFilter;
				Func<CannonCategory, bool> predicate;
				if ((predicate = {22370}.<>O.<0>__AllowedForShip) == null)
				{
					predicate = ({22370}.<>O.<0>__AllowedForShip = new Func<CannonCategory, bool>({22370}.AllowedForShip));
				}
				if (!source.Any(predicate))
				{
					{22370}.cannonsFilter.Add(CannonCategory.Light);
					{22370}.cannonsFilter.Add(CannonCategory.Medium);
					{22370}.cannonsFilter.Add(CannonCategory.Heavy);
				}
			}
			this.{22373}();
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x000ACECC File Offset: 0x000AB0CC
		private void {22373}()
		{
			Form form = this.{22395};
			if (form != null)
			{
				form.RemoveFromContainer();
			}
			this.{22395} = new Form(new Marker(this.{22392}.X, this.{22392}.Y, (float)({22370}.selectMenuItem.Width + AtlasPortGui.scrollBar_Up.Width), (float){22370}.Height).Border(5f), new Rectangle(2024, 630, 314, 312), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false,
				BasicColor = Color.Gray
			};
			base.AddChild(this.{22395});
			int num = 5;
			if (this.{22394} != null)
			{
				Form form2 = this.{22380}(this.{22393}, new CannonGameInstance((int)this.{22394}.ID, 0), 1, true);
				form2.Tag = this.{22394};
				this.{22395}.AddChildPos(form2, PositionAlignment.LeftUp, PositionAlignment.LeftUp, 5f, (float)num, false);
				num += 55;
			}
			if (this.HasFilter)
			{
				this.{22395}.AddChildPos(this.{22374}(), PositionAlignment.LeftUp, PositionAlignment.LeftUp, 5f, (float)num, false);
				num += 30;
			}
			Marker {14054} = new Marker(this.{22395}.Pos.XY.X + this.{22395}.Pos.WH.X - (float)AtlasPortGui.scrollBar_Down.Width, this.{22395}.Pos.XY.Y, (float)AtlasPortGui.scrollBar_Down.Width, this.{22395}.Pos.WH.Y);
			ScrollBarControl scroll = new ScrollBarControl({14054}, AtlasPortGui.scrollBar_Up, AtlasPortGui.scrollBar_Down, AtlasPortGui.scrollBar_Pointer, AtlasPortGui.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			ListItemViewControl listItemViewControl = new ListItemViewControl(this.{22395}.Pos, scroll, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			scroll.EvRemoveFromContainer += delegate()
			{
				{22370}.savedScroll = scroll.CurrentScrollFactor;
			};
			this.{22376}(listItemViewControl);
			int num2 = Session.Account.Shipyard.List.Sum(delegate(PlayerShipDynamicInfo {22397})
			{
				if ({22397} != Global.Player.UsedShipPlayer)
				{
					return {22397}.Cannons.Count;
				}
				return 0;
			});
			Form form3 = new Form(Vector2.Zero, {22370}.selectMenuItem, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form3.Opacity = 0.7f;
			form3.AddChild(new UiControl[]
			{
				new Label({22370}.selectMenuItem_name, Fonts.Arial_12, Color.White * 0.8f, Local.PortVisaulEquipmentInterface_20, PositionAlignment.LeftUp, PositionAlignment.LeftUp),
				new Label({22370}.selectMenuItem_countText, Fonts.Arial_10, Color.White * 0.6f, Local.pcs_format(num2), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			listItemViewControl.AddItem(new UiControl[]
			{
				form3
			});
			scroll.CurrentScrollFactor = {22370}.savedScroll;
			this.{22395}.AddChildPos(listItemViewControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp, 5f, (float)num, false);
			this.{22395}.AddChildPos(scroll, PositionAlignment.LeftUp, PositionAlignment.LeftUp, listItemViewControl.PosWidth - scroll.PosWidth - 5f, (float)num, false);
			this.{22395}.PosHeight = listItemViewControl.Pos.XY.Y - this.{22395}.Pos.XY.Y + listItemViewControl.PosHeight + 5f;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x000AD228 File Offset: 0x000AB428
		private StackForm {22374}()
		{
			CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
			Color white = Color.White;
			int num = 10;
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				this.{22387}(CannonCategory.Light, philosopher_, white)
			});
			stackForm.AddSpace((float)num);
			stackForm.AddItem(new UiControl[]
			{
				this.{22387}(CannonCategory.Medium, philosopher_, white)
			});
			stackForm.AddSpace((float)num);
			stackForm.AddItem(new UiControl[]
			{
				this.{22387}(CannonCategory.Heavy, philosopher_, white)
			});
			return stackForm;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x000AD2AB File Offset: 0x000AB4AB
		private static bool AllowedForShip(CannonCategory {22375})
		{
			return Global.Player.UsedShipPlayer.CraftFrom.IsCannonsAllowed({22375});
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x000AD2C4 File Offset: 0x000AB4C4
		private void {22376}(ListItemViewControl {22377})
		{
			foreach (ValueTuple<CannonGameInstance, int> valueTuple in (from {22391} in new List<ValueTuple<CannonGameInstance, int>>().Concat(Session.Account.CannonsAtStorage.CannonGameInfo.Select(delegate(GSILocalEnumerablePair<CannonGameInfo> {22398})
			{
				int {2286} = ({22398}.Info.Class == CannonClass.Mortar) ? Gameplay.DestroyMortarResource({22398}.Info) : 0;
				return new ValueTuple<CannonGameInstance, int>(new CannonGameInstance((int){22398}.Info.ID, {2286}), {22398}.Count);
			})).Concat(from {22399} in Session.Account.UsedMortarsAtStorage
			select new ValueTuple<CannonGameInstance, int>({22399}, 1))
			where {22391}.Item1 != null && ({22370}.AllowedForShip({22391}.Item1.Info.Category) || {22391}.Item1.Info.Class == CannonClass.Special) && this.{22378}({22391}.Item1.Info) && (!this.HasFilter || {22370}.cannonsFilter.Contains({22391}.Item1.Info.Category))
			select {22391}).OrderBy(delegate([TupleElementNames(new string[]
			{
				"Instance",
				"Count"
			})] ValueTuple<CannonGameInstance, int> {22400})
			{
				if ({22400}.Item1.Info.Class != CannonClass.Bombardier)
				{
					return (int){22400}.Item1.Info.Class;
				}
				return 100;
			}).ThenBy(([TupleElementNames(new string[]
			{
				"Instance",
				"Count"
			})] ValueTuple<CannonGameInstance, int> {22401}) => {22401}.Item1.Info.Category).ThenBy(delegate([TupleElementNames(new string[]
			{
				"Instance",
				"Count"
			})] ValueTuple<CannonGameInstance, int> {22402})
			{
				int? poundage = {22402}.Item1.Info.Poundage;
				if (poundage == null)
				{
					return 50 + {22402}.Item1.Info.CostAsGold.Value / 100;
				}
				return poundage.GetValueOrDefault();
			}))
			{
				CannonGameInstance item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				Form form = this.{22380}(this.{22393}, item, item2, false);
				form.Tag = item.Info;
				{22377}.AddItem(new UiControl[]
				{
					form
				});
			}
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x000AD430 File Offset: 0x000AB630
		private bool {22378}(CannonGameInfo {22379})
		{
			bool result;
			if ({22379}.Class == CannonClass.Mortar)
			{
				result = (this.{22393}.Mode == CannonLocationMode.Mortar);
			}
			else
			{
				bool flag;
				if ({22379}.Class == CannonClass.Special)
				{
					flag = (this.{22393}.Mode == CannonLocationMode.Special);
				}
				else
				{
					bool flag4;
					if ({22379}.Class == CannonClass.Bombardier)
					{
						bool flag2 = this.{22393}.Mode == CannonLocationMode.Default;
						if (flag2)
						{
							CannonLocation side = this.{22393}.Side;
							bool flag3 = side - CannonLocation.InFront <= 1;
							flag2 = flag3;
						}
						flag4 = flag2;
					}
					else
					{
						flag4 = (this.{22393}.Mode == CannonLocationMode.Default);
					}
					flag = flag4;
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x000AD4C4 File Offset: 0x000AB6C4
		private Form {22380}(CannonLocationInfo {22381}, CannonGameInstance {22382}, int {22383}, bool {22384})
		{
			CannonGameInfo info = {22382}.Info;
			string text = ({22382}.Info.Class == CannonClass.Mortar && !{22382}.IsWholeMortar) ? Local.mortar_resource({22382}.RemainReserve) : Local.pcs_format({22383});
			Form form = new Form(Vector2.Zero, {22370}.selectMenuItem, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Rectangle cannonClassIcon = CommonAtlas.GetCannonClassIcon(info.Class, info.Feature == CannonFeature.Firegun);
			form.AddChild(new Image({22370}.selectMenuItem_icon, info.IconTexture, info.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				RenderToDepthMap = false
			});
			form.AddChild(new Image(new Vector2(50f, 1f), CommonAtlas.Texture.Tex, cannonClassIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				RenderToDepthMap = false
			});
			form.AddChild(new Label({22370}.selectMenuItem_name, Fonts.Arial_12, Color.White * 0.8f, info.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				RenderToDepthMap = false
			});
			Color {13344} = {22384} ? (Color.Lerp(Color.Lime, Color.LightYellow, 0.5f) * 0.6f) : (Color.White * 0.6f);
			string {13345} = {22384} ? Local.click_to_discard : text;
			form.AddChild(new Label({22370}.selectMenuItem_countText, Fonts.Arial_10, {13344}, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				RenderToDepthMap = false
			});
			bool flag = info.Class == CannonClass.Mortar && info.Poundage != null && info.Poundage.Value > Global.Player.UsedShipPlayer.CraftFrom.MaxMortarPoundage.GetValueOrDefault(1000);
			bool flag2 = (info.Class == CannonClass.Bombardier && {22381}.Side != CannonLocation.InBack && {22381}.Side != CannonLocation.InFront) || (info.Class == CannonClass.Special && {22381}.Mode != CannonLocationMode.Special) || (info.Class == CannonClass.Mortar && {22382}.RemainReserve == 0) || flag;
			{20431}.CannonToolTIp(info, form, false, !flag2 && !{22384}, null);
			{22279} equipment = {22279}.CurrentInstance;
			if ({22384})
			{
				form.EvClick += delegate(ClickUiEventArgs {22404})
				{
					equipment.SetCannons({22279}.SetCannonMode.RemoveOne, {22382}, this.{22393});
				};
				return form;
			}
			if (flag2)
			{
				form.Opacity = 0.6f;
			}
			else
			{
				form.EvClickEmptiness += delegate(ClickUiEventArgs {22405})
				{
					equipment.SetCannons({22279}.SetCannonMode.One, {22382}, this.{22393});
				};
			}
			form.EvGotMouseFocus += delegate()
			{
				form.TexturePath = {22370}.selectMenuItem_focus;
			};
			form.EvLostMouseFocus += delegate()
			{
				form.TexturePath = {22370}.selectMenuItem;
			};
			if (flag)
			{
				form.AddChild(new Label({22370}.selectMenuItem_countText + ((info.Class == CannonClass.Mortar) ? new Vector2(80f, -20f) : new Vector2(60f, 0f)), Fonts.Arial_10, Color.Orange, Local.weapons_limit2(Global.Player.UsedShipPlayer.CraftFrom.MaxMortarPoundage.Value), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			if (!flag2 && this.{22393}.Mode == CannonLocationMode.Default)
			{
				if (info.Class != CannonClass.Bombardier)
				{
					LabelButton labelButton = new LabelButton({22370}.selectMenuItem_maxButton - new Vector2(25f, 0f) + new Vector2(0f, 22f), Local.PortVisaulEquipmentInterface_8, Fonts.F_m14_ThinBold, Color.Gray * 0.7f, Color.Gold, delegate(ClickUiEventArgs {22406})
					{
						equipment.SetCannons({22279}.SetCannonMode.All, {22382}, this.{22393});
					});
					labelButton.ToolTipState = new ToolTipState(null, Local.PortVisaulEquipmentInterface_9, Array.Empty<ToolTipCharacteristics>());
					labelButton.EvGotMouseFocus += delegate()
					{
						equipment.SetHighlight(-2, null, null);
					};
					labelButton.EvLostMouseFocus += equipment.ResetHighlight;
					labelButton.EvRemoveFromContainer += equipment.ResetHighlight;
					form.AddChild(labelButton);
				}
				if (Global.Player.UsedShip.StaticInfo.Decks.Size != 1)
				{
					LabelButton labelButton2 = new LabelButton({22370}.selectMenuItem_maxButton + new Vector2(15f, 0f) + new Vector2(0f, 22f), Local.PortVisaulEquipmentInterface_10, Fonts.F_m14_ThinBold, Color.Gray * 0.7f, Color.Gold, delegate(ClickUiEventArgs {22407})
					{
						equipment.SetCannons({22279}.SetCannonMode.Deck, {22382}, this.{22393});
					});
					form.AddChild(labelButton2);
					labelButton2.ToolTipState = new ToolTipState(null, Local.PortVisaulEquipmentInterface_11, Array.Empty<ToolTipCharacteristics>());
					labelButton2.EvGotMouseFocus += delegate()
					{
						equipment.SetHighlight(-1, {22381}, null);
					};
					labelButton2.EvLostMouseFocus += equipment.ResetHighlight;
					labelButton2.EvRemoveFromContainer += equipment.ResetHighlight;
				}
				LabelButton labelButton3 = new LabelButton({22370}.selectMenuItem_maxButton - new Vector2(69f, 0f) + new Vector2(0f, 22f), Local.PortVisaulEquipmentInterface_10_b, Fonts.F_m14_ThinBold, Color.Gray * 0.7f, Color.Gold, delegate(ClickUiEventArgs {22408})
				{
					equipment.SetCannons({22279}.SetCannonMode.Board, {22382}, this.{22393});
				});
				form.AddChild(labelButton3);
				labelButton3.ToolTipState = new ToolTipState(null, Local.PortVisaulEquipmentInterface_11, Array.Empty<ToolTipCharacteristics>());
				labelButton3.EvGotMouseFocus += delegate()
				{
					equipment.SetHighlight(-1, null, new CannonLocation?({22381}.Side));
				};
				labelButton3.EvLostMouseFocus += equipment.ResetHighlight;
				labelButton3.EvRemoveFromContainer += equipment.ResetHighlight;
			}
			return form;
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserUpdate(ref FrameTime {22385})
		{
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x000ADAC5 File Offset: 0x000ABCC5
		public static void Open(CannonLocationInfo {22386})
		{
			{22370}.TryClose();
			{22370}.Instance = new {22370}({22386});
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x000ADAD7 File Offset: 0x000ABCD7
		public static void TryClose()
		{
			{22370} instance = {22370}.Instance;
			if (instance != null)
			{
				instance.RemoveFromContainer();
			}
			{22370}.Instance = null;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x000ADBB4 File Offset: 0x000ABDB4
		[CompilerGenerated]
		private CheckboxControl {22387}(CannonCategory {22388}, CustomSpriteFont {22389}, Color {22390})
		{
			bool flag = {22370}.AllowedForShip({22388});
			bool {13137} = flag && {22370}.cannonsFilter.Contains({22388});
			CheckboxControl checkboxControl = new CheckboxControl(Vector2.Zero, AtlasPortGui.vd_checkBox_26px_true, AtlasPortGui.vd_checkBox_26px_false, {13137}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				DisabledMode = (flag ? null : new Rectangle?(AtlasPortGui.vd_checkBox_26px_dis))
			};
			if (!flag)
			{
				checkboxControl.ToolTip = new ToolTip(new ToolTipState(null, Local.cannons_page_lowrank, Array.Empty<ToolTipCharacteristics>()));
			}
			CheckboxControl checkboxControl2 = checkboxControl;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 1);
			defaultInterpolatedStringHandler.AppendLiteral("cannons_category_");
			defaultInterpolatedStringHandler.AppendFormatted<CannonCategory>({22388});
			checkboxControl2.SetText(Local.Current(defaultInterpolatedStringHandler.ToStringAndClear()), {22389}, {22390});
			checkboxControl.ExCheckEvent(delegate(CheckboxCheckedEventArgs {22403})
			{
				if ({22403}.NewValue)
				{
					{22370}.cannonsFilter.Add({22388});
				}
				else
				{
					{22370}.cannonsFilter.Remove({22388});
				}
				this.{22373}();
			});
			return checkboxControl;
		}

		// Token: 0x04001285 RID: 4741
		private static {22370} Instance;

		// Token: 0x04001286 RID: 4742
		private static readonly Rectangle selectMenuItem = new Rectangle(1240, 51, 330, 50);

		// Token: 0x04001287 RID: 4743
		private static readonly Rectangle selectMenuItem_focus = new Rectangle(1240, 102, 330, 50);

		// Token: 0x04001288 RID: 4744
		private static readonly Marker selectMenuItem_icon = new Marker(1f, 1f, 48f, 48f);

		// Token: 0x04001289 RID: 4745
		private static readonly Vector2 selectMenuItem_name = new Vector2(80f, 4f);

		// Token: 0x0400128A RID: 4746
		private static readonly Vector2 selectMenuItem_countText = new Vector2(80f, 24f);

		// Token: 0x0400128B RID: 4747
		private static readonly Vector2 selectMenuItem_maxButton = new Vector2(255f, 0f);

		// Token: 0x0400128C RID: 4748
		private static float savedScroll = 0f;

		// Token: 0x0400128D RID: 4749
		private static readonly HashSet<CannonCategory> cannonsFilter = new HashSet<CannonCategory>
		{
			CannonCategory.Light,
			CannonCategory.Medium,
			CannonCategory.Heavy
		};

		// Token: 0x0400128E RID: 4750
		private readonly Vector2 {22392};

		// Token: 0x0400128F RID: 4751
		private readonly CannonLocationInfo {22393};

		// Token: 0x04001290 RID: 4752
		private readonly CannonGameInfo {22394};

		// Token: 0x04001291 RID: 4753
		private Form {22395};

		// Token: 0x020003AC RID: 940
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001292 RID: 4754
			public static Func<CannonCategory, bool> <0>__AllowedForShip;
		}
	}
}
