using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using WorldOfSeaBattles.Interface.BasicUi;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002B8 RID: 696
	internal class {20791} : CustomUi, IPortPage
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x000070D7 File Offset: 0x000052D7
		public bool CreateChatUi
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x000070D7 File Offset: 0x000052D7
		public bool CreateShipStatUi
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000F55 RID: 3925 RVA: 0x00080BC8 File Offset: 0x0007EDC8
		// (remove) Token: 0x06000F56 RID: 3926 RVA: 0x00080BFC File Offset: 0x0007EDFC
		public static event Action OnClicked;

		// Token: 0x06000F57 RID: 3927 RVA: 0x00080C30 File Offset: 0x0007EE30
		public {20791}() : base(false)
		{
			StackForm stackForm = new StackForm(new Vector2(0f, (float)(Engine.GS.UIArea.Height - {20791}.c_selectFlagBt.Height + {20791}.c_selectFlagBt.Height - 3)), UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.RightDown);
			this.AnimatedFocus = false;
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UIButton, 0.03f, 1f);
			Button openShips = new Button(Vector2.Zero, new Rectangle(685, 465, 143, 53), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			openShips.EvClick += this.{20798};
			stackForm.AddItem(new UiControl[]
			{
				openShips
			});
			Label label = new Label(openShips.Pos.XY + new Vector2(58f, 16f), Fonts.Philosopher_14, Color.Wheat * 0.8f, " ", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			label.RenderToDepthMap = false;
			openShips.AddChild(label);
			label.UpdateComplete += delegate(UiControl {20814})
			{
				Label label2 = (Label){20814};
				label2.Text = Session.Account.Shipyard.List.Count.ToString() + " / " + Session.Account.MaxShipsCount.Value.ToString();
				label2.Opacity = openShips.Opacity;
			};
			openShips.UpdateComplete += delegate(UiControl {20802})
			{
				{20802}.AllowMouseInput = Global.Game.ScenePort.IsAbleToChangeShip;
				{20802}.Opacity = (EducationHelper.MakeInvisibleSelectShipButton ? 0.3f : (Global.Game.ScenePort.IsAbleToChangeShip ? 1f : 0.4f));
			};
			openShips.ForceUpdateComplete();
			this.{20801} = new Button(Vector2.Zero, {20791}.c_selectFlagBt, PositionAlignment.LeftUp, PositionAlignment.RightDown);
			this.{20801}.EvClick += delegate(ClickUiEventArgs {20803})
			{
				new {21949}();
			};
			this.{20801}.ForceUpdateComplete();
			stackForm.AddItem(new UiControl[]
			{
				this.{20801}
			});
			Button button = new Button(Vector2.Zero, new Rectangle(788, 406, 144, 53), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button.Pos = button.Pos.SetWidth(button.Pos.WH.X * 0.8f);
			button.AddChildPos(KeyNotationUi.CreateKeyThenText(Local.open_map("").TrimStart(new char[]
			{
				' ',
				'-'
			}), Global.Settings.kb_Map.Key, Color.Wheat * 0.8f, 24), PositionAlignment.Center, PositionAlignment.Center, 0f);
			button.EvClick += delegate(ClickUiEventArgs {20804})
			{
				{22913}.TryToOpen();
			};
			stackForm.AddItem(new UiControl[]
			{
				button
			});
			Button button2 = new Button(Vector2.Zero, new Rectangle(933, 406, 144, 53), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button2.Pos = button2.Pos.SetWidth(button2.Pos.WH.X + 25f);
			button2.AddChildPos(KeyNotationUi.CreateKeyThenText(Local.port_exit_scene, Global.Settings.kb_Action.Key, Color.Wheat * 0.8f, 24), PositionAlignment.Center, PositionAlignment.Center, 0f);
			button2.EvClick += delegate(ClickUiEventArgs {20805})
			{
				{19086}.CreateExitOfPortMenu();
			};
			button2.UpdateComplete += delegate(UiControl {20806})
			{
				{20806}.BrightnessBlinkingMode = (Session.Account.IsEducationInProgress(EducationOnboarding.DestroyNpcBlack, false, false) && Session.Account.EducationQuest.HasFlag(EducationOnboarding.InteropQuestUnit));
			};
			stackForm.AddItem(new UiControl[]
			{
				button2
			});
			Button button3 = new Button(Vector2.Zero, new Rectangle(933, 352, 58, 53), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			UiControl uiControl = button3;
			string {12777} = "";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 4);
			defaultInterpolatedStringHandler.AppendFormatted(Local.installed);
			defaultInterpolatedStringHandler.AppendLiteral(": ");
			PersonalIsleStatus currentPersonalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
			int? value;
			if (currentPersonalIsle == null)
			{
				value = null;
			}
			else
			{
				Tlist<PersonalIsleInstalledDecorItem> installedDecor = currentPersonalIsle.InstalledDecor;
				value = ((installedDecor != null) ? new int?(installedDecor.Size) : null);
			}
			defaultInterpolatedStringHandler.AppendFormatted<int?>(value);
			defaultInterpolatedStringHandler.AppendLiteral(", ");
			defaultInterpolatedStringHandler.AppendFormatted(Local.in_storage_2);
			defaultInterpolatedStringHandler.AppendLiteral(": ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(Session.Account.EnvDecorElementsAtStorage.GetTotalItemsCount());
			uiControl.ToolTipState = new ToolTipState({12777}, Local.personal_isle_decor(defaultInterpolatedStringHandler.ToStringAndClear()), Array.Empty<ToolTipCharacteristics>());
			button3.EvClick += delegate(ClickUiEventArgs {20807})
			{
				new {20501}();
			};
			button3.IsVisible = false;
			button3.UpdateComplete += delegate(UiControl {20808})
			{
				{20808}.IsVisible = (Global.Game.ScenePort.CurrentPersonalIsle != null);
			};
			stackForm.AddItem(new UiControl[]
			{
				button3
			});
			Button button4 = new Button(Vector2.Zero, new Rectangle(992, 352, 58, 53), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button4.ToolTipState = new ToolTipState("", Local.craft_cballs, Array.Empty<ToolTipCharacteristics>());
			button4.EvClick += delegate(ClickUiEventArgs {20809})
			{
				Global.Game.ScenePort.tradeHandler(null);
			};
			button4.IsVisible = false;
			button4.UpdateComplete += delegate(UiControl {20810})
			{
				PersonalIsleStatus currentPersonalIsle2 = Global.Game.ScenePort.CurrentPersonalIsle;
				{20810}.IsVisible = (currentPersonalIsle2 != null && currentPersonalIsle2.AllowBuyingAmmo);
			};
			stackForm.AddItem(new UiControl[]
			{
				button4
			});
			base.AddChild(stackForm);
			this.{20796}();
			Global.Game.ScenePort.ShipsHolder.SeeTargetChanged += this.{20795};
			Global.Game.ScenePort.ShipsHolder.AddNewShip += this.{20796};
			Global.Game.ScenePort.ShipsHolder.RemoveShip += this.{20796};
			new UiOpacityAnimation(this, 0f, 1f, 150f);
			base.EvClick += delegate(ClickUiEventArgs {20811})
			{
				if ({19717}.CurrentInstance != null && {19717}.CurrentInstance.GetChildren.First().InputMode != MouseInputMode.NoFocus)
				{
					return;
				}
				Action onClicked = {20791}.OnClicked;
				if (onClicked == null)
				{
					return;
				}
				onClicked();
			};
			stackForm.UpdateComplete += delegate(UiControl {20812})
			{
				if ({20755}.CurrentInstance != null)
				{
					{20812}.Opacity = 0f;
					return;
				}
				if (Global.Render.UiMode == InterfaceMode.Cinematografic)
				{
					{20812}.Opacity = Renderer.UiOpacityToFocus({20812}.Pos);
					return;
				}
				{20812}.Opacity = 1f;
			};
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00081270 File Offset: 0x0007F470
		protected override void UserUpdate(ref FrameTime {20794})
		{
			Global.Player.UpdateCapacity();
			if (this.{20801} != null)
			{
				this.{20801}.Opacity = (EducationHelper.MakeInvisibleSelectFlagsButton ? 0.3f : 1f);
				this.{20801}.IsVisible = (Session.EngagingInPortBattle == PbsBatlleSide.None && Global.Game.ScenePort.IsAbleToChangeFlags);
			}
			if (InputHelper.IsClick(Keys.Escape) || Global.Settings.kb_ChatEnter.IsClick)
			{
				{17248} {17248} = this.{20800};
				if ({17248} == null)
				{
					return;
				}
				{17248}.RemoveFromContainer();
			}
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x000812FC File Offset: 0x0007F4FC
		protected override void UserFrontRender()
		{
			float opcaity = base.GetOpcaity();
			new Color(135, 165, 170) * 1.5f * opcaity;
			if (this.{20801} != null && this.{20801}.IsVisible)
			{
				Material material = LocalContent.WorldFlagTexture(Session.Account.WorldFlag, Session.Account.TensityMode, Session.Game.MapMyFraction.GetValueOrDefault(FractionID.None), false, false, false);
				float scale = opcaity * this.{20801}.GetOpcaity();
				Device gs = Engine.GS;
				Texture2D tex = material.Albedo.Tex;
				Rectangle rectangle = new Rectangle(material.Albedo.Tex.Bounds.X, material.Albedo.Tex.Bounds.Y, material.Albedo.Tex.Bounds.Width / 4, material.Albedo.Tex.Bounds.Height - 2);
				Marker marker = new Marker(8f, 6f, 45f, 39f);
				Marker marker2 = this.{20801}.Pos;
				Rectangle rectangle2 = marker.Offset(marker2.XY).ToRect();
				Color color = Color.White * 0.6f * scale;
				gs.DrawCustomTexture(tex, rectangle, rectangle2, color);
				Engine.GS.SetFont(Fonts.Arial_12);
				string[] array = Session.Account.WorldFlag.ToStringLocalFull().Split(' ', StringSplitOptions.None);
				Device gs2 = Engine.GS;
				string {14626} = array[0];
				Vector2 {14627} = new Vector2(56f, 9f) + this.{20801}.Pos.XY;
				color = new Color(162, 168, 138) * scale;
				gs2.DrawString({14626}, {14627}, color, 0f, Vector2.Zero, 0.9f);
				if (array.Length > 1)
				{
					Device gs3 = Engine.GS;
					string {14626}2 = array[1];
					Vector2 {14627}2 = new Vector2(56f, 26f) + this.{20801}.Pos.XY;
					color = new Color(162, 168, 138) * scale;
					gs3.DrawString({14626}2, {14627}2, color, 0f, Vector2.Zero, 0.9f);
				}
			}
			if (Global.Settings.DeathController.BlockPortExitSec > 0f)
			{
				Device gs4 = Engine.GS;
				Marker marker2 = new Marker((float)(Engine.GS.UIArea.Width / 2 - 400), 0f, 800f, 65f);
				Rectangle rectangle = marker2.ToRect();
				Color color = Color.White;
				gs4.Draw({20547}.c_path, rectangle, color);
				Engine.GS.SetFont(Fonts.Philosopher_14);
				Device gs5 = Engine.GS;
				string {14610} = Local.repair_and_recovery(Math.Ceiling((double)Global.Settings.DeathController.BlockPortExitSec));
				Vector2 vector = new Vector2((float)(Engine.GS.UIArea.Width / 2), 25f);
				color = new Color(255, 185, 185);
				gs5.DrawStringCentered({14610}, vector, color);
			}
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00003100 File Offset: 0x00001300
		private void {20795}()
		{
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00081628 File Offset: 0x0007F828
		private void {20796}()
		{
			if (this.{20800} == null)
			{
				return;
			}
			this.{20800}.RemoveFromContainer();
			this.{20797}();
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00081644 File Offset: 0x0007F844
		private void {20797}()
		{
			{20791}.<>c__DisplayClass19_0 CS$<>8__locals1 = new {20791}.<>c__DisplayClass19_0();
			CS$<>8__locals1.<>4__this = this;
			{20791}.<>c__DisplayClass19_0 CS$<>8__locals2 = CS$<>8__locals1;
			Rectangle uiarea = Engine.GS.UIArea;
			CS$<>8__locals2.f = new Form(new Marker(ref uiarea), PositionAlignment.Both, PositionAlignment.Both);
			CS$<>8__locals1.f.AnimatedFocus = false;
			this.{20800} = new {17248}(new Vector2(0f, (float)(Engine.GS.UIArea.Height - {20791}.c_selectFlagBt.Height)), true, delegate(PlayerShipDynamicInfo {20813})
			{
				if (Session.Account.Shipyard.CurrentRealShip != {20813})
				{
					if ({20813}.CraftFrom.Only1ShipInPb && Session.EngagingInPortBattle != PbsBatlleSide.None && !((Session.EngagingInPortBattle == PbsBatlleSide.Attacker) ? Session.PortBattleInfo.HasPlacesForUniqueShipAtt : Session.PortBattleInfo.HasPlacesForUniqueShipDef))
					{
						new {17312}(Local.pbenter_problem9({20813}.CraftFrom.ShipName));
						return;
					}
					Global.Game.ScenePort.ShipsHolder.See({20813});
				}
			}, Session.Account.Shipyard.List.ToArray<PlayerShipDynamicInfo>());
			this.{20800}.EvRemoveFromContainer += delegate()
			{
				CS$<>8__locals1.f.RemoveFromContainer();
			};
			CS$<>8__locals1.f.EvClick += delegate(ClickUiEventArgs {20815})
			{
				if (CS$<>8__locals1.<>4__this.{20800}.WasCheckboxClick)
				{
					CS$<>8__locals1.<>4__this.{20800}.WasCheckboxClick = false;
					return;
				}
				if (CS$<>8__locals1.<>4__this.{20800}.RankButtonClicked > 0)
				{
					return;
				}
				CS$<>8__locals1.<>4__this.{20800}.RemoveFromContainer();
				CS$<>8__locals1.<>4__this.{20800} = null;
			};
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00081790 File Offset: 0x0007F990
		[CompilerGenerated]
		private void {20798}(ClickUiEventArgs {20799})
		{
			{22279} currentInstance = {22279}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.RemoveFromContainer();
			}
			if (this.{20800} == null)
			{
				this.{20797}();
				GameScene.IncreaseGameInput();
				new UiOpacityAnimation({22001}.CurrentInstance, 1f, 0f, 500f);
				this.{20800}.EvRemoveFromContainer += delegate()
				{
					GameScene.DecreaseGameInput();
					if ({22001}.CurrentInstance != null)
					{
						new UiOpacityAnimation({22001}.CurrentInstance, 0f, 1f, 500f);
					}
				};
				return;
			}
			this.{20800}.RemoveFromContainer();
			this.{20800} = null;
		}

		// Token: 0x04000E17 RID: 3607
		public static readonly Rectangle c_button_addShip = new Rectangle(1161, 352, 39, 35);

		// Token: 0x04000E18 RID: 3608
		public static readonly Rectangle c_holdProgress = new Rectangle(788, 398, 142, 8);

		// Token: 0x04000E19 RID: 3609
		public static readonly Rectangle c_holdProgressFr = new Rectangle(788, 407, 142, 8);

		// Token: 0x04000E1A RID: 3610
		public static readonly Rectangle c_selectFlagBt = new Rectangle(1240, 227, 168, 53);

		// Token: 0x04000E1B RID: 3611
		private {17248} {20800};

		// Token: 0x04000E1C RID: 3612
		private Button {20801};
	}
}
