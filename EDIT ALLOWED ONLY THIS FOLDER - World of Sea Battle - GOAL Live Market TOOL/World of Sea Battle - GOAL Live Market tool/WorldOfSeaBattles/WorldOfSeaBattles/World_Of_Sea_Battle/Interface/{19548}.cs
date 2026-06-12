using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Common;
using Common.Account;
using Common.Data;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
using WorldOfSeaBattles.Interface.BasicUi;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001F3 RID: 499
	internal sealed class {19548} : CustomUi
	{
		// Token: 0x06000B65 RID: 2917 RVA: 0x00059268 File Offset: 0x00057468
		internal static int SwitchNextItemId(IStorageAsset {19549})
		{
			CannonBallInfo cannonBallInfo = {19549} as CannonBallInfo;
			if (cannonBallInfo != null && cannonBallInfo.AmmoType == CannonAmmoType.CannonBall)
			{
				int num = Array.IndexOf<int>({19548}.cannonAmmoOrder, (int){19549}.ID);
				int num2;
				do
				{
					num2 = {19548}.cannonAmmoOrder[num % {19548}.cannonAmmoOrder.Length];
				}
				while (num2 == 5 && Global.Player.UsedShipPlayer.BallsOfHold[num2] == 0);
				return num2;
			}
			int num3 = ({19549}.getType == StorageAssetEnum.Ammo) ? Gameplay.BallsInfo.Size : Gameplay.PowderKegsInfo.Size;
			short num4 = {19549}.ID;
			for (;;)
			{
				if ((int)(num4 += 1) > num3)
				{
					num4 = 1;
				}
				if (num4 == {19549}.ID)
				{
					break;
				}
				IStorageAsset resource = Gameplay.GetResource((int)num4, {19549}.getType);
				if (!(resource.getName == Local.removed))
				{
					int num5 = Global.Player.UsedShipPlayer.GetItemsCountInHold(resource);
					CannonBallInfo cannonBallInfo2 = resource as CannonBallInfo;
					if (cannonBallInfo2 != null)
					{
						if (cannonBallInfo2.AmmoType != ((CannonBallInfo){19549}).AmmoType)
						{
							continue;
						}
						if (cannonBallInfo2.Infinity)
						{
							num5 = int.MaxValue;
						}
					}
					if (num5 != 0)
					{
						return (int)num4;
					}
				}
			}
			return (int){19549}.ID;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x00059378 File Offset: 0x00057578
		public static {19548} CurrentInstance
		{
			get
			{
				return {19548}.currentInastance;
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00059380 File Offset: 0x00057580
		public {19548}() : base(false)
		{
			if (Global.Player.UsedShipPlayer.PowderKegsOfHold[Global.Settings.SelectedPowderKegs] == 0)
			{
				using (IEnumerator<GSILocalPair> enumerator = ((IEnumerable<GSILocalPair>)Global.Player.UsedShipPlayer.PowderKegsOfHold).GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						GSILocalPair gsilocalPair = enumerator.Current;
						Global.Settings.SelectedPowderKegs = gsilocalPair.ID;
					}
				}
			}
			if (Global.Player.UsedShipPlayer.BallsOfHold[Global.Settings.SelectedFalkonetsID] == 0 && !Session.SelectedFalkonetsInfo.Infinity)
			{
				foreach (GSILocalEnumerablePair<CannonBallInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<CannonBallInfo>>)Global.Player.UsedShipPlayer.BallsOfHold.CannonBallInfo))
				{
					if (gsilocalEnumerablePair.Info.AmmoType == CannonAmmoType.FalkonetBall)
					{
						Global.Settings.SelectedFalkonetsID = (int)gsilocalEnumerablePair.Info.ID;
						break;
					}
				}
			}
			if (Global.Player.UsedShipPlayer.BallsOfHold[Global.Settings.SelectedMortarBallsID] == 0)
			{
				foreach (GSILocalEnumerablePair<CannonBallInfo> gsilocalEnumerablePair2 in ((IEnumerable<GSILocalEnumerablePair<CannonBallInfo>>)Global.Player.UsedShipPlayer.BallsOfHold.CannonBallInfo))
				{
					if (gsilocalEnumerablePair2.Info.AmmoType == CannonAmmoType.MortarBall)
					{
						Global.Settings.SelectedMortarBallsID = (int)gsilocalEnumerablePair2.Info.ID;
						break;
					}
				}
			}
			this.{19585} = Global.Player.UsedShipPlayer.BallsOfHold.Clone();
			this.{19588} = new Form(new Marker((float)(Engine.GS.UIArea.Width / 2), (float)(Engine.GS.UIArea.Height - 7), 0f, 0f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				PositionAlignment_X = PositionAlignment.Center,
				PositionAlignment_Y = PositionAlignment.RightDown,
				AllowDragDrop = Global.Settings.EnableDragDrop
			};
			base.AddChild(this.{19588});
			this.{19583} = new StackForm(this.{19588}.Pos.XY, UiOrientation.HorizontalBottom, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if ({19548}.currentInastance != null)
			{
				throw new InvalidOperationException();
			}
			{19548}.currentInastance = this;
			this.AnimatedFocus = false;
			this.{19577} = new Tlist<{19548}.PowerupItemItem>(3);
			if (!Global.Player.UsedShip.StaticInfo.IsBalloon)
			{
				for (int i = 0; i < 3; i++)
				{
					if (Session.ActivePowerupItemSlots[i] != null)
					{
						Tlist<{19548}.PowerupItemItem> tlist = this.{19577};
						{19548}.PowerupItemItem powerupItemItem = new {19548}.PowerupItemItem(this, i, Session.ActivePowerupItemSlots[i], (i == 0) ? Global.Settings.kb_Item1 : ((i == 1) ? Global.Settings.kb_Item2 : ((i == 2) ? Global.Settings.kb_Item3 : new KeynameHolder(Keys.N, Array.Empty<Buttons>()))));
						tlist.Add(powerupItemItem);
					}
				}
			}
			for (int j = 0; j < 3 - this.{19577}.Size; j++)
			{
				this.{19583}.AddItem(new UiControl[]
				{
					new Form(new Marker(ref {19548}.c_bt_PowerupItem), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
			}
			for (int k = 0; k < this.{19577}.Size; k++)
			{
				this.{19583}.AddItem(new UiControl[]
				{
					this.{19577}.Array[k]
				});
			}
			this.{19588}.AddChild(this.{19560}(Gameplay.PowerupItems.Array[0], Global.Settings.kb_ItemExtra, 91f, true));
			if (Global.Player.CraftFrom.PowerupSkillId != 0)
			{
				this.{19588}.AddChild(this.{19560}(Gameplay.PowerupItems.Array[Global.Player.CraftFrom.PowerupSkillId], Global.Settings.kb_ShipPerk, 44f, false));
			}
			this.{19576} = new {19548}.SimpleButton(this, Global.Settings.kb_OpenHold, {19548}.c_bt_hold[0]);
			this.{19576}.ToolTip = new ToolTipState(Local.hold, null, Array.Empty<ToolTipCharacteristics>());
			this.{19576}.Apply += delegate()
			{
				if ({18139}.CurrentInstance != null)
				{
					return;
				}
				if ({17745}.CurrentInstance == null)
				{
					new {17745}(new Vector2(410f, (float)(Engine.GS.UIArea.Height + 80)), true);
					EducationHelper.MakeFlag(EducationOnboarding.OpenHoldFromGame, false);
					return;
				}
				{17745}.CurrentInstance.RemoveFromContainer();
			};
			this.{19575} = new {19548}.SimpleButton(this, Global.Settings.kb_Mending, {19548}.c_bt_mending);
			this.{19575}.ToolTip = new ToolTipState(null, Local.mendingButtonTt, Array.Empty<ToolTipCharacteristics>());
			this.{19575}.Apply += delegate()
			{
				if (Global.Player.IsDestroyed || Session.CurrentArenaSession != null)
				{
					return;
				}
				if (Global.Player.IsMendingBegin)
				{
					Global.Player.StopMending(true);
					return;
				}
				if (!Global.Player.CrewIsBusy)
				{
					if (Global.Player.UsedShip.FirstHP.BigBurningProcessTime == 0f)
					{
						if (Global.Player.UsedShip.IsSailesFull && Global.Player.UsedShip.FirstHP.Summary >= Global.Player.UsedShip.MaxHp)
						{
							{19994}.Me({19988}.Info, Local.ItemPanelGui_6, Array.Empty<object>());
							return;
						}
						if (Global.Player.UsedShip.IsSailesFull && Global.Player.ResourcesOfHold[1] < 1)
						{
							{19994}.Me({19988}.Info, Global.Player.UsedShip.StaticInfo.IsBalloon ? Local.PortAllInterface_19 : Local.ItemPanelGui_7, Array.Empty<object>());
							return;
						}
						if (!Global.Player.UsedShip.StaticInfo.IsBalloon && Global.Player.UsedShip.FirstHP.Summary >= Global.Player.UsedShip.MaxHp && Global.Player.ResourcesOfHold[3] < 1)
						{
							{19994}.Me({19988}.Info, Local.ItemPanelGui_8, Array.Empty<object>());
							return;
						}
						if (Global.Player.ResourcesOfHold[1] < 1 && Global.Player.ResourcesOfHold[3] < 1)
						{
							{19994}.Me({19988}.Info, Local.ItemPanelGui_9, Array.Empty<object>());
							return;
						}
					}
					Global.Player.BeginMending();
				}
			};
			this.{19583}.AddItem(new UiControl[]
			{
				this.{19576}
			});
			this.{19583}.AddItem(new UiControl[]
			{
				new Form(new Marker(0f, 0f, 12f, 12f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			float num = 7f;
			Marker pos;
			if (!Global.Player.UsedShip.StaticInfo.IsBalloon)
			{
				this.{19584} = new {19548}.CenterClass(this);
				pos = this.{19584}.Pos;
				Form form = new Form(pos.SetHeight(this.{19584}.Pos.WH.Y + num), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				UiControl uiControl = this.{19584};
				pos = this.{19584}.Pos;
				uiControl.Pos = pos.Offset(0f, num * 2f);
				form.AddChild(this.{19584});
				this.{19583}.AddItem(new UiControl[]
				{
					form
				});
			}
			this.{19583}.AddItem(new UiControl[]
			{
				new Form(new Marker(0f, 0f, 14f, 14f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			this.{19583}.AddItem(new UiControl[]
			{
				this.{19575}
			});
			if (!Global.Player.UsedShip.StaticInfo.IsBalloon)
			{
				if (Global.Player.UsedShip.Mortars.Count > 0)
				{
					this.{19580} = new {19548}.MortarBallItem(this, Vector2.Zero, {19548}.c_bt_weapons_1);
					this.{19583}.AddItem(new UiControl[]
					{
						this.{19580}
					});
				}
				this.{19578} = new {19548}.FalkonetBallItem(this, Global.Settings.kb_SwithPanelFalkonet, {19548}.c_bt_weapons_1);
				this.{19583}.AddItem(new UiControl[]
				{
					this.{19578}
				});
			}
			this.{19583}.AddItem(new UiControl[]
			{
				this.{19579} = new {19548}.PowderKegItem(this, Global.Settings.kb_SwithPanelPowderKeg, {19548}.c_bt_weapons_2)
			});
			this.{19583}.AddItem(new UiControl[]
			{
				this.{19587} = new {19548}.FoodItem(this)
			});
			this.{19581} = new Sprite({19548}.sprite, 10, 1000f, 0);
			this.{19581}.IsLoop = true;
			this.{19588}.Pos = this.{19583}.Pos;
			this.{19588}.AddChild(this.{19583});
			float x;
			if (this.{19584} != null)
			{
				pos = this.{19584}.Pos;
				x = pos.Center.X;
			}
			else
			{
				pos = this.{19588}.Pos;
				x = pos.Center.X;
			}
			float num2 = x - (float)(Engine.GS.UIArea.Width / 2);
			UiControl uiControl2 = this.{19588};
			pos = this.{19588}.Pos;
			uiControl2.Pos = pos.Offset(-num2, (this.{19584} == null) ? 0f : (this.{19588}.Pos.WH.Y * 0f));
			{20391}.WhenInit(this.{19588}, "itemPanelGui");
			base.EvRemoveFromContainer += delegate()
			{
				{19548}.currentInastance = null;
			};
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00059BDC File Offset: 0x00057DDC
		protected override void UserUpdate(ref FrameTime {19550})
		{
			base.IsVisible = ({18139}.CurrentInstance == null);
			if (!base.IsVisible && {17745}.CurrentInstance != null)
			{
				{17745}.CurrentInstance.RemoveFromContainer();
			}
			if (Global.Player != null && Global.Player.IsMendingBegin)
			{
				this.{19581}.Update(ref {19550});
			}
			{19550}.EvaluteTimerMs(ref this.PowerupItemResponseTimeoutMs);
			CannonLocation? lastActiveNearBoard = InGameSightUi.CannonSights.LastActiveNearBoard;
			if (GameScene.GameHasInputFocus && Global.Settings.kb_Mortar_ModifierKey.IsDown && this.{19580} != null)
			{
				if (Global.Settings.kb_Mortar_Whole.IsClick)
				{
					this.{19580}.Load(Gameplay.BallsInfo.FromID(6));
				}
				else if (Global.Settings.kb_Mortar_Splinter.IsClick)
				{
					this.{19580}.Load(Gameplay.BallsInfo.FromID(8));
				}
			}
			if (this.{19584} != null)
			{
				this.{19584}.Opacity = InGameSightUi.CurrentInstance.MarchingModeEffect * 0.7f + 0.3f;
			}
			if (this.{19578} != null)
			{
				this.{19578}.Opacity = InGameSightUi.CurrentInstance.MarchingModeEffect * 0.7f + 0.3f;
			}
			if (this.{19580} != null)
			{
				this.{19580}.Opacity = InGameSightUi.CurrentInstance.MarchingModeEffect * 0.7f + 0.3f;
			}
			if (Global.Player.UsedShip.FirstHP.FloodingFactor > 0f)
			{
				this.{19575}.Opacity = 0.3f;
				foreach ({19548}.PowerupItemItem powerupItemItem in ((IEnumerable<{19548}.PowerupItemItem>)this.{19577}))
				{
					powerupItemItem.Opacity = 0.3f;
				}
			}
			Geometry.Evalute(ref this.{19586}, Math.Min(1f, Global.Player.UsedShipPlayer.GetItemsMass() / Global.Player.UsedShipPlayer.Capacity), {19550}.secElapsed * 0.4f);
			base.FirstOpacity = ((Global.Render.UiMode == InterfaceMode.Default) ? 1f : 0.3f);
			this.{19576}.BrightnessBlinkingMode = {18695}.Active;
			{19550}.EvaluteTimerMs(ref this.{19590});
			this.{19576}.IsVisible = this.ShowHoldButton;
			this.{19575}.IsVisible = this.ShowMendingButton;
			if (this.{19578} != null)
			{
				this.{19578}.IsVisible = this.ShowFalkonets;
			}
			if (this.{19579} != null)
			{
				this.{19579}.IsVisible = this.ShowKegs;
			}
			if (this.{19584} != null)
			{
				this.{19584}.IsVisible = this.ShowAmmoAndReload;
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00059E88 File Offset: 0x00058088
		protected override void UserFrontRender()
		{
			float opcaity = base.GetOpcaity();
			int linearStateCode = Global.Player.FirstController.LinearStateCode;
			if (linearStateCode != this.{19589})
			{
				this.{19589} = linearStateCode;
				this.{19590} = 1500f;
			}
			if (EducationHelper.AlwaysShowSpeedIcon)
			{
				this.{19590} = Math.Max(this.{19590}, 1500f);
			}
			if (this.{19590} > 0f)
			{
				Color color = Color.White * opcaity;
				float num = MathHelper.Clamp((float)Engine.GS.UIArea.Width, 1300f, 2200f) / 1900f;
				float num2 = 302f * num;
				float num3 = 83f * num;
				Marker marker = new Marker((float)(Engine.GS.UIArea.Width / 2) - num2 / 2f, (float)Engine.GS.UIArea.Height - num3 - 125f, num2, num3);
				float scale = MathF.Pow(Geometry.Saturate((this.{19590} - 1000f) / 500f), 2f);
				float scale2 = Geometry.Saturate(this.{19590} / 1000f);
				Device gs = Engine.GS;
				Rectangle rectangle = marker.ToRect();
				Color color2 = color * scale * 0.3f;
				gs.Draw({19548}.c_speed_Anim, rectangle, color2);
				if (linearStateCode == 3)
				{
					Device gs2 = Engine.GS;
					rectangle = marker.ToRect();
					color2 = color * scale2;
					gs2.Draw({19548}.c_speed_3, rectangle, color2);
				}
				if (linearStateCode == 2)
				{
					Device gs3 = Engine.GS;
					rectangle = marker.ToRect();
					color2 = color * scale2;
					gs3.Draw({19548}.c_speed_2, rectangle, color2);
				}
				if (linearStateCode == 1)
				{
					Device gs4 = Engine.GS;
					rectangle = marker.ToRect();
					color2 = color * scale2;
					gs4.Draw({19548}.c_speed_1, rectangle, color2);
				}
				if (linearStateCode == 4)
				{
					Device gs5 = Engine.GS;
					rectangle = marker.ToRect();
					color2 = color * scale2;
					gs5.Draw({19548}.c_speed_Back, rectangle, color2);
				}
				if (EducationHelper.AlwaysShowSpeedIcon)
				{
					{18593} currentInstance = {18593}.CurrentInstance;
					if (currentInstance != null && currentInstance.FollowTaskPosition != null)
					{
						KeyNotationUi.CreateByDraw(Global.Settings.kb_ds_Forward.Key, marker.XY + new Vector2(-30f, 0f), 50, color);
						KeyNotationUi.CreateByDraw(Global.Settings.kb_ds_Backward.Key, marker.XY + new Vector2(-30f, 40f), 50, color);
					}
				}
			}
			if (this.{19576}.IsVisible)
			{
				this.{19576}.TexturePath = ((this.{19586} >= 1f) ? {19548}.overloadedHold : {19548}.c_bt_hold[(this.{19586} < 0.1f) ? 0 : ((this.{19586} < 0.4f) ? 1 : ((this.{19586} < 0.7f) ? 2 : 3))]);
				int num4 = (int)((float){19548}.c_holdProgress.Height * this.{19586});
				Device gs6 = Engine.GS;
				Rectangle rectangle = new Rectangle({19548}.c_holdProgress.X, {19548}.c_holdProgress.Y + ({19548}.c_holdProgress.Height - num4), {19548}.c_holdProgress.Width, num4);
				Vector2 vector = this.{19576}.Pos.XY + new Vector2(42f, (float)(3 + {19548}.c_holdProgress.Height - num4));
				Marker marker2 = new Marker(ref vector, (float){19548}.c_holdProgress.Width, (float)num4);
				Rectangle rectangle2 = marker2.ToRect();
				gs6.Draw(rectangle, rectangle2);
			}
			Texture2D currentTexture = Engine.GS.CurrentTexture;
			for (int i = 0; i < this.{19577}.Size; i++)
			{
				this.{19577}.Array[i].RenderIcon();
			}
			this.{19575}.RenderIcon();
			this.{19576}.RenderIcon();
			{19548}.FalkonetBallItem falkonetBallItem = this.{19578};
			if (falkonetBallItem != null)
			{
				falkonetBallItem.RenderIcon();
			}
			{19548}.PowderKegItem powderKegItem = this.{19579};
			if (powderKegItem != null)
			{
				powderKegItem.RenderIcon();
			}
			{19548}.MortarBallItem mortarBallItem = this.{19580};
			if (mortarBallItem != null)
			{
				mortarBallItem.RenderIcon();
			}
			Engine.GS.SetTexture(currentTexture);
			for (int j = 0; j < this.{19577}.Size; j++)
			{
				this.{19577}.Array[j].RenderFront();
			}
			int num5 = -41;
			int num6 = 68;
			int num7 = 0;
			foreach (int num8 in Session.Account.EnumerateActiveExtraPowerupItems())
			{
				PowerupItemInfo powerupItemInfo = Gameplay.PowerupItems.Array[num8];
				Device gs7 = Engine.GS;
				Texture2D icon = powerupItemInfo.Icon;
				Rectangle rectangle = powerupItemInfo.Icon.Bounds;
				Marker marker2 = new Marker((float)(num5 - num7++ * 39), (float)num6, 39f, 39f);
				Marker marker3 = this.{19583}.Pos;
				marker2 = marker2.Offset(marker3.XY);
				Rectangle rectangle2 = marker2.ToRect();
				Color color2 = Color.White * 0.6f * (0.7f + 0.05f * (float)Math.Sin(Global.Game.GameTotalTimeSec * 4.0));
				gs7.DrawCustomTexture(icon, rectangle, rectangle2, color2);
			}
			if (Global.Player.UsedShip.DamageUpIfStrengthBefore30pNow > 0f)
			{
				ShipUpgradeInfo upgradeByEffect = Global.Player.UsedShipPlayer.Upgrades.GetUpgradeByEffect(ShipBonusEffect.PDamageCannonIfStrengthBelow30P);
				Device gs8 = Engine.GS;
				Texture2D iconTexture = upgradeByEffect.IconTexture;
				Rectangle rectangle = upgradeByEffect.IconTexture.Bounds;
				Marker marker3 = new Marker((float)(num5 - num7++ * 39), (float)num6, 39f, 39f);
				Marker marker2 = this.{19583}.Pos;
				marker3 = marker3.Offset(marker2.XY);
				Rectangle rectangle2 = marker3.ToRect();
				Color color2 = Color.White * 0.6f * (0.7f + 0.05f * (float)Math.Sin(Global.Game.GameTotalTimeSec * 4.0));
				gs8.DrawCustomTexture(iconTexture, rectangle, rectangle2, color2);
			}
			{19548}.<>c__DisplayClass76_0 CS$<>8__locals1;
			CS$<>8__locals1.p = this.{19575}.Pos.XY - new Vector2(-1f, 25f);
			if (Global.Player.IsMendingBegin)
			{
				this.{19581}.Render(this.{19575}.Pos);
				if (!Global.Player.UsedShipPlayer.IsSailesFull)
				{
					{19548}.<UserFrontRender>g__DrawResourceAmount|76_0(3, true, ref CS$<>8__locals1);
				}
				if (Global.Player.UsedShip.FirstHP.Summary < Global.Player.UsedShip.MaxHp)
				{
					{19548}.<UserFrontRender>g__DrawResourceAmount|76_0(1, true, ref CS$<>8__locals1);
				}
				if (Global.Player.UsedShip.FirstHP.BigBurningFlareUpLevel > 0f)
				{
					{19548}.<UserFrontRender>g__DrawResourceAmountNoId|76_1(AtlasGameGui.Texture.Tex, new Rectangle(2631, 679, 18, 18), Local.ItemPanelGui_14b + (Math.Round((double)(Global.Player.UsedShip.FirstHP.BigBurningMengingLevel * 10f)) * 10.0).ToString() + "%", Color.Wheat, true, ref CS$<>8__locals1);
				}
			}
			else if (Global.Player.UsedShip.FirstHP.BigBurningFlareUpLevel > 0f)
			{
				Device gs9 = Engine.GS;
				Form form = this.{19575};
				Marker marker2 = this.{19575}.Pos;
				Marker marker3 = {19548}.mendingButtonArea.Offset(marker2.XY);
				Rectangle rectangle = marker3.ToRect();
				Color color2 = new Color(255, 0, 0, 128) * 0.8f * (0.5f + 0.5f * (float)Math.Sin(Global.Game.GameTotalTimeSec * 8.0));
				gs9.Draw(form.TexturePath, rectangle, color2);
			}
			if (Global.Player.UsedShip.StaticInfo.HasSteamWheel)
			{
				{19548}.<UserFrontRender>g__DrawResourceAmount|76_0(12, true, ref CS$<>8__locals1);
			}
			this.{19575}.RenderFront();
			this.{19576}.RenderFront();
			{19548}.FalkonetBallItem falkonetBallItem2 = this.{19578};
			if (falkonetBallItem2 != null)
			{
				falkonetBallItem2.RenderFront();
			}
			{19548}.FoodItem foodItem = this.{19587};
			if (foodItem != null)
			{
				foodItem.RenderFront();
			}
			{19548}.PowderKegItem powderKegItem2 = this.{19579};
			if (powderKegItem2 != null)
			{
				powderKegItem2.RenderFront();
			}
			{19548}.MortarBallItem mortarBallItem2 = this.{19580};
			if (mortarBallItem2 != null)
			{
				mortarBallItem2.RenderFront();
			}
			if (this.{19575}.IsVisible)
			{
				Engine.GS.SetFont(Fonts.Arial_10Bold);
				Device gs10 = Engine.GS;
				string {14599} = Global.Player.ResourcesOfHold.GetCount(1).ToString();
				Vector2 vector = this.{19575}.Pos.XY + new Vector2(3f, -1f);
				Color color2 = Color.Gray * opcaity;
				gs10.DrawString({14599}, vector, color2);
				if (Session.CurrentArenaSession != null || !Global.Player.SpeedAllowMending)
				{
					Engine.GS.SetFont(Fonts.Philosopher_18);
					Device gs11 = Engine.GS;
					string {14610} = "X";
					Marker marker2 = this.{19575}.Pos;
					Marker marker3 = {19548}.mendingButtonArea.Offset(marker2.XY);
					vector = marker3.Center - new Vector2(0f, 1f);
					color2 = Color.Black * opcaity;
					gs11.DrawStringCentered({14610}, vector, color2);
					Device gs12 = Engine.GS;
					string {14610}2 = "X";
					marker2 = this.{19575}.Pos;
					marker3 = {19548}.mendingButtonArea.Offset(marker2.XY);
					vector = marker3.Center;
					color2 = Color.White * opcaity;
					gs12.DrawStringCentered({14610}2, vector, color2);
					this.{19575}.TexturePath = {19548}.c_bt_mending_disabled;
				}
				else
				{
					this.{19575}.TexturePath = {19548}.c_bt_mending;
				}
				if (Session.CurrentArenaSession == null && !Global.Player.IsMendingBegin && Global.Player.UsedShip.HpFactor < 0.33f && Global.Player.ResourcesOfHold[1] > 0)
				{
					Device gs13 = Engine.GS;
					Marker marker2 = this.{19575}.Pos;
					Rectangle rectangle = marker2.ToRect();
					color2 = Color.White * (0.5f + 0.5f * (float)Math.Sin(Global.Game.GameTotalTimeSec * 2.7));
					gs13.Draw({19548}.c_bt_mending_redAura, rectangle, color2);
				}
			}
			for (int k = 0; k < this.{19577}.Size; k++)
			{
				this.{19577}.Array[k].DrawText();
			}
			Vector2 vector2 = (this.{19584} == null) ? new Vector2(100000f, 0f) : (this.{19584}.Pos.XY + new Vector2(116f, this.{19584}.Pos.WH.Y - 79f + 15f));
			if (Global.Player.IsMendingBegin)
			{
				vector2.Y -= 40f;
			}
			if (Global.Player.UsedShip.FirstHP.BigHoleTime > 0f)
			{
				this.{19551}(ref vector2, {19548}.c_notif_corpus, Local.ItemPanelGui_19);
			}
			if (Global.Player.IsInShallowWater > 0f)
			{
				this.{19551}(ref vector2, {19548}.c_notif_shallow, Local.ItemPanelGui_20);
			}
			if (Global.Settings.ShowAmmoCountUi && {19891}.CurrentInstance != null && Global.Render.UiMode == InterfaceMode.Default)
			{
				float num9 = 0f;
				Engine.GS.SetFont(Fonts.Philosopher_16);
				foreach (CannonBallInfo cannonBallInfo in ((IEnumerable<CannonBallInfo>)Gameplay.BallsInfo))
				{
					int num10 = Global.Player.UsedShipPlayer.BallsOfHold[(int)cannonBallInfo.ID];
					if ((cannonBallInfo.AmmoType == CannonAmmoType.CannonBall || cannonBallInfo.AmmoType == CannonAmmoType.MortarBall) && this.{19585}[(int)cannonBallInfo.ID] + num10 > 0)
					{
						float scale3 = (Global.Player.UsedShip.StaticInfo.LeftSidePorts.Length != 0 && Global.Settings.SelectedCannonBalls[InGameSightUi.CannonSights.LastActiveNearBoard.GetValueOrDefault(CannonLocation.RightSide)] == (int)cannonBallInfo.ID) ? 1f : 0.5f;
						num9 -= (float)({19548}.c_cannonBallHl.Height - 5);
						Vector2 value = new Vector2({19891}.CurrentInstance.Pos.XY.X - 130f, (float)Engine.GS.UIArea.Height + num9 - 10f);
						Device gs14 = Engine.GS;
						Marker marker2 = new Marker(ref value, ref {19548}.c_cannonBallHl);
						marker2 = marker2.ScaleWidth(0.9f);
						Rectangle rectangle = marker2.ToRect();
						Color color2 = Color.White * scale3;
						gs14.Draw({19548}.c_cannonBallHl, rectangle, color2);
						Color value2 = (num10 == 0) ? Color.Gray : Color.Lerp(Color.White, Color.Orange, 1f - Geometry.Saturate((float)(num10 / Math.Max(1, Global.Player.UsedShip.StaticInfo.LeftSidePorts.Length)) / 8f));
						Device gs15 = Engine.GS;
						string {14599}2 = num10.ToString() ?? "";
						Vector2 vector = value + new Vector2(55f, 14f);
						color2 = value2 * scale3;
						gs15.DrawString({14599}2, vector, color2);
						Device gs16 = Engine.GS;
						Texture2D iconTextureCircular = cannonBallInfo.IconTextureCircular;
						rectangle = cannonBallInfo.IconTextureCircular.Bounds;
						marker2 = new Marker(4f, 5f, 42f, 42f);
						marker2 = marker2.Offset(value);
						Rectangle rectangle2 = marker2.ToRect();
						color2 = Color.White * scale3;
						gs16.DrawCustomTexture(iconTextureCircular, rectangle, rectangle2, color2);
					}
				}
			}
			if (InGameSightUi.CannonSights.PickedGunMode != CannonsAttackMode.AllNormal && this.{19584} != null)
			{
				Rectangle rectangle3 = CannonsController.GunModeIconPath(InGameSightUi.CannonSights.PickedGunMode);
				Device gs17 = Engine.GS;
				Texture2D tex = AtlasObjs.Texture.Tex;
				Marker marker2 = this.{19584}.Pos;
				Vector2 vector = new Vector2(marker2.Center.X - (float)(rectangle3.Width / 2), this.{19584}.Pos.XY.Y - (float)rectangle3.Height - 10f);
				Color color2 = Color.White;
				gs17.DrawCustomTexture(tex, rectangle3, vector, color2);
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0005AD64 File Offset: 0x00058F64
		private void {19551}(ref Vector2 {19552}, Rectangle {19553}, string {19554})
		{
			{19552}.Y -= (float)({19553}.Height + 4);
			Device gs = Engine.GS;
			Rectangle rectangle = new Marker(ref {19552}, 36f, 36f).ToRect();
			Color color = Color.White * base.Opacity;
			gs.Draw({19553}, rectangle, color);
			Engine.GS.SetFont(Fonts.Philosopher_16);
			Device gs2 = Engine.GS;
			Vector2 vector = {19552} + new Vector2(40f, 6f);
			color = new Color(255, 65, 48) * base.Opacity;
			gs2.DrawString({19554}, vector, color);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0005AE14 File Offset: 0x00059014
		private void {19555}(Vector2 {19556}, string {19557})
		{
			Label label = {19548}.selectedItemName;
			if (label != null)
			{
				label.RemoveDestroyEvents();
			}
			Label label2 = {19548}.selectedItemName;
			if (label2 != null)
			{
				label2.RemoveFromContainer();
			}
			{19548}.selectedItemName = new Label({19556} + new Vector2(-89f, -58f) + new Vector2(1f, -50f), Fonts.Philosopher_24, Color.White * 0.75f, {19557}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{19548}.selectedItemName.PositionAlignment_Y = PositionAlignment.RightDown;
			{19548}.selectedItemName.EvRemoveFromContainer += delegate()
			{
				{19548}.selectedItemName = null;
			};
			new UiOpacityAnimation({19548}.selectedItemName, 0f, 2500f);
			new UiRemoveAction({19548}.selectedItemName);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0005AEE0 File Offset: 0x000590E0
		private void PrepareSwitchForm(Vector2 {19558}, Func<string> {19559})
		{
			StackForm stackForm = this.{19582};
			if (stackForm != null)
			{
				stackForm.RemoveFromContainer();
			}
			this.{19582} = new StackForm({19558} + new Vector2(-89f, -58f), UiOrientation.Horizontal, base.PositionAlignment_X, base.PositionAlignment_Y);
			string {13345} = {19559}();
			Label {14087} = new Label(this.{19582}.Pos.XY - new Vector2(-1f, 21f), Fonts.F_m14_ThinBold, Color.White * 0.75f, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{19582}.AddItemWithoutChangePosition({14087});
			this.{19582}.DisableDepthFocusTest = true;
			new UiActionsSleep(this.{19582}, 1500f);
			new UiOpacityAnimation(this.{19582}, 1f, 0f, 1000f);
			new UiActor(this.{19582}, new Action(this.{19574}));
			this.{19588}.AddChild(this.{19582});
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0005B3AC File Offset: 0x000595AC
		[CompilerGenerated]
		private {19548}.PowerupItemItem {19560}(PowerupItemInfo {19561}, KeynameHolder {19562}, float {19563}, bool {19564} = false)
		{
			{19548}.PowerupItemItem tempSlot = new {19548}.PowerupItemItem(this, {19564} ? 255 : {19561}.Index, {19561}, {19562});
			this.{19577}.Add(tempSlot);
			tempSlot.UpdateComplete += delegate(UiControl {19716})
			{
				UiControl tempSlot = tempSlot;
				Marker pos = tempSlot.Pos;
				Vector2 vector = this.{19583}.Pos.XY + new Vector2({19563}, -7f);
				tempSlot.Pos = pos.SetXY(vector);
			};
			tempSlot.DisableDepthFocusTest = true;
			return tempSlot;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0005B428 File Offset: 0x00059628
		[CompilerGenerated]
		internal static void <UserFrontRender>g__DrawResourceAmount|76_0(int {19565}, bool {19566} = true, ref {19548}.<>c__DisplayClass76_0 {19567})
		{
			ResourceInfo resourceInfo = Gameplay.ItemsInfo.FromID({19565});
			int count = Global.Player.ResourcesOfHold.GetCount((int)resourceInfo.ID);
			{19548}.<UserFrontRender>g__DrawResourceAmountNoId|76_1(resourceInfo.IconTexture, resourceInfo.IconTexture.Bounds, count.ToString(), (count == 0) ? Color.OrangeRed : Color.Wheat, {19566}, ref {19567});
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0005B488 File Offset: 0x00059688
		[CompilerGenerated]
		internal static void <UserFrontRender>g__DrawResourceAmountNoId|76_1(Texture2D {19568}, Rectangle {19569}, string {19570}, Color {19571}, bool {19572} = true, ref {19548}.<>c__DisplayClass76_0 {19573})
		{
			int num = 18;
			Vector2 vector = {19572} ? ({19573}.p + new Vector2((float)(num + 3), (float)num / 2f - 8f)) : {19573}.p;
			Vector2 vector2 = {19572} ? {19573}.p : ({19573}.p + new Vector2((float)(num + 60), (float)num / 2f - 8f));
			Device gs = Engine.GS;
			Rectangle rectangle = new Marker(ref vector2, (float)num, (float)num).ToRect();
			Color white = Color.White;
			gs.DrawCustomTexture({19568}, {19569}, rectangle, white);
			Engine.GS.SetFont(Fonts.Arial_12);
			Engine.GS.DrawStringShadowed({19570}, vector, {19571});
			{19573}.p.Y = {19573}.p.Y - (float)(num + 1);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0005B55A File Offset: 0x0005975A
		[CompilerGenerated]
		private void {19574}()
		{
			this.{19582}.RemoveFromContainer();
			this.{19582} = null;
		}

		// Token: 0x04000A2B RID: 2603
		internal static int[] cannonAmmoOrder = new int[]
		{
			1,
			2,
			3,
			4,
			7,
			5,
			21
		};

		// Token: 0x04000A2C RID: 2604
		private static {19548} currentInastance;

		// Token: 0x04000A2D RID: 2605
		public bool MortarSightByPowerupItem;

		// Token: 0x04000A2E RID: 2606
		private static readonly Rectangle[] c_bt_hold = new Rectangle[]
		{
			new Rectangle(2585, 447, 57, 57),
			new Rectangle(2585, 505, 57, 57),
			new Rectangle(2527, 505, 57, 57),
			new Rectangle(2527, 447, 57, 57)
		};

		// Token: 0x04000A2F RID: 2607
		private static readonly Rectangle overloadedHold = new Rectangle(2381, 505, 57, 57);

		// Token: 0x04000A30 RID: 2608
		private static readonly Rectangle c_holdProgress = new Rectangle(2309, 510, 13, 52);

		// Token: 0x04000A31 RID: 2609
		private static readonly Rectangle c_bt_PowerupItem = new Rectangle(2540, 390, 44, 56);

		// Token: 0x04000A32 RID: 2610
		private static readonly Rectangle c_bt_PowerupItemWithoutQuantity = new Rectangle(2495, 390, 44, 56);

		// Token: 0x04000A33 RID: 2611
		private static readonly Rectangle c_bt_PowerupItem_iconPos = new Rectangle(3, 14, 39, 39);

		// Token: 0x04000A34 RID: 2612
		private static readonly Rectangle c_PowerupItem_active = new Rectangle(2482, 505, 44, 56);

		// Token: 0x04000A35 RID: 2613
		private static readonly Rectangle c_bt_weapons_1 = new Rectangle(2585, 389, 57, 57);

		// Token: 0x04000A36 RID: 2614
		private static readonly Rectangle c_bt_weapons_2 = new Rectangle(2643, 447, 57, 57);

		// Token: 0x04000A37 RID: 2615
		private static readonly Rectangle c_bt_weapon_countMask = new Rectangle(2527, 563, 57, 57);

		// Token: 0x04000A38 RID: 2616
		private static readonly Rectangle c_bt_weapons_iconPos = new Rectangle(8, 8, 41, 41);

		// Token: 0x04000A39 RID: 2617
		private static readonly Rectangle c_bt_mending = new Rectangle(2631, 609, 57, 69);

		// Token: 0x04000A3A RID: 2618
		private static readonly Rectangle c_bt_mending_disabled = new Rectangle(2689, 609, 57, 69);

		// Token: 0x04000A3B RID: 2619
		private static readonly Rectangle c_bt_mending_redAura = new Rectangle(2751, 636, 57, 57);

		// Token: 0x04000A3C RID: 2620
		private static readonly Rectangle c_bt_ice = new Rectangle(2786, 587, 45, 45);

		// Token: 0x04000A3D RID: 2621
		private static readonly Rectangle c_bt_ice_mask = new Rectangle(2832, 587, 45, 45);

		// Token: 0x04000A3E RID: 2622
		private static readonly Rectangle c_bt_button_mask = new Rectangle(2643, 543, 19, 19);

		// Token: 0x04000A3F RID: 2623
		private static readonly Rectangle c_bt_emptysmall = new Rectangle(2585, 563, 45, 45);

		// Token: 0x04000A40 RID: 2624
		private static readonly Rectangle c_bt_emptysmallCountMarker = new Rectangle(2585, 651, 45, 45);

		// Token: 0x04000A41 RID: 2625
		private static readonly Rectangle c_bt_emptysmallInsideProgress = new Rectangle(2586, 609, 41, 41);

		// Token: 0x04000A42 RID: 2626
		private static readonly Rectangle c_btnotif_warModeOpenWorld = new Rectangle(2585, 563, 45, 45);

		// Token: 0x04000A43 RID: 2627
		private static readonly Rectangle c_notif_fence = new Rectangle(2631, 563, 45, 45);

		// Token: 0x04000A44 RID: 2628
		private static readonly Rectangle c_notif_corpus = new Rectangle(2677, 563, 45, 45);

		// Token: 0x04000A45 RID: 2629
		private static readonly Rectangle c_notif_shallow = new Rectangle(2677, 517, 45, 45);

		// Token: 0x04000A46 RID: 2630
		private static readonly Rectangle sprite = new Rectangle(1210, 740, 44, 44);

		// Token: 0x04000A47 RID: 2631
		private static readonly Rectangle c_useCountProgress_yellow = new Rectangle(2810, 388, 40, 9);

		// Token: 0x04000A48 RID: 2632
		private static readonly Rectangle c_useCountProgress_black = new Rectangle(2851, 388, 40, 9);

		// Token: 0x04000A49 RID: 2633
		private static readonly Rectangle c_extraPowerupItemTimeout = new Rectangle(2447, 592, 67, 67);

		// Token: 0x04000A4A RID: 2634
		private static readonly Rectangle c_cannonBallHl = new Rectangle(2793, 535, 126, 51);

		// Token: 0x04000A4B RID: 2635
		private static readonly Rectangle c_speed_Anim = new Rectangle(2740, 1063, 260, 71);

		// Token: 0x04000A4C RID: 2636
		private static readonly Rectangle c_speed_3 = new Rectangle(2775, 1351, 260, 71);

		// Token: 0x04000A4D RID: 2637
		private static readonly Rectangle c_speed_2 = new Rectangle(2775, 1279, 260, 71);

		// Token: 0x04000A4E RID: 2638
		private static readonly Rectangle c_speed_1 = new Rectangle(2740, 1207, 260, 71);

		// Token: 0x04000A4F RID: 2639
		private static readonly Rectangle c_speed_Back = new Rectangle(2740, 1135, 260, 71);

		// Token: 0x04000A50 RID: 2640
		private static Marker mendingButtonArea = new Marker(0f, 12f, 57f, 57f);

		// Token: 0x04000A51 RID: 2641
		private {19548}.SimpleButton {19575};

		// Token: 0x04000A52 RID: 2642
		private {19548}.SimpleButton {19576};

		// Token: 0x04000A53 RID: 2643
		private Tlist<{19548}.PowerupItemItem> {19577};

		// Token: 0x04000A54 RID: 2644
		private {19548}.FalkonetBallItem {19578};

		// Token: 0x04000A55 RID: 2645
		private {19548}.PowderKegItem {19579};

		// Token: 0x04000A56 RID: 2646
		private {19548}.MortarBallItem {19580};

		// Token: 0x04000A57 RID: 2647
		private Sprite {19581};

		// Token: 0x04000A58 RID: 2648
		private StackForm {19582};

		// Token: 0x04000A59 RID: 2649
		private StackForm {19583};

		// Token: 0x04000A5A RID: 2650
		private {19548}.CenterClass {19584};

		// Token: 0x04000A5B RID: 2651
		private GSI {19585};

		// Token: 0x04000A5C RID: 2652
		internal float PowerupItemResponseTimeoutMs;

		// Token: 0x04000A5D RID: 2653
		private float {19586};

		// Token: 0x04000A5E RID: 2654
		private {19548}.FoodItem {19587};

		// Token: 0x04000A5F RID: 2655
		private Form {19588};

		// Token: 0x04000A60 RID: 2656
		private int {19589};

		// Token: 0x04000A61 RID: 2657
		private float {19590};

		// Token: 0x04000A62 RID: 2658
		public bool ShowHoldButton = true;

		// Token: 0x04000A63 RID: 2659
		public bool ShowAmmoAndReload = true;

		// Token: 0x04000A64 RID: 2660
		public bool ShowMendingButton = true;

		// Token: 0x04000A65 RID: 2661
		public bool ShowFalkonets = true;

		// Token: 0x04000A66 RID: 2662
		public bool ShowKegs = true;

		// Token: 0x04000A67 RID: 2663
		private int {19591};

		// Token: 0x04000A68 RID: 2664
		private static Label selectedItemName;

		// Token: 0x020001F4 RID: 500
		private abstract class ItemBase : CustomUi
		{
			// Token: 0x14000008 RID: 8
			// (add) Token: 0x06000B73 RID: 2931 RVA: 0x0005B570 File Offset: 0x00059770
			// (remove) Token: 0x06000B74 RID: 2932 RVA: 0x0005B5A8 File Offset: 0x000597A8
			public event Action Apply
			{
				[CompilerGenerated]
				add
				{
					Action action = this.{19612};
					Action action2;
					do
					{
						action2 = action;
						Action value2 = (Action)Delegate.Combine(action2, value);
						action = Interlocked.CompareExchange<Action>(ref this.{19612}, value2, action2);
					}
					while (action != action2);
				}
				[CompilerGenerated]
				remove
				{
					Action action = this.{19612};
					Action action2;
					do
					{
						action2 = action;
						Action value2 = (Action)Delegate.Remove(action2, value);
						action = Interlocked.CompareExchange<Action>(ref this.{19612}, value2, action2);
					}
					while (action != action2);
				}
			}

			// Token: 0x06000B75 RID: 2933 RVA: 0x0005B5E0 File Offset: 0x000597E0
			public ItemBase({19548} {19600}, Rectangle {19601}, Marker {19602}, Texture2D {19603}, Rectangle {19604}, KeynameHolder {19605})
			{
				Vector2 zero = Vector2.Zero;
				base..ctor(new Marker(ref zero, ref {19601}), {19601}, PositionAlignment.Center, PositionAlignment.RightDown, Color.White, false);
				this.parent = {19600};
				this.iconTexture = {19603};
				this.iconTexturePath = {19604};
				this.iconPosition = {19602};
				this.iconColor = Color.White;
				this.key = {19605};
				this.AnimatedFocus = false;
				base.EvClick += this.{19607};
				base.ToolTip = new ToolTip(new Func<UiControl, ToolTipState>(this.{19609}));
				this.Apply += this.{19611};
			}

			// Token: 0x06000B76 RID: 2934 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserBackRender()
			{
			}

			// Token: 0x06000B77 RID: 2935 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserFrontRender()
			{
			}

			// Token: 0x06000B78 RID: 2936 RVA: 0x0005B67E File Offset: 0x0005987E
			protected override void UserUpdate(ref FrameTime {19606})
			{
				if (this.{19612} != null && this.key != null && this.key.IsClick && GameScene.GameHasInputFocus && !TextBox.IsThereInput && !KeyInputControl.IsInputElements)
				{
					this.{19612}();
				}
			}

			// Token: 0x06000B79 RID: 2937 RVA: 0x0005B6C0 File Offset: 0x000598C0
			public virtual void RenderIcon()
			{
				if (!base.IsVisible)
				{
					return;
				}
				if (this.iconTexturePath.Width != 0 && this.iconTexture != null)
				{
					Engine.GS.SetTexture(this.iconTexture);
					Device gs = Engine.GS;
					Rectangle rectangle = this.iconPosition.Offset(base.Pos.XY).ToRect();
					Color color = UiControl.ComputeColor(base.GetBrightness(), base.GetOpcaity(), this.iconColor);
					gs.Draw(this.iconTexturePath, rectangle, color);
				}
			}

			// Token: 0x06000B7A RID: 2938 RVA: 0x0005B74C File Offset: 0x0005994C
			public virtual void RenderFront()
			{
				if (!base.IsVisible)
				{
					return;
				}
				if (this.key != null)
				{
					float opcaity = base.GetOpcaity();
					Device gs = Engine.GS;
					Vector2 vector = new Vector2(base.Pos.XY.X, base.Pos.XY.Y + base.Pos.WH.Y - (float){19548}.c_bt_button_mask.Height);
					Color color = Color.White * opcaity;
					gs.Draw({19548}.c_bt_button_mask, vector, color);
					Engine.GS.SetFont(Fonts.F_m14_ThinBold);
					Device gs2 = Engine.GS;
					string keyToString = this.key.KeyToString;
					vector = base.Pos.XY + new Vector2(5f, base.Pos.WH.Y - 21f);
					color = Color.Wheat * (0.8f * opcaity);
					gs2.DrawString(keyToString, vector, color);
				}
			}

			// Token: 0x06000B7B RID: 2939 RVA: 0x0005B841 File Offset: 0x00059A41
			[CompilerGenerated]
			private void {19607}(ClickUiEventArgs {19608})
			{
				Action action = this.{19612};
				if (action == null)
				{
					return;
				}
				action();
			}

			// Token: 0x06000B7C RID: 2940 RVA: 0x0005B853 File Offset: 0x00059A53
			[CompilerGenerated]
			private ToolTipState {19609}(UiControl {19610})
			{
				Func<ToolTipState> showToolTip = this.ShowToolTip;
				return ((showToolTip != null) ? showToolTip() : null) ?? new ToolTipState("", "", Array.Empty<ToolTipCharacteristics>());
			}

			// Token: 0x06000B7D RID: 2941 RVA: 0x0005B87F File Offset: 0x00059A7F
			[CompilerGenerated]
			private void {19611}()
			{
				if ({19548}.currentInastance == null || !{19548}.currentInastance.IsVisible)
				{
					return;
				}
				base.RemoveAnimations();
				new UiBrightnessAnimation(this, 2f, 100f);
				new UiBrightnessAnimation(this, 1f, 300f);
			}

			// Token: 0x04000A69 RID: 2665
			protected Func<ToolTipState> ShowToolTip;

			// Token: 0x04000A6A RID: 2666
			[CompilerGenerated]
			private Action {19612};

			// Token: 0x04000A6B RID: 2667
			protected Color iconColor;

			// Token: 0x04000A6C RID: 2668
			protected Texture2D iconTexture;

			// Token: 0x04000A6D RID: 2669
			protected Rectangle iconTexturePath;

			// Token: 0x04000A6E RID: 2670
			protected KeynameHolder key;

			// Token: 0x04000A6F RID: 2671
			protected readonly {19548} parent;

			// Token: 0x04000A70 RID: 2672
			protected Marker iconPosition;
		}

		// Token: 0x020001F5 RID: 501
		private class SimpleButton : {19548}.ItemBase
		{
			// Token: 0x06000B7E RID: 2942 RVA: 0x0005B8C0 File Offset: 0x00059AC0
			public SimpleButton({19548} {19616}, KeynameHolder {19617}, Rectangle {19618}) : base({19616}, {19618}, Marker.Zero, null, default(Rectangle), {19617})
			{
				this.ShowToolTip = new Func<ToolTipState>(this.{19619});
			}

			// Token: 0x06000B7F RID: 2943 RVA: 0x0005B8F7 File Offset: 0x00059AF7
			[CompilerGenerated]
			private ToolTipState {19619}()
			{
				return this.ToolTip ?? new ToolTipState(null);
			}

			// Token: 0x04000A71 RID: 2673
			public new ToolTipState ToolTip;
		}

		// Token: 0x020001F6 RID: 502
		private class PowerupItemItem : {19548}.ItemBase
		{
			// Token: 0x06000B80 RID: 2944 RVA: 0x0005B90C File Offset: 0x00059B0C
			public PowerupItemItem({19548} {19624}, int {19625}, PowerupItemInfo {19626}, KeynameHolder {19627}) : base({19624}, PowerupItemsGenerator.SkillsIds.Contains(({19626} != null) ? {19626}.Index : 0) ? {19548}.c_bt_PowerupItemWithoutQuantity : {19548}.c_bt_PowerupItem, new Marker(ref {19548}.c_bt_PowerupItem_iconPos), {19626}.Icon, {19626}.Icon.Bounds, {19627})
			{
				{19548}.PowerupItemItem <>4__this = this;
				this.itemInfo = {19626};
				this.{19631} = {19625};
				base.Apply += delegate()
				{
					if (Global.Player.IsDestroyed || !<>4__this.IsVisible || {19624}.PowerupItemResponseTimeoutMs > 0f || Global.Player.IsInBoarding)
					{
						return;
					}
					if ({19625} == 255)
					{
						if (Session.Account.PowerupItemExtraSlot != 255 && Session.CurrentCrewJob == null)
						{
							<>4__this.{19628}(false);
						}
						return;
					}
					PowerupItemReloadCache powerupItemStatusFull = Session.Account.GetPowerupItemStatusFull(<>4__this.itemInfo.Index, false);
					if (<>4__this.{19637} == PowerupItemStatus.Activated && <>4__this.itemInfo.AllowInterrupt && <>4__this.itemInfo.WorkTime.Value - powerupItemStatusFull.TimeLeftSec > 3)
					{
						<>4__this.{19628}(true);
						return;
					}
					if (<>4__this.{19637} == PowerupItemStatus.Ready && <>4__this.{19632} != 0)
					{
						if (Session.CurrentCrewJob != null || Global.Player.UsedShip.FirstHP.FloodingFactor > 0f)
						{
							return;
						}
						if (Session.CurrentArenaSession != null && Global.Player.MapInfo.IsEnableArenaUi && Session.CurrentArenaSession.PowerupItemUseLimit[{19625}] <= 0)
						{
							return;
						}
						<>4__this.{19628}(false);
					}
				};
				this.ShowToolTip = delegate()
				{
					if (<>4__this.itemInfo != null)
					{
						return <>4__this.itemInfo.ToolTip(!PowerupItemsGenerator.SkillsIds.Contains(<>4__this.itemInfo.Index), Array.Empty<ToolTipCharacteristics>());
					}
					return new ToolTipState(null);
				};
				this.{19634} = new Sprite({19548}.sprite, 10, 1000f, 0);
				this.{19634}.IsLoop = true;
				this.{19637} = (({19625} == 255) ? PowerupItemStatus.Ready : Session.Account.GetPowerupItemStatus(this.itemInfo.Index, false));
			}

			// Token: 0x06000B81 RID: 2945 RVA: 0x0005BA08 File Offset: 0x00059C08
			private void {19628}(bool {19629})
			{
				if (this.itemInfo.ServerEffect == PowerupItemServerEffect.RunMortar)
				{
					this.parent.MortarSightByPowerupItem = true;
					return;
				}
				Global.Network.Send(new OnLocPowerupItemUse((byte)this.itemInfo.Index, (byte)this.{19631}, {19629}, Global.Game.InterestPoints.ShipInSightUidOrZero, Vector3.Zero));
				this.parent.PowerupItemResponseTimeoutMs = 250f;
			}

			// Token: 0x06000B82 RID: 2946 RVA: 0x0005BA7C File Offset: 0x00059C7C
			protected override void UserUpdate(ref FrameTime {19630})
			{
				bool flag = this.itemInfo != null && PowerupItemsGenerator.SkillsIds.Contains(this.itemInfo.Index);
				if (this.{19631} == 255)
				{
					this.itemInfo = ((Session.Account.PowerupItemExtraSlot == byte.MaxValue) ? null : Gameplay.PowerupItems.Array[(int)Session.Account.PowerupItemExtraSlot]);
					base.IsVisible = (this.itemInfo != null);
					this.{19632} = (Global.Player.UsedShip.ExtraItemHasTimeout ? ((int)Math.Ceiling((double)(Session.Account.PowerupItemExtraSlotTimeoutMs / 1000f))) : 0);
					this.{19633} = (Session.Account.PowerupItemExtraSlotTimeoutMs < 15000f);
					this.{19637} = PowerupItemStatus.Ready;
					if (this.itemInfo != null)
					{
						this.iconTexture = this.itemInfo.Icon;
						this.iconTexturePath = this.itemInfo.Icon.Bounds;
					}
				}
				else
				{
					PowerupItemStatus powerupItemStatus = Session.Account.GetPowerupItemStatus(this.itemInfo.Index, false);
					if (powerupItemStatus != PowerupItemStatus.Ready)
					{
						this.iconColor = Color.White * 0.4f;
					}
					else
					{
						this.iconColor = Color.White * 0.9f;
					}
					this.{19636} = (powerupItemStatus == PowerupItemStatus.Activated && this.itemInfo.AllowInterrupt);
					this.{19635} = (powerupItemStatus == PowerupItemStatus.Activated);
					if (this.{19637} == PowerupItemStatus.Activated && powerupItemStatus == PowerupItemStatus.Cooldown)
					{
						Global.Game.SoundSystem.PlaySound(GameStaticSoundName.ItemComplete, 0.03f, 1f);
					}
					this.{19637} = powerupItemStatus;
					if (this.{19635})
					{
						this.{19634}.Update(ref {19630});
					}
					this.{19632} = (flag ? int.MaxValue : Session.Account.PowerupItemsAtStorage[this.itemInfo.Index]);
				}
				if (flag)
				{
					base.IsVisible = Global.Player.CanUseShipSkill;
					if (!base.IsVisible && this.{19637} == PowerupItemStatus.Activated)
					{
						this.{19628}(true);
					}
				}
				if (this.itemInfo != null && this.parent.MortarSightByPowerupItem && this.itemInfo.ServerEffect == PowerupItemServerEffect.RunMortar && {19548}.currentInastance != null && this.key.IsRelease)
				{
					Global.Network.Send(new OnLocPowerupItemUse((byte)this.itemInfo.Index, (byte)this.{19631}, false, Global.Game.InterestPoints.ShipInSightUidOrZero, Session.MortarShotParamPowerupItem));
					this.parent.PowerupItemResponseTimeoutMs = 250f;
				}
				base.UserUpdate(ref {19630});
			}

			// Token: 0x06000B83 RID: 2947 RVA: 0x0005BD08 File Offset: 0x00059F08
			protected override void UserBackRender()
			{
				if (!base.IsVisible)
				{
					return;
				}
				base.UserBackRender();
			}

			// Token: 0x06000B84 RID: 2948 RVA: 0x0005BD1C File Offset: 0x00059F1C
			protected override void UserFrontRender()
			{
				if (!base.IsVisible)
				{
					return;
				}
				if (this.{19636})
				{
					Device gs = Engine.GS;
					Rectangle rectangle = base.Pos.ToRect();
					gs.Draw({19548}.c_PowerupItem_active, rectangle);
				}
				if (this.{19635})
				{
					this.{19634}.Render(new Marker(base.Pos.XY.X, base.Pos.XY.Y + 10f, base.Pos.WH.X, base.Pos.WH.Y - 10f));
				}
				if (this.{19637} == PowerupItemStatus.Ready && this.itemInfo.ServerEffect == PowerupItemServerEffect.CaptureNearNpc)
				{
					ShipNpc shipNpc = Global.Game.InterestPoints.ShipInSight as ShipNpc;
					int num;
					if (shipNpc != null && PowerupItemsGenerator.CanNpcBeCapturedByPowerupItem(shipNpc, Global.Player, out num))
					{
						Device gs2 = Engine.GS;
						Rectangle rectangle = new Rectangle(2585, 698, 41, 41);
						Rectangle rectangle2 = new Marker(base.Pos.XY.X, base.Pos.XY.Y + 10f, base.Pos.WH.X, base.Pos.WH.Y - 10f).ToRect();
						gs2.Draw(rectangle, rectangle2);
						Engine.GS.SetFont(Fonts.Arial_10Bold);
						Device gs3 = Engine.GS;
						string {14602} = "-" + num.ToString() + " " + Local.gold2;
						Vector2 vector = base.Pos.XY - new Vector2(0f, 25f);
						Color color = Color.LightGreen;
						gs3.DrawStringShadowed({14602}, vector, color);
					}
				}
				base.UserFrontRender();
				if (Session.CurrentArenaSession != null && Global.Player.MapInfo.IsEnableArenaUi && this.{19631} != 255)
				{
					Vector2 value = new Vector2(1f, -9f);
					int num2 = Math.Max(0, Session.CurrentArenaSession.PowerupItemUseLimit[this.{19631}]);
					if (num2 <= 4)
					{
						int num3 = 4 - num2;
						Device gs4 = Engine.GS;
						Rectangle rectangle = new Rectangle({19548}.c_useCountProgress_yellow.X, {19548}.c_useCountProgress_yellow.Y, num2 * 10, {19548}.c_useCountProgress_yellow.Height);
						Vector2 vector = base.Pos.XY + value;
						gs4.Draw(rectangle, vector);
						Device gs5 = Engine.GS;
						rectangle = new Rectangle({19548}.c_useCountProgress_black.X, {19548}.c_useCountProgress_black.Y, num3 * 10, {19548}.c_useCountProgress_black.Height);
						vector = base.Pos.XY + value + new Vector2((float)(num2 * 10), 0f);
						gs5.Draw(rectangle, vector);
					}
				}
				if (this.{19637} == PowerupItemStatus.Activated)
				{
					if (this.itemInfo.CommonEffects.Any((ShipBonus {19638}) => {19638}.Type == ShipBonusEffect.BAvanpostMode) && Session.AvanpostModeRemainRespawns != 0)
					{
						Engine.GS.SetFont(Fonts.Arial_10Bold);
						Device gs6 = Engine.GS;
						string {14599} = Session.AvanpostModeRemainRespawns.ToString();
						Vector2 vector = base.Pos.End - new Vector2(11f, 13f);
						Color color = Color.Cyan;
						gs6.DrawString({14599}, vector, color);
					}
				}
			}

			// Token: 0x06000B85 RID: 2949 RVA: 0x0005C094 File Offset: 0x0005A294
			public void DrawText()
			{
				if (base.IsVisible)
				{
					float opcaity = base.GetOpcaity();
					Engine.GS.SetFont(Fonts.Arial_10Bold);
					Device gs = Engine.GS;
					string {14599} = (this.{19632} == int.MaxValue) ? "∞" : this.{19632}.ToString();
					Vector2 vector = base.Pos.XY + new Vector2(3f, -1f);
					Color color = (this.{19633} ? Color.Orange : Color.Gray) * opcaity;
					gs.DrawString({14599}, vector, color);
					if (this.{19637} != PowerupItemStatus.Ready)
					{
						PowerupItemReloadCache powerupItemStatusFull = Session.Account.GetPowerupItemStatusFull(this.itemInfo.Index, false);
						if (powerupItemStatusFull.Status != PowerupItemStatus.Ready)
						{
							Engine.GS.SetFont(Fonts.Philosopher_16Bold);
							Device gs2 = Engine.GS;
							string {14610} = powerupItemStatusFull.TimeLeftSec.ToString() ?? "";
							vector = base.Pos.Center;
							color = ((powerupItemStatusFull.Status == PowerupItemStatus.Activated) ? (new Color(159, 255, 138) * 1.1f) : Color.White) * opcaity;
							gs2.DrawStringCentered({14610}, vector, color);
						}
					}
				}
			}

			// Token: 0x04000A72 RID: 2674
			internal PowerupItemInfo itemInfo;

			// Token: 0x04000A73 RID: 2675
			private readonly int {19631};

			// Token: 0x04000A74 RID: 2676
			private int {19632};

			// Token: 0x04000A75 RID: 2677
			private bool {19633};

			// Token: 0x04000A76 RID: 2678
			private Sprite {19634};

			// Token: 0x04000A77 RID: 2679
			private bool {19635};

			// Token: 0x04000A78 RID: 2680
			private bool {19636};

			// Token: 0x04000A79 RID: 2681
			private PowerupItemStatus {19637};
		}

		// Token: 0x020001F9 RID: 505
		private abstract class WeaponItem : {19548}.ItemBase
		{
			// Token: 0x06000B8C RID: 2956 RVA: 0x0005C397 File Offset: 0x0005A597
			public WeaponItem({19548} {19642}, KeynameHolder {19643}, Rectangle {19644}) : base({19642}, {19644}, new Marker(ref {19548}.c_bt_weapons_iconPos), Gameplay.PowderKegsInfo.FromID(Global.Settings.SelectedPowderKegs).IconTexture, Rectangle.Empty, {19643})
			{
			}

			// Token: 0x06000B8D RID: 2957 RVA: 0x0005C3CC File Offset: 0x0005A5CC
			public void Load(Texture2D {19645})
			{
				this.iconTexture = {19645};
				this.iconTexturePath = {19645}.Bounds;
				if (base.ToolTip != null)
				{
					base.ToolTip.RefreshIfIsOpen();
				}
				this.parent.PrepareSwitchForm(base.Pos.XY, new Func<string>(this.LoadSwitchForm));
			}

			// Token: 0x06000B8E RID: 2958
			protected abstract string LoadSwitchForm();

			// Token: 0x06000B8F RID: 2959
			protected abstract int getCount();

			// Token: 0x06000B90 RID: 2960 RVA: 0x0005C424 File Offset: 0x0005A624
			public override void RenderFront()
			{
				base.RenderFront();
				float opcaity = base.GetOpcaity();
				int count = this.getCount();
				Device gs = Engine.GS;
				Rectangle rectangle = base.Pos.ToRect();
				Color color = Color.White * opcaity;
				gs.Draw({19548}.c_bt_weapon_countMask, rectangle, color);
				Engine.GS.SetFont(Fonts.Philosopher_14Bold);
				Device gs2 = Engine.GS;
				string {14610} = (count == int.MaxValue) ? "∞" : count.ToString();
				Vector2 vector = base.Pos.XY + new Vector2(28f, 40f);
				color = new Color(193, 193, 193) * opcaity;
				gs2.DrawStringCentered({14610}, vector, color);
			}

			// Token: 0x06000B91 RID: 2961 RVA: 0x0005C4E4 File Offset: 0x0005A6E4
			protected void DrawReload(float {19646}, float {19647})
			{
				this.iconColor = (({19646} == 0f) ? Color.White : (Color.White * 0.5f));
				if (this.{19651} && {19646} == 0f)
				{
					new UiBrightnessAnimation(this, 4f, 100f);
					new UiBrightnessAnimation(this, 1f, 500f);
				}
				this.{19651} = ({19646} > 0f);
				if ({19646} == 0f)
				{
					return;
				}
				float num = 1f - {19646} / {19647};
				int num2 = Math.Min(AtlasGameGui.circleProgress.Length - 1, Math.Max((int)(num * (float)AtlasGameGui.circleProgress.Length), 0));
				Device gs = Engine.GS;
				Rectangle[] circleProgress = AtlasGameGui.circleProgress;
				int num3 = num2;
				Vector2 vector = base.Pos.XY + new Vector2(4f, 5f);
				Color color = Color.White * base.Opacity;
				gs.Draw(circleProgress[num3], vector, color);
				Engine.GS.SetFont(Fonts.Philosopher_16Bold);
				string text = ((int)Math.Ceiling((double)({19646} / 1000f))).ToString();
				Device gs2 = Engine.GS;
				string {14610} = text;
				vector = base.Pos.XY + new Vector2(28f, 22f);
				color = Color.White * 0.8f;
				gs2.DrawStringCentered({14610}, vector, color);
			}

			// Token: 0x06000B92 RID: 2962 RVA: 0x0005C63C File Offset: 0x0005A83C
			protected void DrawReloadHalf(bool {19648}, float {19649}, float {19650})
			{
				if ({19649} == 0f)
				{
					return;
				}
				float num = (1f - {19649} / {19650}) * 0.5f;
				int num2 = Math.Min(AtlasGameGui.circleProgress.Length - 1, Math.Max((int)(num * (float)AtlasGameGui.circleProgress.Length), 0));
				Vector2 vector = new Vector2((float)AtlasGameGui.circleProgress[num2].Width, (float)AtlasGameGui.circleProgress[num2].Height) * 0.5f;
				Device gs = Engine.GS;
				Rectangle[] circleProgress = AtlasGameGui.circleProgress;
				int num3 = num2;
				Vector2 vector2 = base.Pos.XY + new Vector2(4f, 5f) + vector;
				Vector2 {14552} = vector;
				float {14553} = {19648} ? -1.5707964f : 1.5707964f;
				Color color = Color.White * base.Opacity;
				gs.Draw(circleProgress[num3], vector2, {14552}, {14553}, color);
			}

			// Token: 0x04000A7F RID: 2687
			private bool {19651};
		}

		// Token: 0x020001FA RID: 506
		private class PowderKegItem : {19548}.WeaponItem
		{
			// Token: 0x06000B93 RID: 2963 RVA: 0x0005C718 File Offset: 0x0005A918
			public PowderKegItem({19548} {19655}, KeynameHolder {19656}, Rectangle {19657}) : base({19655}, {19656}, {19657})
			{
				this.{19661} = Session.SelectedPowderKegsInfo;
				this.Load(this.{19661});
				base.Apply += this.{19659};
				this.ShowToolTip = new Func<ToolTipState>(this.{19660});
			}

			// Token: 0x06000B94 RID: 2964 RVA: 0x0005C76C File Offset: 0x0005A96C
			public void Load(PowderKegInfo {19658})
			{
				Global.Settings.SelectedPowderKegs = (int){19658}.ID;
				this.{19661} = {19658};
				base.Load({19658}.IconTextureCircular);
				this.parent.{19555}(base.Pos.XY, {19658}.Name);
			}

			// Token: 0x06000B95 RID: 2965 RVA: 0x0005C7B8 File Offset: 0x0005A9B8
			protected override string LoadSwitchForm()
			{
				for (int i = 0; i < Gameplay.PowderKegsInfo.Count; i++)
				{
					PowderKegInfo item = Gameplay.PowderKegsInfo.FromIndex(i);
					if (!(item.Name == "removed"))
					{
						Image image = new Image(new Marker(0f, 0f, 56f, 56f), item.IconTexture, item.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							BasicColor = ((item.ID == this.{19661}.ID) ? Color.White : (Color.White * 0.3f))
						};
						image.EvClick += delegate(ClickUiEventArgs {19662})
						{
							this.Load(item);
						};
						image.DisableDepthFocusTest = true;
						int count = Global.Player.UsedShipPlayer.PowderKegsOfHold.GetCount((int)item.ID);
						image.AddChild(new Label(image.Pos.XY + new Vector2(3f, 3f), Fonts.F_m14_ThinBold, Color.White, (count == 0) ? "--" : count.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
						{20431}.Set(image, item, 0, null);
						this.parent.{19582}.AddItem(new UiControl[]
						{
							image
						});
					}
				}
				return "[" + Global.Settings.kb_ThrowPowderKeg.KeyToString + Local.ItemPanelGui_0;
			}

			// Token: 0x06000B96 RID: 2966 RVA: 0x0005C94E File Offset: 0x0005AB4E
			protected override int getCount()
			{
				return Global.Player.UsedShipPlayer.PowderKegsOfHold.GetCount((int)this.{19661}.ID);
			}

			// Token: 0x06000B97 RID: 2967 RVA: 0x0005C970 File Offset: 0x0005AB70
			public override void RenderFront()
			{
				if (!base.IsVisible)
				{
					return;
				}
				base.DrawReload(Session.WReload.PowderKegsReloadingLeftSec * 1000f, Session.WReload.PowderKegsReloadingMaxSec * 1000f);
				base.RenderFront();
				if (this.{19661}.isFirebrand && InGameSightUi.PowderKegSights.AllowToRunFirebrand)
				{
					Device gs = Engine.GS;
					Rectangle rectangle = new Rectangle(2586, 609, 41, 41);
					Rectangle rectangle2 = base.Pos.ToRect();
					gs.Draw(rectangle, rectangle2);
				}
			}

			// Token: 0x06000B98 RID: 2968 RVA: 0x0005C9FC File Offset: 0x0005ABFC
			[CompilerGenerated]
			private void {19659}()
			{
				Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UISwitch, 0.03f, 1f);
				this.Load(Gameplay.PowderKegsInfo.FromID({19548}.SwitchNextItemId(this.{19661})));
			}

			// Token: 0x06000B99 RID: 2969 RVA: 0x0005CA33 File Offset: 0x0005AC33
			[CompilerGenerated]
			private ToolTipState {19660}()
			{
				return {20431}.PreviewHandler(this.{19661}, false, false, null);
			}

			// Token: 0x04000A80 RID: 2688
			private PowderKegInfo {19661};
		}

		// Token: 0x020001FC RID: 508
		private class FalkonetBallItem : {19548}.WeaponItem
		{
			// Token: 0x06000B9C RID: 2972 RVA: 0x0005CA58 File Offset: 0x0005AC58
			public FalkonetBallItem({19548} {19666}, KeynameHolder {19667}, Rectangle {19668}) : base({19666}, {19667}, {19668})
			{
				this.{19673} = Gameplay.BallsInfo.FromID(Global.Settings.SelectedFalkonetsID);
				this.Load(this.{19673});
				base.Apply += this.{19671};
				this.ShowToolTip = new Func<ToolTipState>(this.{19672});
			}

			// Token: 0x06000B9D RID: 2973 RVA: 0x0005CAB8 File Offset: 0x0005ACB8
			public void Load(CannonBallInfo {19669})
			{
				Global.Settings.SelectedFalkonetsID = (int){19669}.ID;
				this.{19673} = {19669};
				base.Load({19669}.IconTextureCircular);
				this.parent.{19555}(base.Pos.XY, {19669}.Name);
			}

			// Token: 0x06000B9E RID: 2974 RVA: 0x0005CB04 File Offset: 0x0005AD04
			protected override void UserUpdate(ref FrameTime {19670})
			{
				if (Session.SelectedFalkonetsInfo.ID != this.{19673}.ID)
				{
					this.Load(Session.SelectedFalkonetsInfo);
				}
				if (Session.WReload.BoardingHookReloadingLeftSec > 0f)
				{
					CannonBallInfo cannonBallInfo = Gameplay.BallsInfo.FromID(17);
					this.iconTexture = cannonBallInfo.IconTextureCircular;
					this.iconTexturePath = cannonBallInfo.IconTextureCircular.Bounds;
				}
				else
				{
					CannonBallInfo cannonBallInfo2 = Gameplay.BallsInfo.FromID(Global.Settings.SelectedFalkonetsID);
					this.iconTexture = cannonBallInfo2.IconTextureCircular;
					this.iconTexturePath = cannonBallInfo2.IconTextureCircular.Bounds;
				}
				base.UserUpdate(ref {19670});
			}

			// Token: 0x06000B9F RID: 2975 RVA: 0x0005CBAC File Offset: 0x0005ADAC
			protected override string LoadSwitchForm()
			{
				CannonBallInfo[] array = (from {19674} in Gameplay.BallsInfo
				where {19674}.AmmoType == CannonAmmoType.FalkonetBall
				select {19674}).ToArray<CannonBallInfo>();
				for (int i = 0; i < array.Length; i++)
				{
					CannonBallInfo item = array[i];
					if (!(item.Name == "removed"))
					{
						Image image = new Image(new Marker(0f, 0f, 56f, 56f), item.IconTexture, item.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							BasicColor = ((item.ID == this.{19673}.ID) ? Color.White : (Color.White * 0.3f))
						};
						image.EvClick += delegate(ClickUiEventArgs {19675})
						{
							this.Load(item);
						};
						image.DisableDepthFocusTest = true;
						int num = item.Infinity ? int.MaxValue : Global.Player.UsedShipPlayer.BallsOfHold.GetCount((int)item.ID);
						image.AddChild(new Label(image.Pos.XY + new Vector2(3f, 3f), Fonts.F_m14_ThinBold, Color.White, (num == int.MaxValue) ? "∞" : ((num == 0) ? "--" : num.ToString()), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
						{20431}.Set(image, item, 0, null);
						this.parent.{19582}.AddItem(new UiControl[]
						{
							image
						});
					}
				}
				return "[" + Global.Settings.kb_Falkonet.KeyToString + Local.ItemPanelGui_1;
			}

			// Token: 0x06000BA0 RID: 2976 RVA: 0x0005CD88 File Offset: 0x0005AF88
			protected override int getCount()
			{
				if (!this.{19673}.Infinity)
				{
					return Global.Player.UsedShipPlayer.BallsOfHold.GetCount((int)this.{19673}.ID);
				}
				return int.MaxValue;
			}

			// Token: 0x06000BA1 RID: 2977 RVA: 0x0005CDBC File Offset: 0x0005AFBC
			public override void RenderFront()
			{
				if (!base.IsVisible)
				{
					return;
				}
				if (Session.WReload.BoardingHookReloadingLeftSec > 0f)
				{
					base.DrawReload(Session.WReload.BoardingHookReloadingLeftSec * 1000f, Session.WReload.BoardingHookReloadingMaxSec * 1000f);
				}
				else
				{
					base.DrawReload(Session.WReload.FalkonetsReloadingLeftSec * 1000f, Session.WReload.FalkonetsReloadingMaxSec * 1000f);
				}
				base.RenderFront();
			}

			// Token: 0x06000BA2 RID: 2978 RVA: 0x0005CE37 File Offset: 0x0005B037
			[CompilerGenerated]
			private void {19671}()
			{
				Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UISwitch, 0.03f, 1f);
				this.Load(Gameplay.BallsInfo.FromID({19548}.SwitchNextItemId(this.{19673})));
			}

			// Token: 0x06000BA3 RID: 2979 RVA: 0x0005CE6E File Offset: 0x0005B06E
			[CompilerGenerated]
			private ToolTipState {19672}()
			{
				return {20431}.PreviewHandler(this.{19673}, false, false, null);
			}

			// Token: 0x04000A83 RID: 2691
			private CannonBallInfo {19673};
		}

		// Token: 0x020001FF RID: 511
		private class MortarBallItem : {19548}.WeaponItem
		{
			// Token: 0x06000BA9 RID: 2985 RVA: 0x0005CEA8 File Offset: 0x0005B0A8
			public MortarBallItem({19548} {19679}, Vector2 {19680}, Rectangle {19681}) : base({19679}, null, {19681})
			{
				CannonGameInstance cannonGameInstance = Global.Player.UsedShip.Mortars.Iterate().FirstOrDefault<CannonGameInstance>();
				if (cannonGameInstance != null && cannonGameInstance.Info.Feature == CannonFeature.PowderKegMortar)
				{
					this.{19686} = true;
					base.Load(Gameplay.PowderKegsInfo.FromID(MortarShot.PowderKegMortarType).IconTextureCircular);
					this.ShowToolTip = (() => new ToolTipState(null));
					return;
				}
				this.{19685} = Gameplay.BallsInfo.FromID((int)Session.SelectedMortarBalls.ID);
				this.Load(this.{19685});
				base.Apply += this.{19683};
				this.ShowToolTip = new Func<ToolTipState>(this.{19684});
			}

			// Token: 0x06000BAA RID: 2986 RVA: 0x0005CF7A File Offset: 0x0005B17A
			public void Load(CannonBallInfo {19682})
			{
				if (this.{19686})
				{
					return;
				}
				this.{19685} = {19682};
				base.Load(this.{19685}.IconTextureCircular);
				Global.Settings.SelectedMortarBallsID = (int){19682}.ID;
			}

			// Token: 0x06000BAB RID: 2987 RVA: 0x0005CFB0 File Offset: 0x0005B1B0
			protected override int getCount()
			{
				if (this.{19686})
				{
					return Global.Player.UsedShipPlayer.PowderKegsOfHold.GetCount(MortarShot.PowderKegMortarType);
				}
				return Global.Player.UsedShipPlayer.BallsOfHold.GetCount((int)this.{19685}.ID);
			}

			// Token: 0x06000BAC RID: 2988 RVA: 0x0005D000 File Offset: 0x0005B200
			public override void RenderFront()
			{
				if (!base.IsVisible)
				{
					return;
				}
				if (!Global.Player.UsedShip.StaticInfo.MortarPorts.Any((CannonLocationInfo {19687}) => {19687}.Side == CannonLocation.InBack))
				{
					base.DrawReload(Session.WReload.RearMortarReloadingLeftSec * 1000f, Session.WReload.RearMortarReloadingMaxSec * 1000f);
				}
				else
				{
					this.iconColor = ((Session.WReload.RearMortarReloadingLeftSec != 0f || Session.WReload.BackMortarReloadingLeftSec != 0f) ? (Color.White * 0.5f) : Color.White);
					base.DrawReloadHalf(true, Session.WReload.RearMortarReloadingLeftSec * 1000f, Session.WReload.RearMortarReloadingMaxSec * 1000f);
					base.DrawReloadHalf(false, Session.WReload.BackMortarReloadingLeftSec * 1000f, Session.WReload.BackMortarReloadingMaxSec * 1000f);
				}
				if (Global.Player.UsedShip.Mortars.Iterate().Any((CannonGameInstance {19688}) => {19688}.Info.MortarNeedsPreparation))
				{
					float specialMortarPreparationLevel = InGameSightUi.MortarSights.SpecialMortarPreparationLevel;
					if (specialMortarPreparationLevel != 0f)
					{
						Vector2 value = HealthBarHelper.p_progress_back.WidthHeight() * 0.5f;
						Vector2 value2 = base.Pos.Center + new Vector2(0f, -40f);
						Device gs = Engine.GS;
						Vector2 vector = value2 - value / 2f;
						Rectangle rectangle = new Marker(ref vector, ref value).ToRect();
						Color color = Color.White * base.Opacity;
						gs.Draw(HealthBarHelper.p_progress_back, rectangle, color);
						Rectangle rectangle2 = (specialMortarPreparationLevel >= 1f) ? HealthBarHelper.p_progress_up_green : HealthBarHelper.p_progress_up_red;
						rectangle2.Width = (int)((float)rectangle2.Width * specialMortarPreparationLevel);
						Device gs2 = Engine.GS;
						vector = value2 - value / 2f;
						Vector2 vector2 = value * new Vector2(specialMortarPreparationLevel, 1f);
						rectangle = new Marker(ref vector, ref vector2).ToRect();
						color = Color.White * base.Opacity;
						gs2.Draw(rectangle2, rectangle, color);
					}
				}
				base.RenderFront();
			}

			// Token: 0x06000BAD RID: 2989 RVA: 0x0005D268 File Offset: 0x0005B468
			protected override string LoadSwitchForm()
			{
				if (this.{19686})
				{
					return string.Empty;
				}
				CannonBallInfo[] array = (from {19689} in Gameplay.BallsInfo
				where {19689}.AmmoType == CannonAmmoType.MortarBall
				select {19689}).ToArray<CannonBallInfo>();
				for (int i = 0; i < array.Length; i++)
				{
					CannonBallInfo item = array[i];
					if (!(item.Name == "removed"))
					{
						Image image = new Image(new Marker(0f, 0f, 56f, 56f), item.IconTexture, item.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							BasicColor = ((item.ID == this.{19685}.ID) ? Color.White : (Color.White * 0.3f))
						};
						image.EvClick += delegate(ClickUiEventArgs {19690})
						{
							Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UISwitch, 0.03f, 1f);
							this.Load(item);
						};
						image.DisableDepthFocusTest = true;
						{20431}.Set(image, item, 0, null);
						this.parent.{19582}.AddItem(new UiControl[]
						{
							image
						});
					}
				}
				return Local.ItemPanelGui_2(Global.Settings.kb_Mortar_ModifierKey.KeyToString, Global.Settings.kb_Mortar_Whole.KeyToString, Global.Settings.kb_Mortar_Splinter.KeyToString);
			}

			// Token: 0x06000BAE RID: 2990 RVA: 0x0005D3D9 File Offset: 0x0005B5D9
			[CompilerGenerated]
			private void {19683}()
			{
				this.Load(Gameplay.BallsInfo.FromID((Global.Settings.SelectedMortarBallsID == 6) ? 8 : 6));
			}

			// Token: 0x06000BAF RID: 2991 RVA: 0x0005D3FC File Offset: 0x0005B5FC
			[CompilerGenerated]
			private ToolTipState {19684}()
			{
				return {20431}.PreviewHandler(this.{19685}, false, false, null);
			}

			// Token: 0x04000A88 RID: 2696
			private CannonBallInfo {19685};

			// Token: 0x04000A89 RID: 2697
			private bool {19686};
		}

		// Token: 0x02000202 RID: 514
		private class FoodItem : {19548}.ItemBase
		{
			// Token: 0x06000BB8 RID: 3000 RVA: 0x0005D470 File Offset: 0x0005B670
			public FoodItem({19548} {19692}) : base({19692}, {19548}.c_bt_ice, Marker.Zero, null, default(Rectangle), null)
			{
				this.ShowToolTip = (() => new ToolTipState(Local.ingamett_foodConsumption_h, Local.gamewiki_storm_text3 + " " + Local.ingamett_foodConsumption, Array.Empty<ToolTipCharacteristics>()));
			}

			// Token: 0x06000BB9 RID: 3001 RVA: 0x0005D4C0 File Offset: 0x0005B6C0
			protected override void UserUpdate(ref FrameTime {19693})
			{
				base.IsVisible = (Session.Account.FoodAtShip.CurrentHungerLevel > 0f && Session.Account.FoodAtShip.HasFoodConsumption(Global.Player) == FoodConsumption.Mode.Full && Global.Render.UiMode == InterfaceMode.Default);
				base.UserUpdate(ref {19693});
			}

			// Token: 0x06000BBA RID: 3002 RVA: 0x0005D518 File Offset: 0x0005B718
			public override void RenderFront()
			{
				if (!base.IsVisible)
				{
					return;
				}
				float opcaity = base.GetOpcaity();
				Rectangle c_bt_ice_mask = {19548}.c_bt_ice_mask;
				c_bt_ice_mask.Height = (int)(Session.Account.FoodAtShip.CurrentHungerLevel * (float){19548}.c_bt_ice_mask.Height);
				int num = (int)((1f - Session.Account.FoodAtShip.CurrentHungerLevel) * (float){19548}.c_bt_ice_mask.Height);
				c_bt_ice_mask.Y += num;
				Device gs = Engine.GS;
				Vector2 vector = base.Pos.XY + new Vector2(0f, (float)num);
				Color color = Color.White * opcaity;
				gs.Draw(c_bt_ice_mask, vector, color);
				base.RenderFront();
			}
		}

		// Token: 0x02000204 RID: 516
		private class CenterClass : CustomUi
		{
			// Token: 0x17000114 RID: 276
			// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0005D5FE File Offset: 0x0005B7FE
			private CannonLocation? NearBoard
			{
				get
				{
					return InGameSightUi.CannonSights.LastActiveNearBoard;
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0005D60C File Offset: 0x0005B80C
			private int GetCurrentBoardBallsID
			{
				get
				{
					CannonLocation? nearBoard = this.NearBoard;
					if (nearBoard == null)
					{
						return Global.Settings.SelectedCannonBalls[CannonLocation.RightSide];
					}
					if (nearBoard.GetValueOrDefault() == CannonLocation.InFront && Global.Player.UsedShip.Cannons.HavingFireguns)
					{
						return Global.Settings.SelectedCannonBalls[CannonLocation.RightSide];
					}
					return Global.Settings.SelectedCannonBalls[nearBoard.Value];
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0005D681 File Offset: 0x0005B881
			private CannonBallInfo GetCurrentBoardBalls
			{
				get
				{
					return Gameplay.BallsInfo.FromID(this.GetCurrentBoardBallsID);
				}
			}

			// Token: 0x06000BC1 RID: 3009 RVA: 0x0005D694 File Offset: 0x0005B894
			public CenterClass({19548} {19695}) : base(new Marker(0f, 0f, (float){19548}.CenterClass.c_back.Width, (float){19548}.CenterClass.c_back.Height), {19548}.CenterClass.c_back, PositionAlignment.Center, PositionAlignment.RightDown, Color.White, false)
			{
				{19548}.CenterClass <>4__this = this;
				this.{19708} = {19695};
				this.AnimatedFocus = false;
				base.ToolTip = new ToolTip(delegate(UiControl {19713})
				{
					if (<>4__this.NearBoard.GetValueOrDefault() == CannonLocation.InFront && Global.Player.UsedShip.Cannons.HavingFireguns)
					{
						return new ToolTipState(Local.ncs_cannon_24_name, null, Array.Empty<ToolTipCharacteristics>());
					}
					return {20431}.PreviewHandler(<>4__this.GetCurrentBoardBalls, false, false, null);
				});
				this.{19712} = new Tlist<float>(4);
				this.{19712}.Size = 4;
				this.{19712}.Array[3] = 0f;
				this.{19712}.Array[2] = 0f;
				this.{19712}.Array[0] = 0f;
				this.{19712}.Array[1] = 0f;
				Func<string> <>9__2;
				base.EvClick += delegate(ClickUiEventArgs {19714})
				{
					{19695}.{19555}(<>4__this.Pos.XY, "");
					{19548} parent = {19695};
					Vector2 xy = <>4__this.Pos.XY;
					Func<string> {19559};
					if (({19559} = <>9__2) == null)
					{
						{19559} = (<>9__2 = (() => <>4__this.LoadSwitchForm(null)));
					}
					parent.PrepareSwitchForm(xy, {19559});
					Session.Account.UpdateEducationFlags(EducationOnboarding.GameTT_SwitchCannonBalls);
				};
			}

			// Token: 0x06000BC2 RID: 3010 RVA: 0x0005D78C File Offset: 0x0005B98C
			public void LoadByClick(CannonBallInfo {19696})
			{
				if (Session.Account.Rang <= 3 && Global.Player.UsedShipPlayer.BallsOfHold[(int){19696}.ID] == 0)
				{
					return;
				}
				if (this.NearBoard == null || (this.{19709} != null && this.{19709}.Elapsed.TotalMilliseconds < 300.0 && this.{19710} != null && this.{19710}.ID == {19696}.ID))
				{
					for (int i = 0; i < 4; i++)
					{
						this.{19697}((CannonLocation)i, (int){19696}.ID, true);
					}
					return;
				}
				this.{19710} = {19696};
				if (this.{19709} == null)
				{
					this.{19709} = Stopwatch.StartNew();
				}
				else
				{
					this.{19709}.Restart();
				}
				if (this.NearBoard != null)
				{
					this.{19697}(this.NearBoard.Value, (int){19696}.ID, false);
				}
			}

			// Token: 0x06000BC3 RID: 3011 RVA: 0x0005D884 File Offset: 0x0005BA84
			private void {19697}(CannonLocation {19698}, int {19699}, bool {19700} = false)
			{
				bool flag = Global.Player.UsedShip.Cannons.HavingFireguns || Global.Player.UsedShip.StaticInfo.FrontSidePorts.Length == 0;
				bool flag2 = Global.Player.UsedShip.StaticInfo.BackSidePorts.Length == 0;
				if (({19698} == CannonLocation.InFront && flag) || ({19698} == CannonLocation.InBack && flag2))
				{
					if ({19700})
					{
						Global.Settings.SelectedCannonBalls[{19698}] = {19699};
					}
					return;
				}
				if (Global.Settings.SelectedCannonBalls[CannonLocation.LeftSide] == Global.Settings.SelectedCannonBalls[CannonLocation.RightSide] || {19700})
				{
					if (flag)
					{
						Global.Settings.SelectedCannonBalls[CannonLocation.InFront] = Global.Settings.SelectedCannonBalls[CannonLocation.LeftSide];
					}
					if (flag2)
					{
						Global.Settings.SelectedCannonBalls[CannonLocation.InBack] = Global.Settings.SelectedCannonBalls[CannonLocation.LeftSide];
					}
				}
				if (Session.LastLoadedNonEmptyCannonBalls == null)
				{
					Session.LastLoadedNonEmptyCannonBalls = new SelectedCannonBalls(Global.Settings.SelectedCannonBalls.Value);
				}
				int num = Global.Player.UsedShipPlayer.BallsOfHold[{19699}];
				if (num > 0 && {19699} != Global.Settings.SelectedCannonBalls[{19698}])
				{
					Global.Player.UsedShip.Cannons.BeginReloadWeapons(Global.Player, Gameplay.BallsInfo[Global.Settings.SelectedCannonBalls[{19698}]], Gameplay.BallsInfo[{19699}], new CannonLocation?({19698}));
				}
				Global.Settings.SelectedCannonBalls[{19698}] = {19699};
				CrewNotificationManager.WhenAmmoChanged({19699});
				if (num > 0)
				{
					Session.LastLoadedNonEmptyCannonBalls[{19698}] = {19699};
				}
				this.{19708}.{19555}(base.Pos.XY, Gameplay.BallsInfo.FromID({19699}).Name);
				this.{19708}.PrepareSwitchForm(base.Pos.XY, () => this.LoadSwitchForm(new CannonLocation?({19698})));
				if ({19699} != 1 && Global.Player.MapInfo.IsWorldmap)
				{
					Session.Account.UpdateEducationFlags(EducationOnboarding.GameTT_SwitchCannonBalls);
				}
			}

			// Token: 0x06000BC4 RID: 3012 RVA: 0x0005DACC File Offset: 0x0005BCCC
			private string LoadSwitchForm(CannonLocation? {19701})
			{
				CannonBallInfo[] array = {19548}.cannonAmmoOrder.Select(new Func<int, CannonBallInfo>(Gameplay.BallsInfo.FromID)).ToArray<CannonBallInfo>();
				for (int i = 0; i < array.Length; i++)
				{
					CannonBallInfo item = array[i];
					if (!(item.Name == "removed") && (!item.IsRare || Global.Player.UsedShipPlayer.BallsOfHold.GetCount((int)item.ID) != 0))
					{
						float {11532} = 0f;
						float {11533} = 0f;
						Rectangle bounds = item.IconTexture.Bounds;
						Image image = new Image(new Marker({11532}, {11533}, ref bounds), item.IconTexture, item.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							BasicColor = (({19701} == null || (int)item.ID == Global.Settings.SelectedCannonBalls[{19701}.Value]) ? Color.White : (Color.White * 0.3f))
						};
						image.EvClick += delegate(ClickUiEventArgs {19715})
						{
							Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UISwitch, 0.03f, 1f);
							if ({19701} != null)
							{
								this.LoadByClick(item);
								return;
							}
							for (int j = 0; j < 4; j++)
							{
								this.{19697}((CannonLocation)j, (int)item.ID, false);
							}
						};
						image.DisableDepthFocusTest = true;
						{20431}.Set(image, item, 0, null);
						this.{19708}.{19582}.AddItem(new UiControl[]
						{
							image
						});
					}
				}
				return Local.ItemPanelGui_4;
			}

			// Token: 0x06000BC5 RID: 3013 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserBackRender()
			{
			}

			// Token: 0x06000BC6 RID: 3014 RVA: 0x0005DC70 File Offset: 0x0005BE70
			protected override void UserFrontRender()
			{
				{19548}.CenterClass.<>c__DisplayClass28_0 CS$<>8__locals1;
				CS$<>8__locals1.<>4__this = this;
				CannonLocation? nearBoard = this.NearBoard;
				CannonBallInfo getCurrentBoardBalls = this.GetCurrentBoardBalls;
				int count = Global.Player.UsedShipPlayer.BallsOfHold.GetCount((int)getCurrentBoardBalls.ID);
				float opcaity = base.GetOpcaity();
				Color color = Color.White * opcaity;
				float num = Engine.GS.Camera.Rotation.Y - Global.Player.Rotation + 3.1415927f;
				Marker marker = base.Pos;
				Vector2 vector = marker.Center + Geometry.SubstructRotateFast(num, 54f);
				Engine.GS.Draw({19548}.CenterClass.c_arrow, vector, {19548}.CenterClass.c_arrow.WidthHeight() / 2f, num + 1.5707964f, color);
				int num2 = Global.Player.UsedShip.Cannons.HavingFireguns ? 0 : Global.Player.UsedShip.StaticInfo.FrontSidePorts.Length;
				Rectangle rectangle;
				Marker marker2;
				if (Global.Settings.SelectedCannonBalls.IsSingleSetted)
				{
					Device gs = Engine.GS;
					Texture2D iconTextureCircular = Gameplay.BallsInfo.FromID(Global.Settings.SelectedCannonBalls[CannonLocation.InBack]).IconTextureCircular;
					rectangle = getCurrentBoardBalls.IconTextureCircular.Bounds;
					marker = {19548}.CenterClass.c_pos_ballsIcon;
					marker2 = base.Pos;
					marker = marker.Offset(marker2.XY);
					Rectangle rectangle2 = marker.ToRect();
					gs.DrawCustomTexture(iconTextureCircular, rectangle, rectangle2, color);
				}
				else if (Global.Player.UsedShip.StaticInfo.BackSidePorts.Length + num2 == 0)
				{
					foreach (CannonLocation cannonLocation in new CannonLocation[]
					{
						CannonLocation.LeftSide,
						CannonLocation.RightSide
					})
					{
						CannonBallInfo cannonBallInfo = Gameplay.BallsInfo.FromID(Global.Settings.SelectedCannonBalls[cannonLocation]);
						Device gs2 = Engine.GS;
						Texture2D {14570} = cannonBallInfo.IconTextureShards2[cannonLocation];
						rectangle = getCurrentBoardBalls.IconTextureCircular.Bounds;
						marker2 = {19548}.CenterClass.c_pos_ballsIcon;
						marker = base.Pos;
						marker2 = marker2.Offset(marker.XY);
						Rectangle rectangle2 = marker2.ToRect();
						gs2.DrawCustomTexture({14570}, rectangle, rectangle2, color);
					}
				}
				else
				{
					CannonLocation[] array2 = new CannonLocation[4];
					RuntimeHelpers.InitializeArray(array2, fieldof(<PrivateImplementationDetails>.63760768C8CBC4E348B2562890BE7A3F3449AFDE0B938232D8DF6AFE75FBDA2D).FieldHandle);
					foreach (CannonLocation cannonLocation2 in array2)
					{
						if ((cannonLocation2 != CannonLocation.InFront || num2 != 0) && (cannonLocation2 != CannonLocation.InBack || Global.Player.UsedShip.StaticInfo.BackSidePorts.Length != 0))
						{
							CannonBallInfo cannonBallInfo2 = Gameplay.BallsInfo.FromID(Global.Settings.SelectedCannonBalls[cannonLocation2]);
							Device gs3 = Engine.GS;
							Texture2D {14570}2 = cannonBallInfo2.IconTextureShards4[cannonLocation2];
							rectangle = getCurrentBoardBalls.IconTextureCircular.Bounds;
							marker = {19548}.CenterClass.c_pos_ballsIcon;
							marker2 = base.Pos;
							marker = marker.Offset(marker2.XY);
							Rectangle rectangle2 = marker.ToRect();
							gs3.DrawCustomTexture({14570}2, rectangle, rectangle2, color);
						}
					}
				}
				if (num2 > 0)
				{
					Device gs4 = Engine.GS;
					marker2 = base.Pos;
					rectangle = marker2.ToRect();
					gs4.Draw({19548}.CenterClass.c_areaRear, rectangle, color);
				}
				if (Global.Player.UsedShip.StaticInfo.BackSidePorts.Length != 0)
				{
					Device gs5 = Engine.GS;
					marker2 = base.Pos;
					rectangle = marker2.ToRect();
					gs5.Draw({19548}.CenterClass.c_areaBack, rectangle, color);
				}
				Device gs6 = Engine.GS;
				marker2 = base.Pos;
				rectangle = marker2.ToRect();
				gs6.Draw({19548}.CenterClass.c_overlapIcon, rectangle, color);
				Engine.GS.SetFont(Fonts.Philosopher_14);
				Device gs7 = Engine.GS;
				string {14610} = count.ToString() ?? "";
				Vector2 vector2 = base.Pos.XY + new Vector2(53f, 77f);
				Color color2 = ((count < 50) ? Color.Orange : new Color(193, 193, 193)) * opcaity;
				gs7.DrawStringCentered({14610}, vector2, color2);
				CS$<>8__locals1.c_piv = {19548}.CenterClass.c_reload.WidthHeight() / 2f;
				CS$<>8__locals1.c_pivU2 = {19548}.CenterClass.c_reloadU2.WidthHeight() / 2f;
				CS$<>8__locals1.c_pivU4 = {19548}.CenterClass.c_reloadU4.WidthHeight() / 2f;
				CS$<>8__locals1.c_pivU8 = {19548}.CenterClass.c_reloadU8.WidthHeight() / 2f;
				int num3 = 0;
				CannonLocation[] array3 = new CannonLocation[4];
				RuntimeHelpers.InitializeArray(array3, fieldof(<PrivateImplementationDetails>.63760768C8CBC4E348B2562890BE7A3F3449AFDE0B938232D8DF6AFE75FBDA2D).FieldHandle);
				foreach (CannonLocation cannonLocation3 in array3)
				{
					if ((cannonLocation3 != CannonLocation.InFront || Global.Player.UsedShip.StaticInfo.FrontSidePorts.Length != 0) && (cannonLocation3 != CannonLocation.InBack || Global.Player.UsedShip.StaticInfo.BackSidePorts.Length != 0))
					{
						{19548}.CenterClass.<>c__DisplayClass28_1 CS$<>8__locals2;
						CS$<>8__locals2.virtualAxis = 3.1415927f + ((cannonLocation3 == CannonLocation.InFront) ? 0.7853982f : ((cannonLocation3 == CannonLocation.RightSide) ? -0.7853982f : ((cannonLocation3 == CannonLocation.InBack) ? -2.3561945f : 2.3561945f)));
						float num4 = base.Pos.WH.X / 48f;
						if (cannonLocation3 == CannonLocation.InFront && Global.Player.UsedShip.Cannons.HavingFireguns)
						{
							float overheatValue = Global.Player.UsedShip.Cannons.Fireguns[0].OverheatValue;
							if (overheatValue >= 0f)
							{
								this.{19703}(new Color(151, 70, 22), overheatValue, ref CS$<>8__locals1, ref CS$<>8__locals2);
							}
							else
							{
								Engine.GS.SetFont(Fonts.Arial_10);
								Device gs8 = Engine.GS;
								string itemPanelGui_ = Local.ItemPanelGui_5;
								marker2 = base.Pos;
								vector2 = new Vector2(marker2.Center.X, base.Pos.XY.Y) + new Vector2(0f, -20f);
								color2 = Color.White * 0.7f * Geometry.Saturate((float)Math.Sin(Global.Game.GameTotalTimeSec * 4.0) * 2f + 1f);
								gs8.DrawStringCentered(itemPanelGui_, vector2, color2);
								this.{19703}(new Color(152, 144, 13), 1f + overheatValue, ref CS$<>8__locals1, ref CS$<>8__locals2);
							}
						}
						else
						{
							Vector2 minMaxReloadFactor = Global.Player.UsedShip.Cannons.GetMinMaxReloadFactor(cannonLocation3);
							float num5 = this.{19712}.Array[(int)cannonLocation3];
							if (minMaxReloadFactor.X == 1f)
							{
								num3++;
								CannonLocation? cannonLocation4 = nearBoard;
								CannonLocation cannonLocation5 = cannonLocation3;
								Color color3 = (cannonLocation4.GetValueOrDefault() == cannonLocation5 & cannonLocation4 != null) ? Color.Gray : new Color(51, 51, 57);
								this.{19703}(color3, 1f, ref CS$<>8__locals1, ref CS$<>8__locals2);
								if (num5 == -1f)
								{
									this.{19712}.Array[(int)cannonLocation3] = 1000f;
									Global.Game.SoundSystem.PlaySound(GameStaticSoundName.WeaponsLoaded, 2f, 1f);
								}
								else if (num5 > 0f)
								{
									this.{19703}(Color.Lerp(color3, Color.Lime, 0.7f * num5 / 1000f), 1f, ref CS$<>8__locals1, ref CS$<>8__locals2);
								}
							}
							else
							{
								this.{19712}.Array[(int)cannonLocation3] = -1f;
							}
							if (minMaxReloadFactor.Y < 1f && minMaxReloadFactor.Y > 0f)
							{
								CannonLocation? cannonLocation4 = nearBoard;
								CannonLocation cannonLocation5 = cannonLocation3;
								this.{19703}((cannonLocation4.GetValueOrDefault() == cannonLocation5 & cannonLocation4 != null) ? new Color(152, 104, 43) : Color.Lerp(new Color(152, 104, 43), new Color(51, 51, 57), 0.5f), minMaxReloadFactor.Y, ref CS$<>8__locals1, ref CS$<>8__locals2);
							}
							if (minMaxReloadFactor.X < 1f)
							{
								Color {19704};
								if (Global.Player.ReloadingWeaponsSpeed >= 1f)
								{
									CannonLocation? cannonLocation4 = nearBoard;
									CannonLocation cannonLocation5 = cannonLocation3;
									{19704} = ((cannonLocation4.GetValueOrDefault() == cannonLocation5 & cannonLocation4 != null) ? new Color(152, 144, 13) : Color.Lerp(new Color(152, 144, 13), new Color(51, 51, 57), 0.5f));
								}
								else
								{
									{19704} = new Color(172, 87, 87);
								}
								this.{19703}({19704}, minMaxReloadFactor.X, ref CS$<>8__locals1, ref CS$<>8__locals2);
							}
						}
					}
				}
				int num6 = this.{19711};
				if (num3 > this.{19711} && base.AnimationsCount == 0)
				{
					new UiBrightnessAnimation(this, 1f, 5f, 200f);
					new UiBrightnessAnimation(this, 5f, 1f, 700f);
				}
				this.{19711} = num3;
			}

			// Token: 0x06000BC7 RID: 3015 RVA: 0x0005E534 File Offset: 0x0005C734
			protected override void UserUpdate(ref FrameTime {19702})
			{
				if (this.NearBoard != null && GameScene.GameHasInputFocus && !Global.Settings.kb_Mortar_ModifierKey.IsDown)
				{
					LocalSettings settings = Global.Settings;
					Keys[] array = new Keys[7];
					RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.C4F5DB1DA3F7AC8CEE8A4F207AE447EB25FFE9D7F6A14DB927CFFCB95BD995BA).FieldHandle);
					array[0] = settings.kb_SimpleShot.Key;
					array[1] = settings.kb_FireShot.Key;
					array[2] = settings.kb_AntisailShot.Key;
					array[3] = settings.kb_AnticrewShot.Key;
					Keys[] array2 = array;
					int num = 0;
					foreach (int num2 in {19548}.cannonAmmoOrder)
					{
						CannonBallInfo cannonBallInfo = Gameplay.BallsInfo.FromID(num2);
						if (!(cannonBallInfo.Name == "removed") && cannonBallInfo.AmmoType == CannonAmmoType.CannonBall && (cannonBallInfo.AmmoType != CannonAmmoType.CannonBall || !cannonBallInfo.IsRare || Global.Player.UsedShipPlayer.BallsOfHold[num2] != 0))
						{
							if (InputHelper.IsClick(array2[num]))
							{
								Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UISwitch, 0.03f, 1f);
								this.LoadByClick(cannonBallInfo);
								break;
							}
							num++;
						}
					}
				}
				if (Global.Settings.kb_SwithPanelCannonBall_Gamepad.IsClick && InputHelper.IsGamepadConnected)
				{
					int {19699} = {19548}.SwitchNextItemId(Gameplay.BallsInfo.FromID(Global.Settings.SelectedCannonBalls[CannonLocation.LeftSide]));
					for (int j = 0; j < 4; j++)
					{
						this.{19697}((CannonLocation)j, {19699}, false);
					}
				}
				for (int k = 0; k < this.{19712}.Size; k++)
				{
					if (this.{19712}.Array[k] != -1f)
					{
						{19702}.EvaluteTimerMs(ref this.{19712}.Array[k]);
					}
				}
			}

			// Token: 0x06000BC9 RID: 3017 RVA: 0x0005E83C File Offset: 0x0005CA3C
			[CompilerGenerated]
			private void {19703}(Color {19704}, float {19705}, ref {19548}.CenterClass.<>c__DisplayClass28_0 {19706}, ref {19548}.CenterClass.<>c__DisplayClass28_1 {19707})
			{
				float num = 1.4608406f;
				float num2 = 0.10995574f;
				float num3 = 1.5f * num2 + ({19705} - num2) * num;
				float num4 = num2;
				while (num4 < num3)
				{
					float num5 = num3 - num4;
					float num6 = {19707}.virtualAxis + num4;
					if (num5 >= num / 4f)
					{
						Vector2 vector = base.Pos.Center + Geometry.SubstructRotateFast(num6 + MathHelper.ToRadians(13f), 46f);
						Engine.GS.Draw({19548}.CenterClass.c_reloadU8, vector, {19706}.c_pivU8, num6 + 1.5707964f + MathHelper.ToRadians(13f), {19704});
						num4 += num / 4f;
					}
					else if (num5 >= num / 8f)
					{
						Vector2 vector2 = base.Pos.Center + Geometry.SubstructRotateFast(num6 + MathHelper.ToRadians(6.5f), 46f);
						Engine.GS.Draw({19548}.CenterClass.c_reloadU4, vector2, {19706}.c_pivU4, num6 + 1.5707964f + MathHelper.ToRadians(6.5f), {19704});
						num4 += num / 8f;
					}
					else if (num5 >= num / 16f)
					{
						Vector2 vector3 = base.Pos.Center + Geometry.SubstructRotateFast(num6 + MathHelper.ToRadians(3.25f), 46f);
						Engine.GS.Draw({19548}.CenterClass.c_reloadU2, vector3, {19706}.c_pivU2, num6 + 1.5707964f + MathHelper.ToRadians(3.25f), {19704});
						num4 += num / 16f;
					}
					else if (num5 >= num / 32f)
					{
						Vector2 vector4 = base.Pos.Center + Geometry.SubstructRotateFast(num6 + MathHelper.ToRadians(1.125f), 46f);
						Engine.GS.Draw({19548}.CenterClass.c_reload, vector4, {19706}.c_piv, num6 + 1.5707964f + MathHelper.ToRadians(1.125f), {19704});
						num4 += num / 32f;
					}
					else
					{
						Vector2 vector5 = base.Pos.Center + Geometry.SubstructRotateFast(num6 + MathHelper.ToRadians(0.625f), 46f);
						Engine.GS.Draw({19548}.CenterClass.c_reloadD2, vector5, {19706}.c_piv, num6 + 1.5707964f + MathHelper.ToRadians(0.625f), {19704});
						num4 += num / 64f;
					}
				}
			}

			// Token: 0x04000A93 RID: 2707
			private static readonly Rectangle c_back = new Rectangle(2920, 550, 104, 104);

			// Token: 0x04000A94 RID: 2708
			private static readonly Rectangle c_overlapIcon = new Rectangle(2920, 655, 104, 104);

			// Token: 0x04000A95 RID: 2709
			private static readonly Rectangle c_areaRear = new Rectangle(3025, 655, 104, 104);

			// Token: 0x04000A96 RID: 2710
			private static readonly Rectangle c_areaBack = new Rectangle(3025, 550, 104, 104);

			// Token: 0x04000A97 RID: 2711
			private static readonly Rectangle c_arrow = new Rectangle(2921, 761, 55, 20);

			// Token: 0x04000A98 RID: 2712
			private static readonly Rectangle c_reload = new Rectangle(2979, 760, 3, 14);

			// Token: 0x04000A99 RID: 2713
			private static readonly Rectangle c_reloadD2 = new Rectangle(2985, 760, 3, 14);

			// Token: 0x04000A9A RID: 2714
			private static readonly Rectangle c_reloadU2 = new Rectangle(2991, 760, 6, 14);

			// Token: 0x04000A9B RID: 2715
			private static readonly Rectangle c_reloadU4 = new Rectangle(3000, 760, 12, 14);

			// Token: 0x04000A9C RID: 2716
			private static readonly Rectangle c_reloadU8 = new Rectangle(3015, 760, 24, 14);

			// Token: 0x04000A9D RID: 2717
			private static readonly Rectangle c_blockedOverlay = new Rectangle(3156, 1485, 93, 93);

			// Token: 0x04000A9E RID: 2718
			private static readonly Marker c_pos_ballsIcon = new Marker(18f, 17f, 70f, 70f);

			// Token: 0x04000A9F RID: 2719
			private {19548} {19708};

			// Token: 0x04000AA0 RID: 2720
			private Stopwatch {19709};

			// Token: 0x04000AA1 RID: 2721
			private CannonBallInfo {19710};

			// Token: 0x04000AA2 RID: 2722
			private int {19711};

			// Token: 0x04000AA3 RID: 2723
			private Tlist<float> {19712};
		}
	}
}
