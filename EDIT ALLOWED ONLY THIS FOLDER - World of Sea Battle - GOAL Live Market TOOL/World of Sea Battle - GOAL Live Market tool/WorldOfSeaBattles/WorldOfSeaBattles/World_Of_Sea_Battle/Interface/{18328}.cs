using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Data;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000130 RID: 304
	internal class {18328} : {17625}
	{
		// Token: 0x060006F6 RID: 1782 RVA: 0x00036B9C File Offset: 0x00034D9C
		public {18328}(PbsBuildingStatus {18331}, PbsStatus {18332}) : base(760f, {17625}.c_back1, (Global.Game.InteractiveWorldSystem.ContainsGuildFort != null) ? {17604}.InGameWindowBlockShip : {17604}.InGameWindowBlockShip, {17625}.c_icShield, new {17625}.DynamicTittle[]
		{
			new {17625}.DynamicTittle(Local.GuidFortManagingUi_main),
			new {17625}.DynamicTittle(Local.GuidFortManagingUi_status),
			new {17625}.DynamicTittle(Local.GuidFortManagingUi_towers)
		})
		{
			this.{18374} = {18331};
			this.{18375} = {18332};
			bool flag = false;
			this.{18377} = {18328}.ChangeRistrictions.None;
			if (!Session.MyMemberInGuild.Rights.AllowGiveFortStorage && !Session.MyMemberInGuild.Rights.AllowViewFortStorage && !Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury)
			{
				this.{18377} = {18328}.ChangeRistrictions.WatchOnlyNoRights;
			}
			else if ({18332}.IsBlockedByTensityOrBattle || flag)
			{
				this.{18377} = {18328}.ChangeRistrictions.WatchOnlyWarTime;
			}
			else if (Global.Game.InteractiveWorldSystem.ContainsGuildFort == null || Session.Account.WorldFlag.Mapback() == OpenWorldFlag.Peaceful || Session.Account.WorldFlag.Mapback() == OpenWorldFlag.Trader || Global.Player.IsDestroyed)
			{
				this.{18377} = {18328}.ChangeRistrictions.WatchOnlyWithoutPeaceFlagOrFar;
			}
			else if (Session.MyMemberInGuild.Rights.AllowGiveFortStorage)
			{
				this.{18377} = (Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury ? {18328}.ChangeRistrictions.None : {18328}.ChangeRistrictions.FullStorageOnly);
			}
			else
			{
				this.{18377} = {18328}.ChangeRistrictions.AppendStorageOnly;
			}
			this.{18378} = {18328}.ChangeRistrictions.None;
			if (!Session.MyMemberInGuild.Rights.AllowEditGuildAndFortAndTreasury)
			{
				this.{18378} = {18328}.ChangeRistrictions.WatchOnlyNoRights;
			}
			else if ({18332}.IsBlockedByTensityOrBattle || flag)
			{
				this.{18378} = {18328}.ChangeRistrictions.WatchOnlyWarTime;
			}
			{18328}.CurrentInstance = this;
			this.AnimatedFocus = false;
			this.{18373} = new Tab(base.Pos, Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(this.{18373});
			this.{18335}();
			this.{18376} = false;
			base.EvRemoveFromContainer += delegate()
			{
				{18328}.CurrentInstance = null;
			};
			base.OnTitleItemSelected += delegate({17625}.RoutingEventArgs<int> {18358})
			{
				this.{18373}.Select({18358}.Payload);
			};
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00036DA9 File Offset: 0x00034FA9
		internal void ExternalUpdate(PbsBuildingStatus {18333}, PbsStatus {18334})
		{
			base.AllowMouseInput = true;
			this.{18374} = {18333};
			this.{18375} = {18334};
			this.{18335}();
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00036DC8 File Offset: 0x00034FC8
		private void {18335}()
		{
			int {14113} = 0;
			if (this.{18373}.GetPagesCount != 0)
			{
				{14113} = this.{18373}.SelectedIndex;
				this.{18373}.Clear();
			}
			this.{18373}.Add(new Form[]
			{
				this.{18340}()
			});
			this.{18373}.Add(new Form[]
			{
				this.{18342}()
			});
			this.{18373}.Add(new Form[]
			{
				this.{18341}()
			});
			this.{18373}.Select({14113});
			if (this.{18377} != {18328}.ChangeRistrictions.None)
			{
				this.{18336}(0, this.{18377}, this.{18377} == {18328}.ChangeRistrictions.FullStorageOnly || this.{18377} == {18328}.ChangeRistrictions.AppendStorageOnly);
				for (int i = 2; i < this.{18373}.GetPagesCount; i++)
				{
					this.{18336}(i, this.{18377}, false);
				}
			}
			if (this.{18378} != {18328}.ChangeRistrictions.None)
			{
				this.{18336}(1, this.{18378}, false);
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00036EBC File Offset: 0x000350BC
		private void {18336}(int {18337}, {18328}.ChangeRistrictions {18338}, bool {18339})
		{
			Form form = new Form({18339} ? base.Pos.Offset(0f, 55f).SetWidth(390f) : base.Pos.Offset(0f, 55f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Color color = new Color(255, 66, 21) * 0.5764706f;
			form.AddChild({18328}.c_bannerDownRed, new Marker(5f, 477f, 794f, 74f).Offset(base.Pos.XY));
			Form form2 = form;
			Vector2 {13342} = base.Pos.XY + new Vector2(400f, 512f);
			CustomSpriteFont philosopher_14Bold = Fonts.Philosopher_14Bold;
			Color {13344} = color;
			bool flag = {18338} - {18328}.ChangeRistrictions.AppendStorageOnly <= 1;
			form2.AddChild(new Label({13342}, philosopher_14Bold, {13344}, flag ? Local.GuidFortManagingUi_0_a : Local.GuidFortManagingUi_0, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			string {13345} = "";
			if ({18338} == {18328}.ChangeRistrictions.WatchOnlyWarTime)
			{
				{13345} = Local.GuidFortManagingUi_1;
			}
			if ({18338} == {18328}.ChangeRistrictions.WatchOnlyWithoutPeaceFlagOrFar)
			{
				{13345} = Local.GuidFortManagingUi_2;
			}
			if ({18338} == {18328}.ChangeRistrictions.WatchOnlyNoRights)
			{
				{13345} = Local.GuidFortManagingUi_right1;
			}
			if ({18338} == {18328}.ChangeRistrictions.AppendStorageOnly)
			{
				{13345} = Local.GuidFortManagingUi_right2;
			}
			if ({18338} == {18328}.ChangeRistrictions.FullStorageOnly)
			{
				{13345} = Local.GuidFortManagingUi_right3;
			}
			form.AddChild(new Label(base.Pos.XY + new Vector2(400f, 532f), Fonts.Arial_10, color, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			form.EvClick += delegate(ClickUiEventArgs {18379})
			{
				{18379}.Sender.RemoveAnimations();
				new UiBrightnessAnimation({18379}.Sender, 1f, 10f, 300f);
				new UiBrightnessAnimation({18379}.Sender, 10f, 1f, 400f);
			};
			this.{18373}.GetPage({18337}).AddChild(form);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00037074 File Offset: 0x00035274
		private Form {18340}()
		{
			Form form = new Form(base.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			Form form2 = form;
			Rectangle c_page_background = {18233}.c_page_background;
			Marker marker = new Marker(76f, 55f, 724f, 498f);
			Marker marker2 = base.Pos;
			form2.AddChild(c_page_background, marker.Offset(marker2.XY));
			if (this.{18374} != null && this.{18374}.HoldResources != null && Session.MyMemberInGuild.Rights.AllowViewFortStorage)
			{
				{19275} {19275} = new {19275}(this.{18374}, true)
				{
					AllowOnlyToRight = (this.{18377} == {18328}.ChangeRistrictions.AppendStorageOnly),
					UseScissor = true
				};
				{19275}.TexturePath = Rectangle.Empty;
				UiControl uiControl = {19275};
				marker2 = {19275}.Pos;
				Vector2 vector = base.Pos.XY + new Vector2(352f, -86f);
				marker2 = marker2.SetXY(vector);
				uiControl.Pos = marker2.SetHeight({19275}.Pos.WH.Y + 40f);
				{19275}.Opacity = ((this.{18377} == {18328}.ChangeRistrictions.None || this.{18377} == {18328}.ChangeRistrictions.AppendStorageOnly || this.{18377} == {18328}.ChangeRistrictions.FullStorageOnly) ? 1f : 0.5f);
				marker2 = {19275}.Pos;
				Form form3 = new Form(marker2.SetHeight(586f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					UseScissor = true
				};
				form3.AddChild({19275});
				form.AddChild(form3);
			}
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_12, 0f);
			if (this.{18374} != null)
			{
				textBlockBuilder.WriteLine(Local.GuidFortManagingUi_11((int)this.{18374}.Strength, this.{18374}.MaxStrength), {18328}.lightTextCol);
				if (WosbPbs.MendingAmountPerHour > 0f)
				{
					textBlockBuilder.WriteLine(Local.GuidFortManagingUi_35 + Math.Round((double)(WosbPbs.MendingAmountPerHour * 100f)).ToString() + "% " + Local.per_hour, {18328}.lightTextCol);
					if (this.{18374}.IsFort)
					{
						float num = this.{18375}.Buildings.Sum((PbsBuildingStatus {18380}) => {18380}.MaxStrength - {18380}.Strength);
						if (num > 0f)
						{
							int num2 = (int)Math.Ceiling((double)(num * (float)WosbPbs.GuildMendingPricePer1k.Item2 / 1000f));
							textBlockBuilder.WriteLine(Local.full_mending_1, {18328}.lightTextCol);
							textBlockBuilder.WriteLine(Local.full_mending_2 + num2.ToString(), {18328}.lightTextCol);
						}
					}
				}
				if (Session.Guild.HasMintAndSalary)
				{
					Vector2 vector = base.Pos.XY + {18328}.c_descPos + new Vector2(-3f, textBlockBuilder.Size.Y + 10f);
					Image image = new Image(new Marker(ref vector, ref {20217}.c_node_taxes), CommonAtlas.Texture.Tex, {20217}.c_node_taxes, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					Label {12952} = new Label(base.Pos.XY + {18328}.c_descPos + new Vector2(0f, textBlockBuilder.Size.Y + 10f), Fonts.Philosopher_14, {18328}.lightTextCol * 1.2f, Local.accumulated_mint(this.{18375}.AccumulatedMoney), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					image.ToolTipState = new ToolTipState(Local.GuildWindow_28, Local.gamewiki_treasury_text4, Array.Empty<ToolTipCharacteristics>());
					image.AddChildPos({12952}, PositionAlignment.LeftUp, PositionAlignment.Center, 50f);
					form.AddChild(image);
				}
			}
			form.AddChild(textBlockBuilder.Create(base.Pos.XY + {18328}.c_descPos));
			StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(4f, 264f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if (this.{18374} != null)
			{
				if (Session.Guild.HasMintAndSalary)
				{
					Button button = new Button(Vector2.Zero, {18233}.c_itemButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetLiveText(() => "     " + Local.quick_money_transfer, Fonts.Philosopher_14, {18328}.lightTextCol, true);
					UiControl uiControl2 = button;
					string {12777} = "";
					string {12778} = Local.quick_money_transfer_tt(10, 3);
					ToolTipCharacteristics[] {12779};
					if (this.{18375}.BlockMoneyTransferTimeout <= 0f)
					{
						{12779} = new ToolTipCharacteristics[0];
					}
					else
					{
						({12779} = new ToolTipCharacteristics[1])[0] = new ToolTipCharacteristics(Local.available_after(StringHelper.TimeDHM((double)this.{18375}.BlockMoneyTransferTimeout, false)), CharacteristicsColor.Orange);
					}
					uiControl2.ToolTipState = new ToolTipState({12777}, {12778}, {12779});
					stackForm.AddItem(new UiControl[]
					{
						button
					});
					if (this.{18375}.BlockMoneyTransferTimeout > 0f || this.{18375}.AccumulatedMoney == 0)
					{
						button.Opacity = 0.5f;
					}
					else
					{
						button.EvClick += this.{18359};
					}
				}
				if (this.{18374}.IsFort)
				{
					Button button2 = new Button(Vector2.Zero, {18233}.c_itemButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText("     " + Local.quick_mending, Fonts.Philosopher_14, {18328}.lightTextCol, true);
					button2.ExToolTip(new ToolTip(new ToolTipState(Local.quick_mending, Local.GuidFortManagingUi_mendingTt, Array.Empty<ToolTipCharacteristics>())));
					button2.EvClick += this.{18361};
					stackForm.AddItem(new UiControl[]
					{
						button2
					});
				}
				WorldObjectID groupID = this.{18374}.Place.GroupID;
				if (this.{18374}.Place.GroupID == WorldObjectID.WGuildFort)
				{
					Button button3 = new Button(Vector2.Zero, {18233}.c_itemButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetLiveText(new Func<string>(this.{18363}), Fonts.Philosopher_14, {18328}.lightTextCol, true);
					stackForm.AddItem(new UiControl[]
					{
						button3
					});
					button3.EvClick += this.{18364};
					Button button4 = new Button(Vector2.Zero, {18233}.c_itemButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetLiveText(new Func<string>(this.{18368}), Fonts.Philosopher_14, {18328}.lightTextCol, true);
					stackForm.AddItem(new UiControl[]
					{
						button4
					});
					button4.EvClick += this.{18369};
				}
			}
			if (this.{18376} && this.{18374} != null && this.{18374}.IsFort)
			{
				int num3 = 0;
				foreach (UiControl uiControl3 in ((IEnumerable<UiControl>)stackForm.GetChildren))
				{
					float opacity = uiControl3.Opacity;
					uiControl3.Opacity = 0f;
					new UiActionsSleep(uiControl3, (float)(400 + num3 * 300));
					new UiOpacityAnimation(uiControl3, 0f, opacity, 300f);
					if (uiControl3.Brightness == 1f)
					{
						new UiBrightnessAnimation(uiControl3, 1f, 2f, 200f);
						new UiBrightnessAnimation(uiControl3, 2f, 1f, 300f);
					}
					num3++;
				}
			}
			form.AddChild(stackForm);
			return form;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x000377CC File Offset: 0x000359CC
		private Form {18341}()
		{
			Form form = new Form(base.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			Vector2 vector = default(Vector2);
			Vector2 vector2 = new Vector2(float.MaxValue);
			Vector2 vector3 = new Vector2(float.MinValue);
			foreach (PbsBuildingStatus pbsBuildingStatus in ((IEnumerable<PbsBuildingStatus>)this.{18375}.Buildings))
			{
				vector2 = Vector2.Min(vector2, pbsBuildingStatus.Place.Position);
				vector3 = Vector2.Max(vector3, pbsBuildingStatus.Place.Position);
			}
			vector = 0.5f * (vector2 + vector3);
			Vector2 vector4 = vector3 - vector2;
			float num = Math.Max(vector4.X, vector4.Y) * 2f + 100f;
			Marker contentArea = base.ContentArea;
			{22094}.CreateMapWithMask(form, contentArea, vector, num);
			using (IEnumerator<PbsBuildingStatus> enumerator = (from {18385} in this.{18375}.Buildings
			where {18385}.IsTower
			select {18385}).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PbsBuildingStatus item = enumerator.Current;
					AnimatedButton animatedButton = {22094}.IconHelper(item.Place.Position, num, contentArea, vector, new Rectangle(531, 162, 39, 39), new Rectangle(571, 162, 39, 39), 1.2f);
					Image image = new Image(animatedButton.Pos, OtherTextures.WorldMapUiElements, {22913}.TowerTexPath(item.HpFactor), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					animatedButton.RemoveFromContainer();
					image.EvClick += delegate(ClickUiEventArgs {18389})
					{
						Tlist<{17473}.Item> tlist = new Tlist<{17473}.Item>();
						using (Dictionary<InitialDynamicBuildingId, PbsTowerDescription>.Enumerator enumerator2 = WosbPbs.TowerDesription.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								KeyValuePair<InitialDynamicBuildingId, PbsTowerDescription> item2 = enumerator2.Current;
								Tlist<{17473}.Item> tlist2 = tlist;
								{17473}.Item item = new {17473}.Item(null, WosbPbs.TowerNameByIndex(item2.Key), item2.Key != item.CustomizationModel, default(ImageDecription), new ToolTipState("", {18328}.towerPropHelper(item2.Key), this.TTPriceHelper(WosbPbs.FortTowerBuildCost(), 0, 0).ToArray<ToolTipCharacteristics>()), delegate()
								{
									this.{18355}(new OnBuildingGuildOperationMsg(OnBuildingGuildOperationMsg.OpType.BuildTower, (uint)item2.Key, item.uIDDynamicServerBuild, item.Place.Position));
								});
								tlist2.Add(item);
							}
						}
						new {17473}(delegate(object {18386})
						{
						}, tlist.ToArray());
					};
					UiControl uiControl = image;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler.AppendLiteral("[");
					defaultInterpolatedStringHandler.AppendFormatted<int>(item.TowerNumber);
					defaultInterpolatedStringHandler.AppendLiteral("] ");
					defaultInterpolatedStringHandler.AppendFormatted(item.UiName);
					string {12777} = defaultInterpolatedStringHandler.ToStringAndClear();
					string {12778} = "";
					ToolTipCharacteristics[] array = new ToolTipCharacteristics[3];
					int num2 = 0;
					string worldMapUi_ = Local.WorldMapUi_50;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler2.AppendFormatted<int>((int)item.Strength);
					defaultInterpolatedStringHandler2.AppendLiteral(" / ");
					defaultInterpolatedStringHandler2.AppendFormatted<float>(item.MaxStrength);
					array[num2] = new ToolTipCharacteristics(worldMapUi_, defaultInterpolatedStringHandler2.ToStringAndClear());
					array[1] = new ToolTipCharacteristics(Local.GuidFortManagingUi_35, Local.GuidFortManagingUi_PerHour(Math.Round((double)(WosbPbs.MendingAmountPerHour * 100f))));
					array[2] = new ToolTipCharacteristics(Local.click_to_change, CharacteristicsColor.LimeBold);
					uiControl.ToolTipState = new ToolTipState({12777}, {12778}, array);
					Rectangle {13273} = (item.CustomizationModel == InitialDynamicBuildingId.FortTowerFire) ? new Rectangle(445, 617, 49, 49) : ((item.CustomizationModel == InitialDynamicBuildingId.FortTowerCannons) ? new Rectangle(495, 617, 49, 49) : new Rectangle(545, 617, 49, 49));
					image.AddChild(new Image(image.Pos, OtherTextures.WorldMapUiElements, {13273}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					form.AddChild(image);
				}
			}
			return form;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00037B5C File Offset: 0x00035D5C
		private Form {18342}()
		{
			{18328}.<>c__DisplayClass20_0 CS$<>8__locals1 = new {18328}.<>c__DisplayClass20_0();
			CS$<>8__locals1.<>4__this = this;
			Form form = new Form(base.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			form.AddChild({18233}.c_page_background, new Marker(76f, 55f, 724f, 498f).Offset(base.Pos.XY));
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_12, 0f);
			textBlockBuilder.WriteLines(Local.GuidFortManagingUi_statusDesc, {18328}.lightTextCol, Fonts.Arial_12, 367f, new float?(0f));
			form.AddChild(textBlockBuilder.Create(base.Pos.XY + {18328}.c_descPos));
			StackForm stackForm = new StackForm(new Vector2(10f, 150f) + base.Pos.XY, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			string text = ((double)this.{18375}.TimeToBattleCompletion + this.{18375}.TimeToBeginBattle > 0.0 || this.{18375}.Tensity.Effects.BlockFortWindowActions) ? Local.GuidFortManagingUi_prot_error1 : null;
			{18328}.<>c__DisplayClass20_0 CS$<>8__locals2 = CS$<>8__locals1;
			int[] levels;
			if (this.{18375}.grownCity != 1)
			{
				if (this.{18375}.grownCity != 2)
				{
					RuntimeHelpers.InitializeArray(levels = new int[4], fieldof(<PrivateImplementationDetails>.CF97ADEEDB59E05BFD73A2B4C2A8885708C4F4F70C84C64B27120E72AB733B72).FieldHandle);
				}
				else
				{
					RuntimeHelpers.InitializeArray(levels = new int[3], fieldof(<PrivateImplementationDetails>.4636993D3E1DA4E9D6B8F87B79E8F7C6D018580D52661950EABC3845C5897A4D).FieldHandle);
				}
			}
			else
			{
				int[] array = new int[2];
				array[0] = 1;
				levels = array;
				array[1] = 2;
			}
			CS$<>8__locals2.levels = levels;
			CS$<>8__locals1.protectionPrices = (from {18387} in CS$<>8__locals1.levels
			select WosbPbs.TensityProtectionPrice({18387}, Session.Game.NumCapturedPortsMyFraction)).ToArray<ValueTuple<int, int>>();
			CS$<>8__locals1.localizedPrices = CS$<>8__locals1.protectionPrices.Select(delegate([TupleElementNames(new string[]
			{
				"goldIngots",
				"conquerorBadges"
			})] ValueTuple<int, int> {18388})
			{
				string text2 = {18388}.Item1.ToString() + " " + Local.ingots;
				if ({18388}.Item2 > 0)
				{
					string str = text2;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler.AppendLiteral(", ");
					defaultInterpolatedStringHandler.AppendFormatted<int>({18388}.Item2);
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted(Local.conq_badges_short);
					text2 = str + defaultInterpolatedStringHandler.ToStringAndClear();
				}
				return text2;
			}).ToArray<string>();
			CS$<>8__locals1.lines = CS$<>8__locals1.levels.Select(delegate(int {18390})
			{
				int tensityProtectionDuration = CS$<>8__locals1.<>4__this.{18375}.GetTensityProtectionDuration({18390});
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 6);
				defaultInterpolatedStringHandler.AppendFormatted<int>(tensityProtectionDuration);
				defaultInterpolatedStringHandler.AppendFormatted(Local.StringConstants_64);
				defaultInterpolatedStringHandler.AppendLiteral(" (");
				defaultInterpolatedStringHandler.AppendFormatted(Local.until);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(new LocalizedDateTime(LocalizedDateTime.NowServerTime.AddHours((double)tensityProtectionDuration), false).GetDateAndTimeWithoutYear());
				defaultInterpolatedStringHandler.AppendLiteral(")");
				defaultInterpolatedStringHandler.AppendFormatted(Environment.NewLine);
				defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.localizedPrices[{18390} - 1]);
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}).ToArray<string>();
			this.ProtectionFormHelper(stackForm, Local.guild_protection_title, Local.GuidFortManagingUi_protSdesc, (this.{18375}.BlockAnyProtectionTimeout > 0f) ? Local.GuidFortManagingUi_prot_error2(StringHelper.TimeDHM((double)(this.{18375}.BlockAnyProtectionTimeout * 3600f), false)) : text, delegate
			{
			}, this.{18375}.TimeToProtectionTensity, null, new ValueTuple<int, int, Action, string>?(new ValueTuple<int, int, Action, string>(0, 0, delegate()
			{
				string empty = string.Empty;
				string {17134} = Local.guild_protection_desc(24);
				string guild_protection_desc_duration = Local.guild_protection_desc_duration;
				Action<int> {17136};
				if (({17136} = CS$<>8__locals1.<>9__12) == null)
				{
					{17136} = (CS$<>8__locals1.<>9__12 = delegate(int {18391})
					{
						if ({18391} == CS$<>8__locals1.levels.Length)
						{
							return;
						}
						if (CS$<>8__locals1.protectionPrices[{18391}].Item2 > Session.Guild.ConquerBadges)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 3);
							defaultInterpolatedStringHandler.AppendFormatted(Local.GuildWindow_50);
							defaultInterpolatedStringHandler.AppendLiteral(" (");
							defaultInterpolatedStringHandler.AppendFormatted<int>(Session.Guild.ConquerBadges);
							defaultInterpolatedStringHandler.AppendLiteral(" / ");
							defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.protectionPrices[{18391}].Item2);
							defaultInterpolatedStringHandler.AppendLiteral(")");
							new {17107}(defaultInterpolatedStringHandler.ToStringAndClear(), "");
							return;
						}
						if (CS$<>8__locals1.protectionPrices[{18391}].Item1 > Session.Guild.Treasury)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(6, 3);
							defaultInterpolatedStringHandler2.AppendFormatted(Local.gold_ingots_not_enough);
							defaultInterpolatedStringHandler2.AppendLiteral(" (");
							defaultInterpolatedStringHandler2.AppendFormatted<int>(Session.Guild.Treasury);
							defaultInterpolatedStringHandler2.AppendLiteral(" / ");
							defaultInterpolatedStringHandler2.AppendFormatted<int>(CS$<>8__locals1.protectionPrices[{18391}].Item1);
							defaultInterpolatedStringHandler2.AppendLiteral(")");
							new {17107}(defaultInterpolatedStringHandler2.ToStringAndClear(), "");
							return;
						}
						CS$<>8__locals1.<>4__this.{18355}(new OnBuildingGuildOperationMsg(OnBuildingGuildOperationMsg.OpType.BuyTensityProtection, (uint)({18391} + 1), CS$<>8__locals1.<>4__this.{18374}.uIDDynamicServerBuild, default(Vector2)));
					});
				}
				new {17107}(empty, {17134}, guild_protection_desc_duration, {17136}, true, null, CS$<>8__locals1.lines.Concat(new string[]
				{
					Local.to_back
				}).ToArray<string>());
			}, string.Empty)));
			this.ProtectionFormHelper(stackForm, Local.GuidFortManagingUi_boostFactory, Local.GuidFortManagingUi_boostFactoryd_desc(WosbPbs.FactoryBoostTimeoutDays, WosbPbs.GFactoryTempBoostPrice), null, delegate
			{
			}, this.{18375}.TemporaryFactoryBoostTimeout * 3600f, null, new ValueTuple<int, int, Action, string>?(new ValueTuple<int, int, Action, string>(0, WosbPbs.GFactoryTempBoostPrice, delegate()
			{
				CS$<>8__locals1.<>4__this.{18355}(new OnBuildingGuildOperationMsg(OnBuildingGuildOperationMsg.OpType.BuyBoostFactory, 0U, CS$<>8__locals1.<>4__this.{18374}.uIDDynamicServerBuild, default(Vector2)));
			}, Local.acceptGuildFortAction)));
			form.AddChild(stackForm);
			CS$<>8__locals1.stack2 = new StackForm(new Vector2(410f, 200f) + base.Pos.XY, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if (!this.{18375}.CapturerFraction.IsNation())
			{
				CS$<>8__locals1.<CreateStatusPage>g__Block|7(Local.mooringCharge_neutral_fraction, this.{18375}.MooringChargeNeutral, delegate(MooringChargePolicy {18396})
				{
					CS$<>8__locals1.<>4__this.{18375}.MooringChargeNeutral = {18396};
				}, false);
				CS$<>8__locals1.<CreateStatusPage>g__Block|7(Local.mooringCharge_enemyFr, this.{18375}.MooringChargeEnemy, delegate(MooringChargePolicy {18397})
				{
					CS$<>8__locals1.<>4__this.{18375}.MooringChargeEnemy = {18397};
				}, true);
			}
			else
			{
				CS$<>8__locals1.<CreateStatusPage>g__Block|7(Local.mooringCharge_neutral_pirates, this.{18375}.MooringChargeNeutral, delegate(MooringChargePolicy {18398})
				{
					CS$<>8__locals1.<>4__this.{18375}.MooringChargeNeutral = {18398};
				}, false);
				CS$<>8__locals1.<CreateStatusPage>g__Block|7(Local.mooringCharge_enemyFr, this.{18375}.MooringChargeEnemy, delegate(MooringChargePolicy {18399})
				{
					CS$<>8__locals1.<>4__this.{18375}.MooringChargeEnemy = {18399};
				}, true);
			}
			form.AddChild(CS$<>8__locals1.stack2);
			return form;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00037F54 File Offset: 0x00036154
		private void ProtectionFormHelper(StackForm {18343}, string {18344}, string {18345}, string {18346}, Action {18347}, float {18348}, ResourceInfo {18349} = null, [TupleElementNames(new string[]
		{
			"ingots",
			"cc",
			"click",
			"acceptQuestionText"
		})] ValueTuple<int, int, Action, string>? {18350} = null)
		{
			Form form = new Form(Vector2.Zero, {18328}.c_protectionForm, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChild(new UiControl[]
			{
				new Label(new Vector2(10f, 29f), Fonts.Philosopher_16, Color.Wheat * 0.8f, {18344}, PositionAlignment.LeftUp, PositionAlignment.LeftUp),
				new Label(new Vector2(11f, 53f), Fonts.Arial_10, Color.Wheat * 0.6f, {18345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			if (this.{18375}.IsBlockedByWindowOrBattle && string.IsNullOrEmpty({18346}))
			{
				{18346} = Local.GuidFortManagingUi_1b;
			}
			if (!string.IsNullOrEmpty({18346}))
			{
				form.AddChild(new Label(new Vector2(11f, 69f), Fonts.Arial_10, Color.Yellow * 0.6f, {18346}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			else
			{
				form.EvClick += delegate(ClickUiEventArgs {18401})
				{
					{18347}();
				};
			}
			{18343}.AddItem(new UiControl[]
			{
				form
			});
			if ({18348} > 0f)
			{
				form.Opacity = 0.5f;
				form.AllowMouseInput = false;
				form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Lime * 0.7f, ({18348} < 86400f) ? StringHelper.TimeDHM((double){18348}, false) : StringHelper.TimeD((double){18348}), PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.RightDown, PositionAlignment.LeftUp, 29f);
			}
			if ({18349} != null)
			{
				form.AddChildPos(new Image(new Marker(0f, 0f, 32f, 32f), {18349}.IconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExToolTip(new ToolTip({18349}.ToolTip())), PositionAlignment.RightDown, PositionAlignment.Center, 55f);
			}
			if ({18350} != null)
			{
				form.ToolTipState = new ToolTipState({18344}, "", this.TTPriceHelper(new GSI(), {18350}.Value.Item2, {18350}.Value.Item1).ToArray<ToolTipCharacteristics>());
				if (Session.Guild.Treasury >= {18350}.Value.Item1 && Session.Guild.ConquerBadges >= {18350}.Value.Item2 && string.IsNullOrEmpty({18346}))
				{
					form.EvClick += delegate(ClickUiEventArgs {18402})
					{
						if (!string.IsNullOrEmpty({18350}.Value.Item4))
						{
							new {17312}({18350}.Value.Item4, {18350}.Value.Item3, delegate()
							{
							});
							return;
						}
						{18350}.Value.Item3();
					};
				}
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000381C0 File Offset: 0x000363C0
		private IEnumerable<ToolTipCharacteristics> TTPriceHelper(GSI {18351}, int {18352} = 0, int {18353} = 0)
		{
			{18328}.<TTPriceHelper>d__22 <TTPriceHelper>d__ = new {18328}.<TTPriceHelper>d__22(-2);
			<TTPriceHelper>d__.<>4__this = this;
			<TTPriceHelper>d__.<>3__cost = {18351};
			<TTPriceHelper>d__.<>3__conquerBadgesCount = {18352};
			<TTPriceHelper>d__.<>3__countIngots = {18353};
			return <TTPriceHelper>d__;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000381E8 File Offset: 0x000363E8
		private static string towerPropHelper(InitialDynamicBuildingId {18354})
		{
			if ({18354} == InitialDynamicBuildingId.FortTowerCannons)
			{
				return Local.GuidFortManagingUi_34;
			}
			PbsTowerDescription pbsTowerDescription = WosbPbs.TowerDesription[{18354}];
			return Local.GuidFortManagingUi_33(pbsTowerDescription.DeadZone, pbsTowerDescription.MaxDistance, pbsTowerDescription.SingleWeaponDamage, pbsTowerDescription.SingleWeaponCooldown);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0003823C File Offset: 0x0003643C
		private void {18355}(IPacketBase {18356})
		{
			base.AllowMouseInput = false;
			Global.Network.Send({18356});
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00038250 File Offset: 0x00036450
		protected override void UserUpdate(ref FrameTime {18357})
		{
			if (this.{18377} == {18328}.ChangeRistrictions.None && Global.Game.InteractiveWorldSystem.ContainsGuildFort == null)
			{
				this.BlockAndClose();
			}
			base.UserUpdate(ref {18357});
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00038278 File Offset: 0x00036478
		protected override void UserBackRender()
		{
			base.UserBackRender();
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00038280 File Offset: 0x00036480
		protected override void UserFrontRender()
		{
			base.UserFrontRender();
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00038344 File Offset: 0x00036544
		[CompilerGenerated]
		private void {18359}(ClickUiEventArgs {18360})
		{
			if (Session.Guild.ConquerBadges >= 10)
			{
				this.{18355}(new OnBuildingGuildOperationMsg(OnBuildingGuildOperationMsg.OpType.QuickMoneyTransfer, 0U, this.{18374}.uIDDynamicServerBuild, default(Vector2)));
				return;
			}
			new {17312}(Local.GuildWindow_50);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00038394 File Offset: 0x00036594
		[CompilerGenerated]
		private void {18361}(ClickUiEventArgs {18362})
		{
			if (this.{18375}.FirstFort().HoldResources[40] > 0)
			{
				this.{18355}(new OnBuildingGuildOperationMsg(OnBuildingGuildOperationMsg.OpType.QuickMending, 0U, this.{18374}.uIDDynamicServerBuild, default(Vector2)));
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000383E4 File Offset: 0x000365E4
		[CompilerGenerated]
		private string {18363}()
		{
			return "     " + (this.{18374}.HoldCannonBalls.IsEmpty ? Local.GuidFortManagingUi_13 : (Local.GuidFortManagingUi_14 + Gameplay.BallsInfo.FromID((int)this.{18374}.CurrentSelectedCannonsBalls).Name));
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00038438 File Offset: 0x00036638
		[CompilerGenerated]
		private void {18364}(ClickUiEventArgs {18365})
		{
			if (!this.{18374}.HoldCannonBalls.CannonBallInfo.Any((GSILocalEnumerablePair<CannonBallInfo> {18381}) => {18381}.Info.AmmoType == CannonAmmoType.CannonBall))
			{
				return;
			}
			new {17473}(new Action<object>(this.{18366}), (from {18382} in this.{18374}.HoldCannonBalls.CannonBallInfo
			where {18382}.Info.AmmoType == CannonAmmoType.CannonBall
			select {18382} into {18383}
			select new {17473}.Item((uint){18383}.Info.ID, {18383}.Info.Name, true, new ImageDecription({18383}.Info.IconTexture), null, null)).ToArray<{17473}.Item>());
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000384F8 File Offset: 0x000366F8
		[CompilerGenerated]
		private void {18366}(object {18367})
		{
			this.{18355}(new OnBuildingGuildOperationMsg(OnBuildingGuildOperationMsg.OpType.SetCannonBalls, (uint){18367}, this.{18374}.uIDDynamicServerBuild, default(Vector2)));
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00038530 File Offset: 0x00036730
		[CompilerGenerated]
		private string {18368}()
		{
			return "     " + Local.GuidFortManagingUi_19 + Gameplay.CannonsGameInfo.FromID((int)this.{18374}.CurrentSelectedWeapons).Name;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0003855C File Offset: 0x0003675C
		[CompilerGenerated]
		private void {18369}(ClickUiEventArgs {18370})
		{
			new {17473}(new Action<object>(this.{18371}), (from {18384} in this.{18375}.FortLevelInfo.AvailableCannonsToSet
			select new {17473}.Item((uint){18384}, Gameplay.CannonsGameInfo.FromID({18384}).Name, true, default(ImageDecription), null, null)).ToArray<{17473}.Item>());
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x000385B4 File Offset: 0x000367B4
		[CompilerGenerated]
		private void {18371}(object {18372})
		{
			this.{18355}(new OnBuildingGuildOperationMsg(OnBuildingGuildOperationMsg.OpType.SetCannonsID, (uint){18372}, this.{18374}.uIDDynamicServerBuild, default(Vector2)));
		}

		// Token: 0x04000630 RID: 1584
		public static {18328} CurrentInstance;

		// Token: 0x04000631 RID: 1585
		public static readonly Rectangle c_protectionForm = new Rectangle(1720, 2366, 397, 99);

		// Token: 0x04000632 RID: 1586
		public static readonly Rectangle c_itemButtonToolList = new Rectangle(994, 620, 385, 39);

		// Token: 0x04000633 RID: 1587
		public static readonly Rectangle c_bannerDownRed = new Rectangle(1296, 549, 395, 29);

		// Token: 0x04000634 RID: 1588
		public static readonly Rectangle c_bannerDownGray = new Rectangle(1150, 736, 395, 29);

		// Token: 0x04000635 RID: 1589
		private static readonly Color lightTextCol = new Color(233, 216, 187);

		// Token: 0x04000636 RID: 1590
		private static readonly Vector2 c_descPos = new Vector2(12f, 72f);

		// Token: 0x04000637 RID: 1591
		private Tab {18373};

		// Token: 0x04000638 RID: 1592
		private PbsBuildingStatus {18374};

		// Token: 0x04000639 RID: 1593
		private PbsStatus {18375};

		// Token: 0x0400063A RID: 1594
		private bool {18376} = true;

		// Token: 0x0400063B RID: 1595
		private {18328}.ChangeRistrictions {18377};

		// Token: 0x0400063C RID: 1596
		private {18328}.ChangeRistrictions {18378};

		// Token: 0x02000131 RID: 305
		public enum ChangeRistrictions
		{
			// Token: 0x0400063E RID: 1598
			None,
			// Token: 0x0400063F RID: 1599
			WatchOnlyWithoutPeaceFlagOrFar,
			// Token: 0x04000640 RID: 1600
			WatchOnlyWarTime,
			// Token: 0x04000641 RID: 1601
			WatchOnlyNoRights,
			// Token: 0x04000642 RID: 1602
			AppendStorageOnly,
			// Token: 0x04000643 RID: 1603
			FullStorageOnly
		}
	}
}
