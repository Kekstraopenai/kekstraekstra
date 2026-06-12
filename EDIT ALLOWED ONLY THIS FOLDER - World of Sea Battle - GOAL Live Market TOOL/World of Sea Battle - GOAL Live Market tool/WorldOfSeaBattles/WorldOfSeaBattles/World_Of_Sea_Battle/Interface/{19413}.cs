using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001E7 RID: 487
	internal sealed class {19413} : {17625}
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0005669B File Offset: 0x0005489B
		public static bool IsOpen
		{
			get
			{
				return {19413}.Instance != null;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x000566A5 File Offset: 0x000548A5
		private static int width
		{
			get
			{
				return 605;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x000566AC File Offset: 0x000548AC
		private static float leftOffset
		{
			get
			{
				return 45f;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x000566B3 File Offset: 0x000548B3
		private static float cLeftPos
		{
			get
			{
				return 340f;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x000566BA File Offset: 0x000548BA
		private static float itemHeight
		{
			get
			{
				return 33f;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x000566BA File Offset: 0x000548BA
		private static float itemHeightKeys
		{
			get
			{
				return 33f;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x000566C1 File Offset: 0x000548C1
		private static CustomSpriteFont dropdownFont
		{
			get
			{
				return Fonts.Philosopher_14;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x000566C8 File Offset: 0x000548C8
		private static Vector2 getPosition
		{
			get
			{
				return new Vector2((float)(Engine.GS.UIArea.Width / 2 - {19413}.width / 2), (float)Math.Max(0, Engine.GS.UIArea.Height / 2 - 384 - 50));
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00056718 File Offset: 0x00054918
		public {19413}() : base((float){19413}.width, new Rectangle(0, 0, {19413}.width, 730), new {17604}
		{
			BackgroundIsTransparent = true
		}, Rectangle.Empty, new {17625}.DynamicTittle[]
		{
			Local.GameSettingsWindow_0,
			Local.GameSettingsWindow_3,
			Local.GameSettingsWindow_1,
			Local.GameSettingsWindow_2
		})
		{
			Marker pos = base.Pos;
			Vector2 getPosition = {19413}.getPosition;
			base.Pos = pos.SetXY(getPosition);
			this.backPath = CommonAtlas.transpPixel;
			{19413}.Instance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{19413}.Instance = null;
			};
			base.ComposeTabWithScroll(null, false, true, new Action<ListItemViewControl>[]
			{
				new Action<ListItemViewControl>(this.{19457}),
				new Action<ListItemViewControl>(this.{19459}),
				new Action<ListItemViewControl>(this.{19461}),
				new Action<ListItemViewControl>(this.{19463})
			});
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0005683C File Offset: 0x00054A3C
		private Form {19414}(int {19415})
		{
			StackForm stackForm = new StackForm(base.Pos.XY, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			switch ({19415})
			{
			case 1:
				this.{19416}(stackForm);
				break;
			case 2:
				this.{19426}(stackForm);
				break;
			case 3:
				this.{19418}(stackForm);
				break;
			case 4:
				this.{19420}(stackForm);
				break;
			default:
				throw new NotSupportedException();
			}
			Form form = new Form(stackForm.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			foreach (UiControl {13204} in stackForm.GetChildren.Clone().Reverse<UiControl>())
			{
				form.AddChild({13204});
			}
			stackForm.RemoveFromContainer();
			return form;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00056908 File Offset: 0x00054B08
		private void {19416}(StackForm {19417})
		{
			this.{19428}({19417}, Local.game_settings_sounds);
			this.AddSlider({19417}, Local.GameSettingsWindow_4, MathF.Sqrt(Global.Settings.SoundVolume), delegate(float {19475})
			{
				Global.Settings.SoundVolume = {19475} * {19475};
			});
			this.AddSlider({19417}, Local.GameSettingsWindow_5, MathF.Sqrt(Global.Settings.AmbientVolume), delegate(float {19476})
			{
				Global.Settings.AmbientVolume = {19476} * {19476};
			});
			this.AddSlider({19417}, Local.GameSettingsWindow_6, MathF.Sqrt(Global.Settings.MusicVolume), delegate(float {19477})
			{
				Global.Settings.MusicVolume = {19477} * {19477};
			});
			this.AddToollist<LocalSettings.ShipVoices>({19417}, Local.GameSettingsWindow_7, Global.Settings.VoicesMode, delegate(LocalSettings.ShipVoices {19478})
			{
				Global.Settings.VoicesMode = {19478};
			}, new SelItem<LocalSettings.ShipVoices>[]
			{
				new SelItem<LocalSettings.ShipVoices>(LocalSettings.ShipVoices.Disabled, Local.GameSettingsWindow_n1),
				new SelItem<LocalSettings.ShipVoices>(LocalSettings.ShipVoices.BackgroundOnly, Local.GameSettingsWindow_n2),
				new SelItem<LocalSettings.ShipVoices>(LocalSettings.ShipVoices.Young, Local.GameSettingsWindow_n3),
				new SelItem<LocalSettings.ShipVoices>(LocalSettings.ShipVoices.Old, Local.GameSettingsWindow_n4)
			});
			if (Global.Game.GetCurrentSceneName != GameSceneName.Entry && Session.Account != null)
			{
				string authCode = CommonSupport.GetAuthCode(Session.Account.SID, Session.Account.Login);
				this.{19431}({19417}, Local.game_settings_acc, authCode);
				{19417}.AddSpace(5f);
				StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm.AddSpace(26f);
				if (!PlatformTuning.DisableShop)
				{
					StackForm stackForm2 = stackForm;
					UiControl[] array = new UiControl[1];
					array[0] = new Button(Vector2.Zero, {17625}.c_btGray_mid, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.name + " ✎", Fonts.Philosopher_14, Color.White * 0.9f, false).ExClick(delegate(ClickUiEventArgs {19479})
					{
						if (Session.Account.HasFreeNextChangeNickname)
						{
							new {17312}(delegate(string {19480})
							{
								if (string.Equals({19480}, Session.Account.PlayerName))
								{
									new {17312}(Local.EscapeInterface_16);
									return;
								}
								Global.Network.Send(new OnChangeAccountPeropertyMsg(AccountProtectionProperty.Name, {19480}));
							}, Gcc.NameLengthLimits.Item2, Local.free_name_change + Environment.NewLine + Local.EscapeInterface_18, null, delegate(TextBox {19481})
							{
								TextBox.Moderator {13698};
								if (({13698} = {19413}.<>O.<0>__ModerateNickname) == null)
								{
									{13698} = ({19413}.<>O.<0>__ModerateNickname = new TextBox.Moderator(Gcc.ModerateNickname));
								}
								{19481}.AttachModerator({13698}, Color.OrangeRed);
							});
							return;
						}
						new {17312}(delegate(string {19482})
						{
							if (Session.Account.Monets.Value < Gameplay.ChangeNicknameCost.Value)
							{
								new {17312}(Local.CaptainSkillsInfoWindow_8);
								return;
							}
							if (string.Equals({19482}, Session.Account.PlayerName))
							{
								new {17312}(Local.EscapeInterface_16);
								return;
							}
							Global.Network.Send(new OnChangeAccountPeropertyMsg(AccountProtectionProperty.Name, {19482}));
						}, Gcc.NameLengthLimits.Item2, string.Concat(new string[]
						{
							Local.EscapeInterface_17,
							Gameplay.ChangeNicknameCost.ToString(),
							Local.CaptainSkillsInfoWindow_6,
							Environment.NewLine,
							Local.EscapeInterface_18
						}), null, delegate(TextBox {19483})
						{
							TextBox.Moderator {13698};
							if (({13698} = {19413}.<>O.<0>__ModerateNickname) == null)
							{
								{13698} = ({19413}.<>O.<0>__ModerateNickname = new TextBox.Moderator(Gcc.ModerateNickname));
							}
							{19483}.AttachModerator({13698}, Color.OrangeRed);
						});
					});
					stackForm2.AddItem(array);
				}
				StackForm stackForm3 = stackForm;
				UiControl[] array2 = new UiControl[1];
				array2[0] = new Button(Vector2.Zero, {17625}.c_btGray_mid, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.EntryScene_1_b + " ✎", Fonts.Philosopher_14, Color.White * 0.9f, false).ExClick(delegate(ClickUiEventArgs {19484})
				{
					new {17312}(delegate(string {19485})
					{
						Global.Network.Send(new OnChangeAccountPeropertyMsg(AccountProtectionProperty.EMail, {19485}));
					}, Gcc.MaxEmailLength, Local.EscapeInterface_21 + (string.IsNullOrEmpty(Session.Account.EMail) ? "-" : Session.Account.EMail), null, delegate(TextBox {19486})
					{
						TextBox.Moderator {13698};
						if (({13698} = {19413}.<>O.<1>__ModerateLoginEmail) == null)
						{
							{13698} = ({19413}.<>O.<1>__ModerateLoginEmail = new TextBox.Moderator(Gcc.ModerateLoginEmail));
						}
						{19486}.AttachModerator({13698}, Color.OrangeRed);
					});
				});
				stackForm3.AddItem(array2);
				if (!PlatformTuning.ExternalLoginAPI)
				{
					StackForm stackForm4 = stackForm;
					UiControl[] array3 = new UiControl[1];
					array3[0] = new Button(Vector2.Zero, {17625}.c_btGray_mid, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.EntryScene_2 + " ✎", Fonts.Philosopher_14, Color.White * 0.9f, false).ExClick(delegate(ClickUiEventArgs {19487})
					{
						{19413}.OpenChangePasswordDialog(false);
					});
					stackForm4.AddItem(array3);
				}
				stackForm.AddItem(new UiControl[]
				{
					new Button(Vector2.Zero, {17625}.c_btGray_mid_long, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.delete_account, Fonts.Philosopher_14, Color.White * 0.9f, false).ExClick(new Action<ClickUiEventArgs>(this.{19465}))
				});
				{19417}.AddItem(new UiControl[]
				{
					stackForm
				});
			}
			this.{19428}({19417}, Local.game_settings_game);
			IEnumerable<Locale> source = from {19489} in Enum.GetValues<Locale>()
			where !new LocaleInfo({19489}).IsDevLocale
			select {19489};
			this.AddToollist<Locale>({19417}, Local.GameSettingsWindow_Lang, LocaleInfo.Current.Id, delegate(Locale {19490})
			{
				LanguageSettings.SetUserLocale({19490}, false);
				new {17312}({19490}.GetInfo().Item3);
			}, (from {19491} in source
			select new SelItem<Locale>({19491}, {19491}.GetInfo().Item1)).ToArray<SelItem<Locale>>());
			this.AddToollist<LocalSettings.SailTransparancyMode>({19417}, Local.GameSettingsWindow_18, Global.Settings.SailTransparancy, delegate(LocalSettings.SailTransparancyMode {19492})
			{
				Global.Settings.SailTransparancy = {19492};
			}, new SelItem<LocalSettings.SailTransparancyMode>[]
			{
				new SelItem<LocalSettings.SailTransparancyMode>(LocalSettings.SailTransparancyMode.InBattle, Local.GameSettingsWindow_21b),
				new SelItem<LocalSettings.SailTransparancyMode>(LocalSettings.SailTransparancyMode.WhenZoom, Local.GameSettingsWindow_19),
				new SelItem<LocalSettings.SailTransparancyMode>(LocalSettings.SailTransparancyMode.Always, Local.GameSettingsWindow_20),
				new SelItem<LocalSettings.SailTransparancyMode>(LocalSettings.SailTransparancyMode.NoUse, Local.GameSettingsWindow_21)
			});
			this.AddCheckbox({19417}, Local.GameSettingsWindow_sail_ht, "", Global.Settings.HalfTransparentSailDesign, delegate(bool {19493})
			{
				Global.Settings.HalfTransparentSailDesign = {19493};
			}).ExToolTip(new ToolTip(new ToolTipState("", Local.GameSettingsWindow_sail_ht_tt, Array.Empty<ToolTipCharacteristics>())));
			this.AddCheckbox({19417}, Local.GameSettingsWindow_39, Local.GameSettingsWindow_40, Global.Settings.ForceCameraEffect, delegate(bool {19494})
			{
				Global.Settings.ForceCameraEffect = {19494};
			});
			this.AddCheckbox({19417}, Local.GameSettingsWindow_106, "", Global.Settings.HorizonTilt, delegate(bool {19495})
			{
				Global.Settings.HorizonTilt = {19495};
			});
			this.AddCheckbox({19417}, Local.GameSettingsWindow_107, "", Global.Settings.SpyglassAnimation, delegate(bool {19496})
			{
				Global.Settings.SpyglassAnimation = {19496};
			});
			this.AddToollist<int>({19417}, Local.GameSettingsWindow_14, Global.Settings.WindArrowMode, delegate(int {19497})
			{
				Global.Settings.WindArrowMode = {19497};
			}, new SelItem<int>[]
			{
				new SelItem<int>(0, Local.GameSettingsWindow_15),
				new SelItem<int>(1, Local.GameSettingsWindow_16),
				new SelItem<int>(2, Local.GameSettingsWindow_17)
			});
			this.AddToollist<TimeFormat>({19417}, Local.GameSettingsWindow_timeFormat, Global.Settings.PreferredTime, delegate(TimeFormat {19498})
			{
				Global.Settings.PreferredTime = {19498};
			}, new SelItem<TimeFormat>[]
			{
				new SelItem<TimeFormat>(TimeFormat.PreferServerTime, Local.GameSettingsWindow_timeFormat_server(LocalizedDateTime.ServerTimezonePrefix)),
				new SelItem<TimeFormat>(TimeFormat.PreferLocalTime, Local.GameSettingsWindow_timeFormat_local((TimeZoneInfo.Local.DisplayName.Length > 13) ? (TimeZoneInfo.Local.DisplayName.Substring(0, 13) + "...") : TimeZoneInfo.Local.DisplayName))
			}).ExToolTip(new ToolTip(new ToolTipState("", Local.GameSettingsWindow_timeFormat_tt, Array.Empty<ToolTipCharacteristics>())));
			this.AddCheckbox({19417}, Local.GameSettingsWindow_109, "", Global.Settings.EnableGameTooltips, delegate(bool {19499})
			{
				Global.Settings.EnableGameTooltips = {19499};
			});
			this.AddCheckbox({19417}, Local.GameSettingsWindow_brightNight, "", Global.Settings.BrightNight, delegate(bool {19500})
			{
				Global.Settings.BrightNight = {19500};
			});
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00057008 File Offset: 0x00055208
		private void {19418}(StackForm {19419})
		{
			this.{19428}({19419}, Local.game_settings_window);
			Action<int> setCommon = null;
			this.AddCheckbox({19419}, Local.GameSettingsWindow_32, Local.GameSettingsWindow_33, Global.Settings.FullscreenEnabled, delegate(bool {19501})
			{
				Global.Settings.FullscreenEnabled = {19501};
			});
			this.AddSlider({19419}, Local.GameSettingsWindow_34, Global.Settings.GammaSetting + 0.5f, delegate(float {19502})
			{
				Global.Settings.GammaSetting = {19502} - 0.5f;
			});
			this.AddCheckbox({19419}, Local.GameSettingsWindow_37, Local.GameSettingsWindow_38, Global.Settings.VerticalSyncEnabled, delegate(bool {19503})
			{
				Global.Settings.VerticalSyncEnabled = {19503};
			});
			this.AddCheckbox({19419}, Local.GameSettingsWindow_58b, Local.GameSettingsWindow_59b, Global.Settings.LightWater, delegate(bool {19504})
			{
				Global.Settings.LightWater = {19504};
			});
			this.AddCheckbox({19419}, Local.GameSettingsWindow_58c, Local.GameSettingsWindow_59b, Global.Settings.SharpWater, delegate(bool {19505})
			{
				Global.Settings.SharpWater = {19505};
			});
			this.AddCheckbox({19419}, Local.GameSettingsWindow_58c + " v2", "", Global.Settings.ProgressiveFresnel, delegate(bool {19506})
			{
				Global.Settings.ProgressiveFresnel = {19506};
			});
			this.AddCheckbox({19419}, Local.GameSettingsWindow_freq, Local.GameSettingsWindow_freq_tt, Global.Settings.MonitorFrequency == LocalSettings.DrawFrequency.fps144, delegate(bool {19507})
			{
				Global.Settings.MonitorFrequency = ({19507} ? LocalSettings.DrawFrequency.fps144 : LocalSettings.DrawFrequency.fps60);
			});
			this.{19428}({19419}, Local.game_settings_quality);
			this.AddToollist<int>({19419}, Local.game_settings_select_level, -1, delegate(int {19534})
			{
				if ({19534} != -1)
				{
					setCommon({19534});
				}
			}, new SelItem<int>[]
			{
				new SelItem<int>(-1, Local.GameSettingsWindow_42),
				new SelItem<int>(0, Local.GameSettingsWindow_43),
				new SelItem<int>(1, Local.GameSettingsWindow_44),
				new SelItem<int>(2, Local.GameSettingsWindow_45),
				new SelItem<int>(3, Local.GameSettingsWindow_46)
			});
			DropdownControl<LocalSettings.CrewDisplayMode> a10 = this.AddToollist<LocalSettings.CrewDisplayMode>({19419}, Local.crew, Global.Settings.CrewMode, delegate(LocalSettings.CrewDisplayMode {19508})
			{
				Global.Settings.CrewMode = {19508};
			}, new SelItem<LocalSettings.CrewDisplayMode>[]
			{
				new SelItem<LocalSettings.CrewDisplayMode>(LocalSettings.CrewDisplayMode.None, Local.GameSettingsWindow_47),
				new SelItem<LocalSettings.CrewDisplayMode>(LocalSettings.CrewDisplayMode.OnlyMyShip, Local.GameSettingsWindow_48),
				new SelItem<LocalSettings.CrewDisplayMode>(LocalSettings.CrewDisplayMode.AllShips, Local.GameSettingsWindow_49)
			});
			CheckboxControl a0 = this.AddCheckbox({19419}, Local.GameSettingsWindow_50, Local.GameSettingsWindow_51, Global.Settings.EnableBasicEffects, delegate(bool {19509})
			{
				Global.Settings.EnableBasicEffects = {19509};
			});
			CheckboxControl a2 = this.AddCheckbox({19419}, Local.GameSettingsWindow_54, Local.GameSettingsWindow_55, Global.Settings.HighDetailing, delegate(bool {19510})
			{
				Global.Settings.HighDetailing = {19510};
			});
			DropdownControl<LocalSettings.FloraQualityMode> a12 = this.AddToollist<LocalSettings.FloraQualityMode>({19419}, Local.GameSettingsWindow_floraq, Global.Settings.FloraQuality, delegate(LocalSettings.FloraQualityMode {19511})
			{
				Global.Settings.FloraQuality = {19511};
			}, new SelItem<LocalSettings.FloraQualityMode>[]
			{
				new SelItem<LocalSettings.FloraQualityMode>(LocalSettings.FloraQualityMode.Low, Local.GameSettingsWindow_43),
				new SelItem<LocalSettings.FloraQualityMode>(LocalSettings.FloraQualityMode.Normal, Local.GameSettingsWindow_44),
				new SelItem<LocalSettings.FloraQualityMode>(LocalSettings.FloraQualityMode.High, Local.GameSettingsWindow_45)
			}).ExToolTip(new ToolTip(new ToolTipState("", Local.GameSettingsWindow_floraq_tt, Array.Empty<ToolTipCharacteristics>())));
			CheckboxControl a3 = this.AddCheckbox({19419}, Local.GameSettingsWindow_56, Local.GameSettingsWindow_57, Global.Settings.RendererSsaoAndRefractions, delegate(bool {19512})
			{
				Global.Settings.RendererSsaoAndRefractions = {19512};
			});
			CheckboxControl a5 = this.AddCheckbox({19419}, Local.GameSettingsWindow_60, Local.GameSettingsWindow_61, Global.Settings.RendererDynamicReflections, delegate(bool {19513})
			{
				Global.Settings.RendererDynamicReflections = {19513};
			});
			DropdownControl<LocalSettings.RendererShadows> t6 = this.AddToollist<LocalSettings.RendererShadows>({19419}, Local.GameSettingsWindow_62, Global.Settings.ShadowsQuality, delegate(LocalSettings.RendererShadows {19514})
			{
				Global.Settings.ShadowsQuality = {19514};
			}, new SelItem<LocalSettings.RendererShadows>[]
			{
				new SelItem<LocalSettings.RendererShadows>(LocalSettings.RendererShadows.Off, Local.GameSettingsWindow_63),
				new SelItem<LocalSettings.RendererShadows>(LocalSettings.RendererShadows.Medium, Local.GameSettingsWindow_64),
				new SelItem<LocalSettings.RendererShadows>(LocalSettings.RendererShadows.High, Local.GameSettingsWindow_65)
			});
			t6.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(null, Local.GameSettingsWindow_66, Array.Empty<ToolTipCharacteristics>()));
			DropdownControl<LocalSettings.RendererAntialias> t7 = this.AddToollist<LocalSettings.RendererAntialias>({19419}, Local.GameSettingsWindow_67, Global.Settings.AntialiasingQuality, delegate(LocalSettings.RendererAntialias {19515})
			{
				Global.Settings.AntialiasingQuality = {19515};
			}, new SelItem<LocalSettings.RendererAntialias>[]
			{
				new SelItem<LocalSettings.RendererAntialias>(LocalSettings.RendererAntialias.Off, Local.GameSettingsWindow_68),
				new SelItem<LocalSettings.RendererAntialias>(LocalSettings.RendererAntialias.Fsaa, "SSAA / MSAA"),
				new SelItem<LocalSettings.RendererAntialias>(LocalSettings.RendererAntialias.Txaa, "TXAA")
			});
			t7.ExToolTip(new ToolTip(new ToolTipState("", string.Concat(new string[]
			{
				Local.GameSettingsWindow_69,
				Environment.NewLine,
				Local.GameSettingsWindow_70,
				Environment.NewLine,
				Local.GameSettingsWindow_71,
				Environment.NewLine,
				Environment.NewLine,
				Local.GameSettingsWindow_72
			}), Array.Empty<ToolTipCharacteristics>())));
			DropdownControl<LocalSettings.Postprocessor> a11 = this.AddToollist<LocalSettings.Postprocessor>({19419}, Local.GameSettingsWindow_35, Global.Settings.PostprocessorType, delegate(LocalSettings.Postprocessor {19516})
			{
				Global.Settings.PostprocessorType = {19516};
			}, new SelItem<LocalSettings.Postprocessor>[]
			{
				new SelItem<LocalSettings.Postprocessor>(LocalSettings.Postprocessor.Basic, Local.GameSettingsWindow_post1),
				new SelItem<LocalSettings.Postprocessor>(LocalSettings.Postprocessor.Tonemap1, Local.GameSettingsWindow_post2),
				new SelItem<LocalSettings.Postprocessor>(LocalSettings.Postprocessor.Tonemap2AndHdr, Local.GameSettingsWindow_post3)
			}).ExToolTip(new ToolTip(new ToolTipState("", Local.GameSettingsWindow_36, Array.Empty<ToolTipCharacteristics>())));
			CheckboxControl a8 = this.AddCheckbox({19419}, Local.GameSettingsWindow_73, Local.GameSettingsWindow_74, Global.Settings.ImprovedPostProcess, delegate(bool {19517})
			{
				Global.Settings.ImprovedPostProcess = {19517};
			});
			CheckboxControl a9 = this.AddCheckbox({19419}, Local.GameSettingsWindow_75, Local.GameSettingsWindow_76, Global.Settings.HeatDeformations, delegate(bool {19518})
			{
				Global.Settings.HeatDeformations = {19518};
			});
			CheckboxControl a4 = this.AddCheckbox({19419}, Local.GameSettingsWindow_vd1, Local.GameSettingsWindow_vd2, Global.Settings.LongViewDistance, delegate(bool {19519})
			{
				Global.Settings.LongViewDistance = {19519};
			});
			a9.UpdateComplete += delegate(UiControl {19535})
			{
				{19535}.Opacity = (a3.IsChecked ? 1f : 0.4f);
			};
			setCommon = delegate(int {19536})
			{
				LocalSettings.GraphicsOptions graphicsOptions;
				switch ({19536})
				{
				case 0:
					graphicsOptions = LocalSettings.GraphicsOptions.Minimal;
					break;
				case 1:
					graphicsOptions = LocalSettings.GraphicsOptions.Low;
					break;
				case 2:
					graphicsOptions = LocalSettings.GraphicsOptions.Middle;
					break;
				case 3:
					graphicsOptions = LocalSettings.GraphicsOptions.High;
					break;
				default:
					throw new NotSupportedException();
				}
				a0.IsChecked = graphicsOptions.BasicEffects;
				a2.IsChecked = graphicsOptions.ImprovedMaterials;
				a3.IsChecked = graphicsOptions.RendererSsaoAndRefractions;
				a4.IsChecked = graphicsOptions.longViewDistance;
				a5.IsChecked = graphicsOptions.RendererDynamicReflections;
				t6.SelectByValue(graphicsOptions.ShadowsQuality);
				t7.SelectByValue(graphicsOptions.AntialiasingQuality);
				a8.IsChecked = graphicsOptions.ImprovedPostProcess;
				a9.IsChecked = graphicsOptions.HeatDeformations;
				a10.SelectByValue(graphicsOptions.CrewMode);
				a11.SelectByValue(graphicsOptions.PostprocessorType);
				a12.SelectByValue(graphicsOptions.FloraQuality);
			};
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00057720 File Offset: 0x00055920
		private void {19420}(StackForm {19421})
		{
			this.AddCheckbox({19421}, Local.GameSettingsWindow_Azerty, "", Global.Settings.ControlLayout == ControlLayout.Azerty, new Action<bool>(this.{19469}));
			this.AddCheckbox({19421}, Local.GameSettingsWindow_InvertMouseControlX, "", Global.Settings.MouseControlInversion.X == -1, new Action<bool>(this.{19471}));
			this.AddCheckbox({19421}, Local.GameSettingsWindow_InvertMouseControlY, "", Global.Settings.MouseControlInversion.Y == -1, new Action<bool>(this.{19473}));
			this.AddSlider({19421}, Local.GameSettingsWindow_22, Global.Settings.MouseSensetivity - 0.5f, delegate(float {19520})
			{
				Global.Settings.MouseSensetivity = {19520} + 0.5f;
			});
			this.{19428}({19421}, Local.ship);
			this.{19451}({19421}, Local.to_forw, Global.Settings.kb_ds_Forward);
			this.{19451}({19421}, Local.to_back, Global.Settings.kb_ds_Backward);
			this.{19451}({19421}, Local.GameSettingsWindow_79, Global.Settings.kb_ds_Left);
			this.{19451}({19421}, Local.GameSettingsWindow_80, Global.Settings.kb_ds_Right);
			this.{19451}({19421}, Local.GameSettingsWindow_81, Global.Settings.kb_Mending);
			this.{19451}({19421}, Local.GameSettingsWindow_85, Global.Settings.kb_Spyglass);
			this.AddToollist<RightMouseKeyAction>({19421}, Local.GameSettingsWindow_87, Global.Settings.RightMouseAction, delegate(RightMouseKeyAction {19521})
			{
				Global.Settings.RightMouseAction = {19521};
			}, new SelItem<RightMouseKeyAction>[]
			{
				new SelItem<RightMouseKeyAction>(RightMouseKeyAction.AddDispersion, Local.ResourceOrItemToolTipHelper_23),
				new SelItem<RightMouseKeyAction>(RightMouseKeyAction.SingleCannonGun, Local.GameSettingsWindow_88),
				new SelItem<RightMouseKeyAction>(RightMouseKeyAction.PowderKegThrow, Local.GameSettingsWindow_84),
				new SelItem<RightMouseKeyAction>(RightMouseKeyAction.FalkonetGun, Local.GameSettingsWindow_82)
			});
			this.{19451}({19421}, Local.GameSettingsWindow_91, Global.Settings.kb_Action);
			this.{19428}({19421}, Local.equipment2);
			this.{19451}({19421}, Local.cball_1_name, Global.Settings.kb_SimpleShot);
			this.{19451}({19421}, Local.cball_2_name, Global.Settings.kb_FireShot);
			this.{19451}({19421}, Local.cball_3_name, Global.Settings.kb_AntisailShot);
			this.{19451}({19421}, Local.cball_4_name, Global.Settings.kb_AnticrewShot);
			this.{19451}({19421}, Local.GameSettingsWindow_82, Global.Settings.kb_Falkonet);
			this.{19451}({19421}, Local.GameSettingsWindow_97, Global.Settings.kb_SwithPanelFalkonet);
			this.{19451}({19421}, Local.GameSettingsWindow_84, Global.Settings.kb_ThrowPowderKeg);
			this.{19451}({19421}, Local.GameSettingsWindow_96, Global.Settings.kb_SwithPanelPowderKeg);
			this.{19451}({19421}, Local.GameSettingsWindow_83, Global.Settings.kb_Mortar_ModifierKey);
			this.{19451}({19421}, Local.GameSettingsWindow_86, Global.Settings.kb_SwitchCascadeGunMode);
			this.{19451}({19421}, Local.GameSettingsWindow_92, Global.Settings.kb_Item1);
			this.{19451}({19421}, Local.GameSettingsWindow_93, Global.Settings.kb_Item2);
			this.{19451}({19421}, Local.GameSettingsWindow_94, Global.Settings.kb_Item3);
			this.{19451}({19421}, Local.GameSettingsWindow_95, Global.Settings.kb_ItemExtra);
			this.{19451}({19421}, Local.GameSettingsWindow_ShipPerk, Global.Settings.kb_ShipPerk);
			this.{19428}({19421}, Local.GameSettingsWindow_3);
			this.{19451}({19421}, Local.show_mouse, Global.Settings.kb_KeyShowMouse);
			this.{19451}({19421}, Local.world_map, Global.Settings.kb_Map);
			this.{19451}({19421}, Local.hold, Global.Settings.kb_OpenHold);
			this.{19451}({19421}, Local.GameSettingsWindow_98, Global.Settings.kb_SendGroupCommand);
			this.{19451}({19421}, Local.GameSettingsWindow_104, Global.Settings.kb_OpenQuestPanel);
			this.{19451}({19421}, Local.logbook, Global.Settings.kb_OpenLogbook);
			this.{19451}({19421}, Local.GameSettingsWindow_100, Global.Settings.kb_FreeMode);
			this.{19451}({19421}, Local.GameSettingsWindow_101, Global.Settings.kb_ChatHide);
			this.{19451}({19421}, Local.GameSettingsWindow_102, Global.Settings.kb_ChatEnter);
			this.{19451}({19421}, Local.GameSettingsWindow_103, Global.Settings.kb_Screenshoot);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00057B50 File Offset: 0x00055D50
		private void {19422}(UiControl {19423}, Keys {19424}, KeyInputControl {19425})
		{
			if ({19424} == Keys.None)
			{
				return;
			}
			foreach (UiControl uiControl in ((IEnumerable<UiControl>){19423}.GetChildren))
			{
				this.{19422}(uiControl, {19424}, {19425});
				KeyInputControl keyInputControl = uiControl as KeyInputControl;
				if (keyInputControl != null && keyInputControl.CurrentKey == {19424} && keyInputControl != {19425})
				{
					keyInputControl.SetKey(Keys.None, true);
				}
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00057BC4 File Offset: 0x00055DC4
		private void {19426}(StackForm {19427})
		{
			this.{19428}({19427}, Local.game_settings_size);
			this.AddToollist<LocalSettings.GlobalUiScale>({19427}, Local.GameSettingsWindow_n11, Global.Settings.UiScale, delegate(LocalSettings.GlobalUiScale {19522})
			{
				Global.Settings.UiScale = {19522};
			}, new SelItem<LocalSettings.GlobalUiScale>[]
			{
				new SelItem<LocalSettings.GlobalUiScale>(LocalSettings.GlobalUiScale.IncreaseDouble, "140%"),
				new SelItem<LocalSettings.GlobalUiScale>(LocalSettings.GlobalUiScale.Increase, "120%"),
				new SelItem<LocalSettings.GlobalUiScale>(LocalSettings.GlobalUiScale.Normal, "100%"),
				new SelItem<LocalSettings.GlobalUiScale>(LocalSettings.GlobalUiScale.Decrease, "80%")
			});
			float minimapMin = 1f;
			float minimapMax = 1.6f;
			this.AddSlider({19427}, Local.GameSettingsWindow_105, (Global.Settings.MinimapScale - minimapMin) / (minimapMax - minimapMin), delegate(float {19537})
			{
				Global.Settings.MinimapScale = {19537} * (minimapMax - minimapMin) + minimapMin;
			});
			this.AddCheckbox({19427}, Local.GameSettingsWindow_110_b, "", Global.Settings.MinimapCompas, delegate(bool {19523})
			{
				Global.Settings.MinimapCompas = {19523};
			});
			this.AddCheckbox({19427}, Local.GameSettingsWindow_110, "", Global.Settings.BigChat, delegate(bool {19524})
			{
				Global.Settings.BigChat = {19524};
			});
			this.AddCheckbox({19427}, Local.GameSettingsWindow_111, "", Global.Settings.BigChatFont, delegate(bool {19525})
			{
				Global.Settings.BigChatFont = {19525};
			});
			this.AddCheckbox({19427}, Local.GameSettingsWindow_chatMyLang, "", Global.Settings.ChatOnlyMyLanguage, delegate(bool {19526})
			{
				Global.Settings.ChatOnlyMyLanguage = {19526};
			});
			this.AddCheckbox({19427}, Local.GameSettingsWindow_77, "", Global.Settings.ShowAmmoCountUi, delegate(bool {19527})
			{
				Global.Settings.ShowAmmoCountUi = {19527};
			});
			this.{19428}({19427}, Local.sight);
			this.AddCheckbox({19427}, Local.GameSettingsWindow_13, "", !Global.Settings.NoHideSight, delegate(bool {19528})
			{
				Global.Settings.NoHideSight = !{19528};
			});
			this.AddToollist<int>({19427}, Local.GameSettingsWindow_8, Global.Settings.SightIndex, delegate(int {19529})
			{
				Global.Settings.SightIndex = {19529};
			}, new SelItem<int>[]
			{
				new SelItem<int>(0, Local.GameSettingsWindow_9),
				new SelItem<int>(1, Local.GameSettingsWindow_10),
				new SelItem<int>(2, Local.GameSettingsWindow_11),
				new SelItem<int>(3, Local.GameSettingsWindow_12)
			});
			this.{19428}({19427}, Local.game_settings_other);
			this.AddCheckbox({19427}, Local.GameSettingsWindow_108, "", Global.Settings.ShowNicknames, delegate(bool {19530})
			{
				Global.Settings.ShowNicknames = {19530};
			});
			Action performAfterDraggablesReset = delegate()
			{
				new {19413}();
				{19413}.Instance.ForceItemSelected(1);
			};
			if (Global.Player == null || !Global.Player.MapInfo.IsEducationMap)
			{
				this.AddCheckbox({19427}, Local.enable_ui_drag_and_drop, "", Global.Settings.EnableDragDrop, delegate(bool {19538})
				{
					Global.Settings.EnableDragDrop = {19538};
					this.RefreshCurrentDynamicTabPage();
				});
				Button {12956} = new Button(Vector2.Zero, {17625}.c_btGray_big, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.reset, Fonts.Philosopher_12, Color.White * 0.9f, false).ExClick(delegate(ClickUiEventArgs {19539})
				{
					Global.Settings.ResetDragables(performAfterDraggablesReset);
				});
				{19427}.GetChildren[{19427}.CountChild() - 1].AddChildPos({12956}, PositionAlignment.RightDown, PositionAlignment.Center, 44f, 0f, false);
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00057FAC File Offset: 0x000561AC
		private Form {19428}(StackForm {19429}, string {19430})
		{
			if ({19429}.GetChildren.Size > 0)
			{
				{19429}.AddSpace(20f);
			}
			Form form = new Form(new Marker(0f, 0f, (float){19413}.width, 35f), CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Black * 0.15f,
				AnimatedFocus = false
			};
			Label {12952} = new Label(Vector2.Zero, Fonts.Philosopher_16, Color.White, "⚓ " + {19430}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos({12952}, PositionAlignment.LeftUp, PositionAlignment.Center, 33f);
			{19429}.AddItem(new UiControl[]
			{
				form
			});
			return form;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00058058 File Offset: 0x00056258
		private void {19431}(StackForm {19432}, string {19433}, string {19434})
		{
			Form form = new Form(new Marker(0f, 0f, (float){19413}.width, 35f), CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Black * 0.15f,
				AnimatedFocus = false
			};
			Label label = new Label(Vector2.Zero, Fonts.Philosopher_16, Color.White, "⚓ " + {19433}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos(label, PositionAlignment.LeftUp, PositionAlignment.Center, 33f);
			Label label2 = new Label(Vector2.Zero, Fonts.Philosopher_16Bold, Color.Gray, " " + Local.code_acc(Local.show + "     "), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos(label2, PositionAlignment.LeftUp, PositionAlignment.Center, 38f + label.Pos.WH.X);
			label2.EvClick += delegate(ClickUiEventArgs {19540})
			{
				((Label){19540}.Sender).Text = " " + Local.code_acc({19434});
			};
			Button button = new Button(Vector2.Zero, new Rectangle(785, 2543, 37, 35), PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {19541})
			{
				Engine.SetClipboardText({19434});
				((Button){19541}.Sender).TexturePath = new Rectangle(747, 2543, 37, 35);
			});
			button.Pos = button.Pos.Scale(0.85f);
			form.AddChildPos(button, PositionAlignment.LeftUp, PositionAlignment.Center, 43f + label.Pos.WH.X + label2.Pos.WH.X);
			{19432}.AddItem(new UiControl[]
			{
				form
			});
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x000581E0 File Offset: 0x000563E0
		private void {19435}(Form {19436})
		{
			{19436}.TexturePath = new Rectangle(0, 0, 1, 1);
			{19436}.BasicColor = Color.Transparent;
			{19436}.EvGotMouseFocus += delegate()
			{
				{19436}.BasicColor = Color.White * 0.1f;
			};
			{19436}.EvLostMouseFocus += delegate()
			{
				{19436}.BasicColor = Color.Transparent;
			};
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0005824C File Offset: 0x0005644C
		private CheckboxControl AddCheckbox(StackForm {19437}, string {19438}, string {19439}, bool {19440}, Action<bool> {19441})
		{
			CheckboxControl control = new CheckboxControl(Vector2.Zero, new Rectangle(691, 134, 62, 37), new Rectangle(754, 134, 62, 37), {19440}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			control.EvCheck += delegate(CheckboxCheckedEventArgs {19542})
			{
				{19441}({19542}.NewValue);
			};
			if (!string.IsNullOrEmpty({19439}))
			{
				control.ToolTipState = new ToolTipState(null, {19439}, Array.Empty<ToolTipCharacteristics>());
			}
			Form form = new Form(new Marker(0f, 0f, (float){19413}.width, {19413}.itemHeight), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form.AddChildPos(control, PositionAlignment.LeftUp, PositionAlignment.Center, {19413}.cLeftPos);
			form.AddChild(new Label(new Vector2({19413}.leftOffset, 6f), 300f, Fonts.PhilosopherSizes, Fonts.Philosopher_14, Color.LightGray, {19438}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				RenderToDepthMap = false
			});
			{19437}.AddItem(new UiControl[]
			{
				form
			});
			this.{19435}(form);
			form.EvClickEmptiness += delegate(ClickUiEventArgs {19543})
			{
				control.ImitateClick(false);
			};
			return control;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0005837C File Offset: 0x0005657C
		private DropdownControl<T> AddToollist<T>(StackForm {19442}, string {19443}, T {19444}, Action<T> {19445}, params SelItem<T>[] {19446})
		{
			Vector2 vector = new Vector2({19413}.cLeftPos, 0f);
			DropdownControl<T> dropdownControl = new DropdownControl<T>(new Marker(ref vector, 222f, {19413}.itemHeight), new Rectangle(691, 172, 222, 32), new Rectangle(691, 205, 222, 32), 222f, Fonts.PhilosopherSizes, {19413}.dropdownFont, {19446});
			dropdownControl.SelectByValue({19444});
			dropdownControl.EvChangeItem += delegate(SelItem<T> {19544})
			{
				{19445}({19544}.Value);
			};
			Form form = new Form(new Marker(0f, 0f, (float){19413}.width, {19413}.itemHeight), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form.AddChild(new Label(new Vector2({19413}.leftOffset, 6f), 300f, Fonts.PhilosopherSizes, Fonts.Philosopher_14, Color.LightGray, {19443}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				RenderToDepthMap = false
			});
			form.AddChild(dropdownControl);
			{19442}.AddItem(new UiControl[]
			{
				form
			});
			this.{19435}(form);
			return dropdownControl;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00058494 File Offset: 0x00056694
		private void AddSlider(StackForm {19447}, string {19448}, float {19449}, Action<float> {19450})
		{
			ProgressSelectBar progressSelectBar = new ProgressSelectBar(Vector2.Zero, new Rectangle(671, 80, 204, 26), new Rectangle(671, 107, 204, 26), new Rectangle(876, 80, 25, 25), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			progressSelectBar.MaxValue = 1f;
			progressSelectBar.Value = {19449};
			progressSelectBar.EvChange += delegate(ProgressBarChangeEventArgs {19545})
			{
				{19450}({19545}.NewValue);
			};
			Form form = new Form(new Marker(0f, 0f, (float){19413}.width, {19413}.itemHeight), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form.AddChild(new Label(new Vector2({19413}.leftOffset, 6f), 300f, Fonts.PhilosopherSizes, Fonts.Philosopher_14, Color.LightGray, {19448}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				RenderToDepthMap = false
			});
			form.AddChildPos(progressSelectBar, PositionAlignment.LeftUp, PositionAlignment.Center, {19413}.cLeftPos + 2f);
			{19447}.AddItem(new UiControl[]
			{
				form
			});
			this.{19435}(form);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000585A4 File Offset: 0x000567A4
		private void {19451}(StackForm {19452}, string {19453}, KeynameHolder {19454})
		{
			{19413}.<>c__DisplayClass32_0 CS$<>8__locals1 = new {19413}.<>c__DisplayClass32_0();
			CS$<>8__locals1.holder = {19454};
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.context = {19452};
			{19413}.<>c__DisplayClass32_0 CS$<>8__locals2 = CS$<>8__locals1;
			Vector2 vector = new Vector2({19413}.cLeftPos, 2f);
			Marker {13993} = new Marker(ref vector, (float)CommonAtlas.newToolList_item.Width * 0.6f, {19413}.itemHeightKeys - 2f);
			Rectangle newToolList_item = CommonAtlas.newToolList_item;
			Rectangle newToolList_item2 = CommonAtlas.newToolList_item;
			Keys key = CS$<>8__locals1.holder.Key;
			CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
			Func<Keys, string> {13998};
			if (({13998} = {19413}.<>O.<3>__GetKeyName) == null)
			{
				{13998} = ({19413}.<>O.<3>__GetKeyName = new Func<Keys, string>(CommonEnums.GetKeyName));
			}
			CS$<>8__locals2.control = new KeyInputControl({13993}, newToolList_item, newToolList_item2, key, philosopher_, {13998}, Local.press_key_settings, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.control.EvChangeKey += delegate(Keys {19546})
			{
				CS$<>8__locals1.holder.Set({19546});
				CS$<>8__locals1.<>4__this.{19422}(CS$<>8__locals1.control.GetParent.GetParent, {19546}, CS$<>8__locals1.control);
				StackForm context = CS$<>8__locals1.context;
			};
			Form form = new Form(new Marker(0f, 0f, (float){19413}.width, {19413}.itemHeightKeys), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form.AddChild(new Label(new Vector2({19413}.leftOffset, 6f), 300f, Fonts.PhilosopherSizes, Fonts.Philosopher_14, Color.LightGray, {19453}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				RenderToDepthMap = false
			});
			form.AddChild(CS$<>8__locals1.control);
			CS$<>8__locals1.context.AddItem(new UiControl[]
			{
				form
			});
			this.{19435}(form);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000586E8 File Offset: 0x000568E8
		public static void OpenChangePasswordDialog(bool {19455})
		{
			new {17312}(delegate(string {19531})
			{
				new {17312}(delegate(string {19547})
				{
					if ({19531} == {19547})
					{
						Global.Settings.AuthCreds.UpdatePass({19547});
						Global.Network.Send(new OnChangeAccountPeropertyMsg(AccountProtectionProperty.Password, PasswordHashing.Hash({19547})));
						return;
					}
					new {17312}(Local.EscapeInterface_12);
				}, Gcc.PassLengthLimits.Item2, Local.EscapeInterface_13, null, delegate(TextBox {19532})
				{
					TextBox.Moderator {13698};
					if (({13698} = {19413}.<>O.<2>__ModeratePassword) == null)
					{
						{13698} = ({19413}.<>O.<2>__ModeratePassword = new TextBox.Moderator(Gcc.ModeratePassword));
					}
					{19532}.AttachModerator({13698}, Color.OrangeRed);
				});
			}, Gcc.PassLengthLimits.Item2, {19455} ? Local.enterPasswordAfterReset : Local.EscapeInterface_14, null, delegate(TextBox {19533})
			{
				TextBox.Moderator {13698};
				if (({13698} = {19413}.<>O.<2>__ModeratePassword) == null)
				{
					{13698} = ({19413}.<>O.<2>__ModeratePassword = new TextBox.Moderator(Gcc.ModeratePassword));
				}
				{19533}.AttachModerator({13698}, Color.OrangeRed);
			});
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00058754 File Offset: 0x00056954
		protected override void UserBackRender()
		{
			Device gs = Engine.GS;
			Texture2D tex = AtlasPortGui.Texture.Tex;
			Rectangle rectangle = base.Pos.ToRect();
			Color color = Color.White * base.GetOpcaity();
			gs.DrawCustomTexture(tex, {21684}.main, rectangle, color);
			base.UserBackRender();
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000587A4 File Offset: 0x000569A4
		protected override void UserUpdate(ref FrameTime {19456})
		{
			Marker pos = base.Pos;
			Vector2 getPosition = {19413}.getPosition;
			base.Pos = pos.SetXY(getPosition);
			base.UserUpdate(ref {19456});
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000587ED File Offset: 0x000569ED
		[CompilerGenerated]
		private void {19457}(ListItemViewControl {19458})
		{
			{19458}.AddItem(new UiControl[]
			{
				this.{19414}(1)
			});
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00058805 File Offset: 0x00056A05
		[CompilerGenerated]
		private void {19459}(ListItemViewControl {19460})
		{
			{19460}.AddItem(new UiControl[]
			{
				this.{19414}(2)
			});
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0005881D File Offset: 0x00056A1D
		[CompilerGenerated]
		private void {19461}(ListItemViewControl {19462})
		{
			{19462}.AddItem(new UiControl[]
			{
				this.{19414}(3)
			});
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00058835 File Offset: 0x00056A35
		[CompilerGenerated]
		private void {19463}(ListItemViewControl {19464})
		{
			{19464}.AddItem(new UiControl[]
			{
				this.{19414}(4)
			});
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00058850 File Offset: 0x00056A50
		[CompilerGenerated]
		private void {19465}(ClickUiEventArgs {19466})
		{
			if (Session.Guild != null)
			{
				new {17312}(Local.account_delete_err_guild);
				return;
			}
			new {17312}(new Action<string>(this.{19467}), Gcc.PassLengthLimits.Item2, PlatformTuning.ExternalLoginAPI ? Local.approve_account_deletion : Local.enter_password, null, delegate(TextBox {19488})
			{
				TextBox.Moderator {13698};
				if (({13698} = {19413}.<>O.<2>__ModeratePassword) == null)
				{
					{13698} = ({19413}.<>O.<2>__ModeratePassword = new TextBox.Moderator(Gcc.ModeratePassword));
				}
				{19488}.AttachModerator({13698}, Color.OrangeRed);
			});
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000588C0 File Offset: 0x00056AC0
		[CompilerGenerated]
		private void {19467}(string {19468})
		{
			if ((PlatformTuning.ExternalLoginAPI && {19468} == "DELETE") || (!PlatformTuning.ExternalLoginAPI && PasswordHashing.Hash({19468}) == Session.Account.PasswordHash))
			{
				this.BlockAndClose();
				Global.Network.Conection.Stop(true, false, true);
				Global.Game.RegisterCallBreakToEntryOffline(Local.account_deleted(72));
				return;
			}
			new {17312}(Local.wrong_password);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00058939 File Offset: 0x00056B39
		[CompilerGenerated]
		private void {19469}(bool {19470})
		{
			Global.Settings.SetControlLayoutSpecifics({19470} ? ControlLayout.Azerty : ControlLayout.Qwerty);
			base.ForceItemSelected(3);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00058953 File Offset: 0x00056B53
		[CompilerGenerated]
		private void {19471}(bool {19472})
		{
			Global.Settings.MouseControlInversion = new Point({19472} ? -1 : 1, Global.Settings.MouseControlInversion.Y);
			base.ForceItemSelected(0);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00058981 File Offset: 0x00056B81
		[CompilerGenerated]
		private void {19473}(bool {19474})
		{
			Global.Settings.MouseControlInversion = new Point(Global.Settings.MouseControlInversion.X, {19474} ? -1 : 1);
			base.ForceItemSelected(0);
		}

		// Token: 0x040009CA RID: 2506
		public static {19413} Instance;

		// Token: 0x040009CB RID: 2507
		private static readonly Rectangle c_headLine = new Rectangle(1, 272, 442, 55);

		// Token: 0x040009CC RID: 2508
		private const float maxLabelWidth = 300f;

		// Token: 0x020001E8 RID: 488
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040009CD RID: 2509
			public static TextBox.Moderator <0>__ModerateNickname;

			// Token: 0x040009CE RID: 2510
			public static TextBox.Moderator <1>__ModerateLoginEmail;

			// Token: 0x040009CF RID: 2511
			public static TextBox.Moderator <2>__ModeratePassword;

			// Token: 0x040009D0 RID: 2512
			public static Func<Keys, string> <3>__GetKeyName;
		}
	}
}
