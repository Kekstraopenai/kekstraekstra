using System;
using System.Collections;
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
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001BB RID: 443
	internal sealed class {19150} : {17625}
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x000030FD File Offset: 0x000012FD
		private static bool editor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0004EED8 File Offset: 0x0004D0D8
		private static IEnumerable<{17625}.DynamicTittle> pagesIterator()
		{
			return new {19150}.<pagesIterator>d__15(-2);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0004EEE4 File Offset: 0x0004D0E4
		public static ToolTipState GetToolTipContent(CaptainSkillInfo {19151}, bool {19152} = false)
		{
			PlayerCaptainSkillsStorage.Availability availability;
			if (!Session.Account.CaptainSkills.IsImproved({19151}))
			{
				PlayerCaptainSkillsStorage captainSkills = Session.Account.CaptainSkills;
				PlayerAccount account = Session.Account;
				Decorator game = Session.Game;
				availability = captainSkills.AllowImprove({19151}, account, game.IsInPortOrIsleWithStorage);
			}
			else
			{
				availability = PlayerCaptainSkillsStorage.Availability.Yes;
			}
			PlayerCaptainSkillsStorage.Availability availability2 = availability;
			Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
			if ({19152})
			{
				foreach (string {12730} in {19151}.Description.Split(new char[]
				{
					'$'
				}, StringSplitOptions.RemoveEmptyEntries))
				{
					Tlist<ToolTipCharacteristics> tlist2 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics({12730}, CharacteristicsColor.Gray);
					tlist2.Add(toolTipCharacteristics);
				}
			}
			if (Session.Account.CaptainSkills.IsImproved({19151}))
			{
				PlayerCaptainSkillsStorage captainSkills2 = Session.Account.CaptainSkills;
				Decorator game = Session.Game;
				string {12730}2;
				if (!captainSkills2.CanBeResetted(game, {19151}, Session.CountOfCapers, out {12730}2))
				{
					Tlist<ToolTipCharacteristics> tlist3 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics({12730}2, CharacteristicsColor.Orange);
					tlist3.Add(toolTipCharacteristics);
				}
				else if ({19151}.CostPoints == 0)
				{
					Tlist<ToolTipCharacteristics> tlist4 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.CaptainSkillsInfoWindow_resetReady_1b, CharacteristicsColor.Lime);
					tlist4.Add(toolTipCharacteristics);
				}
				else
				{
					Tlist<ToolTipCharacteristics> tlist5 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.CaptainSkillsInfoWindow_resetReady_2(Gameplay.CostResetCaptainSkillGold(Session.Account)), (Gameplay.CostResetCaptainSkillGold(Session.Account) <= Session.Account.Gold) ? CharacteristicsColor.Lime : CharacteristicsColor.Orange);
					tlist5.Add(toolTipCharacteristics);
					Tlist<ToolTipCharacteristics> tlist6 = tlist;
					toolTipCharacteristics = new ToolTipCharacteristics(Local.CaptainSkillsInfoWindow_resetReady_1, CharacteristicsColor.LimeBold);
					tlist6.Add(toolTipCharacteristics);
				}
			}
			else
			{
				if ({19151}.CostGold.Value > 0)
				{
					Tlist<ToolTipCharacteristics> tlist7 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(string.Concat(new string[]
					{
						Local.cost,
						": ",
						StringHelper.BigValueHelper({19151}.CostGold.Value),
						" ",
						Local.gold2
					}), ({19151}.CostGold.Value <= Session.Account.Gold) ? CharacteristicsColor.Lime : CharacteristicsColor.Orange);
					tlist7.Add(toolTipCharacteristics);
				}
				if ({19151}.CostPoints > 0)
				{
					Tlist<ToolTipCharacteristics> tlist8 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.cost + ": " + Local.CaptainSkillsInfoWindow_1({19151}.CostPoints), ({19151}.CostPoints <= Session.Account.SkillPointsAvailable) ? CharacteristicsColor.Lime : CharacteristicsColor.Orange);
					tlist8.Add(toolTipCharacteristics);
				}
				foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){19151}.CostResources.ResourceInfo))
				{
					Tlist<ToolTipCharacteristics> tlist9 = tlist;
					string {12730}3 = string.Concat(new string[]
					{
						Local.cost,
						": ",
						gsilocalEnumerablePair.Count.ToString(),
						" ",
						gsilocalEnumerablePair.Info.Name
					});
					Decorator game = Session.Game;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics({12730}3, (game.IsInPortOrIsleWithStorage && Session.Account.NearPortStorage[(int)gsilocalEnumerablePair.Info.ID] >= gsilocalEnumerablePair.Count) ? CharacteristicsColor.Lime : CharacteristicsColor.Orange);
					tlist9.Add(toolTipCharacteristics);
				}
				if ({19151}.Effect == PDynamicAccountBonus.CRolePlankingLevel || {19151}.Effect == PDynamicAccountBonus.CRoleSmithevel)
				{
					Tlist<ToolTipCharacteristics> tlist10 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.CaptainSkillsInfoWindow_20, CharacteristicsColor.Gray);
					tlist10.Add(toolTipCharacteristics);
					Tlist<ToolTipCharacteristics> tlist11 = tlist;
					toolTipCharacteristics = new ToolTipCharacteristics(Local.CaptainSkillsInfoWindow_21, CharacteristicsColor.Gray);
					tlist11.Add(toolTipCharacteristics);
				}
				if ({19151}.Effect == PDynamicAccountBonus.CFactoryCount)
				{
					Tlist<ToolTipCharacteristics> tlist12 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.CaptainSkillsInfoWindow_17(Session.Account.Buildings.UsedPlaces), CharacteristicsColor.Gray);
					tlist12.Add(toolTipCharacteristics);
				}
				if ({19151}.Effect == PDynamicAccountBonus.CPortStorageLimit)
				{
					Tlist<ToolTipCharacteristics> tlist13 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.CaptainSkillsInfoWindow_18(Session.Account.ResourcesInPorts.CountBuiltWarehouses()), CharacteristicsColor.Gray);
					tlist13.Add(toolTipCharacteristics);
				}
				bool flag = false;
				if ({19151}.RequiredAchievements.Length != 0)
				{
					Tlist<ToolTipCharacteristics> tlist14 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics("", "");
					tlist14.Add(toolTipCharacteristics);
					Tlist<ToolTipCharacteristics> tlist15 = tlist;
					toolTipCharacteristics = new ToolTipCharacteristics(Local.must_have_achiev, CharacteristicsColor.WheatBold);
					tlist15.Add(toolTipCharacteristics);
					foreach (AchievementEnum achievementEnum in {19151}.RequiredAchievements)
					{
						if (Session.Account.Achievements.Count(achievementEnum) == 0)
						{
							flag = true;
							Tlist<ToolTipCharacteristics> tlist16 = tlist;
							toolTipCharacteristics = new ToolTipCharacteristics(Gameplay.AchievementsByEnum[achievementEnum].Name, Local.no, CharacteristicsColor.Orange);
							tlist16.Add(toolTipCharacteristics);
						}
						else
						{
							Tlist<ToolTipCharacteristics> tlist17 = tlist;
							toolTipCharacteristics = new ToolTipCharacteristics(Gameplay.AchievementsByEnum[achievementEnum].Name, Local.have, CharacteristicsColor.Lime);
							tlist17.Add(toolTipCharacteristics);
						}
					}
				}
				if ({19151}.RequiredShips.Length != 0)
				{
					Tlist<ToolTipCharacteristics> tlist18 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics("", "");
					tlist18.Add(toolTipCharacteristics);
					Tlist<ToolTipCharacteristics> tlist19 = tlist;
					toolTipCharacteristics = new ToolTipCharacteristics(Local.must_have_ships, CharacteristicsColor.WheatBold);
					tlist19.Add(toolTipCharacteristics);
					foreach (PlayerShipInfo playerShipInfo in {19151}.RequiredShips)
					{
						if (!Session.Account.Shipyard.ContainsInfo(playerShipInfo))
						{
							flag = true;
							Tlist<ToolTipCharacteristics> tlist20 = tlist;
							toolTipCharacteristics = new ToolTipCharacteristics(playerShipInfo.ShipName, Local.no, CharacteristicsColor.Orange);
							tlist20.Add(toolTipCharacteristics);
						}
						else
						{
							Tlist<ToolTipCharacteristics> tlist21 = tlist;
							toolTipCharacteristics = new ToolTipCharacteristics(playerShipInfo.ShipName, Local.have, CharacteristicsColor.Lime);
							tlist21.Add(toolTipCharacteristics);
						}
					}
				}
				if (availability2 == PlayerCaptainSkillsStorage.Availability.Yes && !flag)
				{
					Tlist<ToolTipCharacteristics> tlist22 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.CaptainSkillsInfoWindow_16, CharacteristicsColor.LimeBold);
					tlist22.Add(toolTipCharacteristics);
				}
				if (availability2 == PlayerCaptainSkillsStorage.Availability.NoDependSkills)
				{
					Tlist<ToolTipCharacteristics> tlist23 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.CaptainSkillsInfoWindow_241, CharacteristicsColor.Orange);
					tlist23.Add(toolTipCharacteristics);
				}
				if (availability2 == PlayerCaptainSkillsStorage.Availability.ConflictingSkills)
				{
					Tlist<ToolTipCharacteristics> tlist24 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.CaptainSkillsInfoWindow_242, CharacteristicsColor.Orange);
					tlist24.Add(toolTipCharacteristics);
				}
				if (availability2 == PlayerCaptainSkillsStorage.Availability.NoRank)
				{
					Tlist<ToolTipCharacteristics> tlist25 = tlist;
					ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Local.CaptainSkillsInfoWindow_243({19151}.RequiredRank), CharacteristicsColor.Orange);
					tlist25.Add(toolTipCharacteristics);
				}
			}
			return new ToolTipState({19151}.Name, {19151}.Description.Replace("$", " "), tlist.ToArray());
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0004F490 File Offset: 0x0004D690
		public {19150}() : base(920f, {17625}.c_back3, {17604}.InGameWindow, {17625}.c_icStreeringWheel, {19150}.pagesIterator().ToArray<{17625}.DynamicTittle>())
		{
			Global.Game.SceneGame.IncreaseMouse();
			base.EvRemoveFromContainer += delegate()
			{
				Global.Game.SceneGame.DecreaseMouse();
			};
			base.ComposeTabWithScroll(null, true, true, new Action<ListItemViewControl>[]
			{
				new Action<ListItemViewControl>(this.{19155}),
				new Action<ListItemViewControl>(this.{19153})
			});
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0004F534 File Offset: 0x0004D734
		private void {19153}(ListItemViewControl {19154})
		{
			{19154}.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat, " " + Local.titles_tt, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			Form form = new Form(new Marker(0f, 0f, ref {19150}.c_categoryHeader).SetHeight(20f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Form {13204} = new Form(new Vector2(-46f, 0f), {19150}.c_categoryHeader, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form.AddChild({13204});
			{19154}.AddItem(new UiControl[]
			{
				form
			});
			this.{19166}({19154});
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0004F5E8 File Offset: 0x0004D7E8
		private void {19155}(ListItemViewControl {19156})
		{
			Marker marker2;
			if (this.{19177} == null)
			{
				Vector2 vector = Vector2.Zero;
				this.{19177} = new Form(new Marker(ref vector, 250f, 37f), new Rectangle(691, 484, 289, 44), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				this.{19177}.AddChildPos(new LiveLabel(Vector2.Zero, Fonts.Philosopher_16, Color.SkyBlue, () => "◊ " + Local.CaptainSkillsInfoWindow_1(Session.Account.SkillPointsAvailable), 100), PositionAlignment.Center, PositionAlignment.Center, 0f);
				base.AddChildPos(this.{19177}, PositionAlignment.RightDown, PositionAlignment.LeftUp, 45f, 26f, false);
				vector = default(Vector2);
				StackForm stackForm = new StackForm(vector, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Marker marker = new Marker(0f, 0f, 50f, 50f);
				marker2 = base.Pos;
				Form form = new Form(marker.Offset(marker2.XY), Session.CaptainIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form.UpdateComplete += delegate(UiControl {19178})
				{
					((Form){19178}).TexturePath = Session.CaptainIcon;
				};
				StackForm stackForm2 = stackForm;
				UiControl[] array = new UiControl[1];
				int num = 0;
				vector = default(Vector2);
				array[num] = new LabelButton(vector, " < ", Fonts.Philosopher_16Bold, Color.LightGray, Color.Gold, delegate(ClickUiEventArgs {19179})
				{
					Session.Account.SelectedCaptainIcon = (byte)(((int)Session.Account.SelectedCaptainIcon + CommonAtlas.captainIcons.Length - 1) % CommonAtlas.captainIcons.Length);
				});
				stackForm2.AddItem(array);
				stackForm.AddItem(new UiControl[]
				{
					form
				});
				StackForm stackForm3 = stackForm;
				UiControl[] array2 = new UiControl[1];
				int num2 = 0;
				vector = default(Vector2);
				array2[num2] = new LabelButton(vector, " > ", Fonts.Philosopher_16Bold, Color.LightGray, Color.Gold, delegate(ClickUiEventArgs {19180})
				{
					Session.Account.SelectedCaptainIcon = (byte)((int)(Session.Account.SelectedCaptainIcon + 1) % CommonAtlas.captainIcons.Length);
				});
				stackForm3.AddItem(array2);
				base.AddChildPos(stackForm, PositionAlignment.RightDown, PositionAlignment.LeftUp, 300f, 20f, false);
			}
			BlocksStackFormControl blocksStackFormControl = new BlocksStackFormControl(Vector2.Zero, 5, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BorderThickness = 3f
			};
			ValueTuple<string, {19150}.CaptainSkillCategory>[] array3 = {19150}.skillCategoryNames;
			for (int i = 0; i < array3.Length; i++)
			{
				ValueTuple<string, {19150}.CaptainSkillCategory> valueTuple = array3[i];
				{19150}.CaptainSkillCategory categoryIndex = valueTuple.Item2;
				bool flag = this.{19176} == categoryIndex;
				CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
				StackForm stackForm4 = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Button button = new Button(Vector2.Zero, (flag ? AtlasPortGui.cSelectorPicked : AtlasPortGui.cSelectorPickedNeutral).SetWidth(176f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Vector2 vector = default(Vector2);
				Label label = new Label(vector, philosopher_, flag ? new Color(204, 203, 155) : new Color(160, 160, 160), valueTuple.Item1, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button.AddChildPos(label, PositionAlignment.Center, PositionAlignment.LeftUp, 0f, 5f, false);
				float num3 = 27f;
				marker2 = button.Pos;
				float width = marker2.Width;
				float num4 = num3 + (width - num3) / 2f;
				float x = philosopher_.Measure(valueTuple.Item1).X;
				float x2 = num4 - x / 2f;
				UiControl uiControl = label;
				vector = new Vector2(x2, label.Pos.XY.Y);
				marker2 = label.Pos;
				uiControl.Pos = new Marker(ref vector, ref marker2.WH);
				button.EvClick += delegate(ClickUiEventArgs {19183})
				{
					if (this.{19176} != categoryIndex)
					{
						this.{19176} = categoryIndex;
						this.RefreshCurrentDynamicTabPage();
					}
				};
				stackForm4.AddItem(new UiControl[]
				{
					new TextureHost(AtlasPortGui.Texture.Tex, button.Pos).AddChild(button)
				});
				blocksStackFormControl.AddItem(new UiControl[]
				{
					stackForm4
				});
			}
			{19156}.AddItem(new UiControl[]
			{
				new Form(new Marker(0f, 0f, 5f, 5f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			{19156}.AddItem(new UiControl[]
			{
				blocksStackFormControl
			});
			marker2 = new Marker(0f, 0f, ref {19150}.c_categoryHeader);
			Form form2 = new Form(marker2.SetHeight(20f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Form {13204} = new Form(new Vector2(-46f, 0f), {19150}.c_categoryHeader, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form2.AddChild({13204});
			{19156}.AddItem(new UiControl[]
			{
				form2
			});
			this.{19157}({19156});
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0004FA80 File Offset: 0x0004DC80
		private void {19157}(ListItemViewControl {19158})
		{
			{19150}.<>c__DisplayClass24_0 CS$<>8__locals1 = new {19150}.<>c__DisplayClass24_0();
			CS$<>8__locals1.<>4__this = this;
			if (this.{19176} == {19150}.CaptainSkillCategory.Legend && Session.Account.Rang < EducationHelper.ShowLegendarySkillsRank)
			{
				StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Form form = new Form(new Marker(0f, 0f, 400f, 150f), new Rectangle(941, 326, 149, 83), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				Form form2 = new Form(Vector2.Zero, new Rectangle(2933, 0, 733, 486), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form2.Pos = form2.Pos.Scale(0.6f);
				form.AddChildPos(form2, PositionAlignment.Center, PositionAlignment.Center, 0f);
				form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_16, Color.Wheat, Local.CaptainSkillsInfoWindow_243(EducationHelper.ShowLegendarySkillsRank), PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.Center, 0f);
				stackForm.AddSpace(240f);
				stackForm.AddItem(new UiControl[]
				{
					form
				});
				{19158}.AddItem(new UiControl[]
				{
					new Form(new Marker(0f, 0f, 1f, 100f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				{19158}.AddItem(new UiControl[]
				{
					stackForm
				});
				return;
			}
			IEnumerable<CaptainSkillInfo> skillsToDisplay = this.GetSkillsToDisplay();
			float maxYIndex = this.GetMaxYIndex(skillsToDisplay);
			CS$<>8__locals1.dltX = -5;
			CS$<>8__locals1.dltY = 5;
			CS$<>8__locals1.outputForm = new Form(new Marker(5f, -20f, (float)(({19150}.c_item.Width + CS$<>8__locals1.dltX) * 3), (maxYIndex + 1f) * (float)({19150}.c_item.Height + CS$<>8__locals1.dltY) + 20f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Tlist<Form> tlist = new Tlist<Form>();
			CS$<>8__locals1.skillWithUIPos = new Dictionary<short, Vector2>();
			int num = 0;
			int num2 = 0;
			using (IEnumerator<CaptainSkillInfo> enumerator = skillsToDisplay.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					{19150}.<>c__DisplayClass24_1 CS$<>8__locals2 = new {19150}.<>c__DisplayClass24_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					CS$<>8__locals2.skill = enumerator.Current;
					{19150}.<>c__DisplayClass24_2 CS$<>8__locals3 = new {19150}.<>c__DisplayClass24_2();
					CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals2;
					Vector2 {13189} = this.{19160}(CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.dltX, CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.dltY, ref num, ref num2, CS$<>8__locals3.CS$<>8__locals2.skill);
					bool flag = Session.Account.CaptainSkills.IsImproved(CS$<>8__locals3.CS$<>8__locals2.skill);
					int num3 = (int)(flag ? PlayerCaptainSkillsStorage.Availability.Yes : Session.Account.CaptainSkills.AllowImprove(CS$<>8__locals3.CS$<>8__locals2.skill, Session.Account, Session.Game.IsInPortOrIsleWithStorage));
					CS$<>8__locals3.form = new Form({13189}, {19150}.c_item, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					};
					CS$<>8__locals3.form.Tag = CS$<>8__locals3.CS$<>8__locals2.skill;
					tlist.Add(CS$<>8__locals3.form);
					if (!flag)
					{
						CS$<>8__locals3.form.Brightness = 0.75f;
					}
					CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.skillWithUIPos.Add(CS$<>8__locals3.CS$<>8__locals2.skill.ID, CS$<>8__locals3.CS$<>8__locals2.skill.UiPosition);
					CS$<>8__locals3.labelUIPosition = null;
					CS$<>8__locals3.isChanged = false;
					if ({19150}.editor)
					{
						CS$<>8__locals3.<AppendPage>g__UpdateLabelUIPosition|0();
						CS$<>8__locals3.form.AllowDragDrop = true;
						CS$<>8__locals3.form.EvDrop += delegate()
						{
							Vector2 vector2 = CS$<>8__locals3.form.Pos.XY - CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.outputForm.Pos.XY;
							int num4 = (int)Math.Round((double)(vector2.X / ({19150}.c_item.WidthHeight().X + (float)CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.dltX)));
							int num5 = (int)Math.Round((double)(vector2.Y / ({19150}.c_item.WidthHeight().Y + (float)CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.dltY)));
							CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.skillWithUIPos[CS$<>8__locals3.CS$<>8__locals2.skill.ID] = new Vector2((float)num4, (float)num5);
							CS$<>8__locals3.isChanged = ((float)num4 != CS$<>8__locals3.CS$<>8__locals2.skill.UiPosition.X || (float)num5 != CS$<>8__locals3.CS$<>8__locals2.skill.UiPosition.Y);
							base.<AppendPage>g__UpdateLabelUIPosition|0();
						};
					}
					Vector2 vector;
					if (flag)
					{
						Form form3 = CS$<>8__locals3.form;
						vector = CS$<>8__locals3.form.Pos.XY + new Vector2(86f, 13f);
						form3.AddChild(new Form(new Marker(ref vector, 210f, 25f), CommonAtlas.c_line_yellow, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							AnimatedFocus = false
						});
					}
					Form form4 = CS$<>8__locals3.form;
					vector = CS$<>8__locals3.form.Pos.XY + new Vector2(40f, 6f);
					form4.AddChild(new Image(new Marker(ref vector, 43f, 43f), CS$<>8__locals3.CS$<>8__locals2.skill.IconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					CS$<>8__locals3.form.AddChild(new Label(CS$<>8__locals3.form.Pos.XY + new Vector2(90f, 16f), Fonts.Philosopher_14, flag ? Color.LightYellow : new Color(173, 168, 143), CS$<>8__locals3.CS$<>8__locals2.skill.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					string text = CS$<>8__locals3.CS$<>8__locals2.skill.Description.Replace("$", " ");
					if (text.Length > 60)
					{
						text = text.Substring(0, 57) + "...";
					}
					TextBlockBuilder textBlockBuilder = TextBlockBuilder.CreateBlock(250f, text, Color.LightGray * 0.8f, Fonts.Arial_10, -1f);
					CS$<>8__locals3.form.AddChild(textBlockBuilder.Create(CS$<>8__locals3.form.Pos.XY + new Vector2(30f, 55f)));
					if (num3 != 1)
					{
						Label label = new Label(CS$<>8__locals3.form.Pos.XY + new Vector2(30f, 93f), Fonts.Arial_10Bold, flag ? Color.LightGray : ((CS$<>8__locals3.CS$<>8__locals2.skill.RequiredRank > Session.Account.Rang) ? new Color(203, 212, 94) : new Color(99, 206, 228)), (CS$<>8__locals3.CS$<>8__locals2.skill.RequiredRank > Session.Account.Rang) ? Local.CaptainSkillsInfoWindow_243(CS$<>8__locals3.CS$<>8__locals2.skill.RequiredRank) : ((CS$<>8__locals3.CS$<>8__locals2.skill.CostPoints > 0) ? ("◊ " + CS$<>8__locals3.CS$<>8__locals2.skill.CostPoints.ToString()) : ""), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						Label label2 = new Label(label.Pos.XY + new Vector2(label.Pos.WH.X + 8f, 0f), Fonts.Arial_10, flag ? Color.LightGray : new Color(187, 189, 66), (CS$<>8__locals3.CS$<>8__locals2.skill.RequiredRank <= Session.Account.Rang && CS$<>8__locals3.CS$<>8__locals2.skill.CostGold.Value > 0) ? (" -" + StringHelper.BigValueHelper(CS$<>8__locals3.CS$<>8__locals2.skill.CostGold.Value)) : "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						CS$<>8__locals3.form.AddChild(new UiControl[]
						{
							label,
							label2
						});
						if (flag)
						{
							label.Opacity = 0.35f;
							label2.Opacity = 0.35f;
						}
					}
					CS$<>8__locals3.form.ToolTip = new ToolTip((UiControl {19184}) => {19150}.GetToolTipContent(CS$<>8__locals3.CS$<>8__locals2.skill, false));
					if (!{19150}.editor)
					{
						CS$<>8__locals3.form.EvClick += delegate(ClickUiEventArgs {19185})
						{
							Decorator game;
							if (Session.Account.CaptainSkills.IsImproved(CS$<>8__locals3.CS$<>8__locals2.skill))
							{
								PlayerCaptainSkillsStorage captainSkills = Session.Account.CaptainSkills;
								game = Session.Game;
								string text2;
								if (captainSkills.CanBeResetted(game, CS$<>8__locals3.CS$<>8__locals2.skill, Session.CountOfCapers, out text2))
								{
									int num4 = Gameplay.CostResetCaptainSkillGold(Session.Account);
									if (Session.Account.Gold >= num4)
									{
										string skill_cantberesetted_Q = Local.skill_cantberesetted_Q;
										RTI {17353} = num4;
										Action {17354};
										if (({17354} = CS$<>8__locals3.<>9__6) == null)
										{
											{17354} = (CS$<>8__locals3.<>9__6 = delegate()
											{
												PlayerCaptainSkillsStorage captainSkills3 = Session.Account.CaptainSkills;
												CaptainSkillInfo skill2 = CS$<>8__locals3.CS$<>8__locals2.skill;
												int countOfCapers = Session.CountOfCapers;
												Decorator game2 = Session.Game;
												captainSkills3.ResetSkill(skill2, countOfCapers, game2);
												Global.Game.SoundSystem.PlaySound(GameStaticSoundName.AchievOrLevelUp, 0.03f, 1f);
												ToolTip toolTip = CS$<>8__locals3.form.ToolTip;
												if (toolTip != null)
												{
													toolTip.CloseIfIsOpen();
												}
												if (!Global.Player.IsPortEntry)
												{
													Global.Network.Send(new OnChangeCaptainSkillsMsg(CS$<>8__locals3.CS$<>8__locals2.skill.ID, OnChangeCaptainSkillsMsg.SAction.RemoveSkill));
												}
												else if (CS$<>8__locals3.CS$<>8__locals2.skill.NeedSyncInPort)
												{
													Global.Game.ScenePort.MakeAccSync();
												}
												CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.RefreshCurrentDynamicTabPage();
											});
										}
										{17312}.AskPrice(skill_cantberesetted_Q, {17353}, {17354}, true);
									}
								}
								return;
							}
							PlayerCaptainSkillsStorage captainSkills2 = Session.Account.CaptainSkills;
							CaptainSkillInfo skill = CS$<>8__locals3.CS$<>8__locals2.skill;
							PlayerAccount account = Session.Account;
							game = Session.Game;
							PlayerCaptainSkillsStorage.Availability availability = captainSkills2.AllowImprove(skill, account, game.IsInPortOrIsleWithStorage);
							if (availability == PlayerCaptainSkillsStorage.Availability.NoGold || availability == PlayerCaptainSkillsStorage.Availability.NoPoints)
							{
								return;
							}
							foreach (AchievementEnum {1874} in CS$<>8__locals3.CS$<>8__locals2.skill.RequiredAchievements)
							{
								if (Session.Account.Achievements.Count({1874}) == 0)
								{
									return;
								}
							}
							if (availability != PlayerCaptainSkillsStorage.Availability.Yes)
							{
								return;
							}
							string {17371} = Local.CaptainSkillsInfoWindow_23(CS$<>8__locals3.CS$<>8__locals2.skill.Name);
							Action {17372};
							if (({17372} = CS$<>8__locals3.<>9__4) == null)
							{
								{17372} = (CS$<>8__locals3.<>9__4 = delegate()
								{
									Global.Game.SoundSystem.PlaySound(GameStaticSoundName.AchievOrLevelUp, 0.03f, 1f);
									Session.Account.CaptainSkills.Improve(CS$<>8__locals3.CS$<>8__locals2.skill, Session.Account);
									EducationHelper.MakeFlag(EducationOnboarding.UpgradeCaptainSkill, true);
									if (CS$<>8__locals3.CS$<>8__locals2.skill.UiCategory == 1)
									{
										EducationHelper.MakeFlag(EducationOnboarding.ResearchSkillSailingCategory, true);
									}
									if (!Global.Player.IsPortEntry)
									{
										Global.Network.Send(new OnChangeCaptainSkillsMsg(CS$<>8__locals3.CS$<>8__locals2.skill.ID, OnChangeCaptainSkillsMsg.SAction.UpgradeSkill));
									}
									else if (CS$<>8__locals3.CS$<>8__locals2.skill.NeedSyncInPort)
									{
										Global.Game.ScenePort.MakeAccSync();
									}
									ToolTip toolTip = CS$<>8__locals3.form.ToolTip;
									if (toolTip != null)
									{
										toolTip.CloseIfIsOpen();
									}
									CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.RefreshCurrentDynamicTabPage();
								});
							}
							new {17312}({17371}, {17372}, delegate()
							{
							});
						};
					}
					CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.outputForm.AddChild(CS$<>8__locals3.form);
				}
			}
			this.{19175}.Clear();
			foreach (Form form5 in ((IEnumerable<Form>)tlist))
			{
				CaptainSkillInfo skillInfo = (CaptainSkillInfo)form5.Tag;
				if (skillInfo.DependsTo != null && this.{19176} != {19150}.CaptainSkillCategory.Learned)
				{
					Form form6 = tlist.First((Form {19186}) => {19186}.Tag == skillInfo.DependsTo);
					Tlist<ValueTuple<Form, Form>> tlist2 = this.{19175};
					ValueTuple<Form, Form> valueTuple = new ValueTuple<Form, Form>(form5, form6);
					tlist2.Add(valueTuple);
					form6.AddChild(new Form(form6.Pos.XY + new Vector2(form6.Pos.WH.X / 2f - (float)({19150}.c_branch.Width / 2), form6.Pos.WH.Y - 15f) + new Vector2(-4f), {19150}.c_branch, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false,
						RenderToDepthMap = false
					});
				}
			}
			{19158}.AddItem(new UiControl[]
			{
				CS$<>8__locals1.outputForm
			});
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000503B8 File Offset: 0x0004E5B8
		private float GetMaxYIndex(IEnumerable<CaptainSkillInfo> {19159})
		{
			float num = 0f;
			if (this.{19176} == {19150}.CaptainSkillCategory.Learned)
			{
				num = (float)((int)Math.Ceiling((double){19159}.Count<CaptainSkillInfo>() / 3.0));
			}
			else
			{
				foreach (CaptainSkillInfo captainSkillInfo in {19159})
				{
					num = Math.Max(num, captainSkillInfo.UiPosition.Y);
				}
			}
			return num;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00050438 File Offset: 0x0004E638
		private IEnumerable<CaptainSkillInfo> GetSkillsToDisplay()
		{
			IEnumerable<CaptainSkillInfo> result;
			if (this.{19176} == {19150}.CaptainSkillCategory.Learned)
			{
				result = from {19181} in Gameplay.CaptainSkillsInfo
				where !{19181}.IsRemoved && Session.Account.CaptainSkills.IsImproved({19181})
				select {19181} into {19182}
				orderby {19182}.UiCategory
				select {19182};
			}
			else
			{
				result = Gameplay.CaptainSkillsInfo.Where(new Func<CaptainSkillInfo, bool>(this.{19173}));
			}
			return result;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x000504B8 File Offset: 0x0004E6B8
		private Vector2 {19160}(int {19161}, int {19162}, ref int {19163}, ref int {19164}, CaptainSkillInfo {19165})
		{
			Vector2 result;
			if (this.{19176} == {19150}.CaptainSkillCategory.Learned)
			{
				result = new Vector2((float)(({19150}.c_item.Width + {19161}) * {19164}), (float)(({19150}.c_item.Height + {19162}) * {19163}));
				{19164}++;
				if ({19164} >= 3)
				{
					{19164} = 0;
					{19163}++;
				}
			}
			else
			{
				result = {19150}.c_item.WidthHeight() * {19165}.UiPosition;
				result.X += (float){19161} * {19165}.UiPosition.X;
				result.Y += (float){19162} * {19165}.UiPosition.Y;
			}
			return result;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0005055C File Offset: 0x0004E75C
		private void {19166}(ListItemViewControl {19167})
		{
			using (IEnumerator enumerator = Enum.GetValues(typeof(CaptainTitle)).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CaptainTitle item = (CaptainTitle)enumerator.Current;
					Form form = new Form(Vector2.Zero, new Rectangle(2190, 3335, 825, 77), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					bool flag = item.IsAvailable(Session.Account);
					bool flag2 = Session.Account.SelectedCaptainTitle == item;
					if (flag2)
					{
						form.AddChild(new Form(Vector2.Zero, new Rectangle(2190, 3257, 173, 77), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							AnimatedFocus = false
						});
					}
					Color color = flag2 ? Color.SoftLime : (flag ? (Color.White * 0.9f) : Color.Gray);
					form.AddChild(new Label(new Vector2(81f, 20f), Fonts.Philosopher_14, color, flag2 ? Local.CaptainSkillsInfoWindow_selected : (flag ? Local.CaptainSkillsInfoWindow_open : Local.CaptainSkillsInfoWindow_closed), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					string text;
					form.AddChild(new Label(new Vector2(81f, 43f), Fonts.Arial_12, color * 0.8f, item.ConditionString(out text), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					if (!string.IsNullOrEmpty(text))
					{
						form.ToolTipState = new ToolTipState("", text, Array.Empty<ToolTipCharacteristics>());
					}
					form.AddChild(new Image(new Marker(14f, 15f, 53f, 53f), AtlasObjs.Texture.Tex, AtlasObjs.GetCaptainTitleIcon(item), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						BasicColor = (flag2 ? Color.LightGreen : (flag ? (Color.SkyBlue * 1.2f) : Color.White))
					});
					if (flag)
					{
						form.EvClick += delegate(ClickUiEventArgs {19187})
						{
							Session.Account.SelectedCaptainTitle = item;
							this.RefreshCurrentDynamicTabPage();
							Global.Network.Send(new OnWorldActionMsg(Global.Player.uID, WorldRandomAction.ChangeVisibleTitle, string.Empty, null, (int)item, null));
						};
					}
					{19167}.AddItem(new UiControl[]
					{
						form
					});
				}
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000507A0 File Offset: 0x0004E9A0
		private void AppendHeaderDynamic(Func<string> {19168}, ListItemViewControl {19169})
		{
			Form form = new Form(new Marker(0f, 0f, ref {19150}.c_categoryHeader), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Form form2 = new Form(new Vector2(-46f, 0f), {19150}.c_categoryHeader, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Label label = new LiveLabel(form2.Pos.Center, Fonts.Philosopher_18, {19150}.colorHeadSky * 0.8f, {19168}, 100).Center();
			form.AddChild(new UiControl[]
			{
				form2,
				label,
				new Form(new Vector2(label.Pos.XY.X - (float)CommonAtlas.c_textCadreLeft.Width - 4f, label.Pos.Center.Y - (float)(CommonAtlas.c_textCadreLeft.Height / 2)), CommonAtlas.c_textCadreLeft, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					BasicColor = {19150}.colorHeadSky * 0.8f
				},
				new Form(new Vector2(label.Pos.XY.X + label.Pos.WH.X + 4f, label.Pos.Center.Y - (float)(CommonAtlas.c_textCadreRight.Height / 2)), CommonAtlas.c_textCadreRight, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					BasicColor = {19150}.colorHeadSky * 0.8f
				}
			});
			{19169}.AddItem(new UiControl[]
			{
				form
			});
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00050934 File Offset: 0x0004EB34
		private void {19170}(string {19171}, ListItemViewControl {19172})
		{
			Form form = new Form(new Marker(0f, 0f, ref {19150}.c_categoryHeader), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Form form2 = new Form(new Vector2(-46f, 0f), {19150}.c_categoryHeader, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Label label = new Label(form2.Pos.Center, Fonts.Philosopher_24, {19150}.colorHeadSky, {19171}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center();
			form.AddChild(new UiControl[]
			{
				form2,
				label,
				new Form(new Vector2(label.Pos.XY.X - (float)CommonAtlas.c_textCadreLeft.Width - 4f, label.Pos.Center.Y - (float)(CommonAtlas.c_textCadreLeft.Height / 2)), CommonAtlas.c_textCadreLeft, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					BasicColor = {19150}.colorHeadSky
				},
				new Form(new Vector2(label.Pos.XY.X + label.Pos.WH.X + 4f, label.Pos.Center.Y - (float)(CommonAtlas.c_textCadreRight.Height / 2)), CommonAtlas.c_textCadreRight, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					BasicColor = {19150}.colorHeadSky
				}
			});
			{19172}.AddItem(new UiControl[]
			{
				form
			});
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00050AA8 File Offset: 0x0004ECA8
		protected override void UserBackRender()
		{
			base.UserBackRender();
			if ({19150}.editor)
			{
				foreach (ValueTuple<Form, Form> valueTuple in ((IEnumerable<ValueTuple<Form, Form>>)this.{19175}))
				{
					Engine.GS.Line2D(CommonAtlas.whitePixel, valueTuple.Item1.Pos.Center, valueTuple.Item2.Pos.Center, new Color(81, 63, 37), 10);
				}
			}
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00050CCB File Offset: 0x0004EECB
		[NullableContext(1)]
		[CompilerGenerated]
		private bool {19173}(CaptainSkillInfo {19174})
		{
			return !{19174}.IsRemoved && {19174}.UiCategory == (int)this.{19176};
		}

		// Token: 0x040008FF RID: 2303
		private const int SkillColumnsCount = 3;

		// Token: 0x04000900 RID: 2304
		private static readonly Color colorHeadSky = new Color(182, 206, 215);

		// Token: 0x04000901 RID: 2305
		private static readonly Rectangle c_back = new Rectangle(2133, 3596, 384, 235);

		// Token: 0x04000902 RID: 2306
		private static readonly Rectangle c_researchMarker_blue = new Rectangle(0, 3324, 118, 132);

		// Token: 0x04000903 RID: 2307
		private static readonly Rectangle c_researchMarker_yellow = new Rectangle(119, 3324, 118, 132);

		// Token: 0x04000904 RID: 2308
		private static readonly Rectangle c_reserachMarker_red = new Rectangle(238, 3324, 118, 132);

		// Token: 0x04000905 RID: 2309
		private static readonly Rectangle c_defaultMarker_blue = new Rectangle(0, 3457, 118, 132);

		// Token: 0x04000906 RID: 2310
		private static readonly Rectangle c_defaultMarker_yellow = new Rectangle(119, 3457, 118, 132);

		// Token: 0x04000907 RID: 2311
		private static readonly Rectangle c_defaultMarker_red = new Rectangle(238, 3457, 118, 132);

		// Token: 0x04000908 RID: 2312
		private static readonly Rectangle c_categoryHeader = new Rectangle(566, 3728, 885, 59);

		// Token: 0x04000909 RID: 2313
		private static readonly Rectangle c_item = new Rectangle(19, 1245, 305, 124);

		// Token: 0x0400090A RID: 2314
		private static readonly Rectangle c_branch = new Rectangle(1262, 3929, 143, 36);

		// Token: 0x0400090B RID: 2315
		[TupleElementNames(new string[]
		{
			"name",
			"id"
		})]
		private static readonly ValueTuple<string, {19150}.CaptainSkillCategory>[] skillCategoryNames = new ValueTuple<string, {19150}.CaptainSkillCategory>[]
		{
			new ValueTuple<string, {19150}.CaptainSkillCategory>(Local.CaptainSkillsInfoWindow_12, {19150}.CaptainSkillCategory.ExploringTheWorld),
			new ValueTuple<string, {19150}.CaptainSkillCategory>(Local.StringConstants_34, {19150}.CaptainSkillCategory.Battles),
			new ValueTuple<string, {19150}.CaptainSkillCategory>(Local.CaptainSkillsInfoWindow_13, {19150}.CaptainSkillCategory.Craft),
			new ValueTuple<string, {19150}.CaptainSkillCategory>(Local.CaptainSkillsInfoWindow_244, {19150}.CaptainSkillCategory.Legend),
			new ValueTuple<string, {19150}.CaptainSkillCategory>(Local.captain_skills_learned, {19150}.CaptainSkillCategory.Learned)
		};

		// Token: 0x0400090C RID: 2316
		private Tlist<ValueTuple<Form, Form>> {19175} = new Tlist<ValueTuple<Form, Form>>();

		// Token: 0x0400090D RID: 2317
		private {19150}.CaptainSkillCategory {19176} = {19150}.CaptainSkillCategory.ExploringTheWorld;

		// Token: 0x0400090E RID: 2318
		private Form {19177};

		// Token: 0x020001BC RID: 444
		private enum CaptainSkillCategory
		{
			// Token: 0x04000910 RID: 2320
			Craft,
			// Token: 0x04000911 RID: 2321
			ExploringTheWorld,
			// Token: 0x04000912 RID: 2322
			Battles,
			// Token: 0x04000913 RID: 2323
			Legend,
			// Token: 0x04000914 RID: 2324
			Learned
		}
	}
}
