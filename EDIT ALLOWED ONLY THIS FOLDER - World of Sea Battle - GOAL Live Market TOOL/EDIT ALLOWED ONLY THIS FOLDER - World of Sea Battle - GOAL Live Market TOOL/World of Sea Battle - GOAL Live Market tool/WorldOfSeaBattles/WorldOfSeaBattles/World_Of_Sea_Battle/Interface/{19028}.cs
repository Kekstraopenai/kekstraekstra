using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001A8 RID: 424
	internal sealed class {19028} : CustomUi
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x0004AF72 File Offset: 0x00049172
		public static void QueueAchievementDisplay(AchievementEnum {19030})
		{
			{19028}.displayQueue.Enqueue({19030});
			{19028}.WhenUpdate();
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0004AF89 File Offset: 0x00049189
		public static void QueueAchievementDisplay(int {19031})
		{
			{19028}.displayQueue.Enqueue({19031});
			{19028}.WhenUpdate();
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0004AFA0 File Offset: 0x000491A0
		public static void WhenUpdate()
		{
			if ({19028}.currentInstance == null && {19028}.displayQueue.Count > 0)
			{
				new {19028}({19028}.displayQueue.Peek());
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0004AFC8 File Offset: 0x000491C8
		private {19028}(object {19032}) : base(false)
		{
			int num = 576;
			for (int i = 0; i < 32; i++)
			{
				Form form = new Form(new Marker((float)(Engine.GS.UIArea.Width / 2 - num / 32 * (i - 16)), 50f, (float)(num / 32), 170f), AtlasGameGui.rect_asset_whitepixel_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				base.AddChild(form);
				form.Opacity = 0f;
				form.BasicColor = Color.Gold * 0.7f;
				float num2 = Math.Abs(((float)i - 16f) / 16f);
				new UiActionsSleep(form, num2 * 1000f);
				new UiOpacityAnimation(form, 0f, 1f - num2 * num2, 400f);
				new UiOpacityAnimation(form, 1f - num2 * num2, 0f, 400f);
				new UiRemoveAction(form);
			}
			{19028}.currentInstance = this;
			Marker marker = new Marker((float)(Engine.GS.UIArea.Width / 2 - {19028}.c_front.Width / 2), 50f, ref {19028}.c_front);
			Form form2 = new Form(marker, {19028}.c_back, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Form form3 = new Form(marker, {19028}.c_front, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			base.AddChild(new UiControl[]
			{
				form2,
				form3
			});
			new UiMarkerAndOpacityAnimation(form2, 0f, 1f, marker.Offset(-100f, 0f), marker, 800f, UiAmimationCurve.SquaredToBegin);
			new UiMarkerAndOpacityAnimation(form3, 0f, 1f, marker.Offset(100f, 0f), marker, 800f, UiAmimationCurve.SquaredToBegin);
			new UiActionsSleep(form2, 6000f);
			new UiActionsSleep(form3, 6000f);
			new UiOpacityAnimation(form2, 1f, 0f, 2000f);
			new UiOpacityAnimation(form3, 1f, 0f, 2000f);
			new UiActor(form2, new Action(base.RemoveFromContainer));
			if ({19032} is AchievementEnum)
			{
				AchievementEnum key = (AchievementEnum){19032};
				AchievementDisplayInfo achievementDisplayInfo = Gameplay.AchievementsByEnum[key];
				form3.AddChild(new UiControl[]
				{
					new Label(form3.Pos.XY + {19028}.p_achName, Fonts.Philosopher_16Bold, Color.LightGray, achievementDisplayInfo.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp),
					TextBlockBuilder.CreateBlock(270f, achievementDisplayInfo.Description, Color.LightGray, Fonts.Arial_10, -1f).Create(form3.Pos.XY + {19028}.p_achInfoBlock)
				});
				Vector2 vector = marker.XY + {19028}.p_iconCenter - new Vector2(50f, 50f);
				Vector2 vector2 = new Vector2(100f, 100f);
				Marker marker2 = new Marker(ref vector, ref vector2);
				Image image = new Image(marker2, achievementDisplayInfo.Icon, achievementDisplayInfo.Icon.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				UiControl {14249} = image;
				float {14250} = 0f;
				float {14251} = 1f;
				vector = marker.XY + {19028}.p_iconCenter - new Vector2(150f, 150f);
				vector2 = new Vector2(300f, 300f);
				new UiMarkerAndOpacityAnimation({14249}, {14250}, {14251}, new Marker(ref vector, ref vector2), marker2, 1300f, UiAmimationCurve.SquaredToBegin);
				new UiOpacityAnimation(image, 1f, 0f, 8000f);
				base.AddChild(image);
			}
			else
			{
				int num3 = (int){19032};
				TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_12, 1f);
				textBlockBuilder.WriteLine(Local.AchievementGiveUi_0, Color.LightGray);
				form3.AddChild(new UiControl[]
				{
					new Label(form3.Pos.XY + {19028}.p_achName, Fonts.Philosopher_16Bold, Color.LightGray, Local.AchievementGiveUi_1(num3), PositionAlignment.LeftUp, PositionAlignment.LeftUp),
					textBlockBuilder.Create(form3.Pos.XY + {19028}.p_achInfoBlock + new Vector2(0f, 3f))
				});
				Vector2 vector = marker.XY + {19028}.p_iconCenter - new Vector2(50f, 50f);
				Vector2 vector2 = new Vector2(100f, 100f);
				Marker marker3 = new Marker(ref vector, ref vector2);
				Image image2 = new Image(marker3, AtlasGameGui.Texture.Tex, {19028}.c_newRangIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				UiControl {14249}2 = image2;
				float {14250}2 = 0f;
				float {14251}2 = 1f;
				vector = marker.XY + {19028}.p_iconCenter - new Vector2(150f, 150f);
				vector2 = new Vector2(300f, 300f);
				new UiMarkerAndOpacityAnimation({14249}2, {14250}2, {14251}2, new Marker(ref vector, ref vector2), marker3, 1300f, UiAmimationCurve.SquaredToBegin);
				new UiOpacityAnimation(image2, 1f, 0f, 8000f);
				base.AddChild(image2);
			}
			base.EvRemoveFromContainer += this.{19034};
			Global.Game.SoundSystem.PlaySound(GameStaticSoundName.AchievOrLevelUp, 0.03f, 1f);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0004B4FE File Offset: 0x000496FE
		protected override void UserUpdate(ref FrameTime {19033})
		{
			this.{19035} += {19033}.secElapsed;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0004B514 File Offset: 0x00049714
		protected override void UserBackRender()
		{
			if (this.{19036} = (Engine.GS.CurrentTexture != AtlasGameGui.Texture.Tex))
			{
				Engine.GS.SetTexture(AtlasGameGui.Texture);
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0004B554 File Offset: 0x00049754
		protected override void UserFrontRender()
		{
			if (this.{19036})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0004B605 File Offset: 0x00049805
		[CompilerGenerated]
		private void {19034}()
		{
			{19028}.currentInstance = null;
			if (this.{19035} > 1f)
			{
				{19028}.displayQueue.Dequeue();
			}
		}

		// Token: 0x0400089C RID: 2204
		private static {19028} currentInstance;

		// Token: 0x0400089D RID: 2205
		private static Queue<object> displayQueue = new Queue<object>(10);

		// Token: 0x0400089E RID: 2206
		private static readonly Rectangle c_back = new Rectangle(0, 995, 467, 170);

		// Token: 0x0400089F RID: 2207
		private static readonly Rectangle c_front = new Rectangle(0, 1167, 467, 170);

		// Token: 0x040008A0 RID: 2208
		private static readonly Rectangle c_newRangIcon = new Rectangle(0, 1338, 86, 86);

		// Token: 0x040008A1 RID: 2209
		private static readonly Vector2 p_achName = new Vector2(136f, 44f);

		// Token: 0x040008A2 RID: 2210
		private static readonly Vector2 p_achInfoBlock = new Vector2(136f, 68f);

		// Token: 0x040008A3 RID: 2211
		private static readonly Vector2 p_iconCenter = new Vector2(51f, 86f);

		// Token: 0x040008A4 RID: 2212
		private float {19035};

		// Token: 0x040008A5 RID: 2213
		private bool {19036};
	}
}
