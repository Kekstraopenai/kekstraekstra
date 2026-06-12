using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020003E0 RID: 992
	internal class {22687} : CustomUi
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x000B602B File Offset: 0x000B422B
		public bool IsTesxBoxEntering
		{
			get
			{
				return base.IsVisible;
			}
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x000B6034 File Offset: 0x000B4234
		public {22687}() : base(false)
		{
			this.{22718} = new Tlist<{22687}.RoomAndButtonHolder>(10);
			this.{22716} = new UnionFormControl(new Marker(100f, 100f, 500f, 400f), {22687}.main, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{22716}.AnimatedFocus = false;
			this.{22716}.AllowDragDrop = true;
			this.{22716}.Temp_FixTex = CommonAtlas.Texture.Tex;
			this.{22716}.IsVisible = false;
			Vector2 vector = this.{22716}.Pos.XY + {22687}.p_contentStart;
			Vector2 vector2 = this.{22716}.Pos.XY + this.{22716}.Pos.WH - {22687}.p_contentEndOffsetFromEnd;
			Vector2 vector3 = vector2 - vector;
			float num = 48f;
			this.{22716}.AddChild(new UiControl[]
			{
				new Form(new Marker(vector.X, vector.Y, vector3.X, num), CommonAtlas.whitePixel, PositionAlignment.Both, PositionAlignment.LeftUp)
				{
					BasicColor = Color.Black * 0.5f
				}
			});
			base.EvClick += this.{22713};
			int num2 = 0;
			TextBox textBox = new TextBox(new Vector2(vector.X, vector2.Y - {22687}.textBoxTex.Height - (float)num2), new Rectangle({22687}.textBoxTex.Start.X, {22687}.textBoxTex.Start.Y, (int)vector3.X, (int){22687}.textBoxTex.Height), Fonts.Arial_10, Color.White, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			textBox.PosHeight = {22687}.textBoxTex.Height + (float)num2;
			textBox.PositionAlignment_X = PositionAlignment.Both;
			textBox.PositionAlignment_Y = PositionAlignment.RightDown;
			textBox.Multitexture = {22687}.textBoxTex;
			textBox.EnabledMultipliedLines = true;
			textBox.AttachMaxLengthModerator(120, null, Color.Transparent);
			textBox.Text = Session.LsMessageDraft;
			textBox.EvRemoveFromContainer += delegate()
			{
				Session.LsMessageDraft = textBox.Text;
			};
			this.{22716}.AddChild(new UiControl[]
			{
				textBox
			});
			this.{22720} = new ScrollBarControl(Marker.Zero, Rectangle.Empty, Rectangle.Empty, Rectangle.Empty, Rectangle.Empty, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{22720}.IsVisible = false;
			this.{22717} = new ListItemViewControl(new Marker(vector.X, vector.Y + num, vector3.X, vector3.Y - textBox.Pos.WH.Y - num), this.{22720}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{22717}.PositionAlignment_X = PositionAlignment.Both;
			this.{22717}.PositionAlignment_Y = PositionAlignment.Both;
			this.{22716}.AddChild(new UiControl[]
			{
				this.{22717}
			});
			this.{22716}.AddChild(new UiControl[]
			{
				this.{22720}
			});
			base.RemoveWithThis(new UiControl[]
			{
				this.{22716}
			});
			{22478}.currentLSViewBox = this;
			textBox.EvGotEnter += delegate()
			{
			};
			textBox.EvEntering += delegate()
			{
				if (string.IsNullOrEmpty(textBox.Text))
				{
					return;
				}
				this.{22688}(textBox.Text);
				textBox.Text = string.Empty;
			};
			base.EvRemoveFromContainer += this.{22715};
			Form resizeHandForm = new Form(this.{22716}.Pos.End - {22687}.c_hand_resize.WidthHeight() - new Vector2(15f), {22687}.c_hand_resize, PositionAlignment.LeftUp, PositionAlignment.RightDown)
			{
				AllowDragDrop = true
			};
			this.{22716}.AddChild(new UiControl[]
			{
				resizeHandForm
			});
			resizeHandForm.EvIntegerDrop += delegate(Vector2 {22741})
			{
				Vector2 vector4 = this.{22716}.Pos.WH + {22741};
				vector4.X = this.{22716}.Pos.WH.X;
				vector4.Y = MathHelper.Clamp(vector4.Y, 250f, 500f);
				UiControl uiControl = this.{22716};
				Vector2 vector5 = this.{22716}.Pos.XY - {22741};
				uiControl.Pos = new Marker(ref vector5, ref vector4);
				resizeHandForm.Pos = resizeHandForm.Pos.SetXY(resizeHandForm.Pos.XY);
				this.{22707}();
				return false;
			};
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x000B644F File Offset: 0x000B464F
		public void GotFocus()
		{
			Global.Game.SceneGame.IncreaseMouse();
			GameScene.IncreaseGameInput();
			this.{22716}.MoveToFrontLevel();
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x000B6470 File Offset: 0x000B4670
		private void {22688}(string {22689})
		{
			GuildCommon guild = Session.Guild;
			string text = ((guild != null) ? guild.Tag : null) ?? string.Empty;
			GuildCommon guild2 = Session.Guild;
			byte b = (guild2 != null) ? guild2.GetMember(Session.Account.SID).RankId : 0;
			this.AddMessage(this.{22719}.Item.TargetSID, {22689}, {22687}.currentPlayerMessageColor, true, text, b);
			Global.Network.Send(new OnSendOrReceiveLSMsg(new ChatMessageDefault(string.Empty, {22689}, this.{22719}.Item.TargetSID, LocaleInfo.Current.Id), LSFlags.None, false, text, b));
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x000B6518 File Offset: 0x000B4718
		public void AddMessage(uint {22690}, string {22691}, Color {22692}, bool {22693}, string {22694} = "", byte {22695} = 0)
		{
			int i = 0;
			while (i < this.{22718}.Size)
			{
				{22687}.RoomAndButtonHolder roomAndButtonHolder = this.{22718}.Array[i];
				if (roomAndButtonHolder.Item.TargetSID == {22690})
				{
					string str = string.IsNullOrEmpty({22694}) ? "" : ("[" + {22694} + "] ");
					Tlist<LSCacheItem.MessageWithColor> savedMessages = roomAndButtonHolder.Item.SavedMessages;
					LSCacheItem.MessageWithColor messageWithColor = new LSCacheItem.MessageWithColor
					{
						Text = str + ({22693} ? Session.Account.PlayerName : roomAndButtonHolder.Item.TargetName) + ": " + {22691},
						Color = {22692},
						GuildRankId = {22695}
					};
					savedMessages.Add(messageWithColor);
					if (roomAndButtonHolder.Item.SavedMessages.Size > 350)
					{
						roomAndButtonHolder.Item.SavedMessages.RemoveAt(0);
					}
					this.{22703}(roomAndButtonHolder);
					if (roomAndButtonHolder.IsCurrent)
					{
						this.{22710}(roomAndButtonHolder.Item.SavedMessages.Last());
						return;
					}
					roomAndButtonHolder.SetLightEffect();
					return;
				}
				else
				{
					i++;
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x000B6630 File Offset: 0x000B4830
		public bool PutSpecialMessage(uint {22696}, string {22697})
		{
			for (int i = 0; i < this.{22718}.Size; i++)
			{
				{22687}.RoomAndButtonHolder roomAndButtonHolder = this.{22718}.Array[i];
				if (roomAndButtonHolder.Item.TargetSID == {22696})
				{
					Tlist<LSCacheItem.MessageWithColor> savedMessages = roomAndButtonHolder.Item.SavedMessages;
					LSCacheItem.MessageWithColor messageWithColor = new LSCacheItem.MessageWithColor
					{
						Text = {22697},
						Color = {22687}.systemMessageColor
					};
					savedMessages.Add(messageWithColor);
					this.{22703}(roomAndButtonHolder);
					if (roomAndButtonHolder.IsCurrent)
					{
						this.{22710}(roomAndButtonHolder.Item.SavedMessages.Last());
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x000B66C4 File Offset: 0x000B48C4
		public void AddOrUpdateRoom(uint {22698}, string {22699}, bool {22700})
		{
			for (int i = 0; i < this.{22718}.Size; i++)
			{
				{22687}.RoomAndButtonHolder roomAndButtonHolder = this.{22718}.Array[i];
				if (roomAndButtonHolder.Item.TargetSID == {22698})
				{
					roomAndButtonHolder.Item.IsOnline = {22700};
					return;
				}
			}
			LSCacheItem lscacheItem = Global.Settings.LsSavedCacheData.Cache.FirstOrDefault((LSCacheItem {22742}) => {22742}.TargetSID == {22698});
			LSCacheItem lscacheItem2;
			if (lscacheItem != null)
			{
				Tlist<LSCacheItem.MessageWithColor> savedMessages = lscacheItem.SavedMessages;
				int? num = (savedMessages != null) ? new int?(savedMessages.Size) : null;
				int num2 = 0;
				if (num.GetValueOrDefault() >= num2 & num != null)
				{
					lscacheItem2 = lscacheItem;
					goto IL_CA;
				}
			}
			lscacheItem2 = new LSCacheItem({22699}, {22698}, {22700});
			IL_CA:
			LSCacheItem {22724} = lscacheItem2;
			{22687}.RoomAndButtonHolder roomAndButtonHolder2 = new {22687}.RoomAndButtonHolder(Vector2.Zero, {22724});
			this.{22718}.Add(roomAndButtonHolder2);
			this.{22703}(roomAndButtonHolder2);
			this.{22716}.AddChild(new UiControl[]
			{
				roomAndButtonHolder2
			});
			if (this.{22718}.Size == 1)
			{
				roomAndButtonHolder2.SetAsCurrent();
			}
			this.{22707}();
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x000B67EC File Offset: 0x000B49EC
		public void UpdateOnlineStatus(uint {22701}, bool {22702})
		{
			for (int i = 0; i < this.{22718}.Size; i++)
			{
				{22687}.RoomAndButtonHolder roomAndButtonHolder = this.{22718}.Array[i];
				if (roomAndButtonHolder.Item.TargetSID == {22701})
				{
					roomAndButtonHolder.Item.IsOnline = {22702};
					this.{22703}(roomAndButtonHolder);
				}
			}
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x000B6840 File Offset: 0x000B4A40
		private void {22703}({22687}.RoomAndButtonHolder {22704})
		{
			LSCacheItem item = {22704}.Item;
			if (Global.Settings.LsSavedCacheData.Cache.FirstOrDefault((LSCacheItem {22743}) => {22743}.TargetSID == {22704}.Item.TargetSID) == null)
			{
				Global.Settings.LsSavedCacheData.Cache.Add({22704}.Item);
			}
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x000B68AC File Offset: 0x000B4AAC
		public void SaveLSMessages()
		{
			Global.Settings.LsSavedCacheData.Cache.Clear();
			for (int i = 0; i < this.{22718}.Size; i++)
			{
				Global.Settings.LsSavedCacheData.Cache.Add(this.{22718}.Array[i].Item);
			}
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x000B690C File Offset: 0x000B4B0C
		public void LoadCache()
		{
			Tlist<LSCacheItem> cache = Global.Settings.LsSavedCacheData.Cache;
			if (cache.Size == 0 && Global.Settings.LsSavedCacheData.Cache.Size > 0)
			{
				cache = Global.Settings.LsSavedCacheData.Cache;
			}
			for (int i = 0; i < cache.Size; i++)
			{
				{22687}.RoomAndButtonHolder roomAndButtonHolder = new {22687}.RoomAndButtonHolder(Vector2.Zero, cache.Array[i]);
				this.{22718}.Add(roomAndButtonHolder);
				this.{22716}.AddChild(new UiControl[]
				{
					roomAndButtonHolder
				});
			}
			this.{22718}.Array[0].SetAsCurrent();
			this.{22707}();
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x000B69B8 File Offset: 0x000B4BB8
		protected void CloseRoom({22687}.RoomAndButtonHolder {22705})
		{
			int index = this.{22718}.IndexOf({22705});
			Global.Settings.LsSavedCacheData.Cache.RemoveAll((LSCacheItem {22744}) => {22744}.TargetSID == this.{22718}.Array[index].Item.TargetSID);
			this.{22716}.RemoveChild({22705});
			this.{22718}.RemoveAt(index);
			if (this.{22718}.Size == 0)
			{
				base.RemoveFromContainer();
				return;
			}
			this.{22707}();
			if (this.{22718}.Size == 1)
			{
				this.{22718}.Array[0].SetAsCurrent();
				return;
			}
			if (index == 0)
			{
				this.{22718}.Array[1].SetAsCurrent();
				return;
			}
			this.{22718}.Array[index - 1].SetAsCurrent();
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x000B6A94 File Offset: 0x000B4C94
		protected void LoadRoomContent({22687}.RoomAndButtonHolder {22706})
		{
			for (int i = 0; i < this.{22718}.Size; i++)
			{
				if (this.{22718}.Array[i] != {22706})
				{
					this.{22718}.Array[i].SetAsDefault();
				}
			}
			this.{22708}({22706});
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x000B6AE0 File Offset: 0x000B4CE0
		private void {22707}()
		{
			float num = this.{22716}.Pos.WH.X - (float){22687}.c_hand_close.Width - 5f;
			Vector2 vector = this.{22716}.Pos.XY + {22687}.p_tab_line1;
			foreach ({22687}.RoomAndButtonHolder roomAndButtonHolder in ((IEnumerable<{22687}.RoomAndButtonHolder>)this.{22718}))
			{
				if (vector.X - this.{22716}.Pos.XY.X + roomAndButtonHolder.Pos.WH.X > num)
				{
					vector.X = this.{22716}.Pos.XY.X + {22687}.p_tab_line2.X;
					vector.Y = this.{22716}.Pos.XY.Y + {22687}.p_tab_line2.Y;
				}
				roomAndButtonHolder.Pos = roomAndButtonHolder.Pos.SetXY(vector);
				vector.X += roomAndButtonHolder.Pos.WH.X + 1f;
			}
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x000B6C28 File Offset: 0x000B4E28
		private void {22708}({22687}.RoomAndButtonHolder {22709})
		{
			this.{22719} = {22709};
			this.{22717}.Clear();
			foreach (LSCacheItem.MessageWithColor {22711} in ((IEnumerable<LSCacheItem.MessageWithColor>){22709}.Item.SavedMessages))
			{
				this.{22710}({22711});
			}
			this.{22717}.ScrollToEnd();
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x000B6C98 File Offset: 0x000B4E98
		private void {22710}(LSCacheItem.MessageWithColor {22711})
		{
			TextBlockBuilder textBlockBuilder = TextBlockBuilder.CreateBlock(450f, "     " + {22711}.Text, {22711}.Color * 0.8f, Fonts.Arial_10, 1f);
			TextBlockControl content = textBlockBuilder.Create();
			this.{22717}.AddItem(new UiControl[]
			{
				content
			});
			if ({22711}.GuildRankId > 0)
			{
				float {11535} = 3f;
				float {11536} = 2f;
				Vector2 vector = Vector2.One * 22f;
				Form form = new Form(new Marker({11535}, {11536}, ref vector), CommonAtlas.c_ranks[(int)({22711}.GuildRankId - 1)], PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AllowMouseInput = true
				};
				string displayName = GuildAccessRightsInfo.AllRights[(int){22711}.GuildRankId].DisplayName;
				form.ExToolTip(new ToolTip(0f, float.MaxValue, new ToolTipState("", displayName, Array.Empty<ToolTipCharacteristics>())));
				content.AddChildPos(form, PositionAlignment.LeftUp, PositionAlignment.LeftUp, 0f, -4f, false);
			}
			content.UpdateComplete += delegate(UiControl {22739})
			{
				{22739}.Brightness = (float)(({22739}.InputMode == MouseInputMode.Focused) ? 5 : 1);
			};
			Action <>9__3;
			Action <>9__4;
			content.EvClick += delegate(ClickUiEventArgs {22745})
			{
				content.BasicColor = Color.SoftLime;
				Action<object> {17476} = delegate(object {22740})
				{
				};
				{17473}.Item[] array = new {17473}.Item[1];
				int num = 0;
				object {17486} = null;
				string memberSocialActions_9c = Local.MemberSocialActions_9c;
				bool {17488} = true;
				ImageDecription {17489} = default(ImageDecription);
				ToolTipState {17490} = null;
				Action {17491};
				if (({17491} = <>9__3) == null)
				{
					{17491} = (<>9__3 = delegate()
					{
						Engine.SetClipboardText({22711}.Text);
					});
				}
				array[num] = new {17473}.Item({17486}, memberSocialActions_9c, {17488}, {17489}, {17490}, {17491});
				UiControl uiControl = new {17473}({17476}, array);
				Action {12894};
				if (({12894} = <>9__4) == null)
				{
					{12894} = (<>9__4 = delegate()
					{
						content.BasicColor = {22711}.Color;
					});
				}
				uiControl.EvRemoveFromContainer += {12894};
			};
			this.{22717}.ScrollToEnd();
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x000B6E1B File Offset: 0x000B501B
		protected override void UserUpdate(ref FrameTime {22712})
		{
			this.{22716}.IsVisible = base.IsVisible;
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x000201BB File Offset: 0x0001E3BB
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(CommonAtlas.Texture);
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x000201CC File Offset: 0x0001E3CC
		protected override void UserFrontRender()
		{
			Engine.GS.ReturnBackTexture();
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000B7021 File Offset: 0x000B5221
		[CompilerGenerated]
		private void {22713}(ClickUiEventArgs {22714})
		{
			if ({22478}.CurrentInstance != null)
			{
				if (this.{22718}.Size > 0)
				{
					{22478}.currentButton = new {22676}(false);
				}
				base.IsVisible = false;
				Global.Game.SceneGame.DecreaseMouse();
				GameScene.DecreaseGameInput();
			}
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x000B705E File Offset: 0x000B525E
		[CompilerGenerated]
		private void {22715}()
		{
			if (base.IsVisible)
			{
				Global.Game.SceneGame.DecreaseMouse();
				GameScene.DecreaseGameInput();
			}
			{22478}.currentLSViewBox = null;
			if (this.{22718}.Size > 0)
			{
				this.SaveLSMessages();
			}
		}

		// Token: 0x04001388 RID: 5000
		private static readonly Color currentPlayerMessageColor = Color.White;

		// Token: 0x04001389 RID: 5001
		private static readonly Color systemMessageColor = Color.Orange;

		// Token: 0x0400138A RID: 5002
		private const int maxMessageLength = 120;

		// Token: 0x0400138B RID: 5003
		private static readonly ExpandoTexturePath main = ExpandoTexturePath.CreateBox(new Rectangle(1782, 0, 243, 190), new Rectangle(1782, 190, 243, 190), new Rectangle(2025, 0, 243, 190), new Rectangle(2025, 190, 243, 190));

		// Token: 0x0400138C RID: 5004
		private static readonly Rectangle c_hand_resize = new Rectangle(2269, 0, 24, 24);

		// Token: 0x0400138D RID: 5005
		private static readonly Rectangle c_hand_move = new Rectangle(2269, 25, 24, 24);

		// Token: 0x0400138E RID: 5006
		private static readonly Rectangle c_hand_close = new Rectangle(2269, 63, 30, 30);

		// Token: 0x0400138F RID: 5007
		private static readonly Rectangle c_tabItem_closeButton = new Rectangle(2269, 50, 12, 12);

		// Token: 0x04001390 RID: 5008
		private static readonly Rectangle c_tabItem_optionsButton = new Rectangle(2282, 50, 12, 12);

		// Token: 0x04001391 RID: 5009
		protected static readonly ExpandoTextureLinePath textBoxTex = ExpandoTextureLinePath.CreateLine(new Rectangle(1782, 381, 225, 68), new Rectangle(2007, 381, 226, 68));

		// Token: 0x04001392 RID: 5010
		protected static readonly ExpandoTextureLinePath c_tabItemNormal = ExpandoTextureLinePath.CreateLine(new Rectangle(2294, 0, 92, 18), new Rectangle(2386, 0, 92, 18));

		// Token: 0x04001393 RID: 5011
		protected static readonly ExpandoTextureLinePath c_tabItemSelected = ExpandoTextureLinePath.CreateLine(new Rectangle(2294, 19, 92, 18), new Rectangle(2386, 19, 92, 18));

		// Token: 0x04001394 RID: 5012
		protected static readonly ExpandoTextureLinePath c_tabItemLight = ExpandoTextureLinePath.CreateLine(new Rectangle(2479, 19, 92, 18), new Rectangle(2571, 19, 92, 18));

		// Token: 0x04001395 RID: 5013
		private static readonly Vector2 p_contentStart = new Vector2(17f, 14f);

		// Token: 0x04001396 RID: 5014
		private static readonly Vector2 p_contentEndOffsetFromEnd = new Vector2(18f, 16f);

		// Token: 0x04001397 RID: 5015
		private static readonly Vector2 p_tab_line1 = new Vector2(58f, 15f);

		// Token: 0x04001398 RID: 5016
		private static readonly Vector2 p_tab_line2 = new Vector2(58f, 34f);

		// Token: 0x04001399 RID: 5017
		private UnionFormControl {22716};

		// Token: 0x0400139A RID: 5018
		private ListItemViewControl {22717};

		// Token: 0x0400139B RID: 5019
		private Tlist<{22687}.RoomAndButtonHolder> {22718};

		// Token: 0x0400139C RID: 5020
		private {22687}.RoomAndButtonHolder {22719};

		// Token: 0x0400139D RID: 5021
		private ScrollBarControl {22720};

		// Token: 0x020003E1 RID: 993
		protected class RoomAndButtonHolder : CustomUi
		{
			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x060015B5 RID: 5557 RVA: 0x000B7096 File Offset: 0x000B5296
			public bool IsCurrent
			{
				get
				{
					return this.{22738};
				}
			}

			// Token: 0x060015B6 RID: 5558 RVA: 0x000B70A0 File Offset: 0x000B52A0
			internal RoomAndButtonHolder(Vector2 {22723}, LSCacheItem {22724})
			{
				Vector2 vector = new Vector2(({22687}.RoomAndButtonHolder.textSizeHolder = Fonts.Arial_10.Measure({22724}.TargetName)).X + (float)({22687}.c_tabItem_closeButton.Width * 2) + 10f, 18f);
				base..ctor(new Marker(ref {22723}, ref vector), CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp, Color.Transparent, false);
				this.Item = {22724};
				this.{22734} = {22687}.RoomAndButtonHolder.textSizeHolder;
				Label label = new Label(base.Pos.XY + new Vector2(4f, 2f), Fonts.Arial_10, Color.White * 0.8f, {22724}.TargetName, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				base.AddChild(new UiControl[]
				{
					label,
					this.{22736} = new Button(base.Pos.XY + new Vector2(base.Pos.WH.X - 4f - (float){22687}.c_tabItem_closeButton.Width, 3f), {22687}.c_tabItem_closeButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp),
					this.{22737} = new Button(base.Pos.XY + new Vector2(base.Pos.WH.X - 4f - (float){22687}.c_tabItem_closeButton.Width - (float){22687}.c_tabItem_optionsButton.Width, 3f), {22687}.c_tabItem_optionsButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				label.UpdateComplete += this.{22726};
				base.EvClick += this.{22728};
				this.{22736}.EvClick += this.{22730};
				this.{22737}.EvClick += this.{22732};
				this.SetAsDefault();
			}

			// Token: 0x060015B7 RID: 5559 RVA: 0x000B7274 File Offset: 0x000B5474
			public void SetAsCurrent()
			{
				this.{22735} = {22687}.c_tabItemSelected;
				this.{22736}.IsVisible = (this.{22737}.IsVisible = true);
				this.{22738} = true;
				this.Item.NewMessages = false;
				{22478}.currentLSViewBox.LoadRoomContent(this);
			}

			// Token: 0x060015B8 RID: 5560 RVA: 0x000B72C4 File Offset: 0x000B54C4
			public void SetAsDefault()
			{
				this.{22735} = {22687}.c_tabItemNormal;
				this.{22736}.IsVisible = (this.{22737}.IsVisible = false);
				this.{22738} = false;
			}

			// Token: 0x060015B9 RID: 5561 RVA: 0x000B72FD File Offset: 0x000B54FD
			public void SetLightEffect()
			{
				if (this.{22738})
				{
					return;
				}
				this.Item.NewMessages = true;
			}

			// Token: 0x060015BA RID: 5562 RVA: 0x000B7314 File Offset: 0x000B5514
			protected override void UserBackRender()
			{
				this.{22735}.Render(base.Pos, Color.White);
				if (this.Item.NewMessages)
				{
					{22687}.c_tabItemLight.Render(base.Pos, Color.White * (float)(0.5 + 0.5 * Math.Sin(Global.Game.GameTotalTimeSec * 4.0)));
				}
			}

			// Token: 0x060015BB RID: 5563 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserFrontRender()
			{
			}

			// Token: 0x060015BC RID: 5564 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserUpdate(ref FrameTime {22725})
			{
			}

			// Token: 0x060015BD RID: 5565 RVA: 0x000B738C File Offset: 0x000B558C
			[CompilerGenerated]
			private void {22726}(UiControl {22727})
			{
				{22727}.BasicColor = (this.Item.IsOnline ? Color.SoftLime : (Color.White * 0.8f));
			}

			// Token: 0x060015BE RID: 5566 RVA: 0x000B73B7 File Offset: 0x000B55B7
			[CompilerGenerated]
			private void {22728}(ClickUiEventArgs {22729})
			{
				this.SetAsCurrent();
			}

			// Token: 0x060015BF RID: 5567 RVA: 0x000B73BF File Offset: 0x000B55BF
			[CompilerGenerated]
			private void {22730}(ClickUiEventArgs {22731})
			{
				{22478}.currentLSViewBox.CloseRoom(this);
			}

			// Token: 0x060015C0 RID: 5568 RVA: 0x000B73CC File Offset: 0x000B55CC
			[CompilerGenerated]
			private void {22732}(ClickUiEventArgs {22733})
			{
				new {17558}(new {17549}(this.Item.TargetSID, this.Item.TargetName, Array.Empty<{17549}.OptionalAction>()));
			}

			// Token: 0x0400139E RID: 5022
			internal readonly LSCacheItem Item;

			// Token: 0x0400139F RID: 5023
			private static Vector2 textSizeHolder;

			// Token: 0x040013A0 RID: 5024
			private Vector2 {22734};

			// Token: 0x040013A1 RID: 5025
			private ExpandoTextureLinePath {22735};

			// Token: 0x040013A2 RID: 5026
			private Button {22736};

			// Token: 0x040013A3 RID: 5027
			private Button {22737};

			// Token: 0x040013A4 RID: 5028
			private bool {22738};
		}
	}
}
