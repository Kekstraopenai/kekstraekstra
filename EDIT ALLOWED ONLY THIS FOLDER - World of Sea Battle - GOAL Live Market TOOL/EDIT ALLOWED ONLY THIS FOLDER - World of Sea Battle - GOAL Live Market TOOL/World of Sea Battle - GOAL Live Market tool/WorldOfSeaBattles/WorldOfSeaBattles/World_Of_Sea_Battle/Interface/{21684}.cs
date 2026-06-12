using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200034A RID: 842
	internal abstract class {21684} : {17068}
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x0009AE0B File Offset: 0x0009900B
		protected virtual float MainListMaxHeight
		{
			get
			{
				return 530f;
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0009AE14 File Offset: 0x00099014
		public {21684}(string {21688}, string {21689}, string {21690})
		{
			Vector2 vector = new Vector2(0f, (float)(Engine.GS.UIArea.Height / 2 - {21684}.main.Height / 2));
			base..ctor(new Marker(ref vector, ref {21684}.main), {21684}.main, {17068}.BlockingWay.BackgroundClosingTransparentNoInputChaning, false);
			this.background.BasicColor = Color.Transparent;
			base.PositionAlignment_X = PositionAlignment.LeftUp;
			base.PositionAlignment_Y = PositionAlignment.Center;
			new UiMarkerAndOpacityAnimation(this, 0f, 1f, base.Pos.Offset(-50f, 0f), base.Pos, 500f, UiAmimationCurve.SquaredToBegin);
			this.{21707} = new Label(base.Pos.XY + {21684}.p_tittle, this.{21706}, Color.LightGray, {21688}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(new UiControl[]
			{
				this.{21707}.Center(),
				new Label(base.Pos.XY + {21684}.p_info - new Vector2(0f, 13f), Fonts.Arial_10, Color.Gray, {21689}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center(),
				new Label(base.Pos.XY + {21684}.p_info, Fonts.Arial_10, Color.Gray, {21690}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center()
			});
			base.AddChild(this.{21708} = new StackForm(base.Pos.XY + {21684}.p_ItemsStart, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			this.UpdateBlocks(true);
			GameScene.IncreaseGameInput();
			base.EvRemoveFromContainer += this.{21702};
		}

		// Token: 0x06001260 RID: 4704
		protected abstract IEnumerable<UiControl> CreateDesignComponents();

		// Token: 0x06001261 RID: 4705 RVA: 0x0009AFC6 File Offset: 0x000991C6
		protected virtual float GetMainListHeight()
		{
			return 581f;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0009AFD0 File Offset: 0x000991D0
		public void UpdateBlocks(bool {21691})
		{
			this.{21708}.Clear();
			List<UiControl> list = new List<UiControl>(this.CreateDesignComponents());
			float num = list.Sum((UiControl {21714}) => {21714}.Pos.WH.Y);
			float {21697} = Math.Min(1f, 3f / (float)list.Count);
			int num2 = 0;
			foreach (UiControl uiControl in list)
			{
				Form form = uiControl as Form;
				if (form != null)
				{
					form.AnimatedFocus = false;
					AnimatedButton animatedButton = new AnimatedButton(form.Pos, Rectangle.Empty, Rectangle.Empty, {21684}.c_itemTop, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						RenderToDepthMap = false,
						DisableDepthFocusTest = true
					};
					form.AddChild(animatedButton);
					animatedButton.MoveToBackLevel();
				}
			}
			if (num > this.MainListMaxHeight)
			{
				ScrollBarControl scrollBarControl = this.{21710};
				float currentScrollFactor = (scrollBarControl != null) ? scrollBarControl.CurrentScrollFactor : 0f;
				this.{21710} = this.CreateMainFormScroll();
				ListItemViewControl listItemViewControl = new ListItemViewControl(new Marker(0f, 0f, 475f, this.GetMainListHeight()), this.{21710}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				foreach (UiControl uiControl2 in list)
				{
					listItemViewControl.AddItem(new UiControl[]
					{
						uiControl2
					});
					if ({21691})
					{
						this.{21694}(uiControl2, num2, {21697});
					}
					num2++;
				}
				StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					DisableDepthFocusTest = true
				};
				stackForm.AddItem(new UiControl[]
				{
					listItemViewControl
				});
				stackForm.AddItemWithoutChangePosition(this.{21710});
				this.{21708}.AddItem(new UiControl[]
				{
					stackForm
				});
				this.{21710}.CurrentScrollFactor = currentScrollFactor;
				return;
			}
			foreach (UiControl uiControl3 in list)
			{
				this.{21708}.AddItem(new UiControl[]
				{
					uiControl3
				});
				if ({21691})
				{
					this.{21694}(uiControl3, num2, {21697});
				}
				num2++;
			}
			this.{21710} = null;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0009B234 File Offset: 0x00099434
		public void UpdateTitleLabel(string {21692}, Color? {21693} = null)
		{
			Color value = {21693}.GetValueOrDefault();
			if ({21693} == null)
			{
				value = {21684}.DefaultTitleColor;
				{21693} = new Color?(value);
			}
			this.{21707}.Text = {21692};
			this.{21707}.BasicColor = {21693}.Value;
			Vector2 vector = base.Pos.XY + {21684}.p_tittle;
			Vector2 vector2 = this.{21706}.Measure({21692});
			Vector2 vector3 = new Vector2(vector2.X, this.{21707}.Pos.WH.Y);
			UiControl uiControl = this.{21707};
			Vector2 vector4 = new Vector2(vector.X - vector3.X / 2f, vector.Y - vector3.Y / 2f);
			uiControl.Pos = new Marker(ref vector4, ref vector3);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0009B304 File Offset: 0x00099504
		private void {21694}(UiControl {21695}, int {21696}, float {21697})
		{
			new UiActionsSleep({21695}, 400f + (float)({21696} * 300) * {21697});
			new UiOpacityAnimation({21695}, 0f, {21695}.Opacity, 300f);
			{21695}.Opacity = 0f;
			if ({21695}.Brightness == 1f)
			{
				new UiBrightnessAnimation({21695}, 1f, 2f, 200f);
				new UiBrightnessAnimation({21695}, 2f, 1f, 300f);
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0009B383 File Offset: 0x00099583
		protected virtual ScrollBarControl CreateMainFormScroll()
		{
			return new ScrollBarControl(new Marker(0f, 0f, 1f, 1f), AtlasPortGui.transpPixel, AtlasPortGui.transpPixel, AtlasPortGui.transpPixel, AtlasPortGui.transpPixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0009B3BA File Offset: 0x000995BA
		protected virtual IEnumerable<UiControl> CreateElementsToSelection(object {21698})
		{
			return new {21684}.<CreateElementsToSelection>d__47(-2);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00017097 File Offset: 0x00015297
		protected virtual UiControl GetHeadForSelectionForm(object {21699})
		{
			return null;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0009B3C4 File Offset: 0x000995C4
		public void OpenSelectionForm(object {21700})
		{
			if (this.IsSelectionFormOpened)
			{
				return;
			}
			this.{21711} = {21700};
			int num = 660;
			int num2 = {21684}.c_selectMenuItem.Width;
			if (this.TwoColumnsSelectionForm)
			{
				num2 = (int)((float)num2 * 1.75f);
			}
			Vector2 vector = new Vector2(base.Pos.XY.X + base.Pos.WH.X - 10f, base.Pos.XY.Y + 7f);
			Rectangle uiarea = Engine.GS.UIArea;
			this.{21713} = new Form(new Marker(ref uiarea), PositionAlignment.Both, PositionAlignment.Both);
			this.{21712} = new Form(new Marker(vector.X, vector.Y, (float)(num2 + AtlasPortGui.scrollBar_Up.Width), (float)num), AtlasPortGui.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false,
				BasicColor = new Color(24, 25, 26) * 0.88f
			};
			this.RefreshSelectionList({21700});
			this.{21713}.EvClick += this.{21703};
			this.{21713}.EvRemoveFromContainer += this.{21705};
			this.IsSelectionFormOpened = true;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0009B4F8 File Offset: 0x000996F8
		protected void RefreshSelectionList(object {21701})
		{
			this.{21712}.GetChildren.RemoveAll((UiControl {21715}) => {21715}.GetType() == typeof(ListItemViewControl));
			ListItemViewControl listItemViewControl = null;
			if (this.HaveFilter)
			{
				this.{21709} = new ScrollBarControl(new Marker(this.{21712}.Pos.XY.X + this.{21712}.Pos.WH.X - (float)AtlasPortGui.scrollBar_Down.Width, this.{21712}.Pos.XY.Y + 30f, (float)AtlasPortGui.scrollBar_Down.Width, this.{21712}.Pos.WH.Y - 30f), AtlasPortGui.scrollBar_Up, AtlasPortGui.scrollBar_Down, AtlasPortGui.scrollBar_Pointer, AtlasPortGui.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Marker pos = this.{21712}.Pos;
				Vector2 vector = new Vector2(0f, 30f);
				listItemViewControl = new ListItemViewControl(pos + vector, this.{21709}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				UiControl uiControl = listItemViewControl;
				pos = listItemViewControl.Pos;
				uiControl.Pos = pos.SetHeight(listItemViewControl.Pos.WH.Y - 30f);
				StackForm stackForm = new StackForm(this.{21712}.Pos.XY + new Vector2(6f, 3f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.Center);
				using (Dictionary<string, object>.Enumerator enumerator = this.FilterMenu.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, object> keyValuePair = enumerator.Current;
						stackForm.AddItem(new UiControl[]
						{
							new LabelButton(Vector2.Zero, keyValuePair.Key, Fonts.Philosopher_16Bold, keyValuePair.Value.Equals({21701}) ? Color.White : Color.Wheat, Color.White, delegate(ClickUiEventArgs {21716})
							{
								this.CloseAllSelectionForm();
								this.OpenSelectionForm(keyValuePair.Value);
							})
						});
						stackForm.AddSpace(30f);
					}
				}
				this.{21712}.AddChild(stackForm);
			}
			else if (this.HaveSearchBar)
			{
				this.{21709} = new ScrollBarControl(new Marker(this.{21712}.Pos.XY.X + this.{21712}.Pos.WH.X - (float)AtlasPortGui.scrollBar_Down.Width, this.{21712}.Pos.XY.Y + 30f, (float)AtlasPortGui.scrollBar_Down.Width, this.{21712}.Pos.WH.Y - 30f), AtlasPortGui.scrollBar_Up, AtlasPortGui.scrollBar_Down, AtlasPortGui.scrollBar_Pointer, AtlasPortGui.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Marker pos = this.{21712}.Pos;
				Vector2 vector = new Vector2(0f, 30f);
				listItemViewControl = new ListItemViewControl(pos + vector, this.{21709}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				UiControl uiControl2 = listItemViewControl;
				pos = listItemViewControl.Pos;
				uiControl2.Pos = pos.SetHeight(listItemViewControl.Pos.WH.Y - 30f);
				new TextBlockBuilder(Fonts.Arial_12, 0f);
				this.{21712}.Pos.XY + new Vector2(6f, 3f);
				this.SearchBar = new TextBox(this.{21712}.Pos.XY + new Vector2(6f, 3f), new Rectangle(0, 240, 316, 25), Fonts.Arial_12, Color.White, new Rectangle(489, 15, 1, 1), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				this.SearchBar.DefaultText = Local.search;
				this.SearchBar.EvTextChanged += this.OnSearchEnter;
				this.{21712}.AddChild(this.SearchBar);
			}
			else
			{
				this.{21709} = new ScrollBarControl(new Marker(this.{21712}.Pos.XY.X + this.{21712}.Pos.WH.X - (float)AtlasPortGui.scrollBar_Down.Width, this.{21712}.Pos.XY.Y, (float)AtlasPortGui.scrollBar_Down.Width, this.{21712}.Pos.WH.Y), AtlasPortGui.scrollBar_Up, AtlasPortGui.scrollBar_Down, AtlasPortGui.scrollBar_Pointer, AtlasPortGui.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				listItemViewControl = new ListItemViewControl(this.{21712}.Pos, this.{21709}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			}
			UiControl headForSelectionForm = this.GetHeadForSelectionForm({21701});
			if (headForSelectionForm != null)
			{
				listItemViewControl.AddItem(new UiControl[]
				{
					headForSelectionForm
				});
			}
			if (this.TwoColumnsSelectionForm)
			{
				BlocksStackFormControl blocksStackFormControl = new BlocksStackFormControl(Vector2.Zero, 3, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				foreach (UiControl uiControl3 in this.CreateElementsToSelection({21701}))
				{
					blocksStackFormControl.AddItem(new UiControl[]
					{
						uiControl3
					});
				}
				listItemViewControl.AddItem(new UiControl[]
				{
					blocksStackFormControl
				});
			}
			else
			{
				foreach (UiControl uiControl4 in this.CreateElementsToSelection({21701}))
				{
					listItemViewControl.AddItem(new UiControl[]
					{
						uiControl4
					});
				}
			}
			this.{21712}.AddChild(new UiControl[]
			{
				listItemViewControl,
				this.{21709}
			});
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0009BAB8 File Offset: 0x00099CB8
		protected void CloseAllSelectionForm()
		{
			Form form = this.{21712};
			if (form != null)
			{
				form.RemoveFromContainer();
			}
			Form form2 = this.{21713};
			if (form2 != null)
			{
				form2.RemoveFromContainer();
			}
			this.{21712} = (this.{21713} = null);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0009BAF8 File Offset: 0x00099CF8
		protected void ReopenSelectionForm()
		{
			float currentScrollFactor = this.{21709}.CurrentScrollFactor;
			this.CloseAllSelectionForm();
			this.OpenSelectionForm(this.{21711});
			this.{21709}.CurrentScrollFactor = currentScrollFactor;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0009BD83 File Offset: 0x00099F83
		[CompilerGenerated]
		private void {21702}()
		{
			GameScene.DecreaseGameInput();
			this.CloseAllSelectionForm();
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0009BD90 File Offset: 0x00099F90
		[CompilerGenerated]
		private void {21703}(ClickUiEventArgs {21704})
		{
			{21704}.DoNotHandling = true;
			this.{21712}.RemoveFromContainer();
			this.{21713}.RemoveFromContainer();
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0009BDAF File Offset: 0x00099FAF
		[CompilerGenerated]
		private void {21705}()
		{
			this.IsSelectionFormOpened = false;
		}

		// Token: 0x0400109F RID: 4255
		public static readonly Rectangle main = new Rectangle(0, 260, 500, 680);

		// Token: 0x040010A0 RID: 4256
		public static readonly Rectangle c_item = new Rectangle(1505, 1085, 477, 159);

		// Token: 0x040010A1 RID: 4257
		public static readonly Rectangle c_itemExpansion = new Rectangle(793, 1417, 477, 178);

		// Token: 0x040010A2 RID: 4258
		public static readonly Rectangle c_itemExpansion_temporaryUpgrade = new Rectangle(478, 1237, 477, 178);

		// Token: 0x040010A3 RID: 4259
		public static readonly Rectangle c_itemExpansion_slim = new Rectangle(793, 1417, 477, 178);

		// Token: 0x040010A4 RID: 4260
		public static readonly Rectangle c_itemExpansion_slim_extraordinare = new Rectangle(503, 444, 163, 162);

		// Token: 0x040010A5 RID: 4261
		public static readonly Rectangle c_itemNew = new Rectangle(1531, 885, 477, 105);

		// Token: 0x040010A6 RID: 4262
		public static readonly Rectangle c_itemTop = new Rectangle(1938, 1517, 477, 105);

		// Token: 0x040010A7 RID: 4263
		public static readonly Rectangle c_selectMenuItem = new Rectangle(478, 1070, 477, 139);

		// Token: 0x040010A8 RID: 4264
		public static readonly Rectangle c_selectMenuItem_small = new Rectangle(503, 672, 477, 85);

		// Token: 0x040010A9 RID: 4265
		public static readonly Rectangle c_selectMenuItem_small_blockMask = new Rectangle(981, 672, 477, 85);

		// Token: 0x040010AA RID: 4266
		public static readonly Rectangle c_itemNewUnit = new Rectangle(959, 1280, 475, 65);

		// Token: 0x040010AB RID: 4267
		public static readonly Rectangle c_itemNewUnit_orange = new Rectangle(959, 1346, 475, 65);

		// Token: 0x040010AC RID: 4268
		public static readonly Rectangle c_yellowButton = new Rectangle(478, 1210, 102, 26);

		// Token: 0x040010AD RID: 4269
		public static readonly Rectangle c_smallButton = new Rectangle(581, 1210, 81, 26);

		// Token: 0x040010AE RID: 4270
		public static readonly Rectangle c_showAllUpgradesButton = new Rectangle(501, 260, 46, 39);

		// Token: 0x040010AF RID: 4271
		public static readonly Rectangle c_sectionProgressBar_front = new Rectangle(700, 760, 40, 9);

		// Token: 0x040010B0 RID: 4272
		public static readonly Rectangle c_sectionProgressBar_back = new Rectangle(741, 760, 40, 9);

		// Token: 0x040010B1 RID: 4273
		public static readonly Rectangle c_selectionButton = new Rectangle(501, 260, 45, 38);

		// Token: 0x040010B2 RID: 4274
		public static readonly Vector2 p_ItemsStart = new Vector2(10f, 84f);

		// Token: 0x040010B3 RID: 4275
		public static readonly Vector2 p_tittle = new Vector2(249f, 25f);

		// Token: 0x040010B4 RID: 4276
		public static readonly Vector2 p_info = new Vector2(249f, 71f);

		// Token: 0x040010B5 RID: 4277
		private static readonly Color DefaultTitleColor = Color.LightGray;

		// Token: 0x040010B6 RID: 4278
		private readonly CustomSpriteFont {21706} = Fonts.Philosopher_14;

		// Token: 0x040010B7 RID: 4279
		private readonly Label {21707};

		// Token: 0x040010B8 RID: 4280
		protected bool TwoColumnsSelectionForm;

		// Token: 0x040010B9 RID: 4281
		protected bool IsSelectionFormOpened;

		// Token: 0x040010BA RID: 4282
		protected bool HaveFilter;

		// Token: 0x040010BB RID: 4283
		protected bool HaveSearchBar;

		// Token: 0x040010BC RID: 4284
		protected TextBox SearchBar;

		// Token: 0x040010BD RID: 4285
		protected Action<string> OnSearchEnter;

		// Token: 0x040010BE RID: 4286
		protected Dictionary<string, object> FilterMenu;

		// Token: 0x040010BF RID: 4287
		private StackForm {21708};

		// Token: 0x040010C0 RID: 4288
		private ScrollBarControl {21709};

		// Token: 0x040010C1 RID: 4289
		private ScrollBarControl {21710};

		// Token: 0x040010C2 RID: 4290
		private object {21711};

		// Token: 0x040010C3 RID: 4291
		private Form {21712};

		// Token: 0x040010C4 RID: 4292
		private Form {21713};
	}
}
