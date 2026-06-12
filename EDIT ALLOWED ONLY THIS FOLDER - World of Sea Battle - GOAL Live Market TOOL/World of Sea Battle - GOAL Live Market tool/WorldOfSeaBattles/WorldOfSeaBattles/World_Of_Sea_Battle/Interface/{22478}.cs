using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Components.Client;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020003C3 RID: 963
	internal sealed class {22478} : CustomUi
	{
		// Token: 0x060014EA RID: 5354 RVA: 0x000AFE0C File Offset: 0x000AE00C
		public static void Put(in OnChatMessageEvent {22480})
		{
			if (Global.Settings.ChatOnlyMyLanguage && {22480}.Room != ChatRoomType.Special && {22480}.Room != ChatRoomType.GlobalModerator && {22480}.Room != ChatRoomType.GlobalSystem && !{22480}.IsLocalized && !{22478}.<Put>g__IsMyLanguage|0_0({22480}.Payload1.Language))
			{
				OnChatMessageEvent onChatMessageEvent = {22480};
				if (!onChatMessageEvent.Message.Contains(Session.Account.PlayerName))
				{
					return;
				}
			}
			if ({22478}.CurrentInstance != null)
			{
				{22478}.CurrentInstance.MessageHandler({22480}, true);
				return;
			}
			Tlist<ValueTuple<bool, OnChatMessageEvent>> chatOffCache = Session.ChatOffCache;
			ValueTuple<bool, OnChatMessageEvent> valueTuple = new ValueTuple<bool, OnChatMessageEvent>(true, {22480});
			chatOffCache.Add(valueTuple);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x000AFEA8 File Offset: 0x000AE0A8
		public static void WriteSystemMessage(string {22481}, bool {22482} = false)
		{
			{22478} {22478} = {22478}.CurrentInstance;
			if ({22478} == null)
			{
				return;
			}
			ChatRoomType {10417} = {22482} ? ChatRoomType.Special : Session.SelectedChatRoom;
			ChatMessageDefault chatMessageDefault = new ChatMessageDefault(Local.ChatBoxGui_7, {22481}, 0U, LocaleInfo.Current.Id);
			OnChatMessageEvent onChatMessageEvent = new OnChatMessageEvent({10417}, ref chatMessageDefault);
			{22478}.MessageHandler(onChatMessageEvent, true);
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x000AFEF1 File Offset: 0x000AE0F1
		public static bool SpecialMessage(uint {22483}, string {22484})
		{
			return {22478}.currentLSViewBox != null && {22478}.currentLSViewBox.PutSpecialMessage({22483}, {22484});
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x000AFF08 File Offset: 0x000AE108
		public static void AddMessage(uint {22485}, string {22486}, string {22487}, bool {22488}, string {22489}, byte {22490})
		{
			{22478}.TryOpen({22485}, {22486}, false, {22488}, {22487});
			{22478}.currentLSViewBox.AddMessage({22485}, {22487}, {22478}.twinPlayerMessageColor, false, {22489}, {22490});
			{22478}.currentLSViewBox.UpdateOnlineStatus({22485}, {22488});
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x000AFF38 File Offset: 0x000AE138
		public static void TryOpen(uint {22491}, string {22492}, bool {22493}, bool {22494}, string {22495})
		{
			if ({22478}.currentLSViewBox == null)
			{
				{22478}.currentLSViewBox = new {22687}();
				{22478}.currentLSViewBox.IsVisible = false;
				if ({22493})
				{
					{22478}.currentLSViewBox.IsVisible = true;
					{22478}.currentLSViewBox.GotFocus();
				}
			}
			{22478}.currentLSViewBox.AddOrUpdateRoom({22491}, {22492}, {22494});
			if (!{22478}.currentLSViewBox.IsVisible)
			{
				{22478}.AddOrBlinkButton({22495});
			}
			Session.LsMessageDraft = string.Empty;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x000AFFA3 File Offset: 0x000AE1A3
		private static void AddOrBlinkButton(string {22496})
		{
			if ({22478}.currentButton != null)
			{
				{22478}.currentButton.SetBlinked({22496});
				return;
			}
			{22478}.currentButton = new {22676}(Session.LSEventButtonState || !string.IsNullOrEmpty({22496}));
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x000AFFD8 File Offset: 0x000AE1D8
		private static void LoadLSFromCache()
		{
			if (Global.Settings.LsSavedCacheData.Cache.Size == 0)
			{
				return;
			}
			{22478}.currentLSViewBox = new {22687}();
			{22478}.currentLSViewBox.IsVisible = false;
			{22478}.AddOrBlinkButton("");
			Session.LSEventButtonState = false;
			{22478}.currentLSViewBox.LoadCache();
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x000B002B File Offset: 0x000AE22B
		public static {22478} CurrentInstance
		{
			get
			{
				return {22478}.currentInstance;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x000B0032 File Offset: 0x000AE232
		public static int DefaultHeight
		{
			get
			{
				return {22478}.p_mainBox.Height;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x000B0032 File Offset: 0x000AE232
		public static int DefaultWidth
		{
			get
			{
				return {22478}.p_mainBox.Height;
			}
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x000B003E File Offset: 0x000AE23E
		public static Rectangle GetNotificationPath(int {22497})
		{
			if ({22497} == 0)
			{
				return new Rectangle(319, 64, 21, 14);
			}
			return new Rectangle(319 + Math.Min({22497} - 1, 7) * 22, 79, 21, 14);
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060014F5 RID: 5365 RVA: 0x000B0074 File Offset: 0x000AE274
		// (remove) Token: 0x060014F6 RID: 5366 RVA: 0x000B00AC File Offset: 0x000AE2AC
		public event Action GotInput
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{22525};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{22525}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{22525};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{22525}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060014F7 RID: 5367 RVA: 0x000B00E4 File Offset: 0x000AE2E4
		// (remove) Token: 0x060014F8 RID: 5368 RVA: 0x000B011C File Offset: 0x000AE31C
		public event Action LeaveInput
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{22526};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{22526}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{22526};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{22526}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x000B0151 File Offset: 0x000AE351
		public bool IsInput
		{
			get
			{
				return this.{22528}.IsEnter;
			}
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x000B0160 File Offset: 0x000AE360
		public {22478}(bool {22502}) : base(new Marker(0f, 0f, ref {22478}.p_mainBox), Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.RightDown, Color.White, false)
		{
			{22478} <>4__this = this;
			this.{22503}();
			this.{22542} = Global.Settings.BigChat;
			this.{22543} = Global.Settings.BigChatFont;
			this.{22539} = (Global.Settings.BigChat ? 1.2f : 1f);
			new UiOpacityAnimation(this, 0f, 1f, 150f);
			{22478}.currentInstance = this;
			this.AnimatedFocus = false;
			this.{22534} = {22502};
			this.{22528} = new TextBox(base.Pos.XY + {22478}.v_input, {22478}.p_input_passive, Fonts.Arial_10, Color.White, {22478}.WhitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				PositionAlignment_Y = PositionAlignment.RightDown,
				PositionAlignment_X = PositionAlignment.Both
			};
			this.{22528}.EvLostEnter += delegate()
			{
				<>4__this.{22512}(true, InputHelper.NowInputState.IsDown(Keys.Escape));
				<>4__this.{22540} = 1f;
			};
			this.{22528}.EvGotEnter += delegate()
			{
				<>4__this.{22528}.IsEnter = true;
				<>4__this.{22528}.Multitexture = {22478}.p_input_active;
				if (!{22502})
				{
					Global.Game.SceneGame.IncreaseMouse();
					GameScene.IncreaseGameInput();
				}
				for (int i = 0; i < <>4__this.{22529}.Size; i++)
				{
					Tlist<{22478}.ChatItem> items = <>4__this.{22529}.Array[i].ItemView.Items;
					for (int j = 0; j < items.Size; j++)
					{
						items.Array[j].Alive();
					}
				}
				Action action = <>4__this.{22525};
				if (action != null)
				{
					action();
				}
				<>4__this.ShowScrollTooltip();
				<>4__this.{22539} = 1.2f;
				foreach ({22478}.RoomButton roomButton3 in ((IEnumerable<{22478}.RoomButton>)<>4__this.{22529}))
				{
					roomButton3.ItemView.RecomposeAll(true, (int)(24f * <>4__this.{22539} + 8f));
				}
			};
			this.{22528}.AttachMaxLengthModerator(112, null, Color.Transparent);
			this.{22529} = new Tlist<{22478}.RoomButton>(4);
			{22478}.RoomButton roomButton = new {22478}.RoomButton(this, Vector2.Zero, Local.ChatBoxGui_3, ChatRoomType.Global);
			this.{22530} = new {22478}.RoomButton(this, Vector2.Zero, Local.ChatBoxGui_4, ChatRoomType.Group);
			this.{22533} = new {22478}.RoomButton(this, Vector2.Zero, Local.allies, ChatRoomType.Map);
			this.{22531} = new {22478}.RoomButton(this, Vector2.Zero, Local.guild, ChatRoomType.Guild);
			this.{22532} = new {22478}.RoomButton(this, Vector2.Zero, Local.logbook, {22478}.LogbookRoom);
			if (Global.Player.IsDestroyed)
			{
				roomButton.IsVisible = false;
				if (Session.SelectedChatRoom == ChatRoomType.Global)
				{
					this.{22535} = true;
					Session.SelectedChatRoom = ((Session.Guild != null) ? ChatRoomType.Guild : {22478}.LogbookRoom);
				}
			}
			this.{22529}.Add(roomButton);
			this.{22529}.Add(this.{22530});
			this.{22529}.Add(this.{22533});
			this.{22529}.Add(this.{22531});
			this.{22529}.Add(this.{22532});
			this.{22522}();
			base.AddChild(this.{22528});
			foreach ({22478}.RoomButton roomButton2 in ((IEnumerable<{22478}.RoomButton>)this.{22529}))
			{
				roomButton2.EvClick += this.{22516};
			}
			this.{22512}(false, true);
			this.{22518}(Session.SelectedChatRoom);
			Session.EvGroupChanged += this.{22520};
			base.EvRemoveFromContainer += delegate()
			{
				Session.ChatOffCache.Clear();
				foreach ({22478}.RoomButton roomButton3 in ((IEnumerable<{22478}.RoomButton>)<>4__this.{22529}))
				{
					roomButton3.ItemView.SaveToCache();
				}
				{22676} {22676} = {22478}.currentButton;
				if ({22676} != null)
				{
					{22676}.RemoveFromContainer();
				}
				{22687} {22687} = {22478}.currentLSViewBox;
				if ({22687} != null)
				{
					{22687}.RemoveFromContainer();
				}
				<>4__this.{22512}(true, true);
				{22478}.currentInstance = null;
				Session.EvGroupChanged -= <>4__this.{22520};
				if (<>4__this.{22535})
				{
					Session.SelectedChatRoom = ChatRoomType.Global;
				}
			};
			this.{22536} = new EventCounter(0.083333336f, 0f);
			this.{22537} = new EventCounter(0.16666667f, 0f);
			this.{22538} = new EventCounter(1.6666666f, 0f);
			{22478}.LoadLSFromCache();
			this.{22541} = new Label(Vector2.Zero, Fonts.Arial_9, Color.White * 0.4f, " ", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.EvRemoveFromContainer += delegate()
			{
				<>4__this.{22541}.RemoveFromContainer();
			};
			if (Session.ChatOffCache.Size == 0)
			{
				{22478}.WriteSystemMessage(Local.ChatBoxGui_8, false);
				return;
			}
			foreach (ValueTuple<bool, OnChatMessageEvent> valueTuple in ((IEnumerable<ValueTuple<bool, OnChatMessageEvent>>)Session.ChatOffCache))
			{
				this.MessageHandler(valueTuple.Item2, valueTuple.Item1);
			}
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x000B0524 File Offset: 0x000AE724
		private void {22503}()
		{
			if (this.{22534})
			{
				if (this.{22528} != null)
				{
					this.{22528}.LineMaxSizeMode = DirectionDef.Left;
				}
				base.Pos = new Marker((float)Engine.GS.UIArea.Width - (float){22478}.p_mainBox.Width * this.{22539} - 9f + 15f, (float)(Engine.GS.UIArea.Height - 400 + 55 + 87 + 13 + 232 + 7) - (float){22478}.p_mainBox.Height * this.{22539}, (float){22478}.p_mainBox.Width * this.{22539}, (float){22478}.p_mainBox.Height * this.{22539});
				return;
			}
			if (this.{22528} != null)
			{
				this.{22528}.LineMaxSizeMode = DirectionDef.Right;
			}
			base.Pos = new Marker(6f, (float)(Engine.GS.UIArea.Height - 95) - (float){22478}.p_mainBox.Height * this.{22539}, (float){22478}.p_mainBox.Width * this.{22539}, (float){22478}.p_mainBox.Height * this.{22539});
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x000B0658 File Offset: 0x000AE858
		protected override void UserUpdate(ref FrameTime {22504})
		{
			if (this.{22543} != Global.Settings.BigChatFont)
			{
				foreach ({22478}.RoomButton roomButton in ((IEnumerable<{22478}.RoomButton>)this.{22529}))
				{
					roomButton.ItemView.RecomposeAll(false, 24);
				}
				this.{22543} = Global.Settings.BigChatFont;
			}
			if (this.{22542} != Global.Settings.BigChat)
			{
				this.{22539} = (Global.Settings.BigChat ? 1.2f : 1f);
				this.{22542} = Global.Settings.BigChat;
			}
			this.{22503}();
			if (Session.SelectedChatRoom == {22478}.LogbookRoom)
			{
				this.{22528}.Text = "";
			}
			if (this.{22540} > 0f && !Global.Settings.BigChat)
			{
				if (base.InputMode != MouseInputMode.NoFocus)
				{
					this.{22540} = 1000f;
				}
				if ({22504}.EvaluteTimerMs2(ref this.{22540}))
				{
					this.{22539} = 1f;
					foreach ({22478}.RoomButton roomButton2 in ((IEnumerable<{22478}.RoomButton>)this.{22529}))
					{
						roomButton2.ItemView.RecomposeAll(false, 24);
					}
				}
			}
			{22504}.EvaluteTimerMs(ref this.{22527});
			if (Global.Settings.kb_ChatEnter.IsClick && this.{22527} == 0f && {17312}.CurrentInstance == null && !{19994}.HasBlockingKey(Global.Settings.kb_ChatEnter.Key))
			{
				base.IsVisible = true;
				if (this.{22528}.IsEnter)
				{
					if (string.IsNullOrWhiteSpace(this.{22528}.Text))
					{
						this.{22512}(true, true);
						return;
					}
					if (this.{22536}.Count() <= 3 && this.{22537}.Count() <= 4 && this.{22538}.Count() <= 10)
					{
						this.{22536}.AddEvent();
						this.{22537}.AddEvent();
						this.{22538}.AddEvent();
						this.{22510}(this.{22528}.Text);
						this.{22512}(true, true);
					}
					else
					{
						{22478}.WriteSystemMessage(Local.ChatBoxGui_9, false);
					}
				}
				else if (GameScene.GameHasInputFocus)
				{
					this.{22528}.IsEnter = true;
					this.{22515}();
				}
			}
			else if (this.{22528}.IsEnter && Global.Settings.kb_Escape.IsClick)
			{
				this.{22512}(true, true);
			}
			bool isVisible = this.{22533}.IsVisible;
			this.{22533}.IsVisible = (Session.CurrentArenaSession != null && !Session.CurrentArenaSession.ModeInfo.IsDuetl);
			this.{22530}.IsVisible = (Global.Player != null && Session.Group != null && !this.{22533}.IsVisible);
			if (!isVisible && this.{22533}.IsVisible)
			{
				this.{22518}(ChatRoomType.Map);
			}
			this.{22531}.IsVisible = (Session.Guild != null);
			this.{22522}();
			if (this.{22528}.IsEnter && this.{22528}.Text.Length == 0)
			{
				if (InputHelper.IsClick(Keys.Right))
				{
					int i = -1;
					for (int j = 0; j < this.{22529}.Size; j++)
					{
						if (this.{22529}.Array[j].ChatRoom == Session.SelectedChatRoom)
						{
							i = j;
						}
					}
					for (i++; i < this.{22529}.Size; i++)
					{
						if (this.{22529}.Array[i].IsVisible)
						{
							IL_38C:
							this.{22518}(this.{22529}.Array[i].ChatRoom);
							goto IL_47A;
						}
					}
					i = 0;
					goto IL_38C;
				}
				if (InputHelper.IsClick(Keys.Left))
				{
					int k = -1;
					for (int l = 0; l < this.{22529}.Size; l++)
					{
						if (this.{22529}.Array[l].ChatRoom == Session.SelectedChatRoom)
						{
							k = l;
						}
					}
					for (k--; k > -1; k--)
					{
						if (this.{22529}.Array[k].IsVisible)
						{
							IL_462:
							this.{22518}(this.{22529}.Array[k].ChatRoom);
							goto IL_47A;
						}
					}
					for (int m = 0; m < this.{22529}.Size; m++)
					{
						if (this.{22529}.Array[this.{22529}.Size - m - 1].IsVisible)
						{
							k = this.{22529}.Size - m - 1;
							break;
						}
					}
					goto IL_462;
				}
			}
			IL_47A:
			if (Global.Settings.kb_ChatHide.IsClick && GameScene.GameHasInputFocus && !this.{22528}.IsEnter)
			{
				Session.ChatIsHidden = !Session.ChatIsHidden;
			}
			base.IsVisible = (!Session.ChatIsHidden && {22913}.CurrentInstance == null && Global.Render.UiMode != InterfaceMode.Off);
			if (Global.Render.UiMode == InterfaceMode.Cinematografic)
			{
				base.Opacity = Renderer.UiOpacityToFocus(base.Pos);
			}
			else
			{
				base.Opacity = 1f;
			}
			if ({22478}.currentButton != null)
			{
				{22478}.currentButton.Pos = {22478}.currentButton.Pos.SetXY(base.Pos.XY.X - 5f, base.Pos.XY.Y - (float){22676}.c_active.Height - 2f);
			}
			UiControl uiControl = this.{22541};
			Marker pos = this.{22541}.Pos;
			Vector2 vector = new Vector2(base.Pos.XY.X + 6f, base.Pos.End.Y - 20f);
			uiControl.Pos = pos.SetXY(vector);
			this.{22541}.PositionAlignment_Y = PositionAlignment.RightDown;
			if (this.{22527} != 0f)
			{
				this.{22541}.Text = Local.ChatBoxGui_10 + ((int)Math.Ceiling((double)(this.{22527} / 1000f))).ToString();
			}
			else if (this.{22528}.Text.Length > 0 || this.{22528}.IsEnter)
			{
				this.{22541}.Text = "";
			}
			else if (base.IsVisible)
			{
				this.{22541}.Text = Global.Settings.kb_ChatEnter.KeyToString + Local.ChatBoxGui_11 + Global.Settings.kb_ChatHide.KeyToString + Local.ChatBoxGui_12;
			}
			else
			{
				this.{22541}.Text = "[" + Global.Settings.kb_ChatHide.KeyToString + Local.ChatBoxGui_13;
			}
			this.{22541}.IsVisible = (Global.Render.UiMode == InterfaceMode.Default);
			if (InputHelper.LeftWasClicked && base.InputMode == MouseInputMode.NoFocus)
			{
				this.{22515}();
			}
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x000B0D4C File Offset: 0x000AEF4C
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(OtherTextures.ChatAtl);
			Device gs = Engine.GS;
			Rectangle rectangle = new Marker(base.Pos.XY.X, base.Pos.XY.Y + base.Pos.WH.Y - (float){22478}.p_mainBox.Height, base.Pos.WH.X, (float){22478}.p_mainBox.Height).ToRect();
			Color color = Color.White * base.GetOpcaity();
			gs.Draw({22478}.p_mainBox, rectangle, color);
			if (this.IsInput)
			{
				Device gs2 = Engine.GS;
				rectangle = base.Pos.Resize(-10f, 0f).ToRect();
				color = Color.Black * base.GetOpcaity() * 0.5f;
				gs2.Draw({22478}.WhitePixel, rectangle, color);
			}
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x000201CC File Offset: 0x0001E3CC
		protected override void UserFrontRender()
		{
			Engine.GS.ReturnBackTexture();
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x000B0E4C File Offset: 0x000AF04C
		public void MessageHandler(in OnChatMessageEvent {22505}, bool {22506})
		{
			if ({22505}.Room == ChatRoomType.GlobalModerator || {22505}.Room == ChatRoomType.GlobalSystem)
			{
				{22478}.RoomButton roomButton = this.{22529}.Array[0];
				if (!{22505}.IsLocalized)
				{
					string text = {22505}.Payload1.Message;
					if (text.StartsWith("|RAW"))
					{
						string[] array = (from {22583} in text.Split('|', StringSplitOptions.None)
						where !string.IsNullOrEmpty({22583})
						select {22583}).ToArray<string>();
						if (array.Length == 3)
						{
							string str = Local.NetworkManager_13_reason(Sanction.GetLocalizedReason(array[0]));
							string str2 = array[1];
							string text2 = StringHelper.TimeDHM((double)int.Parse(array[2]), false);
							object {859} = str2 + ",";
							object {860};
							if (!text2.EndsWith('.'))
							{
								{860} = text2;
							}
							else
							{
								string text3 = text2;
								{860} = text3.Substring(0, text3.Length - 1);
							}
							text = Local.NetworkManager_13({859}, {860}) + str;
							OnChatMessageEvent {22563} = {22505};
							{22563}.Payload1.Message = text;
							roomButton.ItemView.Add({22563});
							return;
						}
					}
				}
				roomButton.ItemView.Add({22505});
				return;
			}
			for (int i = 0; i < this.{22529}.Size; i++)
			{
				{22478}.RoomButton roomButton2 = this.{22529}.Array[i];
				if (roomButton2.ChatRoom == {22505}.Room || {22505}.Room == ChatRoomType.Special)
				{
					roomButton2.ItemView.Add({22505});
					if (roomButton2.ChatRoom != Session.SelectedChatRoom && roomButton2.ChatRoom != {22478}.LogbookRoom && {22505}.Room != ChatRoomType.Special && {22506} && !{22505}.IsOutdated)
					{
						roomButton2.IncrementSignal();
					}
				}
			}
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x000B1008 File Offset: 0x000AF208
		public void SpecialMessage(GroupCommandId {22507}, string {22508}, string {22509})
		{
			if (string.IsNullOrEmpty({22508}))
			{
				throw new ArgumentNullException();
			}
			GroupCommandInfo groupCommandInfo = new GroupCommandInfo({22507});
			string text = groupCommandInfo.DisplayName;
			if (groupCommandInfo.HasTarget)
			{
				text += {22509};
			}
			ChatMessageDefault chatMessageDefault = new ChatMessageDefault(groupCommandInfo.NpcCommand ? Local.ChatBoxGui_0B({22508}) : Local.ChatBoxGui_0({22508}), text, 0U, LocaleInfo.Current.Id);
			for (int i = 0; i < this.{22529}.Size; i++)
			{
				this.{22529}.Array[i].ItemView.Add(new OnChatMessageEvent(ChatRoomType.Group, ref chatMessageDefault));
			}
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x000B10A4 File Offset: 0x000AF2A4
		private void {22510}(string {22511})
		{
			if (Session.SelectedChatRoom == {22478}.LogbookRoom)
			{
				return;
			}
			DateTime nowServerTime = LocalizedDateTime.NowServerTime;
			Sanction sanction;
			if (Session.SelectedChatRoom == ChatRoomType.Global && Session.Account.Sanctions.IsChatBlocked(ref nowServerTime, out sanction))
			{
				string text = sanction.GetLocalizedReason();
				if (!string.IsNullOrEmpty(text))
				{
					text = Local.NetworkManager_13_reason(text);
				}
				if (sanction.IsDecreasingWhenOnline)
				{
					{22478}.WriteSystemMessage(Local.infoBlockedGlobalOnline(StringHelper.TimeDHM(sanction.OnlineTimeLeftSec, false)) + text, false);
					return;
				}
				if (sanction.IsIndefinite)
				{
					string chatBoxGui_ = Local.ChatBoxGui_10;
					{22478}.WriteSystemMessage(chatBoxGui_.Substring(0, chatBoxGui_.Length - 1) + text, false);
					return;
				}
				{22478}.WriteSystemMessage(Local.infoBlockedGlobal(new LocalizedDateTime(sanction.ServerEndTime, false).GetDateAndTime()) + text, false);
				return;
			}
			else
			{
				if (Session.Account.Sanctions.IsLsAndGroupBlocked(ref nowServerTime, out sanction))
				{
					string text2 = sanction.GetLocalizedReason();
					if (!string.IsNullOrEmpty(text2))
					{
						text2 = Local.NetworkManager_13_reason(text2);
					}
					{22478}.WriteSystemMessage(Local.infoBlockedAllChats(new LocalizedDateTime(sanction.ServerEndTime, false).GetDateAndTime()) + text2, true);
					return;
				}
				if (Session.SelectedChatRoom != ChatRoomType.Guild && Session.SelectedChatRoom != ChatRoomType.Group && Session.SelectedChatRoom != ChatRoomType.Map && !ChatCensure.CheckIsNormal({22511}))
				{
					{22478}.WriteSystemMessage(Local.ChatBoxGui_autoban1, false);
					this.{22527} = 60000f;
					return;
				}
				if (ChatCensure.CheatWordWarning({22511}))
				{
					{22478}.WriteSystemMessage(Local.ChatBoxGui_autoban2, false);
					this.{22527} = 20000f;
					return;
				}
				if ({22511}.SplitSmart(new char[]
				{
					' '
				}).Any((string {22584}) => {22584}.Length > 24))
				{
					{22478}.WriteSystemMessage(Local.ChatBoxGui_2, false);
					return;
				}
				{22511} = ChatCensure.CapsFilter({22511});
				string text3 = Session.Account.PlayerName;
				if (Session.Guild != null)
				{
					text3 = "[" + Session.Guild.Tag + "] " + text3;
				}
				for (int i = 0; i < this.{22529}.Size; i++)
				{
					{22478}.RoomButton roomButton = this.{22529}.Array[i];
					if (roomButton.ChatRoom == Session.SelectedChatRoom)
					{
						{22478}.ChatItemView itemView = roomButton.ItemView;
						ChatRoomType chatRoom = roomButton.ChatRoom;
						ChatMessageDefault chatMessageDefault = new ChatMessageDefault(text3, {22511}, Session.Account.SID, LocaleInfo.Current.Id);
						itemView.Add(new OnChatMessageEvent(chatRoom, ref chatMessageDefault));
						NetworkManager network = Global.Network;
						ChatRoomType selectedChatRoom = Session.SelectedChatRoom;
						chatMessageDefault = new ChatMessageDefault(string.Empty, ChatCensure.CapsFilter(this.{22528}.Text), 0U, LocaleInfo.Current.Id);
						network.Send(new OnChatMessageEvent(selectedChatRoom, ref chatMessageDefault));
						return;
					}
				}
				return;
			}
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x000B1354 File Offset: 0x000AF554
		internal void ShowScrollTooltip()
		{
			Form form = new Form(base.Pos, {22478}.p_scrollToolTip, base.PositionAlignment_X, base.PositionAlignment_Y);
			form.AnimatedFocus = false;
			new UiOpacityAnimation(form, 1f, 0f, 500f);
			new UiRemoveAction(form);
			base.AddChild(form);
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x000B13AC File Offset: 0x000AF5AC
		private void {22512}(bool {22513} = true, bool {22514} = true)
		{
			if ({22514})
			{
				this.{22528}.Text = string.Empty;
			}
			this.{22528}.IsEnter = false;
			this.{22528}.Multitexture = null;
			if ({22513})
			{
				if (!this.{22534})
				{
					Global.Game.SceneGame.DecreaseMouse();
					GameScene.DecreaseGameInput();
				}
				Action action = this.{22526};
				if (action == null)
				{
					return;
				}
				action();
			}
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x000B1414 File Offset: 0x000AF614
		private void {22515}()
		{
			for (int i = 0; i < this.{22529}.Size; i++)
			{
				this.{22529}.Array[i].scroller.Reset();
				this.{22529}.Array[i].ItemView.SetScroll(0f);
			}
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x000B146C File Offset: 0x000AF66C
		private void {22516}(ClickUiEventArgs {22517})
		{
			{22478}.RoomButton roomButton = ({22478}.RoomButton){22517}.Sender;
			if (roomButton.ChatRoom != Session.SelectedChatRoom)
			{
				this.{22518}(roomButton.ChatRoom);
			}
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x000B14A0 File Offset: 0x000AF6A0
		private void {22518}(ChatRoomType {22519})
		{
			Session.SelectedChatRoom = {22519};
			for (int i = 0; i < this.{22529}.Size; i++)
			{
				{22478}.RoomButton roomButton = this.{22529}.Array[i];
				if (roomButton.ChatRoom == {22519})
				{
					roomButton.Set();
				}
				else
				{
					roomButton.Reset();
				}
			}
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x000B14F0 File Offset: 0x000AF6F0
		private void {22520}(GroupCommon {22521})
		{
			this.{22530}.IsVisible = ({22521} != null);
			if ({22521} != null)
			{
				this.{22518}(ChatRoomType.Group);
				ChatRoomType {10417} = ChatRoomType.Group;
				ChatMessageDefault chatMessageDefault = new ChatMessageDefault(Local.ChatBoxGui_15, Local.ChatBoxGui_16, 0U, LocaleInfo.Current.Id);
				OnChatMessageEvent onChatMessageEvent = new OnChatMessageEvent({10417}, ref chatMessageDefault);
				this.MessageHandler(onChatMessageEvent, true);
				return;
			}
			this.{22530}.CleanRoom();
			this.{22518}(ChatRoomType.Global);
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x000B1556 File Offset: 0x000AF756
		public void GuildChanged()
		{
			this.{22531}.ItemView.Clear();
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x000B1568 File Offset: 0x000AF768
		public void MapChanged()
		{
			this.{22533}.IsVisible = false;
			this.{22533}.CleanRoom();
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x000B1584 File Offset: 0x000AF784
		private void {22522}()
		{
			float x = base.Pos.XY.X + 1f;
			Marker pos = base.Pos;
			Vector2 vector = new Vector2(x, pos.End.Y - 42f);
			for (int i = 0; i < this.{22529}.Size; i++)
			{
				{22478}.RoomButton roomButton = this.{22529}.Array[i];
				UiControl uiControl = roomButton;
				pos = roomButton.Pos;
				uiControl.Pos = new Marker(ref vector, ref pos.WH);
				if (roomButton.IsVisible)
				{
					vector.X += roomButton.Pos.WH.X;
				}
			}
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x000B1629 File Offset: 0x000AF829
		public void Reply(string {22523})
		{
			if (this.{22527} > 0f)
			{
				return;
			}
			if (this.{22528}.IsEnter)
			{
				this.{22528}.IsEnter = true;
			}
			this.{22528}.Text = {22523} + ", ";
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x000B1790 File Offset: 0x000AF990
		[CompilerGenerated]
		internal static bool <Put>g__IsMyLanguage|0_0(Locale {22524})
		{
			if (Global.GetCurrentServer().Id.StartsWith("ru"))
			{
				return {22524} == Locale.Ru == (LocaleInfo.Current.Id == Locale.Ru);
			}
			return Session.LastOnline < 1000 || {22524} == LocaleInfo.Current.Id;
		}

		// Token: 0x040012D8 RID: 4824
		private static readonly Color twinPlayerMessageColor = Color.LightGray * 0.8f;

		// Token: 0x040012D9 RID: 4825
		private static {22478} currentInstance;

		// Token: 0x040012DA RID: 4826
		public static {22687} currentLSViewBox;

		// Token: 0x040012DB RID: 4827
		public static {22676} currentButton;

		// Token: 0x040012DC RID: 4828
		public static ChatRoomType LogbookRoom = (ChatRoomType)100;

		// Token: 0x040012DD RID: 4829
		private static readonly Marker messagesBounds = new Marker(3f, 3f, 312f, 185f);

		// Token: 0x040012DE RID: 4830
		private static readonly Rectangle WhitePixel = new Rectangle(489, 15, 1, 1);

		// Token: 0x040012DF RID: 4831
		private static readonly Rectangle p_mainBox = new Rectangle(0, 0, 318, 232);

		// Token: 0x040012E0 RID: 4832
		private static readonly Rectangle p_scrollToolTip = new Rectangle(520, 0, 300, 240);

		// Token: 0x040012E1 RID: 4833
		private static readonly Rectangle p_input_passive = new Rectangle(0, 240, 316, 25);

		// Token: 0x040012E2 RID: 4834
		private static readonly ExpandoTextureLinePath p_input_active = ExpandoTextureLinePath.CreateLine(new Rectangle(317, 240, 253, 25), new Rectangle(570, 240, 250, 25));

		// Token: 0x040012E3 RID: 4835
		private static readonly Vector2 v_buttonRoom_global = new Vector2(1f, 190f);

		// Token: 0x040012E4 RID: 4836
		private static readonly Vector2 v_buttonRoom_1 = new Vector2(59f, 190f);

		// Token: 0x040012E5 RID: 4837
		private static readonly Vector2 v_buttonRoom_2 = new Vector2(121f, 190f);

		// Token: 0x040012E6 RID: 4838
		private static readonly Vector2 v_input = new Vector2(1f, 206f);

		// Token: 0x040012E7 RID: 4839
		private const int maxMessagesPer5Sec = 3;

		// Token: 0x040012E8 RID: 4840
		private const int maxMessagesPer10Sec = 4;

		// Token: 0x040012E9 RID: 4841
		private const int maxMessagesPer100Sec = 10;

		// Token: 0x040012EA RID: 4842
		[CompilerGenerated]
		private Action {22525};

		// Token: 0x040012EB RID: 4843
		[CompilerGenerated]
		private Action {22526};

		// Token: 0x040012EC RID: 4844
		private float {22527};

		// Token: 0x040012ED RID: 4845
		private TextBox {22528};

		// Token: 0x040012EE RID: 4846
		private Tlist<{22478}.RoomButton> {22529};

		// Token: 0x040012EF RID: 4847
		private {22478}.RoomButton {22530};

		// Token: 0x040012F0 RID: 4848
		private {22478}.RoomButton {22531};

		// Token: 0x040012F1 RID: 4849
		private {22478}.RoomButton {22532};

		// Token: 0x040012F2 RID: 4850
		private {22478}.RoomButton {22533};

		// Token: 0x040012F3 RID: 4851
		private readonly bool {22534};

		// Token: 0x040012F4 RID: 4852
		private bool {22535};

		// Token: 0x040012F5 RID: 4853
		private EventCounter {22536};

		// Token: 0x040012F6 RID: 4854
		private EventCounter {22537};

		// Token: 0x040012F7 RID: 4855
		private EventCounter {22538};

		// Token: 0x040012F8 RID: 4856
		private const float CHAT_NORMAL_SIZE = 1f;

		// Token: 0x040012F9 RID: 4857
		private const float CHAT_BIG_SIZE = 1.2f;

		// Token: 0x040012FA RID: 4858
		private float {22539} = 1f;

		// Token: 0x040012FB RID: 4859
		private float {22540};

		// Token: 0x040012FC RID: 4860
		private Label {22541};

		// Token: 0x040012FD RID: 4861
		private bool {22542};

		// Token: 0x040012FE RID: 4862
		private bool {22543};

		// Token: 0x020003C4 RID: 964
		private class ChatItem
		{
			// Token: 0x06001510 RID: 5392 RVA: 0x000B17E2 File Offset: 0x000AF9E2
			public ChatItem(Form {22546}, OnChatMessageEvent {22547})
			{
				this.{22555} = {22546};
				this.Payload = {22547};
				this.Recompose(false, 24);
			}

			// Token: 0x06001511 RID: 5393 RVA: 0x000B1804 File Offset: 0x000AFA04
			public void Recompose(bool {22548}, int {22549} = 24)
			{
				if (Global.Settings.BigChatFont)
				{
					{22549} -= 2;
				}
				TextBlockControl textBlockControl = this.{22556};
				if (textBlockControl != null)
				{
					textBlockControl.RemoveFromContainer();
				}
				string {14371};
				if (this.Payload.Name.Length <= 6 || this.Payload.Name[0] != '[' || this.Payload.Name[4] != ']')
				{
					{14371} = this.Payload.Name;
				}
				else
				{
					string name = this.Payload.Name;
					{14371} = name.Substring(6, name.Length - 6);
				}
				int item = HashHelper.SrtingHashMd5Based({14371});
				bool flag = false;
				bool flag2 = Session.Account.SID == this.Payload.SID;
				bool flag3 = this.Payload.SID != 0U && Session.Account.BlacklistNames.Contains(item);
				TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Global.Settings.BigChatFont ? Fonts.Philosopher_14 : Fonts.Arial_10, 0f);
				string text = this.Payload.Name;
				if (this.Payload.IsOutdated)
				{
					LocalizedDateTime localizedDateTime = new LocalizedDateTime(this.Payload.OutdatedServerDateTime, false);
					localizedDateTime.UsePrefix = false;
					text = localizedDateTime.GetDateAndTimeWithoutYear() + " " + text;
				}
				Color color = (this.Payload.Room == ChatRoomType.GlobalModerator) ? Color.Orange : ((this.Payload.Room == ChatRoomType.GlobalSystem || this.Payload.Room == ChatRoomType.Special) ? Color.OrangeRed : (flag2 ? new Color(113, 145, 187, 255) : (new Color(255, 255, 128, 255) * 0.9f * (flag3 ? 0.5f : 1f))));
				if (this.Payload.Room == ChatRoomType.Guild && !flag2 && Session.Guild != null)
				{
					GuildMember member = Session.Guild.GetMember(this.Payload.SID);
					if ((int)((member != null) ? new byte?(member.RankId) : null).GetValueOrDefault(0) >= GuildAccessRightsInfo.AllRights.Count - 2)
					{
						color = Color.Lerp(color, Color.OrangeRed, 0.5f);
					}
				}
				if (!flag3)
				{
					textBlockBuilder.Write(text + ": ", color);
				}
				if (flag3)
				{
					textBlockBuilder.Write(Local.ChatBoxGui_show, new Color(255, 255, 170) * 0.3f);
				}
				else
				{
					color = (flag2 ? Color.White : (Color.White * 0.75f));
					string[] array = this.Payload.Message.SplitSmart(" ,.".ToCharArray());
					int num = array.Length;
					StringBuilder stringBuilder = new StringBuilder();
					int num2 = 0;
					bool flag4 = true;
					for (int i = 0; i < num; i++)
					{
						while (num2 < this.Payload.Message.Length && " ,.".Contains(this.Payload.Message[num2]))
						{
							stringBuilder.Append(this.Payload.Message[num2]);
							num2++;
						}
						string text2 = array[i];
						if (!string.IsNullOrEmpty(text2))
						{
							ref Vector2 ptr = textBlockBuilder.defaultFont.Measure(stringBuilder.ToString() + text2);
							float num3 = (float)((Global.Settings.BigChat || {22478}.CurrentInstance.IsInput) ? 350 : 300) - (flag4 ? textBlockBuilder.Size.X : 0f) - 20f;
							if (ptr.X > num3)
							{
								if (flag4)
								{
									textBlockBuilder.Write(stringBuilder.ToString(), color);
									flag4 = false;
								}
								else
								{
									textBlockBuilder.WriteLine(stringBuilder.ToString(), color);
								}
								stringBuilder.Clear();
							}
							stringBuilder.Append(text2);
							num2 += text2.Length;
						}
					}
					if (flag4)
					{
						textBlockBuilder.Write(stringBuilder.ToString(), color);
					}
					else
					{
						textBlockBuilder.WriteLine(stringBuilder.ToString(), color);
					}
				}
				Vector2 {13587} = new Vector2(this.{22555}.Pos.XY.X, this.{22555}.Pos.XY.Y + this.{22555}.Pos.WH.Y);
				this.{22556} = textBlockBuilder.Create({13587});
				this.{22556}.BackTexturePath = new Rectangle(321, 6, 158, 35);
				this.{22556}.BackTexturePathColor = Color.Black * ({22548} ? 0.25f : 0.25f);
				this.{22556}.HighlightColor = new Color?(Color.LightYellow);
				this.Height = this.{22556}.Pos.WH.Y;
				this.{22556}.Pos = this.{22556}.Pos.SetY(this.{22556}.Pos.XY.Y - this.{22556}.Pos.WH.Y);
				this.{22555}.AddChild(this.{22556});
				this.{22556}.EvClick += this.{22553};
				if (flag)
				{
					this.{22556}.ToolTipState = new ToolTipState("", this.Payload.Message, Array.Empty<ToolTipCharacteristics>());
				}
				this.Alive();
			}

			// Token: 0x06001512 RID: 5394 RVA: 0x000B1DCC File Offset: 0x000AFFCC
			private void {22550}()
			{
				float num = this.{22556}.Pos.XY.Y + this.{22555}.Pos.XY.Y;
				this.{22556}.IsVisible = (num >= this.Height);
			}

			// Token: 0x06001513 RID: 5395 RVA: 0x000B1E1C File Offset: 0x000B001C
			public void Alive()
			{
				this.{22550}();
				if (this.{22556}.IsVisible)
				{
					this.{22556}.RemoveAnimations();
					new UiOpacityAnimation(this.{22556}, 0f, 1f, 400f);
					new UiActionsSleep(this.{22556}, 15000f);
					new UiOpacityAnimation(this.{22556}, 0.66f, 30000f);
				}
			}

			// Token: 0x06001514 RID: 5396 RVA: 0x000B1E8C File Offset: 0x000B008C
			public bool MoveUp(float {22551})
			{
				UiControl uiControl = this.{22556};
				Marker pos = this.{22556}.Pos;
				Vector2 vector = new Vector2(0f, -{22551} + 1f);
				uiControl.Pos = pos.Offset(vector);
				float num = this.{22556}.Pos.XY.Y - this.{22555}.Pos.XY.Y;
				if (num >= 0f && num < 150f)
				{
					this.{22556}.FirstOpacity = num / 150f * 0.5f + 0.5f;
				}
				this.{22550}();
				return false;
			}

			// Token: 0x06001515 RID: 5397 RVA: 0x000B1F2D File Offset: 0x000B012D
			public void Remove()
			{
				this.{22556}.RemoveFromContainer();
				this.{22556} = null;
			}

			// Token: 0x06001516 RID: 5398 RVA: 0x000B1F44 File Offset: 0x000B0144
			public void SetY(float {22552})
			{
				this.{22556}.Pos = this.{22556}.Pos.SetY({22552});
			}

			// Token: 0x06001517 RID: 5399 RVA: 0x000B1F70 File Offset: 0x000B0170
			[CompilerGenerated]
			private void {22553}(ClickUiEventArgs {22554})
			{
				if (this.Payload.SID == 0U || this.Payload.SID == Session.Account.SID)
				{
					return;
				}
				new {17558}(new {17549}(this.Payload.SID, this.Payload.Name, new {17549}.OptionalAction[]
				{
					{17549}.OptionalAction.ReplyChat,
					{17549}.OptionalAction.CopyMessage
				})
				{
					ChatMessage = this.Payload.Message
				});
			}

			// Token: 0x040012FF RID: 4863
			private const float opacityAnimationTime = 700f;

			// Token: 0x04001300 RID: 4864
			private const float messageHeight = 14f;

			// Token: 0x04001301 RID: 4865
			private const float opacityStartPos = 150f;

			// Token: 0x04001302 RID: 4866
			private const float opacityValue = 0.5f;

			// Token: 0x04001303 RID: 4867
			private const float message_lifeTime = 10000f;

			// Token: 0x04001304 RID: 4868
			private const float message_lostTime = 30000f;

			// Token: 0x04001305 RID: 4869
			private const string messageDeviders = " ,.";

			// Token: 0x04001306 RID: 4870
			public const int maxLineLength = 24;

			// Token: 0x04001307 RID: 4871
			private Form {22555};

			// Token: 0x04001308 RID: 4872
			private TextBlockControl {22556};

			// Token: 0x04001309 RID: 4873
			public float Height;

			// Token: 0x0400130A RID: 4874
			public readonly OnChatMessageEvent Payload;

			// Token: 0x020003C5 RID: 965
			public enum ChatItemError
			{
				// Token: 0x0400130C RID: 4876
				NoError,
				// Token: 0x0400130D RID: 4877
				WordLengthError
			}
		}

		// Token: 0x020003C6 RID: 966
		private class ChatItemView : Form
		{
			// Token: 0x06001518 RID: 5400 RVA: 0x000B1FF1 File Offset: 0x000B01F1
			public ChatItemView(Marker {22559}, {22478}.RoomButton {22560}) : base({22559}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				base.PositionAlignment_X = PositionAlignment.Both;
				base.PositionAlignment_Y = PositionAlignment.Both;
				this.Items = new Tlist<{22478}.ChatItem>(12);
				this.{22568} = {22560};
				base.UseScissor = true;
			}

			// Token: 0x06001519 RID: 5401 RVA: 0x000B2028 File Offset: 0x000B0228
			private void {22561}(float {22562})
			{
				for (int i = 0; i < this.Items.Size; i++)
				{
					if (this.Items.Array[i].MoveUp({22562}))
					{
						this.Items.RemoveAt(i);
						i--;
					}
				}
			}

			// Token: 0x0600151A RID: 5402 RVA: 0x000B2070 File Offset: 0x000B0270
			public void Add(OnChatMessageEvent {22563})
			{
				{22478}.ChatItem chatItem = new {22478}.ChatItem(this, {22563});
				this.{22561}(chatItem.Height);
				if (this.Items.Size > 80)
				{
					this.{22567} -= this.Items.Array[0].Height;
					this.Items.Array[0].Remove();
					this.Items.RemoveAt(0);
				}
				this.Items.Add(chatItem);
				this.{22567} += chatItem.Height;
				if (this.{22568}.scroller.CurrentScrollValue != 0f)
				{
					this.{22568}.scroller.MaxScrollValue = this.MaxScroll();
					this.{22568}.scroller.Stopforward();
					this.{22568}.scroller.CurrentScrollValue += chatItem.Height;
					this.{22568}.scroller.Stopful();
					this.SetScroll(this.{22568}.scroller.CurrentScrollValue);
				}
			}

			// Token: 0x0600151B RID: 5403 RVA: 0x000B2180 File Offset: 0x000B0380
			public void Clear()
			{
				for (int i = 0; i < this.Items.Size; i++)
				{
					this.Items.Array[i].Remove();
				}
				this.Items.Clear();
				this.{22567} = 0f;
			}

			// Token: 0x0600151C RID: 5404 RVA: 0x000B21CC File Offset: 0x000B03CC
			public void SaveToCache()
			{
				foreach ({22478}.ChatItem chatItem in ((IEnumerable<{22478}.ChatItem>)this.Items))
				{
					if ((chatItem.Payload.Room != ChatRoomType.Special || this.{22568}.ChatRoom == ChatRoomType.Global) && (chatItem.Payload.Room != ChatRoomType.Group || this.{22568}.ChatRoom == ChatRoomType.Group) && chatItem.Payload.Room != ChatRoomType.Map)
					{
						Tlist<ValueTuple<bool, OnChatMessageEvent>> chatOffCache = Session.ChatOffCache;
						ValueTuple<bool, OnChatMessageEvent> valueTuple = new ValueTuple<bool, OnChatMessageEvent>(false, chatItem.Payload);
						chatOffCache.Add(valueTuple);
					}
				}
			}

			// Token: 0x0600151D RID: 5405 RVA: 0x000B2274 File Offset: 0x000B0474
			public float MaxScroll()
			{
				return Math.Max(0f, this.{22567} - base.Pos.WH.Y);
			}

			// Token: 0x0600151E RID: 5406 RVA: 0x000B2298 File Offset: 0x000B0498
			public void SetScroll(float {22564})
			{
				float num = 0f;
				for (int i = 0; i < this.Items.Size; i++)
				{
					{22478}.ChatItem chatItem = this.Items.Array[i];
					float y = base.Pos.XY.Y;
					chatItem.SetY(base.Pos.XY.Y + base.Pos.WH.Y - this.{22567} + num + {22564});
					if (y != base.Pos.XY.Y)
					{
						chatItem.Alive();
					}
					num += chatItem.Height;
				}
			}

			// Token: 0x0600151F RID: 5407 RVA: 0x000B2338 File Offset: 0x000B0538
			public void RecomposeAll(bool {22565}, int {22566})
			{
				this.{22567} = 0f;
				foreach ({22478}.ChatItem chatItem in ((IEnumerable<{22478}.ChatItem>)this.Items))
				{
					chatItem.Recompose({22565}, {22566});
					this.{22567} += chatItem.Height;
				}
				this.SetScroll(this.{22567});
			}

			// Token: 0x0400130E RID: 4878
			public const int MaxMessageCount = 80;

			// Token: 0x0400130F RID: 4879
			public readonly Tlist<{22478}.ChatItem> Items;

			// Token: 0x04001310 RID: 4880
			private float {22567};

			// Token: 0x04001311 RID: 4881
			private {22478}.RoomButton {22568};
		}

		// Token: 0x020003C7 RID: 967
		private class RoomButton : CustomUi
		{
			// Token: 0x06001520 RID: 5408 RVA: 0x000B23B0 File Offset: 0x000B05B0
			public RoomButton({22478} {22573}, Vector2 {22574}, string {22575}, ChatRoomType {22576})
			{
				Vector2 vector = Fonts.Arial_9.Measure({22575}) + new Vector2(20f, 0f);
				base..ctor(new Marker(ref {22574}, ref vector), Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp, Color.White, false);
				this.{22582} = {22573};
				this.ChatRoom = {22576};
				this.ItemView = new {22478}.ChatItemView({22478}.messagesBounds.Offset({22573}.Pos.XY), this);
				{22573}.AddChild(new UiControl[]
				{
					this,
					this.ItemView
				});
				this.scroller = new Scroller(300f);
				base.AddChild(new Label(base.Pos.XY + new Vector2(4f, 0f), Fonts.Arial_10, Color.White * 0.6f, {22575}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}

			// Token: 0x06001521 RID: 5409 RVA: 0x000B249C File Offset: 0x000B069C
			public void IncrementSignal()
			{
				this.{22579}++;
				this.{22580} = 0f;
				if (this.{22581} == null)
				{
					this.{22581} = new Form(base.Pos.XY + new Vector2(base.Pos.WH.X - 16f, 0f), {22478}.GetNotificationPath(this.{22579}), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					base.AddChild(this.{22581});
					return;
				}
				this.{22581}.TexturePath = {22478}.GetNotificationPath(this.{22579});
			}

			// Token: 0x06001522 RID: 5410 RVA: 0x000B2535 File Offset: 0x000B0735
			public void Set()
			{
				this.{22577}();
				this.ItemView.IsVisible = true;
				this.scroller.Reset();
				this.ItemView.SetScroll(0f);
				base.Opacity = 1f;
			}

			// Token: 0x06001523 RID: 5411 RVA: 0x000B256F File Offset: 0x000B076F
			public void Reset()
			{
				this.ItemView.IsVisible = false;
				this.scroller.Reset();
				this.ItemView.SetScroll(0f);
				base.Opacity = 0.5f;
			}

			// Token: 0x06001524 RID: 5412 RVA: 0x000B25A3 File Offset: 0x000B07A3
			public void CleanRoom()
			{
				this.Reset();
				this.{22577}();
				this.ItemView.Clear();
			}

			// Token: 0x06001525 RID: 5413 RVA: 0x000B25BC File Offset: 0x000B07BC
			private void {22577}()
			{
				if (this.{22581} != null)
				{
					this.{22581}.RemoveFromContainer();
					this.{22581} = null;
				}
				this.{22580} = 0f;
				this.{22579} = 0;
			}

			// Token: 0x06001526 RID: 5414 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserBackRender()
			{
			}

			// Token: 0x06001527 RID: 5415 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserFrontRender()
			{
			}

			// Token: 0x06001528 RID: 5416 RVA: 0x000B25EC File Offset: 0x000B07EC
			protected override void UserUpdate(ref FrameTime {22578})
			{
				if (this.{22581} != null)
				{
					this.{22580} += {22578}.msElapsed;
					if (this.{22580} > 6000f)
					{
						this.{22577}();
					}
					else
					{
						this.{22581}.Opacity = ((this.{22580} < 4000f) ? 1f : (1f - (this.{22580} - 4000f) / 2000f));
					}
				}
				if (this.{22582}.{22528}.IsEnter)
				{
					if (InputHelper.NowInputState.IsDown(Keys.Up))
					{
						this.scroller.ScrollNext(30f);
					}
					if (InputHelper.NowInputState.IsDown(Keys.Down))
					{
						this.scroller.ScrollBack(30f);
					}
					int num = -(InputHelper.NowMouseState.ScrollValue - InputHelper.LastMouseState.ScrollValue);
					if (num > 0)
					{
						this.scroller.ScrollBack(30f);
					}
					else if (num < 0)
					{
						this.scroller.ScrollNext(30f);
					}
				}
				this.scroller.MaxScrollValue = this.ItemView.MaxScroll();
				this.scroller.Update({22578}.msElapsed);
				this.ItemView.SetScroll(this.scroller.CurrentScrollValue);
			}

			// Token: 0x04001312 RID: 4882
			private const float signalCounterLifeTime = 4000f;

			// Token: 0x04001313 RID: 4883
			private const float signalCounterLostTime = 2000f;

			// Token: 0x04001314 RID: 4884
			private int {22579};

			// Token: 0x04001315 RID: 4885
			private float {22580};

			// Token: 0x04001316 RID: 4886
			private Form {22581};

			// Token: 0x04001317 RID: 4887
			public Scroller scroller;

			// Token: 0x04001318 RID: 4888
			private {22478} {22582};

			// Token: 0x04001319 RID: 4889
			public readonly ChatRoomType ChatRoom;

			// Token: 0x0400131A RID: 4890
			public readonly {22478}.ChatItemView ItemView;
		}
	}
}
