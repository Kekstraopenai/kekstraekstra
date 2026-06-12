using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using TheraEngine.Scene.Lighting;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002EB RID: 747
	internal sealed class {21078} : {20849}, IPortPage
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0008951A File Offset: 0x0008771A
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x00089521 File Offset: 0x00087721
		private static float ScaleFactor { get; set; } = 1.25f;

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x00089529 File Offset: 0x00087729
		private static float MinScaleFactor
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x00089530 File Offset: 0x00087730
		private static float MaxScaleFactor
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x000030FD File Offset: 0x000012FD
		public bool Editor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x000030FD File Offset: 0x000012FD
		public bool CreateChatUi
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x000030FD File Offset: 0x000012FD
		public bool CreateShipStatUi
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x00089537 File Offset: 0x00087737
		public Tlist<{21426}> OpenedCraftPages { get; } = new Tlist<{21426}>();

		// Token: 0x0600105A RID: 4186 RVA: 0x00089540 File Offset: 0x00087740
		public {21078}() : base(true)
		{
			{21078}.<>c__DisplayClass23_0 CS$<>8__locals1 = new {21078}.<>c__DisplayClass23_0();
			CS$<>8__locals1.<>4__this = this;
			this.AnimatedFocus = false;
			base.RenderToDepthMap = true;
			base.EvRemoveFromContainer += this.{21082};
			base.AddChild(new UiControl[]
			{
				new InterfaceBackgroundParticles(base.Pos, ParticlesBackgroundPattern.SparksToUp, ParticlesBackgroundCount.Many, 0.05f, 0.2f, 5f, Global.Render.ParticleManager2D, new Rectangle[]
				{
					AtlasPortGui.backgroundSpark
				})
				{
					Opacity = 0.25f
				},
				new InterfaceBackgroundParticles(base.Pos, ParticlesBackgroundPattern.SparksToUp, ParticlesBackgroundCount.Little, 0.7f, 1.4f, 1f, Global.Render.ParticleManager2D, new Rectangle[]
				{
					AtlasPortGui.backgroundSpark2
				})
				{
					Opacity = 0.1f
				}
			});
			Rectangle uiarea = Engine.GS.UIArea;
			base.AddChild(new Form(new Marker(ref uiarea), new Rectangle(569, 3791, 402, 206), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
			base.AddChild(this.{21092} = new {21201}(100f, 30f, this));
			Form form = new Form(Vector2.Zero, {21078}.c_checkboxBack, PositionAlignment.Center, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false,
				FirstOpacity = 0.6f
			};
			this.{21096} = new ScrollBarControl(new Marker((float)(Engine.GS.UIArea.Width - 30), 80f, 50f, (float)(Engine.GS.UIArea.Height - 90)), CommonAtlas.whitePixel, CommonAtlas.whitePixel, AtlasPortGui.scrollBarNewPointer, CommonAtlas.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{21096}.DisableDepthFocusTest = true;
			Form {13204} = new Form(new Marker((float)(Engine.GS.UIArea.Width - 30), 80f, 10f, (float)(Engine.GS.UIArea.Height - 90)), AtlasPortGui.scrollBarNewFiller, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{21096}.EvChange += this.{21083};
			base.AddChild(this.{21096});
			base.AddChild({13204});
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.Center, PositionAlignment.LeftUp);
			int num = Session.Account.Shipyard.CountOfCompletlyResearchedClasses();
			string {13345} = Local.PortVerfyPage_WaysResearched((int)(Gameplay.ResearchBonus(num) * 100f));
			stackForm.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_14, Color.White * 0.6f, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(Local.PortVerfyPage_WaysResearchedToolTip_2(num), Local.PortVerfyPage_WaysResearchedToolTip_1, Array.Empty<ToolTipCharacteristics>()))
				}
			});
			stackForm.AddSpace(20f);
			CheckboxControl checkboxControl = new CheckboxControl(Vector2.Zero, AtlasPortGui.vd_checkBox_26px_true, AtlasPortGui.vd_checkBox_26px_false, Local.PortVerfyPage_2, Fonts.Philosopher_14, Color.White * 0.6f, false, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState(null, Local.PortVerfyPage_3, Array.Empty<ToolTipCharacteristics>()))
			};
			checkboxControl.EvCheck += this.{21085};
			stackForm.AddItem(new UiControl[]
			{
				checkboxControl
			});
			stackForm.AddSpace(20f);
			CheckboxControl checkboxControl2 = new CheckboxControl(Vector2.Zero, AtlasPortGui.vd_checkBox_26px_true, AtlasPortGui.vd_checkBox_26px_false, Local.PortVerfyPage_2b, Fonts.Philosopher_14, Color.White * 0.6f, false, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			checkboxControl2.EvCheck += this.{21087};
			checkboxControl2.IsChecked = EducationHelper.MakeInvisibleSelectShipButton;
			stackForm.AddItem(new UiControl[]
			{
				checkboxControl2
			});
			stackForm.AddSpace(20f);
			CheckboxControl checkboxControl3 = new CheckboxControl(Vector2.Zero, AtlasPortGui.vd_checkBox_26px_true, AtlasPortGui.vd_checkBox_26px_false, Local.built_many, Fonts.Philosopher_14, Color.White * 0.6f, false, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			checkboxControl3.EvCheck += this.{21089};
			checkboxControl3.IsChecked = EducationHelper.MakeInvisibleSelectShipButton;
			stackForm.AddItem(new UiControl[]
			{
				checkboxControl3
			});
			FractionID[] array = (from {21097} in Enum.GetValues<FractionID>()
			where {21097}.IsNation()
			select {21097}).ToArray<FractionID>();
			for (int i = 0; i < array.Length; i++)
			{
				FractionID fraction = array[i];
				stackForm.AddSpace(20f);
				CheckboxControl fractionCheckBox = new CheckboxControl(Vector2.Zero, AtlasPortGui.vd_checkBox_26px_true, AtlasPortGui.vd_checkBox_26px_false, " ", Fonts.Philosopher_14, Color.White * 0.6f, false, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				fractionCheckBox.EvCheck += delegate(CheckboxCheckedEventArgs {21099})
				{
					if ({21099}.NewValue)
					{
						for (int j = 0; j < CS$<>8__locals1.<>4__this.{21095}.Count; j++)
						{
							if (CS$<>8__locals1.<>4__this.{21095}[j] != fractionCheckBox)
							{
								CS$<>8__locals1.<>4__this.{21095}[j].IsChecked = false;
							}
						}
						CS$<>8__locals1.<>4__this.{21092}.OnlyFraction = new FractionID?(fraction);
						return;
					}
					CS$<>8__locals1.<>4__this.{21092}.OnlyFraction = null;
				};
				fractionCheckBox.ToolTipState = new ToolTipState(fraction.GetName(), "", Array.Empty<ToolTipCharacteristics>());
				this.{21095}.Add(fractionCheckBox);
				float num2 = 0.35f;
				Rectangle fractionFlagPrerender = CommonAtlas.GetFractionFlagPrerender(fraction);
				Image image = new Image(new Marker(0f, 0f, (float)fractionFlagPrerender.Width * num2, (float)fractionFlagPrerender.Height * num2), CommonAtlas.Texture.Tex, fractionFlagPrerender, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm.AddItem(new UiControl[]
				{
					fractionCheckBox
				});
				stackForm.AddItem(new UiControl[]
				{
					image
				});
			}
			form.Pos = new Marker(0f, 0f, stackForm.Pos.WH.X + 20f, 40f);
			form.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.Center, 0f);
			base.AddChildPos(form, PositionAlignment.Center, PositionAlignment.LeftUp, 60f);
			Form form2 = new Form(new Marker((float)(Engine.GS.UIArea.Width - 260), 15f, 245f, 20f), PositionAlignment.RightDown, PositionAlignment.LeftUp);
			Form form3 = form2;
			{21078}.<>c__DisplayClass23_0 CS$<>8__locals3 = CS$<>8__locals1;
			Vector2 {13342} = form2.Pos.XY + new Vector2(208f, -1f);
			CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
			Color lightGray = Color.LightGray;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
			defaultInterpolatedStringHandler.AppendFormatted<float>({21078}.ScaleFactor * 100f, "0");
			defaultInterpolatedStringHandler.AppendLiteral("%");
			form3.AddChild(CS$<>8__locals3.scaleBarLabel = new Label({13342}, philosopher_, lightGray, defaultInterpolatedStringHandler.ToStringAndClear(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form2.AddChildPos(this.{21093} = new ProgressSelectBar(new Marker(0f, 0f, 200f, 15f), AtlasPortGui.cSmallProgressBarFrontSky_Tall, AtlasPortGui.cSmallProgressBarBack_Tall, AtlasPortGui.scrollBar_Pointer.SetWidth(20f).SetHeight(20f), Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExpansionSetVals({21078}.ScaleFactor - {21078}.MinScaleFactor, {21078}.MaxScaleFactor - {21078}.MinScaleFactor).ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {21098})
			{
				{21078}.ScaleFactor = {21078}.MinScaleFactor + {21098}.NewValue * ({21078}.MaxScaleFactor - {21078}.MinScaleFactor);
				Label scaleBarLabel = CS$<>8__locals1.scaleBarLabel;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(1, 1);
				defaultInterpolatedStringHandler2.AppendFormatted<float>({21078}.ScaleFactor * 100f, "0");
				defaultInterpolatedStringHandler2.AppendLiteral("%");
				scaleBarLabel.Text = defaultInterpolatedStringHandler2.ToStringAndClear();
				CS$<>8__locals1.<>4__this.{21092}.Scale({21078}.ScaleFactor);
			}), PositionAlignment.LeftUp, PositionAlignment.Center, 0f);
			this.headLine.AddChild(form2);
			this.headLine.MoveToFrontLevel();
			this.{21092}.Scale({21078}.ScaleFactor);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00089C98 File Offset: 0x00087E98
		protected override void MarkerChanged()
		{
			this.{21092}.MoveView(Vector2.Zero, true);
			this.{21092}.Scale({21078}.ScaleFactor);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00089CBC File Offset: 0x00087EBC
		public void ShowShipPreview({21426} {21080})
		{
			Global.Player.PreviewShip = PlayerShipDynamicInfo.CreateNewFromShipInfo({21080}.Ship, true);
			Global.Player.ForceUpdateShipEffects();
			base.IsVisible = false;
			foreach ({21426} {21426} in ((IEnumerable<{21426}>)this.OpenedCraftPages))
			{
				{21426}.IsVisible = false;
			}
			float num = 1f - Global.Game.StaticSystem.GetSkyShader.DayOrNight;
			Tlist<PointLight> lights = new Tlist<PointLight>();
			PointLight mainLighht = new PointLight(Engine.GS.Camera.Position, Color.Wheat.ToVector3(), 0.15f * num, 200f);
			mainLighht.DrawFlares = PointLightFlaresMode.Disable;
			Global.Render.Pointlights.Add(mainLighht);
			if (num > 0f)
			{
				foreach (Matrix matrix in ((IEnumerable<Matrix>)Global.Player.UsedShip.StaticInfo.SmallLights))
				{
					PointLight {12312} = new PointLight(Global.Player.Transform.Transform3X3(matrix.Translation), Color.Wheat.ToVector3(), 0.3f * num, 5.5f)
					{
						DrawFlares = PointLightFlaresMode.Disable,
						Tag = matrix.Translation
					};
					Global.Render.Pointlights.Add({12312});
					lights.Add({12312});
				}
			}
			Button button = this.{21094};
			if (button != null)
			{
				button.RemoveFromContainer();
			}
			this.{21094} = new Button(new Vector2(Engine.GS.UIArea.HalfWidthHeightInt().X - (float)(AtlasPortGui.buttonBlueBack.Width / 2), (float)Engine.GS.UIArea.Height * 0.9f), AtlasPortGui.buttonBlueBack, PositionAlignment.Center, PositionAlignment.Center).SetText(Local.to_back, Fonts.Philosopher_14, Color.White * 0.7f, false);
			this.{21094}.EvClick += delegate(ClickUiEventArgs {21100})
			{
				this.{21094}.RemoveFromContainer();
				this.{21094} = null;
				this.IsVisible = true;
				foreach ({21426} {21426}2 in ((IEnumerable<{21426}>)this.OpenedCraftPages))
				{
					{21426}2.IsVisible = true;
				}
			};
			this.{21094}.EvRemoveFromContainer += delegate()
			{
				Global.Player.PreviewShip = null;
				Global.Player.ForceUpdateShipEffects();
				foreach (PointLight {12313} in ((IEnumerable<PointLight>)lights))
				{
					Global.Render.Pointlights.Remove({12313});
				}
				Global.Render.Pointlights.Remove(mainLighht);
			};
			this.{21094}.UpdateComplete += delegate(UiControl {21101})
			{
				foreach (PointLight pointLight in ((IEnumerable<PointLight>)lights))
				{
					pointLight.Position = Global.Player.Transform.Transform3X3((Vector3)pointLight.Tag);
				}
				mainLighht.Position = Engine.GS.Camera.Position;
			};
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00089F3C File Offset: 0x0008813C
		public void Close()
		{
			foreach ({21426} {21426} in ((IEnumerable<{21426}>)this.OpenedCraftPages.Clone()))
			{
				{21426}.BlockAndClose();
			}
			base.ClosePage();
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00089F94 File Offset: 0x00088194
		protected override void UserUpdate(ref FrameTime {21081})
		{
			if (InputHelper.NowMouseState.ScrollValue != InputHelper.LastMouseState.ScrollValue)
			{
				int num = InputHelper.NowMouseState.ScrollValue - InputHelper.LastMouseState.ScrollValue;
				if (InputHelper.NowInputState.IsDown(Keys.LeftControl))
				{
					float num2 = this.{21093}.Value + (float)num / 1000f;
					num2 = Math.Clamp(num2, 0f, {21078}.MaxScaleFactor - {21078}.MinScaleFactor);
					Vector2 {21211} = (InputHelper.NowMouseState.Position - new Vector2((float)(Engine.GS.UIArea.Width / 2), 0f)) * (this.{21093}.Value - num2);
					this.{21092}.MoveView({21211}, false);
					this.{21093}.Value = num2;
					this.{21096}.CurrentScrollFactor = 1f - this.{21092}.GetScrollProgress();
				}
				else
				{
					this.{21092}.MoveView(new Vector2(0f, (float)num / 3f), false);
					this.{21096}.CurrentScrollFactor = 1f - this.{21092}.GetScrollProgress();
				}
			}
			base.UserUpdate(ref {21081});
			if (InputHelper.IsClick(Keys.Escape))
			{
				if (this.{21094} != null)
				{
					this.{21094}.ImitateClick(false);
					return;
				}
				if (this.OpenedCraftPages.Size == 0)
				{
					Global.Game.ScenePort.mainHandler(null);
				}
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0008A12A File Offset: 0x0008832A
		[CompilerGenerated]
		private void {21082}()
		{
			base.RemoveFromContainer();
			Button button = this.{21094};
			if (button != null)
			{
				button.RemoveFromContainer();
			}
			this.{21094} = null;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0008A14A File Offset: 0x0008834A
		[CompilerGenerated]
		private void {21083}(ScrollBarChangeEventArgs {21084})
		{
			this.{21092}.SetScrollFromProgress(1f - {21084}.ScrollFactor);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0008A163 File Offset: 0x00088363
		[CompilerGenerated]
		private void {21085}(CheckboxCheckedEventArgs {21086})
		{
			this.{21092}.ShowStats({21086}.NewValue);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0008A176 File Offset: 0x00088376
		[CompilerGenerated]
		private void {21087}(CheckboxCheckedEventArgs {21088})
		{
			this.{21092}.HideDisallowed = {21088}.NewValue;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0008A189 File Offset: 0x00088389
		[CompilerGenerated]
		private void {21089}(CheckboxCheckedEventArgs {21090})
		{
			this.{21092}.HideNotHaving = {21090}.NewValue;
		}

		// Token: 0x04000EF8 RID: 3832
		public static readonly Rectangle c_checkboxBack = new Rectangle(1162, 595, 500, 27);

		// Token: 0x04000EFA RID: 3834
		[CompilerGenerated]
		private readonly Tlist<{21426}> {21091};

		// Token: 0x04000EFB RID: 3835
		private {21201} {21092};

		// Token: 0x04000EFC RID: 3836
		private ProgressSelectBar {21093};

		// Token: 0x04000EFD RID: 3837
		private Button {21094};

		// Token: 0x04000EFE RID: 3838
		private readonly List<CheckboxControl> {21095} = new List<CheckboxControl>();

		// Token: 0x04000EFF RID: 3839
		private ScrollBarControl {21096};
	}
}
