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
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200029E RID: 670
	internal sealed class {20614} : {17177}
	{
		// Token: 0x06000EC2 RID: 3778 RVA: 0x0007C8D4 File Offset: 0x0007AAD4
		public {20614}(PortDevelopment {20616}) : base(false)
		{
			if (Global.Player.IsPortEntry)
			{
				Global.Game.ScenePort.MakeAccSync();
			}
			this.{20632} = {20616};
			this.AllowDragDrop = false;
			this.{20619}();
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0007C90C File Offset: 0x0007AB0C
		public static void Open(PortDevelopment {20617})
		{
			{20614} instance = {20614}.Instance;
			if (instance != null)
			{
				instance.BlockAndClose();
			}
			{20614}.Instance = new {20614}({20617});
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0007C92C File Offset: 0x0007AB2C
		private void {20618}()
		{
			base.ClearAllChild();
			base.AddChild(new Label(base.Pos.XY + new Vector2(186f, 32f), Fonts.Philosopher_18, {17177}.textColor, Local.supply, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0007C97F File Offset: 0x0007AB7F
		private static IEnumerable<ResourceInfo> GetItems()
		{
			return new {20614}.<GetItems>d__11(-2);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0007C988 File Offset: 0x0007AB88
		private void {20619}()
		{
			{20614}.<>c__DisplayClass12_0 CS$<>8__locals1 = new {20614}.<>c__DisplayClass12_0();
			CS$<>8__locals1.<>4__this = this;
			this.{20618}();
			CS$<>8__locals1.continueBt = new AnimatedButton(base.Pos.XY + new Vector2((float)(186 - {17177}.c_craftBr_Active.Width / 2), 439f), {17177}.c_craftBr_Passive, {17177}.c_craftBr_Passive, {17177}.c_craftBr_Active, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			ResourceInfo[] array = {20614}.GetItems().ToArray<ResourceInfo>();
			CS$<>8__locals1.size = 48;
			CS$<>8__locals1.imagesList = new Tlist<Image>();
			Marker marker = new Marker(base.Pos.XY.X, base.Pos.XY.Y, (float)CommonAtlas.c_scrollUp.Width, 260f);
			Vector2 vector = new Vector2(320f, 80f);
			ScrollBarControl scrollBarControl = new ScrollBarControl(marker.Offset(vector), CommonAtlas.c_scrollUp, CommonAtlas.c_scrollDown, CommonAtlas.c_scrollPoint, CommonAtlas.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			vector = base.Pos.XY + new Vector2(34f, 80f);
			Vector2 vector2 = new Vector2(290f, 260f);
			ListItemViewControl listItemViewControl = new ListItemViewControl(new Marker(ref vector, ref vector2), scrollBarControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.grid = new BlocksStackFormControl(Vector2.Zero, 6, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			listItemViewControl.AddItem(new UiControl[]
			{
				CS$<>8__locals1.grid
			});
			base.AddChild(new UiControl[]
			{
				scrollBarControl,
				listItemViewControl
			});
			CS$<>8__locals1.<LoadSelectItemPage>g__AddItem|0(CommonAtlas.Texture.Tex, CommonAtlas.conquerorBadgeIcon, (UiControl {20640}) => new ToolTipState(Local.conquer_badges, "", Array.Empty<ToolTipCharacteristics>()), delegate
			{
				CS$<>8__locals1.<>4__this.{20633} = null;
				CS$<>8__locals1.<>4__this.{20634} = true;
				CS$<>8__locals1.<>4__this.{20620}(Local.PortSupply_SelectItemDesc(StringHelper.BigValueHelper(30000)));
			});
			ResourceInfo[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				ResourceInfo info = array2[i];
				CS$<>8__locals1.<LoadSelectItemPage>g__AddItem|0(info.IconTexture, info.IconTexture.Bounds, (UiControl {20648}) => {20431}.PreviewHandler(info, false, false, null), delegate
				{
					CS$<>8__locals1.<>4__this.{20633} = info;
					CS$<>8__locals1.<>4__this.{20634} = false;
					CS$<>8__locals1.<>4__this.{20620}(Local.PortSupply_SelectItemDesc(StringHelper.BigValueHelper(PortDevelopment.GetSupplyXp(info))));
				});
			}
			vector = base.Pos.XY + new Vector2(25f, 370f);
			this.{20636} = new Form(new Marker(ref vector, base.Pos.WH.X - 50f, 50f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(this.{20636});
			CS$<>8__locals1.continueBt.AllowMouseInput = false;
			CS$<>8__locals1.continueBt.SetText(Local.to_continue, Fonts.Philosopher_14, {17177}.textColor, false);
			CS$<>8__locals1.continueBt.EvClick += delegate(ClickUiEventArgs {20646})
			{
				CS$<>8__locals1.<>4__this.{20622}();
			};
			base.AddChild(CS$<>8__locals1.continueBt);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0007CC58 File Offset: 0x0007AE58
		private void {20620}(string {20621})
		{
			this.{20636}.ClearAllChild();
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_12, 0f);
			textBlockBuilder.WriteLines({20621}, {17177}.textColor, textBlockBuilder.defaultFont, 300f, new float?(0f));
			this.{20636}.AddChildPos(textBlockBuilder.CreateCentroid(), PositionAlignment.Center, PositionAlignment.LeftUp, 0f);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0007CCBC File Offset: 0x0007AEBC
		private void {20622}()
		{
			this.{20618}();
			base.SetData(Local.quanity, false, "", null, null, 0f, 1, true, delegate([TupleElementNames(new string[]
			{
				"resCount",
				"btIndex"
			})] ValueTuple<int, int> {20641})
			{
			}, true, null, null, 1, true, int.MaxValue, false, -1f);
			AnimatedButton animatedButton = new AnimatedButton(base.Pos.XY + new Vector2((float)(186 - {17177}.c_craftBr_Active.Width / 2), 439f), {17177}.c_craftBr_Passive, {17177}.c_craftBr_Passive, {17177}.c_craftBr_Active, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			animatedButton.AllowMouseInput = true;
			animatedButton.SetText(Local.to_continue, Fonts.Philosopher_14, {17177}.textColor, false);
			base.AddChild(animatedButton);
			this.{20635} = 1;
			int maxCount = this.{20634} ? Session.Guild.ConquerBadges : Session.Account.GetItemsCountInStorage(this.{20633});
			int cost = this.{20634} ? 30000 : PortDevelopment.GetSupplyXp(this.{20633});
			maxCount = Math.Min(maxCount, (int)Math.Ceiling((double)this.{20623}() / (double)cost));
			this.CreateQuantityBar(maxCount, delegate(float {20649})
			{
				this.{20635} = (int){20649};
				this.{20620}(Local.PortSupply_SelectItemQuantityDesc(StringHelper.BigValueHelper(this.{20635} * cost)));
			});
			StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(0f, 148f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.smp_addCondition(false, null, ref stackForm, this.{20634} ? CommonAtlas.Texture.Tex : this.{20633}.IconTexture, this.{20634} ? null : this.{20633}, this.{20634} ? CommonAtlas.conquerorBadgeIcon : this.{20633}.IconTexture.Bounds, this.{20634} ? Local.conquer_badges : this.{20633}.Name, () => maxCount, () => this.{20635}, 0);
			stackForm.Pos = stackForm.Pos.SetX(base.Pos.XY.X + (float)({17177}.c_main.Width / 2) - stackForm.Pos.WH.X / 2f);
			base.AddChild(stackForm);
			Vector2 vector = base.Pos.XY + new Vector2(25f, 370f);
			this.{20636} = new Form(new Marker(ref vector, base.Pos.WH.X - 50f, 50f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(this.{20636});
			this.{20620}(Local.PortSupply_SelectItemQuantityDesc(StringHelper.BigValueHelper(this.{20635} * cost)));
			animatedButton.EvClick += delegate(ClickUiEventArgs {20650})
			{
				this.{20635} = Math.Min(this.{20635}, maxCount);
				if (this.{20634})
				{
					if (!Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury)
					{
						new {17107}("", Local.GuildWindow_3(Local.guildright_tt_AllowEditGuildAndFortAndTreasury), "", null, true, null, new string[]
						{
							Local.close
						});
					}
					else
					{
						Global.Network.Send(new OnSupplyPort(this.{20635}));
					}
				}
				else
				{
					Global.Network.Send(new OnSupplyPort((int)this.{20633}.ID, this.{20635}));
				}
				this.BlockAndClose();
			};
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0007CFB8 File Offset: 0x0007B1B8
		private int {20623}()
		{
			int num = 0;
			for (int i = this.{20632}.PortLevel; i < this.{20632}.MaxLevel; i++)
			{
				num += this.{20632}.GetLevelCost(i + 1);
			}
			return num - this.{20632}.Xp;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0007D007 File Offset: 0x0007B207
		private {20614}.CustomSelectBar CreateQuantityBar(RTI {20624}, Action<float> {20625})
		{
			{20614}.CustomSelectBar customSelectBar = this.CreateCustomSelectBar(228f, Local.quanity, {20625}, true, 1f, (float){20624}.Value);
			customSelectBar.TextBox.IsEnter = true;
			return customSelectBar;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0007D034 File Offset: 0x0007B234
		private {20614}.CustomSelectBar CreateCustomSelectBar(float {20626}, string {20627}, Action<float> {20628}, bool {20629}, float {20630}, float {20631})
		{
			base.AddChild(new Label(base.Pos.XY + new Vector2(186f, {20626}), Fonts.Philosopher_14, {17177}.textColor * 0.8f, {20627}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			TextBox textBox = new TextBox(base.Pos.XY + new Vector2(53f, {20626} + 14f), {17177}.c_textBoxBlack, Fonts.Philosopher_14, Color.Black * 0.7f, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			textBox.Text = ({20629} ? Math.Round((double){20630}).ToString() : Math.Max(1f, {20630}).ToString());
			{20614}.CustomSelectBar bar = new {20614}.CustomSelectBar(textBox)
			{
				Min = {20630},
				Max = {20631},
				SelectBarControl = new ProgressSelectBar(base.Pos.XY + new Vector2(177f, {20626} + 18f), {17177}.c_progressSelect, {17177}.c_progressSelect, {17177}.c_progressSelectPoint, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					Value = 0f,
					MaxValue = Math.Max(1f, {20631} - {20630})
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
			textBox.EvTextChanged += delegate(string {20651})
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
			bar.SelectBarControl.EvChange += delegate(ProgressBarChangeEventArgs {20652})
			{
				float num = MathHelper.Clamp(bar.SelectBarControl.Value, 0f, bar.Max - bar.Min);
				float obj = {20629} ? MathF.Round(num + bar.Min) : MathF.Round(num + bar.Min, 1);
				blocker = true;
				textBox.FontColor = Color.Black * 0.7f;
				textBox.Text = obj.ToString();
				blocker = false;
				{20628}(obj);
			};
			return bar;
		}

		// Token: 0x04000DA1 RID: 3489
		private static {20614} Instance;

		// Token: 0x04000DA2 RID: 3490
		private const float GridHeight = 260f;

		// Token: 0x04000DA3 RID: 3491
		private readonly PortDevelopment {20632};

		// Token: 0x04000DA4 RID: 3492
		private ResourceInfo {20633};

		// Token: 0x04000DA5 RID: 3493
		private bool {20634};

		// Token: 0x04000DA6 RID: 3494
		private int {20635};

		// Token: 0x04000DA7 RID: 3495
		private Form {20636};

		// Token: 0x0200029F RID: 671
		private class CustomSelectBar
		{
			// Token: 0x06000ECC RID: 3788 RVA: 0x0007D245 File Offset: 0x0007B445
			public CustomSelectBar(TextBox {20638})
			{
				this.TextBox = {20638};
			}

			// Token: 0x06000ECD RID: 3789 RVA: 0x0007D254 File Offset: 0x0007B454
			public void UpdateMax(float {20639})
			{
				this.Max = {20639};
				this.SelectBarControl.MaxValue = this.Max - this.Min;
				int num;
				if (int.TryParse(this.TextBox.Text, out num) && (float)num > this.Max)
				{
					num = (int)this.Max;
					this.TextBox.Text = num.ToString();
				}
			}

			// Token: 0x04000DA8 RID: 3496
			public TextBox TextBox;

			// Token: 0x04000DA9 RID: 3497
			public ProgressSelectBar SelectBarControl;

			// Token: 0x04000DAA RID: 3498
			public float Max;

			// Token: 0x04000DAB RID: 3499
			public float Min;
		}
	}
}
