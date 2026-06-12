using System;
using System.Collections.Generic;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001B5 RID: 437
	internal sealed class {19146} : {17625}
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x000070D7 File Offset: 0x000052D7
		private static bool editor
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0004E3D4 File Offset: 0x0004C5D4
		public {19146}() : base(900f, {19146}.c_back, {17604}.InGameWindow, {17625}.c_icStreeringWheel, Array.Empty<{17625}.DynamicTittle>())
		{
			Global.Game.SceneGame.IncreaseMouse();
			base.EvRemoveFromContainer += delegate()
			{
				Global.Game.SceneGame.DecreaseMouse();
			};
			this.{19147}();
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0004E43C File Offset: 0x0004C63C
		private void {19147}()
		{
			{19146}.<>c__DisplayClass10_0 CS$<>8__locals1 = new {19146}.<>c__DisplayClass10_0();
			Vector2 vector = base.Pos.XY + new Vector2(base.Pos.WH.X * 0.5f - 23.5f, base.Pos.WH.Y * 0.8f);
			CS$<>8__locals1.allForms = new Tlist<Form>();
			using (IEnumerator<CaptainSkillInfo> enumerator = ((IEnumerable<CaptainSkillInfo>)Gameplay.CaptainSkillsInfo).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					{19146}.<>c__DisplayClass10_1 CS$<>8__locals2 = new {19146}.<>c__DisplayClass10_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					CS$<>8__locals2.skill = enumerator.Current;
					{19146}.<>c__DisplayClass10_2 CS$<>8__locals3 = new {19146}.<>c__DisplayClass10_2();
					CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals2;
					if (!CS$<>8__locals3.CS$<>8__locals2.skill.IsRemoved)
					{
						{19146}.CategorySettings categorySettings = {19146}.settings[CS$<>8__locals3.CS$<>8__locals2.skill.UiCategory];
						CS$<>8__locals3.isResearched = Session.Account.CaptainSkills.IsImproved(CS$<>8__locals3.CS$<>8__locals2.skill);
						Texture2D iconTexture = CS$<>8__locals3.CS$<>8__locals2.skill.IconTexture;
						float num = 1.3f;
						Vector2 uiPosition = CS$<>8__locals3.CS$<>8__locals2.skill.UiPosition;
						float x = MathF.Atan2(uiPosition.Y + categorySettings.PositionAdditional, uiPosition.X + categorySettings.PositionAdditional) * num + (float)categorySettings.Angle * 3.1415927f / 180f;
						Vector2 vector2 = CS$<>8__locals3.CS$<>8__locals2.skill.UiPosition;
						float num2 = vector2.Length() * categorySettings.LengthMultiplier;
						CS$<>8__locals3.form = new Form(new Marker(vector.X - categorySettings.Position.X - MathF.Round(MathF.Cos(x) * num2, 2), vector.Y - categorySettings.Position.Y - MathF.Round(MathF.Sin(x) * num2 * (float)categorySettings.YMultiplier, 2), 47f, 47f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							AnimatedFocus = false,
							AllowDragDrop = {19146}.editor
						};
						CS$<>8__locals3.form.AddChild(new Image(new Marker(ref CS$<>8__locals3.form.Pos.XY, 47f, 47f), CS$<>8__locals3.CS$<>8__locals2.skill.IconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
						CS$<>8__locals3.plocal = default(Rectangle);
						CS$<>8__locals3.researchedMarker = null;
						if (CS$<>8__locals3.isResearched)
						{
							CS$<>8__locals3.plocal = ((CS$<>8__locals3.CS$<>8__locals2.skill.UiCategory == 0) ? {19146}.c_researchMarker_blue : ((CS$<>8__locals3.CS$<>8__locals2.skill.UiCategory == 1) ? {19146}.c_reserachMarker_red : {19146}.c_researchMarker_yellow));
						}
						if (CS$<>8__locals3.plocal.Width != 0)
						{
							{19146}.<>c__DisplayClass10_2 CS$<>8__locals4 = CS$<>8__locals3;
							Form form = CS$<>8__locals3.form;
							Rectangle plocal = CS$<>8__locals3.plocal;
							vector2 = CS$<>8__locals3.form.Pos.XY + {19146}.researchAdditional;
							CS$<>8__locals4.researchedMarker = form.AddChild(plocal, new Marker(ref vector2, ref CS$<>8__locals3.plocal));
						}
						CS$<>8__locals3.form.ToolTip = new ToolTip((UiControl {19148}) => {19150}.GetToolTipContent(CS$<>8__locals3.CS$<>8__locals2.skill, true));
						if (Session.Account.CaptainSkills.AllowImprove(CS$<>8__locals3.CS$<>8__locals2.skill, Session.Account, Session.Game.IsInPortOrIsleWithStorage) == PlayerCaptainSkillsStorage.Availability.NoDependSkills)
						{
							CS$<>8__locals3.form.Opacity = 0.5f;
						}
						base.AddChild(CS$<>8__locals3.form);
						CS$<>8__locals3.form.EvClick += delegate(ClickUiEventArgs {19149})
						{
							if ({19146}.editor)
							{
								return;
							}
							Decorator game;
							if (Session.Account.CaptainSkills.IsImproved(CS$<>8__locals3.CS$<>8__locals2.skill))
							{
								PlayerCaptainSkillsStorage captainSkills = Session.Account.CaptainSkills;
								game = Session.Game;
								string text;
								if (captainSkills.CanBeResetted(game, CS$<>8__locals3.CS$<>8__locals2.skill, Session.CountOfCapers, out text))
								{
									int {11354} = Gameplay.CostResetCaptainSkillGold(Session.Account);
									string skill_cantberesetted_Q = Local.skill_cantberesetted_Q;
									RTI {17353} = {11354};
									Action {17354};
									if (({17354} = CS$<>8__locals3.<>9__5) == null)
									{
										{17354} = (CS$<>8__locals3.<>9__5 = delegate()
										{
											PlayerCaptainSkillsStorage captainSkills3 = Session.Account.CaptainSkills;
											CaptainSkillInfo skill2 = CS$<>8__locals3.CS$<>8__locals2.skill;
											int countOfCapers = Session.CountOfCapers;
											Decorator game2 = Session.Game;
											captainSkills3.ResetSkill(skill2, countOfCapers, game2);
											Global.Game.SoundSystem.PlaySound(GameStaticSoundName.AchievOrLevelUp, 0.03f, 1f);
											CS$<>8__locals3.isResearched = false;
											if (!Global.Player.IsPortEntry)
											{
												Global.Network.Send(new OnChangeCaptainSkillsMsg(CS$<>8__locals3.CS$<>8__locals2.skill.ID, OnChangeCaptainSkillsMsg.SAction.RemoveSkill));
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
											base.<BuildRadialMenu>g__updateResearchMarker|1();
											new UiBrightnessAnimation(CS$<>8__locals3.form, 1f, 4f, 100f);
											new UiBrightnessAnimation(CS$<>8__locals3.form, 1f, 700f);
										});
									}
									{17312}.AskPrice(skill_cantberesetted_Q, {17353}, {17354}, true);
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
							if (({17372} = CS$<>8__locals3.<>9__3) == null)
							{
								{17372} = (CS$<>8__locals3.<>9__3 = delegate()
								{
									Global.Game.SoundSystem.PlaySound(GameStaticSoundName.AchievOrLevelUp, 0.03f, 1f);
									Session.Account.CaptainSkills.Improve(CS$<>8__locals3.CS$<>8__locals2.skill, Session.Account);
									EducationHelper.MakeFlag(EducationOnboarding.UpgradeCaptainSkill, true);
									if (CS$<>8__locals3.CS$<>8__locals2.skill.UiCategory == 1)
									{
										EducationHelper.MakeFlag(EducationOnboarding.ResearchSkillSailingCategory, true);
									}
									CS$<>8__locals3.isResearched = true;
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
									base.<BuildRadialMenu>g__updateResearchMarker|1();
									foreach (Form form2 in ((IEnumerable<Form>)CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.allForms))
									{
										if (((CaptainSkillInfo)form2.Tag).DependsTo == CS$<>8__locals3.CS$<>8__locals2.skill)
										{
											form2.Opacity = 1f;
										}
									}
									new UiBrightnessAnimation(CS$<>8__locals3.form, 1f, 4f, 100f);
									new UiBrightnessAnimation(CS$<>8__locals3.form, 1f, 700f);
								});
							}
							new {17312}({17371}, {17372}, delegate()
							{
							});
						};
						CS$<>8__locals3.form.Tag = CS$<>8__locals3.CS$<>8__locals2.skill;
						CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.allForms.Add(CS$<>8__locals3.form);
					}
				}
			}
		}

		// Token: 0x040008E7 RID: 2279
		private static readonly Rectangle c_back = new Rectangle(2133, 3596, 384, 235);

		// Token: 0x040008E8 RID: 2280
		private static readonly Rectangle c_researchMarker_blue = new Rectangle(0, 3324, 118, 132);

		// Token: 0x040008E9 RID: 2281
		private static readonly Rectangle c_researchMarker_yellow = new Rectangle(119, 3324, 118, 132);

		// Token: 0x040008EA RID: 2282
		private static readonly Rectangle c_reserachMarker_red = new Rectangle(238, 3324, 118, 132);

		// Token: 0x040008EB RID: 2283
		private static readonly {19146}.CategorySettings[] settings = new {19146}.CategorySettings[]
		{
			new {19146}.CategorySettings
			{
				Angle = 0,
				YMultiplier = 1,
				Position = new Vector2(250f, 0f),
				PositionAdditional = 1f,
				LengthMultiplier = 70f
			},
			new {19146}.CategorySettings
			{
				Angle = 80,
				YMultiplier = 1,
				Position = new Vector2(130f, 270f),
				PositionAdditional = 1f,
				LengthMultiplier = 70f
			},
			new {19146}.CategorySettings
			{
				Angle = 180,
				YMultiplier = -1,
				Position = new Vector2(-200f, 0f),
				PositionAdditional = 1f,
				LengthMultiplier = 70f
			},
			new {19146}.CategorySettings
			{
				Angle = -25,
				YMultiplier = 1,
				Position = new Vector2(-100f, 0f),
				PositionAdditional = 1f,
				LengthMultiplier = 100f
			}
		};

		// Token: 0x040008EC RID: 2284
		private static readonly Vector2 researchAdditional = new Vector2(-36f, -8f);

		// Token: 0x020001B6 RID: 438
		private struct CategorySettings
		{
			// Token: 0x040008ED RID: 2285
			public Vector2 Position;

			// Token: 0x040008EE RID: 2286
			public int Angle;

			// Token: 0x040008EF RID: 2287
			public int YMultiplier;

			// Token: 0x040008F0 RID: 2288
			public float PositionAdditional;

			// Token: 0x040008F1 RID: 2289
			public float LengthMultiplier;
		}
	}
}
