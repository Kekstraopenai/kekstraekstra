using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Data;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using TheraEngine.Reactive;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200007C RID: 124
	internal class {17177} : {17068}
	{
		// Token: 0x06000383 RID: 899 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
		public {17177}(bool {17179}) : base(Marker.FromCentrScreen(Engine.GS.UIArea.WidthHeight(), {17177}.c_main), {17177}.c_main, {17179} ? {17068}.BlockingWay.NoBackground : {17068}.BlockingWay.BackgroundClosing, false)
		{
			{17177} currentInstance = {17177}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.RemoveFromContainer();
			}
			{17177}.CurrentInstance = this;
			if ({17179})
			{
				if (Engine.GS.MouseToUI.X > (float)(Engine.GS.UIArea.Width / 2))
				{
					base.Pos = base.Pos.SetX(base.Pos.XY.X - 291f);
				}
				else
				{
					base.Pos = base.Pos.SetX(base.Pos.XY.X + 291f);
				}
			}
			this.AllowDragDrop = true;
			base.EvRemoveFromContainer += delegate()
			{
				{17177}.CurrentInstance = null;
			};
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001D5D0 File Offset: 0x0001B7D0
		public void AddDownText(string {17180})
		{
			TextBlockBuilder textBlockBuilder = TextBlockBuilder.CreateBlock(327f, {17180}, {17177}.textColor * 0.7f, Fonts.Arial_10, -1f);
			textBlockBuilder.replaceFontOrNull = Fonts.Arial_10;
			textBlockBuilder.ReplaceColor({17177}.textColor * 0.7f);
			TextBlockControl {13204} = textBlockBuilder.Create(base.Pos.XY + new Vector2(23f, 373f));
			base.AddChild({13204});
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001D650 File Offset: 0x0001B850
		public void AddTabs(Action<int> {17181}, int {17182}, params string[] {17183})
		{
			StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(186f, 50f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Reactive<int> selectedItem = new Reactive<int>(0);
			for (int i = 0; i < {17183}.Length; i++)
			{
				int cached = i;
				LabelButton lb = new LabelButton(Vector2.Zero, {17183}[i], Fonts.Philosopher_14, {17177}.textColor, Color.Green, delegate(ClickUiEventArgs {17221})
				{
					selectedItem.Value = cached;
					{17181}(cached);
				});
				selectedItem.Subscribe(delegate(int {17222})
				{
					lb.DefaultColor = (({17222} == cached) ? Color.DarkGreen : {17177}.textColor);
				}, true);
				if (stackForm.CountChild() > 0)
				{
					stackForm.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Philosopher_14, {17177}.textColor * 0.7f, " / ", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
				}
				stackForm.AddItem(new UiControl[]
				{
					lb
				});
			}
			{17181}({17182});
			stackForm.Pos = stackForm.Pos.SetX(stackForm.Pos.XY.X - stackForm.Pos.WH.X * 0.5f);
			stackForm.DisableDepthFocusTest = true;
			this.{17219} = stackForm;
			base.AddChild(this.{17219});
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001D7C6 File Offset: 0x0001B9C6
		public void CleanTabs()
		{
			StackForm stackForm = this.{17219};
			if (stackForm != null)
			{
				stackForm.RemoveFromContainer();
			}
			this.{17219} = null;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0001D7E0 File Offset: 0x0001B9E0
		private bool useShipHold
		{
			get
			{
				return Global.Game.GetCurrentSceneName == GameSceneName.Game || (Global.Player.IsPortEntry && Global.Player.NearPortType == PortEnteringType.Miniport && {22452}.CurrentInstance != null);
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001D814 File Offset: 0x0001BA14
		private void AddHeader(string {17184}, string {17185}, Action<TextBlockBuilder> {17186})
		{
			base.AddChild(new Label(base.Pos.XY + new Vector2(186f, 32f), ({17184}.Length > 25) ? Fonts.Philosopher_14 : Fonts.Philosopher_18, {17177}.textColor, {17184}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			TextBlockBuilder textBlockBuilder = TextBlockBuilder.CreateBlock(324f, {17185}, {17177}.textColor * 0.7f, Fonts.Arial_10, -1f);
			textBlockBuilder.replaceFontOrNull = Fonts.Arial_10;
			if ({17186} != null)
			{
				{17186}(textBlockBuilder);
			}
			for (int i = 0; i < textBlockBuilder.blocks.Size; i++)
			{
				TextBlockBuilder.TextBlockFragment textBlockFragment = textBlockBuilder.blocks.Array[i];
				bool flag = false;
				for (int j = 0; j <= 9; j++)
				{
					if (textBlockFragment.Text != null && textBlockFragment.Text.Contains(j.ToString()))
					{
						flag = true;
						break;
					}
				}
				if (flag && textBlockFragment.Text.Length < 20)
				{
					textBlockFragment.Font = Fonts.Arial_10Bold;
				}
				textBlockFragment.Text = textBlockFragment.Text.Replace("#", "");
				textBlockBuilder.blocks.Array[i] = textBlockFragment;
			}
			textBlockBuilder.ReplaceColor({17177}.textColor * 0.7f);
			TextBlockControl {13204} = textBlockBuilder.Create(base.Pos.XY + new Vector2(23f, (float)((this.{17219} == null) ? 53 : 73)));
			base.AddChild({13204});
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001D9A4 File Offset: 0x0001BBA4
		private void {17187}(UiControl {17188}, int {17189})
		{
			new UiBrightnessAnimation({17188}, 1.3f, 70f);
			new UiBrightnessAnimation({17188}, 1f, 300f);
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UiCommonCraftUi, 0.03f, 1f);
			Label label = new Label(Engine.GS.MouseToUI + Rand.NextRadialVector2(0f, 20f) + new Vector2(20f, 0f), Fonts.Philosopher_14Bold, Color.Brown, "+" + {17189}.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			new UiOpacityAnimation(label, 0f, 1f, 50f);
			new UiOpacityAnimation(label, 1f, 0.5f, 300f);
			new UiOpacityAnimation(label, 0.5f, 0f, 150f);
			new UiRemoveAction(label);
			label.RenderToDepthMap = false;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001DA94 File Offset: 0x0001BC94
		public void SetData(string {17190}, bool {17191}, string {17192}, Action<TextBlockBuilder> {17193}, CraftingRecipe {17194}, RTIf {17195}, int {17196}, bool {17197}, [TupleElementNames(new string[]
		{
			"resCount",
			"btIndex"
		})] Action<ValueTuple<int, int>> {17198}, bool {17199} = false, string {17200} = null, string[] {17201} = null, int {17202} = 1, bool {17203} = true, int {17204} = 2147483647, bool {17205} = false, float {17206} = -1f)
		{
			{17177}.<>c__DisplayClass21_0 CS$<>8__locals1 = new {17177}.<>c__DisplayClass21_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.recipie = {17194};
			CS$<>8__locals1.isDestroy = {17191};
			CS$<>8__locals1.craftTimeFactory = {17206};
			CS$<>8__locals1.maxCountPerAction = {17204};
			CS$<>8__locals1.packSize = {17196};
			CS$<>8__locals1.applyCost = {17203};
			CS$<>8__locals1.tittle = {17190};
			CS$<>8__locals1.completeCallback = {17198};
			CS$<>8__locals1.reduceMaxCountPerAction = {17205};
			CS$<>8__locals1.isSingleAction = {17197};
			CS$<>8__locals1.craftTimeFactoryWrap = CS$<>8__locals1.craftTimeFactory;
			if (CS$<>8__locals1.recipie != null && CS$<>8__locals1.recipie.InputItems.IsEmpty && CS$<>8__locals1.recipie.InputMoney.Value == 0)
			{
				throw new InvalidOperationException();
			}
			int {17217} = 0;
			CS$<>8__locals1.useCraftTimeR = new RTIf({17195}.Value);
			float allCraftTimeBonus = Session.Game.AllCraftTimeBonus;
			{17217} = (int)(allCraftTimeBonus * 100f);
			{17177}.<>c__DisplayClass21_0 CS$<>8__locals2 = CS$<>8__locals1;
			CS$<>8__locals2.useCraftTimeR.Value = CS$<>8__locals2.useCraftTimeR.Value * (1f - allCraftTimeBonus);
			base.ClearAllChild();
			if (this.{17219} != null)
			{
				base.AddChild(this.{17219});
			}
			base.AddChild(new LabelButton(base.Pos.XY + new Vector2(323f, 15f), "X", Fonts.Philosopher_36, Color.Black * 0.5f, Color.Pink, delegate(ClickUiEventArgs {17223})
			{
				CS$<>8__locals1.<>4__this.BlockAndClose();
			}));
			CS$<>8__locals1.computeMaxCount = delegate()
			{
				if (CS$<>8__locals1.recipie == null | CS$<>8__locals1.isDestroy)
				{
					return 1;
				}
				int val = CS$<>8__locals1.recipie.MaxCount(Session.Account, CS$<>8__locals1.<>4__this.useShipHold ? Global.Player.ResourcesOfHold : Session.Account.NearPortStorage);
				if (CS$<>8__locals1.useCraftTimeR.Value != 0f)
				{
					val = Math.Min((int)Math.Floor((double)(((CS$<>8__locals1.craftTimeFactory == -1f) ? Session.Account.AvailCraftTime.Value : CS$<>8__locals1.craftTimeFactoryWrap.Value) / CS$<>8__locals1.useCraftTimeR.Value)), val);
				}
				return Math.Min(CS$<>8__locals1.maxCountPerAction, val);
			};
			CS$<>8__locals1.currentCount = {17202};
			this.AddHeader(CS$<>8__locals1.tittle, {17192}, {17193});
			if ({17199} || CS$<>8__locals1.recipie == null)
			{
				return;
			}
			base.AddChild(new Label(base.Pos.XY + new Vector2(186f, 255f), Fonts.Philosopher_18, {17177}.textColor, string.IsNullOrEmpty({17200}) ? (CS$<>8__locals1.isDestroy ? Local.CommonItemCraftUi_0 : ((CS$<>8__locals1.recipie.InputItems.IsEmpty || (CS$<>8__locals1.recipie.InputItems.NamesCount == 1 && CS$<>8__locals1.recipie.InputItems.RandomName() == 37)) ? Local.CommonItemCraftUi_1 : Local.required)) : {17200}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(0f, 278f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			StackForm stackForm2 = new StackForm(base.Pos.XY + new Vector2(0f, 278f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if (CS$<>8__locals1.recipie != null)
			{
				using (IEnumerator<GSILocalEnumerablePair<ResourceInfo>> enumerator = ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)CS$<>8__locals1.recipie.InputItems.ResourceInfo).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GSILocalEnumerablePair<ResourceInfo> name = enumerator.Current;
						this.smp_addCondition(CS$<>8__locals1.isDestroy, stackForm, ref stackForm2, name.Info.IconTexture, name.Info, name.Info.IconTexture.Bounds, name.Info.Name, () => (CS$<>8__locals1.<>4__this.useShipHold ? Global.Player.ResourcesOfHold : Session.Account.NearPortStorage).GetCount((int)name.Info.ID), () => (int)Math.Ceiling((double)((1f - CS$<>8__locals1.recipie.ReduceCraftCost) * (float)CS$<>8__locals1.currentCount.Value * (float)name.Count)), (int)Math.Round((double)(CS$<>8__locals1.recipie.ReduceCraftCost * 100f)));
					}
				}
				if (CS$<>8__locals1.recipie.InputMoney.Value > 0)
				{
					this.smp_addCondition(CS$<>8__locals1.isDestroy, stackForm, ref stackForm2, CommonAtlas.Texture.Tex, null, CommonAtlas.goldIconSingleWithBackground40, Local.gold_s, () => Session.Account.Gold, () => (int)Math.Ceiling((double)((1f - CS$<>8__locals1.recipie.ReduceCraftCost) * (float)CS$<>8__locals1.currentCount.Value * (float)CS$<>8__locals1.recipie.InputMoney.Value)), (int)Math.Round((double)(CS$<>8__locals1.recipie.ReduceCraftCost * 100f)));
				}
			}
			if (CS$<>8__locals1.useCraftTimeR.Value != 0f)
			{
				this.smp_addCondition(CS$<>8__locals1.isDestroy, stackForm, ref stackForm2, CommonAtlas.Texture.Tex, null, CommonAtlas.craftTimeIcon, this.useShipHold ? Local.craft_time_factory : ((Global.Player.IsPortEntry && Global.Player.NearPortType == PortEnteringType.PersonalIsle) ? Local.personal_isle_craftTime2 : Local.craft_time), delegate
				{
					if (CS$<>8__locals1.craftTimeFactory != -1f)
					{
						return (int)CS$<>8__locals1.craftTimeFactoryWrap.Value;
					}
					return (int)Session.Account.AvailCraftTime.Value;
				}, () => (int)Math.Ceiling((double)((float)CS$<>8__locals1.currentCount.Value * CS$<>8__locals1.useCraftTimeR.Value)), {17217});
			}
			stackForm.AddItem(new UiControl[]
			{
				stackForm2
			});
			stackForm.Pos = stackForm.Pos.SetX(base.Pos.XY.X + (float)({17177}.c_main.Width / 2) - stackForm.Pos.WH.X / 2f);
			base.AddChild(stackForm);
			CS$<>8__locals1.selectBar = null;
			if (!CS$<>8__locals1.isSingleAction)
			{
				base.AddChild(new Label(base.Pos.XY + new Vector2(186f, 373f), Fonts.Philosopher_14, {17177}.textColor * 0.8f, Local.quanity, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
				TextBox textBox = new TextBox(base.Pos.XY + new Vector2(53f, 387f), new Rectangle(1400, 508, 94, 31), Fonts.Philosopher_14, Color.Black * 0.7f, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				textBox.IsEnter = true;
				textBox.Mask = TextBoxMaskType.Count;
				textBox.Text = (CS$<>8__locals1.currentCount.Value * CS$<>8__locals1.packSize).ToString();
				textBox.AttachMaxLengthModerator((CS$<>8__locals1.recipie != null && CS$<>8__locals1.recipie.InputItems[24] > 0) ? 8 : 6, null, Color.Transparent);
				CS$<>8__locals1.selectBar = new ProgressSelectBar(base.Pos.XY + new Vector2(177f, 391f), {17177}.c_progressSelect, {17177}.c_progressSelect, {17177}.c_progressSelectPoint, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				CS$<>8__locals1.selectBar.MaxValue = (float)Math.Max(1, CS$<>8__locals1.computeMaxCount());
				CS$<>8__locals1.selectBar.Value = (float)CS$<>8__locals1.currentCount.Value;
				textBox.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Black * 0.45f, Local.pcs, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.RightDown, PositionAlignment.Center, 5f);
				base.AddChild(new UiControl[]
				{
					textBox,
					CS$<>8__locals1.selectBar
				});
				CS$<>8__locals1.selectBar.EvGotMouseFocus += delegate()
				{
					CS$<>8__locals1.<>4__this.AllowDragDrop = false;
				};
				CS$<>8__locals1.selectBar.EvLostMouseFocus += delegate()
				{
					CS$<>8__locals1.<>4__this.AllowDragDrop = true;
				};
				bool blocker = false;
				textBox.EvTextChanged += delegate(string {17224})
				{
					if (blocker)
					{
						return;
					}
					int val;
					if (int.TryParse(textBox.Text, out val))
					{
						blocker = true;
						int val2 = (int)Math.Floor((double)((float)Math.Max(0, val) / (float)CS$<>8__locals1.packSize));
						CS$<>8__locals1.selectBar.Value = (float)Math.Min(CS$<>8__locals1.computeMaxCount(), val2);
						CS$<>8__locals1.currentCount.Value = Math.Max(1, val2);
						blocker = false;
					}
				};
				CS$<>8__locals1.selectBar.EvChange += delegate(ProgressBarChangeEventArgs {17225})
				{
					if (blocker)
					{
						return;
					}
					blocker = true;
					CS$<>8__locals1.currentCount.Value = (int)Math.Round((double)Math.Max(1f, CS$<>8__locals1.selectBar.Value));
					textBox.Text = (CS$<>8__locals1.currentCount.Value * CS$<>8__locals1.packSize).ToString();
					blocker = false;
				};
			}
			if ({17201} == null)
			{
				{17201} = new string[]
				{
					CS$<>8__locals1.isDestroy ? Local.CommonItemCraftUi_5 : ((CS$<>8__locals1.recipie.InputItems.IsEmpty && CS$<>8__locals1.recipie.InputMoney.Value != 0) ? Local.shop : Local.make)
				};
			}
			for (int i = 0; i < {17201}.Length; i++)
			{
				int iCache = i;
				string {13800} = {17201}[i];
				AnimatedButton craftBt = new AnimatedButton(base.Pos.XY + new Vector2((float)(186 - {17177}.c_craftBr_Active.Width / 2 - {17177}.c_craftBr_Active.Width * ({17201}.Length - 1) / 2 + i * {17177}.c_craftBr_Active.Width), 439f), {17177}.c_craftBr_Passive, {17177}.c_craftBr_Passive, {17177}.c_craftBr_Active, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				craftBt.SetText({13800}, Fonts.Philosopher_14, {17177}.textColor, false);
				base.AddChild(craftBt);
				Func<bool> checkAlllow = () => craftBt.AllowMouseInput && (CS$<>8__locals1.isDestroy || (CS$<>8__locals1.currentCount.Value <= CS$<>8__locals1.computeMaxCount() && (float)((int)Math.Ceiling((double)(CS$<>8__locals1.useCraftTimeR.Value * (float)CS$<>8__locals1.currentCount.Value))) <= ((CS$<>8__locals1.craftTimeFactory == -1f) ? ((float)((int)Session.Account.AvailCraftTime.Value)) : CS$<>8__locals1.craftTimeFactoryWrap.Value)));
				craftBt.EvClick += delegate(ClickUiEventArgs {17226})
				{
					if (checkAlllow())
					{
						if ((!CS$<>8__locals1.isDestroy && CS$<>8__locals1.recipie != null) & CS$<>8__locals1.applyCost)
						{
							CS$<>8__locals1.recipie.ApplyMultiplied(Session.Account, CS$<>8__locals1.<>4__this.useShipHold ? Global.Player.ResourcesOfHold : Session.Account.NearPortStorage, CS$<>8__locals1.currentCount.Value);
							if (!string.IsNullOrEmpty(CS$<>8__locals1.tittle))
							{
								{19994}.Logbook(Local.lbe_craft(CS$<>8__locals1.tittle, CS$<>8__locals1.currentCount.Value * CS$<>8__locals1.packSize), LBFlags.L1);
							}
						}
						if (CS$<>8__locals1.craftTimeFactory == -1f)
						{
							int num = (int)Math.Ceiling((double)(CS$<>8__locals1.useCraftTimeR.Value * (float)CS$<>8__locals1.currentCount.Value));
							Session.UsedCraftTimeSync += (float)num;
							PlayerAccount account = Session.Account;
							account.AvailCraftTime.Value = account.AvailCraftTime.Value - (float)num;
						}
						else
						{
							Session.UsedCraftTimeSync += CS$<>8__locals1.useCraftTimeR.Value * (float)CS$<>8__locals1.currentCount.Value;
							{17177}.<>c__DisplayClass21_0 CS$<>8__locals6 = CS$<>8__locals1;
							CS$<>8__locals6.craftTimeFactoryWrap.Value = CS$<>8__locals6.craftTimeFactoryWrap.Value - CS$<>8__locals1.useCraftTimeR.Value * (float)CS$<>8__locals1.currentCount.Value;
						}
						CS$<>8__locals1.completeCallback(new ValueTuple<int, int>(CS$<>8__locals1.currentCount.Value * CS$<>8__locals1.packSize, iCache));
						CS$<>8__locals1.<>4__this.{17187}(craftBt, CS$<>8__locals1.currentCount.Value * CS$<>8__locals1.packSize);
						if (CS$<>8__locals1.reduceMaxCountPerAction)
						{
							CS$<>8__locals1.maxCountPerAction -= CS$<>8__locals1.currentCount.Value;
						}
						if (CS$<>8__locals1.isSingleAction)
						{
							craftBt.AllowMouseInput = false;
							craftBt.Opacity = 0.5f;
						}
						else
						{
							CS$<>8__locals1.selectBar.MaxValue = (float)Math.Max(1, CS$<>8__locals1.computeMaxCount());
						}
						{17745} currentInstance = {17745}.CurrentInstance;
						if (currentInstance == null)
						{
							return;
						}
						currentInstance.ExternalUpdate();
					}
				};
				craftBt.UpdateComplete += delegate(UiControl {17227})
				{
					craftBt.Opacity = (checkAlllow() ? 1f : 0.5f);
				};
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001E39C File Offset: 0x0001C59C
		private static string intToStrLimited(int {17207})
		{
			if ({17207} > 999999)
			{
				return ({17207} / 1000000).ToString() + "kk";
			}
			if ({17207} > 9999)
			{
				return ({17207} / 1000).ToString() + "k";
			}
			return {17207}.ToString();
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001E3F4 File Offset: 0x0001C5F4
		protected void smp_addCondition(bool {17208}, StackForm {17209}, ref StackForm {17210}, Texture2D {17211}, IStorageAsset {17212}, Rectangle {17213}, string {17214}, Func<int> {17215}, Func<int> {17216}, int {17217})
		{
			if ({17210}.CountChild() == 5)
			{
				{17209}.AddItem(new UiControl[]
				{
					{17210}
				});
				{17210} = new StackForm({17209}.Pos.XY, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			}
			Form entry = new Form(new Marker(0f, 0f, 85f, 69f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			string {12778} = "";
			Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
			if ({17212} != null)
			{
				{12778} = {20431}.GenericItemConnection(tlist, {17212});
			}
			entry.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState({17214} + ({17208} ? "" : Local.CommonItemCraftUi_8), {12778}, tlist.ToArray()));
			entry.AddChild(new Image(new Marker(22f, 0f, 40f, 40f), {17211}, {17213}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			LiveLabel live1 = null;
			LiveLabel live2 = null;
			live1 = new LiveLabel(new Vector2(0f, 39f), Fonts.F_m14_ThinBold, Color.Transparent, delegate()
			{
				int num = {17215}();
				int num2 = {17216}();
				if (live1 != null)
				{
					live1.BasicColor = ((num2 > num && !{17208}) ? new Color(120, 25, 25) : new Color(3, 98, 3)) * 0.5f;
				}
				base.<smp_addCondition>g__UpdatePositions|0();
				return {17177}.intToStrLimited(num).ToString();
			}, 16);
			entry.AddChild(live1);
			if (!{17208})
			{
				Label diff = new Label(new Vector2(0f, 60f), Fonts.Arial_9, new Color(120, 25, 25) * 0.66f, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				entry.AddChild(diff);
				live2 = new LiveLabel(new Vector2(0f, 39f), Fonts.F_m14_ThinBold, Color.Transparent, delegate()
				{
					int num = {17215}();
					int num2 = {17216}();
					if (live2 != null)
					{
						live2.BasicColor = ((num2 > num && !{17208}) ? new Color(120, 25, 25) : new Color(3, 98, 3));
					}
					if (num2 > num && num2 > 1 && num > 0)
					{
						diff.IsVisible = true;
						diff.Text = Local.PortСraftShipWindow_25 + (num2 - num).ToString();
						diff.Pos = diff.Pos.SetX(entry.Pos.Center.X - diff.Pos.WH.X / 2f);
					}
					else
					{
						diff.IsVisible = false;
					}
					return "/" + {17177}.intToStrLimited(num2);
				}, 16);
				entry.AddChild(live2);
			}
			if ({17217} != 0)
			{
				Form form = new Form(Vector2.Zero, {17177}.c_discountMarker, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form.Pos = form.Pos.SetHeight(21f);
				form.AddChild(new Label(form.Pos.Center, Fonts.Arial_12, Color.Black * 0.7f, "-" + {17217}.ToString() + "%", PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
				entry.AddChildPos(form, PositionAlignment.Center, PositionAlignment.LeftUp, -16f);
			}
			{17210}.AddItem(new UiControl[]
			{
				entry
			});
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001E6C5 File Offset: 0x0001C8C5
		protected override void UserUpdate(ref FrameTime {17218})
		{
			base.UserUpdate(ref {17218});
			if (InputHelper.IsClick(Keys.Escape))
			{
				base.BlockAndClose();
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001E6E0 File Offset: 0x0001C8E0
		protected override void UserBackRender()
		{
			if (this.{17220} = (Engine.GS.CurrentTexture != CommonAtlas.Texture.Tex))
			{
				Engine.GS.SetTexture(CommonAtlas.Texture);
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001E720 File Offset: 0x0001C920
		protected override void UserFrontRender()
		{
			if (this.{17220})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x040002C4 RID: 708
		public static readonly Rectangle c_main = new Rectangle(1400, 0, 373, 507);

		// Token: 0x040002C5 RID: 709
		public static readonly Rectangle c_progressSelect = new Rectangle(1495, 508, 152, 20);

		// Token: 0x040002C6 RID: 710
		public static readonly Rectangle c_progressSelectPoint = new Rectangle(1648, 508, 20, 20);

		// Token: 0x040002C7 RID: 711
		public static readonly Rectangle c_craftBr_Active = new Rectangle(1232, 47, 167, 46);

		// Token: 0x040002C8 RID: 712
		public static readonly Rectangle c_craftBr_Passive = new Rectangle(1232, 0, 167, 46);

		// Token: 0x040002C9 RID: 713
		public static readonly Rectangle c_craftBr_Passive_AtlPortGui = new Rectangle(207, 168, 167, 46);

		// Token: 0x040002CA RID: 714
		public static readonly Rectangle c_arrowButton_right = new Rectangle(1371, 181, 28, 27);

		// Token: 0x040002CB RID: 715
		public static readonly Rectangle c_arrowButton_left = new Rectangle(1371, 209, 28, 27);

		// Token: 0x040002CC RID: 716
		public static readonly Rectangle c_discountMarker = new Rectangle(1465, 1655, 83, 41);

		// Token: 0x040002CD RID: 717
		public static readonly Rectangle c_textBoxBlack = new Rectangle(1400, 508, 94, 31);

		// Token: 0x040002CE RID: 718
		public static readonly Color textColor = new Color(31, 27, 19);

		// Token: 0x040002CF RID: 719
		public static {17177} CurrentInstance;

		// Token: 0x040002D0 RID: 720
		private StackForm {17219};

		// Token: 0x040002D1 RID: 721
		private bool {17220};
	}
}
