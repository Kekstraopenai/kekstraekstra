using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000159 RID: 345
	internal sealed class {18593} : CustomUi
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0003E8A9 File Offset: 0x0003CAA9
		public bool IsShipReviewTask
		{
			get
			{
				return this.{18606} != null && this.{18606}.GetType() == typeof({18678});
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0003E8CF File Offset: 0x0003CACF
		public bool IsEntryToPortTask
		{
			get
			{
				return this.{18606} != null && this.{18606}.GetType() == typeof({18657});
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0003E8F8 File Offset: 0x0003CAF8
		public Vector2? FollowTaskPosition
		{
			get
			{
				{18673} {18673} = this.{18606} as {18673};
				if ({18673} == null)
				{
					return null;
				}
				return new Vector2?({18673}.targetPosition);
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0003E92C File Offset: 0x0003CB2C
		public static void DebugDisplayAll()
		{
			foreach (object obj in Enum.GetValues(typeof(EducationOnboarding)))
			{
				EducationOnboarding value = (EducationOnboarding)obj;
				if (value.ToString().Contains("GameTT"))
				{
					foreach ({18593}.ShowBasic showBasic in {18593}.GetSlides(new EducationOnboarding?(value)))
					{
					}
				}
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0003E9C4 File Offset: 0x0003CBC4
		internal static {18593}.ShowBasic[] GetSlides(EducationOnboarding? {18595})
		{
			{18593}.ShowBasic[] array = null;
			if ({18595} == null)
			{
				CommonGlobal.EducationMapWeather.SetFullInitialState(Vector3.Forward, OceanStateEnum.Storm, WaterStateEnum.Sunny);
				{18593}.ShowBasic[] array2 = new {18593}.ShowBasic[17];
				array2[0] = new {18593}.ShowDialog("", {18593}.AfterClose.Nothing, delegate()
				{
					new {18738}().Finished += delegate()
					{
						{18593}.CurrentInstance.{18599}({18593}.CurrentInstance.{18605});
					};
				}, new {17464}[0]);
				array2[1] = new {18593}.ShowDialog("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.onboarding_education_start_1)
				});
				array2[2] = new {18593}.ShowSmallHint(Local.onboarding_education_start_q, {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					{19548} {19548} = new {19548}();
					{19548}.ShowAmmoAndReload = false;
					{19548}.ShowFalkonets = false;
					{19548}.ShowKegs = false;
					{19548}.ShowHoldButton = false;
					{19548}.ShowMendingButton = false;
					new {19891}();
					{18593}.CurrentInstance.{18597}(new {18673}(Global.Player.Position + Geometry.RotateVector2(Global.Player.Normal, -0.4f) * 130f));
				}, new {17464}[]
				{
					new {17464}(new Rectangle(0, 624, 502, 49), Local.onboarding_education_start_2(Global.Settings.kb_ds_Forward.KeyToString, Global.Settings.kb_ds_Backward.KeyToString))
				});
				array2[3] = new {18593}.ShowDialog("", {18593}.AfterClose.NextSlide, null, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.onboarding_education_battle1)
				});
				array2[4] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
					new {20059}();
					Global.Network.Send(new OnEducationStatusChangeMsg(OnEducationStatusChangeMsg.Subtype.SpawnShipsScenario, 0));
					{18593}.CurrentInstance.{18597}(new {18646}(0));
				}, new {17464}[]
				{
					new {17464}(new Rectangle(0, 674, 183, 91), Local.onboarding_education_battle7)
				});
				array2[5] = new {18593}.ShowSmallHint(Local.onboarding_education_battle2_q, {18593}.AfterClose.WaitTasksComplete, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(189, 673, 272, 93), Local.onboarding_education_battle2)
				});
				array2[6] = new {18593}.ShowSmallHint(Local.onboarding_education_battle3_q, {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					{18593}.CurrentInstance.{18597}(new {18678}(1));
				}, new {17464}[]
				{
					new {17464}(new Rectangle(0, 766, 353, 124), Local.onboarding_education_battle3)
				});
				array2[7] = new {18593}.ShowSmallHint(Local.onboarding_education_battle4_q, {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					{19548}.CurrentInstance.ShowMendingButton = true;
					{18593}.CurrentInstance.{18597}(new {18684}());
				}, new {17464}[]
				{
					new {17464}(new Rectangle(354, 766, 427, 108), Local.onboarding_education_battle4)
				});
				array2[8] = new {18593}.ShowSmallHint(Local.onboarding_education_battle5_q, {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					{19548}.CurrentInstance.ShowAmmoAndReload = true;
					{18593}.CurrentInstance.{18597}(new {18687}());
				}, new {17464}[]
				{
					new {17464}(new Rectangle(0, 891, 322, 127), Local.onboarding_education_battle5)
				});
				array2[9] = new {18593}.ShowDialog(Local.onboarding_education_battle6_q(Global.Settings.kb_Spyglass.KeyToString), {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					Global.Network.Send(new OnEducationStatusChangeMsg(OnEducationStatusChangeMsg.Subtype.SpawnShipsScenario, 1));
					{18593}.CurrentInstance.{18597}(new {18654}());
				}, new {17464}[]
				{
					new {17464}(new Rectangle(323, 891, 181, 119), Local.onboarding_education_battle6)
				});
				array2[10] = new {18593}.ShowSmallHint(Local.onboarding_education_battle8_q, {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					CommonGlobal.EducationMapWeather.CreateCustomStormApproachingCenter();
					CommonGlobal.EducationMapWeather.EducationMapStormLimit = 1700f;
					{18593}.CurrentInstance.{18597}(new {18646}(1));
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.onboarding_education_battle8)
				});
				array2[11] = new {18593}.ShowSmallHint(Local.onboarding_education_battle9_q, {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					CommonGlobal.EducationMapWeather.EducationMapStormLimit = 800f;
					{18593}.CurrentInstance.{18597}(new {18678}(1));
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.onboarding_education_battle9)
				});
				array2[12] = new {18593}.ShowSmallHint(Local.onboarding_education_back2_q(Global.Settings.kb_OpenHold.KeyToString), {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					{19548}.CurrentInstance.ShowHoldButton = true;
					{18593}.CurrentInstance.{18597}(new {18695}());
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.onboarding_education_back2(Global.Settings.kb_OpenHold.KeyToString))
				});
				array2[13] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
					new {19970}();
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.onboarding_education_back1)
				});
				array2[14] = new {18593}.ShowSmallHint("", {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					{18593}.CurrentInstance.{18597}(new {18690}());
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.onboarding_education_back2_bt(Global.Settings.kb_KeyShowMouse.KeyToString))
				});
				array2[15] = new {18593}.ShowSmallHint(Local.onboarding_education_back3_q, {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					{18593}.CurrentInstance.{18597}(new {18657}(true));
				}, new {17464}[]
				{
					new {17464}(new Rectangle(299, 1019, 65, 65), Local.onboarding_education_back3)
				});
				array2[16] = new {18593}.ShowSmallHint(Local.onboarding_education_back3_q, {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					{18593}.CurrentInstance.{18597}(new {18657}(false));
				}, new {17464}[]
				{
					new {17464}(new Rectangle(507, 623, 269, 49), Local.onboarding_education_back4)
				});
				array = array2;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_FirstPortEntry)
			{
				{18593}.ShowBasic[] array3 = new {18593}.ShowBasic[3];
				array3[0] = new {18593}.ShowDialog(Local.onboarding_port1_q, {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					{18593}.CurrentInstance.{18597}(new {18663}());
				}, new {17464}[]
				{
					new {17464}(Local.onboarding_port1, default(ImageDecription)),
					new {17464}(new Rectangle(493, 1694, 286, 48), Local.onboarding_port2),
					new {17464}(Local.onboarding_port1_b, default(ImageDecription))
				});
				array3[1] = new {18593}.ShowDialog(Local.onboarding_port3_q, {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					{18593}.CurrentInstance.{18597}(new {18667}());
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.onboarding_port3)
				});
				array3[2] = new {18593}.ShowSmallHint(Local.onboarding_port10_q, {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					{18593}.CurrentInstance.{18597}(new {18670}());
				}, new {17464}[]
				{
					new {17464}(new Rectangle(472, 681, 57, 78), Local.onboarding_port10)
				});
				array = array3;
				if (Session.Account.IsPremium && Session.HadReferalBonus)
				{
					int num = CommonSupport.StartPremiumDuration(Session.EventActionsPipeline);
					IEnumerable<{18593}.ShowBasic> first = array;
					{18593}.ShowBasic[] array4 = new {18593}.ShowBasic[1];
					array4[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
					{
					}, new {17464}[]
					{
						new {17464}(default(Rectangle), Session.HadReferalBonus ? Local.referal_bonus2(Gameplay.ReferalBonusExtraShips, Gameplay.ReferalBonusExtraPremium + num) : Local.onboarding_port0(num))
					});
					array = first.Concat(array4).ToArray<{18593}.ShowBasic>();
				}
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_FirstPortEntryAfterTaverna)
			{
				{18593}.ShowBasic[] array5 = new {18593}.ShowBasic[1];
				array5[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.onboarding_port8)
				});
				array = array5;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_FirstExitPort)
			{
				{18593}.ShowBasic[] array6 = new {18593}.ShowBasic[1];
				array6[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(450, 1112, 312, 83), Local.EducationGui_49),
					new {17464}(new Rectangle(584, 1623, 197, 69), Local.EducationGui_49_b(Global.Settings.kb_Spyglass.KeyToString))
				});
				array = array6;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_SellItems)
			{
				{18593}.ShowBasic[] array7 = new {18593}.ShowBasic[1];
				array7[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(0, 1019, 298, 110), Local.EducationGui_51),
					new {17464}(new Rectangle(0, 1019, 298, 110), Local.EducationGui_51_b)
				});
				array = array7;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_ShipBattleDetails)
			{
				{18593}.ShowBasic[] array8 = new {18593}.ShowBasic[1];
				array8[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(505, 891, 216, 97), Local.EducationGui_53)
				});
				array = array8;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_SkillsWindowToolTip)
			{
				{18593}.ShowBasic[] array9 = new {18593}.ShowBasic[1];
				array9[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(207, 0, 153, 84), Local.EducationGui_55)
				});
				array = array9;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_MortarShipBought)
			{
				{18593}.ShowBasic[] array10 = new {18593}.ShowBasic[1];
				array10[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_66),
					new {17464}(default(Rectangle), Local.ship_stat_tt_7(Global.Settings.kb_Mortar_ModifierKey.KeyToString, (Global.Player.UsedShipPlayer.CraftFrom.MaxMortarPoundage != null) ? Global.Player.UsedShipPlayer.CraftFrom.MaxMortarPoundage.Value.ToString() : "-"))
				});
				array = array10;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_Flags)
			{
				{18593}.ShowBasic[] array11 = new {18593}.ShowBasic[1];
				array11[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(600, 1123, 155, 58), Local.EducationGui_68)
				});
				array = array11;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_ArenaWindow)
			{
				{18593}.ShowBasic[] array12 = new {18593}.ShowBasic[1];
				array12[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(Local.EducationGui_71_t, default(ImageDecription))
				});
				array = array12;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_PickWoodForMending)
			{
				{18593}.ShowBasic[] array13 = new {18593}.ShowBasic[1];
				array13[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(226, 1131, 56, 56), Local.EducationGui_72_t)
				});
				array = array13;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_ExitHavingVisibleQuests)
			{
				{18593}.ShowBasic[] array14 = new {18593}.ShowBasic[1];
				array14[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(672, 1371, 156, 73), Local.EducationGui_80)
				});
				array = array14;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_ExitLighthouse)
			{
				{18593}.ShowBasic[] array15 = new {18593}.ShowBasic[1];
				array15[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(346, 1134, 52, 53), Local.EducationGui_81)
				});
				array = array15;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_MendingAndDecraftCapers)
			{
				{18593}.ShowBasic[] array16 = new {18593}.ShowBasic[1];
				array16[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(0, 1443, 213, 89), Local.EducationGui_83)
				});
				array = array16;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_EquipUnitsFromPort)
			{
				{18593}.ShowBasic[] array17 = new {18593}.ShowBasic[1];
				array17[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(480, 1325, 161, 46), Local.EducationGui_84)
				});
				array = array17;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_WantedLevelNps)
			{
				{18593}.ShowBasic[] array18 = new {18593}.ShowBasic[1];
				array18[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(313, 1242, 94, 48), Local.gamewiki_wanted_level_text2)
				});
				array = array18;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_SwitchCannons)
			{
				{18593}.ShowBasic[] array19 = new {18593}.ShowBasic[1];
				array19[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(537, 1373, 131, 75), Local.EducationGui_86)
				});
				array = array19;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_ChangeShips)
			{
				{18593}.ShowBasic[] array20 = new {18593}.ShowBasic[1];
				array20[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(512, 1449, 162, 51), Local.EducationGui_87)
				});
				array = array20;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_FirstWantedLevel)
			{
				{18593}.ShowBasic[] array21 = new {18593}.ShowBasic[1];
				array21[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(609, 500, 160, 45), Local.EducationGui_89)
				});
				array = array21;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_OpenGamepediaByHand)
			{
				{18593}.ShowBasic[] array22 = new {18593}.ShowBasic[1];
				array22[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(612, 549, 159, 71), Local.EducationGui_90)
				});
				array = array22;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_OpenStorm)
			{
				{18593}.ShowBasic[] array23 = new {18593}.ShowBasic[1];
				array23[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_91)
				});
				array = array23;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_OpenBoarding)
			{
				{18593}.ShowBasic[] array24 = new {18593}.ShowBasic[1];
				array24[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.gamewiki_boarding_text1),
					new {17464}(default(Rectangle), Local.gamewiki_boarding_text3)
				});
				array = array24;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_CaptureShip)
			{
				{18593}.ShowBasic[] array25 = new {18593}.ShowBasic[1];
				array25[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.Education_Gui_captured_ship(Global.Settings.kb_SendGroupCommand.KeyToString))
				});
				array = array25;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_SwitchCannonBalls)
			{
				{18593}.ShowBasic[] array26 = new {18593}.ShowBasic[1];
				array26[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_94)
				});
				array = array26;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_UseFalkonets)
			{
				{18593}.ShowBasic[] array27 = new {18593}.ShowBasic[1];
				array27[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_93(Global.Settings.kb_Falkonet.KeyToString))
				});
				array = array27;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_UsePowderKegs)
			{
				{18593}.ShowBasic[] array28 = new {18593}.ShowBasic[1];
				array28[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_PowderKeg(Global.Settings.kb_ThrowPowderKeg.KeyToString))
				});
				array = array28;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_ReceivedMineDamage)
			{
				{18593}.ShowBasic[] array29 = new {18593}.ShowBasic[1];
				array29[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_92)
				});
				array = array29;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_EnterSeamlessSea)
			{
				{18593}.ShowBasic[] array30 = new {18593}.ShowBasic[1];
				array30[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_ss)
				});
				array = array30;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_OpenSocialMenu)
			{
				{18593}.ShowBasic[] array31 = new {18593}.ShowBasic[1];
				array31[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.gamewiki_socialmenu_text1)
				});
				array = array31;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_FirstExitSpecificMortar)
			{
				{18593}.ShowBasic[] array32 = new {18593}.ShowBasic[1];
				array32[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_67)
				});
				array = array32;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_OpenSpecialUnits)
			{
				{18593}.ShowBasic[] array33 = new {18593}.ShowBasic[1];
				array33[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(589, 1509, 127, 42), Local.EducationGui_GameTT_SpecialCrew),
					new {17464}(new Rectangle(589, 1509, 127, 42), Local.gamewiki_crew_text3)
				});
				array = array33;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_EnterFoodConsumptionZone)
			{
				{18593}.ShowBasic[] array34 = new {18593}.ShowBasic[1];
				array34[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.gamewiki_storm_text3),
					new {17464}(default(Rectangle), Local.gamewiki_storm_text4)
				});
				array = array34;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_EnterHazardZone)
			{
				{18593}.ShowBasic[] array35 = new {18593}.ShowBasic[1];
				array35[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_GameTT_EnterHazardZone)
				});
				array = array35;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_FirstGuildSalary)
			{
				{18593}.ShowBasic[] array36 = new {18593}.ShowBasic[1];
				array36[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.GuildWindow_salary_tt)
				});
				array = array36;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_UnitsChangesAndRatio)
			{
				{18593}.ShowBasic[] array37 = new {18593}.ShowBasic[1];
				array37[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.gamewiki_crew_text2)
				});
				array = array37;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_FirstBuildingOnPersonalIsle)
			{
				{18593}.ShowBasic[] array38 = new {18593}.ShowBasic[1];
				array38[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_GameTT_FirstBuildingOnPersonalIsle)
				});
				array = array38;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_LegendSkillsWasOpened)
			{
				{18593}.ShowBasic[] array39 = new {18593}.ShowBasic[1];
				array39[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(215, 1491, 137, 41), Local.EducationGui_GameTT_LegendSkillsWasOpened)
				});
				array = array39;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_ShipLoadingAndOverloading)
			{
				{18593}.ShowBasic[] array40 = new {18593}.ShowBasic[1];
				array40[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_GameTT_ShipLoadingAndOverloading)
				});
				array = array40;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_RespawnNotInNearPort)
			{
				{18593}.ShowBasic[] array41 = new {18593}.ShowBasic[1];
				array41[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_GameTT_RespawnNotInNearPort)
				});
				array = array41;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_UseDoublonesPassingMap)
			{
				{18593}.ShowBasic[] array42 = new {18593}.ShowBasic[1];
				array42[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_GameTT_UseDoublonesPassingMap)
				});
				array = array42;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_FirstCoins)
			{
				{18593}.ShowBasic[] array43 = new {18593}.ShowBasic[1];
				array43[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_GameTT_FirstCoins(Session.Account.Analytics.SummaryDonations))
				});
				array = array43;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_BattleTimer)
			{
				{18593}.ShowBasic[] array44 = new {18593}.ShowBasic[1];
				array44[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(727, 1495, 100, 45), Local.EducationGui_GameTT_BattleTimer)
				});
				array = array44;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_NoCurrentAmmoInCapturedShip)
			{
				{18593}.ShowBasic[] array45 = new {18593}.ShowBasic[1];
				array45[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(0, 1443, 213, 89), Local.EducationGui_GameTT_NoCurrentAmmoInCapturedShip)
				});
				array = array45;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_CountOfNeedCannons)
			{
				{18593}.ShowBasic[] array46 = new {18593}.ShowBasic[1];
				int num2 = 0;
				string {18627} = "";
				{18593}.AfterClose {18628} = {18593}.AfterClose.NextSlide;
				Action {18629} = delegate()
				{
				};
				{17464}[] array47 = new {17464}[1];
				array47[0] = new {17464}(default(Rectangle), Local.EducationGui_GameTT_CountOfNeedCannons(Session.Account.CannonsAtStorage.GetTotalItemsCount(), Math.Max(1, Session.Account.Shipyard.List.Max((PlayerShipDynamicInfo {18631}) => {18631}.StaticInfo.Ports.Length) - Session.Account.CannonsAtStorage.GetTotalItemsCount())));
				array46[num2] = new {18593}.ShowSmallHint({18627}, {18628}, {18629}, array47);
				array = array46;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_LootDivision)
			{
				{18593}.ShowBasic[] array48 = new {18593}.ShowBasic[1];
				array48[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_GameTT_LootDivision)
				});
				array = array48;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_LowReputationKickFromGuild)
			{
				string text = (Session.Guild.Fraction == FractionID.TradeUnion) ? Local.kick_guild_reputation_trade : Local.kick_guild_reputation_nation;
				{18593}.ShowBasic[] array49 = new {18593}.ShowBasic[1];
				array49[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), ((int)Session.MyMemberInGuild.RankId == GuildAccessRightsInfo.FounderRights.RankId) ? Local.EducationGui_GameTT_LowReputationKickFromGuild_founder(text) : Local.EducationGui_GameTT_LowReputationKickFromGuild(text))
				});
				array = array49;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_OpenHold)
			{
				{18593}.ShowBasic[] array50 = new {18593}.ShowBasic[1];
				array50[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_OpenHoldFromGame)
				});
				array = array50;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_MoveByDoubleClick)
			{
				{18593}.ShowBasic[] array51 = new {18593}.ShowBasic[1];
				array51[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.GameTT_MoveByDoubleClick)
				});
				array = array51;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_Whales)
			{
				{18593}.ShowBasic[] array52 = new {18593}.ShowBasic[1];
				array52[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_GameTT_Whales)
				});
				array = array52;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_OpenAuction)
			{
				{18593}.ShowBasic[] array53 = new {18593}.ShowBasic[1];
				array53[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(Local.EducationGui_GameTT_OpenAuction1, default(ImageDecription)),
					new {17464}(Local.EducationGui_GameTT_OpenAuction2, default(ImageDecription))
				});
				array = array53;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_BuyStartPacket)
			{
				{18593}.ShowBasic[] array54 = new {18593}.ShowBasic[1];
				int num3 = 0;
				{18593}.ShowSmallHint showSmallHint = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(293, 1655, 198, 56), Local.EducationGui_GameTT_BuyStartPacket)
				});
				showSmallHint.Decorate = delegate({17312} {18632})
				{
					Button button = new Button(Vector2.Zero, {17312}.cButton_long, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					button.SetText("  " + Local.to_open + " " + Local.PortVisualScene_2, Fonts.Philosopher_14, Color.White, false);
					button.EvClick += delegate(ClickUiEventArgs {18633})
					{
						Global.Game.ScenePort.realShopHandler(null, null);
					};
					{18632}.AddChildPos(button, PositionAlignment.Center, PositionAlignment.RightDown, 55f, 195f, false);
				};
				array54[num3] = showSmallHint;
				array = array54;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_SupportProjectPay)
			{
				{18593}.ShowBasic[] array55 = new {18593}.ShowBasic[1];
				array55[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(293, 1655, 198, 56), Local.EducationGui_GameTT_SupportProject),
					new {17464}(new Rectangle(293, 1655, 198, 56), Local.EducationGui_GameTT_SupportProject2)
				});
				array = array55;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_SailingXp)
			{
				{18593}.ShowBasic[] array56 = new {18593}.ShowBasic[1];
				array56[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(Local.EducationGui_GameTT_SailingXp, default(ImageDecription))
				});
				array = array56;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_MoveResourcesWithoutTravel)
			{
				{18593}.ShowBasic[] array57 = new {18593}.ShowBasic[1];
				array57[0] = new {18593}.ShowDialog("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(Local.EducationGui_GameTT_MoveResourcesWithoutTravel, default(ImageDecription))
				});
				array = array57;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_GetResFromOverloadedFactory)
			{
				{18593}.ShowBasic[] array58 = new {18593}.ShowBasic[1];
				array58[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(583, 1553, 84, 66), Local.EducationGui_GameTT_GetResFromFactory)
				});
				array = array58;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_Shallow)
			{
				{18593}.ShowBasic[] array59 = new {18593}.ShowBasic[1];
				array59[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_GameTT_Shallow)
				});
				array = array59;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_DestroyPowderKegByFire)
			{
				{18593}.ShowBasic[] array60 = new {18593}.ShowBasic[1];
				array60[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_GameTT_DestroyPowderKegByFire)
				});
				array = array60;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_WindOnMap)
			{
				{18593}.ShowBasic[] array61 = new {18593}.ShowBasic[2];
				array61[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(new Rectangle(621, 1035, 121, 68), Local.EducationGui_GameTT_WindOnMap)
				});
				array61[1] = new {18593}.ShowSmallHint("", {18593}.AfterClose.NextSlide, delegate()
				{
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_GameTT_WindOnMap2)
				});
				array = array61;
			}
			if ({18595}.GetValueOrDefault() == EducationOnboarding.GameTT_PirateTrader)
			{
				{18593}.ShowBasic[] array62 = new {18593}.ShowBasic[1];
				array62[0] = new {18593}.ShowSmallHint("", {18593}.AfterClose.WaitTasksComplete, delegate()
				{
					CommonGlobal.EducationMapWeather.EducationMapStormLimit = 800f;
					{18593}.CurrentInstance.{18597}(new {18698}());
				}, new {17464}[]
				{
					new {17464}(default(Rectangle), Local.EducationGui_GameTT_PirateTrader)
				});
				array = array62;
			}
			if (array == null)
			{
				throw new NotSupportedException();
			}
			return array;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00040AA0 File Offset: 0x0003ECA0
		public {18593}(EducationOnboarding? {18596}) : base(false)
		{
			this.{18604} = {18593}.GetSlides({18596});
			if ({18596} == null)
			{
				if (!Session.Account.PlayerName.StartsWith("unnamed_"))
				{
					this.{18605} = 1;
				}
				Global.Camera.StartApproximatingAnimation(new Vector3(0f, 20f, 0f));
				Button soundButton = new Button(new Vector2((float)(Engine.GS.UIArea.Width - AtlasGameGui.soundButton_state1.Width), 0f), AtlasGameGui.soundButton_state1, PositionAlignment.RightDown, PositionAlignment.LeftUp);
				float rollbackSoundVolume = 0f;
				float rollbackMusicVolume = 0f;
				float rollbackAmbientVolume = 0f;
				soundButton.EvClick += delegate(ClickUiEventArgs {18634})
				{
					if (Global.Settings.SoundVolume == 0f)
					{
						Global.Settings.SoundVolume = rollbackSoundVolume;
						Global.Settings.MusicVolume = rollbackMusicVolume;
						Global.Settings.AmbientVolume = rollbackAmbientVolume;
						soundButton.TexturePath = AtlasGameGui.soundButton_state1;
						return;
					}
					rollbackSoundVolume = Global.Settings.SoundVolume;
					rollbackMusicVolume = Global.Settings.MusicVolume;
					rollbackAmbientVolume = Global.Settings.AmbientVolume;
					Global.Settings.SoundVolume = 0f;
					Global.Settings.MusicVolume = 0f;
					Global.Settings.AmbientVolume = 0f;
					soundButton.TexturePath = AtlasGameGui.soundButton_state2;
				};
				base.RemoveWithThis(new UiControl[]
				{
					soundButton
				});
			}
			this.AnimatedFocus = false;
			{18593}.CurrentInstance = this;
			this.{18608} = 1500f;
			base.EvRemoveFromContainer += this.{18603};
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00040BBB File Offset: 0x0003EDBB
		private void {18597}({18637} {18598})
		{
			{18637} {18637} = this.{18606};
			if ({18637} != null)
			{
				{18637}.Dispose();
			}
			this.{18606} = {18598};
			{18598}.Begin();
			{18598}.Completed += this.{18601};
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00040BF0 File Offset: 0x0003EDF0
		private void {18599}(int {18600})
		{
			{18593}.<>c__DisplayClass22_0 CS$<>8__locals1 = new {18593}.<>c__DisplayClass22_0();
			CS$<>8__locals1.<>4__this = this;
			if (this.{18604} == null || {18600} == this.{18604}.Length)
			{
				base.RemoveFromContainer();
				return;
			}
			CS$<>8__locals1.slide = this.{18604}[{18600}];
			this.{18605} = {18600} + 1;
			Action methodWithOpen = CS$<>8__locals1.slide.MethodWithOpen;
			if (methodWithOpen != null)
			{
				methodWithOpen();
			}
			UiControl uiControl = this.{18607};
			if (uiControl != null)
			{
				uiControl.RemoveFromContainer();
			}
			CS$<>8__locals1.evCloseByAnyButton = delegate()
			{
				if ((CS$<>8__locals1.slide.Blocking == {18593}.AfterClose.NextSlide || (CS$<>8__locals1.slide.Blocking == {18593}.AfterClose.WaitTasksComplete && CS$<>8__locals1.<>4__this.{18606} == null)) && CS$<>8__locals1.slide == CS$<>8__locals1.<>4__this.{18604}[CS$<>8__locals1.<>4__this.{18605} - 1])
				{
					CS$<>8__locals1.<>4__this.{18608} = 1f;
				}
				if (Global.Player.MapInfo.IsEducationMap)
				{
					Global.Network.NetClient.Send(new OnEducationStatusChangeMsg(OnEducationStatusChangeMsg.Subtype.SpawnShipsScenario, byte.MaxValue));
				}
			};
			{18593}.ShowSmallHint showSmallHint = CS$<>8__locals1.slide as {18593}.ShowSmallHint;
			if (showSmallHint != null)
			{
				if (showSmallHint.Slides.Length != 0)
				{
					{17312}.TryCloseEducation();
					{17312} window = new {17312}(true, showSmallHint.Slides);
					Action<{17312}> decorate = showSmallHint.Decorate;
					if (decorate != null)
					{
						decorate(window);
					}
					window.EvCloseBySlidesFinished += delegate()
					{
						if (window.IsClosedByHand)
						{
							CS$<>8__locals1.evCloseByAnyButton();
						}
					};
				}
				CS$<>8__locals1.<LoadSlide>g__ShowActionToolTip|0(true, showSmallHint.Action);
			}
			CS$<>8__locals1.dialog = (CS$<>8__locals1.slide as {18593}.ShowDialog);
			if (CS$<>8__locals1.dialog != null)
			{
				if (CS$<>8__locals1.dialog.Slides.Length != 0)
				{
					{17312}.TryCloseEducation();
					{17312} window = new {17312}(false, CS$<>8__locals1.dialog.Slides);
					window.MissclickProtection = 1500f;
					window.EvCloseBySlidesFinished += delegate()
					{
						if (window.IsClosedByHand)
						{
							CS$<>8__locals1.evCloseByAnyButton();
							CS$<>8__locals1.<LoadSlide>g__ShowActionToolTip|0(false, CS$<>8__locals1.dialog.Action);
						}
					};
					return;
				}
				CS$<>8__locals1.<LoadSlide>g__ShowActionToolTip|0(false, CS$<>8__locals1.dialog.Action);
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00040D7A File Offset: 0x0003EF7A
		private void {18601}()
		{
			{18637} {18637} = this.{18606};
			if ({18637} != null)
			{
				{18637}.Dispose();
			}
			this.{18606} = null;
			this.{18608} = 1500f;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00040D9F File Offset: 0x0003EF9F
		public void ServerTaskCompleted()
		{
			{18637} {18637} = this.{18606};
			if ({18637} == null)
			{
				return;
			}
			{18637}.OnCompleted();
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00040DB4 File Offset: 0x0003EFB4
		protected override void UserUpdate(ref FrameTime {18602})
		{
			if (this.{18606} != null)
			{
				this.{18606}.Update(ref {18602});
			}
			if ((GameScene.GameHasInputFocus || {17312}.CurrentInstance != null || (Global.Player.IsPortEntry && {22913}.CurrentInstance == null)) && {18602}.EvaluteTimerMs2(ref this.{18608}))
			{
				this.{18599}(this.{18605});
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00040E2D File Offset: 0x0003F02D
		[CompilerGenerated]
		private void {18603}()
		{
			UiControl uiControl = this.{18607};
			if (uiControl != null)
			{
				uiControl.RemoveFromContainer();
			}
			{18593}.CurrentInstance = null;
		}

		// Token: 0x040006FD RID: 1789
		private static readonly Rectangle c_questMarker = new Rectangle(1214, 160, 300, 41);

		// Token: 0x040006FE RID: 1790
		private {18593}.ShowBasic[] {18604};

		// Token: 0x040006FF RID: 1791
		private int {18605};

		// Token: 0x04000700 RID: 1792
		public static {18593} CurrentInstance;

		// Token: 0x04000701 RID: 1793
		public string CurrentQuestPortMode;

		// Token: 0x04000702 RID: 1794
		private {18637} {18606};

		// Token: 0x04000703 RID: 1795
		private UiControl {18607};

		// Token: 0x04000704 RID: 1796
		private float {18608};

		// Token: 0x0200015A RID: 346
		internal enum AfterClose
		{
			// Token: 0x04000706 RID: 1798
			NextSlide,
			// Token: 0x04000707 RID: 1799
			WaitTasksComplete,
			// Token: 0x04000708 RID: 1800
			Nothing
		}

		// Token: 0x0200015B RID: 347
		public abstract class ShowBasic
		{
			// Token: 0x060007E1 RID: 2017 RVA: 0x00040E46 File Offset: 0x0003F046
			protected ShowBasic(string {18612}, {18593}.AfterClose {18613}, Action {18614})
			{
				this.Action = {18612};
				this.Blocking = {18613};
				this.MethodWithOpen = {18614};
			}

			// Token: 0x04000709 RID: 1801
			public string Action;

			// Token: 0x0400070A RID: 1802
			public {18593}.AfterClose Blocking;

			// Token: 0x0400070B RID: 1803
			public Action MethodWithOpen;
		}

		// Token: 0x0200015C RID: 348
		public class ShowDialog : {18593}.ShowBasic
		{
			// Token: 0x060007E2 RID: 2018 RVA: 0x00040E63 File Offset: 0x0003F063
			public ShowDialog(string {18619}, {18593}.AfterClose {18620}, Action {18621}, params {17464}[] {18622}) : base({18619}, {18620}, {18621})
			{
				this.Slides = {18622};
			}

			// Token: 0x060007E3 RID: 2019 RVA: 0x00040E78 File Offset: 0x0003F078
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach ({17464} {17464} in this.Slides)
				{
					stringBuilder.AppendLine({17464}.Text);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x0400070C RID: 1804
			public {17464}[] Slides;
		}

		// Token: 0x0200015D RID: 349
		public class ShowSmallHint : {18593}.ShowBasic
		{
			// Token: 0x060007E4 RID: 2020 RVA: 0x00040EBB File Offset: 0x0003F0BB
			public ShowSmallHint(string {18627}, {18593}.AfterClose {18628}, Action {18629}, params {17464}[] {18630}) : base({18627}, {18628}, {18629})
			{
				this.Slides = {18630};
			}

			// Token: 0x060007E5 RID: 2021 RVA: 0x00040ED0 File Offset: 0x0003F0D0
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach ({17464} {17464} in this.Slides)
				{
					stringBuilder.AppendLine({17464}.Text);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x0400070D RID: 1805
			public {17464}[] Slides;

			// Token: 0x0400070E RID: 1806
			[Nullable(new byte[]
			{
				2,
				0
			})]
			public Action<{17312}> Decorate;
		}
	}
}
