using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
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
	// Token: 0x020002C3 RID: 707
	internal class {20856} : {17625}
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x000070D7 File Offset: 0x000052D7
		protected virtual bool makeBackGray
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00082C50 File Offset: 0x00080E50
		protected static Form CreateHeadForm(Vector2 {20858}, string {20859}, string {20860} = null)
		{
			Form form = new Form(new Marker({20858}.X, {20858}.Y, (float)({20856}.contentWidth - 1), 48f), AtlasPortGui.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.BasicColor = Color.Black * 0.6f;
			form.AnimatedFocus = false;
			Label label = new Label(form.Pos.XY + new Vector2(14f, 12f), Fonts.Philosopher_14, Color.White * 0.33f, {20859}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if ({20859} == Local.craft_comp && Session.Account.IsEducationInProgress(EducationOnboarding.OpenSpecificWorkshop, false, false))
			{
				label.BasicColor = Color.SoftLime;
			}
			form.AddChild(label);
			if (!string.IsNullOrEmpty({20860}))
			{
				form.AddChild(new Label(label.Pos.XY + new Vector2(label.Pos.WH.X + 5f, 0f), Fonts.Philosopher_14, Color.White * 0.6f, {20860}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			form.AddChild(new Label(form.Pos.XY + new Vector2(-8f, 7f), Fonts.Philosopher_24, Color.White * 0.3f, "◈", PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			return form;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00082DAF File Offset: 0x00080FAF
		protected static int contentWidth
		{
			get
			{
				return 966;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00082DB6 File Offset: 0x00080FB6
		protected static int summaryWidth
		{
			get
			{
				return 980;
			}
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00082DC0 File Offset: 0x00080FC0
		public {20856}(params {20856}.UserContents[] {20861}) : base(965f, {17625}.c_back1, {17604}.PortPage, {17625}.c_icAnchor, (from {20877} in {20861}
		select new {17625}.DynamicTittle({20877}.Tittle)).ToArray<{17625}.DynamicTittle>())
		{
			base.ComposeDynamicTab(AtlasPortGui.Texture.Tex, (from {20871} in {20861}
			select delegate(StackForm {20878})
			{
				this.{20862}({20878}, {20871});
			}).ToArray<Action<StackForm>>());
			base.EvRemoveFromContainer += this.{20872};
			new UiOpacityAnimation(this, 0f, 1f, 190f);
			{19970} currentInstance = {19970}.CurrentInstance;
			if (currentInstance == null)
			{
				return;
			}
			currentInstance.MoveToFrontLevel();
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00082E6F File Offset: 0x0008106F
		private void {20862}(StackForm {20863}, {20856}.UserContents {20864})
		{
			{20864}.AddContent({20863});
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00082E7D File Offset: 0x0008107D
		protected void Refresh()
		{
			Action refreshCurrentDynamicTabPage = this.RefreshCurrentDynamicTabPage;
			if (refreshCurrentDynamicTabPage == null)
			{
				return;
			}
			refreshCurrentDynamicTabPage();
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00082E90 File Offset: 0x00081090
		public static Form GenericItemForm(string {20865}, Func<string> {20866}, ImageDecription {20867}, {20856}.GenericColor {20868}, Action {20869}, Color? {20870} = null)
		{
			Form form = new Form(Vector2.Zero, {20856}.Pattern_Item3RowsGeneric, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChild(new Form(new Vector2(66f, 7f), new Rectangle(2801, 329, 213, 29), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
			form.BasicColor = Color.White * 0.9f;
			if ({20868} != {20856}.GenericColor.No)
			{
				form.AddChild(new Form(new Vector2(66f, 7f), ({20868} == {20856}.GenericColor.Yellow) ? new Rectangle(2801, 299, 213, 29) : (({20868} == {20856}.GenericColor.Cyan) ? new Rectangle(2801, 269, 213, 29) : new Rectangle(2801, 239, 213, 29)), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				});
			}
			form.AddChild(new Image(new Marker(7f, 5f, 60f, 60f), {20867}.Tex, {20867}.Path, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Label(new Vector2(74f, 11f), Fonts.Philosopher_14, {20870} ?? (Color.Wheat * 0.7f), {20865}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			if ({20866} != null)
			{
				form.AddChild(new LiveLabel(new Vector2(74f, 41f), Fonts.Arial_12, Color.Gray, (LiveLabel {20879}) => {20866}(), 100));
			}
			if ({20869} != null)
			{
				form.EvClick += delegate(ClickUiEventArgs {20880})
				{
					{20869}();
				};
			}
			return form;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00083129 File Offset: 0x00081329
		[CompilerGenerated]
		private void {20872}()
		{
			if (this.IsClosedByHand)
			{
				Global.Game.ScenePort.mainHandler(null);
			}
		}

		// Token: 0x04000E4A RID: 3658
		public static readonly Rectangle Pattern_Item5Rows_Cannon = new Rectangle(2728, 518, 192, 71);

		// Token: 0x04000E4B RID: 3659
		public static readonly Rectangle Pattern_Item3Rows = new Rectangle(2921, 518, 322, 71);

		// Token: 0x04000E4C RID: 3660
		public static readonly Rectangle Pattern_Item3RowsGeneric = new Rectangle(2800, 359, 322, 71);

		// Token: 0x04000E4D RID: 3661
		public static readonly Rectangle Pattern_ItemCard = new Rectangle(2637, 229, 159, 179);

		// Token: 0x04000E4E RID: 3662
		public static readonly Rectangle Pattern_ItemCard_corners = new Rectangle(2637, 185, 159, 43);

		// Token: 0x04000E4F RID: 3663
		public static readonly Marker Pattern_ItemIcon = new Marker(7f, 5f, 60f, 60f);

		// Token: 0x020002C4 RID: 708
		public struct UserContents
		{
			// Token: 0x06000FA2 RID: 4002 RVA: 0x00083143 File Offset: 0x00081343
			public UserContents(string {20875}, Action<StackForm> {20876})
			{
				this.Tittle = {20875};
				this.AddContent = {20876};
			}

			// Token: 0x04000E50 RID: 3664
			public string Tittle;

			// Token: 0x04000E51 RID: 3665
			public Action<StackForm> AddContent;
		}

		// Token: 0x020002C5 RID: 709
		public enum GenericColor
		{
			// Token: 0x04000E53 RID: 3667
			No,
			// Token: 0x04000E54 RID: 3668
			Yellow,
			// Token: 0x04000E55 RID: 3669
			Cyan,
			// Token: 0x04000E56 RID: 3670
			LightLime
		}
	}
}
