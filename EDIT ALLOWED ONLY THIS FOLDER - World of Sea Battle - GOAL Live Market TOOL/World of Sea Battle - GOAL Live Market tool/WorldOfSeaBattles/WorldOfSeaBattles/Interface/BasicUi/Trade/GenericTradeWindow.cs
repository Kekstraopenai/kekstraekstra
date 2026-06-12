using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Data;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using WorldOfSeaBattles.Interface.PortPagesUi.Trade;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Interface;

namespace WorldOfSeaBattles.Interface.BasicUi.Trade
{
	// Token: 0x020005A4 RID: 1444
	internal class GenericTradeWindow : {17177}
	{
		// Token: 0x06002154 RID: 8532 RVA: 0x0012A916 File Offset: 0x00128B16
		public GenericTradeWindow(TradeWindowArgs {26460}) : base(false)
		{
			this.{26487} = {26460};
			this.AllowDragDrop = false;
			this.{26462}({26460}.forceSelectItem);
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0012A93C File Offset: 0x00128B3C
		private void {26461}()
		{
			base.ClearAllChild();
			base.AddChild(new Label(base.Pos.XY + new Vector2(186f, 32f), Fonts.Philosopher_18, {17177}.textColor, this.{26487}.title, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x0012A998 File Offset: 0x00128B98
		private void {26462}(IStorageAsset {26463} = null)
		{
			GenericTradeWindow.<>c__DisplayClass8_0 CS$<>8__locals1 = new GenericTradeWindow.<>c__DisplayClass8_0();
			CS$<>8__locals1.<>4__this = this;
			this.{26461}();
			CS$<>8__locals1.continueBt = new AnimatedButton(base.Pos.XY + new Vector2((float)(186 - {17177}.c_craftBr_Active.Width / 2), 439f), {17177}.c_craftBr_Passive, {17177}.c_craftBr_Passive, {17177}.c_craftBr_Active, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if (this.{26487}.Items == null || (!this.{26487}.Items.Any<ValueTuple<IStorageAsset, GenericTradeWindow.ItemFlags>>() && this.{26487}.type != TradeType.Ship))
			{
				base.AddChild(new Label(base.Pos.XY + new Vector2(60f, 105f), Fonts.Philosopher_14, Color.Black, Local.TradePortInterface_NoResources, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				CS$<>8__locals1.continueBt.IsVisible = false;
			}
			else if ({26463} == null)
			{
				GenericTradeWindow.<>c__DisplayClass8_1 CS$<>8__locals2 = new GenericTradeWindow.<>c__DisplayClass8_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				int num = 48;
				CS$<>8__locals2.imagesList = new Tlist<Image>();
				ValueTuple<IStorageAsset, GenericTradeWindow.ItemFlags>[] array = this.{26487}.Items.ToArray<ValueTuple<IStorageAsset, GenericTradeWindow.ItemFlags>>();
				Marker marker = new Marker(base.Pos.XY.X, base.Pos.XY.Y, (float)CommonAtlas.c_scrollUp.Width, 260f);
				Vector2 vector = new Vector2(320f, 80f);
				ScrollBarControl scrollBarControl = new ScrollBarControl(marker.Offset(vector), CommonAtlas.c_scrollUp, CommonAtlas.c_scrollDown, CommonAtlas.c_scrollPoint, CommonAtlas.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				vector = base.Pos.XY + new Vector2(34f, 80f);
				Vector2 vector2 = new Vector2(290f, 260f);
				ListItemViewControl listItemViewControl = new ListItemViewControl(new Marker(ref vector, ref vector2), scrollBarControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				BlocksStackFormControl blocksStackFormControl = new BlocksStackFormControl(Vector2.Zero, 6, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				listItemViewControl.AddItem(new UiControl[]
				{
					blocksStackFormControl
				});
				base.AddChild(new UiControl[]
				{
					scrollBarControl,
					listItemViewControl
				});
				ValueTuple<IStorageAsset, GenericTradeWindow.ItemFlags>[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					GenericTradeWindow.<>c__DisplayClass8_2 CS$<>8__locals3 = new GenericTradeWindow.<>c__DisplayClass8_2();
					CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals2;
					ValueTuple<IStorageAsset, GenericTradeWindow.ItemFlags> valueTuple = array2[i];
					CS$<>8__locals3.info = valueTuple.Item1;
					CS$<>8__locals3.isAllow = valueTuple.Item2;
					GenericTradeWindow.<>c__DisplayClass8_3 CS$<>8__locals4 = new GenericTradeWindow.<>c__DisplayClass8_3();
					CS$<>8__locals4.CS$<>8__locals3 = CS$<>8__locals3;
					GenericTradeWindow.<>c__DisplayClass8_3 CS$<>8__locals5 = CS$<>8__locals4;
					vector = Vector2.Zero;
					CS$<>8__locals5.img = new Image(new Marker(ref vector, (float)num, (float)num), CS$<>8__locals4.CS$<>8__locals3.info.getIconTexture, CS$<>8__locals4.CS$<>8__locals3.info.getIconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					blocksStackFormControl.AddItem(new UiControl[]
					{
						CS$<>8__locals4.img
					});
					CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.imagesList.Add(CS$<>8__locals4.img);
					if (TradeTypesHelper.NeedToExtendImages(this.{26487}.type))
					{
						CS$<>8__locals4.img.AddChild(new Form(new Marker(0f, (float)(num - 15), (float)num, 15f).Offset(CS$<>8__locals4.img.Pos.XY), CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							AnimatedFocus = false,
							BasicColor = Color.Black * 0.6f
						});
						CS$<>8__locals4.img.AddChild(new Label(CS$<>8__locals4.img.Pos.XY + new Vector2(3f, (float)(num - 14)), Fonts.Arial_10, Color.White, TradeTypesHelper.GetValueOfK(CS$<>8__locals4.CS$<>8__locals3.info, this.{26487}.type), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					}
					CS$<>8__locals4.img.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(null));
					if (CS$<>8__locals4.CS$<>8__locals3.info.getType == StorageAssetEnum.Ship_DisplayOnly)
					{
						CS$<>8__locals4.img.ToolTip.CurrentContent = new ToolTipState(CS$<>8__locals4.CS$<>8__locals3.info.getName, "", Array.Empty<ToolTipCharacteristics>());
					}
					else
					{
						CS$<>8__locals4.img.ToolTip.PromisedContent = ((UiControl {26504}) => {20431}.PreviewHandler(CS$<>8__locals4.CS$<>8__locals3.info, false, false, (CS$<>8__locals4.CS$<>8__locals3.isAllow == GenericTradeWindow.ItemFlags.Allow) ? string.Empty : Local.cant_sell_auction));
					}
					if (CS$<>8__locals4.CS$<>8__locals3.isAllow != GenericTradeWindow.ItemFlags.Allow)
					{
						CS$<>8__locals4.img.Opacity = 0.5f;
					}
					else
					{
						CS$<>8__locals4.img.EvClick += delegate(ClickUiEventArgs {26505})
						{
							for (int j = 0; j < CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.imagesList.Size; j++)
							{
								CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.imagesList.Array[j].BasicColor = Color.White;
							}
							CS$<>8__locals4.img.BasicColor = new Color(0, 255, 0, 192);
							CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.{26489} = CS$<>8__locals4.CS$<>8__locals3.info;
							CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.continueBt.AllowMouseInput = true;
						};
					}
				}
				base.AddChild(new Label(base.Pos.XY + new Vector2(186f, 389f), Fonts.Arial_12, {17177}.textColor, this.{26487}.itemsSourceName, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
				CS$<>8__locals2.CS$<>8__locals1.continueBt.AllowMouseInput = false;
			}
			CS$<>8__locals1.continueBt.SetText(Local.to_continue, Fonts.Philosopher_14, {17177}.textColor, false);
			base.AddChild(CS$<>8__locals1.continueBt);
			CS$<>8__locals1.continueBt.EvClick += delegate(ClickUiEventArgs {26503})
			{
				CS$<>8__locals1.<>4__this.{26464}();
			};
			if ({26463} != null)
			{
				this.{26489} = {26463};
				CS$<>8__locals1.continueBt.ImitateClick(false);
			}
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x0012AEC4 File Offset: 0x001290C4
		private void {26464}()
		{
			this.{26461}();
			this.{26488} = true;
			if (true || this.{26487}.type == TradeType.Holding || this.{26487}.type == TradeType.Booking)
			{
				this.{26465}();
				return;
			}
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Rectangle {13194} = new Rectangle(941, 326, 149, 83);
			Form form = new Form(new Marker(0f, 0f, 149f, 83f), {13194}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Form form2 = new Form(new Marker(0f, 0f, 149f, 83f), {13194}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			int num = 40;
			form.AddChildPos(new Form(new Marker(0f, 0f, (float)num, (float)num), CommonAtlas.goldIconSingle64, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			}, PositionAlignment.Center, PositionAlignment.LeftUp, 10f);
			form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Black * 0.9f, Local.gold, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.RightDown, 10f);
			form2.AddChildPos(new Form(new Marker(0f, 0f, (float)num, (float)num), CommonAtlas.monetsIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			}, PositionAlignment.Center, PositionAlignment.LeftUp, 10f);
			form2.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Black * 0.9f, Local.Monets2.TrimEnd(':'), PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.RightDown, 10f);
			form.EvClick += this.{26477};
			form2.EvClick += this.{26479};
			form.UpdateComplete += this.{26481};
			form2.UpdateComplete += this.{26483};
			stackForm.AddItem(new UiControl[]
			{
				form,
				form2
			});
			Label {12952} = new Label(Vector2.Zero, Fonts.Philosopher_14, {17177}.textColor, "За какую валюту продавать товар?", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChildPos({12952}, PositionAlignment.Center, PositionAlignment.LeftUp, 150f);
			base.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.LeftUp, 200f);
			AnimatedButton animatedButton = new AnimatedButton(base.Pos.XY + new Vector2((float)(186 - {17177}.c_craftBr_Active.Width / 2), 439f), {17177}.c_craftBr_Passive, {17177}.c_craftBr_Passive, {17177}.c_craftBr_Active, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			animatedButton.SetText(Local.to_continue, Fonts.Philosopher_14, {17177}.textColor, false);
			animatedButton.EvClick += this.{26485};
			base.AddChild(animatedButton);
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0012B15C File Offset: 0x0012935C
		private void {26465}()
		{
			this.{26461}();
			bool flag = true;
			if (this.{26487}.type != TradeType.TradeBetweenPlayers)
			{
				int num = 0;
				foreach (TradeOrderCommon tradeOrderCommon in this.{26487}.MyOrdersInCurrentPort)
				{
					if (tradeOrderCommon.Mode == TradeOrderMode.Shop == this.{26487}.ShouldModesBeEquals && tradeOrderCommon.ItemInfo == this.{26489} && (int)tradeOrderCommon.PortID == Global.Player.NearPort.PortID)
					{
						num++;
					}
				}
				if (num > 3)
				{
					new {17312}(Local.TradePortInterface_sameOrderError);
					return;
				}
			}
			else
			{
				flag = WosbTrading.AllowFreeTradeBetweenPlayers(this.{26489} as ResourceInfo);
			}
			base.SetData(Local.TradePortInterface_5, false, (this.{26487}.type == TradeType.TradeBetweenPlayers) ? (flag ? Local.tradeBetweenPlayers_tt : Local.tradeBetweenPlayers_tt2(this.{26489}.getName)) : this.{26487}.GetDescription, null, null, 0f, 1, true, delegate([TupleElementNames(new string[]
			{
				"resCount",
				"btIndex"
			})] ValueTuple<int, int> {26493})
			{
			}, true, null, null, 1, true, int.MaxValue, false, -1f);
			AnimatedButton continueBt = new AnimatedButton(base.Pos.XY + new Vector2((float)(186 - {17177}.c_craftBr_Active.Width / 2), 439f), {17177}.c_craftBr_Passive, {17177}.c_craftBr_Passive, {17177}.c_craftBr_Active, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			continueBt.AllowMouseInput = false;
			base.AddChild(continueBt);
			Vector2 minMaxCost = WosbTrading.GetTradeMinAndMaxCost(this.{26489});
			int minPrice = this.{26487}.minPrice(!this.{26488});
			if (!this.{26488})
			{
				minMaxCost = new Vector2(MathF.Floor(minMaxCost.X / 1000f), MathF.Ceiling(minMaxCost.Y / 100f) + 1f);
			}
			if (this.{26487}.type == TradeType.TradeBetweenPlayers && flag)
			{
				minMaxCost.X = (float)minPrice;
			}
			RTI maxCount = TradeTypesHelper.ComputeMaxCount(this.{26489}, this.{26487}.type);
			RTI currentCount = 1;
			RTIf currentCost = (float)((int)minMaxCost.X);
			if (this.{26487}.ShouldHaveQuantityBar)
			{
				this.CreateQuantityBar(maxCount, delegate(float {26494})
				{
					currentCount = (int){26494};
				});
			}
			if (this.{26487}.ShouldHaveCostBar)
			{
				this.CreateCountBar(delegate(float {26495})
				{
					currentCost.Value = (this.{26488} ? ((float)((int){26495})) : {26495});
				}, minMaxCost, this.{26488} || maxCount.Value == 1);
			}
			StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(0f, 148f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.smp_addCondition(false, null, ref stackForm, this.{26489}.getIconTexture, this.{26489}, this.{26489}.getIconTexture.Bounds, this.{26489}.getName, () => maxCount.Value, () => currentCount.Value, 0);
			stackForm.Pos = stackForm.Pos.SetX(base.Pos.XY.X + (float)({17177}.c_main.Width / 2) - stackForm.Pos.WH.X / 2f);
			base.AddChild(stackForm);
			LiveLabel itemsCost = null;
			base.AddChild(new LiveLabel(base.Pos.XY + new Vector2(50f, 390f), Fonts.Arial_12, {17177}.textColor * 0.75f, null, delegate(object {26496})
			{
				if (currentCost.Value != 0f)
				{
					return this.{26487}.taxName + ": " + StringHelper.BigValueHelper((int)MathF.Ceiling((float)currentCount.Value * currentCost.Value * this.{26487}.TaxHelper(this.{26489}, !this.{26488})));
				}
				return "";
			}, 1));
			string pricePostfix = " " + (this.{26488} ? Local.gold2 : Local.monets);
			if (this.{26487}.type == TradeType.Holding)
			{
				base.AddChild(new LiveLabel(base.Pos.XY + new Vector2(50f, 370f), Fonts.Arial_12, {17177}.textColor, null, (object {26497}) => Local.weight_is(Math.Ceiling((double)((float)currentCount.Value * this.{26489}.getStorageMass))), 1));
			}
			else
			{
				base.AddChild(new LiveLabel(base.Pos.XY + new Vector2(50f, 370f), Fonts.Arial_12, {17177}.textColor, null, (object {26498}) => Local.TradePortInterface_13 + ((minMaxCost.X == 0f && currentCost.Value == 0f) ? Local.free : (StringHelper.BigValueHelper((int)MathF.Ceiling((float)currentCount.Value * currentCost.Value)) + pricePostfix)), 1));
			}
			Action customMiddleware = this.CustomMiddleware;
			if (customMiddleware != null)
			{
				customMiddleware();
			}
			Action <>9__11;
			continueBt.EvClick += delegate(ClickUiEventArgs {26499})
			{
				IStorageAsset storageAsset = this.{26489};
				if (storageAsset is ProceduralShipInfo)
				{
					string {17371} = Local.TradePortInterface_21(((ProceduralShipInfo)storageAsset).ShipInfo.ShipName, StringHelper.BigValueHelper((int)currentCost.Value) + pricePostfix);
					Action {17372};
					if (({17372} = <>9__11) == null)
					{
						{17372} = (<>9__11 = delegate()
						{
							base.<LoadSelectCountPage>g__Click|8();
						});
					}
					new {17312}({17371}, {17372}, delegate()
					{
					});
					return;
				}
				base.<LoadSelectCountPage>g__Click|8();
			};
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_14, 0f);
			textBlockBuilder.Write(Local.TradePortInterface_14(minPrice), {17177}.textColor);
			textBlockBuilder.WriteImageTextSize(CommonAtlas.Texture.Tex, this.{26488} ? CommonAtlas.goldIconSingle32 : CommonAtlas.monetsIcon, null);
			TextBlockControl notEnoughText = textBlockBuilder.Create(Vector2.Zero);
			continueBt.AddChildPos(notEnoughText, PositionAlignment.Center, PositionAlignment.Center, 0f);
			continueBt.UpdateComplete += delegate(UiControl {26500})
			{
				continueBt.AllowMouseInput = this.{26487}.IsMouseInputAllowed(currentCost, currentCount, minPrice, this.{26489});
				continueBt.Opacity = (continueBt.AllowMouseInput ? 1f : 0.5f);
				if (itemsCost != null)
				{
					itemsCost.BasicColor = (continueBt.AllowMouseInput ? {17177}.textColor : Color.DarkRed);
				}
				if (notEnoughText.IsVisible = ((float)currentCount.Value * currentCost.Value <= (float)minPrice && minPrice > 0))
				{
					continueBt.SetText("", Fonts.Philosopher_14, {17177}.textColor, false);
					return;
				}
				continueBt.SetText(this.{26487}.GetContinueButtonText, Fonts.Philosopher_14, {17177}.textColor, false);
			};
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0012B6F0 File Offset: 0x001298F0
		private GenericTradeWindow.CustomSelectBar CreateQuantityBar(RTI {26466}, Action<float> {26467})
		{
			GenericTradeWindow.CustomSelectBar customSelectBar = this.CreateCustomSelectBar(228f, Local.quanity, {26467}, true, 1f, (float){26466}.Value);
			customSelectBar.TextBox.IsEnter = true;
			return customSelectBar;
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x0012B71D File Offset: 0x0012991D
		private GenericTradeWindow.CustomSelectBar CreateCountBar(Action<float> {26468}, Vector2 {26469}, bool {26470})
		{
			return this.CreateCustomSelectBar(290f, this.{26487}.PricePerOneName, {26468}, {26470}, {26469}.X, {26469}.Y);
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x0012B744 File Offset: 0x00129944
		private GenericTradeWindow.CustomSelectBar CreateCustomSelectBar(float {26471}, string {26472}, Action<float> {26473}, bool {26474}, float {26475}, float {26476})
		{
			base.AddChild(new Label(base.Pos.XY + new Vector2(186f, {26471}), Fonts.Philosopher_14, {17177}.textColor * 0.8f, {26472}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			TextBox textBox = new TextBox(base.Pos.XY + new Vector2(53f, {26471} + 14f), {17177}.c_textBoxBlack, Fonts.Philosopher_14, Color.Black * 0.7f, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			textBox.Text = ({26474} ? Math.Round((double){26475}).ToString() : Math.Max(1f, {26475}).ToString());
			GenericTradeWindow.CustomSelectBar bar = new GenericTradeWindow.CustomSelectBar(textBox)
			{
				Min = {26475},
				Max = {26476},
				SelectBarControl = new ProgressSelectBar(base.Pos.XY + new Vector2(177f, {26471} + 18f), {17177}.c_progressSelect, {17177}.c_progressSelect, {17177}.c_progressSelectPoint, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					Value = 0f,
					MaxValue = Math.Max(1f, {26476} - {26475})
				}
			};
			base.AddChild(new UiControl[]
			{
				textBox,
				bar.SelectBarControl
			});
			if (this.AllowDragDrop)
			{
				bar.SelectBarControl.EvGotMouseFocus += delegate()
				{
					this.AllowDragDrop = false;
				};
				bar.SelectBarControl.EvLostMouseFocus += delegate()
				{
					this.AllowDragDrop = true;
				};
			}
			bool blocker = false;
			textBox.EvTextChanged += delegate(string {26501})
			{
				if (blocker)
				{
					return;
				}
				float num;
				if (!float.TryParse(textBox.Text, out num))
				{
					textBox.FontColor = Color.Orange * 0.7f;
					return;
				}
				if (num < bar.Min || num > bar.Max)
				{
					textBox.FontColor = Color.Orange * 0.7f;
					return;
				}
				bar.SelectBarControl.Value = num - bar.Min;
			};
			bar.SelectBarControl.EvChange += delegate(ProgressBarChangeEventArgs {26502})
			{
				float num = MathHelper.Clamp(bar.SelectBarControl.Value, 0f, bar.Max - bar.Min);
				float obj = {26474} ? MathF.Round(num + bar.Min) : MathF.Round(num + bar.Min, 1);
				blocker = true;
				textBox.FontColor = Color.Black * 0.7f;
				textBox.Text = obj.ToString();
				blocker = false;
				{26473}(obj);
			};
			return bar;
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0012B955 File Offset: 0x00129B55
		[CompilerGenerated]
		private void {26477}(ClickUiEventArgs {26478})
		{
			this.{26488} = true;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0012B95E File Offset: 0x00129B5E
		[CompilerGenerated]
		private void {26479}(ClickUiEventArgs {26480})
		{
			this.{26488} = false;
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0012B967 File Offset: 0x00129B67
		[CompilerGenerated]
		private void {26481}(UiControl {26482})
		{
			{26482}.Opacity = (this.{26488} ? 1f : 0.6f);
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x0012B983 File Offset: 0x00129B83
		[CompilerGenerated]
		private void {26483}(UiControl {26484})
		{
			{26484}.Opacity = ((!this.{26488}) ? 1f : 0.6f);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0012B99F File Offset: 0x00129B9F
		[CompilerGenerated]
		private void {26485}(ClickUiEventArgs {26486})
		{
			this.{26465}();
		}

		// Token: 0x04002061 RID: 8289
		private const float GridHeight = 260f;

		// Token: 0x04002062 RID: 8290
		private TradeWindowArgs {26487};

		// Token: 0x04002063 RID: 8291
		internal Action CustomMiddleware;

		// Token: 0x04002064 RID: 8292
		private bool {26488};

		// Token: 0x04002065 RID: 8293
		private IStorageAsset {26489};

		// Token: 0x020005A5 RID: 1445
		private class CustomSelectBar
		{
			// Token: 0x06002161 RID: 8545 RVA: 0x0012B9A7 File Offset: 0x00129BA7
			public CustomSelectBar(TextBox {26491})
			{
				this.TextBox = {26491};
			}

			// Token: 0x06002162 RID: 8546 RVA: 0x0012B9B8 File Offset: 0x00129BB8
			public void UpdateMax(float {26492})
			{
				this.Max = {26492};
				this.SelectBarControl.MaxValue = this.Max - this.Min;
				int num;
				if (int.TryParse(this.TextBox.Text, out num) && (float)num > this.Max)
				{
					num = (int)this.Max;
					this.TextBox.Text = num.ToString();
				}
			}

			// Token: 0x04002066 RID: 8294
			public TextBox TextBox;

			// Token: 0x04002067 RID: 8295
			public ProgressSelectBar SelectBarControl;

			// Token: 0x04002068 RID: 8296
			public float Max;

			// Token: 0x04002069 RID: 8297
			public float Min;
		}

		// Token: 0x020005A6 RID: 1446
		internal enum ItemFlags
		{
			// Token: 0x0400206B RID: 8299
			Allow,
			// Token: 0x0400206C RID: 8300
			Disallow
		}
	}
}
