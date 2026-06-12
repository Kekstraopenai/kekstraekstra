using System;
using Common;
using Common.Account;
using Common.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000231 RID: 561
	internal sealed class {19970} : CustomUi
	{
		// Token: 0x06000CB4 RID: 3252 RVA: 0x00068984 File Offset: 0x00066B84
		public {19970}()
		{
			Vector2 vector = new Vector2(-1f, -5f);
			base..ctor(new Marker(ref vector, ref {19970}.main), {19970}.main, PositionAlignment.LeftUp, PositionAlignment.LeftUp, Color.White, false);
			{20391}.WhenInit(this, "moneyBar");
			{19970}.CurrentInstance = this;
			this.AnimatedFocus = false;
			this.{19980} = -1;
			CustomSpriteFont arial_ = Fonts.Arial_10;
			this.{19976} = new Label(base.Pos.XY + new Vector2(75f, 31f), Fonts.Arial_12, new Color(104, 144, 145) * 0.9f, " ", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{19978} = new Label(base.Pos.XY + new Vector2(96f, 31f), Fonts.Arial_8, Color.White * 0.5f, " ", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{19977} = new Label(base.Pos.XY + new Vector2(94f, 54f), arial_, Color.LightYellow * 0.75f, " ", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			UiControl[] array = new UiControl[4];
			array[0] = (this.{19979} = new LiveLabel(base.Pos.XY + new Vector2(75f, 10f), Fonts.Philosopher_14, Color.White * 0.7f, null, delegate(object {19985})
			{
				if (Session.Guild != null)
				{
					return "[" + Session.Guild.Tag + "] " + Session.Account.PlayerName;
				}
				return Session.Account.PlayerName;
			}, 5000));
			array[1] = this.{19976};
			array[2] = this.{19978};
			array[3] = this.{19977};
			base.AddChild(array);
			this.{19977}.ToolTip = new ToolTip((UiControl {19986}) => new ToolTipState(Session.Account.Gold.ToString(), "", Array.Empty<ToolTipCharacteristics>()));
			vector = base.Pos.XY + new Vector2(2f, 7f);
			this.{19983} = new Button(new Marker(ref vector, 70f, 69f), Rectangle.Empty, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{19983}.ToolTipState = new ToolTipState(Local.captain_skills, "", Array.Empty<ToolTipCharacteristics>());
			base.AddChild(this.{19983});
			this.{19983}.EvClick += delegate(ClickUiEventArgs {19987})
			{
				new {19150}();
				if (Global.Player.IsPortEntry && !Global.Game.ScenePort.IsMainPage)
				{
					Global.Game.ScenePort.mainHandler(null);
				}
			};
			base.EvRemoveFromContainer += delegate()
			{
				{19970}.CurrentInstance = null;
			};
			{20500}.CheckGuildAnnouncment();
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00068C44 File Offset: 0x00066E44
		protected override void UserUpdate(ref FrameTime {19971})
		{
			base.IsVisible = (Global.Player != null && Global.Render.UiMode != InterfaceMode.Off);
			if (Global.Render.UiMode == InterfaceMode.Cinematografic)
			{
				base.Opacity = Renderer.UiOpacityToFocus(base.Pos);
			}
			else
			{
				base.Opacity = 1f;
			}
			if (this.{19982} != Session.Account.Gold || this.{19977}.Text == " ")
			{
				this.{19982} = Session.Account.Gold;
				this.{19977}.Text = {19970}.intToStrLimited(Session.Account.Gold);
			}
			this.{19977}.BasicColor = ((Global.Player != null && !Global.Game.ScenePort.IsMainPage && Global.Player.IsPortEntry) ? Color.LightYellow : (Color.LightYellow * 0.75f));
			this.{19976}.Text = Session.Account.Rang.ToString();
			int xp = Session.Account.Xp;
			int rang = Session.Account.Rang;
			if (this.{19980} != xp || this.{19981} != rang)
			{
				int xpToNextRang = Session.Account.XpToNextRang;
				this.{19984} = Geometry.Saturate((float)xp / (float)xpToNextRang);
				if (rang - 1 == Gameplay.RangsInfo.Count)
				{
					this.{19978}.Text = xp.ToString();
				}
				else
				{
					this.{19978}.Text = xp.ToString() + Local.MoneyBarGui_0(xpToNextRang);
				}
				this.{19980} = xp;
				this.{19981} = rang;
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00068DE8 File Offset: 0x00066FE8
		public static string intToStrLimited(int {19972})
		{
			if ({19972} > 999999)
			{
				return ({19972} / 1000).ToString() + "k";
			}
			return {19972}.ToString();
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00068E20 File Offset: 0x00067020
		public static string intToStrLimitedShort(int {19973})
		{
			if ({19973} > 99999)
			{
				return ({19973} / 1000).ToString() + "k";
			}
			return {19973}.ToString();
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000201BB File Offset: 0x0001E3BB
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(CommonAtlas.Texture);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00068E58 File Offset: 0x00067058
		protected override void UserFrontRender()
		{
			float opcaity = base.GetOpcaity();
			Color color = Color.White * opcaity;
			Rectangle rectangle;
			rectangle.X = (int)base.Pos.XY.X + 97;
			rectangle.Y = (int)base.Pos.XY.Y + 42;
			rectangle.Width = (int)((float){19970}.c_xpProgress.Width * this.{19984});
			rectangle.Height = {19970}.c_xpProgress.Height;
			Engine.GS.Draw({19970}.c_xpProgress, rectangle, color);
			Rectangle captainIcon = Session.CaptainIcon;
			Marker marker = new Marker(0f, 0f, 64f, 64f);
			Vector2 vector = base.Pos.XY + {19970}.positoionCaptainSkills;
			Marker marker2 = marker.Offset(vector);
			float scale = Session.Account.IsEducationInProgress(EducationOnboarding.ResearchSkillSailingCategory, false, false) ? (0.7f + 0.3f * (float)Math.Sin(4.400000095367432 * Global.Game.GameTotalTimeSec)) : 1f;
			Device gs = Engine.GS;
			Rectangle rectangle2 = marker2.ToRect();
			Color color2 = ((this.{19983}.InputMode == MouseInputMode.Focused) ? Color.Gold : Color.White) * opcaity * scale;
			gs.Draw(captainIcon, rectangle2, color2);
			if (Session.Account.IsPremium)
			{
				Device gs2 = Engine.GS;
				rectangle2 = new Rectangle(65, 754, 64, 64);
				Rectangle rectangle3 = marker2.ToRect();
				color2 = Color.White * opcaity * 0.75686276f;
				gs2.Draw(rectangle2, rectangle3, color2);
			}
			vector = base.Pos.XY + new Vector2(2f, 77f);
			{19970}.DrawWantedMarkers(vector, color);
			Engine.GS.ReturnBackTexture();
			int skillPointsAvailable = Session.Account.SkillPointsAvailable;
			if (skillPointsAvailable != 0 && EducationHelper.ShowSkillPointsToolTip)
			{
				Device gs3 = Engine.GS;
				Texture2D chatAtl = OtherTextures.ChatAtl;
				rectangle2 = {22478}.GetNotificationPath(skillPointsAvailable);
				vector = marker2.XY + new Vector2(2f, 49f);
				color2 = Color.White * opcaity;
				gs3.DrawCustomTexture(chatAtl, rectangle2, vector, color2);
			}
			if (Session.Account.SelectedCaptainTitle != CaptainTitle.None)
			{
				Rectangle captainTitleIcon = AtlasObjs.GetCaptainTitleIcon(Session.Account.SelectedCaptainTitle);
				Device gs4 = Engine.GS;
				Texture2D tex = AtlasObjs.Texture.Tex;
				rectangle2 = new Marker(this.{19979}.Pos.End.X + 2f, this.{19979}.Pos.Center.Y - 14f - 1f, 28f, 28f).ToRect();
				color2 = Color.White * opcaity * 0.7f;
				gs4.DrawCustomTexture(tex, captainTitleIcon, rectangle2, color2);
			}
			if ((Session.CurrentArenaSession != null && Session.CurrentArenaSession.ModeInfo.EnableDooblonsAndUpgrades) || Session.CurrentPassingSession != null)
			{
				Rectangle rectangle4 = new Rectangle(41, 40, 173, 39);
				Device gs5 = Engine.GS;
				rectangle2 = new Rectangle(2579, 176, 226, 50);
				color2 = Color.White;
				gs5.Draw(rectangle2, rectangle4, color2);
				Device gs6 = Engine.GS;
				rectangle2 = new Rectangle(2579, 176, 226, 50);
				color2 = Color.White;
				gs6.Draw(rectangle2, rectangle4, color2);
				Engine.GS.SetFont(Fonts.Arial_12);
				Device gs7 = Engine.GS;
				string {14599} = Local.doublones + ": " + Global.Settings.GamemodeDoublones.Value.ToString();
				vector = new Vector2((float)rectangle4.X, (float)rectangle4.Y) + new Vector2(47f, 7f);
				color2 = Color.White;
				gs7.DrawString({14599}, vector, color2);
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0006924C File Offset: 0x0006744C
		public static bool DrawWantedMarkers(in Vector2 {19974}, in Color {19975})
		{
			int num = 0;
			if (Global.Player.GetBattleTimer > 0f && !Global.Player.IsPortEntry)
			{
				Vector2 value = {19974} + new Vector2((float)num, 0f);
				Engine.GS.Draw({19970}.c_battleTimer, value, {19975});
				Engine.GS.SetFont(Fonts.Arial_10);
				Device gs = Engine.GS;
				string {14590} = Global.Player.GetBattleTimerSec.ToString();
				Vector2 vector = value + new Vector2((float)({19970}.c_battleTimer.Width / 2 - 3), 30f);
				Color color = {19975} * 0.9f;
				gs.DrawStringCenteredShadow({14590}, vector, color, 0.8f);
				num += {19970}.c_wantedLevelMarker.Width;
			}
			if (Session.Account.CurrentBlackMarkTime > 0f)
			{
				Device gs2 = Engine.GS;
				Vector2 vector = {19974} + new Vector2((float)num, 0f);
				gs2.Draw({19970}.c_wantedMarkerBack, vector, {19975});
				Device gs3 = Engine.GS;
				vector = {19974} + new Vector2((float)num, 0f);
				gs3.DrawProgressbar({19970}.c_wantedLevelMarker, vector, Math.Min(1f, Session.Account.CurrentBlackMarkTime / 900f), {19975});
				num += {19970}.c_wantedLevelMarker.Width;
			}
			if (Session.Account.WantedLevelNps > 0.1f)
			{
				Device gs4 = Engine.GS;
				Vector2 vector = {19974} + new Vector2((float)num, 0f);
				gs4.Draw({19970}.c_wantedMarkerBack, vector, {19975});
				Device gs5 = Engine.GS;
				vector = {19974} + new Vector2((float)num, 0f);
				gs5.DrawProgressbar({19970}.c_wantedRedMarker, vector, Session.Account.WantedLevelNps, {19975});
				num += {19970}.c_wantedLevelMarker.Width;
			}
			return num > 0;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x04000BC8 RID: 3016
		public static {19970} CurrentInstance;

		// Token: 0x04000BC9 RID: 3017
		public static readonly Rectangle main = new Rectangle(2491, 589, 254, 76);

		// Token: 0x04000BCA RID: 3018
		public static readonly Rectangle c_xpProgress = new Rectangle(2491, 666, 122, 3);

		// Token: 0x04000BCB RID: 3019
		public static readonly Rectangle c_wantedMarkerBack = new Rectangle(2567, 673, 37, 30);

		// Token: 0x04000BCC RID: 3020
		public static readonly Rectangle c_wantedLevelMarker = new Rectangle(2491, 673, 37, 30);

		// Token: 0x04000BCD RID: 3021
		public static readonly Rectangle c_wantedRedMarker = new Rectangle(2529, 673, 37, 30);

		// Token: 0x04000BCE RID: 3022
		public static readonly Rectangle c_battleTimer = new Rectangle(2453, 673, 37, 30);

		// Token: 0x04000BCF RID: 3023
		private static readonly Vector2 positoionCaptainSkills = new Vector2(5f, 10f);

		// Token: 0x04000BD0 RID: 3024
		private static readonly Vector2 positionCaptainSkillAvailable = new Vector2(41f, 51f);

		// Token: 0x04000BD1 RID: 3025
		private Label {19976};

		// Token: 0x04000BD2 RID: 3026
		private Label {19977};

		// Token: 0x04000BD3 RID: 3027
		private Label {19978};

		// Token: 0x04000BD4 RID: 3028
		private LiveLabel {19979};

		// Token: 0x04000BD5 RID: 3029
		private int {19980};

		// Token: 0x04000BD6 RID: 3030
		private int {19981};

		// Token: 0x04000BD7 RID: 3031
		private int {19982};

		// Token: 0x04000BD8 RID: 3032
		private Button {19983};

		// Token: 0x04000BD9 RID: 3033
		private float {19984};
	}
}
