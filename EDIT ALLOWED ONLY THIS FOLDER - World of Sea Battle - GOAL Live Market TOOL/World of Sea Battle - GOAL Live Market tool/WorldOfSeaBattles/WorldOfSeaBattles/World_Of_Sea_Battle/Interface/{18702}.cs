using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000172 RID: 370
	internal class {18702} : {17625}
	{
		// Token: 0x06000886 RID: 2182 RVA: 0x00041FD8 File Offset: 0x000401D8
		public static void TryToOpenExactArticle(string {18705})
		{
			if ({18702}.CurrentInstance == null)
			{
				new {18702}(false, Gameplay.Gamepedia.First((GamepediaArticleInfo {18737}) => {18737}.Title == {18705}));
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00042018 File Offset: 0x00040218
		public {18702}(bool {18706}, GamepediaArticleInfo {18707} = null)
		{
			float {17636} = 800f;
			Rectangle c_back = {17625}.c_back3;
			{17604} {17638};
			if (!{18706})
			{
				{17638} = {17604}.InGameWindow;
			}
			else
			{
				({17638} = new {17604}()).CloseThroughBackground = false;
			}
			base..ctor({17636}, c_back, {17638}, {17625}.с_icQuestion, new {17625}.DynamicTittle[]
			{
				Local.EducationPlanWindow_Title
			});
			int j;
			int i;
			for (j = 0; j < Global.Settings.UnreadGamepedia.Size; j = i + 1)
			{
				if (!Gameplay.Gamepedia.Any(delegate(GamepediaArticleInfo {18727})
				{
					EducationOnboarding? condition = {18727}.Condition;
					EducationOnboarding educationOnboarding = Global.Settings.UnreadGamepedia.Array[j];
					return condition.GetValueOrDefault() == educationOnboarding & condition != null;
				}))
				{
					Global.Settings.UnreadGamepedia.FastRemoveAt(j);
					i = j;
					j = i - 1;
				}
				i = j;
			}
			base.AddChild(new Form(new Marker(205f, 44f, 298f, 531f).Offset(base.Pos.XY), new Rectangle(2000, 3240, 171, 204), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
			this.{18722} = {18706};
			{18702}.CurrentInstance = this;
			base.EvRemoveFromContainer += this.{18720};
			this.{18708}({18707});
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00042168 File Offset: 0x00040368
		private void {18708}(GamepediaArticleInfo {18709} = null)
		{
			{18702}.<>c__DisplayClass12_0 CS$<>8__locals1 = new {18702}.<>c__DisplayClass12_0();
			CS$<>8__locals1.<>4__this = this;
			{18702}.<>c__DisplayClass12_0 CS$<>8__locals2 = CS$<>8__locals1;
			GamepediaArticleInfo initArticle = {18709};
			if ({18709} == null)
			{
				if (Global.Settings.UnreadGamepedia.Size <= 0)
				{
					initArticle = Gameplay.Gamepedia.First();
				}
				else
				{
					initArticle = Gameplay.Gamepedia.Find((GamepediaArticleInfo {18725}) => {18725}.Condition != null && Global.Settings.UnreadGamepedia.Contains({18725}.Condition.Value));
				}
			}
			CS$<>8__locals2.initArticle = initArticle;
			Tlist<{17625}.ComposeBookParam> tlist = new Tlist<{17625}.ComposeBookParam>();
			Tlist<{17625}.ComposeBookParam> tlist2 = tlist;
			{17625}.ComposeBookParam composeBookParam = new {17625}.ComposeBookParam("", {18702}.c_item, {18702}.c_itemSelected, null)
			{
				Middleware = delegate(Button {18728})
				{
					if ({18728}.CountChild() >= 2)
					{
						return;
					}
					TextBox textBox = new TextBox(new Marker(0f, 0f, 195f, 26f), new Rectangle(1993, 2305, 300, 35), Fonts.Philosopher_14, Color.White * 0.7f, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					textBox.FontColor = Color.White * 0.9f;
					textBox.AttachMaxLengthModerator(12, null, Color.Transparent);
					textBox.DefaultText = Local.search;
					textBox.Text = CS$<>8__locals1.<>4__this.{18723};
					if (!string.IsNullOrEmpty(CS$<>8__locals1.<>4__this.{18723}))
					{
						textBox.IsEnter = true;
					}
					TextBox textBox2 = textBox;
					Action<string> {13673};
					if (({13673} = CS$<>8__locals1.<>9__5) == null)
					{
						{13673} = (CS$<>8__locals1.<>9__5 = delegate(string {18729})
						{
							if (!string.IsNullOrEmpty({18729}) && !CS$<>8__locals1.<>4__this.{18724})
							{
								CS$<>8__locals1.<>4__this.{18724} = true;
								GameScene.IncreaseGameInput();
							}
							if (CS$<>8__locals1.<>4__this.{18723} != {18729})
							{
								CS$<>8__locals1.<>4__this.{18723} = {18729};
								CS$<>8__locals1.<>4__this.{18708}(null);
							}
						});
					}
					textBox2.EvTextChanged += {13673};
					{18728}.AddChildPos(textBox, PositionAlignment.Center, PositionAlignment.Center, 0f);
				}
			};
			tlist2.Add(composeBookParam);
			tlist.Add(Gameplay.Gamepedia.Where(delegate(GamepediaArticleInfo {18730})
			{
				if (!string.IsNullOrEmpty(CS$<>8__locals1.<>4__this.{18723}) && !{18730}.Title.Contains(CS$<>8__locals1.<>4__this.{18723}, StringComparison.OrdinalIgnoreCase))
				{
					IEnumerable<string> textParagraphs = {18730}.GetTextParagraphs();
					Func<string, bool> predicate;
					if ((predicate = CS$<>8__locals1.<>9__6) == null)
					{
						predicate = (CS$<>8__locals1.<>9__6 = ((string {18731}) => {18731}.Contains(CS$<>8__locals1.<>4__this.{18723}, StringComparison.OrdinalIgnoreCase)));
					}
					return textParagraphs.Any(predicate);
				}
				return true;
			}).Select(delegate(GamepediaArticleInfo {18732})
			{
				bool available = {18732}.Condition == null || Session.Account.EducationQuest.HasFlag({18732}.Condition.Value) || Debugging.DebugInfo;
				return new {17625}.ComposeBookParam({18732}.Title, {18702}.c_item, {18702}.c_itemSelected, delegate(Form {18734})
				{
					CS$<>8__locals1.<>4__this.{18710}({18732}, {18734});
				})
				{
					Middleware = delegate(Button {18735})
					{
						{18735}.Opacity = (available ? 1f : 0.14f);
					},
					HasUnreadMarker = ({18732}.Condition != null && Global.Settings.UnreadGamepedia.Contains({18732}.Condition.Value)),
					SelectedByDefault = ({18732} == CS$<>8__locals1.initArticle)
				};
			}));
			Tlist<{17625}.ComposeBookParam> tlist3 = tlist;
			composeBookParam = new {17625}.ComposeBookParam(Local.EducationPlanWindow_Craft, {18702}.c_item, {18702}.c_itemSelected, delegate(Form {18733})
			{
				CS$<>8__locals1.<>4__this.{18713}({18733});
			});
			tlist3.Add(composeBookParam);
			base.ComposeBook(Fonts.Philosopher_14, tlist.ToArray());
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00042271 File Offset: 0x00040471
		public override void BlockAndClose()
		{
			base.BlockAndClose();
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0004227C File Offset: 0x0004047C
		private void {18710}(GamepediaArticleInfo {18711}, Form {18712})
		{
			if ({18711}.Condition != null)
			{
				Tlist<EducationOnboarding> unreadGamepedia = Global.Settings.UnreadGamepedia;
				EducationOnboarding value = {18711}.Condition.Value;
				unreadGamepedia.FastRemove(value);
			}
			this.{18721} = {18711};
			StackForm stackForm = new StackForm({18712}.Pos.XY + new Vector2(0f, 10f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{18715}(stackForm, {18711}, {18712});
			foreach (GamepediaArticleInfo {18717} in ((IEnumerable<GamepediaArticleInfo>){18711}.Subparagraphs))
			{
				stackForm.AddSpace(15f);
				this.{18715}(stackForm, {18717}, {18712});
			}
			if (this.{18722})
			{
				stackForm.AddSpace(30f);
				StackForm stackForm2 = stackForm;
				UiControl[] array = new UiControl[1];
				array[0] = new Button(Vector2.Zero, CommonAtlas.basicButton, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.close, Fonts.Philosopher_14, Color.Wheat, false).ExClick(delegate(ClickUiEventArgs {18726})
				{
					{18702}.CurrentInstance.BlockAndClose();
				});
				stackForm2.AddItem(array);
			}
			if (stackForm.Pos.WH.Y > {18712}.Pos.WH.Y)
			{
				Viewbox viewbox = new Viewbox({18712}.Pos.SetXY(stackForm.Pos.XY).Resize(-25f, 0f), CommonAtlas.transpPixel, CommonAtlas.transpPixel, CommonAtlas.c_scrollPoint, CommonAtlas.whitePixel);
				viewbox.AddItem(new UiControl[]
				{
					stackForm
				});
				{18712}.AddChild(viewbox);
			}
			else
			{
				{18712}.AddChild(stackForm);
			}
			base.AddChild({18712});
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0004243C File Offset: 0x0004063C
		private void {18713}(Form {18714})
		{
			StackForm stackForm = new StackForm({18714}.Pos.XY + new Vector2(0f, 10f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				TextBlockBuilder.CreateBlock({18714}.Pos.WH.X - 60f, Local.gamewiki_crafts_intro, Color.White * 0.5f, Fonts.Philosopher_14, 0f).Create()
			});
			stackForm.AddSpace(30f);
			StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_14, -1f);
			TextBlockBuilder textBlockBuilder2 = new TextBlockBuilder(Fonts.Philosopher_14, -1f);
			int num = 0;
			foreach (WosbCrafting.Recepie recepie in WosbCrafting.Workshop)
			{
				TextBlockBuilder textBlockBuilder3 = (num++ % 2 == 0) ? textBlockBuilder : textBlockBuilder2;
				if (!recepie.OutputIsGold)
				{
					ResourceInfo resourceInfo = recepie.Output as ResourceInfo;
					if (resourceInfo != null && resourceInfo.MediumCost.Value > 50 && resourceInfo.ID != 24)
					{
						textBlockBuilder3.WriteLine("", Color.White);
						textBlockBuilder3.WriteImage(resourceInfo.IconTexture, resourceInfo.IconTexture.Bounds, 0.45f, null);
						if (recepie.OutputItemCount.Value != 1)
						{
							textBlockBuilder3.Write(" " + recepie.OutputItemCount.Value.ToString() + "x " + recepie.Output.getName, Color.White * 0.95f, Fonts.Philosopher_16, null, false);
						}
						else
						{
							textBlockBuilder3.Write(" " + recepie.Output.getName, Color.White * 0.95f, Fonts.Philosopher_16, null, false);
						}
						if (!Fonts.CurrentLanguageIsCjk)
						{
							stackForm.AddSpace(5f);
						}
						foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>)recepie.Craft.InputItems.ResourceInfo))
						{
							textBlockBuilder3.WriteLine("", Color.White);
							textBlockBuilder3.WriteImage(gsilocalEnumerablePair.Info.IconTexture, gsilocalEnumerablePair.Info.IconTexture.Bounds, 0.3f, null);
							textBlockBuilder3.Write(gsilocalEnumerablePair.Count.ToString() + "x " + gsilocalEnumerablePair.Info.Name, new Color(123, 113, 113) * 1.2f);
						}
						textBlockBuilder3.WriteLine(" ", Color.White);
					}
				}
			}
			stackForm2.AddItem(new UiControl[]
			{
				textBlockBuilder.Create()
			});
			stackForm2.AddSpace(50f);
			stackForm2.AddItem(new UiControl[]
			{
				textBlockBuilder2.Create()
			});
			stackForm.AddItem(new UiControl[]
			{
				stackForm2
			});
			{18714}.AddChild(stackForm);
			base.AddChild({18714});
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000427B0 File Offset: 0x000409B0
		private void {18715}(StackForm {18716}, GamepediaArticleInfo {18717}, Form {18718})
		{
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_18, 0f);
			textBlockBuilder.WriteLine({18717}.Title, Color.White * 0.95f, Fonts.Philosopher_18);
			{18716}.AddItem(new UiControl[]
			{
				textBlockBuilder.Create(Vector2.Zero)
			});
			if ({18717}.TexturePath.Width > 0)
			{
				{18716}.AddItem(new UiControl[]
				{
					new Image(Vector2.Zero, OtherTextures.Images, {18717}.TexturePath, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				{18716}.AddSpace(5f);
			}
			Color textColor = new Color(123, 113, 113) * 1.2f;
			Color hlTextColor = Color.Gold * 0.7f;
			Color searchTextColor = Color.Lime;
			Func<string, Color> <>9__0;
			foreach (string text in {18717}.GetTextParagraphs())
			{
				{18716}.AddSpace(20f);
				TextBlockBuilder textBlockBuilder2;
				if (!string.IsNullOrEmpty(this.{18723}))
				{
					float {13602} = {18718}.Pos.WH.X - 60f;
					string {13603} = text;
					CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
					CustomSpriteFont philosopher_14Bold = Fonts.Philosopher_14Bold;
					Func<string, Color> {13606};
					if (({13606} = <>9__0) == null)
					{
						{13606} = (<>9__0 = delegate(string {18736})
						{
							if (!{18736}.Contains(this.{18723}, StringComparison.OrdinalIgnoreCase))
							{
								return TextBlockBuilder.SpecialColorHighlight({18736}, textColor, hlTextColor);
							}
							return searchTextColor;
						});
					}
					textBlockBuilder2 = TextBlockBuilder.CreateBlockSpecial({13602}, {13603}, philosopher_, philosopher_14Bold, {13606}, 0);
				}
				else
				{
					textBlockBuilder2 = TextBlockBuilder.CreateBlockSpecial({18718}.Pos.WH.X - 60f, text, textColor, hlTextColor, Fonts.Philosopher_14, Fonts.Philosopher_14Bold, 0);
				}
				TextBlockBuilder textBlockBuilder3 = textBlockBuilder2;
				{18716}.AddItem(new UiControl[]
				{
					textBlockBuilder3.Create()
				});
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0004297C File Offset: 0x00040B7C
		protected override void UserUpdate(ref FrameTime {18719})
		{
			{18719}.EvaluteTimerMs(ref this.DisallowClosingMs);
			base.UserUpdate(ref {18719});
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00042A27 File Offset: 0x00040C27
		[CompilerGenerated]
		private void {18720}()
		{
			{18702}.CurrentInstance = null;
			if (this.{18724})
			{
				GameScene.DecreaseGameInput();
			}
		}

		// Token: 0x0400078D RID: 1933
		public static {18702} CurrentInstance;

		// Token: 0x0400078E RID: 1934
		private static readonly Marker c_itemsArea = new Marker(10f, 23f, 207f, 524f);

		// Token: 0x0400078F RID: 1935
		private static readonly Marker c_pageArea = new Marker({18702}.c_itemsArea.End.X, 23f, 519f, {18702}.c_itemsArea.WH.Y);

		// Token: 0x04000790 RID: 1936
		private static readonly Rectangle c_item = new Rectangle(2504, 3290, 212, 43);

		// Token: 0x04000791 RID: 1937
		private static readonly Rectangle c_itemSelected = new Rectangle(2504, 3253, 212, 36);

		// Token: 0x04000792 RID: 1938
		private GamepediaArticleInfo {18721};

		// Token: 0x04000793 RID: 1939
		private bool {18722};

		// Token: 0x04000794 RID: 1940
		private string {18723} = string.Empty;

		// Token: 0x04000795 RID: 1941
		private bool {18724};

		// Token: 0x04000796 RID: 1942
		public float DisallowClosingMs;
	}
}
