using System;
using System.IO;
using Common;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000145 RID: 325
	internal class {18481} : {17068}
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x0003B270 File Offset: 0x00039470
		public {18481}()
		{
			Rectangle uiarea = Engine.GS.UIArea;
			base..ctor(Marker.FromCentrScreen(new Marker(ref uiarea), new Rectangle(0, 260, 480, 604)), new Rectangle(0, 260, 500, 680), {17068}.BlockingWay.BackgroundClosing, false);
			base.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat, Local.author_list, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 15f);
			ScrollBarControl scrollBarControl = new ScrollBarControl(new Marker(base.Pos.XY.X + base.Pos.WH.X - (float)AtlasPortGui.scrollBar_Pointer.Width - 10f, base.Pos.XY.Y + 80f, (float)AtlasPortGui.scrollBar_Pointer.Width, base.Pos.WH.Y - 100f), Rectangle.Empty, Rectangle.Empty, AtlasPortGui.scrollBar_Pointer, AtlasPortGui.whitePixel, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			ListItemViewControl listItemViewControl = new ListItemViewControl(new Marker(base.Pos.XY.X + 20f, base.Pos.XY.Y + 80f, base.Pos.WH.X - scrollBarControl.Pos.WH.X, base.Pos.WH.Y - 100f), scrollBarControl, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(new UiControl[]
			{
				scrollBarControl,
				listItemViewControl
			});
			foreach (string text in File.ReadAllLines("CommonData\\authors.xnb"))
			{
				StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				if (string.IsNullOrEmpty(text))
				{
					listItemViewControl.AddItem(new UiControl[]
					{
						new Form(new Marker(0f, 0f, 5f, 5f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
				}
				else if (text.Contains(":") || text.StartsWith("-"))
				{
					listItemViewControl.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.F_m14_ThinBold, Color.Wheat * 0.9f, text, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
				}
				else
				{
					stackForm.AddItem(new UiControl[]
					{
						new Label(Vector2.Zero, Fonts.F_m14_ThinBold, Color.Wheat * 0.5f, "☆ ", PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					});
					stackForm.AddItem(new UiControl[]
					{
						TextBlockBuilder.CreateBlock(base.Pos.WH.X - 90f, text, Color.Wheat * 0.7f, Fonts.F_m14_ThinBold, 0f).Create(Vector2.Zero)
					});
					listItemViewControl.AddItem(new UiControl[]
					{
						stackForm
					});
				}
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0003B55D File Offset: 0x0003975D
		protected override void UserBackRender()
		{
			this.{18482} = (Engine.GS.CurrentTexture != AtlasPortGui.Texture.Tex);
			if (this.{18482})
			{
				Engine.GS.SetTexture(AtlasPortGui.Texture);
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0003B595 File Offset: 0x00039795
		protected override void UserFrontRender()
		{
			if (this.{18482})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x040006A3 RID: 1699
		private bool {18482};
	}
}
