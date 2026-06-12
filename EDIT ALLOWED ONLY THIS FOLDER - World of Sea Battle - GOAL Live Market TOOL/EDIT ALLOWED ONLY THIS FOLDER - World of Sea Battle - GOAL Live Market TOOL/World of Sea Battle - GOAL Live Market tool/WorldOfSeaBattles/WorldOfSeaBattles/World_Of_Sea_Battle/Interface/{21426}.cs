using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000323 RID: 803
	internal sealed class {21426} : {17068}
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00092CD7 File Offset: 0x00090ED7
		private static int BaseWidth
		{
			get
			{
				return 750;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00092CDE File Offset: 0x00090EDE
		private static int BaseHeight
		{
			get
			{
				return 600;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x00092CE5 File Offset: 0x00090EE5
		public PlayerShipInfo Ship { get; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00092CF0 File Offset: 0x00090EF0
		public bool IsCostVisible
		{
			get
			{
				int num;
				int num2;
				return this.Ship.Coolness != PlayerShipCoolness.Default || Session.Account.Shipyard.IsRankResearched(this.Ship.Rank, this.Ship.Class, out num, out num2) > ShipResearchStatus.Red;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x00092D38 File Offset: 0x00090F38
		public bool HaveResourcesToBuild
		{
			get
			{
				if (this.Ship.Coolness == PlayerShipCoolness.Elite)
				{
					return true;
				}
				if (this.Ship.ID == 70)
				{
					return Session.Account.TreasuryMaps[72] >= this.{21480}[72];
				}
				return Session.Account.Gold >= this.{21482}.Value && Session.Account.NearPortStorage.CanRemove(this.{21480});
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00092DB6 File Offset: 0x00090FB6
		public bool CurrentPortHasEmpireShipbuilder
		{
			get
			{
				return Session.EventActionsPipeline.CurrentActions.Any(delegate(EventActionBase {21489})
				{
					EABehavior1 eabehavior = {21489}.Behavior as EABehavior1;
					return eabehavior != null && eabehavior.Category == EActionCaterory.BEmpireShipbuilderInPort && eabehavior.ArgumentInt == Global.Player.NearPort.PortID;
				});
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x00092DE6 File Offset: 0x00090FE6
		public bool NotAvailable
		{
			get
			{
				return PlatformTuning.DisablePremAnUniqueShips && (this.Ship.Coolness != PlayerShipCoolness.Default || this.Ship.Rank == 1);
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00092E10 File Offset: 0x00091010
		public {21426}(PlayerShipInfo {21430}, {21078} {21431}, int {21432})
		{
			Rectangle uiarea = Engine.GS.UIArea;
			base..ctor(Marker.FromCentrScreen(new Marker(ref uiarea).Offset((float)({21432} * 10), (float)({21432} * 20)), new Rectangle(0, 0, {21430}.StaticInfo.IsBalloon ? ({21426}.BaseWidth - 250) : {21426}.BaseWidth, {21426}.BaseHeight)), {17625}.c_back4, {17068}.BlockingWay.NoBackground, false);
			this.{21477} = {21431};
			this.Ship = {21430};
			this.AllowDragDrop = true;
			base.EvClick += this.{21450};
			this.{21435}();
			this.{21477}.EvRemoveFromContainer += this.{21452};
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00092EC4 File Offset: 0x000910C4
		public static void BuildShipFinishing(PlayerShipInfo {21433})
		{
			PlayerShipDynamicInfo prevShipName = Global.Player.UsedShipPlayer;
			PlayerShipDynamicInfo playerShipDynamicInfo = Session.Account.Shipyard.CreateNewShip({21433}, Session.Account, ({21433}.Coolness == PlayerShipCoolness.Elite) ? new int?(CommonSupport.DonationShipCannonsId[{21433}.MaxCannonCategory]) : null, true, {21433}.Coolness == PlayerShipCoolness.Elite, false);
			Global.Game.ScenePort.ShipsHolder.Add(playerShipDynamicInfo);
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.V_Victory, 0.03f, 1f);
			if ({21433}.StaticInfo.MortarPorts.Any((CannonLocationInfo {21490}) => !{21490}.AvailableWithUpgrade))
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_MortarShipBought, true);
			}
			string {17134};
			if ({21433}.StaticInfo.IsBalloon)
			{
				{17134} = Local.ballon_was_crafted;
			}
			else
			{
				int num = 1;
				string text = "";
				for (int i = 0; i < num; i++)
				{
					ShipUpgradeInfo randomUpgrade = {21426}.GetRandomUpgrade({21433});
					if (playerShipDynamicInfo.Upgrades.HasInstalledUpgradeByID((int)randomUpgrade.ID))
					{
						i--;
					}
					else
					{
						playerShipDynamicInfo.Upgrades.InstallUpgrade(playerShipDynamicInfo, randomUpgrade, i + 1);
						if (i == 0)
						{
							text = randomUpgrade.Name;
						}
						else
						{
							text = text + ", " + randomUpgrade.Name;
						}
					}
				}
				playerShipDynamicInfo.FirstHP.Restore(100000f, playerShipDynamicInfo.MaxHp);
				{17134} = Local.ship_was_crafted_1_up(text);
				{19994}.Logbook(Local.lbe_ship_build(playerShipDynamicInfo.CraftFrom.ShipName), LBFlags.L2);
			}
			if (Session.Account.Shipyard.List.Count == 2)
			{
				playerShipDynamicInfo.Crew.Add(Gameplay.UnitsInfo.FromID(1), playerShipDynamicInfo.CrewPlaces / 3, false);
			}
			for (int j = 0; j < 3; j++)
			{
				PowerupItemInfo powerupItemInfo = Global.Player.UsedShipPlayer.PowerupItemSlots[j];
				if (powerupItemInfo != null && powerupItemInfo.DisallowInLowRangShips.GetValueOrDefault(-1) >= playerShipDynamicInfo.CraftFrom.Rank)
				{
					playerShipDynamicInfo.PowerupItemSlots[j] = powerupItemInfo;
				}
			}
			Global.Game.ScenePort.ShipsHolder.See(playerShipDynamicInfo);
			Global.Game.ScenePort.MakeAccSync();
			Global.Network.Send(new OnAnalyticsEventClient(OnAnalyticsEventClient.Type.CraftShip, {21433}.ShipName, {21433}.Rank));
			if ({21433}.ID != 2)
			{
				EducationHelper.MakeFlag(EducationOnboarding.BuildNextShip, true);
				if (playerShipDynamicInfo.CraftFrom.Rank <= 5)
				{
					EducationHelper.MakeFlag(EducationOnboarding.BuildOrBuyShipRank5, true);
				}
			}
			if (EducationHelper.AskAboutTakingOffEquipmentForJustCraftedShip && !{21433}.StaticInfo.IsBalloon)
			{
				new {17107}({21433}.ShipName, {17134}, Local.ship_was_crafted_2_hig_add, delegate(int {21500})
				{
					if ({21500} == 0)
					{
						Session.Account.TakeOffAllEquipment(prevShipName, false);
					}
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_ChangeShips, true);
				}, true, null, new string[]
				{
					Local.ship_was_crafted_respA(prevShipName.CraftFrom.ShipName),
					Local.no
				});
				return;
			}
			new {17107}({21433}.ShipName, {17134}, Local.ship_was_crafted_2, delegate(int {21491})
			{
			}, true, null, new string[]
			{
				Local.ship_was_crafted_resp
			});
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x000931F8 File Offset: 0x000913F8
		public static ShipUpgradeInfo GetRandomUpgrade(PlayerShipInfo {21434})
		{
			ShipUpgradeInfo[] array = (from {21501} in Gameplay.ShipUpgradesInfo
			where {21501}.IsUpgradeAvailableIn({21434}, false) && {21501}.CategoryUi != ShipUpgradeCategory.Modification && {21501}.CategoryUi != ShipUpgradeCategory.Sailes && !{21501}.HasEffect(ShipBonusEffect.PBoardingGold) && !{21501}.HasEffect(ShipBonusEffect.MExtraUpgradePlaces) && !{21501}.HasEffect(ShipBonusEffect.PDamageCannonIfStrengthBelow30P) && !{21501}.HasEffect(ShipBonusEffect.PTiltReduce)
			select {21501}).ToArray<ShipUpgradeInfo>();
			return array[Rand.RangeInt(0, array.Length)];
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0009323C File Offset: 0x0009143C
		private void {21435}()
		{
			this.{21486} = false;
			Form form = this.{21478};
			if (form != null)
			{
				form.RemoveFromContainer();
			}
			base.PosWidth = (float){21426}.BaseWidth;
			base.PosHeight = (float){21426}.BaseHeight;
			PlayerShipInfo ship = this.Ship;
			Decorator game = Session.Game;
			ValueTuple<GSI, RTI, float> craftPrice = ship.GetCraftPrice(game, Session.EventActionsPipeline);
			this.{21480} = craftPrice.Item1;
			this.{21482} = craftPrice.Item2;
			this.{21483} = craftPrice.Item3;
			if (this.Ship.Coolness == PlayerShipCoolness.Elite)
			{
				ValueTuple<int, float> realBuyPrice = this.Ship.GetRealBuyPrice(Session.EventActionsPipeline);
				RTI rti = realBuyPrice.Item1;
				this.{21484} = rti;
				this.{21483} = realBuyPrice.Item2;
			}
			this.{21481} = this.{21480};
			bool flag = !WosbAchievements.CheckShipAchievementsCompleted(this.Ship.StaticInfo, Session.Account);
			int[] source = new int[]
			{
				72,
				73,
				74,
				75
			};
			if (flag || source.Contains((int)this.Ship.ID))
			{
				this.{21480} = new GSI();
				this.{21482} = 0;
			}
			PlayerShipInfo ship2 = this.Ship;
			float num = 1f;
			game = Session.Game;
			this.{21485} = Gameplay.ShipwayDuration(ship2, num - game.ShipBuildTimeBonus);
			Form form2 = this.{21436}();
			Form form3 = this.{21437}();
			Form form4 = this.{21440}();
			Form form5 = this.{21441}();
			float num2 = 10f;
			Vector2 vector = base.Pos.XY + new Vector2(num2);
			form2.Pos = form2.Pos.SetXY(vector);
			vector.Y += 10f + form2.Pos.WH.Y;
			form3.Pos = form3.Pos.SetXY(vector);
			vector.Y += 10f + form3.Pos.WH.Y;
			form4.Pos = form4.Pos.SetXY(vector);
			vector.Y += form4.Pos.WH.Y;
			form5.Pos = form5.Pos.SetXY(vector);
			vector.Y += form5.Pos.WH.Y;
			float num3 = new float[]
			{
				form2.Pos.WH.X,
				form3.Pos.WH.X,
				form4.Pos.WH.X,
				form5.Pos.WH.X
			}.Max();
			Rectangle uiarea = Engine.GS.UIArea;
			this.{21478} = new Form(new Marker(ref uiarea), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			this.{21478}.AddChild(form3);
			this.{21478}.AddChild(form2);
			this.{21478}.AddChild(form4);
			this.{21478}.AddChild(form5);
			base.AddChild(this.{21478});
			base.PosWidth = num3 + 2f * num2;
			base.PosHeight = vector.Y - (base.Pos.XY.Y + num2) + 2f * num2;
			this.{21487}.Pos = base.Pos;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000935B0 File Offset: 0x000917B0
		private Form {21436}()
		{
			Form form = new Form(base.Pos.WithHeight(170f), CommonAtlas.whitePixel, Color.Transparent, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Label {12952} = new Label(Vector2.Zero, Fonts.Philosopher_24, Color.Wheat, this.Ship.ShipName, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Form form2 = new Form(base.Pos.WithHeight(40f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form2.AddChildPos({12952}, PositionAlignment.Center, PositionAlignment.Center, 0f);
			stackForm.AddItem(new UiControl[]
			{
				form2
			});
			StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Rectangle fractionFlagPrerender = CommonAtlas.GetFractionFlagPrerender(this.Ship.Fraction);
			stackForm2.AddItem(new UiControl[]
			{
				new Image(new Marker(0f, 0f, ref fractionFlagPrerender).ScaleSize(0.55f), CommonAtlas.Texture.Tex, fractionFlagPrerender, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			if (this.Ship.Coolness == PlayerShipCoolness.Default)
			{
				string text = "";
				bool flag = true;
				StackForm stackForm3 = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				if (Global.Player.NearPort.IsArabPort)
				{
					flag = (this.Ship.Class != ShipClass.CargoShip);
					text = (flag ? Local.PortCraftShip_FractionWrongPortArab : "");
					stackForm2.ToolTipState = new ToolTipState("", Local.craft_by_arab_tt, Array.Empty<ToolTipCharacteristics>());
				}
				else if (this.Ship.Fraction.IsNation() && Session.Game.CanHaveShipCraftFractionFine())
				{
					flag = Session.Game.HasCraftShipFractionFine(this.Ship.Fraction);
					text = (flag ? Local.PortCraftShip_FractionWrongPort : Local.PortCraftShip_FractionGoodPort);
					stackForm2.ToolTipState = new ToolTipState("", Local.craft_by_fraction_tt(this.Ship.Fraction.GetName()), Array.Empty<ToolTipCharacteristics>());
				}
				if (!string.IsNullOrEmpty(text))
				{
					Color {13344} = flag ? new Color(255, 73, 79) : Color.YellowGreen;
					string[] array = text.Split(new char[]
					{
						':',
						'：'
					});
					stackForm3.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Arial_10, {13344}, array[0] + ":", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					stackForm3.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Arial_10, {13344}, array[1].Trim(), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
				}
				stackForm2.DisableDepthFocusTest = true;
				stackForm2.AddSpace(5f);
				stackForm2.AddItem(new UiControl[]
				{
					stackForm3
				});
			}
			form.AddChildPos(stackForm2, PositionAlignment.LeftUp, PositionAlignment.LeftUp, 10f);
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_14, 0f);
			if (this.Ship.IsSailageLegend)
			{
				textBlockBuilder.Write(" ☆ " + Local.sailage_legend, Color.Wheat);
				textBlockBuilder.WriteLine();
			}
			string str = (this.Ship.Coolness == PlayerShipCoolness.Empire) ? Local.PortCraftShip_ImperalShip : this.Ship.GetClassName();
			textBlockBuilder.Write(str + " ", Color.Wheat);
			textBlockBuilder.WriteImage(CommonAtlas.Texture.Tex, CommonAtlas.GetShipClassIcon(this.Ship), 0.45f, null);
			textBlockBuilder.Write(" " + this.Ship.SubclassName, Color.Wheat);
			textBlockBuilder.Write("  " + StringHelper.ToRoman(this.Ship.Rank), new Color(137, 160, 193), Fonts.Philosopher_14Bold, null, false);
			textBlockBuilder.Write(Local.StringConstants_115, Color.Wheat);
			TextBlockControl textBlockControl = textBlockBuilder.CreateCentroid(Vector2.Zero);
			Form form3 = new Form(new Marker(0f, 0f, base.Pos.WH.X, textBlockControl.Pos.WH.Y), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form3.AddChildPos(textBlockControl, PositionAlignment.LeftUp, PositionAlignment.Center, form3.Pos.WH.X / 2f);
			stackForm.AddItem(new UiControl[]
			{
				form3
			});
			TextBlockControl textBlockControl2 = TextBlockBuilder.CreateBlock(570f, this.Ship.Text, Color.Wheat * 0.45f, Fonts.Arial_10, -1f).CreateCentroid(Vector2.Zero);
			Form form4 = new Form(new Marker(0f, 0f, base.Pos.WH.X, textBlockControl2.Pos.WH.Y), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form4.AddChildPos(textBlockControl2, PositionAlignment.LeftUp, PositionAlignment.Center, form4.Pos.WH.X / 2f);
			stackForm.AddItem(new UiControl[]
			{
				form4
			});
			form.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.LeftUp, 10f);
			form.AddChildPos(new Button(new Marker(0f, 0f, 37f, 37f), CommonAtlas.whitePixel, new Color(55, 86, 71, 255) * 0.4f, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(new Action<ClickUiEventArgs>(this.{21453})).SetText("X", Fonts.Philosopher_16Bold, Color.White * 0.7f, false), PositionAlignment.RightDown, PositionAlignment.LeftUp, 10f);
			return form;
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00093B50 File Offset: 0x00091D50
		private Form {21437}()
		{
			Marker marker = base.Pos;
			Form form = new Form(marker.WithHeight(300f), CommonAtlas.whitePixel, Color.Black * 0.5f, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			Form form2 = new Form(new Marker(0f, 0f, 350f, 250f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Rectangle bounds = this.Ship.IconTextureWhitespace.Bounds;
			Vector2 vector = bounds.WidthHeight().Normal() * (float)(this.Ship.StaticInfo.IsBalloon ? 500 : 600);
			marker = new Marker(0f, 0f, ref vector);
			marker = marker.Offset(form2.Pos.WH.X / 2f - vector.X / 2f, -form2.Pos.WH.Y / 2f);
			marker = marker.Offset(0f, 10f);
			Vector2 vector2 = this.Ship.StaticInfo.IsBalloon ? new Vector2(-40f, 65f) : Vector2.Zero;
			marker = marker.Offset(vector2);
			Vector2 vector3 = (this.Ship.ID == 62) ? new Vector2(10f, 0f) : ((this.Ship.ID == 65) ? new Vector2(40f, 0f) : Vector2.Zero);
			Image {13204} = new Image(marker.Offset(vector3), this.Ship.IconTextureWhitespace, bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form2.AddChild({13204});
			this.{21487} = new Form(base.Pos, new Rectangle(426, 1737, 264, 215), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false,
				RenderToDepthMap = false
			};
			form2.AddChild(this.{21487});
			Form form3 = this.{21439}();
			bool flag = form3.Pos.WH.Y == 0f;
			form2.PosHeight = form2.Pos.WH.Y + (flag ? 0f : (form3.Pos.WH.Y - 45f));
			form2.AddChildPos(form3, PositionAlignment.Center, PositionAlignment.RightDown, 0f);
			UiControl uiControl = this.{21438}();
			form.PosHeight = Math.Max(form2.Pos.WH.Y, uiControl.Pos.WH.Y + 20f);
			form.AddChildPos(form2, PositionAlignment.LeftUp, PositionAlignment.Center, 20f);
			form.AddChildPos(uiControl, PositionAlignment.RightDown, PositionAlignment.Center, 20f);
			return form;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00093E10 File Offset: 0x00092010
		private UiControl {21438}()
		{
			{21426}.<>c__DisplayClass36_0 CS$<>8__locals1;
			CS$<>8__locals1.stack = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<float>(this.Ship.PatHealth, "0.#");
			{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler.ToStringAndClear(), Local.strength, Local.ship_stat_tt_1, ref CS$<>8__locals1);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler2.AppendFormatted<float>(this.Ship.PatSpeed * PlayerShipInfo.Temp_displaySpeedRefactoring, "0.#");
			{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler2.ToStringAndClear(), Local.speed, Local.ship_stat_tt_2, ref CS$<>8__locals1);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler3.AppendFormatted<float>(this.Ship.PatMobility, "0.#");
			{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler3.ToStringAndClear(), Local.PortСraftShipWindow_8, Local.ship_stat_tt_8, ref CS$<>8__locals1);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler4.AppendFormatted<float>(this.Ship.PatArmor, "0.#");
			{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler4.ToStringAndClear(), Local.armor_full, Local.ship_stat_tt_3, ref CS$<>8__locals1);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler5 = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler5.AppendFormatted<float>(this.Ship.PatCapacity, "0.#");
			{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler5.ToStringAndClear(), Local.hold, Local.ship_stat_tt_4, ref CS$<>8__locals1);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler6 = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler6.AppendFormatted<int>(this.Ship.PatPlacesUnits);
			{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler6.ToStringAndClear(), Local.crew, Local.ship_stat_tt_5, ref CS$<>8__locals1);
			ShipStaticInfo staticInfo = this.Ship.StaticInfo;
			if (!staticInfo.IsBalloon)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler7 = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler7.AppendFormatted<float>(staticInfo.CorpusShape.FinalLength, "0");
				defaultInterpolatedStringHandler7.AppendLiteral("x");
				defaultInterpolatedStringHandler7.AppendFormatted<float>(staticInfo.CorpusShape.MaxP.Y, "0");
				{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler7.ToStringAndClear(), Local.PortСraftShipWindow_9, Local.ship_stat_tt_6, ref CS$<>8__locals1);
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler8 = new DefaultInterpolatedStringHandler(0, 2);
				defaultInterpolatedStringHandler8.AppendFormatted<float>(staticInfo.Weight / 1000f * 45f, "0");
				defaultInterpolatedStringHandler8.AppendFormatted(Local.tonn);
				{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler8.ToStringAndClear(), Local.PortСraftShipWindow_weight, Local.ship_stat_tt_13, ref CS$<>8__locals1);
				if (staticInfo.LeftSidePorts.Length != 0)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler9 = new DefaultInterpolatedStringHandler(2, 3);
					defaultInterpolatedStringHandler9.AppendFormatted<int>(staticInfo.BackSidePorts.Length);
					defaultInterpolatedStringHandler9.AppendLiteral("-");
					defaultInterpolatedStringHandler9.AppendFormatted<int>(staticInfo.LeftSidePorts.Length);
					defaultInterpolatedStringHandler9.AppendLiteral("-");
					defaultInterpolatedStringHandler9.AppendFormatted<int>(staticInfo.FrontSidePorts.Length);
					{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler9.ToStringAndClear(), this.Ship.MaxCannonCategory.GetName(true) + " " + Local.PortСraftShipWindow_10.ToLower(), Local.PortСraftShipWindow_10_tt, ref CS$<>8__locals1);
				}
			}
			if (staticInfo.FalkonetPositions.Length != 0)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler10 = new DefaultInterpolatedStringHandler(0, 1);
				defaultInterpolatedStringHandler10.AppendFormatted<int>(staticInfo.FalkonetPositions.Length);
				{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler10.ToStringAndClear(), Local.PortСraftShipWindow_14, null, ref CS$<>8__locals1);
			}
			if (staticInfo.MortarPorts.Any((CannonLocationInfo {21492}) => {21492}.AvailableWithUpgrade))
			{
				{21426}.<ComposeStats>g__AddStat|36_0("", Local.ShipStatMortarModification, Local.ship_stat_tt_7(Global.Settings.kb_Mortar_ModifierKey.KeyToString, (this.Ship.MaxMortarPoundage != null) ? this.Ship.MaxMortarPoundage.Value : "-"), ref CS$<>8__locals1);
			}
			if (staticInfo.MortarPorts.Any((CannonLocationInfo {21493}) => !{21493}.AvailableWithUpgrade))
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler11 = new DefaultInterpolatedStringHandler(0, 1);
				defaultInterpolatedStringHandler11.AppendFormatted<int>(staticInfo.MortarPorts.Count((CannonLocationInfo {21494}) => !{21494}.AvailableWithUpgrade));
				{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler11.ToStringAndClear(), Local.PortСraftShipWindow_11 + ((this.Ship.MaxMortarPoundage != null) ? (" " + Local.max_caliber2(this.Ship.MaxMortarPoundage.Value)) : ""), Local.ship_stat_tt_7(Global.Settings.kb_Mortar_ModifierKey.KeyToString, (this.Ship.MaxMortarPoundage != null) ? this.Ship.MaxMortarPoundage.Value : "-"), ref CS$<>8__locals1);
			}
			if (staticInfo.SpecialCannonsCount > 0)
			{
				if (staticInfo.ID == 63)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler12 = new DefaultInterpolatedStringHandler(0, 1);
					defaultInterpolatedStringHandler12.AppendFormatted<int>(staticInfo.SpecialCannonsCount);
					{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler12.ToStringAndClear(), Local.ncs_cannon_23_name, null, ref CS$<>8__locals1);
				}
				else
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler13 = new DefaultInterpolatedStringHandler(0, 1);
					defaultInterpolatedStringHandler13.AppendFormatted<int>(staticInfo.SpecialCannonsCount);
					{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler13.ToStringAndClear(), Local.firegun, Local.ship_stat_tt_10, ref CS$<>8__locals1);
				}
			}
			if (this.Ship.MaxIntegrity != -1)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler14 = new DefaultInterpolatedStringHandler(0, 1);
				defaultInterpolatedStringHandler14.AppendFormatted<int>(this.Ship.MaxIntegrity);
				{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler14.ToStringAndClear(), Local.PortShipInfoGui_3, Local.ship_stat_tt_9 + Local.capital_integrity_tt, ref CS$<>8__locals1);
			}
			if (staticInfo.HasUniqueWindRose)
			{
				{21426}.<ComposeStats>g__AddStat|36_0("", Local.PortСraftShipWindow_15, Local.ship_stat_tt_11, ref CS$<>8__locals1);
			}
			if (staticInfo.PaddleModelInstances.Size > 0)
			{
				{21426}.<ComposeStats>g__AddStat|36_0("", Local.PortСraftShipWindow_15b, Local.ship_stat_tt_12, ref CS$<>8__locals1);
			}
			if (this.Ship.ExtraPlacesForUpgrades > 0)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler15 = new DefaultInterpolatedStringHandler(1, 1);
				defaultInterpolatedStringHandler15.AppendLiteral("+");
				defaultInterpolatedStringHandler15.AppendFormatted<int>(this.Ship.ExtraPlacesForUpgrades);
				{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler15.ToStringAndClear(), Local.StringConstants_259, null, ref CS$<>8__locals1);
			}
			if (this.Ship.BattleFarmingBonus > 0f)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler16 = new DefaultInterpolatedStringHandler(2, 1);
				defaultInterpolatedStringHandler16.AppendLiteral("+");
				defaultInterpolatedStringHandler16.AppendFormatted<float>(this.Ship.BattleFarmingBonus * 100f, "0");
				defaultInterpolatedStringHandler16.AppendLiteral("%");
				{21426}.<ComposeStats>g__AddStat|36_0(defaultInterpolatedStringHandler16.ToStringAndClear(), Local.shipo_farming_bonus, null, ref CS$<>8__locals1);
			}
			if (this.Ship.PowerupSkillId != 0)
			{
				{21426}.<ComposeStats>g__AddStat|36_0("", "\"" + this.Ship.PowerupSkill.Name + "\"", this.Ship.PowerupSkill.Description, ref CS$<>8__locals1);
			}
			if (this.Ship.Coolness == PlayerShipCoolness.Empire)
			{
				{21426}.<ComposeStats>g__AddStat|36_0("", Local.PortСraftShipWindow_emp, Local.PortСraftShipWindow_emp_tt, ref CS$<>8__locals1);
			}
			return CS$<>8__locals1.stack;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x000944B8 File Offset: 0x000926B8
		private Form {21439}()
		{
			{21426}.<>c__DisplayClass37_0 CS$<>8__locals1 = new {21426}.<>c__DisplayClass37_0();
			CS$<>8__locals1.<>4__this = this;
			Form form = new Form(new Marker(ref base.Pos.XY, 370f, 0f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			if (this.NotAvailable)
			{
				return form;
			}
			CS$<>8__locals1.stack = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			int num;
			int num2;
			ShipResearchStatus shipResearchStatus = Session.Account.Shipyard.IsRankResearched(this.Ship.Rank, this.Ship.Class, out num, out num2);
			Tlist<ShipUpgradeInstance> tlist = Session.Account.Shipyard.StoredUpgrades.Fetch((byte)this.Ship.ID, false);
			int num3 = (tlist != null) ? tlist.Size : 0;
			string text = (num3 > 0 && !Session.Account.Shipyard.ContainsInfo(this.Ship)) ? (Local.craftship_stored_upgrades(num3) + " " + Local.craftship_stored_upgrades2) : "";
			if (this.Ship.Coolness == PlayerShipCoolness.Default)
			{
				bool flag = this.Ship.Rank >= Session.Game.NearPortShipRankAllowToBuild || this.Ship.ID == 2;
				if (shipResearchStatus == ShipResearchStatus.Red)
				{
					CS$<>8__locals1.<ComposeExtraInfo>g__AddInfoBlock|0({21426}.InfoStyle.Red, Local.craftship_statusred_0 + " [?]", "", Local.craftship_statusred_red_tt, null);
					this.{21486} = true;
				}
				else if (shipResearchStatus == ShipResearchStatus.Yellow && (this.Ship.Rank != 6 || flag || Session.Account.Shipyard.ContainsInfo(this.Ship)))
				{
					float distanceFine = Session.Account.Shipyard.GetDistanceFine(this.Ship.Class);
					PlayerShipInfo playerShipInfo = (from {21507} in Gameplay.PlayersInfo
					where {21507}.Coolness == PlayerShipCoolness.Default && {21507}.Class == CS$<>8__locals1.<>4__this.Ship.Class && {21507}.Rank > CS$<>8__locals1.<>4__this.Ship.Rank
					select {21507}).MinBy((PlayerShipInfo {21495}) => {21495}.Rank);
					string {724} = ((playerShipInfo != null) ? playerShipInfo.ShipName : null) ?? "-";
					if (this.Ship.Class == Global.Player.UsedShipPlayer.CraftFrom.Class)
					{
						int num4 = (int)Math.Round((double)(Session.Account.CurrentShipXpMultiplier * (1f - distanceFine) * 100f));
						if (distanceFine > 0f)
						{
							CS$<>8__locals1.<ComposeExtraInfo>g__AddInfoBlock|0({21426}.InfoStyle.Yellow, Local.craftship_statusyellow_0(num2, num), Local.craftship_xpPercent1(num4) + " [?]", Local.craftship_xp_tt({724}), Local.craftship_xpPercent_tooltip_build(this.Ship.GetClassName()));
						}
						else if (num4 == 100)
						{
							CS$<>8__locals1.<ComposeExtraInfo>g__AddInfoBlock|0({21426}.InfoStyle.Yellow, Local.craftship_statusyellow_0(num2, num), Local.craftship_xpPercent1(num4), null, null);
						}
						else
						{
							CS$<>8__locals1.<ComposeExtraInfo>g__AddInfoBlock|0({21426}.InfoStyle.Yellow, Local.craftship_statusyellow_0(num2, num), Local.craftship_xpPercent1(num4) + " [?]", Local.craftship_xp_tt({724}), Local.craftship_xpPercent_tooltip(Global.Player.UsedShipPlayer.CraftFrom.ShipName, num4, Global.Player.UsedShipPlayer.Upgrades.InstalledCount));
						}
					}
					else
					{
						CS$<>8__locals1.<ComposeExtraInfo>g__AddInfoBlock|0({21426}.InfoStyle.Yellow, Local.craftship_statusyellow_0(num2, num) + " [?]", null, Local.craftship_xp_tt({724}), null);
					}
					this.{21486} = true;
				}
				else if (!flag)
				{
					CS$<>8__locals1.<ComposeExtraInfo>g__AddInfoBlock|0({21426}.InfoStyle.Red, Local.craftship_statuport_0(this.Ship.Rank), Local.craftship_statuport_1, null, null);
					this.{21486} = true;
				}
				else if (this.Ship.Coolness == PlayerShipCoolness.Default && num != 0)
				{
					CS$<>8__locals1.<ComposeExtraInfo>g__AddInfoBlock|0({21426}.InfoStyle.Green, Local.craftship_statusgreen(num), text, null, null).Opacity = 0.5f;
					text = string.Empty;
				}
			}
			if (text.Length > 0)
			{
				CS$<>8__locals1.<ComposeExtraInfo>g__AddInfoBlock|0({21426}.InfoStyle.Green, text, null, null, null);
			}
			if (this.Ship.Coolness == PlayerShipCoolness.Empire && !this.CurrentPortHasEmpireShipbuilder && (!this.Ship.StaticInfo.IsBalloon || !WosbAchievements.CheckShipAchievementsCompleted(this.Ship.StaticInfo, Session.Account)))
			{
				CS$<>8__locals1.<ComposeExtraInfo>g__AddInfoBlock|0({21426}.InfoStyle.Red, Local.PortCraftShip_NeedImperalShipBuilder, null, null, null);
				this.{21486} = true;
			}
			if (this.Ship.Coolness == PlayerShipCoolness.Elite && this.Ship.Rank == 1 && DonationSystem.Disable1RangShipsExperiment)
			{
				if (!Session.Account.Shipyard.List.Any((PlayerShipDynamicInfo {21496}) => {21496}.CraftFrom.Rank <= 2))
				{
					CS$<>8__locals1.<ComposeExtraInfo>g__AddInfoBlock|0({21426}.InfoStyle.Red, Local.buy1rank_limit_1, Local.buy1rank_limit_2, null, null);
					this.{21486} = true;
				}
			}
			if (Global.Player.NearPort.IsArabPort && this.Ship.Coolness == PlayerShipCoolness.Default && this.Ship.Class != ShipClass.CargoShip)
			{
				this.{21486} = true;
			}
			form.PosHeight = CS$<>8__locals1.stack.Pos.WH.Y;
			form.AddChildPos(CS$<>8__locals1.stack, PositionAlignment.Center, PositionAlignment.Center, 0f);
			return form;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x000949EC File Offset: 0x00092BEC
		private Form {21440}()
		{
			{21426}.<>c__DisplayClass38_0 CS$<>8__locals1 = new {21426}.<>c__DisplayClass38_0();
			CS$<>8__locals1.<>4__this = this;
			Form form = new Form(base.Pos.WithHeight(0f), CommonAtlas.whitePixel, Color.Transparent, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			if (this.NotAvailable)
			{
				form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_16Bold, Color.Wheat, Local.PortCraftShipWindow_NotAvailable, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 10f);
				form.PosHeight = 50f;
				return form;
			}
			CS$<>8__locals1.canBuyResToCraftShip = false;
			if (this.Ship.Coolness == PlayerShipCoolness.Elite)
			{
				form.AddChildPos(CS$<>8__locals1.<ComposePrice>g__CreatePriceTitle|0(this.{21483}, this.{21484}.Value), PositionAlignment.Center, PositionAlignment.LeftUp, 0f);
				Form form2 = new Form(base.Pos.WithHeight(50f), CommonAtlas.whitePixel, Color.Transparent, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat * 0.6f, Local.PortСraftShipWindow_16, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				stackForm.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat * 0.6f, Local.PortСraftShipWindow_16b, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				form2.AddChildPos(stackForm, PositionAlignment.LeftUp, PositionAlignment.Center, form.Pos.WH.X / 2f - 100f);
				form.AddChildPos(form2, PositionAlignment.Center, PositionAlignment.LeftUp, 25f);
				form.PosHeight = 80f;
				return form;
			}
			if (!WosbAchievements.CheckShipAchievementsCompleted(this.Ship.StaticInfo, Session.Account))
			{
				StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm2.AddItem(new UiControl[]
				{
					TextBlockBuilder.CreateBlock(500f, Local.PortCraftShipWindow_BalloonInfoTitle, Color.Wheat, Fonts.Philosopher_14Bold, 1f).Create(Vector2.Zero)
				});
				stackForm2.AddSpace(5f);
				foreach (ValueTuple<AchievementEnum, int> valueTuple in WosbAchievements.AchievementsForShips[(int)this.Ship.StaticInfo.ID])
				{
					AchievementEnum item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					AchievementDisplayInfo achievementDisplayInfo = Gameplay.AchievementsByEnum[item];
					int num = Session.Account.Achievements.Count(achievementDisplayInfo.AchievementEnum);
					string shipAchievementState = WosbAchievements.GetShipAchievementState(item, Session.Account, Session.Guild, Session.AuctionSummaryTradePerSession);
					TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_10, 1f);
					textBlockBuilder.Write("    ", Color.White);
					textBlockBuilder.WriteImage(achievementDisplayInfo.Icon, achievementDisplayInfo.Icon.Bounds, 0.25f, null);
					textBlockBuilder.Write(" " + achievementDisplayInfo.Name, Color.Yellow);
					if (num < item2)
					{
						TextBlockBuilder textBlockBuilder2 = textBlockBuilder;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
						defaultInterpolatedStringHandler.AppendLiteral(" ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(num);
						defaultInterpolatedStringHandler.AppendLiteral(" / ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(item2);
						textBlockBuilder2.Write(defaultInterpolatedStringHandler.ToStringAndClear(), Color.Yellow);
					}
					if (item2 == 1)
					{
						if (num < item2)
						{
							textBlockBuilder.Write("  " + shipAchievementState, Color.Gray);
						}
						else
						{
							textBlockBuilder.Write("  ✔", Color.Gray);
						}
					}
					TextBlockControl textBlockControl = textBlockBuilder.Create(Vector2.Zero);
					textBlockControl.ToolTipState = new ToolTipState(achievementDisplayInfo.Name, achievementDisplayInfo.Description, Array.Empty<ToolTipCharacteristics>());
					stackForm2.AddItem(new UiControl[]
					{
						textBlockControl
					});
					stackForm2.AddSpace(1f);
				}
				stackForm2.AddSpace(1f);
				stackForm2.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.Arial_10, Color.Wheat, Local.cost_build + ": " + this.{21481}.ResourceInfo.First<GSILocalEnumerablePair<ResourceInfo>>().ToStringClear<ResourceInfo>(), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				form.PosHeight = stackForm2.Pos.WH.Y;
				form.AddChildPos(stackForm2, PositionAlignment.Center, PositionAlignment.LeftUp, 10f);
				return form;
			}
			CS$<>8__locals1.showCounts = this.IsCostVisible;
			if (this.{21480}.IsEmpty && this.{21482}.Value == 0)
			{
				form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat, Local.unavailable_to_build, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 0f);
				form.Pos = form.Pos.WithHeight(25f);
				return form;
			}
			form.AddChildPos(CS$<>8__locals1.<ComposePrice>g__CreatePriceTitle|0(this.{21483}, 0), PositionAlignment.Center, PositionAlignment.LeftUp, 0f);
			CS$<>8__locals1.priceStack = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if (this.{21482}.Value > 0)
			{
				CS$<>8__locals1.<ComposePrice>g__AddCraftItem|1(CommonAtlas.Texture.Tex, this.{21482}.Value, Session.Account.Gold, new Rectangle?(CommonAtlas.goldIconMany64)).ToolTipState = new ToolTipState(Local.gold, null, Array.Empty<ToolTipCharacteristics>());
			}
			foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)this.{21480}.ResourceInfo))
			{
				int count = gsilocalEnumerablePair.Count;
				int num2 = (gsilocalEnumerablePair.Info.ID == 72) ? Session.Account.TreasuryMaps[(int)gsilocalEnumerablePair.Info.ID] : Session.Account.NearPortStorage[(int)gsilocalEnumerablePair.Info.ID];
				{20431}.Set(CS$<>8__locals1.<ComposePrice>g__AddCraftItem|1(gsilocalEnumerablePair.Info.IconTexture, count, num2, null), gsilocalEnumerablePair.Info, Math.Max(1, count - num2), null);
			}
			form.AddChildPos(CS$<>8__locals1.priceStack, PositionAlignment.Center, PositionAlignment.LeftUp, 30f);
			form.PosHeight = 95f;
			return form;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00095028 File Offset: 0x00093228
		private Form {21441}()
		{
			Form form = new Form(base.Pos.WithHeight(60f), CommonAtlas.whitePixel, Color.Transparent, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BorderThickness = 2f
			};
			Rectangle basicButton = CommonAtlas.basicButton;
			Color white = Color.White;
			Color color = Color.White * 0.7f;
			CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
			if (!Session.Account.Shipyard.ContainsInfo(this.Ship) && !PlatformTuning.DisableShop)
			{
				DonationSystem.PacketOffer packetId = DonationSystem.GetPacketIdWithShip((int)this.Ship.ID);
				if (packetId != null)
				{
					StackForm stackForm2 = stackForm;
					UiControl[] array = new UiControl[1];
					int num = 0;
					Vector2 zero = Vector2.Zero;
					Vector2 vector = new Vector2(158f, 37f);
					array[num] = new Button(new Marker(ref zero, ref vector), basicButton, white, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.PortСraftShipWindow_291, philosopher_, color, false).ExClick(delegate(ClickUiEventArgs {21515})
					{
						this.BlockAndClose();
						Global.Game.ScenePort.realShopHandler(null, packetId);
					});
					stackForm2.AddItem(array);
				}
			}
			stackForm.AddItem(new UiControl[]
			{
				new Button(new Marker(0f, 0f, 158f, 37f), basicButton, white, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.PortСraftShipWindow_1, philosopher_, color, false).ExClick(new Action<ClickUiEventArgs>(this.{21459}))
			});
			if (!this.NotAvailable && (this.Ship.Coolness == PlayerShipCoolness.Elite || !this.{21480}.IsEmpty || this.{21482}.Value > 0))
			{
				{21426}.<>c__DisplayClass39_1 CS$<>8__locals2 = new {21426}.<>c__DisplayClass39_1();
				CS$<>8__locals2.<>4__this = this;
				CS$<>8__locals2.applyButton = new Button(new Marker(0f, 0f, 158f, 37f), basicButton, white, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AllowMouseInput = !this.{21486},
					BasicColor = (this.{21486} ? Color.Gray : Color.White)
				};
				if (!this.HaveResourcesToBuild && CS$<>8__locals2.applyButton.AllowMouseInput)
				{
					if (this.IsCostVisible)
					{
						CS$<>8__locals2.<ComposeButtons>g__SetText|3();
						CS$<>8__locals2.applyButton.EvClick += delegate(ClickUiEventArgs {21516})
						{
							if (Global.Settings.TrackingNotes.Contains(new Func<LogbookTrackingNote, bool>(CS$<>8__locals2.<>4__this.{21463})))
							{
								Global.Settings.TrackingNotes.Remove(new Func<LogbookTrackingNote, bool>(CS$<>8__locals2.<>4__this.{21465}));
							}
							else
							{
								Tlist<LogbookTrackingNote> trackingNotes = Global.Settings.TrackingNotes;
								LogbookTrackingNote logbookTrackingNote = LogbookTrackingNote.CreateShiptracking((int)CS$<>8__locals2.<>4__this.Ship.ID);
								trackingNotes.Add(logbookTrackingNote);
							}
							{21516}.Sender.BrightnessBlinkingMode = false;
							base.<ComposeButtons>g__SetText|3();
						};
						if (this.Ship.Coolness == PlayerShipCoolness.Default && this.Ship.Rank == 7 && !Global.Settings.TrackingNotes.Contains(new Func<LogbookTrackingNote, bool>(this.{21467})))
						{
							CS$<>8__locals2.applyButton.Brightness -= 0.1f;
							CS$<>8__locals2.applyButton.BrightnessBlinkingMode = true;
						}
					}
					else
					{
						CS$<>8__locals2.applyButton.Opacity = 0.5f;
					}
				}
				else
				{
					string text = (this.Ship.Coolness == PlayerShipCoolness.Elite) ? Local.shop : Local.Build;
					if (this.{21485}.Value > 0f && CS$<>8__locals2.applyButton.AllowMouseInput)
					{
						Color color2 = color * 0.4f;
						string text2 = " (" + StringHelper.TimeDHM((double)this.{21485}.Value, false) + ")";
						StackForm stackForm3 = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						stackForm3.AddItem(new UiControl[]
						{
							new Label(Vector2.Zero, philosopher_, this.{21486} ? (color * 0.4f) : color, text, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
						stackForm3.AddItem(new UiControl[]
						{
							new Label(Vector2.Zero, philosopher_, this.{21486} ? (color2 * 0.4f) : color2, text2, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
						float x = philosopher_.Measure(text + text2).X;
						CS$<>8__locals2.applyButton.PosWidth = Math.Max(CS$<>8__locals2.applyButton.Pos.XY.X, x + 20f);
						CS$<>8__locals2.applyButton.AddChildPos(stackForm3, PositionAlignment.Center, PositionAlignment.Center, 0f);
					}
					else
					{
						CS$<>8__locals2.applyButton.SetText(text, philosopher_, this.{21486} ? (color * 0.7f) : color, false);
					}
					CS$<>8__locals2.applyButton.EvClick += this.{21469};
				}
				stackForm.AddItem(new UiControl[]
				{
					CS$<>8__locals2.applyButton
				});
			}
			if (Session.Account.Shipyard.ContainsInfo(this.Ship))
			{
				stackForm.AddItem(new UiControl[]
				{
					new Button(new Marker(0f, 0f, 158f, 37f), basicButton, white, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.select, philosopher_, color, false).ExClick(new Action<ClickUiEventArgs>(this.{21471}))
				});
			}
			form.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.RightDown, 10f);
			return form;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00095520 File Offset: 0x00093720
		private {21426}.BuildError {21442}()
		{
			{21426}.BuildError buildError = {21426}.BuildError.None;
			if (this.Ship.RequiredCraftInGuildHavePort && (Session.Guild == null || Session.Game.NearPortStatus.CapturedBy != Session.Guild.Tag))
			{
				buildError |= {21426}.BuildError.RequiredOwnGuildPort;
			}
			if (this.Ship.Coolness == PlayerShipCoolness.Elite && Session.Account.Monets.Value < this.{21484}.Value)
			{
				buildError |= {21426}.BuildError.NotEnoughMonets;
			}
			if (this.Ship.Coolness != PlayerShipCoolness.Elite && Session.Account.ShipPlacesLeft <= 0)
			{
				buildError |= {21426}.BuildError.NoFreeShipSlot;
			}
			if (this.{21485}.Value > 0f)
			{
				if (Session.Account.Shipyard.Shipways.Any((Shipway {21497}) => (int){21497}.PortID == Global.Player.NearPort.PortID))
				{
					buildError |= {21426}.BuildError.OtherBuildInProgress;
				}
			}
			if (this.Ship.Coolness == PlayerShipCoolness.Empire && !this.CurrentPortHasEmpireShipbuilder)
			{
				buildError |= {21426}.BuildError.NoEmpireShipBuilder;
			}
			if (!this.HaveResourcesToBuild)
			{
				buildError |= {21426}.BuildError.NotEnoughGoldOrResources;
			}
			return buildError;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0009562C File Offset: 0x0009382C
		private bool {21443}(out int {21444}, out GSI {21445}, out int {21446})
		{
			if (this.{21486} || this.Ship.Coolness == PlayerShipCoolness.Elite || this.{21442}() != {21426}.BuildError.NotEnoughGoldOrResources || !PlatformTuning.EnableContextOffers)
			{
				{21444} = 0;
				{21445} = GSI.Empty;
				{21446} = 0;
				return false;
			}
			{21444} = Math.Max(0, this.{21482}.Value - Session.Account.Gold);
			{21445} = this.{21480}.ExceptWith(Session.Account.NearPortStorage);
			int num = {21445}.TotalPrice();
			{21446} = (int)Math.Ceiling((double)(((float){21444} + 1.5f * (float)num) / 1125f));
			{21446} = (({21446} % 5 == 0) ? {21446} : ({21446} + (5 - {21446} % 5)));
			int num2 = this.{21482}.Value + this.{21480}.TotalPrice();
			return (float)({21444} + num) / (float)num2 < 0.4f;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00095704 File Offset: 0x00093904
		private void {21447}()
		{
			GSI needRes;
			int needGold;
			int monets;
			if (this.{21443}(out needGold, out needRes, out monets))
			{
				{17312}.AskPriceMonets(Local.BuyResToCraftShip_Dialog, monets, delegate
				{
					Session.Account.NearPortStorage.Add(needRes);
					Session.Account.Gold += needGold;
					Global.Network.Send(new OnAnalyticsEventClient(OnAnalyticsEventClient.Type.BuyItem, "buy_ship_res", monets));
					{19994}.Logbook(Local.BuyResToCraftShip_Logbook(this.Ship.ShipName, monets), LBFlags.L1);
					this.{21448}();
				}, true);
			}
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0009575C File Offset: 0x0009395C
		private void {21448}()
		{
			{21426}.BuildError buildError = this.{21442}();
			if (buildError.HasFlag({21426}.BuildError.RequiredOwnGuildPort))
			{
				new {17312}(Local.PortСraftShipWindow_2);
				return;
			}
			if (buildError.HasFlag({21426}.BuildError.NotEnoughMonets))
			{
				base.RemoveFromContainer();
				Global.Game.ScenePort.realShopHandler(null, null);
				{20881}.ShowBuyMonetsToolTip(this.{21484}.Value);
				return;
			}
			if (buildError.HasFlag({21426}.BuildError.NoFreeShipSlot))
			{
				DonationSystem.AskForBuyPlaceInPort();
				return;
			}
			if (buildError.HasFlag({21426}.BuildError.OtherBuildInProgress))
			{
				Shipway shipway;
				if (Session.Account.Shipyard.Shipways.TryFind((Shipway {21498}) => (int){21498}.PortID == Global.Player.NearPort.PortID, out shipway))
				{
					new {17312}(Local.shipway_limit_error(shipway.ShipInfo.ShipName));
				}
				return;
			}
			if (buildError.HasFlag({21426}.BuildError.NoEmpireShipBuilder))
			{
				new {17312}(Local.PortCraftShip_NeedImperalShipBuilder);
				return;
			}
			if (buildError.HasFlag({21426}.BuildError.NotEnoughGoldOrResources))
			{
				return;
			}
			base.AllowMouseInput = false;
			base.RemoveFromContainer();
			Global.Game.ScenePort.mainHandler(null);
			if (this.Ship.Coolness == PlayerShipCoolness.Elite)
			{
				PlayerAccount account = Session.Account;
				account.Monets.Value = account.Monets.Value - this.{21484}.Value;
				Global.Shopstat("elship: " + this.Ship.ShipName, this.{21484}.Value);
				Session.Account.MaxShipsCount.Value = Math.Min(Session.Account.MaxShipsCount.Value + 1, Session.Account.HardShipPlacesLimit);
			}
			else
			{
				Session.Account.NearPortStorage.TryRemove(this.{21480});
				Session.Account.TreasuryMaps.TryRemove(this.{21480});
				Session.Account.Gold -= this.{21482}.Value;
			}
			Global.Settings.TrackingNotes.Remove(new Func<LogbookTrackingNote, bool>(this.{21475}));
			if (this.{21485}.Value == 0f)
			{
				{21426}.BuildShipFinishing(this.Ship);
				return;
			}
			Tlist<Shipway> shipways = Session.Account.Shipyard.Shipways;
			Shipway shipway2 = new Shipway((byte)Global.Player.NearPort.PortID, (byte)this.Ship.ID, this.{21485}.Value);
			shipways.Add(shipway2);
			new {17107}(this.Ship.ShipName, Local.ship_crafting_started, "", delegate(int {21499})
			{
			}, true, null, new string[]
			{
				Local.to_continue
			});
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000201BB File Offset: 0x0001E3BB
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(CommonAtlas.Texture);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000201CC File Offset: 0x0001E3CC
		protected override void UserFrontRender()
		{
			Engine.GS.ReturnBackTexture();
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00095A2A File Offset: 0x00093C2A
		protected override void UserUpdate(ref FrameTime {21449})
		{
			if ({17177}.CurrentInstance != null)
			{
				this.{21479} = true;
			}
			else if (this.{21479})
			{
				this.{21479} = false;
				this.{21435}();
			}
			if (base.IsVisible)
			{
				base.UserUpdate(ref {21449});
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00095A79 File Offset: 0x00093C79
		[CompilerGenerated]
		private void {21450}(ClickUiEventArgs {21451})
		{
			base.MoveToFrontLevel();
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00095A81 File Offset: 0x00093C81
		[CompilerGenerated]
		private void {21452}()
		{
			base.RemoveFromContainer();
			this.{21477} = null;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0007A010 File Offset: 0x00078210
		[CompilerGenerated]
		private void {21453}(ClickUiEventArgs {21454})
		{
			base.BlockAndClose();
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00095A90 File Offset: 0x00093C90
		[CompilerGenerated]
		internal static Form <ComposeStats>g__AddStat|36_0(string {21455}, string {21456}, string {21457} = null, ref {21426}.<>c__DisplayClass36_0 {21458})
		{
			{21455} = (string.IsNullOrEmpty({21455}) ? "✔" : {21455}.Trim());
			{21456} = {21456}.Trim();
			Form form = new Form(new Marker(0f, 0f, 300f, 20f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Label label = new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat * 0.8f, {21456}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				ToolTip = (string.IsNullOrEmpty({21457}) ? null : new ToolTip(new ToolTipState("", {21457}, Array.Empty<ToolTipCharacteristics>())))
			};
			form.AddChildPos(label, PositionAlignment.LeftUp, PositionAlignment.Center, 0f);
			Label label2 = new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat * 0.8f, {21455}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos(label2, PositionAlignment.RightDown, PositionAlignment.Center, 0f);
			if (!string.IsNullOrEmpty({21456}))
			{
				float num = label.Pos.WH.X + 2f;
				float num2 = form.Pos.WH.X - num - label2.Pos.WH.X - 4f + 5f;
				num += num2 % 5f / 2f;
				num2 -= num2 % 5f;
				while (num2 > 0f)
				{
					float num3 = Math.Min(num2, (float){21426}.c_dotsLine.Width);
					Form form2 = new Form(form.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					Image {12952} = new Image(new Marker(0f, 0f, num3, 5f), AtlasPortGui.Texture.Tex, {21426}.c_dotsLine.SetWidth(num3), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					form2.AddChildPos({12952}, PositionAlignment.Disabled, PositionAlignment.RightDown, 4f);
					form.AddChildPos(form2, PositionAlignment.LeftUp, PositionAlignment.Center, num);
					num2 -= num3;
					num += num3;
				}
			}
			{21458}.stack.AddItem(new UiControl[]
			{
				form
			});
			return form;
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00095C7E File Offset: 0x00093E7E
		[CompilerGenerated]
		private void {21459}(ClickUiEventArgs {21460})
		{
			this.{21477}.ShowShipPreview(this);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00095C8C File Offset: 0x00093E8C
		[CompilerGenerated]
		private bool {21461}(LogbookTrackingNote {21462})
		{
			return {21462}.IsShipTracking((int)this.Ship.ID);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00095C8C File Offset: 0x00093E8C
		[CompilerGenerated]
		private bool {21463}(LogbookTrackingNote {21464})
		{
			return {21464}.IsShipTracking((int)this.Ship.ID);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00095C8C File Offset: 0x00093E8C
		[CompilerGenerated]
		private bool {21465}(LogbookTrackingNote {21466})
		{
			return {21466}.IsShipTracking((int)this.Ship.ID);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00095C8C File Offset: 0x00093E8C
		[CompilerGenerated]
		private bool {21467}(LogbookTrackingNote {21468})
		{
			return {21468}.IsShipTracking((int)this.Ship.ID);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00095C9F File Offset: 0x00093E9F
		[CompilerGenerated]
		private void {21469}(ClickUiEventArgs {21470})
		{
			this.{21448}();
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00095CA8 File Offset: 0x00093EA8
		[CompilerGenerated]
		private void {21471}(ClickUiEventArgs {21472})
		{
			PlayerShipDynamicInfo playerShipDynamicInfo = Session.Account.Shipyard.List.FirstOrDefault(new Func<PlayerShipDynamicInfo, bool>(this.{21473}), null);
			if (playerShipDynamicInfo != null)
			{
				Global.Game.ScenePort.ShipsHolder.See(playerShipDynamicInfo);
			}
			this.{21477}.Close();
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00095CFC File Offset: 0x00093EFC
		[CompilerGenerated]
		private bool {21473}(PlayerShipDynamicInfo {21474})
		{
			short? num = ({21474} != null) ? new short?({21474}.CraftFrom.ID) : null;
			int? num2 = (num != null) ? new int?((int)num.GetValueOrDefault()) : null;
			int id = (int)this.Ship.ID;
			return num2.GetValueOrDefault() == id & num2 != null;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00095C8C File Offset: 0x00093E8C
		[CompilerGenerated]
		private bool {21475}(LogbookTrackingNote {21476})
		{
			return {21476}.IsShipTracking((int)this.Ship.ID);
		}

		// Token: 0x04000FF2 RID: 4082
		private static readonly Rectangle c_dotsLine = new Rectangle(983, 617, 100, 5);

		// Token: 0x04000FF3 RID: 4083
		private const bool DebugShowDesign = false;

		// Token: 0x04000FF4 RID: 4084
		private {21078} {21477};

		// Token: 0x04000FF5 RID: 4085
		private Form {21478};

		// Token: 0x04000FF6 RID: 4086
		private bool {21479};

		// Token: 0x04000FF7 RID: 4087
		private GSI {21480};

		// Token: 0x04000FF8 RID: 4088
		private GSI {21481};

		// Token: 0x04000FF9 RID: 4089
		private RTI {21482};

		// Token: 0x04000FFA RID: 4090
		private float {21483};

		// Token: 0x04000FFB RID: 4091
		private RTI {21484};

		// Token: 0x04000FFC RID: 4092
		private RTIf {21485};

		// Token: 0x04000FFD RID: 4093
		private bool {21486};

		// Token: 0x04000FFE RID: 4094
		private Form {21487};

		// Token: 0x04000FFF RID: 4095
		[CompilerGenerated]
		private readonly PlayerShipInfo {21488};

		// Token: 0x02000324 RID: 804
		private enum InfoStyle
		{
			// Token: 0x04001001 RID: 4097
			Red,
			// Token: 0x04001002 RID: 4098
			Yellow,
			// Token: 0x04001003 RID: 4099
			Green
		}

		// Token: 0x02000325 RID: 805
		[Flags]
		private enum BuildError
		{
			// Token: 0x04001005 RID: 4101
			None = 0,
			// Token: 0x04001006 RID: 4102
			RequiredOwnGuildPort = 1,
			// Token: 0x04001007 RID: 4103
			NotEnoughMonets = 2,
			// Token: 0x04001008 RID: 4104
			NoFreeShipSlot = 4,
			// Token: 0x04001009 RID: 4105
			OtherBuildInProgress = 8,
			// Token: 0x0400100A RID: 4106
			NoEmpireShipBuilder = 16,
			// Token: 0x0400100B RID: 4107
			NotEnoughGoldOrResources = 32
		}
	}
}
