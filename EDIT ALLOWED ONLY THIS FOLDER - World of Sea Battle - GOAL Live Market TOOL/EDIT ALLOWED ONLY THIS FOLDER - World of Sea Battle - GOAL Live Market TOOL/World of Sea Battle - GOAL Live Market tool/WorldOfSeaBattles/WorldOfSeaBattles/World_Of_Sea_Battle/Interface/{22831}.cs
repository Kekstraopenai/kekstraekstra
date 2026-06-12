using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020003F9 RID: 1017
	[NullableContext(1)]
	[Nullable(0)]
	internal class {22831}
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x000B9838 File Offset: 0x000B7A38
		private static float Width
		{
			get
			{
				return 370f;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x000B983F File Offset: 0x000B7A3F
		private static float SmallIconSize
		{
			get
			{
				return 24f;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x000B9846 File Offset: 0x000B7A46
		private static float Spacing
		{
			get
			{
				return -2f;
			}
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x000B9850 File Offset: 0x000B7A50
		public {22831}(bool {22838}, GSI {22839}, IslePortInfo {22840}, [Nullable(new byte[]
		{
			2,
			1
		})] Tlist<PortLiveTrading> {22841}, [Nullable(2)] PortCaputreStatusList {22842}, [Nullable(2)] Dictionary<int, float> {22843})
		{
			this.{22865} = {22839};
			this.{22866} = {22840};
			this.{22867} = (({22842} != null) ? {22842}.GetByPortId<PortCaptureStatus>({22840}.PortID) : null);
			PortCaptureStatus portCaptureStatus = this.{22867};
			this.{22868} = ((portCaptureStatus != null) ? portCaptureStatus.Dev : null);
			this.{22870} = ({22843} ?? new Dictionary<int, float>());
			if ({22841} != null && {22840}.PortID < {22841}.Size)
			{
				this.{22869} = {22841}.Array[{22840}.PortID];
			}
			if ({22838})
			{
				this.{22844}();
				return;
			}
			this.{22845}();
			this.{22846}();
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000B98FC File Offset: 0x000B7AFC
		private void {22844}()
		{
			Composer composer = new Composer({22831}.Width, {22831}.Spacing).AddHeader(this.{22866}.PortName, null).AddText(Local.hidden_port_text, ComposerTextStyle.Wheat, true).AddComposer(this.{22849}(true));
			this.{22872} = composer.CurrentHeight;
			this.{22871}.Add(composer);
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x000B9968 File Offset: 0x000B7B68
		private void {22845}()
		{
			Composer composer = new Composer({22831}.Width, {22831}.Spacing).AddHeader(this.{22866}.PortName, null).OpenHorizontal().AddImage(CommonAtlas.Texture.Tex, {22831}.GetIcon(this.{22866}, this.{22868}), 100f).AddSpace(10f).AddComposer(this.{22849}(false)).CloseHorizontal().AddSpace(10f).AddComposer(this.{22851}(10f)).AddComposer(this.{22854}(10f)).AddComposer(this.{22853}()).AddSpace(10f).AddComposer(this.{22856}());
			this.{22872} = composer.CurrentHeight;
			this.{22871}.Add(composer);
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x000B9A48 File Offset: 0x000B7C48
		private void {22846}()
		{
			ComposerTextStyle wheat = ComposerTextStyle.Wheat;
			Composer composer = this.{22857}();
			if (this.{22865}.IsEmpty)
			{
				composer.AddText({22831}.SmallIconSize, Local.empty, wheat, true);
				return;
			}
			float num = this.{22872} / 25f;
			int num2 = 0;
			foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in from {22873} in this.{22865}.ResourceInfo
			orderby {22873}.Count descending
			select {22873})
			{
				if ((float)num2 > num)
				{
					composer = this.{22857}();
					num2 = 0;
				}
				Texture2D iconTexture = gsilocalEnumerablePair.Info.IconTexture;
				composer.OpenHorizontal();
				composer.AddAlign({22831}.SmallIconSize);
				composer.AddImage(iconTexture, iconTexture.Bounds, 20f);
				composer.AddAlign(10f);
				composer.AddText(gsilocalEnumerablePair.Info.Name, wheat, true);
				composer.AddAlign(200f);
				Composer composer2 = composer;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
				defaultInterpolatedStringHandler.AppendLiteral(" x");
				defaultInterpolatedStringHandler.AppendFormatted<int>(gsilocalEnumerablePair.Count);
				composer2.AddText(defaultInterpolatedStringHandler.ToStringAndClear(), wheat, true);
				composer.CloseHorizontal();
				num2++;
			}
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x000B9BB4 File Offset: 0x000B7DB4
		public static Rectangle GetIcon(IslePortInfo {22847}, [Nullable(2)] PortDevelopment {22848})
		{
			if ({22847}.Type == PortType.PirateBay)
			{
				return {22831}.iconCityPirate;
			}
			if ({22848} == null || {22847}.FixedPortLevel > 0)
			{
				return {22831}.iconCityNeutral;
			}
			int? num = ({22848} != null) ? new int?({22848}.PortLevel) : null;
			if (num != null)
			{
				switch (num.GetValueOrDefault())
				{
				case 1:
				case 2:
				case 3:
					return {22831}.iconCityLvl1;
				case 4:
				case 5:
					return {22831}.iconCityLvl2;
				case 6:
				case 7:
					return {22831}.iconCityLvl3;
				}
			}
			return Rectangle.Empty;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000B9C54 File Offset: 0x000B7E54
		public string GetPortLevelName()
		{
			PortDevelopment portDevelopment = this.{22868};
			int? num = (portDevelopment != null) ? new int?(portDevelopment.PortLevel) : null;
			if (num != null)
			{
				switch (num.GetValueOrDefault())
				{
				case 1:
				case 2:
				case 3:
					return Local.city_small;
				case 4:
				case 5:
					return Local.city_middle;
				case 6:
				case 7:
					return Local.city_large;
				}
			}
			return "Invalid";
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000B9CD8 File Offset: 0x000B7ED8
		private Composer {22849}(bool {22850} = false)
		{
			{22831}.<>c__DisplayClass30_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			ComposerTextStyle wheat = ComposerTextStyle.Wheat;
			CS$<>8__locals1.warningStyle = ComposerTextStyle.Warning;
			CS$<>8__locals1.composer = new Composer(280f, {22831}.Spacing);
			bool flag = LocaleInfo.Current.Id != Locale.Es;
			if (this.{22868} != null && !{22850})
			{
				CS$<>8__locals1.composer.AddText(Local.level_is(this.{22868}.PortLevel) + ": " + this.GetPortLevelName(), wheat, true);
			}
			string text = StringHelper.ToRoman(this.{22866}.BuildShipRangs);
			string {12528} = this.{22866}.IsArabPort ? Local.port_buildShipRanks_arab(text) : ((this.{22866}.BuildShipRangs >= 7) ? Local.port_buildShipRanks_b : Local.port_buildShipRanks_a);
			if (flag)
			{
				CS$<>8__locals1.composer.OpenHorizontal();
			}
			CS$<>8__locals1.composer.AddText({12528}, wheat, true);
			if (!this.{22866}.IsArabPort)
			{
				if (this.{22868} != null && this.{22867} != null)
				{
					PortDevelopment portDevelopment = this.{22868};
					PortLevelBonusType {7912} = PortLevelBonusType.ShipCraftingInLowerRankPort;
					Decorator game = Session.Game;
					if (portDevelopment.GetAvailableBonus({7912}, game, this.{22867}, null) > 0f)
					{
						CS$<>8__locals1.composer.AddText(Local.port_buildShipRanks(StringHelper.ToRoman(Math.Max(1, this.{22866}.BuildShipRangs - 1))), ComposerTextStyle.Lime, true);
						goto IL_166;
					}
				}
				CS$<>8__locals1.composer.AddText(Local.port_buildShipRanks(text), ComposerTextStyle.BareWhite, true);
			}
			IL_166:
			if (flag)
			{
				CS$<>8__locals1.composer.CloseHorizontal();
			}
			if (this.{22866}.ShallowUpperRang != -1)
			{
				bool flag2 = Global.Player.UsedShipPlayer.CraftFrom.Rank < this.{22866}.ShallowUpperRang;
				CS$<>8__locals1.composer.AddText(Local.shallow + " " + Local.shallow_ranks(StringHelper.ToRoman(this.{22866}.ShallowUpperRang)), flag2 ? CS$<>8__locals1.warningStyle : wheat, true);
			}
			this.{22858}(OpenWorldFlag.Peaceful, ref CS$<>8__locals1);
			this.{22858}(OpenWorldFlag.Trader, ref CS$<>8__locals1);
			if (!{22850})
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted(Local.tradeCharge);
				defaultInterpolatedStringHandler.AppendLiteral(": ");
				defaultInterpolatedStringHandler.AppendFormatted<int>((int)Math.Round((double)(this.{22866}.InternalTradingTax * 100f)));
				defaultInterpolatedStringHandler.AppendLiteral("%");
				string {12528}2 = defaultInterpolatedStringHandler.ToStringAndClear();
				CS$<>8__locals1.composer.AddText({12528}2, wheat, true);
			}
			return CS$<>8__locals1.composer;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x000B9F48 File Offset: 0x000B8148
		private Composer {22851}(float {22852})
		{
			ComposerTextStyle warning = ComposerTextStyle.Warning;
			Composer composer = new Composer({22831}.Width, {22831}.Spacing);
			PbsStatus pbsStatus = this.{22867} as PbsStatus;
			int num;
			if (pbsStatus != null && this.{22866}.AllowCapture)
			{
				PbsTensityEffects effects = pbsStatus.Tensity.Effects;
				if (!effects.DisableMooringCharge)
				{
					PbsStatus pbsStatus2 = pbsStatus;
					PlayerAccount account = Session.Account;
					GuildCommon guild = Session.Guild;
					effects = pbsStatus.Tensity.Effects;
					num = pbsStatus2.GetMooringCharge(account, guild, effects);
					goto IL_6A;
				}
			}
			num = 0;
			IL_6A:
			int num2 = num;
			Player player = Global.Player;
			FractionID? mapMyFraction = Session.Game.MapMyFraction;
			PortCaptureStatus portCaptureStatus = this.{22867};
			int num3 = PbsStatus.MooringChangeForHold(player, mapMyFraction, (portCaptureStatus != null) ? portCaptureStatus.CapturerFraction : FractionID.None, this.{22866});
			if (num2 + num3 > 0)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
				defaultInterpolatedStringHandler.AppendFormatted(Local.payCharge);
				defaultInterpolatedStringHandler.AppendLiteral(": ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(num2 + num3);
				string {12548} = defaultInterpolatedStringHandler.ToStringAndClear() + ((num3 > 0) ? (" " + Local.payCharge_hold) : "");
				composer.AddImageAndText({12548}, CommonAtlas.Texture.Tex, CommonAtlas.goldIconSingle32, {22831}.SmallIconSize, warning, true);
			}
			if (composer.CurrentHeight > 0f)
			{
				composer.AddSpace({22852});
			}
			return composer;
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000BA084 File Offset: 0x000B8284
		private Composer {22853}()
		{
			ComposerTextStyle gray = ComposerTextStyle.Gray;
			{22831}.<>c__DisplayClass32_0 CS$<>8__locals1;
			CS$<>8__locals1.headerStyle = ComposerTextStyle.Wheat;
			CS$<>8__locals1.composer = new Composer({22831}.Width, {22831}.Spacing);
			CS$<>8__locals1.hasIcon = false;
			if (!this.{22866}.AllowCapture && !this.{22866}.AllowCapturePiratePort)
			{
				{22831}.<CreateCaptureInfo>g__AddHeader|32_0(this.{22866}.IsArabPort ? Local.pb_neutral_arab : Local.pb_neutral, ref CS$<>8__locals1);
			}
			if (this.{22867} == null)
			{
				return CS$<>8__locals1.composer;
			}
			Relation relation = (!this.{22867}.IsCapturedByGuild) ? Relation.Ally : ((Session.Guild != null) ? this.{22867}.StatusWith(Session.Guild) : Session.Account.Reputations.NeutralStatusWith(this.{22867}.CapturerFraction));
			CS$<>8__locals1.headerStyle = ((relation == Relation.Ally) ? new ComposerTextStyle(new Color(233, 255, 193), false, null, null) : ((relation == Relation.Enemy) ? ComposerTextStyle.Red : CS$<>8__locals1.headerStyle));
			PbsStatus pbsStatus = this.{22867} as PbsStatus;
			if (pbsStatus != null)
			{
				if (pbsStatus.IsCapturedByGuild)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
					defaultInterpolatedStringHandler.AppendFormatted(Local.pb_have);
					defaultInterpolatedStringHandler.AppendLiteral(": [");
					defaultInterpolatedStringHandler.AppendFormatted(pbsStatus.CapturedBy);
					defaultInterpolatedStringHandler.AppendLiteral("] ");
					defaultInterpolatedStringHandler.AppendFormatted(pbsStatus.CapturerFraction.GetName());
					{22831}.<CreateCaptureInfo>g__AddHeader|32_0(defaultInterpolatedStringHandler.ToStringAndClear(), ref CS$<>8__locals1);
				}
				else
				{
					{22831}.<CreateCaptureInfo>g__AddHeader|32_0(Local.port_not_captured, ref CS$<>8__locals1);
				}
				if (!string.IsNullOrEmpty(pbsStatus.VassalTag))
				{
					Composer composer = CS$<>8__locals1.composer;
					float smallIconSize = {22831}.SmallIconSize;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(7, 4);
					defaultInterpolatedStringHandler2.AppendFormatted(Local.vassal);
					defaultInterpolatedStringHandler2.AppendLiteral(": [");
					defaultInterpolatedStringHandler2.AppendFormatted(pbsStatus.VassalTag);
					defaultInterpolatedStringHandler2.AppendLiteral("] (");
					defaultInterpolatedStringHandler2.AppendFormatted(Local.PortСraftShipWindow_25);
					defaultInterpolatedStringHandler2.AppendFormatted(StringHelper.TimeD((double)pbsStatus.VassalTimeoutSec));
					defaultInterpolatedStringHandler2.AppendLiteral(")");
					composer.AddText(smallIconSize, defaultInterpolatedStringHandler2.ToStringAndClear(), gray, true);
				}
				if (pbsStatus.TimeToBeginBattle > 0.0)
				{
					CS$<>8__locals1.composer.AddText({22831}.SmallIconSize, Local.pbsstatus_waitBattle(pbsStatus.NextAttackBy) + " " + StringHelper.TimeDHM(pbsStatus.TimeToBeginBattle, false), ComposerTextStyle.Wheat, true);
				}
				else if (pbsStatus.TimeToBattleCompletion > 0f)
				{
					CS$<>8__locals1.composer.AddText({22831}.SmallIconSize, Local.pbsstatus_inbattle + " " + StringHelper.TimeMMMSS((double)pbsStatus.TimeToBattleCompletion), ComposerTextStyle.Wheat, true);
					CS$<>8__locals1.composer.AddText({22831}.SmallIconSize, Local.pbsstatus_attackby + " [" + pbsStatus.NextAttackBy + "]", ComposerTextStyle.Wheat, true);
				}
				else
				{
					if (pbsStatus.TimeToProtectionTensity > 0f)
					{
						CS$<>8__locals1.composer.AddText({22831}.SmallIconSize, Local.pbsstatus_protection + " " + StringHelper.TimeDHM((double)pbsStatus.TimeToProtectionTensity, false), gray, true);
						if (pbsStatus.Display_LastEvent != PortBattleEvent.NoEvent)
						{
							CS$<>8__locals1.composer.AddText({22831}.SmallIconSize, pbsStatus.Display_LastEvent.ToStrStatus(pbsStatus.Display_LastEventArg), gray, true);
						}
					}
					else
					{
						{22831}.<>c__DisplayClass32_1 CS$<>8__locals2 = new {22831}.<>c__DisplayClass32_1();
						float num = Math.Max(pbsStatus.EmpireCaptureLevel, pbsStatus.Tensity.Amount);
						Composer composer2 = CS$<>8__locals1.composer;
						float smallIconSize2 = {22831}.SmallIconSize;
						string tensity = Local.tensity;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(3, 1);
						defaultInterpolatedStringHandler3.AppendLiteral(": ");
						defaultInterpolatedStringHandler3.AppendFormatted<double>(Math.Floor((double)(num * 100f)));
						defaultInterpolatedStringHandler3.AppendLiteral("%");
						composer2.AddText(smallIconSize2, tensity + defaultInterpolatedStringHandler3.ToStringAndClear(), (num > 0.1f) ? ComposerTextStyle.Wheat : gray, true);
						int num2 = 3;
						{22831}.<>c__DisplayClass32_1 CS$<>8__locals3 = CS$<>8__locals2;
						string limit;
						if (!pbsStatus.Display_IsWindowActive)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(1, 1);
							defaultInterpolatedStringHandler4.AppendLiteral("/");
							defaultInterpolatedStringHandler4.AppendFormatted<int>(50);
							limit = defaultInterpolatedStringHandler4.ToStringAndClear();
						}
						else
						{
							limit = "";
						}
						CS$<>8__locals3.limit = limit;
						string text = string.Join(", ", pbsStatus.Tensity.GuildsAmount.OrderByDescending(delegate(PbsTensity.Guild {22874})
						{
							if (Session.Guild == null || !({22874}.GuildTag == Session.Guild.Tag))
							{
								return {22874}.Amount;
							}
							return 2f;
						}).Take(num2).Select(delegate(PbsTensity.Guild {22877})
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler11 = new DefaultInterpolatedStringHandler(4, 3);
							defaultInterpolatedStringHandler11.AppendLiteral("[");
							defaultInterpolatedStringHandler11.AppendFormatted({22877}.GuildTag);
							defaultInterpolatedStringHandler11.AppendLiteral("] ");
							defaultInterpolatedStringHandler11.AppendFormatted<double>(Math.Floor((double)({22877}.Amount * 100f)));
							defaultInterpolatedStringHandler11.AppendFormatted(CS$<>8__locals2.limit);
							defaultInterpolatedStringHandler11.AppendLiteral("%");
							return defaultInterpolatedStringHandler11.ToStringAndClear();
						}));
						if (pbsStatus.Tensity.GuildsAmount.Size > num2)
						{
							text += " ...";
						}
						if (!string.IsNullOrEmpty(text))
						{
							CS$<>8__locals1.composer.AddText({22831}.SmallIconSize, text, gray, true);
						}
						if (this.{22869} != null)
						{
							ResourceInfo resourceInfo = WosbPbs.PortHasDeficit(this.{22866}, this.{22869}.CurrentCount);
							if (resourceInfo != null)
							{
								CS$<>8__locals1.composer.AddText({22831}.SmallIconSize, Local.deficit + ": \"" + resourceInfo.Name + "\"", ComposerTextStyle.Wheat, true);
							}
						}
						if (pbsStatus.TimeToResetPortByTensitySec > 0f)
						{
							CS$<>8__locals1.composer.AddText({22831}.SmallIconSize, Local.port_disorder + ": " + StringHelper.TimeDHM((double)pbsStatus.TimeToResetPortByTensitySec, false), ComposerTextStyle.Wheat, true);
						}
						if (pbsStatus.EmpireCaptureLevel > 0f && pbsStatus.IsCapturedByGuild)
						{
							Composer composer3 = CS$<>8__locals1.composer;
							float smallIconSize3 = {22831}.SmallIconSize;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler5 = new DefaultInterpolatedStringHandler(4, 2);
							defaultInterpolatedStringHandler5.AppendLiteral("[");
							defaultInterpolatedStringHandler5.AppendFormatted(Local.pbsstatus_empire);
							defaultInterpolatedStringHandler5.AppendLiteral("]  ");
							defaultInterpolatedStringHandler5.AppendFormatted<double>(Math.Floor((double)(pbsStatus.EmpireCaptureLevel * 100f)));
							composer3.AddText(smallIconSize3, defaultInterpolatedStringHandler5.ToStringAndClear(), gray, true);
						}
					}
					string text2;
					if (this.{22866}.PortBattleTeamLimitation == 2147483647)
					{
						text2 = string.Empty;
					}
					else
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler6 = new DefaultInterpolatedStringHandler(7, 2);
						defaultInterpolatedStringHandler6.AppendLiteral(" [");
						defaultInterpolatedStringHandler6.AppendFormatted<int>(this.{22866}.PortBattleTeamLimitation);
						defaultInterpolatedStringHandler6.AppendLiteral(" vs ");
						defaultInterpolatedStringHandler6.AppendFormatted<int>(this.{22866}.PortBattleTeamLimitation);
						defaultInterpolatedStringHandler6.AppendLiteral("]");
						text2 = defaultInterpolatedStringHandler6.ToStringAndClear();
					}
					string str = text2;
					CS$<>8__locals1.composer.AddText({22831}.SmallIconSize, (this.{22866}.CaptureWithoutWindow ? Local.pbsstatus_windowInfo_always : Local.pbsstatus_windowInfo(pbsStatus.AttackWindow.ToStringFull(this.{22866}, LocalizedDateTime.DefaultTimeFormat == TimeFormat.PreferLocalTime))) + str, gray, true);
				}
			}
			PortCaptureStatus portCaptureStatus = this.{22867};
			bool flag = portCaptureStatus is ArabPortCaptureStatus || portCaptureStatus is PiratePortCaptureStatus;
			if (flag)
			{
				if (this.{22867}.IsCapturedByGuild)
				{
					string value = this.{22866}.AllowCapturePiratePort ? Local.pb_have_pirate : Local.pb_have_arab;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler7 = new DefaultInterpolatedStringHandler(4, 3);
					defaultInterpolatedStringHandler7.AppendFormatted(value);
					defaultInterpolatedStringHandler7.AppendLiteral(" [");
					defaultInterpolatedStringHandler7.AppendFormatted(this.{22867}.CapturedBy);
					defaultInterpolatedStringHandler7.AppendLiteral("] ");
					defaultInterpolatedStringHandler7.AppendFormatted(this.{22867}.CapturerFraction.GetName());
					{22831}.<CreateCaptureInfo>g__AddHeader|32_0(defaultInterpolatedStringHandler7.ToStringAndClear(), ref CS$<>8__locals1);
				}
				else if (!this.{22866}.IsArabPort)
				{
					{22831}.<CreateCaptureInfo>g__AddHeader|32_0(Local.port_not_captured, ref CS$<>8__locals1);
				}
				ArabPortCaptureStatus arabPortCaptureStatus = this.{22867} as ArabPortCaptureStatus;
				if (arabPortCaptureStatus != null && Session.EventActionsPipeline.EventCategoryAmount(EActionCaterory.BTender) > 0f && arabPortCaptureStatus.Points.Count > 0)
				{
					GuildCommon guild = Session.Guild;
					string text3 = (guild != null) ? guild.Tag : null;
					CS$<>8__locals1.composer.AddText({22831}.SmallIconSize, Local.port_arabEventTop, gray, true);
					bool flag2 = false;
					KeyValuePair<string, float>[] array = (from {22875} in arabPortCaptureStatus.Points.BaseDictionary
					orderby {22875}.Value descending
					select {22875}).ToArray<KeyValuePair<string, float>>();
					for (int i = 0; i < Math.Min(array.Length, 5); i++)
					{
						if (text3 != null && array[i].Key == text3)
						{
							Composer composer4 = CS$<>8__locals1.composer;
							float smallIconSize4 = {22831}.SmallIconSize;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler8 = new DefaultInterpolatedStringHandler(4, 2);
							defaultInterpolatedStringHandler8.AppendLiteral("[");
							defaultInterpolatedStringHandler8.AppendFormatted(array[i].Key);
							defaultInterpolatedStringHandler8.AppendLiteral("]: ");
							defaultInterpolatedStringHandler8.AppendFormatted<int>((int)array[i].Value);
							composer4.AddText(smallIconSize4, defaultInterpolatedStringHandler8.ToStringAndClear(), gray, true);
							flag2 = true;
						}
						else
						{
							Composer composer5 = CS$<>8__locals1.composer;
							float smallIconSize5 = {22831}.SmallIconSize;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler9 = new DefaultInterpolatedStringHandler(4, 2);
							defaultInterpolatedStringHandler9.AppendLiteral("[");
							defaultInterpolatedStringHandler9.AppendFormatted(array[i].Key);
							defaultInterpolatedStringHandler9.AppendLiteral("]: ");
							defaultInterpolatedStringHandler9.AppendFormatted<int>((int)array[i].Value);
							composer5.AddText(smallIconSize5, defaultInterpolatedStringHandler9.ToStringAndClear(), gray, true);
						}
					}
					if (text3 != null && !flag2)
					{
						float num3 = arabPortCaptureStatus.Points[text3];
						if (num3 > 0f)
						{
							Composer composer6 = CS$<>8__locals1.composer;
							float smallIconSize6 = {22831}.SmallIconSize;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler10 = new DefaultInterpolatedStringHandler(4, 2);
							defaultInterpolatedStringHandler10.AppendLiteral("[");
							defaultInterpolatedStringHandler10.AppendFormatted(text3);
							defaultInterpolatedStringHandler10.AppendLiteral("]: ");
							defaultInterpolatedStringHandler10.AppendFormatted<int>((int)num3);
							composer6.AddText(smallIconSize6, defaultInterpolatedStringHandler10.ToStringAndClear(), gray, true);
						}
					}
				}
			}
			string {12532} = Local.pbs_welfareformat(StringHelper.BigValueHelper((int)this.{22867}.DisplayWelfare));
			CS$<>8__locals1.composer.AddText({22831}.SmallIconSize, {12532}, gray, true);
			return CS$<>8__locals1.composer;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x000BAA14 File Offset: 0x000B8C14
		private Composer {22854}(float {22855})
		{
			{22831}.<>c__DisplayClass33_0 CS$<>8__locals1;
			CS$<>8__locals1.style = new ComposerTextStyle(Color.Lerp(Color.Gold, Color.Wheat, 0.3f), false, null, null);
			CS$<>8__locals1.composer = new Composer({22831}.Width, {22831}.Spacing);
			CS$<>8__locals1.hasIcon = false;
			int storageLevel = Session.Account.ResourcesInPorts.GetStorageLevel(this.{22866}.PortID);
			if (storageLevel > 0 || !this.{22865}.IsEmpty)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 4);
				defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Ceiling((double)(this.{22865}.ComputeMass<ResourceInfo>() / 1000f)));
				defaultInterpolatedStringHandler.AppendFormatted(Local.tonn);
				defaultInterpolatedStringHandler.AppendLiteral(" / ");
				defaultInterpolatedStringHandler.AppendFormatted<float>(Session.Account.WarehouseLimit(this.{22866}) / 1000f);
				defaultInterpolatedStringHandler.AppendFormatted(Local.tonn);
				string text = defaultInterpolatedStringHandler.ToStringAndClear();
				{22831}.<CreatePlayerBuildingsInfo>g__AddText|33_0((storageLevel > 0) ? Local.storage_port(storageLevel, text) : Local.storage_port_only_res(text), ref CS$<>8__locals1);
			}
			using (IEnumerator<FactoryType> enumerator = Session.Account.ResourcesInPorts.GetOpenedFactories(this.{22866}.PortID).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FactoryType item = enumerator.Current;
					WosbCrafting.Recepie recepie = WosbCrafting.Workshop.FirstOrDefault(delegate(WosbCrafting.Recepie {22878})
					{
						FactoryType? needsFactoryInPort = {22878}.NeedsFactoryInPort;
						FactoryType item = item;
						return needsFactoryInPort.GetValueOrDefault() == item & needsFactoryInPort != null;
					});
					if (recepie != null)
					{
						Texture2D getIconTexture = recepie.Output.getIconTexture;
						CS$<>8__locals1.composer.AddImageAndText(Local.has(item.ToStringLocal()), getIconTexture, getIconTexture.Bounds, {22831}.SmallIconSize, CS$<>8__locals1.style, true);
					}
					else
					{
						{22831}.<CreatePlayerBuildingsInfo>g__AddText|33_0(Local.has(item.ToStringLocal()), ref CS$<>8__locals1);
					}
				}
			}
			foreach (Shipway shipway in ((IEnumerable<Shipway>)Session.Account.Shipyard.Shipways))
			{
				if ((int)shipway.PortID == this.{22866}.PortID)
				{
					string value = (shipway.TimeToFinishSec == 0f) ? Local.built_ready : Local.building;
					string value2 = (shipway.TimeToFinishSec == 0f) ? "" : (Local.PortСraftShipWindow_25 + StringHelper.TimeMMMSS((double)shipway.TimeToFinishSec));
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(2, 3);
					defaultInterpolatedStringHandler2.AppendFormatted(value);
					defaultInterpolatedStringHandler2.AppendLiteral(" ");
					defaultInterpolatedStringHandler2.AppendFormatted(shipway.ShipInfo.ShipName);
					defaultInterpolatedStringHandler2.AppendLiteral(" ");
					defaultInterpolatedStringHandler2.AppendFormatted(value2);
					{22831}.<CreatePlayerBuildingsInfo>g__AddText|33_0(defaultInterpolatedStringHandler2.ToStringAndClear(), ref CS$<>8__locals1);
				}
			}
			if (CS$<>8__locals1.composer.CurrentHeight > 0f)
			{
				CS$<>8__locals1.composer.AddSpace({22855});
			}
			return CS$<>8__locals1.composer;
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x000BAD28 File Offset: 0x000B8F28
		private Composer {22856}()
		{
			ComposerTextStyle wheat = ComposerTextStyle.Wheat;
			Composer composer = new Composer({22831}.Width, {22831}.Spacing);
			if (this.{22869} == null)
			{
				return composer;
			}
			composer.OpenHorizontal().AddImageAndText(Local.PortVisualScene_1, CommonAtlas.Texture.Tex, {22831}.iconMarket, wheat.Color, {22831}.SmallIconSize, wheat, true).AddAlign(185f).AddText(Local.shop, wheat, true).AddAlign(80f).AddText(Local.sell, wheat, true).CloseHorizontal().AddSpace(5f);
			foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in this.{22866}.LiveTrading.ResourceInfo.OrderBy(delegate(GSILocalEnumerablePair<ResourceInfo> {22876})
			{
				if ({22876}.Info.ID != 5)
				{
					return (int){22876}.Info.ID;
				}
				return 1000;
			}))
			{
				if (gsilocalEnumerablePair.Info.ID != 8)
				{
					ResourceInfo info = gsilocalEnumerablePair.Info;
					int count = this.{22869}.CurrentCount.GetCount((int)info.ID);
					bool flag = this.{22866}.ShopResources[(int)info.ID] != 0;
					ResourceInfo pbsManufacture = this.{22866}.PbsManufacture;
					short? num = (pbsManufacture != null) ? new short?(pbsManufacture.ID) : null;
					int? num2 = (num != null) ? new int?((int)num.GetValueOrDefault()) : null;
					int id = (int)gsilocalEnumerablePair.Info.ID;
					bool flag2 = num2.GetValueOrDefault() == id & num2 != null;
					float num3 = 0f;
					float num4 = 0f;
					if (flag)
					{
						num3 = (num4 = (float)this.{22866}.ShopResources[(int)info.ID]);
					}
					else
					{
						this.{22869}.NowPrice((int)info.ID, Session.EventActionsPipeline, out num3, out num4);
					}
					float amount = 0.5f + MathHelper.Clamp((num3 + num4) * 0.5f / ((float)info.MediumCost.Value * 1.33f) - 1f, -0.5f, 0.5f);
					Color {12504} = Color.Lerp(Color.Wheat, Color.Lerp(Color.Lime, Color.Red, amount), 0.5f);
					ComposerTextStyle {12529} = new ComposerTextStyle({12504}, false, null, null);
					ComposerTextStyle {12565} = new ComposerTextStyle({12504}, false, Fonts.Arial_10, null);
					string {12561} = info.Name + " (" + StringHelper.GetValueOfK(count) + ")";
					composer.OpenHorizontal().AddAlign(4f).AddImage(gsilocalEnumerablePair.Info.IconTexture, 20f).AddAlign(23f).AddTextAndImage({12561}, CommonAtlas.Texture.Tex, flag2 ? {22831}.iconPickaxe : CommonAtlas.transpPixel, 14f, {12565}, true).AddAlign(160f).AddText(num3.ToString(), {12529}, true).AddAlign(80f).AddText(num4.ToString(), {12529}, true).CloseHorizontal();
				}
			}
			float num5;
			composer.AddSpace(10f).OpenHorizontal().AddAlign(4f).AddText(Local.auction_lots_count(this.{22870}.TryGetValue(this.{22866}.PortID, out num5) ? num5 : 0f), wheat, true).CloseHorizontal();
			return composer;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000BB0D8 File Offset: 0x000B92D8
		public ToolTipState CreateToolTip()
		{
			if (this.{22871}.Count == 1)
			{
				return new ToolTipState(this.{22871}[0]);
			}
			int num = 1;
			foreach (Composer composer in this.{22871})
			{
				string portName = this.{22866}.PortName;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
				defaultInterpolatedStringHandler.AppendLiteral(" [");
				defaultInterpolatedStringHandler.AppendFormatted<int>(num++);
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.{22871}.Count);
				defaultInterpolatedStringHandler.AppendLiteral("]");
				composer.UpdateHeader(portName + defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return new ToolTipState({22831}.Width, this.{22872}, CommonAtlas.tt_arrowKeys_left, CommonAtlas.tt_arrowKeys_right, CommonAtlas.Texture.Tex, null, this.{22871}.ToArray());
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000BB2D4 File Offset: 0x000B94D4
		[CompilerGenerated]
		private Composer {22857}()
		{
			Composer composer = new Composer({22831}.Width, {22831}.Spacing);
			composer.AddHeader(this.{22866}.PortName, null);
			composer.AddSpace(20f);
			this.{22871}.Add(composer);
			return composer;
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x000BB328 File Offset: 0x000B9528
		[CompilerGenerated]
		private void {22858}(OpenWorldFlag {22859}, ref {22831}.<>c__DisplayClass30_0 {22860})
		{
			if (!this.{22866}.FlagsAllowedToEnter.Contains({22859}))
			{
				if ({22859} == Session.Account.WorldFlag)
				{
					{22860}.composer.AddText(Local.port_pfFlagsNotAvailable_pf6_active({22859}.ToStringLocalShort(), Session.Account.WorldFlag.ToStringLocalSingle()), {22860}.warningStyle, true);
					return;
				}
				{22860}.composer.AddText(Local.port_pfFlagsNotAvailable_pf6({22859}.ToStringLocalShort(), Session.Account.WorldFlag.ToStringLocalSingle()), {22860}.warningStyle, true);
			}
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x000BB3B0 File Offset: 0x000B95B0
		[CompilerGenerated]
		internal static void <CreateCaptureInfo>g__AddHeader|32_0(string {22861}, ref {22831}.<>c__DisplayClass32_0 {22862})
		{
			if ({22862}.hasIcon)
			{
				{22862}.composer.AddText({22831}.SmallIconSize, {22861}, {22862}.headerStyle, true);
				return;
			}
			{22862}.composer.AddImageAndText({22861}, CommonAtlas.Texture.Tex, {22831}.iconShield, {22862}.headerStyle.Color, {22831}.SmallIconSize, {22862}.headerStyle, true);
			{22862}.hasIcon = true;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000BB41C File Offset: 0x000B961C
		[CompilerGenerated]
		internal static void <CreatePlayerBuildingsInfo>g__AddText|33_0(string {22863}, ref {22831}.<>c__DisplayClass33_0 {22864})
		{
			if ({22864}.hasIcon)
			{
				{22864}.composer.AddText({22831}.SmallIconSize, {22863}, {22864}.style, true);
				return;
			}
			{22864}.composer.AddImageAndText({22863}, CommonAtlas.Texture.Tex, {22831}.iconStorage, {22864}.style.Color, {22831}.SmallIconSize, {22864}.style, true);
			{22864}.hasIcon = true;
		}

		// Token: 0x040013ED RID: 5101
		private static readonly Rectangle iconCityLvl1 = new Rectangle(3090, 839, 100, 100);

		// Token: 0x040013EE RID: 5102
		private static readonly Rectangle iconCityLvl2 = new Rectangle(3190, 839, 100, 100);

		// Token: 0x040013EF RID: 5103
		private static readonly Rectangle iconCityLvl3 = new Rectangle(3290, 839, 100, 100);

		// Token: 0x040013F0 RID: 5104
		private static readonly Rectangle iconCityPersonal = new Rectangle(3090, 939, 100, 100);

		// Token: 0x040013F1 RID: 5105
		private static readonly Rectangle iconCityNeutral = new Rectangle(3190, 939, 100, 100);

		// Token: 0x040013F2 RID: 5106
		private static readonly Rectangle iconCityPirate = new Rectangle(3290, 939, 100, 100);

		// Token: 0x040013F3 RID: 5107
		private static readonly Rectangle iconMarket = new Rectangle(3090, 1039, 27, 27);

		// Token: 0x040013F4 RID: 5108
		private static readonly Rectangle iconStorage = new Rectangle(3117, 1039, 27, 27);

		// Token: 0x040013F5 RID: 5109
		private static readonly Rectangle iconShield = new Rectangle(3144, 1039, 27, 27);

		// Token: 0x040013F6 RID: 5110
		private static readonly Rectangle iconPickaxe = new Rectangle(83, 0, 20, 20);

		// Token: 0x040013F7 RID: 5111
		private readonly GSI {22865};

		// Token: 0x040013F8 RID: 5112
		private readonly IslePortInfo {22866};

		// Token: 0x040013F9 RID: 5113
		[Nullable(2)]
		private readonly PortCaptureStatus {22867};

		// Token: 0x040013FA RID: 5114
		[Nullable(2)]
		private readonly PortDevelopment {22868};

		// Token: 0x040013FB RID: 5115
		[Nullable(2)]
		private readonly PortLiveTrading {22869};

		// Token: 0x040013FC RID: 5116
		private readonly Dictionary<int, float> {22870};

		// Token: 0x040013FD RID: 5117
		private readonly List<Composer> {22871} = new List<Composer>();

		// Token: 0x040013FE RID: 5118
		private float {22872};
	}
}
