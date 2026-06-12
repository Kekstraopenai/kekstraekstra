using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Graphics;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using WorldOfSeaBattles;
using World_Of_Sea_Battle.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200014F RID: 335
	internal sealed class {18542} : {17068}, IUiEditorConnection
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x0003C8D8 File Offset: 0x0003AAD8
		public {18542}() : base(new Marker(100f, 150f, 350f, 400f), {17625}.c_back1, {17068}.BlockingWay.NoBackground, false)
		{
			this.AllowDragDrop = true;
			this.{18545}();
			base.EvRemoveFromContainer += delegate()
			{
				UiControl.EditorMode = null;
			};
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000201BB File Offset: 0x0001E3BB
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(CommonAtlas.Texture);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000201CC File Offset: 0x0001E3CC
		protected override void UserFrontRender()
		{
			Engine.GS.ReturnBackTexture();
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0003C93D File Offset: 0x0003AB3D
		void IUiEditorConnection.{18543}(UiControl {18544})
		{
			this.{18552} = {18544};
			this.{18545}();
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0003C94C File Offset: 0x0003AB4C
		private void {18545}()
		{
			base.ClearAllChild();
			StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(7f, 5f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			StackForm stackForm2 = stackForm;
			UiControl[] array = new UiControl[1];
			array[0] = new LabelButton(Vector2.Zero, "Reload AtlasGameGui", Fonts.F_m14_ThinBold, Color.Gray, Color.White, delegate(ClickUiEventArgs {18553})
			{
				{18542}.<update>g__reloadTex|5_0(1);
			});
			stackForm2.AddItem(array);
			StackForm stackForm3 = stackForm;
			UiControl[] array2 = new UiControl[1];
			array2[0] = new LabelButton(Vector2.Zero, "Reload AtlasEntryGui", Fonts.F_m14_ThinBold, Color.Gray, Color.White, delegate(ClickUiEventArgs {18554})
			{
				{18542}.<update>g__reloadTex|5_0(2);
			});
			stackForm3.AddItem(array2);
			StackForm stackForm4 = stackForm;
			UiControl[] array3 = new UiControl[1];
			array3[0] = new LabelButton(Vector2.Zero, "Reload AtlasPortGui", Fonts.F_m14_ThinBold, Color.Gray, Color.White, delegate(ClickUiEventArgs {18555})
			{
				{18542}.<update>g__reloadTex|5_0(3);
			});
			stackForm4.AddItem(array3);
			StackForm stackForm5 = stackForm;
			UiControl[] array4 = new UiControl[1];
			array4[0] = new LabelButton(Vector2.Zero, "Reload CommonAtlas", Fonts.F_m14_ThinBold, Color.Gray, Color.White, delegate(ClickUiEventArgs {18556})
			{
				{18542}.<update>g__reloadTex|5_0(4);
			});
			stackForm5.AddItem(array4);
			stackForm.AddItem(new UiControl[]
			{
				new Form(new Marker(0f, 0f, 1f, 20f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			if (this.{18552} != null)
			{
				stackForm.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.Philosopher_14, Color.White, "Selected: " + this.{18552}.GetType().Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				stackForm.AddItem(new UiControl[]
				{
					new LiveLabel(Vector2.Zero, Fonts.Arial_12, Color.White, new Func<string>(this.{18547}), 100)
				});
				if (this.{18552}.AllowResizeEditor)
				{
					stackForm.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Arial_8, Color.White, "Size_X: ", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					stackForm.AddItem(new UiControl[]
					{
						new TextBox(Vector2.Zero, CommonAtlas.newToolList_item, Fonts.Philosopher_14Bold, Color.White, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp).BindNumerticChange((int)this.{18552}.Pos.WH.X, new Action<int>(this.{18548}))
					});
					stackForm.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Arial_8, Color.White, "Size_Y: ", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					stackForm.AddItem(new UiControl[]
					{
						new TextBox(Vector2.Zero, CommonAtlas.newToolList_item, Fonts.Philosopher_14Bold, Color.White, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp).BindNumerticChange((int)this.{18552}.Pos.WH.Y, new Action<int>(this.{18550}))
					});
				}
				Form colorForm;
				stackForm.AddItem(new UiControl[]
				{
					colorForm = new Form(new Marker(0f, 0f, 50f, 20f), CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				colorForm.BasicColor = this.{18552}.BasicColor;
				stackForm.AddItem(new UiControl[]
				{
					new Label(Vector2.Zero, Fonts.Arial_8, Color.White, "Basic_Col (,,,): ", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				});
				TextBox textBox = new TextBox(Vector2.Zero, CommonAtlas.newToolList_item, Fonts.Philosopher_14Bold, Color.White, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				textBox.Text = string.Concat(new string[]
				{
					this.{18552}.BasicColor.R.ToString(),
					",",
					this.{18552}.BasicColor.G.ToString(),
					",",
					this.{18552}.BasicColor.B.ToString(),
					",",
					this.{18552}.BasicColor.A.ToString(),
					","
				});
				textBox.EvTextChanged += delegate(string {18557})
				{
					{18557} = {18557}.Replace(" ", "");
					string[] array5 = {18557}.Split(',', StringSplitOptions.None);
					Rectangle rectangle;
					if (int.TryParse(array5[0], out rectangle.X) && int.TryParse(array5[1], out rectangle.Y) && int.TryParse(array5[2], out rectangle.Width) && int.TryParse(array5[3], out rectangle.Height))
					{
						this.{18552}.BasicColor = new Color(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
					}
					colorForm.BasicColor = this.{18552}.BasicColor;
				};
				stackForm.AddItem(new UiControl[]
				{
					textBox
				});
				UiControl uiControl = this.{18552};
				Form form = uiControl as Form;
				if (form != null)
				{
					stackForm.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Arial_8, Color.White, "Path (,,,): ", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					textBox = new TextBox(Vector2.Zero, CommonAtlas.newToolList_item, Fonts.Philosopher_14Bold, Color.White, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					textBox.Text = string.Concat(new string[]
					{
						form.TexturePath.X.ToString(),
						",",
						form.TexturePath.Y.ToString(),
						",",
						form.TexturePath.Width.ToString(),
						",",
						form.TexturePath.Height.ToString(),
						","
					});
					textBox.EvTextChanged += delegate(string {18558})
					{
						{18558} = {18558}.Replace(" ", "");
						string[] array5 = {18558}.Split(',', StringSplitOptions.None);
						Rectangle texturePath;
						if (int.TryParse(array5[0], out texturePath.X) && int.TryParse(array5[1], out texturePath.Y) && int.TryParse(array5[2], out texturePath.Width) && int.TryParse(array5[3], out texturePath.Height))
						{
							form.TexturePath = texturePath;
						}
					};
					stackForm.AddItem(new UiControl[]
					{
						textBox
					});
				}
				uiControl = this.{18552};
				Label label = uiControl as Label;
				if (label != null)
				{
					stackForm.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.Arial_8, Color.White, "Text: ", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					textBox = new TextBox(Vector2.Zero, CommonAtlas.newToolList_item, Fonts.Philosopher_14Bold, Color.White, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					textBox.Text = label.Text;
					textBox.EvTextChanged += delegate(string {18559})
					{
						label.Text = {18559};
					};
					stackForm.AddItem(new UiControl[]
					{
						textBox
					});
				}
			}
			base.AddChild(stackForm);
			base.MoveToFrontLevel();
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0003CFA8 File Offset: 0x0003B1A8
		[CompilerGenerated]
		internal static void <update>g__reloadTex|5_0(int {18546})
		{
			string str = "C:\\Users\\Sergey\\Desktop\\WOS - Solution\\Data\\";
			string str2 = ".png";
			if ({18546} == 1)
			{
				AtlasGameGui.Texture = Texture2DAtlas.FromPNG(AtlasGameGui.Texture.Tex, str + PathContent.tex_pack_gamegui + str2, AtlasGameGui.Texture.Tex.Width, AtlasGameGui.Texture.Tex.Height);
			}
			if ({18546} == 2)
			{
				AtlasEntryGui.Texture = Texture2DAtlas.FromPNG(AtlasEntryGui.Texture.Tex, str + PathContent.tex_pack_entrygui + str2, AtlasEntryGui.Texture.Tex.Width, AtlasEntryGui.Texture.Tex.Height);
			}
			if ({18546} == 3)
			{
				AtlasPortGui.Texture = Texture2DAtlas.FromPNG(AtlasPortGui.Texture.Tex, str + PathContent.tex_pack_portgui + str2, AtlasPortGui.Texture.Tex.Width, AtlasPortGui.Texture.Tex.Height);
			}
			if ({18546} == 4)
			{
				CommonAtlas.Texture = Texture2DAtlas.FromPNG(CommonAtlas.Texture.Tex, str + PathContent.tex_pack_commongui + str2, CommonAtlas.Texture.Tex.Width, CommonAtlas.Texture.Tex.Height);
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0003D0CC File Offset: 0x0003B2CC
		[CompilerGenerated]
		private string {18547}()
		{
			return "relative: " + (this.{18552}.Pos.XY - ((this.{18552}.GetParent == null) ? Vector2.Zero : this.{18552}.GetParent.Pos.XY)).ToString();
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0003D130 File Offset: 0x0003B330
		[CompilerGenerated]
		private void {18548}(int {18549})
		{
			this.{18552}.Pos = this.{18552}.Pos.SetWidth((float){18549});
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0003D160 File Offset: 0x0003B360
		[CompilerGenerated]
		private void {18550}(int {18551})
		{
			this.{18552}.Pos = this.{18552}.Pos.SetHeight((float){18551});
		}

		// Token: 0x040006C6 RID: 1734
		private UiControl {18552};
	}
}
