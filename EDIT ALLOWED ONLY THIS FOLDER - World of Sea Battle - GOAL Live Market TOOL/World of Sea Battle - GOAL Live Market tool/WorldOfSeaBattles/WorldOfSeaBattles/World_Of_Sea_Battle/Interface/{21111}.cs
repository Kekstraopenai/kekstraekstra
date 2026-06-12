using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common.Account;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002F1 RID: 753
	internal class {21111} : {21102}
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x0008A4EC File Offset: 0x000886EC
		public bool IsDisallowInCurrentPort
		{
			get
			{
				return ((this.Ship.Coolness == PlayerShipCoolness.Default || EducationHelper.MakeInvisibleSelectShipButton) && this.Ship.Rank < Session.Game.NearPortShipRankAllowToBuild) || (this.Ship.Class != ShipClass.CargoShip && Global.Player.NearPort.IsArabPort);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x0008A548 File Offset: 0x00088748
		public bool NotResearched
		{
			get
			{
				int num;
				int num2;
				return this.Ship.Coolness == PlayerShipCoolness.Default && Session.Account.Shipyard.IsRankResearched(this.Ship.Rank, this.Ship.Class, out num, out num2) != ShipResearchStatus.Green;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x0008A593 File Offset: 0x00088793
		public PlayerShipInfo Ship { get; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x0008A59B File Offset: 0x0008879B
		// (set) Token: 0x0600107C RID: 4220 RVA: 0x0008A5A8 File Offset: 0x000887A8
		public bool ShowStats
		{
			get
			{
				return this.{21158}.IsVisible;
			}
			set
			{
				this.{21158}.IsVisible = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x0008A5B6 File Offset: 0x000887B6
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x0008A5BE File Offset: 0x000887BE
		public bool Hidden { get; set; }

		// Token: 0x0600107F RID: 4223 RVA: 0x0008A5C8 File Offset: 0x000887C8
		public {21111}(Marker {21118}, float {21119}, int {21120}, {21078} {21121}) : base({21118})
		{
			this.{21138} = {21119};
			this.Ship = Gameplay.PlayersInfo.FromID({21120});
			this.{21137} = {21121};
			if (this.Ship.Coolness == PlayerShipCoolness.Default)
			{
				this.{21141} = Session.Account.Shipyard.IsRankResearched(this.Ship.Rank, this.Ship.Class, out this.{21142}, out this.{21143});
			}
			base.Form = new Form({21118}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Transparent,
				TexturePath = CommonAtlas.whitePixel
			};
			base.Form.EvClick += this.{21128};
			base.Form.UpdateComplete += this.{21125};
			this.Recompose();
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0008A6AC File Offset: 0x000888AC
		public void Update()
		{
			if (this.{21139})
			{
				this.{21140} += (Engine.GS.MouseToUI - Engine.GS.MouseToUIPrev).Length();
				if (InputHelper.LeftWasReleased)
				{
					this.{21139} = false;
					if (this.{21140} < 15f)
					{
						foreach ({21426} {21426} in ((IEnumerable<{21426}>)this.{21137}.OpenedCraftPages))
						{
							if ({21426}.Ship == this.Ship)
							{
								{21426}.MoveToFrontLevel();
								return;
							}
						}
						{21426} craftPage = new {21426}(this.Ship, this.{21137}, this.{21137}.OpenedCraftPages.Size);
						this.{21137}.OpenedCraftPages.Add(craftPage);
						craftPage.EvRemoveFromContainer += delegate()
						{
							this.{21137}.OpenedCraftPages.Remove(craftPage);
						};
						EducationHelper.MakeFlag(EducationOnboarding.OpenVerfyAnyShip, true);
					}
				}
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0008A7D0 File Offset: 0x000889D0
		public void Recompose()
		{
			this.{21144} = this.{21138} * this.scaleFactor / (float)this.Ship.IconTextureWhitespace.Width;
			this.{21145} = (float)this.Ship.IconTextureWhitespace.Height;
			this.{21122}();
			this.{21123}();
			this.{21124}();
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0008A82C File Offset: 0x00088A2C
		private void {21122}()
		{
			int num = this.Ship.IconTextureWhitespace.Width;
			int num2 = this.Ship.IconTextureWhitespace.Width;
			float num3 = (float)Math.Max(this.Ship.IconTexture.Width + 30, this.Ship.IconTextureWhitespace.Width);
			float num4 = ((float)num - num3) / 2f;
			switch (this.{21141})
			{
			case ShipResearchStatus.Red:
				num = 0;
				break;
			case ShipResearchStatus.Yellow:
			{
				float num5 = (float)this.{21143} / (float)this.{21142};
				num5 = 0f;
				num = (int)(num4 + num3 * num5);
				num2 -= num;
				break;
			}
			case ShipResearchStatus.Green:
				num2 = 0;
				break;
			}
			if (this.{21146} == null)
			{
				this.{21146} = new Form(Marker.Zero, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				this.{21146}.AddChild(this.{21147} = new Image(Vector2.Zero, this.Ship.IconTextureWhitespace, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				Form form = this.{21146};
				Image image = new Image(Vector2.Zero, this.Ship.IconTextureWhitespaceGray, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				image.Brightness = 0.7f;
				Image {13204} = image;
				this.{21148} = image;
				form.AddChild({13204});
				if (this.Ship.Coolness == PlayerShipCoolness.Elite || this.Ship.Coolness == PlayerShipCoolness.Unique)
				{
					this.{21146}.AddChild(this.{21149} = new Image(Vector2.Zero, AtlasPortGui.Texture.Tex, {21111}.c_eliteFrame, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				}
				base.Form.AddChild(this.{21146});
			}
			Vector2 vector = base.Form.Pos.XY + new Vector2((base.Form.Pos.WH.X - this.{21138} * this.scaleFactor) / 2f, 0f);
			this.{21146}.Pos = new Marker(ref vector, (float)this.Ship.IconTextureWhitespace.Width * this.{21144}, this.{21145} * this.{21144});
			Marker marker;
			if (this.{21149} != null)
			{
				Vector2 value = new Vector2(base.Form.Pos.WH.X, 63f * this.scaleFactor);
				UiControl uiControl = this.{21149};
				marker = base.Form.Pos;
				Marker marker2 = new Marker(ref marker.XY, ref value);
				Vector2 vector2 = base.Form.Pos.WH / 2f - value / 2f;
				uiControl.Pos = marker2.Offset(vector2).Offset(0f, 15f * this.scaleFactor);
			}
			if (PlatformTuning.DisablePremAnUniqueShips && (this.Ship.Coolness != PlayerShipCoolness.Default || this.Ship.Rank == 1))
			{
				num = 0;
				num2 = this.Ship.IconTextureWhitespace.Width;
				this.{21148}.BasicColor = Color.Black * 0.5f;
			}
			this.{21147}.Pos = new Marker(ref vector, (float)num * this.{21144}, this.{21145} * this.{21144});
			this.{21147}.TexturePath = this.Ship.IconTextureWhitespace.Bounds.SetWidth((float)num);
			UiControl uiControl2 = this.{21148};
			marker = new Marker(ref vector, (float)num2 * this.{21144}, this.{21145} * this.{21144});
			uiControl2.Pos = marker.Offset((float)num * this.{21144}, 0f);
			this.{21148}.TexturePath = new Rectangle(num, 0, num2, (int)this.{21145});
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0008ABD8 File Offset: 0x00088DD8
		private void {21123}()
		{
			Color {13344} = ((!Session.Account.Shipyard.ContainsInfo(this.Ship)) ? (Color.LightGray * 0.8f) : Color.Wheat) * ((this.{21141} == ShipResearchStatus.Red) ? 0.5f : 1f);
			ShipResearchStatus shipResearchStatus = this.{21141};
			float num = 0.4f;
			PlayerShipCoolness coolness = this.Ship.Coolness;
			if (this.{21151} == null)
			{
				this.{21151} = new Form(Marker.Zero, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				this.{21152} = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				this.{21153} = new Label(Vector2.Zero, Fonts.Philosopher_16, {13344}, this.Ship.ShipName, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				this.{21151}.AddChild(this.{21152});
				this.{21154} = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				if (this.{21141} == ShipResearchStatus.Yellow)
				{
					Vector2 zero = Vector2.Zero;
					CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
					Color {13344}2 = Color.Lerp(Color.LightYellow, Color.Yellow, 0.5f);
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler.AppendFormatted<int>(this.{21143});
					defaultInterpolatedStringHandler.AppendLiteral(" / ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(this.{21142});
					this.{21157} = new Label(zero, philosopher_, {13344}2, defaultInterpolatedStringHandler.ToStringAndClear(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				}
				if (Session.Account.Shipyard.ContainsInfo(this.Ship))
				{
					this.{21155} = new Image(new Marker(0f, 0f, 64f, 64f), OtherTextures.Images, new Rectangle(748, 976, 79, 79), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					this.{21155}.BasicColor = new Color(1f, 1f, 1f, 0.1f) * 0.15f;
				}
				if (this.Ship.IsSailageLegend)
				{
					this.{21156} = new Label(Vector2.Zero, Fonts.Philosopher_16, {13344}, Session.Account.Shipyard.ContainsInfo(this.Ship) ? "★" : "☆", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				}
				this.{21151}.AddChild(this.{21154});
				base.Form.AddChild(this.{21151});
			}
			UiControl uiControl = this.{21151};
			Marker pos = base.Form.Pos;
			uiControl.Pos = new Marker(ref pos.XY, this.{21146}.Pos.WH.X, 8f * this.scaleFactor).Offset(0f, this.{21145} * this.{21144}).Offset(0f, 11f * this.scaleFactor).Offset((base.Form.Pos.WH.X - this.{21138} * this.scaleFactor) / 2f, 0f);
			this.{21152}.Clear();
			UiControl uiControl2 = this.{21152};
			pos = this.{21151}.Pos;
			uiControl2.Pos = new Marker(ref pos.XY, 0f, 0f);
			this.{21152}.AddSpace(2f * this.scaleFactor);
			PlayerShipCoolness coolness2 = this.Ship.Coolness;
			bool flag = coolness2 - PlayerShipCoolness.Unique <= 1;
			if (flag)
			{
				Image image = this.{21150};
				if (image != null)
				{
					image.RemoveFromContainer();
				}
				this.{21152}.AddItem(new UiControl[]
				{
					this.{21150} = new Image(new Marker(0f, 0f, 12f * this.scaleFactor, 12f * this.scaleFactor), CommonAtlas.Texture.Tex, CommonAtlas.GetShipClassIcon(this.Ship), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}
			this.{21153}.ScaleOfCentr = this.scaleFactor * num;
			this.{21152}.AddItem(new UiControl[]
			{
				this.{21153}
			});
			UiControl uiControl3 = this.{21152};
			pos = this.{21152}.Pos;
			uiControl3.Pos = pos.Offset((this.{21151}.Pos.WH.X - this.{21152}.Pos.WH.X) / 2f - 2f * this.scaleFactor, 0f);
			this.{21154}.Clear();
			this.{21154}.Pos = new Marker(this.{21151}.Pos.XY.X, this.{21151}.Pos.XY.Y + 9f * this.scaleFactor, 0f, 0f);
			if (this.{21157} != null)
			{
				this.{21157}.ScaleOfCentr = this.scaleFactor * num * 0.82f;
				this.{21154}.AddItem(new UiControl[]
				{
					this.{21157}
				});
				this.{21154}.AddSpace(2f * this.scaleFactor);
			}
			if (this.{21155} != null)
			{
				this.{21155}.PosWidth = 64f * this.scaleFactor;
				this.{21155}.PosHeight = 64f * this.scaleFactor;
				UiControl uiControl4 = this.{21155};
				pos = this.{21155}.Pos;
				Vector2 vector = base.Form.Pos.Center - this.{21155}.Pos.WH * 0.5f + new Vector2(0f, 16f * this.scaleFactor);
				uiControl4.Pos = pos.SetXY(vector);
				this.{21151}.AddChild(this.{21155});
				foreach (UiControl uiControl5 in ((IEnumerable<UiControl>)this.{21155}.GetChildren))
				{
					Label label = uiControl5 as Label;
					if (label != null)
					{
						label.ScaleOfCentr = this.scaleFactor;
					}
				}
			}
			if (this.{21156} != null)
			{
				this.{21156}.ScaleOfCentr = this.scaleFactor * num;
				this.{21154}.AddItem(new UiControl[]
				{
					this.{21156}
				});
			}
			if (Debugging.DebugInfo)
			{
				string[] array = new string[]
				{
					this.Ship.ID.ToString(),
					this.Ship.Fraction.GetName()
				};
				for (int i = 0; i < array.Length; i++)
				{
					this.{21154}.AddSpace(2f * this.scaleFactor);
					this.{21154}.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Philosopher_14Bold, Color.Yellow, array[i], PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							ScaleOfCentr = this.scaleFactor * 0.4f
						}
					});
				}
			}
			UiControl uiControl6 = this.{21154};
			pos = this.{21154}.Pos;
			uiControl6.Pos = pos.Offset((this.{21151}.Pos.WH.X - this.{21154}.Pos.WH.X) / 2f, 0f);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0008B328 File Offset: 0x00089528
		private void {21124}()
		{
			if (this.{21158} == null)
			{
				this.{21158} = new Form(Marker.Zero, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					IsVisible = false
				};
				base.Form.AddChild(this.{21158});
			}
			this.{21158}.Pos = new Marker(25f, 15f, 29f, 0f).Scale(this.scaleFactor).Offset(base.Form.Pos.XY.X + base.Form.Pos.WH.X / 2f, base.Form.Pos.XY.Y);
			string {21131} = "hp";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<float>(this.Ship.PatHealth);
			this.{21130}({21131}, defaultInterpolatedStringHandler.ToStringAndClear(), {21111}.c_statIconHp, new Color(0, 131, 255));
			string {21131}2 = "armor";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler2.AppendFormatted<float>(this.Ship.PatArmor);
			this.{21130}({21131}2, defaultInterpolatedStringHandler2.ToStringAndClear(), {21111}.c_statIconArmor, new Color(216, 216, 216));
			string {21131}3 = "speed";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler3.AppendFormatted<float>(this.Ship.PatSpeed * PlayerShipInfo.Temp_displaySpeedRefactoring);
			this.{21130}({21131}3, defaultInterpolatedStringHandler3.ToStringAndClear(), {21111}.c_statIconSail, new Color(0, 246, 255));
			string {21131}4 = "mobility";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler4.AppendFormatted<float>(this.Ship.PatMobility);
			this.{21130}({21131}4, defaultInterpolatedStringHandler4.ToStringAndClear(), {21111}.c_statIconMobility, new Color(0, 255, 169));
			string {21131}5 = "cannons";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler5 = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler5.AppendFormatted<int>(this.Ship.StaticInfo.Ports.Length);
			this.{21130}({21131}5, defaultInterpolatedStringHandler5.ToStringAndClear(), {21111}.c_statIconCannon, new Color(255, 199, 0));
			string {21131}6 = "units";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler6 = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler6.AppendFormatted<int>(this.Ship.PatPlacesUnits);
			this.{21130}({21131}6, defaultInterpolatedStringHandler6.ToStringAndClear(), {21111}.c_statIconCrew, new Color(214, 183, 130));
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0008B588 File Offset: 0x00089788
		private void {21125}(UiControl {21126})
		{
			if (EducationHelper.AvailableResToSellFlickering)
			{
				PlayerShipInfo ship = this.Ship;
				Decorator game = Session.Game;
				ship.GetCraftPrice(game, Session.EventActionsPipeline);
				if (this.Ship.Coolness == PlayerShipCoolness.Default && Session.Account.IsEducationInProgress(EducationOnboarding.BuildNextShip, false, false) && this.Ship.ID == 6)
				{
					base.Form.Brightness = 1f + 0.5f * (float)Math.Sin(Global.Game.GameTotalTimeSec * 4.0);
				}
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0008B611 File Offset: 0x00089811
		protected override void DoScale(float {21127})
		{
			this.Recompose();
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0008B6F2 File Offset: 0x000898F2
		[CompilerGenerated]
		private void {21128}(ClickUiEventArgs {21129})
		{
			this.{21139} = true;
			this.{21140} = 0f;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0008B708 File Offset: 0x00089908
		[CompilerGenerated]
		private void {21130}(string {21131}, string {21132}, Rectangle {21133}, Color {21134})
		{
			Marker marker = new Marker(0f, 0f, 29f, 9f).ScaleSize(this.scaleFactor);
			Marker marker2 = this.{21158}.Pos;
			Marker marker3 = marker.Offset(marker2.XY).Offset(0f, this.{21158}.Pos.WH.Y);
			{21111}.StatFormData value;
			if (!this.{21159}.TryGetValue({21131}, out value))
			{
				value = new {21111}.StatFormData
				{
					Form = new Form(Marker.Zero, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						TexturePath = CommonAtlas.whitePixel,
						BasicColor = Color.White * 0.5f
					},
					Back = new Image(Marker.Zero, AtlasPortGui.Texture.Tex, {21111}.c_statsBack, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						BasicColor = Color.White * 0.75f
					},
					Icon = new Image(Marker.Zero, AtlasPortGui.Texture.Tex, {21133}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						BasicColor = {21134}
					},
					Text = new Label(Vector2.Zero, Fonts.Philosopher_12, {21134}, {21132}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				};
				value.Form.AddChild(value.Back);
				value.Form.AddChild(value.Icon);
				value.Form.AddChild(value.Text);
				this.{21159}.Add({21131}, value);
				this.{21158}.AddChild(value.Form);
			}
			value.Form.Pos = marker3;
			value.Back.Pos = marker3;
			UiControl icon = value.Icon;
			marker2 = new Marker(ref marker3.XY, 6f, 6f);
			marker2 = marker2.ScaleSize(this.scaleFactor);
			icon.Pos = marker2.Offset(1.5f * this.scaleFactor, 1.5f * this.scaleFactor);
			value.Text.ScaleOfCentr = this.scaleFactor * 0.45f;
			UiControl text = value.Text;
			marker2 = value.Text.Pos;
			Vector2 vector = marker3.End - value.Text.Pos.WH;
			marker2 = marker2.SetXY(vector);
			text.Pos = marker2.Offset(-1.5f * this.scaleFactor, 1f * this.scaleFactor);
			UiControl uiControl = this.{21158};
			marker2 = this.{21158}.Pos;
			uiControl.Pos = marker2.SetHeight(this.{21158}.Pos.WH.Y + marker3.WH.Y + 0.5f * this.scaleFactor);
		}

		// Token: 0x04000F0E RID: 3854
		private static readonly Rectangle c_statsBack = new Rectangle(2253, 1368, 237, 106);

		// Token: 0x04000F0F RID: 3855
		private static readonly Rectangle c_statIconHp = new Rectangle(983, 594, 21, 21);

		// Token: 0x04000F10 RID: 3856
		private static readonly Rectangle c_statIconArmor = new Rectangle(1004, 594, 21, 21);

		// Token: 0x04000F11 RID: 3857
		private static readonly Rectangle c_statIconSail = new Rectangle(1025, 594, 21, 21);

		// Token: 0x04000F12 RID: 3858
		private static readonly Rectangle c_statIconMobility = new Rectangle(1046, 594, 21, 21);

		// Token: 0x04000F13 RID: 3859
		private static readonly Rectangle c_statIconCannon = new Rectangle(1067, 594, 21, 21);

		// Token: 0x04000F14 RID: 3860
		private static readonly Rectangle c_statIconCrew = new Rectangle(1088, 594, 21, 21);

		// Token: 0x04000F15 RID: 3861
		private static readonly Rectangle c_eliteFrame = new Rectangle(1718, 678, 271, 203);

		// Token: 0x04000F16 RID: 3862
		[CompilerGenerated]
		private readonly PlayerShipInfo {21135};

		// Token: 0x04000F17 RID: 3863
		[CompilerGenerated]
		private bool {21136};

		// Token: 0x04000F18 RID: 3864
		private readonly {21078} {21137};

		// Token: 0x04000F19 RID: 3865
		private readonly float {21138};

		// Token: 0x04000F1A RID: 3866
		private bool {21139};

		// Token: 0x04000F1B RID: 3867
		private float {21140};

		// Token: 0x04000F1C RID: 3868
		private ShipResearchStatus {21141} = ShipResearchStatus.Green;

		// Token: 0x04000F1D RID: 3869
		private int {21142};

		// Token: 0x04000F1E RID: 3870
		private int {21143};

		// Token: 0x04000F1F RID: 3871
		private float {21144};

		// Token: 0x04000F20 RID: 3872
		private float {21145};

		// Token: 0x04000F21 RID: 3873
		private Form {21146};

		// Token: 0x04000F22 RID: 3874
		private Image {21147};

		// Token: 0x04000F23 RID: 3875
		private Image {21148};

		// Token: 0x04000F24 RID: 3876
		private Image {21149};

		// Token: 0x04000F25 RID: 3877
		private Image {21150};

		// Token: 0x04000F26 RID: 3878
		private Form {21151};

		// Token: 0x04000F27 RID: 3879
		private StackForm {21152};

		// Token: 0x04000F28 RID: 3880
		private Label {21153};

		// Token: 0x04000F29 RID: 3881
		private StackForm {21154};

		// Token: 0x04000F2A RID: 3882
		private UiControl {21155};

		// Token: 0x04000F2B RID: 3883
		private Label {21156};

		// Token: 0x04000F2C RID: 3884
		private Label {21157};

		// Token: 0x04000F2D RID: 3885
		private Form {21158};

		// Token: 0x04000F2E RID: 3886
		private readonly Dictionary<string, {21111}.StatFormData> {21159} = new Dictionary<string, {21111}.StatFormData>();

		// Token: 0x020002F2 RID: 754
		private readonly struct StatFormData
		{
			// Token: 0x17000159 RID: 345
			// (get) Token: 0x0600108A RID: 4234 RVA: 0x0008B9CA File Offset: 0x00089BCA
			// (set) Token: 0x0600108B RID: 4235 RVA: 0x0008B9D2 File Offset: 0x00089BD2
			public Form Form { get; set; }

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x0600108C RID: 4236 RVA: 0x0008B9DB File Offset: 0x00089BDB
			// (set) Token: 0x0600108D RID: 4237 RVA: 0x0008B9E3 File Offset: 0x00089BE3
			public Image Back { get; set; }

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x0600108E RID: 4238 RVA: 0x0008B9EC File Offset: 0x00089BEC
			// (set) Token: 0x0600108F RID: 4239 RVA: 0x0008B9F4 File Offset: 0x00089BF4
			public Image Icon { get; set; }

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x06001090 RID: 4240 RVA: 0x0008B9FD File Offset: 0x00089BFD
			// (set) Token: 0x06001091 RID: 4241 RVA: 0x0008BA05 File Offset: 0x00089C05
			public Label Text { get; set; }

			// Token: 0x04000F2F RID: 3887
			[CompilerGenerated]
			private readonly Form {21164};

			// Token: 0x04000F30 RID: 3888
			[CompilerGenerated]
			private readonly Image {21165};

			// Token: 0x04000F31 RID: 3889
			[CompilerGenerated]
			private readonly Image {21166};

			// Token: 0x04000F32 RID: 3890
			[CompilerGenerated]
			private readonly Label {21167};
		}
	}
}
