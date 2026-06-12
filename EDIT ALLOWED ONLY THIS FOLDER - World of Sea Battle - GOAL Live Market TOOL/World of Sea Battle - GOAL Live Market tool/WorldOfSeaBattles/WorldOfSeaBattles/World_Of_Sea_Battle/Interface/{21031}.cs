using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002DF RID: 735
	internal sealed class {21031} : {17625}
	{
		// Token: 0x06001024 RID: 4132 RVA: 0x00087E10 File Offset: 0x00086010
		public {21031}() : base(820f, {17625}.c_back3, {17604}.InGameWindow, {17625}.c_icStreeringWheel, new {17625}.DynamicTittle[]
		{
			Global.Game.ScenePort.QuesthouseName
		})
		{
			this.{21051} = new TextureHost(AtlasPortGui.Texture.Tex, base.Pos);
			base.AddChild(this.{21051});
			this.{21032}(false);
			base.EvRemoveFromContainer += this.{21049};
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00087E98 File Offset: 0x00086098
		private void {21032}(bool {21033})
		{
			this.{21051}.ClearAllChild();
			StackForm stackForm = new StackForm(this.{21051}.Pos.XY + new Vector2(432f, 29f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				this.{21039}(new Rectangle(102, 1123, 50, 50), Local.guild_company, Local.guild_company_tt, Session.Guild != null && Session.Game.NearPortRelation == Relation.Ally, new Action(this.{21045}))
			});
			stackForm.AddSpace(5f);
			stackForm.AddItem(new UiControl[]
			{
				this.{21039}(new Rectangle(51, 1123, 50, 50), Local.WorldMapUi_downTt_clerk, Local.clerk_trading_route_desc, true, new Action(this.{21050}))
			});
			this.{21051}.AddChild(stackForm);
			StackForm stackForm2 = new StackForm(default(Vector2), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm2.AddItem(new UiControl[]
			{
				this.{21034}(Local.company_battle, Local.company_battle_tt, QuestCompany.Battle, {21033})
			});
			stackForm2.AddItem(new UiControl[]
			{
				this.{21034}(Local.company_trading, Local.company_trading_tt, QuestCompany.Trading, {21033})
			});
			stackForm2.AddItem(new UiControl[]
			{
				this.{21034}(Local.company_coastal, Local.company_coastal_tt, QuestCompany.Coastal, {21033})
			});
			this.{21051}.AddChildPos(stackForm2, PositionAlignment.Center, PositionAlignment.LeftUp, 80f);
			Rectangle {13194} = new Rectangle(3418, 912, 281, 56);
			Form form = new Form(new Marker(0f, 0f, ref {13194}).Scale(1.8f), {13194}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			QuestInfo[] array = Session.Account.Quests.GetAvailableQuests(Global.Player.NearPort, Global.Player).ToArray<QuestInfo>();
			UiControl uiControl = form;
			Vector2 {13342} = default(Vector2);
			CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
			Color {13344} = Color.Wheat * 0.8f;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
			defaultInterpolatedStringHandler.AppendFormatted(Local.taverna_count_quests);
			defaultInterpolatedStringHandler.AppendLiteral(": ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(array.Length);
			uiControl.AddChildPos(new Label({13342}, philosopher_, {13344}, defaultInterpolatedStringHandler.ToStringAndClear(), PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.Center, 0f);
			this.{21051}.AddChildPos(form, PositionAlignment.Center, PositionAlignment.RightDown, 30f);
			if ({21033})
			{
				Global.Game.SoundSystem.PlaySound(GameStaticSoundName.BattleResults, 0.03f, 0.6f);
			}
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00088118 File Offset: 0x00086318
		private Form {21034}(string {21035}, string {21036}, QuestCompany {21037}, bool {21038})
		{
			Rectangle {13194} = new Rectangle(3419, 388, 274, 521);
			Form form = new Form(new Marker(0f, 0f, ref {13194}).Scale(1f), {13194}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form.AddChildPos(new Label(default(Vector2), Fonts.Philosopher_16, new Color(243, 242, 181), {21035}, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 18f);
			form.AddChildPos(TextBlockBuilder.CreateBlock(form.Pos.WH.X - 30f, {21036}, Color.Gray, Fonts.Philosopher_12, 0f).CreateCentroid(), PositionAlignment.Center, PositionAlignment.LeftUp, 10f, 50f, false);
			QuestInfo[] array = (from {21055} in Session.Account.Quests.GetOpenedQuests(Global.Player.NearPort, new QuestCompany?({21037}), true, true)
			orderby {21055}.ID
			select {21055}).ToArray<QuestInfo>();
			int num = 0;
			int num2 = 0;
			QuestInfo[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				QuestInfo notch = array2[i];
				bool flag = Session.Account.Quests.ProgressRunningQuests.Any((QuestRunningProgress {21062}) => {21062}.Info.ID == notch.ID);
				int num3;
				bool flag2 = Session.Account.Quests.CheckDisable(notch.ID, out num3);
				Image image = new Image(new Marker((float)(38 + num * 64), 117f, 66f, 66f), OtherTextures.WorldMapUiElements, notch.Icon, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Rectangle? rectangle = flag2 ? new Rectangle?(new Rectangle(2662, 0, 27, 27)) : (flag ? new Rectangle?(new Rectangle(2718, 0, 27, 28)) : null);
				if (rectangle != null)
				{
					image.Opacity = 0.5f;
					image.AddChildPos(new Form(Vector2.Zero, rectangle.Value, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					}, PositionAlignment.RightDown, PositionAlignment.RightDown, 0f);
				}
				image.ToolTipState = QuestHelper.GetQuestToolTip(false, notch);
				form.AddChild(image);
				num++;
				if ({21038} && {21037} == this.{21052})
				{
					image.Opacity = 0f;
					new UiActionsSleep(image, 1f);
					new UiActor(image, delegate()
					{
						new UiMarkerAndOpacityAnimation(image, 0f, 1f, image.Pos.Border(15f), image.Pos, 800f, UiAmimationCurve.SquaredToEnd);
						new UiBrightnessAnimation(image, 4f, 800f);
						new UiBrightnessAnimation(image, 1f, 1200f);
					});
				}
				if (flag2)
				{
					num2++;
				}
			}
			bool flag3 = array.Length <= 2 || num2 > 0;
			string {12778} = ({21037} == QuestCompany.Trading) ? Local.PortCompanyWindow_10 : (({21037} == QuestCompany.Battle) ? Local.PortCompanyWindow_11 : Local.PortCompanyWindow_12);
			string {13617};
			Button button;
			if (!Session.Account.Quests.GetAvailableQuests(Global.Player.NearPort, Global.Player).Any((QuestInfo {21060}) => {21060}.Company == {21037}))
			{
				{13617} = Local.taverna_no_quests;
				button = new Button(default(Vector2), AtlasPortGui.buttonYellow158, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button.SetText(Local.taverna_no_quests_short, Fonts.Philosopher_14, new Color(201, 220, 255), false);
			}
			else if (flag3)
			{
				{13617} = Local.taverna_click_to_get;
				button = new Button(default(Vector2), AtlasPortGui.buttonBlue158, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button.SetText(Local.taverna_click_to_get_short, Fonts.Philosopher_14, new Color(201, 220, 255), false);
				button.EvClick += delegate(ClickUiEventArgs {21061})
				{
					this.{21052} = {21037};
					if (Session.Account.Quests.TryReopenCategory(Global.Player.NearPort, Global.Player, {21037}))
					{
						if (Session.Account.Quests.GetOpenedQuests(Global.Player.NearPort, new QuestCompany?({21037}), true, true).Count<QuestInfo>() == 3)
						{
							{21031}.SpecialUnitChanse();
						}
						this.{21032}(true);
					}
				};
				button.ToolTipState = new ToolTipState("", {12778}, Array.Empty<ToolTipCharacteristics>());
			}
			else
			{
				int price = 1;
				{13617} = Local.taverna_update_desc;
				button = new Button(default(Vector2), AtlasPortGui.buttonYellow158, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				StackForm stackForm = new StackForm(default(Vector2), UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm.AddItem(new UiControl[]
				{
					new Label(default(Vector2), Fonts.Philosopher_14, new Color(255, 238, 198), Local.taverna_update + " -" + price.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				stackForm.AddSpace(3f);
				stackForm.AddItem(new UiControl[]
				{
					new Image(new Marker(0f, 0f, 18f, 18f), Gameplay.ItemsInfo[35].IconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				button.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.Center, 0f);
				button.EvClick += delegate(ClickUiEventArgs {21063})
				{
					this.{21052} = {21037};
					if (Session.Account.PassingMapScrolls < price)
					{
						return;
					}
					if (Session.Account.Quests.TryReopenCategory(Global.Player.NearPort, Global.Player, {21037}))
					{
						{21031}.SpecialUnitChanse();
						GSI treasuryMaps = Session.Account.TreasuryMaps;
						treasuryMaps[35] = treasuryMaps[35] - price;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
						defaultInterpolatedStringHandler.AppendFormatted(Local.taverna_click_to_get_short);
						defaultInterpolatedStringHandler.AppendLiteral(": ");
						defaultInterpolatedStringHandler.AppendFormatted(Gameplay.ItemsInfo[35].Name);
						defaultInterpolatedStringHandler.AppendLiteral(" -");
						defaultInterpolatedStringHandler.AppendFormatted<int>(price);
						{19994}.Logbook(defaultInterpolatedStringHandler.ToStringAndClear(), LBFlags.L0);
						this.{21032}(true);
					}
				};
				CharacteristicsColor {12729} = (Session.Account.PassingMapScrolls < price) ? CharacteristicsColor.Orange : CharacteristicsColor.Lime;
				button.ToolTipState = new ToolTipState(Local.taverna_click_to_get_short, {12778}, new ToolTipCharacteristics[]
				{
					new ToolTipCharacteristics(Local.scrolls_need + ":", price.ToString(), {12729}),
					new ToolTipCharacteristics(Local.scrolls_have + ":", Session.Account.PassingMapScrolls.ToString(), {12729})
				});
			}
			form.AddChildPos(button, PositionAlignment.Center, PositionAlignment.LeftUp, 209f);
			form.AddChildPos(TextBlockBuilder.CreateBlock(form.Pos.WH.X - 50f, {13617}, Color.Gray, Fonts.Philosopher_12, 0f).CreateCentroid(), PositionAlignment.Center, PositionAlignment.LeftUp, 10f, 263f, false);
			return form;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00088738 File Offset: 0x00086938
		private Button {21039}(Rectangle {21040}, string {21041}, string {21042}, bool {21043}, Action {21044})
		{
			Button button = new Button(Vector2.Zero, new Rectangle(200, 215, 197, 36), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button.AddChildPos(new Form(new Marker(0f, 0f, 36f, 36f), {21040}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			}, PositionAlignment.Center, PositionAlignment.LeftUp, -20f);
			button.SetText({21041}, Fonts.Philosopher_14Bold, Color.Black, false);
			if (!string.IsNullOrEmpty({21042}))
			{
				button.ToolTipState = new ToolTipState("", {21042}, Array.Empty<ToolTipCharacteristics>());
			}
			if ({21043})
			{
				button.EvClick += delegate(ClickUiEventArgs {21064})
				{
					{21044}();
				};
			}
			else
			{
				button.Opacity = 0.5f;
			}
			return button;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00088804 File Offset: 0x00086A04
		private void {21045}()
		{
			if (Session.Guild == null || Session.MyMemberInGuild.Rights.RankId < 3)
			{
				new {17312}(Local.GuildWindow_3b(GuildAccessRightsInfo.AllRights[3].DisplayName));
				return;
			}
			IslePortInfo nearPort = Global.Player.NearPort;
			QuestInfo[] quests = (from {21065} in Gameplay.QuestsInfo
			where {21065}.Company == QuestCompany.War && {21065}.LocationPort == nearPort
			select {21065}).ToArray<QuestInfo>();
			string[] {17139} = (from {21056} in quests
			select {21056}.GetName(Session.Account)).Concat(new <>z__ReadOnlyArray<string>(new string[]
			{
				Local.PortCompanyWindow_36,
				Local.PortCompanyWindow_37,
				Local.PortCompanyWindow_38
			})).ToArray<string>();
			new {17107}(Local.guild_company, Local.PortCompanyWindow_39, Local.PortCompanyWindow_40, delegate(int {21066})
			{
				if ({21066} >= quests.Length)
				{
					return;
				}
				QuestInfo {19785} = quests[{21066}];
				new {19779}(false, null, {19785});
			}, true, null, {17139});
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x000888FC File Offset: 0x00086AFC
		private void {21046}(float {21047} = 4000f)
		{
			{21031}.<>c__DisplayClass7_0 CS$<>8__locals1 = new {21031}.<>c__DisplayClass7_0();
			CS$<>8__locals1.distance = {21047};
			CS$<>8__locals1.<>4__this = this;
			if ({22913}.liveTrading == null)
			{
				{22913}.TryToOpen();
				new UiActionsSleep(this, 1000f);
				new UiActor(this, delegate()
				{
					base.<OpenClerck>g__OpenDialog|1(true);
				});
				return;
			}
			CS$<>8__locals1.<OpenClerck>g__OpenDialog|1(true);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x00088954 File Offset: 0x00086B54
		private static void SpecialUnitChanse()
		{
			Decorator game = Session.Game;
			UnitInfo unitInfo = WosbCrew.TryGetTavernaSpecialUnit(game, Global.Player);
			if (unitInfo != null)
			{
				string text = Local.lbe_newunit(unitInfo.Name);
				new {17312}(Local.radnom_meet_text + ": " + text);
				{19994}.Logbook(text, LBFlags.L1);
				Session.Account.SpecialUnitsAtStorage.Add(unitInfo);
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x000889B4 File Offset: 0x00086BB4
		private static void StartSacrafice(int {21048})
		{
			RTI price = Math.Min(100, 40 + {21048});
			new {17107}(Local.PortCompanyWindow_sacr, Local.sacrafice_desc, Local.sacrafice_desc_b1(Global.Player.ResourcesOfHold[8]) + Environment.NewLine + Local.sacrafice_desc_b2, delegate(int {21077})
			{
				if ({21077} == 1)
				{
					return;
				}
				if (Global.Player.ResourcesOfHold[8] == 0)
				{
					new {17107}(Local.PortCompanyWindow_sacr, Local.sacrafice_desc_b1(Global.Player.ResourcesOfHold[8]), "", delegate(int {21058})
					{
					}, true, null, new string[]
					{
						Local.close
					});
					return;
				}
				GSI resourcesOfHold = Global.Player.ResourcesOfHold;
				int num = resourcesOfHold[8];
				resourcesOfHold[8] = num - 1;
				Session.Account.Gold += price.Value;
				{21031}.StartSacrafice({21048} + 1);
			}, true, null, new string[]
			{
				Local.sacrafice_desc_b3(price.Value),
				Local.close
			});
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00088A56 File Offset: 0x00086C56
		[CompilerGenerated]
		private void {21049}()
		{
			if (this.IsClosedByHand && Session.Account.Quests.VisibleQuestsCount > 0)
			{
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_ExitHavingVisibleQuests, true);
			}
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00088A7A File Offset: 0x00086C7A
		[CompilerGenerated]
		private void {21050}()
		{
			this.{21046}(4000f);
		}

		// Token: 0x04000EDB RID: 3803
		private TextureHost {21051};

		// Token: 0x04000EDC RID: 3804
		private QuestCompany {21052};
	}
}
