using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Common.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020000BD RID: 189
	internal class {17625} : CustomUi
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00025AB4 File Offset: 0x00023CB4
		public Marker ContentArea
		{
			get
			{
				Vector2 contentStart = this.ContentStart;
				return new Marker(ref contentStart, this.{17672}, base.Pos.WH.Y - 68f - 19f);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00025AF4 File Offset: 0x00023CF4
		public Marker ContentAreaWithoutHead
		{
			get
			{
				Vector2 vector = this.ContentStart - new Vector2(0f, 68f);
				return new Marker(ref vector, this.{17672}, base.Pos.WH.Y - 19f);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00025B3F File Offset: 0x00023D3F
		public Vector2 ContentStart
		{
			get
			{
				return base.Pos.XY + new Vector2(19f, 73f);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00025B60 File Offset: 0x00023D60
		protected override bool CanBeWindow
		{
			get
			{
				return this.{17669}.CreateBlockingBackground && this.{17669}.CloseThroughBackground;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060004BE RID: 1214 RVA: 0x00025B7C File Offset: 0x00023D7C
		// (remove) Token: 0x060004BF RID: 1215 RVA: 0x00025BB4 File Offset: 0x00023DB4
		protected event Action<{17625}.RoutingEventArgs<int>> OnTitleItemSelected
		{
			[CompilerGenerated]
			add
			{
				Action<{17625}.RoutingEventArgs<int>> action = this.{17666};
				Action<{17625}.RoutingEventArgs<int>> action2;
				do
				{
					action2 = action;
					Action<{17625}.RoutingEventArgs<int>> value2 = (Action<{17625}.RoutingEventArgs<int>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<{17625}.RoutingEventArgs<int>>>(ref this.{17666}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<{17625}.RoutingEventArgs<int>> action = this.{17666};
				Action<{17625}.RoutingEventArgs<int>> action2;
				do
				{
					action2 = action;
					Action<{17625}.RoutingEventArgs<int>> value2 = (Action<{17625}.RoutingEventArgs<int>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<{17625}.RoutingEventArgs<int>>>(ref this.{17666}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00025BE9 File Offset: 0x00023DE9
		private bool IsWinterStyle
		{
			get
			{
				return CalendarEvents.IsNewYearExtended && !(this is {19779});
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00025C00 File Offset: 0x00023E00
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00025C08 File Offset: 0x00023E08
		protected virtual float winterStyleYDecorOffset { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00025C11 File Offset: 0x00023E11
		private bool IsHalloweenStyle
		{
			get
			{
				return CalendarEvents.IsHalloween && !(this is {19779});
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060004C4 RID: 1220 RVA: 0x00025C28 File Offset: 0x00023E28
		// (remove) Token: 0x060004C5 RID: 1221 RVA: 0x00025C60 File Offset: 0x00023E60
		private event Action<{17625}.RoutingEventArgs<int>> onTitleItemSelectedInternal
		{
			[CompilerGenerated]
			add
			{
				Action<{17625}.RoutingEventArgs<int>> action = this.{17668};
				Action<{17625}.RoutingEventArgs<int>> action2;
				do
				{
					action2 = action;
					Action<{17625}.RoutingEventArgs<int>> value2 = (Action<{17625}.RoutingEventArgs<int>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<{17625}.RoutingEventArgs<int>>>(ref this.{17668}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<{17625}.RoutingEventArgs<int>> action = this.{17668};
				Action<{17625}.RoutingEventArgs<int>> action2;
				do
				{
					action2 = action;
					Action<{17625}.RoutingEventArgs<int>> value2 = (Action<{17625}.RoutingEventArgs<int>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<{17625}.RoutingEventArgs<int>>>(ref this.{17668}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00025C98 File Offset: 0x00023E98
		public {17625}(float {17636}, Rectangle {17637}, {17604} {17638}, Rectangle {17639}, params {17625}.DynamicTittle[] {17640})
		{
			{17625}.<>c__DisplayClass87_0 CS$<>8__locals1 = new {17625}.<>c__DisplayClass87_0();
			CS$<>8__locals1.ux = {17638};
			base..ctor(Marker.FromCentrScreen(Engine.GS.UIArea.WidthHeight(), new Vector2({17636} + 38f, (float)((int)((float){17637}.Height * ({17636} + 38f) / (float){17637}.Width)))), Rectangle.Empty, PositionAlignment.Center, PositionAlignment.Center, Color.White, false);
			CS$<>8__locals1.<>4__this = this;
			Global.Game.inactivityDuration = 0f;
			this.AnimatedFocus = false;
			this.backPath = {17637};
			this.{17672} = {17636};
			this.{17669} = CS$<>8__locals1.ux;
			this.{17680} = ({17640}.Length != 0);
			{17625}.GridElement.ShowSelectedItem = null;
			if (CS$<>8__locals1.ux.BlockGameInput)
			{
				{17625}.numBlockingWindows++;
			}
			this.{17678} = ({17625}.numBlockingWindows > 1);
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UIButton, 0.03f, 1f);
			if (CS$<>8__locals1.ux.StopShip)
			{
				ShipCurrentPlayer player = Global.Player;
				if (player != null)
				{
					player.ResetSpeedAndRotation();
				}
			}
			if (CS$<>8__locals1.ux.BlockGameInput)
			{
				Global.Game.SceneGame.IncreaseMouse();
				if (CS$<>8__locals1.ux.StopShip)
				{
					GameScene.IncreaseGameInput();
				}
			}
			if (CS$<>8__locals1.ux.CreateBlockingBackground)
			{
				this.{17671} = new Image(new Marker(0f, 0f, (float)Engine.GS.UIArea.Width, (float)Engine.GS.UIArea.Height), CommonAtlas.Texture.Tex, new Rectangle(569, 3791, 402, 207), PositionAlignment.Both, PositionAlignment.Both);
				if (CS$<>8__locals1.ux.BackgroundIsTransparent)
				{
					this.{17671}.BasicColor = Color.Transparent;
				}
				else
				{
					this.{17671}.BasicColor = {17625}.c_backColor;
				}
				if (CS$<>8__locals1.ux.CloseThroughBackground)
				{
					this.{17671}.EvClick += delegate(ClickUiEventArgs {17736})
					{
						if (!CS$<>8__locals1.<>4__this.AllowMouseInput && Session.CurrentCrewJob != null)
						{
							return;
						}
						CS$<>8__locals1.<>4__this.BlockAndClose();
					};
				}
				new UiOpacityAnimation(this.{17671}, 0.0001f, 1f, 260f);
			}
			base.MoveToFrontLevel();
			if (CS$<>8__locals1.ux.IsScrollablePage)
			{
				Marker marker = base.Pos;
				base.Pos = marker.SetY(130f);
				this.TexturePath = Rectangle.Empty;
				marker = base.Pos;
				this.{17675} = new ScrollBarControl(new Marker(marker.End.X - (float){17625}.c_scrollPointDefault.Width - 19f + 14f, 133f, (float){17625}.c_scrollPointDefault.Width, (float)(Engine.GS.UIArea.Height - 130 - 68)), CommonAtlas.transpPixel, CommonAtlas.transpPixel, {17625}.c_scrollPointDefault, CommonAtlas.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				this.{17675}.IsVisible = false;
				this.{17675}.PositionAlignment_X = PositionAlignment.Center;
				this.{17675}.PositionAlignment_Y = PositionAlignment.Both;
				TextureHost textureHost = new TextureHost(CommonAtlas.Texture.Tex, false);
				textureHost.AddChild(this.{17675});
				base.RemoveWithThis(new UiControl[]
				{
					textureHost
				});
				textureHost.MoveToFrontLevel();
				this.{17675}.EvChange += delegate(ScrollBarChangeEventArgs {17737})
				{
					if (CS$<>8__locals1.<>4__this.{17676})
					{
						return;
					}
					CS$<>8__locals1.<>4__this.{17674}.Stopful();
					CS$<>8__locals1.<>4__this.{17674}.CurrentScrollValue = CS$<>8__locals1.<>4__this.{17674}.MaxScrollValue * {17737}.ScrollFactor;
					CS$<>8__locals1.<>4__this.{17674}.Stopful();
				};
			}
			base.EvRemoveFromContainer += delegate()
			{
				if (CS$<>8__locals1.ux.BlockGameInput)
				{
					Global.Game.SceneGame.DecreaseMouse();
					if (CS$<>8__locals1.ux.StopShip)
					{
						GameScene.DecreaseGameInput();
					}
				}
				if (CS$<>8__locals1.<>4__this.{17679})
				{
					GameScene.DecreaseGameInput();
				}
				UiControl uiControl = CS$<>8__locals1.<>4__this.{17671};
				if (uiControl != null)
				{
					uiControl.RemoveFromContainer();
				}
				CS$<>8__locals1.<>4__this.{17671} = null;
				if (CS$<>8__locals1.ux.BlockGameInput)
				{
					{17625}.numBlockingWindows--;
				}
			};
			Vector2 value = new Vector2(76f, 30f);
			Vector2 value2 = new Vector2(18f, 19f);
			if ({17640}.Length != 0)
			{
				{17625}.<>c__DisplayClass87_1 CS$<>8__locals2 = new {17625}.<>c__DisplayClass87_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.textColor = (CS$<>8__locals2.CS$<>8__locals1.ux.DarkHeader ? {17625}.c_headTextColorLight : {17625}.c_headTextColor);
				StackForm stackForm = new StackForm(base.Pos.XY + value, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					DisableDepthFocusTest = true
				};
				if ({17640}.Length == 1)
				{
					if ({17640}[0].UpdatableText == null)
					{
						stackForm.AddItem(new UiControl[]
						{
							new Label(Vector2.Zero, Fonts.Philosopher_18, CS$<>8__locals2.textColor, {17640}[0].FixedText, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
					}
					else
					{
						stackForm.AddItem(new UiControl[]
						{
							new LiveLabel(Vector2.Zero, Fonts.Philosopher_18, CS$<>8__locals2.textColor, {17640}[0].UpdatableText, 300)
						});
					}
				}
				else
				{
					int num = 0;
					CS$<>8__locals2.labels = new Tlist<LabelButton>();
					CS$<>8__locals2.textColorSelected = new Color(235, 216, 187);
					for (int i = 0; i < {17640}.Length; i++)
					{
						{17625}.<>c__DisplayClass87_2 CS$<>8__locals3 = new {17625}.<>c__DisplayClass87_2();
						CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals2;
						CS$<>8__locals3.item = {17640}[i];
						int iCached = num++;
						LabelButton button = new LabelButton(Vector2.Zero, CS$<>8__locals3.item.FixedText ?? CS$<>8__locals3.item.UpdatableText(), Fonts.Philosopher_18, (iCached == 0) ? CS$<>8__locals3.CS$<>8__locals2.textColorSelected : ({17625}.c_headTextColor * 0.5f), (iCached == 0) ? CS$<>8__locals3.CS$<>8__locals2.textColorSelected : Color.DarkRed, delegate(ClickUiEventArgs {17739})
						{
							CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.{17681} = ({17739}.Sender as LabelButton);
							CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.ForceItemSelected(iCached);
						})
						{
							DisableDepthFocusTest = true
						};
						if (CS$<>8__locals3.item.UpdatableText != null)
						{
							button.UpdateComplete += delegate(UiControl {17738})
							{
								((LabelButton){17738}).Text = CS$<>8__locals3.item.UpdatableText();
							};
						}
						CS$<>8__locals3.CS$<>8__locals2.labels.Add(button);
						if (iCached != 0)
						{
							stackForm.AddItem(new UiControl[]
							{
								new Form(Vector2.Zero, {17625}.c_tabSeparator, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
								{
									BasicColor = Color.Black * 0.5f,
									AnimatedFocus = false
								}
							});
						}
						Marker marker = button.Pos;
						Form form = new Form(marker.Border(6f, 2f), new Rectangle(639, 239, 268, 73), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							BasicColor = Color.Transparent,
							AnimatedFocus = false
						};
						form.AddChildPos(button, PositionAlignment.Center, PositionAlignment.LeftUp, 0f);
						form.UpdateComplete += delegate(UiControl {17740})
						{
							if (CS$<>8__locals3.CS$<>8__locals2.labels != null && CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.selectedTitle != -1)
							{
								CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.{17681} = CS$<>8__locals3.CS$<>8__locals2.labels[CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.selectedTitle];
							}
							button.DefaultColor = ((CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.{17681} == button) ? CS$<>8__locals3.CS$<>8__locals2.textColorSelected : (CS$<>8__locals3.CS$<>8__locals2.textColor * 0.5f));
							button.FocusColor = ((CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.{17681} == button) ? CS$<>8__locals3.CS$<>8__locals2.textColorSelected : Color.DarkRed);
							{17740}.BasicColor = ((CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.{17681} == button) ? Color.White : Color.Transparent);
						};
						stackForm.AddItem(new UiControl[]
						{
							form
						});
						if (iCached == 0)
						{
							this.{17681} = button;
						}
					}
				}
				if (CS$<>8__locals2.CS$<>8__locals1.ux.DarkHeader)
				{
					Marker marker = new Marker(0f, -10f, base.Pos.WH.X, 474f);
					Marker marker2 = base.Pos;
					base.AddChild(new Form(marker.Offset(marker2.XY), {17625}.c_verticalBlackGradient, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					});
				}
				else
				{
					int num2 = (int)Math.Max(-200f, Math.Max(stackForm.Pos.WH.X + 140f, base.Pos.WH.X * 0.5f) - (float){17625}.c_headBar.Width);
					int num3 = {17625}.c_headBar.Width / 2 + num2 / 2;
					if (CS$<>8__locals2.CS$<>8__locals1.ux.IsScrollablePage)
					{
						Marker marker2 = new Marker(5f, 27f, base.Pos.WH.X - 10f, 31f);
						Marker marker = base.Pos;
						base.AddChild(new Form(marker2.Offset(marker.XY), CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							AnimatedFocus = false,
							BasicColor = new Color(236, 233, 234)
						});
					}
					else
					{
						base.AddChild(new UiControl[]
						{
							new Form(base.Pos.XY, new Rectangle({17625}.c_headBar.X, {17625}.c_headBar.Y, num3, {17625}.c_headBar.Height), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							{
								AnimatedFocus = false
							},
							new Form(base.Pos.XY + new Vector2((float)num3, 0f), new Rectangle({17625}.c_headBar.X + {17625}.c_headBar.Width - num3, {17625}.c_headBar.Y, num3, {17625}.c_headBar.Height), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							{
								AnimatedFocus = false
							}
						});
					}
				}
				base.AddChild(new UiControl[]
				{
					new Form(value2 + base.Pos.XY, {17639}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					},
					stackForm
				});
			}
			if (CS$<>8__locals1.ux.AddBackgroundParticles)
			{
				base.AddChild(new UiControl[]
				{
					new InterfaceBackgroundParticles(this.ContentArea, ParticlesBackgroundPattern.SparksToUp, ParticlesBackgroundCount.Many, 0.05f, 0.2f, 2.5f, Global.Render.ParticleManager2D, new Rectangle[]
					{
						CommonAtlas.backgroundSpark
					})
					{
						Opacity = 0.05f
					},
					new InterfaceBackgroundParticles(this.ContentArea, ParticlesBackgroundPattern.SparksToUp, ParticlesBackgroundCount.Little, 0.7f, 1.4f, 0.5f, Global.Render.ParticleManager2D, new Rectangle[]
					{
						CommonAtlas.backgroundSpark2
					})
					{
						Opacity = 0.03f
					}
				});
			}
			if ({17640}.Length != 0 && this.TextureHost == null)
			{
				if (this.IsWinterStyle)
				{
					Rectangle rectangle = (base.Pos.WH.X < 700f) ? CommonAtlas.WinterWindowSnowShort : CommonAtlas.WinterWindowSnowLong;
					float scaleFactor = VisualHelper.GetRelativeScale(rectangle.WidthHeight(), base.Pos.WH) * 1.015f;
					float {11535} = 0f;
					float {11536} = 0f;
					Vector2 vector = rectangle.WidthHeight() * scaleFactor;
					base.AddChildPos(new Image(new Marker({11535}, {11536}, ref vector), CommonAtlas.Texture.Tex, rectangle, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 0f, (float)(-(float)rectangle.Height / 3) + this.winterStyleYDecorOffset, false);
				}
				if (this.IsHalloweenStyle)
				{
					Rectangle rectangle2 = (base.Pos.WH.X < 700f) ? CommonAtlas.HalloweenWindowDecorShort : CommonAtlas.HalloweenWindowDecorLong;
					float num4 = VisualHelper.GetRelativeScale(rectangle2.WidthHeight(), base.Pos.WH) * 1.015f;
					float {12960} = (float)(-(float)rectangle2.Height) * num4 + 15f;
					float {11535}2 = 0f;
					float {11536}2 = 0f;
					Vector2 vector = rectangle2.WidthHeight() * num4;
					base.AddChildPos(new Image(new Marker({11535}2, {11536}2, ref vector), CommonAtlas.Texture.Tex, rectangle2, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 0f, {12960}, false);
				}
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000267D0 File Offset: 0x000249D0
		public virtual void BlockAndClose()
		{
			if (this.IsClosedByHand)
			{
				return;
			}
			this.IsClosedByHand = true;
			base.AllowMouseInput = false;
			int num = this.{17669}.QuickClosing ? 70 : 260;
			new UiMarkerAndOpacityAnimation(this, 1f, 0f, base.Pos, base.Pos, (float)num, UiAmimationCurve.Linear);
			if (this.{17671} != null)
			{
				new UiOpacityAnimation(this.{17671}, 1f, 0.0001f, (float)num);
			}
			new UiActor(this, new Action(base.RemoveFromContainer));
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UITabSwitch, 0.03f, 1f);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0002687C File Offset: 0x00024A7C
		protected override void UserUpdate(ref FrameTime {17641})
		{
			if (InputHelper.IsClick(Keys.Escape) && this.CanBeWindow && base.AllowMouseInput && base.IsTopmostCustomUi && !KeyInputControl.IsInputElements)
			{
				this.BlockAndClose();
			}
			if (!this.{17669}.StopShip && this.{17669}.CreateBlockingBackground && Global.Player != null && !this.{17678})
			{
				this.{17670}.Evalute(ref {17641}, Global.Player.NowSpeed > 2f);
				this.BasicColor = Color.White * MathHelper.Lerp(1f, 0.7f, this.{17670}.CurrentSoftValueSmoothstep);
				if (this.{17671} != null)
				{
					this.{17671}.BasicColor = Color.Lerp({17625}.c_backColor, Color.Transparent, this.{17670}.CurrentSoftValueSmoothstep);
				}
			}
			if (this.{17671} != null)
			{
				this.{17671}.Opacity = (base.IsVisible ? Geometry.Saturate((base.Opacity - 0.5f) / 0.5f) : 0f);
			}
			if (this.{17669}.IsScrollablePage)
			{
				this.{17675}.IsVisible = (this.{17673} > (float)(Engine.GS.UIArea.Height - 260) && base.IsVisible);
				this.{17674}.MaxScrollValue = Math.Max(0f, base.Pos.WH.Y - (float)Engine.GS.UIArea.Height + 130f);
				this.{17674}.Update({17641}.msElapsed);
				if (base.InputMode != MouseInputMode.NoFocus)
				{
					int num = InputHelper.NowMouseState.ScrollValue - InputHelper.LastMouseState.ScrollValue;
					float num2 = (float)Math.Abs(num) * Scroller.ScrollSpeedFactor;
					if (num > 0)
					{
						this.{17674}.ScrollBack(num2);
					}
					if (num < 0)
					{
						this.{17674}.ScrollNext(num2);
					}
					this.{17676} = true;
					this.{17675}.CurrentScrollFactor = ((this.{17674}.MaxScrollValue == 0f) ? 0f : (this.{17674}.CurrentScrollValue / this.{17674}.MaxScrollValue));
					this.{17676} = false;
				}
				base.Pos = base.Pos.SetHeight(Math.Max(this.{17673} + 30f, (float)(Engine.GS.UIArea.Height - 200)) + 65f).SetY((float)(65 - (int)(this.{17675}.CurrentScrollFactor * this.{17674}.MaxScrollValue)));
			}
			if (!this.{17669}.StopShip)
			{
				if (!this.{17679} && TextBox.IsThereInput)
				{
					GameScene.IncreaseGameInput();
				}
				if (this.{17679} && !TextBox.IsThereInput)
				{
					GameScene.DecreaseGameInput();
				}
				this.{17679} = TextBox.IsThereInput;
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00026B60 File Offset: 0x00024D60
		protected override void UserBackRender()
		{
			Texture2D texture2D = this.TextureHost ?? CommonAtlas.Texture.Tex;
			if (this.{17682} = (Engine.GS.CurrentTexture != texture2D))
			{
				Engine.GS.SetTexture(texture2D);
			}
			float opcaity = base.GetOpcaity();
			Color value = this.BasicColor * opcaity;
			if (this.{17669}.IsScrollablePage)
			{
				this.{17675}.Opacity = opcaity;
				Device gs = Engine.GS;
				Marker marker = base.Pos;
				Rectangle rectangle = new Marker(ref marker.XY, base.Pos.WH.X, (float){17625}.c_seamlessbackUp.Height).ToRect();
				gs.Draw({17625}.c_seamlessbackUp, rectangle, value);
				Device gs2 = Engine.GS;
				Vector2 vector = base.Pos.XY + new Vector2(0f, base.Pos.WH.Y - (float){17625}.c_seamlessbackDown.Height);
				marker = new Marker(ref vector, base.Pos.WH.X, (float){17625}.c_seamlessbackDown.Height);
				rectangle = marker.ToRect();
				gs2.Draw({17625}.c_seamlessbackDown, rectangle, value);
				Device gs3 = Engine.GS;
				marker = new Marker(base.Pos.XY.X, base.Pos.XY.Y + (float){17625}.c_seamlessbackUp.Height, base.Pos.WH.X, base.Pos.WH.Y - (float){17625}.c_seamlessbackUp.Height - (float){17625}.c_seamlessbackDown.Height);
				rectangle = marker.ToRect();
				gs3.Draw({17625}.c_seamlessbackBody, rectangle, value);
				Rectangle uiarea = Engine.GS.UIArea;
				marker = this.ContentArea;
				Rectangle rectangle2 = Rectangle.Intersect(uiarea, marker.ToRect());
				float num = (float)rectangle2.X / (float)Engine.GS.UIArea.Width;
				float num2 = (float)rectangle2.Y / (float)Engine.GS.UIArea.Height;
				float num3 = (float)(rectangle2.X + rectangle2.Width) / (float)Engine.GS.UIArea.Width;
				float num4 = (float)(rectangle2.Y + rectangle2.Height) / (float)Engine.GS.UIArea.Height;
				num -= 0.1f;
				num3 += 0.2f;
				marker = new Marker((float)this.backPath.X + (float)this.backPath.Width * num, (float)this.backPath.Y + (float)this.backPath.Height * num2, (float)this.backPath.Width * (num3 - num), (float)this.backPath.Height * (num4 - num2));
				Rectangle rectangle3 = marker.ToRect();
				Engine.GS.Draw(rectangle3, rectangle2, value);
				Device gs4 = Engine.GS;
				Color color = new Color(68, 69, 84) * 0.33333334f;
				gs4.Draw(CommonAtlas.whitePixel, rectangle2, color);
				if (this.{17681} != null)
				{
					marker = this.{17681}.Pos;
					float x = marker.Center.X - (float)({17625}.c_selectedTitleArrow.Width / 2);
					marker = this.{17681}.Pos;
					Vector2 vector2 = new Vector2(x, marker.End.Y - 2f);
					Device gs5 = Engine.GS;
					color = Color.White * base.GetOpcaity();
					gs5.Draw({17625}.c_selectedTitleArrow, vector2, color);
				}
			}
			else
			{
				Device gs6 = Engine.GS;
				Marker marker = base.Pos;
				Rectangle rectangle = marker.ToRect();
				gs6.Draw(this.backPath, rectangle, value);
				if (this.{17669}.AddDecorations)
				{
					Device gs7 = Engine.GS;
					marker = base.Pos;
					marker = marker.Border(-9f);
					rectangle = marker.ToRect();
					Color color = value * 0.66f;
					gs7.Draw({17625}.c_overlayColor, rectangle, color);
				}
			}
			if (this.IsWinterStyle || this.IsHalloweenStyle)
			{
				Rectangle rectangle4 = this.IsWinterStyle ? new Rectangle(481, 1744, 347, 243) : new Rectangle(481, 1988, 347, 243);
				Color color2 = (this.IsWinterStyle ? (Color.White * 0.07f) : (Color.White * 0.14f)) * base.Opacity;
				int num5 = (int)(base.Pos.WH.Y / ((float)rectangle4.Height * VisualHelper.GetRelativeScale(rectangle4.WidthHeight(), base.Pos.WH)));
				float x2 = base.Pos.WH.X;
				float num6 = base.Pos.WH.Y / (float)num5;
				int num7 = 10;
				for (int i = 0; i < num5; i++)
				{
					Device gs8 = Engine.GS;
					Texture2D images = OtherTextures.Images;
					Rectangle rectangle = new Rectangle(base.Pos.XY.X + (float)num7, base.Pos.XY.Y + (float)i * num6 + (float)num7, x2 - (float)(num7 * 2), num6 - (float)(num7 * 2), false);
					gs8.DrawCustomTexture(images, rectangle4, rectangle, color2);
				}
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000270BD File Offset: 0x000252BD
		protected override void UserFrontRender()
		{
			if (this.{17682})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000270D4 File Offset: 0x000252D4
		protected void ForceItemSelected(int {17642})
		{
			{17625}.RoutingEventArgs<int> routingEventArgs = new {17625}.RoutingEventArgs<int>({17642});
			Action<{17625}.RoutingEventArgs<int>> action = this.{17666};
			if (action != null)
			{
				action(routingEventArgs);
			}
			Action<{17625}.RoutingEventArgs<int>> action2 = this.{17668};
			if (action2 != null)
			{
				action2(routingEventArgs);
			}
			if (!routingEventArgs.Cancelled)
			{
				this.selectedTitle = {17642};
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0002711B File Offset: 0x0002531B
		protected void MakeTabUnselected()
		{
			this.selectedTitle = -1;
			this.{17681} = null;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0002712B File Offset: 0x0002532B
		[NullableContext(2)]
		protected string GetPressedTabButtonText()
		{
			LabelButton labelButton = this.{17681};
			if (labelButton == null)
			{
				return null;
			}
			return labelButton.Text;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00027140 File Offset: 0x00025340
		public void AddBigHeadBar(Texture2D {17643}, Rectangle {17644}, string {17645}, Color {17646}, float {17647} = 0f, float {17648} = 0f)
		{
			Form form = new Form(new Marker(ref {17644}).SetX(base.Pos.Center.X - (float)({17644}.Width / 2)).SetY(base.Pos.XY.Y - (float)({17644}.Height / 2) + {17647}), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Image {13204} = new Image(form.Pos, {17643}, {17644}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChild({13204});
			UiControl uiControl = form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_18, {17646}, {17645}, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.Center, 0f);
			uiControl.Pos = uiControl.Pos.Offset(0f, {17648});
			base.AddChild(form);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00027208 File Offset: 0x00025408
		public void ComposeTab(params Form[] {17649})
		{
			this.{17665}();
			Tab tab = new Tab(base.Pos, Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(tab);
			Tlist<UiControl> tlist = this.{17677};
			UiControl tab2 = tab;
			tlist.Add(tab2);
			foreach (Form form in {17649})
			{
				tab.Add(new Form[]
				{
					form
				});
			}
			tab.Select(this.selectedTitle);
			this.onTitleItemSelectedInternal += delegate({17625}.RoutingEventArgs<int> {17741})
			{
				if (!{17741}.Cancelled)
				{
					tab.Select({17741}.Payload);
				}
			};
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000272A8 File Offset: 0x000254A8
		public void ComposeDynamicTab(params Func<Form>[] {17650})
		{
			this.{17665}();
			Tab tab = new Tab(base.Pos, Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(tab);
			Tlist<UiControl> tlist = this.{17677};
			UiControl tab2 = tab;
			tlist.Add(tab2);
			foreach (Func<Form> func in {17650})
			{
				tab.Add(new Form[]
				{
					new Form(tab.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					}
				});
			}
			this.onTitleItemSelectedInternal += delegate({17625}.RoutingEventArgs<int> {17742})
			{
				if (!{17742}.Cancelled)
				{
					tab.Select({17742}.Payload);
					if (tab.SelectedForm.Tag == null)
					{
						tab.SelectedForm.Tag = new object();
						tab.SelectedForm.AddChildPos({17650}[{17742}.Payload](), PositionAlignment.LeftUp, PositionAlignment.LeftUp, 0f);
					}
				}
			};
			this.ForceItemSelected(this.selectedTitle);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00027364 File Offset: 0x00025564
		public void ComposeDynamicTab(Texture2D {17651}, params Action<StackForm>[] {17652})
		{
			int cached = 0;
			this.{17665}();
			Action <>9__1;
			Action<UiControl> <>9__2;
			this.onTitleItemSelectedInternal += delegate({17625}.RoutingEventArgs<int> {17743})
			{
				if ({17743}.Cancelled)
				{
					return;
				}
				bool flag = cached != {17743}.Payload;
				cached = {17743}.Payload;
				this.{17665}();
				{17625} <>4__this = this;
				Action refreshCurrentDynamicTabPage;
				if ((refreshCurrentDynamicTabPage = <>9__1) == null)
				{
					refreshCurrentDynamicTabPage = (<>9__1 = delegate()
					{
						this.ForceItemSelected(cached);
					});
				}
				<>4__this.RefreshCurrentDynamicTabPage = refreshCurrentDynamicTabPage;
				StackForm stackForm = new StackForm(this.ContentStart, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				{17652}[{17743}.Payload](stackForm);
				if ({17651} == null)
				{
					this.AddChild(stackForm);
					Tlist<UiControl> tlist = this.{17677};
					UiControl uiControl = stackForm;
					tlist.Add(uiControl);
				}
				else
				{
					TextureHost textureHost = new TextureHost({17651}, false);
					textureHost.Pos = stackForm.Pos;
					textureHost.AddChild(stackForm);
					this.AddChild(textureHost);
					UiControl uiControl2 = textureHost;
					Action<UiControl> {12880};
					if (({12880} = <>9__2) == null)
					{
						{12880} = (<>9__2 = delegate(UiControl {17744})
						{
							{17744}.Pos = {17744}.Pos.SetHeight(Math.Max(this.ContentArea.WH.Y, {17744}.Pos.WH.Y));
						});
					}
					uiControl2.UpdateComplete += {12880};
					textureHost.PositionAlignment_X = PositionAlignment.LeftUp;
					textureHost.PositionAlignment_Y = PositionAlignment.LeftUp;
					Tlist<UiControl> tlist2 = this.{17677};
					UiControl uiControl = textureHost;
					tlist2.Add(uiControl);
					if (flag)
					{
						new UiOpacityAnimation(textureHost, 0f, 1f, 150f);
					}
				}
				this.{17673} = stackForm.Pos.WH.Y;
			};
			this.ForceItemSelected(this.selectedTitle);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000273B8 File Offset: 0x000255B8
		public void ComposeTabWithScroll(Action<StackForm> {17653}, bool {17654}, bool {17655}, params Action<ListItemViewControl>[] {17656})
		{
			this.{17665}();
			float headSpaceToKeep = 0f;
			UiControl uiControl;
			if ({17653} != null)
			{
				StackForm stackForm = new StackForm(this.ContentStart, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				{17653}(stackForm);
				base.AddChild(stackForm);
				Tlist<UiControl> tlist = this.{17677};
				uiControl = stackForm;
				tlist.Add(uiControl);
				headSpaceToKeep = stackForm.Pos.WH.Y;
			}
			Marker {14103} = this.ContentArea.Offset(0f, headSpaceToKeep).Resize(0f, -headSpaceToKeep);
			Tab tab = new Tab({14103}, Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(tab);
			Tlist<UiControl> tlist2 = this.{17677};
			uiControl = tab;
			tlist2.Add(uiControl);
			foreach (Action<ListItemViewControl> action in {17656})
			{
				ScrollBarControl scrollBarControl = new ScrollBarControl(new Marker(base.Pos.End.X - 19f - (float){17625}.c_scrollPointDefault.Width, this.ContentArea.XY.Y + headSpaceToKeep, (float){17625}.c_scrollPointDefault.Width, this.ContentArea.WH.Y - headSpaceToKeep), CommonAtlas.transpPixel, CommonAtlas.transpPixel, {17625}.c_scrollPointDefault, CommonAtlas.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				ListItemViewControl listItemViewControl = new ListItemViewControl(tab.Pos, scrollBarControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				if (!{17655})
				{
					action(listItemViewControl);
				}
				scrollBarControl.IsVisible = (({17655} | {17654}) || listItemViewControl.ContentSize.Y + headSpaceToKeep > this.ContentArea.WH.Y);
				Form form = new Form(tab.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form.AddChild(new UiControl[]
				{
					listItemViewControl,
					scrollBarControl
				});
				tab.Add(new Form[]
				{
					form
				});
			}
			this.onTitleItemSelectedInternal += delegate({17625}.RoutingEventArgs<int> {17729})
			{
				if (!{17729}.Cancelled)
				{
					if ({17655})
					{
						this.RefreshCurrentDynamicTabPage = delegate()
						{
							ListItemViewControl listItemViewControl2 = tab.GetPage({17729}.Payload).FirstChild() as ListItemViewControl;
							float currentScrollValue = listItemViewControl2.CurrentScrollValue;
							listItemViewControl2.Clear();
							{17656}[{17729}.Payload](listItemViewControl2);
							listItemViewControl2.ReferencedScrollBar.IsVisible = ({17654} || listItemViewControl2.ContentSize.Y + headSpaceToKeep > this.ContentArea.WH.Y);
							listItemViewControl2.SetScrollValue(currentScrollValue);
						};
						this.RefreshCurrentDynamicTabPage();
					}
					tab.Select({17729}.Payload);
				}
			};
			this.ForceItemSelected(this.selectedTitle);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00027618 File Offset: 0x00025818
		public static StackForm CreateHorizontalUnion(float {17657}, params UiControl[] {17658})
		{
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			foreach (UiControl uiControl in {17658})
			{
				if ({17657} > 0f && stackForm.CountChild() > 0)
				{
					stackForm.AddSpace(Math.Max(0f, {17657} - {17658}[0].Pos.WH.X));
					{17657} = 0f;
				}
				stackForm.AddItem(new UiControl[]
				{
					uiControl
				});
			}
			return stackForm;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00027694 File Offset: 0x00025894
		public void ComposeGraphicItems(int {17659}, params {17625}.ComposeGraphicItemsParam[] {17660})
		{
			IEnumerable<{17625}.GridElement> data = {17660}.Select(delegate({17625}.ComposeGraphicItemsParam {17726})
			{
				{17625}.GridElement gridElement = new {17625}.GridElement({17726}.id, {17726}.header, new Image(new Marker(0f, 0f, 1f, 1f), {17726}.texture, {17726}.path, PositionAlignment.LeftUp, PositionAlignment.LeftUp), {17726}.click);
				Action<Form> custom = {17726}.custom;
				if (custom != null)
				{
					custom(gridElement);
				}
				return gridElement;
			});
			this.ComposeTabWithScroll(null, false, false, new Action<ListItemViewControl>[]
			{
				delegate(ListItemViewControl {17730})
				{
					BlocksStackFormControl blocksStackFormControl = new BlocksStackFormControl(Vector2.Zero, {17659}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					blocksStackFormControl.BorderThickness = -20f;
					foreach ({17625}.GridElement gridElement in data)
					{
						blocksStackFormControl.AddItem(new UiControl[]
						{
							gridElement
						});
					}
					{17730}.AddItem(new UiControl[]
					{
						blocksStackFormControl
					});
				}
			});
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000276F8 File Offset: 0x000258F8
		public void ComposeTextItems([TupleElementNames(new string[]
		{
			"id",
			"text",
			"itemPath",
			"otherContent",
			"click"
		})] params ValueTuple<object, string, Rectangle, Action<Form>, Action<object>>[] {17661})
		{
			IEnumerable<Form> data = {17661}.Select(delegate([TupleElementNames(new string[]
			{
				"id",
				"text",
				"itemPath",
				"otherContent",
				"click"
			})] ValueTuple<object, string, Rectangle, Action<Form>, Action<object>> {17727})
			{
				Form form = new Form(Vector2.Zero, {17727}.Item3, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form.EvClick += delegate(ClickUiEventArgs {17732})
				{
					Action<object> item2 = {17727}.Item5;
					if (item2 == null)
					{
						return;
					}
					item2({17727}.Item1);
				};
				if (!string.IsNullOrEmpty({17727}.Item2))
				{
					form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, Color.White * 0.8f, {17727}.Item2, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, 4f);
				}
				Action<Form> item = {17727}.Item4;
				if (item != null)
				{
					item(form);
				}
				return form;
			});
			this.ComposeTabWithScroll(null, false, false, new Action<ListItemViewControl>[]
			{
				delegate(ListItemViewControl {17731})
				{
					foreach (Form form in data)
					{
						{17731}.AddItem(new UiControl[]
						{
							form
						});
					}
				}
			});
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00027754 File Offset: 0x00025954
		public void ComposeBook(CustomSpriteFont {17662}, {17625}.ComposeBookParam[] {17663})
		{
			{17625}.<>c__DisplayClass106_0 CS$<>8__locals1 = new {17625}.<>c__DisplayClass106_0();
			CS$<>8__locals1.pages = {17663};
			this.{17665}();
			Marker marker = this.{17680} ? this.ContentArea.Resize(0f, -15f) : this.ContentAreaWithoutHead.Offset(0f, 30f).Resize(0f, -50f);
			Marker marker2 = new Marker(marker.XY.X, marker.XY.Y, (float)(CS$<>8__locals1.pages[0].Path.Width + 26), marker.WH.Y);
			Marker {13197} = new Marker(marker.XY.X + marker2.WH.X, marker.XY.Y, marker.WH.X - marker2.WH.X, marker.WH.Y);
			ScrollBarControl scrollBarControl = new ScrollBarControl(new Marker(marker.XY.X + marker2.WH.X - 30f, marker.XY.Y, (float)CommonAtlas.c_scrollPoint.Width, marker2.WH.Y), Rectangle.Empty, Rectangle.Empty, CommonAtlas.c_scrollPoint, CommonAtlas.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			ListItemViewControl listItemViewControl = new ListItemViewControl(marker2, scrollBarControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.content = new Form({13197}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			this.AddChildComposer(new UiControl[]
			{
				listItemViewControl,
				scrollBarControl,
				CS$<>8__locals1.content
			});
			string b = null;
			{17625}.ComposeBookParam[] pages = CS$<>8__locals1.pages;
			for (int i = 0; i < pages.Length; i++)
			{
				{17625}.<>c__DisplayClass106_1 CS$<>8__locals2 = new {17625}.<>c__DisplayClass106_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.page = pages[i];
				if (CS$<>8__locals2.page.IsVisible)
				{
					if (CS$<>8__locals2.page.CategoryOrNull != b)
					{
						listItemViewControl.AddItem(new UiControl[]
						{
							new Label(Vector2.Zero, {17662}, Color.White * 0.7f, CS$<>8__locals2.page.CategoryOrNull, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
						b = CS$<>8__locals2.page.CategoryOrNull;
					}
					CS$<>8__locals2.page.AssignedButton = new Button(Vector2.Zero, CS$<>8__locals2.page.Path, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					CS$<>8__locals2.page.AssignedButton.SetText(CS$<>8__locals2.page.Name, {17662}, Color.White * ((CS$<>8__locals2.page.CategoryOrNull == null) ? 0.9f : 0.5f), true);
					Form unreadMarker = new Form(Vector2.Zero, {17625}.c_unreadMarker, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false,
						IsVisible = CS$<>8__locals2.page.HasUnreadMarker
					};
					CS$<>8__locals2.page.AssignedButton.AddChildPos(unreadMarker, PositionAlignment.RightDown, PositionAlignment.Center, 25f);
					CS$<>8__locals2.page.AssignedButton.UpdateComplete += delegate(UiControl {17735})
					{
						unreadMarker.IsVisible = CS$<>8__locals2.page.HasUnreadMarker;
						Action<Button> middleware = CS$<>8__locals2.page.Middleware;
						if (middleware == null)
						{
							return;
						}
						middleware((Button){17735});
					};
					listItemViewControl.AddItem(new UiControl[]
					{
						CS$<>8__locals2.page.AssignedButton
					});
					CS$<>8__locals2.page.AssignedButton.EvClick += delegate(ClickUiEventArgs {17734})
					{
						CS$<>8__locals2.CS$<>8__locals1.<ComposeBook>g__LoadPage|0(CS$<>8__locals2.page);
					};
				}
			}
			{17625}.ComposeBookParam composeBookParam = CS$<>8__locals1.pages.FirstOrDefault(({17625}.ComposeBookParam {17728}) => {17728}.SelectedByDefault);
			if (composeBookParam != null)
			{
				float currentScrollFactor = Math.Max(0f, (float)(Array.IndexOf<{17625}.ComposeBookParam>(CS$<>8__locals1.pages, composeBookParam) - 1) / (float)CS$<>8__locals1.pages.Length);
				scrollBarControl.CurrentScrollFactor = currentScrollFactor;
				CS$<>8__locals1.<ComposeBook>g__LoadPage|0(composeBookParam);
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00027B6C File Offset: 0x00025D6C
		public void AddChildComposer(params UiControl[] {17664})
		{
			base.AddChild({17664});
			this.{17677}.Add({17664});
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00027B84 File Offset: 0x00025D84
		private void {17665}()
		{
			foreach (UiControl uiControl in ((IEnumerable<UiControl>)this.{17677}))
			{
				uiControl.RemoveFromContainer();
			}
			this.{17677}.Clear();
			this.RefreshCurrentDynamicTabPage = null;
		}

		// Token: 0x04000405 RID: 1029
		public static readonly Rectangle c_verticalBlackGradient = new Rectangle(693, 1489, 322, 224);

		// Token: 0x04000406 RID: 1030
		public static readonly Rectangle c_back1 = new Rectangle(0, 2272, 660, 462);

		// Token: 0x04000407 RID: 1031
		public static readonly Rectangle c_back2 = new Rectangle(890, 1955, 480, 356);

		// Token: 0x04000408 RID: 1032
		public static readonly Rectangle c_back3 = new Rectangle(815, 3126, 480, 356);

		// Token: 0x04000409 RID: 1033
		public static readonly Rectangle c_back4 = new Rectangle(404, 1955, 485, 316);

		// Token: 0x0400040A RID: 1034
		public static readonly Rectangle c_verticalCard = new Rectangle(356, 3732, 199, 268);

		// Token: 0x0400040B RID: 1035
		public static readonly Rectangle c_verticalCardGray = new Rectangle(282, 3463, 198, 268);

		// Token: 0x0400040C RID: 1036
		public static readonly Rectangle v_backVerticalSkullDark = new Rectangle(1826, 1503, 476, 639);

		// Token: 0x0400040D RID: 1037
		public static readonly Rectangle v_backVerticalSkullWhite = new Rectangle(1781, 863, 476, 639);

		// Token: 0x0400040E RID: 1038
		public static readonly Rectangle c_scrollPointDefault = new Rectangle(1483, 1939, 21, 71);

		// Token: 0x0400040F RID: 1039
		public static readonly Rectangle c_btLight_mini = new Rectangle(1473, 2010, 58, 37);

		// Token: 0x04000410 RID: 1040
		public static readonly Rectangle c_btRed_big = new Rectangle(1371, 2267, 157, 37);

		// Token: 0x04000411 RID: 1041
		public static readonly Rectangle c_btLight_small = new Rectangle(1371, 1955, 100, 30);

		// Token: 0x04000412 RID: 1042
		public static readonly Rectangle c_btLight_mid = new Rectangle(1371, 2048, 129, 34);

		// Token: 0x04000413 RID: 1043
		public static readonly Rectangle c_btLight_big = new Rectangle(1371, 2153, 157, 37);

		// Token: 0x04000414 RID: 1044
		public static readonly Rectangle c_btGray_small = new Rectangle(1371, 1986, 100, 30);

		// Token: 0x04000415 RID: 1045
		public static readonly Rectangle c_btGray_mid = new Rectangle(1371, 2083, 129, 34);

		// Token: 0x04000416 RID: 1046
		public static readonly Rectangle c_btGray_mid_long = new Rectangle(1562, 2304, 157, 34);

		// Token: 0x04000417 RID: 1047
		public static readonly Rectangle c_btGray_big = new Rectangle(1371, 2191, 157, 37);

		// Token: 0x04000418 RID: 1048
		public static readonly Rectangle c_btSlate_small = new Rectangle(1371, 2017, 100, 30);

		// Token: 0x04000419 RID: 1049
		public static readonly Rectangle c_btSlate_mid = new Rectangle(1371, 2118, 129, 34);

		// Token: 0x0400041A RID: 1050
		public static readonly Rectangle c_btSlate_big = new Rectangle(1371, 2229, 157, 37);

		// Token: 0x0400041B RID: 1051
		public static readonly Rectangle c_unreadMarker = new Rectangle(81, 113, 14, 14);

		// Token: 0x0400041C RID: 1052
		public static readonly Rectangle c_keepMarker = new Rectangle(96, 115, 46, 12);

		// Token: 0x0400041D RID: 1053
		public static readonly Rectangle c_selectedTitleArrow = new Rectangle(219, 103, 48, 14);

		// Token: 0x0400041E RID: 1054
		public static readonly ExpandoTexturePath c_defaultGoldenFrame = ExpandoTexturePath.CreateBox(new Rectangle(2311, 1248, 509, 390), new Rectangle(2311, 1276, 509, 390), new Rectangle(2341, 1248, 509, 390), new Rectangle(2341, 1276, 509, 390));

		// Token: 0x0400041F RID: 1055
		public static readonly Rectangle c_icStreeringWheel = new Rectangle(0, 997, 48, 48);

		// Token: 0x04000420 RID: 1056
		public static readonly Rectangle c_icTreasury = new Rectangle(49, 997, 48, 48);

		// Token: 0x04000421 RID: 1057
		public static readonly Rectangle c_icBook = new Rectangle(125, 997, 48, 48);

		// Token: 0x04000422 RID: 1058
		public static readonly Rectangle c_icAnchor = new Rectangle(223, 997, 48, 48);

		// Token: 0x04000423 RID: 1059
		public static readonly Rectangle c_icShield = new Rectangle(174, 997, 48, 48);

		// Token: 0x04000424 RID: 1060
		public static readonly Rectangle c_icPeople = new Rectangle(272, 997, 48, 48);

		// Token: 0x04000425 RID: 1061
		public static readonly Rectangle с_icQuestion = new Rectangle(321, 997, 48, 48);

		// Token: 0x04000426 RID: 1062
		protected static readonly Rectangle c_seamlessbackUp = new Rectangle(1826, 2143, 800, 70);

		// Token: 0x04000427 RID: 1063
		protected static readonly Rectangle c_seamlessbackDown = new Rectangle(1826, 2231, 800, 33);

		// Token: 0x04000428 RID: 1064
		protected static readonly Rectangle c_seamlessbackBody = new Rectangle(1826, 2215, 800, 14);

		// Token: 0x04000429 RID: 1065
		private static readonly Rectangle c_headBar = new Rectangle(2311, 1770, 549, 67);

		// Token: 0x0400042A RID: 1066
		private static readonly Rectangle c_tabSeparator = new Rectangle(98, 997, 26, 22);

		// Token: 0x0400042B RID: 1067
		private static readonly Rectangle c_overlayColor = new Rectangle(0, 3740, 354, 260);

		// Token: 0x0400042C RID: 1068
		private static readonly Rectangle c_backShadow = new Rectangle(875, 2945, 219, 167);

		// Token: 0x0400042D RID: 1069
		private static readonly Color c_backColor = Color.White * 0.7f;

		// Token: 0x0400042E RID: 1070
		private static readonly Color c_headTextColor = new Color(4, 11, 17);

		// Token: 0x0400042F RID: 1071
		private static readonly Color c_headTextColorLight = Color.Wheat;

		// Token: 0x04000430 RID: 1072
		private const int scrollableWindowSpace = 130;

		// Token: 0x04000431 RID: 1073
		private const int headSpace = 68;

		// Token: 0x04000432 RID: 1074
		private const int contentXoffset = 19;

		// Token: 0x04000433 RID: 1075
		[CompilerGenerated]
		private Action<{17625}.RoutingEventArgs<int>> {17666};

		// Token: 0x04000434 RID: 1076
		protected bool IsClosedByHand;

		// Token: 0x04000435 RID: 1077
		protected Action RefreshCurrentDynamicTabPage;

		// Token: 0x04000436 RID: 1078
		protected int selectedTitle;

		// Token: 0x04000437 RID: 1079
		[CompilerGenerated]
		private float {17667};

		// Token: 0x04000438 RID: 1080
		[CompilerGenerated]
		private Action<{17625}.RoutingEventArgs<int>> {17668};

		// Token: 0x04000439 RID: 1081
		private {17604} {17669};

		// Token: 0x0400043A RID: 1082
		private SoftTrigger {17670} = new SoftTrigger(0f, 1f, 0.6f);

		// Token: 0x0400043B RID: 1083
		private UiControl {17671};

		// Token: 0x0400043C RID: 1084
		private float {17672};

		// Token: 0x0400043D RID: 1085
		private float {17673};

		// Token: 0x0400043E RID: 1086
		private Scroller {17674} = new Scroller(900f);

		// Token: 0x0400043F RID: 1087
		private ScrollBarControl {17675};

		// Token: 0x04000440 RID: 1088
		private bool {17676};

		// Token: 0x04000441 RID: 1089
		protected Rectangle backPath;

		// Token: 0x04000442 RID: 1090
		private Tlist<UiControl> {17677} = new Tlist<UiControl>();

		// Token: 0x04000443 RID: 1091
		private bool {17678};

		// Token: 0x04000444 RID: 1092
		private bool {17679};

		// Token: 0x04000445 RID: 1093
		private bool {17680};

		// Token: 0x04000446 RID: 1094
		private LabelButton {17681};

		// Token: 0x04000447 RID: 1095
		[Nullable(2)]
		protected Texture2D TextureHost;

		// Token: 0x04000448 RID: 1096
		private static int numBlockingWindows;

		// Token: 0x04000449 RID: 1097
		private bool {17682};

		// Token: 0x020000BE RID: 190
		public readonly struct ComposeGraphicItemsParam
		{
			// Token: 0x060004DA RID: 1242 RVA: 0x00028072 File Offset: 0x00026272
			public ComposeGraphicItemsParam(object {17689}, string {17690}, Texture2D {17691}, Rectangle {17692}, Action<{17625}.GridElement> {17693}, Action<Form> {17694})
			{
				this.id = {17689};
				this.header = {17690};
				this.texture = {17691};
				this.path = {17692};
				this.click = {17693};
				this.custom = {17694};
			}

			// Token: 0x0400044A RID: 1098
			public readonly object id;

			// Token: 0x0400044B RID: 1099
			public readonly string header;

			// Token: 0x0400044C RID: 1100
			public readonly Texture2D texture;

			// Token: 0x0400044D RID: 1101
			public readonly Rectangle path;

			// Token: 0x0400044E RID: 1102
			public readonly Action<{17625}.GridElement> click;

			// Token: 0x0400044F RID: 1103
			public readonly Action<Form> custom;
		}

		// Token: 0x020000BF RID: 191
		public class ComposeBookParam
		{
			// Token: 0x060004DB RID: 1243 RVA: 0x000280A1 File Offset: 0x000262A1
			public ComposeBookParam(string {17699}, Rectangle {17700}, Rectangle {17701}, Action<Form> {17702})
			{
				this.Name = {17699};
				this.Path = {17700};
				this.PathSelected = {17701};
				this.Loader = {17702};
				this.Middleware = null;
				this.IsVisible = true;
			}

			// Token: 0x04000450 RID: 1104
			public readonly string Name;

			// Token: 0x04000451 RID: 1105
			public readonly Rectangle Path;

			// Token: 0x04000452 RID: 1106
			public readonly Rectangle PathSelected;

			// Token: 0x04000453 RID: 1107
			public readonly Action<Form> Loader;

			// Token: 0x04000454 RID: 1108
			public Action<Button> Middleware;

			// Token: 0x04000455 RID: 1109
			public bool IsVisible;

			// Token: 0x04000456 RID: 1110
			public bool HasUnreadMarker;

			// Token: 0x04000457 RID: 1111
			public Button AssignedButton;

			// Token: 0x04000458 RID: 1112
			public object Tag;

			// Token: 0x04000459 RID: 1113
			public string CategoryOrNull;

			// Token: 0x0400045A RID: 1114
			public bool SelectedByDefault;
		}

		// Token: 0x020000C0 RID: 192
		public class GridElement : CustomUi
		{
			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x060004DC RID: 1244 RVA: 0x000280D4 File Offset: 0x000262D4
			public object Id
			{
				get
				{
					return this.{17715};
				}
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x000280DC File Offset: 0x000262DC
			public GridElement(object {17707}, string {17708}, Image {17709}, Action<{17625}.GridElement> {17710})
			{
				Vector2 vector = default(Vector2);
				base..ctor(new Marker(ref vector, ref {17625}.GridElement.c_element), {17625}.GridElement.c_element, PositionAlignment.LeftUp, PositionAlignment.LeftUp, Color.White, false);
				{17625}.GridElement <>4__this = this;
				this.{17714} = 1f;
				this.{17716} = {17708};
				this.{17715} = {17707};
				this.{17717} = {17709};
				{17709}.Pos = {17625}.GridElement.elementContentPos.Offset(base.Pos.XY);
				base.AddChild({17709});
				base.EvClick += delegate(ClickUiEventArgs {17718})
				{
					Action<{17625}.GridElement> click = {17710};
					if (click == null)
					{
						return;
					}
					click(<>4__this);
				};
			}

			// Token: 0x060004DE RID: 1246 RVA: 0x00028180 File Offset: 0x00026380
			protected override void UserUpdate(ref FrameTime {17711})
			{
				if (base.InputMode == MouseInputMode.Down)
				{
					if (this.{17714} > 0.95f)
					{
						this.{17714} -= {17711}.secElapsed * 2f;
						if (this.{17714} < 0.95f)
						{
							this.{17714} = 0.95f;
							return;
						}
					}
				}
				else if (base.InputMode == MouseInputMode.Focused)
				{
					if (this.{17714} < 1.05f)
					{
						this.{17714} += {17711}.secElapsed * 0.5f;
						if (this.{17714} > 1.05f)
						{
							this.{17714} = 1.05f;
							return;
						}
					}
				}
				else
				{
					if (this.{17714} > 1f)
					{
						this.{17714} -= {17711}.secElapsed;
						if (this.{17714} < 1f)
						{
							this.{17714} = 1f;
						}
					}
					if (this.{17714} < 1f)
					{
						this.{17714} += {17711}.secElapsed;
						if (this.{17714} > 1f)
						{
							this.{17714} = 1f;
						}
					}
				}
			}

			// Token: 0x060004DF RID: 1247 RVA: 0x00028298 File Offset: 0x00026498
			protected override void UserBackRender()
			{
				this.{17712} = base.Pos;
				Vector2 vector = base.Pos.XY - base.Pos.WH * (this.{17714} - 1f) * 0.5f;
				Vector2 vector2 = base.Pos.WH * this.{17714};
				base.Pos = new Marker(ref vector, ref vector2);
				this.{17713} = {17625}.GridElement.elementContentPos.Offset(this.{17712}.XY);
				vector = this.{17713}.XY - this.{17713}.WH * (this.{17714} - 1f) * 0.5f;
				vector2 = this.{17713}.WH * this.{17714};
				this.{17713} = new Marker(ref vector, ref vector2);
				this.{17717}.Pos = this.{17713};
			}

			// Token: 0x060004E0 RID: 1248 RVA: 0x0002839C File Offset: 0x0002659C
			protected override void UserFrontRender()
			{
				float num = Math.Max(0f, (this.{17714} - 1f) / 0.05f);
				float num2 = 0.8f + num * 0.2f;
				if (!string.IsNullOrEmpty(this.{17716}))
				{
					Rectangle rectangle = new Rectangle((int)this.{17713}.XY.X, (int)(this.{17713}.XY.Y + this.{17713}.WH.Y - num2 * 23f) + 1, (int)this.{17713}.WH.X, (int)(num2 * 23f));
					Device gs = Engine.GS;
					Color color = Color.DimGray * 0.7f;
					gs.Draw(CommonAtlas.whitePixel, rectangle, color);
					Engine.GS.SetFont(Fonts.Arial_12);
					Device gs2 = Engine.GS;
					string {14599} = this.{17716};
					Vector2 vector = new Vector2((float)(rectangle.X + 5), (float)rectangle.Y);
					color = Color.White * (num2 * num2 * num2);
					gs2.DrawString({14599}, vector, color);
				}
				if ({17625}.GridElement.ShowSelectedItem == this)
				{
					Device gs3 = Engine.GS;
					Rectangle rectangle2 = base.Pos.ToRect();
					Color color = Color.White * 1f;
					gs3.Draw({17625}.GridElement.c_element_selection, rectangle2, color);
					Device gs4 = Engine.GS;
					rectangle2 = base.Pos.ToRect();
					color = Color.White * 1f;
					gs4.Draw({17625}.GridElement.c_element_selection, rectangle2, color);
				}
				base.Pos = this.{17712};
			}

			// Token: 0x0400045B RID: 1115
			public static {17625}.GridElement ShowSelectedItem;

			// Token: 0x0400045C RID: 1116
			private static readonly Rectangle c_element = new Rectangle(2518, 3448, 246, 163);

			// Token: 0x0400045D RID: 1117
			private static readonly Rectangle c_element_selection = new Rectangle(2775, 3612, 246, 163);

			// Token: 0x0400045E RID: 1118
			private static readonly Marker elementContentPos = new Marker(18f, 18f, 205f, 124f);

			// Token: 0x0400045F RID: 1119
			private Marker {17712};

			// Token: 0x04000460 RID: 1120
			private Marker {17713};

			// Token: 0x04000461 RID: 1121
			private float {17714};

			// Token: 0x04000462 RID: 1122
			private object {17715};

			// Token: 0x04000463 RID: 1123
			private string {17716};

			// Token: 0x04000464 RID: 1124
			private Image {17717};
		}

		// Token: 0x020000C2 RID: 194
		protected internal struct DynamicTittle
		{
			// Token: 0x060004E4 RID: 1252 RVA: 0x000285AB File Offset: 0x000267AB
			public DynamicTittle(string {17721})
			{
				this.FixedText = {17721};
				this.UpdatableText = null;
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x000285BB File Offset: 0x000267BB
			public DynamicTittle(Func<string> {17722})
			{
				this.FixedText = null;
				this.UpdatableText = {17722};
			}

			// Token: 0x060004E6 RID: 1254 RVA: 0x000285CB File Offset: 0x000267CB
			public static implicit operator {17625}.DynamicTittle(string {17723})
			{
				return new {17625}.DynamicTittle({17723});
			}

			// Token: 0x04000467 RID: 1127
			public string FixedText;

			// Token: 0x04000468 RID: 1128
			public Func<string> UpdatableText;
		}

		// Token: 0x020000C3 RID: 195
		protected internal class RoutingEventArgs<T>
		{
			// Token: 0x060004E7 RID: 1255 RVA: 0x000285D3 File Offset: 0x000267D3
			public RoutingEventArgs(T {17725})
			{
				this.Payload = {17725};
				this.Cancelled = false;
			}

			// Token: 0x04000469 RID: 1129
			public readonly T Payload;

			// Token: 0x0400046A RID: 1130
			public bool Cancelled;
		}
	}
}
