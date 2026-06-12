using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Common;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002B4 RID: 692
	internal sealed class {20755}
	{
		// Token: 0x06000F37 RID: 3895 RVA: 0x000802FE File Offset: 0x0007E4FE
		public static void StartFitting(ShipDesignInfo {20759}, CustomUi {20760}, [Nullable(2)] {20755}.BuyButton {20761})
		{
			new {20755}({20759}, {20760}, {20761});
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0008030C File Offset: 0x0007E50C
		public {20755}(ShipDesignInfo {20762}, CustomUi {20763}, [Nullable(2)] {20755}.BuyButton {20764})
		{
			{20755}.<>c__DisplayClass4_0 CS$<>8__locals1 = new {20755}.<>c__DisplayClass4_0();
			CS$<>8__locals1.buy = {20764};
			CS$<>8__locals1.sourceUi = {20763};
			base..ctor();
			{20755}.CurrentInstance = this;
			if (CS$<>8__locals1.sourceUi == null)
			{
				return;
			}
			CS$<>8__locals1.sourceUi.IsVisible = false;
			CS$<>8__locals1.cancelMouse = !(CS$<>8__locals1.sourceUi is {20881});
			CS$<>8__locals1.turnedOff = false;
			if (CS$<>8__locals1.cancelMouse)
			{
				Global.Game.SceneGame.DecreaseMouse();
			}
			if ({20762}.Category == ShipDesignCategory.BowFigure && Global.Player.UsedShip.StaticInfo.BowFigurePosition.X == 0f)
			{
				new {17312}(Local.PortRealShopPage_71);
				return;
			}
			Global.Player.Client.ExampleDesigns.Add({20762});
			if ({20762}.Category == ShipDesignCategory.BowFigure || {20762}.Category == ShipDesignCategory.Flag || {20762}.Category == ShipDesignCategory.Satellite)
			{
				Global.Game.ScenePort.UpdateGuiForViewShip();
			}
			Global.Player.UpdateSailClotting();
			CS$<>8__locals1.referencedUi = null;
			{20755}.<>c__DisplayClass4_0 CS$<>8__locals2 = CS$<>8__locals1;
			Rectangle uiarea = Engine.GS.UIArea;
			CS$<>8__locals2.blocker = new Form(new Marker(ref uiarea), PositionAlignment.Both, PositionAlignment.Both);
			CS$<>8__locals1.buttons = new StackForm(new Vector2((float)(Engine.GS.UIArea.Width / 2), (float)(Engine.GS.UIArea.Height - 110)), UiOrientation.Horizontal, PositionAlignment.Center, PositionAlignment.RightDown);
			Button button = new Button(default(Vector2), AtlasPortGui.buttonBlueBack, PositionAlignment.Center, PositionAlignment.Center).SetText(Local.to_back, Fonts.Philosopher_14, Color.White * 0.7f, false);
			Button button2 = new Button(default(Vector2), {20755}.c_buyButton, PositionAlignment.Center, PositionAlignment.Center);
			if ({20762}.Category == ShipDesignCategory.ShipFullDesign && !Session.Account.Shipyard.ContainsInfo({20762}.AssociatedShip))
			{
				button2.AddChildPos(new Label(default(Vector2), Fonts.Philosopher_14, Color.Orange, {20762}.AssociatedShip.ShipName, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, -20f);
			}
			button.Pos = button.Pos.ScaleWidth(1.1f);
			button2.Pos = button2.Pos.ScaleWidth(1.1f);
			CS$<>8__locals1.buttons.AddItem(new UiControl[]
			{
				button
			});
			CS$<>8__locals1.buttons.AddSpace(5f);
			CS$<>8__locals1.buttons.AddItem(new UiControl[]
			{
				button2
			});
			CS$<>8__locals1.buttons.Pos = CS$<>8__locals1.buttons.Pos.Offset(-CS$<>8__locals1.buttons.Pos.WH.X / 2f, 0f);
			CS$<>8__locals1.toolTip = TextBlockBuilder.CreateBlock(500f, ({20762}.Category == ShipDesignCategory.ShipFullDesign) ? Local.design_shipDesignInfo : (({20762}.Category == ShipDesignCategory.BowFigure || {20762}.Category == ShipDesignCategory.Satellite) ? Local.design_bowFigureInfo : (({20762}.Category == ShipDesignCategory.Decal1 || {20762}.Category == ShipDesignCategory.Decal2) ? Local.design_decalInfo : Local.design_designInfo)), Color.LightGray, Fonts.Philosopher_14, -1f).CreateCentroid(new Vector2((float)(Engine.GS.UIArea.Width / 2), (float)(Engine.GS.UIArea.Height - 70)));
			CS$<>8__locals1.toolTip.ShadowingAmount = 0.4f;
			CS$<>8__locals1.toolTip.PositionAlignment_X = CS$<>8__locals1.buttons.PositionAlignment_X;
			CS$<>8__locals1.toolTip.PositionAlignment_Y = CS$<>8__locals1.buttons.PositionAlignment_Y;
			CS$<>8__locals1.rollbackBowFigure = Global.Player.UsedShipPlayer.BowFigurePosition;
			CS$<>8__locals1.rollbackBigLamp = Global.Player.UsedShipPlayer.BigLampPosition;
			ShipDesignCategory category = {20762}.Category;
			bool flag = category == ShipDesignCategory.Satellite || category == ShipDesignCategory.BowFigure;
			if (flag)
			{
				CS$<>8__locals1.referencedUi = {22409}.Create({20762}.Category, false);
			}
			if ({20762}.Category == ShipDesignCategory.ShipFullDesign)
			{
				Global.Player.PreviewShip = PlayerShipDynamicInfo.CreateNewFromShipInfo({20762}.AssociatedShip, true);
				Global.Player.ForceUpdateShipEffects();
			}
			CS$<>8__locals1.sourceUi.RemoveWithThis(new UiControl[]
			{
				CS$<>8__locals1.buttons
			});
			CS$<>8__locals1.sourceUi.RemoveWithThis(new UiControl[]
			{
				CS$<>8__locals1.toolTip
			});
			CS$<>8__locals1.sourceUi.RemoveWithThis(new UiControl[]
			{
				CS$<>8__locals1.blocker
			});
			if (CS$<>8__locals1.buy == null || !CS$<>8__locals1.buy.Enable)
			{
				button2.SetText((CS$<>8__locals1.buy == null) ? Local.shop : CS$<>8__locals1.buy.Text, Fonts.Philosopher_14, Color.Gray, false);
				button2.Brightness = 0.5f;
				button2.AllowMouseInput = false;
			}
			else
			{
				button2.SetText(CS$<>8__locals1.buy.Text, Fonts.Philosopher_14, Color.White, false);
				button2.EvClick += delegate(ClickUiEventArgs {20789})
				{
					base.<.ctor>g__TurnOff|3();
					CS$<>8__locals1.buy.Click();
				};
			}
			CS$<>8__locals1.sourceUi.EvRemoveFromContainer += delegate()
			{
				base.<.ctor>g__TurnOff|3();
			};
			button.EvClick += delegate(ClickUiEventArgs {20790})
			{
				base.<.ctor>g__TurnOff|3();
				Global.Player.UsedShipPlayer.BowFigurePosition = CS$<>8__locals1.rollbackBowFigure;
				Global.Player.UsedShipPlayer.BigLampPosition = CS$<>8__locals1.rollbackBigLamp;
			};
		}

		// Token: 0x04000E07 RID: 3591
		public static {20755} CurrentInstance;

		// Token: 0x04000E08 RID: 3592
		public static readonly Rectangle c_buyButton = new Rectangle(2644, 684, 161, 39);

		// Token: 0x020002B5 RID: 693
		public class BuyButton : IEquatable<{20755}.BuyButton>
		{
			// Token: 0x06000F3A RID: 3898 RVA: 0x00080826 File Offset: 0x0007EA26
			public BuyButton(string {20769}, bool {20770}, Action {20771})
			{
				this.Text = {20769};
				this.Enable = {20770};
				this.Click = {20771};
				base..ctor();
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x06000F3B RID: 3899 RVA: 0x00080843 File Offset: 0x0007EA43
			[Nullable(1)]
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[NullableContext(1)]
				[CompilerGenerated]
				get
				{
					return typeof({20755}.BuyButton);
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0008084F File Offset: 0x0007EA4F
			// (set) Token: 0x06000F3D RID: 3901 RVA: 0x00080857 File Offset: 0x0007EA57
			public string Text { get; set; }

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00080860 File Offset: 0x0007EA60
			// (set) Token: 0x06000F3F RID: 3903 RVA: 0x00080868 File Offset: 0x0007EA68
			public bool Enable { get; set; }

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00080871 File Offset: 0x0007EA71
			// (set) Token: 0x06000F41 RID: 3905 RVA: 0x00080879 File Offset: 0x0007EA79
			public Action Click { get; set; }

			// Token: 0x06000F42 RID: 3906 RVA: 0x00080884 File Offset: 0x0007EA84
			[NullableContext(1)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("BuyButton");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x06000F43 RID: 3907 RVA: 0x000808D0 File Offset: 0x0007EAD0
			[NullableContext(1)]
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder {20775})
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				{20775}.Append("Text = ");
				{20775}.Append(this.Text);
				{20775}.Append(", Enable = ");
				{20775}.Append(this.Enable.ToString());
				{20775}.Append(", Click = ");
				{20775}.Append(this.Click);
				return true;
			}

			// Token: 0x06000F44 RID: 3908 RVA: 0x0008093C File Offset: 0x0007EB3C
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=({20755}.BuyButton {20776}, {20755}.BuyButton {20777})
			{
				return !({20776} == {20777});
			}

			// Token: 0x06000F45 RID: 3909 RVA: 0x00080948 File Offset: 0x0007EB48
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==({20755}.BuyButton {20778}, {20755}.BuyButton {20779})
			{
				return {20778} == {20779} || ({20778} != null && {20778}.Equals({20779}));
			}

			// Token: 0x06000F46 RID: 3910 RVA: 0x0008095C File Offset: 0x0007EB5C
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.{20786})) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.{20787})) * -1521134295 + EqualityComparer<Action>.Default.GetHashCode(this.{20788});
			}

			// Token: 0x06000F47 RID: 3911 RVA: 0x000809BE File Offset: 0x0007EBBE
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object {20780})
			{
				return this.Equals({20780} as {20755}.BuyButton);
			}

			// Token: 0x06000F48 RID: 3912 RVA: 0x000809CC File Offset: 0x0007EBCC
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals({20755}.BuyButton {20781})
			{
				return this == {20781} || ({20781} != null && this.EqualityContract == {20781}.EqualityContract && EqualityComparer<string>.Default.Equals(this.{20786}, {20781}.{20786}) && EqualityComparer<bool>.Default.Equals(this.{20787}, {20781}.{20787}) && EqualityComparer<Action>.Default.Equals(this.{20788}, {20781}.{20788}));
			}

			// Token: 0x06000F4A RID: 3914 RVA: 0x00080A45 File Offset: 0x0007EC45
			[CompilerGenerated]
			protected BuyButton([Nullable(1)] {20755}.BuyButton {20782})
			{
				this.Text = {20782}.{20786};
				this.Enable = {20782}.{20787};
				this.Click = {20782}.{20788};
			}

			// Token: 0x06000F4B RID: 3915 RVA: 0x00080A71 File Offset: 0x0007EC71
			[CompilerGenerated]
			public void Deconstruct(out string {20783}, out bool {20784}, out Action {20785})
			{
				{20783} = this.Text;
				{20784} = this.Enable;
				{20785} = this.Click;
			}

			// Token: 0x04000E09 RID: 3593
			[CompilerGenerated]
			private readonly string {20786};

			// Token: 0x04000E0A RID: 3594
			[CompilerGenerated]
			private readonly bool {20787};

			// Token: 0x04000E0B RID: 3595
			[CompilerGenerated]
			private readonly Action {20788};
		}
	}
}
