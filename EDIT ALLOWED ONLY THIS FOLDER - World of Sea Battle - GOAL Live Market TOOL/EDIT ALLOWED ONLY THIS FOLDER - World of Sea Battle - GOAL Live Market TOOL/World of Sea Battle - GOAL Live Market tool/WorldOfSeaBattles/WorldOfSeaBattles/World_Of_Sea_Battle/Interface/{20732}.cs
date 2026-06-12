using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002AF RID: 687
	internal sealed class {20732}
	{
		// Token: 0x06000F21 RID: 3873 RVA: 0x0007FA38 File Offset: 0x0007DC38
		public {20732}()
		{
			if ({20881}.CurrentInstance != null)
			{
				Global.Game.ScenePort.mainHandler(null);
			}
			new {17312}(Local.design_order_1, new Action<int>(this.{20737}), new {17443}[]
			{
				new {17443}(Local.design_order_select_file, Local.design_order_select_file_size, {17312}.cIconPlus, false, 0f),
				new {17443}(Local.close, "", {17312}.cIconReject, false, 0f)
			});
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0007FAC4 File Offset: 0x0007DCC4
		private bool {20733}()
		{
			string text = Engine.PlatformTools.OpenFileDialog(null, "PNG Image (*.png)|*.png");
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			this.{20743} = File.ReadAllBytes(text);
			this.{20742} = VisualHelper.LoadTexture2DFromBytes(this.{20743}, int.MaxValue, true);
			if (this.{20742} == null)
			{
				new {17312}(Local.design_order_load_err);
				return false;
			}
			if (this.{20742}.Width > 1600 || this.{20742}.Height > 1600)
			{
				this.{20742}.Dispose();
				new {17312}(Local.design_order_size_big(1600)).EvCloseByAnyButton += delegate()
				{
					new {20732}();
				};
				return false;
			}
			if (this.{20742}.Width < 512 || this.{20742}.Height < 512)
			{
				this.{20742}.Dispose();
				new {17312}(Local.design_order_size_small).EvCloseByAnyButton += delegate()
				{
					new {20732}();
				};
				return false;
			}
			return true;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0007FBF0 File Offset: 0x0007DDF0
		private void {20734}(ShipDesignCategory {20735})
		{
			{20732}.<>c__DisplayClass4_0 CS$<>8__locals1 = new {20732}.<>c__DisplayClass4_0();
			CS$<>8__locals1.<>4__this = this;
			{20732}.<>c__DisplayClass4_0 CS$<>8__locals2 = CS$<>8__locals1;
			Rectangle uiarea = Engine.GS.UIArea;
			CS$<>8__locals2.holder = new Form(new Marker(ref uiarea), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			PublicDesignInfo publicDesignInfo = new PublicDesignInfo(10000, ShipDesignCategory.Decal2, PublicDesignStatus.Normal, 0U, null);
			CS$<>8__locals1.design = new ShipDesignInfo(publicDesignInfo)
			{
				ApartIconTex = this.{20742}
			};
			Global.Player.Client.ExampleDesigns.Add(CS$<>8__locals1.design);
			Global.Game.ScenePort.UpdateGuiForViewShip();
			StackForm stackForm = new StackForm(new Vector2((float)(Engine.GS.UIArea.Width / 2), (float)(Engine.GS.UIArea.Height - 250)), UiOrientation.VerticalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.BorderThickness = 3f;
			CS$<>8__locals1.backExaplePreviewButton = new Button(Vector2.Zero, AtlasPortGui.buttonBlueBack, PositionAlignment.Center, PositionAlignment.Center).SetText(Local.to_back, Fonts.Philosopher_14, Color.White * 0.7f, false);
			{20732}.<>c__DisplayClass4_0 CS$<>8__locals3 = CS$<>8__locals1;
			Button button = new Button(Vector2.Zero, {20881}.c_buyButton, PositionAlignment.Center, PositionAlignment.Center);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
			defaultInterpolatedStringHandler.AppendFormatted(Local.TradePortInterface_9);
			defaultInterpolatedStringHandler.AppendLiteral(" (");
			defaultInterpolatedStringHandler.AppendFormatted<RTI>(Gameplay.PublicDesignPrice);
			defaultInterpolatedStringHandler.AppendLiteral(" ");
			defaultInterpolatedStringHandler.AppendFormatted(Local.monets);
			defaultInterpolatedStringHandler.AppendLiteral(")");
			CS$<>8__locals3.backExaplePreviewButton_shop = button.SetText(defaultInterpolatedStringHandler.ToStringAndClear(), Fonts.Philosopher_14, Color.White, false);
			CS$<>8__locals1.backExaplePreviewButton_shop.Pos = CS$<>8__locals1.backExaplePreviewButton_shop.Pos.ScaleWidth(1.3f);
			TextBlockControl textBlockControl = TextBlockBuilder.CreateBlock(550f, publicDesignInfo.IsFlag ? Local.design_designInfo : Local.design_order_tt, Color.White * 0.8f, Fonts.Philosopher_14, -1f).Create(Vector2.Zero);
			LabelButton labelButton = new LabelButton(Vector2.Zero, Local.design_order_rules, Fonts.Philosopher_14, Color.SkyBlue, Color.White, null);
			labelButton.SetUnderlineDecoration(AtlasPortGui.whitePixel, AtlasPortGui.Texture.Tex);
			labelButton.EvClick += delegate(ClickUiEventArgs {20744})
			{
				Helpers.ExecuteBrowser(Local.launcher_designs_rules_ref, false);
			};
			stackForm.AddItem(new UiControl[]
			{
				CS$<>8__locals1.backExaplePreviewButton
			});
			stackForm.AddItem(new UiControl[]
			{
				CS$<>8__locals1.backExaplePreviewButton_shop
			});
			stackForm.AddItem(new UiControl[]
			{
				textBlockControl
			});
			stackForm.AddItem(new UiControl[]
			{
				labelButton
			});
			stackForm.Pos = stackForm.Pos.Offset(-stackForm.Pos.WH.X / 2f, 0f);
			CS$<>8__locals1.holder.AddChild(stackForm);
			CS$<>8__locals1.holder.EvRemoveFromContainer += delegate()
			{
				CS$<>8__locals1.backExaplePreviewButton = null;
				CS$<>8__locals1.backExaplePreviewButton_shop = null;
				Global.Player.Client.ExampleDesigns.Clear();
				if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
				{
					Global.Game.ScenePort.UpdateGuiForViewShip();
				}
				else
				{
					Global.Player.ForceUpdateShipEffects();
				}
				CS$<>8__locals1.<>4__this.{20742}.Dispose();
			};
			CS$<>8__locals1.backExaplePreviewButton.EvClick += delegate(ClickUiEventArgs {20745})
			{
				CS$<>8__locals1.holder.RemoveFromContainer();
			};
			CS$<>8__locals1.backExaplePreviewButton_shop.EvClick += delegate(ClickUiEventArgs {20746})
			{
				string design_order_ = Local.design_order_2;
				Action {17372};
				if (({17372} = CS$<>8__locals1.<>9__4) == null)
				{
					{17372} = (CS$<>8__locals1.<>9__4 = delegate()
					{
						{20732}.<>c__DisplayClass4_0.<<CreateUi>b__4>d <<CreateUi>b__4>d;
						<<CreateUi>b__4>d.<>t__builder = AsyncVoidMethodBuilder.Create();
						<<CreateUi>b__4>d.<>4__this = CS$<>8__locals1;
						<<CreateUi>b__4>d.<>1__state = -1;
						<<CreateUi>b__4>d.<>t__builder.Start<{20732}.<>c__DisplayClass4_0.<<CreateUi>b__4>d>(ref <<CreateUi>b__4>d);
					});
				}
				new {17312}(design_order_, {17372}, delegate()
				{
				});
			};
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0007FF04 File Offset: 0x0007E104
		private Task<string> PlaceOrder(ShipDesignCategory {20736})
		{
			{20732}.<PlaceOrder>d__5 <PlaceOrder>d__;
			<PlaceOrder>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<PlaceOrder>d__.<>4__this = this;
			<PlaceOrder>d__.category = {20736};
			<PlaceOrder>d__.<>1__state = -1;
			<PlaceOrder>d__.<>t__builder.Start<{20732}.<PlaceOrder>d__5>(ref <PlaceOrder>d__);
			return <PlaceOrder>d__.<>t__builder.Task;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0007FF4F File Offset: 0x0007E14F
		[CompilerGenerated]
		private void {20737}(int {20738})
		{
			if ({20738} == 1)
			{
				return;
			}
			new Thread(new ParameterizedThreadStart(this.{20739}))
			{
				ApartmentState = ApartmentState.STA
			}.Start();
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0007FF73 File Offset: 0x0007E173
		[NullableContext(2)]
		[CompilerGenerated]
		private void {20739}(object {20740})
		{
			if (this.{20733}())
			{
				new UiActor(Global.Game.GetInterfaceManager.Host, new Action(this.{20741}));
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0007FF9E File Offset: 0x0007E19E
		[CompilerGenerated]
		private void {20741}()
		{
			this.{20734}(ShipDesignCategory.Decal2);
		}

		// Token: 0x04000DF1 RID: 3569
		private Texture2D {20742};

		// Token: 0x04000DF2 RID: 3570
		private byte[] {20743};
	}
}
