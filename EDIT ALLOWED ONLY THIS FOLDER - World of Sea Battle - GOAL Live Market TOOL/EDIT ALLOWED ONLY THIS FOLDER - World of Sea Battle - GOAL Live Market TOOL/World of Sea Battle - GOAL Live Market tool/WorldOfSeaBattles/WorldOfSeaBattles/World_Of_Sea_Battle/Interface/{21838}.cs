using System;
using System.Runtime.CompilerServices;
using Common;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200035D RID: 861
	internal sealed class {21838} : {21762}
	{
		// Token: 0x060012C5 RID: 4805 RVA: 0x0009F03C File Offset: 0x0009D23C
		public {21838}(string {21857}, string {21858}, Action<int> {21859}, int {21860}, Func<int, string> {21861} = null, int? {21862} = null, int? {21863} = null, [Nullable(2)] string {21864} = null, float? {21865} = null) : this((int {21878}) => {21857}, {21858}, {21859}, {21860}, {21861}, {21862}, {21863}, {21864}, {21865})
		{
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0009F078 File Offset: 0x0009D278
		public {21838}(Func<int, string> {21866}, string {21867}, Action<int> {21868}, int {21869}, Func<int, string> {21870} = null, int? {21871} = null, int? {21872} = null, [Nullable(2)] string {21873} = null, float? {21874} = null)
		{
			{21838}.<>c__DisplayClass4_0 CS$<>8__locals1 = new {21838}.<>c__DisplayClass4_0();
			CS$<>8__locals1.header = {21866};
			CS$<>8__locals1.buttonText = {21867};
			CS$<>8__locals1.complete = {21868};
			CS$<>8__locals1.extendedMaxCountMode = {21872};
			CS$<>8__locals1.additionalLiveLabel = {21870};
			CS$<>8__locals1.subheader = {21873};
			CS$<>8__locals1.itemMass = {21874};
			base..ctor();
			CS$<>8__locals1.<>4__this = this;
			int? extendedMaxCountMode = CS$<>8__locals1.extendedMaxCountMode;
			if (extendedMaxCountMode.GetValueOrDefault() == {21869} & extendedMaxCountMode != null)
			{
				CS$<>8__locals1.extendedMaxCountMode = null;
			}
			{21838}.CurrentInstance = this;
			int num = {21871} ?? Math.Max(1, {21869});
			this.{21876} = new RTI(num);
			this.{21877} = new RTI({21869});
			LiveLabel liveLabel = new LiveLabel(base.Pos.XY + new Vector2(14f, 8f), Fonts.Arial_12, Color.Wheat, () => CS$<>8__locals1.header(CS$<>8__locals1.<>4__this.{21876}.Value), 100);
			base.AddChild(liveLabel);
			if (CS$<>8__locals1.subheader != null)
			{
				base.AddChild(new Label(new Vector2(liveLabel.Pos.XY.X + liveLabel.PosWidth, liveLabel.Pos.XY.Y), Fonts.Arial_12, Color.DimGray, CS$<>8__locals1.subheader, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			Button fullButton = new Button(base.Pos.XY + {21762}.Downbar_Button1Position - new Vector2((float){21762}.Pattern_DownbarButton.Width, 0f), {21762}.Pattern_DownbarButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			fullButton.SetText((Global.Player.ResourcesOfHold.ComputeMass<ResourceInfo>() > Global.Player.UsedShip.Capacity) ? Local.pick_more : Local.pick_all, Fonts.Arial_12, Color.White, false);
			fullButton.IsVisible = false;
			fullButton.EvClick += delegate(ClickUiEventArgs {21879})
			{
				CS$<>8__locals1.<>4__this.RemoveFromContainer();
				Func<int, string> header = CS$<>8__locals1.header;
				string buttonText = CS$<>8__locals1.buttonText;
				Action<int> complete = CS$<>8__locals1.complete;
				int value = CS$<>8__locals1.extendedMaxCountMode.Value;
				Func<int, string> additionalLiveLabel = CS$<>8__locals1.additionalLiveLabel;
				int? {21871}2 = null;
				string subheader = CS$<>8__locals1.subheader;
				new {21838}(header, buttonText, complete, value, additionalLiveLabel, {21871}2, null, subheader, null);
			};
			base.WriteEmptyLine();
			base.AddNodeSwitcherToEnd(Local.PortNumerticInputBasicWindow_2, num, Math.Max(1, {21869}), delegate(int {21882})
			{
				CS$<>8__locals1.<>4__this.{21876}.Value = {21882};
				fullButton.IsVisible = ({21882} >= CS$<>8__locals1.<>4__this.{21877}.Value && CS$<>8__locals1.extendedMaxCountMode != null);
			}, true);
			this.acceptButton = new Button(base.Pos.XY + {21762}.Downbar_Button1Position, {21762}.Pattern_DownbarButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.acceptButton.SetText(CS$<>8__locals1.buttonText, Fonts.Arial_12, Color.White, false);
			this.acceptButton.EvClick += delegate(ClickUiEventArgs {21880})
			{
				if (CS$<>8__locals1.<>4__this.{21876}.Value <= CS$<>8__locals1.<>4__this.{21877}.Value)
				{
					if (CS$<>8__locals1.<>4__this.{21876}.Value > 0)
					{
						CS$<>8__locals1.complete(CS$<>8__locals1.<>4__this.{21876}.Value);
					}
					CS$<>8__locals1.<>4__this.BlockAndClose();
				}
			};
			base.AddChild(new UiControl[]
			{
				fullButton,
				this.acceptButton
			});
			if (CS$<>8__locals1.additionalLiveLabel != null)
			{
				LiveLabel liveLabel2 = new LiveLabel(base.Pos.XY + new Vector2(14f, 123f), Fonts.Arial_12, Color.Gold, delegate(LiveLabel {21881})
				{
					Color basicColor;
					if (CS$<>8__locals1.itemMass == null)
					{
						basicColor = ((CS$<>8__locals1.<>4__this.{21876}.Value > CS$<>8__locals1.<>4__this.{21877}.Value) ? Color.Orange : Color.Gold);
					}
					else
					{
						float? num2 = (float)CS$<>8__locals1.<>4__this.{21876}.Value * CS$<>8__locals1.itemMass;
						float freeCapacity = Global.Player.UsedShipPlayer.FreeCapacity;
						basicColor = ((num2.GetValueOrDefault() > freeCapacity & num2 != null) ? Color.Orange : Color.Gold);
					}
					{21881}.BasicColor = basicColor;
					CS$<>8__locals1.<>4__this.acceptButton.Opacity = ((CS$<>8__locals1.<>4__this.{21876}.Value > CS$<>8__locals1.<>4__this.{21877}.Value) ? 0.5f : 1f);
					return CS$<>8__locals1.additionalLiveLabel(CS$<>8__locals1.<>4__this.{21876}.Value);
				}, 50);
				if (liveLabel2.Pos.WH.X > 300f)
				{
					liveLabel2.Pos = liveLabel2.Pos.Offset(0f, -40f);
				}
				base.AddChild(liveLabel2);
			}
			base.EvRemoveFromContainer += delegate()
			{
				{21838}.CurrentInstance = null;
			};
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0009E4AB File Offset: 0x0009C6AB
		protected override void UserUpdate(ref FrameTime {21875})
		{
			base.UserUpdate(ref {21875});
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0009E4B4 File Offset: 0x0009C6B4
		protected override void UserBackRender()
		{
			base.UserBackRender();
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0009E4BC File Offset: 0x0009C6BC
		protected override void UserFrontRender()
		{
			base.UserFrontRender();
		}

		// Token: 0x04001117 RID: 4375
		public static {21838} CurrentInstance;

		// Token: 0x04001118 RID: 4376
		private RTI {21876};

		// Token: 0x04001119 RID: 4377
		private readonly RTI {21877};
	}
}
