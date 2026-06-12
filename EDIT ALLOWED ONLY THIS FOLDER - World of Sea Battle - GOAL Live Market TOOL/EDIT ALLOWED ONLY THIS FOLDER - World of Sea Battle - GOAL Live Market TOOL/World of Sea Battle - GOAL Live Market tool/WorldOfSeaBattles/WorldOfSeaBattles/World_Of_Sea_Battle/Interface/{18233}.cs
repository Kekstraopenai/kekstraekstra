using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
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
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200011A RID: 282
	internal class {18233} : {17068}
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x000331C1 File Offset: 0x000313C1
		public static {18233} CurrentInstance
		{
			get
			{
				return {18233}.currentInstance;
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000331C8 File Offset: 0x000313C8
		public {18233}(FactoryPlaceIsleInfo {18235}) : base({17625}.c_back1, 700f, {17068}.BlockingWay.BackgroundClosing, true)
		{
			Global.Camera.CameraEffects.RunFocusEffect(new CameraFocusEffect(delegate()
			{
				if ({18233}.CurrentInstance != null)
				{
					return new Vector3?({18235}.GlobalPosition.X0Y() + Vector3.Up * 4f);
				}
				return null;
			}, 0.3f, 0f));
			this.{18259} = {18235};
			{18233}.currentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{18233}.currentInstance = null;
			};
			if (Session.Account.Buildings.Get(this.{18259}.FcID) == null)
			{
				this.{18244}();
			}
			else
			{
				Global.Network.Send(new OnGetFactoryStatus((byte){18235}.FcID, null));
			}
			EducationHelper.MakeFlag(EducationOnboarding.OpenFactoryWindow, true);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000332A2 File Offset: 0x000314A2
		public void ResoponseHandler(OnGetFactoryStatus {18236})
		{
			this.{18260} = {18236}.totalStatus;
			this.{18244}();
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000332B8 File Offset: 0x000314B8
		public void ResoponseHandler(OnFactoryOpenOrUpgradeMsg {18237})
		{
			PlayerBuildingState playerBuildingState = Session.Account.Buildings.Get(this.{18259}.FcID);
			if (playerBuildingState != null)
			{
				FactoryMineLivelInfo level = playerBuildingState.Level;
				if ({18237}.operation == OnFactoryOpenOrUpgradeMsg.Operation.GetStorage)
				{
					this.{18260}.Exs(level.ProducedResId, -{18237}.parameterWithoutBonus);
				}
				if ({18237}.operation == OnFactoryOpenOrUpgradeMsg.Operation.AddConsumedRes)
				{
					this.{18260}.AddOrRemove(level.ConsumedResId, {18237}.parameterWithoutBonus);
				}
				if ({18237}.operation == OnFactoryOpenOrUpgradeMsg.Operation.AddBondman)
				{
					this.{18260}.AddOrRemove(8, {18237}.parameterWithoutBonus);
				}
				if ({18237}.operation == OnFactoryOpenOrUpgradeMsg.Operation.AddAnimals)
				{
					this.{18260}.AddOrRemove(22, {18237}.parameterWithoutBonus);
				}
			}
			if (this.{18260} == null)
			{
				this.{18260} = new GSI();
			}
			this.{18244}();
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0003337F File Offset: 0x0003157F
		protected override void UserUpdate(ref FrameTime {18238})
		{
			if (Global.Game.InteractiveWorldSystem.ContainsWorldFactories.IndexOf(this.{18259}) == 1)
			{
				base.RemoveFromContainer();
			}
			base.UserUpdate(ref {18238});
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000201BB File Offset: 0x0001E3BB
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(CommonAtlas.Texture);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000201CC File Offset: 0x0001E3CC
		protected override void UserFrontRender()
		{
			Engine.GS.ReturnBackTexture();
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000333AC File Offset: 0x000315AC
		private void {18239}(FactoryGameInfo {18240})
		{
			if (Global.Player.ResourcesOfHold[33] > 0)
			{
				new {17312}(Local.askInsuranePaper_b(Global.Player.ResourcesOfHold[33]), delegate()
				{
					this.{18241}({18240}, true);
				}, delegate()
				{
					this.{18241}({18240}, false);
				});
				return;
			}
			this.{18241}({18240}, false);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00033428 File Offset: 0x00031628
		private void {18241}(FactoryGameInfo {18242}, bool {18243})
		{
			if (!{18243} && {18242}.InitBuildCost.Item1 > Session.Account.Gold)
			{
				new {17312}(Local.gold_not_enough);
				return;
			}
			new {17312}(Local.MineUi_0, delegate()
			{
				this.AllowMouseInput = false;
				Global.Network.Send(new OnFactoryOpenOrUpgradeMsg((byte)this.{18259}.FcID, {18243} ? OnFactoryOpenOrUpgradeMsg.Operation.BuildOrUpgradeInsuranePaper : OnFactoryOpenOrUpgradeMsg.Operation.BuildOrUpgrade, (int){18242}.Type, 0));
				Global.Game.SoundSystem.PlaySound(GameStaticSoundName.BattleResults, 0.03f, 1f);
			}, delegate()
			{
			});
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000334B8 File Offset: 0x000316B8
		private void {18244}()
		{
			{18233}.<>c__DisplayClass17_0 CS$<>8__locals1 = new {18233}.<>c__DisplayClass17_0();
			CS$<>8__locals1.<>4__this = this;
			Form form = this.{18261};
			if (form != null)
			{
				form.RemoveFromContainer();
			}
			base.AllowMouseInput = true;
			CS$<>8__locals1.build = Session.Account.Buildings.Get(this.{18259}.FcID);
			this.{18261} = new Form(base.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{18261}.AnimatedFocus = false;
			string {13345} = (CS$<>8__locals1.build == null) ? Local.MineUi_20 : WosbCrafting.FactoriesInfo[CS$<>8__locals1.build.Type].Name;
			this.{18261}.AddChild(new Form(base.Pos.XY + new Vector2(11f, 59f), {19779}.c_paperHeader, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
			this.{18261}.AddChild(new Label(base.Pos.XY + new Vector2(101f, 62f), Fonts.Philosopher_18, {17177}.textColor, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			Form form2 = this.{18261};
			Marker marker = new Marker(13f, 59f, 78f, 78f);
			Marker marker2 = base.Pos;
			form2.AddChild(new Form(marker.Offset(marker2.XY), new Rectangle(1098, 316, 78, 78), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
			Form form3 = this.{18261};
			Rectangle {13205} = {18233}.c_page_background;
			marker2 = new Marker(76f, 55f, 724f, 498f);
			marker = base.Pos;
			form3.AddChild({13205}, marker2.Offset(marker.XY));
			CS$<>8__locals1.options = new BlocksStackFormControl(base.Pos.XY + new Vector2(4f, 194f), 4, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				MaxWidth = (float)({18233}.c_itemButton.Width + 10)
			};
			CS$<>8__locals1.lightTextCol = new Color(233, 216, 187);
			this.{18261}.AddChild(CS$<>8__locals1.options);
			if (CS$<>8__locals1.build != null)
			{
				CS$<>8__locals1.<UpdateUi>g__AddButton|0(Local.improve2, delegate
				{
					if (CS$<>8__locals1.build.NextUpCost.Item1 > Session.Account.Gold || !Global.Player.ResourcesOfHold.CanRemove(CS$<>8__locals1.build.NextUpCost.Item2))
					{
						new {17312}(Local.MineUi_2);
						return;
					}
					string {17371} = (CS$<>8__locals1.build.NextLevel.ConsumeProvision && !CS$<>8__locals1.build.Level.ConsumeProvision) ? Local.MineUi_3_WithTt : Local.MineUi_3;
					Action {17372};
					if (({17372} = CS$<>8__locals1.<>9__9) == null)
					{
						{17372} = (CS$<>8__locals1.<>9__9 = delegate()
						{
							CS$<>8__locals1.<>4__this.AllowMouseInput = false;
							Global.Network.Send(new OnFactoryOpenOrUpgradeMsg((byte)CS$<>8__locals1.<>4__this.{18259}.FcID, OnFactoryOpenOrUpgradeMsg.Operation.BuildOrUpgrade, 0, 0));
							Global.Game.SoundSystem.PlaySound(GameStaticSoundName.BattleResults, 0.03f, 1f);
						});
					}
					new {17312}({17371}, {17372}, delegate()
					{
					});
				}, CS$<>8__locals1.build.HaveNextLevel ? {18233}.ToolTip(this.{18259}, CS$<>8__locals1.build.Type, (int)(CS$<>8__locals1.build.LevelIndex + 1), null, true) : null, CS$<>8__locals1.build.HaveNextLevel, null, default(Rectangle));
				GSI gsi = new GSI();
				GSI gsi2 = new GSI();
				FactoryMineLivelInfo mine = CS$<>8__locals1.build.Level;
				bool flag = mine.ProducedResId != 37 && mine.ProducedResId != 22;
				gsi.Exs(mine.ProducedResId, mine.ProducedResCount);
				gsi2.Exs(mine.ConsumedResId, mine.ConsumedResCount);
				int num = (int)mine.HoldCapacity(Session.Account);
				if (mine.ConsumedResCount > 0)
				{
					{18233}.<>c__DisplayClass17_0 CS$<>8__locals3 = CS$<>8__locals1;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 4);
					defaultInterpolatedStringHandler.AppendFormatted(Local.to_refil);
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted(gsi2.ResourceInfo.First<GSILocalEnumerablePair<ResourceInfo>>().Info.Name);
					defaultInterpolatedStringHandler.AppendLiteral("  (");
					defaultInterpolatedStringHandler.AppendFormatted<int>(Global.Player.ResourcesOfHold[gsi2.First<GSILocalPair>().ID]);
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted(Local.on_ship.ToLower());
					defaultInterpolatedStringHandler.AppendLiteral(")");
					CS$<>8__locals3.<UpdateUi>g__AddButton|0(defaultInterpolatedStringHandler.ToStringAndClear(), delegate
					{
						int count = Global.Player.ResourcesOfHold.GetCount(mine.ConsumedResId);
						if (count == 0)
						{
							new {17312}(Local.MineUi_7);
							return;
						}
						string mineUi_ = Local.MineUi_8;
						string mineUi_2 = Local.MineUi_9;
						Action<int> {21859};
						if (({21859} = CS$<>8__locals1.<>9__11) == null)
						{
							{21859} = (CS$<>8__locals1.<>9__11 = delegate(int {18271})
							{
								CS$<>8__locals1.<>4__this.AllowMouseInput = false;
								Global.Network.Send(new OnFactoryOpenOrUpgradeMsg((byte)CS$<>8__locals1.<>4__this.{18259}.FcID, OnFactoryOpenOrUpgradeMsg.Operation.AddConsumedRes, {18271}, 0));
							});
						}
						new {21838}(mineUi_, mineUi_2, {21859}, count, null, null, null, null, null);
					}, null, true, null, default(Rectangle));
				}
				if (mine.ProducedResId == 22)
				{
					CS$<>8__locals1.<UpdateUi>g__AddButton|0(Local.HoldsUiCommon_5b, delegate
					{
						int num5 = Math.Min(Global.Player.ResourcesOfHold.GetCount(22), (int)(mine.HoldCapacity(Session.Account) - (float)CS$<>8__locals1.<>4__this.{18260}[22]));
						if (num5 == 0)
						{
							new {17312}(Local.item_not_enough(Gameplay.ItemsInfo.FromID(22).Name));
							return;
						}
						string holdsUiCommon_5b = Local.HoldsUiCommon_5b;
						string mineUi_ = Local.MineUi_9;
						Action<int> {21859};
						if (({21859} = CS$<>8__locals1.<>9__12) == null)
						{
							{21859} = (CS$<>8__locals1.<>9__12 = delegate(int {18272})
							{
								CS$<>8__locals1.<>4__this.AllowMouseInput = false;
								Global.Network.Send(new OnFactoryOpenOrUpgradeMsg((byte)CS$<>8__locals1.<>4__this.{18259}.FcID, OnFactoryOpenOrUpgradeMsg.Operation.AddAnimals, {18272}, 0));
							});
						}
						new {21838}(holdsUiCommon_5b, mineUi_, {21859}, num5, null, null, null, null, null);
					}, null, true, null, default(Rectangle));
				}
				else if (flag)
				{
					{18233}.<>c__DisplayClass17_0 CS$<>8__locals4 = CS$<>8__locals1;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(4, 3);
					defaultInterpolatedStringHandler2.AppendFormatted(Local.HoldsUiCommon_5a);
					defaultInterpolatedStringHandler2.AppendLiteral(" (");
					defaultInterpolatedStringHandler2.AppendFormatted<int>(Global.Player.ResourcesOfHold.GetCount(8));
					defaultInterpolatedStringHandler2.AppendLiteral(" ");
					defaultInterpolatedStringHandler2.AppendFormatted(Local.on_ship.ToLower());
					defaultInterpolatedStringHandler2.AppendLiteral(")");
					CS$<>8__locals4.<UpdateUi>g__AddButton|0(defaultInterpolatedStringHandler2.ToStringAndClear(), delegate
					{
						int count = Global.Player.ResourcesOfHold.GetCount(8);
						if (count == 0)
						{
							new {17312}(Local.item_not_enough(Gameplay.ItemsInfo.FromID(8).Name));
							return;
						}
						string holdsUiCommon_5a = Local.HoldsUiCommon_5a;
						string mineUi_ = Local.MineUi_9;
						Action<int> {21859};
						if (({21859} = CS$<>8__locals1.<>9__13) == null)
						{
							{21859} = (CS$<>8__locals1.<>9__13 = delegate(int {18273})
							{
								CS$<>8__locals1.<>4__this.AllowMouseInput = false;
								Global.Network.Send(new OnFactoryOpenOrUpgradeMsg((byte)CS$<>8__locals1.<>4__this.{18259}.FcID, OnFactoryOpenOrUpgradeMsg.Operation.AddBondman, {18273}, 0));
							});
						}
						new {21838}(holdsUiCommon_5a, mineUi_, {21859}, count, null, null, null, null, null);
					}, null, Global.Player.ResourcesOfHold.GetCount(8) > 0, null, default(Rectangle));
				}
				float num2 = (mine.ProducedResId == 22) ? 0f : Session.Account.WorldFlag.FactoryLootBonus(Session.Game);
				string text = Local.MineUi_10;
				if (num2 > 0f)
				{
					string str = text;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(6, 2);
					defaultInterpolatedStringHandler3.AppendLiteral(" (+");
					defaultInterpolatedStringHandler3.AppendFormatted<float>(num2 * 100f, "F0");
					defaultInterpolatedStringHandler3.AppendLiteral("% ");
					defaultInterpolatedStringHandler3.AppendFormatted(Local.flag_bonus);
					defaultInterpolatedStringHandler3.AppendLiteral(")");
					text = str + defaultInterpolatedStringHandler3.ToStringAndClear();
				}
				CS$<>8__locals1.<UpdateUi>g__AddButton|0(text, delegate
				{
					int count = CS$<>8__locals1.<>4__this.{18260}.GetCount(mine.ProducedResId);
					ResourceInfo itemInfo = Gameplay.ItemsInfo.FromID(mine.ProducedResId);
					string mineUi_ = Local.MineUi_10;
					string to_continue = Local.to_continue;
					Action<int> {21859} = delegate(int {18275})
					{
						if ({18275} == 0)
						{
							return;
						}
						float num5 = itemInfo.Mass * (float){18275};
						if (num5 > Global.Player.UsedShipPlayer.FreeCapacity)
						{
							new {17312}(Local.overload_ask((float)((int)Global.Player.UsedShipPlayer.GetItemsMass()) + num5, Global.Player.UsedShipPlayer.Capacity), delegate()
							{
								CS$<>8__locals1.<>4__this.{18245}({18275});
							}, delegate()
							{
							});
							return;
						}
						CS$<>8__locals1.<>4__this.{18245}({18275});
					};
					int {21860} = count;
					Func<int, string> {21861} = delegate(int {18276})
					{
						string text2 = Local.weight_is(StringHelper.BigValueHelper((int)((float){18276} * itemInfo.Mass)));
						if ((float){18276} * itemInfo.Mass > Global.Player.UsedShipPlayer.FreeCapacity)
						{
							text2 = text2 + " (" + Local.overload.ToLower() + ")";
						}
						return text2;
					};
					float? {21865} = new float?(itemInfo.Mass);
					new {21838}(mineUi_, to_continue, {21859}, {21860}, {21861}, null, null, null, {21865});
				}, null, this.{18260}.GetCount(mine.ProducedResId) > 0, null, default(Rectangle));
				float num3 = {18233}.DisplayWorkingSpeed(mine, (float)this.{18260}[mine.ConsumedResId], (float)this.{18260}[mine.ProducedResId], CS$<>8__locals1.build.Place, (float)this.{18260}[8]);
				CS$<>8__locals1.<UpdateUi>g__AddDesc|1(new string[]
				{
					Local.level_is2 + ((int)(CS$<>8__locals1.build.LevelIndex + 1)).ToString() + " / " + WosbCrafting.FactoriesInfo[CS$<>8__locals1.build.Type].Levels.Length.ToString()
				});
				StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(397f, 113f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				StackForm stackForm2 = stackForm;
				UiControl[] array = new UiControl[1];
				int num4 = 0;
				Vector2 {13342} = default(Vector2);
				CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
				Color {13344} = (num3 > 1f) ? Color.SoftLime : ((num3 < 1f) ? (Color.Orange * 0.7f) : CS$<>8__locals1.lightTextCol);
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler4.AppendFormatted(Local.weapons_speed_bycrew);
				defaultInterpolatedStringHandler4.AppendLiteral(": ");
				defaultInterpolatedStringHandler4.AppendFormatted<double>(Math.Round((double)(num3 * 100f)));
				defaultInterpolatedStringHandler4.AppendLiteral("%");
				array[num4] = new Label({13342}, philosopher_, {13344}, defaultInterpolatedStringHandler4.ToStringAndClear(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm2.AddItem(array);
				foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)gsi.ResourceInfo))
				{
					Form form4 = new Form(new Marker(0f, 0f, 380f, 64f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					Image image = new Image(new Marker(0f, 0f, 64f, 64f), gsilocalEnumerablePair.Info.IconTexture, gsilocalEnumerablePair.Info.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					{20431}.Set(image, gsilocalEnumerablePair.Info, 0, null);
					form4.AddChild(image);
					TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_10, 0f);
					textBlockBuilder.WriteLine(string.Concat(new string[]
					{
						Local.per_hour,
						": +",
						gsilocalEnumerablePair.Count.ToString(),
						", ",
						Local.per_day,
						": +",
						(24 * gsilocalEnumerablePair.Count).ToString()
					}), CS$<>8__locals1.lightTextCol * 0.6f);
					if (num3 != 1f)
					{
						textBlockBuilder.WriteLine(string.Concat(new string[]
						{
							Local.per_hour,
							": +",
							MathF.Round((float)gsilocalEnumerablePair.Count * num3, 1).ToString(),
							", ",
							Local.per_day,
							": +",
							MathF.Round((float)(24 * gsilocalEnumerablePair.Count) * num3, 1).ToString()
						}), (num3 > 1f) ? Color.SoftLime : (Color.Orange * 0.7f));
					}
					if (gsi[22] > 0)
					{
						textBlockBuilder.WriteLine(Local.desc_farm_extraLine, CS$<>8__locals1.lightTextCol * 0.6f);
					}
					textBlockBuilder.WriteLine(((gsilocalEnumerablePair.Info.ID == 22) ? (Local.have + ": ") : Local.in_storage) + this.{18260}[(int)gsilocalEnumerablePair.Info.ID].ToString() + " / " + num.ToString(), (this.{18260}[(int)gsilocalEnumerablePair.Info.ID] >= num) ? Color.OrangeRed : (CS$<>8__locals1.lightTextCol * 0.6f));
					form4.AddChild(textBlockBuilder.Create(new Vector2(72f, 0f)));
					stackForm.AddItem(new UiControl[]
					{
						form4
					});
					stackForm.AddSpace(25f);
				}
				if (!gsi2.IsEmpty)
				{
					stackForm.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Philosopher_14, CS$<>8__locals1.lightTextCol, Local.required, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair2 in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)gsi2.ResourceInfo))
					{
						TextBlockBuilder textBlockBuilder2 = new TextBlockBuilder(Fonts.Arial_10, 0f);
						ResourceInfo info2 = gsilocalEnumerablePair2.Info;
						Form form5 = new Form(new Marker(0f, 0f, 380f, 64f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						Image image2 = new Image(new Marker(0f, 0f, 64f, 64f), info2.IconTexture, info2.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						{20431}.Set(image2, info2, 0, null);
						form5.AddChild(image2);
						if (gsi[22] > 0)
						{
							textBlockBuilder2.WriteLine(Local.tt_farm, CS$<>8__locals1.lightTextCol * 0.6f);
						}
						else
						{
							textBlockBuilder2.WriteLine(string.Concat(new string[]
							{
								Local.per_hour,
								": -",
								MathF.Round((float)gsilocalEnumerablePair2.Count, 1).ToString(),
								", ",
								Local.per_day,
								": -",
								MathF.Round((float)(24 * gsilocalEnumerablePair2.Count), 1).ToString()
							}), CS$<>8__locals1.lightTextCol * 0.6f);
							if (num3 != 1f)
							{
								textBlockBuilder2.WriteLine(string.Concat(new string[]
								{
									Local.per_hour,
									": -",
									MathF.Round((float)gsilocalEnumerablePair2.Count * num3, 1).ToString(),
									", ",
									Local.per_day,
									": -",
									MathF.Round((float)(24 * gsilocalEnumerablePair2.Count) * num3, 1).ToString()
								}), (num3 > 1f) ? Color.SoftLime : (Color.Orange * 0.7f));
							}
						}
						textBlockBuilder2.WriteLine(Local.in_storage + this.{18260}[(int)info2.ID].ToString(), (this.{18260}[(int)info2.ID] == 0) ? Color.OrangeRed : (CS$<>8__locals1.lightTextCol * 0.6f));
						if (this.{18260}.CanRemove(gsi2))
						{
							GSILocalPair gsilocalPair = gsi2.First<GSILocalPair>();
							if (gsi[22] == 0)
							{
								textBlockBuilder2.WriteLine(Local.CommonBuildingWindow_9 + MathF.Floor((float)this.{18260}[gsilocalPair.ID] / ((float)gsilocalPair.Count * num3)).ToString() + Local.StringConstants_64, CS$<>8__locals1.lightTextCol * 0.6f);
							}
						}
						form5.AddChild(textBlockBuilder2.Create(new Vector2(72f, 0f)));
						stackForm.AddItem(new UiControl[]
						{
							form5
						});
					}
					stackForm.AddSpace(35f);
				}
				if (flag)
				{
					ResourceInfo resourceInfo = Gameplay.ItemsInfo.FromID(8);
					Form form6 = new Form(new Marker(0f, 0f, 380f, 64f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					Image image3 = new Image(new Marker(0f, 0f, 64f, 64f), resourceInfo.IconTexture, resourceInfo.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					{20431}.Set(image3, resourceInfo, 0, null);
					form6.AddChild(image3);
					TextBlockBuilder textBlockBuilder3 = new TextBlockBuilder(Fonts.Philosopher_14, -1f);
					textBlockBuilder3.WriteLine(Local.CommonBuildingWindow_bondman + this.{18260}[8].ToString(), CS$<>8__locals1.lightTextCol * ((this.{18260}[8] > 0) ? 1f : 0.6f));
					if (this.{18260}[8] != 0)
					{
						TextBlockBuilder textBlockBuilder4 = textBlockBuilder3;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler5 = new DefaultInterpolatedStringHandler(2, 2);
						defaultInterpolatedStringHandler5.AppendLiteral("-");
						defaultInterpolatedStringHandler5.AppendFormatted<float>(WosbCrafting.BondmanFactoryUsagePerHour);
						defaultInterpolatedStringHandler5.AppendLiteral(" ");
						defaultInterpolatedStringHandler5.AppendFormatted(Local.per_hour_reduce);
						textBlockBuilder4.WriteLine(defaultInterpolatedStringHandler5.ToStringAndClear(), CS$<>8__locals1.lightTextCol * 0.6f);
					}
					TextBlockBuilder textBlockBuilder5 = textBlockBuilder3;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler6 = new DefaultInterpolatedStringHandler(0, 1);
					defaultInterpolatedStringHandler6.AppendFormatted<float>(WosbCrafting.BondmanFactoryBoostAmount * 100f, "F0");
					textBlockBuilder5.WriteLines(Local.bondman_factory_tt(defaultInterpolatedStringHandler6.ToStringAndClear()), CS$<>8__locals1.lightTextCol * 0.6f, Fonts.Arial_10, 205f, new float?(0f));
					form6.AddChild(textBlockBuilder3.Create(new Vector2(72f, 0f)));
					stackForm.AddSpace(10f);
					stackForm.AddItem(new UiControl[]
					{
						form6
					});
				}
				if (CS$<>8__locals1.options.Pos.WH.Y > 318f)
				{
					UiControl options = CS$<>8__locals1.options;
					marker = CS$<>8__locals1.options.Pos;
					options.Pos = marker.Offset(0f, -54f);
				}
				this.{18261}.AddChild(stackForm);
				CS$<>8__locals1.<UpdateUi>g__AddButton|0(Local.MineUi_13, delegate
				{
					string mineUi_ = Local.MineUi_14;
					Action {17372};
					if (({17372} = CS$<>8__locals1.<>9__18) == null)
					{
						{17372} = (CS$<>8__locals1.<>9__18 = delegate()
						{
							CS$<>8__locals1.<>4__this.AllowMouseInput = false;
							Global.Network.Send(new OnFactoryOpenOrUpgradeMsg((byte)CS$<>8__locals1.<>4__this.{18259}.FcID, OnFactoryOpenOrUpgradeMsg.Operation.Destroy, 0, 0));
							Global.Game.SoundSystem.PlaySound(GameStaticSoundName.BattleResults, 0.03f, 1f);
						});
					}
					new {17312}(mineUi_, {17372}, delegate()
					{
					});
				}, null, true, null, default(Rectangle));
			}
			else
			{
				foreach (FactoryType factoryType in ((IEnumerable<FactoryType>)this.{18259}.Predefines))
				{
					FactoryGameInfo info = WosbCrafting.FactoriesInfo[factoryType];
					ResourceInfo resourceInfo2 = info.Levels[0].DisplayOutputRes;
					if (resourceInfo2.ID == 31)
					{
						resourceInfo2 = Gameplay.ItemsInfo.FromID(4);
					}
					if (resourceInfo2.ID == 32)
					{
						resourceInfo2 = Gameplay.ItemsInfo.FromID(11);
					}
					if (this.{18259}.Predefines.Size == 1)
					{
						CS$<>8__locals1.<UpdateUi>g__AddButton|0(Local.Build + " " + info.Name, delegate
						{
							CS$<>8__locals1.<>4__this.{18239}(info);
						}, {18233}.ToolTip(this.{18259}, factoryType, 0, null, true), (Session.Account.AvailablePlacesForBuildings > 0 || Global.Player.IsOutsideSeam) && CS$<>8__locals1.build == null, null, default(Rectangle));
					}
					else
					{
						CS$<>8__locals1.<UpdateUi>g__AddButton|0("", delegate
						{
							CS$<>8__locals1.<>4__this.{18239}(info);
						}, {18233}.ToolTip(this.{18259}, factoryType, 0, null, true), (Session.Account.AvailablePlacesForBuildings > 0 || Global.Player.IsOutsideSeam) && CS$<>8__locals1.build == null, resourceInfo2.IconTexture, resourceInfo2.IconTexture.Bounds);
					}
				}
				if (Global.Player.IsOutsideSeam)
				{
					CS$<>8__locals1.<UpdateUi>g__AddDesc|1(new string[]
					{
						Local.MineUi_countBuildings(Session.Account.Buildings.UsedPlaces, Session.Account.TotalPlacesForBuildingsLimit),
						Local.MinuUi_noPlaceUse
					});
				}
				else
				{
					CS$<>8__locals1.<UpdateUi>g__AddDesc|1(new string[]
					{
						Local.MineUi_countBuildings(Session.Account.Buildings.UsedPlaces, Session.Account.TotalPlacesForBuildingsLimit),
						Local.MinuUi_skillTollTip0,
						Local.MinuUi_skillTollTip1
					});
				}
			}
			UiControl uiControl = this.{18261};
			marker = this.{18261}.Pos;
			marker = marker.Offset(0f, -36f);
			uiControl.Pos = marker.SetHeight(this.{18261}.Pos.WH.Y + 36f);
			base.AddChild(this.{18261});
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000347F4 File Offset: 0x000329F4
		private void {18245}(int {18246})
		{
			new UiOpacityAnimation(this, 1f, 0.3f, 700f);
			base.AllowMouseInput = false;
			Session.CurrentCrewJob = new ApplyingEffectCache(Local.carrying_out, (float)(2000 + 10 * {18246}), delegate()
			{
				Global.Network.Send(new OnFactoryOpenOrUpgradeMsg((byte)this.{18259}.FcID, OnFactoryOpenOrUpgradeMsg.Operation.GetStorage, {18246}, 0));
				new UiOpacityAnimation(this, 1f, 500f);
			}, () => {18233}.currentInstance != null);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0003487C File Offset: 0x00032A7C
		public new static ToolTip ToolTip(FactoryPlaceIsleInfo {18247}, FactoryType {18248}, int {18249} = -1, GSI {18250} = null, bool {18251} = false)
		{
			int skip = {18249};
			FactoryGameInfo factoryInfo = WosbCrafting.FactoriesInfo[{18248}];
			IEnumerable<string> source = factoryInfo.Levels.Skip(skip).Select((FactoryMineLivelInfo {18277}, int {18278}) => string.Concat(new string[]
			{
				factoryInfo.Name,
				", ",
				Local.level_is2,
				({18278} + skip + 1).ToString(),
				" / ",
				factoryInfo.Levels.Length.ToString()
			}));
			IEnumerable<ToolTipConstructorPage> source2 = factoryInfo.Levels.Skip(skip).Select(delegate(FactoryMineLivelInfo {18279}, int {18280})
			{
				bool flag = !({18280} + skip != {18249} | {18251});
				Tlist<ValueTuple<string, Color>> tlist = new Tlist<ValueTuple<string, Color>>();
				Tlist<ValueTuple<string, Color>> tlist2 = tlist;
				ValueTuple<string, Color> valueTuple = new ValueTuple<string, Color>(({18279}.ProducedResId == 22) ? Local.desc_farm_tt : Local.desc_mine_tt, Color.Gray);
				tlist2.Add(valueTuple);
				int num = ({18250} == null || !flag) ? -1 : {18250}.GetCount({18279}.ProducedResId);
				int num2 = ({18250} == null || !flag) ? -1 : {18250}.GetCount({18279}.ConsumedResId);
				int num3 = ({18250} == null || !flag) ? -1 : {18250}.GetCount(8);
				float num4 = {18233}.DisplayWorkingSpeed({18279}, (float)num2, (float)num, {18247}, (float)num3);
				float num5 = {18279}.HoldCapacity(Session.Account);
				string item = string.Concat(new string[]
				{
					"+",
					{18279}.ProducedResCount.ToString(),
					"x ",
					Gameplay.ItemsInfo.FromID({18279}.ProducedResId).Name,
					" ",
					Local.per_hour
				});
				Tlist<ValueTuple<string, Color>> tlist3 = tlist;
				valueTuple = new ValueTuple<string, Color>(item, Color.White);
				tlist3.Add(valueTuple);
				if (num != -1)
				{
					Tlist<ValueTuple<string, Color>> tlist4 = tlist;
					valueTuple = new ValueTuple<string, Color>(string.Concat(new string[]
					{
						Local.storage_d,
						num.ToString(),
						" / ",
						num5.ToString(),
						Local.pcs
					}), ((float)num >= num5) ? Color.Gold : Color.White);
					tlist4.Add(valueTuple);
				}
				if ({18279}.ConsumedResCount > 0)
				{
					string item2 = string.Concat(new string[]
					{
						"-",
						{18279}.ConsumedResCount.ToString(),
						"x ",
						Gameplay.ItemsInfo.FromID({18279}.ConsumedResId).Name,
						" ",
						Local.per_hour
					});
					Tlist<ValueTuple<string, Color>> tlist5 = tlist;
					valueTuple = new ValueTuple<string, Color>(item2, Color.White);
					tlist5.Add(valueTuple);
					if (num2 != -1)
					{
						Tlist<ValueTuple<string, Color>> tlist6 = tlist;
						valueTuple = new ValueTuple<string, Color>(Local.storage_d + num2.ToString(), (num2 == 0) ? Color.Gold : Color.White);
						tlist6.Add(valueTuple);
						if (num2 == 0)
						{
							Tlist<ValueTuple<string, Color>> tlist7 = tlist;
							valueTuple = new ValueTuple<string, Color>(Local.CommonBuildingWindow_10, Color.Gold * 0.7f);
							tlist7.Add(valueTuple);
						}
					}
				}
				if (num5 > 0f && num == -1)
				{
					Tlist<ValueTuple<string, Color>> tlist8 = tlist;
					valueTuple = new ValueTuple<string, Color>(Local.storage_d + num5.ToString() + Local.pcs, Color.White);
					tlist8.Add(valueTuple);
				}
				if (num3 > 0)
				{
					Tlist<ValueTuple<string, Color>> tlist9 = tlist;
					valueTuple = new ValueTuple<string, Color>(Local.CommonBuildingWindow_bondman + num3.ToString(), Color.White);
					tlist9.Add(valueTuple);
				}
				Color item3 = (num4 > 1.01f) ? Color.Lerp(Color.Gold, Color.Lime, 0.3f) : ((num4 < 0.99f) ? Color.Lerp(Color.Gold, Color.Red, 0.3f) : Color.White);
				Tlist<ValueTuple<string, Color>> tlist10 = tlist;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted(Local.weapons_speed_bycrew);
				defaultInterpolatedStringHandler.AppendLiteral(": ");
				defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round((double)(num4 * 100f)));
				defaultInterpolatedStringHandler.AppendLiteral("%");
				valueTuple = new ValueTuple<string, Color>(defaultInterpolatedStringHandler.ToStringAndClear(), item3);
				tlist10.Add(valueTuple);
				if (num4 != 1f)
				{
					Tlist<ValueTuple<string, Color>> tlist11 = tlist;
					valueTuple = new ValueTuple<string, Color>(string.Concat(new string[]
					{
						"+",
						{18233}.<ToolTip>g__RoundHelper|19_0((float){18279}.ProducedResCount, num4),
						"x ",
						Gameplay.ItemsInfo.FromID({18279}.ProducedResId).Name,
						" ",
						Local.per_hour
					}), item3);
					tlist11.Add(valueTuple);
					if ({18279}.ConsumedResCount > 0)
					{
						Tlist<ValueTuple<string, Color>> tlist12 = tlist;
						valueTuple = new ValueTuple<string, Color>(string.Concat(new string[]
						{
							"-",
							{18233}.<ToolTip>g__RoundHelper|19_0((float){18279}.ConsumedResCount, num4),
							"x ",
							Gameplay.ItemsInfo.FromID({18279}.ConsumedResId).Name,
							" ",
							Local.per_hour
						}), item3);
						tlist12.Add(valueTuple);
					}
				}
				Tlist<ValueTuple<string, Color>> tlist13 = tlist;
				valueTuple = new ValueTuple<string, Color>(" ", Color.White);
				tlist13.Add(valueTuple);
				if ({18280} + skip < factoryInfo.Levels.Length && ({18280} + skip != {18249} | {18251}))
				{
					if ({18280} + skip == 0)
					{
						Tlist<ValueTuple<string, Color>> tlist14 = tlist;
						valueTuple = new ValueTuple<string, Color>(Local.cost_build, Color.White);
						tlist14.Add(valueTuple);
					}
					else
					{
						Tlist<ValueTuple<string, Color>> tlist15 = tlist;
						valueTuple = new ValueTuple<string, Color>(Local.cost_upgrade, Color.White);
						tlist15.Add(valueTuple);
					}
					ValueTuple<int, GSI> valueTuple2 = factoryInfo.LevelUpCost({18280} + skip);
					if (valueTuple2.Item1 > 0)
					{
						Tlist<ValueTuple<string, Color>> tlist16 = tlist;
						valueTuple = new ValueTuple<string, Color>(Local.gold + " -" + valueTuple2.Item1.ToString(), (valueTuple2.Item1 > Session.Account.Gold) ? Color.OrangeRed : Color.Gold);
						tlist16.Add(valueTuple);
					}
					tlist.Add(valueTuple2.Item2.WriteToolTipData_ResourcesOnly(Global.Player.ResourcesOfHold, false));
				}
				else
				{
					Tlist<ValueTuple<string, Color>> tlist17 = tlist;
					valueTuple = new ValueTuple<string, Color>(Local.worldMapToolTip1, Color.Gray * 0.5f);
					tlist17.Add(valueTuple);
					Tlist<ValueTuple<string, Color>> tlist18 = tlist;
					valueTuple = new ValueTuple<string, Color>(Local.worldMapToolTip2, Color.Gray * 0.5f);
					tlist18.Add(valueTuple);
				}
				return new ToolTipConstructorPage((from {18262} in tlist
				select {18262}.Item1).ToArray<string>(), (from {18263} in tlist
				select {18263}.Item2).ToArray<Color>());
			});
			return new ToolTip(0f, float.MaxValue, new ToolTipState(source.ToArray<string>(), source2.ToArray<ToolTipConstructorPage>(), CommonAtlas.tt_arrowKeys_left, CommonAtlas.tt_arrowKeys_right, CommonAtlas.Texture.Tex, null));
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00034950 File Offset: 0x00032B50
		private static float DisplayWorkingSpeed(FactoryMineLivelInfo {18252}, float {18253}, float {18254}, FactoryPlaceIsleInfo {18255}, float {18256})
		{
			float result = {18252}.WorkingSpeed({18253}, {18254}, {18255}, Session.Account, {18256} > 0f, true, Session.Game.FactoryMiningSpeedBonus({18255}.GlobalPosition));
			if ({18252}.ConsumedResCount > 0 && {18253} == 0f && !{18252}.WorksWithoutPriovision(Session.Account))
			{
				result = 0f;
			}
			return result;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00034A10 File Offset: 0x00032C10
		[CompilerGenerated]
		internal static string <ToolTip>g__RoundHelper|19_0(float {18257}, float {18258})
		{
			return Math.Round((double)({18257} * {18258}), 1).ToString();
		}

		// Token: 0x040005E6 RID: 1510
		public static readonly Rectangle c_itemButton = new Rectangle(994, 580, 385, 39);

		// Token: 0x040005E7 RID: 1511
		public static readonly Rectangle c_itemButtonBox = new Rectangle(1380, 626, 98, 55);

		// Token: 0x040005E8 RID: 1512
		public static Rectangle c_page_background = new Rectangle(1879, 3227, 299, 219);

		// Token: 0x040005E9 RID: 1513
		private static {18233} currentInstance;

		// Token: 0x040005EA RID: 1514
		private FactoryPlaceIsleInfo {18259};

		// Token: 0x040005EB RID: 1515
		private GSI {18260};

		// Token: 0x040005EC RID: 1516
		private Form {18261};
	}
}
