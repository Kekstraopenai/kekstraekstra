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
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020000D4 RID: 212
	internal class {17745} : {17068}
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x00028F38 File Offset: 0x00027138
		public static StackForm GetPriceForm(Vector2 {17748}, string {17749})
		{
			StackForm stackForm = new StackForm({17748}, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				new Image(new Marker(0f, 0f, 24f, 24f), CommonAtlas.Texture.Tex, CommonAtlas.goldIconSingle32, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			stackForm.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_14, Color.White, {17749}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			stackForm.Opacity = 0.5f;
			return stackForm;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00028FBF File Offset: 0x000271BF
		public static IEnumerable<{18027}.ItemPlaceholderBind> GetPowerupItemSlots(Action {17750} = null)
		{
			{17745}.<GetPowerupItemSlots>d__9 <GetPowerupItemSlots>d__ = new {17745}.<GetPowerupItemSlots>d__9(-2);
			<GetPowerupItemSlots>d__.<>3__updateCallback = {17750};
			return <GetPowerupItemSlots>d__;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00028FCF File Offset: 0x000271CF
		private static bool isWinterStyle
		{
			get
			{
				return CalendarEvents.IsNewYearExtended;
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00028FD6 File Offset: 0x000271D6
		public {17745}() : this(new Vector2(410f, (float)(Engine.GS.UIArea.Height + 80)), false)
		{
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00028FFC File Offset: 0x000271FC
		public {17745}(Vector2 {17751}, bool {17752})
		{
			Vector2 vector = new Vector2((float)(Engine.GS.UIArea.Width / 2 - {17745}.c_main_n.Width / 2), {17751}.Y + (float)(-(float){17745}.c_main_n.Height - 170 + 25));
			base..ctor(new Marker(ref vector, ref {17745}.c_main_n), {17745}.c_main_n, {17068}.BlockingWay.BackgroundClosingTransparent, Global.Game.GetCurrentSceneName == GameSceneName.Port);
			base.RemoveAnimations();
			float {14250} = 1E-05f;
			float {14251} = 1f;
			Marker marker = base.Pos;
			new UiMarkerAndOpacityAnimation(this, {14250}, {14251}, marker.Offset(0f, 15f), base.Pos, 180f, UiAmimationCurve.Linear);
			this.{17765} = {17752};
			Marker marker2;
			if ({17752})
			{
				marker = new Marker(294f, 23f, 359f, 490f);
				marker2 = base.Pos;
				base.AddChild(new Form(marker.Offset(marker2.XY), new Rectangle(1999, 3249, 180, 187), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false,
					RenderToDepthMap = false
				});
			}
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UIDeploy, 0.03f, 1f);
			Global.Player.UsedShipPlayer.PrivateResourcesOfHold.OrderByQuantity();
			if (!{17752})
			{
				Session.Account.NearPortStorage.OrderByQuantity();
			}
			{17745}.CurrentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{17745}.CurrentInstance = null;
				Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UIClotting, 0.03f, 1f);
				{17516} currentInstance = {17516}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.RemoveFromContainer();
			};
			marker2 = new Marker(626f, 28f, 21f, 480f);
			marker = base.Pos;
			ScrollBarControl scrollBarControl = new ScrollBarControl(marker2.Offset(marker.XY), Rectangle.Empty, Rectangle.Empty, new Rectangle(2843, 1705, 20, 37), CommonAtlas.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			marker = new Marker(26f, 26f, 600f, 486f);
			marker2 = base.Pos;
			ListItemViewControl listItemViewControl = new ListItemViewControl(marker.Offset(marker2.XY), scrollBarControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(new UiControl[]
			{
				listItemViewControl,
				scrollBarControl
			});
			int num = 4;
			if ({17752})
			{
				if (Global.Player.UsedShipPlayer.Capacity > 13000f)
				{
					num = 5;
				}
				else if (Global.Player.UsedShipPlayer.Capacity > 24000f)
				{
					num = 6;
				}
				else if (Global.Player.UsedShipPlayer.Capacity > 32000f)
				{
					num = 7;
				}
			}
			else
			{
				int storageLevel = Session.Account.ResourcesInPorts.GetStorageLevel(Global.Player.NearPort.PortID);
				if (storageLevel >= 4)
				{
					num = 6;
				}
				else if (storageLevel > 0)
				{
					num = 5;
				}
				num = Math.Max(num, 1 + (int)Math.Ceiling((double)((float)Session.Account.ResourcesInPorts.WriteAccess(Global.Player.NearPort.PortID).Resources.NamesCount / 5f)));
			}
			if ({17745}.isWinterStyle)
			{
				Rectangle winterWindowSnowLong = CommonAtlas.WinterWindowSnowLong;
				float scaleFactor = 0.68f;
				float {11535} = 0f;
				float {11536} = 0f;
				vector = winterWindowSnowLong.WidthHeight() * scaleFactor;
				base.AddChildPos(new Image(new Marker({11535}, {11536}, ref vector), CommonAtlas.Texture.Tex, winterWindowSnowLong, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					RenderToDepthMap = false
				}, PositionAlignment.Center, PositionAlignment.LeftUp, 0f, 6f, false);
			}
			int {17770} = (Global.Game.GetCurrentSceneName == GameSceneName.Game) ? Math.Max(2, (int)Math.Ceiling((double)((float)(Global.Player.UsedShipPlayer.BallsOfHold.NamesCount + Global.Player.UsedShipPlayer.PowderKegsOfHold.NamesCount) / 5f))) : Math.Max(3, (int)Math.Ceiling((double)((float)(Session.Account.CBallsAtStorage.NamesCount + Session.Account.PowderKegsAtStorage.NamesCount) / 5f)));
			this.{17764} = new {18027}(Vector2.Zero, new {17745}.Provider({17770}, num), CommonAtlas.transpPixel);
			listItemViewControl.AddItem(new UiControl[]
			{
				this.{17764}
			});
			UiControl[] array = new UiControl[2];
			array[0] = new LiveLabel(base.Pos.XY + new Vector2(32f, 518f), Fonts.Philosopher_16, Color.SkyBlue * 0.5f, null, delegate(object {17926})
			{
				float num5 = MathF.Round((Global.Player.CapacitySpeedFactor * Global.Player.UsedShip.Speed - Global.Player.UsedShip.Speed) * PlayerShipInfo.Temp_displaySpeedRefactoring, 1);
				return Local.HoldInterfaceCommon_2((num5 < 0f) ? num5.ToString() : ("+" + num5.ToString()));
			}, 100);
			array[1] = new LiveLabel(base.Pos.XY + new Vector2(32f, 542f), Fonts.Philosopher_14, Color.LightYellow * 0.5f, null, delegate(object {17927})
			{
				if (Session.Account.HoldProtectionSubscriptionSec > 0f)
				{
					return Local.PortRealShopPage_135 + " " + StringHelper.TimeDHM((double)Session.Account.HoldProtectionSubscriptionSec, false);
				}
				return Local.lbe_mydeath + Math.Round((double)(100f * Session.Account.WorldFlag.DropResAmount(Global.Player))).ToString() + "%";
			}, 100);
			base.AddChild(array);
			if ({17752})
			{
				PlayerShipDynamicInfo ship = Global.Player.UsedShipPlayer;
				Form form = new Form(new Marker(0f, 0f, 300f, 40f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Vector2 vector2 = new Vector2(150f, 40f);
				Rectangle worldFlagPrerender = CommonAtlas.GetWorldFlagPrerender(Session.Account.WorldFlag, Session.Game.MapMyFraction.GetValueOrDefault(FractionID.None));
				vector = new Vector2(0f, 0f);
				Form form2 = new Form(new Marker(ref vector, vector2.X, vector2.Y), worldFlagPrerender, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				ToolTipState {12676} = new ToolTipState(Session.Account.WorldFlag.ToStringLocalFull(), CommonEnums.GetOpenWorldFlagText(Session.Account.WorldFlag), Array.Empty<ToolTipCharacteristics>());
				form2.ToolTip = new ToolTip({12676});
				form2.BasicColor *= 0.5f;
				vector = new Vector2(0f, 0f);
				Form {12956} = new Form(new Marker(ref vector, vector2.X, vector2.Y), CommonAtlas.shadow, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form2.AddChildPos({12956}, PositionAlignment.RightDown, PositionAlignment.RightDown, -60f, 0f, false);
				form.AddChildPos(form2, PositionAlignment.LeftUp, PositionAlignment.Center, 0f);
				UiControl uiControl = form;
				vector = default(Vector2);
				uiControl.AddChildPos(new Label(vector, Fonts.Philosopher_18, Color.LightGray, ship.ShipNameVisible + " (" + StringHelper.ToRoman(ship.CraftFrom.Rank) + ")", PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.RightDown, PositionAlignment.Center, 8f, 0f, false);
				UiControl uiControl2 = form;
				vector = default(Vector2);
				uiControl2.AddChildPos(new LiveLabel(vector, Fonts.Arial_10, Color.LightGray * 0.5f, () => Local.HoldInterfaceCommon_25 + ": " + StringHelper.TimeMMMSS((double)Session.Account.TimeAtSeaSec), 100), PositionAlignment.RightDown, PositionAlignment.RightDown, 9f, -8f, false);
				StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(326f, 35f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm.AddItem(new UiControl[]
				{
					form
				});
				stackForm.AddSpace(3f);
				stackForm.BorderThickness = 0f;
				CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
				int num2 = 290;
				float opacity = 0.9f;
				float opacity2 = 0.9f;
				int num3 = 16;
				stackForm.AddSpace((float)num3);
				stackForm.AddItem(new UiControl[]
				{
					new {22065}(Vector2.Zero, (float)num2, philosopher_, Local.armor, () => new ValueTuple<string, bool>(Math.Round((double)ship.Armor, 1).ToString(), false), null)
					{
						Opacity = opacity
					}
				});
				StackForm stackForm2 = stackForm;
				UiControl[] array2 = new UiControl[1];
				array2[0] = new {22065}(Vector2.Zero, (float)num2, philosopher_, Local.speed.TrimEnd(' ') + "*", delegate()
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler2.AppendFormatted<float>(Global.Player.PrevDisplayMaxSpeed * PlayerShipInfo.Temp_displaySpeedRefactoring, "F1");
					defaultInterpolatedStringHandler2.AppendLiteral(" - ");
					defaultInterpolatedStringHandler2.AppendFormatted<float>(Global.Player.PrevDisplayMaxSpeedIncMarchMode * PlayerShipInfo.Temp_displaySpeedRefactoring, "F1");
					return new ValueTuple<string, bool>(defaultInterpolatedStringHandler2.ToStringAndClear(), false);
				}, null)
				{
					Opacity = opacity2
				}.ExToolTip(new ToolTip(new ToolTipState("", Local.holdInterfaceSpeedDesc, Array.Empty<ToolTipCharacteristics>())));
				stackForm2.AddItem(array2);
				stackForm.AddItem(new UiControl[]
				{
					new {22065}(Vector2.Zero, (float)num2, philosopher_, Local.mobility, () => new ValueTuple<string, bool>(Math.Round((double)(ship.Mobility + ((Global.Player.FirstController.LinearStateCode <= 1) ? ship.MobilityBonusAtLowSpeeds : 0f))).ToString() + " / " + Math.Round((double)ship.MobilityWithoutTempEffects).ToString(), false), null)
					{
						Opacity = opacity
					}
				});
				StackForm stackForm3 = stackForm;
				UiControl[] array3 = new UiControl[1];
				int num4 = 0;
				Vector2 zero = Vector2.Zero;
				float {22073} = (float)num2;
				CustomSpriteFont {22074} = philosopher_;
				string weapons_d = Local.weapons_d;
				string str;
				if (Global.Player.UsedShip.Cannons.BrokenItems.Size + 1 <= 0)
				{
					str = "";
				}
				else
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
					defaultInterpolatedStringHandler.AppendLiteral(" (");
					defaultInterpolatedStringHandler.AppendFormatted(Local.damaged);
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(Global.Player.UsedShip.Cannons.BrokenItems.Size);
					defaultInterpolatedStringHandler.AppendLiteral(")");
					str = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				array3[num4] = new {22065}(zero, {22073}, {22074}, weapons_d + str, delegate()
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler2.AppendFormatted<int>(Global.Player.UsedShip.Cannons.Count);
					defaultInterpolatedStringHandler2.AppendLiteral(" / ");
					defaultInterpolatedStringHandler2.AppendFormatted<int>(Global.Player.CraftFrom.StaticInfo.Ports.Length);
					return new ValueTuple<string, bool>(defaultInterpolatedStringHandler2.ToStringAndClear(), false);
				}, null)
				{
					Opacity = opacity2
				};
				stackForm3.AddItem(array3);
				Vector2 minMaxDist = Global.Player.UsedShip.Cannons.MinMaxDistance + new Vector2(Global.Player.UsedShip.BallDistanceBonusValue);
				stackForm.AddItem(new UiControl[]
				{
					new {22065}(Vector2.Zero, (float)num2, philosopher_, Local.StringConstants_111, () => new ValueTuple<string, bool>(((int)minMaxDist.X).ToString() + "-" + ((int)minMaxDist.Y).ToString(), false), null)
					{
						Opacity = opacity2
					}
				});
				Form form3 = new Form(new Marker(0f, 0f, 300f, 40f), new Rectangle(1246, 4012, 491, 52), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form3.BasicColor *= 0.9f;
				stackForm.AddItem(new UiControl[]
				{
					form3
				});
				StackForm stackForm4 = stackForm;
				UiControl[] array4 = new UiControl[1];
				array4[0] = new {22065}(Vector2.Zero, (float)num2, philosopher_, Local.crew, delegate()
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler2.AppendFormatted<int>(Global.Player.UsedShip.Crew.Count);
					defaultInterpolatedStringHandler2.AppendLiteral(" / ");
					defaultInterpolatedStringHandler2.AppendFormatted<int>(Global.Player.UsedShipPlayer.CrewPlaces);
					return new ValueTuple<string, bool>(defaultInterpolatedStringHandler2.ToStringAndClear(), false);
				}, null)
				{
					Opacity = opacity2
				};
				stackForm4.AddItem(array4);
				StackForm stackForm5 = stackForm;
				UiControl[] array5 = new UiControl[1];
				array5[0] = new {22065}(Vector2.Zero, (float)num2, philosopher_, Local.unit_sailor_1_name, delegate()
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler2.AppendFormatted<int>(Global.Player.UsedShip.Crew.CountOfSailors);
					defaultInterpolatedStringHandler2.AppendLiteral(" / ");
					defaultInterpolatedStringHandler2.AppendFormatted<int>(Global.Player.UsedShip.NeedSailors);
					return new ValueTuple<string, bool>(defaultInterpolatedStringHandler2.ToStringAndClear(), Global.Player.UsedShip.Crew.CountOfSailors < Global.Player.UsedShip.NeedSailors);
				}, null)
				{
					Opacity = opacity2
				};
				stackForm5.AddItem(array5);
				stackForm.AddItem(new UiControl[]
				{
					new {22065}(Vector2.Zero, (float)num2, philosopher_, Local.weapons_speed_bycrew, delegate()
					{
						float num5 = MathF.Round(100f * Global.Player.UsedShip.Crew.Effectivity(ship));
						return new ValueTuple<string, bool>(num5.ToString() + "%", num5 < 1f);
					}, null)
					{
						Opacity = opacity
					}
				});
				FoodConsumption.Mode foodMode = Session.Account.FoodAtShip.HasFoodConsumption(Global.Player);
				if (foodMode != FoodConsumption.Mode.None)
				{
					Color color = new Color(167, 206, 125);
					float bonusValue = Session.Account.FoodAtShip.BonusAmount(Global.Player.UsedShip);
					if (foodMode != FoodConsumption.Mode.None)
					{
						{22065} {22065} = new {22065}(Vector2.Zero, (float)num2, philosopher_, Local.level_of_hunger_positive, () => new ValueTuple<string, bool>(MathF.Round((1f - Session.Account.FoodAtShip.CurrentHungerLevel) * 100f).ToString() + "%", Session.Account.FoodAtShip.CurrentHungerLevel > 0.5f && foodMode == FoodConsumption.Mode.Full), (bonusValue > 0f) ? new Color?(color) : null)
						{
							Opacity = opacity
						};
						if (bonusValue > 0f)
						{
							ToolTipState toolTipState = new ToolTipState(Local.food_bonus, Local.ingamett_foodConsumption, (from {17939} in WosbCrew.BonusesForSatiety
							select new ShipBonus({17939}.Type, MathF.Round({17939}.Value * bonusValue)) into {17928}
							where {17928}.Value > 0.01f
							select {17928} into {17929}
							select new ToolTipCharacteristics({17929}.ToString(), CharacteristicsColor.Lime)).ToArray<ToolTipCharacteristics>());
							{22065}.BasicColor = color;
							{22065}.AddChildPos(new Form(new Marker(0f, 0f, 18f, 18f), new Rectangle(539, 327, 32, 32), PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, {22065}.PosWidth + 3f);
							Form form4 = new Form({22065}.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							{
								AnimatedFocus = false
							};
							form4.AddChild({22065});
							form4.PosWidth += 50f;
							form4.ToolTipState = toolTipState;
							stackForm.AddItem(new UiControl[]
							{
								form4
							});
						}
						else
						{
							stackForm.AddItem(new UiControl[]
							{
								{22065}
							});
						}
					}
					stackForm.AddSpace(7f);
					vector = default(Vector2);
					StackForm stackForm6 = new StackForm(vector, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					ProgressSelectBar select = new ProgressSelectBar(Vector2.Zero, {17745}.cSelectBack, {17745}.cSelectBack, {17745}.cPointer, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExToolTip(new ToolTip(new ToolTipState("", Local.auto_food_consumtion_tt, Array.Empty<ToolTipCharacteristics>())));
					select.ExpansionSetVals(0f, 1f);
					Form form5 = new Form(select.Pos, AtlasPortGui.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						BasicColor = Color.Transparent,
						AnimatedFocus = false
					};
					form5.AddChild(select);
					Label selectLabel = new Label(Vector2.Zero, Fonts.Philosopher_12, Color.Wheat * 0.6f, Local.auto_food_consumtion + " " + Local.auto_food_consumtion_false, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					stackForm6.AddItem(new UiControl[]
					{
						selectLabel,
						form5
					});
					bool shouldUpdateFilter = false;
					select.EvChange += delegate(ProgressBarChangeEventArgs {17940})
					{
						float num5 = MathF.Round(select.Value, 1);
						if (num5 < 0.15f)
						{
							num5 = 0.15f;
						}
						if (num5 > 0.9f)
						{
							num5 = 1f;
						}
						if (select.Value != num5)
						{
							select.Value = num5;
							return;
						}
						Global.Settings.AutoFoodConsumtion = num5;
						Label selectLabel = selectLabel;
						string text;
						if (num5 <= 0.9f)
						{
							if (num5 <= 0f)
							{
								text = Local.auto_food_consumtion_false;
							}
							else
							{
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(0, 1);
								defaultInterpolatedStringHandler2.AppendFormatted<float>(num5 * 100f, "F0");
								text = Local.auto_food_consumtion_to(defaultInterpolatedStringHandler2.ToStringAndClear());
							}
						}
						else
						{
							text = Local.auto_food_consumtion_max;
						}
						selectLabel.Text = text;
						if (shouldUpdateFilter && num5 > 0f && Global.Settings.FoodConsumptionFilter.IsEmpty)
						{
							foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)Global.Player.ResourcesOfHoldAllowed().ResourceInfo))
							{
								if (gsilocalEnumerablePair.Info.FoodValue(Global.Player) > 0f)
								{
									Global.Settings.FoodConsumptionFilter[(int)gsilocalEnumerablePair.Info.ID] = 1;
								}
							}
						}
					};
					select.Value = Global.Settings.AutoFoodConsumtion;
					shouldUpdateFilter = true;
					CheckboxControl checkboxControl = new CheckboxControl(Vector2.Zero, CommonAtlas.vd_checkBox_26px_true, CommonAtlas.vd_checkBox_26px_false, Global.Settings.AutoFoodConsumtionOnlyOnNorth, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.auto_food_consumtion_only_north, Fonts.Philosopher_14, Color.Wheat * 0.7f).ExCheckEvent(delegate(CheckboxCheckedEventArgs {17930})
					{
						Global.Settings.AutoFoodConsumtionOnlyOnNorth = {17930}.NewValue;
					});
					checkboxControl.UpdateComplete += delegate(UiControl {17931})
					{
						{17931}.Opacity = ((Global.Settings.AutoFoodConsumtion > 0f) ? 1f : 0.4f);
					};
					stackForm6.AddItem(new UiControl[]
					{
						checkboxControl
					});
					stackForm6.UpdateComplete += delegate(UiControl {17932})
					{
						{17932}.IsVisible = (Global.Settings.AutoFoodConsumtion > 0f && Global.Settings.FoodConsumtionFilterActive);
					};
					stackForm.AddItem(new UiControl[]
					{
						stackForm6
					});
				}
				if (Global.Player.MapInfo.IsWorldmap)
				{
					base.AddChildPos(new Button(Vector2.Zero, CommonAtlas.basicButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						Opacity = 0.5f
					}.SetText(Global.Player.UsedShip.StaticInfo.IsBalloon ? Local.leave : Local.destroy_my_ship, Fonts.Philosopher_14, Color.White, false).ExClick(new Action<ClickUiEventArgs>(this.{17756})), PositionAlignment.RightDown, PositionAlignment.RightDown, 33f);
				}
				base.AddChild(stackForm);
				return;
			}
			if (Global.Player.NearPortType != PortEnteringType.Miniport)
			{
				Button button = new Button(base.Pos.XY + new Vector2(308f, 216f), {17745}.c_moveToRight, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Button button2 = new Button(base.Pos.XY + new Vector2(308f, 410f), {17745}.c_moveToRight, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Button button3 = new Button(base.Pos.XY + new Vector2(308f, 452f), {17745}.c_moveToLeft, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				this.{17764}.AddChild(new UiControl[]
				{
					button,
					button2,
					button3
				});
				button.EvClick += this.{17758};
				button2.EvClick += this.{17760};
				button3.EvClick += this.{17762};
				this.{17753}();
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0002A0AC File Offset: 0x000282AC
		private void {17753}()
		{
			if (Global.Player.NearPortType == PortEnteringType.Port)
			{
				StackForm stackForm = this.{17766};
				if (stackForm != null)
				{
					stackForm.RemoveFromContainer();
				}
				this.{17766} = new StackForm(base.Pos.XY + new Vector2(340f, 519f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				int storageLevel = Session.Account.ResourcesInPorts.GetStorageLevel(Global.Player.NearPort.PortID);
				if (storageLevel < WosbStorages.Port.Length - 1)
				{
					Button button = new Button(Vector2.Zero, {17745}.c_button, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					button.SetText((storageLevel == 0) ? Local.HoldInterfaceCommon_17 : Local.HoldInterfaceCommon_22, Fonts.Philosopher_14, Color.White * 0.7f, false);
					button.BrightnessBlinkingMode = Session.Account.IsEducationInProgress(EducationOnboarding.BuildStorage, false, false);
					if (storageLevel > 0)
					{
						button.AddChild(new PointedProgressBar(button.Pos.XY + new Vector2(29f, 30f), {17745}.c_progressTopButton_Yellow, {17745}.c_progressTopButton_Back, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							MaxValue = WosbStorages.Port.Length - 1,
							Value = storageLevel
						});
					}
					button.EvClick += delegate(ClickUiEventArgs {17934})
					{
						{22467}.OpenStorageBuildOrUpgrade();
					};
					this.{17766}.AddItem(new UiControl[]
					{
						button
					});
				}
				if (storageLevel > 0)
				{
					Button button2 = new Button(Vector2.Zero, new Rectangle(883, 2449, 43, 41), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					button2.ToolTipState = new ToolTipState(Local.HoldInterfaceCommon_14, "", Array.Empty<ToolTipCharacteristics>());
					button2.EvClick += delegate(ClickUiEventArgs {17935})
					{
						{22467}.OpenStorageRemoving();
					};
					this.{17766}.AddItem(new UiControl[]
					{
						button2
					});
				}
				Button button3 = new Button(Vector2.Zero, Session.Account.StorageRent.Any((ActiveStrageRent {17936}) => (int){17936}.PortID == Global.Player.NearPort.PortID) ? new Rectangle(795, 2449, 43, 41) : new Rectangle(839, 2449, 43, 41), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button3.ToolTipState = new ToolTipState(Local.storage_rent, "", Array.Empty<ToolTipCharacteristics>());
				button3.EvClick += delegate(ClickUiEventArgs {17937})
				{
					{22467}.OpenStorageRent();
				};
				this.{17766}.AddItem(new UiControl[]
				{
					button3
				});
				base.AddChild(this.{17766});
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0002A354 File Offset: 0x00028554
		private Tlist<ToolTipCharacteristics> GetCrewToolTop()
		{
			Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
			foreach (GSILocalEnumerablePair<UnitInfo> gsilocalEnumerablePair in from {17938} in Global.Player.UsedShip.Crew.Raw.UnitInfo
			orderby {17938}.Info.ID
			select {17938})
			{
				Tlist<ToolTipCharacteristics> tlist2 = tlist;
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(gsilocalEnumerablePair.Count.ToString() + "x " + gsilocalEnumerablePair.Info.Name + ((gsilocalEnumerablePair.Info.Type == UnitType.Sailor) ? (" / " + Global.Player.UsedShip.NeedSailors.ToString()) : string.Empty), CharacteristicsColor.Wheat);
				tlist2.Add(toolTipCharacteristics);
			}
			if (Global.Player.UsedShip.HealRandomUnitsWhenMending && !Global.Player.UsedShip.Crew.HurtedCrew.IsEmpty)
			{
				Tlist<ToolTipCharacteristics> tlist3 = tlist;
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(Global.Player.UsedShip.Crew.HurtedCrew.GetTotalItemsCount().ToString() + "x " + Local.hurted_crew, CharacteristicsColor.Orange);
				tlist3.Add(toolTipCharacteristics);
			}
			for (int i = 0; i < Global.Player.UsedShip.Crew.Special.Size; i++)
			{
				SpecialUnitInstance specialUnitInstance = Global.Player.UsedShip.Crew.Special.Array[i];
				Tlist<ToolTipCharacteristics> tlist4 = tlist;
				ToolTipCharacteristics toolTipCharacteristics = new ToolTipCharacteristics(specialUnitInstance.GetInfo.Name, CharacteristicsColor.WheatBold);
				tlist4.Add(toolTipCharacteristics);
			}
			return tlist;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00003100 File Offset: 0x00001300
		private void {17754}()
		{
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0002A514 File Offset: 0x00028714
		protected override void UserUpdate(ref FrameTime {17755})
		{
			if (Global.Settings.kb_OpenHold.IsClick)
			{
				base.BlockAndClose();
			}
			base.UserUpdate(ref {17755});
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0002A534 File Offset: 0x00028734
		protected override void UserBackRender()
		{
			if (this.{17767} = (Engine.GS.CurrentTexture != CommonAtlas.Texture.Tex))
			{
				Engine.GS.SetTexture(CommonAtlas.Texture);
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0002A574 File Offset: 0x00028774
		protected override void UserFrontRender()
		{
			if (this.{17767})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0002A588 File Offset: 0x00028788
		public void ExternalUpdate()
		{
			this.{17764}.ReloadTable();
			if (Global.Player.IsPortEntry)
			{
				this.{17753}();
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0002A694 File Offset: 0x00028894
		[CompilerGenerated]
		private void {17756}(ClickUiEventArgs {17757})
		{
			base.BlockAndClose();
			if (!Global.Player.CheckBattleTimerAndSpeed())
			{
				return;
			}
			new {17312}(Global.Player.UsedShip.StaticInfo.IsBalloon ? Local.ask_to_leave_balloon : Local.ask_to_leave_ship, delegate(int {17933})
			{
				if ({17933} == 0)
				{
					Global.Player.ResetSpeedAndRotation();
					if (Global.Player.UsedShip.StaticInfo.IsBalloon)
					{
						Global.Network.Send(new OnWorldActionMsg(Global.Player.uID, WorldRandomAction.ShipOverturned, string.Empty, null, 0, null));
						return;
					}
					{18945}.TryShowAcceptingMode(Local.destroy_my_ship + "...", Local.ExitScreen_2, 40000f, delegate
					{
						if (!Global.Player.CheckBattleTimerAndSpeed())
						{
							return;
						}
						Global.Network.Send(new OnWorldActionMsg(Global.Player.uID, WorldRandomAction.ShipOverturned, string.Empty, null, 0, null));
					}, true);
				}
			}, new string[]
			{
				Local.to_continue,
				Local.undo
			});
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0002A711 File Offset: 0x00028911
		[CompilerGenerated]
		private void {17758}(ClickUiEventArgs {17759})
		{
			GameplayHelper.MoveResources({21517}.ArsenalToStorage, true);
			this.{17764}.ReloadTable();
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0002A726 File Offset: 0x00028926
		[CompilerGenerated]
		private void {17760}(ClickUiEventArgs {17761})
		{
			GameplayHelper.MoveResources({21517}.ResToStorageManual, true);
			this.{17764}.ReloadTable();
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0002A73B File Offset: 0x0002893B
		[CompilerGenerated]
		private void {17762}(ClickUiEventArgs {17763})
		{
			GameplayHelper.MoveResources({21517}.ResToShip, true);
			this.{17764}.ReloadTable();
		}

		// Token: 0x04000495 RID: 1173
		private static readonly Rectangle c_button = new Rectangle(297, 173, 145, 41);

		// Token: 0x04000496 RID: 1174
		private static readonly Rectangle c_progressTopButton_Yellow = new Rectangle(297, 164, 14, 8);

		// Token: 0x04000497 RID: 1175
		private static readonly Rectangle c_progressTopButton_Back = new Rectangle(312, 164, 14, 8);

		// Token: 0x04000498 RID: 1176
		public static readonly Rectangle c_main_n = new Rectangle(1116, 2475, 677, 592);

		// Token: 0x04000499 RID: 1177
		public static readonly Rectangle c_moveToRight = new Rectangle(992, 210, 41, 42);

		// Token: 0x0400049A RID: 1178
		public static readonly Rectangle c_moveToLeft = new Rectangle(992, 253, 41, 42);

		// Token: 0x0400049B RID: 1179
		public static readonly Rectangle cSelectBack = new Rectangle(0, 128, 295, 26);

		// Token: 0x0400049C RID: 1180
		public static readonly Rectangle cPointer = new Rectangle(296, 128, 27, 27);

		// Token: 0x0400049D RID: 1181
		private static int[] emptyCellsCount = new int[]
		{
			0,
			4,
			3,
			2,
			1
		};

		// Token: 0x0400049E RID: 1182
		public static {17745} CurrentInstance;

		// Token: 0x0400049F RID: 1183
		private {18027} {17764};

		// Token: 0x040004A0 RID: 1184
		private bool {17765};

		// Token: 0x040004A1 RID: 1185
		private StackForm {17766};

		// Token: 0x040004A2 RID: 1186
		private bool {17767};

		// Token: 0x020000D5 RID: 213
		private class Provider : {18027}.IProvider
		{
			// Token: 0x06000520 RID: 1312 RVA: 0x0002A750 File Offset: 0x00028950
			public Provider(int {17770}, int {17771})
			{
				this.{17790} = 5 * {17770};
				this.{17791} = 5 * {17771};
			}

			// Token: 0x06000521 RID: 1313 RVA: 0x0002A76C File Offset: 0x0002896C
			private {18027}.ItemPlaceholderBind {17772}()
			{
				return new {18027}.ItemPlaceholderBind(new Form(new Marker(0f, 0f, 56f, 56f), CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					BasicColor = Color.Black * 0.35f,
					AnimatedFocus = false
				});
			}

			// Token: 0x06000522 RID: 1314 RVA: 0x0002A7C0 File Offset: 0x000289C0
			private static void AutoFoodConsumptionMiddleware(CustomUi {17773}, ResourceInfo {17774})
			{
				if ({17774}.FoodValue(Global.Player) > 0f && Session.Account.FoodAtShip.HasFoodConsumption(Global.Player) != FoodConsumption.Mode.None)
				{
					Form form = new Form(Vector2.Zero, new Rectangle(2346, 2306, 35, 35), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					};
					{17773}.AddChildPos(form, PositionAlignment.RightDown, PositionAlignment.RightDown, 1f);
					form.UpdateComplete += delegate(UiControl {17840})
					{
						{17840}.IsVisible = (Global.Settings.AutoFoodConsumtion > 0f && Global.Settings.FoodConsumptionFilter[(int){17774}.ID] > 0);
					};
				}
			}

			// Token: 0x06000523 RID: 1315 RVA: 0x0002A850 File Offset: 0x00028A50
			private static {18027}.ItemBind createSeparator(string {17775}, IEnumerable<GSILocalEnumerablePair<ResourceInfo>> {17776})
			{
				return new {18027}.HeaderBind(string.Format({17775}, {17776}.Sum((GSILocalEnumerablePair<ResourceInfo> {17792}) => (float){17792}.Count * {17792}.Info.Mass)), false, null)
				{
					OnCreating = delegate(Form {17841})
					{
						int num = {17776}.Sum((GSILocalEnumerablePair<ResourceInfo> {17793}) => {17793}.Info.MediumCost.Value * {17793}.Count);
						{17841}.AddChild({17745}.GetPriceForm(new Vector2({17841}.Pos.XY.X + 180f, {17841}.Pos.Center.Y + 13f), num.ToString()));
					}
				};
			}

			// Token: 0x06000524 RID: 1316 RVA: 0x0002A8C0 File Offset: 0x00028AC0
			private static {18027}.ItemBind createResItem(ResourceInfo {17777})
			{
				return new {18027}.ItemBind({17777}.IconTexture, {17777}.IconTexture.Bounds, () => Global.Player.UsedShipPlayer.PrivateResourcesOfHold.GetCount((int){17777}.ID), delegate(int {17794})
				{
				}, delegate(int {17842})
				{
					{17842} = Math.Min({17842}, Global.Player.UsedShipPlayer.PrivateResourcesOfHold.GetCount((int){17777}.ID));
					Global.Player.UsedShipPlayer.PrivateResourcesOfHold.AddOrRemove((int){17777}.ID, -{17842});
					Session.Account.NearPortStorage.AddOrRemove((int){17777}.ID, {17842});
				}, true, Session.Account.Quests.ContainsResourceIDInProgressTransfQuests((int){17777}.ID), false, delegate(CustomUi {17843})
				{
					IStorageAsset itemInfo = {17777};
					int {20434} = 0;
					string {20435};
					if (!Global.Player.IsPortEntry)
					{
						if ({17777} != null)
						{
							ResourceInfo itemInfo2 = {17777};
							if (itemInfo2.FoodValue(Global.Player) > 0f)
							{
								{20435} = Local.HoldInterfaceCommon_tt2;
								goto IL_48;
							}
						}
						{20435} = Local.HoldInterfaceCommon_tt1;
					}
					else
					{
						{20435} = "";
					}
					IL_48:
					{20431}.Set({17843}, itemInfo, {20434}, {20435});
					{17745}.Provider.AutoFoodConsumptionMiddleware({17843}, {17777});
				})
				{
					CustomClickHandler = ({17745}.CurrentInstance.{17765} ? delegate()
					{
						{17516}.TryCreate({17777});
					} : null),
					allowMove = ({17745}.CurrentInstance.{17765} || Global.Player.NearPortType != PortEnteringType.Miniport)
				};
			}

			// Token: 0x06000525 RID: 1317 RVA: 0x0002A99F File Offset: 0x00028B9F
			IEnumerable<{18027}.ItemBind> {18027}.IProvider.EnumerateItemsAtShip()
			{
				{17745}.Provider.<World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtShip>d__7 <World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtShip>d__ = new {17745}.Provider.<World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtShip>d__7(-2);
				<World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtShip>d__.<>4__this = this;
				return <World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtShip>d__;
			}

			// Token: 0x06000526 RID: 1318 RVA: 0x0002A9B0 File Offset: 0x00028BB0
			private static {18027}.ItemBind CreateResItemStored(ResourceInfo {17778})
			{
				return new {18027}.ItemBind({17778}.IconTexture, {17778}.IconTexture.Bounds, () => Session.Account.NearPortStorage.GetCount((int){17778}.ID), delegate(int {17858})
				{
					Global.Player.UsedShipPlayer.PrivateResourcesOfHold.AddOrRemove((int){17778}.ID, {17858});
					Session.Account.NearPortStorage.AddOrRemove((int){17778}.ID, -{17858});
				}, delegate(int {17825})
				{
				}, false, false, false, delegate(CustomUi {17859})
				{
					if ({17778}.ID == 24 && Global.Player.IsPortEntry)
					{
						{17859}.EvRightButtonClick += delegate(ClickUiEventArgs {17826})
						{
							{21338}.OpenCraft(WosbCrafting.Workshop.First((WosbCrafting.Recepie {17827}) => {17827}.OutputIsGold));
						};
						{20431}.Set({17859}, {17778}, 0, Local.click_to_open_chest);
						return;
					}
					{20431}.Set({17859}, {17778}, 0, null);
				})
				{
					simgleItemWeight = (int){17778}.Mass
				};
			}

			// Token: 0x06000527 RID: 1319 RVA: 0x0002AA42 File Offset: 0x00028C42
			IEnumerable<{18027}.ItemBind> {18027}.IProvider.EnumerateItemsAtRight()
			{
				{17745}.Provider.<World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtRight>d__9 <World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtRight>d__ = new {17745}.Provider.<World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtRight>d__9(-2);
				<World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtRight>d__.<>4__this = this;
				return <World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtRight>d__;
			}

			// Token: 0x06000528 RID: 1320 RVA: 0x0002AA54 File Offset: 0x00028C54
			private static int GetEmptyCellsCount(int {17779}, GSI {17780}, GSI {17781}, bool {17782})
			{
				int num = {17780}.ResourceInfo.Count((GSILocalEnumerablePair<ResourceInfo> {17838}) => {17838}.Info.IsTradingItem == {17782});
				int num2 = {17781}.ResourceInfo.Count((GSILocalEnumerablePair<ResourceInfo> {17839}) => {17839}.Info.IsTradingItem == {17782});
				int num3 = {17782} ? (num + num2 + 5) : Math.Max(num, num2);
				if (!{17782})
				{
					int val;
					if (!{17780}.ResourceInfo.Any((GSILocalEnumerablePair<ResourceInfo> {17836}) => {17836}.Info.IsTradingItem))
					{
						if (!{17781}.ResourceInfo.Any((GSILocalEnumerablePair<ResourceInfo> {17837}) => {17837}.Info.IsTradingItem))
						{
							val = 10;
							goto IL_CD;
						}
					}
					val = 5;
					IL_CD:
					num3 = Math.Max(val, num3);
				}
				return 5 * (int)Math.Ceiling((double)((float)num3 / 5f)) - 5 * (int)Math.Ceiling((double)((float){17779} / 5f)) + (int)(Math.Ceiling((double)((float){17779} / 5f)) + 1.0) * 5 - {17779};
			}

			// Token: 0x06000529 RID: 1321 RVA: 0x00017097 File Offset: 0x00015297
			UiControl {18027}.IProvider.{17783}({18027} {17784}, Marker {17785}, float {17786})
			{
				return null;
			}

			// Token: 0x0600052A RID: 1322 RVA: 0x00003100 File Offset: 0x00001300
			void {18027}.IProvider.{17787}()
			{
			}

			// Token: 0x0600052B RID: 1323 RVA: 0x0002AB78 File Offset: 0x00028D78
			[CompilerGenerated]
			internal static void <World_Of_Sea_Battle.Interface.HoldsUiCommon.IProvider.EnumerateItemsAtShip>g__AddStrengthBars|7_38(CustomUi {17788}, InstalledShipUpgradeSlot {17789})
			{
				int num = (Global.Player.UsedShipPlayer.Upgrades.InstalledCount >= 5) ? 38 : 48;
				float num2 = {17789}.Info.Strength / (float){17789}.Info.Info.WearAmount.Value;
				int num3 = 7;
				int num4 = 3;
				Image {13204} = new Image(new Marker((float)num3, (float)num4, (float)(num - 6), 12f), AtlasPortGui.Texture.Tex, new Rectangle(81, 0, 54, 13), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				{17788}.AddChild({13204});
				Image {13204}2 = new Image(new Marker((float)num3, (float)num4, num2 * (float)(num - 6), 12f), AtlasPortGui.Texture.Tex, new Rectangle(136, 0, (int)(num2 * 54f), 13), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				{17788}.AddChild({13204}2);
			}

			// Token: 0x040004A3 RID: 1187
			private readonly int {17790};

			// Token: 0x040004A4 RID: 1188
			private readonly int {17791};
		}
	}
}
