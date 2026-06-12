using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000101 RID: 257
	internal class {18027} : {17068}
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x000030FD File Offset: 0x000012FD
		protected override bool CanBeWindow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0002FAB8 File Offset: 0x0002DCB8
		public {18027}(Vector2 {18037}, {18027}.IProvider {18038}, Rectangle {18039}) : base(new Marker(ref {18037}, 600f, 750f), {18039}, {17068}.BlockingWay.NoBackground, false)
		{
			this.{18057} = {18038};
			this.{18058} = 0;
			this.{18059} = true;
			this.{18055} = new BlocksStackFormControl(base.Pos.XY + new Vector2(6f, 5f), 5, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{18055}.MaxWidth = 500f;
			this.{18056} = new BlocksStackFormControl(base.Pos.XY + new Vector2(318f, 5f), 5, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{18056}.MaxWidth = 500f;
			base.AddChild(new UiControl[]
			{
				this.{18055},
				this.{18056}
			});
			this.ReloadTable();
			base.Pos = base.Pos.SetHeight(Math.Max(this.{18055}.Pos.WH.Y, this.{18056}.Pos.WH.Y) + 10f);
			base.EvRemoveFromContainer += delegate()
			{
				{21838} currentInstance = {21838}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.RemoveFromContainer();
			};
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0002FC18 File Offset: 0x0002DE18
		public {18027}({18027}.IProvider {18040}, bool {18041}, Vector2? {18042}, string {18043}, float {18044}, bool {18045})
		{
			Marker marker;
			if ({18042} == null)
			{
				marker = Marker.FromCentrScreen(new Vector2((float)Engine.GS.UIArea.Width, (float)Engine.GS.UIArea.Height), {19275}.back);
			}
			else
			{
				Vector2 value = {18042}.Value;
				marker = new Marker(ref value, ref {19275}.back);
			}
			Marker marker2 = marker;
			base..ctor(marker2.Offset({18044}, 0f), {19275}.back, {18045} ? {17068}.BlockingWay.BackgroundClosing : {17068}.BlockingWay.NoBackground, false);
			this.{18057} = {18040};
			this.{18058} = 5;
			this.{18059} = false;
			this.{18055} = new BlocksStackFormControl(base.Pos.XY + new Vector2(64f, 395f), 6, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{18055}.MaxWidth = 500f;
			this.{18056} = new BlocksStackFormControl(base.Pos.XY + new Vector2(64f, 205f), 6, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{18056}.MaxWidth = 500f;
			base.AddChild(new UiControl[]
			{
				this.{18055},
				this.{18056},
				new Label(base.Pos.XY + new Vector2(237f, 172f), Fonts.Philosopher_18, ({18041} ? Color.Wheat : Color.Black) * 0.5764706f, {18043}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center(),
				new Label(base.Pos.XY + new Vector2(237f, 364f), Fonts.Philosopher_18, ({18041} ? Color.Wheat : Color.Black) * 0.5764706f, Local.hold, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center()
			});
			this.ReloadTable();
			base.Pos = base.Pos.SetHeight(Math.Max(this.{18055}.Pos.WH.Y, this.{18056}.Pos.WH.Y) + 10f);
			base.EvRemoveFromContainer += delegate()
			{
				{21838} currentInstance = {21838}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.RemoveFromContainer();
			};
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0002FE6C File Offset: 0x0002E06C
		public UiControl AddOrUpdateDefaultMoveAllButton(string {18046}, Vector2? {18047} = null)
		{
			if (this.{18060} != null)
			{
				this.{18060}.RemoveFromContainer();
			}
			base.AddChild(this.{18060} = new LabelButton({18047} ?? (base.Pos.XY + new Vector2(550f, 444f)), {18046}, Fonts.F_m14_ThinBold, Color.Gray, Color.Gold, new Action<ClickUiEventArgs>(this.{18053})));
			return this.{18060};
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0002FEF5 File Offset: 0x0002E0F5
		private void {18048}()
		{
			this.{18057}.TryMoveAllToShip();
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0002FF02 File Offset: 0x0002E102
		public void LockToReloadCall()
		{
			base.AllowMouseInput = false;
			this.{18062} = true;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0002FF14 File Offset: 0x0002E114
		public void ReloadTable()
		{
			if (this.{18062})
			{
				base.AllowMouseInput = true;
				this.{18062} = false;
			}
			this.{18055}.Clear();
			this.{18056}.Clear();
			foreach ({18027}.ItemBind {18050} in this.{18057}.EnumerateItemsAtRight())
			{
				this.{18049}({18050}, this.{18056});
			}
			foreach ({18027}.ItemBind {18050}2 in this.{18057}.EnumerateItemsAtShip())
			{
				this.{18049}({18050}2, this.{18055});
			}
			UiControl uiControl = this.{18061};
			if (uiControl != null)
			{
				uiControl.RemoveFromContainer();
			}
			this.{18061} = this.{18057}.AddFooter(this, base.Pos, Math.Max(this.{18055}.Pos.WH.Y, this.{18056}.Pos.WH.Y));
			if (this.{18061} != null)
			{
				base.AddChild(this.{18061});
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0003004C File Offset: 0x0002E24C
		private void {18049}({18027}.ItemBind {18050}, BlocksStackFormControl {18051})
		{
			{18027}.ItemPlaceholderBind itemPlaceholderBind = {18050} as {18027}.ItemPlaceholderBind;
			if (itemPlaceholderBind != null)
			{
				{18051}.AddItem(new UiControl[]
				{
					itemPlaceholderBind.insideContent
				});
				return;
			}
			{18027}.HeaderBind headerBind = {18050} as {18027}.HeaderBind;
			if (headerBind != null)
			{
				Form form = new Form(new Marker(0f, 0f, {18051}.MaxWidth - 2f, 25f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form.AddChild(new Label(new Vector2(0f, 3f), headerBind.isBig ? Fonts.Philosopher_18 : Fonts.Philosopher_14, headerBind.overrideColor ?? (Color.White * 0.4f), headerBind.headerName, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				{18051}.AddItem(new UiControl[]
				{
					form
				});
				Action<Form> onCreating = headerBind.OnCreating;
				if (onCreating == null)
				{
					return;
				}
				onCreating(form);
				return;
			}
			else
			{
				{18027}.InternalHeaderBind internalHeaderBind = {18050} as {18027}.InternalHeaderBind;
				if (internalHeaderBind != null)
				{
					Form form2 = new Form(new Marker(0f, 0f, 280f, 20f), CommonAtlas.whitePixel, Color.Black * 0.4f, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					};
					form2.AddChild(new Label(new Vector2(0f, 3f), Fonts.Arial_10, Color.Wheat * 0.9f, internalHeaderBind.headerName, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					{18051}.AddItem(new UiControl[]
					{
						form2
					});
					{18051}.AddItem(new UiControl[]
					{
						new Form(new Marker(0f, 0f, {18051}.MaxWidth - form2.Pos.WH.X, 20f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					return;
				}
				{18027}.ItemUi itemUi = new {18027}.ItemUi({18050}, this, {18051}, this.{18058}, this.{18059});
				{18051}.AddItem(new UiControl[]
				{
					itemUi
				});
				return;
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00030234 File Offset: 0x0002E434
		protected override void UserUpdate(ref FrameTime {18052})
		{
			base.UserUpdate(ref {18052});
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00030240 File Offset: 0x0002E440
		protected override void UserBackRender()
		{
			if (this.{18063} = (Engine.GS.CurrentTexture != CommonAtlas.Texture.Tex))
			{
				Engine.GS.SetTexture(CommonAtlas.Texture);
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00030280 File Offset: 0x0002E480
		protected override void UserFrontRender()
		{
			if (this.{18063})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00030294 File Offset: 0x0002E494
		[CompilerGenerated]
		private void {18053}(ClickUiEventArgs {18054})
		{
			this.{18048}();
		}

		// Token: 0x04000569 RID: 1385
		private BlocksStackFormControl {18055};

		// Token: 0x0400056A RID: 1386
		private BlocksStackFormControl {18056};

		// Token: 0x0400056B RID: 1387
		private {18027}.IProvider {18057};

		// Token: 0x0400056C RID: 1388
		private int {18058};

		// Token: 0x0400056D RID: 1389
		private bool {18059};

		// Token: 0x0400056E RID: 1390
		private LabelButton {18060};

		// Token: 0x0400056F RID: 1391
		private UiControl {18061};

		// Token: 0x04000570 RID: 1392
		public bool AllowOnlyToRight;

		// Token: 0x04000571 RID: 1393
		public bool BlockedAll;

		// Token: 0x04000572 RID: 1394
		public string Place1Name = Local.HoldsUiCommon_3;

		// Token: 0x04000573 RID: 1395
		public string Place2Name = Local.to_hold;

		// Token: 0x04000574 RID: 1396
		private bool {18062};

		// Token: 0x04000575 RID: 1397
		private bool {18063};

		// Token: 0x02000102 RID: 258
		public interface IProvider
		{
			// Token: 0x06000629 RID: 1577
			IEnumerable<{18027}.ItemBind> EnumerateItemsAtShip();

			// Token: 0x0600062A RID: 1578
			IEnumerable<{18027}.ItemBind> EnumerateItemsAtRight();

			// Token: 0x0600062B RID: 1579
			UiControl AddFooter({18027} {18064}, Marker {18065}, float {18066});

			// Token: 0x0600062C RID: 1580
			void TryMoveAllToShip();
		}

		// Token: 0x02000103 RID: 259
		public class ItemBind
		{
			// Token: 0x0600062D RID: 1581 RVA: 0x0003029C File Offset: 0x0002E49C
			public ItemBind()
			{
			}

			// Token: 0x0600062E RID: 1582 RVA: 0x000302AC File Offset: 0x0002E4AC
			public ItemBind(Texture2D {18076}, Rectangle {18077}, Func<int> {18078}, Action<int> {18079}, Action<int> {18080}, bool {18081}, bool {18082}, bool {18083}, Action<CustomUi> {18084} = null)
			{
				this.iconTexture = {18076};
				this.iconTextureBound = {18077};
				this.getItemCount = {18078};
				this.completeMoveToShip = {18079};
				this.completeMoveToRight = {18080};
				this.allowMoveToRight = {18081};
				this.isRedColor = {18082};
				this.isExternalUpdateTableOnly = {18083};
				this.middleware = {18084};
			}

			// Token: 0x04000576 RID: 1398
			public Texture2D iconTexture;

			// Token: 0x04000577 RID: 1399
			public Rectangle iconTextureBound;

			// Token: 0x04000578 RID: 1400
			public Func<int> getItemCount;

			// Token: 0x04000579 RID: 1401
			public int simgleItemWeight;

			// Token: 0x0400057A RID: 1402
			public Action<int> completeMoveToShip;

			// Token: 0x0400057B RID: 1403
			public Action<int> completeMoveToRight;

			// Token: 0x0400057C RID: 1404
			public bool allowMoveToRight;

			// Token: 0x0400057D RID: 1405
			public bool allowMove = true;

			// Token: 0x0400057E RID: 1406
			public bool isRedColor;

			// Token: 0x0400057F RID: 1407
			public bool isCellsLimitReached;

			// Token: 0x04000580 RID: 1408
			public bool isExternalUpdateTableOnly;

			// Token: 0x04000581 RID: 1409
			public Action CustomClickHandler;

			// Token: 0x04000582 RID: 1410
			public Action<CustomUi> middleware;
		}

		// Token: 0x02000104 RID: 260
		public class HeaderBind : {18027}.ItemBind
		{
			// Token: 0x0600062F RID: 1583 RVA: 0x0003030B File Offset: 0x0002E50B
			public HeaderBind(string {18088}, bool {18089} = false, Color? {18090} = null)
			{
				this.headerName = {18088};
				this.isBig = {18089};
				this.overrideColor = {18090};
			}

			// Token: 0x04000583 RID: 1411
			public string headerName;

			// Token: 0x04000584 RID: 1412
			public bool isBig;

			// Token: 0x04000585 RID: 1413
			public Color? overrideColor;

			// Token: 0x04000586 RID: 1414
			public Action<Form> OnCreating;
		}

		// Token: 0x02000105 RID: 261
		public class InternalHeaderBind : {18027}.ItemBind
		{
			// Token: 0x06000630 RID: 1584 RVA: 0x00030328 File Offset: 0x0002E528
			public InternalHeaderBind(string {18092})
			{
				this.headerName = {18092};
			}

			// Token: 0x04000587 RID: 1415
			public string headerName;
		}

		// Token: 0x02000106 RID: 262
		public class ItemPlaceholderBind : {18027}.ItemBind
		{
			// Token: 0x06000631 RID: 1585 RVA: 0x00030337 File Offset: 0x0002E537
			public ItemPlaceholderBind(UiControl {18094})
			{
				this.insideContent = {18094};
			}

			// Token: 0x04000588 RID: 1416
			public UiControl insideContent;
		}

		// Token: 0x02000107 RID: 263
		private class MoveAnimation : CustomUi
		{
			// Token: 0x06000632 RID: 1586 RVA: 0x00030348 File Offset: 0x0002E548
			public MoveAnimation(Image {18097}, Vector2 {18098}) : base({18097}.Pos, Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp, Color.White, false)
			{
				this.{18100} = {18098} - Engine.GS.MouseToUI;
				this.{18101} = {18098};
				base.AddChild(this.{18105} = new Image({18097}.Pos, {18097}.Texture, {18097}.TexturePath, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}

			// Token: 0x06000633 RID: 1587 RVA: 0x000303B4 File Offset: 0x0002E5B4
			protected override void UserUpdate(ref FrameTime {18099})
			{
				Marker marker;
				if (!this.{18103})
				{
					marker = base.Pos;
					marker = marker.SetX(Engine.GS.MouseToUI.X + this.{18100}.X);
					base.Pos = marker.SetY(Engine.GS.MouseToUI.Y + this.{18100}.Y);
				}
				this.{18104} += {18099}.secElapsed * 6f;
				UiControl uiControl = this.{18105};
				marker = base.Pos;
				uiControl.Pos = marker.ScaleOfCenter((this.{18104} < 1f) ? (1f + MathF.Sqrt(this.{18104}) * 0.25f) : 1.25f);
				if (!this.{18103} && InputHelper.LeftWasReleased)
				{
					base.RemoveFromContainer();
					this.{18103} = true;
				}
			}

			// Token: 0x06000634 RID: 1588 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserBackRender()
			{
			}

			// Token: 0x06000635 RID: 1589 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserFrontRender()
			{
			}

			// Token: 0x04000589 RID: 1417
			private Vector2 {18100};

			// Token: 0x0400058A RID: 1418
			private Vector2 {18101};

			// Token: 0x0400058B RID: 1419
			private Vector2 {18102};

			// Token: 0x0400058C RID: 1420
			private bool {18103};

			// Token: 0x0400058D RID: 1421
			private float {18104};

			// Token: 0x0400058E RID: 1422
			private Image {18105};
		}

		// Token: 0x02000108 RID: 264
		private class ItemUi : CustomUi
		{
			// Token: 0x06000636 RID: 1590 RVA: 0x00030494 File Offset: 0x0002E694
			public ItemUi({18027}.ItemBind {18111}, {18027} {18112}, BlocksStackFormControl {18113}, int {18114}, bool {18115})
			{
				Vector2 vector = default(Vector2);
				Vector2 vector2 = new Vector2((float)(({18115} ? 56 : 53) + {18114}));
				base..ctor(new Marker(ref vector, ref vector2), Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp, Color.White, false);
				this.{18136} = {18114};
				this.{18130} = {18111};
				this.{18133} = {18112};
				this.{18134} = {18113};
				this.BasicColor = Color.Transparent;
				vector = default(Vector2);
				vector2 = new Vector2((float)({18115} ? 56 : 53));
				base.AddChild(this.{18132} = new Image(new Marker(ref vector, ref vector2), {18111}.iconTexture, {18111}.iconTextureBound, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				Action<CustomUi> middleware = {18111}.middleware;
				if (middleware != null)
				{
					middleware(this);
				}
				int num = {18111}.getItemCount();
				this.{18135} = ((num == 0) ? "" : StringHelper.GetValueOfK(num));
				if ({18111}.isRedColor)
				{
					this.{18132}.BasicColor = Color.Red;
				}
				if ({18111}.CustomClickHandler != null)
				{
					base.EvClick += delegate(ClickUiEventArgs {18138})
					{
						Action customClickHandler = {18111}.CustomClickHandler;
						if (customClickHandler == null)
						{
							return;
						}
						customClickHandler();
					};
				}
			}

			// Token: 0x06000637 RID: 1591 RVA: 0x000305E8 File Offset: 0x0002E7E8
			protected override void UserUpdate(ref FrameTime {18116})
			{
				this.{18137} += {18116}.secElapsed;
				if ((!this.{18130}.allowMoveToRight && this.{18133}.{18055} == this.{18134}) || !this.{18130}.allowMove || this.{18133}.BlockedAll)
				{
					return;
				}
				if (InputHelper.LeftWasReleased && this.{18131} != null && base.AllowMouseInput)
				{
					Marker pos = this.{18133}.Pos;
					Vector2 mouseToUI = Engine.GS.MouseToUI;
					if (pos.Collision(mouseToUI))
					{
						if (this.{18130}.isCellsLimitReached)
						{
							new {17312}(Local.cells_limit_reached);
							this.{18131} = null;
						}
						else if (this.{18130}.CustomClickHandler == null)
						{
							if ((this.{18137} < 0.3f || this.{18130}.getItemCount() == 1) && (!this.{18133}.AllowOnlyToRight || this.{18133}.{18055} != this.{18134}))
							{
								this.{18137} = 1f;
								if (this.{18133}.{18055} == this.{18134})
								{
									this.{18130}.completeMoveToRight(this.{18130}.getItemCount());
									if (!this.{18130}.isExternalUpdateTableOnly)
									{
										this.{18133}.ReloadTable();
									}
								}
								else
								{
									this.{18130}.completeMoveToShip(this.{18117}(this.{18130}, 0.5f));
									if (!this.{18130}.isExternalUpdateTableOnly)
									{
										this.{18133}.ReloadTable();
									}
								}
							}
							else
							{
								this.{18137} = 0f;
								bool flag = this.{18133}.{18055}.Pos.XY.X == this.{18133}.{18056}.Pos.XY.X;
								if ((this.{18133}.{18055} != this.{18134} && (flag ? (Engine.GS.MouseToUI.Y > this.{18133}.Pos.Center.Y) : (Engine.GS.MouseToUI.X < this.{18133}.Pos.Center.X))) || (this.{18133}.{18056} != this.{18134} && (flag ? (Engine.GS.MouseToUI.Y < this.{18133}.Pos.Center.Y) : (Engine.GS.MouseToUI.X > this.{18133}.Pos.Center.X))))
								{
									base.AllowMouseInput = false;
									base.Opacity = 0.5f;
									if (this.{18133}.{18055} != this.{18134})
									{
										if (this.{18133}.AllowOnlyToRight)
										{
											new {17312}(Local.HoldsUiCommon_2);
										}
										else
										{
											new {21838}(this.{18133}.Place1Name, Local.move, new Action<int>(this.{18120}), this.{18117}(this.{18130}, 1f), new Func<int, string>(this.{18122}), new int?(this.{18117}(this.{18130}, 1f) / (({19275}.CurrentInstance != null) ? 1 : 2)), new int?(this.{18130}.getItemCount()), null, null).EvRemoveFromContainer += this.{18124};
										}
									}
									else
									{
										new {21838}(this.{18133}.Place2Name, Local.move, new Action<int>(this.{18125}), this.{18130}.getItemCount(), new Func<int, string>(this.{18127}), null, null, null, null).EvRemoveFromContainer += this.{18129};
									}
								}
							}
							this.{18131} = null;
						}
					}
				}
				if ((base.InputMode == MouseInputMode.Focused || base.InputMode == MouseInputMode.Down) && InputHelper.LeftWasClicked)
				{
					this.{18131} = new Vector2?(base.Pos.XY);
					new {18027}.MoveAnimation(this.{18132}, this.{18131}.Value);
				}
				this.{18132}.BasicColor = ((base.InputMode == MouseInputMode.Down) ? Color.Gray : Color.White);
			}

			// Token: 0x06000638 RID: 1592 RVA: 0x00030A88 File Offset: 0x0002EC88
			private int {18117}({18027}.ItemBind {18118}, float {18119} = 1f)
			{
				if ({18118}.simgleItemWeight == 0)
				{
					return {18118}.getItemCount();
				}
				float freeCapacity = Global.Player.UsedShipPlayer.FreeCapacity;
				float num = Global.Player.UsedShip.Capacity * {18119};
				if (freeCapacity <= (float){18118}.simgleItemWeight)
				{
					return Math.Min({18118}.getItemCount(), (int)Math.Ceiling((double)(num / (float){18118}.simgleItemWeight)));
				}
				return Math.Min({18118}.getItemCount(), Math.Max(1, (int)Math.Floor((double)(Math.Min(num, freeCapacity) / (float){18118}.simgleItemWeight))));
			}

			// Token: 0x06000639 RID: 1593 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserBackRender()
			{
			}

			// Token: 0x0600063A RID: 1594 RVA: 0x00030B24 File Offset: 0x0002ED24
			protected override void UserFrontRender()
			{
				float opcaity = base.GetOpcaity();
				Device gs = Engine.GS;
				Rectangle rectangle = new Rectangle((int)base.Pos.XY.X, (int)(base.Pos.XY.Y + base.Pos.WH.Y - 15f - (float)this.{18136}), (int)base.Pos.WH.X - this.{18136}, 15);
				Color color = Color.Black * (0.6f * opcaity);
				gs.Draw(CommonAtlas.whitePixel, rectangle, color);
				Engine.GS.SetFont(Fonts.Arial_10);
				Device gs2 = Engine.GS;
				string {14599} = this.{18135};
				Vector2 vector = base.Pos.XY + new Vector2(4f, base.Pos.WH.X - 15f - (float)this.{18136});
				color = Color.White * opcaity;
				gs2.DrawString({14599}, vector, color);
			}

			// Token: 0x0600063B RID: 1595 RVA: 0x00030C28 File Offset: 0x0002EE28
			[CompilerGenerated]
			private void {18120}(int {18121})
			{
				this.{18130}.completeMoveToShip({18121});
				if (!this.{18130}.isExternalUpdateTableOnly)
				{
					this.{18133}.ReloadTable();
				}
				if (Session.Account.Rang >= 7 && Global.Game.GetCurrentSceneName == GameSceneName.Port)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_MoveByDoubleClick, true);
				}
			}

			// Token: 0x0600063C RID: 1596 RVA: 0x00030C80 File Offset: 0x0002EE80
			[CompilerGenerated]
			private string {18122}(int {18123})
			{
				if (this.{18130}.simgleItemWeight != 0)
				{
					return Local.weight_is(StringHelper.BigValueHelper(this.{18130}.simgleItemWeight * {18123}));
				}
				return string.Empty;
			}

			// Token: 0x0600063D RID: 1597 RVA: 0x00030CAC File Offset: 0x0002EEAC
			[CompilerGenerated]
			private void {18124}()
			{
				base.AllowMouseInput = true;
				base.Opacity = 1f;
			}

			// Token: 0x0600063E RID: 1598 RVA: 0x00030CC0 File Offset: 0x0002EEC0
			[CompilerGenerated]
			private void {18125}(int {18126})
			{
				this.{18130}.completeMoveToRight({18126});
				if (!this.{18130}.isExternalUpdateTableOnly)
				{
					this.{18133}.ReloadTable();
				}
				if (Session.Account.Rang >= 7 && Global.Game.GetCurrentSceneName == GameSceneName.Port)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_MoveByDoubleClick, true);
				}
			}

			// Token: 0x0600063F RID: 1599 RVA: 0x00030C80 File Offset: 0x0002EE80
			[CompilerGenerated]
			private string {18127}(int {18128})
			{
				if (this.{18130}.simgleItemWeight != 0)
				{
					return Local.weight_is(StringHelper.BigValueHelper(this.{18130}.simgleItemWeight * {18128}));
				}
				return string.Empty;
			}

			// Token: 0x06000640 RID: 1600 RVA: 0x00030CAC File Offset: 0x0002EEAC
			[CompilerGenerated]
			private void {18129}()
			{
				base.AllowMouseInput = true;
				base.Opacity = 1f;
			}

			// Token: 0x0400058F RID: 1423
			private const int size_b = 53;

			// Token: 0x04000590 RID: 1424
			private const int size_z = 56;

			// Token: 0x04000591 RID: 1425
			private {18027}.ItemBind {18130};

			// Token: 0x04000592 RID: 1426
			private Vector2? {18131};

			// Token: 0x04000593 RID: 1427
			private Image {18132};

			// Token: 0x04000594 RID: 1428
			private {18027} {18133};

			// Token: 0x04000595 RID: 1429
			private BlocksStackFormControl {18134};

			// Token: 0x04000596 RID: 1430
			private string {18135};

			// Token: 0x04000597 RID: 1431
			private int {18136};

			// Token: 0x04000598 RID: 1432
			private float {18137} = 1f;
		}
	}
}
