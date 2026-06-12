using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200028A RID: 650
	internal sealed class {20501} : {21684}
	{
		// Token: 0x06000E49 RID: 3657 RVA: 0x00078FFC File Offset: 0x000771FC
		public {20501}() : base(Local.design, "", "")
		{
			{20501}.<>c__DisplayClass6_0 CS$<>8__locals1 = new {20501}.<>c__DisplayClass6_0();
			CS$<>8__locals1.<>4__this = this;
			{20501}.CurrentInstance = this;
			{20547}.CurrentInstance.IsVisible = false;
			Global.Game.ScenePort.IsVisibleMainUi = false;
			Global.Camera.CameraEffects.RunFocusEffect(new CameraFocusEffect(delegate()
			{
				if ({20501}.CurrentInstance != null)
				{
					return new Vector3?((Global.Game.ScenePort.CurrentPersonalIsle.Place.GlobalPosition.X0Y ^ 10f) - (Global.Player.Position - Global.Game.ScenePort.CurrentPersonalIsle.Place.GlobalPosition).Normal.X0Y * 20f + this.{20516});
				}
				return null;
			}, 0.9f, 60f));
			base.EvRemoveFromContainer += delegate()
			{
				{20501}.CurrentInstance = null;
				{20547}.CurrentInstance.IsVisible = true;
				Global.Game.ScenePort.IsVisibleMainUi = true;
			};
			base.EnableBackgroundNow = false;
			UiControl[] array = new UiControl[1];
			int num = 0;
			Form form = new Form(new Marker(0f, -45f, (float)Engine.GS.UIArea.Width, (float)({20849}.c_topBlackGradient.Height * (1920 / {20849}.c_topBlackGradient.Width))), {20849}.c_topBlackGradient, PositionAlignment.Both, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			Form form2 = form;
			array[num] = form;
			base.RemoveWithThis(array);
			Form form3 = form2;
			Vector2 {13303} = new Vector2((float)(Engine.GS.UIArea.Width / 2), 24f);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 3);
			defaultInterpolatedStringHandler.AppendLiteral("< ");
			defaultInterpolatedStringHandler.AppendFormatted(Global.Settings.kb_Escape.KeyToString);
			defaultInterpolatedStringHandler.AppendLiteral(" - ");
			defaultInterpolatedStringHandler.AppendFormatted(Local.close);
			defaultInterpolatedStringHandler.AppendLiteral(", ");
			defaultInterpolatedStringHandler.AppendFormatted(Local.isle_decor_window_tt(Global.Settings.ShipControlSchemeName));
			form3.AddChild(new LabelButton({13303}, defaultInterpolatedStringHandler.ToStringAndClear(), Fonts.Philosopher_14, Color.Gray, Color.White, new Action<ClickUiEventArgs>(this.{20509}))
			{
				DisableDepthFocusTest = false,
				PositionAlignment_X = PositionAlignment.Center
			}.Center());
			StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(base.Pos.WH.X / 2f, 40f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			StackForm stackForm2 = new StackForm(base.Pos.XY + new Vector2(base.Pos.WH.X / 2f, 60f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			StackForm stackForm3 = stackForm;
			{20501}.<>c__DisplayClass6_0 CS$<>8__locals2 = CS$<>8__locals1;
			ValueTuple<Func<string>, Func<ShipDesignInfo, bool>>[] array2 = new ValueTuple<Func<string>, Func<ShipDesignInfo, bool>>[5];
			array2[0] = new ValueTuple<Func<string>, Func<ShipDesignInfo, bool>>(() => Local.isle_decor_filter_all, (ShipDesignInfo {20518}) => true);
			array2[1] = new ValueTuple<Func<string>, Func<ShipDesignInfo, bool>>(() => Local.isle_decor_filter_having(Session.Account.EnvDecorElementsAtStorage.GetTotalItemsCount()), (ShipDesignInfo {20519}) => Session.Account.EnvDecorElementsAtStorage[(int){20519}.ID] > 0);
			array2[2] = new ValueTuple<Func<string>, Func<ShipDesignInfo, bool>>(delegate()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler2.AppendFormatted(Local.installed);
				defaultInterpolatedStringHandler2.AppendLiteral(" (");
				defaultInterpolatedStringHandler2.AppendFormatted<int>(Global.Game.ScenePort.CurrentPersonalIsle.InstalledDecor.Size);
				defaultInterpolatedStringHandler2.AppendLiteral(")");
				return defaultInterpolatedStringHandler2.ToStringAndClear();
			}, (ShipDesignInfo {20520}) => Global.Game.ScenePort.CurrentPersonalIsle.InstalledDecor.Any((PersonalIsleInstalledDecorItem {20529}) => {20529}.ID == (int){20520}.ID));
			array2[3] = new ValueTuple<Func<string>, Func<ShipDesignInfo, bool>>(() => Local.isle_decor_filter_flora, (ShipDesignInfo {20521}) => {20521}.nameKey.Contains("flora"));
			array2[4] = new ValueTuple<Func<string>, Func<ShipDesignInfo, bool>>(delegate()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(4, 3);
				defaultInterpolatedStringHandler2.AppendFormatted(Local.bonuses);
				defaultInterpolatedStringHandler2.AppendLiteral(" ");
				defaultInterpolatedStringHandler2.AppendFormatted<int>(Global.Game.ScenePort.CurrentPersonalIsle.InstalledDecor.Count((PersonalIsleInstalledDecorItem {20522}) => {20522}.Info.AccountBonus.Item2 > 0f));
				defaultInterpolatedStringHandler2.AppendLiteral(" / ");
				defaultInterpolatedStringHandler2.AppendFormatted<int>(Global.Game.ScenePort.CurrentPersonalIsle.BuildingsWithBonusLimit);
				return defaultInterpolatedStringHandler2.ToStringAndClear();
			}, (ShipDesignInfo {20523}) => (float){20523}.Bonus.Length + {20523}.AccountBonus.Item2 > 0f);
			CS$<>8__locals2.options = array2;
			CS$<>8__locals1.pickedFilter = 0;
			int num2 = 0;
			ValueTuple<Func<string>, Func<ShipDesignInfo, bool>>[] options = CS$<>8__locals1.options;
			for (int i = 0; i < options.Length; i++)
			{
				ValueTuple<Func<string>, Func<ShipDesignInfo, bool>> item = options[i];
				int myId = num2++;
				LabelButton labelButton = new LabelButton(Vector2.Zero, item.Item1(), Fonts.Philosopher_14, Color.Gray, Color.White, delegate(ClickUiEventArgs {20530})
				{
					CS$<>8__locals1.pickedFilter = myId;
					CS$<>8__locals1.<>4__this.{20517} = item.Item2;
					CS$<>8__locals1.<>4__this.UpdateBlocks(false);
				});
				if (stackForm3.Pos.WH.X + labelButton.Pos.WH.X > base.Pos.WH.X - 100f)
				{
					stackForm3 = stackForm2;
				}
				if (stackForm3.CountChild() > 0)
				{
					stackForm3.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Gray, " ◈ ", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
				}
				labelButton.UpdateComplete += delegate(UiControl {20531})
				{
					((LabelButton){20531}).DefaultColor = ((myId == CS$<>8__locals1.pickedFilter) ? Color.White : Color.Gray);
					((LabelButton){20531}).Text = CS$<>8__locals1.options[myId].Item1();
				};
				stackForm3.AddItem(new UiControl[]
				{
					labelButton
				});
			}
			stackForm.Pos = stackForm.Pos.Offset(-stackForm.Pos.WH.X / 2f, 0f);
			stackForm2.Pos = stackForm2.Pos.Offset(-stackForm2.Pos.WH.X / 2f, 0f);
			base.AddChild(new UiControl[]
			{
				stackForm,
				stackForm2
			});
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0007957A File Offset: 0x0007777A
		protected override IEnumerable<UiControl> CreateDesignComponents()
		{
			{20501}.<CreateDesignComponents>d__7 <CreateDesignComponents>d__ = new {20501}.<CreateDesignComponents>d__7(-2);
			<CreateDesignComponents>d__.<>4__this = this;
			return <CreateDesignComponents>d__;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0007958C File Offset: 0x0007778C
		private void {20502}(ShipDesignInfo {20503})
		{
			if (this.{20513})
			{
				return;
			}
			if ({20503}.AccountBonus.Item2 > 0f)
			{
				if (Global.Game.ScenePort.CurrentPersonalIsle.InstalledDecor.Count((PersonalIsleInstalledDecorItem {20526}) => {20526}.Info.AccountBonus.Item2 > 0f) >= Global.Game.ScenePort.CurrentPersonalIsle.BuildingsWithBonusLimit)
				{
					new {17312}(Local.isle_decor_bonus_limit(Global.Game.ScenePort.CurrentPersonalIsle.BuildingsWithBonusLimit));
					return;
				}
			}
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UIClick, 0.03f, 0.5f);
			base.Opacity = 0.05f;
			base.AllowMouseInput = false;
			this.{20513} = true;
			GSI envDecorElementsAtStorage = Session.Account.EnvDecorElementsAtStorage;
			int id = (int){20503}.ID;
			int num = envDecorElementsAtStorage[id];
			envDecorElementsAtStorage[id] = num - 1;
			PersonalIsleStatus currentPersonalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
			Tlist<PersonalIsleInstalledDecorItem> installedDecor = currentPersonalIsle.InstalledDecor;
			PersonalIsleInstalledDecorItem personalIsleInstalledDecorItem = new PersonalIsleInstalledDecorItem((int){20503}.ID, 0);
			installedDecor.Add(personalIsleInstalledDecorItem);
			this.{20514} = currentPersonalIsle.InstalledDecor.Size - 1;
			this.{20515} = new Button(new Vector2((float)(Engine.GS.UIArea.Width / 2 - AtlasPortGui.buttonBlueBack.Width / 2), (float)(Engine.GS.UIArea.Height - 230)), AtlasPortGui.buttonBlueBack, PositionAlignment.Center, PositionAlignment.Center).SetText(Local.to_back, Fonts.Philosopher_14, Color.White * 0.7f, false);
			base.RemoveWithThis(new UiControl[]
			{
				this.{20515}
			});
			this.{20515}.EvClick += this.{20511};
			base.UpdateBlocks(false);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00079758 File Offset: 0x00077958
		private void {20504}(bool {20505})
		{
			base.Opacity = 1f;
			base.AllowMouseInput = true;
			this.{20513} = false;
			PersonalIsleStatus currentPersonalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
			if ({20505})
			{
				GSI envDecorElementsAtStorage = Session.Account.EnvDecorElementsAtStorage;
				int id = currentPersonalIsle.InstalledDecor[this.{20514}].ID;
				int num = envDecorElementsAtStorage[id];
				envDecorElementsAtStorage[id] = num + 1;
				currentPersonalIsle.InstalledDecor.RemoveAt(this.{20514});
			}
			else
			{
				Global.Game.SoundSystem.PlaySound(GameStaticSoundName.BoardingMove, 0.03f, 0.15f);
				float num2 = 0.6f + currentPersonalIsle.InstalledDecor[this.{20514}].Info.ApartModel.CommonSphere.Radius / 10f;
				Vector3 value = currentPersonalIsle.Place.GlobalTransform.Transform3X3(currentPersonalIsle.InstalledDecor[this.{20514}].GlobalPosition(currentPersonalIsle));
				int num3 = 0;
				while ((float)num3 < 50f * num2)
				{
					FXEngine.CreateEnvironmentLightParticle(value + Rand.NextVector3(-5f, 5f) * num2);
					num3++;
				}
			}
			this.{20514} = -1;
			base.UpdateBlocks(false);
			Button button = this.{20515};
			if (button != null)
			{
				button.RemoveFromContainer();
			}
			this.{20515} = null;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000798B4 File Offset: 0x00077AB4
		private void {20506}(int {20507})
		{
			PersonalIsleStatus currentPersonalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
			this.{20514} = currentPersonalIsle.InstalledDecor.FindIndex((PersonalIsleInstalledDecorItem {20527}) => (int){20527}.PlaceIndex == {20507});
			string text;
			if ({20547}.CheckDecorBonusIsBlockingAndShowMessage(currentPersonalIsle.InstalledDecor[this.{20514}].Info, out text))
			{
				return;
			}
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UIClotting, 0.03f, 0.5f);
			this.{20504}(true);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00079940 File Offset: 0x00077B40
		protected override void UserBackRender()
		{
			base.UserBackRender();
			PersonalIsleStatus currentPersonalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
			if (this.{20513})
			{
				float num = float.MaxValue;
				int num2 = -1;
				for (int i = 0; i < Math.Min(255, currentPersonalIsle.Place.ConnectionsPersonalIsleDecor.Size); i++)
				{
					ValueTuple<Vector3, int> valueTuple = currentPersonalIsle.Place.ConnectionsPersonalIsleDecor[i];
					int num3 = (currentPersonalIsle.InstalledDecor.Array[this.{20514}].Info.ApartModel.CommonSphere.Radius < 30f) ? 1 : 3;
					if (num3 == valueTuple.Item2 || num3 + 1 == valueTuple.Item2)
					{
						Vector3 vector = currentPersonalIsle.Place.GlobalTransform.Transform3X3(valueTuple.Item1);
						if (Global.Camera.IsVisible(vector, 1f))
						{
							Vector2 projection = Global.Camera.GetProjection(ref vector);
							bool flag = false;
							for (int j = 0; j < currentPersonalIsle.InstalledDecor.Size; j++)
							{
								if (j != this.{20514} && ((int)currentPersonalIsle.InstalledDecor[j].PlaceIndex == i || Vector3.Distance(currentPersonalIsle.Place.ConnectionsPersonalIsleDecor[(int)currentPersonalIsle.InstalledDecor[j].PlaceIndex].Item1, valueTuple.Item1) < currentPersonalIsle.InstalledDecor[j].Info.ApartModel.CommonSphere.Radius * 0.4f))
								{
									flag = true;
								}
							}
							if (flag)
							{
								Device gs = Engine.GS;
								Rectangle rectangle = new Rectangle(421, 0, 32, 32);
								Vector2 vector2 = projection - new Vector2(16f);
								Color color = Color.White;
								gs.Draw(rectangle, vector2, color);
							}
							else
							{
								float num4 = Vector2.DistanceSquared(Engine.GS.MouseToUI, projection);
								if (num4 < num)
								{
									num = num4;
									num2 = i;
								}
								Device gs2 = Engine.GS;
								Rectangle rectangle = new Rectangle(656, 15, 27, 27);
								Vector2 vector2 = projection - new Vector2(13f);
								Color color = Color.White;
								gs2.Draw(rectangle, vector2, color);
							}
						}
					}
				}
				if (num2 != -1)
				{
					currentPersonalIsle.InstalledDecor.Array[this.{20514}].PlaceIndex = (byte)num2;
					if (InputHelper.LeftWasClicked && base.InputMode == MouseInputMode.NoFocus)
					{
						this.{20504}(false);
						return;
					}
				}
			}
			else
			{
				float num5 = 2500f;
				Vector2 value = Vector2.Zero;
				int nDecorPlaceIndex = -1;
				foreach (PersonalIsleInstalledDecorItem personalIsleInstalledDecorItem in ((IEnumerable<PersonalIsleInstalledDecorItem>)currentPersonalIsle.InstalledDecor))
				{
					Vector3 vector3 = currentPersonalIsle.Place.GlobalTransform.Transform3X3(personalIsleInstalledDecorItem.GlobalPosition(currentPersonalIsle));
					if (Global.Camera.IsVisible(vector3, 1f))
					{
						Vector2 projection2 = Global.Camera.GetProjection(ref vector3);
						float num6 = Vector2.DistanceSquared(Engine.GS.MouseToUI, projection2);
						Device gs3 = Engine.GS;
						Rectangle rectangle = new Rectangle(421, 33, 32, 32);
						Vector2 vector2 = projection2 - new Vector2(16f);
						Color color = Color.SkyBlue * 0.7f;
						gs3.Draw(rectangle, vector2, color);
						if (num6 < num5 && base.InputMode == MouseInputMode.NoFocus)
						{
							num5 = num6;
							value = projection2;
							nDecorPlaceIndex = (int)personalIsleInstalledDecorItem.PlaceIndex;
						}
					}
				}
				if (nDecorPlaceIndex != -1)
				{
					Device gs4 = Engine.GS;
					Rectangle rectangle = new Rectangle(421, 33, 32, 32);
					Vector2 vector2 = value - new Vector2(16f);
					Color color = Color.White;
					gs4.Draw(rectangle, vector2, color);
					string[] array = {20431}.SeparateNames(currentPersonalIsle.InstalledDecor.First((PersonalIsleInstalledDecorItem {20528}) => (int){20528}.PlaceIndex == nDecorPlaceIndex).Info.Name);
					Engine.GS.SetFont(Fonts.Arial_10);
					Device gs5 = Engine.GS;
					string {14590} = array[0];
					vector2 = value + new Vector2(1f, 20f);
					color = Color.Black;
					gs5.DrawStringCenteredShadow({14590}, vector2, color, 0.8f);
					Device gs6 = Engine.GS;
					string {14590}2 = array[0];
					vector2 = value + new Vector2(0f, 19f);
					color = Color.Orange;
					gs6.DrawStringCenteredShadow({14590}2, vector2, color, 0.8f);
					if (InputHelper.LeftWasClicked && base.InputMode == MouseInputMode.NoFocus)
					{
						this.{20506}(nDecorPlaceIndex);
					}
				}
			}
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00079DF0 File Offset: 0x00077FF0
		protected override void UserFrontRender()
		{
			base.UserFrontRender();
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00079DF8 File Offset: 0x00077FF8
		protected override void UserUpdate(ref FrameTime {20508})
		{
			float scaleFactor = {20508}.secElapsed * 25f;
			if (Global.Settings.kb_ds_Forward.IsDown)
			{
				this.{20516} += Engine.GS.Camera.Direction * scaleFactor;
			}
			if (Global.Settings.kb_ds_Backward.IsDown)
			{
				this.{20516} -= Engine.GS.Camera.Direction * scaleFactor;
			}
			if (Global.Settings.kb_ds_Left.IsDown)
			{
				this.{20516}.XZ = this.{20516}.XZ - Geometry.RotateVector2(Engine.GS.Camera.Direction.XZ, 1.5707964f) * scaleFactor;
			}
			if (Global.Settings.kb_ds_Right.IsDown)
			{
				this.{20516}.XZ = this.{20516}.XZ + Geometry.RotateVector2(Engine.GS.Camera.Direction.XZ, 1.5707964f) * scaleFactor;
			}
			this.{20516}.Y = 0f;
			if (this.{20516}.Length() > 50f)
			{
				this.{20516} = this.{20516}.Normal * Math.Min(50f, this.{20516}.Length());
			}
			base.UserUpdate(ref {20508});
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0007A010 File Offset: 0x00078210
		[CompilerGenerated]
		private void {20509}(ClickUiEventArgs {20510})
		{
			base.BlockAndClose();
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0007A018 File Offset: 0x00078218
		[CompilerGenerated]
		private void {20511}(ClickUiEventArgs {20512})
		{
			this.{20504}(true);
			Button button = this.{20515};
			if (button == null)
			{
				return;
			}
			button.RemoveFromContainer();
		}

		// Token: 0x04000D4E RID: 3406
		public static {20501} CurrentInstance;

		// Token: 0x04000D4F RID: 3407
		private bool {20513};

		// Token: 0x04000D50 RID: 3408
		private int {20514};

		// Token: 0x04000D51 RID: 3409
		private Button {20515};

		// Token: 0x04000D52 RID: 3410
		private Vector3 {20516};

		// Token: 0x04000D53 RID: 3411
		private Func<ShipDesignInfo, bool> {20517} = (ShipDesignInfo {20524}) => true;
	}
}
