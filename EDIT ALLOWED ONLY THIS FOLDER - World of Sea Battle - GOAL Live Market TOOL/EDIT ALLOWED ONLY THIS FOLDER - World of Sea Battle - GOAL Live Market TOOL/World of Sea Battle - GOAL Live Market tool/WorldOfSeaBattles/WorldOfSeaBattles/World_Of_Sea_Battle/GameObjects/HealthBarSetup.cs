using System;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Interface;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x02000062 RID: 98
	internal struct HealthBarSetup
	{
		// Token: 0x06000311 RID: 785 RVA: 0x0001A318 File Offset: 0x00018518
		public HealthBarSetup()
		{
			this.NameFont = null;
			this.Name = null;
			this.SmallText = null;
			this.GuildTag = null;
			this.IconOrEmpty = default(Rectangle);
			this.NameMainColor = default(Color);
			this.GuildTagColor = default(Color);
			this.NameSize = default(Vector2);
			this.NameSizeOver2 = default(Vector2);
			this.TagSize = default(Vector2);
			this.TagSizeOver2 = default(Vector2);
			this.SmallTextSize = default(Vector2);
			this.HealthBarOffset = default(Vector2);
			this.iconSize = 27;
			this.iconOffset = default(Vector2);
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0001A3C1 File Offset: 0x000185C1
		public CustomSpriteFont TagFont
		{
			get
			{
				return Fonts.Arial_10Bold;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0001A3C8 File Offset: 0x000185C8
		public CustomSpriteFont SmalltextFont
		{
			get
			{
				return Fonts.Arial_9;
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001A3D0 File Offset: 0x000185D0
		public void Update(Ship {17013}, ShipPartial {17014})
		{
			ShipNpc shipNpc = {17013} as ShipNpc;
			this.NameFont = ((shipNpc != null && shipNpc.UsedShipNpc.Information.Extras.IsEmpire) ? Fonts.F_m14_Ghotic : Fonts.F_m12_Ghotic);
			if (Global.Settings.ShowNicknames && Global.Render.UiMode == InterfaceMode.Default)
			{
				ShipOtherPlayer shipOtherPlayer = {17013} as ShipOtherPlayer;
				bool flag = shipOtherPlayer != null && shipOtherPlayer.HideNickname;
				bool isFreeMode = Global.Camera.IsFreeMode;
				this.Name = {17014}.GetName2();
				this.NameSize = this.NameFont.Measure(this.Name);
				this.NameSizeOver2 = this.NameSize / 2f;
				this.NameMainColor = Color.White;
				if (shipNpc != null)
				{
					bool flag2 = shipNpc.CurrentAgressionTargetUID == Global.Player.uID;
					Npc npc = Global.Game.WorldInstance.GetShipFromUID(shipNpc.CurrentAgressionTargetUID) as Npc;
					bool flag3 = npc != null && npc.UidPlayerForCaper == Global.Player.uID;
					if (flag2 || flag3)
					{
						this.NameMainColor = Color.Lerp(Color.DarkRed, Color.White, 0.5f);
						this.NameMainColor.A = (byte)((float)this.NameMainColor.A * 0.75f);
					}
				}
				if (!flag && {17014}.IsVisibleWithOcclusionQueryAndCorpusTransparancy && Global.Player != null && Global.Game.GetCurrentSceneName == GameSceneName.Game)
				{
					IClientShip clientShip = null;
					if (Global.Game.SceneGame.MouseState == 1 && GameScene.GameHasInputFocus && Global.Settings.kb_KeyShowMouse.IsDown)
					{
						this.NameMainColor = Color.SkyBlue;
						this.Name += "≡";
						float num = (float)Engine.GS.UIArea.Width * 0.03f;
						foreach (Ship ship in Global.Game.WorldInstance.EnumerateShips(true, true, false))
						{
							IClientShip clientShip2 = (IClientShip)ship;
							if (clientShip2.GetClient.IsVisibleWithOcclusionQueryAndCorpusTransparancy)
							{
								float num2 = Vector2.Distance(clientShip2.GetClient.Graphics2DPos, Engine.GS.MouseToUI);
								if (num2 < num)
								{
									num = num2;
									clientShip = clientShip2;
								}
							}
						}
					}
					if (clientShip == {17013} && {18139}.CurrentInstance == null)
					{
						this.NameMainColor = new Color(Color.SoftLime.ToVector4() * new Vector4(1f, 1f, 1f, 0.66f));
						if (InputHelper.LeftWasClicked)
						{
							ShipOtherPlayer shipOtherPlayer2 = clientShip as ShipOtherPlayer;
							UiControl uiControl;
							if (shipOtherPlayer2 == null)
							{
								uiControl = new {17473}(delegate(object {17021})
								{
								}, new {17473}.Item[]
								{
									new {17473}.Item(null, Local.not_available, true, default(ImageDecription), null, null)
								});
							}
							else
							{
								uiControl = new {17558}(new {17549}(shipOtherPlayer2.AccountSID, clientShip.GetClient.GetName2(), Array.Empty<{17549}.OptionalAction>()));
							}
							Global.Game.SceneGame.IncreaseMouse();
							uiControl.EvRemoveFromContainer += delegate()
							{
								Global.Game.SceneGame.DecreaseMouse();
							};
						}
					}
				}
				this.GuildTag = string.Empty;
				this.TagSize = Vector2.Zero;
				if (!flag && !{17013}.MapInfo.IsEnableArenaUi && !string.IsNullOrEmpty({17014}.Guild.Tag))
				{
					this.GuildTag = "[" + {17014}.Guild.Tag + "] ";
					this.TagSize = this.TagFont.Measure(this.GuildTag);
					this.GuildTagColor = HealthBarSetup.GetColorByRelation(Relation.Neutral);
					if (Session.Guild != null)
					{
						if (Session.Guild.Tag == {17014}.Guild.Tag || Session.Guild.IsTrusted({17014}.Guild.Tag, false))
						{
							this.GuildTagColor = HealthBarSetup.GetColorByRelation(Relation.Ally);
						}
						else
						{
							this.GuildTagColor = HealthBarSetup.GetColorByRelation(FractionAPI.StatusWith(Session.Guild.Fraction, {17014}.Guild.Fraction));
						}
					}
					else
					{
						this.GuildTagColor = HealthBarSetup.GetColorByRelation(Session.Account.Reputations.NeutralStatusWith({17014}.Guild.Fraction));
					}
					if ({17014}.Guild.IsFlotilia)
					{
						this.GuildTagColor *= 0.66f;
					}
				}
				this.TagSizeOver2 = this.TagSize / 2f;
				this.IconOrEmpty = Rectangle.Empty;
				ShipOtherPlayer shipOtherPlayer3 = {17013} as ShipOtherPlayer;
				if (shipOtherPlayer3 != null)
				{
					this.IconOrEmpty = ((shipOtherPlayer3.RemoteInfo.SelectedCaptainTitle == CaptainTitle.None) ? new Rectangle(267, 659, 64, 64) : AtlasObjs.GetCaptainTitleIcon(shipOtherPlayer3.RemoteInfo.SelectedCaptainTitle));
				}
			}
			else
			{
				this.Name = string.Empty;
				this.GuildTag = string.Empty;
				this.IconOrEmpty = Rectangle.Empty;
			}
			this.SmallText = ((shipNpc != null && shipNpc.MapInfo.IsWorldmap) ? (shipNpc.OwnerName ?? string.Empty) : string.Empty);
			this.SmallTextSize = this.SmalltextFont.Measure(this.SmallText);
			this.HealthBarOffset = (ShipPartial.drawGuildTagOnHp ? new Vector2(Math.Max(0f, this.TagSizeOver2.X - 3f), 0f) : Vector2.Zero);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00003100 File Offset: 0x00001300
		public void DrawIcons(in Vector2 {17015}, float {17016})
		{
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0001A988 File Offset: 0x00018B88
		public void DrawText(in Vector2 {17017}, float {17018}, IClientShip {17019})
		{
			Vector2 vector;
			vector.X = {17017}.X - this.NameSizeOver2.X - ((this.IconOrEmpty.Width > 0) ? ((float)this.iconSize * 0.4f) : 0f);
			vector.Y = {17017}.Y - this.NameSizeOver2.Y * 2f - 4f;
			if (!ShipPartial.drawGuildTagOnHp)
			{
				vector.X -= this.TagSizeOver2.X;
			}
			if (this.GuildTag.Length > 0)
			{
				Vector2 value = ShipPartial.drawGuildTagOnHp ? ({17017} + new Vector2(-48f - this.TagSizeOver2.X, -6f)) : vector;
				Engine.GS.SetFont(Fonts.Arial_10Bold);
				Device gs = Engine.GS;
				string guildTag = this.GuildTag;
				Vector2 vector2 = value + new Vector2(-1f, 1f);
				Color color = Color.Black * {17018};
				gs.DrawString(guildTag, vector2, color);
				Device gs2 = Engine.GS;
				string guildTag2 = this.GuildTag;
				color = this.GuildTagColor * {17018};
				gs2.DrawString(guildTag2, value, color);
				if (!ShipPartial.drawGuildTagOnHp)
				{
					vector.X += this.TagSize.X;
				}
			}
			if (!string.IsNullOrEmpty(this.Name))
			{
				Engine.GS.SetFont(this.NameFont);
				Device gs3 = Engine.GS;
				string name = this.Name;
				Vector2 vector2 = vector + new Vector2(-1f, 0f);
				Color color = Color.Black * {17018};
				gs3.DrawString(name, vector2, color);
				Device gs4 = Engine.GS;
				string name2 = this.Name;
				vector2 = vector + new Vector2(0f, -1f);
				color = this.NameMainColor * {17018};
				gs4.DrawString(name2, vector2, color);
			}
			if (!string.IsNullOrEmpty(this.SmallText))
			{
				Engine.GS.SetFont(this.SmalltextFont);
				vector = {17017} - new Vector2(this.SmallTextSize.X / 2f, 0f);
				Device gs5 = Engine.GS;
				string smallText = this.SmallText;
				Vector2 vector2 = vector + new Vector2(-1f, -36f);
				Color color = Color.Black * {17018};
				gs5.DrawString(smallText, vector2, color);
				Device gs6 = Engine.GS;
				string smallText2 = this.SmallText;
				vector2 = vector + new Vector2(0f, -35f);
				color = Color.White * {17018};
				gs6.DrawString(smallText2, vector2, color);
			}
			if (this.IconOrEmpty.Width > 0)
			{
				vector.X = {17017}.X + this.NameSizeOver2.X - (float)this.iconSize * 0.4f;
				vector.Y = {17017}.Y - (float)(this.iconSize / 2) - (float)(this.iconSize / 2) - 1f;
				if (!ShipPartial.drawGuildTagOnHp)
				{
					vector.X += this.TagSizeOver2.X;
				}
				if (((Ship){17019}) is Player)
				{
					vector.Y -= 3f;
				}
				Device gs7 = Engine.GS;
				Vector2 vector2 = vector + this.iconOffset;
				Rectangle rectangle = new Marker(ref vector2, (float)this.iconSize, (float)this.iconSize).ToRect();
				Color color = Color.White * {17018};
				gs7.Draw(this.IconOrEmpty, rectangle, color);
			}
			if ({17019}.MakeTransparentForMe && EducationHelper.ShowUnavailableForAttackTt > 0f)
			{
				Engine.GS.SetFont(Fonts.Philosopher_12);
				vector = {17017} + new Vector2(0f, 20f);
				Device gs8 = Engine.GS;
				string cannot_attack = Local.cannot_attack;
				Color color = Color.White * Math.Min({17018}, EducationHelper.ShowUnavailableForAttackTt) * (1f - Geometry.InverseLerp(80f, 280f, Vector2.Distance(Engine.GS.Camera.Position.XZ(), ((Ship){17019}).Position)));
				gs8.DrawStringCentered(cannot_attack, vector, color);
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001ADA8 File Offset: 0x00018FA8
		private static Color GetColorByRelation(Relation {17020})
		{
			Color result;
			if ({17020} != Relation.Ally)
			{
				if ({17020} != Relation.Enemy)
				{
					result = Color.Gray;
				}
				else
				{
					result = Color.DarkRed;
				}
			}
			else
			{
				result = Color.Green;
			}
			return result;
		}

		// Token: 0x04000266 RID: 614
		public int iconSize;

		// Token: 0x04000267 RID: 615
		public Vector2 iconOffset;

		// Token: 0x04000268 RID: 616
		public CustomSpriteFont NameFont;

		// Token: 0x04000269 RID: 617
		public string Name;

		// Token: 0x0400026A RID: 618
		public string SmallText;

		// Token: 0x0400026B RID: 619
		public string GuildTag;

		// Token: 0x0400026C RID: 620
		public Rectangle IconOrEmpty;

		// Token: 0x0400026D RID: 621
		public Color NameMainColor;

		// Token: 0x0400026E RID: 622
		public Color GuildTagColor;

		// Token: 0x0400026F RID: 623
		public Vector2 NameSize;

		// Token: 0x04000270 RID: 624
		public Vector2 NameSizeOver2;

		// Token: 0x04000271 RID: 625
		public Vector2 TagSize;

		// Token: 0x04000272 RID: 626
		public Vector2 TagSizeOver2;

		// Token: 0x04000273 RID: 627
		public Vector2 SmallTextSize;

		// Token: 0x04000274 RID: 628
		public Vector2 HealthBarOffset;
	}
}
