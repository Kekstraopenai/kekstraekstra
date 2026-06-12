using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Input;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Eventing;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000CA RID: 202
	public class DropdownControl<TItem> : Button
	{
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000543 RID: 1347 RVA: 0x0001BE58 File Offset: 0x0001A058
		// (remove) Token: 0x06000544 RID: 1348 RVA: 0x0001BE90 File Offset: 0x0001A090
		public event Action EvOpen;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000545 RID: 1349 RVA: 0x0001BEC8 File Offset: 0x0001A0C8
		// (remove) Token: 0x06000546 RID: 1350 RVA: 0x0001BF00 File Offset: 0x0001A100
		public event Action<SelItem<TItem>> EvChangeItem;

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001BF35 File Offset: 0x0001A135
		public SelItem<TItem> Selected
		{
			get
			{
				if (this.selectedIndex != -1)
				{
					return this.variants.Array[this.selectedIndex];
				}
				return null;
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001BF54 File Offset: 0x0001A154
		public DropdownControl(Marker {13855}, Rectangle {13856}, Rectangle {13857}, CustomSpriteFont {13858}, params SelItem<TItem>[] {13859}) : base({13855}, {13856}, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.Font = {13858};
			this.ItemTexturePath = {13857};
			this.variants = new Tlist<SelItem<TItem>>({13859});
			this.InitializeCommonLogic();
			this.TextColor = Color.White * 0.8f;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001BFAE File Offset: 0x0001A1AE
		public DropdownControl(Marker {13860}, Rectangle {13861}, Rectangle {13862}, float {13863}, CustomSpriteFont[] {13864}, CustomSpriteFont {13865}, params SelItem<TItem>[] {13866}) : this({13860}, {13861}, {13862}, {13865}, {13866})
		{
			this.maxWidth = {13863};
			this.fontSizes = {13864};
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001BFD0 File Offset: 0x0001A1D0
		private void InitializeCommonLogic()
		{
			this.selectedIndex = -1;
			this.isOpen = false;
			base.EvClick += delegate(ClickUiEventArgs {13885})
			{
				this.TryToOpen();
			};
			if (this.variants.Size != 0)
			{
				this.Select(0);
			}
			base.EvRemoveFromContainer += delegate()
			{
				if (this.isOpen)
				{
					this.Close();
				}
				this.activeTexture = null;
			};
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001C024 File Offset: 0x0001A224
		public DropdownControl(Marker {13867}, Rectangle {13868}, Rectangle {13869}, CustomSpriteFont {13870}, Color {13871}, params SelItem<TItem>[] {13872}) : base({13867}, {13868}, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.Font = {13870};
			this.ItemTexturePath = {13869};
			this.variants = new Tlist<SelItem<TItem>>({13872});
			this.InitializeCommonLogic();
			this.TextColor = {13871};
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001C071 File Offset: 0x0001A271
		public DropdownControl(Vector2 {13873}, Rectangle {13874}, Rectangle {13875}, CustomSpriteFont {13876}, params SelItem<TItem>[] {13877}) : this(new Marker(ref {13873}, ref {13874}), {13874}, {13875}, {13876}, {13877})
		{
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001C088 File Offset: 0x0001A288
		internal override void Update(ref FrameTime {13878}, ref int {13879})
		{
			base.Update(ref {13878}, ref {13879});
			if (this.isOpen && base.InputMode == MouseInputMode.NoFocus && (InputHelper.NowMouseState.LeftPressed || InputHelper.NowMouseState.RightPressed))
			{
				this.Close();
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001C0C0 File Offset: 0x0001A2C0
		internal override void Render()
		{
			this.activeTexture = Engine.GS.CurrentTexture;
			base.Render();
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001C0D8 File Offset: 0x0001A2D8
		public void SelectByValue(TItem {13880})
		{
			for (int i = 0; i < this.variants.Size; i++)
			{
				TItem value = this.variants.Array[i].Value;
				if (value.Equals({13880}))
				{
					this.Select(i);
					return;
				}
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001C12C File Offset: 0x0001A32C
		public void Select(int {13881})
		{
			if (this.isOpen)
			{
				this.Close();
			}
			this.selectedIndex = {13881};
			if ({13881} == -1)
			{
				base.SetText("-", this.Font, this.TextColor, this.TextLeftAlignment);
				return;
			}
			SelItem<TItem> selItem = this.variants.Array[{13881}];
			Action<SelItem<TItem>> evChangeItem = this.EvChangeItem;
			if (evChangeItem != null)
			{
				evChangeItem(selItem);
			}
			this.RenderItem(this, selItem);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001C19C File Offset: 0x0001A39C
		public void SetItems(params SelItem<TItem>[] {13882})
		{
			if (this.isOpen)
			{
				this.Close();
			}
			this.variants = new Tlist<SelItem<TItem>>({13882});
			if (this.variants.Size != 0)
			{
				this.Select(Math.Min(this.variants.Size - 1, this.selectedIndex));
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000E21A File Offset: 0x0000C41A
		protected override bool AllowResize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001C1F0 File Offset: 0x0001A3F0
		private void TryToOpen()
		{
			Action evOpen = this.EvOpen;
			if (evOpen != null)
			{
				evOpen();
			}
			if (this.formItems != null)
			{
				this.Close();
			}
			this.isOpen = true;
			this.ownExtraBrightness = 0.5f;
			Texture2D {12838} = this.activeTexture;
			Marker pos = base.Pos;
			Vector2 vector = new Vector2((float)this.ItemTexturePath.Width, base.Pos.WH.Y + (float)(this.ItemTexturePath.Height * this.variants.Size) + 4f);
			this.formItems = new TextureHost({12838}, new Marker(ref pos.XY, ref vector))
			{
				AnimatedFocus = false
			};
			float num = base.Pos.XY.Y + base.Pos.WH.Y;
			this.openItemsCount = this.variants.Size;
			for (int i = 0; i < this.variants.Size; i++)
			{
				SelItem<TItem> selItem = this.variants.Array[i];
				Button button = new Button(new Vector2(base.Pos.XY.X, num + (float)(this.ItemTexturePath.Height * i)), this.ItemTexturePath, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button.Tag = i;
				if (selItem.ToolTip != null)
				{
					button.ToolTip = selItem.ToolTip;
				}
				Action<UiControl, SelItem<TItem>> addExtraContent = selItem.AddExtraContent;
				if (addExtraContent != null)
				{
					addExtraContent(button, selItem);
				}
				this.RenderItem(button, selItem);
				UiControl {14249} = button;
				float {14250} = 0f;
				float {14251} = 1f;
				pos = base.Pos;
				new UiMarkerAndOpacityAnimation({14249}, {14250}, {14251}, new Marker(ref pos.XY, ref this.ItemTexturePath), button.Pos, 400f, UiAmimationCurve.SquaredToBegin);
				button.EvClick += delegate(ClickUiEventArgs {13886})
				{
					this.Select((int){13886}.Sender.Tag);
				};
				this.formItems.AddChild(button);
			}
			this.formItems.DisableDepthFocusTest = true;
			Engine.Game.GetInterfaceManager.parent.AddChild(this.formItems);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001C3F4 File Offset: 0x0001A5F4
		private void RenderItem(Button {13883}, SelItem<TItem> {13884})
		{
			if (this.maxWidth > 0f && this.fontSizes != null)
			{
				{13883}.SetText({13884}.Text, this.maxWidth, this.fontSizes, this.Font, this.TextColor, this.TextLeftAlignment);
			}
			else
			{
				{13883}.SetText({13884}.Text, this.Font, this.TextColor, this.TextLeftAlignment);
			}
			{13883}.ClearAllChild();
			if ({13884}.CoverDecoration.Width != 0)
			{
				{13883}.AddChildPos(new Form(Vector2.Zero, {13884}.CoverDecoration, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					RenderToDepthMap = false
				}, PositionAlignment.RightDown, PositionAlignment.Center, 0f);
			}
			Action<UiControl, SelItem<TItem>> addExtraContent = {13884}.AddExtraContent;
			if (addExtraContent == null)
			{
				return;
			}
			addExtraContent({13883}, {13884});
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001C4B5 File Offset: 0x0001A6B5
		private void Close()
		{
			this.ownExtraBrightness = 0f;
			this.formItems.ClearAllChild();
			this.formItems.RemoveFromContainer();
			this.formItems = null;
			this.isOpen = false;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001C4E6 File Offset: 0x0001A6E6
		protected override void CleanResources()
		{
			if (this.isOpen)
			{
				this.Close();
			}
			this.variants = null;
			this.EvChangeItem = null;
			this.EvOpen = null;
			base.CleanResources();
		}

		// Token: 0x0400040F RID: 1039
		public Rectangle ItemTexturePath;

		// Token: 0x04000410 RID: 1040
		public new readonly CustomSpriteFont Font;

		// Token: 0x04000411 RID: 1041
		public bool TextLeftAlignment = true;

		// Token: 0x04000412 RID: 1042
		private Tlist<SelItem<TItem>> variants;

		// Token: 0x04000413 RID: 1043
		private int selectedIndex;

		// Token: 0x04000414 RID: 1044
		private TextureHost formItems;

		// Token: 0x04000415 RID: 1045
		private bool isOpen;

		// Token: 0x04000416 RID: 1046
		private int openItemsCount;

		// Token: 0x04000417 RID: 1047
		private Texture2D activeTexture;

		// Token: 0x04000418 RID: 1048
		private float maxWidth;

		// Token: 0x04000419 RID: 1049
		private CustomSpriteFont[] fontSizes;
	}
}
