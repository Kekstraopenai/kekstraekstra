using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001C8 RID: 456
	internal sealed class {19215} : CustomUi
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00051983 File Offset: 0x0004FB83
		public static bool IsOpen
		{
			get
			{
				return {19215}.current != null;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00051990 File Offset: 0x0004FB90
		private string timeWaitText
		{
			get
			{
				return Math.Ceiling((double)Global.Settings.DeathController.BlockRespawnOnSeaSec).ToString();
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x000519BC File Offset: 0x0004FBBC
		private Label {19216}(string {19217}, Action {19218}, bool {19219} = false)
		{
			Label label = new Label({19219} ? new Vector2((float)(Engine.GS.UIArea.Width / 2), (float)(Engine.GS.UIArea.Height / 4 + Engine.GS.UIArea.Height / 2)) : Engine.GS.UIArea.HalfWidthHeightInt(), Fonts.Philosopher_24, Color.White * 0.7f, {19217}, PositionAlignment.Center, PositionAlignment.Center).Center();
			Form form = new Form(label.Pos.Border(5f), AtlasGameGui.rect_asset_whitepixel_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Black * 0.4f,
				AnimatedFocus = false
			};
			label.RenderToDepthMap = false;
			form.RenderToDepthMap = false;
			form.PositionAlignment_X = label.PositionAlignment_X;
			form.PositionAlignment_Y = label.PositionAlignment_Y;
			form.AddChild(label);
			new UiOpacityAnimation(form, 0f, 1f, 3000f);
			if (!{19219})
			{
				new UiActionsSleep(form, 500f);
				new UiOpacityAnimation(form, 1f, 0f, 1500f);
				new UiActor(form, {19218});
			}
			return label;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00051AE8 File Offset: 0x0004FCE8
		public {19215}() : base(false)
		{
			this.TexturePath = {19215}.c_background;
			this.BasicColor = Color.White;
			new UiOpacityAnimation(this, 0f, 1f, 1000f);
			this.AnimatedFocus = false;
			bool allowToChangeUpgrades = false;
			Global.Game.SceneGame.CreateSlimInterface();
			{19215}.current = this;
			if (Global.Player.MapInfo.IsWorldmap)
			{
				this.{19216}(Local.DeathPlayerScreen_0, new Action(this.{19229}), false);
			}
			else
			{
				this.{19216}(Local.DeathPlayerScreen_0, delegate
				{
					if (allowToChangeUpgrades)
					{
						this.{19216}(Local.DeathPlayerScreen_f, new Action(this.{19220}), false);
						return;
					}
					this.{19220}();
				}, false);
			}
			if (allowToChangeUpgrades)
			{
				new {22195}();
			}
			base.EvRemoveFromContainer += delegate()
			{
				{19215}.current = null;
			};
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00051C04 File Offset: 0x0004FE04
		private void {19220}()
		{
			if (!string.IsNullOrEmpty(Session.LastDamage))
			{
				this.{19216}({19994}.Logbook(Local.DeathPlayerScreen_1(Session.LastDamage), LBFlags.L0), new Action(this.{19230}), false);
				return;
			}
			if (Global.Settings.DeathController.BlockRespawnOnSeaSec > 3f)
			{
				this.{19233} = this.{19216}(this.timeWaitText, delegate
				{
				}, true);
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00051C8C File Offset: 0x0004FE8C
		private Button createButton(Rectangle {19221}, RespawnMethods {19222}, string {19223}, float {19224} = -1f, Func<string> {19225} = null, int {19226} = 0, bool {19227} = false)
		{
			{19215}.<>c__DisplayClass21_0 CS$<>8__locals1 = new {19215}.<>c__DisplayClass21_0();
			CS$<>8__locals1.goldPay = {19226};
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.pickMethod = {19222};
			CS$<>8__locals1.bt = new Button(Vector2.Zero, {19221}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if (CS$<>8__locals1.goldPay > 0)
			{
				{19225} = (() => string.Concat(new string[]
				{
					Local.pay,
					" ",
					CS$<>8__locals1.goldPay.ToString(),
					" ",
					Local.gold2
				}));
			}
			if ({19225} == null)
			{
				CS$<>8__locals1.bt.SetText({19223}, Fonts.Philosopher_16, ({19227} ? Color.Orange : Color.White) * 0.8f, false);
			}
			else
			{
				CS$<>8__locals1.bt.AddChild(new Label(CS$<>8__locals1.bt.Pos.Center + new Vector2(0f, -8f), Fonts.Philosopher_16, Color.White * 0.8f, {19223}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
				CS$<>8__locals1.bt.AddChild(new LiveLabel(CS$<>8__locals1.bt.Pos.Center + new Vector2(0f, 10f), Fonts.Arial_10, ((CS$<>8__locals1.goldPay > Session.Account.Gold && CS$<>8__locals1.goldPay > 0) ? Color.Orange : Color.White) * 0.8f, {19225}, 200).Center());
			}
			if (!{19227})
			{
				CS$<>8__locals1.bt.EvClick += delegate(ClickUiEventArgs {19240})
				{
					if (CS$<>8__locals1.goldPay <= Session.Account.Gold || CS$<>8__locals1.goldPay <= 0)
					{
						if (CS$<>8__locals1.pickMethod == RespawnMethods.InNearAllowPort && Session.EngagingInPortBattle != PbsBatlleSide.None)
						{
							new {17312}(Local.to_near_port_leavepb_tt, new Action(base.<createButton>g__clickHandler|1), delegate()
							{
							});
							return;
						}
						if (CS$<>8__locals1.pickMethod == RespawnMethods.InLocalRespawn && CS$<>8__locals1.goldPay > 0)
						{
							Action<ValueTuple<object, {22094}.Mode>> {22098};
							if (({22098} = CS$<>8__locals1.<>9__4) == null)
							{
								{22098} = (CS$<>8__locals1.<>9__4 = delegate(ValueTuple<object, {22094}.Mode> {19241})
								{
									CS$<>8__locals1.<>4__this.{19235} = ({19241}.Item1 as IslePortPharosInfo).MapGlobalPosition;
									base.<createButton>g__clickHandler|1();
								});
							}
							new {22094}({22098}, {22094}.Mode.SelectRespawnInSea);
							return;
						}
						base.<createButton>g__clickHandler|1();
					}
				};
			}
			else
			{
				CS$<>8__locals1.bt.Opacity = 0.7f;
			}
			if ({19224} != -1f)
			{
				{19215}.<>c__DisplayClass21_1 CS$<>8__locals2 = new {19215}.<>c__DisplayClass21_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				if (!this.{19238}.ContainsKey(CS$<>8__locals2.CS$<>8__locals1.pickMethod))
				{
					this.{19238}.Add(CS$<>8__locals2.CS$<>8__locals1.pickMethod, {19224});
				}
				CS$<>8__locals2.time = {19224};
				CS$<>8__locals2.formProgress = new Form(CS$<>8__locals2.CS$<>8__locals1.bt.Pos.XY + new Vector2(7f, 52f), {19215}.c_timeoutbar, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					RenderToDepthMap = false
				};
				CS$<>8__locals2.CS$<>8__locals1.bt.AddChild(CS$<>8__locals2.formProgress);
				CS$<>8__locals2.formProgress.UpdateComplete += delegate(UiControl {19242})
				{
					base.<createButton>g__UpdateFormProgress|6();
				};
				CS$<>8__locals2.<createButton>g__UpdateFormProgress|6();
			}
			return CS$<>8__locals1.bt;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00051EE4 File Offset: 0x000500E4
		public void UpdateButtons()
		{
			foreach (UiControl uiControl in ((IEnumerable<UiControl>)this.{19237}))
			{
				uiControl.RemoveFromContainer();
			}
			this.{19237}.Clear();
			string canInSeaButBlocked;
			IslePortInfo inPort;
			IslePortPharosInfo islePortPharosInfo;
			bool flag;
			bool flag2;
			Session.Game.GetAvailableRespawnMethods(Global.Player.MapInfo.IsEnableArenaUi && Session.CurrentArenaSession != null && Session.CurrentArenaSession.ModeInfo.RespawnLimit > 0, out inPort, out islePortPharosInfo, out flag, out flag2, out canInSeaButBlocked);
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				PositionAlignment_X = PositionAlignment.Center,
				PositionAlignment_Y = PositionAlignment.RightDown
			};
			StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				PositionAlignment_X = PositionAlignment.Center,
				PositionAlignment_Y = PositionAlignment.RightDown
			};
			Tlist<UiControl> tlist = this.{19237};
			UiControl uiControl2 = stackForm;
			tlist.Add(uiControl2);
			Tlist<UiControl> tlist2 = this.{19237};
			uiControl2 = stackForm2;
			tlist2.Add(uiControl2);
			if (flag)
			{
				StackForm stackForm3 = Global.Player.MapInfo.IsPassingUi ? stackForm : stackForm2;
				UiControl[] array = new UiControl[1];
				array[0] = this.createButton({19215}.c_button_leave_transparent, RespawnMethods.Leave, Local.leave_pm, -1f, () => Local.DeathPlayerScreen_3, 0, false);
				stackForm3.AddItem(array);
			}
			if (flag2)
			{
				stackForm.AddItem(new UiControl[]
				{
					this.createButton({19215}.c_button_anchor, RespawnMethods.InLocalRespawn, Local.DeathPlayerScreen_4, this.{19231}, null, 0, false)
				});
			}
			if (islePortPharosInfo != null || Session.EngagingInPortBattle != PbsBatlleSide.None || !string.IsNullOrEmpty(canInSeaButBlocked))
			{
				Button button;
				if (Session.EngagingInPortBattle != PbsBatlleSide.None)
				{
					button = this.createButton({19215}.c_button_flag, RespawnMethods.InLocalRespawn, Local.DeathPlayerScreen_5, -1f, null, 0, false);
				}
				else if (islePortPharosInfo != null)
				{
					button = this.createButton({19215}.c_button_flag, RespawnMethods.InLocalRespawn, Local.DeathPlayerScreen_6, -1f, null, Gameplay.RestoringCostVillage(Session.Account), false);
				}
				else
				{
					button = this.createButton({19215}.c_button_flag, RespawnMethods.InLocalRespawn, Local.DeathPlayerScreen_6, -1f, () => canInSeaButBlocked, 0, true);
				}
				if (Session.Account.Shipyard.CurrentRealShip.StaticInfo.IsBalloon && Session.EngagingInPortBattle != PbsBatlleSide.None)
				{
					button.ImitateClick(false);
				}
				stackForm.AddItem(new UiControl[]
				{
					button
				});
			}
			if (inPort != null)
			{
				if (Session.EngagingInPortBattle != PbsBatlleSide.None)
				{
					stackForm2.AddItem(new UiControl[]
					{
						this.createButton({19215}.c_button_anchor, RespawnMethods.InNearAllowPort, Local.to_near_port_leavepb, -1f, () => inPort.PortName, 0, false)
					});
				}
				else
				{
					string {19223} = (inPort == Global.Player.NearPort) ? Local.to_near_port : ((Session.Guild != null && !Session.Guild.IsFlotilia) ? Local.to_ally_port : Local.to_available_port);
					Button button2 = this.createButton({19215}.c_button_anchor, RespawnMethods.InNearAllowPort, {19223}, Math.Max(1f, Session.Account.RespawnOnBoatTimeouSec), delegate
					{
						if (Vector2.Distance(inPort.EntryPos, Global.Player.Position) > 3000f)
						{
							return inPort.PortName + " (" + Local.far + "!)";
						}
						return inPort.PortName;
					}, 0, false);
					if (Session.Account.Shipyard.CurrentRealShip.StaticInfo.IsBalloon)
					{
						button2.ImitateClick(false);
					}
					stackForm.AddItem(new UiControl[]
					{
						button2
					});
				}
			}
			if (Session.Account.Shipyard.CurrentRealShip.StaticInfo.ID != 58 && Session.Group != null)
			{
				AllyStateTransfer avanpostAlly;
				if (Session.LastMinimapAndGroupUpdate.allies.TryFind((AllyStateTransfer {19239}) => {19239}.uID != Global.Player.uID && {19239}.IsActiveAvanpostShip && {19239}.IsOneMap && (!{19239}.IsPeaceActivated || ({19239}.IsPeaceActivated && Global.Player.AccountConnection.WorldFlag.IsPeaceMode())), out avanpostAlly))
				{
					bool noStrength = Session.Account.Shipyard.CurrentRealShip.IntegrityIsDestroyed;
					Button button3 = this.createButton({19215}.c_button_flag, RespawnMethods.MobileAvanpost, Local.ally, -1f, delegate
					{
						if (!noStrength)
						{
							return Local.remain_respawn(avanpostAlly.ActiveAvanpostRemainRespawns);
						}
						return Local.has_no_integrity;
					}, 0, noStrength);
					stackForm.AddItem(new UiControl[]
					{
						button3
					});
				}
			}
			Label label = new Label(new Vector2((float)(Engine.GS.UIArea.Width / 2), (float)(Engine.GS.UIArea.Height - 100)), Fonts.Arial_12, Color.White * 0.7f, "[" + Global.Settings.kb_KeyShowMouse.KeyToString + "] - " + Local.show_mouse, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center();
			Tlist<UiControl> tlist3 = this.{19237};
			uiControl2 = label;
			tlist3.Add(uiControl2);
			stackForm.Pos = new Marker((float)(Engine.GS.UIArea.Width / 2) - stackForm.Pos.WH.X / 2f, (float)Engine.GS.UIArea.Height - stackForm.Pos.WH.Y, stackForm.Pos.WH.X, stackForm.Pos.WH.Y);
			stackForm2.Pos = new Marker(5f, (float)Engine.GS.UIArea.Height - stackForm2.Pos.WH.Y, stackForm2.Pos.WH.X, stackForm2.Pos.WH.Y);
			base.AddChild(new UiControl[]
			{
				stackForm,
				stackForm2
			});
			base.AddChild(label);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00052464 File Offset: 0x00050664
		protected override void UserUpdate(ref FrameTime {19228})
		{
			if (this.{19233} != null)
			{
				this.{19233}.Text = this.timeWaitText;
			}
			Global.Settings.DeathController.TimeBeingOnBoat += {19228}.secElapsed;
			if (Global.Settings.DeathController.BlockRespawnOnSeaSec == 0f && this.{19237}.Size == 0)
			{
				if (this.{19233} != null)
				{
					this.{19233}.GetParent.RemoveFromContainer();
				}
				this.{19233} = null;
				this.UpdateButtons();
			}
			if ({19228}.EvaluteTimerMs2(ref this.{19232}))
			{
				Global.Network.Send(new OnPlayerRespawnMethodMsg(this.{19234}, this.{19235}));
				base.RemoveFromContainer();
			}
			if (this.{19237}.Size > 0 && this.{19236}.Sample(ref {19228}))
			{
				this.UpdateButtons();
			}
			{19228}.EvaluteTimerSec(ref this.{19231});
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00052550 File Offset: 0x00050750
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(AtlasGameGui.Texture.Tex);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000525F1 File Offset: 0x000507F1
		[CompilerGenerated]
		private void {19229}()
		{
			this.{19216}((Session.EngagingInPortBattle == PbsBatlleSide.None) ? Local.death_port_tool_tip : Local.to_near_port_tt2, new Action(this.{19220}), false);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0005261C File Offset: 0x0005081C
		[CompilerGenerated]
		private void {19230}()
		{
			if (Global.Settings.DeathController.BlockRespawnOnSeaSec > 30300f)
			{
				this.{19233} = this.{19216}(this.timeWaitText, delegate
				{
				}, true);
			}
		}

		// Token: 0x0400093D RID: 2365
		private float {19231} = 12f;

		// Token: 0x0400093E RID: 2366
		public static {19215} current;

		// Token: 0x0400093F RID: 2367
		private float {19232};

		// Token: 0x04000940 RID: 2368
		private Label {19233};

		// Token: 0x04000941 RID: 2369
		private RespawnMethods {19234};

		// Token: 0x04000942 RID: 2370
		private Vector2 {19235};

		// Token: 0x04000943 RID: 2371
		private Timer {19236} = new Timer(1500f);

		// Token: 0x04000944 RID: 2372
		private Tlist<UiControl> {19237} = new Tlist<UiControl>();

		// Token: 0x04000945 RID: 2373
		private Dictionary<RespawnMethods, float> {19238} = new Dictionary<RespawnMethods, float>();

		// Token: 0x04000946 RID: 2374
		private static readonly Rectangle c_background = new Rectangle(1869, 861, 452, 239);

		// Token: 0x04000947 RID: 2375
		private static readonly Rectangle c_timeoutbar = new Rectangle(637, 74, 278, 17);

		// Token: 0x04000948 RID: 2376
		private static readonly Rectangle c_button_anchor = new Rectangle(345, 20, 291, 73);

		// Token: 0x04000949 RID: 2377
		private static readonly Rectangle c_button_flag = new Rectangle(637, 0, 291, 73);

		// Token: 0x0400094A RID: 2378
		private static readonly Rectangle c_button_leave_transparent = new Rectangle(1206, 0, 291, 73);
	}
}
