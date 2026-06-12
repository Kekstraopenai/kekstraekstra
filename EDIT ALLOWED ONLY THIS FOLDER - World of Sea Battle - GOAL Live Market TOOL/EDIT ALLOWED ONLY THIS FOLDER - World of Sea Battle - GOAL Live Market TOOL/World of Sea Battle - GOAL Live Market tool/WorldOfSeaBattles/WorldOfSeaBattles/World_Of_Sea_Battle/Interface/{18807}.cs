using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Common;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using TheraEngine.Scene.ParticleSystem;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000186 RID: 390
	internal sealed class {18807} : CustomUi
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x000070D7 File Offset: 0x000052D7
		protected override bool CanBeWindow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00044B24 File Offset: 0x00042D24
		public {18807}() : base(true)
		{
			if (Global.Game.GetCurrentSceneName == GameSceneName.Game)
			{
				Global.Player.ResetSpeedAndRotation();
			}
			{18807}.CurrentInstance = this;
			this.{18823} = (Session.Group != null);
			this.TexturePath = CommonAtlas.whitePixel;
			this.BasicColor = Color.Black * 0.5f;
			this.AnimatedFocus = false;
			Global.Game.SceneGame.IncreaseMouse();
			Rectangle uiarea = Engine.GS.UIArea;
			this.{18817} = new Form(Marker.FromCentrScreen(new Marker(ref uiarea), new Rectangle(0, 0, {18807}.main.Width * 2, {18807}.main.Height * 2)), {18807}.main, PositionAlignment.Center, PositionAlignment.Center);
			this.{18817}.AnimatedFocus = false;
			new UiOpacityAnimation(this.{18817}, 0f, 1f, 700f);
			this.{18822} = new Button(this.{18817}.Pos.XY + new Vector2(239f, 381f), {18807}.c_undoButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{18822}.SetText(Local.exit, Fonts.Philosopher_14, new Color(164, 181, 197), false);
			this.{18822}.EvClick += this.{18814};
			this.{18817}.AddChild(new UiControl[]
			{
				this.{18822},
				new Label(this.{18817}.Pos.XY + new Vector2(336f, 148f), Fonts.Philosopher_18, Color.White * 0.7f, Local.ship + StringHelper.ToRoman(Global.Player.UsedShipPlayer.CraftFrom.Rank) + Local.StringConstants_115, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center()
			});
			base.AddChild(this.{18817});
			base.EvRemoveFromContainer += this.{18816};
			this.{18818} = Stopwatch.StartNew();
			Global.Game.ScenePort.UpdateTopmostControls();
			base.AddChild(new Label(new Vector2((float)(Engine.GS.UIArea.Width / 2), (float)(Engine.GS.UIArea.Height / 2 + 190)), Fonts.Philosopher_16, Color.White * 0.7f, Session.CachedUiArenaMode.Value.ArenaModeName(true), PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			base.AddChild(TextBlockBuilder.CreateBlock(500f, Session.CachedUiArenaMode.Value.ToStringDescription(), Color.White * 0.5f, Fonts.Philosopher_14, -2f).CreateCentroid(new Vector2((float)(Engine.GS.UIArea.Width / 2), (float)(Engine.GS.UIArea.Height / 2 + 190 + 20))));
			this.{18819} = new ParticleSystem2D(Engine.GS.UIArea.HalfWidthHeightInt(), 1f, FXEngine.ArenaJoinGuiParticleEffect, Color.White * 0.1f, new Range1D(-150f, 150f), new Range1D(-150f, 150f), Global.Render.ParticleManager2D);
			this.{18819}.CountPerSecound = 100f;
			this.{18818}.Restart();
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00044E8C File Offset: 0x0004308C
		protected override void UserUpdate(ref FrameTime {18808})
		{
			if (this.{18819} != null)
			{
				this.{18819}.Centr = Engine.GS.UIArea.HalfWidthHeightInt();
			}
			this.{18821} = Math.Max(0f, this.{18821} - {18808}.msElapsed);
			if (Global.Game.GetCurrentSceneName == GameSceneName.Game && Global.Player.GetBattleTimer > 0f)
			{
				this.{18822}.ImitateClick(false);
			}
			if ((!Global.Settings.kb_Escape.IsClick || !base.IsTopmostCustomUi) && (Session.CachedUiArenaMode.GetValueOrDefault() != ArenaMode.DuelRating || Session.EventActionsPipeline.EventCategoryAmount(EActionCaterory.BArenaAllowTournamentGame) != 0f))
			{
				ArenaMode? cachedUiArenaMode = Session.CachedUiArenaMode;
				ArenaMode arenaMode = ArenaMode.DuelTraning;
				if (!(cachedUiArenaMode.GetValueOrDefault() == arenaMode & cachedUiArenaMode != null) || this.{18823} == (Session.Group != null))
				{
					return;
				}
			}
			this.{18822}.ImitateClick(false);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000201BB File Offset: 0x0001E3BB
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(CommonAtlas.Texture);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00044F74 File Offset: 0x00043174
		protected override void UserFrontRender()
		{
			Engine.GS.ReturnBackTexture();
			Engine.GS.SetFont(Fonts.Philosopher_18);
			ArenaMode? cachedUiArenaMode = Session.CachedUiArenaMode;
			ArenaMode arenaMode = ArenaMode.DuelTraning;
			string text;
			if (!(cachedUiArenaMode.GetValueOrDefault() == arenaMode & cachedUiArenaMode != null))
			{
				text = Local.ArenaJoinGui_2 + new TimeSpan(0, 0, (int)(this.{18821} / 1000f)).ToString("mm\\:ss");
			}
			else if (Session.Group != null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
				defaultInterpolatedStringHandler.AppendFormatted(Local.ChatBoxGui_4);
				defaultInterpolatedStringHandler.AppendLiteral(": ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(Session.LastMinimapAndGroupUpdate.allies.Count((AllyStateTransfer {18824}) => {18824}.IsWaitingTrainingArena) + 1);
				defaultInterpolatedStringHandler.AppendLiteral(" / ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(Session.Group.Members.Size);
				text = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			else
			{
				text = this.{18818}.Elapsed.ToString("mm\\:ss");
			}
			string text2 = text;
			Device gs = Engine.GS;
			string {14610} = text2;
			Vector2 vector = this.{18817}.Pos.XY + new Vector2(331f, 180f);
			Color white = Color.White;
			gs.DrawStringCentered({14610}, vector, white);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x000450C8 File Offset: 0x000432C8
		private string {18809}(byte[] {18810})
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < {18810}.Length; i++)
			{
				if ({18810}[i] > 0)
				{
					stringBuilder.Append(StringHelper.ToRoman(i + 1) + ": " + {18810}[i].ToString() + " ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00045120 File Offset: 0x00043320
		internal void UpdateQueue(byte[] {18811}, float {18812}, ArenaGamesCount {18813})
		{
			if (Session.CachedUiArenaMode == null)
			{
				return;
			}
			if (this.{18820} != null)
			{
				this.{18820}.RemoveFromContainer();
			}
			ArenaModeSettings info = Session.CachedUiArenaMode.Value.GetInfo();
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_12, -1f);
			int num = {18811}.Sum((byte {18825}) => (int){18825});
			ArenaMode modeEnum = info.ModeEnum;
			bool flag = modeEnum - ArenaMode.TowerDefence <= 2;
			int num2 = flag ? (ArenaModeSettings.MinPlayersPeerTeamForMassiveModes * 2) : 0;
			textBlockBuilder.WriteLine((Session.Group != null && info.ModeEnum == ArenaMode.DuelTraning) ? Local.waiting_group_arena_tt : Local.ArenaJoinGui_1, Color.White * 0.3f, Fonts.Philosopher_14);
			TextBlockBuilder textBlockBuilder2 = textBlockBuilder;
			string arenaJoinGui_ = Local.ArenaJoinGui_3;
			string str = " ";
			string str2 = (num == 1) ? "1" : (info.HideQuantity ? "1+" : num.ToString());
			string str3;
			if (num2 != 0 && num <= num2)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
				defaultInterpolatedStringHandler.AppendLiteral(" / ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(Local.ArenaJoinGui_3_min);
				str3 = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			else
			{
				str3 = "";
			}
			textBlockBuilder2.WriteLine(arenaJoinGui_ + str + str2 + str3, (num < num2) ? Color.Lerp(Color.Yellow, Color.LightYellow, 0.5f) : (Color.White * 0.5f), Fonts.Philosopher_14);
			if (!info.HideQuantity)
			{
				textBlockBuilder.WriteLine(Local.ArenaJoinGui_4 + " " + this.{18809}({18811}), Color.White * 0.5f, Fonts.Arial_10);
			}
			if ({18813}.TotalCountRunGames > 0)
			{
				textBlockBuilder.WriteLine(Local.ArenaJoinGui_5({18813}.TotalCountRunGames), Color.White * 0.5f, Fonts.Philosopher_14);
			}
			this.{18820} = textBlockBuilder.Create(this.{18817}.Pos.XY + new Vector2(164f, 203f));
			this.{18820}.PositionAlignment_X = PositionAlignment.Center;
			this.{18820}.PositionAlignment_Y = PositionAlignment.Center;
			base.AddChild(this.{18820});
			this.{18821} = {18812};
			Session.ArenaGames = {18813};
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000453CA File Offset: 0x000435CA
		[CompilerGenerated]
		private void {18814}(ClickUiEventArgs {18815})
		{
			base.RemoveFromContainer();
			new {18826}();
			Global.Network.Send(new OnJoinOrLeaveArena(ArenaMode.DuelTraning, false, null));
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000453EF File Offset: 0x000435EF
		[CompilerGenerated]
		private void {18816}()
		{
			{18807}.CurrentInstance = null;
			ParticleSystem2D particleSystem2D = this.{18819};
			if (particleSystem2D != null)
			{
				particleSystem2D.Remove();
			}
			Global.Game.SceneGame.DecreaseMouse();
		}

		// Token: 0x040007E4 RID: 2020
		public static readonly Rectangle main = new Rectangle(765, 2580, 336, 267);

		// Token: 0x040007E5 RID: 2021
		public static readonly Rectangle c_undoButton = new Rectangle(905, 2545, 196, 34);

		// Token: 0x040007E6 RID: 2022
		public static readonly Rectangle c_loadingCircle = new Rectangle(2139, 788, 50, 50);

		// Token: 0x040007E7 RID: 2023
		public static {18807} CurrentInstance;

		// Token: 0x040007E8 RID: 2024
		private Form {18817};

		// Token: 0x040007E9 RID: 2025
		private Stopwatch {18818};

		// Token: 0x040007EA RID: 2026
		private ParticleSystem2D {18819};

		// Token: 0x040007EB RID: 2027
		private TextBlockControl {18820};

		// Token: 0x040007EC RID: 2028
		private float {18821};

		// Token: 0x040007ED RID: 2029
		private Button {18822};

		// Token: 0x040007EE RID: 2030
		private bool {18823};
	}
}
