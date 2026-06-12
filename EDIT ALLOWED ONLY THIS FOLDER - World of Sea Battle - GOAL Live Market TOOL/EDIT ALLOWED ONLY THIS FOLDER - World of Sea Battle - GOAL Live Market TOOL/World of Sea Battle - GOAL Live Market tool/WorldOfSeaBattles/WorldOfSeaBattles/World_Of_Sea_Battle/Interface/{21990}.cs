using System;
using Common;
using Common.Account;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000377 RID: 887
	internal sealed class {21990} : {17625}
	{
		// Token: 0x0600132C RID: 4908 RVA: 0x000A2B74 File Offset: 0x000A0D74
		public {21990}(Action {21992}) : base((float){21949}.c_path.Width * 1.1f, {21949}.c_path, {17604}.InGameWindowWithoutDeco, Rectangle.Empty, Array.Empty<{17625}.DynamicTittle>())
		{
			{21990} <>4__this = this;
			base.Pos = base.Pos.SetHeight(360f);
			base.Pos = base.Pos.Offset(0f, -90f);
			base.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_16, Color.White * 0.8f, Local.PortSelectFlagsWindow_1 + ":", PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 15f);
			this.{21993}();
			base.AddChildPos(new Button(Vector2.Zero, {17625}.c_btLight_big, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.to_continue, Fonts.Philosopher_14, Color.Black, false).ExClick(delegate(ClickUiEventArgs {21997})
			{
				<>4__this.BlockAndClose();
			}), PositionAlignment.Center, PositionAlignment.RightDown, 15f);
			base.EvRemoveFromContainer += delegate()
			{
				if (<>4__this.IsClosedByHand)
				{
					{21992}();
				}
			};
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x000A2C94 File Offset: 0x000A0E94
		private void {21993}()
		{
			StackForm stackForm = this.{21996};
			if (stackForm != null)
			{
				stackForm.RemoveFromContainer();
			}
			this.{21996} = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{21996}.BorderThickness = 2f;
			this.{21996}.AddItem(new UiControl[]
			{
				this.{21994}(OpenWorldFlag.Peaceful)
			});
			this.{21996}.AddItem(new UiControl[]
			{
				this.{21994}(OpenWorldFlag.Trader)
			});
			this.{21996}.AddItem(new UiControl[]
			{
				this.{21994}(OpenWorldFlag.Pirate)
			});
			base.AddChildPos(this.{21996}, PositionAlignment.Center, PositionAlignment.LeftUp, 65f);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x000A2D3C File Offset: 0x000A0F3C
		private Form {21994}(OpenWorldFlag {21995})
		{
			{21990}.<>c__DisplayClass3_0 CS$<>8__locals1 = new {21990}.<>c__DisplayClass3_0();
			CS$<>8__locals1.flag = {21995};
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.form = new Form(new Marker(0f, 0f, 170f, 235f), {17625}.c_verticalCardGray, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Color {13344} = Color.Gray;
			if (CS$<>8__locals1.flag == Session.Account.WorldFlag)
			{
				CS$<>8__locals1.form.TexturePath = {17625}.c_verticalCard;
				CS$<>8__locals1.form.AddChild(new Form(CS$<>8__locals1.form.Pos, {17625}.c_verticalCard, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				});
				{13344} = Color.White;
			}
			CS$<>8__locals1.form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, {13344}, CS$<>8__locals1.flag.ToStringLocalFull(), PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 13f);
			CS$<>8__locals1.form.AddChildPos(new Form(new Marker(0f, 0f, CS$<>8__locals1.form.Pos.WH.X - 6f, 52f), CommonAtlas.GetWorldFlagPrerender(CS$<>8__locals1.flag, FractionID.None), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			}, PositionAlignment.Center, PositionAlignment.LeftUp, 36f);
			CS$<>8__locals1.tb = new TextBlockBuilder(Fonts.Arial_10, 1f);
			if (CS$<>8__locals1.flag == OpenWorldFlag.Pirate)
			{
				CS$<>8__locals1.<CreateFlagCard>g__AppendHelper|0(Local.educ_no_pvp, Color.Pink);
			}
			foreach (string text in {21949}.GetFeatures(CS$<>8__locals1.flag, true))
			{
				CS$<>8__locals1.<CreateFlagCard>g__AppendHelper|0(text, ((CS$<>8__locals1.flag == OpenWorldFlag.Trader && text == Local.flag_trader_1) ? new Color(230, 232, 148) : new Color(128, 254, 116)) * 0.70980394f);
			}
			CS$<>8__locals1.form.AddChild(CS$<>8__locals1.tb.CreateCentroid(new Vector2(CS$<>8__locals1.form.Pos.Center.X, 96f)));
			if (CS$<>8__locals1.flag != Session.Account.WorldFlag)
			{
				CS$<>8__locals1.form.AddChild(new AnimatedButton(CS$<>8__locals1.form.Pos, Rectangle.Empty, Rectangle.Empty, new Rectangle(813, 3573, 67, 92), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				CS$<>8__locals1.form.EvClick += delegate(ClickUiEventArgs {22000})
				{
					Session.Account.WorldFlag = CS$<>8__locals1.flag;
					Session.Account.IsPeaceActivated = false;
					EducationHelper.MakeEducationFlagWhenQuestActive(EducationOnboarding.ChangeFlags, true);
					new UiActor({22000}.Sender, new Action(CS$<>8__locals1.<>4__this.{21993}));
				};
			}
			return CS$<>8__locals1.form;
		}

		// Token: 0x04001175 RID: 4469
		private StackForm {21996};
	}
}
