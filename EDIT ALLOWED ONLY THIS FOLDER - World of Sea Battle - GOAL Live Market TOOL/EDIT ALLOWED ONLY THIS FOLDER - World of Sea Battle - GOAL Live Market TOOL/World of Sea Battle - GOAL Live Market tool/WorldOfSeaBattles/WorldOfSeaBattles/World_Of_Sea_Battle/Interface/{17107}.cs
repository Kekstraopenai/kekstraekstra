using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Data;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000070 RID: 112
	internal sealed class {17107} : {17068}
	{
		// Token: 0x06000359 RID: 857 RVA: 0x0001BFD4 File Offset: 0x0001A1D4
		private static bool bigMode(int {17130})
		{
			return {17130} > 600;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001BFE0 File Offset: 0x0001A1E0
		public {17107}(string {17131}, string {17132}) : this("", {17131}, {17132}, null, true, null, new string[]
		{
			Local.to_continue
		})
		{
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001C00C File Offset: 0x0001A20C
		public {17107}(string {17133}, string {17134}, string {17135}, Action<int> {17136}, bool {17137}, CraftingRecipe {17138}, params string[] {17139})
		{
			Marker marker = Marker.FromCentrScreen(Engine.GS.UIArea.WidthHeight(), {17107}.c_main);
			base..ctor(marker.ScaleOfCenter({17107}.bigMode({17134}.Length + {17135}.Length) ? 1.1f : 1f), {17107}.c_main, {17137} ? {17068}.BlockingWay.BackgroundClosing : {17068}.BlockingWay.NoBackground, true);
			{17107} <>4__this = this;
			{17107} currentInstance = {17107}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.RemoveFromContainer();
			}
			{17107}.CurrentInstance = this;
			this.AllowDragDrop = true;
			base.EvRemoveFromContainer += delegate()
			{
				{17107}.CurrentInstance = null;
			};
			base.AddChild(new Label(base.Pos.XY + new Vector2(239f, 173f), Fonts.Philosopher_14, {17177}.textColor, {17133}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			float {17167} = 360f;
			float num = 0f;
			if ({17107}.bigMode({17134}.Length + {17135}.Length))
			{
				{17167} = 430f;
				num = 100f;
			}
			float num2 = {17107}.WriteBlock(this, {17134}, Fonts.Arial_10Bold, new Vector2(55f, 201f), {17167});
			if (!string.IsNullOrEmpty({17135}))
			{
				{17107}.WriteBlock(this, {17135}, Fonts.Arial_10, new Vector2(55f, 201f + num2 + 20f), {17167});
			}
			int num3 = 0;
			float num4 = 0f - (float)(Math.Max(0, {17139}.Length - 4) * 27);
			for (int i = 0; i < {17139}.Length; i++)
			{
				if ({17139}[i].Contains(Environment.NewLine))
				{
					num4 -= 20f;
				}
			}
			for (int i = 0; i < {17139}.Length; i++)
			{
				string text = {17139}[i];
				bool flag = false;
				string {17154} = text;
				if (text.StartsWith('X'))
				{
					flag = true;
					{17154} = text.Substring(1);
				}
				int index = num3;
				Form form = {17107}.MakeScrollButton(new Vector2(base.Pos.XY.X + 55f + 27f, base.Pos.XY.Y + 451f + num4 + num), {17154}, delegate(ClickUiEventArgs {17176})
				{
					Action<int> selectedVariant = {17136};
					if (selectedVariant != null)
					{
						selectedVariant(index);
					}
					<>4__this.BlockAndClose();
				});
				num4 += form.Pos.WH.Y + 8f;
				base.AddChild(form);
				num3++;
				if (flag)
				{
					form.Opacity = 0.6f;
					form.AllowMouseInput = false;
				}
			}
			if ({17138} != null)
			{
				StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(0f, 358f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				StackForm stackForm2 = new StackForm(base.Pos.XY + new Vector2(0f, 358f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				using (IEnumerator<GSILocalPair> enumerator = ((IEnumerable<GSILocalPair>){17138}.InputItems).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GSILocalPair name = enumerator.Current;
						if (name.ID != 252)
						{
							ResourceInfo resourceInfo = Gameplay.ItemsInfo[name.ID];
							this.smp_addCondition(stackForm, ref stackForm2, resourceInfo.IconTexture, resourceInfo, resourceInfo.IconTexture.Bounds, resourceInfo.Name, () => (int)Math.Ceiling((double)((1f - {17138}.ReduceCraftCost) * (float)name.Count)));
						}
					}
				}
				if ({17138}.InputMoney.Value > 0)
				{
					this.smp_addCondition(stackForm, ref stackForm2, CommonAtlas.Texture.Tex, null, CommonAtlas.goldIconSingleWithBackground40, Local.gold_s.TrimEnd(':'), () => {17138}.InputMoney.Value);
				}
				if ({17138}.InputItems[252] > 0)
				{
					this.smp_addCondition(stackForm, ref stackForm2, CommonAtlas.Texture.Tex, null, CommonAtlas.monetsIcon, Local.Monets2.TrimEnd(':'), () => {17138}.InputItems[252]);
				}
				stackForm.AddItem(new UiControl[]
				{
					stackForm2
				});
				UiControl uiControl = stackForm;
				marker = stackForm.Pos;
				uiControl.Pos = marker.SetX(base.Pos.XY.X + (float)({17107}.c_main.Width / 2) - stackForm.Pos.WH.X / 2f);
				base.AddChild(stackForm);
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001C4C8 File Offset: 0x0001A6C8
		public {17107}(string {17140}, string {17141}, string {17142}, int {17143}, Action<int> {17144}, bool {17145}, CraftingRecipe {17146}, params string[] {17147}) : base(Marker.FromCentrScreen(Engine.GS.UIArea.WidthHeight(), {17107}.c_main), {17107}.c_main, {17145} ? {17068}.BlockingWay.BackgroundClosing : {17068}.BlockingWay.NoBackground, true)
		{
			{17107} <>4__this = this;
			{17107} currentInstance = {17107}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.RemoveFromContainer();
			}
			{17107}.CurrentInstance = this;
			this.AllowDragDrop = true;
			base.EvRemoveFromContainer += delegate()
			{
				{17107}.CurrentInstance = null;
			};
			base.AddChild(new Label(base.Pos.XY + new Vector2(239f, 173f), Fonts.Philosopher_14, {17177}.textColor, {17140}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			float num = {17107}.WriteBlock(this, {17141}, Fonts.Arial_10Bold, new Vector2(55f, 201f), 360f);
			if (!string.IsNullOrEmpty({17142}))
			{
				{17107}.WriteBlock(this, {17142}, Fonts.Arial_10, new Vector2(55f, 201f + num + 20f), 360f);
			}
			int num2 = {17147}.Length / {17143} + (({17147}.Length % {17143} != 0) ? 1 : 0);
			int num3 = 82;
			float num4 = (base.Pos.WH.X - (float)(num3 * 2)) / (float){17143};
			float num5 = 0f;
			float num6 = 0f - (float)(Math.Max(0, {17147}.Length / {17143} - 5) * 27);
			float num7 = num6;
			for (int i = 0; i < {17147}.Length; i++)
			{
				string {17154} = {17147}[i];
				if (i != 0 && i % num2 == 0)
				{
					num7 = num6;
					num5 += num4;
				}
				int index = i;
				Form form = {17107}.MakeScrollButton(new Vector2(base.Pos.XY.X + (float)num3 + num5, base.Pos.XY.Y + 451f + num7), {17154}, delegate(ClickUiEventArgs {17170})
				{
					Action<int> selectedVariant = {17144};
					if (selectedVariant != null)
					{
						selectedVariant(index);
					}
					<>4__this.BlockAndClose();
				});
				num7 += form.Pos.WH.Y + 4f;
				base.AddChild(form);
			}
			if ({17146} != null)
			{
				StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(0f, 358f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				StackForm stackForm2 = new StackForm(base.Pos.XY + new Vector2(0f, 358f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				using (IEnumerator<GSILocalEnumerablePair<ResourceInfo>> enumerator = ((IEnumerable<GSILocalEnumerablePair<ResourceInfo>>){17146}.InputItems.ResourceInfo).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GSILocalEnumerablePair<ResourceInfo> name = enumerator.Current;
						this.smp_addCondition(stackForm, ref stackForm2, name.Info.IconTexture, name.Info, name.Info.IconTexture.Bounds, name.Info.Name, () => (int)Math.Ceiling((double)((1f - {17146}.ReduceCraftCost) * (float)name.Count)));
					}
				}
				if ({17146}.InputMoney.Value > 0)
				{
					this.smp_addCondition(stackForm, ref stackForm2, CommonAtlas.Texture.Tex, null, CommonAtlas.goldIconSingleWithBackground40, Local.gold_s, () => {17146}.InputMoney.Value);
				}
				stackForm.AddItem(new UiControl[]
				{
					stackForm2
				});
				stackForm.Pos = stackForm.Pos.SetX(base.Pos.XY.X + (float)({17107}.c_main.Width / 2) - stackForm.Pos.WH.X / 2f);
				base.AddChild(stackForm);
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001C8C8 File Offset: 0x0001AAC8
		public {17107}(string {17148}, string {17149}, string {17150}, PbsAttackWindow[] {17151}, Action<int> {17152})
		{
			{17107}.<>c__DisplayClass11_0 CS$<>8__locals1 = new {17107}.<>c__DisplayClass11_0();
			CS$<>8__locals1.allowedHours = {17151};
			CS$<>8__locals1.onClick = {17152};
			base..ctor(Marker.FromCentrScreen(Engine.GS.UIArea.WidthHeight(), {17107}.c_main), {17107}.c_main, {17068}.BlockingWay.BackgroundClosing, true);
			CS$<>8__locals1.<>4__this = this;
			{17107}.<>c__DisplayClass11_1 CS$<>8__locals2 = new {17107}.<>c__DisplayClass11_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			{17107} currentInstance = {17107}.CurrentInstance;
			if (currentInstance != null)
			{
				currentInstance.RemoveFromContainer();
			}
			{17107}.CurrentInstance = this;
			this.AllowDragDrop = true;
			base.EvRemoveFromContainer += delegate()
			{
				{17107}.CurrentInstance = null;
			};
			base.AddChild(new Label(base.Pos.XY + new Vector2(239f, 173f), Fonts.Philosopher_14, {17177}.textColor, {17148}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			float num = {17107}.WriteBlock(this, {17149}, Fonts.Arial_10Bold, new Vector2(55f, 201f), 360f);
			if (!string.IsNullOrEmpty({17150}))
			{
				{17107}.WriteBlock(this, {17150}, Fonts.Arial_10, new Vector2(55f, 201f + num + 20f), 360f);
			}
			CS$<>8__locals2.numberBox = new Label(Vector2.Zero, Fonts.Arial_12, Color.Black, CS$<>8__locals2.CS$<>8__locals1.allowedHours.First<PbsAttackWindow>().ToStringFull(null, false), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals2.currentIndex = 0;
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				new Button(Vector2.Zero, CommonAtlas.arrowBlack_left, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {17173})
				{
					base.<.ctor>g__Next|1(-1);
				})
			});
			Form form = new Form(new Marker(0f, 0f, 160f, 30f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AllowMouseInput = false
			};
			form.AddChildPos(CS$<>8__locals2.numberBox, PositionAlignment.LeftUp, PositionAlignment.Center, 0f);
			stackForm.AddItem(new UiControl[]
			{
				form
			});
			stackForm.AddItem(new UiControl[]
			{
				new Button(Vector2.Zero, CommonAtlas.arrowBlack_right, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {17174})
				{
					base.<.ctor>g__Next|1(1);
				})
			});
			CS$<>8__locals2.<.ctor>g__Next|1(0);
			base.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.LeftUp, 340f);
			StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm2.AddItem(new UiControl[]
			{
				{17107}.MakeScrollButton(Vector2.Zero, Local.accept, delegate(ClickUiEventArgs {17175})
				{
					Action<int> onClick = CS$<>8__locals2.CS$<>8__locals1.onClick;
					if (onClick != null)
					{
						onClick(CS$<>8__locals2.currentIndex);
					}
					CS$<>8__locals2.CS$<>8__locals1.<>4__this.BlockAndClose();
				})
			});
			stackForm2.AddSpace(5f);
			stackForm2.AddItem(new UiControl[]
			{
				{17107}.MakeScrollButton(Vector2.Zero, Local.undo, delegate(ClickUiEventArgs {17171})
				{
					CS$<>8__locals2.CS$<>8__locals1.<>4__this.BlockAndClose();
				})
			});
			base.AddChildPos(stackForm2, PositionAlignment.LeftUp, PositionAlignment.LeftUp, 70f, 450f, false);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001CB94 File Offset: 0x0001AD94
		private static Form MakeScrollButton(Vector2 {17153}, string {17154}, Action<ClickUiEventArgs> {17155})
		{
			TextBlockControl builder = TextBlockBuilder.CreateBlock(290f, {17154}, Color.White, Fonts.Arial_12, 0f).Create();
			builder.EvClick += {17155};
			builder.BasicColor = {17177}.textColor;
			builder.EvGotMouseFocus += delegate()
			{
				builder.BasicColor = Color.Green;
			};
			builder.EvLostMouseFocus += delegate()
			{
				builder.BasicColor = {17177}.textColor;
			};
			Form {12952} = new Form(Vector2.Zero, {17107}.c_scroll, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Form form = new Form(new Marker(ref {17153}, 30f + builder.Pos.WH.X, builder.Pos.WH.Y), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AnimatedFocus = false;
			form.AddChildPos({12952}, PositionAlignment.LeftUp, PositionAlignment.Center, 0f);
			form.AddChildPos(builder, PositionAlignment.RightDown, PositionAlignment.Center, 0f);
			return form;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001CC98 File Offset: 0x0001AE98
		private void smp_addCondition(StackForm {17156}, ref StackForm {17157}, Texture2D {17158}, IStorageAsset {17159}, Rectangle {17160}, string {17161}, Func<int> {17162})
		{
			if ({17157}.CountChild() == 5)
			{
				{17156}.AddItem(new UiControl[]
				{
					{17157}
				});
				{17157} = new StackForm({17156}.Pos.XY, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			}
			Form entry = new Form(new Marker(0f, 0f, 75f, 69f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			string {12778} = "";
			Tlist<ToolTipCharacteristics> tlist = new Tlist<ToolTipCharacteristics>();
			if ({17159} != null)
			{
				{12778} = {20431}.GenericItemConnection(tlist, {17159});
			}
			entry.ToolTip = new ToolTip(0f, float.MaxValue, new ToolTipState({17161}, {12778}, tlist.ToArray()));
			entry.AddChild(new Image(new Marker(17f, 0f, 40f, 40f), {17158}, {17160}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			LiveLabel live1 = null;
			live1 = new LiveLabel(new Vector2(0f, 39f), Fonts.Philosopher_14Bold, new Color(3, 98, 3), delegate()
			{
				int num = {17162}();
				if (live1 != null)
				{
					live1.Pos = live1.Pos.SetX(entry.Pos.XY.X + 37f - live1.Pos.WH.X / 2f);
				}
				return num.ToString();
			}, 16);
			entry.AddChild(live1);
			{17157}.AddItem(new UiControl[]
			{
				entry
			});
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001CDE4 File Offset: 0x0001AFE4
		public static float WriteBlock(Form {17163}, string {17164}, CustomSpriteFont {17165}, Vector2 {17166}, float {17167} = 360f)
		{
			string[] array = {17164}.Split(new string[]
			{
				Environment.NewLine
			}, StringSplitOptions.None);
			Tlist<string> tlist = new Tlist<string>();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].SplitSmart(new char[]
				{
					' '
				});
				if (array3.Length == 0)
				{
					Tlist<string> tlist2 = tlist;
					string text = " ";
					tlist2.Add(text);
				}
				string text2 = "";
				for (int j = 0; j < array3.Length; j++)
				{
					if ({17165}.Measure(text2 + array3[j]).X < {17167})
					{
						text2 += array3[j];
					}
					else
					{
						tlist.Add(text2);
						text2 = array3[j];
					}
					if (!array3[j].EndsWithCjk())
					{
						text2 += " ";
					}
				}
				if (!string.IsNullOrEmpty(text2))
				{
					tlist.Add(text2);
				}
			}
			float num = 0f;
			float num2 = 0f;
			foreach (string text3 in ((IEnumerable<string>)tlist))
			{
				int num3 = int.MaxValue;
				for (int k = 0; k < 11; k++)
				{
					int num4 = text3.IndexOf((k == 10) ? '#' : k.ToString()[0]);
					if (num4 != -1)
					{
						num3 = Math.Min(num4, num3);
					}
				}
				if (num3 == 2147483647)
				{
					{17163}.AddChild(new Label({17163}.Pos.XY + {17166} + new Vector2(num, num2), {17165}, {17177}.textColor, text3, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
				}
				else
				{
					foreach (string text4 in text3.SplitSmart(new char[]
					{
						' '
					}))
					{
						Color textColor = {17177}.textColor;
						int num5 = 0;
						int num6 = 0;
						for (int l = 0; l < text4.Length; l++)
						{
							if ("x+-0123456789.,".IndexOf(text4[l]) != -1)
							{
								num5++;
							}
							if ("x+-.,".IndexOf(text4[l]) != -1)
							{
								num6++;
							}
						}
						string text5 = text4;
						if (text4.Contains('#') || (num5 == text4.Length && num6 < text4.Length))
						{
							textColor = new Color(154, 0, 0);
							text5 = text4.Replace("#", "");
						}
						{17163}.AddChild(new Label({17163}.Pos.XY + {17166} + new Vector2(num, num2), {17165}, textColor, text5, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
						num += {17165}.Measure(text5.EndsWithCjk() ? text5 : (text5 + " ")).X;
					}
				}
				num = 0f;
				num2 += 15f;
			}
			return num2;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001D0E8 File Offset: 0x0001B2E8
		public void AddDownText(string {17168})
		{
			TextBlockBuilder textBlockBuilder = TextBlockBuilder.CreateBlock(327f, {17168}, {17177}.textColor * 0.7f, Fonts.Arial_10, -1f);
			textBlockBuilder.replaceFontOrNull = Fonts.Arial_10;
			textBlockBuilder.ReplaceColor({17177}.textColor * 0.7f);
			TextBlockControl {13204} = textBlockBuilder.Create(base.Pos.XY + new Vector2(23f, 373f));
			base.AddChild({13204});
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001D168 File Offset: 0x0001B368
		protected override void UserBackRender()
		{
			if (this.{17169} = (Engine.GS.CurrentTexture != CommonAtlas.Texture.Tex))
			{
				Engine.GS.SetTexture(CommonAtlas.Texture);
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001D1A8 File Offset: 0x0001B3A8
		protected override void UserFrontRender()
		{
			if (this.{17169})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x040002A0 RID: 672
		public static readonly Rectangle c_main = new Rectangle(1781, 863, 476, 639);

		// Token: 0x040002A1 RID: 673
		public static readonly Rectangle c_scroll = new Rectangle(2258, 863, 23, 23);

		// Token: 0x040002A2 RID: 674
		public static readonly Rectangle c_dropdown_main = new Rectangle(2258, 901, 115, 35);

		// Token: 0x040002A3 RID: 675
		public static readonly Rectangle c_dropdown_item = new Rectangle(2375, 901, 115, 35);

		// Token: 0x040002A4 RID: 676
		public static readonly Rectangle c_dropdown_main_fullWidth = new Rectangle(2258, 937, 356, 35);

		// Token: 0x040002A5 RID: 677
		public static readonly Rectangle c_dropdown_item_fullWidth = new Rectangle(2258, 973, 356, 35);

		// Token: 0x040002A6 RID: 678
		public static {17107} CurrentInstance;

		// Token: 0x040002A7 RID: 679
		private bool {17169};
	}
}
