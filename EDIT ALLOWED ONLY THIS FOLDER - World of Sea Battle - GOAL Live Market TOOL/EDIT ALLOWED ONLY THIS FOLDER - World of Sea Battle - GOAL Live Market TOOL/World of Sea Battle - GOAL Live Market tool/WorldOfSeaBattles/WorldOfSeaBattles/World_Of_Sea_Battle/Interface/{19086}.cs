using System;
using System.Collections.Generic;
using Common;
using Common.Account;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001AB RID: 427
	public class {19086} : TextureHost
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0004C8B9 File Offset: 0x0004AAB9
		private static CustomSpriteFont MenuItemFont
		{
			get
			{
				return Fonts.Philosopher_24;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0004C8C0 File Offset: 0x0004AAC0
		private static int IconSize
		{
			get
			{
				return 39;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0004C8C4 File Offset: 0x0004AAC4
		private static float ItemHeight
		{
			get
			{
				return 50f;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0004C8CB File Offset: 0x0004AACB
		private static Color MenuItemColor
		{
			get
			{
				return Color.White;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0004C8D2 File Offset: 0x0004AAD2
		private static Color MenuItemSelectedColor
		{
			get
			{
				return new Color(255, 216, 93);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0004C8E5 File Offset: 0x0004AAE5
		private static Vector2 BackgroundScale
		{
			get
			{
				return new Vector2(1f, 0.9f);
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0004C8F8 File Offset: 0x0004AAF8
		public {19086}(params {19086}.Item[] {19088}) : base(CommonAtlas.Texture.Tex, true)
		{
			base.AddChild(new Form(base.Pos, new Marker(ref {19086}.backgroundGradient).Border(-1f).ToRect(), PositionAlignment.Both, PositionAlignment.Both)
			{
				AnimatedFocus = false
			});
			new UiMarkerAndOpacityAnimation(this, 0f, 1f, base.Pos.Offset(0f, 0f), base.Pos, 300f, UiAmimationCurve.Linear);
			{19086}.CurrentMenu = this;
			Global.Game.SceneGame.IncreaseMouse();
			GameScene.IncreaseGameInput();
			base.EvRemoveFromContainer += delegate()
			{
				{19086}.CurrentMenu = null;
				Global.Game.SceneGame.DecreaseMouse();
				GameScene.DecreaseGameInput();
			};
			Vector2 value = new Vector2(MathF.Max(180f, (float)Engine.GS.UIArea.Width * 0.09f), 180f);
			for (int i = 0; i < {19088}.Length; i++)
			{
				{19086}.Item item = {19088}[i];
				if (item.Visible)
				{
					if (string.IsNullOrEmpty(item.Text))
					{
						value.Y += {19086}.ItemHeight;
					}
					else
					{
						{19086}.ItemUi {12841} = new {19086}.ItemUi(base.Pos.XY + value, item, delegate()
						{
							item.Click();
							this.{19090}();
						});
						if (item.Style == {19086}.ItemStyle.Link)
						{
							value.X += (float)({19086}.IconSize + 15);
						}
						else
						{
							value.Y += {19086}.ItemHeight;
						}
						this.{19091}.Add({12841});
						base.AddChild({12841});
					}
				}
			}
			this.{19092} = this.{19091}.First();
			if (this.{19091}.Size == 1)
			{
				this.{19092}.ImitateClick(false);
				base.RemoveFromContainer();
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0004CB1A File Offset: 0x0004AD1A
		protected override void UserBackRender()
		{
			base.UserBackRender();
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0004CB22 File Offset: 0x0004AD22
		protected override void UserFrontRender()
		{
			base.UserFrontRender();
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0004CB2C File Offset: 0x0004AD2C
		protected override void UserUpdate(ref FrameTime {19089})
		{
			base.UserUpdate(ref {19089});
			if (!base.AllowMouseInput)
			{
				return;
			}
			if (!this.{19094} && (Global.Settings.kb_Escape.IsClick || (InputHelper.LeftWasClicked && this.{19092}.InputMode == MouseInputMode.NoFocus)))
			{
				this.{19090}();
			}
			if (!this.{19094} && (Global.Settings.kb_Accept.IsClick || Global.Settings.kb_Action.IsClick || Global.Settings.kb_ChatEnter.IsClick))
			{
				{19086}.ItemUi itemUi = this.{19092};
				if (itemUi != null)
				{
					itemUi.ImitateClick(false);
				}
			}
			if (InputHelper.IsClick(Keys.Up) || Global.Settings.kb_ds_Forward.IsClick)
			{
				this.{19092} = this.{19091}.Array[(this.{19091}.IndexOf(this.{19092}) - 1 + this.{19091}.Size) % this.{19091}.Size];
				this.{19093} = true;
			}
			if (InputHelper.IsClick(Keys.Down) || Global.Settings.kb_ds_Backward.IsClick)
			{
				this.{19092} = this.{19091}.Array[(this.{19091}.IndexOf(this.{19092}) + 1) % this.{19091}.Size];
				this.{19093} = true;
			}
			if (Vector2.Distance(Engine.GS.MouseToUIPrev, Engine.GS.MouseToUI) >= 5f)
			{
				this.{19093} = false;
			}
			if (!this.{19093})
			{
				foreach ({19086}.ItemUi itemUi2 in ((IEnumerable<{19086}.ItemUi>)this.{19091}))
				{
					if (itemUi2.InputMode == MouseInputMode.Focused)
					{
						this.{19092} = itemUi2;
					}
				}
			}
			foreach ({19086}.ItemUi itemUi3 in ((IEnumerable<{19086}.ItemUi>)this.{19091}))
			{
				if (itemUi3 != this.{19092})
				{
					itemUi3.IsMenuFocused = false;
				}
			}
			if (this.{19092} != null)
			{
				this.{19092}.IsMenuFocused = true;
			}
			this.{19094} = false;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0004CD54 File Offset: 0x0004AF54
		private void {19090}()
		{
			base.AllowMouseInput = false;
			new UiMarkerAndOpacityAnimation(this, 1f, 0f, base.Pos, base.Pos.Offset(0f, 0f), 300f, UiAmimationCurve.Linear);
			new UiRemoveAction(this);
			{19086}.CurrentMenu = null;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0004CDAC File Offset: 0x0004AFAC
		public static void CreateExitOfPortMenu()
		{
			if ({18826}.CurrentInstance != null || {18981}.CurrentInstance != null || {22094}.CurrentInstance != null || {17312}.CurrentInstance != null)
			{
				return;
			}
			if (!Session.Account.EducationQuest.HasFlag(EducationOnboarding.InteropQuestUnit))
			{
				{19994}.Me({19988}.Info, Local.onboarding_port10_q, Array.Empty<object>());
				return;
			}
			if (!Global.Game.ScenePort.IsMainPage)
			{
				Global.Game.ScenePort.mainHandler(null);
			}
			{19086}.Item[] array = new {19086}.Item[5];
			array[0] = new {19086}.Item(new Rectangle(1712, 3220, 64, 64), Local.exit_to_sea, delegate()
			{
				Global.Game.ScenePort.TryRegisterCallExitToWorld(-1, null);
			}, (Global.Settings.DeathController.BlockPortExitSec == 0f) ? null : Local.mending, true, {19086}.ItemStyle.Default, null);
			array[1] = new {19086}.Item(new Rectangle(1777, 3220, 64, 64), Local.PortVisualScene_3a, delegate()
			{
				new {22094}(delegate(ValueTuple<object, {22094}.Mode> {19125})
				{
					Global.Game.ScenePort.TryRegisterCallExitToWorld((int){19125}.Item1, null);
				}, {22094}.Mode.SelectLighthouse);
			}, (Global.Settings.DeathController.BlockPortExitSec > 0f) ? Local.mending : (((Session.ServerWorldStatus.NearPortWindowOpenedAndNotInBattle && Session.Account.WorldFlag != OpenWorldFlag.Peaceful) || Session.EngagingInPortBattlePort == Global.Player.NearPort) ? Local.lighthousErrWindow : ((!Session.Account.WorldFlag.LighthouseEnable()) ? Local.PortSelectFlagsWindow_0 : null)), Global.Player.NearPortType == PortEnteringType.Port && Global.Player.NearPort.ReferencedPharosesTransformed.Size > 0 && !EducationHelper.MakeInvisibleExitInLighthouse, {19086}.ItemStyle.Default, null);
			int num = 2;
			Rectangle {19118} = new Rectangle(1712, 3285, 64, 64);
			string skill_47_name = Local.skill_47_name;
			Action {19120} = delegate()
			{
				new {22094}(delegate(ValueTuple<object, {22094}.Mode> {19126})
				{
					{22094}.MakeWorldTravel(true, {19126}.Item2, {19126}.Item1);
				}, {22094}.Mode.SelectNearTravelPort);
			};
			string {19121} = (Session.Account.IsEducationInProgress(EducationOnboarding.WorldTravel, true, true) || Session.Account.Rang >= 10) ? null : Local.education_quest_not_opened_2;
			bool {19122};
			if (Global.Player.NearPortType != PortEnteringType.Port)
			{
				if (Global.Player.NearPortType == PortEnteringType.PersonalIsle)
				{
					PersonalIsleStatus currentPersonalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
					{19122} = (currentPersonalIsle != null && currentPersonalIsle.AllowWorldTravel);
				}
				else
				{
					{19122} = false;
				}
			}
			else
			{
				{19122} = true;
			}
			array[num] = new {19086}.Item({19118}, skill_47_name, {19120}, {19121}, {19122}, {19086}.ItemStyle.Default, null);
			int num2 = 3;
			Rectangle {19118}2 = new Rectangle(1777, 3285, 64, 64);
			string portVisualScene_ = Local.PortVisualScene_4;
			Action {19120}2 = delegate()
			{
				new {18981}();
			};
			string {19121}2 = null;
			bool {19122}2;
			if (Global.Player.NearPortType != PortEnteringType.Port)
			{
				if (Global.Player.NearPortType == PortEnteringType.PersonalIsle)
				{
					PersonalIsleStatus currentPersonalIsle2 = Global.Game.ScenePort.CurrentPersonalIsle;
					{19122}2 = (currentPersonalIsle2 != null && currentPersonalIsle2.AllowArenaAndModes);
				}
				else
				{
					{19122}2 = false;
				}
			}
			else
			{
				{19122}2 = true;
			}
			array[num2] = new {19086}.Item({19118}2, portVisualScene_, {19120}2, {19121}2, {19122}2, {19086}.ItemStyle.Default, null);
			int num3 = 4;
			Rectangle {19118}3 = new Rectangle(1712, 3350, 64, 64);
			string arena = Local.arena;
			Action {19120}3 = delegate()
			{
				new {18826}();
			};
			string {19121}3 = null;
			bool {19122}3;
			if (Global.Player.NearPortType != PortEnteringType.Port)
			{
				if (Global.Player.NearPortType == PortEnteringType.PersonalIsle)
				{
					PersonalIsleStatus currentPersonalIsle3 = Global.Game.ScenePort.CurrentPersonalIsle;
					{19122}3 = (currentPersonalIsle3 != null && currentPersonalIsle3.AllowArenaAndModes);
				}
				else
				{
					{19122}3 = false;
				}
			}
			else
			{
				{19122}3 = true;
			}
			array[num3] = new {19086}.Item({19118}3, arena, {19120}3, {19121}3, {19122}3, {19086}.ItemStyle.Default, (Session.ArenaGames.TotalCountWaitGames > 0 && !EducationHelper.MakeInvisibleArenaAndModesPortButtons) ? Local.LeftUpPanel_8(Session.ArenaGames.TotalCountWaitGames) : null);
			new {19086}(array);
		}

		// Token: 0x040008B6 RID: 2230
		public static {19086} CurrentMenu;

		// Token: 0x040008B7 RID: 2231
		public static readonly Rectangle backgroundGradient = new Rectangle(2135, 3834, 301, 163);

		// Token: 0x040008B8 RID: 2232
		public static readonly Rectangle cSelectedItemBackground = new Rectangle(2495, 861, 414, 56);

		// Token: 0x040008B9 RID: 2233
		public static readonly Rectangle c_btHolderEnter = new Rectangle(2298, 3146, 64, 64);

		// Token: 0x040008BA RID: 2234
		public static readonly Rectangle c_btHolderSpace = new Rectangle(2363, 3146, 64, 64);

		// Token: 0x040008BB RID: 2235
		public static readonly Rectangle c_btHolderEmpty = new Rectangle(2428, 3146, 64, 64);

		// Token: 0x040008BC RID: 2236
		private const float ignoreMouseThreshold = 5f;

		// Token: 0x040008BD RID: 2237
		private Tlist<{19086}.ItemUi> {19091} = new Tlist<{19086}.ItemUi>();

		// Token: 0x040008BE RID: 2238
		private {19086}.ItemUi {19092};

		// Token: 0x040008BF RID: 2239
		private bool {19093} = true;

		// Token: 0x040008C0 RID: 2240
		private bool {19094} = true;

		// Token: 0x020001AC RID: 428
		public enum ItemStyle
		{
			// Token: 0x040008C2 RID: 2242
			Default,
			// Token: 0x040008C3 RID: 2243
			Link
		}

		// Token: 0x020001AD RID: 429
		private class ItemUi : Form
		{
			// Token: 0x170000FA RID: 250
			// (get) Token: 0x060009CB RID: 2507 RVA: 0x0004D1CA File Offset: 0x0004B3CA
			// (set) Token: 0x060009CC RID: 2508 RVA: 0x0004D1D7 File Offset: 0x0004B3D7
			public Color Foreground
			{
				get
				{
					return this.{19103}.BasicColor;
				}
				set
				{
					this.{19103}.BasicColor = value;
					this.{19104}.BasicColor = value;
					this.{19106}.BasicColor = value;
					if (this.{19105} != null)
					{
						this.{19105}.BasicColor = value;
					}
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x060009CD RID: 2509 RVA: 0x0004D211 File Offset: 0x0004B411
			// (set) Token: 0x060009CE RID: 2510 RVA: 0x0004D21C File Offset: 0x0004B41C
			public bool IsMenuFocused
			{
				get
				{
					return this.{19107};
				}
				set
				{
					if (this.{19107} != value)
					{
						Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UIClick, 0.03f, 0.8f);
						this.Foreground = (value ? {19086}.MenuItemSelectedColor : {19086}.MenuItemColor);
						this.TexturePath = ((value && this.{19104}.Text.Length > 0) ? {19086}.cSelectedItemBackground : Rectangle.Empty);
						this.{19107} = value;
						base.RemoveAnimations();
						if (this.{19104}.Text.Length > 0)
						{
							if (value)
							{
								Marker pos = base.Pos;
								float {11535} = this.{19108}.X + 10f;
								float y = base.Pos.XY.Y;
								Marker pos2 = base.Pos;
								new UiMarkerAnimation(this, pos, new Marker({11535}, y, ref pos2.WH), 250f);
							}
							else
							{
								Marker pos3 = base.Pos;
								float x = this.{19108}.X;
								float y2 = base.Pos.XY.Y;
								Marker pos2 = base.Pos;
								new UiMarkerAnimation(this, pos3, new Marker(x, y2, ref pos2.WH), 250f);
							}
						}
					}
					this.{19106}.IsVisible = value;
				}
			}

			// Token: 0x060009CF RID: 2511 RVA: 0x0004D348 File Offset: 0x0004B548
			public ItemUi(Vector2 {19100}, {19086}.Item {19101}, Action {19102})
			{
				float x = {19100}.X;
				float y = {19100}.Y;
				Vector2 vector = {19086}.cSelectedItemBackground.WidthHeight() * {19086}.BackgroundScale;
				base..ctor(new Marker(x, y, ref vector), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				{19086}.ItemUi <>4__this = this;
				this.AnimatedFocus = false;
				this.TexturePath = Rectangle.Empty;
				this.{19108} = {19100};
				int num = 15;
				Form form = new Form(new Marker(0f, 0f, (float){19086}.IconSize, (float){19086}.IconSize), {19101}.Icon, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form.BasicColor = {19086}.MenuItemColor;
				form.AnimatedFocus = false;
				Form {12952} = form;
				this.{19103} = form;
				base.AddChildPos({12952}, PositionAlignment.LeftUp, PositionAlignment.Center, (float)num);
				Label label = new Label(Vector2.Zero, {19086}.MenuItemFont, {19086}.MenuItemColor, {19101}.Text.ToUpper(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				label.Shadowed = true;
				Label {12952}2 = label;
				this.{19104} = label;
				base.AddChildPos({12952}2, PositionAlignment.LeftUp, PositionAlignment.Center, (float)({19086}.IconSize + 9 + num));
				Form form2 = new Form(new Marker(0f, 0f, 33f, 33f), (Global.Settings.kb_Action.Key == Keys.Enter) ? {19086}.c_btHolderEnter : ((Global.Settings.kb_Action.Key == Keys.Space) ? {19086}.c_btHolderSpace : {19086}.c_btHolderEmpty), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form2.AnimatedFocus = false;
				{12952} = form2;
				this.{19106} = form2;
				base.AddChildPos({12952}, PositionAlignment.LeftUp, PositionAlignment.Center, (float)({19086}.IconSize + 9 + num) + this.{19104}.Pos.WH.X + 7f);
				if (Global.Settings.kb_Action.Key != Keys.Enter && Global.Settings.kb_Action.Key != Keys.Space)
				{
					this.{19106}.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_16Bold, {19086}.MenuItemSelectedColor, Global.Settings.kb_Action.Key.GetKeyName(), PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.Center, 0f);
				}
				UiControl uiControl = this.{19104};
				Marker pos = this.{19104}.Pos;
				uiControl.Pos = pos.Offset(0f, 2f);
				pos = base.Pos;
				base.Pos = pos.Offset(-5f, 0f);
				Marker pos2 = base.Pos;
				float x2 = this.{19108}.X;
				float y2 = base.Pos.XY.Y;
				pos = base.Pos;
				new UiMarkerAnimation(this, pos2, new Marker(x2, y2, ref pos.WH), 250f);
				if ({19101}.Style == {19086}.ItemStyle.Link)
				{
					this.{19104}.Text = "";
					this.{19106}.Opacity = 0f;
					this.{19103}.ToolTipState = new ToolTipState("", {19101}.Text, Array.Empty<ToolTipCharacteristics>());
				}
				if (!string.IsNullOrEmpty({19101}.Crossout))
				{
					this.{19104}.Opacity = 0.6f;
					this.{19103}.Opacity = 0.5f;
					this.{19106}.Opacity = 0f;
					this.{19104}.SetStrikethroughDecoration(CommonAtlas.whitePixel, CommonAtlas.Texture.Tex);
					Label label2 = new Label(Vector2.Zero, Fonts.Philosopher_14Bold, Color.Lerp({19086}.MenuItemColor, Color.OrangeRed, 0.2f), {19101}.Crossout.ToUpper(), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						Shadowed = true
					};
					base.AddChildPos(label2, PositionAlignment.LeftUp, PositionAlignment.Center, (float)({19086}.IconSize + 20 + num) + this.{19104}.Pos.WH.X);
					UiControl uiControl2 = label2;
					pos = label2.Pos;
					uiControl2.Pos = pos.Offset(0f, 2f);
					return;
				}
				if (!string.IsNullOrEmpty({19101}.Extra))
				{
					Label label3 = new Label(Vector2.Zero, Fonts.Philosopher_14Bold, Color.Lerp({19086}.MenuItemColor, Color.Wheat, 0.2f), {19101}.Extra, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						Shadowed = true
					};
					base.AddChildPos(label3, PositionAlignment.LeftUp, PositionAlignment.Center, (float)({19086}.IconSize + 20 + num) + this.{19104}.Pos.WH.X);
					UiControl uiControl3 = label3;
					pos = label3.Pos;
					uiControl3.Pos = pos.Offset(0f, 2f);
					label3.UpdateComplete += delegate(UiControl {19109})
					{
						{19109}.IsVisible = !<>4__this.IsMenuFocused;
					};
				}
				base.EvClick += delegate(ClickUiEventArgs {19110})
				{
					{19102}();
				};
			}

			// Token: 0x040008C4 RID: 2244
			private Form {19103};

			// Token: 0x040008C5 RID: 2245
			private Label {19104};

			// Token: 0x040008C6 RID: 2246
			private Label {19105};

			// Token: 0x040008C7 RID: 2247
			private Form {19106};

			// Token: 0x040008C8 RID: 2248
			private bool {19107};

			// Token: 0x040008C9 RID: 2249
			private Vector2 {19108};
		}

		// Token: 0x020001AF RID: 431
		public struct Item
		{
			// Token: 0x060009D3 RID: 2515 RVA: 0x0004D7D3 File Offset: 0x0004B9D3
			public Item(Rectangle {19118}, string {19119}, Action {19120}, string {19121} = null, bool {19122} = true, {19086}.ItemStyle {19123} = {19086}.ItemStyle.Default, string {19124} = null)
			{
				this.Visible = {19122};
				this.Text = {19119};
				this.Icon = {19118};
				this.Click = {19120};
				this.Style = {19123};
				this.Crossout = {19121};
				this.Extra = {19124};
			}

			// Token: 0x040008CC RID: 2252
			public bool Visible;

			// Token: 0x040008CD RID: 2253
			public string Text;

			// Token: 0x040008CE RID: 2254
			public Rectangle Icon;

			// Token: 0x040008CF RID: 2255
			public Action Click;

			// Token: 0x040008D0 RID: 2256
			public {19086}.ItemStyle Style;

			// Token: 0x040008D1 RID: 2257
			public string Crossout;

			// Token: 0x040008D2 RID: 2258
			public string Extra;
		}
	}
}
