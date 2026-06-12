using System;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SDL2;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Core;
using TheraEngine.Grphics.Device;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using WorldOfSeaBattles;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Components.Client;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Scenes
{
	// Token: 0x02000027 RID: 39
	internal sealed class EntryScene : Scene
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00007AED File Offset: 0x00005CED
		public bool IsEntryToGameAnmation
		{
			get
			{
				return this.{16430} != 0f;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000070D7 File Offset: 0x000052D7
		public override bool CleanScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000070D7 File Offset: 0x000052D7
		public override bool UseStaticCamera
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000070D7 File Offset: 0x000052D7
		public override bool UseStaticWeather
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007B12 File Offset: 0x00005D12
		public override void Initialize(ContentManager {16365})
		{
			Global.Game.EvEntryToGame += delegate()
			{
			};
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00007B40 File Offset: 0x00005D40
		public override void OnBegin()
		{
			this.{16373}();
			Label label = new Label(new Vector2(2f, (float)(Engine.GS.UIArea.Height - 24)), Fonts.Arial_12, Color.White, Version.GameVersion.BuildingNumber.ToString() + " pre-beta " + Local.entry_scene, PositionAlignment.LeftUp, PositionAlignment.RightDown);
			new Label(new Vector2(8f + label.Pos.End.X, (float)(Engine.GS.UIArea.Height - 24)), Fonts.Arial_12, Color.White * 0.2f, CommonGlobal.DebugMagicNumber ?? "", PositionAlignment.LeftUp, PositionAlignment.RightDown);
			Label label2 = new Label(new Vector2((float)(Engine.GS.UIArea.Width - 2), (float)(Engine.GS.UIArea.Height - 24)), Fonts.Arial_12, Color.White * 0.3f, "(c) Thera Interactive DMCC", PositionAlignment.RightDown, PositionAlignment.RightDown);
			label2.Pos = label2.Pos.Offset(-label2.Pos.WH.X, 0f);
			LabelButton labelButton = new LabelButton(label2.Pos.XY - new Vector2(0f, 25f), Local.author_list + "...", label2.Font, label2.BasicColor * 0.8f, Color.White, null);
			labelButton.PositionAlignment_X = label2.PositionAlignment_X;
			labelButton.PositionAlignment_Y = label2.PositionAlignment_Y;
			labelButton.EvClick += delegate(ClickUiEventArgs {16451})
			{
				new {18481}();
			};
			Global.Game.StaticSystem.SetDecorateMap();
			Session.RemoveResources();
			Global.Render.PostProcess.GradientAnimationBegin(1500f, false, false);
			Global.Game.WorldInstance.InitializeEntryScene();
			EntryScene.AddDebugInfo();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003100 File Offset: 0x00001300
		private static void AddDebugInfo()
		{
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00007D39 File Offset: 0x00005F39
		public override void OnEnd()
		{
			this.{16434} = null;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00007D42 File Offset: 0x00005F42
		public void ReloadForm()
		{
			Form form = this.{16434};
			if (form != null)
			{
				form.RemoveFromContainer();
			}
			this.{16373}();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00007D5C File Offset: 0x00005F5C
		public override void Update(ref FrameTime {16366})
		{
			if ({16366}.EvaluteTimerSec2(ref this.{16444}))
			{
				this.{16373}();
			}
			if (this.{16431} != null && InputHelper.IsClick(Keys.Tab) && this.{16431}.IsEnter)
			{
				this.{16431}.IsEnter = false;
				this.{16432}.IsEnter = true;
			}
			if (this.{16431} != null && InputHelper.IsClick(Keys.Enter) && {17312}.CurrentInstance == null)
			{
				AnimatedButton animatedButton = this.{16433};
				if (animatedButton != null)
				{
					animatedButton.ImitateClick(false);
				}
			}
			if ({16366}.EvaluteTimerMs2(ref this.{16430}))
			{
				Global.Game.ChangeSceneToGame();
				if (this.StartStateMessageHandler != null)
				{
					this.StartStateMessageHandler();
					this.StartStateMessageHandler = null;
				}
				ulong num = {17053}.Sign(ulong.Parse(Session.ServerSessionHash));
				Global.Network.Send(new OnStartEntry(Global.Player.DebugEnabled, num.ToString(), false));
				Global.Network.Conection.OnGameEntry();
				if (Session.NotificationsTooltip)
				{
					new {17312}(Local.EntryScene_0);
				}
				Session.NotificationsTooltip = false;
				Global.Network.timeToUpdateDailies = 3000f;
			}
			if (this.{16448} != null)
			{
				new {17312}(Local.NetworkManager_0 + " (" + this.{16448} + ")");
				this.{16434}.AllowMouseInput = true;
				this.{16434}.Opacity = 1f;
				this.{16448} = null;
			}
			Global.Game.IsMouseVisible = true;
			Global.Camera.EnabledMouseRead = (Global.Game.SceneGame.MouseState == 0);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007EF8 File Offset: 0x000060F8
		public override void Render2D()
		{
			Device gs = Engine.GS;
			Texture2D tex = AtlasPortGui.Texture.Tex;
			Rectangle rectangle = new Rectangle(502, 301, 258, 136);
			Rectangle uiarea = Engine.GS.UIArea;
			Color color = Color.White;
			gs.DrawCustomTexture(tex, rectangle, uiarea, color);
			Engine.GS.SetTexture(AtlasObjs.Texture);
			Global.Game.StaticSystem.Render2DItems();
			Engine.GS.SetTexture(AtlasEntryGui.Texture);
			if (this.{16444} > 0f)
			{
				Rectangle {11434} = new Rectangle(484, 565, 354, 65);
				Device gs2 = Engine.GS;
				Vector2 vector = Engine.GS.UIArea.HalfWidthHeightInt() - {11434}.HalfWidthHeightInt();
				color = Color.White;
				gs2.Draw({11434}, vector, color);
				Engine.GS.SetFont(Fonts.Philosopher_14Bold);
				Device gs3 = Engine.GS;
				string connecting = Local.connecting;
				vector = Engine.GS.UIArea.HalfWidthHeightInt();
				color = Color.Black * 0.9f;
				gs3.DrawStringCentered(connecting, vector, color);
				if (ServerList.LastLoadError != null)
				{
					Engine.GS.SetFont(Fonts.Arial_10);
					Device gs4 = Engine.GS;
					string {14599} = "Connection status: " + ServerList.LastLoadError.Message;
					vector = new Vector2(10f);
					color = Color.Black * 0.4f;
					gs4.DrawString({14599}, vector, color);
				}
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00008068 File Offset: 0x00006268
		public void ResetIncludeGameQuery()
		{
			this.{16430} = 0f;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00008075 File Offset: 0x00006275
		private void {16367}()
		{
			new {18754}().EvRemoveFromContainer += this.{16388};
			ThreadPool.QueueUserWorkItem(delegate(object {16452})
			{
				EntryScene.<>c.<<LoadStartConfigurator>b__39_1>d <<LoadStartConfigurator>b__39_1>d;
				<<LoadStartConfigurator>b__39_1>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<LoadStartConfigurator>b__39_1>d.<>1__state = -1;
				<<LoadStartConfigurator>b__39_1>d.<>t__builder.Start<EntryScene.<>c.<<LoadStartConfigurator>b__39_1>d>(ref <<LoadStartConfigurator>b__39_1>d);
			});
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000080B4 File Offset: 0x000062B4
		private void {16368}(bool {16369})
		{
			Button button = this.{16441};
			if (button != null)
			{
				button.RemoveFromContainer();
			}
			if ({16369})
			{
				this.{16441} = new Button(this.{16434}.Pos.XY + new Vector2(351f, 605f), new Rectangle(849, 541, 59, 57), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				this.{16434}.AddChild(this.{16441});
				this.{16441}.EvClick += this.{16389};
				return;
			}
			this.{16441} = new Button(this.{16434}.Pos.XY + new Vector2(351f, 605f), AtlasEntryGui.btExit, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{16434}.AddChild(this.{16441});
			this.{16441}.EvClick += delegate(ClickUiEventArgs {16453})
			{
				Global.Game.ExitFromGame();
			};
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000081B4 File Offset: 0x000063B4
		private void {16370}()
		{
			EntryScene.<>c__DisplayClass41_0 CS$<>8__locals1 = new EntryScene.<>c__DisplayClass41_0();
			CS$<>8__locals1.<>4__this = this;
			float num = (float)Engine.GS.UIArea.Width * ((float)AtlasEntryGui.formLight.Height / (float)AtlasEntryGui.formLight.Width);
			Form form = new Form(new Marker(0f, (float)(Engine.GS.UIArea.Height / 2) - num / 2f, (float)Engine.GS.UIArea.Width, num), AtlasEntryGui.formLight, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			form.PositionAlignment_X = PositionAlignment.Both;
			form.PositionAlignment_Y = PositionAlignment.Center;
			form.BasicColor = Color.White * 0.7f;
			Rectangle uiarea = Engine.GS.UIArea;
			Marker marker = Marker.FromCentrScreen(new Marker(ref uiarea), AtlasEntryGui.basicForm);
			this.{16434} = new Form(marker.Offset(0f, 20f), AtlasEntryGui.basicForm, PositionAlignment.Center, PositionAlignment.Center);
			this.{16434}.AnimatedFocus = false;
			UiControl uiControl = this.{16434};
			marker = this.{16434}.Pos;
			uiControl.Pos = marker.SetHeight(this.{16434}.Pos.WH.Y + 30f);
			new UiOpacityAnimation(this.{16434}, 0f, 1f, 500f);
			Rectangle {13273} = new Rectangle(149, 797, 660, 63);
			UiControl uiControl2 = this.{16434};
			marker = new Marker(0f, 0f, ref {13273});
			uiControl2.AddChildPos(new Image(marker.Scale(0.45f), CommonAtlas.Texture.Tex, {13273}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Gray
			}, PositionAlignment.Center, PositionAlignment.LeftUp, 10f, 200f, false);
			this.{16447} = this.CreateServerChangeDropdown<string>(Global.GetCurrentServer().Id, delegate(string {16469})
			{
				bool hasAnySavedCreds = Global.Settings.AuthCreds.HasAnySavedCreds;
				CS$<>8__locals1.<>4__this.{16436}.IsVisible = (Global.Settings.AuthCreds.LastLoginServer != {16469} && hasAnySavedCreds);
				Global.Settings.SavedServerID = {16469};
			}, ServerList.Servers.Select(delegate(ServerList.ServerInfo {16454})
			{
				SelItem<string> selItem = new SelItem<string>({16454}.Id, Local.server + " " + {16454}.Id.ToUpper());
				selItem.AddExtraContent = delegate(UiControl {16455}, SelItem<string> {16456})
				{
					ServerList.ServerInfo serverInfo = ServerList.Servers.First((ServerList.ServerInfo {16471}) => {16471}.Id == {16456}.Value);
					if (serverInfo.Online > 0)
					{
						int num4 = (serverInfo.Online < 700) ? 1 : ((serverInfo.Online < 1700) ? 2 : 3);
						if (num4 != 1)
						{
							if (num4 != 2)
							{
								Color.Lerp(Color.DarkRed, new Color(73, 49, 2), 0.5f);
							}
							else
							{
								new Color(73, 49, 2);
							}
						}
						else
						{
							Color.Lerp(Color.DarkGreen, new Color(73, 49, 2), 0.5f);
						}
						StackForm stackForm = new StackForm(default(Vector2), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						for (int i = 0; i < num4; i++)
						{
							stackForm.AddItem(new UiControl[]
							{
								new Form(Vector2.Zero, new Rectangle(712, 274, 19, 19), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							});
						}
						for (int j = 0; j < 3 - num4; j++)
						{
							stackForm.AddItem(new UiControl[]
							{
								new Form(Vector2.Zero, new Rectangle(712, 274, 19, 19), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
								{
									BasicColor = Color.Black * 0.2f
								}
							});
						}
						{16455}.AddChildPos(stackForm, PositionAlignment.RightDown, PositionAlignment.Center, 7f);
					}
				};
				return selItem;
			}).ToArray<SelItem<string>>());
			this.{16434}.AddChild(this.{16447});
			Rectangle rectangle = new Rectangle(484, 470, 32, 32);
			Rectangle rectangle2 = new Rectangle(708, 297, 244, 172);
			Color {13572} = new Color(204, 188, 147);
			int num2 = rectangle2.Width + 28;
			float {11535} = this.{16447}.Pos.XY.X + this.{16447}.PosWidth - (float)rectangle.Width + 2f;
			float {11536} = this.{16447}.Pos.XY.Y + this.{16447}.PosHeight / 2f - (float)(rectangle.Height / 2) + 5f;
			Vector2 vector = Vector2.One * (float)rectangle.Width * 0.68f;
			this.{16436} = new Form(new Marker({11535}, {11536}, ref vector), rectangle, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				IsVisible = false
			};
			EntryScene.<>c__DisplayClass41_0 CS$<>8__locals2 = CS$<>8__locals1;
			marker = this.{16436}.Pos;
			marker = marker.Offset((float)rectangle.Width, -8f);
			CS$<>8__locals2.customTooltip = new Form(new Marker(ref marker.XY, (float)num2, (float)(rectangle2.Height + 6)), rectangle2, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				IsVisible = false
			};
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_14Bold, 0f);
			float {13574} = (float)num2 * 0.9f;
			int num3 = -4;
			textBlockBuilder.WriteLines(Local.different_servers_title, {13572}, Fonts.Philosopher_14Bold, {13574}, new float?((float)num3));
			textBlockBuilder.WriteSpaceLine(18f);
			textBlockBuilder.WriteLines(Local.different_servers_desc, {13572}, Fonts.Philosopher_14, {13574}, new float?((float)num3));
			CS$<>8__locals1.customTooltip.AddChildPos(textBlockBuilder.Create(), PositionAlignment.LeftUp, PositionAlignment.Center, 16f, 0f, false);
			this.{16436}.UpdateComplete += delegate(UiControl {16470})
			{
				if (CS$<>8__locals1.<>4__this.{16431} != null && string.IsNullOrEmpty(CS$<>8__locals1.<>4__this.{16431}.Text) && string.IsNullOrEmpty(CS$<>8__locals1.<>4__this.{16432}.Text))
				{
					CS$<>8__locals1.<>4__this.{16436}.IsVisible = false;
				}
				CS$<>8__locals1.customTooltip.IsVisible = CS$<>8__locals1.<>4__this.{16436}.IsVisible;
			};
			this.{16434}.AddChild(this.{16436});
			this.{16434}.AddChild(CS$<>8__locals1.customTooltip);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000085D4 File Offset: 0x000067D4
		private void {16371}()
		{
			EntryScene.<>c__DisplayClass42_0 CS$<>8__locals1 = new EntryScene.<>c__DisplayClass42_0();
			CS$<>8__locals1.<>4__this = this;
			if (this.{16434} == null)
			{
				throw new InvalidOperationException("loginBox must be loaded");
			}
			CS$<>8__locals1.buttons = new StackForm(this.{16434}.Pos.XY + new Vector2(119f, 300f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.btPickWay = new Rectangle(544, 471, 265, 62);
			CS$<>8__locals1.btPickWayActive = new Rectangle(489, 672, 265, 62);
			Rectangle {16472} = new Rectangle(810, 470, 37, 37);
			new Rectangle(846, 470, 37, 37);
			Rectangle rectangle = new Rectangle(882, 470, 37, 37);
			Rectangle rectangle2 = new Rectangle(810, 506, 37, 37);
			string {16474} = Steam.IsActive ? Local.EntryScene_Steam : Local.EntryScene_VKPlay;
			CS$<>8__locals1.<LoadAuthTypeForm>g__AddButton|0(Steam.IsActive ? rectangle : rectangle2, Color.SkyBlue, {16474}, null).EvClick += delegate(ClickUiEventArgs {16476})
			{
				ValueTuple<string, string> saved = Global.Settings.AuthCreds.GetSaved(Global.Settings.SavedServerID);
				string item = saved.Item1;
				string item2 = saved.Item2;
				if (!string.IsNullOrEmpty(item) && string.IsNullOrEmpty(item2))
				{
					StackForm buttons = CS$<>8__locals1.buttons;
					if (buttons != null)
					{
						buttons.RemoveFromContainer();
					}
					CS$<>8__locals1.<>4__this.{16372}();
					CS$<>8__locals1.<>4__this.{16368}(true);
					return;
				}
				CS$<>8__locals1.<>4__this.{16374}(item, item2, 0U, false, false);
			};
			CS$<>8__locals1.<LoadAuthTypeForm>g__AddButton|0({16472}, Color.Wheat, Local.EntryScene_Direct, delegate
			{
				string item = Global.Settings.AuthCreds.GetSaved(Global.Settings.SavedServerID).Item1;
				if (!string.IsNullOrEmpty(item))
				{
					return Local.Login + " " + item;
				}
				return Local.EntryScene_Direct2;
			}).EvClick += delegate(ClickUiEventArgs {16477})
			{
				StackForm buttons = CS$<>8__locals1.buttons;
				if (buttons != null)
				{
					buttons.RemoveFromContainer();
				}
				CS$<>8__locals1.<>4__this.{16372}();
				CS$<>8__locals1.<>4__this.{16368}(true);
			};
			this.{16434}.AddChild(CS$<>8__locals1.buttons);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00008750 File Offset: 0x00006950
		private void {16372}()
		{
			UiControl {13207};
			if (this.{16434}.GetChildren.TryFind((UiControl {16457}) => {16457} is Image, out {13207}))
			{
				this.{16434}.RemoveChild({13207});
			}
			CustomSpriteFont f_m14_ThinBold = Fonts.F_m14_ThinBold;
			int num = 37;
			this.{16433} = new AnimatedButton(this.{16434}.Pos.XY + new Vector2(135f, 454f), AtlasEntryGui.btPlayPassive, AtlasEntryGui.btPlayPassive, AtlasEntryGui.btPlayActive, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{16433}.SetText(Local.buttonPlay_sign, Fonts.Philosopher_18, Color.White, false);
			this.{16434}.AddChild(this.{16433});
			this.{16431} = new TextBox(this.{16434}.Pos.XY + new Vector2(120f, (float)(246 + num)), AtlasEntryGui.textBox, f_m14_ThinBold, new Color(13, 13, 13), AtlasEntryGui.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{16431}.CharsMode = SpecialCharMode.SlimCharMap;
			this.{16431}.DefaultText = Local.EntryScene_1;
			TextBox textBox = this.{16431};
			TextBox.Moderator {13698};
			if (({13698} = EntryScene.<>O.<0>__ModerateLogin) == null)
			{
				{13698} = (EntryScene.<>O.<0>__ModerateLogin = new TextBox.Moderator(Gcc.ModerateLogin));
			}
			textBox.AttachModerator({13698}, this.{16442});
			this.{16432} = new TextBox(this.{16434}.Pos.XY + new Vector2(120f, (float)(292 + num)), AtlasEntryGui.textBox, f_m14_ThinBold, new Color(13, 13, 13), AtlasEntryGui.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			TextBox textBox2 = this.{16432};
			TextBox.Moderator {13698}2;
			if (({13698}2 = EntryScene.<>O.<1>__ModeratePassword) == null)
			{
				{13698}2 = (EntryScene.<>O.<1>__ModeratePassword = new TextBox.Moderator(Gcc.ModeratePassword));
			}
			textBox2.AttachModerator({13698}2, this.{16442});
			this.{16432}.Mask = TextBoxMaskType.Password;
			this.{16432}.DefaultText = Local.EntryScene_2;
			this.{16445} = new Button(this.{16434}.Pos.XY + new Vector2(350f, (float)(300 + num)), AtlasEntryGui.eyePasswordHidden, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{16446} = new Button(this.{16434}.Pos.XY + new Vector2(350f, (float)(300 + num)), AtlasEntryGui.eyePasswordVisible, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{16445}.EvClick += this.{16391};
			this.{16445}.EvClick += this.{16393};
			this.{16445}.EvClick += this.{16395};
			this.{16432}.AddChildPos(this.{16445}, PositionAlignment.Disabled, PositionAlignment.Disabled, 0f);
			this.{16446}.EvClick += this.{16397};
			this.{16446}.EvClick += this.{16399};
			this.{16446}.EvClick += this.{16401};
			this.{16432}.AddChildPos(this.{16446}, PositionAlignment.Disabled, PositionAlignment.Disabled, 0f);
			this.{16446}.IsVisible = false;
			Vector2 value = this.{16434}.Pos.XY + new Vector2(120f, (float)(338 + num));
			TextBlockControl textBlockControl = TextBlockBuilder.CreateBlock(250f, Local.EntryScene_mailTt, new Color(13, 13, 13) * 0.7f, Fonts.Arial_12, 0f).CreateCentroid(value + new Vector2(130f, 15f));
			textBlockControl.IsVisible = false;
			this.{16434}.AddChild(new UiControl[]
			{
				this.{16431},
				this.{16432},
				textBlockControl
			});
			this.{16438} = new CheckboxControl(this.{16434}.Pos.XY + new Vector2(151f, (float)(346 + num)), AtlasEntryGui.cbOn, AtlasEntryGui.cbOff, false, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{16438}.SetText(" " + Local.EntryScene_30, Fonts.Philosopher_14, new Color(83, 12, 12));
			this.{16438}.EvCheck += this.{16403};
			this.{16437} = new CheckboxControl(this.{16434}.Pos.XY + new Vector2(151f, (float)(376 + num)), AtlasEntryGui.cbOn, AtlasEntryGui.cbOff, Global.Settings.SaveLoginData, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{16437}.SetText(" " + Local.EntryScene_5, Fonts.Philosopher_14, new Color(83, 12, 12));
			this.{16437}.EvCheck += delegate(CheckboxCheckedEventArgs {16458})
			{
				Global.Settings.SaveLoginData = {16458}.NewValue;
			};
			this.{16434}.AddChild(new UiControl[]
			{
				this.{16438},
				this.{16437}
			});
			this.{16431}.EvTextChanged += this.{16407};
			this.{16432}.EvTextChanged += this.{16409};
			this.{16405}();
			if (this.{16447} != null)
			{
				this.{16447}.EvChangeItem += delegate(SelItem<string> {16411})
				{
					this.{16405}();
				};
			}
			Button button = new Button(this.{16434}.Pos.XY + new Vector2(118f, 193f), AtlasEntryGui.buttonTop, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button.AddChild(new Label(button.Pos.Center, Fonts.Philosopher_14, new Color(179, 176, 163), Local.EntryScene_7, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				RenderToDepthMap = false
			}.Center());
			Button button2 = new Button(this.{16434}.Pos.XY + new Vector2(250f, 193f), AtlasEntryGui.buttonTop, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button2.AddChild(new Label(button2.Pos.Center, Fonts.Philosopher_14, new Color(179, 176, 163), Local.EntryScene_8, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				RenderToDepthMap = false
			}.Center());
			button.UpdateComplete += this.{16412};
			button2.UpdateComplete += this.{16414};
			this.{16434}.AddChild(new UiControl[]
			{
				button,
				button2
			});
			int num2 = 30;
			int num3 = 6;
			this.{16439} = new CheckboxControl(this.{16434}.Pos.XY + new Vector2(117f, (float)(386 + num2)), new Rectangle(771, 184, 32, 32), new Rectangle(811, 183, 32, 32), false, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{16440} = TextBlockBuilder.CreateBlock(220f, Local.EntryScene_31, new Color(99, 13, 0), Fonts.Arial_10, -2f).Create(this.{16439}.Pos.XY + new Vector2((float)(34 + num3), -2f));
			this.{16440}.IsVisible = false;
			this.{16440}.EvClick += delegate(ClickUiEventArgs {16459})
			{
				Helpers.ExecuteBrowser(Local.launcher_rules_ref, false);
			};
			this.{16440}.HighlightColor = new Color?(Color.Blue);
			this.{16439}.EvCheck += this.{16416};
			textBlockControl.UpdateComplete += this.{16418};
			this.{16434}.AddChild(new UiControl[]
			{
				this.{16439},
				this.{16440}
			});
			button.EvClick += this.{16422};
			button2.EvClick += this.{16424};
			this.{16433}.EvClick += this.{16426};
			if (this.{16450})
			{
				button2.ImitateClick(false);
				return;
			}
			button.ImitateClick(false);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00008F80 File Offset: 0x00007180
		private void {16373}()
		{
			if (Global.Settings.IsFirstLaunch && !this.{16450})
			{
				this.{16450} = true;
				this.{16367}();
				return;
			}
			if (ServerList.Servers == null || ServerList.LastLoadError != null)
			{
				this.{16444} = 1f;
				return;
			}
			this.{16443} = false;
			this.{16370}();
			this.{16368}(false);
			if (PlatformTuning.ExternalLoginAPI)
			{
				this.{16371}();
			}
			else
			{
				this.{16372}();
			}
			this.{16450} = false;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00008FFC File Offset: 0x000071FC
		private void {16374}(string {16375}, string {16376}, uint {16377}, bool {16378}, bool {16379})
		{
			EntryScene.<>c__DisplayClass45_0 CS$<>8__locals1 = new EntryScene.<>c__DisplayClass45_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.login = {16375};
			CS$<>8__locals1.password = {16376};
			this.{16383}();
			CS$<>8__locals1.authParams = new AuthParams
			{
				RegNew = {16379},
				RegName = "unnamed_" + DateTime.Now.Ticks.ToString(),
				RegReferalSID = {16377},
				Query = ({16379} ? CS$<>8__locals1.login.ToLower() : CS$<>8__locals1.login),
				Password = ({16378} ? string.Empty : CS$<>8__locals1.password),
				Platform = PlatformTuning.GetPlatform()
			};
			if (PlatformTuning.DebuggingAuth != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Параметры входа, передаваемые серверу:");
				stringBuilder.AppendLine("");
				StringBuilder stringBuilder2 = stringBuilder;
				StringBuilder stringBuilder3 = stringBuilder2;
				StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(8, 1, stringBuilder2);
				appendInterpolatedStringHandler.AppendLiteral("RegNew: ");
				appendInterpolatedStringHandler.AppendFormatted<bool>(CS$<>8__locals1.authParams.RegNew);
				stringBuilder3.AppendLine(ref appendInterpolatedStringHandler);
				stringBuilder2 = stringBuilder;
				StringBuilder stringBuilder4 = stringBuilder2;
				appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(7, 1, stringBuilder2);
				appendInterpolatedStringHandler.AppendLiteral("Query: ");
				appendInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.authParams.Query);
				stringBuilder4.AppendLine(ref appendInterpolatedStringHandler);
				stringBuilder2 = stringBuilder;
				StringBuilder stringBuilder5 = stringBuilder2;
				appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(10, 1, stringBuilder2);
				appendInterpolatedStringHandler.AppendLiteral("Password: ");
				appendInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.authParams.Password);
				stringBuilder5.AppendLine(ref appendInterpolatedStringHandler);
				stringBuilder2 = stringBuilder;
				StringBuilder stringBuilder6 = stringBuilder2;
				appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(10, 1, stringBuilder2);
				appendInterpolatedStringHandler.AppendLiteral("Platform: ");
				appendInterpolatedStringHandler.AppendFormatted<PlatformType>(CS$<>8__locals1.authParams.Platform);
				stringBuilder6.AppendLine(ref appendInterpolatedStringHandler);
				stringBuilder2 = stringBuilder;
				StringBuilder stringBuilder7 = stringBuilder2;
				appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(15, 1, stringBuilder2);
				appendInterpolatedStringHandler.AppendLiteral("Auth.AuthType: ");
				appendInterpolatedStringHandler.AppendFormatted<AuthType>(PlatformTuning.DebuggingAuth.AuthType);
				stringBuilder7.AppendLine(ref appendInterpolatedStringHandler);
				stringBuilder2 = stringBuilder;
				StringBuilder stringBuilder8 = stringBuilder2;
				appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(13, 1, stringBuilder2);
				appendInterpolatedStringHandler.AppendLiteral("Auth.UserId: ");
				appendInterpolatedStringHandler.AppendFormatted(PlatformTuning.DebuggingAuth.UserId);
				stringBuilder8.AppendLine(ref appendInterpolatedStringHandler);
				Engine.ShowMessageBox(stringBuilder.ToString(), "", SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION);
			}
			Global.ServerAddress = new IPEndPoint(IPAddress.Parse(Global.GetCurrentServer().Ip), 23771);
			if (PlatformTuning.ExternalLoginAPI)
			{
				Task.Run(delegate()
				{
					EntryScene.<>c__DisplayClass45_0.<<BeginAuth>b__0>d <<BeginAuth>b__0>d;
					<<BeginAuth>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<BeginAuth>b__0>d.<>4__this = CS$<>8__locals1;
					<<BeginAuth>b__0>d.<>1__state = -1;
					<<BeginAuth>b__0>d.<>t__builder.Start<EntryScene.<>c__DisplayClass45_0.<<BeginAuth>b__0>d>(ref <<BeginAuth>b__0>d);
					return <<BeginAuth>b__0>d.<>t__builder.Task;
				}).ContinueWith(delegate(Task {16460})
				{
					if ({16460}.IsFaulted)
					{
						throw {16460}.Exception;
					}
				});
				return;
			}
			Global.Settings.AuthCreds.WhenSuccessLogin(CS$<>8__locals1.login, CS$<>8__locals1.password);
			Global.Network.Conection.LoginAsync(Global.ServerAddress, CS$<>8__locals1.authParams);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000092A8 File Offset: 0x000074A8
		private DropdownControl<T> CreateServerChangeDropdown<T>(T {16380}, Action<T> {16381}, params SelItem<T>[] {16382})
		{
			Vector2 vector = this.{16434}.Pos.XY + new Vector2(120f, 237f);
			DropdownControl<T> dropdownControl = new DropdownControl<T>(new Marker(ref vector, 264f, 36f), AtlasEntryGui.textBox, AtlasEntryGui.dropdownItemBox, Fonts.F_m14_ThinBold, new Color(13, 13, 13), {16382});
			dropdownControl.SelectByValue({16380});
			dropdownControl.EvChangeItem += delegate(SelItem<T> {16483})
			{
				{16381}({16483}.Value);
			};
			return dropdownControl;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00009331 File Offset: 0x00007531
		private void {16383}()
		{
			this.{16434}.AllowMouseInput = false;
			this.{16434}.Opacity = 0.5f;
			this.{16435} = EntryScene.IncludeConnectionForm();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000935A File Offset: 0x0000755A
		private void {16384}()
		{
			Form form = this.{16435};
			if (form != null)
			{
				form.RemoveFromContainer();
			}
			this.{16435} = null;
			this.{16434}.AllowMouseInput = true;
			this.{16434}.Opacity = 1f;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00009390 File Offset: 0x00007590
		private static Form IncludeConnectionForm()
		{
			Rectangle uiarea = Engine.GS.UIArea;
			Form form = new Form(Marker.FromCentrScreen(new Marker(ref uiarea), AtlasEntryGui.authForm), AtlasEntryGui.authForm, PositionAlignment.Center, PositionAlignment.Center);
			form.AddChild(new Label(form.Pos.Center, Fonts.Philosopher_14, Color.Black * 0.8f, Local.EntryScene_9, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			form.RenderToDepthMap = false;
			form.AnimatedFocus = false;
			return form;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000940C File Offset: 0x0000760C
		public void AuthError(LoginQueryResult? {16385}, string {16386})
		{
			EntryScene.<>c__DisplayClass50_0 CS$<>8__locals1;
			CS$<>8__locals1.args = {16386};
			if ({16385} != null)
			{
				switch ({16385}.GetValueOrDefault())
				{
				case LoginQueryResult.UncorrectAnswer:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_15, ref CS$<>8__locals1);
					goto IL_2DF;
				case LoginQueryResult.LoginIsUsed:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_12, ref CS$<>8__locals1);
					goto IL_2DF;
				case LoginQueryResult.NameIsUsed:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_13, ref CS$<>8__locals1);
					goto IL_2DF;
				case LoginQueryResult.Error:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_11, ref CS$<>8__locals1);
					goto IL_2DF;
				case LoginQueryResult.OutdatedVersion:
					if (Steam.IsActive)
					{
						EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_34, ref CS$<>8__locals1);
						goto IL_2DF;
					}
					if (Session.VKPlay.IsActive)
					{
						EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_35, ref CS$<>8__locals1);
						goto IL_2DF;
					}
					new {17312}(Local.EntryScene_14, delegate(int {16461})
					{
						if ({16461} == 0)
						{
							Helpers.ExecuteBrowser(Local.launcher_download_ref, false);
						}
					}, new {17443}[]
					{
						new {17443}(Local.open_site, "", {17312}.cIconSpyglass, false, 0f),
						new {17443}(Local.close, "", {17312}.cIconReject, false, 0f)
					});
					goto IL_2DF;
				case LoginQueryResult.UncorrectReferalFriendSID:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_16, ref CS$<>8__locals1);
					goto IL_2DF;
				case LoginQueryResult.Blocked:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_17, ref CS$<>8__locals1);
					goto IL_2DF;
				case LoginQueryResult.ClosedAccess:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_18, ref CS$<>8__locals1);
					goto IL_2DF;
				case LoginQueryResult.AccessKeyUsed:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_20(CS$<>8__locals1.args), ref CS$<>8__locals1);
					goto IL_2DF;
				case LoginQueryResult.GameProtectionProblem:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_21, ref CS$<>8__locals1);
					goto IL_2DF;
				case LoginQueryResult.PasswordResetSuccess:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_32, ref CS$<>8__locals1);
					goto IL_2DF;
				case LoginQueryResult.PasswordResetError:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_33, ref CS$<>8__locals1);
					goto IL_2DF;
				case LoginQueryResult.ExternalIdAlreadyUsed:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_Error_ExternalIdAlreadyUsed(Session.VKPlay.IsActive ? "VKPlay" : "Steam", (PlatformTuning.DebuggingAuth != null) ? PlatformTuning.DebuggingAuth.UserId : (Session.VKPlay.IsActive ? Session.VKPlay.Uid : Steam.GetSteamID())), ref CS$<>8__locals1);
					goto IL_2DF;
				case LoginQueryResult.ExternalIdCheckError:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_Error_ExternalIdCheckError((PlatformTuning.DebuggingAuth != null) ? PlatformTuning.DebuggingAuth.UserId : (Session.VKPlay.IsActive ? "VKPlay" : "Steam"), Session.VKPlay.IsActive ? Session.VKPlay.Uid : Steam.GetSteamID()), ref CS$<>8__locals1);
					goto IL_2DF;
				case LoginQueryResult.ServerOverload:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_Error_Overload, ref CS$<>8__locals1);
					goto IL_2DF;
				case LoginQueryResult.LoginIsLinked:
					EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_Error_LoginIsLinked(Session.VKPlay.IsActive ? "VKPlay" : "Steam"), ref CS$<>8__locals1);
					goto IL_2DF;
				}
				EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_22, ref CS$<>8__locals1);
			}
			else
			{
				EntryScene.<AuthError>g__ShowError|50_0(Local.EntryScene_10, ref CS$<>8__locals1);
			}
			IL_2DF:
			if (this.{16434} != null)
			{
				new UiActor(this.{16434}, new Action(this.{16384}));
				return;
			}
			this.{16373}();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009720 File Offset: 0x00007920
		public void AuthSucces(PlayerAccount {16387})
		{
			this.{16430} = IntroScreenRenderer.AnimationTime;
			Global.Render.PostProcess.RunIntroScreen(new IntroScreenRenderer({16387}.SavedWorldIsEnteredPort ? Local.loc_port : Local.loc_world, false, IntroScreenRenderer.ExtraEffects.None, null));
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000976B File Offset: 0x0000796B
		[CompilerGenerated]
		private void {16388}()
		{
			this.{16373}();
			Global.Settings.OnSave(false);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000977E File Offset: 0x0000797E
		[CompilerGenerated]
		private void {16389}(ClickUiEventArgs {16390})
		{
			this.ReloadForm();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009786 File Offset: 0x00007986
		[CompilerGenerated]
		private void {16391}(ClickUiEventArgs {16392})
		{
			this.{16432}.Mask = TextBoxMaskType.Disabled;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009794 File Offset: 0x00007994
		[CompilerGenerated]
		private void {16393}(ClickUiEventArgs {16394})
		{
			this.{16445}.IsVisible = false;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000097A2 File Offset: 0x000079A2
		[CompilerGenerated]
		private void {16395}(ClickUiEventArgs {16396})
		{
			this.{16446}.IsVisible = true;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000097B0 File Offset: 0x000079B0
		[CompilerGenerated]
		private void {16397}(ClickUiEventArgs {16398})
		{
			this.{16432}.Mask = TextBoxMaskType.Password;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000097BE File Offset: 0x000079BE
		[CompilerGenerated]
		private void {16399}(ClickUiEventArgs {16400})
		{
			this.{16446}.IsVisible = false;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000097CC File Offset: 0x000079CC
		[CompilerGenerated]
		private void {16401}(ClickUiEventArgs {16402})
		{
			this.{16445}.IsVisible = true;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000097DA File Offset: 0x000079DA
		[CompilerGenerated]
		private void {16403}(CheckboxCheckedEventArgs {16404})
		{
			this.{16432}.IsVisible = !{16404}.NewValue;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000097F0 File Offset: 0x000079F0
		[CompilerGenerated]
		private void {16405}()
		{
			ValueTuple<string, string> saved = Global.Settings.AuthCreds.GetSaved(Global.Settings.SavedServerID);
			string item = saved.Item1;
			string item2 = saved.Item2;
			if (Global.Settings.SaveLoginData && !string.IsNullOrEmpty(item) && !string.IsNullOrEmpty(item2))
			{
				this.{16431}.Text = item;
				this.{16432}.Text = item2;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00009857 File Offset: 0x00007A57
		[CompilerGenerated]
		private void {16406}()
		{
			Global.Settings.AuthCreds.WhenSuccessLogin(this.{16431}.Text, this.{16432}.Text);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000987E File Offset: 0x00007A7E
		[CompilerGenerated]
		private void {16407}(string {16408})
		{
			this.{16406}();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000987E File Offset: 0x00007A7E
		[CompilerGenerated]
		private void {16409}(string {16410})
		{
			this.{16406}();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000988E File Offset: 0x00007A8E
		[CompilerGenerated]
		private void {16412}(UiControl {16413})
		{
			((Button){16413}).Opacity = (this.{16443} ? 1f : 0.6f);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000098AF File Offset: 0x00007AAF
		[CompilerGenerated]
		private void {16414}(UiControl {16415})
		{
			((Button){16415}).Opacity = (this.{16443} ? 0.6f : 1f);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000098D0 File Offset: 0x00007AD0
		[CompilerGenerated]
		private void {16416}(CheckboxCheckedEventArgs {16417})
		{
			this.{16433}.IsVisible = {16417}.NewValue;
			bool newValue = {16417}.NewValue;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000098EC File Offset: 0x00007AEC
		[CompilerGenerated]
		private void {16418}(UiControl {16419})
		{
			{16419}.IsVisible = (!this.{16443} && (this.{16431}.Text.Length == 0 || this.{16432}.Text.Length == 0));
			this.{16439}.IsVisible = (this.{16440}.IsVisible = (!{16419}.IsVisible && !this.{16443}));
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00009960 File Offset: 0x00007B60
		[CompilerGenerated]
		private void {16420}()
		{
			this.{16443} = true;
			this.{16433}.SetText(Local.buttonPlay_sign, Fonts.Philosopher_18, Color.White, false);
			this.{16433}.IsVisible = true;
			this.{16437}.IsVisible = true;
			this.{16438}.IsVisible = true;
			this.{16432}.Mask = (this.{16445}.IsVisible ? TextBoxMaskType.Password : TextBoxMaskType.Disabled);
			this.{16431}.CharsMode = SpecialCharMode.SlimCharMap;
			TextBox textBox = this.{16431};
			TextBox.Moderator {13698};
			if (({13698} = EntryScene.<>O.<0>__ModerateLogin) == null)
			{
				{13698} = (EntryScene.<>O.<0>__ModerateLogin = new TextBox.Moderator(Gcc.ModerateLogin));
			}
			textBox.AttachModerator({13698}, this.{16442});
			this.{16431}.DefaultText = Local.EntryScene_1;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00009A18 File Offset: 0x00007C18
		[CompilerGenerated]
		private void {16421}()
		{
			this.{16432}.Text = "";
			this.{16443} = false;
			if (this.{16436} != null)
			{
				this.{16436}.IsVisible = false;
			}
			this.{16433}.SetText(Local.buttonPlay_create, Fonts.Philosopher_18, Color.White, false);
			this.{16433}.IsVisible = true;
			this.{16437}.IsVisible = false;
			this.{16438}.IsVisible = false;
			this.{16438}.IsChecked = false;
			this.{16439}.IsChecked = true;
			this.{16431}.CharsMode = SpecialCharMode.SlimCharMap;
			TextBox textBox = this.{16431};
			TextBox.Moderator {13698};
			if (({13698} = EntryScene.<>O.<2>__ModerateLoginEmail) == null)
			{
				{13698} = (EntryScene.<>O.<2>__ModerateLoginEmail = new TextBox.Moderator(Gcc.ModerateLoginEmail));
			}
			textBox.AttachModerator({13698}, this.{16442});
			this.{16431}.DefaultText = Local.EntryScene_1_b;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00009AF0 File Offset: 0x00007CF0
		[CompilerGenerated]
		private void {16422}(ClickUiEventArgs {16423})
		{
			this.{16420}();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00009AF8 File Offset: 0x00007CF8
		[CompilerGenerated]
		private void {16424}(ClickUiEventArgs {16425})
		{
			this.{16421}();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00009B00 File Offset: 0x00007D00
		[CompilerGenerated]
		private void {16426}(ClickUiEventArgs {16427})
		{
			if (this.{16431}.Text.Length == 0 || (this.{16432}.Text.Length == 0 && !this.{16438}.IsChecked))
			{
				new {17312}(Local.EntryScene_23);
				return;
			}
			if (this.{16431}.HasModeratorError || (this.{16432}.HasModeratorError && !this.{16438}.IsChecked))
			{
				return;
			}
			string text = this.{16431}.Text;
			string text2 = this.{16432}.Text;
			this.{16374}(text, text2, 0U, this.{16438}.IsChecked, !this.{16443});
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009BA7 File Offset: 0x00007DA7
		[CompilerGenerated]
		internal static void <AuthError>g__ShowError|50_0(string {16428}, ref EntryScene.<>c__DisplayClass50_0 {16429})
		{
			if (!string.IsNullOrEmpty({16429}.args))
			{
				{16428} = {16428} + " " + {16429}.args;
			}
			new {17312}({16428});
		}

		// Token: 0x040000CC RID: 204
		private float {16430};

		// Token: 0x040000CD RID: 205
		public Action StartStateMessageHandler;

		// Token: 0x040000CE RID: 206
		private TextBox {16431};

		// Token: 0x040000CF RID: 207
		private TextBox {16432};

		// Token: 0x040000D0 RID: 208
		private AnimatedButton {16433};

		// Token: 0x040000D1 RID: 209
		private Form {16434};

		// Token: 0x040000D2 RID: 210
		private Form {16435};

		// Token: 0x040000D3 RID: 211
		private Form {16436};

		// Token: 0x040000D4 RID: 212
		private CheckboxControl {16437};

		// Token: 0x040000D5 RID: 213
		private CheckboxControl {16438};

		// Token: 0x040000D6 RID: 214
		private CheckboxControl {16439};

		// Token: 0x040000D7 RID: 215
		private TextBlockControl {16440};

		// Token: 0x040000D8 RID: 216
		private Button {16441};

		// Token: 0x040000D9 RID: 217
		private Color {16442} = Color.DarkRed;

		// Token: 0x040000DA RID: 218
		private bool {16443};

		// Token: 0x040000DB RID: 219
		private float {16444};

		// Token: 0x040000DC RID: 220
		private Button {16445};

		// Token: 0x040000DD RID: 221
		private Button {16446};

		// Token: 0x040000DE RID: 222
		private DropdownControl<string> {16447};

		// Token: 0x040000DF RID: 223
		private volatile string {16448};

		// Token: 0x040000E0 RID: 224
		private volatile ExternalAuth {16449};

		// Token: 0x040000E1 RID: 225
		private bool {16450};

		// Token: 0x02000028 RID: 40
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040000E2 RID: 226
			public static TextBox.Moderator <0>__ModerateLogin;

			// Token: 0x040000E3 RID: 227
			public static TextBox.Moderator <1>__ModeratePassword;

			// Token: 0x040000E4 RID: 228
			public static TextBox.Moderator <2>__ModerateLoginEmail;
		}
	}
}
